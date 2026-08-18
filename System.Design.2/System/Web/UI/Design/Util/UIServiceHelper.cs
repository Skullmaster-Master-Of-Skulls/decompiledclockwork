using System;
using System.Collections;
using System.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.Util
{
	// Token: 0x02000168 RID: 360
	internal static class UIServiceHelper
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x00051EC8 File Offset: 0x000500C8
		public static Font GetDialogFont(IServiceProvider serviceProvider)
		{
			if (serviceProvider != null)
			{
				IUIService iuiservice = (IUIService)serviceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					IDictionary styles = iuiservice.Styles;
					if (styles != null)
					{
						return (Font)styles["DialogFont"];
					}
				}
			}
			return null;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00051F10 File Offset: 0x00050110
		public static IWin32Window GetDialogOwnerWindow(IServiceProvider serviceProvider)
		{
			if (serviceProvider != null)
			{
				IUIService iuiservice = (IUIService)serviceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					return iuiservice.GetDialogOwnerWindow();
				}
			}
			return null;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00051F44 File Offset: 0x00050144
		public static DialogResult ShowDialog(IServiceProvider serviceProvider, Form form)
		{
			if (serviceProvider != null)
			{
				IUIService iuiservice = (IUIService)serviceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					return iuiservice.ShowDialog(form);
				}
			}
			return form.ShowDialog();
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00051F7C File Offset: 0x0005017C
		public static void ShowError(IServiceProvider serviceProvider, string message)
		{
			if (serviceProvider != null)
			{
				IUIService iuiservice = (IUIService)serviceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					iuiservice.ShowError(message);
					return;
				}
			}
			RTLAwareMessageBox.Show(null, message, SR.GetString("UIServiceHelper_ErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00051FC4 File Offset: 0x000501C4
		public static void ShowError(IServiceProvider serviceProvider, Exception ex, string message)
		{
			if (ex != null)
			{
				message = message + Environment.NewLine + Environment.NewLine + ex.Message;
			}
			if (serviceProvider != null)
			{
				IUIService iuiservice = (IUIService)serviceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					iuiservice.ShowError(message);
					return;
				}
			}
			RTLAwareMessageBox.Show(null, message, SR.GetString("UIServiceHelper_ErrorCaption"), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00052028 File Offset: 0x00050228
		public static void ShowMessage(IServiceProvider serviceProvider, string message)
		{
			if (serviceProvider != null)
			{
				IUIService iuiservice = (IUIService)serviceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					iuiservice.ShowMessage(message);
					return;
				}
			}
			RTLAwareMessageBox.Show(null, message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0005206C File Offset: 0x0005026C
		public static DialogResult ShowMessage(IServiceProvider serviceProvider, string message, string caption, MessageBoxButtons buttons)
		{
			if (serviceProvider != null)
			{
				IUIService iuiservice = (IUIService)serviceProvider.GetService(typeof(IUIService));
				if (iuiservice != null)
				{
					return iuiservice.ShowMessage(message, caption, buttons);
				}
			}
			return RTLAwareMessageBox.Show(null, message, caption, buttons, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}
	}
}
