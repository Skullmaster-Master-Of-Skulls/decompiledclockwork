using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000262 RID: 610
	public class PannableConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060015FC RID: 5628 RVA: 0x0004AD78 File Offset: 0x00048F78
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Pannable pannable = obj as Pannable;
			ExplicitJavaScriptConverter.AddProperty(state, "key", StringHelpers.ToCamelCase(pannable.Key.ToString()), StringHelpers.ToCamelCase(ModifierKey.Ctrl.ToString()));
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0004ADBC File Offset: 0x00048FBC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Pannable)
				};
			}
		}
	}
}
