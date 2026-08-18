using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000375 RID: 885
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("PanelClick")]
	[DefaultProperty("Text")]
	[Designer("System.Windows.Forms.Design.StatusBarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class StatusBar : Control
	{
		// Token: 0x060039DC RID: 14812 RVA: 0x0010004C File Offset: 0x000FE24C
		public StatusBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Selectable, false);
			this.Dock = DockStyle.Bottom;
			this.TabStop = false;
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x00100098 File Offset: 0x000FE298
		private static VisualStyleRenderer VisualStyleRenderer
		{
			get
			{
				if (VisualStyleRenderer.IsSupported)
				{
					if (StatusBar.renderer == null)
					{
						StatusBar.renderer = new VisualStyleRenderer(VisualStyleElement.ToolBar.Button.Normal);
					}
				}
				else
				{
					StatusBar.renderer = null;
				}
				return StatusBar.renderer;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x001000C4 File Offset: 0x000FE2C4
		private int SizeGripWidth
		{
			get
			{
				if (this.sizeGripWidth == 0)
				{
					if (Application.RenderWithVisualStyles && StatusBar.VisualStyleRenderer != null)
					{
						VisualStyleRenderer visualStyleRenderer = StatusBar.VisualStyleRenderer;
						VisualStyleElement normal = VisualStyleElement.Status.GripperPane.Normal;
						visualStyleRenderer.SetParameters(normal);
						this.sizeGripWidth = visualStyleRenderer.GetPartSize(Graphics.FromHwndInternal(base.Handle), ThemeSizeType.True).Width;
						normal = VisualStyleElement.Status.Gripper.Normal;
						visualStyleRenderer.SetParameters(normal);
						Size partSize = visualStyleRenderer.GetPartSize(Graphics.FromHwndInternal(base.Handle), ThemeSizeType.True);
						this.sizeGripWidth += partSize.Width;
						this.sizeGripWidth = Math.Max(this.sizeGripWidth, 16);
					}
					else
					{
						this.sizeGripWidth = 16;
					}
				}
				return this.sizeGripWidth;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x00030717 File Offset: 0x0002E917
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return SystemColors.Control;
			}
			set
			{
			}
		}

		// Token: 0x140002C8 RID: 712
		// (add) Token: 0x060039E1 RID: 14817 RVA: 0x00058DD2 File Offset: 0x00056FD2
		// (remove) Token: 0x060039E2 RID: 14818 RVA: 0x00058DDB File Offset: 0x00056FDB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x00011A98 File Offset: 0x0000FC98
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		// Token: 0x140002C9 RID: 713
		// (add) Token: 0x060039E5 RID: 14821 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x060039E6 RID: 14822 RVA: 0x00011AAA File Offset: 0x0000FCAA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x00011ABB File Offset: 0x0000FCBB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x140002CA RID: 714
		// (add) Token: 0x060039E9 RID: 14825 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x060039EA RID: 14826 RVA: 0x00011ACD File Offset: 0x0000FCCD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x00100178 File Offset: 0x000FE378
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "msctls_statusbar32";
				if (this.sizeGrip)
				{
					createParams.Style |= 256;
				}
				else
				{
					createParams.Style &= -257;
				}
				createParams.Style |= 12;
				return createParams;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000111DC File Offset: 0x0000F3DC
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x060039EF RID: 14831 RVA: 0x000131DF File Offset: 0x000113DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x060039F1 RID: 14833 RVA: 0x000FFF26 File Offset: 0x000FE126
		[Localizable(true)]
		[DefaultValue(DockStyle.Bottom)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x060039F3 RID: 14835 RVA: 0x001001D5 File Offset: 0x000FE3D5
		[Localizable(true)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				this.SetPanelContentsWidths(false);
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x060039F5 RID: 14837 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x140002CB RID: 715
		// (add) Token: 0x060039F6 RID: 14838 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x060039F7 RID: 14839 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x060039F9 RID: 14841 RVA: 0x0001A1F5 File Offset: 0x000183F5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		// Token: 0x140002CC RID: 716
		// (add) Token: 0x060039FA RID: 14842 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x060039FB RID: 14843 RVA: 0x00024115 File Offset: 0x00022315
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x001001E5 File Offset: 0x000FE3E5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("StatusBarPanelsDescr")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[MergableProperty(false)]
		public StatusBar.StatusBarPanelCollection Panels
		{
			get
			{
				if (this.panelsCollection == null)
				{
					this.panelsCollection = new StatusBar.StatusBarPanelCollection(this);
				}
				return this.panelsCollection;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x00100201 File Offset: 0x000FE401
		// (set) Token: 0x060039FE RID: 14846 RVA: 0x00100217 File Offset: 0x000FE417
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.simpleText == null)
				{
					return "";
				}
				return this.simpleText;
			}
			set
			{
				this.SetSimpleText(value);
				if (this.simpleText != value)
				{
					this.simpleText = value;
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x00100240 File Offset: 0x000FE440
		// (set) Token: 0x06003A00 RID: 14848 RVA: 0x00100248 File Offset: 0x000FE448
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("StatusBarShowPanelsDescr")]
		public bool ShowPanels
		{
			get
			{
				return this.showPanels;
			}
			set
			{
				if (this.showPanels != value)
				{
					this.showPanels = value;
					this.layoutDirty = true;
					if (base.IsHandleCreated)
					{
						int wparam = (!this.showPanels) ? 1 : 0;
						base.SendMessage(1033, wparam, 0);
						if (this.showPanels)
						{
							base.PerformLayout();
							this.RealizePanels();
						}
						else if (this.tooltips != null)
						{
							for (int i = 0; i < this.panels.Count; i++)
							{
								this.tooltips.SetTool(this.panels[i], null);
							}
						}
						this.SetSimpleText(this.simpleText);
					}
				}
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x001002EA File Offset: 0x000FE4EA
		// (set) Token: 0x06003A02 RID: 14850 RVA: 0x001002F2 File Offset: 0x000FE4F2
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("StatusBarSizingGripDescr")]
		public bool SizingGrip
		{
			get
			{
				return this.sizeGrip;
			}
			set
			{
				if (value != this.sizeGrip)
				{
					this.sizeGrip = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x000B2619 File Offset: 0x000B0819
		[DefaultValue(false)]
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

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x0010030A File Offset: 0x000FE50A
		internal bool ToolTipSet
		{
			get
			{
				return this.toolTipSet;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x00100312 File Offset: 0x000FE512
		internal ToolTip MainToolTip
		{
			get
			{
				return this.mainToolTip;
			}
		}

		// Token: 0x140002CD RID: 717
		// (add) Token: 0x06003A07 RID: 14855 RVA: 0x0010031A File Offset: 0x000FE51A
		// (remove) Token: 0x06003A08 RID: 14856 RVA: 0x0010032D File Offset: 0x000FE52D
		[SRCategory("CatBehavior")]
		[SRDescription("StatusBarDrawItem")]
		public event StatusBarDrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(StatusBar.EVENT_SBDRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(StatusBar.EVENT_SBDRAWITEM, value);
			}
		}

		// Token: 0x140002CE RID: 718
		// (add) Token: 0x06003A09 RID: 14857 RVA: 0x00100340 File Offset: 0x000FE540
		// (remove) Token: 0x06003A0A RID: 14858 RVA: 0x00100353 File Offset: 0x000FE553
		[SRCategory("CatMouse")]
		[SRDescription("StatusBarOnPanelClickDescr")]
		public event StatusBarPanelClickEventHandler PanelClick
		{
			add
			{
				base.Events.AddHandler(StatusBar.EVENT_PANELCLICK, value);
			}
			remove
			{
				base.Events.RemoveHandler(StatusBar.EVENT_PANELCLICK, value);
			}
		}

		// Token: 0x140002CF RID: 719
		// (add) Token: 0x06003A0B RID: 14859 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06003A0C RID: 14860 RVA: 0x00013F90 File Offset: 0x00012190
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x00100366 File Offset: 0x000FE566
		internal bool ArePanelsRealized()
		{
			return this.showPanels && base.IsHandleCreated;
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x00100378 File Offset: 0x000FE578
		internal void DirtyLayout()
		{
			this.layoutDirty = true;
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x00100384 File Offset: 0x000FE584
		private void ApplyPanelWidths()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			int count = this.panels.Count;
			if (count == 0)
			{
				int[] array = new int[]
				{
					base.Size.Width
				};
				if (this.sizeGrip)
				{
					array[0] -= this.SizeGripWidth;
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1028, 1, array);
				base.SendMessage(1039, 0, IntPtr.Zero);
				return;
			}
			int[] array2 = new int[count];
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				num += statusBarPanel.Width;
				array2[i] = num;
				statusBarPanel.Right = array2[i];
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1028, count, array2);
			for (int j = 0; j < count; j++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[j];
				this.UpdateTooltip(statusBarPanel);
			}
			this.layoutDirty = false;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x0010049C File Offset: 0x000FE69C
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 4
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x001004EC File Offset: 0x000FE6EC
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.panelsCollection != null)
			{
				StatusBarPanel[] array = new StatusBarPanel[this.panelsCollection.Count];
				((ICollection)this.panelsCollection).CopyTo(array, 0);
				this.panelsCollection.Clear();
				foreach (StatusBarPanel statusBarPanel in array)
				{
					statusBarPanel.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x0010054E File Offset: 0x000FE74E
		private void ForcePanelUpdate()
		{
			if (this.ArePanelsRealized())
			{
				this.layoutDirty = true;
				this.SetPanelContentsWidths(true);
				base.PerformLayout();
				this.RealizePanels();
			}
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x00100574 File Offset: 0x000FE774
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!base.DesignMode)
			{
				this.tooltips = new StatusBar.ControlToolTip(this);
			}
			if (!this.showPanels)
			{
				base.SendMessage(1033, 1, 0);
				this.SetSimpleText(this.simpleText);
				return;
			}
			this.ForcePanelUpdate();
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x001005C5 File Offset: 0x000FE7C5
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (this.tooltips != null)
			{
				this.tooltips.Dispose();
				this.tooltips = null;
			}
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x001005E8 File Offset: 0x000FE7E8
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.lastClick.X = e.X;
			this.lastClick.Y = e.Y;
			base.OnMouseDown(e);
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x00100614 File Offset: 0x000FE814
		protected virtual void OnPanelClick(StatusBarPanelClickEventArgs e)
		{
			StatusBarPanelClickEventHandler statusBarPanelClickEventHandler = (StatusBarPanelClickEventHandler)base.Events[StatusBar.EVENT_PANELCLICK];
			if (statusBarPanelClickEventHandler != null)
			{
				statusBarPanelClickEventHandler(this, e);
			}
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x00100642 File Offset: 0x000FE842
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (this.showPanels)
			{
				this.LayoutPanels();
				if (base.IsHandleCreated && this.panelsRealized != this.panels.Count)
				{
					this.RealizePanels();
				}
			}
			base.OnLayout(levent);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0010067C File Offset: 0x000FE87C
		internal void RealizePanels()
		{
			int count = this.panels.Count;
			int num = this.panelsRealized;
			this.panelsRealized = 0;
			if (count == 0)
			{
				base.SendMessage(NativeMethods.SB_SETTEXT, 0, "");
			}
			int i;
			for (i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				try
				{
					statusBarPanel.Realize();
					this.panelsRealized++;
				}
				catch
				{
				}
			}
			while (i < num)
			{
				base.SendMessage(NativeMethods.SB_SETTEXT, 0, null);
				i++;
			}
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x0010071C File Offset: 0x000FE91C
		internal void RemoveAllPanelsWithoutUpdate()
		{
			int count = this.panels.Count;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				statusBarPanel.ParentInternal = null;
			}
			this.panels.Clear();
			if (this.showPanels)
			{
				this.ApplyPanelWidths();
				this.ForcePanelUpdate();
			}
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x0010077C File Offset: 0x000FE97C
		internal void SetPanelContentsWidths(bool newPanels)
		{
			int count = this.panels.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Contents)
				{
					int contentsWidth = statusBarPanel.GetContentsWidth(newPanels);
					if (statusBarPanel.Width != contentsWidth)
					{
						statusBarPanel.Width = contentsWidth;
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.DirtyLayout();
				base.PerformLayout();
			}
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x001007EC File Offset: 0x000FE9EC
		private void SetSimpleText(string simpleText)
		{
			if (!this.showPanels && base.IsHandleCreated)
			{
				int num = 511;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					num |= 1024;
				}
				base.SendMessage(NativeMethods.SB_SETTEXT, num, simpleText);
			}
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x00100830 File Offset: 0x000FEA30
		private void LayoutPanels()
		{
			int num = 0;
			int num2 = 0;
			StatusBarPanel[] array = new StatusBarPanel[this.panels.Count];
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Spring)
				{
					array[num2] = statusBarPanel;
					num2++;
				}
				else
				{
					num += statusBarPanel.Width;
				}
			}
			if (num2 > 0)
			{
				Rectangle bounds = base.Bounds;
				int j = num2;
				int num3 = bounds.Width - num;
				if (this.sizeGrip)
				{
					num3 -= this.SizeGripWidth;
				}
				int num4 = int.MinValue;
				while (j > 0)
				{
					int num5 = num3 / j;
					if (num3 == num4)
					{
						break;
					}
					num4 = num3;
					for (int k = 0; k < num2; k++)
					{
						StatusBarPanel statusBarPanel = array[k];
						if (statusBarPanel != null)
						{
							if (num5 < statusBarPanel.MinWidth)
							{
								if (statusBarPanel.Width != statusBarPanel.MinWidth)
								{
									flag = true;
								}
								statusBarPanel.Width = statusBarPanel.MinWidth;
								array[k] = null;
								j--;
								num3 -= statusBarPanel.MinWidth;
							}
							else
							{
								if (statusBarPanel.Width != num5)
								{
									flag = true;
								}
								statusBarPanel.Width = num5;
							}
						}
					}
				}
			}
			if (flag || this.layoutDirty)
			{
				this.ApplyPanelWidths();
			}
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x00100968 File Offset: 0x000FEB68
		protected virtual void OnDrawItem(StatusBarDrawItemEventArgs sbdievent)
		{
			StatusBarDrawItemEventHandler statusBarDrawItemEventHandler = (StatusBarDrawItemEventHandler)base.Events[StatusBar.EVENT_SBDRAWITEM];
			if (statusBarDrawItemEventHandler != null)
			{
				statusBarDrawItemEventHandler(this, sbdievent);
			}
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x00100996 File Offset: 0x000FEB96
		protected override void OnResize(EventArgs e)
		{
			base.Invalidate();
			base.OnResize(e);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x001009A8 File Offset: 0x000FEBA8
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Panels != null)
			{
				text = text + ", Panels.Count: " + this.Panels.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Panels.Count > 0)
				{
					text = text + ", Panels[0]: " + this.Panels[0].ToString();
				}
			}
			return text;
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x00100A14 File Offset: 0x000FEC14
		internal void SetToolTip(ToolTip t)
		{
			this.mainToolTip = t;
			this.toolTipSet = true;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x00100A24 File Offset: 0x000FEC24
		internal void UpdateTooltip(StatusBarPanel panel)
		{
			if (this.tooltips == null)
			{
				if (!base.IsHandleCreated || base.DesignMode)
				{
					return;
				}
				this.tooltips = new StatusBar.ControlToolTip(this);
			}
			if (panel.Parent == this && panel.ToolTipText.Length > 0)
			{
				int width = SystemInformation.Border3DSize.Width;
				StatusBar.ControlToolTip.Tool tool = this.tooltips.GetTool(panel);
				if (tool == null)
				{
					tool = new StatusBar.ControlToolTip.Tool();
				}
				tool.text = panel.ToolTipText;
				tool.rect = new Rectangle(panel.Right - panel.Width + width, 0, panel.Width - width, base.Height);
				this.tooltips.SetTool(panel, tool);
				return;
			}
			this.tooltips.SetTool(panel, null);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x00100AE4 File Offset: 0x000FECE4
		private void UpdatePanelIndex()
		{
			int count = this.panels.Count;
			for (int i = 0; i < count; i++)
			{
				((StatusBarPanel)this.panels[i]).Index = i;
			}
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x00100B20 File Offset: 0x000FED20
		private void WmDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			int count = this.panels.Count;
			if (drawitemstruct.itemID >= 0)
			{
				int itemID = drawitemstruct.itemID;
			}
			StatusBarPanel panel = (StatusBarPanel)this.panels[drawitemstruct.itemID];
			Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC);
			Rectangle r = Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom);
			this.OnDrawItem(new StatusBarDrawItemEventArgs(graphics, this.Font, r, drawitemstruct.itemID, DrawItemState.None, panel, this.ForeColor, this.BackColor));
			graphics.Dispose();
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x00100BE8 File Offset: 0x000FEDE8
		private void WmNotifyNMClick(NativeMethods.NMHDR note)
		{
			if (!this.showPanels)
			{
				return;
			}
			int count = this.panels.Count;
			int num = 0;
			int num2 = -1;
			for (int i = 0; i < count; i++)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.panels[i];
				num += statusBarPanel.Width;
				if (this.lastClick.X < num)
				{
					num2 = i;
					break;
				}
			}
			if (num2 != -1)
			{
				MouseButtons button = MouseButtons.Left;
				int clicks = 0;
				switch (note.code)
				{
				case -6:
					button = MouseButtons.Right;
					clicks = 2;
					break;
				case -5:
					button = MouseButtons.Right;
					clicks = 1;
					break;
				case -3:
					button = MouseButtons.Left;
					clicks = 2;
					break;
				case -2:
					button = MouseButtons.Left;
					clicks = 1;
					break;
				}
				Point point = this.lastClick;
				StatusBarPanel statusBarPanel2 = (StatusBarPanel)this.panels[num2];
				StatusBarPanelClickEventArgs e = new StatusBarPanelClickEventArgs(statusBarPanel2, button, clicks, point.X, point.Y);
				this.OnPanelClick(e);
			}
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x00100CF0 File Offset: 0x000FEEF0
		private void WmNCHitTest(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.LParam);
			Rectangle bounds = base.Bounds;
			bool flag = true;
			if (num > bounds.X + bounds.Width - this.SizeGripWidth)
			{
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null && parentInternal is Form)
				{
					FormBorderStyle formBorderStyle = ((Form)parentInternal).FormBorderStyle;
					if (formBorderStyle != FormBorderStyle.Sizable && formBorderStyle != FormBorderStyle.SizableToolWindow)
					{
						flag = false;
					}
					if (!((Form)parentInternal).TopLevel || this.Dock != DockStyle.Bottom)
					{
						flag = false;
					}
					if (flag)
					{
						Control.ControlCollection controls = parentInternal.Controls;
						int count = controls.Count;
						for (int i = 0; i < count; i++)
						{
							Control control = controls[i];
							if (control != this && control.Dock == DockStyle.Bottom && control.Top > base.Top)
							{
								flag = false;
								break;
							}
						}
					}
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				base.WndProc(ref m);
				return;
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x00100DE4 File Offset: 0x000FEFE4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 132)
			{
				if (msg != 78)
				{
					if (msg != 132)
					{
						goto IL_7B;
					}
					this.WmNCHitTest(ref m);
					return;
				}
			}
			else
			{
				if (msg == 8235)
				{
					this.WmDrawItem(ref m);
					return;
				}
				if (msg != 8270)
				{
					goto IL_7B;
				}
			}
			NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
			int code = nmhdr.code;
			if (code - -6 <= 1 || code - -3 <= 1)
			{
				this.WmNotifyNMClick(nmhdr);
				return;
			}
			base.WndProc(ref m);
			return;
			IL_7B:
			base.WndProc(ref m);
		}

		// Token: 0x040022DC RID: 8924
		private int sizeGripWidth;

		// Token: 0x040022DD RID: 8925
		private const int SIMPLE_INDEX = 255;

		// Token: 0x040022DE RID: 8926
		private static readonly object EVENT_PANELCLICK = new object();

		// Token: 0x040022DF RID: 8927
		private static readonly object EVENT_SBDRAWITEM = new object();

		// Token: 0x040022E0 RID: 8928
		private bool showPanels;

		// Token: 0x040022E1 RID: 8929
		private bool layoutDirty;

		// Token: 0x040022E2 RID: 8930
		private int panelsRealized;

		// Token: 0x040022E3 RID: 8931
		private bool sizeGrip = true;

		// Token: 0x040022E4 RID: 8932
		private string simpleText;

		// Token: 0x040022E5 RID: 8933
		private Point lastClick = new Point(0, 0);

		// Token: 0x040022E6 RID: 8934
		private IList panels = new ArrayList();

		// Token: 0x040022E7 RID: 8935
		private StatusBar.StatusBarPanelCollection panelsCollection;

		// Token: 0x040022E8 RID: 8936
		private StatusBar.ControlToolTip tooltips;

		// Token: 0x040022E9 RID: 8937
		private ToolTip mainToolTip;

		// Token: 0x040022EA RID: 8938
		private bool toolTipSet;

		// Token: 0x040022EB RID: 8939
		private static VisualStyleRenderer renderer = null;

		// Token: 0x020007EA RID: 2026
		[ListBindable(false)]
		public class StatusBarPanelCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006DFE RID: 28158 RVA: 0x00193913 File Offset: 0x00191B13
			public StatusBarPanelCollection(StatusBar owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001806 RID: 6150
			public virtual StatusBarPanel this[int index]
			{
				get
				{
					return (StatusBarPanel)this.owner.panels[index];
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("StatusBarPanel");
					}
					this.owner.layoutDirty = true;
					if (value.Parent != null)
					{
						throw new ArgumentException(SR.GetString("ObjectHasParent"), "value");
					}
					int count = this.owner.panels.Count;
					if (index < 0 || index >= count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					StatusBarPanel statusBarPanel = (StatusBarPanel)this.owner.panels[index];
					statusBarPanel.ParentInternal = null;
					value.ParentInternal = this.owner;
					if (value.AutoSize == StatusBarPanelAutoSize.Contents)
					{
						value.Width = value.GetContentsWidth(true);
					}
					this.owner.panels[index] = value;
					value.Index = index;
					if (this.owner.ArePanelsRealized())
					{
						this.owner.PerformLayout();
						value.Realize();
					}
				}
			}

			// Token: 0x17001807 RID: 6151
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is StatusBarPanel)
					{
						this[index] = (StatusBarPanel)value;
						return;
					}
					throw new ArgumentException(SR.GetString("StatusBarBadStatusBarPanel"), "value");
				}
			}

			// Token: 0x17001808 RID: 6152
			public virtual StatusBarPanel this[string key]
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

			// Token: 0x17001809 RID: 6153
			// (get) Token: 0x06006E04 RID: 28164 RVA: 0x00193AB1 File Offset: 0x00191CB1
			[Browsable(false)]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public int Count
			{
				get
				{
					return this.owner.panels.Count;
				}
			}

			// Token: 0x1700180A RID: 6154
			// (get) Token: 0x06006E05 RID: 28165 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700180B RID: 6155
			// (get) Token: 0x06006E06 RID: 28166 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700180C RID: 6156
			// (get) Token: 0x06006E07 RID: 28167 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700180D RID: 6157
			// (get) Token: 0x06006E08 RID: 28168 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006E09 RID: 28169 RVA: 0x00193AC4 File Offset: 0x00191CC4
			public virtual StatusBarPanel Add(string text)
			{
				StatusBarPanel statusBarPanel = new StatusBarPanel();
				statusBarPanel.Text = text;
				this.Add(statusBarPanel);
				return statusBarPanel;
			}

			// Token: 0x06006E0A RID: 28170 RVA: 0x00193AE8 File Offset: 0x00191CE8
			public virtual int Add(StatusBarPanel value)
			{
				int count = this.owner.panels.Count;
				this.Insert(count, value);
				return count;
			}

			// Token: 0x06006E0B RID: 28171 RVA: 0x00193B0F File Offset: 0x00191D0F
			int IList.Add(object value)
			{
				if (value is StatusBarPanel)
				{
					return this.Add((StatusBarPanel)value);
				}
				throw new ArgumentException(SR.GetString("StatusBarBadStatusBarPanel"), "value");
			}

			// Token: 0x06006E0C RID: 28172 RVA: 0x00193B3C File Offset: 0x00191D3C
			public virtual void AddRange(StatusBarPanel[] panels)
			{
				if (panels == null)
				{
					throw new ArgumentNullException("panels");
				}
				foreach (StatusBarPanel value in panels)
				{
					this.Add(value);
				}
			}

			// Token: 0x06006E0D RID: 28173 RVA: 0x00193B73 File Offset: 0x00191D73
			public bool Contains(StatusBarPanel panel)
			{
				return this.IndexOf(panel) != -1;
			}

			// Token: 0x06006E0E RID: 28174 RVA: 0x00193B82 File Offset: 0x00191D82
			bool IList.Contains(object panel)
			{
				return panel is StatusBarPanel && this.Contains((StatusBarPanel)panel);
			}

			// Token: 0x06006E0F RID: 28175 RVA: 0x00193B9A File Offset: 0x00191D9A
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006E10 RID: 28176 RVA: 0x00193BAC File Offset: 0x00191DAC
			public int IndexOf(StatusBarPanel panel)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == panel)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006E11 RID: 28177 RVA: 0x00193BD7 File Offset: 0x00191DD7
			int IList.IndexOf(object panel)
			{
				if (panel is StatusBarPanel)
				{
					return this.IndexOf((StatusBarPanel)panel);
				}
				return -1;
			}

			// Token: 0x06006E12 RID: 28178 RVA: 0x00193BF0 File Offset: 0x00191DF0
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

			// Token: 0x06006E13 RID: 28179 RVA: 0x00193C70 File Offset: 0x00191E70
			public virtual void Insert(int index, StatusBarPanel value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.owner.layoutDirty = true;
				if (value.Parent != this.owner && value.Parent != null)
				{
					throw new ArgumentException(SR.GetString("ObjectHasParent"), "value");
				}
				int count = this.owner.panels.Count;
				if (index < 0 || index > count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				value.ParentInternal = this.owner;
				StatusBarPanelAutoSize autoSize = value.AutoSize;
				if (autoSize - StatusBarPanelAutoSize.None > 1 && autoSize == StatusBarPanelAutoSize.Contents)
				{
					value.Width = value.GetContentsWidth(true);
				}
				this.owner.panels.Insert(index, value);
				this.owner.UpdatePanelIndex();
				this.owner.ForcePanelUpdate();
			}

			// Token: 0x06006E14 RID: 28180 RVA: 0x00193D5F File Offset: 0x00191F5F
			void IList.Insert(int index, object value)
			{
				if (value is StatusBarPanel)
				{
					this.Insert(index, (StatusBarPanel)value);
					return;
				}
				throw new ArgumentException(SR.GetString("StatusBarBadStatusBarPanel"), "value");
			}

			// Token: 0x06006E15 RID: 28181 RVA: 0x00193D8B File Offset: 0x00191F8B
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006E16 RID: 28182 RVA: 0x00193D9C File Offset: 0x00191F9C
			public virtual void Clear()
			{
				this.owner.RemoveAllPanelsWithoutUpdate();
				this.owner.PerformLayout();
			}

			// Token: 0x06006E17 RID: 28183 RVA: 0x00193DB4 File Offset: 0x00191FB4
			public virtual void Remove(StatusBarPanel value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("StatusBarPanel");
				}
				if (value.Parent != this.owner)
				{
					return;
				}
				this.RemoveAt(value.Index);
			}

			// Token: 0x06006E18 RID: 28184 RVA: 0x00193DDF File Offset: 0x00191FDF
			void IList.Remove(object value)
			{
				if (value is StatusBarPanel)
				{
					this.Remove((StatusBarPanel)value);
				}
			}

			// Token: 0x06006E19 RID: 28185 RVA: 0x00193DF8 File Offset: 0x00191FF8
			public virtual void RemoveAt(int index)
			{
				int count = this.Count;
				if (index < 0 || index >= count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				StatusBarPanel statusBarPanel = (StatusBarPanel)this.owner.panels[index];
				this.owner.panels.RemoveAt(index);
				statusBarPanel.ParentInternal = null;
				this.owner.UpdateTooltip(statusBarPanel);
				this.owner.UpdatePanelIndex();
				this.owner.ForcePanelUpdate();
			}

			// Token: 0x06006E1A RID: 28186 RVA: 0x00193E98 File Offset: 0x00192098
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x06006E1B RID: 28187 RVA: 0x00193EBD File Offset: 0x001920BD
			void ICollection.CopyTo(Array dest, int index)
			{
				this.owner.panels.CopyTo(dest, index);
			}

			// Token: 0x06006E1C RID: 28188 RVA: 0x00193ED1 File Offset: 0x001920D1
			public IEnumerator GetEnumerator()
			{
				if (this.owner.panels != null)
				{
					return this.owner.panels.GetEnumerator();
				}
				return new StatusBarPanel[0].GetEnumerator();
			}

			// Token: 0x040042D1 RID: 17105
			private StatusBar owner;

			// Token: 0x040042D2 RID: 17106
			private int lastAccessedIndex = -1;
		}

		// Token: 0x020007EB RID: 2027
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private class ControlToolTip
		{
			// Token: 0x06006E1D RID: 28189 RVA: 0x00193EFC File Offset: 0x001920FC
			public ControlToolTip(Control parent)
			{
				this.window = new StatusBar.ControlToolTip.ToolTipNativeWindow(this);
				this.parent = parent;
			}

			// Token: 0x1700180E RID: 6158
			// (get) Token: 0x06006E1E RID: 28190 RVA: 0x00193F24 File Offset: 0x00192124
			protected CreateParams CreateParams
			{
				get
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 8
					});
					CreateParams createParams = new CreateParams();
					createParams.Parent = IntPtr.Zero;
					createParams.ClassName = "tooltips_class32";
					createParams.Style |= 1;
					createParams.ExStyle = 0;
					createParams.Caption = null;
					return createParams;
				}
			}

			// Token: 0x1700180F RID: 6159
			// (get) Token: 0x06006E1F RID: 28191 RVA: 0x00193F7E File Offset: 0x0019217E
			public IntPtr Handle
			{
				get
				{
					if (this.window.Handle == IntPtr.Zero)
					{
						this.CreateHandle();
					}
					return this.window.Handle;
				}
			}

			// Token: 0x17001810 RID: 6160
			// (get) Token: 0x06006E20 RID: 28192 RVA: 0x00193FA8 File Offset: 0x001921A8
			private bool IsHandleCreated
			{
				get
				{
					return this.window.Handle != IntPtr.Zero;
				}
			}

			// Token: 0x06006E21 RID: 28193 RVA: 0x00193FBF File Offset: 0x001921BF
			private void AssignId(StatusBar.ControlToolTip.Tool tool)
			{
				tool.id = (IntPtr)this.nextId;
				this.nextId++;
			}

			// Token: 0x06006E22 RID: 28194 RVA: 0x00193FE0 File Offset: 0x001921E0
			public void SetTool(object key, StatusBar.ControlToolTip.Tool tool)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				StatusBar.ControlToolTip.Tool tool2 = null;
				if (this.tools.ContainsKey(key))
				{
					tool2 = (StatusBar.ControlToolTip.Tool)this.tools[key];
				}
				if (tool2 != null)
				{
					flag = true;
				}
				if (tool != null)
				{
					flag2 = true;
				}
				if (tool != null && tool2 != null && tool.id == tool2.id)
				{
					flag3 = true;
				}
				if (flag3)
				{
					this.UpdateTool(tool);
				}
				else
				{
					if (flag)
					{
						this.RemoveTool(tool2);
					}
					if (flag2)
					{
						this.AddTool(tool);
					}
				}
				if (tool != null)
				{
					this.tools[key] = tool;
					return;
				}
				this.tools.Remove(key);
			}

			// Token: 0x06006E23 RID: 28195 RVA: 0x00194077 File Offset: 0x00192277
			public StatusBar.ControlToolTip.Tool GetTool(object key)
			{
				return (StatusBar.ControlToolTip.Tool)this.tools[key];
			}

			// Token: 0x06006E24 RID: 28196 RVA: 0x0019408C File Offset: 0x0019228C
			private void AddTool(StatusBar.ControlToolTip.Tool tool)
			{
				if (tool != null && tool.text != null && tool.text.Length > 0)
				{
					StatusBar statusBar = (StatusBar)this.parent;
					int num;
					if (statusBar.ToolTipSet)
					{
						num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(statusBar.MainToolTip, statusBar.MainToolTip.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO(tool));
					}
					else
					{
						num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_ADDTOOL, 0, this.GetTOOLINFO(tool));
					}
					if (num == 0)
					{
						throw new InvalidOperationException(SR.GetString("StatusBarAddFailed"));
					}
				}
			}

			// Token: 0x06006E25 RID: 28197 RVA: 0x00194134 File Offset: 0x00192334
			private void RemoveTool(StatusBar.ControlToolTip.Tool tool)
			{
				if (tool != null && tool.text != null && tool.text.Length > 0 && (int)tool.id >= 0)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_DELTOOL, 0, this.GetMinTOOLINFO(tool));
				}
			}

			// Token: 0x06006E26 RID: 28198 RVA: 0x00194188 File Offset: 0x00192388
			private void UpdateTool(StatusBar.ControlToolTip.Tool tool)
			{
				if (tool != null && tool.text != null && tool.text.Length > 0 && (int)tool.id >= 0)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.TTM_SETTOOLINFO, 0, this.GetTOOLINFO(tool));
				}
			}

			// Token: 0x06006E27 RID: 28199 RVA: 0x001941DC File Offset: 0x001923DC
			protected void CreateHandle()
			{
				if (this.IsHandleCreated)
				{
					return;
				}
				this.window.CreateHandle(this.CreateParams);
				SafeNativeMethods.SetWindowPos(new HandleRef(this, this.Handle), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
			}

			// Token: 0x06006E28 RID: 28200 RVA: 0x00194245 File Offset: 0x00192445
			protected void DestroyHandle()
			{
				if (this.IsHandleCreated)
				{
					this.window.DestroyHandle();
					this.tools.Clear();
				}
			}

			// Token: 0x06006E29 RID: 28201 RVA: 0x00194265 File Offset: 0x00192465
			public void Dispose()
			{
				this.DestroyHandle();
			}

			// Token: 0x06006E2A RID: 28202 RVA: 0x00194270 File Offset: 0x00192470
			private NativeMethods.TOOLINFO_T GetMinTOOLINFO(StatusBar.ControlToolTip.Tool tool)
			{
				NativeMethods.TOOLINFO_T toolinfo_T = new NativeMethods.TOOLINFO_T();
				toolinfo_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				toolinfo_T.hwnd = this.parent.Handle;
				if ((int)tool.id < 0)
				{
					this.AssignId(tool);
				}
				StatusBar statusBar = (StatusBar)this.parent;
				if (statusBar != null && statusBar.ToolTipSet)
				{
					toolinfo_T.uId = this.parent.Handle;
				}
				else
				{
					toolinfo_T.uId = tool.id;
				}
				return toolinfo_T;
			}

			// Token: 0x06006E2B RID: 28203 RVA: 0x001942F8 File Offset: 0x001924F8
			private NativeMethods.TOOLINFO_T GetTOOLINFO(StatusBar.ControlToolTip.Tool tool)
			{
				NativeMethods.TOOLINFO_T minTOOLINFO = this.GetMinTOOLINFO(tool);
				minTOOLINFO.cbSize = Marshal.SizeOf(typeof(NativeMethods.TOOLINFO_T));
				minTOOLINFO.uFlags |= 272;
				Control control = this.parent;
				if (control != null && control.RightToLeft == RightToLeft.Yes)
				{
					minTOOLINFO.uFlags |= 4;
				}
				minTOOLINFO.lpszText = tool.text;
				minTOOLINFO.rect = NativeMethods.RECT.FromXYWH(tool.rect.X, tool.rect.Y, tool.rect.Width, tool.rect.Height);
				return minTOOLINFO;
			}

			// Token: 0x06006E2C RID: 28204 RVA: 0x0019439C File Offset: 0x0019259C
			~ControlToolTip()
			{
				this.DestroyHandle();
			}

			// Token: 0x06006E2D RID: 28205 RVA: 0x001943C8 File Offset: 0x001925C8
			protected void WndProc(ref Message msg)
			{
				int msg2 = msg.Msg;
				if (msg2 == 7)
				{
					return;
				}
				this.window.DefWndProc(ref msg);
			}

			// Token: 0x040042D3 RID: 17107
			private Hashtable tools = new Hashtable();

			// Token: 0x040042D4 RID: 17108
			private StatusBar.ControlToolTip.ToolTipNativeWindow window;

			// Token: 0x040042D5 RID: 17109
			private Control parent;

			// Token: 0x040042D6 RID: 17110
			private int nextId;

			// Token: 0x020008C7 RID: 2247
			public class Tool
			{
				// Token: 0x04004553 RID: 17747
				public Rectangle rect = Rectangle.Empty;

				// Token: 0x04004554 RID: 17748
				public string text;

				// Token: 0x04004555 RID: 17749
				internal IntPtr id = new IntPtr(-1);
			}

			// Token: 0x020008C8 RID: 2248
			private class ToolTipNativeWindow : NativeWindow
			{
				// Token: 0x06007307 RID: 29447 RVA: 0x001A4DD2 File Offset: 0x001A2FD2
				internal ToolTipNativeWindow(StatusBar.ControlToolTip control)
				{
					this.control = control;
				}

				// Token: 0x06007308 RID: 29448 RVA: 0x001A4DE1 File Offset: 0x001A2FE1
				protected override void WndProc(ref Message m)
				{
					if (this.control != null)
					{
						this.control.WndProc(ref m);
					}
				}

				// Token: 0x04004556 RID: 17750
				private StatusBar.ControlToolTip control;
			}
		}
	}
}
