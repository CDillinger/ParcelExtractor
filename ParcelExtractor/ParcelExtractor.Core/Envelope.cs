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

using System;
using System.Xml.Serialization;

namespace ParcelExtractor.Core
{
	[XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
	public class Envelope
	{
		[XmlElement("Body")]
		public Body Body { get; set; }
		public Parcel[] Results { get { return Body.Response.Results; } }
		public bool HasResults { get { return Results.Length > 0; } }
	}

	[XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class Body
	{
		[XmlElement("GetDataResponse", Namespace = "http://tempuri.org/")]
		public DataResponse Response { get; set; }
	}

	[XmlType(AnonymousType = true, Namespace = "http://tempuri.org/")]
	[XmlRoot("GetDataResponse", Namespace = "http://tempuri.org/", IsNullable = false)]
	public class DataResponse
	{
		[XmlArray("GetDataResult")]
		[XmlArrayItem("clsParcel", Namespace = "http://schemas.datacontract.org/2004/07/ParcelServiceApp", IsNullable = false)]
		public Parcel[] Results { get; set; }
	}

	[XmlType(AnonymousType = true, Namespace = "http://schemas.datacontract.org/2004/07/ParcelServiceApp")]
	[XmlRoot("CLSParcel", Namespace = "http://schemas.datacontract.org/2004/07/ParcelServiceApp", IsNullable = false)]
	public class Parcel
	{
		[XmlElement("Acreage")]
		public double Acreage { get; set; }

		[XmlElement("LastName")]
		public string LastName { get; set; }

		[XmlElement("Owner")]
		public string Owner { get; set; }

		[XmlElement("OwnerAddress")]
		public string OwnerAddress { get; set; }

		[XmlElement("OwnerCity")]
		public string OwnerCity { get; set; }

		[XmlElement("OwnerState")]
		public string OwnerState { get; set; }

		[XmlElement("OwnerZIP")]
		public string OwnerZIP { get; set; }

		[XmlElement("PIN")]
		public string PIN { get; set; }

		[XmlElement("Parcel")]
		public string ParcelNumber { get; set; }

		[XmlElement("Pre")]
		public string Pre { get; set; }

		[XmlElement("PropertyApartment")]
		public string PropertyApartment { get; set; }

		[XmlElement("PropertyCity")]
		public string PropertyCity { get; set; }

		[XmlElement("PropertyClass")]
		public uint PropertyClass { get; set; } // TODO: get enum values

		[XmlElement("PropertyDirection")]
		public string PropertyDirection { get; set; }

		[XmlElement("PropertyNumber")]
		public string PropertyNumber { get; set; }

		[XmlElement("PropertyStreet")]
		public string PropertyStreet { get; set; }

		[XmlElement("PropertyZIP")]
		public string PropertyZIP { get; set; }

		[XmlElement("SchoolDistrict")]
		public uint SchoolDistrict { get; set; } // TODO: get enum values

		[XmlElement("Sev2014")]
		public double StateEqualizedValue2014 { get; set; }

		[XmlElement("Tax2014")]
		public double TaxableValue2014 { get; set; }

		[XmlElement("center")]
		public string Center { get; set; }

		[XmlElement("geo")]
		public string Geo { get; set; }

		[XmlElement("geom")]
		public string Geom { get; set; }

		public Uri AssessingDataURI
		{
			get { return new Uri(string.Format("https://is.bsasoftware.com/bsa.is/AssessingServices/ServiceAssessingDetails.aspx?p=1&dp={0}&i=1&appid=0&unit=383", PIN)); }
		}
	}
}