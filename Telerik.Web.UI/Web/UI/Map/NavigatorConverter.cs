using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005A9 RID: 1449
	public class NavigatorConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060033E6 RID: 13286 RVA: 0x000AC5F8 File Offset: 0x000AA7F8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Navigator navigator = obj as Navigator;
			ExplicitJavaScriptConverter.AddProperty(state, "position", StringHelpers.ToCamelCase(navigator.Position.ToString()), StringHelpers.ToCamelCase(NavigatorPosition.TopLeft.ToString()));
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x000AC63C File Offset: 0x000AA83C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Navigator)
				};
			}
		}
	}
}
