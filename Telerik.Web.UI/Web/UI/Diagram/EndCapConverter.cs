using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000251 RID: 593
	public class EndCapConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x00049FB4 File Offset: 0x000481B4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			EndCap endCap = obj as EndCap;
			ExplicitJavaScriptConverter.AddProperty(state, "fill", endCap.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", endCap.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", endCap.Stroke, "");
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", endCap.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "type", endCap.Type.ToString(), ConnectionEndCap.None.ToString());
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0004A040 File Offset: 0x00048240
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EndCap)
				};
			}
		}
	}
}
