using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Diagnostics.Application;
using System.IdentityModel.Selectors;
using System.Runtime.Diagnostics;
using System.Security.Claims;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000179 RID: 377
	public abstract class SecurityTokenHandler : ICustomIdentityConfiguration
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x000371EC File Offset: 0x000353EC
		private EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null)
				{
					this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
				}
				return this.eventTraceActivity;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool CanValidateToken
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool CanWriteToken
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00037208 File Offset: 0x00035408
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x00037210 File Offset: 0x00035410
		public SecurityTokenHandlerConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
			set
			{
				this.configuration = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00037219 File Offset: 0x00035419
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x00037221 File Offset: 0x00035421
		public SecurityTokenHandlerCollection ContainingCollection
		{
			get
			{
				return this.collection;
			}
			internal set
			{
				this.collection = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000BD3 RID: 3027
		public abstract Type TokenType { get; }

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool CanReadToken(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool CanReadToken(string tokenString)
		{
			return false;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0003722A File Offset: 0x0003542A
		public virtual SecurityToken ReadToken(XmlReader reader)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"ReadToken"
			})));
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003725B File Offset: 0x0003545B
		public virtual SecurityToken ReadToken(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			return this.ReadToken(reader);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0003722A File Offset: 0x0003542A
		public virtual SecurityToken ReadToken(string tokenString)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"ReadToken"
			})));
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00037264 File Offset: 0x00035464
		public virtual void WriteToken(XmlWriter writer, SecurityToken token)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"WriteToken"
			})));
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00037264 File Offset: 0x00035464
		public virtual string WriteToken(SecurityToken token)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"WriteToken"
			})));
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool CanReadKeyIdentifierClause(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00037295 File Offset: 0x00035495
		public virtual SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"ReadKeyIdentifierClause"
			})));
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool CanWriteKeyIdentifierClause(SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			return false;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000372C6 File Offset: 0x000354C6
		public virtual void WriteKeyIdentifierClause(XmlWriter writer, SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"WriteKeyIdentifierClause"
			})));
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000372F7 File Offset: 0x000354F7
		public virtual SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"CreateToken"
			})));
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00037328 File Offset: 0x00035528
		public virtual SecurityKeyIdentifierClause CreateSecurityTokenReference(SecurityToken token, bool attached)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"CreateSecurityTokenReference"
			})));
		}

		// Token: 0x06000BE1 RID: 3041
		public abstract string[] GetTokenTypeIdentifiers();

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00037359 File Offset: 0x00035559
		public virtual ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID4008", new object[]
			{
				"SecurityTokenHandler",
				"ValidateToken"
			})));
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000024C1 File Offset: 0x000006C1
		protected virtual void DetectReplayedToken(SecurityToken token)
		{
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0000443A File Offset: 0x0000263A
		public virtual void LoadCustomConfiguration(XmlNodeList nodelist)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID0023", new object[]
			{
				base.GetType().AssemblyQualifiedName
			})));
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0003738A File Offset: 0x0003558A
		protected void TraceTokenValidationSuccess(SecurityToken token)
		{
			if (TD.TokenValidationSuccessIsEnabled())
			{
				TD.TokenValidationSuccess(this.EventTraceActivity, token.GetType().ToString(), token.Id);
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000373AF File Offset: 0x000355AF
		protected void TraceTokenValidationFailure(SecurityToken token, string errorMessage)
		{
			if (TD.TokenValidationFailureIsEnabled())
			{
				TD.TokenValidationFailure(this.EventTraceActivity, token.GetType().ToString(), token.Id, errorMessage);
			}
		}

		// Token: 0x04000C4C RID: 3148
		private SecurityTokenHandlerCollection collection;

		// Token: 0x04000C4D RID: 3149
		private SecurityTokenHandlerConfiguration configuration;

		// Token: 0x04000C4E RID: 3150
		private EventTraceActivity eventTraceActivity;
	}
}
