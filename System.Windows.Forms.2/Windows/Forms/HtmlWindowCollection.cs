using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000287 RID: 647
	public class HtmlWindowCollection : ICollection, IEnumerable
	{
		// Token: 0x06002985 RID: 10629 RVA: 0x000BDC53 File Offset: 0x000BBE53
		internal HtmlWindowCollection(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLFramesCollection2 collection)
		{
			this.htmlFramesCollection2 = collection;
			this.shimManager = shimManager;
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000BDC69 File Offset: 0x000BBE69
		private UnsafeNativeMethods.IHTMLFramesCollection2 NativeHTMLFramesCollection2
		{
			get
			{
				return this.htmlFramesCollection2;
			}
		}

		// Token: 0x170009B8 RID: 2488
		public HtmlWindow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidBoundArgument", new object[]
					{
						"index",
						index,
						0,
						this.Count - 1
					}));
				}
				object obj = index;
				UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = this.NativeHTMLFramesCollection2.Item(ref obj) as UnsafeNativeMethods.IHTMLWindow2;
				if (ihtmlwindow == null)
				{
					return null;
				}
				return new HtmlWindow(this.shimManager, ihtmlwindow);
			}
		}

		// Token: 0x170009B9 RID: 2489
		public HtmlWindow this[string windowId]
		{
			get
			{
				object obj = windowId;
				UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = null;
				try
				{
					ihtmlwindow = (this.htmlFramesCollection2.Item(ref obj) as UnsafeNativeMethods.IHTMLWindow2);
				}
				catch (COMException)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"windowId",
						windowId
					}));
				}
				if (ihtmlwindow == null)
				{
					return null;
				}
				return new HtmlWindow(this.shimManager, ihtmlwindow);
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x000BDD6C File Offset: 0x000BBF6C
		public int Count
		{
			get
			{
				return this.NativeHTMLFramesCollection2.GetLength();
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000BDD7C File Offset: 0x000BBF7C
		void ICollection.CopyTo(Array dest, int index)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				dest.SetValue(this[i], index++);
			}
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000BDDB0 File Offset: 0x000BBFB0
		public IEnumerator GetEnumerator()
		{
			HtmlWindow[] array = new HtmlWindow[this.Count];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		// Token: 0x040010EE RID: 4334
		private UnsafeNativeMethods.IHTMLFramesCollection2 htmlFramesCollection2;

		// Token: 0x040010EF RID: 4335
		private HtmlShimManager shimManager;
	}
}
