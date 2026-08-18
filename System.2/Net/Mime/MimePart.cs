using System;
using System.IO;
using System.Net.Mail;

namespace System.Net.Mime
{
	// Token: 0x0200024C RID: 588
	internal class MimePart : MimeBasePart, IDisposable
	{
		// Token: 0x0600164D RID: 5709 RVA: 0x0007395A File Offset: 0x00071B5A
		internal MimePart()
		{
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00073962 File Offset: 0x00071B62
		public void Dispose()
		{
			if (this.stream != null)
			{
				this.stream.Close();
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00073977 File Offset: 0x00071B77
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0007397F File Offset: 0x00071B7F
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x00073987 File Offset: 0x00071B87
		internal ContentDisposition ContentDisposition
		{
			get
			{
				return this.contentDisposition;
			}
			set
			{
				this.contentDisposition = value;
				if (value == null)
				{
					((HeaderCollection)base.Headers).InternalRemove(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition));
					return;
				}
				this.contentDisposition.PersistIfNeeded((HeaderCollection)base.Headers, true);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x000739C4 File Offset: 0x00071BC4
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x00073A24 File Offset: 0x00071C24
		internal TransferEncoding TransferEncoding
		{
			get
			{
				string text = base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)];
				if (text.Equals("base64", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.Base64;
				}
				if (text.Equals("quoted-printable", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.QuotedPrintable;
				}
				if (text.Equals("7bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.SevenBit;
				}
				if (text.Equals("8bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.EightBit;
				}
				return TransferEncoding.Unknown;
			}
			set
			{
				if (value == TransferEncoding.Base64)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "base64";
					return;
				}
				if (value == TransferEncoding.QuotedPrintable)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "quoted-printable";
					return;
				}
				if (value == TransferEncoding.SevenBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "7bit";
					return;
				}
				if (value == TransferEncoding.EightBit)
				{
					base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)] = "8bit";
					return;
				}
				throw new NotSupportedException(SR.GetString("MimeTransferEncodingNotSupported", new object[]
				{
					value
				}));
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00073ABC File Offset: 0x00071CBC
		internal void SetContent(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (this.streamSet)
			{
				this.stream.Close();
				this.stream = null;
				this.streamSet = false;
			}
			this.stream = stream;
			this.streamSet = true;
			this.streamUsedOnce = false;
			this.TransferEncoding = TransferEncoding.Base64;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00073B14 File Offset: 0x00071D14
		internal void SetContent(Stream stream, string name, string mimeType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (mimeType != null && mimeType != string.Empty)
			{
				this.contentType = new ContentType(mimeType);
			}
			if (name != null && name != string.Empty)
			{
				base.ContentType.Name = name;
			}
			this.SetContent(stream);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00073B6E File Offset: 0x00071D6E
		internal void SetContent(Stream stream, ContentType contentType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.contentType = contentType;
			this.SetContent(stream);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00073B8C File Offset: 0x00071D8C
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			if (mimePartContext.completed)
			{
				throw e;
			}
			try
			{
				if (mimePartContext.outputStream != null)
				{
					mimePartContext.outputStream.Close();
				}
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext.completed = true;
			mimePartContext.result.InvokeCallback(e);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00073BF4 File Offset: 0x00071DF4
		internal void ReadCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.ReadCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00073C40 File Offset: 0x00071E40
		internal void ReadCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext.bytesLeft = this.Stream.EndRead(result);
			if (mimePartContext.bytesLeft > 0)
			{
				IAsyncResult asyncResult = mimePartContext.outputStream.BeginWrite(mimePartContext.buffer, 0, mimePartContext.bytesLeft, this.writeCallback, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.WriteCallbackHandler(asyncResult);
					return;
				}
			}
			else
			{
				this.Complete(result, null);
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00073CAC File Offset: 0x00071EAC
		internal void WriteCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.WriteCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00073CF8 File Offset: 0x00071EF8
		internal void WriteCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			mimePartContext.outputStream.EndWrite(result);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext.buffer, 0, mimePartContext.buffer.Length, this.readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00073D50 File Offset: 0x00071F50
		internal Stream GetEncodedStream(Stream stream)
		{
			Stream result = stream;
			if (this.TransferEncoding == TransferEncoding.Base64)
			{
				result = new Base64Stream(result, new Base64WriteStateInfo());
			}
			else if (this.TransferEncoding == TransferEncoding.QuotedPrintable)
			{
				result = new QuotedPrintableStream(result, true);
			}
			else if (this.TransferEncoding == TransferEncoding.SevenBit || this.TransferEncoding == TransferEncoding.EightBit)
			{
				result = new EightBitStream(result);
			}
			return result;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00073DA4 File Offset: 0x00071FA4
		internal void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimePart.MimePartContext mimePartContext = (MimePart.MimePartContext)result.AsyncState;
			Stream stream = mimePartContext.writer.EndGetContentStream(result);
			mimePartContext.outputStream = this.GetEncodedStream(stream);
			this.readCallback = new AsyncCallback(this.ReadCallback);
			this.writeCallback = new AsyncCallback(this.WriteCallback);
			IAsyncResult asyncResult = this.Stream.BeginRead(mimePartContext.buffer, 0, mimePartContext.buffer.Length, this.readCallback, mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadCallbackHandler(asyncResult);
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00073E2C File Offset: 0x0007202C
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimePart.MimePartContext)result.AsyncState).completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception e)
			{
				this.Complete(result, e);
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00073E78 File Offset: 0x00072078
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult result = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimePart.MimePartContext state2 = new MimePart.MimePartContext(writer, result);
			this.ResetStream();
			this.streamUsedOnce = true;
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), state2);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return result;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00073EDC File Offset: 0x000720DC
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			if (this.Stream != null)
			{
				byte[] buffer = new byte[17408];
				base.PrepareHeaders(allowUnicode);
				writer.WriteHeaders(base.Headers, allowUnicode);
				Stream stream = writer.GetContentStream();
				stream = this.GetEncodedStream(stream);
				this.ResetStream();
				this.streamUsedOnce = true;
				int count;
				while ((count = this.Stream.Read(buffer, 0, 17408)) > 0)
				{
					stream.Write(buffer, 0, count);
				}
				stream.Close();
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00073F54 File Offset: 0x00072154
		internal void ResetStream()
		{
			if (!this.streamUsedOnce)
			{
				return;
			}
			if (this.Stream.CanSeek)
			{
				this.Stream.Seek(0L, SeekOrigin.Begin);
				this.streamUsedOnce = false;
				return;
			}
			throw new InvalidOperationException(SR.GetString("MimePartCantResetStream"));
		}

		// Token: 0x04001739 RID: 5945
		private Stream stream;

		// Token: 0x0400173A RID: 5946
		private bool streamSet;

		// Token: 0x0400173B RID: 5947
		private bool streamUsedOnce;

		// Token: 0x0400173C RID: 5948
		private AsyncCallback readCallback;

		// Token: 0x0400173D RID: 5949
		private AsyncCallback writeCallback;

		// Token: 0x0400173E RID: 5950
		private const int maxBufferSize = 17408;

		// Token: 0x02000798 RID: 1944
		internal class MimePartContext
		{
			// Token: 0x060042E1 RID: 17121 RVA: 0x0011822B File Offset: 0x0011642B
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result)
			{
				this.writer = writer;
				this.result = result;
				this.buffer = new byte[17408];
			}

			// Token: 0x04003396 RID: 13206
			internal Stream outputStream;

			// Token: 0x04003397 RID: 13207
			internal LazyAsyncResult result;

			// Token: 0x04003398 RID: 13208
			internal int bytesLeft;

			// Token: 0x04003399 RID: 13209
			internal BaseWriter writer;

			// Token: 0x0400339A RID: 13210
			internal byte[] buffer;

			// Token: 0x0400339B RID: 13211
			internal bool completed;

			// Token: 0x0400339C RID: 13212
			internal bool completedSynchronously = true;
		}
	}
}
