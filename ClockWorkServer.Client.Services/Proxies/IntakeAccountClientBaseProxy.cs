using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000A8 RID: 168
	internal class IntakeAccountClientBaseProxy : ClientBase<IIntakeAccount>, IIntakeAccount, IService
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00012150 File Offset: 0x00010350
		public IntakeAccountClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001215B File Offset: 0x0001035B
		public IntakeAccountClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00012168 File Offset: 0x00010368
		public CreateNewIntakeAccountResp CreateNewIntakeAccount(CreateNewIntakeAccountReq Request)
		{
			return base.Channel.CreateNewIntakeAccount(Request);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00012188 File Offset: 0x00010388
		public LoadPendingIntakeEntriesResp LoadPendingIntakeEntries(LoadPendingIntakeEntriesReq Request)
		{
			return base.Channel.LoadPendingIntakeEntries(Request);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000121A8 File Offset: 0x000103A8
		public LoadPendingIntakeEntryQueueItemsResp LoadPendingIntakeEntryQueueItems(LoadPendingIntakeEntryQueueItemsReq Request)
		{
			return base.Channel.LoadPendingIntakeEntryQueueItems(Request);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000121C8 File Offset: 0x000103C8
		public UpdateActiveIntakeStatusAndNoteResp UpdateActiveIntakeStatusAndNote(UpdateActiveIntakeStatusAndNoteReq Request)
		{
			return base.Channel.UpdateActiveIntakeStatusAndNote(Request);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000121E8 File Offset: 0x000103E8
		public UpdateActiveIntakeStatusResp UpdateActiveIntakeStatus(UpdateActiveIntakeStatusReq Request)
		{
			return base.Channel.UpdateActiveIntakeStatus(Request);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00012208 File Offset: 0x00010408
		public UpdateActiveIntakeNoteResp UpdateActiveIntakeNote(UpdateActiveIntakeNoteReq Request)
		{
			return base.Channel.UpdateActiveIntakeNote(Request);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00012228 File Offset: 0x00010428
		public RemoveIntakeResp RemoveIntake(RemoveIntakeReq Request)
		{
			return base.Channel.RemoveIntake(Request);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00012248 File Offset: 0x00010448
		public LoadLookupStatusesResp LoadLookupStatuses(LoadLookupStatusesReq Request)
		{
			return base.Channel.LoadLookupStatuses(Request);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00012268 File Offset: 0x00010468
		public CreateRealStudentAccountFromIntakeAndRemoveIntakeResp CreateRealStudentAccountFromIntakeAndRemoveIntake(CreateRealStudentAccountFromIntakeAndRemoveIntakeReq Request)
		{
			return base.Channel.CreateRealStudentAccountFromIntakeAndRemoveIntake(Request);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00012288 File Offset: 0x00010488
		public LoadIntakeFormDataResp LoadIntakeFormData(LoadIntakeFormDataReq Request)
		{
			return base.Channel.LoadIntakeFormData(Request);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000122A8 File Offset: 0x000104A8
		public GetIntakeStatusesResp GetIntakeStatuses(GetIntakeStatusesReq Request)
		{
			return base.Channel.GetIntakeStatuses(Request);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000122C8 File Offset: 0x000104C8
		public SyncIntakeDataResp SyncIntakeData(SyncIntakeDataReq Request)
		{
			return base.Channel.SyncIntakeData(Request);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000122E8 File Offset: 0x000104E8
		public RemoveIntakesResp RemoveIntakes(RemoveIntakesReq Request)
		{
			return base.Channel.RemoveIntakes(Request);
		}
	}
}
