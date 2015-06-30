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
using ParcelExtractor.Core.Models;

namespace ParcelExtractor.Core
{
	public static class StringUtils
	{
		public static string EncodeXML(string str)
		{
			if (str.Contains("&"))
			{
				// Special care for "&", because the method used for others would cause an endless loop
				var str2 = str;
				var indices = new List<int>();

				while (str2.Contains("&"))
				{
					var index = str2.LastIndexOf("&", StringComparison.Ordinal);
					if (index != -1)
					{
						str2 = str2.Substring(0, index);
						indices.Add(index);
					}
					else
					{
						break;
					}
				}

				foreach (var index in indices)
				{
					var part1 = str.Substring(0, index);
					var part2 = str.Substring(index + 1);
					str = part1 + "&amp;" + part2;
				}
			}

			while (str.Contains("<"))
			{
				var part1 = str.Substring(0, str.IndexOf("<", StringComparison.Ordinal));
				var part2 = str.Substring(str.IndexOf("<", StringComparison.Ordinal) + 1);
				str = part1 + "&lt;" + part2;
			}

			while (str.Contains(">"))
			{
				var part1 = str.Substring(0, str.IndexOf(">", StringComparison.Ordinal));
				var part2 = str.Substring(str.IndexOf(">", StringComparison.Ordinal) + 1);
				str = part1 + "&gt;" + part2;
			}

			return str;
		}

		public static string HandleIssues(string str)
		{
			if ((str.StartsWith("\"") && !str.EndsWith("\"")) || (!str.StartsWith("\"") && str.EndsWith("\"")))
				str = str.Substring(1);

			if (str.Contains(",") && !str.Contains("\""))
				str = "\"" + str + "\"";

			return str;
		}

		public static string UnencodeXML(string str)
		{
			if (str.Contains("&amp;"))
			{
				// Special care for "&", because the method used for others would cause an endless loop
				var str2 = str;
				var indices = new List<int>();

				while (str2.Contains("&amp;"))
				{
					var index = str2.LastIndexOf("&amp;", StringComparison.Ordinal);
					if (index != -1)
					{
						str2 = str2.Substring(0, index);
						indices.Add(index);
					}
					else
					{
						break;
					}
				}

				foreach (var index in indices)
				{
					var part1 = str.Substring(0, index);
					var part2 = str.Substring(index + 1);
					str = part1 + "&" + part2;
				}
			}

			while (str.Contains("&lt;"))
			{
				var part1 = str.Substring(0, str.IndexOf("&lt;", StringComparison.Ordinal));
				var part2 = str.Substring(str.IndexOf("&lt;", StringComparison.Ordinal) + 4);
				str = part1 + '<' + part2;
			}

			while (str.Contains("&gt;"))
			{
				var part1 = str.Substring(0, str.IndexOf("&gt;", StringComparison.Ordinal));
				var part2 = str.Substring(str.IndexOf("&gt;", StringComparison.Ordinal) + 4);
				str = part1 + '>' + part2;
			}

			return str;
		}

		public static string ParcelPropertyClassToFriendlyString(ParcelPropertyClass propertyClass)
		{
			switch (propertyClass)
			{
				case ParcelPropertyClass.Exempt:
					return "Exempt";
				case ParcelPropertyClass.ExemptPersonalProperty:
					return "Exempt - Personal Property";
				case ParcelPropertyClass.ExemptRealProperty:
					return "Exempt - Real Property";
				case ParcelPropertyClass.AgriculturalPropertyImproved:
					return "Agricultural Property - Improved";
				case ParcelPropertyClass.AgriculturalPropertyVacant:
					return "Agricultural Property - Vacant";
				case ParcelPropertyClass.MichiganDNR:
					return "Michigan Department of Natural Resources";
				case ParcelPropertyClass.CommercialPropertyImproved:
					return "Commercial Property - Improved";
				case ParcelPropertyClass.CommercialPropertyVacant:
					return "Commercial Property - Vacant";
				case ParcelPropertyClass.CommercialPropertyCondos:
					return "Commercial Property - Condominiums";
				case ParcelPropertyClass.IndustrialPropertyImproved:
					return "Industrial Property - Improved";
				case ParcelPropertyClass.IndustrialPropertyVacant:
					return "Industrial Property - Vacant";
				case ParcelPropertyClass.ResidentialPropertyImproved:
					return "Residential Property - Improved";
				case ParcelPropertyClass.ResidentialPropertyVacant:
					return "Residential Property - Vacant";
				case ParcelPropertyClass.PrivateRoad:
					return "Private Road";
				case ParcelPropertyClass.DevelopmentalPropertyImproved:
					return "Developmental Property - Improved";
				case ParcelPropertyClass.DevelopmentalPropertyVacant:
					return "Developmental Property - Vacant";
				case ParcelPropertyClass.OPRAFrozen:
					return "Obsolete Property Rehabilitation Act - Frozen";
				case ParcelPropertyClass.LandBank:
					return "Land Bank Ownership";
				case ParcelPropertyClass.CountyLandBankSale:
				case ParcelPropertyClass.CountyLandBankSale2:
					return "County Land Bank Sale";
				case ParcelPropertyClass.RefundPersonalProperty:
					return "Refund - Personal Property";
				default:
					return "Unknown";
			}
		}
	}
}
