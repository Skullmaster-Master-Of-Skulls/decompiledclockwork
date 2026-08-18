using System;
using System.Collections;
using System.Collections.Generic;
using Google.Apis.Util;

namespace Google.Apis.Requests.Parameters
{
	// Token: 0x02000015 RID: 21
	public class ParameterCollection : List<KeyValuePair<string, string>>
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002ED0 File Offset: 0x000010D0
		public ParameterCollection()
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002ED8 File Offset: 0x000010D8
		public ParameterCollection(IEnumerable<KeyValuePair<string, string>> collection) : base(collection)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002EE1 File Offset: 0x000010E1
		public void Add(string key, string value)
		{
			base.Add(new KeyValuePair<string, string>(key, value));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EF0 File Offset: 0x000010F0
		public bool ContainsKey(string key)
		{
			key.ThrowIfNullOrEmpty("key");
			string text;
			return this.TryGetValue(key, out text);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F14 File Offset: 0x00001114
		public bool TryGetValue(string key, out string value)
		{
			key.ThrowIfNullOrEmpty("key");
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				if (keyValuePair.Key.Equals(key))
				{
					value = keyValuePair.Value;
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F8C File Offset: 0x0000118C
		public string GetFirstMatch(string key)
		{
			string result;
			if (!this.TryGetValue(key, out result))
			{
				throw new KeyNotFoundException("Parameter with the name '" + key + "' was not found.");
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002FBB File Offset: 0x000011BB
		public IEnumerable<string> GetAllMatches(string key)
		{
			key.ThrowIfNullOrEmpty("key");
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				if (keyValuePair.Key.Equals(key))
				{
					yield return keyValuePair.Value;
				}
			}
			List<KeyValuePair<string, string>>.Enumerator enumerator = default(List<KeyValuePair<string, string>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x1700001F RID: 31
		public IEnumerable<string> this[string key]
		{
			get
			{
				return this.GetAllMatches(key);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FDC File Offset: 0x000011DC
		public static ParameterCollection FromQueryString(string qs)
		{
			ParameterCollection parameterCollection = new ParameterCollection();
			foreach (string text in qs.Split(new char[]
			{
				'&'
			}))
			{
				string[] array2 = text.Split(new char[]
				{
					'='
				});
				if (array2.Length != 2)
				{
					throw new ArgumentException(string.Format("Invalid query string [{0}]. Invalid part [{1}]", qs, text));
				}
				parameterCollection.Add(Uri.UnescapeDataString(array2[0]), Uri.UnescapeDataString(array2[1]));
			}
			return parameterCollection;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000305C File Offset: 0x0000125C
		public static ParameterCollection FromDictionary(IDictionary<string, object> dictionary)
		{
			ParameterCollection parameterCollection = new ParameterCollection();
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				IEnumerable enumerable = keyValuePair.Value as IEnumerable;
				if (!(keyValuePair.Value is string) && enumerable != null)
				{
					using (IEnumerator enumerator2 = enumerable.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object o = enumerator2.Current;
							parameterCollection.Add(keyValuePair.Key, Utilities.ConvertToString(o));
						}
						continue;
					}
				}
				parameterCollection.Add(keyValuePair.Key, (keyValuePair.Value == null) ? null : Utilities.ConvertToString(keyValuePair.Value));
			}
			return parameterCollection;
		}
	}
}
