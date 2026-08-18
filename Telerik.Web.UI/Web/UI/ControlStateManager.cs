using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	internal class ControlStateManager : IStateManager
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002DE33 File Offset: 0x0002C033
		public ControlStateManager()
		{
			this.dictionary = new Dictionary<string, object>();
		}

		// Token: 0x17000470 RID: 1136
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

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002DE73 File Offset: 0x0002C073
		public bool ContainsKey(string name)
		{
			return this.dictionary.ContainsKey(name);
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0002DE81 File Offset: 0x0002C081
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002DE90 File Offset: 0x0002C090
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

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002DEEC File Offset: 0x0002C0EC
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

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002DF78 File Offset: 0x0002C178
		void IStateManager.TrackViewState()
		{
			this.isTrackingViewState = true;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002DF81 File Offset: 0x0002C181
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		// Token: 0x04000328 RID: 808
		private bool isTrackingViewState;

		// Token: 0x04000329 RID: 809
		private Dictionary<string, object> dictionary;
	}
}
