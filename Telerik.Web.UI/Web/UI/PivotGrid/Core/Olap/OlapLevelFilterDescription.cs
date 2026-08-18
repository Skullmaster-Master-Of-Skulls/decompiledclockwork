using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x020006F7 RID: 1783
	[DataContract]
	public abstract class OlapLevelFilterDescription : OlapFilterDescriptionBase, IDistinctValuesDescription
	{
		// Token: 0x06003F77 RID: 16247 RVA: 0x000C9514 File Offset: 0x000C7714
		internal OlapLevelFilterDescription()
		{
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x000C951C File Offset: 0x000C771C
		internal OlapLevelFilterDescription(OlapHierarchyFieldInfo fieldInfo, OlapHierarchyFieldInfo parentInfo)
		{
			base.FieldInfo = fieldInfo;
			this.ParentInfo = parentInfo;
		}

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000C9532 File Offset: 0x000C7732
		// (set) Token: 0x06003F7A RID: 16250 RVA: 0x000C953A File Offset: 0x000C773A
		internal OlapHierarchyFieldInfo ParentInfo { get; set; }

		// Token: 0x06003F7B RID: 16251 RVA: 0x000C9544 File Offset: 0x000C7744
		internal override IEnumerable<OlapExpression> GetExpressions()
		{
			if (base.Condition == null || base.FieldInfo == null || !base.Condition.IsActive)
			{
				return new List<OlapExpression>();
			}
			OlapExpressionOptions options = new OlapExpressionOptions
			{
				HierarchyInfo = this.ParentInfo,
				MemberInfo = base.FieldInfo,
				UseHierarchyAsAccess = true
			};
			return base.Condition.GetExpressions(options);
		}
	}
}
