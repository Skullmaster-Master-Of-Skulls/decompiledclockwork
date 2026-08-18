using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000715 RID: 1813
	internal class OlapHierarchyFieldInfo : OlapFieldInfo
	{
		// Token: 0x0600405E RID: 16478 RVA: 0x000CACCA File Offset: 0x000C8ECA
		public OlapHierarchyFieldInfo()
		{
			this.Levels = new List<OlapHierarchyFieldInfo>();
			base.AllowedRoles = (FieldRoles.Row | FieldRoles.Column | FieldRoles.Filter);
			this.IsUserHierarchy = false;
			base.SupportsMembersFunction = false;
			this.ShouldIgnoreHierarchicalStructure = false;
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x000CACFA File Offset: 0x000C8EFA
		// (set) Token: 0x06004060 RID: 16480 RVA: 0x000CAD02 File Offset: 0x000C8F02
		public string AllMemberName { get; internal set; }

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x000CAD0B File Offset: 0x000C8F0B
		// (set) Token: 0x06004062 RID: 16482 RVA: 0x000CAD13 File Offset: 0x000C8F13
		public IList<OlapHierarchyFieldInfo> Levels { get; internal set; }

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x000CAD1C File Offset: 0x000C8F1C
		// (set) Token: 0x06004064 RID: 16484 RVA: 0x000CAD24 File Offset: 0x000C8F24
		public bool ShouldIgnoreHierarchicalStructure { get; internal set; }

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x000CAD2D File Offset: 0x000C8F2D
		// (set) Token: 0x06004066 RID: 16486 RVA: 0x000CAD35 File Offset: 0x000C8F35
		public bool IsUserHierarchy { get; internal set; }
	}
}
