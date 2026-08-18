using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002AC RID: 684
	public class RotateConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x0004FE80 File Offset: 0x0004E080
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Rotate rotate = obj as Rotate;
			ExplicitJavaScriptConverter.AddProperty(state, "fill", rotate.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", rotate.StrokeSettings, null);
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x0004FEB8 File Offset: 0x0004E0B8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Rotate)
				};
			}
		}
	}
}
