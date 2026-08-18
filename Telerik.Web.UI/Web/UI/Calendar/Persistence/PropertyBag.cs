using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar.Persistence
{
	// Token: 0x02001004 RID: 4100
	public sealed class PropertyBag : IDictionary, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600A021 RID: 40993 RVA: 0x0023A3D5 File Offset: 0x002385D5
		public PropertyBag()
		{
			this._PropertiesBag = this.CreatePropertiesBag();
		}

		// Token: 0x0600A022 RID: 40994 RVA: 0x0023A3E9 File Offset: 0x002385E9
		private IDictionary CreatePropertiesBag()
		{
			return new Hashtable();
		}

		// Token: 0x0600A023 RID: 40995 RVA: 0x0023A3F0 File Offset: 0x002385F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600A024 RID: 40996 RVA: 0x0023A3F8 File Offset: 0x002385F8
		public IDictionaryEnumerator GetEnumerator()
		{
			return this._PropertiesBag.GetEnumerator();
		}

		// Token: 0x1700329A RID: 12954
		// (get) Token: 0x0600A025 RID: 40997 RVA: 0x0023A405 File Offset: 0x00238605
		public int Count
		{
			get
			{
				return this._PropertiesBag.Count;
			}
		}

		// Token: 0x1700329B RID: 12955
		// (get) Token: 0x0600A026 RID: 40998 RVA: 0x0023A412 File Offset: 0x00238612
		public ICollection Keys
		{
			get
			{
				return this._PropertiesBag.Keys;
			}
		}

		// Token: 0x1700329C RID: 12956
		// (get) Token: 0x0600A027 RID: 40999 RVA: 0x0023A41F File Offset: 0x0023861F
		public ICollection Values
		{
			get
			{
				return this._PropertiesBag.Values;
			}
		}

		// Token: 0x0600A028 RID: 41000 RVA: 0x0023A42C File Offset: 0x0023862C
		void ICollection.CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x1700329D RID: 12957
		// (get) Token: 0x0600A029 RID: 41001 RVA: 0x0023A43B File Offset: 0x0023863B
		object ICollection.SyncRoot
		{
			get
			{
				return this._PropertiesBag;
			}
		}

		// Token: 0x1700329E RID: 12958
		// (get) Token: 0x0600A02A RID: 41002 RVA: 0x0023A443 File Offset: 0x00238643
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600A02B RID: 41003 RVA: 0x0023A446 File Offset: 0x00238646
		void IDictionary.Add(object key, object value)
		{
			this.Add((string)key, value, null);
		}

		// Token: 0x0600A02C RID: 41004 RVA: 0x0023A458 File Offset: 0x00238658
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public PropertyItem Add(string key, object value, object defaultValue)
		{
			if (key == null || key.Length == 0)
			{
				throw new ArgumentException("null value is not a valid key value.");
			}
			object obj = this._PropertiesBag[key];
			if (obj == null)
			{
				if (value != null && value != defaultValue)
				{
					obj = new PropertyItem(value);
					((PropertyItem)obj).IsDirty = true;
					this._PropertiesBag.Add(key, obj);
				}
			}
			else if (value == null || value == defaultValue)
			{
				this._PropertiesBag.Remove(key);
			}
			else
			{
				((PropertyItem)obj).Value = value;
				((PropertyItem)obj).IsDirty = true;
			}
			return (PropertyItem)obj;
		}

		// Token: 0x0600A02D RID: 41005 RVA: 0x0023A4E7 File Offset: 0x002386E7
		bool IDictionary.Contains(object key)
		{
			return this._PropertiesBag.Contains((string)key);
		}

		// Token: 0x0600A02E RID: 41006 RVA: 0x0023A4FA File Offset: 0x002386FA
		void IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		// Token: 0x1700329F RID: 12959
		// (get) Token: 0x0600A02F RID: 41007 RVA: 0x0023A508 File Offset: 0x00238708
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170032A0 RID: 12960
		// (get) Token: 0x0600A030 RID: 41008 RVA: 0x0023A50B File Offset: 0x0023870B
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600A031 RID: 41009 RVA: 0x0023A50E File Offset: 0x0023870E
		public void Remove(string key)
		{
			this._PropertiesBag.Remove(key);
		}

		// Token: 0x0600A032 RID: 41010 RVA: 0x0023A51C File Offset: 0x0023871C
		public void Clear()
		{
			this._PropertiesBag.Clear();
		}

		// Token: 0x170032A1 RID: 12961
		object IDictionary.this[object key]
		{
			get
			{
				string text = key as string;
				if (key == null || string.IsNullOrEmpty(text))
				{
					throw new ArgumentException("Only string values are valid keys.");
				}
				PropertyItem propertyItem = (PropertyItem)this._PropertiesBag[text];
				if (propertyItem != null)
				{
					return propertyItem.Value;
				}
				return null;
			}
			set
			{
				((IDictionary)this).Add((string)key, value);
			}
		}

		// Token: 0x0600A035 RID: 41013 RVA: 0x0023A582 File Offset: 0x00238782
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x0600A036 RID: 41014 RVA: 0x0023A58C File Offset: 0x0023878C
		internal void LoadViewState(object state)
		{
			if (state != null)
			{
				ArrayList arrayList = (ArrayList)state;
				for (int i = 0; i < arrayList.Count; i += 2)
				{
					string key = (string)arrayList[i];
					object obj;
					if (arrayList[i + 1] is Pair)
					{
						Pair pair = (Pair)arrayList[i + 1];
						Type type = Type.GetType((string)pair.First);
						obj = Activator.CreateInstance(type);
						IStateManager stateManager = obj as IStateManager;
						stateManager.TrackViewState();
						stateManager.LoadViewState(pair.Second);
					}
					else
					{
						obj = arrayList[i + 1];
					}
					((IDictionary)this).Add(key, obj);
				}
			}
		}

		// Token: 0x0600A037 RID: 41015 RVA: 0x0023A635 File Offset: 0x00238835
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600A038 RID: 41016 RVA: 0x0023A640 File Offset: 0x00238840
		internal object SaveViewState()
		{
			ArrayList arrayList = null;
			if (this._PropertiesBag.Count != 0)
			{
				IDictionaryEnumerator enumerator = this._PropertiesBag.GetEnumerator();
				while (enumerator.MoveNext())
				{
					PropertyItem propertyItem = (PropertyItem)enumerator.Value;
					if (propertyItem.IsDirty)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add((string)enumerator.Key);
						if (propertyItem.Value is IStateManager)
						{
							arrayList.Add(new Pair
							{
								First = propertyItem.Value.GetType().AssemblyQualifiedName,
								Second = ((IStateManager)propertyItem.Value).SaveViewState()
							});
						}
						else
						{
							arrayList.Add(propertyItem.Value);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600A039 RID: 41017 RVA: 0x0023A701 File Offset: 0x00238901
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0600A03A RID: 41018 RVA: 0x0023A709 File Offset: 0x00238909
		internal void TrackViewState()
		{
			this.marked = true;
		}

		// Token: 0x170032A2 RID: 12962
		// (get) Token: 0x0600A03B RID: 41019 RVA: 0x0023A712 File Offset: 0x00238912
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x170032A3 RID: 12963
		// (get) Token: 0x0600A03C RID: 41020 RVA: 0x0023A71A File Offset: 0x0023891A
		internal bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x170032A4 RID: 12964
		public object this[string key]
		{
			get
			{
				if (key == null || key.Length == 0)
				{
					throw new ArgumentException("null value is not a valid key value.");
				}
				PropertyItem propertyItem = (PropertyItem)this._PropertiesBag[key];
				if (propertyItem != null)
				{
					return propertyItem.Value;
				}
				return null;
			}
			set
			{
				((IDictionary)this).Add(key, value);
			}
		}

		// Token: 0x04002CDA RID: 11482
		private IDictionary _PropertiesBag;

		// Token: 0x04002CDB RID: 11483
		private bool marked;
	}
}
