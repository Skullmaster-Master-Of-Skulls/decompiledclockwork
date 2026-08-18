using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Client.CallbackContracts;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F4 RID: 244
	internal class MessagingClientBaseProxy : DuplexClientBase<IMessaging>, IMessaging, IService
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0001821F File Offset: 0x0001641F
		public MessagingClientBaseProxy(MessagingCallback callback, string endpoint) : base(callback, endpoint)
		{
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001822B File Offset: 0x0001642B
		public MessagingClientBaseProxy(MessagingCallback callback, Binding binding, EndpointAddress endpointAddress) : base(callback, binding, endpointAddress)
		{
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00018238 File Offset: 0x00016438
		public IM_User Login()
		{
			return base.Channel.Login();
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00018255 File Offset: 0x00016455
		public void SendMessage(InstantMessage msg)
		{
			base.Channel.SendMessage(msg);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00018265 File Offset: 0x00016465
		public void SendAttachment(AttachmentFile att)
		{
			base.Channel.SendAttachment(att);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00018278 File Offset: 0x00016478
		public List<IM_User> GetOnlineUsers()
		{
			return base.Channel.GetOnlineUsers();
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00018298 File Offset: 0x00016498
		public List<IM_User> GetOnlineUsers(OnlineUsersRequest onlineUsersRequest)
		{
			return base.Channel.GetOnlineUsers(onlineUsersRequest);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000182B8 File Offset: 0x000164B8
		public List<string> GetOnlineGroups()
		{
			return base.Channel.GetOnlineGroups();
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000182D5 File Offset: 0x000164D5
		public void Logout()
		{
			base.Channel.Logout();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000182E4 File Offset: 0x000164E4
		public int CheckConnectivity()
		{
			return base.Channel.CheckConnectivity();
		}
	}
}
