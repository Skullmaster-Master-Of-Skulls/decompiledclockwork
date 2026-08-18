using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001AC9 RID: 6857
	internal class SliderItemConverter : JavaScriptConverter
	{
		// Token: 0x06010989 RID: 67977 RVA: 0x003B371E File Offset: 0x003B191E
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0601098A RID: 67978 RVA: 0x003B3728 File Offset: 0x003B1928
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadSliderItem radSliderItem = obj as RadSliderItem;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(radSliderItem.Text))
			{
				dictionary.Add("text", radSliderItem.Text);
			}
			if (!string.IsNullOrEmpty(radSliderItem.Value))
			{
				dictionary.Add("value", radSliderItem.Value);
			}
			if (!radSliderItem.Enabled)
			{
				dictionary.Add("enabled", radSliderItem.Enabled);
			}
			if (!string.IsNullOrEmpty(radSliderItem.CssClass))
			{
				dictionary.Add("cssClass", radSliderItem.CssClass);
			}
			if (!string.IsNullOrEmpty(radSliderItem.ToolTip))
			{
				dictionary.Add("tooltip", radSliderItem.ToolTip);
			}
			return dictionary;
		}

		// Token: 0x170050AE RID: 20654
		// (get) Token: 0x0601098B RID: 67979 RVA: 0x003B38A8 File Offset: 0x003B1AA8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadSliderItem);
				yield break;
			}
		}
	}
}
