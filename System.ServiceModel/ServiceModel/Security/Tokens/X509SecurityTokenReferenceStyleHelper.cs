using System;
using System.ComponentModel;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003AB RID: 939
	internal static class X509SecurityTokenReferenceStyleHelper
	{
		// Token: 0x06002331 RID: 9009 RVA: 0x0008061A File Offset: 0x0007E81A
		public static bool IsDefined(X509KeyIdentifierClauseType value)
		{
			return value == X509KeyIdentifierClauseType.Any || value == X509KeyIdentifierClauseType.IssuerSerial || value == X509KeyIdentifierClauseType.SubjectKeyIdentifier || value == X509KeyIdentifierClauseType.Thumbprint || value == X509KeyIdentifierClauseType.RawDataKeyIdentifier;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00080631 File Offset: 0x0007E831
		public static void Validate(X509KeyIdentifierClauseType value)
		{
			if (!X509SecurityTokenReferenceStyleHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(X509KeyIdentifierClauseType)));
			}
		}
	}
}
