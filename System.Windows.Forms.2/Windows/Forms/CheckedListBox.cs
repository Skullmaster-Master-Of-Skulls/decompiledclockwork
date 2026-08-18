using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x0200014E RID: 334
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[LookupBindingProperties]
	[SRDescription("DescriptionCheckedListBox")]
	public class CheckedListBox : ListBox
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x00025AA6 File Offset: 0x00023CA6
		public CheckedListBox()
		{
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00025ACD File Offset: 0x00023CCD
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x00025AD5 File Offset: 0x00023CD5
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("CheckedListBoxCheckOnClickDescr")]
		public bool CheckOnClick
		{
			get
			{
				return this.checkOnClick;
			}
			set
			{
				this.checkOnClick = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00025ADE File Offset: 0x00023CDE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckedListBox.CheckedIndexCollection CheckedIndices
		{
			get
			{
				if (this.checkedIndexCollection == null)
				{
					this.checkedIndexCollection = new CheckedListBox.CheckedIndexCollection(this);
				}
				return this.checkedIndexCollection;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00025AFA File Offset: 0x00023CFA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public CheckedListBox.CheckedItemCollection CheckedItems
		{
			get
			{
				if (this.checkedItemCollection == null)
				{
					this.checkedItemCollection = new CheckedListBox.CheckedItemCollection(this);
				}
				return this.checkedItemCollection;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00025B18 File Offset: 0x00023D18
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 1040;
				return createParams;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00025B3F File Offset: 0x00023D3F
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x00025B47 File Offset: 0x00023D47
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00025B50 File Offset: 0x00023D50
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00025B58 File Offset: 0x00023D58
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string DisplayMember
		{
			get
			{
				return base.DisplayMember;
			}
			set
			{
				base.DisplayMember = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DrawMode DrawMode
		{
			get
			{
				return DrawMode.Normal;
			}
			set
			{
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00025B61 File Offset: 0x00023D61
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int ItemHeight
		{
			get
			{
				return this.Font.Height + this.scaledListItemBordersHeight;
			}
			set
			{
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00025B75 File Offset: 0x00023D75
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ListBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public new CheckedListBox.ObjectCollection Items
		{
			get
			{
				return (CheckedListBox.ObjectCollection)base.Items;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00025B82 File Offset: 0x00023D82
		internal override int MaxItemWidth
		{
			get
			{
				return base.MaxItemWidth + this.idealCheckSize + this.scaledListItemPaddingBuffer;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x00025B98 File Offset: 0x00023D98
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x00025BA0 File Offset: 0x00023DA0
		public override SelectionMode SelectionMode
		{
			get
			{
				return base.SelectionMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SelectionMode));
				}
				if (value != SelectionMode.One && value != SelectionMode.None)
				{
					throw new ArgumentException(SR.GetString("CheckedListBoxInvalidSelectionMode"));
				}
				if (value != this.SelectionMode)
				{
					base.SelectionMode = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x00025C00 File Offset: 0x00023E00
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x00025C0C File Offset: 0x00023E0C
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("CheckedListBoxThreeDCheckBoxesDescr")]
		public bool ThreeDCheckBoxes
		{
			get
			{
				return !this.flat;
			}
			set
			{
				if (this.flat == value)
				{
					this.flat = !value;
					CheckedListBox.ObjectCollection items = this.Items;
					if (items != null && items.Count > 0)
					{
						base.Invalidate();
					}
				}
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x000249A3 File Offset: 0x00022BA3
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x000249AB File Offset: 0x00022BAB
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRenderingInt;
			}
			set
			{
				base.UseCompatibleTextRenderingInt = value;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00025C45 File Offset: 0x00023E45
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x00025C4D File Offset: 0x00023E4D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new string ValueMember
		{
			get
			{
				return base.ValueMember;
			}
			set
			{
				base.ValueMember = value;
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06000D3D RID: 3389 RVA: 0x00025C56 File Offset: 0x00023E56
		// (remove) Token: 0x06000D3E RID: 3390 RVA: 0x00025C5F File Offset: 0x00023E5F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DataSourceChanged
		{
			add
			{
				base.DataSourceChanged += value;
			}
			remove
			{
				base.DataSourceChanged -= value;
			}
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06000D3F RID: 3391 RVA: 0x00025C68 File Offset: 0x00023E68
		// (remove) Token: 0x06000D40 RID: 3392 RVA: 0x00025C71 File Offset: 0x00023E71
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DisplayMemberChanged
		{
			add
			{
				base.DisplayMemberChanged += value;
			}
			remove
			{
				base.DisplayMemberChanged -= value;
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06000D41 RID: 3393 RVA: 0x00025C7A File Offset: 0x00023E7A
		// (remove) Token: 0x06000D42 RID: 3394 RVA: 0x00025C93 File Offset: 0x00023E93
		[SRCategory("CatBehavior")]
		[SRDescription("CheckedListBoxItemCheckDescr")]
		public event ItemCheckEventHandler ItemCheck
		{
			add
			{
				this.onItemCheck = (ItemCheckEventHandler)Delegate.Combine(this.onItemCheck, value);
			}
			remove
			{
				this.onItemCheck = (ItemCheckEventHandler)Delegate.Remove(this.onItemCheck, value);
			}
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06000D43 RID: 3395 RVA: 0x00025CAC File Offset: 0x00023EAC
		// (remove) Token: 0x06000D44 RID: 3396 RVA: 0x00025CB5 File Offset: 0x00023EB5
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

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06000D45 RID: 3397 RVA: 0x00025CBE File Offset: 0x00023EBE
		// (remove) Token: 0x06000D46 RID: 3398 RVA: 0x00025CC7 File Offset: 0x00023EC7
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

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06000D47 RID: 3399 RVA: 0x00025CD0 File Offset: 0x00023ED0
		// (remove) Token: 0x06000D48 RID: 3400 RVA: 0x00025CD9 File Offset: 0x00023ED9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event DrawItemEventHandler DrawItem
		{
			add
			{
				base.DrawItem += value;
			}
			remove
			{
				base.DrawItem -= value;
			}
		}

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06000D49 RID: 3401 RVA: 0x00025CE2 File Offset: 0x00023EE2
		// (remove) Token: 0x06000D4A RID: 3402 RVA: 0x00025CEB File Offset: 0x00023EEB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.MeasureItem += value;
			}
			remove
			{
				base.MeasureItem -= value;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00025CF4 File Offset: 0x00023EF4
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00025CFC File Offset: 0x00023EFC
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

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x06000D4D RID: 3405 RVA: 0x00025D05 File Offset: 0x00023F05
		// (remove) Token: 0x06000D4E RID: 3406 RVA: 0x00025D0E File Offset: 0x00023F0E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ValueMemberChanged
		{
			add
			{
				base.ValueMemberChanged += value;
			}
			remove
			{
				base.ValueMemberChanged -= value;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00025D17 File Offset: 0x00023F17
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new CheckedListBox.CheckedListBoxAccessibleObject(this);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00025D1F File Offset: 0x00023F1F
		protected override ListBox.ObjectCollection CreateItemCollection()
		{
			return new CheckedListBox.ObjectCollection(this);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00025D28 File Offset: 0x00023F28
		public CheckState GetItemCheckState(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return this.CheckedItems.GetCheckedState(index);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00025D85 File Offset: 0x00023F85
		public bool GetItemChecked(int index)
		{
			return this.GetItemCheckState(index) > CheckState.Unchecked;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00025D94 File Offset: 0x00023F94
		private void InvalidateItem(int index)
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				base.SendMessage(408, index, ref rect);
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), ref rect, false);
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00025DD8 File Offset: 0x00023FD8
		private void LbnSelChange()
		{
			int selectedIndex = this.SelectedIndex;
			if (selectedIndex < 0 || selectedIndex >= this.Items.Count)
			{
				return;
			}
			base.AccessibilityNotifyClients(AccessibleEvents.Focus, selectedIndex);
			base.AccessibilityNotifyClients(AccessibleEvents.Selection, selectedIndex);
			if (!this.killnextselect && (selectedIndex == this.lastSelected || this.checkOnClick))
			{
				CheckState checkedState = this.CheckedItems.GetCheckedState(selectedIndex);
				CheckState newCheckValue = (checkedState != CheckState.Unchecked) ? CheckState.Unchecked : CheckState.Checked;
				ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(selectedIndex, newCheckValue, checkedState);
				this.OnItemCheck(itemCheckEventArgs);
				this.CheckedItems.SetCheckedState(selectedIndex, itemCheckEventArgs.NewValue);
				if (AccessibilityImprovements.Level1)
				{
					base.AccessibilityNotifyClients(AccessibleEvents.StateChange, selectedIndex);
					base.AccessibilityNotifyClients(AccessibleEvents.NameChange, selectedIndex);
				}
			}
			this.lastSelected = selectedIndex;
			this.InvalidateItem(selectedIndex);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00025E94 File Offset: 0x00024094
		protected override void OnClick(EventArgs e)
		{
			this.killnextselect = false;
			base.OnClick(e);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00025EA4 File Offset: 0x000240A4
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(416, 0, this.ItemHeight);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00025EC0 File Offset: 0x000240C0
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.Font.Height < 0)
			{
				this.Font = Control.DefaultFont;
			}
			if (e.Index >= 0)
			{
				object item;
				if (e.Index < this.Items.Count)
				{
					item = this.Items[e.Index];
				}
				else
				{
					item = base.NativeGetItemText(e.Index);
				}
				Rectangle bounds = e.Bounds;
				int itemHeight = this.ItemHeight;
				ButtonState buttonState = ButtonState.Normal;
				if (this.flat)
				{
					buttonState |= ButtonState.Flat;
				}
				if (e.Index < this.Items.Count)
				{
					CheckState checkedState = this.CheckedItems.GetCheckedState(e.Index);
					if (checkedState != CheckState.Checked)
					{
						if (checkedState == CheckState.Indeterminate)
						{
							buttonState |= (ButtonState.Checked | ButtonState.Inactive);
						}
					}
					else
					{
						buttonState |= ButtonState.Checked;
					}
				}
				if (Application.RenderWithVisualStyles)
				{
					CheckBoxState state = CheckBoxRenderer.ConvertFromButtonState(buttonState, false, (e.State & DrawItemState.HotLight) == DrawItemState.HotLight);
					this.idealCheckSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, state, base.HandleInternal).Width;
				}
				int num = Math.Max((itemHeight - this.idealCheckSize) / 2, 0);
				if (num + this.idealCheckSize > bounds.Height)
				{
					num = bounds.Height - this.idealCheckSize;
				}
				Rectangle rectangle = new Rectangle(bounds.X + this.scaledListItemStartPosition, bounds.Y + num, this.idealCheckSize, this.idealCheckSize);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					rectangle.X = bounds.X + bounds.Width - this.idealCheckSize - this.scaledListItemStartPosition;
				}
				if (Application.RenderWithVisualStyles)
				{
					CheckBoxState state2 = CheckBoxRenderer.ConvertFromButtonState(buttonState, false, (e.State & DrawItemState.HotLight) == DrawItemState.HotLight);
					CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(rectangle.X, rectangle.Y), state2, base.HandleInternal);
				}
				else
				{
					ControlPaint.DrawCheckBox(e.Graphics, rectangle, buttonState);
				}
				Rectangle rectangle2 = new Rectangle(bounds.X + this.idealCheckSize + this.scaledListItemStartPosition * 2, bounds.Y, bounds.Width - (this.idealCheckSize + this.scaledListItemStartPosition * 2), bounds.Height);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					rectangle2.X = bounds.X;
				}
				string text = "";
				Color color = (this.SelectionMode != SelectionMode.None) ? e.BackColor : this.BackColor;
				Color color2 = (this.SelectionMode != SelectionMode.None) ? e.ForeColor : this.ForeColor;
				if (!base.Enabled)
				{
					color2 = SystemColors.GrayText;
				}
				Font font = this.Font;
				text = base.GetItemText(item);
				if (this.SelectionMode != SelectionMode.None && (e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					if (base.Enabled)
					{
						color = SystemColors.Highlight;
						color2 = SystemColors.HighlightText;
					}
					else
					{
						color = SystemColors.InactiveBorder;
						color2 = SystemColors.GrayText;
					}
				}
				using (Brush brush = new SolidBrush(color))
				{
					e.Graphics.FillRectangle(brush, rectangle2);
				}
				Rectangle rectangle3 = new Rectangle(rectangle2.X + 1, rectangle2.Y, rectangle2.Width - 1, rectangle2.Height - 2);
				if (this.UseCompatibleTextRendering)
				{
					using (StringFormat stringFormat = new StringFormat())
					{
						if (base.UseTabStops)
						{
							float num2 = 3.6f * (float)this.Font.Height;
							float[] array = new float[15];
							float num3 = (float)(-(float)(this.idealCheckSize + this.scaledListItemStartPosition * 2));
							for (int i = 1; i < array.Length; i++)
							{
								array[i] = num2;
							}
							if (Math.Abs(num3) < num2)
							{
								array[0] = num2 + num3;
							}
							else
							{
								array[0] = num2;
							}
							stringFormat.SetTabStops(0f, array);
						}
						else if (base.UseCustomTabOffsets)
						{
							int count = base.CustomTabOffsets.Count;
							float[] array2 = new float[count];
							base.CustomTabOffsets.CopyTo(array2, 0);
							stringFormat.SetTabStops(0f, array2);
						}
						if (this.RightToLeft == RightToLeft.Yes)
						{
							stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
						}
						stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
						stringFormat.Trimming = StringTrimming.None;
						using (SolidBrush solidBrush = new SolidBrush(color2))
						{
							e.Graphics.DrawString(text, font, solidBrush, rectangle3, stringFormat);
							goto IL_4A0;
						}
					}
				}
				TextFormatFlags textFormatFlags = TextFormatFlags.Default;
				textFormatFlags |= TextFormatFlags.NoPrefix;
				if (base.UseTabStops || base.UseCustomTabOffsets)
				{
					textFormatFlags |= TextFormatFlags.ExpandTabs;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					textFormatFlags |= TextFormatFlags.RightToLeft;
					textFormatFlags |= TextFormatFlags.Right;
				}
				TextRenderer.DrawText(e.Graphics, text, font, rectangle3, color2, textFormatFlags);
				IL_4A0:
				if ((e.State & DrawItemState.Focus) == DrawItemState.Focus && (e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect)
				{
					ControlPaint.DrawFocusRectangle(e.Graphics, rectangle2, color2, color);
				}
			}
			if (this.Items.Count == 0 && AccessibilityImprovements.Level3 && e.Bounds.Width > 2 && e.Bounds.Height > 2)
			{
				Color color3 = (this.SelectionMode != SelectionMode.None) ? e.BackColor : this.BackColor;
				Rectangle bounds2 = e.Bounds;
				Rectangle rectangle4 = new Rectangle(bounds2.X + 1, bounds2.Y, bounds2.Width - 1, bounds2.Height - 2);
				if (this.Focused)
				{
					Color foreColor = (this.SelectionMode != SelectionMode.None) ? e.ForeColor : this.ForeColor;
					if (!base.Enabled)
					{
						foreColor = SystemColors.GrayText;
					}
					ControlPaint.DrawFocusRectangle(e.Graphics, rectangle4, foreColor, color3);
					return;
				}
				if (!Application.RenderWithVisualStyles)
				{
					using (Brush brush2 = new SolidBrush(color3))
					{
						e.Graphics.FillRectangle(brush2, rectangle4);
					}
				}
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x00026500 File Offset: 0x00024700
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (base.IsHandleCreated)
			{
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), null, true);
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00026525 File Offset: 0x00024725
		protected override void OnFontChanged(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(416, 0, this.ItemHeight);
			}
			base.OnFontChanged(e);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00026549 File Offset: 0x00024749
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ' && this.SelectionMode != SelectionMode.None)
			{
				this.LbnSelChange();
			}
			if (base.FormattingEnabled)
			{
				base.OnKeyPress(e);
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00026572 File Offset: 0x00024772
		protected virtual void OnItemCheck(ItemCheckEventArgs ice)
		{
			if (this.onItemCheck != null)
			{
				this.onItemCheck(this, ice);
			}
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00026589 File Offset: 0x00024789
		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			base.OnMeasureItem(e);
			if (e.ItemHeight < this.idealCheckSize + 2)
			{
				e.ItemHeight = this.idealCheckSize + 2;
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000265B0 File Offset: 0x000247B0
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			this.lastSelected = this.SelectedIndex;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000265C8 File Offset: 0x000247C8
		protected override void RefreshItems()
		{
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < this.Items.Count; i++)
			{
				hashtable[i] = this.CheckedItems.GetCheckedState(i);
			}
			base.RefreshItems();
			for (int j = 0; j < this.Items.Count; j++)
			{
				this.CheckedItems.SetCheckedState(j, (CheckState)hashtable[j]);
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00026648 File Offset: 0x00024848
		public void SetItemCheckState(int index, CheckState value)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
			{
				throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
			}
			CheckState checkedState = this.CheckedItems.GetCheckedState(index);
			if (value != checkedState)
			{
				ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(index, value, checkedState);
				this.OnItemCheck(itemCheckEventArgs);
				if (itemCheckEventArgs.NewValue != checkedState)
				{
					this.CheckedItems.SetCheckedState(index, itemCheckEventArgs.NewValue);
					this.InvalidateItem(index);
				}
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00026702 File Offset: 0x00024902
		public void SetItemChecked(int index, bool value)
		{
			this.SetItemCheckState(index, value ? CheckState.Checked : CheckState.Unchecked);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00026714 File Offset: 0x00024914
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WmReflectCommand(ref Message m)
		{
			int num = NativeMethods.Util.HIWORD(m.WParam);
			if (num == 1)
			{
				this.LbnSelChange();
				base.WmReflectCommand(ref m);
				return;
			}
			if (num != 2)
			{
				base.WmReflectCommand(ref m);
				return;
			}
			this.LbnSelChange();
			base.WmReflectCommand(ref m);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002675C File Offset: 0x0002495C
		private void WmReflectVKeyToItem(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			Keys keys = (Keys)num;
			if (keys - Keys.Prior <= 7)
			{
				this.killnextselect = true;
			}
			else
			{
				this.killnextselect = false;
			}
			m.Result = NativeMethods.InvalidIntPtr;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002679C File Offset: 0x0002499C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 8238)
			{
				this.WmReflectVKeyToItem(ref m);
				return;
			}
			if (msg == 8239)
			{
				m.Result = NativeMethods.InvalidIntPtr;
				return;
			}
			if (m.Msg == CheckedListBox.LBC_GETCHECKSTATE)
			{
				int num = (int)((long)m.WParam);
				if (num < 0 || num >= this.Items.Count)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				m.Result = (IntPtr)(this.GetItemChecked(num) ? 1 : 0);
				return;
			}
			else
			{
				if (m.Msg != CheckedListBox.LBC_SETCHECKSTATE)
				{
					base.WndProc(ref m);
					return;
				}
				int num2 = (int)((long)m.WParam);
				int num3 = (int)((long)m.LParam);
				if (num2 < 0 || num2 >= this.Items.Count || (num3 != 1 && num3 != 0))
				{
					m.Result = IntPtr.Zero;
					return;
				}
				this.SetItemChecked(num2, num3 == 1);
				m.Result = (IntPtr)1;
				return;
			}
		}

		// Token: 0x04000777 RID: 1911
		private int idealCheckSize = 13;

		// Token: 0x04000778 RID: 1912
		private const int LB_CHECKED = 1;

		// Token: 0x04000779 RID: 1913
		private const int LB_UNCHECKED = 0;

		// Token: 0x0400077A RID: 1914
		private const int LB_ERROR = -1;

		// Token: 0x0400077B RID: 1915
		private const int BORDER_SIZE = 1;

		// Token: 0x0400077C RID: 1916
		private bool killnextselect;

		// Token: 0x0400077D RID: 1917
		private ItemCheckEventHandler onItemCheck;

		// Token: 0x0400077E RID: 1918
		private bool checkOnClick;

		// Token: 0x0400077F RID: 1919
		private bool flat = true;

		// Token: 0x04000780 RID: 1920
		private int lastSelected = -1;

		// Token: 0x04000781 RID: 1921
		private CheckedListBox.CheckedItemCollection checkedItemCollection;

		// Token: 0x04000782 RID: 1922
		private CheckedListBox.CheckedIndexCollection checkedIndexCollection;

		// Token: 0x04000783 RID: 1923
		private static int LBC_GETCHECKSTATE = SafeNativeMethods.RegisterWindowMessage("LBC_GETCHECKSTATE");

		// Token: 0x04000784 RID: 1924
		private static int LBC_SETCHECKSTATE = SafeNativeMethods.RegisterWindowMessage("LBC_SETCHECKSTATE");

		// Token: 0x0200061E RID: 1566
		public new class ObjectCollection : ListBox.ObjectCollection
		{
			// Token: 0x0600630D RID: 25357 RVA: 0x0016E760 File Offset: 0x0016C960
			public ObjectCollection(CheckedListBox owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x0600630E RID: 25358 RVA: 0x0016E770 File Offset: 0x0016C970
			public int Add(object item, bool isChecked)
			{
				return this.Add(item, isChecked ? CheckState.Checked : CheckState.Unchecked);
			}

			// Token: 0x0600630F RID: 25359 RVA: 0x0016E780 File Offset: 0x0016C980
			public int Add(object item, CheckState check)
			{
				if (!ClientUtils.IsEnumValid(check, (int)check, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)check, typeof(CheckState));
				}
				int num = base.Add(item);
				this.owner.SetItemCheckState(num, check);
				return num;
			}

			// Token: 0x04003923 RID: 14627
			private CheckedListBox owner;
		}

		// Token: 0x0200061F RID: 1567
		public class CheckedIndexCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006310 RID: 25360 RVA: 0x0016E7C9 File Offset: 0x0016C9C9
			internal CheckedIndexCollection(CheckedListBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700151C RID: 5404
			// (get) Token: 0x06006311 RID: 25361 RVA: 0x0016E7D8 File Offset: 0x0016C9D8
			public int Count
			{
				get
				{
					return this.owner.CheckedItems.Count;
				}
			}

			// Token: 0x1700151D RID: 5405
			// (get) Token: 0x06006312 RID: 25362 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700151E RID: 5406
			// (get) Token: 0x06006313 RID: 25363 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700151F RID: 5407
			// (get) Token: 0x06006314 RID: 25364 RVA: 0x00013062 File Offset: 0x00011262
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001520 RID: 5408
			// (get) Token: 0x06006315 RID: 25365 RVA: 0x00013062 File Offset: 0x00011262
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001521 RID: 5409
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public int this[int index]
			{
				get
				{
					object entryObject = this.InnerArray.GetEntryObject(index, CheckedListBox.CheckedItemCollection.AnyMask);
					return this.InnerArray.IndexOfIdentifier(entryObject, 0);
				}
			}

			// Token: 0x17001522 RID: 5410
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
				}
			}

			// Token: 0x06006319 RID: 25369 RVA: 0x0016E826 File Offset: 0x0016CA26
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600631A RID: 25370 RVA: 0x0016E826 File Offset: 0x0016CA26
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600631B RID: 25371 RVA: 0x0016E826 File Offset: 0x0016CA26
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600631C RID: 25372 RVA: 0x0016E826 File Offset: 0x0016CA26
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600631D RID: 25373 RVA: 0x0016E826 File Offset: 0x0016CA26
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600631E RID: 25374 RVA: 0x0016E837 File Offset: 0x0016CA37
			public bool Contains(int index)
			{
				return this.IndexOf(index) != -1;
			}

			// Token: 0x0600631F RID: 25375 RVA: 0x0016E846 File Offset: 0x0016CA46
			bool IList.Contains(object index)
			{
				return index is int && this.Contains((int)index);
			}

			// Token: 0x06006320 RID: 25376 RVA: 0x0016E860 File Offset: 0x0016CA60
			public void CopyTo(Array dest, int index)
			{
				int count = this.owner.CheckedItems.Count;
				for (int i = 0; i < count; i++)
				{
					dest.SetValue(this[i], i + index);
				}
			}

			// Token: 0x17001523 RID: 5411
			// (get) Token: 0x06006321 RID: 25377 RVA: 0x0016E89F File Offset: 0x0016CA9F
			private ListBox.ItemArray InnerArray
			{
				get
				{
					return this.owner.Items.InnerArray;
				}
			}

			// Token: 0x06006322 RID: 25378 RVA: 0x0016E8B4 File Offset: 0x0016CAB4
			public IEnumerator GetEnumerator()
			{
				int[] array = new int[this.Count];
				this.CopyTo(array, 0);
				return array.GetEnumerator();
			}

			// Token: 0x06006323 RID: 25379 RVA: 0x0016E8DC File Offset: 0x0016CADC
			public int IndexOf(int index)
			{
				if (index >= 0 && index < this.owner.Items.Count)
				{
					object entryObject = this.InnerArray.GetEntryObject(index, 0);
					return this.owner.CheckedItems.IndexOfIdentifier(entryObject);
				}
				return -1;
			}

			// Token: 0x06006324 RID: 25380 RVA: 0x0016E921 File Offset: 0x0016CB21
			int IList.IndexOf(object index)
			{
				if (index is int)
				{
					return this.IndexOf((int)index);
				}
				return -1;
			}

			// Token: 0x04003924 RID: 14628
			private CheckedListBox owner;
		}

		// Token: 0x02000620 RID: 1568
		public class CheckedItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006325 RID: 25381 RVA: 0x0016E939 File Offset: 0x0016CB39
			internal CheckedItemCollection(CheckedListBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001524 RID: 5412
			// (get) Token: 0x06006326 RID: 25382 RVA: 0x0016E948 File Offset: 0x0016CB48
			public int Count
			{
				get
				{
					return this.InnerArray.GetCount(CheckedListBox.CheckedItemCollection.AnyMask);
				}
			}

			// Token: 0x17001525 RID: 5413
			// (get) Token: 0x06006327 RID: 25383 RVA: 0x0016E95A File Offset: 0x0016CB5A
			private ListBox.ItemArray InnerArray
			{
				get
				{
					return this.owner.Items.InnerArray;
				}
			}

			// Token: 0x17001526 RID: 5414
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public object this[int index]
			{
				get
				{
					return this.InnerArray.GetItem(index, CheckedListBox.CheckedItemCollection.AnyMask);
				}
				set
				{
					throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
				}
			}

			// Token: 0x17001527 RID: 5415
			// (get) Token: 0x0600632A RID: 25386 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001528 RID: 5416
			// (get) Token: 0x0600632B RID: 25387 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001529 RID: 5417
			// (get) Token: 0x0600632C RID: 25388 RVA: 0x00013062 File Offset: 0x00011262
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700152A RID: 5418
			// (get) Token: 0x0600632D RID: 25389 RVA: 0x00013062 File Offset: 0x00011262
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600632E RID: 25390 RVA: 0x0016E990 File Offset: 0x0016CB90
			public bool Contains(object item)
			{
				return this.IndexOf(item) != -1;
			}

			// Token: 0x0600632F RID: 25391 RVA: 0x0016E99F File Offset: 0x0016CB9F
			public int IndexOf(object item)
			{
				return this.InnerArray.IndexOf(item, CheckedListBox.CheckedItemCollection.AnyMask);
			}

			// Token: 0x06006330 RID: 25392 RVA: 0x0016E9B2 File Offset: 0x0016CBB2
			internal int IndexOfIdentifier(object item)
			{
				return this.InnerArray.IndexOfIdentifier(item, CheckedListBox.CheckedItemCollection.AnyMask);
			}

			// Token: 0x06006331 RID: 25393 RVA: 0x0016E97F File Offset: 0x0016CB7F
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			// Token: 0x06006332 RID: 25394 RVA: 0x0016E97F File Offset: 0x0016CB7F
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			// Token: 0x06006333 RID: 25395 RVA: 0x0016E97F File Offset: 0x0016CB7F
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			// Token: 0x06006334 RID: 25396 RVA: 0x0016E97F File Offset: 0x0016CB7F
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			// Token: 0x06006335 RID: 25397 RVA: 0x0016E97F File Offset: 0x0016CB7F
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("CheckedListBoxCheckedItemCollectionIsReadOnly"));
			}

			// Token: 0x06006336 RID: 25398 RVA: 0x0016E9C8 File Offset: 0x0016CBC8
			public void CopyTo(Array dest, int index)
			{
				int count = this.InnerArray.GetCount(CheckedListBox.CheckedItemCollection.AnyMask);
				for (int i = 0; i < count; i++)
				{
					dest.SetValue(this.InnerArray.GetItem(i, CheckedListBox.CheckedItemCollection.AnyMask), i + index);
				}
			}

			// Token: 0x06006337 RID: 25399 RVA: 0x0016EA0C File Offset: 0x0016CC0C
			internal CheckState GetCheckedState(int index)
			{
				bool state = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.CheckedItemMask);
				bool state2 = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.IndeterminateItemMask);
				if (state2)
				{
					return CheckState.Indeterminate;
				}
				if (state)
				{
					return CheckState.Checked;
				}
				return CheckState.Unchecked;
			}

			// Token: 0x06006338 RID: 25400 RVA: 0x0016EA48 File Offset: 0x0016CC48
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(CheckedListBox.CheckedItemCollection.AnyMask, true);
			}

			// Token: 0x06006339 RID: 25401 RVA: 0x0016EA5C File Offset: 0x0016CC5C
			internal void SetCheckedState(int index, CheckState value)
			{
				bool flag;
				bool flag2;
				if (value != CheckState.Checked)
				{
					if (value != CheckState.Indeterminate)
					{
						flag = false;
						flag2 = false;
					}
					else
					{
						flag = false;
						flag2 = true;
					}
				}
				else
				{
					flag = true;
					flag2 = false;
				}
				bool state = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.CheckedItemMask);
				bool state2 = this.InnerArray.GetState(index, CheckedListBox.CheckedItemCollection.IndeterminateItemMask);
				this.InnerArray.SetState(index, CheckedListBox.CheckedItemCollection.CheckedItemMask, flag);
				this.InnerArray.SetState(index, CheckedListBox.CheckedItemCollection.IndeterminateItemMask, flag2);
				if (state != flag || state2 != flag2)
				{
					this.owner.AccessibilityNotifyClients(AccessibleEvents.StateChange, index);
				}
			}

			// Token: 0x04003925 RID: 14629
			internal static int CheckedItemMask = ListBox.ItemArray.CreateMask();

			// Token: 0x04003926 RID: 14630
			internal static int IndeterminateItemMask = ListBox.ItemArray.CreateMask();

			// Token: 0x04003927 RID: 14631
			internal static int AnyMask = CheckedListBox.CheckedItemCollection.CheckedItemMask | CheckedListBox.CheckedItemCollection.IndeterminateItemMask;

			// Token: 0x04003928 RID: 14632
			private CheckedListBox owner;
		}

		// Token: 0x02000621 RID: 1569
		[ComVisible(true)]
		internal class CheckedListBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x0600633B RID: 25403 RVA: 0x0009B963 File Offset: 0x00099B63
			public CheckedListBoxAccessibleObject(CheckedListBox owner) : base(owner)
			{
			}

			// Token: 0x1700152B RID: 5419
			// (get) Token: 0x0600633C RID: 25404 RVA: 0x0016EB0A File Offset: 0x0016CD0A
			private CheckedListBox CheckedListBox
			{
				get
				{
					return (CheckedListBox)base.Owner;
				}
			}

			// Token: 0x0600633D RID: 25405 RVA: 0x0016EB18 File Offset: 0x0016CD18
			public override AccessibleObject GetChild(int index)
			{
				if (!base.IsOwnerControlDestroyed() && index >= 0 && index < this.CheckedListBox.Items.Count)
				{
					return new CheckedListBox.CheckedListBoxItemAccessibleObject(this.CheckedListBox.GetItemText(this.CheckedListBox.Items[index]), index, this);
				}
				return null;
			}

			// Token: 0x0600633E RID: 25406 RVA: 0x0016EB69 File Offset: 0x0016CD69
			public override int GetChildCount()
			{
				if (!base.IsOwnerControlDestroyed())
				{
					return this.CheckedListBox.Items.Count;
				}
				return 0;
			}

			// Token: 0x0600633F RID: 25407 RVA: 0x0016EB88 File Offset: 0x0016CD88
			public override AccessibleObject GetFocused()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				int focusedIndex = this.CheckedListBox.FocusedIndex;
				if (focusedIndex >= 0)
				{
					return this.GetChild(focusedIndex);
				}
				return null;
			}

			// Token: 0x06006340 RID: 25408 RVA: 0x0016EBB8 File Offset: 0x0016CDB8
			public override AccessibleObject GetSelected()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				int selectedIndex = this.CheckedListBox.SelectedIndex;
				if (selectedIndex >= 0)
				{
					return this.GetChild(selectedIndex);
				}
				return null;
			}

			// Token: 0x06006341 RID: 25409 RVA: 0x0016EBE8 File Offset: 0x0016CDE8
			public override AccessibleObject HitTest(int x, int y)
			{
				int childCount = this.GetChildCount();
				for (int i = 0; i < childCount; i++)
				{
					AccessibleObject child = this.GetChild(i);
					if (child.Bounds.Contains(x, y))
					{
						return child;
					}
				}
				if (this.Bounds.Contains(x, y))
				{
					return this;
				}
				return null;
			}

			// Token: 0x06006342 RID: 25410 RVA: 0x0016EC39 File Offset: 0x0016CE39
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation direction)
			{
				if (this.GetChildCount() > 0)
				{
					if (direction == AccessibleNavigation.FirstChild)
					{
						return this.GetChild(0);
					}
					if (direction == AccessibleNavigation.LastChild)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
				}
				return base.Navigate(direction);
			}
		}

		// Token: 0x02000622 RID: 1570
		[ComVisible(true)]
		internal class CheckedListBoxItemAccessibleObject : AccessibleObject
		{
			// Token: 0x06006343 RID: 25411 RVA: 0x0016EC6A File Offset: 0x0016CE6A
			public CheckedListBoxItemAccessibleObject(string name, int index, CheckedListBox.CheckedListBoxAccessibleObject parent)
			{
				this.name = name;
				this.parent = parent;
				this.index = index;
			}

			// Token: 0x1700152C RID: 5420
			// (get) Token: 0x06006344 RID: 25412 RVA: 0x0016EC88 File Offset: 0x0016CE88
			public override Rectangle Bounds
			{
				get
				{
					if (this.parent.IsOwnerControlDestroyed())
					{
						return Rectangle.Empty;
					}
					Rectangle itemRectangle = this.ParentCheckedListBox.GetItemRectangle(this.index);
					NativeMethods.POINT point = new NativeMethods.POINT(itemRectangle.X, itemRectangle.Y);
					UnsafeNativeMethods.ClientToScreen(new HandleRef(this.ParentCheckedListBox, this.ParentCheckedListBox.Handle), point);
					return new Rectangle(point.x, point.y, itemRectangle.Width, itemRectangle.Height);
				}
			}

			// Token: 0x1700152D RID: 5421
			// (get) Token: 0x06006345 RID: 25413 RVA: 0x0016ED0A File Offset: 0x0016CF0A
			public override string DefaultAction
			{
				get
				{
					if (this.parent.IsOwnerControlDestroyed())
					{
						return string.Empty;
					}
					if (this.ParentCheckedListBox.GetItemChecked(this.index))
					{
						return SR.GetString("AccessibleActionUncheck");
					}
					return SR.GetString("AccessibleActionCheck");
				}
			}

			// Token: 0x1700152E RID: 5422
			// (get) Token: 0x06006346 RID: 25414 RVA: 0x0016ED47 File Offset: 0x0016CF47
			private CheckedListBox ParentCheckedListBox
			{
				get
				{
					return (CheckedListBox)this.parent.Owner;
				}
			}

			// Token: 0x1700152F RID: 5423
			// (get) Token: 0x06006347 RID: 25415 RVA: 0x0016ED59 File Offset: 0x0016CF59
			// (set) Token: 0x06006348 RID: 25416 RVA: 0x0016ED61 File Offset: 0x0016CF61
			public override string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			// Token: 0x17001530 RID: 5424
			// (get) Token: 0x06006349 RID: 25417 RVA: 0x0016ED6A File Offset: 0x0016CF6A
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.parent;
				}
			}

			// Token: 0x17001531 RID: 5425
			// (get) Token: 0x0600634A RID: 25418 RVA: 0x0016ED72 File Offset: 0x0016CF72
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.CheckButton;
				}
			}

			// Token: 0x17001532 RID: 5426
			// (get) Token: 0x0600634B RID: 25419 RVA: 0x0016ED78 File Offset: 0x0016CF78
			public override AccessibleStates State
			{
				get
				{
					if (this.parent.IsOwnerControlDestroyed())
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					switch (this.ParentCheckedListBox.GetItemCheckState(this.index))
					{
					case CheckState.Checked:
						accessibleStates |= AccessibleStates.Checked;
						break;
					case CheckState.Indeterminate:
						accessibleStates |= AccessibleStates.Mixed;
						break;
					}
					if (this.ParentCheckedListBox.SelectedIndex == this.index)
					{
						accessibleStates |= (AccessibleStates.Selected | AccessibleStates.Focused);
					}
					if (AccessibilityImprovements.Level3 && this.ParentCheckedListBox.Focused && this.ParentCheckedListBox.SelectedIndex == -1)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x17001533 RID: 5427
			// (get) Token: 0x0600634C RID: 25420 RVA: 0x0016EE0C File Offset: 0x0016D00C
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.parent.IsOwnerControlDestroyed())
					{
						return string.Empty;
					}
					return this.ParentCheckedListBox.GetItemChecked(this.index).ToString();
				}
			}

			// Token: 0x0600634D RID: 25421 RVA: 0x0016EE45 File Offset: 0x0016D045
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.parent.IsOwnerControlDestroyed())
				{
					return;
				}
				this.ParentCheckedListBox.SetItemChecked(this.index, !this.ParentCheckedListBox.GetItemChecked(this.index));
			}

			// Token: 0x0600634E RID: 25422 RVA: 0x0016EE7C File Offset: 0x0016D07C
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation direction)
			{
				if ((direction == AccessibleNavigation.Down || direction == AccessibleNavigation.Next) && this.index < this.parent.GetChildCount() - 1)
				{
					return this.parent.GetChild(this.index + 1);
				}
				if ((direction == AccessibleNavigation.Up || direction == AccessibleNavigation.Previous) && this.index > 0)
				{
					return this.parent.GetChild(this.index - 1);
				}
				return base.Navigate(direction);
			}

			// Token: 0x0600634F RID: 25423 RVA: 0x0016EEE8 File Offset: 0x0016D0E8
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				try
				{
					if (!this.parent.IsOwnerControlDestroyed())
					{
						this.ParentCheckedListBox.AccessibilityObject.GetSystemIAccessibleInternal().accSelect((int)flags, this.index + 1);
					}
				}
				catch (ArgumentException)
				{
				}
			}

			// Token: 0x04003929 RID: 14633
			private string name;

			// Token: 0x0400392A RID: 14634
			private int index;

			// Token: 0x0400392B RID: 14635
			private CheckedListBox.CheckedListBoxAccessibleObject parent;
		}
	}
}
