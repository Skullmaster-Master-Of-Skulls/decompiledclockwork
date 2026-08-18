using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200010E RID: 270
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("TextChanged")]
	[DefaultBindingProperty("Text")]
	[Designer("System.Windows.Forms.Design.TextBoxBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class TextBoxBase : Control
	{
		// Token: 0x060006AE RID: 1710 RVA: 0x00012D5C File Offset: 0x00010F5C
		internal TextBoxBase()
		{
			base.SetState2(2048, true);
			this.textBoxFlags[TextBoxBase.autoSize | TextBoxBase.hideSelection | TextBoxBase.wordWrap | TextBoxBase.shortcutsEnabled] = true;
			base.SetStyle(ControlStyles.FixedHeight, this.textBoxFlags[TextBoxBase.autoSize]);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick | ControlStyles.UseTextForAccessibility, false);
			this.requestedHeight = base.Height;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00012DE0 File Offset: 0x00010FE0
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00012DF2 File Offset: 0x00010FF2
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsTabDescr")]
		public bool AcceptsTab
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.acceptsTab];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.acceptsTab] != value)
				{
					this.textBoxFlags[TextBoxBase.acceptsTab] = value;
					this.OnAcceptsTabChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060006B1 RID: 1713 RVA: 0x00012E23 File Offset: 0x00011023
		// (remove) Token: 0x060006B2 RID: 1714 RVA: 0x00012E36 File Offset: 0x00011036
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnAcceptsTabChangedDescr")]
		public event EventHandler AcceptsTabChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_ACCEPTSTABCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_ACCEPTSTABCHANGED, value);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00012E49 File Offset: 0x00011049
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00012E5B File Offset: 0x0001105B
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxShortcutsEnabledDescr")]
		public virtual bool ShortcutsEnabled
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.shortcutsEnabled];
			}
			set
			{
				if (TextBoxBase.shortcutsToDisable == null)
				{
					TextBoxBase.shortcutsToDisable = new int[]
					{
						131162,
						131139,
						131160,
						131158,
						131137,
						131148,
						131154,
						131141,
						131161,
						131080,
						131118,
						65582,
						65581,
						131146
					};
				}
				this.textBoxFlags[TextBoxBase.shortcutsEnabled] = value;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00012E8C File Offset: 0x0001108C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool flag = base.ProcessCmdKey(ref msg, keyData);
			if (!this.ShortcutsEnabled)
			{
				foreach (int num in TextBoxBase.shortcutsToDisable)
				{
					if (keyData == (Keys)num || keyData == (Keys)(num | 65536))
					{
						return true;
					}
				}
			}
			return (this.textBoxFlags[TextBoxBase.readOnly] && (keyData == (Keys)131148 || keyData == (Keys)131154 || keyData == (Keys)131141 || keyData == (Keys)131146)) || flag;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00012F0E File Offset: 0x0001110E
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x00012F20 File Offset: 0x00011120
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("TextBoxAutoSizeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoSize
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.autoSize];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.autoSize] != value)
				{
					this.textBoxFlags[TextBoxBase.autoSize] = value;
					if (!this.Multiline)
					{
						base.SetStyle(ControlStyles.FixedHeight, value);
						this.AdjustHeight(false);
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00012F74 File Offset: 0x00011174
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x00012F98 File Offset: 0x00011198
		[SRCategory("CatAppearance")]
		[DispId(-501)]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				if (this.ReadOnly)
				{
					return SystemColors.Control;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060006BC RID: 1724 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x060006BD RID: 1725 RVA: 0x00011A5F File Offset: 0x0000FC5F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060006BE RID: 1726 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x060006BF RID: 1727 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060006C2 RID: 1730 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x060006C3 RID: 1731 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00012FA1 File Offset: 0x000111A1
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x00012FAC File Offset: 0x000111AC
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("TextBoxBorderDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
					base.RecreateHandle();
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.BorderStyle))
					{
						this.OnBorderStyleChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060006C6 RID: 1734 RVA: 0x0001303C File Offset: 0x0001123C
		// (remove) Token: 0x060006C7 RID: 1735 RVA: 0x0001304F File Offset: 0x0001124F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnBorderStyleChangedDescr")]
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_BORDERSTYLECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_BORDERSTYLECHANGED, value);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool CanRaiseTextChangedEvent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00013068 File Offset: 0x00011268
		protected override bool CanEnableIme
		{
			get
			{
				return !this.ReadOnly && !this.PasswordProtect && base.CanEnableIme;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00013090 File Offset: 0x00011290
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxCanUndoDescr")]
		public bool CanUndo
		{
			get
			{
				return base.IsHandleCreated && (int)((long)base.SendMessage(198, 0, 0)) != 0;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x000130C0 File Offset: 0x000112C0
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "EDIT";
				createParams.Style |= 192;
				if (!this.textBoxFlags[TextBoxBase.hideSelection])
				{
					createParams.Style |= 256;
				}
				if (this.textBoxFlags[TextBoxBase.readOnly])
				{
					createParams.Style |= 2048;
				}
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				if (this.textBoxFlags[TextBoxBase.multiline])
				{
					createParams.Style |= 4;
					if (this.textBoxFlags[TextBoxBase.wordWrap])
					{
						createParams.Style &= -129;
					}
				}
				return createParams;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x000131DF File Offset: 0x000113DF
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

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060006CE RID: 1742 RVA: 0x000131E8 File Offset: 0x000113E8
		// (remove) Token: 0x060006CF RID: 1743 RVA: 0x000131F1 File Offset: 0x000113F1
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060006D0 RID: 1744 RVA: 0x000131FA File Offset: 0x000113FA
		// (remove) Token: 0x060006D1 RID: 1745 RVA: 0x00013203 File Offset: 0x00011403
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001320C File Offset: 0x0001140C
		protected override Cursor DefaultCursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00013213 File Offset: 0x00011413
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, this.PreferredHeight);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00013238 File Offset: 0x00011438
		[SRCategory("CatAppearance")]
		[DispId(-513)]
		[SRDescription("ControlForeColorDescr")]
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00013241 File Offset: 0x00011441
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00013253 File Offset: 0x00011453
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.hideSelection];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.hideSelection] != value)
				{
					this.textBoxFlags[TextBoxBase.hideSelection] = value;
					base.RecreateHandle();
					this.OnHideSelectionChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060006D8 RID: 1752 RVA: 0x0001328A File Offset: 0x0001148A
		// (remove) Token: 0x060006D9 RID: 1753 RVA: 0x0001329D File Offset: 0x0001149D
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnHideSelectionChangedDescr")]
		public event EventHandler HideSelectionChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_HIDESELECTIONCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_HIDESELECTIONCHANGED, value);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x000132B0 File Offset: 0x000114B0
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x000132DF File Offset: 0x000114DF
		protected override ImeMode ImeModeBase
		{
			get
			{
				if (base.DesignMode)
				{
					return base.ImeModeBase;
				}
				return this.CanEnableIme ? base.ImeModeBase : ImeMode.Disable;
			}
			set
			{
				base.ImeModeBase = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x000132E8 File Offset: 0x000114E8
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x000133CC File Offset: 0x000115CC
		[SRCategory("CatAppearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MergableProperty(false)]
		[Localizable(true)]
		[SRDescription("TextBoxLinesDescr")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string[] Lines
		{
			get
			{
				string text = this.Text;
				ArrayList arrayList = new ArrayList();
				int j;
				for (int i = 0; i < text.Length; i = j)
				{
					for (j = i; j < text.Length; j++)
					{
						char c = text[j];
						if (c == '\r' || c == '\n')
						{
							break;
						}
					}
					string value = text.Substring(i, j - i);
					arrayList.Add(value);
					if (j < text.Length && text[j] == '\r')
					{
						j++;
					}
					if (j < text.Length && text[j] == '\n')
					{
						j++;
					}
				}
				if (text.Length > 0 && (text[text.Length - 1] == '\r' || text[text.Length - 1] == '\n'))
				{
					arrayList.Add("");
				}
				return (string[])arrayList.ToArray(typeof(string));
			}
			set
			{
				if (value != null && value.Length != 0)
				{
					StringBuilder stringBuilder = new StringBuilder(value[0]);
					for (int i = 1; i < value.Length; i++)
					{
						stringBuilder.Append("\r\n");
						stringBuilder.Append(value[i]);
					}
					this.Text = stringBuilder.ToString();
					return;
				}
				this.Text = "";
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00013425 File Offset: 0x00011625
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00013430 File Offset: 0x00011630
		[SRCategory("CatBehavior")]
		[DefaultValue(32767)]
		[Localizable(true)]
		[SRDescription("TextBoxMaxLengthDescr")]
		public virtual int MaxLength
		{
			get
			{
				return this.maxLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MaxLength", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MaxLength",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.maxLength != value)
				{
					this.maxLength = value;
					this.UpdateMaxLength();
				}
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001349C File Offset: 0x0001169C
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0001350C File Offset: 0x0001170C
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxModifiedDescr")]
		public bool Modified
		{
			get
			{
				if (base.IsHandleCreated)
				{
					bool flag = (int)((long)base.SendMessage(184, 0, 0)) != 0;
					if (this.textBoxFlags[TextBoxBase.modified] != flag)
					{
						this.textBoxFlags[TextBoxBase.modified] = flag;
						this.OnModifiedChanged(EventArgs.Empty);
					}
					return flag;
				}
				return this.textBoxFlags[TextBoxBase.modified];
			}
			set
			{
				if (this.Modified != value)
				{
					if (base.IsHandleCreated)
					{
						base.SendMessage(185, value ? 1 : 0, 0);
					}
					this.textBoxFlags[TextBoxBase.modified] = value;
					this.OnModifiedChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060006E2 RID: 1762 RVA: 0x0001355A File Offset: 0x0001175A
		// (remove) Token: 0x060006E3 RID: 1763 RVA: 0x0001356D File Offset: 0x0001176D
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnModifiedChangedDescr")]
		public event EventHandler ModifiedChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_MODIFIEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_MODIFIEDCHANGED, value);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00013580 File Offset: 0x00011780
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00013594 File Offset: 0x00011794
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("TextBoxMultilineDescr")]
		[RefreshProperties(RefreshProperties.All)]
		public virtual bool Multiline
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.multiline];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.multiline] != value)
				{
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Multiline))
					{
						this.textBoxFlags[TextBoxBase.multiline] = value;
						if (value)
						{
							base.SetStyle(ControlStyles.FixedHeight, false);
						}
						else
						{
							base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
						}
						base.RecreateHandle();
						this.AdjustHeight(false);
						this.OnMultilineChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060006E6 RID: 1766 RVA: 0x00013630 File Offset: 0x00011830
		// (remove) Token: 0x060006E7 RID: 1767 RVA: 0x00013643 File Offset: 0x00011843
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnMultilineChangedDescr")]
		public event EventHandler MultilineChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_MULTILINECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_MULTILINECHANGED, value);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060006EA RID: 1770 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x060006EB RID: 1771 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRCategory("CatLayout")]
		[SRDescription("ControlOnPaddingChangedDescr")]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool PasswordProtect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001367C File Offset: 0x0001187C
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxPreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				int num = base.FontHeight;
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.GetBorderSizeForDpi(this.deviceDpi).Height * 4 + 3;
				}
				return num;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000136B4 File Offset: 0x000118B4
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size size = this.SizeFromClientSize(Size.Empty) + this.Padding.Size;
			if (this.BorderStyle != BorderStyle.None)
			{
				size += new Size(0, 3);
			}
			if (this.BorderStyle == BorderStyle.FixedSingle)
			{
				size.Width += 2;
				size.Height += 2;
			}
			proposedConstraints -= size;
			TextFormatFlags textFormatFlags = TextFormatFlags.NoPrefix;
			if (!this.Multiline)
			{
				textFormatFlags |= TextFormatFlags.SingleLine;
			}
			else if (this.WordWrap)
			{
				textFormatFlags |= TextFormatFlags.WordBreak;
			}
			Size sz = TextRenderer.MeasureText(this.Text, this.Font, proposedConstraints, textFormatFlags);
			sz.Height = Math.Max(sz.Height, base.FontHeight);
			return sz + size;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00013780 File Offset: 0x00011980
		internal void GetSelectionStartAndLength(out int start, out int length)
		{
			int num = 0;
			if (!base.IsHandleCreated)
			{
				this.AdjustSelectionStartAndEnd(this.selectionStart, this.selectionLength, out start, out num, -1);
				length = num - start;
				return;
			}
			start = 0;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 176, ref start, ref num);
			start = Math.Max(0, start);
			num = Math.Max(0, num);
			if (this.SelectionUsesDbcsOffsetsInWin9x && Marshal.SystemDefaultCharSize == 1)
			{
				TextBoxBase.ToUnicodeOffsets(this.WindowText, ref start, ref num);
			}
			length = num - start;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00013807 File Offset: 0x00011A07
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001381C File Offset: 0x00011A1C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TextBoxReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.readOnly];
			}
			set
			{
				if (this.textBoxFlags[TextBoxBase.readOnly] != value)
				{
					this.textBoxFlags[TextBoxBase.readOnly] = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(207, value ? -1 : 0, 0);
					}
					this.OnReadOnlyChanged(EventArgs.Empty);
					base.VerifyImeRestrictedModeChanged();
				}
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060006F2 RID: 1778 RVA: 0x0001387A File Offset: 0x00011A7A
		// (remove) Token: 0x060006F3 RID: 1779 RVA: 0x0001388D File Offset: 0x00011A8D
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnReadOnlyChangedDescr")]
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(TextBoxBase.EVENT_READONLYCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBoxBase.EVENT_READONLYCHANGED, value);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x000138A0 File Offset: 0x00011AA0
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x000138C4 File Offset: 0x00011AC4
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectedTextDescr")]
		public virtual string SelectedText
		{
			get
			{
				int startIndex;
				int length;
				this.GetSelectionStartAndLength(out startIndex, out length);
				return this.Text.Substring(startIndex, length);
			}
			set
			{
				this.SetSelectedTextInternal(value, true);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000138D0 File Offset: 0x00011AD0
		internal virtual void SetSelectedTextInternal(string text, bool clearUndo)
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (text == null)
			{
				text = "";
			}
			base.SendMessage(197, 0, 0);
			if (clearUndo)
			{
				base.SendMessage(194, 0, text);
				base.SendMessage(185, 0, 0);
				this.ClearUndo();
			}
			else
			{
				base.SendMessage(194, -1, text);
			}
			base.SendMessage(197, this.maxLength, 0);
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001394C File Offset: 0x00011B4C
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00013964 File Offset: 0x00011B64
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionLengthDescr")]
		public virtual int SelectionLength
		{
			get
			{
				int num;
				int result;
				this.GetSelectionStartAndLength(out num, out result);
				return result;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectionLength", SR.GetString("InvalidArgument", new object[]
					{
						"SelectionLength",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				int start;
				int num;
				this.GetSelectionStartAndLength(out start, out num);
				if (value != num)
				{
					this.Select(start, value);
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x000139C0 File Offset: 0x00011BC0
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x000139D8 File Offset: 0x00011BD8
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				int result;
				int num;
				this.GetSelectionStartAndLength(out result, out num);
				return result;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectionStart", SR.GetString("InvalidArgument", new object[]
					{
						"SelectionStart",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.Select(value, this.SelectionLength);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool SetSelectionInCreateHandle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00013A30 File Offset: 0x00011C30
		[Localizable(true)]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value != base.Text)
				{
					base.Text = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(185, 0, 0);
					}
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00013A5D File Offset: 0x00011C5D
		[Browsable(false)]
		public virtual int TextLength
		{
			get
			{
				if (base.IsHandleCreated && Marshal.SystemDefaultCharSize == 2)
				{
					return SafeNativeMethods.GetWindowTextLength(new HandleRef(this, base.Handle));
				}
				return this.Text.Length;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool SelectionUsesDbcsOffsetsInWin9x
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00013A8C File Offset: 0x00011C8C
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00013A94 File Offset: 0x00011C94
		internal override string WindowText
		{
			get
			{
				return base.WindowText;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (!this.WindowText.Equals(value))
				{
					this.textBoxFlags[TextBoxBase.codeUpdateText] = true;
					try
					{
						base.WindowText = value;
					}
					finally
					{
						this.textBoxFlags[TextBoxBase.codeUpdateText] = false;
					}
				}
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00013AF8 File Offset: 0x00011CF8
		internal void ForceWindowText(string value)
		{
			if (value == null)
			{
				value = "";
			}
			this.textBoxFlags[TextBoxBase.codeUpdateText] = true;
			try
			{
				if (base.IsHandleCreated)
				{
					UnsafeNativeMethods.SetWindowText(new HandleRef(this, base.Handle), value);
				}
				else if (value.Length == 0)
				{
					this.Text = null;
				}
				else
				{
					this.Text = value;
				}
			}
			finally
			{
				this.textBoxFlags[TextBoxBase.codeUpdateText] = false;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00013B7C File Offset: 0x00011D7C
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00013B90 File Offset: 0x00011D90
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("TextBoxWordWrapDescr")]
		public bool WordWrap
		{
			get
			{
				return this.textBoxFlags[TextBoxBase.wordWrap];
			}
			set
			{
				using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.WordWrap))
				{
					if (this.textBoxFlags[TextBoxBase.wordWrap] != value)
					{
						this.textBoxFlags[TextBoxBase.wordWrap] = value;
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00013BFC File Offset: 0x00011DFC
		private void AdjustHeight(bool returnIfAnchored)
		{
			if (returnIfAnchored && (this.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom))
			{
				return;
			}
			int num = this.requestedHeight;
			try
			{
				if (this.textBoxFlags[TextBoxBase.autoSize] && !this.textBoxFlags[TextBoxBase.multiline])
				{
					base.Height = this.PreferredHeight;
				}
				else
				{
					int height = base.Height;
					if (this.textBoxFlags[TextBoxBase.multiline])
					{
						base.Height = Math.Max(num, this.PreferredHeight + 2);
					}
					this.integralHeightAdjust = true;
					try
					{
						base.Height = num;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
			finally
			{
				this.requestedHeight = num;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00013CBC File Offset: 0x00011EBC
		public void AppendText(string text)
		{
			if (text.Length > 0)
			{
				int start;
				int length;
				this.GetSelectionStartAndLength(out start, out length);
				try
				{
					int endPosition = this.GetEndPosition();
					this.SelectInternal(endPosition, endPosition, endPosition);
					this.SelectedText = text;
				}
				finally
				{
					if (base.Width == 0 || base.Height == 0)
					{
						this.Select(start, length);
					}
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00013D20 File Offset: 0x00011F20
		public void Clear()
		{
			this.Text = null;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00013D29 File Offset: 0x00011F29
		public void ClearUndo()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(205, 0, 0);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00013D41 File Offset: 0x00011F41
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public void Copy()
		{
			base.SendMessage(769, 0, 0);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00013D51 File Offset: 0x00011F51
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (!AccessibilityImprovements.Level5)
			{
				return base.CreateAccessibilityInstance();
			}
			return new TextBoxBase.TextBoxBaseAccessibleObject(this);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00013D68 File Offset: 0x00011F68
		protected override void CreateHandle()
		{
			this.textBoxFlags[TextBoxBase.creatingHandle] = true;
			try
			{
				base.CreateHandle();
				if (this.SetSelectionInCreateHandle)
				{
					this.SetSelectionOnHandle();
				}
			}
			finally
			{
				this.textBoxFlags[TextBoxBase.creatingHandle] = false;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public void Cut()
		{
			base.SendMessage(768, 0, 0);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00013DD0 File Offset: 0x00011FD0
		internal virtual int GetEndPosition()
		{
			if (!base.IsHandleCreated)
			{
				return this.TextLength;
			}
			return this.TextLength + 1;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00013DEC File Offset: 0x00011FEC
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) != Keys.Alt)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Tab)
				{
					if (keys != Keys.Back)
					{
						if (keys == Keys.Tab)
						{
							return this.Multiline && this.textBoxFlags[TextBoxBase.acceptsTab] && (keyData & Keys.Control) == Keys.None;
						}
					}
					else if (!this.ReadOnly)
					{
						return true;
					}
				}
				else if (keys != Keys.Escape)
				{
					if (keys - Keys.Prior <= 3)
					{
						return true;
					}
				}
				else if (this.Multiline)
				{
					return false;
				}
			}
			return base.IsInputKey(keyData);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00013E74 File Offset: 0x00012074
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			CommonProperties.xClearPreferredSizeCache(this);
			this.AdjustHeight(true);
			this.UpdateMaxLength();
			if (this.textBoxFlags[TextBoxBase.modified])
			{
				base.SendMessage(185, 1, 0);
			}
			if (this.textBoxFlags[TextBoxBase.scrollToCaretOnHandleCreated])
			{
				this.ScrollToCaret();
				this.textBoxFlags[TextBoxBase.scrollToCaretOnHandleCreated] = false;
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00013EE4 File Offset: 0x000120E4
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.textBoxFlags[TextBoxBase.modified] = this.Modified;
			this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated] = true;
			this.GetSelectionStartAndLength(out this.selectionStart, out this.selectionLength);
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00013F31 File Offset: 0x00012131
		[UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
		public void Paste()
		{
			IntSecurity.ClipboardRead.Demand();
			base.SendMessage(770, 0, 0);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00013F4C File Offset: 0x0001214C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys == Keys.Tab && this.AcceptsTab && (keyData & Keys.Control) != Keys.None)
			{
				keyData &= ~Keys.Control;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000713 RID: 1811 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06000714 RID: 1812 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x06000715 RID: 1813 RVA: 0x00013F9C File Offset: 0x0001219C
		protected virtual void OnAcceptsTabChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_ACCEPTSTABCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00013FCC File Offset: 0x000121CC
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_BORDERSTYLECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00013FFA File Offset: 0x000121FA
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AdjustHeight(false);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001400C File Offset: 0x0001220C
		protected virtual void OnHideSelectionChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_HIDESELECTIONCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001403C File Offset: 0x0001223C
		protected virtual void OnModifiedChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_MODIFIEDCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001406C File Offset: 0x0001226C
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			Point point = base.PointToScreen(mevent.Location);
			if (mevent.Button == MouseButtons.Left)
			{
				if (!base.ValidationCancelled && UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
				{
					if (!this.doubleClickFired)
					{
						this.OnClick(mevent);
						this.OnMouseClick(mevent);
					}
					else
					{
						this.doubleClickFired = false;
						this.OnDoubleClick(mevent);
						this.OnMouseDoubleClick(mevent);
					}
				}
				this.doubleClickFired = false;
			}
			base.OnMouseUp(mevent);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000140F8 File Offset: 0x000122F8
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_MULTILINECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00014126 File Offset: 0x00012326
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			this.AdjustHeight(false);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00014138 File Offset: 0x00012338
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBoxBase.EVENT_READONLYCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00014166 File Offset: 0x00012366
		protected override void OnTextChanged(EventArgs e)
		{
			CommonProperties.xClearPreferredSizeCache(this);
			base.OnTextChanged(e);
			this.RaiseAccessibilityTextChangedEvent();
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001417B File Offset: 0x0001237B
		internal virtual void RaiseAccessibilityTextChangedEvent()
		{
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20015);
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000141A0 File Offset: 0x000123A0
		public virtual char GetCharFromPosition(Point pt)
		{
			string text = this.Text;
			int charIndexFromPosition = this.GetCharIndexFromPosition(pt);
			if (charIndexFromPosition >= 0 && charIndexFromPosition < text.Length)
			{
				return text[charIndexFromPosition];
			}
			return '\0';
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000141D4 File Offset: 0x000123D4
		public virtual int GetCharIndexFromPosition(Point pt)
		{
			int lParam = NativeMethods.Util.MAKELONG(pt.X, pt.Y);
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 215, 0, lParam);
			num = NativeMethods.Util.LOWORD(num);
			if (num < 0)
			{
				num = 0;
			}
			else
			{
				string text = this.Text;
				if (num >= text.Length)
				{
					num = Math.Max(text.Length - 1, 0);
				}
			}
			return num;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00014242 File Offset: 0x00012442
		public virtual int GetLineFromCharIndex(int index)
		{
			return (int)((long)base.SendMessage(201, index, 0));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00014258 File Offset: 0x00012458
		public virtual Point GetPositionFromCharIndex(int index)
		{
			if (index < 0 || index >= this.Text.Length)
			{
				return Point.Empty;
			}
			int n = (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 214, index, 0));
			return new Point(NativeMethods.Util.SignedLOWORD(n), NativeMethods.Util.SignedHIWORD(n));
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000142B0 File Offset: 0x000124B0
		public int GetFirstCharIndexFromLine(int lineNumber)
		{
			if (lineNumber < 0)
			{
				throw new ArgumentOutOfRangeException("lineNumber", SR.GetString("InvalidArgument", new object[]
				{
					"lineNumber",
					lineNumber.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return (int)((long)base.SendMessage(187, lineNumber, 0));
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00014306 File Offset: 0x00012506
		public int GetFirstCharIndexOfCurrentLine()
		{
			return (int)((long)base.SendMessage(187, -1, 0));
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001431C File Offset: 0x0001251C
		public void ScrollToCaret()
		{
			if (base.IsHandleCreated)
			{
				if (string.IsNullOrEmpty(this.WindowText))
				{
					return;
				}
				bool flag = false;
				object o = null;
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					if (UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1084, 0, out o) != 0)
					{
						intPtr = Marshal.GetIUnknownForObject(o);
						if (intPtr != IntPtr.Zero)
						{
							IntPtr zero = IntPtr.Zero;
							Guid guid = typeof(UnsafeNativeMethods.ITextDocument).GUID;
							try
							{
								Marshal.QueryInterface(intPtr, ref guid, out zero);
								UnsafeNativeMethods.ITextDocument textDocument = Marshal.GetObjectForIUnknown(zero) as UnsafeNativeMethods.ITextDocument;
								if (textDocument != null)
								{
									int num;
									int num2;
									this.GetSelectionStartAndLength(out num, out num2);
									int lineFromCharIndex = this.GetLineFromCharIndex(num);
									UnsafeNativeMethods.ITextRange textRange = textDocument.Range(this.WindowText.Length - 1, this.WindowText.Length - 1);
									textRange.ScrollIntoView(0);
									int num3 = (int)((long)base.SendMessage(206, 0, 0));
									if (num3 > lineFromCharIndex)
									{
										textRange = textDocument.Range(num, num + num2);
										textRange.ScrollIntoView(32);
									}
									flag = true;
								}
							}
							finally
							{
								if (zero != IntPtr.Zero)
								{
									Marshal.Release(zero);
								}
							}
						}
					}
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.Release(intPtr);
					}
				}
				if (!flag)
				{
					base.SendMessage(183, 0, 0);
					return;
				}
			}
			else
			{
				this.textBoxFlags[TextBoxBase.scrollToCaretOnHandleCreated] = true;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00014498 File Offset: 0x00012698
		public void DeselectAll()
		{
			this.SelectionLength = 0;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000144A4 File Offset: 0x000126A4
		public void Select(int start, int length)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start", SR.GetString("InvalidArgument", new object[]
				{
					"start",
					start.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int textLength = this.TextLength;
			if (start > textLength)
			{
				long num = Math.Min(0L, (long)length + (long)start - (long)textLength);
				if (num < -2147483648L)
				{
					length = int.MinValue;
				}
				else
				{
					length = (int)num;
				}
				start = textLength;
			}
			this.SelectInternal(start, length, textLength);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00014524 File Offset: 0x00012724
		internal virtual void SelectInternal(int start, int length, int textLen)
		{
			if (base.IsHandleCreated)
			{
				int wparam;
				int lparam;
				this.AdjustSelectionStartAndEnd(start, length, out wparam, out lparam, textLen);
				base.SendMessage(177, wparam, lparam);
				return;
			}
			this.selectionStart = start;
			this.selectionLength = length;
			this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated] = true;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00014574 File Offset: 0x00012774
		public void SelectAll()
		{
			int textLength = this.TextLength;
			this.SelectInternal(0, textLength, textLength);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00014594 File Offset: 0x00012794
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.integralHeightAdjust && height != base.Height)
			{
				this.requestedHeight = height;
			}
			if (this.textBoxFlags[TextBoxBase.autoSize] && !this.textBoxFlags[TextBoxBase.multiline])
			{
				height = this.PreferredHeight;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000145F4 File Offset: 0x000127F4
		private static void Swap(ref int n1, ref int n2)
		{
			int num = n2;
			n2 = n1;
			n1 = num;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001460C File Offset: 0x0001280C
		internal void AdjustSelectionStartAndEnd(int selStart, int selLength, out int start, out int end, int textLen)
		{
			start = selStart;
			end = 0;
			if (start <= -1)
			{
				start = -1;
				return;
			}
			int num;
			if (textLen >= 0)
			{
				num = textLen;
			}
			else
			{
				num = this.TextLength;
			}
			if (start > num)
			{
				start = num;
			}
			try
			{
				end = checked(start + selLength);
			}
			catch (OverflowException)
			{
				end = ((start > 0) ? int.MaxValue : int.MinValue);
			}
			if (end < 0)
			{
				end = 0;
			}
			else if (end > num)
			{
				end = num;
			}
			if (this.SelectionUsesDbcsOffsetsInWin9x && Marshal.SystemDefaultCharSize == 1)
			{
				TextBoxBase.ToDbcsOffsets(this.WindowText, ref start, ref end);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000146A8 File Offset: 0x000128A8
		internal void SetSelectionOnHandle()
		{
			if (this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated])
			{
				this.textBoxFlags[TextBoxBase.setSelectionOnHandleCreated] = false;
				int wparam;
				int lparam;
				this.AdjustSelectionStartAndEnd(this.selectionStart, this.selectionLength, out wparam, out lparam, -1);
				base.SendMessage(177, wparam, lparam);
			}
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00014700 File Offset: 0x00012900
		private static void ToUnicodeOffsets(string str, ref int start, ref int end)
		{
			Encoding @default = Encoding.Default;
			byte[] bytes = @default.GetBytes(str);
			bool flag = start > end;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > bytes.Length)
			{
				start = bytes.Length;
			}
			if (end > bytes.Length)
			{
				end = bytes.Length;
			}
			int num = (start == 0) ? 0 : @default.GetCharCount(bytes, 0, start);
			end = num + @default.GetCharCount(bytes, start, end - start);
			start = num;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00014780 File Offset: 0x00012980
		internal static void ToDbcsOffsets(string str, ref int start, ref int end)
		{
			Encoding @default = Encoding.Default;
			bool flag = start > end;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > str.Length)
			{
				start = str.Length;
			}
			if (end < start)
			{
				end = start;
			}
			if (end > str.Length)
			{
				end = str.Length;
			}
			int num = (start == 0) ? 0 : @default.GetByteCount(str.Substring(0, start));
			end = num + @default.GetByteCount(str.Substring(start, end - start));
			start = num;
			if (flag)
			{
				TextBoxBase.Swap(ref start, ref end);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00014818 File Offset: 0x00012A18
		public override string ToString()
		{
			string str = base.ToString();
			string text = this.Text;
			if (text.Length > 40)
			{
				text = text.Substring(0, 40) + "...";
			}
			return str + ", Text: " + text.ToString();
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00014862 File Offset: 0x00012A62
		public void Undo()
		{
			base.SendMessage(199, 0, 0);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00014872 File Offset: 0x00012A72
		internal virtual void UpdateMaxLength()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(197, this.maxLength, 0);
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001488F File Offset: 0x00012A8F
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (msg == 312 && !this.ShouldSerializeBackColor())
			{
				return IntPtr.Zero;
			}
			return base.InitializeDCForWmCtlColor(dc, msg);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000148B0 File Offset: 0x00012AB0
		private void WmReflectCommand(ref Message m)
		{
			if (!this.textBoxFlags[TextBoxBase.codeUpdateText] && !this.textBoxFlags[TextBoxBase.creatingHandle])
			{
				if (NativeMethods.Util.HIWORD(m.WParam) == 768 && this.CanRaiseTextChangedEvent)
				{
					this.OnTextChanged(EventArgs.Empty);
					return;
				}
				if (NativeMethods.Util.HIWORD(m.WParam) == 1024)
				{
					bool flag = this.Modified;
				}
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00014920 File Offset: 0x00012B20
		private void WmSetFont(ref Message m)
		{
			base.WndProc(ref m);
			if (!this.textBoxFlags[TextBoxBase.multiline])
			{
				base.SendMessage(211, 3, 0);
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001494C File Offset: 0x00012B4C
		private void WmGetDlgCode(ref Message m)
		{
			base.WndProc(ref m);
			if (this.AcceptsTab)
			{
				m.Result = (IntPtr)((int)((long)m.Result) | 2);
				return;
			}
			m.Result = (IntPtr)((int)((long)m.Result) & -7);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001499C File Offset: 0x00012B9C
		private void WmTextBoxContextMenu(ref Message m)
		{
			if (this.ContextMenu != null || this.ContextMenuStrip != null)
			{
				int x = NativeMethods.Util.SignedLOWORD(m.LParam);
				int y = NativeMethods.Util.SignedHIWORD(m.LParam);
				bool isKeyboardActivated = false;
				Point point;
				if ((int)((long)m.LParam) == -1)
				{
					isKeyboardActivated = true;
					point = new Point(base.Width / 2, base.Height / 2);
				}
				else
				{
					point = base.PointToClientInternal(new Point(x, y));
				}
				if (base.ClientRectangle.Contains(point))
				{
					if (this.ContextMenu != null)
					{
						this.ContextMenu.Show(this, point);
						return;
					}
					if (this.ContextMenuStrip != null)
					{
						this.ContextMenuStrip.ShowInternal(this, point, isKeyboardActivated);
						return;
					}
					this.DefWndProc(ref m);
				}
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00014A54 File Offset: 0x00012C54
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 123)
			{
				if (msg != 2)
				{
					if (msg == 48)
					{
						this.WmSetFont(ref m);
						return;
					}
					if (msg == 123)
					{
						if (this.ShortcutsEnabled)
						{
							base.WndProc(ref m);
							return;
						}
						this.WmTextBoxContextMenu(ref m);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
					if (!AccessibilityImprovements.Level5 || !base.IsAccessibilityObjectCreated || base.RecreatingHandle)
					{
						return;
					}
					if (ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
					{
						int num = UnsafeNativeMethods.UiaDisconnectProvider(base.AccessibilityObject);
					}
					TextBoxBase.TextBoxBaseAccessibleObject textBoxBaseAccessibleObject = base.AccessibilityObject as TextBoxBase.TextBoxBaseAccessibleObject;
					if (textBoxBaseAccessibleObject != null)
					{
						textBoxBaseAccessibleObject.ClearObjects();
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 135)
				{
					this.WmGetDlgCode(ref m);
					return;
				}
				if (msg == 515)
				{
					this.doubleClickFired = true;
					base.WndProc(ref m);
					return;
				}
				if (msg == 8465)
				{
					this.WmReflectCommand(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x040004CF RID: 1231
		private static readonly int autoSize = BitVector32.CreateMask();

		// Token: 0x040004D0 RID: 1232
		private static readonly int hideSelection = BitVector32.CreateMask(TextBoxBase.autoSize);

		// Token: 0x040004D1 RID: 1233
		private static readonly int multiline = BitVector32.CreateMask(TextBoxBase.hideSelection);

		// Token: 0x040004D2 RID: 1234
		private static readonly int modified = BitVector32.CreateMask(TextBoxBase.multiline);

		// Token: 0x040004D3 RID: 1235
		private static readonly int readOnly = BitVector32.CreateMask(TextBoxBase.modified);

		// Token: 0x040004D4 RID: 1236
		private static readonly int acceptsTab = BitVector32.CreateMask(TextBoxBase.readOnly);

		// Token: 0x040004D5 RID: 1237
		private static readonly int wordWrap = BitVector32.CreateMask(TextBoxBase.acceptsTab);

		// Token: 0x040004D6 RID: 1238
		private static readonly int creatingHandle = BitVector32.CreateMask(TextBoxBase.wordWrap);

		// Token: 0x040004D7 RID: 1239
		private static readonly int codeUpdateText = BitVector32.CreateMask(TextBoxBase.creatingHandle);

		// Token: 0x040004D8 RID: 1240
		private static readonly int shortcutsEnabled = BitVector32.CreateMask(TextBoxBase.codeUpdateText);

		// Token: 0x040004D9 RID: 1241
		private static readonly int scrollToCaretOnHandleCreated = BitVector32.CreateMask(TextBoxBase.shortcutsEnabled);

		// Token: 0x040004DA RID: 1242
		private static readonly int setSelectionOnHandleCreated = BitVector32.CreateMask(TextBoxBase.scrollToCaretOnHandleCreated);

		// Token: 0x040004DB RID: 1243
		private static readonly object EVENT_ACCEPTSTABCHANGED = new object();

		// Token: 0x040004DC RID: 1244
		private static readonly object EVENT_BORDERSTYLECHANGED = new object();

		// Token: 0x040004DD RID: 1245
		private static readonly object EVENT_HIDESELECTIONCHANGED = new object();

		// Token: 0x040004DE RID: 1246
		private static readonly object EVENT_MODIFIEDCHANGED = new object();

		// Token: 0x040004DF RID: 1247
		private static readonly object EVENT_MULTILINECHANGED = new object();

		// Token: 0x040004E0 RID: 1248
		private static readonly object EVENT_READONLYCHANGED = new object();

		// Token: 0x040004E1 RID: 1249
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x040004E2 RID: 1250
		private int maxLength = 32767;

		// Token: 0x040004E3 RID: 1251
		private int requestedHeight;

		// Token: 0x040004E4 RID: 1252
		private bool integralHeightAdjust;

		// Token: 0x040004E5 RID: 1253
		private int selectionStart;

		// Token: 0x040004E6 RID: 1254
		private int selectionLength;

		// Token: 0x040004E7 RID: 1255
		private bool doubleClickFired;

		// Token: 0x040004E8 RID: 1256
		private static int[] shortcutsToDisable;

		// Token: 0x040004E9 RID: 1257
		private BitVector32 textBoxFlags;

		// Token: 0x020005FA RID: 1530
		internal class TextBoxBaseAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006171 RID: 24945 RVA: 0x001685F0 File Offset: 0x001667F0
			public TextBoxBaseAccessibleObject(TextBoxBase owner) : base(owner)
			{
				this._owningTextBoxBase = owner;
				this._textProvider = new TextBoxBase.TextBoxBaseUiaTextProvider(owner);
			}

			// Token: 0x06006172 RID: 24946 RVA: 0x0016860C File Offset: 0x0016680C
			internal override void ClearOwnerControlInternal()
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider != null)
				{
					textProvider.ClearOwnerTextBoxBase();
				}
				this._textProvider = null;
				this._owningTextBoxBase = null;
				base.ClearOwnerControlInternal();
			}

			// Token: 0x06006173 RID: 24947 RVA: 0x00168633 File Offset: 0x00166833
			internal void ClearObjects()
			{
				this._owningTextBoxBase = null;
			}

			// Token: 0x06006174 RID: 24948 RVA: 0x00162A9D File Offset: 0x00160C9D
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed();
			}

			// Token: 0x06006175 RID: 24949 RVA: 0x0016863C File Offset: 0x0016683C
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && (patternId == 10014 || patternId == 10018 || patternId == 10024 || base.IsPatternSupported(patternId));
			}

			// Token: 0x06006176 RID: 24950 RVA: 0x0016866C File Offset: 0x0016686C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30040)
				{
					switch (propertyID)
					{
					case 30001:
						return this.Bounds;
					case 30002:
					case 30004:
						break;
					case 30003:
						return 50004;
					case 30005:
						return this.Name;
					default:
						if (propertyID == 30040)
						{
							return this.IsPatternSupported(10014);
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30043)
					{
						return this.IsPatternSupported(10002);
					}
					if (propertyID == 30090)
					{
						return this.IsPatternSupported(10018);
					}
					if (propertyID == 30119)
					{
						return this.IsPatternSupported(10024);
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x170014E5 RID: 5349
			// (get) Token: 0x06006177 RID: 24951 RVA: 0x0016872E File Offset: 0x0016692E
			internal override bool IsReadOnly
			{
				get
				{
					TextBoxBase owningTextBoxBase = this._owningTextBoxBase;
					return owningTextBoxBase != null && owningTextBoxBase.ReadOnly;
				}
			}

			// Token: 0x170014E6 RID: 5350
			// (get) Token: 0x06006178 RID: 24952 RVA: 0x00168741 File Offset: 0x00166941
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRangeInternal
			{
				get
				{
					TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
					if (textProvider == null)
					{
						return null;
					}
					return textProvider.DocumentRange;
				}
			}

			// Token: 0x06006179 RID: 24953 RVA: 0x00168754 File Offset: 0x00166954
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextSelection()
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.GetSelection();
			}

			// Token: 0x0600617A RID: 24954 RVA: 0x00168767 File Offset: 0x00166967
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetTextVisibleRanges()
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.GetVisibleRanges();
			}

			// Token: 0x0600617B RID: 24955 RVA: 0x0016877A File Offset: 0x0016697A
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.RangeFromChild(childElement);
			}

			// Token: 0x0600617C RID: 24956 RVA: 0x0016878E File Offset: 0x0016698E
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextRangeFromPoint(Point screenLocation)
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.RangeFromPoint(screenLocation);
			}

			// Token: 0x170014E7 RID: 5351
			// (get) Token: 0x0600617D RID: 24957 RVA: 0x001687A2 File Offset: 0x001669A2
			internal override UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelectionInternal
			{
				get
				{
					TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
					if (textProvider == null)
					{
						return UnsafeNativeMethods.UiaCore.SupportedTextSelection.None;
					}
					return textProvider.SupportedTextSelection;
				}
			}

			// Token: 0x0600617E RID: 24958 RVA: 0x001687B5 File Offset: 0x001669B5
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetTextCaretRange(out UnsafeNativeMethods.BOOL isActive)
			{
				isActive = UnsafeNativeMethods.BOOL.FALSE;
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.GetCaretRange(out isActive);
			}

			// Token: 0x0600617F RID: 24959 RVA: 0x001687CC File Offset: 0x001669CC
			internal override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetRangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
			{
				TextBoxBase.TextBoxBaseUiaTextProvider textProvider = this._textProvider;
				if (textProvider == null)
				{
					return null;
				}
				return textProvider.RangeFromAnnotation(annotationElement);
			}

			// Token: 0x0400389B RID: 14491
			private TextBoxBase _owningTextBoxBase;

			// Token: 0x0400389C RID: 14492
			private TextBoxBase.TextBoxBaseUiaTextProvider _textProvider;
		}

		// Token: 0x020005FB RID: 1531
		internal class TextBoxBaseUiaTextProvider : UiaTextProvider2
		{
			// Token: 0x06006180 RID: 24960 RVA: 0x001687E0 File Offset: 0x001669E0
			public TextBoxBaseUiaTextProvider(TextBoxBase owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this._owningTextBoxBase = owner;
			}

			// Token: 0x06006181 RID: 24961 RVA: 0x001687FE File Offset: 0x001669FE
			internal void ClearOwnerTextBoxBase()
			{
				this._owningTextBoxBase = null;
			}

			// Token: 0x06006182 RID: 24962 RVA: 0x00168807 File Offset: 0x00166A07
			private bool IsOwnerTextBoxBaseDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this._owningTextBoxBase == null;
			}

			// Token: 0x170014E8 RID: 5352
			// (get) Token: 0x06006183 RID: 24963 RVA: 0x0016881B File Offset: 0x00166A1B
			private bool IsHandleCreated
			{
				get
				{
					return !this.IsOwnerTextBoxBaseDestroyed() && this._owningTextBoxBase.IsHandleCreated;
				}
			}

			// Token: 0x06006184 RID: 24964 RVA: 0x00168834 File Offset: 0x00166A34
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetSelection()
			{
				if (!this.IsHandleCreated)
				{
					return null;
				}
				int start = 0;
				int end = 0;
				this._owningTextBoxBase.SendMessage(176, ref start, ref end);
				InternalAccessibleObject enclosingElement = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UnsafeNativeMethods.UiaCore.ITextRangeProvider[]
				{
					new UiaTextRange(enclosingElement, this, start, end)
				};
			}

			// Token: 0x06006185 RID: 24965 RVA: 0x00168888 File Offset: 0x00166A88
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider[] GetVisibleRanges()
			{
				if (!this.IsHandleCreated)
				{
					return null;
				}
				int start;
				int end;
				this.GetVisibleRangePoints(out start, out end);
				InternalAccessibleObject enclosingElement = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UnsafeNativeMethods.UiaCore.ITextRangeProvider[]
				{
					new UiaTextRange(enclosingElement, this, start, end)
				};
			}

			// Token: 0x06006186 RID: 24966 RVA: 0x00015ECC File Offset: 0x000140CC
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromChild(UnsafeNativeMethods.IRawElementProviderSimple childElement)
			{
				return null;
			}

			// Token: 0x06006187 RID: 24967 RVA: 0x001688CC File Offset: 0x00166ACC
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromPoint(Point screenLocation)
			{
				if (!this.IsHandleCreated)
				{
					return null;
				}
				Point pt = screenLocation;
				if (UnsafeNativeMethods.MapWindowPoint(IntPtr.Zero, this._owningTextBoxBase.InternalHandle, ref pt) == 0)
				{
					return new UiaTextRange(new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject), this, 0, 0);
				}
				NativeMethods.RECT rect = this._owningTextBoxBase.ClientRectangle;
				pt.X = Math.Max(pt.X, rect.left);
				pt.X = Math.Min(pt.X, rect.right);
				pt.Y = Math.Max(pt.Y, rect.top);
				pt.Y = Math.Min(pt.Y, rect.bottom);
				int charIndexFromPosition = this._owningTextBoxBase.GetCharIndexFromPosition(pt);
				return new UiaTextRange(new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject), this, charIndexFromPosition, charIndexFromPosition);
			}

			// Token: 0x170014E9 RID: 5353
			// (get) Token: 0x06006188 RID: 24968 RVA: 0x001689B1 File Offset: 0x00166BB1
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider DocumentRange
			{
				get
				{
					if (!this.IsOwnerTextBoxBaseDestroyed())
					{
						return new UiaTextRange(new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject), this, 0, this.TextLength);
					}
					return null;
				}
			}

			// Token: 0x170014EA RID: 5354
			// (get) Token: 0x06006189 RID: 24969 RVA: 0x00013062 File Offset: 0x00011262
			public override UnsafeNativeMethods.UiaCore.SupportedTextSelection SupportedTextSelection
			{
				get
				{
					return UnsafeNativeMethods.UiaCore.SupportedTextSelection.Single;
				}
			}

			// Token: 0x0600618A RID: 24970 RVA: 0x001689DC File Offset: 0x00166BDC
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider GetCaretRange(out UnsafeNativeMethods.BOOL isActive)
			{
				isActive = UnsafeNativeMethods.BOOL.FALSE;
				if (!this.IsHandleCreated)
				{
					return null;
				}
				object propertyValue = this._owningTextBoxBase.AccessibilityObject.GetPropertyValue(30008);
				if (propertyValue is bool && (bool)propertyValue)
				{
					isActive = UnsafeNativeMethods.BOOL.TRUE;
				}
				InternalAccessibleObject enclosingElement = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UiaTextRange(enclosingElement, this, this._owningTextBoxBase.SelectionStart, this._owningTextBoxBase.SelectionStart);
			}

			// Token: 0x0600618B RID: 24971 RVA: 0x00168A4D File Offset: 0x00166C4D
			public override Point PointToScreen(Point pt)
			{
				if (!this.IsOwnerTextBoxBaseDestroyed())
				{
					return this._owningTextBoxBase.PointToScreen(pt);
				}
				return Point.Empty;
			}

			// Token: 0x0600618C RID: 24972 RVA: 0x00168A6C File Offset: 0x00166C6C
			public override UnsafeNativeMethods.UiaCore.ITextRangeProvider RangeFromAnnotation(UnsafeNativeMethods.IRawElementProviderSimple annotationElement)
			{
				if (this.IsOwnerTextBoxBaseDestroyed())
				{
					return null;
				}
				InternalAccessibleObject enclosingElement = new InternalAccessibleObject(this._owningTextBoxBase.AccessibilityObject);
				return new UiaTextRange(enclosingElement, this, 0, 0);
			}

			// Token: 0x170014EB RID: 5355
			// (get) Token: 0x0600618D RID: 24973 RVA: 0x00168A9D File Offset: 0x00166C9D
			public override Rectangle BoundingRectangle
			{
				get
				{
					if (this.IsHandleCreated)
					{
						return this.GetFormattingRectangle();
					}
					return Rectangle.Empty;
				}
			}

			// Token: 0x170014EC RID: 5356
			// (get) Token: 0x0600618E RID: 24974 RVA: 0x00168AB8 File Offset: 0x00166CB8
			public override int FirstVisibleLine
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return -1;
					}
					return (int)((long)this._owningTextBoxBase.SendMessage(206, 0, 0));
				}
			}

			// Token: 0x170014ED RID: 5357
			// (get) Token: 0x0600618F RID: 24975 RVA: 0x00168ADC File Offset: 0x00166CDC
			public override bool IsMultiline
			{
				get
				{
					return !this.IsOwnerTextBoxBaseDestroyed() && this._owningTextBoxBase.Multiline;
				}
			}

			// Token: 0x170014EE RID: 5358
			// (get) Token: 0x06006190 RID: 24976 RVA: 0x00168AF3 File Offset: 0x00166CF3
			public override bool IsReadingRTL
			{
				get
				{
					return this.IsHandleCreated && NativeMethods.HasFlag(this.WindowExStyle, 8192);
				}
			}

			// Token: 0x170014EF RID: 5359
			// (get) Token: 0x06006191 RID: 24977 RVA: 0x00168B0F File Offset: 0x00166D0F
			public override bool IsReadOnly
			{
				get
				{
					return this.IsOwnerTextBoxBaseDestroyed() || this._owningTextBoxBase.ReadOnly;
				}
			}

			// Token: 0x170014F0 RID: 5360
			// (get) Token: 0x06006192 RID: 24978 RVA: 0x00168B28 File Offset: 0x00166D28
			public override bool IsScrollable
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return false;
					}
					int value = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle), -16));
					return NativeMethods.HasFlag(value, 128) || NativeMethods.HasFlag(value, 64);
				}
			}

			// Token: 0x170014F1 RID: 5361
			// (get) Token: 0x06006193 RID: 24979 RVA: 0x00168B7A File Offset: 0x00166D7A
			public override int LinesCount
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return -1;
					}
					return (int)((long)this._owningTextBoxBase.SendMessage(186, 0, 0));
				}
			}

			// Token: 0x170014F2 RID: 5362
			// (get) Token: 0x06006194 RID: 24980 RVA: 0x00168BA0 File Offset: 0x00166DA0
			public override int LinesPerPage
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return -1;
					}
					Rectangle clientRectangle = this._owningTextBoxBase.ClientRectangle;
					if (clientRectangle.IsEmpty)
					{
						return 0;
					}
					if (!this._owningTextBoxBase.Multiline)
					{
						return 1;
					}
					int height = this._owningTextBoxBase.Font.Height;
					if (height == 0)
					{
						return 0;
					}
					return (int)Math.Ceiling((double)clientRectangle.Height / (double)height);
				}
			}

			// Token: 0x170014F3 RID: 5363
			// (get) Token: 0x06006195 RID: 24981 RVA: 0x00168C04 File Offset: 0x00166E04
			public override NativeMethods.LOGFONT Logfont
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return new NativeMethods.LOGFONT();
					}
					return NativeMethods.LOGFONT.FromFont(this._owningTextBoxBase.Font);
				}
			}

			// Token: 0x170014F4 RID: 5364
			// (get) Token: 0x06006196 RID: 24982 RVA: 0x00168C24 File Offset: 0x00166E24
			public override string Text
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return string.Empty;
					}
					return this._owningTextBoxBase.Text;
				}
			}

			// Token: 0x170014F5 RID: 5365
			// (get) Token: 0x06006197 RID: 24983 RVA: 0x00168C3F File Offset: 0x00166E3F
			public override int TextLength
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return -1;
					}
					return (int)((long)this._owningTextBoxBase.SendMessage(14, 0, 0));
				}
			}

			// Token: 0x170014F6 RID: 5366
			// (get) Token: 0x06006198 RID: 24984 RVA: 0x00168C60 File Offset: 0x00166E60
			public override int WindowExStyle
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return 0;
					}
					return base.GetWindowExStyle(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				}
			}

			// Token: 0x170014F7 RID: 5367
			// (get) Token: 0x06006199 RID: 24985 RVA: 0x00168C88 File Offset: 0x00166E88
			public override int WindowStyle
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return 0;
					}
					return base.GetWindowStyle(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				}
			}

			// Token: 0x170014F8 RID: 5368
			// (get) Token: 0x0600619A RID: 24986 RVA: 0x00168CB0 File Offset: 0x00166EB0
			public override int EditStyle
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return 0;
					}
					return base.GetEditStyle(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				}
			}

			// Token: 0x0600619B RID: 24987 RVA: 0x00168CD8 File Offset: 0x00166ED8
			public override int GetLineFromCharIndex(int charIndex)
			{
				if (!this.IsHandleCreated)
				{
					return -1;
				}
				return this._owningTextBoxBase.GetLineFromCharIndex(charIndex);
			}

			// Token: 0x0600619C RID: 24988 RVA: 0x00168CF0 File Offset: 0x00166EF0
			public override int GetLineIndex(int line)
			{
				if (!this.IsHandleCreated)
				{
					return -1;
				}
				return (int)((long)this._owningTextBoxBase.SendMessage(187, line, 0));
			}

			// Token: 0x0600619D RID: 24989 RVA: 0x00168D14 File Offset: 0x00166F14
			public override Point GetPositionFromChar(int charIndex)
			{
				if (!this.IsHandleCreated)
				{
					return Point.Empty;
				}
				return this._owningTextBoxBase.GetPositionFromCharIndex(charIndex);
			}

			// Token: 0x0600619E RID: 24990 RVA: 0x00168D30 File Offset: 0x00166F30
			public override Point GetPositionFromCharForUpperRightCorner(int startCharIndex, string text)
			{
				if (!this.IsHandleCreated || startCharIndex < 0 || startCharIndex >= text.Length)
				{
					return Point.Empty;
				}
				char c = text[startCharIndex];
				Point positionFromCharIndex;
				if (!char.IsControl(c))
				{
					positionFromCharIndex = this._owningTextBoxBase.GetPositionFromCharIndex(startCharIndex);
					Size size;
					if (this.GetTextExtentPoint32(c, out size))
					{
						positionFromCharIndex.X += size.Width;
					}
					return positionFromCharIndex;
				}
				if (c == '\t')
				{
					bool flag = startCharIndex < this.TextLength - 1 && this.GetLineFromCharIndex(startCharIndex + 1) == this.GetLineFromCharIndex(startCharIndex);
					return this._owningTextBoxBase.GetPositionFromCharIndex(flag ? (startCharIndex + 1) : startCharIndex);
				}
				positionFromCharIndex = this._owningTextBoxBase.GetPositionFromCharIndex(startCharIndex);
				if (c == '\r' || c == '\n')
				{
					positionFromCharIndex.X += 2;
				}
				return positionFromCharIndex;
			}

			// Token: 0x0600619F RID: 24991 RVA: 0x00168DFC File Offset: 0x00166FFC
			public override void GetVisibleRangePoints(out int visibleStart, out int visibleEnd)
			{
				visibleStart = 0;
				visibleEnd = 0;
				if (!this.IsHandleCreated || TextBoxBase.TextBoxBaseUiaTextProvider.<GetVisibleRangePoints>g__IsDegenerate|49_0(this._owningTextBoxBase.ClientRectangle))
				{
					return;
				}
				Rectangle rect = this.GetFormattingRectangle();
				if (TextBoxBase.TextBoxBaseUiaTextProvider.<GetVisibleRangePoints>g__IsDegenerate|49_0(rect))
				{
					return;
				}
				Point pt = new Point(rect.X + 1, rect.Y + 1);
				Point pt2 = new Point(rect.Right - 1, rect.Bottom - 1);
				visibleStart = this._owningTextBoxBase.GetCharIndexFromPosition(pt);
				visibleEnd = this._owningTextBoxBase.GetCharIndexFromPosition(pt2) + 1;
			}

			// Token: 0x060061A0 RID: 24992 RVA: 0x00168E8F File Offset: 0x0016708F
			public override bool LineScroll(int charactersHorizontal, int linesVertical)
			{
				return this.IsHandleCreated && this._owningTextBoxBase.SendMessage(182, charactersHorizontal, linesVertical) != IntPtr.Zero;
			}

			// Token: 0x060061A1 RID: 24993 RVA: 0x00168EB7 File Offset: 0x001670B7
			public override void SetSelection(int start, int end)
			{
				if (!this.IsHandleCreated)
				{
					return;
				}
				if (start < 0 || start > this.TextLength)
				{
					return;
				}
				if (end < 0 || end > this.TextLength)
				{
					return;
				}
				this._owningTextBoxBase.SendMessage(177, start, end);
			}

			// Token: 0x060061A2 RID: 24994 RVA: 0x00168EF4 File Offset: 0x001670F4
			private NativeMethods.RECT GetFormattingRectangle()
			{
				if (!this.IsHandleCreated)
				{
					return Rectangle.Empty;
				}
				NativeMethods.RECT result = default(NativeMethods.RECT);
				this._owningTextBoxBase.SendMessage(178, 0, ref result);
				return result;
			}

			// Token: 0x060061A3 RID: 24995 RVA: 0x00168F34 File Offset: 0x00167134
			private bool GetTextExtentPoint32(char item, out Size size)
			{
				size = Size.Empty;
				if (!this.IsHandleCreated)
				{
					return false;
				}
				IntNativeMethods.SIZE size2 = new IntNativeMethods.SIZE();
				IntPtr dc = UnsafeNativeMethods.GetDC(new HandleRef(this._owningTextBoxBase, this._owningTextBoxBase.Handle));
				bool result = IntUnsafeNativeMethods.GetTextExtentPoint32(new HandleRef(this._owningTextBoxBase, dc), item.ToString(), size2) != 0;
				size = size2.ToSize();
				return result;
			}

			// Token: 0x060061A4 RID: 24996 RVA: 0x00168FA2 File Offset: 0x001671A2
			[CompilerGenerated]
			internal static bool <GetVisibleRangePoints>g__IsDegenerate|49_0(Rectangle rect)
			{
				return rect.IsEmpty || rect.Width <= 0 || rect.Height <= 0;
			}

			// Token: 0x0400389D RID: 14493
			private TextBoxBase _owningTextBoxBase;
		}
	}
}
