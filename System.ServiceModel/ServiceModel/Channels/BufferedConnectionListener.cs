using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007CB RID: 1995
	internal class BufferedConnectionListener : IConnectionListener, IDisposable
	{
		// Token: 0x06004B2A RID: 19242 RVA: 0x0011371E File Offset: 0x0011191E
		public BufferedConnectionListener(IConnectionListener connectionListener, TimeSpan flushTimeout, int writeBufferSize)
		{
			this.connectionListener = connectionListener;
			this.flushTimeout = flushTimeout;
			this.writeBufferSize = writeBufferSize;
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x0011373B File Offset: 0x0011193B
		public void Dispose()
		{
			this.connectionListener.Dispose();
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00113748 File Offset: 0x00111948
		public void Listen()
		{
			this.connectionListener.Listen();
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00113755 File Offset: 0x00111955
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			return this.connectionListener.BeginAccept(callback, state);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00113764 File Offset: 0x00111964
		public IConnection EndAccept(IAsyncResult result)
		{
			IConnection connection = this.connectionListener.EndAccept(result);
			if (connection == null)
			{
				return connection;
			}
			return new BufferedConnection(connection, this.flushTimeout, this.writeBufferSize);
		}

		// Token: 0x04002F3B RID: 12091
		private int writeBufferSize;

		// Token: 0x04002F3C RID: 12092
		private TimeSpan flushTimeout;

		// Token: 0x04002F3D RID: 12093
		private IConnectionListener connectionListener;
	}
}
