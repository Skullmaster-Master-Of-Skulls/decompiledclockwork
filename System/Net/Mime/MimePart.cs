using System;
using System.IO;
using System.Net.Mail;

namespace System.Net.Mime
{
	// Token: 0x020006AE RID: 1710
	internal class MimePart : MimeBasePart, IDisposable
	{
		// Token: 0x060034CA RID: 13514 RVA: 0x000E02CC File Offset: 0x000DF2CC
		internal MimePart()
		{
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000E02D4 File Offset: 0x000DF2D4
		public void Dispose()
		{
			if (this.stream != null)
			{
				this.stream.Close();
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000E02E9 File Offset: 0x000DF2E9
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000E02F1 File Offset: 0x000DF2F1
		// (set) Token: 0x060034CE RID: 13518 RVA: 0x000E02F9 File Offset: 0x000DF2F9
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

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000E0334 File Offset: 0x000DF334
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x000E03A4 File Offset: 0x000DF3A4
		internal TransferEncoding TransferEncoding
		{
			get
			{
				if (base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)].Equals("base64", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.Base64;
				}
				if (base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)].Equals("quoted-printable", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.QuotedPrintable;
				}
				if (base.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentTransferEncoding)].Equals("7bit", StringComparison.OrdinalIgnoreCase))
				{
					return TransferEncoding.SevenBit;
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
				throw new NotSupportedException(SR.GetString("MimeTransferEncodingNotSupported", new object[]
				{
					value
				}));
			}
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000E0424 File Offset: 0x000DF424
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

		// Token: 0x060034D2 RID: 13522 RVA: 0x000E047C File Offset: 0x000DF47C
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

		// Token: 0x060034D3 RID: 13523 RVA: 0x000E04D6 File Offset: 0x000DF4D6
		internal void SetContent(Stream stream, ContentType contentType)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.contentType = contentType;
			this.SetContent(stream);
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000E04F4 File Offset: 0x000DF4F4
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
			catch
			{
				if (e == null)
				{
					e = new Exception(SR.GetString("net_nonClsCompliantException"));
				}
			}
			mimePartContext.completed = true;
			mimePartContext.result.InvokeCallback(e);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000E057C File Offset: 0x000DF57C
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
			catch
			{
				this.Complete(result, new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000E05EC File Offset: 0x000DF5EC
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

		// Token: 0x060034D7 RID: 13527 RVA: 0x000E0658 File Offset: 0x000DF658
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
			catch
			{
				this.Complete(result, new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000E06C8 File Offset: 0x000DF6C8
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

		// Token: 0x060034D9 RID: 13529 RVA: 0x000E0720 File Offset: 0x000DF720
		internal Stream GetEncodedStream(Stream stream)
		{
			Stream result = stream;
			if (this.TransferEncoding == TransferEncoding.Base64)
			{
				result = new Base64Stream(result);
			}
			else if (this.TransferEncoding == TransferEncoding.QuotedPrintable)
			{
				result = new QuotedPrintableStream(result, true);
			}
			else if (this.TransferEncoding == TransferEncoding.SevenBit)
			{
				result = new SevenBitStream(result, false);
			}
			return result;
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000E0768 File Offset: 0x000DF768
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

		// Token: 0x060034DB RID: 13531 RVA: 0x000E07F0 File Offset: 0x000DF7F0
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
			catch
			{
				this.Complete(result, new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000E0860 File Offset: 0x000DF860
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, object state)
		{
			writer.WriteHeaders(base.Headers);
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

		// Token: 0x060034DD RID: 13533 RVA: 0x000E08BC File Offset: 0x000DF8BC
		internal override void Send(BaseWriter writer)
		{
			if (this.Stream != null)
			{
				byte[] buffer = new byte[17408];
				writer.WriteHeaders(base.Headers);
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

		// Token: 0x060034DE RID: 13534 RVA: 0x000E092C File Offset: 0x000DF92C
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

		// Token: 0x04003086 RID: 12422
		private const int maxBufferSize = 17408;

		// Token: 0x04003087 RID: 12423
		private Stream stream;

		// Token: 0x04003088 RID: 12424
		private bool streamSet;

		// Token: 0x04003089 RID: 12425
		private bool streamUsedOnce;

		// Token: 0x0400308A RID: 12426
		private AsyncCallback readCallback;

		// Token: 0x0400308B RID: 12427
		private AsyncCallback writeCallback;

		// Token: 0x020006AF RID: 1711
		internal class MimePartContext
		{
			// Token: 0x060034DF RID: 13535 RVA: 0x000E096A File Offset: 0x000DF96A
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result)
			{
				this.writer = writer;
				this.result = result;
				this.buffer = new byte[17408];
			}

			// Token: 0x0400308C RID: 12428
			internal Stream outputStream;

			// Token: 0x0400308D RID: 12429
			internal LazyAsyncResult result;

			// Token: 0x0400308E RID: 12430
			internal int bytesLeft;

			// Token: 0x0400308F RID: 12431
			internal BaseWriter writer;

			// Token: 0x04003090 RID: 12432
			internal byte[] buffer;

			// Token: 0x04003091 RID: 12433
			internal bool completed;

			// Token: 0x04003092 RID: 12434
			internal bool completedSynchronously = true;
		}
	}
}
