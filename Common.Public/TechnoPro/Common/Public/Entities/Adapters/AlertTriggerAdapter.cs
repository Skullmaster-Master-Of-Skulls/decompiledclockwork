using System;
using System.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005B5 RID: 1461
	public static class AlertTriggerAdapter
	{
		// Token: 0x06002F35 RID: 12085 RVA: 0x00033E88 File Offset: 0x00032088
		public static bool GetDontAllowAppointmentBooking(this AlertTriggerForUserSet set)
		{
			bool result;
			if (((set != null) ? set.AlertTriggerGroups : null) != null)
			{
				result = set.AlertTriggerGroups.SelectMany((AlertTriggerForUserGroup item) => item.Triggers).Any((AlertTriggerForUser item2) => item2.DontAllowAppointmentBooking);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x00033EFC File Offset: 0x000320FC
		public static eAlertTriggerType GetAlertTriggerType(this IAlertTriggerDefinition alertTriggerDefinition)
		{
			bool flag = alertTriggerDefinition == null;
			eAlertTriggerType result;
			if (flag)
			{
				result = eAlertTriggerType.Unknown;
			}
			else
			{
				AlertTriggerDefinition alertTriggerDefinition2 = alertTriggerDefinition as AlertTriggerDefinition;
				bool flag2 = alertTriggerDefinition2 != null;
				if (flag2)
				{
					result = alertTriggerDefinition2.GetAlertTriggerType();
				}
				else
				{
					AlertTriggerDefinitionBase alertTriggerDefinitionBase = alertTriggerDefinition as AlertTriggerDefinitionBase;
					bool flag3 = alertTriggerDefinitionBase != null;
					if (flag3)
					{
						result = alertTriggerDefinitionBase.GetAlertTriggerType();
					}
					else
					{
						result = eAlertTriggerType.Unknown;
					}
				}
			}
			return result;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00033F50 File Offset: 0x00032150
		public static eAlertTriggerType GetAlertTriggerType(this IAlertTriggerDefinitionBase alertTriggerDefinitionBase)
		{
			bool flag = alertTriggerDefinitionBase == null;
			eAlertTriggerType result;
			if (flag)
			{
				result = eAlertTriggerType.Unknown;
			}
			else
			{
				AlertTriggerDefinition alertTriggerDefinition = alertTriggerDefinitionBase as AlertTriggerDefinition;
				bool flag2 = alertTriggerDefinition != null;
				if (flag2)
				{
					result = alertTriggerDefinition.GetAlertTriggerType();
				}
				else
				{
					AlertTriggerDefinitionBase alertTriggerDefinitionBase2 = alertTriggerDefinitionBase as AlertTriggerDefinitionBase;
					bool flag3 = alertTriggerDefinitionBase2 != null;
					if (flag3)
					{
						result = alertTriggerDefinitionBase2.GetAlertTriggerType();
					}
					else
					{
						result = eAlertTriggerType.Unknown;
					}
				}
			}
			return result;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x00033FA4 File Offset: 0x000321A4
		public static eAlertTriggerType GetAlertTriggerType(this AlertTriggerDefinition alertTriggerDefinition)
		{
			eAlertTriggerType? eAlertTriggerType;
			if (alertTriggerDefinition == null)
			{
				eAlertTriggerType = null;
			}
			else
			{
				AlertDefAttribute alertTriggerAttribute = alertTriggerDefinition.GetAlertTriggerAttribute();
				eAlertTriggerType = ((alertTriggerAttribute != null) ? new eAlertTriggerType?(alertTriggerAttribute.TriggerType) : null);
			}
			eAlertTriggerType? eAlertTriggerType2 = eAlertTriggerType;
			return eAlertTriggerType2.GetValueOrDefault();
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x00033FEC File Offset: 0x000321EC
		public static eAlertTriggerType GetAlertTriggerType(this AlertTriggerDefinitionBase alertTriggerDefinitionBase)
		{
			eAlertTriggerType? eAlertTriggerType;
			if (alertTriggerDefinitionBase == null)
			{
				eAlertTriggerType = null;
			}
			else
			{
				AlertDefAttribute alertTriggerAttribute = alertTriggerDefinitionBase.GetAlertTriggerAttribute();
				eAlertTriggerType = ((alertTriggerAttribute != null) ? new eAlertTriggerType?(alertTriggerAttribute.TriggerType) : null);
			}
			eAlertTriggerType? eAlertTriggerType2 = eAlertTriggerType;
			return eAlertTriggerType2.GetValueOrDefault();
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00034034 File Offset: 0x00032234
		public static string GetCode(this IAlertTriggerDefinition alertTriggerDefinition)
		{
			string result;
			if (alertTriggerDefinition == null)
			{
				result = null;
			}
			else
			{
				AlertDefAttribute alertTriggerAttribute = alertTriggerDefinition.GetAlertTriggerAttribute();
				result = ((alertTriggerAttribute != null) ? alertTriggerAttribute.Code : null);
			}
			return result;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x00034060 File Offset: 0x00032260
		public static string GetCode(this AlertTriggerDefinition alertTriggerDefinition)
		{
			string result;
			if (alertTriggerDefinition == null)
			{
				result = null;
			}
			else
			{
				AlertDefAttribute alertTriggerAttribute = alertTriggerDefinition.GetAlertTriggerAttribute();
				result = ((alertTriggerAttribute != null) ? alertTriggerAttribute.Code : null);
			}
			return result;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0003408C File Offset: 0x0003228C
		public static string GetCode(this AlertTriggerDefinitionBase alertTriggerDefinitionBase)
		{
			string result;
			if (alertTriggerDefinitionBase == null)
			{
				result = null;
			}
			else
			{
				AlertDefAttribute alertTriggerAttribute = alertTriggerDefinitionBase.GetAlertTriggerAttribute();
				result = ((alertTriggerAttribute != null) ? alertTriggerAttribute.Code : null);
			}
			return result;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000340B8 File Offset: 0x000322B8
		public static string GetCode(this Type alertTriggerDefType)
		{
			AlertDefAttribute alertTriggerAttribute = alertTriggerDefType.GetAlertTriggerAttribute();
			return (alertTriggerAttribute != null) ? alertTriggerAttribute.Code : null;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000340E0 File Offset: 0x000322E0
		public static AlertDefAttribute GetAlertTriggerDefAttribute(this eAlertTriggerType triggerType)
		{
			AlertTriggerTypeAttribute attribute = triggerType.GetAttribute<AlertTriggerTypeAttribute>();
			return (attribute != null) ? attribute.DefinitionBaseType.GetAlertTriggerAttribute() : null;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x00034110 File Offset: 0x00032310
		public static AlertDefAttribute GetAlertTriggerAttribute(this Type type)
		{
			bool flag = type == null;
			AlertDefAttribute result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = (AlertDefAttribute)Attribute.GetCustomAttribute(type, typeof(AlertDefAttribute));
			}
			return result;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x00034148 File Offset: 0x00032348
		public static AlertDefAttribute GetAlertTriggerAttribute(this IAlertTriggerDefinition alertTriggerDefinition)
		{
			AlertTriggerDefinition alertTriggerDefinition2 = alertTriggerDefinition as AlertTriggerDefinition;
			return (alertTriggerDefinition2 != null) ? alertTriggerDefinition2.GetAlertTriggerAttribute() : null;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0003416C File Offset: 0x0003236C
		public static AlertDefAttribute GetAlertTriggerAttribute(this IAlertTriggerDefinitionBase alertTriggerDefinitionBase)
		{
			AlertTriggerDefinitionBase alertTriggerDefinitionBase2 = alertTriggerDefinitionBase as AlertTriggerDefinitionBase;
			return (alertTriggerDefinitionBase2 != null) ? alertTriggerDefinitionBase2.GetAlertTriggerAttribute() : null;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x00034190 File Offset: 0x00032390
		public static AlertDefAttribute GetAlertTriggerAttribute(this AlertTriggerDefinition alertTriggerDefinition)
		{
			return (alertTriggerDefinition == null) ? null : ((AlertDefAttribute)Attribute.GetCustomAttribute(alertTriggerDefinition.GetType(), typeof(AlertDefAttribute)));
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000341C4 File Offset: 0x000323C4
		public static AlertDefAttribute GetAlertTriggerAttribute(this AlertTriggerDefinitionBase alertTriggerDefinitionBase)
		{
			return (alertTriggerDefinitionBase == null) ? null : ((AlertDefAttribute)Attribute.GetCustomAttribute(alertTriggerDefinitionBase.GetType(), typeof(AlertDefAttribute)));
		}
	}
}
