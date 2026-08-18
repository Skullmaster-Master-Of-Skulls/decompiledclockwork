using System;

namespace Telerik.Charting
{
	// Token: 0x02001738 RID: 5944
	internal class Popular
	{
		// Token: 0x17004675 RID: 18037
		// (get) Token: 0x0600E785 RID: 59269 RVA: 0x0033C95E File Offset: 0x0033AB5E
		// (set) Token: 0x0600E786 RID: 59270 RVA: 0x0033C966 File Offset: 0x0033AB66
		internal float X
		{
			get
			{
				return this.popularX;
			}
			set
			{
				this.popularX = value;
			}
		}

		// Token: 0x17004676 RID: 18038
		// (get) Token: 0x0600E787 RID: 59271 RVA: 0x0033C96F File Offset: 0x0033AB6F
		// (set) Token: 0x0600E788 RID: 59272 RVA: 0x0033C977 File Offset: 0x0033AB77
		internal double YPositive
		{
			get
			{
				return this.popularYpositive;
			}
			set
			{
				this.popularYpositive = value;
			}
		}

		// Token: 0x17004677 RID: 18039
		// (get) Token: 0x0600E789 RID: 59273 RVA: 0x0033C980 File Offset: 0x0033AB80
		// (set) Token: 0x0600E78A RID: 59274 RVA: 0x0033C988 File Offset: 0x0033AB88
		internal double YNegative
		{
			get
			{
				return this.popularYnegative;
			}
			set
			{
				this.popularYnegative = value;
			}
		}

		// Token: 0x17004678 RID: 18040
		// (get) Token: 0x0600E78B RID: 59275 RVA: 0x0033C991 File Offset: 0x0033AB91
		internal float Value
		{
			get
			{
				return this.popularValue;
			}
		}

		// Token: 0x17004679 RID: 18041
		// (get) Token: 0x0600E78C RID: 59276 RVA: 0x0033C999 File Offset: 0x0033AB99
		internal int Number
		{
			get
			{
				return this.popularNumber;
			}
		}

		// Token: 0x0600E78D RID: 59277 RVA: 0x0033C9A1 File Offset: 0x0033ABA1
		internal Popular(float val, int num, float x)
		{
			this.popularValue = val;
			this.popularNumber = num;
			this.popularX = x;
			this.popularYpositive = 0.0;
			this.popularYnegative = 0.0;
		}

		// Token: 0x0600E78E RID: 59278 RVA: 0x0033C9DC File Offset: 0x0033ABDC
		internal Popular(float val, int num, float x, double yPositive, double yNegative)
		{
			this.popularValue = val;
			this.popularNumber = num;
			this.popularX = x;
			this.popularYpositive = yPositive;
			this.popularYnegative = yNegative;
		}

		// Token: 0x04004282 RID: 17026
		private float popularValue;

		// Token: 0x04004283 RID: 17027
		private int popularNumber;

		// Token: 0x04004284 RID: 17028
		private float popularX;

		// Token: 0x04004285 RID: 17029
		private double popularYpositive;

		// Token: 0x04004286 RID: 17030
		private double popularYnegative;
	}
}
