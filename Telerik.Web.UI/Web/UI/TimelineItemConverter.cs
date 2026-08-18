using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Common.SerializeJS;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x02000928 RID: 2344
	internal class TimelineItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06005910 RID: 22800 RVA: 0x0010F9DA File Offset: 0x0010DBDA
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x0010F9E4 File Offset: 0x0010DBE4
		public bool IsNumber(object value)
		{
			return value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal;
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x0010FA4C File Offset: 0x0010DC4C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TimelineItem timelineItem = (TimelineItem)obj;
			JavaScriptSerializerMarkers javaScriptSerializerMarkers = new JavaScriptSerializerMarkers();
			bool flag = timelineItem.Owner != null;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(timelineItem.Description))
			{
				dictionary.Add((flag && !string.IsNullOrEmpty(timelineItem.Owner.DataDescriptionField)) ? timelineItem.Owner.DataDescriptionField : "description", timelineItem.Description);
			}
			if (!string.IsNullOrEmpty(timelineItem.Subtitle))
			{
				dictionary.Add((flag && !string.IsNullOrEmpty(timelineItem.Owner.DataSubtitleField)) ? timelineItem.Owner.DataSubtitleField : "subtitle", timelineItem.Subtitle);
			}
			if (!string.IsNullOrEmpty(timelineItem.Title))
			{
				dictionary.Add((flag && !string.IsNullOrEmpty(timelineItem.Owner.DataTitleField)) ? timelineItem.Owner.DataTitleField : "title", timelineItem.Title);
			}
			string value = string.Empty;
			string value2 = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				timelineItem.Date
			});
			value = javaScriptSerializerMarkers.WrapInMarkers(HtmlChartHelper.GetSerializedValueField(value2, true));
			dictionary.Add((flag && !string.IsNullOrEmpty(timelineItem.Owner.DataDateField)) ? timelineItem.Owner.DataDateField : "date", value);
			if (timelineItem.TemplateData != null)
			{
				using (Dictionary<string, object>.Enumerator enumerator = timelineItem.TemplateData.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						string value3 = string.Empty;
						if (keyValuePair.Value is DateTime || keyValuePair.Value is DateTime?)
						{
							string value4 = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
							{
								keyValuePair.Value
							});
							value3 = javaScriptSerializerMarkers.WrapInMarkers(HtmlChartHelper.GetSerializedValueField(value4, true));
							dictionary.Add(keyValuePair.Key, value3);
						}
						else if (this.IsNumber(keyValuePair.Value))
						{
							string value5 = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
							{
								keyValuePair.Value
							});
							value3 = javaScriptSerializerMarkers.WrapInMarkers(HtmlChartHelper.GetSerializedValueField(value5, false));
							dictionary.Add(keyValuePair.Key, value3);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					goto IL_27F;
				}
			}
			if (timelineItem.DataItem != null)
			{
				dictionary.Add("dataItem", timelineItem.DataItem);
			}
			IL_27F:
			if (timelineItem.Attributes.Count > 0)
			{
				dictionary.Add("attributes", timelineItem.Attributes);
			}
			if (timelineItem.Actions.Count > 0)
			{
				dictionary.Add((flag && !string.IsNullOrEmpty(timelineItem.Owner.DataActionsField)) ? timelineItem.Owner.DataActionsField : "actions", timelineItem.Actions);
			}
			if (timelineItem.Images.Count > 0)
			{
				dictionary.Add((flag && !string.IsNullOrEmpty(timelineItem.Owner.DataImagesField)) ? timelineItem.Owner.DataImagesField : "images", timelineItem.Images);
			}
			return dictionary;
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x0010FE58 File Offset: 0x0010E058
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(TimelineItem);
				yield break;
			}
		}
	}
}
