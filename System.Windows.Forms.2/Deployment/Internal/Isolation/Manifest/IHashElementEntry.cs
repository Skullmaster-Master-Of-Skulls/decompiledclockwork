using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200008F RID: 143
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9D46FB70-7B54-4f4f-9331-BA9E87833FF5")]
	[ComImport]
	internal interface IHashElementEntry
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000246 RID: 582
		HashElementEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000247 RID: 583
		uint index { [SecurityCritical] get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000248 RID: 584
		byte Transform { [SecurityCritical] get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000249 RID: 585
		object TransformMetadata { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600024A RID: 586
		byte DigestMethod { [SecurityCritical] get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600024B RID: 587
		object DigestValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600024C RID: 588
		string Xml { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
