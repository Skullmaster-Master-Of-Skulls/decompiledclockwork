using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Common.SerializeJS;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x020005FA RID: 1530
	internal class MultiSelectItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600375B RID: 14171 RVA: 0x000B73D8 File Offset: 0x000B55D8
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000B73E0 File Offset: 0x000B55E0
		public bool IsNumber(object value)
		{
			return value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal;
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000B7448 File Offset: 0x000B5648
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			MultiSelectItem multiSelectItem = (MultiSelectItem)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(multiSelectItem.Text))
			{
				dictionary.Add("text", multiSelectItem.Text);
			}
			if (!string.IsNullOrEmpty(multiSelectItem.Value))
			{
				dictionary.Add("value", multiSelectItem.Value);
			}
			if (multiSelectItem.TemplateData != null)
			{
				using (Dictionary<string, object>.Enumerator enumerator = multiSelectItem.TemplateData.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, object> keyValuePair = enumerator.Current;
						JavaScriptSerializerMarkers javaScriptSerializerMarkers = new JavaScriptSerializerMarkers();
						string value = string.Empty;
						if (keyValuePair.Value is DateTime || keyValuePair.Value is DateTime?)
						{
							string value2 = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
							{
								keyValuePair.Value
							});
							value = javaScriptSerializerMarkers.WrapInMarkers(HtmlChartHelper.GetSerializedValueField(value2, true));
							dictionary.Add(keyValuePair.Key, value);
						}
						else if (this.IsNumber(keyValuePair.Value))
						{
							string value3 = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
							{
								keyValuePair.Value
							});
							value = javaScriptSerializerMarkers.WrapInMarkers(HtmlChartHelper.GetSerializedValueField(value3, false));
							dictionary.Add(keyValuePair.Key, value);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					goto IL_17D;
				}
			}
			if (multiSelectItem.DataItem != null)
			{
				dictionary.Add("dataItem", multiSelectItem.DataItem);
			}
			IL_17D:
			if (multiSelectItem.Attributes.Count > 0)
			{
				dictionary.Add("attributes", multiSelectItem.Attributes);
			}
			if (multiSelectItem.Items.Count > 0)
			{
				dictionary.Add("items", multiSelectItem.Items);
			}
			return dictionary;
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000B76F0 File Offset: 0x000B58F0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(MultiSelectItem);
				yield break;
			}
		}
	}
}
