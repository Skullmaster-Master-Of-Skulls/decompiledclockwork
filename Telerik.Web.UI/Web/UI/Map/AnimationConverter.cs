using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200058C RID: 1420
	public class AnimationConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003328 RID: 13096 RVA: 0x000AA9FC File Offset: 0x000A8BFC
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Animation animation = obj as Animation;
			ExplicitJavaScriptConverter.AddProperty(state, "close", animation.CloseSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "open", animation.OpenSettings, null);
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000AAA34 File Offset: 0x000A8C34
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Animation)
				};
			}
		}
	}
}
