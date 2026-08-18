using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x020004ED RID: 1261
	internal class XAxisDateFormatterConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002CFF RID: 11519 RVA: 0x00093E2C File Offset: 0x0009202C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			DateFormatter dateFormatter = obj as DateFormatter;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "seconds", dateFormatter.SecondsFormat, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "minutes", dateFormatter.MinutesFormat, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hours", dateFormatter.HoursFormat, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "days", dateFormatter.DaysFormat, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "weeks", dateFormatter.WeeksFormat, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "months", dateFormatter.MonthsFormat, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "years", dateFormatter.YearsFormat, "");
			return dictionary;
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x00093EE4 File Offset: 0x000920E4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DateFormatter)
				};
			}
		}
	}
}
