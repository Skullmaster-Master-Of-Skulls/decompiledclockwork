using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005ED RID: 1517
	internal class IpHelperErrors
	{
		// Token: 0x06002FD4 RID: 12244 RVA: 0x000CF336 File Offset: 0x000CE336
		internal static void CheckFamilyUnspecified(AddressFamily family)
		{
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6 && family != AddressFamily.Unspecified)
			{
				throw new ArgumentException(SR.GetString("net_invalidversion"), "family");
			}
		}

		// Token: 0x04002CC8 RID: 11464
		internal const uint Success = 0U;

		// Token: 0x04002CC9 RID: 11465
		internal const uint ErrorInvalidFunction = 1U;

		// Token: 0x04002CCA RID: 11466
		internal const uint ErrorNoSuchDevice = 2U;

		// Token: 0x04002CCB RID: 11467
		internal const uint ErrorInvalidData = 13U;

		// Token: 0x04002CCC RID: 11468
		internal const uint ErrorInvalidParameter = 87U;

		// Token: 0x04002CCD RID: 11469
		internal const uint ErrorBufferOverflow = 111U;

		// Token: 0x04002CCE RID: 11470
		internal const uint ErrorInsufficientBuffer = 122U;

		// Token: 0x04002CCF RID: 11471
		internal const uint ErrorNoData = 232U;

		// Token: 0x04002CD0 RID: 11472
		internal const uint Pending = 997U;

		// Token: 0x04002CD1 RID: 11473
		internal const uint ErrorNotFound = 1168U;
	}
}
