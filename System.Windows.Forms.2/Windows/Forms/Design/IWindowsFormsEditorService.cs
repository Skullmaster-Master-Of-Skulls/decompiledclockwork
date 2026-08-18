using System;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200048C RID: 1164
	public interface IWindowsFormsEditorService
	{
		// Token: 0x06004E31 RID: 20017
		void CloseDropDown();

		// Token: 0x06004E32 RID: 20018
		void DropDownControl(Control control);

		// Token: 0x06004E33 RID: 20019
		DialogResult ShowDialog(Form dialog);
	}
}
