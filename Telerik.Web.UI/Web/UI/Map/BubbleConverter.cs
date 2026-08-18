using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200043D RID: 1085
	public class BubbleConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060026EB RID: 9963 RVA: 0x0007EC28 File Offset: 0x0007CE28
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Bubble bubble = obj as Bubble;
			ExplicitJavaScriptConverter.AddProperty(state, "attribution", bubble.Attribution, "");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", bubble.Opacity, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "maxSize", bubble.MaxSize, 100.0);
			ExplicitJavaScriptConverter.AddProperty(state, "minSize", bubble.MinSize, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "style", bubble.StyleSettings, null);
			if (bubble.Symbol.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "symbol", bubble.Symbol.Substring(11).TrimStart(new char[0]));
				return;
			}
			ExplicitJavaScriptConverter.AddProperty(state, "symbol", bubble.Symbol, "circle");
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060026EC RID: 9964 RVA: 0x0007ED20 File Offset: 0x0007CF20
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Bubble)
				};
			}
		}
	}
}
