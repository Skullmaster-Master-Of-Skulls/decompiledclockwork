using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.AlertTrigger;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Core.AlertTrigger.AlertTriggerFunctions
{
	// Token: 0x02000168 RID: 360
	public class AlertTriggerFunctionExpiredAccommodations : IAlertTriggerFunction
	{
		// Token: 0x06001025 RID: 4133 RVA: 0x000764B0 File Offset: 0x000746B0
		public AlertTriggerFunctionExpiredAccommodations(AlertTriggerHelperManagers managers)
		{
			this._managers = managers;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000764C4 File Offset: 0x000746C4
		public IAlertTriggerDefinition ConvertAlertTriggerDefBaseToAlertTriggerDef(IAlertTriggerDefinitionBase baseTrigger)
		{
			AlertTriggerDefinitionExpiredAccommodationsBase alertTriggerDefinitionExpiredAccommodationsBase = baseTrigger as AlertTriggerDefinitionExpiredAccommodationsBase;
			bool flag = alertTriggerDefinitionExpiredAccommodationsBase == null;
			IAlertTriggerDefinition result;
			if (flag)
			{
				result = null;
			}
			else
			{
				AlertTriggerDefinitionExpiredAccommodations alertTriggerDefinitionExpiredAccommodations = baseTrigger.Clone<AlertTriggerDefinitionExpiredAccommodations>();
				alertTriggerDefinitionExpiredAccommodations.NumberOfDaysEarlyToWarn = alertTriggerDefinitionExpiredAccommodationsBase.NumberOfDaysEarlyToWarn;
				alertTriggerDefinitionExpiredAccommodations.ShouldWarnIfExpiryDateIsEmpty = alertTriggerDefinitionExpiredAccommodationsBase.ShouldWarnIfExpiryDateIsEmpty;
				result = alertTriggerDefinitionExpiredAccommodations;
			}
			return result;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0007650C File Offset: 0x0007470C
		public AlertTriggerForUser[] CheckForTriggerAlerts(IAlertTriggerDefinition[] triggers, int studentPersonId)
		{
			List<AlertTriggerDefinitionExpiredAccommodations> source = (from g in triggers
			select g as AlertTriggerDefinitionExpiredAccommodations into h
			where h != null
			select h).ToList<AlertTriggerDefinitionExpiredAccommodations>();
			AlertTriggerDefinitionExpiredAccommodations alertTriggerDefinitionExpiredAccommodations = source.FirstOrDefault<AlertTriggerDefinitionExpiredAccommodations>();
			bool flag = alertTriggerDefinitionExpiredAccommodations == null;
			AlertTriggerForUser[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DateTime? studentAccommodationsExpiryDate = this._managers.AccommodationsManager.GetStudentAccommodationsExpiryDate(studentPersonId);
				bool flag2 = studentAccommodationsExpiryDate == null;
				if (flag2)
				{
					AlertTriggerForUser[] array;
					if (alertTriggerDefinitionExpiredAccommodations.ShouldWarnIfExpiryDateIsEmpty)
					{
						(array = new AlertTriggerForUser[1])[0] = new AlertTriggerForUser
						{
							MessageToUser = "Accommodation expiry date is not filled in",
							Args = new Dictionary<string, string>(),
							DontAllowAppointmentBooking = alertTriggerDefinitionExpiredAccommodations.DontAllowAppointmentBooking
						};
					}
					else
					{
						array = null;
					}
					result = array;
				}
				else
				{
					object obj;
					if (!(studentAccommodationsExpiryDate.Value < DateTime.Now.Date.AddDays((double)(alertTriggerDefinitionExpiredAccommodations.NumberOfDaysEarlyToWarn + 1))))
					{
						obj = null;
					}
					else
					{
						(obj = new AlertTriggerForUser[1])[0] = new AlertTriggerForUser
						{
							MessageToUser = "Accommodation expiry date is not filled in",
							Args = new Dictionary<string, string>(),
							DontAllowAppointmentBooking = alertTriggerDefinitionExpiredAccommodations.DontAllowAppointmentBooking
						};
					}
					result = obj;
				}
			}
			return result;
		}

		// Token: 0x040002E3 RID: 739
		private readonly AlertTriggerHelperManagers _managers;
	}
}
