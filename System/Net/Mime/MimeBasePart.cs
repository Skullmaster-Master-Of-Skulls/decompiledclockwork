using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020006A9 RID: 1705
	internal class MimeBasePart
	{
		// Token: 0x060034A9 RID: 13481 RVA: 0x000DF89B File Offset: 0x000DE89B
		internal MimeBasePart()
		{
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000DF8A3 File Offset: 0x000DE8A3
		internal static bool ShouldUseBase64Encoding(Encoding encoding)
		{
			return encoding == Encoding.Unicode || encoding == Encoding.UTF8 || encoding == Encoding.UTF32 || encoding == Encoding.BigEndianUnicode;
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000DF8C8 File Offset: 0x000DE8C8
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (encoding == null && MimeBasePart.IsAscii(value, false))
			{
				return value;
			}
			if (encoding == null)
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			string value2 = encoding.BodyName;
			if (encoding == Encoding.BigEndianUnicode)
			{
				value2 = "utf-16be";
			}
			stringBuilder.Append("=?");
			stringBuilder.Append(value2);
			stringBuilder.Append("?");
			stringBuilder.Append(base64Encoding ? "B" : "Q");
			stringBuilder.Append("?");
			byte[] bytes = encoding.GetBytes(value);
			if (base64Encoding)
			{
				Base64Stream base64Stream = new Base64Stream(-1);
				base64Stream.EncodeBytes(bytes, 0, bytes.Length, true);
				stringBuilder.Append(Encoding.ASCII.GetString(base64Stream.WriteState.Buffer, 0, base64Stream.WriteState.Length));
			}
			else
			{
				QuotedPrintableStream quotedPrintableStream = new QuotedPrintableStream(-1);
				quotedPrintableStream.EncodeBytes(bytes, 0, bytes.Length);
				stringBuilder.Append(Encoding.ASCII.GetString(quotedPrintableStream.WriteState.Buffer, 0, quotedPrintableStream.WriteState.Length));
			}
			stringBuilder.Append("?=");
			return stringBuilder.ToString();
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000DF9EC File Offset: 0x000DE9EC
		internal static string DecodeHeaderValue(string value)
		{
			if (value == null || value.Length == 0)
			{
				return string.Empty;
			}
			string[] array = value.Split(new char[]
			{
				'?'
			});
			if (array.Length != 5 || array[0] != "=" || array[4] != "=")
			{
				return value;
			}
			string name = array[1];
			bool flag = array[2] == "B";
			byte[] bytes = Encoding.ASCII.GetBytes(array[3]);
			int count;
			if (flag)
			{
				Base64Stream base64Stream = new Base64Stream();
				count = base64Stream.DecodeBytes(bytes, 0, bytes.Length);
			}
			else
			{
				QuotedPrintableStream quotedPrintableStream = new QuotedPrintableStream();
				count = quotedPrintableStream.DecodeBytes(bytes, 0, bytes.Length);
			}
			Encoding encoding = Encoding.GetEncoding(name);
			return encoding.GetString(bytes, 0, count);
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000DFAB0 File Offset: 0x000DEAB0
		internal static Encoding DecodeEncoding(string value)
		{
			if (value == null || value.Length == 0)
			{
				return null;
			}
			string[] array = value.Split(new char[]
			{
				'?'
			});
			if (array.Length != 5 || array[0] != "=" || array[4] != "=")
			{
				return null;
			}
			string name = array[1];
			return Encoding.GetEncoding(name);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000DFB10 File Offset: 0x000DEB10
		internal static bool IsAscii(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool result;
				if (c > '\u007f')
				{
					result = false;
				}
				else
				{
					if (permitCROrLF || (c != '\r' && c != '\n'))
					{
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000DFB64 File Offset: 0x000DEB64
		internal static bool IsAnsi(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				bool result;
				if (c > 'ÿ')
				{
					result = false;
				}
				else
				{
					if (permitCROrLF || (c != '\r' && c != '\n'))
					{
						i++;
						continue;
					}
					result = false;
				}
				return result;
			}
			return true;
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x000DFBBA File Offset: 0x000DEBBA
		// (set) Token: 0x060034B1 RID: 13489 RVA: 0x000DFBCD File Offset: 0x000DEBCD
		internal string ContentID
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentID));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)] = value;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x000DFBFB File Offset: 0x000DEBFB
		// (set) Token: 0x060034B3 RID: 13491 RVA: 0x000DFC0E File Offset: 0x000DEC0E
		internal string ContentLocation
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentLocation));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)] = value;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x000DFC3C File Offset: 0x000DEC3C
		internal NameValueCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HeaderCollection();
				}
				if (this.contentType == null)
				{
					this.contentType = new ContentType();
				}
				this.contentType.PersistIfNeeded(this.headers, false);
				if (this.contentDisposition != null)
				{
					this.contentDisposition.PersistIfNeeded(this.headers, false);
				}
				return this.headers;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x000DFCA1 File Offset: 0x000DECA1
		// (set) Token: 0x060034B6 RID: 13494 RVA: 0x000DFCBC File Offset: 0x000DECBC
		internal ContentType ContentType
		{
			get
			{
				if (this.contentType == null)
				{
					this.contentType = new ContentType();
				}
				return this.contentType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.contentType = value;
				this.contentType.PersistIfNeeded((HeaderCollection)this.Headers, true);
			}
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000DFCEA File Offset: 0x000DECEA
		internal virtual void Send(BaseWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000DFCF1 File Offset: 0x000DECF1
		internal virtual IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000DFCF8 File Offset: 0x000DECF8
		internal void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as MimeBasePart.MimePartAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"), "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndSend"
				}));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			if (lazyAsyncResult.Result is Exception)
			{
				throw (Exception)lazyAsyncResult.Result;
			}
		}

		// Token: 0x04003073 RID: 12403
		internal const string defaultCharSet = "utf-8";

		// Token: 0x04003074 RID: 12404
		protected ContentType contentType;

		// Token: 0x04003075 RID: 12405
		protected ContentDisposition contentDisposition;

		// Token: 0x04003076 RID: 12406
		private HeaderCollection headers;

		// Token: 0x020006AA RID: 1706
		internal class MimePartAsyncResult : LazyAsyncResult
		{
			// Token: 0x060034BA RID: 13498 RVA: 0x000DFD8A File Offset: 0x000DED8A
			internal MimePartAsyncResult(MimeBasePart part, object state, AsyncCallback callback) : base(part, state, callback)
			{
			}
		}
	}
}
