using System;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007CC RID: 1996
	internal interface IConnection
	{
		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06004B2F RID: 19247
		byte[] AsyncReadBuffer { get; }

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x06004B30 RID: 19248
		int AsyncReadBufferSize { get; }

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06004B31 RID: 19249
		// (set) Token: 0x06004B32 RID: 19250
		TraceEventType ExceptionEventType { get; set; }

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06004B33 RID: 19251
		IPEndPoint RemoteIPEndPoint { get; }

		// Token: 0x06004B34 RID: 19252
		void Abort();

		// Token: 0x06004B35 RID: 19253
		void Close(TimeSpan timeout, bool asyncAndLinger);

		// Token: 0x06004B36 RID: 19254
		void Shutdown(TimeSpan timeout);

		// Token: 0x06004B37 RID: 19255
		AsyncCompletionResult BeginWrite(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x06004B38 RID: 19256
		void EndWrite();

		// Token: 0x06004B39 RID: 19257
		void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout);

		// Token: 0x06004B3A RID: 19258
		void Write(byte[] buffer, int offset, int size, bool immediate, TimeSpan timeout, BufferManager bufferManager);

		// Token: 0x06004B3B RID: 19259
		int Read(byte[] buffer, int offset, int size, TimeSpan timeout);

		// Token: 0x06004B3C RID: 19260
		AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state);

		// Token: 0x06004B3D RID: 19261
		int EndRead();

		// Token: 0x06004B3E RID: 19262
		object DuplicateAndClose(int targetProcessId);

		// Token: 0x06004B3F RID: 19263
		object GetCoreTransport();

		// Token: 0x06004B40 RID: 19264
		IAsyncResult BeginValidate(Uri uri, AsyncCallback callback, object state);

		// Token: 0x06004B41 RID: 19265
		bool EndValidate(IAsyncResult result);
	}
}
