using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A3 RID: 163
	public class AdminPeopleManager : IAdminPeopleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00021CC7 File Offset: 0x0001FEC7
		public AdminPeopleManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new PeopleDAO(opContext);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00021CE5 File Offset: 0x0001FEE5
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00021CED File Offset: 0x0001FEED
		public OperationContext OpContext { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00021CF8 File Offset: 0x0001FEF8
		private IPeopleGroupManager peopleGroupManager
		{
			get
			{
				IPeopleGroupManager result;
				if ((result = this._pgm) == null)
				{
					result = (this._pgm = new PeopleGroupManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00021D24 File Offset: 0x0001FF24
		public PersonBase LoadPersonWithGroups(int PersonId)
		{
			bool flag = !this.peopleGroupManager.HasManageUserRoomPermissions(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			return this.dao.LoadPerson(PersonId);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00021D6C File Offset: 0x0001FF6C
		public IList<Group> LoadGroupsById(params int[] GroupIds)
		{
			bool flag = !this.peopleGroupManager.HasManageUserRoomPermissions(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IPeopleGroupDAO peopleGroupDAO = new PeopleGroupDAO(this.OpContext);
			return peopleGroupDAO.LoadGroupsById(GroupIds);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00021DBC File Offset: 0x0001FFBC
		public IList<Group> LoadAllGroups()
		{
			IPeopleGroupDAO peopleGroupDAO = new PeopleGroupDAO(this.OpContext);
			return peopleGroupDAO.LoadAllGroups();
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		public IList<PersonBase> LoadPersonsByUsername(string Username, bool includeDeletedAccounts = false)
		{
			bool flag = !this.peopleGroupManager.HasManageUserRoomPermissions(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			IUserAccountManager userAccountManager = new UserAccountManager(this.OpContext);
			IList<int> list = userAccountManager.LoadPersonIdsWithUsername(Username, includeDeletedAccounts);
			bool flag2 = list == null || list.Count < 1;
			IList<PersonBase> result;
			if (flag2)
			{
				result = new List<PersonBase>();
			}
			else
			{
				result = this.dao.LoadPersonsByIds(list);
			}
			return result;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00021E5C File Offset: 0x0002005C
		public PersonBase LoadAnyNonDeletedAccountByStudentNumber(string studentNumber)
		{
			bool flag = !this.peopleGroupManager.HasManageUserRoomPermissions(this.OpContext.WhoAmI);
			if (flag)
			{
				throw new PermissionDeniedException("Not admin");
			}
			string text = (studentNumber ?? "").Trim();
			bool flag2 = text.Length < 1;
			PersonBase result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				IPeopleDAO peopleDAO = new PeopleDAO(this.OpContext);
				result = peopleDAO.LoadAnyNonDeletedAccountByStudentNumber(studentNumber);
			}
			return result;
		}

		// Token: 0x04000121 RID: 289
		private IPeopleDAO dao;

		// Token: 0x04000123 RID: 291
		private IPeopleGroupManager _pgm;
	}
}
