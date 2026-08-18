using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006F6 RID: 1782
	[DataContract]
	public sealed class OlapLabelGroupFilter : GroupFilter, ILabelGroupFilter, IConditionFactory
	{
		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000C9457 File Offset: 0x000C7657
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x000C945F File Offset: 0x000C765F
		[DataMember]
		public OlapCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					this.condition = value;
					base.OnPropertyChanged("Condition");
				}
			}
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x000C947C File Offset: 0x000C767C
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapLabelGroupFilter();
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000C9484 File Offset: 0x000C7684
		protected override void CloneCore(Cloneable source)
		{
			OlapLabelGroupFilter olapLabelGroupFilter = source as OlapLabelGroupFilter;
			if (olapLabelGroupFilter != null)
			{
				this.Condition = ((olapLabelGroupFilter.Condition == null) ? null : (olapLabelGroupFilter.Condition.Clone() as OlapCondition));
			}
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x000C94BC File Offset: 0x000C76BC
		internal IEnumerable<OlapExpression> GetExpressions(OlapExpressionOptions options)
		{
			if (this.Condition == null || options.HierarchyInfo == null)
			{
				return new List<OlapExpression>();
			}
			return this.Condition.GetExpressions(options);
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x000C94EE File Offset: 0x000C76EE
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x000C94F6 File Offset: 0x000C76F6
		Condition ILabelGroupFilter.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = (value as OlapCondition);
			}
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000C9504 File Offset: 0x000C7704
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			return DescriptionBase.CreateOlapCondition(conditionType);
		}

		// Token: 0x040010C7 RID: 4295
		private OlapCondition condition;
	}
}
