using System;

namespace System.Windows.Forms
{
	// Token: 0x02000351 RID: 849
	internal sealed class RTLAwareMessageBox
	{
		// Token: 0x060036B5 RID: 14005 RVA: 0x000F77C3 File Offset: 0x000F59C3
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			if (RTLAwareMessageBox.IsRTLResources)
			{
				options |= (MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
			}
			return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x000F77E5 File Offset: 0x000F59E5
		public static bool IsRTLResources
		{
			get
			{
				return SR.GetString("RTL") != "RTL_False";
			}
		}
	}
}
