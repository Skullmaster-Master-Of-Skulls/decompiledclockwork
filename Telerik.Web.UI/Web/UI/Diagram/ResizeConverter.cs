using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002AA RID: 682
	public class ResizeConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001819 RID: 6169 RVA: 0x0004FD10 File Offset: 0x0004DF10
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Resize resize = obj as Resize;
			ExplicitJavaScriptConverter.AddProperty(state, "handles", resize.HandlesSettings, null);
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0004FD38 File Offset: 0x0004DF38
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Resize)
				};
			}
		}
	}
}
