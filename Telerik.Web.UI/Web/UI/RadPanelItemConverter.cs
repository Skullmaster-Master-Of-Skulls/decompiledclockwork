using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019FC RID: 6652
	internal class RadPanelItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0601019B RID: 65947 RVA: 0x0039E7E8 File Offset: 0x0039C9E8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadPanelItem radPanelItem = obj as RadPanelItem;
			if (radPanelItem == null)
			{
				throw new InvalidOperationException("Can serialize only RadPanelItem objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = attributeCollectionConverter.Serialize(radPanelItem.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			IList<ControlItem> visibleItems = radPanelItem.Items.VisibleItems;
			if (visibleItems.Count > 0)
			{
				dictionary.Add("items", visibleItems);
			}
			if (!radPanelItem.PostBack)
			{
				dictionary.Add("postBack", false);
			}
			if (radPanelItem.Templated)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "text", radPanelItem.Text, string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isSeparator", radPanelItem.IsSeparator, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "preventCollapse", radPanelItem.PreventCollapse, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "value", radPanelItem.Value, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selected", radPanelItem.Selected, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enabled", radPanelItem.Enabled, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "expanded", radPanelItem.Expanded, false);
			if (!string.IsNullOrEmpty(radPanelItem.NavigateUrl) && radPanelItem.Templated)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "navigateUrl", radPanelItem.ResolveClientUrl(radPanelItem.NavigateUrl), string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "target", radPanelItem.Target, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "cssClass", radPanelItem.CssClass, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "expandedCssClass", radPanelItem.ExpandedCssClass, "rpExpanded");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "clickedCssClass", radPanelItem.ClickedCssClass, "rpClicked");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledCssClass", radPanelItem.DisabledCssClass, "rpDisabled");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selectedCssClass", radPanelItem.SelectedCssClass, "rpSelected");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "focusedCssClass", radPanelItem.FocusedCssClass, "rpFocused");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredImageUrl", radPanelItem.ResolveClientUrl(radPanelItem.HoveredImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selectedImageUrl", radPanelItem.ResolveClientUrl(radPanelItem.SelectedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledImageUrl", radPanelItem.ResolveClientUrl(radPanelItem.DisabledImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "expandedImageUrl", radPanelItem.ResolveClientUrl(radPanelItem.ExpandedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "imageUrl", radPanelItem.ResolveClientUrl(radPanelItem.ImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "target", radPanelItem.Target, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "templated", radPanelItem.Templated, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hasContentTemplate", radPanelItem.HasContentTemplate, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "cssClass", radPanelItem.CssClass, string.Empty);
			if (radPanelItem.ChildGroupHeight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "isChildGroupHeightSet", true, false);
			}
			return dictionary;
		}

		// Token: 0x17004DB6 RID: 19894
		// (get) Token: 0x0601019C RID: 65948 RVA: 0x0039EB1C File Offset: 0x0039CD1C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadPanelItem)
				};
			}
		}
	}
}
