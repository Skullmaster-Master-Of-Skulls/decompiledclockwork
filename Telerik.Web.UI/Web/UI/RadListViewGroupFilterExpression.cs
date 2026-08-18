using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001988 RID: 6536
	[Browsable(false)]
	public class RadListViewGroupFilterExpression : RadListViewFilterExpression, IRadListViewFilterExpressionContainer
	{
		// Token: 0x0600FD17 RID: 64791 RVA: 0x0038E8F7 File Offset: 0x0038CAF7
		public RadListViewGroupFilterExpression() : this(RadListViewGroupFilterOperator.And)
		{
		}

		// Token: 0x0600FD18 RID: 64792 RVA: 0x0038E900 File Offset: 0x0038CB00
		public RadListViewGroupFilterExpression(RadListViewGroupFilterOperator groupOperator)
		{
			this._filterExpressions = new List<RadListViewFilterExpression>();
			this.GroupOperator = groupOperator;
			this._containerHelper = new ListViewFilterExpressionContainerHelper(this);
		}

		// Token: 0x17004C67 RID: 19559
		// (get) Token: 0x0600FD19 RID: 64793 RVA: 0x0038E926 File Offset: 0x0038CB26
		// (set) Token: 0x0600FD1A RID: 64794 RVA: 0x0038E92E File Offset: 0x0038CB2E
		[Browsable(false)]
		public RadListViewGroupFilterOperator GroupOperator { get; protected set; }

		// Token: 0x17004C68 RID: 19560
		// (get) Token: 0x0600FD1B RID: 64795 RVA: 0x0038E937 File Offset: 0x0038CB37
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.Group;
			}
		}

		// Token: 0x17004C69 RID: 19561
		// (get) Token: 0x0600FD1C RID: 64796 RVA: 0x0038E93B File Offset: 0x0038CB3B
		public override Type FieldType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x0600FD1D RID: 64797 RVA: 0x0038E96E File Offset: 0x0038CB6E
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				RadListViewGroupFilterExpression.ExpressionEvaluator expressionEvaluator = RadListViewGroupFilterExpression.ExpressionEvaluator.CreateEvaluator(this.GroupOperator, this._filterExpressions);
				return expressionEvaluator.Evaluate(item);
			};
		}

		// Token: 0x0600FD1E RID: 64798 RVA: 0x0038E97C File Offset: 0x0038CB7C
		public override string ToDynamicLinq()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			stringBuilder.Append(new DynamicLinqExpressionBuilder(this._filterExpressions, this.GroupOperator).Convert());
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600FD1F RID: 64799 RVA: 0x0038E9CC File Offset: 0x0038CBCC
		public override string ToEntitySQL()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			stringBuilder.Append(new EntitySQLExpressionBuilder(this._filterExpressions, this.GroupOperator).Convert());
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600FD20 RID: 64800 RVA: 0x0038EA1A File Offset: 0x0038CC1A
		public void Add(RadListViewFilterExpression filterExpression)
		{
			if (filterExpression == null)
			{
				throw new ArgumentNullException("filterExpression", "filterExpression cannot be null.");
			}
			this.TrackViewState();
			((IStateManager)filterExpression).TrackViewState();
			this._filterExpressions.Add(filterExpression);
		}

		// Token: 0x17004C6A RID: 19562
		// (get) Token: 0x0600FD21 RID: 64801 RVA: 0x0038EA47 File Offset: 0x0038CC47
		public IList<RadListViewFilterExpression> Expressions
		{
			get
			{
				return this._filterExpressions;
			}
		}

		// Token: 0x0600FD22 RID: 64802 RVA: 0x0038EA4F File Offset: 0x0038CC4F
		public RadListViewFilterExpression FindByFieldName(string fieldName)
		{
			return this._containerHelper.FindByFieldName(fieldName);
		}

		// Token: 0x0600FD23 RID: 64803 RVA: 0x0038EA60 File Offset: 0x0038CC60
		protected override void LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				base.LoadViewState(array[0]);
				this.GroupOperator = (RadListViewGroupFilterOperator)array[1];
				int num = (int)((Pair)array[2]).First;
				int num2 = (int)((Pair)array[2]).Second;
				int num3 = 3;
				int num4 = 0;
				while (num4 < num2 && num4 < array.Length - num3)
				{
					Pair pair = array[num4 + num3] as Pair;
					if (pair != null && this._filterExpressions.Count > 0)
					{
						RadListViewFilterExpression radListViewFilterExpression = this._filterExpressions[num4];
						((IStateManager)radListViewFilterExpression).LoadViewState(pair.Second);
					}
					num4++;
				}
				int num5 = num2;
				while (num5 < num && num5 < array.Length)
				{
					Pair pair2 = array[num5 + num3] as Pair;
					if (pair2 != null)
					{
						Pair pair3 = pair2.First as Pair;
						if (pair3 != null)
						{
							RadListViewFilterExpression radListViewFilterExpression2 = RadListViewFilterExpression.CreateExpressionFromTypeName((string)pair3.First, (string)pair3.Second);
							this._filterExpressions.Add(radListViewFilterExpression2);
							((IStateManager)radListViewFilterExpression2).TrackViewState();
							((IStateManager)radListViewFilterExpression2).LoadViewState(pair2.Second);
						}
					}
					num5++;
				}
			}
		}

		// Token: 0x0600FD24 RID: 64804 RVA: 0x0038EB94 File Offset: 0x0038CD94
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(base.SaveViewState());
			arrayList.Add(this.GroupOperator);
			arrayList.Add(new Pair(this.Expressions.Count, this._notTrackedExpressionCount));
			bool flag = false;
			foreach (RadListViewFilterExpression radListViewFilterExpression in this.Expressions)
			{
				flag = true;
				arrayList.Add(new Pair(new Pair(radListViewFilterExpression.ExpressionType, this.GetExpressionTypeName(radListViewFilterExpression.FieldType)), ((IStateManager)radListViewFilterExpression).SaveViewState()));
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600FD25 RID: 64805 RVA: 0x0038EC68 File Offset: 0x0038CE68
		private string GetExpressionTypeName(Type type)
		{
			string text = type.FullName;
			Type type2 = Type.GetType(text);
			if (type2 == null)
			{
				text = string.Format("{0}, {1}", type.FullName, type.Assembly);
			}
			return text;
		}

		// Token: 0x0600FD26 RID: 64806 RVA: 0x0038ECAC File Offset: 0x0038CEAC
		protected override void TrackViewState()
		{
			if (this._isMarked)
			{
				return;
			}
			this._isMarked = true;
			this._notTrackedExpressionCount = this.Expressions.Count;
			this._filterExpressions.ForEach(delegate(RadListViewFilterExpression expression)
			{
				((IStateManager)expression).TrackViewState();
			});
			base.TrackViewState();
		}

		// Token: 0x0600FD27 RID: 64807 RVA: 0x0038ED08 File Offset: 0x0038CF08
		public override string ToOql()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			stringBuilder.Append(new OqlExpressionBuilder(this._filterExpressions, this.GroupOperator).Convert());
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x040047ED RID: 18413
		private List<RadListViewFilterExpression> _filterExpressions;

		// Token: 0x040047EE RID: 18414
		private ListViewFilterExpressionContainerHelper _containerHelper;

		// Token: 0x040047EF RID: 18415
		private bool _isMarked;

		// Token: 0x040047F0 RID: 18416
		private int _notTrackedExpressionCount;

		// Token: 0x02001989 RID: 6537
		private abstract class ExpressionEvaluator
		{
			// Token: 0x0600FD2A RID: 64810 RVA: 0x0038ED58 File Offset: 0x0038CF58
			public static RadListViewGroupFilterExpression.ExpressionEvaluator CreateEvaluator(RadListViewGroupFilterOperator groupOperator, IEnumerable<RadListViewFilterExpression> expressions)
			{
				RadListViewGroupFilterExpression.ExpressionEvaluator result;
				if (groupOperator == RadListViewGroupFilterOperator.Or)
				{
					result = new RadListViewGroupFilterExpression.ExpressionEvaluator.OrExpressionEvaluator(expressions);
				}
				else
				{
					result = new RadListViewGroupFilterExpression.ExpressionEvaluator.AndExpressionEvaluator(expressions);
				}
				return result;
			}

			// Token: 0x0600FD2B RID: 64811
			public abstract bool Evaluate(object item);

			// Token: 0x0200198A RID: 6538
			private class OrExpressionEvaluator : RadListViewGroupFilterExpression.ExpressionEvaluator
			{
				// Token: 0x0600FD2D RID: 64813 RVA: 0x0038ED86 File Offset: 0x0038CF86
				public OrExpressionEvaluator(IEnumerable<RadListViewFilterExpression> filterExpressions)
				{
					this._filterExpressions = filterExpressions;
				}

				// Token: 0x0600FD2E RID: 64814 RVA: 0x0038ED98 File Offset: 0x0038CF98
				public override bool Evaluate(object item)
				{
					foreach (RadListViewFilterExpression radListViewFilterExpression in this._filterExpressions)
					{
						if (radListViewFilterExpression.ToPredicate()(item))
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x040047F3 RID: 18419
				private IEnumerable<RadListViewFilterExpression> _filterExpressions;
			}

			// Token: 0x0200198B RID: 6539
			private class AndExpressionEvaluator : RadListViewGroupFilterExpression.ExpressionEvaluator
			{
				// Token: 0x0600FD2F RID: 64815 RVA: 0x0038EDF4 File Offset: 0x0038CFF4
				public AndExpressionEvaluator(IEnumerable<RadListViewFilterExpression> filterExpressions)
				{
					this._filterExpressions = filterExpressions;
				}

				// Token: 0x0600FD30 RID: 64816 RVA: 0x0038EE04 File Offset: 0x0038D004
				public override bool Evaluate(object item)
				{
					foreach (RadListViewFilterExpression radListViewFilterExpression in this._filterExpressions)
					{
						if (!radListViewFilterExpression.ToPredicate()(item))
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x040047F4 RID: 18420
				private IEnumerable<RadListViewFilterExpression> _filterExpressions;
			}
		}
	}
}
