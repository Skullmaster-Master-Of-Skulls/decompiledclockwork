using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020005EE RID: 1518
	public class MultiColumnComboBoxColumnConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003707 RID: 14087 RVA: 0x000B6210 File Offset: 0x000B4410
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			MultiColumnComboBoxColumn multiColumnComboBoxColumn = obj as MultiColumnComboBoxColumn;
			ExplicitJavaScriptConverter.AddProperty(state, "field", multiColumnComboBoxColumn.Field, "");
			ExplicitJavaScriptConverter.AddProperty(state, "title", multiColumnComboBoxColumn.Title, "");
			if (multiColumnComboBoxColumn.Template.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", multiColumnComboBoxColumn.Template.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "template", multiColumnComboBoxColumn.Template, "");
			}
			if (multiColumnComboBoxColumn.HeaderTemplate.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "headerTemplate", multiColumnComboBoxColumn.HeaderTemplate.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "headerTemplate", multiColumnComboBoxColumn.HeaderTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "width", multiColumnComboBoxColumn.Width, "");
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000B6304 File Offset: 0x000B4504
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(MultiColumnComboBoxColumn)
				};
			}
		}
	}
}
