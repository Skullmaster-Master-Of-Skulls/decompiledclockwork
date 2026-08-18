using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000566 RID: 1382
	[Serializable]
	internal class LightBoxControlStateManager : IStateManager
	{
		// Token: 0x060031CE RID: 12750 RVA: 0x000A3519 File Offset: 0x000A1719
		public LightBoxControlStateManager()
		{
			this.dictionary = new Dictionary<string, object>();
		}

		// Token: 0x17001026 RID: 4134
		public object this[string name]
		{
			get
			{
				if (this.dictionary.ContainsKey(name))
				{
					return this.dictionary[name];
				}
				return null;
			}
			set
			{
				this.dictionary[name] = value;
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000A3559 File Offset: 0x000A1759
		public bool ContainsKey(string name)
		{
			return this.dictionary.ContainsKey(name);
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000A3567 File Offset: 0x000A1767
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000A3574 File Offset: 0x000A1774
		void IStateManager.LoadViewState(object baseState)
		{
			this.dictionary.Clear();
			object[] array = baseState as object[];
			if (array == null)
			{
				return;
			}
			foreach (Pair pair in array)
			{
				if (pair != null)
				{
					this.dictionary.Add((string)pair.First, pair.Second);
				}
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000A35D0 File Offset: 0x000A17D0
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			foreach (KeyValuePair<string, object> keyValuePair in this.dictionary)
			{
				arrayList.Add(new Pair
				{
					First = keyValuePair.Key,
					Second = keyValuePair.Value
				});
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000A365C File Offset: 0x000A185C
		void IStateManager.TrackViewState()
		{
			this.isTrackingViewState = true;
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000A3665 File Offset: 0x000A1865
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		// Token: 0x04000D74 RID: 3444
		private bool isTrackingViewState;

		// Token: 0x04000D75 RID: 3445
		private Dictionary<string, object> dictionary;
	}
}
