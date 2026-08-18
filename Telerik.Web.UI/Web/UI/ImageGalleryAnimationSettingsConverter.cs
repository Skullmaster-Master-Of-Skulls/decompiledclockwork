using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000540 RID: 1344
	internal class ImageGalleryAnimationSettingsConverter : JavaScriptConverter
	{
		// Token: 0x06002F7F RID: 12159 RVA: 0x0009B709 File Offset: 0x00099909
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x0009B710 File Offset: 0x00099910
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ImageGalleryAnimationSettings imageGalleryAnimationSettings = (ImageGalleryAnimationSettings)obj;
			dictionary.Add("slideDuration", imageGalleryAnimationSettings.SlideshowSlideDuration);
			if (!this.IsAnimationDefault(imageGalleryAnimationSettings.NextImagesAnimation))
			{
				dictionary.Add("nextImagesAnimation", this.SerializeAnimationSetting(imageGalleryAnimationSettings.NextImagesAnimation));
			}
			if (!this.IsAnimationDefault(imageGalleryAnimationSettings.PrevImagesAnimation))
			{
				dictionary.Add("prevImagesAnimation", this.SerializeAnimationSetting(imageGalleryAnimationSettings.PrevImagesAnimation));
			}
			return dictionary;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x0009B78C File Offset: 0x0009998C
		private Dictionary<string, object> SerializeAnimationSetting(ImageGalleryAnimationSetting setting)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("type", setting.Type.ToString());
			dictionary.Add("speed", setting.Speed);
			string text = setting.Easing.ToString();
			dictionary.Add("easing", text[0].ToString().ToLower() + text.Substring(1));
			return dictionary;
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x0009B8DC File Offset: 0x00099ADC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(ImageGalleryAnimationSettings);
				yield break;
			}
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x0009B8F9 File Offset: 0x00099AF9
		private bool IsAnimationDefault(ImageGalleryAnimationSetting setting)
		{
			return false;
		}
	}
}
