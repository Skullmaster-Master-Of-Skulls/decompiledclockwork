using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A6 RID: 166
	internal class SecuritySessionKeyClass
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x00013454 File Offset: 0x00011654
		internal SecuritySessionKeyClass(SafeHandle safeHandle, int sessionKeyLength)
		{
			byte[] destination = new byte[sessionKeyLength];
			Marshal.Copy(safeHandle.DangerousGetHandle(), destination, 0, sessionKeyLength);
			this.sessionKey = destination;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00013483 File Offset: 0x00011683
		internal byte[] SessionKey
		{
			get
			{
				return this.sessionKey;
			}
		}

		// Token: 0x040004AC RID: 1196
		private byte[] sessionKey;
	}
}
