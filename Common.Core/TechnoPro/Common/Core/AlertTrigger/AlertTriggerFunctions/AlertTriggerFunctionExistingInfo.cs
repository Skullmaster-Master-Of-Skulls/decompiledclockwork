using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions
{
	// Token: 0x02000167 RID: 359
	public class AlertTriggerFunctionExistingInfo : IAlertTriggerFunction
	{
		// Token: 0x06001021 RID: 4129 RVA: 0x00075F6D File Offset: 0x0007416D
		public AlertTriggerFunctionExistingInfo(AlertTriggerHelperManagers managers)
		{
			this._managers = managers;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00075F80 File Offset: 0x00074180
		public IAlertTriggerDefinition ConvertAlertTriggerDefBaseToAlertTriggerDef(IAlertTriggerDefinitionBase baseTrigger)
		{
			AlertTriggerDefinitionExistingInfoBase alertTriggerDefinitionExistingInfoBase = baseTrigger as AlertTriggerDefinitionExistingInfoBase;
			bool flag = alertTriggerDefinitionExistingInfoBase == null;
			IAlertTriggerDefinition result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDynamicFormType preferredFormType = ((alertTriggerDefinitionExistingInfoBase.PreferredFormTypeCode ?? "").Trim().ToLower() == "pa") ? eDynamicFormType.PerAppointment : eDynamicFormType.PerStudent;
				int controlId = alertTriggerDefinitionExistingInfoBase.ControlId;
				int screenNum = alertTriggerDefinitionExistingInfoBase.ScreenNum;
				DynamicField dynamicField = (controlId > 0) ? this._managers.DynamicFieldManager.LoadFieldByControlId(alertTriggerDefinitionExistingInfoBase.ControlId) : null;
				bool flag2 = dynamicField == null;
				if (flag2)
				{
					CWLogger.Logger.Error("ConvertBaseTriggerDefinitionToTriggerDefinition:existingInfo:Can'tFindField:cid={0}", alertTriggerDefinitionExistingInfoBase.ControlId);
					result = null;
				}
				else
				{
					bool flag3 = screenNum < 1;
					DynamicForm dynamicForm;
					if (flag3)
					{
						int[] array = (from g in this._managers.DynamicFormManager.FindScreensAControlExistsOn(alertTriggerDefinitionExistingInfoBase.ControlId) ?? new List<int>()
						where g > 0
						select g).Distinct<int>().ToArray<int>();
						IList<DynamicForm> list2;
						if (array.Length == 0)
						{
							IList<DynamicForm> list = new List<DynamicForm>();
							list2 = list;
						}
						else
						{
							list2 = (this._managers.DynamicFormManager.LoadDynamicFormsByIds(array) ?? new List<DynamicForm>());
						}
						IList<DynamicForm> source = list2;
						dynamicForm = (source.FirstOrDefault((DynamicForm g) => g.FormType == preferredFormType) ?? source.FirstOrDefault<DynamicForm>());
					}
					else
					{
						dynamicForm = this._managers.DynamicFormManager.LoadDynamicFormById(screenNum);
					}
					bool flag4 = dynamicForm == null;
					if (flag4)
					{
						dynamicForm = new DynamicForm
						{
							ScreenNum = 0,
							FormType = preferredFormType
						};
					}
					AlertTriggerDefinitionExistingInfo alertTriggerDefinitionExistingInfo = baseTrigger.Clone<AlertTriggerDefinitionExistingInfo>();
					alertTriggerDefinitionExistingInfo.FieldWithForm = dynamicField.Clone<DynamicFieldWithForm>();
					alertTriggerDefinitionExistingInfo.FieldWithForm.Form = dynamicForm;
					result = alertTriggerDefinitionExistingInfo;
				}
			}
			return result;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00076148 File Offset: 0x00074348
		public AlertTriggerForUser[] CheckForTriggerAlerts(IAlertTriggerDefinition[] triggers, int studentPersonId)
		{
			List<AlertTriggerDefinitionExistingInfo> alertDefs = (from g in triggers
			select g as AlertTriggerDefinitionExistingInfo into h
			where h != null
			select h).ToList<AlertTriggerDefinitionExistingInfo>();
			bool flag = alertDefs.Count < 1;
			AlertTriggerForUser[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<eDynamicFormType, List<AlertTriggerDefinitionExistingInfo>> dictionary = (from g in alertDefs
				group g by g.FieldWithForm.Form.FormType).ToDictionary((IGrouping<eDynamicFormType, AlertTriggerDefinitionExistingInfo> g) => g.Key, (IGrouping<eDynamicFormType, AlertTriggerDefinitionExistingInfo> g) => g.ToList<AlertTriggerDefinitionExistingInfo>());
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = studentPersonId
				};
				List<AlertTriggerForUser> list = new List<AlertTriggerForUser>();
				using (Dictionary<eDynamicFormType, List<AlertTriggerDefinitionExistingInfo>>.Enumerator enumerator = dictionary.GetEnumerator())
				{
					Func<AlertTriggerDefinitionExistingInfo, AlertTriggerForUser> <>9__7;
					while (enumerator.MoveNext())
					{
						KeyValuePair<eDynamicFormType, List<AlertTriggerDefinitionExistingInfo>> kvp = enumerator.Current;
						eDynamicFormType key = kvp.Key;
						List<int> controlIds = (from g in kvp.Value
						select g.FieldWithForm.ControlId into h
						where h > 0
						select h).Distinct<int>().ToList<int>();
						bool flag2 = kvp.Key == eDynamicFormType.PerAppointment;
						IList<AlertTriggerDefinitionExistingInfo> list2;
						if (flag2)
						{
							IList<int> existingCids = this._managers.DynamicDataManager.FindPerAppointmentExistingDataForAnyAppointment(studentPersonId, controlIds);
							list2 = (from g in kvp.Value
							where existingCids.Contains(g.ControlId)
							select g).ToList<AlertTriggerDefinitionExistingInfo>();
						}
						else
						{
							List<DynamicData> source = this._managers.DynamicDataManager.LoadDataByFields(context, controlIds, key) ?? new List<DynamicData>();
							list2 = (from g in source
							select kvp.Value.FirstOrDefault((AlertTriggerDefinitionExistingInfo h) => h.ControlId > 0 && h.ControlId == g.Field.ControlId) into m
							where m != null
							select m).ToList<AlertTriggerDefinitionExistingInfo>();
						}
						List<AlertTriggerForUser> list3 = list;
						IEnumerable<AlertTriggerDefinitionExistingInfo> source2 = list2;
						Func<AlertTriggerDefinitionExistingInfo, AlertTriggerForUser> selector;
						if ((selector = <>9__7) == null)
						{
							selector = (<>9__7 = delegate(AlertTriggerDefinitionExistingInfo g)
							{
								AlertTriggerForUser alertTriggerForUser = new AlertTriggerForUser();
								alertTriggerForUser.MessageToUser = string.Format("Important information ({0})", g.ControlId);
								alertTriggerForUser.Args = AlertTriggerFunctionExistingInfo.SerializeArgs(g.FieldWithForm);
								AlertTriggerDefinitionExistingInfo alertTriggerDefinitionExistingInfo = alertDefs.FirstOrDefault((AlertTriggerDefinitionExistingInfo m) => m.ControlId == g.ControlId);
								alertTriggerForUser.DontAllowAppointmentBooking = (alertTriggerDefinitionExistingInfo != null && alertTriggerDefinitionExistingInfo.DontAllowAppointmentBooking);
								return alertTriggerForUser;
							});
						}
						list3.AddRange(source2.Select(selector).ToList<AlertTriggerForUser>());
					}
				}
				result = ((list.Count < 1) ? null : list.ToArray());
			}
			return result;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00076428 File Offset: 0x00074628
		private static IDictionary<string, string> SerializeArgs(DynamicFieldWithForm fieldWithForm)
		{
			DynamicForm dynamicForm = (fieldWithForm != null) ? fieldWithForm.Form : null;
			return new Dictionary<string, string>
			{
				{
					"formtype",
					((int)((dynamicForm != null) ? dynamicForm.FormType : eDynamicFormType.UnknownLegacy)).ToString()
				},
				{
					"cid",
					((fieldWithForm != null) ? fieldWithForm.ControlId : 0).ToString()
				},
				{
					"screennum",
					((dynamicForm != null) ? dynamicForm.ScreenNum : 0).ToString()
				}
			};
		}

		// Token: 0x040002E2 RID: 738
		private readonly AlertTriggerHelperManagers _managers;
	}
}
