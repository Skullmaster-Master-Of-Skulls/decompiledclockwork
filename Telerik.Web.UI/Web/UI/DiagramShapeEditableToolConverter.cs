using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000249 RID: 585
	public class DiagramShapeEditableToolConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x00049A80 File Offset: 0x00047C80
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramShapeEditableTool diagramShapeEditableTool = obj as DiagramShapeEditableTool;
			ExplicitJavaScriptConverter.AddProperty(state, "name", diagramShapeEditableTool.Name, "");
			ExplicitJavaScriptConverter.AddProperty(state, "step", diagramShapeEditableTool.Step, 90.0);
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00049AD0 File Offset: 0x00047CD0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramShapeEditableTool)
				};
			}
		}
	}
}
