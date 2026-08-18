using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200029A RID: 666
	internal class WSTrustDec2005 : WSTrustFeb2005
	{
		// Token: 0x06001432 RID: 5170 RVA: 0x0004C1CF File Offset: 0x0004A3CF
		public WSTrustDec2005(WSSecurityTokenSerializer tokenSerializer) : base(tokenSerializer)
		{
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0004C1D8 File Offset: 0x0004A3D8
		public override TrustDictionary SerializerDictionary
		{
			get
			{
				return DXD.TrustDec2005Dictionary;
			}
		}

		// Token: 0x02000B36 RID: 2870
		public class DriverDec2005 : WSTrustFeb2005.DriverFeb2005
		{
			// Token: 0x0600708F RID: 28815 RVA: 0x001A336E File Offset: 0x001A156E
			public DriverDec2005(SecurityStandardsManager standardsManager) : base(standardsManager)
			{
			}

			// Token: 0x17001A42 RID: 6722
			// (get) Token: 0x06007090 RID: 28816 RVA: 0x001A3377 File Offset: 0x001A1577
			public override TrustDictionary DriverDictionary
			{
				get
				{
					return DXD.TrustDec2005Dictionary;
				}
			}

			// Token: 0x17001A43 RID: 6723
			// (get) Token: 0x06007091 RID: 28817 RVA: 0x001A337E File Offset: 0x001A157E
			public override XmlDictionaryString RequestSecurityTokenResponseFinalAction
			{
				get
				{
					return DXD.TrustDec2005Dictionary.RequestSecurityTokenCollectionIssuanceFinalResponse;
				}
			}

			// Token: 0x06007092 RID: 28818 RVA: 0x001A338C File Offset: 0x001A158C
			public override XmlElement CreateKeyTypeElement(SecurityKeyType keyType)
			{
				if (keyType == SecurityKeyType.BearerKey)
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlElement xmlElement = xmlDocument.CreateElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.KeyType.Value, this.DriverDictionary.Namespace.Value);
					xmlElement.AppendChild(xmlDocument.CreateTextNode(DXD.TrustDec2005Dictionary.BearerKeyType.Value));
					return xmlElement;
				}
				return base.CreateKeyTypeElement(keyType);
			}

			// Token: 0x06007093 RID: 28819 RVA: 0x001A3400 File Offset: 0x001A1600
			public override bool TryParseKeyTypeElement(XmlElement element, out SecurityKeyType keyType)
			{
				if (element == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
				}
				if (element.LocalName == this.DriverDictionary.KeyType.Value && element.NamespaceURI == this.DriverDictionary.Namespace.Value && element.InnerText == DXD.TrustDec2005Dictionary.BearerKeyType.Value)
				{
					keyType = SecurityKeyType.BearerKey;
					return true;
				}
				return base.TryParseKeyTypeElement(element, out keyType);
			}

			// Token: 0x06007094 RID: 28820 RVA: 0x001A3484 File Offset: 0x001A1684
			public override XmlElement CreateRequiredClaimsElement(IEnumerable<XmlElement> claimsList)
			{
				XmlElement xmlElement = base.CreateRequiredClaimsElement(claimsList);
				XmlAttribute xmlAttribute = xmlElement.OwnerDocument.CreateAttribute(DXD.TrustDec2005Dictionary.Dialect.Value);
				xmlAttribute.Value = DXD.TrustDec2005Dictionary.DialectType.Value;
				xmlElement.Attributes.Append(xmlAttribute);
				return xmlElement;
			}

			// Token: 0x06007095 RID: 28821 RVA: 0x001A34D8 File Offset: 0x001A16D8
			public override IChannelFactory<IRequestChannel> CreateFederationProxy(EndpointAddress address, Binding binding, KeyedByTypeCollection<IEndpointBehavior> channelBehaviors)
			{
				if (channelBehaviors == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelBehaviors");
				}
				ChannelFactory<WSTrustDec2005.DriverDec2005.IWsTrustDec2005SecurityTokenService> channelFactory = new ChannelFactory<WSTrustDec2005.DriverDec2005.IWsTrustDec2005SecurityTokenService>(binding, address);
				base.SetProtectionLevelForFederation(channelFactory.Endpoint.Contract.Operations);
				channelFactory.Endpoint.Behaviors.Remove<ClientCredentials>();
				for (int i = 0; i < channelBehaviors.Count; i++)
				{
					channelFactory.Endpoint.Behaviors.Add(channelBehaviors[i]);
				}
				channelFactory.Endpoint.Behaviors.Add(new WSTrustFeb2005.DriverFeb2005.InteractiveInitializersRemovingBehavior());
				return new WSTrustFeb2005.DriverFeb2005.RequestChannelFactory<WSTrustDec2005.DriverDec2005.IWsTrustDec2005SecurityTokenService>(channelFactory);
			}

			// Token: 0x06007096 RID: 28822 RVA: 0x001A356C File Offset: 0x001A176C
			public override Collection<XmlElement> ProcessUnknownRequestParameters(Collection<XmlElement> unknownRequestParameters, Collection<XmlElement> originalRequestParameters)
			{
				if (originalRequestParameters == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("originalRequestParameters");
				}
				if (originalRequestParameters.Count > 0 && originalRequestParameters[0] != null && originalRequestParameters[0].OwnerDocument != null)
				{
					XmlElement xmlElement = originalRequestParameters[0].OwnerDocument.CreateElement(DXD.TrustDec2005Dictionary.Prefix.Value, DXD.TrustDec2005Dictionary.SecondaryParameters.Value, DXD.TrustDec2005Dictionary.Namespace.Value);
					for (int i = 0; i < originalRequestParameters.Count; i++)
					{
						xmlElement.AppendChild(originalRequestParameters[i]);
					}
					return new Collection<XmlElement>
					{
						xmlElement
					};
				}
				return originalRequestParameters;
			}

			// Token: 0x06007097 RID: 28823 RVA: 0x001A361D File Offset: 0x001A181D
			internal virtual bool IsSecondaryParametersElement(XmlElement element)
			{
				return element.LocalName == DXD.TrustDec2005Dictionary.SecondaryParameters.Value && element.NamespaceURI == DXD.TrustDec2005Dictionary.Namespace.Value;
			}

			// Token: 0x06007098 RID: 28824 RVA: 0x001A3658 File Offset: 0x001A1858
			public virtual XmlElement CreateKeyWrapAlgorithmElement(string keyWrapAlgorithm)
			{
				if (keyWrapAlgorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyWrapAlgorithm");
				}
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(DXD.TrustDec2005Dictionary.Prefix.Value, DXD.TrustDec2005Dictionary.KeyWrapAlgorithm.Value, DXD.TrustDec2005Dictionary.Namespace.Value);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(keyWrapAlgorithm));
				return xmlElement;
			}

			// Token: 0x06007099 RID: 28825 RVA: 0x001A36C1 File Offset: 0x001A18C1
			internal override bool IsKeyWrapAlgorithmElement(XmlElement element, out string keyWrapAlgorithm)
			{
				return WSTrust.CheckElement(element, DXD.TrustDec2005Dictionary.KeyWrapAlgorithm.Value, DXD.TrustDec2005Dictionary.Namespace.Value, out keyWrapAlgorithm);
			}

			// Token: 0x02000ED7 RID: 3799
			[ServiceContract]
			internal interface IWsTrustDec2005SecurityTokenService
			{
				// Token: 0x0600849C RID: 33948
				[OperationContract(IsOneWay = false, Action = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue", ReplyAction = "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RSTRC/IssueFinal")]
				[FaultContract(typeof(string), Action = "*", ProtectionLevel = ProtectionLevel.Sign)]
				Message RequestToken(Message message);
			}
		}
	}
}
