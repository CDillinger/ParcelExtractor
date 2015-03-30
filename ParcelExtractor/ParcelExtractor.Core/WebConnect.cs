/*

 *  ParcelExtractor - Ingham County Parcel Data Extraction
 *  Copyright (C) 2015  Collin Dillinger

 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.

 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.

 *  You should have received a copy of the GNU General Public License along
 *  with this program; if not, write to the Free Software Foundation, Inc.,
 *  51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

 */

using ParcelExtractor.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ParcelExtractor.Core
{
	public class WebConnect
	{
		private readonly HttpClient _client;

		public WebConnect()
		{
			_client = new HttpClient();
			_client.DefaultRequestHeaders.Add("soapaction", "http://tempuri.org/IParcelServiceApp/GetData");
		}

		public enum SearchType
		{
			Address,
			LastName,
			ParcelNumber
		}

		private static string ComposeRequestString(SearchType searchType, string query)
		{
			var typeString = "";
			switch (searchType)
			{
				case SearchType.Address:
					typeString = "AddrQuery2";
					break;
				case SearchType.ParcelNumber:
					typeString = "PinQuery2";
					break;
				case SearchType.LastName:
					typeString = "OwnerQuery2";
					break;
			}

			return string.Format("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\"><s:Body><GetData xmlns=\"http://tempuri.org/\"><spName>{0}</spName><queryValue>{1}%</queryValue></GetData></s:Body></s:Envelope>", typeString, query);
		}

		public async Task<Envelope> QueryAsync(SearchType searchType, string query = "", string query2 = "")
		{
			if (searchType == SearchType.Address)
				query = string.Format("{0},{1}", query, query2);

			query = StringUtils.EncodeXML(query);

			// Try to query the service...
			try
			{
				var response = await _client.PostAsync("http://ingham-equalization.rsgis.msu.edu/ParcelServiceApp/ParcelServiceApp.svc", new StringContent(ComposeRequestString(searchType, query), Encoding.UTF8, "text/xml"));
				if (!response.IsSuccessStatusCode)
					throw new ConnectionFailedException();

				var stream = await response.Content.ReadAsStreamAsync();
				var serializer = new XmlSerializer(typeof(Envelope));
				return (Envelope)serializer.Deserialize(stream);
			}
			catch (Exception)
			{
				throw new ConnectionFailedException();
			}
		}

		public async Task<bool> QueryAppendAsync(List<Parcel> parcels, SearchType searchType, string query = "", string query2 = "")
		{
			var envelope = await QueryAsync(searchType, query, query2);
			parcels.AddRange(envelope.Results);
			return true;
		}
	}
}
