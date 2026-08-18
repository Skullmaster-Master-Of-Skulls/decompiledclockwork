using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000779 RID: 1913
	public class ProgressBarAnimationSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06004396 RID: 17302 RVA: 0x000D37C4 File Offset: 0x000D19C4
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ProgressBarAnimationSettings progressBarAnimationSettings = obj as ProgressBarAnimationSettings;
			ExplicitJavaScriptConverter.AddProperty(state, "duration", progressBarAnimationSettings.Duration, 400);
			ExplicitJavaScriptConverter.AddProperty(state, "enableChunkAnimation", progressBarAnimationSettings.EnableChunkAnimation, false);
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x000D3814 File Offset: 0x000D1A14
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ProgressBarAnimationSettings)
				};
			}
		}
	}
}
