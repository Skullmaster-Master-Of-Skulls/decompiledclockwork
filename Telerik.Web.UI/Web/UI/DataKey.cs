using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010E8 RID: 4328
	public class DataKey : Hashtable, IStateManager
	{
		// Token: 0x0600B144 RID: 45380 RVA: 0x00265ECB File Offset: 0x002640CB
		public DataKey()
		{
		}

		// Token: 0x0600B145 RID: 45381 RVA: 0x00265ED3 File Offset: 0x002640D3
		public DataKey(bool IsTrackingViewState)
		{
			this._isTracking = IsTrackingViewState;
		}

		// Token: 0x17003973 RID: 14707
		// (get) Token: 0x0600B146 RID: 45382 RVA: 0x00265EE2 File Offset: 0x002640E2
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x0600B147 RID: 45383 RVA: 0x00265EEC File Offset: 0x002640EC
		void IStateManager.LoadViewState(object state)
		{
			if (state == null)
			{
				return;
			}
			this.Clear();
			object[] array = (object[])state;
			object[] array2 = (object[])array[0];
			object[] array3 = (object[])array[1];
			for (int i = 0; i < array2.Length; i++)
			{
				this[array2[i]] = array3[i];
			}
		}

		// Token: 0x0600B148 RID: 45384 RVA: 0x00265F38 File Offset: 0x00264138
		object IStateManager.SaveViewState()
		{
			if (this.Count == 0 || !this._isTracking)
			{
				return null;
			}
			object[] array = new object[2];
			object[] array2 = new object[this.Count];
			object[] array3 = new object[this.Count];
			int num = 0;
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array2[num] = dictionaryEntry.Key;
				array3[num] = dictionaryEntry.Value;
				num++;
			}
			array[0] = array2;
			array[1] = array3;
			return array;
		}

		// Token: 0x0600B149 RID: 45385 RVA: 0x00265FE4 File Offset: 0x002641E4
		void IStateManager.TrackViewState()
		{
			this._isTracking = true;
		}

		// Token: 0x04002E81 RID: 11905
		private bool _isTracking;
	}
}
