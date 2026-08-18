using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000232 RID: 562
	public class DiagramConnectionPointConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060014AB RID: 5291 RVA: 0x000477A4 File Offset: 0x000459A4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramConnectionPoint diagramConnectionPoint = obj as DiagramConnectionPoint;
			ExplicitJavaScriptConverter.AddProperty(state, "x", diagramConnectionPoint.X, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "y", diagramConnectionPoint.Y, 0.0);
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00047800 File Offset: 0x00045A00
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramConnectionPoint)
				};
			}
		}
	}
}
