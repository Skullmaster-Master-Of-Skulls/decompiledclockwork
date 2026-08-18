using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000048 RID: 72
	public class TasksTooltipConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600024B RID: 587 RVA: 0x000065C8 File Offset: 0x000047C8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			TasksTooltip tasksTooltip = obj as TasksTooltip;
			string text = "javascript:";
			if (tasksTooltip.ClientTemplate.StartsWith(text, StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", tasksTooltip.ClientTemplate.Substring(text.Length).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "template", tasksTooltip.ClientTemplate, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "visible", tasksTooltip.Visible, true);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00006650 File Offset: 0x00004850
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TasksTooltip)
				};
			}
		}
	}
}
