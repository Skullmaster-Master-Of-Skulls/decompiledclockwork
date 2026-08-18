using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000307 RID: 775
	internal class WeekViewSettingsConverter : BaseViewSettingsConverter
	{
		// Token: 0x06001A5A RID: 6746 RVA: 0x00055BA2 File Offset: 0x00053DA2
		public WeekViewSettingsConverter(IGantt owner)
		{
			this._gantt = owner;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00055BB1 File Offset: 0x00053DB1
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00055BB8 File Offset: 0x00053DB8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			WeekViewSettings weekViewSettings = obj as WeekViewSettings;
			Dictionary<string, object> baseDictionary = base.GetBaseDictionary(obj);
			string text = "javascript:";
			if (this._gantt.SelectedView == weekViewSettings.Type)
			{
				baseDictionary["selected"] = true;
			}
			if (weekViewSettings.SlotWidth.Value != 100.0)
			{
				baseDictionary["slotSize"] = weekViewSettings.SlotWidth.Value;
			}
			if (weekViewSettings.DayHeaderDateFormat != "ddd M/dd" && string.IsNullOrEmpty(weekViewSettings.DayHeaderTemplate))
			{
				baseDictionary["dayHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')#", weekViewSettings.DayHeaderDateFormat);
			}
			else if (weekViewSettings.DayHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "dayHeaderTemplate", weekViewSettings.DayHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "dayHeaderTemplate", weekViewSettings.DayHeaderTemplate, "");
			}
			if (weekViewSettings.WeekHeaderDateFormat != "ddd M/dd" && string.IsNullOrEmpty(weekViewSettings.WeekHeaderTemplate))
			{
				baseDictionary["weekHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')# - #=kendo.toString(kendo.date.addDays(end, -1), '{0}')#", weekViewSettings.WeekHeaderDateFormat);
			}
			else if (weekViewSettings.WeekHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "weekHeaderTemplate", weekViewSettings.WeekHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "weekHeaderTemplate", weekViewSettings.WeekHeaderTemplate, "");
			}
			baseDictionary["type"] = "week";
			return baseDictionary;
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x00055D60 File Offset: 0x00053F60
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(WeekViewSettings)
				};
			}
		}

		// Token: 0x040006BB RID: 1723
		private readonly IGantt _gantt;
	}
}
