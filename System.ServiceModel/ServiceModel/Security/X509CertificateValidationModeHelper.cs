using System;
using System.ComponentModel;

namespace System.ServiceModel.Security
{
	// Token: 0x02000346 RID: 838
	internal static class X509CertificateValidationModeHelper
	{
		// Token: 0x06001E68 RID: 7784 RVA: 0x00070760 File Offset: 0x0006E960
		public static bool IsDefined(X509CertificateValidationMode validationMode)
		{
			return validationMode == X509CertificateValidationMode.None || validationMode == X509CertificateValidationMode.PeerTrust || validationMode == X509CertificateValidationMode.ChainTrust || validationMode == X509CertificateValidationMode.PeerOrChainTrust || validationMode == X509CertificateValidationMode.Custom;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x00070777 File Offset: 0x0006E977
		internal static void Validate(X509CertificateValidationMode value)
		{
			if (!X509CertificateValidationModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(X509CertificateValidationMode)));
			}
		}
	}
}
