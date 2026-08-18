using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020003E2 RID: 994
	[Guid("0000010E-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDataObject
	{
		// Token: 0x06002611 RID: 9745
		void GetData([In] ref FORMATETC format, out STGMEDIUM medium);

		// Token: 0x06002612 RID: 9746
		void GetDataHere([In] ref FORMATETC format, ref STGMEDIUM medium);

		// Token: 0x06002613 RID: 9747
		[PreserveSig]
		int QueryGetData([In] ref FORMATETC format);

		// Token: 0x06002614 RID: 9748
		[PreserveSig]
		int GetCanonicalFormatEtc([In] ref FORMATETC formatIn, out FORMATETC formatOut);

		// Token: 0x06002615 RID: 9749
		void SetData([In] ref FORMATETC formatIn, [In] ref STGMEDIUM medium, [MarshalAs(UnmanagedType.Bool)] bool release);

		// Token: 0x06002616 RID: 9750
		IEnumFORMATETC EnumFormatEtc(DATADIR direction);

		// Token: 0x06002617 RID: 9751
		[PreserveSig]
		int DAdvise([In] ref FORMATETC pFormatetc, ADVF advf, IAdviseSink adviseSink, out int connection);

		// Token: 0x06002618 RID: 9752
		void DUnadvise(int connection);

		// Token: 0x06002619 RID: 9753
		[PreserveSig]
		int EnumDAdvise(out IEnumSTATDATA enumAdvise);
	}
}
