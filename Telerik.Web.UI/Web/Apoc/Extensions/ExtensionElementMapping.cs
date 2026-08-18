using System;
using System.Collections;
using Telerik.Web.Apoc.Fo;
using Telerik.Web.Apoc.Fo.Properties;

namespace Telerik.Web.Apoc.Extensions
{
	// Token: 0x02001392 RID: 5010
	internal class ExtensionElementMapping
	{
		// Token: 0x0600D0C8 RID: 53448 RVA: 0x002E3988 File Offset: 0x002E1B88
		static ExtensionElementMapping()
		{
			ExtensionElementMapping.foObjs["outline"] = Outline.GetMaker();
			ExtensionElementMapping.foObjs["label"] = Label.GetMaker();
		}

		// Token: 0x0600D0C9 RID: 53449 RVA: 0x002E39BC File Offset: 0x002E1BBC
		public void AddToBuilder(FOTreeBuilder builder)
		{
			builder.AddElementMapping("http://www.chive.com/apoc/ext", ExtensionElementMapping.foObjs);
			builder.AddPropertyMapping("http://www.chive.com/apoc/ext", ExtensionPropertyMapping.getGenericMappings());
		}

		// Token: 0x04003801 RID: 14337
		public const string URI = "http://www.chive.com/apoc/ext";

		// Token: 0x04003802 RID: 14338
		private static Hashtable foObjs = new Hashtable();
	}
}
