using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B2 RID: 1458
	public class StrokeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003414 RID: 13332 RVA: 0x000ACE14 File Offset: 0x000AB014
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Stroke stroke = obj as Stroke;
			ExplicitJavaScriptConverter.AddProperty(state, "color", stroke.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "dashType", stroke.DashType, "solid");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", stroke.Opacity, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "width", stroke.Width, 1.0);
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x000ACE9C File Offset: 0x000AB09C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Stroke)
				};
			}
		}
	}
}
