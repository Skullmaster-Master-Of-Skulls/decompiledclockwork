using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200048B RID: 1163
	[Guid("06A9C74B-5E32-4561-BE73-381B37869F4F")]
	public interface IUIService
	{
		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06004E24 RID: 20004
		IDictionary Styles { get; }

		// Token: 0x06004E25 RID: 20005
		bool CanShowComponentEditor(object component);

		// Token: 0x06004E26 RID: 20006
		IWin32Window GetDialogOwnerWindow();

		// Token: 0x06004E27 RID: 20007
		void SetUIDirty();

		// Token: 0x06004E28 RID: 20008
		bool ShowComponentEditor(object component, IWin32Window parent);

		// Token: 0x06004E29 RID: 20009
		DialogResult ShowDialog(Form form);

		// Token: 0x06004E2A RID: 20010
		void ShowError(string message);

		// Token: 0x06004E2B RID: 20011
		void ShowError(Exception ex);

		// Token: 0x06004E2C RID: 20012
		void ShowError(Exception ex, string message);

		// Token: 0x06004E2D RID: 20013
		void ShowMessage(string message);

		// Token: 0x06004E2E RID: 20014
		void ShowMessage(string message, string caption);

		// Token: 0x06004E2F RID: 20015
		DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons);

		// Token: 0x06004E30 RID: 20016
		bool ShowToolWindow(Guid toolWindow);
	}
}
