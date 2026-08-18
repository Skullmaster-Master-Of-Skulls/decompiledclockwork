using System;
using System.Security.Cryptography;
using System.Text;

namespace System.IdentityModel
{
	// Token: 0x02000068 RID: 104
	public sealed class ProtectedDataCookieTransform : CookieTransform
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000C678 File Offset: 0x0000A878
		public ProtectedDataCookieTransform()
		{
			this.entropy = Encoding.UTF8.GetBytes("System.IdentityModel.ProtectedDataCookieTransform");
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C698 File Offset: 0x0000A898
		public override byte[] Decode(byte[] encoded)
		{
			if (encoded == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoded");
			}
			if (encoded.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("encoded", SR.GetString("ID6045"));
			}
			byte[] result;
			try
			{
				result = ProtectedData.Unprotect(encoded, this.entropy, DataProtectionScope.CurrentUser);
			}
			catch (CryptographicException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID1073"), innerException));
			}
			return result;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C714 File Offset: 0x0000A914
		public override byte[] Encode(byte[] value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (value.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID6044"));
			}
			byte[] result;
			try
			{
				result = ProtectedData.Protect(value, this.entropy, DataProtectionScope.CurrentUser);
			}
			catch (CryptographicException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID1074"), innerException));
			}
			return result;
		}

		// Token: 0x0400035B RID: 859
		private const string entropyString = "System.IdentityModel.ProtectedDataCookieTransform";

		// Token: 0x0400035C RID: 860
		private byte[] entropy;
	}
}
