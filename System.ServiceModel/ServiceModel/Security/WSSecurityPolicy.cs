using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000293 RID: 659
	internal abstract class WSSecurityPolicy
	{
		// Token: 0x0600137E RID: 4990 RVA: 0x000475A0 File Offset: 0x000457A0
		public virtual XmlElement CreateWsspAssertion(string name)
		{
			return WSSecurityPolicy.doc.CreateElement("sp", name, this.WsspNamespaceUri);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000475B8 File Offset: 0x000457B8
		public virtual bool IsWsspAssertion(XmlElement assertion)
		{
			return assertion.NamespaceURI == this.WsspNamespaceUri;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000475CB File Offset: 0x000457CB
		public virtual bool IsWsspAssertion(XmlElement assertion, string name)
		{
			return assertion.NamespaceURI == this.WsspNamespaceUri && assertion.LocalName == name;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000475EE File Offset: 0x000457EE
		public virtual bool IsMsspAssertion(XmlElement assertion, string name)
		{
			return assertion.NamespaceURI == "http://schemas.microsoft.com/ws/2005/07/securitypolicy" && assertion.LocalName == name;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00047610 File Offset: 0x00045810
		public virtual bool TryImportWsspAssertion(ICollection<XmlElement> assertions, string name, out XmlElement assertion)
		{
			assertion = null;
			foreach (XmlElement xmlElement in assertions)
			{
				if (xmlElement.LocalName == name && xmlElement.NamespaceURI == this.WsspNamespaceUri)
				{
					assertion = xmlElement;
					assertions.Remove(xmlElement);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00047688 File Offset: 0x00045888
		public virtual bool TryImportWsspAssertion(ICollection<XmlElement> assertions, string name)
		{
			return this.TryImportWsspAssertion(assertions, name, false);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00047694 File Offset: 0x00045894
		public virtual bool TryImportWsspAssertion(ICollection<XmlElement> assertions, string name, bool isOptional)
		{
			foreach (XmlElement xmlElement in assertions)
			{
				if (xmlElement.LocalName == name && xmlElement.NamespaceURI == this.WsspNamespaceUri)
				{
					assertions.Remove(xmlElement);
					return true;
				}
			}
			return isOptional;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00047708 File Offset: 0x00045908
		public virtual XmlElement CreateMsspAssertion(string name)
		{
			return WSSecurityPolicy.doc.CreateElement("mssp", name, "http://schemas.microsoft.com/ws/2005/07/securitypolicy");
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00047720 File Offset: 0x00045920
		public virtual bool CanImportAssertion(ICollection<XmlElement> assertions)
		{
			foreach (XmlElement xmlElement in assertions)
			{
				if (xmlElement.NamespaceURI == this.WsspNamespaceUri || xmlElement.NamespaceURI == "http://schemas.microsoft.com/ws/2005/07/securitypolicy")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001387 RID: 4999
		public abstract bool IsSecurityVersionSupported(MessageSecurityVersion version);

		// Token: 0x06001388 RID: 5000
		public abstract MessageSecurityVersion GetSupportedMessageSecurityVersion(SecurityVersion version);

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001389 RID: 5001
		public abstract string WsspNamespaceUri { get; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600138A RID: 5002
		public abstract TrustDriver TrustDriver { get; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00047790 File Offset: 0x00045990
		public virtual string AlwaysToRecipientUri
		{
			get
			{
				return this.WsspNamespaceUri + "/IncludeToken/AlwaysToRecipient";
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x000477A2 File Offset: 0x000459A2
		public virtual string NeverUri
		{
			get
			{
				return this.WsspNamespaceUri + "/IncludeToken/Never";
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x000477B4 File Offset: 0x000459B4
		public virtual string OnceUri
		{
			get
			{
				return this.WsspNamespaceUri + "/IncludeToken/Once";
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x000477C6 File Offset: 0x000459C6
		public virtual string AlwaysToInitiatorUri
		{
			get
			{
				return this.WsspNamespaceUri + "/IncludeToken/AlwaysToInitiator";
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000477D8 File Offset: 0x000459D8
		public virtual bool TryImportMsspAssertion(ICollection<XmlElement> assertions, string name)
		{
			foreach (XmlElement xmlElement in assertions)
			{
				if (xmlElement.LocalName == name && xmlElement.NamespaceURI == "http://schemas.microsoft.com/ws/2005/07/securitypolicy")
				{
					assertions.Remove(xmlElement);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00047848 File Offset: 0x00045A48
		public virtual XmlElement CreateWspPolicyWrapper(MetadataExporter exporter, params XmlElement[] nestedAssertions)
		{
			XmlElement xmlElement = WSSecurityPolicy.doc.CreateElement("wsp", "Policy", exporter.PolicyVersion.Namespace);
			if (nestedAssertions != null)
			{
				foreach (XmlElement xmlElement2 in nestedAssertions)
				{
					if (xmlElement2 != null)
					{
						xmlElement.AppendChild(xmlElement2);
					}
				}
			}
			return xmlElement;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00047898 File Offset: 0x00045A98
		public virtual XmlElement CreateWsspSignedPartsAssertion(MessagePartSpecification parts)
		{
			if (parts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parts");
			}
			XmlElement xmlElement;
			if (parts.IsEmpty())
			{
				xmlElement = null;
			}
			else
			{
				xmlElement = this.CreateWsspAssertion("SignedParts");
				if (parts.IsBodyIncluded)
				{
					xmlElement.AppendChild(this.CreateWsspAssertion("Body"));
				}
				foreach (XmlQualifiedName header in parts.HeaderTypes)
				{
					xmlElement.AppendChild(this.CreateWsspHeaderAssertion(header));
				}
			}
			return xmlElement;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00047934 File Offset: 0x00045B34
		public virtual XmlElement CreateWsspEncryptedPartsAssertion(MessagePartSpecification parts)
		{
			if (parts == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parts");
			}
			XmlElement xmlElement;
			if (parts.IsEmpty())
			{
				xmlElement = null;
			}
			else
			{
				xmlElement = this.CreateWsspAssertion("EncryptedParts");
				if (parts.IsBodyIncluded)
				{
					xmlElement.AppendChild(this.CreateWsspAssertion("Body"));
				}
				foreach (XmlQualifiedName header in parts.HeaderTypes)
				{
					xmlElement.AppendChild(this.CreateWsspHeaderAssertion(header));
				}
			}
			return xmlElement;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000479D0 File Offset: 0x00045BD0
		public virtual MessagePartSpecification TryGetProtectedParts(XmlElement assertion)
		{
			MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
			foreach (object obj in assertion.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Whitespace && xmlNode.NodeType != XmlNodeType.Comment)
				{
					if (!(xmlNode is XmlElement))
					{
						messagePartSpecification = null;
						break;
					}
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (this.IsWsspAssertion(xmlElement, "Body"))
					{
						messagePartSpecification.IsBodyIncluded = true;
					}
					else
					{
						if (!this.IsWsspAssertion(xmlElement, "Header"))
						{
							messagePartSpecification = null;
							break;
						}
						string attribute = xmlElement.GetAttribute("Name");
						string attribute2 = xmlElement.GetAttribute("Namespace");
						if (attribute2 == null)
						{
							messagePartSpecification = null;
							break;
						}
						messagePartSpecification.HeaderTypes.Add(new XmlQualifiedName(attribute, attribute2));
					}
				}
			}
			return messagePartSpecification;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00047ABC File Offset: 0x00045CBC
		public virtual bool TryImportWsspEncryptedPartsAssertion(ICollection<XmlElement> assertions, out MessagePartSpecification parts, out XmlElement assertion)
		{
			if (this.TryImportWsspAssertion(assertions, "EncryptedParts", out assertion))
			{
				parts = this.TryGetProtectedParts(assertion);
			}
			else
			{
				parts = null;
			}
			return parts != null;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00047AE1 File Offset: 0x00045CE1
		public virtual bool TryImportWsspSignedPartsAssertion(ICollection<XmlElement> assertions, out MessagePartSpecification parts, out XmlElement assertion)
		{
			if (this.TryImportWsspAssertion(assertions, "SignedParts", out assertion))
			{
				parts = this.TryGetProtectedParts(assertion);
			}
			else
			{
				parts = null;
			}
			return parts != null;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00047B08 File Offset: 0x00045D08
		public virtual XmlElement CreateWsspHeaderAssertion(XmlQualifiedName header)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("Header");
			xmlElement.SetAttribute("Name", header.Name);
			xmlElement.SetAttribute("Namespace", header.Namespace);
			return xmlElement;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00047B44 File Offset: 0x00045D44
		public virtual XmlElement CreateWsspSymmetricBindingAssertion(MetadataExporter exporter, PolicyConversionContext policyContext, SymmetricSecurityBindingElement binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			XmlElement xmlElement = this.CreateWsspAssertion("SymmetricBinding");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspProtectionTokenAssertion(exporter, binding.ProtectionTokenParameters),
				this.CreateWsspAlgorithmSuiteAssertion(exporter, binding.DefaultAlgorithmSuite),
				this.CreateWsspLayoutAssertion(exporter, binding.SecurityHeaderLayout),
				this.CreateWsspIncludeTimestampAssertion(binding.IncludeTimestamp),
				this.CreateWsspEncryptBeforeSigningAssertion(binding.MessageProtectionOrder),
				this.CreateWsspEncryptSignatureAssertion(policyContext, binding),
				this.CreateWsspProtectTokensAssertion(binding),
				this.CreateWsspAssertion("OnlySignEntireHeadersAndBody")
			}));
			return xmlElement;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00047BF8 File Offset: 0x00045DF8
		public virtual bool TryGetNestedPolicyAlternatives(MetadataImporter importer, XmlElement assertion, out Collection<Collection<XmlElement>> alternatives)
		{
			alternatives = null;
			XmlElement xmlElement = null;
			foreach (object obj in assertion.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode is XmlElement && xmlNode.LocalName == "Policy" && (xmlNode.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/policy" || xmlNode.NamespaceURI == "http://www.w3.org/ns/ws-policy"))
				{
					xmlElement = (XmlElement)xmlNode;
					break;
				}
			}
			if (xmlElement == null)
			{
				alternatives = null;
			}
			else
			{
				IEnumerable<IEnumerable<XmlElement>> enumerable = importer.NormalizePolicy(new XmlElement[]
				{
					xmlElement
				});
				alternatives = new Collection<Collection<XmlElement>>();
				foreach (IEnumerable<XmlElement> enumerable2 in enumerable)
				{
					Collection<XmlElement> collection = new Collection<XmlElement>();
					alternatives.Add(collection);
					foreach (XmlElement item in enumerable2)
					{
						collection.Add(item);
					}
				}
			}
			return alternatives != null;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00047D48 File Offset: 0x00045F48
		public virtual bool TryImportWsspSymmetricBindingAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, out SymmetricSecurityBindingElement binding, out XmlElement assertion)
		{
			binding = null;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "SymmetricBinding", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					binding = new SymmetricSecurityBindingElement();
					MessageProtectionOrder messageProtectionOrder;
					bool protectTokens;
					if (this.TryImportWsspProtectionTokenAssertion(importer, policyContext, collection2, binding) && this.TryImportWsspAlgorithmSuiteAssertion(importer, collection2, binding) && this.TryImportWsspLayoutAssertion(importer, collection2, binding) && this.TryImportWsspIncludeTimestampAssertion(collection2, binding) && this.TryImportMessageProtectionOrderAssertions(collection2, out messageProtectionOrder) && this.TryImportWsspProtectTokensAssertion(collection2, out protectTokens) && this.TryImportWsspAssertion(collection2, "OnlySignEntireHeadersAndBody", true) && collection2.Count == 0)
					{
						binding.MessageProtectionOrder = messageProtectionOrder;
						binding.ProtectTokens = protectTokens;
						break;
					}
					binding = null;
				}
			}
			return binding != null;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00047E40 File Offset: 0x00046040
		public virtual XmlElement CreateWsspAsymmetricBindingAssertion(MetadataExporter exporter, PolicyConversionContext policyContext, AsymmetricSecurityBindingElement binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			XmlElement xmlElement = this.CreateWsspAssertion("AsymmetricBinding");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspInitiatorTokenAssertion(exporter, binding.InitiatorTokenParameters),
				this.CreateWsspRecipientTokenAssertion(exporter, binding.RecipientTokenParameters),
				this.CreateWsspAlgorithmSuiteAssertion(exporter, binding.DefaultAlgorithmSuite),
				this.CreateWsspLayoutAssertion(exporter, binding.SecurityHeaderLayout),
				this.CreateWsspIncludeTimestampAssertion(binding.IncludeTimestamp),
				this.CreateWsspEncryptBeforeSigningAssertion(binding.MessageProtectionOrder),
				this.CreateWsspEncryptSignatureAssertion(policyContext, binding),
				this.CreateWsspProtectTokensAssertion(binding),
				this.CreateWsspAssertion("OnlySignEntireHeadersAndBody")
			}));
			return xmlElement;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00047F04 File Offset: 0x00046104
		public virtual bool TryImportWsspAsymmetricBindingAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, out AsymmetricSecurityBindingElement binding, out XmlElement assertion)
		{
			binding = null;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "AsymmetricBinding", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					binding = new AsymmetricSecurityBindingElement();
					MessageProtectionOrder messageProtectionOrder;
					bool protectTokens;
					if (this.TryImportWsspInitiatorTokenAssertion(importer, policyContext, collection2, binding) && this.TryImportWsspRecipientTokenAssertion(importer, policyContext, collection2, binding) && this.TryImportWsspAlgorithmSuiteAssertion(importer, collection2, binding) && this.TryImportWsspLayoutAssertion(importer, collection2, binding) && this.TryImportWsspIncludeTimestampAssertion(collection2, binding) && this.TryImportMessageProtectionOrderAssertions(collection2, out messageProtectionOrder) && this.TryImportWsspProtectTokensAssertion(collection2, out protectTokens) && this.TryImportWsspAssertion(collection2, "OnlySignEntireHeadersAndBody", true) && collection2.Count == 0)
					{
						binding.MessageProtectionOrder = messageProtectionOrder;
						binding.ProtectTokens = protectTokens;
						break;
					}
					binding = null;
				}
			}
			return binding != null;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004800C File Offset: 0x0004620C
		public virtual XmlElement CreateWsspTransportBindingAssertion(MetadataExporter exporter, TransportSecurityBindingElement binding, XmlElement transportTokenAssertion)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("TransportBinding");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspTransportTokenAssertion(exporter, transportTokenAssertion),
				this.CreateWsspAlgorithmSuiteAssertion(exporter, binding.DefaultAlgorithmSuite),
				this.CreateWsspLayoutAssertion(exporter, binding.SecurityHeaderLayout),
				this.CreateWsspIncludeTimestampAssertion(binding.IncludeTimestamp)
			}));
			return xmlElement;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00048074 File Offset: 0x00046274
		public virtual bool TryImportWsspTransportBindingAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, out TransportSecurityBindingElement binding, out XmlElement assertion)
		{
			binding = null;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "TransportBinding", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					binding = new TransportSecurityBindingElement();
					XmlElement item;
					if (this.TryImportWsspTransportTokenAssertion(importer, collection2, out item) && this.TryImportWsspAlgorithmSuiteAssertion(importer, collection2, binding) && this.TryImportWsspLayoutAssertion(importer, collection2, binding) && this.TryImportWsspIncludeTimestampAssertion(collection2, binding) && collection2.Count == 0)
					{
						if (!importer.State.ContainsKey("InSecureConversationBootstrapBindingImportMode"))
						{
							assertions.Add(item);
							break;
						}
						break;
					}
					else
					{
						binding = null;
					}
				}
			}
			return binding != null;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00048140 File Offset: 0x00046340
		public virtual XmlElement CreateWsspWssAssertion(MetadataExporter exporter, SecurityBindingElement binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (binding.MessageSecurityVersion.SecurityVersion == SecurityVersion.WSSecurity10)
			{
				return this.CreateWsspWss10Assertion(exporter);
			}
			if (binding.MessageSecurityVersion.SecurityVersion != SecurityVersion.WSSecurity11)
			{
				return null;
			}
			if (binding is SymmetricSecurityBindingElement)
			{
				return this.CreateWsspWss11Assertion(exporter, ((SymmetricSecurityBindingElement)binding).RequireSignatureConfirmation);
			}
			if (binding is AsymmetricSecurityBindingElement)
			{
				return this.CreateWsspWss11Assertion(exporter, ((AsymmetricSecurityBindingElement)binding).RequireSignatureConfirmation);
			}
			return this.CreateWsspWss11Assertion(exporter, false);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x000481CC File Offset: 0x000463CC
		public virtual bool TryImportWsspWssAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, SecurityBindingElement binding, out XmlElement assertion)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertions");
			}
			bool result = true;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "Wss10", out assertion))
			{
				if (!this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					return result;
				}
				using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Collection<XmlElement> collection2 = enumerator.Current;
						this.TryImportWsspAssertion(collection2, "MustSupportRefKeyIdentifier");
						this.TryImportWsspAssertion(collection2, "MustSupportRefIssuerSerial");
						if (collection2.Count == 0)
						{
							binding.MessageSecurityVersion = this.GetSupportedMessageSecurityVersion(SecurityVersion.WSSecurity10);
							result = true;
							break;
						}
						result = false;
					}
					return result;
				}
			}
			if (this.TryImportWsspAssertion(assertions, "Wss11", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection3 in collection)
				{
					this.TryImportWsspAssertion(collection3, "MustSupportRefKeyIdentifier");
					this.TryImportWsspAssertion(collection3, "MustSupportRefIssuerSerial");
					this.TryImportWsspAssertion(collection3, "MustSupportRefThumbprint");
					this.TryImportWsspAssertion(collection3, "MustSupportRefEncryptedKey");
					bool requireSignatureConfirmation = this.TryImportWsspAssertion(collection3, "RequireSignatureConfirmation");
					if (collection3.Count == 0)
					{
						binding.MessageSecurityVersion = this.GetSupportedMessageSecurityVersion(SecurityVersion.WSSecurity11);
						if (binding is SymmetricSecurityBindingElement)
						{
							((SymmetricSecurityBindingElement)binding).RequireSignatureConfirmation = requireSignatureConfirmation;
						}
						else if (binding is AsymmetricSecurityBindingElement)
						{
							((AsymmetricSecurityBindingElement)binding).RequireSignatureConfirmation = requireSignatureConfirmation;
						}
						result = true;
						break;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004838C File Offset: 0x0004658C
		public virtual XmlElement CreateWsspWss10Assertion(MetadataExporter exporter)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("Wss10");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspAssertionMustSupportRefKeyIdentifierName(),
				this.CreateWsspAssertionMustSupportRefIssuerSerialName()
			}));
			return xmlElement;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x000483CC File Offset: 0x000465CC
		public virtual XmlElement CreateWsspWss11Assertion(MetadataExporter exporter, bool requireSignatureConfirmation)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("Wss11");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspAssertionMustSupportRefKeyIdentifierName(),
				this.CreateWsspAssertionMustSupportRefIssuerSerialName(),
				this.CreateWsspAssertionMustSupportRefThumbprintName(),
				this.CreateWsspAssertionMustSupportRefEncryptedKeyName(),
				this.CreateWsspRequireSignatureConformationAssertion(requireSignatureConfirmation)
			}));
			return xmlElement;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00048428 File Offset: 0x00046628
		public virtual XmlElement CreateWsspAssertionMustSupportRefKeyIdentifierName()
		{
			if (this._mustSupportRefKeyIdentifierName)
			{
				return this.CreateWsspAssertion("MustSupportRefKeyIdentifier");
			}
			return null;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004843F File Offset: 0x0004663F
		public virtual XmlElement CreateWsspAssertionMustSupportRefIssuerSerialName()
		{
			if (this._mustSupportRefIssuerSerialName)
			{
				return this.CreateWsspAssertion("MustSupportRefIssuerSerial");
			}
			return null;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00048456 File Offset: 0x00046656
		public virtual XmlElement CreateWsspAssertionMustSupportRefThumbprintName()
		{
			if (this._mustSupportRefThumbprintName)
			{
				return this.CreateWsspAssertion("MustSupportRefThumbprint");
			}
			return null;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004846D File Offset: 0x0004666D
		public virtual XmlElement CreateWsspAssertionMustSupportRefEncryptedKeyName()
		{
			if (this._protectionTokenHasAsymmetricKey)
			{
				return this.CreateWsspAssertion("MustSupportRefEncryptedKey");
			}
			return null;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00048484 File Offset: 0x00046684
		public virtual XmlElement CreateWsspRequireSignatureConformationAssertion(bool requireSignatureConfirmation)
		{
			if (requireSignatureConfirmation)
			{
				return this.CreateWsspAssertion("RequireSignatureConfirmation");
			}
			return null;
		}

		// Token: 0x060013A7 RID: 5031
		public abstract XmlElement CreateWsspTrustAssertion(MetadataExporter exporter, SecurityKeyEntropyMode keyEntropyMode);

		// Token: 0x060013A8 RID: 5032
		public abstract bool TryImportWsspTrustAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, SecurityBindingElement binding, out XmlElement assertion);

		// Token: 0x060013A9 RID: 5033 RVA: 0x00048498 File Offset: 0x00046698
		protected XmlElement CreateWsspTrustAssertion(string trustName, MetadataExporter exporter, SecurityKeyEntropyMode keyEntropyMode)
		{
			XmlElement xmlElement = this.CreateWsspAssertion(trustName);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspAssertion("MustSupportIssuedTokens"),
				this.CreateWsspRequireClientEntropyAssertion(keyEntropyMode),
				this.CreateWsspRequireServerEntropyAssertion(keyEntropyMode)
			}));
			return xmlElement;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x000484E4 File Offset: 0x000466E4
		protected bool TryImportWsspTrustAssertion(string trustName, MetadataImporter importer, ICollection<XmlElement> assertions, SecurityBindingElement binding, out XmlElement assertion)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertions");
			}
			bool result = true;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, trustName, out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					this.TryImportWsspAssertion(collection2, "MustSupportIssuedTokens");
					bool flag = this.TryImportWsspAssertion(collection2, "RequireClientEntropy");
					bool flag2 = this.TryImportWsspAssertion(collection2, "RequireServerEntropy");
					if (trustName == "Trust13")
					{
						this.TryImportWsspAssertion(collection2, "RequireAppliesTo");
					}
					if (collection2.Count == 0)
					{
						if (flag)
						{
							if (flag2)
							{
								binding.KeyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;
							}
							else
							{
								binding.KeyEntropyMode = SecurityKeyEntropyMode.ClientEntropy;
							}
						}
						else if (flag2)
						{
							binding.KeyEntropyMode = SecurityKeyEntropyMode.ServerEntropy;
						}
						result = true;
						break;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000485F0 File Offset: 0x000467F0
		public virtual XmlElement CreateWsspRequireClientEntropyAssertion(SecurityKeyEntropyMode keyEntropyMode)
		{
			if (keyEntropyMode == SecurityKeyEntropyMode.ClientEntropy || keyEntropyMode == SecurityKeyEntropyMode.CombinedEntropy)
			{
				return this.CreateWsspAssertion("RequireClientEntropy");
			}
			return null;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00048606 File Offset: 0x00046806
		public virtual XmlElement CreateWsspRequireServerEntropyAssertion(SecurityKeyEntropyMode keyEntropyMode)
		{
			if (keyEntropyMode == SecurityKeyEntropyMode.ServerEntropy || keyEntropyMode == SecurityKeyEntropyMode.CombinedEntropy)
			{
				return this.CreateWsspAssertion("RequireServerEntropy");
			}
			return null;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x00048620 File Offset: 0x00046820
		public virtual Collection<XmlElement> CreateWsspSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> signedEndorsing, Collection<SecurityTokenParameters> optionalSigned, Collection<SecurityTokenParameters> optionalSignedEncrypted, Collection<SecurityTokenParameters> optionalEndorsing, Collection<SecurityTokenParameters> optionalSignedEndorsing)
		{
			return this.CreateWsspSupportingTokensAssertion(exporter, signed, signedEncrypted, endorsing, signedEndorsing, optionalSigned, optionalSignedEncrypted, optionalEndorsing, optionalSignedEndorsing, null);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00048644 File Offset: 0x00046844
		public virtual Collection<XmlElement> CreateWsspSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> signedEndorsing, Collection<SecurityTokenParameters> optionalSigned, Collection<SecurityTokenParameters> optionalSignedEncrypted, Collection<SecurityTokenParameters> optionalEndorsing, Collection<SecurityTokenParameters> optionalSignedEndorsing, AddressingVersion addressingVersion)
		{
			Collection<XmlElement> collection = new Collection<XmlElement>();
			XmlElement xmlElement = this.CreateWsspSignedSupportingTokensAssertion(exporter, signed, signedEncrypted, optionalSigned, optionalSignedEncrypted);
			if (xmlElement != null)
			{
				collection.Add(xmlElement);
			}
			xmlElement = this.CreateWsspEndorsingSupportingTokensAssertion(exporter, endorsing, optionalEndorsing, addressingVersion);
			if (xmlElement != null)
			{
				collection.Add(xmlElement);
			}
			xmlElement = this.CreateWsspSignedEndorsingSupportingTokensAssertion(exporter, signedEndorsing, optionalSignedEndorsing, addressingVersion);
			if (xmlElement != null)
			{
				collection.Add(xmlElement);
			}
			return collection;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000486A0 File Offset: 0x000468A0
		protected XmlElement CreateWsspSignedSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> optionalSigned, Collection<SecurityTokenParameters> optionalSignedEncrypted)
		{
			XmlElement xmlElement;
			if ((signed == null || signed.Count == 0) && (signedEncrypted == null || signedEncrypted.Count == 0) && (optionalSigned == null || optionalSigned.Count == 0) && (optionalSignedEncrypted == null || optionalSignedEncrypted.Count == 0))
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
				if (signedEncrypted != null)
				{
					foreach (SecurityTokenParameters parameters2 in signedEncrypted)
					{
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, parameters2));
					}
				}
				if (optionalSigned != null)
				{
					foreach (SecurityTokenParameters parameters3 in optionalSigned)
					{
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, parameters3, true));
					}
				}
				if (optionalSignedEncrypted != null)
				{
					foreach (SecurityTokenParameters parameters4 in optionalSignedEncrypted)
					{
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, parameters4, true));
					}
				}
				xmlElement = this.CreateWsspAssertion("SignedSupportingTokens");
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00048830 File Offset: 0x00046A30
		protected XmlElement CreateWsspEndorsingSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> optionalEndorsing, AddressingVersion addressingVersion)
		{
			return this.CreateWsspiSupportingTokensAssertion(exporter, endorsing, optionalEndorsing, addressingVersion, "EndorsingSupportingTokens");
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00048842 File Offset: 0x00046A42
		protected XmlElement CreateWsspSignedEndorsingSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> signedEndorsing, Collection<SecurityTokenParameters> optionalSignedEndorsing, AddressingVersion addressingVersion)
		{
			return this.CreateWsspiSupportingTokensAssertion(exporter, signedEndorsing, optionalSignedEndorsing, addressingVersion, "SignedEndorsingSupportingTokens");
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00048854 File Offset: 0x00046A54
		protected XmlElement CreateWsspiSupportingTokensAssertion(MetadataExporter exporter, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> optionalEndorsing, AddressingVersion addressingVersion, string assertionName)
		{
			bool flag = false;
			XmlElement xmlElement;
			if ((endorsing == null || endorsing.Count == 0) && (optionalEndorsing == null || optionalEndorsing.Count == 0))
			{
				xmlElement = null;
			}
			else
			{
				XmlElement xmlElement2 = this.CreateWspPolicyWrapper(exporter, new XmlElement[0]);
				if (endorsing != null)
				{
					foreach (SecurityTokenParameters securityTokenParameters in endorsing)
					{
						if (securityTokenParameters.HasAsymmetricKey)
						{
							flag = true;
						}
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, securityTokenParameters));
					}
				}
				if (optionalEndorsing != null)
				{
					foreach (SecurityTokenParameters securityTokenParameters2 in optionalEndorsing)
					{
						if (securityTokenParameters2.HasAsymmetricKey)
						{
							flag = true;
						}
						xmlElement2.AppendChild(this.CreateTokenAssertion(exporter, securityTokenParameters2, true));
					}
				}
				if (addressingVersion != null && AddressingVersion.None != addressingVersion && flag)
				{
					xmlElement2.AppendChild(this.CreateWsspSignedPartsAssertion(new MessagePartSpecification(new XmlQualifiedName[]
					{
						new XmlQualifiedName("To", addressingVersion.Namespace)
					})));
				}
				xmlElement = this.CreateWsspAssertion(assertionName);
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00048984 File Offset: 0x00046B84
		public virtual bool TryImportWsspSupportingTokensAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> signedEndorsing, Collection<SecurityTokenParameters> optionalSigned, Collection<SecurityTokenParameters> optionalSignedEncrypted, Collection<SecurityTokenParameters> optionalEndorsing, Collection<SecurityTokenParameters> optionalSignedEndorsing)
		{
			XmlElement xmlElement;
			if (!this.TryImportWsspSignedSupportingTokensAssertion(importer, policyContext, assertions, signed, signedEncrypted, optionalSigned, optionalSignedEncrypted, out xmlElement) && xmlElement != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
				{
					xmlElement.OuterXml
				})));
			}
			if (!this.TryImportWsspEndorsingSupportingTokensAssertion(importer, policyContext, assertions, endorsing, optionalEndorsing, out xmlElement) && xmlElement != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
				{
					xmlElement.OuterXml
				})));
			}
			if (!this.TryImportWsspSignedEndorsingSupportingTokensAssertion(importer, policyContext, assertions, signedEndorsing, optionalSignedEndorsing, out xmlElement) && xmlElement != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
				{
					xmlElement.OuterXml
				})));
			}
			return true;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00048A50 File Offset: 0x00046C50
		protected bool TryImportWsspSignedSupportingTokensAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, Collection<SecurityTokenParameters> signed, Collection<SecurityTokenParameters> signedEncrypted, Collection<SecurityTokenParameters> optionalSigned, Collection<SecurityTokenParameters> optionalSignedEncrypted, out XmlElement assertion)
		{
			if (signed == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signed");
			}
			if (signedEncrypted == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signedEncrypted");
			}
			if (optionalSigned == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalSigned");
			}
			if (optionalSignedEncrypted == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalSignedEncrypted");
			}
			bool result = true;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "SignedSupportingTokens", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					Collection<SecurityTokenParameters> collection3 = new Collection<SecurityTokenParameters>();
					Collection<SecurityTokenParameters> collection4 = new Collection<SecurityTokenParameters>();
					SecurityTokenParameters item;
					bool flag;
					while (collection2.Count > 0 && this.TryImportTokenAssertion(importer, policyContext, collection2, out item, out flag))
					{
						if (flag)
						{
							collection4.Add(item);
						}
						else
						{
							collection3.Add(item);
						}
					}
					if (collection2.Count == 0)
					{
						foreach (SecurityTokenParameters securityTokenParameters in collection3)
						{
							if (securityTokenParameters is UserNameSecurityTokenParameters)
							{
								signedEncrypted.Add(securityTokenParameters);
							}
							else
							{
								signed.Add(securityTokenParameters);
							}
						}
						foreach (SecurityTokenParameters securityTokenParameters2 in collection4)
						{
							if (securityTokenParameters2 is UserNameSecurityTokenParameters)
							{
								optionalSignedEncrypted.Add(securityTokenParameters2);
							}
							else
							{
								optionalSigned.Add(securityTokenParameters2);
							}
						}
						result = true;
						break;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00048C08 File Offset: 0x00046E08
		protected bool TryImportWsspEndorsingSupportingTokensAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, Collection<SecurityTokenParameters> endorsing, Collection<SecurityTokenParameters> optionalEndorsing, out XmlElement assertion)
		{
			if (endorsing == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endorsing");
			}
			if (optionalEndorsing == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalEndorsing");
			}
			bool result = true;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "EndorsingSupportingTokens", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					MessagePartSpecification messagePartSpecification;
					if (!this.TryImportWsspSignedPartsAssertion(collection2, out messagePartSpecification, out assertion) && assertion != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
						{
							assertion.OuterXml
						})));
					}
					Collection<SecurityTokenParameters> collection3 = new Collection<SecurityTokenParameters>();
					Collection<SecurityTokenParameters> collection4 = new Collection<SecurityTokenParameters>();
					SecurityTokenParameters item;
					bool flag;
					while (collection2.Count > 0 && this.TryImportTokenAssertion(importer, policyContext, collection2, out item, out flag))
					{
						if (flag)
						{
							collection4.Add(item);
						}
						else
						{
							collection3.Add(item);
						}
					}
					if (collection2.Count == 0)
					{
						foreach (SecurityTokenParameters item2 in collection3)
						{
							endorsing.Add(item2);
						}
						foreach (SecurityTokenParameters item3 in collection4)
						{
							optionalEndorsing.Add(item3);
						}
						result = true;
						break;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00048DD0 File Offset: 0x00046FD0
		protected bool TryImportWsspSignedEndorsingSupportingTokensAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, Collection<SecurityTokenParameters> signedEndorsing, Collection<SecurityTokenParameters> optionalSignedEndorsing, out XmlElement assertion)
		{
			if (signedEndorsing == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("signedEndorsing");
			}
			if (optionalSignedEndorsing == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("optionalSignedEndorsing");
			}
			bool result = true;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "SignedEndorsingSupportingTokens", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					MessagePartSpecification messagePartSpecification;
					if (!this.TryImportWsspSignedPartsAssertion(collection2, out messagePartSpecification, out assertion) && assertion != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
						{
							assertion.OuterXml
						})));
					}
					Collection<SecurityTokenParameters> collection3 = new Collection<SecurityTokenParameters>();
					Collection<SecurityTokenParameters> collection4 = new Collection<SecurityTokenParameters>();
					SecurityTokenParameters item;
					bool flag;
					while (collection2.Count > 0 && this.TryImportTokenAssertion(importer, policyContext, collection2, out item, out flag))
					{
						if (flag)
						{
							collection4.Add(item);
						}
						else
						{
							collection3.Add(item);
						}
					}
					if (collection2.Count == 0)
					{
						foreach (SecurityTokenParameters item2 in collection3)
						{
							signedEndorsing.Add(item2);
						}
						foreach (SecurityTokenParameters item3 in collection4)
						{
							optionalSignedEndorsing.Add(item3);
						}
						result = true;
						break;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00048F98 File Offset: 0x00047198
		public virtual XmlElement CreateWsspEncryptSignatureAssertion(PolicyConversionContext policyContext, SecurityBindingElement binding)
		{
			MessageProtectionOrder messageProtectionOrder;
			if (binding is SymmetricSecurityBindingElement)
			{
				messageProtectionOrder = ((SymmetricSecurityBindingElement)binding).MessageProtectionOrder;
			}
			else
			{
				messageProtectionOrder = ((AsymmetricSecurityBindingElement)binding).MessageProtectionOrder;
			}
			if (messageProtectionOrder == MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature && this.ContainsEncryptionParts(policyContext, binding))
			{
				return this.CreateWsspAssertion("EncryptSignature");
			}
			return null;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00048FE4 File Offset: 0x000471E4
		private bool ContainsEncryptionParts(PolicyConversionContext policyContext, SecurityBindingElement security)
		{
			if (policyContext.Contract == WSSecurityPolicy.NullContract)
			{
				return true;
			}
			if (security.EndpointSupportingTokenParameters.SignedEncrypted.Count > 0 || security.OptionalEndpointSupportingTokenParameters.SignedEncrypted.Count > 0)
			{
				return true;
			}
			foreach (SupportingTokenParameters supportingTokenParameters in security.OperationSupportingTokenParameters.Values)
			{
				if (supportingTokenParameters.SignedEncrypted.Count > 0)
				{
					return true;
				}
			}
			foreach (SupportingTokenParameters supportingTokenParameters2 in security.OptionalOperationSupportingTokenParameters.Values)
			{
				if (supportingTokenParameters2.SignedEncrypted.Count > 0)
				{
					return true;
				}
			}
			ChannelProtectionRequirements channelProtectionRequirements = SecurityBindingElement.ComputeProtectionRequirements(security, new BindingParameterCollection
			{
				ChannelProtectionRequirements.CreateFromContract(policyContext.Contract, policyContext.BindingElements.Find<SecurityBindingElement>().GetIndividualProperty<ISecurityCapabilities>(), false)
			}, policyContext.BindingElements, true);
			channelProtectionRequirements.MakeReadOnly();
			WSSecurityPolicy securityPolicyDriver = WSSecurityPolicy.GetSecurityPolicyDriver(security.MessageSecurityVersion);
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					ScopedMessagePartSpecification scopedMessagePartSpecification;
					if (messageDescription.Direction == MessageDirection.Input)
					{
						scopedMessagePartSpecification = channelProtectionRequirements.IncomingEncryptionParts;
					}
					else
					{
						scopedMessagePartSpecification = channelProtectionRequirements.OutgoingEncryptionParts;
					}
					MessagePartSpecification messagePartSpecification;
					if (scopedMessagePartSpecification.TryGetParts(messageDescription.Action, out messagePartSpecification) && !messagePartSpecification.IsEmpty())
					{
						return true;
					}
				}
				foreach (FaultDescription faultDescription in operationDescription.Faults)
				{
					MessagePartSpecification messagePartSpecification2;
					if (channelProtectionRequirements.OutgoingEncryptionParts.TryGetParts(faultDescription.Action, out messagePartSpecification2) && !messagePartSpecification2.IsEmpty())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00049244 File Offset: 0x00047444
		public virtual XmlElement CreateWsspEncryptBeforeSigningAssertion(MessageProtectionOrder protectionOrder)
		{
			if (protectionOrder == MessageProtectionOrder.EncryptBeforeSign)
			{
				return this.CreateWsspAssertion("EncryptBeforeSigning");
			}
			return null;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00049257 File Offset: 0x00047457
		public virtual XmlElement CreateWsspProtectTokensAssertion(SecurityBindingElement sbe)
		{
			if (sbe.ProtectTokens)
			{
				return this.CreateWsspAssertion("ProtectTokens");
			}
			return null;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004926E File Offset: 0x0004746E
		public virtual bool TryImportMessageProtectionOrderAssertions(ICollection<XmlElement> assertions, out MessageProtectionOrder order)
		{
			if (this.TryImportWsspAssertion(assertions, "EncryptBeforeSigning"))
			{
				order = MessageProtectionOrder.EncryptBeforeSign;
			}
			else if (this.TryImportWsspAssertion(assertions, "EncryptSignature"))
			{
				order = MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature;
			}
			else
			{
				order = MessageProtectionOrder.SignBeforeEncrypt;
			}
			return true;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0004929A File Offset: 0x0004749A
		public virtual XmlElement CreateWsspIncludeTimestampAssertion(bool includeTimestamp)
		{
			if (includeTimestamp)
			{
				return this.CreateWsspAssertion("IncludeTimestamp");
			}
			return null;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000492AC File Offset: 0x000474AC
		public virtual bool TryImportWsspIncludeTimestampAssertion(ICollection<XmlElement> assertions, SecurityBindingElement binding)
		{
			binding.IncludeTimestamp = this.TryImportWsspAssertion(assertions, "IncludeTimestamp");
			return true;
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x000492C1 File Offset: 0x000474C1
		public virtual bool TryImportWsspProtectTokensAssertion(ICollection<XmlElement> assertions, out bool protectTokens)
		{
			if (this.TryImportWsspAssertion(assertions, "ProtectTokens"))
			{
				protectTokens = true;
			}
			else
			{
				protectTokens = false;
			}
			return true;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x000492DC File Offset: 0x000474DC
		public virtual XmlElement CreateWsspLayoutAssertion(MetadataExporter exporter, SecurityHeaderLayout layout)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("Layout");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateLayoutAssertion(layout)
			}));
			return xmlElement;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00049314 File Offset: 0x00047514
		public virtual bool TryImportWsspLayoutAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, SecurityBindingElement binding)
		{
			bool result = false;
			XmlElement assertion;
			if (this.TryImportWsspAssertion(assertions, "Layout", out assertion))
			{
				Collection<Collection<XmlElement>> collection;
				if (!this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					return result;
				}
				using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Collection<XmlElement> collection2 = enumerator.Current;
						SecurityHeaderLayout securityHeaderLayout;
						if (this.TryImportLayoutAssertion(collection2, out securityHeaderLayout) && collection2.Count == 0)
						{
							binding.SecurityHeaderLayout = securityHeaderLayout;
							result = true;
							break;
						}
					}
					return result;
				}
			}
			binding.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
			result = true;
			return result;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x000493A4 File Offset: 0x000475A4
		public virtual XmlElement CreateLayoutAssertion(SecurityHeaderLayout layout)
		{
			switch (layout)
			{
			case SecurityHeaderLayout.Strict:
				return this.CreateWsspAssertion("Strict");
			case SecurityHeaderLayout.Lax:
				return this.CreateWsspAssertion("Lax");
			case SecurityHeaderLayout.LaxTimestampFirst:
				return this.CreateWsspAssertion("LaxTsFirst");
			case SecurityHeaderLayout.LaxTimestampLast:
				return this.CreateWsspAssertion("LaxTsLast");
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("layout"));
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004940C File Offset: 0x0004760C
		public virtual bool TryImportLayoutAssertion(ICollection<XmlElement> assertions, out SecurityHeaderLayout layout)
		{
			bool result = true;
			layout = SecurityHeaderLayout.Lax;
			if (this.TryImportWsspAssertion(assertions, "Lax"))
			{
				layout = SecurityHeaderLayout.Lax;
			}
			else if (this.TryImportWsspAssertion(assertions, "LaxTsFirst"))
			{
				layout = SecurityHeaderLayout.LaxTimestampFirst;
			}
			else if (this.TryImportWsspAssertion(assertions, "LaxTsLast"))
			{
				layout = SecurityHeaderLayout.LaxTimestampLast;
			}
			else if (this.TryImportWsspAssertion(assertions, "Strict"))
			{
				layout = SecurityHeaderLayout.Strict;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00049470 File Offset: 0x00047670
		public virtual XmlElement CreateWsspAlgorithmSuiteAssertion(MetadataExporter exporter, SecurityAlgorithmSuite suite)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("AlgorithmSuite");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateAlgorithmSuiteAssertion(suite)
			}));
			return xmlElement;
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x000494A8 File Offset: 0x000476A8
		public virtual bool TryImportWsspAlgorithmSuiteAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, SecurityBindingElement binding)
		{
			SecurityAlgorithmSuite securityAlgorithmSuite = null;
			XmlElement assertion;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "AlgorithmSuite", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					if (this.TryImportAlgorithmSuiteAssertion(collection2, out securityAlgorithmSuite) && collection2.Count == 0)
					{
						binding.DefaultAlgorithmSuite = securityAlgorithmSuite;
						break;
					}
					securityAlgorithmSuite = null;
				}
			}
			return securityAlgorithmSuite != null;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004952C File Offset: 0x0004772C
		public virtual XmlElement CreateAlgorithmSuiteAssertion(SecurityAlgorithmSuite suite)
		{
			if (suite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("suite");
			}
			XmlElement result;
			if (suite == SecurityAlgorithmSuite.Basic256)
			{
				result = this.CreateWsspAssertion("Basic256");
			}
			else if (suite == SecurityAlgorithmSuite.Basic192)
			{
				result = this.CreateWsspAssertion("Basic192");
			}
			else if (suite == SecurityAlgorithmSuite.Basic128)
			{
				result = this.CreateWsspAssertion("Basic128");
			}
			else if (suite == SecurityAlgorithmSuite.TripleDes)
			{
				result = this.CreateWsspAssertion("TripleDes");
			}
			else if (suite == SecurityAlgorithmSuite.Basic256Rsa15)
			{
				result = this.CreateWsspAssertion("Basic256Rsa15");
			}
			else if (suite == SecurityAlgorithmSuite.Basic192Rsa15)
			{
				result = this.CreateWsspAssertion("Basic192Rsa15");
			}
			else if (suite == SecurityAlgorithmSuite.Basic128Rsa15)
			{
				result = this.CreateWsspAssertion("Basic128Rsa15");
			}
			else if (suite == SecurityAlgorithmSuite.TripleDesRsa15)
			{
				result = this.CreateWsspAssertion("TripleDesRsa15");
			}
			else if (suite == SecurityAlgorithmSuite.Basic256Sha256)
			{
				result = this.CreateWsspAssertion("Basic256Sha256");
			}
			else if (suite == SecurityAlgorithmSuite.Basic192Sha256)
			{
				result = this.CreateWsspAssertion("Basic192Sha256");
			}
			else if (suite == SecurityAlgorithmSuite.Basic128Sha256)
			{
				result = this.CreateWsspAssertion("Basic128Sha256");
			}
			else if (suite == SecurityAlgorithmSuite.TripleDesSha256)
			{
				result = this.CreateWsspAssertion("TripleDesSha256");
			}
			else if (suite == SecurityAlgorithmSuite.Basic256Sha256Rsa15)
			{
				result = this.CreateWsspAssertion("Basic256Sha256Rsa15");
			}
			else if (suite == SecurityAlgorithmSuite.Basic192Sha256Rsa15)
			{
				result = this.CreateWsspAssertion("Basic192Sha256Rsa15");
			}
			else if (suite == SecurityAlgorithmSuite.Basic128Sha256Rsa15)
			{
				result = this.CreateWsspAssertion("Basic128Sha256Rsa15");
			}
			else
			{
				if (suite != SecurityAlgorithmSuite.TripleDesSha256Rsa15)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("suite"));
				}
				result = this.CreateWsspAssertion("TripleDesSha256Rsa15");
			}
			return result;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000496E4 File Offset: 0x000478E4
		public virtual bool TryImportAlgorithmSuiteAssertion(ICollection<XmlElement> assertions, out SecurityAlgorithmSuite suite)
		{
			if (this.TryImportWsspAssertion(assertions, "Basic256"))
			{
				suite = SecurityAlgorithmSuite.Basic256;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic192"))
			{
				suite = SecurityAlgorithmSuite.Basic192;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic128"))
			{
				suite = SecurityAlgorithmSuite.Basic128;
			}
			else if (this.TryImportWsspAssertion(assertions, "TripleDes"))
			{
				suite = SecurityAlgorithmSuite.TripleDes;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic256Rsa15"))
			{
				suite = SecurityAlgorithmSuite.Basic256Rsa15;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic192Rsa15"))
			{
				suite = SecurityAlgorithmSuite.Basic192Rsa15;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic128Rsa15"))
			{
				suite = SecurityAlgorithmSuite.Basic128Rsa15;
			}
			else if (this.TryImportWsspAssertion(assertions, "TripleDesRsa15"))
			{
				suite = SecurityAlgorithmSuite.TripleDesRsa15;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic256Sha256"))
			{
				suite = SecurityAlgorithmSuite.Basic256Sha256;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic192Sha256"))
			{
				suite = SecurityAlgorithmSuite.Basic192Sha256;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic128Sha256"))
			{
				suite = SecurityAlgorithmSuite.Basic128Sha256;
			}
			else if (this.TryImportWsspAssertion(assertions, "TripleDesSha256"))
			{
				suite = SecurityAlgorithmSuite.TripleDesSha256;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic256Sha256Rsa15"))
			{
				suite = SecurityAlgorithmSuite.Basic256Sha256Rsa15;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic192Sha256Rsa15"))
			{
				suite = SecurityAlgorithmSuite.Basic192Sha256Rsa15;
			}
			else if (this.TryImportWsspAssertion(assertions, "Basic128Sha256Rsa15"))
			{
				suite = SecurityAlgorithmSuite.Basic128Sha256Rsa15;
			}
			else if (this.TryImportWsspAssertion(assertions, "TripleDesSha256Rsa15"))
			{
				suite = SecurityAlgorithmSuite.TripleDesSha256Rsa15;
			}
			else
			{
				suite = null;
			}
			return suite != null;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00049888 File Offset: 0x00047A88
		public virtual XmlElement CreateWsspProtectionTokenAssertion(MetadataExporter exporter, SecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("ProtectionToken");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateTokenAssertion(exporter, parameters)
			}));
			this._protectionTokenHasAsymmetricKey = parameters.HasAsymmetricKey;
			return xmlElement;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000498D0 File Offset: 0x00047AD0
		public virtual bool TryImportWsspProtectionTokenAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, SymmetricSecurityBindingElement binding)
		{
			bool result = false;
			XmlElement assertion;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "ProtectionToken", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					SecurityTokenParameters protectionTokenParameters;
					bool flag;
					if (this.TryImportTokenAssertion(importer, policyContext, collection2, out protectionTokenParameters, out flag) && collection2.Count == 0)
					{
						result = true;
						binding.ProtectionTokenParameters = protectionTokenParameters;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00049958 File Offset: 0x00047B58
		public virtual bool TryImportWsspInitiatorTokenAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, AsymmetricSecurityBindingElement binding)
		{
			bool result = false;
			XmlElement assertion;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "InitiatorToken", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					SecurityTokenParameters initiatorTokenParameters;
					bool flag;
					if (this.TryImportTokenAssertion(importer, policyContext, collection2, out initiatorTokenParameters, out flag) && collection2.Count == 0)
					{
						result = true;
						binding.InitiatorTokenParameters = initiatorTokenParameters;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000499E0 File Offset: 0x00047BE0
		public virtual bool TryImportWsspRecipientTokenAssertion(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, AsymmetricSecurityBindingElement binding)
		{
			bool result = false;
			XmlElement assertion;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "RecipientToken", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				foreach (Collection<XmlElement> collection2 in collection)
				{
					SecurityTokenParameters recipientTokenParameters;
					bool flag;
					if (this.TryImportTokenAssertion(importer, policyContext, collection2, out recipientTokenParameters, out flag) && collection2.Count == 0)
					{
						result = true;
						binding.RecipientTokenParameters = recipientTokenParameters;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00049A68 File Offset: 0x00047C68
		public virtual XmlElement CreateWsspInitiatorTokenAssertion(MetadataExporter exporter, SecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("InitiatorToken");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateTokenAssertion(exporter, parameters)
			}));
			return xmlElement;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00049AA4 File Offset: 0x00047CA4
		public virtual XmlElement CreateWsspRecipientTokenAssertion(MetadataExporter exporter, SecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("RecipientToken");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateTokenAssertion(exporter, parameters)
			}));
			return xmlElement;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00049AE0 File Offset: 0x00047CE0
		public virtual XmlElement CreateWsspTransportTokenAssertion(MetadataExporter exporter, XmlElement transportTokenAssertion)
		{
			if (transportTokenAssertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("transportTokenAssertion");
			}
			XmlElement xmlElement = this.CreateWsspAssertion("TransportToken");
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				(XmlElement)WSSecurityPolicy.doc.ImportNode(transportTokenAssertion, true)
			}));
			return xmlElement;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00049B38 File Offset: 0x00047D38
		public virtual bool TryImportWsspTransportTokenAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, out XmlElement transportBindingAssertion)
		{
			transportBindingAssertion = null;
			XmlElement assertion;
			Collection<Collection<XmlElement>> collection;
			if (this.TryImportWsspAssertion(assertions, "TransportToken", out assertion) && this.TryGetNestedPolicyAlternatives(importer, assertion, out collection) && collection.Count == 1 && collection[0].Count == 1)
			{
				transportBindingAssertion = collection[0][0];
			}
			return transportBindingAssertion != null;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00049B90 File Offset: 0x00047D90
		public virtual XmlElement CreateTokenAssertion(MetadataExporter exporter, SecurityTokenParameters parameters)
		{
			return this.CreateTokenAssertion(exporter, parameters, false);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00049B9C File Offset: 0x00047D9C
		public virtual XmlElement CreateTokenAssertion(MetadataExporter exporter, SecurityTokenParameters parameters, bool isOptional)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			XmlElement xmlElement;
			if (parameters is KerberosSecurityTokenParameters)
			{
				xmlElement = this.CreateWsspKerberosTokenAssertion(exporter, (KerberosSecurityTokenParameters)parameters);
			}
			else if (parameters is X509SecurityTokenParameters)
			{
				xmlElement = this.CreateWsspX509TokenAssertion(exporter, (X509SecurityTokenParameters)parameters);
			}
			else if (parameters is UserNameSecurityTokenParameters)
			{
				xmlElement = this.CreateWsspUsernameTokenAssertion(exporter, (UserNameSecurityTokenParameters)parameters);
			}
			else if (parameters is IssuedSecurityTokenParameters)
			{
				xmlElement = this.CreateWsspIssuedTokenAssertion(exporter, (IssuedSecurityTokenParameters)parameters);
			}
			else if (parameters is SspiSecurityTokenParameters)
			{
				xmlElement = this.CreateWsspSpnegoContextTokenAssertion(exporter, (SspiSecurityTokenParameters)parameters);
			}
			else if (parameters is SslSecurityTokenParameters)
			{
				xmlElement = this.CreateMsspSslContextTokenAssertion(exporter, (SslSecurityTokenParameters)parameters);
			}
			else if (parameters is SecureConversationSecurityTokenParameters)
			{
				xmlElement = this.CreateWsspSecureConversationTokenAssertion(exporter, (SecureConversationSecurityTokenParameters)parameters);
			}
			else
			{
				if (!(parameters is RsaSecurityTokenParameters))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("parameters"));
				}
				xmlElement = this.CreateWsspRsaTokenAssertion((RsaSecurityTokenParameters)parameters);
			}
			if (xmlElement != null && isOptional)
			{
				xmlElement.SetAttribute("Optional", exporter.PolicyVersion.Namespace, "true");
			}
			return xmlElement;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00049CC0 File Offset: 0x00047EC0
		public virtual bool TryImportTokenAssertion(MetadataImporter importer, PolicyConversionContext policyContext, Collection<XmlElement> assertions, out SecurityTokenParameters parameters, out bool isOptional)
		{
			parameters = null;
			isOptional = false;
			if (assertions.Count >= 1)
			{
				XmlElement xmlElement = assertions[0];
				if (this.TryImportWsspKerberosTokenAssertion(importer, xmlElement, out parameters) || this.TryImportWsspX509TokenAssertion(importer, xmlElement, out parameters) || this.TryImportWsspUsernameTokenAssertion(importer, xmlElement, out parameters) || this.TryImportWsspIssuedTokenAssertion(importer, policyContext, xmlElement, out parameters) || this.TryImportWsspSpnegoContextTokenAssertion(importer, xmlElement, out parameters) || this.TryImportMsspSslContextTokenAssertion(importer, xmlElement, out parameters) || this.TryImportWsspSecureConversationTokenAssertion(importer, xmlElement, out parameters) || this.TryImportWsspRsaTokenAssertion(importer, xmlElement, out parameters))
				{
					string attribute = xmlElement.GetAttribute("Optional", "http://schemas.xmlsoap.org/ws/2004/09/policy");
					if (string.IsNullOrEmpty(attribute))
					{
						attribute = xmlElement.GetAttribute("Optional", "http://www.w3.org/ns/ws-policy");
					}
					try
					{
						isOptional = XmlUtil.IsTrue(attribute);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (ex is NullReferenceException)
						{
							throw;
						}
						importer.Errors.Add(new MetadataConversionError(SR.GetString("UnsupportedBooleanAttribute", new object[]
						{
							"Optional",
							ex.Message
						}), false));
						return false;
					}
					assertions.RemoveAt(0);
				}
			}
			return parameters != null;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00049DF0 File Offset: 0x00047FF0
		public virtual void SetIncludeTokenValue(XmlElement tokenAssertion, SecurityTokenInclusionMode inclusionMode)
		{
			switch (inclusionMode)
			{
			case SecurityTokenInclusionMode.AlwaysToRecipient:
				tokenAssertion.SetAttribute("IncludeToken", this.WsspNamespaceUri, this.AlwaysToRecipientUri);
				return;
			case SecurityTokenInclusionMode.Never:
				tokenAssertion.SetAttribute("IncludeToken", this.WsspNamespaceUri, this.NeverUri);
				return;
			case SecurityTokenInclusionMode.Once:
				tokenAssertion.SetAttribute("IncludeToken", this.WsspNamespaceUri, this.OnceUri);
				return;
			case SecurityTokenInclusionMode.AlwaysToInitiator:
				tokenAssertion.SetAttribute("IncludeToken", this.WsspNamespaceUri, this.AlwaysToInitiatorUri);
				return;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("inclusionMode"));
			}
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00049E8C File Offset: 0x0004808C
		public virtual bool TryGetIncludeTokenValue(XmlElement assertion, out SecurityTokenInclusionMode mode)
		{
			string attribute = assertion.GetAttribute("IncludeToken", this.WsspNamespaceUri);
			if (attribute == this.AlwaysToInitiatorUri)
			{
				mode = SecurityTokenInclusionMode.AlwaysToInitiator;
				return true;
			}
			if (attribute == this.AlwaysToRecipientUri)
			{
				mode = SecurityTokenInclusionMode.AlwaysToRecipient;
				return true;
			}
			if (attribute == this.NeverUri)
			{
				mode = SecurityTokenInclusionMode.Never;
				return true;
			}
			if (attribute == this.OnceUri)
			{
				mode = SecurityTokenInclusionMode.Once;
				return true;
			}
			mode = SecurityTokenInclusionMode.Never;
			return false;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00049EFB File Offset: 0x000480FB
		public virtual XmlElement CreateWsspRequireDerivedKeysAssertion(bool requireDerivedKeys)
		{
			if (requireDerivedKeys)
			{
				return this.CreateWsspAssertion("RequireDerivedKeys");
			}
			return null;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00049F0D File Offset: 0x0004810D
		public virtual bool TryImportWsspRequireDerivedKeysAssertion(ICollection<XmlElement> assertions, SecurityTokenParameters parameters)
		{
			parameters.RequireDerivedKeys = this.TryImportWsspAssertion(assertions, "RequireDerivedKeys");
			return true;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00049F24 File Offset: 0x00048124
		public virtual XmlElement CreateWsspKerberosTokenAssertion(MetadataExporter exporter, KerberosSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("KerberosToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateWsspAssertion("WssGssKerberosV5ApReqToken11")
			}));
			return xmlElement;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00049F7C File Offset: 0x0004817C
		public virtual bool TryImportWsspKerberosTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			if (this.IsWsspAssertion(assertion, "KerberosToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode))
			{
				Collection<Collection<XmlElement>> collection;
				if (this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collection<XmlElement> collection2 = enumerator.Current;
							parameters = new KerberosSecurityTokenParameters();
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, parameters) && this.TryImportWsspAssertion(collection2, "WssGssKerberosV5ApReqToken11", true) && collection2.Count == 0)
							{
								parameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_9C;
					}
				}
				parameters = new KerberosSecurityTokenParameters();
				parameters.RequireDerivedKeys = false;
				parameters.InclusionMode = inclusionMode;
			}
			IL_9C:
			return parameters != null;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0004A03C File Offset: 0x0004823C
		public virtual XmlElement CreateX509ReferenceStyleAssertion(X509KeyIdentifierClauseType referenceStyle)
		{
			switch (referenceStyle)
			{
			case X509KeyIdentifierClauseType.Any:
				this._mustSupportRefIssuerSerialName = true;
				this._mustSupportRefKeyIdentifierName = true;
				this._mustSupportRefThumbprintName = true;
				return null;
			case X509KeyIdentifierClauseType.Thumbprint:
				this._mustSupportRefThumbprintName = true;
				return this.CreateWsspAssertion("RequireThumbprintReference");
			case X509KeyIdentifierClauseType.IssuerSerial:
				this._mustSupportRefIssuerSerialName = true;
				return this.CreateWsspAssertion("RequireIssuerSerialReference");
			case X509KeyIdentifierClauseType.SubjectKeyIdentifier:
				this._mustSupportRefKeyIdentifierName = true;
				return this.CreateWsspAssertion("RequireKeyIdentifierReference");
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("referenceStyle"));
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0004A0C4 File Offset: 0x000482C4
		public virtual bool TryImportX509ReferenceStyleAssertion(ICollection<XmlElement> assertions, X509SecurityTokenParameters parameters)
		{
			if (this.TryImportWsspAssertion(assertions, "RequireIssuerSerialReference"))
			{
				parameters.X509ReferenceStyle = X509KeyIdentifierClauseType.IssuerSerial;
			}
			else if (this.TryImportWsspAssertion(assertions, "RequireKeyIdentifierReference"))
			{
				parameters.X509ReferenceStyle = X509KeyIdentifierClauseType.SubjectKeyIdentifier;
			}
			else if (this.TryImportWsspAssertion(assertions, "RequireThumbprintReference"))
			{
				parameters.X509ReferenceStyle = X509KeyIdentifierClauseType.Thumbprint;
			}
			return true;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0004A118 File Offset: 0x00048318
		public virtual XmlElement CreateWsspX509TokenAssertion(MetadataExporter exporter, X509SecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("X509Token");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateX509ReferenceStyleAssertion(parameters.X509ReferenceStyle),
				this.CreateWsspAssertion("WssX509V3Token10")
			}));
			return xmlElement;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0004A180 File Offset: 0x00048380
		public virtual bool TryImportWsspX509TokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			if (this.IsWsspAssertion(assertion, "X509Token") && this.TryGetIncludeTokenValue(assertion, out inclusionMode))
			{
				Collection<Collection<XmlElement>> collection;
				if (this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collection<XmlElement> collection2 = enumerator.Current;
							X509SecurityTokenParameters x509SecurityTokenParameters = new X509SecurityTokenParameters();
							parameters = x509SecurityTokenParameters;
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, x509SecurityTokenParameters) && this.TryImportX509ReferenceStyleAssertion(collection2, x509SecurityTokenParameters) && this.TryImportWsspAssertion(collection2, "WssX509V3Token10", true) && collection2.Count == 0)
							{
								parameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_AE;
					}
				}
				parameters = new X509SecurityTokenParameters();
				parameters.RequireDerivedKeys = false;
				parameters.InclusionMode = inclusionMode;
			}
			IL_AE:
			return parameters != null;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0004A250 File Offset: 0x00048450
		public virtual XmlElement CreateWsspUsernameTokenAssertion(MetadataExporter exporter, UserNameSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("UsernameToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspAssertion("WssUsernameToken10")
			}));
			return xmlElement;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0004A29C File Offset: 0x0004849C
		public virtual bool TryImportWsspUsernameTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			if (this.IsWsspAssertion(assertion, "UsernameToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode))
			{
				Collection<Collection<XmlElement>> collection;
				if (this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collection<XmlElement> collection2 = enumerator.Current;
							if (this.TryImportWsspAssertion(collection2, "WssUsernameToken10") && collection2.Count == 0)
							{
								parameters = new UserNameSecurityTokenParameters();
								parameters.InclusionMode = inclusionMode;
								break;
							}
						}
						goto IL_82;
					}
				}
				parameters = new UserNameSecurityTokenParameters();
				parameters.InclusionMode = inclusionMode;
			}
			IL_82:
			return parameters != null;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0004A340 File Offset: 0x00048540
		public virtual XmlElement CreateWsspRsaTokenAssertion(RsaSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateMsspAssertion("RsaToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			return xmlElement;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0004A368 File Offset: 0x00048568
		public virtual bool TryImportWsspRsaTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			Collection<Collection<XmlElement>> collection;
			if (this.IsMsspAssertion(assertion, "RsaToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode) && !this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
			{
				parameters = new RsaSecurityTokenParameters();
				parameters.InclusionMode = inclusionMode;
			}
			return parameters != null;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0004A3B1 File Offset: 0x000485B1
		public virtual XmlElement CreateReferenceStyleAssertion(SecurityTokenReferenceStyle referenceStyle)
		{
			if (referenceStyle == SecurityTokenReferenceStyle.Internal)
			{
				return this.CreateWsspAssertion("RequireInternalReference");
			}
			if (referenceStyle != SecurityTokenReferenceStyle.External)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("referenceStyle"));
			}
			return this.CreateWsspAssertion("RequireExternalReference");
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0004A3E6 File Offset: 0x000485E6
		public virtual bool TryImportReferenceStyleAssertion(ICollection<XmlElement> assertions, IssuedSecurityTokenParameters parameters)
		{
			if (this.TryImportWsspAssertion(assertions, "RequireExternalReference"))
			{
				parameters.ReferenceStyle = SecurityTokenReferenceStyle.External;
			}
			else if (this.TryImportWsspAssertion(assertions, "RequireInternalReference"))
			{
				parameters.ReferenceStyle = SecurityTokenReferenceStyle.Internal;
			}
			return true;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0004A418 File Offset: 0x00048618
		public virtual XmlElement CreateWsspIssuerElement(EndpointAddress issuerAddress, EndpointAddress issuerMetadataAddress)
		{
			XmlElement result;
			if (issuerAddress == null && issuerMetadataAddress == null)
			{
				result = null;
			}
			else
			{
				EndpointAddress endpointAddress = (issuerAddress == null) ? EndpointAddress.AnonymousAddress : issuerAddress;
				MemoryStream memoryStream;
				XmlWriter xmlWriter;
				if (issuerMetadataAddress != null)
				{
					MetadataSet metadataSet = new MetadataSet();
					metadataSet.MetadataSections.Add(new MetadataSection(null, null, new MetadataReference(issuerMetadataAddress, AddressingVersion.WSAddressing10)));
					memoryStream = new MemoryStream();
					xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
					metadataSet.WriteTo(XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter));
					xmlWriter.Flush();
					memoryStream.Seek(0L, SeekOrigin.Begin);
					endpointAddress = new EndpointAddress(endpointAddress.Uri, endpointAddress.Identity, endpointAddress.Headers, XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(memoryStream)), endpointAddress.GetReaderAtExtensions());
				}
				memoryStream = new MemoryStream();
				xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				xmlWriter.WriteStartElement("Issuer", this.WsspNamespaceUri);
				endpointAddress.WriteContentsTo(AddressingVersion.WSAddressing10, xmlWriter);
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = (XmlElement)WSSecurityPolicy.doc.ReadNode(new XmlTextReader(memoryStream)
				{
					DtdProcessing = DtdProcessing.Prohibit
				});
			}
			return result;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0004A53C File Offset: 0x0004873C
		public virtual bool TryGetIssuer(XmlElement assertion, out EndpointAddress issuer, out EndpointAddress issuerMetadata)
		{
			bool result = true;
			issuer = null;
			issuerMetadata = null;
			foreach (object obj in assertion.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode is XmlElement && this.IsWsspAssertion((XmlElement)xmlNode, "Issuer"))
				{
					try
					{
						issuer = EndpointAddress.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(new XmlNodeReader(xmlNode)));
						XmlDictionaryReader readerAtMetadata = issuer.GetReaderAtMetadata();
						if (readerAtMetadata != null)
						{
							while (readerAtMetadata.MoveToContent() == XmlNodeType.Element)
							{
								if (readerAtMetadata.LocalName == "Metadata" && readerAtMetadata.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/mex")
								{
									MetadataSet metadataSet = MetadataSet.ReadFrom(readerAtMetadata);
									using (IEnumerator<MetadataSection> enumerator2 = metadataSet.MetadataSections.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											MetadataSection metadataSection = enumerator2.Current;
											if (metadataSection.Metadata is MetadataReference)
											{
												issuerMetadata = ((MetadataReference)metadataSection.Metadata).Address;
											}
										}
										break;
									}
								}
								readerAtMetadata.Skip();
							}
						}
						break;
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (ex is NullReferenceException)
						{
							throw;
						}
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0004A6D0 File Offset: 0x000488D0
		public virtual XmlElement CreateWsspIssuedTokenAssertion(MetadataExporter exporter, IssuedSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("IssuedToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			XmlElement xmlElement2 = this.CreateWsspIssuerElement(parameters.IssuerAddress, parameters.IssuerMetadataAddress);
			if (xmlElement2 != null)
			{
				xmlElement.AppendChild(xmlElement2);
			}
			XmlElement xmlElement3 = this.CreateWsspAssertion("RequestSecurityTokenTemplate");
			TrustDriver trustDriver = this.TrustDriver;
			foreach (XmlElement node in parameters.CreateRequestParameters(trustDriver))
			{
				xmlElement3.AppendChild(WSSecurityPolicy.doc.ImportNode(node, true));
			}
			xmlElement.AppendChild(xmlElement3);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateReferenceStyleAssertion(parameters.ReferenceStyle)
			}));
			return xmlElement;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0004A7B8 File Offset: 0x000489B8
		public virtual bool TryGetRequestSecurityTokenTemplate(XmlElement assertion, out Collection<XmlElement> requestParameters)
		{
			requestParameters = null;
			foreach (object obj in assertion.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode is XmlElement && this.IsWsspAssertion((XmlElement)xmlNode, "RequestSecurityTokenTemplate"))
				{
					requestParameters = new Collection<XmlElement>();
					foreach (object obj2 in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj2;
						if (xmlNode2 is XmlElement)
						{
							requestParameters.Add((XmlElement)xmlNode2);
						}
					}
				}
			}
			return requestParameters != null;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0004A894 File Offset: 0x00048A94
		public virtual bool TryImportWsspIssuedTokenAssertion(MetadataImporter importer, PolicyConversionContext policyContext, XmlElement assertion, out SecurityTokenParameters parameters)
		{
			parameters = null;
			SecurityTokenInclusionMode inclusionMode;
			EndpointAddress issuerAddress;
			EndpointAddress issuerMetadataAddress;
			Collection<XmlElement> requestParameters;
			if (this.IsWsspAssertion(assertion, "IssuedToken") && this.TryGetIncludeTokenValue(assertion, out inclusionMode) && this.TryGetIssuer(assertion, out issuerAddress, out issuerMetadataAddress) && this.TryGetRequestSecurityTokenTemplate(assertion, out requestParameters))
			{
				Collection<Collection<XmlElement>> collection;
				if (this.TryGetNestedPolicyAlternatives(importer, assertion, out collection))
				{
					using (IEnumerator<Collection<XmlElement>> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collection<XmlElement> collection2 = enumerator.Current;
							IssuedSecurityTokenParameters issuedSecurityTokenParameters = new IssuedSecurityTokenParameters();
							parameters = issuedSecurityTokenParameters;
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, issuedSecurityTokenParameters) && this.TryImportReferenceStyleAssertion(collection2, issuedSecurityTokenParameters) && collection2.Count == 0)
							{
								issuedSecurityTokenParameters.InclusionMode = inclusionMode;
								issuedSecurityTokenParameters.IssuerAddress = issuerAddress;
								issuedSecurityTokenParameters.IssuerMetadataAddress = issuerMetadataAddress;
								issuedSecurityTokenParameters.SetRequestParameters(requestParameters, this.TrustDriver);
								WSSecurityPolicy.TokenIssuerPolicyResolver tokenIssuerPolicyResolver = new WSSecurityPolicy.TokenIssuerPolicyResolver(this.TrustDriver);
								tokenIssuerPolicyResolver.ResolveTokenIssuerPolicy(importer, policyContext, issuedSecurityTokenParameters);
								break;
							}
							parameters = null;
						}
						goto IL_12A;
					}
				}
				IssuedSecurityTokenParameters issuedSecurityTokenParameters2 = new IssuedSecurityTokenParameters();
				parameters = issuedSecurityTokenParameters2;
				issuedSecurityTokenParameters2.InclusionMode = inclusionMode;
				issuedSecurityTokenParameters2.IssuerAddress = issuerAddress;
				issuedSecurityTokenParameters2.IssuerMetadataAddress = issuerMetadataAddress;
				issuedSecurityTokenParameters2.SetRequestParameters(requestParameters, this.TrustDriver);
				issuedSecurityTokenParameters2.RequireDerivedKeys = false;
			}
			IL_12A:
			return parameters != null;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0004A9E4 File Offset: 0x00048BE4
		public virtual XmlElement CreateWsspMustNotSendCancelAssertion(bool requireCancel)
		{
			if (!requireCancel)
			{
				return this.CreateWsspAssertion("MustNotSendCancel");
			}
			return null;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0004AA03 File Offset: 0x00048C03
		public virtual bool TryImportWsspMustNotSendCancelAssertion(ICollection<XmlElement> assertions, out bool requireCancellation)
		{
			requireCancellation = !this.TryImportWsspAssertion(assertions, "MustNotSendCancel");
			return true;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0004AA18 File Offset: 0x00048C18
		public virtual XmlElement CreateWsspSpnegoContextTokenAssertion(MetadataExporter exporter, SspiSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("SpnegoContextToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateWsspMustNotSendCancelAssertion(parameters.RequireCancellation)
			}));
			return xmlElement;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0004AA74 File Offset: 0x00048C74
		public virtual bool TryImportWsspSpnegoContextTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
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
							bool requireCancellation;
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, sspiSecurityTokenParameters) && this.TryImportWsspMustNotSendCancelAssertion(collection2, out requireCancellation) && collection2.Count == 0)
							{
								sspiSecurityTokenParameters.RequireCancellation = requireCancellation;
								sspiSecurityTokenParameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_A8;
					}
				}
				parameters = new SspiSecurityTokenParameters();
				parameters.RequireDerivedKeys = false;
				parameters.InclusionMode = inclusionMode;
			}
			IL_A8:
			return parameters != null;
		}

		// Token: 0x060013EB RID: 5099
		public abstract XmlElement CreateWsspHttpsTokenAssertion(MetadataExporter exporter, HttpsTransportBindingElement httpsBinding);

		// Token: 0x060013EC RID: 5100
		public abstract bool TryImportWsspHttpsTokenAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, HttpsTransportBindingElement httpsBinding);

		// Token: 0x060013ED RID: 5101 RVA: 0x0004AB40 File Offset: 0x00048D40
		public virtual bool ContainsWsspHttpsTokenAssertion(ICollection<XmlElement> assertions)
		{
			return PolicyConversionContext.FindAssertion(assertions, "HttpsToken", this.WsspNamespaceUri, false) != null;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0004AB57 File Offset: 0x00048D57
		public virtual XmlElement CreateMsspRequireClientCertificateAssertion(bool requireClientCertificate)
		{
			if (requireClientCertificate)
			{
				return this.CreateMsspAssertion("RequireClientCertificate");
			}
			return null;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004AB69 File Offset: 0x00048D69
		public virtual bool TryImportMsspRequireClientCertificateAssertion(ICollection<XmlElement> assertions, SslSecurityTokenParameters parameters)
		{
			parameters.RequireClientCertificate = this.TryImportMsspAssertion(assertions, "RequireClientCertificate");
			return true;
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0004AB80 File Offset: 0x00048D80
		public virtual XmlElement CreateMsspSslContextTokenAssertion(MetadataExporter exporter, SslSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateMsspAssertion("SslContextToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateWsspMustNotSendCancelAssertion(parameters.RequireCancellation),
				this.CreateMsspRequireClientCertificateAssertion(parameters.RequireClientCertificate)
			}));
			return xmlElement;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0004ABE8 File Offset: 0x00048DE8
		public virtual bool TryImportMsspSslContextTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
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
							bool requireCancellation;
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, sslSecurityTokenParameters) && this.TryImportWsspMustNotSendCancelAssertion(collection2, out requireCancellation) && this.TryImportMsspRequireClientCertificateAssertion(collection2, sslSecurityTokenParameters) && collection2.Count == 0)
							{
								sslSecurityTokenParameters.RequireCancellation = requireCancellation;
								sslSecurityTokenParameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_B3;
					}
				}
				parameters = new SslSecurityTokenParameters();
				parameters.RequireDerivedKeys = false;
				parameters.InclusionMode = inclusionMode;
			}
			IL_B3:
			return parameters != null;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004ACC0 File Offset: 0x00048EC0
		public virtual XmlElement CreateWsspBootstrapPolicyAssertion(MetadataExporter exporter, SecurityBindingElement bootstrapSecurity)
		{
			if (bootstrapSecurity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bootstrapBinding");
			}
			WSSecurityPolicy securityPolicyDriver = WSSecurityPolicy.GetSecurityPolicyDriver(bootstrapSecurity.MessageSecurityVersion);
			CustomBinding customBinding = new CustomBinding(new BindingElement[]
			{
				bootstrapSecurity
			});
			if (exporter.State.ContainsKey("SecureConversationBootstrapBindingElementsBelowSecurityKey"))
			{
				BindingElementCollection bindingElementCollection = exporter.State["SecureConversationBootstrapBindingElementsBelowSecurityKey"] as BindingElementCollection;
				if (bindingElementCollection != null)
				{
					foreach (BindingElement item in bindingElementCollection)
					{
						customBinding.Elements.Add(item);
					}
				}
			}
			PolicyConversionContext policyConversionContext = exporter.ExportPolicy(new ServiceEndpoint(WSSecurityPolicy.NullContract)
			{
				Binding = customBinding
			});
			ChannelProtectionRequirements channelProtectionRequirements = new ChannelProtectionRequirements();
			channelProtectionRequirements.IncomingEncryptionParts.AddParts(new MessagePartSpecification(true));
			channelProtectionRequirements.OutgoingEncryptionParts.AddParts(new MessagePartSpecification(true));
			channelProtectionRequirements.IncomingSignatureParts.AddParts(new MessagePartSpecification(true));
			channelProtectionRequirements.OutgoingSignatureParts.AddParts(new MessagePartSpecification(true));
			ChannelProtectionRequirements property = customBinding.GetProperty<ChannelProtectionRequirements>(new BindingParameterCollection());
			if (property != null)
			{
				channelProtectionRequirements.Add(property);
			}
			MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
			messagePartSpecification.Union(channelProtectionRequirements.IncomingEncryptionParts.ChannelParts);
			messagePartSpecification.Union(channelProtectionRequirements.OutgoingEncryptionParts.ChannelParts);
			messagePartSpecification.MakeReadOnly();
			MessagePartSpecification messagePartSpecification2 = new MessagePartSpecification();
			messagePartSpecification2.Union(channelProtectionRequirements.IncomingSignatureParts.ChannelParts);
			messagePartSpecification2.Union(channelProtectionRequirements.OutgoingSignatureParts.ChannelParts);
			messagePartSpecification2.MakeReadOnly();
			XmlElement xmlElement = this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				securityPolicyDriver.CreateWsspSignedPartsAssertion(messagePartSpecification2),
				securityPolicyDriver.CreateWsspEncryptedPartsAssertion(messagePartSpecification)
			});
			foreach (XmlElement newChild in securityPolicyDriver.FilterWsspPolicyAssertions(policyConversionContext.GetBindingAssertions()))
			{
				xmlElement.AppendChild(newChild);
			}
			XmlElement xmlElement2 = this.CreateWsspAssertion("BootstrapPolicy");
			xmlElement2.AppendChild(xmlElement);
			return xmlElement2;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004AEE8 File Offset: 0x000490E8
		public virtual ICollection<XmlElement> FilterWsspPolicyAssertions(ICollection<XmlElement> policyAssertions)
		{
			Collection<XmlElement> collection = new Collection<XmlElement>();
			foreach (XmlElement xmlElement in policyAssertions)
			{
				if (this.IsWsspAssertion(xmlElement))
				{
					collection.Add(xmlElement);
				}
			}
			return collection;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0004AF40 File Offset: 0x00049140
		public virtual bool TryImportWsspBootstrapPolicyAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, SecureConversationSecurityTokenParameters parameters)
		{
			bool result = false;
			XmlElement assertion;
			Collection<Collection<XmlElement>> policyAlternatives;
			if (!this.TryImportWsspAssertion(assertions, "BootstrapPolicy", out assertion) || !this.TryGetNestedPolicyAlternatives(importer, assertion, out policyAlternatives))
			{
				return result;
			}
			importer.State["InSecureConversationBootstrapBindingImportMode"] = "InSecureConversationBootstrapBindingImportMode";
			BindingElementCollection bindingElementCollection;
			try
			{
				bindingElementCollection = importer.ImportPolicy(WSSecurityPolicy.NullServiceEndpoint, policyAlternatives);
				if (importer.State.ContainsKey("SecureConversationBootstrapEncryptionRequirements"))
				{
					MessagePartSpecification messagePartSpecification = (MessagePartSpecification)importer.State["SecureConversationBootstrapEncryptionRequirements"];
					if (!messagePartSpecification.IsBodyIncluded)
					{
						importer.Errors.Add(new MetadataConversionError(SR.GetString("UnsupportedSecureConversationBootstrapProtectionRequirements"), false));
						bindingElementCollection = null;
					}
				}
				if (importer.State.ContainsKey("SecureConversationBootstrapSignatureRequirements"))
				{
					MessagePartSpecification messagePartSpecification2 = (MessagePartSpecification)importer.State["SecureConversationBootstrapSignatureRequirements"];
					if (!messagePartSpecification2.IsBodyIncluded)
					{
						importer.Errors.Add(new MetadataConversionError(SR.GetString("UnsupportedSecureConversationBootstrapProtectionRequirements"), false));
						bindingElementCollection = null;
					}
				}
			}
			finally
			{
				importer.State.Remove("InSecureConversationBootstrapBindingImportMode");
				if (importer.State.ContainsKey("SecureConversationBootstrapEncryptionRequirements"))
				{
					importer.State.Remove("SecureConversationBootstrapEncryptionRequirements");
				}
				if (importer.State.ContainsKey("SecureConversationBootstrapSignatureRequirements"))
				{
					importer.State.Remove("SecureConversationBootstrapSignatureRequirements");
				}
			}
			if (bindingElementCollection != null)
			{
				parameters.BootstrapSecurityBindingElement = bindingElementCollection.Find<SecurityBindingElement>();
				return true;
			}
			parameters.BootstrapSecurityBindingElement = null;
			return true;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0004B0B8 File Offset: 0x000492B8
		public virtual XmlElement CreateWsspSecureConversationTokenAssertion(MetadataExporter exporter, SecureConversationSecurityTokenParameters parameters)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("SecureConversationToken");
			this.SetIncludeTokenValue(xmlElement, parameters.InclusionMode);
			xmlElement.AppendChild(this.CreateWspPolicyWrapper(exporter, new XmlElement[]
			{
				this.CreateWsspRequireDerivedKeysAssertion(parameters.RequireDerivedKeys),
				this.CreateWsspMustNotSendCancelAssertion(parameters.RequireCancellation),
				this.CreateWsspBootstrapPolicyAssertion(exporter, parameters.BootstrapSecurityBindingElement)
			}));
			return xmlElement;
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0004B124 File Offset: 0x00049324
		public virtual bool TryImportWsspSecureConversationTokenAssertion(MetadataImporter importer, XmlElement assertion, out SecurityTokenParameters parameters)
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
							if (this.TryImportWsspRequireDerivedKeysAssertion(collection2, secureConversationSecurityTokenParameters) && this.TryImportWsspMustNotSendCancelAssertion(collection2, out requireCancellation) && this.TryImportWsspBootstrapPolicyAssertion(importer, collection2, secureConversationSecurityTokenParameters) && collection2.Count == 0)
							{
								secureConversationSecurityTokenParameters.RequireCancellation = requireCancellation;
								secureConversationSecurityTokenParameters.InclusionMode = inclusionMode;
								break;
							}
							parameters = null;
						}
						goto IL_B4;
					}
				}
				parameters = new SecureConversationSecurityTokenParameters();
				parameters.InclusionMode = inclusionMode;
				parameters.RequireDerivedKeys = false;
			}
			IL_B4:
			return parameters != null;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004B1FC File Offset: 0x000493FC
		public static bool TryGetSecurityPolicyDriver(ICollection<XmlElement> assertions, out WSSecurityPolicy securityPolicy)
		{
			WSSecurityPolicy.SecurityPolicyManager securityPolicyManager = new WSSecurityPolicy.SecurityPolicyManager();
			return securityPolicyManager.TryGetSecurityPolicyDriver(assertions, out securityPolicy);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004B218 File Offset: 0x00049418
		public static WSSecurityPolicy GetSecurityPolicyDriver(MessageSecurityVersion version)
		{
			WSSecurityPolicy.SecurityPolicyManager securityPolicyManager = new WSSecurityPolicy.SecurityPolicyManager();
			return securityPolicyManager.GetSecurityPolicyDriver(version);
		}

		// Token: 0x04001A32 RID: 6706
		public static ContractDescription NullContract = new ContractDescription("null");

		// Token: 0x04001A33 RID: 6707
		public static ServiceEndpoint NullServiceEndpoint = new ServiceEndpoint(WSSecurityPolicy.NullContract);

		// Token: 0x04001A34 RID: 6708
		public static XmlDocument doc = new XmlDocument();

		// Token: 0x04001A35 RID: 6709
		public const string WsspPrefix = "sp";

		// Token: 0x04001A36 RID: 6710
		public const string WspNamespace = "http://schemas.xmlsoap.org/ws/2004/09/policy";

		// Token: 0x04001A37 RID: 6711
		public const string Wsp15Namespace = "http://www.w3.org/ns/ws-policy";

		// Token: 0x04001A38 RID: 6712
		public const string WspPrefix = "wsp";

		// Token: 0x04001A39 RID: 6713
		public const string MsspNamespace = "http://schemas.microsoft.com/ws/2005/07/securitypolicy";

		// Token: 0x04001A3A RID: 6714
		public const string MsspPrefix = "mssp";

		// Token: 0x04001A3B RID: 6715
		public const string PolicyName = "Policy";

		// Token: 0x04001A3C RID: 6716
		public const string OptionalName = "Optional";

		// Token: 0x04001A3D RID: 6717
		public const string TrueName = "true";

		// Token: 0x04001A3E RID: 6718
		public const string FalseName = "false";

		// Token: 0x04001A3F RID: 6719
		public const string SymmetricBindingName = "SymmetricBinding";

		// Token: 0x04001A40 RID: 6720
		public const string AsymmetricBindingName = "AsymmetricBinding";

		// Token: 0x04001A41 RID: 6721
		public const string TransportBindingName = "TransportBinding";

		// Token: 0x04001A42 RID: 6722
		public const string OnlySignEntireHeadersAndBodyName = "OnlySignEntireHeadersAndBody";

		// Token: 0x04001A43 RID: 6723
		public const string ProtectionTokenName = "ProtectionToken";

		// Token: 0x04001A44 RID: 6724
		public const string InitiatorTokenName = "InitiatorToken";

		// Token: 0x04001A45 RID: 6725
		public const string RecipientTokenName = "RecipientToken";

		// Token: 0x04001A46 RID: 6726
		public const string TransportTokenName = "TransportToken";

		// Token: 0x04001A47 RID: 6727
		public const string AlgorithmSuiteName = "AlgorithmSuite";

		// Token: 0x04001A48 RID: 6728
		public const string LaxName = "Lax";

		// Token: 0x04001A49 RID: 6729
		public const string LaxTsLastName = "LaxTsLast";

		// Token: 0x04001A4A RID: 6730
		public const string LaxTsFirstName = "LaxTsFirst";

		// Token: 0x04001A4B RID: 6731
		public const string StrictName = "Strict";

		// Token: 0x04001A4C RID: 6732
		public const string IncludeTimestampName = "IncludeTimestamp";

		// Token: 0x04001A4D RID: 6733
		public const string EncryptBeforeSigningName = "EncryptBeforeSigning";

		// Token: 0x04001A4E RID: 6734
		public const string ProtectTokens = "ProtectTokens";

		// Token: 0x04001A4F RID: 6735
		public const string EncryptSignatureName = "EncryptSignature";

		// Token: 0x04001A50 RID: 6736
		public const string SignedSupportingTokensName = "SignedSupportingTokens";

		// Token: 0x04001A51 RID: 6737
		public const string EndorsingSupportingTokensName = "EndorsingSupportingTokens";

		// Token: 0x04001A52 RID: 6738
		public const string SignedEndorsingSupportingTokensName = "SignedEndorsingSupportingTokens";

		// Token: 0x04001A53 RID: 6739
		public const string Wss10Name = "Wss10";

		// Token: 0x04001A54 RID: 6740
		public const string MustSupportRefKeyIdentifierName = "MustSupportRefKeyIdentifier";

		// Token: 0x04001A55 RID: 6741
		public const string MustSupportRefIssuerSerialName = "MustSupportRefIssuerSerial";

		// Token: 0x04001A56 RID: 6742
		public const string MustSupportRefThumbprintName = "MustSupportRefThumbprint";

		// Token: 0x04001A57 RID: 6743
		public const string MustSupportRefEncryptedKeyName = "MustSupportRefEncryptedKey";

		// Token: 0x04001A58 RID: 6744
		public const string RequireSignatureConfirmationName = "RequireSignatureConfirmation";

		// Token: 0x04001A59 RID: 6745
		public const string MustSupportIssuedTokensName = "MustSupportIssuedTokens";

		// Token: 0x04001A5A RID: 6746
		public const string RequireClientEntropyName = "RequireClientEntropy";

		// Token: 0x04001A5B RID: 6747
		public const string RequireServerEntropyName = "RequireServerEntropy";

		// Token: 0x04001A5C RID: 6748
		public const string Wss11Name = "Wss11";

		// Token: 0x04001A5D RID: 6749
		public const string Trust10Name = "Trust10";

		// Token: 0x04001A5E RID: 6750
		public const string Trust13Name = "Trust13";

		// Token: 0x04001A5F RID: 6751
		public const string RequireAppliesTo = "RequireAppliesTo";

		// Token: 0x04001A60 RID: 6752
		public const string SignedPartsName = "SignedParts";

		// Token: 0x04001A61 RID: 6753
		public const string EncryptedPartsName = "EncryptedParts";

		// Token: 0x04001A62 RID: 6754
		public const string BodyName = "Body";

		// Token: 0x04001A63 RID: 6755
		public const string HeaderName = "Header";

		// Token: 0x04001A64 RID: 6756
		public const string NameName = "Name";

		// Token: 0x04001A65 RID: 6757
		public const string NamespaceName = "Namespace";

		// Token: 0x04001A66 RID: 6758
		public const string Basic128Name = "Basic128";

		// Token: 0x04001A67 RID: 6759
		public const string Basic192Name = "Basic192";

		// Token: 0x04001A68 RID: 6760
		public const string Basic256Name = "Basic256";

		// Token: 0x04001A69 RID: 6761
		public const string TripleDesName = "TripleDes";

		// Token: 0x04001A6A RID: 6762
		public const string Basic128Rsa15Name = "Basic128Rsa15";

		// Token: 0x04001A6B RID: 6763
		public const string Basic192Rsa15Name = "Basic192Rsa15";

		// Token: 0x04001A6C RID: 6764
		public const string Basic256Rsa15Name = "Basic256Rsa15";

		// Token: 0x04001A6D RID: 6765
		public const string TripleDesRsa15Name = "TripleDesRsa15";

		// Token: 0x04001A6E RID: 6766
		public const string Basic128Sha256Name = "Basic128Sha256";

		// Token: 0x04001A6F RID: 6767
		public const string Basic192Sha256Name = "Basic192Sha256";

		// Token: 0x04001A70 RID: 6768
		public const string Basic256Sha256Name = "Basic256Sha256";

		// Token: 0x04001A71 RID: 6769
		public const string TripleDesSha256Name = "TripleDesSha256";

		// Token: 0x04001A72 RID: 6770
		public const string Basic128Sha256Rsa15Name = "Basic128Sha256Rsa15";

		// Token: 0x04001A73 RID: 6771
		public const string Basic192Sha256Rsa15Name = "Basic192Sha256Rsa15";

		// Token: 0x04001A74 RID: 6772
		public const string Basic256Sha256Rsa15Name = "Basic256Sha256Rsa15";

		// Token: 0x04001A75 RID: 6773
		public const string TripleDesSha256Rsa15Name = "TripleDesSha256Rsa15";

		// Token: 0x04001A76 RID: 6774
		public const string IncludeTokenName = "IncludeToken";

		// Token: 0x04001A77 RID: 6775
		public const string KerberosTokenName = "KerberosToken";

		// Token: 0x04001A78 RID: 6776
		public const string X509TokenName = "X509Token";

		// Token: 0x04001A79 RID: 6777
		public const string IssuedTokenName = "IssuedToken";

		// Token: 0x04001A7A RID: 6778
		public const string UsernameTokenName = "UsernameToken";

		// Token: 0x04001A7B RID: 6779
		public const string RsaTokenName = "RsaToken";

		// Token: 0x04001A7C RID: 6780
		public const string KeyValueTokenName = "KeyValueToken";

		// Token: 0x04001A7D RID: 6781
		public const string SpnegoContextTokenName = "SpnegoContextToken";

		// Token: 0x04001A7E RID: 6782
		public const string SslContextTokenName = "SslContextToken";

		// Token: 0x04001A7F RID: 6783
		public const string SecureConversationTokenName = "SecureConversationToken";

		// Token: 0x04001A80 RID: 6784
		public const string WssGssKerberosV5ApReqToken11Name = "WssGssKerberosV5ApReqToken11";

		// Token: 0x04001A81 RID: 6785
		public const string RequireDerivedKeysName = "RequireDerivedKeys";

		// Token: 0x04001A82 RID: 6786
		public const string RequireIssuerSerialReferenceName = "RequireIssuerSerialReference";

		// Token: 0x04001A83 RID: 6787
		public const string RequireKeyIdentifierReferenceName = "RequireKeyIdentifierReference";

		// Token: 0x04001A84 RID: 6788
		public const string RequireThumbprintReferenceName = "RequireThumbprintReference";

		// Token: 0x04001A85 RID: 6789
		public const string WssX509V3Token10Name = "WssX509V3Token10";

		// Token: 0x04001A86 RID: 6790
		public const string WssUsernameToken10Name = "WssUsernameToken10";

		// Token: 0x04001A87 RID: 6791
		public const string RequestSecurityTokenTemplateName = "RequestSecurityTokenTemplate";

		// Token: 0x04001A88 RID: 6792
		public const string RequireExternalReferenceName = "RequireExternalReference";

		// Token: 0x04001A89 RID: 6793
		public const string RequireInternalReferenceName = "RequireInternalReference";

		// Token: 0x04001A8A RID: 6794
		public const string IssuerName = "Issuer";

		// Token: 0x04001A8B RID: 6795
		public const string RequireClientCertificateName = "RequireClientCertificate";

		// Token: 0x04001A8C RID: 6796
		public const string MustNotSendCancelName = "MustNotSendCancel";

		// Token: 0x04001A8D RID: 6797
		public const string MustNotSendAmendName = "MustNotSendAmend";

		// Token: 0x04001A8E RID: 6798
		public const string MustNotSendRenewName = "MustNotSendRenew";

		// Token: 0x04001A8F RID: 6799
		public const string LayoutName = "Layout";

		// Token: 0x04001A90 RID: 6800
		public const string BootstrapPolicyName = "BootstrapPolicy";

		// Token: 0x04001A91 RID: 6801
		public const string HttpsTokenName = "HttpsToken";

		// Token: 0x04001A92 RID: 6802
		public const string HttpBasicAuthenticationName = "HttpBasicAuthentication";

		// Token: 0x04001A93 RID: 6803
		public const string HttpDigestAuthenticationName = "HttpDigestAuthentication";

		// Token: 0x04001A94 RID: 6804
		private bool _mustSupportRefKeyIdentifierName;

		// Token: 0x04001A95 RID: 6805
		private bool _mustSupportRefIssuerSerialName;

		// Token: 0x04001A96 RID: 6806
		private bool _mustSupportRefThumbprintName;

		// Token: 0x04001A97 RID: 6807
		private bool _protectionTokenHasAsymmetricKey;

		// Token: 0x02000B2E RID: 2862
		private class TokenIssuerPolicyResolver
		{
			// Token: 0x06007021 RID: 28705 RVA: 0x0019FC42 File Offset: 0x0019DE42
			public TokenIssuerPolicyResolver(TrustDriver driver)
			{
				this.trustDriver = driver;
			}

			// Token: 0x06007022 RID: 28706 RVA: 0x0019FC54 File Offset: 0x0019DE54
			public void ResolveTokenIssuerPolicy(MetadataImporter importer, PolicyConversionContext policyContext, IssuedSecurityTokenParameters parameters)
			{
				if (policyContext == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
				}
				if (parameters == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
				}
				EndpointAddress endpointAddress = (parameters.IssuerMetadataAddress != null) ? parameters.IssuerMetadataAddress : parameters.IssuerAddress;
				if (endpointAddress == null || endpointAddress.IsAnonymous || endpointAddress.Uri.Equals(WSSecurityPolicy.TokenIssuerPolicyResolver.SelfIssuerUri))
				{
					return;
				}
				int num = (int)importer.State["MaxPolicyRedirections"];
				if (num <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MaximumPolicyRedirectionsExceeded")));
				}
				num--;
				MetadataExchangeClient metadataExchangeClient = null;
				if (importer.State != null && importer.State.ContainsKey("MetadataExchangeClientKey"))
				{
					metadataExchangeClient = (importer.State["MetadataExchangeClientKey"] as MetadataExchangeClient);
				}
				if (metadataExchangeClient == null)
				{
					metadataExchangeClient = new MetadataExchangeClient(endpointAddress);
				}
				MetadataSet metadataSet = null;
				Exception ex = null;
				try
				{
					metadataSet = metadataExchangeClient.GetMetadata(endpointAddress);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (ex2 is NullReferenceException)
					{
						throw;
					}
					ex = ex2;
				}
				if (metadataSet == null)
				{
					try
					{
						metadataSet = metadataExchangeClient.GetMetadata(endpointAddress.Uri, MetadataExchangeClientMode.HttpGet);
					}
					catch (Exception ex3)
					{
						if (Fx.IsFatal(ex3))
						{
							throw;
						}
						if (ex3 is NullReferenceException)
						{
							throw;
						}
						if (ex == null)
						{
							ex = ex3;
						}
					}
				}
				if (metadataSet == null)
				{
					if (ex != null)
					{
						importer.Errors.Add(new MetadataConversionError(SR.GetString("UnableToObtainIssuerMetadata", new object[]
						{
							endpointAddress,
							ex
						}), false));
					}
					return;
				}
				WsdlImporter wsdlImporter = importer as WsdlImporter;
				WsdlImporter wsdlImporter2;
				if (wsdlImporter != null)
				{
					wsdlImporter2 = new WsdlImporter(metadataSet, importer.PolicyImportExtensions, wsdlImporter.WsdlImportExtensions);
				}
				else
				{
					wsdlImporter2 = new WsdlImporter(metadataSet, importer.PolicyImportExtensions, null);
				}
				if (importer.State != null && importer.State.ContainsKey("MetadataExchangeClientKey"))
				{
					wsdlImporter2.State.Add("MetadataExchangeClientKey", importer.State["MetadataExchangeClientKey"]);
				}
				wsdlImporter2.State.Add("MaxPolicyRedirections", num);
				ServiceEndpointCollection serviceEndpointCollection = wsdlImporter2.ImportAllEndpoints();
				for (int i = 0; i < wsdlImporter2.Errors.Count; i++)
				{
					MetadataConversionError metadataConversionError = wsdlImporter2.Errors[i];
					importer.Errors.Add(new MetadataConversionError(SR.GetString("ErrorImportingIssuerMetadata", new object[]
					{
						endpointAddress,
						WSSecurityPolicy.TokenIssuerPolicyResolver.InsertEllipsisIfTooLong(metadataConversionError.Message)
					}), metadataConversionError.IsWarning));
				}
				if (serviceEndpointCollection != null)
				{
					this.AddCompatibleFederationEndpoints(serviceEndpointCollection, parameters);
					if (parameters.AlternativeIssuerEndpoints != null && parameters.AlternativeIssuerEndpoints.Count > 0)
					{
						importer.Errors.Add(new MetadataConversionError(SR.GetString("MultipleIssuerEndpointsFound", new object[]
						{
							endpointAddress
						})));
					}
				}
			}

			// Token: 0x06007023 RID: 28707 RVA: 0x0019FF2C File Offset: 0x0019E12C
			private static string InsertEllipsisIfTooLong(string message)
			{
				if (message != null && message.Length > 1024)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
					{
						message.Substring(0, (1024 - "....".Length) / 2),
						"....",
						message.Substring(message.Length - (1024 - "....".Length) / 2)
					});
				}
				return message;
			}

			// Token: 0x06007024 RID: 28708 RVA: 0x0019FFA8 File Offset: 0x0019E1A8
			private void AddCompatibleFederationEndpoints(ServiceEndpointCollection serviceEndpoints, IssuedSecurityTokenParameters parameters)
			{
				bool flag = parameters.IssuerAddress != null && !parameters.IssuerAddress.IsAnonymous;
				foreach (ServiceEndpoint serviceEndpoint in serviceEndpoints)
				{
					TrustDriver trustDriver;
					if (!this.TryGetTrustDriver(serviceEndpoint, out trustDriver))
					{
						trustDriver = this.trustDriver;
					}
					bool flag2 = false;
					ContractDescription contract = serviceEndpoint.Contract;
					for (int i = 0; i < contract.Operations.Count; i++)
					{
						OperationDescription operationDescription = contract.Operations[i];
						bool flag3 = false;
						bool flag4 = false;
						for (int j = 0; j < operationDescription.Messages.Count; j++)
						{
							MessageDescription messageDescription = operationDescription.Messages[j];
							if (messageDescription.Action == trustDriver.RequestSecurityTokenAction.Value && messageDescription.Direction == MessageDirection.Input)
							{
								flag3 = true;
							}
							else if (((trustDriver.StandardsManager.TrustVersion == TrustVersion.WSTrustFeb2005 && messageDescription.Action == trustDriver.RequestSecurityTokenResponseAction.Value) || (trustDriver.StandardsManager.TrustVersion == TrustVersion.WSTrust13 && messageDescription.Action == trustDriver.RequestSecurityTokenResponseFinalAction.Value)) && messageDescription.Direction == MessageDirection.Output)
							{
								flag4 = true;
							}
						}
						if (flag3 && flag4)
						{
							flag2 = true;
							break;
						}
					}
					if (flag2 && (!flag || parameters.IssuerAddress.Uri.Equals(serviceEndpoint.Address.Uri)))
					{
						if (parameters.IssuerBinding == null)
						{
							parameters.IssuerAddress = serviceEndpoint.Address;
							parameters.IssuerBinding = serviceEndpoint.Binding;
						}
						else
						{
							IssuedSecurityTokenParameters.AlternativeIssuerEndpoint item = default(IssuedSecurityTokenParameters.AlternativeIssuerEndpoint);
							item.IssuerAddress = serviceEndpoint.Address;
							item.IssuerBinding = serviceEndpoint.Binding;
							parameters.AlternativeIssuerEndpoints.Add(item);
						}
					}
				}
			}

			// Token: 0x06007025 RID: 28709 RVA: 0x001A01AC File Offset: 0x0019E3AC
			private bool TryGetTrustDriver(ServiceEndpoint endpoint, out TrustDriver trustDriver)
			{
				SecurityBindingElement securityBindingElement = endpoint.Binding.CreateBindingElements().Find<SecurityBindingElement>();
				trustDriver = null;
				if (securityBindingElement != null)
				{
					MessageSecurityVersion messageSecurityVersion = securityBindingElement.MessageSecurityVersion;
					if (messageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005)
					{
						trustDriver = new WSTrustFeb2005.DriverFeb2005(new SecurityStandardsManager(messageSecurityVersion, WSSecurityTokenSerializer.DefaultInstance));
					}
					else if (messageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
					{
						trustDriver = new WSTrustDec2005.DriverDec2005(new SecurityStandardsManager(messageSecurityVersion, WSSecurityTokenSerializer.DefaultInstance));
					}
				}
				return trustDriver != null;
			}

			// Token: 0x04003FF8 RID: 16376
			private const string WSIdentityNamespace = "http://schemas.xmlsoap.org/ws/2005/05/identity";

			// Token: 0x04003FF9 RID: 16377
			private static readonly Uri SelfIssuerUri = new Uri("http://schemas.xmlsoap.org/ws/2005/05/identity/issuer/self");

			// Token: 0x04003FFA RID: 16378
			private TrustDriver trustDriver;
		}

		// Token: 0x02000B2F RID: 2863
		private class SecurityPolicyManager
		{
			// Token: 0x06007027 RID: 28711 RVA: 0x001A022D File Offset: 0x0019E42D
			public SecurityPolicyManager()
			{
				this.drivers = new List<WSSecurityPolicy>();
				this.Initialize();
			}

			// Token: 0x06007028 RID: 28712 RVA: 0x001A0246 File Offset: 0x0019E446
			public void Initialize()
			{
				this.drivers.Add(new WSSecurityPolicy11());
				this.drivers.Add(new WSSecurityPolicy12());
			}

			// Token: 0x06007029 RID: 28713 RVA: 0x001A0268 File Offset: 0x0019E468
			public bool TryGetSecurityPolicyDriver(ICollection<XmlElement> assertions, out WSSecurityPolicy securityPolicy)
			{
				securityPolicy = null;
				for (int i = 0; i < this.drivers.Count; i++)
				{
					if (this.drivers[i].CanImportAssertion(assertions))
					{
						securityPolicy = this.drivers[i];
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600702A RID: 28714 RVA: 0x001A02B4 File Offset: 0x0019E4B4
			public WSSecurityPolicy GetSecurityPolicyDriver(MessageSecurityVersion version)
			{
				for (int i = 0; i < this.drivers.Count; i++)
				{
					if (this.drivers[i].IsSecurityVersionSupported(version))
					{
						return this.drivers[i];
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}

			// Token: 0x04003FFB RID: 16379
			private List<WSSecurityPolicy> drivers;
		}
	}
}
