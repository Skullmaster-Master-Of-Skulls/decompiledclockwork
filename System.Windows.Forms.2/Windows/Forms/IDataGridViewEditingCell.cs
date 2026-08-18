using System;

namespace System.Windows.Forms
{
	// Token: 0x020001D0 RID: 464
	public interface IDataGridViewEditingCell
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002060 RID: 8288
		// (set) Token: 0x06002061 RID: 8289
		object EditingCellFormattedValue { get; set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002062 RID: 8290
		// (set) Token: 0x06002063 RID: 8291
		bool EditingCellValueChanged { get; set; }

		// Token: 0x06002064 RID: 8292
		object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context);

		// Token: 0x06002065 RID: 8293
		void PrepareEditingCellForEdit(bool selectAll);
	}
}
