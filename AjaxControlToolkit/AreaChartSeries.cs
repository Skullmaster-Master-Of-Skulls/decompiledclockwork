using System;
using System.ComponentModel;

namespace AjaxControlToolkit
{
	// Token: 0x02000038 RID: 56
	public class AreaChartSeries
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00007117 File Offset: 0x00005317
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000711F File Offset: 0x0000531F
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007128 File Offset: 0x00005328
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00007130 File Offset: 0x00005330
		public string AreaColor
		{
			get
			{
				return this._areaColor;
			}
			set
			{
				this._areaColor = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00007139 File Offset: 0x00005339
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00007141 File Offset: 0x00005341
		[TypeConverter(typeof(DataConverter<decimal>))]
		public decimal[] Data { get; set; }

		// Token: 0x040000A0 RID: 160
		private string _name = string.Empty;

		// Token: 0x040000A1 RID: 161
		private string _areaColor = string.Empty;
	}
}
