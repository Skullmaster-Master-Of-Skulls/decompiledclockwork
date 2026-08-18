using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005E6 RID: 1510
	public class AnimationConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060036B6 RID: 14006 RVA: 0x000B55E0 File Offset: 0x000B37E0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Animation animation = obj as Animation;
			ExplicitJavaScriptConverter.AddProperty(state, "close", animation.CloseSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "open", animation.OpenSettings, null);
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x000B5618 File Offset: 0x000B3818
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
