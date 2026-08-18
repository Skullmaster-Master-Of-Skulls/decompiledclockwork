using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200053E RID: 1342
	internal class ImageGalleryImageAreaSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06002F77 RID: 12151 RVA: 0x0009B449 File Offset: 0x00099649
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x0009B450 File Offset: 0x00099650
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ImageGalleryImageAreaSettings imageGalleryImageAreaSettings = (ImageGalleryImageAreaSettings)obj;
			dictionary.Add("width", imageGalleryImageAreaSettings.Width.Value);
			dictionary.Add("height", imageGalleryImageAreaSettings.Height.Value);
			if (imageGalleryImageAreaSettings.NavigationMode == ImageGalleryNavigationMode.Zone)
			{
				dictionary.Add("isZoneNavigation", true);
			}
			if (imageGalleryImageAreaSettings.ResizeMode != ImageGalleryResizeMode.Fit)
			{
				dictionary.Add("resizeMode", imageGalleryImageAreaSettings.ResizeMode);
			}
			return dictionary;
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x0009B5AC File Offset: 0x000997AC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ImageGalleryImageAreaSettings);
				yield break;
			}
		}
	}
}
