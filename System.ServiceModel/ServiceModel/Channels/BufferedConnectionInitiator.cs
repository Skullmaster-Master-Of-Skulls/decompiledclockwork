using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007CA RID: 1994
	internal class BufferedConnectionInitiator : IConnectionInitiator
	{
		// Token: 0x06004B24 RID: 19236 RVA: 0x001136A0 File Offset: 0x001118A0
		public BufferedConnectionInitiator(IConnectionInitiator connectionInitiator, TimeSpan flushTimeout, int writeBufferSize)
		{
			this.connectionInitiator = connectionInitiator;
			this.flushTimeout = flushTimeout;
			this.writeBufferSize = writeBufferSize;
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x001136BD File Offset: 0x001118BD
		protected TimeSpan FlushTimeout
		{
			get
			{
				return this.flushTimeout;
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x001136C5 File Offset: 0x001118C5
		protected int WriteBufferSize
		{
			get
			{
				return this.writeBufferSize;
			}
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x001136CD File Offset: 0x001118CD
		public IConnection Connect(Uri uri, TimeSpan timeout)
		{
			return new BufferedConnection(this.connectionInitiator.Connect(uri, timeout), this.flushTimeout, this.writeBufferSize);
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x001136ED File Offset: 0x001118ED
		public IAsyncResult BeginConnect(Uri uri, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.connectionInitiator.BeginConnect(uri, timeout, callback, state);
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x001136FF File Offset: 0x001118FF
		public IConnection EndConnect(IAsyncResult result)
		{
			return new BufferedConnection(this.connectionInitiator.EndConnect(result), this.flushTimeout, this.writeBufferSize);
		}

		// Token: 0x04002F38 RID: 12088
		private int writeBufferSize;

		// Token: 0x04002F39 RID: 12089
		private TimeSpan flushTimeout;

		// Token: 0x04002F3A RID: 12090
		private IConnectionInitiator connectionInitiator;
	}
}
