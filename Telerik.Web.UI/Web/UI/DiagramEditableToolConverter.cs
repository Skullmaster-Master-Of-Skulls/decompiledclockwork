using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000238 RID: 568
	public class DiagramEditableToolConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x00047E00 File Offset: 0x00046000
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramEditableTool diagramEditableTool = obj as DiagramEditableTool;
			ExplicitJavaScriptConverter.AddProperty(state, "name", diagramEditableTool.Name, "");
			ExplicitJavaScriptConverter.AddProperty(state, "step", diagramEditableTool.Step, 90.0);
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00047E50 File Offset: 0x00046050
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramEditableTool)
				};
			}
		}
	}
}
