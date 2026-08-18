using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.Security.Claims;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000171 RID: 369
	public class SecurityTokenDescriptor
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00036D58 File Offset: 0x00034F58
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x00036D60 File Offset: 0x00034F60
		public string AppliesToAddress
		{
			get
			{
				return this.appliesToAddress;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !UriUtil.CanCreateValidUri(value, UriKind.Absolute))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2002")));
				}
				this.appliesToAddress = value;
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00036D94 File Offset: 0x00034F94
		public virtual void ApplyTo(RequestSecurityTokenResponse response)
		{
			if (response == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("response");
			}
			if (this.tokenType != null)
			{
				response.TokenType = this.tokenType;
			}
			if (this.token != null)
			{
				response.RequestedSecurityToken = new RequestedSecurityToken(this.token);
			}
			if (this.attachedReference != null)
			{
				response.RequestedAttachedReference = this.attachedReference;
			}
			if (this.unattachedReference != null)
			{
				response.RequestedUnattachedReference = this.unattachedReference;
			}
			if (this.lifetime != null)
			{
				response.Lifetime = this.lifetime;
			}
			if (this.proofDescriptor != null)
			{
				this.proofDescriptor.ApplyTo(response);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00036E31 File Offset: 0x00035031
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x00036E39 File Offset: 0x00035039
		public string ReplyToAddress
		{
			get
			{
				return this.replyToAddress;
			}
			set
			{
				this.replyToAddress = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00036E42 File Offset: 0x00035042
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x00036E4A File Offset: 0x0003504A
		public EncryptingCredentials EncryptingCredentials
		{
			get
			{
				return this.encryptingCredentials;
			}
			set
			{
				this.encryptingCredentials = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x00036E53 File Offset: 0x00035053
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x00036E5B File Offset: 0x0003505B
		public SigningCredentials SigningCredentials
		{
			get
			{
				return this.signingCredentials;
			}
			set
			{
				this.signingCredentials = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00036E64 File Offset: 0x00035064
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x00036E6C File Offset: 0x0003506C
		public SecurityKeyIdentifierClause AttachedReference
		{
			get
			{
				return this.attachedReference;
			}
			set
			{
				this.attachedReference = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x00036E75 File Offset: 0x00035075
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x00036E7D File Offset: 0x0003507D
		public string TokenIssuerName
		{
			get
			{
				return this.tokenIssuerName;
			}
			set
			{
				this.tokenIssuerName = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00036E86 File Offset: 0x00035086
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x00036E8E File Offset: 0x0003508E
		public ProofDescriptor Proof
		{
			get
			{
				return this.proofDescriptor;
			}
			set
			{
				this.proofDescriptor = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00036E97 File Offset: 0x00035097
		public Dictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00036E9F File Offset: 0x0003509F
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00036EA7 File Offset: 0x000350A7
		public SecurityToken Token
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00036EB0 File Offset: 0x000350B0
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x00036EB8 File Offset: 0x000350B8
		public string TokenType
		{
			get
			{
				return this.tokenType;
			}
			set
			{
				this.tokenType = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00036EC1 File Offset: 0x000350C1
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00036EC9 File Offset: 0x000350C9
		public SecurityKeyIdentifierClause UnattachedReference
		{
			get
			{
				return this.unattachedReference;
			}
			set
			{
				this.unattachedReference = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00036ED2 File Offset: 0x000350D2
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x00036EDA File Offset: 0x000350DA
		public Lifetime Lifetime
		{
			get
			{
				return this.lifetime;
			}
			set
			{
				this.lifetime = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00036EE3 File Offset: 0x000350E3
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x00036EEB File Offset: 0x000350EB
		public ClaimsIdentity Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00036EF4 File Offset: 0x000350F4
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x00036EFC File Offset: 0x000350FC
		public AuthenticationInformation AuthenticationInfo
		{
			get
			{
				return this.authenticationInfo;
			}
			set
			{
				this.authenticationInfo = value;
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00036F05 File Offset: 0x00035105
		public void AddAuthenticationClaims(string authType)
		{
			this.AddAuthenticationClaims(authType, DateTime.UtcNow);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00036F14 File Offset: 0x00035114
		public void AddAuthenticationClaims(string authType, DateTime time)
		{
			this.Subject.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", authType, "http://www.w3.org/2001/XMLSchema#string"));
			this.Subject.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", XmlConvert.ToString(time.ToUniversalTime(), DateTimeFormats.Generated), "http://www.w3.org/2001/XMLSchema#dateTime"));
		}

		// Token: 0x04000C2C RID: 3116
		private SecurityKeyIdentifierClause attachedReference;

		// Token: 0x04000C2D RID: 3117
		private AuthenticationInformation authenticationInfo;

		// Token: 0x04000C2E RID: 3118
		private string tokenIssuerName;

		// Token: 0x04000C2F RID: 3119
		private ProofDescriptor proofDescriptor;

		// Token: 0x04000C30 RID: 3120
		private ClaimsIdentity subject;

		// Token: 0x04000C31 RID: 3121
		private SecurityToken token;

		// Token: 0x04000C32 RID: 3122
		private string tokenType;

		// Token: 0x04000C33 RID: 3123
		private SecurityKeyIdentifierClause unattachedReference;

		// Token: 0x04000C34 RID: 3124
		private Lifetime lifetime;

		// Token: 0x04000C35 RID: 3125
		private string appliesToAddress;

		// Token: 0x04000C36 RID: 3126
		private string replyToAddress;

		// Token: 0x04000C37 RID: 3127
		private EncryptingCredentials encryptingCredentials;

		// Token: 0x04000C38 RID: 3128
		private SigningCredentials signingCredentials;

		// Token: 0x04000C39 RID: 3129
		private Dictionary<string, object> properties = new Dictionary<string, object>();
	}
}
