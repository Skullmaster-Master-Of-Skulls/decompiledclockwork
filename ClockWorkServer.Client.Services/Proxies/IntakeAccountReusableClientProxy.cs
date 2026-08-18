using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000A7 RID: 167
	public class IntakeAccountReusableClientProxy : WCFTokenBasedReusableClientProxy<IIntakeAccount>, IIntakeAccount, IService
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00011E5E File Offset: 0x0001005E
		public IntakeAccountReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00011E69 File Offset: 0x00010069
		public IntakeAccountReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00011E78 File Offset: 0x00010078
		public CreateNewIntakeAccountResp CreateNewIntakeAccount(CreateNewIntakeAccountReq Request)
		{
			return this.WrapServiceMethod<CreateNewIntakeAccountResp>(() => this.Proxy.CreateNewIntakeAccount(Request));
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00011EB0 File Offset: 0x000100B0
		public LoadPendingIntakeEntriesResp LoadPendingIntakeEntries(LoadPendingIntakeEntriesReq Request)
		{
			return this.WrapServiceMethod<LoadPendingIntakeEntriesResp>(() => this.Proxy.LoadPendingIntakeEntries(Request));
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00011EE8 File Offset: 0x000100E8
		public LoadPendingIntakeEntryQueueItemsResp LoadPendingIntakeEntryQueueItems(LoadPendingIntakeEntryQueueItemsReq Request)
		{
			return this.WrapServiceMethod<LoadPendingIntakeEntryQueueItemsResp>(() => this.Proxy.LoadPendingIntakeEntryQueueItems(Request));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00011F20 File Offset: 0x00010120
		public UpdateActiveIntakeStatusAndNoteResp UpdateActiveIntakeStatusAndNote(UpdateActiveIntakeStatusAndNoteReq Request)
		{
			return this.WrapServiceMethod<UpdateActiveIntakeStatusAndNoteResp>(() => this.Proxy.UpdateActiveIntakeStatusAndNote(Request));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00011F58 File Offset: 0x00010158
		public UpdateActiveIntakeStatusResp UpdateActiveIntakeStatus(UpdateActiveIntakeStatusReq Request)
		{
			return this.WrapServiceMethod<UpdateActiveIntakeStatusResp>(() => this.Proxy.UpdateActiveIntakeStatus(Request));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00011F90 File Offset: 0x00010190
		public UpdateActiveIntakeNoteResp UpdateActiveIntakeNote(UpdateActiveIntakeNoteReq Request)
		{
			return this.WrapServiceMethod<UpdateActiveIntakeNoteResp>(() => this.Proxy.UpdateActiveIntakeNote(Request));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00011FC8 File Offset: 0x000101C8
		public RemoveIntakeResp RemoveIntake(RemoveIntakeReq Request)
		{
			return this.WrapServiceMethod<RemoveIntakeResp>(() => this.Proxy.RemoveIntake(Request));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00012000 File Offset: 0x00010200
		public LoadLookupStatusesResp LoadLookupStatuses(LoadLookupStatusesReq Request)
		{
			return this.WrapServiceMethod<LoadLookupStatusesResp>(() => this.Proxy.LoadLookupStatuses(Request));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00012038 File Offset: 0x00010238
		public CreateRealStudentAccountFromIntakeAndRemoveIntakeResp CreateRealStudentAccountFromIntakeAndRemoveIntake(CreateRealStudentAccountFromIntakeAndRemoveIntakeReq Request)
		{
			return this.WrapServiceMethod<CreateRealStudentAccountFromIntakeAndRemoveIntakeResp>(() => this.Proxy.CreateRealStudentAccountFromIntakeAndRemoveIntake(Request));
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00012070 File Offset: 0x00010270
		public LoadIntakeFormDataResp LoadIntakeFormData(LoadIntakeFormDataReq Request)
		{
			return this.WrapServiceMethod<LoadIntakeFormDataResp>(() => this.Proxy.LoadIntakeFormData(Request));
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000120A8 File Offset: 0x000102A8
		public GetIntakeStatusesResp GetIntakeStatuses(GetIntakeStatusesReq Request)
		{
			return this.WrapServiceMethod<GetIntakeStatusesResp>(() => this.Proxy.GetIntakeStatuses(Request));
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000120E0 File Offset: 0x000102E0
		public RemoveIntakesResp RemoveIntakes(RemoveIntakesReq Request)
		{
			return this.WrapServiceMethod<RemoveIntakesResp>(() => this.Proxy.RemoveIntakes(Request));
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00012118 File Offset: 0x00010318
		public SyncIntakeDataResp SyncIntakeData(SyncIntakeDataReq Request)
		{
			return this.WrapServiceMethod<SyncIntakeDataResp>(() => this.Proxy.SyncIntakeData(Request));
		}
	}
}
