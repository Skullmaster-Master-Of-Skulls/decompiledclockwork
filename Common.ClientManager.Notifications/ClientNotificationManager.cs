using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.CallbackContracts;
using TechnoPro.ClockWorkServer.Client.Messaging.Core;
using TechnoPro.ClockWorkServer.Client.Services.Exceptions;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Notifications.Entities;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Notifications
{
	// Token: 0x02000002 RID: 2
	public class ClientNotificationManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static ClientNotificationManager CurrentInstance
		{
			get
			{
				if (ClientNotificationManager._currentInstance == null)
				{
					ClientNotificationManager._currentInstance = new ClientNotificationManager();
				}
				return ClientNotificationManager._currentInstance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public IM_User CurrentUser
		{
			get
			{
				if (!ObjectFactory.Resolve<ClientCache>().IsClockWorkServerEnable)
				{
					return null;
				}
				return MessagingManager.CurrentInstance.MessagingClient.CurrentUser;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002087 File Offset: 0x00000287
		public ClientNotificationManager()
		{
			if (ObjectFactory.Resolve<ClientCache>().IsClockWorkServerEnable)
			{
				MessagingManager.CurrentInstance.OnMessageDelivery += delegate(InstantMessage msg)
				{
					ClientNotificationManager.<<-ctor>b__5_0>d <<-ctor>b__5_0>d;
					<<-ctor>b__5_0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
					<<-ctor>b__5_0>d.<>4__this = this;
					<<-ctor>b__5_0>d.msg = msg;
					<<-ctor>b__5_0>d.<>1__state = -1;
					<<-ctor>b__5_0>d.<>t__builder.Start<ClientNotificationManager.<<-ctor>b__5_0>d>(ref <<-ctor>b__5_0>d);
				};
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000004 RID: 4 RVA: 0x000020B4 File Offset: 0x000002B4
		// (remove) Token: 0x06000005 RID: 5 RVA: 0x000020EC File Offset: 0x000002EC
		public event UserMessageReceivedHandler OnUserMessageReceived;

		// Token: 0x06000006 RID: 6 RVA: 0x00002124 File Offset: 0x00000324
		private void FireOnUserMessageReceived(InstantMessage msg)
		{
			UserMessageReceivedHandler onUserMessageReceived = this.OnUserMessageReceived;
			if (onUserMessageReceived != null)
			{
				onUserMessageReceived(new MessageEventArgs
				{
					InstantMessage = msg
				});
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002188 File Offset: 0x00000388
		public event OnMessageDeliveryEventHandler OnInstantMessageDelivered;

		// Token: 0x06000009 RID: 9 RVA: 0x000021C0 File Offset: 0x000003C0
		private void FireOnInstantMessageDelivered(InstantMessage msg)
		{
			OnMessageDeliveryEventHandler onInstantMessageDelivered = this.OnInstantMessageDelivered;
			if (onInstantMessageDelivered != null)
			{
				onInstantMessageDelivered(msg);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E0 File Offset: 0x000003E0
		private Task CurrentInstance_OnMessageDelivery(InstantMessage msg)
		{
			ClientNotificationManager.<CurrentInstance_OnMessageDelivery>d__14 <CurrentInstance_OnMessageDelivery>d__;
			<CurrentInstance_OnMessageDelivery>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CurrentInstance_OnMessageDelivery>d__.<>4__this = this;
			<CurrentInstance_OnMessageDelivery>d__.msg = msg;
			<CurrentInstance_OnMessageDelivery>d__.<>1__state = -1;
			<CurrentInstance_OnMessageDelivery>d__.<>t__builder.Start<ClientNotificationManager.<CurrentInstance_OnMessageDelivery>d__14>(ref <CurrentInstance_OnMessageDelivery>d__);
			return <CurrentInstance_OnMessageDelivery>d__.<>t__builder.Task;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000222C File Offset: 0x0000042C
		private Task SendPrivateMessageAsync(ClientNotificationManager.PrivateMessage privateMessage)
		{
			ClientNotificationManager.<SendPrivateMessageAsync>d__15 <SendPrivateMessageAsync>d__;
			<SendPrivateMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendPrivateMessageAsync>d__.<>4__this = this;
			<SendPrivateMessageAsync>d__.privateMessage = privateMessage;
			<SendPrivateMessageAsync>d__.<>1__state = -1;
			<SendPrivateMessageAsync>d__.<>t__builder.Start<ClientNotificationManager.<SendPrivateMessageAsync>d__15>(ref <SendPrivateMessageAsync>d__);
			return <SendPrivateMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002278 File Offset: 0x00000478
		internal Task SendMessageToEveryoneAsync(MessageContent content)
		{
			ClientNotificationManager.<SendMessageToEveryoneAsync>d__16 <SendMessageToEveryoneAsync>d__;
			<SendMessageToEveryoneAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendMessageToEveryoneAsync>d__.<>4__this = this;
			<SendMessageToEveryoneAsync>d__.content = content;
			<SendMessageToEveryoneAsync>d__.<>1__state = -1;
			<SendMessageToEveryoneAsync>d__.<>t__builder.Start<ClientNotificationManager.<SendMessageToEveryoneAsync>d__16>(ref <SendMessageToEveryoneAsync>d__);
			return <SendMessageToEveryoneAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C4 File Offset: 0x000004C4
		private Task SendMessageToEveryoneAsync(MessageParameters parameters, string messageText = "")
		{
			ClientNotificationManager.<SendMessageToEveryoneAsync>d__17 <SendMessageToEveryoneAsync>d__;
			<SendMessageToEveryoneAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendMessageToEveryoneAsync>d__.<>4__this = this;
			<SendMessageToEveryoneAsync>d__.parameters = parameters;
			<SendMessageToEveryoneAsync>d__.messageText = messageText;
			<SendMessageToEveryoneAsync>d__.<>1__state = -1;
			<SendMessageToEveryoneAsync>d__.<>t__builder.Start<ClientNotificationManager.<SendMessageToEveryoneAsync>d__17>(ref <SendMessageToEveryoneAsync>d__);
			return <SendMessageToEveryoneAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002318 File Offset: 0x00000518
		public bool IsMessageFromMyself(InstantMessage msg)
		{
			string a = (msg == null || msg.From == null || msg.From.Username == null) ? "" : msg.From.Username;
			IM_User currentUser = ClientNotificationManager.CurrentInstance.CurrentUser;
			return currentUser != null && a == currentUser.Username;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000236C File Offset: 0x0000056C
		public Task SendPrivateMessageAsync(string toUsername, MessageCode code, MessageParameters messageParameters = null)
		{
			ClientNotificationManager.<SendPrivateMessageAsync>d__19 <SendPrivateMessageAsync>d__;
			<SendPrivateMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendPrivateMessageAsync>d__.<>4__this = this;
			<SendPrivateMessageAsync>d__.toUsername = toUsername;
			<SendPrivateMessageAsync>d__.code = code;
			<SendPrivateMessageAsync>d__.messageParameters = messageParameters;
			<SendPrivateMessageAsync>d__.<>1__state = -1;
			<SendPrivateMessageAsync>d__.<>t__builder.Start<ClientNotificationManager.<SendPrivateMessageAsync>d__19>(ref <SendPrivateMessageAsync>d__);
			return <SendPrivateMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023C8 File Offset: 0x000005C8
		public void SendMessage(InstantMessage msg)
		{
			if (!ObjectFactory.Resolve<ClientCache>().IsClockWorkServerEnable)
			{
				return;
			}
			try
			{
				MessagingManager.CurrentInstance.MessagingClient.SendMessage(msg);
			}
			catch (ConnectionFailedException)
			{
				MessagingManager.CurrentInstance.MessagingClient.SendMessage(msg);
			}
			catch
			{
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002428 File Offset: 0x00000628
		public Task SendMessageAsync(InstantMessage msg)
		{
			ClientNotificationManager.<SendMessageAsync>d__21 <SendMessageAsync>d__;
			<SendMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendMessageAsync>d__.msg = msg;
			<SendMessageAsync>d__.<>1__state = -1;
			<SendMessageAsync>d__.<>t__builder.Start<ClientNotificationManager.<SendMessageAsync>d__21>(ref <SendMessageAsync>d__);
			return <SendMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000246C File Offset: 0x0000066C
		internal Task SendMessageToEveryoneAsync(eMessageTypeCode msgCode, string messageText = "")
		{
			ClientNotificationManager.<SendMessageToEveryoneAsync>d__22 <SendMessageToEveryoneAsync>d__;
			<SendMessageToEveryoneAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendMessageToEveryoneAsync>d__.<>4__this = this;
			<SendMessageToEveryoneAsync>d__.msgCode = msgCode;
			<SendMessageToEveryoneAsync>d__.messageText = messageText;
			<SendMessageToEveryoneAsync>d__.<>1__state = -1;
			<SendMessageToEveryoneAsync>d__.<>t__builder.Start<ClientNotificationManager.<SendMessageToEveryoneAsync>d__22>(ref <SendMessageToEveryoneAsync>d__);
			return <SendMessageToEveryoneAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000001 RID: 1
		public static ClientNotificationManager _currentInstance;

		// Token: 0x02000029 RID: 41
		internal class PrivateMessage
		{
			// Token: 0x17000038 RID: 56
			// (get) Token: 0x0600012D RID: 301 RVA: 0x00004521 File Offset: 0x00002721
			// (set) Token: 0x0600012E RID: 302 RVA: 0x00004529 File Offset: 0x00002729
			public MessageParameters MessageParameters { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600012F RID: 303 RVA: 0x00004532 File Offset: 0x00002732
			// (set) Token: 0x06000130 RID: 304 RVA: 0x0000453A File Offset: 0x0000273A
			public string ToUsername { get; set; }

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x06000131 RID: 305 RVA: 0x00004543 File Offset: 0x00002743
			// (set) Token: 0x06000132 RID: 306 RVA: 0x0000454B File Offset: 0x0000274B
			public MessageCode Code { get; set; }
		}
	}
}
