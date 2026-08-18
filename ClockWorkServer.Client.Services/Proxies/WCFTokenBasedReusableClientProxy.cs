using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.Services.Adapters;
using TechnoPro.ClockWorkServer.Client.Services.Exceptions;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200016B RID: 363
	public abstract class WCFTokenBasedReusableClientProxy<TInterface> : WCFReusableClientProxy<TInterface> where TInterface : class, IService
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00024680 File Offset: 0x00022880
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00024688 File Offset: 0x00022888
		public bool AutomaticRelogin { get; set; }

		// Token: 0x06000E2A RID: 3626 RVA: 0x00024691 File Offset: 0x00022891
		protected WCFTokenBasedReusableClientProxy(string endpoint) : base(endpoint)
		{
			this.AutomaticRelogin = true;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000246A4 File Offset: 0x000228A4
		protected WCFTokenBasedReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
			this.AutomaticRelogin = true;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000246B8 File Offset: 0x000228B8
		protected override TResult WrapServiceMethod<TResult>(Func<TResult> serviceMethod)
		{
			for (int i = 0; i < base.NRetries; i++)
			{
				try
				{
					this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
					return serviceMethod();
				}
				catch (FaultException<HeaderNullFault>)
				{
					base.InnerChannel.SetSessionParametersHeader(ClientCredential.CurrentInstance.SessionTicket);
					bool flag = i == base.NRetries - 1;
					if (flag)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (FaultException<InvalidSessionIdentifierFault>)
				{
					bool automaticRelogin = this.AutomaticRelogin;
					if (automaticRelogin)
					{
						ClientCredential.CurrentInstance.Logon();
					}
					bool flag2 = i == base.NRetries - 1;
					if (flag2)
					{
						throw;
					}
				}
				catch (FaultException<PermissionDeniedFault>)
				{
					throw;
				}
				catch (FaultException ex)
				{
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex.ToString()), ex);
					bool flag3 = i == base.NRetries - 1;
					if (flag3)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (CommunicationException ex2)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex2.ToString()), ex2);
					bool flag4 = i == base.NRetries - 1;
					if (flag4)
					{
						throw new ConnectionFailedException(ex2.Message, ex2);
					}
				}
				catch (TimeoutException ex3)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex3.ToString()), ex3);
					bool flag5 = i == base.NRetries - 1;
					if (flag5)
					{
						throw new ConnectionFailedException(ex3.Message, ex3);
					}
				}
				catch (Exception ex4)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex4.ToString()), ex4);
					bool flag6 = i == base.NRetries - 1;
					if (flag6)
					{
						throw;
					}
				}
			}
			return serviceMethod();
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00024984 File Offset: 0x00022B84
		protected override void WrapServiceMethod(Action serviceMethod)
		{
			for (int i = 0; i < base.NRetries; i++)
			{
				try
				{
					this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
					serviceMethod();
					break;
				}
				catch (FaultException<HeaderNullFault>)
				{
					base.InnerChannel.SetSessionParametersHeader(ClientCredential.CurrentInstance.SessionTicket);
					bool flag = i == base.NRetries - 1;
					if (flag)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (FaultException<InvalidSessionIdentifierFault>)
				{
					bool automaticRelogin = this.AutomaticRelogin;
					if (automaticRelogin)
					{
						ClientCredential.CurrentInstance.Logon();
					}
					bool flag2 = i == base.NRetries - 1;
					if (flag2)
					{
						throw;
					}
				}
				catch (FaultException<PermissionDeniedFault>)
				{
					throw;
				}
				catch (FaultException ex)
				{
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex.ToString()), ex);
					bool flag3 = i == base.NRetries - 1;
					if (flag3)
					{
						this.CloseProxyBecauseOfException();
						throw;
					}
				}
				catch (CommunicationException ex2)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex2.ToString()), ex2);
					bool flag4 = i == base.NRetries - 1;
					if (flag4)
					{
						throw new ConnectionFailedException(ex2.Message, ex2);
					}
				}
				catch (TimeoutException ex3)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex3.ToString()), ex3);
					bool flag5 = i == base.NRetries - 1;
					if (flag5)
					{
						throw new ConnectionFailedException(ex3.Message, ex3);
					}
				}
				catch (Exception ex4)
				{
					this.CloseProxyBecauseOfException();
					CWLogger.Logger.ErrorException(string.Format("WCFTokenBasedReusableClientProxy::WrapServiceMethod ({0}): {1}", (serviceMethod.Method != null) ? serviceMethod.Method.Name : "NULL", ex4.ToString()), ex4);
					bool flag6 = i == base.NRetries - 1;
					if (flag6)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00024C44 File Offset: 0x00022E44
		protected override void VerifyProxyHeader(string methodName)
		{
			try
			{
				OperationData header = base.InnerChannel.GetHeader("operationDetails");
				bool flag = header == null;
				if (flag)
				{
					CWLogger.Logger.Error("WCFReusableClientProxy::VerifyProxyHeader: Check 1, proxy header for method '{0}' is NULL", methodName);
					base.InnerChannel.SetSessionParametersHeader(ClientCredential.CurrentInstance.SessionTicket);
					CWLogger.Logger.Trace("WCFReusableClientProxy::VerifyProxyHeader: proxy header for method '{0}' was set", methodName);
					OperationData header2 = base.InnerChannel.GetHeader("operationDetails");
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

		// Token: 0x06000E2F RID: 3631 RVA: 0x00024D04 File Offset: 0x00022F04
		protected override void AssureProxy()
		{
			bool manuallyClosed = this.manuallyClosed;
			if (manuallyClosed)
			{
				throw new ObjectDisposedException("This proxy was already closed.");
			}
			bool flag = this.cachedProxy == null;
			if (flag)
			{
				this.cachedProxy = this.CreateProxyInstance();
				base.Open();
				base.InnerChannel.SetSessionParametersHeader(ClientCredential.CurrentInstance.SessionTicket);
				base.InnerChannel.OperationTimeout = this.OperationTimeout;
				ClientCredential.CurrentInstance.OnSessionIdentifierChanged += this.OnSessionIdentifierChanged;
				base.OnProxyCreated();
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00024D97 File Offset: 0x00022F97
		protected void OnSessionIdentifierChanged(Token newticket)
		{
			base.InnerChannel.SetSessionParametersHeader(newticket);
			CWLogger.Logger.Trace("WCFTokenBasedReusableClientProxy::OnSessionIdentifierChanged::{0}: New Session Id = '{1}'", base.GetType().Name, newticket.SessionId);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00024DC8 File Offset: 0x00022FC8
		protected override void CloseProxyBecauseOfException()
		{
			ClientCredential.CurrentInstance.OnSessionIdentifierChanged -= this.OnSessionIdentifierChanged;
			base.CloseProxyBecauseOfException();
		}
	}
}
