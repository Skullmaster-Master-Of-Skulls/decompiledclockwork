using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000274 RID: 628
	internal class TypedServiceChannelBuilder : IProxyCreator, IDisposable, IProvideChannelBuilderSettings, ICreateServiceChannel
	{
		// Token: 0x060011E6 RID: 4582 RVA: 0x00041604 File Offset: 0x0003F804
		void IDisposable.Dispose()
		{
			if (this.serviceProxy != null)
			{
				IChannel channel = this.serviceProxy.GetTransparentProxy() as IChannel;
				if (channel == null)
				{
					throw Fx.AssertAndThrow("serviceProxy MUST support IChannel");
				}
				channel.Close();
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00041642 File Offset: 0x0003F842
		ServiceChannelFactory IProvideChannelBuilderSettings.ServiceChannelFactoryReadWrite
		{
			get
			{
				if (this.serviceProxy != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("TooLate"), HR.RPC_E_TOO_LATE));
				}
				return this.serviceChannelFactory;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00041673 File Offset: 0x0003F873
		ServiceChannelFactory IProvideChannelBuilderSettings.ServiceChannelFactoryReadOnly
		{
			get
			{
				return this.serviceChannelFactory;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x0004167B File Offset: 0x0003F87B
		KeyedByTypeCollection<IEndpointBehavior> IProvideChannelBuilderSettings.Behaviors
		{
			get
			{
				if (this.serviceProxy != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(SR.GetString("TooLate"), HR.RPC_E_TOO_LATE));
				}
				return this.behaviors;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x000416AC File Offset: 0x0003F8AC
		ServiceChannel IProvideChannelBuilderSettings.ServiceChannel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x000416B0 File Offset: 0x0003F8B0
		RealProxy ICreateServiceChannel.CreateChannel()
		{
			if (this.serviceProxy == null)
			{
				lock (this)
				{
					if (this.serviceProxy == null)
					{
						try
						{
							if (this.serviceChannelFactory == null)
							{
								this.FaultInserviceChannelFactory();
							}
							if (this.serviceChannelFactory == null)
							{
								throw Fx.AssertAndThrow("ServiceChannelFactory cannot be null at this point");
							}
							this.serviceChannelFactory.Open();
							if (this.contractType == null)
							{
								throw Fx.AssertAndThrow("contractType cannot be null");
							}
							if (this.serviceEndpoint == null)
							{
								throw Fx.AssertAndThrow("serviceEndpoint cannot be null");
							}
							object proxy = this.serviceChannelFactory.CreateChannel(this.contractType, new EndpointAddress(this.serviceEndpoint.Address.Uri, this.serviceEndpoint.Address.Identity, this.serviceEndpoint.Address.Headers), this.serviceEndpoint.Address.Uri);
							ComPlusChannelCreatedTrace.Trace(TraceEventType.Verbose, 327711, "TraceCodeComIntegrationChannelCreated", this.serviceEndpoint.Address.Uri, this.contractType);
							RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
							this.serviceProxy = realProxy;
							if (this.serviceProxy == null)
							{
								throw Fx.AssertAndThrow("serviceProxy MUST derive from RealProxy");
							}
						}
						finally
						{
							if (this.serviceProxy == null && this.serviceChannelFactory != null)
							{
								this.serviceChannelFactory.Close();
							}
						}
					}
				}
			}
			return this.serviceProxy;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00041848 File Offset: 0x0003FA48
		private ServiceEndpoint CreateServiceEndpoint()
		{
			TypeLoader typeLoader = new TypeLoader();
			ContractDescription contract = typeLoader.LoadContractDescription(this.contractType);
			ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contract);
			if (this.address != null)
			{
				serviceEndpoint.Address = new EndpointAddress(new Uri(this.address), this.identity, new AddressHeader[0]);
			}
			if (this.binding != null)
			{
				serviceEndpoint.Binding = this.binding;
			}
			if (this.configurationName != null)
			{
				ConfigLoader configLoader = new ConfigLoader();
				configLoader.LoadChannelBehaviors(serviceEndpoint, this.configurationName);
			}
			ComPlusTypedChannelBuilderTrace.Trace(TraceEventType.Verbose, 327710, "TraceCodeComIntegrationTypedChannelBuilderLoaded", this.contractType, this.binding);
			return serviceEndpoint;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x000418E8 File Offset: 0x0003FAE8
		private ServiceChannelFactory CreateServiceChannelFactory()
		{
			ServiceChannelFactory serviceChannelFactory = ServiceChannelFactory.BuildChannelFactory(this.serviceEndpoint);
			if (serviceChannelFactory == null)
			{
				throw Fx.AssertAndThrow("We should get a ServiceChannelFactory back");
			}
			return serviceChannelFactory;
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00041910 File Offset: 0x0003FB10
		private void FaultInserviceChannelFactory()
		{
			if (this.contractType == null)
			{
				throw Fx.AssertAndThrow("contractType should not be null");
			}
			if (this.serviceEndpoint == null)
			{
				this.serviceEndpoint = this.CreateServiceEndpoint();
			}
			foreach (IEndpointBehavior item in this.behaviors)
			{
				this.serviceEndpoint.Behaviors.Add(item);
			}
			this.serviceChannelFactory = this.CreateServiceChannelFactory();
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x000419A0 File Offset: 0x0003FBA0
		internal void ResolveTypeIfPossible(Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable)
		{
			string text;
			propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Contract, out text);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					this.dispatchEnabled = true;
					Guid riid = new Guid(text);
					TypeCacheManager.Provider.FindOrCreateType(riid, out this.contractType, true, false);
					this.serviceEndpoint = this.CreateServiceEndpoint();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("TypeLoadForContractTypeIIDFailedWith", new object[]
					{
						text,
						ex.Message
					})));
				}
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00041A38 File Offset: 0x0003FC38
		internal TypedServiceChannelBuilder(Dictionary<MonikerHelper.MonikerAttribute, string> propertyTable)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Address, out this.address);
			propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.Binding, out text);
			propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.BindingConfiguration, out text2);
			propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.SpnIdentity, out text3);
			propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.UpnIdentity, out text4);
			propertyTable.TryGetValue(MonikerHelper.MonikerAttribute.DnsIdentity, out text5);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					this.binding = ConfigLoader.LookupBinding(text, text2);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("BindingLoadFromConfigFailedWith", new object[]
					{
						text,
						ex.Message
					})));
				}
				if (this.binding == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("BindingNotFoundInConfig", new object[]
					{
						text,
						text2
					})));
				}
			}
			if (this.binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("BindingNotSpecified")));
			}
			if (string.IsNullOrEmpty(this.address))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("AddressNotSpecified")));
			}
			if (!string.IsNullOrEmpty(text3))
			{
				if (!string.IsNullOrEmpty(text4) || !string.IsNullOrEmpty(text5))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentity")));
				}
				this.identity = EndpointIdentity.CreateSpnIdentity(text3);
			}
			else if (!string.IsNullOrEmpty(text4))
			{
				if (!string.IsNullOrEmpty(text3) || !string.IsNullOrEmpty(text5))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentity")));
				}
				this.identity = EndpointIdentity.CreateUpnIdentity(text4);
			}
			else if (!string.IsNullOrEmpty(text5))
			{
				if (!string.IsNullOrEmpty(text3) || !string.IsNullOrEmpty(text4))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MonikerSyntaxException(SR.GetString("MonikerIncorrectServerIdentity")));
				}
				this.identity = EndpointIdentity.CreateDnsIdentity(text5);
			}
			else
			{
				this.identity = null;
			}
			this.ResolveTypeIfPossible(propertyTable);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00041C54 File Offset: 0x0003FE54
		private bool CheckDispatch(ref Guid riid)
		{
			return this.dispatchEnabled && riid == InterfaceID.idIDispatch;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00041C74 File Offset: 0x0003FE74
		ComProxy IProxyCreator.CreateProxy(IntPtr outer, ref Guid riid)
		{
			if (outer == IntPtr.Zero)
			{
				throw Fx.AssertAndThrow("OuterProxy cannot be null");
			}
			if (this.contractType == null)
			{
				TypeCacheManager.Provider.FindOrCreateType(riid, out this.contractType, true, false);
			}
			if (this.contractType.GUID != riid && !this.CheckDispatch(ref riid))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidCastException(SR.GetString("NoInterface", new object[]
				{
					riid
				})));
			}
			Type proxiedType = EmitterCache.TypeEmitter.FindOrCreateType(this.contractType);
			ComProxy comProxy = null;
			TearOffProxy tearOffProxy = null;
			ComProxy result;
			try
			{
				tearOffProxy = new TearOffProxy(this, proxiedType);
				comProxy = ComProxy.Create(outer, tearOffProxy.GetTransparentProxy(), tearOffProxy);
				result = comProxy;
			}
			finally
			{
				if (comProxy == null && tearOffProxy != null)
				{
					((IDisposable)tearOffProxy).Dispose();
				}
			}
			return result;
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00041D5C File Offset: 0x0003FF5C
		bool IProxyCreator.SupportsErrorInfo(ref Guid riid)
		{
			return !(this.contractType == null) && (!(this.contractType.GUID != riid) || this.CheckDispatch(ref riid));
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00041D92 File Offset: 0x0003FF92
		bool IProxyCreator.SupportsDispatch()
		{
			return this.dispatchEnabled;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00041D9A File Offset: 0x0003FF9A
		bool IProxyCreator.SupportsIntrinsics()
		{
			return true;
		}

		// Token: 0x040019B7 RID: 6583
		private ServiceChannelFactory serviceChannelFactory;

		// Token: 0x040019B8 RID: 6584
		private Type contractType;

		// Token: 0x040019B9 RID: 6585
		private volatile RealProxy serviceProxy;

		// Token: 0x040019BA RID: 6586
		private ServiceEndpoint serviceEndpoint;

		// Token: 0x040019BB RID: 6587
		private KeyedByTypeCollection<IEndpointBehavior> behaviors = new KeyedByTypeCollection<IEndpointBehavior>();

		// Token: 0x040019BC RID: 6588
		private Binding binding;

		// Token: 0x040019BD RID: 6589
		private string configurationName;

		// Token: 0x040019BE RID: 6590
		private string address;

		// Token: 0x040019BF RID: 6591
		private EndpointIdentity identity;

		// Token: 0x040019C0 RID: 6592
		private bool dispatchEnabled;
	}
}
