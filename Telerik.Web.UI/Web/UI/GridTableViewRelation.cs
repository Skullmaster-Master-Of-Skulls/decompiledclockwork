using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200119A RID: 4506
	public class GridTableViewRelation : CollectionBase, IStateManager
	{
		// Token: 0x0600B90A RID: 47370 RVA: 0x0028F220 File Offset: 0x0028D420
		public GridTableViewRelation()
		{
		}

		// Token: 0x0600B90B RID: 47371 RVA: 0x0028F228 File Offset: 0x0028D428
		public GridTableViewRelation(GridTableViewRelation value)
		{
			this.AddRange(value);
		}

		// Token: 0x0600B90C RID: 47372 RVA: 0x0028F237 File Offset: 0x0028D437
		public GridTableViewRelation(GridRelationFields[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x17003BD3 RID: 15315
		public GridRelationFields this[int index]
		{
			get
			{
				return (GridRelationFields)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600B90F RID: 47375 RVA: 0x0028F268 File Offset: 0x0028D468
		public int Add(GridRelationFields value)
		{
			return base.List.Add(value);
		}

		// Token: 0x0600B910 RID: 47376 RVA: 0x0028F278 File Offset: 0x0028D478
		public void AddRange(GridRelationFields[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600B911 RID: 47377 RVA: 0x0028F2A0 File Offset: 0x0028D4A0
		public void AddRange(GridTableViewRelation value)
		{
			for (int i = 0; i < value.Count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x0600B912 RID: 47378 RVA: 0x0028F2CC File Offset: 0x0028D4CC
		public bool Contains(GridRelationFields value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x0600B913 RID: 47379 RVA: 0x0028F2DA File Offset: 0x0028D4DA
		public void CopyTo(GridRelationFields[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600B914 RID: 47380 RVA: 0x0028F2E9 File Offset: 0x0028D4E9
		public int IndexOf(GridRelationFields value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x0600B915 RID: 47381 RVA: 0x0028F2F7 File Offset: 0x0028D4F7
		public void Insert(int index, GridRelationFields value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x0600B916 RID: 47382 RVA: 0x0028F306 File Offset: 0x0028D506
		public new GridTableViewRelation.GridRelationFieldsEnumerator GetEnumerator()
		{
			return new GridTableViewRelation.GridRelationFieldsEnumerator(this);
		}

		// Token: 0x0600B917 RID: 47383 RVA: 0x0028F30E File Offset: 0x0028D50E
		public void Remove(GridRelationFields value)
		{
			base.List.Remove(value);
		}

		// Token: 0x0600B918 RID: 47384 RVA: 0x0028F31C File Offset: 0x0028D51C
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		// Token: 0x0600B919 RID: 47385 RVA: 0x0028F31E File Offset: 0x0028D51E
		protected override void OnInsert(int index, object value)
		{
		}

		// Token: 0x0600B91A RID: 47386 RVA: 0x0028F320 File Offset: 0x0028D520
		protected override void OnClear()
		{
		}

		// Token: 0x0600B91B RID: 47387 RVA: 0x0028F322 File Offset: 0x0028D522
		protected override void OnRemove(int index, object value)
		{
		}

		// Token: 0x0600B91C RID: 47388 RVA: 0x0028F324 File Offset: 0x0028D524
		protected override void OnValidate(object value)
		{
			if (!(value is GridRelationFields))
			{
				throw new GridException("Only objects of type GridRelationFields allowed");
			}
			if (this.IsTrackingViewState)
			{
				(value as IStateManager).TrackViewState();
				return;
			}
		}

		// Token: 0x0600B91D RID: 47389 RVA: 0x0028F350 File Offset: 0x0028D550
		public void LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			int num = (int)array[0];
			object[] array2 = (object[])array[1];
			for (int i = 0; i < num; i++)
			{
				if (i < base.Count)
				{
					((IStateManager)this[i]).LoadViewState(array2[i]);
				}
				else
				{
					GridRelationFields gridRelationFields = new GridRelationFields();
					((IStateManager)gridRelationFields).TrackViewState();
					this.Add(gridRelationFields);
					((IStateManager)gridRelationFields).LoadViewState(array2[i]);
				}
			}
		}

		// Token: 0x0600B91E RID: 47390 RVA: 0x0028F3C4 File Offset: 0x0028D5C4
		public object SaveViewState()
		{
			if (base.Count == 0 || !this.marked)
			{
				return null;
			}
			object[] array = new object[2];
			array[0] = base.Count;
			object[] array2 = new object[base.Count];
			int num = 0;
			foreach (object obj in this)
			{
				array2[num] = ((IStateManager)obj).SaveViewState();
				num++;
			}
			array[1] = array2;
			return array;
		}

		// Token: 0x0600B91F RID: 47391 RVA: 0x0028F460 File Offset: 0x0028D660
		public void TrackViewState()
		{
			this.marked = true;
			foreach (object obj in this)
			{
				((IStateManager)obj).TrackViewState();
			}
		}

		// Token: 0x17003BD4 RID: 15316
		// (get) Token: 0x0600B920 RID: 47392 RVA: 0x0028F4BC File Offset: 0x0028D6BC
		public bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x0600B921 RID: 47393 RVA: 0x0028F4C4 File Offset: 0x0028D6C4
		public GridTableViewRelation Clone()
		{
			GridTableViewRelation gridTableViewRelation = new GridTableViewRelation();
			foreach (GridRelationFields gridRelationFields in this)
			{
				gridTableViewRelation.Add(new GridRelationFields
				{
					MasterKeyField = gridRelationFields.MasterKeyField,
					DetailKeyField = gridRelationFields.DetailKeyField
				});
			}
			return gridTableViewRelation;
		}

		// Token: 0x040030F1 RID: 12529
		private bool marked;

		// Token: 0x0200119B RID: 4507
		public class GridRelationFieldsEnumerator : IEnumerator
		{
			// Token: 0x0600B922 RID: 47394 RVA: 0x0028F53C File Offset: 0x0028D73C
			public GridRelationFieldsEnumerator(GridTableViewRelation mappings)
			{
				this.temp = mappings;
				this.baseEnumerator = this.temp.GetEnumerator();
			}

			// Token: 0x17003BD5 RID: 15317
			// (get) Token: 0x0600B923 RID: 47395 RVA: 0x0028F55C File Offset: 0x0028D75C
			public GridRelationFields Current
			{
				get
				{
					return (GridRelationFields)this.baseEnumerator.Current;
				}
			}

			// Token: 0x17003BD6 RID: 15318
			// (get) Token: 0x0600B924 RID: 47396 RVA: 0x0028F56E File Offset: 0x0028D76E
			object IEnumerator.Current
			{
				get
				{
					return this.baseEnumerator.Current;
				}
			}

			// Token: 0x0600B925 RID: 47397 RVA: 0x0028F57B File Offset: 0x0028D77B
			public bool MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600B926 RID: 47398 RVA: 0x0028F588 File Offset: 0x0028D788
			bool IEnumerator.MoveNext()
			{
				return this.baseEnumerator.MoveNext();
			}

			// Token: 0x0600B927 RID: 47399 RVA: 0x0028F595 File Offset: 0x0028D795
			public void Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x0600B928 RID: 47400 RVA: 0x0028F5A2 File Offset: 0x0028D7A2
			void IEnumerator.Reset()
			{
				this.baseEnumerator.Reset();
			}

			// Token: 0x040030F2 RID: 12530
			private IEnumerator baseEnumerator;

			// Token: 0x040030F3 RID: 12531
			private IEnumerable temp;
		}
	}
}
