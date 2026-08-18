using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B3E RID: 6974
	internal class AnimationSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06010DC8 RID: 69064 RVA: 0x003BDA6C File Offset: 0x003BBC6C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AnimationSettings animationSettings = obj as AnimationSettings;
			if (animationSettings == null)
			{
				throw new InvalidOperationException("Can serialize only AnimationSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "type", animationSettings.Type, AnimationType.OutQuart);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "duration", animationSettings.Duration, 300);
			return dictionary;
		}

		// Token: 0x17005232 RID: 21042
		// (get) Token: 0x06010DC9 RID: 69065 RVA: 0x003BDAD4 File Offset: 0x003BBCD4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(AnimationSettings)
				};
			}
		}
	}
}
