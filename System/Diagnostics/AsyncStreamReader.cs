using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000742 RID: 1858
	internal class AsyncStreamReader : IDisposable
	{
		// Token: 0x060038AC RID: 14508 RVA: 0x000EF2BC File Offset: 0x000EE2BC
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding) : this(process, stream, callback, encoding, 1024)
		{
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000EF2CE File Offset: 0x000EE2CE
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.Init(process, stream, callback, encoding, bufferSize);
			this.messageQueue = new Queue();
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000EF2F0 File Offset: 0x000EE2F0
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

		// Token: 0x060038AF RID: 14511 RVA: 0x000EF385 File Offset: 0x000EE385
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000EF38E File Offset: 0x000EE38E
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000EF398 File Offset: 0x000EE398
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

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000EF400 File Offset: 0x000EE400
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000EF408 File Offset: 0x000EE408
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000EF410 File Offset: 0x000EE410
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

		// Token: 0x060038B5 RID: 14517 RVA: 0x000EF473 File Offset: 0x000EE473
		internal void CancelOperation()
		{
			this.cancelOperation = true;
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000EF47C File Offset: 0x000EE47C
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
				lock (this.messageQueue)
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

		// Token: 0x060038B7 RID: 14519 RVA: 0x000EF59C File Offset: 0x000EE59C
		private void GetLinesFromStringBuilder()
		{
			int i = 0;
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
					lock (this.messageQueue)
					{
						this.messageQueue.Enqueue(obj);
					}
				}
				i++;
			}
			if (this.sb[length - 1] == '\r')
			{
				this.bLastCarriageReturn = true;
			}
			if (num < length)
			{
				this.sb.Remove(0, num);
			}
			else
			{
				this.sb.Length = 0;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000EF6B4 File Offset: 0x000EE6B4
		private void FlushMessageQueue()
		{
			while (this.messageQueue.Count > 0)
			{
				lock (this.messageQueue)
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

		// Token: 0x060038B9 RID: 14521 RVA: 0x000EF728 File Offset: 0x000EE728
		internal void WaitUtilEOF()
		{
			if (this.eofEvent != null)
			{
				this.eofEvent.WaitOne();
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x04003263 RID: 12899
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04003264 RID: 12900
		private const int MinBufferSize = 128;

		// Token: 0x04003265 RID: 12901
		private Stream stream;

		// Token: 0x04003266 RID: 12902
		private Encoding encoding;

		// Token: 0x04003267 RID: 12903
		private Decoder decoder;

		// Token: 0x04003268 RID: 12904
		private byte[] byteBuffer;

		// Token: 0x04003269 RID: 12905
		private char[] charBuffer;

		// Token: 0x0400326A RID: 12906
		private int _maxCharsPerBuffer;

		// Token: 0x0400326B RID: 12907
		private Process process;

		// Token: 0x0400326C RID: 12908
		private UserCallBack userCallBack;

		// Token: 0x0400326D RID: 12909
		private bool cancelOperation;

		// Token: 0x0400326E RID: 12910
		private ManualResetEvent eofEvent;

		// Token: 0x0400326F RID: 12911
		private Queue messageQueue;

		// Token: 0x04003270 RID: 12912
		private StringBuilder sb;

		// Token: 0x04003271 RID: 12913
		private bool bLastCarriageReturn;
	}
}
