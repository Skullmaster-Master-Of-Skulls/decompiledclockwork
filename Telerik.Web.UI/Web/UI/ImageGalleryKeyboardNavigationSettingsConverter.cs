using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000541 RID: 1345
	internal class ImageGalleryKeyboardNavigationSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06002F85 RID: 12165 RVA: 0x0009B904 File Offset: 0x00099B04
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x0009B90C File Offset: 0x00099B0C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ImageGalleryKeyboardNavigationSettings imageGalleryKeyboardNavigationSettings = (ImageGalleryKeyboardNavigationSettings)obj;
			if (!imageGalleryKeyboardNavigationSettings.AllowCycle)
			{
				dictionary.Add("allowCycle", false);
			}
			dictionary.Add("shortcuts", imageGalleryKeyboardNavigationSettings.Shortcuts);
			return dictionary;
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x0009BA20 File Offset: 0x00099C20
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ImageGalleryKeyboardNavigationSettings);
				yield break;
			}
		}
	}
}
