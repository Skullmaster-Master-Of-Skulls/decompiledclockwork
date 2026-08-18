using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200003B RID: 59
	public class DateEditorFactory : ColumnTypeEditorFactory
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00012320 File Offset: 0x00011320
		public override ColumnTypeEditorPanel getInstance(ColumnTypeDef editee)
		{
			return new DateEditor(editee as DateDef);
		}
	}
}
