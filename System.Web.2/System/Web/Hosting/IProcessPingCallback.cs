using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000799 RID: 1945
	[Guid("f11dc4c9-ddd1-4566-ad53-cf6f3a28fefe")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IProcessPingCallback
	{
		// Token: 0x06005CA3 RID: 23715
		void Respond();
	}
}
