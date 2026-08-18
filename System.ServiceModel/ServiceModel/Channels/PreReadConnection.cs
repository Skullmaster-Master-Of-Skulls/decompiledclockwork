using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D0 RID: 2000
	internal class PreReadConnection : DelegatingConnection
	{
		// Token: 0x06004B5D RID: 19293 RVA: 0x001138D3 File Offset: 0x00111AD3
		public PreReadConnection(IConnection innerConnection, byte[] initialData) : this(innerConnection, initialData, 0, initialData.Length)
		{
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x001138E1 File Offset: 0x00111AE1
		public PreReadConnection(IConnection innerConnection, byte[] initialData, int initialOffset, int initialSize) : base(innerConnection)
		{
			this.preReadData = initialData;
			this.preReadOffset = initialOffset;
			this.preReadCount = initialSize;
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x00113900 File Offset: 0x00111B00
		public void AddPreReadData(byte[] initialData, int initialOffset, int initialSize)
		{
			if (this.preReadCount > 0)
			{
				byte[] src = this.preReadData;
				this.preReadData = DiagnosticUtility.Utility.AllocateByteArray(initialSize + this.preReadCount);
				Buffer.BlockCopy(src, this.preReadOffset, this.preReadData, 0, this.preReadCount);
				Buffer.BlockCopy(initialData, initialOffset, this.preReadData, this.preReadCount, initialSize);
				this.preReadOffset = 0;
				this.preReadCount += initialSize;
				return;
			}
			this.preReadData = initialData;
			this.preReadOffset = initialOffset;
			this.preReadCount = initialSize;
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00113990 File Offset: 0x00111B90
		public override int Read(byte[] buffer, int offset, int size, TimeSpan timeout)
		{
			ConnectionUtilities.ValidateBufferBounds(buffer, offset, size);
			if (this.preReadCount > 0)
			{
				int num = Math.Min(size, this.preReadCount);
				Buffer.BlockCopy(this.preReadData, this.preReadOffset, buffer, offset, num);
				this.preReadOffset += num;
				this.preReadCount -= num;
				return num;
			}
			return base.Read(buffer, offset, size, timeout);
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x001139F8 File Offset: 0x00111BF8
		public override AsyncCompletionResult BeginRead(int offset, int size, TimeSpan timeout, WaitCallback callback, object state)
		{
			ConnectionUtilities.ValidateBufferBounds(this.AsyncReadBufferSize, offset, size);
			if (this.preReadCount > 0)
			{
				int num = Math.Min(size, this.preReadCount);
				Buffer.BlockCopy(this.preReadData, this.preReadOffset, this.AsyncReadBuffer, offset, num);
				this.preReadOffset += num;
				this.preReadCount -= num;
				this.asyncBytesRead = num;
				return AsyncCompletionResult.Completed;
			}
			return base.BeginRead(offset, size, timeout, callback, state);
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00113A74 File Offset: 0x00111C74
		public override int EndRead()
		{
			if (this.asyncBytesRead > 0)
			{
				int result = this.asyncBytesRead;
				this.asyncBytesRead = 0;
				return result;
			}
			return base.EndRead();
		}

		// Token: 0x04002F3F RID: 12095
		private int asyncBytesRead;

		// Token: 0x04002F40 RID: 12096
		private byte[] preReadData;

		// Token: 0x04002F41 RID: 12097
		private int preReadOffset;

		// Token: 0x04002F42 RID: 12098
		private int preReadCount;
	}
}
