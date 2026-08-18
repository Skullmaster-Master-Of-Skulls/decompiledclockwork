using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001980 RID: 6528
	[PersistChildren(false)]
	[Serializable]
	public class RadListViewFilterExpressionCollection : IList<RadListViewFilterExpression>, ICollection<RadListViewFilterExpression>, IEnumerable<RadListViewFilterExpression>, IHideObjectMembers, IStateManager, IList, ICollection, IEnumerable, IRadListViewFilterExpressionContainer
	{
		// Token: 0x0600FCAA RID: 64682 RVA: 0x0038DEE3 File Offset: 0x0038C0E3
		public RadListViewFilterExpressionCollection()
		{
			this._expressions = new List<RadListViewFilterExpression>();
		}

		// Token: 0x17004C53 RID: 19539
		// (get) Token: 0x0600FCAB RID: 64683 RVA: 0x0038DEF6 File Offset: 0x0038C0F6
		protected RadListViewFilterExpressionFluentBuilder FluentBuilder
		{
			get
			{
				if (this._fluentBuilder == null)
				{
					this._fluentBuilder = new RadListViewFilterExpressionFluentBuilder(this);
				}
				return this._fluentBuilder;
			}
		}

		// Token: 0x0600FCAC RID: 64684 RVA: 0x0038DF12 File Offset: 0x0038C112
		protected virtual RadListViewFilterExpression CreateExpressionFromTypeName(string expressionTypeName, string expressionFieldType)
		{
			return RadListViewFilterExpression.CreateExpressionFromTypeName(expressionTypeName, expressionFieldType);
		}

		// Token: 0x0600FCAD RID: 64685 RVA: 0x0038DF1B File Offset: 0x0038C11B
		public RadListViewFilterExpressionFluentBuilder BuildExpression()
		{
			return this.FluentBuilder;
		}

		// Token: 0x0600FCAE RID: 64686 RVA: 0x0038DF24 File Offset: 0x0038C124
		public void BuildExpression(Action<RadListViewFilterExpressionFluentBuilder> configuration)
		{
			RadListViewFilterExpressionFluentBuilder fluentBuilder = this.FluentBuilder;
			configuration(fluentBuilder);
			if (!fluentBuilder.IsBuild)
			{
				fluentBuilder.Build();
			}
		}

		// Token: 0x0600FCAF RID: 64687 RVA: 0x0038DF4D File Offset: 0x0038C14D
		public virtual string ToDynamicLinq()
		{
			return new DynamicLinqExpressionBuilder(this._expressions).Convert();
		}

		// Token: 0x0600FCB0 RID: 64688 RVA: 0x0038DF5F File Offset: 0x0038C15F
		public virtual string ToEntitySQL()
		{
			return new EntitySQLExpressionBuilder(this._expressions).Convert();
		}

		// Token: 0x0600FCB1 RID: 64689 RVA: 0x0038DF71 File Offset: 0x0038C171
		public virtual string ToOql()
		{
			return new OqlExpressionBuilder(this._expressions).Convert();
		}

		// Token: 0x17004C54 RID: 19540
		// (get) Token: 0x0600FCB2 RID: 64690 RVA: 0x0038DF83 File Offset: 0x0038C183
		private ListViewFilterExpressionContainerHelper ContainerHelper
		{
			get
			{
				if (this._containerHelper == null)
				{
					this._containerHelper = new ListViewFilterExpressionContainerHelper(this);
				}
				return this._containerHelper;
			}
		}

		// Token: 0x0600FCB3 RID: 64691 RVA: 0x0038DF9F File Offset: 0x0038C19F
		public virtual RadListViewFilterExpression FindByFieldName(string fieldName)
		{
			return this.ContainerHelper.FindByFieldName(fieldName);
		}

		// Token: 0x0600FCB4 RID: 64692 RVA: 0x0038DFAD File Offset: 0x0038C1AD
		RadListViewFilterExpression IRadListViewFilterExpressionContainer.FindByFieldName(string fieldName)
		{
			return this.FindByFieldName(fieldName);
		}

		// Token: 0x17004C55 RID: 19541
		// (get) Token: 0x0600FCB5 RID: 64693 RVA: 0x0038DFB6 File Offset: 0x0038C1B6
		IList<RadListViewFilterExpression> IRadListViewFilterExpressionContainer.Expressions
		{
			get
			{
				return this._expressions;
			}
		}

		// Token: 0x0600FCB6 RID: 64694 RVA: 0x0038DFC0 File Offset: 0x0038C1C0
		void IStateManager.LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				int num = (int)((Pair)array[0]).First;
				int num2 = (int)((Pair)array[0]).Second;
				int num3 = 0;
				while (num3 < num2 && num3 < array.Length)
				{
					Pair pair = array[num3 + 1] as Pair;
					if (pair != null)
					{
						RadListViewFilterExpression radListViewFilterExpression = this[num3];
						((IStateManager)radListViewFilterExpression).LoadViewState(pair.Second);
					}
					num3++;
				}
				int num4 = num2;
				while (num4 < num && num4 < array.Length)
				{
					Pair pair2 = array[num4 + 1] as Pair;
					if (pair2 != null)
					{
						Pair pair3 = pair2.First as Pair;
						if (pair3 != null)
						{
							RadListViewFilterExpression radListViewFilterExpression2 = this.CreateExpressionFromTypeName((string)pair3.First, (string)pair3.Second);
							this.Add(radListViewFilterExpression2);
							((IStateManager)radListViewFilterExpression2).LoadViewState(pair2.Second);
						}
					}
					num4++;
				}
			}
		}

		// Token: 0x0600FCB7 RID: 64695 RVA: 0x0038E0B4 File Offset: 0x0038C2B4
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.Count, this._notTrackedExpressionCount));
			bool flag = false;
			foreach (RadListViewFilterExpression radListViewFilterExpression in this)
			{
				flag = true;
				arrayList.Add(new Pair(new Pair(radListViewFilterExpression.ExpressionType, radListViewFilterExpression.FieldType.FullName), ((IStateManager)radListViewFilterExpression).SaveViewState()));
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600FCB8 RID: 64696 RVA: 0x0038E168 File Offset: 0x0038C368
		void IStateManager.TrackViewState()
		{
			if (this._isMarked)
			{
				return;
			}
			this._isMarked = true;
			this._notTrackedExpressionCount = this.Count;
			this._isTrackingViewState = true;
			this._expressions.ForEach(delegate(RadListViewFilterExpression expression)
			{
				((IStateManager)expression).TrackViewState();
			});
		}

		// Token: 0x17004C56 RID: 19542
		// (get) Token: 0x0600FCB9 RID: 64697 RVA: 0x0038E1C0 File Offset: 0x0038C3C0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600FCBA RID: 64698 RVA: 0x0038E1C8 File Offset: 0x0038C3C8
		public IEnumerator<RadListViewFilterExpression> GetEnumerator()
		{
			return this._expressions.GetEnumerator();
		}

		// Token: 0x0600FCBB RID: 64699 RVA: 0x0038E1DA File Offset: 0x0038C3DA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600FCBC RID: 64700 RVA: 0x0038E1E2 File Offset: 0x0038C3E2
		public virtual void Add(RadListViewFilterExpression item)
		{
			((IStateManager)item).TrackViewState();
			this._expressions.Add(item);
		}

		// Token: 0x0600FCBD RID: 64701 RVA: 0x0038E1F8 File Offset: 0x0038C3F8
		int IList.Add(object value)
		{
			int count = this.Count;
			this.Add((RadListViewFilterExpression)value);
			return count;
		}

		// Token: 0x0600FCBE RID: 64702 RVA: 0x0038E219 File Offset: 0x0038C419
		bool IList.Contains(object value)
		{
			return this._expressions.Contains((RadListViewFilterExpression)value);
		}

		// Token: 0x0600FCBF RID: 64703 RVA: 0x0038E22C File Offset: 0x0038C42C
		public void Clear()
		{
			this._expressions.Clear();
		}

		// Token: 0x0600FCC0 RID: 64704 RVA: 0x0038E239 File Offset: 0x0038C439
		int IList.IndexOf(object value)
		{
			return this._expressions.IndexOf((RadListViewFilterExpression)value);
		}

		// Token: 0x0600FCC1 RID: 64705 RVA: 0x0038E24C File Offset: 0x0038C44C
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (RadListViewFilterExpression)value);
		}

		// Token: 0x0600FCC2 RID: 64706 RVA: 0x0038E25B File Offset: 0x0038C45B
		void IList.Remove(object value)
		{
			this._expressions.Remove((RadListViewFilterExpression)value);
		}

		// Token: 0x0600FCC3 RID: 64707 RVA: 0x0038E26F File Offset: 0x0038C46F
		public bool Contains(RadListViewFilterExpression item)
		{
			return this._expressions.Contains(item);
		}

		// Token: 0x0600FCC4 RID: 64708 RVA: 0x0038E27D File Offset: 0x0038C47D
		public void CopyTo(RadListViewFilterExpression[] array, int arrayIndex)
		{
			this._expressions.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600FCC5 RID: 64709 RVA: 0x0038E28C File Offset: 0x0038C48C
		public bool Remove(RadListViewFilterExpression item)
		{
			return this._expressions.Remove(item);
		}

		// Token: 0x0600FCC6 RID: 64710 RVA: 0x0038E29A File Offset: 0x0038C49A
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._expressions).CopyTo(array, index);
		}

		// Token: 0x17004C57 RID: 19543
		// (get) Token: 0x0600FCC7 RID: 64711 RVA: 0x0038E2A9 File Offset: 0x0038C4A9
		public int Count
		{
			get
			{
				return this._expressions.Count;
			}
		}

		// Token: 0x17004C58 RID: 19544
		// (get) Token: 0x0600FCC8 RID: 64712 RVA: 0x0038E2B6 File Offset: 0x0038C4B6
		object ICollection.SyncRoot
		{
			get
			{
				return ((ICollection)this._expressions).SyncRoot;
			}
		}

		// Token: 0x17004C59 RID: 19545
		// (get) Token: 0x0600FCC9 RID: 64713 RVA: 0x0038E2C3 File Offset: 0x0038C4C3
		bool ICollection.IsSynchronized
		{
			get
			{
				return ((ICollection)this._expressions).IsSynchronized;
			}
		}

		// Token: 0x17004C5A RID: 19546
		// (get) Token: 0x0600FCCA RID: 64714 RVA: 0x0038E2D0 File Offset: 0x0038C4D0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004C5B RID: 19547
		// (get) Token: 0x0600FCCB RID: 64715 RVA: 0x0038E2D3 File Offset: 0x0038C4D3
		bool IList.IsFixedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600FCCC RID: 64716 RVA: 0x0038E2DA File Offset: 0x0038C4DA
		public int IndexOf(RadListViewFilterExpression item)
		{
			return this._expressions.IndexOf(item);
		}

		// Token: 0x0600FCCD RID: 64717 RVA: 0x0038E2E8 File Offset: 0x0038C4E8
		public void Insert(int index, RadListViewFilterExpression item)
		{
			((IStateManager)item).TrackViewState();
			this._expressions.Insert(index, item);
		}

		// Token: 0x0600FCCE RID: 64718 RVA: 0x0038E2FD File Offset: 0x0038C4FD
		public void RemoveAt(int index)
		{
			this._expressions.RemoveAt(index);
		}

		// Token: 0x17004C5C RID: 19548
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (RadListViewFilterExpression)value;
			}
		}

		// Token: 0x17004C5D RID: 19549
		public RadListViewFilterExpression this[int index]
		{
			get
			{
				return this._expressions[index];
			}
			set
			{
				((IStateManager)value).TrackViewState();
				this._expressions[index] = value;
			}
		}

		// Token: 0x0600FCD3 RID: 64723 RVA: 0x0038E346 File Offset: 0x0038C546
		Type IHideObjectMembers.GetType()
		{
			return base.GetType();
		}

		// Token: 0x040047C9 RID: 18377
		private bool _isTrackingViewState;

		// Token: 0x040047CA RID: 18378
		private List<RadListViewFilterExpression> _expressions;

		// Token: 0x040047CB RID: 18379
		private bool _isMarked;

		// Token: 0x040047CC RID: 18380
		private int _notTrackedExpressionCount;

		// Token: 0x040047CD RID: 18381
		private RadListViewFilterExpressionFluentBuilder _fluentBuilder;

		// Token: 0x040047CE RID: 18382
		private ListViewFilterExpressionContainerHelper _containerHelper;
	}
}
