using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005C4 RID: 1476
	[PersistChildren(false)]
	public class MediaPlayerSourcesCollection : IList, ICollection, IList<MediaPlayerSource>, ICollection<MediaPlayerSource>, IEnumerable<MediaPlayerSource>, IEnumerable, IStateManager
	{
		// Token: 0x060034C0 RID: 13504 RVA: 0x000AE5C8 File Offset: 0x000AC7C8
		public MediaPlayerSourcesCollection()
		{
			this.sources = new List<MediaPlayerSource>();
		}

		// Token: 0x1700113A RID: 4410
		public MediaPlayerSource this[int index]
		{
			get
			{
				return ((IList<MediaPlayerSource>)this)[index];
			}
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000AE5E4 File Offset: 0x000AC7E4
		private void InsertInternal(int index, MediaPlayerSource item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.isTrackingViewState)
			{
				((IStateManager)item).TrackViewState();
			}
			if (index < 0)
			{
				this.sources.Add(item);
				return;
			}
			this.sources.Insert(index, item);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000AE620 File Offset: 0x000AC820
		private bool RemoveInternal(int index, MediaPlayerSource item)
		{
			bool result;
			if (index < 0)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item", "Value cannot be null.");
				}
				result = this.sources.Remove(item);
			}
			else
			{
				this.sources.RemoveAt(index);
				result = true;
			}
			return result;
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000AE664 File Offset: 0x000AC864
		public int IndexOf(MediaPlayerSource item)
		{
			return this.sources.IndexOf(item);
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000AE672 File Offset: 0x000AC872
		public void Insert(int index, MediaPlayerSource item)
		{
			this.InsertInternal(index, item);
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x000AE67C File Offset: 0x000AC87C
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x1700113B RID: 4411
		MediaPlayerSource IList<MediaPlayerSource>.this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.sources.Count == 0)
				{
					throw new NullReferenceException("Files collection is empty.");
				}
				return this.sources[index];
			}
			set
			{
				this.sources[index] = value;
			}
		}

		// Token: 0x060034C9 RID: 13513 RVA: 0x000AE6C6 File Offset: 0x000AC8C6
		public void Add(MediaPlayerSource item)
		{
			this.InsertInternal(-1, item);
		}

		// Token: 0x060034CA RID: 13514 RVA: 0x000AE6D0 File Offset: 0x000AC8D0
		public void Clear()
		{
			this.sources.Clear();
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000AE6DD File Offset: 0x000AC8DD
		public bool Contains(MediaPlayerSource item)
		{
			return this.sources.Contains(item);
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000AE6EB File Offset: 0x000AC8EB
		public void CopyTo(MediaPlayerSource[] array, int arrayIndex)
		{
			this.sources.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000AE6FA File Offset: 0x000AC8FA
		public int Count
		{
			get
			{
				return this.sources.Count;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000AE707 File Offset: 0x000AC907
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034CF RID: 13519 RVA: 0x000AE70A File Offset: 0x000AC90A
		public bool Remove(MediaPlayerSource item)
		{
			return this.RemoveInternal(-1, item);
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000AE714 File Offset: 0x000AC914
		public IEnumerator<MediaPlayerSource> GetEnumerator()
		{
			return this.sources.GetEnumerator();
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000AE726 File Offset: 0x000AC926
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000AE72E File Offset: 0x000AC92E
		int IList.Add(object value)
		{
			this.InsertInternal(-1, (MediaPlayerSource)value);
			return this.Count - 1;
		}

		// Token: 0x060034D3 RID: 13523 RVA: 0x000AE745 File Offset: 0x000AC945
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000AE74D File Offset: 0x000AC94D
		bool IList.Contains(object value)
		{
			return this.Contains((MediaPlayerSource)value);
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x000AE75B File Offset: 0x000AC95B
		int IList.IndexOf(object value)
		{
			return this.IndexOf((MediaPlayerSource)value);
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000AE769 File Offset: 0x000AC969
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (MediaPlayerSource)value);
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x000AE778 File Offset: 0x000AC978
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x000AE77B File Offset: 0x000AC97B
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000AE783 File Offset: 0x000AC983
		void IList.Remove(object value)
		{
			this.Remove((MediaPlayerSource)value);
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000AE792 File Offset: 0x000AC992
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17001140 RID: 4416
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				((IList<MediaPlayerSource>)this)[index] = (MediaPlayerSource)value;
			}
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000AE7B4 File Offset: 0x000AC9B4
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000AE7E4 File Offset: 0x000AC9E4
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000AE7EC File Offset: 0x000AC9EC
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000AE7EF File Offset: 0x000AC9EF
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000AE7F2 File Offset: 0x000AC9F2
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000AE7FC File Offset: 0x000AC9FC
		void IStateManager.LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				int num = (int)((Pair)array[0]).First;
				int num2 = (int)((Pair)array[0]).Second;
				int num3 = 0;
				while (num3 < num2 && num3 < array.Length)
				{
					object obj = array[num3 + 1];
					if (obj != null)
					{
						MediaPlayerSource mediaPlayerSource = this[num3];
						((IStateManager)mediaPlayerSource).LoadViewState(obj);
					}
					num3++;
				}
				int num4 = num2;
				while (num4 < num && num4 < array.Length)
				{
					object obj2 = array[num4 + 1];
					if (obj2 != null)
					{
						MediaPlayerSource mediaPlayerSource2 = new MediaPlayerSource();
						this.Add(mediaPlayerSource2);
						((IStateManager)mediaPlayerSource2).LoadViewState(obj2);
					}
					num4++;
				}
			}
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000AE8B0 File Offset: 0x000ACAB0
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.Count, this._notTrackedColumnsCount));
			bool flag = false;
			foreach (MediaPlayerSource mediaPlayerSource in this)
			{
				object value = ((IStateManager)mediaPlayerSource).SaveViewState();
				arrayList.Add(value);
				flag = true;
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000AE950 File Offset: 0x000ACB50
		void IStateManager.TrackViewState()
		{
			if (this.isMarked)
			{
				return;
			}
			this.isMarked = true;
			this._notTrackedColumnsCount = this.Count;
			this.isTrackingViewState = true;
			this.sources.ForEach(delegate(MediaPlayerSource item)
			{
				((IStateManager)item).TrackViewState();
			});
		}

		// Token: 0x04000E4D RID: 3661
		private List<MediaPlayerSource> sources;

		// Token: 0x04000E4E RID: 3662
		private bool isTrackingViewState;

		// Token: 0x04000E4F RID: 3663
		private int _notTrackedColumnsCount;

		// Token: 0x04000E50 RID: 3664
		private bool isMarked;
	}
}
