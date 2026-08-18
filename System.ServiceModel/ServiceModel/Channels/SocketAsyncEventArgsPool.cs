using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008BE RID: 2238
	internal class SocketAsyncEventArgsPool : QueuedObjectPool<SocketAsyncEventArgs>
	{
		// Token: 0x06005555 RID: 21845 RVA: 0x00139578 File Offset: 0x00137778
		public SocketAsyncEventArgsPool(int acceptBufferSize)
		{
			if (acceptBufferSize <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("acceptBufferSize"));
			}
			this.acceptBufferSize = acceptBufferSize;
			int num = (131072 + acceptBufferSize - 1) / acceptBufferSize;
			if (num > 16)
			{
				num = 16;
			}
			base.Initialize(num, num * 4);
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x001395C9 File Offset: 0x001377C9
		public override bool Return(SocketAsyncEventArgs socketAsyncEventArgs)
		{
			SocketAsyncEventArgsPool.CleanupAcceptSocket(socketAsyncEventArgs);
			if (!base.Return(socketAsyncEventArgs))
			{
				this.CleanupItem(socketAsyncEventArgs);
				return false;
			}
			return true;
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x001395E4 File Offset: 0x001377E4
		internal static void CleanupAcceptSocket(SocketAsyncEventArgs socketAsyncEventArgs)
		{
			Socket acceptSocket = socketAsyncEventArgs.AcceptSocket;
			if (acceptSocket != null)
			{
				socketAsyncEventArgs.AcceptSocket = null;
				try
				{
					acceptSocket.Close(0);
				}
				catch (SocketException exception)
				{
					FxTrace.Exception.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (ObjectDisposedException exception2)
				{
					FxTrace.Exception.TraceHandledException(exception2, TraceEventType.Information);
				}
			}
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x00139648 File Offset: 0x00137848
		protected override void CleanupItem(SocketAsyncEventArgs item)
		{
			item.Dispose();
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x00139650 File Offset: 0x00137850
		protected override SocketAsyncEventArgs Create()
		{
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			byte[] buffer = DiagnosticUtility.Utility.AllocateByteArray(this.acceptBufferSize);
			socketAsyncEventArgs.SetBuffer(buffer, 0, this.acceptBufferSize);
			return socketAsyncEventArgs;
		}

		// Token: 0x04003375 RID: 13173
		private const int SingleBatchSize = 131072;

		// Token: 0x04003376 RID: 13174
		private const int MaxBatchCount = 16;

		// Token: 0x04003377 RID: 13175
		private const int MaxFreeCountFactor = 4;

		// Token: 0x04003378 RID: 13176
		private int acceptBufferSize;
	}
}
