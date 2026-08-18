using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x02000144 RID: 324
	public class TestProctorManager : ITestProctorManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0006BA98 File Offset: 0x00069C98
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0006BAA0 File Offset: 0x00069CA0
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E59 RID: 3673 RVA: 0x0006BAA9 File Offset: 0x00069CA9
		public TestProctorManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0006BABC File Offset: 0x00069CBC
		public IList<Proctor> LoadAllReaders()
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.LoadStaffWithCommonInfoByGroupTitle<Proctor>(TestProctorManager.readerGroups);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0006BAE8 File Offset: 0x00069CE8
		public IList<Proctor> LoadAllScribes()
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.LoadStaffWithCommonInfoByGroupTitle<Proctor>(TestProctorManager.scribeGroups);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0006BB14 File Offset: 0x00069D14
		public IList<Proctor> LoadAllProctors()
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.LoadStaffWithCommonInfoByGroupTitle<Proctor>(TestProctorManager.proctorGroups);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<Proctor> LoadProctorsByAppointmentId(int AppointmentId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0006BB40 File Offset: 0x00069D40
		public Proctor LoadProctorById(int ProctorPersonId)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.LoadStaffWithCommonInfoById<Proctor>(ProctorPersonId);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0006BB68 File Offset: 0x00069D68
		public int CreateProctor(Proctor Proctor)
		{
			return this.CreateProctor(Proctor, "P", TestProctorManager.proctorGroups);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0006BB8C File Offset: 0x00069D8C
		public int CreateReader(Proctor Proctor)
		{
			return this.CreateProctor(Proctor, "R", TestProctorManager.readerGroups);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0006BBB0 File Offset: 0x00069DB0
		public int CreateScribe(Proctor Proctor)
		{
			return this.CreateProctor(Proctor, "S", TestProctorManager.scribeGroups);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0006BBD4 File Offset: 0x00069DD4
		private int CreateProctor(Proctor Proctor, string snumPrefix, string[] groups)
		{
			bool flag = string.IsNullOrEmpty(Proctor.Staff.Student_no);
			if (flag)
			{
				IPeopleManager peopleManager = new PeopleManager(this.OpContext);
				Proctor.Staff.Student_no = peopleManager.GetTempStudentNumber(snumPrefix, "");
			}
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			return staffCommonInfoManager.CreateStaffWithCommonInfo(Proctor, groups);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0006BC34 File Offset: 0x00069E34
		public void DeleteProctor(int ProctorPersonId)
		{
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			peopleManager.DeleteUser(ProctorPersonId, true);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0006BC58 File Offset: 0x00069E58
		public void UpdateProctor(Proctor Proctor)
		{
			IStaffCommonInfoManager staffCommonInfoManager = new StaffCommonInfoManager(this.OpContext);
			staffCommonInfoManager.UpdateStaffWithCommonInfo(Proctor, true);
		}

		// Token: 0x040002A5 RID: 677
		private static readonly string[] proctorGroups = new string[]
		{
			"proctors",
			"proctor",
			"invigilators",
			"invigilator"
		};

		// Token: 0x040002A6 RID: 678
		private static readonly string[] readerGroups = new string[]
		{
			"Reader"
		};

		// Token: 0x040002A7 RID: 679
		private static readonly string[] scribeGroups = new string[]
		{
			"Scribe"
		};
	}
}
