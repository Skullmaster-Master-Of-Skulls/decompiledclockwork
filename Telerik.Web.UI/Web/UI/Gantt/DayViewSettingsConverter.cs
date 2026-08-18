using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000308 RID: 776
	internal class DayViewSettingsConverter : BaseViewSettingsConverter
	{
		// Token: 0x06001A5E RID: 6750 RVA: 0x00055D82 File Offset: 0x00053F82
		public DayViewSettingsConverter(IGantt owner)
		{
			this._gantt = owner;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00055D91 File Offset: 0x00053F91
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00055D98 File Offset: 0x00053F98
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			DayViewSettings dayViewSettings = obj as DayViewSettings;
			Dictionary<string, object> baseDictionary = base.GetBaseDictionary(obj);
			string text = "javascript:";
			if (this._gantt.SelectedView == dayViewSettings.Type)
			{
				baseDictionary["selected"] = true;
			}
			if (dayViewSettings.SlotWidth.Value != 100.0)
			{
				baseDictionary["slotSize"] = dayViewSettings.SlotWidth.Value;
			}
			if (dayViewSettings.TimeHeaderDateFormat != "t" && string.IsNullOrEmpty(dayViewSettings.TimeHeaderTemplate))
			{
				baseDictionary["timeHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')#", dayViewSettings.TimeHeaderDateFormat);
			}
			else if (dayViewSettings.TimeHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "timeHeaderTemplate", dayViewSettings.TimeHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "timeHeaderTemplate", dayViewSettings.TimeHeaderTemplate, "");
			}
			if (dayViewSettings.DayHeaderDateFormat != "ddd M/dd" && string.IsNullOrEmpty(dayViewSettings.DayHeaderTemplate))
			{
				baseDictionary["dayHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')#", dayViewSettings.DayHeaderDateFormat);
			}
			else if (dayViewSettings.DayHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "dayHeaderTemplate", dayViewSettings.DayHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "dayHeaderTemplate", dayViewSettings.DayHeaderTemplate, "");
			}
			baseDictionary["type"] = "day";
			return baseDictionary;
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00055F40 File Offset: 0x00054140
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DayViewSettings)
				};
			}
		}

		// Token: 0x040006BC RID: 1724
		private readonly IGantt _gantt;
	}
}
