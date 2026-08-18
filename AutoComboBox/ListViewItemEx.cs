using System;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x0200008E RID: 142
	public class ListViewItemEx : ListViewItem
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0002F9AC File Offset: 0x0002E9AC
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0002F9C4 File Offset: 0x0002E9C4
		public string DataField
		{
			get
			{
				return this.dataField;
			}
			set
			{
				this.dataField = value;
			}
		}

		// Token: 0x040004A6 RID: 1190
		private string dataField = "";
	}
}
