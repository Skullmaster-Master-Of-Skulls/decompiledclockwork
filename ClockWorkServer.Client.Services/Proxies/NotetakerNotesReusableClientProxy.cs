using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F7 RID: 247
	public class NotetakerNotesReusableClientProxy : WCFTokenBasedReusableClientProxy<INotetakerNotes>, INotetakerNotes, IService
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x0001850A File Offset: 0x0001670A
		public NotetakerNotesReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00018515 File Offset: 0x00016715
		public NotetakerNotesReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00018524 File Offset: 0x00016724
		public LoadLectureNoteDescriptionsResp LoadLectureNoteDescriptions(LoadLectureNoteDescriptionsReq Request)
		{
			return this.WrapServiceMethod<LoadLectureNoteDescriptionsResp>(() => this.Proxy.LoadLectureNoteDescriptions(Request));
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001855C File Offset: 0x0001675C
		public DeleteAllNotesMarkedForDeletionTodayOrEarlierResp DeleteAllNotesMarkedForDeletionTodayOrEarlier(DeleteAllNotesMarkedForDeletionTodayOrEarlierReq Request)
		{
			return this.WrapServiceMethod<DeleteAllNotesMarkedForDeletionTodayOrEarlierResp>(() => this.Proxy.DeleteAllNotesMarkedForDeletionTodayOrEarlier(Request));
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00018594 File Offset: 0x00016794
		public DeleteAllNotesMarkedForDeletionResp DeleteAllNotesMarkedForDeletion(DeleteAllNotesMarkedForDeletionReq Request)
		{
			return this.WrapServiceMethod<DeleteAllNotesMarkedForDeletionResp>(() => this.Proxy.DeleteAllNotesMarkedForDeletion(Request));
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000185CC File Offset: 0x000167CC
		public RemoveAllNotesDeletionMarksResp RemoveAllNotesDeletionMarks(RemoveAllNotesDeletionMarksReq Request)
		{
			return this.WrapServiceMethod<RemoveAllNotesDeletionMarksResp>(() => this.Proxy.RemoveAllNotesDeletionMarks(Request));
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00018604 File Offset: 0x00016804
		public RemoveNotesDeletionMarksResp RemoveNotesDeletionMarks(RemoveNotesDeletionMarksReq Request)
		{
			return this.WrapServiceMethod<RemoveNotesDeletionMarksResp>(() => this.Proxy.RemoveNotesDeletionMarks(Request));
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001863C File Offset: 0x0001683C
		public AddNotesDeletionMarksResp AddNotesDeletionMarks(AddNotesDeletionMarksReq Request)
		{
			return this.WrapServiceMethod<AddNotesDeletionMarksResp>(() => this.Proxy.AddNotesDeletionMarks(Request));
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00018674 File Offset: 0x00016874
		public DownloadLectureNoteResp DownloadLectureNote(DownloadLectureNoteReq Request)
		{
			return this.WrapServiceMethod<DownloadLectureNoteResp>(() => this.Proxy.DownloadLectureNote(Request));
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000186AC File Offset: 0x000168AC
		public GetTotalFileSizeByMonthResp GetTotalFileSizeByMonth(GetTotalFileSizeByMonthReq Request)
		{
			return this.WrapServiceMethod<GetTotalFileSizeByMonthResp>(() => this.Proxy.GetTotalFileSizeByMonth(Request));
		}
	}
}
