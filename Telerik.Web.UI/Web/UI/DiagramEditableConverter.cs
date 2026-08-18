using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000236 RID: 566
	public class DiagramEditableConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x00047BC4 File Offset: 0x00045DC4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			DiagramEditable diagramEditable = obj as DiagramEditable;
			if (diagramEditable.ConnectionTemplate.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "connectionTemplate", diagramEditable.ConnectionTemplate.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "connectionTemplate", diagramEditable.ConnectionTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "drag", diagramEditable.Drag, true);
			if (diagramEditable.Drag)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "drag", diagramEditable.DragSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "remove", diagramEditable.Remove, true);
			ExplicitJavaScriptConverter.AddProperty(state, "resize", diagramEditable.Resize, true);
			if (diagramEditable.Resize)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "resize", diagramEditable.ResizeSettings, null);
			}
			ExplicitJavaScriptConverter.AddProperty(state, "rotate", diagramEditable.Rotate, true);
			if (diagramEditable.Rotate)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "rotate", diagramEditable.RotateSettings, null);
			}
			if (diagramEditable.ShapeTemplate.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "shapeTemplate", diagramEditable.ShapeTemplate.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "shapeTemplate", diagramEditable.ShapeTemplate, "");
			}
			if (diagramEditable.ToolsCollection.Count != 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, "tools", diagramEditable.ToolsCollection.ItemsList, null);
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00047D58 File Offset: 0x00045F58
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DiagramEditable)
				};
			}
		}
	}
}
