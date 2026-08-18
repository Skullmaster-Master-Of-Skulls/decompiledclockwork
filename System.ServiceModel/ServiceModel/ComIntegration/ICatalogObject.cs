using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001D9 RID: 473
	[Guid("6EB22871-8A19-11D0-81B6-00A0C9231C29")]
	[ComImport]
	internal interface ICatalogObject
	{
		// Token: 0x06000F55 RID: 3925
		[DispId(1)]
		object GetValue([MarshalAs(UnmanagedType.BStr)] [In] string propName);

		// Token: 0x06000F56 RID: 3926
		[DispId(1)]
		void SetValue([MarshalAs(UnmanagedType.BStr)] [In] string propName, [In] object value);

		// Token: 0x06000F57 RID: 3927
		[DispId(2)]
		object Key();

		// Token: 0x06000F58 RID: 3928
		[DispId(3)]
		object Name();

		// Token: 0x06000F59 RID: 3929
		[DispId(4)]
		[return: MarshalAs(UnmanagedType.VariantBool)]
		bool IsPropertyReadOnly([MarshalAs(UnmanagedType.BStr)] [In] string bstrPropName);

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F5A RID: 3930
		bool Valid { [DispId(5)] [return: MarshalAs(UnmanagedType.VariantBool)] get; }

		// Token: 0x06000F5B RID: 3931
		[DispId(6)]
		[return: MarshalAs(UnmanagedType.VariantBool)]
		bool IsPropertyWriteOnly([MarshalAs(UnmanagedType.BStr)] [In] string bstrPropName);
	}
}
