using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004B5 RID: 1205
	public interface ICom2PropertyPageDisplayService
	{
		// Token: 0x06004F79 RID: 20345
		void ShowPropertyPage(string title, object component, int dispid, Guid pageGuid, IntPtr parentHandle);
	}
}
