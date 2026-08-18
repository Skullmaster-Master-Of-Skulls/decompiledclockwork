using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200026A RID: 618
	public class StartCapConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600165D RID: 5725 RVA: 0x0004BFBC File Offset: 0x0004A1BC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			StartCap startCap = obj as StartCap;
			ExplicitJavaScriptConverter.AddProperty(state, "fill", startCap.Fill, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fill", startCap.FillSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", startCap.Stroke, "");
			ExplicitJavaScriptConverter.AddProperty(state, "stroke", startCap.StrokeSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "type", startCap.Type.ToString(), ConnectionStartCap.None.ToString());
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0004C048 File Offset: 0x0004A248
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(StartCap)
				};
			}
		}
	}
}
