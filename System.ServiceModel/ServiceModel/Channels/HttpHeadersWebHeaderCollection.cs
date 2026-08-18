using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200088B RID: 2187
	internal class HttpHeadersWebHeaderCollection : WebHeaderCollection
	{
		// Token: 0x06005306 RID: 21254 RVA: 0x00131D44 File Offset: 0x0012FF44
		public HttpHeadersWebHeaderCollection(HttpRequestMessage httpRequestMessage)
		{
			this.httpRequestMessage = httpRequestMessage;
			this.EnsureBaseHasKeysIsAccurate();
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x00131D59 File Offset: 0x0012FF59
		public HttpHeadersWebHeaderCollection(HttpResponseMessage httpResponseMessage)
		{
			this.httpResponseMessage = httpResponseMessage;
			this.EnsureBaseHasKeysIsAccurate();
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06005308 RID: 21256 RVA: 0x00131D6E File Offset: 0x0012FF6E
		public override string[] AllKeys
		{
			get
			{
				return (from header in this.AllHeaders
				select header.Key).ToArray<string>();
			}
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06005309 RID: 21257 RVA: 0x00131D9F File Offset: 0x0012FF9F
		public override int Count
		{
			get
			{
				return this.AllHeaders.Count<KeyValuePair<string, IEnumerable<string>>>();
			}
		}

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x0600530A RID: 21258 RVA: 0x00131DAC File Offset: 0x0012FFAC
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				NameValueCollection nameValueCollection = new NameValueCollection();
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in this.AllHeaders)
				{
					string[] array = keyValuePair.Value.ToArray<string>();
					if (array.Length == 0)
					{
						nameValueCollection.Add(keyValuePair.Key, string.Empty);
					}
					else
					{
						foreach (string value in array)
						{
							nameValueCollection.Add(keyValuePair.Key, value);
						}
					}
				}
				return nameValueCollection.Keys;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x0600530B RID: 21259 RVA: 0x00131E50 File Offset: 0x00130050
		private IEnumerable<KeyValuePair<string, IEnumerable<string>>> AllHeaders
		{
			get
			{
				IEnumerable<KeyValuePair<string, IEnumerable<string>>> enumerable;
				HttpContent content;
				if (this.httpRequestMessage != null)
				{
					enumerable = this.httpRequestMessage.Headers;
					content = this.httpRequestMessage.Content;
				}
				else
				{
					enumerable = this.httpResponseMessage.Headers;
					content = this.httpResponseMessage.Content;
				}
				if (content != null)
				{
					enumerable = enumerable.Concat(content.Headers);
				}
				return enumerable;
			}
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x00131EAA File Offset: 0x001300AA
		public override void Add(string name, string value)
		{
			name = HttpHeadersWebHeaderCollection.CheckBadChars(name, false);
			value = HttpHeadersWebHeaderCollection.CheckBadChars(value, true);
			if (this.httpRequestMessage != null)
			{
				this.httpRequestMessage.AddHeader(name, value);
			}
			else
			{
				this.httpResponseMessage.AddHeader(name, value);
			}
			this.EnsureBaseHasKeysIsAccurate();
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x00131EE8 File Offset: 0x001300E8
		public override void Clear()
		{
			HttpContent content;
			if (this.httpRequestMessage != null)
			{
				this.httpRequestMessage.Headers.Clear();
				content = this.httpRequestMessage.Content;
			}
			else
			{
				this.httpResponseMessage.Headers.Clear();
				content = this.httpResponseMessage.Content;
			}
			if (content != null)
			{
				content.Headers.Clear();
			}
			this.EnsureBaseHasKeysIsAccurate();
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x00131F4D File Offset: 0x0013014D
		public override void Remove(string name)
		{
			name = HttpHeadersWebHeaderCollection.CheckBadChars(name, false);
			if (this.httpRequestMessage != null)
			{
				this.httpRequestMessage.RemoveHeader(name);
			}
			else
			{
				this.httpResponseMessage.RemoveHeader(name);
			}
			this.EnsureBaseHasKeysIsAccurate();
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x00131F80 File Offset: 0x00130180
		public override void Set(string name, string value)
		{
			name = HttpHeadersWebHeaderCollection.CheckBadChars(name, false);
			value = HttpHeadersWebHeaderCollection.CheckBadChars(value, true);
			if (this.httpRequestMessage != null)
			{
				this.httpRequestMessage.SetHeader(name, value);
			}
			else
			{
				this.httpResponseMessage.SetHeader(name, value);
			}
			this.EnsureBaseHasKeysIsAccurate();
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x00131FBE File Offset: 0x001301BE
		public override IEnumerator GetEnumerator()
		{
			return new HttpHeadersWebHeaderCollection.HttpHeadersEnumerator(this.AllKeys);
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x00131FCC File Offset: 0x001301CC
		public override string Get(int index)
		{
			string[] values = this.GetValues(index);
			return HttpHeadersWebHeaderCollection.GetSingleValue(values);
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x00131FE8 File Offset: 0x001301E8
		public override string GetKey(int index)
		{
			return this.GetHeaderAt(index).Key;
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x00132004 File Offset: 0x00130204
		public override string[] GetValues(int index)
		{
			return this.GetHeaderAt(index).Value.ToArray<string>();
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x00132028 File Offset: 0x00130228
		public override string Get(string name)
		{
			string[] values = this.GetValues(name);
			return HttpHeadersWebHeaderCollection.GetSingleValue(values);
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x00132044 File Offset: 0x00130244
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in this.AllHeaders)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key))
				{
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append(": ");
					stringBuilder.AppendLine(HttpHeadersWebHeaderCollection.GetSingleValue(keyValuePair.Value.ToArray<string>()));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x001320D8 File Offset: 0x001302D8
		public override string[] GetValues(string header)
		{
			IEnumerable<string> header2;
			if (this.httpRequestMessage != null)
			{
				header2 = this.httpRequestMessage.GetHeader(header);
			}
			else
			{
				header2 = this.httpResponseMessage.GetHeader(header);
			}
			if (header2 == null)
			{
				return HttpHeadersWebHeaderCollection.emptyStringArray;
			}
			return header2.SelectMany((string str) => str.Split(HttpHeadersWebHeaderCollection.stringSplitArray, StringSplitOptions.None)).ToArray<string>();
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0013213E File Offset: 0x0013033E
		private static string GetSingleValue(string[] values)
		{
			if (values == null)
			{
				return null;
			}
			if (values.Length == 1)
			{
				return values[0];
			}
			return string.Join(",", values);
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x0013215C File Offset: 0x0013035C
		private static string CheckBadChars(string name, bool isHeaderValue)
		{
			if (name != null && name.Length != 0)
			{
				if (isHeaderValue)
				{
					name = name.Trim(HttpHeadersWebHeaderCollection.HttpTrimCharacters);
					int num = 0;
					for (int i = 0; i < name.Length; i++)
					{
						char c = 'ÿ' & name[i];
						switch (num)
						{
						case 0:
							if (c == '\r')
							{
								num = 1;
							}
							else if (c == '\n')
							{
								num = 2;
							}
							else if (c == '\u007f' || (c < ' ' && c != '\t'))
							{
								throw new ArgumentException(SR.GetString("WebHeaderInvalidControlChars"), "value");
							}
							break;
						case 1:
							if (c != '\n')
							{
								throw new ArgumentException(SR.GetString("WebHeaderInvalidCRLFChars"), "value");
							}
							num = 2;
							break;
						case 2:
							if (c != ' ' && c != '\t')
							{
								throw new ArgumentException(SR.GetString("WebHeaderInvalidCRLFChars"), "value");
							}
							num = 0;
							break;
						}
					}
					if (num != 0)
					{
						throw new ArgumentException(SR.GetString("WebHeaderInvalidCRLFChars"), "value");
					}
				}
				else
				{
					if (name.IndexOfAny(HttpHeadersWebHeaderCollection.InvalidParamChars) != -1)
					{
						throw new ArgumentException(SR.GetString("WebHeaderInvalidHeaderChars"), "name");
					}
					if (HttpHeadersWebHeaderCollection.ContainsNonAsciiChars(name))
					{
						throw new ArgumentException(SR.GetString("WebHeaderInvalidNonAsciiChars"), "name");
					}
				}
				return name;
			}
			if (!isHeaderValue)
			{
				throw (name == null) ? new ArgumentNullException("name") : new ArgumentException(SR.GetString("WebHeaderEmptyStringCall", new object[]
				{
					"name"
				}), "name");
			}
			return string.Empty;
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x001322D0 File Offset: 0x001304D0
		private static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x00132308 File Offset: 0x00130508
		private void EnsureBaseHasKeysIsAccurate()
		{
			bool flag = this.hasKeys;
			this.hasKeys = this.BackingHttpHeadersHasKeys();
			if (flag && !this.hasKeys)
			{
				base.Remove("hk");
				return;
			}
			if (!flag && this.hasKeys)
			{
				base.AddWithoutValidate("hk", string.Empty);
			}
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x0013235C File Offset: 0x0013055C
		private bool BackingHttpHeadersHasKeys()
		{
			if (this.httpRequestMessage == null)
			{
				return this.httpResponseMessage.Headers.Any<KeyValuePair<string, IEnumerable<string>>>() || (this.httpResponseMessage.Content != null && this.httpResponseMessage.Content.Headers.Any<KeyValuePair<string, IEnumerable<string>>>());
			}
			return this.httpRequestMessage.Headers.Any<KeyValuePair<string, IEnumerable<string>>>() || (this.httpRequestMessage.Content != null && this.httpRequestMessage.Content.Headers.Any<KeyValuePair<string, IEnumerable<string>>>());
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x001323E4 File Offset: 0x001305E4
		private KeyValuePair<string, IEnumerable<string>> GetHeaderAt(int index)
		{
			if (index >= 0)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> result in this.AllHeaders)
				{
					if (index == 0)
					{
						return result;
					}
					index--;
				}
			}
			throw new ArgumentOutOfRangeException("index", "WebHeaderArgumentOutOfRange");
		}

		// Token: 0x040032AA RID: 12970
		private const string HasKeysHeader = "hk";

		// Token: 0x040032AB RID: 12971
		private static readonly string[] emptyStringArray = new string[]
		{
			string.Empty
		};

		// Token: 0x040032AC RID: 12972
		private static readonly string[] stringSplitArray = new string[]
		{
			", "
		};

		// Token: 0x040032AD RID: 12973
		private static readonly char[] HttpTrimCharacters = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			' '
		};

		// Token: 0x040032AE RID: 12974
		private static readonly char[] InvalidParamChars = new char[]
		{
			'(',
			')',
			'<',
			'>',
			'@',
			',',
			';',
			':',
			'\\',
			'"',
			'\'',
			'/',
			'[',
			']',
			'?',
			'=',
			'{',
			'}',
			' ',
			'\t',
			'\r',
			'\n'
		};

		// Token: 0x040032AF RID: 12975
		private HttpRequestMessage httpRequestMessage;

		// Token: 0x040032B0 RID: 12976
		private HttpResponseMessage httpResponseMessage;

		// Token: 0x040032B1 RID: 12977
		private bool hasKeys;

		// Token: 0x02000D69 RID: 3433
		private class HttpHeadersEnumerator : IEnumerator
		{
			// Token: 0x06007DCB RID: 32203 RVA: 0x001D6528 File Offset: 0x001D4728
			public HttpHeadersEnumerator(string[] keys)
			{
				this.keys = keys;
				this.position = -1;
			}

			// Token: 0x17001C15 RID: 7189
			// (get) Token: 0x06007DCC RID: 32204 RVA: 0x001D653E File Offset: 0x001D473E
			public object Current
			{
				get
				{
					if (this.position < 0 || this.position >= this.keys.Length)
					{
						throw new InvalidOperationException(SR.GetString("WebHeaderEnumOperationCantHappen"));
					}
					return this.keys[this.position];
				}
			}

			// Token: 0x06007DCD RID: 32205 RVA: 0x001D6576 File Offset: 0x001D4776
			public bool MoveNext()
			{
				if (this.position < this.keys.Length - 1)
				{
					this.position++;
					return true;
				}
				this.position = this.keys.Length;
				return false;
			}

			// Token: 0x06007DCE RID: 32206 RVA: 0x001D65A9 File Offset: 0x001D47A9
			public void Reset()
			{
				this.position = -1;
			}

			// Token: 0x0400484B RID: 18507
			private string[] keys;

			// Token: 0x0400484C RID: 18508
			private int position;
		}
	}
}
