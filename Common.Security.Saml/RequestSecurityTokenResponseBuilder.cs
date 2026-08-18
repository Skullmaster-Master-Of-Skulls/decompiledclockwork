using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x0200000E RID: 14
	public class RequestSecurityTokenResponseBuilder
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000331B File Offset: 0x0000151B
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003323 File Offset: 0x00001523
		private SecurityToken RequestedSecurityToken { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000332C File Offset: 0x0000152C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003334 File Offset: 0x00001534
		private SecurityToken RequestedProofToken { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000333D File Offset: 0x0000153D
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003345 File Offset: 0x00001545
		private SecurityKeyIdentifierClause RequestedAttachedReference { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000334E File Offset: 0x0000154E
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003356 File Offset: 0x00001556
		private SecurityKeyIdentifierClause RequestedUnattachedReference { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000335F File Offset: 0x0000155F
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003367 File Offset: 0x00001567
		private SecurityToken IssuerEntropy { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003370 File Offset: 0x00001570
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003378 File Offset: 0x00001578
		private bool ComputeKey { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003381 File Offset: 0x00001581
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003389 File Offset: 0x00001589
		private string Context { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003392 File Offset: 0x00001592
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000339A File Offset: 0x0000159A
		private string TokenType { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000033A3 File Offset: 0x000015A3
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000033AB File Offset: 0x000015AB
		private int KeySize { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000033B4 File Offset: 0x000015B4
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000033BC File Offset: 0x000015BC
		private EndpointAddress AppliesTo { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000033C5 File Offset: 0x000015C5
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000033CD File Offset: 0x000015CD
		private Lifetime TokenLifetime { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000033D6 File Offset: 0x000015D6
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000033DE File Offset: 0x000015DE
		private XmlElement RequestedSecurityTokenElement { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000033E8 File Offset: 0x000015E8
		public TokenXmlSerializer TokenSerializer
		{
			get
			{
				bool flag = this._tokenSerializer == null;
				if (flag)
				{
					this._tokenSerializer = new Saml2XmlSerializer();
				}
				return this._tokenSerializer;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003418 File Offset: 0x00001618
		public void AddRequestedSecurityToken(SecurityToken token)
		{
			this.RequestedSecurityToken = token;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003423 File Offset: 0x00001623
		public void AddRequestedSecurityToken(XmlElement requestedSecurityTokenXmlElement)
		{
			this.RequestedSecurityTokenElement = requestedSecurityTokenXmlElement;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000342E File Offset: 0x0000162E
		public void AddRequestedProofToken(SecurityToken token)
		{
			this.RequestedProofToken = token;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003439 File Offset: 0x00001639
		public void AddRequestedAttachedReference(SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			this.RequestedAttachedReference = securityKeyIdentifierClause;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003444 File Offset: 0x00001644
		public void AddRequestedUnattachedReference(SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			this.RequestedUnattachedReference = securityKeyIdentifierClause;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000344F File Offset: 0x0000164F
		public void AddIssuerEntropy(SecurityToken token)
		{
			this.IssuerEntropy = token;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000345A File Offset: 0x0000165A
		public void AddComputeKey(bool isComputeKey)
		{
			this.ComputeKey = isComputeKey;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003465 File Offset: 0x00001665
		public void AddContext(string context)
		{
			this.Context = context;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003470 File Offset: 0x00001670
		public void AddTokenType(string tokenType)
		{
			this.TokenType = tokenType;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000347B File Offset: 0x0000167B
		public void AddKeySize(int keySize)
		{
			this.KeySize = keySize;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003486 File Offset: 0x00001686
		public void AddAppliesTo(EndpointAddress endpointAddress)
		{
			this.AppliesTo = endpointAddress;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003491 File Offset: 0x00001691
		public void AddTokenLifetime(Lifetime lifetime)
		{
			this.TokenLifetime = lifetime;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000349C File Offset: 0x0000169C
		public RequestSecurityTokenResponse ToObject()
		{
			bool flag = this.RequestedSecurityToken == null && this.RequestedSecurityTokenElement != null;
			if (flag)
			{
				this.RequestedSecurityToken = this.TokenSerializer.DeserializeToken(this.RequestedSecurityTokenElement, this.RequestedProofToken, this.RequestedAttachedReference, this.RequestedUnattachedReference);
			}
			return new RequestSecurityTokenResponse(this.Context, this.TokenType, this.KeySize, this.AppliesTo, this.RequestedSecurityToken, this.RequestedProofToken, this.RequestedAttachedReference, this.RequestedUnattachedReference, this.ComputeKey, this.IssuerEntropy, this.TokenLifetime);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003538 File Offset: 0x00001738
		public void Clear()
		{
			this.RequestedSecurityToken = null;
			this.RequestedProofToken = null;
			this.RequestedAttachedReference = null;
			this.RequestedUnattachedReference = null;
			this.Context = string.Empty;
			this.TokenType = string.Empty;
			this.KeySize = 0;
			this.AppliesTo = null;
			this.ComputeKey = false;
			this.TokenLifetime = null;
		}

		// Token: 0x04000030 RID: 48
		private TokenXmlSerializer _tokenSerializer;
	}
}
