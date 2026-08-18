using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020011B8 RID: 4536
	internal class RadMenuItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600BA48 RID: 47688 RVA: 0x002978FC File Offset: 0x00295AFC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadMenuItem radMenuItem = obj as RadMenuItem;
			RadMenu menu = radMenuItem.Menu;
			if (radMenuItem == null)
			{
				throw new InvalidOperationException("Can serialize only RadMenuItem objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RadMenuItemGroupSettingsConverter radMenuItemGroupSettingsConverter = new RadMenuItemGroupSettingsConverter();
			IDictionary<string, object> dictionary2 = radMenuItemGroupSettingsConverter.Serialize(radMenuItem.GroupSettings, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("groupSettings", radMenuItem.GroupSettings);
			}
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary3 = attributeCollectionConverter.Serialize(radMenuItem.Attributes, serializer);
			if (dictionary3.Count > 0)
			{
				dictionary.Add("attributes", dictionary3);
			}
			IList<ControlItem> visibleItems = radMenuItem.Items.VisibleItems;
			if (visibleItems.Count > 0)
			{
				dictionary.Add("items", visibleItems);
			}
			if (!radMenuItem.PostBack)
			{
				dictionary.Add("postBack", 0);
			}
			if (radMenuItem.Templated)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "text", radMenuItem.Text, string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "value", radMenuItem.Value, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enabled", radMenuItem.Enabled, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selected", radMenuItem.Selected, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "navigateUrl", radMenuItem.NavigateUrl, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "target", radMenuItem.Target, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isSeparator", radMenuItem.IsSeparator, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledCssClass", radMenuItem.DisabledCssClass, "rmDisabled");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "expandedCssClass", radMenuItem.ExpandedCssClass, "rmExpanded");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "focusedCssClass", radMenuItem.FocusedCssClass, "rmFocused");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selectedCssClass", radMenuItem.SelectedCssClass, "rmSelected");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "clickedCssClass", radMenuItem.ClickedCssClass, "rmClicked");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "outerCssClass", radMenuItem.OuterCssClass, "");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "cssClass", radMenuItem.CssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredImageUrl", radMenuItem.ResolveClientUrl(radMenuItem.HoveredImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "clickedImageUrl", radMenuItem.ResolveClientUrl(radMenuItem.ClickedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledImageUrl", radMenuItem.ResolveClientUrl(radMenuItem.DisabledImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "expandedImageUrl", radMenuItem.ResolveClientUrl(radMenuItem.ExpandedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selectedImageUrl", radMenuItem.ResolveClientUrl(radMenuItem.SelectedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "expandMode", radMenuItem.ExpandMode, MenuItemExpandMode.ClientSide);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "templated", radMenuItem.Templated, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hasContentTemplate", radMenuItem.HasContentTemplate, false);
			if (menu != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "enableImageSprite", radMenuItem.EnableImageSpriteResolved, menu.EnableImageSprites);
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "enableImageSprite", radMenuItem.EnableImageSpriteResolved, false);
			}
			if (radMenuItem.CurrentImageUrl != radMenuItem.ImageUrl && !string.IsNullOrEmpty(radMenuItem.ImageUrl))
			{
				dictionary.Add("imageUrl", radMenuItem.ResolveClientUrl(radMenuItem.ImageUrl));
			}
			return dictionary;
		}

		// Token: 0x17003C10 RID: 15376
		// (get) Token: 0x0600BA49 RID: 47689 RVA: 0x00297C70 File Offset: 0x00295E70
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadMenuItem)
				};
			}
		}
	}
}
