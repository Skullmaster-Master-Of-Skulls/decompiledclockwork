using System;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x020006A7 RID: 1703
	internal class Message
	{
		// Token: 0x0600348C RID: 13452 RVA: 0x000DF06C File Offset: 0x000DE06C
		internal Message()
		{
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000DF07C File Offset: 0x000DE07C
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

		// Token: 0x0600348E RID: 13454 RVA: 0x000DF12F File Offset: 0x000DE12F
		internal Message(MailAddress from, MailAddress to) : this()
		{
			this.from = from;
			this.To.Add(to);
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000DF14A File Offset: 0x000DE14A
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x000DF15D File Offset: 0x000DE15D
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

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x000DF166 File Offset: 0x000DE166
		// (set) Token: 0x06003492 RID: 13458 RVA: 0x000DF16E File Offset: 0x000DE16E
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

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000DF185 File Offset: 0x000DE185
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000DF18D File Offset: 0x000DE18D
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

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000DF196 File Offset: 0x000DE196
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000DF19E File Offset: 0x000DE19E
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

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000DF1A7 File Offset: 0x000DE1A7
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

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x000DF1C2 File Offset: 0x000DE1C2
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

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000DF1DD File Offset: 0x000DE1DD
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

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x000DF1F8 File Offset: 0x000DE1F8
		// (set) Token: 0x0600349B RID: 13467 RVA: 0x000DF200 File Offset: 0x000DE200
		internal string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				if (value != null && MailBnfHelper.HasCROrLF(value))
				{
					throw new ArgumentException(SR.GetString("MailSubjectInvalidFormat"));
				}
				this.subject = value;
				if (this.subject != null && this.subjectEncoding == null && !MimeBasePart.IsAscii(this.subject, false))
				{
					this.subjectEncoding = Encoding.GetEncoding("utf-8");
				}
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000DF25D File Offset: 0x000DE25D
		// (set) Token: 0x0600349D RID: 13469 RVA: 0x000DF265 File Offset: 0x000DE265
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

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600349E RID: 13470 RVA: 0x000DF26E File Offset: 0x000DE26E
		internal NameValueCollection Headers
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

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000DF2A1 File Offset: 0x000DE2A1
		internal NameValueCollection EnvelopeHeaders
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

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000DF2D4 File Offset: 0x000DE2D4
		// (set) Token: 0x060034A1 RID: 13473 RVA: 0x000DF2DC File Offset: 0x000DE2DC
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

		// Token: 0x060034A2 RID: 13474 RVA: 0x000DF2F4 File Offset: 0x000DE2F4
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
			catch
			{
				result2 = new Exception(SR.GetString("net_nonClsCompliantException"));
			}
			emptySendContext.result.InvokeCallback(result2);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000DF36C File Offset: 0x000DE36C
		internal virtual IAsyncResult BeginSend(BaseWriter writer, bool sendEnvelope, AsyncCallback callback, object state)
		{
			this.PrepareHeaders(sendEnvelope);
			writer.WriteHeaders(this.Headers);
			if (this.Content != null)
			{
				return this.Content.BeginSend(writer, callback, state);
			}
			LazyAsyncResult result = new LazyAsyncResult(this, state, callback);
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.EmptySendCallback), new Message.EmptySendContext(writer, result));
			if (asyncResult.CompletedSynchronously)
			{
				writer.EndGetContentStream(asyncResult).Close();
			}
			return result;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000DF3E0 File Offset: 0x000DE3E0
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

		// Token: 0x060034A5 RID: 13477 RVA: 0x000DF484 File Offset: 0x000DE484
		internal virtual void Send(BaseWriter writer, bool sendEnvelope)
		{
			if (sendEnvelope)
			{
				this.PrepareEnvelopeHeaders(sendEnvelope);
				writer.WriteHeaders(this.EnvelopeHeaders);
			}
			this.PrepareHeaders(sendEnvelope);
			writer.WriteHeaders(this.Headers);
			if (this.Content != null)
			{
				this.Content.Send(writer);
				return;
			}
			writer.GetContentStream().Close();
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000DF4DC File Offset: 0x000DE4DC
		internal void PrepareEnvelopeHeaders(bool sendEnvelope)
		{
			this.EnvelopeHeaders[MailHeaderInfo.GetString(MailHeaderID.XSender)] = this.From.ToEncodedString();
			this.EnvelopeHeaders.Remove(MailHeaderInfo.GetString(MailHeaderID.XReceiver));
			foreach (MailAddress mailAddress in this.To)
			{
				this.EnvelopeHeaders.Add(MailHeaderInfo.GetString(MailHeaderID.XReceiver), mailAddress.ToEncodedString());
			}
			foreach (MailAddress mailAddress2 in this.CC)
			{
				this.EnvelopeHeaders.Add(MailHeaderInfo.GetString(MailHeaderID.XReceiver), mailAddress2.ToEncodedString());
			}
			foreach (MailAddress mailAddress3 in this.Bcc)
			{
				this.EnvelopeHeaders.Add(MailHeaderInfo.GetString(MailHeaderID.XReceiver), mailAddress3.ToEncodedString());
			}
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000DF610 File Offset: 0x000DE610
		internal void PrepareHeaders(bool sendEnvelope)
		{
			this.Headers[MailHeaderInfo.GetString(MailHeaderID.MimeVersion)] = "1.0";
			this.Headers[MailHeaderInfo.GetString(MailHeaderID.From)] = this.From.ToEncodedString();
			if (this.Sender != null)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Sender)] = this.Sender.ToEncodedString();
			}
			else
			{
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Sender));
			}
			if (this.To.Count > 0)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.To)] = this.To.ToEncodedString();
			}
			else
			{
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.To));
			}
			if (this.CC.Count > 0)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Cc)] = this.CC.ToEncodedString();
			}
			else
			{
				this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Cc));
			}
			if (this.replyTo != null)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ReplyTo)] = this.ReplyTo.ToEncodedString();
			}
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
			this.Headers[MailHeaderInfo.GetString(MailHeaderID.Date)] = MailBnfHelper.GetDateTimeString(DateTime.Now, null);
			if (this.subject != null && this.subject != string.Empty)
			{
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.Subject)] = MimeBasePart.EncodeHeaderValue(this.subject, this.subjectEncoding, MimeBasePart.ShouldUseBase64Encoding(this.subjectEncoding));
				return;
			}
			this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.Subject));
		}

		// Token: 0x04003065 RID: 12389
		private MailAddress from;

		// Token: 0x04003066 RID: 12390
		private MailAddress sender;

		// Token: 0x04003067 RID: 12391
		private MailAddress replyTo;

		// Token: 0x04003068 RID: 12392
		private MailAddressCollection to;

		// Token: 0x04003069 RID: 12393
		private MailAddressCollection cc;

		// Token: 0x0400306A RID: 12394
		private MailAddressCollection bcc;

		// Token: 0x0400306B RID: 12395
		private MimeBasePart content;

		// Token: 0x0400306C RID: 12396
		private HeaderCollection headers;

		// Token: 0x0400306D RID: 12397
		private HeaderCollection envelopeHeaders;

		// Token: 0x0400306E RID: 12398
		private string subject;

		// Token: 0x0400306F RID: 12399
		private Encoding subjectEncoding;

		// Token: 0x04003070 RID: 12400
		private MailPriority priority = (MailPriority)(-1);

		// Token: 0x020006A8 RID: 1704
		internal class EmptySendContext
		{
			// Token: 0x060034A8 RID: 13480 RVA: 0x000DF885 File Offset: 0x000DE885
			internal EmptySendContext(BaseWriter writer, LazyAsyncResult result)
			{
				this.writer = writer;
				this.result = result;
			}

			// Token: 0x04003071 RID: 12401
			internal LazyAsyncResult result;

			// Token: 0x04003072 RID: 12402
			internal BaseWriter writer;
		}
	}
}
