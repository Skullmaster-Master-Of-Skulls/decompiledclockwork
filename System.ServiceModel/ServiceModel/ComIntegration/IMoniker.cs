using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000226 RID: 550
	[Guid("0000000f-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IMoniker
	{
		// Token: 0x06001095 RID: 4245
		void GetClassID(out Guid pClassID);

		// Token: 0x06001096 RID: 4246
		[PreserveSig]
		int IsDirty();

		// Token: 0x06001097 RID: 4247
		void Load(IStream pStm);

		// Token: 0x06001098 RID: 4248
		void Save(IStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

		// Token: 0x06001099 RID: 4249
		void GetSizeMax(out long pcbSize);

		// Token: 0x0600109A RID: 4250
		void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riidResult, IntPtr ppvResult);

		// Token: 0x0600109B RID: 4251
		void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

		// Token: 0x0600109C RID: 4252
		void Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

		// Token: 0x0600109D RID: 4253
		void ComposeWith(IMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

		// Token: 0x0600109E RID: 4254
		void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out IEnumMoniker ppenumMoniker);

		// Token: 0x0600109F RID: 4255
		[PreserveSig]
		int IsEqual(IMoniker pmkOtherMoniker);

		// Token: 0x060010A0 RID: 4256
		void Hash(IntPtr pdwHash);

		// Token: 0x060010A1 RID: 4257
		[PreserveSig]
		int IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

		// Token: 0x060010A2 RID: 4258
		void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out System.Runtime.InteropServices.ComTypes.FILETIME pFileTime);

		// Token: 0x060010A3 RID: 4259
		void Inverse(out IMoniker ppmk);

		// Token: 0x060010A4 RID: 4260
		void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

		// Token: 0x060010A5 RID: 4261
		void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

		// Token: 0x060010A6 RID: 4262
		void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

		// Token: 0x060010A7 RID: 4263
		void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

		// Token: 0x060010A8 RID: 4264
		[PreserveSig]
		int IsSystemMoniker(IntPtr pdwMksys);
	}
}
