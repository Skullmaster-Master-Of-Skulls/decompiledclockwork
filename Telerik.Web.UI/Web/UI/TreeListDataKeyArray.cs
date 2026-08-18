using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001269 RID: 4713
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	public class TreeListDataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600C401 RID: 50177 RVA: 0x002BE99C File Offset: 0x002BCB9C
		public TreeListDataKeyArray(List<DataKey> dataKeys)
		{
			this._dataKeys = dataKeys;
		}

		// Token: 0x0600C402 RID: 50178 RVA: 0x002BE9AB File Offset: 0x002BCBAB
		public IEnumerator GetEnumerator()
		{
			return this._dataKeys.GetEnumerator();
		}

		// Token: 0x0600C403 RID: 50179 RVA: 0x002BE9BD File Offset: 0x002BCBBD
		public void CopyTo(DataKey[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600C404 RID: 50180 RVA: 0x002BE9C8 File Offset: 0x002BCBC8
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17003F1F RID: 16159
		// (get) Token: 0x0600C405 RID: 50181 RVA: 0x002BE9F8 File Offset: 0x002BCBF8
		public int Count
		{
			get
			{
				return this._dataKeys.Count;
			}
		}

		// Token: 0x17003F20 RID: 16160
		// (get) Token: 0x0600C406 RID: 50182 RVA: 0x002BEA05 File Offset: 0x002BCC05
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17003F21 RID: 16161
		// (get) Token: 0x0600C407 RID: 50183 RVA: 0x002BEA08 File Offset: 0x002BCC08
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003F22 RID: 16162
		public DataKey this[int index]
		{
			get
			{
				return this._dataKeys[index];
			}
		}

		// Token: 0x0600C409 RID: 50185 RVA: 0x002BEA1C File Offset: 0x002BCC1C
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

		// Token: 0x0600C40A RID: 50186 RVA: 0x002BEA74 File Offset: 0x002BCC74
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

		// Token: 0x0600C40B RID: 50187 RVA: 0x002BEACB File Offset: 0x002BCCCB
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			this._dataKeys.ForEach(delegate(DataKey key)
			{
				((IStateManager)key).TrackViewState();
			});
		}

		// Token: 0x17003F23 RID: 16163
		// (get) Token: 0x0600C40C RID: 50188 RVA: 0x002BEAFC File Offset: 0x002BCCFC
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x040033EE RID: 13294
		private readonly List<DataKey> _dataKeys;

		// Token: 0x040033EF RID: 13295
		private bool _isTrackingViewState;
	}
}
