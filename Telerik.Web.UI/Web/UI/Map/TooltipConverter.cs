using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B8 RID: 1464
	public class TooltipConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600344B RID: 13387 RVA: 0x000AD60C File Offset: 0x000AB80C
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Tooltip tooltip = obj as Tooltip;
			ExplicitJavaScriptConverter.AddProperty(state, "autoHide", tooltip.AutoHide, true);
			ExplicitJavaScriptConverter.AddProperty(state, "animation", tooltip.AnimationSettings, null);
			if (tooltip.Content.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "content", tooltip.Content.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "content", tooltip.Content, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "content", tooltip.ContentSettings, null);
			ExplicitJavaScriptConverter.AddProperty(state, "template", tooltip.Template, "");
			ExplicitJavaScriptConverter.AddProperty(state, "callout", tooltip.Callout, true);
			ExplicitJavaScriptConverter.AddProperty(state, "iframe", tooltip.Iframe, false);
			ExplicitJavaScriptConverter.AddProperty(state, "height", tooltip.Height, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "width", tooltip.Width, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "position", StringHelpers.ToCamelCase(tooltip.Position.ToString()), StringHelpers.ToCamelCase(TooltipPosition.Top.ToString()));
			ExplicitJavaScriptConverter.AddProperty(state, "showAfter", tooltip.ShowAfter, 100.0);
			ExplicitJavaScriptConverter.AddProperty(state, "showOn", tooltip.ShowOn, "mouseenter");
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x0600344C RID: 13388 RVA: 0x000AD7B0 File Offset: 0x000AB9B0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Tooltip)
				};
			}
		}
	}
}
