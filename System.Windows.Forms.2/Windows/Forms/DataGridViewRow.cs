using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000208 RID: 520
	[TypeConverter(typeof(DataGridViewRowConverter))]
	public class DataGridViewRow : DataGridViewBand
	{
		// Token: 0x060021D0 RID: 8656 RVA: 0x0009FE43 File Offset: 0x0009E043
		public DataGridViewRow()
		{
			this.bandIsRow = true;
			base.MinimumThickness = 3;
			base.Thickness = Control.DefaultFont.Height + 9;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0009FE6C File Offset: 0x0009E06C
		[Browsable(false)]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)base.Properties.GetObject(DataGridViewRow.PropRowAccessibilityObject);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					base.Properties.SetObject(DataGridViewRow.PropRowAccessibilityObject, accessibleObject);
				}
				return accessibleObject;
			}
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0009FEAC File Offset: 0x0009E0AC
		internal void ClearAccessibilityObjectOwner()
		{
			object @object = base.Properties.GetObject(DataGridViewRow.PropRowAccessibilityObject);
			DataGridViewRow.DataGridViewRowAccessibleObject dataGridViewRowAccessibleObject = @object as DataGridViewRow.DataGridViewRowAccessibleObject;
			if (dataGridViewRowAccessibleObject != null)
			{
				dataGridViewRowAccessibleObject.ClearOwnerRow();
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0009FEDA File Offset: 0x0009E0DA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataGridViewCellCollection Cells
		{
			get
			{
				if (this.rowCells == null)
				{
					this.rowCells = this.CreateCellsInstance();
				}
				return this.rowCells;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x000936C6 File Offset: 0x000918C6
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x000936CE File Offset: 0x000918CE
		[DefaultValue(null)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_RowContextMenuStripDescr")]
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0009FEF8 File Offset: 0x0009E0F8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object DataBoundItem
		{
			get
			{
				if (base.DataGridView != null && base.DataGridView.DataConnection != null && base.Index > -1 && base.Index != base.DataGridView.NewRowIndex)
				{
					return base.DataGridView.DataConnection.CurrencyManager[base.Index];
				}
				return null;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x00093714 File Offset: 0x00091914
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x0009FF53 File Offset: 0x0009E153
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowDefaultCellStyleDescr")]
		public override DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				return base.DefaultCellStyle;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
					{
						"DefaultCellStyle"
					}));
				}
				base.DefaultCellStyle = value;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0009FF8B File Offset: 0x0009E18B
		[Browsable(false)]
		public override bool Displayed
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"Displayed"
					}));
				}
				return this.GetDisplayed(base.Index);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060021DA RID: 8666 RVA: 0x000939D3 File Offset: 0x00091BD3
		// (set) Token: 0x060021DB RID: 8667 RVA: 0x0009FFC8 File Offset: 0x0009E1C8
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowDividerHeightDescr")]
		public int DividerHeight
		{
			get
			{
				return base.DividerThickness;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
					{
						"DividerHeight"
					}));
				}
				base.DividerThickness = value;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x000A0000 File Offset: 0x0009E200
		// (set) Token: 0x060021DD RID: 8669 RVA: 0x000A000E File Offset: 0x0009E20E
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowErrorTextDescr")]
		public string ErrorText
		{
			get
			{
				return this.GetErrorText(base.Index);
			}
			set
			{
				this.ErrorTextInternal = value;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x000A0018 File Offset: 0x0009E218
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x000A0048 File Offset: 0x0009E248
		private string ErrorTextInternal
		{
			get
			{
				object @object = base.Properties.GetObject(DataGridViewRow.PropRowErrorText);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				string errorTextInternal = this.ErrorTextInternal;
				if (!string.IsNullOrEmpty(value) || base.Properties.ContainsObject(DataGridViewRow.PropRowErrorText))
				{
					base.Properties.SetObject(DataGridViewRow.PropRowErrorText, value);
				}
				if (base.DataGridView != null && !errorTextInternal.Equals(this.ErrorTextInternal))
				{
					base.DataGridView.OnRowErrorTextChanged(this);
				}
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000A00A9 File Offset: 0x0009E2A9
		// (set) Token: 0x060021E1 RID: 8673 RVA: 0x000A00E6 File Offset: 0x0009E2E6
		[Browsable(false)]
		public override bool Frozen
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"Frozen"
					}));
				}
				return this.GetFrozen(base.Index);
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
					{
						"Frozen"
					}));
				}
				base.Frozen = value;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000A011E File Offset: 0x0009E31E
		internal bool HasErrorText
		{
			get
			{
				return base.Properties.ContainsObject(DataGridViewRow.PropRowErrorText) && base.Properties.GetObject(DataGridViewRow.PropRowErrorText) != null;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000A0147 File Offset: 0x0009E347
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x00093AEA File Offset: 0x00091CEA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataGridViewRowHeaderCell HeaderCell
		{
			get
			{
				return (DataGridViewRowHeaderCell)base.HeaderCellCore;
			}
			set
			{
				base.HeaderCellCore = value;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x0009413F File Offset: 0x0009233F
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x000A0154 File Offset: 0x0009E354
		[DefaultValue(22)]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("DataGridView_RowHeightDescr")]
		public int Height
		{
			get
			{
				return base.Thickness;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
					{
						"Height"
					}));
				}
				base.Thickness = value;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000A018C File Offset: 0x0009E38C
		public override DataGridViewCellStyle InheritedStyle
		{
			get
			{
				if (base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"InheritedStyle"
					}));
				}
				DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
				this.BuildInheritedRowStyle(base.Index, dataGridViewCellStyle);
				return dataGridViewCellStyle;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000A01D4 File Offset: 0x0009E3D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsNewRow
		{
			get
			{
				return base.DataGridView != null && base.DataGridView.NewRowIndex == base.Index;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x00093E5B File Offset: 0x0009205B
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x000A01F3 File Offset: 0x0009E3F3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MinimumHeight
		{
			get
			{
				return base.MinimumThickness;
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
					{
						"MinimumHeight"
					}));
				}
				base.MinimumThickness = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000A022B File Offset: 0x0009E42B
		// (set) Token: 0x060021EC RID: 8684 RVA: 0x000A0268 File Offset: 0x0009E468
		[Browsable(true)]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_RowReadOnlyDescr")]
		public override bool ReadOnly
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"ReadOnly"
					}));
				}
				return this.GetReadOnly(base.Index);
			}
			set
			{
				base.ReadOnly = value;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x000A0271 File Offset: 0x0009E471
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x00093F69 File Offset: 0x00092169
		[NotifyParentProperty(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("DataGridView_RowResizableDescr")]
		public override DataGridViewTriState Resizable
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"Resizable"
					}));
				}
				return this.GetResizable(base.Index);
			}
			set
			{
				base.Resizable = value;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x000A02AE File Offset: 0x0009E4AE
		// (set) Token: 0x060021F0 RID: 8688 RVA: 0x000A02EB File Offset: 0x0009E4EB
		public override bool Selected
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"Selected"
					}));
				}
				return this.GetSelected(base.Index);
			}
			set
			{
				base.Selected = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000A02F4 File Offset: 0x0009E4F4
		public override DataGridViewElementStates State
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"State"
					}));
				}
				return this.GetState(base.Index);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x000A0331 File Offset: 0x0009E531
		// (set) Token: 0x060021F3 RID: 8691 RVA: 0x000A036E File Offset: 0x0009E56E
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedRow", new object[]
					{
						"Visible"
					}));
				}
				return this.GetVisible(base.Index);
			}
			set
			{
				if (base.DataGridView != null && base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
					{
						"Visible"
					}));
				}
				base.Visible = value;
			}
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000A03A8 File Offset: 0x0009E5A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewAdvancedBorderStyle AdjustRowHeaderBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			if (base.DataGridView != null && base.DataGridView.ApplyVisualStylesToHeaderCells)
			{
				switch (dataGridViewAdvancedBorderStyleInput.All)
				{
				case DataGridViewAdvancedCellBorderStyle.Single:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Single;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.Inset:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.InsetDouble:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
					}
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.Outset:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
					if (isFirstDisplayedRow && !base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					}
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
			}
			else
			{
				switch (dataGridViewAdvancedBorderStyleInput.All)
				{
				case DataGridViewAdvancedCellBorderStyle.Single:
					if (!isFirstDisplayedRow || base.DataGridView.ColumnHeadersVisible)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
						dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
						return dataGridViewAdvancedBorderStylePlaceholder;
					}
					break;
				case DataGridViewAdvancedCellBorderStyle.Inset:
					if (isFirstDisplayedRow && singleHorizontalBorderAdded)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						return dataGridViewAdvancedBorderStylePlaceholder;
					}
					break;
				case DataGridViewAdvancedCellBorderStyle.InsetDouble:
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Inset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.InsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					if (isFirstDisplayedRow)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = (base.DataGridView.ColumnHeadersVisible ? DataGridViewAdvancedCellBorderStyle.Inset : DataGridViewAdvancedCellBorderStyle.InsetDouble);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					}
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Inset;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.Outset:
					if (isFirstDisplayedRow && singleHorizontalBorderAdded)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						return dataGridViewAdvancedBorderStylePlaceholder;
					}
					break;
				case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					if (isFirstDisplayedRow)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = (base.DataGridView.ColumnHeadersVisible ? DataGridViewAdvancedCellBorderStyle.Outset : DataGridViewAdvancedCellBorderStyle.OutsetDouble);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					return dataGridViewAdvancedBorderStylePlaceholder;
				case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Outset;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.OutsetDouble;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Outset;
					}
					if (isFirstDisplayedRow)
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = (base.DataGridView.ColumnHeadersVisible ? DataGridViewAdvancedCellBorderStyle.Outset : DataGridViewAdvancedCellBorderStyle.OutsetDouble);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.OutsetPartial;
					}
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = (isLastVisibleRow ? DataGridViewAdvancedCellBorderStyle.Outset : DataGridViewAdvancedCellBorderStyle.OutsetPartial);
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
			}
			return dataGridViewAdvancedBorderStyleInput;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000A077C File Offset: 0x0009E97C
		private void BuildInheritedRowHeaderCellStyle(DataGridViewCellStyle inheritedCellStyle)
		{
			DataGridViewCellStyle dataGridViewCellStyle = null;
			if (this.HeaderCell.HasStyle)
			{
				dataGridViewCellStyle = this.HeaderCell.Style;
			}
			DataGridViewCellStyle rowHeadersDefaultCellStyle = base.DataGridView.RowHeadersDefaultCellStyle;
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.BackColor.IsEmpty)
			{
				inheritedCellStyle.BackColor = dataGridViewCellStyle.BackColor;
			}
			else if (!rowHeadersDefaultCellStyle.BackColor.IsEmpty)
			{
				inheritedCellStyle.BackColor = rowHeadersDefaultCellStyle.BackColor;
			}
			else
			{
				inheritedCellStyle.BackColor = defaultCellStyle.BackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.ForeColor.IsEmpty)
			{
				inheritedCellStyle.ForeColor = dataGridViewCellStyle.ForeColor;
			}
			else if (!rowHeadersDefaultCellStyle.ForeColor.IsEmpty)
			{
				inheritedCellStyle.ForeColor = rowHeadersDefaultCellStyle.ForeColor;
			}
			else
			{
				inheritedCellStyle.ForeColor = defaultCellStyle.ForeColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedCellStyle.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			}
			else if (!rowHeadersDefaultCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedCellStyle.SelectionBackColor = rowHeadersDefaultCellStyle.SelectionBackColor;
			}
			else
			{
				inheritedCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedCellStyle.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			}
			else if (!rowHeadersDefaultCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedCellStyle.SelectionForeColor = rowHeadersDefaultCellStyle.SelectionForeColor;
			}
			else
			{
				inheritedCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Font != null)
			{
				inheritedCellStyle.Font = dataGridViewCellStyle.Font;
			}
			else if (rowHeadersDefaultCellStyle.Font != null)
			{
				inheritedCellStyle.Font = rowHeadersDefaultCellStyle.Font;
			}
			else
			{
				inheritedCellStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsNullValueDefault)
			{
				inheritedCellStyle.NullValue = dataGridViewCellStyle.NullValue;
			}
			else if (!rowHeadersDefaultCellStyle.IsNullValueDefault)
			{
				inheritedCellStyle.NullValue = rowHeadersDefaultCellStyle.NullValue;
			}
			else
			{
				inheritedCellStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedCellStyle.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			}
			else if (!rowHeadersDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedCellStyle.DataSourceNullValue = rowHeadersDefaultCellStyle.DataSourceNullValue;
			}
			else
			{
				inheritedCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Format.Length != 0)
			{
				inheritedCellStyle.Format = dataGridViewCellStyle.Format;
			}
			else if (rowHeadersDefaultCellStyle.Format.Length != 0)
			{
				inheritedCellStyle.Format = rowHeadersDefaultCellStyle.Format;
			}
			else
			{
				inheritedCellStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsFormatProviderDefault)
			{
				inheritedCellStyle.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			else if (!rowHeadersDefaultCellStyle.IsFormatProviderDefault)
			{
				inheritedCellStyle.FormatProvider = rowHeadersDefaultCellStyle.FormatProvider;
			}
			else
			{
				inheritedCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedCellStyle.AlignmentInternal = dataGridViewCellStyle.Alignment;
			}
			else if (rowHeadersDefaultCellStyle != null && rowHeadersDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedCellStyle.AlignmentInternal = rowHeadersDefaultCellStyle.Alignment;
			}
			else
			{
				inheritedCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedCellStyle.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			}
			else if (rowHeadersDefaultCellStyle != null && rowHeadersDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedCellStyle.WrapModeInternal = rowHeadersDefaultCellStyle.WrapMode;
			}
			else
			{
				inheritedCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Tag != null)
			{
				inheritedCellStyle.Tag = dataGridViewCellStyle.Tag;
			}
			else if (rowHeadersDefaultCellStyle.Tag != null)
			{
				inheritedCellStyle.Tag = rowHeadersDefaultCellStyle.Tag;
			}
			else
			{
				inheritedCellStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Padding != Padding.Empty)
			{
				inheritedCellStyle.PaddingInternal = dataGridViewCellStyle.Padding;
				return;
			}
			if (rowHeadersDefaultCellStyle.Padding != Padding.Empty)
			{
				inheritedCellStyle.PaddingInternal = rowHeadersDefaultCellStyle.Padding;
				return;
			}
			inheritedCellStyle.PaddingInternal = defaultCellStyle.Padding;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000A0B20 File Offset: 0x0009ED20
		private void BuildInheritedRowStyle(int rowIndex, DataGridViewCellStyle inheritedRowStyle)
		{
			DataGridViewCellStyle dataGridViewCellStyle = null;
			if (base.HasDefaultCellStyle)
			{
				dataGridViewCellStyle = this.DefaultCellStyle;
			}
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			DataGridViewCellStyle rowsDefaultCellStyle = base.DataGridView.RowsDefaultCellStyle;
			DataGridViewCellStyle alternatingRowsDefaultCellStyle = base.DataGridView.AlternatingRowsDefaultCellStyle;
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.BackColor.IsEmpty)
			{
				inheritedRowStyle.BackColor = dataGridViewCellStyle.BackColor;
			}
			else if (!rowsDefaultCellStyle.BackColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.BackColor.IsEmpty))
			{
				inheritedRowStyle.BackColor = rowsDefaultCellStyle.BackColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.BackColor.IsEmpty)
			{
				inheritedRowStyle.BackColor = alternatingRowsDefaultCellStyle.BackColor;
			}
			else
			{
				inheritedRowStyle.BackColor = defaultCellStyle.BackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.ForeColor.IsEmpty)
			{
				inheritedRowStyle.ForeColor = dataGridViewCellStyle.ForeColor;
			}
			else if (!rowsDefaultCellStyle.ForeColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.ForeColor.IsEmpty))
			{
				inheritedRowStyle.ForeColor = rowsDefaultCellStyle.ForeColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.ForeColor.IsEmpty)
			{
				inheritedRowStyle.ForeColor = alternatingRowsDefaultCellStyle.ForeColor;
			}
			else
			{
				inheritedRowStyle.ForeColor = defaultCellStyle.ForeColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedRowStyle.SelectionBackColor = dataGridViewCellStyle.SelectionBackColor;
			}
			else if (!rowsDefaultCellStyle.SelectionBackColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty))
			{
				inheritedRowStyle.SelectionBackColor = rowsDefaultCellStyle.SelectionBackColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty)
			{
				inheritedRowStyle.SelectionBackColor = alternatingRowsDefaultCellStyle.SelectionBackColor;
			}
			else
			{
				inheritedRowStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedRowStyle.SelectionForeColor = dataGridViewCellStyle.SelectionForeColor;
			}
			else if (!rowsDefaultCellStyle.SelectionForeColor.IsEmpty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty))
			{
				inheritedRowStyle.SelectionForeColor = rowsDefaultCellStyle.SelectionForeColor;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty)
			{
				inheritedRowStyle.SelectionForeColor = alternatingRowsDefaultCellStyle.SelectionForeColor;
			}
			else
			{
				inheritedRowStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Font != null)
			{
				inheritedRowStyle.Font = dataGridViewCellStyle.Font;
			}
			else if (rowsDefaultCellStyle.Font != null && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Font == null))
			{
				inheritedRowStyle.Font = rowsDefaultCellStyle.Font;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Font != null)
			{
				inheritedRowStyle.Font = alternatingRowsDefaultCellStyle.Font;
			}
			else
			{
				inheritedRowStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsNullValueDefault)
			{
				inheritedRowStyle.NullValue = dataGridViewCellStyle.NullValue;
			}
			else if (!rowsDefaultCellStyle.IsNullValueDefault && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.IsNullValueDefault))
			{
				inheritedRowStyle.NullValue = rowsDefaultCellStyle.NullValue;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.IsNullValueDefault)
			{
				inheritedRowStyle.NullValue = alternatingRowsDefaultCellStyle.NullValue;
			}
			else
			{
				inheritedRowStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedRowStyle.DataSourceNullValue = dataGridViewCellStyle.DataSourceNullValue;
			}
			else if (!rowsDefaultCellStyle.IsDataSourceNullValueDefault && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault))
			{
				inheritedRowStyle.DataSourceNullValue = rowsDefaultCellStyle.DataSourceNullValue;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				inheritedRowStyle.DataSourceNullValue = alternatingRowsDefaultCellStyle.DataSourceNullValue;
			}
			else
			{
				inheritedRowStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Format.Length != 0)
			{
				inheritedRowStyle.Format = dataGridViewCellStyle.Format;
			}
			else if (rowsDefaultCellStyle.Format.Length != 0 && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Format.Length == 0))
			{
				inheritedRowStyle.Format = rowsDefaultCellStyle.Format;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Format.Length != 0)
			{
				inheritedRowStyle.Format = alternatingRowsDefaultCellStyle.Format;
			}
			else
			{
				inheritedRowStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle != null && !dataGridViewCellStyle.IsFormatProviderDefault)
			{
				inheritedRowStyle.FormatProvider = dataGridViewCellStyle.FormatProvider;
			}
			else if (!rowsDefaultCellStyle.IsFormatProviderDefault && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.IsFormatProviderDefault))
			{
				inheritedRowStyle.FormatProvider = rowsDefaultCellStyle.FormatProvider;
			}
			else if (rowIndex % 2 == 1 && !alternatingRowsDefaultCellStyle.IsFormatProviderDefault)
			{
				inheritedRowStyle.FormatProvider = alternatingRowsDefaultCellStyle.FormatProvider;
			}
			else
			{
				inheritedRowStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedRowStyle.AlignmentInternal = dataGridViewCellStyle.Alignment;
			}
			else if (rowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet))
			{
				inheritedRowStyle.AlignmentInternal = rowsDefaultCellStyle.Alignment;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				inheritedRowStyle.AlignmentInternal = alternatingRowsDefaultCellStyle.Alignment;
			}
			else
			{
				inheritedRowStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedRowStyle.WrapModeInternal = dataGridViewCellStyle.WrapMode;
			}
			else if (rowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.WrapMode == DataGridViewTriState.NotSet))
			{
				inheritedRowStyle.WrapModeInternal = rowsDefaultCellStyle.WrapMode;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				inheritedRowStyle.WrapModeInternal = alternatingRowsDefaultCellStyle.WrapMode;
			}
			else
			{
				inheritedRowStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Tag != null)
			{
				inheritedRowStyle.Tag = dataGridViewCellStyle.Tag;
			}
			else if (rowsDefaultCellStyle.Tag != null && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Tag == null))
			{
				inheritedRowStyle.Tag = rowsDefaultCellStyle.Tag;
			}
			else if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Tag != null)
			{
				inheritedRowStyle.Tag = alternatingRowsDefaultCellStyle.Tag;
			}
			else
			{
				inheritedRowStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle != null && dataGridViewCellStyle.Padding != Padding.Empty)
			{
				inheritedRowStyle.PaddingInternal = dataGridViewCellStyle.Padding;
				return;
			}
			if (rowsDefaultCellStyle.Padding != Padding.Empty && (rowIndex % 2 == 0 || alternatingRowsDefaultCellStyle.Padding == Padding.Empty))
			{
				inheritedRowStyle.PaddingInternal = rowsDefaultCellStyle.Padding;
				return;
			}
			if (rowIndex % 2 == 1 && alternatingRowsDefaultCellStyle.Padding != Padding.Empty)
			{
				inheritedRowStyle.PaddingInternal = alternatingRowsDefaultCellStyle.Padding;
				return;
			}
			inheritedRowStyle.PaddingInternal = defaultCellStyle.Padding;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000A1140 File Offset: 0x0009F340
		public override object Clone()
		{
			Type type = base.GetType();
			DataGridViewRow dataGridViewRow;
			if (type == DataGridViewRow.rowType)
			{
				dataGridViewRow = new DataGridViewRow();
			}
			else
			{
				dataGridViewRow = (DataGridViewRow)Activator.CreateInstance(type);
			}
			if (dataGridViewRow != null)
			{
				base.CloneInternal(dataGridViewRow);
				if (this.HasErrorText)
				{
					dataGridViewRow.ErrorText = this.ErrorTextInternal;
				}
				if (base.HasHeaderCell)
				{
					dataGridViewRow.HeaderCell = (DataGridViewRowHeaderCell)this.HeaderCell.Clone();
				}
				dataGridViewRow.CloneCells(this);
			}
			return dataGridViewRow;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000A11BC File Offset: 0x0009F3BC
		private void CloneCells(DataGridViewRow rowTemplate)
		{
			int count = rowTemplate.Cells.Count;
			if (count > 0)
			{
				DataGridViewCell[] array = new DataGridViewCell[count];
				for (int i = 0; i < count; i++)
				{
					DataGridViewCell dataGridViewCell = rowTemplate.Cells[i];
					DataGridViewCell dataGridViewCell2 = (DataGridViewCell)dataGridViewCell.Clone();
					array[i] = dataGridViewCell2;
				}
				this.Cells.AddRange(array);
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000A1217 File Offset: 0x0009F417
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewRow.DataGridViewRowAccessibleObject(this);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000A1220 File Offset: 0x0009F420
		public void CreateCells(DataGridView dataGridView)
		{
			if (dataGridView == null)
			{
				throw new ArgumentNullException("dataGridView");
			}
			if (base.DataGridView != null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowAlreadyBelongsToDataGridView"));
			}
			DataGridViewCellCollection cells = this.Cells;
			cells.Clear();
			DataGridViewColumnCollection columns = dataGridView.Columns;
			foreach (object obj in columns)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)obj;
				if (dataGridViewColumn.CellTemplate == null)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_AColumnHasNoCellTemplate"));
				}
				DataGridViewCell dataGridViewCell = (DataGridViewCell)dataGridViewColumn.CellTemplate.Clone();
				cells.Add(dataGridViewCell);
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000A12E0 File Offset: 0x0009F4E0
		public void CreateCells(DataGridView dataGridView, params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.CreateCells(dataGridView);
			this.SetValuesInternal(values);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000A12FF File Offset: 0x0009F4FF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual DataGridViewCellCollection CreateCellsInstance()
		{
			return new DataGridViewCellCollection(this);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000A1308 File Offset: 0x0009F508
		internal void DetachFromDataGridView()
		{
			if (base.DataGridView != null)
			{
				base.DataGridViewInternal = null;
				base.IndexInternal = -1;
				if (base.HasHeaderCell)
				{
					this.HeaderCell.DataGridViewInternal = null;
				}
				foreach (object obj in this.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					dataGridViewCell.DataGridViewInternal = null;
					if (dataGridViewCell.Selected)
					{
						dataGridViewCell.SelectedInternal = false;
					}
				}
				if (this.Selected)
				{
					base.SelectedInternal = false;
				}
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000A13AC File Offset: 0x0009F5AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void DrawFocus(Graphics graphics, Rectangle clipBounds, Rectangle bounds, int rowIndex, DataGridViewElementStates rowState, DataGridViewCellStyle cellStyle, bool cellsPaintSelectionBackground)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			Color backColor;
			if (cellsPaintSelectionBackground && (rowState & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
			{
				backColor = cellStyle.SelectionBackColor;
			}
			else
			{
				backColor = cellStyle.BackColor;
			}
			ControlPaint.DrawFocusRectangle(graphics, bounds, Color.Empty, backColor);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000A1418 File Offset: 0x0009F618
		public ContextMenuStrip GetContextMenuStrip(int rowIndex)
		{
			ContextMenuStrip contextMenuStrip = base.ContextMenuStripInternal;
			if (base.DataGridView != null)
			{
				if (rowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedRow"));
				}
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null)
				{
					contextMenuStrip = base.DataGridView.OnRowContextMenuStripNeeded(rowIndex, contextMenuStrip);
				}
			}
			return contextMenuStrip;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000A1493 File Offset: 0x0009F693
		internal bool GetDisplayed(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Displayed) > DataGridViewElementStates.None;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000A14A4 File Offset: 0x0009F6A4
		public string GetErrorText(int rowIndex)
		{
			string text = this.ErrorTextInternal;
			if (base.DataGridView != null)
			{
				if (rowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedRow"));
				}
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (string.IsNullOrEmpty(text) && base.DataGridView.DataSource != null && rowIndex != base.DataGridView.NewRowIndex)
				{
					text = base.DataGridView.DataConnection.GetError(rowIndex);
				}
				if (base.DataGridView.DataSource != null || base.DataGridView.VirtualMode)
				{
					text = base.DataGridView.OnRowErrorTextNeeded(rowIndex, text);
				}
			}
			return text;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000A1557 File Offset: 0x0009F757
		internal bool GetFrozen(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000A1568 File Offset: 0x0009F768
		internal int GetHeight(int rowIndex)
		{
			int result;
			int num;
			base.GetHeightInfo(rowIndex, out result, out num);
			return result;
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000A1584 File Offset: 0x0009F784
		internal int GetMinimumHeight(int rowIndex)
		{
			int num;
			int result;
			base.GetHeightInfo(rowIndex, out num, out result);
			return result;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000A15A0 File Offset: 0x0009F7A0
		public virtual int GetPreferredHeight(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
		{
			if ((autoSizeRowMode & (DataGridViewAutoSizeRowMode)(-4)) != (DataGridViewAutoSizeRowMode)0)
			{
				throw new InvalidEnumArgumentException("autoSizeRowMode", (int)autoSizeRowMode, typeof(DataGridViewAutoSizeRowMode));
			}
			if (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView == null)
			{
				return -1;
			}
			int num = 0;
			if (base.DataGridView.RowHeadersVisible && (autoSizeRowMode & DataGridViewAutoSizeRowMode.RowHeader) != (DataGridViewAutoSizeRowMode)0)
			{
				if (fixedWidth || base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing || base.DataGridView.RowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.DisableResizing)
				{
					num = Math.Max(num, this.HeaderCell.GetPreferredHeight(rowIndex, base.DataGridView.RowHeadersWidth));
				}
				else
				{
					num = Math.Max(num, this.HeaderCell.GetPreferredSize(rowIndex).Height);
				}
			}
			if ((autoSizeRowMode & DataGridViewAutoSizeRowMode.AllCellsExceptHeader) != (DataGridViewAutoSizeRowMode)0)
			{
				foreach (object obj in this.Cells)
				{
					DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
					DataGridViewColumn dataGridViewColumn = base.DataGridView.Columns[dataGridViewCell.ColumnIndex];
					if (dataGridViewColumn.Visible)
					{
						int num2;
						if (fixedWidth || (dataGridViewColumn.InheritedAutoSizeMode & (DataGridViewAutoSizeColumnMode)12) == DataGridViewAutoSizeColumnMode.NotSet)
						{
							num2 = dataGridViewCell.GetPreferredHeight(rowIndex, dataGridViewColumn.Width);
						}
						else
						{
							num2 = dataGridViewCell.GetPreferredSize(rowIndex).Height;
						}
						if (num < num2)
						{
							num = num2;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000A1718 File Offset: 0x0009F918
		internal bool GetReadOnly(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.ReadOnly) != DataGridViewElementStates.None || (base.DataGridView != null && base.DataGridView.ReadOnly);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000A173C File Offset: 0x0009F93C
		internal DataGridViewTriState GetResizable(int rowIndex)
		{
			if ((this.GetState(rowIndex) & DataGridViewElementStates.ResizableSet) != DataGridViewElementStates.None)
			{
				if ((this.GetState(rowIndex) & DataGridViewElementStates.Resizable) == DataGridViewElementStates.None)
				{
					return DataGridViewTriState.False;
				}
				return DataGridViewTriState.True;
			}
			else
			{
				if (base.DataGridView == null)
				{
					return DataGridViewTriState.NotSet;
				}
				if (!base.DataGridView.AllowUserToResizeRows)
				{
					return DataGridViewTriState.False;
				}
				return DataGridViewTriState.True;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000A1773 File Offset: 0x0009F973
		internal bool GetSelected(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000A1784 File Offset: 0x0009F984
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewElementStates GetState(int rowIndex)
		{
			if (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (base.DataGridView != null && base.DataGridView.Rows.SharedRow(rowIndex).Index == -1)
			{
				return base.DataGridView.Rows.GetRowState(rowIndex);
			}
			if (rowIndex != base.Index)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"rowIndex",
					rowIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return base.State;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000A182A File Offset: 0x0009FA2A
		internal bool GetVisible(int rowIndex)
		{
			return (this.GetState(rowIndex) & DataGridViewElementStates.Visible) > DataGridViewElementStates.None;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000A1839 File Offset: 0x0009FA39
		internal void OnSharedStateChanged(int sharedRowIndex, DataGridViewElementStates elementState)
		{
			base.DataGridView.Rows.InvalidateCachedRowCount(elementState);
			base.DataGridView.Rows.InvalidateCachedRowsHeight(elementState);
			base.DataGridView.OnDataGridViewElementStateChanged(this, sharedRowIndex, elementState);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000A186B File Offset: 0x0009FA6B
		internal void OnSharedStateChanging(int sharedRowIndex, DataGridViewElementStates elementState)
		{
			base.DataGridView.OnDataGridViewElementStateChanging(this, sharedRowIndex, elementState);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000A187C File Offset: 0x0009FA7C
		protected internal virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			DataGridView dataGridView = base.DataGridView;
			DataGridViewRow dataGridViewRow = dataGridView.Rows.SharedRow(rowIndex);
			DataGridViewCellStyle inheritedRowStyle = new DataGridViewCellStyle();
			this.BuildInheritedRowStyle(rowIndex, inheritedRowStyle);
			DataGridViewRowPrePaintEventArgs rowPrePaintEventArgs = dataGridView.RowPrePaintEventArgs;
			rowPrePaintEventArgs.SetProperties(graphics, clipBounds, rowBounds, rowIndex, rowState, dataGridViewRow.GetErrorText(rowIndex), inheritedRowStyle, isFirstDisplayedRow, isLastVisibleRow);
			dataGridView.OnRowPrePaint(rowPrePaintEventArgs);
			if (rowPrePaintEventArgs.Handled)
			{
				return;
			}
			DataGridViewPaintParts paintParts = rowPrePaintEventArgs.PaintParts;
			Rectangle clipBounds2 = rowPrePaintEventArgs.ClipBounds;
			this.PaintHeader(graphics, clipBounds2, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
			this.PaintCells(graphics, clipBounds2, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
			dataGridViewRow = dataGridView.Rows.SharedRow(rowIndex);
			this.BuildInheritedRowStyle(rowIndex, inheritedRowStyle);
			DataGridViewRowPostPaintEventArgs rowPostPaintEventArgs = dataGridView.RowPostPaintEventArgs;
			rowPostPaintEventArgs.SetProperties(graphics, clipBounds2, rowBounds, rowIndex, rowState, dataGridViewRow.GetErrorText(rowIndex), inheritedRowStyle, isFirstDisplayedRow, isLastVisibleRow);
			dataGridView.OnRowPostPaint(rowPostPaintEventArgs);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000A1978 File Offset: 0x0009FB78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			if (paintParts < DataGridViewPaintParts.None || paintParts > DataGridViewPaintParts.All)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewPaintPartsCombination", new object[]
				{
					"paintParts"
				}));
			}
			DataGridView dataGridView = base.DataGridView;
			Rectangle rectangle = rowBounds;
			int num = dataGridView.RowHeadersVisible ? dataGridView.RowHeadersWidth : 0;
			bool flag = true;
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
			DataGridViewColumn dataGridViewColumn = dataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible);
			while (dataGridViewColumn != null)
			{
				DataGridViewCell dataGridViewCell = this.Cells[dataGridViewColumn.Index];
				rectangle.Width = dataGridViewColumn.Thickness;
				if (dataGridView.SingleVerticalBorderAdded && flag)
				{
					int width = rectangle.Width;
					rectangle.Width = width + 1;
				}
				if (dataGridView.RightToLeftInternal)
				{
					rectangle.X = rowBounds.Right - num - rectangle.Width;
				}
				else
				{
					rectangle.X = rowBounds.X + num;
				}
				DataGridViewColumn nextColumn = dataGridView.Columns.GetNextColumn(dataGridViewColumn, DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible, DataGridViewElementStates.None);
				if (clipBounds.IntersectsWith(rectangle))
				{
					DataGridViewElementStates dataGridViewElementStates = dataGridViewCell.CellStateFromColumnRowStates(rowState);
					if (base.Index != -1)
					{
						dataGridViewElementStates |= dataGridViewCell.State;
					}
					dataGridViewCell.GetInheritedStyle(dataGridViewCellStyle, rowIndex, true);
					DataGridViewAdvancedBorderStyle advancedBorderStyle = dataGridViewCell.AdjustCellBorderStyle(dataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, dataGridView.SingleVerticalBorderAdded, dataGridView.SingleHorizontalBorderAdded, flag, isFirstDisplayedRow);
					dataGridViewCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, dataGridViewElementStates, dataGridViewCellStyle, advancedBorderStyle, paintParts);
				}
				num += rectangle.Width;
				if (num >= rowBounds.Width)
				{
					break;
				}
				dataGridViewColumn = nextColumn;
				flag = false;
			}
			Rectangle rectangle2 = rowBounds;
			if (num < rectangle2.Width && dataGridView.FirstDisplayedScrollingColumnIndex >= 0)
			{
				if (!dataGridView.RightToLeftInternal)
				{
					rectangle2.X -= dataGridView.FirstDisplayedScrollingColumnHiddenWidth;
				}
				rectangle2.Width += dataGridView.FirstDisplayedScrollingColumnHiddenWidth;
				Region region = null;
				if (dataGridView.FirstDisplayedScrollingColumnHiddenWidth > 0)
				{
					region = graphics.Clip;
					Rectangle clip = rowBounds;
					if (!dataGridView.RightToLeftInternal)
					{
						clip.X += num;
					}
					clip.Width -= num;
					graphics.SetClip(clip);
				}
				dataGridViewColumn = dataGridView.Columns[dataGridView.FirstDisplayedScrollingColumnIndex];
				while (dataGridViewColumn != null)
				{
					DataGridViewCell dataGridViewCell = this.Cells[dataGridViewColumn.Index];
					rectangle.Width = dataGridViewColumn.Thickness;
					if (dataGridView.SingleVerticalBorderAdded && flag)
					{
						int width = rectangle.Width;
						rectangle.Width = width + 1;
					}
					if (dataGridView.RightToLeftInternal)
					{
						rectangle.X = rectangle2.Right - num - rectangle.Width;
					}
					else
					{
						rectangle.X = rectangle2.X + num;
					}
					DataGridViewColumn nextColumn = dataGridView.Columns.GetNextColumn(dataGridViewColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None);
					if (clipBounds.IntersectsWith(rectangle))
					{
						DataGridViewElementStates dataGridViewElementStates = dataGridViewCell.CellStateFromColumnRowStates(rowState);
						if (base.Index != -1)
						{
							dataGridViewElementStates |= dataGridViewCell.State;
						}
						dataGridViewCell.GetInheritedStyle(dataGridViewCellStyle, rowIndex, true);
						DataGridViewAdvancedBorderStyle advancedBorderStyle = dataGridViewCell.AdjustCellBorderStyle(dataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, dataGridView.SingleVerticalBorderAdded, dataGridView.SingleHorizontalBorderAdded, flag, isFirstDisplayedRow);
						dataGridViewCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, dataGridViewElementStates, dataGridViewCellStyle, advancedBorderStyle, paintParts);
					}
					num += rectangle.Width;
					if (num >= rectangle2.Width)
					{
						break;
					}
					dataGridViewColumn = nextColumn;
					flag = false;
				}
				if (region != null)
				{
					graphics.Clip = region;
					region.Dispose();
				}
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000A1CE8 File Offset: 0x0009FEE8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void PaintHeader(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_RowDoesNotYetBelongToDataGridView"));
			}
			if (paintParts < DataGridViewPaintParts.None || paintParts > DataGridViewPaintParts.All)
			{
				throw new InvalidEnumArgumentException("paintParts", (int)paintParts, typeof(DataGridViewPaintParts));
			}
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView.RowHeadersVisible)
			{
				Rectangle rectangle = rowBounds;
				rectangle.Width = dataGridView.RowHeadersWidth;
				if (dataGridView.RightToLeftInternal)
				{
					rectangle.X = rowBounds.Right - rectangle.Width;
				}
				if (clipBounds.IntersectsWith(rectangle))
				{
					DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
					this.BuildInheritedRowHeaderCellStyle(dataGridViewCellStyle);
					DataGridViewAdvancedBorderStyle advancedBorderStyle = this.AdjustRowHeaderBorderStyle(dataGridView.AdvancedRowHeadersBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, dataGridView.SingleVerticalBorderAdded, dataGridView.SingleHorizontalBorderAdded, isFirstDisplayedRow, isLastVisibleRow);
					this.HeaderCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, rowState, dataGridViewCellStyle, advancedBorderStyle, paintParts);
				}
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000A1DC4 File Offset: 0x0009FFC4
		internal void SetReadOnlyCellCore(DataGridViewCell dataGridViewCell, bool readOnly)
		{
			if (this.ReadOnly && !readOnly)
			{
				foreach (object obj in this.Cells)
				{
					DataGridViewCell dataGridViewCell2 = (DataGridViewCell)obj;
					dataGridViewCell2.ReadOnlyInternal = true;
				}
				dataGridViewCell.ReadOnlyInternal = false;
				this.ReadOnly = false;
				return;
			}
			if (!this.ReadOnly && readOnly)
			{
				dataGridViewCell.ReadOnlyInternal = true;
			}
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000A1E4C File Offset: 0x000A004C
		public bool SetValues(params object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (base.DataGridView != null)
			{
				if (base.DataGridView.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationInVirtualMode"));
				}
				if (base.Index == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedRow"));
				}
			}
			return this.SetValuesInternal(values);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000A1EAC File Offset: 0x000A00AC
		internal bool SetValuesInternal(params object[] values)
		{
			bool flag = true;
			DataGridViewCellCollection cells = this.Cells;
			int count = cells.Count;
			int num = 0;
			while (num < cells.Count && num != values.Length)
			{
				if (!cells[num].SetValueInternal(base.Index, values[num]))
				{
					flag = false;
				}
				num++;
			}
			return flag && values.Length <= count;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000A1F08 File Offset: 0x000A0108
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(36);
			stringBuilder.Append("DataGridViewRow { Index=");
			stringBuilder.Append(base.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000E25 RID: 3621
		private static Type rowType = typeof(DataGridViewRow);

		// Token: 0x04000E26 RID: 3622
		private static readonly int PropRowErrorText = PropertyStore.CreateKey();

		// Token: 0x04000E27 RID: 3623
		private static readonly int PropRowAccessibilityObject = PropertyStore.CreateKey();

		// Token: 0x04000E28 RID: 3624
		private const DataGridViewAutoSizeRowCriteriaInternal invalidDataGridViewAutoSizeRowCriteriaInternalMask = ~(DataGridViewAutoSizeRowCriteriaInternal.Header | DataGridViewAutoSizeRowCriteriaInternal.AllColumns);

		// Token: 0x04000E29 RID: 3625
		internal const int defaultMinRowThickness = 3;

		// Token: 0x04000E2A RID: 3626
		private DataGridViewCellCollection rowCells;

		// Token: 0x02000675 RID: 1653
		[ComVisible(true)]
		protected class DataGridViewRowAccessibleObject : AccessibleObject
		{
			// Token: 0x0600668E RID: 26254 RVA: 0x00177BCE File Offset: 0x00175DCE
			public DataGridViewRowAccessibleObject()
			{
			}

			// Token: 0x0600668F RID: 26255 RVA: 0x0017F1E8 File Offset: 0x0017D3E8
			public DataGridViewRowAccessibleObject(DataGridViewRow owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006690 RID: 26256 RVA: 0x0017F1F7 File Offset: 0x0017D3F7
			internal void ClearOwnerRow()
			{
				this.owner = null;
				DataGridViewRow.DataGridViewSelectedRowCellsAccessibleObject dataGridViewSelectedRowCellsAccessibleObject = this.selectedCellsAccessibilityObject;
				if (dataGridViewSelectedRowCellsAccessibleObject == null)
				{
					return;
				}
				dataGridViewSelectedRowCellsAccessibleObject.ClearOwnerRow();
			}

			// Token: 0x06006691 RID: 26257 RVA: 0x0017F210 File Offset: 0x0017D410
			internal bool IsOwnerRowDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.owner == null;
			}

			// Token: 0x1700164B RID: 5707
			// (get) Token: 0x06006692 RID: 26258 RVA: 0x0017F224 File Offset: 0x0017D424
			public override Rectangle Bounds
			{
				get
				{
					if (this.owner != null)
					{
						Rectangle rectangle;
						if (this.owner.Index < this.owner.DataGridView.FirstDisplayedScrollingRowIndex)
						{
							int rowCount = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, this.owner.Index);
							rectangle = this.ParentPrivate.GetChild(rowCount + 1 + 1).Bounds;
							rectangle.Y -= this.owner.Height;
							rectangle.Height = this.owner.Height;
						}
						else if (this.owner.Index >= this.owner.DataGridView.FirstDisplayedScrollingRowIndex && this.owner.Index < this.owner.DataGridView.FirstDisplayedScrollingRowIndex + this.owner.DataGridView.DisplayedRowCount(true))
						{
							rectangle = this.owner.DataGridView.GetRowDisplayRectangle(this.owner.Index, false);
							rectangle = this.owner.DataGridView.RectangleToScreen(rectangle);
						}
						else
						{
							int num = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, this.owner.Index);
							if (!this.owner.DataGridView.Rows[0].Visible)
							{
								num--;
							}
							if (!this.owner.DataGridView.ColumnHeadersVisible)
							{
								num--;
							}
							rectangle = this.ParentPrivate.GetChild(num).Bounds;
							rectangle.Y += rectangle.Height;
							rectangle.Height = this.owner.Height;
						}
						return rectangle;
					}
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return Rectangle.Empty;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
			}

			// Token: 0x1700164C RID: 5708
			// (get) Token: 0x06006693 RID: 26259 RVA: 0x0017F3F4 File Offset: 0x0017D5F4
			public override string Name
			{
				get
				{
					if (this.owner != null)
					{
						return SR.GetString("DataGridView_AccRowName", new object[]
						{
							(AccessibilityImprovements.Level5 ? ((this.owner.DataGridView != null && this.owner.Visible) ? this.owner.DataGridView.Rows.GetVisibleIndex(this.owner) : -1) : this.owner.Index).ToString(CultureInfo.CurrentCulture)
						});
					}
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return string.Empty;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
			}

			// Token: 0x1700164D RID: 5709
			// (get) Token: 0x06006694 RID: 26260 RVA: 0x0017F492 File Offset: 0x0017D692
			// (set) Token: 0x06006695 RID: 26261 RVA: 0x0017F49A File Offset: 0x0017D69A
			public DataGridViewRow Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					if (this.owner != null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerAlreadySet"));
					}
					this.owner = value;
				}
			}

			// Token: 0x1700164E RID: 5710
			// (get) Token: 0x06006696 RID: 26262 RVA: 0x0017F4BB File Offset: 0x0017D6BB
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentPrivate;
				}
			}

			// Token: 0x1700164F RID: 5711
			// (get) Token: 0x06006697 RID: 26263 RVA: 0x0017F4C3 File Offset: 0x0017D6C3
			private AccessibleObject ParentPrivate
			{
				get
				{
					if (this.owner != null)
					{
						return this.owner.DataGridView.AccessibilityObject;
					}
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
			}

			// Token: 0x17001650 RID: 5712
			// (get) Token: 0x06006698 RID: 26264 RVA: 0x001786EE File Offset: 0x001768EE
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Row;
				}
			}

			// Token: 0x17001651 RID: 5713
			// (get) Token: 0x06006699 RID: 26265 RVA: 0x0017F4F8 File Offset: 0x0017D6F8
			internal override int[] RuntimeId
			{
				get
				{
					if (AccessibilityImprovements.Level3 && this.runtimeId == null)
					{
						this.runtimeId = new int[3];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = (this.IsOwnerRowDestroyed() ? 0 : this.Parent.GetHashCode());
						this.runtimeId[2] = this.GetHashCode();
					}
					return this.runtimeId;
				}
			}

			// Token: 0x17001652 RID: 5714
			// (get) Token: 0x0600669A RID: 26266 RVA: 0x0017F55C File Offset: 0x0017D75C
			private AccessibleObject SelectedCellsAccessibilityObject
			{
				get
				{
					if (this.owner != null)
					{
						if (this.selectedCellsAccessibilityObject == null)
						{
							this.selectedCellsAccessibilityObject = new DataGridViewRow.DataGridViewSelectedRowCellsAccessibleObject(this.owner);
						}
						return this.selectedCellsAccessibilityObject;
					}
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
			}

			// Token: 0x17001653 RID: 5715
			// (get) Token: 0x0600669B RID: 26267 RVA: 0x0017F5AC File Offset: 0x0017D7AC
			public override AccessibleStates State
			{
				get
				{
					if (this.owner != null)
					{
						AccessibleStates accessibleStates = AccessibleStates.Selectable;
						bool flag = true;
						if (this.owner.Selected)
						{
							flag = true;
						}
						else
						{
							for (int i = 0; i < this.owner.Cells.Count; i++)
							{
								if (!this.owner.Cells[i].Selected)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							accessibleStates |= AccessibleStates.Selected;
						}
						if (!this.owner.DataGridView.GetRowDisplayRectangle(this.owner.Index, true).IntersectsWith(this.owner.DataGridView.ClientRectangle))
						{
							accessibleStates |= AccessibleStates.Offscreen;
						}
						return accessibleStates;
					}
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return AccessibleStates.None;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
			}

			// Token: 0x17001654 RID: 5716
			// (get) Token: 0x0600669C RID: 26268 RVA: 0x0017F670 File Offset: 0x0017D870
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.owner == null)
					{
						if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
						{
							return string.Empty;
						}
						throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
					}
					else
					{
						if (this.owner.DataGridView.AllowUserToAddRows && this.owner.Index == this.owner.DataGridView.NewRowIndex)
						{
							return SR.GetString("DataGridView_AccRowCreateNew");
						}
						StringBuilder stringBuilder = new StringBuilder(1024);
						int childCount = this.GetChildCount();
						int num = this.owner.DataGridView.RowHeadersVisible ? 1 : 0;
						for (int i = num; i < childCount; i++)
						{
							AccessibleObject child = this.GetChild(i);
							if (child != null)
							{
								stringBuilder.Append(child.Value);
							}
							if (i != childCount - 1)
							{
								stringBuilder.Append(";");
							}
						}
						return stringBuilder.ToString();
					}
				}
			}

			// Token: 0x17001655 RID: 5717
			// (get) Token: 0x0600669D RID: 26269 RVA: 0x0017F744 File Offset: 0x0017D944
			private int VisibleIndex
			{
				get
				{
					DataGridViewRow dataGridViewRow = this.owner;
					if (((dataGridViewRow != null) ? dataGridViewRow.DataGridView : null) == null)
					{
						return -1;
					}
					if (!this.owner.DataGridView.ColumnHeadersVisible)
					{
						return this.owner.DataGridView.Rows.GetVisibleIndex(this.owner);
					}
					return this.owner.DataGridView.Rows.GetVisibleIndex(this.owner) + 1;
				}
			}

			// Token: 0x0600669E RID: 26270 RVA: 0x0017F7B4 File Offset: 0x0017D9B4
			public override AccessibleObject GetChild(int index)
			{
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				else
				{
					if (AccessibilityImprovements.Level5 && (this.owner.DataGridView == null || index > this.GetChildCount() - 1))
					{
						return null;
					}
					if (index == 0 && this.owner.DataGridView.RowHeadersVisible)
					{
						return this.owner.HeaderCell.AccessibilityObject;
					}
					if (this.owner.DataGridView.RowHeadersVisible)
					{
						index--;
					}
					int index2 = this.owner.DataGridView.Columns.ActualDisplayIndexToColumnIndex(index, DataGridViewElementStates.Visible);
					return this.owner.Cells[index2].AccessibilityObject;
				}
			}

			// Token: 0x0600669F RID: 26271 RVA: 0x0017F880 File Offset: 0x0017DA80
			public override int GetChildCount()
			{
				if (this.owner != null)
				{
					int num = this.owner.DataGridView.Columns.GetColumnCount(DataGridViewElementStates.Visible);
					if (this.owner.DataGridView.RowHeadersVisible)
					{
						num++;
					}
					return num;
				}
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
				{
					return 0;
				}
				throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
			}

			// Token: 0x060066A0 RID: 26272 RVA: 0x0017F8DD File Offset: 0x0017DADD
			public override AccessibleObject GetSelected()
			{
				return this.SelectedCellsAccessibilityObject;
			}

			// Token: 0x060066A1 RID: 26273 RVA: 0x0017F8E8 File Offset: 0x0017DAE8
			public override AccessibleObject GetFocused()
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				else
				{
					if (this.owner.DataGridView.Focused && this.owner.DataGridView.CurrentCell != null && this.owner.DataGridView.CurrentCell.RowIndex == this.owner.Index)
					{
						return this.owner.DataGridView.CurrentCell.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x060066A2 RID: 26274 RVA: 0x0017F974 File Offset: 0x0017DB74
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (this.owner != null)
				{
					switch (navigationDirection)
					{
					case AccessibleNavigation.Up:
					case AccessibleNavigation.Previous:
						if (this.owner.Index != this.owner.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible))
						{
							if (AccessibilityImprovements.Level5)
							{
								return this.owner.DataGridView.AccessibilityObject.GetChild(this.VisibleIndex - 1);
							}
							int previousRow = this.owner.DataGridView.Rows.GetPreviousRow(this.owner.Index, DataGridViewElementStates.Visible);
							int rowCount = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, previousRow);
							if (this.owner.DataGridView.ColumnHeadersVisible)
							{
								return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount + 1);
							}
							return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount);
						}
						else
						{
							if (this.owner.DataGridView.ColumnHeadersVisible)
							{
								return this.ParentPrivate.GetChild(0);
							}
							return null;
						}
						break;
					case AccessibleNavigation.Down:
					case AccessibleNavigation.Next:
						if (this.owner.Index == this.owner.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible))
						{
							return null;
						}
						if (AccessibilityImprovements.Level5)
						{
							int visibleIndex = this.VisibleIndex;
							if (visibleIndex >= 0)
							{
								return this.owner.DataGridView.AccessibilityObject.GetChild(visibleIndex + 1);
							}
							return null;
						}
						else
						{
							int nextRow = this.owner.DataGridView.Rows.GetNextRow(this.owner.Index, DataGridViewElementStates.Visible);
							int rowCount2 = this.owner.DataGridView.Rows.GetRowCount(DataGridViewElementStates.Visible, 0, nextRow);
							if (this.owner.DataGridView.ColumnHeadersVisible)
							{
								return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount2 + 1);
							}
							return this.owner.DataGridView.AccessibilityObject.GetChild(rowCount2);
						}
						break;
					case AccessibleNavigation.FirstChild:
						if (this.GetChildCount() == 0)
						{
							return null;
						}
						return this.GetChild(0);
					case AccessibleNavigation.LastChild:
					{
						int childCount = this.GetChildCount();
						if (childCount == 0)
						{
							return null;
						}
						return this.GetChild(childCount - 1);
					}
					}
					return null;
				}
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
				{
					return null;
				}
				throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
			}

			// Token: 0x060066A3 RID: 26275 RVA: 0x0017FBC0 File Offset: 0x0017DDC0
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				else
				{
					DataGridView dataGridView = this.owner.DataGridView;
					if (dataGridView == null)
					{
						return;
					}
					if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
					{
						dataGridView.FocusInternal();
					}
					if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection && this.owner.Cells.Count > 0)
					{
						if (dataGridView.CurrentCell != null && dataGridView.CurrentCell.OwningColumn != null)
						{
							dataGridView.CurrentCell = this.owner.Cells[dataGridView.CurrentCell.OwningColumn.Index];
						}
						else
						{
							int index = dataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index;
							if (index > -1)
							{
								dataGridView.CurrentCell = this.owner.Cells[index];
							}
						}
					}
					if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection && (flags & AccessibleSelection.TakeSelection) == AccessibleSelection.None && (dataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || dataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect))
					{
						this.owner.Selected = true;
					}
					if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection && (flags & (AccessibleSelection.TakeSelection | AccessibleSelection.AddSelection)) == AccessibleSelection.None)
					{
						this.owner.Selected = false;
					}
					return;
				}
			}

			// Token: 0x060066A4 RID: 26276 RVA: 0x0017FCD4 File Offset: 0x0017DED4
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.Owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewRowAccessibleObject_OwnerNotSet"));
				}
				else
				{
					DataGridView dataGridView = this.Owner.DataGridView;
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.Parent:
						return this.Parent;
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
						return this.Navigate(AccessibleNavigation.Next);
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
						return this.Navigate(AccessibleNavigation.Previous);
					case UnsafeNativeMethods.NavigateDirection.FirstChild:
						return this.Navigate(AccessibleNavigation.FirstChild);
					case UnsafeNativeMethods.NavigateDirection.LastChild:
						return this.Navigate(AccessibleNavigation.LastChild);
					default:
						return null;
					}
				}
			}

			// Token: 0x17001656 RID: 5718
			// (get) Token: 0x060066A5 RID: 26277 RVA: 0x0017FD52 File Offset: 0x0017DF52
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (!this.IsOwnerRowDestroyed())
					{
						return this.Owner.DataGridView.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x060066A6 RID: 26278 RVA: 0x0017FD6E File Offset: 0x0017DF6E
			internal override bool IsPatternSupported(int patternId)
			{
				return !this.IsOwnerRowDestroyed() && patternId.Equals(10018);
			}

			// Token: 0x060066A7 RID: 26279 RVA: 0x0017FD88 File Offset: 0x0017DF88
			internal override object GetPropertyValue(int propertyId)
			{
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyId)
					{
					case 30005:
						return this.Name;
					case 30006:
					case 30011:
					case 30012:
						goto IL_91;
					case 30007:
						return string.Empty;
					case 30008:
					case 30009:
						break;
					case 30010:
						return !this.IsOwnerRowDestroyed() && this.Owner.DataGridView.Enabled;
					case 30013:
						return this.Help ?? string.Empty;
					default:
						if (propertyId != 30019 && propertyId != 30022)
						{
							goto IL_91;
						}
						break;
					}
					return false;
				}
				IL_91:
				return base.GetPropertyValue(propertyId);
			}

			// Token: 0x04003A75 RID: 14965
			private int[] runtimeId;

			// Token: 0x04003A76 RID: 14966
			private DataGridViewRow owner;

			// Token: 0x04003A77 RID: 14967
			private DataGridViewRow.DataGridViewSelectedRowCellsAccessibleObject selectedCellsAccessibilityObject;
		}

		// Token: 0x02000676 RID: 1654
		private class DataGridViewSelectedRowCellsAccessibleObject : AccessibleObject
		{
			// Token: 0x060066A8 RID: 26280 RVA: 0x0017FE2D File Offset: 0x0017E02D
			internal DataGridViewSelectedRowCellsAccessibleObject(DataGridViewRow owner)
			{
				this.owner = owner;
			}

			// Token: 0x060066A9 RID: 26281 RVA: 0x0017FE3C File Offset: 0x0017E03C
			internal void ClearOwnerRow()
			{
				this.owner = null;
			}

			// Token: 0x060066AA RID: 26282 RVA: 0x0017FE45 File Offset: 0x0017E045
			private bool IsOwnerRowDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.owner == null;
			}

			// Token: 0x17001657 RID: 5719
			// (get) Token: 0x060066AB RID: 26283 RVA: 0x0017FE59 File Offset: 0x0017E059
			public override string Name
			{
				get
				{
					return SR.GetString("DataGridView_AccSelectedRowCellsName");
				}
			}

			// Token: 0x17001658 RID: 5720
			// (get) Token: 0x060066AC RID: 26284 RVA: 0x0017FE65 File Offset: 0x0017E065
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (!this.IsOwnerRowDestroyed())
					{
						return this.owner.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x17001659 RID: 5721
			// (get) Token: 0x060066AD RID: 26285 RVA: 0x0017C12B File Offset: 0x0017A32B
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Grouping;
				}
			}

			// Token: 0x1700165A RID: 5722
			// (get) Token: 0x060066AE RID: 26286 RVA: 0x0017C12F File Offset: 0x0017A32F
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Selected | AccessibleStates.Selectable;
				}
			}

			// Token: 0x1700165B RID: 5723
			// (get) Token: 0x060066AF RID: 26287 RVA: 0x000163B4 File Offset: 0x000145B4
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.Name;
				}
			}

			// Token: 0x060066B0 RID: 26288 RVA: 0x0017FE7C File Offset: 0x0017E07C
			public override AccessibleObject GetChild(int index)
			{
				if (this.IsOwnerRowDestroyed())
				{
					return null;
				}
				if (index < this.GetChildCount())
				{
					int num = -1;
					for (int i = 1; i < this.owner.AccessibilityObject.GetChildCount(); i++)
					{
						if ((this.owner.AccessibilityObject.GetChild(i).State & AccessibleStates.Selected) == AccessibleStates.Selected)
						{
							num++;
						}
						if (num == index)
						{
							return this.owner.AccessibilityObject.GetChild(i);
						}
					}
					return null;
				}
				return null;
			}

			// Token: 0x060066B1 RID: 26289 RVA: 0x0017FEF4 File Offset: 0x0017E0F4
			public override int GetChildCount()
			{
				if (this.IsOwnerRowDestroyed())
				{
					return 0;
				}
				int num = 0;
				for (int i = 1; i < this.owner.AccessibilityObject.GetChildCount(); i++)
				{
					if ((this.owner.AccessibilityObject.GetChild(i).State & AccessibleStates.Selected) == AccessibleStates.Selected)
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x060066B2 RID: 26290 RVA: 0x00006C59 File Offset: 0x00004E59
			public override AccessibleObject GetSelected()
			{
				return this;
			}

			// Token: 0x060066B3 RID: 26291 RVA: 0x0017FF48 File Offset: 0x0017E148
			public override AccessibleObject GetFocused()
			{
				if (!this.IsOwnerRowDestroyed() && this.owner.DataGridView.CurrentCell != null && this.owner.DataGridView.CurrentCell.Selected)
				{
					return this.owner.DataGridView.CurrentCell.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060066B4 RID: 26292 RVA: 0x0017FF9D File Offset: 0x0017E19D
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (navigationDirection != AccessibleNavigation.FirstChild)
				{
					if (navigationDirection != AccessibleNavigation.LastChild)
					{
						return null;
					}
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
					return null;
				}
				else
				{
					if (this.GetChildCount() > 0)
					{
						return this.GetChild(0);
					}
					return null;
				}
			}

			// Token: 0x04003A78 RID: 14968
			private DataGridViewRow owner;
		}
	}
}
