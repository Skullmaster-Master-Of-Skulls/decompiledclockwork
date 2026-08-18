using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x0200060A RID: 1546
	public class AnimationConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600384F RID: 14415 RVA: 0x000B961C File Offset: 0x000B781C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Animation animation = obj as Animation;
			ExplicitJavaScriptConverter.AddProperty(state, "close", animation.CloseSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "open", animation.OpenSettings, null);
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000B9654 File Offset: 0x000B7854
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
