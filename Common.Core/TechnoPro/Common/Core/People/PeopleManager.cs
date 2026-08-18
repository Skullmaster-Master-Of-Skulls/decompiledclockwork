using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000AA RID: 170
	public class PeopleManager : IPeopleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00023066 File Offset: 0x00021266
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0002306E File Offset: 0x0002126E
		public IPeopleDAO dao { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00023078 File Offset: 0x00021278
		private IOldUserSettingManager oldUserSettingManager
		{
			get
			{
				IOldUserSettingManager result;
				if ((result = this.osm) == null)
				{
					result = (this.osm = new OldUserSettingManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000230A3 File Offset: 0x000212A3
		public PeopleManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new PeopleDAO(opContext);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00003940 File Offset: 0x00001B40
		private void RefreshCache_OnTimer(object sender, ElapsedEventArgs e)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000230C4 File Offset: 0x000212C4
		public IList<PersonBase> LoadPersonsByIds(IList<int> PersonIds)
		{
			List<int> list = PersonIds.Distinct<int>().ToList<int>();
			IList<Chunk> list2 = list.Count.BreakdownItemsIntoChunks(100000);
			List<PersonBase> list3 = new List<PersonBase>();
			foreach (Chunk chunk in list2)
			{
				list3.AddRange(this.dao.LoadPersonsByIds(list.GetRange(chunk.Start, chunk.End - chunk.Start + 1)));
			}
			return list3;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00023164 File Offset: 0x00021364
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0002316C File Offset: 0x0002136C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000606 RID: 1542 RVA: 0x00023178 File Offset: 0x00021378
		private void UpdateCacheForAddedOrModifiedUser(PersonBase user)
		{
			bool flag = user == null || user.PersonId < 1;
			if (!flag)
			{
				List<PersonBase> list = this.LoadAllUserObjects(false);
				PersonBase personBase = list.Find((PersonBase f) => f.PersonId == user.PersonId);
				bool flag2 = personBase == null;
				if (flag2)
				{
					list.Add(user);
				}
				else
				{
					list.Remove(personBase);
					list.Add(user);
				}
				OperationContext opContext = this.OpContext;
				IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
				userDatabaseCacheStorageManager.Clear(eServerCacheItemType.uAllowedResourcePids);
				userDatabaseCacheStorageManager.Clear(eServerCacheItemType.uAllowedRoomPids);
				userDatabaseCacheStorageManager.Clear(eServerCacheItemType.uAllowedStaffPids);
				userDatabaseCacheStorageManager.Clear(eServerCacheItemType.uAllowedStudentPids);
				userDatabaseCacheStorageManager.Clear(eServerCacheItemType.uAllowedPidsCombined);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002325C File Offset: 0x0002145C
		private void UpdateCacheForAddedOrModifiedGroup(Group group)
		{
			bool flag = group == null || group.GroupId < 1;
			if (!flag)
			{
				List<Group> list = this.LoadAllGroups();
				Group group2 = list.Find((Group f) => f.GroupId == group.GroupId);
				bool flag2 = group2 == null;
				if (flag2)
				{
					list.Add(group);
				}
				else
				{
					list.Remove(group2);
					list.Add(group);
				}
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000232E4 File Offset: 0x000214E4
		private void UpdateCacheForDeletedUser(int pid)
		{
			bool flag = pid < 1;
			if (!flag)
			{
				List<PersonBase> list = this.LoadAllUserObjects(false);
				PersonBase personBase = list.Find((PersonBase f) => f.PersonId == pid);
				bool flag2 = personBase != null;
				if (flag2)
				{
					list.Remove(personBase);
				}
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00023340 File Offset: 0x00021540
		private void UpdateCacheForDeletedGroup(int gid)
		{
			bool flag = gid < 1;
			if (!flag)
			{
				List<Group> list = this.LoadAllGroups();
				Group group = list.Find((Group f) => f.GroupId == gid);
				bool flag2 = group != null;
				if (flag2)
				{
					list.Remove(group);
				}
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00023398 File Offset: 0x00021598
		private List<int> LoadAllowedGroupGids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			List<int> list = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithGroupIdsForGroupsDropList);
			bool flag = list.Count == 0;
			if (flag)
			{
				bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
				List<Group> list2 = this.LoadAllGroups();
				bool flag2 = !settingValue_Bool;
				if (flag2)
				{
					list = list2.FindAll((Group g) => g.VisibleInCalendar).ConvertAll<int>((Group f) => f.GroupId);
				}
			}
			return list;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00023448 File Offset: 0x00021648
		public List<int> LoadAllowedStudentPids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			string text = oldUserSettingManager.GetSettingValue_String(whoAmI, eSettingCode.SETTING_GroupWithStudentForDropList_SQL, false);
			text = text.Replace("@whoamiid", whoAmI.ToString());
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithStudentForDropList);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			return this.dao.LoadAllowedStudentPids(text, settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x000234BC File Offset: 0x000216BC
		private List<int> LoadAllowedStaffPids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithStaffForDropList);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			return this.dao.LoadAllowedStaffPids(settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00023510 File Offset: 0x00021710
		public static List<int> LoadAllowedRoomPids(OperationContext OpContext, IOldUserSettingManager sm, IPeopleDAO dao)
		{
			bool flag = sm == null;
			if (flag)
			{
				sm = new OldUserSettingManager(OpContext);
			}
			int whoAmI = OpContext.WhoAmI;
			List<int> settingValue_ConcatenatedIntList = sm.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithRoomForDropList);
			bool settingValue_Bool = sm.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			bool flag2 = dao == null;
			if (flag2)
			{
				dao = new PeopleDAO(OpContext);
			}
			return dao.LoadAllowedRoomPids(settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00023570 File Offset: 0x00021770
		private List<int> LoadAllowedRoomPids()
		{
			return PeopleManager.LoadAllowedRoomPids(this.OpContext, this.oldUserSettingManager, this.dao);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002359C File Offset: 0x0002179C
		private List<int> LoadAllowedResourcePids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithResourceForDropList);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			return this.dao.LoadAllowedResourcePids(settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000235EC File Offset: 0x000217EC
		public int CreateUser(PersonBase User, List<int> GroupIds)
		{
			bool flag = User == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				PersonBase existingPerson = string.IsNullOrEmpty(User.Student_no) ? null : this.dao.LoadPersonByStudentNumber(User.Student_no);
				bool flag2 = existingPerson != null;
				if (flag2)
				{
					bool flag3 = existingPerson.Groups == null;
					if (flag3)
					{
						existingPerson.Groups = new List<Group>();
					}
					IEnumerable<int> source = from g in GroupIds
					where existingPerson.Groups.FirstOrDefault((Group h) => h.GroupId == g) == null
					select g;
					this.AddUserToGroups(existingPerson.PersonId, source.ToList<int>());
					result = existingPerson.PersonId;
				}
				else
				{
					int num = this.dao.CreateUser(User, GroupIds);
					string str = User.Student_no ?? "NULL";
					string str2 = "-";
					string str3;
					if (GroupIds != null)
					{
						str3 = string.Join(",", (from g in GroupIds
						select g.ToString()).ToArray<string>());
					}
					else
					{
						str3 = "NULL";
					}
					string text = str + str2 + str3;
					bool flag4 = num > 0;
					if (flag4)
					{
						CWLogger.Logger.Trace("Common.Core.People.PeopleManager.CreateUser:SuccessfullyCreatedUser:pid={0}:info={0}", num.ToString(), text);
						User.PersonId = num;
						User = this.LoadPerson(num);
						this.UpdateCacheForAddedOrModifiedUser(User);
					}
					else
					{
						CWLogger.Logger.Warn("Common.Core.People.PeopleManager.CreateUser:FailedToCreateUser:info={0}", text);
					}
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0002376C File Offset: 0x0002196C
		public bool IsPersonInAtLeastOneCoreGroup(int personId, params eCoreGroup[] coreGroups)
		{
			PersonBase personBase = this.LoadPerson(personId);
			bool result;
			if (personBase != null)
			{
				List<Group> groups = personBase.Groups;
				int[] personGroupIds;
				if (groups == null)
				{
					personGroupIds = null;
				}
				else
				{
					personGroupIds = (from g in groups
					select g.GroupId).ToArray<int>();
				}
				result = this.IsPersonInAtLeastOneCoreGroup(personGroupIds, coreGroups);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000237CC File Offset: 0x000219CC
		public bool IsPersonInAtLeastOneCoreGroup(int[] personGroupIds, params eCoreGroup[] coreGroups)
		{
			return coreGroups != null && coreGroups.Any(delegate(eCoreGroup g)
			{
				int[] personGroupIds2 = personGroupIds;
				return personGroupIds2 != null && personGroupIds2.Contains((int)g);
			});
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00023804 File Offset: 0x00021A04
		public bool IsPersonInCoreGroup(PersonBase Person, eCoreGroup CoreGroup)
		{
			return Person.CoreGroup == CoreGroup;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0002381F File Offset: 0x00021A1F
		public void AddPersonToCoreGroup(PersonBase Person, eCoreGroup CoreGroup)
		{
			Person.CoreGroup |= CoreGroup;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00023834 File Offset: 0x00021A34
		public string GetStudentName(PersonBase Person)
		{
			bool flag = Person == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Format("{0} {1} . ({2})", Person.FirstName, Person.LastName, Person.Student_no);
			}
			return result;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00023874 File Offset: 0x00021A74
		public List<PersonBase> LoadGroupMembers(int GroupId)
		{
			return this.LoadGroupMembers(new int[]
			{
				GroupId
			});
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002389C File Offset: 0x00021A9C
		public List<PersonBase> LoadGroupMembers(int[] GroupIds)
		{
			List<PersonBase> list = this.dao.LoadGroupMembers(GroupIds);
			list.Sort((PersonBase p1, PersonBase p2) => p1.GetName().CompareTo(p2.GetName()));
			return list;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000238E4 File Offset: 0x00021AE4
		public PersonBase CopyPerson(PersonBase Person)
		{
			return new PersonBase
			{
				PersonId = Person.PersonId,
				FirstName = Person.FirstName,
				MiddleName = Person.MiddleName,
				LastName = Person.LastName,
				Student_no = Person.Student_no,
				CoreGroup = Person.CoreGroup,
				Groups = Person.Groups
			};
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00023958 File Offset: 0x00021B58
		private void CheckForNewPeopleToAddToCache()
		{
			try
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				object obj = cacheStorageManager[eServerCacheItemType.uAllUserObjectsBiggestPid];
				int num = (obj == null) ? 0 : ((int)obj);
				CWLogger.Logger.Debug("CheckForNewPeopleToAddToCache:Biggestpid={0}", num.ToString());
				bool flag = num < 1;
				if (!flag)
				{
					int lastPersonIdAddedToClockWork = this.dao.GetLastPersonIdAddedToClockWork();
					CWLogger.Logger.Debug("CheckForNewPeopleToAddToCache:lastpidadded={0}", lastPersonIdAddedToClockWork.ToString());
					bool flag2 = lastPersonIdAddedToClockWork < 1;
					if (!flag2)
					{
						bool flag3 = lastPersonIdAddedToClockWork > num;
						if (flag3)
						{
							IList<int> pidsGreaterThan = this.dao.GetPidsGreaterThan(num);
							bool flag4 = pidsGreaterThan != null;
							if (flag4)
							{
								foreach (int personId in pidsGreaterThan)
								{
									CWLogger.Logger.Debug("CheckForNewPeopleToAddToCache:pid={0}", personId.ToString());
									PersonBase user = this.LoadPerson(personId);
									this.UpdateCacheForAddedOrModifiedUser(user);
								}
								cacheStorageManager.Insert(eServerCacheItemType.uAllUserObjectsBiggestPid, lastPersonIdAddedToClockWork);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("CheckForNewPeopleToAddToCache:Error={0}", ex.ToString());
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00023ACC File Offset: 0x00021CCC
		public List<PersonBase> LoadAllUserObjects(bool CheckForNewStudents = true)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<PersonBase> list = (List<PersonBase>)cacheStorageManager[eServerCacheItemType.allUserObjects];
			bool flag = list != null && list.Count > 0 && CheckForNewStudents;
			if (flag)
			{
				this.CheckForNewPeopleToAddToCache();
			}
			bool flag2 = list == null;
			if (flag2)
			{
				list = new List<PersonBase>();
				cacheStorageManager.Insert(eServerCacheItemType.allUserObjects, list, TimeSpan.FromHours(16.0), false);
				int num;
				list = this.dao.LoadAllUserObjectsAndBiggestPid(out num, false);
				cacheStorageManager[eServerCacheItemType.allUserObjects] = list;
				cacheStorageManager.Insert(eServerCacheItemType.uAllUserObjectsBiggestPid, num);
				CWLogger.Logger.Trace("PeopleManager::LoadAllUserObjects::biggestPid={0}:allUserObjectsCount={1}", num.ToString(), (list == null) ? "NULL" : list.Count.ToString());
			}
			return list;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00023BA8 File Offset: 0x00021DA8
		private List<Group> LoadAllGroups()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<Group> list = (List<Group>)cacheStorageManager[eServerCacheItemType.allGroups];
			bool flag = list == null;
			if (flag)
			{
				list = this.dao.LoadAllGroups();
				cacheStorageManager[eServerCacheItemType.allGroups] = list;
			}
			return list;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00023BF8 File Offset: 0x00021DF8
		public List<Group> LoadGroups()
		{
			List<Group> list = this.LoadAllGroups();
			List<int> allowedGids = this.LoadAllowedGroupGids();
			return list.FindAll((Group g) => allowedGids.Contains(g.GroupId));
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00023C38 File Offset: 0x00021E38
		public List<Group> LoadRoomGroups()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<Group> allRoomGroups = (List<Group>)cacheStorageManager[eServerCacheItemType.allRoomGroups];
			bool flag = allRoomGroups == null;
			if (flag)
			{
				allRoomGroups = this.dao.LoadAllRoomGroups();
				cacheStorageManager[eServerCacheItemType.allRoomGroups] = allRoomGroups;
			}
			List<Group> list = this.LoadGroups();
			return list.FindAll((Group g) => allRoomGroups.Find((Group g2) => g2.GroupId == g.GroupId) != null);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000072EA File Offset: 0x000054EA
		public DateTime? GetStudentAccommodationExpiryDate(int PersonId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000072EA File Offset: 0x000054EA
		public bool IsStudentsAccommodationsExpired(int PersonId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00023CBC File Offset: 0x00021EBC
		public List<PersonBase> LoadStudents()
		{
			int whoAmI = this.OpContext.WhoAmI;
			eServerCacheItemType eServerCacheItemType = eServerCacheItemType.uAllowedStudentPids;
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			object obj = userDatabaseCacheStorageManager[whoAmI, eServerCacheItemType];
			bool flag = obj == null;
			List<int> allowedPids;
			if (flag)
			{
				allowedPids = this.LoadAllowedStudentPids();
				userDatabaseCacheStorageManager.Insert(whoAmI, eServerCacheItemType, allowedPids);
			}
			else
			{
				allowedPids = (List<int>)obj;
			}
			List<PersonBase> source = this.LoadAllUserObjects(true);
			return (from uo in source
			where allowedPids.Contains(uo.PersonId)
			select uo).ToList<PersonBase>();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00023D68 File Offset: 0x00021F68
		public List<PersonBase> LoadStaff()
		{
			int whoAmI = this.OpContext.WhoAmI;
			eServerCacheItemType eServerCacheItemType = eServerCacheItemType.uAllowedStaffPids;
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			object obj = userDatabaseCacheStorageManager[whoAmI, eServerCacheItemType];
			bool flag = obj == null;
			List<int> allowedPids;
			if (flag)
			{
				allowedPids = this.LoadAllowedStaffPids();
				userDatabaseCacheStorageManager.Insert(whoAmI, eServerCacheItemType, allowedPids);
			}
			else
			{
				allowedPids = (List<int>)obj;
			}
			List<PersonBase> list = this.LoadAllUserObjects(true);
			return list.FindAll((PersonBase uo) => allowedPids.Contains(uo.PersonId));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00023E10 File Offset: 0x00022010
		public List<PersonBase> LoadRooms()
		{
			int whoAmI = this.OpContext.WhoAmI;
			eServerCacheItemType eServerCacheItemType = eServerCacheItemType.uAllowedRoomPids;
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			object obj = userDatabaseCacheStorageManager[whoAmI, eServerCacheItemType];
			bool flag = obj == null;
			List<int> allowedPids;
			if (flag)
			{
				allowedPids = this.LoadAllowedRoomPids();
				userDatabaseCacheStorageManager.Insert(whoAmI, eServerCacheItemType, allowedPids);
			}
			else
			{
				allowedPids = (List<int>)obj;
			}
			List<PersonBase> list = this.LoadAllUserObjects(true);
			return list.FindAll((PersonBase uo) => allowedPids.Contains(uo.PersonId));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00023EB8 File Offset: 0x000220B8
		public List<PersonBase> LoadResources()
		{
			int whoAmI = this.OpContext.WhoAmI;
			eServerCacheItemType eServerCacheItemType = eServerCacheItemType.uAllowedResourcePids;
			OperationContext opContext = this.OpContext;
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager((opContext != null) ? opContext.TenantId : null);
			object obj = userDatabaseCacheStorageManager[whoAmI, eServerCacheItemType];
			bool flag = obj == null;
			List<int> allowedPids;
			if (flag)
			{
				allowedPids = this.LoadAllowedResourcePids();
				userDatabaseCacheStorageManager.Insert(whoAmI, eServerCacheItemType, allowedPids);
			}
			else
			{
				allowedPids = (List<int>)obj;
			}
			List<PersonBase> list = this.LoadAllUserObjects(true);
			return list.FindAll((PersonBase uo) => allowedPids.Contains(uo.PersonId));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00023F60 File Offset: 0x00022160
		public PersonBase LoadPerson(int PersonId)
		{
			return this.dao.LoadPerson(PersonId);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00023F80 File Offset: 0x00022180
		public PersonBase LoadPersonByStudentNumber(string Student_No)
		{
			bool flag;
			return this.LoadPersonByStudentNumber(Student_No, out flag, false);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00023F9C File Offset: 0x0002219C
		public PersonBase LoadPersonByStudentNumber(string Student_No, out bool WhoAmIIsAllowedToSeeThisStudent, bool CheckIfWhoAmIIsAllowedToSeeThisStudent = false)
		{
			PersonBase personBase = this.dao.LoadPersonByStudentNumber(Student_No);
			bool flag = personBase != null && CheckIfWhoAmIIsAllowedToSeeThisStudent;
			if (flag)
			{
				List<int> list = this.LoadAllowedStudentPids();
				WhoAmIIsAllowedToSeeThisStudent = list.Contains(personBase.PersonId);
			}
			else
			{
				WhoAmIIsAllowedToSeeThisStudent = true;
			}
			return personBase;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00023FE4 File Offset: 0x000221E4
		public DateTime GetPersonDateAdded(int PersonId)
		{
			return this.dao.GetPersonDateAdded(PersonId);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00024004 File Offset: 0x00022204
		public IList<PersonBase> FindStudentBySearchString(string searchString)
		{
			bool flag = string.IsNullOrEmpty(searchString);
			IList<PersonBase> result;
			if (flag)
			{
				result = new List<PersonBase>();
			}
			else
			{
				List<PersonBase> list = this.LoadStudents();
				int num = searchString.IndexOf(",");
				bool flag2 = num > 0;
				string lastName;
				string firstName;
				if (flag2)
				{
					lastName = searchString.Substring(0, num);
					firstName = searchString.Substring(num + 1).Trim();
				}
				else
				{
					lastName = searchString;
					firstName = "";
				}
				bool flag3 = firstName.Length > 0;
				List<PersonBase> list2;
				if (flag3)
				{
					list2 = list.FindAll((PersonBase p) => p.LastName.EqualsCaseAndAccentInsensitive(lastName) && p.FirstName.IndexOfCaseAndAccentInsensitive(firstName) >= 0);
				}
				else
				{
					list2 = list.FindAll((PersonBase p) => (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.IndexOfCaseAndAccentInsensitive(searchString) >= 0) || (!string.IsNullOrEmpty(p.LastName) && p.LastName.IndexOfCaseAndAccentInsensitive(searchString) >= 0) || (!string.IsNullOrEmpty(p.Student_no) && p.Student_no.IndexOfCaseAndAccentInsensitive(searchString) >= 0));
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000240F0 File Offset: 0x000222F0
		private List<UserGroupObject> SearchForPersonObject(List<PersonBase> list, string SearchString, List<int> allowedPids, List<int> allowedGids)
		{
			IEnumerable<PersonBase> source = from p in list
			where (allowedPids.BinarySearch(p.PersonId) >= 0 || allowedGids.Contains((int)p.CoreGroup)) && ((!string.IsNullOrEmpty(p.FirstName) && p.FirstName.IndexOfCaseAndAccentInsensitive(SearchString) >= 0) || (!string.IsNullOrEmpty(p.LastName) && p.LastName.IndexOfCaseAndAccentInsensitive(SearchString) >= 0) || (!string.IsNullOrEmpty(p.Student_no) && p.Student_no.IndexOfCaseAndAccentInsensitive(SearchString) >= 0))
			select p;
			return source.Select(new Func<PersonBase, UserGroupObject>(this.GetUserGroupObjectFromPersonBase)).ToList<UserGroupObject>();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00024148 File Offset: 0x00022348
		private List<UserGroupObject> SearchForPersonObjectWithCommaInSearchString(List<PersonBase> list, string SearchString, List<int> allowedPids, int commaCharacterIndex, List<int> allowedGids)
		{
			string ln = SearchString.Substring(0, commaCharacterIndex).Trim();
			string fn = SearchString.Substring(commaCharacterIndex + 1).Trim();
			int num = fn.IndexOf(' ');
			bool flag = num > 0;
			if (flag)
			{
				string text = fn.Substring(num + 1).Trim();
				fn = fn.Substring(0, num).Trim();
			}
			IEnumerable<PersonBase> source = from p in list
			where (allowedPids.BinarySearch(p.PersonId) > 0 || allowedGids.Contains((int)p.CoreGroup)) && (!string.IsNullOrEmpty(p.LastName) && p.LastName.EqualsCaseAndAccentInsensitive(ln)) && (fn.Length < 1 || (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.IndexOfCaseAndAccentInsensitive(fn) >= 0))
			select p;
			return source.Select(new Func<PersonBase, UserGroupObject>(this.GetUserGroupObjectFromPersonBase)).ToList<UserGroupObject>();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00024214 File Offset: 0x00022414
		private UserGroupObject GetUserGroupObjectFromPersonBase(PersonBase s)
		{
			UserGroupObject result;
			switch (s.CoreGroup)
			{
			case eCoreGroup.Students:
				result = new UserGroupObject
				{
					DisplayName = s.GetStudentNameWithMiddleName(),
					Description = "",
					ObjectId = new UserGroupObjectId
					{
						UserGroupObjectType = eUserGroupObjectType.Student,
						ObjectId = s.PersonId
					},
					Person = s
				};
				break;
			case eCoreGroup.Staff:
				result = new UserGroupObject
				{
					DisplayName = s.GetStudentName(),
					Description = "",
					ObjectId = new UserGroupObjectId
					{
						UserGroupObjectType = eUserGroupObjectType.Staff,
						ObjectId = s.PersonId
					},
					Person = s
				};
				break;
			case eCoreGroup.Rooms:
			case eCoreGroup.Resources:
				result = new UserGroupObject
				{
					DisplayName = (s.LastName ?? ""),
					Description = (s.FirstName ?? ""),
					ObjectId = new UserGroupObjectId
					{
						UserGroupObjectType = ((s.CoreGroup == eCoreGroup.Resources) ? eUserGroupObjectType.Resource : eUserGroupObjectType.Room),
						ObjectId = s.PersonId
					},
					Person = s
				};
				break;
			case eCoreGroup.Tutors:
				result = new UserGroupObject
				{
					DisplayName = s.GetStudentName(),
					Description = "",
					ObjectId = new UserGroupObjectId
					{
						UserGroupObjectType = eUserGroupObjectType.Tutor,
						ObjectId = s.PersonId
					},
					Person = s
				};
				break;
			default:
				result = new UserGroupObject
				{
					DisplayName = s.GetName(),
					Description = "",
					ObjectId = new UserGroupObjectId
					{
						UserGroupObjectType = eUserGroupObjectType.Student,
						ObjectId = s.PersonId
					},
					Person = s
				};
				break;
			}
			return result;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000243E8 File Offset: 0x000225E8
		private List<PersonBase> SearchForStudent(List<PersonBase> list, string SearchString)
		{
			List<PersonBase> list2 = null;
			int num = SearchString.IndexOf(',');
			bool flag = num > 0;
			if (flag)
			{
				string ln = SearchString.Substring(0, num).Trim();
				string fn = SearchString.Substring(num + 1).Trim();
				bool flag2 = fn.Length > 0;
				if (flag2)
				{
					return list.FindAll((PersonBase p) => !string.IsNullOrEmpty(p.FirstName) && !string.IsNullOrEmpty(p.LastName) && p.FirstName.IndexOfCaseAndAccentInsensitive(fn) >= 0 && p.LastName.IndexOfCaseAndAccentInsensitive(ln) >= 0);
				}
				SearchString = ln;
			}
			int num2 = SearchString.IndexOf(' ');
			bool flag3 = num2 > 0;
			if (flag3)
			{
				string fn2 = SearchString.Substring(0, num2).Trim();
				string ln2 = SearchString.Substring(num2 + 1).Trim();
				bool flag4 = ln2.Length > 0;
				if (flag4)
				{
					return list.FindAll((PersonBase p) => !string.IsNullOrEmpty(p.FirstName) && !string.IsNullOrEmpty(p.LastName) && p.FirstName.IndexOfCaseAndAccentInsensitive(fn2) >= 0 && p.LastName.IndexOfCaseAndAccentInsensitive(ln2) >= 0);
				}
				SearchString = fn2;
			}
			List<PersonBase> found2 = list.FindAll((PersonBase p) => (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.IndexOfCaseAndAccentInsensitive(SearchString) >= 0) || (!string.IsNullOrEmpty(p.LastName) && p.LastName.IndexOfCaseAndAccentInsensitive(SearchString) >= 0) || (!string.IsNullOrEmpty(p.Student_no) && p.Student_no.IndexOfCaseAndAccentInsensitive(SearchString) >= 0) || (!string.IsNullOrEmpty(p.MiddleName) && p.MiddleName.IndexOfCaseAndAccentInsensitive(SearchString) >= 0));
			bool flag5 = list2 != null;
			if (flag5)
			{
				found2.AddRange(list2.FindAll((PersonBase f) => found2.Find((PersonBase g) => g.PersonId == f.PersonId) == null));
			}
			else
			{
				list2 = found2;
			}
			return list2;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002457C File Offset: 0x0002277C
		private List<PersonBase> SearchForStaff(List<PersonBase> list, string SearchString)
		{
			return this.SearchForStudent(list, SearchString);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00024598 File Offset: 0x00022798
		public IList<UserGroupObject> FindUserGroupObjectBySearchString(string SearchString, eUserGroupObjectType[] ObjectTypesToExclude, int startIndex, int MaxResultsCount, out int TotalResultsCount)
		{
			bool flag = string.IsNullOrEmpty(SearchString);
			IList<UserGroupObject> result;
			if (flag)
			{
				TotalResultsCount = 0;
				result = new List<UserGroupObject>();
			}
			else
			{
				bool flag2 = ObjectTypesToExclude == null;
				if (flag2)
				{
					ObjectTypesToExclude = new eUserGroupObjectType[0];
				}
				List<PersonBase> list = this.LoadAllUserObjects(true);
				List<Group> list2 = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Group) < 0) ? this.LoadGroups() : new List<Group>();
				List<int> list3;
				if (list2 != null)
				{
					list3 = list2.ConvertAll<int>((Group g) => g.GroupId);
				}
				else
				{
					list3 = new List<int>();
				}
				List<int> allowedGids = list3;
				List<int> list4 = null;
				bool flag3 = list4 == null;
				if (flag3)
				{
					List<int> first = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Student) < 0) ? this.LoadAllowedStudentPids() : new List<int>();
					List<int> second = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Staff) < 0) ? this.LoadAllowedStaffPids() : new List<int>();
					List<int> second2 = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Room) < 0) ? this.LoadAllowedRoomPids() : new List<int>();
					List<int> second3 = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Resource) < 0) ? this.LoadAllowedResourcePids() : new List<int>();
					list4 = first.Union(second).Union(second2).Union(second3).ToList<int>();
					list4.Sort((int i1, int i2) => i1.CompareTo(i2));
				}
				CWLogger.Logger.Trace("FindUserGroupObjectBySearchString:AllUserObjectsCount={0}", list.Count.ToString());
				int num = SearchString.IndexOf(',');
				CWLogger.Logger.Trace("FindUserGroupObjectBySearchString:ind={0}", num.ToString());
				bool flag4 = num > 0;
				List<UserGroupObject> list5;
				if (flag4)
				{
					list5 = this.SearchForPersonObjectWithCommaInSearchString(list, SearchString, list4, num, allowedGids);
				}
				else
				{
					list5 = this.SearchForPersonObject(list, SearchString, list4, allowedGids);
				}
				List<Group> list6 = list2.FindAll((Group g) => !string.IsNullOrEmpty(g.Description) && g.Description.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0);
				List<UserGroupObject> list7 = new List<UserGroupObject>(list5.Count + list6.Count);
				list7.AddRange(list5);
				list7.AddRange(list6.ConvertAll<UserGroupObject>((Group g) => new UserGroupObject
				{
					DisplayName = (g.Description ?? ""),
					ObjectId = new UserGroupObjectId
					{
						UserGroupObjectType = eUserGroupObjectType.Group,
						ObjectId = g.GroupId
					},
					Person = null
				}));
				list7.Sort((UserGroupObject u1, UserGroupObject u2) => u1.DisplayName.CompareTo(u2.DisplayName));
				TotalResultsCount = list7.Count;
				bool flag5 = startIndex >= list7.Count;
				if (flag5)
				{
					result = new List<UserGroupObject>();
				}
				else
				{
					int num2 = list7.Count - startIndex;
					bool flag6 = num2 < MaxResultsCount;
					if (flag6)
					{
						result = list7.GetRange(startIndex, num2);
					}
					else
					{
						list7 = list7.GetRange(startIndex, MaxResultsCount);
						result = list7;
					}
				}
			}
			return result;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00024858 File Offset: 0x00022A58
		public void UpdateUser(PersonBase User, bool UpdateGroupMemberships)
		{
			bool flag = User == null || User.PersonId < 1;
			if (flag)
			{
				throw new Exception("Can't update user because user is null or has a 0 personid.");
			}
			this.dao.UpdateUser(User);
			bool flag2 = UpdateGroupMemberships && User.Groups != null;
			if (flag2)
			{
				IPeopleGroupDAO peopleGroupDAO = new PeopleGroupDAO(this.OpContext);
				IList<int> existingGids = peopleGroupDAO.GetGroupIdsByPersonId(User.PersonId);
				IEnumerable<Group> source = from g in User.Groups
				where !existingGids.Contains(g.GroupId)
				select g;
				IEnumerable<int> source2 = from g in existingGids
				where User.Groups.FirstOrDefault((Group h) => h.GroupId == g) == null
				select g;
				this.dao.RemoveUserFromGroups(User.PersonId, source2.ToList<int>());
				this.AddUserToGroups(User.PersonId, source.ToList<Group>().ConvertAll<int>((Group g) => g.GroupId));
			}
			this.UpdateCacheForAddedOrModifiedUser(User);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00024994 File Offset: 0x00022B94
		public int CreateGroup(Group Group)
		{
			int num = this.dao.CreateGroup(Group);
			bool flag = num > 0;
			if (flag)
			{
				Group.GroupId = num;
				this.UpdateCacheForAddedOrModifiedGroup(Group);
			}
			return num;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000249D0 File Offset: 0x00022BD0
		public void UpdateGroup(Group Group)
		{
			bool flag = Group == null || Group.GroupId < 1;
			if (flag)
			{
				throw new Exception("Can't update group because it's null or has a 0 groupid");
			}
			this.dao.UpdateGroup(Group);
			this.UpdateCacheForAddedOrModifiedGroup(Group);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00024A14 File Offset: 0x00022C14
		public void DeleteGroup(int GroupId)
		{
			bool flag = this.dao.DeleteGroup(GroupId);
			bool flag2 = flag;
			if (flag2)
			{
				this.UpdateCacheForDeletedGroup(GroupId);
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00024A3C File Offset: 0x00022C3C
		public void DeleteUser(int PersonId, bool JustDeactivate)
		{
			bool flag = this.dao.DeleteUser(PersonId, JustDeactivate);
			bool flag2 = flag;
			if (flag2)
			{
				this.UpdateCacheForDeletedUser(PersonId);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00024A68 File Offset: 0x00022C68
		public PersonBase UnDeleteUser(int PersonId)
		{
			PersonBase personBase = this.dao.UnDeleteUser(PersonId);
			personBase = this.LoadPerson(PersonId);
			this.UpdateCacheForAddedOrModifiedUser(personBase);
			return personBase;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00024A98 File Offset: 0x00022C98
		public IList<Group> LoadUserGroupMemberships(int PersonId)
		{
			return this.dao.LoadUserGroupMemberships(PersonId);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00024AB6 File Offset: 0x00022CB6
		public void AddUserToGroups(int PersonId, IList<int> GroupIds)
		{
			this.dao.AddUserToGroups(PersonId, GroupIds);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00024AC7 File Offset: 0x00022CC7
		public void RemoveUserFromGroups(int PersonId, IList<int> GroupIds)
		{
			this.dao.RemoveUserFromGroups(PersonId, GroupIds);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00024AD8 File Offset: 0x00022CD8
		public IList<int> LoadPersonIdsByStudentNumbers(IList<string> StudentNumbers)
		{
			return this.dao.LoadPersonIdsByStudentNumbers(StudentNumbers);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00024AF8 File Offset: 0x00022CF8
		public PersonBaseWithExtendedInfo LoadPersonWithExtendedInfo(int PersonId)
		{
			return this.dao.LoadPersonWithExtendedInfo(PersonId);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00024B18 File Offset: 0x00022D18
		public bool IsUserInGroup(int PersonId, int GroupId)
		{
			return this.dao.IsUserInGroup(PersonId, GroupId);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00024B38 File Offset: 0x00022D38
		public List<PersonBase> LoadGroupMembersByPersonIds(int GroupId, IList<int> PersonIds)
		{
			return this.LoadGroupMembersByPersonIds(new int[]
			{
				GroupId
			}, PersonIds);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00024B60 File Offset: 0x00022D60
		public List<PersonBase> LoadGroupMembersByPersonIds(int[] GroupIds, IList<int> PersonIds)
		{
			List<PersonBase> list = this.dao.LoadGroupMembersByPersonIds(GroupIds, PersonIds);
			list.Sort((PersonBase p1, PersonBase p2) => p1.GetName().CompareTo(p2.GetName()));
			return list;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00024BA8 File Offset: 0x00022DA8
		public string GetTempStudentNumber(string prefix, string postfix)
		{
			string text = this.dao.GetTempStudentNumber();
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = Guid.NewGuid().ToString();
			}
			return (prefix ?? "") + text + (postfix ?? "");
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00024C00 File Offset: 0x00022E00
		public IDictionary<string, int> LoadPersonIdsByStudentNumbers2(IList<string> StudentNumbers)
		{
			return this.dao.LoadPersonIdsByStudentNumbers2(StudentNumbers);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00024C20 File Offset: 0x00022E20
		public IList<PersonBase> LoadDeletedAccounts(params int[] GroupIds)
		{
			return this.dao.LoadDeletedAccounts(GroupIds);
		}

		// Token: 0x04000135 RID: 309
		private IOldUserSettingManager osm;
	}
}
