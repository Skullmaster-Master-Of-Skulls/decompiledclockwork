using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001287 RID: 4743
	[Serializable]
	internal class TreeListControlStateManager : IStateManager
	{
		// Token: 0x0600C5D5 RID: 50645 RVA: 0x002C31D8 File Offset: 0x002C13D8
		public TreeListControlStateManager()
		{
			this._dictionary = new Dictionary<string, object>();
		}

		// Token: 0x17003FDF RID: 16351
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

		// Token: 0x0600C5D8 RID: 50648 RVA: 0x002C3218 File Offset: 0x002C1418
		public bool ContainsKey(string name)
		{
			return this._dictionary.ContainsKey(name);
		}

		// Token: 0x17003FE0 RID: 16352
		// (get) Token: 0x0600C5D9 RID: 50649 RVA: 0x002C3226 File Offset: 0x002C1426
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x0600C5DA RID: 50650 RVA: 0x002C3234 File Offset: 0x002C1434
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

		// Token: 0x0600C5DB RID: 50651 RVA: 0x002C3290 File Offset: 0x002C1490
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

		// Token: 0x0600C5DC RID: 50652 RVA: 0x002C331C File Offset: 0x002C151C
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
		}

		// Token: 0x17003FE1 RID: 16353
		// (get) Token: 0x0600C5DD RID: 50653 RVA: 0x002C3325 File Offset: 0x002C1525
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0400344A RID: 13386
		private bool _isTrackingViewState;

		// Token: 0x0400344B RID: 13387
		private Dictionary<string, object> _dictionary;
	}
}
