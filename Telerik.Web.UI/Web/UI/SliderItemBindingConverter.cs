using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200087C RID: 2172
	internal class SliderItemBindingConverter : JavaScriptConverter
	{
		// Token: 0x06005063 RID: 20579 RVA: 0x000FB50C File Offset: 0x000F970C
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x000FB514 File Offset: 0x000F9714
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			SliderItemBinding sliderItemBinding = obj as SliderItemBinding;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(sliderItemBinding.TextField))
			{
				dictionary.Add("textField", sliderItemBinding.TextField);
			}
			if (!string.IsNullOrEmpty(sliderItemBinding.ValueField))
			{
				dictionary.Add("valueField", sliderItemBinding.ValueField);
			}
			if (!string.IsNullOrEmpty(sliderItemBinding.ToolTipField))
			{
				dictionary.Add("toolTipField", sliderItemBinding.ToolTipField);
			}
			return dictionary;
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06005065 RID: 20581 RVA: 0x000FB658 File Offset: 0x000F9858
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(SliderItemBinding);
				yield break;
			}
		}
	}
}
