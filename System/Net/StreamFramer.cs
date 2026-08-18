using System;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200054C RID: 1356
	internal class StreamFramer
	{
		// Token: 0x06002929 RID: 10537 RVA: 0x000ABDE0 File Offset: 0x000AADE0
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

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600292A RID: 10538 RVA: 0x000ABEA7 File Offset: 0x000AAEA7
		public FrameHeader ReadHeader
		{
			get
			{
				return this.m_CurReadHeader;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x000ABEAF File Offset: 0x000AAEAF
		public FrameHeader WriteHeader
		{
			get
			{
				return this.m_WriteHeader;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600292C RID: 10540 RVA: 0x000ABEB7 File Offset: 0x000AAEB7
		public Stream Transport
		{
			get
			{
				return this.m_Transport;
			}
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000ABEC0 File Offset: 0x000AAEC0
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

		// Token: 0x0600292E RID: 10542 RVA: 0x000AC010 File Offset: 0x000AB010
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

		// Token: 0x0600292F RID: 10543 RVA: 0x000AC08C File Offset: 0x000AB08C
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
			catch
			{
				Exception result = new IOException(SR.GetString("net_io_readfailure", new object[]
				{
					string.Empty
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
				workerAsyncResult.InvokeCallback(result);
			}
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000AC160 File Offset: 0x000AB160
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
						goto IL_146;
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
			IL_146:
			workerAsyncResult.HeaderDone = false;
			workerAsyncResult.InvokeCallback(workerAsyncResult.End);
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000AC304 File Offset: 0x000AB304
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

		// Token: 0x06002932 RID: 10546 RVA: 0x000AC3AC File Offset: 0x000AB3AC
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

		// Token: 0x06002933 RID: 10547 RVA: 0x000AC460 File Offset: 0x000AB460
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

		// Token: 0x06002934 RID: 10548 RVA: 0x000AC548 File Offset: 0x000AB548
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
			catch
			{
				workerAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000AC5CC File Offset: 0x000AB5CC
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

		// Token: 0x06002936 RID: 10550 RVA: 0x000AC63C File Offset: 0x000AB63C
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

		// Token: 0x0400283E RID: 10302
		private Stream m_Transport;

		// Token: 0x0400283F RID: 10303
		private bool m_Eof;

		// Token: 0x04002840 RID: 10304
		private FrameHeader m_WriteHeader = new FrameHeader();

		// Token: 0x04002841 RID: 10305
		private FrameHeader m_CurReadHeader = new FrameHeader();

		// Token: 0x04002842 RID: 10306
		private FrameHeader m_ReadVerifier = new FrameHeader(-1, -1, -1);

		// Token: 0x04002843 RID: 10307
		private byte[] m_ReadHeaderBuffer;

		// Token: 0x04002844 RID: 10308
		private byte[] m_WriteHeaderBuffer;

		// Token: 0x04002845 RID: 10309
		private readonly AsyncCallback m_ReadFrameCallback;

		// Token: 0x04002846 RID: 10310
		private readonly AsyncCallback m_BeginWriteCallback;

		// Token: 0x04002847 RID: 10311
		private NetworkStream m_NetworkStream;
	}
}
