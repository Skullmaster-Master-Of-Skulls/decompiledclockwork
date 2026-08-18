using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000A23 RID: 2595
	internal class RadButtonToggleStateConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x0600620D RID: 25101 RVA: 0x00171F0F File Offset: 0x0017010F
		// (set) Token: 0x0600620E RID: 25102 RVA: 0x00171F17 File Offset: 0x00170117
		public Control ParentButton { get; set; }

		// Token: 0x0600620F RID: 25103 RVA: 0x00171F20 File Offset: 0x00170120
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadButtonToggleState radButtonToggleState = obj as RadButtonToggleState;
			if (radButtonToggleState == null)
			{
				throw new InvalidOperationException("Can serialize only RadButtonToggleState objects.");
			}
			Control parentButton = this.ParentButton;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string text = radButtonToggleState.Text;
			if (!string.IsNullOrEmpty(text))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "text", text, string.Empty);
			}
			string cssClass = radButtonToggleState.CssClass;
			if (!string.IsNullOrEmpty(cssClass))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "cssClass", cssClass, string.Empty);
			}
			string value = radButtonToggleState.Value;
			if (!string.IsNullOrEmpty(value))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "value", value, string.Empty);
			}
			string hoveredCssClass = radButtonToggleState.HoveredCssClass;
			if (!string.IsNullOrEmpty(hoveredCssClass))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredCssClass", hoveredCssClass, string.Empty);
			}
			string pressedCssClass = radButtonToggleState.PressedCssClass;
			if (!string.IsNullOrEmpty(pressedCssClass))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "pressedCssClass", pressedCssClass, string.Empty);
			}
			Unit height = radButtonToggleState.Height;
			if (height != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "height", height.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit width = radButtonToggleState.Width;
			if (width != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "width", width.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			string primaryIconCssClass = radButtonToggleState.PrimaryIconCssClass;
			if (!string.IsNullOrEmpty(primaryIconCssClass))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconCssClass", primaryIconCssClass, string.Empty);
			}
			string primaryIconUrl = radButtonToggleState.PrimaryIconUrl;
			if (!string.IsNullOrEmpty(primaryIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconUrl", parentButton.ResolveUrl(primaryIconUrl), string.Empty);
			}
			string primaryHoveredIconUrl = radButtonToggleState.PrimaryHoveredIconUrl;
			if (!string.IsNullOrEmpty(primaryHoveredIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryHoveredIconUrl", parentButton.ResolveUrl(primaryHoveredIconUrl), string.Empty);
			}
			string primaryPressedIconUrl = radButtonToggleState.PrimaryPressedIconUrl;
			if (!string.IsNullOrEmpty(primaryPressedIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryPressedIconUrl", parentButton.ResolveUrl(primaryPressedIconUrl), string.Empty);
			}
			Unit primaryIconHeight = radButtonToggleState.PrimaryIconHeight;
			if (primaryIconHeight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconHeight", primaryIconHeight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconWidth = radButtonToggleState.PrimaryIconWidth;
			if (primaryIconWidth != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconWidth", primaryIconWidth.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconTop = radButtonToggleState.PrimaryIconTop;
			if (primaryIconTop != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconTop", primaryIconTop.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconLeft = radButtonToggleState.PrimaryIconLeft;
			if (primaryIconLeft != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconLeft", primaryIconLeft.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconBottom = radButtonToggleState.PrimaryIconBottom;
			if (primaryIconBottom != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconBottom", primaryIconBottom.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconRight = radButtonToggleState.PrimaryIconRight;
			if (primaryIconRight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconRight", primaryIconRight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			string secondaryIconCssClass = radButtonToggleState.SecondaryIconCssClass;
			if (!string.IsNullOrEmpty(secondaryIconCssClass))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconCssClass", secondaryIconCssClass, string.Empty);
			}
			string secondaryIconUrl = radButtonToggleState.SecondaryIconUrl;
			if (!string.IsNullOrEmpty(secondaryIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconUrl", parentButton.ResolveUrl(secondaryIconUrl), string.Empty);
			}
			string secondaryHoveredIconUrl = radButtonToggleState.SecondaryHoveredIconUrl;
			if (!string.IsNullOrEmpty(secondaryHoveredIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryHoveredIconUrl", parentButton.ResolveUrl(secondaryHoveredIconUrl), string.Empty);
			}
			string secondaryPressedIconUrl = radButtonToggleState.SecondaryPressedIconUrl;
			if (!string.IsNullOrEmpty(secondaryPressedIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryPressedIconUrl", parentButton.ResolveUrl(secondaryPressedIconUrl), string.Empty);
			}
			Unit secondaryIconHeight = radButtonToggleState.SecondaryIconHeight;
			if (secondaryIconHeight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconHeight", secondaryIconHeight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconWidth = radButtonToggleState.SecondaryIconWidth;
			if (secondaryIconWidth != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconWidth", secondaryIconWidth.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconTop = radButtonToggleState.SecondaryIconTop;
			if (secondaryIconTop != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconTop", secondaryIconTop.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconLeft = radButtonToggleState.SecondaryIconLeft;
			if (secondaryIconLeft != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconLeft", secondaryIconLeft.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconBottom = radButtonToggleState.SecondaryIconBottom;
			if (secondaryIconBottom != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconBottom", secondaryIconBottom.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconRight = radButtonToggleState.SecondaryIconRight;
			if (secondaryIconRight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconRight", secondaryIconRight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "isBackgroundImage", radButtonToggleState.IsBackgroundImage, false);
			string imageUrl = radButtonToggleState.ImageUrl;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "imageUrl", parentButton.ResolveUrl(imageUrl), string.Empty);
			}
			string hoveredImageUrl = radButtonToggleState.HoveredImageUrl;
			if (!string.IsNullOrEmpty(hoveredImageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredImageUrl", parentButton.ResolveUrl(hoveredImageUrl), string.Empty);
			}
			string pressedImageUrl = radButtonToggleState.PressedImageUrl;
			if (!string.IsNullOrEmpty(pressedImageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "pressedImageUrl", parentButton.ResolveUrl(pressedImageUrl), string.Empty);
			}
			return dictionary;
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x06006210 RID: 25104 RVA: 0x0017248C File Offset: 0x0017068C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadButtonToggleState)
				};
			}
		}
	}
}
