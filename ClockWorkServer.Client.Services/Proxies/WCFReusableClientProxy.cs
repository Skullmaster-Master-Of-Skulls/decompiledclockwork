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
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000168 RID: 360
	public abstract class WCFReusableClientProxy<TInterface> : IClientBase, IDisposable, IConnectivity where TInterface : class, IService
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00022CF4 File Offset: 0x00020EF4
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x00022CFC File Offset: 0x00020EFC
		protected Binding Binding { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00022D05 File Offset: 0x00020F05
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x00022D0D File Offset: 0x00020F0D
		protected EndpointAddress EndpointAddress { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00022D16 File Offset: 0x00020F16
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x00022D1E File Offset: 0x00020F1E
		protected bool IncludeProxyHeader { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00022D28 File Offset: 0x00020F28
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x00022D40 File Offset: 0x00020F40
		public int NRetries
		{
			get
			{
				return this._nRetries;
			}
			set
			{
				this._nRetries = ((value < 1) ? 1 : value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00022D54 File Offset: 0x00020F54
		protected TInterface Proxy
		{
			get
			{
				this.AssureProxy();
				return this.cachedProxy;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000DE0 RID: 3552 RVA: 0x00022D74 File Offset: 0x00020F74
		// (remove) Token: 0x06000DE1 RID: 3553 RVA: 0x00022DAC File Offset: 0x00020FAC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event ProxyCreatedHandler ProxyCreated;

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00022DF0 File Offset: 0x00020FF0
		protected WCFReusableClientProxy(string endpoint)
		{
			this.NRetries = 3;
			this.configName = endpoint;
			this.IncludeProxyHeader = true;
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00022E44 File Offset: 0x00021044
		protected WCFReusableClientProxy(Binding binding, EndpointAddress endpointAddress)
		{
			this.NRetries = 3;
			this.Binding = binding;
			this.EndpointAddress = endpointAddress;
			this.IncludeProxyHeader = true;
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00022EA0 File Offset: 0x000210A0
		~WCFReusableClientProxy()
		{
			this.Dispose(false);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00022ED4 File Offset: 0x000210D4
		protected void OnProxyCreated()
		{
			ProxyCreatedHandler proxyCreated = this.ProxyCreated;
			bool flag = proxyCreated != null;
			if (flag)
			{
				proxyCreated(this);
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00022EFC File Offset: 0x000210FC
		protected virtual void VerifyProxyHeader(string methodName)
		{
			bool flag = !this.IncludeProxyHeader;
			if (!flag)
			{
				try
				{
					ClientParametersDTO header = this.InnerChannel.GetHeader("clientDetails");
					bool flag2 = header == null;
					if (flag2)
					{
						CWLogger.Logger.Error("WCFReusableClientProxy::VerifyProxyHeader: Check 1, proxy header for method '{0}' is NULL", methodName);
						this.InnerChannel.SetClientParametersHeader();
						CWLogger.Logger.Trace("WCFReusableClientProxy::VerifyProxyHeader: proxy header for method '{0}' was set", methodName);
						ClientParametersDTO header2 = this.InnerChannel.GetHeader("clientDetails");
						bool flag3 = header2 == null;
						if (flag3)
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
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00022FC8 File Offset: 0x000211C8
		protected virtual TResult WrapServiceMethod<TResult>(Func<TResult> serviceMethod)
		{
			for (int i = 0; i < this.NRetries; i++)
			{
				try
				{
					this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
					return serviceMethod();
				}
				catch (FaultException<HeaderNullFault>)
				{
					bool includeProxyHeader = this.IncludeProxyHeader;
					if (includeProxyHeader)
					{
						this.InnerChannel.SetClientParametersHeader();
					}
					bool flag = i == this.NRetries - 1;
					if (flag)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (FaultException<PermissionDeniedFault>)
				{
					throw;
				}
				catch (FaultException ex)
				{
					CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex.ToString()), ex);
					bool flag2 = i == this.NRetries - 1;
					if (flag2)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (CommunicationException ex2)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex2.ToString()), ex2);
					bool flag3 = i == this.NRetries - 1;
					if (flag3)
					{
						throw new ConnectionFailedException(ex2.Message, ex2);
					}
				}
				catch (TimeoutException ex3)
				{
					this.CloseProxyBecauseOfException();
					bool flag4 = i == this.NRetries - 1;
					if (flag4)
					{
						throw new ConnectionFailedException(ex3.Message, ex3);
					}
				}
				catch (Exception ex4)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex4.ToString()), ex4);
					bool flag5 = i == this.NRetries - 1;
					if (flag5)
					{
						throw;
					}
				}
			}
			return serviceMethod();
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00023218 File Offset: 0x00021418
		protected virtual void WrapServiceMethod(Action serviceMethod)
		{
			for (int i = 0; i < this.NRetries; i++)
			{
				try
				{
					this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
					serviceMethod();
					break;
				}
				catch (FaultException<HeaderNullFault>)
				{
					bool includeProxyHeader = this.IncludeProxyHeader;
					if (includeProxyHeader)
					{
						this.InnerChannel.SetClientParametersHeader();
					}
					bool flag = i == this.NRetries - 1;
					if (flag)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (FaultException<PermissionDeniedFault>)
				{
					throw;
				}
				catch (FaultException ex)
				{
					CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex.ToString()), ex);
					bool flag2 = i == this.NRetries - 1;
					if (flag2)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (CommunicationException ex2)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex2.ToString()), ex2);
					bool flag3 = i == this.NRetries - 1;
					if (flag3)
					{
						throw new ConnectionFailedException(ex2.Message, ex2);
					}
				}
				catch (TimeoutException ex3)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex3.ToString()), ex3);
					bool flag4 = i == this.NRetries - 1;
					if (flag4)
					{
						throw new ConnectionFailedException(ex3.Message, ex3);
					}
				}
				catch (Exception ex4)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex4.ToString()), ex4);
					bool flag5 = i == this.NRetries - 1;
					if (flag5)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0002349C File Offset: 0x0002169C
		protected virtual void CloseProxyBecauseOfException()
		{
			bool flag = this.cachedProxy != null;
			if (flag)
			{
				ICommunicationObject communicationObject = this.cachedProxy as ICommunicationObject;
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
					this.cachedProxy = default(TInterface);
				}
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002358C File Offset: 0x0002178C
		protected virtual void AssureProxy()
		{
			bool flag = this.manuallyClosed;
			if (flag)
			{
				throw new ObjectDisposedException("This proxy was already closed.");
			}
			bool flag2;
			try
			{
				flag2 = (this.cachedProxy == null);
			}
			catch
			{
				flag2 = true;
			}
			bool flag3 = flag2;
			if (flag3)
			{
				this.cachedProxy = this.CreateProxyInstance();
				this.Open();
				this.InnerChannel.OperationTimeout = this.OperationTimeout;
				bool includeProxyHeader = this.IncludeProxyHeader;
				if (includeProxyHeader)
				{
					this.InnerChannel.SetClientParametersHeader();
				}
				this.OnProxyCreated();
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00023628 File Offset: 0x00021828
		protected virtual TInterface CreateProxyInstanceByConfigName()
		{
			return (TInterface)((object)Activator.CreateInstance(WCFReusableClientProxy<TInterface>._instanceType, new object[]
			{
				this.configName
			}));
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0002365C File Offset: 0x0002185C
		protected virtual TInterface CreateProxyInstance()
		{
			return (TInterface)((object)Activator.CreateInstance(WCFReusableClientProxy<TInterface>._instanceType, new object[]
			{
				this.Binding,
				this.EndpointAddress
			}));
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00023698 File Offset: 0x00021898
		public ClientCredentials ClientCredentials
		{
			get
			{
				return (this.Proxy as ClientBase<TInterface>).ClientCredentials;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x000236C0 File Offset: 0x000218C0
		public ServiceEndpoint Endpoint
		{
			get
			{
				bool flag = this.manuallyClosed;
				ServiceEndpoint result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (this.Proxy as ClientBase<TInterface>).Endpoint;
				}
				return result;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x000236F8 File Offset: 0x000218F8
		public ServiceEndpoint CurrentEndpoint
		{
			get
			{
				bool flag = this.manuallyClosed;
				ServiceEndpoint result;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = this.cachedProxy == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = (this.cachedProxy as ClientBase<TInterface>).Endpoint;
					}
				}
				return result;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00023748 File Offset: 0x00021948
		public IClientChannel InnerChannel
		{
			get
			{
				ClientBase<TInterface> clientBase = this.Proxy as ClientBase<TInterface>;
				return clientBase.InnerChannel;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00023774 File Offset: 0x00021974
		public CommunicationState State
		{
			get
			{
				bool flag = this.cachedProxy != null;
				CommunicationState result;
				if (flag)
				{
					result = (this.cachedProxy as ICommunicationObject).State;
				}
				else
				{
					result = CommunicationState.Closed;
				}
				return result;
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000237B4 File Offset: 0x000219B4
		public void Abort()
		{
			try
			{
				bool flag = this.cachedProxy != null;
				if (flag)
				{
					(this.cachedProxy as ICommunicationObject).Abort();
				}
			}
			finally
			{
				this.CloseProxyBecauseOfException();
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002380C File Offset: 0x00021A0C
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00023818 File Offset: 0x00021A18
		public void Open()
		{
			try
			{
				(this.Proxy as ICommunicationObject).Open();
			}
			catch
			{
				this.CloseProxyBecauseOfException();
				throw;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0002385C File Offset: 0x00021A5C
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00023864 File Offset: 0x00021A64
		public object Tag { get; set; }

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0002386D File Offset: 0x00021A6D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00023880 File Offset: 0x00021A80
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

		// Token: 0x06000DFA RID: 3578 RVA: 0x000238D8 File Offset: 0x00021AD8
		public virtual int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0400000C RID: 12
		protected TInterface cachedProxy;

		// Token: 0x0400000D RID: 13
		protected readonly string configName;

		// Token: 0x0400000E RID: 14
		protected volatile bool manuallyClosed;

		// Token: 0x0400000F RID: 15
		protected static readonly Type _instanceType = WCFClientProxy<TInterface>.GetInstanceType();

		// Token: 0x04000013 RID: 19
		protected TimeSpan CheckConnectivityTimeout = new TimeSpan(0, 0, 10);

		// Token: 0x04000014 RID: 20
		public TimeSpan OperationTimeout = new TimeSpan(0, 10, 0);

		// Token: 0x04000017 RID: 23
		private bool disposed = false;

		// Token: 0x04000018 RID: 24
		private int _nRetries;
	}
}
