using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000280 RID: 640
	public sealed class HtmlElementEventArgs : EventArgs
	{
		// Token: 0x060028FD RID: 10493 RVA: 0x000BCB38 File Offset: 0x000BAD38
		internal HtmlElementEventArgs(HtmlShimManager shimManager, UnsafeNativeMethods.IHTMLEventObj eventObj)
		{
			this.htmlEventObj = eventObj;
			this.shimManager = shimManager;
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x000BCB4E File Offset: 0x000BAD4E
		private UnsafeNativeMethods.IHTMLEventObj NativeHTMLEventObj
		{
			get
			{
				return this.htmlEventObj;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x000BCB58 File Offset: 0x000BAD58
		public MouseButtons MouseButtonsPressed
		{
			get
			{
				MouseButtons mouseButtons = MouseButtons.None;
				int button = this.NativeHTMLEventObj.GetButton();
				if ((button & 1) != 0)
				{
					mouseButtons |= MouseButtons.Left;
				}
				if ((button & 2) != 0)
				{
					mouseButtons |= MouseButtons.Right;
				}
				if ((button & 4) != 0)
				{
					mouseButtons |= MouseButtons.Middle;
				}
				return mouseButtons;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x000BCB9B File Offset: 0x000BAD9B
		public Point ClientMousePosition
		{
			get
			{
				return new Point(this.NativeHTMLEventObj.GetClientX(), this.NativeHTMLEventObj.GetClientY());
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x000BCBB8 File Offset: 0x000BADB8
		public Point OffsetMousePosition
		{
			get
			{
				return new Point(this.NativeHTMLEventObj.GetOffsetX(), this.NativeHTMLEventObj.GetOffsetY());
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x000BCBD5 File Offset: 0x000BADD5
		public Point MousePosition
		{
			get
			{
				return new Point(this.NativeHTMLEventObj.GetX(), this.NativeHTMLEventObj.GetY());
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x000BCBF2 File Offset: 0x000BADF2
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x000BCC02 File Offset: 0x000BAE02
		public bool BubbleEvent
		{
			get
			{
				return !this.NativeHTMLEventObj.GetCancelBubble();
			}
			set
			{
				this.NativeHTMLEventObj.SetCancelBubble(!value);
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x000BCC13 File Offset: 0x000BAE13
		public int KeyPressedCode
		{
			get
			{
				return this.NativeHTMLEventObj.GetKeyCode();
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002906 RID: 10502 RVA: 0x000BCC20 File Offset: 0x000BAE20
		public bool AltKeyPressed
		{
			get
			{
				return this.NativeHTMLEventObj.GetAltKey();
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x000BCC2D File Offset: 0x000BAE2D
		public bool CtrlKeyPressed
		{
			get
			{
				return this.NativeHTMLEventObj.GetCtrlKey();
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002908 RID: 10504 RVA: 0x000BCC3A File Offset: 0x000BAE3A
		public bool ShiftKeyPressed
		{
			get
			{
				return this.NativeHTMLEventObj.GetShiftKey();
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x000BCC47 File Offset: 0x000BAE47
		public string EventType
		{
			get
			{
				return this.NativeHTMLEventObj.GetEventType();
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x000BCC54 File Offset: 0x000BAE54
		// (set) Token: 0x0600290B RID: 10507 RVA: 0x000BCC78 File Offset: 0x000BAE78
		public bool ReturnValue
		{
			get
			{
				object returnValue = this.NativeHTMLEventObj.GetReturnValue();
				return returnValue == null || (bool)returnValue;
			}
			set
			{
				object returnValue = value;
				this.NativeHTMLEventObj.SetReturnValue(returnValue);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600290C RID: 10508 RVA: 0x000BCC98 File Offset: 0x000BAE98
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public HtmlElement FromElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement fromElement = this.NativeHTMLEventObj.GetFromElement();
				if (fromElement != null)
				{
					return new HtmlElement(this.shimManager, fromElement);
				}
				return null;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x000BCCC4 File Offset: 0x000BAEC4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public HtmlElement ToElement
		{
			get
			{
				UnsafeNativeMethods.IHTMLElement toElement = this.NativeHTMLEventObj.GetToElement();
				if (toElement != null)
				{
					return new HtmlElement(this.shimManager, toElement);
				}
				return null;
			}
		}

		// Token: 0x040010D7 RID: 4311
		private UnsafeNativeMethods.IHTMLEventObj htmlEventObj;

		// Token: 0x040010D8 RID: 4312
		private HtmlShimManager shimManager;
	}
}
