using System;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000BEE RID: 3054
	[Flags]
	public enum OrgChartLoadOnDemand
	{
		// Token: 0x04001FAB RID: 8107
		[XmlEnum(Name = "None")]
		None = 0,
		// Token: 0x04001FAC RID: 8108
		[XmlEnum(Name = "Nodes")]
		Nodes = 1,
		// Token: 0x04001FAD RID: 8109
		[XmlEnum(Name = "Nodes")]
		Groups = 2,
		// Token: 0x04001FAE RID: 8110
		[XmlEnum(Name = "NodesAndGroups")]
		NodesAndGroups = 3
	}
}
