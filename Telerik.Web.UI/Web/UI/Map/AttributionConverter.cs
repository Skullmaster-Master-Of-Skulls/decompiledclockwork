using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200058E RID: 1422
	public class AttributionConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600332F RID: 13103 RVA: 0x000AAAAC File Offset: 0x000A8CAC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Attribution attribution = obj as Attribution;
			ExplicitJavaScriptConverter.AddProperty(state, "position", StringHelpers.ToCamelCase(attribution.Position.ToString()), StringHelpers.ToCamelCase(AttributionPosition.BottomRight.ToString()));
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x000AAAF0 File Offset: 0x000A8CF0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Attribution)
				};
			}
		}
	}
}
