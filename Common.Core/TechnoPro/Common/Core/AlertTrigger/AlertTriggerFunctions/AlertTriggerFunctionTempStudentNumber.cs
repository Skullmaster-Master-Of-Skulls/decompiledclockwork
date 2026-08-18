using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions
{
	// Token: 0x0200016B RID: 363
	public class AlertTriggerFunctionTempStudentNumber : IAlertTriggerFunction
	{
		// Token: 0x0600102F RID: 4143 RVA: 0x00076CFC File Offset: 0x00074EFC
		public AlertTriggerFunctionTempStudentNumber(AlertTriggerHelperManagers managers)
		{
			this._managers = managers;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00076D10 File Offset: 0x00074F10
		public IAlertTriggerDefinition ConvertAlertTriggerDefBaseToAlertTriggerDef(IAlertTriggerDefinitionBase baseTrigger)
		{
			AlertTriggerDefinitionTempStudentNumberBase alertTriggerDefinitionTempStudentNumberBase = baseTrigger as AlertTriggerDefinitionTempStudentNumberBase;
			AlertTriggerDefinitionTempStudentNumber alertTriggerDefinitionTempStudentNumber = (alertTriggerDefinitionTempStudentNumberBase == null) ? null : baseTrigger.Clone<AlertTriggerDefinitionTempStudentNumber>();
			bool flag = alertTriggerDefinitionTempStudentNumber == null;
			IAlertTriggerDefinition result;
			if (flag)
			{
				result = null;
			}
			else
			{
				alertTriggerDefinitionTempStudentNumber.MinNumCharacters = alertTriggerDefinitionTempStudentNumberBase.MinNumCharacters;
				alertTriggerDefinitionTempStudentNumber.MaxNumCharacters = alertTriggerDefinitionTempStudentNumberBase.MaxNumCharacters;
				alertTriggerDefinitionTempStudentNumber.AllowLettersInStudentNumber = alertTriggerDefinitionTempStudentNumberBase.AllowLettersInStudentNumber;
				result = alertTriggerDefinitionTempStudentNumber;
			}
			return result;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00076D6C File Offset: 0x00074F6C
		public AlertTriggerForUser[] CheckForTriggerAlerts(IAlertTriggerDefinition[] triggers, int studentPersonId)
		{
			List<AlertTriggerDefinitionTempStudentNumber> source = (from g in triggers
			select g as AlertTriggerDefinitionTempStudentNumber into h
			where h != null
			select h).ToList<AlertTriggerDefinitionTempStudentNumber>();
			AlertTriggerDefinitionTempStudentNumber alertTriggerDefinitionTempStudentNumber = source.FirstOrDefault<AlertTriggerDefinitionTempStudentNumber>();
			bool flag = alertTriggerDefinitionTempStudentNumber == null;
			AlertTriggerForUser[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string snum = (this._managers.StudentManagementManager.LoadStudentNumber(studentPersonId) ?? "").Trim();
				object obj;
				if (!AlertTriggerFunctionTempStudentNumber.IsStudentNumberValid(snum, alertTriggerDefinitionTempStudentNumber.MinNumCharacters, alertTriggerDefinitionTempStudentNumber.MaxNumCharacters, alertTriggerDefinitionTempStudentNumber.AllowLettersInStudentNumber))
				{
					obj = null;
				}
				else
				{
					(obj = new AlertTriggerForUser[1])[0] = new AlertTriggerForUser
					{
						MessageToUser = "Requires valid student number",
						Args = null,
						DontAllowAppointmentBooking = alertTriggerDefinitionTempStudentNumber.DontAllowAppointmentBooking
					};
				}
				result = obj;
			}
			return result;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00076E54 File Offset: 0x00075054
		private static bool IsStudentNumberValid(string snum, int minNumChars, int maxNumChars, bool allowLetters)
		{
			bool flag = snum.Length < 1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = minNumChars > 0 && snum.Length < minNumChars;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = maxNumChars > 0 && snum.Length > maxNumChars;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = !allowLetters;
						result = (!flag4 || !snum.All(new Func<char, bool>(char.IsDigit)));
					}
				}
			}
			return result;
		}

		// Token: 0x040002E6 RID: 742
		private readonly AlertTriggerHelperManagers _managers;
	}
}
