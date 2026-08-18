using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A2E RID: 6702
	internal class AdvancedFormSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0601044D RID: 66637 RVA: 0x003A2B3C File Offset: 0x003A0D3C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AdvancedFormSettings advancedFormSettings = obj as AdvancedFormSettings;
			if (advancedFormSettings == null)
			{
				throw new InvalidOperationException("Can serialize only AdvancedFormSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enabled", advancedFormSettings.Enabled, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "modal", advancedFormSettings.Modal, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "zIndex", advancedFormSettings.ZIndex, 2500);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "maxHeight", advancedFormSettings.MaximumHeight.ToString(), "550px");
			ExplicitJavaScriptConverter.AddProperty(dictionary, "width", advancedFormSettings.Width.ToString(), "700px");
			return dictionary;
		}

		// Token: 0x17004ED2 RID: 20178
		// (get) Token: 0x0601044E RID: 66638 RVA: 0x003A2C08 File Offset: 0x003A0E08
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(AdvancedFormSettings)
				};
			}
		}
	}
}
