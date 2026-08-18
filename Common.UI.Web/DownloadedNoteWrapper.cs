using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.Common.UI.Web.NotetakingStudents.Entity
{
	// Token: 0x02000002 RID: 2
	public class DownloadedNoteWrapper : WrapperBase<DownloadedLectureNoteDTO>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public DownloadedNoteWrapper(DownloadedLectureNoteDTO item) : base(item)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public DateTime? LectureDate
		{
			get
			{
				DownloadedLectureNoteDTO item = base.Item;
				if (item == null)
				{
					return null;
				}
				LectureNoteDescriptionDTO lectureNoteDescription = item.LectureNoteDescription;
				if (lectureNoteDescription == null)
				{
					return null;
				}
				return new DateTime?(lectureNoteDescription.LectureDate);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000209A File Offset: 0x0000029A
		public string Description
		{
			get
			{
				DownloadedLectureNoteDTO item = base.Item;
				string result;
				if (((item != null) ? item.LectureNoteDescription : null) != null)
				{
					if ((result = base.Item.LectureNoteDescription.Comment) == null)
					{
						return "";
					}
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CF File Offset: 0x000002CF
		public string DocName
		{
			get
			{
				DownloadedLectureNoteDTO item = base.Item;
				string result;
				if (((item != null) ? item.LectureNoteDocument : null) != null)
				{
					if ((result = base.Item.LectureNoteDocument.FileName) == null)
					{
						return "";
					}
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002104 File Offset: 0x00000304
		public int FileId
		{
			get
			{
				DownloadedLectureNoteDTO item = base.Item;
				if (item == null)
				{
					return 0;
				}
				return item.NotetakerDocumentId;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002118 File Offset: 0x00000318
		public DateTime? DateDownloaded
		{
			get
			{
				DownloadedLectureNoteDTO item = base.Item;
				if (item == null)
				{
					return null;
				}
				return new DateTime?(item.LastDateDownloaded);
			}
		}
	}
}
