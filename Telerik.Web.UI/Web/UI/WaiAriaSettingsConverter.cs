using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020000D2 RID: 210
	internal class WaiAriaSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x0001E360 File Offset: 0x0001C560
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			WaiAriaSettings waiAriaSettings = obj as WaiAriaSettings;
			if (waiAriaSettings == null)
			{
				throw new InvalidOperationException("Can serialize only WaiAriaSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "aria-describedby", waiAriaSettings.DescribedBy, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "aria-label", waiAriaSettings.Label, string.Empty);
			return dictionary;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(WaiAriaSettings)
				};
			}
		}
	}
}
