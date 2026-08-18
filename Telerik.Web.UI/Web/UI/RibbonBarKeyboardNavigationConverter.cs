using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E50 RID: 3664
	internal class RibbonBarKeyboardNavigationConverter : JavaScriptConverter
	{
		// Token: 0x06008B04 RID: 35588 RVA: 0x001FA905 File Offset: 0x001F8B05
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008B05 RID: 35589 RVA: 0x001FA90C File Offset: 0x001F8B0C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RibbonBarKeyboardNavigationSettings ribbonBarKeyboardNavigationSettings = obj as RibbonBarKeyboardNavigationSettings;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["CommandKey"] = ribbonBarKeyboardNavigationSettings.CommandKey;
			dictionary["FocusKey"] = ribbonBarKeyboardNavigationSettings.FocusKey;
			dictionary["Activated"] = ribbonBarKeyboardNavigationSettings.Activated;
			return dictionary;
		}

		// Token: 0x17002BEE RID: 11246
		// (get) Token: 0x06008B06 RID: 35590 RVA: 0x001FAA38 File Offset: 0x001F8C38
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarKeyboardNavigationSettings);
				yield break;
			}
		}
	}
}
