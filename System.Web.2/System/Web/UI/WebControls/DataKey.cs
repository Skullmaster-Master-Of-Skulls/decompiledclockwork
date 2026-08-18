using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C9 RID: 969
	public class DataKey : IStateManager, IEquatable<DataKey>
	{
		// Token: 0x06002E99 RID: 11929 RVA: 0x000988E5 File Offset: 0x00096AE5
		public DataKey(IOrderedDictionary keyTable)
		{
			this._keyTable = keyTable;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000988F4 File Offset: 0x00096AF4
		public DataKey(IOrderedDictionary keyTable, string[] keyNames) : this(keyTable)
		{
			this._keyNames = keyNames;
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x00098904 File Offset: 0x00096B04
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x17000D52 RID: 3410
		public virtual object this[int index]
		{
			get
			{
				if (this._keyTable != null)
				{
					return this._keyTable[index];
				}
				return null;
			}
		}

		// Token: 0x17000D53 RID: 3411
		public virtual object this[string name]
		{
			get
			{
				if (this._keyTable != null)
				{
					return this._keyTable[name];
				}
				return null;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002E9E RID: 11934 RVA: 0x0009893C File Offset: 0x00096B3C
		public virtual object Value
		{
			get
			{
				if (this._keyTable != null && this._keyTable.Count > 0)
				{
					return this._keyTable[0];
				}
				return null;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x00098964 File Offset: 0x00096B64
		public virtual IOrderedDictionary Values
		{
			get
			{
				if (this._keyTable == null)
				{
					return null;
				}
				if (this._keyTable is OrderedDictionary)
				{
					return ((OrderedDictionary)this._keyTable).AsReadOnly();
				}
				if (this._keyTable is ICloneable)
				{
					return (IOrderedDictionary)((ICloneable)this._keyTable).Clone();
				}
				OrderedDictionary orderedDictionary = new OrderedDictionary();
				foreach (object obj in this._keyTable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					orderedDictionary.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
				return orderedDictionary.AsReadOnly();
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x00098A24 File Offset: 0x00096C24
		protected virtual void LoadViewState(object state)
		{
			if (state != null)
			{
				if (this._keyNames != null)
				{
					object[] array = (object[])state;
					if (array[0] != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (i >= this._keyNames.Length)
							{
								return;
							}
							this._keyTable.Add(this._keyNames[i], array[i]);
						}
					}
				}
				else if (state != null)
				{
					ArrayList arrayList = state as ArrayList;
					if (arrayList == null)
					{
						throw new HttpException(SR.GetString("ViewState_InvalidViewState"));
					}
					OrderedDictionaryStateHelper.LoadViewState(this._keyTable, arrayList);
				}
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00098AA4 File Offset: 0x00096CA4
		protected virtual object SaveViewState()
		{
			int count = this._keyTable.Count;
			if (count > 0)
			{
				object obj;
				if (this._keyNames != null)
				{
					obj = new object[count];
					for (int i = 0; i < count; i++)
					{
						((object[])obj)[i] = this._keyTable[i];
					}
				}
				else
				{
					obj = OrderedDictionaryStateHelper.SaveViewState(this._keyTable);
				}
				return obj;
			}
			return null;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x00098B01 File Offset: 0x00096D01
		protected virtual void TrackViewState()
		{
			this._isTracking = true;
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x00098B0A File Offset: 0x00096D0A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x00098B12 File Offset: 0x00096D12
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x00098B1B File Offset: 0x00096D1B
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x00098B23 File Offset: 0x00096D23
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00098B2C File Offset: 0x00096D2C
		public bool Equals(DataKey other)
		{
			if (other == null)
			{
				return false;
			}
			string[] array = this._keyNames;
			string[] array2 = other._keyNames;
			if (array == null && this._keyTable != null)
			{
				array = new string[this._keyTable.Count];
				this._keyTable.Keys.CopyTo(array, 0);
			}
			if (array2 == null && this._keyTable != null)
			{
				array2 = new string[other._keyTable.Count];
				other._keyTable.Keys.CopyTo(array2, 0);
			}
			bool flag = DataBoundControlHelper.CompareStringArrays(array, array2);
			if (flag)
			{
				if (array != null && array2 != null)
				{
					foreach (string name in array)
					{
						if (!object.Equals(this[name], other[name]))
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04001FFE RID: 8190
		private IOrderedDictionary _keyTable;

		// Token: 0x04001FFF RID: 8191
		private bool _isTracking;

		// Token: 0x04002000 RID: 8192
		private string[] _keyNames;
	}
}
