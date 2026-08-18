using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x0200005A RID: 90
	public static class XmlAdapter
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00006080 File Offset: 0x00004280
		public static IList<int> ParseIntList(this string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			List<int> list = new List<int>();
			foreach (string s2 in array)
			{
				int item;
				bool flag = int.TryParse(s2, out item);
				if (flag)
				{
					bool flag2 = !list.Contains(item);
					if (flag2)
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000060F0 File Offset: 0x000042F0
		public static IList<string> ParseStringList(this string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			List<string> list = new List<string>();
			foreach (string text in array)
			{
				string text2 = text.Trim();
				bool flag = text2.Length > 0 && !list.Contains(text2);
				if (flag)
				{
					list.Add(text2);
				}
			}
			return list;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00006168 File Offset: 0x00004368
		public static int GetIntFromAttribute(this XElement element, int defaultValue = 0)
		{
			bool flag = element == null;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num;
				bool flag2 = !int.TryParse(element.Value, out num);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000061A0 File Offset: 0x000043A0
		public static int GetIntFromAttribute(this XElement element, string attributeName, int defaultValue = 0)
		{
			bool flag = element == null;
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				XAttribute xattribute = element.Attribute(attributeName);
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					int num;
					bool flag3 = !int.TryParse(element.Value, out num);
					if (flag3)
					{
						result = defaultValue;
					}
					else
					{
						result = num;
					}
				}
			}
			return result;
		}
	}
}
