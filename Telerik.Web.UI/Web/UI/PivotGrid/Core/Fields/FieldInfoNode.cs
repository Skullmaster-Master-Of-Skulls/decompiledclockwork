using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CAE RID: 3246
	public sealed class FieldInfoNode : ContainerNode
	{
		// Token: 0x0600799B RID: 31131 RVA: 0x001BEF70 File Offset: 0x001BD170
		public FieldInfoNode(IPivotFieldInfo info, ContainerNodeRole role) : base(info.Name, info.DisplayName, role)
		{
			this.FieldInfo = info;
		}

		// Token: 0x0600799C RID: 31132 RVA: 0x001BEF8C File Offset: 0x001BD18C
		public FieldInfoNode(IPivotFieldInfo info) : this(info, ContainerNodeRole.Selectable)
		{
			this.FieldInfo = info;
		}

		// Token: 0x1700272E RID: 10030
		// (get) Token: 0x0600799D RID: 31133 RVA: 0x001BEF9D File Offset: 0x001BD19D
		// (set) Token: 0x0600799E RID: 31134 RVA: 0x001BEFA5 File Offset: 0x001BD1A5
		public IPivotFieldInfo FieldInfo { get; private set; }
	}
}
