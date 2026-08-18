using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;

namespace System.Net.Mime
{
	// Token: 0x0200023E RID: 574
	internal abstract class BaseWriter
	{
		// Token: 0x060015C2 RID: 5570 RVA: 0x00070A60 File Offset: 0x0006EC60
		protected BaseWriter(Stream stream, bool shouldEncodeLeadingDots)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.shouldEncodeLeadingDots = shouldEncodeLeadingDots;
			this.onCloseHandler = new EventHandler(this.OnClose);
			this.bufferBuilder = new BufferBuilder();
			this.lineLength = BaseWriter.DefaultLineLength;
		}

		// Token: 0x060015C3 RID: 5571
		internal abstract void WriteHeaders(NameValueCollection headers, bool allowUnicode);

		// Token: 0x060015C4 RID: 5572 RVA: 0x00070AB8 File Offset: 0x0006ECB8
		internal void WriteHeader(string name, string value, bool allowUnicode)
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
			this.WriteAndFold(value, name.Length + 2, allowUnicode);
			this.bufferBuilder.Append(BaseWriter.CRLF);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00070B3C File Offset: 0x0006ED3C
		private void WriteAndFold(string value, int charsAlreadyOnLine, bool allowUnicode)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (MailBnfHelper.IsFWSAt(value, i))
				{
					i += 2;
					this.bufferBuilder.Append(value, num2, i - num2, allowUnicode);
					num2 = i;
					num = i;
					charsAlreadyOnLine = 0;
				}
				else if (i - num2 > this.lineLength - charsAlreadyOnLine && num != num2)
				{
					this.bufferBuilder.Append(value, num2, num - num2, allowUnicode);
					this.bufferBuilder.Append(BaseWriter.CRLF);
					num2 = num;
					charsAlreadyOnLine = 0;
				}
				else if (value[i] == MailBnfHelper.Space || value[i] == MailBnfHelper.Tab)
				{
					num = i;
				}
			}
			if (value.Length - num2 > 0)
			{
				this.bufferBuilder.Append(value, num2, value.Length - num2, allowUnicode);
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00070C03 File Offset: 0x0006EE03
		internal Stream GetContentStream()
		{
			return this.GetContentStream(null);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00070C0C File Offset: 0x0006EE0C
		private Stream GetContentStream(MultiAsyncResult multiResult)
		{
			if (this.isInContent)
			{
				throw new InvalidOperationException(SR.GetString("MailWriterIsInContent"));
			}
			this.isInContent = true;
			this.CheckBoundary();
			this.bufferBuilder.Append(BaseWriter.CRLF);
			this.Flush(multiResult);
			Stream stream = new EightBitStream(this.stream, this.shouldEncodeLeadingDots);
			ClosableStream result = new ClosableStream(stream, this.onCloseHandler);
			this.contentStream = result;
			return result;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00070C7C File Offset: 0x0006EE7C
		internal IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			Stream result = this.GetContentStream(multiAsyncResult);
			if (!(multiAsyncResult.Result is Exception))
			{
				multiAsyncResult.Result = result;
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00070CB8 File Offset: 0x0006EEB8
		internal Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (Stream)obj;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00070CE4 File Offset: 0x0006EEE4
		protected void Flush(MultiAsyncResult multiResult)
		{
			if (this.bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this.stream.BeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, BaseWriter.onWrite, multiResult);
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

		// Token: 0x060015CB RID: 5579 RVA: 0x00070D7C File Offset: 0x0006EF7C
		protected static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				BaseWriter baseWriter = (BaseWriter)multiAsyncResult.Context;
				try
				{
					baseWriter.stream.EndWrite(result);
					multiAsyncResult.Leave();
				}
				catch (Exception result2)
				{
					multiAsyncResult.Leave(result2);
				}
			}
		}

		// Token: 0x060015CC RID: 5580
		internal abstract void Close();

		// Token: 0x060015CD RID: 5581
		protected abstract void OnClose(object sender, EventArgs args);

		// Token: 0x060015CE RID: 5582 RVA: 0x00070DD8 File Offset: 0x0006EFD8
		protected virtual void CheckBoundary()
		{
		}

		// Token: 0x040016E4 RID: 5860
		private static int DefaultLineLength = 76;

		// Token: 0x040016E5 RID: 5861
		private static AsyncCallback onWrite = new AsyncCallback(BaseWriter.OnWrite);

		// Token: 0x040016E6 RID: 5862
		protected static byte[] CRLF = new byte[]
		{
			13,
			10
		};

		// Token: 0x040016E7 RID: 5863
		protected BufferBuilder bufferBuilder;

		// Token: 0x040016E8 RID: 5864
		protected Stream contentStream;

		// Token: 0x040016E9 RID: 5865
		protected bool isInContent;

		// Token: 0x040016EA RID: 5866
		protected Stream stream;

		// Token: 0x040016EB RID: 5867
		private int lineLength;

		// Token: 0x040016EC RID: 5868
		private EventHandler onCloseHandler;

		// Token: 0x040016ED RID: 5869
		private bool shouldEncodeLeadingDots;
	}
}
