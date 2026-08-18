using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200008A RID: 138
	public class MediaJobVolunteerRestClientManager : BearerTokenRestProxy<IMediaVolunteerClientManager>, IMediaVolunteerClientManager, IWebService
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x0000FEE3 File Offset: 0x0000E0E3
		public MediaJobVolunteerRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000FEED File Offset: 0x0000E0ED
		public MediaJobVolunteerRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		public IList<AlternateFormatVolunteerDTO> GetAllMediaJobVolunteers()
		{
			return base.GetMany<AlternateFormatVolunteerDTO>("mediajobvolunteer/volunteers", true);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000FF06 File Offset: 0x0000E106
		public int AddMediaJobVolunteer(AlternateFormatVolunteerDTO volunteer)
		{
			return base.Post<AlternateFormatVolunteerDTO, int>(volunteer, "mediajobvolunteer/volunteer");
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000FF14 File Offset: 0x0000E114
		public void UpdateMediaJobVolunteer(AlternateFormatVolunteerDTO volunteer)
		{
			base.Put<AlternateFormatVolunteerDTO>(volunteer, "mediajobvolunteer/volunteer");
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000FF22 File Offset: 0x0000E122
		public void DeleteMediaJobVolunteer(int vPersonId)
		{
			base.Delete(string.Format("mediajobvolunteer/volunteerbypersonid/{0}", vPersonId));
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000FF3A File Offset: 0x0000E13A
		public AlternateFormatVolunteerDTO GetMediaVolunteerByPersonId(int personId)
		{
			return base.Get<AlternateFormatVolunteerDTO>(string.Format("mediajobvolunteer/volunteerbypersonid/{0}", personId), true);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000FF53 File Offset: 0x0000E153
		public MediaJobVolunteerInfoDTO GetMediaVolunteerById(int jobVolunteerId)
		{
			return base.Get<MediaJobVolunteerInfoDTO>(string.Format("mediajobvolunteer/job/{0}", jobVolunteerId), true);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000FF6C File Offset: 0x0000E16C
		public MediaJobVolunteerInfoDTO GetMediaVolunteerByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			return base.Get<MediaJobVolunteerInfoDTO>(string.Format("mediajobvolunteer/volunteer/{0}/jobid/{1}", volunteerId, mediaJobId), true);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000FF8B File Offset: 0x0000E18B
		public IList<MediaJobVolunteerInfoDTO> GetMediaVolunteersAssignedToMediaJob(int mediaJobId)
		{
			return base.GetMany<MediaJobVolunteerInfoDTO>(string.Format("mediajobvolunteer/assigned/jobid/{0}", mediaJobId), true);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		public IList<MediaJobVolunteerInfoDTO> GetMediaJobVolunteerInfoByVolunteer(int volunteerId)
		{
			return base.GetMany<MediaJobVolunteerInfoDTO>(string.Format("mediajobvolunteer/volunteerbyid/{0}", volunteerId), true);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000FFBD File Offset: 0x0000E1BD
		public int CreateMediaJobVolunteer(MediaJobVolunteerInfoDTO mediaJobVolunteer)
		{
			return base.Post<MediaJobVolunteerInfoDTO, int>(mediaJobVolunteer, "mediajobvolunteer");
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		public void ChangeMediaJobVolunteerNotes(int volunteerId, int mediaJobId, string newNotes)
		{
			ChangeMediaJobVolunteerNotesReq changeMediaJobVolunteerNotesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerNotesReq>();
			changeMediaJobVolunteerNotesReq.VolunteerId = volunteerId;
			changeMediaJobVolunteerNotesReq.MediaJobId = mediaJobId;
			changeMediaJobVolunteerNotesReq.Notes = newNotes;
			base.Put<ChangeMediaJobVolunteerNotesReq>(changeMediaJobVolunteerNotesReq, "mediajobvolunteer/changejobvolunteernotes");
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00010008 File Offset: 0x0000E208
		public void ChangeMediaJobVolunteerActiveStatus(int volunteerId, int mediaJobId, bool isActive)
		{
			ChangeMediaJobVolunteerActiveStatusReq changeMediaJobVolunteerActiveStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerActiveStatusReq>();
			changeMediaJobVolunteerActiveStatusReq.VolunteerId = volunteerId;
			changeMediaJobVolunteerActiveStatusReq.MediaJobId = mediaJobId;
			changeMediaJobVolunteerActiveStatusReq.IsActive = isActive;
			base.Put<ChangeMediaJobVolunteerActiveStatusReq>(changeMediaJobVolunteerActiveStatusReq, "mediajobvolunteer/changejobvolunteeractivestatus");
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00010044 File Offset: 0x0000E244
		public void ChangeMediaJobVolunteerActiveStatus(int jobVolunteerId, bool isActive)
		{
			ChangeMediaJobVolunteerActiveStatusReq changeMediaJobVolunteerActiveStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerActiveStatusReq>();
			changeMediaJobVolunteerActiveStatusReq.JobVolunteerId = jobVolunteerId;
			changeMediaJobVolunteerActiveStatusReq.IsActive = isActive;
			base.Put<ChangeMediaJobVolunteerActiveStatusReq>(changeMediaJobVolunteerActiveStatusReq, "mediajobvolunteer/changejobvolunteeractivestatus");
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00010078 File Offset: 0x0000E278
		public void ChangeMediaJobVolunteerListActiveStatus(IList<int> jobVolunteerIdList, bool isActive)
		{
			ChangeMediaJobVolunteerListActiveStatusReq changeMediaJobVolunteerListActiveStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ChangeMediaJobVolunteerListActiveStatusReq>();
			changeMediaJobVolunteerListActiveStatusReq.JobVolunteerIdList = jobVolunteerIdList;
			changeMediaJobVolunteerListActiveStatusReq.IsActive = isActive;
			base.Put<ChangeMediaJobVolunteerListActiveStatusReq>(changeMediaJobVolunteerListActiveStatusReq, "mediajobvolunteer/changejobvolunteerlistactivestatus");
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000100AA File Offset: 0x0000E2AA
		public IList<MediaJobVolunteerWorkingHoursInfoDTO> GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			return base.GetMany<MediaJobVolunteerWorkingHoursInfoDTO>(string.Format("mediajobvolunteer/volunteerworkinghoursbyjob/volunteerid/{0}/jobid/{1}", volunteerId, mediaJobId), true);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000100C9 File Offset: 0x0000E2C9
		public IList<MediaJobVolunteerWorkingHoursInfoDTO> GetAllMediaJobVolunteerWorkingHoursByVolunteerId(int volunteerId)
		{
			return base.GetMany<MediaJobVolunteerWorkingHoursInfoDTO>(string.Format("mediajobvolunteer/volunteerworkinghours/volunteerid/{0}", volunteerId), true);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000100E2 File Offset: 0x0000E2E2
		public int AddMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfoDTO volunteerWorkingHours)
		{
			return base.Post<MediaJobVolunteerWorkingHoursInfoDTO, int>(volunteerWorkingHours, "mediajobvolunteer/volunteerworkinghours");
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public void UpdateMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfoDTO volunteerWorkingHours)
		{
			base.Put<MediaJobVolunteerWorkingHoursInfoDTO>(volunteerWorkingHours, "mediajobvolunteer/volunteerworkinghours");
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000100FE File Offset: 0x0000E2FE
		public void DeleteMediaJobVolunteerWorkingHours(int jobVolunteerWorkingHoursId)
		{
			base.Delete(string.Format("mediajobvolunteer/volunteerworkinghours/{0}", jobVolunteerWorkingHoursId));
		}
	}
}
