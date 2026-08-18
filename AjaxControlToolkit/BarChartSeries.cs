using System;
using System.ComponentModel;

namespace AjaxControlToolkit
{
	// Token: 0x02000047 RID: 71
	public class BarChartSeries
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000088A7 File Offset: 0x00006AA7
		// (set) Token: 0x0600025E RID: 606 RVA: 0x000088AF File Offset: 0x00006AAF
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000088B8 File Offset: 0x00006AB8
		// (set) Token: 0x06000260 RID: 608 RVA: 0x000088C0 File Offset: 0x00006AC0
		public string BarColor
		{
			get
			{
				return this._barColor;
			}
			set
			{
				this._barColor = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000088C9 File Offset: 0x00006AC9
		// (set) Token: 0x06000262 RID: 610 RVA: 0x000088D1 File Offset: 0x00006AD1
		[TypeConverter(typeof(DataConverter<decimal>))]
		public decimal[] Data { get; set; }

		// Token: 0x040000CE RID: 206
		private string _name = string.Empty;

		// Token: 0x040000CF RID: 207
		private string _barColor = string.Empty;
	}
}
