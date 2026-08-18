using System;

namespace System.Windows.Forms
{
	// Token: 0x020001D1 RID: 465
	public interface IDataGridViewEditingControl
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002066 RID: 8294
		// (set) Token: 0x06002067 RID: 8295
		DataGridView EditingControlDataGridView { get; set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002068 RID: 8296
		// (set) Token: 0x06002069 RID: 8297
		object EditingControlFormattedValue { get; set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600206A RID: 8298
		// (set) Token: 0x0600206B RID: 8299
		int EditingControlRowIndex { get; set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600206C RID: 8300
		// (set) Token: 0x0600206D RID: 8301
		bool EditingControlValueChanged { get; set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600206E RID: 8302
		Cursor EditingPanelCursor { get; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600206F RID: 8303
		bool RepositionEditingControlOnValueChange { get; }

		// Token: 0x06002070 RID: 8304
		void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle);

		// Token: 0x06002071 RID: 8305
		bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey);

		// Token: 0x06002072 RID: 8306
		object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context);

		// Token: 0x06002073 RID: 8307
		void PrepareEditingControlForEdit(bool selectAll);
	}
}
