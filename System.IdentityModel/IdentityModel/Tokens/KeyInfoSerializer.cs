using System;
using System.Collections.Generic;
using System.IdentityModel.Security;
using System.IdentityModel.Selectors;
using System.Runtime;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000129 RID: 297
	internal class KeyInfoSerializer : SecurityTokenSerializer
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x000222F8 File Offset: 0x000204F8
		internal static void ResetReadDepth()
		{
			KeyInfoSerializer.t_keyIdentifierReadDepth = 0;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00022300 File Offset: 0x00020500
		public KeyInfoSerializer(bool emitBspRequiredAttributes) : this(emitBspRequiredAttributes, new DictionaryManager(), XD.TrustDec2005Dictionary, null)
		{
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00022314 File Offset: 0x00020514
		public KeyInfoSerializer(bool emitBspRequiredAttributes, DictionaryManager dictionaryManager, TrustDictionary trustDictionary, SecurityTokenSerializer innerSecurityTokenSerializer) : this(emitBspRequiredAttributes, dictionaryManager, trustDictionary, innerSecurityTokenSerializer, null)
		{
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00022324 File Offset: 0x00020524
		public KeyInfoSerializer(bool emitBspRequiredAttributes, DictionaryManager dictionaryManager, TrustDictionary trustDictionary, SecurityTokenSerializer innerSecurityTokenSerializer, Func<KeyInfoSerializer, IEnumerable<SecurityTokenSerializer.SerializerEntries>> additionalEntries)
		{
			this.dictionaryManager = dictionaryManager;
			this.emitBspRequiredAttributes = emitBspRequiredAttributes;
			this.innerSecurityTokenSerializer = innerSecurityTokenSerializer;
			this.serializerEntries = new List<SecurityTokenSerializer.SerializerEntries>();
			this.serializerEntries.Add(new XmlDsigSep2000(this));
			this.serializerEntries.Add(new XmlEncApr2001(this));
			this.serializerEntries.Add(new WSTrust(this, trustDictionary));
			if (additionalEntries != null)
			{
				foreach (SecurityTokenSerializer.SerializerEntries item in additionalEntries(this))
				{
					this.serializerEntries.Add(item);
				}
			}
			bool flag = false;
			foreach (SecurityTokenSerializer.SerializerEntries serializerEntries in this.serializerEntries)
			{
				if (serializerEntries is WSSecurityXXX2005 || serializerEntries is WSSecurityJan2004)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.serializerEntries.Add(new WSSecurityXXX2005(this));
			}
			this.tokenEntries = new List<SecurityTokenSerializer.TokenEntry>();
			this.keyIdentifierEntries = new List<SecurityTokenSerializer.KeyIdentifierEntry>();
			this.keyIdentifierClauseEntries = new List<SecurityTokenSerializer.KeyIdentifierClauseEntry>();
			for (int i = 0; i < this.serializerEntries.Count; i++)
			{
				SecurityTokenSerializer.SerializerEntries serializerEntries2 = this.serializerEntries[i];
				serializerEntries2.PopulateTokenEntries(this.tokenEntries);
				serializerEntries2.PopulateKeyIdentifierEntries(this.keyIdentifierEntries);
				serializerEntries2.PopulateKeyIdentifierClauseEntries(this.keyIdentifierClauseEntries);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x000224B0 File Offset: 0x000206B0
		public DictionaryManager DictionaryManager
		{
			get
			{
				return this.dictionaryManager;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000224B8 File Offset: 0x000206B8
		public bool EmitBspRequiredAttributes
		{
			get
			{
				return this.emitBspRequiredAttributes;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000224C0 File Offset: 0x000206C0
		// (set) Token: 0x0600083D RID: 2109 RVA: 0x000224D2 File Offset: 0x000206D2
		public SecurityTokenSerializer InnerSecurityTokenSerializer
		{
			get
			{
				if (this.innerSecurityTokenSerializer != null)
				{
					return this.innerSecurityTokenSerializer;
				}
				return this;
			}
			set
			{
				this.innerSecurityTokenSerializer = value;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00002D09 File Offset: 0x00000F09
		protected override bool CanReadTokenCore(XmlReader reader)
		{
			return false;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000224DC File Offset: 0x000206DC
		protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CannotReadToken", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI,
				xmlDictionaryReader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null)
			})));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00002D09 File Offset: 0x00000F09
		protected override bool CanWriteTokenCore(SecurityToken token)
		{
			return false;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00022535 File Offset: 0x00020735
		protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StandardsManagerCannotWriteObject", new object[]
			{
				token.GetType()
			})));
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00022560 File Offset: 0x00020760
		protected override bool CanReadKeyIdentifierCore(XmlReader reader)
		{
			XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
			for (int i = 0; i < this.keyIdentifierEntries.Count; i++)
			{
				SecurityTokenSerializer.KeyIdentifierEntry keyIdentifierEntry = this.keyIdentifierEntries[i];
				if (keyIdentifierEntry.CanReadKeyIdentifierCore(reader2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000225A4 File Offset: 0x000207A4
		protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
		{
			KeyInfoSerializer.t_keyIdentifierReadDepth++;
			SecurityKeyIdentifier result;
			try
			{
				if (!LocalAppContextSwitches.AllowUnlimitedXmlRecursion && KeyInfoSerializer.t_keyIdentifierReadDepth > 8)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID4194", new object[]
					{
						KeyInfoSerializer.t_keyIdentifierReadDepth,
						8
					})));
				}
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
				xmlDictionaryReader.ReadStartElement(XD.XmlSignatureDictionary.KeyInfo, XD.XmlSignatureDictionary.Namespace);
				SecurityKeyIdentifier securityKeyIdentifier = new SecurityKeyIdentifier();
				while (xmlDictionaryReader.IsStartElement())
				{
					SecurityKeyIdentifierClause securityKeyIdentifierClause = this.InnerSecurityTokenSerializer.ReadKeyIdentifierClause(xmlDictionaryReader);
					if (securityKeyIdentifierClause == null)
					{
						xmlDictionaryReader.Skip();
					}
					else
					{
						securityKeyIdentifier.Add(securityKeyIdentifierClause);
					}
				}
				if (securityKeyIdentifier.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorDeserializingKeyIdentifierClause")));
				}
				xmlDictionaryReader.ReadEndElement();
				result = securityKeyIdentifier;
			}
			finally
			{
				KeyInfoSerializer.t_keyIdentifierReadDepth--;
			}
			return result;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002269C File Offset: 0x0002089C
		protected override bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier)
		{
			for (int i = 0; i < this.keyIdentifierEntries.Count; i++)
			{
				SecurityTokenSerializer.KeyIdentifierEntry keyIdentifierEntry = this.keyIdentifierEntries[i];
				if (keyIdentifierEntry.SupportsCore(keyIdentifier))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x000226D8 File Offset: 0x000208D8
		protected override void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
		{
			bool flag = false;
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			for (int i = 0; i < this.keyIdentifierEntries.Count; i++)
			{
				SecurityTokenSerializer.KeyIdentifierEntry keyIdentifierEntry = this.keyIdentifierEntries[i];
				if (keyIdentifierEntry.SupportsCore(keyIdentifier))
				{
					try
					{
						keyIdentifierEntry.WriteKeyIdentifierCore(xmlDictionaryWriter, keyIdentifier);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!this.ShouldWrapException(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorSerializingKeyIdentifier"), ex));
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StandardsManagerCannotWriteObject", new object[]
				{
					keyIdentifier.GetType()
				})));
			}
			xmlDictionaryWriter.Flush();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000227A0 File Offset: 0x000209A0
		protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
		{
			XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
			for (int i = 0; i < this.keyIdentifierClauseEntries.Count; i++)
			{
				SecurityTokenSerializer.KeyIdentifierClauseEntry keyIdentifierClauseEntry = this.keyIdentifierClauseEntries[i];
				if (keyIdentifierClauseEntry.CanReadKeyIdentifierClauseCore(reader2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000227E4 File Offset: 0x000209E4
		protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
		{
			XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
			for (int i = 0; i < this.keyIdentifierClauseEntries.Count; i++)
			{
				SecurityTokenSerializer.KeyIdentifierClauseEntry keyIdentifierClauseEntry = this.keyIdentifierClauseEntries[i];
				if (keyIdentifierClauseEntry.CanReadKeyIdentifierClauseCore(reader2))
				{
					try
					{
						return keyIdentifierClauseEntry.ReadKeyIdentifierClauseCore(reader2);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!this.ShouldWrapException(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorDeserializingKeyIdentifierClause"), ex));
					}
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CannotReadKeyIdentifierClause", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI
			})));
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000228A8 File Offset: 0x00020AA8
		protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			for (int i = 0; i < this.keyIdentifierClauseEntries.Count; i++)
			{
				SecurityTokenSerializer.KeyIdentifierClauseEntry keyIdentifierClauseEntry = this.keyIdentifierClauseEntries[i];
				if (keyIdentifierClauseEntry.SupportsCore(keyIdentifierClause))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000228E4 File Offset: 0x00020AE4
		protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			bool flag = false;
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			for (int i = 0; i < this.keyIdentifierClauseEntries.Count; i++)
			{
				SecurityTokenSerializer.KeyIdentifierClauseEntry keyIdentifierClauseEntry = this.keyIdentifierClauseEntries[i];
				if (keyIdentifierClauseEntry.SupportsCore(keyIdentifierClause))
				{
					try
					{
						keyIdentifierClauseEntry.WriteKeyIdentifierClauseCore(xmlDictionaryWriter, keyIdentifierClause);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!this.ShouldWrapException(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorSerializingKeyIdentifierClause"), ex));
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StandardsManagerCannotWriteObject", new object[]
				{
					keyIdentifierClause.GetType()
				})));
			}
			xmlDictionaryWriter.Flush();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000229AC File Offset: 0x00020BAC
		internal void PopulateStrEntries(IList<SecurityTokenSerializer.StrEntry> strEntries)
		{
			foreach (SecurityTokenSerializer.SerializerEntries serializerEntries in this.serializerEntries)
			{
				serializerEntries.PopulateStrEntries(strEntries);
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00022A00 File Offset: 0x00020C00
		private bool ShouldWrapException(Exception e)
		{
			return e is ArgumentException || e is FormatException || e is InvalidOperationException;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00022A20 File Offset: 0x00020C20
		internal Type[] GetTokenTypes(string tokenTypeUri)
		{
			if (tokenTypeUri != null)
			{
				for (int i = 0; i < this.tokenEntries.Count; i++)
				{
					SecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
					if (tokenEntry.SupportsTokenTypeUri(tokenTypeUri))
					{
						return tokenEntry.GetTokenTypes();
					}
				}
			}
			return null;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00022A64 File Offset: 0x00020C64
		protected internal virtual string GetTokenTypeUri(Type tokenType)
		{
			if (tokenType != null)
			{
				for (int i = 0; i < this.tokenEntries.Count; i++)
				{
					SecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
					if (tokenEntry.SupportsCore(tokenType))
					{
						return tokenEntry.TokenTypeUri;
					}
				}
			}
			return null;
		}

		// Token: 0x04000B03 RID: 2819
		private const int MaxKeyIdentifierReadDepth = 8;

		// Token: 0x04000B04 RID: 2820
		[ThreadStatic]
		private static int t_keyIdentifierReadDepth;

		// Token: 0x04000B05 RID: 2821
		private readonly List<SecurityTokenSerializer.KeyIdentifierEntry> keyIdentifierEntries;

		// Token: 0x04000B06 RID: 2822
		private readonly List<SecurityTokenSerializer.KeyIdentifierClauseEntry> keyIdentifierClauseEntries;

		// Token: 0x04000B07 RID: 2823
		private readonly List<SecurityTokenSerializer.SerializerEntries> serializerEntries;

		// Token: 0x04000B08 RID: 2824
		private readonly List<SecurityTokenSerializer.TokenEntry> tokenEntries;

		// Token: 0x04000B09 RID: 2825
		private DictionaryManager dictionaryManager;

		// Token: 0x04000B0A RID: 2826
		private bool emitBspRequiredAttributes;

		// Token: 0x04000B0B RID: 2827
		private SecurityTokenSerializer innerSecurityTokenSerializer;
	}
}
