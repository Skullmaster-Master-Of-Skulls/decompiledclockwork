using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000067 RID: 103
	public class FileNameEditorFactory : ColumnTypeEditorFactory
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0001D8D4 File Offset: 0x0001C8D4
		public override ColumnTypeEditorPanel getInstance(ColumnTypeDef editee)
		{
			return new FileNameEditor(editee as FileNameDef);
		}
	}
}
