using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000F9 RID: 249
	public class ConfidentialityFormSignedManager : IConfidentialityFormSignedManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x0003DD10 File Offset: 0x0003BF10
		public ConfidentialityFormSignedManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0003DD22 File Offset: 0x0003BF22
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x0003DD2A File Offset: 0x0003BF2A
		public OperationContext OpContext { get; set; }

		// Token: 0x060009BD RID: 2493 RVA: 0x0003DD34 File Offset: 0x0003BF34
		public DynamicField GetLastSignedConfidentialityAgreementField(string controlName, string controlCaption)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = controlName + "_LastSigned";
			DynamicField dynamicField = (DynamicField)cacheStorageManager[key];
			bool flag = dynamicField == null;
			if (flag)
			{
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				dynamicField = dynamicFieldManager.LoadFieldByName(controlName);
				bool flag2 = dynamicField == null;
				if (flag2)
				{
					dynamicField = new DynamicField
					{
						ControlName = controlName,
						ControlCaption = controlCaption,
						ControlCode = eControlCode.Date,
						EnforceMethod = eEnforceType.Optional
					};
					dynamicFieldManager.CreateField(dynamicField);
					dynamicField = dynamicFieldManager.LoadFieldByName(controlName);
				}
				bool flag3 = dynamicField != null;
				if (flag3)
				{
					cacheStorageManager.Insert(key, dynamicField);
				}
			}
			return dynamicField;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0003DDE0 File Offset: 0x0003BFE0
		public Range<DateTime> GetConfidentialityResignDateRange(Setting reSignPolicySetting)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			int settingValue = webSettingManager.GetSettingValue<int>(reSignPolicySetting);
			return this.GetConfidentialityResignDateRange(settingValue);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0003DE14 File Offset: 0x0003C014
		private Range<DateTime> GetConfidentialityResignDateRange(int reSignPolicySettingValue)
		{
			Range<DateTime> result;
			switch (reSignPolicySettingValue)
			{
			case 0:
				result = this.GetCurrentYearRange();
				break;
			case 1:
				result = this.GetCurrentTermRange();
				break;
			case 2:
				result = new Range<DateTime>
				{
					Start = DateTime.MinValue,
					End = DateTime.MaxValue
				};
				break;
			case 3:
				result = null;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0003DE7C File Offset: 0x0003C07C
		private Range<DateTime> GetCurrentTermRange()
		{
			ISessionManager sessionManager = new SessionManager();
			Session currentSession = sessionManager.GetCurrentSession();
			return new Range<DateTime>
			{
				Start = currentSession.StartDate,
				End = currentSession.EndDate
			};
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0003DEBC File Offset: 0x0003C0BC
		private Range<DateTime> GetCurrentYearRange()
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(this.OpContext);
			List<AcademicTerm> list = academicTermManager.LoadAcademicTerms(false).ToList<AcademicTerm>();
			list.Sort(delegate(AcademicTerm g1, AcademicTerm g2)
			{
				int month = g1.EndMonthDay.Month;
				int month2 = g2.EndMonthDay.Month;
				bool flag2 = month > month2;
				int result;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					bool flag3 = month < month2;
					if (flag3)
					{
						result = -1;
					}
					else
					{
						int day = g1.EndMonthDay.Day;
						int day2 = g2.EndMonthDay.Day;
						result = day.CompareTo(day2);
					}
				}
				return result;
			});
			AcademicTerm academicTerm = list[list.Count - 1];
			DateTime dateTime = new DateTime(DateTime.Now.Year, academicTerm.StartMonthDay.Month, academicTerm.StartMonthDay.Day);
			bool flag = dateTime > DateTime.Now.Date;
			if (flag)
			{
				dateTime = dateTime.AddYears(-1);
			}
			return new Range<DateTime>
			{
				Start = dateTime,
				End = dateTime.AddYears(1).AddDays(-1.0)
			};
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0003DFA4 File Offset: 0x0003C1A4
		public bool IsConfidentialityAgreementSigningRequired(int pid, Setting reSignPolicySetting, string controlName, string controlCaption)
		{
			ISettingManager currentInstance = SettingManager.CurrentInstance;
			int settingValue = currentInstance.GetSettingValue<int>(reSignPolicySetting);
			Range<DateTime> confidentialityResignDateRange = this.GetConfidentialityResignDateRange(settingValue);
			bool flag = confidentialityResignDateRange == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DynamicField lastSignedConfidentialityAgreementField = this.GetLastSignedConfidentialityAgreementField(controlName, controlCaption);
				int num = (lastSignedConfidentialityAgreementField != null) ? lastSignedConfidentialityAgreementField.ControlId : 0;
				bool flag2 = num < 1;
				if (flag2)
				{
					throw new NullOrInvalidIdParameterException(string.Format("TechnoPro.Common.Core.DynamicForms.IsConfidentialityAgreementSigningRequired:pid={0}:cid={1}:controlName={2}", pid.ToString(), num.ToString(), controlName ?? "NULL"));
				}
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = pid
				};
				List<DynamicData> list = dynamicDataManager.LoadDataByFields(context, new List<int>
				{
					num
				}, eDynamicFormType.PerStudent);
				bool flag3 = list == null || list.Count < 1 || list[0].Value == null || !(list[0].Value is DateTime);
				result = (flag3 || ((DateTime)list[0].Value).Date < confidentialityResignDateRange.Start.Date);
			}
			return result;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		public void RecordConfidentialityAgreementSignedByTutor(int PersonId, string controlName, string controlCaption)
		{
			DynamicField lastSignedConfidentialityAgreementField = this.GetLastSignedConfidentialityAgreementField(controlName, controlCaption);
			bool flag = lastSignedConfidentialityAgreementField == null || lastSignedConfidentialityAgreementField.ControlId < 1;
			if (flag)
			{
				throw new NullOrInvalidIdParameterException(string.Format("TechnoPro.Common.Core.DynamicForms.RecordConfidentialityAgreementSignedByTutor:pid={0}:controlName={1}", PersonId.ToString(), controlName ?? "NULL"));
			}
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			DynamicDataContext context = new DynamicDataContext
			{
				PrimaryId = PersonId
			};
			DynamicDataItemDateChooser item = new DynamicDataItemDateChooser
			{
				Field = lastSignedConfidentialityAgreementField,
				Value = new DynamicDataItemDateValue(new DateTime?(DateTime.Now.Date))
			};
			dynamicDataManager.SaveDynamicDataItems(context, new List<IDynamicDataSerializableItem>
			{
				item
			}, eDynamicFormType.PerStudent);
		}
	}
}
