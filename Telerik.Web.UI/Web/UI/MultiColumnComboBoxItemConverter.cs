using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Common.SerializeJS;
using Telerik.Web.UI.HtmlChart;

namespace Telerik.Web.UI
{
	// Token: 0x02000059 RID: 89
	internal class MultiColumnComboBoxItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000073E6 File Offset: 0x000055E6
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000073F0 File Offset: 0x000055F0
		public bool IsNumber(object value)
		{
			return value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007458 File Offset: 0x00005658
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			MultiColumnComboBoxItem multiColumnComboBoxItem = (MultiColumnComboBoxItem)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(multiColumnComboBoxItem.Text))
			{
				dictionary.Add("text", multiColumnComboBoxItem.Text);
			}
			if (!string.IsNullOrEmpty(multiColumnComboBoxItem.Value))
			{
				dictionary.Add("value", multiColumnComboBoxItem.Value);
			}
			if (multiColumnComboBoxItem.TemplateData != null)
			{
				using (Dictionary<string, object>.Enumerator enumerator = multiColumnComboBoxItem.TemplateData.GetEnumerator())
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
			if (multiColumnComboBoxItem.DataItem != null)
			{
				dictionary.Add("dataItem", multiColumnComboBoxItem.DataItem);
			}
			IL_17D:
			if (multiColumnComboBoxItem.Attributes.Count > 0)
			{
				dictionary.Add("attributes", multiColumnComboBoxItem.Attributes);
			}
			if (multiColumnComboBoxItem.Items.Count > 0)
			{
				dictionary.Add("items", multiColumnComboBoxItem.Items);
			}
			return dictionary;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00007700 File Offset: 0x00005900
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(MultiColumnComboBoxItem);
				yield break;
			}
		}
	}
}
