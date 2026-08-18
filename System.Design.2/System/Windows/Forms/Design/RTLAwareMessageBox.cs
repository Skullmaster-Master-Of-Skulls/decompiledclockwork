using System;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000327 RID: 807
	internal static class RTLAwareMessageBox
	{
		// Token: 0x06001FE1 RID: 8161 RVA: 0x000C1289 File Offset: 0x000BF489
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			if (RTLAwareMessageBox.IsRTLResources)
			{
				options |= (MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
			}
			return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x000C12AB File Offset: 0x000BF4AB
		public static bool IsRTLResources
		{
			get
			{
				return SR.GetString("RTL") != "RTL_False";
			}
		}
	}
}
