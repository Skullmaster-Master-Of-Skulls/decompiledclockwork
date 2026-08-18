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
using WCFExtrasPlus.Soap;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200016A RID: 362
	public class WCFTokenBasedAsyncClientProxy<TInterface> : WCFAsyncClientProxy<TInterface> where TInterface : class, IService
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00024303 File Offset: 0x00022503
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x0002430B File Offset: 0x0002250B
		public bool AutomaticRelogin { get; set; }

		// Token: 0x06000E20 RID: 3616 RVA: 0x00024314 File Offset: 0x00022514
		protected WCFTokenBasedAsyncClientProxy(string endpoint) : base(endpoint)
		{
			this.AutomaticRelogin = true;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00024327 File Offset: 0x00022527
		protected WCFTokenBasedAsyncClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00024334 File Offset: 0x00022534
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

		// Token: 0x06000E23 RID: 3619 RVA: 0x000243F4 File Offset: 0x000225F4
		protected override void AssureProxy()
		{
			base.Proxy = this.CreateProxyInstance();
			base.Open();
			base.InnerChannel.SetSessionParametersHeader(ClientCredential.CurrentInstance.SessionTicket);
			base.InnerChannel.OperationTimeout = base.OperationTimeout;
			ClientCredential.CurrentInstance.OnSessionIdentifierChanged += this.OnSessionIdentifierChanged;
			base.OnProxyCreated();
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002445C File Offset: 0x0002265C
		protected override void CloseProxyBecauseOfException()
		{
			ClientCredential.CurrentInstance.OnSessionIdentifierChanged -= this.OnSessionIdentifierChanged;
			base.CloseProxyBecauseOfException();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002447D File Offset: 0x0002267D
		protected void OnSessionIdentifierChanged(Token newticket)
		{
			base.InnerChannel.SetSessionParametersHeader(newticket);
			CWLogger.Logger.Trace("WCFTokenBasedAsyncClientProxy::OnSessionIdentifierChanged::{0}: New Session Id = '{1}'", base.GetType().Name, newticket.SessionId);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000244B0 File Offset: 0x000226B0
		protected override TResult WrapServiceMethod<TResult>(Func<TResult> serviceMethod)
		{
			TResult result;
			try
			{
				this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
				result = serviceMethod();
			}
			catch (FaultException<InvalidSessionIdentifierFault>)
			{
				bool automaticRelogin = this.AutomaticRelogin;
				if (!automaticRelogin)
				{
					throw;
				}
				ClientCredential.CurrentInstance.Logon();
				result = serviceMethod();
			}
			catch (FaultException)
			{
				throw;
			}
			catch (CommunicationException ex)
			{
				this.CloseProxyBecauseOfException();
				throw new ConnectionFailedException(ex.Message, ex);
			}
			catch (TimeoutException ex2)
			{
				this.CloseProxyBecauseOfException();
				throw new ConnectionFailedException(ex2.Message, ex2);
			}
			catch
			{
				this.CloseProxyBecauseOfException();
				throw;
			}
			return result;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00024598 File Offset: 0x00022798
		protected override void WrapServiceMethod(Action serviceMethod)
		{
			try
			{
				this.VerifyProxyHeader((serviceMethod.Method != null) ? (serviceMethod.Method.Name ?? string.Empty) : string.Empty);
				serviceMethod();
			}
			catch (FaultException<InvalidSessionIdentifierFault>)
			{
				bool automaticRelogin = this.AutomaticRelogin;
				if (!automaticRelogin)
				{
					throw;
				}
				ClientCredential.CurrentInstance.Logon();
				serviceMethod();
			}
			catch (FaultException)
			{
				throw;
			}
			catch (CommunicationException ex)
			{
				this.CloseProxyBecauseOfException();
				throw new ConnectionFailedException(ex.Message, ex);
			}
			catch (TimeoutException ex2)
			{
				this.CloseProxyBecauseOfException();
				throw new ConnectionFailedException(ex2.Message, ex2);
			}
			catch
			{
				this.CloseProxyBecauseOfException();
				throw;
			}
		}
	}
}
