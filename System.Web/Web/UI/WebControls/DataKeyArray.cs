using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000549 RID: 1353
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataKeyArray : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600429E RID: 17054 RVA: 0x00113E0F File Offset: 0x00112E0F
		public DataKeyArray(ArrayList keys)
		{
			this._keys = keys;
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x00113E1E File Offset: 0x00112E1E
		public int Count
		{
			get
			{
				return this._keys.Count;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x00113E2B File Offset: 0x00112E2B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x00113E2E File Offset: 0x00112E2E
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x00113E31 File Offset: 0x00112E31
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001025 RID: 4133
		public DataKey this[int index]
		{
			get
			{
				return this._keys[index] as DataKey;
			}
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x00113E47 File Offset: 0x00112E47
		public void CopyTo(DataKey[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x00113E54 File Offset: 0x00112E54
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x00113E84 File Offset: 0x00112E84
		public IEnumerator GetEnumerator()
		{
			return this._keys.GetEnumerator();
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x00113E91 File Offset: 0x00112E91
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x00113E9C File Offset: 0x00112E9C
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

		// Token: 0x060042A9 RID: 17065 RVA: 0x00113EE0 File Offset: 0x00112EE0
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

		// Token: 0x060042AA RID: 17066 RVA: 0x00113F34 File Offset: 0x00112F34
		void IStateManager.TrackViewState()
		{
			this._isTracking = true;
			int count = this._keys.Count;
			for (int i = 0; i < count; i++)
			{
				((IStateManager)this._keys[i]).TrackViewState();
			}
		}

		// Token: 0x04002920 RID: 10528
		private ArrayList _keys;

		// Token: 0x04002921 RID: 10529
		private bool _isTracking;
	}
}
