using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000191 RID: 401
	[SchemaElementName("WebHandler")]
	internal class WebHandler4_0 : WebHandler
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x000546C1 File Offset: 0x000528C1
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x000546C9 File Offset: 0x000528C9
		[Browsable(false)]
		[Filterable(false)]
		public string Description { get; set; }
	}
}
