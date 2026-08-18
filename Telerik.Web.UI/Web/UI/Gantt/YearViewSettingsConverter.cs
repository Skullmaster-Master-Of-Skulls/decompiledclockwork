using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000305 RID: 773
	internal class YearViewSettingsConverter : BaseViewSettingsConverter
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x000557DF File Offset: 0x000539DF
		public YearViewSettingsConverter(IGantt owner)
		{
			this._gantt = owner;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000557EE File Offset: 0x000539EE
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000557F8 File Offset: 0x000539F8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			YearViewSettings yearViewSettings = obj as YearViewSettings;
			Dictionary<string, object> baseDictionary = base.GetBaseDictionary(obj);
			string text = "javascript:";
			if (this._gantt.SelectedView == yearViewSettings.Type)
			{
				baseDictionary["selected"] = true;
			}
			if (yearViewSettings.SlotWidth.Value != 100.0)
			{
				baseDictionary["slotSize"] = yearViewSettings.SlotWidth.Value;
			}
			if (yearViewSettings.YearHeaderDateFormat != "yyyy" && string.IsNullOrEmpty(yearViewSettings.YearHeaderTemplate))
			{
				baseDictionary["yearHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')#", yearViewSettings.YearHeaderDateFormat);
			}
			else if (yearViewSettings.YearHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "yearHeaderTemplate", yearViewSettings.YearHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "yearHeaderTemplate", yearViewSettings.YearHeaderTemplate, "");
			}
			if (yearViewSettings.MonthHeaderDateFormat != "MMM" && string.IsNullOrEmpty(yearViewSettings.MonthHeaderTemplate))
			{
				baseDictionary["monthHeaderTemplate"] = string.Format("#=kendo.toString(start, '{0}')#", yearViewSettings.MonthHeaderDateFormat);
			}
			else if (yearViewSettings.MonthHeaderTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(baseDictionary, "monthHeaderTemplate", yearViewSettings.MonthHeaderTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(baseDictionary, "monthHeaderTemplate", yearViewSettings.MonthHeaderTemplate, "");
			}
			baseDictionary["type"] = "year";
			return baseDictionary;
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x000559A0 File Offset: 0x00053BA0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(YearViewSettings)
				};
			}
		}

		// Token: 0x040006B9 RID: 1721
		private readonly IGantt _gantt;
	}
}
