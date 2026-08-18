using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005AC RID: 1452
	public class OpenConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060033EF RID: 13295 RVA: 0x000AC70C File Offset: 0x000AA90C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Open open = obj as Open;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", open.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", open.Duration, 0.0);
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x000AC75C File Offset: 0x000AA95C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Open)
				};
			}
		}
	}
}
