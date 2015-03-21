using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
				var builder = new StringBuilder("PIN,Owner Full Name,Owner Address,Owner City,Owner State,Owner ZIP,Property Number,Property Direction,Property Street,Property Apartment,Property City,Property ZIP,Property Class,Property School District,Property Acerage,Property SEV 2014,Property Tax 2014");
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
					builder.Append(parcel.PropertyClass);
					builder.Append(',');
					builder.Append(parcel.SchoolDistrict);
					builder.Append(',');
					builder.Append(parcel.Acreage);
					builder.Append(',');
					builder.Append("\"" + parcel.StateEqualizedValue2014.ToString("C0") + "\"");
					builder.Append(',');
					builder.Append("\"" + parcel.TaxableValue2014.ToString("C0") + "\"");
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
