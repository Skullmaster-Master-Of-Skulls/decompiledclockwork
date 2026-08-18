using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x0200010B RID: 267
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripTextBox : ToolStripControlHost
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x000110FC File Offset: 0x0000F2FC
		public ToolStripTextBox() : base(ToolStripTextBox.CreateControlInstance())
		{
			ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Control as ToolStripTextBox.ToolStripTextBoxControl;
			toolStripTextBoxControl.Owner = this;
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripTextBox.defaultMargin, 0);
				this.scaledDefaultDropDownMargin = DpiHelper.LogicalToDeviceUnits(ToolStripTextBox.defaultDropDownMargin, 0);
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00011166 File Offset: 0x0000F366
		public ToolStripTextBox(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011175 File Offset: 0x0000F375
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripTextBox(Control c) : base(c)
		{
			throw new NotSupportedException(SR.GetString("ToolStripMustSupplyItsOwnTextBox"));
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x000111A3 File Offset: 0x0000F3A3
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x000111AB File Offset: 0x0000F3AB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x000111B4 File Offset: 0x0000F3B4
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x000111BC File Offset: 0x0000F3BC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000111C5 File Offset: 0x0000F3C5
		protected internal override Padding DefaultMargin
		{
			get
			{
				if (base.IsOnDropDown)
				{
					return this.scaledDefaultDropDownMargin;
				}
				return this.scaledDefaultMargin;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000111DC File Offset: 0x0000F3DC
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000111E7 File Offset: 0x0000F3E7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TextBox TextBox
		{
			get
			{
				return base.Control as TextBox;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000111F4 File Offset: 0x0000F3F4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripTextBox.ToolStripTextBoxAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001120C File Offset: 0x0000F40C
		private static Control CreateControlInstance()
		{
			return new ToolStripTextBox.ToolStripTextBoxControl
			{
				BorderStyle = BorderStyle.Fixed3D,
				AutoSize = true
			};
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011230 File Offset: 0x0000F430
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return new Size(CommonProperties.GetSpecifiedBounds(this.TextBox).Width, this.TextBox.PreferredHeight);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011260 File Offset: 0x0000F460
		private void HandleAcceptsTabChanged(object sender, EventArgs e)
		{
			this.OnAcceptsTabChanged(e);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011269 File Offset: 0x0000F469
		private void HandleBorderStyleChanged(object sender, EventArgs e)
		{
			this.OnBorderStyleChanged(e);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011272 File Offset: 0x0000F472
		private void HandleHideSelectionChanged(object sender, EventArgs e)
		{
			this.OnHideSelectionChanged(e);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001127B File Offset: 0x0000F47B
		private void HandleModifiedChanged(object sender, EventArgs e)
		{
			this.OnModifiedChanged(e);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00011284 File Offset: 0x0000F484
		private void HandleMultilineChanged(object sender, EventArgs e)
		{
			this.OnMultilineChanged(e);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001128D File Offset: 0x0000F48D
		private void HandleReadOnlyChanged(object sender, EventArgs e)
		{
			this.OnReadOnlyChanged(e);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00011296 File Offset: 0x0000F496
		private void HandleTextBoxTextAlignChanged(object sender, EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventTextBoxTextAlignChanged, e);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000112A4 File Offset: 0x0000F4A4
		protected virtual void OnAcceptsTabChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventAcceptsTabChanged, e);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000112B2 File Offset: 0x0000F4B2
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventBorderStyleChanged, e);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000112C0 File Offset: 0x0000F4C0
		protected virtual void OnHideSelectionChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventHideSelectionChanged, e);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000112CE File Offset: 0x0000F4CE
		protected virtual void OnModifiedChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventModifiedChanged, e);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000112DC File Offset: 0x0000F4DC
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventMultilineChanged, e);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000112EA File Offset: 0x0000F4EA
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripTextBox.EventReadOnlyChanged, e);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000112F8 File Offset: 0x0000F4F8
		protected override void OnSubscribeControlEvents(Control control)
		{
			TextBox textBox = control as TextBox;
			if (textBox != null)
			{
				textBox.AcceptsTabChanged += this.HandleAcceptsTabChanged;
				textBox.BorderStyleChanged += this.HandleBorderStyleChanged;
				textBox.HideSelectionChanged += this.HandleHideSelectionChanged;
				textBox.ModifiedChanged += this.HandleModifiedChanged;
				textBox.MultilineChanged += this.HandleMultilineChanged;
				textBox.ReadOnlyChanged += this.HandleReadOnlyChanged;
				textBox.TextAlignChanged += this.HandleTextBoxTextAlignChanged;
			}
			base.OnSubscribeControlEvents(control);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00011394 File Offset: 0x0000F594
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			TextBox textBox = control as TextBox;
			if (textBox != null)
			{
				textBox.AcceptsTabChanged -= this.HandleAcceptsTabChanged;
				textBox.BorderStyleChanged -= this.HandleBorderStyleChanged;
				textBox.HideSelectionChanged -= this.HandleHideSelectionChanged;
				textBox.ModifiedChanged -= this.HandleModifiedChanged;
				textBox.MultilineChanged -= this.HandleMultilineChanged;
				textBox.ReadOnlyChanged -= this.HandleReadOnlyChanged;
				textBox.TextAlignChanged -= this.HandleTextBoxTextAlignChanged;
			}
			base.OnUnsubscribeControlEvents(control);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00011430 File Offset: 0x0000F630
		internal override bool ShouldSerializeFont()
		{
			return this.Font != ToolStripManager.DefaultFont;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00011442 File Offset: 0x0000F642
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001144F File Offset: 0x0000F64F
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsTabDescr")]
		public bool AcceptsTab
		{
			get
			{
				return this.TextBox.AcceptsTab;
			}
			set
			{
				this.TextBox.AcceptsTab = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001145D File Offset: 0x0000F65D
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0001146A File Offset: 0x0000F66A
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsReturnDescr")]
		public bool AcceptsReturn
		{
			get
			{
				return this.TextBox.AcceptsReturn;
			}
			set
			{
				this.TextBox.AcceptsReturn = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00011478 File Offset: 0x0000F678
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00011485 File Offset: 0x0000F685
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("TextBoxAutoCompleteCustomSourceDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				return this.TextBox.AutoCompleteCustomSource;
			}
			set
			{
				this.TextBox.AutoCompleteCustomSource = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00011493 File Offset: 0x0000F693
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000114A0 File Offset: 0x0000F6A0
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("TextBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.TextBox.AutoCompleteMode;
			}
			set
			{
				this.TextBox.AutoCompleteMode = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000114AE File Offset: 0x0000F6AE
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000114BB File Offset: 0x0000F6BB
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("TextBoxAutoCompleteSourceDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.TextBox.AutoCompleteSource;
			}
			set
			{
				this.TextBox.AutoCompleteSource = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000114C9 File Offset: 0x0000F6C9
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000114D6 File Offset: 0x0000F6D6
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("TextBoxBorderDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.TextBox.BorderStyle;
			}
			set
			{
				this.TextBox.BorderStyle = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000114E4 File Offset: 0x0000F6E4
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxCanUndoDescr")]
		public bool CanUndo
		{
			get
			{
				return this.TextBox.CanUndo;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000114F1 File Offset: 0x0000F6F1
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x000114FE File Offset: 0x0000F6FE
		[SRCategory("CatBehavior")]
		[DefaultValue(CharacterCasing.Normal)]
		[SRDescription("TextBoxCharacterCasingDescr")]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this.TextBox.CharacterCasing;
			}
			set
			{
				this.TextBox.CharacterCasing = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0001150C File Offset: 0x0000F70C
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x00011519 File Offset: 0x0000F719
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.TextBox.HideSelection;
			}
			set
			{
				this.TextBox.HideSelection = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00011527 File Offset: 0x0000F727
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00011534 File Offset: 0x0000F734
		[SRCategory("CatAppearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(true)]
		[SRDescription("TextBoxLinesDescr")]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string[] Lines
		{
			get
			{
				return this.TextBox.Lines;
			}
			set
			{
				this.TextBox.Lines = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00011542 File Offset: 0x0000F742
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0001154F File Offset: 0x0000F74F
		[SRCategory("CatBehavior")]
		[DefaultValue(32767)]
		[Localizable(true)]
		[SRDescription("TextBoxMaxLengthDescr")]
		public int MaxLength
		{
			get
			{
				return this.TextBox.MaxLength;
			}
			set
			{
				this.TextBox.MaxLength = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001155D File Offset: 0x0000F75D
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001156A File Offset: 0x0000F76A
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxModifiedDescr")]
		public bool Modified
		{
			get
			{
				return this.TextBox.Modified;
			}
			set
			{
				this.TextBox.Modified = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00011578 File Offset: 0x0000F778
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x00011585 File Offset: 0x0000F785
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("TextBoxMultilineDescr")]
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Multiline
		{
			get
			{
				return this.TextBox.Multiline;
			}
			set
			{
				this.TextBox.Multiline = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00011593 File Offset: 0x0000F793
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x000115A0 File Offset: 0x0000F7A0
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.TextBox.ReadOnly;
			}
			set
			{
				this.TextBox.ReadOnly = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000115AE File Offset: 0x0000F7AE
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000115BB File Offset: 0x0000F7BB
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectedTextDescr")]
		public string SelectedText
		{
			get
			{
				return this.TextBox.SelectedText;
			}
			set
			{
				this.TextBox.SelectedText = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000115C9 File Offset: 0x0000F7C9
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000115D6 File Offset: 0x0000F7D6
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionLengthDescr")]
		public int SelectionLength
		{
			get
			{
				return this.TextBox.SelectionLength;
			}
			set
			{
				this.TextBox.SelectionLength = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000115E4 File Offset: 0x0000F7E4
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x000115F1 File Offset: 0x0000F7F1
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TextBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				return this.TextBox.SelectionStart;
			}
			set
			{
				this.TextBox.SelectionStart = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000115FF File Offset: 0x0000F7FF
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0001160C File Offset: 0x0000F80C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TextBoxShortcutsEnabledDescr")]
		public bool ShortcutsEnabled
		{
			get
			{
				return this.TextBox.ShortcutsEnabled;
			}
			set
			{
				this.TextBox.ShortcutsEnabled = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001161A File Offset: 0x0000F81A
		[Browsable(false)]
		public int TextLength
		{
			get
			{
				return this.TextBox.TextLength;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00011627 File Offset: 0x0000F827
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x00011634 File Offset: 0x0000F834
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[SRDescription("TextBoxTextAlignDescr")]
		public HorizontalAlignment TextBoxTextAlign
		{
			get
			{
				return this.TextBox.TextAlign;
			}
			set
			{
				this.TextBox.TextAlign = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00011642 File Offset: 0x0000F842
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001164F File Offset: 0x0000F84F
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(true)]
		[SRDescription("TextBoxWordWrapDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool WordWrap
		{
			get
			{
				return this.TextBox.WordWrap;
			}
			set
			{
				this.TextBox.WordWrap = value;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004C4 RID: 1220 RVA: 0x0001165D File Offset: 0x0000F85D
		// (remove) Token: 0x060004C5 RID: 1221 RVA: 0x00011670 File Offset: 0x0000F870
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnAcceptsTabChangedDescr")]
		public event EventHandler AcceptsTabChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventAcceptsTabChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventAcceptsTabChanged, value);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004C6 RID: 1222 RVA: 0x00011683 File Offset: 0x0000F883
		// (remove) Token: 0x060004C7 RID: 1223 RVA: 0x00011696 File Offset: 0x0000F896
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnBorderStyleChangedDescr")]
		public event EventHandler BorderStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventBorderStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventBorderStyleChanged, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004C8 RID: 1224 RVA: 0x000116A9 File Offset: 0x0000F8A9
		// (remove) Token: 0x060004C9 RID: 1225 RVA: 0x000116BC File Offset: 0x0000F8BC
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnHideSelectionChangedDescr")]
		public event EventHandler HideSelectionChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventHideSelectionChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventHideSelectionChanged, value);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004CA RID: 1226 RVA: 0x000116CF File Offset: 0x0000F8CF
		// (remove) Token: 0x060004CB RID: 1227 RVA: 0x000116E2 File Offset: 0x0000F8E2
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnModifiedChangedDescr")]
		public event EventHandler ModifiedChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventModifiedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventModifiedChanged, value);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060004CC RID: 1228 RVA: 0x000116F5 File Offset: 0x0000F8F5
		// (remove) Token: 0x060004CD RID: 1229 RVA: 0x00011708 File Offset: 0x0000F908
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnMultilineChangedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler MultilineChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventMultilineChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventMultilineChanged, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060004CE RID: 1230 RVA: 0x0001171B File Offset: 0x0000F91B
		// (remove) Token: 0x060004CF RID: 1231 RVA: 0x0001172E File Offset: 0x0000F92E
		[SRCategory("CatPropertyChanged")]
		[SRDescription("TextBoxBaseOnReadOnlyChangedDescr")]
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventReadOnlyChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventReadOnlyChanged, value);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060004D0 RID: 1232 RVA: 0x00011741 File Offset: 0x0000F941
		// (remove) Token: 0x060004D1 RID: 1233 RVA: 0x00011754 File Offset: 0x0000F954
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ToolStripTextBoxTextBoxTextAlignChangedDescr")]
		public event EventHandler TextBoxTextAlignChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripTextBox.EventTextBoxTextAlignChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripTextBox.EventTextBoxTextAlignChanged, value);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011767 File Offset: 0x0000F967
		public void AppendText(string text)
		{
			this.TextBox.AppendText(text);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00011775 File Offset: 0x0000F975
		public void Clear()
		{
			this.TextBox.Clear();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00011782 File Offset: 0x0000F982
		public void ClearUndo()
		{
			this.TextBox.ClearUndo();
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001178F File Offset: 0x0000F98F
		public void Copy()
		{
			this.TextBox.Copy();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001178F File Offset: 0x0000F98F
		public void Cut()
		{
			this.TextBox.Copy();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001179C File Offset: 0x0000F99C
		public void DeselectAll()
		{
			this.TextBox.DeselectAll();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000117A9 File Offset: 0x0000F9A9
		public char GetCharFromPosition(Point pt)
		{
			return this.TextBox.GetCharFromPosition(pt);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000117B7 File Offset: 0x0000F9B7
		public int GetCharIndexFromPosition(Point pt)
		{
			return this.TextBox.GetCharIndexFromPosition(pt);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000117C5 File Offset: 0x0000F9C5
		public int GetFirstCharIndexFromLine(int lineNumber)
		{
			return this.TextBox.GetFirstCharIndexFromLine(lineNumber);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000117D3 File Offset: 0x0000F9D3
		public int GetFirstCharIndexOfCurrentLine()
		{
			return this.TextBox.GetFirstCharIndexOfCurrentLine();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000117E0 File Offset: 0x0000F9E0
		public int GetLineFromCharIndex(int index)
		{
			return this.TextBox.GetLineFromCharIndex(index);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000117EE File Offset: 0x0000F9EE
		public Point GetPositionFromCharIndex(int index)
		{
			return this.TextBox.GetPositionFromCharIndex(index);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000117FC File Offset: 0x0000F9FC
		public void Paste()
		{
			this.TextBox.Paste();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00011809 File Offset: 0x0000FA09
		public void ScrollToCaret()
		{
			this.TextBox.ScrollToCaret();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00011816 File Offset: 0x0000FA16
		public void Select(int start, int length)
		{
			this.TextBox.Select(start, length);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00011825 File Offset: 0x0000FA25
		public void SelectAll()
		{
			this.TextBox.SelectAll();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00011832 File Offset: 0x0000FA32
		public void Undo()
		{
			this.TextBox.Undo();
		}

		// Token: 0x040004AC RID: 1196
		internal static readonly object EventTextBoxTextAlignChanged = new object();

		// Token: 0x040004AD RID: 1197
		internal static readonly object EventAcceptsTabChanged = new object();

		// Token: 0x040004AE RID: 1198
		internal static readonly object EventBorderStyleChanged = new object();

		// Token: 0x040004AF RID: 1199
		internal static readonly object EventHideSelectionChanged = new object();

		// Token: 0x040004B0 RID: 1200
		internal static readonly object EventReadOnlyChanged = new object();

		// Token: 0x040004B1 RID: 1201
		internal static readonly object EventMultilineChanged = new object();

		// Token: 0x040004B2 RID: 1202
		internal static readonly object EventModifiedChanged = new object();

		// Token: 0x040004B3 RID: 1203
		private static readonly Padding defaultMargin = new Padding(1, 0, 1, 0);

		// Token: 0x040004B4 RID: 1204
		private static readonly Padding defaultDropDownMargin = new Padding(1);

		// Token: 0x040004B5 RID: 1205
		private Padding scaledDefaultMargin = ToolStripTextBox.defaultMargin;

		// Token: 0x040004B6 RID: 1206
		private Padding scaledDefaultDropDownMargin = ToolStripTextBox.defaultDropDownMargin;

		// Token: 0x02000554 RID: 1364
		private class ToolStripTextBoxControlAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
		{
			// Token: 0x06005591 RID: 21905 RVA: 0x0001101C File Offset: 0x0000F21C
			public ToolStripTextBoxControlAccessibleObjectLevel5(ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl) : base(toolStripTextBoxControl)
			{
			}

			// Token: 0x06005592 RID: 21906 RVA: 0x00166BF3 File Offset: 0x00164DF3
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30008)
				{
					return (this.State & AccessibleStates.Focused) == AccessibleStates.Focused;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06005593 RID: 21907 RVA: 0x000110D4 File Offset: 0x0000F2D4
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && (patternId == 10002 || base.IsPatternSupported(patternId));
			}

			// Token: 0x17001486 RID: 5254
			// (get) Token: 0x06005594 RID: 21908 RVA: 0x00166C18 File Offset: 0x00164E18
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.Owner.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x06005595 RID: 21909 RVA: 0x00166C4C File Offset: 0x00164E4C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.AccessibilityObject.FragmentNavigate(direction);
					}
				}
				return base.FragmentNavigate(direction);
			}
		}

		// Token: 0x02000555 RID: 1365
		[ComVisible(true)]
		internal class ToolStripTextBoxAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06005596 RID: 21910 RVA: 0x00166C85 File Offset: 0x00164E85
			public ToolStripTextBoxAccessibleObject(ToolStripTextBox ownerItem) : base(ownerItem)
			{
			}

			// Token: 0x17001487 RID: 5255
			// (get) Token: 0x06005597 RID: 21911 RVA: 0x00166C90 File Offset: 0x00164E90
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleRole.Text;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Text;
				}
			}

			// Token: 0x06005598 RID: 21912 RVA: 0x00166CBC File Offset: 0x00164EBC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (!base.IsOwnerItemCleared())
				{
					ToolStripTextBox toolStripTextBox = base.Owner as ToolStripTextBox;
					if (toolStripTextBox != null)
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild || direction == UnsafeNativeMethods.NavigateDirection.LastChild)
						{
							return toolStripTextBox.TextBox.AccessibilityObject;
						}
						return base.FragmentNavigate(direction);
					}
				}
				return null;
			}
		}

		// Token: 0x02000556 RID: 1366
		private class ToolStripTextBoxControl : TextBox
		{
			// Token: 0x06005599 RID: 21913 RVA: 0x00166CFD File Offset: 0x00164EFD
			public ToolStripTextBoxControl()
			{
				this.Font = ToolStripManager.DefaultFont;
				this.isFontSet = false;
			}

			// Token: 0x17001488 RID: 5256
			// (get) Token: 0x0600559A RID: 21914 RVA: 0x00166D20 File Offset: 0x00164F20
			private NativeMethods.RECT AbsoluteClientRECT
			{
				get
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					CreateParams createParams = this.CreateParams;
					base.AdjustWindowRectEx(ref rect, createParams.Style, this.HasMenu, createParams.ExStyle);
					int num = -rect.left;
					int num2 = -rect.top;
					UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
					rect.left += num;
					rect.right += num;
					rect.top += num2;
					rect.bottom += num2;
					return rect;
				}
			}

			// Token: 0x17001489 RID: 5257
			// (get) Token: 0x0600559B RID: 21915 RVA: 0x00166DAC File Offset: 0x00164FAC
			private Rectangle AbsoluteClientRectangle
			{
				get
				{
					NativeMethods.RECT absoluteClientRECT = this.AbsoluteClientRECT;
					return Rectangle.FromLTRB(absoluteClientRECT.top, absoluteClientRECT.top, absoluteClientRECT.right, absoluteClientRECT.bottom);
				}
			}

			// Token: 0x1700148A RID: 5258
			// (get) Token: 0x0600559C RID: 21916 RVA: 0x00166DE0 File Offset: 0x00164FE0
			private ProfessionalColorTable ColorTable
			{
				get
				{
					if (this.Owner != null)
					{
						ToolStripProfessionalRenderer toolStripProfessionalRenderer = this.Owner.Renderer as ToolStripProfessionalRenderer;
						if (toolStripProfessionalRenderer != null)
						{
							return toolStripProfessionalRenderer.ColorTable;
						}
					}
					return ProfessionalColors.ColorTable;
				}
			}

			// Token: 0x1700148B RID: 5259
			// (get) Token: 0x0600559D RID: 21917 RVA: 0x00166E15 File Offset: 0x00165015
			private bool IsPopupTextBox
			{
				get
				{
					return base.BorderStyle == BorderStyle.Fixed3D && this.Owner != null && this.Owner.Renderer is ToolStripProfessionalRenderer;
				}
			}

			// Token: 0x1700148C RID: 5260
			// (get) Token: 0x0600559E RID: 21918 RVA: 0x00166E3F File Offset: 0x0016503F
			// (set) Token: 0x0600559F RID: 21919 RVA: 0x00166E47 File Offset: 0x00165047
			internal bool MouseIsOver
			{
				get
				{
					return this.mouseIsOver;
				}
				set
				{
					if (this.mouseIsOver != value)
					{
						this.mouseIsOver = value;
						if (!this.Focused)
						{
							this.InvalidateNonClient();
						}
					}
				}
			}

			// Token: 0x1700148D RID: 5261
			// (get) Token: 0x060055A0 RID: 21920 RVA: 0x0001A272 File Offset: 0x00018472
			// (set) Token: 0x060055A1 RID: 21921 RVA: 0x00166E67 File Offset: 0x00165067
			public override Font Font
			{
				get
				{
					return base.Font;
				}
				set
				{
					base.Font = value;
					this.isFontSet = this.ShouldSerializeFont();
				}
			}

			// Token: 0x1700148E RID: 5262
			// (get) Token: 0x060055A2 RID: 21922 RVA: 0x00166E7C File Offset: 0x0016507C
			// (set) Token: 0x060055A3 RID: 21923 RVA: 0x00166E84 File Offset: 0x00165084
			public ToolStripTextBox Owner
			{
				get
				{
					return this.ownerItem;
				}
				set
				{
					this.ownerItem = value;
				}
			}

			// Token: 0x1700148F RID: 5263
			// (get) Token: 0x060055A4 RID: 21924 RVA: 0x000A8615 File Offset: 0x000A6815
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x060055A5 RID: 21925 RVA: 0x00166E90 File Offset: 0x00165090
			private void InvalidateNonClient()
			{
				if (!this.IsPopupTextBox)
				{
					return;
				}
				NativeMethods.RECT absoluteClientRECT = this.AbsoluteClientRECT;
				HandleRef handleRef = NativeMethods.NullHandleRef;
				HandleRef handleRef2 = NativeMethods.NullHandleRef;
				HandleRef handleRef3 = NativeMethods.NullHandleRef;
				try
				{
					handleRef3 = new HandleRef(this, SafeNativeMethods.CreateRectRgn(0, 0, base.Width, base.Height));
					handleRef2 = new HandleRef(this, SafeNativeMethods.CreateRectRgn(absoluteClientRECT.left, absoluteClientRECT.top, absoluteClientRECT.right, absoluteClientRECT.bottom));
					handleRef = new HandleRef(this, SafeNativeMethods.CreateRectRgn(0, 0, 0, 0));
					SafeNativeMethods.CombineRgn(handleRef, handleRef3, handleRef2, 3);
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), ref rect, handleRef, 1797);
				}
				finally
				{
					try
					{
						if (handleRef.Handle != IntPtr.Zero)
						{
							SafeNativeMethods.DeleteObject(handleRef);
						}
					}
					finally
					{
						try
						{
							if (handleRef2.Handle != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(handleRef2);
							}
						}
						finally
						{
							if (handleRef3.Handle != IntPtr.Zero)
							{
								SafeNativeMethods.DeleteObject(handleRef3);
							}
						}
					}
				}
			}

			// Token: 0x060055A6 RID: 21926 RVA: 0x00166FBC File Offset: 0x001651BC
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				this.InvalidateNonClient();
			}

			// Token: 0x060055A7 RID: 21927 RVA: 0x00166FCB File Offset: 0x001651CB
			protected override void OnLostFocus(EventArgs e)
			{
				base.OnLostFocus(e);
				this.InvalidateNonClient();
			}

			// Token: 0x060055A8 RID: 21928 RVA: 0x00166FDA File Offset: 0x001651DA
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				this.MouseIsOver = true;
			}

			// Token: 0x060055A9 RID: 21929 RVA: 0x00166FEA File Offset: 0x001651EA
			protected override void OnMouseLeave(EventArgs e)
			{
				base.OnMouseLeave(e);
				this.MouseIsOver = false;
			}

			// Token: 0x060055AA RID: 21930 RVA: 0x00166FFC File Offset: 0x001651FC
			private void HookStaticEvents(bool hook)
			{
				if (hook)
				{
					if (this.alreadyHooked)
					{
						return;
					}
					try
					{
						SystemEvents.UserPreferenceChanged += this.OnUserPreferenceChanged;
						return;
					}
					finally
					{
						this.alreadyHooked = true;
					}
				}
				if (this.alreadyHooked)
				{
					try
					{
						SystemEvents.UserPreferenceChanged -= this.OnUserPreferenceChanged;
					}
					finally
					{
						this.alreadyHooked = false;
					}
				}
			}

			// Token: 0x060055AB RID: 21931 RVA: 0x00167070 File Offset: 0x00165270
			private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
			{
				if (e.Category == UserPreferenceCategory.Window && !this.isFontSet)
				{
					this.Font = ToolStripManager.DefaultFont;
				}
			}

			// Token: 0x060055AC RID: 21932 RVA: 0x0016708F File Offset: 0x0016528F
			protected override void OnVisibleChanged(EventArgs e)
			{
				base.OnVisibleChanged(e);
				if (!base.Disposing && !base.IsDisposed)
				{
					this.HookStaticEvents(base.Visible);
				}
			}

			// Token: 0x060055AD RID: 21933 RVA: 0x001670B4 File Offset: 0x001652B4
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level5)
				{
					return new ToolStripTextBox.ToolStripTextBoxControlAccessibleObjectLevel5(this);
				}
				if (AccessibilityImprovements.Level3)
				{
					return new ToolStripTextBox.ToolStripTextBoxControlAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x060055AE RID: 21934 RVA: 0x001670D8 File Offset: 0x001652D8
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.HookStaticEvents(false);
				}
				base.Dispose(disposing);
			}

			// Token: 0x060055AF RID: 21935 RVA: 0x001670EC File Offset: 0x001652EC
			private void WmNCPaint(ref Message m)
			{
				if (!this.IsPopupTextBox)
				{
					base.WndProc(ref m);
					return;
				}
				HandleRef hDC = new HandleRef(this, UnsafeNativeMethods.GetWindowDC(new HandleRef(this, m.HWnd)));
				if (hDC.Handle == IntPtr.Zero)
				{
					throw new Win32Exception();
				}
				try
				{
					Color color = (this.MouseIsOver || this.Focused) ? this.ColorTable.TextBoxBorder : this.BackColor;
					Color color2 = this.BackColor;
					if (!base.Enabled)
					{
						color = SystemColors.ControlDark;
						color2 = SystemColors.Control;
					}
					using (Graphics graphics = Graphics.FromHdcInternal(hDC.Handle))
					{
						Rectangle absoluteClientRectangle = this.AbsoluteClientRectangle;
						using (Brush brush = new SolidBrush(color2))
						{
							graphics.FillRectangle(brush, 0, 0, base.Width, absoluteClientRectangle.Top);
							graphics.FillRectangle(brush, 0, 0, absoluteClientRectangle.Left, base.Height);
							graphics.FillRectangle(brush, 0, absoluteClientRectangle.Bottom, base.Width, base.Height - absoluteClientRectangle.Height);
							graphics.FillRectangle(brush, absoluteClientRectangle.Right, 0, base.Width - absoluteClientRectangle.Right, base.Height);
						}
						using (Pen pen = new Pen(color))
						{
							graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
						}
					}
				}
				finally
				{
					UnsafeNativeMethods.ReleaseDC(new HandleRef(this, base.Handle), hDC);
				}
				m.Result = IntPtr.Zero;
			}

			// Token: 0x060055B0 RID: 21936 RVA: 0x001672DC File Offset: 0x001654DC
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 133)
				{
					this.WmNCPaint(ref m);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x0400382E RID: 14382
			private bool mouseIsOver;

			// Token: 0x0400382F RID: 14383
			private ToolStripTextBox ownerItem;

			// Token: 0x04003830 RID: 14384
			private bool isFontSet = true;

			// Token: 0x04003831 RID: 14385
			private bool alreadyHooked;
		}

		// Token: 0x02000557 RID: 1367
		private class ToolStripTextBoxControlAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060055B1 RID: 21937 RVA: 0x0009B963 File Offset: 0x00099B63
			public ToolStripTextBoxControlAccessibleObject(ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl) : base(toolStripTextBoxControl)
			{
			}

			// Token: 0x17001490 RID: 5264
			// (get) Token: 0x060055B2 RID: 21938 RVA: 0x001672FC File Offset: 0x001654FC
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.Owner.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x060055B3 RID: 21939 RVA: 0x00167330 File Offset: 0x00165530
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ToolStripTextBox.ToolStripTextBoxControl toolStripTextBoxControl = base.Owner as ToolStripTextBox.ToolStripTextBoxControl;
					if (toolStripTextBoxControl != null)
					{
						return toolStripTextBoxControl.Owner.AccessibilityObject.FragmentNavigate(direction);
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x060055B4 RID: 21940 RVA: 0x0016736C File Offset: 0x0016556C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50004;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID != 30008)
				{
					return base.GetPropertyValue(propertyID);
				}
				return (this.State & AccessibleStates.Focused) == AccessibleStates.Focused;
			}

			// Token: 0x060055B5 RID: 21941 RVA: 0x001673BD File Offset: 0x001655BD
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && (patternId == 10002 || patternId == 10018 || base.IsPatternSupported(patternId));
			}
		}
	}
}
