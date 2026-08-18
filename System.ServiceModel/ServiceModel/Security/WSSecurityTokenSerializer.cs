using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000292 RID: 658
	public class WSSecurityTokenSerializer : SecurityTokenSerializer
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x00046BC7 File Offset: 0x00044DC7
		public WSSecurityTokenSerializer() : this(SecurityVersion.WSSecurity11)
		{
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00046BD4 File Offset: 0x00044DD4
		public WSSecurityTokenSerializer(bool emitBspRequiredAttributes) : this(SecurityVersion.WSSecurity11, emitBspRequiredAttributes)
		{
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00046BE2 File Offset: 0x00044DE2
		public WSSecurityTokenSerializer(SecurityVersion securityVersion) : this(securityVersion, false)
		{
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00046BEC File Offset: 0x00044DEC
		public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes) : this(securityVersion, emitBspRequiredAttributes, null)
		{
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00046BF7 File Offset: 0x00044DF7
		public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes, SamlSerializer samlSerializer) : this(securityVersion, emitBspRequiredAttributes, samlSerializer, null, null)
		{
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00046C04 File Offset: 0x00044E04
		public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes, SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes) : this(securityVersion, emitBspRequiredAttributes, samlSerializer, securityStateEncoder, knownTypes, 64, 128, 128)
		{
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00046C2C File Offset: 0x00044E2C
		public WSSecurityTokenSerializer(SecurityVersion securityVersion, TrustVersion trustVersion, SecureConversationVersion secureConversationVersion, bool emitBspRequiredAttributes, SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes) : this(securityVersion, trustVersion, secureConversationVersion, emitBspRequiredAttributes, samlSerializer, securityStateEncoder, knownTypes, 64, 128, 128)
		{
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00046C58 File Offset: 0x00044E58
		public WSSecurityTokenSerializer(SecurityVersion securityVersion, bool emitBspRequiredAttributes, SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes, int maximumKeyDerivationOffset, int maximumKeyDerivationLabelLength, int maximumKeyDerivationNonceLength) : this(securityVersion, TrustVersion.Default, SecureConversationVersion.Default, emitBspRequiredAttributes, samlSerializer, securityStateEncoder, knownTypes, maximumKeyDerivationOffset, maximumKeyDerivationLabelLength, maximumKeyDerivationNonceLength)
		{
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00046C84 File Offset: 0x00044E84
		public WSSecurityTokenSerializer(SecurityVersion securityVersion, TrustVersion trustVersion, SecureConversationVersion secureConversationVersion, bool emitBspRequiredAttributes, SamlSerializer samlSerializer, SecurityStateEncoder securityStateEncoder, IEnumerable<Type> knownTypes, int maximumKeyDerivationOffset, int maximumKeyDerivationLabelLength, int maximumKeyDerivationNonceLength)
		{
			if (securityVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("securityVersion"));
			}
			if (maximumKeyDerivationOffset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maximumKeyDerivationOffset", SR.GetString("ValueMustBeNonNegative")));
			}
			if (maximumKeyDerivationLabelLength < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maximumKeyDerivationLabelLength", SR.GetString("ValueMustBeNonNegative")));
			}
			if (maximumKeyDerivationNonceLength <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maximumKeyDerivationNonceLength", SR.GetString("ValueMustBeGreaterThanZero")));
			}
			this.securityVersion = securityVersion;
			this.emitBspRequiredAttributes = emitBspRequiredAttributes;
			this.maximumKeyDerivationOffset = maximumKeyDerivationOffset;
			this.maximumKeyDerivationNonceLength = maximumKeyDerivationNonceLength;
			this.maximumKeyDerivationLabelLength = maximumKeyDerivationLabelLength;
			this.serializerEntries = new List<WSSecurityTokenSerializer.SerializerEntries>();
			if (secureConversationVersion == SecureConversationVersion.WSSecureConversationFeb2005)
			{
				this.secureConversation = new WSSecureConversationFeb2005(this, securityStateEncoder, knownTypes, maximumKeyDerivationOffset, maximumKeyDerivationLabelLength, maximumKeyDerivationNonceLength);
			}
			else
			{
				if (secureConversationVersion != SecureConversationVersion.WSSecureConversation13)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				this.secureConversation = new WSSecureConversationDec2005(this, securityStateEncoder, knownTypes, maximumKeyDerivationOffset, maximumKeyDerivationLabelLength, maximumKeyDerivationNonceLength);
			}
			if (securityVersion == SecurityVersion.WSSecurity10)
			{
				this.serializerEntries.Add(new WSSecurityJan2004(this, samlSerializer));
			}
			else
			{
				if (securityVersion != SecurityVersion.WSSecurity11)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("securityVersion", SR.GetString("MessageSecurityVersionOutOfRange")));
				}
				this.serializerEntries.Add(new WSSecurityXXX2005(this, samlSerializer));
			}
			this.serializerEntries.Add(this.secureConversation);
			TrustDictionary trustDictionary;
			if (trustVersion == TrustVersion.WSTrustFeb2005)
			{
				this.serializerEntries.Add(new WSTrustFeb2005(this));
				trustDictionary = new TrustFeb2005Dictionary(new WSSecurityTokenSerializer.CollectionDictionary(DXD.TrustDec2005Dictionary.Feb2005DictionaryStrings));
			}
			else
			{
				if (trustVersion != TrustVersion.WSTrust13)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
				}
				this.serializerEntries.Add(new WSTrustDec2005(this));
				trustDictionary = new TrustDec2005Dictionary(new WSSecurityTokenSerializer.CollectionDictionary(DXD.TrustDec2005Dictionary.Dec2005DictionaryString));
			}
			this.tokenEntries = new List<WSSecurityTokenSerializer.TokenEntry>();
			for (int i = 0; i < this.serializerEntries.Count; i++)
			{
				WSSecurityTokenSerializer.SerializerEntries serializerEntries = this.serializerEntries[i];
				serializerEntries.PopulateTokenEntries(this.tokenEntries);
			}
			DictionaryManager dictionaryManager = new DictionaryManager(ServiceModelDictionary.CurrentVersion);
			dictionaryManager.SecureConversationDec2005Dictionary = new SecureConversationDec2005Dictionary(new WSSecurityTokenSerializer.CollectionDictionary(DXD.SecureConversationDec2005Dictionary.SecureConversationDictionaryStrings));
			dictionaryManager.SecurityAlgorithmDec2005Dictionary = new SecurityAlgorithmDec2005Dictionary(new WSSecurityTokenSerializer.CollectionDictionary(DXD.SecurityAlgorithmDec2005Dictionary.SecurityAlgorithmDictionaryStrings));
			this.keyInfoSerializer = new WSKeyInfoSerializer(this.emitBspRequiredAttributes, dictionaryManager, trustDictionary, this, securityVersion, secureConversationVersion);
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x00046F06 File Offset: 0x00045106
		public static WSSecurityTokenSerializer DefaultInstance
		{
			get
			{
				if (WSSecurityTokenSerializer.instance == null)
				{
					WSSecurityTokenSerializer.instance = new WSSecurityTokenSerializer();
				}
				return WSSecurityTokenSerializer.instance;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00046F1E File Offset: 0x0004511E
		public bool EmitBspRequiredAttributes
		{
			get
			{
				return this.emitBspRequiredAttributes;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x00046F26 File Offset: 0x00045126
		public SecurityVersion SecurityVersion
		{
			get
			{
				return this.securityVersion;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00046F2E File Offset: 0x0004512E
		public int MaximumKeyDerivationOffset
		{
			get
			{
				return this.maximumKeyDerivationOffset;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00046F36 File Offset: 0x00045136
		public int MaximumKeyDerivationLabelLength
		{
			get
			{
				return this.maximumKeyDerivationLabelLength;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x00046F3E File Offset: 0x0004513E
		public int MaximumKeyDerivationNonceLength
		{
			get
			{
				return this.maximumKeyDerivationNonceLength;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00046F46 File Offset: 0x00045146
		internal WSSecureConversation SecureConversation
		{
			get
			{
				return this.secureConversation;
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00046F4E File Offset: 0x0004514E
		private bool ShouldWrapException(Exception e)
		{
			return !Fx.IsFatal(e) && (e is ArgumentException || e is FormatException || e is InvalidOperationException);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00046F78 File Offset: 0x00045178
		protected override bool CanReadTokenCore(XmlReader reader)
		{
			XmlDictionaryReader reader2 = XmlDictionaryReader.CreateDictionaryReader(reader);
			for (int i = 0; i < this.tokenEntries.Count; i++)
			{
				WSSecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
				if (tokenEntry.CanReadTokenCore(reader2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00046FBC File Offset: 0x000451BC
		protected override SecurityToken ReadTokenCore(XmlReader reader, SecurityTokenResolver tokenResolver)
		{
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			for (int i = 0; i < this.tokenEntries.Count; i++)
			{
				WSSecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
				if (tokenEntry.CanReadTokenCore(xmlDictionaryReader))
				{
					try
					{
						return tokenEntry.ReadTokenCore(xmlDictionaryReader, tokenResolver);
					}
					catch (Exception ex)
					{
						if (!this.ShouldWrapException(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorDeserializingTokenXml"), ex));
					}
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CannotReadToken", new object[]
			{
				reader.LocalName,
				reader.NamespaceURI,
				xmlDictionaryReader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null)
			})));
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004708C File Offset: 0x0004528C
		protected override bool CanWriteTokenCore(SecurityToken token)
		{
			for (int i = 0; i < this.tokenEntries.Count; i++)
			{
				WSSecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
				if (tokenEntry.SupportsCore(token.GetType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000470D0 File Offset: 0x000452D0
		protected override void WriteTokenCore(XmlWriter writer, SecurityToken token)
		{
			bool flag = false;
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			if (token.GetType() == typeof(ProviderBackedSecurityToken))
			{
				token = (token as ProviderBackedSecurityToken).Token;
			}
			for (int i = 0; i < this.tokenEntries.Count; i++)
			{
				WSSecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
				if (tokenEntry.SupportsCore(token.GetType()))
				{
					try
					{
						tokenEntry.WriteTokenCore(xmlDictionaryWriter, token);
					}
					catch (Exception ex)
					{
						if (!this.ShouldWrapException(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorSerializingSecurityToken"), ex));
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("StandardsManagerCannotWriteObject", new object[]
				{
					token.GetType()
				})));
			}
			xmlDictionaryWriter.Flush();
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000471B8 File Offset: 0x000453B8
		protected override bool CanReadKeyIdentifierCore(XmlReader reader)
		{
			bool result;
			try
			{
				result = this.keyInfoSerializer.CanReadKeyIdentifier(reader);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
			return result;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000471FC File Offset: 0x000453FC
		protected override SecurityKeyIdentifier ReadKeyIdentifierCore(XmlReader reader)
		{
			SecurityKeyIdentifier result;
			try
			{
				result = this.keyInfoSerializer.ReadKeyIdentifier(reader);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
			return result;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00047240 File Offset: 0x00045440
		protected override bool CanWriteKeyIdentifierCore(SecurityKeyIdentifier keyIdentifier)
		{
			bool result;
			try
			{
				result = this.keyInfoSerializer.CanWriteKeyIdentifier(keyIdentifier);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
			return result;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00047284 File Offset: 0x00045484
		protected override void WriteKeyIdentifierCore(XmlWriter writer, SecurityKeyIdentifier keyIdentifier)
		{
			try
			{
				this.keyInfoSerializer.WriteKeyIdentifier(writer, keyIdentifier);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000472C8 File Offset: 0x000454C8
		protected override bool CanReadKeyIdentifierClauseCore(XmlReader reader)
		{
			bool result;
			try
			{
				result = this.keyInfoSerializer.CanReadKeyIdentifierClause(reader);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
			return result;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0004730C File Offset: 0x0004550C
		protected override SecurityKeyIdentifierClause ReadKeyIdentifierClauseCore(XmlReader reader)
		{
			SecurityKeyIdentifierClause result;
			try
			{
				result = this.keyInfoSerializer.ReadKeyIdentifierClause(reader);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
			return result;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00047350 File Offset: 0x00045550
		protected override bool CanWriteKeyIdentifierClauseCore(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			bool result;
			try
			{
				result = this.keyInfoSerializer.CanWriteKeyIdentifierClause(keyIdentifierClause);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
			return result;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00047394 File Offset: 0x00045594
		protected override void WriteKeyIdentifierClauseCore(XmlWriter writer, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			try
			{
				this.keyInfoSerializer.WriteKeyIdentifierClause(writer, keyIdentifierClause);
			}
			catch (SecurityMessageSerializationException ex)
			{
				throw FxTrace.Exception.AsError(new MessageSecurityException(ex.Message));
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000473D8 File Offset: 0x000455D8
		internal Type[] GetTokenTypes(string tokenTypeUri)
		{
			if (tokenTypeUri != null)
			{
				for (int i = 0; i < this.tokenEntries.Count; i++)
				{
					WSSecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
					if (tokenEntry.SupportsTokenTypeUri(tokenTypeUri))
					{
						return tokenEntry.GetTokenTypes();
					}
				}
			}
			return null;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004741C File Offset: 0x0004561C
		protected internal virtual string GetTokenTypeUri(Type tokenType)
		{
			if (tokenType != null)
			{
				for (int i = 0; i < this.tokenEntries.Count; i++)
				{
					WSSecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
					if (tokenEntry.SupportsCore(tokenType))
					{
						return tokenEntry.TokenTypeUri;
					}
				}
			}
			return null;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00047468 File Offset: 0x00045668
		public virtual bool TryCreateKeyIdentifierClauseFromTokenXml(XmlElement element, SecurityTokenReferenceStyle tokenReferenceStyle, out SecurityKeyIdentifierClause securityKeyIdentifierClause)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			securityKeyIdentifierClause = null;
			try
			{
				securityKeyIdentifierClause = this.CreateKeyIdentifierClauseFromTokenXml(element, tokenReferenceStyle);
			}
			catch (XmlException exception)
			{
				if (DiagnosticUtility.ShouldTraceError)
				{
					TraceUtility.TraceEvent(TraceEventType.Error, 458752, SR.GetString("TraceCodeSecurity"), null, exception);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000474D0 File Offset: 0x000456D0
		public virtual SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXml(XmlElement element, SecurityTokenReferenceStyle tokenReferenceStyle)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			for (int i = 0; i < this.tokenEntries.Count; i++)
			{
				WSSecurityTokenSerializer.TokenEntry tokenEntry = this.tokenEntries[i];
				if (tokenEntry.CanReadTokenCore(element))
				{
					try
					{
						return tokenEntry.CreateKeyIdentifierClauseFromTokenXmlCore(element, tokenReferenceStyle);
					}
					catch (Exception ex)
					{
						if (!this.ShouldWrapException(ex))
						{
							throw;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ErrorDeserializingKeyIdentifierClauseFromTokenXml"), ex));
					}
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("CannotReadToken", new object[]
			{
				element.LocalName,
				element.NamespaceURI,
				element.GetAttribute("ValueType", null)
			})));
		}

		// Token: 0x04001A25 RID: 6693
		private const int DefaultMaximumKeyDerivationOffset = 64;

		// Token: 0x04001A26 RID: 6694
		private const int DefaultMaximumKeyDerivationLabelLength = 128;

		// Token: 0x04001A27 RID: 6695
		private const int DefaultMaximumKeyDerivationNonceLength = 128;

		// Token: 0x04001A28 RID: 6696
		private static WSSecurityTokenSerializer instance;

		// Token: 0x04001A29 RID: 6697
		private readonly bool emitBspRequiredAttributes;

		// Token: 0x04001A2A RID: 6698
		private readonly SecurityVersion securityVersion;

		// Token: 0x04001A2B RID: 6699
		private readonly List<WSSecurityTokenSerializer.SerializerEntries> serializerEntries;

		// Token: 0x04001A2C RID: 6700
		private WSSecureConversation secureConversation;

		// Token: 0x04001A2D RID: 6701
		private readonly List<WSSecurityTokenSerializer.TokenEntry> tokenEntries;

		// Token: 0x04001A2E RID: 6702
		private int maximumKeyDerivationOffset;

		// Token: 0x04001A2F RID: 6703
		private int maximumKeyDerivationLabelLength;

		// Token: 0x04001A30 RID: 6704
		private int maximumKeyDerivationNonceLength;

		// Token: 0x04001A31 RID: 6705
		private KeyInfoSerializer keyInfoSerializer;

		// Token: 0x02000B2B RID: 2859
		internal new abstract class TokenEntry
		{
			// Token: 0x06007009 RID: 28681 RVA: 0x0019F950 File Offset: 0x0019DB50
			public virtual IAsyncResult BeginReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver, AsyncCallback callback, object state)
			{
				SecurityToken data = this.ReadTokenCore(reader, tokenResolver);
				return new CompletedAsyncResult<SecurityToken>(data, callback, state);
			}

			// Token: 0x17001A2E RID: 6702
			// (get) Token: 0x0600700A RID: 28682
			protected abstract XmlDictionaryString LocalName { get; }

			// Token: 0x17001A2F RID: 6703
			// (get) Token: 0x0600700B RID: 28683
			protected abstract XmlDictionaryString NamespaceUri { get; }

			// Token: 0x17001A30 RID: 6704
			// (get) Token: 0x0600700C RID: 28684 RVA: 0x0019F96F File Offset: 0x0019DB6F
			public Type TokenType
			{
				get
				{
					return this.GetTokenTypes()[0];
				}
			}

			// Token: 0x17001A31 RID: 6705
			// (get) Token: 0x0600700D RID: 28685
			public abstract string TokenTypeUri { get; }

			// Token: 0x17001A32 RID: 6706
			// (get) Token: 0x0600700E RID: 28686
			protected abstract string ValueTypeUri { get; }

			// Token: 0x0600700F RID: 28687
			protected abstract Type[] GetTokenTypesCore();

			// Token: 0x06007010 RID: 28688 RVA: 0x0019F979 File Offset: 0x0019DB79
			public Type[] GetTokenTypes()
			{
				if (this.tokenTypes == null)
				{
					this.tokenTypes = this.GetTokenTypesCore();
				}
				return this.tokenTypes;
			}

			// Token: 0x06007011 RID: 28689 RVA: 0x0019F998 File Offset: 0x0019DB98
			public bool SupportsCore(Type tokenType)
			{
				Type[] array = this.GetTokenTypes();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsAssignableFrom(tokenType))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06007012 RID: 28690 RVA: 0x0019F9C8 File Offset: 0x0019DBC8
			public virtual bool SupportsTokenTypeUri(string tokenTypeUri)
			{
				return this.TokenTypeUri == tokenTypeUri;
			}

			// Token: 0x06007013 RID: 28691 RVA: 0x0019F9D8 File Offset: 0x0019DBD8
			protected static SecurityKeyIdentifierClause CreateDirectReference(XmlElement issuedTokenXml, string idAttributeLocalName, string idAttributeNamespace, Type tokenType)
			{
				string attribute = issuedTokenXml.GetAttribute(idAttributeLocalName, idAttributeNamespace);
				if (attribute == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("RequiredAttributeMissing", new object[]
					{
						idAttributeLocalName,
						issuedTokenXml.LocalName
					})));
				}
				return new LocalIdKeyIdentifierClause(attribute, tokenType);
			}

			// Token: 0x06007014 RID: 28692 RVA: 0x0019FA28 File Offset: 0x0019DC28
			public virtual bool CanReadTokenCore(XmlElement element)
			{
				string a = null;
				if (element.HasAttribute("ValueType", null))
				{
					a = element.GetAttribute("ValueType", null);
				}
				return element.LocalName == this.LocalName.Value && element.NamespaceURI == this.NamespaceUri.Value && a == this.ValueTypeUri;
			}

			// Token: 0x06007015 RID: 28693 RVA: 0x0019FA90 File Offset: 0x0019DC90
			public virtual bool CanReadTokenCore(XmlDictionaryReader reader)
			{
				return reader.IsStartElement(this.LocalName, this.NamespaceUri) && reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null) == this.ValueTypeUri;
			}

			// Token: 0x06007016 RID: 28694 RVA: 0x0019FAC4 File Offset: 0x0019DCC4
			public virtual SecurityToken EndReadTokenCore(IAsyncResult result)
			{
				return CompletedAsyncResult<SecurityToken>.End(result);
			}

			// Token: 0x06007017 RID: 28695
			public abstract SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle);

			// Token: 0x06007018 RID: 28696
			public abstract SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver);

			// Token: 0x06007019 RID: 28697
			public abstract void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token);

			// Token: 0x04003FF6 RID: 16374
			private Type[] tokenTypes;
		}

		// Token: 0x02000B2C RID: 2860
		internal new abstract class SerializerEntries
		{
			// Token: 0x0600701B RID: 28699 RVA: 0x0019FAD4 File Offset: 0x0019DCD4
			public virtual void PopulateTokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntries)
			{
			}
		}

		// Token: 0x02000B2D RID: 2861
		internal class CollectionDictionary : IXmlDictionary
		{
			// Token: 0x0600701D RID: 28701 RVA: 0x0019FADE File Offset: 0x0019DCDE
			public CollectionDictionary(List<XmlDictionaryString> dictionaryStrings)
			{
				if (dictionaryStrings == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("dictionaryStrings"));
				}
				this.dictionaryStrings = dictionaryStrings;
			}

			// Token: 0x0600701E RID: 28702 RVA: 0x0019FB08 File Offset: 0x0019DD08
			public bool TryLookup(string value, out XmlDictionaryString result)
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				for (int i = 0; i < this.dictionaryStrings.Count; i++)
				{
					if (this.dictionaryStrings[i].Value.Equals(value))
					{
						result = this.dictionaryStrings[i];
						return true;
					}
				}
				result = null;
				return false;
			}

			// Token: 0x0600701F RID: 28703 RVA: 0x0019FB70 File Offset: 0x0019DD70
			public bool TryLookup(int key, out XmlDictionaryString result)
			{
				for (int i = 0; i < this.dictionaryStrings.Count; i++)
				{
					if (this.dictionaryStrings[i].Key == key)
					{
						result = this.dictionaryStrings[i];
						return true;
					}
				}
				result = null;
				return false;
			}

			// Token: 0x06007020 RID: 28704 RVA: 0x0019FBBC File Offset: 0x0019DDBC
			public bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result)
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				for (int i = 0; i < this.dictionaryStrings.Count; i++)
				{
					if (this.dictionaryStrings[i].Key == value.Key && this.dictionaryStrings[i].Value.Equals(value.Value))
					{
						result = this.dictionaryStrings[i];
						return true;
					}
				}
				result = null;
				return false;
			}

			// Token: 0x04003FF7 RID: 16375
			private List<XmlDictionaryString> dictionaryStrings;
		}
	}
}
