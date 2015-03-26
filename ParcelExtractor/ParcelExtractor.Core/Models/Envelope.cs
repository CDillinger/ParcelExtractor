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
		private double _acreage;
		private string _lastName;
		private string _owner;
		private string _ownerAddress;
		private string _ownerCity;
		private string _ownerState;
		private string _ownerZIP;
		private string _pin;
		private string _parcelNumber;
		private string _pre;
		private string _propertyApartment;
		private string _propertyCity;
		private uint _propertyClass;
		private string _propertyDirection;
		private string _propertyNumber;
		private string _propertyStreet;
		private string _propertyZIP;
		private uint _schoolDistrict;
		private double _stateEqualizedValue2014;
		private double _taxableValue2014;
		private string _center;
		private string _geo;
		private string _geom;

		[XmlElement("Acreage")]
		public double Acreage
		{
			get { return _acreage; }
			set { _acreage = value; }
		}

		[XmlElement("LastName")]
		public string LastName
		{
			get { return StringUtils.HandleIssues(_lastName); }
			set { _lastName = value; }
		}

		[XmlElement("Owner")]
		public string Owner
		{
			get { return StringUtils.HandleIssues(_owner); }
			set { _owner = value; }
		}

		[XmlElement("OwnerAddress")]
		public string OwnerAddress
		{
			get { return StringUtils.HandleIssues(_ownerAddress); }
			set { _ownerAddress = value; }
		}

		[XmlElement("OwnerCity")]
		public string OwnerCity
		{
			get { return StringUtils.HandleIssues(_ownerCity); }
			set { _ownerCity = value; }
		}

		[XmlElement("OwnerState")]
		public string OwnerState
		{
			get { return StringUtils.HandleIssues(_ownerState); }
			set { _ownerState = value; }
		}

		[XmlElement("OwnerZIP")]
		public string OwnerZIP
		{
			get { return StringUtils.HandleIssues(_ownerZIP); }
			set { _ownerZIP = value; }
		}

		[XmlElement("PIN")]
		public string PIN
		{
			get { return StringUtils.HandleIssues(_pin); }
			set { _pin = value; }
		}

		[XmlElement("Parcel")]
		public string ParcelNumber
		{
			get { return StringUtils.HandleIssues(_parcelNumber); }
			set { _parcelNumber = value; }
		}

		[XmlElement("Pre")]
		public string Pre
		{
			get { return StringUtils.HandleIssues(_pre); }
			set { _pre = value; }
		}

		[XmlElement("PropertyApartment")]
		public string PropertyApartment
		{
			get { return StringUtils.HandleIssues(_propertyApartment); }
			set { _propertyApartment = value; }
		}

		[XmlElement("PropertyCity")]
		public string PropertyCity
		{
			get { return StringUtils.HandleIssues(_propertyCity); }
			set { _propertyCity = value; }
		}

		[XmlElement("PropertyClass")]
		public uint PropertyClass // TODO: get enum values
		{
			get { return _propertyClass; }
			set { _propertyClass = value; }
		}

		[XmlElement("PropertyDirection")]
		public string PropertyDirection
		{
			get { return StringUtils.HandleIssues(_propertyDirection); }
			set { _propertyDirection = value; }
		}

		[XmlElement("PropertyNumber")]
		public string PropertyNumber
		{
			get { return StringUtils.HandleIssues(_propertyNumber); }
			set { _propertyNumber = value; }
		}

		[XmlElement("PropertyStreet")]
		public string PropertyStreet
		{
			get { return StringUtils.HandleIssues(_propertyStreet); }
			set { _propertyStreet = value; }
		}

		[XmlElement("PropertyZIP")]
		public string PropertyZIP
		{
			get { return StringUtils.HandleIssues(_propertyZIP); }
			set { _propertyZIP = value; }
		}

		[XmlElement("SchoolDistrict")]
		public uint SchoolDistrict // TODO: get enum values
		{
			get { return _schoolDistrict; }
			set { _schoolDistrict = value; }
		}

		[XmlElement("Sev2014")]
		public double StateEqualizedValue2014
		{
			get { return _stateEqualizedValue2014; }
			set { _stateEqualizedValue2014 = value; }
		}

		[XmlElement("Tax2014")]
		public double TaxableValue2014
		{
			get { return _taxableValue2014; }
			set { _taxableValue2014 = value; }
		}

		[XmlElement("center")]
		public string Center
		{
			get { return StringUtils.HandleIssues(_center); }
			set { _center = value; }
		}

		[XmlElement("geo")]
		public string Geo
		{
			get { return StringUtils.HandleIssues(_geo); }
			set { _geo = value; }
		}

		[XmlElement("geom")]
		public string Geom
		{
			get { return StringUtils.HandleIssues(_geom); }
			set { _geom = value; }
		}

		public Uri AssessingDataURI
		{
			get { return new Uri(string.Format("https://is.bsasoftware.com/bsa.is/AssessingServices/ServiceAssessingDetails.aspx?p=1&dp={0}&i=1&appid=0&unit=383", PIN)); }
		}
	}
}