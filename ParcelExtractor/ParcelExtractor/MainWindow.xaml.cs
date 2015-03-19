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

using System.Windows;
using ParcelExtractor.Core;
using System.Threading.Tasks;

namespace ParcelExtractor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly WebConnect _connector;

		public MainWindow()
		{
			InitializeComponent();
			_connector = new WebConnect();
		}

		private async void ParcelNumberSearchButton_OnClick(object sender, RoutedEventArgs e)
		{
			await QueryAsync(WebConnect.SearchType.ParcelNumber, ParcelNumberTextBox.Text);
		}

		private async void OwnerLastNameSearchButton_OnClick(object sender, RoutedEventArgs e)
		{
			await QueryAsync(WebConnect.SearchType.LastName, OwnerLastNameTextBox.Text);
		}

		private async void AddressSearchButton_OnClick(object sender, RoutedEventArgs e)
		{
			await QueryAsync(WebConnect.SearchType.Address, StreetNumberTextBox.Text, StreetNameTextBox.Text);
		}

		private async Task QueryAsync(WebConnect.SearchType searchType, string query = "", string query2 = "")
		{
			StatusProgressBar.Value = 0;
			MainStatusTextBlock.Text = "Querying...";
			var results = await _connector.QueryAsync(searchType, query, query2);
			SecondaryStatusTextBlock.Text = results.Results.Length == 1 ? results.Results.Length + " Result" : results.Results.Length + " Results";
			StatusProgressBar.Value = 100;
			MainStatusTextBlock.Text = "Ready";
			if (!results.HasResults)
				MessageBox.Show("The query returned no results.", "No Results!");
		}
	}
}
