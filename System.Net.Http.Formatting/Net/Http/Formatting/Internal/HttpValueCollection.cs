using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Properties;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Internal
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	internal class HttpValueCollection : NameValueCollection
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x00007CDD File Offset: 0x00005EDD
		protected HttpValueCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00007CE7 File Offset: 0x00005EE7
		private HttpValueCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007CF4 File Offset: 0x00005EF4
		internal static HttpValueCollection Create()
		{
			return new HttpValueCollection();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007CFC File Offset: 0x00005EFC
		internal static HttpValueCollection Create(IEnumerable<KeyValuePair<string, string>> pairs)
		{
			HttpValueCollection httpValueCollection = new HttpValueCollection();
			foreach (KeyValuePair<string, string> keyValuePair in pairs)
			{
				httpValueCollection.Add(keyValuePair.Key, keyValuePair.Value);
			}
			httpValueCollection.IsReadOnly = false;
			return httpValueCollection;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007D60 File Offset: 0x00005F60
		public override void Add(string name, string value)
		{
			HttpValueCollection.ThrowIfMaxHttpCollectionKeysExceeded(this.Count);
			name = (name ?? string.Empty);
			value = (value ?? string.Empty);
			base.Add(name, value);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007D8D File Offset: 0x00005F8D
		public override string ToString()
		{
			return this.ToString(true);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007D98 File Offset: 0x00005F98
		private static void ThrowIfMaxHttpCollectionKeysExceeded(int count)
		{
			if (count >= MediaTypeFormatter.MaxHttpCollectionKeys)
			{
				throw Error.InvalidOperation(Resources.MaxHttpCollectionKeyLimitReached, new object[]
				{
					MediaTypeFormatter.MaxHttpCollectionKeys,
					typeof(MediaTypeFormatter)
				});
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007DDC File Offset: 0x00005FDC
		private string ToString(bool urlEncode)
		{
			if (this.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool first = true;
			foreach (object obj in this)
			{
				string name = (string)obj;
				string[] values = this.GetValues(name);
				if (values == null || values.Length == 0)
				{
					first = HttpValueCollection.AppendNameValuePair(stringBuilder, first, urlEncode, name, string.Empty);
				}
				else
				{
					foreach (string value in values)
					{
						first = HttpValueCollection.AppendNameValuePair(stringBuilder, first, urlEncode, name, value);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007E98 File Offset: 0x00006098
		private static bool AppendNameValuePair(StringBuilder builder, bool first, bool urlEncode, string name, string value)
		{
			string text = name ?? string.Empty;
			string value2 = urlEncode ? UriQueryUtility.UrlEncode(text) : text;
			string text2 = value ?? string.Empty;
			string value3 = urlEncode ? UriQueryUtility.UrlEncode(text2) : text2;
			if (first)
			{
				first = false;
			}
			else
			{
				builder.Append("&");
			}
			builder.Append(value2);
			if (!string.IsNullOrEmpty(value3))
			{
				builder.Append("=");
				builder.Append(value3);
			}
			return first;
		}
	}
}
