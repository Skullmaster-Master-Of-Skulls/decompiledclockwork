using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B54 RID: 6996
	internal class RadToolBarItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06010F3A RID: 69434 RVA: 0x003C08B0 File Offset: 0x003BEAB0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadToolBarItem radToolBarItem = obj as RadToolBarItem;
			RadToolBar toolBar = radToolBarItem.ToolBar;
			if (radToolBarItem == null)
			{
				throw new InvalidOperationException("Can serialize only RadToolBarItem objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			AttributeCollectionConverter attributeCollectionConverter = new AttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = attributeCollectionConverter.Serialize(radToolBarItem.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			this.SerializeButtonContainer(dictionary, radToolBarItem as IRadToolBarButtonContainer);
			this.SerializeIButton(dictionary, radToolBarItem as IRadToolBarButton);
			this.SerializeButton(dictionary, radToolBarItem as RadToolBarButton);
			this.SerializeSplitButton(dictionary, radToolBarItem as RadToolBarSplitButton);
			this.SerializeDropDown(dictionary, radToolBarItem as RadToolBarDropDown);
			if (radToolBarItem.Templated)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "text", radToolBarItem.Text, string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "type", radToolBarItem.ItemType, RadToolBarItemType.Button);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enabled", radToolBarItem.Enabled, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "toolTip", radToolBarItem.ToolTip, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "cssClass", radToolBarItem.CssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "spriteCssClass", radToolBarItem.SpriteCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "outerCssClass", radToolBarItem.OuterCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "templated", radToolBarItem.Templated, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "imageUrl", radToolBarItem.ResolveClientUrl(radToolBarItem.ImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredImageUrl", radToolBarItem.ResolveClientUrl(radToolBarItem.HoveredImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredCssClass", radToolBarItem.HoveredCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "clickedCssClass", radToolBarItem.ClickedCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "clickedImageUrl", radToolBarItem.ResolveClientUrl(radToolBarItem.ClickedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledImageUrl", radToolBarItem.ResolveClientUrl(radToolBarItem.DisabledImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledCssClass", radToolBarItem.DisabledCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "focusedImageUrl", radToolBarItem.ResolveClientUrl(radToolBarItem.FocusedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "focusedCssClass", radToolBarItem.FocusedCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "imagePosition", radToolBarItem.ImagePosition, ToolBarImagePosition.Left);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "overFlow", radToolBarItem.OverFlow, ToolBarOverflow.Auto);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showText", radToolBarItem.ShowText, ToolBarShowPosition.Both);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "showImage", radToolBarItem.ShowImage, ToolBarShowPosition.Both);
			if (toolBar != null)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "enableImageSprite", radToolBarItem.EnableImageSpriteResolved, toolBar.EnableImageSprites);
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "enableImageSprite", radToolBarItem.EnableImageSpriteResolved, false);
			}
			return dictionary;
		}

		// Token: 0x06010F3B RID: 69435 RVA: 0x003C0BB0 File Offset: 0x003BEDB0
		public void SerializeButtonContainer(Dictionary<string, object> state, IRadToolBarButtonContainer buttonContainer)
		{
			if (buttonContainer == null)
			{
				return;
			}
			IList<ControlItem> visibleItems = buttonContainer.Buttons.VisibleItems;
			if (visibleItems.Count > 0)
			{
				state.Add("items", visibleItems);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "dropDownWidth", buttonContainer.DropDownWidth.ToString(), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "dropDownHeight", buttonContainer.DropDownHeight.ToString(), string.Empty);
		}

		// Token: 0x06010F3C RID: 69436 RVA: 0x003C0C2C File Offset: 0x003BEE2C
		public void SerializeButton(Dictionary<string, object> state, RadToolBarButton button)
		{
			if (button == null)
			{
				return;
			}
			ExplicitJavaScriptConverter.AddProperty(state, "isSeparator", button.IsSeparator, false);
			if (button.IsSeparator && !string.IsNullOrEmpty(button.Text))
			{
				ExplicitJavaScriptConverter.AddProperty(state, "text", button.Text, string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "checkOnClick", button.CheckOnClick, false);
			ExplicitJavaScriptConverter.AddProperty(state, "group", button.Group, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "checked", button.Checked, false);
			ExplicitJavaScriptConverter.AddProperty(state, "checkedCssClass", button.CheckedCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "checkedImageUrl", button.ResolveClientUrl(button.CheckedImageUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "allowSelfUnCheck", button.AllowSelfUnCheck, false);
		}

		// Token: 0x06010F3D RID: 69437 RVA: 0x003C0D20 File Offset: 0x003BEF20
		public void SerializeIButton(Dictionary<string, object> state, IRadToolBarButton button)
		{
			if (button == null)
			{
				return;
			}
			ExplicitJavaScriptConverter.AddProperty(state, "value", button.Value, string.Empty);
			RadToolBarButton radToolBarButton = button as RadToolBarButton;
			RadToolBarItem radToolBarItem = button as RadToolBarItem;
			if (radToolBarButton != null && radToolBarButton.IsSeparator)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "navigateUrl", button.NavigateUrl, string.Empty);
				ExplicitJavaScriptConverter.AddProperty(state, "target", button.Target, string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "commandName", button.CommandName, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "commandArgument", button.CommandArgument, string.Empty);
			RadToolBar toolBar = radToolBarItem.ToolBar;
			ExplicitJavaScriptConverter.AddProperty(state, "causesValidation", button.CausesValidation, toolBar.CausesValidation);
			if (button.ValidationGroup != toolBar.ValidationGroup)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "validationGroup", button.ValidationGroup, string.Empty);
			}
			if (button.PostBackUrl != toolBar.PostBackUrl)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "postBackUrl", button.PostBackUrl, string.Empty);
			}
			if (radToolBarItem.ToolBar.PostBack)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "postback", button.PostBack, true);
			}
		}

		// Token: 0x06010F3E RID: 69438 RVA: 0x003C0E5C File Offset: 0x003BF05C
		public void SerializeSplitButton(Dictionary<string, object> state, RadToolBarSplitButton splitButton)
		{
			if (splitButton == null)
			{
				return;
			}
			ExplicitJavaScriptConverter.AddProperty(state, "enableDefaultButton", splitButton.EnableDefaultButton, true);
			if (splitButton.EnableDefaultButton)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "defaultButtonIndex", splitButton.DefaultButtonIndex, 0);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "expandDirection", splitButton.ExpandDirection, ToolBarDropDownExpandDirection.Down);
		}

		// Token: 0x06010F3F RID: 69439 RVA: 0x003C0EC9 File Offset: 0x003BF0C9
		public void SerializeDropDown(Dictionary<string, object> state, RadToolBarDropDown dropDown)
		{
			if (dropDown == null)
			{
				return;
			}
			ExplicitJavaScriptConverter.AddProperty(state, "expandDirection", dropDown.ExpandDirection, ToolBarDropDownExpandDirection.Down);
		}

		// Token: 0x170052C2 RID: 21186
		// (get) Token: 0x06010F40 RID: 69440 RVA: 0x003C0EEC File Offset: 0x003BF0EC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadToolBarItem)
				};
			}
		}
	}
}
