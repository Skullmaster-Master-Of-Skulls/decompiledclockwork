using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x0200060C RID: 1548
	public class CloseConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003858 RID: 14424 RVA: 0x000B9724 File Offset: 0x000B7924
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Close close = obj as Close;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", close.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", close.Duration, 100.0);
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000B9774 File Offset: 0x000B7974
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
