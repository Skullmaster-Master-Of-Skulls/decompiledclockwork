using System;
using System.Collections;

namespace System.Diagnostics
{
	// Token: 0x020004B4 RID: 1204
	public class TraceListenerCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x06002CEB RID: 11499 RVA: 0x000C9FAD File Offset: 0x000C81AD
		internal TraceListenerCollection()
		{
			this.list = new ArrayList(1);
		}

		// Token: 0x17000ADF RID: 2783
		public TraceListener this[int i]
		{
			get
			{
				return (TraceListener)this.list[i];
			}
			set
			{
				this.InitializeListener(value);
				this.list[i] = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		public TraceListener this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					TraceListener traceListener = (TraceListener)obj;
					if (traceListener.Name == name)
					{
						return traceListener;
					}
				}
				return null;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000CA050 File Offset: 0x000C8250
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000CA060 File Offset: 0x000C8260
		public int Add(TraceListener listener)
		{
			this.InitializeListener(listener);
			object critSec = TraceInternal.critSec;
			int result;
			lock (critSec)
			{
				result = this.list.Add(listener);
			}
			return result;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000CA0B0 File Offset: 0x000C82B0
		public void AddRange(TraceListener[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000CA0E4 File Offset: 0x000C82E4
		public void AddRange(TraceListenerCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000CA120 File Offset: 0x000C8320
		public void Clear()
		{
			this.list = new ArrayList();
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000CA12D File Offset: 0x000C832D
		public bool Contains(TraceListener listener)
		{
			return ((IList)this).Contains(listener);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000CA136 File Offset: 0x000C8336
		public void CopyTo(TraceListener[] listeners, int index)
		{
			((ICollection)this).CopyTo(listeners, index);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000CA140 File Offset: 0x000C8340
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000CA14D File Offset: 0x000C834D
		internal void InitializeListener(TraceListener listener)
		{
			if (listener == null)
			{
				throw new ArgumentNullException("listener");
			}
			listener.IndentSize = TraceInternal.IndentSize;
			listener.IndentLevel = TraceInternal.IndentLevel;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000CA173 File Offset: 0x000C8373
		public int IndexOf(TraceListener listener)
		{
			return ((IList)this).IndexOf(listener);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000CA17C File Offset: 0x000C837C
		public void Insert(int index, TraceListener listener)
		{
			this.InitializeListener(listener);
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Insert(index, listener);
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000CA1CC File Offset: 0x000C83CC
		public void Remove(TraceListener listener)
		{
			((IList)this).Remove(listener);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000CA1D8 File Offset: 0x000C83D8
		public void Remove(string name)
		{
			TraceListener traceListener = this[name];
			if (traceListener != null)
			{
				((IList)this).Remove(traceListener);
			}
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000CA1F8 File Offset: 0x000C83F8
		public void RemoveAt(int index)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.RemoveAt(index);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				TraceListener traceListener = value as TraceListener;
				if (traceListener == null)
				{
					throw new ArgumentException(SR.GetString("MustAddListener"), "value");
				}
				this.InitializeListener(traceListener);
				this.list[index] = traceListener;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000CA290 File Offset: 0x000C8490
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000CA293 File Offset: 0x000C8493
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000CA298 File Offset: 0x000C8498
		int IList.Add(object value)
		{
			TraceListener traceListener = value as TraceListener;
			if (traceListener == null)
			{
				throw new ArgumentException(SR.GetString("MustAddListener"), "value");
			}
			this.InitializeListener(traceListener);
			object critSec = TraceInternal.critSec;
			int result;
			lock (critSec)
			{
				result = this.list.Add(value);
			}
			return result;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000CA308 File Offset: 0x000C8508
		bool IList.Contains(object value)
		{
			return this.list.Contains(value);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000CA316 File Offset: 0x000C8516
		int IList.IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000CA324 File Offset: 0x000C8524
		void IList.Insert(int index, object value)
		{
			TraceListener traceListener = value as TraceListener;
			if (traceListener == null)
			{
				throw new ArgumentException(SR.GetString("MustAddListener"), "value");
			}
			this.InitializeListener(traceListener);
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Insert(index, value);
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000CA390 File Offset: 0x000C8590
		void IList.Remove(object value)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.Remove(value);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000CA3D8 File Offset: 0x000C85D8
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x000CA3DB File Offset: 0x000C85DB
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000CA3E0 File Offset: 0x000C85E0
		void ICollection.CopyTo(Array array, int index)
		{
			object critSec = TraceInternal.critSec;
			lock (critSec)
			{
				this.list.CopyTo(array, index);
			}
		}

		// Token: 0x040026F9 RID: 9977
		private ArrayList list;
	}
}
