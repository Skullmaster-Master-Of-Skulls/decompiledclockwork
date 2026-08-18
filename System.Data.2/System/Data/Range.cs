using System;

namespace System.Data
{
	// Token: 0x02000119 RID: 281
	internal struct Range
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x00083364 File Offset: 0x00082764
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00083394 File Offset: 0x00082794
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

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x000833BC File Offset: 0x000827BC
		public bool IsNull
		{
			get
			{
				return !this.isNotNull;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x000833D4 File Offset: 0x000827D4
		public int Max
		{
			get
			{
				this.CheckNull();
				return this.max;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x000833F0 File Offset: 0x000827F0
		public int Min
		{
			get
			{
				this.CheckNull();
				return this.min;
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0008340C File Offset: 0x0008280C
		internal void CheckNull()
		{
			if (this.IsNull)
			{
				throw ExceptionBuilder.NullRange();
			}
		}

		// Token: 0x040005A1 RID: 1441
		private int min;

		// Token: 0x040005A2 RID: 1442
		private int max;

		// Token: 0x040005A3 RID: 1443
		private bool isNotNull;
	}
}
