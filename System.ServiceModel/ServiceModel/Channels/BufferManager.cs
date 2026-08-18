using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000771 RID: 1905
	[__DynamicallyInvokable]
	public abstract class BufferManager
	{
		// Token: 0x060048B9 RID: 18617
		[__DynamicallyInvokable]
		public abstract byte[] TakeBuffer(int bufferSize);

		// Token: 0x060048BA RID: 18618
		[__DynamicallyInvokable]
		public abstract void ReturnBuffer(byte[] buffer);

		// Token: 0x060048BB RID: 18619
		[__DynamicallyInvokable]
		public abstract void Clear();

		// Token: 0x060048BC RID: 18620 RVA: 0x0010C9D4 File Offset: 0x0010ABD4
		[__DynamicallyInvokable]
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

		// Token: 0x060048BD RID: 18621 RVA: 0x0010CA40 File Offset: 0x0010AC40
		internal static InternalBufferManager GetInternalBufferManager(BufferManager bufferManager)
		{
			if (bufferManager is BufferManager.WrappingBufferManager)
			{
				return ((BufferManager.WrappingBufferManager)bufferManager).InternalBufferManager;
			}
			return new BufferManager.WrappingInternalBufferManager(bufferManager);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0010CA5C File Offset: 0x0010AC5C
		[__DynamicallyInvokable]
		protected BufferManager()
		{
		}

		// Token: 0x02000CE6 RID: 3302
		private class WrappingBufferManager : BufferManager
		{
			// Token: 0x06007A33 RID: 31283 RVA: 0x001C77C9 File Offset: 0x001C59C9
			public WrappingBufferManager(InternalBufferManager innerBufferManager)
			{
				this.innerBufferManager = innerBufferManager;
			}

			// Token: 0x17001BA7 RID: 7079
			// (get) Token: 0x06007A34 RID: 31284 RVA: 0x001C77D8 File Offset: 0x001C59D8
			public InternalBufferManager InternalBufferManager
			{
				get
				{
					return this.innerBufferManager;
				}
			}

			// Token: 0x06007A35 RID: 31285 RVA: 0x001C77E0 File Offset: 0x001C59E0
			public override byte[] TakeBuffer(int bufferSize)
			{
				if (bufferSize < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("bufferSize", bufferSize, SR.GetString("ValueMustBeNonNegative")));
				}
				return this.innerBufferManager.TakeBuffer(bufferSize);
			}

			// Token: 0x06007A36 RID: 31286 RVA: 0x001C7817 File Offset: 0x001C5A17
			public override void ReturnBuffer(byte[] buffer)
			{
				if (buffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
				}
				this.innerBufferManager.ReturnBuffer(buffer);
			}

			// Token: 0x06007A37 RID: 31287 RVA: 0x001C7838 File Offset: 0x001C5A38
			public override void Clear()
			{
				this.innerBufferManager.Clear();
			}

			// Token: 0x040045E6 RID: 17894
			private InternalBufferManager innerBufferManager;
		}

		// Token: 0x02000CE7 RID: 3303
		private class WrappingInternalBufferManager : InternalBufferManager
		{
			// Token: 0x06007A38 RID: 31288 RVA: 0x001C7845 File Offset: 0x001C5A45
			public WrappingInternalBufferManager(BufferManager innerBufferManager)
			{
				this.innerBufferManager = innerBufferManager;
			}

			// Token: 0x06007A39 RID: 31289 RVA: 0x001C7854 File Offset: 0x001C5A54
			public override void Clear()
			{
				this.innerBufferManager.Clear();
			}

			// Token: 0x06007A3A RID: 31290 RVA: 0x001C7861 File Offset: 0x001C5A61
			public override void ReturnBuffer(byte[] buffer)
			{
				this.innerBufferManager.ReturnBuffer(buffer);
			}

			// Token: 0x06007A3B RID: 31291 RVA: 0x001C786F File Offset: 0x001C5A6F
			public override byte[] TakeBuffer(int bufferSize)
			{
				return this.innerBufferManager.TakeBuffer(bufferSize);
			}

			// Token: 0x040045E7 RID: 17895
			private BufferManager innerBufferManager;
		}
	}
}
