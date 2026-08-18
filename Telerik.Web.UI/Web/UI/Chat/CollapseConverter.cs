using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x02000085 RID: 133
	public class CollapseConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Collapse collapse = obj as Collapse;
			ExplicitJavaScriptConverter.AddProperty(state, "effects", collapse.Effects, "");
			ExplicitJavaScriptConverter.AddProperty(state, "duration", collapse.Duration, 0.0);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000D320 File Offset: 0x0000B520
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Collapse)
				};
			}
		}
	}
}
