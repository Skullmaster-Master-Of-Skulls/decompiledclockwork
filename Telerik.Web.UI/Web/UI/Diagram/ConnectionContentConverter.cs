using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000215 RID: 533
	public class ConnectionContentConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x00044F30 File Offset: 0x00043130
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			ConnectionContent connectionContent = obj as ConnectionContent;
			ExplicitJavaScriptConverter.AddProperty(state, "color", connectionContent.Color, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontFamily", connectionContent.FontFamily, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontSize", connectionContent.FontSize, 0.0);
			ExplicitJavaScriptConverter.AddProperty(state, "fontStyle", connectionContent.FontStyle, "");
			ExplicitJavaScriptConverter.AddProperty(state, "fontWeight", connectionContent.FontWeight, "");
			if (connectionContent.Template.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
			{
				base.AddScript(state, "template", connectionContent.Template.Substring(11).TrimStart(new char[0]));
			}
			else
			{
				ExplicitJavaScriptConverter.AddProperty(state, "template", connectionContent.Template, "");
			}
			ExplicitJavaScriptConverter.AddProperty(state, "text", connectionContent.Text, "");
			base.AddScript(state, "visual", connectionContent.Visual);
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00045038 File Offset: 0x00043238
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ConnectionContent)
				};
			}
		}
	}
}
