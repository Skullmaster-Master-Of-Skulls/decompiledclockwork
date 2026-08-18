using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000A26 RID: 2598
	internal class RadButtonImageConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x0600625F RID: 25183 RVA: 0x00173042 File Offset: 0x00171242
		// (set) Token: 0x06006260 RID: 25184 RVA: 0x0017304A File Offset: 0x0017124A
		public Control ParentButton { get; set; }

		// Token: 0x06006261 RID: 25185 RVA: 0x00173054 File Offset: 0x00171254
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadButtonImage radButtonImage = obj as RadButtonImage;
			if (radButtonImage == null)
			{
				throw new InvalidOperationException("Can serialize only RadButtonImage objects.");
			}
			Control parentButton = this.ParentButton;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string imageUrl = radButtonImage.ImageUrl;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "imageUrl", parentButton.ResolveUrl(imageUrl), string.Empty);
			}
			string disabledImageUrl = radButtonImage.DisabledImageUrl;
			if (!string.IsNullOrEmpty(disabledImageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledImageUrl", parentButton.ResolveUrl(disabledImageUrl), string.Empty);
			}
			string hoveredImageUrl = radButtonImage.HoveredImageUrl;
			if (!string.IsNullOrEmpty(hoveredImageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredImageUrl", parentButton.ResolveUrl(hoveredImageUrl), string.Empty);
			}
			string pressedImageUrl = radButtonImage.PressedImageUrl;
			if (!string.IsNullOrEmpty(pressedImageUrl))
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary, "pressedImageUrl", parentButton.ResolveUrl(pressedImageUrl), string.Empty);
			}
			return dictionary;
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x06006262 RID: 25186 RVA: 0x00173128 File Offset: 0x00171328
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(RadButtonImage)
				};
			}
		}
	}
}
