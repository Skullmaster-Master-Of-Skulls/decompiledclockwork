using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Notetaking;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Notetaking
{
	// Token: 0x02000036 RID: 54
	public class NotetakerNotesClientManager : INotetakerNotesClientManager
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00009418 File Offset: 0x00007618
		public IList<LectureNoteDescriptionDTO> LoadLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate, bool onlyShowFilesMarkedForDeletion)
		{
			LoadLectureNoteDescriptionsReq loadLectureNoteDescriptionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLectureNoteDescriptionsReq>();
			loadLectureNoteDescriptionsReq.CourseStartDate = courseStartDate;
			loadLectureNoteDescriptionsReq.CourseEndDate = courseEndDate;
			loadLectureNoteDescriptionsReq.OnlyReturnNotesMarkedForDeletion = onlyShowFilesMarkedForDeletion;
			return ClientServiceFactory.GetClientInstance<INotetakerNotes>().LoadLectureNoteDescriptions(loadLectureNoteDescriptionsReq).LectureNoteDescriptions;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009460 File Offset: 0x00007660
		public int DeleteAllNotesMarkedForDeletionTodayOrEarlier()
		{
			DeleteAllNotesMarkedForDeletionTodayOrEarlierReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAllNotesMarkedForDeletionTodayOrEarlierReq>();
			return ClientServiceFactory.GetClientInstance<INotetakerNotes>().DeleteAllNotesMarkedForDeletionTodayOrEarlier(request).NumNotesDeleted;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009490 File Offset: 0x00007690
		public int DeleteAllNotesMarkedForDeletion()
		{
			DeleteAllNotesMarkedForDeletionReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAllNotesMarkedForDeletionReq>();
			return ClientServiceFactory.GetClientInstance<INotetakerNotes>().DeleteAllNotesMarkedForDeletion(request).NumNotesDeleted;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000094C0 File Offset: 0x000076C0
		public void RemoveAllNotesDeletionMarks()
		{
			RemoveAllNotesDeletionMarksReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveAllNotesDeletionMarksReq>();
			ClientServiceFactory.GetClientInstance<INotetakerNotes>().RemoveAllNotesDeletionMarks(request);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000094E8 File Offset: 0x000076E8
		public void RemoveNotesDeletionMarks(params int[] notetakerDocumentIds)
		{
			RemoveNotesDeletionMarksReq removeNotesDeletionMarksReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveNotesDeletionMarksReq>();
			removeNotesDeletionMarksReq.NotetakerDocumentIds = notetakerDocumentIds;
			ClientServiceFactory.GetClientInstance<INotetakerNotes>().RemoveNotesDeletionMarks(removeNotesDeletionMarksReq);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009518 File Offset: 0x00007718
		public void AddNotesDeletionMarks(DateTime newDateOfDeletion, params int[] notetakerDocumentIds)
		{
			AddNotesDeletionMarksReq addNotesDeletionMarksReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddNotesDeletionMarksReq>();
			addNotesDeletionMarksReq.DateOfDeletion = newDateOfDeletion;
			addNotesDeletionMarksReq.NotetakerDocumentIds = notetakerDocumentIds;
			ClientServiceFactory.GetClientInstance<INotetakerNotes>().AddNotesDeletionMarks(addNotesDeletionMarksReq);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009550 File Offset: 0x00007750
		public LectureNoteDTO DownloadLectureNote(int notetakerDocumentId)
		{
			DownloadLectureNoteReq downloadLectureNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DownloadLectureNoteReq>();
			downloadLectureNoteReq.NotetakerDocumentId = notetakerDocumentId;
			return ClientServiceFactory.GetClientInstance<INotetakerNotes>().DownloadLectureNote(downloadLectureNoteReq).LectureNote;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009588 File Offset: 0x00007788
		public IDictionary<DateTime, long> GetTotalFileSizeByMonth()
		{
			GetTotalFileSizeByMonthReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetTotalFileSizeByMonthReq>();
			return ClientServiceFactory.GetClientInstance<INotetakerNotes>().GetTotalFileSizeByMonth(request).TotalFileSizesByMonths;
		}
	}
}
