using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003CA RID: 970
	public sealed class DataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x06002EA8 RID: 11944 RVA: 0x00098BEF File Offset: 0x00096DEF
		public DataKeyArray(ArrayList keys)
		{
			this._keys = keys;
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x00098BFE File Offset: 0x00096DFE
		public int Count
		{
			get
			{
				return this._keys.Count;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06002EAA RID: 11946 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000D5B RID: 3419
		public DataKey this[int index]
		{
			get
			{
				return this._keys[index] as DataKey;
			}
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x00095DD9 File Offset: 0x00093FD9
		public void CopyTo(DataKey[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x00098C20 File Offset: 0x00096E20
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x00098C50 File Offset: 0x00096E50
		public IEnumerator GetEnumerator()
		{
			return this._keys.GetEnumerator();
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x00098C5D File Offset: 0x00096E5D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x00098C68 File Offset: 0x00096E68
		void IStateManager.LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						((IStateManager)this._keys[i]).LoadViewState(array[i]);
					}
				}
			}
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x00098CAC File Offset: 0x00096EAC
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

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00098D00 File Offset: 0x00096F00
		void IStateManager.TrackViewState()
		{
			this._isTracking = true;
			int count = this._keys.Count;
			for (int i = 0; i < count; i++)
			{
				((IStateManager)this._keys[i]).TrackViewState();
			}
		}

		// Token: 0x04002001 RID: 8193
		private ArrayList _keys;

		// Token: 0x04002002 RID: 8194
		private bool _isTracking;
	}
}
