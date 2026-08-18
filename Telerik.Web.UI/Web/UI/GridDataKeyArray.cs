using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010E7 RID: 4327
	public class GridDataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600B137 RID: 45367 RVA: 0x00265D46 File Offset: 0x00263F46
		public GridDataKeyArray(ArrayList keys)
		{
			this._keys = keys;
		}

		// Token: 0x0600B138 RID: 45368 RVA: 0x00265D55 File Offset: 0x00263F55
		public void CopyTo(DataKey[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600B139 RID: 45369 RVA: 0x00265D5F File Offset: 0x00263F5F
		public IEnumerator GetEnumerator()
		{
			return this._keys.GetEnumerator();
		}

		// Token: 0x0600B13A RID: 45370 RVA: 0x00265D6C File Offset: 0x00263F6C
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x0600B13B RID: 45371 RVA: 0x00265D9C File Offset: 0x00263F9C
		void IStateManager.LoadViewState(object state)
		{
			this._keys.Clear();
			if (state != null)
			{
				object[] array = (object[])state;
				for (int i = 0; i < array.Length; i++)
				{
					DataKey value = new DataKey(((IStateManager)this).IsTrackingViewState);
					this._keys.Add(value);
					if (array[i] != null)
					{
						((IStateManager)this._keys[i]).LoadViewState(array[i]);
					}
				}
			}
		}

		// Token: 0x0600B13C RID: 45372 RVA: 0x00265E04 File Offset: 0x00264004
		object IStateManager.SaveViewState()
		{
			int count = this._keys.Count;
			object[] array = new object[count];
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				array[i] = ((IStateManager)this._keys[i]).SaveViewState();
				if (array[i] != null)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x0600B13D RID: 45373 RVA: 0x00265E58 File Offset: 0x00264058
		void IStateManager.TrackViewState()
		{
			this._isTracking = true;
			int count = this._keys.Count;
			for (int i = 0; i < count; i++)
			{
				((IStateManager)this._keys[i]).TrackViewState();
			}
		}

		// Token: 0x1700396D RID: 14701
		// (get) Token: 0x0600B13E RID: 45374 RVA: 0x00265E9A File Offset: 0x0026409A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x1700396E RID: 14702
		// (get) Token: 0x0600B13F RID: 45375 RVA: 0x00265EA2 File Offset: 0x002640A2
		public int Count
		{
			get
			{
				return this._keys.Count;
			}
		}

		// Token: 0x1700396F RID: 14703
		// (get) Token: 0x0600B140 RID: 45376 RVA: 0x00265EAF File Offset: 0x002640AF
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003970 RID: 14704
		// (get) Token: 0x0600B141 RID: 45377 RVA: 0x00265EB2 File Offset: 0x002640B2
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003971 RID: 14705
		public DataKey this[int index]
		{
			get
			{
				return this._keys[index] as DataKey;
			}
		}

		// Token: 0x17003972 RID: 14706
		// (get) Token: 0x0600B143 RID: 45379 RVA: 0x00265EC8 File Offset: 0x002640C8
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x04002E7F RID: 11903
		private bool _isTracking;

		// Token: 0x04002E80 RID: 11904
		private ArrayList _keys;
	}
}
