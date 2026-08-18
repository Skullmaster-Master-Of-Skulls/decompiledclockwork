using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x020001A3 RID: 419
	[TypeConverter(typeof(DataGridViewCellConverter))]
	public abstract class DataGridViewCell : DataGridViewElement, ICloneable, IDisposable
	{
		// Token: 0x06001D31 RID: 7473 RVA: 0x00089814 File Offset: 0x00087A14
		protected DataGridViewCell()
		{
			if (!DataGridViewCell.isScalingInitialized)
			{
				if (DpiHelper.IsScalingRequired)
				{
					DataGridViewCell.iconsWidth = (byte)DpiHelper.LogicalToDeviceUnitsX(12);
					DataGridViewCell.iconsHeight = (byte)DpiHelper.LogicalToDeviceUnitsY(11);
				}
				DataGridViewCell.isScalingInitialized = true;
			}
			this.propertyStore = new PropertyStore();
			base.StateInternal = DataGridViewElementStates.None;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x00089868 File Offset: 0x00087A68
		~DataGridViewCell()
		{
			this.Dispose(false);
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00089898 File Offset: 0x00087A98
		[Browsable(false)]
		public AccessibleObject AccessibilityObject
		{
			get
			{
				AccessibleObject accessibleObject = (AccessibleObject)this.Properties.GetObject(DataGridViewCell.PropCellAccessibilityObject);
				if (accessibleObject == null)
				{
					accessibleObject = this.CreateAccessibilityInstance();
					this.Properties.SetObject(DataGridViewCell.PropCellAccessibilityObject, accessibleObject);
				}
				return accessibleObject;
			}
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000898D8 File Offset: 0x00087AD8
		internal void ClearAccessibilityObjectOwner()
		{
			object @object = this.Properties.GetObject(DataGridViewCell.PropCellAccessibilityObject);
			DataGridViewCell.DataGridViewCellAccessibleObject dataGridViewCellAccessibleObject = @object as DataGridViewCell.DataGridViewCellAccessibleObject;
			if (dataGridViewCellAccessibleObject != null)
			{
				dataGridViewCellAccessibleObject.ClearOwnerCell();
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00089906 File Offset: 0x00087B06
		public int ColumnIndex
		{
			get
			{
				if (this.owningColumn == null)
				{
					return -1;
				}
				return this.owningColumn.Index;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x0008991D File Offset: 0x00087B1D
		[Browsable(false)]
		public Rectangle ContentBounds
		{
			get
			{
				return this.GetContentBounds(this.RowIndex);
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0008992B File Offset: 0x00087B2B
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x00089939 File Offset: 0x00087B39
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.GetContextMenuStrip(this.RowIndex);
			}
			set
			{
				this.ContextMenuStripInternal = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00089942 File Offset: 0x00087B42
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x0008995C File Offset: 0x00087B5C
		private ContextMenuStrip ContextMenuStripInternal
		{
			get
			{
				return (ContextMenuStrip)this.Properties.GetObject(DataGridViewCell.PropCellContextMenuStrip);
			}
			set
			{
				ContextMenuStrip contextMenuStrip = (ContextMenuStrip)this.Properties.GetObject(DataGridViewCell.PropCellContextMenuStrip);
				if (contextMenuStrip != value)
				{
					EventHandler value2 = new EventHandler(this.DetachContextMenuStrip);
					if (contextMenuStrip != null)
					{
						contextMenuStrip.Disposed -= value2;
					}
					this.Properties.SetObject(DataGridViewCell.PropCellContextMenuStrip, value);
					if (value != null)
					{
						value.Disposed += value2;
					}
					if (base.DataGridView != null)
					{
						base.DataGridView.OnCellContextMenuStripChanged(this);
					}
				}
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000899C9 File Offset: 0x00087BC9
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x000899D4 File Offset: 0x00087BD4
		private byte CurrentMouseLocation
		{
			get
			{
				return this.flags & 3;
			}
			set
			{
				this.flags = (byte)((int)this.flags & -4);
				this.flags |= value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x00015ECC File Offset: 0x000140CC
		[Browsable(false)]
		public virtual object DefaultNewRowValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x000899F8 File Offset: 0x00087BF8
		[Browsable(false)]
		public virtual bool Displayed
		{
			get
			{
				return base.DataGridView != null && (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0) && this.owningColumn.Displayed && this.owningRow.Displayed;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00089A48 File Offset: 0x00087C48
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object EditedFormattedValue
		{
			get
			{
				if (base.DataGridView == null)
				{
					return null;
				}
				DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, this.RowIndex, false);
				return this.GetEditedFormattedValue(this.GetValue(this.RowIndex), this.RowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Formatting);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x00089A89 File Offset: 0x00087C89
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual Type EditType
		{
			get
			{
				return typeof(DataGridViewTextBoxEditingControl);
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00089A95 File Offset: 0x00087C95
		private static Bitmap ErrorBitmap
		{
			get
			{
				if (DataGridViewCell.errorBmp == null)
				{
					DataGridViewCell.errorBmp = DataGridViewCell.GetBitmap("DataGridViewRow.error.bmp");
				}
				return DataGridViewCell.errorBmp;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x00089AB2 File Offset: 0x00087CB2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Rectangle ErrorIconBounds
		{
			get
			{
				return this.GetErrorIconBounds(this.RowIndex);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00089AC0 File Offset: 0x00087CC0
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x00089ACE File Offset: 0x00087CCE
		[Browsable(false)]
		public string ErrorText
		{
			get
			{
				return this.GetErrorText(this.RowIndex);
			}
			set
			{
				this.ErrorTextInternal = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00089AD8 File Offset: 0x00087CD8
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x00089B08 File Offset: 0x00087D08
		private string ErrorTextInternal
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCell.PropCellErrorText);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				string errorTextInternal = this.ErrorTextInternal;
				if (!string.IsNullOrEmpty(value) || this.Properties.ContainsObject(DataGridViewCell.PropCellErrorText))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellErrorText, value);
				}
				if (base.DataGridView != null && !errorTextInternal.Equals(this.ErrorTextInternal))
				{
					base.DataGridView.OnCellErrorTextChanged(this);
				}
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00089B6C File Offset: 0x00087D6C
		[Browsable(false)]
		public object FormattedValue
		{
			get
			{
				if (base.DataGridView == null)
				{
					return null;
				}
				DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, this.RowIndex, false);
				return this.GetFormattedValue(this.RowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Formatting);
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00089BA1 File Offset: 0x00087DA1
		[Browsable(false)]
		public virtual Type FormattedValueType
		{
			get
			{
				return this.ValueType;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00089BAC File Offset: 0x00087DAC
		private TypeConverter FormattedValueTypeConverter
		{
			get
			{
				TypeConverter result = null;
				if (this.FormattedValueType != null)
				{
					if (base.DataGridView != null)
					{
						result = base.DataGridView.GetCachedTypeConverter(this.FormattedValueType);
					}
					else
					{
						result = TypeDescriptor.GetConverter(this.FormattedValueType);
					}
				}
				return result;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00089BF4 File Offset: 0x00087DF4
		[Browsable(false)]
		public virtual bool Frozen
		{
			get
			{
				if (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0)
				{
					return this.owningColumn.Frozen && this.owningRow.Frozen;
				}
				return this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Frozen;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00089C61 File Offset: 0x00087E61
		internal bool HasErrorText
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellErrorText) && this.Properties.GetObject(DataGridViewCell.PropCellErrorText) != null;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00089C8A File Offset: 0x00087E8A
		[Browsable(false)]
		public bool HasStyle
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellStyle) && this.Properties.GetObject(DataGridViewCell.PropCellStyle) != null;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00089CB3 File Offset: 0x00087EB3
		internal bool HasToolTipText
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellToolTipText) && this.Properties.GetObject(DataGridViewCell.PropCellToolTipText) != null;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x00089CDC File Offset: 0x00087EDC
		internal bool HasValue
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellValue) && this.Properties.GetObject(DataGridViewCell.PropCellValue) != null;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x00089D05 File Offset: 0x00087F05
		internal virtual bool HasValueType
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewCell.PropCellValueType) && this.Properties.GetObject(DataGridViewCell.PropCellValueType) != null;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00089D2E File Offset: 0x00087F2E
		[Browsable(false)]
		public DataGridViewElementStates InheritedState
		{
			get
			{
				return this.GetInheritedState(this.RowIndex);
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00089D3C File Offset: 0x00087F3C
		[Browsable(false)]
		public DataGridViewCellStyle InheritedStyle
		{
			get
			{
				return this.GetInheritedStyleInternal(this.RowIndex);
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00089D4C File Offset: 0x00087F4C
		[Browsable(false)]
		public bool IsInEditMode
		{
			get
			{
				if (base.DataGridView == null)
				{
					return false;
				}
				if (this.RowIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
				}
				Point currentCellAddress = base.DataGridView.CurrentCellAddress;
				return currentCellAddress.X != -1 && currentCellAddress.X == this.ColumnIndex && currentCellAddress.Y == this.RowIndex && base.DataGridView.IsCurrentCellInEditMode;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00089DBD File Offset: 0x00087FBD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataGridViewColumn OwningColumn
		{
			get
			{
				return this.owningColumn;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x00089DC5 File Offset: 0x00087FC5
		internal DataGridViewColumn OwningColumnInternal
		{
			set
			{
				this.owningColumn = value;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00089DCE File Offset: 0x00087FCE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataGridViewRow OwningRow
		{
			get
			{
				return this.owningRow;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x00089DD6 File Offset: 0x00087FD6
		internal DataGridViewRow OwningRowInternal
		{
			set
			{
				this.owningRow = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00089DDF File Offset: 0x00087FDF
		[Browsable(false)]
		public Size PreferredSize
		{
			get
			{
				return this.GetPreferredSize(this.RowIndex);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00089DED File Offset: 0x00087FED
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00089DF8 File Offset: 0x00087FF8
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x00089E68 File Offset: 0x00088068
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool ReadOnly
		{
			get
			{
				return (this.State & DataGridViewElementStates.ReadOnly) != DataGridViewElementStates.None || (this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.ReadOnly) || (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0 && this.owningColumn.ReadOnly);
			}
			set
			{
				if (base.DataGridView != null)
				{
					if (this.RowIndex == -1)
					{
						throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
					}
					if (value != this.ReadOnly && !base.DataGridView.ReadOnly)
					{
						base.DataGridView.OnDataGridViewElementStateChanging(this, -1, DataGridViewElementStates.ReadOnly);
						base.DataGridView.SetReadOnlyCellCore(this.ColumnIndex, this.RowIndex, value);
						return;
					}
				}
				else if (this.owningRow == null)
				{
					if (value != this.ReadOnly)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCell_CannotSetReadOnlyState"));
					}
				}
				else
				{
					this.owningRow.SetReadOnlyCellCore(this, value);
				}
			}
		}

		// Token: 0x17000672 RID: 1650
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x00089F01 File Offset: 0x00088101
		internal bool ReadOnlyInternal
		{
			set
			{
				if (value)
				{
					base.StateInternal = (this.State | DataGridViewElementStates.ReadOnly);
				}
				else
				{
					base.StateInternal = (this.State & ~DataGridViewElementStates.ReadOnly);
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.OnDataGridViewElementStateChanged(this, -1, DataGridViewElementStates.ReadOnly);
				}
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00089F3C File Offset: 0x0008813C
		[Browsable(false)]
		public virtual bool Resizable
		{
			get
			{
				return (this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Resizable == DataGridViewTriState.True) || (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0 && this.owningColumn.Resizable == DataGridViewTriState.True);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00089FA1 File Offset: 0x000881A1
		[Browsable(false)]
		public int RowIndex
		{
			get
			{
				if (this.owningRow == null)
				{
					return -1;
				}
				return this.owningRow.Index;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00089FB8 File Offset: 0x000881B8
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0008A028 File Offset: 0x00088228
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return (this.State & DataGridViewElementStates.Selected) != DataGridViewElementStates.None || (this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Selected) || (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0 && this.owningColumn.Selected);
			}
			set
			{
				if (base.DataGridView != null)
				{
					if (this.RowIndex == -1)
					{
						throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
					}
					base.DataGridView.SetSelectedCellCoreInternal(this.ColumnIndex, this.RowIndex, value);
					return;
				}
				else
				{
					if (value)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCell_CannotSetSelectedState"));
					}
					return;
				}
			}
		}

		// Token: 0x17000676 RID: 1654
		// (set) Token: 0x06001D60 RID: 7520 RVA: 0x0008A082 File Offset: 0x00088282
		internal bool SelectedInternal
		{
			set
			{
				if (value)
				{
					base.StateInternal = (this.State | DataGridViewElementStates.Selected);
				}
				else
				{
					base.StateInternal = (this.State & ~DataGridViewElementStates.Selected);
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.OnDataGridViewElementStateChanged(this, -1, DataGridViewElementStates.Selected);
				}
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0008A0BE File Offset: 0x000882BE
		[Browsable(false)]
		public Size Size
		{
			get
			{
				return this.GetSize(this.RowIndex);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0008A0CC File Offset: 0x000882CC
		internal Rectangle StdBorderWidths
		{
			get
			{
				if (base.DataGridView != null)
				{
					DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
					DataGridViewAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, false, false, false, false);
					return this.BorderWidths(advancedBorderStyle);
				}
				return Rectangle.Empty;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0008A10C File Offset: 0x0008830C
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0008A158 File Offset: 0x00088358
		[Browsable(true)]
		public DataGridViewCellStyle Style
		{
			get
			{
				DataGridViewCellStyle dataGridViewCellStyle = (DataGridViewCellStyle)this.Properties.GetObject(DataGridViewCell.PropCellStyle);
				if (dataGridViewCellStyle == null)
				{
					dataGridViewCellStyle = new DataGridViewCellStyle();
					dataGridViewCellStyle.AddScope(base.DataGridView, DataGridViewCellStyleScopes.Cell);
					this.Properties.SetObject(DataGridViewCell.PropCellStyle, dataGridViewCellStyle);
				}
				return dataGridViewCellStyle;
			}
			set
			{
				DataGridViewCellStyle dataGridViewCellStyle = null;
				if (this.HasStyle)
				{
					dataGridViewCellStyle = this.Style;
					dataGridViewCellStyle.RemoveScope(DataGridViewCellStyleScopes.Cell);
				}
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellStyle))
				{
					if (value != null)
					{
						value.AddScope(base.DataGridView, DataGridViewCellStyleScopes.Cell);
					}
					this.Properties.SetObject(DataGridViewCell.PropCellStyle, value);
				}
				if (((dataGridViewCellStyle != null && value == null) || (dataGridViewCellStyle == null && value != null) || (dataGridViewCellStyle != null && value != null && !dataGridViewCellStyle.Equals(this.Style))) && base.DataGridView != null)
				{
					base.DataGridView.OnCellStyleChanged(this);
				}
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x0008A1E7 File Offset: 0x000883E7
		// (set) Token: 0x06001D66 RID: 7526 RVA: 0x0008A1F9 File Offset: 0x000883F9
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
				return this.Properties.GetObject(DataGridViewCell.PropCellTag);
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellTag))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellTag, value);
				}
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0008A221 File Offset: 0x00088421
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x0008A22F File Offset: 0x0008842F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ToolTipText
		{
			get
			{
				return this.GetToolTipText(this.RowIndex);
			}
			set
			{
				this.ToolTipTextInternal = value;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0008A238 File Offset: 0x00088438
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0008A268 File Offset: 0x00088468
		private string ToolTipTextInternal
		{
			get
			{
				object @object = this.Properties.GetObject(DataGridViewCell.PropCellToolTipText);
				if (@object != null)
				{
					return (string)@object;
				}
				return string.Empty;
			}
			set
			{
				string toolTipTextInternal = this.ToolTipTextInternal;
				if (!string.IsNullOrEmpty(value) || this.Properties.ContainsObject(DataGridViewCell.PropCellToolTipText))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellToolTipText, value);
				}
				if (base.DataGridView != null && !toolTipTextInternal.Equals(this.ToolTipTextInternal))
				{
					base.DataGridView.OnCellToolTipTextChanged(this);
				}
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0008A2C9 File Offset: 0x000884C9
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x0008A2D7 File Offset: 0x000884D7
		[Browsable(false)]
		public object Value
		{
			get
			{
				return this.GetValue(this.RowIndex);
			}
			set
			{
				this.SetValue(this.RowIndex, value);
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x0008A2E8 File Offset: 0x000884E8
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x0008A329 File Offset: 0x00088529
		[Browsable(false)]
		public virtual Type ValueType
		{
			get
			{
				Type type = (Type)this.Properties.GetObject(DataGridViewCell.PropCellValueType);
				if (type == null && this.OwningColumn != null)
				{
					type = this.OwningColumn.ValueType;
				}
				return type;
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellValueType))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellValueType, value);
				}
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0008A358 File Offset: 0x00088558
		private TypeConverter ValueTypeConverter
		{
			get
			{
				TypeConverter typeConverter = null;
				if (this.OwningColumn != null)
				{
					typeConverter = this.OwningColumn.BoundColumnConverter;
				}
				if (typeConverter == null && this.ValueType != null)
				{
					if (base.DataGridView != null)
					{
						typeConverter = base.DataGridView.GetCachedTypeConverter(this.ValueType);
					}
					else
					{
						typeConverter = TypeDescriptor.GetConverter(this.ValueType);
					}
				}
				return typeConverter;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0008A3B8 File Offset: 0x000885B8
		[Browsable(false)]
		public virtual bool Visible
		{
			get
			{
				if (base.DataGridView != null && this.RowIndex >= 0 && this.ColumnIndex >= 0)
				{
					return this.owningColumn.Visible && this.owningRow.Visible;
				}
				return this.owningRow != null && (this.owningRow.DataGridView == null || this.RowIndex >= 0) && this.owningRow.Visible;
			}
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0008A428 File Offset: 0x00088628
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			DataGridViewAdvancedCellBorderStyle all = dataGridViewAdvancedBorderStyleInput.All;
			if (all != DataGridViewAdvancedCellBorderStyle.NotSet)
			{
				if (all == DataGridViewAdvancedCellBorderStyle.Single)
				{
					if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
					}
					dataGridViewAdvancedBorderStylePlaceholder.TopInternal = ((isFirstDisplayedRow && singleHorizontalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Single;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
				if (all != DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
				}
			}
			else if (base.DataGridView != null && base.DataGridView.AdvancedCellBorderStyle == dataGridViewAdvancedBorderStyleInput)
			{
				DataGridViewCellBorderStyle cellBorderStyle = base.DataGridView.CellBorderStyle;
				if (cellBorderStyle == DataGridViewCellBorderStyle.SingleVertical)
				{
					if (base.DataGridView.RightToLeftInternal)
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.Single;
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					}
					else
					{
						dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = ((isFirstDisplayedColumn && singleVerticalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
						dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.Single;
					}
					dataGridViewAdvancedBorderStylePlaceholder.TopInternal = DataGridViewAdvancedCellBorderStyle.None;
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.None;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
				if (cellBorderStyle == DataGridViewCellBorderStyle.SingleHorizontal)
				{
					dataGridViewAdvancedBorderStylePlaceholder.LeftInternal = DataGridViewAdvancedCellBorderStyle.None;
					dataGridViewAdvancedBorderStylePlaceholder.RightInternal = DataGridViewAdvancedCellBorderStyle.None;
					dataGridViewAdvancedBorderStylePlaceholder.TopInternal = ((isFirstDisplayedRow && singleHorizontalBorderAdded) ? DataGridViewAdvancedCellBorderStyle.Single : DataGridViewAdvancedCellBorderStyle.None);
					dataGridViewAdvancedBorderStylePlaceholder.BottomInternal = DataGridViewAdvancedCellBorderStyle.Single;
					return dataGridViewAdvancedBorderStylePlaceholder;
				}
			}
			return dataGridViewAdvancedBorderStyleInput;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0008A558 File Offset: 0x00088758
		protected virtual Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			Rectangle result = default(Rectangle);
			result.X = ((advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = result.X;
				result.X = num + 1;
			}
			result.Y = ((advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = result.Y;
				result.Y = num + 1;
			}
			result.Width = ((advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = result.Width;
				result.Width = num + 1;
			}
			result.Height = ((advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.None) ? 0 : 1);
			if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.InsetDouble)
			{
				int num = result.Height;
				result.Height = num + 1;
			}
			if (this.owningColumn != null)
			{
				if (base.DataGridView != null && base.DataGridView.RightToLeftInternal)
				{
					result.X += this.owningColumn.DividerWidth;
				}
				else
				{
					result.Width += this.owningColumn.DividerWidth;
				}
			}
			if (this.owningRow != null)
			{
				result.Height += this.owningRow.DividerHeight;
			}
			return result;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void CacheEditingControl()
		{
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0008A6BC File Offset: 0x000888BC
		internal DataGridViewElementStates CellStateFromColumnRowStates(DataGridViewElementStates rowState)
		{
			DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable | DataGridViewElementStates.Selected;
			DataGridViewElementStates dataGridViewElementStates2 = DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen | DataGridViewElementStates.Visible;
			DataGridViewElementStates dataGridViewElementStates3 = this.owningColumn.State & dataGridViewElementStates;
			dataGridViewElementStates3 |= (rowState & dataGridViewElementStates);
			return dataGridViewElementStates3 | (this.owningColumn.State & dataGridViewElementStates2 & (rowState & dataGridViewElementStates2));
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool ClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0008A6F8 File Offset: 0x000888F8
		internal bool ClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.ClickUnsharesRow(e);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0008A704 File Offset: 0x00088904
		internal void CloneInternal(DataGridViewCell dataGridViewCell)
		{
			if (this.HasValueType)
			{
				dataGridViewCell.ValueType = this.ValueType;
			}
			if (this.HasStyle)
			{
				dataGridViewCell.Style = new DataGridViewCellStyle(this.Style);
			}
			if (this.HasErrorText)
			{
				dataGridViewCell.ErrorText = this.ErrorTextInternal;
			}
			if (this.HasToolTipText)
			{
				dataGridViewCell.ToolTipText = this.ToolTipTextInternal;
			}
			if (this.ContextMenuStripInternal != null)
			{
				dataGridViewCell.ContextMenuStrip = this.ContextMenuStripInternal.Clone();
			}
			dataGridViewCell.StateInternal = (this.State & ~DataGridViewElementStates.Selected);
			dataGridViewCell.Tag = this.Tag;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0008A79C File Offset: 0x0008899C
		public virtual object Clone()
		{
			DataGridViewCell dataGridViewCell = (DataGridViewCell)Activator.CreateInstance(base.GetType());
			this.CloneInternal(dataGridViewCell);
			return dataGridViewCell;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0008A7C4 File Offset: 0x000889C4
		internal static int ColorDistance(Color color1, Color color2)
		{
			int num = (int)(color1.R - color2.R);
			int num2 = (int)(color1.G - color2.G);
			int num3 = (int)(color1.B - color2.B);
			return num * num + num2 * num2 + num3 * num3;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0008A80C File Offset: 0x00088A0C
		internal void ComputeBorderStyleCellStateAndCellBounds(int rowIndex, out DataGridViewAdvancedBorderStyle dgvabsEffective, out DataGridViewElementStates cellState, out Rectangle cellBounds)
		{
			bool singleVerticalBorderAdded = !base.DataGridView.RowHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
			bool singleHorizontalBorderAdded = !base.DataGridView.ColumnHeadersVisible && base.DataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single;
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
			if (rowIndex > -1 && this.OwningColumn != null)
			{
				dgvabsEffective = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded, this.ColumnIndex == base.DataGridView.FirstDisplayedColumnIndex, rowIndex == base.DataGridView.FirstDisplayedRowIndex);
				DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
				cellState = this.CellStateFromColumnRowStates(rowState);
				cellState |= this.State;
			}
			else if (this.OwningColumn != null)
			{
				DataGridViewColumn lastColumn = base.DataGridView.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None);
				bool isLastVisibleColumn = lastColumn != null && lastColumn.Index == this.ColumnIndex;
				dgvabsEffective = base.DataGridView.AdjustColumnHeaderBorderStyle(base.DataGridView.AdvancedColumnHeadersBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, this.ColumnIndex == base.DataGridView.FirstDisplayedColumnIndex, isLastVisibleColumn);
				cellState = (this.OwningColumn.State | this.State);
			}
			else if (this.OwningRow != null)
			{
				dgvabsEffective = this.OwningRow.AdjustRowHeaderBorderStyle(base.DataGridView.AdvancedRowHeadersBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded, rowIndex == base.DataGridView.FirstDisplayedRowIndex, rowIndex == base.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible));
				cellState = (this.OwningRow.GetState(rowIndex) | this.State);
			}
			else
			{
				dgvabsEffective = base.DataGridView.AdjustedTopLeftHeaderBorderStyle;
				cellState = this.State;
			}
			cellBounds = new Rectangle(new Point(0, 0), this.GetSize(rowIndex));
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0008A9DC File Offset: 0x00088BDC
		internal Rectangle ComputeErrorIconBounds(Rectangle cellValueBounds)
		{
			if (cellValueBounds.Width >= (int)(8 + DataGridViewCell.iconsWidth) && cellValueBounds.Height >= (int)(8 + DataGridViewCell.iconsHeight))
			{
				Rectangle result = new Rectangle(base.DataGridView.RightToLeftInternal ? (cellValueBounds.Left + 4) : (cellValueBounds.Right - 4 - (int)DataGridViewCell.iconsWidth), cellValueBounds.Y + (cellValueBounds.Height - (int)DataGridViewCell.iconsHeight) / 2, (int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight);
				return result;
			}
			return Rectangle.Empty;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0008AA5F File Offset: 0x00088C5F
		internal bool ContentClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.ContentClickUnsharesRow(e);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0008AA68 File Offset: 0x00088C68
		internal bool ContentDoubleClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.ContentDoubleClickUnsharesRow(e);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0008AA71 File Offset: 0x00088C71
		protected virtual AccessibleObject CreateAccessibilityInstance()
		{
			return new DataGridViewCell.DataGridViewCellAccessibleObject(this);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0008AA79 File Offset: 0x00088C79
		private void DetachContextMenuStrip(object sender, EventArgs e)
		{
			this.ContextMenuStripInternal = null;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0008AA84 File Offset: 0x00088C84
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void DetachEditingControl()
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			if (dataGridView.EditingControl.ParentInternal != null)
			{
				if (dataGridView.EditingControl.ContainsFocus)
				{
					ContainerControl containerControl = dataGridView.GetContainerControlInternal() as ContainerControl;
					if (containerControl != null && (dataGridView.EditingControl == containerControl.ActiveControl || dataGridView.EditingControl.Contains(containerControl.ActiveControl)))
					{
						dataGridView.FocusInternal();
					}
					else
					{
						UnsafeNativeMethods.SetFocus(new HandleRef(null, IntPtr.Zero));
					}
				}
				dataGridView.EditingPanel.Controls.Remove(dataGridView.EditingControl);
				if (AccessibilityImprovements.Level3 && this.AccessibleRestructuringNeeded)
				{
					dataGridView.EditingControlAccessibleObject.SetParent(null);
					this.AccessibilityObject.SetDetachableChild(null);
					this.AccessibilityObject.RaiseStructureChangedEvent(UnsafeNativeMethods.StructureChangeType.ChildRemoved, dataGridView.EditingControlAccessibleObject.RuntimeId);
				}
			}
			if (dataGridView.EditingPanel.ParentInternal != null)
			{
				((DataGridView.DataGridViewControlCollection)dataGridView.Controls).RemoveInternal(dataGridView.EditingPanel);
			}
			this.CurrentMouseLocation = 0;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0008AB90 File Offset: 0x00088D90
		private bool AccessibleRestructuringNeeded
		{
			get
			{
				Type type = base.DataGridView.EditingControl.GetType();
				return (type == typeof(DataGridViewComboBoxEditingControl) && !type.IsSubclassOf(typeof(DataGridViewComboBoxEditingControl))) || (type == typeof(DataGridViewTextBoxEditingControl) && !type.IsSubclassOf(typeof(DataGridViewTextBoxEditingControl)));
			}
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0008ABFB File Offset: 0x00088DFB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0008AC0C File Offset: 0x00088E0C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ContextMenuStrip contextMenuStripInternal = this.ContextMenuStripInternal;
				if (contextMenuStripInternal != null)
				{
					contextMenuStripInternal.Disposed -= this.DetachContextMenuStrip;
				}
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool DoubleClickUnsharesRow(DataGridViewCellEventArgs e)
		{
			return false;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0008AC38 File Offset: 0x00088E38
		internal bool DoubleClickUnsharesRowInternal(DataGridViewCellEventArgs e)
		{
			return this.DoubleClickUnsharesRow(e);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool EnterUnsharesRow(int rowIndex, bool throughMouseClick)
		{
			return false;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0008AC41 File Offset: 0x00088E41
		internal bool EnterUnsharesRowInternal(int rowIndex, bool throughMouseClick)
		{
			return this.EnterUnsharesRow(rowIndex, throughMouseClick);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0008AC4C File Offset: 0x00088E4C
		internal static void FormatPlainText(string s, bool csv, TextWriter output, ref bool escapeApplied)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				char c = s[i];
				if (c != '\t')
				{
					if (c != '"')
					{
						if (c != ',')
						{
							output.Write(c);
						}
						else
						{
							if (csv)
							{
								escapeApplied = true;
							}
							output.Write(',');
						}
					}
					else if (csv)
					{
						output.Write("\"\"");
						escapeApplied = true;
					}
					else
					{
						output.Write('"');
					}
				}
				else if (!csv)
				{
					output.Write(' ');
				}
				else
				{
					output.Write('\t');
				}
			}
			if (escapeApplied)
			{
				output.Write('"');
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0008ACE0 File Offset: 0x00088EE0
		internal static void FormatPlainTextAsHtml(string s, TextWriter output)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			char c = '\0';
			int i = 0;
			while (i < length)
			{
				char c2 = s[i];
				if (c2 <= ' ')
				{
					if (c2 != '\n')
					{
						if (c2 != '\r')
						{
							if (c2 != ' ')
							{
								goto IL_B7;
							}
							if (c == ' ')
							{
								output.Write("&nbsp;");
							}
							else
							{
								output.Write(c2);
							}
						}
					}
					else
					{
						output.Write("<br>");
					}
				}
				else if (c2 <= '&')
				{
					if (c2 != '"')
					{
						if (c2 != '&')
						{
							goto IL_B7;
						}
						output.Write("&amp;");
					}
					else
					{
						output.Write("&quot;");
					}
				}
				else if (c2 != '<')
				{
					if (c2 != '>')
					{
						goto IL_B7;
					}
					output.Write("&gt;");
				}
				else
				{
					output.Write("&lt;");
				}
				IL_F8:
				c = c2;
				i++;
				continue;
				IL_B7:
				if (c2 >= '\u00a0' && c2 < 'Ā')
				{
					output.Write("&#");
					int num = (int)c2;
					output.Write(num.ToString(NumberFormatInfo.InvariantInfo));
					output.Write(';');
					goto IL_F8;
				}
				output.Write(c2);
				goto IL_F8;
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0008ADF4 File Offset: 0x00088FF4
		private static Bitmap GetBitmap(string bitmapName)
		{
			Bitmap bitmap = new Bitmap(typeof(DataGridViewCell), bitmapName);
			bitmap.MakeTransparent();
			if (DpiHelper.IsScalingRequired)
			{
				Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, new Size((int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight));
				if (bitmap2 != null)
				{
					bitmap.Dispose();
					bitmap = bitmap2;
				}
			}
			return bitmap;
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0008AE44 File Offset: 0x00089044
		protected virtual object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			object obj = null;
			if (base.DataGridView.IsSharedCellSelected(this, rowIndex))
			{
				obj = this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.ClipboardContent);
			}
			StringBuilder stringBuilder = new StringBuilder(64);
			if (string.Equals(format, DataFormats.Html, StringComparison.OrdinalIgnoreCase))
			{
				if (firstCell)
				{
					if (inFirstRow)
					{
						stringBuilder.Append("<TABLE>");
					}
					stringBuilder.Append("<TR>");
				}
				stringBuilder.Append("<TD>");
				if (obj != null)
				{
					DataGridViewCell.FormatPlainTextAsHtml(obj.ToString(), new StringWriter(stringBuilder, CultureInfo.CurrentCulture));
				}
				else
				{
					stringBuilder.Append("&nbsp;");
				}
				stringBuilder.Append("</TD>");
				if (lastCell)
				{
					stringBuilder.Append("</TR>");
					if (inLastRow)
					{
						stringBuilder.Append("</TABLE>");
					}
				}
				return stringBuilder.ToString();
			}
			bool flag = string.Equals(format, DataFormats.CommaSeparatedValue, StringComparison.OrdinalIgnoreCase);
			if (flag || string.Equals(format, DataFormats.Text, StringComparison.OrdinalIgnoreCase) || string.Equals(format, DataFormats.UnicodeText, StringComparison.OrdinalIgnoreCase))
			{
				if (obj != null)
				{
					if (firstCell && lastCell && inFirstRow && inLastRow)
					{
						stringBuilder.Append(obj.ToString());
					}
					else
					{
						bool flag2 = false;
						int length = stringBuilder.Length;
						DataGridViewCell.FormatPlainText(obj.ToString(), flag, new StringWriter(stringBuilder, CultureInfo.CurrentCulture), ref flag2);
						if (flag2)
						{
							stringBuilder.Insert(length, '"');
						}
					}
				}
				if (lastCell)
				{
					if (!inLastRow)
					{
						stringBuilder.Append('\r');
						stringBuilder.Append('\n');
					}
				}
				else
				{
					stringBuilder.Append(flag ? ',' : '\t');
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0008B001 File Offset: 0x00089201
		internal object GetClipboardContentInternal(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
		{
			return this.GetClipboardContent(rowIndex, firstCell, lastCell, inFirstRow, inLastRow, format);
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0008B014 File Offset: 0x00089214
		internal ContextMenuStrip GetContextMenuStrip(int rowIndex)
		{
			ContextMenuStrip contextMenuStrip = this.ContextMenuStripInternal;
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null))
			{
				contextMenuStrip = base.DataGridView.OnCellContextMenuStripNeeded(this.ColumnIndex, rowIndex, contextMenuStrip);
			}
			return contextMenuStrip;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0008B060 File Offset: 0x00089260
		internal void GetContrastedPens(Color baseline, ref Pen darkPen, ref Pen lightPen)
		{
			int num = DataGridViewCell.ColorDistance(baseline, SystemColors.ControlDark);
			int num2 = DataGridViewCell.ColorDistance(baseline, SystemColors.ControlLightLight);
			if (SystemInformation.HighContrast)
			{
				if (num < 2000)
				{
					darkPen = base.DataGridView.GetCachedPen(ControlPaint.DarkDark(baseline));
				}
				else
				{
					darkPen = base.DataGridView.GetCachedPen(SystemColors.ControlDark);
				}
				if (num2 < 2000)
				{
					lightPen = base.DataGridView.GetCachedPen(ControlPaint.LightLight(baseline));
					return;
				}
				lightPen = base.DataGridView.GetCachedPen(SystemColors.ControlLightLight);
				return;
			}
			else
			{
				if (num < 1000)
				{
					darkPen = base.DataGridView.GetCachedPen(ControlPaint.Dark(baseline));
				}
				else
				{
					darkPen = base.DataGridView.GetCachedPen(SystemColors.ControlDark);
				}
				if (num2 < 1000)
				{
					lightPen = base.DataGridView.GetCachedPen(ControlPaint.Light(baseline));
					return;
				}
				lightPen = base.DataGridView.GetCachedPen(SystemColors.ControlLightLight);
				return;
			}
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0008B148 File Offset: 0x00089348
		public Rectangle GetContentBounds(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return Rectangle.Empty;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Rectangle contentBounds;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				contentBounds = this.GetContentBounds(graphics, inheritedStyle, rowIndex);
			}
			return contentBounds;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00054335 File Offset: 0x00052535
		protected virtual Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			return Rectangle.Empty;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0008B19C File Offset: 0x0008939C
		internal object GetEditedFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle dataGridViewCellStyle, DataGridViewDataErrorContexts context)
		{
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (this.ColumnIndex != currentCellAddress.X || rowIndex != currentCellAddress.Y)
			{
				return this.GetFormattedValue(value, rowIndex, ref dataGridViewCellStyle, null, null, context);
			}
			IDataGridViewEditingControl dataGridViewEditingControl = (IDataGridViewEditingControl)base.DataGridView.EditingControl;
			if (dataGridViewEditingControl != null)
			{
				return dataGridViewEditingControl.GetEditingControlFormattedValue(context);
			}
			IDataGridViewEditingCell dataGridViewEditingCell = this as IDataGridViewEditingCell;
			if (dataGridViewEditingCell != null && base.DataGridView.IsCurrentCellInEditMode)
			{
				return dataGridViewEditingCell.GetEditingCellFormattedValue(context);
			}
			return this.GetFormattedValue(value, rowIndex, ref dataGridViewCellStyle, null, null, context);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0008B228 File Offset: 0x00089428
		public object GetEditedFormattedValue(int rowIndex, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			return this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref inheritedStyle, context);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0008B25C File Offset: 0x0008945C
		internal Rectangle GetErrorIconBounds(int rowIndex)
		{
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Rectangle errorIconBounds;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				errorIconBounds = this.GetErrorIconBounds(graphics, inheritedStyle, rowIndex);
			}
			return errorIconBounds;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00054335 File Offset: 0x00052535
		protected virtual Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
		{
			return Rectangle.Empty;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0008B2A0 File Offset: 0x000894A0
		protected internal virtual string GetErrorText(int rowIndex)
		{
			string text = string.Empty;
			object @object = this.Properties.GetObject(DataGridViewCell.PropCellErrorText);
			if (@object != null)
			{
				text = (string)@object;
			}
			else if (base.DataGridView != null && rowIndex != -1 && rowIndex != base.DataGridView.NewRowIndex && this.OwningColumn != null && this.OwningColumn.IsDataBound && base.DataGridView.DataConnection != null)
			{
				text = base.DataGridView.DataConnection.GetError(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex);
			}
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null) && this.ColumnIndex >= 0 && rowIndex >= 0)
			{
				text = base.DataGridView.OnCellErrorTextNeeded(this.ColumnIndex, rowIndex, text);
			}
			return text;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0008B373 File Offset: 0x00089573
		internal object GetFormattedValue(int rowIndex, ref DataGridViewCellStyle cellStyle, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			return this.GetFormattedValue(this.GetValue(rowIndex), rowIndex, ref cellStyle, null, null, context);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0008B394 File Offset: 0x00089594
		protected virtual object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			DataGridViewCellFormattingEventArgs dataGridViewCellFormattingEventArgs = base.DataGridView.OnCellFormatting(this.ColumnIndex, rowIndex, value, this.FormattedValueType, cellStyle);
			cellStyle = dataGridViewCellFormattingEventArgs.CellStyle;
			bool formattingApplied = dataGridViewCellFormattingEventArgs.FormattingApplied;
			object obj = dataGridViewCellFormattingEventArgs.Value;
			bool flag = true;
			if (!formattingApplied && this.FormattedValueType != null && (obj == null || !this.FormattedValueType.IsAssignableFrom(obj.GetType())))
			{
				try
				{
					obj = Formatter.FormatObject(obj, this.FormattedValueType, (valueTypeConverter == null) ? this.ValueTypeConverter : valueTypeConverter, (formattedValueTypeConverter == null) ? this.FormattedValueTypeConverter : formattedValueTypeConverter, cellStyle.Format, cellStyle.FormatProvider, cellStyle.NullValue, cellStyle.DataSourceNullValue);
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsCriticalException(ex))
					{
						throw;
					}
					DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs = new DataGridViewDataErrorEventArgs(ex, this.ColumnIndex, rowIndex, context);
					base.RaiseDataError(dataGridViewDataErrorEventArgs);
					if (dataGridViewDataErrorEventArgs.ThrowException)
					{
						throw dataGridViewDataErrorEventArgs.Exception;
					}
					flag = false;
				}
			}
			if (flag && (obj == null || this.FormattedValueType == null || !this.FormattedValueType.IsAssignableFrom(obj.GetType())))
			{
				if (obj == null && cellStyle.NullValue == null && this.FormattedValueType != null && !typeof(ValueType).IsAssignableFrom(this.FormattedValueType))
				{
					return null;
				}
				Exception exception;
				if (this.FormattedValueType == null)
				{
					exception = new FormatException(SR.GetString("DataGridViewCell_FormattedValueTypeNull"));
				}
				else
				{
					exception = new FormatException(SR.GetString("DataGridViewCell_FormattedValueHasWrongType"));
				}
				DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs2 = new DataGridViewDataErrorEventArgs(exception, this.ColumnIndex, rowIndex, context);
				base.RaiseDataError(dataGridViewDataErrorEventArgs2);
				if (dataGridViewDataErrorEventArgs2.ThrowException)
				{
					throw dataGridViewDataErrorEventArgs2.Exception;
				}
			}
			return obj;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0008B564 File Offset: 0x00089764
		internal static DataGridViewFreeDimension GetFreeDimensionFromConstraint(Size constraintSize)
		{
			if (constraintSize.Width < 0 || constraintSize.Height < 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"constraintSize",
					constraintSize.ToString()
				}));
			}
			if (constraintSize.Width == 0)
			{
				if (constraintSize.Height == 0)
				{
					return DataGridViewFreeDimension.Both;
				}
				return DataGridViewFreeDimension.Width;
			}
			else
			{
				if (constraintSize.Height == 0)
				{
					return DataGridViewFreeDimension.Height;
				}
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"constraintSize",
					constraintSize.ToString()
				}));
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0008B601 File Offset: 0x00089801
		internal int GetHeight(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return -1;
			}
			return this.owningRow.GetHeight(rowIndex);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0008B61C File Offset: 0x0008981C
		public virtual ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
		{
			if (base.DataGridView != null)
			{
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (this.ColumnIndex < 0)
				{
					throw new InvalidOperationException();
				}
			}
			ContextMenuStrip contextMenuStrip = this.GetContextMenuStrip(rowIndex);
			if (contextMenuStrip != null)
			{
				return contextMenuStrip;
			}
			if (this.owningRow != null)
			{
				contextMenuStrip = this.owningRow.GetContextMenuStrip(rowIndex);
				if (contextMenuStrip != null)
				{
					return contextMenuStrip;
				}
			}
			if (this.owningColumn != null)
			{
				contextMenuStrip = this.owningColumn.ContextMenuStrip;
				if (contextMenuStrip != null)
				{
					return contextMenuStrip;
				}
			}
			if (base.DataGridView != null)
			{
				return base.DataGridView.ContextMenuStrip;
			}
			return null;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0008B6B8 File Offset: 0x000898B8
		public virtual DataGridViewElementStates GetInheritedState(int rowIndex)
		{
			DataGridViewElementStates dataGridViewElementStates = this.State | DataGridViewElementStates.ResizableSet;
			if (base.DataGridView == null)
			{
				if (rowIndex != -1)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owningRow != null)
				{
					dataGridViewElementStates |= (this.owningRow.GetState(-1) & (DataGridViewElementStates.Frozen | DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible));
					if (this.owningRow.GetResizable(rowIndex) == DataGridViewTriState.True)
					{
						dataGridViewElementStates |= DataGridViewElementStates.Resizable;
					}
				}
				return dataGridViewElementStates;
			}
			else
			{
				if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (base.DataGridView.Rows.SharedRow(rowIndex) != this.owningRow)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"rowIndex",
						rowIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				DataGridViewElementStates rowState = base.DataGridView.Rows.GetRowState(rowIndex);
				dataGridViewElementStates |= (rowState & (DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Selected));
				dataGridViewElementStates |= (this.owningColumn.State & (DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Selected));
				if (this.owningRow.GetResizable(rowIndex) == DataGridViewTriState.True || this.owningColumn.Resizable == DataGridViewTriState.True)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (this.owningColumn.Visible && this.owningRow.GetVisible(rowIndex))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
					if (this.owningColumn.Displayed && this.owningRow.GetDisplayed(rowIndex))
					{
						dataGridViewElementStates |= DataGridViewElementStates.Displayed;
					}
				}
				if (this.owningColumn.Frozen && this.owningRow.GetFrozen(rowIndex))
				{
					dataGridViewElementStates |= DataGridViewElementStates.Frozen;
				}
				return dataGridViewElementStates;
			}
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0008B84C File Offset: 0x00089A4C
		public virtual DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_CellNeedsDataGridViewForInheritedStyle"));
			}
			if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			if (this.ColumnIndex < 0)
			{
				throw new InvalidOperationException();
			}
			DataGridViewCellStyle dataGridViewCellStyle;
			if (inheritedCellStyle == null)
			{
				dataGridViewCellStyle = base.DataGridView.PlaceholderCellStyle;
				if (!includeColors)
				{
					dataGridViewCellStyle.BackColor = Color.Empty;
					dataGridViewCellStyle.ForeColor = Color.Empty;
					dataGridViewCellStyle.SelectionBackColor = Color.Empty;
					dataGridViewCellStyle.SelectionForeColor = Color.Empty;
				}
			}
			else
			{
				dataGridViewCellStyle = inheritedCellStyle;
			}
			DataGridViewCellStyle dataGridViewCellStyle2 = null;
			if (this.HasStyle)
			{
				dataGridViewCellStyle2 = this.Style;
			}
			DataGridViewCellStyle dataGridViewCellStyle3 = null;
			if (base.DataGridView.Rows.SharedRow(rowIndex).HasDefaultCellStyle)
			{
				dataGridViewCellStyle3 = base.DataGridView.Rows.SharedRow(rowIndex).DefaultCellStyle;
			}
			DataGridViewCellStyle dataGridViewCellStyle4 = null;
			if (this.owningColumn.HasDefaultCellStyle)
			{
				dataGridViewCellStyle4 = this.owningColumn.DefaultCellStyle;
			}
			DataGridViewCellStyle defaultCellStyle = base.DataGridView.DefaultCellStyle;
			if (includeColors)
			{
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle2.BackColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle3.BackColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.BackColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.BackColor.IsEmpty))
				{
					dataGridViewCellStyle.BackColor = base.DataGridView.RowsDefaultCellStyle.BackColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = base.DataGridView.AlternatingRowsDefaultCellStyle.BackColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.BackColor.IsEmpty)
				{
					dataGridViewCellStyle.BackColor = dataGridViewCellStyle4.BackColor;
				}
				else
				{
					dataGridViewCellStyle.BackColor = defaultCellStyle.BackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle2.ForeColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle3.ForeColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.ForeColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.ForeColor.IsEmpty))
				{
					dataGridViewCellStyle.ForeColor = base.DataGridView.RowsDefaultCellStyle.ForeColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = base.DataGridView.AlternatingRowsDefaultCellStyle.ForeColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.ForeColor.IsEmpty)
				{
					dataGridViewCellStyle.ForeColor = dataGridViewCellStyle4.ForeColor;
				}
				else
				{
					dataGridViewCellStyle.ForeColor = defaultCellStyle.ForeColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle2.SelectionBackColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle3.SelectionBackColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.SelectionBackColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty))
				{
					dataGridViewCellStyle.SelectionBackColor = base.DataGridView.RowsDefaultCellStyle.SelectionBackColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.SelectionBackColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionBackColor = dataGridViewCellStyle4.SelectionBackColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionBackColor = defaultCellStyle.SelectionBackColor;
				}
				if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle2.SelectionForeColor;
				}
				else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle3.SelectionForeColor;
				}
				else if (!base.DataGridView.RowsDefaultCellStyle.SelectionForeColor.IsEmpty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty))
				{
					dataGridViewCellStyle.SelectionForeColor = base.DataGridView.RowsDefaultCellStyle.SelectionForeColor;
				}
				else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = base.DataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor;
				}
				else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.SelectionForeColor.IsEmpty)
				{
					dataGridViewCellStyle.SelectionForeColor = dataGridViewCellStyle4.SelectionForeColor;
				}
				else
				{
					dataGridViewCellStyle.SelectionForeColor = defaultCellStyle.SelectionForeColor;
				}
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle2.Font;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle3.Font;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Font != null && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Font == null))
			{
				dataGridViewCellStyle.Font = base.DataGridView.RowsDefaultCellStyle.Font;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Font != null)
			{
				dataGridViewCellStyle.Font = base.DataGridView.AlternatingRowsDefaultCellStyle.Font;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Font != null)
			{
				dataGridViewCellStyle.Font = dataGridViewCellStyle4.Font;
			}
			else
			{
				dataGridViewCellStyle.Font = defaultCellStyle.Font;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle2.NullValue;
			}
			else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle3.NullValue;
			}
			else if (!base.DataGridView.RowsDefaultCellStyle.IsNullValueDefault && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.IsNullValueDefault))
			{
				dataGridViewCellStyle.NullValue = base.DataGridView.RowsDefaultCellStyle.NullValue;
			}
			else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = base.DataGridView.AlternatingRowsDefaultCellStyle.NullValue;
			}
			else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.IsNullValueDefault)
			{
				dataGridViewCellStyle.NullValue = dataGridViewCellStyle4.NullValue;
			}
			else
			{
				dataGridViewCellStyle.NullValue = defaultCellStyle.NullValue;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle2.DataSourceNullValue;
			}
			else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle3.DataSourceNullValue;
			}
			else if (!base.DataGridView.RowsDefaultCellStyle.IsDataSourceNullValueDefault && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault))
			{
				dataGridViewCellStyle.DataSourceNullValue = base.DataGridView.RowsDefaultCellStyle.DataSourceNullValue;
			}
			else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = base.DataGridView.AlternatingRowsDefaultCellStyle.DataSourceNullValue;
			}
			else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.IsDataSourceNullValueDefault)
			{
				dataGridViewCellStyle.DataSourceNullValue = dataGridViewCellStyle4.DataSourceNullValue;
			}
			else
			{
				dataGridViewCellStyle.DataSourceNullValue = defaultCellStyle.DataSourceNullValue;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle2.Format;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle3.Format;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Format.Length != 0 && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Format.Length == 0))
			{
				dataGridViewCellStyle.Format = base.DataGridView.RowsDefaultCellStyle.Format;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = base.DataGridView.AlternatingRowsDefaultCellStyle.Format;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Format.Length != 0)
			{
				dataGridViewCellStyle.Format = dataGridViewCellStyle4.Format;
			}
			else
			{
				dataGridViewCellStyle.Format = defaultCellStyle.Format;
			}
			if (dataGridViewCellStyle2 != null && !dataGridViewCellStyle2.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle2.FormatProvider;
			}
			else if (dataGridViewCellStyle3 != null && !dataGridViewCellStyle3.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle3.FormatProvider;
			}
			else if (!base.DataGridView.RowsDefaultCellStyle.IsFormatProviderDefault && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.IsFormatProviderDefault))
			{
				dataGridViewCellStyle.FormatProvider = base.DataGridView.RowsDefaultCellStyle.FormatProvider;
			}
			else if (rowIndex % 2 == 1 && !base.DataGridView.AlternatingRowsDefaultCellStyle.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = base.DataGridView.AlternatingRowsDefaultCellStyle.FormatProvider;
			}
			else if (dataGridViewCellStyle4 != null && !dataGridViewCellStyle4.IsFormatProviderDefault)
			{
				dataGridViewCellStyle.FormatProvider = dataGridViewCellStyle4.FormatProvider;
			}
			else
			{
				dataGridViewCellStyle.FormatProvider = defaultCellStyle.FormatProvider;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle2.Alignment;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle3.Alignment;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet))
			{
				dataGridViewCellStyle.AlignmentInternal = base.DataGridView.RowsDefaultCellStyle.Alignment;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = base.DataGridView.AlternatingRowsDefaultCellStyle.Alignment;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Alignment != DataGridViewContentAlignment.NotSet)
			{
				dataGridViewCellStyle.AlignmentInternal = dataGridViewCellStyle4.Alignment;
			}
			else
			{
				dataGridViewCellStyle.AlignmentInternal = defaultCellStyle.Alignment;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle2.WrapMode;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle3.WrapMode;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.WrapMode == DataGridViewTriState.NotSet))
			{
				dataGridViewCellStyle.WrapModeInternal = base.DataGridView.RowsDefaultCellStyle.WrapMode;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = base.DataGridView.AlternatingRowsDefaultCellStyle.WrapMode;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.WrapMode != DataGridViewTriState.NotSet)
			{
				dataGridViewCellStyle.WrapModeInternal = dataGridViewCellStyle4.WrapMode;
			}
			else
			{
				dataGridViewCellStyle.WrapModeInternal = defaultCellStyle.WrapMode;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle2.Tag;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle3.Tag;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Tag != null && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Tag == null))
			{
				dataGridViewCellStyle.Tag = base.DataGridView.RowsDefaultCellStyle.Tag;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Tag != null)
			{
				dataGridViewCellStyle.Tag = base.DataGridView.AlternatingRowsDefaultCellStyle.Tag;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Tag != null)
			{
				dataGridViewCellStyle.Tag = dataGridViewCellStyle4.Tag;
			}
			else
			{
				dataGridViewCellStyle.Tag = defaultCellStyle.Tag;
			}
			if (dataGridViewCellStyle2 != null && dataGridViewCellStyle2.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle2.Padding;
			}
			else if (dataGridViewCellStyle3 != null && dataGridViewCellStyle3.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle3.Padding;
			}
			else if (base.DataGridView.RowsDefaultCellStyle.Padding != Padding.Empty && (rowIndex % 2 == 0 || base.DataGridView.AlternatingRowsDefaultCellStyle.Padding == Padding.Empty))
			{
				dataGridViewCellStyle.PaddingInternal = base.DataGridView.RowsDefaultCellStyle.Padding;
			}
			else if (rowIndex % 2 == 1 && base.DataGridView.AlternatingRowsDefaultCellStyle.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = base.DataGridView.AlternatingRowsDefaultCellStyle.Padding;
			}
			else if (dataGridViewCellStyle4 != null && dataGridViewCellStyle4.Padding != Padding.Empty)
			{
				dataGridViewCellStyle.PaddingInternal = dataGridViewCellStyle4.Padding;
			}
			else
			{
				dataGridViewCellStyle.PaddingInternal = defaultCellStyle.Padding;
			}
			return dataGridViewCellStyle;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0008C50A File Offset: 0x0008A70A
		internal DataGridViewCellStyle GetInheritedStyleInternal(int rowIndex)
		{
			return this.GetInheritedStyle(null, rowIndex, true);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0008C518 File Offset: 0x0008A718
		internal int GetPreferredHeight(int rowIndex, int width)
		{
			if (base.DataGridView == null)
			{
				return -1;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			int height;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				height = this.GetPreferredSize(graphics, inheritedStyle, rowIndex, new Size(width, 0)).Height;
			}
			return height;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0008C578 File Offset: 0x0008A778
		internal Size GetPreferredSize(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			Size preferredSize;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				preferredSize = this.GetPreferredSize(graphics, inheritedStyle, rowIndex, Size.Empty);
			}
			return preferredSize;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0008C5D4 File Offset: 0x0008A7D4
		protected virtual Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
		{
			return new Size(-1, -1);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0008C5E0 File Offset: 0x0008A7E0
		internal static int GetPreferredTextHeight(Graphics g, bool rightToLeft, string text, DataGridViewCellStyle cellStyle, int maxWidth, out bool widthTruncated)
		{
			TextFormatFlags textFormatFlags = DataGridViewUtilities.ComputeTextFormatFlagsForCellStyleAlignment(rightToLeft, cellStyle.Alignment, cellStyle.WrapMode);
			if (cellStyle.WrapMode == DataGridViewTriState.True)
			{
				return DataGridViewCell.MeasureTextHeight(g, text, cellStyle.Font, maxWidth, textFormatFlags, out widthTruncated);
			}
			Size size = DataGridViewCell.MeasureTextSize(g, text, cellStyle.Font, textFormatFlags);
			widthTruncated = (size.Width > maxWidth);
			return size.Height;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0008C640 File Offset: 0x0008A840
		internal int GetPreferredWidth(int rowIndex, int height)
		{
			if (base.DataGridView == null)
			{
				return -1;
			}
			DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
			int width;
			using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
			{
				width = this.GetPreferredSize(graphics, inheritedStyle, rowIndex, new Size(0, height)).Width;
			}
			return width;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0008C6A0 File Offset: 0x0008A8A0
		protected virtual Size GetSize(int rowIndex)
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			if (rowIndex == -1)
			{
				throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertyGetOnSharedCell", new object[]
				{
					"Size"
				}));
			}
			return new Size(this.owningColumn.Thickness, this.owningRow.GetHeight(rowIndex));
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x0008C6FC File Offset: 0x0008A8FC
		private string GetToolTipText(int rowIndex)
		{
			string text = this.ToolTipTextInternal;
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null))
			{
				text = base.DataGridView.OnCellToolTipTextNeeded(this.ColumnIndex, rowIndex, text);
			}
			return text;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x0008C748 File Offset: 0x0008A948
		protected virtual object GetValue(int rowIndex)
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView != null)
			{
				if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
				{
					throw new ArgumentOutOfRangeException("rowIndex");
				}
				if (this.ColumnIndex < 0)
				{
					throw new InvalidOperationException();
				}
			}
			if (dataGridView == null || (dataGridView.AllowUserToAddRowsInternal && rowIndex > -1 && rowIndex == dataGridView.NewRowIndex && rowIndex != dataGridView.CurrentCellAddress.Y) || (!dataGridView.VirtualMode && this.OwningColumn != null && !this.OwningColumn.IsDataBound) || rowIndex == -1 || this.ColumnIndex == -1)
			{
				return this.Properties.GetObject(DataGridViewCell.PropCellValue);
			}
			if (this.OwningColumn == null || !this.OwningColumn.IsDataBound)
			{
				return dataGridView.OnCellValueNeeded(this.ColumnIndex, rowIndex);
			}
			DataGridView.DataGridViewDataConnection dataConnection = dataGridView.DataConnection;
			if (dataConnection == null)
			{
				return null;
			}
			if (dataConnection.CurrencyManager.Count <= rowIndex)
			{
				return this.Properties.GetObject(DataGridViewCell.PropCellValue);
			}
			return dataConnection.GetValue(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0008C855 File Offset: 0x0008AA55
		internal object GetValueInternal(int rowIndex)
		{
			return this.GetValue(rowIndex);
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0008C860 File Offset: 0x0008AA60
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
			{
				throw new InvalidOperationException();
			}
			if (dataGridView.EditingControl.ParentInternal == null)
			{
				dataGridView.EditingControl.CausesValidation = dataGridView.CausesValidation;
				dataGridView.EditingPanel.CausesValidation = dataGridView.CausesValidation;
				dataGridView.EditingControl.Visible = true;
				dataGridView.EditingPanel.Visible = false;
				dataGridView.Controls.Add(dataGridView.EditingPanel);
				dataGridView.EditingPanel.Controls.Add(dataGridView.EditingControl);
			}
			if (AccessibilityImprovements.Level3 && this.AccessibleRestructuringNeeded)
			{
				dataGridView.EditingControlAccessibleObject.SetParent(this.AccessibilityObject);
				this.AccessibilityObject.SetDetachableChild(dataGridView.EditingControl.AccessibilityObject);
				this.AccessibilityObject.RaiseStructureChangedEvent(UnsafeNativeMethods.StructureChangeType.ChildAdded, dataGridView.EditingControlAccessibleObject.RuntimeId);
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x0008C941 File Offset: 0x0008AB41
		internal bool KeyDownUnsharesRowInternal(KeyEventArgs e, int rowIndex)
		{
			return this.KeyDownUnsharesRow(e, rowIndex);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual bool KeyEntersEditMode(KeyEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool KeyPressUnsharesRow(KeyPressEventArgs e, int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0008C94B File Offset: 0x0008AB4B
		internal bool KeyPressUnsharesRowInternal(KeyPressEventArgs e, int rowIndex)
		{
			return this.KeyPressUnsharesRow(e, rowIndex);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0008C955 File Offset: 0x0008AB55
		internal bool KeyUpUnsharesRowInternal(KeyEventArgs e, int rowIndex)
		{
			return this.KeyUpUnsharesRow(e, rowIndex);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool LeaveUnsharesRow(int rowIndex, bool throughMouseClick)
		{
			return false;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0008C95F File Offset: 0x0008AB5F
		internal bool LeaveUnsharesRowInternal(int rowIndex, bool throughMouseClick)
		{
			return this.LeaveUnsharesRow(rowIndex, throughMouseClick);
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0008C96C File Offset: 0x0008AB6C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags)
		{
			bool flag;
			return DataGridViewCell.MeasureTextHeight(graphics, text, font, maxWidth, flags, out flag);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0008C988 File Offset: 0x0008AB88
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags, out bool widthTruncated)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (maxWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("maxWidth", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"maxWidth",
					maxWidth.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!DataGridViewUtilities.ValidTextFormatFlags(flags))
			{
				throw new InvalidEnumArgumentException("flags", (int)flags, typeof(TextFormatFlags));
			}
			flags &= (TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak);
			Size size = TextRenderer.MeasureText(text, font, new Size(maxWidth, int.MaxValue), flags);
			widthTruncated = (size.Width > maxWidth);
			return size.Height;
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0008CA4C File Offset: 0x0008AC4C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Size MeasureTextPreferredSize(Graphics graphics, string text, Font font, float maxRatio, TextFormatFlags flags)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (maxRatio <= 0f)
			{
				throw new ArgumentOutOfRangeException("maxRatio", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"maxRatio",
					maxRatio.ToString(CultureInfo.CurrentCulture),
					"0.0"
				}));
			}
			if (!DataGridViewUtilities.ValidTextFormatFlags(flags))
			{
				throw new InvalidEnumArgumentException("flags", (int)flags, typeof(TextFormatFlags));
			}
			if (string.IsNullOrEmpty(text))
			{
				return new Size(0, 0);
			}
			Size result = DataGridViewCell.MeasureTextSize(graphics, text, font, flags);
			if ((float)(result.Width / result.Height) <= maxRatio)
			{
				return result;
			}
			flags &= (TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak);
			float num = (float)(result.Width * result.Width) / (float)result.Height / maxRatio * 1.1f;
			Size result2;
			for (;;)
			{
				result2 = TextRenderer.MeasureText(text, font, new Size((int)num, int.MaxValue), flags);
				if ((float)(result2.Width / result2.Height) <= maxRatio || result2.Width > (int)num)
				{
					break;
				}
				num = (float)result2.Width * 0.9f;
				if (num <= 1f)
				{
					return result2;
				}
			}
			return result2;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0008CB84 File Offset: 0x0008AD84
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Size MeasureTextSize(Graphics graphics, string text, Font font, TextFormatFlags flags)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (!DataGridViewUtilities.ValidTextFormatFlags(flags))
			{
				throw new InvalidEnumArgumentException("flags", (int)flags, typeof(TextFormatFlags));
			}
			flags &= (TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak);
			return TextRenderer.MeasureText(text, font, new Size(int.MaxValue, int.MaxValue), flags);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0008CBEC File Offset: 0x0008ADEC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static int MeasureTextWidth(Graphics graphics, string text, Font font, int maxHeight, TextFormatFlags flags)
		{
			if (maxHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("maxHeight", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"maxHeight",
					maxHeight.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}));
			}
			Size size = DataGridViewCell.MeasureTextSize(graphics, text, font, flags);
			if (size.Height >= maxHeight || (flags & TextFormatFlags.SingleLine) != TextFormatFlags.Default)
			{
				return size.Width;
			}
			flags &= (TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak);
			int num = size.Width;
			float num2 = (float)num * 0.9f;
			for (;;)
			{
				Size size2 = TextRenderer.MeasureText(text, font, new Size((int)num2, maxHeight), flags);
				if (size2.Height > maxHeight || size2.Width > (int)num2)
				{
					break;
				}
				num = (int)num2;
				num2 = (float)size2.Width * 0.9f;
				if (num2 <= 1f)
				{
					return num;
				}
			}
			return num;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool MouseClickUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x0008CCC4 File Offset: 0x0008AEC4
		internal bool MouseClickUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseClickUnsharesRow(e);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool MouseDoubleClickUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0008CCCD File Offset: 0x0008AECD
		internal bool MouseDoubleClickUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseDoubleClickUnsharesRow(e);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0008CCD6 File Offset: 0x0008AED6
		internal bool MouseDownUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseDownUnsharesRow(e);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool MouseEnterUnsharesRow(int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0008CCDF File Offset: 0x0008AEDF
		internal bool MouseEnterUnsharesRowInternal(int rowIndex)
		{
			return this.MouseEnterUnsharesRow(rowIndex);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool MouseLeaveUnsharesRow(int rowIndex)
		{
			return false;
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0008CCE8 File Offset: 0x0008AEE8
		internal bool MouseLeaveUnsharesRowInternal(int rowIndex)
		{
			return this.MouseLeaveUnsharesRow(rowIndex);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0008CCF1 File Offset: 0x0008AEF1
		internal bool MouseMoveUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseMoveUnsharesRow(e);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected virtual bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
		{
			return false;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0008CCFA File Offset: 0x0008AEFA
		internal bool MouseUpUnsharesRowInternal(DataGridViewCellMouseEventArgs e)
		{
			return this.MouseUpUnsharesRow(e);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0008CD04 File Offset: 0x0008AF04
		private void OnCellDataAreaMouseEnterInternal(int rowIndex)
		{
			if (!base.DataGridView.ShowCellToolTips)
			{
				return;
			}
			Point currentCellAddress = base.DataGridView.CurrentCellAddress;
			if (currentCellAddress.X != -1 && currentCellAddress.X == this.ColumnIndex && currentCellAddress.Y == rowIndex && base.DataGridView.EditingControl != null)
			{
				return;
			}
			string text = this.GetToolTipText(rowIndex);
			if (string.IsNullOrEmpty(text))
			{
				if (!(this.FormattedValueType == DataGridViewCell.stringType))
				{
					goto IL_1E5;
				}
				if (rowIndex != -1 && this.OwningColumn != null)
				{
					int preferredWidth = this.GetPreferredWidth(rowIndex, this.OwningRow.Height);
					int preferredHeight = this.GetPreferredHeight(rowIndex, this.OwningColumn.Width);
					if (this.OwningColumn.Width >= preferredWidth && this.OwningRow.Height >= preferredHeight)
					{
						goto IL_1E5;
					}
					DataGridViewCellStyle inheritedStyle = this.GetInheritedStyle(null, rowIndex, false);
					string text2 = this.GetEditedFormattedValue(this.GetValue(rowIndex), rowIndex, ref inheritedStyle, DataGridViewDataErrorContexts.Display) as string;
					if (!string.IsNullOrEmpty(text2))
					{
						text = DataGridViewCell.TruncateToolTipText(text2);
						goto IL_1E5;
					}
					goto IL_1E5;
				}
				else
				{
					if ((rowIndex == -1 || this.OwningRow == null || !base.DataGridView.RowHeadersVisible || base.DataGridView.RowHeadersWidth <= 0 || this.OwningColumn != null) && rowIndex != -1)
					{
						goto IL_1E5;
					}
					string text3 = this.GetValue(rowIndex) as string;
					if (string.IsNullOrEmpty(text3))
					{
						goto IL_1E5;
					}
					DataGridViewCellStyle inheritedStyle2 = this.GetInheritedStyle(null, rowIndex, false);
					using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
					{
						Rectangle contentBounds = this.GetContentBounds(graphics, inheritedStyle2, rowIndex);
						bool flag = false;
						int num = 0;
						if (contentBounds.Width > 0)
						{
							num = DataGridViewCell.GetPreferredTextHeight(graphics, base.DataGridView.RightToLeftInternal, text3, inheritedStyle2, contentBounds.Width, out flag);
						}
						else
						{
							flag = true;
						}
						if (num > contentBounds.Height || flag)
						{
							text = DataGridViewCell.TruncateToolTipText(text3);
						}
						goto IL_1E5;
					}
				}
			}
			if (base.DataGridView.IsRestricted)
			{
				text = DataGridViewCell.TruncateToolTipText(text);
			}
			IL_1E5:
			if (!string.IsNullOrEmpty(text))
			{
				base.DataGridView.ActivateToolTip(true, text, this.ColumnIndex, rowIndex);
			}
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0008CF24 File Offset: 0x0008B124
		private void OnCellDataAreaMouseLeaveInternal()
		{
			if (base.DataGridView.IsDisposed)
			{
				return;
			}
			base.DataGridView.ActivateToolTip(false, string.Empty, -1, -1);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0008CF48 File Offset: 0x0008B148
		private void OnCellErrorAreaMouseEnterInternal(int rowIndex)
		{
			string errorText = this.GetErrorText(rowIndex);
			base.DataGridView.ActivateToolTip(true, errorText, this.ColumnIndex, rowIndex);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0008CF71 File Offset: 0x0008B171
		private void OnCellErrorAreaMouseLeaveInternal()
		{
			base.DataGridView.ActivateToolTip(false, string.Empty, -1, -1);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0008CF86 File Offset: 0x0008B186
		internal void OnClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnClick(e);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0008CF90 File Offset: 0x0008B190
		internal void OnCommonChange()
		{
			if (base.DataGridView != null && !base.DataGridView.IsDisposed && !base.DataGridView.Disposing)
			{
				if (this.RowIndex == -1)
				{
					base.DataGridView.OnColumnCommonChange(this.ColumnIndex);
					return;
				}
				base.DataGridView.OnCellCommonChange(this.ColumnIndex, this.RowIndex);
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnContentClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0008CFF1 File Offset: 0x0008B1F1
		internal void OnContentClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnContentClick(e);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnContentDoubleClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0008CFFA File Offset: 0x0008B1FA
		internal void OnContentDoubleClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnContentDoubleClick(e);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnDoubleClick(DataGridViewCellEventArgs e)
		{
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0008D003 File Offset: 0x0008B203
		internal void OnDoubleClickInternal(DataGridViewCellEventArgs e)
		{
			this.OnDoubleClick(e);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnEnter(int rowIndex, bool throughMouseClick)
		{
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0008D00C File Offset: 0x0008B20C
		internal void OnEnterInternal(int rowIndex, bool throughMouseClick)
		{
			this.OnEnter(rowIndex, throughMouseClick);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0008D016 File Offset: 0x0008B216
		internal void OnKeyDownInternal(KeyEventArgs e, int rowIndex)
		{
			this.OnKeyDown(e, rowIndex);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnKeyDown(KeyEventArgs e, int rowIndex)
		{
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0008D020 File Offset: 0x0008B220
		internal void OnKeyPressInternal(KeyPressEventArgs e, int rowIndex)
		{
			this.OnKeyPress(e, rowIndex);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnKeyPress(KeyPressEventArgs e, int rowIndex)
		{
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnKeyUp(KeyEventArgs e, int rowIndex)
		{
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0008D02A File Offset: 0x0008B22A
		internal void OnKeyUpInternal(KeyEventArgs e, int rowIndex)
		{
			this.OnKeyUp(e, rowIndex);
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnLeave(int rowIndex, bool throughMouseClick)
		{
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0008D034 File Offset: 0x0008B234
		internal void OnLeaveInternal(int rowIndex, bool throughMouseClick)
		{
			this.OnLeave(rowIndex, throughMouseClick);
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0008D03E File Offset: 0x0008B23E
		internal void OnMouseClickInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseClick(e);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0008D047 File Offset: 0x0008B247
		internal void OnMouseDoubleClickInternal(DataGridViewCellMouseEventArgs e)
		{
			this.OnMouseDoubleClick(e);
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0008D050 File Offset: 0x0008B250
		internal void OnMouseDownInternal(DataGridViewCellMouseEventArgs e)
		{
			base.DataGridView.CellMouseDownInContentBounds = this.GetContentBounds(e.RowIndex).Contains(e.X, e.Y);
			if (((this.ColumnIndex < 0 || e.RowIndex < 0) && base.DataGridView.ApplyVisualStylesToHeaderCells) || (this.ColumnIndex >= 0 && e.RowIndex >= 0 && base.DataGridView.ApplyVisualStylesToInnerCells))
			{
				base.DataGridView.InvalidateCell(this.ColumnIndex, e.RowIndex);
			}
			this.OnMouseDown(e);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseEnter(int rowIndex)
		{
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0008D0E4 File Offset: 0x0008B2E4
		internal void OnMouseEnterInternal(int rowIndex)
		{
			this.OnMouseEnter(rowIndex);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseLeave(int rowIndex)
		{
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0008D0F0 File Offset: 0x0008B2F0
		internal void OnMouseLeaveInternal(int rowIndex)
		{
			switch (this.CurrentMouseLocation)
			{
			case 1:
				this.OnCellDataAreaMouseLeaveInternal();
				break;
			case 2:
				this.OnCellErrorAreaMouseLeaveInternal();
				break;
			}
			this.CurrentMouseLocation = 0;
			this.OnMouseLeave(rowIndex);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseMove(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0008D134 File Offset: 0x0008B334
		internal void OnMouseMoveInternal(DataGridViewCellMouseEventArgs e)
		{
			byte currentMouseLocation = this.CurrentMouseLocation;
			this.UpdateCurrentMouseLocation(e);
			switch (currentMouseLocation)
			{
			case 0:
				if (this.CurrentMouseLocation == 1)
				{
					this.OnCellDataAreaMouseEnterInternal(e.RowIndex);
				}
				else
				{
					this.OnCellErrorAreaMouseEnterInternal(e.RowIndex);
				}
				break;
			case 1:
				if (this.CurrentMouseLocation == 2)
				{
					this.OnCellDataAreaMouseLeaveInternal();
					this.OnCellErrorAreaMouseEnterInternal(e.RowIndex);
				}
				break;
			case 2:
				if (this.CurrentMouseLocation == 1)
				{
					this.OnCellErrorAreaMouseLeaveInternal();
					this.OnCellDataAreaMouseEnterInternal(e.RowIndex);
				}
				break;
			}
			this.OnMouseMove(e);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnMouseUp(DataGridViewCellMouseEventArgs e)
		{
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0008D1C8 File Offset: 0x0008B3C8
		internal void OnMouseUpInternal(DataGridViewCellMouseEventArgs e)
		{
			int x = e.X;
			int y = e.Y;
			if (((this.ColumnIndex < 0 || e.RowIndex < 0) && base.DataGridView.ApplyVisualStylesToHeaderCells) || (this.ColumnIndex >= 0 && e.RowIndex >= 0 && base.DataGridView.ApplyVisualStylesToInnerCells))
			{
				base.DataGridView.InvalidateCell(this.ColumnIndex, e.RowIndex);
			}
			if (e.Button == MouseButtons.Left && this.GetContentBounds(e.RowIndex).Contains(x, y))
			{
				base.DataGridView.OnCommonCellContentClick(e.ColumnIndex, e.RowIndex, e.Clicks > 1);
			}
			if (base.DataGridView != null && e.ColumnIndex < base.DataGridView.Columns.Count && e.RowIndex < base.DataGridView.Rows.Count)
			{
				this.OnMouseUp(e);
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0008D2BC File Offset: 0x0008B4BC
		protected override void OnDataGridViewChanged()
		{
			if (this.HasStyle)
			{
				if (base.DataGridView == null)
				{
					this.Style.RemoveScope(DataGridViewCellStyleScopes.Cell);
				}
				else
				{
					this.Style.AddScope(base.DataGridView, DataGridViewCellStyleScopes.Cell);
				}
			}
			base.OnDataGridViewChanged();
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x0008D2F4 File Offset: 0x0008B4F4
		internal void PaintInternal(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x0008D31A File Offset: 0x0008B51A
		internal static bool PaintBackground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.Background) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0008D322 File Offset: 0x0008B522
		internal static bool PaintBorder(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.Border) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0008D32C File Offset: 0x0008B52C
		protected virtual void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (base.DataGridView == null)
			{
				return;
			}
			Pen pen = null;
			Pen pen2 = null;
			Pen cachedPen = base.DataGridView.GetCachedPen(cellStyle.BackColor);
			Pen gridPen = base.DataGridView.GridPen;
			this.GetContrastedPens(cellStyle.BackColor, ref pen, ref pen2);
			int num = (this.owningColumn == null) ? 0 : this.owningColumn.DividerWidth;
			if (num != 0)
			{
				if (num > bounds.Width)
				{
					num = bounds.Width;
				}
				DataGridViewAdvancedCellBorderStyle right = advancedBorderStyle.Right;
				Color color;
				if (right != DataGridViewAdvancedCellBorderStyle.Single)
				{
					if (right != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						color = SystemColors.ControlDark;
					}
					else
					{
						color = SystemColors.ControlLightLight;
					}
				}
				else
				{
					color = base.DataGridView.GridPen.Color;
				}
				graphics.FillRectangle(base.DataGridView.GetCachedBrush(color), base.DataGridView.RightToLeftInternal ? bounds.X : (bounds.Right - num), bounds.Y, num, bounds.Height);
				if (base.DataGridView.RightToLeftInternal)
				{
					bounds.X += num;
				}
				bounds.Width -= num;
				if (bounds.Width <= 0)
				{
					return;
				}
			}
			num = ((this.owningRow == null) ? 0 : this.owningRow.DividerHeight);
			if (num != 0)
			{
				if (num > bounds.Height)
				{
					num = bounds.Height;
				}
				DataGridViewAdvancedCellBorderStyle bottom = advancedBorderStyle.Bottom;
				Color color2;
				if (bottom != DataGridViewAdvancedCellBorderStyle.Single)
				{
					if (bottom != DataGridViewAdvancedCellBorderStyle.Inset)
					{
						color2 = SystemColors.ControlDark;
					}
					else
					{
						color2 = SystemColors.ControlLightLight;
					}
				}
				else
				{
					color2 = base.DataGridView.GridPen.Color;
				}
				graphics.FillRectangle(base.DataGridView.GetCachedBrush(color2), bounds.X, bounds.Bottom - num, bounds.Width, num);
				bounds.Height -= num;
				if (bounds.Height <= 0)
				{
					return;
				}
			}
			if (advancedBorderStyle.All == DataGridViewAdvancedCellBorderStyle.None)
			{
				return;
			}
			switch (advancedBorderStyle.Left)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
					num3++;
				}
				graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				graphics.DrawLine(pen, bounds.X + 1, num2, bounds.X + 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.Outset:
				graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
					num3++;
				}
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				graphics.DrawLine(pen2, bounds.X + 1, num2, bounds.X + 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num2 = bounds.Y + 2;
				int num3 = bounds.Bottom - 3;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num2++;
				}
				else if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				graphics.DrawLine(cachedPen, bounds.X, bounds.Y, bounds.X, bounds.Bottom - 1);
				graphics.DrawLine(pen2, bounds.X, num2, bounds.X, num3);
				break;
			}
			}
			switch (advancedBorderStyle.Right)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
				graphics.DrawLine(pen2, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.Inset)
				{
					num3++;
				}
				graphics.DrawLine(pen2, bounds.Right - 2, bounds.Y, bounds.Right - 2, bounds.Bottom - 1);
				graphics.DrawLine(pen, bounds.Right - 1, num2, bounds.Right - 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.Outset:
				graphics.DrawLine(pen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				break;
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			{
				int num2 = bounds.Y + 1;
				int num3 = bounds.Bottom - 1;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				if (advancedBorderStyle.Bottom == DataGridViewAdvancedCellBorderStyle.OutsetPartial)
				{
					num3++;
				}
				graphics.DrawLine(pen, bounds.Right - 2, bounds.Y, bounds.Right - 2, bounds.Bottom - 1);
				graphics.DrawLine(pen2, bounds.Right - 1, num2, bounds.Right - 1, num3);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num2 = bounds.Y + 2;
				int num3 = bounds.Bottom - 3;
				if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num2++;
				}
				else if (advancedBorderStyle.Top == DataGridViewAdvancedCellBorderStyle.None)
				{
					num2--;
				}
				graphics.DrawLine(cachedPen, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom - 1);
				graphics.DrawLine(pen, bounds.Right - 1, num2, bounds.Right - 1, num3);
				break;
			}
			}
			switch (advancedBorderStyle.Top)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
				break;
			case DataGridViewAdvancedCellBorderStyle.Inset:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num4++;
				}
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Inset || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Outset)
				{
					num5--;
				}
				graphics.DrawLine(pen, num4, bounds.Y, num5, bounds.Y);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			{
				int num4 = bounds.X;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.OutsetPartial && advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
				}
				int num5 = bounds.Right - 2;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.None)
				{
					num5++;
				}
				graphics.DrawLine(pen2, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
				graphics.DrawLine(pen, num4, bounds.Y + 1, num5, bounds.Y + 1);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.Outset:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num4++;
				}
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Inset || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.Outset)
				{
					num5--;
				}
				graphics.DrawLine(pen2, num4, bounds.Y, num5, bounds.Y);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			{
				int num4 = bounds.X;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.OutsetPartial && advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
				}
				int num5 = bounds.Right - 2;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetPartial || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.None)
				{
					num5++;
				}
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.Right - 1, bounds.Y);
				graphics.DrawLine(pen2, num4, bounds.Y + 1, num5, bounds.Y + 1);
				break;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
					if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num4++;
					}
				}
				if (advancedBorderStyle.Right != DataGridViewAdvancedCellBorderStyle.None)
				{
					num5--;
					if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num5--;
					}
				}
				graphics.DrawLine(cachedPen, num4, bounds.Y, num5, bounds.Y);
				graphics.DrawLine(pen2, num4 + 1, bounds.Y, num5 - 1, bounds.Y);
				break;
			}
			}
			switch (advancedBorderStyle.Bottom)
			{
			case DataGridViewAdvancedCellBorderStyle.Single:
				graphics.DrawLine(gridPen, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
				return;
			case DataGridViewAdvancedCellBorderStyle.Inset:
			{
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
				{
					num5--;
				}
				graphics.DrawLine(pen2, bounds.X, bounds.Bottom - 1, num5, bounds.Bottom - 1);
				return;
			}
			case DataGridViewAdvancedCellBorderStyle.InsetDouble:
			case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
				break;
			case DataGridViewAdvancedCellBorderStyle.Outset:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble)
				{
					num5--;
				}
				graphics.DrawLine(pen, num4, bounds.Bottom - 1, num5, bounds.Bottom - 1);
				return;
			}
			case DataGridViewAdvancedCellBorderStyle.OutsetPartial:
			{
				int num4 = bounds.X;
				int num5 = bounds.Right - 1;
				if (advancedBorderStyle.Left != DataGridViewAdvancedCellBorderStyle.None)
				{
					num4++;
					if (advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Left == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num4++;
					}
				}
				if (advancedBorderStyle.Right != DataGridViewAdvancedCellBorderStyle.None)
				{
					num5--;
					if (advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.OutsetDouble || advancedBorderStyle.Right == DataGridViewAdvancedCellBorderStyle.InsetDouble)
					{
						num5--;
					}
				}
				graphics.DrawLine(cachedPen, num4, bounds.Bottom - 1, num5, bounds.Bottom - 1);
				graphics.DrawLine(pen, num4 + 1, bounds.Bottom - 1, num5 - 1, bounds.Bottom - 1);
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0008DE12 File Offset: 0x0008C012
		internal static bool PaintContentBackground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.ContentBackground) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0008DE1A File Offset: 0x0008C01A
		internal static bool PaintContentForeground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.ContentForeground) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0008DE22 File Offset: 0x0008C022
		protected virtual void PaintErrorIcon(Graphics graphics, Rectangle clipBounds, Rectangle cellValueBounds, string errorText)
		{
			if (!string.IsNullOrEmpty(errorText) && cellValueBounds.Width >= (int)(8 + DataGridViewCell.iconsWidth) && cellValueBounds.Height >= (int)(8 + DataGridViewCell.iconsHeight))
			{
				DataGridViewCell.PaintErrorIcon(graphics, this.ComputeErrorIconBounds(cellValueBounds));
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0008DE5C File Offset: 0x0008C05C
		private static void PaintErrorIcon(Graphics graphics, Rectangle iconBounds)
		{
			Bitmap errorBitmap = DataGridViewCell.ErrorBitmap;
			if (errorBitmap != null)
			{
				Bitmap obj = errorBitmap;
				lock (obj)
				{
					graphics.DrawImage(errorBitmap, iconBounds, 0, 0, (int)DataGridViewCell.iconsWidth, (int)DataGridViewCell.iconsHeight, GraphicsUnit.Pixel);
				}
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0008DEB0 File Offset: 0x0008C0B0
		internal void PaintErrorIcon(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Rectangle cellBounds, Rectangle cellValueBounds, string errorText)
		{
			if (!string.IsNullOrEmpty(errorText) && cellValueBounds.Width >= (int)(8 + DataGridViewCell.iconsWidth) && cellValueBounds.Height >= (int)(8 + DataGridViewCell.iconsHeight))
			{
				Rectangle errorIconBounds = this.GetErrorIconBounds(graphics, cellStyle, rowIndex);
				if (errorIconBounds.Width >= 4 && errorIconBounds.Height >= (int)DataGridViewCell.iconsHeight)
				{
					errorIconBounds.X += cellBounds.X;
					errorIconBounds.Y += cellBounds.Y;
					DataGridViewCell.PaintErrorIcon(graphics, errorIconBounds);
				}
			}
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0008DF39 File Offset: 0x0008C139
		internal static bool PaintErrorIcon(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.ErrorIcon) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0008DF42 File Offset: 0x0008C142
		internal static bool PaintFocus(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.Focus) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x0008DF4C File Offset: 0x0008C14C
		internal static void PaintPadding(Graphics graphics, Rectangle bounds, DataGridViewCellStyle cellStyle, Brush br, bool rightToLeft)
		{
			Rectangle rect;
			if (rightToLeft)
			{
				rect = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Right, bounds.Height);
				graphics.FillRectangle(br, rect);
				rect.X = bounds.Right - cellStyle.Padding.Left;
				rect.Width = cellStyle.Padding.Left;
				graphics.FillRectangle(br, rect);
				rect.X = bounds.Left + cellStyle.Padding.Right;
			}
			else
			{
				rect = new Rectangle(bounds.X, bounds.Y, cellStyle.Padding.Left, bounds.Height);
				graphics.FillRectangle(br, rect);
				rect.X = bounds.Right - cellStyle.Padding.Right;
				rect.Width = cellStyle.Padding.Right;
				graphics.FillRectangle(br, rect);
				rect.X = bounds.Left + cellStyle.Padding.Left;
			}
			rect.Y = bounds.Y;
			rect.Width = bounds.Width - cellStyle.Padding.Horizontal;
			rect.Height = cellStyle.Padding.Top;
			graphics.FillRectangle(br, rect);
			rect.Y = bounds.Bottom - cellStyle.Padding.Bottom;
			rect.Height = cellStyle.Padding.Bottom;
			graphics.FillRectangle(br, rect);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0008E0F9 File Offset: 0x0008C2F9
		internal static bool PaintSelectionBackground(DataGridViewPaintParts paintParts)
		{
			return (paintParts & DataGridViewPaintParts.SelectionBackground) > DataGridViewPaintParts.None;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0008E104 File Offset: 0x0008C304
		internal void PaintWork(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			DataGridView dataGridView = base.DataGridView;
			int columnIndex = this.ColumnIndex;
			object value = this.GetValue(rowIndex);
			string errorText = this.GetErrorText(rowIndex);
			object formattedValue;
			if (columnIndex > -1 && rowIndex > -1)
			{
				formattedValue = this.GetEditedFormattedValue(value, rowIndex, ref cellStyle, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.Display);
			}
			else
			{
				formattedValue = value;
			}
			DataGridViewCellPaintingEventArgs cellPaintingEventArgs = dataGridView.CellPaintingEventArgs;
			cellPaintingEventArgs.SetProperties(graphics, clipBounds, cellBounds, rowIndex, columnIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
			dataGridView.OnCellPainting(cellPaintingEventArgs);
			if (cellPaintingEventArgs.Handled)
			{
				return;
			}
			this.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0008E196 File Offset: 0x0008C396
		public virtual object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			return this.ParseFormattedValueInternal(this.ValueType, formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0008E1AC File Offset: 0x0008C3AC
		internal object ParseFormattedValueInternal(Type valueType, object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if (this.FormattedValueType == null)
			{
				throw new FormatException(SR.GetString("DataGridViewCell_FormattedValueTypeNull"));
			}
			if (valueType == null)
			{
				throw new FormatException(SR.GetString("DataGridViewCell_ValueTypeNull"));
			}
			if (formattedValue == null || !this.FormattedValueType.IsAssignableFrom(formattedValue.GetType()))
			{
				throw new ArgumentException(SR.GetString("DataGridViewCell_FormattedValueHasWrongType"), "formattedValue");
			}
			return Formatter.ParseObject(formattedValue, valueType, this.FormattedValueType, (valueTypeConverter == null) ? this.ValueTypeConverter : valueTypeConverter, (formattedValueTypeConverter == null) ? this.FormattedValueTypeConverter : formattedValueTypeConverter, cellStyle.FormatProvider, cellStyle.NullValue, cellStyle.IsDataSourceNullValueDefault ? Formatter.GetDefaultDataSourceNullValue(valueType) : cellStyle.DataSourceNullValue);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0008E274 File Offset: 0x0008C474
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			Rectangle rectangle = this.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			if (setLocation)
			{
				base.DataGridView.EditingControl.Location = new Point(rectangle.X, rectangle.Y);
			}
			if (setSize)
			{
				base.DataGridView.EditingControl.Size = new Size(rectangle.Width, rectangle.Height);
			}
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x0008E2E4 File Offset: 0x0008C4E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException();
			}
			DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = new DataGridViewAdvancedBorderStyle();
			DataGridViewAdvancedBorderStyle advancedBorderStyle = this.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			Rectangle rectangle = this.BorderWidths(advancedBorderStyle);
			rectangle.X += cellStyle.Padding.Left;
			rectangle.Y += cellStyle.Padding.Top;
			rectangle.Width += cellStyle.Padding.Right;
			rectangle.Height += cellStyle.Padding.Bottom;
			int num = cellBounds.Width;
			int num2 = cellBounds.Height;
			int x;
			if (cellClip.X - cellBounds.X >= rectangle.X)
			{
				x = cellClip.X;
				num -= cellClip.X - cellBounds.X;
			}
			else
			{
				x = cellBounds.X + rectangle.X;
				num -= rectangle.X;
			}
			if (cellClip.Right <= cellBounds.Right - rectangle.Width)
			{
				num -= cellBounds.Right - cellClip.Right;
			}
			else
			{
				num -= rectangle.Width;
			}
			int x2 = cellBounds.X - cellClip.X;
			int width = cellBounds.Width - rectangle.X - rectangle.Width;
			int y;
			if (cellClip.Y - cellBounds.Y >= rectangle.Y)
			{
				y = cellClip.Y;
				num2 -= cellClip.Y - cellBounds.Y;
			}
			else
			{
				y = cellBounds.Y + rectangle.Y;
				num2 -= rectangle.Y;
			}
			if (cellClip.Bottom <= cellBounds.Bottom - rectangle.Height)
			{
				num2 -= cellBounds.Bottom - cellClip.Bottom;
			}
			else
			{
				num2 -= rectangle.Height;
			}
			int y2 = cellBounds.Y - cellClip.Y;
			int height = cellBounds.Height - rectangle.Y - rectangle.Height;
			base.DataGridView.EditingPanel.Location = new Point(x, y);
			base.DataGridView.EditingPanel.Size = new Size(num, num2);
			return new Rectangle(x2, y2, width, height);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x0008E55C File Offset: 0x0008C75C
		protected virtual bool SetValue(int rowIndex, object value)
		{
			object obj = null;
			DataGridView dataGridView = base.DataGridView;
			if (dataGridView != null && !dataGridView.InSortOperation)
			{
				obj = this.GetValue(rowIndex);
			}
			if (dataGridView != null && this.OwningColumn != null && this.OwningColumn.IsDataBound)
			{
				DataGridView.DataGridViewDataConnection dataConnection = dataGridView.DataConnection;
				if (dataConnection == null)
				{
					return false;
				}
				if (dataConnection.CurrencyManager.Count <= rowIndex)
				{
					if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellValue))
					{
						this.Properties.SetObject(DataGridViewCell.PropCellValue, value);
					}
				}
				else
				{
					if (!dataConnection.PushValue(this.OwningColumn.BoundColumnIndex, this.ColumnIndex, rowIndex, value))
					{
						return false;
					}
					if (base.DataGridView == null || this.OwningRow == null || this.OwningRow.DataGridView == null)
					{
						return true;
					}
					if (this.OwningRow.Index == base.DataGridView.CurrentCellAddress.Y)
					{
						base.DataGridView.IsCurrentRowDirtyInternal = true;
					}
				}
			}
			else if (dataGridView == null || !dataGridView.VirtualMode || rowIndex == -1 || this.ColumnIndex == -1)
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewCell.PropCellValue))
				{
					this.Properties.SetObject(DataGridViewCell.PropCellValue, value);
				}
			}
			else
			{
				dataGridView.OnCellValuePushed(this.ColumnIndex, rowIndex, value);
			}
			if (dataGridView != null && !dataGridView.InSortOperation && ((obj == null && value != null) || (obj != null && value == null) || (obj != null && !value.Equals(obj))))
			{
				base.RaiseCellValueChanged(new DataGridViewCellEventArgs(this.ColumnIndex, rowIndex));
			}
			return true;
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x0008E6DC File Offset: 0x0008C8DC
		internal bool SetValueInternal(int rowIndex, object value)
		{
			return this.SetValue(rowIndex, value);
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0008E6E8 File Offset: 0x0008C8E8
		internal static bool TextFitsInBounds(Graphics graphics, string text, Font font, Size maxBounds, TextFormatFlags flags)
		{
			bool flag;
			int num = DataGridViewCell.MeasureTextHeight(graphics, text, font, maxBounds.Width, flags, out flag);
			return num <= maxBounds.Height && !flag;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0008E71C File Offset: 0x0008C91C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridViewCell { ColumnIndex=",
				this.ColumnIndex.ToString(CultureInfo.CurrentCulture),
				", RowIndex=",
				this.RowIndex.ToString(CultureInfo.CurrentCulture),
				" }"
			});
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0008E778 File Offset: 0x0008C978
		private static string TruncateToolTipText(string toolTipText)
		{
			if (toolTipText.Length > 288)
			{
				StringBuilder stringBuilder = new StringBuilder(toolTipText.Substring(0, 256), 259);
				stringBuilder.Append("...");
				return stringBuilder.ToString();
			}
			return toolTipText;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0008E7C0 File Offset: 0x0008C9C0
		private void UpdateCurrentMouseLocation(DataGridViewCellMouseEventArgs e)
		{
			if (this.GetErrorIconBounds(e.RowIndex).Contains(e.X, e.Y))
			{
				this.CurrentMouseLocation = 2;
				return;
			}
			this.CurrentMouseLocation = 1;
		}

		// Token: 0x04000C94 RID: 3220
		private const TextFormatFlags textFormatSupportedFlags = TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;

		// Token: 0x04000C95 RID: 3221
		private const int DATAGRIDVIEWCELL_constrastThreshold = 1000;

		// Token: 0x04000C96 RID: 3222
		private const int DATAGRIDVIEWCELL_highConstrastThreshold = 2000;

		// Token: 0x04000C97 RID: 3223
		private const int DATAGRIDVIEWCELL_maxToolTipLength = 288;

		// Token: 0x04000C98 RID: 3224
		private const int DATAGRIDVIEWCELL_maxToolTipCutOff = 256;

		// Token: 0x04000C99 RID: 3225
		private const int DATAGRIDVIEWCELL_toolTipEllipsisLength = 3;

		// Token: 0x04000C9A RID: 3226
		private const string DATAGRIDVIEWCELL_toolTipEllipsis = "...";

		// Token: 0x04000C9B RID: 3227
		private const byte DATAGRIDVIEWCELL_flagAreaNotSet = 0;

		// Token: 0x04000C9C RID: 3228
		private const byte DATAGRIDVIEWCELL_flagDataArea = 1;

		// Token: 0x04000C9D RID: 3229
		private const byte DATAGRIDVIEWCELL_flagErrorArea = 2;

		// Token: 0x04000C9E RID: 3230
		internal const byte DATAGRIDVIEWCELL_iconMarginWidth = 4;

		// Token: 0x04000C9F RID: 3231
		internal const byte DATAGRIDVIEWCELL_iconMarginHeight = 4;

		// Token: 0x04000CA0 RID: 3232
		private const byte DATAGRIDVIEWCELL_iconsWidth = 12;

		// Token: 0x04000CA1 RID: 3233
		private const byte DATAGRIDVIEWCELL_iconsHeight = 11;

		// Token: 0x04000CA2 RID: 3234
		private static bool isScalingInitialized = false;

		// Token: 0x04000CA3 RID: 3235
		internal static byte iconsWidth = 12;

		// Token: 0x04000CA4 RID: 3236
		internal static byte iconsHeight = 11;

		// Token: 0x04000CA5 RID: 3237
		internal static readonly int PropCellValue = PropertyStore.CreateKey();

		// Token: 0x04000CA6 RID: 3238
		private static readonly int PropCellContextMenuStrip = PropertyStore.CreateKey();

		// Token: 0x04000CA7 RID: 3239
		private static readonly int PropCellErrorText = PropertyStore.CreateKey();

		// Token: 0x04000CA8 RID: 3240
		private static readonly int PropCellStyle = PropertyStore.CreateKey();

		// Token: 0x04000CA9 RID: 3241
		private static readonly int PropCellValueType = PropertyStore.CreateKey();

		// Token: 0x04000CAA RID: 3242
		private static readonly int PropCellTag = PropertyStore.CreateKey();

		// Token: 0x04000CAB RID: 3243
		private static readonly int PropCellToolTipText = PropertyStore.CreateKey();

		// Token: 0x04000CAC RID: 3244
		private static readonly int PropCellAccessibilityObject = PropertyStore.CreateKey();

		// Token: 0x04000CAD RID: 3245
		private static Bitmap errorBmp = null;

		// Token: 0x04000CAE RID: 3246
		private PropertyStore propertyStore;

		// Token: 0x04000CAF RID: 3247
		private DataGridViewRow owningRow;

		// Token: 0x04000CB0 RID: 3248
		private DataGridViewColumn owningColumn;

		// Token: 0x04000CB1 RID: 3249
		private static Type stringType = typeof(string);

		// Token: 0x04000CB2 RID: 3250
		private byte flags;

		// Token: 0x02000667 RID: 1639
		protected class DataGridViewCellAccessibleObject : AccessibleObject
		{
			// Token: 0x06006603 RID: 26115 RVA: 0x00177BCE File Offset: 0x00175DCE
			public DataGridViewCellAccessibleObject()
			{
			}

			// Token: 0x06006604 RID: 26116 RVA: 0x0017C966 File Offset: 0x0017AB66
			public DataGridViewCellAccessibleObject(DataGridViewCell owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006605 RID: 26117 RVA: 0x0017C975 File Offset: 0x0017AB75
			internal void ClearOwnerCell()
			{
				this.owner = null;
			}

			// Token: 0x06006606 RID: 26118 RVA: 0x0017C97E File Offset: 0x0017AB7E
			internal bool IsOwnerCellDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this.owner == null;
			}

			// Token: 0x1700161B RID: 5659
			// (get) Token: 0x06006607 RID: 26119 RVA: 0x0017C992 File Offset: 0x0017AB92
			public override Rectangle Bounds
			{
				get
				{
					return this.GetAccessibleObjectBounds(this.GetAccessibleObjectParent());
				}
			}

			// Token: 0x1700161C RID: 5660
			// (get) Token: 0x06006608 RID: 26120 RVA: 0x0017C9A0 File Offset: 0x0017ABA0
			public override string DefaultAction
			{
				get
				{
					if (this.Owner == null)
					{
						if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
						{
							return string.Empty;
						}
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					else
					{
						if (!this.Owner.ReadOnly)
						{
							return SR.GetString("DataGridView_AccCellDefaultAction");
						}
						return string.Empty;
					}
				}
			}

			// Token: 0x1700161D RID: 5661
			// (get) Token: 0x06006609 RID: 26121 RVA: 0x0017C9F0 File Offset: 0x0017ABF0
			public override string Help
			{
				get
				{
					if (AccessibilityImprovements.Level2 || this.IsOwnerCellDestroyed())
					{
						return null;
					}
					return this.owner.GetType().Name + "(" + this.owner.GetType().BaseType.Name + ")";
				}
			}

			// Token: 0x1700161E RID: 5662
			// (get) Token: 0x0600660A RID: 26122 RVA: 0x0017CA44 File Offset: 0x0017AC44
			public override string Name
			{
				get
				{
					if (this.owner == null)
					{
						if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
						{
							return string.Empty;
						}
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					else
					{
						if (this.owner.OwningColumn == null || (AccessibilityImprovements.Level5 && this.owner.OwningRow == null))
						{
							return string.Empty;
						}
						int num = AccessibilityImprovements.Level5 ? ((this.owner.DataGridView == null) ? -1 : this.owner.DataGridView.Rows.GetVisibleIndex(this.owner.OwningRow)) : this.owner.OwningRow.Index;
						string text = SR.GetString("DataGridView_AccDataGridViewCellName", new object[]
						{
							this.owner.OwningColumn.HeaderText,
							num
						});
						if (AccessibilityImprovements.Level3 && this.owner.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
						{
							DataGridViewCell dataGridViewCell = this.Owner;
							DataGridView dataGridView = dataGridViewCell.DataGridView;
							if (dataGridViewCell.OwningColumn != null && dataGridViewCell.OwningColumn == dataGridView.SortedColumn)
							{
								text = text + ", " + ((dataGridView.SortOrder == SortOrder.Ascending) ? SR.GetString("SortedAscendingAccessibleStatus") : SR.GetString("SortedDescendingAccessibleStatus"));
							}
							else
							{
								text = text + ", " + SR.GetString("NotSortedAccessibleStatus");
							}
						}
						return text;
					}
				}
			}

			// Token: 0x1700161F RID: 5663
			// (get) Token: 0x0600660B RID: 26123 RVA: 0x0017CB95 File Offset: 0x0017AD95
			// (set) Token: 0x0600660C RID: 26124 RVA: 0x0017CB9D File Offset: 0x0017AD9D
			public DataGridViewCell Owner
			{
				get
				{
					return this.owner;
				}
				set
				{
					if (this.owner != null)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerAlreadySet"));
					}
					this.owner = value;
				}
			}

			// Token: 0x17001620 RID: 5664
			// (get) Token: 0x0600660D RID: 26125 RVA: 0x0017CBBE File Offset: 0x0017ADBE
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.ParentPrivate;
				}
			}

			// Token: 0x17001621 RID: 5665
			// (get) Token: 0x0600660E RID: 26126 RVA: 0x0017CBC8 File Offset: 0x0017ADC8
			private AccessibleObject ParentPrivate
			{
				get
				{
					if (this.owner == null)
					{
						if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
						{
							return null;
						}
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					else
					{
						if (this.owner.OwningRow == null)
						{
							return null;
						}
						return this.owner.OwningRow.AccessibilityObject;
					}
				}
			}

			// Token: 0x17001622 RID: 5666
			// (get) Token: 0x0600660F RID: 26127 RVA: 0x00178958 File Offset: 0x00176B58
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Cell;
				}
			}

			// Token: 0x17001623 RID: 5667
			// (get) Token: 0x06006610 RID: 26128 RVA: 0x0017CC18 File Offset: 0x0017AE18
			public override AccessibleStates State
			{
				get
				{
					if (this.owner != null)
					{
						AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
						if (this.owner == this.owner.DataGridView.CurrentCell)
						{
							accessibleStates |= AccessibleStates.Focused;
						}
						if (this.owner.Selected)
						{
							accessibleStates |= AccessibleStates.Selected;
						}
						if (AccessibilityImprovements.Level1 && this.owner.ReadOnly)
						{
							accessibleStates |= AccessibleStates.ReadOnly;
						}
						Rectangle cellDisplayRectangle;
						if (this.owner.OwningColumn != null && this.owner.OwningRow != null)
						{
							cellDisplayRectangle = this.owner.DataGridView.GetCellDisplayRectangle(this.owner.OwningColumn.Index, this.owner.OwningRow.Index, false);
						}
						else if (this.owner.OwningRow != null)
						{
							cellDisplayRectangle = this.owner.DataGridView.GetCellDisplayRectangle(-1, this.owner.OwningRow.Index, false);
						}
						else if (this.owner.OwningColumn != null)
						{
							cellDisplayRectangle = this.owner.DataGridView.GetCellDisplayRectangle(this.owner.OwningColumn.Index, -1, false);
						}
						else
						{
							cellDisplayRectangle = this.owner.DataGridView.GetCellDisplayRectangle(-1, -1, false);
						}
						if (!cellDisplayRectangle.IntersectsWith(this.owner.DataGridView.ClientRectangle))
						{
							accessibleStates |= AccessibleStates.Offscreen;
						}
						return accessibleStates;
					}
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return AccessibleStates.None;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
			}

			// Token: 0x17001624 RID: 5668
			// (get) Token: 0x06006611 RID: 26129 RVA: 0x0017CD7C File Offset: 0x0017AF7C
			// (set) Token: 0x06006612 RID: 26130 RVA: 0x0017CE24 File Offset: 0x0017B024
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
						throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
					}
					else
					{
						object formattedValue = this.owner.FormattedValue;
						string text = formattedValue as string;
						if (formattedValue == null || (text != null && string.IsNullOrEmpty(text)))
						{
							return SR.GetString("DataGridView_AccNullValue");
						}
						if (text != null)
						{
							return text;
						}
						if (this.owner.OwningColumn == null)
						{
							return string.Empty;
						}
						TypeConverter formattedValueTypeConverter = this.owner.FormattedValueTypeConverter;
						if (formattedValueTypeConverter != null && formattedValueTypeConverter.CanConvertTo(typeof(string)))
						{
							return formattedValueTypeConverter.ConvertToString(formattedValue);
						}
						return formattedValue.ToString();
					}
				}
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				set
				{
					if (this.IsOwnerCellDestroyed())
					{
						return;
					}
					if (this.owner is DataGridViewHeaderCell)
					{
						return;
					}
					if (this.owner.ReadOnly)
					{
						return;
					}
					if (this.owner.OwningRow == null)
					{
						return;
					}
					if (this.owner.DataGridView.IsCurrentCellInEditMode)
					{
						this.owner.DataGridView.EndEdit();
					}
					DataGridViewCellStyle inheritedStyle = this.owner.InheritedStyle;
					object formattedValue = this.owner.GetFormattedValue(value, this.owner.OwningRow.Index, ref inheritedStyle, null, null, DataGridViewDataErrorContexts.Formatting);
					this.owner.Value = this.owner.ParseFormattedValue(formattedValue, inheritedStyle, null, null);
				}
			}

			// Token: 0x06006613 RID: 26131 RVA: 0x0017CED0 File Offset: 0x0017B0D0
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				else
				{
					DataGridViewCell dataGridViewCell = this.Owner;
					DataGridView dataGridView = dataGridViewCell.DataGridView;
					if (dataGridViewCell is DataGridViewHeaderCell)
					{
						return;
					}
					if (dataGridView != null && dataGridViewCell.RowIndex == -1)
					{
						throw new InvalidOperationException(SR.GetString("DataGridView_InvalidOperationOnSharedCell"));
					}
					this.Select(AccessibleSelection.TakeFocus | AccessibleSelection.TakeSelection);
					if (dataGridViewCell.ReadOnly)
					{
						return;
					}
					if (dataGridViewCell.EditType != null)
					{
						if (dataGridView.InBeginEdit || dataGridView.InEndEdit)
						{
							return;
						}
						if (dataGridView.IsCurrentCellInEditMode)
						{
							dataGridView.EndEdit();
							return;
						}
						if (dataGridView.EditMode != DataGridViewEditMode.EditProgrammatically)
						{
							dataGridView.BeginEdit(true);
						}
					}
					return;
				}
			}

			// Token: 0x06006614 RID: 26132 RVA: 0x0017CF80 File Offset: 0x0017B180
			internal Rectangle GetAccessibleObjectBounds(AccessibleObject parentAccObject)
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return Rectangle.Empty;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				else
				{
					if (this.owner.OwningColumn == null)
					{
						return Rectangle.Empty;
					}
					Rectangle bounds = parentAccObject.Bounds;
					int num = this.owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(this.owner.DataGridView.FirstDisplayedScrollingColumnIndex, DataGridViewElementStates.Visible);
					int num2 = this.owner.DataGridView.Columns.ColumnIndexToActualDisplayIndex(this.owner.ColumnIndex, DataGridViewElementStates.Visible);
					bool rowHeadersVisible = this.owner.DataGridView.RowHeadersVisible;
					Rectangle r;
					if (num2 < num)
					{
						r = parentAccObject.GetChild(num2 + 1 + (rowHeadersVisible ? 1 : 0)).Bounds;
						if (this.Owner.DataGridView.RightToLeft == RightToLeft.No)
						{
							r.X -= this.owner.OwningColumn.Width;
						}
						else
						{
							r.X = r.Right;
						}
						r.Width = this.owner.OwningColumn.Width;
					}
					else if (num2 == num)
					{
						r = this.owner.DataGridView.GetColumnDisplayRectangle(this.owner.ColumnIndex, false);
						int firstDisplayedScrollingColumnHiddenWidth = this.owner.DataGridView.FirstDisplayedScrollingColumnHiddenWidth;
						if (firstDisplayedScrollingColumnHiddenWidth != 0)
						{
							if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
							{
								r.X -= firstDisplayedScrollingColumnHiddenWidth;
							}
							r.Width += firstDisplayedScrollingColumnHiddenWidth;
						}
						r = this.owner.DataGridView.RectangleToScreen(r);
					}
					else
					{
						r = parentAccObject.GetChild(num2 - 1 + (rowHeadersVisible ? 1 : 0)).Bounds;
						if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
						{
							r.X = r.Right;
						}
						else
						{
							r.X -= this.owner.OwningColumn.Width;
						}
						r.Width = this.owner.OwningColumn.Width;
					}
					bounds.X = r.X;
					bounds.Width = r.Width;
					return bounds;
				}
			}

			// Token: 0x06006615 RID: 26133 RVA: 0x0017D1AC File Offset: 0x0017B3AC
			private AccessibleObject GetAccessibleObjectParent()
			{
				if (this.owner is DataGridViewButtonCell || this.owner is DataGridViewCheckBoxCell || this.owner is DataGridViewComboBoxCell || this.owner is DataGridViewImageCell || this.owner is DataGridViewLinkCell || this.owner is DataGridViewTextBoxCell)
				{
					return this.ParentPrivate;
				}
				return this.Parent;
			}

			// Token: 0x06006616 RID: 26134 RVA: 0x0017D214 File Offset: 0x0017B414
			public override AccessibleObject GetChild(int index)
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				else
				{
					if (this.owner.DataGridView.EditingControl != null && this.owner.DataGridView.IsCurrentCellInEditMode && this.owner.DataGridView.CurrentCell == this.owner && index == 0)
					{
						return this.owner.DataGridView.EditingControl.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x06006617 RID: 26135 RVA: 0x0017D298 File Offset: 0x0017B498
			public override int GetChildCount()
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return 0;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				else
				{
					if (this.owner.DataGridView.EditingControl != null && this.owner.DataGridView.IsCurrentCellInEditMode && this.owner.DataGridView.CurrentCell == this.owner)
					{
						return 1;
					}
					return 0;
				}
			}

			// Token: 0x06006618 RID: 26136 RVA: 0x00015ECC File Offset: 0x000140CC
			public override AccessibleObject GetFocused()
			{
				return null;
			}

			// Token: 0x06006619 RID: 26137 RVA: 0x00015ECC File Offset: 0x000140CC
			public override AccessibleObject GetSelected()
			{
				return null;
			}

			// Token: 0x0600661A RID: 26138 RVA: 0x0017D308 File Offset: 0x0017B508
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				else
				{
					if (this.owner.OwningColumn == null || this.owner.OwningRow == null)
					{
						return null;
					}
					switch (navigationDirection)
					{
					case AccessibleNavigation.Up:
						if (this.owner.OwningRow.Index != this.owner.DataGridView.Rows.GetFirstRow(DataGridViewElementStates.Visible))
						{
							int previousRow = this.owner.DataGridView.Rows.GetPreviousRow(this.owner.OwningRow.Index, DataGridViewElementStates.Visible);
							return this.owner.DataGridView.Rows[previousRow].Cells[this.owner.OwningColumn.Index].AccessibilityObject;
						}
						if (this.owner.DataGridView.ColumnHeadersVisible)
						{
							return this.owner.OwningColumn.HeaderCell.AccessibilityObject;
						}
						return null;
					case AccessibleNavigation.Down:
					{
						if (this.owner.OwningRow.Index == this.owner.DataGridView.Rows.GetLastRow(DataGridViewElementStates.Visible))
						{
							return null;
						}
						int nextRow = this.owner.DataGridView.Rows.GetNextRow(this.owner.OwningRow.Index, DataGridViewElementStates.Visible);
						return this.owner.DataGridView.Rows[nextRow].Cells[this.owner.OwningColumn.Index].AccessibilityObject;
					}
					case AccessibleNavigation.Left:
						if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
						{
							return this.NavigateBackward(true);
						}
						return this.NavigateForward(true);
					case AccessibleNavigation.Right:
						if (this.owner.DataGridView.RightToLeft == RightToLeft.No)
						{
							return this.NavigateForward(true);
						}
						return this.NavigateBackward(true);
					case AccessibleNavigation.Next:
						return this.NavigateForward(false);
					case AccessibleNavigation.Previous:
						return this.NavigateBackward(false);
					default:
						return null;
					}
				}
			}

			// Token: 0x0600661B RID: 26139 RVA: 0x0017D508 File Offset: 0x0017B708
			private AccessibleObject NavigateBackward(bool wrapAround)
			{
				if (this.IsOwnerCellDestroyed())
				{
					return null;
				}
				if (this.owner.OwningColumn != this.owner.DataGridView.Columns.GetFirstColumn(DataGridViewElementStates.Visible))
				{
					int index = this.owner.DataGridView.Columns.GetPreviousColumn(this.owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					return this.owner.OwningRow.Cells[index].AccessibilityObject;
				}
				if (wrapAround)
				{
					AccessibleObject accessibleObject = this.Owner.OwningRow.AccessibilityObject.Navigate(AccessibleNavigation.Previous);
					if (accessibleObject != null && accessibleObject.GetChildCount() > 0)
					{
						return accessibleObject.GetChild(accessibleObject.GetChildCount() - 1);
					}
					return null;
				}
				else
				{
					if (this.owner.DataGridView.RowHeadersVisible)
					{
						return this.owner.OwningRow.AccessibilityObject.GetChild(0);
					}
					return null;
				}
			}

			// Token: 0x0600661C RID: 26140 RVA: 0x0017D5EC File Offset: 0x0017B7EC
			private AccessibleObject NavigateForward(bool wrapAround)
			{
				if (this.IsOwnerCellDestroyed())
				{
					return null;
				}
				if (this.owner.OwningColumn != this.owner.DataGridView.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None))
				{
					int index = this.owner.DataGridView.Columns.GetNextColumn(this.owner.OwningColumn, DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
					return this.owner.OwningRow.Cells[index].AccessibilityObject;
				}
				if (!wrapAround)
				{
					return null;
				}
				AccessibleObject accessibleObject = this.Owner.OwningRow.AccessibilityObject.Navigate(AccessibleNavigation.Next);
				if (accessibleObject == null || accessibleObject.GetChildCount() <= 0)
				{
					return null;
				}
				if (this.Owner.DataGridView.RowHeadersVisible)
				{
					return accessibleObject.GetChild(1);
				}
				return accessibleObject.GetChild(0);
			}

			// Token: 0x0600661D RID: 26141 RVA: 0x0017D6B8 File Offset: 0x0017B8B8
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void Select(AccessibleSelection flags)
			{
				if (this.owner != null)
				{
					if ((flags & AccessibleSelection.TakeFocus) == AccessibleSelection.TakeFocus)
					{
						this.owner.DataGridView.FocusInternal();
					}
					if ((flags & AccessibleSelection.TakeSelection) == AccessibleSelection.TakeSelection)
					{
						this.owner.Selected = true;
						this.owner.DataGridView.CurrentCell = this.owner;
					}
					if ((flags & AccessibleSelection.AddSelection) == AccessibleSelection.AddSelection)
					{
						this.owner.Selected = true;
					}
					if ((flags & AccessibleSelection.RemoveSelection) == AccessibleSelection.RemoveSelection && (flags & (AccessibleSelection.TakeSelection | AccessibleSelection.AddSelection)) == AccessibleSelection.None)
					{
						this.owner.Selected = false;
					}
					return;
				}
				if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
				{
					return;
				}
				throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
			}

			// Token: 0x0600661E RID: 26142 RVA: 0x0017D750 File Offset: 0x0017B950
			internal override void SetDetachableChild(AccessibleObject child)
			{
				this._child = child;
			}

			// Token: 0x0600661F RID: 26143 RVA: 0x0017D759 File Offset: 0x0017B959
			internal override void SetFocus()
			{
				if (this.IsOwnerCellDestroyed())
				{
					return;
				}
				base.SetFocus();
				base.RaiseAutomationEvent(20005);
			}

			// Token: 0x17001625 RID: 5669
			// (get) Token: 0x06006620 RID: 26144 RVA: 0x0017D776 File Offset: 0x0017B976
			internal override int[] RuntimeId
			{
				get
				{
					if (this.runtimeId == null)
					{
						this.runtimeId = new int[2];
						this.runtimeId[0] = 42;
						this.runtimeId[1] = this.GetHashCode();
					}
					return this.runtimeId;
				}
			}

			// Token: 0x17001626 RID: 5670
			// (get) Token: 0x06006621 RID: 26145 RVA: 0x0017D7AC File Offset: 0x0017B9AC
			private string AutomationId
			{
				get
				{
					string text = string.Empty;
					foreach (int num in this.RuntimeId)
					{
						text += num.ToString();
					}
					return text;
				}
			}

			// Token: 0x06006622 RID: 26146 RVA: 0x0017D7E7 File Offset: 0x0017B9E7
			internal override bool IsIAccessibleExSupported()
			{
				return !this.IsOwnerCellDestroyed() && (AccessibilityImprovements.Level2 || base.IsIAccessibleExSupported());
			}

			// Token: 0x17001627 RID: 5671
			// (get) Token: 0x06006623 RID: 26147 RVA: 0x00016275 File Offset: 0x00014475
			internal override Rectangle BoundingRectangle
			{
				get
				{
					return this.Bounds;
				}
			}

			// Token: 0x17001628 RID: 5672
			// (get) Token: 0x06006624 RID: 26148 RVA: 0x0017D802 File Offset: 0x0017BA02
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (this.IsOwnerCellDestroyed())
					{
						return null;
					}
					return this.owner.DataGridView.AccessibilityObject;
				}
			}

			// Token: 0x06006625 RID: 26149 RVA: 0x0017D820 File Offset: 0x0017BA20
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.owner == null)
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5)
					{
						return null;
					}
					throw new InvalidOperationException(SR.GetString("DataGridViewCellAccessibleObject_OwnerNotSet"));
				}
				else
				{
					if (this.owner.OwningColumn == null || this.owner.OwningRow == null)
					{
						return null;
					}
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.Parent:
						return this.owner.OwningRow.AccessibilityObject;
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
						return this.NavigateForward(false);
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
						return this.NavigateBackward(false);
					case UnsafeNativeMethods.NavigateDirection.FirstChild:
					case UnsafeNativeMethods.NavigateDirection.LastChild:
						if (this.owner.DataGridView.CurrentCell == this.owner && this.owner.DataGridView.IsCurrentCellInEditMode && this.owner.DataGridView.EditingControl != null)
						{
							return this._child;
						}
						return null;
					default:
						return null;
					}
				}
			}

			// Token: 0x06006626 RID: 26150 RVA: 0x0017D8F0 File Offset: 0x0017BAF0
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level5 && propertyID == 30003)
				{
					return 50029;
				}
				if (AccessibilityImprovements.Level3)
				{
					switch (propertyID)
					{
					case 30005:
						return this.Name;
					case 30006:
					case 30012:
					case 30014:
					case 30015:
					case 30016:
					case 30017:
					case 30018:
						break;
					case 30007:
						return string.Empty;
					case 30008:
						return (this.State & AccessibleStates.Focused) == AccessibleStates.Focused;
					case 30009:
						return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
					case 30010:
						return !this.IsOwnerCellDestroyed() && this.owner.DataGridView.Enabled;
					case 30011:
						return this.AutomationId;
					case 30013:
						return this.Help ?? string.Empty;
					case 30019:
						return false;
					default:
						if (propertyID == 30022)
						{
							return (this.State & AccessibleStates.Offscreen) == AccessibleStates.Offscreen;
						}
						if (propertyID == 30068)
						{
							if (!this.IsOwnerCellDestroyed())
							{
								return this.Owner.DataGridView.AccessibilityObject;
							}
							return null;
						}
						break;
					}
				}
				if (propertyID == 30039)
				{
					return this.IsPatternSupported(10013);
				}
				if (propertyID == 30029)
				{
					return this.IsPatternSupported(10007);
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006627 RID: 26151 RVA: 0x0017DA68 File Offset: 0x0017BC68
			internal override bool IsPatternSupported(int patternId)
			{
				return !this.IsOwnerCellDestroyed() && ((AccessibilityImprovements.Level3 && (patternId.Equals(10018) || patternId.Equals(10000) || patternId.Equals(10002))) || ((patternId == 10013 || patternId == 10007) && this.owner.ColumnIndex != -1 && this.owner.RowIndex != -1) || base.IsPatternSupported(patternId));
			}

			// Token: 0x06006628 RID: 26152 RVA: 0x0017DAE8 File Offset: 0x0017BCE8
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
			{
				if (this.IsOwnerCellDestroyed())
				{
					return null;
				}
				if (this.owner.DataGridView.RowHeadersVisible && this.owner.OwningRow.HasHeaderCell)
				{
					return new UnsafeNativeMethods.IRawElementProviderSimple[]
					{
						this.owner.OwningRow.HeaderCell.AccessibilityObject
					};
				}
				return null;
			}

			// Token: 0x06006629 RID: 26153 RVA: 0x0017DB44 File Offset: 0x0017BD44
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
			{
				if (this.IsOwnerCellDestroyed())
				{
					return null;
				}
				if (this.owner.DataGridView.ColumnHeadersVisible && this.owner.OwningColumn.HasHeaderCell)
				{
					return new UnsafeNativeMethods.IRawElementProviderSimple[]
					{
						this.owner.OwningColumn.HeaderCell.AccessibilityObject
					};
				}
				return null;
			}

			// Token: 0x17001629 RID: 5673
			// (get) Token: 0x0600662A RID: 26154 RVA: 0x0017DBA0 File Offset: 0x0017BDA0
			internal override int Row
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.IsOwnerCellDestroyed())
					{
						return -1;
					}
					if (!AccessibilityImprovements.Level5)
					{
						if (this.owner.OwningRow == null)
						{
							return -1;
						}
						return this.owner.OwningRow.Index;
					}
					else
					{
						DataGridViewCell dataGridViewCell = this.owner;
						bool? flag;
						if (dataGridViewCell == null)
						{
							flag = null;
						}
						else
						{
							DataGridViewRow owningRow = dataGridViewCell.OwningRow;
							flag = ((owningRow != null) ? new bool?(owningRow.Visible) : null);
						}
						if (!(flag ?? false) || this.owner.DataGridView == null)
						{
							return -1;
						}
						return this.owner.DataGridView.Rows.GetVisibleIndex(this.owner.OwningRow);
					}
				}
			}

			// Token: 0x1700162A RID: 5674
			// (get) Token: 0x0600662B RID: 26155 RVA: 0x0017DC54 File Offset: 0x0017BE54
			internal override int Column
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.IsOwnerCellDestroyed())
					{
						return -1;
					}
					if (!AccessibilityImprovements.Level5)
					{
						if (this.owner.OwningColumn == null)
						{
							return -1;
						}
						return this.owner.OwningColumn.Index;
					}
					else
					{
						DataGridViewCell dataGridViewCell = this.owner;
						bool? flag;
						if (dataGridViewCell == null)
						{
							flag = null;
						}
						else
						{
							DataGridViewColumn owningColumn = dataGridViewCell.OwningColumn;
							flag = ((owningColumn != null) ? new bool?(owningColumn.Visible) : null);
						}
						if (!(flag ?? false) || this.owner.DataGridView == null)
						{
							return -1;
						}
						return this.owner.DataGridView.Columns.GetVisibleIndex(this.owner.OwningColumn);
					}
				}
			}

			// Token: 0x1700162B RID: 5675
			// (get) Token: 0x0600662C RID: 26156 RVA: 0x0017D802 File Offset: 0x0017BA02
			internal override UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (this.IsOwnerCellDestroyed())
					{
						return null;
					}
					return this.owner.DataGridView.AccessibilityObject;
				}
			}

			// Token: 0x1700162C RID: 5676
			// (get) Token: 0x0600662D RID: 26157 RVA: 0x0017DD07 File Offset: 0x0017BF07
			internal override bool IsReadOnly
			{
				get
				{
					return this.IsOwnerCellDestroyed() || this.owner.ReadOnly;
				}
			}

			// Token: 0x04003A60 RID: 14944
			private int[] runtimeId;

			// Token: 0x04003A61 RID: 14945
			private AccessibleObject _child;

			// Token: 0x04003A62 RID: 14946
			private DataGridViewCell owner;
		}
	}
}
