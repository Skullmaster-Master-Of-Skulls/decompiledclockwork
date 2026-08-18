using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000791 RID: 1937
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BB1AEEC0-E4EC-47BA-8724-D26AC4F16604")]
	[ComImport]
	internal interface IProcessResumeCallback
	{
		// Token: 0x06005C8C RID: 23692
		void Resume();
	}
}
