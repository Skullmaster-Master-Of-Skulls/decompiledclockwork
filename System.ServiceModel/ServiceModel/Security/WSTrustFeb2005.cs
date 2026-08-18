using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Net.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200029B RID: 667
	internal class WSTrustFeb2005 : WSTrust
	{
		// Token: 0x06001434 RID: 5172 RVA: 0x0004C1DF File Offset: 0x0004A3DF
		public WSTrustFeb2005(WSSecurityTokenSerializer tokenSerializer) : base(tokenSerializer)
		{
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0004C1E8 File Offset: 0x0004A3E8
		public override TrustDictionary SerializerDictionary
		{
			get
			{
				return XD.TrustFeb2005Dictionary;
			}
		}

		// Token: 0x02000B37 RID: 2871
		public class DriverFeb2005 : WSTrust.Driver
		{
			// Token: 0x0600709A RID: 28826 RVA: 0x001A36E8 File Offset: 0x001A18E8
			public DriverFeb2005(SecurityStandardsManager standardsManager) : base(standardsManager)
			{
			}

			// Token: 0x17001A44 RID: 6724
			// (get) Token: 0x0600709B RID: 28827 RVA: 0x001A36F1 File Offset: 0x001A18F1
			public override TrustDictionary DriverDictionary
			{
				get
				{
					return XD.TrustFeb2005Dictionary;
				}
			}

			// Token: 0x17001A45 RID: 6725
			// (get) Token: 0x0600709C RID: 28828 RVA: 0x001A36F8 File Offset: 0x001A18F8
			public override XmlDictionaryString RequestSecurityTokenResponseFinalAction
			{
				get
				{
					return XD.TrustFeb2005Dictionary.RequestSecurityTokenIssuanceResponse;
				}
			}

			// Token: 0x17001A46 RID: 6726
			// (get) Token: 0x0600709D RID: 28829 RVA: 0x001A3704 File Offset: 0x001A1904
			public override bool IsSessionSupported
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001A47 RID: 6727
			// (get) Token: 0x0600709E RID: 28830 RVA: 0x001A3707 File Offset: 0x001A1907
			public override bool IsIssuedTokensSupported
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001A48 RID: 6728
			// (get) Token: 0x0600709F RID: 28831 RVA: 0x001A370A File Offset: 0x001A190A
			public override string IssuedTokensHeaderName
			{
				get
				{
					return this.DriverDictionary.IssuedTokensHeader.Value;
				}
			}

			// Token: 0x17001A49 RID: 6729
			// (get) Token: 0x060070A0 RID: 28832 RVA: 0x001A371C File Offset: 0x001A191C
			public override string IssuedTokensHeaderNamespace
			{
				get
				{
					return this.DriverDictionary.Namespace.Value;
				}
			}

			// Token: 0x17001A4A RID: 6730
			// (get) Token: 0x060070A1 RID: 28833 RVA: 0x001A372E File Offset: 0x001A192E
			public override string RequestTypeRenew
			{
				get
				{
					return this.DriverDictionary.RequestTypeRenew.Value;
				}
			}

			// Token: 0x17001A4B RID: 6731
			// (get) Token: 0x060070A2 RID: 28834 RVA: 0x001A3740 File Offset: 0x001A1940
			public override string RequestTypeClose
			{
				get
				{
					return this.DriverDictionary.RequestTypeClose.Value;
				}
			}

			// Token: 0x060070A3 RID: 28835 RVA: 0x001A3752 File Offset: 0x001A1952
			public override Collection<XmlElement> ProcessUnknownRequestParameters(Collection<XmlElement> unknownRequestParameters, Collection<XmlElement> originalRequestParameters)
			{
				return unknownRequestParameters;
			}

			// Token: 0x060070A4 RID: 28836 RVA: 0x001A3758 File Offset: 0x001A1958
			protected override void ReadReferences(XmlElement rstrXml, out SecurityKeyIdentifierClause requestedAttachedReference, out SecurityKeyIdentifierClause requestedUnattachedReference)
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
						else if (xmlElement2.LocalName == this.DriverDictionary.RequestedAttachedReference.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							requestedAttachedReference = base.GetKeyIdentifierXmlReferenceClause(XmlHelper.GetChildElement(xmlElement2));
						}
						else if (xmlElement2.LocalName == this.DriverDictionary.RequestedUnattachedReference.Value && xmlElement2.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							requestedUnattachedReference = base.GetKeyIdentifierXmlReferenceClause(XmlHelper.GetChildElement(xmlElement2));
						}
					}
				}
				try
				{
					if (xmlElement != null)
					{
						if (requestedAttachedReference == null)
						{
							this.StandardsManager.TryCreateKeyIdentifierClauseFromTokenXml(xmlElement, SecurityTokenReferenceStyle.Internal, out requestedAttachedReference);
						}
						if (requestedUnattachedReference == null)
						{
							this.StandardsManager.TryCreateKeyIdentifierClauseFromTokenXml(xmlElement, SecurityTokenReferenceStyle.External, out requestedUnattachedReference);
						}
					}
				}
				catch (XmlException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("TrustDriverIsUnableToCreatedNecessaryAttachedOrUnattachedReferences", new object[]
					{
						xmlElement.ToString()
					})));
				}
			}

			// Token: 0x060070A5 RID: 28837 RVA: 0x001A38E0 File Offset: 0x001A1AE0
			protected override bool ReadRequestedTokenClosed(XmlElement rstrXml)
			{
				for (int i = 0; i < rstrXml.ChildNodes.Count; i++)
				{
					XmlElement xmlElement = rstrXml.ChildNodes[i] as XmlElement;
					if (xmlElement != null && xmlElement.LocalName == this.DriverDictionary.RequestedTokenClosed.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060070A6 RID: 28838 RVA: 0x001A3958 File Offset: 0x001A1B58
			protected override void ReadTargets(XmlElement rstXml, out SecurityKeyIdentifierClause renewTarget, out SecurityKeyIdentifierClause closeTarget)
			{
				renewTarget = null;
				closeTarget = null;
				for (int i = 0; i < rstXml.ChildNodes.Count; i++)
				{
					XmlElement xmlElement = rstXml.ChildNodes[i] as XmlElement;
					if (xmlElement != null)
					{
						if (xmlElement.LocalName == this.DriverDictionary.RenewTarget.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							renewTarget = this.StandardsManager.SecurityTokenSerializer.ReadKeyIdentifierClause(new XmlNodeReader(xmlElement.FirstChild));
						}
						else if (xmlElement.LocalName == this.DriverDictionary.CloseTarget.Value && xmlElement.NamespaceURI == this.DriverDictionary.Namespace.Value)
						{
							closeTarget = this.StandardsManager.SecurityTokenSerializer.ReadKeyIdentifierClause(new XmlNodeReader(xmlElement.FirstChild));
						}
					}
				}
			}

			// Token: 0x060070A7 RID: 28839 RVA: 0x001A3A50 File Offset: 0x001A1C50
			protected override void WriteReferences(RequestSecurityTokenResponse rstr, XmlDictionaryWriter writer)
			{
				if (rstr.RequestedAttachedReference != null)
				{
					writer.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestedAttachedReference, this.DriverDictionary.Namespace);
					this.StandardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer, rstr.RequestedAttachedReference);
					writer.WriteEndElement();
				}
				if (rstr.RequestedUnattachedReference != null)
				{
					writer.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RequestedUnattachedReference, this.DriverDictionary.Namespace);
					this.StandardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer, rstr.RequestedUnattachedReference);
					writer.WriteEndElement();
				}
			}

			// Token: 0x060070A8 RID: 28840 RVA: 0x001A3AFF File Offset: 0x001A1CFF
			protected override void WriteRequestedTokenClosed(RequestSecurityTokenResponse rstr, XmlDictionaryWriter writer)
			{
				if (rstr.IsRequestedTokenClosed)
				{
					writer.WriteElementString(this.DriverDictionary.RequestedTokenClosed, this.DriverDictionary.Namespace, string.Empty);
				}
			}

			// Token: 0x060070A9 RID: 28841 RVA: 0x001A3B2C File Offset: 0x001A1D2C
			protected override void WriteTargets(RequestSecurityToken rst, XmlDictionaryWriter writer)
			{
				if (rst.RenewTarget != null)
				{
					writer.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.RenewTarget, this.DriverDictionary.Namespace);
					this.StandardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer, rst.RenewTarget);
					writer.WriteEndElement();
				}
				if (rst.CloseTarget != null)
				{
					writer.WriteStartElement(this.DriverDictionary.Prefix.Value, this.DriverDictionary.CloseTarget, this.DriverDictionary.Namespace);
					this.StandardsManager.SecurityTokenSerializer.WriteKeyIdentifierClause(writer, rst.CloseTarget);
					writer.WriteEndElement();
				}
			}

			// Token: 0x060070AA RID: 28842 RVA: 0x001A3BDC File Offset: 0x001A1DDC
			public override IChannelFactory<IRequestChannel> CreateFederationProxy(EndpointAddress address, Binding binding, KeyedByTypeCollection<IEndpointBehavior> channelBehaviors)
			{
				if (channelBehaviors == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelBehaviors");
				}
				ChannelFactory<WSTrustFeb2005.DriverFeb2005.IWsTrustFeb2005SecurityTokenService> channelFactory = new ChannelFactory<WSTrustFeb2005.DriverFeb2005.IWsTrustFeb2005SecurityTokenService>(binding, address);
				base.SetProtectionLevelForFederation(channelFactory.Endpoint.Contract.Operations);
				channelFactory.Endpoint.Behaviors.Remove<ClientCredentials>();
				for (int i = 0; i < channelBehaviors.Count; i++)
				{
					channelFactory.Endpoint.Behaviors.Add(channelBehaviors[i]);
				}
				channelFactory.Endpoint.Behaviors.Add(new WSTrustFeb2005.DriverFeb2005.InteractiveInitializersRemovingBehavior());
				return new WSTrustFeb2005.DriverFeb2005.RequestChannelFactory<WSTrustFeb2005.DriverFeb2005.IWsTrustFeb2005SecurityTokenService>(channelFactory);
			}

			// Token: 0x02000ED8 RID: 3800
			[ServiceContract]
			internal interface IWsTrustFeb2005SecurityTokenService
			{
				// Token: 0x0600849D RID: 33949
				[OperationContract(IsOneWay = false, Action = "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue", ReplyAction = "http://schemas.xmlsoap.org/ws/2005/02/trust/RSTR/Issue")]
				[FaultContract(typeof(string), Action = "*", ProtectionLevel = ProtectionLevel.Sign)]
				Message RequestToken(Message message);
			}

			// Token: 0x02000ED9 RID: 3801
			public class InteractiveInitializersRemovingBehavior : IEndpointBehavior
			{
				// Token: 0x0600849E RID: 33950 RVA: 0x001EA001 File Offset: 0x001E8201
				public void Validate(ServiceEndpoint serviceEndpoint)
				{
				}

				// Token: 0x0600849F RID: 33951 RVA: 0x001EA003 File Offset: 0x001E8203
				public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
				{
				}

				// Token: 0x060084A0 RID: 33952 RVA: 0x001EA005 File Offset: 0x001E8205
				public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
				{
				}

				// Token: 0x060084A1 RID: 33953 RVA: 0x001EA007 File Offset: 0x001E8207
				public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
				{
					if (behavior != null && behavior.InteractiveChannelInitializers != null)
					{
						behavior.InteractiveChannelInitializers.Clear();
					}
				}
			}

			// Token: 0x02000EDA RID: 3802
			public class RequestChannelFactory<TokenService> : ChannelFactoryBase, IChannelFactory<IRequestChannel>, IChannelFactory, ICommunicationObject
			{
				// Token: 0x060084A3 RID: 33955 RVA: 0x001EA027 File Offset: 0x001E8227
				public RequestChannelFactory(ChannelFactory<TokenService> innerChannelFactory)
				{
					this.innerChannelFactory = innerChannelFactory;
				}

				// Token: 0x060084A4 RID: 33956 RVA: 0x001EA036 File Offset: 0x001E8236
				public IRequestChannel CreateChannel(EndpointAddress address)
				{
					return this.innerChannelFactory.CreateChannel<IRequestChannel>(address);
				}

				// Token: 0x060084A5 RID: 33957 RVA: 0x001EA044 File Offset: 0x001E8244
				public IRequestChannel CreateChannel(EndpointAddress address, Uri via)
				{
					return this.innerChannelFactory.CreateChannel<IRequestChannel>(address, via);
				}

				// Token: 0x060084A6 RID: 33958 RVA: 0x001EA053 File Offset: 0x001E8253
				protected override void OnAbort()
				{
					this.innerChannelFactory.Abort();
				}

				// Token: 0x060084A7 RID: 33959 RVA: 0x001EA060 File Offset: 0x001E8260
				protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
				{
					return this.innerChannelFactory.BeginOpen(timeout, callback, state);
				}

				// Token: 0x060084A8 RID: 33960 RVA: 0x001EA070 File Offset: 0x001E8270
				protected override void OnEndOpen(IAsyncResult result)
				{
					this.innerChannelFactory.EndOpen(result);
				}

				// Token: 0x060084A9 RID: 33961 RVA: 0x001EA07E File Offset: 0x001E827E
				protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
				{
					return this.innerChannelFactory.BeginClose(timeout, callback, state);
				}

				// Token: 0x060084AA RID: 33962 RVA: 0x001EA08E File Offset: 0x001E828E
				protected override void OnEndClose(IAsyncResult result)
				{
					this.innerChannelFactory.EndClose(result);
				}

				// Token: 0x060084AB RID: 33963 RVA: 0x001EA09C File Offset: 0x001E829C
				protected override void OnClose(TimeSpan timeout)
				{
					this.innerChannelFactory.Close(timeout);
				}

				// Token: 0x060084AC RID: 33964 RVA: 0x001EA0AA File Offset: 0x001E82AA
				protected override void OnOpen(TimeSpan timeout)
				{
					this.innerChannelFactory.Open(timeout);
				}

				// Token: 0x060084AD RID: 33965 RVA: 0x001EA0B8 File Offset: 0x001E82B8
				public override T GetProperty<T>()
				{
					return this.innerChannelFactory.GetProperty<T>();
				}

				// Token: 0x04004CC0 RID: 19648
				private ChannelFactory<TokenService> innerChannelFactory;
			}
		}
	}
}
