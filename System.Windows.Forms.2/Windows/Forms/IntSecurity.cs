using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000445 RID: 1093
	internal static class IntSecurity
	{
		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06004BCC RID: 19404 RVA: 0x0013B338 File Offset: 0x00139538
		public static CodeAccessPermission AdjustCursorClip
		{
			get
			{
				if (IntSecurity.adjustCursorClip == null)
				{
					IntSecurity.adjustCursorClip = IntSecurity.AllWindows;
				}
				return IntSecurity.adjustCursorClip;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06004BCD RID: 19405 RVA: 0x0013B350 File Offset: 0x00139550
		public static CodeAccessPermission AdjustCursorPosition
		{
			get
			{
				return IntSecurity.AllWindows;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06004BCE RID: 19406 RVA: 0x0013B357 File Offset: 0x00139557
		public static CodeAccessPermission AffectMachineState
		{
			get
			{
				if (IntSecurity.affectMachineState == null)
				{
					IntSecurity.affectMachineState = IntSecurity.UnmanagedCode;
				}
				return IntSecurity.affectMachineState;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06004BCF RID: 19407 RVA: 0x0013B36F File Offset: 0x0013956F
		public static CodeAccessPermission AffectThreadBehavior
		{
			get
			{
				if (IntSecurity.affectThreadBehavior == null)
				{
					IntSecurity.affectThreadBehavior = IntSecurity.UnmanagedCode;
				}
				return IntSecurity.affectThreadBehavior;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06004BD0 RID: 19408 RVA: 0x0013B387 File Offset: 0x00139587
		public static CodeAccessPermission AllPrinting
		{
			get
			{
				if (IntSecurity.allPrinting == null)
				{
					IntSecurity.allPrinting = new PrintingPermission(PrintingPermissionLevel.AllPrinting);
				}
				return IntSecurity.allPrinting;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x0013B3A0 File Offset: 0x001395A0
		public static PermissionSet AllPrintingAndUnmanagedCode
		{
			get
			{
				if (IntSecurity.allPrintingAndUnmanagedCode == null)
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.SetPermission(IntSecurity.UnmanagedCode);
					permissionSet.SetPermission(IntSecurity.AllPrinting);
					IntSecurity.allPrintingAndUnmanagedCode = permissionSet;
				}
				return IntSecurity.allPrintingAndUnmanagedCode;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x0013B3DE File Offset: 0x001395DE
		public static CodeAccessPermission AllWindows
		{
			get
			{
				if (IntSecurity.allWindows == null)
				{
					IntSecurity.allWindows = new UIPermission(UIPermissionWindow.AllWindows);
				}
				return IntSecurity.allWindows;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x0013B3F7 File Offset: 0x001395F7
		public static CodeAccessPermission ClipboardRead
		{
			get
			{
				if (IntSecurity.clipboardRead == null)
				{
					IntSecurity.clipboardRead = new UIPermission(UIPermissionClipboard.AllClipboard);
				}
				return IntSecurity.clipboardRead;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x0013B410 File Offset: 0x00139610
		public static CodeAccessPermission ClipboardOwn
		{
			get
			{
				if (IntSecurity.clipboardOwn == null)
				{
					IntSecurity.clipboardOwn = new UIPermission(UIPermissionClipboard.OwnClipboard);
				}
				return IntSecurity.clipboardOwn;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06004BD5 RID: 19413 RVA: 0x0013B429 File Offset: 0x00139629
		public static PermissionSet ClipboardWrite
		{
			get
			{
				if (IntSecurity.clipboardWrite == null)
				{
					IntSecurity.clipboardWrite = new PermissionSet(PermissionState.None);
					IntSecurity.clipboardWrite.SetPermission(IntSecurity.UnmanagedCode);
					IntSecurity.clipboardWrite.SetPermission(IntSecurity.ClipboardOwn);
				}
				return IntSecurity.clipboardWrite;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06004BD6 RID: 19414 RVA: 0x0013B462 File Offset: 0x00139662
		public static CodeAccessPermission ChangeWindowRegionForTopLevel
		{
			get
			{
				if (IntSecurity.changeWindowRegionForTopLevel == null)
				{
					IntSecurity.changeWindowRegionForTopLevel = IntSecurity.AllWindows;
				}
				return IntSecurity.changeWindowRegionForTopLevel;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06004BD7 RID: 19415 RVA: 0x0013B47A File Offset: 0x0013967A
		public static CodeAccessPermission ControlFromHandleOrLocation
		{
			get
			{
				if (IntSecurity.controlFromHandleOrLocation == null)
				{
					IntSecurity.controlFromHandleOrLocation = IntSecurity.AllWindows;
				}
				return IntSecurity.controlFromHandleOrLocation;
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06004BD8 RID: 19416 RVA: 0x0013B492 File Offset: 0x00139692
		public static CodeAccessPermission CreateAnyWindow
		{
			get
			{
				if (IntSecurity.createAnyWindow == null)
				{
					IntSecurity.createAnyWindow = IntSecurity.SafeSubWindows;
				}
				return IntSecurity.createAnyWindow;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06004BD9 RID: 19417 RVA: 0x0013B4AA File Offset: 0x001396AA
		public static CodeAccessPermission CreateGraphicsForControl
		{
			get
			{
				if (IntSecurity.createGraphicsForControl == null)
				{
					IntSecurity.createGraphicsForControl = IntSecurity.SafeSubWindows;
				}
				return IntSecurity.createGraphicsForControl;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06004BDA RID: 19418 RVA: 0x0013B4C2 File Offset: 0x001396C2
		public static CodeAccessPermission DefaultPrinting
		{
			get
			{
				if (IntSecurity.defaultPrinting == null)
				{
					IntSecurity.defaultPrinting = new PrintingPermission(PrintingPermissionLevel.DefaultPrinting);
				}
				return IntSecurity.defaultPrinting;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06004BDB RID: 19419 RVA: 0x0013B4DB File Offset: 0x001396DB
		public static CodeAccessPermission FileDialogCustomization
		{
			get
			{
				if (IntSecurity.fileDialogCustomization == null)
				{
					IntSecurity.fileDialogCustomization = new FileIOPermission(PermissionState.Unrestricted);
				}
				return IntSecurity.fileDialogCustomization;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x0013B4F4 File Offset: 0x001396F4
		public static CodeAccessPermission FileDialogOpenFile
		{
			get
			{
				if (IntSecurity.fileDialogOpenFile == null)
				{
					IntSecurity.fileDialogOpenFile = new FileDialogPermission(FileDialogPermissionAccess.Open);
				}
				return IntSecurity.fileDialogOpenFile;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x0013B50D File Offset: 0x0013970D
		public static CodeAccessPermission FileDialogSaveFile
		{
			get
			{
				if (IntSecurity.fileDialogSaveFile == null)
				{
					IntSecurity.fileDialogSaveFile = new FileDialogPermission(FileDialogPermissionAccess.Save);
				}
				return IntSecurity.fileDialogSaveFile;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06004BDE RID: 19422 RVA: 0x0013B526 File Offset: 0x00139726
		public static CodeAccessPermission GetCapture
		{
			get
			{
				if (IntSecurity.getCapture == null)
				{
					IntSecurity.getCapture = IntSecurity.AllWindows;
				}
				return IntSecurity.getCapture;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06004BDF RID: 19423 RVA: 0x0013B53E File Offset: 0x0013973E
		public static CodeAccessPermission GetParent
		{
			get
			{
				if (IntSecurity.getParent == null)
				{
					IntSecurity.getParent = IntSecurity.AllWindows;
				}
				return IntSecurity.getParent;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06004BE0 RID: 19424 RVA: 0x0013B556 File Offset: 0x00139756
		public static CodeAccessPermission ManipulateWndProcAndHandles
		{
			get
			{
				if (IntSecurity.manipulateWndProcAndHandles == null)
				{
					IntSecurity.manipulateWndProcAndHandles = IntSecurity.AllWindows;
				}
				return IntSecurity.manipulateWndProcAndHandles;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06004BE1 RID: 19425 RVA: 0x0013B56E File Offset: 0x0013976E
		public static CodeAccessPermission ModifyCursor
		{
			get
			{
				if (IntSecurity.modifyCursor == null)
				{
					IntSecurity.modifyCursor = IntSecurity.SafeSubWindows;
				}
				return IntSecurity.modifyCursor;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06004BE2 RID: 19426 RVA: 0x0013B586 File Offset: 0x00139786
		public static CodeAccessPermission ModifyFocus
		{
			get
			{
				if (IntSecurity.modifyFocus == null)
				{
					IntSecurity.modifyFocus = IntSecurity.AllWindows;
				}
				return IntSecurity.modifyFocus;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06004BE3 RID: 19427 RVA: 0x0013B59E File Offset: 0x0013979E
		public static CodeAccessPermission ObjectFromWin32Handle
		{
			get
			{
				if (IntSecurity.objectFromWin32Handle == null)
				{
					IntSecurity.objectFromWin32Handle = IntSecurity.UnmanagedCode;
				}
				return IntSecurity.objectFromWin32Handle;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x0013B5B6 File Offset: 0x001397B6
		public static CodeAccessPermission SafePrinting
		{
			get
			{
				if (IntSecurity.safePrinting == null)
				{
					IntSecurity.safePrinting = new PrintingPermission(PrintingPermissionLevel.SafePrinting);
				}
				return IntSecurity.safePrinting;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06004BE5 RID: 19429 RVA: 0x0013B5CF File Offset: 0x001397CF
		public static CodeAccessPermission SafeSubWindows
		{
			get
			{
				if (IntSecurity.safeSubWindows == null)
				{
					IntSecurity.safeSubWindows = new UIPermission(UIPermissionWindow.SafeSubWindows);
				}
				return IntSecurity.safeSubWindows;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x0013B5E8 File Offset: 0x001397E8
		public static CodeAccessPermission SafeTopLevelWindows
		{
			get
			{
				if (IntSecurity.safeTopLevelWindows == null)
				{
					IntSecurity.safeTopLevelWindows = new UIPermission(UIPermissionWindow.SafeTopLevelWindows);
				}
				return IntSecurity.safeTopLevelWindows;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06004BE7 RID: 19431 RVA: 0x0013B601 File Offset: 0x00139801
		public static CodeAccessPermission SendMessages
		{
			get
			{
				if (IntSecurity.sendMessages == null)
				{
					IntSecurity.sendMessages = IntSecurity.UnmanagedCode;
				}
				return IntSecurity.sendMessages;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x0013B619 File Offset: 0x00139819
		public static CodeAccessPermission SensitiveSystemInformation
		{
			get
			{
				if (IntSecurity.sensitiveSystemInformation == null)
				{
					IntSecurity.sensitiveSystemInformation = new EnvironmentPermission(PermissionState.Unrestricted);
				}
				return IntSecurity.sensitiveSystemInformation;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06004BE9 RID: 19433 RVA: 0x0013B632 File Offset: 0x00139832
		public static CodeAccessPermission TransparentWindows
		{
			get
			{
				if (IntSecurity.transparentWindows == null)
				{
					IntSecurity.transparentWindows = IntSecurity.AllWindows;
				}
				return IntSecurity.transparentWindows;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06004BEA RID: 19434 RVA: 0x0013B64A File Offset: 0x0013984A
		public static CodeAccessPermission TopLevelWindow
		{
			get
			{
				if (IntSecurity.topLevelWindow == null)
				{
					IntSecurity.topLevelWindow = IntSecurity.SafeTopLevelWindows;
				}
				return IntSecurity.topLevelWindow;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06004BEB RID: 19435 RVA: 0x0013B662 File Offset: 0x00139862
		public static CodeAccessPermission UnmanagedCode
		{
			get
			{
				if (IntSecurity.unmanagedCode == null)
				{
					IntSecurity.unmanagedCode = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
				}
				return IntSecurity.unmanagedCode;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x0013B67B File Offset: 0x0013987B
		public static CodeAccessPermission UnrestrictedWindows
		{
			get
			{
				if (IntSecurity.unrestrictedWindows == null)
				{
					IntSecurity.unrestrictedWindows = IntSecurity.AllWindows;
				}
				return IntSecurity.unrestrictedWindows;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06004BED RID: 19437 RVA: 0x0013B693 File Offset: 0x00139893
		public static CodeAccessPermission WindowAdornmentModification
		{
			get
			{
				if (IntSecurity.windowAdornmentModification == null)
				{
					IntSecurity.windowAdornmentModification = IntSecurity.AllWindows;
				}
				return IntSecurity.windowAdornmentModification;
			}
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0013B6AC File Offset: 0x001398AC
		internal static string UnsafeGetFullPath(string fileName)
		{
			string result = fileName;
			new FileIOPermission(PermissionState.None)
			{
				AllFiles = FileIOPermissionAccess.PathDiscovery
			}.Assert();
			try
			{
				result = Path.GetFullPath(fileName);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0013B6F0 File Offset: 0x001398F0
		internal static void DemandFileIO(FileIOPermissionAccess access, string fileName)
		{
			new FileIOPermission(access, IntSecurity.UnsafeGetFullPath(fileName)).Demand();
		}

		// Token: 0x04002847 RID: 10311
		public static readonly TraceSwitch SecurityDemand = new TraceSwitch("SecurityDemand", "Trace when security demands occur.");

		// Token: 0x04002848 RID: 10312
		private static CodeAccessPermission adjustCursorClip;

		// Token: 0x04002849 RID: 10313
		private static CodeAccessPermission affectMachineState;

		// Token: 0x0400284A RID: 10314
		private static CodeAccessPermission affectThreadBehavior;

		// Token: 0x0400284B RID: 10315
		private static CodeAccessPermission allPrinting;

		// Token: 0x0400284C RID: 10316
		private static PermissionSet allPrintingAndUnmanagedCode;

		// Token: 0x0400284D RID: 10317
		private static CodeAccessPermission allWindows;

		// Token: 0x0400284E RID: 10318
		private static CodeAccessPermission clipboardRead;

		// Token: 0x0400284F RID: 10319
		private static CodeAccessPermission clipboardOwn;

		// Token: 0x04002850 RID: 10320
		private static PermissionSet clipboardWrite;

		// Token: 0x04002851 RID: 10321
		private static CodeAccessPermission changeWindowRegionForTopLevel;

		// Token: 0x04002852 RID: 10322
		private static CodeAccessPermission controlFromHandleOrLocation;

		// Token: 0x04002853 RID: 10323
		private static CodeAccessPermission createAnyWindow;

		// Token: 0x04002854 RID: 10324
		private static CodeAccessPermission createGraphicsForControl;

		// Token: 0x04002855 RID: 10325
		private static CodeAccessPermission defaultPrinting;

		// Token: 0x04002856 RID: 10326
		private static CodeAccessPermission fileDialogCustomization;

		// Token: 0x04002857 RID: 10327
		private static CodeAccessPermission fileDialogOpenFile;

		// Token: 0x04002858 RID: 10328
		private static CodeAccessPermission fileDialogSaveFile;

		// Token: 0x04002859 RID: 10329
		private static CodeAccessPermission getCapture;

		// Token: 0x0400285A RID: 10330
		private static CodeAccessPermission getParent;

		// Token: 0x0400285B RID: 10331
		private static CodeAccessPermission manipulateWndProcAndHandles;

		// Token: 0x0400285C RID: 10332
		private static CodeAccessPermission modifyCursor;

		// Token: 0x0400285D RID: 10333
		private static CodeAccessPermission modifyFocus;

		// Token: 0x0400285E RID: 10334
		private static CodeAccessPermission objectFromWin32Handle;

		// Token: 0x0400285F RID: 10335
		private static CodeAccessPermission safePrinting;

		// Token: 0x04002860 RID: 10336
		private static CodeAccessPermission safeSubWindows;

		// Token: 0x04002861 RID: 10337
		private static CodeAccessPermission safeTopLevelWindows;

		// Token: 0x04002862 RID: 10338
		private static CodeAccessPermission sendMessages;

		// Token: 0x04002863 RID: 10339
		private static CodeAccessPermission sensitiveSystemInformation;

		// Token: 0x04002864 RID: 10340
		private static CodeAccessPermission transparentWindows;

		// Token: 0x04002865 RID: 10341
		private static CodeAccessPermission topLevelWindow;

		// Token: 0x04002866 RID: 10342
		private static CodeAccessPermission unmanagedCode;

		// Token: 0x04002867 RID: 10343
		private static CodeAccessPermission unrestrictedWindows;

		// Token: 0x04002868 RID: 10344
		private static CodeAccessPermission windowAdornmentModification;
	}
}
