using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200195F RID: 6495
	[PersistChildren(false)]
	public class RadDataPagerFieldCollection : IList, ICollection, IList<RadDataPagerField>, ICollection<RadDataPagerField>, IEnumerable<RadDataPagerField>, IEnumerable, IStateManager
	{
		// Token: 0x0600FB61 RID: 64353 RVA: 0x0038A0AF File Offset: 0x003882AF
		public RadDataPagerFieldCollection(RadDataPager owner)
		{
			this._collectionItems = new List<RadDataPagerField>();
			this._owner = owner;
		}

		// Token: 0x17004BF7 RID: 19447
		public RadDataPagerField this[int index]
		{
			get
			{
				return ((IList<RadDataPagerField>)this)[index];
			}
		}

		// Token: 0x17004BF8 RID: 19448
		// (get) Token: 0x0600FB63 RID: 64355 RVA: 0x0038A0D2 File Offset: 0x003882D2
		public bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600FB64 RID: 64356 RVA: 0x0038A0DC File Offset: 0x003882DC
		public void LoadViewState(object state)
		{
			if (state != null)
			{
				Pair pair = (Pair)state;
				this._viewStateNotManagedCount = (int)pair.First;
				object[] array = pair.Second as object[];
				for (int i = 0; i < this._viewStateNotManagedCount; i++)
				{
					((IStateManager)this._collectionItems[i]).LoadViewState(array[i]);
				}
				for (int j = this._viewStateNotManagedCount; j < array.Length; j++)
				{
					Pair pair2 = array[j] as Pair;
					RadDataPagerField radDataPagerField = this.CreatePagerFieldFromTypeName((string)pair2.First);
					this.Add(radDataPagerField);
					((IStateManager)radDataPagerField).LoadViewState(pair2.Second);
				}
			}
		}

		// Token: 0x0600FB65 RID: 64357 RVA: 0x0038A180 File Offset: 0x00388380
		public object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			this._viewStateNotManagedCount = Math.Min(this._viewStateNotManagedCount, this._collectionItems.Count);
			for (int i = 0; i < this._viewStateNotManagedCount; i++)
			{
				arrayList.Add(((IStateManager)this._collectionItems[i]).SaveViewState());
			}
			for (int j = this._viewStateNotManagedCount; j < this._collectionItems.Count; j++)
			{
				RadDataPagerField radDataPagerField = this._collectionItems[j];
				arrayList.Add(new Pair(radDataPagerField.PagerType, ((IStateManager)radDataPagerField).SaveViewState()));
			}
			return new Pair(this._viewStateNotManagedCount, arrayList.ToArray(typeof(object)));
		}

		// Token: 0x0600FB66 RID: 64358 RVA: 0x0038A240 File Offset: 0x00388440
		public void TrackViewState()
		{
			this._isTrackingViewState = true;
			this._viewStateNotManagedCount = this._collectionItems.Count;
			this._collectionItems.ForEach(delegate(RadDataPagerField item)
			{
				((IStateManager)item).TrackViewState();
			});
		}

		// Token: 0x0600FB67 RID: 64359 RVA: 0x0038A290 File Offset: 0x00388490
		protected virtual RadDataPagerField CreatePagerFieldFromTypeName(string typeName)
		{
			RadDataPagerField radDataPagerField = null;
			if (typeName != null)
			{
				if (!(typeName == "RadDataPagerButtonField"))
				{
					if (!(typeName == "RadDataPagerPageSizeField"))
					{
						if (!(typeName == "RadDataPagerTemplatePageField"))
						{
							if (!(typeName == "RadDataPagerGoToPageField"))
							{
								if (!(typeName == "RadDataPagerSliderField"))
								{
									if (typeName == "RadDataPagerNumericPageSizeField")
									{
										radDataPagerField = new RadDataPagerNumericPageSizeField();
									}
								}
								else
								{
									radDataPagerField = new RadDataPagerSliderField();
								}
							}
							else
							{
								radDataPagerField = new RadDataPagerGoToPageField();
							}
						}
						else
						{
							radDataPagerField = new RadDataPagerTemplatePageField();
						}
					}
					else
					{
						radDataPagerField = new RadDataPagerPageSizeField();
					}
				}
				else
				{
					radDataPagerField = new RadDataPagerButtonField();
				}
			}
			RadDataPagerFieldCreatingEventArgs radDataPagerFieldCreatingEventArgs = new RadDataPagerFieldCreatingEventArgs(radDataPagerField, typeName);
			this._owner.CallOnFieldCreating(radDataPagerFieldCreatingEventArgs);
			radDataPagerField = radDataPagerFieldCreatingEventArgs.Field;
			if (radDataPagerField != null)
			{
				return radDataPagerField;
			}
			throw new ArgumentNullException(string.Format("Cannot create pager field with the specified type name: {0}", typeName));
		}

		// Token: 0x0600FB68 RID: 64360 RVA: 0x0038A354 File Offset: 0x00388554
		private void InsertItem(int index, RadDataPagerField item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this._isTrackingViewState)
			{
				((IStateManager)item).TrackViewState();
			}
			item.SetOwner(this._owner);
			if (index < 0)
			{
				this._collectionItems.Add(item);
			}
			else
			{
				this._collectionItems.Insert(index, item);
			}
			this._owner.CallFieldsChanged();
		}

		// Token: 0x0600FB69 RID: 64361 RVA: 0x0038A3B3 File Offset: 0x003885B3
		public void Add(RadDataPagerField item)
		{
			this.InsertItem(-1, item);
		}

		// Token: 0x0600FB6A RID: 64362 RVA: 0x0038A3BD File Offset: 0x003885BD
		public void Clear()
		{
			this._collectionItems.Clear();
		}

		// Token: 0x0600FB6B RID: 64363 RVA: 0x0038A3CA File Offset: 0x003885CA
		public bool Contains(RadDataPagerField item)
		{
			return this._collectionItems.Contains(item);
		}

		// Token: 0x0600FB6C RID: 64364 RVA: 0x0038A3D8 File Offset: 0x003885D8
		public void CopyTo(RadDataPagerField[] array, int arrayIndex)
		{
			this._collectionItems.CopyTo(array, arrayIndex);
		}

		// Token: 0x17004BF9 RID: 19449
		// (get) Token: 0x0600FB6D RID: 64365 RVA: 0x0038A3E7 File Offset: 0x003885E7
		public int Count
		{
			get
			{
				return this._collectionItems.Count;
			}
		}

		// Token: 0x17004BFA RID: 19450
		// (get) Token: 0x0600FB6E RID: 64366 RVA: 0x0038A3F4 File Offset: 0x003885F4
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600FB6F RID: 64367 RVA: 0x0038A3F8 File Offset: 0x003885F8
		public bool Remove(RadDataPagerField item)
		{
			bool result = this._collectionItems.Remove(item);
			this._owner.CallFieldsChanged();
			return result;
		}

		// Token: 0x0600FB70 RID: 64368 RVA: 0x0038A41E File Offset: 0x0038861E
		public IEnumerator<RadDataPagerField> GetEnumerator()
		{
			return this._collectionItems.GetEnumerator();
		}

		// Token: 0x0600FB71 RID: 64369 RVA: 0x0038A430 File Offset: 0x00388630
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600FB72 RID: 64370 RVA: 0x0038A438 File Offset: 0x00388638
		public int IndexOf(RadDataPagerField item)
		{
			return this._collectionItems.IndexOf(item);
		}

		// Token: 0x0600FB73 RID: 64371 RVA: 0x0038A446 File Offset: 0x00388646
		public void Insert(int index, RadDataPagerField item)
		{
			this.InsertItem(index, item);
		}

		// Token: 0x0600FB74 RID: 64372 RVA: 0x0038A450 File Offset: 0x00388650
		public void RemoveAt(int index)
		{
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this._collectionItems.RemoveAt(index);
			this._owner.CallFieldsChanged();
		}

		// Token: 0x17004BFB RID: 19451
		RadDataPagerField IList<RadDataPagerField>.this[int index]
		{
			get
			{
				RadDataPagerField result;
				try
				{
					result = this._collectionItems[index];
				}
				catch (ArgumentOutOfRangeException inner)
				{
					throw new GridException("Failed accessing DataPagerField by index. Please verify that you have specified the structure of RadDataPager correctly.", inner);
				}
				return result;
			}
			set
			{
				this._collectionItems[index] = value;
			}
		}

		// Token: 0x0600FB77 RID: 64375 RVA: 0x0038A4D0 File Offset: 0x003886D0
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17004BFC RID: 19452
		// (get) Token: 0x0600FB78 RID: 64376 RVA: 0x0038A500 File Offset: 0x00388700
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004BFD RID: 19453
		// (get) Token: 0x0600FB79 RID: 64377 RVA: 0x0038A503 File Offset: 0x00388703
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600FB7A RID: 64378 RVA: 0x0038A506 File Offset: 0x00388706
		int IList.Add(object value)
		{
			this.InsertItem(-1, (RadDataPagerField)value);
			return this._collectionItems.Count - 1;
		}

		// Token: 0x0600FB7B RID: 64379 RVA: 0x0038A522 File Offset: 0x00388722
		bool IList.Contains(object value)
		{
			return this.Contains((RadDataPagerField)value);
		}

		// Token: 0x0600FB7C RID: 64380 RVA: 0x0038A530 File Offset: 0x00388730
		int IList.IndexOf(object value)
		{
			return this.IndexOf((RadDataPagerField)value);
		}

		// Token: 0x0600FB7D RID: 64381 RVA: 0x0038A53E File Offset: 0x0038873E
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, (RadDataPagerField)value);
		}

		// Token: 0x17004BFE RID: 19454
		// (get) Token: 0x0600FB7E RID: 64382 RVA: 0x0038A54D File Offset: 0x0038874D
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600FB7F RID: 64383 RVA: 0x0038A550 File Offset: 0x00388750
		void IList.Remove(object value)
		{
			this.Remove((RadDataPagerField)value);
		}

		// Token: 0x0600FB80 RID: 64384 RVA: 0x0038A55F File Offset: 0x0038875F
		void IList.RemoveAt(int index)
		{
			((IList<RadDataPagerField>)this).RemoveAt(index);
		}

		// Token: 0x17004BFF RID: 19455
		object IList.this[int index]
		{
			get
			{
				return ((IList<RadDataPagerField>)this)[index];
			}
			set
			{
				((IList<RadDataPagerField>)this)[index] = (RadDataPagerField)value;
			}
		}

		// Token: 0x04004781 RID: 18305
		private RadDataPager _owner;

		// Token: 0x04004782 RID: 18306
		private List<RadDataPagerField> _collectionItems;

		// Token: 0x04004783 RID: 18307
		private bool _isTrackingViewState;

		// Token: 0x04004784 RID: 18308
		private int _viewStateNotManagedCount;
	}
}
