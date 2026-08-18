using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions
{
	// Token: 0x02000169 RID: 361
	public class AlertTriggerFunctionMissingInfo : IAlertTriggerFunction
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x0007664E File Offset: 0x0007484E
		public AlertTriggerFunctionMissingInfo(AlertTriggerHelperManagers managers)
		{
			this._managers = managers;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00076660 File Offset: 0x00074860
		public IAlertTriggerDefinition ConvertAlertTriggerDefBaseToAlertTriggerDef(IAlertTriggerDefinitionBase baseTrigger)
		{
			AlertTriggerDefinitionMissingInfoBase alertTriggerDefinitionMissingInfoBase = baseTrigger as AlertTriggerDefinitionMissingInfoBase;
			bool flag = alertTriggerDefinitionMissingInfoBase == null;
			IAlertTriggerDefinition result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AlertTriggerDefinitionMissingInfo alertTriggerDefinitionMissingInfo = baseTrigger.Clone<AlertTriggerDefinitionMissingInfo>();
				DynamicField dynamicField = (alertTriggerDefinitionMissingInfoBase.ControlId > 0) ? this._managers.DynamicFieldManager.LoadFieldByControlId(alertTriggerDefinitionMissingInfoBase.ControlId) : null;
				DynamicForm dynamicForm = (alertTriggerDefinitionMissingInfoBase.ScreenNum > 0) ? this._managers.DynamicFormManager.LoadDynamicFormById(alertTriggerDefinitionMissingInfoBase.ScreenNum) : null;
				alertTriggerDefinitionMissingInfo.FieldWithForm = ((dynamicField != null) ? dynamicField.Clone<DynamicFieldWithForm>() : null);
				bool flag2 = alertTriggerDefinitionMissingInfo.FieldWithForm == null;
				if (flag2)
				{
					result = alertTriggerDefinitionMissingInfo;
				}
				else
				{
					bool flag3 = dynamicForm == null && alertTriggerDefinitionMissingInfo.FieldWithForm != null && alertTriggerDefinitionMissingInfo.FieldWithForm.ControlId > 0;
					if (flag3)
					{
						IList<int> list = this._managers.DynamicFormManager.FindScreensAControlExistsOn(alertTriggerDefinitionMissingInfo.FieldWithForm.ControlId);
						dynamicForm = ((list.Count > 0) ? this._managers.DynamicFormManager.LoadDynamicFormById(list[0]) : null);
					}
					alertTriggerDefinitionMissingInfo.FieldWithForm.Form = dynamicForm;
					result = alertTriggerDefinitionMissingInfo;
				}
			}
			return result;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00076778 File Offset: 0x00074978
		public AlertTriggerForUser[] CheckForTriggerAlerts(IAlertTriggerDefinition[] triggers, int studentPersonId)
		{
			AlertTriggerFunctionMissingInfo.<>c__DisplayClass3_0 CS$<>8__locals1 = new AlertTriggerFunctionMissingInfo.<>c__DisplayClass3_0();
			CS$<>8__locals1.alertDefs = (from g in triggers
			select g as AlertTriggerDefinitionMissingInfo into h
			where h != null
			select h).ToList<AlertTriggerDefinitionMissingInfo>();
			bool flag = CS$<>8__locals1.alertDefs.Count < 1;
			AlertTriggerForUser[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<eDynamicFormType, List<AlertTriggerDefinitionMissingInfo>> dictionary = (from g in CS$<>8__locals1.alertDefs
				group g by g.FieldWithForm.Form.FormType).ToDictionary((IGrouping<eDynamicFormType, AlertTriggerDefinitionMissingInfo> g) => g.Key, (IGrouping<eDynamicFormType, AlertTriggerDefinitionMissingInfo> g) => g.ToList<AlertTriggerDefinitionMissingInfo>());
				DynamicDataContext context = new DynamicDataContext
				{
					PrimaryId = studentPersonId
				};
				List<AlertTriggerForUser> list = new List<AlertTriggerForUser>();
				using (Dictionary<eDynamicFormType, List<AlertTriggerDefinitionMissingInfo>>.Enumerator enumerator = dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AlertTriggerFunctionMissingInfo.<>c__DisplayClass3_1 CS$<>8__locals2 = new AlertTriggerFunctionMissingInfo.<>c__DisplayClass3_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.kvp = enumerator.Current;
						eDynamicFormType formType = CS$<>8__locals2.kvp.Key;
						List<int> list2 = (from g in CS$<>8__locals2.kvp.Value
						select g.FieldWithForm.ControlId into h
						where h > 0
						select h).Distinct<int>().ToList<int>();
						List<DynamicData> source = this._managers.DynamicDataManager.LoadDataByFields(context, list2, formType) ?? new List<DynamicData>();
						List<AlertTriggerDefinitionMissingInfo> existingData = (from g in source
						select CS$<>8__locals2.kvp.Value.FirstOrDefault((AlertTriggerDefinitionMissingInfo h) => h.ControlId > 0 && h.ControlId == g.Field.ControlId) into m
						where m != null
						select m).ToList<AlertTriggerDefinitionMissingInfo>();
						List<int> source2 = (from m in (from g in list2
						where existingData.All((AlertTriggerDefinitionMissingInfo h) => h.ControlId != g)
						select g).Distinct<int>()
						where m > 0
						select m).ToList<int>();
						list.AddRange(source2.Select(delegate(int g)
						{
							AlertTriggerForUser alertTriggerForUser = new AlertTriggerForUser();
							alertTriggerForUser.MessageToUser = string.Format("Important information ({0})", g);
							int g2 = g;
							eDynamicFormType formType = formType;
							AlertTriggerDefinitionMissingInfo alertTriggerDefinitionMissingInfo = CS$<>8__locals2.kvp.Value.FirstOrDefault((AlertTriggerDefinitionMissingInfo m) => m.ControlId == g);
							alertTriggerForUser.Args = AlertTriggerFunctionMissingInfo.SerializeArgs(g2, formType, (alertTriggerDefinitionMissingInfo != null) ? alertTriggerDefinitionMissingInfo.FieldWithForm : null);
							AlertTriggerDefinitionMissingInfo alertTriggerDefinitionMissingInfo2 = CS$<>8__locals2.CS$<>8__locals1.alertDefs.FirstOrDefault((AlertTriggerDefinitionMissingInfo m) => m.ControlId == g);
							alertTriggerForUser.DontAllowAppointmentBooking = (alertTriggerDefinitionMissingInfo2 != null && alertTriggerDefinitionMissingInfo2.DontAllowAppointmentBooking);
							return alertTriggerForUser;
						}).ToList<AlertTriggerForUser>());
					}
				}
				result = ((list.Count < 1) ? null : list.ToArray());
			}
			return result;
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00076A5C File Offset: 0x00074C5C
		private static IDictionary<string, string> SerializeArgs(int cid, eDynamicFormType formType, DynamicFieldWithForm fieldWithForm)
		{
			DynamicForm dynamicForm = (fieldWithForm != null) ? fieldWithForm.Form : null;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("cid", cid.ToString());
			string key = "formtype";
			int num = (int)formType;
			dictionary.Add(key, num.ToString());
			dictionary.Add("screennum", ((dynamicForm != null) ? dynamicForm.ScreenNum : 0).ToString());
			dictionary.Add("controlcaption", ((fieldWithForm != null) ? fieldWithForm.ControlCaption : null) ?? "");
			return dictionary;
		}

		// Token: 0x040002E4 RID: 740
		private readonly AlertTriggerHelperManagers _managers;
	}
}
