using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C6 RID: 710
	internal class SecurityTokenParametersEnumerable : IEnumerable<SecurityTokenParameters>, IEnumerable
	{
		// Token: 0x060016F6 RID: 5878 RVA: 0x00057240 File Offset: 0x00055440
		public SecurityTokenParametersEnumerable(SecurityBindingElement sbe) : this(sbe, false)
		{
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0005724A File Offset: 0x0005544A
		public SecurityTokenParametersEnumerable(SecurityBindingElement sbe, bool clientTokensOnly)
		{
			if (sbe == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sbe");
			}
			this.sbe = sbe;
			this.clientTokensOnly = clientTokensOnly;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00057273 File Offset: 0x00055473
		public IEnumerator<SecurityTokenParameters> GetEnumerator()
		{
			if (this.sbe is SymmetricSecurityBindingElement)
			{
				SymmetricSecurityBindingElement symmetricSecurityBindingElement = (SymmetricSecurityBindingElement)this.sbe;
				if (symmetricSecurityBindingElement.ProtectionTokenParameters != null && (!this.clientTokensOnly || !symmetricSecurityBindingElement.ProtectionTokenParameters.HasAsymmetricKey))
				{
					yield return symmetricSecurityBindingElement.ProtectionTokenParameters;
				}
			}
			else if (this.sbe is AsymmetricSecurityBindingElement)
			{
				AsymmetricSecurityBindingElement asbe = (AsymmetricSecurityBindingElement)this.sbe;
				if (asbe.InitiatorTokenParameters != null)
				{
					yield return asbe.InitiatorTokenParameters;
				}
				if (asbe.RecipientTokenParameters != null && !this.clientTokensOnly)
				{
					yield return asbe.RecipientTokenParameters;
				}
				asbe = null;
			}
			foreach (SecurityTokenParameters securityTokenParameters in this.sbe.EndpointSupportingTokenParameters.Endorsing)
			{
				if (securityTokenParameters != null)
				{
					yield return securityTokenParameters;
				}
			}
			IEnumerator<SecurityTokenParameters> enumerator = null;
			foreach (SecurityTokenParameters securityTokenParameters2 in this.sbe.EndpointSupportingTokenParameters.SignedEncrypted)
			{
				if (securityTokenParameters2 != null)
				{
					yield return securityTokenParameters2;
				}
			}
			enumerator = null;
			foreach (SecurityTokenParameters securityTokenParameters3 in this.sbe.EndpointSupportingTokenParameters.SignedEndorsing)
			{
				if (securityTokenParameters3 != null)
				{
					yield return securityTokenParameters3;
				}
			}
			enumerator = null;
			foreach (SecurityTokenParameters securityTokenParameters4 in this.sbe.EndpointSupportingTokenParameters.Signed)
			{
				if (securityTokenParameters4 != null)
				{
					yield return securityTokenParameters4;
				}
			}
			enumerator = null;
			foreach (SupportingTokenParameters str in this.sbe.OperationSupportingTokenParameters.Values)
			{
				if (str != null)
				{
					foreach (SecurityTokenParameters securityTokenParameters5 in str.Endorsing)
					{
						if (securityTokenParameters5 != null)
						{
							yield return securityTokenParameters5;
						}
					}
					enumerator = null;
					foreach (SecurityTokenParameters securityTokenParameters6 in str.SignedEncrypted)
					{
						if (securityTokenParameters6 != null)
						{
							yield return securityTokenParameters6;
						}
					}
					enumerator = null;
					foreach (SecurityTokenParameters securityTokenParameters7 in str.SignedEndorsing)
					{
						if (securityTokenParameters7 != null)
						{
							yield return securityTokenParameters7;
						}
					}
					enumerator = null;
					foreach (SecurityTokenParameters securityTokenParameters8 in str.Signed)
					{
						if (securityTokenParameters8 != null)
						{
							yield return securityTokenParameters8;
						}
					}
					enumerator = null;
				}
				str = null;
			}
			IEnumerator<SupportingTokenParameters> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00057282 File Offset: 0x00055482
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x04001C01 RID: 7169
		private SecurityBindingElement sbe;

		// Token: 0x04001C02 RID: 7170
		private bool clientTokensOnly;
	}
}
