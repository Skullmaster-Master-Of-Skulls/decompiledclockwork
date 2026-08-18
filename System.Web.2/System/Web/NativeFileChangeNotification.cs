using System;
using System.Runtime.InteropServices;

namespace System.Web
{
	// Token: 0x02000071 RID: 113
	// (Invoke) Token: 0x06000699 RID: 1689
	internal delegate void NativeFileChangeNotification(FileAction action, [MarshalAs(UnmanagedType.LPWStr)] [In] string fileName, long ticks);
}
