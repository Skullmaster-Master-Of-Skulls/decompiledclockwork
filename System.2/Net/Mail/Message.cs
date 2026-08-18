using System;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000273 RID: 627
	internal class Message
	{
		// Token: 0x06001793 RID: 6035 RVA: 0x00078298 File Offset: 0x00076498
		internal Message()
		{
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x000782A8 File Offset: 0x000764A8
		internal Message(string from, string to) : this()
		{
			if (from == null)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to");
			}
			if (from == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"from"
				}), "from");
			}
			if (to == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"to"
				}), "to");
			}
			this.from = new MailAddress(from);
			this.to = new MailAddressCollection
			{
				to
			};
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00078357 File Offset: 0x00076557
		internal Message(MailAddress from, MailAddress to) : this()
		{
			this.from = from;
			this.To.Add(to);
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00078372 File Offset: 0x00076572
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x00078385 File Offset: 0x00076585
		public MailPriority Priority
		{
			get
			{
				if (this.priority != (MailPriority)(-1))
				{
					return this.priority;
				}
				return MailPriority.Normal;
			}
			set
			{
				this.priority = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0007838E File Offset: 0x0007658E
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x00078396 File Offset: 0x00076596
		internal MailAddress From
		{
			get
			{
				return this.from;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.from = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x000783AD File Offset: 0x000765AD
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x000783B5 File Offset: 0x000765B5
		internal MailAddress Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x000783BE File Offset: 0x000765BE
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x000783C6 File Offset: 0x000765C6
		internal MailAddress ReplyTo
		{
			get
			{
				return this.replyTo;
			}
			set
			{
				this.replyTo = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x000783CF File Offset: 0x000765CF
		internal MailAddressCollection ReplyToList
		{
			get
			{
				if (this.replyToList == null)
				{
					this.replyToList = new MailAddressCollection();
				}
				return this.replyToList;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x000783EA File Offset: 0x000765EA
		internal MailAddressCollection To
		{
			get
			{
				if (this.to == null)
				{
					this.to = new MailAddressCollection();
				}
				return this.to;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00078405 File Offset: 0x00076605
		internal MailAddressCollection Bcc
		{
			get
			{
				if (this.bcc == null)
				{
					this.bcc = new MailAddressCollection();
				}
				return this.bcc;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00078420 File Offset: 0x00076620
		internal MailAddressCollection CC
		{
			get
			{
				if (this.cc == null)
				{
					this.cc = new MailAddressCollection();
				}
				return this.cc;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0007843B File Offset: 0x0007663B
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00078444 File Offset: 0x00076644
		internal string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				Encoding encoding = null;
				try
				{
					encoding = MimeBasePart.DecodeEncoding(value);
				}
				catch (ArgumentException)
				{
				}
				if (encoding != null && value != null)
				{
					try
					{
						value = MimeBasePart.DecodeHeaderValue(value);
						this.subjectEncoding = (this.subjectEncoding ?? encoding);
					}
					catch (FormatException)
					{
					}
				}
				if (value != null && MailBnfHelper.HasCROrLF(value))
				{
					throw new ArgumentException(SR.GetString("MailSubjectInvalidFormat"));
				}
				this.subject = value;
				if (this.subject != null)
				{
					this.subject = this.subject.Normalize(NormalizationForm.FormC);
					if (this.subjectEncoding == null && !MimeBasePart.IsAscii(this.subject, false))
					{
						this.subjectEncoding = Encoding.GetEncoding("utf-8");
					}
				}
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00078504 File Offset: 0x00076704
		// (set) Token: 0x060017A5 RID: 6053 RVA: 0x0007850C File Offset: 0x0007670C
		internal Encoding SubjectEncoding
		{
			get
			{
				return this.subjectEncoding;
			}
			set
			{
				this.subjectEncoding = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x00078515 File Offset: 0x00076715
		internal HeaderCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HeaderCollection();
					if (Logging.On)
					{
						Logging.Associate(Logging.Web, this, this.headers);
					}
				}
				return this.headers;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00078548 File Offset: 0x00076748
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x00078550 File Offset: 0x00076750
		internal Encoding HeadersEncoding
		{
			get
			{
				return this.headersEncoding;
			}
			set
			{
				this.headersEncoding = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00078559 File Offset: 0x00076759
		internal HeaderCollection EnvelopeHeaders
		{
			get
			{
				if (this.envelopeHeaders == null)
				{
					this.envelopeHeaders = new HeaderCollection();
					if (Logging.On)
					{
						Logging.Associate(Logging.Web, this, this.envelopeHeaders);
					}
				}
				return this.envelopeHeaders;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0007858C File Offset: 0x0007678C
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x00078594 File Offset: 0x00076794
		internal virtual MimeBasePart Content
		{
			get
			{
				return this.content;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.content = value;
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000785AC File Offset: 0x000767AC
		internal void EmptySendCallback(IAsyncResult result)
		{
			Exception result2 = null;
			if (result.CompletedSynchronously)
			{
				return;
			}
			Message.EmptySendContext emptySendContext = (Message.EmptySendContext)result.AsyncState;
			try
			{
				emptySendContext.writer.EndGetContentStream(result).Close();
			}
			catch (Exception ex)
			{
				result2 = ex;
			}
			emptySendContext.result.InvokeCallback(result2);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00078604 File Offset: 0x00076804
		internal virtual IAsyncResult BeginSend(BaseWriter writer, bool sendEnvelope, bool allowUnicode, AsyncCallback callback, object state)
		{
			this.PrepareHeaders(sendEnvelope, allowUnicode);
			writer.WriteHeaders(this.Headers, allowUnicode);
			if (this.Content != null)
			{
				return this.Content.BeginSend(writer, callback, allowUnicode, state);
			}
			LazyAsyncResult result = new LazyAsyncResult(this, state, callback);
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.EmptySendCallback), new Message.EmptySendContext(writer, result));
			if (asyncResult.CompletedSynchronously)
			{
				writer.EndGetContentStream(asyncResult).Close();
			}
			return result;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0007867C File Offset: 0x0007687C
		internal virtual void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this.Content != null)
			{
				this.Content.EndSend(asyncResult);
				return;
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"));
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

		// Token: 0x060017AF RID: 6063 RVA: 0x0007871C File Offset: 0x0007691C
		internal virtual void Send(BaseWriter writer, bool sendEnvelope, bool allowUnicode)
		{
			if (sendEnvelope)
			{
				this.PrepareEnvelopeHeaders(sendEnvelope, allowUnicode);
				writer.WriteHeaders(this.EnvelopeHeaders, allowUnicode);
			}
			this.PrepareHeaders(sendEnvelope, allowUnicode);
			writer.WriteHeaders(this.Headers, allowUnicode);
			if (this.Content != null)
			{
				this.Content.Send(writer, allowUnicode);
				return;
			}
			writer.GetContentStream().Close();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00078778 File Offset: 0x00076978
		internal void PrepareEnvelopeHeaders(bool sendEnvelope, bool allowUnicode)
		{
			if (this.headersEncoding == null)
			{
				this.headersEncoding = Encoding.GetEncoding("utf-8");
			}
			this.EncodeHeaders(this.EnvelopeHeaders, allowUnicode);
			string @string = MailHeaderInfo.GetString(MailHeaderID.XSender);
			if (!this.IsHeaderSet(@string))
			{
				MailAddress mailAddress = this.Sender ?? this.From;
				this.EnvelopeHeaders.InternalSet(@string, mailAddress.Encode(@string.Length, allowUnicode));
			}
			string string2 = MailHeaderInfo.GetString(MailHeaderID.XReceiver);
			this.EnvelopeHeaders.Remove(string2);
			foreach (MailAddress mailAddress2 in this.To)
			{
				this.EnvelopeHeaders.InternalAdd(string2, mailAddress2.Encode(string2.Length, allowUnicode));
			}
			foreach (MailAddress mailAddress3 in this.CC)
			{
				this.EnvelopeHeaders.InternalAdd(string2, mailAddress3.Encode(string2.Length, allowUnicode));
			}
			foreach (MailAddress mailAddress4 in this.Bcc)
			{
				this.EnvelopeHeaders.InternalAdd(string2, mailAddress4.Encode(string2.Length, allowUnicode));
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000788F8 File Offset: 0x00076AF8
		internal void PrepareHeaders(bool sendEnvelope, bool allowUnicode)
		{
			if (this.headersEncoding == null)
			{
				this.headersEncoding = Encoding.GetEncoding("utf-8");
			}
			this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentType));
			this.Headers[MailHeaderInfo.GetString(MailHeaderID.MimeVersion)] = "1.0";
			string @string = MailHeaderInfo.GetString(MailHeaderID.Sender);
			if (this.Sender != null)
			{
				this.Headers.InternalAdd(@string, this.Sender.Encode(@string.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(@string);
			}
			@string = MailHeaderInfo.GetString(MailHeaderID.From);
			this.Headers.InternalAdd(@string, this.From.Encode(@string.Length, allowUnicode));
			@string = MailHeaderInfo.GetString(MailHeaderID.To);
			if (this.To.Count > 0)
			{
				this.Headers.InternalAdd(@string, this.To.Encode(@string.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(@string);
			}
			@string = MailHeaderInfo.GetString(MailHeaderID.Cc);
			if (this.CC.Count > 0)
			{
				this.Headers.InternalAdd(@string, this.CC.Encode(@string.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(@string);
			}
			@string = MailHeaderInfo.GetString(MailHeaderID.ReplyTo);
			if (this.ReplyTo != null)
			{
				this.Headers.InternalAdd(@string, this.ReplyTo.Encode(@string.Length, allowUnicode));
			}
			else if (this.ReplyToList.Count > 0)
			{
				this.Headers.InternalAdd(@string, this.ReplyToList.Encode(@string.Length, allowUnicode));
			}
			else
			{
				this.Headers.Remove(@string);
			}
			this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Bcc));
			if (this.priority == MailPriority.High)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.XPriority)] = "1";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Priority)] = "urgent";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Importance)] = "high";
			}
			else if (this.priority == MailPriority.Low)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.XPriority)] = "5";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Priority)] = "non-urgent";
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Importance)] = "low";
			}
			else if (this.priority != (MailPriority)(-1))
			{
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.XPriority));
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Priority));
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Importance));
			}
			this.Headers.InternalAdd(MailHeaderInfo.GetString(MailHeaderID.Date), MailBnfHelper.GetDateTimeString(DateTime.Now, null));
			@string = MailHeaderInfo.GetString(MailHeaderID.Subject);
			if (!string.IsNullOrEmpty(this.subject))
			{
				if (allowUnicode)
				{
					this.Headers.InternalAdd(@string, this.subject);
				}
				else
				{
					this.Headers.InternalAdd(@string, MimeBasePart.EncodeHeaderValue(this.subject, this.subjectEncoding, MimeBasePart.ShouldUseBase64Encoding(this.subjectEncoding), @string.Length));
				}
			}
			else
			{
				this.Headers.Remove(@string);
			}
			this.EncodeHeaders(this.headers, allowUnicode);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00078C1C File Offset: 0x00076E1C
		internal void EncodeHeaders(HeaderCollection headers, bool allowUnicode)
		{
			if (this.headersEncoding == null)
			{
				this.headersEncoding = Encoding.GetEncoding("utf-8");
			}
			for (int i = 0; i < headers.Count; i++)
			{
				string key = headers.GetKey(i);
				if (MailHeaderInfo.IsUserSettable(key))
				{
					string[] values = headers.GetValues(key);
					string value = string.Empty;
					for (int j = 0; j < values.Length; j++)
					{
						if (MimeBasePart.IsAscii(values[j], false) || (allowUnicode && MailHeaderInfo.AllowsUnicode(key) && !MailBnfHelper.HasCROrLF(values[j])))
						{
							value = values[j];
						}
						else
						{
							value = MimeBasePart.EncodeHeaderValue(values[j], this.headersEncoding, MimeBasePart.ShouldUseBase64Encoding(this.headersEncoding), key.Length);
						}
						if (j == 0)
						{
							headers.Set(key, value);
						}
						else
						{
							headers.Add(key, value);
						}
					}
				}
			}
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00078CEC File Offset: 0x00076EEC
		private bool IsHeaderSet(string headerName)
		{
			for (int i = 0; i < this.Headers.Count; i++)
			{
				if (string.Compare(this.Headers.GetKey(i), headerName, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040017EE RID: 6126
		private MailAddress from;

		// Token: 0x040017EF RID: 6127
		private MailAddress sender;

		// Token: 0x040017F0 RID: 6128
		private MailAddressCollection replyToList;

		// Token: 0x040017F1 RID: 6129
		private MailAddress replyTo;

		// Token: 0x040017F2 RID: 6130
		private MailAddressCollection to;

		// Token: 0x040017F3 RID: 6131
		private MailAddressCollection cc;

		// Token: 0x040017F4 RID: 6132
		private MailAddressCollection bcc;

		// Token: 0x040017F5 RID: 6133
		private MimeBasePart content;

		// Token: 0x040017F6 RID: 6134
		private HeaderCollection headers;

		// Token: 0x040017F7 RID: 6135
		private HeaderCollection envelopeHeaders;

		// Token: 0x040017F8 RID: 6136
		private string subject;

		// Token: 0x040017F9 RID: 6137
		private Encoding subjectEncoding;

		// Token: 0x040017FA RID: 6138
		private Encoding headersEncoding;

		// Token: 0x040017FB RID: 6139
		private MailPriority priority = (MailPriority)(-1);

		// Token: 0x0200079E RID: 1950
		internal class EmptySendContext
		{
			// Token: 0x060042F9 RID: 17145 RVA: 0x001185BF File Offset: 0x001167BF
			internal EmptySendContext(BaseWriter writer, LazyAsyncResult result)
			{
				this.writer = writer;
				this.result = result;
			}

			// Token: 0x040033B2 RID: 13234
			internal LazyAsyncResult result;

			// Token: 0x040033B3 RID: 13235
			internal BaseWriter writer;
		}
	}
}
