using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000591 RID: 1425
	public class BingConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600333E RID: 13118 RVA: 0x000AAC98 File Offset: 0x000A8E98
		protected override void PopulateProperties(IDictionary<string, object> state, object obj)
		{
			Bing bing = obj as Bing;
			ExplicitJavaScriptConverter.AddProperty(state, "attribution", bing.Attribution, "");
			ExplicitJavaScriptConverter.AddProperty(state, "opacity", bing.Opacity, 1.0);
			ExplicitJavaScriptConverter.AddProperty(state, "key", bing.Key, "");
			ExplicitJavaScriptConverter.AddProperty(state, "imagerySet", bing.ImagerySet, "road");
			ExplicitJavaScriptConverter.AddProperty(state, "culture", bing.Culture, "en-US");
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000AAD28 File Offset: 0x000A8F28
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Bing)
				};
			}
		}
	}
}
