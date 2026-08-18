using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x02000088 RID: 136
	public class MediaJobRestClientManager : BearerTokenRestProxy<IMediaJobClientManager>, IMediaJobClientManager, IWebService
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x0000F733 File Offset: 0x0000D933
		public MediaJobRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000F73D File Offset: 0x0000D93D
		public MediaJobRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000F748 File Offset: 0x0000D948
		public IList<MediaJobDTO> SplitJobIntoChapters(MediaJobDTO job, params string[] chapterTitles)
		{
			if (chapterTitles == null || chapterTitles.Length == 0)
			{
				return null;
			}
			List<MediaJobDTO> list = new List<MediaJobDTO>();
			job.JobTitle = chapterTitles[0];
			list.Add(job);
			this.UpdateMediaJob(job);
			IStudentMediaRequestClientManager studentMediaRequestClientManager = ObjectFactory.Resolve<IStudentMediaRequestClientManager>();
			IList<MediaContentRequestedInfoDTO> list2 = studentMediaRequestClientManager.LoadAllMediaRequestInfoByJobId(job.MediaJobId);
			for (int i = 1; i < chapterTitles.Length; i++)
			{
				MediaJobDTO mediaJobDTO = job.Clone();
				mediaJobDTO.JobTitle = chapterTitles[i];
				mediaJobDTO.JobCurrentStatusNameAboutInHouse = string.Empty;
				mediaJobDTO.JobCurrentStatusNameAboutPublisher = string.Empty;
				mediaJobDTO.JobCurrentStatusNameAboutVendor = string.Empty;
				mediaJobDTO.JobCurrentStatusNameGeneral = "Created";
				mediaJobDTO.MediaJobId = this.CreateMediaJob(mediaJobDTO);
				foreach (MediaContentRequestedInfoDTO mediaContentRequestedInfoDTO in list2)
				{
					MediaContentRequestedInfoDTO mediaContentRequestedInfoDTO2 = mediaContentRequestedInfoDTO.Clone();
					mediaContentRequestedInfoDTO2.MediaJobId = mediaJobDTO.MediaJobId;
					mediaContentRequestedInfoDTO2.CreatedDatetime = DateTime.Now;
					mediaContentRequestedInfoDTO2.MediaContentRequestedInfoID = studentMediaRequestClientManager.AddStudentContentMediaRequestInfo(mediaContentRequestedInfoDTO2);
				}
				list.Add(mediaJobDTO);
			}
			return list;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000F864 File Offset: 0x0000DA64
		public int AddMediaJobNote(int mediaJobId, MediaJobRunningNoteDTO note)
		{
			AddMediaJobNoteReq addMediaJobNoteReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddMediaJobNoteReq>();
			addMediaJobNoteReq.MediaJobId = mediaJobId;
			addMediaJobNoteReq.Note = note;
			return base.Post<AddMediaJobNoteReq, int>(addMediaJobNoteReq, "mediajob/note");
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000F896 File Offset: 0x0000DA96
		public void UpdateMediaJobNote(MediaJobRunningNoteDTO note)
		{
			base.Put<MediaJobRunningNoteDTO>(note, "mediajob/note");
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public IList<MediaJobRunningNoteDTO> GetRunningNotesByMediaJob(int mediaJobId)
		{
			return base.GetMany<MediaJobRunningNoteDTO>(string.Format("mediajob/note/{0}", mediaJobId), true);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000F8BD File Offset: 0x0000DABD
		public MediaJobDTO GetActiveMediaJobById(int mediaJobId)
		{
			return base.Get<MediaJobDTO>(string.Format("mediajob/jobid/{0}", mediaJobId), true);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		public IList<MediaJobDTO> GetActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentId = 0)
		{
			if (studentId <= 0)
			{
				return base.GetMany<MediaJobDTO>(string.Format("mediajob/contentid/{0}/format/{1}", mediaContentId, mediaContentFormat), true);
			}
			return base.GetMany<MediaJobDTO>(string.Format("mediajob/contentid/{0}/format/{1}?studentid={2}", mediaContentId, mediaContentFormat, studentId), true);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000F92A File Offset: 0x0000DB2A
		public IList<MediaJobDTO> GetActiveMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaJobDTO>(string.Format("mediajob/assignedtostaffid/{0}", assignedStaffId), true);
			}
			return base.GetMany<MediaJobDTO>(string.Format("mediajob/assignedtostaffid/{0}?campusid={1}", assignedStaffId, campusId), true);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000F965 File Offset: 0x0000DB65
		public IList<MediaJobDTO> GetActiveMediaJobsByExpiredInLessThan(TimeSpan dueDateIn)
		{
			return base.GetMany<MediaJobDTO>(string.Format("mediajob/expiringinless/{0}", (int)dueDateIn.TotalDays), true);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000F985 File Offset: 0x0000DB85
		public IList<MediaJobDTO> GetActiveExpiredMediaJobs()
		{
			return base.GetMany<MediaJobDTO>("mediajob/activeexpiredjobs", true);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000F993 File Offset: 0x0000DB93
		public IList<MediaJobDTO> GetActiveJobs(int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaJobDTO>("mediajob/activejobs", true);
			}
			return base.GetMany<MediaJobDTO>(string.Format("mediajob/activejobs?campusid={0}", campusId), true);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000F9BD File Offset: 0x0000DBBD
		public IList<MediaJobDTO> GetActiveJobsByStudent(int studentPersonId, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaJobDTO>(string.Format("mediajob/activejobsbystudent/{0}", studentPersonId), true);
			}
			return base.GetMany<MediaJobDTO>(string.Format("mediajob/activejobsbystudent/{0}?campusid={1}", studentPersonId, campusId), true);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public CompletedMediaJobDTO GetCompletedMediaJobById(int mediaJobId)
		{
			return base.Get<CompletedMediaJobDTO>(string.Format("mediajob/completedjobbyid/{0}", mediaJobId), true);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public IList<CompletedMediaJobDTO> GetCompletedMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat)
		{
			return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsbycontentandformat/contentid/{0}/format/{1}", mediaContentId, mediaContentFormat), true);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000FA30 File Offset: 0x0000DC30
		public IList<CompletedMediaJobDTO> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsassignedto/staffpid/{0}", assignedStaffId), true);
			}
			return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsassignedto/staffpid/{0}?campusid={1}", assignedStaffId, campusId), true);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public IList<CompletedMediaJobDTO> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobs/range/{0}/{1}", startDate, endDate), true);
			}
			return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobs/range/{0}/{1}?campusid={2}", startDate, endDate, campusId), true);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000FABE File Offset: 0x0000DCBE
		public IList<CompletedMediaJobDTO> GetCompletedJobs(int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CompletedMediaJobDTO>("mediajob/completedjobs", true);
			}
			return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobs?campusid={0}", campusId), true);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public IList<CompletedMediaJobDTO> GetCompletedJobsByStudent(int studentPersonId, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsbystudent/studentpid/{0}", studentPersonId), true);
			}
			return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsbystudent/studentpid/{0}?campusid={1}", studentPersonId, campusId), true);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000FB24 File Offset: 0x0000DD24
		public IList<CompletedMediaJobDTO> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsbystudent/studentpid/{0}/range/{1}/{2}", studentPersonId, startDate, endDate), true);
			}
			return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsbystudent/studentpid/{0}/range/{1}/{2}?campusid={3}", new object[]
			{
				studentPersonId,
				startDate,
				endDate,
				campusId
			}), true);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public IList<CompletedMediaJobDTO> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsassignedto/staffpid/{0}/range/{1}/{2}", assignedStaffId, startDate, endDate), true);
			}
			return base.GetMany<CompletedMediaJobDTO>(string.Format("mediajob/completedjobsassignedto/staffpid/{0}/range/{1}/{2}?campusid={3}", new object[]
			{
				assignedStaffId,
				startDate,
				endDate,
				campusId
			}), true);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000FC0A File Offset: 0x0000DE0A
		public CancelledMediaJobDTO GetCancelledMediaJobById(int mediaJobId)
		{
			return base.Get<CancelledMediaJobDTO>(string.Format("mediajob/cancelledjobbyid/{0}", mediaJobId), true);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000FC24 File Offset: 0x0000DE24
		public IList<CancelledMediaJobDTO> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CancelledMediaJobDTO>(string.Format("cancelledjobs/range/{0}/{1}", startDate, endDate), true);
			}
			return base.GetMany<CancelledMediaJobDTO>(string.Format("mediajob/cancelledjobs/range/{0}/{1}?campusid={2}", startDate, endDate, campusId), true);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000FC76 File Offset: 0x0000DE76
		public IList<CancelledMediaJobDTO> GetCancelledJobs(int campusId = 0)
		{
			return base.GetMany<CancelledMediaJobDTO>(string.Format("mediajob/cancelledjobs?campusid={0}", campusId), true);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000FC90 File Offset: 0x0000DE90
		public IList<CancelledMediaJobDTO> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CancelledMediaJobDTO>(string.Format("mediajob/cancelledjobsbystudent/studentpid/{0}/range/{1}/{2}", studentPersonId, startDate, endDate), true);
			}
			return base.GetMany<CancelledMediaJobDTO>(string.Format("mediajob/cancelledjobsbystudent/studentpid/{0}/range/{1}/{2}?campusid={3}", new object[]
			{
				studentPersonId,
				startDate,
				endDate,
				campusId
			}), true);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000FD04 File Offset: 0x0000DF04
		public IList<CancelledMediaJobDTO> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<CancelledMediaJobDTO>(string.Format("mediajob/cancelledjobsassignedto/staffpid/{0}/range/{1}/{2}", assignedStaffId, startDate, endDate), true);
			}
			return base.GetMany<CancelledMediaJobDTO>(string.Format("mediajob/cancelledjobsassignedto/staffpid/{0}/range/{1}/{2}?campusid={3}", new object[]
			{
				assignedStaffId,
				startDate,
				endDate,
				campusId
			}), true);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000FD76 File Offset: 0x0000DF76
		public int CreateMediaJob(MediaJobDTO mediaJob)
		{
			return base.Post<MediaJobDTO, int>(mediaJob, "mediajob");
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public void UpdateMediaJob(MediaJobDTO mediaJob)
		{
			base.Put<MediaJobDTO>(mediaJob, "mediajob");
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000FD94 File Offset: 0x0000DF94
		public IList<MediaContentRequestedInfoDTO> CancelMediaJob(MediaJobDTO mediaJob, string changeNotes)
		{
			CancelMediaJobReq cancelMediaJobReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CancelMediaJobReq>();
			cancelMediaJobReq.MediaJob = mediaJob;
			cancelMediaJobReq.CancelNotes = changeNotes;
			return base.Post<CancelMediaJobReq, IList<MediaContentRequestedInfoDTO>>(cancelMediaJobReq, "mediajob/canceljob");
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		public IList<MediaContentRequestedInfoDTO> MarkMediaJobAsCompleted(MediaJobDTO mediaJob, string changeNotes, DateTime availableStartTime, DateTime availableEndTime)
		{
			MarkMediaJobAsCompletedReq markMediaJobAsCompletedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MarkMediaJobAsCompletedReq>();
			markMediaJobAsCompletedReq.MediaJob = mediaJob;
			markMediaJobAsCompletedReq.CompletedNotes = changeNotes;
			markMediaJobAsCompletedReq.AvailableStartTime = availableStartTime;
			markMediaJobAsCompletedReq.AvailableEndTime = availableEndTime;
			return base.Post<MarkMediaJobAsCompletedReq, IList<MediaContentRequestedInfoDTO>>(markMediaJobAsCompletedReq, "mediajob/markjobascompleted");
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000FE0C File Offset: 0x0000E00C
		public void ChangeMediaJobStatus(int mediaJobId, string changeNotes, ref string generalStatusnName, ref string publisherStatusName, ref string vendorStatusName, ref string inHouseStatusName)
		{
			ChangeMediaJobStatusReq changeMediaJobStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobStatusReq>();
			changeMediaJobStatusReq.MediaJobId = mediaJobId;
			changeMediaJobStatusReq.StatusChangedNotes = changeNotes;
			changeMediaJobStatusReq.GeneralStatusName = generalStatusnName;
			changeMediaJobStatusReq.PublisherStatusName = publisherStatusName;
			changeMediaJobStatusReq.VendorStatusName = vendorStatusName;
			changeMediaJobStatusReq.InHouseStatusName = inHouseStatusName;
			ChangeMediaJobStatusResp changeMediaJobStatusResp = base.Post<ChangeMediaJobStatusReq, ChangeMediaJobStatusResp>(changeMediaJobStatusReq, "changejobstatus");
			generalStatusnName = changeMediaJobStatusResp.GeneralStatusName;
			publisherStatusName = changeMediaJobStatusResp.PublisherStatusName;
			vendorStatusName = changeMediaJobStatusResp.VendorStatusName;
			inHouseStatusName = changeMediaJobStatusResp.InHouseStatusName;
		}
	}
}
