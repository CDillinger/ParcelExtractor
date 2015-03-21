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
using System.Windows;
using ParcelExtractor.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace ParcelExtractor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string[] _chars = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		private readonly WebConnect _connector;
		private bool _isQuerying;

		public MainWindow()
		{
			InitializeComponent();
			_connector = new WebConnect();
			App.SetMainWindow(this);
		}

		private async void ParcelNumberSearchButton_OnClick(object sender, RoutedEventArgs e)
		{
			await QueryAsync(WebConnect.SearchType.ParcelNumber, ParcelNumberTextBox.Text);
		}

		private async void ParcelNumberTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				await QueryAsync(WebConnect.SearchType.ParcelNumber, ParcelNumberTextBox.Text);
		}

		private async void OwnerLastNameSearchButton_OnClick(object sender, RoutedEventArgs e)
		{
			await QueryAsync(WebConnect.SearchType.LastName, OwnerLastNameTextBox.Text);
		}

		private async void OwnerLastNameTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				await QueryAsync(WebConnect.SearchType.LastName, OwnerLastNameTextBox.Text);
		}

		private async void AddressSearchButton_OnClick(object sender, RoutedEventArgs e)
		{
			await QueryAsync(WebConnect.SearchType.Address, StreetNumberTextBox.Text, StreetNameTextBox.Text);
		}

		private async void AddressTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				await QueryAsync(WebConnect.SearchType.Address, StreetNumberTextBox.Text, StreetNameTextBox.Text);
		}

		private async Task QueryAsync(WebConnect.SearchType searchType, string query = "", string query2 = "")
		{
			if (_isQuerying)
			{
				MessageBoxEx.Show(this, "Please wait for the current query to finish...");
				return;
			}


			if (query == "" && query2 == "")
			{
				MessageBoxEx.Show(this, "Please enter a search query!");
				return;
			}

			// Reset everything
			ParcelNumberSearchButton.IsEnabled = OwnerLastNameSearchButton.IsEnabled = AddressSearchButton.IsEnabled = GetAllButton.IsEnabled = false;
			SecondaryStatusTextBlock.Text = "0 Results";
			StatusProgressBar.Value = 0;
			MainStatusTextBlock.Text = "Querying...";
			_isQuerying = true;

			var results = await _connector.QueryAsync(searchType, query, query2);

			// Update everything
			ParcelNumberSearchButton.IsEnabled = OwnerLastNameSearchButton.IsEnabled = AddressSearchButton.IsEnabled = GetAllButton.IsEnabled = true;
			SecondaryStatusTextBlock.Text = results.Results.Length == 1 ? results.Results.Length + " Result" : results.Results.Length + " Results";
			StatusProgressBar.Value = 100;
			MainStatusTextBlock.Text = "Ready";
			_isQuerying = false;

			if (!results.HasResults)
			{
				MessageBoxEx.Show(this, "The query returned no results.", "No Results!");
				return;
			}

			var dialog = new SaveFileDialog
			{
				AddExtension = true,
				FileName = query + ".csv",
				Filter = "Comma Seperated Value Files (*.csv)|*.csv|Text Files (*.txt)|*.txt",
				DefaultExt = "csv",
				OverwritePrompt = true
			};
			var success = (bool)dialog.ShowDialog(this);
			if (!success)
				return;

			var file = dialog.FileName;
			Exporter.ExportToCSV(results, file);
		}

		public void ShowExceptionMessageOfferReport(Exception exception)
		{
			var message = "Exception type: " + exception.GetType() + "\nException details:\n" + exception.Message + "\r\n" + exception.StackTrace;
			var result = MessageBoxEx.Show(this, message + "\n\nWould you like to report this issue on GitHub?", "Unhandled Exception!", MessageBoxButton.YesNo);
			message = "[Please enter steps to reproduce this error]\n\n" + message;
			message = message.Replace("\r", "").Replace("\n", "%0D%0A"); // Percent encoded line-breaks for URL
			if (result == MessageBoxResult.Yes)
				System.Diagnostics.Process.Start(string.Format("https://github.com/CDillinger/ParcelExtractor/issues/new?body={0}", message));
		}

		public void ShowExceptionMessage(string text, string caption)
		{
			MessageBoxEx.Show(this, text, caption, MessageBoxButton.OK);
		}

		private async void GetAllButton_OnClick(object sender, RoutedEventArgs e)
		{
			var parcels = new List<Parcel>();

			if (_isQuerying)
			{
				MessageBoxEx.Show(this, "Please wait for the current query to finish...");
				return;
			}

			// Reset everything
			ParcelNumberSearchButton.IsEnabled = OwnerLastNameSearchButton.IsEnabled = AddressSearchButton.IsEnabled = GetAllButton.IsEnabled = false;
			SecondaryStatusTextBlock.Text = "0 Results";
			StatusProgressBar.Value = 0;
			MainStatusTextBlock.Text = "Querying...";
			_isQuerying = true;

			for (var i = 0; i < _chars.Length; i++)
			{
				MainStatusTextBlock.Text = string.Format("Querying Last Names: {0}...", _chars[i].ToUpper());

				await _connector.QueryAppendAsync(parcels, WebConnect.SearchType.LastName, _chars[i]);

				// Update everything
				SecondaryStatusTextBlock.Text = parcels.Count == 1 ? parcels.Count + " Result" : parcels.Count + " Results";
				StatusProgressBar.Value = (double)(i + 1) * 100 / _chars.Length;
			}

			// Complete!
			ParcelNumberSearchButton.IsEnabled = OwnerLastNameSearchButton.IsEnabled = AddressSearchButton.IsEnabled = GetAllButton.IsEnabled = true;
			SecondaryStatusTextBlock.Text = parcels.Count == 1 ? parcels.Count + " Result" : parcels.Count + " Results";
			StatusProgressBar.Value = 100;
			MainStatusTextBlock.Text = "Ready";
			_isQuerying = false;

			var dialog = new SaveFileDialog
			{
				AddExtension = true,
				FileName = "all.csv",
				Filter = "Comma Seperated Value Files (*.csv)|*.csv|Text Files (*.txt)|*.txt",
				DefaultExt = "csv",
				OverwritePrompt = true
			};
			var success = (bool)dialog.ShowDialog(this);
			if (!success)
				return;

			var file = dialog.FileName;
			Exporter.ExportToCSV(parcels, file);
		}
	}
}
