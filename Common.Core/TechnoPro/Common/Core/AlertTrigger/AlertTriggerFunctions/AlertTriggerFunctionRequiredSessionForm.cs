using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions
{
	// Token: 0x0200016A RID: 362
	public class AlertTriggerFunctionRequiredSessionForm : IAlertTriggerFunction
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x00076AE9 File Offset: 0x00074CE9
		public AlertTriggerFunctionRequiredSessionForm(AlertTriggerHelperManagers managers)
		{
			this._managers = managers;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00076AFC File Offset: 0x00074CFC
		public IAlertTriggerDefinition ConvertAlertTriggerDefBaseToAlertTriggerDef(IAlertTriggerDefinitionBase baseTrigger)
		{
			AlertTriggerDefinitionRequiredSessionFormBase alertTriggerDefinitionRequiredSessionFormBase = baseTrigger as AlertTriggerDefinitionRequiredSessionFormBase;
			bool flag = alertTriggerDefinitionRequiredSessionFormBase == null;
			IAlertTriggerDefinition result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string requiredSessionFormName = (alertTriggerDefinitionRequiredSessionFormBase.RequiredSessionFormRuleName ?? "").Trim();
				bool flag2 = requiredSessionFormName.Length < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string settingValue = this._managers.WebSettingManager.GetSettingValue<string>(Setting.REQUIREDSESSIONFORM_RequiredFormInfos);
					List<RequiredSessionFormItem> source = (from g in settingValue.RequiredSessionsFormItemFromXml() ?? new RequiredSessionFormItem[0]
					where !g.Disabled
					select g).ToList<RequiredSessionFormItem>();
					RequiredSessionFormItem requiredSessionFormItem = source.FirstOrDefault((RequiredSessionFormItem g) => (g.Name ?? "").Equals(requiredSessionFormName, StringComparison.OrdinalIgnoreCase));
					bool flag3 = requiredSessionFormItem == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						AlertTriggerDefinitionRequiredSessionForm alertTriggerDefinitionRequiredSessionForm = baseTrigger.Clone<AlertTriggerDefinitionRequiredSessionForm>();
						alertTriggerDefinitionRequiredSessionForm.RequiredSessionFormRule = requiredSessionFormItem;
						result = alertTriggerDefinitionRequiredSessionForm;
					}
				}
			}
			return result;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00076BF0 File Offset: 0x00074DF0
		public AlertTriggerForUser[] CheckForTriggerAlerts(IAlertTriggerDefinition[] triggers, int studentPersonId)
		{
			List<AlertTriggerDefinitionRequiredSessionForm> list = (from g in triggers
			select g as AlertTriggerDefinitionRequiredSessionForm into h
			where ((h != null) ? h.RequiredSessionFormRule : null) != null && h.RequiredSessionFormRule.ScreenNum > 0
			select h).ToList<AlertTriggerDefinitionRequiredSessionForm>();
			bool flag = list.Count < 1;
			AlertTriggerForUser[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<AlertTriggerDefinitionRequiredSessionForm, int> source = list.ToDictionary((AlertTriggerDefinitionRequiredSessionForm g) => g, (AlertTriggerDefinitionRequiredSessionForm g) => this._managers.RequiredSessionFormManager.LoadInfoPmIdForCurrentSession(studentPersonId, g.RequiredSessionFormRule.ScreenNum));
				result = source.Where(delegate(KeyValuePair<AlertTriggerDefinitionRequiredSessionForm, int> item)
				{
					KeyValuePair<AlertTriggerDefinitionRequiredSessionForm, int> keyValuePair = item;
					return keyValuePair.Value <= 0;
				}).Select(delegate(KeyValuePair<AlertTriggerDefinitionRequiredSessionForm, int> item)
				{
					AlertTriggerForUser alertTriggerForUser = new AlertTriggerForUser();
					KeyValuePair<AlertTriggerDefinitionRequiredSessionForm, int> keyValuePair = item;
					alertTriggerForUser.MessageToUser = keyValuePair.Key.RequiredSessionFormRuleName + " not filled in for this session";
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					string key = "screennum";
					keyValuePair = item;
					dictionary.Add(key, keyValuePair.Key.RequiredSessionFormRule.ScreenNum.ToString());
					string key2 = "title";
					keyValuePair = item;
					dictionary.Add(key2, keyValuePair.Key.RequiredSessionFormRule.Title ?? "");
					alertTriggerForUser.Args = dictionary;
					keyValuePair = item;
					alertTriggerForUser.DontAllowAppointmentBooking = keyValuePair.Key.DontAllowAppointmentBooking;
					return alertTriggerForUser;
				}).ToArray<AlertTriggerForUser>();
			}
			return result;
		}

		// Token: 0x040002E5 RID: 741
		private readonly AlertTriggerHelperManagers _managers;
	}
}
