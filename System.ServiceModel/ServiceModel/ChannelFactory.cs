using System;
using System.Configuration;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x020000E7 RID: 231
	[__DynamicallyInvokable]
	public abstract class ChannelFactory : CommunicationObject, IChannelFactory, ICommunicationObject, IDisposable
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0001674F File Offset: 0x0001494F
		[__DynamicallyInvokable]
		protected ChannelFactory()
		{
			TraceUtility.SetEtwProviderId();
			base.TraceOpenAndClose = true;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00016770 File Offset: 0x00014970
		[__DynamicallyInvokable]
		public ClientCredentials Credentials
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Endpoint == null)
				{
					return null;
				}
				if (base.State == CommunicationState.Created || base.State == CommunicationState.Opening)
				{
					return this.EnsureCredentials(this.Endpoint);
				}
				if (this.readOnlyClientCredentials == null)
				{
					ClientCredentials clientCredentials = new ClientCredentials();
					clientCredentials.MakeReadOnly();
					this.readOnlyClientCredentials = clientCredentials;
				}
				return this.readOnlyClientCredentials;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000167C6 File Offset: 0x000149C6
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultCloseTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Endpoint != null && this.Endpoint.Binding != null)
				{
					return this.Endpoint.Binding.CloseTimeout;
				}
				return ServiceDefaults.CloseTimeout;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x000167F3 File Offset: 0x000149F3
		[__DynamicallyInvokable]
		protected override TimeSpan DefaultOpenTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Endpoint != null && this.Endpoint.Binding != null)
				{
					return this.Endpoint.Binding.OpenTimeout;
				}
				return ServiceDefaults.OpenTimeout;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00016820 File Offset: 0x00014A20
		[__DynamicallyInvokable]
		public ServiceEndpoint Endpoint
		{
			[__DynamicallyInvokable]
			get
			{
				return this.serviceEndpoint;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00016828 File Offset: 0x00014A28
		internal IChannelFactory InnerFactory
		{
			get
			{
				return this.innerFactory;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00016830 File Offset: 0x00014A30
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00016838 File Offset: 0x00014A38
		internal bool UseActiveAutoClose { get; set; }

		// Token: 0x06000492 RID: 1170 RVA: 0x00016844 File Offset: 0x00014A44
		[__DynamicallyInvokable]
		protected internal void EnsureOpened()
		{
			base.ThrowIfDisposed();
			if (base.State != CommunicationState.Opened)
			{
				object obj = this.openLock;
				lock (obj)
				{
					if (base.State != CommunicationState.Opened)
					{
						base.Open();
					}
				}
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001689C File Offset: 0x00014A9C
		[__DynamicallyInvokable]
		protected virtual void ApplyConfiguration(string configurationName)
		{
			this.ApplyConfiguration(configurationName, null);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000168A8 File Offset: 0x00014AA8
		private void ApplyConfiguration(string configurationName, Configuration configuration)
		{
			if (this.Endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryCannotApplyConfigurationWithoutEndpoint")));
			}
			if (!this.Endpoint.IsFullyConfigured)
			{
				ConfigLoader configLoader;
				if (configuration != null)
				{
					configLoader = new ConfigLoader(configuration.EvaluationContext);
				}
				else
				{
					configLoader = new ConfigLoader();
				}
				if (configurationName == null)
				{
					configLoader.LoadCommonClientBehaviors(this.Endpoint);
					return;
				}
				configLoader.LoadChannelBehaviors(this.Endpoint, configurationName);
			}
		}

		// Token: 0x06000495 RID: 1173
		[__DynamicallyInvokable]
		protected abstract ServiceEndpoint CreateDescription();

		// Token: 0x06000496 RID: 1174 RVA: 0x00016918 File Offset: 0x00014B18
		internal EndpointAddress CreateEndpointAddress(ServiceEndpoint endpoint)
		{
			if (endpoint.Address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryEndpointAddressUri")));
			}
			return endpoint.Address;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00016948 File Offset: 0x00014B48
		[__DynamicallyInvokable]
		protected virtual IChannelFactory CreateFactory()
		{
			if (this.Endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryCannotCreateFactoryWithoutDescription")));
			}
			if (this.Endpoint.Binding != null)
			{
				return ServiceChannelFactory.BuildChannelFactory(this.Endpoint, this.UseActiveAutoClose);
			}
			if (this.configurationName != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryNoBindingFoundInConfig1", new object[]
				{
					this.configurationName
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxChannelFactoryNoBindingFoundInConfigOrCode")));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000169E0 File Offset: 0x00014BE0
		[__DynamicallyInvokable]
		void IDisposable.Dispose()
		{
			base.Close();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000169E8 File Offset: 0x00014BE8
		private void EnsureSecurityCredentialsManager(ServiceEndpoint endpoint)
		{
			if (endpoint.Behaviors.Find<SecurityCredentialsManager>() == null)
			{
				endpoint.Behaviors.Add(new ClientCredentials());
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00016A08 File Offset: 0x00014C08
		private ClientCredentials EnsureCredentials(ServiceEndpoint endpoint)
		{
			ClientCredentials clientCredentials = endpoint.Behaviors.Find<ClientCredentials>();
			if (clientCredentials == null)
			{
				clientCredentials = new ClientCredentials();
				endpoint.Behaviors.Add(clientCredentials);
			}
			return clientCredentials;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00016A38 File Offset: 0x00014C38
		[__DynamicallyInvokable]
		public T GetProperty<T>() where T : class
		{
			if (this.innerFactory != null)
			{
				return this.innerFactory.GetProperty<T>();
			}
			return default(T);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00016A64 File Offset: 0x00014C64
		internal bool HasDuplexOperations()
		{
			OperationDescriptionCollection operations = this.Endpoint.Contract.Operations;
			for (int i = 0; i < operations.Count; i++)
			{
				OperationDescription operationDescription = operations[i];
				if (operationDescription.IsServerInitiated())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00016AA8 File Offset: 0x00014CA8
		[__DynamicallyInvokable]
		protected void InitializeEndpoint(string configurationName, EndpointAddress address)
		{
			this.serviceEndpoint = this.CreateDescription();
			ServiceEndpoint serviceEndpoint = null;
			if (configurationName != null)
			{
				serviceEndpoint = ConfigLoader.LookupEndpoint(configurationName, address, this.serviceEndpoint.Contract);
			}
			if (serviceEndpoint != null)
			{
				this.serviceEndpoint = serviceEndpoint;
			}
			else
			{
				if (address != null)
				{
					this.Endpoint.Address = address;
				}
				this.ApplyConfiguration(configurationName);
			}
			this.configurationName = configurationName;
			this.EnsureSecurityCredentialsManager(this.serviceEndpoint);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00016B14 File Offset: 0x00014D14
		internal void InitializeEndpoint(string configurationName, EndpointAddress address, Configuration configuration)
		{
			this.serviceEndpoint = this.CreateDescription();
			ServiceEndpoint serviceEndpoint = null;
			if (configurationName != null)
			{
				serviceEndpoint = ConfigLoader.LookupEndpoint(configurationName, address, this.serviceEndpoint.Contract, configuration.EvaluationContext);
			}
			if (serviceEndpoint != null)
			{
				this.serviceEndpoint = serviceEndpoint;
			}
			else
			{
				if (address != null)
				{
					this.Endpoint.Address = address;
				}
				this.ApplyConfiguration(configurationName, configuration);
			}
			this.configurationName = configurationName;
			this.EnsureSecurityCredentialsManager(this.serviceEndpoint);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00016B87 File Offset: 0x00014D87
		[__DynamicallyInvokable]
		protected void InitializeEndpoint(ServiceEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpoint");
			}
			this.serviceEndpoint = endpoint;
			this.ApplyConfiguration(null);
			this.EnsureSecurityCredentialsManager(this.serviceEndpoint);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016BB8 File Offset: 0x00014DB8
		[__DynamicallyInvokable]
		protected void InitializeEndpoint(Binding binding, EndpointAddress address)
		{
			this.serviceEndpoint = this.CreateDescription();
			if (binding != null)
			{
				this.Endpoint.Binding = binding;
			}
			if (address != null)
			{
				this.Endpoint.Address = address;
			}
			this.ApplyConfiguration(null);
			this.EnsureSecurityCredentialsManager(this.serviceEndpoint);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00016C08 File Offset: 0x00014E08
		[__DynamicallyInvokable]
		protected override void OnOpened()
		{
			if (this.Endpoint != null)
			{
				ClientCredentials clientCredentials = this.Endpoint.Behaviors.Find<ClientCredentials>();
				if (clientCredentials != null)
				{
					ClientCredentials clientCredentials2 = clientCredentials.Clone();
					clientCredentials2.MakeReadOnly();
					this.readOnlyClientCredentials = clientCredentials2;
				}
			}
			base.OnOpened();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00016C4B File Offset: 0x00014E4B
		[__DynamicallyInvokable]
		protected override void OnAbort()
		{
			if (this.innerFactory != null)
			{
				this.innerFactory.Abort();
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016C60 File Offset: 0x00014E60
		[__DynamicallyInvokable]
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChannelFactory.CloseAsyncResult(this.innerFactory, timeout, callback, state);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00016C70 File Offset: 0x00014E70
		[__DynamicallyInvokable]
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChannelFactory.OpenAsyncResult(this.innerFactory, timeout, callback, state);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00016C80 File Offset: 0x00014E80
		[__DynamicallyInvokable]
		protected override void OnClose(TimeSpan timeout)
		{
			if (this.innerFactory != null)
			{
				this.innerFactory.Close(timeout);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00016C96 File Offset: 0x00014E96
		[__DynamicallyInvokable]
		protected override void OnEndClose(IAsyncResult result)
		{
			ChannelFactory.CloseAsyncResult.End(result);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016C9E File Offset: 0x00014E9E
		[__DynamicallyInvokable]
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChannelFactory.OpenAsyncResult.End(result);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00016CA6 File Offset: 0x00014EA6
		[__DynamicallyInvokable]
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerFactory.Open(timeout);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00016CB4 File Offset: 0x00014EB4
		[__DynamicallyInvokable]
		protected override void OnOpening()
		{
			base.OnOpening();
			this.innerFactory = this.CreateFactory();
			if (TD.ChannelFactoryCreatedIsEnabled())
			{
				TD.ChannelFactoryCreated(this);
			}
			if (this.innerFactory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("InnerChannelFactoryWasNotSet")));
			}
		}

		// Token: 0x04000A14 RID: 2580
		private string configurationName;

		// Token: 0x04000A15 RID: 2581
		private IChannelFactory innerFactory;

		// Token: 0x04000A16 RID: 2582
		private ServiceEndpoint serviceEndpoint;

		// Token: 0x04000A17 RID: 2583
		private ClientCredentials readOnlyClientCredentials;

		// Token: 0x04000A18 RID: 2584
		private object openLock = new object();

		// Token: 0x02000AD5 RID: 2773
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x06006E79 RID: 28281 RVA: 0x0019BCA0 File Offset: 0x00199EA0
			public OpenAsyncResult(ICommunicationObject communicationObject, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = communicationObject;
				if (this.communicationObject == null)
				{
					base.Complete(true);
					return;
				}
				IAsyncResult asyncResult = this.communicationObject.BeginOpen(timeout, ChannelFactory.OpenAsyncResult.onOpenComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.communicationObject.EndOpen(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x06006E7A RID: 28282 RVA: 0x0019BCFC File Offset: 0x00199EFC
			private static void OnOpenComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ChannelFactory.OpenAsyncResult openAsyncResult = (ChannelFactory.OpenAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					openAsyncResult.communicationObject.EndOpen(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				openAsyncResult.Complete(false, exception);
			}

			// Token: 0x06006E7B RID: 28283 RVA: 0x0019BD58 File Offset: 0x00199F58
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ChannelFactory.OpenAsyncResult>(result);
			}

			// Token: 0x04003F13 RID: 16147
			private ICommunicationObject communicationObject;

			// Token: 0x04003F14 RID: 16148
			private static AsyncCallback onOpenComplete = Fx.ThunkCallback(new AsyncCallback(ChannelFactory.OpenAsyncResult.OnOpenComplete));
		}

		// Token: 0x02000AD6 RID: 2774
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06006E7D RID: 28285 RVA: 0x0019BD7C File Offset: 0x00199F7C
			public CloseAsyncResult(ICommunicationObject communicationObject, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.communicationObject = communicationObject;
				if (this.communicationObject == null)
				{
					base.Complete(true);
					return;
				}
				IAsyncResult asyncResult = this.communicationObject.BeginClose(timeout, ChannelFactory.CloseAsyncResult.onCloseComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.communicationObject.EndClose(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x06006E7E RID: 28286 RVA: 0x0019BDD8 File Offset: 0x00199FD8
			private static void OnCloseComplete(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ChannelFactory.CloseAsyncResult closeAsyncResult = (ChannelFactory.CloseAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					closeAsyncResult.communicationObject.EndClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				closeAsyncResult.Complete(false, exception);
			}

			// Token: 0x06006E7F RID: 28287 RVA: 0x0019BE34 File Offset: 0x0019A034
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ChannelFactory.CloseAsyncResult>(result);
			}

			// Token: 0x04003F15 RID: 16149
			private ICommunicationObject communicationObject;

			// Token: 0x04003F16 RID: 16150
			private static AsyncCallback onCloseComplete = Fx.ThunkCallback(new AsyncCallback(ChannelFactory.CloseAsyncResult.OnCloseComplete));
		}
	}
}
