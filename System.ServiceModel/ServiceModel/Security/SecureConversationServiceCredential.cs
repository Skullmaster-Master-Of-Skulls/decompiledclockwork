using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x0200033A RID: 826
	public sealed class SecureConversationServiceCredential
	{
		// Token: 0x06001DF6 RID: 7670 RVA: 0x0006ED21 File Offset: 0x0006CF21
		internal SecureConversationServiceCredential()
		{
			this.securityStateEncoder = SecureConversationServiceCredential.defaultSecurityStateEncoder;
			this.securityContextClaimTypes = new Collection<Type>();
			SamlAssertion.AddSamlClaimTypes(this.securityContextClaimTypes);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0006ED4C File Offset: 0x0006CF4C
		internal SecureConversationServiceCredential(SecureConversationServiceCredential other)
		{
			this.securityStateEncoder = other.securityStateEncoder;
			this.securityContextClaimTypes = new Collection<Type>();
			for (int i = 0; i < other.securityContextClaimTypes.Count; i++)
			{
				this.securityContextClaimTypes.Add(other.securityContextClaimTypes[i]);
			}
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0006EDAF File Offset: 0x0006CFAF
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x0006EDB7 File Offset: 0x0006CFB7
		public SecurityStateEncoder SecurityStateEncoder
		{
			get
			{
				return this.securityStateEncoder;
			}
			set
			{
				this.ThrowIfImmutable();
				this.securityStateEncoder = value;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0006EDC6 File Offset: 0x0006CFC6
		public Collection<Type> SecurityContextClaimTypes
		{
			get
			{
				return this.securityContextClaimTypes;
			}
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0006EDCE File Offset: 0x0006CFCE
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0006EDD7 File Offset: 0x0006CFD7
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E54 RID: 7764
		private static readonly SecurityStateEncoder defaultSecurityStateEncoder = new DataProtectionSecurityStateEncoder();

		// Token: 0x04001E55 RID: 7765
		private SecurityStateEncoder securityStateEncoder;

		// Token: 0x04001E56 RID: 7766
		private Collection<Type> securityContextClaimTypes;

		// Token: 0x04001E57 RID: 7767
		private bool isReadOnly;
	}
}
