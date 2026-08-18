using System;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Converter.AlertTriggers.Serializers
{
	// Token: 0x0200002D RID: 45
	public class AlertTriggerDefinitionTempStudentNumberSerializer : IAlertTriggerDefinitionSerializer<AlertTriggerDefinitionTempStudentNumberBase>
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000065B0 File Offset: 0x000047B0
		public XElement Serialize(AlertTriggerDefinitionTempStudentNumberBase dataObj)
		{
			XElement xelement = dataObj.CreateBaseAlertTriggerElement<AlertTriggerDefinitionTempStudentNumber>();
			if (xelement != null)
			{
				xelement.Add(new XAttribute("minnumchars", dataObj.MinNumCharacters.ToString()));
			}
			if (xelement != null)
			{
				xelement.Add(new XAttribute("maxnumchars", dataObj.MaxNumCharacters.ToString()));
			}
			if (xelement != null)
			{
				xelement.Add(new XAttribute("allowletters", dataObj.AllowLettersInStudentNumber.ToString()));
			}
			return xelement;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006648 File Offset: 0x00004848
		public AlertTriggerDefinitionTempStudentNumberBase DeSerialize(XElement element)
		{
			AlertTriggerDefinitionTempStudentNumberBase alertTriggerDefinitionTempStudentNumberBase = element.ExtractBaseAlertTriggerDefinition<AlertTriggerDefinitionTempStudentNumberBase>();
			XAttribute attribute = element.Attribute("minnumchars");
			XAttribute attribute2 = element.Attribute("maxnumchars");
			XAttribute attribute3 = element.Attribute("allowletters");
			alertTriggerDefinitionTempStudentNumberBase.MinNumCharacters = attribute.GetIntFromAttribute(0);
			alertTriggerDefinitionTempStudentNumberBase.MaxNumCharacters = attribute2.GetIntFromAttribute(0);
			alertTriggerDefinitionTempStudentNumberBase.AllowLettersInStudentNumber = attribute3.GetBoolFromAttribute(false);
			return alertTriggerDefinitionTempStudentNumberBase;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000066C4 File Offset: 0x000048C4
		public AlertTriggerDefinitionTempStudentNumberBase[] DeSerializeLegacy(string codeStr, string[] parts)
		{
			return new AlertTriggerDefinitionTempStudentNumberBase[]
			{
				new AlertTriggerDefinitionTempStudentNumberBase()
			};
		}
	}
}
