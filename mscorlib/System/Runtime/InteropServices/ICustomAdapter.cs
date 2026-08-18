using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000522 RID: 1314
	[ComVisible(true)]
	public interface ICustomAdapter
	{
		// Token: 0x060032E5 RID: 13029
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetUnderlyingObject();
	}
}
