using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ServiceModel.Security
{
	// Token: 0x0200035B RID: 859
	internal class SecurityTokenSpecificationEnumerable : IEnumerable<SecurityTokenSpecification>, IEnumerable
	{
		// Token: 0x06001F95 RID: 8085 RVA: 0x00076566 File Offset: 0x00074766
		public SecurityTokenSpecificationEnumerable(SecurityMessageProperty securityMessageProperty)
		{
			if (securityMessageProperty == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityMessageProperty");
			}
			this._securityMessageProperty = securityMessageProperty;
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00076588 File Offset: 0x00074788
		public IEnumerator<SecurityTokenSpecification> GetEnumerator()
		{
			if (this._securityMessageProperty.InitiatorToken != null)
			{
				yield return this._securityMessageProperty.InitiatorToken;
			}
			if (this._securityMessageProperty.ProtectionToken != null)
			{
				yield return this._securityMessageProperty.ProtectionToken;
			}
			if (this._securityMessageProperty.HasIncomingSupportingTokens)
			{
				foreach (SecurityTokenSpecification securityTokenSpecification in this._securityMessageProperty.IncomingSupportingTokens)
				{
					if (securityTokenSpecification != null)
					{
						yield return securityTokenSpecification;
					}
				}
				IEnumerator<SupportingTokenSpecification> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00076597 File Offset: 0x00074797
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x04001EE9 RID: 7913
		private SecurityMessageProperty _securityMessageProperty;
	}
}
