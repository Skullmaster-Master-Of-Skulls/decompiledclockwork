using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002AE RID: 686
	public class SelectableConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001832 RID: 6194 RVA: 0x00050028 File Offset: 0x0004E228
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Selectable selectable = obj as Selectable;
			ExplicitJavaScriptConverter.AddProperty(state, "multiple", selectable.Multiple, true);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", selectable.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "key", StringHelpers.ToCamelCase(selectable.Key.ToString()), StringHelpers.ToCamelCase(ModifierKey.None.ToString()));
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0005009C File Offset: 0x0004E29C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Selectable)
				};
			}
		}
	}
}
