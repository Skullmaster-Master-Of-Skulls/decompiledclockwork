using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x020000D2 RID: 210
	public class CheckBoxEditorFactory : ColumnTypeEditorFactory
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0003F020 File Offset: 0x0003E020
		public override ColumnTypeEditorPanel getInstance(ColumnTypeDef editee)
		{
			return new CheckBoxEditor();
		}
	}
}
