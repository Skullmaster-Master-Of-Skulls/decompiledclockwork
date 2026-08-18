using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000215 RID: 533
	internal class NegotiationInfoClass
	{
		// Token: 0x060013C7 RID: 5063 RVA: 0x00068770 File Offset: 0x00066970
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
				if (string.Compare(text, "WDigest", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "WDigest";
					return;
				}
				this.AuthenticationPackage = text;
			}
		}

		// Token: 0x040015BE RID: 5566
		internal const string NTLM = "NTLM";

		// Token: 0x040015BF RID: 5567
		internal const string Kerberos = "Kerberos";

		// Token: 0x040015C0 RID: 5568
		internal const string WDigest = "WDigest";

		// Token: 0x040015C1 RID: 5569
		internal const string Negotiate = "Negotiate";

		// Token: 0x040015C2 RID: 5570
		internal string AuthenticationPackage;
	}
}
