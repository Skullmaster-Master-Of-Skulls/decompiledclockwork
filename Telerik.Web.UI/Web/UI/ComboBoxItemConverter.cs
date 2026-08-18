using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001AE6 RID: 6886
	internal class ComboBoxItemConverter : JavaScriptConverter
	{
		// Token: 0x06010ADA RID: 68314 RVA: 0x003B7616 File Offset: 0x003B5816
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06010ADB RID: 68315 RVA: 0x003B7620 File Offset: 0x003B5820
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadComboBoxItem radComboBoxItem = obj as RadComboBoxItem;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			bool flag = radComboBoxItem.Text.StartsWith(" ");
			bool flag2 = radComboBoxItem.Text.Contains("  ");
			if (radComboBoxItem.Templated || flag || flag2)
			{
				dictionary.Add("text", radComboBoxItem.Text);
			}
			if (!string.IsNullOrEmpty(radComboBoxItem.Value))
			{
				dictionary.Add("value", radComboBoxItem.Value);
			}
			if (radComboBoxItem.Selected)
			{
				dictionary.Add("selected", radComboBoxItem.Selected);
			}
			if (!radComboBoxItem.Enabled)
			{
				dictionary.Add("enabled", 0);
			}
			if (!radComboBoxItem.ComboBoxParent.IsNativeMode)
			{
				if (radComboBoxItem.IsSeparator)
				{
					dictionary.Add("isSeparator", true);
				}
				if (!string.IsNullOrEmpty(radComboBoxItem.DisabledImageUrl))
				{
					dictionary.Add("disabledImageUrl", radComboBoxItem.ResolveClientUrl(radComboBoxItem.DisabledImageUrl));
				}
				if (!string.IsNullOrEmpty(radComboBoxItem.ImageUrl))
				{
					dictionary.Add("imageUrl", radComboBoxItem.ResolveClientUrl(radComboBoxItem.ImageUrl));
				}
				if (radComboBoxItem.ComboBoxParent.CheckBoxes)
				{
					dictionary.Add("checked", radComboBoxItem.Checked);
				}
			}
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = attributeCollectionConverter.Serialize(radComboBoxItem.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			return dictionary;
		}

		// Token: 0x17005127 RID: 20775
		// (get) Token: 0x06010ADC RID: 68316 RVA: 0x003B7860 File Offset: 0x003B5A60
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadComboBoxItem);
				yield break;
			}
		}
	}
}
