using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000AFB RID: 2811
	public interface IHierarchyContainer
	{
		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x06006985 RID: 27013
		// (set) Token: 0x06006986 RID: 27014
		string DataFieldID { get; set; }

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x06006987 RID: 27015
		// (set) Token: 0x06006988 RID: 27016
		string DataFieldParentID { get; set; }

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x06006989 RID: 27017
		// (set) Token: 0x0600698A RID: 27018
		string DataNavigateUrlField { get; set; }
	}
}
