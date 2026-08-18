using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x02000087 RID: 135
	public class ExpandConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0000D3F0 File Offset: 0x0000B5F0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Expand expand = obj as Expand;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", expand.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", expand.Duration, 0.0);
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000D440 File Offset: 0x0000B640
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Expand)
				};
			}
		}
	}
}
