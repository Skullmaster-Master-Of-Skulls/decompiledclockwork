using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020000C3 RID: 195
	internal class KeyboardNavigationSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x0001C8C4 File Offset: 0x0001AAC4
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			KeyboardNavigationSettings keyboardNavigationSettings = obj as KeyboardNavigationSettings;
			if (keyboardNavigationSettings == null)
			{
				throw new InvalidOperationException("Can serialize only KeyboardNavigationSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "focusKey", keyboardNavigationSettings.FocusKey, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "commandKey", keyboardNavigationSettings.CommandKey, string.Empty);
			return dictionary;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001C924 File Offset: 0x0001AB24
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(KeyboardNavigationSettings)
				};
			}
		}
	}
}
