using System;
using System.Xml.Linq;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Converter.AlertTriggers.Serializers
{
	// Token: 0x0200002A RID: 42
	public class AlertTriggerDefinitionExpiredAccommodationsSerializer : IAlertTriggerDefinitionSerializer<AlertTriggerDefinitionExpiredAccommodationsBase>
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00006114 File Offset: 0x00004314
		public XElement Serialize(AlertTriggerDefinitionExpiredAccommodationsBase dataObj)
		{
			XElement xelement = (dataObj != null) ? dataObj.CreateBaseAlertTriggerElement<AlertTriggerDefinitionExpiredAccommodations>() : null;
			if (xelement != null)
			{
				xelement.Add(new XAttribute("numdays", dataObj.NumberOfDaysEarlyToWarn));
			}
			if (xelement != null)
			{
				xelement.Add(new XAttribute("emptywarningenabled", dataObj.ShouldWarnIfExpiryDateIsEmpty.ToString()));
			}
			return xelement;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006184 File Offset: 0x00004384
		public AlertTriggerDefinitionExpiredAccommodationsBase DeSerialize(XElement element)
		{
			AlertTriggerDefinitionExpiredAccommodations alertTriggerDefinitionExpiredAccommodations = element.ExtractBaseAlertTriggerDefinition<AlertTriggerDefinitionExpiredAccommodations>();
			XAttribute xattribute = element.Attribute("numdays");
			XAttribute xattribute2 = element.Attribute("emptywarningenabled");
			string text = (((xattribute != null) ? xattribute.Value : null) ?? "").Trim();
			int numberOfDaysEarlyToWarn;
			bool flag = text.Length < 1 || !int.TryParse(text, out numberOfDaysEarlyToWarn);
			if (flag)
			{
				numberOfDaysEarlyToWarn = 0;
			}
			alertTriggerDefinitionExpiredAccommodations.NumberOfDaysEarlyToWarn = numberOfDaysEarlyToWarn;
			alertTriggerDefinitionExpiredAccommodations.ShouldWarnIfExpiryDateIsEmpty = (xattribute2 != null && xattribute2.GetBoolFromAttribute(false));
			return alertTriggerDefinitionExpiredAccommodations;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000621C File Offset: 0x0000441C
		public AlertTriggerDefinitionExpiredAccommodationsBase[] DeSerializeLegacy(string codeStr, string[] parts)
		{
			int numberOfDaysEarlyToWarn = (parts.Length > 1) ? parts[0].Trim().ConvertStringToInt(0) : 0;
			return new AlertTriggerDefinitionExpiredAccommodationsBase[]
			{
				new AlertTriggerDefinitionExpiredAccommodationsBase
				{
					NumberOfDaysEarlyToWarn = numberOfDaysEarlyToWarn,
					ShouldWarnIfExpiryDateIsEmpty = (parts.Length > 2 && parts[1].Trim().ConvertStringToBool(false))
				}
			};
		}
	}
}
