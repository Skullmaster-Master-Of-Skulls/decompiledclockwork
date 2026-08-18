using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000047 RID: 71
	public class GanttToolbarItemConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000248 RID: 584 RVA: 0x000064FC File Offset: 0x000046FC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			GanttToolbarItem ganttToolbarItem = obj as GanttToolbarItem;
			ExplicitJavaScriptConverter.AddProperty(state, "name", ganttToolbarItem.Name, "");
			if (ganttToolbarItem.ClientTemplate.TrimStart(new char[0]).StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", ganttToolbarItem.ClientTemplate.TrimStart(new char[0]).Substring(11).Trim());
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "template", ganttToolbarItem.ClientTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "text", ganttToolbarItem.Text, "");
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000659C File Offset: 0x0000479C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(GanttToolbarItem)
				};
			}
		}
	}
}
