using System;
using System.Runtime;

namespace System.IdentityModel
{
	// Token: 0x02000028 RID: 40
	internal class BufferManagerOutputStream : BufferedOutputStream
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public BufferManagerOutputStream(string quotaExceededString)
		{
			this.quotaExceededString = quotaExceededString;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005DAF File Offset: 0x00003FAF
		public BufferManagerOutputStream(string quotaExceededString, int maxSize) : base(maxSize)
		{
			this.quotaExceededString = quotaExceededString;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005DBF File Offset: 0x00003FBF
		public BufferManagerOutputStream(string quotaExceededString, int initialSize, int maxSize, BufferManager bufferManager) : base(initialSize, maxSize, BufferManager.GetInternalBufferManager(bufferManager))
		{
			this.quotaExceededString = quotaExceededString;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005DD7 File Offset: 0x00003FD7
		public void Init(int initialSize, int maxSizeQuota, BufferManager bufferManager)
		{
			this.Init(initialSize, maxSizeQuota, maxSizeQuota, bufferManager);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005DE3 File Offset: 0x00003FE3
		public void Init(int initialSize, int maxSizeQuota, int effectiveMaxSize, BufferManager bufferManager)
		{
			base.Reinitialize(initialSize, maxSizeQuota, effectiveMaxSize, BufferManager.GetInternalBufferManager(bufferManager));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005DF5 File Offset: 0x00003FF5
		protected override Exception CreateQuotaExceededException(int maxSizeQuota)
		{
			return new LimitExceededException(SR.GetString(this.quotaExceededString, new object[]
			{
				maxSizeQuota
			}));
		}

		// Token: 0x040000E5 RID: 229
		private string quotaExceededString;
	}
}
