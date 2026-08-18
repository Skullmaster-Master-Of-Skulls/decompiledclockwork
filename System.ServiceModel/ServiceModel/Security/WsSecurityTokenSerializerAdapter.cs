using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200035E RID: 862
	internal class WsSecurityTokenSerializerAdapter : WSSecurityTokenSerializer
	{
		// Token: 0x06001FA3 RID: 8099 RVA: 0x0007685C File Offset: 0x00074A5C
		public WsSecurityTokenSerializerAdapter(SecurityTokenHandlerCollection securityTokenHandlerCollection) : this(securityTokenHandlerCollection, MessageSecurityVersion.Default.SecurityVersion)
		{
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0007686F File Offset: 0x00074A6F
		public WsSecurityTokenSerializerAdapter(SecurityTokenHandlerCollection securityTokenHandlerCollection, SecurityVersion securityVersion) : this(securityTokenHandlerCollection, securityVersion, true, new SamlSerializer(), null, null)
		{
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00076884 File Offset: 0x00074A84
		public WsSecurityTokenSerializerAdapter(SecurityTokenHandlerCollection securityTokenHandlerCollection, SecurityVersion securityVersion, bool emitBspAttributes, SamlSerializer samlSerializer, SecurityStateEncoder stateEncoder, IEnumerable<Type> knownTypes) : this(securityTokenHandlerCollection, securityVersion, TrustVersion.WSTrust13, SecureConversationVersion.WSSecureConversation13, emitBspAttributes, samlSerializer, stateEncoder, knownTypes)
		{
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000768AC File Offset: 0x00074AAC
		public WsSecurityTokenSerializerAdapter(SecurityTokenHandlerCollection securityTokenHandlerCollection, SecurityVersion securityVersion, TrustVersion trustVersion, SecureConversationVersion secureConversationVersion, bool emitBspAttributes, SamlSerializer samlSerializer, SecurityStateEncoder stateEncoder, IEnumerable<Type> knownTypes) : base(securityVersion, trustVersion, secureConversationVersion, emitBspAttributes, samlSerializer, stateEncoder, knownTypes)
		{
			if (securityTokenHandlerCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandlerCollection");
			}
			this._scVersion = secureConversationVersion;
			this._securityTokenHandlers = securityTokenHandlerCollection;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x000768F8 File Offset: 0x00074AF8
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x00076900 File Offset: 0x00074B00
		public bool MapExceptionsToSoapFaults
		{
			get
			{
				return this._mapExceptionsToSoapFaults;
			}
			set
			{
				this._mapExceptionsToSoapFaults = value;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00076909 File Offset: 0x00074B09
		public SecurityTokenHandlerCollection SecurityTokenHandlers
		{
			get
			{
				return this._securityTokenHandlers;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001FAA RID: 8106 RVA: 0x00076911 File Offset: 0x00074B11
		// (set) Token: 0x06001FAB RID: 8107 RVA: 0x00076919 File Offset: 0x00074B19
		public ExceptionMapper ExceptionMapper
		{
			get
			{
				return this._exceptionMapper;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._exceptionMapper = value;
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00076935 File Offset: 0x00074B35
		protected override bool CanReadTokenCore(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return this._securityTokenHandlers.CanReadToken(reader) || base.CanReadTokenCore(reader);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00076961 File Offset: 0x00074B61
		protected override bool CanWriteTokenCore(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			return this._securityTokenHandlers.CanWriteToken(token) || base.CanWriteTokenCore(token);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00076990 File Offset: 0x00074B90
		protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			try
			{
				foreach (SecurityTokenHandler securityTokenHandler in this._securityTokenHandlers)
				{
					if (securityTokenHandler.CanReadToken(reader))
					{
						SecurityToken securityToken = securityTokenHandler.ReadToken(reader, tokenResolver);
						SessionSecurityToken sessionSecurityToken = securityToken as SessionSecurityToken;
						if (sessionSecurityToken == null)
						{
							return securityToken;
						}
						if (sessionSecurityToken.SecureConversationVersion.AbsoluteUri != this._scVersion.Namespace.Value)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperInvalidOperation(SR.GetString("ID4053", new object[]
							{
								sessionSecurityToken.SecureConversationVersion,
								this._scVersion
							}));
						}
						return SecurityContextSecurityTokenHelper.ConvertSessionTokenToSecurityContextSecurityToken(sessionSecurityToken);
					}
				}
				return base.ReadTokenCore(reader, tokenResolver);
			}
			catch (Exception ex)
			{
				if (!this.MapExceptionsToSoapFaults || !this._exceptionMapper.HandleSecurityTokenProcessingException(ex))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00076AA4 File Offset: 0x00074CA4
		protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			try
			{
				SecurityContextSecurityToken securityContextSecurityToken = token as SecurityContextSecurityToken;
				if (securityContextSecurityToken != null)
				{
					token = SecurityContextSecurityTokenHelper.ConvertSctToSessionToken(securityContextSecurityToken, this._scVersion);
				}
				SecurityTokenHandler securityTokenHandler = this._securityTokenHandlers[token];
				if (securityTokenHandler != null && securityTokenHandler.CanWriteToken)
				{
					securityTokenHandler.WriteToken(writer, token);
				}
				else
				{
					base.WriteTokenCore(writer, token);
				}
			}
			catch (Exception ex)
			{
				if (!this.MapExceptionsToSoapFaults || !this._exceptionMapper.HandleSecurityTokenProcessingException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00076B48 File Offset: 0x00074D48
		protected override bool CanReadKeyIdentifierCore(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#") || base.CanReadKeyIdentifierCore(reader);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00076B78 File Offset: 0x00074D78
		protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (reader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#"))
			{
				KeyInfo keyInfo = new KeyInfo(this);
				keyInfo.ReadXml(XmlDictionaryReader.CreateDictionaryReader(reader));
				return keyInfo.KeyIdentifier;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperXml(reader, SR.GetString("ID4192"));
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00076BDC File Offset: 0x00074DDC
		protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this._securityTokenHandlers)
			{
				if (securityTokenHandler.CanReadKeyIdentifierClause(reader))
				{
					return true;
				}
			}
			return base.CanReadKeyIdentifierClauseCore(reader);
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00076C4C File Offset: 0x00074E4C
		protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			foreach (SecurityTokenHandler securityTokenHandler in this._securityTokenHandlers)
			{
				if (securityTokenHandler.CanWriteKeyIdentifierClause(keyIdentifierClause))
				{
					return true;
				}
			}
			return base.CanWriteKeyIdentifierClauseCore(keyIdentifierClause);
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00076CBC File Offset: 0x00074EBC
		protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			try
			{
				foreach (SecurityTokenHandler securityTokenHandler in this._securityTokenHandlers)
				{
					if (securityTokenHandler.CanReadKeyIdentifierClause(reader))
					{
						return securityTokenHandler.ReadKeyIdentifierClause(reader);
					}
				}
				return base.ReadKeyIdentifierClauseCore(reader);
			}
			catch (Exception ex)
			{
				if (!this.MapExceptionsToSoapFaults || !this._exceptionMapper.HandleSecurityTokenProcessingException(ex))
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00076D60 File Offset: 0x00074F60
		protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (keyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifierClause");
			}
			try
			{
				foreach (SecurityTokenHandler securityTokenHandler in this._securityTokenHandlers)
				{
					if (securityTokenHandler.CanWriteKeyIdentifierClause(keyIdentifierClause))
					{
						securityTokenHandler.WriteKeyIdentifierClause(writer, keyIdentifierClause);
						return;
					}
				}
				base.WriteKeyIdentifierClauseCore(writer, keyIdentifierClause);
			}
			catch (Exception ex)
			{
				if (!this.MapExceptionsToSoapFaults || !this._exceptionMapper.HandleSecurityTokenProcessingException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x04001EEE RID: 7918
		private SecureConversationVersion _scVersion;

		// Token: 0x04001EEF RID: 7919
		private SecurityTokenHandlerCollection _securityTokenHandlers;

		// Token: 0x04001EF0 RID: 7920
		private bool _mapExceptionsToSoapFaults;

		// Token: 0x04001EF1 RID: 7921
		private ExceptionMapper _exceptionMapper = new ExceptionMapper();
	}
}
