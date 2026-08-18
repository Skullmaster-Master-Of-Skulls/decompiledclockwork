using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x020007B8 RID: 1976
	[ComVisible(true)]
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0x00000000000000000400000000000000", Name = "System.Runtime.Remoting")]
	public sealed class InternalRM
	{
		// Token: 0x06004674 RID: 18036 RVA: 0x000F07A4 File Offset: 0x000EF7A4
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x000F07A6 File Offset: 0x000EF7A6
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("SOAP");
		}
	}
}
