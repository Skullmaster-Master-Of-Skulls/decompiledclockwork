using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Client.CallbackContracts;
using TechnoPro.ClockWorkServer.Client.Services.Adapters;
using TechnoPro.ClockWorkServer.Client.Services.Exceptions;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F3 RID: 243
	public class MessagingReusableClientProxy : WCFTokenBasedReusableClientProxy<IMessaging>, IMessaging, IService
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00017D3A File Offset: 0x00015F3A
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00017D42 File Offset: 0x00015F42
		public IM_User CurrentUser { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00017D4B File Offset: 0x00015F4B
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00017D53 File Offset: 0x00015F53
		public MessagingCallback MessagingCallback { get; set; }

		// Token: 0x06000952 RID: 2386 RVA: 0x00017D5C File Offset: 0x00015F5C
		public MessagingReusableClientProxy(string endpoint) : this(new MessagingCallback(), endpoint)
		{
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00017D6C File Offset: 0x00015F6C
		public MessagingReusableClientProxy(MessagingCallback callback, string endpoint) : base(endpoint)
		{
			this.MessagingCallback = callback;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00017D7F File Offset: 0x00015F7F
		public MessagingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : this(new MessagingCallback(), binding, endpointAddress)
		{
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00017D90 File Offset: 0x00015F90
		public MessagingReusableClientProxy(MessagingCallback callback, Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
			this.MessagingCallback = callback;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public IM_User Login()
		{
			this.CurrentUser = null;
			this.CurrentUser = this.WrapServiceMethod<IM_User>(() => base.Proxy.Login());
			return this.CurrentUser;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00017DE0 File Offset: 0x00015FE0
		public void SendMessage(InstantMessage msg)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SendMessage(msg);
			});
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00017E18 File Offset: 0x00016018
		public void SendAttachment(AttachmentFile att)
		{
			TimeSpan operationTimeout = this.OperationTimeout;
			base.InnerChannel.OperationTimeout = new TimeSpan(0, 15, 0);
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SendAttachment(att);
			});
			base.InnerChannel.OperationTimeout = operationTimeout;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00017E78 File Offset: 0x00016078
		public List<IM_User> GetOnlineUsers()
		{
			return this.WrapServiceMethod<List<IM_User>>(() => base.Proxy.GetOnlineUsers());
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00017E9C File Offset: 0x0001609C
		public List<IM_User> GetOnlineUsers(OnlineUsersRequest onlineUsersRequest)
		{
			return this.WrapServiceMethod<List<IM_User>>(() => this.Proxy.GetOnlineUsers(onlineUsersRequest));
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00017ED4 File Offset: 0x000160D4
		public List<string> GetOnlineGroups()
		{
			return this.WrapServiceMethod<List<string>>(() => base.Proxy.GetOnlineGroups());
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00017EF8 File Offset: 0x000160F8
		public void Logout()
		{
			base.Proxy.Logout();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00017F08 File Offset: 0x00016108
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
				this.Login();
				ClientCredential.CurrentInstance.OnSessionIdentifierChanged += base.OnSessionIdentifierChanged;
				base.OnProxyCreated();
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00017FA0 File Offset: 0x000161A0
		protected override IMessaging CreateProxyInstanceByConfigName()
		{
			return (IMessaging)Activator.CreateInstance(WCFReusableClientProxy<IMessaging>._instanceType, new object[]
			{
				this.MessagingCallback,
				this.configName
			});
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00017FDC File Offset: 0x000161DC
		protected override IMessaging CreateProxyInstance()
		{
			return (IMessaging)Activator.CreateInstance(WCFReusableClientProxy<IMessaging>._instanceType, new object[]
			{
				this.MessagingCallback,
				base.Binding,
				base.EndpointAddress
			});
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00018020 File Offset: 0x00016220
		protected override void CloseProxyBecauseOfException()
		{
			ClientCredential.CurrentInstance.OnSessionIdentifierChanged -= base.OnSessionIdentifierChanged;
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
							try
							{
								this.Logout();
							}
							catch (FaultException<InvalidSessionIdentifierFault>)
							{
							}
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
					this.cachedProxy = null;
				}
			}
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00018134 File Offset: 0x00016334
		public override int CheckConnectivity()
		{
			TimeSpan operationTimeout = this.CheckConnectivityTimeout;
			int result;
			try
			{
				operationTimeout = base.InnerChannel.OperationTimeout;
				base.InnerChannel.OperationTimeout = this.CheckConnectivityTimeout;
				int num = base.Proxy.CheckConnectivity();
				base.InnerChannel.OperationTimeout = operationTimeout;
				result = num;
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
	}
}
