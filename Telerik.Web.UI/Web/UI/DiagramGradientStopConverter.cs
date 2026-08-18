using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200023B RID: 571
	public class DiagramGradientStopConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060014DC RID: 5340 RVA: 0x00047F48 File Offset: 0x00046148
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramGradientStop diagramGradientStop = obj as DiagramGradientStop;
			ExplicitJavaScriptConverter.AddProperty(state, "offset", diagramGradientStop.Offset, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "color", diagramGradientStop.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", diagramGradientStop.Opacity, 0.0);
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00047FBC File Offset: 0x000461BC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramGradientStop)
				};
			}
		}
	}
}
