using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001088 RID: 4232
	internal class EditorModuleConverter : EditorConverterBase
	{
		// Token: 0x1700369D RID: 13981
		// (get) Token: 0x0600AA1E RID: 43550 RVA: 0x0024E328 File Offset: 0x0024C528
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorModule)
				};
			}
		}
	}
}
