using System;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000029 RID: 41
	public class DroplistEditorFactory : ColumnTypeEditorFactory
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000C628 File Offset: 0x0000B628
		public override ColumnTypeEditorPanel getInstance(ColumnTypeDef editee)
		{
			return new DroplistEditor(editee as DroplistDef);
		}
	}
}
