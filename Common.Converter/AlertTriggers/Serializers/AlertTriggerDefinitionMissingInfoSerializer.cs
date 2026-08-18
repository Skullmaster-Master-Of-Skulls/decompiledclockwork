using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AlertTrigger.AlertTriggerDefinitions;

namespace TechnoPro.Common.Converter.AlertTriggers.Serializers
{
	// Token: 0x0200002B RID: 43
	public class AlertTriggerDefinitionMissingInfoSerializer : IAlertTriggerDefinitionSerializer<AlertTriggerDefinitionMissingInfoBase>
	{
		// Token: 0x060000DB RID: 219 RVA: 0x0000627C File Offset: 0x0000447C
		public XElement Serialize(AlertTriggerDefinitionMissingInfoBase dataObj)
		{
			XElement xelement = (dataObj != null) ? dataObj.CreateBaseAlertTriggerElement<AlertTriggerDefinitionMissingInfo>() : null;
			if (xelement != null)
			{
				xelement.Add(new XAttribute("cid", (dataObj != null) ? dataObj.ControlId : 0));
			}
			if (xelement != null)
			{
				xelement.Add(new XAttribute("screennum", (dataObj != null) ? dataObj.ScreenNum : 0));
			}
			return xelement;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000062F4 File Offset: 0x000044F4
		public AlertTriggerDefinitionMissingInfoBase DeSerialize(XElement element)
		{
			AlertTriggerDefinitionMissingInfoBase alertTriggerDefinitionMissingInfoBase = element.ExtractBaseAlertTriggerDefinition<AlertTriggerDefinitionMissingInfoBase>();
			XAttribute xattribute = element.Attribute("cid");
			XAttribute xattribute2 = element.Attribute("screennum");
			alertTriggerDefinitionMissingInfoBase.SetValues((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0, (xattribute2 != null) ? xattribute2.GetIntFromAttribute(0) : 0);
			return alertTriggerDefinitionMissingInfoBase;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006354 File Offset: 0x00004554
		public AlertTriggerDefinitionMissingInfoBase[] DeSerializeLegacy(string codeStr, string[] parts)
		{
			bool flag = parts.Length != 0 && parts[0].ToLower().StartsWith("screencontrolids=");
			if (flag)
			{
				parts[0] = parts[0].Substring(17);
			}
			List<Pair<int, int>> list = (from g in parts
			select g.Split(new char[]
			{
				'/'
			}) into h
			where h.Length > 1
			select h into m
			select new Pair<int, int>(m[0].Trim().ConvertStringToInt(0), m[1].Trim().ConvertStringToInt(0)) into n
			where n != null && n.Item1 > 0 && n.Item2 > 0
			select n).ToList<Pair<int, int>>();
			AlertTriggerDefinitionMissingInfoBase[] result;
			if (list.Count <= 0)
			{
				result = null;
			}
			else
			{
				result = (from g in list
				select new AlertTriggerDefinitionMissingInfoBase(g.Item1, g.Item2)
				{
					IsDisabled = false,
					OrderNum = 0
				}).ToArray<AlertTriggerDefinitionMissingInfoBase>();
			}
			return result;
		}
	}
}
