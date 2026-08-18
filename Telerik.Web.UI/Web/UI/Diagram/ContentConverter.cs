using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000225 RID: 549
	public class ContentConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06001407 RID: 5127 RVA: 0x000460D0 File Offset: 0x000442D0
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Content content = obj as Content;
			ExplicitJavaScriptConverter.AddProperty(state, "align", content.Align, "");
			ExplicitJavaScriptConverter.AddProperty(state, "color", content.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontFamily", content.FontFamily, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontSize", content.FontSize, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "fontStyle", content.FontStyle, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontWeight", content.FontWeight, "");
			if (content.Template.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", content.Template.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "template", content.Template, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "text", content.Text, "");
			ExplicitJavaScriptConverter.AddProperty(state, "html", content.Html, "");
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000461F4 File Offset: 0x000443F4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Content)
				};
			}
		}
	}
}
