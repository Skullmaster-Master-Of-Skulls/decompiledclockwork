using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B8 RID: 2232
	internal class ConnectionBufferPool : QueuedObjectPool<byte[]>
	{
		// Token: 0x06005512 RID: 21778 RVA: 0x00138AF8 File Offset: 0x00136CF8
		public ConnectionBufferPool(int bufferSize)
		{
			int num = ConnectionBufferPool.ComputeBatchCount(bufferSize);
			this.Initialize(bufferSize, num, num * 4);
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x00138B1D File Offset: 0x00136D1D
		public ConnectionBufferPool(int bufferSize, int maxFreeCount)
		{
			this.Initialize(bufferSize, ConnectionBufferPool.ComputeBatchCount(bufferSize), maxFreeCount);
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x00138B33 File Offset: 0x00136D33
		private void Initialize(int bufferSize, int batchCount, int maxFreeCount)
		{
			this.bufferSize = bufferSize;
			if (maxFreeCount < batchCount)
			{
				maxFreeCount = batchCount;
			}
			base.Initialize(batchCount, maxFreeCount);
		}

		// Token: 0x170014ED RID: 5357
		// (get) Token: 0x06005515 RID: 21781 RVA: 0x00138B4B File Offset: 0x00136D4B
		public int BufferSize
		{
			get
			{
				return this.bufferSize;
			}
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x00138B53 File Offset: 0x00136D53
		protected override byte[] Create()
		{
			return DiagnosticUtility.Utility.AllocateByteArray(this.bufferSize);
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x00138B68 File Offset: 0x00136D68
		private static int ComputeBatchCount(int bufferSize)
		{
			int num;
			if (bufferSize != 0)
			{
				num = (131072 + bufferSize - 1) / bufferSize;
				if (num > 16)
				{
					num = 16;
				}
			}
			else
			{
				num = 16;
			}
			return num;
		}

		// Token: 0x04003358 RID: 13144
		private const int SingleBatchSize = 131072;

		// Token: 0x04003359 RID: 13145
		private const int MaxBatchCount = 16;

		// Token: 0x0400335A RID: 13146
		private const int MaxFreeCountFactor = 4;

		// Token: 0x0400335B RID: 13147
		private int bufferSize;
	}
}
