using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000296 RID: 662
	internal class WSSecurityPolicy12 : WSSecurityPolicy
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0004B3F9 File Offset: 0x000495F9
		public override string WsspNamespaceUri
		{
			get
			{
				return "http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702";
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0004B400 File Offset: 0x00049600
		public override bool IsSecurityVersionSupported(MessageSecurityVersion version)
		{
			return version == MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10 || version == MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12 || version == MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0004B41C File Offset: 0x0004961C
		public override MessageSecurityVersion GetSupportedMessageSecurityVersion(SecurityVersion version)
		{
			if (version != SecurityVersion.WSSecurity10)
			{
				return MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
			}
			return MessageSecurityVersion.WSSecurity10WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0004B431 File Offset: 0x00049631
		public override TrustDriver TrustDriver
		{
			get
			{
				return new WSTrustDec2005.DriverDec2005(new SecurityStandardsManager(MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12, WSSecurityTokenSerializer.DefaultInstance));
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0004B448 File Offset: 0x00049648
		public override XmlElement CreateWsspHttpsTokenAssertion(MetadataExporter exporter, HttpsTransportBindingElement httpsBinding)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("HttpsToken");
			if (httpsBinding.RequireClientCertificate || httpsBinding.AuthenticationScheme == AuthenticationSchemes.Basic || httpsBinding.AuthenticationScheme == AuthenticationSchemes.Digest)
			{
				XmlElement xmlElement2 = this.CreateWspPolicyWrapper(exporter, new XmlElement[0]);
				if (httpsBinding.RequireClientCertificate)
				{
					xmlElement2.AppendChild(this.CreateWsspAssertion("RequireClientCertificate"));
				}
				if (httpsBinding.AuthenticationScheme == AuthenticationSchemes.Basic)
				{
					xmlElement2.AppendChild(this.CreateWsspAssertion("HttpBasicAuthentication"));
				}
				else if (httpsBinding.AuthenticationScheme == AuthenticationSchemes.Digest)
				{
					xmlElement2.AppendChild(this.CreateWsspAssertion("HttpDigestAuthentication"));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0004B4E4 File Offset: 0x000496E4
		public override bool TryImportWsspHttpsTokenAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, HttpsTransportBindingElement httpsBinding)
		{
			if (assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertions");
			}
			bool result = true;
			XmlElement xmlElement;
			if (this.TryImportWsspAssertion(assertions, "HttpsToken", out xmlElement))
			{
				XmlElement xmlElement2 = null;
				foreach (object obj in xmlElement.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode is XmlElement && xmlNode.LocalName == "Policy" && (xmlNode.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/policy" || xmlNode.NamespaceURI == "http://www.w3.org/ns/ws-policy"))
					{
						xmlElement2 = (XmlElement)xmlNode;
						break;
					}
				}
				if (xmlElement2 == null)
				{
					return result;
				}
				using (IEnumerator enumerator2 = xmlElement2.ChildNodes.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2 is XmlElement && xmlNode2.NamespaceURI == this.WsspNamespaceUri)
						{
							if (xmlNode2.LocalName == "RequireClientCertificate")
							{
								httpsBinding.RequireClientCertificate = true;
							}
							else if (xmlNode2.LocalName == "HttpBasicAuthentication")
							{
								httpsBinding.AuthenticationScheme = AuthenticationSchemes.Basic;
							}
							else if (xmlNode2.LocalName == "HttpDigestAuthentication")
							{
								httpsBinding.AuthenticationScheme = AuthenticationSchemes.Digest;
							}
						}
					}
					return result;
				}
			}
			result = false;
			return result;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0004B674 File Offset: 0x00049874
		public override Collection<XmlElement> CreateWsspSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> signedEndorsing, Collection<SecurityTokenParameters> optionalSigned, Collection<SecurityTokenParameters> optionalSignedEncrypted, Collection<SecurityTokenParameters> optionalEndorsing, Collection<SecurityTokenParameters> optionalSignedEndorsing, AddressingVersion addressingVersion)
		{
			Collection<XmlElement> collection = new Collection<XmlElement>();
			XmlElement xmlElement = this.CreateWsspSignedSupportingTokensAssertion(exporter, signed, optionalSigned);
			if (xmlElement != null)
			{
				collection.Add(xmlElement);
			}
			xmlElement = this.CreateWsspSignedEncryptedSupportingTokensAssertion(exporter, signedEncrypted, optionalSignedEncrypted);
			if (xmlElement != null)
			{
				collection.Add(xmlElement);
			}
			xmlElement = base.CreateWsspEndorsingSupportingTokensAssertion(exporter, endorsing, optionalEndorsing, addressingVersion);
			if (xmlElement != null)
			{
				collection.Add(xmlElement);
			}
			xmlElement = base.CreateWsspSignedEndorsingSupportingTokensAssertion(exporter, signedEndorsing, optionalSignedEndorsing, addressingVersion);
			if (xmlElement != null)
			{
				collection.Add(xmlElement);
			}
			return collection;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0004B6E4 File Offset: 0x000498E4
		public override XmlElement CreateWsspSpnegoContextTokenAssertion(MetadataExporter exporter, SspiSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("SpnegoContextToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateWsspMustNotSendCancelAssertion(false),
				this.CreateWsspMustNotSendAmendAssertion(),
				this.CreateWsspMustNotSendRenewAssertion()
			}));
			return xmlElement;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0004B74C File Offset: 0x0004994C
		public override XmlElement CreateMsspSslContextTokenAssertion(MetadataExporter exporter, SslSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateMsspAssertion("SslContextToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateWsspMustNotSendCancelAssertion(false),
				this.CreateMsspRequireClientCertificateAssertion(parameters.RequireClientCertificate),
				this.CreateWsspMustNotSendAmendAssertion(),
				this.CreateWsspMustNotSendRenewAssertion()
			}));
			return xmlElement;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0004B7C4 File Offset: 0x000499C4
		public override XmlElement CreateWsspSecureConversationTokenAssertion(MetadataExporter exporter, SecureConversationSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("SecureConversationToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateWsspMustNotSendCancelAssertion(parameters.RequireCancellation),
				this.CreateWsspBootstrapPolicyAssertion(exporter, parameters.BootstrapSecurityBindingElement),
				this.CreateWsspMustNotSendAmendAssertion(),
				(!parameters.RequireCancellation || !parameters.CanRenewSession) ? this.CreateWsspMustNotSendRenewAssertion() : null
			}));
			return xmlElement;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0004B854 File Offset: 0x00049A54
		private XmlElement CreateWsspMustNotSendAmendAssertion()
		{
			return this.CreateWsspAssertion("MustNotSendAmend");
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0004B870 File Offset: 0x00049A70
		private XmlElement CreateWsspMustNotSendRenewAssertion()
		{
			return this.CreateWsspAssertion("MustNotSendRenew");
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0004B88C File Offset: 0x00049A8C
		public override bool TryImportWsspSpnegoContextTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			if (this.IsWsspAssertion(assertion, "SpnegoContextToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode))
			{
				Collection<Collection<XmlElement>> collection;
				if (this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collection<XmlElement> collection2 = enumerator.Current;
							SspiSecurityTokenParameters sspiSecurityTokenParameters = new SspiSecurityTokenParameters();
							parameters = sspiSecurityTokenParameters;
							bool flag;
							bool flag2;
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, sspiSecurityTokenParameters) && this.TryImportWsspMustNotSendCancelAssertion(collection2, out flag) && this.TryImportWsspMustNotSendAmendAssertion(collection2) && this.TryImportWsspMustNotSendRenewAssertion(collection2, out flag2) && collection2.Count == 0)
							{
								sspiSecurityTokenParameters.RequireCancellation = true;
								sspiSecurityTokenParameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_BB;
					}
				}
				parameters = new SspiSecurityTokenParameters();
				parameters.RequireDerivedKeys = false;
				parameters.InclusionMode = inclusionMode;
			}
			IL_BB:
			return parameters != null;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0004B96C File Offset: 0x00049B6C
		public override bool TryImportMsspSslContextTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			if (this.IsMsspAssertion(assertion, "SslContextToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode))
			{
				Collection<Collection<XmlElement>> collection;
				if (this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collection<XmlElement> collection2 = enumerator.Current;
							SslSecurityTokenParameters sslSecurityTokenParameters = new SslSecurityTokenParameters();
							parameters = sslSecurityTokenParameters;
							bool flag;
							bool flag2;
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, sslSecurityTokenParameters) && this.TryImportWsspMustNotSendCancelAssertion(collection2, out flag) && this.TryImportWsspMustNotSendAmendAssertion(collection2) && this.TryImportWsspMustNotSendRenewAssertion(collection2, out flag2) && this.TryImportMsspRequireClientCertificateAssertion(collection2, sslSecurityTokenParameters) && collection2.Count == 0)
							{
								sslSecurityTokenParameters.RequireCancellation = true;
								sslSecurityTokenParameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_C9;
					}
				}
				parameters = new SslSecurityTokenParameters();
				parameters.RequireDerivedKeys = false;
				parameters.InclusionMode = inclusionMode;
			}
			IL_C9:
			return parameters != null;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0004BA58 File Offset: 0x00049C58
		public override bool TryImportWsspSecureConversationTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			if (this.IsWsspAssertion(assertion, "SecureConversationToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode))
			{
				Collection<Collection<XmlElement>> collection;
				if (this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collection<XmlElement> collection2 = enumerator.Current;
							SecureConversationSecurityTokenParameters secureConversationSecurityTokenParameters = new SecureConversationSecurityTokenParameters();
							parameters = secureConversationSecurityTokenParameters;
							bool requireCancellation;
							bool canRenewSession;
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, secureConversationSecurityTokenParameters) && this.TryImportWsspMustNotSendCancelAssertion(collection2, out requireCancellation) && this.TryImportWsspMustNotSendAmendAssertion(collection2) && this.TryImportWsspMustNotSendRenewAssertion(collection2, out canRenewSession) && this.TryImportWsspBootstrapPolicyAssertion(importer, collection2, secureConversationSecurityTokenParameters) && collection2.Count == 0)
							{
								secureConversationSecurityTokenParameters.RequireCancellation = requireCancellation;
								secureConversationSecurityTokenParameters.CanRenewSession = canRenewSession;
								secureConversationSecurityTokenParameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_D4;
					}
				}
				parameters = new SecureConversationSecurityTokenParameters();
				parameters.InclusionMode = inclusionMode;
				parameters.RequireDerivedKeys = false;
			}
			IL_D4:
			return parameters != null;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0004BB50 File Offset: 0x00049D50
		public virtual bool TryImportWsspMustNotSendAmendAssertion(ICollection<XmlElement> assertions)
		{
			this.TryImportWsspAssertion(assertions, "MustNotSendAmend");
			return true;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0004BB60 File Offset: 0x00049D60
		public virtual bool TryImportWsspMustNotSendRenewAssertion(ICollection<XmlElement> assertions, out bool canRenewSession)
		{
			canRenewSession = !this.TryImportWsspAssertion(assertions, "MustNotSendRenew");
			return true;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0004BB74 File Offset: 0x00049D74
		private XmlElement CreateWsspSignedSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> optionalSigned)
		{
			XmlElement xmlElement;
			if ((signed == null || signed.Count == 0) && (optionalSigned == null || optionalSigned.Count == 0))
			{
				xmlElement = null;
			}
			else
			{
				XmlElement xmlElement2 = this.CreateWspPolicyWrapper(exporter, new XmlElement[0]);
				if (signed != null)
				{
					foreach (SecurityTokenParameters parameters in signed)
					{
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, parameters));
					}
				}
				if (optionalSigned != null)
				{
					foreach (SecurityTokenParameters parameters2 in optionalSigned)
					{
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, parameters2, true));
					}
				}
				xmlElement = this.CreateWsspAssertion("SignedSupportingTokens");
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0004BC54 File Offset: 0x00049E54
		private XmlElement CreateWsspSignedEncryptedSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> optionalSignedEncrypted)
		{
			XmlElement xmlElement;
			if ((signedEncrypted == null || signedEncrypted.Count == 0) && (optionalSignedEncrypted == null || optionalSignedEncrypted.Count == 0))
			{
				xmlElement = null;
			}
			else
			{
				XmlElement xmlElement2 = this.CreateWspPolicyWrapper(exporter, new XmlElement[0]);
				if (signedEncrypted != null)
				{
					foreach (SecurityTokenParameters parameters in signedEncrypted)
					{
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, parameters));
					}
				}
				if (optionalSignedEncrypted != null)
				{
					foreach (SecurityTokenParameters parameters2 in optionalSignedEncrypted)
					{
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, parameters2, true));
					}
				}
				xmlElement = this.CreateWsspAssertion("SignedEncryptedSupportingTokens");
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0004BD34 File Offset: 0x00049F34
		public override bool TryImportWsspSupportingTokensAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> signedEndorsing, Collection<SecurityTokenParameters> optionalSigned, Collection<SecurityTokenParameters> optionalSignedEncrypted, Collection<SecurityTokenParameters> optionalEndorsing, Collection<SecurityTokenParameters> optionalSignedEndorsing)
		{
			XmlElement xmlElement;
			if (!this.TryImportWsspSignedSupportingTokensAssertion(importer, policyContext, assertions, signed, optionalSigned, out xmlElement) && xmlElement != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
				{
					xmlElement.OuterXml
				})));
			}
			if (!this.TryImportWsspSignedEncryptedSupportingTokensAssertion(importer, policyContext, assertions, signedEncrypted, optionalSignedEncrypted, out xmlElement) && xmlElement != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
				{
					xmlElement.OuterXml
				})));
			}
			if (!base.TryImportWsspEndorsingSupportingTokensAssertion(importer, policyContext, assertions, endorsing, optionalEndorsing, out xmlElement) && xmlElement != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
				{
					xmlElement.OuterXml
				})));
			}
			if (!base.TryImportWsspSignedEndorsingSupportingTokensAssertion(importer, policyContext, assertions, signedEndorsing, optionalSignedEndorsing, out xmlElement) && xmlElement != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
				{
					xmlElement.OuterXml
				})));
			}
			return true;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0004BE38 File Offset: 0x0004A038
		private bool TryImportWsspSignedSupportingTokensAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> optionalSigned, out XmlElement assertion)
		{
			if (signed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signed");
			}
			if (optionalSigned == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalSigned");
			}
			bool result = true;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "SignedSupportingTokens", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					SecurityTokenParameters item;
					bool flag;
					while (collection2.Count > 0 && this.TryImportTokenAssertion(importer, policyContext, collection2, out item, out flag))
					{
						if (flag)
						{
							optionalSigned.Add(item);
						}
						else
						{
							signed.Add(item);
						}
					}
					if (collection2.Count == 0)
					{
						result = true;
						break;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004BF04 File Offset: 0x0004A104
		private bool TryImportWsspSignedEncryptedSupportingTokensAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> optionalSignedEncrypted, out XmlElement assertion)
		{
			if (signedEncrypted == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signedEncrypted");
			}
			if (optionalSignedEncrypted == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalSignedEncrypted");
			}
			bool result = true;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "SignedEncryptedSupportingTokens", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					SecurityTokenParameters item;
					bool flag;
					while (collection2.Count > 0 && this.TryImportTokenAssertion(importer, policyContext, collection2, out item, out flag))
					{
						if (flag)
						{
							optionalSignedEncrypted.Add(item);
						}
						else
						{
							signedEncrypted.Add(item);
						}
					}
					if (collection2.Count == 0)
					{
						result = true;
						break;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004BFD0 File Offset: 0x0004A1D0
		public override bool TryImportWsspRequireDerivedKeysAssertion(ICollection<XmlElement> assertions, SecurityTokenParameters parameters)
		{
			parameters.RequireDerivedKeys = this.TryImportWsspAssertion(assertions, "RequireDerivedKeys");
			if (!parameters.RequireDerivedKeys)
			{
				parameters.RequireDerivedKeys = this.TryImportWsspAssertion(assertions, "RequireExplicitDerivedKeys");
			}
			if (!parameters.RequireDerivedKeys)
			{
				XmlElement xmlElement = null;
				if (this.TryImportWsspAssertion(assertions, "RequireImpliedDerivedKeys", out xmlElement))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
			}
			return true;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0004C04D File Offset: 0x0004A24D
		public override XmlElement CreateWsspTrustAssertion(MetadataExporter exporter, SecurityKeyEntropyMode keyEntropyMode)
		{
			return base.CreateWsspTrustAssertion("Trust13", exporter, keyEntropyMode);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0004C05C File Offset: 0x0004A25C
		public override bool TryImportWsspTrustAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, SecurityBindingElement binding, out XmlElement assertion)
		{
			return base.TryImportWsspTrustAssertion("Trust13", importer, assertions, binding, out assertion);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0004C070 File Offset: 0x0004A270
		public override XmlElement CreateWsspRsaTokenAssertion(RsaSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("KeyValueToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			return xmlElement;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0004C098 File Offset: 0x0004A298
		public override bool TryImportWsspRsaTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			Collection<Collection<XmlElement>> collection;
			if (this.IsWsspAssertion(assertion, "KeyValueToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode) && !this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				parameters = new RsaSecurityTokenParameters();
				parameters.InclusionMode = inclusionMode;
			}
			return parameters != null;
		}

		// Token: 0x04001A9A RID: 6810
		public const string WsspNamespace = "http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702";

		// Token: 0x04001A9B RID: 6811
		public const string SignedEncryptedSupportingTokensName = "SignedEncryptedSupportingTokens";

		// Token: 0x04001A9C RID: 6812
		public const string RequireImpliedDerivedKeysName = "RequireImpliedDerivedKeys";

		// Token: 0x04001A9D RID: 6813
		public const string RequireExplicitDerivedKeysName = "RequireExplicitDerivedKeys";
	}
}
