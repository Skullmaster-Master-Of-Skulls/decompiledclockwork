using System;

namespace System.Data
{
	// Token: 0x020000CF RID: 207
	internal struct Range
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x00212328 File Offset: 0x00211728
		public Range(int min, int max)
		{
			if (min > max)
			{
				throw ExceptionBuilder.RangeArgument(min, max);
			}
			this.min = min;
			this.max = max;
			this.isNotNull = true;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00212358 File Offset: 0x00211758
		public int Count
		{
			get
			{
				if (this.IsNull)
				{
					return 0;
				}
				return this.max - this.min + 1;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00212388 File Offset: 0x00211788
		public bool IsNull
		{
			get
			{
				return !this.isNotNull;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x002123A8 File Offset: 0x002117A8
		public int Max
		{
			get
			{
				this.CheckNull();
				return this.max;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x002123C8 File Offset: 0x002117C8
		public int Min
		{
			get
			{
				this.CheckNull();
				return this.min;
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x002123E8 File Offset: 0x002117E8
		internal void CheckNull()
		{
			if (this.IsNull)
			{
				throw ExceptionBuilder.NullRange();
			}
		}

		// Token: 0x040008D1 RID: 2257
		private int min;

		// Token: 0x040008D2 RID: 2258
		private int max;

		// Token: 0x040008D3 RID: 2259
		private bool isNotNull;
	}
}
