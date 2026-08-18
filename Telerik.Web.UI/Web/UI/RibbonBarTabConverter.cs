using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F2E RID: 3886
	internal class RibbonBarTabConverter : JavaScriptConverter
	{
		// Token: 0x06009425 RID: 37925 RVA: 0x002139E9 File Offset: 0x00211BE9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009426 RID: 37926 RVA: 0x002139F0 File Offset: 0x00211BF0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarTab ribbonBarTab = (RibbonBarTab)obj;
			if (!string.IsNullOrEmpty(ribbonBarTab.Value))
			{
				dictionary["value"] = ribbonBarTab.Value;
			}
			return dictionary;
		}

		// Token: 0x17002ED7 RID: 11991
		// (get) Token: 0x06009427 RID: 37927 RVA: 0x00213AF8 File Offset: 0x00211CF8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarTab);
				yield break;
			}
		}
	}
}
