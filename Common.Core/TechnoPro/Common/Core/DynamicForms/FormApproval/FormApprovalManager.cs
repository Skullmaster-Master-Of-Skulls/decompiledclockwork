using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicForms.FormApproval;
using TechnoPro.Common.DAO.Impl.DynamicForms.FormApproval;
using TechnoPro.Common.ICore.DynamicForms.FormApproval;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.DynamicForms.FormApproval
{
	// Token: 0x02000102 RID: 258
	public class FormApprovalManager : IFormApprovalManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0000672B File Offset: 0x0000492B
		public FormApprovalManager()
		{
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00043D98 File Offset: 0x00041F98
		public FormApprovalManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00043DAC File Offset: 0x00041FAC
		public eFormApprovalState LoadFormApprovalStatus(int studentPersonId, int appId, int screenNum)
		{
			IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
			return formApprovalDAO.LoadFormApprovalStatus(studentPersonId, appId, screenNum);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00043DD4 File Offset: 0x00041FD4
		public bool AreAnyFormApprovalScreensEnabledForLoggedInUser()
		{
			IList<FormApprovalPendingItem> list = this.LoadPendingFormApprovalItemsForCurrentUser();
			return ((list != null) ? list.Count : 0) > 0;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00043DFC File Offset: 0x00041FFC
		public IDictionary<int, bool> GetActiveFormApprovalScreenNumsWithAdminStatus(int personId)
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_FormApprovalOptions);
			bool flag = string.IsNullOrEmpty(settingValue_String);
			IDictionary<int, bool> result;
			if (flag)
			{
				result = new Dictionary<int, bool>();
			}
			else
			{
				IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
				IList<int> gids = peopleGroupManager.GetGroupIdsByPersonId(personId);
				IList<FormApprovalOptions> list = settingValue_String.XmlToFormApprovalOptions();
				Func<int, bool> <>9__3;
				List<FormApprovalOptions> source = ((list != null) ? list.Where(delegate(FormApprovalOptions g)
				{
					bool result2;
					if (g.IsEnabled)
					{
						if (g.ExemptGroupIds != null)
						{
							IEnumerable<int> exemptGroupIds = g.ExemptGroupIds;
							Func<int, bool> predicate;
							if ((predicate = <>9__3) == null)
							{
								predicate = (<>9__3 = ((int h) => gids.Contains(h)));
							}
							result2 = !exemptGroupIds.Any(predicate);
						}
						else
						{
							result2 = true;
						}
					}
					else
					{
						result2 = false;
					}
					return result2;
				}).ToList<FormApprovalOptions>() : null) ?? new List<FormApprovalOptions>();
				Func<int, bool> <>9__4;
				result = source.ToDictionary((FormApprovalOptions g) => g.ScreenNum, delegate(FormApprovalOptions g)
				{
					bool result2;
					if (g.SupervisorGroupIds != null)
					{
						IEnumerable<int> supervisorGroupIds = g.SupervisorGroupIds;
						Func<int, bool> predicate;
						if ((predicate = <>9__4) == null)
						{
							predicate = (<>9__4 = ((int h) => gids.Contains(h)));
						}
						result2 = supervisorGroupIds.Any(predicate);
					}
					else
					{
						result2 = false;
					}
					return result2;
				});
			}
			return result;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00043EC8 File Offset: 0x000420C8
		public FormApprovalScreenUserOptions GetFormApprovalScreenUserForLoggedInUserOptions(int screenNum)
		{
			int whoAmI = this.OpContext.WhoAmI;
			bool flag = screenNum < 1;
			FormApprovalScreenUserOptions result;
			if (flag)
			{
				result = this.GetEmptyFormApprovalScreenUserOptions(whoAmI, screenNum);
			}
			else
			{
				OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_FormApprovalOptions);
				bool flag2 = string.IsNullOrEmpty(settingValue_String);
				if (flag2)
				{
					result = this.GetEmptyFormApprovalScreenUserOptions(whoAmI, screenNum);
				}
				else
				{
					IList<FormApprovalOptions> source = settingValue_String.XmlToFormApprovalOptions() ?? new List<FormApprovalOptions>();
					FormApprovalOptions formApprovalOptions = source.FirstOrDefault((FormApprovalOptions g) => g.ScreenNum == screenNum);
					bool flag3 = formApprovalOptions == null || !formApprovalOptions.IsEnabled;
					if (flag3)
					{
						result = this.GetEmptyFormApprovalScreenUserOptions(whoAmI, screenNum);
					}
					else
					{
						IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
						IList<int> gids = peopleGroupManager.GetGroupIdsByPersonId(whoAmI);
						bool flag4 = formApprovalOptions.ExemptGroupIds != null && formApprovalOptions.ExemptGroupIds.Any((int g) => gids.Contains(g));
						if (flag4)
						{
							result = this.GetEmptyFormApprovalScreenUserOptions(whoAmI, screenNum);
						}
						else
						{
							bool isSupervisor = formApprovalOptions.SupervisorGroupIds != null && formApprovalOptions.SupervisorGroupIds.Any((int g) => gids.Contains(g));
							result = new FormApprovalScreenUserOptions
							{
								IsEnabled = true,
								IsSupervisor = isSupervisor,
								PersonId = whoAmI,
								ScreenNum = screenNum
							};
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00044054 File Offset: 0x00042254
		public int GetScreenNumForFormApproval(Guid formApprovalId)
		{
			bool flag = formApprovalId == Guid.Empty;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
				result = formApprovalDAO.GetScreenNumForFormApproval(formApprovalId);
			}
			return result;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0004408C File Offset: 0x0004228C
		public FormApprovalPendingItem LoadPendingFormApprovalItemForCurrentUserByFormApprovalId(Guid formApprovalId)
		{
			bool flag = formApprovalId == Guid.Empty;
			if (flag)
			{
				throw new InvalidParameterException();
			}
			IList<FormApprovalPendingItem> list = this.LoadPendingFormApprovalItemsForCurrentUser();
			return (list != null) ? list.FirstOrDefault((FormApprovalPendingItem g) => g.FormApprovalId == formApprovalId) : null;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000440E4 File Offset: 0x000422E4
		public IList<FormApprovalPendingItem> LoadPendingFormApprovalItemsForCurrentUser()
		{
			OperationContext opContext = this.OpContext;
			int num = (opContext != null) ? opContext.WhoAmI : 0;
			bool flag = num < 1;
			if (flag)
			{
				throw new InvalidParameterException("FormApprovalManager:LoadPendingFormApprovalItemsForCurrentUser:WhoAmI=" + num.ToString());
			}
			IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(this.OpContext);
			IList<int> gids = peopleGroupManager.GetGroupIdsByPersonId(num);
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(num, eSettingCode.SETTING_FormApprovalOptions);
			IList<FormApprovalOptions> list = ((settingValue_String != null) ? settingValue_String.XmlToFormApprovalOptions() : null) ?? new List<FormApprovalOptions>();
			List<FormApprovalOptions> list2 = new List<FormApprovalOptions>();
			Func<int, bool> <>9__3;
			Func<int, bool> <>9__4;
			foreach (FormApprovalOptions formApprovalOptions in list)
			{
				bool flag2 = !formApprovalOptions.IsEnabled;
				if (!flag2)
				{
					int[] array = formApprovalOptions.ExemptGroupIds ?? new int[0];
					int[] array2 = formApprovalOptions.SupervisorGroupIds ?? new int[0];
					IEnumerable<int> source = array2;
					Func<int, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((int g) => gids.Contains(g)));
					}
					bool flag3 = source.Any(predicate);
					if (flag3)
					{
						list2.Add(formApprovalOptions);
					}
					else
					{
						IEnumerable<int> source2 = array;
						Func<int, bool> predicate2;
						if ((predicate2 = <>9__4) == null)
						{
							predicate2 = (<>9__4 = ((int g) => gids.Contains(g)));
						}
						bool flag4 = !source2.Any(predicate2);
						if (flag4)
						{
							list2.Add(formApprovalOptions);
						}
					}
				}
			}
			bool flag5 = list2.Count < 1;
			IList<FormApprovalPendingItem> result;
			if (flag5)
			{
				result = new List<FormApprovalPendingItem>();
			}
			else
			{
				IFormApprovalDAO formApprovalDAO = new FormApprovalDAO(this.OpContext);
				IList<FormApprovalPendingItem> list3 = formApprovalDAO.LoadPendingFormApprovalItemsForUser(num, (from g in list2
				select g.ScreenNum).ToArray<int>());
				Func<int, bool> <>9__5;
				List<int> list4 = (from m in list2.Where(delegate(FormApprovalOptions g)
				{
					bool result2;
					if (g.SupervisorGroupIds != null)
					{
						IEnumerable<int> supervisorGroupIds = g.SupervisorGroupIds;
						Func<int, bool> predicate3;
						if ((predicate3 = <>9__5) == null)
						{
							predicate3 = (<>9__5 = ((int h) => gids.Contains(h)));
						}
						result2 = supervisorGroupIds.Any(predicate3);
					}
					else
					{
						result2 = false;
					}
					return result2;
				})
				select m.ScreenNum).ToList<int>();
				foreach (FormApprovalPendingItem formApprovalPendingItem in list3)
				{
					formApprovalPendingItem.IsCurrentUserSupervisor = list4.Contains(formApprovalPendingItem.ScreenNum);
				}
				result = list3;
			}
			return result;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00044368 File Offset: 0x00042568
		private FormApprovalScreenUserOptions GetEmptyFormApprovalScreenUserOptions(int personId, int screenNum)
		{
			return new FormApprovalScreenUserOptions
			{
				IsEnabled = false,
				IsSupervisor = false,
				PersonId = personId,
				ScreenNum = screenNum
			};
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0004439F File Offset: 0x0004259F
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x000443A7 File Offset: 0x000425A7
		public OperationContext OpContext { get; set; }
	}
}
