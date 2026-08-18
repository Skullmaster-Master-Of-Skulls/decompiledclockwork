using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB0 RID: 3248
	public interface IFieldInfoData
	{
		// Token: 0x1700272F RID: 10031
		// (get) Token: 0x060079A1 RID: 31137
		ContainerNode RootFieldInfo { get; }

		// Token: 0x060079A2 RID: 31138
		IPivotFieldInfo GetFieldDescriptionByMember(string name);
	}
}
