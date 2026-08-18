using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000548 RID: 1352
	internal class NegotiationInfoClass
	{
		// Token: 0x06002925 RID: 10533 RVA: 0x000ABAB8 File Offset: 0x000AAAB8
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
					text = (ComNetOS.IsWin9x ? Marshal.PtrToStringAnsi(intPtr) : Marshal.PtrToStringUni(intPtr));
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

		// Token: 0x04002829 RID: 10281
		internal const string NTLM = "NTLM";

		// Token: 0x0400282A RID: 10282
		internal const string Kerberos = "Kerberos";

		// Token: 0x0400282B RID: 10283
		internal const string WDigest = "WDigest";

		// Token: 0x0400282C RID: 10284
		internal const string Negotiate = "Negotiate";

		// Token: 0x0400282D RID: 10285
		internal string AuthenticationPackage;
	}
}
