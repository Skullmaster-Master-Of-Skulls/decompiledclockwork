using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000515 RID: 1301
	[Guid("F1C3BF76-C3E4-11d3-88E7-00902754C43A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface ITypeLibImporterNotifySink
	{
		// Token: 0x060032AB RID: 12971
		void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg);

		// Token: 0x060032AC RID: 12972
		Assembly ResolveRef([MarshalAs(UnmanagedType.Interface)] object typeLib);
	}
}
