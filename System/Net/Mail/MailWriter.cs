using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x020006A1 RID: 1697
	internal class MailWriter : BaseWriter
	{
		// Token: 0x0600347C RID: 13436 RVA: 0x000DEBA0 File Offset: 0x000DDBA0
		internal MailWriter(Stream stream) : this(stream, MailWriter.DefaultLineLength)
		{
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000DEBB0 File Offset: 0x000DDBB0
		internal MailWriter(Stream stream, int lineLength)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (lineLength < 0)
			{
				throw new ArgumentOutOfRangeException("lineLength");
			}
			this.stream = stream;
			this.lineLength = lineLength;
			this.onCloseHandler = new EventHandler(this.OnClose);
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000DEC0B File Offset: 0x000DDC0B
		internal override void Close()
		{
			this.stream.Write(MailWriter.CRLF, 0, 2);
			this.stream.Close();
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000DEC2C File Offset: 0x000DDC2C
		internal IAsyncResult BeginGetContentStream(ContentTransferEncoding contentTransferEncoding, AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			Stream result = this.GetContentStream(contentTransferEncoding, multiAsyncResult);
			if (!(multiAsyncResult.Result is Exception))
			{
				multiAsyncResult.Result = result;
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000DEC66 File Offset: 0x000DDC66
		internal override IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			return this.BeginGetContentStream(ContentTransferEncoding.SevenBit, callback, state);
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000DEC74 File Offset: 0x000DDC74
		internal override Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (Stream)obj;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000DEC9D File Offset: 0x000DDC9D
		internal Stream GetContentStream(ContentTransferEncoding contentTransferEncoding)
		{
			return this.GetContentStream(contentTransferEncoding, null);
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000DECA7 File Offset: 0x000DDCA7
		internal override Stream GetContentStream()
		{
			return this.GetContentStream(ContentTransferEncoding.SevenBit);
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000DECB0 File Offset: 0x000DDCB0
		private Stream GetContentStream(ContentTransferEncoding contentTransferEncoding, MultiAsyncResult multiResult)
		{
			if (this.isInContent)
			{
				throw new InvalidOperationException(SR.GetString("MailWriterIsInContent"));
			}
			this.isInContent = true;
			this.bufferBuilder.Append(MailWriter.CRLF);
			this.Flush(multiResult);
			Stream stream = this.stream;
			if (contentTransferEncoding == ContentTransferEncoding.SevenBit)
			{
				stream = new SevenBitStream(stream, true);
			}
			else if (contentTransferEncoding == ContentTransferEncoding.QuotedPrintable)
			{
				stream = new QuotedPrintableStream(stream, this.lineLength);
			}
			else if (contentTransferEncoding == ContentTransferEncoding.Base64)
			{
				stream = new Base64Stream(stream, this.lineLength);
			}
			ClosableStream result = new ClosableStream(stream, this.onCloseHandler);
			this.contentStream = result;
			return result;
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000DED40 File Offset: 0x000DDD40
		internal override void WriteHeader(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.isInContent)
			{
				throw new InvalidOperationException(SR.GetString("MailWriterIsInContent"));
			}
			this.bufferBuilder.Append(name);
			this.bufferBuilder.Append(": ");
			this.WriteAndFold(value);
			this.bufferBuilder.Append(MailWriter.CRLF);
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000DEDB4 File Offset: 0x000DDDB4
		internal override void WriteHeaders(NameValueCollection headers)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			if (this.isInContent)
			{
				throw new InvalidOperationException(SR.GetString("MailWriterIsInContent"));
			}
			foreach (object obj in headers)
			{
				string name = (string)obj;
				string[] values = headers.GetValues(name);
				foreach (string value in values)
				{
					this.WriteHeader(name, value);
				}
			}
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000DEE58 File Offset: 0x000DDE58
		private void OnClose(object sender, EventArgs args)
		{
			this.contentStream.Flush();
			this.contentStream = null;
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000DEE6C File Offset: 0x000DDE6C
		private static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				MailWriter mailWriter = (MailWriter)multiAsyncResult.Context;
				try
				{
					mailWriter.stream.EndWrite(result);
					multiAsyncResult.Leave();
				}
				catch (Exception result2)
				{
					multiAsyncResult.Leave(result2);
				}
				catch
				{
					multiAsyncResult.Leave(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000DEEEC File Offset: 0x000DDEEC
		private void Flush(MultiAsyncResult multiResult)
		{
			if (this.bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this.stream.BeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, MailWriter.onWrite, multiResult);
					if (asyncResult.CompletedSynchronously)
					{
						this.stream.EndWrite(asyncResult);
						multiResult.Leave();
					}
				}
				else
				{
					this.stream.Write(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length);
				}
				this.bufferBuilder.Reset();
			}
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000DEF84 File Offset: 0x000DDF84
		private void WriteAndFold(string value)
		{
			if (value.Length < MailWriter.DefaultLineLength)
			{
				this.bufferBuilder.Append(value);
				return;
			}
			int num = 0;
			int length = value.Length;
			while (length - num > MailWriter.DefaultLineLength)
			{
				int num2 = value.LastIndexOf(' ', num + MailWriter.DefaultLineLength - 1, MailWriter.DefaultLineLength - 1);
				if (num2 > -1)
				{
					this.bufferBuilder.Append(value, num, num2 - num);
					this.bufferBuilder.Append(MailWriter.CRLF);
					num = num2;
				}
				else
				{
					this.bufferBuilder.Append(value, num, MailWriter.DefaultLineLength);
					num += MailWriter.DefaultLineLength;
				}
			}
			if (num < length)
			{
				this.bufferBuilder.Append(value, num, length - num);
			}
		}

		// Token: 0x0400304C RID: 12364
		private static byte[] CRLF = new byte[]
		{
			13,
			10
		};

		// Token: 0x0400304D RID: 12365
		private static int DefaultLineLength = 78;

		// Token: 0x0400304E RID: 12366
		private Stream contentStream;

		// Token: 0x0400304F RID: 12367
		private bool isInContent;

		// Token: 0x04003050 RID: 12368
		private int lineLength;

		// Token: 0x04003051 RID: 12369
		private EventHandler onCloseHandler;

		// Token: 0x04003052 RID: 12370
		private Stream stream;

		// Token: 0x04003053 RID: 12371
		private BufferBuilder bufferBuilder = new BufferBuilder();

		// Token: 0x04003054 RID: 12372
		private static AsyncCallback onWrite = new AsyncCallback(MailWriter.OnWrite);
	}
}
