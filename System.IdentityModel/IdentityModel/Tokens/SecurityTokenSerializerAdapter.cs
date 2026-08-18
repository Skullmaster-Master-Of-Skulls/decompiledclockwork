using System;
using System.IdentityModel.Selectors;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200017D RID: 381
	internal class SecurityTokenSerializerAdapter : SecurityTokenSerializer
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00038158 File Offset: 0x00036358
		public SecurityTokenSerializerAdapter(SecurityTokenHandlerCollection securityTokenHandlerCollection)
		{
			if (securityTokenHandlerCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityTokenHandlerCollection");
			}
			this._securityTokenHandlers = securityTokenHandlerCollection;
			KeyInfoSerializer keyInfoSerializer = securityTokenHandlerCollection.KeyInfoSerializer as KeyInfoSerializer;
			if (keyInfoSerializer != null)
			{
				keyInfoSerializer.InnerSecurityTokenSerializer = this;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0003819B File Offset: 0x0003639B
		public SecurityTokenHandlerCollection SecurityTokenHandlers
		{
			get
			{
				return this._securityTokenHandlers;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000381A3 File Offset: 0x000363A3
		protected override bool CanReadTokenCore(XmlReader reader)
		{
			return this._securityTokenHandlers.CanReadToken(reader);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000381B1 File Offset: 0x000363B1
		protected override bool CanWriteTokenCore(SecurityToken token)
		{
			return this._securityTokenHandlers.CanWriteToken(token);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000381BF File Offset: 0x000363BF
		protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			return this._securityTokenHandlers.ReadToken(reader);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000381CD File Offset: 0x000363CD
		protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
		{
			this._securityTokenHandlers.WriteToken(writer, token);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000381DC File Offset: 0x000363DC
		protected override bool CanReadKeyIdentifierCore(XmlReader reader)
		{
			return reader.IsStartElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000381F0 File Offset: 0x000363F0
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

		// Token: 0x06000C39 RID: 3129 RVA: 0x00038251 File Offset: 0x00036451
		protected override bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier)
		{
			return this._securityTokenHandlers.KeyInfoSerializer != null && this._securityTokenHandlers.KeyInfoSerializer.CanWriteKeyIdentifier(keyIdentifier);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00038273 File Offset: 0x00036473
		protected override void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
		{
			this._securityTokenHandlers.KeyInfoSerializer.WriteKeyIdentifier(writer, keyIdentifier);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00038288 File Offset: 0x00036488
		protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
		{
			foreach (SecurityTokenHandler securityTokenHandler in this._securityTokenHandlers)
			{
				if (securityTokenHandler.CanReadKeyIdentifierClause(reader))
				{
					return true;
				}
			}
			return this._securityTokenHandlers.KeyInfoSerializer != null && this._securityTokenHandlers.KeyInfoSerializer.CanReadKeyIdentifierClause(reader);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00038300 File Offset: 0x00036500
		protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			foreach (SecurityTokenHandler securityTokenHandler in this._securityTokenHandlers)
			{
				if (securityTokenHandler.CanWriteKeyIdentifierClause(keyIdentifierClause))
				{
					return true;
				}
			}
			return this._securityTokenHandlers.KeyInfoSerializer != null && this._securityTokenHandlers.KeyInfoSerializer.CanWriteKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00038378 File Offset: 0x00036578
		protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
		{
			return this._securityTokenHandlers.ReadKeyIdentifierClause(reader);
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00038386 File Offset: 0x00036586
		protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			this._securityTokenHandlers.WriteKeyIdentifierClause(writer, keyIdentifierClause);
		}

		// Token: 0x04000C6D RID: 3181
		private SecurityTokenHandlerCollection _securityTokenHandlers;
	}
}
