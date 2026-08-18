using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.Services.Adapters;
using TechnoPro.ClockWorkServer.Client.Services.Exceptions;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000169 RID: 361
	public class WCFAsyncClientProxy<TInterface> : IClientBase, IDisposable, IConnectivity where TInterface : class, IService
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000238EB File Offset: 0x00021AEB
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x000238F3 File Offset: 0x00021AF3
		protected Binding Binding { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000238FC File Offset: 0x00021AFC
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x00023904 File Offset: 0x00021B04
		protected EndpointAddress EndpointAddress { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00023910 File Offset: 0x00021B10
		// (set) Token: 0x06000E00 RID: 3584 RVA: 0x00023928 File Offset: 0x00021B28
		public TimeSpan OperationTimeout
		{
			get
			{
				return this._operationTimeout;
			}
			set
			{
				this._operationTimeout = value;
				bool flag = this.InnerChannel != null;
				if (flag)
				{
					this.InnerChannel.OperationTimeout = value;
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x00023957 File Offset: 0x00021B57
		// (set) Token: 0x06000E02 RID: 3586 RVA: 0x0002395F File Offset: 0x00021B5F
		protected TInterface Proxy { get; set; }

		// Token: 0x06000E04 RID: 3588 RVA: 0x00023975 File Offset: 0x00021B75
		protected WCFAsyncClientProxy(string endpoint)
		{
			this.configName = endpoint;
			this.AssureProxy();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000239B4 File Offset: 0x00021BB4
		protected WCFAsyncClientProxy(Binding binding, EndpointAddress endpointAddress)
		{
			this.Binding = binding;
			this.EndpointAddress = endpointAddress;
			this.AssureProxy();
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00023A08 File Offset: 0x00021C08
		~WCFAsyncClientProxy()
		{
			this.Dispose(false);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00023A3C File Offset: 0x00021C3C
		protected virtual void VerifyProxyHeader(string methodName)
		{
			try
			{
				ClientParametersDTO header = this.InnerChannel.GetHeader("clientDetails");
				bool flag = header == null;
				if (flag)
				{
					CWLogger.Logger.Error("WCFReusableClientProxy::VerifyProxyHeader: Check 1, proxy header for method '{0}' is NULL", methodName);
					this.InnerChannel.SetClientParametersHeader();
					CWLogger.Logger.Trace("WCFReusableClientProxy::VerifyProxyHeader: proxy header for method '{0}' was set", methodName);
					ClientParametersDTO header2 = this.InnerChannel.GetHeader("clientDetails");
					bool flag2 = header2 == null;
					if (flag2)
					{
						CWLogger.Logger.Fatal("WCFReusableClientProxy::VerifyProxyHeader: Check 2, proxy header for method '{0}' is NULL. Failed to reset proxy header", methodName);
					}
				}
			}
			catch (Exception exception)
			{
				CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::VerifyProxyHeader: Exception checking proxy header for method '{0}'", methodName), exception);
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00023AF4 File Offset: 0x00021CF4
		protected void OnProxyCreated()
		{
			bool flag = this.ProxyCreated != null;
			if (flag)
			{
				this.ProxyCreated(this);
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00023B1E File Offset: 0x00021D1E
		protected virtual void AssureProxy()
		{
			this.Proxy = this.CreateProxyInstance();
			this.Open();
			this.InnerChannel.OperationTimeout = this.OperationTimeout;
			this.InnerChannel.SetClientParametersHeader();
			this.OnProxyCreated();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00023B5C File Offset: 0x00021D5C
		protected virtual TInterface CreateProxyInstanceByConfigName()
		{
			return (TInterface)((object)Activator.CreateInstance(WCFAsyncClientProxy<TInterface>._instanceType, new object[]
			{
				this.configName
			}));
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00023B90 File Offset: 0x00021D90
		protected virtual TInterface CreateProxyInstance()
		{
			return (TInterface)((object)Activator.CreateInstance(WCFAsyncClientProxy<TInterface>._instanceType, new object[]
			{
				this.Binding,
				this.EndpointAddress
			}));
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00023BCC File Offset: 0x00021DCC
		protected virtual void CloseProxyBecauseOfException()
		{
			bool flag = this.Proxy != null;
			if (flag)
			{
				ICommunicationObject communicationObject = this.Proxy as ICommunicationObject;
				try
				{
					bool flag2 = communicationObject != null;
					if (flag2)
					{
						bool flag3 = communicationObject.State != CommunicationState.Faulted;
						if (flag3)
						{
							communicationObject.Close();
						}
						else
						{
							communicationObject.Abort();
						}
					}
				}
				catch (CommunicationException)
				{
					bool flag4 = communicationObject != null;
					if (flag4)
					{
						communicationObject.Abort();
					}
				}
				catch (TimeoutException)
				{
					bool flag5 = communicationObject != null;
					if (flag5)
					{
						communicationObject.Abort();
					}
				}
				catch
				{
					bool flag6 = communicationObject != null;
					if (flag6)
					{
						communicationObject.Abort();
					}
					throw;
				}
				finally
				{
					this.Proxy = default(TInterface);
				}
			}
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00023CC0 File Offset: 0x00021EC0
		protected virtual TResult WrapServiceMethod<TResult>(Func<TResult> serviceMethod)
		{
			TResult result;
			try
			{
				this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
				result = serviceMethod();
			}
			catch (FaultException<PermissionDeniedFault> faultException)
			{
				throw;
			}
			catch (FaultException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex.ToString()), ex);
				throw;
			}
			catch (CommunicationException ex2)
			{
				this.CloseProxyBecauseOfException();
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex2.ToString()), ex2);
				throw new ConnectionFailedException(ex2.Message, ex2);
			}
			catch (TimeoutException ex3)
			{
				this.CloseProxyBecauseOfException();
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex3.ToString()), ex3);
				throw new ConnectionFailedException(ex3.Message, ex3);
			}
			catch (Exception ex4)
			{
				this.CloseProxyBecauseOfException();
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex4.ToString()), ex4);
				throw;
			}
			return result;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00023E84 File Offset: 0x00022084
		protected virtual void WrapServiceMethod(Action serviceMethod)
		{
			try
			{
				this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
				serviceMethod();
			}
			catch (FaultException<PermissionDeniedFault> faultException)
			{
				throw;
			}
			catch (FaultException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex.ToString()), ex);
				throw;
			}
			catch (CommunicationException ex2)
			{
				this.CloseProxyBecauseOfException();
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex2.ToString()), ex2);
				throw new ConnectionFailedException(ex2.Message, ex2);
			}
			catch (TimeoutException ex3)
			{
				this.CloseProxyBecauseOfException();
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex3.ToString()), ex3);
				throw new ConnectionFailedException(ex3.Message, ex3);
			}
			catch (Exception ex4)
			{
				this.CloseProxyBecauseOfException();
				CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex4.ToString()), ex4);
				throw;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000E0F RID: 3599 RVA: 0x00024044 File Offset: 0x00022244
		// (remove) Token: 0x06000E10 RID: 3600 RVA: 0x0002407C File Offset: 0x0002227C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ProxyCreatedHandler ProxyCreated;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x000240B4 File Offset: 0x000222B4
		public ClientCredentials ClientCredentials
		{
			get
			{
				return (this.Proxy != null) ? (this.Proxy as ClientBase<TInterface>).ClientCredentials : null;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x000240EC File Offset: 0x000222EC
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.CurrentEndpoint;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00024104 File Offset: 0x00022304
		public ServiceEndpoint CurrentEndpoint
		{
			get
			{
				return this.manuallyClosed ? null : ((this.Proxy == null) ? null : (this.Proxy as ClientBase<TInterface>).Endpoint);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00024148 File Offset: 0x00022348
		public IClientChannel InnerChannel
		{
			get
			{
				return (this.Proxy != null) ? (this.Proxy as ClientBase<TInterface>).InnerChannel : null;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00024180 File Offset: 0x00022380
		public CommunicationState State
		{
			get
			{
				return (this.Proxy != null) ? (this.Proxy as ICommunicationObject).State : CommunicationState.Closed;
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000241B8 File Offset: 0x000223B8
		public void Abort()
		{
			try
			{
				bool flag = this.Proxy != null;
				if (flag)
				{
					(this.Proxy as ICommunicationObject).Abort();
				}
			}
			finally
			{
				this.CloseProxyBecauseOfException();
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00024210 File Offset: 0x00022410
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002421C File Offset: 0x0002241C
		public void Open()
		{
			try
			{
				bool flag = this.Proxy != null;
				if (flag)
				{
					(this.Proxy as ICommunicationObject).Open();
				}
			}
			catch
			{
				this.CloseProxyBecauseOfException();
				throw;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00024274 File Offset: 0x00022474
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x0002427C File Offset: 0x0002247C
		public object Tag { get; set; }

		// Token: 0x06000E1B RID: 3611 RVA: 0x00024285 File Offset: 0x00022485
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00024298 File Offset: 0x00022498
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				try
				{
					this.CloseProxyBecauseOfException();
					this.manuallyClosed = true;
				}
				catch
				{
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000242F0 File Offset: 0x000224F0
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x04000019 RID: 25
		protected readonly string configName;

		// Token: 0x0400001A RID: 26
		protected volatile bool manuallyClosed;

		// Token: 0x0400001B RID: 27
		protected static readonly Type _instanceType = WCFClientProxy<TInterface>.GetInstanceType();

		// Token: 0x0400001E RID: 30
		private TimeSpan _operationTimeout = new TimeSpan(0, 10, 0);

		// Token: 0x0400001F RID: 31
		protected TimeSpan CheckConnectivityTimeout = new TimeSpan(0, 0, 10);

		// Token: 0x04000023 RID: 35
		private bool disposed = false;
	}
}
