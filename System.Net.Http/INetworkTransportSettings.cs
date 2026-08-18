using System;
using System.Runtime.InteropServices;

namespace System.Net.Http
{
	// Token: 0x02000020 RID: 32
	[ComVisible(true)]
	[Guid("5e7abb2c-f2c1-4a61-bd35-deb7a08ab0f1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface INetworkTransportSettings
	{
		// Token: 0x06000191 RID: 401
		void ApplySetting([In] ref TRANSPORT_SETTING_ID settingId, [In] int lengthIn, [In] IntPtr valueIn, out int lengthOut, out IntPtr valueOut);

		// Token: 0x06000192 RID: 402
		void QuerySetting([In] ref TRANSPORT_SETTING_ID settingId, [In] int lengthIn, [In] IntPtr valueIn, out int lengthOut, out IntPtr valueOut);
	}
}
