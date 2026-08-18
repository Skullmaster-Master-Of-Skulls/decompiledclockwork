using System;

namespace System.Windows.Forms
{
	// Token: 0x020002B1 RID: 689
	public interface IWindowTarget
	{
		// Token: 0x06002A5D RID: 10845
		void OnHandleChange(IntPtr newHandle);

		// Token: 0x06002A5E RID: 10846
		void OnMessage(ref Message m);
	}
}
