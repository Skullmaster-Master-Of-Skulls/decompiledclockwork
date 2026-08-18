using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000593 RID: 1427
	public class CloseConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003347 RID: 13127 RVA: 0x000AADF8 File Offset: 0x000A8FF8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Close close = obj as Close;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", close.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", close.Duration, 0.0);
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000AAE48 File Offset: 0x000A9048
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Close)
				};
			}
		}
	}
}
