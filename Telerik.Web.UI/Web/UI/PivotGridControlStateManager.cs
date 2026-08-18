using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000DF2 RID: 3570
	[Serializable]
	internal class PivotGridControlStateManager : IStateManager
	{
		// Token: 0x060084A7 RID: 33959 RVA: 0x001E4520 File Offset: 0x001E2720
		public PivotGridControlStateManager()
		{
			this._dictionary = new Dictionary<string, object>();
		}

		// Token: 0x170029F4 RID: 10740
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

		// Token: 0x060084AA RID: 33962 RVA: 0x001E4560 File Offset: 0x001E2760
		public bool ContainsKey(string name)
		{
			return this._dictionary.ContainsKey(name);
		}

		// Token: 0x170029F5 RID: 10741
		// (get) Token: 0x060084AB RID: 33963 RVA: 0x001E456E File Offset: 0x001E276E
		public int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		// Token: 0x060084AC RID: 33964 RVA: 0x001E457C File Offset: 0x001E277C
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

		// Token: 0x060084AD RID: 33965 RVA: 0x001E45D8 File Offset: 0x001E27D8
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

		// Token: 0x060084AE RID: 33966 RVA: 0x001E4664 File Offset: 0x001E2864
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
		}

		// Token: 0x170029F6 RID: 10742
		// (get) Token: 0x060084AF RID: 33967 RVA: 0x001E466D File Offset: 0x001E286D
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x040024F5 RID: 9461
		private bool _isTrackingViewState;

		// Token: 0x040024F6 RID: 9462
		private Dictionary<string, object> _dictionary;
	}
}
