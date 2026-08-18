using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200053D RID: 1341
	internal class ImageGalleryThumbnailsAreaSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06002F73 RID: 12147 RVA: 0x0009B1F5 File Offset: 0x000993F5
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x0009B1FC File Offset: 0x000993FC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ImageGalleryThumbnailsAreaSettings imageGalleryThumbnailsAreaSettings = (ImageGalleryThumbnailsAreaSettings)obj;
			dictionary.Add("width", imageGalleryThumbnailsAreaSettings.Width.ToString());
			dictionary.Add("height", imageGalleryThumbnailsAreaSettings.Height.ToString());
			dictionary.Add("thumbnailWidth", imageGalleryThumbnailsAreaSettings.ThumbnailWidth.ToString());
			dictionary.Add("thumbnailHeight", imageGalleryThumbnailsAreaSettings.ThumbnailHeight.ToString());
			if (imageGalleryThumbnailsAreaSettings.Mode == ImageGalleryThumbnailsAreaMode.Thumbnails && imageGalleryThumbnailsAreaSettings.Position != ImageGalleryThumbnailsAreaPosition.Bottom && imageGalleryThumbnailsAreaSettings.Position != ImageGalleryThumbnailsAreaPosition.Top)
			{
				dictionary.Add("position", imageGalleryThumbnailsAreaSettings.Position.ToString());
			}
			dictionary.Add("mode", imageGalleryThumbnailsAreaSettings.Mode);
			if (imageGalleryThumbnailsAreaSettings.EnableZoneScroll)
			{
				dictionary.Add("enableZoneScroll", imageGalleryThumbnailsAreaSettings.EnableZoneScroll);
			}
			if (imageGalleryThumbnailsAreaSettings.ShowScrollButtons && imageGalleryThumbnailsAreaSettings.ScrollButtonsTrigger != ImageGalleryScrollButtonsTrigger.Click)
			{
				dictionary.Add("scrollButtonsTrigger", imageGalleryThumbnailsAreaSettings.ScrollButtonsTrigger);
			}
			if (imageGalleryThumbnailsAreaSettings.ScrollOrientation == ImageGalleryScrollOrientation.Vertical)
			{
				dictionary.Add("isVertical", true);
			}
			if (imageGalleryThumbnailsAreaSettings.ShowScrollbar)
			{
				dictionary.Add("showScrollbar", true);
			}
			return dictionary;
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x0009B424 File Offset: 0x00099624
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ImageGalleryThumbnailsAreaSettings);
				yield break;
			}
		}
	}
}
