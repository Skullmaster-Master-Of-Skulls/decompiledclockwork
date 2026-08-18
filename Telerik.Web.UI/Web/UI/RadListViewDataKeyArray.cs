using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019B7 RID: 6583
	public class RadListViewDataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600FE7D RID: 65149 RVA: 0x00392606 File Offset: 0x00390806
		public RadListViewDataKeyArray(List<DataKey> dataKeys)
		{
			this._dataKeys = dataKeys;
		}

		// Token: 0x0600FE7E RID: 65150 RVA: 0x00392615 File Offset: 0x00390815
		public IEnumerator GetEnumerator()
		{
			return this._dataKeys.GetEnumerator();
		}

		// Token: 0x0600FE7F RID: 65151 RVA: 0x00392627 File Offset: 0x00390827
		public void CopyTo(DataKey[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600FE80 RID: 65152 RVA: 0x00392634 File Offset: 0x00390834
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17004CD4 RID: 19668
		// (get) Token: 0x0600FE81 RID: 65153 RVA: 0x00392664 File Offset: 0x00390864
		public int Count
		{
			get
			{
				return this._dataKeys.Count;
			}
		}

		// Token: 0x17004CD5 RID: 19669
		// (get) Token: 0x0600FE82 RID: 65154 RVA: 0x00392671 File Offset: 0x00390871
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17004CD6 RID: 19670
		// (get) Token: 0x0600FE83 RID: 65155 RVA: 0x00392674 File Offset: 0x00390874
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004CD7 RID: 19671
		public DataKey this[int index]
		{
			get
			{
				return this._dataKeys[index];
			}
		}

		// Token: 0x0600FE85 RID: 65157 RVA: 0x00392688 File Offset: 0x00390888
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

		// Token: 0x0600FE86 RID: 65158 RVA: 0x003926E0 File Offset: 0x003908E0
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

		// Token: 0x0600FE87 RID: 65159 RVA: 0x00392737 File Offset: 0x00390937
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			this._dataKeys.ForEach(delegate(DataKey key)
			{
				((IStateManager)key).TrackViewState();
			});
		}

		// Token: 0x17004CD8 RID: 19672
		// (get) Token: 0x0600FE88 RID: 65160 RVA: 0x00392768 File Offset: 0x00390968
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x04004832 RID: 18482
		private readonly List<DataKey> _dataKeys;

		// Token: 0x04004833 RID: 18483
		private bool _isTrackingViewState;
	}
}
