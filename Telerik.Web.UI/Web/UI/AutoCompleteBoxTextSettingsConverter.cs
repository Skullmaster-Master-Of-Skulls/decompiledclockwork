using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020009B1 RID: 2481
	internal class AutoCompleteBoxTextSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06005F10 RID: 24336 RVA: 0x001221F8 File Offset: 0x001203F8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AutoCompleteBoxTextSettings autoCompleteBoxTextSettings = obj as AutoCompleteBoxTextSettings;
			if (autoCompleteBoxTextSettings == null)
			{
				throw new InvalidOperationException("Can serialize only AutoCompleteBoxTextSettings objects.");
			}
			return new Dictionary<string, object>
			{
				{
					"selectionMode",
					autoCompleteBoxTextSettings.SelectionMode
				}
			};
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x00122238 File Offset: 0x00120438
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(AutoCompleteBoxTextSettings)
				};
			}
		}
	}
}
