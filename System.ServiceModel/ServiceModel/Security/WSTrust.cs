using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net.Security;
using System.Runtime;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000298 RID: 664
	internal abstract class WSTrust : WSSecurityTokenSerializer.SerializerEntries
	{
		// Token: 0x06001427 RID: 5159 RVA: 0x0004C11D File Offset: 0x0004A31D
		public WSTrust(WSSecurityTokenSerializer tokenSerializer)
		{
			this.tokenSerializer = tokenSerializer;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0004C12C File Offset: 0x0004A32C
		public WSSecurityTokenSerializer WSSecurityTokenSerializer
		{
			get
			{
				return this.tokenSerializer;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001429 RID: 5161
		public abstract TrustDictionary SerializerDictionary { get; }

		// Token: 0x0600142A RID: 5162 RVA: 0x0004C134 File Offset: 0x0004A334
		public override void PopulateTokenEntries(IList<WSSecurityTokenSerializer.TokenEntry> tokenEntryList)
		{
			tokenEntryList.Add(new WSTrust.BinarySecretTokenEntry(this));
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004C144 File Offset: 0x0004A344
		protected static bool CheckElement(XmlElement element, string name, string ns, out string value)
		{
			value = null;
			if (element.LocalName != name || element.NamespaceURI != ns)
			{
				return false;
			}
			if (element.FirstChild is XmlText)
			{
				value = ((XmlText)element.FirstChild).Value;
				return true;
			}
			return false;
		}

		// Token: 0x04001AA0 RID: 6816
		private WSSecurityTokenSerializer tokenSerializer;

		// Token: 0x02000B32 RID: 2866
		private class BinarySecretTokenEntry : WSSecurityTokenSerializer.TokenEntry
		{
			// Token: 0x06007031 RID: 28721 RVA: 0x001A0354 File Offset: 0x0019E554
			public BinarySecretTokenEntry(WSTrust parent)
			{
				this.parent = parent;
				this.otherDictionary = null;
				if (parent.SerializerDictionary is TrustDec2005Dictionary)
				{
					this.otherDictionary = XD.TrustFeb2005Dictionary;
				}
				if (parent.SerializerDictionary is TrustFeb2005Dictionary)
				{
					this.otherDictionary = DXD.TrustDec2005Dictionary;
				}
				if (this.otherDictionary == null)
				{
					this.otherDictionary = this.parent.SerializerDictionary;
				}
			}

			// Token: 0x17001A35 RID: 6709
			// (get) Token: 0x06007032 RID: 28722 RVA: 0x001A03BE File Offset: 0x0019E5BE
			protected override XmlDictionaryString LocalName
			{
				get
				{
					return this.parent.SerializerDictionary.BinarySecret;
				}
			}

			// Token: 0x17001A36 RID: 6710
			// (get) Token: 0x06007033 RID: 28723 RVA: 0x001A03D0 File Offset: 0x0019E5D0
			protected override XmlDictionaryString NamespaceUri
			{
				get
				{
					return this.parent.SerializerDictionary.Namespace;
				}
			}

			// Token: 0x06007034 RID: 28724 RVA: 0x001A03E2 File Offset: 0x0019E5E2
			protected override Type[] GetTokenTypesCore()
			{
				return new Type[]
				{
					typeof(BinarySecretSecurityToken)
				};
			}

			// Token: 0x17001A37 RID: 6711
			// (get) Token: 0x06007035 RID: 28725 RVA: 0x001A03F7 File Offset: 0x0019E5F7
			public override string TokenTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001A38 RID: 6712
			// (get) Token: 0x06007036 RID: 28726 RVA: 0x001A03FA File Offset: 0x0019E5FA
			protected override string ValueTypeUri
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007037 RID: 28727 RVA: 0x001A0400 File Offset: 0x0019E600
			public override bool CanReadTokenCore(XmlElement element)
			{
				string a = null;
				if (element.HasAttribute("ValueType", null))
				{
					a = element.GetAttribute("ValueType", null);
				}
				return element.LocalName == this.LocalName.Value && (element.NamespaceURI == this.NamespaceUri.Value || element.NamespaceURI == this.otherDictionary.Namespace.Value) && a == this.ValueTypeUri;
			}

			// Token: 0x06007038 RID: 28728 RVA: 0x001A0488 File Offset: 0x0019E688
			public override bool CanReadTokenCore(XmlDictionaryReader reader)
			{
				return (reader.IsStartElement(this.LocalName, this.NamespaceUri) || reader.IsStartElement(this.LocalName, this.otherDictionary.Namespace)) && reader.GetAttribute(XD.SecurityJan2004Dictionary.ValueType, null) == this.ValueTypeUri;
			}

			// Token: 0x06007039 RID: 28729 RVA: 0x001A04E0 File Offset: 0x0019E6E0
			public override SecurityKeyIdentifierClause CreateKeyIdentifierClauseFromTokenXmlCore(XmlElement issuedTokenXml, SecurityTokenReferenceStyle tokenReferenceStyle)
			{
				TokenReferenceStyleHelper.Validate(tokenReferenceStyle);
				if (tokenReferenceStyle == SecurityTokenReferenceStyle.Internal)
				{
					return WSSecurityTokenSerializer.TokenEntry.CreateDirectReference(issuedTokenXml, "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", typeof(GenericXmlSecurityToken));
				}
				if (tokenReferenceStyle != SecurityTokenReferenceStyle.External)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("tokenReferenceStyle"));
				}
				return null;
			}

			// Token: 0x0600703A RID: 28730 RVA: 0x001A0530 File Offset: 0x0019E730
			public override SecurityToken ReadTokenCore(XmlDictionaryReader reader, SecurityTokenResolver tokenResolver)
			{
				string attribute = reader.GetAttribute(XD.SecurityJan2004Dictionary.TypeAttribute, null);
				string attribute2 = reader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				bool flag = false;
				if (attribute != null && attribute.Length > 0)
				{
					if (attribute == this.parent.SerializerDictionary.NonceBinarySecret.Value || attribute == this.otherDictionary.NonceBinarySecret.Value)
					{
						flag = true;
					}
					else if (attribute != this.parent.SerializerDictionary.SymmetricKeyBinarySecret.Value && attribute != this.otherDictionary.SymmetricKeyBinarySecret.Value)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("UnexpectedBinarySecretType", new object[]
						{
							this.parent.SerializerDictionary.SymmetricKeyBinarySecret.Value,
							attribute
						})));
					}
				}
				byte[] key = reader.ReadElementContentAsBase64();
				if (flag)
				{
					return new NonceToken(attribute2, key);
				}
				return new BinarySecretSecurityToken(attribute2, key);
			}

			// Token: 0x0600703B RID: 28731 RVA: 0x001A0644 File Offset: 0x0019E844
			public override void WriteTokenCore(XmlDictionaryWriter writer, SecurityToken token)
			{
				BinarySecretSecurityToken binarySecretSecurityToken = token as BinarySecretSecurityToken;
				byte[] keyBytes = binarySecretSecurityToken.GetKeyBytes();
				writer.WriteStartElement(this.parent.SerializerDictionary.Prefix.Value, this.parent.SerializerDictionary.BinarySecret, this.parent.SerializerDictionary.Namespace);
				if (binarySecretSecurityToken.Id != null)
				{
					writer.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, binarySecretSecurityToken.Id);
				}
				if (token is NonceToken)
				{
					writer.WriteAttributeString(XD.SecurityJan2004Dictionary.TypeAttribute, null, this.parent.SerializerDictionary.NonceBinarySecret.Value);
				}
				writer.WriteBase64(keyBytes, 0, keyBytes.Length);
				writer.WriteEndElement();
			}

			// Token: 0x04003FFE RID: 16382
			private WSTrust parent;

			// Token: 0x04003FFF RID: 16383
			private TrustDictionary otherDictionary;
		}

		// Token: 0x02000B33 RID: 2867
		public abstract class Driver : TrustDriver
		{
			// Token: 0x0600703C RID: 28732 RVA: 0x001A0710 File Offset: 0x0019E910
			public Driver(SecurityStandardsManager standardsManager)
			{
				this.standardsManager = standardsManager;
				this.entropyAuthenticators = new List<SecurityTokenAuthenticator>(2);
			}

			// Token: 0x17001A39 RID: 6713
			// (get) Token: 0x0600703D RID: 28733
			public abstract TrustDictionary DriverDictionary { get; }

			// Token: 0x17001A3A RID: 6714
			// (get) Token: 0x0600703E RID: 28734 RVA: 0x001A072B File Offset: 0x0019E92B
			public override XmlDictionaryString RequestSecurityTokenAction
			{
				get
				{
					return this.DriverDictionary.RequestSecurityTokenIssuance;
				}
			}

			// Token: 0x17001A3B RID: 6715
			// (get) Token: 0x0600703F RID: 28735 RVA: 0x001A0738 File Offset: 0x0019E938
			public override XmlDictionaryString RequestSecurityTokenResponseAction
			{
				get
				{
					return this.DriverDictionary.RequestSecurityTokenIssuanceResponse;
				}
			}

			// Token: 0x17001A3C RID: 6716
			// (get) Token: 0x06007040 RID: 28736 RVA: 0x001A0745 File Offset: 0x0019E945
			public override string RequestTypeIssue
			{
				get
				{
					return this.DriverDictionary.RequestTypeIssue.Value;
				}
			}

			// Token: 0x17001A3D RID: 6717
			// (get) Token: 0x06007041 RID: 28737 RVA: 0x001A0757 File Offset: 0x0019E957
			public override string ComputedKeyAlgorithm
			{
				get
				{
					return this.DriverDictionary.Psha1ComputedKeyUri.Value;
				}
			}

			// Token: 0x17001A3E RID: 6718
			// (get) Token: 0x06007042 RID: 28738 RVA: 0x001A0769 File Offset: 0x0019E969
			public override SecurityStandardsManager StandardsManager
			{
				get
				{
					return this.standardsManager;
				}
			}

			// Token: 0x17001A3F RID: 6719
			// (get) Token: 0x06007043 RID: 28739 RVA: 0x001A0771 File Offset: 0x0019E971
			public override XmlDictionaryString Namespace
			{
				get
				{
					return this.DriverDictionary.Namespace;
				}
			}

			// Token: 0x06007044 RID: 28740 RVA: 0x001A0780 File Offset: 0x0019E980
			public override RequestSecurityToken CreateRequestSecurityToken(XmlReader xmlReader)
			{
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(xmlReader);
				xmlDictionaryReader.MoveToStartElement(this.DriverDictionary.RequestSecurityToken, this.DriverDictionary.Namespace);
				string context = null;
				string tokenType = null;
				string requestType = null;
				int keySize = 0;
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.ReadNode(xmlDictionaryReader) as XmlElement;
				SecurityKeyIdentifierClause renewTarget = null;
				SecurityKeyIdentifierClause closeTarget = null;
				for (int i = 0; i < xmlElement.Attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = xmlElement.Attributes[i];
					if (xmlAttribute.LocalName == this.DriverDictionary.Context.Value)
					{
						context = xmlAttribute.Value;
					}
				}
				for (int j = 0; j < xmlElement.ChildNodes.Count; j++)
				{
					XmlElement xmlElement2 = xmlElement.ChildNodes[j] as XmlElement;
					if (xmlElement2 != null)
					{
						if (xmlElement2.LocalName == this.DriverDictionary.TokenType.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							tokenType = XmlHelper.ReadTextElementAsTrimmedString(xmlElement2);
						}
						else if (xmlElement2.LocalName == this.DriverDictionary.RequestType.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							requestType = XmlHelper.ReadTextElementAsTrimmedString(xmlElement2);
						}
						else if (xmlElement2.LocalName == this.DriverDictionary.KeySize.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							keySize = int.Parse(XmlHelper.ReadTextElementAsTrimmedString(xmlElement2), NumberFormatInfo.InvariantInfo);
						}
					}
				}
				this.ReadTargets(xmlElement, out renewTarget, out closeTarget);
				return new RequestSecurityToken(this.standardsManager, xmlElement, context, tokenType, requestType, keySize, renewTarget, closeTarget);
			}

			// Token: 0x06007045 RID: 28741 RVA: 0x001A0968 File Offset: 0x0019EB68
			private XmlBuffer GetIssuedTokenBuffer(XmlBuffer rstrBuffer)
			{
				XmlBuffer xmlBuffer = null;
				using (XmlDictionaryReader reader = rstrBuffer.GetReader(0))
				{
					reader.ReadFullStartElement();
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement(this.DriverDictionary.RequestedSecurityToken, this.DriverDictionary.Namespace))
						{
							reader.ReadStartElement();
							reader.MoveToContent();
							xmlBuffer = new XmlBuffer(int.MaxValue);
							using (XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(reader.Quotas))
							{
								xmlDictionaryWriter.WriteNode(reader, false);
								xmlBuffer.CloseSection();
								xmlBuffer.Close();
							}
							reader.ReadEndElement();
							break;
						}
						reader.Skip();
					}
				}
				return xmlBuffer;
			}

			// Token: 0x06007046 RID: 28742 RVA: 0x001A0A2C File Offset: 0x0019EC2C
			public override RequestSecurityTokenResponse CreateRequestSecurityTokenResponse(XmlReader xmlReader)
			{
				if (xmlReader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlReader");
				}
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(xmlReader);
				if (!xmlDictionaryReader.IsStartElement(this.DriverDictionary.RequestSecurityTokenResponse, this.DriverDictionary.Namespace))
				{
					XmlHelper.OnRequiredElementMissing(this.DriverDictionary.RequestSecurityTokenResponse.Value, this.DriverDictionary.Namespace.Value);
				}
				XmlBuffer xmlBuffer = new XmlBuffer(int.MaxValue);
				using (XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(xmlDictionaryReader.Quotas))
				{
					xmlDictionaryWriter.WriteNode(xmlDictionaryReader, false);
					xmlBuffer.CloseSection();
					xmlBuffer.Close();
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement;
				using (XmlReader reader = xmlBuffer.GetReader(0))
				{
					xmlElement = (xmlDocument.ReadNode(reader) as XmlElement);
				}
				XmlBuffer issuedTokenBuffer = this.GetIssuedTokenBuffer(xmlBuffer);
				string context = null;
				string tokenType = null;
				int keySize = 0;
				SecurityKeyIdentifierClause requestedAttachedReference = null;
				SecurityKeyIdentifierClause requestedUnattachedReference = null;
				bool computeKey = false;
				DateTime validFrom = DateTime.UtcNow;
				DateTime validTo = SecurityUtils.MaxUtcDateTime;
				for (int i = 0; i < xmlElement.Attributes.Count; i++)
				{
					XmlAttribute xmlAttribute = xmlElement.Attributes[i];
					if (xmlAttribute.LocalName == this.DriverDictionary.Context.Value)
					{
						context = xmlAttribute.Value;
					}
				}
				for (int j = 0; j < xmlElement.ChildNodes.Count; j++)
				{
					XmlElement xmlElement2 = xmlElement.ChildNodes[j] as XmlElement;
					if (xmlElement2 != null)
					{
						if (xmlElement2.LocalName == this.DriverDictionary.TokenType.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							tokenType = XmlHelper.ReadTextElementAsTrimmedString(xmlElement2);
						}
						else if (xmlElement2.LocalName == this.DriverDictionary.KeySize.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							keySize = int.Parse(XmlHelper.ReadTextElementAsTrimmedString(xmlElement2), NumberFormatInfo.InvariantInfo);
						}
						else if (xmlElement2.LocalName == this.DriverDictionary.RequestedProofToken.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							XmlElement childElement = XmlHelper.GetChildElement(xmlElement2);
							if (childElement.LocalName == this.DriverDictionary.ComputedKey.Value && childElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
							{
								string text = XmlHelper.ReadTextElementAsTrimmedString(childElement);
								if (text != this.DriverDictionary.Psha1ComputedKeyUri.Value)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("UnknownComputedKeyAlgorithm", new object[]
									{
										text
									})));
								}
								computeKey = true;
							}
						}
						else if (xmlElement2.LocalName == this.DriverDictionary.Lifetime.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							XmlElement childElement2 = XmlHelper.GetChildElement(xmlElement2, "Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
							if (childElement2 != null)
							{
								validFrom = DateTime.ParseExact(XmlHelper.ReadTextElementAsTrimmedString(childElement2), WSUtilitySpecificationVersion.AcceptedDateTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
							}
							XmlElement childElement3 = XmlHelper.GetChildElement(xmlElement2, "Expires", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
							if (childElement3 != null)
							{
								validTo = DateTime.ParseExact(XmlHelper.ReadTextElementAsTrimmedString(childElement3), WSUtilitySpecificationVersion.AcceptedDateTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None).ToUniversalTime();
							}
						}
					}
				}
				bool isRequestedTokenClosed = this.ReadRequestedTokenClosed(xmlElement);
				this.ReadReferences(xmlElement, out requestedAttachedReference, out requestedUnattachedReference);
				return new RequestSecurityTokenResponse(this.standardsManager, xmlElement, context, tokenType, keySize, requestedAttachedReference, requestedUnattachedReference, computeKey, validFrom, validTo, isRequestedTokenClosed, issuedTokenBuffer);
			}

			// Token: 0x06007047 RID: 28743 RVA: 0x001A0E30 File Offset: 0x0019F030
			public override RequestSecurityTokenResponseCollection CreateRequestSecurityTokenResponseCollection(XmlReader xmlReader)
			{
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(xmlReader);
				List<RequestSecurityTokenResponse> list = new List<RequestSecurityTokenResponse>(2);
				string name = xmlDictionaryReader.Name;
				xmlDictionaryReader.ReadStartElement(this.DriverDictionary.RequestSecurityTokenResponseCollection, this.DriverDictionary.Namespace);
				while (xmlDictionaryReader.IsStartElement(this.DriverDictionary.RequestSecurityTokenResponse.Value, this.DriverDictionary.Namespace.Value))
				{
					RequestSecurityTokenResponse item = this.CreateRequestSecurityTokenResponse(xmlDictionaryReader);
					list.Add(item);
				}
				xmlDictionaryReader.ReadEndElement();
				if (list.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("NoRequestSecurityTokenResponseElements")));
				}
				return new RequestSecurityTokenResponseCollection(list.AsReadOnly(), this.StandardsManager);
			}

			// Token: 0x06007048 RID: 28744 RVA: 0x001A0EE0 File Offset: 0x0019F0E0
			private XmlElement GetAppliesToElement(XmlElement rootElement)
			{
				if (rootElement == null)
				{
					return null;
				}
				for (int i = 0; i < rootElement.ChildNodes.Count; i++)
				{
					XmlElement xmlElement = rootElement.ChildNodes[i] as XmlElement;
					if (xmlElement != null && xmlElement.LocalName == this.DriverDictionary.AppliesTo.Value && xmlElement.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/policy")
					{
						return xmlElement;
					}
				}
				return null;
			}

			// Token: 0x06007049 RID: 28745 RVA: 0x001A0F50 File Offset: 0x0019F150
			private T GetAppliesTo<T>(XmlElement rootXml, XmlObjectSerializer serializer)
			{
				XmlElement appliesToElement = this.GetAppliesToElement(rootXml);
				if (appliesToElement != null)
				{
					using (XmlReader xmlReader = new XmlNodeReader(appliesToElement))
					{
						xmlReader.ReadStartElement();
						lock (serializer)
						{
							return (T)((object)serializer.ReadObject(xmlReader));
						}
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoAppliesToPresent")));
			}

			// Token: 0x0600704A RID: 28746 RVA: 0x001A0FE0 File Offset: 0x0019F1E0
			public override T GetAppliesTo<T>(RequestSecurityToken rst, XmlObjectSerializer serializer)
			{
				if (rst == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
				}
				return this.GetAppliesTo<T>(rst.RequestSecurityTokenXml, serializer);
			}

			// Token: 0x0600704B RID: 28747 RVA: 0x001A1002 File Offset: 0x0019F202
			public override T GetAppliesTo<T>(RequestSecurityTokenResponse rstr, XmlObjectSerializer serializer)
			{
				if (rstr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
				}
				return this.GetAppliesTo<T>(rstr.RequestSecurityTokenResponseXml, serializer);
			}

			// Token: 0x0600704C RID: 28748 RVA: 0x001A1024 File Offset: 0x0019F224
			public override bool IsAppliesTo(string localName, string namespaceUri)
			{
				return localName == this.DriverDictionary.AppliesTo.Value && namespaceUri == "http://schemas.xmlsoap.org/ws/2004/09/policy";
			}

			// Token: 0x0600704D RID: 28749 RVA: 0x001A104C File Offset: 0x0019F24C
			private void GetAppliesToQName(XmlElement rootElement, out string localName, out string namespaceUri)
			{
				string text;
				namespaceUri = (text = null);
				localName = text;
				XmlElement appliesToElement = this.GetAppliesToElement(rootElement);
				if (appliesToElement != null)
				{
					using (XmlReader xmlReader = new XmlNodeReader(appliesToElement))
					{
						xmlReader.ReadStartElement();
						xmlReader.MoveToContent();
						localName = xmlReader.LocalName;
						namespaceUri = xmlReader.NamespaceURI;
					}
				}
			}

			// Token: 0x0600704E RID: 28750 RVA: 0x001A10AC File Offset: 0x0019F2AC
			public override void GetAppliesToQName(RequestSecurityToken rst, out string localName, out string namespaceUri)
			{
				if (rst == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
				}
				this.GetAppliesToQName(rst.RequestSecurityTokenXml, out localName, out namespaceUri);
			}

			// Token: 0x0600704F RID: 28751 RVA: 0x001A10CF File Offset: 0x0019F2CF
			public override void GetAppliesToQName(RequestSecurityTokenResponse rstr, out string localName, out string namespaceUri)
			{
				if (rstr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
				}
				this.GetAppliesToQName(rstr.RequestSecurityTokenResponseXml, out localName, out namespaceUri);
			}

			// Token: 0x06007050 RID: 28752 RVA: 0x001A10F4 File Offset: 0x0019F2F4
			public override byte[] GetAuthenticator(RequestSecurityTokenResponse rstr)
			{
				if (rstr != null && rstr.RequestSecurityTokenResponseXml != null && rstr.RequestSecurityTokenResponseXml.ChildNodes != null)
				{
					for (int i = 0; i < rstr.RequestSecurityTokenResponseXml.ChildNodes.Count; i++)
					{
						XmlElement xmlElement = rstr.RequestSecurityTokenResponseXml.ChildNodes[i] as XmlElement;
						if (xmlElement != null && xmlElement.LocalName == this.DriverDictionary.Authenticator.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							XmlElement childElement = XmlHelper.GetChildElement(xmlElement);
							if (childElement.LocalName == this.DriverDictionary.CombinedHash.Value && childElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
							{
								string s = XmlHelper.ReadTextElementAsTrimmedString(childElement);
								return Convert.FromBase64String(s);
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06007051 RID: 28753 RVA: 0x001A11EA File Offset: 0x0019F3EA
			public override BinaryNegotiation GetBinaryNegotiation(RequestSecurityTokenResponse rstr)
			{
				if (rstr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
				}
				return this.GetBinaryNegotiation(rstr.RequestSecurityTokenResponseXml);
			}

			// Token: 0x06007052 RID: 28754 RVA: 0x001A120B File Offset: 0x0019F40B
			public override BinaryNegotiation GetBinaryNegotiation(RequestSecurityToken rst)
			{
				if (rst == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
				}
				return this.GetBinaryNegotiation(rst.RequestSecurityTokenXml);
			}

			// Token: 0x06007053 RID: 28755 RVA: 0x001A122C File Offset: 0x0019F42C
			private BinaryNegotiation GetBinaryNegotiation(XmlElement rootElement)
			{
				if (rootElement == null)
				{
					return null;
				}
				for (int i = 0; i < rootElement.ChildNodes.Count; i++)
				{
					XmlElement xmlElement = rootElement.ChildNodes[i] as XmlElement;
					if (xmlElement != null && xmlElement.LocalName == this.DriverDictionary.BinaryExchange.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
					{
						return WSTrust.Driver.ReadBinaryNegotiation(xmlElement);
					}
				}
				return null;
			}

			// Token: 0x06007054 RID: 28756 RVA: 0x001A12AB File Offset: 0x0019F4AB
			public override SecurityToken GetEntropy(RequestSecurityToken rst, SecurityTokenResolver resolver)
			{
				if (rst == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
				}
				return this.GetEntropy(rst.RequestSecurityTokenXml, resolver);
			}

			// Token: 0x06007055 RID: 28757 RVA: 0x001A12CD File Offset: 0x0019F4CD
			public override SecurityToken GetEntropy(RequestSecurityTokenResponse rstr, SecurityTokenResolver resolver)
			{
				if (rstr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
				}
				return this.GetEntropy(rstr.RequestSecurityTokenResponseXml, resolver);
			}

			// Token: 0x06007056 RID: 28758 RVA: 0x001A12F0 File Offset: 0x0019F4F0
			private SecurityToken GetEntropy(XmlElement rootElement, SecurityTokenResolver resolver)
			{
				if (rootElement == null || rootElement.ChildNodes == null)
				{
					return null;
				}
				for (int i = 0; i < rootElement.ChildNodes.Count; i++)
				{
					XmlElement xmlElement = rootElement.ChildNodes[i] as XmlElement;
					if (xmlElement != null && xmlElement.LocalName == this.DriverDictionary.Entropy.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
					{
						XmlElement childElement = XmlHelper.GetChildElement(xmlElement);
						string attribute = xmlElement.GetAttribute("ValueType");
						if (attribute.Length == 0)
						{
						}
						return this.standardsManager.SecurityTokenSerializer.ReadToken(new XmlNodeReader(childElement), resolver);
					}
				}
				return null;
			}

			// Token: 0x06007057 RID: 28759 RVA: 0x001A13AC File Offset: 0x0019F5AC
			private void GetIssuedAndProofXml(RequestSecurityTokenResponse rstr, out XmlElement issuedTokenXml, out XmlElement proofTokenXml)
			{
				issuedTokenXml = null;
				proofTokenXml = null;
				if (rstr.RequestSecurityTokenResponseXml != null && rstr.RequestSecurityTokenResponseXml.ChildNodes != null)
				{
					for (int i = 0; i < rstr.RequestSecurityTokenResponseXml.ChildNodes.Count; i++)
					{
						XmlElement xmlElement = rstr.RequestSecurityTokenResponseXml.ChildNodes[i] as XmlElement;
						if (xmlElement != null)
						{
							if (xmlElement.LocalName == this.DriverDictionary.RequestedSecurityToken.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
							{
								if (issuedTokenXml != null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("RstrHasMultipleIssuedTokens")));
								}
								issuedTokenXml = XmlHelper.GetChildElement(xmlElement);
							}
							else if (xmlElement.LocalName == this.DriverDictionary.RequestedProofToken.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
							{
								if (proofTokenXml != null)
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("RstrHasMultipleProofTokens")));
								}
								proofTokenXml = XmlHelper.GetChildElement(xmlElement);
							}
						}
					}
				}
			}

			// Token: 0x06007058 RID: 28760 RVA: 0x001A14DC File Offset: 0x0019F6DC
			public override GenericXmlSecurityToken GetIssuedToken(RequestSecurityTokenResponse rstr, SecurityTokenResolver resolver, IList<SecurityTokenAuthenticator> allowedAuthenticators, SecurityKeyEntropyMode keyEntropyMode, byte[] requestorEntropy, string expectedTokenType, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, int defaultKeySize, bool isBearerKeyType)
			{
				SecurityKeyEntropyModeHelper.Validate(keyEntropyMode);
				if (defaultKeySize < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("defaultKeySize", SR.GetString("ValueMustBeNonNegative")));
				}
				if (rstr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
				}
				if (rstr.TokenType != null)
				{
					if (expectedTokenType != null && expectedTokenType != rstr.TokenType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BadIssuedTokenType", new object[]
						{
							rstr.TokenType,
							expectedTokenType
						})));
					}
					string tokenType = rstr.TokenType;
				}
				DateTime validFrom = rstr.ValidFrom;
				DateTime validTo = rstr.ValidTo;
				XmlElement xmlElement;
				XmlElement xmlElement2;
				this.GetIssuedAndProofXml(rstr, out xmlElement, out xmlElement2);
				if (xmlElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoLicenseXml")));
				}
				if (!isBearerKeyType)
				{
					SecurityToken entropy = this.GetEntropy(rstr, resolver);
					SecurityToken proofToken;
					if (keyEntropyMode == SecurityKeyEntropyMode.ClientEntropy)
					{
						if (requestorEntropy == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeRequiresRequestorEntropy", new object[]
							{
								keyEntropyMode
							})));
						}
						if (xmlElement2 != null || entropy != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeCannotHaveProofTokenOrIssuerEntropy", new object[]
							{
								keyEntropyMode
							})));
						}
						proofToken = new BinarySecretSecurityToken(requestorEntropy);
					}
					else if (keyEntropyMode == SecurityKeyEntropyMode.ServerEntropy)
					{
						if (requestorEntropy != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeCannotHaveRequestorEntropy", new object[]
							{
								keyEntropyMode
							})));
						}
						if (rstr.ComputeKey || entropy != null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeCannotHaveComputedKey", new object[]
							{
								keyEntropyMode
							})));
						}
						if (xmlElement2 == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeRequiresProofToken", new object[]
							{
								keyEntropyMode
							})));
						}
						string attribute = xmlElement2.GetAttribute("ValueType");
						if (attribute.Length == 0)
						{
						}
						proofToken = this.standardsManager.SecurityTokenSerializer.ReadToken(new XmlNodeReader(xmlElement2), resolver);
					}
					else
					{
						if (!rstr.ComputeKey)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeRequiresComputedKey", new object[]
							{
								keyEntropyMode
							})));
						}
						if (entropy == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeRequiresIssuerEntropy", new object[]
							{
								keyEntropyMode
							})));
						}
						if (requestorEntropy == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EntropyModeRequiresRequestorEntropy", new object[]
							{
								keyEntropyMode
							})));
						}
						if (rstr.KeySize == 0 && defaultKeySize == 0)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("RstrKeySizeNotProvided")));
						}
						int keySizeInBits = (rstr.KeySize != 0) ? rstr.KeySize : defaultKeySize;
						byte[] issuerEntropy;
						if (entropy is BinarySecretSecurityToken)
						{
							issuerEntropy = ((BinarySecretSecurityToken)entropy).GetKeyBytes();
						}
						else
						{
							if (!(entropy is WrappedKeySecurityToken))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedIssuerEntropyType")));
							}
							issuerEntropy = ((WrappedKeySecurityToken)entropy).GetWrappedKey();
						}
						byte[] key = RequestSecurityTokenResponse.ComputeCombinedKey(requestorEntropy, issuerEntropy, keySizeInBits);
						proofToken = new BinarySecretSecurityToken(key);
					}
					SecurityKeyIdentifierClause requestedAttachedReference = rstr.RequestedAttachedReference;
					SecurityKeyIdentifierClause requestedUnattachedReference = rstr.RequestedUnattachedReference;
					return new BufferedGenericXmlSecurityToken(xmlElement, proofToken, validFrom, validTo, requestedAttachedReference, requestedUnattachedReference, authorizationPolicies, rstr.IssuedTokenBuffer);
				}
				if (xmlElement2 != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BearerKeyTypeCannotHaveProofKey")));
				}
				return new GenericXmlSecurityToken(xmlElement, null, validFrom, validTo, rstr.RequestedAttachedReference, rstr.RequestedUnattachedReference, authorizationPolicies);
			}

			// Token: 0x06007059 RID: 28761 RVA: 0x001A188C File Offset: 0x0019FA8C
			public override GenericXmlSecurityToken GetIssuedToken(RequestSecurityTokenResponse rstr, string expectedTokenType, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies, RSA clientKey)
			{
				if (rstr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("rstr"));
				}
				if (rstr.TokenType != null)
				{
					if (expectedTokenType != null && expectedTokenType != rstr.TokenType)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BadIssuedTokenType", new object[]
						{
							rstr.TokenType,
							expectedTokenType
						})));
					}
					string tokenType = rstr.TokenType;
				}
				DateTime validFrom = rstr.ValidFrom;
				DateTime validTo = rstr.ValidTo;
				XmlElement xmlElement;
				XmlElement xmlElement2;
				this.GetIssuedAndProofXml(rstr, out xmlElement, out xmlElement2);
				if (xmlElement == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoLicenseXml")));
				}
				if (xmlElement2 != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ProofTokenXmlUnexpectedInRstr")));
				}
				SecurityKeyIdentifierClause requestedAttachedReference = rstr.RequestedAttachedReference;
				SecurityKeyIdentifierClause requestedUnattachedReference = rstr.RequestedUnattachedReference;
				SecurityToken proofToken = new RsaSecurityToken(clientKey);
				return new BufferedGenericXmlSecurityToken(xmlElement, proofToken, validFrom, validTo, requestedAttachedReference, requestedUnattachedReference, authorizationPolicies, rstr.IssuedTokenBuffer);
			}

			// Token: 0x0600705A RID: 28762 RVA: 0x001A1985 File Offset: 0x0019FB85
			public override bool IsAtRequestSecurityTokenResponse(XmlReader reader)
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				return reader.IsStartElement(this.DriverDictionary.RequestSecurityTokenResponse.Value, this.DriverDictionary.Namespace.Value);
			}

			// Token: 0x0600705B RID: 28763 RVA: 0x001A19C0 File Offset: 0x0019FBC0
			public override bool IsAtRequestSecurityTokenResponseCollection(XmlReader reader)
			{
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				return reader.IsStartElement(this.DriverDictionary.RequestSecurityTokenResponseCollection.Value, this.DriverDictionary.Namespace.Value);
			}

			// Token: 0x0600705C RID: 28764 RVA: 0x001A19FB File Offset: 0x0019FBFB
			public override bool IsRequestedSecurityTokenElement(string name, string nameSpace)
			{
				return name == this.DriverDictionary.RequestedSecurityToken.Value && nameSpace == this.DriverDictionary.Namespace.Value;
			}

			// Token: 0x0600705D RID: 28765 RVA: 0x001A1A2D File Offset: 0x0019FC2D
			public override bool IsRequestedProofTokenElement(string name, string nameSpace)
			{
				return name == this.DriverDictionary.RequestedProofToken.Value && nameSpace == this.DriverDictionary.Namespace.Value;
			}

			// Token: 0x0600705E RID: 28766 RVA: 0x001A1A60 File Offset: 0x0019FC60
			public static BinaryNegotiation ReadBinaryNegotiation(XmlElement elem)
			{
				if (elem == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elem");
				}
				string text = null;
				string text2 = null;
				if (elem.Attributes != null)
				{
					for (int i = 0; i < elem.Attributes.Count; i++)
					{
						XmlAttribute xmlAttribute = elem.Attributes[i];
						if (xmlAttribute.LocalName == "EncodingType" && xmlAttribute.NamespaceURI.Length == 0)
						{
							text = xmlAttribute.Value;
							if (text != WSTrust.Driver.base64Uri && text != WSTrust.Driver.hexBinaryUri)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnsupportedBinaryEncoding", new object[]
								{
									text
								})));
							}
						}
						else if (xmlAttribute.LocalName == "ValueType" && xmlAttribute.NamespaceURI.Length == 0)
						{
							text2 = xmlAttribute.Value;
						}
					}
				}
				if (text == null)
				{
					XmlHelper.OnRequiredAttributeMissing("EncodingType", elem.Name);
				}
				if (text2 == null)
				{
					XmlHelper.OnRequiredAttributeMissing("ValueType", elem.Name);
				}
				string text3 = XmlHelper.ReadTextElementAsTrimmedString(elem);
				byte[] negotiationData;
				if (text == WSTrust.Driver.base64Uri)
				{
					negotiationData = Convert.FromBase64String(text3);
				}
				else
				{
					negotiationData = SoapHexBinary.Parse(text3).Value;
				}
				return new BinaryNegotiation(text2, negotiationData);
			}

			// Token: 0x0600705F RID: 28767 RVA: 0x001A1BA8 File Offset: 0x0019FDA8
			protected virtual void ReadReferences(XmlElement rstrXml, out SecurityKeyIdentifierClause requestedAttachedReference, out SecurityKeyIdentifierClause requestedUnattachedReference)
			{
				XmlElement xmlElement = null;
				requestedAttachedReference = null;
				requestedUnattachedReference = null;
				for (int i = 0; i < rstrXml.ChildNodes.Count; i++)
				{
					XmlElement xmlElement2 = rstrXml.ChildNodes[i] as XmlElement;
					if (xmlElement2 != null)
					{
						if (xmlElement2.LocalName == this.DriverDictionary.RequestedSecurityToken.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							xmlElement = XmlHelper.GetChildElement(xmlElement2);
						}
						else if (xmlElement2.LocalName == this.DriverDictionary.RequestedTokenReference.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							requestedUnattachedReference = this.GetKeyIdentifierXmlReferenceClause(XmlHelper.GetChildElement(xmlElement2));
						}
					}
				}
				if (xmlElement != null)
				{
					requestedAttachedReference = this.standardsManager.CreateKeyIdentifierClauseFromTokenXml(xmlElement, SecurityTokenReferenceStyle.Internal);
					if (requestedUnattachedReference == null)
					{
						try
						{
							requestedUnattachedReference = this.standardsManager.CreateKeyIdentifierClauseFromTokenXml(xmlElement, SecurityTokenReferenceStyle.External);
						}
						catch (XmlException)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("TrustDriverIsUnableToCreatedNecessaryAttachedOrUnattachedReferences", new object[]
							{
								xmlElement.ToString()
							})));
						}
					}
				}
			}

			// Token: 0x06007060 RID: 28768 RVA: 0x001A1CE0 File Offset: 0x0019FEE0
			internal bool TryReadKeyIdentifierClause(XmlNodeReader reader, out SecurityKeyIdentifierClause keyIdentifierClause)
			{
				keyIdentifierClause = null;
				try
				{
					keyIdentifierClause = this.standardsManager.SecurityTokenSerializer.ReadKeyIdentifierClause(reader);
				}
				catch (XmlException exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					keyIdentifierClause = null;
					return false;
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					keyIdentifierClause = null;
					return false;
				}
				return true;
			}

			// Token: 0x06007061 RID: 28769 RVA: 0x001A1D48 File Offset: 0x0019FF48
			internal SecurityKeyIdentifierClause CreateGenericXmlSecurityKeyIdentifierClause(XmlNodeReader reader, XmlElement keyIdentifierReferenceXmlElement)
			{
				XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
				string attribute = xmlDictionaryReader.GetAttribute(XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace);
				SecurityKeyIdentifierClause securityKeyIdentifierClause = new GenericXmlSecurityKeyIdentifierClause(keyIdentifierReferenceXmlElement);
				if (!string.IsNullOrEmpty(attribute))
				{
					securityKeyIdentifierClause.Id = attribute;
				}
				return securityKeyIdentifierClause;
			}

			// Token: 0x06007062 RID: 28770 RVA: 0x001A1D90 File Offset: 0x0019FF90
			internal SecurityKeyIdentifierClause GetKeyIdentifierXmlReferenceClause(XmlElement keyIdentifierReferenceXmlElement)
			{
				SecurityKeyIdentifierClause result = null;
				XmlNodeReader reader = new XmlNodeReader(keyIdentifierReferenceXmlElement);
				if (!this.TryReadKeyIdentifierClause(reader, out result))
				{
					result = this.CreateGenericXmlSecurityKeyIdentifierClause(new XmlNodeReader(keyIdentifierReferenceXmlElement), keyIdentifierReferenceXmlElement);
				}
				return result;
			}

			// Token: 0x06007063 RID: 28771 RVA: 0x001A1DC0 File Offset: 0x0019FFC0
			protected virtual bool ReadRequestedTokenClosed(XmlElement rstrXml)
			{
				return false;
			}

			// Token: 0x06007064 RID: 28772 RVA: 0x001A1DC3 File Offset: 0x0019FFC3
			protected virtual void ReadTargets(XmlElement rstXml, out SecurityKeyIdentifierClause renewTarget, out SecurityKeyIdentifierClause closeTarget)
			{
				renewTarget = null;
				closeTarget = null;
			}

			// Token: 0x06007065 RID: 28773 RVA: 0x001A1DCC File Offset: 0x0019FFCC
			public override void OnRSTRorRSTRCMissingException()
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("ExpectedOneOfTwoElementsFromNamespace", new object[]
				{
					this.DriverDictionary.RequestSecurityTokenResponse,
					this.DriverDictionary.RequestSecurityTokenResponseCollection,
					this.DriverDictionary.Namespace
				})));
			}

			// Token: 0x06007066 RID: 28774 RVA: 0x001A1E24 File Offset: 0x001A0024
			private void WriteAppliesTo(object appliesTo, Type appliesToType, XmlObjectSerializer serializer, XmlWriter xmlWriter)
			{
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter);
				xmlDictionaryWriter.WriteStartElement("wsp", this.DriverDictionary.AppliesTo.Value, "http://schemas.xmlsoap.org/ws/2004/09/policy");
				lock (serializer)
				{
					serializer.WriteObject(xmlDictionaryWriter, appliesTo);
				}
				xmlDictionaryWriter.WriteEndElement();
			}

			// Token: 0x06007067 RID: 28775 RVA: 0x001A1E90 File Offset: 0x001A0090
			public void WriteBinaryNegotiation(BinaryNegotiation negotiation, XmlWriter xmlWriter)
			{
				if (negotiation == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("negotiation");
				}
				XmlDictionaryWriter writer = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter);
				negotiation.WriteTo(writer, this.DriverDictionary.Prefix.Value, this.DriverDictionary.BinaryExchange, this.DriverDictionary.Namespace, XD.SecurityJan2004Dictionary.ValueType, null);
			}

			// Token: 0x06007068 RID: 28776 RVA: 0x001A1EF0 File Offset: 0x001A00F0
			public override void WriteRequestSecurityToken(RequestSecurityToken rst, XmlWriter xmlWriter)
			{
				if (rst == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
				}
				if (xmlWriter == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlWriter");
				}
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter);
				if (rst.IsReceiver)
				{
					rst.WriteTo(xmlDictionaryWriter);
					return;
				}
				xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestSecurityToken, this.DriverDictionary.Namespace);
				XmlHelper.AddNamespaceDeclaration(xmlDictionaryWriter, this.DriverDictionary.Prefix.Value, this.DriverDictionary.Namespace);
				if (rst.Context != null)
				{
					xmlDictionaryWriter.WriteAttributeString(this.DriverDictionary.Context, null, rst.Context);
				}
				rst.OnWriteCustomAttributes(xmlDictionaryWriter);
				if (rst.TokenType != null)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.TokenType, this.DriverDictionary.Namespace);
					xmlDictionaryWriter.WriteString(rst.TokenType);
					xmlDictionaryWriter.WriteEndElement();
				}
				if (rst.RequestType != null)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestType, this.DriverDictionary.Namespace);
					xmlDictionaryWriter.WriteString(rst.RequestType);
					xmlDictionaryWriter.WriteEndElement();
				}
				if (rst.AppliesTo != null)
				{
					this.WriteAppliesTo(rst.AppliesTo, rst.AppliesToType, rst.AppliesToSerializer, xmlDictionaryWriter);
				}
				SecurityToken requestorEntropy = rst.GetRequestorEntropy();
				if (requestorEntropy != null)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.Entropy, this.DriverDictionary.Namespace);
					this.standardsManager.SecurityTokenSerializer.WriteToken(xmlDictionaryWriter, requestorEntropy);
					xmlDictionaryWriter.WriteEndElement();
				}
				if (rst.KeySize != 0)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.KeySize, this.DriverDictionary.Namespace);
					xmlDictionaryWriter.WriteValue(rst.KeySize);
					xmlDictionaryWriter.WriteEndElement();
				}
				BinaryNegotiation binaryNegotiation = rst.GetBinaryNegotiation();
				if (binaryNegotiation != null)
				{
					this.WriteBinaryNegotiation(binaryNegotiation, xmlDictionaryWriter);
				}
				this.WriteTargets(rst, xmlDictionaryWriter);
				if (rst.RequestProperties != null)
				{
					foreach (XmlElement xmlElement in rst.RequestProperties)
					{
						xmlElement.WriteTo(xmlDictionaryWriter);
					}
				}
				rst.OnWriteCustomElements(xmlDictionaryWriter);
				xmlDictionaryWriter.WriteEndElement();
			}

			// Token: 0x06007069 RID: 28777 RVA: 0x001A2160 File Offset: 0x001A0360
			protected virtual void WriteTargets(RequestSecurityToken rst, XmlDictionaryWriter writer)
			{
			}

			// Token: 0x0600706A RID: 28778 RVA: 0x001A2164 File Offset: 0x001A0364
			protected virtual void WriteReferences(RequestSecurityTokenResponse rstr, XmlDictionaryWriter writer)
			{
				if (rstr.RequestedUnattachedReference != null)
				{
					writer.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestedTokenReference, this.DriverDictionary.Namespace);
					this.standardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer, rstr.RequestedUnattachedReference);
					writer.WriteEndElement();
				}
			}

			// Token: 0x0600706B RID: 28779 RVA: 0x001A21C2 File Offset: 0x001A03C2
			protected virtual void WriteRequestedTokenClosed(RequestSecurityTokenResponse rstr, XmlDictionaryWriter writer)
			{
			}

			// Token: 0x0600706C RID: 28780 RVA: 0x001A21C4 File Offset: 0x001A03C4
			public override void WriteRequestSecurityTokenResponse(RequestSecurityTokenResponse rstr, XmlWriter xmlWriter)
			{
				if (rstr == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstr");
				}
				if (xmlWriter == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("xmlWriter");
				}
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter);
				if (rstr.IsReceiver)
				{
					rstr.WriteTo(xmlDictionaryWriter);
					return;
				}
				xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestSecurityTokenResponse, this.DriverDictionary.Namespace);
				if (rstr.Context != null)
				{
					xmlDictionaryWriter.WriteAttributeString(this.DriverDictionary.Context, null, rstr.Context);
				}
				XmlHelper.AddNamespaceDeclaration(xmlDictionaryWriter, "u", XD.UtilityDictionary.Namespace);
				rstr.OnWriteCustomAttributes(xmlDictionaryWriter);
				if (rstr.TokenType != null)
				{
					xmlDictionaryWriter.WriteElementString(this.DriverDictionary.Prefix.Value, this.DriverDictionary.TokenType, this.DriverDictionary.Namespace, rstr.TokenType);
				}
				if (rstr.RequestedSecurityToken != null)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestedSecurityToken, this.DriverDictionary.Namespace);
					this.standardsManager.SecurityTokenSerializer.WriteToken(xmlDictionaryWriter, rstr.RequestedSecurityToken);
					xmlDictionaryWriter.WriteEndElement();
				}
				if (rstr.AppliesTo != null)
				{
					this.WriteAppliesTo(rstr.AppliesTo, rstr.AppliesToType, rstr.AppliesToSerializer, xmlDictionaryWriter);
				}
				this.WriteReferences(rstr, xmlDictionaryWriter);
				if (rstr.ComputeKey || rstr.RequestedProofToken != null)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestedProofToken, this.DriverDictionary.Namespace);
					if (rstr.ComputeKey)
					{
						xmlDictionaryWriter.WriteElementString(this.DriverDictionary.Prefix.Value, this.DriverDictionary.ComputedKey, this.DriverDictionary.Namespace, this.DriverDictionary.Psha1ComputedKeyUri.Value);
					}
					else
					{
						this.standardsManager.SecurityTokenSerializer.WriteToken(xmlDictionaryWriter, rstr.RequestedProofToken);
					}
					xmlDictionaryWriter.WriteEndElement();
				}
				SecurityToken issuerEntropy = rstr.GetIssuerEntropy();
				if (issuerEntropy != null)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.Entropy, this.DriverDictionary.Namespace);
					this.standardsManager.SecurityTokenSerializer.WriteToken(xmlDictionaryWriter, issuerEntropy);
					xmlDictionaryWriter.WriteEndElement();
				}
				if (rstr.IsLifetimeSet || rstr.RequestedSecurityToken != null)
				{
					DateTime dateTime = SecurityUtils.MinUtcDateTime;
					DateTime dateTime2 = SecurityUtils.MaxUtcDateTime;
					if (rstr.IsLifetimeSet)
					{
						dateTime = rstr.ValidFrom.ToUniversalTime();
						dateTime2 = rstr.ValidTo.ToUniversalTime();
					}
					else if (rstr.RequestedSecurityToken != null)
					{
						dateTime = rstr.RequestedSecurityToken.ValidFrom.ToUniversalTime();
						dateTime2 = rstr.RequestedSecurityToken.ValidTo.ToUniversalTime();
					}
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.Lifetime, this.DriverDictionary.Namespace);
					xmlDictionaryWriter.WriteStartElement(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.CreatedElement, XD.UtilityDictionary.Namespace);
					xmlDictionaryWriter.WriteString(dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture.DateTimeFormat));
					xmlDictionaryWriter.WriteEndElement();
					xmlDictionaryWriter.WriteStartElement(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.ExpiresElement, XD.UtilityDictionary.Namespace);
					xmlDictionaryWriter.WriteString(dateTime2.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture.DateTimeFormat));
					xmlDictionaryWriter.WriteEndElement();
					xmlDictionaryWriter.WriteEndElement();
				}
				byte[] authenticator = rstr.GetAuthenticator();
				if (authenticator != null)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.Authenticator, this.DriverDictionary.Namespace);
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.CombinedHash, this.DriverDictionary.Namespace);
					xmlDictionaryWriter.WriteBase64(authenticator, 0, authenticator.Length);
					xmlDictionaryWriter.WriteEndElement();
					xmlDictionaryWriter.WriteEndElement();
				}
				if (rstr.KeySize > 0)
				{
					xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.KeySize, this.DriverDictionary.Namespace);
					xmlDictionaryWriter.WriteValue(rstr.KeySize);
					xmlDictionaryWriter.WriteEndElement();
				}
				this.WriteRequestedTokenClosed(rstr, xmlDictionaryWriter);
				BinaryNegotiation binaryNegotiation = rstr.GetBinaryNegotiation();
				if (binaryNegotiation != null)
				{
					this.WriteBinaryNegotiation(binaryNegotiation, xmlDictionaryWriter);
				}
				rstr.OnWriteCustomElements(xmlDictionaryWriter);
				xmlDictionaryWriter.WriteEndElement();
			}

			// Token: 0x0600706D RID: 28781 RVA: 0x001A264C File Offset: 0x001A084C
			public override void WriteRequestSecurityTokenResponseCollection(RequestSecurityTokenResponseCollection rstrCollection, XmlWriter xmlWriter)
			{
				if (rstrCollection == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rstrCollection");
				}
				XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter);
				xmlDictionaryWriter.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestSecurityTokenResponseCollection, this.DriverDictionary.Namespace);
				foreach (RequestSecurityTokenResponse requestSecurityTokenResponse in rstrCollection.RstrCollection)
				{
					requestSecurityTokenResponse.WriteTo(xmlDictionaryWriter);
				}
				xmlDictionaryWriter.WriteEndElement();
			}

			// Token: 0x0600706E RID: 28782 RVA: 0x001A26E8 File Offset: 0x001A08E8
			protected void SetProtectionLevelForFederation(OperationDescriptionCollection operations)
			{
				foreach (OperationDescription operationDescription in operations)
				{
					foreach (MessageDescription messageDescription in operationDescription.Messages)
					{
						if (messageDescription.Body.Parts.Count > 0)
						{
							foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
							{
								messagePartDescription.ProtectionLevel = ProtectionLevel.EncryptAndSign;
							}
						}
						if (OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue))
						{
							messageDescription.Body.ReturnValue.ProtectionLevel = ProtectionLevel.EncryptAndSign;
						}
					}
				}
			}

			// Token: 0x0600706F RID: 28783 RVA: 0x001A27E8 File Offset: 0x001A09E8
			public override bool TryParseKeySizeElement(XmlElement element, out int keySize)
			{
				if (element == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
				}
				if (element.LocalName == this.DriverDictionary.KeySize.Value && element.NamespaceURI == this.DriverDictionary.Namespace.Value)
				{
					keySize = int.Parse(XmlHelper.ReadTextElementAsTrimmedString(element), NumberFormatInfo.InvariantInfo);
					return true;
				}
				keySize = 0;
				return false;
			}

			// Token: 0x06007070 RID: 28784 RVA: 0x001A285C File Offset: 0x001A0A5C
			public override XmlElement CreateKeySizeElement(int keySize)
			{
				if (keySize < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("keySize", SR.GetString("ValueMustBeNonNegative")));
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.KeySize.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(keySize.ToString(CultureInfo.InvariantCulture.NumberFormat)));
				return xmlElement;
			}

			// Token: 0x06007071 RID: 28785 RVA: 0x001A28E8 File Offset: 0x001A0AE8
			public override XmlElement CreateKeyTypeElement(SecurityKeyType keyType)
			{
				if (keyType == SecurityKeyType.SymmetricKey)
				{
					return this.CreateSymmetricKeyTypeElement();
				}
				if (keyType == SecurityKeyType.AsymmetricKey)
				{
					return this.CreatePublicKeyTypeElement();
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToCreateKeyTypeElementForUnknownKeyType", new object[]
				{
					keyType.ToString()
				})));
			}

			// Token: 0x06007072 RID: 28786 RVA: 0x001A2939 File Offset: 0x001A0B39
			public override bool TryParseKeyTypeElement(XmlElement element, out SecurityKeyType keyType)
			{
				if (element == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
				}
				if (this.TryParseSymmetricKeyElement(element))
				{
					keyType = SecurityKeyType.SymmetricKey;
					return true;
				}
				if (this.TryParsePublicKeyElement(element))
				{
					keyType = SecurityKeyType.AsymmetricKey;
					return true;
				}
				keyType = SecurityKeyType.SymmetricKey;
				return false;
			}

			// Token: 0x06007073 RID: 28787 RVA: 0x001A2970 File Offset: 0x001A0B70
			public bool TryParseSymmetricKeyElement(XmlElement element)
			{
				if (element == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
				}
				return element.LocalName == this.DriverDictionary.KeyType.Value && element.NamespaceURI == this.DriverDictionary.Namespace.Value && element.InnerText == this.DriverDictionary.SymmetricKeyType.Value;
			}

			// Token: 0x06007074 RID: 28788 RVA: 0x001A29E8 File Offset: 0x001A0BE8
			private XmlElement CreateSymmetricKeyTypeElement()
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.KeyType.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(this.DriverDictionary.SymmetricKeyType.Value));
				return xmlElement;
			}

			// Token: 0x06007075 RID: 28789 RVA: 0x001A2A50 File Offset: 0x001A0C50
			private bool TryParsePublicKeyElement(XmlElement element)
			{
				if (element == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
				}
				return element.LocalName == this.DriverDictionary.KeyType.Value && element.NamespaceURI == this.DriverDictionary.Namespace.Value && element.InnerText == this.DriverDictionary.PublicKeyType.Value;
			}

			// Token: 0x06007076 RID: 28790 RVA: 0x001A2AC8 File Offset: 0x001A0CC8
			private XmlElement CreatePublicKeyTypeElement()
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.KeyType.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(this.DriverDictionary.PublicKeyType.Value));
				return xmlElement;
			}

			// Token: 0x06007077 RID: 28791 RVA: 0x001A2B30 File Offset: 0x001A0D30
			public override bool TryParseTokenTypeElement(XmlElement element, out string tokenType)
			{
				if (element == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
				}
				if (element.LocalName == this.DriverDictionary.TokenType.Value && element.NamespaceURI == this.DriverDictionary.Namespace.Value)
				{
					tokenType = element.InnerText;
					return true;
				}
				tokenType = null;
				return false;
			}

			// Token: 0x06007078 RID: 28792 RVA: 0x001A2B98 File Offset: 0x001A0D98
			public override XmlElement CreateTokenTypeElement(string tokenTypeUri)
			{
				if (tokenTypeUri == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("tokenTypeUri");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.TokenType.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(tokenTypeUri));
				return xmlElement;
			}

			// Token: 0x06007079 RID: 28793 RVA: 0x001A2C04 File Offset: 0x001A0E04
			public override XmlElement CreateUseKeyElement(SecurityKeyIdentifier keyIdentifier, SecurityStandardsManager standardsManager)
			{
				if (keyIdentifier == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
				}
				if (standardsManager == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("standardsManager");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.UseKey.Value, this.DriverDictionary.Namespace.Value);
				MemoryStream memoryStream = new MemoryStream();
				using (XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(new XmlTextWriter(memoryStream, Encoding.UTF8)))
				{
					standardsManager.SecurityTokenSerializer.WriteKeyIdentifier(xmlDictionaryWriter, keyIdentifier);
					xmlDictionaryWriter.Flush();
					memoryStream.Seek(0L, SeekOrigin.Begin);
					XmlNode newChild;
					using (XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(new XmlTextReader(memoryStream)
					{
						DtdProcessing = DtdProcessing.Prohibit
					}))
					{
						xmlDictionaryReader.MoveToContent();
						newChild = xmlDocument.ReadNode(xmlDictionaryReader);
					}
					xmlElement.AppendChild(newChild);
				}
				return xmlElement;
			}

			// Token: 0x0600707A RID: 28794 RVA: 0x001A2D00 File Offset: 0x001A0F00
			public override XmlElement CreateSignWithElement(string signatureAlgorithm)
			{
				if (signatureAlgorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signatureAlgorithm");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.SignWith.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(signatureAlgorithm));
				return xmlElement;
			}

			// Token: 0x0600707B RID: 28795 RVA: 0x001A2D6C File Offset: 0x001A0F6C
			internal override bool IsSignWithElement(XmlElement element, out string signatureAlgorithm)
			{
				return WSTrust.CheckElement(element, this.DriverDictionary.SignWith.Value, this.DriverDictionary.Namespace.Value, out signatureAlgorithm);
			}

			// Token: 0x0600707C RID: 28796 RVA: 0x001A2D98 File Offset: 0x001A0F98
			public override XmlElement CreateEncryptWithElement(string encryptionAlgorithm)
			{
				if (encryptionAlgorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encryptionAlgorithm");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.EncryptWith.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(encryptionAlgorithm));
				return xmlElement;
			}

			// Token: 0x0600707D RID: 28797 RVA: 0x001A2E04 File Offset: 0x001A1004
			public override XmlElement CreateEncryptionAlgorithmElement(string encryptionAlgorithm)
			{
				if (encryptionAlgorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encryptionAlgorithm");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.EncryptionAlgorithm.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(encryptionAlgorithm));
				return xmlElement;
			}

			// Token: 0x0600707E RID: 28798 RVA: 0x001A2E70 File Offset: 0x001A1070
			internal override bool IsEncryptWithElement(XmlElement element, out string encryptWithAlgorithm)
			{
				return WSTrust.CheckElement(element, this.DriverDictionary.EncryptWith.Value, this.DriverDictionary.Namespace.Value, out encryptWithAlgorithm);
			}

			// Token: 0x0600707F RID: 28799 RVA: 0x001A2E99 File Offset: 0x001A1099
			internal override bool IsEncryptionAlgorithmElement(XmlElement element, out string encryptionAlgorithm)
			{
				return WSTrust.CheckElement(element, this.DriverDictionary.EncryptionAlgorithm.Value, this.DriverDictionary.Namespace.Value, out encryptionAlgorithm);
			}

			// Token: 0x06007080 RID: 28800 RVA: 0x001A2EC4 File Offset: 0x001A10C4
			public override XmlElement CreateComputedKeyAlgorithmElement(string algorithm)
			{
				if (algorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("algorithm");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.ComputedKeyAlgorithm.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(algorithm));
				return xmlElement;
			}

			// Token: 0x06007081 RID: 28801 RVA: 0x001A2F30 File Offset: 0x001A1130
			public override XmlElement CreateCanonicalizationAlgorithmElement(string algorithm)
			{
				if (algorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("algorithm");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.CanonicalizationAlgorithm.Value, this.DriverDictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(algorithm));
				return xmlElement;
			}

			// Token: 0x06007082 RID: 28802 RVA: 0x001A2F9C File Offset: 0x001A119C
			internal override bool IsCanonicalizationAlgorithmElement(XmlElement element, out string canonicalizationAlgorithm)
			{
				return WSTrust.CheckElement(element, this.DriverDictionary.CanonicalizationAlgorithm.Value, this.DriverDictionary.Namespace.Value, out canonicalizationAlgorithm);
			}

			// Token: 0x06007083 RID: 28803 RVA: 0x001A2FC8 File Offset: 0x001A11C8
			public override bool TryParseRequiredClaimsElement(XmlElement element, out Collection<XmlElement> requiredClaims)
			{
				if (element == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
				}
				if (element.LocalName == this.DriverDictionary.Claims.Value && element.NamespaceURI == this.DriverDictionary.Namespace.Value)
				{
					requiredClaims = new Collection<XmlElement>();
					foreach (object obj in element.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode is XmlElement)
						{
							requiredClaims.Add((XmlElement)xmlNode);
						}
					}
					return true;
				}
				requiredClaims = null;
				return false;
			}

			// Token: 0x06007084 RID: 28804 RVA: 0x001A308C File Offset: 0x001A128C
			public override XmlElement CreateRequiredClaimsElement(IEnumerable<XmlElement> claimsList)
			{
				if (claimsList == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("claimsList");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.Claims.Value, this.DriverDictionary.Namespace.Value);
				foreach (XmlElement node in claimsList)
				{
					XmlElement newChild = (XmlElement)xmlDocument.ImportNode(node, true);
					xmlElement.AppendChild(newChild);
				}
				return xmlElement;
			}

			// Token: 0x06007085 RID: 28805 RVA: 0x001A3138 File Offset: 0x001A1338
			internal static void ValidateRequestedKeySize(int keySize, SecurityAlgorithmSuite algorithmSuite)
			{
				if (keySize % 8 == 0 && algorithmSuite.IsSymmetricKeyLengthSupported(keySize))
				{
					return;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidKeyLengthRequested", new object[]
				{
					keySize
				})));
			}

			// Token: 0x06007086 RID: 28806 RVA: 0x001A3174 File Offset: 0x001A1374
			private static void ValidateRequestorEntropy(SecurityToken entropy, SecurityKeyEntropyMode mode)
			{
				if ((mode == SecurityKeyEntropyMode.ClientEntropy || mode == SecurityKeyEntropyMode.CombinedEntropy) && entropy == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("EntropyModeRequiresRequestorEntropy", new object[]
					{
						mode
					})));
				}
				if (mode == SecurityKeyEntropyMode.ServerEntropy && entropy != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("EntropyModeCannotHaveRequestorEntropy", new object[]
					{
						mode
					})));
				}
			}

			// Token: 0x06007087 RID: 28807 RVA: 0x001A31E4 File Offset: 0x001A13E4
			internal static void ProcessRstAndIssueKey(RequestSecurityToken requestSecurityToken, SecurityTokenResolver resolver, SecurityKeyEntropyMode keyEntropyMode, SecurityAlgorithmSuite algorithmSuite, out int issuedKeySize, out byte[] issuerEntropy, out byte[] proofKey, out SecurityToken proofToken)
			{
				SecurityToken requestorEntropy = requestSecurityToken.GetRequestorEntropy(resolver);
				WSTrust.Driver.ValidateRequestorEntropy(requestorEntropy, keyEntropyMode);
				byte[] array;
				if (requestorEntropy != null)
				{
					if (requestorEntropy is BinarySecretSecurityToken)
					{
						BinarySecretSecurityToken binarySecretSecurityToken = (BinarySecretSecurityToken)requestorEntropy;
						array = binarySecretSecurityToken.GetKeyBytes();
					}
					else
					{
						if (!(requestorEntropy is WrappedKeySecurityToken))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("TokenCannotCreateSymmetricCrypto", new object[]
							{
								requestorEntropy
							})));
						}
						array = ((WrappedKeySecurityToken)requestorEntropy).GetWrappedKey();
					}
				}
				else
				{
					array = null;
				}
				if (keyEntropyMode == SecurityKeyEntropyMode.ClientEntropy)
				{
					if (array != null)
					{
						WSTrust.Driver.ValidateRequestedKeySize(array.Length * 8, algorithmSuite);
					}
					proofKey = array;
					issuerEntropy = null;
					issuedKeySize = 0;
					proofToken = null;
					return;
				}
				if (requestSecurityToken.KeySize != 0)
				{
					WSTrust.Driver.ValidateRequestedKeySize(requestSecurityToken.KeySize, algorithmSuite);
					issuedKeySize = requestSecurityToken.KeySize;
				}
				else
				{
					issuedKeySize = algorithmSuite.DefaultSymmetricKeyLength;
				}
				RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
				if (keyEntropyMode == SecurityKeyEntropyMode.ServerEntropy)
				{
					proofKey = new byte[issuedKeySize / 8];
					rngcryptoServiceProvider.GetNonZeroBytes(proofKey);
					issuerEntropy = null;
					proofToken = new BinarySecretSecurityToken(proofKey);
					return;
				}
				issuerEntropy = new byte[issuedKeySize / 8];
				rngcryptoServiceProvider.GetNonZeroBytes(issuerEntropy);
				proofKey = RequestSecurityTokenResponse.ComputeCombinedKey(array, issuerEntropy, issuedKeySize);
				proofToken = null;
			}

			// Token: 0x04004000 RID: 16384
			private static readonly string base64Uri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary";

			// Token: 0x04004001 RID: 16385
			private static readonly string hexBinaryUri = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#HexBinary";

			// Token: 0x04004002 RID: 16386
			private SecurityStandardsManager standardsManager;

			// Token: 0x04004003 RID: 16387
			private List<SecurityTokenAuthenticator> entropyAuthenticators;
		}
	}
}
