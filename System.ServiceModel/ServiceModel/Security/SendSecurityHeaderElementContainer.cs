using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B8 RID: 696
	internal class SendSecurityHeaderElementContainer
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x00053C32 File Offset: 0x00051E32
		private void Add<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00053C47 File Offset: 0x00051E47
		public SecurityToken[] GetSignedSupportingTokens()
		{
			if (this.signedSupportingTokens == null)
			{
				return null;
			}
			return this.signedSupportingTokens.ToArray();
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00053C5E File Offset: 0x00051E5E
		public void AddSignedSupportingToken(SecurityToken token)
		{
			this.Add<SecurityToken>(ref this.signedSupportingTokens, token);
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00053C6D File Offset: 0x00051E6D
		public List<SecurityToken> EndorsingSupportingTokens
		{
			get
			{
				return this.endorsingSupportingTokens;
			}
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00053C75 File Offset: 0x00051E75
		public SendSecurityHeaderElement[] GetBasicSupportingTokens()
		{
			if (this.basicSupportingTokens == null)
			{
				return null;
			}
			return this.basicSupportingTokens.ToArray();
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00053C8C File Offset: 0x00051E8C
		public void AddBasicSupportingToken(SendSecurityHeaderElement tokenElement)
		{
			this.Add<SendSecurityHeaderElement>(ref this.basicSupportingTokens, tokenElement);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00053C9B File Offset: 0x00051E9B
		public SecurityToken[] GetSignedEndorsingSupportingTokens()
		{
			if (this.signedEndorsingSupportingTokens == null)
			{
				return null;
			}
			return this.signedEndorsingSupportingTokens.ToArray();
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00053CB2 File Offset: 0x00051EB2
		public void AddSignedEndorsingSupportingToken(SecurityToken token)
		{
			this.Add<SecurityToken>(ref this.signedEndorsingSupportingTokens, token);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00053CC1 File Offset: 0x00051EC1
		public SecurityToken[] GetSignedEndorsingDerivedSupportingTokens()
		{
			if (this.signedEndorsingDerivedSupportingTokens == null)
			{
				return null;
			}
			return this.signedEndorsingDerivedSupportingTokens.ToArray();
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00053CD8 File Offset: 0x00051ED8
		public void AddSignedEndorsingDerivedSupportingToken(SecurityToken token)
		{
			this.Add<SecurityToken>(ref this.signedEndorsingDerivedSupportingTokens, token);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00053CE7 File Offset: 0x00051EE7
		public SecurityToken[] GetEndorsingSupportingTokens()
		{
			if (this.endorsingSupportingTokens == null)
			{
				return null;
			}
			return this.endorsingSupportingTokens.ToArray();
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00053CFE File Offset: 0x00051EFE
		public void AddEndorsingSupportingToken(SecurityToken token)
		{
			this.Add<SecurityToken>(ref this.endorsingSupportingTokens, token);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00053D0D File Offset: 0x00051F0D
		public SecurityToken[] GetEndorsingDerivedSupportingTokens()
		{
			if (this.endorsingDerivedSupportingTokens == null)
			{
				return null;
			}
			return this.endorsingDerivedSupportingTokens.ToArray();
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00053D24 File Offset: 0x00051F24
		public void AddEndorsingDerivedSupportingToken(SecurityToken token)
		{
			this.Add<SecurityToken>(ref this.endorsingDerivedSupportingTokens, token);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00053D33 File Offset: 0x00051F33
		public SendSecurityHeaderElement[] GetSignatureConfirmations()
		{
			if (this.signatureConfirmations == null)
			{
				return null;
			}
			return this.signatureConfirmations.ToArray();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00053D4A File Offset: 0x00051F4A
		public void AddSignatureConfirmation(SendSecurityHeaderElement confirmation)
		{
			this.Add<SendSecurityHeaderElement>(ref this.signatureConfirmations, confirmation);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00053D59 File Offset: 0x00051F59
		public SendSecurityHeaderElement[] GetEndorsingSignatures()
		{
			if (this.endorsingSignatures == null)
			{
				return null;
			}
			return this.endorsingSignatures.ToArray();
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00053D70 File Offset: 0x00051F70
		public void AddEndorsingSignature(SendSecurityHeaderElement signature)
		{
			this.Add<SendSecurityHeaderElement>(ref this.endorsingSignatures, signature);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00053D7F File Offset: 0x00051F7F
		public void MapSecurityTokenToStrClause(SecurityToken securityToken, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (this.securityTokenMappedToIdentifierClause == null)
			{
				this.securityTokenMappedToIdentifierClause = new Dictionary<SecurityToken, SecurityKeyIdentifierClause>();
			}
			if (!this.securityTokenMappedToIdentifierClause.ContainsKey(securityToken))
			{
				this.securityTokenMappedToIdentifierClause.Add(securityToken, keyIdentifierClause);
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00053DAF File Offset: 0x00051FAF
		public bool TryGetIdentifierClauseFromSecurityToken(SecurityToken securityToken, out SecurityKeyIdentifierClause keyIdentifierClause)
		{
			keyIdentifierClause = null;
			return securityToken != null && this.securityTokenMappedToIdentifierClause != null && this.securityTokenMappedToIdentifierClause.TryGetValue(securityToken, out keyIdentifierClause);
		}

		// Token: 0x04001B96 RID: 7062
		private List<SecurityToken> signedSupportingTokens;

		// Token: 0x04001B97 RID: 7063
		private List<SendSecurityHeaderElement> basicSupportingTokens;

		// Token: 0x04001B98 RID: 7064
		private List<SecurityToken> endorsingSupportingTokens;

		// Token: 0x04001B99 RID: 7065
		private List<SecurityToken> endorsingDerivedSupportingTokens;

		// Token: 0x04001B9A RID: 7066
		private List<SecurityToken> signedEndorsingSupportingTokens;

		// Token: 0x04001B9B RID: 7067
		private List<SecurityToken> signedEndorsingDerivedSupportingTokens;

		// Token: 0x04001B9C RID: 7068
		private List<SendSecurityHeaderElement> signatureConfirmations;

		// Token: 0x04001B9D RID: 7069
		private List<SendSecurityHeaderElement> endorsingSignatures;

		// Token: 0x04001B9E RID: 7070
		private Dictionary<SecurityToken, SecurityKeyIdentifierClause> securityTokenMappedToIdentifierClause;

		// Token: 0x04001B9F RID: 7071
		public SecurityTimestamp Timestamp;

		// Token: 0x04001BA0 RID: 7072
		public SecurityToken PrerequisiteToken;

		// Token: 0x04001BA1 RID: 7073
		public SecurityToken SourceSigningToken;

		// Token: 0x04001BA2 RID: 7074
		public SecurityToken DerivedSigningToken;

		// Token: 0x04001BA3 RID: 7075
		public SecurityToken SourceEncryptionToken;

		// Token: 0x04001BA4 RID: 7076
		public SecurityToken WrappedEncryptionToken;

		// Token: 0x04001BA5 RID: 7077
		public SecurityToken DerivedEncryptionToken;

		// Token: 0x04001BA6 RID: 7078
		public ISecurityElement ReferenceList;

		// Token: 0x04001BA7 RID: 7079
		public SendSecurityHeaderElement PrimarySignature;
	}
}
