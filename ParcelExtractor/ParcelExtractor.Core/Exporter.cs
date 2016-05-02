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
using System.Collections.Generic;
using System.IO;
using System.Text;
using ParcelExtractor.Core.Models;

namespace ParcelExtractor.Core
{
	public static class Exporter
	{
		public static bool ExportToCSV(Envelope envelope, string saveLocation)
		{
			return ExportToCSV(envelope.Results, saveLocation);
		}

		public static bool ExportToCSV(IEnumerable<Parcel> parcels, string saveLocation)
		{
			try
			{
				var builder = new StringBuilder("PIN,Owner Full Name,Owner Address,Owner City,Owner State,Owner ZIP,Property Number,Property Direction,Property Street,Property Apartment,Property City,Property ZIP,Property Class,Property School District,Property Acerage,Property SEV,Property Tax");
				foreach (var parcel in parcels)
				{
					builder.AppendLine();
					builder.Append(parcel.PIN);
					builder.Append(',');
					builder.Append(parcel.Owner);
					builder.Append(',');
					builder.Append(parcel.OwnerAddress);
					builder.Append(',');
					builder.Append(parcel.OwnerCity);
					builder.Append(',');
					builder.Append(parcel.OwnerState);
					builder.Append(',');
					builder.Append(parcel.OwnerZIP);
					builder.Append(',');
					builder.Append(parcel.PropertyNumber);
					builder.Append(',');
					builder.Append(parcel.PropertyDirection);
					builder.Append(',');
					builder.Append(parcel.PropertyStreet);
					builder.Append(',');
					builder.Append(parcel.PropertyApartment);
					builder.Append(',');
					builder.Append(parcel.PropertyCity);
					builder.Append(',');
					builder.Append(parcel.PropertyZIP);
					builder.Append(',');
					builder.Append(parcel.PropertyClassNumber + ": " + parcel.PropertyClassString);
					builder.Append(',');
					builder.Append(parcel.SchoolDistrict);
					builder.Append(',');
					builder.Append(parcel.Acreage);
					builder.Append(',');
					builder.Append("\"" + parcel.StateEqualizedValue.ToString("C0") + "\"");
					builder.Append(',');
					builder.Append("\"" + parcel.TaxableValue.ToString("C0") + "\"");
					builder.Append(',');
				}
				File.WriteAllText(saveLocation, builder.ToString());
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}
	}
}
