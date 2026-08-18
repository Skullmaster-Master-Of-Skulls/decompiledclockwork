using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.People;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.People
{
	// Token: 0x020000A6 RID: 166
	public class StaffCommonInfoManager : IStaffCommonInfoManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0002235C File Offset: 0x0002055C
		private DynamicFieldManager dynamicFieldManager
		{
			get
			{
				DynamicFieldManager result;
				if ((result = this._dynamicFieldManager) == null)
				{
					result = (this._dynamicFieldManager = new DynamicFieldManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00022388 File Offset: 0x00020588
		private DynamicDataManager dynamicDataManager
		{
			get
			{
				DynamicDataManager result;
				if ((result = this._dynamicDataManager) == null)
				{
					result = (this._dynamicDataManager = new DynamicDataManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000223B4 File Offset: 0x000205B4
		private IStudentCommonInfoManager studentCommonInfoManager
		{
			get
			{
				IStudentCommonInfoManager result;
				if ((result = this.stm) == null)
				{
					result = (this.stm = new StudentCommonInfoManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000223DF File Offset: 0x000205DF
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000223E7 File Offset: 0x000205E7
		public OperationContext OpContext { get; set; }

		// Token: 0x060005D6 RID: 1494 RVA: 0x000223F0 File Offset: 0x000205F0
		public StaffCommonInfoManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00022404 File Offset: 0x00020604
		private int GetStaffSignatureControlId()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager["staffStoredSignatureCid"];
			bool flag = obj == null;
			int num;
			if (flag)
			{
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
				num = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorSignature_controlid);
				cacheStorageManager["staffStoredSignatureCid"] = num;
			}
			else
			{
				num = (int)obj;
			}
			return num;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00022478 File Offset: 0x00020678
		private void SaveStaffStoredSignature(int StaffPersonId, DynamicData data)
		{
			DynamicDataContext context = new DynamicDataContext
			{
				PrimaryId = StaffPersonId
			};
			this.dynamicDataManager.SaveData(context, new List<DynamicData>
			{
				data
			}, eDynamicFormType.PerStudent);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000224B0 File Offset: 0x000206B0
		public byte[] LoadStaffStoredSignature(int StaffPersonId)
		{
			DynamicData dynamicData = this.LoadStaffStoredSignatureData(StaffPersonId);
			return (dynamicData == null || dynamicData.Value == null || !(dynamicData.Value is byte[])) ? null : ((byte[])dynamicData.Value);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000224F0 File Offset: 0x000206F0
		public DynamicData LoadStaffStoredSignatureData(int StaffPersonId)
		{
			int staffSignatureControlId = this.GetStaffSignatureControlId();
			bool flag = staffSignatureControlId < 1;
			DynamicData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = StaffPersonId
				};
				List<DynamicData> list = this.dynamicDataManager.LoadDataByFields(context, new List<int>
				{
					staffSignatureControlId
				}, eDynamicFormType.PerStudent);
				bool flag2 = list == null || list.Count < 1;
				if (flag2)
				{
					List<DynamicField> list2 = this.dynamicFieldManager.LoadFieldsByControlIds(new List<int>
					{
						staffSignatureControlId
					});
					bool flag3 = list2 != null && list2.Count > 0;
					if (flag3)
					{
						return new DynamicData
						{
							Field = list2[0],
							DataId = 0,
							Value = null
						};
					}
				}
				result = ((list == null || list.Count < 1) ? null : list[0]);
			}
			return result;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000225CC File Offset: 0x000207CC
		public void SaveStaffStoredSignature(int StaffPersonId, byte[] imageBytes)
		{
			DynamicData dynamicData = this.LoadStaffStoredSignatureData(StaffPersonId);
			bool flag = dynamicData == null;
			if (!flag)
			{
				dynamicData.Value = imageBytes;
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = StaffPersonId
				};
				this.dynamicDataManager.SaveData(context, new List<DynamicData>
				{
					dynamicData
				}, eDynamicFormType.PerStudent);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00022620 File Offset: 0x00020820
		public DynamicData LoadAssignedAdvisorSignatureData(int StudentPersonId)
		{
			StudentCommonInfo studentCommonInfo = this.studentCommonInfoManager.LoadStudentCommonInfo(StudentPersonId);
			bool flag = studentCommonInfo == null || studentCommonInfo.AssignedCounsellor == null;
			DynamicData result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.LoadStaffStoredSignatureData(studentCommonInfo.AssignedCounsellor.PersonId);
			}
			return result;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00022668 File Offset: 0x00020868
		public void SaveAssignedAdvisorStoredSignature(int StudentPersonId, DynamicData dataItem)
		{
			StudentCommonInfo studentCommonInfo = this.studentCommonInfoManager.LoadStudentCommonInfo(StudentPersonId);
			bool flag = studentCommonInfo == null || studentCommonInfo.AssignedCounsellor == null;
			if (!flag)
			{
				this.SaveStaffStoredSignature(studentCommonInfo.AssignedCounsellor.PersonId, dataItem);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000226AC File Offset: 0x000208AC
		public void SaveAssignedAdvisorStoredSignatureWithImageBytes(int StudentPersonId, byte[] imageBytes)
		{
			StudentCommonInfo studentCommonInfo = this.studentCommonInfoManager.LoadStudentCommonInfo(StudentPersonId);
			bool flag = studentCommonInfo == null || studentCommonInfo.AssignedCounsellor == null;
			if (!flag)
			{
				this.SaveStaffStoredSignature(studentCommonInfo.AssignedCounsellor.PersonId, imageBytes);
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000226F0 File Offset: 0x000208F0
		public string LoadStaffEmail(int StaffPersonId)
		{
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorEmail_controlid);
			bool flag = settingValue_Int < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				DynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				List<DynamicData> list = dynamicDataManager.LoadDataByFields(new DynamicDataContext
				{
					PrimaryId = StaffPersonId
				}, new List<int>
				{
					settingValue_Int
				}, eDynamicFormType.PerStaff);
				bool flag2 = list == null || list.Count < 1;
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = list[0].Value.ToString().Trim();
				}
			}
			return result;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000227A0 File Offset: 0x000209A0
		public StaffWithCommonInfo LoadStaffWithCommonInfoById(int PersonId)
		{
			IStaffCommonInfoDAO staffCommonInfoDAO = new StaffCommonInfoDAO(this.OpContext);
			return staffCommonInfoDAO.LoadStaffWithCommonInfoById(PersonId);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000227C8 File Offset: 0x000209C8
		public T LoadStaffWithCommonInfoById<T>(int PersonId) where T : StaffWithCommonInfo
		{
			IStaffCommonInfoDAO staffCommonInfoDAO = new StaffCommonInfoDAO(this.OpContext);
			return staffCommonInfoDAO.LoadStaffWithCommonInfoById<T>(PersonId);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000227F0 File Offset: 0x000209F0
		public PersonBase LoadStaffByEmail(string Email)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorEmail_controlid);
			bool flag = settingValue_Int < 1;
			PersonBase result;
			if (flag)
			{
				CWLogger.Logger.Warn("StaffCommonInfoManager.LoadStaffByEmail:Email control id is not defined for user " + this.OpContext.WhoAmI.ToString());
				result = null;
			}
			else
			{
				DynamicField dynamicField = this.dynamicFieldManager.LoadFieldByControlId(settingValue_Int);
				bool flag2 = dynamicField == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					IDynamicDataDAO dynamicDataDAO = new DynamicDataDAO(this.OpContext);
					IList<PersonBase> list = dynamicDataDAO.LoadStudentByDataItem(eDynamicFormType.PerStudent, dynamicField, Email);
					bool flag3 = list == null || list.Count < 1;
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = list[0];
					}
				}
			}
			return result;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000228B8 File Offset: 0x00020AB8
		public void UpdateCommonInfo(int PersonId, StaffCommonInfo CommonInfo, bool JustUpdateEmailAndPhone)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int emailCid = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorEmail_controlid);
			int phoneCid = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorPhone_controlid);
			int titleCid = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_CounsellorTitle_controlid);
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			DynamicDataContext context = new DynamicDataContext
			{
				PrimaryId = PersonId
			};
			List<int> list = new List<int>();
			list.Add(emailCid);
			list.Add(phoneCid);
			bool flag = !JustUpdateEmailAndPhone;
			if (flag)
			{
				list.Add(titleCid);
			}
			list = (from g in list
			where g > 0
			select g).ToList<int>();
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			List<DynamicField> source = dynamicFieldManager.LoadFieldsByControlIds(list);
			List<DynamicData> list2 = new List<DynamicData>();
			bool flag2 = list.Contains(emailCid);
			if (flag2)
			{
				list2.Add(new DynamicData
				{
					Field = source.FirstOrDefault((DynamicField g) => g.ControlId == emailCid),
					Value = (CommonInfo.Email ?? "")
				});
			}
			bool flag3 = list.Contains(phoneCid);
			if (flag3)
			{
				list2.Add(new DynamicData
				{
					Field = source.FirstOrDefault((DynamicField g) => g.ControlId == phoneCid),
					Value = (CommonInfo.Phone ?? "")
				});
			}
			bool flag4 = list.Contains(titleCid);
			if (flag4)
			{
				list2.Add(new DynamicData
				{
					Field = source.FirstOrDefault((DynamicField g) => g.ControlId == titleCid),
					Value = (CommonInfo.Title ?? "")
				});
			}
			bool flag5 = list2.Count > 0;
			if (flag5)
			{
				dynamicDataManager.SaveData(context, list2, eDynamicFormType.PerStudent);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00022AD8 File Offset: 0x00020CD8
		public IList<T> LoadStaffWithCommonInfoByGroupTitle<T>(params string[] GroupTitles) where T : StaffWithCommonInfo
		{
			IStaffCommonInfoDAO staffCommonInfoDAO = new StaffCommonInfoDAO(this.OpContext);
			return staffCommonInfoDAO.LoadStaffWithCommonInfoByGroupTitle<T>(GroupTitles);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00022B00 File Offset: 0x00020D00
		private string ValidateStaffWithCommonInfo(StaffWithCommonInfo staffWithCommonInfo)
		{
			bool flag = staffWithCommonInfo.Staff == null;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException(".Staff is null");
			}
			bool flag2 = staffWithCommonInfo.Staff.Student_no == null || staffWithCommonInfo.Staff.Student_no.Trim().Length < 1;
			if (flag2)
			{
				throw new NullOrInvalidIdParameterException(".Staff.Student_no is null or empty");
			}
			bool flag3 = staffWithCommonInfo.Staff.LastName == null || staffWithCommonInfo.Staff.LastName.Trim().Length < 1;
			if (flag3)
			{
				throw new NullOrInvalidIdParameterException(".Staff.LastName is null or empty");
			}
			bool flag4 = staffWithCommonInfo.StaffCommonInfo == null;
			if (flag4)
			{
				staffWithCommonInfo.StaffCommonInfo = new StaffCommonInfo();
			}
			return null;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00022BB4 File Offset: 0x00020DB4
		public int CreateStaffWithCommonInfo(StaffWithCommonInfo staffWithCommonInfo, params string[] addToFirstGroupTitleInThisList)
		{
			string text = this.ValidateStaffWithCommonInfo(staffWithCommonInfo);
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("CreateStaffWithCommonInfo:CreateStaff:" + text);
			}
			IGroupManager groupManager = new GroupManager(this.OpContext);
			int item = groupManager.TryToLoadGroupOrCreateFirstIfNoneFound(addToFirstGroupTitleInThisList);
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			int result = peopleManager.CreateUser(staffWithCommonInfo.Staff, new List<int>
			{
				item
			});
			this.UpdateCommonInfo(staffWithCommonInfo.Staff.PersonId, staffWithCommonInfo.StaffCommonInfo, false);
			return result;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00022C48 File Offset: 0x00020E48
		public void UpdateStaffWithCommonInfo(StaffWithCommonInfo staffWithCommonInfo, bool justUpdateEmailAndPhone)
		{
			string text = this.ValidateStaffWithCommonInfo(staffWithCommonInfo);
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				throw new NullOrInvalidIdParameterException("CreateStaffWithCommonInfo:CreateStaff:" + text);
			}
			bool flag2 = staffWithCommonInfo.Staff.PersonId < 1;
			if (flag2)
			{
				throw new InvalidParameterIdException("UPdateStaffWithCommonInfo:staff.Personid<1");
			}
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			peopleManager.UpdateUser(staffWithCommonInfo.Staff, false);
			this.UpdateCommonInfo(staffWithCommonInfo.Staff.PersonId, staffWithCommonInfo.StaffCommonInfo, justUpdateEmailAndPhone);
		}

		// Token: 0x04000128 RID: 296
		private DynamicFieldManager _dynamicFieldManager;

		// Token: 0x04000129 RID: 297
		private DynamicDataManager _dynamicDataManager;

		// Token: 0x0400012A RID: 298
		private IStudentCommonInfoManager stm;
	}
}
