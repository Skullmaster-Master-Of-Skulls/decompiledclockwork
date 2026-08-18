using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Web.UI.ButtonNS;

namespace Telerik.Web.UI.ButtonJavaScriptSerialization
{
	// Token: 0x020000D9 RID: 217
	internal class ButtonIconConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001EC8E File Offset: 0x0001CE8E
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0001EC96 File Offset: 0x0001CE96
		public Func<string, string> ResolveUrl { get; set; }

		// Token: 0x06000840 RID: 2112 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ButtonIcon buttonIcon = obj as ButtonIcon;
			if (this.ResolveUrl == null)
			{
				throw new MissingMemberException("Please, make sure ResolveUrl resolver method is defined.");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "primaryIconUrl", this.ResolveUrl(buttonIcon.Url), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryHoveredIconUrl", this.ResolveUrl(buttonIcon.HoveredUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryPressedIconUrl", this.ResolveUrl(buttonIcon.PressedUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryHeight", buttonIcon.Height.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryWidth", buttonIcon.Width.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryTop", buttonIcon.Top.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryLeft", buttonIcon.Left.ToString(CultureInfo.InvariantCulture), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryCssClass", buttonIcon.CssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryHoveredCssClass", buttonIcon.HoveredCssClass, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "primaryPressedCssClass", buttonIcon.PressedCssClass, string.Empty);
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0001EDFC File Offset: 0x0001CFFC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ButtonIcon)
				};
			}
		}
	}
}
