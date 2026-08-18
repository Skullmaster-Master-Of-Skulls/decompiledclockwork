using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002B2 RID: 690
	public class ShapeEditableConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001848 RID: 6216 RVA: 0x000502E0 File Offset: 0x0004E4E0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ShapeEditable shapeEditable = obj as ShapeEditable;
			ExplicitJavaScriptConverter.AddProperty(state, "connect", shapeEditable.Connect, false);
			if (shapeEditable.ToolsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "tools", shapeEditable.ToolsCollection.ItemsList, null);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00050334 File Offset: 0x0004E534
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ShapeEditable)
				};
			}
		}
	}
}
