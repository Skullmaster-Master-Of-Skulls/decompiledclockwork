using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200012A RID: 298
	public class AutoCompleteStringCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x17000289 RID: 649
		public string this[int index]
		{
			get
			{
				return (string)this.data[index];
			}
			set
			{
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, this.data[index]));
				this.data[index] = value;
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00019E37 File Offset: 0x00018037
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000983 RID: 2435 RVA: 0x00019E44 File Offset: 0x00018044
		// (remove) Token: 0x06000984 RID: 2436 RVA: 0x00019E5D File Offset: 0x0001805D
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanged, value);
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00019E76 File Offset: 0x00018076
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00019E90 File Offset: 0x00018090
		public int Add(string value)
		{
			int result = this.data.Add(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			return result;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00019EB8 File Offset: 0x000180B8
		public void AddRange(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.data.AddRange(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00019EE1 File Offset: 0x000180E1
		public void Clear()
		{
			this.data.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00019EFB File Offset: 0x000180FB
		public bool Contains(string value)
		{
			return this.data.Contains(value);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00019F09 File Offset: 0x00018109
		public void CopyTo(string[] array, int index)
		{
			this.data.CopyTo(array, index);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00019F18 File Offset: 0x00018118
		public int IndexOf(string value)
		{
			return this.data.IndexOf(value);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00019F26 File Offset: 0x00018126
		public void Insert(int index, string value)
		{
			this.data.Insert(index, value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00011A20 File Offset: 0x0000FC20
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00011A20 File Offset: 0x0000FC20
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00019F42 File Offset: 0x00018142
		public void Remove(string value)
		{
			this.data.Remove(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, value));
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00019F60 File Offset: 0x00018160
		public void RemoveAt(int index)
		{
			string element = (string)this.data[index];
			this.data.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, element));
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00006C59 File Offset: 0x00004E59
		public object SyncRoot
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				return this;
			}
		}

		// Token: 0x17000290 RID: 656
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00019FB0 File Offset: 0x000181B0
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00019FBE File Offset: 0x000181BE
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00019FCC File Offset: 0x000181CC
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00019FDA File Offset: 0x000181DA
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00019FE9 File Offset: 0x000181E9
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00019F09 File Offset: 0x00018109
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00019FF7 File Offset: 0x000181F7
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x0400061E RID: 1566
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x0400061F RID: 1567
		private ArrayList data = new ArrayList();
	}
}
