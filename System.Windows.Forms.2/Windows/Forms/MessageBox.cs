using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020002FA RID: 762
	public class MessageBox
	{
		// Token: 0x06003051 RID: 12369 RVA: 0x00002843 File Offset: 0x00000A43
		private MessageBox()
		{
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000D949C File Offset: 0x000D769C
		private static DialogResult Win32ToDialogResult(int value)
		{
			switch (value)
			{
			case 1:
				return DialogResult.OK;
			case 2:
				return DialogResult.Cancel;
			case 3:
				return DialogResult.Abort;
			case 4:
				return DialogResult.Retry;
			case 5:
				return DialogResult.Ignore;
			case 6:
				return DialogResult.Yes;
			case 7:
				return DialogResult.No;
			default:
				return DialogResult.No;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003053 RID: 12371 RVA: 0x000D94D3 File Offset: 0x000D76D3
		internal static HelpInfo HelpInfo
		{
			get
			{
				if (MessageBox.helpInfoTable != null && MessageBox.helpInfoTable.Length != 0)
				{
					return MessageBox.helpInfoTable[MessageBox.helpInfoTable.Length - 1];
				}
				return null;
			}
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000D94F8 File Offset: 0x000D76F8
		private static void PopHelpInfo()
		{
			if (MessageBox.helpInfoTable != null)
			{
				if (MessageBox.helpInfoTable.Length == 1)
				{
					MessageBox.helpInfoTable = null;
					return;
				}
				int num = MessageBox.helpInfoTable.Length - 1;
				HelpInfo[] destinationArray = new HelpInfo[num];
				Array.Copy(MessageBox.helpInfoTable, destinationArray, num);
				MessageBox.helpInfoTable = destinationArray;
			}
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000D9540 File Offset: 0x000D7740
		private static void PushHelpInfo(HelpInfo hpi)
		{
			int num = 0;
			HelpInfo[] array;
			if (MessageBox.helpInfoTable == null)
			{
				array = new HelpInfo[num + 1];
			}
			else
			{
				num = MessageBox.helpInfoTable.Length;
				array = new HelpInfo[num + 1];
				Array.Copy(MessageBox.helpInfoTable, array, num);
			}
			array[num] = hpi;
			MessageBox.helpInfoTable = array;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000D9588 File Offset: 0x000D7788
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, displayHelpButton);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000D959C File Offset: 0x000D779C
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000D95C0 File Offset: 0x000D77C0
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000D95E8 File Offset: 0x000D77E8
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath, keyword);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000D9610 File Offset: 0x000D7810
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, string keyword)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath, keyword);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000D9638 File Offset: 0x000D7838
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath, navigator);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000D9660 File Offset: 0x000D7860
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath, navigator);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000D9688 File Offset: 0x000D7888
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath, navigator, param);
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000D96B0 File Offset: 0x000D78B0
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, string helpFilePath, HelpNavigator navigator, object param)
		{
			HelpInfo hpi = new HelpInfo(helpFilePath, navigator, param);
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, hpi);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000D96D9 File Offset: 0x000D78D9
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, options, false);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000D96EA File Offset: 0x000D78EA
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, defaultButton, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000D96FA File Offset: 0x000D78FA
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000D9709 File Offset: 0x000D7909
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			return MessageBox.ShowCore(null, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000D9718 File Offset: 0x000D7918
		public static DialogResult Show(string text, string caption)
		{
			return MessageBox.ShowCore(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000D9727 File Offset: 0x000D7927
		public static DialogResult Show(string text)
		{
			return MessageBox.ShowCore(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000D973A File Offset: 0x000D793A
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, false);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000D974C File Offset: 0x000D794C
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000D975D File Offset: 0x000D795D
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000D976D File Offset: 0x000D796D
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			return MessageBox.ShowCore(owner, text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000D977C File Offset: 0x000D797C
		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			return MessageBox.ShowCore(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000D978B File Offset: 0x000D798B
		public static DialogResult Show(IWin32Window owner, string text)
		{
			return MessageBox.ShowCore(owner, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0, false);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x000D97A0 File Offset: 0x000D79A0
		private static DialogResult ShowCore(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, HelpInfo hpi)
		{
			DialogResult result = DialogResult.None;
			try
			{
				MessageBox.PushHelpInfo(hpi);
				result = MessageBox.ShowCore(owner, text, caption, buttons, icon, defaultButton, options, true);
			}
			finally
			{
				MessageBox.PopHelpInfo();
			}
			return result;
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000D97E0 File Offset: 0x000D79E0
		private static DialogResult ShowCore(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool showHelp)
		{
			if (!ClientUtils.IsEnumValid(buttons, (int)buttons, 0, 5))
			{
				throw new InvalidEnumArgumentException("buttons", (int)buttons, typeof(MessageBoxButtons));
			}
			if (!WindowsFormsUtils.EnumValidator.IsEnumWithinShiftedRange(icon, 4, 0, 4))
			{
				throw new InvalidEnumArgumentException("icon", (int)icon, typeof(MessageBoxIcon));
			}
			if (!WindowsFormsUtils.EnumValidator.IsEnumWithinShiftedRange(defaultButton, 8, 0, 2))
			{
				throw new InvalidEnumArgumentException("defaultButton", (int)defaultButton, typeof(DialogResult));
			}
			if (!SystemInformation.UserInteractive && (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == (MessageBoxOptions)0)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			if (owner != null && (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != (MessageBoxOptions)0)
			{
				throw new ArgumentException(SR.GetString("CantShowMBServiceWithOwner"), "options");
			}
			if (showHelp && (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) != (MessageBoxOptions)0)
			{
				throw new ArgumentException(SR.GetString("CantShowMBServiceWithHelp"), "options");
			}
			if ((options & ~(MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading)) != (MessageBoxOptions)0)
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			IntSecurity.SafeSubWindows.Demand();
			int num = showHelp ? 16384 : 0;
			num |= (int)(buttons | (MessageBoxButtons)icon | (MessageBoxButtons)defaultButton | (MessageBoxButtons)options);
			IntPtr handle = IntPtr.Zero;
			if (showHelp || (options & (MessageBoxOptions.ServiceNotification | MessageBoxOptions.DefaultDesktopOnly)) == (MessageBoxOptions)0)
			{
				if (owner == null)
				{
					handle = UnsafeNativeMethods.GetActiveWindow();
				}
				else
				{
					handle = Control.GetSafeHandle(owner);
				}
			}
			IntPtr userCookie = IntPtr.Zero;
			if (Application.UseVisualStyles)
			{
				if (UnsafeNativeMethods.GetModuleHandle("shell32.dll") == IntPtr.Zero && UnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable("shell32.dll") == IntPtr.Zero)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new Win32Exception(lastWin32Error, SR.GetString("LoadDLLError", new object[]
					{
						"shell32.dll"
					}));
				}
				userCookie = UnsafeNativeMethods.ThemingScope.Activate();
			}
			Application.BeginModalMessageLoop();
			DialogResult result;
			try
			{
				result = MessageBox.Win32ToDialogResult(SafeNativeMethods.MessageBox(new HandleRef(owner, handle), text, caption, num));
			}
			finally
			{
				Application.EndModalMessageLoop();
				UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(owner, handle), 7, 0, 0);
			return result;
		}

		// Token: 0x040013EF RID: 5103
		private const int IDOK = 1;

		// Token: 0x040013F0 RID: 5104
		private const int IDCANCEL = 2;

		// Token: 0x040013F1 RID: 5105
		private const int IDABORT = 3;

		// Token: 0x040013F2 RID: 5106
		private const int IDRETRY = 4;

		// Token: 0x040013F3 RID: 5107
		private const int IDIGNORE = 5;

		// Token: 0x040013F4 RID: 5108
		private const int IDYES = 6;

		// Token: 0x040013F5 RID: 5109
		private const int IDNO = 7;

		// Token: 0x040013F6 RID: 5110
		private const int HELP_BUTTON = 16384;

		// Token: 0x040013F7 RID: 5111
		[ThreadStatic]
		private static HelpInfo[] helpInfoTable;
	}
}
