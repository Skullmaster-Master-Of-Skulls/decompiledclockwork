using System;
using System.Internal;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;

namespace System.Design
{
	// Token: 0x02000283 RID: 643
	[SuppressUnmanagedCodeSecurity]
	internal class UnsafeNativeMethods
	{
		// Token: 0x06001886 RID: 6278
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int ClientToScreen(HandleRef hWnd, [In] [Out] NativeMethods.POINT pt);

		// Token: 0x06001887 RID: 6279
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

		// Token: 0x06001888 RID: 6280
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hwnd, int msg, bool wparam, int lparam);

		// Token: 0x06001889 RID: 6281
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

		// Token: 0x0600188A RID: 6282
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetMessageTime();

		// Token: 0x0600188B RID: 6283
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetActiveWindow(HandleRef hWnd);

		// Token: 0x0600188C RID: 6284
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern void NotifyWinEvent(int winEvent, HandleRef hwnd, int objType, int objID);

		// Token: 0x0600188D RID: 6285
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetFocus(HandleRef hWnd);

		// Token: 0x0600188E RID: 6286
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetFocus();

		// Token: 0x0600188F RID: 6287
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsChild(HandleRef hWndParent, HandleRef hwnd);

		// Token: 0x06001890 RID: 6288
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

		// Token: 0x06001891 RID: 6289
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MsgWaitForMultipleObjectsEx(int nCount, IntPtr pHandles, int dwMilliseconds, int dwWakeMask, int dwFlags);

		// Token: 0x06001892 RID: 6290
		[DllImport("ole32.dll")]
		public static extern int ReadClassStg(HandleRef pStg, [In] [Out] ref Guid pclsid);

		// Token: 0x06001893 RID: 6291
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetStockObject(int nIndex);

		// Token: 0x06001894 RID: 6292
		[DllImport("oleacc.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr LresultFromObject(ref Guid refiid, IntPtr wParam, IntPtr pAcc);

		// Token: 0x06001895 RID: 6293
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr BeginPaint(IntPtr hWnd, [In] [Out] ref UnsafeNativeMethods.PAINTSTRUCT lpPaint);

		// Token: 0x06001896 RID: 6294
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool EndPaint(IntPtr hWnd, ref UnsafeNativeMethods.PAINTSTRUCT lpPaint);

		// Token: 0x06001897 RID: 6295
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true)]
		private static extern IntPtr IntGetDC(HandleRef hWnd);

		// Token: 0x06001898 RID: 6296 RVA: 0x0008B08C File Offset: 0x0008928C
		public static IntPtr GetDC(HandleRef hWnd)
		{
			return System.Internal.HandleCollector.Add(UnsafeNativeMethods.IntGetDC(hWnd), NativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06001899 RID: 6297
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true)]
		private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

		// Token: 0x0600189A RID: 6298 RVA: 0x0008B09E File Offset: 0x0008929E
		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, NativeMethods.CommonHandles.HDC);
			return UnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
		}

		// Token: 0x0600189B RID: 6299
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

		// Token: 0x0600189C RID: 6300
		[DllImport("shell32.dll")]
		public static extern IntPtr ExtractIcon(HandleRef hMod, string exeName, int index);

		// Token: 0x0600189D RID: 6301
		[DllImport("user32.dll")]
		public static extern bool DestroyIcon(HandleRef hIcon);

		// Token: 0x0600189E RID: 6302
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SetWindowsHookEx(int hookid, UnsafeNativeMethods.HookProc pfnhook, HandleRef hinst, int threadid);

		// Token: 0x0600189F RID: 6303 RVA: 0x0008B0B8 File Offset: 0x000892B8
		public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.GetWindowLong32(hWnd, nIndex);
			}
			return UnsafeNativeMethods.GetWindowLongPtr64(hWnd, nIndex);
		}

		// Token: 0x060018A0 RID: 6304
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
		public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

		// Token: 0x060018A1 RID: 6305
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLongPtr")]
		public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

		// Token: 0x060018A2 RID: 6306 RVA: 0x0008B0D1 File Offset: 0x000892D1
		public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
		{
			if (IntPtr.Size == 4)
			{
				return UnsafeNativeMethods.SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
			}
			return UnsafeNativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
		}

		// Token: 0x060018A3 RID: 6307
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
		public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

		// Token: 0x060018A4 RID: 6308
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
		public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

		// Token: 0x060018A5 RID: 6309
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool UnhookWindowsHookEx(HandleRef hhook);

		// Token: 0x060018A6 RID: 6310
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);

		// Token: 0x060018A7 RID: 6311
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr CallNextHookEx(HandleRef hhook, int code, IntPtr wparam, IntPtr lparam);

		// Token: 0x060018A8 RID: 6312
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern UnsafeNativeMethods.ILockBytes CreateILockBytesOnHGlobal(HandleRef hGlobal, bool fDeleteOnRelease);

		// Token: 0x060018A9 RID: 6313
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern UnsafeNativeMethods.IStorage StgCreateDocfileOnILockBytes(UnsafeNativeMethods.ILockBytes iLockBytes, int grfMode, int reserved);

		// Token: 0x0200050F RID: 1295
		[Flags]
		public enum BrowseInfos
		{
			// Token: 0x04002063 RID: 8291
			ReturnOnlyFSDirs = 1,
			// Token: 0x04002064 RID: 8292
			DontGoBelowDomain = 2,
			// Token: 0x04002065 RID: 8293
			StatusText = 4,
			// Token: 0x04002066 RID: 8294
			ReturnFSAncestors = 8,
			// Token: 0x04002067 RID: 8295
			EditBox = 16,
			// Token: 0x04002068 RID: 8296
			Validate = 32,
			// Token: 0x04002069 RID: 8297
			NewDialogStyle = 64,
			// Token: 0x0400206A RID: 8298
			UseNewUI = 80,
			// Token: 0x0400206B RID: 8299
			AllowUrls = 128,
			// Token: 0x0400206C RID: 8300
			BrowseForComputer = 4096,
			// Token: 0x0400206D RID: 8301
			BrowseForPrinter = 8192,
			// Token: 0x0400206E RID: 8302
			BrowseForEverything = 16384,
			// Token: 0x0400206F RID: 8303
			ShowShares = 32768
		}

		// Token: 0x02000510 RID: 1296
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class BROWSEINFO
		{
			// Token: 0x04002070 RID: 8304
			public IntPtr hwndOwner;

			// Token: 0x04002071 RID: 8305
			public IntPtr pidlRoot;

			// Token: 0x04002072 RID: 8306
			public IntPtr pszDisplayName;

			// Token: 0x04002073 RID: 8307
			public string lpszTitle;

			// Token: 0x04002074 RID: 8308
			public int ulFlags;

			// Token: 0x04002075 RID: 8309
			public IntPtr lpfn;

			// Token: 0x04002076 RID: 8310
			public IntPtr lParam;

			// Token: 0x04002077 RID: 8311
			public int iImage;
		}

		// Token: 0x02000511 RID: 1297
		public class Shell32
		{
			// Token: 0x06002FC0 RID: 12224
			[DllImport("shell32.dll")]
			public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);

			// Token: 0x06002FC1 RID: 12225
			[DllImport("shell32.dll", CharSet = CharSet.Auto)]
			public static extern bool SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);

			// Token: 0x06002FC2 RID: 12226
			[DllImport("shell32.dll", CharSet = CharSet.Auto)]
			public static extern IntPtr SHBrowseForFolder([In] UnsafeNativeMethods.BROWSEINFO lpbi);

			// Token: 0x06002FC3 RID: 12227
			[DllImport("shell32.dll")]
			public static extern int SHGetMalloc([MarshalAs(UnmanagedType.LPArray)] [Out] UnsafeNativeMethods.IMalloc[] ppMalloc);
		}

		// Token: 0x02000512 RID: 1298
		[Guid("00000002-0000-0000-c000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IMalloc
		{
			// Token: 0x06002FC5 RID: 12229
			[PreserveSig]
			IntPtr Alloc(int cb);

			// Token: 0x06002FC6 RID: 12230
			[PreserveSig]
			IntPtr Realloc(IntPtr pv, int cb);

			// Token: 0x06002FC7 RID: 12231
			[PreserveSig]
			void Free(IntPtr pv);

			// Token: 0x06002FC8 RID: 12232
			[PreserveSig]
			int GetSize(IntPtr pv);

			// Token: 0x06002FC9 RID: 12233
			[PreserveSig]
			int DidAlloc(IntPtr pv);

			// Token: 0x06002FCA RID: 12234
			[PreserveSig]
			void HeapMinimize();
		}

		// Token: 0x02000513 RID: 1299
		public struct PAINTSTRUCT
		{
			// Token: 0x04002078 RID: 8312
			public IntPtr hdc;

			// Token: 0x04002079 RID: 8313
			public bool fErase;

			// Token: 0x0400207A RID: 8314
			public int rcPaint_left;

			// Token: 0x0400207B RID: 8315
			public int rcPaint_top;

			// Token: 0x0400207C RID: 8316
			public int rcPaint_right;

			// Token: 0x0400207D RID: 8317
			public int rcPaint_bottom;

			// Token: 0x0400207E RID: 8318
			public bool fRestore;

			// Token: 0x0400207F RID: 8319
			public bool fIncUpdate;

			// Token: 0x04002080 RID: 8320
			public int reserved1;

			// Token: 0x04002081 RID: 8321
			public int reserved2;

			// Token: 0x04002082 RID: 8322
			public int reserved3;

			// Token: 0x04002083 RID: 8323
			public int reserved4;

			// Token: 0x04002084 RID: 8324
			public int reserved5;

			// Token: 0x04002085 RID: 8325
			public int reserved6;

			// Token: 0x04002086 RID: 8326
			public int reserved7;

			// Token: 0x04002087 RID: 8327
			public int reserved8;
		}

		// Token: 0x02000514 RID: 1300
		// (Invoke) Token: 0x06002FCC RID: 12236
		public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000515 RID: 1301
		[Guid("00020D03-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IRichEditOleCallback
		{
		}

		// Token: 0x02000516 RID: 1302
		[Guid("00020D03-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IRichTextBoxOleCallback
		{
			// Token: 0x06002FCF RID: 12239
			[PreserveSig]
			int GetNewStorage(out UnsafeNativeMethods.IStorage ret);

			// Token: 0x06002FD0 RID: 12240
			[PreserveSig]
			int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo);

			// Token: 0x06002FD1 RID: 12241
			[PreserveSig]
			int ShowContainerUI(int fShow);

			// Token: 0x06002FD2 RID: 12242
			[PreserveSig]
			int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp);

			// Token: 0x06002FD3 RID: 12243
			[PreserveSig]
			int DeleteObject(IntPtr lpoleobj);

			// Token: 0x06002FD4 RID: 12244
			[PreserveSig]
			int QueryAcceptData(IDataObject lpdataobj, IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict);

			// Token: 0x06002FD5 RID: 12245
			[PreserveSig]
			int ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06002FD6 RID: 12246
			[PreserveSig]
			int GetClipboardData(NativeMethods.CHARRANGE lpchrg, int reco, IntPtr lplpdataobj);

			// Token: 0x06002FD7 RID: 12247
			[PreserveSig]
			int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect);

			// Token: 0x06002FD8 RID: 12248
			[PreserveSig]
			int GetContextMenu(short seltype, IntPtr lpoleobj, NativeMethods.CHARRANGE lpchrg, out IntPtr hmenu);
		}

		// Token: 0x02000517 RID: 1303
		[Guid("0000000B-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStorage
		{
			// Token: 0x06002FD9 RID: 12249
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream CreateStream([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

			// Token: 0x06002FDA RID: 12250
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream OpenStream([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, IntPtr reserved1, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

			// Token: 0x06002FDB RID: 12251
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStorage CreateStorage([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, [MarshalAs(UnmanagedType.U4)] [In] int reserved1, [MarshalAs(UnmanagedType.U4)] [In] int reserved2);

			// Token: 0x06002FDC RID: 12252
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStorage OpenStorage([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, IntPtr pstgPriority, [MarshalAs(UnmanagedType.U4)] [In] int grfMode, IntPtr snbExclude, [MarshalAs(UnmanagedType.U4)] [In] int reserved);

			// Token: 0x06002FDD RID: 12253
			void CopyTo(int ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] pIIDExclude, IntPtr snbExclude, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStorage stgDest);

			// Token: 0x06002FDE RID: 12254
			void MoveElementTo([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStorage stgDest, [MarshalAs(UnmanagedType.BStr)] [In] string pwcsNewName, [MarshalAs(UnmanagedType.U4)] [In] int grfFlags);

			// Token: 0x06002FDF RID: 12255
			void Commit(int grfCommitFlags);

			// Token: 0x06002FE0 RID: 12256
			void Revert();

			// Token: 0x06002FE1 RID: 12257
			void EnumElements([MarshalAs(UnmanagedType.U4)] [In] int reserved1, IntPtr reserved2, [MarshalAs(UnmanagedType.U4)] [In] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);

			// Token: 0x06002FE2 RID: 12258
			void DestroyElement([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName);

			// Token: 0x06002FE3 RID: 12259
			void RenameElement([MarshalAs(UnmanagedType.BStr)] [In] string pwcsOldName, [MarshalAs(UnmanagedType.BStr)] [In] string pwcsNewName);

			// Token: 0x06002FE4 RID: 12260
			void SetElementTimes([MarshalAs(UnmanagedType.BStr)] [In] string pwcsName, [In] NativeMethods.FILETIME pctime, [In] NativeMethods.FILETIME patime, [In] NativeMethods.FILETIME pmtime);

			// Token: 0x06002FE5 RID: 12261
			void SetClass([In] ref Guid clsid);

			// Token: 0x06002FE6 RID: 12262
			void SetStateBits(int grfStateBits, int grfMask);

			// Token: 0x06002FE7 RID: 12263
			void Stat([Out] NativeMethods.STATSTG pStatStg, int grfStatFlag);
		}

		// Token: 0x02000518 RID: 1304
		[SuppressUnmanagedCodeSecurity]
		[Guid("0000000C-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStream
		{
			// Token: 0x06002FE8 RID: 12264
			int Read(IntPtr buf, int len);

			// Token: 0x06002FE9 RID: 12265
			int Write(IntPtr buf, int len);

			// Token: 0x06002FEA RID: 12266
			[return: MarshalAs(UnmanagedType.I8)]
			long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, int dwOrigin);

			// Token: 0x06002FEB RID: 12267
			void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);

			// Token: 0x06002FEC RID: 12268
			[return: MarshalAs(UnmanagedType.I8)]
			long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] UnsafeNativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);

			// Token: 0x06002FED RID: 12269
			void Commit(int grfCommitFlags);

			// Token: 0x06002FEE RID: 12270
			void Revert();

			// Token: 0x06002FEF RID: 12271
			void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, int dwLockType);

			// Token: 0x06002FF0 RID: 12272
			void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, int dwLockType);

			// Token: 0x06002FF1 RID: 12273
			void Stat([Out] NativeMethods.STATSTG pStatstg, int grfStatFlag);

			// Token: 0x06002FF2 RID: 12274
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IStream Clone();
		}

		// Token: 0x02000519 RID: 1305
		[Guid("0000000A-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ILockBytes
		{
			// Token: 0x06002FF3 RID: 12275
			void ReadAt([MarshalAs(UnmanagedType.U8)] [In] long ulOffset, [Out] IntPtr pv, [MarshalAs(UnmanagedType.U4)] [In] int cb, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pcbRead);

			// Token: 0x06002FF4 RID: 12276
			void WriteAt([MarshalAs(UnmanagedType.U8)] [In] long ulOffset, IntPtr pv, [MarshalAs(UnmanagedType.U4)] [In] int cb, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pcbWritten);

			// Token: 0x06002FF5 RID: 12277
			void Flush();

			// Token: 0x06002FF6 RID: 12278
			void SetSize([MarshalAs(UnmanagedType.U8)] [In] long cb);

			// Token: 0x06002FF7 RID: 12279
			void LockRegion([MarshalAs(UnmanagedType.U8)] [In] long libOffset, [MarshalAs(UnmanagedType.U8)] [In] long cb, [MarshalAs(UnmanagedType.U4)] [In] int dwLockType);

			// Token: 0x06002FF8 RID: 12280
			void UnlockRegion([MarshalAs(UnmanagedType.U8)] [In] long libOffset, [MarshalAs(UnmanagedType.U8)] [In] long cb, [MarshalAs(UnmanagedType.U4)] [In] int dwLockType);

			// Token: 0x06002FF9 RID: 12281
			void Stat([Out] NativeMethods.STATSTG pstatstg, [MarshalAs(UnmanagedType.U4)] [In] int grfStatFlag);
		}
	}
}
