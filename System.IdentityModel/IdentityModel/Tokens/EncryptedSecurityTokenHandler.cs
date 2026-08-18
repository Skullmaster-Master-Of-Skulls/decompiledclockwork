using System;
using System.Diagnostics;
using System.IdentityModel.Selectors;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200011D RID: 285
	public class EncryptedSecurityTokenHandler : SecurityTokenHandler
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x00020C2D File Offset: 0x0001EE2D
		public override bool CanReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("EncryptedKey", "http://www.w3.org/2001/04/xmlenc#");
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00020C52 File Offset: 0x0001EE52
		public override bool CanReadToken(XmlReader reader)
		{
			return EncryptedDataElement.CanReadFrom(reader);
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWriteToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00020C5C File Offset: 0x0001EE5C
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00020CD0 File Offset: 0x0001EED0
		public SecurityTokenSerializer KeyInfoSerializer
		{
			get
			{
				if (this._keyInfoSerializer == null)
				{
					object syncObject = this._syncObject;
					lock (syncObject)
					{
						if (this._keyInfoSerializer == null)
						{
							SecurityTokenHandlerCollection securityTokenHandlerCollection = (base.ContainingCollection != null) ? base.ContainingCollection : SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
							this._keyInfoSerializer = new SecurityTokenSerializerAdapter(securityTokenHandlerCollection);
						}
					}
				}
				return this._keyInfoSerializer;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this._keyInfoSerializer = value;
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00020CEC File Offset: 0x0001EEEC
		public override SecurityToken ReadToken(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			KeyInfo.ResetReadDepth();
			System.IdentityModel.Tokens.KeyInfoSerializer.ResetReadDepth();
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			if (base.Configuration.ServiceTokenResolver == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4276"));
			}
			EncryptedDataElement encryptedDataElement = new EncryptedDataElement(this.KeyInfoSerializer);
			encryptedDataElement.ReadXml(XmlDictionaryReader.CreateDictionaryReader(reader));
			SecurityKey securityKey = null;
			foreach (SecurityKeyIdentifierClause keyIdentifierClause in encryptedDataElement.KeyIdentifier)
			{
				base.Configuration.ServiceTokenResolver.TryResolveSecurityKey(keyIdentifierClause, out securityKey);
				if (securityKey != null)
				{
					break;
				}
			}
			if (securityKey == null && encryptedDataElement.KeyIdentifier.CanCreateKey)
			{
				securityKey = encryptedDataElement.KeyIdentifier.CreateKey();
			}
			if (securityKey == null)
			{
				EncryptedKeyIdentifierClause encryptedKeyIdentifierClause;
				if (encryptedDataElement.KeyIdentifier.TryFind<EncryptedKeyIdentifierClause>(out encryptedKeyIdentifierClause))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EncryptedTokenDecryptionFailedException(SR.GetString("ID4036", new object[]
					{
						XmlUtil.SerializeSecurityKeyIdentifier(encryptedDataElement.KeyIdentifier, base.ContainingCollection.KeyInfoSerializer)
					})));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new EncryptedTokenDecryptionFailedException(SR.GetString("ID4036", new object[]
				{
					encryptedDataElement.KeyIdentifier.ToString()
				})));
			}
			else
			{
				SymmetricSecurityKey symmetricSecurityKey = securityKey as SymmetricSecurityKey;
				if (symmetricSecurityKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID4023")));
				}
				byte[] buffer;
				using (SymmetricAlgorithm symmetricAlgorithm = symmetricSecurityKey.GetSymmetricAlgorithm(encryptedDataElement.Algorithm))
				{
					buffer = encryptedDataElement.Decrypt(symmetricAlgorithm);
				}
				SecurityToken result;
				using (XmlReader xmlReader = XmlDictionaryReader.CreateTextReader(buffer, XmlDictionaryReaderQuotas.Max))
				{
					if (base.ContainingCollection == null || !base.ContainingCollection.CanReadToken(xmlReader))
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4014", new object[]
						{
							xmlReader.LocalName,
							xmlReader.NamespaceURI
						}));
					}
					result = base.ContainingCollection.ReadToken(xmlReader);
				}
				return result;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00020F24 File Offset: 0x0001F124
		public override SecurityKeyIdentifierClause ReadKeyIdentifierClause(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (reader.IsStartElement("EncryptedKey", "http://www.w3.org/2001/04/xmlenc#"))
			{
				EncryptedKeyElement encryptedKeyElement = new EncryptedKeyElement(this.KeyInfoSerializer);
				encryptedKeyElement.ReadXml(XmlDictionaryReader.CreateDictionaryReader(reader));
				return new EncryptedKeyIdentifierClause(encryptedKeyElement.CipherData.CipherValue, encryptedKeyElement.Algorithm, encryptedKeyElement.KeyIdentifier);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3275", new object[]
			{
				reader.Name,
				reader.NamespaceURI
			})));
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00020FBC File Offset: 0x0001F1BC
		[Conditional("DEBUG")]
		private static void DebugEncryptedTokenClearText(byte[] bytes, Encoding encoding)
		{
			string @string = encoding.GetString(bytes);
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00020FD1 File Offset: 0x0001F1D1
		public override Type TokenType
		{
			get
			{
				return typeof(EncryptedSecurityToken);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00020FDD File Offset: 0x0001F1DD
		public override string[] GetTokenTypeIdentifiers()
		{
			return EncryptedSecurityTokenHandler._tokenTypeIdentifiers;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		public override void WriteToken(XmlWriter writer, SecurityToken token)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			EncryptedSecurityToken encryptedSecurityToken = token as EncryptedSecurityToken;
			if (encryptedSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID4024"));
			}
			if (base.ContainingCollection == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4279"));
			}
			EncryptedDataElement encryptedDataElement = new EncryptedDataElement(this.KeyInfoSerializer);
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false))
				{
					SecurityTokenHandler securityTokenHandler = base.ContainingCollection[encryptedSecurityToken.Token.GetType()];
					if (securityTokenHandler == null)
					{
						throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4224", new object[]
						{
							encryptedSecurityToken.Token.GetType()
						}));
					}
					securityTokenHandler.WriteToken(xmlDictionaryWriter, encryptedSecurityToken.Token);
				}
				EncryptingCredentials encryptingCredentials = encryptedSecurityToken.EncryptingCredentials;
				encryptedDataElement.Type = "http://www.w3.org/2001/04/xmlenc#Element";
				encryptedDataElement.KeyIdentifier = encryptingCredentials.SecurityKeyIdentifier;
				encryptedDataElement.Algorithm = encryptingCredentials.Algorithm;
				SymmetricSecurityKey symmetricSecurityKey = encryptingCredentials.SecurityKey as SymmetricSecurityKey;
				if (symmetricSecurityKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID3064")));
				}
				using (SymmetricAlgorithm symmetricAlgorithm = symmetricSecurityKey.GetSymmetricAlgorithm(encryptingCredentials.Algorithm))
				{
					byte[] buffer = memoryStream.GetBuffer();
					encryptedDataElement.Encrypt(symmetricAlgorithm, buffer, 0, (int)memoryStream.Length);
				}
			}
			encryptedDataElement.WriteXml(writer, this.KeyInfoSerializer);
		}

		// Token: 0x04000ADD RID: 2781
		private static string[] _tokenTypeIdentifiers = new string[1];

		// Token: 0x04000ADE RID: 2782
		private SecurityTokenSerializer _keyInfoSerializer;

		// Token: 0x04000ADF RID: 2783
		private object _syncObject = new object();
	}
}
