using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel
{
	// Token: 0x020000E8 RID: 232
	[__DynamicallyInvokable]
	public class ChannelFactory<TChannel> : ChannelFactory, IChannelFactory<TChannel>, IChannelFactory, ICommunicationObject
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00016D04 File Offset: 0x00014F04
		[__DynamicallyInvokable]
		protected ChannelFactory(Type channelType)
		{
			if (channelType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelType");
			}
			if (!channelType.IsInterface)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryTypeMustBeInterface")));
			}
			this.channelType = channelType;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016D5C File Offset: 0x00014F5C
		public ChannelFactory() : this(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						typeof(TChannel).FullName
					}), ActivityType.Construct);
				}
				base.InitializeEndpoint(null, null);
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00016DE0 File Offset: 0x00014FE0
		[__DynamicallyInvokable]
		public ChannelFactory(string endpointConfigurationName) : this(endpointConfigurationName, null)
		{
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00016DEC File Offset: 0x00014FEC
		[__DynamicallyInvokable]
		public ChannelFactory(string endpointConfigurationName, EndpointAddress remoteAddress) : this(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						typeof(TChannel).FullName
					}), ActivityType.Construct);
				}
				if (endpointConfigurationName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
				}
				base.InitializeEndpoint(endpointConfigurationName, remoteAddress);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016E80 File Offset: 0x00015080
		public ChannelFactory(Binding binding) : this(binding, null)
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00016E8A File Offset: 0x0001508A
		public ChannelFactory(Binding binding, string remoteAddress) : this(binding, new EndpointAddress(remoteAddress))
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00016E9C File Offset: 0x0001509C
		[__DynamicallyInvokable]
		public ChannelFactory(Binding binding, EndpointAddress remoteAddress) : this(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						typeof(TChannel).FullName
					}), ActivityType.Construct);
				}
				if (binding == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
				}
				base.InitializeEndpoint(binding, remoteAddress);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00016F30 File Offset: 0x00015130
		public ChannelFactory(ServiceEndpoint endpoint) : this(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						typeof(TChannel).FullName
					}), ActivityType.Construct);
				}
				if (endpoint == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
				}
				base.InitializeEndpoint(endpoint);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00016FC4 File Offset: 0x000151C4
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00016FCC File Offset: 0x000151CC
		internal InstanceContext CallbackInstance
		{
			get
			{
				return this.callbackInstance;
			}
			set
			{
				this.callbackInstance = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00016FD5 File Offset: 0x000151D5
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00016FDD File Offset: 0x000151DD
		internal Type CallbackType
		{
			get
			{
				return this.callbackType;
			}
			set
			{
				this.callbackType = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00016FE6 File Offset: 0x000151E6
		internal ServiceChannelFactory ServiceChannelFactory
		{
			get
			{
				return (ServiceChannelFactory)base.InnerFactory;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00016FF3 File Offset: 0x000151F3
		internal TypeLoader TypeLoader
		{
			get
			{
				if (this.typeLoader == null)
				{
					this.typeLoader = new TypeLoader();
				}
				return this.typeLoader;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0001700E File Offset: 0x0001520E
		internal override string CloseActivityName
		{
			get
			{
				return SR.GetString("ActivityCloseChannelFactory", new object[]
				{
					typeof(TChannel).FullName
				});
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00017032 File Offset: 0x00015232
		internal override string OpenActivityName
		{
			get
			{
				return SR.GetString("ActivityOpenChannelFactory", new object[]
				{
					typeof(TChannel).FullName
				});
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00017056 File Offset: 0x00015256
		internal override ActivityType OpenActivityType
		{
			get
			{
				return ActivityType.OpenClient;
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00017059 File Offset: 0x00015259
		[__DynamicallyInvokable]
		public TChannel CreateChannel(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			return this.CreateChannel(address, address.Uri);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00017084 File Offset: 0x00015284
		[__DynamicallyInvokable]
		public virtual TChannel CreateChannel(EndpointAddress address, Uri via)
		{
			bool traceOpenAndClose = base.TraceOpenAndClose;
			TChannel result;
			try
			{
				using (ServiceModelActivity serviceModelActivity = (DiagnosticUtility.ShouldUseActivity && base.TraceOpenAndClose) ? ServiceModelActivity.CreateBoundedActivity() : null)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity.Start(serviceModelActivity, this.OpenActivityName, this.OpenActivityType);
						base.TraceOpenAndClose = false;
					}
					if (address == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
					}
					if (base.HasDuplexOperations())
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCreateNonDuplexChannel1", new object[]
						{
							base.Endpoint.Contract.Name
						})));
					}
					base.EnsureOpened();
					result = (TChannel)((object)this.ServiceChannelFactory.CreateChannel(typeof(TChannel), address, via));
				}
			}
			finally
			{
				base.TraceOpenAndClose = traceOpenAndClose;
			}
			return result;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00017178 File Offset: 0x00015378
		[__DynamicallyInvokable]
		public TChannel CreateChannel()
		{
			return this.CreateChannel(base.CreateEndpointAddress(base.Endpoint), null);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00017190 File Offset: 0x00015390
		public TChannel CreateChannelWithIssuedToken(SecurityToken issuedToken)
		{
			TChannel tchannel = this.CreateChannel();
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.IssuedSecurityToken = issuedToken;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000171C8 File Offset: 0x000153C8
		public TChannel CreateChannelWithIssuedToken(SecurityToken issuedToken, EndpointAddress address)
		{
			TChannel tchannel = this.CreateChannel(address);
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.IssuedSecurityToken = issuedToken;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00017204 File Offset: 0x00015404
		public TChannel CreateChannelWithIssuedToken(SecurityToken issuedToken, EndpointAddress address, Uri via)
		{
			TChannel tchannel = this.CreateChannel(address, via);
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.IssuedSecurityToken = issuedToken;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00017240 File Offset: 0x00015440
		public TChannel CreateChannelWithActAsToken(SecurityToken actAsToken)
		{
			TChannel tchannel = this.CreateChannel();
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.ActAs = actAsToken;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017278 File Offset: 0x00015478
		public TChannel CreateChannelWithActAsToken(SecurityToken actAsToken, EndpointAddress address)
		{
			TChannel tchannel = this.CreateChannel(address);
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.ActAs = actAsToken;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000172B4 File Offset: 0x000154B4
		public TChannel CreateChannelWithActAsToken(SecurityToken actAsToken, EndpointAddress address, Uri via)
		{
			TChannel tchannel = this.CreateChannel(address, via);
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.ActAs = actAsToken;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000172F0 File Offset: 0x000154F0
		public TChannel CreateChannelWithOnBehalfOfToken(SecurityToken onBehalfOf)
		{
			TChannel tchannel = this.CreateChannel();
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.OnBehalfOf = onBehalfOf;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00017328 File Offset: 0x00015528
		public TChannel CreateChannelWithOnBehalfOfToken(SecurityToken onBehalfOf, EndpointAddress address)
		{
			TChannel tchannel = this.CreateChannel(address);
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.OnBehalfOf = onBehalfOf;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00017364 File Offset: 0x00015564
		public TChannel CreateChannelWithOnBehalfOfToken(SecurityToken onBehalfOf, EndpointAddress address, Uri via)
		{
			TChannel tchannel = this.CreateChannel(address, via);
			FederatedClientCredentialsParameters federatedClientCredentialsParameters = new FederatedClientCredentialsParameters();
			federatedClientCredentialsParameters.OnBehalfOf = onBehalfOf;
			((IChannel)((object)tchannel)).GetProperty<ChannelParameterCollection>().Add(federatedClientCredentialsParameters);
			return tchannel;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001739E File Offset: 0x0001559E
		internal UChannel CreateChannel<UChannel>(EndpointAddress address)
		{
			base.EnsureOpened();
			return this.ServiceChannelFactory.CreateChannel<UChannel>(address);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000173B2 File Offset: 0x000155B2
		internal UChannel CreateChannel<UChannel>(EndpointAddress address, Uri via)
		{
			base.EnsureOpened();
			return this.ServiceChannelFactory.CreateChannel<UChannel>(address, via);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000173C7 File Offset: 0x000155C7
		internal bool CanCreateChannel<UChannel>()
		{
			base.EnsureOpened();
			return this.ServiceChannelFactory.CanCreateChannel<UChannel>();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000173DC File Offset: 0x000155DC
		[__DynamicallyInvokable]
		protected override ServiceEndpoint CreateDescription()
		{
			ContractDescription contract = this.TypeLoader.LoadContractDescription(this.channelType);
			ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contract);
			this.ReflectOnCallbackInstance(serviceEndpoint);
			this.TypeLoader.AddBehaviorsSFx(serviceEndpoint, this.channelType);
			return serviceEndpoint;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001741C File Offset: 0x0001561C
		private void ReflectOnCallbackInstance(ServiceEndpoint endpoint)
		{
			if (!(this.callbackType != null))
			{
				if (this.CallbackInstance != null && this.CallbackInstance.UserObject != null)
				{
					if (endpoint.Contract.CallbackContractType == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxCallbackTypeCannotBeNull", new object[]
						{
							endpoint.Contract.ContractType.FullName
						})));
					}
					object userObject = this.CallbackInstance.UserObject;
					Type type = userObject.GetType();
					this.TypeLoader.AddBehaviorsFromImplementationType(endpoint, type);
					IEndpointBehavior endpointBehavior = userObject as IEndpointBehavior;
					if (endpointBehavior != null)
					{
						endpoint.Behaviors.Add(endpointBehavior);
					}
					IContractBehavior contractBehavior = userObject as IContractBehavior;
					if (contractBehavior != null)
					{
						endpoint.Contract.Behaviors.Add(contractBehavior);
					}
				}
				return;
			}
			if (endpoint.Contract.CallbackContractType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SfxCallbackTypeCannotBeNull", new object[]
				{
					endpoint.Contract.ContractType.FullName
				})));
			}
			this.TypeLoader.AddBehaviorsFromImplementationType(endpoint, this.callbackType);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00017544 File Offset: 0x00015744
		protected static TChannel CreateChannel(string endpointConfigurationName)
		{
			ChannelFactory<TChannel> channelFactory = new ChannelFactory<TChannel>(endpointConfigurationName);
			if (channelFactory.HasDuplexOperations())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidStaticOverloadCalledForDuplexChannelFactory1", new object[]
				{
					channelFactory.channelType.Name
				})));
			}
			TChannel tchannel = channelFactory.CreateChannel();
			ChannelFactory<TChannel>.SetFactoryToAutoClose(tchannel);
			return tchannel;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001759C File Offset: 0x0001579C
		public static TChannel CreateChannel(Binding binding, EndpointAddress endpointAddress)
		{
			ChannelFactory<TChannel> channelFactory = new ChannelFactory<TChannel>(binding, endpointAddress);
			if (channelFactory.HasDuplexOperations())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidStaticOverloadCalledForDuplexChannelFactory1", new object[]
				{
					channelFactory.channelType.Name
				})));
			}
			TChannel tchannel = channelFactory.CreateChannel();
			ChannelFactory<TChannel>.SetFactoryToAutoClose(tchannel);
			return tchannel;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000175F8 File Offset: 0x000157F8
		public static TChannel CreateChannel(Binding binding, EndpointAddress endpointAddress, Uri via)
		{
			ChannelFactory<TChannel> channelFactory = new ChannelFactory<TChannel>(binding);
			if (channelFactory.HasDuplexOperations())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidStaticOverloadCalledForDuplexChannelFactory1", new object[]
				{
					channelFactory.channelType.Name
				})));
			}
			TChannel tchannel = channelFactory.CreateChannel(endpointAddress, via);
			ChannelFactory<TChannel>.SetFactoryToAutoClose(tchannel);
			return tchannel;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00017654 File Offset: 0x00015854
		internal static void SetFactoryToAutoClose(TChannel channel)
		{
			ServiceChannel serviceChannel = ServiceChannelFactory.GetServiceChannel(channel);
			serviceChannel.CloseFactory = true;
		}

		// Token: 0x04000A1A RID: 2586
		private InstanceContext callbackInstance;

		// Token: 0x04000A1B RID: 2587
		private Type channelType;

		// Token: 0x04000A1C RID: 2588
		private TypeLoader typeLoader;

		// Token: 0x04000A1D RID: 2589
		private Type callbackType;
	}
}
