using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x0200015D RID: 349
	public class MediaVolunteerManager : IMediaVolunteerManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00073681 File Offset: 0x00071881
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00073689 File Offset: 0x00071889
		private IMediaVolunteerDAO VolunteerDAO { get; set; }

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00073692 File Offset: 0x00071892
		public MediaVolunteerManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.VolunteerDAO = new MediaVolunteerDAO(this.OpContext);
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x000736B6 File Offset: 0x000718B6
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x000736BE File Offset: 0x000718BE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000736C8 File Offset: 0x000718C8
		public IList<AlternateFormatVolunteer> GetAllMediaJobVolunteers()
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.LoadStaffWithCommonInfoByGroupTitle<AlternateFormatVolunteer>(new string[]
			{
				"MediaVolunteer"
			});
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000736FC File Offset: 0x000718FC
		public int AddMediaJobVolunteer(AlternateFormatVolunteer volunteer)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.CreateStaffWithCommonInfo(volunteer, new string[]
			{
				"MediaVolunteer"
			});
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00073730 File Offset: 0x00071930
		public void UpdateMediaJobVolunteer(AlternateFormatVolunteer volunteer)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			staffCommonInfoManager.UpdateStaffWithCommonInfo(volunteer, true);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00073754 File Offset: 0x00071954
		public void DeleteMediaJobVolunteer(int vPersonId)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			peopleManager.DeleteUser(vPersonId, true);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00073778 File Offset: 0x00071978
		public MediaJobVolunteerInfo GetMediaVolunteerById(int jobVolunteerId)
		{
			return this.VolunteerDAO.GetMediaVolunteerById(jobVolunteerId);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00073798 File Offset: 0x00071998
		public AlternateFormatVolunteer GetMediaVolunteerByPersonId(int personId)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.LoadStaffWithCommonInfoById<AlternateFormatVolunteer>(personId);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000737C0 File Offset: 0x000719C0
		public MediaJobVolunteerInfo GetMediaVolunteerByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			return this.VolunteerDAO.GetMediaVolunteerByVolunteerAndJob(volunteerId, mediaJobId);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000737E0 File Offset: 0x000719E0
		public IList<MediaJobVolunteerInfo> GetMediaVolunteersAssignedToMediaJob(int mediaJobId)
		{
			return this.VolunteerDAO.GetMediaVolunteersAssignedToMediaJob(mediaJobId);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00073800 File Offset: 0x00071A00
		public IList<MediaJobVolunteerInfo> GetMediaJobVolunteerInfoByVolunteer(int volunteerId)
		{
			return this.VolunteerDAO.GetMediaJobVolunteerInfoByVolunteer(volunteerId);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00073820 File Offset: 0x00071A20
		public int CreateMediaJobVolunteer(MediaJobVolunteerInfo mediaJobVolunteer)
		{
			return this.VolunteerDAO.CreateMediaJobVolunteer(mediaJobVolunteer);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0007383E File Offset: 0x00071A3E
		public void ChangeMediaJobVolunteerNotes(int volunteerId, int mediaJobId, string newNotes)
		{
			this.VolunteerDAO.ChangeMediaJobVolunteerNotes(volunteerId, mediaJobId, newNotes);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00073850 File Offset: 0x00071A50
		public void ChangeMediaJobVolunteerActiveStatus(int volunteerId, int mediaJobId, bool isActive)
		{
			this.VolunteerDAO.ChangeMediaJobVolunteerActiveStatus(volunteerId, mediaJobId, isActive);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00073862 File Offset: 0x00071A62
		public void ChangeMediaJobVolunteerActiveStatus(int jobVolunteerId, bool isActive)
		{
			this.VolunteerDAO.ChangeMediaJobVolunteerActiveStatus(jobVolunteerId, isActive);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00073873 File Offset: 0x00071A73
		public void ChangeMediaJobVolunteerActiveStatus(IList<int> jobVolunteerIdList, bool isActive)
		{
			this.VolunteerDAO.ChangeMediaJobVolunteerActiveStatus(jobVolunteerIdList, isActive);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00073884 File Offset: 0x00071A84
		public IList<MediaJobVolunteerWorkingHoursInfo> GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(int volunteerId, int mediaJobId)
		{
			return this.VolunteerDAO.GetMediaJobVolunteerWorkingHoursByVolunteerAndJob(volunteerId, mediaJobId);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000738A4 File Offset: 0x00071AA4
		public IList<MediaJobVolunteerWorkingHoursInfo> GetAllMediaJobVolunteerWorkingHoursByVolunteerId(int volunteerId)
		{
			return this.VolunteerDAO.GetAllMediaJobVolunteerWorkingHoursByVolunteerId(volunteerId);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000738C4 File Offset: 0x00071AC4
		public int AddMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours)
		{
			return this.VolunteerDAO.AddMediaJobVolunteerWorkingHours(volunteerWorkingHours);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000738E2 File Offset: 0x00071AE2
		public void UpdateMediaJobVolunteerWorkingHours(MediaJobVolunteerWorkingHoursInfo volunteerWorkingHours)
		{
			this.VolunteerDAO.UpdateMediaJobVolunteerWorkingHours(volunteerWorkingHours);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000738F2 File Offset: 0x00071AF2
		public void DeleteMediaJobVolunteerWorkingHours(int jobVolunteerWorkingHoursId)
		{
			this.VolunteerDAO.DeleteMediaJobVolunteerWorkingHours(jobVolunteerWorkingHoursId);
		}

		// Token: 0x040002D1 RID: 721
		private const string MEDIA_VOLUNTEER_GROUP_TITLE = "MediaVolunteer";
	}
}
