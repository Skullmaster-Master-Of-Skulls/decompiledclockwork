using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Authentication;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D9 RID: 1497
	public static class LdapConnectionInfoAdapter
	{
		// Token: 0x0600303A RID: 12346 RVA: 0x0003DB94 File Offset: 0x0003BD94
		public static LdapConnectionInfo GetConnectionInfoFromArgs(this IDictionary<string, string> ldapInfo)
		{
			bool flag = ldapInfo == null;
			if (flag)
			{
				ldapInfo = new Dictionary<string, string>();
			}
			string argSafe = ldapInfo.GetArgSafe("ldapreturnattribute");
			return new LdapConnectionInfo
			{
				AuthType = ldapInfo.GetArgSafe("ldapauthtype"),
				Domain = ldapInfo.GetArgSafe("ldapdomain"),
				IsDoubleBinding = (ldapInfo.GetArgIntSafe("Isdoublebinding", 0) != 0),
				IsActiveDirectory = (ldapInfo.GetArgIntSafe("activedirectory", 0) != 0),
				UseLookupAttributeForActiveDirectory = (ldapInfo.GetArgIntSafe("activedirectoryuselookupattribute", 0) == 1),
				LookupAttribute = ldapInfo.GetArgSafe("ldaplookupattribute"),
				Port = ldapInfo.GetArgIntSafe("ldapport", 0),
				PreDomain = ldapInfo.GetArgSafe("Ldappredomain"),
				PreLookupAttribute = ldapInfo.GetArgSafe("Ldapprelookupattribute"),
				PrePassword = ldapInfo.GetArgSafe("Ldapprepassword"),
				PreUsername = ldapInfo.GetArgSafe("Ldappreusername"),
				ProtocolVersion = ldapInfo.GetArgIntSafe("Ldapprotocolversion", 0),
				ReturnAttributes = (string.IsNullOrEmpty(argSafe) ? new string[0] : argSafe.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)),
				ServerName = ldapInfo.GetArgSafe("Ldapserver"),
				SSL = (ldapInfo.GetArgIntSafe("Ldapusessl", 0) != 0),
				TLS = (ldapInfo.GetArgIntSafe("Ldapusetls", 0) != 0),
				DontVerifyServerCertificate = (ldapInfo.GetArgIntSafe("Ldapdontverifyservercertificate", 0) != 0)
			};
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0003DD2C File Offset: 0x0003BF2C
		public static string GetArgSafe(this IDictionary<string, string> args, string argKey)
		{
			string keyCaseInsensitive = LdapConnectionInfoAdapter.GetKeyCaseInsensitive(args, argKey);
			bool flag = !args.ContainsKey(keyCaseInsensitive);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = (args[keyCaseInsensitive] ?? "");
			}
			return result;
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0003DD6C File Offset: 0x0003BF6C
		private static string GetKeyCaseInsensitive(IDictionary<string, string> args, string argKey)
		{
			bool flag = args.ContainsKey(argKey);
			string result;
			if (flag)
			{
				result = argKey;
			}
			else
			{
				foreach (KeyValuePair<string, string> keyValuePair in args)
				{
					bool flag2 = keyValuePair.Key.Equals(argKey, StringComparison.OrdinalIgnoreCase);
					if (flag2)
					{
						return keyValuePair.Key;
					}
				}
				result = argKey;
			}
			return result;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
		private static int GetArgIntSafe(this IDictionary<string, string> args, string argKey, int defaultValue = 0)
		{
			string keyCaseInsensitive = LdapConnectionInfoAdapter.GetKeyCaseInsensitive(args, argKey);
			int num;
			bool flag = !args.ContainsKey(keyCaseInsensitive) || !int.TryParse(args[keyCaseInsensitive] ?? "", out num);
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

		// Token: 0x0600303E RID: 12350 RVA: 0x0003DE30 File Offset: 0x0003C030
		public static string LdapConnectionInfoToString(this LdapConnectionInfo info)
		{
			return string.Format("{0}`{1}`{2}`{3}`{4}`{5}`{6}`{7}`{8}`{9}`{10}`{11}`{12}`{13}", new object[]
			{
				(info.ServerName == null) ? "" : info.ServerName,
				info.Port.ToString(),
				info.Domain ?? "",
				info.AuthType ?? "",
				info.LookupAttribute ?? "",
				"",
				info.PreDomain ?? "",
				info.PreLookupAttribute ?? "",
				info.PreUsername ?? "",
				info.PrePassword ?? "",
				info.ProtocolVersion.ToString(),
				info.SSL ? "1" : "",
				info.DontVerifyServerCertificate.ToString(),
				info.TLS ? "1" : ""
			});
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0003DF58 File Offset: 0x0003C158
		public static LdapConnectionInfo LdapConnectionInfoFromString(this string infoStr)
		{
			bool flag = infoStr == null;
			LdapConnectionInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string[] array = infoStr.Split(new char[]
				{
					'`'
				});
				int num = array.Length;
				string serverName = (num > 0) ? array[0] : "";
				bool flag2 = num > 1;
				int port;
				if (flag2)
				{
					bool flag3 = !int.TryParse(array[1], out port);
					if (flag3)
					{
						port = 389;
					}
				}
				else
				{
					port = 389;
				}
				string domain = (num > 2) ? array[2] : "";
				string authType = (num > 3) ? array[3] : "";
				string lookupAttribute = (num > 4) ? array[4] : "";
				string text = (num > 5) ? array[5] : "";
				string preDomain = (num > 6) ? array[6] : "";
				string preLookupAttribute = (num > 7) ? array[7] : "";
				string text2 = (num > 8) ? array[8] : "";
				string prePassword = (num > 9) ? array[9] : "";
				string s = (num > 10) ? array[10] : "";
				string text3 = (num > 11) ? array[11] : "";
				string text4 = (num > 12) ? array[12] : "";
				string text5 = (num > 13) ? array[13] : "";
				int protocolVersion;
				bool flag4 = !int.TryParse(s, out protocolVersion);
				if (flag4)
				{
					protocolVersion = 0;
				}
				bool ssl = text3.Length > 0;
				bool dontVerifyServerCertificate = text4.Length > 0;
				bool tls = text5.Length > 0;
				result = new LdapConnectionInfo
				{
					ServerName = serverName,
					Port = port,
					AuthType = authType,
					Domain = domain,
					IsDoubleBinding = !string.IsNullOrEmpty(text2),
					LookupAttribute = lookupAttribute,
					PreDomain = preDomain,
					PreUsername = text2,
					PrePassword = prePassword,
					PreLookupAttribute = preLookupAttribute,
					ProtocolVersion = protocolVersion,
					SSL = ssl,
					DontVerifyServerCertificate = dontVerifyServerCertificate,
					TLS = tls
				};
			}
			return result;
		}
	}
}
