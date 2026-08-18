using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020005C2 RID: 1474
	[PersistChildren(false)]
	public class MediaPlayerFilesCollection : IList, ICollection, IList<MediaPlayerFile>, ICollection<MediaPlayerFile>, IEnumerable<MediaPlayerFile>, IEnumerable, IStateManager
	{
		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000AE016 File Offset: 0x000AC216
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x000AE01E File Offset: 0x000AC21E
		public RadMediaPlayer Owner { get; internal set; }

		// Token: 0x06003491 RID: 13457 RVA: 0x000AE027 File Offset: 0x000AC227
		public MediaPlayerFilesCollection(RadMediaPlayer owner)
		{
			this.files = new List<MediaPlayerFile>();
			this.Owner = owner;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000AE041 File Offset: 0x000AC241
		internal MediaPlayerFilesCollection() : this(null)
		{
		}

		// Token: 0x1700112C RID: 4396
		public MediaPlayerFile this[int index]
		{
			get
			{
				return ((IList<MediaPlayerFile>)this)[index];
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000AE054 File Offset: 0x000AC254
		private void InsertInternal(int index, MediaPlayerFile item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			item.SetOwner(this.Owner);
			if (this.isTrackingViewState)
			{
				((IStateManager)item).TrackViewState();
			}
			if (index < 0)
			{
				this.files.Add(item);
				return;
			}
			this.files.Insert(index, item);
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000AE0A8 File Offset: 0x000AC2A8
		private bool RemoveInternal(int index, MediaPlayerFile item)
		{
			bool result;
			if (index < 0)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item", "Value cannot be null.");
				}
				result = this.files.Remove(item);
			}
			else
			{
				this.files.RemoveAt(index);
				result = true;
			}
			return result;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000AE0EC File Offset: 0x000AC2EC
		public int IndexOf(MediaPlayerFile item)
		{
			return this.files.IndexOf(item);
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000AE0FA File Offset: 0x000AC2FA
		public void Insert(int index, MediaPlayerFile item)
		{
			this.InsertInternal(index, item);
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000AE104 File Offset: 0x000AC304
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x1700112D RID: 4397
		MediaPlayerFile IList<MediaPlayerFile>.this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.files.Count == 0)
				{
					throw new NullReferenceException("Files collection is empty.");
				}
				return this.files[index];
			}
			set
			{
				this.files[index] = value;
			}
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000AE14E File Offset: 0x000AC34E
		public void Add(MediaPlayerFile item)
		{
			this.InsertInternal(-1, item);
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000AE158 File Offset: 0x000AC358
		public void Clear()
		{
			this.files.Clear();
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000AE165 File Offset: 0x000AC365
		public bool Contains(MediaPlayerFile item)
		{
			return this.files.Contains(item);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000AE173 File Offset: 0x000AC373
		public void CopyTo(MediaPlayerFile[] array, int arrayIndex)
		{
			this.files.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000AE182 File Offset: 0x000AC382
		public int Count
		{
			get
			{
				return this.files.Count;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060034A0 RID: 13472 RVA: 0x000AE18F File Offset: 0x000AC38F
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000AE192 File Offset: 0x000AC392
		public bool Remove(MediaPlayerFile item)
		{
			return this.RemoveInternal(-1, item);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000AE19C File Offset: 0x000AC39C
		public IEnumerator<MediaPlayerFile> GetEnumerator()
		{
			return this.files.GetEnumerator();
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000AE1AE File Offset: 0x000AC3AE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000AE1B6 File Offset: 0x000AC3B6
		int IList.Add(object value)
		{
			this.InsertInternal(-1, (MediaPlayerFile)value);
			return this.Count - 1;
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000AE1CD File Offset: 0x000AC3CD
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000AE1D5 File Offset: 0x000AC3D5
		bool IList.Contains(object value)
		{
			return this.Contains((MediaPlayerFile)value);
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000AE1E3 File Offset: 0x000AC3E3
		int IList.IndexOf(object value)
		{
			return this.IndexOf((MediaPlayerFile)value);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000AE1F1 File Offset: 0x000AC3F1
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (MediaPlayerFile)value);
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060034A9 RID: 13481 RVA: 0x000AE200 File Offset: 0x000AC400
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x000AE203 File Offset: 0x000AC403
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000AE20B File Offset: 0x000AC40B
		void IList.Remove(object value)
		{
			this.Remove((MediaPlayerFile)value);
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x000AE21A File Offset: 0x000AC41A
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17001132 RID: 4402
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				((IList<MediaPlayerFile>)this)[index] = (MediaPlayerFile)value;
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000AE23C File Offset: 0x000AC43C
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x000AE26C File Offset: 0x000AC46C
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x000AE274 File Offset: 0x000AC474
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x000AE277 File Offset: 0x000AC477
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000AE27A File Offset: 0x000AC47A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000AE284 File Offset: 0x000AC484
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
					Pair pair = array[num3 + 1] as Pair;
					if (pair != null)
					{
						MediaPlayerFile mediaPlayerFile = this[num3];
						((IStateManager)mediaPlayerFile).LoadViewState(pair.Second);
					}
					num3++;
				}
				int num4 = num2;
				while (num4 < num && num4 < array.Length)
				{
					Pair pair2 = array[num4 + 1] as Pair;
					if (pair2 != null)
					{
						object first = pair2.First;
						if (first != null)
						{
							MediaPlayerFile mediaPlayerFile2 = this.Owner.CreateFileByType((string)first);
							if (mediaPlayerFile2 != null)
							{
								this.Add(mediaPlayerFile2);
								((IStateManager)mediaPlayerFile2).LoadViewState(pair2.Second);
							}
						}
					}
					num4++;
				}
			}
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000AE36C File Offset: 0x000AC56C
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.Count, this._notTrackedColumnsCount));
			bool flag = false;
			foreach (MediaPlayerFile mediaPlayerFile in this)
			{
				arrayList.Add(new Pair
				{
					First = mediaPlayerFile.FileType,
					Second = ((IStateManager)mediaPlayerFile).SaveViewState()
				});
				flag = true;
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000AE424 File Offset: 0x000AC624
		void IStateManager.TrackViewState()
		{
			if (this.isMarked)
			{
				return;
			}
			this.isMarked = true;
			this._notTrackedColumnsCount = this.Count;
			this.isTrackingViewState = true;
			this.files.ForEach(delegate(MediaPlayerFile item)
			{
				((IStateManager)item).TrackViewState();
			});
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000AE498 File Offset: 0x000AC698
		public List<MediaPlayerFile> GetFilesByType(string fileType)
		{
			return (from f in this.files
			where f.FileType == fileType
			select f).ToList<MediaPlayerFile>();
		}

		// Token: 0x04000E47 RID: 3655
		private List<MediaPlayerFile> files;

		// Token: 0x04000E48 RID: 3656
		private bool isTrackingViewState;

		// Token: 0x04000E49 RID: 3657
		private int _notTrackedColumnsCount;

		// Token: 0x04000E4A RID: 3658
		private bool isMarked;
	}
}
