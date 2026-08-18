using System;

namespace System.Web
{
	// Token: 0x02000041 RID: 65
	internal class IntegerArrayAllocator : BufferAllocator
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x00006632 File Offset: 0x00004832
		internal IntegerArrayAllocator(int arraySize, int maxFree) : base(maxFree)
		{
			this._arraySize = arraySize;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00006642 File Offset: 0x00004842
		protected override object AllocBuffer()
		{
			return new int[this._arraySize];
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000664F File Offset: 0x0000484F
		public override int BufferSize
		{
			get
			{
				return this._arraySize;
			}
		}

		// Token: 0x04000123 RID: 291
		private int _arraySize;
	}
}
