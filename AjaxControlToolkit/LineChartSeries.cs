using System;
using System.ComponentModel;

namespace AjaxControlToolkit
{
	// Token: 0x0200012C RID: 300
	public class LineChartSeries
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001437B File Offset: 0x0001257B
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00014383 File Offset: 0x00012583
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

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001438C File Offset: 0x0001258C
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00014394 File Offset: 0x00012594
		public string LineColor
		{
			get
			{
				return this._lineColor;
			}
			set
			{
				this._lineColor = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001439D File Offset: 0x0001259D
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x000143A5 File Offset: 0x000125A5
		[TypeConverter(typeof(DataConverter<decimal>))]
		public decimal[] Data { get; set; }

		// Token: 0x0400031B RID: 795
		private string _name = string.Empty;

		// Token: 0x0400031C RID: 796
		private string _lineColor = string.Empty;
	}
}
