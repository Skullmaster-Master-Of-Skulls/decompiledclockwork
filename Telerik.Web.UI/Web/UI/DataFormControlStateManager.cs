using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020001E5 RID: 485
	internal class DataFormControlStateManager : IStateManager
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x0003EBCD File Offset: 0x0003CDCD
		public DataFormControlStateManager()
		{
			this._dictionary = new Dictionary<string, object>();
		}

		// Token: 0x170005BE RID: 1470
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

		// Token: 0x06001125 RID: 4389 RVA: 0x0003EC0D File Offset: 0x0003CE0D
		public bool ContainsKey(string name)
		{
			return this._dictionary.ContainsKey(name);
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0003EC1B File Offset: 0x0003CE1B
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003EC28 File Offset: 0x0003CE28
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

		// Token: 0x06001128 RID: 4392 RVA: 0x0003EC84 File Offset: 0x0003CE84
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

		// Token: 0x06001129 RID: 4393 RVA: 0x0003ED10 File Offset: 0x0003CF10
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003ED19 File Offset: 0x0003CF19
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x040004E9 RID: 1257
		private bool _isTrackingViewState;

		// Token: 0x040004EA RID: 1258
		private Dictionary<string, object> _dictionary;
	}
}
