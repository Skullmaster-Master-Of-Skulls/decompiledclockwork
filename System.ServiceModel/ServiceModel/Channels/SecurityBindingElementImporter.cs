using System;
using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200098F RID: 2447
	public class SecurityBindingElementImporter : IPolicyImportExtension
	{
		// Token: 0x06005EE5 RID: 24293 RVA: 0x0015F300 File Offset: 0x0015D500
		public SecurityBindingElementImporter()
		{
			this.maxPolicyRedirections = 10;
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06005EE6 RID: 24294 RVA: 0x0015F310 File Offset: 0x0015D510
		public int MaxPolicyRedirections
		{
			get
			{
				return this.maxPolicyRedirections;
			}
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x0015F318 File Offset: 0x0015D518
		private void ImportOperationScopeSupportingTokensPolicy(MetadataImporter importer, PolicyConversionContext policyContext, SecurityBindingElement binding)
		{
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				string text = null;
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					if (messageDescription.Direction == MessageDirection.Input)
					{
						text = messageDescription.Action;
						break;
					}
				}
				SupportingTokenParameters supportingTokenParameters = new SupportingTokenParameters();
				SupportingTokenParameters supportingTokenParameters2 = new SupportingTokenParameters();
				ICollection<XmlElement> operationBindingAssertions = policyContext.GetOperationBindingAssertions(operationDescription);
				this.ImportSupportingTokenAssertions(importer, policyContext, operationBindingAssertions, supportingTokenParameters, supportingTokenParameters2);
				if (supportingTokenParameters.Endorsing.Count > 0 || supportingTokenParameters.Signed.Count > 0 || supportingTokenParameters.SignedEncrypted.Count > 0 || supportingTokenParameters.SignedEndorsing.Count > 0)
				{
					if (text == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotImportSupportingTokensForOperationWithoutRequestAction")));
					}
					binding.OperationSupportingTokenParameters[text] = supportingTokenParameters;
				}
				if (supportingTokenParameters2.Endorsing.Count > 0 || supportingTokenParameters2.Signed.Count > 0 || supportingTokenParameters2.SignedEncrypted.Count > 0 || supportingTokenParameters2.SignedEndorsing.Count > 0)
				{
					if (text == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CannotImportSupportingTokensForOperationWithoutRequestAction")));
					}
					binding.OptionalOperationSupportingTokenParameters[text] = supportingTokenParameters2;
				}
			}
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x0015F4C4 File Offset: 0x0015D6C4
		private void ImportProtectionAssertions(ICollection<XmlElement> assertions, out MessagePartSpecification signedParts, out MessagePartSpecification encryptedParts)
		{
			signedParts = null;
			encryptedParts = null;
			WSSecurityPolicy wssecurityPolicy;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(assertions, out wssecurityPolicy))
			{
				XmlElement xmlElement;
				if (!wssecurityPolicy.TryImportWsspEncryptedPartsAssertion(assertions, out encryptedParts, out xmlElement) && xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
				if (!wssecurityPolicy.TryImportWsspSignedPartsAssertion(assertions, out signedParts, out xmlElement) && xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
			}
			if (encryptedParts == null)
			{
				encryptedParts = MessagePartSpecification.NoParts;
			}
			if (signedParts == null)
			{
				signedParts = MessagePartSpecification.NoParts;
			}
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x0015F568 File Offset: 0x0015D768
		private void ValidateExistingOrSetNewProtectionLevel(MessagePartDescription part, MessageDescription message, OperationDescription operation, ContractDescription contract, ProtectionLevel newProtectionLevel)
		{
			ProtectionLevel protectionLevel;
			if (part != null && part.HasProtectionLevel)
			{
				protectionLevel = part.ProtectionLevel;
			}
			else if (message.HasProtectionLevel)
			{
				protectionLevel = message.ProtectionLevel;
			}
			else if (operation.HasProtectionLevel)
			{
				protectionLevel = operation.ProtectionLevel;
			}
			else
			{
				if (part != null)
				{
					part.ProtectionLevel = newProtectionLevel;
				}
				else
				{
					message.ProtectionLevel = newProtectionLevel;
				}
				protectionLevel = newProtectionLevel;
			}
			if (protectionLevel == newProtectionLevel)
			{
				return;
			}
			if (part != null && !part.HasProtectionLevel)
			{
				part.ProtectionLevel = newProtectionLevel;
				return;
			}
			if (part == null && !message.HasProtectionLevel)
			{
				message.ProtectionLevel = newProtectionLevel;
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("CannotImportProtectionLevelForContract", new object[]
			{
				contract.Name,
				contract.Namespace
			})));
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x0015F624 File Offset: 0x0015D824
		private void AddParts(ref MessagePartSpecification parts1, MessagePartSpecification parts2)
		{
			if (parts1 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parts1"));
			}
			if (parts2 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("parts2"));
			}
			if (!parts2.IsEmpty())
			{
				if (parts1.IsReadOnly)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification();
					messagePartSpecification.Union(parts1);
					messagePartSpecification.Union(parts2);
					parts1 = messagePartSpecification;
					return;
				}
				parts1.Union(parts2);
			}
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x0015F694 File Offset: 0x0015D894
		private void ImportMessageScopeProtectionPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			SecurityBindingElementImporter.ContractProtectionLevel contractProtectionLevel = null;
			bool flag = false;
			bool flag2 = true;
			ProtectionLevel protectionLevel = ProtectionLevel.None;
			string key = string.Format("{0}:{1}:{2}", "ContractProtectionLevelKey", policyContext.Contract.Name, policyContext.Contract.Namespace);
			bool flag3;
			if (importer.State.ContainsKey(key))
			{
				flag3 = true;
				contractProtectionLevel = (SecurityBindingElementImporter.ContractProtectionLevel)importer.State[key];
			}
			else
			{
				flag3 = false;
			}
			ICollection<XmlElement> bindingAssertions = policyContext.GetBindingAssertions();
			MessagePartSpecification messagePartSpecification;
			MessagePartSpecification messagePartSpecification2;
			this.ImportProtectionAssertions(bindingAssertions, out messagePartSpecification, out messagePartSpecification2);
			if (importer.State.ContainsKey("InSecureConversationBootstrapBindingImportMode"))
			{
				if (messagePartSpecification2 != null)
				{
					importer.State["SecureConversationBootstrapEncryptionRequirements"] = messagePartSpecification2;
				}
				if (messagePartSpecification != null)
				{
					importer.State["SecureConversationBootstrapSignatureRequirements"] = messagePartSpecification;
				}
			}
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				ICollection<XmlElement> operationBindingAssertions = policyContext.GetOperationBindingAssertions(operationDescription);
				MessagePartSpecification parts;
				MessagePartSpecification parts2;
				this.ImportProtectionAssertions(operationBindingAssertions, out parts, out parts2);
				this.AddParts(ref parts, messagePartSpecification);
				this.AddParts(ref parts2, messagePartSpecification2);
				bool flag4 = false;
				bool flag5 = true;
				ProtectionLevel protectionLevel2 = ProtectionLevel.None;
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					ICollection<XmlElement> messageBindingAssertions = policyContext.GetMessageBindingAssertions(messageDescription);
					MessagePartSpecification messagePartSpecification3;
					MessagePartSpecification messagePartSpecification4;
					this.ImportProtectionAssertions(messageBindingAssertions, out messagePartSpecification3, out messagePartSpecification4);
					this.AddParts(ref messagePartSpecification3, parts);
					this.AddParts(ref messagePartSpecification4, parts2);
					ProtectionLevel protectionLevel3 = SecurityBindingElementImporter.GetProtectionLevel(messagePartSpecification3.IsBodyIncluded, messagePartSpecification4.IsBodyIncluded, messageDescription.Action);
					if (OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue))
					{
						this.ValidateExistingOrSetNewProtectionLevel(messageDescription.Body.ReturnValue, messageDescription, operationDescription, policyContext.Contract, protectionLevel3);
					}
					foreach (MessagePartDescription part in messageDescription.Body.Parts)
					{
						this.ValidateExistingOrSetNewProtectionLevel(part, messageDescription, operationDescription, policyContext.Contract, protectionLevel3);
					}
					if (!OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue) || messageDescription.Body.Parts.Count == 0)
					{
						this.ValidateExistingOrSetNewProtectionLevel(null, messageDescription, operationDescription, policyContext.Contract, protectionLevel3);
					}
					if (flag4)
					{
						if (protectionLevel2 != protectionLevel3)
						{
							flag5 = false;
						}
					}
					else
					{
						protectionLevel2 = protectionLevel3;
						flag4 = true;
					}
					if (flag)
					{
						if (protectionLevel != protectionLevel3)
						{
							flag2 = false;
						}
					}
					else
					{
						protectionLevel = protectionLevel3;
						flag = true;
					}
					foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
					{
						bool signed = messagePartSpecification3.IsHeaderIncluded(messageHeaderDescription.Name, messageHeaderDescription.Namespace);
						bool encrypted = messagePartSpecification4.IsHeaderIncluded(messageHeaderDescription.Name, messageHeaderDescription.Namespace);
						protectionLevel3 = SecurityBindingElementImporter.GetProtectionLevel(signed, encrypted, messageDescription.Action);
						this.ValidateExistingOrSetNewProtectionLevel(messageHeaderDescription, messageDescription, operationDescription, policyContext.Contract, protectionLevel3);
						if (flag4)
						{
							if (protectionLevel2 != protectionLevel3)
							{
								flag5 = false;
							}
						}
						else
						{
							protectionLevel2 = protectionLevel3;
							flag4 = true;
						}
						if (flag)
						{
							if (protectionLevel != protectionLevel3)
							{
								flag2 = false;
							}
						}
						else
						{
							protectionLevel = protectionLevel3;
							flag = true;
						}
					}
				}
				if (flag4 && flag5)
				{
					this.ResetProtectionLevelForMessages(operationDescription);
					operationDescription.ProtectionLevel = protectionLevel2;
				}
				foreach (FaultDescription faultDescription in operationDescription.Faults)
				{
					ICollection<XmlElement> faultBindingAssertions = policyContext.GetFaultBindingAssertions(faultDescription);
					MessagePartSpecification messagePartSpecification3;
					MessagePartSpecification messagePartSpecification4;
					this.ImportProtectionAssertions(faultBindingAssertions, out messagePartSpecification3, out messagePartSpecification4);
					this.AddParts(ref messagePartSpecification3, parts);
					this.AddParts(ref messagePartSpecification4, parts2);
					ProtectionLevel protectionLevel4 = SecurityBindingElementImporter.GetProtectionLevel(messagePartSpecification3.IsBodyIncluded, messagePartSpecification4.IsBodyIncluded, faultDescription.Action);
					if (faultDescription.HasProtectionLevel)
					{
						if (faultDescription.ProtectionLevel != protectionLevel4)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("CannotImportProtectionLevelForContract", new object[]
							{
								policyContext.Contract.Name,
								policyContext.Contract.Namespace
							})));
						}
					}
					else
					{
						faultDescription.ProtectionLevel = protectionLevel4;
					}
					if (flag)
					{
						if (protectionLevel != protectionLevel4)
						{
							flag2 = false;
						}
					}
					else
					{
						protectionLevel = protectionLevel4;
						flag = true;
					}
				}
			}
			if (flag3)
			{
				if (flag != contractProtectionLevel.HasProtectionRequirements || flag2 != contractProtectionLevel.HasUniformProtectionLevel || protectionLevel != contractProtectionLevel.UniformProtectionLevel)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("CannotImportProtectionLevelForContract", new object[]
					{
						policyContext.Contract.Name,
						policyContext.Contract.Namespace
					})));
				}
			}
			else
			{
				if (flag && flag2 && protectionLevel == ProtectionLevel.EncryptAndSign)
				{
					foreach (OperationDescription operationDescription2 in policyContext.Contract.Operations)
					{
						this.ResetProtectionLevelForMessages(operationDescription2);
						foreach (FaultDescription faultDescription2 in operationDescription2.Faults)
						{
							faultDescription2.ResetProtectionLevel();
						}
						operationDescription2.ResetProtectionLevel();
					}
				}
				importer.State[key] = new SecurityBindingElementImporter.ContractProtectionLevel(flag, flag2, protectionLevel);
			}
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x0015FC74 File Offset: 0x0015DE74
		private void ResetProtectionLevelForMessages(OperationDescription operation)
		{
			foreach (MessageDescription messageDescription in operation.Messages)
			{
				if (OperationFormatter.IsValidReturnValue(messageDescription.Body.ReturnValue))
				{
					messageDescription.Body.ReturnValue.ResetProtectionLevel();
				}
				foreach (MessagePartDescription messagePartDescription in messageDescription.Body.Parts)
				{
					messagePartDescription.ResetProtectionLevel();
				}
				foreach (MessageHeaderDescription messageHeaderDescription in messageDescription.Headers)
				{
					messageHeaderDescription.ResetProtectionLevel();
				}
				messageDescription.ResetProtectionLevel();
			}
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x0015FD6C File Offset: 0x0015DF6C
		private static ProtectionLevel GetProtectionLevel(bool signed, bool encrypted, string action)
		{
			ProtectionLevel result;
			if (encrypted)
			{
				if (!signed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("PolicyRequiresConfidentialityWithoutIntegrity", new object[]
					{
						action
					})));
				}
				result = ProtectionLevel.EncryptAndSign;
			}
			else if (signed)
			{
				result = ProtectionLevel.Sign;
			}
			else
			{
				result = ProtectionLevel.None;
			}
			return result;
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x0015FDB4 File Offset: 0x0015DFB4
		private void ImportSupportingTokenAssertions(MetadataImporter importer, PolicyConversionContext policyContext, ICollection<XmlElement> assertions, SupportingTokenParameters requirements, SupportingTokenParameters optionalRequirements)
		{
			WSSecurityPolicy wssecurityPolicy;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(assertions, out wssecurityPolicy))
			{
				wssecurityPolicy.TryImportWsspSupportingTokensAssertion(importer, policyContext, assertions, requirements.Signed, requirements.SignedEncrypted, requirements.Endorsing, requirements.SignedEndorsing, optionalRequirements.Signed, optionalRequirements.SignedEncrypted, optionalRequirements.Endorsing, optionalRequirements.SignedEndorsing);
			}
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x0015FE10 File Offset: 0x0015E010
		private void ImportEndpointScopeMessageBindingAssertions(MetadataImporter importer, PolicyConversionContext policyContext, SecurityBindingElement binding)
		{
			XmlElement xmlElement = null;
			this.ImportSupportingTokenAssertions(importer, policyContext, policyContext.GetBindingAssertions(), binding.EndpointSupportingTokenParameters, binding.OptionalEndpointSupportingTokenParameters);
			WSSecurityPolicy wssecurityPolicy;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(policyContext.GetBindingAssertions(), out wssecurityPolicy))
			{
				if (!wssecurityPolicy.TryImportWsspWssAssertion(importer, policyContext.GetBindingAssertions(), binding, out xmlElement) && xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
				if (!wssecurityPolicy.TryImportWsspTrustAssertion(importer, policyContext.GetBindingAssertions(), binding, out xmlElement) && xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
			}
			if (xmlElement == null)
			{
				binding.DoNotEmitTrust = true;
			}
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x0015FED0 File Offset: 0x0015E0D0
		private bool TryImportSymmetricSecurityBindingElement(MetadataImporter importer, PolicyConversionContext policyContext, out SecurityBindingElement sbe)
		{
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = null;
			WSSecurityPolicy wssecurityPolicy;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(policyContext.GetBindingAssertions(), out wssecurityPolicy))
			{
				XmlElement xmlElement;
				if (wssecurityPolicy.TryImportWsspSymmetricBindingAssertion(importer, policyContext, policyContext.GetBindingAssertions(), out symmetricSecurityBindingElement, out xmlElement))
				{
					this.ImportEndpointScopeMessageBindingAssertions(importer, policyContext, symmetricSecurityBindingElement);
					this.ImportOperationScopeSupportingTokensPolicy(importer, policyContext, symmetricSecurityBindingElement);
					this.ImportMessageScopeProtectionPolicy(importer, policyContext);
					policyContext.BindingElements.Add(symmetricSecurityBindingElement);
				}
				else if (xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
			}
			sbe = symmetricSecurityBindingElement;
			return symmetricSecurityBindingElement != null;
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x0015FF60 File Offset: 0x0015E160
		private bool TryImportAsymmetricSecurityBindingElement(MetadataImporter importer, PolicyConversionContext policyContext, out SecurityBindingElement sbe)
		{
			AsymmetricSecurityBindingElement asymmetricSecurityBindingElement = null;
			WSSecurityPolicy wssecurityPolicy;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(policyContext.GetBindingAssertions(), out wssecurityPolicy))
			{
				XmlElement xmlElement;
				if (wssecurityPolicy.TryImportWsspAsymmetricBindingAssertion(importer, policyContext, policyContext.GetBindingAssertions(), out asymmetricSecurityBindingElement, out xmlElement))
				{
					this.ImportEndpointScopeMessageBindingAssertions(importer, policyContext, asymmetricSecurityBindingElement);
					this.ImportOperationScopeSupportingTokensPolicy(importer, policyContext, asymmetricSecurityBindingElement);
					this.ImportMessageScopeProtectionPolicy(importer, policyContext);
					policyContext.BindingElements.Add(asymmetricSecurityBindingElement);
				}
				else if (xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
			}
			sbe = asymmetricSecurityBindingElement;
			return asymmetricSecurityBindingElement != null;
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x0015FFF0 File Offset: 0x0015E1F0
		private bool TryImportTransportSecurityBindingElement(MetadataImporter importer, PolicyConversionContext policyContext, out SecurityBindingElement sbe, bool isDualSecurityModeOnly)
		{
			TransportSecurityBindingElement transportSecurityBindingElement = null;
			sbe = null;
			WSSecurityPolicy wssecurityPolicy;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(policyContext.GetBindingAssertions(), out wssecurityPolicy))
			{
				XmlElement xmlElement;
				if (wssecurityPolicy.TryImportWsspTransportBindingAssertion(importer, policyContext.GetBindingAssertions(), out transportSecurityBindingElement, out xmlElement))
				{
					this.ImportEndpointScopeMessageBindingAssertions(importer, policyContext, transportSecurityBindingElement);
					if (!isDualSecurityModeOnly)
					{
						this.ImportOperationScopeSupportingTokensPolicy(importer, policyContext, transportSecurityBindingElement);
						if (importer.State.ContainsKey("InSecureConversationBootstrapBindingImportMode"))
						{
							this.ImportMessageScopeProtectionPolicy(importer, policyContext);
						}
						if (SecurityBindingElementImporter.HasSupportingTokens(transportSecurityBindingElement) || transportSecurityBindingElement.IncludeTimestamp)
						{
							sbe = transportSecurityBindingElement;
							policyContext.BindingElements.Add(transportSecurityBindingElement);
						}
					}
				}
				else if (xmlElement != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSecurityPolicyAssertion", new object[]
					{
						xmlElement.OuterXml
					})));
				}
			}
			return transportSecurityBindingElement != null;
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x001600A8 File Offset: 0x0015E2A8
		private static bool HasSupportingTokens(SecurityBindingElement binding)
		{
			if (binding.EndpointSupportingTokenParameters.Endorsing.Count > 0 || binding.EndpointSupportingTokenParameters.SignedEndorsing.Count > 0 || binding.EndpointSupportingTokenParameters.SignedEncrypted.Count > 0 || binding.EndpointSupportingTokenParameters.Signed.Count > 0)
			{
				return true;
			}
			foreach (SupportingTokenParameters supportingTokenParameters in binding.OperationSupportingTokenParameters.Values)
			{
				if (supportingTokenParameters.Endorsing.Count > 0 || supportingTokenParameters.SignedEndorsing.Count > 0 || supportingTokenParameters.SignedEncrypted.Count > 0 || supportingTokenParameters.Signed.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x00160180 File Offset: 0x0015E380
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext policyContext)
		{
			if (importer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("importer");
			}
			if (policyContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policyContext");
			}
			WSSecurityPolicy wssecurityPolicy;
			if (WSSecurityPolicy.TryGetSecurityPolicyDriver(policyContext.GetBindingAssertions(), out wssecurityPolicy))
			{
				if (importer.State != null && !importer.State.ContainsKey("MaxPolicyRedirections"))
				{
					importer.State.Add("MaxPolicyRedirections", this.MaxPolicyRedirections);
				}
				SecurityBindingElement securityBindingElement = null;
				bool flag = this.TryImportSymmetricSecurityBindingElement(importer, policyContext, out securityBindingElement);
				if (!flag)
				{
					flag = this.TryImportAsymmetricSecurityBindingElement(importer, policyContext, out securityBindingElement);
				}
				if (!flag)
				{
					flag = this.TryImportTransportSecurityBindingElement(importer, policyContext, out securityBindingElement, false);
				}
				else
				{
					SecurityBindingElement securityBindingElement2 = null;
					this.TryImportTransportSecurityBindingElement(importer, policyContext, out securityBindingElement2, true);
				}
				if (securityBindingElement != null)
				{
					SecurityElement securityElement = new SecurityElement();
					securityElement.InitializeFrom(securityBindingElement, false);
					if (securityElement.HasImportFailed)
					{
						importer.Errors.Add(new MetadataConversionError(SR.GetString("SecurityBindingElementCannotBeExpressedInConfig"), true));
					}
				}
			}
		}

		// Token: 0x04003816 RID: 14358
		internal const string MaxPolicyRedirectionsKey = "MaxPolicyRedirections";

		// Token: 0x04003817 RID: 14359
		internal const string SecureConversationBootstrapEncryptionRequirements = "SecureConversationBootstrapEncryptionRequirements";

		// Token: 0x04003818 RID: 14360
		internal const string SecureConversationBootstrapSignatureRequirements = "SecureConversationBootstrapSignatureRequirements";

		// Token: 0x04003819 RID: 14361
		internal const string InSecureConversationBootstrapBindingImportMode = "InSecureConversationBootstrapBindingImportMode";

		// Token: 0x0400381A RID: 14362
		internal const string ContractProtectionLevelKey = "ContractProtectionLevelKey";

		// Token: 0x0400381B RID: 14363
		private int maxPolicyRedirections;

		// Token: 0x02000E12 RID: 3602
		private class ContractProtectionLevel
		{
			// Token: 0x060081BA RID: 33210 RVA: 0x001E0C36 File Offset: 0x001DEE36
			public ContractProtectionLevel(bool hasProtectionRequirements, bool hasUniformProtectionLevel, ProtectionLevel uniformProtectionLevel)
			{
				this.hasProtectionRequirements = hasProtectionRequirements;
				this.hasUniformProtectionLevel = hasUniformProtectionLevel;
				this.uniformProtectionLevel = uniformProtectionLevel;
			}

			// Token: 0x17001CA1 RID: 7329
			// (get) Token: 0x060081BB RID: 33211 RVA: 0x001E0C53 File Offset: 0x001DEE53
			public bool HasProtectionRequirements
			{
				get
				{
					return this.hasProtectionRequirements;
				}
			}

			// Token: 0x17001CA2 RID: 7330
			// (get) Token: 0x060081BC RID: 33212 RVA: 0x001E0C5B File Offset: 0x001DEE5B
			public bool HasUniformProtectionLevel
			{
				get
				{
					return this.hasUniformProtectionLevel;
				}
			}

			// Token: 0x17001CA3 RID: 7331
			// (get) Token: 0x060081BD RID: 33213 RVA: 0x001E0C63 File Offset: 0x001DEE63
			public ProtectionLevel UniformProtectionLevel
			{
				get
				{
					return this.uniformProtectionLevel;
				}
			}

			// Token: 0x040049CA RID: 18890
			private bool hasProtectionRequirements;

			// Token: 0x040049CB RID: 18891
			private bool hasUniformProtectionLevel;

			// Token: 0x040049CC RID: 18892
			private ProtectionLevel uniformProtectionLevel;
		}
	}
}
