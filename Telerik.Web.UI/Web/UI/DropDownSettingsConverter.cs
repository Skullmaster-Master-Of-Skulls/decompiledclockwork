using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200046A RID: 1130
	internal class DropDownSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06002884 RID: 10372 RVA: 0x000833C0 File Offset: 0x000815C0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			DropDownSettings dropDownSettings = obj as DropDownSettings;
			if (dropDownSettings == null)
			{
				throw new InvalidOperationException("Can serialize only DropDownSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "autoWidth", dropDownSettings.AutoWidth, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "closeDropDownOnSelection", dropDownSettings.CloseDropDownOnSelection, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "openDropDownOnLoad", dropDownSettings.OpenDropDownOnLoad, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "width", dropDownSettings.Width.ToString(CultureInfo.InvariantCulture), false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "height", dropDownSettings.Height.ToString(CultureInfo.InvariantCulture), false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "cssClass", dropDownSettings.CssClass, "");
			return dictionary;
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x0008349C File Offset: 0x0008169C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DropDownSettings)
				};
			}
		}
	}
}
