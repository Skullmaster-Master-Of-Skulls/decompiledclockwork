using System;

namespace System.Web
{
	// Token: 0x02000045 RID: 69
	internal class AllocatorProvider : IAllocatorProvider
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00006827 File Offset: 0x00004A27
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000682F File Offset: 0x00004A2F
		public IBufferAllocator<char> CharBufferAllocator
		{
			get
			{
				return this._charAllocator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._charAllocator = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00006846 File Offset: 0x00004A46
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000684E File Offset: 0x00004A4E
		public IBufferAllocator<int> IntBufferAllocator
		{
			get
			{
				return this._intAllocator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._intAllocator = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00006865 File Offset: 0x00004A65
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000686D File Offset: 0x00004A6D
		public IBufferAllocator<IntPtr> IntPtrBufferAllocator
		{
			get
			{
				return this._intPtrAllocator;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._intPtrAllocator = value;
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00006884 File Offset: 0x00004A84
		public void TrimMemory()
		{
			if (this._charAllocator != null)
			{
				this._charAllocator.ReleaseAllBuffers();
			}
			if (this._intAllocator != null)
			{
				this._intAllocator.ReleaseAllBuffers();
			}
			if (this._intPtrAllocator != null)
			{
				this._intPtrAllocator.ReleaseAllBuffers();
			}
		}

		// Token: 0x04000128 RID: 296
		private IBufferAllocator<char> _charAllocator;

		// Token: 0x04000129 RID: 297
		private IBufferAllocator<int> _intAllocator;

		// Token: 0x0400012A RID: 298
		private IBufferAllocator<IntPtr> _intPtrAllocator;
	}
}
