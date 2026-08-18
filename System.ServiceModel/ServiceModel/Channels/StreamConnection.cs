using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D3 RID: 2003
	internal class StreamConnection : IConnection
	{
		// Token: 0x06004B8D RID: 19341 RVA: 0x00113EE4 File Offset: 0x001120E4
		public StreamConnection(Stream stream, ConnectionStream innerStream)
		{
			this.stream = stream;
			this.innerStream = innerStream;
			this.onRead = Fx.ThunkCallback(new AsyncCallback(this.OnRead));
			this.onWrite = Fx.ThunkCallback(new AsyncCallback(this.OnWrite));
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x00113F34 File Offset: 0x00112134
		public byte[] AsyncReadBuffer
		{
			get
			{
				if (this.asyncReadBuffer == null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.asyncReadBuffer == null)
						{
							this.asyncReadBuffer = DiagnosticUtility.Utility.AllocateByteArray(this.innerStream.Connection.AsyncReadBufferSize);
						}
					}
				}
				return this.asyncReadBuffer;
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06004B8F RID: 19343 RVA: 0x00113FA4 File Offset: 0x001121A4
		public int AsyncReadBufferSize
		{
			get
			{
				return this.innerStream.Connection.AsyncReadBufferSize;
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x00113FB6 File Offset: 0x001121B6
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06004B91 RID: 19345 RVA: 0x00113FBE File Offset: 0x001121BE
		public object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x00113FC1 File Offset: 0x001121C1
		// (set) Token: 0x06004B93 RID: 19347 RVA: 0x00113FCE File Offset: 0x001121CE
		public TraceEventType ExceptionEventType
		{
			get
			{
				return this.innerStream.ExceptionEventType;
			}
			set
			{
				this.innerStream.ExceptionEventType = value;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x00113FDC File Offset: 0x001121DC
		public IPEndPoint RemoteIPEndPoint
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x00113FED File Offset: 0x001121ED
		public void Abort()
		{
			this.innerStream.Abort();
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x00113FFC File Offset: 0x001121FC
		private Exception ConvertIOException(IOException ioException)
		{
			if (ioException.InnerException is TimeoutException)
			{
				return new TimeoutException(ioException.InnerException.Message, ioException);
			}
			if (ioException.InnerException is CommunicationObjectAbortedException)
			{
				return new CommunicationObjectAbortedException(ioException.InnerException.Message, ioException);
			}
			if (ioException.InnerException is CommunicationException)
			{
				return new CommunicationException(ioException.InnerException.Message, ioException);
			}
			return new CommunicationException(SR.GetString("StreamError"), ioException);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x00114078 File Offset: 0x00112278
		public void Close(TimeSpan timeout, bool asyncAndLinger)
		{
			this.innerStream.CloseTimeout = timeout;
			try
			{
				this.stream.Close();
			}
			catch (IOException ioException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertIOException(ioException));
			}
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x001140C4 File Offset: 0x001122C4
		public void Shutdown(TimeSpan timeout)
		{
			this.innerStream.Shutdown(timeout);
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x001140D2 File Offset: 0x001122D2
		public object DuplicateAndClose(int targetProcessId)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x001140E3 File Offset: 0x001122E3
		public virtual object GetCoreTransport()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x001140F4 File Offset: 0x001122F4
		public IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state)
		{
			return this.innerStream.BeginValidate(uri, callback, state);
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x00114104 File Offset: 0x00112304
		public bool EndValidate(IAsyncResult result)
		{
			return this.innerStream.EndValidate(result);
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x00114114 File Offset: 0x00112314
		public AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state)
		{
			if (callback == null)
			{
				Fx.AssertAndThrow("Cannot call BeginWrite without a callback");
			}
			if (this.writeCallback != null)
			{
				Fx.AssertAndThrow("BeginWrite cannot be called twice");
			}
			this.writeCallback = callback;
			bool flag = true;
			try
			{
				this.innerStream.Immediate = immediate;
				this.SetWriteTimeout(timeout);
				IAsyncResult asyncResult = this.stream.BeginWrite(buffer, offset, size, this.onWrite, state);
				if (!asyncResult.CompletedSynchronously)
				{
					flag = false;
					return AsyncCompletionResult.Queued;
				}
				flag = false;
				this.stream.EndWrite(asyncResult);
			}
			catch (IOException ioException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertIOException(ioException));
			}
			finally
			{
				if (flag)
				{
					this.writeCallback = null;
				}
			}
			return AsyncCompletionResult.Completed;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x001141D4 File Offset: 0x001123D4
		public void EndWrite()
		{
			IAsyncResult asyncResult = this.writeResult;
			this.writeResult = null;
			this.writeCallback = null;
			if (asyncResult != null)
			{
				try
				{
					this.stream.EndWrite(asyncResult);
				}
				catch (IOException ioException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertIOException(ioException));
				}
			}
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0011422C File Offset: 0x0011242C
		private void OnWrite(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			if (this.writeResult != null)
			{
				throw Fx.AssertAndThrow("StreamConnection: OnWrite called twice.");
			}
			this.writeResult = result;
			this.writeCallback(result.AsyncState);
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x00114264 File Offset: 0x00112464
		public void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout)
		{
			try
			{
				this.innerStream.Immediate = immediate;
				this.SetWriteTimeout(timeout);
				this.stream.Write(buffer, offset, size);
			}
			catch (IOException ioException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertIOException(ioException));
			}
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x001142BC File Offset: 0x001124BC
		public void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager)
		{
			this.Write(buffer, offset, size, immediate, timeout);
			bufferManager.ReturnBuffer(buffer);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x001142D4 File Offset: 0x001124D4
		private void SetReadTimeout(TimeSpan timeout)
		{
			int readTimeout = TimeoutHelper.ToMilliseconds(timeout);
			if (this.stream.CanTimeout)
			{
				this.stream.ReadTimeout = readTimeout;
			}
			this.innerStream.ReadTimeout = readTimeout;
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x00114310 File Offset: 0x00112510
		private void SetWriteTimeout(TimeSpan timeout)
		{
			int writeTimeout = TimeoutHelper.ToMilliseconds(timeout);
			if (this.stream.CanTimeout)
			{
				this.stream.WriteTimeout = writeTimeout;
			}
			this.innerStream.WriteTimeout = writeTimeout;
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0011434C File Offset: 0x0011254C
		public int Read(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			int result;
			try
			{
				this.SetReadTimeout(timeout);
				result = this.stream.Read(buffer, offset, size);
			}
			catch (IOException ioException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertIOException(ioException));
			}
			return result;
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x00114398 File Offset: 0x00112598
		public AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			ConnectionUtilities.ValidateBufferBounds(this.AsyncReadBufferSize, offset, size);
			this.readCallback = callback;
			try
			{
				this.SetReadTimeout(timeout);
				IAsyncResult asyncResult = this.stream.BeginRead(this.AsyncReadBuffer, offset, size, this.onRead, state);
				if (!asyncResult.CompletedSynchronously)
				{
					return AsyncCompletionResult.Queued;
				}
				this.bytesRead = this.stream.EndRead(asyncResult);
			}
			catch (IOException ioException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertIOException(ioException));
			}
			return AsyncCompletionResult.Completed;
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00114424 File Offset: 0x00112624
		public int EndRead()
		{
			IAsyncResult asyncResult = this.readResult;
			this.readResult = null;
			if (asyncResult != null)
			{
				try
				{
					this.bytesRead = this.stream.EndRead(asyncResult);
				}
				catch (IOException ioException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.ConvertIOException(ioException));
				}
			}
			return this.bytesRead;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x00114480 File Offset: 0x00112680
		private void OnRead(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			if (this.readResult != null)
			{
				throw Fx.AssertAndThrow("StreamConnection: OnRead called twice.");
			}
			this.readResult = result;
			this.readCallback(result.AsyncState);
		}

		// Token: 0x04002F49 RID: 12105
		private byte[] asyncReadBuffer;

		// Token: 0x04002F4A RID: 12106
		private int bytesRead;

		// Token: 0x04002F4B RID: 12107
		private ConnectionStream innerStream;

		// Token: 0x04002F4C RID: 12108
		private AsyncCallback onRead;

		// Token: 0x04002F4D RID: 12109
		private AsyncCallback onWrite;

		// Token: 0x04002F4E RID: 12110
		private IAsyncResult readResult;

		// Token: 0x04002F4F RID: 12111
		private IAsyncResult writeResult;

		// Token: 0x04002F50 RID: 12112
		private WaitCallback readCallback;

		// Token: 0x04002F51 RID: 12113
		private WaitCallback writeCallback;

		// Token: 0x04002F52 RID: 12114
		private Stream stream;
	}
}
