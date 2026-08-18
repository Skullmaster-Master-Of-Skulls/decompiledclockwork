using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020009AF RID: 2479
	internal class AutoCompleteBoxTokensSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06005F08 RID: 24328 RVA: 0x001220F0 File Offset: 0x001202F0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AutoCompleteBoxTokensSettings autoCompleteBoxTokensSettings = obj as AutoCompleteBoxTokensSettings;
			if (autoCompleteBoxTokensSettings == null)
			{
				throw new InvalidOperationException("Can serialize only AutoCompleteBoxTokensSettings objects.");
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "allowTokenEditing", autoCompleteBoxTokensSettings.AllowTokenEditing, false);
			return dictionary;
		}

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x00122138 File Offset: 0x00120338
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(AutoCompleteBoxTokensSettings)
				};
			}
		}
	}
}
