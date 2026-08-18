using System;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000772 RID: 1906
	internal class BufferManagerOutputStream : BufferedOutputStream
	{
		// Token: 0x060048BF RID: 18623 RVA: 0x0010CA64 File Offset: 0x0010AC64
		public BufferManagerOutputStream(string quotaExceededString)
		{
			this.quotaExceededString = quotaExceededString;
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0010CA73 File Offset: 0x0010AC73
		public BufferManagerOutputStream(string quotaExceededString, int maxSize) : base(maxSize)
		{
			this.quotaExceededString = quotaExceededString;
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x0010CA83 File Offset: 0x0010AC83
		public BufferManagerOutputStream(string quotaExceededString, int initialSize, int maxSize, BufferManager bufferManager) : base(initialSize, maxSize, BufferManager.GetInternalBufferManager(bufferManager))
		{
			this.quotaExceededString = quotaExceededString;
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x0010CA9B File Offset: 0x0010AC9B
		public void Init(int initialSize, int maxSizeQuota, BufferManager bufferManager)
		{
			this.Init(initialSize, maxSizeQuota, maxSizeQuota, bufferManager);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0010CAA7 File Offset: 0x0010ACA7
		public void Init(int initialSize, int maxSizeQuota, int effectiveMaxSize, BufferManager bufferManager)
		{
			base.Reinitialize(initialSize, maxSizeQuota, effectiveMaxSize, BufferManager.GetInternalBufferManager(bufferManager));
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x0010CABC File Offset: 0x0010ACBC
		protected override Exception CreateQuotaExceededException(int maxSizeQuota)
		{
			string @string = SR.GetString(this.quotaExceededString, new object[]
			{
				maxSizeQuota
			});
			if (TD.MaxSentMessageSizeExceededIsEnabled())
			{
				TD.MaxSentMessageSizeExceeded(@string);
			}
			return new QuotaExceededException(@string);
		}

		// Token: 0x04002DFD RID: 11773
		private string quotaExceededString;
	}
}
