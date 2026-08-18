using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000306 RID: 774
	internal class MonthViewSettingsConverter : BaseViewSettingsConverter
	{
		// Token: 0x06001A56 RID: 6742 RVA: 0x000559C2 File Offset: 0x00053BC2
		public MonthViewSettingsConverter(IGantt owner)
		{
			this._gantt = owner;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000559D1 File Offset: 0x00053BD1
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000559D8 File Offset: 0x00053BD8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			MonthViewSettings monthViewSettings = obj as MonthViewSettings;
			Dictionary<string, object> baseDictionary = base.GetBaseDictionary(obj);
			string text = "javascript:";
			if (this._gantt.SelectedView == monthViewSettings.Type)
			{
				baseDictionary["selected"] = true;
			}
			if (monthViewSettings.SlotWidth.Value != 150.0)
			{
				baseDictionary["slotSize"] = monthViewSettings.SlotWidth.Value;
			}
			if (monthViewSettings.WeekHeaderDateFormat != "ddd M/dd" && string.IsNullOrEmpty(monthViewSettings.WeekHeaderTemplate))
			{
				baseDictionary["weekHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')# - #=kendo.toString(kendo.date.addDays(end, -1), '{0}')#", monthViewSettings.WeekHeaderDateFormat);
			}
			else if (monthViewSettings.WeekHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "weekHeaderTemplate", monthViewSettings.WeekHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "weekHeaderTemplate", monthViewSettings.WeekHeaderTemplate, "");
			}
			if (monthViewSettings.MonthHeaderDateFormat != "MMMM, yyyy" && string.IsNullOrEmpty(monthViewSettings.MonthHeaderTemplate))
			{
				baseDictionary["monthHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')#", monthViewSettings.MonthHeaderDateFormat);
			}
			else if (monthViewSettings.MonthHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "monthHeaderTemplate", monthViewSettings.MonthHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "monthHeaderTemplate", monthViewSettings.MonthHeaderTemplate, "");
			}
			baseDictionary["type"] = "month";
			return baseDictionary;
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00055B80 File Offset: 0x00053D80
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MonthViewSettings)
				};
			}
		}

		// Token: 0x040006BA RID: 1722
		private readonly IGantt _gantt;
	}
}
