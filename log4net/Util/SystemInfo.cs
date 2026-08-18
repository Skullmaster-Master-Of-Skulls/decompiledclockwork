using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security;
using System.Threading;

namespace log4net.Util
{
	// Token: 0x02000116 RID: 278
	public sealed class SystemInfo
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x0001905B File Offset: 0x0001725B
		private SystemInfo()
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00019064 File Offset: 0x00017264
		static SystemInfo()
		{
			string text = "(null)";
			string text2 = "NOT AVAILABLE";
			string appSetting = SystemInfo.GetAppSetting("log4net.NullText");
			if (appSetting != null && appSetting.Length > 0)
			{
				LogLog.Debug(SystemInfo.declaringType, "Initializing NullText value to [" + appSetting + "].");
				text = appSetting;
			}
			string appSetting2 = SystemInfo.GetAppSetting("log4net.NotAvailableText");
			if (appSetting2 != null && appSetting2.Length > 0)
			{
				LogLog.Debug(SystemInfo.declaringType, "Initializing NotAvailableText value to [" + appSetting2 + "].");
				text2 = appSetting2;
			}
			SystemInfo.s_notAvailableText = text2;
			SystemInfo.s_nullText = text;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00019113 File Offset: 0x00017313
		public static string NewLine
		{
			get
			{
				return Environment.NewLine;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001911A File Offset: 0x0001731A
		public static string ApplicationBaseDirectory
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00019126 File Offset: 0x00017326
		public static string ConfigurationFileLocation
		{
			get
			{
				return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00019137 File Offset: 0x00017337
		public static string EntryAssemblyLocation
		{
			get
			{
				return Assembly.GetEntryAssembly().Location;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00019143 File Offset: 0x00017343
		public static int CurrentThreadId
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00019150 File Offset: 0x00017350
		public static string HostName
		{
			get
			{
				if (SystemInfo.s_hostName == null)
				{
					try
					{
						SystemInfo.s_hostName = Dns.GetHostName();
					}
					catch (SocketException)
					{
						LogLog.Debug(SystemInfo.declaringType, "Socket exception occurred while getting the dns hostname. Error Ignored.");
					}
					catch (SecurityException)
					{
						LogLog.Debug(SystemInfo.declaringType, "Security exception occurred while getting the dns hostname. Error Ignored.");
					}
					catch (Exception exception)
					{
						LogLog.Debug(SystemInfo.declaringType, "Some other exception occurred while getting the dns hostname. Error Ignored.", exception);
					}
					if (SystemInfo.s_hostName != null)
					{
						if (SystemInfo.s_hostName.Length != 0)
						{
							goto IL_72;
						}
					}
					try
					{
						SystemInfo.s_hostName = Environment.MachineName;
					}
					catch (InvalidOperationException)
					{
					}
					catch (SecurityException)
					{
					}
					IL_72:
					if (SystemInfo.s_hostName == null || SystemInfo.s_hostName.Length == 0)
					{
						SystemInfo.s_hostName = SystemInfo.s_notAvailableText;
						LogLog.Debug(SystemInfo.declaringType, "Could not determine the hostname. Error Ignored. Empty host name will be used");
					}
				}
				return SystemInfo.s_hostName;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00019240 File Offset: 0x00017440
		public static string ApplicationFriendlyName
		{
			get
			{
				if (SystemInfo.s_appFriendlyName == null)
				{
					try
					{
						SystemInfo.s_appFriendlyName = AppDomain.CurrentDomain.FriendlyName;
					}
					catch (SecurityException)
					{
						LogLog.Debug(SystemInfo.declaringType, "Security exception while trying to get current domain friendly name. Error Ignored.");
					}
					if (SystemInfo.s_appFriendlyName != null)
					{
						if (SystemInfo.s_appFriendlyName.Length != 0)
						{
							goto IL_53;
						}
					}
					try
					{
						string entryAssemblyLocation = SystemInfo.EntryAssemblyLocation;
						SystemInfo.s_appFriendlyName = Path.GetFileName(entryAssemblyLocation);
					}
					catch (SecurityException)
					{
					}
					IL_53:
					if (SystemInfo.s_appFriendlyName == null || SystemInfo.s_appFriendlyName.Length == 0)
					{
						SystemInfo.s_appFriendlyName = SystemInfo.s_notAvailableText;
					}
				}
				return SystemInfo.s_appFriendlyName;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x000192E0 File Offset: 0x000174E0
		[Obsolete("Use ProcessStartTimeUtc and convert to local time if needed.")]
		public static DateTime ProcessStartTime
		{
			get
			{
				return SystemInfo.s_processStartTimeUtc.ToLocalTime();
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000192EC File Offset: 0x000174EC
		public static DateTime ProcessStartTimeUtc
		{
			get
			{
				return SystemInfo.s_processStartTimeUtc;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x000192F3 File Offset: 0x000174F3
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x000192FA File Offset: 0x000174FA
		public static string NullText
		{
			get
			{
				return SystemInfo.s_nullText;
			}
			set
			{
				SystemInfo.s_nullText = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00019302 File Offset: 0x00017502
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00019309 File Offset: 0x00017509
		public static string NotAvailableText
		{
			get
			{
				return SystemInfo.s_notAvailableText;
			}
			set
			{
				SystemInfo.s_notAvailableText = value;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00019314 File Offset: 0x00017514
		public static string AssemblyLocationInfo(Assembly myAssembly)
		{
			if (myAssembly.GlobalAssemblyCache)
			{
				return "Global Assembly Cache";
			}
			string result;
			try
			{
				if (myAssembly.IsDynamic)
				{
					result = "Dynamic Assembly";
				}
				else
				{
					result = myAssembly.Location;
				}
			}
			catch (NotSupportedException)
			{
				result = "Dynamic Assembly";
			}
			catch (TargetInvocationException ex)
			{
				result = "Location Detect Failed (" + ex.Message + ")";
			}
			catch (ArgumentException ex2)
			{
				result = "Location Detect Failed (" + ex2.Message + ")";
			}
			catch (SecurityException)
			{
				result = "Location Permission Denied";
			}
			return result;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000193C4 File Offset: 0x000175C4
		public static string AssemblyQualifiedName(Type type)
		{
			return type.FullName + ", " + type.Assembly.FullName;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000193E4 File Offset: 0x000175E4
		public static string AssemblyShortName(Assembly myAssembly)
		{
			string text = myAssembly.FullName;
			int num = text.IndexOf(',');
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			return text.Trim();
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00019414 File Offset: 0x00017614
		public static string AssemblyFileName(Assembly myAssembly)
		{
			return Path.GetFileName(myAssembly.Location);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00019421 File Offset: 0x00017621
		public static Type GetTypeFromString(Type relativeType, string typeName, bool throwOnError, bool ignoreCase)
		{
			return SystemInfo.GetTypeFromString(relativeType.Assembly, typeName, throwOnError, ignoreCase);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00019431 File Offset: 0x00017631
		public static Type GetTypeFromString(string typeName, bool throwOnError, bool ignoreCase)
		{
			return SystemInfo.GetTypeFromString(Assembly.GetCallingAssembly(), typeName, throwOnError, ignoreCase);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00019440 File Offset: 0x00017640
		public static Type GetTypeFromString(Assembly relativeAssembly, string typeName, bool throwOnError, bool ignoreCase)
		{
			if (typeName.IndexOf(',') != -1)
			{
				return Type.GetType(typeName, throwOnError, ignoreCase);
			}
			Type type = relativeAssembly.GetType(typeName, false, ignoreCase);
			if (type != null)
			{
				return type;
			}
			Assembly[] array = null;
			try
			{
				array = AppDomain.CurrentDomain.GetAssemblies();
			}
			catch (SecurityException)
			{
			}
			if (array != null)
			{
				Type type2 = null;
				foreach (Assembly assembly in array)
				{
					Type type3 = assembly.GetType(typeName, false, ignoreCase);
					if (type3 != null)
					{
						LogLog.Debug(SystemInfo.declaringType, string.Concat(new string[]
						{
							"Loaded type [",
							typeName,
							"] from assembly [",
							assembly.FullName,
							"] by searching loaded assemblies."
						}));
						if (!assembly.GlobalAssemblyCache)
						{
							return type3;
						}
						type2 = type3;
					}
				}
				if (type2 != null)
				{
					return type2;
				}
			}
			if (throwOnError)
			{
				throw new TypeLoadException(string.Concat(new string[]
				{
					"Could not load type [",
					typeName,
					"]. Tried assembly [",
					relativeAssembly.FullName,
					"] and all loaded assemblies"
				}));
			}
			return null;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001957C File Offset: 0x0001777C
		public static Guid NewGuid()
		{
			return Guid.NewGuid();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00019583 File Offset: 0x00017783
		public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string parameterName, object actualValue, string message)
		{
			return new ArgumentOutOfRangeException(parameterName, actualValue, message);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00019590 File Offset: 0x00017790
		public static bool TryParse(string s, out int val)
		{
			val = 0;
			try
			{
				double value;
				if (double.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
				{
					val = Convert.ToInt32(value);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000195D4 File Offset: 0x000177D4
		public static bool TryParse(string s, out long val)
		{
			val = 0L;
			try
			{
				double value;
				if (double.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
				{
					val = Convert.ToInt64(value);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001961C File Offset: 0x0001781C
		public static bool TryParse(string s, out short val)
		{
			val = 0;
			try
			{
				double value;
				if (double.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
				{
					val = Convert.ToInt16(value);
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00019660 File Offset: 0x00017860
		public static string GetAppSetting(string key)
		{
			try
			{
				return ConfigurationManager.AppSettings[key];
			}
			catch (Exception exception)
			{
				LogLog.Error(SystemInfo.declaringType, "Exception while reading ConfigurationSettings. Check your .config file is well formed XML.", exception);
			}
			return null;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000196A4 File Offset: 0x000178A4
		public static string ConvertToFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			string text = "";
			try
			{
				string applicationBaseDirectory = SystemInfo.ApplicationBaseDirectory;
				if (applicationBaseDirectory != null)
				{
					Uri uri = new Uri(applicationBaseDirectory);
					if (uri.IsFile)
					{
						text = uri.LocalPath;
					}
				}
			}
			catch
			{
			}
			if (text != null && text.Length > 0)
			{
				return Path.GetFullPath(Path.Combine(text, path));
			}
			return Path.GetFullPath(path);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00019718 File Offset: 0x00017918
		public static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00019724 File Offset: 0x00017924
		public static bool EqualsIgnoringCase(string a, string b)
		{
			return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040002F1 RID: 753
		private const string DEFAULT_NULL_TEXT = "(null)";

		// Token: 0x040002F2 RID: 754
		private const string DEFAULT_NOT_AVAILABLE_TEXT = "NOT AVAILABLE";

		// Token: 0x040002F3 RID: 755
		public static readonly Type[] EmptyTypes = new Type[0];

		// Token: 0x040002F4 RID: 756
		private static readonly Type declaringType = typeof(SystemInfo);

		// Token: 0x040002F5 RID: 757
		private static string s_hostName;

		// Token: 0x040002F6 RID: 758
		private static string s_appFriendlyName;

		// Token: 0x040002F7 RID: 759
		private static string s_nullText;

		// Token: 0x040002F8 RID: 760
		private static string s_notAvailableText;

		// Token: 0x040002F9 RID: 761
		private static DateTime s_processStartTimeUtc = DateTime.UtcNow;
	}
}
