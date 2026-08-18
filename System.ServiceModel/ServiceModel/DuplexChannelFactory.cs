using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel
{
	// Token: 0x020000ED RID: 237
	[__DynamicallyInvokable]
	public class DuplexChannelFactory<TChannel> : ChannelFactory<TChannel>
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0001774B File Offset: 0x0001594B
		public DuplexChannelFactory(Type callbackInstanceType) : this(callbackInstanceType)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00017754 File Offset: 0x00015954
		public DuplexChannelFactory(Type callbackInstanceType, Binding binding, string remoteAddress) : this(callbackInstanceType, binding, new EndpointAddress(remoteAddress))
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00017764 File Offset: 0x00015964
		public DuplexChannelFactory(Type callbackInstanceType, Binding binding, EndpointAddress remoteAddress) : this(callbackInstanceType, binding, remoteAddress)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001776F File Offset: 0x0001596F
		public DuplexChannelFactory(Type callbackInstanceType, Binding binding) : this(callbackInstanceType, binding)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00017779 File Offset: 0x00015979
		public DuplexChannelFactory(Type callbackInstanceType, string endpointConfigurationName, EndpointAddress remoteAddress) : this(callbackInstanceType, endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00017784 File Offset: 0x00015984
		public DuplexChannelFactory(Type callbackInstanceType, string endpointConfigurationName) : this(callbackInstanceType, endpointConfigurationName)
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001778E File Offset: 0x0001598E
		public DuplexChannelFactory(Type callbackInstanceType, ServiceEndpoint endpoint) : this(callbackInstanceType, endpoint)
		{
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00017798 File Offset: 0x00015998
		public DuplexChannelFactory(InstanceContext callbackInstance) : this(callbackInstance)
		{
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000177A1 File Offset: 0x000159A1
		[__DynamicallyInvokable]
		public DuplexChannelFactory(InstanceContext callbackInstance, Binding binding, string remoteAddress) : this(callbackInstance, binding, new EndpointAddress(remoteAddress))
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000177B1 File Offset: 0x000159B1
		[__DynamicallyInvokable]
		public DuplexChannelFactory(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) : this(callbackInstance, binding, remoteAddress)
		{
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000177BC File Offset: 0x000159BC
		[__DynamicallyInvokable]
		public DuplexChannelFactory(InstanceContext callbackInstance, Binding binding) : this(callbackInstance, binding)
		{
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000177C6 File Offset: 0x000159C6
		[__DynamicallyInvokable]
		public DuplexChannelFactory(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : this(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000177D1 File Offset: 0x000159D1
		[__DynamicallyInvokable]
		public DuplexChannelFactory(InstanceContext callbackInstance, string endpointConfigurationName) : this(callbackInstance, endpointConfigurationName)
		{
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000177DB File Offset: 0x000159DB
		public DuplexChannelFactory(InstanceContext callbackInstance, ServiceEndpoint endpoint) : this(callbackInstance, endpoint)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000177E8 File Offset: 0x000159E8
		public DuplexChannelFactory(object callbackObject) : base(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						TraceUtility.CreateSourceString(this)
					}), ActivityType.Construct);
				}
				if (callbackObject == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackObject");
				}
				this.CheckAndAssignCallbackInstance(callbackObject);
				base.InitializeEndpoint(null, null);
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001787C File Offset: 0x00015A7C
		public DuplexChannelFactory(object callbackObject, string endpointConfigurationName) : this(callbackObject, endpointConfigurationName, null)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00017888 File Offset: 0x00015A88
		public DuplexChannelFactory(object callbackObject, string endpointConfigurationName, EndpointAddress remoteAddress) : base(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						TraceUtility.CreateSourceString(this)
					}), ActivityType.Construct);
				}
				if (callbackObject == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackObject");
				}
				if (endpointConfigurationName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
				}
				this.CheckAndAssignCallbackInstance(callbackObject);
				base.InitializeEndpoint(endpointConfigurationName, remoteAddress);
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00017930 File Offset: 0x00015B30
		public DuplexChannelFactory(object callbackObject, Binding binding) : this(callbackObject, binding, null)
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001793B File Offset: 0x00015B3B
		public DuplexChannelFactory(object callbackObject, Binding binding, string remoteAddress) : this(callbackObject, binding, new EndpointAddress(remoteAddress))
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001794C File Offset: 0x00015B4C
		public DuplexChannelFactory(object callbackObject, Binding binding, EndpointAddress remoteAddress) : base(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						TraceUtility.CreateSourceString(this)
					}), ActivityType.Construct);
				}
				if (callbackObject == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackObject");
				}
				if (binding == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
				}
				this.CheckAndAssignCallbackInstance(callbackObject);
				base.InitializeEndpoint(binding, remoteAddress);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000179F4 File Offset: 0x00015BF4
		public DuplexChannelFactory(object callbackObject, ServiceEndpoint endpoint) : base(typeof(TChannel))
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityConstructChannelFactory", new object[]
					{
						TraceUtility.CreateSourceString(this)
					}), ActivityType.Construct);
				}
				if (callbackObject == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackObject");
				}
				if (endpoint == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
				}
				this.CheckAndAssignCallbackInstance(callbackObject);
				base.InitializeEndpoint(endpoint);
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017A98 File Offset: 0x00015C98
		internal void CheckAndAssignCallbackInstance(object callbackInstance)
		{
			if (callbackInstance is Type)
			{
				base.CallbackType = (Type)callbackInstance;
				return;
			}
			if (callbackInstance is InstanceContext)
			{
				base.CallbackInstance = (InstanceContext)callbackInstance;
				return;
			}
			base.CallbackInstance = new InstanceContext(callbackInstance);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00017AD0 File Offset: 0x00015CD0
		[__DynamicallyInvokable]
		public TChannel CreateChannel(InstanceContext callbackInstance)
		{
			return this.CreateChannel(callbackInstance, base.CreateEndpointAddress(base.Endpoint), null);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00017AE6 File Offset: 0x00015CE6
		[__DynamicallyInvokable]
		public TChannel CreateChannel(InstanceContext callbackInstance, EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			return this.CreateChannel(callbackInstance, address, address.Uri);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00017B0F File Offset: 0x00015D0F
		[__DynamicallyInvokable]
		public override TChannel CreateChannel(EndpointAddress address, Uri via)
		{
			return this.CreateChannel(base.CallbackInstance, address, via);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00017B20 File Offset: 0x00015D20
		[__DynamicallyInvokable]
		public virtual TChannel CreateChannel(InstanceContext callbackInstance, EndpointAddress address, Uri via)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			if (base.CallbackType != null && callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCreateDuplexChannelNoCallback1")));
			}
			if (callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCreateDuplexChannelNoCallback")));
			}
			if (callbackInstance.UserObject == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCreateDuplexChannelNoCallbackUserObject")));
			}
			if (!base.HasDuplexOperations())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCreateDuplexChannel1", new object[]
				{
					base.Endpoint.Contract.Name
				})));
			}
			Type type = callbackInstance.UserObject.GetType();
			Type callbackContractType = base.Endpoint.Contract.CallbackContractType;
			if (callbackContractType != null && !callbackContractType.IsAssignableFrom(type))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxCreateDuplexChannelBadCallbackUserObject", new object[]
				{
					callbackContractType
				})));
			}
			base.EnsureOpened();
			TChannel tchannel = (TChannel)((object)base.ServiceChannelFactory.CreateChannel(typeof(TChannel), address, via));
			IDuplexContextChannel duplexContextChannel = tchannel as IDuplexContextChannel;
			if (duplexContextChannel != null)
			{
				duplexContextChannel.CallbackInstance = callbackInstance;
			}
			return tchannel;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00017C78 File Offset: 0x00015E78
		private static InstanceContext GetInstanceContextForObject(object callbackObject)
		{
			if (callbackObject is InstanceContext)
			{
				return (InstanceContext)callbackObject;
			}
			return new InstanceContext(callbackObject);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017C8F File Offset: 0x00015E8F
		public static TChannel CreateChannel(object callbackObject, string endpointConfigurationName)
		{
			return DuplexChannelFactory<TChannel>.CreateChannel(DuplexChannelFactory<TChannel>.GetInstanceContextForObject(callbackObject), endpointConfigurationName);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00017C9D File Offset: 0x00015E9D
		public static TChannel CreateChannel(object callbackObject, Binding binding, EndpointAddress endpointAddress)
		{
			return DuplexChannelFactory<TChannel>.CreateChannel(DuplexChannelFactory<TChannel>.GetInstanceContextForObject(callbackObject), binding, endpointAddress);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017CAC File Offset: 0x00015EAC
		public static TChannel CreateChannel(object callbackObject, Binding binding, EndpointAddress endpointAddress, Uri via)
		{
			return DuplexChannelFactory<TChannel>.CreateChannel(DuplexChannelFactory<TChannel>.GetInstanceContextForObject(callbackObject), binding, endpointAddress, via);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017CBC File Offset: 0x00015EBC
		public static TChannel CreateChannel(InstanceContext callbackInstance, string endpointConfigurationName)
		{
			DuplexChannelFactory<TChannel> duplexChannelFactory = new DuplexChannelFactory<TChannel>(callbackInstance, endpointConfigurationName);
			TChannel tchannel = duplexChannelFactory.CreateChannel();
			ChannelFactory<TChannel>.SetFactoryToAutoClose(tchannel);
			return tchannel;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00017CE0 File Offset: 0x00015EE0
		public static TChannel CreateChannel(InstanceContext callbackInstance, Binding binding, EndpointAddress endpointAddress)
		{
			DuplexChannelFactory<TChannel> duplexChannelFactory = new DuplexChannelFactory<TChannel>(callbackInstance, binding, endpointAddress);
			TChannel tchannel = duplexChannelFactory.CreateChannel();
			ChannelFactory<TChannel>.SetFactoryToAutoClose(tchannel);
			return tchannel;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00017D04 File Offset: 0x00015F04
		public static TChannel CreateChannel(InstanceContext callbackInstance, Binding binding, EndpointAddress endpointAddress, Uri via)
		{
			DuplexChannelFactory<TChannel> duplexChannelFactory = new DuplexChannelFactory<TChannel>(callbackInstance, binding);
			TChannel tchannel = duplexChannelFactory.CreateChannel(endpointAddress, via);
			ChannelFactory<TChannel>.SetFactoryToAutoClose(tchannel);
			return tchannel;
		}
	}
}
