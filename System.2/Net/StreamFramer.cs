using System;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000219 RID: 537
	internal class StreamFramer
	{
		// Token: 0x060013CB RID: 5067 RVA: 0x00068A5C File Offset: 0x00066C5C
		public StreamFramer(Stream Transport)
		{
			if (Transport == null || Transport == Stream.Null)
			{
				throw new ArgumentNullException("Transport");
			}
			this.m_Transport = Transport;
			if (this.m_Transport.GetType() == typeof(NetworkStream))
			{
				this.m_NetworkStream = (Transport as NetworkStream);
			}
			this.m_ReadHeaderBuffer = new byte[this.m_CurReadHeader.Size];
			this.m_WriteHeaderBuffer = new byte[this.m_WriteHeader.Size];
			this.m_ReadFrameCallback = new AsyncCallback(this.ReadFrameCallback);
			this.m_BeginWriteCallback = new AsyncCallback(this.BeginWriteCallback);
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00068B28 File Offset: 0x00066D28
		public FrameHeader ReadHeader
		{
			get
			{
				return this.m_CurReadHeader;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00068B30 File Offset: 0x00066D30
		public FrameHeader WriteHeader
		{
			get
			{
				return this.m_WriteHeader;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00068B38 File Offset: 0x00066D38
		public Stream Transport
		{
			get
			{
				return this.m_Transport;
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00068B40 File Offset: 0x00066D40
		public byte[] ReadMessage()
		{
			if (this.m_Eof)
			{
				return null;
			}
			int i = 0;
			byte[] array = this.m_ReadHeaderBuffer;
			int num;
			while (i < array.Length)
			{
				num = this.Transport.Read(array, i, array.Length - i);
				if (num == 0)
				{
					if (i == 0)
					{
						this.m_Eof = true;
						return null;
					}
					throw new IOException(SR.GetString("net_io_readfailure", new object[]
					{
						SR.GetString("net_io_connectionclosed")
					}));
				}
				else
				{
					i += num;
				}
			}
			this.m_CurReadHeader.CopyFrom(array, 0, this.m_ReadVerifier);
			if (this.m_CurReadHeader.PayloadSize > this.m_CurReadHeader.MaxMessageSize)
			{
				throw new InvalidOperationException(SR.GetString("net_frame_size", new object[]
				{
					this.m_CurReadHeader.MaxMessageSize.ToString(NumberFormatInfo.InvariantInfo),
					this.m_CurReadHeader.PayloadSize.ToString(NumberFormatInfo.InvariantInfo)
				}));
			}
			array = new byte[this.m_CurReadHeader.PayloadSize];
			for (i = 0; i < array.Length; i += num)
			{
				num = this.Transport.Read(array, i, array.Length - i);
				if (num == 0)
				{
					throw new IOException(SR.GetString("net_io_readfailure", new object[]
					{
						SR.GetString("net_io_connectionclosed")
					}));
				}
			}
			return array;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00068C84 File Offset: 0x00066E84
		public IAsyncResult BeginReadMessage(AsyncCallback asyncCallback, object stateObject)
		{
			WorkerAsyncResult workerAsyncResult;
			if (this.m_Eof)
			{
				workerAsyncResult = new WorkerAsyncResult(this, stateObject, asyncCallback, null, 0, 0);
				workerAsyncResult.InvokeCallback(-1);
				return workerAsyncResult;
			}
			workerAsyncResult = new WorkerAsyncResult(this, stateObject, asyncCallback, this.m_ReadHeaderBuffer, 0, this.m_ReadHeaderBuffer.Length);
			IAsyncResult asyncResult = this.Transport.BeginRead(this.m_ReadHeaderBuffer, 0, this.m_ReadHeaderBuffer.Length, this.m_ReadFrameCallback, workerAsyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				this.ReadFrameComplete(asyncResult);
			}
			return workerAsyncResult;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00068D00 File Offset: 0x00066F00
		private void ReadFrameCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			WorkerAsyncResult workerAsyncResult = (WorkerAsyncResult)transportResult.AsyncState;
			try
			{
				this.ReadFrameComplete(transportResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (!(ex is IOException))
				{
					ex = new IOException(SR.GetString("net_io_readfailure", new object[]
					{
						ex.Message
					}), ex);
				}
				workerAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00068D88 File Offset: 0x00066F88
		private void ReadFrameComplete(IAsyncResult transportResult)
		{
			WorkerAsyncResult workerAsyncResult;
			int payloadSize;
			for (;;)
			{
				workerAsyncResult = (WorkerAsyncResult)transportResult.AsyncState;
				int num = this.Transport.EndRead(transportResult);
				workerAsyncResult.Offset += num;
				if (num <= 0)
				{
					break;
				}
				if (workerAsyncResult.Offset >= workerAsyncResult.End)
				{
					if (workerAsyncResult.HeaderDone)
					{
						goto IL_140;
					}
					workerAsyncResult.HeaderDone = true;
					this.m_CurReadHeader.CopyFrom(workerAsyncResult.Buffer, 0, this.m_ReadVerifier);
					payloadSize = this.m_CurReadHeader.PayloadSize;
					if (payloadSize < 0)
					{
						workerAsyncResult.InvokeCallback(new IOException(SR.GetString("net_frame_read_size")));
					}
					if (payloadSize == 0)
					{
						goto Block_6;
					}
					if (payloadSize > this.m_CurReadHeader.MaxMessageSize)
					{
						goto Block_7;
					}
					byte[] array = new byte[payloadSize];
					workerAsyncResult.Buffer = array;
					workerAsyncResult.End = array.Length;
					workerAsyncResult.Offset = 0;
				}
				transportResult = this.Transport.BeginRead(workerAsyncResult.Buffer, workerAsyncResult.Offset, workerAsyncResult.End - workerAsyncResult.Offset, this.m_ReadFrameCallback, workerAsyncResult);
				if (!transportResult.CompletedSynchronously)
				{
					return;
				}
			}
			object result;
			if (!workerAsyncResult.HeaderDone && workerAsyncResult.Offset == 0)
			{
				result = -1;
			}
			else
			{
				result = new IOException(SR.GetString("net_frame_read_io"));
			}
			workerAsyncResult.InvokeCallback(result);
			return;
			Block_6:
			workerAsyncResult.InvokeCallback(0);
			return;
			Block_7:
			throw new InvalidOperationException(SR.GetString("net_frame_size", new object[]
			{
				this.m_CurReadHeader.MaxMessageSize.ToString(NumberFormatInfo.InvariantInfo),
				payloadSize.ToString(NumberFormatInfo.InvariantInfo)
			}));
			IL_140:
			workerAsyncResult.HeaderDone = false;
			workerAsyncResult.InvokeCallback(workerAsyncResult.End);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00068F28 File Offset: 0x00067128
		public byte[] EndReadMessage(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			WorkerAsyncResult workerAsyncResult = asyncResult as WorkerAsyncResult;
			if (workerAsyncResult == null)
			{
				throw new ArgumentException(SR.GetString("net_io_async_result", new object[]
				{
					typeof(WorkerAsyncResult).FullName
				}), "asyncResult");
			}
			if (!workerAsyncResult.InternalPeekCompleted)
			{
				workerAsyncResult.InternalWaitForCompletion();
			}
			if (workerAsyncResult.Result is Exception)
			{
				throw (Exception)workerAsyncResult.Result;
			}
			int num = (int)workerAsyncResult.Result;
			if (num == -1)
			{
				this.m_Eof = true;
				return null;
			}
			if (num == 0)
			{
				return new byte[0];
			}
			return workerAsyncResult.Buffer;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00068FCC File Offset: 0x000671CC
		public void WriteMessage(byte[] message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this.m_WriteHeader.PayloadSize = message.Length;
			this.m_WriteHeader.CopyTo(this.m_WriteHeaderBuffer, 0);
			if (this.m_NetworkStream != null && message.Length != 0)
			{
				BufferOffsetSize[] buffers = new BufferOffsetSize[]
				{
					new BufferOffsetSize(this.m_WriteHeaderBuffer, 0, this.m_WriteHeaderBuffer.Length, false),
					new BufferOffsetSize(message, 0, message.Length, false)
				};
				this.m_NetworkStream.MultipleWrite(buffers);
				return;
			}
			this.Transport.Write(this.m_WriteHeaderBuffer, 0, this.m_WriteHeaderBuffer.Length);
			if (message.Length == 0)
			{
				return;
			}
			this.Transport.Write(message, 0, message.Length);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0006907C File Offset: 0x0006727C
		public IAsyncResult BeginWriteMessage(byte[] message, AsyncCallback asyncCallback, object stateObject)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this.m_WriteHeader.PayloadSize = message.Length;
			this.m_WriteHeader.CopyTo(this.m_WriteHeaderBuffer, 0);
			if (this.m_NetworkStream != null && message.Length != 0)
			{
				BufferOffsetSize[] buffers = new BufferOffsetSize[]
				{
					new BufferOffsetSize(this.m_WriteHeaderBuffer, 0, this.m_WriteHeaderBuffer.Length, false),
					new BufferOffsetSize(message, 0, message.Length, false)
				};
				return this.m_NetworkStream.BeginMultipleWrite(buffers, asyncCallback, stateObject);
			}
			if (message.Length == 0)
			{
				return this.Transport.BeginWrite(this.m_WriteHeaderBuffer, 0, this.m_WriteHeaderBuffer.Length, asyncCallback, stateObject);
			}
			WorkerAsyncResult workerAsyncResult = new WorkerAsyncResult(this, stateObject, asyncCallback, message, 0, message.Length);
			IAsyncResult asyncResult = this.Transport.BeginWrite(this.m_WriteHeaderBuffer, 0, this.m_WriteHeaderBuffer.Length, this.m_BeginWriteCallback, workerAsyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				this.BeginWriteComplete(asyncResult);
			}
			return workerAsyncResult;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00069160 File Offset: 0x00067360
		private void BeginWriteCallback(IAsyncResult transportResult)
		{
			if (transportResult.CompletedSynchronously)
			{
				return;
			}
			WorkerAsyncResult workerAsyncResult = (WorkerAsyncResult)transportResult.AsyncState;
			try
			{
				this.BeginWriteComplete(transportResult);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				workerAsyncResult.InvokeCallback(ex);
			}
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x000691C0 File Offset: 0x000673C0
		private void BeginWriteComplete(IAsyncResult transportResult)
		{
			WorkerAsyncResult workerAsyncResult;
			for (;;)
			{
				workerAsyncResult = (WorkerAsyncResult)transportResult.AsyncState;
				this.Transport.EndWrite(transportResult);
				if (workerAsyncResult.Offset == workerAsyncResult.End)
				{
					break;
				}
				workerAsyncResult.Offset = workerAsyncResult.End;
				transportResult = this.Transport.BeginWrite(workerAsyncResult.Buffer, 0, workerAsyncResult.End, this.m_BeginWriteCallback, workerAsyncResult);
				if (!transportResult.CompletedSynchronously)
				{
					return;
				}
			}
			workerAsyncResult.InvokeCallback();
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x00069230 File Offset: 0x00067430
		public void EndWriteMessage(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			WorkerAsyncResult workerAsyncResult = asyncResult as WorkerAsyncResult;
			if (workerAsyncResult != null)
			{
				if (!workerAsyncResult.InternalPeekCompleted)
				{
					workerAsyncResult.InternalWaitForCompletion();
				}
				if (workerAsyncResult.Result is Exception)
				{
					throw (Exception)workerAsyncResult.Result;
				}
			}
			else
			{
				this.Transport.EndWrite(asyncResult);
			}
		}

		// Token: 0x040015D3 RID: 5587
		private Stream m_Transport;

		// Token: 0x040015D4 RID: 5588
		private bool m_Eof;

		// Token: 0x040015D5 RID: 5589
		private FrameHeader m_WriteHeader = new FrameHeader();

		// Token: 0x040015D6 RID: 5590
		private FrameHeader m_CurReadHeader = new FrameHeader();

		// Token: 0x040015D7 RID: 5591
		private FrameHeader m_ReadVerifier = new FrameHeader(-1, -1, -1);

		// Token: 0x040015D8 RID: 5592
		private byte[] m_ReadHeaderBuffer;

		// Token: 0x040015D9 RID: 5593
		private byte[] m_WriteHeaderBuffer;

		// Token: 0x040015DA RID: 5594
		private readonly AsyncCallback m_ReadFrameCallback;

		// Token: 0x040015DB RID: 5595
		private readonly AsyncCallback m_BeginWriteCallback;

		// Token: 0x040015DC RID: 5596
		private NetworkStream m_NetworkStream;
	}
}
