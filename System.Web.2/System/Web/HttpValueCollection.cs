using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000BF RID: 191
	[Serializable]
	internal class HttpValueCollection : NameValueCollection
	{
		// Token: 0x06000D3F RID: 3391 RVA: 0x000250AA File Offset: 0x000232AA
		internal HttpValueCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x000250B8 File Offset: 0x000232B8
		internal HttpValueCollection(HttpValueCollection col) : base(StringComparer.OrdinalIgnoreCase)
		{
			for (int i = 0; i < col.Count; i++)
			{
				this.ThrowIfMaxHttpCollectionKeysExceeded();
				string name = col.BaseGetKey(i);
				object value = col.BaseGet(i);
				base.BaseAdd(name, value);
			}
			base.IsReadOnly = col.IsReadOnly;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002510B File Offset: 0x0002330B
		internal HttpValueCollection(string str, bool readOnly, bool urlencoded, Encoding encoding) : base(StringComparer.OrdinalIgnoreCase)
		{
			if (!string.IsNullOrEmpty(str))
			{
				this.FillFromString(str, urlencoded, encoding);
			}
			base.IsReadOnly = readOnly;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00025131 File Offset: 0x00023331
		internal HttpValueCollection(int capacity) : base(capacity, StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002513F File Offset: 0x0002333F
		protected HttpValueCollection(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00025149 File Offset: 0x00023349
		internal void EnableGranularValidation(ValidateStringCallback validationCallback)
		{
			this._keysAwaitingValidation = new HashSet<string>(this.Keys.Cast<string>().Where(new Func<string, bool>(HttpValueCollection.KeyIsCandidateForValidation)), StringComparer.OrdinalIgnoreCase);
			this._validationCallback = validationCallback;
			base.InvalidateCachedArrays();
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00025184 File Offset: 0x00023384
		internal static bool KeyIsCandidateForValidation(string key)
		{
			return key == null || !key.StartsWith("__", StringComparison.Ordinal);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002519C File Offset: 0x0002339C
		private void EnsureKeyValidated(string key)
		{
			if (this._keysAwaitingValidation == null)
			{
				return;
			}
			if (!this._keysAwaitingValidation.Contains(key))
			{
				return;
			}
			string value = base.Get(key);
			if (!string.IsNullOrEmpty(value))
			{
				this._validationCallback(key, value);
			}
			this._keysAwaitingValidation.Remove(key);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000251EC File Offset: 0x000233EC
		public override string Get(int index)
		{
			string key = this.GetKey(index);
			this.EnsureKeyValidated(key);
			return base.Get(index);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002520F File Offset: 0x0002340F
		public override string Get(string name)
		{
			this.EnsureKeyValidated(name);
			return base.Get(name);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00025220 File Offset: 0x00023420
		public override string[] GetValues(int index)
		{
			string key = this.GetKey(index);
			this.EnsureKeyValidated(key);
			return base.GetValues(index);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00025243 File Offset: 0x00023443
		public override string[] GetValues(string name)
		{
			this.EnsureKeyValidated(name);
			return base.GetValues(name);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000164EA File Offset: 0x000146EA
		internal void MakeReadOnly()
		{
			base.IsReadOnly = true;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00025253 File Offset: 0x00023453
		internal void MakeReadWrite()
		{
			base.IsReadOnly = false;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002525C File Offset: 0x0002345C
		internal void FillFromString(string s)
		{
			this.FillFromString(s, false, null);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00025268 File Offset: 0x00023468
		internal void FillFromString(string s, bool urlencoded, Encoding encoding)
		{
			int num = (s != null) ? s.Length : 0;
			for (int i = 0; i < num; i++)
			{
				this.ThrowIfMaxHttpCollectionKeysExceeded();
				int num2 = i;
				int num3 = -1;
				while (i < num)
				{
					char c = s[i];
					if (c == '=')
					{
						if (num3 < 0)
						{
							num3 = i;
						}
					}
					else if (c == '&')
					{
						break;
					}
					i++;
				}
				string text = null;
				string text2;
				if (num3 >= 0)
				{
					text = s.Substring(num2, num3 - num2);
					text2 = s.Substring(num3 + 1, i - num3 - 1);
				}
				else
				{
					text2 = s.Substring(num2, i - num2);
				}
				if (urlencoded)
				{
					base.Add(HttpUtility.UrlDecode(text, encoding), HttpUtility.UrlDecode(text2, encoding));
				}
				else
				{
					base.Add(text, text2);
				}
				if (i == num - 1 && s[i] == '&')
				{
					base.Add(null, string.Empty);
				}
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002533C File Offset: 0x0002353C
		internal void FillFromEncodedBytes(byte[] bytes, Encoding encoding)
		{
			int num = (bytes != null) ? bytes.Length : 0;
			for (int i = 0; i < num; i++)
			{
				this.ThrowIfMaxHttpCollectionKeysExceeded();
				int num2 = i;
				int num3 = -1;
				while (i < num)
				{
					byte b = bytes[i];
					if (b == 61)
					{
						if (num3 < 0)
						{
							num3 = i;
						}
					}
					else if (b == 38)
					{
						break;
					}
					i++;
				}
				string name;
				string value;
				if (num3 >= 0)
				{
					name = HttpUtility.UrlDecode(bytes, num2, num3 - num2, encoding);
					value = HttpUtility.UrlDecode(bytes, num3 + 1, i - num3 - 1, encoding);
				}
				else
				{
					name = null;
					value = HttpUtility.UrlDecode(bytes, num2, i - num2, encoding);
				}
				base.Add(name, value);
				if (i == num - 1 && bytes[i] == 38)
				{
					base.Add(null, string.Empty);
				}
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000253EC File Offset: 0x000235EC
		internal void Add(HttpCookieCollection c)
		{
			int count = c.Count;
			for (int i = 0; i < count; i++)
			{
				this.ThrowIfMaxHttpCollectionKeysExceeded();
				HttpCookie httpCookie = c.Get(i);
				base.Add(httpCookie.Name, httpCookie.Value);
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002542C File Offset: 0x0002362C
		internal void ThrowIfMaxHttpCollectionKeysExceeded()
		{
			if (base.Count >= AppSettings.MaxHttpCollectionKeys)
			{
				throw new InvalidOperationException(SR.GetString("CollectionCountExceeded_HttpValueCollection", new object[]
				{
					AppSettings.MaxHttpCollectionKeys
				}));
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002545E File Offset: 0x0002365E
		internal void Reset()
		{
			base.Clear();
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00025466 File Offset: 0x00023666
		public override string ToString()
		{
			return this.ToString(true);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002546F File Offset: 0x0002366F
		internal virtual string ToString(bool urlencoded)
		{
			return this.ToString(urlencoded, null);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002547C File Offset: 0x0002367C
		internal virtual string ToString(bool urlencoded, IDictionary excludeKeys)
		{
			int count = this.Count;
			if (count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = excludeKeys != null && excludeKeys["__VIEWSTATE"] != null;
			for (int i = 0; i < count; i++)
			{
				string text = this.GetKey(i);
				if ((!flag || text == null || !text.StartsWith("__VIEWSTATE", StringComparison.Ordinal)) && (excludeKeys == null || text == null || excludeKeys[text] == null))
				{
					if (urlencoded)
					{
						text = HttpValueCollection.UrlEncodeForToString(text);
					}
					string value = (text != null) ? (text + "=") : string.Empty;
					string[] values = this.GetValues(i);
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append('&');
					}
					if (values == null || values.Length == 0)
					{
						stringBuilder.Append(value);
					}
					else if (values.Length == 1)
					{
						stringBuilder.Append(value);
						string text2 = values[0];
						if (urlencoded)
						{
							text2 = HttpValueCollection.UrlEncodeForToString(text2);
						}
						stringBuilder.Append(text2);
					}
					else
					{
						for (int j = 0; j < values.Length; j++)
						{
							if (j > 0)
							{
								stringBuilder.Append('&');
							}
							stringBuilder.Append(value);
							string text2 = values[j];
							if (urlencoded)
							{
								text2 = HttpValueCollection.UrlEncodeForToString(text2);
							}
							stringBuilder.Append(text2);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000255C4 File Offset: 0x000237C4
		internal static string UrlEncodeForToString(string input)
		{
			if (AppSettings.DontUsePercentUUrlEncoding)
			{
				return HttpUtility.UrlEncode(input);
			}
			return HttpUtility.UrlEncodeUnicode(input);
		}

		// Token: 0x040004E9 RID: 1257
		[NonSerialized]
		private ValidateStringCallback _validationCallback;

		// Token: 0x040004EA RID: 1258
		[NonSerialized]
		private HashSet<string> _keysAwaitingValidation;
	}
}
