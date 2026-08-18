using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000548 RID: 1352
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataKey : IStateManager
	{
		// Token: 0x06004290 RID: 17040 RVA: 0x00113BF1 File Offset: 0x00112BF1
		public DataKey(IOrderedDictionary keyTable)
		{
			this._keyTable = keyTable;
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x00113C00 File Offset: 0x00112C00
		public DataKey(IOrderedDictionary keyTable, string[] keyNames) : this(keyTable)
		{
			this._keyNames = keyNames;
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06004292 RID: 17042 RVA: 0x00113C10 File Offset: 0x00112C10
		protected virtual bool IsTrackingViewState
		{
			get
			{
				return this._isTracking;
			}
		}

		// Token: 0x1700101C RID: 4124
		public virtual object this[int index]
		{
			get
			{
				return this._keyTable[index];
			}
		}

		// Token: 0x1700101D RID: 4125
		public virtual object this[string name]
		{
			get
			{
				return this._keyTable[name];
			}
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x00113C34 File Offset: 0x00112C34
		public virtual object Value
		{
			get
			{
				if (this._keyTable.Count > 0)
				{
					return this._keyTable[0];
				}
				return null;
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x00113C54 File Offset: 0x00112C54
		public virtual IOrderedDictionary Values
		{
			get
			{
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

		// Token: 0x06004297 RID: 17047 RVA: 0x00113D08 File Offset: 0x00112D08
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

		// Token: 0x06004298 RID: 17048 RVA: 0x00113D88 File Offset: 0x00112D88
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

		// Token: 0x06004299 RID: 17049 RVA: 0x00113DE5 File Offset: 0x00112DE5
		protected virtual void TrackViewState()
		{
			this._isTracking = true;
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x00113DEE File Offset: 0x00112DEE
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00113DF6 File Offset: 0x00112DF6
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x00113DFF File Offset: 0x00112DFF
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x00113E07 File Offset: 0x00112E07
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0400291D RID: 10525
		private IOrderedDictionary _keyTable;

		// Token: 0x0400291E RID: 10526
		private bool _isTracking;

		// Token: 0x0400291F RID: 10527
		private string[] _keyNames;
	}
}
