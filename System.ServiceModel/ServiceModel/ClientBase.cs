using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x02000111 RID: 273
	[__DynamicallyInvokable]
	public abstract class ClientBase<TChannel> : ICommunicationObject, IDisposable where TChannel : class
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0001BF80 File Offset: 0x0001A180
		[__DynamicallyInvokable]
		protected ClientBase()
		{
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new ChannelFactory<TChannel>("*"));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>("*", null, null);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001C004 File Offset: 0x0001A204
		[__DynamicallyInvokable]
		protected ClientBase(string endpointConfigurationName)
		{
			if (endpointConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new ChannelFactory<TChannel>(endpointConfigurationName));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>(endpointConfigurationName, null, null);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001C094 File Offset: 0x0001A294
		[__DynamicallyInvokable]
		protected ClientBase(string endpointConfigurationName, string remoteAddress)
		{
			if (endpointConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
			}
			if (remoteAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("remoteAddress");
			}
			this.MakeCacheSettingReadOnly();
			EndpointAddress remoteAddress2 = new EndpointAddress(remoteAddress);
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new ChannelFactory<TChannel>(endpointConfigurationName, remoteAddress2));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>(endpointConfigurationName, remoteAddress2, null);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001C140 File Offset: 0x0001A340
		[__DynamicallyInvokable]
		protected ClientBase(string endpointConfigurationName, EndpointAddress remoteAddress)
		{
			if (endpointConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
			}
			if (remoteAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("remoteAddress");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new ChannelFactory<TChannel>(endpointConfigurationName, remoteAddress));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>(endpointConfigurationName, remoteAddress, null);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001C1E8 File Offset: 0x0001A3E8
		[__DynamicallyInvokable]
		protected ClientBase(Binding binding, EndpointAddress remoteAddress)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (remoteAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("remoteAddress");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOn)
			{
				this.endpointTrait = new ProgrammaticEndpointTrait<TChannel>(binding, remoteAddress, null);
				this.InitializeChannelFactoryRef();
				return;
			}
			this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new ChannelFactory<TChannel>(binding, remoteAddress));
			this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
			this.TryDisableSharing();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001C290 File Offset: 0x0001A490
		protected ClientBase(ServiceEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOn)
			{
				this.endpointTrait = new ServiceEndpointTrait<TChannel>(endpoint, null);
				this.InitializeChannelFactoryRef();
				return;
			}
			this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new ChannelFactory<TChannel>(endpoint));
			this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
			this.TryDisableSharing();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001C320 File Offset: 0x0001A520
		protected ClientBase(InstanceContext callbackInstance)
		{
			if (callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackInstance");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new DuplexChannelFactory<TChannel>(callbackInstance, "*"));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>("*", null, callbackInstance);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		protected ClientBase(InstanceContext callbackInstance, string endpointConfigurationName)
		{
			if (callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackInstance");
			}
			if (endpointConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new DuplexChannelFactory<TChannel>(callbackInstance, endpointConfigurationName));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>(endpointConfigurationName, null, callbackInstance);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001C45C File Offset: 0x0001A65C
		protected ClientBase(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
		{
			if (callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackInstance");
			}
			if (endpointConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
			}
			if (remoteAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("remoteAddress");
			}
			this.MakeCacheSettingReadOnly();
			EndpointAddress remoteAddress2 = new EndpointAddress(remoteAddress);
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new DuplexChannelFactory<TChannel>(callbackInstance, endpointConfigurationName, remoteAddress2));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>(endpointConfigurationName, remoteAddress2, callbackInstance);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001C51C File Offset: 0x0001A71C
		protected ClientBase(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
		{
			if (callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackInstance");
			}
			if (endpointConfigurationName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointConfigurationName");
			}
			if (remoteAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("remoteAddress");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOff)
			{
				this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new DuplexChannelFactory<TChannel>(callbackInstance, endpointConfigurationName, remoteAddress));
				this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
				this.TryDisableSharing();
				return;
			}
			this.endpointTrait = new ConfigurationEndpointTrait<TChannel>(endpointConfigurationName, remoteAddress, callbackInstance);
			this.InitializeChannelFactoryRef();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		protected ClientBase(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
		{
			if (callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackInstance");
			}
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (remoteAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("remoteAddress");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOn)
			{
				this.endpointTrait = new ProgrammaticEndpointTrait<TChannel>(binding, remoteAddress, callbackInstance);
				this.InitializeChannelFactoryRef();
				return;
			}
			this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new DuplexChannelFactory<TChannel>(callbackInstance, binding, remoteAddress));
			this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
			this.TryDisableSharing();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001C694 File Offset: 0x0001A894
		protected ClientBase(InstanceContext callbackInstance, ServiceEndpoint endpoint)
		{
			if (callbackInstance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("callbackInstance");
			}
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			this.MakeCacheSettingReadOnly();
			if (ClientBase<TChannel>.cacheSetting == CacheSetting.AlwaysOn)
			{
				this.endpointTrait = new ServiceEndpointTrait<TChannel>(endpoint, callbackInstance);
				this.InitializeChannelFactoryRef();
				return;
			}
			this.channelFactoryRef = new ChannelFactoryRef<TChannel>(new DuplexChannelFactory<TChannel>(callbackInstance, endpoint));
			this.channelFactoryRef.ChannelFactory.TraceOpenAndClose = false;
			this.TryDisableSharing();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001C738 File Offset: 0x0001A938
		[__DynamicallyInvokable]
		protected T GetDefaultValueForInitialization<T>()
		{
			return default(T);
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001C74E File Offset: 0x0001A94E
		private object ThisLock
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001C758 File Offset: 0x0001A958
		[__DynamicallyInvokable]
		protected TChannel Channel
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.channel == null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this.channel == null)
						{
							using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
							{
								if (DiagnosticUtility.ShouldUseActivity)
								{
									ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityOpenClientBase", new object[]
									{
										typeof(TChannel).FullName
									}), ActivityType.OpenClient);
								}
								if (this.useCachedFactory)
								{
									try
									{
										this.CreateChannelInternal();
										goto IL_D3;
									}
									catch (Exception ex)
									{
										if (this.useCachedFactory && (ex is CommunicationException || ex is ObjectDisposedException || ex is TimeoutException))
										{
											DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
											this.InvalidateCacheAndCreateChannel();
											goto IL_D3;
										}
										throw;
									}
								}
								this.CreateChannelInternal();
							}
						}
					}
				}
				IL_D3:
				return this.channel;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001C868 File Offset: 0x0001AA68
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001C870 File Offset: 0x0001AA70
		public static CacheSetting CacheSetting
		{
			get
			{
				return ClientBase<TChannel>.cacheSetting;
			}
			set
			{
				object obj = ClientBase<TChannel>.cacheLock;
				lock (obj)
				{
					if (ClientBase<TChannel>.isCacheSettingReadOnly && ClientBase<TChannel>.cacheSetting != value)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxImmutableClientBaseCacheSetting", new object[]
						{
							typeof(TChannel).ToString()
						})));
					}
					ClientBase<TChannel>.cacheSetting = value;
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001C8F0 File Offset: 0x0001AAF0
		[__DynamicallyInvokable]
		public ChannelFactory<TChannel> ChannelFactory
		{
			[__DynamicallyInvokable]
			get
			{
				if (ClientBase<TChannel>.cacheSetting == CacheSetting.Default)
				{
					this.TryDisableSharing();
				}
				return this.GetChannelFactory();
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x0001C905 File Offset: 0x0001AB05
		[__DynamicallyInvokable]
		public ClientCredentials ClientCredentials
		{
			[__DynamicallyInvokable]
			get
			{
				if (ClientBase<TChannel>.cacheSetting == CacheSetting.Default)
				{
					this.TryDisableSharing();
				}
				return this.ChannelFactory.Credentials;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001C920 File Offset: 0x0001AB20
		[__DynamicallyInvokable]
		public CommunicationState State
		{
			[__DynamicallyInvokable]
			get
			{
				IChannel channel = (IChannel)((object)this.channel);
				if (channel != null)
				{
					return channel.State;
				}
				if (!this.useCachedFactory)
				{
					return this.GetChannelFactory().State;
				}
				return CommunicationState.Created;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001C95D File Offset: 0x0001AB5D
		[__DynamicallyInvokable]
		public IClientChannel InnerChannel
		{
			[__DynamicallyInvokable]
			get
			{
				return (IClientChannel)((object)this.Channel);
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001C96F File Offset: 0x0001AB6F
		[__DynamicallyInvokable]
		public ServiceEndpoint Endpoint
		{
			[__DynamicallyInvokable]
			get
			{
				if (ClientBase<TChannel>.cacheSetting == CacheSetting.Default)
				{
					this.TryDisableSharing();
				}
				return this.GetChannelFactory().Endpoint;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001C989 File Offset: 0x0001AB89
		public void Open()
		{
			((ICommunicationObject)this).Open(this.GetChannelFactory().InternalOpenTimeout);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001C99C File Offset: 0x0001AB9C
		[__DynamicallyInvokable]
		public void Abort()
		{
			IChannel channel = (IChannel)((object)this.channel);
			if (channel != null)
			{
				channel.Abort();
			}
			if (!this.channelFactoryRefReleased)
			{
				object obj = ClientBase<TChannel>.staticLock;
				lock (obj)
				{
					if (!this.channelFactoryRefReleased)
					{
						if (this.channelFactoryRef.Release())
						{
							this.releasedLastRef = true;
						}
						this.channelFactoryRefReleased = true;
					}
				}
			}
			if (this.releasedLastRef)
			{
				this.channelFactoryRef.Abort();
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001CA30 File Offset: 0x0001AC30
		public void Close()
		{
			((ICommunicationObject)this).Close(this.GetChannelFactory().InternalCloseTimeout);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001CA43 File Offset: 0x0001AC43
		public void DisplayInitializationUI()
		{
			this.InnerChannel.DisplayInitializationUI();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001CA50 File Offset: 0x0001AC50
		private void MakeCacheSettingReadOnly()
		{
			if (ClientBase<TChannel>.isCacheSettingReadOnly)
			{
				return;
			}
			object obj = ClientBase<TChannel>.cacheLock;
			lock (obj)
			{
				ClientBase<TChannel>.isCacheSettingReadOnly = true;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001CA98 File Offset: 0x0001AC98
		private void CreateChannelInternal()
		{
			try
			{
				this.channel = this.CreateChannel();
				if (this.sharingFinalized && this.canShareFactory && !this.useCachedFactory)
				{
					this.TryAddChannelFactoryToCache();
				}
			}
			finally
			{
				if (!this.sharingFinalized && ClientBase<TChannel>.cacheSetting == CacheSetting.Default)
				{
					this.TryDisableSharing();
				}
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		[__DynamicallyInvokable]
		protected virtual TChannel CreateChannel()
		{
			if (this.sharingFinalized)
			{
				return this.GetChannelFactory().CreateChannel();
			}
			object obj = this.finalizeLock;
			TChannel result;
			lock (obj)
			{
				this.sharingFinalized = true;
				result = this.GetChannelFactory().CreateChannel();
			}
			return result;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001CB5C File Offset: 0x0001AD5C
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001CB64 File Offset: 0x0001AD64
		[__DynamicallyInvokable]
		void ICommunicationObject.Open(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!this.useCachedFactory)
			{
				this.GetChannelFactory().Open(timeoutHelper.RemainingTime());
			}
			this.InnerChannel.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		[__DynamicallyInvokable]
		void ICommunicationObject.Close(TimeSpan timeout)
		{
			using (ServiceModelActivity serviceModelActivity = DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.CreateBoundedActivity() : null)
			{
				if (DiagnosticUtility.ShouldUseActivity)
				{
					ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityCloseClientBase", new object[]
					{
						typeof(TChannel).FullName
					}), ActivityType.Close);
				}
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (this.channel != null)
				{
					this.InnerChannel.Close(timeoutHelper.RemainingTime());
				}
				if (!this.channelFactoryRefReleased)
				{
					object obj = ClientBase<TChannel>.staticLock;
					lock (obj)
					{
						if (!this.channelFactoryRefReleased)
						{
							if (this.channelFactoryRef.Release())
							{
								this.releasedLastRef = true;
							}
							this.channelFactoryRefReleased = true;
						}
					}
					if (this.releasedLastRef)
					{
						if (this.useCachedFactory)
						{
							this.channelFactoryRef.Abort();
						}
						else
						{
							this.channelFactoryRef.Close(timeoutHelper.RemainingTime());
						}
					}
				}
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060006A0 RID: 1696 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		// (remove) Token: 0x060006A1 RID: 1697 RVA: 0x0001CCCA File Offset: 0x0001AECA
		[__DynamicallyInvokable]
		event EventHandler ICommunicationObject.Closed
		{
			[__DynamicallyInvokable]
			add
			{
				this.InnerChannel.Closed += value;
			}
			[__DynamicallyInvokable]
			remove
			{
				this.InnerChannel.Closed -= value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060006A2 RID: 1698 RVA: 0x0001CCD8 File Offset: 0x0001AED8
		// (remove) Token: 0x060006A3 RID: 1699 RVA: 0x0001CCE6 File Offset: 0x0001AEE6
		[__DynamicallyInvokable]
		event EventHandler ICommunicationObject.Closing
		{
			[__DynamicallyInvokable]
			add
			{
				this.InnerChannel.Closing += value;
			}
			[__DynamicallyInvokable]
			remove
			{
				this.InnerChannel.Closing -= value;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060006A4 RID: 1700 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
		// (remove) Token: 0x060006A5 RID: 1701 RVA: 0x0001CD02 File Offset: 0x0001AF02
		[__DynamicallyInvokable]
		event EventHandler ICommunicationObject.Faulted
		{
			[__DynamicallyInvokable]
			add
			{
				this.InnerChannel.Faulted += value;
			}
			[__DynamicallyInvokable]
			remove
			{
				this.InnerChannel.Faulted -= value;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060006A6 RID: 1702 RVA: 0x0001CD10 File Offset: 0x0001AF10
		// (remove) Token: 0x060006A7 RID: 1703 RVA: 0x0001CD1E File Offset: 0x0001AF1E
		[__DynamicallyInvokable]
		event EventHandler ICommunicationObject.Opened
		{
			[__DynamicallyInvokable]
			add
			{
				this.InnerChannel.Opened += value;
			}
			[__DynamicallyInvokable]
			remove
			{
				this.InnerChannel.Opened -= value;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060006A8 RID: 1704 RVA: 0x0001CD2C File Offset: 0x0001AF2C
		// (remove) Token: 0x060006A9 RID: 1705 RVA: 0x0001CD3A File Offset: 0x0001AF3A
		[__DynamicallyInvokable]
		event EventHandler ICommunicationObject.Opening
		{
			[__DynamicallyInvokable]
			add
			{
				this.InnerChannel.Opening += value;
			}
			[__DynamicallyInvokable]
			remove
			{
				this.InnerChannel.Opening -= value;
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001CD48 File Offset: 0x0001AF48
		[__DynamicallyInvokable]
		IAsyncResult ICommunicationObject.BeginClose(AsyncCallback callback, object state)
		{
			return ((ICommunicationObject)this).BeginClose(this.GetChannelFactory().InternalCloseTimeout, callback, state);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001CD5D File Offset: 0x0001AF5D
		[__DynamicallyInvokable]
		IAsyncResult ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.BeginChannelClose), new ChainedEndHandler(this.EndChannelClose), new ChainedBeginHandler(this.BeginFactoryClose), new ChainedEndHandler(this.EndFactoryClose));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001CD97 File Offset: 0x0001AF97
		[__DynamicallyInvokable]
		void ICommunicationObject.EndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001CD9F File Offset: 0x0001AF9F
		[__DynamicallyInvokable]
		IAsyncResult ICommunicationObject.BeginOpen(AsyncCallback callback, object state)
		{
			return ((ICommunicationObject)this).BeginOpen(this.GetChannelFactory().InternalOpenTimeout, callback, state);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		[__DynamicallyInvokable]
		IAsyncResult ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.BeginFactoryOpen), new ChainedEndHandler(this.EndFactoryOpen), new ChainedBeginHandler(this.BeginChannelOpen), new ChainedEndHandler(this.EndChannelOpen));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001CDEE File Offset: 0x0001AFEE
		[__DynamicallyInvokable]
		void ICommunicationObject.EndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001CDF6 File Offset: 0x0001AFF6
		internal IAsyncResult BeginFactoryOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.useCachedFactory)
			{
				return new CompletedAsyncResult(callback, state);
			}
			return this.GetChannelFactory().BeginOpen(timeout, callback, state);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001CE16 File Offset: 0x0001B016
		internal void EndFactoryOpen(IAsyncResult result)
		{
			if (this.useCachedFactory)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			this.GetChannelFactory().EndOpen(result);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001CE33 File Offset: 0x0001B033
		internal IAsyncResult BeginChannelOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InnerChannel.BeginOpen(timeout, callback, state);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001CE43 File Offset: 0x0001B043
		internal void EndChannelOpen(IAsyncResult result)
		{
			this.InnerChannel.EndOpen(result);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001CE51 File Offset: 0x0001B051
		internal IAsyncResult BeginFactoryClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.useCachedFactory)
			{
				return new CompletedAsyncResult(callback, state);
			}
			return this.GetChannelFactory().BeginClose(timeout, callback, state);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001CE71 File Offset: 0x0001B071
		internal void EndFactoryClose(IAsyncResult result)
		{
			if (typeof(CompletedAsyncResult).IsAssignableFrom(result.GetType()))
			{
				CompletedAsyncResult.End(result);
				return;
			}
			this.GetChannelFactory().EndClose(result);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001CE9D File Offset: 0x0001B09D
		internal IAsyncResult BeginChannelClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.channel != null)
			{
				return this.InnerChannel.BeginClose(timeout, callback, state);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001CEC2 File Offset: 0x0001B0C2
		internal void EndChannelClose(IAsyncResult result)
		{
			if (typeof(CompletedAsyncResult).IsAssignableFrom(result.GetType()))
			{
				CompletedAsyncResult.End(result);
				return;
			}
			this.InnerChannel.EndClose(result);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001CEEE File Offset: 0x0001B0EE
		private ChannelFactory<TChannel> GetChannelFactory()
		{
			return this.channelFactoryRef.ChannelFactory;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		private void InitializeChannelFactoryRef()
		{
			object obj = ClientBase<TChannel>.staticLock;
			lock (obj)
			{
				ChannelFactoryRef<TChannel> channelFactoryRef;
				if (ClientBase<TChannel>.factoryRefCache.TryGetValue(this.endpointTrait, out channelFactoryRef))
				{
					if (channelFactoryRef.ChannelFactory.State == CommunicationState.Opened)
					{
						this.channelFactoryRef = channelFactoryRef;
						this.channelFactoryRef.AddRef();
						this.useCachedFactory = true;
						if (TD.ClientBaseChannelFactoryCacheHitIsEnabled())
						{
							TD.ClientBaseChannelFactoryCacheHit(this);
						}
						return;
					}
					ClientBase<TChannel>.factoryRefCache.Remove(this.endpointTrait);
				}
			}
			if (this.channelFactoryRef == null)
			{
				this.channelFactoryRef = ClientBase<TChannel>.CreateChannelFactoryRef(this.endpointTrait);
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001CFAC File Offset: 0x0001B1AC
		private static ChannelFactoryRef<TChannel> CreateChannelFactoryRef(EndpointTrait<TChannel> endpointTrait)
		{
			ChannelFactory<TChannel> channelFactory = endpointTrait.CreateChannelFactory();
			channelFactory.TraceOpenAndClose = false;
			return new ChannelFactoryRef<TChannel>(channelFactory);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001CFD0 File Offset: 0x0001B1D0
		private void TryDisableSharing()
		{
			if (this.sharingFinalized)
			{
				return;
			}
			object obj = this.finalizeLock;
			lock (obj)
			{
				if (this.sharingFinalized)
				{
					return;
				}
				this.canShareFactory = false;
				this.sharingFinalized = true;
				if (this.useCachedFactory)
				{
					ChannelFactoryRef<TChannel> channelFactoryRef = this.channelFactoryRef;
					this.channelFactoryRef = ClientBase<TChannel>.CreateChannelFactoryRef(this.endpointTrait);
					this.useCachedFactory = false;
					object obj2 = ClientBase<TChannel>.staticLock;
					lock (obj2)
					{
						if (!channelFactoryRef.Release())
						{
							channelFactoryRef = null;
						}
					}
					if (channelFactoryRef != null)
					{
						channelFactoryRef.Abort();
					}
				}
			}
			if (TD.ClientBaseUsingLocalChannelFactoryIsEnabled())
			{
				TD.ClientBaseUsingLocalChannelFactory(this);
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001D09C File Offset: 0x0001B29C
		private void TryAddChannelFactoryToCache()
		{
			object obj = ClientBase<TChannel>.staticLock;
			lock (obj)
			{
				ChannelFactoryRef<TChannel> channelFactoryRef;
				if (!ClientBase<TChannel>.factoryRefCache.TryGetValue(this.endpointTrait, out channelFactoryRef))
				{
					this.channelFactoryRef.AddRef();
					ClientBase<TChannel>.factoryRefCache.Add(this.endpointTrait, this.channelFactoryRef);
					this.useCachedFactory = true;
					if (TD.ClientBaseCachedChannelFactoryCountIsEnabled())
					{
						TD.ClientBaseCachedChannelFactoryCount(ClientBase<TChannel>.factoryRefCache.Count, 32, this);
					}
				}
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001D12C File Offset: 0x0001B32C
		private void InvalidateCacheAndCreateChannel()
		{
			this.RemoveFactoryFromCache();
			this.TryDisableSharing();
			this.CreateChannelInternal();
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001D140 File Offset: 0x0001B340
		private void RemoveFactoryFromCache()
		{
			object obj = ClientBase<TChannel>.staticLock;
			lock (obj)
			{
				ChannelFactoryRef<TChannel> channelFactoryRef;
				if (ClientBase<TChannel>.factoryRefCache.TryGetValue(this.endpointTrait, out channelFactoryRef) && this.channelFactoryRef == channelFactoryRef)
				{
					ClientBase<TChannel>.factoryRefCache.Remove(this.endpointTrait);
				}
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001D1A8 File Offset: 0x0001B3A8
		[__DynamicallyInvokable]
		protected void InvokeAsync(ClientBase<TChannel>.BeginOperationDelegate beginOperationDelegate, object[] inValues, ClientBase<TChannel>.EndOperationDelegate endOperationDelegate, SendOrPostCallback operationCompletedCallback, object userState)
		{
			if (beginOperationDelegate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("beginOperationDelegate");
			}
			if (endOperationDelegate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endOperationDelegate");
			}
			AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(userState);
			ClientBase<TChannel>.AsyncOperationContext asyncOperationContext = new ClientBase<TChannel>.AsyncOperationContext(asyncOperation, endOperationDelegate, operationCompletedCallback);
			Exception ex = null;
			object[] results = null;
			IAsyncResult asyncResult = null;
			try
			{
				asyncResult = beginOperationDelegate(inValues, ClientBase<TChannel>.onAsyncCallCompleted, asyncOperationContext);
				if (asyncResult.CompletedSynchronously)
				{
					results = endOperationDelegate(asyncResult);
				}
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex != null || asyncResult.CompletedSynchronously)
			{
				ClientBase<TChannel>.CompleteAsyncCall(asyncOperationContext, results, ex);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001D250 File Offset: 0x0001B450
		private static void OnAsyncCallCompleted(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ClientBase<TChannel>.AsyncOperationContext asyncOperationContext = (ClientBase<TChannel>.AsyncOperationContext)result.AsyncState;
			Exception error = null;
			object[] results = null;
			try
			{
				results = asyncOperationContext.EndDelegate(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				error = ex;
			}
			ClientBase<TChannel>.CompleteAsyncCall(asyncOperationContext, results, error);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		private static void CompleteAsyncCall(ClientBase<TChannel>.AsyncOperationContext context, object[] results, Exception error)
		{
			if (context.CompletionCallback != null)
			{
				ClientBase<TChannel>.InvokeAsyncCompletedEventArgs arg = new ClientBase<TChannel>.InvokeAsyncCompletedEventArgs(results, error, false, context.AsyncOperation.UserSuppliedState);
				context.AsyncOperation.PostOperationCompleted(context.CompletionCallback, arg);
				return;
			}
			context.AsyncOperation.OperationCompleted();
		}

		// Token: 0x04000A89 RID: 2697
		private TChannel channel;

		// Token: 0x04000A8A RID: 2698
		private ChannelFactoryRef<TChannel> channelFactoryRef;

		// Token: 0x04000A8B RID: 2699
		private EndpointTrait<TChannel> endpointTrait;

		// Token: 0x04000A8C RID: 2700
		private bool canShareFactory = true;

		// Token: 0x04000A8D RID: 2701
		private bool useCachedFactory;

		// Token: 0x04000A8E RID: 2702
		private bool sharingFinalized;

		// Token: 0x04000A8F RID: 2703
		private bool channelFactoryRefReleased;

		// Token: 0x04000A90 RID: 2704
		private bool releasedLastRef;

		// Token: 0x04000A91 RID: 2705
		private object syncRoot = new object();

		// Token: 0x04000A92 RID: 2706
		private object finalizeLock = new object();

		// Token: 0x04000A93 RID: 2707
		private const int maxNumChannelFactories = 32;

		// Token: 0x04000A94 RID: 2708
		private static ChannelFactoryRefCache<TChannel> factoryRefCache = new ChannelFactoryRefCache<TChannel>(32);

		// Token: 0x04000A95 RID: 2709
		private static object staticLock = new object();

		// Token: 0x04000A96 RID: 2710
		private static object cacheLock = new object();

		// Token: 0x04000A97 RID: 2711
		private static CacheSetting cacheSetting = CacheSetting.Default;

		// Token: 0x04000A98 RID: 2712
		private static bool isCacheSettingReadOnly;

		// Token: 0x04000A99 RID: 2713
		private static AsyncCallback onAsyncCallCompleted = Fx.ThunkCallback(new AsyncCallback(ClientBase<TChannel>.OnAsyncCallCompleted));

		// Token: 0x02000AE6 RID: 2790
		// (Invoke) Token: 0x06006EC2 RID: 28354
		[__DynamicallyInvokable]
		protected delegate IAsyncResult BeginOperationDelegate(object[] inValues, AsyncCallback asyncCallback, object state);

		// Token: 0x02000AE7 RID: 2791
		// (Invoke) Token: 0x06006EC6 RID: 28358
		[__DynamicallyInvokable]
		protected delegate object[] EndOperationDelegate(IAsyncResult result);

		// Token: 0x02000AE8 RID: 2792
		[__DynamicallyInvokable]
		protected class InvokeAsyncCompletedEventArgs : AsyncCompletedEventArgs
		{
			// Token: 0x06006EC9 RID: 28361 RVA: 0x0019C94B File Offset: 0x0019AB4B
			internal InvokeAsyncCompletedEventArgs(object[] results, Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
			{
				this.results = results;
			}

			// Token: 0x170019D7 RID: 6615
			// (get) Token: 0x06006ECA RID: 28362 RVA: 0x0019C95E File Offset: 0x0019AB5E
			[__DynamicallyInvokable]
			public object[] Results
			{
				[__DynamicallyInvokable]
				get
				{
					return this.results;
				}
			}

			// Token: 0x04003F2D RID: 16173
			private object[] results;
		}

		// Token: 0x02000AE9 RID: 2793
		private class AsyncOperationContext
		{
			// Token: 0x06006ECB RID: 28363 RVA: 0x0019C966 File Offset: 0x0019AB66
			internal AsyncOperationContext(AsyncOperation asyncOperation, ClientBase<TChannel>.EndOperationDelegate endDelegate, SendOrPostCallback completionCallback)
			{
				this.asyncOperation = asyncOperation;
				this.endDelegate = endDelegate;
				this.completionCallback = completionCallback;
			}

			// Token: 0x170019D8 RID: 6616
			// (get) Token: 0x06006ECC RID: 28364 RVA: 0x0019C983 File Offset: 0x0019AB83
			internal AsyncOperation AsyncOperation
			{
				get
				{
					return this.asyncOperation;
				}
			}

			// Token: 0x170019D9 RID: 6617
			// (get) Token: 0x06006ECD RID: 28365 RVA: 0x0019C98B File Offset: 0x0019AB8B
			internal ClientBase<TChannel>.EndOperationDelegate EndDelegate
			{
				get
				{
					return this.endDelegate;
				}
			}

			// Token: 0x170019DA RID: 6618
			// (get) Token: 0x06006ECE RID: 28366 RVA: 0x0019C993 File Offset: 0x0019AB93
			internal SendOrPostCallback CompletionCallback
			{
				get
				{
					return this.completionCallback;
				}
			}

			// Token: 0x04003F2E RID: 16174
			private AsyncOperation asyncOperation;

			// Token: 0x04003F2F RID: 16175
			private ClientBase<TChannel>.EndOperationDelegate endDelegate;

			// Token: 0x04003F30 RID: 16176
			private SendOrPostCallback completionCallback;
		}

		// Token: 0x02000AEA RID: 2794
		[__DynamicallyInvokable]
		protected class ChannelBase<T> : IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable, IOutputChannel, IRequestChannel, IChannelBaseProxy where T : class
		{
			// Token: 0x06006ECF RID: 28367 RVA: 0x0019C99C File Offset: 0x0019AB9C
			[__DynamicallyInvokable]
			protected ChannelBase(ClientBase<T> client)
			{
				if (client.Endpoint.Address == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryEndpointAddressUri")));
				}
				ChannelFactory<T> channelFactory = client.ChannelFactory;
				channelFactory.EnsureOpened();
				this.channel = channelFactory.ServiceChannelFactory.CreateServiceChannel(client.Endpoint.Address, client.Endpoint.Address.Uri);
				this.channel.InstanceContext = channelFactory.CallbackInstance;
				this.runtime = this.channel.ClientRuntime.GetRuntime();
			}

			// Token: 0x06006ED0 RID: 28368 RVA: 0x0019CA3C File Offset: 0x0019AC3C
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			protected IAsyncResult BeginInvoke(string methodName, object[] args, AsyncCallback callback, object state)
			{
				object[] array = new object[args.Length + 2];
				Array.Copy(args, array, args.Length);
				array[array.Length - 2] = callback;
				array[array.Length - 1] = state;
				IMethodCallMessage methodCall = new ClientBase<TChannel>.ChannelBase<T>.MethodCallMessage(array);
				ProxyOperationRuntime operationByName = this.GetOperationByName(methodName);
				object[] ins = operationByName.MapAsyncBeginInputs(methodCall, out callback, out state);
				return this.channel.BeginCall(operationByName.Action, operationByName.IsOneWay, operationByName, ins, callback, state);
			}

			// Token: 0x06006ED1 RID: 28369 RVA: 0x0019CAA8 File Offset: 0x0019ACA8
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			protected object EndInvoke(string methodName, object[] args, IAsyncResult result)
			{
				object[] array = new object[args.Length + 1];
				Array.Copy(args, array, args.Length);
				array[array.Length - 1] = result;
				IMethodCallMessage methodCall = new ClientBase<TChannel>.ChannelBase<T>.MethodCallMessage(array);
				ProxyOperationRuntime operationByName = this.GetOperationByName(methodName);
				object[] outs;
				operationByName.MapAsyncEndInputs(methodCall, out result, out outs);
				object result2 = this.channel.EndCall(operationByName.Action, outs, result);
				object[] array2 = operationByName.MapAsyncOutputs(methodCall, outs, ref result2);
				if (array2 != null)
				{
					Array.Copy(array2, args, args.Length);
				}
				return result2;
			}

			// Token: 0x06006ED2 RID: 28370 RVA: 0x0019CB20 File Offset: 0x0019AD20
			private ProxyOperationRuntime GetOperationByName(string methodName)
			{
				ProxyOperationRuntime operationByName = this.runtime.GetOperationByName(methodName);
				if (operationByName == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("SFxMethodNotSupported1", new object[]
					{
						methodName
					})));
				}
				return operationByName;
			}

			// Token: 0x170019DB RID: 6619
			// (get) Token: 0x06006ED3 RID: 28371 RVA: 0x0019CB62 File Offset: 0x0019AD62
			// (set) Token: 0x06006ED4 RID: 28372 RVA: 0x0019CB6F File Offset: 0x0019AD6F
			[__DynamicallyInvokable]
			bool IClientChannel.AllowInitializationUI
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IClientChannel)this.channel).AllowInitializationUI;
				}
				[__DynamicallyInvokable]
				set
				{
					((IClientChannel)this.channel).AllowInitializationUI = value;
				}
			}

			// Token: 0x170019DC RID: 6620
			// (get) Token: 0x06006ED5 RID: 28373 RVA: 0x0019CB7D File Offset: 0x0019AD7D
			[__DynamicallyInvokable]
			bool IClientChannel.DidInteractiveInitialization
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IClientChannel)this.channel).DidInteractiveInitialization;
				}
			}

			// Token: 0x170019DD RID: 6621
			// (get) Token: 0x06006ED6 RID: 28374 RVA: 0x0019CB8A File Offset: 0x0019AD8A
			[__DynamicallyInvokable]
			Uri IClientChannel.Via
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IClientChannel)this.channel).Via;
				}
			}

			// Token: 0x1400005C RID: 92
			// (add) Token: 0x06006ED7 RID: 28375 RVA: 0x0019CB97 File Offset: 0x0019AD97
			// (remove) Token: 0x06006ED8 RID: 28376 RVA: 0x0019CBA5 File Offset: 0x0019ADA5
			[__DynamicallyInvokable]
			event EventHandler<UnknownMessageReceivedEventArgs> IClientChannel.UnknownMessageReceived
			{
				[__DynamicallyInvokable]
				add
				{
					((IClientChannel)this.channel).UnknownMessageReceived += value;
				}
				[__DynamicallyInvokable]
				remove
				{
					((IClientChannel)this.channel).UnknownMessageReceived -= value;
				}
			}

			// Token: 0x06006ED9 RID: 28377 RVA: 0x0019CBB3 File Offset: 0x0019ADB3
			[__DynamicallyInvokable]
			void IClientChannel.DisplayInitializationUI()
			{
				((IClientChannel)this.channel).DisplayInitializationUI();
			}

			// Token: 0x06006EDA RID: 28378 RVA: 0x0019CBC0 File Offset: 0x0019ADC0
			[__DynamicallyInvokable]
			IAsyncResult IClientChannel.BeginDisplayInitializationUI(AsyncCallback callback, object state)
			{
				return ((IClientChannel)this.channel).BeginDisplayInitializationUI(callback, state);
			}

			// Token: 0x06006EDB RID: 28379 RVA: 0x0019CBCF File Offset: 0x0019ADCF
			[__DynamicallyInvokable]
			void IClientChannel.EndDisplayInitializationUI(IAsyncResult result)
			{
				((IClientChannel)this.channel).EndDisplayInitializationUI(result);
			}

			// Token: 0x170019DE RID: 6622
			// (get) Token: 0x06006EDC RID: 28380 RVA: 0x0019CBDD File Offset: 0x0019ADDD
			// (set) Token: 0x06006EDD RID: 28381 RVA: 0x0019CBEA File Offset: 0x0019ADEA
			[__DynamicallyInvokable]
			bool IContextChannel.AllowOutputBatching
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IContextChannel)this.channel).AllowOutputBatching;
				}
				[__DynamicallyInvokable]
				set
				{
					((IContextChannel)this.channel).AllowOutputBatching = value;
				}
			}

			// Token: 0x170019DF RID: 6623
			// (get) Token: 0x06006EDE RID: 28382 RVA: 0x0019CBF8 File Offset: 0x0019ADF8
			[__DynamicallyInvokable]
			IInputSession IContextChannel.InputSession
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IContextChannel)this.channel).InputSession;
				}
			}

			// Token: 0x170019E0 RID: 6624
			// (get) Token: 0x06006EDF RID: 28383 RVA: 0x0019CC05 File Offset: 0x0019AE05
			[__DynamicallyInvokable]
			EndpointAddress IContextChannel.LocalAddress
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IContextChannel)this.channel).LocalAddress;
				}
			}

			// Token: 0x170019E1 RID: 6625
			// (get) Token: 0x06006EE0 RID: 28384 RVA: 0x0019CC12 File Offset: 0x0019AE12
			// (set) Token: 0x06006EE1 RID: 28385 RVA: 0x0019CC1F File Offset: 0x0019AE1F
			[__DynamicallyInvokable]
			TimeSpan IContextChannel.OperationTimeout
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IContextChannel)this.channel).OperationTimeout;
				}
				[__DynamicallyInvokable]
				set
				{
					((IContextChannel)this.channel).OperationTimeout = value;
				}
			}

			// Token: 0x170019E2 RID: 6626
			// (get) Token: 0x06006EE2 RID: 28386 RVA: 0x0019CC2D File Offset: 0x0019AE2D
			[__DynamicallyInvokable]
			IOutputSession IContextChannel.OutputSession
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IContextChannel)this.channel).OutputSession;
				}
			}

			// Token: 0x170019E3 RID: 6627
			// (get) Token: 0x06006EE3 RID: 28387 RVA: 0x0019CC3A File Offset: 0x0019AE3A
			[__DynamicallyInvokable]
			EndpointAddress IContextChannel.RemoteAddress
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IContextChannel)this.channel).RemoteAddress;
				}
			}

			// Token: 0x170019E4 RID: 6628
			// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x0019CC47 File Offset: 0x0019AE47
			[__DynamicallyInvokable]
			string IContextChannel.SessionId
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IContextChannel)this.channel).SessionId;
				}
			}

			// Token: 0x06006EE5 RID: 28389 RVA: 0x0019CC54 File Offset: 0x0019AE54
			[__DynamicallyInvokable]
			TProperty IChannel.GetProperty<TProperty>()
			{
				return ((IChannel)this.channel).GetProperty<TProperty>();
			}

			// Token: 0x170019E5 RID: 6629
			// (get) Token: 0x06006EE6 RID: 28390 RVA: 0x0019CC61 File Offset: 0x0019AE61
			[__DynamicallyInvokable]
			CommunicationState ICommunicationObject.State
			{
				[__DynamicallyInvokable]
				get
				{
					return ((ICommunicationObject)this.channel).State;
				}
			}

			// Token: 0x1400005D RID: 93
			// (add) Token: 0x06006EE7 RID: 28391 RVA: 0x0019CC6E File Offset: 0x0019AE6E
			// (remove) Token: 0x06006EE8 RID: 28392 RVA: 0x0019CC7C File Offset: 0x0019AE7C
			[__DynamicallyInvokable]
			event EventHandler ICommunicationObject.Closed
			{
				[__DynamicallyInvokable]
				add
				{
					((ICommunicationObject)this.channel).Closed += value;
				}
				[__DynamicallyInvokable]
				remove
				{
					((ICommunicationObject)this.channel).Closed -= value;
				}
			}

			// Token: 0x1400005E RID: 94
			// (add) Token: 0x06006EE9 RID: 28393 RVA: 0x0019CC8A File Offset: 0x0019AE8A
			// (remove) Token: 0x06006EEA RID: 28394 RVA: 0x0019CC98 File Offset: 0x0019AE98
			[__DynamicallyInvokable]
			event EventHandler ICommunicationObject.Closing
			{
				[__DynamicallyInvokable]
				add
				{
					((ICommunicationObject)this.channel).Closing += value;
				}
				[__DynamicallyInvokable]
				remove
				{
					((ICommunicationObject)this.channel).Closing -= value;
				}
			}

			// Token: 0x1400005F RID: 95
			// (add) Token: 0x06006EEB RID: 28395 RVA: 0x0019CCA6 File Offset: 0x0019AEA6
			// (remove) Token: 0x06006EEC RID: 28396 RVA: 0x0019CCB4 File Offset: 0x0019AEB4
			[__DynamicallyInvokable]
			event EventHandler ICommunicationObject.Faulted
			{
				[__DynamicallyInvokable]
				add
				{
					((ICommunicationObject)this.channel).Faulted += value;
				}
				[__DynamicallyInvokable]
				remove
				{
					((ICommunicationObject)this.channel).Faulted -= value;
				}
			}

			// Token: 0x14000060 RID: 96
			// (add) Token: 0x06006EED RID: 28397 RVA: 0x0019CCC2 File Offset: 0x0019AEC2
			// (remove) Token: 0x06006EEE RID: 28398 RVA: 0x0019CCD0 File Offset: 0x0019AED0
			[__DynamicallyInvokable]
			event EventHandler ICommunicationObject.Opened
			{
				[__DynamicallyInvokable]
				add
				{
					((ICommunicationObject)this.channel).Opened += value;
				}
				[__DynamicallyInvokable]
				remove
				{
					((ICommunicationObject)this.channel).Opened -= value;
				}
			}

			// Token: 0x14000061 RID: 97
			// (add) Token: 0x06006EEF RID: 28399 RVA: 0x0019CCDE File Offset: 0x0019AEDE
			// (remove) Token: 0x06006EF0 RID: 28400 RVA: 0x0019CCEC File Offset: 0x0019AEEC
			[__DynamicallyInvokable]
			event EventHandler ICommunicationObject.Opening
			{
				[__DynamicallyInvokable]
				add
				{
					((ICommunicationObject)this.channel).Opening += value;
				}
				[__DynamicallyInvokable]
				remove
				{
					((ICommunicationObject)this.channel).Opening -= value;
				}
			}

			// Token: 0x06006EF1 RID: 28401 RVA: 0x0019CCFA File Offset: 0x0019AEFA
			[__DynamicallyInvokable]
			void ICommunicationObject.Abort()
			{
				((ICommunicationObject)this.channel).Abort();
			}

			// Token: 0x06006EF2 RID: 28402 RVA: 0x0019CD07 File Offset: 0x0019AF07
			[__DynamicallyInvokable]
			void ICommunicationObject.Close()
			{
				((ICommunicationObject)this.channel).Close();
			}

			// Token: 0x06006EF3 RID: 28403 RVA: 0x0019CD14 File Offset: 0x0019AF14
			[__DynamicallyInvokable]
			void ICommunicationObject.Close(TimeSpan timeout)
			{
				((ICommunicationObject)this.channel).Close(timeout);
			}

			// Token: 0x06006EF4 RID: 28404 RVA: 0x0019CD22 File Offset: 0x0019AF22
			[__DynamicallyInvokable]
			IAsyncResult ICommunicationObject.BeginClose(AsyncCallback callback, object state)
			{
				return ((ICommunicationObject)this.channel).BeginClose(callback, state);
			}

			// Token: 0x06006EF5 RID: 28405 RVA: 0x0019CD31 File Offset: 0x0019AF31
			[__DynamicallyInvokable]
			IAsyncResult ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ((ICommunicationObject)this.channel).BeginClose(timeout, callback, state);
			}

			// Token: 0x06006EF6 RID: 28406 RVA: 0x0019CD41 File Offset: 0x0019AF41
			[__DynamicallyInvokable]
			void ICommunicationObject.EndClose(IAsyncResult result)
			{
				((ICommunicationObject)this.channel).EndClose(result);
			}

			// Token: 0x06006EF7 RID: 28407 RVA: 0x0019CD4F File Offset: 0x0019AF4F
			[__DynamicallyInvokable]
			void ICommunicationObject.Open()
			{
				((ICommunicationObject)this.channel).Open();
			}

			// Token: 0x06006EF8 RID: 28408 RVA: 0x0019CD5C File Offset: 0x0019AF5C
			[__DynamicallyInvokable]
			void ICommunicationObject.Open(TimeSpan timeout)
			{
				((ICommunicationObject)this.channel).Open(timeout);
			}

			// Token: 0x06006EF9 RID: 28409 RVA: 0x0019CD6A File Offset: 0x0019AF6A
			[__DynamicallyInvokable]
			IAsyncResult ICommunicationObject.BeginOpen(AsyncCallback callback, object state)
			{
				return ((ICommunicationObject)this.channel).BeginOpen(callback, state);
			}

			// Token: 0x06006EFA RID: 28410 RVA: 0x0019CD79 File Offset: 0x0019AF79
			[__DynamicallyInvokable]
			IAsyncResult ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ((ICommunicationObject)this.channel).BeginOpen(timeout, callback, state);
			}

			// Token: 0x06006EFB RID: 28411 RVA: 0x0019CD89 File Offset: 0x0019AF89
			[__DynamicallyInvokable]
			void ICommunicationObject.EndOpen(IAsyncResult result)
			{
				((ICommunicationObject)this.channel).EndOpen(result);
			}

			// Token: 0x170019E6 RID: 6630
			// (get) Token: 0x06006EFC RID: 28412 RVA: 0x0019CD97 File Offset: 0x0019AF97
			[__DynamicallyInvokable]
			IExtensionCollection<IContextChannel> IExtensibleObject<IContextChannel>.Extensions
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IExtensibleObject<IContextChannel>)this.channel).Extensions;
				}
			}

			// Token: 0x06006EFD RID: 28413 RVA: 0x0019CDA4 File Offset: 0x0019AFA4
			[__DynamicallyInvokable]
			void IDisposable.Dispose()
			{
				((IDisposable)this.channel).Dispose();
			}

			// Token: 0x170019E7 RID: 6631
			// (get) Token: 0x06006EFE RID: 28414 RVA: 0x0019CDB1 File Offset: 0x0019AFB1
			[__DynamicallyInvokable]
			Uri IOutputChannel.Via
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IOutputChannel)this.channel).Via;
				}
			}

			// Token: 0x170019E8 RID: 6632
			// (get) Token: 0x06006EFF RID: 28415 RVA: 0x0019CDBE File Offset: 0x0019AFBE
			[__DynamicallyInvokable]
			EndpointAddress IOutputChannel.RemoteAddress
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IOutputChannel)this.channel).RemoteAddress;
				}
			}

			// Token: 0x06006F00 RID: 28416 RVA: 0x0019CDCB File Offset: 0x0019AFCB
			[__DynamicallyInvokable]
			void IOutputChannel.Send(Message message)
			{
				((IOutputChannel)this.channel).Send(message);
			}

			// Token: 0x06006F01 RID: 28417 RVA: 0x0019CDD9 File Offset: 0x0019AFD9
			[__DynamicallyInvokable]
			void IOutputChannel.Send(Message message, TimeSpan timeout)
			{
				((IOutputChannel)this.channel).Send(message, timeout);
			}

			// Token: 0x06006F02 RID: 28418 RVA: 0x0019CDE8 File Offset: 0x0019AFE8
			[__DynamicallyInvokable]
			IAsyncResult IOutputChannel.BeginSend(Message message, AsyncCallback callback, object state)
			{
				return ((IOutputChannel)this.channel).BeginSend(message, callback, state);
			}

			// Token: 0x06006F03 RID: 28419 RVA: 0x0019CDF8 File Offset: 0x0019AFF8
			[__DynamicallyInvokable]
			IAsyncResult IOutputChannel.BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ((IOutputChannel)this.channel).BeginSend(message, timeout, callback, state);
			}

			// Token: 0x06006F04 RID: 28420 RVA: 0x0019CE0A File Offset: 0x0019B00A
			[__DynamicallyInvokable]
			void IOutputChannel.EndSend(IAsyncResult result)
			{
				((IOutputChannel)this.channel).EndSend(result);
			}

			// Token: 0x170019E9 RID: 6633
			// (get) Token: 0x06006F05 RID: 28421 RVA: 0x0019CE18 File Offset: 0x0019B018
			[__DynamicallyInvokable]
			Uri IRequestChannel.Via
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IRequestChannel)this.channel).Via;
				}
			}

			// Token: 0x170019EA RID: 6634
			// (get) Token: 0x06006F06 RID: 28422 RVA: 0x0019CE25 File Offset: 0x0019B025
			[__DynamicallyInvokable]
			EndpointAddress IRequestChannel.RemoteAddress
			{
				[__DynamicallyInvokable]
				get
				{
					return ((IRequestChannel)this.channel).RemoteAddress;
				}
			}

			// Token: 0x06006F07 RID: 28423 RVA: 0x0019CE32 File Offset: 0x0019B032
			[__DynamicallyInvokable]
			Message IRequestChannel.Request(Message message)
			{
				return ((IRequestChannel)this.channel).Request(message);
			}

			// Token: 0x06006F08 RID: 28424 RVA: 0x0019CE40 File Offset: 0x0019B040
			[__DynamicallyInvokable]
			Message IRequestChannel.Request(Message message, TimeSpan timeout)
			{
				return ((IRequestChannel)this.channel).Request(message, timeout);
			}

			// Token: 0x06006F09 RID: 28425 RVA: 0x0019CE4F File Offset: 0x0019B04F
			[__DynamicallyInvokable]
			IAsyncResult IRequestChannel.BeginRequest(Message message, AsyncCallback callback, object state)
			{
				return ((IRequestChannel)this.channel).BeginRequest(message, callback, state);
			}

			// Token: 0x06006F0A RID: 28426 RVA: 0x0019CE5F File Offset: 0x0019B05F
			[__DynamicallyInvokable]
			IAsyncResult IRequestChannel.BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ((IRequestChannel)this.channel).BeginRequest(message, timeout, callback, state);
			}

			// Token: 0x06006F0B RID: 28427 RVA: 0x0019CE71 File Offset: 0x0019B071
			[__DynamicallyInvokable]
			Message IRequestChannel.EndRequest(IAsyncResult result)
			{
				return ((IRequestChannel)this.channel).EndRequest(result);
			}

			// Token: 0x06006F0C RID: 28428 RVA: 0x0019CE7F File Offset: 0x0019B07F
			ServiceChannel IChannelBaseProxy.GetServiceChannel()
			{
				return this.channel;
			}

			// Token: 0x04003F31 RID: 16177
			private ServiceChannel channel;

			// Token: 0x04003F32 RID: 16178
			private ImmutableClientRuntime runtime;

			// Token: 0x02000ED4 RID: 3796
			private class MethodCallMessage : IMethodCallMessage, IMethodMessage, IMessage
			{
				// Token: 0x0600847E RID: 33918 RVA: 0x001E9A00 File Offset: 0x001E7C00
				public MethodCallMessage(object[] args)
				{
					this.args = args;
				}

				// Token: 0x17001D2E RID: 7470
				// (get) Token: 0x0600847F RID: 33919 RVA: 0x001E9A0F File Offset: 0x001E7C0F
				public object[] Args
				{
					get
					{
						return this.args;
					}
				}

				// Token: 0x17001D2F RID: 7471
				// (get) Token: 0x06008480 RID: 33920 RVA: 0x001E9A17 File Offset: 0x001E7C17
				public int ArgCount
				{
					get
					{
						return this.args.Length;
					}
				}

				// Token: 0x17001D30 RID: 7472
				// (get) Token: 0x06008481 RID: 33921 RVA: 0x001E9A21 File Offset: 0x001E7C21
				public LogicalCallContext LogicalCallContext
				{
					get
					{
						return null;
					}
				}

				// Token: 0x06008482 RID: 33922 RVA: 0x001E9A24 File Offset: 0x001E7C24
				public object GetInArg(int argNum)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
				}

				// Token: 0x06008483 RID: 33923 RVA: 0x001E9A35 File Offset: 0x001E7C35
				public string GetInArgName(int index)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
				}

				// Token: 0x17001D31 RID: 7473
				// (get) Token: 0x06008484 RID: 33924 RVA: 0x001E9A46 File Offset: 0x001E7C46
				public int InArgCount
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x17001D32 RID: 7474
				// (get) Token: 0x06008485 RID: 33925 RVA: 0x001E9A57 File Offset: 0x001E7C57
				public object[] InArgs
				{
					get
					{
						return this.args;
					}
				}

				// Token: 0x06008486 RID: 33926 RVA: 0x001E9A5F File Offset: 0x001E7C5F
				public object GetArg(int argNum)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
				}

				// Token: 0x06008487 RID: 33927 RVA: 0x001E9A70 File Offset: 0x001E7C70
				public string GetArgName(int index)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
				}

				// Token: 0x17001D33 RID: 7475
				// (get) Token: 0x06008488 RID: 33928 RVA: 0x001E9A81 File Offset: 0x001E7C81
				public bool HasVarArgs
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x17001D34 RID: 7476
				// (get) Token: 0x06008489 RID: 33929 RVA: 0x001E9A92 File Offset: 0x001E7C92
				public MethodBase MethodBase
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x17001D35 RID: 7477
				// (get) Token: 0x0600848A RID: 33930 RVA: 0x001E9AA3 File Offset: 0x001E7CA3
				public string MethodName
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x17001D36 RID: 7478
				// (get) Token: 0x0600848B RID: 33931 RVA: 0x001E9AB4 File Offset: 0x001E7CB4
				public object MethodSignature
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x17001D37 RID: 7479
				// (get) Token: 0x0600848C RID: 33932 RVA: 0x001E9AC5 File Offset: 0x001E7CC5
				public string TypeName
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x17001D38 RID: 7480
				// (get) Token: 0x0600848D RID: 33933 RVA: 0x001E9AD6 File Offset: 0x001E7CD6
				public string Uri
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x17001D39 RID: 7481
				// (get) Token: 0x0600848E RID: 33934 RVA: 0x001E9AE7 File Offset: 0x001E7CE7
				public IDictionary Properties
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
					}
				}

				// Token: 0x04004CB7 RID: 19639
				private readonly object[] args;
			}
		}
	}
}
