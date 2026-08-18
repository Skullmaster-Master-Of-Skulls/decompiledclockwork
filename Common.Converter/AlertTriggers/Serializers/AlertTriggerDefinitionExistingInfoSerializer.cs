using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Converter.AlertTriggers.Serializers
{
	// Token: 0x02000029 RID: 41
	public class AlertTriggerDefinitionExistingInfoSerializer : IAlertTriggerDefinitionSerializer<AlertTriggerDefinitionExistingInfoBase>
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00005F44 File Offset: 0x00004144
		public XElement Serialize(AlertTriggerDefinitionExistingInfoBase dataObj)
		{
			XElement xelement = (dataObj != null) ? dataObj.CreateBaseAlertTriggerElement<AlertTriggerDefinitionExistingInfo>() : null;
			int num = (dataObj != null) ? dataObj.ControlId : 0;
			if (xelement != null)
			{
				xelement.Add(new XAttribute("cid", num));
			}
			return xelement;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005F94 File Offset: 0x00004194
		public AlertTriggerDefinitionExistingInfoBase DeSerialize(XElement element)
		{
			AlertTriggerDefinitionExistingInfoBase alertTriggerDefinitionExistingInfoBase = element.ExtractBaseAlertTriggerDefinition<AlertTriggerDefinitionExistingInfoBase>();
			XAttribute xattribute = element.Attribute("cid");
			XAttribute xattribute2 = element.Attribute("screennum");
			alertTriggerDefinitionExistingInfoBase.SetValues((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0, (xattribute2 != null) ? xattribute2.GetIntFromAttribute(0) : 0);
			return alertTriggerDefinitionExistingInfoBase;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005FF4 File Offset: 0x000041F4
		public AlertTriggerDefinitionExistingInfoBase[] DeSerializeLegacy(string codeStr, string[] parts)
		{
			int[] array;
			if (parts.Length <= 1)
			{
				array = null;
			}
			else
			{
				int num;
				array = (from g in parts.ToList<string>().GetRange(1, parts.Length - 1)
				select g.Trim() into h
				where h.Length > 0
				select h into m
				select int.TryParse(m, out num) ? num : 0 into n
				where n > 0
				select n).ToArray<int>();
			}
			int[] array2 = array;
			string formTypeStr = (parts.Length > 1) ? parts[1].Trim().ToLower() : "";
			AlertTriggerDefinitionExistingInfoBase[] result;
			if (array2 == null || array2.Length == 0)
			{
				result = null;
			}
			else
			{
				result = (from cid in array2
				select new AlertTriggerDefinitionExistingInfoBase(cid, 0)
				{
					IsDisabled = false,
					OrderNum = 0,
					PreferredFormTypeCode = formTypeStr
				} into m
				where m.ControlId > 0
				select m).ToArray<AlertTriggerDefinitionExistingInfoBase>();
			}
			return result;
		}
	}
}
