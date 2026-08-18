using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;
using Telerik.Web.UI.PivotGrid.Filtering;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x0200070E RID: 1806
	public class OlapItemsFilterCondition : OlapCondition, IItemsFilterCondition
	{
		// Token: 0x06004023 RID: 16419 RVA: 0x000CA617 File Offset: 0x000C8817
		public OlapItemsFilterCondition()
		{
			this.distinctCondition = new OlapSetCondition();
			this.distinctCondition.Comparison = SetComparison.DoesNotInclude;
			this.condition = new OlapComparisonCondition();
		}

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x000CA641 File Offset: 0x000C8841
		// (set) Token: 0x06004025 RID: 16421 RVA: 0x000CA649 File Offset: 0x000C8849
		ISetCondition IItemsFilterCondition.DistinctCondition
		{
			get
			{
				return this.DistinctCondition;
			}
			set
			{
				this.DistinctCondition = (value as OlapSetCondition);
			}
		}

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x000CA657 File Offset: 0x000C8857
		// (set) Token: 0x06004027 RID: 16423 RVA: 0x000CA65F File Offset: 0x000C885F
		Condition IItemsFilterCondition.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.condition = (value as OlapCondition);
			}
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x000CA66D File Offset: 0x000C886D
		// (set) Token: 0x06004029 RID: 16425 RVA: 0x000CA675 File Offset: 0x000C8875
		public OlapSetCondition DistinctCondition
		{
			get
			{
				return this.distinctCondition;
			}
			set
			{
				if (this.distinctCondition != value)
				{
					base.ChangeSettingsProperty<OlapSetCondition>(ref this.distinctCondition, value);
					base.OnPropertyChanged("DistinctCondition");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x000CA6A3 File Offset: 0x000C88A3
		// (set) Token: 0x0600402B RID: 16427 RVA: 0x000CA6AB File Offset: 0x000C88AB
		public OlapCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x000CA6B4 File Offset: 0x000C88B4
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapItemsFilterCondition();
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x000CA6BC File Offset: 0x000C88BC
		internal override IEnumerable<OlapExpression> GetExpressions(OlapExpressionOptions options)
		{
			if (options.HierarchyInfo == null)
			{
				return new List<OlapExpression>();
			}
			IEnumerable<OlapExpression> expressions = this.DistinctCondition.GetExpressions(options);
			List<OlapExpression> list = new List<OlapExpression>();
			list.AddRange(expressions);
			if (this.Condition != null && this.Condition.IsActive)
			{
				IEnumerable<OlapExpression> expressions2 = this.Condition.GetExpressions(options);
				list.AddRange(expressions2);
			}
			return list;
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x000CA71C File Offset: 0x000C891C
		protected sealed override void CloneCore(Cloneable source)
		{
			OlapItemsFilterCondition olapItemsFilterCondition = source as OlapItemsFilterCondition;
			if (olapItemsFilterCondition != null)
			{
				this.DistinctCondition = Cloneable.CloneOrDefault<OlapSetCondition>(olapItemsFilterCondition.DistinctCondition);
				this.condition = Cloneable.CloneOrDefault<OlapCondition>(olapItemsFilterCondition.Condition);
			}
		}

		// Token: 0x04001105 RID: 4357
		private OlapSetCondition distinctCondition;

		// Token: 0x04001106 RID: 4358
		private OlapCondition condition;
	}
}
