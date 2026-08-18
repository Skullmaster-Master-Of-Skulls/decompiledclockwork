using System;

namespace AjaxControlToolkit
{
	// Token: 0x0200015B RID: 347
	public class PieChartValue
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00017FB3 File Offset: 0x000161B3
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00017FBB File Offset: 0x000161BB
		public decimal Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00017FC4 File Offset: 0x000161C4
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00017FCC File Offset: 0x000161CC
		public string Category
		{
			get
			{
				return this._category;
			}
			set
			{
				this._category = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00017FD5 File Offset: 0x000161D5
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00017FDD File Offset: 0x000161DD
		public string PieChartValueColor
		{
			get
			{
				return this._pieChartValueColor;
			}
			set
			{
				this._pieChartValueColor = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00017FE6 File Offset: 0x000161E6
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00017FEE File Offset: 0x000161EE
		public string PieChartValueStrokeColor
		{
			get
			{
				return this._pieChartValueStrokeColor;
			}
			set
			{
				this._pieChartValueStrokeColor = value;
			}
		}

		// Token: 0x0400039D RID: 925
		private decimal _data;

		// Token: 0x0400039E RID: 926
		private string _category = string.Empty;

		// Token: 0x0400039F RID: 927
		private string _pieChartValueColor = string.Empty;

		// Token: 0x040003A0 RID: 928
		private string _pieChartValueStrokeColor = string.Empty;
	}
}
