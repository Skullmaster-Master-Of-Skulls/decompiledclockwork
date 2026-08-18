using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Converter.AlertTriggers;
using TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.AlertTrigger;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.AlertTrigger
{
	// Token: 0x02000166 RID: 358
	public class AlertTriggerManager : IAlertTriggerManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0007590C File Offset: 0x00073B0C
		public void ClearAlertTriggersForCurrentUser()
		{
			UserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager(this.OpContext.TenantId);
			userDatabaseCacheStorageManager.Remove(this.OpContext.WhoAmI, "AlertTriggers");
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00075942 File Offset: 0x00073B42
		public AlertTriggerManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00075954 File Offset: 0x00073B54
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0007595C File Offset: 0x00073B5C
		public OperationContext OpContext { get; set; }

		// Token: 0x0600101C RID: 4124 RVA: 0x00075968 File Offset: 0x00073B68
		private static IDictionary<eAlertTriggerType, IAlertTriggerFunction> GetTriggerFunctions(AlertTriggerHelperManagers managers, IAlertTriggerDefinition[] triggerDefs)
		{
			bool flag = triggerDefs == null || triggerDefs.Length < 1;
			IDictionary<eAlertTriggerType, IAlertTriggerFunction> result;
			if (flag)
			{
				result = new Dictionary<eAlertTriggerType, IAlertTriggerFunction>();
			}
			else
			{
				Dictionary<eAlertTriggerType, IAlertTriggerFunction> dictionary = new Dictionary<eAlertTriggerType, IAlertTriggerFunction>();
				foreach (IAlertTriggerDefinition alertTriggerDefinition in triggerDefs)
				{
					eAlertTriggerType alertTriggerType = alertTriggerDefinition.GetAlertTriggerType();
					bool flag2 = dictionary.ContainsKey(alertTriggerType);
					if (!flag2)
					{
						AlertDefAttribute alertTriggerAttribute = alertTriggerDefinition.GetType().GetAlertTriggerAttribute();
						string str = (alertTriggerAttribute != null) ? alertTriggerAttribute.AlertTriggerFunctionClassName : null;
						try
						{
							Type type = Type.GetType("TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions." + str);
							IAlertTriggerFunction value = (IAlertTriggerFunction)Activator.CreateInstance(type, new object[]
							{
								managers
							});
							dictionary.Add(alertTriggerType, value);
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Error("AlertTriggerManager:GetTriggerFunctions:Can'tGetAlertTriggerFunction:type={0}:ealerttriggertype={1}", (alertTriggerDefinition != null) ? alertTriggerDefinition.GetType().ToString() : null, alertTriggerType.ToString());
						}
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00075A7C File Offset: 0x00073C7C
		private static IDictionary<eAlertTriggerType, IAlertTriggerFunction> GetTriggerFunctions(AlertTriggerHelperManagers managers, IAlertTriggerDefinitionBase[] triggerDefs)
		{
			bool flag = triggerDefs == null || triggerDefs.Length < 1;
			IDictionary<eAlertTriggerType, IAlertTriggerFunction> result;
			if (flag)
			{
				result = new Dictionary<eAlertTriggerType, IAlertTriggerFunction>();
			}
			else
			{
				Dictionary<eAlertTriggerType, IAlertTriggerFunction> dictionary = new Dictionary<eAlertTriggerType, IAlertTriggerFunction>();
				int i = 0;
				while (i < triggerDefs.Length)
				{
					IAlertTriggerDefinitionBase alertTriggerDefinitionBase = triggerDefs[i];
					eAlertTriggerType key = eAlertTriggerType.Unknown;
					try
					{
						key = alertTriggerDefinitionBase.GetAlertTriggerType();
						bool flag2 = dictionary.ContainsKey(key);
						if (!flag2)
						{
							AlertDefAttribute alertTriggerAttribute = alertTriggerDefinitionBase.GetType().GetAlertTriggerAttribute();
							string str = (alertTriggerAttribute != null) ? alertTriggerAttribute.AlertTriggerFunctionClassName : null;
							Type type = Type.GetType("TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions." + str);
							IAlertTriggerFunction value = (IAlertTriggerFunction)Activator.CreateInstance(type, new object[]
							{
								managers
							});
							dictionary.Add(key, value);
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.Error("AlertTriggerManager:GetTriggerFunctions:Can'tGetAlertTriggerFunction:type={0}:ealerttriggertype={1}:err={2}", (alertTriggerDefinitionBase != null) ? alertTriggerDefinitionBase.GetType().ToString() : null, key.ToString(), ex.ToString());
					}
					IL_E9:
					i++;
					continue;
					goto IL_E9;
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00075B98 File Offset: 0x00073D98
		public IAlertTriggerDefinition[] GetAlertTriggersForCurrentUser()
		{
			AlertTriggerManager.<>c__DisplayClass8_0 CS$<>8__locals1 = new AlertTriggerManager.<>c__DisplayClass8_0();
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			bool settingValue_Bool = oldUserSettingManager.GetSettingValue_Bool(this.OpContext.WhoAmI, eSettingCode.SETTING_AlertsDisabled, false);
			bool flag = settingValue_Bool;
			IAlertTriggerDefinition[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				UserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager(this.OpContext.TenantId);
				IAlertTriggerDefinition[] array = userDatabaseCacheStorageManager[this.OpContext.WhoAmI, "AlertTriggers"] as IAlertTriggerDefinition[];
				bool flag2 = array != null;
				if (flag2)
				{
					result = array;
				}
				else
				{
					string xml = (oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_AlertsCode, false) ?? "").Trim();
					AlertTriggerDefinitionBase[] array2 = xml.DeSerializeAlertTriggers();
					AlertTriggerHelperManagers alertTriggerHelperManagers = new AlertTriggerHelperManagers(this.OpContext);
					AlertTriggerManager.<>c__DisplayClass8_0 CS$<>8__locals2 = CS$<>8__locals1;
					AlertTriggerHelperManagers managers = alertTriggerHelperManagers;
					IAlertTriggerDefinitionBase[] array3 = array2;
					CS$<>8__locals2.functions = AlertTriggerManager.GetTriggerFunctions(managers, array3 ?? new IAlertTriggerDefinitionBase[0]);
					List<IAlertTriggerDefinition> list = (from baseTrigger in array2
					let baseTriggerType = baseTrigger.GetAlertTriggerType()
					where CS$<>8__locals1.functions.ContainsKey(baseTriggerType)
					select CS$<>8__locals1.functions[baseTriggerType].ConvertAlertTriggerDefBaseToAlertTriggerDef(baseTrigger)).ToList<IAlertTriggerDefinition>();
					bool settingValue = alertTriggerHelperManagers.WebSettingManager.GetSettingValue<bool>(Setting.REQUIREDSESSIONFORM_RequiredFormsEnabled);
					bool flag3 = settingValue;
					if (flag3)
					{
						string settingValue2 = alertTriggerHelperManagers.WebSettingManager.GetSettingValue<string>(Setting.REQUIREDSESSIONFORM_RequiredFormInfos);
						RequiredSessionFormItem[] source = settingValue2.RequiredSessionsFormItemFromXml() ?? new RequiredSessionFormItem[0];
						List<RequiredSessionFormItem> list2 = (from g in source
						where !g.Disabled
						select g).ToList<RequiredSessionFormItem>();
						bool flag4 = list2.Count > 0;
						if (flag4)
						{
							List<AlertTriggerDefinitionRequiredSessionForm> collection = (from g in list2
							select new AlertTriggerDefinitionRequiredSessionForm
							{
								OrderNum = 99999999,
								IsDisabled = false,
								RequiredSessionFormRule = g
							}).ToList<AlertTriggerDefinitionRequiredSessionForm>();
							list.AddRange(collection);
							array = list.ToArray();
						}
					}
					userDatabaseCacheStorageManager.Insert(this.OpContext.WhoAmI, "AlertTriggers", array ?? list.ToArray());
					list.Sort(delegate(IAlertTriggerDefinition g1, IAlertTriggerDefinition g2)
					{
						int num = g1.GetAlertTriggerType().CompareTo(g2.GetAlertTriggerType());
						return (num != 0) ? num : g1.OrderNum.CompareTo(g2.OrderNum);
					});
					result = list.ToArray();
				}
			}
			return result;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00075DE8 File Offset: 0x00073FE8
		public AlertTriggerForUserSet CheckForTriggerAlerts(int studentPersonId)
		{
			bool flag = studentPersonId < 1;
			AlertTriggerForUserSet result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IAlertTriggerDefinition[] alertTriggersForCurrentUser = this.GetAlertTriggersForCurrentUser();
				bool flag2 = alertTriggersForCurrentUser == null || alertTriggersForCurrentUser.Length < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					AlertTriggerHelperManagers managers = new AlertTriggerHelperManagers(this.OpContext);
					IDictionary<eAlertTriggerType, IAlertTriggerFunction> triggerFunctions = AlertTriggerManager.GetTriggerFunctions(managers, alertTriggersForCurrentUser ?? new IAlertTriggerDefinition[0]);
					List<AlertTriggerForUserGroup> list = new List<AlertTriggerForUserGroup>();
					int i = 0;
					while (i < alertTriggersForCurrentUser.Length)
					{
						IAlertTriggerDefinition alertTriggerDefinition = alertTriggersForCurrentUser[i];
						eAlertTriggerType alertTriggerType = alertTriggerDefinition.GetAlertTriggerType();
						bool flag3 = !triggerFunctions.ContainsKey(alertTriggerType);
						if (!flag3)
						{
							IAlertTriggerFunction alertTriggerFunction = triggerFunctions[alertTriggerType];
							int j;
							for (j = i + 1; j < alertTriggersForCurrentUser.Length; j++)
							{
								IAlertTriggerDefinition alertTriggerDefinition2 = alertTriggersForCurrentUser[j];
								eAlertTriggerType alertTriggerType2 = alertTriggerDefinition2.GetAlertTriggerType();
								bool flag4 = alertTriggerType2 != alertTriggerType;
								if (flag4)
								{
									break;
								}
							}
							AlertTriggerForUser[] array = alertTriggerFunction.CheckForTriggerAlerts(alertTriggersForCurrentUser.GetRangeByIndices(i, j - 1), studentPersonId);
							i = j;
							bool flag5 = array == null || array.Length < 1;
							if (!flag5)
							{
								list.Add(new AlertTriggerForUserGroup
								{
									TriggerType = alertTriggerType,
									Triggers = array
								});
							}
						}
					}
					result = new AlertTriggerForUserSet
					{
						StudentPersonId = studentPersonId,
						AlertTriggerGroups = list.ToArray()
					};
				}
			}
			return result;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00075F44 File Offset: 0x00074144
		public bool AllowedToBookAppointmentForStudent(int studentPersonId)
		{
			AlertTriggerForUserSet alertTriggerForUserSet = this.CheckForTriggerAlerts(studentPersonId);
			return alertTriggerForUserSet != null && !alertTriggerForUserSet.GetDontAllowAppointmentBooking();
		}
	}
}
