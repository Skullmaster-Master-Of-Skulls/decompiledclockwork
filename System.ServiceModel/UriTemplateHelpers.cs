using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel;

namespace System
{
	// Token: 0x02000008 RID: 8
	internal static class UriTemplateHelpers
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002294 File Offset: 0x00000494
		[Conditional("DEBUG")]
		public static void AssertCanonical(string s)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002298 File Offset: 0x00000498
		public static bool CanMatchQueryInterestingly(UriTemplate ut, NameValueCollection query, bool mustBeEspeciallyInteresting)
		{
			if (ut.queries.Count == 0)
			{
				return false;
			}
			string[] allKeys = query.AllKeys;
			foreach (KeyValuePair<string, UriTemplateQueryValue> keyValuePair in ut.queries)
			{
				string key = keyValuePair.Key;
				if (keyValuePair.Value.Nature == UriTemplatePartType.Literal)
				{
					bool flag = false;
					for (int i = 0; i < allKeys.Length; i++)
					{
						if (StringComparer.OrdinalIgnoreCase.Equals(allKeys[i], key))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
					if (keyValuePair.Value == UriTemplateQueryValue.Empty)
					{
						if (!string.IsNullOrEmpty(query[key]))
						{
							return false;
						}
					}
					else if (((UriTemplateLiteralQueryValue)keyValuePair.Value).AsRawUnescapedString() != query[key])
					{
						return false;
					}
				}
				else if (mustBeEspeciallyInteresting && Array.IndexOf<string>(allKeys, key) == -1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023A8 File Offset: 0x000005A8
		public static bool CanMatchQueryTrivially(UriTemplate ut)
		{
			return ut.queries.Count == 0;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023B8 File Offset: 0x000005B8
		public static void DisambiguateSamePath(UriTemplate[] array, int a, int b, bool allowDuplicateEquivalentUriTemplates)
		{
			Array.Sort<UriTemplate>(array, a, b - a, UriTemplateHelpers.queryComparer);
			if (b - a == 1)
			{
				return;
			}
			if (!allowDuplicateEquivalentUriTemplates)
			{
				if (array[a].queries.Count == 0)
				{
					a++;
				}
				if (array[a].queries.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTDuplicate", new object[]
					{
						array[a].ToString(),
						array[a - 1].ToString()
					})));
				}
				if (b - a == 1)
				{
					return;
				}
			}
			else
			{
				while (a < b && array[a].queries.Count == 0)
				{
					a++;
				}
				if (b - a <= 1)
				{
					return;
				}
			}
			UriTemplateHelpers.EnsureQueriesAreDistinct(array, a, b, allowDuplicateEquivalentUriTemplates);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002468 File Offset: 0x00000668
		public static IEqualityComparer<string> GetQueryKeyComparer()
		{
			return UriTemplateHelpers.queryKeyComperar;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000246F File Offset: 0x0000066F
		public static string GetUriPath(Uri uri)
		{
			return uri.GetComponents(UriComponents.Path | UriComponents.KeepDelimiter, UriFormat.Unescaped);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002480 File Offset: 0x00000680
		public static bool HasQueryLiteralRequirements(UriTemplate ut)
		{
			foreach (UriTemplateQueryValue uriTemplateQueryValue in ut.queries.Values)
			{
				if (uriTemplateQueryValue.Nature == UriTemplatePartType.Literal)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024E0 File Offset: 0x000006E0
		public static UriTemplatePartType IdentifyPartType(string part)
		{
			int num = part.IndexOf("{", StringComparison.Ordinal);
			int num2 = part.IndexOf("}", StringComparison.Ordinal);
			if (num == -1)
			{
				if (num2 != -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidFormatSegmentOrQueryPart", new object[]
					{
						part
					})));
				}
				return UriTemplatePartType.Literal;
			}
			else
			{
				if (num2 < num + 2)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidFormatSegmentOrQueryPart", new object[]
					{
						part
					})));
				}
				if (num > 0)
				{
					return UriTemplatePartType.Compound;
				}
				if (num2 < part.Length - 2 || (num2 == part.Length - 2 && !part.EndsWith("/", StringComparison.Ordinal)))
				{
					return UriTemplatePartType.Compound;
				}
				return UriTemplatePartType.Variable;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000258C File Offset: 0x0000078C
		public static bool IsWildcardPath(string path)
		{
			UriTemplatePartType uriTemplatePartType;
			return path.IndexOf('/') == -1 && UriTemplateHelpers.IsWildcardSegment(path, out uriTemplatePartType);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025B0 File Offset: 0x000007B0
		public static bool IsWildcardSegment(string segment, out UriTemplatePartType type)
		{
			type = UriTemplateHelpers.IdentifyPartType(segment);
			switch (type)
			{
			case UriTemplatePartType.Literal:
				return string.Compare(segment, "*", StringComparison.Ordinal) == 0;
			case UriTemplatePartType.Compound:
				return false;
			case UriTemplatePartType.Variable:
				return segment.IndexOf("*", StringComparison.Ordinal) == 1 && !segment.EndsWith("/", StringComparison.Ordinal) && segment.Length > "*".Length + 2;
			default:
				return false;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002624 File Offset: 0x00000824
		public static NameValueCollection ParseQueryString(string query)
		{
			NameValueCollection nameValueCollection = UrlUtility.ParseQueryString(query);
			string text = nameValueCollection[null];
			if (!string.IsNullOrEmpty(text))
			{
				nameValueCollection.Remove(null);
				string[] array = text.Split(new char[]
				{
					','
				});
				for (int i = 0; i < array.Length; i++)
				{
					nameValueCollection.Add(array[i], null);
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000267C File Offset: 0x0000087C
		private static bool AllTemplatesAreEquivalent(IList<UriTemplate> array, int a, int b)
		{
			for (int i = a; i < b - 1; i++)
			{
				if (!array[i].IsEquivalentTo(array[i + 1]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026B4 File Offset: 0x000008B4
		private static void EnsureQueriesAreDistinct(UriTemplate[] array, int a, int b, bool allowDuplicateEquivalentUriTemplates)
		{
			Dictionary<string, byte> dictionary = new Dictionary<string, byte>(StringComparer.OrdinalIgnoreCase);
			for (int i = a; i < b; i++)
			{
				foreach (KeyValuePair<string, UriTemplateQueryValue> keyValuePair in array[i].queries)
				{
					if (keyValuePair.Value.Nature == UriTemplatePartType.Literal && !dictionary.ContainsKey(keyValuePair.Key))
					{
						dictionary.Add(keyValuePair.Key, 0);
					}
				}
			}
			Dictionary<string, byte> dictionary2 = new Dictionary<string, byte>(dictionary);
			for (int j = a; j < b; j++)
			{
				foreach (string key in dictionary.Keys)
				{
					if (!array[j].queries.ContainsKey(key) || array[j].queries[key].Nature != UriTemplatePartType.Literal)
					{
						dictionary2.Remove(key);
					}
				}
			}
			dictionary = null;
			if (dictionary2.Count == 0 && (!allowDuplicateEquivalentUriTemplates || !UriTemplateHelpers.AllTemplatesAreEquivalent(array, a, b)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTOtherAmbiguousQueries", new object[]
				{
					array[a].ToString()
				})));
			}
			string[][] array2 = new string[b - a][];
			for (int k = 0; k < b - a; k++)
			{
				array2[k] = UriTemplateHelpers.GetQueryLiterals(array[k + a], dictionary2);
			}
			for (int l = 0; l < b - a; l++)
			{
				for (int m = l + 1; m < b - a; m++)
				{
					if (UriTemplateHelpers.Same(array2[l], array2[m]))
					{
						if (!array[l + a].IsEquivalentTo(array[m + a]))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTAmbiguousQueries", new object[]
							{
								array[a + l].ToString(),
								array[m + a].ToString()
							})));
						}
						if (!allowDuplicateEquivalentUriTemplates)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTDuplicate", new object[]
							{
								array[a + l].ToString(),
								array[m + a].ToString()
							})));
						}
					}
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000290C File Offset: 0x00000B0C
		private static string[] GetQueryLiterals(UriTemplate up, Dictionary<string, byte> queryVarNames)
		{
			string[] array = new string[queryVarNames.Count];
			int num = 0;
			foreach (string key in queryVarNames.Keys)
			{
				UriTemplateQueryValue uriTemplateQueryValue = up.queries[key];
				if (uriTemplateQueryValue == UriTemplateQueryValue.Empty)
				{
					array[num] = null;
				}
				else
				{
					array[num] = ((UriTemplateLiteralQueryValue)uriTemplateQueryValue).AsRawUnescapedString();
				}
				num++;
			}
			return array;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002998 File Offset: 0x00000B98
		private static bool Same(string[] a, string[] b)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000054 RID: 84
		private static UriTemplateHelpers.UriTemplateQueryComparer queryComparer = new UriTemplateHelpers.UriTemplateQueryComparer();

		// Token: 0x04000055 RID: 85
		private static UriTemplateHelpers.UriTemplateQueryKeyComparer queryKeyComperar = new UriTemplateHelpers.UriTemplateQueryKeyComparer();

		// Token: 0x02000AB1 RID: 2737
		private class UriTemplateQueryComparer : IComparer<UriTemplate>
		{
			// Token: 0x06006DC6 RID: 28102 RVA: 0x0019A098 File Offset: 0x00198298
			public int Compare(UriTemplate x, UriTemplate y)
			{
				return Comparer<int>.Default.Compare(x.queries.Count, y.queries.Count);
			}
		}

		// Token: 0x02000AB2 RID: 2738
		private class UriTemplateQueryKeyComparer : IEqualityComparer<string>
		{
			// Token: 0x06006DC8 RID: 28104 RVA: 0x0019A0C2 File Offset: 0x001982C2
			public bool Equals(string x, string y)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x06006DC9 RID: 28105 RVA: 0x0019A0CF File Offset: 0x001982CF
			public int GetHashCode(string obj)
			{
				if (obj == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("obj");
				}
				return obj.ToUpperInvariant().GetHashCode();
			}
		}
	}
}
