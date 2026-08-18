using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000F73 RID: 3955
	public abstract class RadFilterQueryProvider
	{
		// Token: 0x17002FD9 RID: 12249
		// (get) Token: 0x0600977E RID: 38782
		public abstract IList<RadFilterFunction> SupportedFilterFunctions { get; }

		// Token: 0x17002FDA RID: 12250
		// (get) Token: 0x0600977F RID: 38783
		public abstract IList<RadFilterGroupOperation> SupportedGroupOperations { get; }

		// Token: 0x06009780 RID: 38784
		public abstract void ProcessGroup(RadFilterGroupExpression rootGroup);

		// Token: 0x06009781 RID: 38785 RVA: 0x0021F7DF File Offset: 0x0021D9DF
		public virtual bool IsValidFilterFunction(RadFilterFunction filterFunction)
		{
			return this.SupportedFilterFunctions == null || this.SupportedFilterFunctions.Count == 0 || this.SupportedFilterFunctions.Contains(filterFunction);
		}

		// Token: 0x06009782 RID: 38786 RVA: 0x0021F804 File Offset: 0x0021DA04
		public virtual bool IsValidGroupOperation(RadFilterGroupOperation groupOperation)
		{
			return this.SupportedGroupOperations == null || this.SupportedGroupOperations.Count == 0 || this.SupportedGroupOperations.Contains(groupOperation);
		}

		// Token: 0x17002FDB RID: 12251
		// (get) Token: 0x06009783 RID: 38787 RVA: 0x0021F829 File Offset: 0x0021DA29
		public virtual string Result
		{
			get
			{
				return this.Expression.ToString();
			}
		}

		// Token: 0x06009784 RID: 38788 RVA: 0x0021F836 File Offset: 0x0021DA36
		protected bool IsValidGroup(RadFilterGroupExpression group)
		{
			return !group.IsEmpty && this.IsValidGroupOperation(group.GroupOperation);
		}

		// Token: 0x06009785 RID: 38789 RVA: 0x0021F854 File Offset: 0x0021DA54
		protected virtual void ProcessGroupInternal(RadFilterGroupExpression group)
		{
			if (this.Expression.Length == 0 && !this.IsValidGroup(group))
			{
				return;
			}
			this.Expression.Append(this.ConvertStartGroupOperatorToString(group.GroupOperation));
			this.ProcessGroupCollection(group.Expressions, group.GroupOperation);
			this.Expression.Append(")");
		}

		// Token: 0x06009786 RID: 38790 RVA: 0x0021F8B4 File Offset: 0x0021DAB4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected virtual void ProcessGroupCollection(RadFilterExpressionsCollection expressions, RadFilterGroupOperation groupOperation)
		{
			bool flag = true;
			foreach (RadFilterExpression radFilterExpression in expressions)
			{
				if (this.IsValidFilterFunction(radFilterExpression.FilterFunction))
				{
					if (flag)
					{
						if (radFilterExpression.FilterFunction == RadFilterFunction.Group)
						{
							RadFilterGroupExpression group = radFilterExpression as RadFilterGroupExpression;
							if (this.IsValidGroup(group))
							{
								flag = false;
								this.ProcessGroupInternal(group);
							}
						}
						else
						{
							flag = false;
							this.Expression.Append(this.PrepareQuery(radFilterExpression as RadFilterNonGroupExpression));
						}
					}
					else if (radFilterExpression.FilterFunction == RadFilterFunction.Group)
					{
						RadFilterGroupExpression group2 = radFilterExpression as RadFilterGroupExpression;
						if (this.IsValidGroup(group2))
						{
							this.Expression.Append(this.ConvertInGroupOperatorToString(groupOperation));
							this.ProcessGroupInternal(group2);
						}
					}
					else
					{
						this.Expression.Append(this.ConvertInGroupOperatorToString(groupOperation));
						this.Expression.Append(this.PrepareQuery(radFilterExpression as RadFilterNonGroupExpression));
					}
				}
			}
		}

		// Token: 0x06009787 RID: 38791 RVA: 0x0021F9BC File Offset: 0x0021DBBC
		protected virtual string ConvertInGroupOperatorToString(RadFilterGroupOperation groupOperation)
		{
			string result;
			switch (groupOperation)
			{
			case RadFilterGroupOperation.And:
			case RadFilterGroupOperation.NotAnd:
				result = " AND ";
				break;
			case RadFilterGroupOperation.Or:
			case RadFilterGroupOperation.NotOr:
				result = " OR ";
				break;
			default:
				result = " AND ";
				break;
			}
			return result;
		}

		// Token: 0x06009788 RID: 38792 RVA: 0x0021F9FC File Offset: 0x0021DBFC
		protected virtual string ConvertStartGroupOperatorToString(RadFilterGroupOperation groupOperation)
		{
			string result;
			switch (groupOperation)
			{
			case RadFilterGroupOperation.NotAnd:
			case RadFilterGroupOperation.NotOr:
				result = "NOT(";
				break;
			default:
				result = "(";
				break;
			}
			return result;
		}

		// Token: 0x17002FDC RID: 12252
		// (get) Token: 0x06009789 RID: 38793 RVA: 0x0021FA2C File Offset: 0x0021DC2C
		// (set) Token: 0x0600978A RID: 38794 RVA: 0x0021FA34 File Offset: 0x0021DC34
		public Action<RadFilterEvaluationData> OnExpressionEvaluated { get; set; }

		// Token: 0x0600978B RID: 38795 RVA: 0x0021FA3D File Offset: 0x0021DC3D
		protected void CallOnExpressionEvaluated(RadFilterEvaluationData data)
		{
			if (this.OnExpressionEvaluated != null)
			{
				this.OnExpressionEvaluated(data);
			}
		}

		// Token: 0x0600978C RID: 38796
		protected abstract string PrepareQuery(RadFilterNonGroupExpression expression);

		// Token: 0x04002B4D RID: 11085
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected StringBuilder Expression;
	}
}
