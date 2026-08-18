using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Text;

namespace ClockWorkAPI
{
	// Token: 0x02000021 RID: 33
	public static class SearchResponseAdapter
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00008EFC File Offset: 0x00007EFC
		public static List<KeyValuePair<string, string>> GetAttributes(this SearchResponse response)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);
			List<KeyValuePair<string, string>> result;
			if (response.Entries.Count > 0)
			{
				SearchResultEntry searchResultEntry = response.Entries[0];
				foreach (object obj in searchResultEntry.Attributes.Values)
				{
					DirectoryAttribute directoryAttribute = (DirectoryAttribute)obj;
					foreach (object obj2 in directoryAttribute)
					{
						byte[] bytes = (byte[])obj2;
						try
						{
							string @string = utf8Encoding.GetString(bytes);
							list.Add(new KeyValuePair<string, string>(directoryAttribute.Name, @string));
						}
						catch
						{
						}
					}
				}
				result = list;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
