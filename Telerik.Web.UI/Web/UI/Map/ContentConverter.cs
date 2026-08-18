using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000595 RID: 1429
	public class ContentConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600334E RID: 13134 RVA: 0x000AAEC0 File Offset: 0x000A90C0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Content content = obj as Content;
			ExplicitJavaScriptConverter.AddProperty(state, "url", content.Url, "");
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000AAEEC File Offset: 0x000A90EC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Content)
				};
			}
		}
	}
}
