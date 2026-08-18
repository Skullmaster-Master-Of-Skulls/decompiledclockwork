using System;
using System.Collections.Specialized;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x02000056 RID: 86
	public static class LdapAdapters
	{
		// Token: 0x06000279 RID: 633 RVA: 0x00005708 File Offset: 0x00003908
		public static LdapConnectionInfoDTO ParseConnectionInfo(this StringDictionary Args)
		{
			string stringTypeStringDictionaryValue = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldapreturnattribute");
			LdapConnectionInfoDTO ldapConnectionInfoDTO = new LdapConnectionInfoDTO
			{
				AuthType = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldapauthtype"),
				Domain = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldapdomain"),
				IsDoubleBinding = LdapAdapters.GetBooleanTypeStringDictionaryValue(Args, "isdoublebinding"),
				IsActiveDirectory = LdapAdapters.GetBooleanTypeStringDictionaryValue(Args, "activedirectory"),
				LookupAttribute = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldaplookupattribute"),
				Port = LdapAdapters.GetIntTypeStringDictionaryValue(Args, "ldapport", 0),
				PreDomain = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldappredomain"),
				PreLookupAttribute = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldapprelookupattribute"),
				PrePassword = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldapprepassword"),
				PreUsername = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldappreusername"),
				ProtocolVersion = LdapAdapters.GetIntTypeStringDictionaryValue(Args, "ldapprotocolversion", 0),
				ReturnAttributes = (string.IsNullOrEmpty(stringTypeStringDictionaryValue) ? new string[0] : stringTypeStringDictionaryValue.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)),
				ServerName = LdapAdapters.GetStringTypeStringDictionaryValue(Args, "ldapserver"),
				SSL = LdapAdapters.GetBooleanTypeStringDictionaryValue(Args, "ldapusessl"),
				TLS = LdapAdapters.GetBooleanTypeStringDictionaryValue(Args, "ldapusetls"),
				DontVerifyServerCertificate = LdapAdapters.GetBooleanTypeStringDictionaryValue(Args, "ldapdontverifyservercertificate")
			};
			bool flag = ldapConnectionInfoDTO.ReturnAttributes == null || ldapConnectionInfoDTO.ReturnAttributes.Count<string>() < 1;
			if (flag)
			{
				ldapConnectionInfoDTO.ReturnAttributes = new string[]
				{
					ldapConnectionInfoDTO.LookupAttribute ?? ""
				};
			}
			return ldapConnectionInfoDTO;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000058A8 File Offset: 0x00003AA8
		public static StringDictionary CreateConnectionInfoStringDictionary(this LdapConnectionInfoDTO connInfo)
		{
			StringDictionary stringDictionary = new StringDictionary();
			bool flag = !string.IsNullOrEmpty(connInfo.AuthType);
			if (flag)
			{
				stringDictionary.Add("ldapauthtype", connInfo.AuthType);
			}
			bool flag2 = !string.IsNullOrEmpty(connInfo.Domain);
			if (flag2)
			{
				stringDictionary.Add("ldapdomain", connInfo.Domain);
			}
			bool isDoubleBinding = connInfo.IsDoubleBinding;
			if (isDoubleBinding)
			{
				stringDictionary.Add("isdoublebinding", connInfo.IsDoubleBinding ? "1" : "0");
			}
			bool isActiveDirectory = connInfo.IsActiveDirectory;
			if (isActiveDirectory)
			{
				stringDictionary.Add("activedirectory", connInfo.IsActiveDirectory ? "1" : "0");
			}
			bool flag3 = !string.IsNullOrEmpty(connInfo.LookupAttribute);
			if (flag3)
			{
				stringDictionary.Add("ldaplookupattribute", connInfo.LookupAttribute);
			}
			bool flag4 = connInfo.Port > 0;
			if (flag4)
			{
				stringDictionary.Add("ldapport", connInfo.Port.ToString());
			}
			bool flag5 = !string.IsNullOrEmpty(connInfo.PreDomain);
			if (flag5)
			{
				stringDictionary.Add("ldappredomain", connInfo.PreDomain);
			}
			bool flag6 = !string.IsNullOrEmpty(connInfo.PreLookupAttribute);
			if (flag6)
			{
				stringDictionary.Add("ldapprelookupattribute", connInfo.PreLookupAttribute);
			}
			bool flag7 = !string.IsNullOrEmpty(connInfo.PrePassword);
			if (flag7)
			{
				stringDictionary.Add("ldapprepassword", connInfo.PrePassword);
			}
			bool flag8 = !string.IsNullOrEmpty(connInfo.PreUsername);
			if (flag8)
			{
				stringDictionary.Add("ldappreusername", connInfo.PreUsername);
			}
			bool flag9 = connInfo.ProtocolVersion > 0;
			if (flag9)
			{
				stringDictionary.Add("ldapprotocolversion", connInfo.ProtocolVersion.ToString());
			}
			bool flag10 = connInfo.ReturnAttributes != null && connInfo.ReturnAttributes.Length != 0;
			if (flag10)
			{
				stringDictionary.Add("ldapreturnattribute", (connInfo.ReturnAttributes == null) ? "" : string.Join(",", connInfo.ReturnAttributes));
			}
			bool flag11 = !string.IsNullOrEmpty(connInfo.ServerName);
			if (flag11)
			{
				stringDictionary.Add("ldapserver", connInfo.ServerName);
			}
			bool ssl = connInfo.SSL;
			if (ssl)
			{
				stringDictionary.Add("ldapusessl", connInfo.SSL ? "1" : "0");
			}
			bool tls = connInfo.TLS;
			if (tls)
			{
				stringDictionary.Add("ldapusetls", connInfo.TLS ? "1" : "0");
			}
			bool dontVerifyServerCertificate = connInfo.DontVerifyServerCertificate;
			if (dontVerifyServerCertificate)
			{
				stringDictionary.Add("dontverifyservercertificate", connInfo.DontVerifyServerCertificate ? "1" : "0");
			}
			return stringDictionary;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00005B64 File Offset: 0x00003D64
		private static bool GetBooleanTypeStringDictionaryValue(StringDictionary args, string key)
		{
			string stringTypeStringDictionaryValue = LdapAdapters.GetStringTypeStringDictionaryValue(args, key);
			return !string.IsNullOrEmpty(stringTypeStringDictionaryValue) && "yestrue1".IndexOf(stringTypeStringDictionaryValue.ToLower()) >= 0;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00005BA0 File Offset: 0x00003DA0
		private static int GetIntTypeStringDictionaryValue(StringDictionary args, string key, int defaultValue = 0)
		{
			string stringTypeStringDictionaryValue = LdapAdapters.GetStringTypeStringDictionaryValue(args, key);
			int num;
			bool flag = !int.TryParse(stringTypeStringDictionaryValue, out num);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00005BD0 File Offset: 0x00003DD0
		private static string GetStringTypeStringDictionaryValue(StringDictionary args, string key)
		{
			bool flag = !args.ContainsKey(key);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (args[key] ?? "");
			}
			return result;
		}

		// Token: 0x0400019A RID: 410
		private const string trueString = "yestrue1";
	}
}
