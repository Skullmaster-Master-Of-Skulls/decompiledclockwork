using System;
using System.Xml.Linq;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005D4 RID: 1492
	public static class XmlAdapter
	{
		// Token: 0x06002FF6 RID: 12278 RVA: 0x0003B5A4 File Offset: 0x000397A4
		public static int? GetIntFromElement(this XElement element)
		{
			return XmlAdapter.GetInt(((element != null) ? element.Value : null) ?? string.Empty);
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x0003B5D0 File Offset: 0x000397D0
		public static bool GetBoolFromAttribute(this XAttribute attribute, bool defaultValue)
		{
			return ((attribute != null) ? attribute.GetBoolFromAttribute() : null) ?? defaultValue;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x0003B60C File Offset: 0x0003980C
		public static bool? GetBoolFromAttribute(this XAttribute attribute)
		{
			return XmlAdapter.GetBool((attribute != null) ? attribute.Value : null);
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x0003B630 File Offset: 0x00039830
		private static bool? GetBool(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool value;
				bool flag2 = bool.TryParse(s, out value);
				if (flag2)
				{
					result = new bool?(value);
				}
				else
				{
					result = new bool?("1yestrue".IndexOf(s) >= 0);
				}
			}
			return result;
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x0003B688 File Offset: 0x00039888
		public static string GetStringFromAttribute(this XAttribute attribute)
		{
			return (attribute != null) ? attribute.Value : null;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x0003B6A8 File Offset: 0x000398A8
		public static int GetIntFromAttribute(this XAttribute attribute, int defaultValue)
		{
			return ((attribute != null) ? attribute.GetIntFromAttribute() : null) ?? defaultValue;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x0003B6E4 File Offset: 0x000398E4
		public static int? GetIntFromAttribute(this XAttribute attribute)
		{
			bool flag = string.IsNullOrEmpty((attribute != null) ? attribute.Value : null);
			int? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = XmlAdapter.GetInt(attribute.Value);
			}
			return result;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x0003B724 File Offset: 0x00039924
		private static int? GetInt(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			int? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int value;
				bool flag2 = !int.TryParse(s, out value);
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new int?(value);
				}
			}
			return result;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0003B770 File Offset: 0x00039970
		public static T GetEnumFromAttributeInt<T>(this XAttribute attribute, T defaultValue)
		{
			int? intFromAttribute = attribute.GetIntFromAttribute();
			bool flag = intFromAttribute == null || !Enum.IsDefined(typeof(T), intFromAttribute);
			T result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = (T)((object)intFromAttribute);
			}
			return result;
		}
	}
}
