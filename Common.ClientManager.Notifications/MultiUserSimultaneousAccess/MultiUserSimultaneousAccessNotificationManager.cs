using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notifications.MultiUserSimulatenousAccess;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Notifications.MultiUserSimultaneousAccess
{
	// Token: 0x02000003 RID: 3
	public class MultiUserSimultaneousAccessNotificationManager : IDisposable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000024FF File Offset: 0x000006FF
		public static MultiUserSimultaneousAccessNotificationManager CurrentInstance
		{
			get
			{
				MultiUserSimultaneousAccessNotificationManager result;
				if ((result = MultiUserSimultaneousAccessNotificationManager._currentInstance) == null)
				{
					result = (MultiUserSimultaneousAccessNotificationManager._currentInstance = new MultiUserSimultaneousAccessNotificationManager());
				}
				return result;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000015 RID: 21 RVA: 0x00002518 File Offset: 0x00000718
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x00002550 File Offset: 0x00000750
		public event EventHandler<MultiUserSimultaneousAccessArgs> OnQueryAreYouEditingThisData;

		// Token: 0x06000017 RID: 23 RVA: 0x00002588 File Offset: 0x00000788
		private bool FireOnQueryAreYouEditingThisData(MultiAccessInfo multiAccessInfo)
		{
			MultiUserSimultaneousAccessArgs multiUserSimultaneousAccessArgs = new MultiUserSimultaneousAccessArgs
			{
				MultiAccessInfo = multiAccessInfo
			};
			EventHandler<MultiUserSimultaneousAccessArgs> onQueryAreYouEditingThisData = this.OnQueryAreYouEditingThisData;
			if (onQueryAreYouEditingThisData != null)
			{
				onQueryAreYouEditingThisData(this, multiUserSimultaneousAccessArgs);
			}
			return multiUserSimultaneousAccessArgs.AlreadyEditing;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000018 RID: 24 RVA: 0x000025BC File Offset: 0x000007BC
		// (remove) Token: 0x06000019 RID: 25 RVA: 0x000025F4 File Offset: 0x000007F4
		public event EventHandler<MultiUserSimultaneousAccessArgs> OnAnotherUserIsAlreadyEditingMessageReceived;

		// Token: 0x0600001A RID: 26 RVA: 0x0000262C File Offset: 0x0000082C
		private void FireOnAnotherUserIsAlreadyEditingMessageReceived(MultiAccessInfo multiAccessInfo)
		{
			EventHandler<MultiUserSimultaneousAccessArgs> onAnotherUserIsAlreadyEditingMessageReceived = this.OnAnotherUserIsAlreadyEditingMessageReceived;
			if (onAnotherUserIsAlreadyEditingMessageReceived != null)
			{
				onAnotherUserIsAlreadyEditingMessageReceived(this, new MultiUserSimultaneousAccessArgs
				{
					MultiAccessInfo = multiAccessInfo
				});
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002658 File Offset: 0x00000858
		public Task MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync(InstantMessage msg)
		{
			MultiUserSimultaneousAccessNotificationManager.<MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__11 <MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__;
			<MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__.<>4__this = this;
			<MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__.msg = msg;
			<MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__.<>1__state = -1;
			<MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__.<>t__builder.Start<MultiUserSimultaneousAccessNotificationManager.<MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__11>(ref <MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__);
			return <MessageReceived_UserIsBroadcastingThatHeJustEnteredDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026A4 File Offset: 0x000008A4
		public void MessageReceived_UserIsRespondingThatHeIsAlreadyEditingThisData(InstantMessage msg)
		{
			if (ClientNotificationManager.CurrentInstance.IsMessageFromMyself(msg))
			{
				return;
			}
			MultiAccessInfo multiAccessInfoFromString = (msg.Parameters["info"] ?? "").GetMultiAccessInfoFromString();
			this.FireOnAnotherUserIsAlreadyEditingMessageReceived(multiAccessInfoFromString);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026E8 File Offset: 0x000008E8
		private MultiAccessInfo GetNewMultiAccessInfo()
		{
			PersonBaseDTO personBaseDTO = (PersonBaseDTO)ObjectFactory.Resolve<ICacheStorageManager>()["cWhoAmI"];
			return new MultiAccessInfo
			{
				WhoIsAccessingPersonId = ((personBaseDTO != null) ? personBaseDTO.PersonId : 0),
				WhoIsAccessingDisplayName = ((personBaseDTO != null) ? personBaseDTO.GetName() : string.Empty)
			};
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002738 File Offset: 0x00000938
		public Task NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync(int appId)
		{
			MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__14 <NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__;
			<NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__.<>4__this = this;
			<NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__.appId = appId;
			<NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__.<>1__state = -1;
			<NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__.<>t__builder.Start<MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__14>(ref <NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__);
			return <NotifyEveryoneThatIJustStartedEditingAnAppointmentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002784 File Offset: 0x00000984
		public Task NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync(int studentPersonId, int courseId)
		{
			MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__15 <NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__;
			<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__.<>4__this = this;
			<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__.studentPersonId = studentPersonId;
			<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__.courseId = courseId;
			<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__.<>1__state = -1;
			<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__.<>t__builder.Start<MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__15>(ref <NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__);
			return <NotifyEveryoneThatIJustStartedEditingAccommodationsForAStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027D8 File Offset: 0x000009D8
		public Task NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync(int studentPersonId, int screenNum)
		{
			MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__16 <NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__;
			<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__.<>4__this = this;
			<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__.studentPersonId = studentPersonId;
			<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__.screenNum = screenNum;
			<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__.<>1__state = -1;
			<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__.<>t__builder.Start<MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__16>(ref <NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__);
			return <NotifyEveryoneThatIJustStartedEditingPerStudentDataForAStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000282C File Offset: 0x00000A2C
		public Task NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync(int studentPersonId, int appointmentId, int screenNum)
		{
			MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__17 <NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__;
			<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.<>4__this = this;
			<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.studentPersonId = studentPersonId;
			<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.appointmentId = appointmentId;
			<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.screenNum = screenNum;
			<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.<>1__state = -1;
			<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.<>t__builder.Start<MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__17>(ref <NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__);
			return <NotifyEveryoneThatIJustStartedEditingPerDateDataForAStudentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002888 File Offset: 0x00000A88
		public Task NotifyEveryoneThatIJustStartedEditingSomethingAsync(MultiAccessInfo info)
		{
			MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__18 <NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__;
			<NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__.info = info;
			<NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__.<>1__state = -1;
			<NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__.<>t__builder.Start<MultiUserSimultaneousAccessNotificationManager.<NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__18>(ref <NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__);
			return <NotifyEveryoneThatIJustStartedEditingSomethingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000028CB File Offset: 0x00000ACB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028DA File Offset: 0x00000ADA
		private void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				CWLogger.Logger.Debug("MultiUserSimultaneousAccessNotificationManager::Dispose::It has been disposed.");
			}
		}

		// Token: 0x04000004 RID: 4
		private static MultiUserSimultaneousAccessNotificationManager _currentInstance;

		// Token: 0x04000007 RID: 7
		protected bool disposed;
	}
}
