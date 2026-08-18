using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsPointOfContact;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsPointOfContact;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsPointOfContact;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.AppointmentsPointOfContact
{
	// Token: 0x02000128 RID: 296
	public class PointOfContactManager : IPointOfContactManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000578B0 File Offset: 0x00055AB0
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

		// Token: 0x06000C74 RID: 3188 RVA: 0x000578DB File Offset: 0x00055ADB
		public PointOfContactManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new PointOfContactDAO(opContext);
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000578F9 File Offset: 0x00055AF9
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00057901 File Offset: 0x00055B01
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C77 RID: 3191 RVA: 0x0005790C File Offset: 0x00055B0C
		private int GetPointOfContactAppTypeId()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_UsePointsOfContact);
			bool flag = !settingValue_Bool;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this.OpContext.WhoAmI, eSettingCode.SETTING_PointOfContactAppointmentTypeGroupIds);
				bool flag2 = settingValue_ConcatenatedIntList == null || settingValue_ConcatenatedIntList.Count < 1;
				if (flag2)
				{
					result = 0;
				}
				else
				{
					IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
					AppTypeGroupWithAppTypes appTypeGroupWithAppTypes = appointmentTypeManager.LoadAppTypeGroupWithAppTypesById(settingValue_ConcatenatedIntList[0], true);
					bool flag3 = appTypeGroupWithAppTypes == null || appTypeGroupWithAppTypes.SubAppTypes == null || appTypeGroupWithAppTypes.SubAppTypes.Count < 1;
					if (flag3)
					{
						result = 0;
					}
					else
					{
						AppType appType = appTypeGroupWithAppTypes.SubAppTypes.FirstOrDefault((AppType g) => g.IsActive != null && g.IsActive.Value);
						result = ((appType != null) ? appType.AppTypeId : appTypeGroupWithAppTypes.SubAppTypes[0].AppTypeId);
					}
				}
			}
			return result;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00057A1C File Offset: 0x00055C1C
		private void FindScreenNumAndCidToSavePOCNotesTo(int AppTypeId, out int screenNumToSaveNotesTo, out int rtfTextBoxCidToSaveNotesTo)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			int settingValue_Int = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_DragAndDropEmailsRTBDestinationControlId);
			bool flag = settingValue_Int > 0;
			if (flag)
			{
				IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
				IList<int> list = dynamicFormManager.FindScreensAControlExistsOn(settingValue_Int);
				rtfTextBoxCidToSaveNotesTo = settingValue_Int;
				screenNumToSaveNotesTo = ((list != null && list.Count > 0) ? list[0] : 0);
			}
			bool flag2 = AppTypeId > 0;
			if (flag2)
			{
				IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
				IList<int> appointmentTypeAssociatedPerAppScreenNums = appointmentTypeManager.GetAppointmentTypeAssociatedPerAppScreenNums(AppTypeId);
				bool flag3 = appointmentTypeAssociatedPerAppScreenNums != null;
				if (flag3)
				{
					foreach (int num in appointmentTypeAssociatedPerAppScreenNums)
					{
						int rtfTextBoxCidToSaveNotesTo2 = this.GetRtfTextBoxCidToSaveNotesTo(num, "NotesEmails");
						bool flag4 = rtfTextBoxCidToSaveNotesTo2 <= 0;
						if (!flag4)
						{
							screenNumToSaveNotesTo = num;
							rtfTextBoxCidToSaveNotesTo = rtfTextBoxCidToSaveNotesTo2;
							return;
						}
					}
					foreach (int num2 in appointmentTypeAssociatedPerAppScreenNums)
					{
						int rtfTextBoxCidToSaveNotesTo3 = this.GetRtfTextBoxCidToSaveNotesTo(num2, null);
						bool flag5 = rtfTextBoxCidToSaveNotesTo3 <= 0;
						if (!flag5)
						{
							screenNumToSaveNotesTo = num2;
							rtfTextBoxCidToSaveNotesTo = rtfTextBoxCidToSaveNotesTo3;
							return;
						}
					}
				}
			}
			screenNumToSaveNotesTo = 0;
			rtfTextBoxCidToSaveNotesTo = 0;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00057B8C File Offset: 0x00055D8C
		private int GetRtfTextBoxCidToSaveNotesTo(int screenNumToSaveNotesTo, string controlNameToMatch = null)
		{
			bool flag = screenNumToSaveNotesTo < 1;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				List<DynamicField> source = this.dynamicFieldManager.LoadFields(new DynamicForm
				{
					ScreenNum = screenNumToSaveNotesTo
				});
				DynamicField dynamicField;
				if (string.IsNullOrEmpty(controlNameToMatch))
				{
					dynamicField = source.FirstOrDefault((DynamicField g) => g.ControlCode == eControlCode.RtfTextBox);
				}
				else
				{
					dynamicField = source.FirstOrDefault((DynamicField g) => g.ControlCode == eControlCode.RtfTextBox && g.ControlName.Equals(controlNameToMatch, StringComparison.OrdinalIgnoreCase));
				}
				DynamicField dynamicField2 = dynamicField;
				result = ((dynamicField2 != null) ? dynamicField2.ControlId : 0);
			}
			return result;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00057C28 File Offset: 0x00055E28
		private IDynamicDataManager dynamicDataManager
		{
			get
			{
				IDynamicDataManager result;
				if ((result = this._dynamicDataManager) == null)
				{
					result = (this._dynamicDataManager = new DynamicDataManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00057C58 File Offset: 0x00055E58
		private void FixPointOfContactForSaving(PointOfContact poc, int overrideAppTypeId = 0)
		{
			bool flag = poc == null;
			if (!flag)
			{
				bool flag2 = poc.AppType == null || poc.AppType.AppTypeId < 1;
				if (flag2)
				{
					int appTypeId = (overrideAppTypeId > 0) ? overrideAppTypeId : this.GetPointOfContactAppTypeId();
					poc.AppType = new AppType
					{
						AppTypeId = appTypeId
					};
				}
				bool flag3 = poc.SessionNotesData == null;
				if (flag3)
				{
					poc.SessionNotesData = new List<DynamicData>();
				}
				bool flag4 = poc.SessionNotesData.Count >= 1 || string.IsNullOrEmpty(poc.Memo) || poc.AppType == null;
				if (!flag4)
				{
					int num;
					int num2;
					this.FindScreenNumAndCidToSavePOCNotesTo(poc.AppType.AppTypeId, out num, out num2);
					bool flag5 = num2 <= 0;
					if (!flag5)
					{
						IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
						DynamicField dynamicField = dynamicFieldManager.LoadFieldByControlId(num2);
						bool flag6 = dynamicField == null;
						if (!flag6)
						{
							poc.SessionNotesData.Add(new DynamicData
							{
								Field = dynamicField,
								Value = poc.Memo
							});
							poc.Memo = "";
						}
					}
				}
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00057D7C File Offset: 0x00055F7C
		private IAppointmentLogDAO appLogDao
		{
			get
			{
				IAppointmentLogDAO result;
				if ((result = this._appLogDao) == null)
				{
					result = (this._appLogDao = new AppointmentLogDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00057DA8 File Offset: 0x00055FA8
		public PointOfContact LoadPointOfContactById(int AppointmentId)
		{
			IAppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			List<int> list = appointmentTypeManager.GetAllowedAppTypeIds(this.OpContext.WhoAmI).ToList<int>();
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			PointOfContact pointOfContact = baseAppointmentManager.LoadBaseExtendedAppointmentById<PointOfContact>(AppointmentId);
			bool flag = pointOfContact == null;
			PointOfContact result;
			if (flag)
			{
				result = null;
			}
			else
			{
				pointOfContact.Student = pointOfContact.Attendees.Find((Attendee f) => f.Person.CoreGroup == eCoreGroup.Students);
				pointOfContact.Staff = pointOfContact.Attendees.Find((Attendee f) => f.Person.CoreGroup == eCoreGroup.Staff);
				AppType appType = pointOfContact.AppType;
				int num = (appType != null) ? appType.AppTypeId : 0;
				bool flag2 = num > 0 && list.Contains(num);
				bool flag3 = !flag2;
				if (flag3)
				{
					result = null;
				}
				else
				{
					AppTypeWithExtendedInfo appTypeWithExtendedInfo = appointmentTypeManager.LoadAppTypeWithExtendedInfoIdById(num);
					int? num2;
					if (appTypeWithExtendedInfo == null)
					{
						num2 = null;
					}
					else
					{
						IList<int> perAppScreenNumsForTabs = appTypeWithExtendedInfo.PerAppScreenNumsForTabs;
						if (perAppScreenNumsForTabs == null)
						{
							num2 = null;
						}
						else
						{
							num2 = new int?(perAppScreenNumsForTabs.FirstOrDefault((int g) => g > 0));
						}
					}
					int? num3 = num2;
					int valueOrDefault = num3.GetValueOrDefault();
					bool flag4 = pointOfContact.Student != null && valueOrDefault > 0;
					if (flag4)
					{
						IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
						pointOfContact.SessionNotesData = dynamicDataManager.LoadData(new DynamicDataContext
						{
							PrimaryId = pointOfContact.Student.Person.PersonId,
							SecondaryId = pointOfContact.AppointmentId
						}, new DynamicForm
						{
							ScreenNum = valueOrDefault
						});
					}
					result = pointOfContact;
				}
			}
			return result;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00057F70 File Offset: 0x00056170
		public int CreatePointOfContact(bool runInTransaction, PointOfContact PointOfContact)
		{
			return this.CreatePointOfContact(runInTransaction, PointOfContact, 0);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00057F8C File Offset: 0x0005618C
		public int CreatePointOfContact(bool runInTransaction, PointOfContact PointOfContact, int overrideAppTypeId)
		{
			this.FixPointOfContactForSaving(PointOfContact, overrideAppTypeId);
			return this.dao.CreatePointOfContact(PointOfContact, 0, 0);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00057FB8 File Offset: 0x000561B8
		public void UpdatePointOfContact(bool runInTransaction, PointOfContact PointOfContact)
		{
			bool flag = !runInTransaction;
			if (flag)
			{
				this.appLogDao.LogAppModificationsPreChangeCommitted(PointOfContact.AppointmentId);
			}
			this.FixPointOfContactForSaving(PointOfContact, 0);
			this.dao.UpdatePointOfContact(PointOfContact);
			bool flag2 = !runInTransaction;
			if (flag2)
			{
				Task.Run(delegate()
				{
					IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
					appointmentLogManager.LogAppModifications(PointOfContact.AppointmentId, eAppointmentModifiedItemType.None);
				});
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00058038 File Offset: 0x00056238
		public int CreatePointOfContactFromMessage(ePointOfContactContext PocContext, int StudentPersonId, string PlainTextMessage)
		{
			PointOfContact pointOfContact = new PointOfContact
			{
				Student = new Attendee
				{
					Person = new PersonBase
					{
						PersonId = StudentPersonId
					}
				},
				StartDateTime = DateTime.Now,
				SessionNotesData = new List<DynamicData>(),
				WhoBooked = new PersonBase
				{
					PersonId = StudentPersonId
				},
				PocContext = PocContext,
				Memo = (PlainTextMessage ?? "").ConvertPlainTextToRtf()
			};
			return this.CreatePointOfContact(true, pointOfContact);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000580C4 File Offset: 0x000562C4
		public int SaveEmailAsPointOfContact(bool runInTransaction, int StudentPersonId, int StaffPersonId, TPMailMessage Email, ePointOfContactContext PocContext)
		{
			return this.SaveEmailAsPointOfContact(runInTransaction, StudentPersonId, StaffPersonId, Email, PocContext, 0);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000580E4 File Offset: 0x000562E4
		public int SaveEmailAsPointOfContact(bool runInTransaction, int StudentPersonId, int StaffPersonId, TPMailMessage Email, ePointOfContactContext PocContext, int overrideAppTypeId)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (TPMailAttachment tpmailAttachment in Email.Attachments)
			{
				bool flag = tpmailAttachment.FileBytes == null || tpmailAttachment.FileBytes.Length == 0;
				if (!flag)
				{
					int num = (tpmailAttachment.FileIdForSavedAttachment > 0) ? tpmailAttachment.FileIdForSavedAttachment : this.dynamicDataManager.UploadDocumentToDatabase(new BinaryFile
					{
						ByteArray = tpmailAttachment.FileBytes,
						FileName = tpmailAttachment.FileNameForDisplay,
						FileSize = tpmailAttachment.FileBytes.Length
					}, 1000);
					bool flag2 = num < 1;
					if (!flag2)
					{
						string key = tpmailAttachment.FileNameForDisplay;
						int num2 = 1;
						while (dictionary.ContainsKey(key) && num2 < 1000)
						{
							key = tpmailAttachment.FileNameForDisplay + num2++.ToString();
						}
						bool flag3 = !dictionary.ContainsKey(key);
						if (flag3)
						{
							dictionary.Add(key, num);
						}
					}
				}
			}
			PointOfContact pointOfContact = Email.ConvertToPointOfContact(this.OpContext.WhoAmI, StudentPersonId, StaffPersonId, dictionary);
			bool flag4 = pointOfContact == null;
			int result;
			if (flag4)
			{
				result = 0;
			}
			else
			{
				pointOfContact.PocContext = PocContext;
				int num3 = this.CreatePointOfContact(true, pointOfContact, overrideAppTypeId);
				result = num3;
			}
			return result;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00058278 File Offset: 0x00056478
		public void DeletePointOfContact(bool runInTransaction, int AppointmentId)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.DeleteAppointment(runInTransaction, AppointmentId);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0005829C File Offset: 0x0005649C
		public IList<AppType> LoadAllowedPOCAppointmentTypes(int PersonId)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			List<int> pocAppTypeGroupIds = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(PersonId, eSettingCode.SETTING_PointOfContactAppointmentTypeGroupIds);
			AppointmentTypeManager appointmentTypeManager = new AppointmentTypeManager(this.OpContext);
			List<AppType> first = appointmentTypeManager.LoadAllAppTypes();
			List<AppType> second = appointmentTypeManager.LoadAllInactiveAppTypes();
			IEnumerable<AppType> source = first.Union(second);
			return (from h in source
			where h.Group != null && pocAppTypeGroupIds.Contains(h.Group.AppointmentTypeGroupId)
			select h).ToList<AppType>();
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00058314 File Offset: 0x00056514
		[DebuggerStepThrough]
		public Task<IList<AppType>> LoadAllowedPOCAppointmentTypesAsync(int PersonId)
		{
			PointOfContactManager.<LoadAllowedPOCAppointmentTypesAsync>d__28 <LoadAllowedPOCAppointmentTypesAsync>d__ = new PointOfContactManager.<LoadAllowedPOCAppointmentTypesAsync>d__28();
			<LoadAllowedPOCAppointmentTypesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<AppType>>.Create();
			<LoadAllowedPOCAppointmentTypesAsync>d__.<>4__this = this;
			<LoadAllowedPOCAppointmentTypesAsync>d__.PersonId = PersonId;
			<LoadAllowedPOCAppointmentTypesAsync>d__.<>1__state = -1;
			<LoadAllowedPOCAppointmentTypesAsync>d__.<>t__builder.Start<PointOfContactManager.<LoadAllowedPOCAppointmentTypesAsync>d__28>(ref <LoadAllowedPOCAppointmentTypesAsync>d__);
			return <LoadAllowedPOCAppointmentTypesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000258 RID: 600
		private DynamicFieldManager _dynamicFieldManager;

		// Token: 0x04000259 RID: 601
		private IPointOfContactDAO dao;

		// Token: 0x0400025B RID: 603
		private IDynamicDataManager _dynamicDataManager;

		// Token: 0x0400025C RID: 604
		private IAppointmentLogDAO _appLogDao;
	}
}
