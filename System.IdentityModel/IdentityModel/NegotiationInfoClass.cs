using System;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x020000A5 RID: 165
	internal class NegotiationInfoClass
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x000133CC File Offset: 0x000115CC
		internal NegotiationInfoClass(SafeHandle safeHandle, int negotiationState)
		{
			if (safeHandle.IsInvalid)
			{
				return;
			}
			IntPtr ptr = safeHandle.DangerousGetHandle();
			if (negotiationState == 0 || negotiationState == 1)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(ptr, SecurityPackageInfo.NameOffest);
				string text = null;
				if (intPtr != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(intPtr);
				}
				if (string.Compare(text, "Kerberos", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "Kerberos";
					return;
				}
				if (string.Compare(text, "NTLM", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "NTLM";
					return;
				}
				this.AuthenticationPackage = text;
			}
		}

		// Token: 0x040004A9 RID: 1193
		internal const string NTLM = "NTLM";

		// Token: 0x040004AA RID: 1194
		internal const string Kerberos = "Kerberos";

		// Token: 0x040004AB RID: 1195
		internal string AuthenticationPackage;
	}
}
