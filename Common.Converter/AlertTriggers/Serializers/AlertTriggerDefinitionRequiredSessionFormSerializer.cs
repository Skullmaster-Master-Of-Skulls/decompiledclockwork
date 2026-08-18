using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Converter.AlertTriggers.Serializers
{
	// Token: 0x0200002C RID: 44
	public class AlertTriggerDefinitionRequiredSessionFormSerializer : IAlertTriggerDefinitionSerializer<AlertTriggerDefinitionRequiredSessionFormBase>
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00006460 File Offset: 0x00004660
		public XElement Serialize(AlertTriggerDefinitionRequiredSessionFormBase dataObj)
		{
			XElement xelement = (dataObj != null) ? dataObj.CreateBaseAlertTriggerElement<AlertTriggerDefinitionRequiredSessionFormBase>() : null;
			if (xelement != null)
			{
				xelement.Add(new XAttribute("rulename", string.Join(",", new string[]
				{
					((dataObj != null) ? dataObj.RequiredSessionFormRuleName : null) ?? ""
				})));
			}
			return xelement;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000064C4 File Offset: 0x000046C4
		public AlertTriggerDefinitionRequiredSessionFormBase DeSerialize(XElement element)
		{
			AlertTriggerDefinitionRequiredSessionForm alertTriggerDefinitionRequiredSessionForm = element.ExtractBaseAlertTriggerDefinition<AlertTriggerDefinitionRequiredSessionForm>();
			XAttribute xattribute = element.Attribute("rulename");
			alertTriggerDefinitionRequiredSessionForm.SetValues(((xattribute != null) ? xattribute.Value : null) ?? "");
			return alertTriggerDefinitionRequiredSessionForm;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000650C File Offset: 0x0000470C
		public AlertTriggerDefinitionRequiredSessionFormBase[] DeSerializeLegacy(string codeStr, string[] parts)
		{
			string[] array;
			if (parts.Length <= 1)
			{
				array = null;
			}
			else
			{
				array = (from g in parts
				select (g ?? "").ToString().Trim() into m
				where m.Length > 0
				select m).Distinct<string>().ToArray<string>();
			}
			string[] array2 = array;
			AlertTriggerDefinitionRequiredSessionFormBase[] result;
			if (array2 == null || array2.Length == 0)
			{
				result = null;
			}
			else
			{
				result = (from g in array2
				select new AlertTriggerDefinitionRequiredSessionFormBase(g)
				{
					IsDisabled = false,
					OrderNum = 0
				}).ToArray<AlertTriggerDefinitionRequiredSessionFormBase>();
			}
			return result;
		}
	}
}
