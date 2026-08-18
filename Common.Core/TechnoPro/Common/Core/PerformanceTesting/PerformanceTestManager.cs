using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Impl.PerformanceTesting;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.PerformanceTesting;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.PerformanceTesting;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.PerformanceTesting
{
	// Token: 0x020000A1 RID: 161
	public class PerformanceTestManager : IPerformanceTestManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00020A5B File Offset: 0x0001EC5B
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00020A63 File Offset: 0x0001EC63
		public IPeopleDAO dao { get; set; }

		// Token: 0x0600058C RID: 1420 RVA: 0x00020A6C File Offset: 0x0001EC6C
		public PerformanceTestManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new PeopleDAO(opContext);
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00020A8B File Offset: 0x0001EC8B
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00020A93 File Offset: 0x0001EC93
		public OperationContext OpContext { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00020A9C File Offset: 0x0001EC9C
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

		// Token: 0x06000590 RID: 1424 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		public IList<UserGroupObject> FindUserGroupObjectBySearchString(string SearchString, eUserGroupObjectType[] ObjectTypesToExclude, int startIndex, int MaxResultsCount, out int TotalResultsCount, out string notes)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = string.IsNullOrEmpty(SearchString);
			IList<UserGroupObject> result;
			if (flag)
			{
				TotalResultsCount = 0;
				notes = "Empty search string";
				stopwatch.Stop();
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
				stringBuilder.AppendLine("completed LoadAllUserObjects: " + stopwatch.Elapsed.ToString());
				List<Group> list2 = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Group) < 0) ? this.LoadGroups() : new List<Group>();
				stringBuilder.AppendLine("completed set groups: " + stopwatch.Elapsed.ToString());
				List<int> list3 = null;
				bool flag3 = list3 == null;
				if (flag3)
				{
					List<int> first = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Student) < 0) ? this.LoadAllowedStudentPids() : new List<int>();
					List<int> second = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Staff) < 0) ? this.LoadAllowedStaffPids() : new List<int>();
					List<int> second2 = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Room) < 0) ? this.LoadAllowedRoomPids() : new List<int>();
					List<int> second3 = (Array.IndexOf<eUserGroupObjectType>(ObjectTypesToExclude, eUserGroupObjectType.Resource) < 0) ? this.LoadAllowedResourcePids() : new List<int>();
					list3 = first.Union(second).Union(second2).Union(second3).ToList<int>();
					list3.Sort((int i1, int i2) => i1.CompareTo(i2));
				}
				stringBuilder.AppendLine("completed allowedpids: " + stopwatch.Elapsed.ToString());
				CWLogger.Logger.Trace("FindUserGroupObjectBySearchString:AllUserObjectsCount={0}", list.Count.ToString());
				int num = SearchString.IndexOf(',');
				CWLogger.Logger.Trace("FindUserGroupObjectBySearchString:ind={0}", num.ToString());
				bool flag4 = num > 0;
				List<UserGroupObject> list4;
				if (flag4)
				{
					list4 = this.SearchForPersonObjectWithCommaInSearchString(list, SearchString, list3, num);
				}
				else
				{
					list4 = this.SearchForPersonObject(list, SearchString, list3);
				}
				stringBuilder.AppendLine("completed find matches: " + stopwatch.Elapsed.ToString());
				List<Group> list5 = list2.FindAll((Group g) => !string.IsNullOrEmpty(g.Description) && g.Description.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0);
				stringBuilder.AppendLine("completed findgroups: " + stopwatch.Elapsed.ToString());
				List<UserGroupObject> list6 = new List<UserGroupObject>(list4.Count + list5.Count);
				list6.AddRange(list4);
				stringBuilder.AppendLine("completed concatenate results: " + stopwatch.Elapsed.ToString());
				list6.AddRange(list5.ConvertAll<UserGroupObject>((Group g) => new UserGroupObject
				{
					DisplayName = (g.Description ?? ""),
					ObjectId = new UserGroupObjectId
					{
						UserGroupObjectType = eUserGroupObjectType.Group,
						ObjectId = g.GroupId
					},
					Person = null
				}));
				stringBuilder.AppendLine("completed add range: " + stopwatch.Elapsed.ToString());
				list6.Sort((UserGroupObject u1, UserGroupObject u2) => u1.DisplayName.CompareTo(u2.DisplayName));
				stringBuilder.AppendLine("completed sort: " + stopwatch.Elapsed.ToString());
				TotalResultsCount = list6.Count;
				bool flag5 = startIndex >= list6.Count;
				if (flag5)
				{
					stopwatch.Stop();
					notes = stringBuilder.ToString();
					result = new List<UserGroupObject>();
				}
				else
				{
					int num2 = list6.Count - startIndex;
					bool flag6 = num2 < MaxResultsCount;
					if (flag6)
					{
						stopwatch.Stop();
						notes = stringBuilder.ToString();
						result = list6.GetRange(startIndex, num2);
					}
					else
					{
						list6 = list6.GetRange(startIndex, MaxResultsCount);
						stopwatch.Stop();
						stringBuilder.AppendLine("stopped: " + stopwatch.Elapsed.ToString());
						notes = stringBuilder.ToString();
						result = list6;
					}
				}
			}
			return result;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00020F04 File Offset: 0x0001F104
		private List<UserGroupObject> SearchForPersonObject(List<PersonBase> list, string SearchString, List<int> allowedPids)
		{
			IEnumerable<PersonBase> source = from p in list
			where allowedPids.BinarySearch(p.PersonId) >= 0 && ((!string.IsNullOrEmpty(p.FirstName) && p.FirstName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0) || (!string.IsNullOrEmpty(p.LastName) && p.LastName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0) || (!string.IsNullOrEmpty(p.Student_no) && p.Student_no.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0))
			select p;
			return (from s in source
			select this.GetUserGroupObjectFromPersonBase(s)).ToList<UserGroupObject>();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00020F5C File Offset: 0x0001F15C
		private List<UserGroupObject> SearchForPersonObjectWithCommaInSearchString(List<PersonBase> list, string SearchString, List<int> allowedPids, int commaCharacterIndex)
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
			where allowedPids.BinarySearch(p.PersonId) > 0 && (!string.IsNullOrEmpty(p.LastName) && p.LastName.Equals(ln, StringComparison.OrdinalIgnoreCase)) && (fn.Length < 1 || (!string.IsNullOrEmpty(p.FirstName) && p.FirstName.IndexOf(fn, StringComparison.OrdinalIgnoreCase) >= 0))
			select p;
			return (from s in source
			select this.GetUserGroupObjectFromPersonBase(s)).ToList<UserGroupObject>();
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00021028 File Offset: 0x0001F228
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
						UserGroupObjectType = eUserGroupObjectType.Room,
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

		// Token: 0x06000594 RID: 1428 RVA: 0x000211A0 File Offset: 0x0001F3A0
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

		// Token: 0x06000595 RID: 1429 RVA: 0x00021314 File Offset: 0x0001F514
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

		// Token: 0x06000596 RID: 1430 RVA: 0x000213FC File Offset: 0x0001F5FC
		public PersonBase LoadPerson(int PersonId)
		{
			return this.dao.LoadPerson(PersonId);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002141C File Offset: 0x0001F61C
		public List<PersonBase> LoadAllUserObjects(bool CheckForNewStudents = true)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			List<PersonBase> list = (List<PersonBase>)cacheStorageManager[eServerCacheItemType.allUserObjects];
			bool flag = CheckForNewStudents && list != null;
			if (flag)
			{
				this.CheckForNewPeopleToAddToCache();
			}
			bool flag2 = list == null;
			if (flag2)
			{
				int num;
				list = this.dao.LoadAllUserObjectsAndBiggestPid(out num, false);
				cacheStorageManager.Insert(eServerCacheItemType.allUserObjects, list, TimeSpan.FromHours(16.0), false);
				cacheStorageManager.Insert(eServerCacheItemType.uAllUserObjectsBiggestPid, num);
				CWLogger.Logger.Trace("PeopleManager::LoadAllUserObjects::biggestPid={0}:allUserObjectsCount={1}", num.ToString(), (list == null) ? "NULL" : list.Count.ToString());
			}
			return list;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000214DC File Offset: 0x0001F6DC
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

		// Token: 0x06000599 RID: 1433 RVA: 0x0002152C File Offset: 0x0001F72C
		public List<Group> LoadGroups()
		{
			CWLogger.Logger.Trace("PERFTEST:LoadGroups:A");
			List<Group> list = this.LoadAllGroups();
			CWLogger.Logger.Trace("PERFTEST:LoadGroups:B");
			List<int> allowedGids = this.LoadAllowedGroupGids();
			CWLogger.Logger.Trace("PERFTEST:LoadGroups:C");
			List<Group> result = list.FindAll((Group g) => allowedGids.Contains(g.GroupId));
			CWLogger.Logger.Trace("PERFTEST:LoadGroups:D");
			return result;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000215AC File Offset: 0x0001F7AC
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

		// Token: 0x0600059B RID: 1435 RVA: 0x00021630 File Offset: 0x0001F830
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

		// Token: 0x0600059C RID: 1436 RVA: 0x000216E0 File Offset: 0x0001F8E0
		private List<int> LoadAllowedStudentPids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			string text = oldUserSettingManager.GetSettingValue_String(whoAmI, eSettingCode.SETTING_GroupWithStudentForDropList_SQL, false);
			text = text.Replace("@whoamiid", whoAmI.ToString());
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithStudentForDropList);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			return this.dao.LoadAllowedStudentPids(text, settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00021754 File Offset: 0x0001F954
		private List<int> LoadAllowedStaffPids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithStaffForDropList);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			return this.dao.LoadAllowedStaffPids(settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000217A8 File Offset: 0x0001F9A8
		private List<int> LoadAllowedRoomPids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithRoomForDropList);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			return this.dao.LoadAllowedRoomPids(settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000217F8 File Offset: 0x0001F9F8
		private List<int> LoadAllowedResourcePids()
		{
			int whoAmI = this.OpContext.WhoAmI;
			IOldUserSettingManager oldUserSettingManager = this.oldUserSettingManager;
			List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(whoAmI, eSettingCode.SETTING_GroupWithResourceForDropList);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(whoAmI, eSettingCode.SETTING_Use_Restrictive_Default_Settings_and_Permissions);
			return this.dao.LoadAllowedResourcePids(settingValue_ConcatenatedIntList, settingValue_Bool);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00021848 File Offset: 0x0001FA48
		private IList<Appointment> LoadAppointmentsPerformanceTest(DateTime startDate, DateTime endDate, IList<int> personIds, out string note)
		{
			PerformanceTestDAO performanceTestDAO = new PerformanceTestDAO(this.OpContext);
			IList<Appointment> result = performanceTestDAO.LoadAppointments(personIds.ToList<int>(), null, false, false, false, startDate, endDate);
			note = "";
			return result;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00021884 File Offset: 0x0001FA84
		public SearchForPersonPerformanceTestResult SearchForPersonPerformanceTest(string searchString)
		{
			DateTime now = DateTime.Now;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num;
			string notes;
			IList<UserGroupObject> foundPersons = this.FindUserGroupObjectBySearchString(searchString, new eUserGroupObjectType[0], 0, 200, out num, out notes);
			stopwatch.Stop();
			return new SearchForPersonPerformanceTestResult
			{
				FoundPersons = foundPersons,
				TestResult = new PerformanceTestResult
				{
					ManagerTimeTaken = new PerformanceTestTimeTaken
					{
						TimeElapsed = stopwatch.Elapsed,
						EntryPoint = now
					},
					Notes = notes
				}
			};
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00021910 File Offset: 0x0001FB10
		public PerformanceTestResult LoadAppointmentsPerformanceTest(DateTime StartDate, DateTime EndDate, IList<int> PersonIds, out IList<Appointment> apps)
		{
			DateTime now = DateTime.Now;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			string notes;
			apps = this.LoadAppointmentsPerformanceTest(StartDate, EndDate, PersonIds, out notes);
			stopwatch.Stop();
			return new PerformanceTestResult
			{
				ManagerTimeTaken = new PerformanceTestTimeTaken
				{
					TimeElapsed = stopwatch.Elapsed,
					EntryPoint = now
				},
				Notes = notes
			};
		}

		// Token: 0x0400011F RID: 287
		private IOldUserSettingManager osm;
	}
}
