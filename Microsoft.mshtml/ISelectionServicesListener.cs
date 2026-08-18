using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC9 RID: 3273
	[Guid("3050F699-98B5-11CF-BB82-00AA00BDCE0B")]
	[InterfaceType(1)]
	[ComImport]
	public interface ISelectionServicesListener
	{
		// Token: 0x06016308 RID: 90888
		[MethodImpl(MethodImplOptions.InternalCall)]
		void BeginSelectionUndo();

		// Token: 0x06016309 RID: 90889
		[MethodImpl(MethodImplOptions.InternalCall)]
		void EndSelectionUndo();

		// Token: 0x0601630A RID: 90890
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnSelectedElementExit([MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIElementStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIElementEnd, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIElementContentStart, [MarshalAs(UnmanagedType.Interface)] [In] IMarkupPointer pIElementContentEnd);

		// Token: 0x0601630B RID: 90891
		[MethodImpl(MethodImplOptions.InternalCall)]
		void OnChangeType([In] _SELECTION_TYPE eType, [MarshalAs(UnmanagedType.Interface)] [In] ISelectionServicesListener pIListener);

		// Token: 0x0601630C RID: 90892
		[MethodImpl(MethodImplOptions.InternalCall)]
		void GetTypeDetail([MarshalAs(UnmanagedType.BStr)] out string pTypeDetail);
	}
}
