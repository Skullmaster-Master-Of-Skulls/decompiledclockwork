using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005F4 RID: 1524
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadMultiColumnComboBoxClientState
	{
		// Token: 0x06003720 RID: 14112 RVA: 0x000B65BA File Offset: 0x000B47BA
		public RadMultiColumnComboBoxClientState()
		{
			this.Enabled = true;
			this.Value = string.Empty;
			this.Text = string.Empty;
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000B65DF File Offset: 0x000B47DF
		// (set) Token: 0x06003722 RID: 14114 RVA: 0x000B65E7 File Offset: 0x000B47E7
		public ClientStateLogEntry[] LogEntries { get; set; }

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000B65F0 File Offset: 0x000B47F0
		// (set) Token: 0x06003724 RID: 14116 RVA: 0x000B65F8 File Offset: 0x000B47F8
		public string Value { get; set; }

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x000B6601 File Offset: 0x000B4801
		// (set) Token: 0x06003726 RID: 14118 RVA: 0x000B6609 File Offset: 0x000B4809
		public string Text { get; set; }

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x000B6612 File Offset: 0x000B4812
		// (set) Token: 0x06003728 RID: 14120 RVA: 0x000B661A File Offset: 0x000B481A
		public bool Enabled { get; set; }
	}
}
