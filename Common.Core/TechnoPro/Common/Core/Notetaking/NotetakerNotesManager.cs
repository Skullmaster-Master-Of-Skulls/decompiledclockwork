using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Notetaking;
using TechnoPro.Common.DAO.Notetaking;
using TechnoPro.Common.ICore.Notetaking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.Core.Notetaking
{
	// Token: 0x020000AE RID: 174
	public class NotetakerNotesManager : INotetakerNotesManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0000672B File Offset: 0x0000492B
		public NotetakerNotesManager()
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00025CB0 File Offset: 0x00023EB0
		public NotetakerNotesManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00025CC2 File Offset: 0x00023EC2
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00025CCA File Offset: 0x00023ECA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000675 RID: 1653 RVA: 0x00025CD4 File Offset: 0x00023ED4
		public IList<LectureNoteDescription> LoadLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate, bool onlyShowFilesMarkedForDeletion)
		{
			INotetakerNotesDAO notetakerNotesDAO = new NotetakerNotesDAO(this.OpContext);
			return onlyShowFilesMarkedForDeletion ? notetakerNotesDAO.LoadMarkedForDeletionLectureNoteDescriptions(courseStartDate, courseEndDate) : notetakerNotesDAO.LoadLectureNoteDescriptions(courseStartDate, courseEndDate);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00025D08 File Offset: 0x00023F08
		public int DeleteAllNotesMarkedForDeletionTodayOrEarlier()
		{
			INotetakerNotesDAO notetakerNotesDAO = new NotetakerNotesDAO(this.OpContext);
			return notetakerNotesDAO.DeleteAllNotesMarkedForDeletionTodayOrEarlier();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00025D2C File Offset: 0x00023F2C
		public int DeleteAllNotesMarkedForDeletion()
		{
			INotetakerNotesDAO notetakerNotesDAO = new NotetakerNotesDAO(this.OpContext);
			return notetakerNotesDAO.DeleteAllNotesMarkedForDeletion();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00025D50 File Offset: 0x00023F50
		public void RemoveAllNotesDeletionMarks()
		{
			INotetakerNotesDAO notetakerNotesDAO = new NotetakerNotesDAO(this.OpContext);
			notetakerNotesDAO.RemoveAllNotesDeletionMarks();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00025D74 File Offset: 0x00023F74
		public void RemoveNotesDeletionMarks(params int[] notetakerDocumentIds)
		{
			INotetakerNotesDAO notetakerNotesDAO = new NotetakerNotesDAO(this.OpContext);
			notetakerNotesDAO.RemoveNotesDeletionMarks(notetakerDocumentIds);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00025D98 File Offset: 0x00023F98
		public void AddNotesDeletionMarks(DateTime newDateOfDeletion, params int[] notetakerDocumentIds)
		{
			INotetakerNotesDAO notetakerNotesDAO = new NotetakerNotesDAO(this.OpContext);
			notetakerNotesDAO.AddNotesDeletionMarks(newDateOfDeletion, notetakerDocumentIds);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00025DBC File Offset: 0x00023FBC
		public LectureNote DownloadLectureNote(int notetakerDocumentId)
		{
			INotetakingManager notetakingManager = new NotetakingManager(this.OpContext);
			return notetakingManager.LoadLectureNoteById(notetakerDocumentId);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00025DE4 File Offset: 0x00023FE4
		public IDictionary<DateTime, long> GetTotalFileSizeByMonth()
		{
			INotetakerNotesDAO notetakerNotesDAO = new NotetakerNotesDAO(this.OpContext);
			return notetakerNotesDAO.GetTotalFileSizeByMonth();
		}
	}
}
