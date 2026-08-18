using System;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000714 RID: 1812
	internal class OlapFieldInfo : PivotFieldInfo
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x000CAC9F File Offset: 0x000C8E9F
		public OlapFieldInfo()
		{
			base.AllowedRoles = (FieldRoles.Row | FieldRoles.Column | FieldRoles.Filter);
			this.SupportsMembersFunction = false;
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x0600405B RID: 16475 RVA: 0x000CACB6 File Offset: 0x000C8EB6
		// (set) Token: 0x0600405C RID: 16476 RVA: 0x000CACBE File Offset: 0x000C8EBE
		public bool SupportsMembersFunction { get; internal set; }

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x000CACC7 File Offset: 0x000C8EC7
		public virtual bool IsMeasure
		{
			get
			{
				return false;
			}
		}
	}
}
