using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Web.Helpers.Resources;
using System.Web.WebPages;

namespace System.Web.Helpers
{
	// Token: 0x02000017 RID: 23
	public static class ServerInfo
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00005530 File Offset: 0x00003730
		internal static IDictionary<string, string> EnvironmentVariables()
		{
			IDictionary<string, string> dictionary = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			IDictionary environmentVariables;
			try
			{
				environmentVariables = Environment.GetEnvironmentVariables();
			}
			catch (SecurityException)
			{
				return dictionary;
			}
			foreach (object obj in environmentVariables)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				dictionary.Add(dictionaryEntry.Key.ToString(), ServerInfo.InsertWhiteSpace(dictionaryEntry.Value.ToString()));
			}
			return dictionary;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000055D0 File Offset: 0x000037D0
		internal static IDictionary<string, string> ServerVariables()
		{
			HttpContext httpContext = HttpContext.Current;
			return ServerInfo.ServerVariables((httpContext != null) ? new HttpContextWrapper(httpContext) : null);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000055F4 File Offset: 0x000037F4
		internal static IDictionary<string, string> ServerVariables(HttpContextBase context)
		{
			IDictionary<string, string> dictionary = new SortedDictionary<string, string>();
			NameValueCollection serverVariables;
			try
			{
				if (context == null || context.Request == null)
				{
					return dictionary;
				}
				serverVariables = context.Request.ServerVariables;
			}
			catch (SecurityException)
			{
				return dictionary;
			}
			foreach (string text in serverVariables.AllKeys)
			{
				if (!text.Equals("ALL_HTTP", StringComparison.OrdinalIgnoreCase) && !text.Equals("ALL_RAW", StringComparison.OrdinalIgnoreCase) && !text.Equals("HTTP_AUTHORIZATION", StringComparison.OrdinalIgnoreCase) && !text.Equals("HTTP_COOKIE", StringComparison.OrdinalIgnoreCase))
				{
					dictionary.Add(text, ServerInfo.InsertWhiteSpace(serverVariables[text]));
				}
			}
			return dictionary;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000056AC File Offset: 0x000038AC
		internal static IDictionary<string, string> Configuration()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("Current Local Time", DateTime.Now.ToString(CultureInfo.CurrentCulture));
			dictionary.Add("Current UTC Time", DateTime.UtcNow.ToString(CultureInfo.CurrentCulture));
			dictionary.Add("Current Culture", CultureInfo.CurrentCulture.DisplayName);
			dictionary.Add("Machine Name", Environment.MachineName);
			dictionary.Add("OS Version", Environment.OSVersion.ToString());
			dictionary.Add("ASP.NET Version", Environment.Version.ToString());
			dictionary.Add("ASP.NET Web Pages Version", new AssemblyName(typeof(WebPage).Assembly.FullName).Version.ToString());
			dictionary.Add("User Name", Environment.UserName);
			dictionary.Add("User Interactive", Environment.UserInteractive.ToString());
			dictionary.Add("Processor Count", Environment.ProcessorCount.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("Tick Count", Environment.TickCount.ToString(CultureInfo.InvariantCulture));
			try
			{
				dictionary.Add("Current Directory", Environment.CurrentDirectory);
			}
			catch (SecurityException)
			{
				return dictionary;
			}
			dictionary.Add("System Directory", Environment.SystemDirectory);
			dictionary.Add("User Domain Name", Environment.UserDomainName);
			dictionary.Add("Working Set", Environment.WorkingSet.ToString(CultureInfo.InvariantCulture) + " bytes");
			return dictionary;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005850 File Offset: 0x00003A50
		internal static IDictionary<string, string> HttpRuntimeInfo()
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				dictionary.Add("CLR Install Directory", HttpRuntime.ClrInstallDirectory);
			}
			catch (SecurityException)
			{
				return dictionary;
			}
			try
			{
				dictionary.Add("Codegen Directory", HttpRuntime.CodegenDir);
				dictionary.Add("Bin Directory", HttpRuntime.BinDirectory);
				dictionary.Add("AppDomain Application Path", HttpRuntime.AppDomainAppPath);
			}
			catch (ArgumentException)
			{
			}
			dictionary.Add("Asp Install Directory", HttpRuntime.AspInstallDirectory);
			dictionary.Add("Machine Configuration Directory", HttpRuntime.MachineConfigurationDirectory);
			dictionary.Add("AppDomain Id", HttpRuntime.AppDomainId);
			dictionary.Add("AppDomain Application Id", HttpRuntime.AppDomainAppId);
			dictionary.Add("AppDomain Application Virtual Path", HttpRuntime.AppDomainAppVirtualPath);
			dictionary.Add("Asp Client Script Physical Path", HttpRuntime.AspClientScriptPhysicalPath);
			dictionary.Add("Asp Client Script Virtual Path", HttpRuntime.AspClientScriptVirtualPath);
			dictionary.Add("Cache Size", HttpRuntime.Cache.Count.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("Cache Effective Percentage Physical Memory Limit", HttpRuntime.Cache.EffectivePercentagePhysicalMemoryLimit.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("Cache Effective Private Bytes Limit", HttpRuntime.Cache.EffectivePrivateBytesLimit.ToString(CultureInfo.InvariantCulture));
			dictionary.Add("On UNC Share", HttpRuntime.IsOnUNCShare.ToString());
			return dictionary;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000059C4 File Offset: 0x00003BC4
		internal static IDictionary<string, string> LegacyCAS()
		{
			return ServerInfo.LegacyCAS(AppDomain.CurrentDomain);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000059D0 File Offset: 0x00003BD0
		internal static IDictionary<string, string> LegacyCAS(AppDomain appDomain)
		{
			IDictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				bool flag = !appDomain.IsHomogenous;
				if (flag)
				{
					dictionary[HelpersResources.ServerInfo_LegacyCAS] = HelpersResources.ServerInfo_LegacyCasHelpInfo;
				}
			}
			catch (SecurityException)
			{
				return dictionary;
			}
			return dictionary;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A1C File Offset: 0x00003C1C
		public static HtmlString GetHtml()
		{
			StringBuilder stringBuilder = new StringBuilder("<style type=\"text/css\">  div.server-info { text-align: center; }  table.server-info { border-collapse:collapse; text-align:center; margin: auto; width:600px; direction: ltr; }  table.server-info tbody tr:nth-child(even){ background-color: #EEE; }  table.server-info, table.server-info th, table.server-info td { border:1px solid black; }  table.server-info th, table.server-info td  { text-align:left; padding:2px; font-family:Tahoma, Arial, sans-serif; font-size:0.75em; }  h1.server-info { font-family:Tahoma, Arial, sans-serif; font-size:150%; text-align:center; }  table.server-info h2 { font-family:Tahoma, Arial, sans-serif; font-size:125%; text-align:center; }  p.server-info { text-align:center; font-family:Tahoma, Arial, sans-serif; font-size:0.75em; }  .ital { font-style: italic; }   .warn { color: #F00; } </style>");
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "<h1 class=\"server-info\">{0}</h1>", new object[]
			{
				HttpUtility.HtmlEncode(HelpersResources.ServerInfo_Header)
			}));
			IDictionary<string, string> entries = ServerInfo.Configuration();
			ServerInfo.PrintInfoSection(stringBuilder, HelpersResources.ServerInfo_ServerConfigTable, entries);
			IDictionary<string, string> entries2 = ServerInfo.ServerVariables();
			ServerInfo.PrintInfoSection(stringBuilder, HelpersResources.ServerInfo_ServerVars, entries2);
			IDictionary<string, string> dictionary = ServerInfo.LegacyCAS();
			if (dictionary.Any<KeyValuePair<string, string>>())
			{
				ServerInfo.PrintInfoSection(stringBuilder, HelpersResources.ServerInfo_LegacyCAS, dictionary);
			}
			IDictionary<string, string> dictionary2 = ServerInfo.HttpRuntimeInfo();
			if (!dictionary2.Any<KeyValuePair<string, string>>())
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "<p class=\"server-info\">{0}</p>", new object[]
				{
					HttpUtility.HtmlEncode(HelpersResources.ServerInfo_AdditionalInfo)
				}));
				return new HtmlString(stringBuilder.ToString());
			}
			ServerInfo.PrintInfoSection(stringBuilder, HelpersResources.ServerInfo_HttpRuntime, dictionary2);
			IDictionary<string, string> entries3 = ServerInfo.EnvironmentVariables();
			ServerInfo.PrintInfoSection(stringBuilder, HelpersResources.ServerInfo_EnvVars, entries3);
			return new HtmlString(stringBuilder.ToString());
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005B18 File Offset: 0x00003D18
		private static void PrintInfoSection(StringBuilder builder, string sectionTitle, IDictionary<string, string> entries)
		{
			builder.AppendLine("<div class=\"server-info\">");
			builder.AppendLine("<table class=\"server-info\" dir=\"ltr\">");
			if (!string.IsNullOrEmpty(sectionTitle))
			{
				builder.AppendLine("<caption>");
				builder.AppendFormat(CultureInfo.InvariantCulture, "<h2>{0}</h2>", new object[]
				{
					HttpUtility.HtmlEncode(sectionTitle)
				}).AppendLine();
				builder.AppendLine("</caption>");
			}
			builder.AppendLine("<colgroup><col style=\"width:30%;\" /> <col style=\"width:70%;\"  /></colgroup>");
			builder.AppendLine("<tbody>");
			foreach (KeyValuePair<string, string> keyValuePair in entries)
			{
				string text = string.Empty;
				string s = keyValuePair.Value;
				if (keyValuePair.Key == HelpersResources.ServerInfo_LegacyCAS)
				{
					text = "warn";
				}
				else if (string.IsNullOrEmpty(keyValuePair.Value))
				{
					text = "ital";
					s = HelpersResources.ServerInfo_NoValue;
				}
				if (text.Any<char>())
				{
					text = " class=\"" + text + "\"";
				}
				builder.Append("<tr>");
				builder.AppendFormat(CultureInfo.InvariantCulture, "<th scope=\"row\">{0}</th>", new object[]
				{
					HttpUtility.HtmlEncode(keyValuePair.Key)
				});
				builder.AppendFormat(CultureInfo.InvariantCulture, "<td{0}>{1}</td>", new object[]
				{
					text,
					HttpUtility.HtmlEncode(s)
				});
				builder.AppendLine("</tr>");
			}
			builder.AppendLine("</tbody>");
			builder.AppendLine("</table>");
			builder.AppendLine("</div>");
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005CC8 File Offset: 0x00003EC8
		private static string InsertWhiteSpace(string s)
		{
			return s.Replace(",", ", ").Replace(";", "; ");
		}

		// Token: 0x04000047 RID: 71
		private const string Style = "<style type=\"text/css\">  div.server-info { text-align: center; }  table.server-info { border-collapse:collapse; text-align:center; margin: auto; width:600px; direction: ltr; }  table.server-info tbody tr:nth-child(even){ background-color: #EEE; }  table.server-info, table.server-info th, table.server-info td { border:1px solid black; }  table.server-info th, table.server-info td  { text-align:left; padding:2px; font-family:Tahoma, Arial, sans-serif; font-size:0.75em; }  h1.server-info { font-family:Tahoma, Arial, sans-serif; font-size:150%; text-align:center; }  table.server-info h2 { font-family:Tahoma, Arial, sans-serif; font-size:125%; text-align:center; }  p.server-info { text-align:center; font-family:Tahoma, Arial, sans-serif; font-size:0.75em; }  .ital { font-style: italic; }   .warn { color: #F00; } </style>";
	}
}
