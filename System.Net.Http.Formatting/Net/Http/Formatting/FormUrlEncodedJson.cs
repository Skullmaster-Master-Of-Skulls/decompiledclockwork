using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000031 RID: 49
	internal static class FormUrlEncodedJson
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000068F6 File Offset: 0x00004AF6
		public static JObject Parse(IEnumerable<KeyValuePair<string, string>> nameValuePairs)
		{
			return FormUrlEncodedJson.ParseInternal(nameValuePairs, int.MaxValue, true);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006904 File Offset: 0x00004B04
		public static JObject Parse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth)
		{
			return FormUrlEncodedJson.ParseInternal(nameValuePairs, maxDepth, true);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006910 File Offset: 0x00004B10
		public static bool TryParse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, out JObject value)
		{
			JObject jobject;
			value = (jobject = FormUrlEncodedJson.ParseInternal(nameValuePairs, int.MaxValue, false));
			return jobject != null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006934 File Offset: 0x00004B34
		public static bool TryParse(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth, out JObject value)
		{
			JObject jobject;
			value = (jobject = FormUrlEncodedJson.ParseInternal(nameValuePairs, maxDepth, false));
			return jobject != null;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006954 File Offset: 0x00004B54
		private static JObject ParseInternal(IEnumerable<KeyValuePair<string, string>> nameValuePairs, int maxDepth, bool throwOnError)
		{
			if (nameValuePairs == null)
			{
				throw Error.ArgumentNull("nameValuePairs");
			}
			if (maxDepth <= 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxDepth", maxDepth, 1);
			}
			JObject jobject = new JObject();
			foreach (KeyValuePair<string, string> keyValuePair in nameValuePairs)
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (key == null)
				{
					if (string.IsNullOrEmpty(value))
					{
						if (throwOnError)
						{
							throw Error.Argument("nameValuePairs", Resources.QueryStringNameShouldNotNull, new object[0]);
						}
						return null;
					}
					else
					{
						string[] path = new string[]
						{
							value
						};
						if (!FormUrlEncodedJson.Insert(jobject, path, null, throwOnError))
						{
							return null;
						}
					}
				}
				else
				{
					string[] path2 = FormUrlEncodedJson.GetPath(key, maxDepth, throwOnError);
					if (path2 == null || !FormUrlEncodedJson.Insert(jobject, path2, value, throwOnError))
					{
						return null;
					}
				}
			}
			FormUrlEncodedJson.FixContiguousArrays(jobject);
			return jobject;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006A54 File Offset: 0x00004C54
		private static string[] GetPath(string key, int maxDepth, bool throwOnError)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return FormUrlEncodedJson._emptyPath;
			}
			if (!FormUrlEncodedJson.ValidateQueryString(key, throwOnError))
			{
				return null;
			}
			string[] array = key.Split(new char[]
			{
				'['
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].EndsWith("]", StringComparison.Ordinal))
				{
					array[i] = array[i].Substring(0, array[i].Length - 1);
				}
			}
			if (array.Length < maxDepth)
			{
				return array;
			}
			if (throwOnError)
			{
				throw Error.Argument(Resources.MaxDepthExceeded, new object[]
				{
					maxDepth
				});
			}
			return null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006AEC File Offset: 0x00004CEC
		private static bool ValidateQueryString(string key, bool throwOnError)
		{
			bool flag = false;
			for (int i = 0; i < key.Length; i++)
			{
				switch (key[i])
				{
				case '[':
					if (!flag)
					{
						flag = true;
					}
					else
					{
						if (throwOnError)
						{
							throw Error.Argument(Resources.NestedBracketNotValid, "application/x-www-form-urlencoded", new object[]
							{
								i
							});
						}
						return false;
					}
					break;
				case ']':
					if (flag)
					{
						flag = false;
					}
					else
					{
						if (throwOnError)
						{
							throw Error.Argument(Resources.UnMatchedBracketNotValid, "application/x-www-form-urlencoded", new object[]
							{
								i
							});
						}
						return false;
					}
					break;
				}
			}
			if (!flag)
			{
				return true;
			}
			if (throwOnError)
			{
				throw Error.Argument(Resources.NestedBracketNotValid, "application/x-www-form-urlencoded", new object[]
				{
					key.LastIndexOf('[')
				});
			}
			return false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006BC4 File Offset: 0x00004DC4
		private static bool Insert(JObject root, string[] path, string value, bool throwOnError)
		{
			JObject jobject = root;
			JObject parent = null;
			int i = 0;
			while (i < path.Length - 1)
			{
				if (string.IsNullOrEmpty(path[i]))
				{
					if (throwOnError)
					{
						throw Error.Argument(Resources.InvalidArrayInsert, FormUrlEncodedJson.BuildPathString(path, i), new object[0]);
					}
					return false;
				}
				else
				{
					if (!((IDictionary<string, JToken>)jobject).ContainsKey(path[i]))
					{
						jobject[path[i]] = new JObject();
					}
					else if (jobject[path[i]] == null || jobject[path[i]] is JValue)
					{
						if (throwOnError)
						{
							throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, i), new object[0]);
						}
						return false;
					}
					parent = jobject;
					jobject = (jobject[path[i]] as JObject);
					i++;
				}
			}
			string value2 = path[path.Length - 1];
			if (string.IsNullOrEmpty(value2) && path.Length > 1)
			{
				if (!FormUrlEncodedJson.AddToArray(parent, path, value, throwOnError))
				{
					return false;
				}
			}
			else if (jobject == null)
			{
				if (throwOnError)
				{
					throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, path.Length - 1), new object[0]);
				}
				return false;
			}
			else if (!FormUrlEncodedJson.AddToObject(jobject, path, value, throwOnError))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006CCC File Offset: 0x00004ECC
		private static bool AddToObject(JObject obj, string[] path, string value, bool throwOnError)
		{
			int num = path.Length - 1;
			string text = path[num];
			if (((IDictionary<string, JToken>)obj).ContainsKey(text))
			{
				if (obj[text] == null || obj[text].Type == JTokenType.Null)
				{
					if (throwOnError)
					{
						throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, num), new object[0]);
					}
					return false;
				}
				else
				{
					bool flag = path.Length == 1;
					if (flag)
					{
						if (obj[text].Type == JTokenType.String)
						{
							string value2 = obj[text].ToObject<string>();
							obj[text] = new JObject
							{
								{
									"0",
									value2
								},
								{
									"1",
									value
								}
							};
						}
						else if (obj[text] is JObject)
						{
							JObject jobject = obj[text] as JObject;
							string index = FormUrlEncodedJson.GetIndex(jobject, throwOnError);
							if (index == null)
							{
								return false;
							}
							jobject.Add(index, value);
						}
					}
					else
					{
						if (throwOnError)
						{
							throw Error.Argument(Resources.JQuery13CompatModeNotSupportNestedJson, FormUrlEncodedJson.BuildPathString(path, num), new object[0]);
						}
						return false;
					}
				}
			}
			else if (value == null)
			{
				obj[text] = null;
			}
			else
			{
				obj[text] = value;
			}
			return true;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006DFC File Offset: 0x00004FFC
		private static bool AddToArray(JObject parent, string[] path, string value, bool throwOnError)
		{
			string propertyName = path[path.Length - 2];
			JObject jobject = parent[propertyName] as JObject;
			if (jobject == null)
			{
				if (throwOnError)
				{
					throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, FormUrlEncodedJson.BuildPathString(path, path.Length - 1), new object[0]);
				}
				return false;
			}
			else
			{
				string index = FormUrlEncodedJson.GetIndex(jobject, throwOnError);
				if (index == null)
				{
					return false;
				}
				jobject.Add(index, value);
				return true;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006E60 File Offset: 0x00005060
		private static string GetIndex(JObject jsonObject, bool throwOnError)
		{
			int num = -1;
			if (jsonObject.Count > 0)
			{
				IEnumerable<string> keys = ((IDictionary<string, JToken>)jsonObject).Keys;
				foreach (string text in keys)
				{
					int num2;
					if (int.TryParse(text, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2) && num2 > num)
					{
						num = num2;
					}
					else
					{
						if (throwOnError)
						{
							throw Error.Argument(Resources.FormUrlEncodedMismatchingTypes, text, new object[0]);
						}
						return null;
					}
				}
			}
			num++;
			return num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006F00 File Offset: 0x00005100
		private static void FixContiguousArrays(JToken jv)
		{
			JArray jarray = jv as JArray;
			if (jarray != null)
			{
				for (int i = 0; i < jarray.Count; i++)
				{
					if (jarray[i] != null)
					{
						jarray[i] = FormUrlEncodedJson.FixSingleContiguousArray(jarray[i]);
						FormUrlEncodedJson.FixContiguousArrays(jarray[i]);
					}
				}
				return;
			}
			JObject jobject = jv as JObject;
			if (jobject != null && jobject.Count > 0)
			{
				List<string> list = new List<string>(((IDictionary<string, JToken>)jobject).Keys);
				foreach (string propertyName in list)
				{
					if (jobject[propertyName] != null)
					{
						jobject[propertyName] = FormUrlEncodedJson.FixSingleContiguousArray(jobject[propertyName]);
						FormUrlEncodedJson.FixContiguousArrays(jobject[propertyName]);
					}
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006FD8 File Offset: 0x000051D8
		private static JToken FixSingleContiguousArray(JToken original)
		{
			JObject jobject = original as JObject;
			if (jobject != null && jobject.Count > 0)
			{
				List<string> keys = new List<string>(((IDictionary<string, JToken>)jobject).Keys);
				List<string> list;
				if (FormUrlEncodedJson.CanBecomeArray(keys, out list))
				{
					JArray jarray = new JArray();
					foreach (string propertyName in list)
					{
						jarray.Add(jobject[propertyName]);
					}
					return jarray;
				}
			}
			return original;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000707C File Offset: 0x0000527C
		private static bool CanBecomeArray(List<string> keys, out List<string> sortedKeys)
		{
			List<FormUrlEncodedJson.ArrayCandidate> list = new List<FormUrlEncodedJson.ArrayCandidate>();
			sortedKeys = null;
			bool flag = true;
			foreach (string text in keys)
			{
				int key;
				if (!int.TryParse(text, NumberStyles.None, CultureInfo.InvariantCulture, out key))
				{
					flag = false;
					break;
				}
				string text2 = key.ToString(CultureInfo.InvariantCulture);
				if (!text2.Equals(text, StringComparison.Ordinal))
				{
					flag = false;
					break;
				}
				list.Add(new FormUrlEncodedJson.ArrayCandidate(key, text2));
			}
			if (flag)
			{
				list.Sort((FormUrlEncodedJson.ArrayCandidate x, FormUrlEncodedJson.ArrayCandidate y) => x.Key - y.Key);
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Key != i)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				sortedKeys = new List<string>(from x in list
				select x.Value);
			}
			return flag;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000718C File Offset: 0x0000538C
		private static string BuildPathString(string[] path, int i)
		{
			StringBuilder stringBuilder = new StringBuilder(path[0]);
			for (int j = 1; j <= i; j++)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[{0}]", new object[]
				{
					path[j]
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400006B RID: 107
		private const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";

		// Token: 0x0400006C RID: 108
		private const int MinDepth = 0;

		// Token: 0x0400006D RID: 109
		private static readonly string[] _emptyPath = new string[]
		{
			string.Empty
		};

		// Token: 0x02000032 RID: 50
		private class ArrayCandidate
		{
			// Token: 0x0600017D RID: 381 RVA: 0x000071F6 File Offset: 0x000053F6
			public ArrayCandidate(int key, string value)
			{
				this.Key = key;
				this.Value = value;
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x0600017E RID: 382 RVA: 0x0000720C File Offset: 0x0000540C
			// (set) Token: 0x0600017F RID: 383 RVA: 0x00007214 File Offset: 0x00005414
			public int Key { get; set; }

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000180 RID: 384 RVA: 0x0000721D File Offset: 0x0000541D
			// (set) Token: 0x06000181 RID: 385 RVA: 0x00007225 File Offset: 0x00005425
			public string Value { get; set; }
		}
	}
}
