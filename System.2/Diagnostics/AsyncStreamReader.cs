using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020004BF RID: 1215
	internal class AsyncStreamReader : IDisposable
	{
		// Token: 0x06002D63 RID: 11619 RVA: 0x000CC550 File Offset: 0x000CA750
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding) : this(process, stream, callback, encoding, 1024)
		{
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000CC562 File Offset: 0x000CA762
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.Init(process, stream, callback, encoding, bufferSize);
			this.messageQueue = new Queue();
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000CC584 File Offset: 0x000CA784
		private void Init(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.process = process;
			this.stream = stream;
			this.encoding = encoding;
			this.userCallBack = callback;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.cancelOperation = false;
			this.eofEvent = new ManualResetEvent(false);
			this.sb = null;
			this.bLastCarriageReturn = false;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000CC619 File Offset: 0x000CA819
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000CC622 File Offset: 0x000CA822
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000CC634 File Offset: 0x000CA834
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Close();
			}
			if (this.stream != null)
			{
				this.stream = null;
				this.encoding = null;
				this.decoder = null;
				this.byteBuffer = null;
				this.charBuffer = null;
			}
			if (this.eofEvent != null)
			{
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x000CC69C File Offset: 0x000CA89C
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x000CC6A4 File Offset: 0x000CA8A4
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000CC6AC File Offset: 0x000CA8AC
		internal void BeginReadLine()
		{
			if (this.cancelOperation)
			{
				this.cancelOperation = false;
			}
			if (this.sb == null)
			{
				this.sb = new StringBuilder(1024);
				this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				return;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000CC70F File Offset: 0x000CA90F
		internal void CancelOperation()
		{
			this.cancelOperation = true;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000CC718 File Offset: 0x000CA918
		private void ReadBuffer(IAsyncResult ar)
		{
			int num;
			try
			{
				num = this.stream.EndRead(ar);
			}
			catch (IOException)
			{
				num = 0;
			}
			catch (OperationCanceledException)
			{
				num = 0;
			}
			if (num == 0)
			{
				Queue obj = this.messageQueue;
				lock (obj)
				{
					if (this.sb.Length != 0)
					{
						this.messageQueue.Enqueue(this.sb.ToString());
						this.sb.Length = 0;
					}
					this.messageQueue.Enqueue(null);
				}
				try
				{
					this.FlushMessageQueue();
					return;
				}
				finally
				{
					this.eofEvent.Set();
				}
			}
			int chars = this.decoder.GetChars(this.byteBuffer, 0, num, this.charBuffer, 0);
			this.sb.Append(this.charBuffer, 0, chars);
			this.GetLinesFromStringBuilder();
			this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000CC840 File Offset: 0x000CAA40
		private void GetLinesFromStringBuilder()
		{
			int i = this.currentLinePos;
			int num = 0;
			int length = this.sb.Length;
			if (this.bLastCarriageReturn && length > 0 && this.sb[0] == '\n')
			{
				i = 1;
				num = 1;
				this.bLastCarriageReturn = false;
			}
			while (i < length)
			{
				char c = this.sb[i];
				if (c == '\r' || c == '\n')
				{
					string obj = this.sb.ToString(num, i - num);
					num = i + 1;
					if (c == '\r' && num < length && this.sb[num] == '\n')
					{
						num++;
						i++;
					}
					Queue obj2 = this.messageQueue;
					lock (obj2)
					{
						this.messageQueue.Enqueue(obj);
					}
				}
				i++;
			}
			if (length > 0 && this.sb[length - 1] == '\r')
			{
				this.bLastCarriageReturn = true;
			}
			if (num < length)
			{
				if (num == 0)
				{
					this.currentLinePos = i;
				}
				else
				{
					this.sb.Remove(0, num);
					this.currentLinePos = 0;
				}
			}
			else
			{
				this.sb.Length = 0;
				this.currentLinePos = 0;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000CC98C File Offset: 0x000CAB8C
		private void FlushMessageQueue()
		{
			while (this.messageQueue.Count > 0)
			{
				Queue obj = this.messageQueue;
				lock (obj)
				{
					if (this.messageQueue.Count > 0)
					{
						string data = (string)this.messageQueue.Dequeue();
						if (!this.cancelOperation)
						{
							this.userCallBack(data);
						}
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000CCA08 File Offset: 0x000CAC08
		internal void WaitUtilEOF()
		{
			if (this.eofEvent != null)
			{
				this.eofEvent.WaitOne();
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x04002719 RID: 10009
		internal const int DefaultBufferSize = 1024;

		// Token: 0x0400271A RID: 10010
		private const int MinBufferSize = 128;

		// Token: 0x0400271B RID: 10011
		private Stream stream;

		// Token: 0x0400271C RID: 10012
		private Encoding encoding;

		// Token: 0x0400271D RID: 10013
		private Decoder decoder;

		// Token: 0x0400271E RID: 10014
		private byte[] byteBuffer;

		// Token: 0x0400271F RID: 10015
		private char[] charBuffer;

		// Token: 0x04002720 RID: 10016
		private int _maxCharsPerBuffer;

		// Token: 0x04002721 RID: 10017
		private Process process;

		// Token: 0x04002722 RID: 10018
		private UserCallBack userCallBack;

		// Token: 0x04002723 RID: 10019
		private bool cancelOperation;

		// Token: 0x04002724 RID: 10020
		private ManualResetEvent eofEvent;

		// Token: 0x04002725 RID: 10021
		private Queue messageQueue;

		// Token: 0x04002726 RID: 10022
		private StringBuilder sb;

		// Token: 0x04002727 RID: 10023
		private bool bLastCarriageReturn;

		// Token: 0x04002728 RID: 10024
		private int currentLinePos;
	}
}
