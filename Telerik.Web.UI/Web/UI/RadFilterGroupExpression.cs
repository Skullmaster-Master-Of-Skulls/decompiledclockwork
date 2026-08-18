using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200188B RID: 6283
	public class RadFilterGroupExpression : RadFilterExpression
	{
		// Token: 0x17004950 RID: 18768
		// (get) Token: 0x0600F335 RID: 62261 RVA: 0x00375C59 File Offset: 0x00373E59
		// (set) Token: 0x0600F336 RID: 62262 RVA: 0x00375C61 File Offset: 0x00373E61
		public virtual RadFilterGroupOperation GroupOperation { get; set; }

		// Token: 0x17004951 RID: 18769
		// (get) Token: 0x0600F337 RID: 62263 RVA: 0x00375C6A File Offset: 0x00373E6A
		public virtual RadFilterExpressionsCollection Expressions
		{
			get
			{
				if (this._childExpression == null)
				{
					this._childExpression = new RadFilterExpressionsCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._childExpression).TrackViewState();
					}
				}
				return this._childExpression;
			}
		}

		// Token: 0x17004952 RID: 18770
		// (get) Token: 0x0600F338 RID: 62264 RVA: 0x00375C98 File Offset: 0x00373E98
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.Group;
			}
		}

		// Token: 0x17004953 RID: 18771
		// (get) Token: 0x0600F339 RID: 62265 RVA: 0x00375C9C File Offset: 0x00373E9C
		public bool IsEmpty
		{
			get
			{
				return this.CheckIsEmpty(this);
			}
		}

		// Token: 0x0600F33A RID: 62266 RVA: 0x00375CA8 File Offset: 0x00373EA8
		protected bool CheckIsEmpty(RadFilterGroupExpression group)
		{
			if (group.Expressions.Count == 0)
			{
				return true;
			}
			bool flag = false;
			foreach (RadFilterExpression radFilterExpression in group.Expressions)
			{
				flag = (radFilterExpression.FilterFunction == RadFilterFunction.Group && this.CheckIsEmpty((RadFilterGroupExpression)radFilterExpression));
				if (!flag)
				{
					return flag;
				}
			}
			return flag;
		}

		// Token: 0x0600F33B RID: 62267 RVA: 0x00375D24 File Offset: 0x00373F24
		public RadFilterNonGroupExpression FindByFieldName(string fieldName)
		{
			if (string.IsNullOrEmpty(fieldName))
			{
				throw new ArgumentOutOfRangeException("fieldName", "FieldName cannot be null or empty string!");
			}
			Stack<RadFilterExpression> stack = new Stack<RadFilterExpression>();
			for (int i = this.Expressions.Count - 1; i >= 0; i--)
			{
				stack.Push(this.Expressions[i]);
			}
			while (stack.Count > 0)
			{
				RadFilterExpression radFilterExpression = stack.Pop();
				if (radFilterExpression.FilterFunction == RadFilterFunction.Group)
				{
					RadFilterGroupExpression radFilterGroupExpression = (RadFilterGroupExpression)radFilterExpression;
					for (int j = radFilterGroupExpression.Expressions.Count - 1; j >= 0; j--)
					{
						stack.Push(radFilterGroupExpression.Expressions[j]);
					}
				}
				else
				{
					RadFilterNonGroupExpression radFilterNonGroupExpression = (RadFilterNonGroupExpression)radFilterExpression;
					if (radFilterNonGroupExpression.FieldName == fieldName)
					{
						return radFilterNonGroupExpression;
					}
				}
			}
			return null;
		}

		// Token: 0x0600F33C RID: 62268 RVA: 0x00375DE8 File Offset: 0x00373FE8
		public void AddExpression(RadFilterExpression expression)
		{
			this.Expressions.Add(expression);
		}

		// Token: 0x0600F33D RID: 62269 RVA: 0x00375DF8 File Offset: 0x00373FF8
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				base.LoadViewState(array[0]);
				((IStateManager)this.Expressions).LoadViewState(array[1]);
				this.GroupOperation = (RadFilterGroupOperation)array[2];
				return;
			}
			base.LoadViewState(savedState);
		}

		// Token: 0x0600F33E RID: 62270 RVA: 0x00375E3C File Offset: 0x0037403C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Expressions).SaveViewState(),
				this.GroupOperation
			};
		}

		// Token: 0x0600F33F RID: 62271 RVA: 0x00375E76 File Offset: 0x00374076
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Expressions).TrackViewState();
		}

		// Token: 0x040045D0 RID: 17872
		private RadFilterExpressionsCollection _childExpression;
	}
}
