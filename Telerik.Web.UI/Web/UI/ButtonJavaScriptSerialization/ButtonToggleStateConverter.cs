using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Web.UI.ButtonJavaScriptSerialization
{
	// Token: 0x020000D7 RID: 215
	internal class ButtonToggleStateConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001E81C File Offset: 0x0001CA1C
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x0001E824 File Offset: 0x0001CA24
		public Func<string, string> ResolveUrl { get; set; }

		// Token: 0x06000836 RID: 2102 RVA: 0x0001E830 File Offset: 0x0001CA30
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ButtonToggleState buttonToggleState = obj as ButtonToggleState;
			if (this.ResolveUrl == null)
			{
				throw new MissingMemberException("Please, make sure ResolveUrl resolver method is defined.");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "text", buttonToggleState.Text, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "value", buttonToggleState.Value, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "selected", buttonToggleState.Selected, false);
			ExplicitJavaScriptConverter.AddProperty(state, "cssClass", buttonToggleState.CssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "hoveredCssClass", buttonToggleState.HoveredCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "pressedCssClass", buttonToggleState.PressedCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "width", buttonToggleState.Width.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "height", buttonToggleState.Height.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconUrl", this.ResolveUrl(buttonToggleState.Icon.Url), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryHoveredIconUrl", this.ResolveUrl(buttonToggleState.Icon.HoveredUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryPressedIconUrl", this.ResolveUrl(buttonToggleState.Icon.PressedUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconHeight", buttonToggleState.Icon.Height.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconWidth", buttonToggleState.Icon.Width.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconTop", buttonToggleState.Icon.Top.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconLeft", buttonToggleState.Icon.Left.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconCssClass", buttonToggleState.Icon.CssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconHoveredCssClass", buttonToggleState.Icon.HoveredCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconPressedCssClass", buttonToggleState.Icon.PressedCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "isBackgroundImage", true, false);
			ExplicitJavaScriptConverter.AddProperty(state, "imageUrl", this.ResolveUrl(buttonToggleState.Image.Url), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "hoveredImageUrl", this.ResolveUrl(buttonToggleState.Image.HoveredUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "pressedImageUrl", this.ResolveUrl(buttonToggleState.Image.PressedUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "disabledImageUrl", this.ResolveUrl(buttonToggleState.Image.DisabledUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "imageSizing", buttonToggleState.Image.Sizing, ImageSizing.Original);
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001EB60 File Offset: 0x0001CD60
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ButtonToggleState)
				};
			}
		}
	}
}
