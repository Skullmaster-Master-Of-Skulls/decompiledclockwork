using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200053C RID: 1340
	internal class ImageGalleryItemConverter : JavaScriptConverter
	{
		// Token: 0x06002F6F RID: 12143 RVA: 0x0009AF8A File Offset: 0x0009918A
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x0009AF94 File Offset: 0x00099194
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ImageGalleryItemBase imageGalleryItemBase = (ImageGalleryItemBase)obj;
			if (!imageGalleryItemBase.Width.IsEmpty)
			{
				dictionary.Add("width", imageGalleryItemBase.Width.ToString());
			}
			if (!imageGalleryItemBase.Height.IsEmpty)
			{
				dictionary.Add("height", imageGalleryItemBase.Height.ToString());
			}
			if (!string.IsNullOrEmpty(imageGalleryItemBase.Title))
			{
				dictionary.Add("title", imageGalleryItemBase.Title);
			}
			if (!string.IsNullOrEmpty(imageGalleryItemBase.Description))
			{
				dictionary.Add("description", imageGalleryItemBase.Description);
			}
			if (imageGalleryItemBase.PreventDefaultGestures)
			{
				dictionary.Add("preventDefaultGestures", imageGalleryItemBase.PreventDefaultGestures);
			}
			ImageGalleryItem imageGalleryItem = obj as ImageGalleryItem;
			if (imageGalleryItem != null)
			{
				dictionary.Add("thumbnailUrl", imageGalleryItem.ThumbnailUrl);
				dictionary.Add("imageUrl", imageGalleryItem.GetImageUrl());
				if (!string.IsNullOrEmpty(imageGalleryItem.NavigateUrl))
				{
					dictionary.Add("navigateUrl", imageGalleryItem.NavigateUrl);
				}
			}
			ImageGalleryTemplateItem imageGalleryTemplateItem = obj as ImageGalleryTemplateItem;
			if (imageGalleryTemplateItem != null)
			{
				dictionary.Add("type", imageGalleryItemBase.Type);
				if (imageGalleryItemBase.Gallery.Items[imageGalleryItemBase.Gallery.CurrentItemIndex] == imageGalleryTemplateItem)
				{
					dictionary.Add("loaded", true);
				}
			}
			return dictionary;
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x0009B1D0 File Offset: 0x000993D0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ImageGalleryItemBase);
				yield break;
			}
		}
	}
}
