using System;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007CF RID: 1999
	internal abstract class DelegatingConnection : IConnection
	{
		// Token: 0x06004B48 RID: 19272 RVA: 0x00113795 File Offset: 0x00111995
		protected DelegatingConnection(IConnection connection)
		{
			this.connection = connection;
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x001137A4 File Offset: 0x001119A4
		public virtual byte[] AsyncReadBuffer
		{
			get
			{
				return this.connection.AsyncReadBuffer;
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x001137B1 File Offset: 0x001119B1
		public virtual int AsyncReadBufferSize
		{
			get
			{
				return this.connection.AsyncReadBufferSize;
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06004B4B RID: 19275 RVA: 0x001137BE File Offset: 0x001119BE
		// (set) Token: 0x06004B4C RID: 19276 RVA: 0x001137CB File Offset: 0x001119CB
		public TraceEventType ExceptionEventType
		{
			get
			{
				return this.connection.ExceptionEventType;
			}
			set
			{
				this.connection.ExceptionEventType = value;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06004B4D RID: 19277 RVA: 0x001137D9 File Offset: 0x001119D9
		protected IConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x001137E1 File Offset: 0x001119E1
		public IPEndPoint RemoteIPEndPoint
		{
			get
			{
				return this.connection.RemoteIPEndPoint;
			}
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x001137EE File Offset: 0x001119EE
		public virtual void Abort()
		{
			this.connection.Abort();
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x001137FB File Offset: 0x001119FB
		public virtual void Close(TimeSpan timeout, bool asyncAndLinger)
		{
			this.connection.Close(timeout, asyncAndLinger);
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x0011380A File Offset: 0x00111A0A
		public virtual void Shutdown(TimeSpan timeout)
		{
			this.connection.Shutdown(timeout);
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x00113818 File Offset: 0x00111A18
		public virtual object DuplicateAndClose(int targetProcessId)
		{
			return this.connection.DuplicateAndClose(targetProcessId);
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x00113826 File Offset: 0x00111A26
		public virtual object GetCoreTransport()
		{
			return this.connection.GetCoreTransport();
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x00113833 File Offset: 0x00111A33
		public virtual IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state)
		{
			return this.connection.BeginValidate(uri, callback, state);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x00113843 File Offset: 0x00111A43
		public virtual bool EndValidate(IAsyncResult result)
		{
			return this.connection.EndValidate(result);
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x00113851 File Offset: 0x00111A51
		public virtual AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state)
		{
			return this.connection.BeginWrite(buffer, offset, size, immediate, timeout, callback, state);
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x00113869 File Offset: 0x00111A69
		public virtual void EndWrite()
		{
			this.connection.EndWrite();
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00113876 File Offset: 0x00111A76
		public virtual void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout)
		{
			this.connection.Write(buffer, offset, size, immediate, timeout);
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x0011388A File Offset: 0x00111A8A
		public virtual void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager)
		{
			this.connection.Write(buffer, offset, size, immediate, timeout, bufferManager);
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x001138A0 File Offset: 0x00111AA0
		public virtual int Read(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			return this.connection.Read(buffer, offset, size, timeout);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x001138B2 File Offset: 0x00111AB2
		public virtual AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			return this.connection.BeginRead(offset, size, timeout, callback, state);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x001138C6 File Offset: 0x00111AC6
		public virtual int EndRead()
		{
			return this.connection.EndRead();
		}

		// Token: 0x04002F3E RID: 12094
		private IConnection connection;
	}
}
