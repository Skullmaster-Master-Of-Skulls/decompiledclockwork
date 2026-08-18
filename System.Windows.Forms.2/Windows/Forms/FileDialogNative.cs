using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000252 RID: 594
	internal static class FileDialogNative
	{
		// Token: 0x0200068D RID: 1677
		[Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
		[CoClass(typeof(FileDialogNative.FileOpenDialogRCW))]
		[ComImport]
		internal interface NativeFileOpenDialog : FileDialogNative.IFileOpenDialog, FileDialogNative.IFileDialog
		{
		}

		// Token: 0x0200068E RID: 1678
		[Guid("84bccd23-5fde-4cdb-aea4-af64b83d78ab")]
		[CoClass(typeof(FileDialogNative.FileSaveDialogRCW))]
		[ComImport]
		internal interface NativeFileSaveDialog : FileDialogNative.IFileSaveDialog, FileDialogNative.IFileDialog
		{
		}

		// Token: 0x0200068F RID: 1679
		[ClassInterface(ClassInterfaceType.None)]
		[TypeLibType(TypeLibTypeFlags.FCanCreate)]
		[Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
		[ComImport]
		internal class FileOpenDialogRCW
		{
			// Token: 0x06006776 RID: 26486
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern FileOpenDialogRCW();
		}

		// Token: 0x02000690 RID: 1680
		[ClassInterface(ClassInterfaceType.None)]
		[TypeLibType(TypeLibTypeFlags.FCanCreate)]
		[Guid("C0B4E2F3-BA21-4773-8DBA-335EC946EB8B")]
		[ComImport]
		internal class FileSaveDialogRCW
		{
			// Token: 0x06006777 RID: 26487
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern FileSaveDialogRCW();
		}

		// Token: 0x02000691 RID: 1681
		internal class IIDGuid
		{
			// Token: 0x06006778 RID: 26488 RVA: 0x00002843 File Offset: 0x00000A43
			private IIDGuid()
			{
			}

			// Token: 0x04003AA8 RID: 15016
			internal const string IModalWindow = "b4db1657-70d7-485e-8e3e-6fcb5a5c1802";

			// Token: 0x04003AA9 RID: 15017
			internal const string IFileDialog = "42f85136-db7e-439c-85f1-e4075d135fc8";

			// Token: 0x04003AAA RID: 15018
			internal const string IFileOpenDialog = "d57c7288-d4ad-4768-be02-9d969532d960";

			// Token: 0x04003AAB RID: 15019
			internal const string IFileSaveDialog = "84bccd23-5fde-4cdb-aea4-af64b83d78ab";

			// Token: 0x04003AAC RID: 15020
			internal const string IFileDialogEvents = "973510DB-7D7F-452B-8975-74A85828D354";

			// Token: 0x04003AAD RID: 15021
			internal const string IShellItem = "43826D1E-E718-42EE-BC55-A1E261C37BFE";

			// Token: 0x04003AAE RID: 15022
			internal const string IShellItemArray = "B63EA76D-1F85-456F-A19C-48159EFA858B";
		}

		// Token: 0x02000692 RID: 1682
		internal class CLSIDGuid
		{
			// Token: 0x06006779 RID: 26489 RVA: 0x00002843 File Offset: 0x00000A43
			private CLSIDGuid()
			{
			}

			// Token: 0x04003AAF RID: 15023
			internal const string FileOpenDialog = "DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7";

			// Token: 0x04003AB0 RID: 15024
			internal const string FileSaveDialog = "C0B4E2F3-BA21-4773-8DBA-335EC946EB8B";
		}

		// Token: 0x02000693 RID: 1683
		[Guid("b4db1657-70d7-485e-8e3e-6fcb5a5c1802")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IModalWindow
		{
			// Token: 0x0600677A RID: 26490
			[PreserveSig]
			int Show([In] IntPtr parent);
		}

		// Token: 0x02000694 RID: 1684
		internal enum SIATTRIBFLAGS
		{
			// Token: 0x04003AB2 RID: 15026
			SIATTRIBFLAGS_AND = 1,
			// Token: 0x04003AB3 RID: 15027
			SIATTRIBFLAGS_OR,
			// Token: 0x04003AB4 RID: 15028
			SIATTRIBFLAGS_APPCOMPAT
		}

		// Token: 0x02000695 RID: 1685
		[Guid("B63EA76D-1F85-456F-A19C-48159EFA858B")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IShellItemArray
		{
			// Token: 0x0600677B RID: 26491
			void BindToHandler([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pbc, [In] ref Guid rbhid, [In] ref Guid riid, out IntPtr ppvOut);

			// Token: 0x0600677C RID: 26492
			void GetPropertyStore([In] int Flags, [In] ref Guid riid, out IntPtr ppv);

			// Token: 0x0600677D RID: 26493
			void GetPropertyDescriptionList([In] ref FileDialogNative.PROPERTYKEY keyType, [In] ref Guid riid, out IntPtr ppv);

			// Token: 0x0600677E RID: 26494
			void GetAttributes([In] FileDialogNative.SIATTRIBFLAGS dwAttribFlags, [In] uint sfgaoMask, out uint psfgaoAttribs);

			// Token: 0x0600677F RID: 26495
			void GetCount(out uint pdwNumItems);

			// Token: 0x06006780 RID: 26496
			void GetItemAt([In] uint dwIndex, [MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x06006781 RID: 26497
			void EnumItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenumShellItems);
		}

		// Token: 0x02000696 RID: 1686
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PROPERTYKEY
		{
			// Token: 0x04003AB5 RID: 15029
			internal Guid fmtid;

			// Token: 0x04003AB6 RID: 15030
			internal uint pid;
		}

		// Token: 0x02000697 RID: 1687
		[Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IFileDialog
		{
			// Token: 0x06006782 RID: 26498
			[PreserveSig]
			int Show([In] IntPtr parent);

			// Token: 0x06006783 RID: 26499
			void SetFileTypes([In] uint cFileTypes, [MarshalAs(UnmanagedType.LPArray)] [In] FileDialogNative.COMDLG_FILTERSPEC[] rgFilterSpec);

			// Token: 0x06006784 RID: 26500
			void SetFileTypeIndex([In] uint iFileType);

			// Token: 0x06006785 RID: 26501
			void GetFileTypeIndex(out uint piFileType);

			// Token: 0x06006786 RID: 26502
			void Advise([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialogEvents pfde, out uint pdwCookie);

			// Token: 0x06006787 RID: 26503
			void Unadvise([In] uint dwCookie);

			// Token: 0x06006788 RID: 26504
			void SetOptions([In] FileDialogNative.FOS fos);

			// Token: 0x06006789 RID: 26505
			void GetOptions(out FileDialogNative.FOS pfos);

			// Token: 0x0600678A RID: 26506
			void SetDefaultFolder([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi);

			// Token: 0x0600678B RID: 26507
			void SetFolder([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi);

			// Token: 0x0600678C RID: 26508
			void GetFolder([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x0600678D RID: 26509
			void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x0600678E RID: 26510
			void SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszName);

			// Token: 0x0600678F RID: 26511
			void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

			// Token: 0x06006790 RID: 26512
			void SetTitle([MarshalAs(UnmanagedType.LPWStr)] [In] string pszTitle);

			// Token: 0x06006791 RID: 26513
			void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

			// Token: 0x06006792 RID: 26514
			void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

			// Token: 0x06006793 RID: 26515
			void GetResult([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x06006794 RID: 26516
			void AddPlace([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi, int alignment);

			// Token: 0x06006795 RID: 26517
			void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] [In] string pszDefaultExtension);

			// Token: 0x06006796 RID: 26518
			void Close([MarshalAs(UnmanagedType.Error)] int hr);

			// Token: 0x06006797 RID: 26519
			void SetClientGuid([In] ref Guid guid);

			// Token: 0x06006798 RID: 26520
			void ClearClientData();

			// Token: 0x06006799 RID: 26521
			void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);
		}

		// Token: 0x02000698 RID: 1688
		[Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IFileOpenDialog : FileDialogNative.IFileDialog
		{
			// Token: 0x0600679A RID: 26522
			[PreserveSig]
			int Show([In] IntPtr parent);

			// Token: 0x0600679B RID: 26523
			void SetFileTypes([In] uint cFileTypes, [In] ref FileDialogNative.COMDLG_FILTERSPEC rgFilterSpec);

			// Token: 0x0600679C RID: 26524
			void SetFileTypeIndex([In] uint iFileType);

			// Token: 0x0600679D RID: 26525
			void GetFileTypeIndex(out uint piFileType);

			// Token: 0x0600679E RID: 26526
			void Advise([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialogEvents pfde, out uint pdwCookie);

			// Token: 0x0600679F RID: 26527
			void Unadvise([In] uint dwCookie);

			// Token: 0x060067A0 RID: 26528
			void SetOptions([In] FileDialogNative.FOS fos);

			// Token: 0x060067A1 RID: 26529
			void GetOptions(out FileDialogNative.FOS pfos);

			// Token: 0x060067A2 RID: 26530
			void SetDefaultFolder([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi);

			// Token: 0x060067A3 RID: 26531
			void SetFolder([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi);

			// Token: 0x060067A4 RID: 26532
			void GetFolder([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x060067A5 RID: 26533
			void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x060067A6 RID: 26534
			void SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszName);

			// Token: 0x060067A7 RID: 26535
			void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

			// Token: 0x060067A8 RID: 26536
			void SetTitle([MarshalAs(UnmanagedType.LPWStr)] [In] string pszTitle);

			// Token: 0x060067A9 RID: 26537
			void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

			// Token: 0x060067AA RID: 26538
			void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

			// Token: 0x060067AB RID: 26539
			void GetResult([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x060067AC RID: 26540
			void AddPlace([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi, FileDialogCustomPlace fdcp);

			// Token: 0x060067AD RID: 26541
			void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] [In] string pszDefaultExtension);

			// Token: 0x060067AE RID: 26542
			void Close([MarshalAs(UnmanagedType.Error)] int hr);

			// Token: 0x060067AF RID: 26543
			void SetClientGuid([In] ref Guid guid);

			// Token: 0x060067B0 RID: 26544
			void ClearClientData();

			// Token: 0x060067B1 RID: 26545
			void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

			// Token: 0x060067B2 RID: 26546
			void GetResults([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItemArray ppenum);

			// Token: 0x060067B3 RID: 26547
			void GetSelectedItems([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItemArray ppsai);
		}

		// Token: 0x02000699 RID: 1689
		[Guid("84bccd23-5fde-4cdb-aea4-af64b83d78ab")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IFileSaveDialog : FileDialogNative.IFileDialog
		{
			// Token: 0x060067B4 RID: 26548
			[PreserveSig]
			int Show([In] IntPtr parent);

			// Token: 0x060067B5 RID: 26549
			void SetFileTypes([In] uint cFileTypes, [In] ref FileDialogNative.COMDLG_FILTERSPEC rgFilterSpec);

			// Token: 0x060067B6 RID: 26550
			void SetFileTypeIndex([In] uint iFileType);

			// Token: 0x060067B7 RID: 26551
			void GetFileTypeIndex(out uint piFileType);

			// Token: 0x060067B8 RID: 26552
			void Advise([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialogEvents pfde, out uint pdwCookie);

			// Token: 0x060067B9 RID: 26553
			void Unadvise([In] uint dwCookie);

			// Token: 0x060067BA RID: 26554
			void SetOptions([In] FileDialogNative.FOS fos);

			// Token: 0x060067BB RID: 26555
			void GetOptions(out FileDialogNative.FOS pfos);

			// Token: 0x060067BC RID: 26556
			void SetDefaultFolder([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi);

			// Token: 0x060067BD RID: 26557
			void SetFolder([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi);

			// Token: 0x060067BE RID: 26558
			void GetFolder([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x060067BF RID: 26559
			void GetCurrentSelection([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x060067C0 RID: 26560
			void SetFileName([MarshalAs(UnmanagedType.LPWStr)] [In] string pszName);

			// Token: 0x060067C1 RID: 26561
			void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);

			// Token: 0x060067C2 RID: 26562
			void SetTitle([MarshalAs(UnmanagedType.LPWStr)] [In] string pszTitle);

			// Token: 0x060067C3 RID: 26563
			void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszText);

			// Token: 0x060067C4 RID: 26564
			void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] [In] string pszLabel);

			// Token: 0x060067C5 RID: 26565
			void GetResult([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x060067C6 RID: 26566
			void AddPlace([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi, FileDialogCustomPlace fdcp);

			// Token: 0x060067C7 RID: 26567
			void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] [In] string pszDefaultExtension);

			// Token: 0x060067C8 RID: 26568
			void Close([MarshalAs(UnmanagedType.Error)] int hr);

			// Token: 0x060067C9 RID: 26569
			void SetClientGuid([In] ref Guid guid);

			// Token: 0x060067CA RID: 26570
			void ClearClientData();

			// Token: 0x060067CB RID: 26571
			void SetFilter([MarshalAs(UnmanagedType.Interface)] IntPtr pFilter);

			// Token: 0x060067CC RID: 26572
			void SetSaveAsItem([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi);

			// Token: 0x060067CD RID: 26573
			void SetProperties([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pStore);

			// Token: 0x060067CE RID: 26574
			void SetCollectedProperties([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pList, [In] int fAppendDefault);

			// Token: 0x060067CF RID: 26575
			void GetProperties([MarshalAs(UnmanagedType.Interface)] out IntPtr ppStore);

			// Token: 0x060067D0 RID: 26576
			void ApplyProperties([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi, [MarshalAs(UnmanagedType.Interface)] [In] IntPtr pStore, [ComAliasName("ShellObjects.wireHWND")] [In] ref IntPtr hwnd, [MarshalAs(UnmanagedType.Interface)] [In] IntPtr pSink);
		}

		// Token: 0x0200069A RID: 1690
		[Guid("973510DB-7D7F-452B-8975-74A85828D354")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IFileDialogEvents
		{
			// Token: 0x060067D1 RID: 26577
			[PreserveSig]
			int OnFileOk([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialog pfd);

			// Token: 0x060067D2 RID: 26578
			[PreserveSig]
			int OnFolderChanging([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialog pfd, [MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psiFolder);

			// Token: 0x060067D3 RID: 26579
			void OnFolderChange([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialog pfd);

			// Token: 0x060067D4 RID: 26580
			void OnSelectionChange([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialog pfd);

			// Token: 0x060067D5 RID: 26581
			void OnShareViolation([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialog pfd, [MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi, out FileDialogNative.FDE_SHAREVIOLATION_RESPONSE pResponse);

			// Token: 0x060067D6 RID: 26582
			void OnTypeChange([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialog pfd);

			// Token: 0x060067D7 RID: 26583
			void OnOverwrite([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IFileDialog pfd, [MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi, out FileDialogNative.FDE_OVERWRITE_RESPONSE pResponse);
		}

		// Token: 0x0200069B RID: 1691
		[Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IShellItem
		{
			// Token: 0x060067D8 RID: 26584
			void BindToHandler([MarshalAs(UnmanagedType.Interface)] [In] IntPtr pbc, [In] ref Guid bhid, [In] ref Guid riid, out IntPtr ppv);

			// Token: 0x060067D9 RID: 26585
			void GetParent([MarshalAs(UnmanagedType.Interface)] out FileDialogNative.IShellItem ppsi);

			// Token: 0x060067DA RID: 26586
			void GetDisplayName([In] FileDialogNative.SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

			// Token: 0x060067DB RID: 26587
			void GetAttributes([In] uint sfgaoMask, out uint psfgaoAttribs);

			// Token: 0x060067DC RID: 26588
			void Compare([MarshalAs(UnmanagedType.Interface)] [In] FileDialogNative.IShellItem psi, [In] uint hint, out int piOrder);
		}

		// Token: 0x0200069C RID: 1692
		internal enum SIGDN : uint
		{
			// Token: 0x04003AB8 RID: 15032
			SIGDN_NORMALDISPLAY,
			// Token: 0x04003AB9 RID: 15033
			SIGDN_PARENTRELATIVEPARSING = 2147581953U,
			// Token: 0x04003ABA RID: 15034
			SIGDN_DESKTOPABSOLUTEPARSING = 2147647488U,
			// Token: 0x04003ABB RID: 15035
			SIGDN_PARENTRELATIVEEDITING = 2147684353U,
			// Token: 0x04003ABC RID: 15036
			SIGDN_DESKTOPABSOLUTEEDITING = 2147794944U,
			// Token: 0x04003ABD RID: 15037
			SIGDN_FILESYSPATH = 2147844096U,
			// Token: 0x04003ABE RID: 15038
			SIGDN_URL = 2147909632U,
			// Token: 0x04003ABF RID: 15039
			SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553U,
			// Token: 0x04003AC0 RID: 15040
			SIGDN_PARENTRELATIVE = 2148007937U
		}

		// Token: 0x0200069D RID: 1693
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
		internal struct COMDLG_FILTERSPEC
		{
			// Token: 0x04003AC1 RID: 15041
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszName;

			// Token: 0x04003AC2 RID: 15042
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string pszSpec;
		}

		// Token: 0x0200069E RID: 1694
		[Flags]
		internal enum FOS : uint
		{
			// Token: 0x04003AC4 RID: 15044
			FOS_OVERWRITEPROMPT = 2U,
			// Token: 0x04003AC5 RID: 15045
			FOS_STRICTFILETYPES = 4U,
			// Token: 0x04003AC6 RID: 15046
			FOS_NOCHANGEDIR = 8U,
			// Token: 0x04003AC7 RID: 15047
			FOS_PICKFOLDERS = 32U,
			// Token: 0x04003AC8 RID: 15048
			FOS_FORCEFILESYSTEM = 64U,
			// Token: 0x04003AC9 RID: 15049
			FOS_ALLNONSTORAGEITEMS = 128U,
			// Token: 0x04003ACA RID: 15050
			FOS_NOVALIDATE = 256U,
			// Token: 0x04003ACB RID: 15051
			FOS_ALLOWMULTISELECT = 512U,
			// Token: 0x04003ACC RID: 15052
			FOS_PATHMUSTEXIST = 2048U,
			// Token: 0x04003ACD RID: 15053
			FOS_FILEMUSTEXIST = 4096U,
			// Token: 0x04003ACE RID: 15054
			FOS_CREATEPROMPT = 8192U,
			// Token: 0x04003ACF RID: 15055
			FOS_SHAREAWARE = 16384U,
			// Token: 0x04003AD0 RID: 15056
			FOS_NOREADONLYRETURN = 32768U,
			// Token: 0x04003AD1 RID: 15057
			FOS_NOTESTFILECREATE = 65536U,
			// Token: 0x04003AD2 RID: 15058
			FOS_HIDEMRUPLACES = 131072U,
			// Token: 0x04003AD3 RID: 15059
			FOS_HIDEPINNEDPLACES = 262144U,
			// Token: 0x04003AD4 RID: 15060
			FOS_NODEREFERENCELINKS = 1048576U,
			// Token: 0x04003AD5 RID: 15061
			FOS_DONTADDTORECENT = 33554432U,
			// Token: 0x04003AD6 RID: 15062
			FOS_FORCESHOWHIDDEN = 268435456U,
			// Token: 0x04003AD7 RID: 15063
			FOS_DEFAULTNOMINIMODE = 536870912U
		}

		// Token: 0x0200069F RID: 1695
		internal enum FDE_SHAREVIOLATION_RESPONSE
		{
			// Token: 0x04003AD9 RID: 15065
			FDESVR_DEFAULT,
			// Token: 0x04003ADA RID: 15066
			FDESVR_ACCEPT,
			// Token: 0x04003ADB RID: 15067
			FDESVR_REFUSE
		}

		// Token: 0x020006A0 RID: 1696
		internal enum FDE_OVERWRITE_RESPONSE
		{
			// Token: 0x04003ADD RID: 15069
			FDEOR_DEFAULT,
			// Token: 0x04003ADE RID: 15070
			FDEOR_ACCEPT,
			// Token: 0x04003ADF RID: 15071
			FDEOR_REFUSE
		}
	}
}
