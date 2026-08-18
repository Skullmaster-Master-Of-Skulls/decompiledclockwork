using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x0200022F RID: 559
	public class DiagramConnectionEditableToolConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x000476B4 File Offset: 0x000458B4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramConnectionEditableTool diagramConnectionEditableTool = obj as DiagramConnectionEditableTool;
			ExplicitJavaScriptConverter.AddProperty(state, "name", diagramConnectionEditableTool.Name, "");
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000476E0 File Offset: 0x000458E0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramConnectionEditableTool)
				};
			}
		}
	}
}
