using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019A0 RID: 6560
	internal class ListViewControlStateManager : IStateManager
	{
		// Token: 0x0600FDB2 RID: 64946 RVA: 0x0038FC09 File Offset: 0x0038DE09
		public ListViewControlStateManager()
		{
			this._dictionary = new Dictionary<string, object>();
		}

		// Token: 0x17004C94 RID: 19604
		public object this[string name]
		{
			get
			{
				if (this._dictionary.ContainsKey(name))
				{
					return this._dictionary[name];
				}
				return null;
			}
			set
			{
				this._dictionary[name] = value;
			}
		}

		// Token: 0x0600FDB5 RID: 64949 RVA: 0x0038FC49 File Offset: 0x0038DE49
		public bool ContainsKey(string name)
		{
			return this._dictionary.ContainsKey(name);
		}

		// Token: 0x17004C95 RID: 19605
		// (get) Token: 0x0600FDB6 RID: 64950 RVA: 0x0038FC57 File Offset: 0x0038DE57
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x0600FDB7 RID: 64951 RVA: 0x0038FC64 File Offset: 0x0038DE64
		void IStateManager.LoadViewState(object baseState)
		{
			this._dictionary.Clear();
			object[] array = baseState as object[];
			if (array == null)
			{
				return;
			}
			foreach (Pair pair in array)
			{
				if (pair != null)
				{
					this._dictionary.Add((string)pair.First, pair.Second);
				}
			}
		}

		// Token: 0x0600FDB8 RID: 64952 RVA: 0x0038FCC0 File Offset: 0x0038DEC0
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			foreach (KeyValuePair<string, object> keyValuePair in this._dictionary)
			{
				arrayList.Add(new Pair
				{
					First = keyValuePair.Key,
					Second = keyValuePair.Value
				});
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600FDB9 RID: 64953 RVA: 0x0038FD4C File Offset: 0x0038DF4C
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
		}

		// Token: 0x17004C96 RID: 19606
		// (get) Token: 0x0600FDBA RID: 64954 RVA: 0x0038FD55 File Offset: 0x0038DF55
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x04004801 RID: 18433
		private bool _isTrackingViewState;

		// Token: 0x04004802 RID: 18434
		private Dictionary<string, object> _dictionary;
	}
}
