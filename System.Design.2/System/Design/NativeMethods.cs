using System;
using System.Drawing;
using System.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Design
{
	// Token: 0x02000281 RID: 641
	internal static class NativeMethods
	{
		// Token: 0x06001849 RID: 6217
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int MultiByteToWideChar(int CodePage, int dwFlags, byte[] lpMultiByteStr, int cchMultiByte, char[] lpWideCharStr, int cchWideChar);

		// Token: 0x0600184A RID: 6218
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetClientRect(IntPtr hWnd, [In] [Out] NativeMethods.COMRECT rect);

		// Token: 0x0600184B RID: 6219
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool PeekMessage([In] [Out] ref NativeMethods.MSG msg, IntPtr hwnd, int msgMin, int msgMax, int remove);

		// Token: 0x0600184C RID: 6220
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetCursor();

		// Token: 0x0600184D RID: 6221
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetCursorPos([In] [Out] NativeMethods.POINT pt);

		// Token: 0x0600184E RID: 6222
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr WindowFromPoint(int x, int y);

		// Token: 0x0600184F RID: 6223
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06001850 RID: 6224
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [In] [Out] NativeMethods.HDHITTESTINFO lParam);

		// Token: 0x06001851 RID: 6225
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

		// Token: 0x06001852 RID: 6226
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

		// Token: 0x06001853 RID: 6227
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [In] [Out] NativeMethods.TV_HITTESTINFO lParam);

		// Token: 0x06001854 RID: 6228
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06001855 RID: 6229
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

		// Token: 0x06001856 RID: 6230
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern short GetKeyState(int keyCode);

		// Token: 0x06001857 RID: 6231
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In] [Out] ref NativeMethods.RECT rect, int cPoints);

		// Token: 0x06001858 RID: 6232
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In] [Out] NativeMethods.POINT pt, int cPoints);

		// Token: 0x06001859 RID: 6233
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ValidateRect(IntPtr hwnd, IntPtr prect);

		// Token: 0x0600185A RID: 6234
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true)]
		private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

		// Token: 0x0600185B RID: 6235 RVA: 0x0008AFDB File Offset: 0x000891DB
		public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
		{
			return System.Internal.HandleCollector.Add(NativeMethods.IntCreateRectRgn(x1, y1, x2, y2), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600185C RID: 6236
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetUpdateRect(IntPtr hwnd, [In] [Out] ref NativeMethods.RECT rc, bool fErase);

		// Token: 0x0600185D RID: 6237
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetUpdateRgn(IntPtr hwnd, IntPtr hrgn, bool fErase);

		// Token: 0x0600185E RID: 6238
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true)]
		public static extern bool ExternalDeleteObject(HandleRef hObject);

		// Token: 0x0600185F RID: 6239
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true)]
		private static extern bool IntDeleteObject(IntPtr hObject);

		// Token: 0x06001860 RID: 6240 RVA: 0x0008AFF0 File Offset: 0x000891F0
		public static bool DeleteObject(IntPtr hObject)
		{
			System.Internal.HandleCollector.Remove(hObject, NativeMethods.CommonHandles.GDI);
			return NativeMethods.IntDeleteObject(hObject);
		}

		// Token: 0x06001861 RID: 6241
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

		// Token: 0x06001862 RID: 6242
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool TranslateMessage([In] [Out] ref NativeMethods.MSG msg);

		// Token: 0x06001863 RID: 6243
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int DispatchMessage([In] ref NativeMethods.MSG msg);

		// Token: 0x06001864 RID: 6244
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetWindowRect(IntPtr hWnd, [In] [Out] ref NativeMethods.RECT rect);

		// Token: 0x06001865 RID: 6245
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int RevokeDragDrop(IntPtr hwnd);

		// Token: 0x06001866 RID: 6246
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr ChildWindowFromPointEx(IntPtr hwndParent, int x, int y, int uFlags);

		// Token: 0x06001867 RID: 6247
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		// Token: 0x06001868 RID: 6248
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetFocus();

		// Token: 0x06001869 RID: 6249 RVA: 0x0008B004 File Offset: 0x00089204
		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0008B00D File Offset: 0x0008920D
		public static bool Failed(int hr)
		{
			return hr < 0;
		}

		// Token: 0x0600186B RID: 6251
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern ITypeLib LoadRegTypeLib(ref Guid clsid, short majorVersion, short minorVersion, int lcid);

		// Token: 0x0600186C RID: 6252
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern ITypeLib LoadTypeLib([MarshalAs(UnmanagedType.LPWStr)] [In] string typelib);

		// Token: 0x0600186D RID: 6253
		[DllImport("oleaut32.dll", PreserveSig = false)]
		[return: MarshalAs(UnmanagedType.BStr)]
		public static extern string QueryPathOfRegTypeLib(ref Guid guid, short majorVersion, short minorVersion, int lcid);

		// Token: 0x04000CA7 RID: 3239
		public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

		// Token: 0x04000CA8 RID: 3240
		public static int PS_SOLID = 0;

		// Token: 0x04000CA9 RID: 3241
		public const int HOLLOW_BRUSH = 5;

		// Token: 0x04000CAA RID: 3242
		public const int WM_USER = 1024;

		// Token: 0x04000CAB RID: 3243
		public const int WM_CLOSE = 16;

		// Token: 0x04000CAC RID: 3244
		public const int WM_GETDLGCODE = 135;

		// Token: 0x04000CAD RID: 3245
		public const int WM_MOUSEMOVE = 512;

		// Token: 0x04000CAE RID: 3246
		public const int WM_NOTIFY = 78;

		// Token: 0x04000CAF RID: 3247
		public const int DLGC_WANTALLKEYS = 4;

		// Token: 0x04000CB0 RID: 3248
		public const int NM_CLICK = -2;

		// Token: 0x04000CB1 RID: 3249
		public const int WM_REFLECT = 8192;

		// Token: 0x04000CB2 RID: 3250
		public const int BM_SETIMAGE = 247;

		// Token: 0x04000CB3 RID: 3251
		public const int IMAGE_ICON = 1;

		// Token: 0x04000CB4 RID: 3252
		public const int WM_DESTROY = 2;

		// Token: 0x04000CB5 RID: 3253
		public const int BS_ICON = 64;

		// Token: 0x04000CB6 RID: 3254
		public const int VK_PROCESSKEY = 229;

		// Token: 0x04000CB7 RID: 3255
		public const int STGM_READ = 0;

		// Token: 0x04000CB8 RID: 3256
		public const int STGM_WRITE = 1;

		// Token: 0x04000CB9 RID: 3257
		public const int STGM_READWRITE = 2;

		// Token: 0x04000CBA RID: 3258
		public const int STGM_SHARE_EXCLUSIVE = 16;

		// Token: 0x04000CBB RID: 3259
		public const int STGM_CREATE = 4096;

		// Token: 0x04000CBC RID: 3260
		public const int STGM_TRANSACTED = 65536;

		// Token: 0x04000CBD RID: 3261
		public const int STGM_CONVERT = 131072;

		// Token: 0x04000CBE RID: 3262
		public const int STGM_DELETEONRELEASE = 67108864;

		// Token: 0x04000CBF RID: 3263
		public const int RECO_PASTE = 0;

		// Token: 0x04000CC0 RID: 3264
		public const int RECO_DROP = 1;

		// Token: 0x04000CC1 RID: 3265
		public const int LOGPIXELSX = 88;

		// Token: 0x04000CC2 RID: 3266
		public const int LOGPIXELSY = 90;

		// Token: 0x04000CC3 RID: 3267
		public const int TCM_HITTEST = 4877;

		// Token: 0x04000CC4 RID: 3268
		public static IntPtr InvalidIntPtr = (IntPtr)(-1);

		// Token: 0x04000CC5 RID: 3269
		public const int S_OK = 0;

		// Token: 0x04000CC6 RID: 3270
		public const int S_FALSE = 1;

		// Token: 0x04000CC7 RID: 3271
		public const int E_NOTIMPL = -2147467263;

		// Token: 0x04000CC8 RID: 3272
		public const int E_NOINTERFACE = -2147467262;

		// Token: 0x04000CC9 RID: 3273
		public const int E_INVALIDARG = -2147024809;

		// Token: 0x04000CCA RID: 3274
		public const int E_FAIL = -2147467259;

		// Token: 0x04000CCB RID: 3275
		public const int WS_EX_STATICEDGE = 131072;

		// Token: 0x04000CCC RID: 3276
		public static int TME_HOVER = 1;

		// Token: 0x04000CCD RID: 3277
		public const int OLEIVERB_PRIMARY = 0;

		// Token: 0x04000CCE RID: 3278
		public const int OLEIVERB_SHOW = -1;

		// Token: 0x04000CCF RID: 3279
		public const int OLEIVERB_OPEN = -2;

		// Token: 0x04000CD0 RID: 3280
		public const int OLEIVERB_HIDE = -3;

		// Token: 0x04000CD1 RID: 3281
		public const int OLEIVERB_UIACTIVATE = -4;

		// Token: 0x04000CD2 RID: 3282
		public const int OLEIVERB_INPLACEACTIVATE = -5;

		// Token: 0x04000CD3 RID: 3283
		public const int OLEIVERB_DISCARDUNDOSTATE = -6;

		// Token: 0x04000CD4 RID: 3284
		public const int OLEIVERB_PROPERTIES = -7;

		// Token: 0x04000CD5 RID: 3285
		public const int OLECLOSE_SAVEIFDIRTY = 0;

		// Token: 0x04000CD6 RID: 3286
		public const int OLECLOSE_NOSAVE = 1;

		// Token: 0x04000CD7 RID: 3287
		public const int OLECLOSE_PROMPTSAVE = 2;

		// Token: 0x04000CD8 RID: 3288
		public const int PM_NOREMOVE = 0;

		// Token: 0x04000CD9 RID: 3289
		public const int PM_REMOVE = 1;

		// Token: 0x04000CDA RID: 3290
		public const int WM_CHAR = 258;

		// Token: 0x04000CDB RID: 3291
		public static readonly int WM_MOUSEENTER = NativeMethods.Util.RegisterWindowMessage("WinFormsMouseEnter");

		// Token: 0x04000CDC RID: 3292
		public static readonly int HDN_ENDTRACK = (Marshal.SystemDefaultCharSize == 1) ? -307 : -327;

		// Token: 0x04000CDD RID: 3293
		public const int DT_CALCRECT = 1024;

		// Token: 0x04000CDE RID: 3294
		public const int WM_CAPTURECHANGED = 533;

		// Token: 0x04000CDF RID: 3295
		public const int WM_PARENTNOTIFY = 528;

		// Token: 0x04000CE0 RID: 3296
		public const int WM_CREATE = 1;

		// Token: 0x04000CE1 RID: 3297
		public const int WM_SETREDRAW = 11;

		// Token: 0x04000CE2 RID: 3298
		public const int WM_NCACTIVATE = 134;

		// Token: 0x04000CE3 RID: 3299
		public const int WM_HSCROLL = 276;

		// Token: 0x04000CE4 RID: 3300
		public const int WM_VSCROLL = 277;

		// Token: 0x04000CE5 RID: 3301
		public const int WM_SHOWWINDOW = 24;

		// Token: 0x04000CE6 RID: 3302
		public const int WM_WINDOWPOSCHANGING = 70;

		// Token: 0x04000CE7 RID: 3303
		public const int WM_WINDOWPOSCHANGED = 71;

		// Token: 0x04000CE8 RID: 3304
		public const int WS_DISABLED = 134217728;

		// Token: 0x04000CE9 RID: 3305
		public const int WS_CLIPSIBLINGS = 67108864;

		// Token: 0x04000CEA RID: 3306
		public const int WS_CLIPCHILDREN = 33554432;

		// Token: 0x04000CEB RID: 3307
		public const int WS_EX_TOOLWINDOW = 128;

		// Token: 0x04000CEC RID: 3308
		public const int WS_POPUP = -2147483648;

		// Token: 0x04000CED RID: 3309
		public const int WS_BORDER = 8388608;

		// Token: 0x04000CEE RID: 3310
		public const int CS_DROPSHADOW = 131072;

		// Token: 0x04000CEF RID: 3311
		public const int CS_DBLCLKS = 8;

		// Token: 0x04000CF0 RID: 3312
		public const int NOTSRCCOPY = 3342344;

		// Token: 0x04000CF1 RID: 3313
		public const int SRCCOPY = 13369376;

		// Token: 0x04000CF2 RID: 3314
		public const int LVM_SETCOLUMNWIDTH = 4126;

		// Token: 0x04000CF3 RID: 3315
		public const int LVM_GETHEADER = 4127;

		// Token: 0x04000CF4 RID: 3316
		public const int LVM_CREATEDRAGIMAGE = 4129;

		// Token: 0x04000CF5 RID: 3317
		public const int LVM_GETVIEWRECT = 4130;

		// Token: 0x04000CF6 RID: 3318
		public const int LVM_GETTEXTCOLOR = 4131;

		// Token: 0x04000CF7 RID: 3319
		public const int LVM_SETTEXTCOLOR = 4132;

		// Token: 0x04000CF8 RID: 3320
		public const int LVM_GETTEXTBKCOLOR = 4133;

		// Token: 0x04000CF9 RID: 3321
		public const int LVM_SETTEXTBKCOLOR = 4134;

		// Token: 0x04000CFA RID: 3322
		public const int LVM_GETTOPINDEX = 4135;

		// Token: 0x04000CFB RID: 3323
		public const int LVM_GETCOUNTPERPAGE = 4136;

		// Token: 0x04000CFC RID: 3324
		public const int LVM_GETORIGIN = 4137;

		// Token: 0x04000CFD RID: 3325
		public const int LVM_UPDATE = 4138;

		// Token: 0x04000CFE RID: 3326
		public const int LVM_SETITEMSTATE = 4139;

		// Token: 0x04000CFF RID: 3327
		public const int LVM_GETITEMSTATE = 4140;

		// Token: 0x04000D00 RID: 3328
		public const int LVM_GETITEMTEXTA = 4141;

		// Token: 0x04000D01 RID: 3329
		public const int LVM_GETITEMTEXTW = 4211;

		// Token: 0x04000D02 RID: 3330
		public const int LVM_SETITEMTEXTA = 4142;

		// Token: 0x04000D03 RID: 3331
		public const int LVM_SETITEMTEXTW = 4212;

		// Token: 0x04000D04 RID: 3332
		public const int LVSICF_NOINVALIDATEALL = 1;

		// Token: 0x04000D05 RID: 3333
		public const int LVSICF_NOSCROLL = 2;

		// Token: 0x04000D06 RID: 3334
		public const int LVM_SETITEMCOUNT = 4143;

		// Token: 0x04000D07 RID: 3335
		public const int LVM_SORTITEMS = 4144;

		// Token: 0x04000D08 RID: 3336
		public const int LVM_SETITEMPOSITION32 = 4145;

		// Token: 0x04000D09 RID: 3337
		public const int LVM_GETSELECTEDCOUNT = 4146;

		// Token: 0x04000D0A RID: 3338
		public const int LVM_GETITEMSPACING = 4147;

		// Token: 0x04000D0B RID: 3339
		public const int LVM_GETISEARCHSTRINGA = 4148;

		// Token: 0x04000D0C RID: 3340
		public const int LVM_GETISEARCHSTRINGW = 4213;

		// Token: 0x04000D0D RID: 3341
		public const int LVM_SETICONSPACING = 4149;

		// Token: 0x04000D0E RID: 3342
		public const int LVM_SETEXTENDEDLISTVIEWSTYLE = 4150;

		// Token: 0x04000D0F RID: 3343
		public const int LVM_GETEXTENDEDLISTVIEWSTYLE = 4151;

		// Token: 0x04000D10 RID: 3344
		public const int LVS_EX_GRIDLINES = 1;

		// Token: 0x04000D11 RID: 3345
		public const int HDM_HITTEST = 4614;

		// Token: 0x04000D12 RID: 3346
		public const int HDM_GETITEMRECT = 4615;

		// Token: 0x04000D13 RID: 3347
		public const int HDM_SETIMAGELIST = 4616;

		// Token: 0x04000D14 RID: 3348
		public const int HDM_GETIMAGELIST = 4617;

		// Token: 0x04000D15 RID: 3349
		public const int HDM_ORDERTOINDEX = 4623;

		// Token: 0x04000D16 RID: 3350
		public const int HDM_CREATEDRAGIMAGE = 4624;

		// Token: 0x04000D17 RID: 3351
		public const int HDM_GETORDERARRAY = 4625;

		// Token: 0x04000D18 RID: 3352
		public const int HDM_SETORDERARRAY = 4626;

		// Token: 0x04000D19 RID: 3353
		public const int HDM_SETHOTDIVIDER = 4627;

		// Token: 0x04000D1A RID: 3354
		public const int HDN_ITEMCHANGINGA = -300;

		// Token: 0x04000D1B RID: 3355
		public const int HDN_ITEMCHANGINGW = -320;

		// Token: 0x04000D1C RID: 3356
		public const int HDN_ITEMCHANGEDA = -301;

		// Token: 0x04000D1D RID: 3357
		public const int HDN_ITEMCHANGEDW = -321;

		// Token: 0x04000D1E RID: 3358
		public const int HDN_ITEMCLICKA = -302;

		// Token: 0x04000D1F RID: 3359
		public const int HDN_ITEMCLICKW = -322;

		// Token: 0x04000D20 RID: 3360
		public const int HDN_ITEMDBLCLICKA = -303;

		// Token: 0x04000D21 RID: 3361
		public const int HDN_ITEMDBLCLICKW = -323;

		// Token: 0x04000D22 RID: 3362
		public const int HDN_DIVIDERDBLCLICKA = -305;

		// Token: 0x04000D23 RID: 3363
		public const int HDN_DIVIDERDBLCLICKW = -325;

		// Token: 0x04000D24 RID: 3364
		public const int HDN_BEGINTRACKA = -306;

		// Token: 0x04000D25 RID: 3365
		public const int HDN_BEGINTRACKW = -326;

		// Token: 0x04000D26 RID: 3366
		public const int HDN_ENDTRACKA = -307;

		// Token: 0x04000D27 RID: 3367
		public const int HDN_ENDTRACKW = -327;

		// Token: 0x04000D28 RID: 3368
		public const int HDN_TRACKA = -308;

		// Token: 0x04000D29 RID: 3369
		public const int HDN_TRACKW = -328;

		// Token: 0x04000D2A RID: 3370
		public const int HDN_GETDISPINFOA = -309;

		// Token: 0x04000D2B RID: 3371
		public const int HDN_GETDISPINFOW = -329;

		// Token: 0x04000D2C RID: 3372
		public const int HDN_BEGINDRAG = -310;

		// Token: 0x04000D2D RID: 3373
		public const int HDN_ENDDRAG = -311;

		// Token: 0x04000D2E RID: 3374
		public const int HC_ACTION = 0;

		// Token: 0x04000D2F RID: 3375
		public const int HIST_BACK = 0;

		// Token: 0x04000D30 RID: 3376
		public const int HHT_ONHEADER = 2;

		// Token: 0x04000D31 RID: 3377
		public const int HHT_ONDIVIDER = 4;

		// Token: 0x04000D32 RID: 3378
		public const int HHT_ONDIVOPEN = 8;

		// Token: 0x04000D33 RID: 3379
		public const int HHT_ABOVE = 256;

		// Token: 0x04000D34 RID: 3380
		public const int HHT_BELOW = 512;

		// Token: 0x04000D35 RID: 3381
		public const int HHT_TORIGHT = 1024;

		// Token: 0x04000D36 RID: 3382
		public const int HHT_TOLEFT = 2048;

		// Token: 0x04000D37 RID: 3383
		public const int HWND_TOP = 0;

		// Token: 0x04000D38 RID: 3384
		public const int HWND_BOTTOM = 1;

		// Token: 0x04000D39 RID: 3385
		public const int HWND_TOPMOST = -1;

		// Token: 0x04000D3A RID: 3386
		public const int HWND_NOTOPMOST = -2;

		// Token: 0x04000D3B RID: 3387
		public const int CWP_SKIPINVISIBLE = 1;

		// Token: 0x04000D3C RID: 3388
		public const int RDW_FRAME = 1024;

		// Token: 0x04000D3D RID: 3389
		public const int WM_KILLFOCUS = 8;

		// Token: 0x04000D3E RID: 3390
		public const int WM_STYLECHANGED = 125;

		// Token: 0x04000D3F RID: 3391
		public const int TVM_GETITEMRECT = 4356;

		// Token: 0x04000D40 RID: 3392
		public const int TVM_GETCOUNT = 4357;

		// Token: 0x04000D41 RID: 3393
		public const int TVM_GETINDENT = 4358;

		// Token: 0x04000D42 RID: 3394
		public const int TVM_SETINDENT = 4359;

		// Token: 0x04000D43 RID: 3395
		public const int TVM_GETIMAGELIST = 4360;

		// Token: 0x04000D44 RID: 3396
		public const int TVSIL_NORMAL = 0;

		// Token: 0x04000D45 RID: 3397
		public const int TVSIL_STATE = 2;

		// Token: 0x04000D46 RID: 3398
		public const int TVM_SETIMAGELIST = 4361;

		// Token: 0x04000D47 RID: 3399
		public const int TVM_GETNEXTITEM = 4362;

		// Token: 0x04000D48 RID: 3400
		public const int TVGN_ROOT = 0;

		// Token: 0x04000D49 RID: 3401
		public const int TV_FIRST = 4352;

		// Token: 0x04000D4A RID: 3402
		public const int TVM_SETEXTENDEDSTYLE = 4396;

		// Token: 0x04000D4B RID: 3403
		public const int TVM_GETEXTENDEDSTYLE = 4397;

		// Token: 0x04000D4C RID: 3404
		public const int TVS_EX_FADEINOUTEXPANDOS = 64;

		// Token: 0x04000D4D RID: 3405
		public const int TVS_EX_DOUBLEBUFFER = 4;

		// Token: 0x04000D4E RID: 3406
		public const int LVS_EX_DOUBLEBUFFER = 65536;

		// Token: 0x04000D4F RID: 3407
		public const int TVHT_ONITEMICON = 2;

		// Token: 0x04000D50 RID: 3408
		public const int TVHT_ONITEMLABEL = 4;

		// Token: 0x04000D51 RID: 3409
		public const int TVHT_ONITEMINDENT = 8;

		// Token: 0x04000D52 RID: 3410
		public const int TVHT_ONITEMBUTTON = 16;

		// Token: 0x04000D53 RID: 3411
		public const int TVHT_ONITEMRIGHT = 32;

		// Token: 0x04000D54 RID: 3412
		public const int TVHT_ONITEMSTATEICON = 64;

		// Token: 0x04000D55 RID: 3413
		public const int TVHT_ABOVE = 256;

		// Token: 0x04000D56 RID: 3414
		public const int TVHT_BELOW = 512;

		// Token: 0x04000D57 RID: 3415
		public const int TVHT_TORIGHT = 1024;

		// Token: 0x04000D58 RID: 3416
		public const int TVHT_TOLEFT = 2048;

		// Token: 0x04000D59 RID: 3417
		public const int GW_HWNDFIRST = 0;

		// Token: 0x04000D5A RID: 3418
		public const int GW_HWNDLAST = 1;

		// Token: 0x04000D5B RID: 3419
		public const int GW_HWNDNEXT = 2;

		// Token: 0x04000D5C RID: 3420
		public const int GW_HWNDPREV = 3;

		// Token: 0x04000D5D RID: 3421
		public const int GW_OWNER = 4;

		// Token: 0x04000D5E RID: 3422
		public const int GW_CHILD = 5;

		// Token: 0x04000D5F RID: 3423
		public const int GW_MAX = 5;

		// Token: 0x04000D60 RID: 3424
		public const int GWL_HWNDPARENT = -8;

		// Token: 0x04000D61 RID: 3425
		public const int SB_HORZ = 0;

		// Token: 0x04000D62 RID: 3426
		public const int SB_VERT = 1;

		// Token: 0x04000D63 RID: 3427
		public const int SB_CTL = 2;

		// Token: 0x04000D64 RID: 3428
		public const int SB_BOTH = 3;

		// Token: 0x04000D65 RID: 3429
		public const int SB_LINEUP = 0;

		// Token: 0x04000D66 RID: 3430
		public const int SB_LINELEFT = 0;

		// Token: 0x04000D67 RID: 3431
		public const int SB_LINEDOWN = 1;

		// Token: 0x04000D68 RID: 3432
		public const int SB_LINERIGHT = 1;

		// Token: 0x04000D69 RID: 3433
		public const int SB_PAGEUP = 2;

		// Token: 0x04000D6A RID: 3434
		public const int SB_PAGELEFT = 2;

		// Token: 0x04000D6B RID: 3435
		public const int SB_PAGEDOWN = 3;

		// Token: 0x04000D6C RID: 3436
		public const int SB_PAGERIGHT = 3;

		// Token: 0x04000D6D RID: 3437
		public const int SB_THUMBPOSITION = 4;

		// Token: 0x04000D6E RID: 3438
		public const int SB_THUMBTRACK = 5;

		// Token: 0x04000D6F RID: 3439
		public const int SB_TOP = 6;

		// Token: 0x04000D70 RID: 3440
		public const int SB_LEFT = 6;

		// Token: 0x04000D71 RID: 3441
		public const int SB_BOTTOM = 7;

		// Token: 0x04000D72 RID: 3442
		public const int SB_RIGHT = 7;

		// Token: 0x04000D73 RID: 3443
		public const int SB_ENDSCROLL = 8;

		// Token: 0x04000D74 RID: 3444
		public const int MK_LBUTTON = 1;

		// Token: 0x04000D75 RID: 3445
		public const int TVM_HITTEST = 4369;

		// Token: 0x04000D76 RID: 3446
		public const int MK_RBUTTON = 2;

		// Token: 0x04000D77 RID: 3447
		public const int MK_SHIFT = 4;

		// Token: 0x04000D78 RID: 3448
		public const int MK_CONTROL = 8;

		// Token: 0x04000D79 RID: 3449
		public const int MK_MBUTTON = 16;

		// Token: 0x04000D7A RID: 3450
		public const int MK_XBUTTON1 = 32;

		// Token: 0x04000D7B RID: 3451
		public const int MK_XBUTTON2 = 64;

		// Token: 0x04000D7C RID: 3452
		public const int LB_ADDSTRING = 384;

		// Token: 0x04000D7D RID: 3453
		public const int LB_INSERTSTRING = 385;

		// Token: 0x04000D7E RID: 3454
		public const int LB_DELETESTRING = 386;

		// Token: 0x04000D7F RID: 3455
		public const int LB_SELITEMRANGEEX = 387;

		// Token: 0x04000D80 RID: 3456
		public const int LB_RESETCONTENT = 388;

		// Token: 0x04000D81 RID: 3457
		public const int LB_SETSEL = 389;

		// Token: 0x04000D82 RID: 3458
		public const int LB_SETCURSEL = 390;

		// Token: 0x04000D83 RID: 3459
		public const int LB_GETSEL = 391;

		// Token: 0x04000D84 RID: 3460
		public const int LB_GETCURSEL = 392;

		// Token: 0x04000D85 RID: 3461
		public const int LB_GETTEXT = 393;

		// Token: 0x04000D86 RID: 3462
		public const int LB_GETTEXTLEN = 394;

		// Token: 0x04000D87 RID: 3463
		public const int LB_GETCOUNT = 395;

		// Token: 0x04000D88 RID: 3464
		public const int LB_SELECTSTRING = 396;

		// Token: 0x04000D89 RID: 3465
		public const int LB_DIR = 397;

		// Token: 0x04000D8A RID: 3466
		public const int LB_GETTOPINDEX = 398;

		// Token: 0x04000D8B RID: 3467
		public const int LB_FINDSTRING = 399;

		// Token: 0x04000D8C RID: 3468
		public const int LB_GETSELCOUNT = 400;

		// Token: 0x04000D8D RID: 3469
		public const int LB_GETSELITEMS = 401;

		// Token: 0x04000D8E RID: 3470
		public const int LB_SETTABSTOPS = 402;

		// Token: 0x04000D8F RID: 3471
		public const int LB_GETHORIZONTALEXTENT = 403;

		// Token: 0x04000D90 RID: 3472
		public const int LB_SETHORIZONTALEXTENT = 404;

		// Token: 0x04000D91 RID: 3473
		public const int LB_SETCOLUMNWIDTH = 405;

		// Token: 0x04000D92 RID: 3474
		public const int LB_ADDFILE = 406;

		// Token: 0x04000D93 RID: 3475
		public const int LB_SETTOPINDEX = 407;

		// Token: 0x04000D94 RID: 3476
		public const int LB_GETITEMRECT = 408;

		// Token: 0x04000D95 RID: 3477
		public const int LB_GETITEMDATA = 409;

		// Token: 0x04000D96 RID: 3478
		public const int LB_SETITEMDATA = 410;

		// Token: 0x04000D97 RID: 3479
		public const int LB_SELITEMRANGE = 411;

		// Token: 0x04000D98 RID: 3480
		public const int LB_SETANCHORINDEX = 412;

		// Token: 0x04000D99 RID: 3481
		public const int LB_GETANCHORINDEX = 413;

		// Token: 0x04000D9A RID: 3482
		public const int LB_SETCARETINDEX = 414;

		// Token: 0x04000D9B RID: 3483
		public const int LB_GETCARETINDEX = 415;

		// Token: 0x04000D9C RID: 3484
		public const int LB_SETITEMHEIGHT = 416;

		// Token: 0x04000D9D RID: 3485
		public const int LB_GETITEMHEIGHT = 417;

		// Token: 0x04000D9E RID: 3486
		public const int LB_FINDSTRINGEXACT = 418;

		// Token: 0x04000D9F RID: 3487
		public const int LB_SETLOCALE = 421;

		// Token: 0x04000DA0 RID: 3488
		public const int LB_GETLOCALE = 422;

		// Token: 0x04000DA1 RID: 3489
		public const int LB_SETCOUNT = 423;

		// Token: 0x04000DA2 RID: 3490
		public const int LB_INITSTORAGE = 424;

		// Token: 0x04000DA3 RID: 3491
		public const int LB_ITEMFROMPOINT = 425;

		// Token: 0x04000DA4 RID: 3492
		public const int LB_MSGMAX = 432;

		// Token: 0x04000DA5 RID: 3493
		public const int HTHSCROLL = 6;

		// Token: 0x04000DA6 RID: 3494
		public const int HTVSCROLL = 7;

		// Token: 0x04000DA7 RID: 3495
		public const int HTERROR = -2;

		// Token: 0x04000DA8 RID: 3496
		public const int HTTRANSPARENT = -1;

		// Token: 0x04000DA9 RID: 3497
		public const int HTNOWHERE = 0;

		// Token: 0x04000DAA RID: 3498
		public const int HTCLIENT = 1;

		// Token: 0x04000DAB RID: 3499
		public const int HTCAPTION = 2;

		// Token: 0x04000DAC RID: 3500
		public const int HTSYSMENU = 3;

		// Token: 0x04000DAD RID: 3501
		public const int HTGROWBOX = 4;

		// Token: 0x04000DAE RID: 3502
		public const int HTSIZE = 4;

		// Token: 0x04000DAF RID: 3503
		public const int PRF_NONCLIENT = 2;

		// Token: 0x04000DB0 RID: 3504
		public const int PRF_CLIENT = 4;

		// Token: 0x04000DB1 RID: 3505
		public const int PRF_ERASEBKGND = 8;

		// Token: 0x04000DB2 RID: 3506
		public const int PRF_CHILDREN = 16;

		// Token: 0x04000DB3 RID: 3507
		public const int SWP_NOSIZE = 1;

		// Token: 0x04000DB4 RID: 3508
		public const int SWP_NOMOVE = 2;

		// Token: 0x04000DB5 RID: 3509
		public const int SWP_NOZORDER = 4;

		// Token: 0x04000DB6 RID: 3510
		public const int SWP_NOREDRAW = 8;

		// Token: 0x04000DB7 RID: 3511
		public const int SWP_NOACTIVATE = 16;

		// Token: 0x04000DB8 RID: 3512
		public const int SWP_FRAMECHANGED = 32;

		// Token: 0x04000DB9 RID: 3513
		public const int SWP_SHOWWINDOW = 64;

		// Token: 0x04000DBA RID: 3514
		public const int SWP_HIDEWINDOW = 128;

		// Token: 0x04000DBB RID: 3515
		public const int SWP_NOCOPYBITS = 256;

		// Token: 0x04000DBC RID: 3516
		public const int SWP_NOOWNERZORDER = 512;

		// Token: 0x04000DBD RID: 3517
		public const int SWP_NOSENDCHANGING = 1024;

		// Token: 0x04000DBE RID: 3518
		public const int SWP_DRAWFRAME = 32;

		// Token: 0x04000DBF RID: 3519
		public const int SWP_NOREPOSITION = 512;

		// Token: 0x04000DC0 RID: 3520
		public const int SWP_DEFERERASE = 8192;

		// Token: 0x04000DC1 RID: 3521
		public const int SWP_ASYNCWINDOWPOS = 16384;

		// Token: 0x04000DC2 RID: 3522
		public const int WA_INACTIVE = 0;

		// Token: 0x04000DC3 RID: 3523
		public const int WA_ACTIVE = 1;

		// Token: 0x04000DC4 RID: 3524
		public const int WH_MOUSE = 7;

		// Token: 0x04000DC5 RID: 3525
		public const int WM_IME_STARTCOMPOSITION = 269;

		// Token: 0x04000DC6 RID: 3526
		public const int WM_IME_ENDCOMPOSITION = 270;

		// Token: 0x04000DC7 RID: 3527
		public const int WM_IME_COMPOSITION = 271;

		// Token: 0x04000DC8 RID: 3528
		public const int WM_ACTIVATE = 6;

		// Token: 0x04000DC9 RID: 3529
		public const int WM_NCMOUSEMOVE = 160;

		// Token: 0x04000DCA RID: 3530
		public const int WM_NCLBUTTONDOWN = 161;

		// Token: 0x04000DCB RID: 3531
		public const int WM_NCLBUTTONUP = 162;

		// Token: 0x04000DCC RID: 3532
		public const int WM_NCLBUTTONDBLCLK = 163;

		// Token: 0x04000DCD RID: 3533
		public const int WM_NCRBUTTONDOWN = 164;

		// Token: 0x04000DCE RID: 3534
		public const int WM_NCRBUTTONUP = 165;

		// Token: 0x04000DCF RID: 3535
		public const int WM_NCRBUTTONDBLCLK = 166;

		// Token: 0x04000DD0 RID: 3536
		public const int WM_NCMBUTTONDOWN = 167;

		// Token: 0x04000DD1 RID: 3537
		public const int WM_NCMBUTTONUP = 168;

		// Token: 0x04000DD2 RID: 3538
		public const int WM_NCMBUTTONDBLCLK = 169;

		// Token: 0x04000DD3 RID: 3539
		public const int WM_NCXBUTTONDOWN = 171;

		// Token: 0x04000DD4 RID: 3540
		public const int WM_NCXBUTTONUP = 172;

		// Token: 0x04000DD5 RID: 3541
		public const int WM_NCXBUTTONDBLCLK = 173;

		// Token: 0x04000DD6 RID: 3542
		public const int WM_MOUSEHOVER = 673;

		// Token: 0x04000DD7 RID: 3543
		public const int WM_MOUSELEAVE = 675;

		// Token: 0x04000DD8 RID: 3544
		public const int WM_MOUSEFIRST = 512;

		// Token: 0x04000DD9 RID: 3545
		public const int WM_MOUSEACTIVATE = 33;

		// Token: 0x04000DDA RID: 3546
		public const int WM_LBUTTONDOWN = 513;

		// Token: 0x04000DDB RID: 3547
		public const int WM_LBUTTONUP = 514;

		// Token: 0x04000DDC RID: 3548
		public const int WM_LBUTTONDBLCLK = 515;

		// Token: 0x04000DDD RID: 3549
		public const int WM_RBUTTONDOWN = 516;

		// Token: 0x04000DDE RID: 3550
		public const int WM_RBUTTONUP = 517;

		// Token: 0x04000DDF RID: 3551
		public const int WM_RBUTTONDBLCLK = 518;

		// Token: 0x04000DE0 RID: 3552
		public const int WM_MBUTTONDOWN = 519;

		// Token: 0x04000DE1 RID: 3553
		public const int WM_MBUTTONUP = 520;

		// Token: 0x04000DE2 RID: 3554
		public const int WM_MBUTTONDBLCLK = 521;

		// Token: 0x04000DE3 RID: 3555
		public const int WM_NCMOUSEHOVER = 672;

		// Token: 0x04000DE4 RID: 3556
		public const int WM_NCMOUSELEAVE = 674;

		// Token: 0x04000DE5 RID: 3557
		public const int WM_MOUSEWHEEL = 522;

		// Token: 0x04000DE6 RID: 3558
		public const int WM_MOUSELAST = 522;

		// Token: 0x04000DE7 RID: 3559
		public const int WM_NCHITTEST = 132;

		// Token: 0x04000DE8 RID: 3560
		public const int WM_SETCURSOR = 32;

		// Token: 0x04000DE9 RID: 3561
		public const int WM_GETOBJECT = 61;

		// Token: 0x04000DEA RID: 3562
		public const int WM_CANCELMODE = 31;

		// Token: 0x04000DEB RID: 3563
		public const int WM_SETFOCUS = 7;

		// Token: 0x04000DEC RID: 3564
		public const int WM_KEYFIRST = 256;

		// Token: 0x04000DED RID: 3565
		public const int WM_KEYDOWN = 256;

		// Token: 0x04000DEE RID: 3566
		public const int WM_KEYUP = 257;

		// Token: 0x04000DEF RID: 3567
		public const int WM_DEADCHAR = 259;

		// Token: 0x04000DF0 RID: 3568
		public const int WM_SYSKEYDOWN = 260;

		// Token: 0x04000DF1 RID: 3569
		public const int WM_SYSKEYUP = 261;

		// Token: 0x04000DF2 RID: 3570
		public const int WM_SYSCHAR = 262;

		// Token: 0x04000DF3 RID: 3571
		public const int WM_SYSDEADCHAR = 263;

		// Token: 0x04000DF4 RID: 3572
		public const int WM_KEYLAST = 264;

		// Token: 0x04000DF5 RID: 3573
		public const int WM_CONTEXTMENU = 123;

		// Token: 0x04000DF6 RID: 3574
		public const int WM_PAINT = 15;

		// Token: 0x04000DF7 RID: 3575
		public const int WM_PRINTCLIENT = 792;

		// Token: 0x04000DF8 RID: 3576
		public const int WM_NCPAINT = 133;

		// Token: 0x04000DF9 RID: 3577
		public const int WM_SIZE = 5;

		// Token: 0x04000DFA RID: 3578
		public const int WM_TIMER = 275;

		// Token: 0x04000DFB RID: 3579
		public const int WM_PRINT = 791;

		// Token: 0x04000DFC RID: 3580
		public const int CHILDID_SELF = 0;

		// Token: 0x04000DFD RID: 3581
		public const int OBJID_WINDOW = 0;

		// Token: 0x04000DFE RID: 3582
		public const int OBJID_CLIENT = -4;

		// Token: 0x04000DFF RID: 3583
		public const string uuid_IAccessible = "{618736E0-3C3D-11CF-810C-00AA00389B71}";

		// Token: 0x04000E00 RID: 3584
		public const string uuid_IEnumVariant = "{00020404-0000-0000-C000-000000000046}";

		// Token: 0x04000E01 RID: 3585
		public const int QS_KEY = 1;

		// Token: 0x04000E02 RID: 3586
		public const int QS_MOUSEMOVE = 2;

		// Token: 0x04000E03 RID: 3587
		public const int QS_MOUSEBUTTON = 4;

		// Token: 0x04000E04 RID: 3588
		public const int QS_POSTMESSAGE = 8;

		// Token: 0x04000E05 RID: 3589
		public const int QS_TIMER = 16;

		// Token: 0x04000E06 RID: 3590
		public const int QS_PAINT = 32;

		// Token: 0x04000E07 RID: 3591
		public const int QS_SENDMESSAGE = 64;

		// Token: 0x04000E08 RID: 3592
		public const int QS_HOTKEY = 128;

		// Token: 0x04000E09 RID: 3593
		public const int QS_ALLPOSTMESSAGE = 256;

		// Token: 0x04000E0A RID: 3594
		public const int QS_MOUSE = 6;

		// Token: 0x04000E0B RID: 3595
		public const int QS_INPUT = 7;

		// Token: 0x04000E0C RID: 3596
		public const int QS_ALLEVENTS = 191;

		// Token: 0x04000E0D RID: 3597
		public const int QS_ALLINPUT = 255;

		// Token: 0x04000E0E RID: 3598
		public const int MWMO_INPUTAVAILABLE = 4;

		// Token: 0x04000E0F RID: 3599
		public const int GWL_EXSTYLE = -20;

		// Token: 0x04000E10 RID: 3600
		public const int GWL_STYLE = -16;

		// Token: 0x04000E11 RID: 3601
		public const int WS_EX_LAYOUTRTL = 4194304;

		// Token: 0x04000E12 RID: 3602
		public const int SPI_GETNONCLIENTMETRICS = 41;

		// Token: 0x020004C5 RID: 1221
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class TEXTMETRIC
		{
			// Token: 0x04001F0B RID: 7947
			public int tmHeight;

			// Token: 0x04001F0C RID: 7948
			public int tmAscent;

			// Token: 0x04001F0D RID: 7949
			public int tmDescent;

			// Token: 0x04001F0E RID: 7950
			public int tmInternalLeading;

			// Token: 0x04001F0F RID: 7951
			public int tmExternalLeading;

			// Token: 0x04001F10 RID: 7952
			public int tmAveCharWidth;

			// Token: 0x04001F11 RID: 7953
			public int tmMaxCharWidth;

			// Token: 0x04001F12 RID: 7954
			public int tmWeight;

			// Token: 0x04001F13 RID: 7955
			public int tmOverhang;

			// Token: 0x04001F14 RID: 7956
			public int tmDigitizedAspectX;

			// Token: 0x04001F15 RID: 7957
			public int tmDigitizedAspectY;

			// Token: 0x04001F16 RID: 7958
			public char tmFirstChar;

			// Token: 0x04001F17 RID: 7959
			public char tmLastChar;

			// Token: 0x04001F18 RID: 7960
			public char tmDefaultChar;

			// Token: 0x04001F19 RID: 7961
			public char tmBreakChar;

			// Token: 0x04001F1A RID: 7962
			public byte tmItalic;

			// Token: 0x04001F1B RID: 7963
			public byte tmUnderlined;

			// Token: 0x04001F1C RID: 7964
			public byte tmStruckOut;

			// Token: 0x04001F1D RID: 7965
			public byte tmPitchAndFamily;

			// Token: 0x04001F1E RID: 7966
			public byte tmCharSet;
		}

		// Token: 0x020004C6 RID: 1222
		// (Invoke) Token: 0x06002C4D RID: 11341
		public delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);

		// Token: 0x020004C7 RID: 1223
		[StructLayout(LayoutKind.Sequential)]
		public class NMHEADER
		{
			// Token: 0x04001F1F RID: 7967
			public int hwndFrom;

			// Token: 0x04001F20 RID: 7968
			public int idFrom;

			// Token: 0x04001F21 RID: 7969
			public int code;

			// Token: 0x04001F22 RID: 7970
			public int iItem;

			// Token: 0x04001F23 RID: 7971
			public int iButton;

			// Token: 0x04001F24 RID: 7972
			public int pItem;
		}

		// Token: 0x020004C8 RID: 1224
		[StructLayout(LayoutKind.Sequential)]
		public class POINT
		{
			// Token: 0x06002C51 RID: 11345 RVA: 0x0000362F File Offset: 0x0000182F
			public POINT()
			{
			}

			// Token: 0x06002C52 RID: 11346 RVA: 0x001074A6 File Offset: 0x001056A6
			public POINT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x04001F25 RID: 7973
			public int x;

			// Token: 0x04001F26 RID: 7974
			public int y;
		}

		// Token: 0x020004C9 RID: 1225
		public struct POINTL
		{
			// Token: 0x04001F27 RID: 7975
			public int x;

			// Token: 0x04001F28 RID: 7976
			public int y;
		}

		// Token: 0x020004CA RID: 1226
		public struct WINDOWPOS
		{
			// Token: 0x04001F29 RID: 7977
			public IntPtr hwnd;

			// Token: 0x04001F2A RID: 7978
			public IntPtr hwndInsertAfter;

			// Token: 0x04001F2B RID: 7979
			public int x;

			// Token: 0x04001F2C RID: 7980
			public int y;

			// Token: 0x04001F2D RID: 7981
			public int cx;

			// Token: 0x04001F2E RID: 7982
			public int cy;

			// Token: 0x04001F2F RID: 7983
			public int flags;
		}

		// Token: 0x020004CB RID: 1227
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public class TV_ITEM
		{
			// Token: 0x04001F30 RID: 7984
			public int mask;

			// Token: 0x04001F31 RID: 7985
			public int hItem;

			// Token: 0x04001F32 RID: 7986
			public int state;

			// Token: 0x04001F33 RID: 7987
			public int stateMask;

			// Token: 0x04001F34 RID: 7988
			public int pszText;

			// Token: 0x04001F35 RID: 7989
			public int cchTextMax;

			// Token: 0x04001F36 RID: 7990
			public int iImage;

			// Token: 0x04001F37 RID: 7991
			public int iSelectedImage;

			// Token: 0x04001F38 RID: 7992
			public int cChildren;

			// Token: 0x04001F39 RID: 7993
			public int lParam;
		}

		// Token: 0x020004CC RID: 1228
		[StructLayout(LayoutKind.Sequential)]
		public class NMHDR
		{
			// Token: 0x04001F3A RID: 7994
			public int hwndFrom;

			// Token: 0x04001F3B RID: 7995
			public int idFrom;

			// Token: 0x04001F3C RID: 7996
			public int code;
		}

		// Token: 0x020004CD RID: 1229
		[StructLayout(LayoutKind.Sequential)]
		public class NMTREEVIEW
		{
			// Token: 0x04001F3D RID: 7997
			public NativeMethods.NMHDR nmhdr;

			// Token: 0x04001F3E RID: 7998
			public int action;

			// Token: 0x04001F3F RID: 7999
			public NativeMethods.TV_ITEM itemOld;

			// Token: 0x04001F40 RID: 8000
			public NativeMethods.TV_ITEM itemNew;

			// Token: 0x04001F41 RID: 8001
			public NativeMethods.POINT ptDrag;
		}

		// Token: 0x020004CE RID: 1230
		[StructLayout(LayoutKind.Sequential)]
		public class TCHITTESTINFO
		{
			// Token: 0x04001F42 RID: 8002
			public Point pt;

			// Token: 0x04001F43 RID: 8003
			public NativeMethods.TabControlHitTest flags;
		}

		// Token: 0x020004CF RID: 1231
		[Flags]
		public enum TabControlHitTest
		{
			// Token: 0x04001F45 RID: 8005
			TCHT_NOWHERE = 1,
			// Token: 0x04001F46 RID: 8006
			TCHT_ONITEMICON = 2,
			// Token: 0x04001F47 RID: 8007
			TCHT_ONITEMLABEL = 4
		}

		// Token: 0x020004D0 RID: 1232
		[StructLayout(LayoutKind.Sequential)]
		public class TRACKMOUSEEVENT
		{
			// Token: 0x04001F48 RID: 8008
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.TRACKMOUSEEVENT));

			// Token: 0x04001F49 RID: 8009
			public int dwFlags;

			// Token: 0x04001F4A RID: 8010
			public IntPtr hwndTrack;

			// Token: 0x04001F4B RID: 8011
			public int dwHoverTime;
		}

		// Token: 0x020004D1 RID: 1233
		[ComVisible(false)]
		public enum StructFormat
		{
			// Token: 0x04001F4D RID: 8013
			Ansi = 1,
			// Token: 0x04001F4E RID: 8014
			Unicode,
			// Token: 0x04001F4F RID: 8015
			Auto
		}

		// Token: 0x020004D2 RID: 1234
		public struct MOUSEHOOKSTRUCT
		{
			// Token: 0x04001F50 RID: 8016
			public int pt_x;

			// Token: 0x04001F51 RID: 8017
			public int pt_y;

			// Token: 0x04001F52 RID: 8018
			public IntPtr hWnd;

			// Token: 0x04001F53 RID: 8019
			public int wHitTestCode;

			// Token: 0x04001F54 RID: 8020
			public int dwExtraInfo;
		}

		// Token: 0x020004D3 RID: 1235
		public struct MSG
		{
			// Token: 0x04001F55 RID: 8021
			public IntPtr hwnd;

			// Token: 0x04001F56 RID: 8022
			public int message;

			// Token: 0x04001F57 RID: 8023
			public IntPtr wParam;

			// Token: 0x04001F58 RID: 8024
			public IntPtr lParam;

			// Token: 0x04001F59 RID: 8025
			public int time;

			// Token: 0x04001F5A RID: 8026
			public int pt_x;

			// Token: 0x04001F5B RID: 8027
			public int pt_y;
		}

		// Token: 0x020004D4 RID: 1236
		[StructLayout(LayoutKind.Sequential)]
		public class COMRECT
		{
			// Token: 0x06002C58 RID: 11352 RVA: 0x0000362F File Offset: 0x0000182F
			public COMRECT()
			{
			}

			// Token: 0x06002C59 RID: 11353 RVA: 0x001074D9 File Offset: 0x001056D9
			public COMRECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x04001F5C RID: 8028
			public int left;

			// Token: 0x04001F5D RID: 8029
			public int top;

			// Token: 0x04001F5E RID: 8030
			public int right;

			// Token: 0x04001F5F RID: 8031
			public int bottom;
		}

		// Token: 0x020004D5 RID: 1237
		[StructLayout(LayoutKind.Sequential)]
		public sealed class FORMATETC
		{
			// Token: 0x04001F60 RID: 8032
			[MarshalAs(UnmanagedType.I4)]
			public int cfFormat;

			// Token: 0x04001F61 RID: 8033
			[MarshalAs(UnmanagedType.I4)]
			public IntPtr ptd = IntPtr.Zero;

			// Token: 0x04001F62 RID: 8034
			[MarshalAs(UnmanagedType.I4)]
			public int dwAspect;

			// Token: 0x04001F63 RID: 8035
			[MarshalAs(UnmanagedType.I4)]
			public int lindex;

			// Token: 0x04001F64 RID: 8036
			[MarshalAs(UnmanagedType.I4)]
			public int tymed;
		}

		// Token: 0x020004D6 RID: 1238
		[StructLayout(LayoutKind.Sequential)]
		public class STGMEDIUM
		{
			// Token: 0x04001F65 RID: 8037
			[MarshalAs(UnmanagedType.I4)]
			public int tymed;

			// Token: 0x04001F66 RID: 8038
			public IntPtr unionmember = IntPtr.Zero;

			// Token: 0x04001F67 RID: 8039
			public IntPtr pUnkForRelease = IntPtr.Zero;
		}

		// Token: 0x020004D7 RID: 1239
		public struct RECT
		{
			// Token: 0x06002C5C RID: 11356 RVA: 0x0010752F File Offset: 0x0010572F
			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x04001F68 RID: 8040
			public int left;

			// Token: 0x04001F69 RID: 8041
			public int top;

			// Token: 0x04001F6A RID: 8042
			public int right;

			// Token: 0x04001F6B RID: 8043
			public int bottom;
		}

		// Token: 0x020004D8 RID: 1240
		[StructLayout(LayoutKind.Sequential)]
		public sealed class OLECMD
		{
			// Token: 0x04001F6C RID: 8044
			[MarshalAs(UnmanagedType.U4)]
			public int cmdID;

			// Token: 0x04001F6D RID: 8045
			[MarshalAs(UnmanagedType.U4)]
			public int cmdf;
		}

		// Token: 0x020004D9 RID: 1241
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOIFI
		{
			// Token: 0x04001F6E RID: 8046
			[MarshalAs(UnmanagedType.U4)]
			public int cb;

			// Token: 0x04001F6F RID: 8047
			[MarshalAs(UnmanagedType.I4)]
			public int fMDIApp;

			// Token: 0x04001F70 RID: 8048
			public IntPtr hwndFrame;

			// Token: 0x04001F71 RID: 8049
			public IntPtr hAccel;

			// Token: 0x04001F72 RID: 8050
			[MarshalAs(UnmanagedType.U4)]
			public int cAccelEntries;
		}

		// Token: 0x020004DA RID: 1242
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagSIZE
		{
			// Token: 0x04001F73 RID: 8051
			[MarshalAs(UnmanagedType.I4)]
			public int cx;

			// Token: 0x04001F74 RID: 8052
			[MarshalAs(UnmanagedType.I4)]
			public int cy;
		}

		// Token: 0x020004DB RID: 1243
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagSIZEL
		{
			// Token: 0x04001F75 RID: 8053
			[MarshalAs(UnmanagedType.I4)]
			public int cx;

			// Token: 0x04001F76 RID: 8054
			[MarshalAs(UnmanagedType.I4)]
			public int cy;
		}

		// Token: 0x020004DC RID: 1244
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagLOGPALETTE
		{
			// Token: 0x04001F77 RID: 8055
			[MarshalAs(UnmanagedType.U2)]
			public short palVersion;

			// Token: 0x04001F78 RID: 8056
			[MarshalAs(UnmanagedType.U2)]
			public short palNumEntries;
		}

		// Token: 0x020004DD RID: 1245
		public class DOCHOSTUIDBLCLICK
		{
			// Token: 0x04001F79 RID: 8057
			public const int DEFAULT = 0;

			// Token: 0x04001F7A RID: 8058
			public const int SHOWPROPERTIES = 1;

			// Token: 0x04001F7B RID: 8059
			public const int SHOWCODE = 2;
		}

		// Token: 0x020004DE RID: 1246
		public class DOCHOSTUIFLAG
		{
			// Token: 0x04001F7C RID: 8060
			public const int DIALOG = 1;

			// Token: 0x04001F7D RID: 8061
			public const int DISABLE_HELP_MENU = 2;

			// Token: 0x04001F7E RID: 8062
			public const int NO3DBORDER = 4;

			// Token: 0x04001F7F RID: 8063
			public const int SCROLL_NO = 8;

			// Token: 0x04001F80 RID: 8064
			public const int DISABLE_SCRIPT_INACTIVE = 16;

			// Token: 0x04001F81 RID: 8065
			public const int OPENNEWWIN = 32;

			// Token: 0x04001F82 RID: 8066
			public const int DISABLE_OFFSCREEN = 64;

			// Token: 0x04001F83 RID: 8067
			public const int FLAT_SCROLLBAR = 128;

			// Token: 0x04001F84 RID: 8068
			public const int DIV_BLOCKDEFAULT = 256;

			// Token: 0x04001F85 RID: 8069
			public const int ACTIVATE_CLIENTHIT_ONLY = 512;

			// Token: 0x04001F86 RID: 8070
			public const int DISABLE_COOKIE = 1024;
		}

		// Token: 0x020004DF RID: 1247
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public class DOCHOSTUIINFO
		{
			// Token: 0x04001F87 RID: 8071
			[MarshalAs(UnmanagedType.U4)]
			public int cbSize;

			// Token: 0x04001F88 RID: 8072
			[MarshalAs(UnmanagedType.I4)]
			public int dwFlags;

			// Token: 0x04001F89 RID: 8073
			[MarshalAs(UnmanagedType.I4)]
			public int dwDoubleClick;

			// Token: 0x04001F8A RID: 8074
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved1;

			// Token: 0x04001F8B RID: 8075
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved2;
		}

		// Token: 0x020004E0 RID: 1248
		[ComVisible(true)]
		[Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IDocHostUIHandler
		{
			// Token: 0x06002C65 RID: 11365
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ShowContextMenu([MarshalAs(UnmanagedType.U4)] [In] int dwID, [In] NativeMethods.POINT pt, [MarshalAs(UnmanagedType.Interface)] [In] object pcmdtReserved, [MarshalAs(UnmanagedType.Interface)] [In] object pdispReserved);

			// Token: 0x06002C66 RID: 11366
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetHostInfo([In] [Out] NativeMethods.DOCHOSTUIINFO info);

			// Token: 0x06002C67 RID: 11367
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ShowUI([MarshalAs(UnmanagedType.I4)] [In] int dwID, [In] NativeMethods.IOleInPlaceActiveObject activeObject, [In] NativeMethods.IOleCommandTarget commandTarget, [In] NativeMethods.IOleInPlaceFrame frame, [In] NativeMethods.IOleInPlaceUIWindow doc);

			// Token: 0x06002C68 RID: 11368
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int HideUI();

			// Token: 0x06002C69 RID: 11369
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int UpdateUI();

			// Token: 0x06002C6A RID: 11370
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int EnableModeless([MarshalAs(UnmanagedType.Bool)] [In] bool fEnable);

			// Token: 0x06002C6B RID: 11371
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnDocWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);

			// Token: 0x06002C6C RID: 11372
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnFrameWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);

			// Token: 0x06002C6D RID: 11373
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ResizeBorder([In] NativeMethods.COMRECT rect, [In] NativeMethods.IOleInPlaceUIWindow doc, bool fFrameWindow);

			// Token: 0x06002C6E RID: 11374
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([In] ref NativeMethods.MSG msg, [In] ref Guid group, [MarshalAs(UnmanagedType.I4)] [In] int nCmdID);

			// Token: 0x06002C6F RID: 11375
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOptionKeyPath([MarshalAs(UnmanagedType.LPArray)] [Out] string[] pbstrKey, [MarshalAs(UnmanagedType.U4)] [In] int dw);

			// Token: 0x06002C70 RID: 11376
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetDropTarget([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleDropTarget pDropTarget, [MarshalAs(UnmanagedType.Interface)] out NativeMethods.IOleDropTarget ppDropTarget);

			// Token: 0x06002C71 RID: 11377
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetExternal([MarshalAs(UnmanagedType.Interface)] out object ppDispatch);

			// Token: 0x06002C72 RID: 11378
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateUrl([MarshalAs(UnmanagedType.U4)] [In] int dwTranslate, [MarshalAs(UnmanagedType.LPWStr)] [In] string strURLIn, [MarshalAs(UnmanagedType.LPWStr)] out string pstrURLOut);

			// Token: 0x06002C73 RID: 11379
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int FilterDataObject(IDataObject pDO, out IDataObject ppDORet);
		}

		// Token: 0x020004E1 RID: 1249
		[ComVisible(true)]
		[Guid("00000122-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDropTarget
		{
			// Token: 0x06002C74 RID: 11380
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OleDragEnter(IDataObject pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] NativeMethods.POINTL pt, [MarshalAs(UnmanagedType.I4)] [In] [Out] ref int pdwEffect);

			// Token: 0x06002C75 RID: 11381
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OleDragOver([MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] NativeMethods.POINTL pt, [MarshalAs(UnmanagedType.I4)] [In] [Out] ref int pdwEffect);

			// Token: 0x06002C76 RID: 11382
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OleDragLeave();

			// Token: 0x06002C77 RID: 11383
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OleDrop(IDataObject pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] NativeMethods.POINTL pt, [MarshalAs(UnmanagedType.I4)] [In] [Out] ref int pdwEffect);
		}

		// Token: 0x020004E2 RID: 1250
		[ComVisible(true)]
		[Guid("B722BCCB-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleCommandTarget
		{
			// Token: 0x06002C78 RID: 11384
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int QueryStatus(ref Guid pguidCmdGroup, int cCmds, [In] [Out] NativeMethods.OLECMD prgCmds, [In] [Out] string pCmdText);

			// Token: 0x06002C79 RID: 11385
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Exec(ref Guid pguidCmdGroup, int nCmdID, int nCmdexecopt, [MarshalAs(UnmanagedType.LPArray)] [In] object[] pvaIn, IntPtr pvaOut);
		}

		// Token: 0x020004E3 RID: 1251
		[ComVisible(true)]
		[Guid("00000116-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceFrame
		{
			// Token: 0x06002C7A RID: 11386
			IntPtr GetWindow();

			// Token: 0x06002C7B RID: 11387
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);

			// Token: 0x06002C7C RID: 11388
			void GetBorder([Out] NativeMethods.COMRECT lprectBorder);

			// Token: 0x06002C7D RID: 11389
			void RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x06002C7E RID: 11390
			void SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x06002C7F RID: 11391
			void SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);

			// Token: 0x06002C80 RID: 11392
			void InsertMenus([In] IntPtr hmenuShared, [In] [Out] object lpMenuWidths);

			// Token: 0x06002C81 RID: 11393
			void SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);

			// Token: 0x06002C82 RID: 11394
			void RemoveMenus([In] IntPtr hmenuShared);

			// Token: 0x06002C83 RID: 11395
			void SetStatusText([MarshalAs(UnmanagedType.BStr)] [In] string pszStatusText);

			// Token: 0x06002C84 RID: 11396
			void EnableModeless([MarshalAs(UnmanagedType.I4)] [In] int fEnable);

			// Token: 0x06002C85 RID: 11397
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([In] ref NativeMethods.MSG lpmsg, [MarshalAs(UnmanagedType.U2)] [In] short wID);
		}

		// Token: 0x020004E4 RID: 1252
		[ComVisible(true)]
		[Guid("00000115-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceUIWindow
		{
			// Token: 0x06002C86 RID: 11398
			IntPtr GetWindow();

			// Token: 0x06002C87 RID: 11399
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);

			// Token: 0x06002C88 RID: 11400
			void GetBorder([Out] NativeMethods.COMRECT lprectBorder);

			// Token: 0x06002C89 RID: 11401
			void RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x06002C8A RID: 11402
			void SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);

			// Token: 0x06002C8B RID: 11403
			void SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);
		}

		// Token: 0x020004E5 RID: 1253
		[ComVisible(true)]
		[Guid("00000117-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceActiveObject
		{
			// Token: 0x06002C8C RID: 11404
			int GetWindow(out IntPtr hwnd);

			// Token: 0x06002C8D RID: 11405
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);

			// Token: 0x06002C8E RID: 11406
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([In] ref NativeMethods.MSG lpmsg);

			// Token: 0x06002C8F RID: 11407
			void OnFrameWindowActivate([MarshalAs(UnmanagedType.I4)] [In] int fActivate);

			// Token: 0x06002C90 RID: 11408
			void OnDocWindowActivate([MarshalAs(UnmanagedType.I4)] [In] int fActivate);

			// Token: 0x06002C91 RID: 11409
			void ResizeBorder([In] NativeMethods.COMRECT prcBorder, [In] NativeMethods.IOleInPlaceUIWindow pUIWindow, [MarshalAs(UnmanagedType.I4)] [In] int fFrameWindow);

			// Token: 0x06002C92 RID: 11410
			void EnableModeless([MarshalAs(UnmanagedType.I4)] [In] int fEnable);
		}

		// Token: 0x020004E6 RID: 1254
		[ComVisible(true)]
		[Guid("0000011B-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleContainer
		{
			// Token: 0x06002C93 RID: 11411
			void ParseDisplayName([MarshalAs(UnmanagedType.Interface)] [In] object pbc, [MarshalAs(UnmanagedType.BStr)] [In] string pszDisplayName, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pchEaten, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] ppmkOut);

			// Token: 0x06002C94 RID: 11412
			void EnumObjects([MarshalAs(UnmanagedType.U4)] [In] int grfFlags, [MarshalAs(UnmanagedType.Interface)] out object ppenum);

			// Token: 0x06002C95 RID: 11413
			void LockContainer([MarshalAs(UnmanagedType.I4)] [In] int fLock);
		}

		// Token: 0x020004E7 RID: 1255
		[ComVisible(true)]
		[Guid("00000118-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleClientSite
		{
			// Token: 0x06002C96 RID: 11414
			void SaveObject();

			// Token: 0x06002C97 RID: 11415
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwAssign, [MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker);

			// Token: 0x06002C98 RID: 11416
			[PreserveSig]
			int GetContainer(out NativeMethods.IOleContainer ppContainer);

			// Token: 0x06002C99 RID: 11417
			void ShowObject();

			// Token: 0x06002C9A RID: 11418
			void OnShowWindow([MarshalAs(UnmanagedType.I4)] [In] int fShow);

			// Token: 0x06002C9B RID: 11419
			void RequestNewObjectLayout();
		}

		// Token: 0x020004E8 RID: 1256
		[ComVisible(true)]
		[Guid("B722BCC7-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDocumentSite
		{
			// Token: 0x06002C9C RID: 11420
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ActivateMe([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleDocumentView pViewToActivate);
		}

		// Token: 0x020004E9 RID: 1257
		[ComVisible(true)]
		[Guid("B722BCC6-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDocumentView
		{
			// Token: 0x06002C9D RID: 11421
			void SetInPlaceSite([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceSite pIPSite);

			// Token: 0x06002C9E RID: 11422
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IOleInPlaceSite GetInPlaceSite();

			// Token: 0x06002C9F RID: 11423
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetDocument();

			// Token: 0x06002CA0 RID: 11424
			void SetRect([In] NativeMethods.COMRECT prcView);

			// Token: 0x06002CA1 RID: 11425
			void GetRect([Out] NativeMethods.COMRECT prcView);

			// Token: 0x06002CA2 RID: 11426
			void SetRectComplex([In] NativeMethods.COMRECT prcView, [In] NativeMethods.COMRECT prcHScroll, [In] NativeMethods.COMRECT prcVScroll, [In] NativeMethods.COMRECT prcSizeBox);

			// Token: 0x06002CA3 RID: 11427
			void Show([MarshalAs(UnmanagedType.I4)] [In] int fShow);

			// Token: 0x06002CA4 RID: 11428
			void UIActivate([MarshalAs(UnmanagedType.I4)] [In] int fUIActivate);

			// Token: 0x06002CA5 RID: 11429
			void Open();

			// Token: 0x06002CA6 RID: 11430
			void CloseView([MarshalAs(UnmanagedType.U4)] [In] int dwReserved);

			// Token: 0x06002CA7 RID: 11431
			void SaveViewState([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm);

			// Token: 0x06002CA8 RID: 11432
			void ApplyViewState([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm);

			// Token: 0x06002CA9 RID: 11433
			void Clone([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleInPlaceSite pIPSiteNew, [MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.IOleDocumentView[] ppViewNew);
		}

		// Token: 0x020004EA RID: 1258
		[ComVisible(true)]
		[Guid("00000119-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceSite
		{
			// Token: 0x06002CAA RID: 11434
			IntPtr GetWindow();

			// Token: 0x06002CAB RID: 11435
			void ContextSensitiveHelp([MarshalAs(UnmanagedType.I4)] [In] int fEnterMode);

			// Token: 0x06002CAC RID: 11436
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int CanInPlaceActivate();

			// Token: 0x06002CAD RID: 11437
			void OnInPlaceActivate();

			// Token: 0x06002CAE RID: 11438
			void OnUIActivate();

			// Token: 0x06002CAF RID: 11439
			void GetWindowContext(out NativeMethods.IOleInPlaceFrame ppFrame, out NativeMethods.IOleInPlaceUIWindow ppDoc, [Out] NativeMethods.COMRECT lprcPosRect, [Out] NativeMethods.COMRECT lprcClipRect, [In] [Out] NativeMethods.tagOIFI lpFrameInfo);

			// Token: 0x06002CB0 RID: 11440
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Scroll([MarshalAs(UnmanagedType.U4)] [In] NativeMethods.tagSIZE scrollExtant);

			// Token: 0x06002CB1 RID: 11441
			void OnUIDeactivate([MarshalAs(UnmanagedType.I4)] [In] int fUndoable);

			// Token: 0x06002CB2 RID: 11442
			void OnInPlaceDeactivate();

			// Token: 0x06002CB3 RID: 11443
			void DiscardUndoState();

			// Token: 0x06002CB4 RID: 11444
			void DeactivateAndUndo();

			// Token: 0x06002CB5 RID: 11445
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnPosRectChange([In] NativeMethods.COMRECT lprcPosRect);
		}

		// Token: 0x020004EB RID: 1259
		[ComVisible(true)]
		[Guid("0000000C-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStream
		{
			// Token: 0x06002CB6 RID: 11446
			[return: MarshalAs(UnmanagedType.I4)]
			int Read([In] IntPtr buf, [MarshalAs(UnmanagedType.I4)] [In] int len);

			// Token: 0x06002CB7 RID: 11447
			[return: MarshalAs(UnmanagedType.I4)]
			int Write([In] IntPtr buf, [MarshalAs(UnmanagedType.I4)] [In] int len);

			// Token: 0x06002CB8 RID: 11448
			[return: MarshalAs(UnmanagedType.I8)]
			long Seek([MarshalAs(UnmanagedType.I8)] [In] long dlibMove, [MarshalAs(UnmanagedType.I4)] [In] int dwOrigin);

			// Token: 0x06002CB9 RID: 11449
			void SetSize([MarshalAs(UnmanagedType.I8)] [In] long libNewSize);

			// Token: 0x06002CBA RID: 11450
			[return: MarshalAs(UnmanagedType.I8)]
			long CopyTo([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.LPArray)] [Out] long[] pcbRead);

			// Token: 0x06002CBB RID: 11451
			void Commit([MarshalAs(UnmanagedType.I4)] [In] int grfCommitFlags);

			// Token: 0x06002CBC RID: 11452
			void Revert();

			// Token: 0x06002CBD RID: 11453
			void LockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.I4)] [In] int dwLockType);

			// Token: 0x06002CBE RID: 11454
			void UnlockRegion([MarshalAs(UnmanagedType.I8)] [In] long libOffset, [MarshalAs(UnmanagedType.I8)] [In] long cb, [MarshalAs(UnmanagedType.I4)] [In] int dwLockType);

			// Token: 0x06002CBF RID: 11455
			void Stat([In] IntPtr pStatstg, [MarshalAs(UnmanagedType.I4)] [In] int grfStatFlag);

			// Token: 0x06002CC0 RID: 11456
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IStream Clone();
		}

		// Token: 0x020004EC RID: 1260
		[ComVisible(true)]
		[Guid("00000112-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleObject
		{
			// Token: 0x06002CC1 RID: 11457
			[PreserveSig]
			int SetClientSite([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleClientSite pClientSite);

			// Token: 0x06002CC2 RID: 11458
			[PreserveSig]
			int GetClientSite(out NativeMethods.IOleClientSite site);

			// Token: 0x06002CC3 RID: 11459
			[PreserveSig]
			int SetHostNames([MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerApp, [MarshalAs(UnmanagedType.LPWStr)] [In] string szContainerObj);

			// Token: 0x06002CC4 RID: 11460
			[PreserveSig]
			int Close([MarshalAs(UnmanagedType.I4)] [In] int dwSaveOption);

			// Token: 0x06002CC5 RID: 11461
			[PreserveSig]
			int SetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] [In] object pmk);

			// Token: 0x06002CC6 RID: 11462
			[PreserveSig]
			int GetMoniker([MarshalAs(UnmanagedType.U4)] [In] int dwAssign, [MarshalAs(UnmanagedType.U4)] [In] int dwWhichMoniker, out object moniker);

			// Token: 0x06002CC7 RID: 11463
			[PreserveSig]
			int InitFromData(IDataObject pDataObject, [MarshalAs(UnmanagedType.I4)] [In] int fCreation, [MarshalAs(UnmanagedType.U4)] [In] int dwReserved);

			// Token: 0x06002CC8 RID: 11464
			[PreserveSig]
			int GetClipboardData([MarshalAs(UnmanagedType.U4)] [In] int dwReserved, out IDataObject data);

			// Token: 0x06002CC9 RID: 11465
			[PreserveSig]
			int DoVerb([MarshalAs(UnmanagedType.I4)] [In] int iVerb, [In] IntPtr lpmsg, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IOleClientSite pActiveSite, [MarshalAs(UnmanagedType.I4)] [In] int lindex, [In] IntPtr hwndParent, [In] NativeMethods.COMRECT lprcPosRect);

			// Token: 0x06002CCA RID: 11466
			[PreserveSig]
			int EnumVerbs(out NativeMethods.IEnumOLEVERB e);

			// Token: 0x06002CCB RID: 11467
			[PreserveSig]
			int OleUpdate();

			// Token: 0x06002CCC RID: 11468
			[PreserveSig]
			int IsUpToDate();

			// Token: 0x06002CCD RID: 11469
			[PreserveSig]
			int GetUserClassID([In] [Out] ref Guid pClsid);

			// Token: 0x06002CCE RID: 11470
			[PreserveSig]
			int GetUserType([MarshalAs(UnmanagedType.U4)] [In] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

			// Token: 0x06002CCF RID: 11471
			[PreserveSig]
			int SetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [In] NativeMethods.tagSIZEL pSizel);

			// Token: 0x06002CD0 RID: 11472
			[PreserveSig]
			int GetExtent([MarshalAs(UnmanagedType.U4)] [In] int dwDrawAspect, [Out] NativeMethods.tagSIZEL pSizel);

			// Token: 0x06002CD1 RID: 11473
			[PreserveSig]
			int Advise([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IAdviseSink pAdvSink, out int cookie);

			// Token: 0x06002CD2 RID: 11474
			[PreserveSig]
			int Unadvise([MarshalAs(UnmanagedType.U4)] [In] int dwConnection);

			// Token: 0x06002CD3 RID: 11475
			[PreserveSig]
			int EnumAdvise(out object e);

			// Token: 0x06002CD4 RID: 11476
			[PreserveSig]
			int GetMiscStatus([MarshalAs(UnmanagedType.U4)] [In] int dwAspect, out int misc);

			// Token: 0x06002CD5 RID: 11477
			[PreserveSig]
			int SetColorScheme([In] NativeMethods.tagLOGPALETTE pLogpal);
		}

		// Token: 0x020004ED RID: 1261
		[ComVisible(true)]
		[Guid("0000010F-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IAdviseSink
		{
			// Token: 0x06002CD6 RID: 11478
			void OnDataChange([In] NativeMethods.FORMATETC pFormatetc, [In] NativeMethods.STGMEDIUM pStgmed);

			// Token: 0x06002CD7 RID: 11479
			void OnViewChange([MarshalAs(UnmanagedType.U4)] [In] int dwAspect, [MarshalAs(UnmanagedType.I4)] [In] int lindex);

			// Token: 0x06002CD8 RID: 11480
			void OnRename([MarshalAs(UnmanagedType.Interface)] [In] object pmk);

			// Token: 0x06002CD9 RID: 11481
			void OnSave();

			// Token: 0x06002CDA RID: 11482
			void OnClose();
		}

		// Token: 0x020004EE RID: 1262
		[ComVisible(true)]
		[Guid("7FD52380-4E07-101B-AE2D-08002B2EC713")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IPersistStreamInit
		{
			// Token: 0x06002CDB RID: 11483
			void GetClassID([In] [Out] ref Guid pClassID);

			// Token: 0x06002CDC RID: 11484
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int IsDirty();

			// Token: 0x06002CDD RID: 11485
			void Load([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm);

			// Token: 0x06002CDE RID: 11486
			void Save([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IStream pstm, [MarshalAs(UnmanagedType.Bool)] [In] bool fClearDirty);

			// Token: 0x06002CDF RID: 11487
			void GetSizeMax([MarshalAs(UnmanagedType.LPArray)] [Out] long pcbSize);

			// Token: 0x06002CE0 RID: 11488
			void InitNew();
		}

		// Token: 0x020004EF RID: 1263
		[ComVisible(true)]
		[Guid("25336920-03F9-11CF-8FD0-00AA00686F13")]
		[ComImport]
		public class HTMLDocument
		{
			// Token: 0x06002CE1 RID: 11489
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern HTMLDocument();
		}

		// Token: 0x020004F0 RID: 1264
		[ComVisible(true)]
		[Guid("626FC520-A41E-11CF-A731-00A0C9082637")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLDocument
		{
			// Token: 0x06002CE2 RID: 11490
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetScript();
		}

		// Token: 0x020004F1 RID: 1265
		[ComVisible(true)]
		[Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLDocument2
		{
			// Token: 0x06002CE3 RID: 11491
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetScript();

			// Token: 0x06002CE4 RID: 11492
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetAll();

			// Token: 0x06002CE5 RID: 11493
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetBody();

			// Token: 0x06002CE6 RID: 11494
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetActiveElement();

			// Token: 0x06002CE7 RID: 11495
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetImages();

			// Token: 0x06002CE8 RID: 11496
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetApplets();

			// Token: 0x06002CE9 RID: 11497
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetLinks();

			// Token: 0x06002CEA RID: 11498
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetForms();

			// Token: 0x06002CEB RID: 11499
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetAnchors();

			// Token: 0x06002CEC RID: 11500
			void SetTitle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002CED RID: 11501
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTitle();

			// Token: 0x06002CEE RID: 11502
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetScripts();

			// Token: 0x06002CEF RID: 11503
			void SetDesignMode([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002CF0 RID: 11504
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDesignMode();

			// Token: 0x06002CF1 RID: 11505
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetSelection();

			// Token: 0x06002CF2 RID: 11506
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetReadyState();

			// Token: 0x06002CF3 RID: 11507
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetFrames();

			// Token: 0x06002CF4 RID: 11508
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetEmbeds();

			// Token: 0x06002CF5 RID: 11509
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetPlugins();

			// Token: 0x06002CF6 RID: 11510
			void SetAlinkColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002CF7 RID: 11511
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetAlinkColor();

			// Token: 0x06002CF8 RID: 11512
			void SetBgColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002CF9 RID: 11513
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBgColor();

			// Token: 0x06002CFA RID: 11514
			void SetFgColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002CFB RID: 11515
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFgColor();

			// Token: 0x06002CFC RID: 11516
			void SetLinkColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002CFD RID: 11517
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLinkColor();

			// Token: 0x06002CFE RID: 11518
			void SetVlinkColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002CFF RID: 11519
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVlinkColor();

			// Token: 0x06002D00 RID: 11520
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetReferrer();

			// Token: 0x06002D01 RID: 11521
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetLocation();

			// Token: 0x06002D02 RID: 11522
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLastModified();

			// Token: 0x06002D03 RID: 11523
			void SetURL([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D04 RID: 11524
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetURL();

			// Token: 0x06002D05 RID: 11525
			void SetDomain([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D06 RID: 11526
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDomain();

			// Token: 0x06002D07 RID: 11527
			void SetCookie([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D08 RID: 11528
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCookie();

			// Token: 0x06002D09 RID: 11529
			void SetExpando([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002D0A RID: 11530
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetExpando();

			// Token: 0x06002D0B RID: 11531
			void SetCharset([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D0C RID: 11532
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCharset();

			// Token: 0x06002D0D RID: 11533
			void SetDefaultCharset([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D0E RID: 11534
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDefaultCharset();

			// Token: 0x06002D0F RID: 11535
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetMimeType();

			// Token: 0x06002D10 RID: 11536
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileSize();

			// Token: 0x06002D11 RID: 11537
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileCreatedDate();

			// Token: 0x06002D12 RID: 11538
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileModifiedDate();

			// Token: 0x06002D13 RID: 11539
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFileUpdatedDate();

			// Token: 0x06002D14 RID: 11540
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetSecurity();

			// Token: 0x06002D15 RID: 11541
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetProtocol();

			// Token: 0x06002D16 RID: 11542
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetNameProp();

			// Token: 0x06002D17 RID: 11543
			void DummyWrite([MarshalAs(UnmanagedType.I4)] [In] int psarray);

			// Token: 0x06002D18 RID: 11544
			void DummyWriteln([MarshalAs(UnmanagedType.I4)] [In] int psarray);

			// Token: 0x06002D19 RID: 11545
			[return: MarshalAs(UnmanagedType.Interface)]
			object Open([MarshalAs(UnmanagedType.BStr)] [In] string URL, [MarshalAs(UnmanagedType.Struct)] [In] object name, [MarshalAs(UnmanagedType.Struct)] [In] object features, [MarshalAs(UnmanagedType.Struct)] [In] object replace);

			// Token: 0x06002D1A RID: 11546
			void Close();

			// Token: 0x06002D1B RID: 11547
			void Clear();

			// Token: 0x06002D1C RID: 11548
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandSupported([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

			// Token: 0x06002D1D RID: 11549
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandEnabled([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

			// Token: 0x06002D1E RID: 11550
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandState([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

			// Token: 0x06002D1F RID: 11551
			[return: MarshalAs(UnmanagedType.Bool)]
			bool QueryCommandIndeterm([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

			// Token: 0x06002D20 RID: 11552
			[return: MarshalAs(UnmanagedType.BStr)]
			string QueryCommandText([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

			// Token: 0x06002D21 RID: 11553
			[return: MarshalAs(UnmanagedType.Struct)]
			object QueryCommandValue([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

			// Token: 0x06002D22 RID: 11554
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommand([MarshalAs(UnmanagedType.BStr)] [In] string cmdID, [MarshalAs(UnmanagedType.Bool)] [In] bool showUI, [MarshalAs(UnmanagedType.Struct)] [In] object value);

			// Token: 0x06002D23 RID: 11555
			[return: MarshalAs(UnmanagedType.Bool)]
			bool ExecCommandShowHelp([MarshalAs(UnmanagedType.BStr)] [In] string cmdID);

			// Token: 0x06002D24 RID: 11556
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement CreateElement([MarshalAs(UnmanagedType.BStr)] [In] string eTag);

			// Token: 0x06002D25 RID: 11557
			void SetOnhelp([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D26 RID: 11558
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnhelp();

			// Token: 0x06002D27 RID: 11559
			void SetOnclick([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D28 RID: 11560
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnclick();

			// Token: 0x06002D29 RID: 11561
			void SetOndblclick([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D2A RID: 11562
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndblclick();

			// Token: 0x06002D2B RID: 11563
			void SetOnkeyup([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D2C RID: 11564
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeyup();

			// Token: 0x06002D2D RID: 11565
			void SetOnkeydown([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D2E RID: 11566
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeydown();

			// Token: 0x06002D2F RID: 11567
			void SetOnkeypress([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D30 RID: 11568
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeypress();

			// Token: 0x06002D31 RID: 11569
			void SetOnmouseup([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D32 RID: 11570
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseup();

			// Token: 0x06002D33 RID: 11571
			void SetOnmousedown([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D34 RID: 11572
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousedown();

			// Token: 0x06002D35 RID: 11573
			void SetOnmousemove([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D36 RID: 11574
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousemove();

			// Token: 0x06002D37 RID: 11575
			void SetOnmouseout([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D38 RID: 11576
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseout();

			// Token: 0x06002D39 RID: 11577
			void SetOnmouseover([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D3A RID: 11578
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseover();

			// Token: 0x06002D3B RID: 11579
			void SetOnreadystatechange([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D3C RID: 11580
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnreadystatechange();

			// Token: 0x06002D3D RID: 11581
			void SetOnafterupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D3E RID: 11582
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnafterupdate();

			// Token: 0x06002D3F RID: 11583
			void SetOnrowexit([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D40 RID: 11584
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowexit();

			// Token: 0x06002D41 RID: 11585
			void SetOnrowenter([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D42 RID: 11586
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowenter();

			// Token: 0x06002D43 RID: 11587
			void SetOndragstart([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D44 RID: 11588
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragstart();

			// Token: 0x06002D45 RID: 11589
			void SetOnselectstart([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D46 RID: 11590
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnselectstart();

			// Token: 0x06002D47 RID: 11591
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement ElementFromPoint([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);

			// Token: 0x06002D48 RID: 11592
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetParentWindow();

			// Token: 0x06002D49 RID: 11593
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetStyleSheets();

			// Token: 0x06002D4A RID: 11594
			void SetOnbeforeupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D4B RID: 11595
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeupdate();

			// Token: 0x06002D4C RID: 11596
			void SetOnerrorupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D4D RID: 11597
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnerrorupdate();

			// Token: 0x06002D4E RID: 11598
			[return: MarshalAs(UnmanagedType.BStr)]
			string toString();

			// Token: 0x06002D4F RID: 11599
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheet CreateStyleSheet([MarshalAs(UnmanagedType.BStr)] [In] string bstrHref, [MarshalAs(UnmanagedType.I4)] [In] int lIndex);
		}

		// Token: 0x020004F2 RID: 1266
		[ComVisible(true)]
		[Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLElement
		{
			// Token: 0x06002D50 RID: 11600
			void SetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.Struct)] [In] object AttributeValue, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);

			// Token: 0x06002D51 RID: 11601
			void GetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags, [MarshalAs(UnmanagedType.LPArray)] [Out] object[] pvars);

			// Token: 0x06002D52 RID: 11602
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);

			// Token: 0x06002D53 RID: 11603
			void SetClassName([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D54 RID: 11604
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClassName();

			// Token: 0x06002D55 RID: 11605
			void SetId([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D56 RID: 11606
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetId();

			// Token: 0x06002D57 RID: 11607
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTagName();

			// Token: 0x06002D58 RID: 11608
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetParentElement();

			// Token: 0x06002D59 RID: 11609
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetStyle();

			// Token: 0x06002D5A RID: 11610
			void SetOnhelp([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D5B RID: 11611
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnhelp();

			// Token: 0x06002D5C RID: 11612
			void SetOnclick([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D5D RID: 11613
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnclick();

			// Token: 0x06002D5E RID: 11614
			void SetOndblclick([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D5F RID: 11615
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndblclick();

			// Token: 0x06002D60 RID: 11616
			void SetOnkeydown([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D61 RID: 11617
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeydown();

			// Token: 0x06002D62 RID: 11618
			void SetOnkeyup([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D63 RID: 11619
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeyup();

			// Token: 0x06002D64 RID: 11620
			void SetOnkeypress([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D65 RID: 11621
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeypress();

			// Token: 0x06002D66 RID: 11622
			void SetOnmouseout([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D67 RID: 11623
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseout();

			// Token: 0x06002D68 RID: 11624
			void SetOnmouseover([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D69 RID: 11625
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseover();

			// Token: 0x06002D6A RID: 11626
			void SetOnmousemove([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D6B RID: 11627
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousemove();

			// Token: 0x06002D6C RID: 11628
			void SetOnmousedown([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D6D RID: 11629
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousedown();

			// Token: 0x06002D6E RID: 11630
			void SetOnmouseup([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D6F RID: 11631
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmouseup();

			// Token: 0x06002D70 RID: 11632
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDocument2 GetDocument();

			// Token: 0x06002D71 RID: 11633
			void SetTitle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D72 RID: 11634
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTitle();

			// Token: 0x06002D73 RID: 11635
			void SetLanguage([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D74 RID: 11636
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLanguage();

			// Token: 0x06002D75 RID: 11637
			void SetOnselectstart([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D76 RID: 11638
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnselectstart();

			// Token: 0x06002D77 RID: 11639
			void ScrollIntoView([MarshalAs(UnmanagedType.Struct)] [In] object varargStart);

			// Token: 0x06002D78 RID: 11640
			[return: MarshalAs(UnmanagedType.Bool)]
			bool Contains([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement pChild);

			// Token: 0x06002D79 RID: 11641
			[return: MarshalAs(UnmanagedType.I4)]
			int GetSourceIndex();

			// Token: 0x06002D7A RID: 11642
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetRecordNumber();

			// Token: 0x06002D7B RID: 11643
			void SetLang([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D7C RID: 11644
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetLang();

			// Token: 0x06002D7D RID: 11645
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetLeft();

			// Token: 0x06002D7E RID: 11646
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetTop();

			// Token: 0x06002D7F RID: 11647
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetWidth();

			// Token: 0x06002D80 RID: 11648
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOffsetHeight();

			// Token: 0x06002D81 RID: 11649
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetOffsetParent();

			// Token: 0x06002D82 RID: 11650
			void SetInnerHTML([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D83 RID: 11651
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetInnerHTML();

			// Token: 0x06002D84 RID: 11652
			void SetInnerText([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D85 RID: 11653
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetInnerText();

			// Token: 0x06002D86 RID: 11654
			void SetOuterHTML([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D87 RID: 11655
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOuterHTML();

			// Token: 0x06002D88 RID: 11656
			void SetOuterText([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002D89 RID: 11657
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOuterText();

			// Token: 0x06002D8A RID: 11658
			void InsertAdjacentHTML([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.BStr)] [In] string html);

			// Token: 0x06002D8B RID: 11659
			void InsertAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.BStr)] [In] string text);

			// Token: 0x06002D8C RID: 11660
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetParentTextEdit();

			// Token: 0x06002D8D RID: 11661
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetIsTextEdit();

			// Token: 0x06002D8E RID: 11662
			void Click();

			// Token: 0x06002D8F RID: 11663
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetFilters();

			// Token: 0x06002D90 RID: 11664
			void SetOndragstart([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D91 RID: 11665
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragstart();

			// Token: 0x06002D92 RID: 11666
			[return: MarshalAs(UnmanagedType.BStr)]
			string toString();

			// Token: 0x06002D93 RID: 11667
			void SetOnbeforeupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D94 RID: 11668
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeupdate();

			// Token: 0x06002D95 RID: 11669
			void SetOnafterupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D96 RID: 11670
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnafterupdate();

			// Token: 0x06002D97 RID: 11671
			void SetOnerrorupdate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D98 RID: 11672
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnerrorupdate();

			// Token: 0x06002D99 RID: 11673
			void SetOnrowexit([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D9A RID: 11674
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowexit();

			// Token: 0x06002D9B RID: 11675
			void SetOnrowenter([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D9C RID: 11676
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowenter();

			// Token: 0x06002D9D RID: 11677
			void SetOndatasetchanged([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002D9E RID: 11678
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndatasetchanged();

			// Token: 0x06002D9F RID: 11679
			void SetOndataavailable([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DA0 RID: 11680
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndataavailable();

			// Token: 0x06002DA1 RID: 11681
			void SetOndatasetcomplete([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DA2 RID: 11682
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndatasetcomplete();

			// Token: 0x06002DA3 RID: 11683
			void SetOnfilterchange([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DA4 RID: 11684
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnfilterchange();

			// Token: 0x06002DA5 RID: 11685
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetChildren();

			// Token: 0x06002DA6 RID: 11686
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetAll();
		}

		// Token: 0x020004F3 RID: 1267
		[Guid("3050F434-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLElement2
		{
			// Token: 0x06002DA7 RID: 11687
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetScopeName();

			// Token: 0x06002DA8 RID: 11688
			void SetCapture([MarshalAs(UnmanagedType.Bool)] [In] bool containerCapture);

			// Token: 0x06002DA9 RID: 11689
			void ReleaseCapture();

			// Token: 0x06002DAA RID: 11690
			void SetOnlosecapture([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DAB RID: 11691
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnlosecapture();

			// Token: 0x06002DAC RID: 11692
			[return: MarshalAs(UnmanagedType.BStr)]
			string ComponentFromPoint([MarshalAs(UnmanagedType.I4)] [In] int x, [MarshalAs(UnmanagedType.I4)] [In] int y);

			// Token: 0x06002DAD RID: 11693
			void DoScroll([MarshalAs(UnmanagedType.Struct)] [In] object component);

			// Token: 0x06002DAE RID: 11694
			void SetOnscroll([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DAF RID: 11695
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnscroll();

			// Token: 0x06002DB0 RID: 11696
			void SetOndrag([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DB1 RID: 11697
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndrag();

			// Token: 0x06002DB2 RID: 11698
			void SetOndragend([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DB3 RID: 11699
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragend();

			// Token: 0x06002DB4 RID: 11700
			void SetOndragenter([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DB5 RID: 11701
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragenter();

			// Token: 0x06002DB6 RID: 11702
			void SetOndragover([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DB7 RID: 11703
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragover();

			// Token: 0x06002DB8 RID: 11704
			void SetOndragleave([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DB9 RID: 11705
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndragleave();

			// Token: 0x06002DBA RID: 11706
			void SetOndrop([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DBB RID: 11707
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOndrop();

			// Token: 0x06002DBC RID: 11708
			void SetOnbeforecut([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DBD RID: 11709
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforecut();

			// Token: 0x06002DBE RID: 11710
			void SetOncut([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DBF RID: 11711
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncut();

			// Token: 0x06002DC0 RID: 11712
			void SetOnbeforecopy([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DC1 RID: 11713
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforecopy();

			// Token: 0x06002DC2 RID: 11714
			void SetOncopy([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DC3 RID: 11715
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncopy();

			// Token: 0x06002DC4 RID: 11716
			void SetOnbeforepaste([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DC5 RID: 11717
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforepaste();

			// Token: 0x06002DC6 RID: 11718
			void SetOnpaste([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DC7 RID: 11719
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnpaste();

			// Token: 0x06002DC8 RID: 11720
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLCurrentStyle GetCurrentStyle();

			// Token: 0x06002DC9 RID: 11721
			void SetOnpropertychange([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DCA RID: 11722
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnpropertychange();

			// Token: 0x06002DCB RID: 11723
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLRectCollection GetClientRects();

			// Token: 0x06002DCC RID: 11724
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLRect GetBoundingClientRect();

			// Token: 0x06002DCD RID: 11725
			void SetExpression([MarshalAs(UnmanagedType.BStr)] [In] string propname, [MarshalAs(UnmanagedType.BStr)] [In] string expression, [MarshalAs(UnmanagedType.BStr)] [In] string language);

			// Token: 0x06002DCE RID: 11726
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetExpression([MarshalAs(UnmanagedType.BStr)] [In] object propname);

			// Token: 0x06002DCF RID: 11727
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveExpression([MarshalAs(UnmanagedType.BStr)] [In] string propname);

			// Token: 0x06002DD0 RID: 11728
			void SetTabIndex([MarshalAs(UnmanagedType.I2)] [In] short p);

			// Token: 0x06002DD1 RID: 11729
			[return: MarshalAs(UnmanagedType.I2)]
			short GetTabIndex();

			// Token: 0x06002DD2 RID: 11730
			void Focus();

			// Token: 0x06002DD3 RID: 11731
			void SetAccessKey([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002DD4 RID: 11732
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetAccessKey();

			// Token: 0x06002DD5 RID: 11733
			void SetOnblur([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DD6 RID: 11734
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnblur();

			// Token: 0x06002DD7 RID: 11735
			void SetOnfocus([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DD8 RID: 11736
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnfocus();

			// Token: 0x06002DD9 RID: 11737
			void SetOnresize([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DDA RID: 11738
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnresize();

			// Token: 0x06002DDB RID: 11739
			void Blur();

			// Token: 0x06002DDC RID: 11740
			void AddFilter([MarshalAs(UnmanagedType.Interface)] [In] object pUnk);

			// Token: 0x06002DDD RID: 11741
			void RemoveFilter([MarshalAs(UnmanagedType.Interface)] [In] object pUnk);

			// Token: 0x06002DDE RID: 11742
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientHeight();

			// Token: 0x06002DDF RID: 11743
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientWidth();

			// Token: 0x06002DE0 RID: 11744
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientTop();

			// Token: 0x06002DE1 RID: 11745
			[return: MarshalAs(UnmanagedType.I4)]
			int GetClientLeft();

			// Token: 0x06002DE2 RID: 11746
			[return: MarshalAs(UnmanagedType.Bool)]
			bool AttachEvent([MarshalAs(UnmanagedType.BStr)] [In] string ev, [MarshalAs(UnmanagedType.Interface)] [In] object pdisp);

			// Token: 0x06002DE3 RID: 11747
			void DetachEvent([MarshalAs(UnmanagedType.BStr)] [In] string ev, [MarshalAs(UnmanagedType.Interface)] [In] object pdisp);

			// Token: 0x06002DE4 RID: 11748
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetReadyState();

			// Token: 0x06002DE5 RID: 11749
			void SetOnreadystatechange([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DE6 RID: 11750
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnreadystatechange();

			// Token: 0x06002DE7 RID: 11751
			void SetOnrowsdelete([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DE8 RID: 11752
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowsdelete();

			// Token: 0x06002DE9 RID: 11753
			void SetOnrowsinserted([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DEA RID: 11754
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnrowsinserted();

			// Token: 0x06002DEB RID: 11755
			void SetOncellchange([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DEC RID: 11756
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncellchange();

			// Token: 0x06002DED RID: 11757
			void SetDir([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002DEE RID: 11758
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDir();

			// Token: 0x06002DEF RID: 11759
			[return: MarshalAs(UnmanagedType.Interface)]
			object CreateControlRange();

			// Token: 0x06002DF0 RID: 11760
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollHeight();

			// Token: 0x06002DF1 RID: 11761
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollWidth();

			// Token: 0x06002DF2 RID: 11762
			void SetScrollTop([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002DF3 RID: 11763
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollTop();

			// Token: 0x06002DF4 RID: 11764
			void SetScrollLeft([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002DF5 RID: 11765
			[return: MarshalAs(UnmanagedType.I4)]
			int GetScrollLeft();

			// Token: 0x06002DF6 RID: 11766
			void ClearAttributes();

			// Token: 0x06002DF7 RID: 11767
			void MergeAttributes([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement mergeThis);

			// Token: 0x06002DF8 RID: 11768
			void SetOncontextmenu([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002DF9 RID: 11769
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOncontextmenu();

			// Token: 0x06002DFA RID: 11770
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement InsertAdjacentElement([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement insertedElement);

			// Token: 0x06002DFB RID: 11771
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement ApplyElement([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement apply, [MarshalAs(UnmanagedType.BStr)] [In] string where);

			// Token: 0x06002DFC RID: 11772
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string where);

			// Token: 0x06002DFD RID: 11773
			[return: MarshalAs(UnmanagedType.BStr)]
			string ReplaceAdjacentText([MarshalAs(UnmanagedType.BStr)] [In] string where, [MarshalAs(UnmanagedType.BStr)] [In] string newText);

			// Token: 0x06002DFE RID: 11774
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetCanHaveChildren();

			// Token: 0x06002DFF RID: 11775
			[return: MarshalAs(UnmanagedType.I4)]
			int AddBehavior([MarshalAs(UnmanagedType.BStr)] [In] string bstrUrl, [In] ref object pvarFactory);

			// Token: 0x06002E00 RID: 11776
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveBehavior([MarshalAs(UnmanagedType.I4)] [In] int cookie);

			// Token: 0x06002E01 RID: 11777
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetRuntimeStyle();

			// Token: 0x06002E02 RID: 11778
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetBehaviorUrns();

			// Token: 0x06002E03 RID: 11779
			void SetTagUrn([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E04 RID: 11780
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTagUrn();

			// Token: 0x06002E05 RID: 11781
			void SetOnbeforeeditfocus([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E06 RID: 11782
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeeditfocus();

			// Token: 0x06002E07 RID: 11783
			[return: MarshalAs(UnmanagedType.I4)]
			int GetReadyStateValue();

			// Token: 0x06002E08 RID: 11784
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElementCollection GetElementsByTagName([MarshalAs(UnmanagedType.BStr)] [In] string v);

			// Token: 0x06002E09 RID: 11785
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetBaseStyle();

			// Token: 0x06002E0A RID: 11786
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLCurrentStyle GetBaseCurrentStyle();

			// Token: 0x06002E0B RID: 11787
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyle GetBaseRuntimeStyle();

			// Token: 0x06002E0C RID: 11788
			void SetOnmousehover([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E0D RID: 11789
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnmousehover();

			// Token: 0x06002E0E RID: 11790
			void SetOnkeydownpreview([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E0F RID: 11791
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnkeydownpreview();

			// Token: 0x06002E10 RID: 11792
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetBehavior([MarshalAs(UnmanagedType.BStr)] [In] string bstrName, [MarshalAs(UnmanagedType.BStr)] [In] string bstrUrn);
		}

		// Token: 0x020004F4 RID: 1268
		[ComVisible(true)]
		[Guid("3050F673-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLElement3
		{
			// Token: 0x06002E11 RID: 11793
			void MergeAttributes([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLElement mergeThis, [MarshalAs(UnmanagedType.Struct)] [In] object pvarFlags);

			// Token: 0x06002E12 RID: 11794
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetIsMultiLine();

			// Token: 0x06002E13 RID: 11795
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetCanHaveHTML();

			// Token: 0x06002E14 RID: 11796
			void SetOnLayoutComplete([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E15 RID: 11797
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnLayoutComplete();

			// Token: 0x06002E16 RID: 11798
			void SetOnPage([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E17 RID: 11799
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnPage();

			// Token: 0x06002E18 RID: 11800
			void SetInflateBlock([MarshalAs(UnmanagedType.Bool)] [In] bool inflate);

			// Token: 0x06002E19 RID: 11801
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetInflateBlock();

			// Token: 0x06002E1A RID: 11802
			void SetOnBeforeDeactivate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E1B RID: 11803
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnBeforeDeactivate();

			// Token: 0x06002E1C RID: 11804
			void SetActive();

			// Token: 0x06002E1D RID: 11805
			void SetContentEditable([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E1E RID: 11806
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetContentEditable();

			// Token: 0x06002E1F RID: 11807
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetIsContentEditable();

			// Token: 0x06002E20 RID: 11808
			void SetHideFocus([MarshalAs(UnmanagedType.Bool)] [In] bool v);

			// Token: 0x06002E21 RID: 11809
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetHideFocus();

			// Token: 0x06002E22 RID: 11810
			void SetDisabled([MarshalAs(UnmanagedType.Bool)] [In] bool v);

			// Token: 0x06002E23 RID: 11811
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetDisabled();

			// Token: 0x06002E24 RID: 11812
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetIsDisabled();

			// Token: 0x06002E25 RID: 11813
			void SetOnMove([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E26 RID: 11814
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnMove();

			// Token: 0x06002E27 RID: 11815
			void SetOnControlSelect([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E28 RID: 11816
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnControlSelect();

			// Token: 0x06002E29 RID: 11817
			[return: MarshalAs(UnmanagedType.Bool)]
			bool FireEvent([MarshalAs(UnmanagedType.BStr)] [In] string eventName, [MarshalAs(UnmanagedType.Struct)] [In] object eventObject);

			// Token: 0x06002E2A RID: 11818
			void SetOnResizeStart([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E2B RID: 11819
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnResizeStart();

			// Token: 0x06002E2C RID: 11820
			void SetOnResizeEnd([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E2D RID: 11821
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnResizeEnd();

			// Token: 0x06002E2E RID: 11822
			void SetOnMoveStart([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E2F RID: 11823
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnMoveStart();

			// Token: 0x06002E30 RID: 11824
			void SetOnMoveEnd([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E31 RID: 11825
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnMoveEnd();

			// Token: 0x06002E32 RID: 11826
			void SetOnMouseEnter([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E33 RID: 11827
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnMouseEnter();

			// Token: 0x06002E34 RID: 11828
			void SetOnMouseLeave([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E35 RID: 11829
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnMouseLeave();

			// Token: 0x06002E36 RID: 11830
			void SetOnActivate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E37 RID: 11831
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnActivate();

			// Token: 0x06002E38 RID: 11832
			void SetOnDeactivate([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E39 RID: 11833
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnDeactivate();

			// Token: 0x06002E3A RID: 11834
			[return: MarshalAs(UnmanagedType.Bool)]
			bool DragDrop();

			// Token: 0x06002E3B RID: 11835
			[return: MarshalAs(UnmanagedType.I4)]
			int GetGlyphMode();
		}

		// Token: 0x020004F5 RID: 1269
		[ComVisible(true)]
		[Guid("3050F1D8-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLBodyElement
		{
			// Token: 0x06002E3C RID: 11836
			void SetBackground([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E3D RID: 11837
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackground();

			// Token: 0x06002E3E RID: 11838
			void SetBgProperties([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E3F RID: 11839
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBgProperties();

			// Token: 0x06002E40 RID: 11840
			void SetLeftMargin([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E41 RID: 11841
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLeftMargin();

			// Token: 0x06002E42 RID: 11842
			void SetTopMargin([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E43 RID: 11843
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTopMargin();

			// Token: 0x06002E44 RID: 11844
			void SetRightMargin([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E45 RID: 11845
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetRightMargin();

			// Token: 0x06002E46 RID: 11846
			void SetBottomMargin([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E47 RID: 11847
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBottomMargin();

			// Token: 0x06002E48 RID: 11848
			void SetNoWrap([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002E49 RID: 11849
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetNoWrap();

			// Token: 0x06002E4A RID: 11850
			void SetBgColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E4B RID: 11851
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBgColor();

			// Token: 0x06002E4C RID: 11852
			void SetText([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E4D RID: 11853
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetText();

			// Token: 0x06002E4E RID: 11854
			void SetLink([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E4F RID: 11855
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLink();

			// Token: 0x06002E50 RID: 11856
			void SetVLink([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E51 RID: 11857
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVLink();

			// Token: 0x06002E52 RID: 11858
			void SetALink([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E53 RID: 11859
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetALink();

			// Token: 0x06002E54 RID: 11860
			void SetOnload([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E55 RID: 11861
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnload();

			// Token: 0x06002E56 RID: 11862
			void SetOnunload([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E57 RID: 11863
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnunload();

			// Token: 0x06002E58 RID: 11864
			void SetScroll([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E59 RID: 11865
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetScroll();

			// Token: 0x06002E5A RID: 11866
			void SetOnselect([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E5B RID: 11867
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnselect();

			// Token: 0x06002E5C RID: 11868
			void SetOnbeforeunload([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E5D RID: 11869
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetOnbeforeunload();

			// Token: 0x06002E5E RID: 11870
			[return: MarshalAs(UnmanagedType.Interface)]
			object CreateTextRange();
		}

		// Token: 0x020004F6 RID: 1270
		[ComVisible(true)]
		[Guid("3050F2E3-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLStyleSheet
		{
			// Token: 0x06002E5F RID: 11871
			void SetTitle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E60 RID: 11872
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTitle();

			// Token: 0x06002E61 RID: 11873
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLStyleSheet GetParentStyleSheet();

			// Token: 0x06002E62 RID: 11874
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement GetOwningElement();

			// Token: 0x06002E63 RID: 11875
			void SetDisabled([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002E64 RID: 11876
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetDisabled();

			// Token: 0x06002E65 RID: 11877
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetReadOnly();

			// Token: 0x06002E66 RID: 11878
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetImports();

			// Token: 0x06002E67 RID: 11879
			void SetHref([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E68 RID: 11880
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetHref();

			// Token: 0x06002E69 RID: 11881
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStyleSheetType();

			// Token: 0x06002E6A RID: 11882
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetId();

			// Token: 0x06002E6B RID: 11883
			[return: MarshalAs(UnmanagedType.I4)]
			int AddImport([MarshalAs(UnmanagedType.BStr)] [In] string bstrURL, [MarshalAs(UnmanagedType.I4)] [In] int lIndex);

			// Token: 0x06002E6C RID: 11884
			[return: MarshalAs(UnmanagedType.I4)]
			int AddRule([MarshalAs(UnmanagedType.BStr)] [In] string bstrSelector, [MarshalAs(UnmanagedType.BStr)] [In] string bstrStyle, [MarshalAs(UnmanagedType.I4)] [In] int lIndex);

			// Token: 0x06002E6D RID: 11885
			void RemoveImport([MarshalAs(UnmanagedType.I4)] [In] int lIndex);

			// Token: 0x06002E6E RID: 11886
			void RemoveRule([MarshalAs(UnmanagedType.I4)] [In] int lIndex);

			// Token: 0x06002E6F RID: 11887
			void SetMedia([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E70 RID: 11888
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetMedia();

			// Token: 0x06002E71 RID: 11889
			void SetCssText([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E72 RID: 11890
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCssText();

			// Token: 0x06002E73 RID: 11891
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetRules();
		}

		// Token: 0x020004F7 RID: 1271
		[ComVisible(true)]
		[Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLStyle
		{
			// Token: 0x06002E74 RID: 11892
			void SetFontFamily([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E75 RID: 11893
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontFamily();

			// Token: 0x06002E76 RID: 11894
			void SetFontStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E77 RID: 11895
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontStyle();

			// Token: 0x06002E78 RID: 11896
			void SetFontObject([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E79 RID: 11897
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontObject();

			// Token: 0x06002E7A RID: 11898
			void SetFontWeight([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E7B RID: 11899
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontWeight();

			// Token: 0x06002E7C RID: 11900
			void SetFontSize([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E7D RID: 11901
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFontSize();

			// Token: 0x06002E7E RID: 11902
			void SetFont([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E7F RID: 11903
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFont();

			// Token: 0x06002E80 RID: 11904
			void SetColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E81 RID: 11905
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetColor();

			// Token: 0x06002E82 RID: 11906
			void SetBackground([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E83 RID: 11907
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackground();

			// Token: 0x06002E84 RID: 11908
			void SetBackgroundColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E85 RID: 11909
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundColor();

			// Token: 0x06002E86 RID: 11910
			void SetBackgroundImage([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E87 RID: 11911
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundImage();

			// Token: 0x06002E88 RID: 11912
			void SetBackgroundRepeat([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E89 RID: 11913
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundRepeat();

			// Token: 0x06002E8A RID: 11914
			void SetBackgroundAttachment([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E8B RID: 11915
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundAttachment();

			// Token: 0x06002E8C RID: 11916
			void SetBackgroundPosition([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E8D RID: 11917
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundPosition();

			// Token: 0x06002E8E RID: 11918
			void SetBackgroundPositionX([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E8F RID: 11919
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionX();

			// Token: 0x06002E90 RID: 11920
			void SetBackgroundPositionY([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E91 RID: 11921
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionY();

			// Token: 0x06002E92 RID: 11922
			void SetWordSpacing([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E93 RID: 11923
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWordSpacing();

			// Token: 0x06002E94 RID: 11924
			void SetLetterSpacing([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002E95 RID: 11925
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLetterSpacing();

			// Token: 0x06002E96 RID: 11926
			void SetTextDecoration([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002E97 RID: 11927
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextDecoration();

			// Token: 0x06002E98 RID: 11928
			void SetTextDecorationNone([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002E99 RID: 11929
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationNone();

			// Token: 0x06002E9A RID: 11930
			void SetTextDecorationUnderline([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002E9B RID: 11931
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationUnderline();

			// Token: 0x06002E9C RID: 11932
			void SetTextDecorationOverline([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002E9D RID: 11933
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationOverline();

			// Token: 0x06002E9E RID: 11934
			void SetTextDecorationLineThrough([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002E9F RID: 11935
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationLineThrough();

			// Token: 0x06002EA0 RID: 11936
			void SetTextDecorationBlink([MarshalAs(UnmanagedType.Bool)] [In] bool p);

			// Token: 0x06002EA1 RID: 11937
			[return: MarshalAs(UnmanagedType.Bool)]
			bool GetTextDecorationBlink();

			// Token: 0x06002EA2 RID: 11938
			void SetVerticalAlign([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EA3 RID: 11939
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVerticalAlign();

			// Token: 0x06002EA4 RID: 11940
			void SetTextTransform([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EA5 RID: 11941
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextTransform();

			// Token: 0x06002EA6 RID: 11942
			void SetTextAlign([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EA7 RID: 11943
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextAlign();

			// Token: 0x06002EA8 RID: 11944
			void SetTextIndent([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EA9 RID: 11945
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTextIndent();

			// Token: 0x06002EAA RID: 11946
			void SetLineHeight([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EAB RID: 11947
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLineHeight();

			// Token: 0x06002EAC RID: 11948
			void SetMarginTop([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EAD RID: 11949
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginTop();

			// Token: 0x06002EAE RID: 11950
			void SetMarginRight([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EAF RID: 11951
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginRight();

			// Token: 0x06002EB0 RID: 11952
			void SetMarginBottom([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EB1 RID: 11953
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginBottom();

			// Token: 0x06002EB2 RID: 11954
			void SetMarginLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EB3 RID: 11955
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginLeft();

			// Token: 0x06002EB4 RID: 11956
			void SetMargin([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EB5 RID: 11957
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetMargin();

			// Token: 0x06002EB6 RID: 11958
			void SetPaddingTop([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EB7 RID: 11959
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingTop();

			// Token: 0x06002EB8 RID: 11960
			void SetPaddingRight([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EB9 RID: 11961
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingRight();

			// Token: 0x06002EBA RID: 11962
			void SetPaddingBottom([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EBB RID: 11963
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingBottom();

			// Token: 0x06002EBC RID: 11964
			void SetPaddingLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EBD RID: 11965
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingLeft();

			// Token: 0x06002EBE RID: 11966
			void SetPadding([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EBF RID: 11967
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPadding();

			// Token: 0x06002EC0 RID: 11968
			void SetBorder([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EC1 RID: 11969
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorder();

			// Token: 0x06002EC2 RID: 11970
			void SetBorderTop([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EC3 RID: 11971
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTop();

			// Token: 0x06002EC4 RID: 11972
			void SetBorderRight([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EC5 RID: 11973
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRight();

			// Token: 0x06002EC6 RID: 11974
			void SetBorderBottom([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EC7 RID: 11975
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottom();

			// Token: 0x06002EC8 RID: 11976
			void SetBorderLeft([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EC9 RID: 11977
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeft();

			// Token: 0x06002ECA RID: 11978
			void SetBorderColor([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002ECB RID: 11979
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderColor();

			// Token: 0x06002ECC RID: 11980
			void SetBorderTopColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002ECD RID: 11981
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopColor();

			// Token: 0x06002ECE RID: 11982
			void SetBorderRightColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002ECF RID: 11983
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightColor();

			// Token: 0x06002ED0 RID: 11984
			void SetBorderBottomColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002ED1 RID: 11985
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomColor();

			// Token: 0x06002ED2 RID: 11986
			void SetBorderLeftColor([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002ED3 RID: 11987
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftColor();

			// Token: 0x06002ED4 RID: 11988
			void SetBorderWidth([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002ED5 RID: 11989
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderWidth();

			// Token: 0x06002ED6 RID: 11990
			void SetBorderTopWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002ED7 RID: 11991
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopWidth();

			// Token: 0x06002ED8 RID: 11992
			void SetBorderRightWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002ED9 RID: 11993
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightWidth();

			// Token: 0x06002EDA RID: 11994
			void SetBorderBottomWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EDB RID: 11995
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomWidth();

			// Token: 0x06002EDC RID: 11996
			void SetBorderLeftWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EDD RID: 11997
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftWidth();

			// Token: 0x06002EDE RID: 11998
			void SetBorderStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EDF RID: 11999
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderStyle();

			// Token: 0x06002EE0 RID: 12000
			void SetBorderTopStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EE1 RID: 12001
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTopStyle();

			// Token: 0x06002EE2 RID: 12002
			void SetBorderRightStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EE3 RID: 12003
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRightStyle();

			// Token: 0x06002EE4 RID: 12004
			void SetBorderBottomStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EE5 RID: 12005
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottomStyle();

			// Token: 0x06002EE6 RID: 12006
			void SetBorderLeftStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EE7 RID: 12007
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeftStyle();

			// Token: 0x06002EE8 RID: 12008
			void SetWidth([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EE9 RID: 12009
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWidth();

			// Token: 0x06002EEA RID: 12010
			void SetHeight([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EEB RID: 12011
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetHeight();

			// Token: 0x06002EEC RID: 12012
			void SetStyleFloat([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EED RID: 12013
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStyleFloat();

			// Token: 0x06002EEE RID: 12014
			void SetClear([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EEF RID: 12015
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClear();

			// Token: 0x06002EF0 RID: 12016
			void SetDisplay([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EF1 RID: 12017
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDisplay();

			// Token: 0x06002EF2 RID: 12018
			void SetVisibility([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EF3 RID: 12019
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetVisibility();

			// Token: 0x06002EF4 RID: 12020
			void SetListStyleType([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EF5 RID: 12021
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleType();

			// Token: 0x06002EF6 RID: 12022
			void SetListStylePosition([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EF7 RID: 12023
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStylePosition();

			// Token: 0x06002EF8 RID: 12024
			void SetListStyleImage([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EF9 RID: 12025
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleImage();

			// Token: 0x06002EFA RID: 12026
			void SetListStyle([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EFB RID: 12027
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyle();

			// Token: 0x06002EFC RID: 12028
			void SetWhiteSpace([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002EFD RID: 12029
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetWhiteSpace();

			// Token: 0x06002EFE RID: 12030
			void SetTop([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002EFF RID: 12031
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTop();

			// Token: 0x06002F00 RID: 12032
			void SetLeft([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002F01 RID: 12033
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLeft();

			// Token: 0x06002F02 RID: 12034
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPosition();

			// Token: 0x06002F03 RID: 12035
			void SetZIndex([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002F04 RID: 12036
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetZIndex();

			// Token: 0x06002F05 RID: 12037
			void SetOverflow([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002F06 RID: 12038
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOverflow();

			// Token: 0x06002F07 RID: 12039
			void SetPageBreakBefore([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002F08 RID: 12040
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakBefore();

			// Token: 0x06002F09 RID: 12041
			void SetPageBreakAfter([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002F0A RID: 12042
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakAfter();

			// Token: 0x06002F0B RID: 12043
			void SetCssText([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002F0C RID: 12044
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCssText();

			// Token: 0x06002F0D RID: 12045
			void SetPixelTop([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F0E RID: 12046
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelTop();

			// Token: 0x06002F0F RID: 12047
			void SetPixelLeft([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F10 RID: 12048
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelLeft();

			// Token: 0x06002F11 RID: 12049
			void SetPixelWidth([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F12 RID: 12050
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelWidth();

			// Token: 0x06002F13 RID: 12051
			void SetPixelHeight([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F14 RID: 12052
			[return: MarshalAs(UnmanagedType.I4)]
			int GetPixelHeight();

			// Token: 0x06002F15 RID: 12053
			void SetPosTop([MarshalAs(UnmanagedType.R4)] [In] float p);

			// Token: 0x06002F16 RID: 12054
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosTop();

			// Token: 0x06002F17 RID: 12055
			void SetPosLeft([MarshalAs(UnmanagedType.R4)] [In] float p);

			// Token: 0x06002F18 RID: 12056
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosLeft();

			// Token: 0x06002F19 RID: 12057
			void SetPosWidth([MarshalAs(UnmanagedType.R4)] [In] float p);

			// Token: 0x06002F1A RID: 12058
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosWidth();

			// Token: 0x06002F1B RID: 12059
			void SetPosHeight([MarshalAs(UnmanagedType.R4)] [In] float p);

			// Token: 0x06002F1C RID: 12060
			[return: MarshalAs(UnmanagedType.R4)]
			float GetPosHeight();

			// Token: 0x06002F1D RID: 12061
			void SetCursor([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002F1E RID: 12062
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCursor();

			// Token: 0x06002F1F RID: 12063
			void SetClip([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002F20 RID: 12064
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClip();

			// Token: 0x06002F21 RID: 12065
			void SetFilter([MarshalAs(UnmanagedType.BStr)] [In] string p);

			// Token: 0x06002F22 RID: 12066
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFilter();

			// Token: 0x06002F23 RID: 12067
			void SetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.Struct)] [In] object AttributeValue, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);

			// Token: 0x06002F24 RID: 12068
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);

			// Token: 0x06002F25 RID: 12069
			[return: MarshalAs(UnmanagedType.Bool)]
			bool RemoveAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);
		}

		// Token: 0x020004F8 RID: 1272
		[ComVisible(true)]
		[Guid("3050F3DB-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLCurrentStyle
		{
			// Token: 0x06002F26 RID: 12070
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPosition();

			// Token: 0x06002F27 RID: 12071
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetStyleFloat();

			// Token: 0x06002F28 RID: 12072
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetColor();

			// Token: 0x06002F29 RID: 12073
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundColor();

			// Token: 0x06002F2A RID: 12074
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontFamily();

			// Token: 0x06002F2B RID: 12075
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontStyle();

			// Token: 0x06002F2C RID: 12076
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetFontObject();

			// Token: 0x06002F2D RID: 12077
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFontWeight();

			// Token: 0x06002F2E RID: 12078
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetFontSize();

			// Token: 0x06002F2F RID: 12079
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundImage();

			// Token: 0x06002F30 RID: 12080
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionX();

			// Token: 0x06002F31 RID: 12081
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBackgroundPositionY();

			// Token: 0x06002F32 RID: 12082
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundRepeat();

			// Token: 0x06002F33 RID: 12083
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftColor();

			// Token: 0x06002F34 RID: 12084
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopColor();

			// Token: 0x06002F35 RID: 12085
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightColor();

			// Token: 0x06002F36 RID: 12086
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomColor();

			// Token: 0x06002F37 RID: 12087
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderTopStyle();

			// Token: 0x06002F38 RID: 12088
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderRightStyle();

			// Token: 0x06002F39 RID: 12089
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderBottomStyle();

			// Token: 0x06002F3A RID: 12090
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderLeftStyle();

			// Token: 0x06002F3B RID: 12091
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderTopWidth();

			// Token: 0x06002F3C RID: 12092
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderRightWidth();

			// Token: 0x06002F3D RID: 12093
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderBottomWidth();

			// Token: 0x06002F3E RID: 12094
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBorderLeftWidth();

			// Token: 0x06002F3F RID: 12095
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLeft();

			// Token: 0x06002F40 RID: 12096
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTop();

			// Token: 0x06002F41 RID: 12097
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetWidth();

			// Token: 0x06002F42 RID: 12098
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetHeight();

			// Token: 0x06002F43 RID: 12099
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingLeft();

			// Token: 0x06002F44 RID: 12100
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingTop();

			// Token: 0x06002F45 RID: 12101
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingRight();

			// Token: 0x06002F46 RID: 12102
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetPaddingBottom();

			// Token: 0x06002F47 RID: 12103
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextAlign();

			// Token: 0x06002F48 RID: 12104
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTextDecoration();

			// Token: 0x06002F49 RID: 12105
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDisplay();

			// Token: 0x06002F4A RID: 12106
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetVisibility();

			// Token: 0x06002F4B RID: 12107
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetZIndex();

			// Token: 0x06002F4C RID: 12108
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLetterSpacing();

			// Token: 0x06002F4D RID: 12109
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetLineHeight();

			// Token: 0x06002F4E RID: 12110
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetTextIndent();

			// Token: 0x06002F4F RID: 12111
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetVerticalAlign();

			// Token: 0x06002F50 RID: 12112
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBackgroundAttachment();

			// Token: 0x06002F51 RID: 12113
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginTop();

			// Token: 0x06002F52 RID: 12114
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginRight();

			// Token: 0x06002F53 RID: 12115
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginBottom();

			// Token: 0x06002F54 RID: 12116
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetMarginLeft();

			// Token: 0x06002F55 RID: 12117
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetClear();

			// Token: 0x06002F56 RID: 12118
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleType();

			// Token: 0x06002F57 RID: 12119
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStylePosition();

			// Token: 0x06002F58 RID: 12120
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetListStyleImage();

			// Token: 0x06002F59 RID: 12121
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipTop();

			// Token: 0x06002F5A RID: 12122
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipRight();

			// Token: 0x06002F5B RID: 12123
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipBottom();

			// Token: 0x06002F5C RID: 12124
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetClipLeft();

			// Token: 0x06002F5D RID: 12125
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetOverflow();

			// Token: 0x06002F5E RID: 12126
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakBefore();

			// Token: 0x06002F5F RID: 12127
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetPageBreakAfter();

			// Token: 0x06002F60 RID: 12128
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetCursor();

			// Token: 0x06002F61 RID: 12129
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetTableLayout();

			// Token: 0x06002F62 RID: 12130
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBorderCollapse();

			// Token: 0x06002F63 RID: 12131
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetDirection();

			// Token: 0x06002F64 RID: 12132
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetBehavior();

			// Token: 0x06002F65 RID: 12133
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetAttribute([MarshalAs(UnmanagedType.BStr)] [In] string strAttributeName, [MarshalAs(UnmanagedType.I4)] [In] int lFlags);

			// Token: 0x06002F66 RID: 12134
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetUnicodeBidi();

			// Token: 0x06002F67 RID: 12135
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetRight();

			// Token: 0x06002F68 RID: 12136
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetBottom();
		}

		// Token: 0x020004F9 RID: 1273
		[ComVisible(true)]
		[Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLElementCollection
		{
			// Token: 0x06002F69 RID: 12137
			[return: MarshalAs(UnmanagedType.BStr)]
			string toString();

			// Token: 0x06002F6A RID: 12138
			void SetLength([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F6B RID: 12139
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLength();

			// Token: 0x06002F6C RID: 12140
			[return: MarshalAs(UnmanagedType.Interface)]
			object Get_newEnum();

			// Token: 0x06002F6D RID: 12141
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLElement Item([MarshalAs(UnmanagedType.Struct)] [In] object name, [MarshalAs(UnmanagedType.Struct)] [In] object index);

			// Token: 0x06002F6E RID: 12142
			[return: MarshalAs(UnmanagedType.Interface)]
			object Tags([MarshalAs(UnmanagedType.Struct)] [In] object tagName);
		}

		// Token: 0x020004FA RID: 1274
		[ComVisible(true)]
		[Guid("3050F4A3-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLRect
		{
			// Token: 0x06002F6F RID: 12143
			void SetLeft([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F70 RID: 12144
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLeft();

			// Token: 0x06002F71 RID: 12145
			void SetTop([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F72 RID: 12146
			[return: MarshalAs(UnmanagedType.I4)]
			int GetTop();

			// Token: 0x06002F73 RID: 12147
			void SetRight([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F74 RID: 12148
			[return: MarshalAs(UnmanagedType.I4)]
			int GetRight();

			// Token: 0x06002F75 RID: 12149
			void SetBottom([MarshalAs(UnmanagedType.I4)] [In] int p);

			// Token: 0x06002F76 RID: 12150
			[return: MarshalAs(UnmanagedType.I4)]
			int GetBottom();
		}

		// Token: 0x020004FB RID: 1275
		[ComVisible(true)]
		[Guid("3050F4A4-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLRectCollection
		{
			// Token: 0x06002F77 RID: 12151
			[return: MarshalAs(UnmanagedType.I4)]
			int GetLength();

			// Token: 0x06002F78 RID: 12152
			[return: MarshalAs(UnmanagedType.Interface)]
			object Get_newEnum();

			// Token: 0x06002F79 RID: 12153
			[return: MarshalAs(UnmanagedType.Struct)]
			object Item([In] ref object pvarIndex);
		}

		// Token: 0x020004FC RID: 1276
		[ComVisible(true)]
		[Guid("3050F5DA-98B5-11CF-BB82-00AA00BDCE0B")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[ComImport]
		public interface IHTMLDOMNode
		{
			// Token: 0x06002F7A RID: 12154
			[return: MarshalAs(UnmanagedType.I4)]
			int GetNodeType();

			// Token: 0x06002F7B RID: 12155
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode GetParentNode();

			// Token: 0x06002F7C RID: 12156
			[return: MarshalAs(UnmanagedType.Bool)]
			bool HasChildNodes();

			// Token: 0x06002F7D RID: 12157
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetChildNodes();

			// Token: 0x06002F7E RID: 12158
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetAttributes();

			// Token: 0x06002F7F RID: 12159
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode InsertBefore([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Struct)] [In] object refChild);

			// Token: 0x06002F80 RID: 12160
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode RemoveChild([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDOMNode oldChild);

			// Token: 0x06002F81 RID: 12161
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode ReplaceChild([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDOMNode newChild, [MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDOMNode oldChild);

			// Token: 0x06002F82 RID: 12162
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode CloneNode([MarshalAs(UnmanagedType.Bool)] [In] bool fDeep);

			// Token: 0x06002F83 RID: 12163
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode RemoveNode([MarshalAs(UnmanagedType.Bool)] [In] bool fDeep);

			// Token: 0x06002F84 RID: 12164
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode SwapNode([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDOMNode otherNode);

			// Token: 0x06002F85 RID: 12165
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode ReplaceNode([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDOMNode replacement);

			// Token: 0x06002F86 RID: 12166
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode AppendChild([MarshalAs(UnmanagedType.Interface)] [In] NativeMethods.IHTMLDOMNode newChild);

			// Token: 0x06002F87 RID: 12167
			[return: MarshalAs(UnmanagedType.BStr)]
			string GetNodeName();

			// Token: 0x06002F88 RID: 12168
			void SetNodeValue([MarshalAs(UnmanagedType.Struct)] [In] object p);

			// Token: 0x06002F89 RID: 12169
			[return: MarshalAs(UnmanagedType.Struct)]
			object GetNodeValue();

			// Token: 0x06002F8A RID: 12170
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode GetFirstChild();

			// Token: 0x06002F8B RID: 12171
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode GetLastChild();

			// Token: 0x06002F8C RID: 12172
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode GetPreviousSibling();

			// Token: 0x06002F8D RID: 12173
			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IHTMLDOMNode GetNextSibling();
		}

		// Token: 0x020004FD RID: 1277
		[StructLayout(LayoutKind.Sequential)]
		public class HDHITTESTINFO
		{
			// Token: 0x04001F8C RID: 8076
			public int pt_x;

			// Token: 0x04001F8D RID: 8077
			public int pt_y;

			// Token: 0x04001F8E RID: 8078
			public int flags;

			// Token: 0x04001F8F RID: 8079
			public int iItem;
		}

		// Token: 0x020004FE RID: 1278
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOLEVERB
		{
			// Token: 0x04001F90 RID: 8080
			[MarshalAs(UnmanagedType.I4)]
			public int lVerb;

			// Token: 0x04001F91 RID: 8081
			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpszVerbName;

			// Token: 0x04001F92 RID: 8082
			[MarshalAs(UnmanagedType.U4)]
			public int fuFlags;

			// Token: 0x04001F93 RID: 8083
			[MarshalAs(UnmanagedType.U4)]
			public int grfAttribs;
		}

		// Token: 0x020004FF RID: 1279
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
		public class TV_HITTESTINFO
		{
			// Token: 0x04001F94 RID: 8084
			public int pt_x;

			// Token: 0x04001F95 RID: 8085
			public int pt_y;

			// Token: 0x04001F96 RID: 8086
			public int flags;

			// Token: 0x04001F97 RID: 8087
			public int hItem;
		}

		// Token: 0x02000500 RID: 1280
		// (Invoke) Token: 0x06002F92 RID: 12178
		public delegate int ListViewCompareCallback(IntPtr lParam1, IntPtr lParam2, IntPtr lParamSort);

		// Token: 0x02000501 RID: 1281
		// (Invoke) Token: 0x06002F96 RID: 12182
		public delegate void TimerProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000502 RID: 1282
		// (Invoke) Token: 0x06002F9A RID: 12186
		public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000503 RID: 1283
		internal class Util
		{
			// Token: 0x06002F9D RID: 12189 RVA: 0x0010754E File Offset: 0x0010574E
			public static int MAKELONG(int low, int high)
			{
				return high << 16 | (low & 65535);
			}

			// Token: 0x06002F9E RID: 12190 RVA: 0x0010754E File Offset: 0x0010574E
			public static int MAKELPARAM(int low, int high)
			{
				return high << 16 | (low & 65535);
			}

			// Token: 0x06002F9F RID: 12191 RVA: 0x0010755C File Offset: 0x0010575C
			public static int HIWORD(int n)
			{
				return n >> 16 & 65535;
			}

			// Token: 0x06002FA0 RID: 12192 RVA: 0x00107568 File Offset: 0x00105768
			public static int LOWORD(int n)
			{
				return n & 65535;
			}

			// Token: 0x06002FA1 RID: 12193 RVA: 0x00107574 File Offset: 0x00105774
			public static int SignedHIWORD(int n)
			{
				int num = (int)((short)(n >> 16 & 65535));
				num <<= 16;
				return num >> 16;
			}

			// Token: 0x06002FA2 RID: 12194 RVA: 0x00107598 File Offset: 0x00105798
			public static int SignedLOWORD(int n)
			{
				int num = (int)((short)(n & 65535));
				num <<= 16;
				return num >> 16;
			}

			// Token: 0x06002FA3 RID: 12195 RVA: 0x001075B9 File Offset: 0x001057B9
			public static int SignedHIWORD(IntPtr n)
			{
				return NativeMethods.Util.SignedHIWORD((int)((long)n));
			}

			// Token: 0x06002FA4 RID: 12196 RVA: 0x001075C7 File Offset: 0x001057C7
			public static int SignedLOWORD(IntPtr n)
			{
				return NativeMethods.Util.SignedLOWORD((int)((long)n));
			}

			// Token: 0x06002FA5 RID: 12197
			[DllImport("user32.dll", CharSet = CharSet.Auto)]
			internal static extern int RegisterWindowMessage(string msg);
		}

		// Token: 0x02000504 RID: 1284
		public sealed class CommonHandles
		{
			// Token: 0x04001F98 RID: 8088
			public static readonly int Accelerator = System.Internal.HandleCollector.RegisterType("Accelerator", 80, 50);

			// Token: 0x04001F99 RID: 8089
			public static readonly int Cursor = System.Internal.HandleCollector.RegisterType("Cursor", 20, 500);

			// Token: 0x04001F9A RID: 8090
			public static readonly int EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);

			// Token: 0x04001F9B RID: 8091
			public static readonly int Find = System.Internal.HandleCollector.RegisterType("Find", 0, 1000);

			// Token: 0x04001F9C RID: 8092
			public static readonly int GDI = System.Internal.HandleCollector.RegisterType("GDI", 90, 50);

			// Token: 0x04001F9D RID: 8093
			public static readonly int HDC = System.Internal.HandleCollector.RegisterType("HDC", 100, 2);

			// Token: 0x04001F9E RID: 8094
			public static readonly int Icon = System.Internal.HandleCollector.RegisterType("Icon", 20, 500);

			// Token: 0x04001F9F RID: 8095
			public static readonly int Kernel = System.Internal.HandleCollector.RegisterType("Kernel", 0, 1000);

			// Token: 0x04001FA0 RID: 8096
			public static readonly int Menu = System.Internal.HandleCollector.RegisterType("Menu", 30, 1000);

			// Token: 0x04001FA1 RID: 8097
			public static readonly int Window = System.Internal.HandleCollector.RegisterType("Window", 5, 1000);
		}

		// Token: 0x02000505 RID: 1285
		internal class ActiveX
		{
			// Token: 0x04001FA2 RID: 8098
			public const int OCM__BASE = 8192;

			// Token: 0x04001FA3 RID: 8099
			public const int DISPID_VALUE = 0;

			// Token: 0x04001FA4 RID: 8100
			public const int DISPID_UNKNOWN = -1;

			// Token: 0x04001FA5 RID: 8101
			public const int DISPID_AUTOSIZE = -500;

			// Token: 0x04001FA6 RID: 8102
			public const int DISPID_BACKCOLOR = -501;

			// Token: 0x04001FA7 RID: 8103
			public const int DISPID_BACKSTYLE = -502;

			// Token: 0x04001FA8 RID: 8104
			public const int DISPID_BORDERCOLOR = -503;

			// Token: 0x04001FA9 RID: 8105
			public const int DISPID_BORDERSTYLE = -504;

			// Token: 0x04001FAA RID: 8106
			public const int DISPID_BORDERWIDTH = -505;

			// Token: 0x04001FAB RID: 8107
			public const int DISPID_DRAWMODE = -507;

			// Token: 0x04001FAC RID: 8108
			public const int DISPID_DRAWSTYLE = -508;

			// Token: 0x04001FAD RID: 8109
			public const int DISPID_DRAWWIDTH = -509;

			// Token: 0x04001FAE RID: 8110
			public const int DISPID_FILLCOLOR = -510;

			// Token: 0x04001FAF RID: 8111
			public const int DISPID_FILLSTYLE = -511;

			// Token: 0x04001FB0 RID: 8112
			public const int DISPID_FONT = -512;

			// Token: 0x04001FB1 RID: 8113
			public const int DISPID_FORECOLOR = -513;

			// Token: 0x04001FB2 RID: 8114
			public const int DISPID_ENABLED = -514;

			// Token: 0x04001FB3 RID: 8115
			public const int DISPID_HWND = -515;

			// Token: 0x04001FB4 RID: 8116
			public const int DISPID_TABSTOP = -516;

			// Token: 0x04001FB5 RID: 8117
			public const int DISPID_TEXT = -517;

			// Token: 0x04001FB6 RID: 8118
			public const int DISPID_CAPTION = -518;

			// Token: 0x04001FB7 RID: 8119
			public const int DISPID_BORDERVISIBLE = -519;

			// Token: 0x04001FB8 RID: 8120
			public const int DISPID_APPEARANCE = -520;

			// Token: 0x04001FB9 RID: 8121
			public const int DISPID_MOUSEPOINTER = -521;

			// Token: 0x04001FBA RID: 8122
			public const int DISPID_MOUSEICON = -522;

			// Token: 0x04001FBB RID: 8123
			public const int DISPID_PICTURE = -523;

			// Token: 0x04001FBC RID: 8124
			public const int DISPID_VALID = -524;

			// Token: 0x04001FBD RID: 8125
			public const int DISPID_READYSTATE = -525;

			// Token: 0x04001FBE RID: 8126
			public const int DISPID_REFRESH = -550;

			// Token: 0x04001FBF RID: 8127
			public const int DISPID_DOCLICK = -551;

			// Token: 0x04001FC0 RID: 8128
			public const int DISPID_ABOUTBOX = -552;

			// Token: 0x04001FC1 RID: 8129
			public const int DISPID_CLICK = -600;

			// Token: 0x04001FC2 RID: 8130
			public const int DISPID_DBLCLICK = -601;

			// Token: 0x04001FC3 RID: 8131
			public const int DISPID_KEYDOWN = -602;

			// Token: 0x04001FC4 RID: 8132
			public const int DISPID_KEYPRESS = -603;

			// Token: 0x04001FC5 RID: 8133
			public const int DISPID_KEYUP = -604;

			// Token: 0x04001FC6 RID: 8134
			public const int DISPID_MOUSEDOWN = -605;

			// Token: 0x04001FC7 RID: 8135
			public const int DISPID_MOUSEMOVE = -606;

			// Token: 0x04001FC8 RID: 8136
			public const int DISPID_MOUSEUP = -607;

			// Token: 0x04001FC9 RID: 8137
			public const int DISPID_ERROREVENT = -608;

			// Token: 0x04001FCA RID: 8138
			public const int DISPID_RIGHTTOLEFT = -611;

			// Token: 0x04001FCB RID: 8139
			public const int DISPID_READYSTATECHANGE = -609;

			// Token: 0x04001FCC RID: 8140
			public const int DISPID_AMBIENT_BACKCOLOR = -701;

			// Token: 0x04001FCD RID: 8141
			public const int DISPID_AMBIENT_DISPLAYNAME = -702;

			// Token: 0x04001FCE RID: 8142
			public const int DISPID_AMBIENT_FONT = -703;

			// Token: 0x04001FCF RID: 8143
			public const int DISPID_AMBIENT_FORECOLOR = -704;

			// Token: 0x04001FD0 RID: 8144
			public const int DISPID_AMBIENT_LOCALEID = -705;

			// Token: 0x04001FD1 RID: 8145
			public const int DISPID_AMBIENT_MESSAGEREFLECT = -706;

			// Token: 0x04001FD2 RID: 8146
			public const int DISPID_AMBIENT_SCALEUNITS = -707;

			// Token: 0x04001FD3 RID: 8147
			public const int DISPID_AMBIENT_TEXTALIGN = -708;

			// Token: 0x04001FD4 RID: 8148
			public const int DISPID_AMBIENT_USERMODE = -709;

			// Token: 0x04001FD5 RID: 8149
			public const int DISPID_AMBIENT_UIDEAD = -710;

			// Token: 0x04001FD6 RID: 8150
			public const int DISPID_AMBIENT_SHOWGRABHANDLES = -711;

			// Token: 0x04001FD7 RID: 8151
			public const int DISPID_AMBIENT_SHOWHATCHING = -712;

			// Token: 0x04001FD8 RID: 8152
			public const int DISPID_AMBIENT_DISPLAYASDEFAULT = -713;

			// Token: 0x04001FD9 RID: 8153
			public const int DISPID_AMBIENT_SUPPORTSMNEMONICS = -714;

			// Token: 0x04001FDA RID: 8154
			public const int DISPID_AMBIENT_AUTOCLIP = -715;

			// Token: 0x04001FDB RID: 8155
			public const int DISPID_AMBIENT_APPEARANCE = -716;

			// Token: 0x04001FDC RID: 8156
			public const int DISPID_AMBIENT_PALETTE = -726;

			// Token: 0x04001FDD RID: 8157
			public const int DISPID_AMBIENT_TRANSFERPRIORITY = -728;

			// Token: 0x04001FDE RID: 8158
			public const int DISPID_Name = -800;

			// Token: 0x04001FDF RID: 8159
			public const int DISPID_Delete = -801;

			// Token: 0x04001FE0 RID: 8160
			public const int DISPID_Object = -802;

			// Token: 0x04001FE1 RID: 8161
			public const int DISPID_Parent = -803;

			// Token: 0x04001FE2 RID: 8162
			public const int DVASPECT_CONTENT = 1;

			// Token: 0x04001FE3 RID: 8163
			public const int DVASPECT_THUMBNAIL = 2;

			// Token: 0x04001FE4 RID: 8164
			public const int DVASPECT_ICON = 4;

			// Token: 0x04001FE5 RID: 8165
			public const int DVASPECT_DOCPRINT = 8;

			// Token: 0x04001FE6 RID: 8166
			public const int OLEMISC_RECOMPOSEONRESIZE = 1;

			// Token: 0x04001FE7 RID: 8167
			public const int OLEMISC_ONLYICONIC = 2;

			// Token: 0x04001FE8 RID: 8168
			public const int OLEMISC_INSERTNOTREPLACE = 4;

			// Token: 0x04001FE9 RID: 8169
			public const int OLEMISC_STATIC = 8;

			// Token: 0x04001FEA RID: 8170
			public const int OLEMISC_CANTLINKINSIDE = 16;

			// Token: 0x04001FEB RID: 8171
			public const int OLEMISC_CANLINKBYOLE1 = 32;

			// Token: 0x04001FEC RID: 8172
			public const int OLEMISC_ISLINKOBJECT = 64;

			// Token: 0x04001FED RID: 8173
			public const int OLEMISC_INSIDEOUT = 128;

			// Token: 0x04001FEE RID: 8174
			public const int OLEMISC_ACTIVATEWHENVISIBLE = 256;

			// Token: 0x04001FEF RID: 8175
			public const int OLEMISC_RENDERINGISDEVICEINDEPENDENT = 512;

			// Token: 0x04001FF0 RID: 8176
			public const int OLEMISC_INVISIBLEATRUNTIME = 1024;

			// Token: 0x04001FF1 RID: 8177
			public const int OLEMISC_ALWAYSRUN = 2048;

			// Token: 0x04001FF2 RID: 8178
			public const int OLEMISC_ACTSLIKEBUTTON = 4096;

			// Token: 0x04001FF3 RID: 8179
			public const int OLEMISC_ACTSLIKELABEL = 8192;

			// Token: 0x04001FF4 RID: 8180
			public const int OLEMISC_NOUIACTIVATE = 16384;

			// Token: 0x04001FF5 RID: 8181
			public const int OLEMISC_ALIGNABLE = 32768;

			// Token: 0x04001FF6 RID: 8182
			public const int OLEMISC_SIMPLEFRAME = 65536;

			// Token: 0x04001FF7 RID: 8183
			public const int OLEMISC_SETCLIENTSITEFIRST = 131072;

			// Token: 0x04001FF8 RID: 8184
			public const int OLEMISC_IMEMODE = 262144;

			// Token: 0x04001FF9 RID: 8185
			public const int OLEMISC_IGNOREACTIVATEWHENVISIBLE = 524288;

			// Token: 0x04001FFA RID: 8186
			public const int OLEMISC_WANTSTOMENUMERGE = 1048576;

			// Token: 0x04001FFB RID: 8187
			public const int OLEMISC_SUPPORTSMULTILEVELUNDO = 2097152;

			// Token: 0x04001FFC RID: 8188
			public const int QACONTAINER_SHOWHATCHING = 1;

			// Token: 0x04001FFD RID: 8189
			public const int QACONTAINER_SHOWGRABHANDLES = 2;

			// Token: 0x04001FFE RID: 8190
			public const int QACONTAINER_USERMODE = 4;

			// Token: 0x04001FFF RID: 8191
			public const int QACONTAINER_DISPLAYASDEFAULT = 8;

			// Token: 0x04002000 RID: 8192
			public const int QACONTAINER_UIDEAD = 16;

			// Token: 0x04002001 RID: 8193
			public const int QACONTAINER_AUTOCLIP = 32;

			// Token: 0x04002002 RID: 8194
			public const int QACONTAINER_MESSAGEREFLECT = 64;

			// Token: 0x04002003 RID: 8195
			public const int QACONTAINER_SUPPORTSMNEMONICS = 128;

			// Token: 0x04002004 RID: 8196
			public const int XFORMCOORDS_POSITION = 1;

			// Token: 0x04002005 RID: 8197
			public const int XFORMCOORDS_SIZE = 2;

			// Token: 0x04002006 RID: 8198
			public const int XFORMCOORDS_HIMETRICTOCONTAINER = 4;

			// Token: 0x04002007 RID: 8199
			public const int XFORMCOORDS_CONTAINERTOHIMETRIC = 8;

			// Token: 0x04002008 RID: 8200
			public const int PROPCAT_Nil = -1;

			// Token: 0x04002009 RID: 8201
			public const int PROPCAT_Misc = -2;

			// Token: 0x0400200A RID: 8202
			public const int PROPCAT_Font = -3;

			// Token: 0x0400200B RID: 8203
			public const int PROPCAT_Position = -4;

			// Token: 0x0400200C RID: 8204
			public const int PROPCAT_Appearance = -5;

			// Token: 0x0400200D RID: 8205
			public const int PROPCAT_Behavior = -6;

			// Token: 0x0400200E RID: 8206
			public const int PROPCAT_Data = -7;

			// Token: 0x0400200F RID: 8207
			public const int PROPCAT_List = -8;

			// Token: 0x04002010 RID: 8208
			public const int PROPCAT_Text = -9;

			// Token: 0x04002011 RID: 8209
			public const int PROPCAT_Scale = -10;

			// Token: 0x04002012 RID: 8210
			public const int PROPCAT_DDE = -11;

			// Token: 0x04002013 RID: 8211
			public const int GC_WCH_SIBLING = 1;

			// Token: 0x04002014 RID: 8212
			public const int GC_WCH_CONTAINER = 2;

			// Token: 0x04002015 RID: 8213
			public const int GC_WCH_CONTAINED = 3;

			// Token: 0x04002016 RID: 8214
			public const int GC_WCH_ALL = 4;

			// Token: 0x04002017 RID: 8215
			public const int GC_WCH_FREVERSEDIR = 134217728;

			// Token: 0x04002018 RID: 8216
			public const int GC_WCH_FONLYNEXT = 268435456;

			// Token: 0x04002019 RID: 8217
			public const int GC_WCH_FONLYPREV = 536870912;

			// Token: 0x0400201A RID: 8218
			public const int GC_WCH_FSELECTED = 1073741824;

			// Token: 0x0400201B RID: 8219
			public const int OLECONTF_EMBEDDINGS = 1;

			// Token: 0x0400201C RID: 8220
			public const int OLECONTF_LINKS = 2;

			// Token: 0x0400201D RID: 8221
			public const int OLECONTF_OTHERS = 4;

			// Token: 0x0400201E RID: 8222
			public const int OLECONTF_ONLYUSER = 8;

			// Token: 0x0400201F RID: 8223
			public const int OLECONTF_ONLYIFRUNNING = 16;

			// Token: 0x04002020 RID: 8224
			public const int ALIGN_MIN = 0;

			// Token: 0x04002021 RID: 8225
			public const int ALIGN_NO_CHANGE = 0;

			// Token: 0x04002022 RID: 8226
			public const int ALIGN_TOP = 1;

			// Token: 0x04002023 RID: 8227
			public const int ALIGN_BOTTOM = 2;

			// Token: 0x04002024 RID: 8228
			public const int ALIGN_LEFT = 3;

			// Token: 0x04002025 RID: 8229
			public const int ALIGN_RIGHT = 4;

			// Token: 0x04002026 RID: 8230
			public const int ALIGN_MAX = 4;

			// Token: 0x04002027 RID: 8231
			public const int OLEVERBATTRIB_NEVERDIRTIES = 1;

			// Token: 0x04002028 RID: 8232
			public const int OLEVERBATTRIB_ONCONTAINERMENU = 2;

			// Token: 0x04002029 RID: 8233
			public static Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");
		}

		// Token: 0x02000506 RID: 1286
		[Guid("00000104-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumOLEVERB
		{
			// Token: 0x06002FAB RID: 12203
			[PreserveSig]
			int Next([MarshalAs(UnmanagedType.U4)] int celt, [In] [Out] NativeMethods.tagOLEVERB rgelt, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);

			// Token: 0x06002FAC RID: 12204
			[PreserveSig]
			int Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);

			// Token: 0x06002FAD RID: 12205
			void Reset();

			// Token: 0x06002FAE RID: 12206
			void Clone(out NativeMethods.IEnumOLEVERB ppenum);
		}

		// Token: 0x02000507 RID: 1287
		[Guid("00000105-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumSTATDATA
		{
			// Token: 0x06002FAF RID: 12207
			void Next([MarshalAs(UnmanagedType.U4)] [In] int celt, [Out] NativeMethods.STATDATA rgelt, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] pceltFetched);

			// Token: 0x06002FB0 RID: 12208
			void Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);

			// Token: 0x06002FB1 RID: 12209
			void Reset();

			// Token: 0x06002FB2 RID: 12210
			void Clone([MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.IEnumSTATDATA[] ppenum);
		}

		// Token: 0x02000508 RID: 1288
		[StructLayout(LayoutKind.Sequential)]
		public sealed class STATDATA
		{
			// Token: 0x0400202A RID: 8234
			[MarshalAs(UnmanagedType.U4)]
			public int advf;

			// Token: 0x0400202B RID: 8235
			[MarshalAs(UnmanagedType.U4)]
			public int dwConnection;
		}

		// Token: 0x02000509 RID: 1289
		[StructLayout(LayoutKind.Sequential)]
		public class CHARRANGE
		{
			// Token: 0x0400202C RID: 8236
			public int cpMin;

			// Token: 0x0400202D RID: 8237
			public int cpMax;
		}

		// Token: 0x0200050A RID: 1290
		[StructLayout(LayoutKind.Sequential)]
		public class STATSTG
		{
			// Token: 0x0400202E RID: 8238
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwcsName;

			// Token: 0x0400202F RID: 8239
			public int type;

			// Token: 0x04002030 RID: 8240
			[MarshalAs(UnmanagedType.I8)]
			public long cbSize;

			// Token: 0x04002031 RID: 8241
			[MarshalAs(UnmanagedType.I8)]
			public long mtime;

			// Token: 0x04002032 RID: 8242
			[MarshalAs(UnmanagedType.I8)]
			public long ctime;

			// Token: 0x04002033 RID: 8243
			[MarshalAs(UnmanagedType.I8)]
			public long atime;

			// Token: 0x04002034 RID: 8244
			[MarshalAs(UnmanagedType.I4)]
			public int grfMode;

			// Token: 0x04002035 RID: 8245
			[MarshalAs(UnmanagedType.I4)]
			public int grfLocksSupported;

			// Token: 0x04002036 RID: 8246
			public int clsid_data1;

			// Token: 0x04002037 RID: 8247
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data2;

			// Token: 0x04002038 RID: 8248
			[MarshalAs(UnmanagedType.I2)]
			public short clsid_data3;

			// Token: 0x04002039 RID: 8249
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b0;

			// Token: 0x0400203A RID: 8250
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b1;

			// Token: 0x0400203B RID: 8251
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b2;

			// Token: 0x0400203C RID: 8252
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b3;

			// Token: 0x0400203D RID: 8253
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b4;

			// Token: 0x0400203E RID: 8254
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b5;

			// Token: 0x0400203F RID: 8255
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b6;

			// Token: 0x04002040 RID: 8256
			[MarshalAs(UnmanagedType.U1)]
			public byte clsid_b7;

			// Token: 0x04002041 RID: 8257
			[MarshalAs(UnmanagedType.I4)]
			public int grfStateBits;

			// Token: 0x04002042 RID: 8258
			[MarshalAs(UnmanagedType.I4)]
			public int reserved;
		}

		// Token: 0x0200050B RID: 1291
		[StructLayout(LayoutKind.Sequential)]
		public class FILETIME
		{
			// Token: 0x04002043 RID: 8259
			public uint dwLowDateTime;

			// Token: 0x04002044 RID: 8260
			public uint dwHighDateTime;
		}

		// Token: 0x0200050C RID: 1292
		[Guid("00000103-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumFORMATETC
		{
			// Token: 0x06002FB7 RID: 12215
			[PreserveSig]
			int Next([MarshalAs(UnmanagedType.U4)] [In] int celt, [Out] NativeMethods.FORMATETC rgelt, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] int[] pceltFetched);

			// Token: 0x06002FB8 RID: 12216
			[PreserveSig]
			int Skip([MarshalAs(UnmanagedType.U4)] [In] int celt);

			// Token: 0x06002FB9 RID: 12217
			[PreserveSig]
			int Reset();

			// Token: 0x06002FBA RID: 12218
			[PreserveSig]
			int Clone([MarshalAs(UnmanagedType.LPArray)] [Out] NativeMethods.IEnumFORMATETC[] ppenum);
		}

		// Token: 0x0200050D RID: 1293
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LOGFONT
		{
			// Token: 0x06002FBB RID: 12219 RVA: 0x0000362F File Offset: 0x0000182F
			public LOGFONT()
			{
			}

			// Token: 0x06002FBC RID: 12220 RVA: 0x001076C8 File Offset: 0x001058C8
			public LOGFONT(NativeMethods.LOGFONT lf)
			{
				this.lfHeight = lf.lfHeight;
				this.lfWidth = lf.lfWidth;
				this.lfEscapement = lf.lfEscapement;
				this.lfOrientation = lf.lfOrientation;
				this.lfWeight = lf.lfWeight;
				this.lfItalic = lf.lfItalic;
				this.lfUnderline = lf.lfUnderline;
				this.lfStrikeOut = lf.lfStrikeOut;
				this.lfCharSet = lf.lfCharSet;
				this.lfOutPrecision = lf.lfOutPrecision;
				this.lfClipPrecision = lf.lfClipPrecision;
				this.lfQuality = lf.lfQuality;
				this.lfPitchAndFamily = lf.lfPitchAndFamily;
				this.lfFaceName = lf.lfFaceName;
			}

			// Token: 0x06002FBD RID: 12221 RVA: 0x00107784 File Offset: 0x00105984
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"lfHeight=",
					this.lfHeight.ToString(),
					", lfWidth=",
					this.lfWidth.ToString(),
					", lfEscapement=",
					this.lfEscapement.ToString(),
					", lfOrientation=",
					this.lfOrientation.ToString(),
					", lfWeight=",
					this.lfWeight.ToString(),
					", lfItalic=",
					this.lfItalic.ToString(),
					", lfUnderline=",
					this.lfUnderline.ToString(),
					", lfStrikeOut=",
					this.lfStrikeOut.ToString(),
					", lfCharSet=",
					this.lfCharSet.ToString(),
					", lfOutPrecision=",
					this.lfOutPrecision.ToString(),
					", lfClipPrecision=",
					this.lfClipPrecision.ToString(),
					", lfQuality=",
					this.lfQuality.ToString(),
					", lfPitchAndFamily=",
					this.lfPitchAndFamily.ToString(),
					", lfFaceName=",
					this.lfFaceName
				});
			}

			// Token: 0x04002045 RID: 8261
			public int lfHeight;

			// Token: 0x04002046 RID: 8262
			public int lfWidth;

			// Token: 0x04002047 RID: 8263
			public int lfEscapement;

			// Token: 0x04002048 RID: 8264
			public int lfOrientation;

			// Token: 0x04002049 RID: 8265
			public int lfWeight;

			// Token: 0x0400204A RID: 8266
			public byte lfItalic;

			// Token: 0x0400204B RID: 8267
			public byte lfUnderline;

			// Token: 0x0400204C RID: 8268
			public byte lfStrikeOut;

			// Token: 0x0400204D RID: 8269
			public byte lfCharSet;

			// Token: 0x0400204E RID: 8270
			public byte lfOutPrecision;

			// Token: 0x0400204F RID: 8271
			public byte lfClipPrecision;

			// Token: 0x04002050 RID: 8272
			public byte lfQuality;

			// Token: 0x04002051 RID: 8273
			public byte lfPitchAndFamily;

			// Token: 0x04002052 RID: 8274
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		// Token: 0x0200050E RID: 1294
		[StructLayout(LayoutKind.Sequential)]
		public class NONCLIENTMETRICS
		{
			// Token: 0x04002053 RID: 8275
			public int cbSize = Marshal.SizeOf(typeof(NativeMethods.NONCLIENTMETRICS));

			// Token: 0x04002054 RID: 8276
			public int iBorderWidth;

			// Token: 0x04002055 RID: 8277
			public int iScrollWidth;

			// Token: 0x04002056 RID: 8278
			public int iScrollHeight;

			// Token: 0x04002057 RID: 8279
			public int iCaptionWidth;

			// Token: 0x04002058 RID: 8280
			public int iCaptionHeight;

			// Token: 0x04002059 RID: 8281
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfCaptionFont;

			// Token: 0x0400205A RID: 8282
			public int iSmCaptionWidth;

			// Token: 0x0400205B RID: 8283
			public int iSmCaptionHeight;

			// Token: 0x0400205C RID: 8284
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfSmCaptionFont;

			// Token: 0x0400205D RID: 8285
			public int iMenuWidth;

			// Token: 0x0400205E RID: 8286
			public int iMenuHeight;

			// Token: 0x0400205F RID: 8287
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfMenuFont;

			// Token: 0x04002060 RID: 8288
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfStatusFont;

			// Token: 0x04002061 RID: 8289
			[MarshalAs(UnmanagedType.Struct)]
			public NativeMethods.LOGFONT lfMessageFont;
		}
	}
}
