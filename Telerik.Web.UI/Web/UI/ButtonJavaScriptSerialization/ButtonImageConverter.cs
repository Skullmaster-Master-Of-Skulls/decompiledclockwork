using System;
using System.Collections.Generic;
using Telerik.Web.UI.ButtonBase;

namespace Telerik.Web.UI.ButtonJavaScriptSerialization
{
	// Token: 0x020000D8 RID: 216
	internal class ButtonImageConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001EB8A File Offset: 0x0001CD8A
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0001EB92 File Offset: 0x0001CD92
		public Func<string, string> ResolveUrl { get; set; }

		// Token: 0x0600083B RID: 2107 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ButtonImage buttonImage = obj as ButtonImage;
			if (this.ResolveUrl == null)
			{
				throw new MissingMemberException("Please, make sure ResolveUrl resolver method is defined.");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "imageUrl", this.ResolveUrl(buttonImage.Url), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "imageSizing", buttonImage.Sizing, ImageSizing.Original);
			ExplicitJavaScriptConverter.AddProperty(state, "disabledImageUrl", this.ResolveUrl(buttonImage.DisabledUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "hoveredImageUrl", this.ResolveUrl(buttonImage.HoveredUrl), string.Empty);
			ExplicitJavaScriptConverter.AddProperty(state, "pressedImageUrl", this.ResolveUrl(buttonImage.PressedUrl), string.Empty);
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x0001EC64 File Offset: 0x0001CE64
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ButtonImage)
				};
			}
		}
	}
}
