using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200053F RID: 1343
	internal class ImageGalleryToolbarSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06002F7B RID: 12155 RVA: 0x0009B5D1 File Offset: 0x000997D1
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x0009B5D8 File Offset: 0x000997D8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ImageGalleryToolbarSettings imageGalleryToolbarSettings = (ImageGalleryToolbarSettings)obj;
			if (imageGalleryToolbarSettings.ItemsCounterFormat != "Item {0} of {1}")
			{
				dictionary.Add("itemsCounterFormat", imageGalleryToolbarSettings.ItemsCounterFormat);
			}
			return dictionary;
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x0009B6E4 File Offset: 0x000998E4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ImageGalleryToolbarSettings);
				yield break;
			}
		}
	}
}
