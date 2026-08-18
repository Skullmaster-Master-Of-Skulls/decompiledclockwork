using System;
using System.Runtime;

namespace System.IdentityModel
{
	// Token: 0x02000027 RID: 39
	internal abstract class BufferManager
	{
		// Token: 0x06000125 RID: 293
		public abstract byte[] TakeBuffer(int bufferSize);

		// Token: 0x06000126 RID: 294
		public abstract void ReturnBuffer(byte[] buffer);

		// Token: 0x06000127 RID: 295
		public abstract void Clear();

		// Token: 0x06000128 RID: 296 RVA: 0x00005D18 File Offset: 0x00003F18
		public static BufferManager CreateBufferManager(long maxBufferPoolSize, int maxBufferSize)
		{
			if (maxBufferPoolSize < 0L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxBufferPoolSize", maxBufferPoolSize, SR.GetString("ValueMustBeNonNegative")));
			}
			if (maxBufferSize < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxBufferSize", maxBufferSize, SR.GetString("ValueMustBeNonNegative")));
			}
			return new BufferManager.WrappingBufferManager(InternalBufferManager.Create(maxBufferPoolSize, maxBufferSize));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005D84 File Offset: 0x00003F84
		internal static InternalBufferManager GetInternalBufferManager(BufferManager bufferManager)
		{
			if (bufferManager is BufferManager.WrappingBufferManager)
			{
				return ((BufferManager.WrappingBufferManager)bufferManager).InternalBufferManager;
			}
			return new BufferManager.WrappingInternalBufferManager(bufferManager);
		}

		// Token: 0x0200022A RID: 554
		private class WrappingBufferManager : BufferManager
		{
			// Token: 0x060011E8 RID: 4584 RVA: 0x0004E596 File Offset: 0x0004C796
			public WrappingBufferManager(InternalBufferManager innerBufferManager)
			{
				this.innerBufferManager = innerBufferManager;
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0004E5A5 File Offset: 0x0004C7A5
			public InternalBufferManager InternalBufferManager
			{
				get
				{
					return this.innerBufferManager;
				}
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x0004E5AD File Offset: 0x0004C7AD
			public override byte[] TakeBuffer(int bufferSize)
			{
				if (bufferSize < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("bufferSize", bufferSize, SR.GetString("ValueMustBeNonNegative")));
				}
				return this.innerBufferManager.TakeBuffer(bufferSize);
			}

			// Token: 0x060011EB RID: 4587 RVA: 0x0004E5E4 File Offset: 0x0004C7E4
			public override void ReturnBuffer(byte[] buffer)
			{
				if (buffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
				}
				this.innerBufferManager.ReturnBuffer(buffer);
			}

			// Token: 0x060011EC RID: 4588 RVA: 0x0004E605 File Offset: 0x0004C805
			public override void Clear()
			{
				this.innerBufferManager.Clear();
			}

			// Token: 0x04000F0B RID: 3851
			private InternalBufferManager innerBufferManager;
		}

		// Token: 0x0200022B RID: 555
		private class WrappingInternalBufferManager : InternalBufferManager
		{
			// Token: 0x060011ED RID: 4589 RVA: 0x0004E612 File Offset: 0x0004C812
			public WrappingInternalBufferManager(BufferManager innerBufferManager)
			{
				this.innerBufferManager = innerBufferManager;
			}

			// Token: 0x060011EE RID: 4590 RVA: 0x0004E621 File Offset: 0x0004C821
			public override void Clear()
			{
				this.innerBufferManager.Clear();
			}

			// Token: 0x060011EF RID: 4591 RVA: 0x0004E62E File Offset: 0x0004C82E
			public override void ReturnBuffer(byte[] buffer)
			{
				this.innerBufferManager.ReturnBuffer(buffer);
			}

			// Token: 0x060011F0 RID: 4592 RVA: 0x0004E63C File Offset: 0x0004C83C
			public override byte[] TakeBuffer(int bufferSize)
			{
				return this.innerBufferManager.TakeBuffer(bufferSize);
			}

			// Token: 0x04000F0C RID: 3852
			private BufferManager innerBufferManager;
		}
	}
}
