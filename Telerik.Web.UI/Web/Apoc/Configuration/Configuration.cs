using System;
using System.Collections;

namespace Telerik.Web.Apoc.Configuration
{
	// Token: 0x02001377 RID: 4983
	internal class Configuration
	{
		// Token: 0x0600CFF4 RID: 53236 RVA: 0x002E1394 File Offset: 0x002DF594
		public static object GetValue(string key)
		{
			return Configuration.config[key];
		}

		// Token: 0x0600CFF5 RID: 53237 RVA: 0x002E13A4 File Offset: 0x002DF5A4
		public static string GetStringValue(string key)
		{
			return Configuration.GetValue(key) as string;
		}

		// Token: 0x0600CFF6 RID: 53238 RVA: 0x002E13C0 File Offset: 0x002DF5C0
		public static int GetIntValue(string key)
		{
			object value = Configuration.GetValue(key);
			if (value is int)
			{
				return (int)value;
			}
			return -1;
		}

		// Token: 0x0600CFF7 RID: 53239 RVA: 0x002E13E4 File Offset: 0x002DF5E4
		public static bool GetBooleanValue(string key)
		{
			object value = Configuration.GetValue(key);
			return value is bool && (bool)value;
		}

		// Token: 0x0600CFF8 RID: 53240 RVA: 0x002E1408 File Offset: 0x002DF608
		public static ArrayList GetListValue(string key)
		{
			return Configuration.GetValue(key) as ArrayList;
		}

		// Token: 0x0600CFF9 RID: 53241 RVA: 0x002E1422 File Offset: 0x002DF622
		public static ArrayList GetFonts()
		{
			return new ArrayList();
		}

		// Token: 0x0600CFFA RID: 53242 RVA: 0x002E1429 File Offset: 0x002DF629
		public static void PutValue(string key, object value)
		{
			Configuration.config[key] = value;
		}

		// Token: 0x0600CFFB RID: 53243 RVA: 0x002E1438 File Offset: 0x002DF638
		public static void PutListValue(string key, object[] values)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(values);
			Configuration.config[key] = arrayList;
		}

		// Token: 0x040037BE RID: 14270
		private static Hashtable config = new Hashtable();
	}
}
