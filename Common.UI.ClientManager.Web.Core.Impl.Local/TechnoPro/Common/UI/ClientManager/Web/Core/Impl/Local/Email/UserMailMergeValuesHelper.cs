using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Email
{
	// Token: 0x0200001D RID: 29
	public static class UserMailMergeValuesHelper
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00006E4C File Offset: 0x0000504C
		public static Dictionary<string, string> InsertBaseUserMailMergeValues(this Dictionary<string, string> args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new Dictionary<string, string>();
			}
			string text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			UserMailMergeValuesHelper.SafeAddItemToDictionary(ref args, "ip", text ?? "");
			return args;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006EA0 File Offset: 0x000050A0
		public static StringDictionary InsertBaseUserMailMergeValues(this StringDictionary args)
		{
			bool flag = args == null;
			if (flag)
			{
				args = new StringDictionary();
			}
			string text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			UserMailMergeValuesHelper.SafeAddItemToDictionary(ref args, "ip", text ?? "");
			return args;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006EF4 File Offset: 0x000050F4
		private static void SafeAddItemToDictionary(ref StringDictionary args, string key, string val)
		{
			bool flag = args.ContainsKey(key);
			if (flag)
			{
				args[key] = val;
			}
			else
			{
				args.Add(key, val);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006F24 File Offset: 0x00005124
		private static void SafeAddItemToDictionary(ref Dictionary<string, string> args, string key, string val)
		{
			bool flag = args.ContainsKey(key);
			if (flag)
			{
				args[key] = val;
			}
			else
			{
				args.Add(key, val);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006F54 File Offset: 0x00005154
		public static Dictionary<string, string> GetBaseUserMailMergeValues()
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			string text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			UserMailMergeValuesHelper.SafeAddItemToDictionary(ref result, "ip", text ?? "");
			return result;
		}
	}
}
