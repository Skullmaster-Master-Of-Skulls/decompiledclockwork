using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000286 RID: 646
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class HtmlWindow
	{
		// Token: 0x0600294A RID: 10570 RVA: 0x000BD630 File Offset: 0x000BB830
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		internal HtmlWindow(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLWindow2 win)
		{
			this.htmlWindow2 = win;
			this.shimManager = shimManager;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x000BD646 File Offset: 0x000BB846
		internal UnsafeNativeMethods.IHTMLWindow2 NativeHtmlWindow
		{
			get
			{
				return this.htmlWindow2;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000BD64E File Offset: 0x000BB84E
		private HtmlShimManager ShimManager
		{
			get
			{
				return this.shimManager;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x000BD658 File Offset: 0x000BB858
		private HtmlWindow.HtmlWindowShim WindowShim
		{
			get
			{
				if (this.ShimManager != null)
				{
					HtmlWindow.HtmlWindowShim windowShim = this.ShimManager.GetWindowShim(this);
					if (windowShim == null)
					{
						this.shimManager.AddWindowShim(this);
						windowShim = this.ShimManager.GetWindowShim(this);
					}
					return windowShim;
				}
				return null;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x000BD69C File Offset: 0x000BB89C
		public HtmlDocument Document
		{
			get
			{
				UnsafeNativeMethods.IHTMLDocument ihtmldocument = this.NativeHtmlWindow.GetDocument() as UnsafeNativeMethods.IHTMLDocument;
				if (ihtmldocument == null)
				{
					return null;
				}
				return new HtmlDocument(this.ShimManager, ihtmldocument);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000BD6CB File Offset: 0x000BB8CB
		public object DomWindow
		{
			get
			{
				return this.NativeHtmlWindow;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x000BD6D4 File Offset: 0x000BB8D4
		public HtmlWindowCollection Frames
		{
			get
			{
				UnsafeNativeMethods.IHTMLFramesCollection2 frames = this.NativeHtmlWindow.GetFrames();
				if (frames == null)
				{
					return null;
				}
				return new HtmlWindowCollection(this.ShimManager, frames);
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x000BD700 File Offset: 0x000BB900
		public HtmlHistory History
		{
			get
			{
				UnsafeNativeMethods.IOmHistory history = this.NativeHtmlWindow.GetHistory();
				if (history == null)
				{
					return null;
				}
				return new HtmlHistory(history);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x000BD724 File Offset: 0x000BB924
		public bool IsClosed
		{
			get
			{
				return this.NativeHtmlWindow.GetClosed();
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x000BD731 File Offset: 0x000BB931
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x000BD73E File Offset: 0x000BB93E
		public string Name
		{
			get
			{
				return this.NativeHtmlWindow.GetName();
			}
			set
			{
				this.NativeHtmlWindow.SetName(value);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x000BD74C File Offset: 0x000BB94C
		public HtmlWindow Opener
		{
			get
			{
				UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = this.NativeHtmlWindow.GetOpener() as UnsafeNativeMethods.IHTMLWindow2;
				if (ihtmlwindow == null)
				{
					return null;
				}
				return new HtmlWindow(this.ShimManager, ihtmlwindow);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x000BD77C File Offset: 0x000BB97C
		public HtmlWindow Parent
		{
			get
			{
				UnsafeNativeMethods.IHTMLWindow2 parent = this.NativeHtmlWindow.GetParent();
				if (parent == null)
				{
					return null;
				}
				return new HtmlWindow(this.ShimManager, parent);
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x000BD7A6 File Offset: 0x000BB9A6
		public Point Position
		{
			get
			{
				return new Point(((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).GetScreenLeft(), ((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).GetScreenTop());
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002958 RID: 10584 RVA: 0x000BD7D0 File Offset: 0x000BB9D0
		// (set) Token: 0x06002959 RID: 10585 RVA: 0x000BD7FF File Offset: 0x000BB9FF
		public Size Size
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement body = this.NativeHtmlWindow.GetDocument().GetBody();
				return new Size(body.GetOffsetWidth(), body.GetOffsetHeight());
			}
			set
			{
				this.ResizeTo(value.Width, value.Height);
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x000BD815 File Offset: 0x000BBA15
		// (set) Token: 0x0600295B RID: 10587 RVA: 0x000BD822 File Offset: 0x000BBA22
		public string StatusBarText
		{
			get
			{
				return this.NativeHtmlWindow.GetStatus();
			}
			set
			{
				this.NativeHtmlWindow.SetStatus(value);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600295C RID: 10588 RVA: 0x000BD830 File Offset: 0x000BBA30
		public Uri Url
		{
			get
			{
				UnsafeNativeMethods.IHTMLLocation location = this.NativeHtmlWindow.GetLocation();
				string text = (location == null) ? "" : location.GetHref();
				if (!string.IsNullOrEmpty(text))
				{
					return new Uri(text);
				}
				return null;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000BD86C File Offset: 0x000BBA6C
		public HtmlElement WindowFrameElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement ihtmlelement = ((UnsafeNativeMethods.IHTMLWindow4)this.NativeHtmlWindow).frameElement() as UnsafeNativeMethods.IHTMLElement;
				if (ihtmlelement == null)
				{
					return null;
				}
				return new HtmlElement(this.ShimManager, ihtmlelement);
			}
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000BD8A0 File Offset: 0x000BBAA0
		public void Alert(string message)
		{
			this.NativeHtmlWindow.Alert(message);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000BD8AE File Offset: 0x000BBAAE
		public void AttachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.WindowShim.AttachEventHandler(eventName, eventHandler);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000BD8BD File Offset: 0x000BBABD
		public void Close()
		{
			this.NativeHtmlWindow.Close();
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000BD8CA File Offset: 0x000BBACA
		public bool Confirm(string message)
		{
			return this.NativeHtmlWindow.Confirm(message);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000BD8D8 File Offset: 0x000BBAD8
		public void DetachEventHandler(string eventName, EventHandler eventHandler)
		{
			this.WindowShim.DetachEventHandler(eventName, eventHandler);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000BD8E7 File Offset: 0x000BBAE7
		public void Focus()
		{
			this.NativeHtmlWindow.Focus();
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000BD8F4 File Offset: 0x000BBAF4
		public void MoveTo(int x, int y)
		{
			this.NativeHtmlWindow.MoveTo(x, y);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000BD903 File Offset: 0x000BBB03
		public void MoveTo(Point point)
		{
			this.NativeHtmlWindow.MoveTo(point.X, point.Y);
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x000BD91E File Offset: 0x000BBB1E
		public void Navigate(Uri url)
		{
			this.NativeHtmlWindow.Navigate(url.ToString());
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x000BD931 File Offset: 0x000BBB31
		public void Navigate(string urlString)
		{
			this.NativeHtmlWindow.Navigate(urlString);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000BD940 File Offset: 0x000BBB40
		public HtmlWindow Open(string urlString, string target, string windowOptions, bool replaceEntry)
		{
			UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = this.NativeHtmlWindow.Open(urlString, target, windowOptions, replaceEntry);
			if (ihtmlwindow == null)
			{
				return null;
			}
			return new HtmlWindow(this.ShimManager, ihtmlwindow);
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000BD96F File Offset: 0x000BBB6F
		public HtmlWindow Open(Uri url, string target, string windowOptions, bool replaceEntry)
		{
			return this.Open(url.ToString(), target, windowOptions, replaceEntry);
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x000BD984 File Offset: 0x000BBB84
		public HtmlWindow OpenNew(string urlString, string windowOptions)
		{
			UnsafeNativeMethods.IHTMLWindow2 ihtmlwindow = this.NativeHtmlWindow.Open(urlString, "_blank", windowOptions, true);
			if (ihtmlwindow == null)
			{
				return null;
			}
			return new HtmlWindow(this.ShimManager, ihtmlwindow);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x000BD9B6 File Offset: 0x000BBBB6
		public HtmlWindow OpenNew(Uri url, string windowOptions)
		{
			return this.OpenNew(url.ToString(), windowOptions);
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x000BD9C5 File Offset: 0x000BBBC5
		public string Prompt(string message, string defaultInputValue)
		{
			return this.NativeHtmlWindow.Prompt(message, defaultInputValue).ToString();
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000BD9D9 File Offset: 0x000BBBD9
		public void RemoveFocus()
		{
			this.NativeHtmlWindow.Blur();
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000BD9E6 File Offset: 0x000BBBE6
		public void ResizeTo(int width, int height)
		{
			this.NativeHtmlWindow.ResizeTo(width, height);
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x000BD9F5 File Offset: 0x000BBBF5
		public void ResizeTo(Size size)
		{
			this.NativeHtmlWindow.ResizeTo(size.Width, size.Height);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000BDA10 File Offset: 0x000BBC10
		public void ScrollTo(int x, int y)
		{
			this.NativeHtmlWindow.ScrollTo(x, y);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000BDA1F File Offset: 0x000BBC1F
		public void ScrollTo(Point point)
		{
			this.NativeHtmlWindow.ScrollTo(point.X, point.Y);
		}

		// Token: 0x140001E2 RID: 482
		// (add) Token: 0x06002972 RID: 10610 RVA: 0x000BDA3A File Offset: 0x000BBC3A
		// (remove) Token: 0x06002973 RID: 10611 RVA: 0x000BDA4D File Offset: 0x000BBC4D
		public event HtmlElementErrorEventHandler Error
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventError, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventError, value);
			}
		}

		// Token: 0x140001E3 RID: 483
		// (add) Token: 0x06002974 RID: 10612 RVA: 0x000BDA60 File Offset: 0x000BBC60
		// (remove) Token: 0x06002975 RID: 10613 RVA: 0x000BDA73 File Offset: 0x000BBC73
		public event HtmlElementEventHandler GotFocus
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventGotFocus, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventGotFocus, value);
			}
		}

		// Token: 0x140001E4 RID: 484
		// (add) Token: 0x06002976 RID: 10614 RVA: 0x000BDA86 File Offset: 0x000BBC86
		// (remove) Token: 0x06002977 RID: 10615 RVA: 0x000BDA99 File Offset: 0x000BBC99
		public event HtmlElementEventHandler Load
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventLoad, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventLoad, value);
			}
		}

		// Token: 0x140001E5 RID: 485
		// (add) Token: 0x06002978 RID: 10616 RVA: 0x000BDAAC File Offset: 0x000BBCAC
		// (remove) Token: 0x06002979 RID: 10617 RVA: 0x000BDABF File Offset: 0x000BBCBF
		public event HtmlElementEventHandler LostFocus
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventLostFocus, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventLostFocus, value);
			}
		}

		// Token: 0x140001E6 RID: 486
		// (add) Token: 0x0600297A RID: 10618 RVA: 0x000BDAD2 File Offset: 0x000BBCD2
		// (remove) Token: 0x0600297B RID: 10619 RVA: 0x000BDAE5 File Offset: 0x000BBCE5
		public event HtmlElementEventHandler Resize
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventResize, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventResize, value);
			}
		}

		// Token: 0x140001E7 RID: 487
		// (add) Token: 0x0600297C RID: 10620 RVA: 0x000BDAF8 File Offset: 0x000BBCF8
		// (remove) Token: 0x0600297D RID: 10621 RVA: 0x000BDB0B File Offset: 0x000BBD0B
		public event HtmlElementEventHandler Scroll
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventScroll, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventScroll, value);
			}
		}

		// Token: 0x140001E8 RID: 488
		// (add) Token: 0x0600297E RID: 10622 RVA: 0x000BDB1E File Offset: 0x000BBD1E
		// (remove) Token: 0x0600297F RID: 10623 RVA: 0x000BDB31 File Offset: 0x000BBD31
		public event HtmlElementEventHandler Unload
		{
			add
			{
				this.WindowShim.AddHandler(HtmlWindow.EventUnload, value);
			}
			remove
			{
				this.WindowShim.RemoveHandler(HtmlWindow.EventUnload, value);
			}
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000BDB44 File Offset: 0x000BBD44
		public static bool operator ==(HtmlWindow left, HtmlWindow right)
		{
			if (left == null != (right == null))
			{
				return false;
			}
			if (left == null)
			{
				return true;
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			bool result;
			try
			{
				intPtr = Marshal.GetIUnknownForObject(left.NativeHtmlWindow);
				intPtr2 = Marshal.GetIUnknownForObject(right.NativeHtmlWindow);
				result = (intPtr == intPtr2);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.Release(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.Release(intPtr2);
				}
			}
			return result;
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000BDBCC File Offset: 0x000BBDCC
		public static bool operator !=(HtmlWindow left, HtmlWindow right)
		{
			return !(left == right);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000BDBD8 File Offset: 0x000BBDD8
		public override int GetHashCode()
		{
			if (this.htmlWindow2 != null)
			{
				return this.htmlWindow2.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000BDBEF File Offset: 0x000BBDEF
		public override bool Equals(object obj)
		{
			return this == (HtmlWindow)obj;
		}

		// Token: 0x040010E5 RID: 4325
		internal static readonly object EventError = new object();

		// Token: 0x040010E6 RID: 4326
		internal static readonly object EventGotFocus = new object();

		// Token: 0x040010E7 RID: 4327
		internal static readonly object EventLoad = new object();

		// Token: 0x040010E8 RID: 4328
		internal static readonly object EventLostFocus = new object();

		// Token: 0x040010E9 RID: 4329
		internal static readonly object EventResize = new object();

		// Token: 0x040010EA RID: 4330
		internal static readonly object EventScroll = new object();

		// Token: 0x040010EB RID: 4331
		internal static readonly object EventUnload = new object();

		// Token: 0x040010EC RID: 4332
		private HtmlShimManager shimManager;

		// Token: 0x040010ED RID: 4333
		private UnsafeNativeMethods.IHTMLWindow2 htmlWindow2;

		// Token: 0x020006AA RID: 1706
		[ClassInterface(ClassInterfaceType.None)]
		private class HTMLWindowEvents2 : StandardOleMarshalObject, UnsafeNativeMethods.DHTMLWindowEvents2
		{
			// Token: 0x0600687E RID: 26750 RVA: 0x001851AA File Offset: 0x001833AA
			public HTMLWindowEvents2(HtmlWindow htmlWindow)
			{
				this.parent = htmlWindow;
			}

			// Token: 0x0600687F RID: 26751 RVA: 0x001851B9 File Offset: 0x001833B9
			private void FireEvent(object key, EventArgs e)
			{
				if (this.parent != null)
				{
					this.parent.WindowShim.FireEvent(key, e);
				}
			}

			// Token: 0x06006880 RID: 26752 RVA: 0x001851DC File Offset: 0x001833DC
			public void onfocus(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventGotFocus, e);
			}

			// Token: 0x06006881 RID: 26753 RVA: 0x00185208 File Offset: 0x00183408
			public void onblur(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventLostFocus, e);
			}

			// Token: 0x06006882 RID: 26754 RVA: 0x00185234 File Offset: 0x00183434
			public bool onerror(string description, string urlString, int line)
			{
				HtmlElementErrorEventArgs htmlElementErrorEventArgs = new HtmlElementErrorEventArgs(description, urlString, line);
				this.FireEvent(HtmlWindow.EventError, htmlElementErrorEventArgs);
				return htmlElementErrorEventArgs.Handled;
			}

			// Token: 0x06006883 RID: 26755 RVA: 0x0018525C File Offset: 0x0018345C
			public void onload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventLoad, e);
			}

			// Token: 0x06006884 RID: 26756 RVA: 0x00185288 File Offset: 0x00183488
			public void onunload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventUnload, e);
				if (this.parent != null)
				{
					this.parent.WindowShim.OnWindowUnload();
				}
			}

			// Token: 0x06006885 RID: 26757 RVA: 0x001852D4 File Offset: 0x001834D4
			public void onscroll(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventScroll, e);
			}

			// Token: 0x06006886 RID: 26758 RVA: 0x00185300 File Offset: 0x00183500
			public void onresize(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs e = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				this.FireEvent(HtmlWindow.EventResize, e);
			}

			// Token: 0x06006887 RID: 26759 RVA: 0x0018532C File Offset: 0x0018352C
			public bool onhelp(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
				HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs(this.parent.ShimManager, evtObj);
				return htmlElementEventArgs.ReturnValue;
			}

			// Token: 0x06006888 RID: 26760 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onbeforeunload(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x06006889 RID: 26761 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onbeforeprint(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x0600688A RID: 26762 RVA: 0x000072B6 File Offset: 0x000054B6
			public void onafterprint(UnsafeNativeMethods.IHTMLEventObj evtObj)
			{
			}

			// Token: 0x04003AF1 RID: 15089
			private HtmlWindow parent;
		}

		// Token: 0x020006AB RID: 1707
		internal class HtmlWindowShim : HtmlShim
		{
			// Token: 0x0600688B RID: 26763 RVA: 0x00185351 File Offset: 0x00183551
			public HtmlWindowShim(HtmlWindow window)
			{
				this.htmlWindow = window;
			}

			// Token: 0x17001692 RID: 5778
			// (get) Token: 0x0600688C RID: 26764 RVA: 0x00185360 File Offset: 0x00183560
			public UnsafeNativeMethods.IHTMLWindow2 NativeHtmlWindow
			{
				get
				{
					return this.htmlWindow.NativeHtmlWindow;
				}
			}

			// Token: 0x17001693 RID: 5779
			// (get) Token: 0x0600688D RID: 26765 RVA: 0x00185360 File Offset: 0x00183560
			public override UnsafeNativeMethods.IHTMLWindow2 AssociatedWindow
			{
				get
				{
					return this.htmlWindow.NativeHtmlWindow;
				}
			}

			// Token: 0x0600688E RID: 26766 RVA: 0x00185370 File Offset: 0x00183570
			public override void AttachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy pdisp = base.AddEventProxy(eventName, eventHandler);
				bool flag = ((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).AttachEvent(eventName, pdisp);
			}

			// Token: 0x0600688F RID: 26767 RVA: 0x0018539C File Offset: 0x0018359C
			public override void ConnectToEvents()
			{
				if (this.cookie == null || !this.cookie.Connected)
				{
					this.cookie = new AxHost.ConnectionPointCookie(this.NativeHtmlWindow, new HtmlWindow.HTMLWindowEvents2(this.htmlWindow), typeof(UnsafeNativeMethods.DHTMLWindowEvents2), false);
					if (!this.cookie.Connected)
					{
						this.cookie = null;
					}
				}
			}

			// Token: 0x06006890 RID: 26768 RVA: 0x001853FC File Offset: 0x001835FC
			public override void DetachEventHandler(string eventName, EventHandler eventHandler)
			{
				HtmlToClrEventProxy htmlToClrEventProxy = base.RemoveEventProxy(eventHandler);
				if (htmlToClrEventProxy != null)
				{
					((UnsafeNativeMethods.IHTMLWindow3)this.NativeHtmlWindow).DetachEvent(eventName, htmlToClrEventProxy);
				}
			}

			// Token: 0x06006891 RID: 26769 RVA: 0x00185426 File Offset: 0x00183626
			public override void DisconnectFromEvents()
			{
				if (this.cookie != null)
				{
					this.cookie.Disconnect();
					this.cookie = null;
				}
			}

			// Token: 0x06006892 RID: 26770 RVA: 0x00185442 File Offset: 0x00183642
			protected override void Dispose(bool disposing)
			{
				base.Dispose(disposing);
				if (disposing)
				{
					if (this.htmlWindow != null && this.htmlWindow.NativeHtmlWindow != null)
					{
						Marshal.FinalReleaseComObject(this.htmlWindow.NativeHtmlWindow);
					}
					this.htmlWindow = null;
				}
			}

			// Token: 0x06006893 RID: 26771 RVA: 0x00185481 File Offset: 0x00183681
			protected override object GetEventSender()
			{
				return this.htmlWindow;
			}

			// Token: 0x06006894 RID: 26772 RVA: 0x00185489 File Offset: 0x00183689
			public void OnWindowUnload()
			{
				if (this.htmlWindow != null)
				{
					this.htmlWindow.ShimManager.OnWindowUnloaded(this.htmlWindow);
				}
			}

			// Token: 0x04003AF2 RID: 15090
			private AxHost.ConnectionPointCookie cookie;

			// Token: 0x04003AF3 RID: 15091
			private HtmlWindow htmlWindow;
		}
	}
}
