using System;

namespace System.Web
{
	// Token: 0x02000042 RID: 66
	internal class IntPtrArrayAllocator : BufferAllocator
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00006657 File Offset: 0x00004857
		internal IntPtrArrayAllocator(int arraySize, int maxFree) : base(maxFree)
		{
			this._arraySize = arraySize;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00006667 File Offset: 0x00004867
		protected override object AllocBuffer()
		{
			return new IntPtr[this._arraySize];
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00006674 File Offset: 0x00004874
		public override int BufferSize
		{
			get
			{
				return this._arraySize;
			}
		}

		// Token: 0x04000124 RID: 292
		private int _arraySize;
	}
}
