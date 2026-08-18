using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005EA RID: 1514
	public class CloseConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060036CE RID: 14030 RVA: 0x000B59F8 File Offset: 0x000B3BF8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Close close = obj as Close;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", close.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", close.Duration, 100.0);
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x060036CF RID: 14031 RVA: 0x000B5A48 File Offset: 0x000B3C48
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
