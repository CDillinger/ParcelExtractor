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
using System.Windows.Threading;
using ParcelExtractor.Core;

namespace ParcelExtractor
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static MainWindow _mainWindow;

		App()
		{
			DispatcherUnhandledException += OnDispatcherUnhandledException;
		}

		private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = true;
			if (e.Exception.GetType() == typeof (ConnectionFailedException))
				_mainWindow.ShowExceptionMessage(e.Exception.Message, "Connection Failed!");
			else
				_mainWindow.ShowExceptionMessageOfferReport(e.Exception);
		}

		public static void SetMainWindow(MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
		}
	}
}
