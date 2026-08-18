using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	// Token: 0x020003B7 RID: 951
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	[DefaultProperty("Items")]
	public class ToolStripComboBox : ToolStripControlHost
	{
		// Token: 0x06003F11 RID: 16145 RVA: 0x001113EC File Offset: 0x0010F5EC
		public ToolStripComboBox() : base(ToolStripComboBox.CreateControlInstance())
		{
			ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = base.Control as ToolStripComboBox.ToolStripComboBoxControl;
			toolStripComboBoxControl.Owner = this;
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledPadding = DpiHelper.LogicalToDeviceUnits(ToolStripComboBox.padding, 0);
				this.scaledDropDownPadding = DpiHelper.LogicalToDeviceUnits(ToolStripComboBox.dropDownPadding, 0);
			}
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x00111456 File Offset: 0x0010F656
		public ToolStripComboBox(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x00111465 File Offset: 0x0010F665
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToolStripComboBox(Control c) : base(c)
		{
			throw new NotSupportedException(SR.GetString("ToolStripMustSupplyItsOwnComboBox"));
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x00111493 File Offset: 0x0010F693
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripComboBox.ToolStripComboBoxAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x001114AC File Offset: 0x0010F6AC
		private static Control CreateControlInstance()
		{
			return new ToolStripComboBox.ToolStripComboBoxControl
			{
				FlatStyle = FlatStyle.Popup,
				Font = ToolStripManager.DefaultFont
			};
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x001114D2 File Offset: 0x0010F6D2
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x001114DF File Offset: 0x0010F6DF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxAutoCompleteCustomSourceDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				return this.ComboBox.AutoCompleteCustomSource;
			}
			set
			{
				this.ComboBox.AutoCompleteCustomSource = value;
			}
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x001114ED File Offset: 0x0010F6ED
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x001114FA File Offset: 0x0010F6FA
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("ComboBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.ComboBox.AutoCompleteMode;
			}
			set
			{
				this.ComboBox.AutoCompleteMode = value;
			}
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x00111508 File Offset: 0x0010F708
		// (set) Token: 0x06003F1B RID: 16155 RVA: 0x00111515 File Offset: 0x0010F715
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("ComboBoxAutoCompleteSourceDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.ComboBox.AutoCompleteSource;
			}
			set
			{
				this.ComboBox.AutoCompleteSource = value;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000111A3 File Offset: 0x0000F3A3
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x000111AB File Offset: 0x0000F3AB
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

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000111B4 File Offset: 0x0000F3B4
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x000111BC File Offset: 0x0000F3BC
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

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x00111523 File Offset: 0x0010F723
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ComboBox ComboBox
		{
			get
			{
				return base.Control as ComboBox;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x000111DC File Offset: 0x0000F3DC
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x00111530 File Offset: 0x0010F730
		protected internal override Padding DefaultMargin
		{
			get
			{
				if (base.IsOnDropDown)
				{
					return this.scaledDropDownPadding;
				}
				return this.scaledPadding;
			}
		}

		// Token: 0x14000307 RID: 775
		// (add) Token: 0x06003F23 RID: 16163 RVA: 0x00111547 File Offset: 0x0010F747
		// (remove) Token: 0x06003F24 RID: 16164 RVA: 0x00111550 File Offset: 0x0010F750
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		// Token: 0x14000308 RID: 776
		// (add) Token: 0x06003F25 RID: 16165 RVA: 0x00111559 File Offset: 0x0010F759
		// (remove) Token: 0x06003F26 RID: 16166 RVA: 0x0011156C File Offset: 0x0010F76C
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownDescr")]
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventDropDown, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventDropDown, value);
			}
		}

		// Token: 0x14000309 RID: 777
		// (add) Token: 0x06003F27 RID: 16167 RVA: 0x0011157F File Offset: 0x0010F77F
		// (remove) Token: 0x06003F28 RID: 16168 RVA: 0x00111592 File Offset: 0x0010F792
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownClosedDescr")]
		public event EventHandler DropDownClosed
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventDropDownClosed, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventDropDownClosed, value);
			}
		}

		// Token: 0x1400030A RID: 778
		// (add) Token: 0x06003F29 RID: 16169 RVA: 0x001115A5 File Offset: 0x0010F7A5
		// (remove) Token: 0x06003F2A RID: 16170 RVA: 0x001115B8 File Offset: 0x0010F7B8
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownStyleChangedDescr")]
		public event EventHandler DropDownStyleChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventDropDownStyleChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventDropDownStyleChanged, value);
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x001115CB File Offset: 0x0010F7CB
		// (set) Token: 0x06003F2C RID: 16172 RVA: 0x001115D8 File Offset: 0x0010F7D8
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownHeightDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(106)]
		public int DropDownHeight
		{
			get
			{
				return this.ComboBox.DropDownHeight;
			}
			set
			{
				this.ComboBox.DropDownHeight = value;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x001115E6 File Offset: 0x0010F7E6
		// (set) Token: 0x06003F2E RID: 16174 RVA: 0x001115F3 File Offset: 0x0010F7F3
		[SRCategory("CatAppearance")]
		[DefaultValue(ComboBoxStyle.DropDown)]
		[SRDescription("ComboBoxStyleDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				return this.ComboBox.DropDownStyle;
			}
			set
			{
				this.ComboBox.DropDownStyle = value;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003F2F RID: 16175 RVA: 0x00111601 File Offset: 0x0010F801
		// (set) Token: 0x06003F30 RID: 16176 RVA: 0x0011160E File Offset: 0x0010F80E
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownWidthDescr")]
		public int DropDownWidth
		{
			get
			{
				return this.ComboBox.DropDownWidth;
			}
			set
			{
				this.ComboBox.DropDownWidth = value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x0011161C File Offset: 0x0010F81C
		// (set) Token: 0x06003F32 RID: 16178 RVA: 0x00111629 File Offset: 0x0010F829
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxDroppedDownDescr")]
		public bool DroppedDown
		{
			get
			{
				return this.ComboBox.DroppedDown;
			}
			set
			{
				this.ComboBox.DroppedDown = value;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x00111637 File Offset: 0x0010F837
		// (set) Token: 0x06003F34 RID: 16180 RVA: 0x00111644 File Offset: 0x0010F844
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Popup)]
		[Localizable(true)]
		[SRDescription("ComboBoxFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.ComboBox.FlatStyle;
			}
			set
			{
				this.ComboBox.FlatStyle = value;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x00111652 File Offset: 0x0010F852
		// (set) Token: 0x06003F36 RID: 16182 RVA: 0x0011165F File Offset: 0x0010F85F
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ComboBoxIntegralHeightDescr")]
		public bool IntegralHeight
		{
			get
			{
				return this.ComboBox.IntegralHeight;
			}
			set
			{
				this.ComboBox.IntegralHeight = value;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x0011166D File Offset: 0x0010F86D
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				return this.ComboBox.Items;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x0011167A File Offset: 0x0010F87A
		// (set) Token: 0x06003F39 RID: 16185 RVA: 0x00111687 File Offset: 0x0010F887
		[SRCategory("CatBehavior")]
		[DefaultValue(8)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxDropDownItemsDescr")]
		public int MaxDropDownItems
		{
			get
			{
				return this.ComboBox.MaxDropDownItems;
			}
			set
			{
				this.ComboBox.MaxDropDownItems = value;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x00111695 File Offset: 0x0010F895
		// (set) Token: 0x06003F3B RID: 16187 RVA: 0x001116A2 File Offset: 0x0010F8A2
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxLengthDescr")]
		public int MaxLength
		{
			get
			{
				return this.ComboBox.MaxLength;
			}
			set
			{
				this.ComboBox.MaxLength = value;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x001116B0 File Offset: 0x0010F8B0
		// (set) Token: 0x06003F3D RID: 16189 RVA: 0x001116BD File Offset: 0x0010F8BD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedIndexDescr")]
		public int SelectedIndex
		{
			get
			{
				return this.ComboBox.SelectedIndex;
			}
			set
			{
				this.ComboBox.SelectedIndex = value;
			}
		}

		// Token: 0x1400030B RID: 779
		// (add) Token: 0x06003F3E RID: 16190 RVA: 0x001116CB File Offset: 0x0010F8CB
		// (remove) Token: 0x06003F3F RID: 16191 RVA: 0x001116DE File Offset: 0x0010F8DE
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06003F40 RID: 16192 RVA: 0x001116F1 File Offset: 0x0010F8F1
		// (set) Token: 0x06003F41 RID: 16193 RVA: 0x001116FE File Offset: 0x0010F8FE
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				return this.ComboBox.SelectedItem;
			}
			set
			{
				this.ComboBox.SelectedItem = value;
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003F42 RID: 16194 RVA: 0x0011170C File Offset: 0x0010F90C
		// (set) Token: 0x06003F43 RID: 16195 RVA: 0x00111719 File Offset: 0x0010F919
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedTextDescr")]
		public string SelectedText
		{
			get
			{
				return this.ComboBox.SelectedText;
			}
			set
			{
				this.ComboBox.SelectedText = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003F44 RID: 16196 RVA: 0x00111727 File Offset: 0x0010F927
		// (set) Token: 0x06003F45 RID: 16197 RVA: 0x00111734 File Offset: 0x0010F934
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionLengthDescr")]
		public int SelectionLength
		{
			get
			{
				return this.ComboBox.SelectionLength;
			}
			set
			{
				this.ComboBox.SelectionLength = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06003F46 RID: 16198 RVA: 0x00111742 File Offset: 0x0010F942
		// (set) Token: 0x06003F47 RID: 16199 RVA: 0x0011174F File Offset: 0x0010F94F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				return this.ComboBox.SelectionStart;
			}
			set
			{
				this.ComboBox.SelectionStart = value;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06003F48 RID: 16200 RVA: 0x0011175D File Offset: 0x0010F95D
		// (set) Token: 0x06003F49 RID: 16201 RVA: 0x0011176A File Offset: 0x0010F96A
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ComboBoxSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.ComboBox.Sorted;
			}
			set
			{
				this.ComboBox.Sorted = value;
			}
		}

		// Token: 0x1400030C RID: 780
		// (add) Token: 0x06003F4A RID: 16202 RVA: 0x00111778 File Offset: 0x0010F978
		// (remove) Token: 0x06003F4B RID: 16203 RVA: 0x0011178B File Offset: 0x0010F98B
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnTextUpdateDescr")]
		public event EventHandler TextUpdate
		{
			add
			{
				base.Events.AddHandler(ToolStripComboBox.EventTextUpdate, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripComboBox.EventTextUpdate, value);
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0011179E File Offset: 0x0010F99E
		public void BeginUpdate()
		{
			this.ComboBox.BeginUpdate();
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x001117AB File Offset: 0x0010F9AB
		public void EndUpdate()
		{
			this.ComboBox.EndUpdate();
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x001117B8 File Offset: 0x0010F9B8
		public int FindString(string s)
		{
			return this.ComboBox.FindString(s);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x001117C6 File Offset: 0x0010F9C6
		public int FindString(string s, int startIndex)
		{
			return this.ComboBox.FindString(s, startIndex);
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x001117D5 File Offset: 0x0010F9D5
		public int FindStringExact(string s)
		{
			return this.ComboBox.FindStringExact(s);
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x001117E3 File Offset: 0x0010F9E3
		public int FindStringExact(string s, int startIndex)
		{
			return this.ComboBox.FindStringExact(s, startIndex);
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x001117F2 File Offset: 0x0010F9F2
		public int GetItemHeight(int index)
		{
			return this.ComboBox.GetItemHeight(index);
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x00111800 File Offset: 0x0010FA00
		public void Select(int start, int length)
		{
			this.ComboBox.Select(start, length);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x0011180F File Offset: 0x0010FA0F
		public void SelectAll()
		{
			this.ComboBox.SelectAll();
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0011181C File Offset: 0x0010FA1C
		public override Size GetPreferredSize(Size constrainingSize)
		{
			Size preferredSize = base.GetPreferredSize(constrainingSize);
			preferredSize.Width = Math.Max(preferredSize.Width, 75);
			return preferredSize;
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x00111847 File Offset: 0x0010FA47
		private void HandleDropDown(object sender, EventArgs e)
		{
			this.OnDropDown(e);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00111850 File Offset: 0x0010FA50
		private void HandleDropDownClosed(object sender, EventArgs e)
		{
			this.OnDropDownClosed(e);
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x00111859 File Offset: 0x0010FA59
		private void HandleDropDownStyleChanged(object sender, EventArgs e)
		{
			this.OnDropDownStyleChanged(e);
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x00111862 File Offset: 0x0010FA62
		private void HandleSelectedIndexChanged(object sender, EventArgs e)
		{
			this.OnSelectedIndexChanged(e);
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0011186B File Offset: 0x0010FA6B
		private void HandleSelectionChangeCommitted(object sender, EventArgs e)
		{
			this.OnSelectionChangeCommitted(e);
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x00111874 File Offset: 0x0010FA74
		private void HandleTextUpdate(object sender, EventArgs e)
		{
			this.OnTextUpdate(e);
		}

		// Token: 0x06003F5C RID: 16220 RVA: 0x0011187D File Offset: 0x0010FA7D
		protected virtual void OnDropDown(EventArgs e)
		{
			if (base.ParentInternal != null)
			{
				Application.ThreadContext.FromCurrent().RemoveMessageFilter(base.ParentInternal.RestoreFocusFilter);
				ToolStripManager.ModalMenuFilter.SuspendMenuMode();
			}
			base.RaiseEvent(ToolStripComboBox.EventDropDown, e);
		}

		// Token: 0x06003F5D RID: 16221 RVA: 0x001118AD File Offset: 0x0010FAAD
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			if (base.ParentInternal != null)
			{
				Application.ThreadContext.FromCurrent().RemoveMessageFilter(base.ParentInternal.RestoreFocusFilter);
				ToolStripManager.ModalMenuFilter.ResumeMenuMode();
			}
			base.RaiseEvent(ToolStripComboBox.EventDropDownClosed, e);
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x001118DD File Offset: 0x0010FADD
		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventDropDownStyleChanged, e);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x001118EB File Offset: 0x0010FAEB
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventSelectedIndexChanged, e);
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x001118F9 File Offset: 0x0010FAF9
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventSelectionChangeCommitted, e);
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x00111907 File Offset: 0x0010FB07
		protected virtual void OnTextUpdate(EventArgs e)
		{
			base.RaiseEvent(ToolStripComboBox.EventTextUpdate, e);
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x00111918 File Offset: 0x0010FB18
		protected override void OnSubscribeControlEvents(Control control)
		{
			ComboBox comboBox = control as ComboBox;
			if (comboBox != null)
			{
				comboBox.DropDown += this.HandleDropDown;
				comboBox.DropDownClosed += this.HandleDropDownClosed;
				comboBox.DropDownStyleChanged += this.HandleDropDownStyleChanged;
				comboBox.SelectedIndexChanged += this.HandleSelectedIndexChanged;
				comboBox.SelectionChangeCommitted += this.HandleSelectionChangeCommitted;
				comboBox.TextUpdate += this.HandleTextUpdate;
			}
			base.OnSubscribeControlEvents(control);
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x001119A4 File Offset: 0x0010FBA4
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			ComboBox comboBox = control as ComboBox;
			if (comboBox != null)
			{
				comboBox.DropDown -= this.HandleDropDown;
				comboBox.DropDownClosed -= this.HandleDropDownClosed;
				comboBox.DropDownStyleChanged -= this.HandleDropDownStyleChanged;
				comboBox.SelectedIndexChanged -= this.HandleSelectedIndexChanged;
				comboBox.SelectionChangeCommitted -= this.HandleSelectionChangeCommitted;
				comboBox.TextUpdate -= this.HandleTextUpdate;
			}
			base.OnUnsubscribeControlEvents(control);
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x00111A2E File Offset: 0x0010FC2E
		private bool ShouldSerializeDropDownWidth()
		{
			return this.ComboBox.ShouldSerializeDropDownWidth();
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x00111A3B File Offset: 0x0010FC3B
		internal override bool ShouldSerializeFont()
		{
			return !object.Equals(this.Font, ToolStripManager.DefaultFont);
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x00111A50 File Offset: 0x0010FC50
		public override string ToString()
		{
			return base.ToString() + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x040024A6 RID: 9382
		internal static readonly object EventDropDown = new object();

		// Token: 0x040024A7 RID: 9383
		internal static readonly object EventDropDownClosed = new object();

		// Token: 0x040024A8 RID: 9384
		internal static readonly object EventDropDownStyleChanged = new object();

		// Token: 0x040024A9 RID: 9385
		internal static readonly object EventSelectedIndexChanged = new object();

		// Token: 0x040024AA RID: 9386
		internal static readonly object EventSelectionChangeCommitted = new object();

		// Token: 0x040024AB RID: 9387
		internal static readonly object EventTextUpdate = new object();

		// Token: 0x040024AC RID: 9388
		private static readonly Padding dropDownPadding = new Padding(2);

		// Token: 0x040024AD RID: 9389
		private static readonly Padding padding = new Padding(1, 0, 1, 0);

		// Token: 0x040024AE RID: 9390
		private Padding scaledDropDownPadding = ToolStripComboBox.dropDownPadding;

		// Token: 0x040024AF RID: 9391
		private Padding scaledPadding = ToolStripComboBox.padding;

		// Token: 0x020007FD RID: 2045
		[ComVisible(true)]
		internal class ToolStripComboBoxAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006EC8 RID: 28360 RVA: 0x0019665B File Offset: 0x0019485B
			public ToolStripComboBoxAccessibleObject(ToolStripComboBox ownerItem) : base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006EC9 RID: 28361 RVA: 0x0019666B File Offset: 0x0019486B
			internal override void ClearOwnerItem()
			{
				this.ownerItem = null;
				base.ClearOwnerItem();
			}

			// Token: 0x1700182D RID: 6189
			// (get) Token: 0x06006ECA RID: 28362 RVA: 0x0017F055 File Offset: 0x0017D255
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06006ECB RID: 28363 RVA: 0x000072B6 File Offset: 0x000054B6
			public override void DoDefaultAction()
			{
			}

			// Token: 0x1700182E RID: 6190
			// (get) Token: 0x06006ECC RID: 28364 RVA: 0x0019667C File Offset: 0x0019487C
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleRole.ComboBox;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.ComboBox;
				}
			}

			// Token: 0x06006ECD RID: 28365 RVA: 0x001966A8 File Offset: 0x001948A8
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerItemCleared())
				{
					return null;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild || direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return this.ownerItem.ComboBox.AccessibilityObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x1700182F RID: 6191
			// (get) Token: 0x06006ECE RID: 28366 RVA: 0x001966D4 File Offset: 0x001948D4
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return null;
					}
					return this.ownerItem.RootToolStrip.AccessibilityObject;
				}
			}

			// Token: 0x040042F4 RID: 17140
			private ToolStripComboBox ownerItem;
		}

		// Token: 0x020007FE RID: 2046
		internal class ToolStripComboBoxControl : ComboBox
		{
			// Token: 0x06006ECF RID: 28367 RVA: 0x001966F0 File Offset: 0x001948F0
			public ToolStripComboBoxControl()
			{
				base.FlatStyle = FlatStyle.Popup;
				base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
			}

			// Token: 0x17001830 RID: 6192
			// (get) Token: 0x06006ED0 RID: 28368 RVA: 0x0019670B File Offset: 0x0019490B
			// (set) Token: 0x06006ED1 RID: 28369 RVA: 0x00196713 File Offset: 0x00194913
			public ToolStripComboBox Owner
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

			// Token: 0x17001831 RID: 6193
			// (get) Token: 0x06006ED2 RID: 28370 RVA: 0x0019671C File Offset: 0x0019491C
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

			// Token: 0x06006ED3 RID: 28371 RVA: 0x00196751 File Offset: 0x00194951
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level3)
				{
					return new ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxControlAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x06006ED4 RID: 28372 RVA: 0x00196767 File Offset: 0x00194967
			internal override ComboBox.FlatComboAdapter CreateFlatComboAdapterInstance()
			{
				return new ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter(this);
			}

			// Token: 0x06006ED5 RID: 28373 RVA: 0x00196770 File Offset: 0x00194970
			protected override bool IsInputKey(Keys keyData)
			{
				if ((keyData & Keys.Alt) == Keys.Alt)
				{
					if (AccessibilityImprovements.Level5)
					{
						Keys keys = keyData & Keys.KeyCode;
						if (keys == Keys.Up || keys == Keys.Down)
						{
							return true;
						}
					}
					else if ((keyData & Keys.Down) == Keys.Down || (keyData & Keys.Up) == Keys.Up)
					{
						return true;
					}
				}
				return base.IsInputKey(keyData);
			}

			// Token: 0x06006ED6 RID: 28374 RVA: 0x001967BF File Offset: 0x001949BF
			protected override void OnDropDownClosed(EventArgs e)
			{
				base.OnDropDownClosed(e);
				base.Invalidate();
				base.Update();
			}

			// Token: 0x17001832 RID: 6194
			// (get) Token: 0x06006ED7 RID: 28375 RVA: 0x000A8615 File Offset: 0x000A6815
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x040042F5 RID: 17141
			private ToolStripComboBox owner;

			// Token: 0x020008C9 RID: 2249
			internal class ToolStripComboBoxFlatComboAdapter : ComboBox.FlatComboAdapter
			{
				// Token: 0x06007309 RID: 29449 RVA: 0x001A4DF7 File Offset: 0x001A2FF7
				public ToolStripComboBoxFlatComboAdapter(ComboBox comboBox) : base(comboBox, true)
				{
				}

				// Token: 0x0600730A RID: 29450 RVA: 0x001A4E04 File Offset: 0x001A3004
				private static bool UseBaseAdapter(ComboBox comboBox)
				{
					ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = comboBox as ToolStripComboBox.ToolStripComboBoxControl;
					return toolStripComboBoxControl == null || !(toolStripComboBoxControl.Owner.Renderer is ToolStripProfessionalRenderer);
				}

				// Token: 0x0600730B RID: 29451 RVA: 0x001A4E30 File Offset: 0x001A3030
				private static ProfessionalColorTable GetColorTable(ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl)
				{
					if (toolStripComboBoxControl != null)
					{
						return toolStripComboBoxControl.ColorTable;
					}
					return ProfessionalColors.ColorTable;
				}

				// Token: 0x0600730C RID: 29452 RVA: 0x001A4E41 File Offset: 0x001A3041
				protected override Color GetOuterBorderColor(ComboBox comboBox)
				{
					if (ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.UseBaseAdapter(comboBox))
					{
						return base.GetOuterBorderColor(comboBox);
					}
					if (!comboBox.Enabled)
					{
						return ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.GetColorTable(comboBox as ToolStripComboBox.ToolStripComboBoxControl).ComboBoxBorder;
					}
					return SystemColors.Window;
				}

				// Token: 0x0600730D RID: 29453 RVA: 0x001A4E71 File Offset: 0x001A3071
				protected override Color GetPopupOuterBorderColor(ComboBox comboBox, bool focused)
				{
					if (ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.UseBaseAdapter(comboBox))
					{
						return base.GetPopupOuterBorderColor(comboBox, focused);
					}
					if (!comboBox.Enabled)
					{
						return SystemColors.ControlDark;
					}
					if (!focused)
					{
						return SystemColors.Window;
					}
					return ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.GetColorTable(comboBox as ToolStripComboBox.ToolStripComboBoxControl).ComboBoxBorder;
				}

				// Token: 0x0600730E RID: 29454 RVA: 0x001A4EAC File Offset: 0x001A30AC
				protected override void DrawFlatComboDropDown(ComboBox comboBox, Graphics g, Rectangle dropDownRect)
				{
					if (ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.UseBaseAdapter(comboBox))
					{
						base.DrawFlatComboDropDown(comboBox, g, dropDownRect);
						return;
					}
					if (!comboBox.Enabled || !ToolStripManager.VisualStylesEnabled)
					{
						g.FillRectangle(SystemBrushes.Control, dropDownRect);
					}
					else
					{
						ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = comboBox as ToolStripComboBox.ToolStripComboBoxControl;
						ProfessionalColorTable colorTable = ToolStripComboBox.ToolStripComboBoxControl.ToolStripComboBoxFlatComboAdapter.GetColorTable(toolStripComboBoxControl);
						if (!comboBox.DroppedDown)
						{
							bool flag = comboBox.ContainsFocus || comboBox.MouseIsOver;
							if (flag)
							{
								using (Brush brush = new LinearGradientBrush(dropDownRect, colorTable.ComboBoxButtonSelectedGradientBegin, colorTable.ComboBoxButtonSelectedGradientEnd, LinearGradientMode.Vertical))
								{
									g.FillRectangle(brush, dropDownRect);
									goto IL_11A;
								}
							}
							if (toolStripComboBoxControl.Owner.IsOnOverflow)
							{
								using (Brush brush2 = new SolidBrush(colorTable.ComboBoxButtonOnOverflow))
								{
									g.FillRectangle(brush2, dropDownRect);
									goto IL_11A;
								}
							}
							using (Brush brush3 = new LinearGradientBrush(dropDownRect, colorTable.ComboBoxButtonGradientBegin, colorTable.ComboBoxButtonGradientEnd, LinearGradientMode.Vertical))
							{
								g.FillRectangle(brush3, dropDownRect);
								goto IL_11A;
							}
						}
						using (Brush brush4 = new LinearGradientBrush(dropDownRect, colorTable.ComboBoxButtonPressedGradientBegin, colorTable.ComboBoxButtonPressedGradientEnd, LinearGradientMode.Vertical))
						{
							g.FillRectangle(brush4, dropDownRect);
						}
					}
					IL_11A:
					Brush brush5;
					if (comboBox.Enabled)
					{
						if (AccessibilityImprovements.Level2 && SystemInformation.HighContrast && (comboBox.ContainsFocus || comboBox.MouseIsOver) && ToolStripManager.VisualStylesEnabled)
						{
							brush5 = SystemBrushes.HighlightText;
						}
						else
						{
							brush5 = SystemBrushes.ControlText;
						}
					}
					else
					{
						brush5 = SystemBrushes.GrayText;
					}
					Point point = new Point(dropDownRect.Left + dropDownRect.Width / 2, dropDownRect.Top + dropDownRect.Height / 2);
					point.X += dropDownRect.Width % 2;
					g.FillPolygon(brush5, new Point[]
					{
						new Point(point.X - ComboBox.FlatComboAdapter.Offset2Pixels, point.Y - 1),
						new Point(point.X + ComboBox.FlatComboAdapter.Offset2Pixels + 1, point.Y - 1),
						new Point(point.X, point.Y + ComboBox.FlatComboAdapter.Offset2Pixels)
					});
				}
			}

			// Token: 0x020008CA RID: 2250
			internal class ToolStripComboBoxControlAccessibleObject : ComboBox.ComboBoxUiaProvider
			{
				// Token: 0x0600730F RID: 29455 RVA: 0x001A5100 File Offset: 0x001A3300
				public ToolStripComboBoxControlAccessibleObject(ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl) : base(toolStripComboBoxControl)
				{
					this.childAccessibleObject = new ComboBox.ChildAccessibleObject(toolStripComboBoxControl, toolStripComboBoxControl.Handle);
				}

				// Token: 0x06007310 RID: 29456 RVA: 0x001A511C File Offset: 0x001A331C
				internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
				{
					if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = base.Owner as ToolStripComboBox.ToolStripComboBoxControl;
						if (toolStripComboBoxControl != null)
						{
							return toolStripComboBoxControl.Owner.AccessibilityObject.FragmentNavigate(direction);
						}
					}
					return base.FragmentNavigate(direction);
				}

				// Token: 0x1700193C RID: 6460
				// (get) Token: 0x06007311 RID: 29457 RVA: 0x001A5158 File Offset: 0x001A3358
				internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
				{
					get
					{
						ToolStripComboBox.ToolStripComboBoxControl toolStripComboBoxControl = base.Owner as ToolStripComboBox.ToolStripComboBoxControl;
						if (toolStripComboBoxControl != null)
						{
							return toolStripComboBoxControl.Owner.Owner.AccessibilityObject;
						}
						return base.FragmentRoot;
					}
				}

				// Token: 0x06007312 RID: 29458 RVA: 0x001A518B File Offset: 0x001A338B
				internal override object GetPropertyValue(int propertyID)
				{
					if (propertyID == 30003)
					{
						return 50003;
					}
					if (propertyID != 30022)
					{
						return base.GetPropertyValue(propertyID);
					}
					return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
				}

				// Token: 0x06007313 RID: 29459 RVA: 0x001A51CA File Offset: 0x001A33CA
				internal override bool IsPatternSupported(int patternId)
				{
					return !base.IsOwnerControlDestroyed() && (patternId == 10005 || patternId == 10002 || base.IsPatternSupported(patternId));
				}

				// Token: 0x04004557 RID: 17751
				private ComboBox.ChildAccessibleObject childAccessibleObject;
			}
		}
	}
}
