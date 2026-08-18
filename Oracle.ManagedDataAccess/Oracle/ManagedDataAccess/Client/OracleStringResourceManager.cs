using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Permissions;
using System.Threading;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200007D RID: 125
	internal static class OracleStringResourceManager
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x000391C0 File Offset: 0x000373C0
		static OracleStringResourceManager()
		{
			OracleStringResourceManager.m_CultureToResourceStringMap = (from resourceName in Assembly.GetExecutingAssembly().GetManifestResourceNames()
			where resourceName.StartsWith("Oracle.ManagedDataAccess.src.Client.Resources.Exception.") && resourceName.EndsWith(".resources")
			select resourceName).ToDictionary((string resourceName) => resourceName.Substring("Oracle.ManagedDataAccess.src.Client.Resources.Exception".Length, resourceName.Length - ("Oracle.ManagedDataAccess.src.Client.Resources.Exception".Length + "resources".Length)).Trim(new char[]
			{
				'.'
			}), (string resourceName) => null);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003925C File Offset: 0x0003745C
		internal static string GetString(string key, CultureInfo culture)
		{
			string result;
			try
			{
				foreach (string cultureName2 in from cultureName in culture.NextMatchingNonInvariantCulture()
				where OracleStringResourceManager.m_CultureToResourceStringMap.ContainsKey(cultureName)
				select cultureName)
				{
					string resourceStringForCultureName = OracleStringResourceManager.GetResourceStringForCultureName(key, cultureName2);
					if (resourceStringForCultureName != null)
					{
						return resourceStringForCultureName;
					}
				}
				result = OracleStringResourceManager.GetResourceStringForCultureName(key, OracleStringResourceManager.DFEAULT_RESOURCE_NAME);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000392F4 File Offset: 0x000374F4
		private static string GetResourceStringForCultureName(string key, string cultureName)
		{
			if (!OracleStringResourceManager.m_CultureToResourceStringMap.ContainsKey(cultureName))
			{
				return null;
			}
			if (OracleStringResourceManager.m_CultureToResourceStringMap[cultureName] == null)
			{
				OracleStringResourceManager.ExtractEmbeddedResourceStringsForCultureName(cultureName);
			}
			ResourceSet resourceSet = OracleStringResourceManager.m_CultureToResourceStringMap[cultureName];
			if (resourceSet != null)
			{
				return resourceSet.GetString(key);
			}
			return null;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0003933C File Offset: 0x0003753C
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static void ExtractEmbeddedResourceStringsForCultureName(string name)
		{
			if (!OracleStringResourceManager.m_CultureToResourceStringMap.ContainsKey(name) || OracleStringResourceManager.m_CultureToResourceStringMap[name] != null)
			{
				return;
			}
			string name2 = "Oracle.ManagedDataAccess.src.Client.Resources.Exception." + ((string.IsNullOrEmpty(name) || name.Trim().Length == 0) ? "resources" : (name.Trim() + ".resources"));
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name2);
			if (manifestResourceStream == null)
			{
				return;
			}
			OracleStringResourceManager.m_CultureToResourceStringMap[name] = new ResourceSet(manifestResourceStream);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000393BC File Offset: 0x000375BC
		private static IEnumerable<string> NextMatchingNonInvariantCulture(this CultureInfo culture)
		{
			CultureInfo ci = culture;
			while (ci != null && ci != CultureInfo.InvariantCulture)
			{
				yield return ci.Name;
				ci = ci.Parent;
			}
			yield break;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000393DC File Offset: 0x000375DC
		internal static string GetErrorMesg(int errorcode, params string[] args)
		{
			string text = string.Empty;
			string @string = OracleStringResourceManager.GetString(Convert.ToString(errorcode), Thread.CurrentThread.CurrentCulture);
			if (@string != null)
			{
				text = string.Format(@string, args);
				if (errorcode > 0)
				{
					bool flag = text.StartsWith("Ora-12541", StringComparison.InvariantCulture);
					if (flag)
					{
						text = text.Replace("Ora-12541", "ORA-12541");
					}
					if (!text.StartsWith("ORA-", StringComparison.InvariantCultureIgnoreCase))
					{
						text = string.Format("ORA-{0}: {1}", errorcode.ToString("D5"), text);
					}
				}
			}
			else if ((@string = OracleStringResourceManager.GetString(OracleStringResourceManager.DEFAULT_MESSAGE_NUMBER, Thread.CurrentThread.CurrentCulture)) != null)
			{
				text = string.Format(@string, errorcode);
			}
			return text;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00039488 File Offset: 0x00037688
		internal static string GetErrorMesgWithErrCode(int errorcode, params string[] args)
		{
			string errorMesg = OracleStringResourceManager.GetErrorMesg(errorcode, args);
			if (errorcode > 0)
			{
				return errorMesg;
			}
			return string.Format("ORA-{0}: {1}", errorcode.ToString("D5"), errorMesg);
		}

		// Token: 0x040006CE RID: 1742
		private const string RESOURCE_NAME_PREFIX = "Oracle.ManagedDataAccess.src.Client.Resources.Exception";

		// Token: 0x040006CF RID: 1743
		private const string RESOURCE_NAME_PREFIX_WITH_DOT = "Oracle.ManagedDataAccess.src.Client.Resources.Exception.";

		// Token: 0x040006D0 RID: 1744
		private const string RESOURCE_NAME_SUFFIX = "resources";

		// Token: 0x040006D1 RID: 1745
		private const string RESOURCE_NAME_SUFFIX_WITH_DOT = ".resources";

		// Token: 0x040006D2 RID: 1746
		private const char DOT = '.';

		// Token: 0x040006D3 RID: 1747
		private static readonly string DFEAULT_RESOURCE_NAME = string.Empty;

		// Token: 0x040006D4 RID: 1748
		private static string DEFAULT_MESSAGE_NUMBER = Convert.ToString(-12);

		// Token: 0x040006D5 RID: 1749
		private static Dictionary<string, ResourceSet> m_CultureToResourceStringMap = null;
	}
}
