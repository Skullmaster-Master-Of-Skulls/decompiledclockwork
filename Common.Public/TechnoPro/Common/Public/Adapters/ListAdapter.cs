using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005F0 RID: 1520
	public static class ListAdapter
	{
		// Token: 0x060030D7 RID: 12503 RVA: 0x00042D80 File Offset: 0x00040F80
		public static IList<T> RemoveDuplicateItemsFromList<T>(this IList<T> items, Func<Pair<T, T>, int> itemCompare)
		{
			List<T> list = items.ToList<T>();
			list.Sort((T g1, T g2) => itemCompare(new Pair<T, T>(g1, g2)));
			List<T> list2 = new List<T>();
			for (int i = 1; i < list.Count; i++)
			{
				int num = itemCompare(new Pair<T, T>(list[i - 1], list[i]));
				bool flag = num == 0;
				if (flag)
				{
					list2.Add(list[i - 1]);
				}
			}
			foreach (T item in list2)
			{
				list.Remove(item);
			}
			return list;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x00042E60 File Offset: 0x00041060
		public static List<T> GetListRange<T>(this List<T> items, int startIndex, int count)
		{
			bool flag = items == null || items.Count < 1 || items.Count <= count;
			List<T> result;
			if (flag)
			{
				result = items;
			}
			else
			{
				result = items.GetRange(startIndex, count);
			}
			return result;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x00042EA0 File Offset: 0x000410A0
		public static string StringDictionaryToXml(this IDictionary<string, string> args)
		{
			bool flag = args == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
				object[] array = new object[1];
				array[0] = new XElement("args", (from g in args
				select new XElement(g.Key, g.Value ?? "")).ToArray<object>());
				XDocument xdocument = new XDocument(declaration, array);
				result = xdocument.Declaration.ToString() + xdocument.ToString();
			}
			return result;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x00042F34 File Offset: 0x00041134
		public static IDictionary<string, string> StringDictionaryFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			IDictionary<string, string> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					XDocument xdocument = XDocument.Parse(xml);
					XElement xelement = xdocument.Element("args");
					IEnumerable<XElement> enumerable = (xelement != null) ? xelement.Elements() : null;
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					foreach (XElement xelement2 in enumerable)
					{
						try
						{
							string localName = xelement2.Name.LocalName;
							bool flag2 = dictionary.ContainsKey(localName);
							if (!flag2)
							{
								dictionary.Add(localName, xelement2.Value);
							}
						}
						catch
						{
						}
					}
					return dictionary;
				}
				catch
				{
				}
				result = null;
			}
			return result;
		}
	}
}
