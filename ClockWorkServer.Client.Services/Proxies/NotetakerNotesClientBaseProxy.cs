using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F8 RID: 248
	internal class NotetakerNotesClientBaseProxy : ClientBase<INotetakerNotes>, INotetakerNotes, IService
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x000186E4 File Offset: 0x000168E4
		public NotetakerNotesClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000186EF File Offset: 0x000168EF
		public NotetakerNotesClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000186FC File Offset: 0x000168FC
		public LoadLectureNoteDescriptionsResp LoadLectureNoteDescriptions(LoadLectureNoteDescriptionsReq Request)
		{
			return base.Channel.LoadLectureNoteDescriptions(Request);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001871C File Offset: 0x0001691C
		public DeleteAllNotesMarkedForDeletionTodayOrEarlierResp DeleteAllNotesMarkedForDeletionTodayOrEarlier(DeleteAllNotesMarkedForDeletionTodayOrEarlierReq Request)
		{
			return base.Channel.DeleteAllNotesMarkedForDeletionTodayOrEarlier(Request);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0001873C File Offset: 0x0001693C
		public DeleteAllNotesMarkedForDeletionResp DeleteAllNotesMarkedForDeletion(DeleteAllNotesMarkedForDeletionReq Request)
		{
			return base.Channel.DeleteAllNotesMarkedForDeletion(Request);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001875C File Offset: 0x0001695C
		public RemoveAllNotesDeletionMarksResp RemoveAllNotesDeletionMarks(RemoveAllNotesDeletionMarksReq Request)
		{
			return base.Channel.RemoveAllNotesDeletionMarks(Request);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001877C File Offset: 0x0001697C
		public RemoveNotesDeletionMarksResp RemoveNotesDeletionMarks(RemoveNotesDeletionMarksReq Request)
		{
			return base.Channel.RemoveNotesDeletionMarks(Request);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001879C File Offset: 0x0001699C
		public AddNotesDeletionMarksResp AddNotesDeletionMarks(AddNotesDeletionMarksReq Request)
		{
			return base.Channel.AddNotesDeletionMarks(Request);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000187BC File Offset: 0x000169BC
		public DownloadLectureNoteResp DownloadLectureNote(DownloadLectureNoteReq Request)
		{
			return base.Channel.DownloadLectureNote(Request);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000187DC File Offset: 0x000169DC
		public GetTotalFileSizeByMonthResp GetTotalFileSizeByMonth(GetTotalFileSizeByMonthReq Request)
		{
			return base.Channel.GetTotalFileSizeByMonth(Request);
		}
	}
}
