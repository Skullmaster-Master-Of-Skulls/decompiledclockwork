using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using Accessibility;

namespace System.Windows.Forms
{
	// Token: 0x02000160 RID: 352
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[DefaultBindingProperty("Text")]
	[Designer("System.Windows.Forms.Design.ComboBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionComboBox")]
	public class ComboBox : ListControl
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00027CBC File Offset: 0x00025EBC
		public ComboBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
			this.requestedHeight = 150;
			base.SetState2(2048, true);
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00027D4B File Offset: 0x00025F4B
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x00027D54 File Offset: 0x00025F54
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("ComboBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.autoCompleteMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteMode));
				}
				if (this.DropDownStyle == ComboBoxStyle.DropDownList && this.AutoCompleteSource != AutoCompleteSource.ListItems && value != AutoCompleteMode.None)
				{
					throw new NotSupportedException(SR.GetString("ComboBoxAutoCompleteModeOnlyNoneAllowed"));
				}
				if (Application.OleRequired() != ApartmentState.STA)
				{
					throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
				}
				bool reset = false;
				if (this.autoCompleteMode != AutoCompleteMode.None && value == AutoCompleteMode.None)
				{
					reset = true;
				}
				this.autoCompleteMode = value;
				this.SetAutoComplete(reset, true);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00027DE5 File Offset: 0x00025FE5
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x00027DF0 File Offset: 0x00025FF0
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("ComboBoxAutoCompleteSourceDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.autoCompleteSource;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[]
				{
					128,
					7,
					6,
					64,
					1,
					32,
					2,
					256,
					4
				}))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteSource));
				}
				if (this.DropDownStyle == ComboBoxStyle.DropDownList && this.AutoCompleteMode != AutoCompleteMode.None && value != AutoCompleteSource.ListItems)
				{
					throw new NotSupportedException(SR.GetString("ComboBoxAutoCompleteSourceOnlyListItemsAllowed"));
				}
				if (Application.OleRequired() != ApartmentState.STA)
				{
					throw new ThreadStateException(SR.GetString("ThreadMustBeSTA"));
				}
				if (value != AutoCompleteSource.None && value != AutoCompleteSource.CustomSource && value != AutoCompleteSource.ListItems)
				{
					new FileIOPermission(PermissionState.Unrestricted)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Demand();
				}
				this.autoCompleteSource = value;
				this.SetAutoComplete(false, true);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00027EAB File Offset: 0x000260AB
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x00027EE0 File Offset: 0x000260E0
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
				if (this.autoCompleteCustomSource == null)
				{
					this.autoCompleteCustomSource = new AutoCompleteStringCollection();
					this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
				}
				return this.autoCompleteCustomSource;
			}
			set
			{
				if (this.autoCompleteCustomSource != value)
				{
					if (this.autoCompleteCustomSource != null)
					{
						this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
					}
					this.autoCompleteCustomSource = value;
					if (this.autoCompleteCustomSource != null)
					{
						this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
					}
					this.SetAutoComplete(false, true);
				}
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00027F43 File Offset: 0x00026143
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x00012F98 File Offset: 0x00011198
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06000DED RID: 3565 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06000DEE RID: 3566 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06000DEF RID: 3567 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06000DF0 RID: 3568 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00027F59 File Offset: 0x00026159
		internal ComboBox.ChildAccessibleObject ChildEditAccessibleObject
		{
			get
			{
				if (this.childEditAccessibleObject == null)
				{
					this.childEditAccessibleObject = new ComboBox.ComboBoxChildEditUiaProvider(this, this.childEdit.Handle);
				}
				return this.childEditAccessibleObject;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00027F80 File Offset: 0x00026180
		internal ComboBox.ChildAccessibleObject ChildListAccessibleObject
		{
			get
			{
				if (this.childListAccessibleObject == null)
				{
					this.childListAccessibleObject = new ComboBox.ComboBoxChildListUiaProvider(this, (this.DropDownStyle == ComboBoxStyle.Simple) ? this.childListBox.Handle : this.dropDownHandle);
				}
				return this.childListAccessibleObject;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00027FB7 File Offset: 0x000261B7
		internal AccessibleObject ChildTextAccessibleObject
		{
			get
			{
				if (this.childTextAccessibleObject == null)
				{
					this.childTextAccessibleObject = new ComboBox.ComboBoxChildTextUiaProvider(this);
				}
				return this.childTextAccessibleObject;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00027FD4 File Offset: 0x000261D4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "COMBOBOX";
				createParams.Style |= 2097728;
				createParams.ExStyle |= 512;
				if (!this.integralHeight)
				{
					createParams.Style |= 1024;
				}
				switch (this.DropDownStyle)
				{
				case ComboBoxStyle.Simple:
					createParams.Style |= 1;
					break;
				case ComboBoxStyle.DropDown:
					createParams.Style |= 2;
					createParams.Height = this.PreferredHeight;
					break;
				case ComboBoxStyle.DropDownList:
					createParams.Style |= 3;
					createParams.Height = this.PreferredHeight;
					break;
				}
				DrawMode drawMode = this.DrawMode;
				if (drawMode != DrawMode.OwnerDrawFixed)
				{
					if (drawMode == DrawMode.OwnerDrawVariable)
					{
						createParams.Style |= 32;
					}
				}
				else
				{
					createParams.Style |= 16;
				}
				return createParams;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x000280C4 File Offset: 0x000262C4
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, this.PreferredHeight);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00025B3F File Offset: 0x00023D3F
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x00025B47 File Offset: 0x00023D47
		[SRCategory("CatData")]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[AttributeProvider(typeof(IListSource))]
		[SRDescription("ListControlDataSourceDescr")]
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

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x000280D4 File Offset: 0x000262D4
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x000280FC File Offset: 0x000262FC
		[SRCategory("CatBehavior")]
		[DefaultValue(DrawMode.Normal)]
		[SRDescription("ComboBoxDrawModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public DrawMode DrawMode
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropDrawMode, out flag);
				if (flag)
				{
					return (DrawMode)integer;
				}
				return DrawMode.Normal;
			}
			set
			{
				if (this.DrawMode != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(DrawMode));
					}
					this.ResetHeightCache();
					base.Properties.SetInteger(ComboBox.PropDrawMode, (int)value);
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00028158 File Offset: 0x00026358
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x00028184 File Offset: 0x00026384
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownWidthDescr")]
		public int DropDownWidth
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropDropDownWidth, out flag);
				if (flag)
				{
					return integer;
				}
				return base.Width;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownWidth", SR.GetString("InvalidArgument", new object[]
					{
						"DropDownWidth",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.Properties.GetInteger(ComboBox.PropDropDownWidth) != value)
				{
					base.Properties.SetInteger(ComboBox.PropDropDownWidth, value);
					if (base.IsHandleCreated)
					{
						base.SendMessage(352, value, 0);
					}
				}
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00028204 File Offset: 0x00026404
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x0002822C File Offset: 0x0002642C
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownHeightDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(106)]
		public int DropDownHeight
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropDropDownHeight, out flag);
				if (flag)
				{
					return integer;
				}
				return 106;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("DropDownHeight", SR.GetString("InvalidArgument", new object[]
					{
						"DropDownHeight",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (base.Properties.GetInteger(ComboBox.PropDropDownHeight) != value)
				{
					base.Properties.SetInteger(ComboBox.PropDropDownHeight, value);
					this.IntegralHeight = false;
				}
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0002829A File Offset: 0x0002649A
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x000282BC File Offset: 0x000264BC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxDroppedDownDescr")]
		public bool DroppedDown
		{
			get
			{
				return base.IsHandleCreated && (int)((long)base.SendMessage(343, 0, 0)) != 0;
			}
			set
			{
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				base.SendMessage(335, value ? -1 : 0, 0);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x000282E0 File Offset: 0x000264E0
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x000282E8 File Offset: 0x000264E8
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[Localizable(true)]
		[SRDescription("ComboBoxFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				this.flatStyle = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00028320 File Offset: 0x00026520
		public override bool Focused
		{
			get
			{
				if (base.Focused)
				{
					return true;
				}
				IntPtr focus = UnsafeNativeMethods.GetFocus();
				return focus != IntPtr.Zero && ((this.childEdit != null && focus == this.childEdit.Handle) || (this.childListBox != null && focus == this.childListBox.Handle));
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x06000E04 RID: 3588 RVA: 0x00013238 File Offset: 0x00011438
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00028384 File Offset: 0x00026584
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0002838C File Offset: 0x0002658C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ComboBoxIntegralHeightDescr")]
		public bool IntegralHeight
		{
			get
			{
				return this.integralHeight;
			}
			set
			{
				if (this.integralHeight != value)
				{
					this.integralHeight = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x000283A4 File Offset: 0x000265A4
		// (set) Token: 0x06000E08 RID: 3592 RVA: 0x00028408 File Offset: 0x00026608
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("ComboBoxItemHeightDescr")]
		public int ItemHeight
		{
			get
			{
				DrawMode drawMode = this.DrawMode;
				if (drawMode == DrawMode.OwnerDrawFixed || drawMode == DrawMode.OwnerDrawVariable || !base.IsHandleCreated)
				{
					bool flag;
					int integer = base.Properties.GetInteger(ComboBox.PropItemHeight, out flag);
					if (flag)
					{
						return integer;
					}
					return base.FontHeight + 2;
				}
				else
				{
					int num = (int)((long)base.SendMessage(340, 0, 0));
					if (num == -1)
					{
						throw new Win32Exception();
					}
					return num;
				}
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidArgument", new object[]
					{
						"ItemHeight",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.ResetHeightCache();
				if (base.Properties.GetInteger(ComboBox.PropItemHeight) != value)
				{
					base.Properties.SetInteger(ComboBox.PropItemHeight, value);
					if (this.DrawMode != DrawMode.Normal)
					{
						this.UpdateItemHeight();
					}
				}
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00028483 File Offset: 0x00026683
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ComboBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public ComboBox.ObjectCollection Items
		{
			get
			{
				if (this.itemsCollection == null)
				{
					this.itemsCollection = new ComboBox.ObjectCollection(this);
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000284A0 File Offset: 0x000266A0
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x000284CD File Offset: 0x000266CD
		private string MatchingText
		{
			get
			{
				string text = (string)base.Properties.GetObject(ComboBox.PropMatchingText);
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value != null || base.Properties.ContainsObject(ComboBox.PropMatchingText))
				{
					base.Properties.SetObject(ComboBox.PropMatchingText, value);
				}
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000284F5 File Offset: 0x000266F5
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00028500 File Offset: 0x00026700
		[SRCategory("CatBehavior")]
		[DefaultValue(8)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxDropDownItemsDescr")]
		public int MaxDropDownItems
		{
			get
			{
				return (int)this.maxDropDownItems;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("MaxDropDownItems", SR.GetString("InvalidBoundArgument", new object[]
					{
						"MaxDropDownItems",
						value.ToString(CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture),
						100.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.maxDropDownItems = (short)value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00011C22 File Offset: 0x0000FE22
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00011C2A File Offset: 0x0000FE2A
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = new Size(value.Width, 0);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00011C3F File Offset: 0x0000FE3F
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x00011C47 File Offset: 0x0000FE47
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = new Size(value.Width, 0);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00028573 File Offset: 0x00026773
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x00028585 File Offset: 0x00026785
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("ComboBoxMaxLengthDescr")]
		public int MaxLength
		{
			get
			{
				return base.Properties.GetInteger(ComboBox.PropMaxLength);
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				if (this.MaxLength != value)
				{
					base.Properties.SetInteger(ComboBox.PropMaxLength, value);
					if (base.IsHandleCreated)
					{
						base.SendMessage(321, value, 0);
					}
				}
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x000285BE File Offset: 0x000267BE
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x000285C6 File Offset: 0x000267C6
		internal bool MouseIsOver
		{
			get
			{
				return this.mouseOver;
			}
			set
			{
				if (this.mouseOver != value)
				{
					this.mouseOver = value;
					if ((!base.ContainsFocus || !Application.RenderWithVisualStyles) && this.FlatStyle == FlatStyle.Popup)
					{
						base.Invalidate();
						base.Update();
					}
				}
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0001365E File Offset: 0x0001185E
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

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06000E18 RID: 3608 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06000E19 RID: 3609 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000285FC File Offset: 0x000267FC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxPreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				if (!base.FormattingEnabled)
				{
					this.prefHeightCache = (short)(TextRenderer.MeasureText(LayoutUtils.TestString, this.Font, new Size(32767, (int)((double)base.FontHeight * 1.25)), TextFormatFlags.SingleLine).Height + SystemInformation.BorderSize.Height * 8 + this.Padding.Size.Height);
					return (int)this.prefHeightCache;
				}
				if (this.prefHeightCache < 0)
				{
					Size size = TextRenderer.MeasureText(LayoutUtils.TestString, this.Font, new Size(32767, (int)((double)base.FontHeight * 1.25)), TextFormatFlags.SingleLine);
					if (this.DropDownStyle == ComboBoxStyle.Simple)
					{
						int num = this.Items.Count + 1;
						this.prefHeightCache = (short)(size.Height * num + SystemInformation.BorderSize.Height * 16 + this.Padding.Size.Height);
					}
					else
					{
						this.prefHeightCache = (short)this.GetComboHeight();
					}
				}
				return (int)this.prefHeightCache;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002871C File Offset: 0x0002691C
		private int GetComboHeight()
		{
			Size size = Size.Empty;
			using (WindowsFont windowsFont = WindowsFont.FromFont(this.Font))
			{
				size = WindowsGraphicsCacheManager.MeasurementGraphics.GetTextExtent("0", windowsFont);
			}
			int num = size.Height + SystemInformation.Border3DSize.Height;
			if (this.DrawMode != DrawMode.Normal)
			{
				num = this.ItemHeight;
			}
			return 2 * SystemInformation.FixedFrameBorderSize.Height + num;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000287A8 File Offset: 0x000269A8
		private string[] GetStringsForAutoComplete(IList collection)
		{
			if (collection is AutoCompleteStringCollection)
			{
				string[] array = new string[this.AutoCompleteCustomSource.Count];
				for (int i = 0; i < this.AutoCompleteCustomSource.Count; i++)
				{
					array[i] = this.AutoCompleteCustomSource[i];
				}
				return array;
			}
			if (collection is ComboBox.ObjectCollection)
			{
				string[] array2 = new string[this.itemsCollection.Count];
				for (int j = 0; j < this.itemsCollection.Count; j++)
				{
					array2[j] = base.GetItemText(this.itemsCollection[j]);
				}
				return array2;
			}
			return new string[0];
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00028841 File Offset: 0x00026A41
		// (set) Token: 0x06000E1E RID: 3614 RVA: 0x00028868 File Offset: 0x00026A68
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedIndexDescr")]
		public override int SelectedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(327, 0, 0));
				}
				return this.selectedIndex;
			}
			set
			{
				if (this.SelectedIndex != value)
				{
					int num = 0;
					if (this.itemsCollection != null)
					{
						num = this.itemsCollection.Count;
					}
					if (value < -1 || value >= num)
					{
						throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidArgument", new object[]
						{
							"SelectedIndex",
							value.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(334, value, 0);
					}
					else
					{
						this.selectedIndex = value;
					}
					this.UpdateText();
					if (base.IsHandleCreated)
					{
						this.OnTextChanged(EventArgs.Empty);
					}
					this.OnSelectedItemChanged(EventArgs.Empty);
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00028920 File Offset: 0x00026B20
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00028948 File Offset: 0x00026B48
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				int num = this.SelectedIndex;
				if (num != -1)
				{
					return this.Items[num];
				}
				return null;
			}
			set
			{
				int num = -1;
				if (this.itemsCollection != null)
				{
					if (value != null)
					{
						num = this.itemsCollection.IndexOf(value);
					}
					else
					{
						this.SelectedIndex = -1;
					}
				}
				if (num != -1)
				{
					this.SelectedIndex = num;
				}
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00028983 File Offset: 0x00026B83
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x000289AC File Offset: 0x00026BAC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectedTextDescr")]
		public string SelectedText
		{
			get
			{
				if (this.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					return "";
				}
				return this.Text.Substring(this.SelectionStart, this.SelectionLength);
			}
			set
			{
				if (this.DropDownStyle != ComboBoxStyle.DropDownList)
				{
					string lParam = (value == null) ? "" : value;
					base.CreateControl();
					if (base.IsHandleCreated && this.childEdit != null)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 194, NativeMethods.InvalidIntPtr, lParam);
					}
				}
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00028A08 File Offset: 0x00026C08
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x00028A43 File Offset: 0x00026C43
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionLengthDescr")]
		public int SelectionLength
		{
			get
			{
				int[] array = new int[1];
				int[] array2 = new int[1];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 320, array2, array);
				return array[0] - array2[0];
			}
			set
			{
				this.Select(this.SelectionStart, value);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00028A54 File Offset: 0x00026C54
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00028A84 File Offset: 0x00026C84
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ComboBoxSelectionStartDescr")]
		public int SelectionStart
		{
			get
			{
				int[] array = new int[1];
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 320, array, null);
				return array[0];
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

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00028AD4 File Offset: 0x00026CD4
		// (set) Token: 0x06000E28 RID: 3624 RVA: 0x00028ADC File Offset: 0x00026CDC
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ComboBoxSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted != value)
				{
					if (this.DataSource != null && value)
					{
						throw new ArgumentException(SR.GetString("ComboBoxSortWithDataSource"));
					}
					this.sorted = value;
					this.RefreshItems();
					this.SelectedIndex = -1;
				}
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00028B18 File Offset: 0x00026D18
		// (set) Token: 0x06000E2A RID: 3626 RVA: 0x00028B40 File Offset: 0x00026D40
		[SRCategory("CatAppearance")]
		[DefaultValue(ComboBoxStyle.DropDown)]
		[SRDescription("ComboBoxStyleDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public ComboBoxStyle DropDownStyle
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(ComboBox.PropStyle, out flag);
				if (flag)
				{
					return (ComboBoxStyle)integer;
				}
				return ComboBoxStyle.DropDown;
			}
			set
			{
				if (this.DropDownStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ComboBoxStyle));
					}
					if (value == ComboBoxStyle.DropDownList && this.AutoCompleteSource != AutoCompleteSource.ListItems && this.AutoCompleteMode != AutoCompleteMode.None)
					{
						this.AutoCompleteMode = AutoCompleteMode.None;
					}
					this.ResetHeightCache();
					base.Properties.SetInteger(ComboBox.PropStyle, (int)value);
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					this.OnDropDownStyleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x00028BCC File Offset: 0x00026DCC
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x00028C34 File Offset: 0x00026E34
		[Localizable(true)]
		[Bindable(true)]
		public override string Text
		{
			get
			{
				if (this.SelectedItem != null && !base.BindingFieldEmpty)
				{
					if (!base.FormattingEnabled)
					{
						return base.FilterItemOnProperty(this.SelectedItem).ToString();
					}
					string itemText = base.GetItemText(this.SelectedItem);
					if (!string.IsNullOrEmpty(itemText) && string.Compare(itemText, base.Text, true, CultureInfo.CurrentCulture) == 0)
					{
						return itemText;
					}
				}
				return base.Text;
			}
			set
			{
				if (this.DropDownStyle == ComboBoxStyle.DropDownList && !base.IsHandleCreated && !string.IsNullOrEmpty(value) && this.FindStringExact(value) == -1)
				{
					return;
				}
				base.Text = value;
				object selectedItem = this.SelectedItem;
				if (!base.DesignMode)
				{
					if (value == null)
					{
						this.SelectedIndex = -1;
						return;
					}
					if (value != null && (selectedItem == null || string.Compare(value, base.GetItemText(selectedItem), false, CultureInfo.CurrentCulture) != 0))
					{
						int num = this.FindStringIgnoreCase(value);
						if (num != -1)
						{
							this.SelectedIndex = num;
						}
					}
				}
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00028CB8 File Offset: 0x00026EB8
		private int FindStringIgnoreCase(string value)
		{
			int num = this.FindStringExact(value, -1, false);
			if (num == -1)
			{
				num = this.FindStringExact(value, -1, true);
			}
			return num;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00028CDE File Offset: 0x00026EDE
		private void NotifyAutoComplete()
		{
			this.NotifyAutoComplete(true);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00028CE8 File Offset: 0x00026EE8
		private void NotifyAutoComplete(bool setSelectedIndex)
		{
			string text = this.Text;
			bool flag = text != this.lastTextChangedValue;
			bool flag2 = false;
			if (setSelectedIndex)
			{
				int num = this.FindStringIgnoreCase(text);
				if (num != -1 && num != this.SelectedIndex)
				{
					this.SelectedIndex = num;
					this.SelectionStart = 0;
					this.SelectionLength = text.Length;
					flag2 = true;
				}
			}
			if (flag && !flag2)
			{
				this.OnTextChanged(EventArgs.Empty);
			}
			this.lastTextChangedValue = text;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00028D57 File Offset: 0x00026F57
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00028D6B File Offset: 0x00026F6B
		private bool SystemAutoCompleteEnabled
		{
			get
			{
				return this.autoCompleteMode != AutoCompleteMode.None && this.DropDownStyle != ComboBoxStyle.DropDownList;
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06000E32 RID: 3634 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x06000E33 RID: 3635 RVA: 0x000238FC File Offset: 0x00021AFC
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

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06000E34 RID: 3636 RVA: 0x00028D83 File Offset: 0x00026F83
		// (remove) Token: 0x06000E35 RID: 3637 RVA: 0x00028D96 File Offset: 0x00026F96
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DRAWITEM, value);
			}
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06000E36 RID: 3638 RVA: 0x00028DA9 File Offset: 0x00026FA9
		// (remove) Token: 0x06000E37 RID: 3639 RVA: 0x00028DBC File Offset: 0x00026FBC
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownDescr")]
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DROPDOWN, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DROPDOWN, value);
			}
		}

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06000E38 RID: 3640 RVA: 0x00028DCF File Offset: 0x00026FCF
		// (remove) Token: 0x06000E39 RID: 3641 RVA: 0x00028DE8 File Offset: 0x00026FE8
		[SRCategory("CatBehavior")]
		[SRDescription("measureItemEventDescr")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_MEASUREITEM, value);
				this.UpdateItemHeight();
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_MEASUREITEM, value);
				this.UpdateItemHeight();
			}
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06000E3A RID: 3642 RVA: 0x00028E01 File Offset: 0x00027001
		// (remove) Token: 0x06000E3B RID: 3643 RVA: 0x00028E14 File Offset: 0x00027014
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
		}

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06000E3C RID: 3644 RVA: 0x00028E27 File Offset: 0x00027027
		// (remove) Token: 0x06000E3D RID: 3645 RVA: 0x00028E3A File Offset: 0x0002703A
		[SRCategory("CatBehavior")]
		[SRDescription("selectionChangeCommittedEventDescr")]
		public event EventHandler SelectionChangeCommitted
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_SELECTIONCHANGECOMMITTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_SELECTIONCHANGECOMMITTED, value);
			}
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06000E3E RID: 3646 RVA: 0x00028E4D File Offset: 0x0002704D
		// (remove) Token: 0x06000E3F RID: 3647 RVA: 0x00028E60 File Offset: 0x00027060
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxDropDownStyleChangedDescr")]
		public event EventHandler DropDownStyleChanged
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DROPDOWNSTYLE, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DROPDOWNSTYLE, value);
			}
		}

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06000E40 RID: 3648 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06000E41 RID: 3649 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06000E42 RID: 3650 RVA: 0x00028E73 File Offset: 0x00027073
		// (remove) Token: 0x06000E43 RID: 3651 RVA: 0x00028E86 File Offset: 0x00027086
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnTextUpdateDescr")]
		public event EventHandler TextUpdate
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_TEXTUPDATE, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_TEXTUPDATE, value);
			}
		}

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06000E44 RID: 3652 RVA: 0x00028E99 File Offset: 0x00027099
		// (remove) Token: 0x06000E45 RID: 3653 RVA: 0x00028EAC File Offset: 0x000270AC
		[SRCategory("CatBehavior")]
		[SRDescription("ComboBoxOnDropDownClosedDescr")]
		public event EventHandler DropDownClosed
		{
			add
			{
				base.Events.AddHandler(ComboBox.EVENT_DROPDOWNCLOSED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBox.EVENT_DROPDOWNCLOSED, value);
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00028EC0 File Offset: 0x000270C0
		[Obsolete("This method has been deprecated.  There is no replacement.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void AddItemsCore(object[] value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			this.BeginUpdate();
			try
			{
				this.Items.AddRangeInternal(value);
			}
			finally
			{
				this.EndUpdate();
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00028F08 File Offset: 0x00027108
		public void BeginUpdate()
		{
			this.updateCount++;
			base.BeginUpdateInternal();
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00028F1E File Offset: 0x0002711E
		private void CheckNoDataSource()
		{
			if (this.DataSource != null)
			{
				throw new ArgumentException(SR.GetString("DataSourceLocksItems"));
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00028F38 File Offset: 0x00027138
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ComboBox.ComboBoxUiaProvider(this);
			}
			if (AccessibilityImprovements.Level1)
			{
				return new ComboBox.ComboBoxExAccessibleObject(this);
			}
			return new ComboBox.ComboBoxAccessibleObject(this);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00028F5C File Offset: 0x0002715C
		internal bool UpdateNeeded()
		{
			return this.updateCount == 0;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00028F68 File Offset: 0x00027168
		internal Point EditToComboboxMapping(Message m)
		{
			if (this.childEdit == null)
			{
				return new Point(0, 0);
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
			NativeMethods.RECT rect2 = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, this.childEdit.Handle), ref rect2);
			int x = NativeMethods.Util.SignedLOWORD(m.LParam) + (rect2.left - rect.left);
			int y = NativeMethods.Util.SignedHIWORD(m.LParam) + (rect2.top - rect.top);
			return new Point(x, y);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00029000 File Offset: 0x00027200
		private void ChildWndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 48)
			{
				if (msg <= 8)
				{
					if (msg != 7)
					{
						if (msg == 8)
						{
							if (!base.DesignMode)
							{
								base.OnImeContextStatusChanged(m.HWnd);
							}
							this.DefChildWndProc(ref m);
							if (this.fireLostFocus)
							{
								base.InvokeLostFocus(this, EventArgs.Empty);
							}
							if (this.FlatStyle == FlatStyle.Popup)
							{
								base.Invalidate();
								return;
							}
							return;
						}
					}
					else
					{
						if (!base.DesignMode)
						{
							ImeContext.SetImeStatus(base.CachedImeMode, m.HWnd);
						}
						if (!base.HostedInWin32DialogManager)
						{
							IContainerControl containerControlInternal = base.GetContainerControlInternal();
							if (containerControlInternal != null)
							{
								ContainerControl containerControl = containerControlInternal as ContainerControl;
								if (containerControl != null && !containerControl.ActivateControlInternal(this, false))
								{
									return;
								}
							}
						}
						this.DefChildWndProc(ref m);
						if (this.fireSetFocus)
						{
							if (!base.DesignMode && this.childEdit != null && m.HWnd == this.childEdit.Handle && !LocalAppContextSwitches.EnableLegacyIMEFocusInComboBox)
							{
								base.WmImeSetFocus();
							}
							base.InvokeGotFocus(this, EventArgs.Empty);
						}
						if (this.FlatStyle == FlatStyle.Popup)
						{
							base.Invalidate();
							return;
						}
						return;
					}
				}
				else if (msg != 32)
				{
					if (msg == 48)
					{
						this.DefChildWndProc(ref m);
						if (this.childEdit != null && m.HWnd == this.childEdit.Handle)
						{
							UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 211, 3, 0);
							return;
						}
						return;
					}
				}
				else
				{
					if (this.Cursor != this.DefaultCursor && this.childEdit != null && m.HWnd == this.childEdit.Handle && NativeMethods.Util.LOWORD(m.LParam) == 1)
					{
						Cursor.CurrentInternal = this.Cursor;
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				}
			}
			else if (msg <= 123)
			{
				if (msg == 81)
				{
					this.DefChildWndProc(ref m);
					return;
				}
				if (msg == 123)
				{
					if (this.ContextMenu != null || this.ContextMenuStrip != null)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 123, m.WParam, m.LParam);
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				}
			}
			else
			{
				switch (msg)
				{
				case 256:
				case 260:
					if (this.SystemAutoCompleteEnabled && !ComboBox.ACNativeWindow.AutoCompleteActive)
					{
						this.finder.FindDropDowns(false);
					}
					if (this.AutoCompleteMode != AutoCompleteMode.None)
					{
						char c = (char)((long)m.WParam);
						if (c == '\u001b')
						{
							this.DroppedDown = false;
						}
						else if (c == '\r' && this.DroppedDown)
						{
							this.UpdateText();
							this.OnSelectionChangeCommittedInternal(EventArgs.Empty);
							this.DroppedDown = false;
						}
					}
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
						return;
					}
					if (base.PreProcessControlMessage(ref m) == PreProcessControlState.MessageProcessed)
					{
						return;
					}
					if (this.ProcessKeyMessage(ref m))
					{
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				case 257:
				case 261:
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
					}
					else if (base.PreProcessControlMessage(ref m) != PreProcessControlState.MessageProcessed)
					{
						if (this.ProcessKeyMessage(ref m))
						{
							return;
						}
						this.DefChildWndProc(ref m);
					}
					if (this.SystemAutoCompleteEnabled && !ComboBox.ACNativeWindow.AutoCompleteActive)
					{
						this.finder.FindDropDowns();
						return;
					}
					return;
				case 258:
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
						return;
					}
					if (base.PreProcessControlMessage(ref m) == PreProcessControlState.MessageProcessed)
					{
						return;
					}
					if (this.ProcessKeyMessage(ref m))
					{
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				case 259:
					break;
				case 262:
					if (this.DropDownStyle == ComboBoxStyle.Simple && m.HWnd == this.childListBox.Handle)
					{
						this.DefChildWndProc(ref m);
						return;
					}
					if (base.PreProcessControlMessage(ref m) == PreProcessControlState.MessageProcessed)
					{
						return;
					}
					if (this.ProcessKeyEventArgs(ref m))
					{
						return;
					}
					this.DefChildWndProc(ref m);
					return;
				default:
					switch (msg)
					{
					case 512:
					{
						Point point = this.EditToComboboxMapping(m);
						this.DefChildWndProc(ref m);
						this.OnMouseEnterInternal(EventArgs.Empty);
						this.OnMouseMove(new MouseEventArgs(Control.MouseButtons, 0, point.X, point.Y, 0));
						return;
					}
					case 513:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point2 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, point2.X, point2.Y, 0));
						return;
					}
					case 514:
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
						Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
						int x = NativeMethods.Util.SignedLOWORD(m.LParam);
						int y = NativeMethods.Util.SignedHIWORD(m.LParam);
						Point point3 = new Point(x, y);
						point3 = base.PointToScreen(point3);
						if (this.mouseEvents && !base.ValidationCancelled)
						{
							this.mouseEvents = false;
							if (this.mousePressed)
							{
								if (rectangle.Contains(point3))
								{
									this.mousePressed = false;
									this.OnClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
									this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								}
								else
								{
									this.mousePressed = false;
									this.mouseInEdit = false;
									this.OnMouseLeave(EventArgs.Empty);
								}
							}
						}
						this.DefChildWndProc(ref m);
						base.CaptureInternal = false;
						point3 = this.EditToComboboxMapping(m);
						this.OnMouseUp(new MouseEventArgs(MouseButtons.Left, 1, point3.X, point3.Y, 0));
						return;
					}
					case 515:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point4 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, point4.X, point4.Y, 0));
						return;
					}
					case 516:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						if (this.ContextMenu != null || this.ContextMenuStrip != null)
						{
							base.CaptureInternal = true;
						}
						this.DefChildWndProc(ref m);
						Point point5 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, point5.X, point5.Y, 0));
						return;
					}
					case 517:
					{
						this.mousePressed = false;
						this.mouseEvents = false;
						if (this.ContextMenu != null)
						{
							base.CaptureInternal = false;
						}
						this.DefChildWndProc(ref m);
						Point point6 = this.EditToComboboxMapping(m);
						this.OnMouseUp(new MouseEventArgs(MouseButtons.Right, 1, point6.X, point6.Y, 0));
						return;
					}
					case 518:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point7 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Right, 1, point7.X, point7.Y, 0));
						return;
					}
					case 519:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point8 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Middle, 1, point8.X, point8.Y, 0));
						return;
					}
					case 520:
						this.mousePressed = false;
						this.mouseEvents = false;
						base.CaptureInternal = false;
						this.DefChildWndProc(ref m);
						this.OnMouseUp(new MouseEventArgs(MouseButtons.Middle, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						return;
					case 521:
					{
						this.mousePressed = true;
						this.mouseEvents = true;
						base.CaptureInternal = true;
						this.DefChildWndProc(ref m);
						Point point9 = this.EditToComboboxMapping(m);
						this.OnMouseDown(new MouseEventArgs(MouseButtons.Middle, 1, point9.X, point9.Y, 0));
						return;
					}
					default:
						if (msg == 675)
						{
							this.DefChildWndProc(ref m);
							this.OnMouseLeaveInternal(EventArgs.Empty);
							return;
						}
						break;
					}
					break;
				}
			}
			this.DefChildWndProc(ref m);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002985F File Offset: 0x00027A5F
		private void OnMouseEnterInternal(EventArgs args)
		{
			if (!this.mouseInEdit)
			{
				this.OnMouseEnter(args);
				this.mouseInEdit = true;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00029878 File Offset: 0x00027A78
		private void OnMouseLeaveInternal(EventArgs args)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
			Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
			Point mousePosition = Control.MousePosition;
			if (!rectangle.Contains(mousePosition))
			{
				this.OnMouseLeave(args);
				this.mouseInEdit = false;
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000298EC File Offset: 0x00027AEC
		private void DefChildWndProc(ref Message m)
		{
			if (this.childEdit != null)
			{
				NativeWindow nativeWindow;
				if (m.HWnd == this.childEdit.Handle)
				{
					nativeWindow = this.childEdit;
				}
				else if (AccessibilityImprovements.Level3 && m.HWnd == this.dropDownHandle)
				{
					nativeWindow = this.childDropDown;
				}
				else
				{
					nativeWindow = this.childListBox;
				}
				if (nativeWindow != null)
				{
					nativeWindow.DefWndProc(ref m);
				}
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00029958 File Offset: 0x00027B58
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.autoCompleteCustomSource != null)
				{
					this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
				}
				if (this.stringSource != null)
				{
					this.stringSource.ReleaseAutoComplete();
					this.stringSource = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000299A8 File Offset: 0x00027BA8
		public void EndUpdate()
		{
			this.updateCount--;
			if (this.updateCount == 0 && this.AutoCompleteSource == AutoCompleteSource.ListItems)
			{
				this.SetAutoComplete(false, false);
			}
			if (base.EndUpdateInternal())
			{
				if (this.childEdit != null && this.childEdit.Handle != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(this, this.childEdit.Handle), null, false);
				}
				if (this.childListBox != null && this.childListBox.Handle != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(this, this.childListBox.Handle), null, false);
				}
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00029A58 File Offset: 0x00027C58
		public int FindString(string s)
		{
			return this.FindString(s, -1);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00029A64 File Offset: 0x00027C64
		public int FindString(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			if (this.itemsCollection == null || this.itemsCollection.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.itemsCollection.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, false);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00029AB9 File Offset: 0x00027CB9
		public int FindStringExact(string s)
		{
			return this.FindStringExact(s, -1, true);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00029AC4 File Offset: 0x00027CC4
		public int FindStringExact(string s, int startIndex)
		{
			return this.FindStringExact(s, startIndex, true);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00029AD0 File Offset: 0x00027CD0
		internal int FindStringExact(string s, int startIndex, bool ignorecase)
		{
			if (s == null)
			{
				return -1;
			}
			if (this.itemsCollection == null || this.itemsCollection.Count == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= this.itemsCollection.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, true, ignorecase);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00029B26 File Offset: 0x00027D26
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			if (this.DropDownStyle == ComboBoxStyle.DropDown || this.DropDownStyle == ComboBoxStyle.DropDownList)
			{
				proposedHeight = this.PreferredHeight;
			}
			return base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, proposedHeight);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00029B4D File Offset: 0x00027D4D
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			if (factor.Width != 1f && factor.Height != 1f)
			{
				this.ResetHeightCache();
			}
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00029B7C File Offset: 0x00027D7C
		public int GetItemHeight(int index)
		{
			if (this.DrawMode != DrawMode.OwnerDrawVariable)
			{
				return this.ItemHeight;
			}
			if (index < 0 || this.itemsCollection == null || index >= this.itemsCollection.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!base.IsHandleCreated)
			{
				return this.ItemHeight;
			}
			int num = (int)((long)base.SendMessage(340, index, 0));
			if (num == -1)
			{
				throw new Win32Exception();
			}
			return num;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00029C13 File Offset: 0x00027E13
		internal IntPtr GetListHandle()
		{
			if (this.DropDownStyle != ComboBoxStyle.Simple)
			{
				return this.dropDownHandle;
			}
			return this.childListBox.Handle;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00029C2F File Offset: 0x00027E2F
		internal NativeWindow GetListNativeWindow()
		{
			if (this.DropDownStyle != ComboBoxStyle.Simple)
			{
				return this.childDropDown;
			}
			return this.childListBox;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00029C48 File Offset: 0x00027E48
		internal int GetListNativeWindowRuntimeIdPart()
		{
			NativeWindow listNativeWindow = this.GetListNativeWindow();
			if (listNativeWindow == null)
			{
				return 0;
			}
			return listNativeWindow.GetHashCode();
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00029C68 File Offset: 0x00027E68
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			if (msg == 312 && !this.ShouldSerializeBackColor())
			{
				return IntPtr.Zero;
			}
			if (msg == 308 && base.GetStyle(ControlStyles.UserPaint))
			{
				SafeNativeMethods.SetTextColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.ForeColor));
				SafeNativeMethods.SetBkColor(new HandleRef(null, dc), ColorTranslator.ToWin32(this.BackColor));
				return base.BackColorBrush;
			}
			return base.InitializeDCForWmCtlColor(dc, msg);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00029CDC File Offset: 0x00027EDC
		private bool InterceptAutoCompleteKeystroke(Message m)
		{
			if (m.Msg == 256)
			{
				if ((int)((long)m.WParam) == 46)
				{
					this.MatchingText = "";
					this.autoCompleteTimeStamp = DateTime.Now.Ticks;
					if (this.Items.Count > 0)
					{
						this.SelectedIndex = 0;
					}
					return false;
				}
			}
			else if (m.Msg == 258)
			{
				char c = (char)((long)m.WParam);
				if (c == '\b')
				{
					if (DateTime.Now.Ticks - this.autoCompleteTimeStamp > 10000000L || this.MatchingText.Length <= 1)
					{
						this.MatchingText = "";
						if (this.Items.Count > 0)
						{
							this.SelectedIndex = 0;
						}
					}
					else
					{
						this.MatchingText = this.MatchingText.Remove(this.MatchingText.Length - 1);
						this.SelectedIndex = this.FindString(this.MatchingText);
					}
					this.autoCompleteTimeStamp = DateTime.Now.Ticks;
					return false;
				}
				if (c == '\u001b')
				{
					this.MatchingText = "";
				}
				if (c != '\u001b' && c != '\r' && !this.DroppedDown && this.AutoCompleteMode != AutoCompleteMode.Append)
				{
					this.DroppedDown = true;
				}
				string text;
				if (DateTime.Now.Ticks - this.autoCompleteTimeStamp > 10000000L)
				{
					text = new string(c, 1);
					if (this.FindString(text) != -1)
					{
						this.MatchingText = text;
					}
					this.autoCompleteTimeStamp = DateTime.Now.Ticks;
					return false;
				}
				text = this.MatchingText + c.ToString();
				int num = this.FindString(text);
				if (num != -1)
				{
					this.MatchingText = text;
					if (num != this.SelectedIndex)
					{
						this.SelectedIndex = num;
					}
				}
				this.autoCompleteTimeStamp = DateTime.Now.Ticks;
				return true;
			}
			return false;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00029EBF File Offset: 0x000280BF
		private void InvalidateEverything()
		{
			SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1157);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00029EE0 File Offset: 0x000280E0
		protected override bool IsInputKey(Keys keyData)
		{
			Keys keys = keyData & (Keys.KeyCode | Keys.Alt);
			if (keys == Keys.Return || keys == Keys.Escape)
			{
				if (this.DroppedDown || this.autoCompleteDroppedDown)
				{
					return true;
				}
				if (this.SystemAutoCompleteEnabled && ComboBox.ACNativeWindow.AutoCompleteActive)
				{
					this.autoCompleteDroppedDown = true;
					return true;
				}
			}
			return base.IsInputKey(keyData);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00029F30 File Offset: 0x00028130
		private int NativeAdd(object item)
		{
			int num = (int)((long)base.SendMessage(323, 0, base.GetItemText(item)));
			if (num < 0)
			{
				throw new OutOfMemoryException(SR.GetString("ComboBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00029F6C File Offset: 0x0002816C
		private void NativeClear()
		{
			string text = null;
			if (this.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				text = this.WindowText;
			}
			base.SendMessage(331, 0, 0);
			if (text != null)
			{
				this.WindowText = text;
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00029FA4 File Offset: 0x000281A4
		private string NativeGetItemText(int index)
		{
			int num = (int)((long)base.SendMessage(329, index, 0));
			StringBuilder stringBuilder = new StringBuilder(num + 1);
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 328, index, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00029FF0 File Offset: 0x000281F0
		private int NativeInsert(int index, object item)
		{
			int num = (int)((long)base.SendMessage(330, index, base.GetItemText(item)));
			if (num < 0)
			{
				throw new OutOfMemoryException(SR.GetString("ComboBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0002A02C File Offset: 0x0002822C
		private void NativeRemoveAt(int index)
		{
			if (this.DropDownStyle == ComboBoxStyle.DropDownList && this.SelectedIndex == index)
			{
				base.Invalidate();
			}
			base.SendMessage(324, index, 0);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002A054 File Offset: 0x00028254
		internal override void RecreateHandleCore()
		{
			string windowText = this.WindowText;
			base.RecreateHandleCore();
			if (!string.IsNullOrEmpty(windowText) && string.IsNullOrEmpty(this.WindowText))
			{
				this.WindowText = windowText;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0002A08C File Offset: 0x0002828C
		protected override void CreateHandle()
		{
			using (new LayoutTransaction(this.ParentInternal, this, PropertyNames.Bounds))
			{
				base.CreateHandle();
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0002A0D0 File Offset: 0x000282D0
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.MaxLength > 0)
			{
				base.SendMessage(321, this.MaxLength, 0);
			}
			bool flag = this.childEdit == null && this.childListBox == null;
			if (flag && this.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				IntPtr window = UnsafeNativeMethods.GetWindow(new HandleRef(this, base.Handle), 5);
				if (window != IntPtr.Zero)
				{
					if (this.DropDownStyle == ComboBoxStyle.Simple)
					{
						this.childListBox = new ComboBox.ComboBoxChildNativeWindow(this, ComboBox.ChildWindowType.ListBox);
						this.childListBox.AssignHandle(window);
						window = UnsafeNativeMethods.GetWindow(new HandleRef(this, window), 2);
					}
					this.childEdit = new ComboBox.ComboBoxChildNativeWindow(this, ComboBox.ChildWindowType.Edit);
					this.childEdit.AssignHandle(window);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 211, 3, 0);
				}
			}
			bool flag2;
			int integer = base.Properties.GetInteger(ComboBox.PropDropDownWidth, out flag2);
			if (flag2)
			{
				base.SendMessage(352, integer, 0);
			}
			flag2 = false;
			int integer2 = base.Properties.GetInteger(ComboBox.PropItemHeight, out flag2);
			if (flag2)
			{
				this.UpdateItemHeight();
			}
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				base.Height = this.requestedHeight;
			}
			try
			{
				this.fromHandleCreate = true;
				this.SetAutoComplete(false, false);
			}
			finally
			{
				this.fromHandleCreate = false;
			}
			if (this.itemsCollection != null)
			{
				foreach (object item in this.itemsCollection)
				{
					this.NativeAdd(item);
				}
				if (this.selectedIndex >= 0)
				{
					base.SendMessage(334, this.selectedIndex, 0);
					this.UpdateText();
					this.selectedIndex = -1;
				}
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0002A2B4 File Offset: 0x000284B4
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.dropDownHandle = IntPtr.Zero;
			if (base.Disposing)
			{
				this.itemsCollection = null;
				this.selectedIndex = -1;
			}
			else
			{
				this.selectedIndex = this.SelectedIndex;
			}
			if (this.stringSource != null)
			{
				this.stringSource.ReleaseAutoComplete();
				this.stringSource = null;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0002A314 File Offset: 0x00028514
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[ComboBox.EVENT_DRAWITEM];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0002A344 File Offset: 0x00028544
		protected virtual void OnDropDown(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_DROPDOWN];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (AccessibilityImprovements.Level3 && base.IsHandleCreated)
			{
				base.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Collapsed, UnsafeNativeMethods.ExpandCollapseState.Expanded);
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null)
				{
					comboBoxUiaProvider.SetComboBoxItemFocus();
				}
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0002A3B4 File Offset: 0x000285B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (this.SystemAutoCompleteEnabled)
			{
				if (e.KeyCode == Keys.Return)
				{
					this.NotifyAutoComplete(true);
				}
				else if (e.KeyCode == Keys.Escape && this.autoCompleteDroppedDown)
				{
					this.NotifyAutoComplete(false);
				}
				this.autoCompleteDroppedDown = false;
			}
			base.OnKeyDown(e);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0002A404 File Offset: 0x00028604
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (!e.Handled && (e.KeyChar == '\r' || e.KeyChar == '\u001b') && this.DroppedDown)
			{
				this.dropDown = false;
				if (base.FormattingEnabled)
				{
					this.Text = this.WindowText;
					this.SelectAll();
					e.Handled = false;
					return;
				}
				this.DroppedDown = false;
				e.Handled = true;
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002A474 File Offset: 0x00028674
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[ComboBox.EVENT_MEASUREITEM];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002A4A2 File Offset: 0x000286A2
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this.MouseIsOver = true;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002A4B2 File Offset: 0x000286B2
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.MouseIsOver = false;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0002A4C4 File Offset: 0x000286C4
		private void OnSelectionChangeCommittedInternal(EventArgs e)
		{
			if (this.allowCommit)
			{
				try
				{
					this.allowCommit = false;
					this.OnSelectionChangeCommitted(e);
				}
				finally
				{
					this.allowCommit = true;
				}
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0002A504 File Offset: 0x00028704
		protected virtual void OnSelectionChangeCommitted(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_SELECTIONCHANGECOMMITTED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.dropDown)
			{
				this.dropDownWillBeClosed = true;
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0002A544 File Offset: 0x00028744
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_SELECTEDINDEXCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (this.dropDownWillBeClosed)
			{
				this.dropDownWillBeClosed = false;
			}
			else if (AccessibilityImprovements.Level3 && base.IsHandleCreated)
			{
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null && (this.DropDownStyle == ComboBoxStyle.DropDownList || this.DropDownStyle == ComboBoxStyle.DropDown))
				{
					if (this.dropDown)
					{
						comboBoxUiaProvider.SetComboBoxItemFocus();
					}
					comboBoxUiaProvider.SetComboBoxItemSelection();
				}
			}
			if (base.DataManager != null && base.DataManager.Position != this.SelectedIndex && (!base.FormattingEnabled || this.SelectedIndex != -1))
			{
				base.DataManager.Position = this.SelectedIndex;
			}
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0002A60B File Offset: 0x0002880B
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
			this.selectedValueChangedFired = true;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002A61C File Offset: 0x0002881C
		protected virtual void OnSelectedItemChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_SELECTEDITEMCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002A64C File Offset: 0x0002884C
		protected virtual void OnDropDownStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_DROPDOWNSTYLE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0002A67A File Offset: 0x0002887A
		protected override void OnParentBackColorChanged(EventArgs e)
		{
			base.OnParentBackColorChanged(e);
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002A691 File Offset: 0x00028891
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.ResetHeightCache();
			if (this.AutoCompleteMode == AutoCompleteMode.None)
			{
				this.UpdateControl(true);
			}
			else
			{
				base.RecreateHandle();
			}
			CommonProperties.xClearPreferredSizeCache(this);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0002A6BD File Offset: 0x000288BD
		private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
			{
				if (this.AutoCompleteCustomSource.Count == 0)
				{
					this.SetAutoComplete(true, true);
					return;
				}
				this.SetAutoComplete(true, false);
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0002A6E7 File Offset: 0x000288E7
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.UpdateControl(false);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0002A6F7 File Offset: 0x000288F7
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.UpdateControl(false);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0002A707 File Offset: 0x00028907
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnGotFocus(EventArgs e)
		{
			if (!this.canFireLostFocus)
			{
				base.OnGotFocus(e);
				this.canFireLostFocus = true;
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002A720 File Offset: 0x00028920
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.canFireLostFocus)
			{
				if (this.AutoCompleteMode != AutoCompleteMode.None && this.AutoCompleteSource == AutoCompleteSource.ListItems && this.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					this.MatchingText = "";
				}
				base.OnLostFocus(e);
				this.canFireLostFocus = false;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0002A76C File Offset: 0x0002896C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnTextChanged(EventArgs e)
		{
			if (this.SystemAutoCompleteEnabled)
			{
				string text = this.Text;
				if (text != this.lastTextChangedValue)
				{
					base.OnTextChanged(e);
					this.lastTextChangedValue = text;
					return;
				}
			}
			else
			{
				base.OnTextChanged(e);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002A7AC File Offset: 0x000289AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnValidating(CancelEventArgs e)
		{
			if (this.SystemAutoCompleteEnabled)
			{
				this.NotifyAutoComplete();
			}
			base.OnValidating(e);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002A7C3 File Offset: 0x000289C3
		private void UpdateControl(bool recreate)
		{
			this.ResetHeightCache();
			if (base.IsHandleCreated)
			{
				if (this.DropDownStyle == ComboBoxStyle.Simple && recreate)
				{
					base.RecreateHandle();
					return;
				}
				this.UpdateItemHeight();
				this.InvalidateEverything();
			}
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002A7F3 File Offset: 0x000289F3
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.DropDownStyle == ComboBoxStyle.Simple)
			{
				this.InvalidateEverything();
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0002A80C File Offset: 0x00028A0C
		protected override void OnDataSourceChanged(EventArgs e)
		{
			if (this.Sorted && this.DataSource != null && base.Created)
			{
				this.DataSource = null;
				throw new InvalidOperationException(SR.GetString("ComboBoxDataSourceWithSort"));
			}
			if (this.DataSource == null)
			{
				this.BeginUpdate();
				this.SelectedIndex = -1;
				this.Items.ClearInternal();
				this.EndUpdate();
			}
			if (!this.Sorted && base.Created)
			{
				base.OnDataSourceChanged(e);
			}
			this.RefreshItems();
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002A88B File Offset: 0x00028A8B
		protected override void OnDisplayMemberChanged(EventArgs e)
		{
			base.OnDisplayMemberChanged(e);
			this.RefreshItems();
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002A89C File Offset: 0x00028A9C
		protected virtual void OnDropDownClosed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_DROPDOWNCLOSED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (AccessibilityImprovements.Level3 && base.IsHandleCreated)
			{
				if (this.DropDownStyle == ComboBoxStyle.DropDown)
				{
					base.AccessibilityObject.RaiseAutomationEvent(20005);
				}
				base.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Expanded, UnsafeNativeMethods.ExpandCollapseState.Collapsed);
				this.dropDownWillBeClosed = false;
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002A918 File Offset: 0x00028B18
		protected virtual void OnTextUpdate(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBox.EVENT_TEXTUPDATE];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0002A946 File Offset: 0x00028B46
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyEventArgs(ref Message m)
		{
			return (this.AutoCompleteMode != AutoCompleteMode.None && this.AutoCompleteSource == AutoCompleteSource.ListItems && this.DropDownStyle == ComboBoxStyle.DropDownList && this.InterceptAutoCompleteKeystroke(m)) || base.ProcessKeyEventArgs(ref m);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0002A97D File Offset: 0x00028B7D
		private void ResetHeightCache()
		{
			this.prefHeightCache = -1;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0002A988 File Offset: 0x00028B88
		protected override void RefreshItems()
		{
			int num = this.SelectedIndex;
			ComboBox.ObjectCollection objectCollection = this.itemsCollection;
			this.itemsCollection = null;
			if (base.IsHandleCreated && base.IsAccessibilityObjectCreated)
			{
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null)
				{
					comboBoxUiaProvider.ResetListItemAccessibleObjects();
				}
			}
			object[] array = null;
			if (base.DataManager != null && base.DataManager.Count != -1)
			{
				array = new object[base.DataManager.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = base.DataManager[i];
				}
			}
			else if (objectCollection != null)
			{
				array = new object[objectCollection.Count];
				objectCollection.CopyTo(array, 0);
			}
			this.BeginUpdate();
			try
			{
				if (base.IsHandleCreated)
				{
					this.NativeClear();
				}
				if (array != null)
				{
					this.Items.AddRangeInternal(array);
				}
				if (base.DataManager != null)
				{
					this.SelectedIndex = base.DataManager.Position;
				}
				else
				{
					this.SelectedIndex = num;
				}
			}
			finally
			{
				this.EndUpdate();
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0002AA90 File Offset: 0x00028C90
		protected override void RefreshItem(int index)
		{
			this.Items.SetItemInternal(index, this.Items[index]);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0002AAAC File Offset: 0x00028CAC
		private void ReleaseChildWindow()
		{
			if (this.childEdit != null)
			{
				this.childEdit.ReleaseHandle();
				this.childEdit = null;
				if (LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy)
				{
					ComboBox.ComboBoxChildEditUiaProvider comboBoxChildEditUiaProvider = this.childEditAccessibleObject;
					if (comboBoxChildEditUiaProvider != null)
					{
						comboBoxChildEditUiaProvider.ClearOwner();
					}
				}
			}
			if (this.childListBox != null)
			{
				if (AccessibilityImprovements.Level3 && !LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy)
				{
					base.ReleaseUiaProvider(this.childListBox.Handle);
				}
				this.childListBox.ReleaseHandle();
				this.childListBox = null;
			}
			if (this.childDropDown != null)
			{
				if (AccessibilityImprovements.Level3 && !LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy)
				{
					base.ReleaseUiaProvider(this.childDropDown.Handle);
				}
				this.childDropDown.ReleaseHandle();
				this.childDropDown = null;
			}
			if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && base.IsAccessibilityObjectCreated)
			{
				ComboBox.ComboBoxChildTextUiaProvider comboBoxChildTextUiaProvider = this.childTextAccessibleObject;
				if (comboBoxChildTextUiaProvider != null)
				{
					comboBoxChildTextUiaProvider.ClearOwnerComboBox();
				}
				ComboBox.ComboBoxChildListUiaProvider comboBoxChildListUiaProvider = this.childListAccessibleObject;
				if (comboBoxChildListUiaProvider == null)
				{
					return;
				}
				comboBoxChildListUiaProvider.ClearOwner();
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0002AB8D File Offset: 0x00028D8D
		private void ResetAutoCompleteCustomSource()
		{
			this.AutoCompleteCustomSource = null;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002AB96 File Offset: 0x00028D96
		private void ResetDropDownWidth()
		{
			base.Properties.RemoveInteger(ComboBox.PropDropDownWidth);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002ABA8 File Offset: 0x00028DA8
		private void ResetItemHeight()
		{
			base.Properties.RemoveInteger(ComboBox.PropItemHeight);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002ABBA File Offset: 0x00028DBA
		public override void ResetText()
		{
			base.ResetText();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002ABC4 File Offset: 0x00028DC4
		private void SetAutoComplete(bool reset, bool recreate)
		{
			if (!base.IsHandleCreated || this.childEdit == null)
			{
				return;
			}
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				if (!this.fromHandleCreate && recreate && base.IsHandleCreated)
				{
					AutoCompleteMode autoCompleteMode = this.AutoCompleteMode;
					this.autoCompleteMode = AutoCompleteMode.None;
					base.RecreateHandle();
					this.autoCompleteMode = autoCompleteMode;
				}
				if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
				{
					if (this.AutoCompleteCustomSource == null)
					{
						return;
					}
					if (this.AutoCompleteCustomSource.Count == 0)
					{
						int flags = -1610612736;
						SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), flags);
						return;
					}
					if (this.stringSource != null)
					{
						this.stringSource.RefreshList(this.GetStringsForAutoComplete(this.AutoCompleteCustomSource));
						return;
					}
					this.stringSource = new StringSource(this.GetStringsForAutoComplete(this.AutoCompleteCustomSource));
					if (!this.stringSource.Bind(new HandleRef(this, this.childEdit.Handle), (int)this.AutoCompleteMode))
					{
						throw new ArgumentException(SR.GetString("AutoCompleteFailure"));
					}
					return;
				}
				else if (this.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					if (this.DropDownStyle == ComboBoxStyle.DropDownList)
					{
						int flags2 = -1610612736;
						SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), flags2);
						return;
					}
					if (this.itemsCollection == null)
					{
						return;
					}
					if (this.itemsCollection.Count == 0)
					{
						int flags3 = -1610612736;
						SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), flags3);
						return;
					}
					if (this.stringSource != null)
					{
						this.stringSource.RefreshList(this.GetStringsForAutoComplete(this.Items));
						return;
					}
					this.stringSource = new StringSource(this.GetStringsForAutoComplete(this.Items));
					if (!this.stringSource.Bind(new HandleRef(this, this.childEdit.Handle), (int)this.AutoCompleteMode))
					{
						throw new ArgumentException(SR.GetString("AutoCompleteFailureListItems"));
					}
					return;
				}
				else
				{
					try
					{
						int num = 0;
						if (this.AutoCompleteMode == AutoCompleteMode.Suggest)
						{
							num |= -1879048192;
						}
						if (this.AutoCompleteMode == AutoCompleteMode.Append)
						{
							num |= 1610612736;
						}
						if (this.AutoCompleteMode == AutoCompleteMode.SuggestAppend)
						{
							num |= 268435456;
							num |= 1073741824;
						}
						int num2 = SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), (int)(this.AutoCompleteSource | (AutoCompleteSource)num));
						return;
					}
					catch (SecurityException)
					{
						return;
					}
				}
			}
			if (reset)
			{
				int flags4 = -1610612736;
				SafeNativeMethods.SHAutoComplete(new HandleRef(this, this.childEdit.Handle), flags4);
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0002AE50 File Offset: 0x00029050
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
			int num = start + length;
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.GetString("InvalidArgument", new object[]
				{
					"length",
					length.ToString(CultureInfo.CurrentCulture)
				}));
			}
			base.SendMessage(322, 0, NativeMethods.Util.MAKELPARAM(start, num));
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002AEE1 File Offset: 0x000290E1
		public void SelectAll()
		{
			this.Select(0, int.MaxValue);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0002AEEF File Offset: 0x000290EF
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				this.requestedHeight = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002AF0C File Offset: 0x0002910C
		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			this.Items.ClearInternal();
			this.Items.AddRangeInternal(value);
			if (base.DataManager != null)
			{
				if (this.DataSource is ICurrencyManagerProvider)
				{
					this.selectedValueChangedFired = false;
				}
				if (base.IsHandleCreated)
				{
					base.SendMessage(334, base.DataManager.Position, 0);
				}
				else
				{
					this.selectedIndex = base.DataManager.Position;
				}
				if (!this.selectedValueChangedFired)
				{
					this.OnSelectedValueChanged(EventArgs.Empty);
					this.selectedValueChangedFired = false;
				}
			}
			this.EndUpdate();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0002AFA5 File Offset: 0x000291A5
		protected override void SetItemCore(int index, object value)
		{
			this.Items.SetItemInternal(index, value);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0002AFB4 File Offset: 0x000291B4
		private bool ShouldSerializeAutoCompleteCustomSource()
		{
			return this.autoCompleteCustomSource != null && this.autoCompleteCustomSource.Count > 0;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0002AFCE File Offset: 0x000291CE
		internal bool ShouldSerializeDropDownWidth()
		{
			return base.Properties.ContainsInteger(ComboBox.PropDropDownWidth);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002AFE0 File Offset: 0x000291E0
		internal bool ShouldSerializeItemHeight()
		{
			return base.Properties.ContainsInteger(ComboBox.PropItemHeight);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0002AFF2 File Offset: 0x000291F2
		internal override bool ShouldSerializeText()
		{
			return this.SelectedIndex == -1 && base.ShouldSerializeText();
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002B008 File Offset: 0x00029208
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Items.Count: " + ((this.itemsCollection == null) ? 0.ToString(CultureInfo.CurrentCulture) : this.itemsCollection.Count.ToString(CultureInfo.CurrentCulture));
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002B058 File Offset: 0x00029258
		private void UpdateDropDownHeight()
		{
			if (this.dropDownHandle != IntPtr.Zero)
			{
				int num = this.DropDownHeight;
				if (num == 106)
				{
					int val = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
					int num2 = Math.Min(Math.Max(val, 1), (int)this.maxDropDownItems);
					num = this.ItemHeight * num2 + 2;
				}
				SafeNativeMethods.SetWindowPos(new HandleRef(this, this.dropDownHandle), NativeMethods.NullHandleRef, 0, 0, this.DropDownWidth, num, 6);
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002B0DC File Offset: 0x000292DC
		private void UpdateItemHeight()
		{
			if (!base.IsHandleCreated)
			{
				base.CreateControl();
			}
			if (this.DrawMode == DrawMode.OwnerDrawFixed)
			{
				base.SendMessage(339, -1, this.ItemHeight);
				base.SendMessage(339, 0, this.ItemHeight);
				return;
			}
			if (this.DrawMode == DrawMode.OwnerDrawVariable)
			{
				base.SendMessage(339, -1, this.ItemHeight);
				Graphics graphics = base.CreateGraphicsInternal();
				for (int i = 0; i < this.Items.Count; i++)
				{
					int num = (int)((long)base.SendMessage(340, i, 0));
					MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, i, num);
					this.OnMeasureItem(measureItemEventArgs);
					if (measureItemEventArgs.ItemHeight != num)
					{
						base.SendMessage(339, i, measureItemEventArgs.ItemHeight);
					}
				}
				graphics.Dispose();
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002B1A8 File Offset: 0x000293A8
		private void UpdateText()
		{
			string text = null;
			if (this.SelectedIndex != -1)
			{
				object obj = this.Items[this.SelectedIndex];
				if (obj != null)
				{
					text = base.GetItemText(obj);
				}
			}
			this.Text = text;
			if (this.DropDownStyle == ComboBoxStyle.DropDown && this.childEdit != null && this.childEdit.Handle != IntPtr.Zero)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, this.childEdit.Handle), 12, IntPtr.Zero, text);
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002B22C File Offset: 0x0002942C
		private void WmEraseBkgnd(ref Message m)
		{
			if (this.DropDownStyle == ComboBoxStyle.Simple && this.ParentInternal != null)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				SafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
				Control parentInternal = this.ParentInternal;
				Graphics graphics = Graphics.FromHdcInternal(m.WParam);
				if (parentInternal != null)
				{
					Brush brush = new SolidBrush(parentInternal.BackColor);
					graphics.FillRectangle(brush, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
					brush.Dispose();
				}
				else
				{
					graphics.FillRectangle(SystemBrushes.Control, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
				}
				graphics.Dispose();
				m.Result = (IntPtr)1;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0002B310 File Offset: 0x00029510
		private void WmParentNotify(ref Message m)
		{
			base.WndProc(ref m);
			if ((int)((long)m.WParam) == 65536001)
			{
				this.dropDownHandle = m.LParam;
				if (AccessibilityImprovements.Level3)
				{
					if (this.childDropDown != null)
					{
						this.ReleaseUiaProvider(this.childDropDown.Handle);
						this.childDropDown.ReleaseHandle();
					}
					this.childDropDown = new ComboBox.ComboBoxChildNativeWindow(this, ComboBox.ChildWindowType.DropDownList);
					this.childDropDown.AssignHandle(this.dropDownHandle);
					this.childListAccessibleObject = null;
				}
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002B394 File Offset: 0x00029594
		private void WmReflectCommand(ref Message m)
		{
			switch (NativeMethods.Util.HIWORD(m.WParam))
			{
			case 1:
				this.UpdateText();
				this.OnSelectedIndexChanged(EventArgs.Empty);
				return;
			case 2:
			case 3:
			case 4:
				break;
			case 5:
				this.OnTextChanged(EventArgs.Empty);
				return;
			case 6:
				this.OnTextUpdate(EventArgs.Empty);
				return;
			case 7:
				this.currentText = this.Text;
				this.dropDown = true;
				this.OnDropDown(EventArgs.Empty);
				this.UpdateDropDownHeight();
				return;
			case 8:
				this.OnDropDownClosed(EventArgs.Empty);
				if (base.FormattingEnabled && this.Text != this.currentText && this.dropDown)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
				this.dropDown = false;
				return;
			case 9:
				this.OnSelectionChangeCommittedInternal(EventArgs.Empty);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002B478 File Offset: 0x00029678
		private void WmReflectDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr intPtr = Control.SetUpPalette(drawitemstruct.hDC, false, false);
			try
			{
				Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC);
				try
				{
					this.OnDrawItem(new DrawItemEventArgs(graphics, this.Font, Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom), drawitemstruct.itemID, (DrawItemState)drawitemstruct.itemState, this.ForeColor, this.BackColor));
				}
				finally
				{
					graphics.Dispose();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectPalette(new HandleRef(this, drawitemstruct.hDC), new HandleRef(null, intPtr), 0);
				}
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002B56C File Offset: 0x0002976C
		private void WmReflectMeasureItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			if (this.DrawMode == DrawMode.OwnerDrawVariable && measureitemstruct.itemID >= 0)
			{
				Graphics graphics = base.CreateGraphicsInternal();
				MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, measureitemstruct.itemID, this.ItemHeight);
				this.OnMeasureItem(measureItemEventArgs);
				measureitemstruct.itemHeight = measureItemEventArgs.ItemHeight;
				graphics.Dispose();
			}
			else
			{
				measureitemstruct.itemHeight = this.ItemHeight;
			}
			Marshal.StructureToPtr(measureitemstruct, m.LParam, false);
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002B5FC File Offset: 0x000297FC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 130)
			{
				if (msg <= 20)
				{
					if (msg <= 8)
					{
						if (msg != 7)
						{
							if (msg != 8)
							{
								goto IL_547;
							}
						}
						else
						{
							try
							{
								this.fireSetFocus = false;
								base.WndProc(ref m);
								return;
							}
							finally
							{
								this.fireSetFocus = true;
							}
						}
						try
						{
							this.fireLostFocus = false;
							base.WndProc(ref m);
							if (!Application.RenderWithVisualStyles && !base.GetStyle(ControlStyles.UserPaint) && this.DropDownStyle == ComboBoxStyle.DropDownList && (this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup))
							{
								UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 675, 0, 0);
							}
							return;
						}
						finally
						{
							this.fireLostFocus = true;
						}
					}
					else
					{
						if (msg == 15)
						{
							if (!base.GetStyle(ControlStyles.UserPaint) && (this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup))
							{
								using (WindowsRegion windowsRegion = new WindowsRegion(this.FlatComboBoxAdapter.dropDownRect))
								{
									using (WindowsRegion windowsRegion2 = new WindowsRegion(base.Bounds))
									{
										NativeMethods.RegionFlags updateRgn = (NativeMethods.RegionFlags)SafeNativeMethods.GetUpdateRgn(new HandleRef(this, base.Handle), new HandleRef(this, windowsRegion2.HRegion), true);
										windowsRegion.CombineRegion(windowsRegion2, windowsRegion, RegionCombineMode.DIFF);
										Rectangle updateRegionBox = windowsRegion2.ToRectangle();
										this.FlatComboBoxAdapter.ValidateOwnerDrawRegions(this, updateRegionBox);
										NativeMethods.PAINTSTRUCT paintstruct = default(NativeMethods.PAINTSTRUCT);
										bool flag = false;
										IntPtr intPtr;
										if (m.WParam == IntPtr.Zero)
										{
											intPtr = UnsafeNativeMethods.BeginPaint(new HandleRef(this, base.Handle), ref paintstruct);
											flag = true;
										}
										else
										{
											intPtr = m.WParam;
										}
										using (DeviceContext deviceContext = DeviceContext.FromHdc(intPtr))
										{
											using (WindowsGraphics windowsGraphics = new WindowsGraphics(deviceContext))
											{
												if (updateRgn != NativeMethods.RegionFlags.ERROR)
												{
													windowsGraphics.DeviceContext.SetClip(windowsRegion);
												}
												m.WParam = intPtr;
												this.DefWndProc(ref m);
												if (updateRgn != NativeMethods.RegionFlags.ERROR)
												{
													windowsGraphics.DeviceContext.SetClip(windowsRegion2);
												}
												using (Graphics graphics = Graphics.FromHdcInternal(intPtr))
												{
													this.FlatComboBoxAdapter.DrawFlatCombo(this, graphics);
												}
											}
										}
										if (flag)
										{
											UnsafeNativeMethods.EndPaint(new HandleRef(this, base.Handle), ref paintstruct);
										}
										return;
									}
								}
							}
							base.WndProc(ref m);
							return;
						}
						if (msg != 20)
						{
							goto IL_547;
						}
						this.WmEraseBkgnd(ref m);
						return;
					}
				}
				else if (msg <= 48)
				{
					if (msg == 32)
					{
						base.WndProc(ref m);
						return;
					}
					if (msg != 48)
					{
						goto IL_547;
					}
					if (base.Width == 0)
					{
						this.suppressNextWindosPos = true;
					}
					base.WndProc(ref m);
					return;
				}
				else
				{
					if (msg == 71)
					{
						if (!this.suppressNextWindosPos)
						{
							base.WndProc(ref m);
						}
						this.suppressNextWindosPos = false;
						return;
					}
					if (msg != 130)
					{
						goto IL_547;
					}
					base.WndProc(ref m);
					this.ReleaseChildWindow();
					return;
				}
			}
			else if (msg <= 528)
			{
				if (msg <= 513)
				{
					if (msg - 307 > 1)
					{
						if (msg != 513)
						{
							goto IL_547;
						}
						this.mouseEvents = true;
						base.WndProc(ref m);
						return;
					}
				}
				else if (msg != 514)
				{
					if (msg != 528)
					{
						goto IL_547;
					}
					this.WmParentNotify(ref m);
					return;
				}
				else
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
					Rectangle rectangle = new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
					int x = NativeMethods.Util.SignedLOWORD(m.LParam);
					int y = NativeMethods.Util.SignedHIWORD(m.LParam);
					Point point = new Point(x, y);
					point = base.PointToScreen(point);
					if (this.mouseEvents && !base.ValidationCancelled)
					{
						this.mouseEvents = false;
						bool capture = base.Capture;
						if (capture && rectangle.Contains(point))
						{
							this.OnClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						}
						base.WndProc(ref m);
						return;
					}
					base.CaptureInternal = false;
					this.DefWndProc(ref m);
					return;
				}
			}
			else if (msg <= 792)
			{
				if (msg == 675)
				{
					this.DefWndProc(ref m);
					this.OnMouseLeaveInternal(EventArgs.Empty);
					return;
				}
				if (msg != 792)
				{
					goto IL_547;
				}
				if ((!base.GetStyle(ControlStyles.UserPaint) && this.FlatStyle == FlatStyle.Flat) || this.FlatStyle == FlatStyle.Popup)
				{
					this.DefWndProc(ref m);
					if (((int)((long)m.LParam) & 4) == 4)
					{
						if ((!base.GetStyle(ControlStyles.UserPaint) && this.FlatStyle == FlatStyle.Flat) || this.FlatStyle == FlatStyle.Popup)
						{
							using (Graphics graphics2 = Graphics.FromHdcInternal(m.WParam))
							{
								this.FlatComboBoxAdapter.DrawFlatCombo(this, graphics2);
							}
						}
						return;
					}
				}
				base.WndProc(ref m);
				return;
			}
			else
			{
				if (msg == 8235)
				{
					this.WmReflectDrawItem(ref m);
					return;
				}
				if (msg == 8236)
				{
					this.WmReflectMeasureItem(ref m);
					return;
				}
				if (msg != 8465)
				{
					goto IL_547;
				}
				this.WmReflectCommand(ref m);
				return;
			}
			m.Result = this.InitializeDCForWmCtlColor(m.WParam, m.Msg);
			return;
			IL_547:
			if (m.Msg == NativeMethods.WM_MOUSEENTER)
			{
				this.DefWndProc(ref m);
				this.OnMouseEnterInternal(EventArgs.Empty);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0002BC3C File Offset: 0x00029E3C
		private ComboBox.FlatComboAdapter FlatComboBoxAdapter
		{
			get
			{
				ComboBox.FlatComboAdapter flatComboAdapter = base.Properties.GetObject(ComboBox.PropFlatComboAdapter) as ComboBox.FlatComboAdapter;
				if (flatComboAdapter == null || !flatComboAdapter.IsValid(this))
				{
					flatComboAdapter = this.CreateFlatComboAdapterInstance();
					base.Properties.SetObject(ComboBox.PropFlatComboAdapter, flatComboAdapter);
				}
				return flatComboAdapter;
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002BC84 File Offset: 0x00029E84
		internal virtual ComboBox.FlatComboAdapter CreateFlatComboAdapterInstance()
		{
			return new ComboBox.FlatComboAdapter(this, false);
		}

		// Token: 0x040007B3 RID: 1971
		private static readonly object EVENT_DROPDOWN = new object();

		// Token: 0x040007B4 RID: 1972
		private static readonly object EVENT_DRAWITEM = new object();

		// Token: 0x040007B5 RID: 1973
		private static readonly object EVENT_MEASUREITEM = new object();

		// Token: 0x040007B6 RID: 1974
		private static readonly object EVENT_SELECTEDINDEXCHANGED = new object();

		// Token: 0x040007B7 RID: 1975
		private static readonly object EVENT_SELECTIONCHANGECOMMITTED = new object();

		// Token: 0x040007B8 RID: 1976
		private static readonly object EVENT_SELECTEDITEMCHANGED = new object();

		// Token: 0x040007B9 RID: 1977
		private static readonly object EVENT_DROPDOWNSTYLE = new object();

		// Token: 0x040007BA RID: 1978
		private static readonly object EVENT_TEXTUPDATE = new object();

		// Token: 0x040007BB RID: 1979
		private static readonly object EVENT_DROPDOWNCLOSED = new object();

		// Token: 0x040007BC RID: 1980
		private static readonly int PropMaxLength = PropertyStore.CreateKey();

		// Token: 0x040007BD RID: 1981
		private static readonly int PropItemHeight = PropertyStore.CreateKey();

		// Token: 0x040007BE RID: 1982
		private static readonly int PropDropDownWidth = PropertyStore.CreateKey();

		// Token: 0x040007BF RID: 1983
		private static readonly int PropDropDownHeight = PropertyStore.CreateKey();

		// Token: 0x040007C0 RID: 1984
		private static readonly int PropStyle = PropertyStore.CreateKey();

		// Token: 0x040007C1 RID: 1985
		private static readonly int PropDrawMode = PropertyStore.CreateKey();

		// Token: 0x040007C2 RID: 1986
		private static readonly int PropMatchingText = PropertyStore.CreateKey();

		// Token: 0x040007C3 RID: 1987
		private static readonly int PropFlatComboAdapter = PropertyStore.CreateKey();

		// Token: 0x040007C4 RID: 1988
		private const int DefaultSimpleStyleHeight = 150;

		// Token: 0x040007C5 RID: 1989
		private const int DefaultDropDownHeight = 106;

		// Token: 0x040007C6 RID: 1990
		private const int AutoCompleteTimeout = 10000000;

		// Token: 0x040007C7 RID: 1991
		private bool autoCompleteDroppedDown;

		// Token: 0x040007C8 RID: 1992
		private FlatStyle flatStyle = FlatStyle.Standard;

		// Token: 0x040007C9 RID: 1993
		private int updateCount;

		// Token: 0x040007CA RID: 1994
		private long autoCompleteTimeStamp;

		// Token: 0x040007CB RID: 1995
		private int selectedIndex = -1;

		// Token: 0x040007CC RID: 1996
		private bool allowCommit = true;

		// Token: 0x040007CD RID: 1997
		private int requestedHeight;

		// Token: 0x040007CE RID: 1998
		private ComboBox.ComboBoxChildNativeWindow childDropDown;

		// Token: 0x040007CF RID: 1999
		private ComboBox.ComboBoxChildNativeWindow childEdit;

		// Token: 0x040007D0 RID: 2000
		private ComboBox.ComboBoxChildNativeWindow childListBox;

		// Token: 0x040007D1 RID: 2001
		private IntPtr dropDownHandle;

		// Token: 0x040007D2 RID: 2002
		private ComboBox.ObjectCollection itemsCollection;

		// Token: 0x040007D3 RID: 2003
		private short prefHeightCache = -1;

		// Token: 0x040007D4 RID: 2004
		private short maxDropDownItems = 8;

		// Token: 0x040007D5 RID: 2005
		private bool integralHeight = true;

		// Token: 0x040007D6 RID: 2006
		private bool mousePressed;

		// Token: 0x040007D7 RID: 2007
		private bool mouseEvents;

		// Token: 0x040007D8 RID: 2008
		private bool mouseInEdit;

		// Token: 0x040007D9 RID: 2009
		private bool sorted;

		// Token: 0x040007DA RID: 2010
		private bool fireSetFocus = true;

		// Token: 0x040007DB RID: 2011
		private bool fireLostFocus = true;

		// Token: 0x040007DC RID: 2012
		private bool mouseOver;

		// Token: 0x040007DD RID: 2013
		private bool suppressNextWindosPos;

		// Token: 0x040007DE RID: 2014
		private bool canFireLostFocus;

		// Token: 0x040007DF RID: 2015
		private string currentText = "";

		// Token: 0x040007E0 RID: 2016
		private string lastTextChangedValue;

		// Token: 0x040007E1 RID: 2017
		private bool dropDown;

		// Token: 0x040007E2 RID: 2018
		private ComboBox.AutoCompleteDropDownFinder finder = new ComboBox.AutoCompleteDropDownFinder();

		// Token: 0x040007E3 RID: 2019
		private bool selectedValueChangedFired;

		// Token: 0x040007E4 RID: 2020
		private AutoCompleteMode autoCompleteMode;

		// Token: 0x040007E5 RID: 2021
		private AutoCompleteSource autoCompleteSource = AutoCompleteSource.None;

		// Token: 0x040007E6 RID: 2022
		private AutoCompleteStringCollection autoCompleteCustomSource;

		// Token: 0x040007E7 RID: 2023
		private StringSource stringSource;

		// Token: 0x040007E8 RID: 2024
		private bool fromHandleCreate;

		// Token: 0x040007E9 RID: 2025
		private ComboBox.ComboBoxChildListUiaProvider childListAccessibleObject;

		// Token: 0x040007EA RID: 2026
		private ComboBox.ComboBoxChildEditUiaProvider childEditAccessibleObject;

		// Token: 0x040007EB RID: 2027
		private ComboBox.ComboBoxChildTextUiaProvider childTextAccessibleObject;

		// Token: 0x040007EC RID: 2028
		private bool dropDownWillBeClosed;

		// Token: 0x02000624 RID: 1572
		[ComVisible(true)]
		internal class ComboBoxChildNativeWindow : NativeWindow
		{
			// Token: 0x06006353 RID: 25427 RVA: 0x0016EF74 File Offset: 0x0016D174
			public ComboBoxChildNativeWindow(ComboBox comboBox, ComboBox.ChildWindowType childWindowType)
			{
				this._owner = comboBox;
				this._childWindowType = childWindowType;
			}

			// Token: 0x06006354 RID: 25428 RVA: 0x0016EF8C File Offset: 0x0016D18C
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg != 2)
				{
					if (msg != 61)
					{
						if (msg != 512)
						{
							if (this._childWindowType == ComboBox.ChildWindowType.DropDownList)
							{
								base.DefWndProc(ref m);
								return;
							}
							this._owner.ChildWndProc(ref m);
						}
						else
						{
							if (this._childWindowType != ComboBox.ChildWindowType.DropDownList)
							{
								this._owner.ChildWndProc(ref m);
								return;
							}
							object selectedItem = this._owner.SelectedItem;
							base.DefWndProc(ref m);
							object selectedItem2 = this._owner.SelectedItem;
							if (selectedItem != selectedItem2)
							{
								(this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider).SetComboBoxItemFocus();
								return;
							}
						}
						return;
					}
					this.WmGetObject(ref m);
					return;
				}
				else
				{
					if (AccessibilityImprovements.Level3 && LocalAppContextSwitches.DisconnectUiaProvidersOnWmDestroy && (this._childWindowType == ComboBox.ChildWindowType.ListBox || this._childWindowType == ComboBox.ChildWindowType.DropDownList))
					{
						if (base.Handle != IntPtr.Zero)
						{
							UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, base.Handle), IntPtr.Zero, IntPtr.Zero, null);
						}
						if (this._accessibilityObject != null && ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
						{
							int num = UnsafeNativeMethods.UiaDisconnectProvider(this._accessibilityObject);
						}
					}
					if (this._childWindowType == ComboBox.ChildWindowType.DropDownList)
					{
						base.DefWndProc(ref m);
						return;
					}
					this._owner.ChildWndProc(ref m);
					return;
				}
			}

			// Token: 0x06006355 RID: 25429 RVA: 0x0016F0BE File Offset: 0x0016D2BE
			private ComboBox.ChildAccessibleObject GetChildAccessibleObject(ComboBox.ChildWindowType childWindowType)
			{
				if (childWindowType == ComboBox.ChildWindowType.Edit)
				{
					return this._owner.ChildEditAccessibleObject;
				}
				if (childWindowType == ComboBox.ChildWindowType.ListBox || childWindowType == ComboBox.ChildWindowType.DropDownList)
				{
					return this._owner.ChildListAccessibleObject;
				}
				return new ComboBox.ChildAccessibleObject(this._owner, base.Handle);
			}

			// Token: 0x06006356 RID: 25430 RVA: 0x0016F0F4 File Offset: 0x0016D2F4
			private void WmGetObject(ref Message m)
			{
				if (AccessibilityImprovements.Level3 && m.LParam == (IntPtr)(-25) && (this._childWindowType == ComboBox.ChildWindowType.ListBox || this._childWindowType == ComboBox.ChildWindowType.DropDownList))
				{
					if (this._accessibilityObject == null)
					{
						this._accessibilityObject = Control.CreateInternalAccessibleObject(this.GetChildAccessibleObject(this._childWindowType));
					}
					m.Result = UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, base.Handle), m.WParam, m.LParam, this._accessibilityObject);
					return;
				}
				if (-4 == (int)((long)m.LParam))
				{
					Guid guid = new Guid("{618736E0-3C3D-11CF-810C-00AA00389B71}");
					try
					{
						if (this._accessibilityObject == null)
						{
							AccessibleObject obj = AccessibilityImprovements.Level3 ? this.GetChildAccessibleObject(this._childWindowType) : new ComboBox.ChildAccessibleObject(this._owner, base.Handle);
							this._accessibilityObject = Control.CreateInternalAccessibleObject(obj);
						}
						IntPtr iunknownForObject = Marshal.GetIUnknownForObject(this._accessibilityObject);
						IntSecurity.UnmanagedCode.Assert();
						try
						{
							m.Result = UnsafeNativeMethods.LresultFromObject(ref guid, m.WParam, new HandleRef(this, iunknownForObject));
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
							Marshal.Release(iunknownForObject);
						}
						return;
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException(SR.GetString("RichControlLresult"), innerException);
					}
				}
				base.DefWndProc(ref m);
			}

			// Token: 0x0400392D RID: 14637
			private ComboBox _owner;

			// Token: 0x0400392E RID: 14638
			private InternalAccessibleObject _accessibilityObject;

			// Token: 0x0400392F RID: 14639
			private ComboBox.ChildWindowType _childWindowType;
		}

		// Token: 0x02000625 RID: 1573
		private sealed class ItemComparer : IComparer
		{
			// Token: 0x06006357 RID: 25431 RVA: 0x0016F248 File Offset: 0x0016D448
			public ItemComparer(ComboBox comboBox)
			{
				this.comboBox = comboBox;
			}

			// Token: 0x06006358 RID: 25432 RVA: 0x0016F258 File Offset: 0x0016D458
			public int Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					string itemText = this.comboBox.GetItemText(item1);
					string itemText2 = this.comboBox.GetItemText(item2);
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					return compareInfo.Compare(itemText, itemText2, CompareOptions.StringSort);
				}
			}

			// Token: 0x04003930 RID: 14640
			private ComboBox comboBox;
		}

		// Token: 0x02000626 RID: 1574
		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006359 RID: 25433 RVA: 0x0016F2A6 File Offset: 0x0016D4A6
			public ObjectCollection(ComboBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001535 RID: 5429
			// (get) Token: 0x0600635A RID: 25434 RVA: 0x0016F2B5 File Offset: 0x0016D4B5
			private IComparer Comparer
			{
				get
				{
					if (this.comparer == null)
					{
						this.comparer = new ComboBox.ItemComparer(this.owner);
					}
					return this.comparer;
				}
			}

			// Token: 0x17001536 RID: 5430
			// (get) Token: 0x0600635B RID: 25435 RVA: 0x0016F2D6 File Offset: 0x0016D4D6
			private ArrayList InnerList
			{
				get
				{
					if (this.innerList == null)
					{
						this.innerList = new ArrayList();
					}
					return this.innerList;
				}
			}

			// Token: 0x17001537 RID: 5431
			// (get) Token: 0x0600635C RID: 25436 RVA: 0x0016F2F1 File Offset: 0x0016D4F1
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			// Token: 0x17001538 RID: 5432
			// (get) Token: 0x0600635D RID: 25437 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001539 RID: 5433
			// (get) Token: 0x0600635E RID: 25438 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700153A RID: 5434
			// (get) Token: 0x0600635F RID: 25439 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700153B RID: 5435
			// (get) Token: 0x06006360 RID: 25440 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006361 RID: 25441 RVA: 0x0016F300 File Offset: 0x0016D500
			public int Add(object item)
			{
				this.owner.CheckNoDataSource();
				int result = this.AddInternal(item);
				if (this.owner.UpdateNeeded() && this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, false);
				}
				return result;
			}

			// Token: 0x06006362 RID: 25442 RVA: 0x0016F350 File Offset: 0x0016D550
			private int AddInternal(object item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int num = -1;
				if (!this.owner.sorted)
				{
					this.InnerList.Add(item);
				}
				else
				{
					num = this.InnerList.BinarySearch(item, this.Comparer);
					if (num < 0)
					{
						num = ~num;
					}
					this.InnerList.Insert(num, item);
				}
				bool flag = false;
				try
				{
					if (this.owner.sorted)
					{
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeInsert(num, item);
						}
					}
					else
					{
						num = this.InnerList.Count - 1;
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeAdd(item);
						}
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.InnerList.Remove(item);
					}
				}
				if (flag && this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						comboBoxUiaProvider.InsertToItemsCollection(num, item, this.InnerList.Count);
					}
				}
				return num;
			}

			// Token: 0x06006363 RID: 25443 RVA: 0x0016F470 File Offset: 0x0016D670
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			// Token: 0x06006364 RID: 25444 RVA: 0x0016F47C File Offset: 0x0016D67C
			public void AddRange(object[] items)
			{
				this.owner.CheckNoDataSource();
				this.owner.BeginUpdate();
				try
				{
					this.AddRangeInternal(items);
				}
				finally
				{
					this.owner.EndUpdate();
				}
			}

			// Token: 0x06006365 RID: 25445 RVA: 0x0016F4C4 File Offset: 0x0016D6C4
			internal void AddRangeInternal(IList items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				foreach (object item in items)
				{
					this.AddInternal(item);
				}
				if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, false);
				}
			}

			// Token: 0x1700153C RID: 5436
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerList.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerList[index];
				}
				set
				{
					this.owner.CheckNoDataSource();
					this.SetItemInternal(index, value);
				}
			}

			// Token: 0x06006368 RID: 25448 RVA: 0x0016F5B6 File Offset: 0x0016D7B6
			public void Clear()
			{
				this.owner.CheckNoDataSource();
				this.ClearInternal();
			}

			// Token: 0x06006369 RID: 25449 RVA: 0x0016F5CC File Offset: 0x0016D7CC
			internal void ClearInternal()
			{
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeClear();
				}
				this.InnerList.Clear();
				this.owner.selectedIndex = -1;
				if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, true);
				}
				if (this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider == null)
					{
						return;
					}
					comboBoxUiaProvider.ResetListItemAccessibleObjects();
				}
			}

			// Token: 0x0600636A RID: 25450 RVA: 0x0016F65B File Offset: 0x0016D85B
			public bool Contains(object value)
			{
				return this.IndexOf(value) != -1;
			}

			// Token: 0x0600636B RID: 25451 RVA: 0x0016F66A File Offset: 0x0016D86A
			public void CopyTo(object[] destination, int arrayIndex)
			{
				this.InnerList.CopyTo(destination, arrayIndex);
			}

			// Token: 0x0600636C RID: 25452 RVA: 0x0016F66A File Offset: 0x0016D86A
			void ICollection.CopyTo(Array destination, int index)
			{
				this.InnerList.CopyTo(destination, index);
			}

			// Token: 0x0600636D RID: 25453 RVA: 0x0016F679 File Offset: 0x0016D879
			public IEnumerator GetEnumerator()
			{
				return this.InnerList.GetEnumerator();
			}

			// Token: 0x0600636E RID: 25454 RVA: 0x0016F686 File Offset: 0x0016D886
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerList.IndexOf(value);
			}

			// Token: 0x0600636F RID: 25455 RVA: 0x0016F6A4 File Offset: 0x0016D8A4
			public void Insert(int index, object item)
			{
				this.owner.CheckNoDataSource();
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (index < 0 || index > this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.sorted)
				{
					this.Add(item);
					return;
				}
				this.InnerList.Insert(index, item);
				if (this.owner.IsHandleCreated)
				{
					bool flag = false;
					try
					{
						this.owner.NativeInsert(index, item);
						flag = true;
					}
					finally
					{
						if (flag)
						{
							if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
							{
								this.owner.SetAutoComplete(false, false);
							}
							if (this.owner.IsAccessibilityObjectCreated)
							{
								ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
								if (comboBoxUiaProvider != null)
								{
									comboBoxUiaProvider.InsertToItemsCollection(index, item, this.InnerList.Count);
								}
							}
						}
						else
						{
							this.InnerList.RemoveAt(index);
						}
					}
				}
			}

			// Token: 0x06006370 RID: 25456 RVA: 0x0016F7C8 File Offset: 0x0016D9C8
			public void RemoveAt(int index)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index >= this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeRemoveAt(index);
				}
				this.InnerList.RemoveAt(index);
				if (this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						comboBoxUiaProvider.RemoveFromItemsCollection(index, this.InnerList.Count);
					}
				}
				if (!this.owner.IsHandleCreated && index < this.owner.selectedIndex)
				{
					this.owner.selectedIndex--;
				}
				if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
				{
					this.owner.SetAutoComplete(false, false);
				}
			}

			// Token: 0x06006371 RID: 25457 RVA: 0x0016F8D8 File Offset: 0x0016DAD8
			public void Remove(object value)
			{
				int num = this.InnerList.IndexOf(value);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006372 RID: 25458 RVA: 0x0016F900 File Offset: 0x0016DB00
			internal void SetItemInternal(int index, object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (index < 0 || index >= this.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerList[index] = value;
				if (this.owner.IsHandleCreated && this.owner.IsAccessibilityObjectCreated)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this.owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						comboBoxUiaProvider.SetItemInternal(index, value, this.InnerList.Count);
					}
				}
				if (this.owner.IsHandleCreated)
				{
					bool flag = index == this.owner.SelectedIndex;
					if (string.Compare(this.owner.GetItemText(value), this.owner.NativeGetItemText(index), true, CultureInfo.CurrentCulture) != 0)
					{
						this.owner.NativeRemoveAt(index);
						this.owner.NativeInsert(index, value);
						if (flag)
						{
							this.owner.SelectedIndex = index;
							this.owner.UpdateText();
						}
						if (this.owner.AutoCompleteSource == AutoCompleteSource.ListItems)
						{
							this.owner.SetAutoComplete(false, false);
							return;
						}
					}
					else if (flag)
					{
						this.owner.OnSelectedItemChanged(EventArgs.Empty);
						this.owner.OnSelectedIndexChanged(EventArgs.Empty);
					}
				}
			}

			// Token: 0x04003931 RID: 14641
			private ComboBox owner;

			// Token: 0x04003932 RID: 14642
			private ArrayList innerList;

			// Token: 0x04003933 RID: 14643
			private IComparer comparer;
		}

		// Token: 0x02000627 RID: 1575
		[ComVisible(true)]
		public class ChildAccessibleObject : AccessibleObject
		{
			// Token: 0x06006373 RID: 25459 RVA: 0x0016FA69 File Offset: 0x0016DC69
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public ChildAccessibleObject(ComboBox owner, IntPtr handle)
			{
				this.Owner = owner;
				base.UseStdAccessibleObjects(handle);
			}

			// Token: 0x1700153D RID: 5437
			// (get) Token: 0x06006374 RID: 25460 RVA: 0x0016FA7F File Offset: 0x0016DC7F
			// (set) Token: 0x06006375 RID: 25461 RVA: 0x0016FA87 File Offset: 0x0016DC87
			internal ComboBox Owner { get; private set; }

			// Token: 0x06006376 RID: 25462 RVA: 0x0016FA90 File Offset: 0x0016DC90
			internal void ClearOwner()
			{
				this.Owner = null;
			}

			// Token: 0x1700153E RID: 5438
			// (get) Token: 0x06006377 RID: 25463 RVA: 0x0016FA99 File Offset: 0x0016DC99
			public override string Name
			{
				get
				{
					ComboBox owner = this.Owner;
					if (owner == null)
					{
						return null;
					}
					return owner.AccessibilityObject.Name;
				}
			}
		}

		// Token: 0x02000628 RID: 1576
		[ComVisible(true)]
		internal class ComboBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006378 RID: 25464 RVA: 0x0009B963 File Offset: 0x00099B63
			public ComboBoxAccessibleObject(Control ownerControl) : base(ownerControl)
			{
			}

			// Token: 0x06006379 RID: 25465 RVA: 0x0016FAB1 File Offset: 0x0016DCB1
			internal override string get_accNameInternal(object childID)
			{
				base.ValidateChildID(ref childID);
				if (childID != null && (int)childID == 1)
				{
					return this.Name;
				}
				return base.get_accNameInternal(childID);
			}

			// Token: 0x0600637A RID: 25466 RVA: 0x0016FAD5 File Offset: 0x0016DCD5
			internal override string get_accKeyboardShortcutInternal(object childID)
			{
				base.ValidateChildID(ref childID);
				if (childID != null && (int)childID == 1)
				{
					return this.KeyboardShortcut;
				}
				return base.get_accKeyboardShortcutInternal(childID);
			}

			// Token: 0x04003935 RID: 14645
			private const int COMBOBOX_ACC_ITEM_INDEX = 1;
		}

		// Token: 0x02000629 RID: 1577
		[ComVisible(true)]
		internal class ComboBoxExAccessibleObject : ComboBox.ComboBoxAccessibleObject
		{
			// Token: 0x0600637B RID: 25467 RVA: 0x0016FAF9 File Offset: 0x0016DCF9
			private void ComboBoxDefaultAction(bool expand)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				if (this.ownerItem.DroppedDown != expand)
				{
					this.ownerItem.DroppedDown = expand;
				}
			}

			// Token: 0x0600637C RID: 25468 RVA: 0x0016FB1E File Offset: 0x0016DD1E
			public ComboBoxExAccessibleObject(ComboBox ownerControl) : base(ownerControl)
			{
				this.ownerItem = ownerControl;
			}

			// Token: 0x0600637D RID: 25469 RVA: 0x0016FB2E File Offset: 0x0016DD2E
			internal override void ClearOwnerControlInternal()
			{
				this.ownerItem = null;
				base.ClearOwnerControlInternal();
			}

			// Token: 0x0600637E RID: 25470 RVA: 0x0016FB3D File Offset: 0x0016DD3D
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed() && (this.ownerItem != null || base.IsIAccessibleExSupported());
			}

			// Token: 0x0600637F RID: 25471 RVA: 0x0016FB5C File Offset: 0x0016DD5C
			internal override bool IsPatternSupported(int patternId)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return false;
				}
				if (patternId == 10005)
				{
					return this.ownerItem.DropDownStyle != ComboBoxStyle.Simple;
				}
				if (patternId == 10002)
				{
					return this.ownerItem.DropDownStyle != ComboBoxStyle.DropDownList || AccessibilityImprovements.Level3;
				}
				return base.IsPatternSupported(patternId);
			}

			// Token: 0x1700153F RID: 5439
			// (get) Token: 0x06006380 RID: 25472 RVA: 0x0016FBB4 File Offset: 0x0016DDB4
			internal override int[] RuntimeId
			{
				get
				{
					if (this.ownerItem != null)
					{
						return new int[]
						{
							42,
							(int)((long)this.ownerItem.Handle),
							this.ownerItem.GetHashCode()
						};
					}
					return base.RuntimeId;
				}
			}

			// Token: 0x06006381 RID: 25473 RVA: 0x0016FC00 File Offset: 0x0016DE00
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID == 30028)
				{
					return this.IsPatternSupported(10005);
				}
				if (propertyID != 30043)
				{
					return base.GetPropertyValue(propertyID);
				}
				return this.IsPatternSupported(10002);
			}

			// Token: 0x06006382 RID: 25474 RVA: 0x0016FC57 File Offset: 0x0016DE57
			internal override void Expand()
			{
				this.ComboBoxDefaultAction(true);
			}

			// Token: 0x06006383 RID: 25475 RVA: 0x0016FC60 File Offset: 0x0016DE60
			internal override void Collapse()
			{
				this.ComboBoxDefaultAction(false);
			}

			// Token: 0x17001540 RID: 5440
			// (get) Token: 0x06006384 RID: 25476 RVA: 0x0016FC69 File Offset: 0x0016DE69
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (base.IsOwnerControlDestroyed() || !this.ownerItem.DroppedDown)
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return UnsafeNativeMethods.ExpandCollapseState.Expanded;
				}
			}

			// Token: 0x04003936 RID: 14646
			private ComboBox ownerItem;
		}

		// Token: 0x0200062A RID: 1578
		[ComVisible(true)]
		internal class ComboBoxItemAccessibleObject : AccessibleObject
		{
			// Token: 0x06006385 RID: 25477 RVA: 0x0016FC83 File Offset: 0x0016DE83
			public ComboBoxItemAccessibleObject(ComboBox owningComboBox, object owningItem)
			{
				this._owningComboBox = owningComboBox;
				this._owningItem = owningItem;
				this._systemIAccessible = this._owningComboBox.ChildListAccessibleObject.GetSystemIAccessibleInternal();
			}

			// Token: 0x06006386 RID: 25478 RVA: 0x0016FCAF File Offset: 0x0016DEAF
			internal void ClearOwnerComboBox()
			{
				this._owningComboBox = null;
				this._owningItem = null;
			}

			// Token: 0x06006387 RID: 25479 RVA: 0x0016FCBF File Offset: 0x0016DEBF
			private bool IsOwnerComboBoxDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this._owningComboBox == null;
			}

			// Token: 0x17001541 RID: 5441
			// (get) Token: 0x06006388 RID: 25480 RVA: 0x0016FCD4 File Offset: 0x0016DED4
			public override Rectangle Bounds
			{
				get
				{
					if (this.IsOwnerComboBoxDestroyed())
					{
						return Rectangle.Empty;
					}
					int currentIndex = this.GetCurrentIndex();
					IntPtr listHandle = this._owningComboBox.GetListHandle();
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					if ((int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, listHandle), 408, currentIndex, ref rect)) == -1)
					{
						return Rectangle.Empty;
					}
					UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, listHandle), NativeMethods.NullHandleRef, ref rect, 2);
					return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
				}
			}

			// Token: 0x17001542 RID: 5442
			// (get) Token: 0x06006389 RID: 25481 RVA: 0x0016FD5F File Offset: 0x0016DF5F
			public override string DefaultAction
			{
				get
				{
					return this._systemIAccessible.get_accDefaultAction(this.GetChildId());
				}
			}

			// Token: 0x0600638A RID: 25482 RVA: 0x0016FD78 File Offset: 0x0016DF78
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.IsOwnerComboBoxDestroyed())
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this._owningComboBox.ChildListAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					if (!this._owningComboBox.IsHandleCreated)
					{
						return null;
					}
					int currentIndex = this.GetCurrentIndex();
					ComboBox.ComboBoxChildListUiaProvider comboBoxChildListUiaProvider = this._owningComboBox.ChildListAccessibleObject as ComboBox.ComboBoxChildListUiaProvider;
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owningComboBox.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (currentIndex >= 0 && comboBoxChildListUiaProvider != null && comboBoxUiaProvider != null)
					{
						int count = comboBoxUiaProvider.ItemsAccessibleObjects.Count;
						int num = currentIndex + 1;
						if (num < count)
						{
							return comboBoxUiaProvider.ItemsAccessibleObjects[num];
						}
					}
					break;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					if (!this._owningComboBox.IsHandleCreated)
					{
						return null;
					}
					int currentIndex = this.GetCurrentIndex();
					ComboBox.ComboBoxChildListUiaProvider comboBoxChildListUiaProvider = this._owningComboBox.ChildListAccessibleObject as ComboBox.ComboBoxChildListUiaProvider;
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owningComboBox.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (currentIndex > 0 && comboBoxChildListUiaProvider != null && comboBoxUiaProvider != null)
					{
						int count2 = comboBoxUiaProvider.ItemsAccessibleObjects.Count;
						int num2 = currentIndex - 1;
						if (num2 < count2)
						{
							return comboBoxUiaProvider.ItemsAccessibleObjects[num2];
						}
					}
					break;
				}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x17001543 RID: 5443
			// (get) Token: 0x0600638B RID: 25483 RVA: 0x0016FE90 File Offset: 0x0016E090
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (this.IsOwnerComboBoxDestroyed())
					{
						return null;
					}
					return this._owningComboBox.AccessibilityObject;
				}
			}

			// Token: 0x0600638C RID: 25484 RVA: 0x0016FEA8 File Offset: 0x0016E0A8
			private int GetCurrentIndex()
			{
				if (this.IsOwnerComboBoxDestroyed() || !this._owningComboBox.IsHandleCreated)
				{
					return -1;
				}
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owningComboBox.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (comboBoxUiaProvider != null)
				{
					return comboBoxUiaProvider.ItemsAccessibleObjects.IndexOf(this);
				}
				return -1;
			}

			// Token: 0x0600638D RID: 25485 RVA: 0x0016FEEE File Offset: 0x0016E0EE
			internal override int GetChildId()
			{
				return this.GetCurrentIndex() + 1;
			}

			// Token: 0x0600638E RID: 25486 RVA: 0x0016FEF8 File Offset: 0x0016E0F8
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30035)
				{
					switch (propertyID)
					{
					case 30000:
						return this.RuntimeId;
					case 30001:
						return this.BoundingRectangle;
					case 30002:
					case 30004:
					case 30006:
					case 30011:
					case 30012:
					case 30014:
					case 30015:
					case 30018:
					case 30020:
					case 30021:
						break;
					case 30003:
						return 50007;
					case 30005:
						return this.Name;
					case 30007:
						return this.KeyboardShortcut ?? string.Empty;
					case 30008:
						return !this.IsOwnerComboBoxDestroyed() && this._owningComboBox.Focused && this._owningComboBox.SelectedIndex == this.GetCurrentIndex();
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
						return !this.IsOwnerComboBoxDestroyed() && this._owningComboBox.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					case 30016:
						return true;
					case 30017:
						return true;
					case 30019:
						return false;
					case 30022:
						return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
					default:
						if (propertyID == 30035)
						{
							return true;
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30036)
					{
						return true;
					}
					if (propertyID == 30079)
					{
						return (this.State & AccessibleStates.Selected) > AccessibleStates.None;
					}
					if (propertyID == 30080)
					{
						if (!this.IsOwnerComboBoxDestroyed())
						{
							return this._owningComboBox.ChildListAccessibleObject;
						}
						return null;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001544 RID: 5444
			// (get) Token: 0x0600638F RID: 25487 RVA: 0x001700C0 File Offset: 0x0016E2C0
			public override string Help
			{
				get
				{
					return this._systemIAccessible.get_accHelp(this.GetChildId());
				}
			}

			// Token: 0x06006390 RID: 25488 RVA: 0x001700D8 File Offset: 0x0016E2D8
			internal override bool IsPatternSupported(int patternId)
			{
				return !this.IsOwnerComboBoxDestroyed() && (patternId == 10018 || patternId == 10000 || patternId == 10017 || patternId == 10010 || base.IsPatternSupported(patternId));
			}

			// Token: 0x17001545 RID: 5445
			// (get) Token: 0x06006391 RID: 25489 RVA: 0x0017010D File Offset: 0x0016E30D
			// (set) Token: 0x06006392 RID: 25490 RVA: 0x0017012F File Offset: 0x0016E32F
			public override string Name
			{
				get
				{
					if (this._owningComboBox != null)
					{
						return this._owningComboBox.GetItemText(this._owningItem);
					}
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x17001546 RID: 5446
			// (get) Token: 0x06006393 RID: 25491 RVA: 0x00170138 File Offset: 0x0016E338
			public override AccessibleRole Role
			{
				get
				{
					return (AccessibleRole)this._systemIAccessible.get_accRole(this.GetChildId());
				}
			}

			// Token: 0x17001547 RID: 5447
			// (get) Token: 0x06006394 RID: 25492 RVA: 0x00170158 File Offset: 0x0016E358
			internal override int[] RuntimeId
			{
				get
				{
					int[] array = new int[5];
					array[0] = 42;
					if (this.IsOwnerComboBoxDestroyed())
					{
						array[1] = 0;
						array[2] = 0;
						array[3] = 0;
					}
					else
					{
						array[1] = (int)((long)this._owningComboBox.Handle);
						array[2] = this._owningComboBox.GetListNativeWindowRuntimeIdPart();
						array[3] = this._owningItem.GetHashCode();
					}
					array[4] = this.GetCurrentIndex();
					return array;
				}
			}

			// Token: 0x06006395 RID: 25493 RVA: 0x001701C1 File Offset: 0x0016E3C1
			internal void SetItemInternal(object newValue)
			{
				this._owningItem = newValue;
			}

			// Token: 0x06006396 RID: 25494 RVA: 0x001701CC File Offset: 0x0016E3CC
			internal override void ScrollIntoView()
			{
				if (this.IsOwnerComboBoxDestroyed() || !this._owningComboBox.IsHandleCreated || !this._owningComboBox.Enabled)
				{
					return;
				}
				if (this._owningComboBox.ChildListAccessibleObject.BoundingRectangle.IntersectsWith(this.Bounds))
				{
					return;
				}
				this._owningComboBox.SendMessage(348, this.GetCurrentIndex(), 0);
			}

			// Token: 0x17001548 RID: 5448
			// (get) Token: 0x06006397 RID: 25495 RVA: 0x00170235 File Offset: 0x0016E435
			public override AccessibleStates State
			{
				get
				{
					return (AccessibleStates)this._systemIAccessible.get_accState(this.GetChildId());
				}
			}

			// Token: 0x06006398 RID: 25496 RVA: 0x00170252 File Offset: 0x0016E452
			internal override void SetFocus()
			{
				base.RaiseAutomationEvent(20005);
				base.SetFocus();
			}

			// Token: 0x06006399 RID: 25497 RVA: 0x00170266 File Offset: 0x0016E466
			internal override void SelectItem()
			{
				if (this.IsOwnerComboBoxDestroyed())
				{
					return;
				}
				this._owningComboBox.SelectedIndex = this.GetCurrentIndex();
				SafeNativeMethods.InvalidateRect(new HandleRef(this, this._owningComboBox.GetListHandle()), null, false);
			}

			// Token: 0x0600639A RID: 25498 RVA: 0x000174F5 File Offset: 0x000156F5
			internal override void AddToSelection()
			{
				this.SelectItem();
			}

			// Token: 0x0600639B RID: 25499 RVA: 0x000072B6 File Offset: 0x000054B6
			internal override void RemoveFromSelection()
			{
			}

			// Token: 0x17001549 RID: 5449
			// (get) Token: 0x0600639C RID: 25500 RVA: 0x0017029B File Offset: 0x0016E49B
			internal override bool IsItemSelected
			{
				get
				{
					return (this.State & AccessibleStates.Selected) > AccessibleStates.None;
				}
			}

			// Token: 0x1700154A RID: 5450
			// (get) Token: 0x0600639D RID: 25501 RVA: 0x001702A8 File Offset: 0x0016E4A8
			internal override UnsafeNativeMethods.IRawElementProviderSimple ItemSelectionContainer
			{
				get
				{
					if (!this.IsOwnerComboBoxDestroyed())
					{
						return this._owningComboBox.ChildListAccessibleObject;
					}
					return null;
				}
			}

			// Token: 0x04003937 RID: 14647
			private ComboBox _owningComboBox;

			// Token: 0x04003938 RID: 14648
			private object _owningItem;

			// Token: 0x04003939 RID: 14649
			private IAccessible _systemIAccessible;
		}

		// Token: 0x0200062B RID: 1579
		[ComVisible(true)]
		internal class ComboBoxUiaProvider : ComboBox.ComboBoxExAccessibleObject
		{
			// Token: 0x0600639E RID: 25502 RVA: 0x001702BF File Offset: 0x0016E4BF
			public ComboBoxUiaProvider(ComboBox owningComboBox) : base(owningComboBox)
			{
				this._owningComboBox = owningComboBox;
			}

			// Token: 0x0600639F RID: 25503 RVA: 0x001702D0 File Offset: 0x0016E4D0
			internal override void ClearOwnerControlInternal()
			{
				ComboBox.ComboBoxChildDropDownButtonUiaProvider dropDownButtonUiaProvider = this._dropDownButtonUiaProvider;
				if (dropDownButtonUiaProvider != null)
				{
					dropDownButtonUiaProvider.ClearOwnerComboBox();
				}
				foreach (ComboBox.ComboBoxItemAccessibleObject comboBoxItemAccessibleObject in this.ItemsAccessibleObjects)
				{
					comboBoxItemAccessibleObject.ClearOwnerComboBox();
				}
				this.ResetListItemAccessibleObjects();
				this._owningComboBox = null;
				base.ClearOwnerControlInternal();
			}

			// Token: 0x1700154B RID: 5451
			// (get) Token: 0x060063A0 RID: 25504 RVA: 0x00170348 File Offset: 0x0016E548
			internal List<ComboBox.ComboBoxItemAccessibleObject> ItemsAccessibleObjects
			{
				get
				{
					if (this.IsItemsCollectionCreated)
					{
						return this._itemAccessibleObjects;
					}
					this._itemAccessibleObjects = new List<ComboBox.ComboBoxItemAccessibleObject>();
					if (base.IsOwnerControlDestroyed())
					{
						return this._itemAccessibleObjects;
					}
					foreach (object owningItem in this._owningComboBox.Items)
					{
						this._itemAccessibleObjects.Add(new ComboBox.ComboBoxItemAccessibleObject(this._owningComboBox, owningItem));
					}
					return this._itemAccessibleObjects;
				}
			}

			// Token: 0x1700154C RID: 5452
			// (get) Token: 0x060063A1 RID: 25505 RVA: 0x001703E0 File Offset: 0x0016E5E0
			private bool IsItemsCollectionCreated
			{
				get
				{
					return this._itemAccessibleObjects != null;
				}
			}

			// Token: 0x060063A2 RID: 25506 RVA: 0x001703EB File Offset: 0x0016E5EB
			internal void InsertToItemsCollection(int index, object item, int total)
			{
				if (!base.IsOwnerControlDestroyed() && this.IsItemsCollectionCreated && this._itemAccessibleObjects.Count == total - 1)
				{
					this._itemAccessibleObjects.Insert(index, new ComboBox.ComboBoxItemAccessibleObject(this._owningComboBox, item));
				}
			}

			// Token: 0x060063A3 RID: 25507 RVA: 0x00170425 File Offset: 0x0016E625
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && (patternId == 10018 || base.IsPatternSupported(patternId));
			}

			// Token: 0x1700154D RID: 5453
			// (get) Token: 0x060063A4 RID: 25508 RVA: 0x00170444 File Offset: 0x0016E644
			public ComboBox.ComboBoxChildDropDownButtonUiaProvider DropDownButtonUiaProvider
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					if (this._dropDownButtonUiaProvider == null && this._owningComboBox.IsHandleCreated)
					{
						this._dropDownButtonUiaProvider = new ComboBox.ComboBoxChildDropDownButtonUiaProvider(this._owningComboBox, this._owningComboBox.Handle);
					}
					return this._dropDownButtonUiaProvider;
				}
			}

			// Token: 0x060063A5 RID: 25509 RVA: 0x00170494 File Offset: 0x0016E694
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					return this.GetChildFragment(0);
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					int childFragmentCount = this.GetChildFragmentCount();
					if (childFragmentCount > 0)
					{
						return this.GetChildFragment(childFragmentCount - 1);
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x1700154E RID: 5454
			// (get) Token: 0x060063A6 RID: 25510 RVA: 0x001704D0 File Offset: 0x0016E6D0
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					ToolStripControlHost toolStripControlHost = base.Owner.ToolStripControlHost;
					ToolStrip toolStrip = (toolStripControlHost != null) ? toolStripControlHost.Owner : null;
					if (toolStrip != null && toolStrip.IsHandleCreated)
					{
						return toolStrip.AccessibilityObject;
					}
					return this;
				}
			}

			// Token: 0x060063A7 RID: 25511 RVA: 0x00170514 File Offset: 0x0016E714
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetOverrideProviderForHwnd(IntPtr hwnd)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (hwnd == this._owningComboBox.childEdit.Handle)
				{
					return this._owningComboBox.ChildEditAccessibleObject;
				}
				if (hwnd == this._owningComboBox.childListBox.Handle || hwnd == this._owningComboBox.dropDownHandle)
				{
					return this._owningComboBox.ChildListAccessibleObject;
				}
				return null;
			}

			// Token: 0x060063A8 RID: 25512 RVA: 0x00170588 File Offset: 0x0016E788
			internal AccessibleObject GetChildFragment(int index)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (this._owningComboBox.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					if (index == 0)
					{
						return this._owningComboBox.ChildTextAccessibleObject;
					}
					index--;
				}
				if (index == 0 && this._owningComboBox.DropDownStyle != ComboBoxStyle.Simple)
				{
					return this.DropDownButtonUiaProvider;
				}
				return null;
			}

			// Token: 0x060063A9 RID: 25513 RVA: 0x001705DC File Offset: 0x0016E7DC
			internal int GetChildFragmentCount()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return 0;
				}
				int num = 0;
				if (this._owningComboBox.DropDownStyle == ComboBoxStyle.DropDownList)
				{
					num++;
				}
				if (this._owningComboBox.DropDownStyle != ComboBoxStyle.Simple)
				{
					num++;
				}
				return num;
			}

			// Token: 0x060063AA RID: 25514 RVA: 0x0017061C File Offset: 0x0016E81C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50003;
				}
				if (propertyID == 30008)
				{
					return !base.IsOwnerControlDestroyed() && this._owningComboBox.Focused;
				}
				if (propertyID != 30020)
				{
					return base.GetPropertyValue(propertyID);
				}
				return base.IsOwnerControlDestroyed() ? IntPtr.Zero : this._owningComboBox.Handle;
			}

			// Token: 0x060063AB RID: 25515 RVA: 0x00170691 File Offset: 0x0016E891
			internal void RemoveFromItemsCollection(int index, int total)
			{
				if (this.IsItemsCollectionCreated && this._itemAccessibleObjects.Count == total + 1)
				{
					this._itemAccessibleObjects.RemoveAt(index);
				}
			}

			// Token: 0x060063AC RID: 25516 RVA: 0x001706B7 File Offset: 0x0016E8B7
			internal void ResetListItemAccessibleObjects()
			{
				List<ComboBox.ComboBoxItemAccessibleObject> itemAccessibleObjects = this._itemAccessibleObjects;
				if (itemAccessibleObjects != null)
				{
					itemAccessibleObjects.Clear();
				}
				this._itemAccessibleObjects = null;
			}

			// Token: 0x060063AD RID: 25517 RVA: 0x001706D4 File Offset: 0x0016E8D4
			internal void SetComboBoxItemFocus()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				int selectedIndex = this._owningComboBox.SelectedIndex;
				if (selectedIndex < 0 || selectedIndex >= this.ItemsAccessibleObjects.Count)
				{
					return;
				}
				this.ItemsAccessibleObjects[selectedIndex].SetFocus();
			}

			// Token: 0x060063AE RID: 25518 RVA: 0x0017071C File Offset: 0x0016E91C
			internal void SetComboBoxItemSelection()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				int selectedIndex = this._owningComboBox.SelectedIndex;
				if (selectedIndex < 0 || selectedIndex >= this.ItemsAccessibleObjects.Count)
				{
					return;
				}
				this.ItemsAccessibleObjects[selectedIndex].RaiseAutomationEvent(20012);
			}

			// Token: 0x060063AF RID: 25519 RVA: 0x00170768 File Offset: 0x0016E968
			internal void SetItemInternal(int index, object value, int total)
			{
				if (this.IsItemsCollectionCreated && this._itemAccessibleObjects.Count == total)
				{
					this._itemAccessibleObjects[index].SetItemInternal(value);
				}
			}

			// Token: 0x060063B0 RID: 25520 RVA: 0x00170792 File Offset: 0x0016E992
			internal override void SetFocus()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				base.SetFocus();
				base.RaiseAutomationEvent(20005);
			}

			// Token: 0x0400393A RID: 14650
			private ComboBox.ComboBoxChildDropDownButtonUiaProvider _dropDownButtonUiaProvider;

			// Token: 0x0400393B RID: 14651
			private List<ComboBox.ComboBoxItemAccessibleObject> _itemAccessibleObjects;

			// Token: 0x0400393C RID: 14652
			private ComboBox _owningComboBox;
		}

		// Token: 0x0200062C RID: 1580
		[ComVisible(true)]
		internal class ComboBoxChildEditUiaProvider : ComboBox.ChildAccessibleObject
		{
			// Token: 0x060063B1 RID: 25521 RVA: 0x001707AF File Offset: 0x0016E9AF
			public ComboBoxChildEditUiaProvider(ComboBox owner, IntPtr childEditControlhandle) : base(owner, childEditControlhandle)
			{
				this._handle = childEditControlhandle;
			}

			// Token: 0x060063B2 RID: 25522 RVA: 0x001707C0 File Offset: 0x0016E9C0
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.Owner == null)
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return base.Owner.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					if (base.Owner.DropDownStyle == ComboBoxStyle.Simple)
					{
						return null;
					}
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.Owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						int childFragmentCount = comboBoxUiaProvider.GetChildFragmentCount();
						if (childFragmentCount > 1)
						{
							return comboBoxUiaProvider.GetChildFragment(childFragmentCount - 1);
						}
					}
					return null;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.Owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						AccessibleObject childFragment = comboBoxUiaProvider.GetChildFragment(0);
						if (this.RuntimeId != childFragment.RuntimeId)
						{
							return childFragment;
						}
					}
					return null;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x1700154F RID: 5455
			// (get) Token: 0x060063B3 RID: 25523 RVA: 0x00170866 File Offset: 0x0016EA66
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ComboBox owner = base.Owner;
					if (owner == null)
					{
						return null;
					}
					return owner.AccessibilityObject;
				}
			}

			// Token: 0x060063B4 RID: 25524 RVA: 0x0017087C File Offset: 0x0016EA7C
			internal override object GetPropertyValue(int propertyID)
			{
				switch (propertyID)
				{
				case 30000:
					return this.RuntimeId;
				case 30001:
					return this.Bounds;
				case 30003:
					return 50004;
				case 30005:
					return this.Name;
				case 30007:
					return string.Empty;
				case 30008:
				{
					ComboBox owner = base.Owner;
					return (owner != null) ? new bool?(owner.Focused) : null;
				}
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
				{
					ComboBox owner2 = base.Owner;
					return (owner2 != null) ? new bool?(owner2.Enabled) : null;
				}
				case 30011:
					return "1001";
				case 30013:
					return this.Help ?? string.Empty;
				case 30019:
					return false;
				case 30020:
					return this._handle;
				case 30022:
					return false;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001550 RID: 5456
			// (get) Token: 0x060063B5 RID: 25525 RVA: 0x001709BC File Offset: 0x0016EBBC
			internal override UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
			{
				get
				{
					if (AccessibilityImprovements.Level3)
					{
						UnsafeNativeMethods.IRawElementProviderSimple result;
						UnsafeNativeMethods.UiaHostProviderFromHwnd(new HandleRef(this, this._handle), out result);
						return result;
					}
					return base.HostRawElementProvider;
				}
			}

			// Token: 0x060063B6 RID: 25526 RVA: 0x00013062 File Offset: 0x00011262
			internal override bool IsIAccessibleExSupported()
			{
				return true;
			}

			// Token: 0x17001551 RID: 5457
			// (get) Token: 0x060063B7 RID: 25527 RVA: 0x00013062 File Offset: 0x00011262
			internal override int ProviderOptions
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x17001552 RID: 5458
			// (get) Token: 0x060063B8 RID: 25528 RVA: 0x001709EC File Offset: 0x0016EBEC
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						this.GetHashCode()
					};
				}
			}

			// Token: 0x0400393D RID: 14653
			private const string COMBO_BOX_EDIT_AUTOMATION_ID = "1001";

			// Token: 0x0400393E RID: 14654
			private IntPtr _handle;
		}

		// Token: 0x0200062D RID: 1581
		[ComVisible(true)]
		internal class ComboBoxChildListUiaProvider : ComboBox.ChildAccessibleObject
		{
			// Token: 0x060063B9 RID: 25529 RVA: 0x00170A0F File Offset: 0x0016EC0F
			public ComboBoxChildListUiaProvider(ComboBox owner, IntPtr childListControlhandle) : base(owner, childListControlhandle)
			{
				this._childListControlhandle = childListControlhandle;
			}

			// Token: 0x060063BA RID: 25530 RVA: 0x00170A20 File Offset: 0x0016EC20
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				if (AccessibilityImprovements.Level3)
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					if (systemIAccessibleInternal != null)
					{
						object obj = systemIAccessibleInternal.accHitTest((int)x, (int)y);
						if (obj is int)
						{
							int num = (int)obj;
							return this.GetChildFragment(num - 1);
						}
						return null;
					}
				}
				return base.ElementProviderFromPoint(x, y);
			}

			// Token: 0x060063BB RID: 25531 RVA: 0x00170A6C File Offset: 0x0016EC6C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					return this.GetChildFragment(0);
				}
				if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return base.FragmentNavigate(direction);
				}
				int childFragmentCount = this.GetChildFragmentCount();
				if (childFragmentCount > 0)
				{
					return this.GetChildFragment(childFragmentCount - 1);
				}
				return null;
			}

			// Token: 0x17001553 RID: 5459
			// (get) Token: 0x060063BC RID: 25532 RVA: 0x00170866 File Offset: 0x0016EA66
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ComboBox owner = base.Owner;
					if (owner == null)
					{
						return null;
					}
					return owner.AccessibilityObject;
				}
			}

			// Token: 0x060063BD RID: 25533 RVA: 0x00170AAC File Offset: 0x0016ECAC
			public AccessibleObject GetChildFragment(int index)
			{
				if (base.Owner == null || !base.Owner.IsHandleCreated)
				{
					return null;
				}
				ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = base.Owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
				if (index < 0 || index >= base.Owner.Items.Count || comboBoxUiaProvider == null || index >= comboBoxUiaProvider.ItemsAccessibleObjects.Count)
				{
					return null;
				}
				return comboBoxUiaProvider.ItemsAccessibleObjects[index];
			}

			// Token: 0x060063BE RID: 25534 RVA: 0x00170B17 File Offset: 0x0016ED17
			public int GetChildFragmentCount()
			{
				if (base.Owner == null)
				{
					return 0;
				}
				return base.Owner.Items.Count;
			}

			// Token: 0x060063BF RID: 25535 RVA: 0x00170B34 File Offset: 0x0016ED34
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30037)
				{
					switch (propertyID)
					{
					case 30000:
						return this.RuntimeId;
					case 30001:
						return this.Bounds;
					case 30002:
					case 30004:
					case 30006:
					case 30012:
					case 30014:
					case 30015:
					case 30016:
					case 30017:
					case 30018:
					case 30021:
						break;
					case 30003:
						return 50008;
					case 30005:
						return this.Name;
					case 30007:
						return string.Empty;
					case 30008:
						return false;
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
					{
						ComboBox owner = base.Owner;
						return (owner != null) ? new bool?(owner.Enabled) : null;
					}
					case 30011:
						return "1000";
					case 30013:
						return this.Help ?? string.Empty;
					case 30019:
						return false;
					case 30020:
						return this._childListControlhandle;
					case 30022:
						return false;
					default:
						if (propertyID == 30037)
						{
							return true;
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30060)
					{
						return this.CanSelectMultiple;
					}
					if (propertyID == 30061)
					{
						return this.IsSelectionRequired;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x060063C0 RID: 25536 RVA: 0x000F17D2 File Offset: 0x000EF9D2
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				return this.GetFocused();
			}

			// Token: 0x060063C1 RID: 25537 RVA: 0x00170CA4 File Offset: 0x0016EEA4
			public override AccessibleObject GetFocused()
			{
				if (base.Owner == null)
				{
					return null;
				}
				int selectedIndex = base.Owner.SelectedIndex;
				return this.GetChildFragment(selectedIndex);
			}

			// Token: 0x060063C2 RID: 25538 RVA: 0x00170CD0 File Offset: 0x0016EED0
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetSelection()
			{
				if (base.Owner == null)
				{
					return null;
				}
				int selectedIndex = base.Owner.SelectedIndex;
				AccessibleObject childFragment = this.GetChildFragment(selectedIndex);
				if (childFragment != null)
				{
					return new UnsafeNativeMethods.IRawElementProviderSimple[]
					{
						childFragment
					};
				}
				return new UnsafeNativeMethods.IRawElementProviderSimple[0];
			}

			// Token: 0x17001554 RID: 5460
			// (get) Token: 0x060063C3 RID: 25539 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal override bool CanSelectMultiple
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001555 RID: 5461
			// (get) Token: 0x060063C4 RID: 25540 RVA: 0x00013062 File Offset: 0x00011262
			internal override bool IsSelectionRequired
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060063C5 RID: 25541 RVA: 0x00170D0F File Offset: 0x0016EF0F
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || patternId == 10001 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001556 RID: 5462
			// (get) Token: 0x060063C6 RID: 25542 RVA: 0x00170D2C File Offset: 0x0016EF2C
			internal override UnsafeNativeMethods.IRawElementProviderSimple HostRawElementProvider
			{
				get
				{
					if (AccessibilityImprovements.Level3)
					{
						UnsafeNativeMethods.IRawElementProviderSimple result;
						UnsafeNativeMethods.UiaHostProviderFromHwnd(new HandleRef(this, this._childListControlhandle), out result);
						return result;
					}
					return base.HostRawElementProvider;
				}
			}

			// Token: 0x17001557 RID: 5463
			// (get) Token: 0x060063C7 RID: 25543 RVA: 0x00170D5C File Offset: 0x0016EF5C
			internal override int[] RuntimeId
			{
				get
				{
					if (base.Owner == null)
					{
						return new int[0];
					}
					return new int[]
					{
						42,
						(int)((long)base.Owner.Handle),
						base.Owner.GetListNativeWindowRuntimeIdPart()
					};
				}
			}

			// Token: 0x17001558 RID: 5464
			// (get) Token: 0x060063C8 RID: 25544 RVA: 0x00170DA8 File Offset: 0x0016EFA8
			public override AccessibleStates State
			{
				get
				{
					if (base.Owner == null)
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable;
					if (base.Owner.Focused)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x0400393F RID: 14655
			private const string COMBO_BOX_LIST_AUTOMATION_ID = "1000";

			// Token: 0x04003940 RID: 14656
			private IntPtr _childListControlhandle;
		}

		// Token: 0x0200062E RID: 1582
		[ComVisible(true)]
		internal class ComboBoxChildTextUiaProvider : AccessibleObject
		{
			// Token: 0x060063C9 RID: 25545 RVA: 0x00170DD7 File Offset: 0x0016EFD7
			public ComboBoxChildTextUiaProvider(ComboBox owner)
			{
				this._owner = owner;
			}

			// Token: 0x060063CA RID: 25546 RVA: 0x00170DE6 File Offset: 0x0016EFE6
			internal void ClearOwnerComboBox()
			{
				this._owner = null;
			}

			// Token: 0x060063CB RID: 25547 RVA: 0x00170DEF File Offset: 0x0016EFEF
			private bool IsOwnerComboBoxDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this._owner == null;
			}

			// Token: 0x17001559 RID: 5465
			// (get) Token: 0x060063CC RID: 25548 RVA: 0x00170E03 File Offset: 0x0016F003
			public override Rectangle Bounds
			{
				get
				{
					if (this.IsOwnerComboBoxDestroyed())
					{
						return Rectangle.Empty;
					}
					return this._owner.AccessibilityObject.Bounds;
				}
			}

			// Token: 0x060063CD RID: 25549 RVA: 0x00013062 File Offset: 0x00011262
			internal override int GetChildId()
			{
				return 1;
			}

			// Token: 0x1700155A RID: 5466
			// (get) Token: 0x060063CE RID: 25550 RVA: 0x00170E23 File Offset: 0x0016F023
			// (set) Token: 0x060063CF RID: 25551 RVA: 0x000072B6 File Offset: 0x000054B6
			public override string Name
			{
				get
				{
					if (this.IsOwnerComboBoxDestroyed())
					{
						return string.Empty;
					}
					return this._owner.AccessibilityObject.Name ?? string.Empty;
				}
				set
				{
				}
			}

			// Token: 0x060063D0 RID: 25552 RVA: 0x00170E4C File Offset: 0x0016F04C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.IsOwnerComboBoxDestroyed())
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this._owner.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						int childFragmentCount = comboBoxUiaProvider.GetChildFragmentCount();
						if (childFragmentCount > 1)
						{
							return comboBoxUiaProvider.GetChildFragment(childFragmentCount - 1);
						}
					}
					return null;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						AccessibleObject childFragment = comboBoxUiaProvider.GetChildFragment(0);
						if (this.RuntimeId != childFragment.RuntimeId)
						{
							return childFragment;
						}
					}
					return null;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x1700155B RID: 5467
			// (get) Token: 0x060063D1 RID: 25553 RVA: 0x00170EE3 File Offset: 0x0016F0E3
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (!this.IsOwnerComboBoxDestroyed())
					{
						return this._owner.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x060063D2 RID: 25554 RVA: 0x00170EFC File Offset: 0x0016F0FC
			internal override object GetPropertyValue(int propertyID)
			{
				switch (propertyID)
				{
				case 30000:
					return this.RuntimeId;
				case 30001:
					return this.Bounds;
				case 30002:
				case 30004:
				case 30006:
				case 30011:
				case 30012:
					break;
				case 30003:
					return 50020;
				case 30005:
					return this.Name;
				case 30007:
					return string.Empty;
				case 30008:
					return !this.IsOwnerComboBoxDestroyed() && this._owner.Focused;
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
					return !this.IsOwnerComboBoxDestroyed() && this._owner.Enabled;
				case 30013:
					return this.Help ?? string.Empty;
				default:
					if (propertyID == 30019 || propertyID == 30022)
					{
						return false;
					}
					break;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x1700155C RID: 5468
			// (get) Token: 0x060063D3 RID: 25555 RVA: 0x00171004 File Offset: 0x0016F204
			internal override int[] RuntimeId
			{
				get
				{
					int[] array = new int[5];
					array[0] = 42;
					if (this.IsOwnerComboBoxDestroyed())
					{
						array[1] = 0;
						array[2] = 0;
					}
					else
					{
						array[1] = (int)((long)this._owner.Handle);
						array[2] = this._owner.GetHashCode();
					}
					array[3] = this.GetHashCode();
					array[4] = this.GetChildId();
					return array;
				}
			}

			// Token: 0x1700155D RID: 5469
			// (get) Token: 0x060063D4 RID: 25556 RVA: 0x00171064 File Offset: 0x0016F264
			public override AccessibleStates State
			{
				get
				{
					if (this.IsOwnerComboBoxDestroyed())
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable;
					if (this._owner.Focused)
					{
						accessibleStates |= AccessibleStates.Focused;
					}
					return accessibleStates;
				}
			}

			// Token: 0x04003941 RID: 14657
			private const int COMBOBOX_TEXT_ACC_ITEM_INDEX = 1;

			// Token: 0x04003942 RID: 14658
			private ComboBox _owner;
		}

		// Token: 0x0200062F RID: 1583
		[ComVisible(true)]
		internal class ComboBoxChildDropDownButtonUiaProvider : AccessibleObject
		{
			// Token: 0x060063D5 RID: 25557 RVA: 0x00171093 File Offset: 0x0016F293
			public ComboBoxChildDropDownButtonUiaProvider(ComboBox owner, IntPtr comboBoxControlhandle)
			{
				this._owner = owner;
				base.UseStdAccessibleObjects(comboBoxControlhandle);
			}

			// Token: 0x060063D6 RID: 25558 RVA: 0x001710A9 File Offset: 0x0016F2A9
			internal void ClearOwnerComboBox()
			{
				this._owner = null;
			}

			// Token: 0x060063D7 RID: 25559 RVA: 0x001710B2 File Offset: 0x0016F2B2
			private bool IsOwnerComboBoxDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this._owner == null;
			}

			// Token: 0x1700155E RID: 5470
			// (get) Token: 0x060063D8 RID: 25560 RVA: 0x001710C6 File Offset: 0x0016F2C6
			// (set) Token: 0x060063D9 RID: 25561 RVA: 0x001710F4 File Offset: 0x0016F2F4
			public override string Name
			{
				get
				{
					if (this.IsOwnerComboBoxDestroyed())
					{
						return string.Empty;
					}
					return SR.GetString(this._owner.DroppedDown ? "ComboboxDropDownButtonCloseName" : "ComboboxDropDownButtonOpenName");
				}
				set
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					systemIAccessibleInternal.set_accName(2, value);
				}
			}

			// Token: 0x1700155F RID: 5471
			// (get) Token: 0x060063DA RID: 25562 RVA: 0x00171118 File Offset: 0x0016F318
			public override Rectangle Bounds
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					int x;
					int y;
					int width;
					int height;
					systemIAccessibleInternal.accLocation(out x, out y, out width, out height, 2);
					return new Rectangle(x, y, width, height);
				}
			}

			// Token: 0x17001560 RID: 5472
			// (get) Token: 0x060063DB RID: 25563 RVA: 0x0017114C File Offset: 0x0016F34C
			public override string DefaultAction
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accDefaultAction(2);
				}
			}

			// Token: 0x060063DC RID: 25564 RVA: 0x0017116C File Offset: 0x0016F36C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.IsOwnerComboBoxDestroyed())
				{
					return null;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent)
				{
					return this._owner.AccessibilityObject;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ComboBox.ComboBoxUiaProvider comboBoxUiaProvider = this._owner.AccessibilityObject as ComboBox.ComboBoxUiaProvider;
					if (comboBoxUiaProvider != null)
					{
						int childFragmentCount = comboBoxUiaProvider.GetChildFragmentCount();
						if (childFragmentCount > 1)
						{
							return comboBoxUiaProvider.GetChildFragment(childFragmentCount - 1);
						}
					}
					return null;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x17001561 RID: 5473
			// (get) Token: 0x060063DD RID: 25565 RVA: 0x001711C8 File Offset: 0x0016F3C8
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (!this.IsOwnerComboBoxDestroyed())
					{
						return this._owner.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x060063DE RID: 25566 RVA: 0x0001627D File Offset: 0x0001447D
			internal override int GetChildId()
			{
				return 2;
			}

			// Token: 0x060063DF RID: 25567 RVA: 0x001711E0 File Offset: 0x0016F3E0
			internal override object GetPropertyValue(int propertyID)
			{
				switch (propertyID)
				{
				case 30000:
					return this.RuntimeId;
				case 30001:
					return this.BoundingRectangle;
				case 30002:
				case 30004:
				case 30006:
				case 30011:
				case 30012:
					break;
				case 30003:
					return 50000;
				case 30005:
					return this.Name;
				case 30007:
					return this.KeyboardShortcut;
				case 30008:
					return !this.IsOwnerComboBoxDestroyed() && this._owner.Focused;
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
					return !this.IsOwnerComboBoxDestroyed() && this._owner.Enabled;
				case 30013:
					return this.Help ?? string.Empty;
				default:
					if (propertyID == 30019)
					{
						return false;
					}
					if (propertyID == 30022)
					{
						return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
					}
					break;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x17001562 RID: 5474
			// (get) Token: 0x060063E0 RID: 25568 RVA: 0x00171300 File Offset: 0x0016F500
			public override string Help
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accHelp(2);
				}
			}

			// Token: 0x17001563 RID: 5475
			// (get) Token: 0x060063E1 RID: 25569 RVA: 0x00171320 File Offset: 0x0016F520
			public override string KeyboardShortcut
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accKeyboardShortcut(2);
				}
			}

			// Token: 0x060063E2 RID: 25570 RVA: 0x00171340 File Offset: 0x0016F540
			internal override bool IsPatternSupported(int patternId)
			{
				return !this.IsOwnerComboBoxDestroyed() && (patternId == 10018 || patternId == 10000 || base.IsPatternSupported(patternId));
			}

			// Token: 0x17001564 RID: 5476
			// (get) Token: 0x060063E3 RID: 25571 RVA: 0x00171368 File Offset: 0x0016F568
			public override AccessibleRole Role
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleRole)systemIAccessibleInternal.get_accRole(2);
				}
			}

			// Token: 0x17001565 RID: 5477
			// (get) Token: 0x060063E4 RID: 25572 RVA: 0x00171390 File Offset: 0x0016F590
			internal override int[] RuntimeId
			{
				get
				{
					int[] array = new int[5];
					array[0] = 42;
					if (!this.IsOwnerComboBoxDestroyed())
					{
						array[1] = (int)((long)this._owner.Handle);
						array[2] = this._owner.GetHashCode();
					}
					else
					{
						array[1] = 0;
						array[2] = 0;
					}
					array[3] = 61453;
					array[4] = 2;
					return array;
				}
			}

			// Token: 0x17001566 RID: 5478
			// (get) Token: 0x060063E5 RID: 25573 RVA: 0x001713EC File Offset: 0x0016F5EC
			public override AccessibleStates State
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleStates)systemIAccessibleInternal.get_accState(2);
				}
			}

			// Token: 0x04003943 RID: 14659
			private const int COMBOBOX_DROPDOWN_BUTTON_ACC_ITEM_INDEX = 2;

			// Token: 0x04003944 RID: 14660
			private ComboBox _owner;
		}

		// Token: 0x02000630 RID: 1584
		private sealed class ACNativeWindow : NativeWindow
		{
			// Token: 0x060063E6 RID: 25574 RVA: 0x00171411 File Offset: 0x0016F611
			internal ACNativeWindow(IntPtr acHandle)
			{
				base.AssignHandle(acHandle);
				ComboBox.ACNativeWindow.ACWindows.Add(acHandle, this);
				UnsafeNativeMethods.EnumChildWindows(new HandleRef(this, acHandle), new NativeMethods.EnumChildrenCallback(ComboBox.ACNativeWindow.RegisterACWindowRecursive), NativeMethods.NullHandleRef);
			}

			// Token: 0x060063E7 RID: 25575 RVA: 0x00171450 File Offset: 0x0016F650
			private static bool RegisterACWindowRecursive(IntPtr handle, IntPtr lparam)
			{
				if (!ComboBox.ACNativeWindow.ACWindows.ContainsKey(handle))
				{
					ComboBox.ACNativeWindow acnativeWindow = new ComboBox.ACNativeWindow(handle);
				}
				return true;
			}

			// Token: 0x17001567 RID: 5479
			// (get) Token: 0x060063E8 RID: 25576 RVA: 0x00171477 File Offset: 0x0016F677
			internal bool Visible
			{
				get
				{
					return SafeNativeMethods.IsWindowVisible(new HandleRef(this, base.Handle));
				}
			}

			// Token: 0x17001568 RID: 5480
			// (get) Token: 0x060063E9 RID: 25577 RVA: 0x0017148C File Offset: 0x0016F68C
			internal static bool AutoCompleteActive
			{
				get
				{
					if (ComboBox.ACNativeWindow.inWndProcCnt > 0)
					{
						return true;
					}
					foreach (object obj in ComboBox.ACNativeWindow.ACWindows.Values)
					{
						ComboBox.ACNativeWindow acnativeWindow = obj as ComboBox.ACNativeWindow;
						if (acnativeWindow != null && acnativeWindow.Visible)
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x060063EA RID: 25578 RVA: 0x00171504 File Offset: 0x0016F704
			protected override void WndProc(ref Message m)
			{
				ComboBox.ACNativeWindow.inWndProcCnt++;
				try
				{
					base.WndProc(ref m);
				}
				finally
				{
					ComboBox.ACNativeWindow.inWndProcCnt--;
				}
				if (m.Msg == 130)
				{
					ComboBox.ACNativeWindow.ACWindows.Remove(base.Handle);
				}
			}

			// Token: 0x060063EB RID: 25579 RVA: 0x00171568 File Offset: 0x0016F768
			internal static void RegisterACWindow(IntPtr acHandle, bool subclass)
			{
				if (subclass && ComboBox.ACNativeWindow.ACWindows.ContainsKey(acHandle) && ComboBox.ACNativeWindow.ACWindows[acHandle] == null)
				{
					ComboBox.ACNativeWindow.ACWindows.Remove(acHandle);
				}
				if (!ComboBox.ACNativeWindow.ACWindows.ContainsKey(acHandle))
				{
					if (subclass)
					{
						ComboBox.ACNativeWindow acnativeWindow = new ComboBox.ACNativeWindow(acHandle);
						return;
					}
					ComboBox.ACNativeWindow.ACWindows.Add(acHandle, null);
				}
			}

			// Token: 0x060063EC RID: 25580 RVA: 0x001715DC File Offset: 0x0016F7DC
			internal static void ClearNullACWindows()
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in ComboBox.ACNativeWindow.ACWindows)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value == null)
					{
						arrayList.Add(dictionaryEntry.Key);
					}
				}
				foreach (object obj2 in arrayList)
				{
					IntPtr intPtr = (IntPtr)obj2;
					ComboBox.ACNativeWindow.ACWindows.Remove(intPtr);
				}
			}

			// Token: 0x04003945 RID: 14661
			internal static int inWndProcCnt;

			// Token: 0x04003946 RID: 14662
			private static Hashtable ACWindows = new Hashtable();
		}

		// Token: 0x02000631 RID: 1585
		private class AutoCompleteDropDownFinder
		{
			// Token: 0x060063EE RID: 25582 RVA: 0x001716A8 File Offset: 0x0016F8A8
			internal void FindDropDowns()
			{
				this.FindDropDowns(true);
			}

			// Token: 0x060063EF RID: 25583 RVA: 0x001716B1 File Offset: 0x0016F8B1
			internal void FindDropDowns(bool subclass)
			{
				if (!subclass)
				{
					ComboBox.ACNativeWindow.ClearNullACWindows();
				}
				this.shouldSubClass = subclass;
				UnsafeNativeMethods.EnumThreadWindows(SafeNativeMethods.GetCurrentThreadId(), new NativeMethods.EnumThreadWindowsCallback(this.Callback), new HandleRef(null, IntPtr.Zero));
			}

			// Token: 0x060063F0 RID: 25584 RVA: 0x001716E4 File Offset: 0x0016F8E4
			private bool Callback(IntPtr hWnd, IntPtr lParam)
			{
				HandleRef hRef = new HandleRef(null, hWnd);
				if (ComboBox.AutoCompleteDropDownFinder.GetClassName(hRef) == "Auto-Suggest Dropdown")
				{
					ComboBox.ACNativeWindow.RegisterACWindow(hRef.Handle, this.shouldSubClass);
				}
				return true;
			}

			// Token: 0x060063F1 RID: 25585 RVA: 0x00171720 File Offset: 0x0016F920
			private static string GetClassName(HandleRef hRef)
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				UnsafeNativeMethods.GetClassName(hRef, stringBuilder, 256);
				return stringBuilder.ToString();
			}

			// Token: 0x04003947 RID: 14663
			private const int MaxClassName = 256;

			// Token: 0x04003948 RID: 14664
			private const string AutoCompleteClassName = "Auto-Suggest Dropdown";

			// Token: 0x04003949 RID: 14665
			private bool shouldSubClass;
		}

		// Token: 0x02000632 RID: 1586
		internal class FlatComboAdapter
		{
			// Token: 0x060063F3 RID: 25587 RVA: 0x0017174C File Offset: 0x0016F94C
			public FlatComboAdapter(ComboBox comboBox, bool smallButton)
			{
				if ((!ComboBox.FlatComboAdapter.isScalingInitialized && DpiHelper.IsScalingRequired) || DpiHelper.EnableDpiChangedMessageHandling)
				{
					ComboBox.FlatComboAdapter.Offset2Pixels = comboBox.LogicalToDeviceUnits(ComboBox.FlatComboAdapter.OFFSET_2PIXELS);
					ComboBox.FlatComboAdapter.isScalingInitialized = true;
				}
				this.clientRect = comboBox.ClientRectangle;
				int horizontalScrollBarArrowWidthForDpi = SystemInformation.GetHorizontalScrollBarArrowWidthForDpi(comboBox.deviceDpi);
				this.outerBorder = new Rectangle(this.clientRect.Location, new Size(this.clientRect.Width - 1, this.clientRect.Height - 1));
				this.innerBorder = new Rectangle(this.outerBorder.X + 1, this.outerBorder.Y + 1, this.outerBorder.Width - horizontalScrollBarArrowWidthForDpi - 2, this.outerBorder.Height - 2);
				this.innerInnerBorder = new Rectangle(this.innerBorder.X + 1, this.innerBorder.Y + 1, this.innerBorder.Width - 2, this.innerBorder.Height - 2);
				this.dropDownRect = new Rectangle(this.innerBorder.Right + 1, this.innerBorder.Y, horizontalScrollBarArrowWidthForDpi, this.innerBorder.Height + 1);
				if (smallButton)
				{
					this.whiteFillRect = this.dropDownRect;
					this.whiteFillRect.Width = 5;
					this.dropDownRect.X = this.dropDownRect.X + 5;
					this.dropDownRect.Width = this.dropDownRect.Width - 5;
				}
				this.origRightToLeft = comboBox.RightToLeft;
				if (this.origRightToLeft == RightToLeft.Yes)
				{
					this.innerBorder.X = this.clientRect.Width - this.innerBorder.Right;
					this.innerInnerBorder.X = this.clientRect.Width - this.innerInnerBorder.Right;
					this.dropDownRect.X = this.clientRect.Width - this.dropDownRect.Right;
					this.whiteFillRect.X = this.clientRect.Width - this.whiteFillRect.Right + 1;
				}
			}

			// Token: 0x060063F4 RID: 25588 RVA: 0x0017196B File Offset: 0x0016FB6B
			public bool IsValid(ComboBox combo)
			{
				return combo.ClientRectangle == this.clientRect && combo.RightToLeft == this.origRightToLeft;
			}

			// Token: 0x060063F5 RID: 25589 RVA: 0x00171990 File Offset: 0x0016FB90
			public virtual void DrawFlatCombo(ComboBox comboBox, Graphics g)
			{
				if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
				{
					return;
				}
				Color outerBorderColor = this.GetOuterBorderColor(comboBox);
				Color innerBorderColor = this.GetInnerBorderColor(comboBox);
				bool flag = comboBox.RightToLeft == RightToLeft.Yes;
				this.DrawFlatComboDropDown(comboBox, g, this.dropDownRect);
				if (!LayoutUtils.IsZeroWidthOrHeight(this.whiteFillRect))
				{
					using (Brush brush = new SolidBrush(innerBorderColor))
					{
						g.FillRectangle(brush, this.whiteFillRect);
					}
				}
				if (outerBorderColor.IsSystemColor)
				{
					Pen pen = SystemPens.FromSystemColor(outerBorderColor);
					g.DrawRectangle(pen, this.outerBorder);
					if (flag)
					{
						g.DrawRectangle(pen, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
					}
					else
					{
						g.DrawRectangle(pen, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
					}
				}
				else
				{
					using (Pen pen2 = new Pen(outerBorderColor))
					{
						g.DrawRectangle(pen2, this.outerBorder);
						if (flag)
						{
							g.DrawRectangle(pen2, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
						}
						else
						{
							g.DrawRectangle(pen2, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
						}
					}
				}
				if (innerBorderColor.IsSystemColor)
				{
					Pen pen3 = SystemPens.FromSystemColor(innerBorderColor);
					g.DrawRectangle(pen3, this.innerBorder);
					g.DrawRectangle(pen3, this.innerInnerBorder);
				}
				else
				{
					using (Pen pen4 = new Pen(innerBorderColor))
					{
						g.DrawRectangle(pen4, this.innerBorder);
						g.DrawRectangle(pen4, this.innerInnerBorder);
					}
				}
				if (!comboBox.Enabled || comboBox.FlatStyle == FlatStyle.Popup)
				{
					bool focused = comboBox.ContainsFocus || comboBox.MouseIsOver;
					Color popupOuterBorderColor = this.GetPopupOuterBorderColor(comboBox, focused);
					using (Pen pen5 = new Pen(popupOuterBorderColor))
					{
						Pen pen6 = comboBox.Enabled ? pen5 : SystemPens.Control;
						if (flag)
						{
							g.DrawRectangle(pen6, new Rectangle(this.outerBorder.X, this.outerBorder.Y, this.dropDownRect.Width + 1, this.outerBorder.Height));
						}
						else
						{
							g.DrawRectangle(pen6, new Rectangle(this.dropDownRect.X, this.outerBorder.Y, this.outerBorder.Right - this.dropDownRect.X, this.outerBorder.Height));
						}
						g.DrawRectangle(pen5, this.outerBorder);
					}
				}
			}

			// Token: 0x060063F6 RID: 25590 RVA: 0x00171CD8 File Offset: 0x0016FED8
			protected virtual void DrawFlatComboDropDown(ComboBox comboBox, Graphics g, Rectangle dropDownRect)
			{
				g.FillRectangle(SystemBrushes.Control, dropDownRect);
				Brush brush = comboBox.Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
				Point point = new Point(dropDownRect.Left + dropDownRect.Width / 2, dropDownRect.Top + dropDownRect.Height / 2);
				if (this.origRightToLeft == RightToLeft.Yes)
				{
					point.X -= dropDownRect.Width % 2;
				}
				else
				{
					point.X += dropDownRect.Width % 2;
				}
				g.FillPolygon(brush, new Point[]
				{
					new Point(point.X - ComboBox.FlatComboAdapter.Offset2Pixels, point.Y - 1),
					new Point(point.X + ComboBox.FlatComboAdapter.Offset2Pixels + 1, point.Y - 1),
					new Point(point.X, point.Y + ComboBox.FlatComboAdapter.Offset2Pixels)
				});
			}

			// Token: 0x060063F7 RID: 25591 RVA: 0x00171DDB File Offset: 0x0016FFDB
			protected virtual Color GetOuterBorderColor(ComboBox comboBox)
			{
				if (!comboBox.Enabled)
				{
					return SystemColors.ControlDark;
				}
				return SystemColors.Window;
			}

			// Token: 0x060063F8 RID: 25592 RVA: 0x00171DF0 File Offset: 0x0016FFF0
			protected virtual Color GetPopupOuterBorderColor(ComboBox comboBox, bool focused)
			{
				if (!comboBox.Enabled)
				{
					return SystemColors.ControlDark;
				}
				if (!focused)
				{
					return SystemColors.Window;
				}
				return SystemColors.ControlDark;
			}

			// Token: 0x060063F9 RID: 25593 RVA: 0x00171E0E File Offset: 0x0017000E
			protected virtual Color GetInnerBorderColor(ComboBox comboBox)
			{
				if (!comboBox.Enabled)
				{
					return SystemColors.Control;
				}
				return comboBox.BackColor;
			}

			// Token: 0x060063FA RID: 25594 RVA: 0x00171E24 File Offset: 0x00170024
			public void ValidateOwnerDrawRegions(ComboBox comboBox, Rectangle updateRegionBox)
			{
				if (comboBox != null)
				{
					return;
				}
				Rectangle r = new Rectangle(0, 0, comboBox.Width, this.innerBorder.Top);
				Rectangle r2 = new Rectangle(0, this.innerBorder.Bottom, comboBox.Width, comboBox.Height - this.innerBorder.Bottom);
				Rectangle r3 = new Rectangle(0, 0, this.innerBorder.Left, comboBox.Height);
				Rectangle r4 = new Rectangle(this.innerBorder.Right, 0, comboBox.Width - this.innerBorder.Right, comboBox.Height);
				if (r.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(r);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
				if (r2.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(r2);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
				if (r3.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(r3);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
				if (r4.IntersectsWith(updateRegionBox))
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(r4);
					SafeNativeMethods.ValidateRect(new HandleRef(comboBox, comboBox.Handle), ref rect);
				}
			}

			// Token: 0x0400394A RID: 14666
			private Rectangle outerBorder;

			// Token: 0x0400394B RID: 14667
			private Rectangle innerBorder;

			// Token: 0x0400394C RID: 14668
			private Rectangle innerInnerBorder;

			// Token: 0x0400394D RID: 14669
			internal Rectangle dropDownRect;

			// Token: 0x0400394E RID: 14670
			private Rectangle whiteFillRect;

			// Token: 0x0400394F RID: 14671
			private Rectangle clientRect;

			// Token: 0x04003950 RID: 14672
			private RightToLeft origRightToLeft;

			// Token: 0x04003951 RID: 14673
			private const int WhiteFillRectWidth = 5;

			// Token: 0x04003952 RID: 14674
			private static bool isScalingInitialized = false;

			// Token: 0x04003953 RID: 14675
			private static int OFFSET_2PIXELS = 2;

			// Token: 0x04003954 RID: 14676
			protected static int Offset2Pixels = ComboBox.FlatComboAdapter.OFFSET_2PIXELS;
		}

		// Token: 0x02000633 RID: 1587
		internal enum ChildWindowType
		{
			// Token: 0x04003956 RID: 14678
			ListBox,
			// Token: 0x04003957 RID: 14679
			Edit,
			// Token: 0x04003958 RID: 14680
			DropDownList
		}
	}
}
