using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200002F RID: 47
	public class FormDataCollection : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000672B File Offset: 0x0000492B
		public FormDataCollection(IEnumerable<KeyValuePair<string, string>> pairs)
		{
			if (pairs == null)
			{
				throw Error.ArgumentNull("pairs");
			}
			this._pairs = pairs;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006748 File Offset: 0x00004948
		public FormDataCollection(Uri uri)
		{
			if (uri == null)
			{
				throw Error.ArgumentNull("uri");
			}
			string text = uri.Query;
			if (text != null && text.Length > 0 && text[0] == '?')
			{
				text = text.Substring(1);
			}
			this._pairs = FormDataCollection.ParseQueryString(text);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000067A1 File Offset: 0x000049A1
		public FormDataCollection(string query)
		{
			this._pairs = FormDataCollection.ParseQueryString(query);
		}

		// Token: 0x17000039 RID: 57
		public string this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000067C0 File Offset: 0x000049C0
		private static IEnumerable<KeyValuePair<string, string>> ParseQueryString(string query)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (string.IsNullOrWhiteSpace(query))
			{
				return list;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(query);
			FormUrlEncodedParser formUrlEncodedParser = new FormUrlEncodedParser(list, long.MaxValue);
			int num = 0;
			ParserState parserState = formUrlEncodedParser.ParseBuffer(bytes, bytes.Length, ref num, true);
			if (parserState != ParserState.Done)
			{
				throw Error.InvalidOperation(Resources.FormUrlEncodedParseError, new object[]
				{
					num
				});
			}
			return list;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006830 File Offset: 0x00004A30
		public NameValueCollection ReadAsNameValueCollection()
		{
			if (this._nameValueCollection == null)
			{
				HttpValueCollection value = HttpValueCollection.Create(this);
				Interlocked.Exchange<NameValueCollection>(ref this._nameValueCollection, value);
			}
			return this._nameValueCollection;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000685F File Offset: 0x00004A5F
		public string Get(string key)
		{
			return this.ReadAsNameValueCollection().Get(key);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000686D File Offset: 0x00004A6D
		public string[] GetValues(string key)
		{
			return this.ReadAsNameValueCollection().GetValues(key);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000687B File Offset: 0x00004A7B
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this._pairs.GetEnumerator();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006888 File Offset: 0x00004A88
		IEnumerator IEnumerable.GetEnumerator()
		{
			IEnumerable pairs = this._pairs;
			return pairs.GetEnumerator();
		}

		// Token: 0x04000067 RID: 103
		private readonly IEnumerable<KeyValuePair<string, string>> _pairs;

		// Token: 0x04000068 RID: 104
		private NameValueCollection _nameValueCollection;
	}
}
