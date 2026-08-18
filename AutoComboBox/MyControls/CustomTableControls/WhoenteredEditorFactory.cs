using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200009C RID: 156
	public class WhoenteredEditorFactory : ColumnTypeEditorFactory
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x000314B8 File Offset: 0x000304B8
		public override ColumnTypeEditorPanel getInstance(ColumnTypeDef editee)
		{
			return new WhoenteredEditor(editee as WhoenteredDef);
		}
	}
}
