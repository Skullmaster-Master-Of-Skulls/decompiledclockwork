using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000A25 RID: 2597
	internal class RadButtonIconConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x0600625A RID: 25178 RVA: 0x00172C5D File Offset: 0x00170E5D
		// (set) Token: 0x0600625B RID: 25179 RVA: 0x00172C65 File Offset: 0x00170E65
		public Control ParentButton { get; set; }

		// Token: 0x0600625C RID: 25180 RVA: 0x00172C70 File Offset: 0x00170E70
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadButtonIcon radButtonIcon = obj as RadButtonIcon;
			if (radButtonIcon == null)
			{
				throw new InvalidOperationException("Can serialize only RadButtonIcon objects.");
			}
			Control parentButton = this.ParentButton;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string primaryIconUrl = radButtonIcon.PrimaryIconUrl;
			if (!string.IsNullOrEmpty(primaryIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryIconUrl", parentButton.ResolveUrl(primaryIconUrl), string.Empty);
			}
			string primaryHoveredIconUrl = radButtonIcon.PrimaryHoveredIconUrl;
			if (!string.IsNullOrEmpty(primaryHoveredIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryHoveredIconUrl", parentButton.ResolveUrl(primaryHoveredIconUrl), string.Empty);
			}
			string primaryPressedIconUrl = radButtonIcon.PrimaryPressedIconUrl;
			if (!string.IsNullOrEmpty(primaryPressedIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryPressedIconUrl", parentButton.ResolveUrl(primaryPressedIconUrl), string.Empty);
			}
			Unit primaryIconHeight = radButtonIcon.PrimaryIconHeight;
			if (primaryIconHeight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryHeight", primaryIconHeight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconWidth = radButtonIcon.PrimaryIconWidth;
			if (primaryIconWidth != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryWidth", primaryIconWidth.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconTop = radButtonIcon.PrimaryIconTop;
			if (primaryIconTop != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryTop", primaryIconTop.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconBottom = radButtonIcon.PrimaryIconBottom;
			if (primaryIconBottom != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryBottom", primaryIconBottom.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconLeft = radButtonIcon.PrimaryIconLeft;
			if (primaryIconLeft != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryLeft", primaryIconLeft.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit primaryIconRight = radButtonIcon.PrimaryIconRight;
			if (primaryIconRight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryRight", primaryIconRight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "primaryCssClass", radButtonIcon.PrimaryIconCssClass, string.Empty);
			string secondaryIconUrl = radButtonIcon.SecondaryIconUrl;
			if (!string.IsNullOrEmpty(secondaryIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryIconUrl", parentButton.ResolveUrl(secondaryIconUrl), string.Empty);
			}
			string secondaryHoveredIconUrl = radButtonIcon.SecondaryHoveredIconUrl;
			if (!string.IsNullOrEmpty(secondaryHoveredIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryHoveredIconUrl", parentButton.ResolveUrl(secondaryHoveredIconUrl), string.Empty);
			}
			string secondaryPressedIconUrl = radButtonIcon.SecondaryPressedIconUrl;
			if (!string.IsNullOrEmpty(secondaryPressedIconUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryPressedIconUrl", parentButton.ResolveUrl(secondaryPressedIconUrl), string.Empty);
			}
			Unit secondaryIconHeight = radButtonIcon.SecondaryIconHeight;
			if (secondaryIconHeight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryHeight", secondaryIconHeight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconWidth = radButtonIcon.SecondaryIconWidth;
			if (secondaryIconWidth != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryWidth", secondaryIconWidth.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconTop = radButtonIcon.SecondaryIconTop;
			if (secondaryIconTop != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryTop", secondaryIconTop.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconBottom = radButtonIcon.SecondaryIconBottom;
			if (secondaryIconBottom != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryBottom", secondaryIconBottom.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconLeft = radButtonIcon.SecondaryIconLeft;
			if (secondaryIconLeft != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryLeft", secondaryIconLeft.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			Unit secondaryIconRight = radButtonIcon.SecondaryIconRight;
			if (secondaryIconRight != Unit.Empty)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryRight", secondaryIconRight.ToString(CultureInfo.InvariantCulture), string.Empty);
			}
			ExplicitJavaScriptConverter.AddProperty(dictionary, "secondaryCssClass", radButtonIcon.SecondaryIconCssClass, string.Empty);
			return dictionary;
		}

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x0600625D RID: 25181 RVA: 0x00173018 File Offset: 0x00171218
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadButtonIcon)
				};
			}
		}
	}
}
