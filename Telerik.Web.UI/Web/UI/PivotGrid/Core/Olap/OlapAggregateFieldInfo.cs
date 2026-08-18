using System;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D07 RID: 3335
	internal class OlapAggregateFieldInfo : OlapFieldInfo
	{
		// Token: 0x06007C51 RID: 31825 RVA: 0x001C95FA File Offset: 0x001C77FA
		public OlapAggregateFieldInfo()
		{
			base.PreferredRole = FieldRoles.Value;
			base.AllowedRoles = FieldRoles.Value;
		}

		// Token: 0x170027AB RID: 10155
		// (get) Token: 0x06007C52 RID: 31826 RVA: 0x001C9610 File Offset: 0x001C7810
		// (set) Token: 0x06007C53 RID: 31827 RVA: 0x001C9618 File Offset: 0x001C7818
		public virtual bool DisplayValueAsKpi { get; internal set; }

		// Token: 0x170027AC RID: 10156
		// (get) Token: 0x06007C54 RID: 31828 RVA: 0x001C9621 File Offset: 0x001C7821
		public override bool IsMeasure
		{
			get
			{
				return true;
			}
		}
	}
}
