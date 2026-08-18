using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x0200007D RID: 125
	public class AnimationConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Animation animation = obj as Animation;
			ExplicitJavaScriptConverter.AddProperty(state, "collapse", animation.CollapseSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "expand", animation.ExpandSettings, null);
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
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
