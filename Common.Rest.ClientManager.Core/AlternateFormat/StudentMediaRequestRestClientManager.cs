using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200008D RID: 141
	public class StudentMediaRequestRestClientManager : BearerTokenRestProxy<IStudentMediaRequestClientManager>, IStudentMediaRequestClientManager, IWebService
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0001021F File Offset: 0x0000E41F
		public StudentMediaRequestRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00010229 File Offset: 0x0000E429
		public StudentMediaRequestRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00010234 File Offset: 0x0000E434
		public StudentMediaRequestDTO CreateStudentMediaRequest(StudentMediaRequestDTO studentMediaRequest)
		{
			return base.Post<StudentMediaRequestDTO, StudentMediaRequestDTO>(studentMediaRequest, "studentmediarequest");
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00010242 File Offset: 0x0000E442
		public void UpdateStudentMediaRequest(StudentMediaRequestDTO studentMediaRequest)
		{
			base.Put<StudentMediaRequestDTO>(studentMediaRequest, "studentmediarequest");
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00010250 File Offset: 0x0000E450
		public bool IsMediaContentAlreadyRequested(int studentId, MediaContentIdentifierDTO identifier)
		{
			IsMediaContentAlreadyRequestedReq isMediaContentAlreadyRequestedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsMediaContentAlreadyRequestedReq>();
			isMediaContentAlreadyRequestedReq.Identifier = identifier;
			isMediaContentAlreadyRequestedReq.StudentPersonId = studentId;
			return base.Get<bool>(string.Format("studentmediarequest/ismediaalreadyrequested/studentid/{0}/identiifer/{1}", studentId, identifier), true);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00010281 File Offset: 0x0000E481
		public void DeleteStudentContentMediaRequestInfo(int requestedInfoId)
		{
			base.Delete(string.Format("studentmediarequest/mediarequestedbyid/{0}", requestedInfoId));
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00010299 File Offset: 0x0000E499
		public StudentMediaRequestDTO LoadStudentMediaRequestById(int studentMediaRequestId)
		{
			return base.Get<StudentMediaRequestDTO>(string.Format("studentmediarequest/id/{0}", studentMediaRequestId), true);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000102B2 File Offset: 0x0000E4B2
		public void UpdateStudentContentMediaRequestInfo(MediaContentRequestedInfoDTO requestedInfo)
		{
			base.Put<MediaContentRequestedInfoDTO>(requestedInfo, "studentmediarequest");
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000102C0 File Offset: 0x0000E4C0
		public int AddStudentContentMediaRequestInfo(MediaContentRequestedInfoDTO requestedInfo)
		{
			return base.Post<MediaContentRequestedInfoDTO, int>(requestedInfo, "studentmediarequest/mediarequested");
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000102CE File Offset: 0x0000E4CE
		public IList<MediaContentRequestedInfoDTO> LoadStudentMediaRequestByStatus(MediaRequestStatus status)
		{
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/mediarequestedbystatus/{0}", status), true);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000102E7 File Offset: 0x0000E4E7
		public IList<MediaContentRequestedInfoDTO> LoadAllMediaRequestInfoByJobId(int jobId)
		{
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/mediarequestedbyjob/{0}", jobId), true);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00010300 File Offset: 0x0000E500
		public IList<MediaContentRequestedInfoDTO> LoadAllStudentMediaRequestByStudentAndDates(int studentId, DateTime startdate, DateTime enddate)
		{
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/mediarequested/studentid/{0}/range/{1}/{2}", studentId, startdate, enddate), true);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00010328 File Offset: 0x0000E528
		public async Task<IList<MediaContentRequestedInfoDTO>> LoadAllStudentMediaRequestByStudentAndDatesAsync(int studentId, DateTime startdate, DateTime enddate)
		{
			return await this.GetManyAsync<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/mediarequested/studentid/{0}/range/{1}/{2}", studentId, startdate, enddate), true).ConfigureAwait(false);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00010385 File Offset: 0x0000E585
		public IList<MediaContentRequestedInfoDTO> LoadAllApprovedMediaRequest(int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>("studentmediarequest/approvedmediarequested", true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/approvedmediarequested?campusid={0}", campusId), true);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000103AF File Offset: 0x0000E5AF
		public IList<MediaContentRequestedInfoDTO> LoadAllToBeApprovedMediaRequest(int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>("studentmediarequest/tobeapprovedmediarequested", true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/tobeapprovedmediarequested?campusid={0}", campusId), true);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000103D9 File Offset: 0x0000E5D9
		public IList<MediaContentRequestedInfoDTO> LoadAllToBeApprovedMediaRequestByStudent(int studentId, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/tobeapprovedmediarequested/studentid/{0}", studentId), true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/tobeapprovedmediarequested/studentid/{0}?campusid={1}", studentId, campusId), true);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00010414 File Offset: 0x0000E614
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequest(int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>("studentmediarequest/completedmediarequested", true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/completedmediarequested?campusid={0}", campusId), true);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001043E File Offset: 0x0000E63E
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/completedmediarequested/studentid/{0}", studentId), true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/completedmediarequested/studentid/{0}?campusid={1}", studentId, campusId), true);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001047C File Offset: 0x0000E67C
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequest(DateTime startDate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/completedmediarequested/range/{0}/{1}", startDate, endDate), true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/completedmediarequested/range/{0}/{1}?campusid={2}", startDate, endDate, campusId), true);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000104D0 File Offset: 0x0000E6D0
		public IList<MediaContentRequestedInfoDTO> LoadAllCompletedStudentMediaRequestByStudentAndDate(int studentId, DateTime startdate, DateTime endDate, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/completedmediarequested/studentid/{0}/range/{1}/{2}", studentId, startdate, endDate), true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/completedmediarequested/studentid/{0}/range/{1}/{2}?campusid={3}", new object[]
			{
				studentId,
				startdate,
				endDate,
				campusId
			}), true);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00010542 File Offset: 0x0000E742
		public IList<MediaContentRequestedInfoDTO> LoadAllInProgressStudentMediaRequest(int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>("studentmediarequest/inprogressmediarequested", true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/inprogressmediarequested?campusid={0}", campusId), true);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001056C File Offset: 0x0000E76C
		public IList<MediaContentRequestedInfoDTO> LoadAllInProgressStudentMediaRequestByStudent(int studentId, int campusId = 0)
		{
			if (campusId <= 0)
			{
				return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/inprogressmediarequested/studentid/{0}", studentId), true);
			}
			return base.GetMany<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/inprogressmediarequested/studentid/{0}?campusid={1}", studentId, campusId), true);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000105A7 File Offset: 0x0000E7A7
		public ProofOfPurchaseInfoDTO AcceptProofOfPurchaseReceipt(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			return base.Post<ProofOfPurchaseInfoDTO, ProofOfPurchaseInfoDTO>(proofOfPurchaseInfo, "studentmediarequest/acceptproofofpurchase");
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000105B5 File Offset: 0x0000E7B5
		public bool RejectProofOfPurchaseReceipt(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			return base.Post<ProofOfPurchaseInfoDTO, bool>(proofOfPurchaseInfo, "studentmediarequest/rejectproofofpurchase");
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000105C3 File Offset: 0x0000E7C3
		public ProofOfPurchaseInfoDTO DownloadProofOfPurchase(int proofOfPurchaseId)
		{
			return base.Get<ProofOfPurchaseInfoDTO>(string.Format("studentmediarequest/proofofpurchase/{0}", proofOfPurchaseId), true);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000105DC File Offset: 0x0000E7DC
		public async Task<ProofOfPurchaseInfoDTO> DownloadProofOfPurchaseAsync(int proofOfPurchaseId)
		{
			return await this.GetAsync<ProofOfPurchaseInfoDTO>(string.Format("studentmediarequest/proofofpurchase/{0}", proofOfPurchaseId), true).ConfigureAwait(false);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00010629 File Offset: 0x0000E829
		public int UploadProofOfPurchase(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			return base.Post<ProofOfPurchaseInfoDTO, int>(proofOfPurchaseInfo, "studentmediarequest/proofofpurchase");
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00010638 File Offset: 0x0000E838
		public async Task<int> UploadProofOfPurchaseAsync(ProofOfPurchaseInfoDTO proofOfPurchaseInfo)
		{
			return await this.PostAsync<ProofOfPurchaseInfoDTO, int>(proofOfPurchaseInfo, "studentmediarequest/proofofpurchase").ConfigureAwait(false);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00010685 File Offset: 0x0000E885
		public MediaContentRequestedInfoDTO LoadMediaContentRequestedInfoById(int mediaContentRequestedInfoId)
		{
			return base.Get<MediaContentRequestedInfoDTO>(string.Format("studentmediarequest/mediarequested/id/{0}", mediaContentRequestedInfoId), true);
		}
	}
}
