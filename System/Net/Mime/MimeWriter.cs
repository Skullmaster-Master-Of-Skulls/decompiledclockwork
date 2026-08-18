using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020006B0 RID: 1712
	internal class MimeWriter : BaseWriter
	{
		// Token: 0x060034E0 RID: 13536 RVA: 0x000E0997 File Offset: 0x000DF997
		internal MimeWriter(Stream stream, string boundary) : this(stream, boundary, null, MimeWriter.DefaultLineLength)
		{
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000E09A8 File Offset: 0x000DF9A8
		internal MimeWriter(Stream stream, string boundary, string preface, int lineLength)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			if (lineLength < 40)
			{
				throw new ArgumentOutOfRangeException("lineLength", lineLength, SR.GetString("MailWriterLineLengthTooSmall"));
			}
			this.stream = stream;
			this.lineLength = lineLength;
			this.onCloseHandler = new EventHandler(this.OnClose);
			this.boundaryBytes = Encoding.ASCII.GetBytes(boundary);
			this.preface = preface;
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000E0A44 File Offset: 0x000DFA44
		internal IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			this.Close(multiAsyncResult);
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000E0A68 File Offset: 0x000DFA68
		internal void EndClose(IAsyncResult result)
		{
			MultiAsyncResult.End(result);
			this.stream.Close();
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000E0A7C File Offset: 0x000DFA7C
		internal override void Close()
		{
			this.Close(null);
			this.stream.Close();
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000E0A90 File Offset: 0x000DFA90
		private void Close(MultiAsyncResult multiResult)
		{
			this.bufferBuilder.Append(MimeWriter.CRLF);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(this.boundaryBytes);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(MimeWriter.CRLF);
			this.Flush(multiResult);
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x000E0AF8 File Offset: 0x000DFAF8
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

		// Token: 0x060034E7 RID: 13543 RVA: 0x000E0B32 File Offset: 0x000DFB32
		internal override IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			return this.BeginGetContentStream(ContentTransferEncoding.SevenBit, callback, state);
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000E0B40 File Offset: 0x000DFB40
		internal override Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (Stream)obj;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000E0B69 File Offset: 0x000DFB69
		internal Stream GetContentStream(ContentTransferEncoding contentTransferEncoding)
		{
			if (this.isInContent)
			{
				throw new InvalidOperationException(SR.GetString("MailWriterIsInContent"));
			}
			this.isInContent = true;
			return this.GetContentStream(contentTransferEncoding, null);
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000E0B92 File Offset: 0x000DFB92
		internal override Stream GetContentStream()
		{
			return this.GetContentStream(ContentTransferEncoding.SevenBit);
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000E0B9C File Offset: 0x000DFB9C
		private Stream GetContentStream(ContentTransferEncoding contentTransferEncoding, MultiAsyncResult multiResult)
		{
			this.CheckBoundary();
			this.bufferBuilder.Append(MimeWriter.CRLF);
			this.Flush(multiResult);
			Stream stream = this.stream;
			if (contentTransferEncoding == ContentTransferEncoding.SevenBit)
			{
				stream = new SevenBitStream(stream, false);
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

		// Token: 0x060034EC RID: 13548 RVA: 0x000E0C14 File Offset: 0x000DFC14
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
			this.CheckBoundary();
			this.bufferBuilder.Append(name);
			this.bufferBuilder.Append(": ");
			this.WriteAndFold(value, name.Length + 2);
			this.bufferBuilder.Append(MimeWriter.CRLF);
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000E0C98 File Offset: 0x000DFC98
		internal override void WriteHeaders(NameValueCollection headers)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string name = (string)obj;
				this.WriteHeader(name, headers[name]);
			}
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000E0D04 File Offset: 0x000DFD04
		private void OnClose(object sender, EventArgs args)
		{
			if (this.contentStream != sender)
			{
				return;
			}
			this.contentStream.Flush();
			this.contentStream = null;
			this.writeBoundary = true;
			this.isInContent = false;
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000E0D30 File Offset: 0x000DFD30
		private void CheckBoundary()
		{
			if (this.preface != null)
			{
				this.bufferBuilder.Append(this.preface);
				this.preface = null;
			}
			if (this.writeBoundary)
			{
				this.bufferBuilder.Append(MimeWriter.CRLF);
				this.bufferBuilder.Append(MimeWriter.DASHDASH);
				this.bufferBuilder.Append(this.boundaryBytes);
				this.bufferBuilder.Append(MimeWriter.CRLF);
				this.writeBoundary = false;
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000E0DB0 File Offset: 0x000DFDB0
		private static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				MimeWriter mimeWriter = (MimeWriter)multiAsyncResult.Context;
				try
				{
					mimeWriter.stream.EndWrite(result);
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

		// Token: 0x060034F1 RID: 13553 RVA: 0x000E0E30 File Offset: 0x000DFE30
		private void Flush(MultiAsyncResult multiResult)
		{
			if (this.bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this.stream.BeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, MimeWriter.onWrite, multiResult);
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

		// Token: 0x060034F2 RID: 13554 RVA: 0x000E0EC8 File Offset: 0x000DFEC8
		private void WriteAndFold(string value, int startLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (num != value.Length)
			{
				if (value[num] == ' ' || value[num] == '\t')
				{
					if (num - num3 >= this.lineLength - startLength)
					{
						startLength = 0;
						if (num2 == num3)
						{
							num2 = num;
						}
						this.bufferBuilder.Append(value, num3, num2 - num3);
						this.bufferBuilder.Append(MimeWriter.CRLF);
						num3 = num2;
					}
					num2 = num;
				}
				num++;
			}
			if (num - num3 > 0)
			{
				this.bufferBuilder.Append(value, num3, num - num3);
				return;
			}
		}

		// Token: 0x04003093 RID: 12435
		private static int DefaultLineLength = 78;

		// Token: 0x04003094 RID: 12436
		private static byte[] DASHDASH = new byte[]
		{
			45,
			45
		};

		// Token: 0x04003095 RID: 12437
		private static byte[] CRLF = new byte[]
		{
			13,
			10
		};

		// Token: 0x04003096 RID: 12438
		private byte[] boundaryBytes;

		// Token: 0x04003097 RID: 12439
		private BufferBuilder bufferBuilder = new BufferBuilder();

		// Token: 0x04003098 RID: 12440
		private Stream contentStream;

		// Token: 0x04003099 RID: 12441
		private bool isInContent;

		// Token: 0x0400309A RID: 12442
		private int lineLength;

		// Token: 0x0400309B RID: 12443
		private EventHandler onCloseHandler;

		// Token: 0x0400309C RID: 12444
		private Stream stream;

		// Token: 0x0400309D RID: 12445
		private bool writeBoundary = true;

		// Token: 0x0400309E RID: 12446
		private string preface;

		// Token: 0x0400309F RID: 12447
		private static AsyncCallback onWrite = new AsyncCallback(MimeWriter.OnWrite);
	}
}
