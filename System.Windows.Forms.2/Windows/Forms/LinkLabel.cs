using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020002C6 RID: 710
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("LinkClicked")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionLinkLabel")]
	public class LinkLabel : Label, IButtonControl
	{
		// Token: 0x06002B48 RID: 11080 RVA: 0x000C27C4 File Offset: 0x000C09C4
		public LinkLabel()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.StandardClick | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.ResetLinkArea();
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000C2821 File Offset: 0x000C0A21
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x000C283D File Offset: 0x000C0A3D
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelActiveLinkColorDescr")]
		public Color ActiveLinkColor
		{
			get
			{
				if (this.activeLinkColor.IsEmpty)
				{
					return this.IEActiveLinkColor;
				}
				return this.activeLinkColor;
			}
			set
			{
				if (this.activeLinkColor != value)
				{
					this.activeLinkColor = value;
					this.InvalidateLink(null);
				}
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000C285B File Offset: 0x000C0A5B
		// (set) Token: 0x06002B4C RID: 11084 RVA: 0x000C2877 File Offset: 0x000C0A77
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelDisabledLinkColorDescr")]
		public Color DisabledLinkColor
		{
			get
			{
				if (this.disabledLinkColor.IsEmpty)
				{
					return this.IEDisabledLinkColor;
				}
				return this.disabledLinkColor;
			}
			set
			{
				if (this.disabledLinkColor != value)
				{
					this.disabledLinkColor = value;
					this.InvalidateLink(null);
				}
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x000C2895 File Offset: 0x000C0A95
		// (set) Token: 0x06002B4E RID: 11086 RVA: 0x000C28A0 File Offset: 0x000C0AA0
		private LinkLabel.Link FocusLink
		{
			get
			{
				return this.focusLink;
			}
			set
			{
				if (this.focusLink != value)
				{
					if (this.focusLink != null)
					{
						this.InvalidateLink(this.focusLink);
					}
					this.focusLink = value;
					if (this.focusLink != null)
					{
						this.InvalidateLink(this.focusLink);
						this.UpdateAccessibilityLink(this.focusLink);
					}
				}
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x000C28F1 File Offset: 0x000C0AF1
		private Color IELinkColor
		{
			get
			{
				return LinkUtilities.IELinkColor;
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x000C28F8 File Offset: 0x000C0AF8
		private Color IEActiveLinkColor
		{
			get
			{
				return LinkUtilities.IEActiveLinkColor;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x000C28FF File Offset: 0x000C0AFF
		private Color IEVisitedLinkColor
		{
			get
			{
				return LinkUtilities.IEVisitedLinkColor;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x000C2906 File Offset: 0x000C0B06
		private Color IEDisabledLinkColor
		{
			get
			{
				if (LinkLabel.iedisabledLinkColor.IsEmpty)
				{
					LinkLabel.iedisabledLinkColor = ControlPaint.Dark(base.DisabledColor);
				}
				return LinkLabel.iedisabledLinkColor;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000C2929 File Offset: 0x000C0B29
		private Rectangle ClientRectWithPadding
		{
			get
			{
				return LayoutUtils.DeflateRect(base.ClientRectangle, this.Padding);
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000C293C File Offset: 0x000C0B3C
		// (set) Token: 0x06002B55 RID: 11093 RVA: 0x000C2944 File Offset: 0x000C0B44
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new FlatStyle FlatStyle
		{
			get
			{
				return base.FlatStyle;
			}
			set
			{
				base.FlatStyle = value;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000C2950 File Offset: 0x000C0B50
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x000C29A4 File Offset: 0x000C0BA4
		[Editor("System.Windows.Forms.Design.LinkAreaEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("LinkLabelLinkAreaDescr")]
		public LinkArea LinkArea
		{
			get
			{
				if (this.links.Count == 0)
				{
					return new LinkArea(0, 0);
				}
				return new LinkArea(((LinkLabel.Link)this.links[0]).Start, ((LinkLabel.Link)this.links[0]).Length);
			}
			set
			{
				LinkArea linkArea = this.LinkArea;
				this.links.Clear();
				if (!value.IsEmpty)
				{
					if (value.Start < 0)
					{
						throw new ArgumentOutOfRangeException("LinkArea", value, SR.GetString("LinkLabelAreaStart"));
					}
					if (value.Length < -1)
					{
						throw new ArgumentOutOfRangeException("LinkArea", value, SR.GetString("LinkLabelAreaLength"));
					}
					if (value.Start != 0 || value.Length != 0)
					{
						this.Links.Add(new LinkLabel.Link(this));
						((LinkLabel.Link)this.links[0]).Start = value.Start;
						((LinkLabel.Link)this.links[0]).Length = value.Length;
					}
				}
				this.UpdateSelectability();
				if (!linkArea.Equals(this.LinkArea))
				{
					this.InvalidateTextLayout();
					LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.LinkArea);
					base.AdjustSize();
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x000C2ABA File Offset: 0x000C0CBA
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x000C2AC4 File Offset: 0x000C0CC4
		[DefaultValue(LinkBehavior.SystemDefault)]
		[SRCategory("CatBehavior")]
		[SRDescription("LinkLabelLinkBehaviorDescr")]
		public LinkBehavior LinkBehavior
		{
			get
			{
				return this.linkBehavior;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("LinkBehavior", (int)value, typeof(LinkBehavior));
				}
				if (value != this.linkBehavior)
				{
					this.linkBehavior = value;
					this.InvalidateLinkFonts();
					this.InvalidateLink(null);
				}
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x000C2B14 File Offset: 0x000C0D14
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x000C2B3D File Offset: 0x000C0D3D
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelLinkColorDescr")]
		public Color LinkColor
		{
			get
			{
				if (!this.linkColor.IsEmpty)
				{
					return this.linkColor;
				}
				if (SystemInformation.HighContrast)
				{
					return SystemColors.HotTrack;
				}
				return this.IELinkColor;
			}
			set
			{
				if (this.linkColor != value)
				{
					this.linkColor = value;
					this.InvalidateLink(null);
				}
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x000C2B5B File Offset: 0x000C0D5B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LinkLabel.LinkCollection Links
		{
			get
			{
				if (this.linkCollection == null)
				{
					this.linkCollection = new LinkLabel.LinkCollection(this);
				}
				return this.linkCollection;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000C2B77 File Offset: 0x000C0D77
		// (set) Token: 0x06002B5E RID: 11102 RVA: 0x000C2BA0 File Offset: 0x000C0DA0
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelLinkVisitedDescr")]
		public bool LinkVisited
		{
			get
			{
				return this.links.Count != 0 && ((LinkLabel.Link)this.links[0]).Visited;
			}
			set
			{
				if (value != this.LinkVisited)
				{
					if (this.links.Count == 0)
					{
						this.Links.Add(new LinkLabel.Link(this));
					}
					((LinkLabel.Link)this.links[0]).Visited = value;
				}
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002B5F RID: 11103 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool OwnerDraw
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x000C2BEC File Offset: 0x000C0DEC
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x000C2BF4 File Offset: 0x000C0DF4
		protected Cursor OverrideCursor
		{
			get
			{
				return this.overrideCursor;
			}
			set
			{
				if (this.overrideCursor != value)
				{
					this.overrideCursor = value;
					if (base.IsHandleCreated)
					{
						NativeMethods.POINT point = new NativeMethods.POINT();
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetCursorPos(point);
						UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
						if ((rect.left <= point.x && point.x < rect.right && rect.top <= point.y && point.y < rect.bottom) || UnsafeNativeMethods.GetCapture() == base.Handle)
						{
							base.SendMessage(32, base.Handle, 1);
						}
					}
				}
			}
		}

		// Token: 0x140001F4 RID: 500
		// (add) Token: 0x06002B62 RID: 11106 RVA: 0x000C2CA7 File Offset: 0x000C0EA7
		// (remove) Token: 0x06002B63 RID: 11107 RVA: 0x000C2CB0 File Offset: 0x000C0EB0
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000C2CB9 File Offset: 0x000C0EB9
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x000C2CC1 File Offset: 0x000C0EC1
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000C2CCA File Offset: 0x000C0ECA
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x000C2CD2 File Offset: 0x000C0ED2
		[RefreshProperties(RefreshProperties.Repaint)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06002B69 RID: 11113 RVA: 0x0001365E File Offset: 0x0001185E
		[RefreshProperties(RefreshProperties.Repaint)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002B6A RID: 11114 RVA: 0x000C2CDB File Offset: 0x000C0EDB
		// (set) Token: 0x06002B6B RID: 11115 RVA: 0x000C2D04 File Offset: 0x000C0F04
		[SRCategory("CatAppearance")]
		[SRDescription("LinkLabelVisitedLinkColorDescr")]
		public Color VisitedLinkColor
		{
			get
			{
				if (!this.visitedLinkColor.IsEmpty)
				{
					return this.visitedLinkColor;
				}
				if (SystemInformation.HighContrast)
				{
					return LinkUtilities.GetVisitedLinkColor();
				}
				return this.IEVisitedLinkColor;
			}
			set
			{
				if (this.visitedLinkColor != value)
				{
					this.visitedLinkColor = value;
					this.InvalidateLink(null);
				}
			}
		}

		// Token: 0x140001F5 RID: 501
		// (add) Token: 0x06002B6C RID: 11116 RVA: 0x000C2D22 File Offset: 0x000C0F22
		// (remove) Token: 0x06002B6D RID: 11117 RVA: 0x000C2D35 File Offset: 0x000C0F35
		[WinCategory("Action")]
		[SRDescription("LinkLabelLinkClickedDescr")]
		public event LinkLabelLinkClickedEventHandler LinkClicked
		{
			add
			{
				base.Events.AddHandler(LinkLabel.EventLinkClicked, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkLabel.EventLinkClicked, value);
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000C2D48 File Offset: 0x000C0F48
		internal static Rectangle CalcTextRenderBounds(Rectangle textRect, Rectangle clientRect, ContentAlignment align)
		{
			int x;
			if ((align & WindowsFormsUtils.AnyRightAlign) != (ContentAlignment)0)
			{
				x = clientRect.Right - textRect.Width;
			}
			else if ((align & WindowsFormsUtils.AnyCenterAlign) != (ContentAlignment)0)
			{
				x = (clientRect.Width - textRect.Width) / 2;
			}
			else
			{
				x = clientRect.X;
			}
			int y;
			if ((align & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
			{
				y = clientRect.Bottom - textRect.Height;
			}
			else if ((align & WindowsFormsUtils.AnyMiddleAlign) != (ContentAlignment)0)
			{
				y = (clientRect.Height - textRect.Height) / 2;
			}
			else
			{
				y = clientRect.Y;
			}
			int width;
			if (textRect.Width > clientRect.Width)
			{
				x = clientRect.X;
				width = clientRect.Width;
			}
			else
			{
				width = textRect.Width;
			}
			int height;
			if (textRect.Height > clientRect.Height)
			{
				y = clientRect.Y;
				height = clientRect.Height;
			}
			else
			{
				height = textRect.Height;
			}
			return new Rectangle(x, y, width, height);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000C2E32 File Offset: 0x000C1032
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new LinkLabel.LinkLabelAccessibleObject(this);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x000C2E3A File Offset: 0x000C103A
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.InvalidateTextLayout();
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002B71 RID: 11121 RVA: 0x000C2E48 File Offset: 0x000C1048
		internal override bool CanUseTextRenderer
		{
			get
			{
				StringInfo stringInfo = new StringInfo(this.Text);
				return this.LinkArea.Start == 0 && (this.LinkArea.Length == 0 || this.LinkArea.Length == stringInfo.LengthInTextElements);
			}
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000C2E9B File Offset: 0x000C109B
		internal override bool UseGDIMeasuring()
		{
			return !this.UseCompatibleTextRendering;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000C2EA8 File Offset: 0x000C10A8
		private static int ConvertToCharIndex(int index, string text)
		{
			if (index <= 0)
			{
				return 0;
			}
			if (string.IsNullOrEmpty(text))
			{
				return index;
			}
			StringInfo stringInfo = new StringInfo(text);
			int lengthInTextElements = stringInfo.LengthInTextElements;
			if (index > lengthInTextElements)
			{
				return index - lengthInTextElements + text.Length;
			}
			string text2 = stringInfo.SubstringByTextElements(0, index);
			return text2.Length;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000C2EF4 File Offset: 0x000C10F4
		private void EnsureRun(Graphics g)
		{
			if (this.textLayoutValid)
			{
				return;
			}
			if (this.textRegion != null)
			{
				this.textRegion.Dispose();
				this.textRegion = null;
			}
			if (this.Text.Length == 0)
			{
				this.Links.Clear();
				this.Links.Add(new LinkLabel.Link(0, -1));
				this.textLayoutValid = true;
				return;
			}
			StringFormat stringFormat = this.CreateStringFormat();
			string text = this.Text;
			try
			{
				Font font = new Font(this.Font, this.Font.Style | FontStyle.Underline);
				Graphics graphics = null;
				try
				{
					if (g == null)
					{
						graphics = (g = base.CreateGraphicsInternal());
					}
					if (this.UseCompatibleTextRendering)
					{
						Region[] array = g.MeasureCharacterRanges(text, font, this.ClientRectWithPadding, stringFormat);
						int num = 0;
						for (int i = 0; i < this.Links.Count; i++)
						{
							LinkLabel.Link link = this.Links[i];
							int num2 = LinkLabel.ConvertToCharIndex(link.Start, text);
							int num3 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
							if (this.LinkInText(num2, num3 - num2))
							{
								this.Links[i].VisualRegion = array[num];
								num++;
							}
						}
						this.textRegion = array[array.Length - 1];
					}
					else
					{
						Rectangle clientRectWithPadding = this.ClientRectWithPadding;
						Size size = new Size(clientRectWithPadding.Width, clientRectWithPadding.Height);
						TextFormatFlags textFormatFlags = this.CreateTextFormatFlags(size);
						Size size2 = TextRenderer.MeasureText(text, font, size, textFormatFlags);
						int iLeftMargin;
						int iRightMargin;
						using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
						{
							if ((textFormatFlags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
							{
								windowsGraphics.TextPadding = TextPaddingOptions.NoPadding;
							}
							else if ((textFormatFlags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
							{
								windowsGraphics.TextPadding = TextPaddingOptions.LeftAndRightPadding;
							}
							using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(this.Font))
							{
								IntNativeMethods.DRAWTEXTPARAMS textMargins = windowsGraphics.GetTextMargins(windowsFont);
								iLeftMargin = textMargins.iLeftMargin;
								iRightMargin = textMargins.iRightMargin;
							}
						}
						Rectangle rectangle = new Rectangle(clientRectWithPadding.X + iLeftMargin, clientRectWithPadding.Y, size2.Width - iRightMargin - iLeftMargin, size2.Height);
						rectangle = LinkLabel.CalcTextRenderBounds(rectangle, clientRectWithPadding, base.RtlTranslateContent(this.TextAlign));
						Region visualRegion = new Region(rectangle);
						if (this.links != null && this.links.Count == 1)
						{
							this.Links[0].VisualRegion = visualRegion;
						}
						this.textRegion = visualRegion;
					}
				}
				finally
				{
					font.Dispose();
					font = null;
					if (graphics != null)
					{
						graphics.Dispose();
						graphics = null;
					}
				}
				this.textLayoutValid = true;
			}
			finally
			{
				stringFormat.Dispose();
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000C31F8 File Offset: 0x000C13F8
		internal override StringFormat CreateStringFormat()
		{
			StringFormat stringFormat = base.CreateStringFormat();
			if (string.IsNullOrEmpty(this.Text))
			{
				return stringFormat;
			}
			CharacterRange[] measurableCharacterRanges = this.AdjustCharacterRangesForSurrogateChars();
			stringFormat.SetMeasurableCharacterRanges(measurableCharacterRanges);
			return stringFormat;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000C322C File Offset: 0x000C142C
		private CharacterRange[] AdjustCharacterRangesForSurrogateChars()
		{
			string text = this.Text;
			if (string.IsNullOrEmpty(text))
			{
				return new CharacterRange[0];
			}
			StringInfo stringInfo = new StringInfo(text);
			int lengthInTextElements = stringInfo.LengthInTextElements;
			ArrayList arrayList = new ArrayList(this.Links.Count);
			foreach (object obj in this.Links)
			{
				LinkLabel.Link link = (LinkLabel.Link)obj;
				int num = LinkLabel.ConvertToCharIndex(link.Start, text);
				int num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
				if (this.LinkInText(num, num2 - num))
				{
					int num3 = Math.Min(link.Length, lengthInTextElements - link.Start);
					arrayList.Add(new CharacterRange(num, LinkLabel.ConvertToCharIndex(link.Start + num3, text) - num));
				}
			}
			CharacterRange[] array = new CharacterRange[arrayList.Count + 1];
			arrayList.CopyTo(array, 0);
			array[array.Length - 1] = new CharacterRange(0, text.Length);
			return array;
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000C3368 File Offset: 0x000C1568
		private bool IsOneLink()
		{
			if (this.links == null || this.links.Count != 1 || this.Text == null)
			{
				return false;
			}
			StringInfo stringInfo = new StringInfo(this.Text);
			return this.LinkArea.Start == 0 && this.LinkArea.Length == stringInfo.LengthInTextElements;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000C33CC File Offset: 0x000C15CC
		protected LinkLabel.Link PointInLink(int x, int y)
		{
			Graphics graphics = base.CreateGraphicsInternal();
			LinkLabel.Link result = null;
			try
			{
				this.EnsureRun(graphics);
				foreach (object obj in this.links)
				{
					LinkLabel.Link link = (LinkLabel.Link)obj;
					if (link.VisualRegion != null && link.VisualRegion.IsVisible(x, y, graphics))
					{
						result = link;
						break;
					}
				}
			}
			finally
			{
				graphics.Dispose();
				graphics = null;
			}
			return result;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000C3464 File Offset: 0x000C1664
		private void InvalidateLink(LinkLabel.Link link)
		{
			if (base.IsHandleCreated)
			{
				if (link == null || link.VisualRegion == null || this.IsOneLink())
				{
					base.Invalidate();
					return;
				}
				base.Invalidate(link.VisualRegion);
			}
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000C3494 File Offset: 0x000C1694
		private void InvalidateLinkFonts()
		{
			if (this.linkFont != null)
			{
				this.linkFont.Dispose();
			}
			if (this.hoverLinkFont != null && this.hoverLinkFont != this.linkFont)
			{
				this.hoverLinkFont.Dispose();
			}
			this.linkFont = null;
			this.hoverLinkFont = null;
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000C34E3 File Offset: 0x000C16E3
		private void InvalidateTextLayout()
		{
			this.textLayoutValid = false;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000C34EC File Offset: 0x000C16EC
		private bool LinkInText(int start, int length)
		{
			return 0 <= start && start < this.Text.Length && 0 < length;
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000C3506 File Offset: 0x000C1706
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000C350E File Offset: 0x000C170E
		DialogResult IButtonControl.DialogResult
		{
			get
			{
				return this.dialogResult;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
				}
				this.dialogResult = value;
			}
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000072B6 File Offset: 0x000054B6
		void IButtonControl.NotifyDefault(bool value)
		{
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000C3540 File Offset: 0x000C1740
		protected override void OnGotFocus(EventArgs e)
		{
			if (!this.processingOnGotFocus)
			{
				base.OnGotFocus(e);
				this.processingOnGotFocus = true;
			}
			try
			{
				LinkLabel.Link link = this.FocusLink;
				if (link == null)
				{
					IntSecurity.ModifyFocus.Assert();
					this.Select(true, true);
				}
				else
				{
					this.InvalidateLink(link);
					this.UpdateAccessibilityLink(link);
				}
			}
			finally
			{
				if (this.processingOnGotFocus)
				{
					this.processingOnGotFocus = false;
				}
			}
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000C35B4 File Offset: 0x000C17B4
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (this.FocusLink != null)
			{
				this.InvalidateLink(this.FocusLink);
			}
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000C35D1 File Offset: 0x000C17D1
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Return && this.FocusLink != null && this.FocusLink.Enabled)
			{
				this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(this.FocusLink));
			}
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x000C360C File Offset: 0x000C180C
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (!base.Enabled)
			{
				return;
			}
			foreach (object obj in this.links)
			{
				LinkLabel.Link link = (LinkLabel.Link)obj;
				if ((link.State & LinkState.Hover) == LinkState.Hover || (link.State & LinkState.Active) == LinkState.Active)
				{
					bool flag = (link.State & LinkState.Active) == LinkState.Active;
					link.State &= (LinkState)(-4);
					if (flag || this.hoverLinkFont != this.linkFont)
					{
						this.InvalidateLink(link);
					}
					this.OverrideCursor = null;
				}
			}
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000C36C0 File Offset: 0x000C18C0
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!base.Enabled || e.Clicks > 1)
			{
				this.receivedDoubleClick = true;
				return;
			}
			for (int i = 0; i < this.links.Count; i++)
			{
				if ((((LinkLabel.Link)this.links[i]).State & LinkState.Hover) == LinkState.Hover)
				{
					((LinkLabel.Link)this.links[i]).State |= LinkState.Active;
					this.FocusInternal();
					if (((LinkLabel.Link)this.links[i]).Enabled)
					{
						this.FocusLink = (LinkLabel.Link)this.links[i];
						this.InvalidateLink(this.FocusLink);
					}
					base.CaptureInternal = true;
					return;
				}
			}
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000C378C File Offset: 0x000C198C
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (base.Disposing || base.IsDisposed)
			{
				return;
			}
			if (!base.Enabled || e.Clicks > 1 || this.receivedDoubleClick)
			{
				this.receivedDoubleClick = false;
				return;
			}
			for (int i = 0; i < this.links.Count; i++)
			{
				if ((((LinkLabel.Link)this.links[i]).State & LinkState.Active) == LinkState.Active)
				{
					((LinkLabel.Link)this.links[i]).State &= (LinkState)(-3);
					this.InvalidateLink((LinkLabel.Link)this.links[i]);
					base.CaptureInternal = false;
					LinkLabel.Link link = this.PointInLink(e.X, e.Y);
					if (link != null && link == this.FocusLink && link.Enabled)
					{
						this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(link, e.Button));
					}
				}
			}
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000C3880 File Offset: 0x000C1A80
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!base.Enabled)
			{
				return;
			}
			LinkLabel.Link link = null;
			foreach (object obj in this.links)
			{
				LinkLabel.Link link2 = (LinkLabel.Link)obj;
				if ((link2.State & LinkState.Hover) == LinkState.Hover)
				{
					link = link2;
					break;
				}
			}
			LinkLabel.Link link3 = this.PointInLink(e.X, e.Y);
			if (link3 != link)
			{
				if (link != null)
				{
					link.State &= (LinkState)(-2);
				}
				if (link3 != null)
				{
					link3.State |= LinkState.Hover;
					if (link3.Enabled)
					{
						this.OverrideCursor = Cursors.Hand;
					}
				}
				else
				{
					this.OverrideCursor = null;
				}
				if (this.hoverLinkFont != this.linkFont)
				{
					if (link != null)
					{
						this.InvalidateLink(link);
					}
					if (link3 != null)
					{
						this.InvalidateLink(link3);
					}
				}
			}
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000C3970 File Offset: 0x000C1B70
		protected virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
		{
			LinkLabelLinkClickedEventHandler linkLabelLinkClickedEventHandler = (LinkLabelLinkClickedEventHandler)base.Events[LinkLabel.EventLinkClicked];
			if (linkLabelLinkClickedEventHandler != null)
			{
				linkLabelLinkClickedEventHandler(this, e);
			}
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000C399E File Offset: 0x000C1B9E
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			this.InvalidateTextLayout();
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000C39B0 File Offset: 0x000C1BB0
		protected override void OnPaint(PaintEventArgs e)
		{
			RectangleF rectangleF = RectangleF.Empty;
			base.Animate();
			ImageAnimator.UpdateFrames(base.Image);
			this.EnsureRun(e.Graphics);
			if (this.Text.Length == 0)
			{
				this.PaintLinkBackground(e.Graphics);
			}
			else
			{
				if (base.AutoEllipsis)
				{
					Rectangle clientRectWithPadding = this.ClientRectWithPadding;
					Size preferredSize = this.GetPreferredSize(new Size(clientRectWithPadding.Width, clientRectWithPadding.Height));
					this.showToolTip = (clientRectWithPadding.Width < preferredSize.Width || clientRectWithPadding.Height < preferredSize.Height);
				}
				else
				{
					this.showToolTip = false;
				}
				if (base.Enabled)
				{
					bool flag = !base.GetStyle(ControlStyles.OptimizedDoubleBuffer);
					SolidBrush solidBrush = new SolidBrush(this.ForeColor);
					SolidBrush solidBrush2 = new SolidBrush(this.LinkColor);
					try
					{
						if (!flag)
						{
							this.PaintLinkBackground(e.Graphics);
						}
						LinkUtilities.EnsureLinkFonts(this.Font, this.LinkBehavior, ref this.linkFont, ref this.hoverLinkFont);
						Region clip = e.Graphics.Clip;
						try
						{
							if (this.IsOneLink())
							{
								e.Graphics.Clip = clip;
								RectangleF[] regionScans = ((LinkLabel.Link)this.links[0]).VisualRegion.GetRegionScans(e.Graphics.Transform);
								if (regionScans == null || regionScans.Length == 0)
								{
									goto IL_2B6;
								}
								if (this.UseCompatibleTextRendering)
								{
									rectangleF = new RectangleF(regionScans[0].Location, SizeF.Empty);
									foreach (RectangleF b in regionScans)
									{
										rectangleF = RectangleF.Union(rectangleF, b);
									}
								}
								else
								{
									rectangleF = this.ClientRectWithPadding;
									Size size = rectangleF.Size.ToSize();
									Size textSize = base.MeasureTextCache.GetTextSize(this.Text, this.Font, size, this.CreateTextFormatFlags(size));
									rectangleF.Width = (float)textSize.Width;
									if ((float)textSize.Height < rectangleF.Height)
									{
										rectangleF.Height = (float)textSize.Height;
									}
									rectangleF = LinkLabel.CalcTextRenderBounds(Rectangle.Round(rectangleF), this.ClientRectWithPadding, base.RtlTranslateContent(this.TextAlign));
								}
								using (Region region = new Region(rectangleF))
								{
									e.Graphics.ExcludeClip(region);
									goto IL_2B6;
								}
							}
							foreach (object obj in this.links)
							{
								LinkLabel.Link link = (LinkLabel.Link)obj;
								if (link.VisualRegion != null)
								{
									e.Graphics.ExcludeClip(link.VisualRegion);
								}
							}
							IL_2B6:
							if (!this.IsOneLink())
							{
								this.PaintLink(e.Graphics, null, solidBrush, solidBrush2, flag, rectangleF);
							}
							foreach (object obj2 in this.links)
							{
								LinkLabel.Link link2 = (LinkLabel.Link)obj2;
								this.PaintLink(e.Graphics, link2, solidBrush, solidBrush2, flag, rectangleF);
							}
							if (flag)
							{
								e.Graphics.Clip = clip;
								e.Graphics.ExcludeClip(this.textRegion);
								this.PaintLinkBackground(e.Graphics);
							}
							goto IL_456;
						}
						finally
						{
							e.Graphics.Clip = clip;
						}
					}
					finally
					{
						solidBrush.Dispose();
						solidBrush2.Dispose();
					}
				}
				Region clip2 = e.Graphics.Clip;
				try
				{
					this.PaintLinkBackground(e.Graphics);
					e.Graphics.IntersectClip(this.textRegion);
					if (this.UseCompatibleTextRendering)
					{
						StringFormat format = this.CreateStringFormat();
						ControlPaint.DrawStringDisabled(e.Graphics, this.Text, this.Font, base.DisabledColor, this.ClientRectWithPadding, format);
					}
					else
					{
						IntPtr hdc = e.Graphics.GetHdc();
						Color nearestColor;
						try
						{
							using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
							{
								nearestColor = windowsGraphics.GetNearestColor(base.DisabledColor);
							}
						}
						finally
						{
							e.Graphics.ReleaseHdc();
						}
						Rectangle clientRectWithPadding2 = this.ClientRectWithPadding;
						ControlPaint.DrawStringDisabled(e.Graphics, this.Text, this.Font, nearestColor, clientRectWithPadding2, this.CreateTextFormatFlags(clientRectWithPadding2.Size));
					}
				}
				finally
				{
					e.Graphics.Clip = clip2;
				}
			}
			IL_456:
			base.RaisePaintEvent(this, e);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000C3EE0 File Offset: 0x000C20E0
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			Image image = base.Image;
			if (image != null)
			{
				Region clip = e.Graphics.Clip;
				Rectangle rect = base.CalcImageRenderBounds(image, base.ClientRectangle, base.RtlTranslateAlignment(base.ImageAlign));
				e.Graphics.ExcludeClip(rect);
				try
				{
					base.OnPaintBackground(e);
				}
				finally
				{
					e.Graphics.Clip = clip;
				}
				e.Graphics.IntersectClip(rect);
				try
				{
					base.OnPaintBackground(e);
					base.DrawImage(e.Graphics, image, base.ClientRectangle, base.RtlTranslateAlignment(base.ImageAlign));
					return;
				}
				finally
				{
					e.Graphics.Clip = clip;
				}
			}
			base.OnPaintBackground(e);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000C3FA8 File Offset: 0x000C21A8
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.InvalidateTextLayout();
			this.InvalidateLinkFonts();
			base.Invalidate();
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000C3FC3 File Offset: 0x000C21C3
		protected override void OnAutoSizeChanged(EventArgs e)
		{
			base.OnAutoSizeChanged(e);
			this.InvalidateTextLayout();
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000C3FD2 File Offset: 0x000C21D2
		internal override void OnAutoEllipsisChanged()
		{
			base.OnAutoEllipsisChanged();
			this.InvalidateTextLayout();
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000C3FE0 File Offset: 0x000C21E0
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (!base.Enabled)
			{
				for (int i = 0; i < this.links.Count; i++)
				{
					((LinkLabel.Link)this.links[i]).State &= (LinkState)(-4);
				}
				this.OverrideCursor = null;
			}
			this.InvalidateTextLayout();
			base.Invalidate();
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000C4044 File Offset: 0x000C2244
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.InvalidateTextLayout();
			this.UpdateSelectability();
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000C4059 File Offset: 0x000C2259
		protected override void OnTextAlignChanged(EventArgs e)
		{
			base.OnTextAlignChanged(e);
			this.InvalidateTextLayout();
			this.UpdateSelectability();
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000C4070 File Offset: 0x000C2270
		private void PaintLink(Graphics g, LinkLabel.Link link, SolidBrush foreBrush, SolidBrush linkBrush, bool optimizeBackgroundRendering, RectangleF finalrect)
		{
			Font font = this.Font;
			if (link != null)
			{
				if (link.VisualRegion != null)
				{
					Color color = Color.Empty;
					LinkState state = link.State;
					if ((state & LinkState.Hover) == LinkState.Hover)
					{
						font = this.hoverLinkFont;
					}
					else
					{
						font = this.linkFont;
					}
					if (link.Enabled)
					{
						if ((state & LinkState.Active) == LinkState.Active)
						{
							color = this.ActiveLinkColor;
						}
						else if ((state & LinkState.Visited) == LinkState.Visited)
						{
							color = this.VisitedLinkColor;
						}
					}
					else
					{
						color = this.DisabledLinkColor;
					}
					if (this.IsOneLink())
					{
						g.Clip = new Region(finalrect);
					}
					else
					{
						g.Clip = link.VisualRegion;
					}
					if (optimizeBackgroundRendering)
					{
						this.PaintLinkBackground(g);
					}
					if (this.UseCompatibleTextRendering)
					{
						SolidBrush solidBrush = (color == Color.Empty) ? linkBrush : new SolidBrush(color);
						StringFormat format = this.CreateStringFormat();
						g.DrawString(this.Text, font, solidBrush, this.ClientRectWithPadding, format);
						if (solidBrush != linkBrush)
						{
							solidBrush.Dispose();
						}
					}
					else
					{
						if (color == Color.Empty)
						{
							color = linkBrush.Color;
						}
						IntPtr hdc = g.GetHdc();
						try
						{
							using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
							{
								color = windowsGraphics.GetNearestColor(color);
							}
						}
						finally
						{
							g.ReleaseHdc();
						}
						Rectangle clientRectWithPadding = this.ClientRectWithPadding;
						TextRenderer.DrawText(g, this.Text, font, clientRectWithPadding, color, this.CreateTextFormatFlags(clientRectWithPadding.Size));
					}
					if (this.Focused && this.ShowFocusCues && this.FocusLink == link)
					{
						RectangleF[] regionScans = link.VisualRegion.GetRegionScans(g.Transform);
						if (regionScans != null && regionScans.Length != 0)
						{
							if (this.IsOneLink())
							{
								Rectangle rectangle = Rectangle.Ceiling(finalrect);
								ControlPaint.DrawFocusRectangle(g, rectangle, this.ForeColor, this.BackColor);
								return;
							}
							foreach (RectangleF value in regionScans)
							{
								ControlPaint.DrawFocusRectangle(g, Rectangle.Ceiling(value), this.ForeColor, this.BackColor);
							}
							return;
						}
					}
				}
			}
			else
			{
				g.IntersectClip(this.textRegion);
				if (optimizeBackgroundRendering)
				{
					this.PaintLinkBackground(g);
				}
				if (this.UseCompatibleTextRendering)
				{
					StringFormat format2 = this.CreateStringFormat();
					g.DrawString(this.Text, font, foreBrush, this.ClientRectWithPadding, format2);
					return;
				}
				IntPtr hdc2 = g.GetHdc();
				Color nearestColor;
				try
				{
					using (WindowsGraphics windowsGraphics2 = WindowsGraphics.FromHdc(hdc2))
					{
						nearestColor = windowsGraphics2.GetNearestColor(foreBrush.Color);
					}
				}
				finally
				{
					g.ReleaseHdc();
				}
				Rectangle clientRectWithPadding2 = this.ClientRectWithPadding;
				TextRenderer.DrawText(g, this.Text, font, clientRectWithPadding2, nearestColor, this.CreateTextFormatFlags(clientRectWithPadding2.Size));
			}
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x000C4344 File Offset: 0x000C2544
		private void PaintLinkBackground(Graphics g)
		{
			using (PaintEventArgs paintEventArgs = new PaintEventArgs(g, base.ClientRectangle))
			{
				base.InvokePaintBackground(this, paintEventArgs);
			}
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000C4384 File Offset: 0x000C2584
		void IButtonControl.PerformClick()
		{
			if (this.FocusLink == null && this.Links.Count > 0)
			{
				string text = this.Text;
				foreach (object obj in this.Links)
				{
					LinkLabel.Link link = (LinkLabel.Link)obj;
					int num = LinkLabel.ConvertToCharIndex(link.Start, text);
					int num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
					if (link.Enabled && this.LinkInText(num, num2 - num))
					{
						this.FocusLink = link;
						break;
					}
				}
			}
			if (this.FocusLink != null)
			{
				this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(this.FocusLink));
			}
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000C4454 File Offset: 0x000C2654
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) != Keys.Alt)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Tab)
				{
					if (keys - Keys.Left > 1)
					{
						if (keys - Keys.Right <= 1)
						{
							if (this.FocusNextLink(true))
							{
								return true;
							}
						}
					}
					else if (this.FocusNextLink(false))
					{
						return true;
					}
				}
				else if (this.TabStop)
				{
					bool forward = (keyData & Keys.Shift) != Keys.Shift;
					if (this.FocusNextLink(forward))
					{
						return true;
					}
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000C44D0 File Offset: 0x000C26D0
		private bool FocusNextLink(bool forward)
		{
			int num = -1;
			if (this.focusLink != null)
			{
				for (int i = 0; i < this.links.Count; i++)
				{
					if (this.links[i] == this.focusLink)
					{
						num = i;
						break;
					}
				}
			}
			num = this.GetNextLinkIndex(num, forward);
			if (num != -1)
			{
				this.FocusLink = this.Links[num];
				return true;
			}
			this.FocusLink = null;
			return false;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000C4540 File Offset: 0x000C2740
		private int GetNextLinkIndex(int focusIndex, bool forward)
		{
			string text = this.Text;
			int num = 0;
			int num2 = 0;
			if (forward)
			{
				do
				{
					focusIndex++;
					LinkLabel.Link link;
					if (focusIndex < this.Links.Count)
					{
						link = this.Links[focusIndex];
						num = LinkLabel.ConvertToCharIndex(link.Start, text);
						num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
					}
					else
					{
						link = null;
					}
					if (link == null || link.Enabled)
					{
						break;
					}
				}
				while (this.LinkInText(num, num2 - num));
			}
			else
			{
				LinkLabel.Link link;
				do
				{
					focusIndex--;
					if (focusIndex >= 0)
					{
						link = this.Links[focusIndex];
						num = LinkLabel.ConvertToCharIndex(link.Start, text);
						num2 = LinkLabel.ConvertToCharIndex(link.Start + link.Length, text);
					}
					else
					{
						link = null;
					}
				}
				while (link != null && !link.Enabled && this.LinkInText(num, num2 - num));
			}
			if (focusIndex < 0 || focusIndex >= this.links.Count)
			{
				return -1;
			}
			return focusIndex;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000C4620 File Offset: 0x000C2820
		private void ResetLinkArea()
		{
			this.LinkArea = new LinkArea(0, -1);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000C462F File Offset: 0x000C282F
		internal void ResetActiveLinkColor()
		{
			this.activeLinkColor = Color.Empty;
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000C463C File Offset: 0x000C283C
		internal void ResetDisabledLinkColor()
		{
			this.disabledLinkColor = Color.Empty;
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000C4649 File Offset: 0x000C2849
		internal void ResetLinkColor()
		{
			this.linkColor = Color.Empty;
			this.InvalidateLink(null);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000C465D File Offset: 0x000C285D
		private void ResetVisitedLinkColor()
		{
			this.visitedLinkColor = Color.Empty;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000C466A File Offset: 0x000C286A
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			this.InvalidateTextLayout();
			base.Invalidate();
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000C4688 File Offset: 0x000C2888
		protected override void Select(bool directed, bool forward)
		{
			if (directed && this.links.Count > 0)
			{
				int focusIndex = -1;
				if (this.FocusLink != null)
				{
					focusIndex = this.links.IndexOf(this.FocusLink);
				}
				this.FocusLink = null;
				int nextLinkIndex = this.GetNextLinkIndex(focusIndex, forward);
				if (nextLinkIndex == -1)
				{
					if (forward)
					{
						nextLinkIndex = this.GetNextLinkIndex(-1, forward);
					}
					else
					{
						nextLinkIndex = this.GetNextLinkIndex(this.links.Count, forward);
					}
				}
				if (nextLinkIndex != -1)
				{
					this.FocusLink = (LinkLabel.Link)this.links[nextLinkIndex];
				}
			}
			base.Select(directed, forward);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000C471A File Offset: 0x000C291A
		internal bool ShouldSerializeActiveLinkColor()
		{
			return !this.activeLinkColor.IsEmpty;
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000C472A File Offset: 0x000C292A
		internal bool ShouldSerializeDisabledLinkColor()
		{
			return !this.disabledLinkColor.IsEmpty;
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000C473A File Offset: 0x000C293A
		private bool ShouldSerializeLinkArea()
		{
			return this.links.Count != 1 || this.Links[0].Start != 0 || this.Links[0].length != -1;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000C4778 File Offset: 0x000C2978
		internal bool ShouldSerializeLinkColor()
		{
			return !this.linkColor.IsEmpty;
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000C4788 File Offset: 0x000C2988
		private bool ShouldSerializeUseCompatibleTextRendering()
		{
			return !this.CanUseTextRenderer || this.UseCompatibleTextRendering != Control.UseCompatibleTextRenderingDefault;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000C47A4 File Offset: 0x000C29A4
		private bool ShouldSerializeVisitedLinkColor()
		{
			return !this.visitedLinkColor.IsEmpty;
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000C47B4 File Offset: 0x000C29B4
		private void UpdateAccessibilityLink(LinkLabel.Link focusLink)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			int childID = -1;
			for (int i = 0; i < this.links.Count; i++)
			{
				if (this.links[i] == focusLink)
				{
					childID = i;
				}
			}
			base.AccessibilityNotifyClients(AccessibleEvents.Focus, childID);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000C4800 File Offset: 0x000C2A00
		private void ValidateNoOverlappingLinks()
		{
			for (int i = 0; i < this.links.Count; i++)
			{
				LinkLabel.Link link = (LinkLabel.Link)this.links[i];
				if (link.Length < 0)
				{
					throw new InvalidOperationException(SR.GetString("LinkLabelOverlap"));
				}
				for (int j = i; j < this.links.Count; j++)
				{
					if (i != j)
					{
						LinkLabel.Link link2 = (LinkLabel.Link)this.links[j];
						int num = Math.Max(link.Start, link2.Start);
						int num2 = Math.Min(link.Start + link.Length, link2.Start + link2.Length);
						if (num < num2)
						{
							throw new InvalidOperationException(SR.GetString("LinkLabelOverlap"));
						}
					}
				}
			}
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000C48CC File Offset: 0x000C2ACC
		private void UpdateSelectability()
		{
			LinkArea linkArea = this.LinkArea;
			bool flag = false;
			string text = this.Text;
			int num = LinkLabel.ConvertToCharIndex(linkArea.Start, text);
			int num2 = LinkLabel.ConvertToCharIndex(linkArea.Start + linkArea.Length, text);
			if (this.LinkInText(num, num2 - num))
			{
				flag = true;
			}
			else if (this.FocusLink != null)
			{
				this.FocusLink = null;
			}
			this.OverrideCursor = null;
			this.TabStop = flag;
			base.SetStyle(ControlStyles.Selectable, flag);
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000C4948 File Offset: 0x000C2B48
		// (set) Token: 0x06002BA8 RID: 11176 RVA: 0x000C4950 File Offset: 0x000C2B50
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public new bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRendering;
			}
			set
			{
				if (base.UseCompatibleTextRendering != value)
				{
					base.UseCompatibleTextRendering = value;
					this.InvalidateTextLayout();
				}
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override bool SupportsUiaProviders
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000C4968 File Offset: 0x000C2B68
		private void WmSetCursor(ref Message m)
		{
			if (!(m.WParam == base.InternalHandle) || NativeMethods.Util.LOWORD(m.LParam) != 1)
			{
				this.DefWndProc(ref m);
				return;
			}
			if (this.OverrideCursor != null)
			{
				Cursor.CurrentInternal = this.OverrideCursor;
				return;
			}
			Cursor.CurrentInternal = this.Cursor;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000C49C4 File Offset: 0x000C2BC4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 == 32)
			{
				this.WmSetCursor(ref msg);
				return;
			}
			base.WndProc(ref msg);
		}

		// Token: 0x04001238 RID: 4664
		private static readonly object EventLinkClicked = new object();

		// Token: 0x04001239 RID: 4665
		private static Color iedisabledLinkColor = Color.Empty;

		// Token: 0x0400123A RID: 4666
		private static LinkLabel.LinkComparer linkComparer = new LinkLabel.LinkComparer();

		// Token: 0x0400123B RID: 4667
		private DialogResult dialogResult;

		// Token: 0x0400123C RID: 4668
		private Color linkColor = Color.Empty;

		// Token: 0x0400123D RID: 4669
		private Color activeLinkColor = Color.Empty;

		// Token: 0x0400123E RID: 4670
		private Color visitedLinkColor = Color.Empty;

		// Token: 0x0400123F RID: 4671
		private Color disabledLinkColor = Color.Empty;

		// Token: 0x04001240 RID: 4672
		private Font linkFont;

		// Token: 0x04001241 RID: 4673
		private Font hoverLinkFont;

		// Token: 0x04001242 RID: 4674
		private bool textLayoutValid;

		// Token: 0x04001243 RID: 4675
		private bool receivedDoubleClick;

		// Token: 0x04001244 RID: 4676
		private ArrayList links = new ArrayList(2);

		// Token: 0x04001245 RID: 4677
		private LinkLabel.Link focusLink;

		// Token: 0x04001246 RID: 4678
		private LinkLabel.LinkCollection linkCollection;

		// Token: 0x04001247 RID: 4679
		private Region textRegion;

		// Token: 0x04001248 RID: 4680
		private Cursor overrideCursor;

		// Token: 0x04001249 RID: 4681
		private bool processingOnGotFocus;

		// Token: 0x0400124A RID: 4682
		private LinkBehavior linkBehavior;

		// Token: 0x020006BB RID: 1723
		public class LinkCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060068EB RID: 26859 RVA: 0x0018654E File Offset: 0x0018474E
			public LinkCollection(LinkLabel owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this.owner = owner;
			}

			// Token: 0x170016A5 RID: 5797
			public virtual LinkLabel.Link this[int index]
			{
				get
				{
					return (LinkLabel.Link)this.owner.links[index];
				}
				set
				{
					this.owner.links[index] = value;
					this.owner.links.Sort(LinkLabel.linkComparer);
					this.owner.InvalidateTextLayout();
					this.owner.Invalidate();
				}
			}

			// Token: 0x170016A6 RID: 5798
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is LinkLabel.Link)
					{
						this[index] = (LinkLabel.Link)value;
						return;
					}
					throw new ArgumentException(SR.GetString("LinkLabelBadLink"), "value");
				}
			}

			// Token: 0x170016A7 RID: 5799
			public virtual LinkLabel.Link this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int index = this.IndexOfKey(key);
					if (this.IsValidIndex(index))
					{
						return this[index];
					}
					return null;
				}
			}

			// Token: 0x170016A8 RID: 5800
			// (get) Token: 0x060068F1 RID: 26865 RVA: 0x00186631 File Offset: 0x00184831
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.links.Count;
				}
			}

			// Token: 0x170016A9 RID: 5801
			// (get) Token: 0x060068F2 RID: 26866 RVA: 0x00186643 File Offset: 0x00184843
			public bool LinksAdded
			{
				get
				{
					return this.linksAdded;
				}
			}

			// Token: 0x170016AA RID: 5802
			// (get) Token: 0x060068F3 RID: 26867 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016AB RID: 5803
			// (get) Token: 0x060068F4 RID: 26868 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016AC RID: 5804
			// (get) Token: 0x060068F5 RID: 26869 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016AD RID: 5805
			// (get) Token: 0x060068F6 RID: 26870 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060068F7 RID: 26871 RVA: 0x0018664B File Offset: 0x0018484B
			public LinkLabel.Link Add(int start, int length)
			{
				if (length != 0)
				{
					this.linksAdded = true;
				}
				return this.Add(start, length, null);
			}

			// Token: 0x060068F8 RID: 26872 RVA: 0x00186660 File Offset: 0x00184860
			public LinkLabel.Link Add(int start, int length, object linkData)
			{
				if (length != 0)
				{
					this.linksAdded = true;
				}
				if (this.owner.links.Count == 1 && this[0].Start == 0 && this[0].length == -1)
				{
					this.owner.links.Clear();
					this.owner.FocusLink = null;
				}
				LinkLabel.Link link = new LinkLabel.Link(this.owner);
				link.Start = start;
				link.Length = length;
				link.LinkData = linkData;
				this.Add(link);
				return link;
			}

			// Token: 0x060068F9 RID: 26873 RVA: 0x001866F0 File Offset: 0x001848F0
			public int Add(LinkLabel.Link value)
			{
				if (value != null && value.Length != 0)
				{
					this.linksAdded = true;
				}
				if (this.owner.links.Count == 1 && this[0].Start == 0 && this[0].length == -1)
				{
					this.owner.links.Clear();
					this.owner.FocusLink = null;
				}
				value.Owner = this.owner;
				this.owner.links.Add(value);
				if (this.owner.AutoSize)
				{
					LayoutTransaction.DoLayout(this.owner.ParentInternal, this.owner, PropertyNames.Links);
					this.owner.AdjustSize();
					this.owner.Invalidate();
				}
				if (this.owner.Links.Count > 1)
				{
					this.owner.links.Sort(LinkLabel.linkComparer);
				}
				this.owner.ValidateNoOverlappingLinks();
				this.owner.UpdateSelectability();
				this.owner.InvalidateTextLayout();
				this.owner.Invalidate();
				if (this.owner.Links.Count > 1)
				{
					return this.IndexOf(value);
				}
				return 0;
			}

			// Token: 0x060068FA RID: 26874 RVA: 0x00186827 File Offset: 0x00184A27
			int IList.Add(object value)
			{
				if (value is LinkLabel.Link)
				{
					return this.Add((LinkLabel.Link)value);
				}
				throw new ArgumentException(SR.GetString("LinkLabelBadLink"), "value");
			}

			// Token: 0x060068FB RID: 26875 RVA: 0x00186852 File Offset: 0x00184A52
			void IList.Insert(int index, object value)
			{
				if (value is LinkLabel.Link)
				{
					this.Add((LinkLabel.Link)value);
					return;
				}
				throw new ArgumentException(SR.GetString("LinkLabelBadLink"), "value");
			}

			// Token: 0x060068FC RID: 26876 RVA: 0x0018687E File Offset: 0x00184A7E
			public bool Contains(LinkLabel.Link link)
			{
				return this.owner.links.Contains(link);
			}

			// Token: 0x060068FD RID: 26877 RVA: 0x00186891 File Offset: 0x00184A91
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x060068FE RID: 26878 RVA: 0x001868A0 File Offset: 0x00184AA0
			bool IList.Contains(object link)
			{
				return link is LinkLabel.Link && this.Contains((LinkLabel.Link)link);
			}

			// Token: 0x060068FF RID: 26879 RVA: 0x001868B8 File Offset: 0x00184AB8
			public int IndexOf(LinkLabel.Link link)
			{
				return this.owner.links.IndexOf(link);
			}

			// Token: 0x06006900 RID: 26880 RVA: 0x001868CB File Offset: 0x00184ACB
			int IList.IndexOf(object link)
			{
				if (link is LinkLabel.Link)
				{
					return this.IndexOf((LinkLabel.Link)link);
				}
				return -1;
			}

			// Token: 0x06006901 RID: 26881 RVA: 0x001868E4 File Offset: 0x00184AE4
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x06006902 RID: 26882 RVA: 0x00186961 File Offset: 0x00184B61
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006903 RID: 26883 RVA: 0x00186974 File Offset: 0x00184B74
			public virtual void Clear()
			{
				bool flag = this.owner.links.Count > 0 && this.owner.AutoSize;
				this.owner.links.Clear();
				if (flag)
				{
					LayoutTransaction.DoLayout(this.owner.ParentInternal, this.owner, PropertyNames.Links);
					this.owner.AdjustSize();
					this.owner.Invalidate();
				}
				this.owner.UpdateSelectability();
				this.owner.InvalidateTextLayout();
				this.owner.Invalidate();
			}

			// Token: 0x06006904 RID: 26884 RVA: 0x00186A08 File Offset: 0x00184C08
			void ICollection.CopyTo(Array dest, int index)
			{
				this.owner.links.CopyTo(dest, index);
			}

			// Token: 0x06006905 RID: 26885 RVA: 0x00186A1C File Offset: 0x00184C1C
			public IEnumerator GetEnumerator()
			{
				if (this.owner.links != null)
				{
					return this.owner.links.GetEnumerator();
				}
				return new LinkLabel.Link[0].GetEnumerator();
			}

			// Token: 0x06006906 RID: 26886 RVA: 0x00186A48 File Offset: 0x00184C48
			public void Remove(LinkLabel.Link value)
			{
				if (value.Owner != this.owner)
				{
					return;
				}
				this.owner.links.Remove(value);
				if (this.owner.AutoSize)
				{
					LayoutTransaction.DoLayout(this.owner.ParentInternal, this.owner, PropertyNames.Links);
					this.owner.AdjustSize();
					this.owner.Invalidate();
				}
				this.owner.links.Sort(LinkLabel.linkComparer);
				this.owner.ValidateNoOverlappingLinks();
				this.owner.UpdateSelectability();
				this.owner.InvalidateTextLayout();
				this.owner.Invalidate();
				if (this.owner.FocusLink == null && this.owner.links.Count > 0)
				{
					this.owner.FocusLink = (LinkLabel.Link)this.owner.links[0];
				}
			}

			// Token: 0x06006907 RID: 26887 RVA: 0x00186B35 File Offset: 0x00184D35
			public void RemoveAt(int index)
			{
				this.Remove(this[index]);
			}

			// Token: 0x06006908 RID: 26888 RVA: 0x00186B44 File Offset: 0x00184D44
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x06006909 RID: 26889 RVA: 0x00186B69 File Offset: 0x00184D69
			void IList.Remove(object value)
			{
				if (value is LinkLabel.Link)
				{
					this.Remove((LinkLabel.Link)value);
				}
			}

			// Token: 0x04003B1C RID: 15132
			private LinkLabel owner;

			// Token: 0x04003B1D RID: 15133
			private bool linksAdded;

			// Token: 0x04003B1E RID: 15134
			private int lastAccessedIndex = -1;
		}

		// Token: 0x020006BC RID: 1724
		[TypeConverter(typeof(LinkConverter))]
		public class Link
		{
			// Token: 0x0600690A RID: 26890 RVA: 0x00186B7F File Offset: 0x00184D7F
			public Link()
			{
			}

			// Token: 0x0600690B RID: 26891 RVA: 0x00186B8E File Offset: 0x00184D8E
			public Link(int start, int length)
			{
				this.start = start;
				this.length = length;
			}

			// Token: 0x0600690C RID: 26892 RVA: 0x00186BAB File Offset: 0x00184DAB
			public Link(int start, int length, object linkData)
			{
				this.start = start;
				this.length = length;
				this.linkData = linkData;
			}

			// Token: 0x0600690D RID: 26893 RVA: 0x00186BCF File Offset: 0x00184DCF
			internal Link(LinkLabel owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016AE RID: 5806
			// (get) Token: 0x0600690E RID: 26894 RVA: 0x00186BE5 File Offset: 0x00184DE5
			internal AccessibleObject AccessibilityObject
			{
				get
				{
					if (this.accessibleObject == null)
					{
						this.accessibleObject = new LinkLabel.LinkAccessibleObject(this);
					}
					return this.accessibleObject;
				}
			}

			// Token: 0x170016AF RID: 5807
			// (get) Token: 0x0600690F RID: 26895 RVA: 0x00186C01 File Offset: 0x00184E01
			// (set) Token: 0x06006910 RID: 26896 RVA: 0x00186C09 File Offset: 0x00184E09
			public string Description
			{
				get
				{
					return this.description;
				}
				set
				{
					this.description = value;
				}
			}

			// Token: 0x170016B0 RID: 5808
			// (get) Token: 0x06006911 RID: 26897 RVA: 0x00186C12 File Offset: 0x00184E12
			// (set) Token: 0x06006912 RID: 26898 RVA: 0x00186C1C File Offset: 0x00184E1C
			[DefaultValue(true)]
			public bool Enabled
			{
				get
				{
					return this.enabled;
				}
				set
				{
					if (this.enabled != value)
					{
						this.enabled = value;
						if ((this.state & (LinkState)3) != LinkState.Normal)
						{
							this.state &= (LinkState)(-4);
							if (this.owner != null)
							{
								this.owner.OverrideCursor = null;
							}
						}
						if (this.owner != null)
						{
							this.owner.InvalidateLink(this);
						}
					}
				}
			}

			// Token: 0x170016B1 RID: 5809
			// (get) Token: 0x06006913 RID: 26899 RVA: 0x00186C7C File Offset: 0x00184E7C
			// (set) Token: 0x06006914 RID: 26900 RVA: 0x00186CD3 File Offset: 0x00184ED3
			public int Length
			{
				get
				{
					if (this.length != -1)
					{
						return this.length;
					}
					if (this.owner != null && !string.IsNullOrEmpty(this.owner.Text))
					{
						StringInfo stringInfo = new StringInfo(this.owner.Text);
						return stringInfo.LengthInTextElements - this.Start;
					}
					return 0;
				}
				set
				{
					if (this.length != value)
					{
						this.length = value;
						if (this.owner != null)
						{
							this.owner.InvalidateTextLayout();
							this.owner.Invalidate();
						}
					}
				}
			}

			// Token: 0x170016B2 RID: 5810
			// (get) Token: 0x06006915 RID: 26901 RVA: 0x00186D03 File Offset: 0x00184F03
			// (set) Token: 0x06006916 RID: 26902 RVA: 0x00186D0B File Offset: 0x00184F0B
			[DefaultValue(null)]
			public object LinkData
			{
				get
				{
					return this.linkData;
				}
				set
				{
					this.linkData = value;
				}
			}

			// Token: 0x170016B3 RID: 5811
			// (get) Token: 0x06006917 RID: 26903 RVA: 0x00186D14 File Offset: 0x00184F14
			// (set) Token: 0x06006918 RID: 26904 RVA: 0x00186D1C File Offset: 0x00184F1C
			internal LinkLabel Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					this.owner = value;
				}
			}

			// Token: 0x170016B4 RID: 5812
			// (get) Token: 0x06006919 RID: 26905 RVA: 0x00186D25 File Offset: 0x00184F25
			// (set) Token: 0x0600691A RID: 26906 RVA: 0x00186D2D File Offset: 0x00184F2D
			internal LinkState State
			{
				get
				{
					return this.state;
				}
				set
				{
					this.state = value;
				}
			}

			// Token: 0x170016B5 RID: 5813
			// (get) Token: 0x0600691B RID: 26907 RVA: 0x00186D36 File Offset: 0x00184F36
			// (set) Token: 0x0600691C RID: 26908 RVA: 0x00186D4C File Offset: 0x00184F4C
			[DefaultValue("")]
			[SRCategory("CatAppearance")]
			[SRDescription("TreeNodeNodeNameDescr")]
			public string Name
			{
				get
				{
					if (this.name != null)
					{
						return this.name;
					}
					return "";
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x170016B6 RID: 5814
			// (get) Token: 0x0600691D RID: 26909 RVA: 0x00186D55 File Offset: 0x00184F55
			// (set) Token: 0x0600691E RID: 26910 RVA: 0x00186D60 File Offset: 0x00184F60
			public int Start
			{
				get
				{
					return this.start;
				}
				set
				{
					if (this.start != value)
					{
						this.start = value;
						if (this.owner != null)
						{
							this.owner.links.Sort(LinkLabel.linkComparer);
							this.owner.InvalidateTextLayout();
							this.owner.Invalidate();
						}
					}
				}
			}

			// Token: 0x170016B7 RID: 5815
			// (get) Token: 0x0600691F RID: 26911 RVA: 0x00186DB0 File Offset: 0x00184FB0
			// (set) Token: 0x06006920 RID: 26912 RVA: 0x00186DB8 File Offset: 0x00184FB8
			[SRCategory("CatData")]
			[Localizable(false)]
			[Bindable(true)]
			[SRDescription("ControlTagDescr")]
			[DefaultValue(null)]
			[TypeConverter(typeof(StringConverter))]
			public object Tag
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			// Token: 0x170016B8 RID: 5816
			// (get) Token: 0x06006921 RID: 26913 RVA: 0x00186DC1 File Offset: 0x00184FC1
			// (set) Token: 0x06006922 RID: 26914 RVA: 0x00186DD0 File Offset: 0x00184FD0
			[DefaultValue(false)]
			public bool Visited
			{
				get
				{
					return (this.State & LinkState.Visited) == LinkState.Visited;
				}
				set
				{
					bool visited = this.Visited;
					if (value)
					{
						this.State |= LinkState.Visited;
					}
					else
					{
						this.State &= (LinkState)(-5);
					}
					if (visited != this.Visited && this.owner != null)
					{
						this.owner.InvalidateLink(this);
					}
				}
			}

			// Token: 0x170016B9 RID: 5817
			// (get) Token: 0x06006923 RID: 26915 RVA: 0x00186E23 File Offset: 0x00185023
			// (set) Token: 0x06006924 RID: 26916 RVA: 0x00186E2B File Offset: 0x0018502B
			internal Region VisualRegion
			{
				get
				{
					return this.visualRegion;
				}
				set
				{
					this.visualRegion = value;
				}
			}

			// Token: 0x04003B1F RID: 15135
			private int start;

			// Token: 0x04003B20 RID: 15136
			private object linkData;

			// Token: 0x04003B21 RID: 15137
			private LinkState state;

			// Token: 0x04003B22 RID: 15138
			private bool enabled = true;

			// Token: 0x04003B23 RID: 15139
			private Region visualRegion;

			// Token: 0x04003B24 RID: 15140
			internal int length;

			// Token: 0x04003B25 RID: 15141
			private LinkLabel owner;

			// Token: 0x04003B26 RID: 15142
			private string name;

			// Token: 0x04003B27 RID: 15143
			private string description;

			// Token: 0x04003B28 RID: 15144
			internal LinkLabel.LinkAccessibleObject accessibleObject;

			// Token: 0x04003B29 RID: 15145
			private object userData;
		}

		// Token: 0x020006BD RID: 1725
		private class LinkComparer : IComparer
		{
			// Token: 0x06006925 RID: 26917 RVA: 0x00186E34 File Offset: 0x00185034
			int IComparer.Compare(object link1, object link2)
			{
				int start = ((LinkLabel.Link)link1).Start;
				int start2 = ((LinkLabel.Link)link2).Start;
				return start - start2;
			}
		}

		// Token: 0x020006BE RID: 1726
		[ComVisible(true)]
		internal class LinkLabelAccessibleObject : Label.LabelAccessibleObject
		{
			// Token: 0x06006927 RID: 26919 RVA: 0x00186E5C File Offset: 0x0018505C
			public LinkLabelAccessibleObject(LinkLabel owner) : base(owner)
			{
			}

			// Token: 0x06006928 RID: 26920 RVA: 0x00186E68 File Offset: 0x00185068
			internal override void ClearOwnerControlInternal()
			{
				LinkLabel linkLabel = base.Owner as LinkLabel;
				if (linkLabel != null && linkLabel.links != null)
				{
					foreach (object obj in linkLabel.links)
					{
						LinkLabel.Link link = (LinkLabel.Link)obj;
						LinkLabel.LinkAccessibleObject accessibleObject = link.accessibleObject;
						if (accessibleObject != null)
						{
							accessibleObject.ClearOwnerLink();
						}
					}
				}
				base.ClearOwnerControlInternal();
			}

			// Token: 0x06006929 RID: 26921 RVA: 0x00186EE8 File Offset: 0x001850E8
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed() && (AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported());
			}

			// Token: 0x0600692A RID: 26922 RVA: 0x00186F04 File Offset: 0x00185104
			public override AccessibleObject GetChild(int index)
			{
				if (!base.IsOwnerControlDestroyed() && index >= 0 && index < ((LinkLabel)base.Owner).Links.Count)
				{
					return ((LinkLabel)base.Owner).Links[index].AccessibilityObject;
				}
				return null;
			}

			// Token: 0x0600692B RID: 26923 RVA: 0x00186F52 File Offset: 0x00185152
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30010 && (base.IsOwnerControlDestroyed() || !base.Owner.Enabled))
				{
					return false;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x0600692C RID: 26924 RVA: 0x00186F80 File Offset: 0x00185180
			public override AccessibleObject HitTest(int x, int y)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				Point point = base.Owner.PointToClient(new Point(x, y));
				LinkLabel.Link link = ((LinkLabel)base.Owner).PointInLink(point.X, point.Y);
				if (link != null)
				{
					return link.AccessibilityObject;
				}
				if (this.Bounds.Contains(x, y))
				{
					return this;
				}
				return null;
			}

			// Token: 0x0600692D RID: 26925 RVA: 0x00186FE8 File Offset: 0x001851E8
			public override int GetChildCount()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return 0;
				}
				return ((LinkLabel)base.Owner).Links.Count;
			}
		}

		// Token: 0x020006BF RID: 1727
		[ComVisible(true)]
		internal class LinkAccessibleObject : AccessibleObject
		{
			// Token: 0x0600692E RID: 26926 RVA: 0x00187009 File Offset: 0x00185209
			public LinkAccessibleObject(LinkLabel.Link link)
			{
				this.link = link;
				link.accessibleObject = this;
			}

			// Token: 0x0600692F RID: 26927 RVA: 0x0018701F File Offset: 0x0018521F
			public void ClearOwnerLink()
			{
				this.link = null;
			}

			// Token: 0x06006930 RID: 26928 RVA: 0x00187028 File Offset: 0x00185228
			private bool IsOwnerLinkDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.link == null;
			}

			// Token: 0x170016BA RID: 5818
			// (get) Token: 0x06006931 RID: 26929 RVA: 0x0018703C File Offset: 0x0018523C
			public override Rectangle Bounds
			{
				get
				{
					if (this.IsOwnerLinkDestroyed())
					{
						return Rectangle.Empty;
					}
					Region visualRegion = this.link.VisualRegion;
					Graphics graphics = null;
					IntSecurity.ObjectFromWin32Handle.Assert();
					try
					{
						graphics = Graphics.FromHwnd(this.link.Owner.Handle);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
					if (visualRegion == null)
					{
						this.link.Owner.EnsureRun(graphics);
						visualRegion = this.link.VisualRegion;
						if (visualRegion == null)
						{
							graphics.Dispose();
							return Rectangle.Empty;
						}
					}
					Rectangle r;
					try
					{
						r = Rectangle.Ceiling(visualRegion.GetBounds(graphics));
					}
					finally
					{
						graphics.Dispose();
					}
					return this.link.Owner.RectangleToScreen(r);
				}
			}

			// Token: 0x170016BB RID: 5819
			// (get) Token: 0x06006932 RID: 26930 RVA: 0x00187100 File Offset: 0x00185300
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccessibleActionClick");
				}
			}

			// Token: 0x170016BC RID: 5820
			// (get) Token: 0x06006933 RID: 26931 RVA: 0x0018710C File Offset: 0x0018530C
			public override string Description
			{
				get
				{
					if (this.IsOwnerLinkDestroyed())
					{
						return string.Empty;
					}
					return this.link.Description;
				}
			}

			// Token: 0x170016BD RID: 5821
			// (get) Token: 0x06006934 RID: 26932 RVA: 0x00187128 File Offset: 0x00185328
			// (set) Token: 0x06006935 RID: 26933 RVA: 0x0017012F File Offset: 0x0016E32F
			public override string Name
			{
				get
				{
					if (this.IsOwnerLinkDestroyed())
					{
						return string.Empty;
					}
					string text = this.link.Owner.Text;
					string text2;
					if (AccessibilityImprovements.Level3)
					{
						text2 = text;
						if (this.link.Owner.UseMnemonic)
						{
							text2 = WindowsFormsUtils.TextWithoutMnemonics(text2);
						}
					}
					else
					{
						int num = LinkLabel.ConvertToCharIndex(this.link.Start, text);
						int num2 = LinkLabel.ConvertToCharIndex(this.link.Start + this.link.Length, text);
						text2 = text.Substring(num, num2 - num);
						if (AccessibilityImprovements.Level1 && this.link.Owner.UseMnemonic)
						{
							text2 = WindowsFormsUtils.TextWithoutMnemonics(text2);
						}
					}
					return text2;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x170016BE RID: 5822
			// (get) Token: 0x06006936 RID: 26934 RVA: 0x001871D4 File Offset: 0x001853D4
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.IsOwnerLinkDestroyed())
					{
						return null;
					}
					return this.link.Owner.AccessibilityObject;
				}
			}

			// Token: 0x170016BF RID: 5823
			// (get) Token: 0x06006937 RID: 26935 RVA: 0x001782A3 File Offset: 0x001764A3
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Link;
				}
			}

			// Token: 0x170016C0 RID: 5824
			// (get) Token: 0x06006938 RID: 26936 RVA: 0x001871F0 File Offset: 0x001853F0
			public override AccessibleStates State
			{
				get
				{
					if (this.IsOwnerLinkDestroyed())
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable;
					if (this.link.Owner.FocusLink == this.link)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x170016C1 RID: 5825
			// (get) Token: 0x06006939 RID: 26937 RVA: 0x0018722A File Offset: 0x0018542A
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (AccessibilityImprovements.Level1)
					{
						return string.Empty;
					}
					return this.Name;
				}
			}

			// Token: 0x0600693A RID: 26938 RVA: 0x0018723F File Offset: 0x0018543F
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.IsOwnerLinkDestroyed())
				{
					return;
				}
				this.link.Owner.OnLinkClicked(new LinkLabelLinkClickedEventArgs(this.link));
			}

			// Token: 0x0600693B RID: 26939 RVA: 0x00187265 File Offset: 0x00185465
			internal override bool IsIAccessibleExSupported()
			{
				return !this.IsOwnerLinkDestroyed() && (AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported());
			}

			// Token: 0x0600693C RID: 26940 RVA: 0x00187280 File Offset: 0x00185480
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30010 && (this.IsOwnerLinkDestroyed() || !this.link.Owner.Enabled))
				{
					return false;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04003B2A RID: 15146
			private LinkLabel.Link link;
		}
	}
}
