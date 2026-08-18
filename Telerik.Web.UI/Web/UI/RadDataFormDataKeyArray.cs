using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000208 RID: 520
	public class RadDataFormDataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600133F RID: 4927 RVA: 0x000442CC File Offset: 0x000424CC
		public RadDataFormDataKeyArray(List<DataKey> dataKeys)
		{
			this._dataKeys = dataKeys;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000442DB File Offset: 0x000424DB
		public IEnumerator GetEnumerator()
		{
			return this._dataKeys.GetEnumerator();
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000442ED File Offset: 0x000424ED
		public void CopyTo(DataKey[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000442F8 File Offset: 0x000424F8
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x00044328 File Offset: 0x00042528
		public int Count
		{
			get
			{
				return this._dataKeys.Count;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00044335 File Offset: 0x00042535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x00044338 File Offset: 0x00042538
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000657 RID: 1623
		public DataKey this[int index]
		{
			get
			{
				return this._dataKeys[index];
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0004434C File Offset: 0x0004254C
		void IStateManager.LoadViewState(object state)
		{
			this._dataKeys.Clear();
			if (state != null)
			{
				object[] array = (object[])state;
				for (int i = 0; i < array.Length; i++)
				{
					DataKey dataKey = new DataKey(((IStateManager)this).IsTrackingViewState);
					this._dataKeys.Add(dataKey);
					if (array[i] != null)
					{
						((IStateManager)dataKey).LoadViewState(array[i]);
					}
				}
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000443A4 File Offset: 0x000425A4
		object IStateManager.SaveViewState()
		{
			int count = this._dataKeys.Count;
			object[] array = new object[count];
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				array[i] = ((IStateManager)this._dataKeys[i]).SaveViewState();
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

		// Token: 0x06001349 RID: 4937 RVA: 0x000443FB File Offset: 0x000425FB
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			this._dataKeys.ForEach(delegate(DataKey key)
			{
				((IStateManager)key).TrackViewState();
			});
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0004442C File Offset: 0x0004262C
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x04000565 RID: 1381
		private readonly List<DataKey> _dataKeys;

		// Token: 0x04000566 RID: 1382
		private bool _isTrackingViewState;
	}
}
