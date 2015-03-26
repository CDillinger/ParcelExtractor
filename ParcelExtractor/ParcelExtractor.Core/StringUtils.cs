using System;
using System.Collections.Generic;

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
	}
}
