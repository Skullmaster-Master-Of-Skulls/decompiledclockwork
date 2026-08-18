using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200119F RID: 4511
	[Serializable]
	public class GridGroupByExpressionCollection : CollectionBase
	{
		// Token: 0x0600B954 RID: 47444 RVA: 0x002908EB File Offset: 0x0028EAEB
		public GridGroupByExpressionCollection()
		{
		}

		// Token: 0x0600B955 RID: 47445 RVA: 0x002908F3 File Offset: 0x0028EAF3
		public GridGroupByExpressionCollection(GridGroupByExpressionCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x0600B956 RID: 47446 RVA: 0x00290902 File Offset: 0x0028EB02
		public GridGroupByExpressionCollection(GridGroupByExpression[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17003BE0 RID: 15328
		[NotifyParentProperty(true)]
		public GridGroupByExpression this[int index]
		{
			get
			{
				return (GridGroupByExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600B959 RID: 47449 RVA: 0x00290934 File Offset: 0x0028EB34
		public int Add(GridGroupByExpression value)
		{
			foreach (GridGroupByExpression gridGroupByExpression in this)
			{
				if (gridGroupByExpression.ContainsSameGroupByField(value))
				{
					gridGroupByExpression.CopyFrom(value);
					return gridGroupByExpression.Index;
				}
			}
			return base.List.Add(value);
		}

		// Token: 0x0600B95A RID: 47450 RVA: 0x002909A4 File Offset: 0x0028EBA4
		public int Add(string value)
		{
			GridGroupByExpression value2 = new GridGroupByExpression(value);
			return this.Add(value2);
		}

		// Token: 0x0600B95B RID: 47451 RVA: 0x002909C0 File Offset: 0x0028EBC0
		public void AddRange(GridGroupByExpression[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600B95C RID: 47452 RVA: 0x002909E8 File Offset: 0x0028EBE8
		public void AddRange(GridGroupByExpressionCollection value)
		{
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600B95D RID: 47453 RVA: 0x00290A14 File Offset: 0x0028EC14
		public bool Contains(GridGroupByExpression value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600B95E RID: 47454 RVA: 0x00290A22 File Offset: 0x0028EC22
		public void CopyTo(GridGroupByExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600B95F RID: 47455 RVA: 0x00290A31 File Offset: 0x0028EC31
		public int IndexOf(GridGroupByExpression value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600B960 RID: 47456 RVA: 0x00290A40 File Offset: 0x0028EC40
		private void RecalcIndexes(int fromIndex)
		{
			for (int i = fromIndex; i < base.Count; i++)
			{
				this[i].SetIndex(i);
			}
		}

		// Token: 0x0600B961 RID: 47457 RVA: 0x00290A6B File Offset: 0x0028EC6B
		public void Insert(int index, GridGroupByExpression value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600B962 RID: 47458 RVA: 0x00290A7A File Offset: 0x0028EC7A
		public new GridGroupByExpressionCollection.GridGroupByExpressionEnumerator GetEnumerator()
		{
			return new GridGroupByExpressionCollection.GridGroupByExpressionEnumerator(this);
		}

		// Token: 0x0600B963 RID: 47459 RVA: 0x00290A82 File Offset: 0x0028EC82
		public void Remove(GridGroupByExpression value)
		{
			base.List.Remove(value);
		}

		// Token: 0x0600B964 RID: 47460 RVA: 0x00290A90 File Offset: 0x0028EC90
		public void Remove(string value)
		{
			GridGroupByExpression expression = new GridGroupByExpression(value);
			GridGroupByExpression gridGroupByExpression = null;
			foreach (GridGroupByExpression gridGroupByExpression2 in this)
			{
				if (gridGroupByExpression2.IsSame(expression))
				{
					gridGroupByExpression = gridGroupByExpression2;
					break;
				}
			}
			if (gridGroupByExpression != null)
			{
				this.Remove(gridGroupByExpression);
			}
		}

		// Token: 0x0600B965 RID: 47461 RVA: 0x00290AFC File Offset: 0x0028ECFC
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			this.RecalcIndexes(index);
		}

		// Token: 0x0600B966 RID: 47462 RVA: 0x00290B0D File Offset: 0x0028ED0D
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			this.RecalcIndexes(index);
		}

		// Token: 0x0600B967 RID: 47463 RVA: 0x00290B20 File Offset: 0x0028ED20
		internal void CopyTo(GridGroupByExpressionCollection dest)
		{
			foreach (GridGroupByExpression gridGroupByExpression in this)
			{
				dest.Add(gridGroupByExpression.Clone());
			}
		}

		// Token: 0x020011A0 RID: 4512
		public class GridGroupByExpressionEnumerator : IEnumerator
		{
			// Token: 0x0600B968 RID: 47464 RVA: 0x00290B78 File Offset: 0x0028ED78
			public GridGroupByExpressionEnumerator(GridGroupByExpressionCollection mappings)
			{
				this.temp = mappings;
				this.baseEnumerator = this.temp.GetEnumerator();
			}

			// Token: 0x17003BE1 RID: 15329
			// (get) Token: 0x0600B969 RID: 47465 RVA: 0x00290B98 File Offset: 0x0028ED98
			public GridGroupByExpression Current
			{
				get
				{
					return (GridGroupByExpression)this.baseEnumerator.Current;
				}
			}

			// Token: 0x17003BE2 RID: 15330
			// (get) Token: 0x0600B96A RID: 47466 RVA: 0x00290BAA File Offset: 0x0028EDAA
			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}

			// Token: 0x0600B96B RID: 47467 RVA: 0x00290BB7 File Offset: 0x0028EDB7
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600B96C RID: 47468 RVA: 0x00290BC4 File Offset: 0x0028EDC4
			bool IEnumerator.MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600B96D RID: 47469 RVA: 0x00290BD1 File Offset: 0x0028EDD1
			public void Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x0600B96E RID: 47470 RVA: 0x00290BDE File Offset: 0x0028EDDE
			void IEnumerator.Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x040030FA RID: 12538
			private IEnumerator baseEnumerator;

			// Token: 0x040030FB RID: 12539
			private IEnumerable temp;
		}
	}
}
