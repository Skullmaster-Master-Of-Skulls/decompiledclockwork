using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Drawer
{
	// Token: 0x02000041 RID: 65
	public class MiniConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000215 RID: 533 RVA: 0x00005B90 File Offset: 0x00003D90
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Mini mini = obj as Mini;
			ExplicitJavaScriptConverter.AddProperty(state, "width", mini.Width, 0.0);
			if (mini.MiniTemplate.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", mini.MiniTemplate.Substring(11).TrimStart(new char[0]));
				return;
			}
			ExplicitJavaScriptConverter.AddProperty(state, "template", mini.MiniTemplate, "");
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00005C18 File Offset: 0x00003E18
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Mini)
				};
			}
		}
	}
}
