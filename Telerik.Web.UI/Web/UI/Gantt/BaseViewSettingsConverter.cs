using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000303 RID: 771
	public abstract class BaseViewSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001A49 RID: 6729 RVA: 0x00055694 File Offset: 0x00053894
		internal Dictionary<string, object> GetBaseDictionary(object obj)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			BaseViewSettings baseViewSettings = obj as BaseViewSettings;
			if (baseViewSettings.SelectedDate != null)
			{
				dictionary["date"] = baseViewSettings.SelectedDate.Value.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
			}
			if (baseViewSettings.RangeStart != null || baseViewSettings.RangeEnd != null)
			{
				Range range = new Range();
				if (baseViewSettings.RangeStart != null)
				{
					range.start = baseViewSettings.RangeStart.Value.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
				}
				if (baseViewSettings.RangeEnd != null)
				{
					range.end = baseViewSettings.RangeEnd.Value.ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
				}
				dictionary["range"] = range;
			}
			return dictionary;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00055795 File Offset: 0x00053995
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000557A1 File Offset: 0x000539A1
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x040006B6 RID: 1718
		public const string JavaScriptDateFormat = "yyyy/MM/dd HH:mm";
	}
}
