using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C9A RID: 3226
	[InterfaceType(1)]
	[ComConversionLoss]
	[Guid("08C0E040-62D1-11D1-9326-0060B067B86E")]
	[ComImport]
	public interface IActiveIMMApp
	{
		// Token: 0x0601620B RID: 90635
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AssociateContext([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint hIME, out uint phPrev);

		// Token: 0x0601620C RID: 90636
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConfigureIMEA([In] IntPtr hKL, [ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint dwMode, [In] ref __MIDL___MIDL_itf_mshtml_0250_0001 pData);

		// Token: 0x0601620D RID: 90637
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ConfigureIMEW([In] IntPtr hKL, [ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint dwMode, [In] ref __MIDL___MIDL_itf_mshtml_0250_0002 pData);

		// Token: 0x0601620E RID: 90638
		[MethodImpl(MethodImplOptions.InternalCall)]
		void CreateContext(out uint phIMC);

		// Token: 0x0601620F RID: 90639
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DestroyContext([In] uint hIME);

		// Token: 0x06016210 RID: 90640
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnumRegisterWordA([In] IntPtr hKL, [MarshalAs(UnmanagedType.LPStr)] [In] string szReading, [In] uint dwStyle, [MarshalAs(UnmanagedType.LPStr)] [In] string szRegister, [In] IntPtr pData, [MarshalAs(UnmanagedType.Interface)] out IEnumRegisterWordA pEnum);

		// Token: 0x06016211 RID: 90641
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnumRegisterWordW([In] IntPtr hKL, [MarshalAs(UnmanagedType.LPWStr)] [In] string szReading, [In] uint dwStyle, [MarshalAs(UnmanagedType.LPWStr)] [In] string szRegister, [In] IntPtr pData, [MarshalAs(UnmanagedType.Interface)] out IEnumRegisterWordW pEnum);

		// Token: 0x06016212 RID: 90642
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EscapeA([In] IntPtr hKL, [In] uint hIMC, [In] uint uEscape, [In] [Out] IntPtr pData, [ComAliasName("mshtml.LONG_PTR")] out int plResult);

		// Token: 0x06016213 RID: 90643
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EscapeW([In] IntPtr hKL, [In] uint hIMC, [In] uint uEscape, [In] [Out] IntPtr pData, [ComAliasName("mshtml.LONG_PTR")] out int plResult);

		// Token: 0x06016214 RID: 90644
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCandidateListA([In] uint hIMC, [In] uint dwIndex, [In] uint uBufLen, out __MIDL___MIDL_itf_mshtml_0250_0007 pCandList, out uint puCopied);

		// Token: 0x06016215 RID: 90645
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCandidateListW([In] uint hIMC, [In] uint dwIndex, [In] uint uBufLen, out __MIDL___MIDL_itf_mshtml_0250_0007 pCandList, out uint puCopied);

		// Token: 0x06016216 RID: 90646
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCandidateListCountA([In] uint hIMC, out uint pdwListSize, out uint pdwBufLen);

		// Token: 0x06016217 RID: 90647
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCandidateListCountW([In] uint hIMC, out uint pdwListSize, out uint pdwBufLen);

		// Token: 0x06016218 RID: 90648
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCandidateWindow([In] uint hIMC, [In] uint dwIndex, out __MIDL___MIDL_itf_mshtml_0250_0005 pCandidate);

		// Token: 0x06016219 RID: 90649
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCompositionFontA([In] uint hIMC, out __MIDL___MIDL_itf_mshtml_0250_0003 plf);

		// Token: 0x0601621A RID: 90650
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCompositionFontW([In] uint hIMC, out __MIDL___MIDL_itf_mshtml_0250_0004 plf);

		// Token: 0x0601621B RID: 90651
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCompositionStringA([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, out int plCopied, [Out] IntPtr pBuf);

		// Token: 0x0601621C RID: 90652
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCompositionStringW([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, out int plCopied, [Out] IntPtr pBuf);

		// Token: 0x0601621D RID: 90653
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCompositionWindow([In] uint hIMC, out __MIDL___MIDL_itf_mshtml_0250_0006 pCompForm);

		// Token: 0x0601621E RID: 90654
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetContext([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, out uint phIMC);

		// Token: 0x0601621F RID: 90655
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetConversionListA([In] IntPtr hKL, [In] uint hIMC, [MarshalAs(UnmanagedType.LPStr)] [In] string pSrc, [In] uint uBufLen, [In] uint uFlag, out __MIDL___MIDL_itf_mshtml_0250_0007 pDst, out uint puCopied);

		// Token: 0x06016220 RID: 90656
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetConversionListW([In] IntPtr hKL, [In] uint hIMC, [MarshalAs(UnmanagedType.LPWStr)] [In] string pSrc, [In] uint uBufLen, [In] uint uFlag, out __MIDL___MIDL_itf_mshtml_0250_0007 pDst, out uint puCopied);

		// Token: 0x06016221 RID: 90657
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetConversionStatus([In] uint hIMC, out uint pfdwConversion, out uint pfdwSentence);

		// Token: 0x06016222 RID: 90658
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDefaultIMEWnd([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [ComAliasName("mshtml.wireHWND")] [Out] IntPtr phDefWnd);

		// Token: 0x06016223 RID: 90659
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDescriptionA([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(UnmanagedType.LPStr)] [Out] string szDescription, out uint puCopied);

		// Token: 0x06016224 RID: 90660
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetDescriptionW([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(UnmanagedType.LPWStr)] [Out] string szDescription, out uint puCopied);

		// Token: 0x06016225 RID: 90661
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetGuideLineA([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, [MarshalAs(UnmanagedType.LPStr)] [Out] string pBuf, out uint pdwResult);

		// Token: 0x06016226 RID: 90662
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetGuideLineW([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, [MarshalAs(UnmanagedType.LPWStr)] [Out] string pBuf, out uint pdwResult);

		// Token: 0x06016227 RID: 90663
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetIMEFileNameA([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(UnmanagedType.LPStr)] [Out] string szFileName, out uint puCopied);

		// Token: 0x06016228 RID: 90664
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetIMEFileNameW([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(UnmanagedType.LPWStr)] [Out] string szFileName, out uint puCopied);

		// Token: 0x06016229 RID: 90665
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetOpenStatus([In] uint hIMC);

		// Token: 0x0601622A RID: 90666
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetProperty([In] IntPtr hKL, [In] uint fdwIndex, out uint pdwProperty);

		// Token: 0x0601622B RID: 90667
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRegisterWordStyleA([In] IntPtr hKL, [In] uint nItem, out __MIDL___MIDL_itf_mshtml_0250_0008 pStyleBuf, out uint puCopied);

		// Token: 0x0601622C RID: 90668
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetRegisterWordStyleW([In] IntPtr hKL, [In] uint nItem, out __MIDL___MIDL_itf_mshtml_0250_0009 pStyleBuf, out uint puCopied);

		// Token: 0x0601622D RID: 90669
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetStatusWindowPos([In] uint hIMC, out tagPOINT pptPos);

		// Token: 0x0601622E RID: 90670
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetVirtualKey([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, out uint puVirtualKey);

		// Token: 0x0601622F RID: 90671
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InstallIMEA([MarshalAs(UnmanagedType.LPStr)] [In] string szIMEFileName, [MarshalAs(UnmanagedType.LPStr)] [In] string szLayoutText, out IntPtr phKL);

		// Token: 0x06016230 RID: 90672
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InstallIMEW([MarshalAs(UnmanagedType.LPWStr)] [In] string szIMEFileName, [MarshalAs(UnmanagedType.LPWStr)] [In] string szLayoutText, out IntPtr phKL);

		// Token: 0x06016231 RID: 90673
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsIME([In] IntPtr hKL);

		// Token: 0x06016232 RID: 90674
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsUIMessageA([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWndIME, [In] uint msg, [ComAliasName("mshtml.UINT_PTR")] [In] uint wParam, [ComAliasName("mshtml.LONG_PTR")] [In] int lParam);

		// Token: 0x06016233 RID: 90675
		[MethodImpl(MethodImplOptions.InternalCall)]
		void IsUIMessageW([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWndIME, [In] uint msg, [ComAliasName("mshtml.UINT_PTR")] [In] uint wParam, [ComAliasName("mshtml.LONG_PTR")] [In] int lParam);

		// Token: 0x06016234 RID: 90676
		[MethodImpl(MethodImplOptions.InternalCall)]
		void NotifyIME([In] uint hIMC, [In] uint dwAction, [In] uint dwIndex, [In] uint dwValue);

		// Token: 0x06016235 RID: 90677
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterWordA([In] IntPtr hKL, [MarshalAs(UnmanagedType.LPStr)] [In] string szReading, [In] uint dwStyle, [MarshalAs(UnmanagedType.LPStr)] [In] string szRegister);

		// Token: 0x06016236 RID: 90678
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RegisterWordW([In] IntPtr hKL, [MarshalAs(UnmanagedType.LPWStr)] [In] string szReading, [In] uint dwStyle, [MarshalAs(UnmanagedType.LPWStr)] [In] string szRegister);

		// Token: 0x06016237 RID: 90679
		[MethodImpl(MethodImplOptions.InternalCall)]
		void ReleaseContext([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint hIMC);

		// Token: 0x06016238 RID: 90680
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCandidateWindow([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0005 pCandidate);

		// Token: 0x06016239 RID: 90681
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCompositionFontA([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0003 plf);

		// Token: 0x0601623A RID: 90682
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCompositionFontW([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0004 plf);

		// Token: 0x0601623B RID: 90683
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCompositionStringA([In] uint hIMC, [In] uint dwIndex, [In] IntPtr pComp, [In] uint dwCompLen, [In] IntPtr pRead, [In] uint dwReadLen);

		// Token: 0x0601623C RID: 90684
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCompositionStringW([In] uint hIMC, [In] uint dwIndex, [In] IntPtr pComp, [In] uint dwCompLen, [In] IntPtr pRead, [In] uint dwReadLen);

		// Token: 0x0601623D RID: 90685
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetCompositionWindow([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0006 pCompForm);

		// Token: 0x0601623E RID: 90686
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetConversionStatus([In] uint hIMC, [In] uint fdwConversion, [In] uint fdwSentence);

		// Token: 0x0601623F RID: 90687
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetOpenStatus([In] uint hIMC, [In] int fOpen);

		// Token: 0x06016240 RID: 90688
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetStatusWindowPos([In] uint hIMC, [In] ref tagPOINT pptPos);

		// Token: 0x06016241 RID: 90689
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SimulateHotKey([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint dwHotKeyID);

		// Token: 0x06016242 RID: 90690
		[MethodImpl(MethodImplOptions.InternalCall)]
		void UnregisterWordA([In] IntPtr hKL, [MarshalAs(UnmanagedType.LPStr)] [In] string szReading, [In] uint dwStyle, [MarshalAs(UnmanagedType.LPStr)] [In] string szUnregister);

		// Token: 0x06016243 RID: 90691
		[MethodImpl(MethodImplOptions.InternalCall)]
		void UnregisterWordW([In] IntPtr hKL, [MarshalAs(UnmanagedType.LPWStr)] [In] string szReading, [In] uint dwStyle, [MarshalAs(UnmanagedType.LPWStr)] [In] string szUnregister);

		// Token: 0x06016244 RID: 90692
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Activate([In] int fRestoreLayout);

		// Token: 0x06016245 RID: 90693
		[MethodImpl(MethodImplOptions.InternalCall)]
		void Deactivate();

		// Token: 0x06016246 RID: 90694
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnDefWindowProc([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint msg, [ComAliasName("mshtml.UINT_PTR")] [In] uint wParam, [ComAliasName("mshtml.LONG_PTR")] [In] int lParam, [ComAliasName("mshtml.LONG_PTR")] out int plResult);

		// Token: 0x06016247 RID: 90695
		[MethodImpl(MethodImplOptions.InternalCall)]
		void FilterClientWindows([In] ref ushort aaClassList, [In] uint uSize);

		// Token: 0x06016248 RID: 90696
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetCodePageA([In] IntPtr hKL, out uint uCodePage);

		// Token: 0x06016249 RID: 90697
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetLangId([In] IntPtr hKL, out ushort plid);

		// Token: 0x0601624A RID: 90698
		[MethodImpl(MethodImplOptions.InternalCall)]
		void AssociateContextEx([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint hIMC, [In] uint dwFlags);

		// Token: 0x0601624B RID: 90699
		[MethodImpl(MethodImplOptions.InternalCall)]
		void DisableIME([In] uint idThread);

		// Token: 0x0601624C RID: 90700
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetImeMenuItemsA([In] uint hIMC, [In] uint dwFlags, [In] uint dwType, [In] ref __MIDL___MIDL_itf_mshtml_0250_0010 pImeParentMenu, out __MIDL___MIDL_itf_mshtml_0250_0010 pImeMenu, [In] uint dwSize, out uint pdwResult);

		// Token: 0x0601624D RID: 90701
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetImeMenuItemsW([In] uint hIMC, [In] uint dwFlags, [In] uint dwType, [In] ref __MIDL___MIDL_itf_mshtml_0250_0011 pImeParentMenu, out __MIDL___MIDL_itf_mshtml_0250_0011 pImeMenu, [In] uint dwSize, out uint pdwResult);

		// Token: 0x0601624E RID: 90702
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EnumInputContext([In] uint idThread, [MarshalAs(UnmanagedType.Interface)] out IEnumInputContext ppEnum);
	}
}
