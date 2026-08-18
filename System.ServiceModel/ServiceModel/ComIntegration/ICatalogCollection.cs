using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001DA RID: 474
	[Guid("6EB22872-8A19-11D0-81B6-00A0C9231C29")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[ComImport]
	internal interface ICatalogCollection
	{
		// Token: 0x06000F5C RID: 3932
		[DispId(-4)]
		void GetEnumerator(out IEnumerator pEnum);

		// Token: 0x06000F5D RID: 3933
		[DispId(1)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object Item([In] int lIndex);

		// Token: 0x06000F5E RID: 3934
		[DispId(1610743810)]
		int Count();

		// Token: 0x06000F5F RID: 3935
		[DispId(1610743811)]
		void Remove([In] int lIndex);

		// Token: 0x06000F60 RID: 3936
		[DispId(1610743812)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object Add();

		// Token: 0x06000F61 RID: 3937
		[DispId(2)]
		void Populate();

		// Token: 0x06000F62 RID: 3938
		[DispId(3)]
		int SaveChanges();

		// Token: 0x06000F63 RID: 3939
		[DispId(4)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetCollection([MarshalAs(UnmanagedType.BStr)] [In] string bstrCollName, [In] object varObjectKey);

		// Token: 0x06000F64 RID: 3940
		[DispId(6)]
		object Name();

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F65 RID: 3941
		bool IsAddEnabled { [DispId(7)] [return: MarshalAs(UnmanagedType.VariantBool)] get; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F66 RID: 3942
		bool IsRemoveEnabled { [DispId(8)] [return: MarshalAs(UnmanagedType.VariantBool)] get; }

		// Token: 0x06000F67 RID: 3943
		[DispId(9)]
		[return: MarshalAs(UnmanagedType.Interface)]
		object GetUtilInterface();

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F68 RID: 3944
		int DataStoreMajorVersion { [DispId(10)] get; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F69 RID: 3945
		int DataStoreMinorVersion { [DispId(11)] get; }

		// Token: 0x06000F6A RID: 3946
		void PopulateByKey([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] [In] object[] aKeys);

		// Token: 0x06000F6B RID: 3947
		[DispId(13)]
		void PopulateByQuery([MarshalAs(UnmanagedType.BStr)] [In] string bstrQueryString, [In] int lQueryType);
	}
}
