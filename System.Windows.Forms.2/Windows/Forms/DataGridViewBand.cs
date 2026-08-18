using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x0200019F RID: 415
	public class DataGridViewBand : DataGridViewElement, ICloneable, IDisposable
	{
		// Token: 0x06001CC0 RID: 7360 RVA: 0x00086B48 File Offset: 0x00084D48
		internal DataGridViewBand()
		{
			this.propertyStore = new PropertyStore();
			this.bandIndex = -1;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00086B64 File Offset: 0x00084D64
		~DataGridViewBand()
		{
			this.Dispose(false);
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00086B94 File Offset: 0x00084D94
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x00086B9C File Offset: 0x00084D9C
		internal int CachedThickness
		{
			get
			{
				return this.cachedThickness;
			}
			set
			{
				this.cachedThickness = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00086BA5 File Offset: 0x00084DA5
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x00086BC7 File Offset: 0x00084DC7
		[DefaultValue(null)]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				if (this.bandIsRow)
				{
					return ((DataGridViewRow)this).GetContextMenuStrip(this.Index);
				}
				return this.ContextMenuStripInternal;
			}
			set
			{
				this.ContextMenuStripInternal = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00086BD0 File Offset: 0x00084DD0
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x00086BE8 File Offset: 0x00084DE8
		internal ContextMenuStrip ContextMenuStripInternal
		{
			get
			{
				return (ContextMenuStrip)this.Properties.GetObject(DataGridViewBand.PropContextMenuStrip);
			}
			set
			{
				ContextMenuStrip contextMenuStrip = (ContextMenuStrip)this.Properties.GetObject(DataGridViewBand.PropContextMenuStrip);
				if (contextMenuStrip != value)
				{
					EventHandler value2 = new EventHandler(this.DetachContextMenuStrip);
					if (contextMenuStrip != null)
					{
						contextMenuStrip.Disposed -= value2;
					}
					this.Properties.SetObject(DataGridViewBand.PropContextMenuStrip, value);
					if (value != null)
					{
						value.Disposed += value2;
					}
					if (base.DataGridView != null)
					{
						base.DataGridView.OnBandContextMenuStripChanged(this);
					}
				}
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00086C58 File Offset: 0x00084E58
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x00086CB0 File Offset: 0x00084EB0
		[Browsable(false)]
		public virtual DataGridViewCellStyle DefaultCellStyle
		{
			get
			{
				DataGridViewCellStyle dataGridViewCellStyle = (DataGridViewCellStyle)this.Properties.GetObject(DataGridViewBand.PropDefaultCellStyle);
				if (dataGridViewCellStyle == null)
				{
					dataGridViewCellStyle = new DataGridViewCellStyle();
					dataGridViewCellStyle.AddScope(base.DataGridView, this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
					this.Properties.SetObject(DataGridViewBand.PropDefaultCellStyle, dataGridViewCellStyle);
				}
				return dataGridViewCellStyle;
			}
			set
			{
				DataGridViewCellStyle dataGridViewCellStyle = null;
				if (this.HasDefaultCellStyle)
				{
					dataGridViewCellStyle = this.DefaultCellStyle;
					dataGridViewCellStyle.RemoveScope(this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
				}
				if (value != null || this.Properties.ContainsObject(DataGridViewBand.PropDefaultCellStyle))
				{
					if (value != null)
					{
						value.AddScope(base.DataGridView, this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
					}
					this.Properties.SetObject(DataGridViewBand.PropDefaultCellStyle, value);
				}
				if (((dataGridViewCellStyle != null && value == null) || (dataGridViewCellStyle == null && value != null) || (dataGridViewCellStyle != null && value != null && !dataGridViewCellStyle.Equals(this.DefaultCellStyle))) && base.DataGridView != null)
				{
					base.DataGridView.OnBandDefaultCellStyleChanged(this);
				}
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x00086D58 File Offset: 0x00084F58
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x00086DA8 File Offset: 0x00084FA8
		[Browsable(false)]
		public Type DefaultHeaderCellType
		{
			get
			{
				Type type = (Type)this.Properties.GetObject(DataGridViewBand.PropDefaultHeaderCellType);
				if (type == null)
				{
					if (this.bandIsRow)
					{
						type = typeof(DataGridViewRowHeaderCell);
					}
					else
					{
						type = typeof(DataGridViewColumnHeaderCell);
					}
				}
				return type;
			}
			set
			{
				if (!(value != null) && !this.Properties.ContainsObject(DataGridViewBand.PropDefaultHeaderCellType))
				{
					return;
				}
				if (Type.GetType("System.Windows.Forms.DataGridViewHeaderCell").IsAssignableFrom(value))
				{
					this.Properties.SetObject(DataGridViewBand.PropDefaultHeaderCellType, value);
					return;
				}
				throw new ArgumentException(SR.GetString("DataGridView_WrongType", new object[]
				{
					"DefaultHeaderCellType",
					"System.Windows.Forms.DataGridViewHeaderCell"
				}));
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00086E1C File Offset: 0x0008501C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Displayed
		{
			get
			{
				return (this.State & DataGridViewElementStates.Displayed) > DataGridViewElementStates.None;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x00086E36 File Offset: 0x00085036
		internal bool DisplayedInternal
		{
			set
			{
				if (value)
				{
					base.StateInternal = (this.State | DataGridViewElementStates.Displayed);
				}
				else
				{
					base.StateInternal = (this.State & ~DataGridViewElementStates.Displayed);
				}
				if (base.DataGridView != null)
				{
					this.OnStateChanged(DataGridViewElementStates.Displayed);
				}
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00086E6C File Offset: 0x0008506C
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x00086E94 File Offset: 0x00085094
		internal int DividerThickness
		{
			get
			{
				bool flag;
				int integer = this.Properties.GetInteger(DataGridViewBand.PropDividerThickness, out flag);
				if (!flag)
				{
					return 0;
				}
				return integer;
			}
			set
			{
				if (value < 0)
				{
					if (this.bandIsRow)
					{
						throw new ArgumentOutOfRangeException("DividerHeight", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"DividerHeight",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					throw new ArgumentOutOfRangeException("DividerWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"DividerWidth",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				else
				{
					if (value <= 65536)
					{
						if (value != this.DividerThickness)
						{
							this.Properties.SetInteger(DataGridViewBand.PropDividerThickness, value);
							if (base.DataGridView != null)
							{
								base.DataGridView.OnBandDividerThicknessChanged(this);
							}
						}
						return;
					}
					if (this.bandIsRow)
					{
						throw new ArgumentOutOfRangeException("DividerHeight", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"DividerHeight",
							value.ToString(CultureInfo.CurrentCulture),
							65536.ToString(CultureInfo.CurrentCulture)
						}));
					}
					throw new ArgumentOutOfRangeException("DividerWidth", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"DividerWidth",
						value.ToString(CultureInfo.CurrentCulture),
						65536.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00087005 File Offset: 0x00085205
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x00087012 File Offset: 0x00085212
		[DefaultValue(false)]
		public virtual bool Frozen
		{
			get
			{
				return (this.State & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None;
			}
			set
			{
				if ((this.State & DataGridViewElementStates.Frozen) > DataGridViewElementStates.None != value)
				{
					this.OnStateChanging(DataGridViewElementStates.Frozen);
					if (value)
					{
						base.StateInternal = (this.State | DataGridViewElementStates.Frozen);
					}
					else
					{
						base.StateInternal = (this.State & ~DataGridViewElementStates.Frozen);
					}
					this.OnStateChanged(DataGridViewElementStates.Frozen);
				}
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00087052 File Offset: 0x00085252
		[Browsable(false)]
		public bool HasDefaultCellStyle
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewBand.PropDefaultCellStyle) && this.Properties.GetObject(DataGridViewBand.PropDefaultCellStyle) != null;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0008707B File Offset: 0x0008527B
		internal bool HasDefaultHeaderCellType
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewBand.PropDefaultHeaderCellType) && this.Properties.GetObject(DataGridViewBand.PropDefaultHeaderCellType) != null;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x000870A4 File Offset: 0x000852A4
		internal bool HasHeaderCell
		{
			get
			{
				return this.Properties.ContainsObject(DataGridViewBand.PropHeaderCell) && this.Properties.GetObject(DataGridViewBand.PropHeaderCell) != null;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x000870D0 File Offset: 0x000852D0
		// (set) Token: 0x06001CD6 RID: 7382 RVA: 0x00087190 File Offset: 0x00085390
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected DataGridViewHeaderCell HeaderCellCore
		{
			get
			{
				DataGridViewHeaderCell dataGridViewHeaderCell = (DataGridViewHeaderCell)this.Properties.GetObject(DataGridViewBand.PropHeaderCell);
				if (dataGridViewHeaderCell == null)
				{
					Type defaultHeaderCellType = this.DefaultHeaderCellType;
					dataGridViewHeaderCell = (DataGridViewHeaderCell)SecurityUtils.SecureCreateInstance(defaultHeaderCellType);
					dataGridViewHeaderCell.DataGridViewInternal = base.DataGridView;
					if (this.bandIsRow)
					{
						dataGridViewHeaderCell.OwningRowInternal = (DataGridViewRow)this;
						this.Properties.SetObject(DataGridViewBand.PropHeaderCell, dataGridViewHeaderCell);
					}
					else
					{
						DataGridViewColumn dataGridViewColumn = this as DataGridViewColumn;
						dataGridViewHeaderCell.OwningColumnInternal = dataGridViewColumn;
						this.Properties.SetObject(DataGridViewBand.PropHeaderCell, dataGridViewHeaderCell);
						if (base.DataGridView != null && base.DataGridView.SortedColumn == dataGridViewColumn)
						{
							DataGridViewColumnHeaderCell dataGridViewColumnHeaderCell = dataGridViewHeaderCell as DataGridViewColumnHeaderCell;
							dataGridViewColumnHeaderCell.SortGlyphDirection = base.DataGridView.SortOrder;
						}
					}
				}
				return dataGridViewHeaderCell;
			}
			set
			{
				DataGridViewHeaderCell dataGridViewHeaderCell = (DataGridViewHeaderCell)this.Properties.GetObject(DataGridViewBand.PropHeaderCell);
				if (value != null || this.Properties.ContainsObject(DataGridViewBand.PropHeaderCell))
				{
					if (dataGridViewHeaderCell != null)
					{
						dataGridViewHeaderCell.DataGridViewInternal = null;
						if (this.bandIsRow)
						{
							dataGridViewHeaderCell.OwningRowInternal = null;
						}
						else
						{
							dataGridViewHeaderCell.OwningColumnInternal = null;
							((DataGridViewColumnHeaderCell)dataGridViewHeaderCell).SortGlyphDirectionInternal = SortOrder.None;
						}
					}
					if (value != null)
					{
						if (this.bandIsRow)
						{
							if (!(value is DataGridViewRowHeaderCell))
							{
								throw new ArgumentException(SR.GetString("DataGridView_WrongType", new object[]
								{
									"HeaderCell",
									"System.Windows.Forms.DataGridViewRowHeaderCell"
								}));
							}
							if (value.OwningRow != null)
							{
								value.OwningRow.HeaderCell = null;
							}
							value.OwningRowInternal = (DataGridViewRow)this;
						}
						else
						{
							if (!(value is DataGridViewColumnHeaderCell))
							{
								throw new ArgumentException(SR.GetString("DataGridView_WrongType", new object[]
								{
									"HeaderCell",
									"System.Windows.Forms.DataGridViewColumnHeaderCell"
								}));
							}
							if (value.OwningColumn != null)
							{
								value.OwningColumn.HeaderCell = null;
							}
							value.OwningColumnInternal = (DataGridViewColumn)this;
						}
						value.DataGridViewInternal = base.DataGridView;
					}
					this.Properties.SetObject(DataGridViewBand.PropHeaderCell, value);
				}
				if (((value == null && dataGridViewHeaderCell != null) || (value != null && dataGridViewHeaderCell == null) || (value != null && dataGridViewHeaderCell != null && !dataGridViewHeaderCell.Equals(value))) && base.DataGridView != null)
				{
					base.DataGridView.OnBandHeaderCellChanged(this);
				}
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x000872F3 File Offset: 0x000854F3
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this.bandIndex;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (set) Token: 0x06001CD8 RID: 7384 RVA: 0x000872FB File Offset: 0x000854FB
		internal int IndexInternal
		{
			set
			{
				this.bandIndex = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x00015ECC File Offset: 0x000140CC
		[Browsable(false)]
		public virtual DataGridViewCellStyle InheritedStyle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00087304 File Offset: 0x00085504
		protected bool IsRow
		{
			get
			{
				return this.bandIsRow;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x0008730C File Offset: 0x0008550C
		// (set) Token: 0x06001CDC RID: 7388 RVA: 0x00087344 File Offset: 0x00085544
		internal int MinimumThickness
		{
			get
			{
				if (this.bandIsRow && this.bandIndex > -1)
				{
					int num;
					int result;
					this.GetHeightInfo(this.bandIndex, out num, out result);
					return result;
				}
				return this.minimumThickness;
			}
			set
			{
				if (this.minimumThickness != value)
				{
					if (value < 2)
					{
						if (this.bandIsRow)
						{
							throw new ArgumentOutOfRangeException("MinimumHeight", value, SR.GetString("DataGridViewBand_MinimumHeightSmallerThanOne", new object[]
							{
								2.ToString(CultureInfo.CurrentCulture)
							}));
						}
						throw new ArgumentOutOfRangeException("MinimumWidth", value, SR.GetString("DataGridViewBand_MinimumWidthSmallerThanOne", new object[]
						{
							2.ToString(CultureInfo.CurrentCulture)
						}));
					}
					else
					{
						if (this.Thickness < value)
						{
							if (base.DataGridView != null && !this.bandIsRow)
							{
								base.DataGridView.OnColumnMinimumWidthChanging((DataGridViewColumn)this, value);
							}
							this.Thickness = value;
						}
						this.minimumThickness = value;
						if (base.DataGridView != null)
						{
							base.DataGridView.OnBandMinimumThicknessChanged(this);
						}
					}
				}
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x0008741A File Offset: 0x0008561A
		internal PropertyStore Properties
		{
			get
			{
				return this.propertyStore;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x00087422 File Offset: 0x00085622
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x00087448 File Offset: 0x00085648
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return (this.State & DataGridViewElementStates.ReadOnly) != DataGridViewElementStates.None || (base.DataGridView != null && base.DataGridView.ReadOnly);
			}
			set
			{
				if (base.DataGridView == null)
				{
					if ((this.State & DataGridViewElementStates.ReadOnly) > DataGridViewElementStates.None != value)
					{
						if (value)
						{
							if (this.bandIsRow)
							{
								foreach (object obj in ((DataGridViewRow)this).Cells)
								{
									DataGridViewCell dataGridViewCell = (DataGridViewCell)obj;
									if (dataGridViewCell.ReadOnly)
									{
										dataGridViewCell.ReadOnlyInternal = false;
									}
								}
							}
							base.StateInternal = (this.State | DataGridViewElementStates.ReadOnly);
							return;
						}
						base.StateInternal = (this.State & ~DataGridViewElementStates.ReadOnly);
					}
					return;
				}
				if (base.DataGridView.ReadOnly)
				{
					return;
				}
				if (!this.bandIsRow)
				{
					this.OnStateChanging(DataGridViewElementStates.ReadOnly);
					base.DataGridView.SetReadOnlyColumnCore(this.bandIndex, value);
					return;
				}
				if (this.bandIndex == -1)
				{
					throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
					{
						"ReadOnly"
					}));
				}
				this.OnStateChanging(DataGridViewElementStates.ReadOnly);
				base.DataGridView.SetReadOnlyRowCore(this.bandIndex, value);
			}
		}

		// Token: 0x1700063B RID: 1595
		// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x00087560 File Offset: 0x00085760
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
				this.OnStateChanged(DataGridViewElementStates.ReadOnly);
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0008758B File Offset: 0x0008578B
		// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x000875C0 File Offset: 0x000857C0
		[Browsable(true)]
		public virtual DataGridViewTriState Resizable
		{
			get
			{
				if ((this.State & DataGridViewElementStates.ResizableSet) != DataGridViewElementStates.None)
				{
					if ((this.State & DataGridViewElementStates.Resizable) == DataGridViewElementStates.None)
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
					if (!base.DataGridView.AllowUserToResizeColumns)
					{
						return DataGridViewTriState.False;
					}
					return DataGridViewTriState.True;
				}
			}
			set
			{
				DataGridViewTriState resizable = this.Resizable;
				if (value == DataGridViewTriState.NotSet)
				{
					base.StateInternal = (this.State & ~DataGridViewElementStates.ResizableSet);
				}
				else
				{
					base.StateInternal = (this.State | DataGridViewElementStates.ResizableSet);
					if ((this.State & DataGridViewElementStates.Resizable) > DataGridViewElementStates.None != (value == DataGridViewTriState.True))
					{
						if (value == DataGridViewTriState.True)
						{
							base.StateInternal = (this.State | DataGridViewElementStates.Resizable);
						}
						else
						{
							base.StateInternal = (this.State & ~DataGridViewElementStates.Resizable);
						}
					}
				}
				if (resizable != this.Resizable)
				{
					this.OnStateChanged(DataGridViewElementStates.Resizable);
				}
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0008763B File Offset: 0x0008583B
		// (set) Token: 0x06001CE4 RID: 7396 RVA: 0x0008764C File Offset: 0x0008584C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Selected
		{
			get
			{
				return (this.State & DataGridViewElementStates.Selected) > DataGridViewElementStates.None;
			}
			set
			{
				if (base.DataGridView != null)
				{
					if (this.bandIsRow)
					{
						if (this.bandIndex == -1)
						{
							throw new InvalidOperationException(SR.GetString("DataGridView_InvalidPropertySetOnSharedRow", new object[]
							{
								"Selected"
							}));
						}
						if (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullRowSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.RowHeaderSelect)
						{
							base.DataGridView.SetSelectedRowCoreInternal(this.bandIndex, value);
							return;
						}
					}
					else if (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect)
					{
						base.DataGridView.SetSelectedColumnCoreInternal(this.bandIndex, value);
						return;
					}
				}
				else if (value)
				{
					throw new InvalidOperationException(SR.GetString("DataGridViewBand_CannotSelect"));
				}
			}
		}

		// Token: 0x1700063E RID: 1598
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x00087704 File Offset: 0x00085904
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
					this.OnStateChanged(DataGridViewElementStates.Selected);
				}
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x00087739 File Offset: 0x00085939
		// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0008774B File Offset: 0x0008594B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Tag
		{
			get
			{
				return this.Properties.GetObject(DataGridViewBand.PropUserData);
			}
			set
			{
				if (value != null || this.Properties.ContainsObject(DataGridViewBand.PropUserData))
				{
					this.Properties.SetObject(DataGridViewBand.PropUserData, value);
				}
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x00087774 File Offset: 0x00085974
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x000877AC File Offset: 0x000859AC
		internal int Thickness
		{
			get
			{
				if (this.bandIsRow && this.bandIndex > -1)
				{
					int result;
					int num;
					this.GetHeightInfo(this.bandIndex, out result, out num);
					return result;
				}
				return this.thickness;
			}
			set
			{
				int num = this.MinimumThickness;
				if (value < num)
				{
					value = num;
				}
				if (value <= 65536)
				{
					bool flag = true;
					if (this.bandIsRow)
					{
						if (base.DataGridView != null && base.DataGridView.AutoSizeRowsMode != DataGridViewAutoSizeRowsMode.None)
						{
							this.cachedThickness = value;
							flag = false;
						}
					}
					else
					{
						DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this;
						DataGridViewAutoSizeColumnMode inheritedAutoSizeMode = dataGridViewColumn.InheritedAutoSizeMode;
						if (inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.Fill && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.None && inheritedAutoSizeMode != DataGridViewAutoSizeColumnMode.NotSet)
						{
							this.cachedThickness = value;
							flag = false;
						}
						else if (inheritedAutoSizeMode == DataGridViewAutoSizeColumnMode.Fill && base.DataGridView != null && dataGridViewColumn.Visible)
						{
							IntPtr handle = base.DataGridView.Handle;
							base.DataGridView.AdjustFillingColumn(dataGridViewColumn, value);
							flag = false;
						}
					}
					if (flag && this.thickness != value)
					{
						if (base.DataGridView != null)
						{
							base.DataGridView.OnBandThicknessChanging();
						}
						this.ThicknessInternal = value;
					}
					return;
				}
				if (this.bandIsRow)
				{
					throw new ArgumentOutOfRangeException("Height", SR.GetString("InvalidHighBoundArgumentEx", new object[]
					{
						"Height",
						value.ToString(CultureInfo.CurrentCulture),
						65536.ToString(CultureInfo.CurrentCulture)
					}));
				}
				throw new ArgumentOutOfRangeException("Width", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"Width",
					value.ToString(CultureInfo.CurrentCulture),
					65536.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00087913 File Offset: 0x00085B13
		// (set) Token: 0x06001CEB RID: 7403 RVA: 0x0008791B File Offset: 0x00085B1B
		internal int ThicknessInternal
		{
			get
			{
				return this.thickness;
			}
			set
			{
				this.thickness = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnBandThicknessChanged(this);
				}
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x00087938 File Offset: 0x00085B38
		// (set) Token: 0x06001CED RID: 7405 RVA: 0x00087948 File Offset: 0x00085B48
		[DefaultValue(true)]
		public virtual bool Visible
		{
			get
			{
				return (this.State & DataGridViewElementStates.Visible) > DataGridViewElementStates.None;
			}
			set
			{
				if ((this.State & DataGridViewElementStates.Visible) > DataGridViewElementStates.None != value)
				{
					if (base.DataGridView != null && this.bandIsRow && base.DataGridView.NewRowIndex != -1 && base.DataGridView.NewRowIndex == this.bandIndex && !value)
					{
						throw new InvalidOperationException(SR.GetString("DataGridViewBand_NewRowCannotBeInvisible"));
					}
					this.OnStateChanging(DataGridViewElementStates.Visible);
					if (value)
					{
						base.StateInternal = (this.State | DataGridViewElementStates.Visible);
					}
					else
					{
						base.StateInternal = (this.State & ~DataGridViewElementStates.Visible);
					}
					this.OnStateChanged(DataGridViewElementStates.Visible);
				}
			}
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x000879DC File Offset: 0x00085BDC
		public virtual object Clone()
		{
			DataGridViewBand dataGridViewBand = (DataGridViewBand)Activator.CreateInstance(base.GetType());
			if (dataGridViewBand != null)
			{
				this.CloneInternal(dataGridViewBand);
			}
			return dataGridViewBand;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00087A08 File Offset: 0x00085C08
		internal void CloneInternal(DataGridViewBand dataGridViewBand)
		{
			dataGridViewBand.propertyStore = new PropertyStore();
			dataGridViewBand.bandIndex = -1;
			dataGridViewBand.bandIsRow = this.bandIsRow;
			if (!this.bandIsRow || this.bandIndex >= 0 || base.DataGridView == null)
			{
				dataGridViewBand.StateInternal = (this.State & ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected));
			}
			dataGridViewBand.thickness = this.Thickness;
			dataGridViewBand.MinimumThickness = this.MinimumThickness;
			dataGridViewBand.cachedThickness = this.CachedThickness;
			dataGridViewBand.DividerThickness = this.DividerThickness;
			dataGridViewBand.Tag = this.Tag;
			if (this.HasDefaultCellStyle)
			{
				dataGridViewBand.DefaultCellStyle = new DataGridViewCellStyle(this.DefaultCellStyle);
			}
			if (this.HasDefaultHeaderCellType)
			{
				dataGridViewBand.DefaultHeaderCellType = this.DefaultHeaderCellType;
			}
			if (this.ContextMenuStripInternal != null)
			{
				dataGridViewBand.ContextMenuStrip = this.ContextMenuStripInternal.Clone();
			}
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x00087ADD File Offset: 0x00085CDD
		private void DetachContextMenuStrip(object sender, EventArgs e)
		{
			this.ContextMenuStripInternal = null;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00087AE6 File Offset: 0x00085CE6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00087AF8 File Offset: 0x00085CF8
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

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00087B24 File Offset: 0x00085D24
		internal void GetHeightInfo(int rowIndex, out int height, out int minimumHeight)
		{
			if (base.DataGridView != null && (base.DataGridView.VirtualMode || base.DataGridView.DataSource != null) && base.DataGridView.AutoSizeRowsMode == DataGridViewAutoSizeRowsMode.None)
			{
				DataGridViewRowHeightInfoNeededEventArgs dataGridViewRowHeightInfoNeededEventArgs = base.DataGridView.OnRowHeightInfoNeeded(rowIndex, this.thickness, this.minimumThickness);
				height = dataGridViewRowHeightInfoNeededEventArgs.Height;
				minimumHeight = dataGridViewRowHeightInfoNeededEventArgs.MinimumHeight;
				return;
			}
			height = this.thickness;
			minimumHeight = this.minimumThickness;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00087B9C File Offset: 0x00085D9C
		internal void OnStateChanged(DataGridViewElementStates elementState)
		{
			if (base.DataGridView != null)
			{
				if (this.bandIsRow)
				{
					base.DataGridView.Rows.InvalidateCachedRowCount(elementState);
					base.DataGridView.Rows.InvalidateCachedRowsHeight(elementState);
					if (this.bandIndex != -1)
					{
						base.DataGridView.OnDataGridViewElementStateChanged(this, -1, elementState);
						return;
					}
				}
				else
				{
					base.DataGridView.Columns.InvalidateCachedColumnCount(elementState);
					base.DataGridView.Columns.InvalidateCachedColumnsWidth(elementState);
					base.DataGridView.OnDataGridViewElementStateChanged(this, -1, elementState);
				}
			}
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00087C23 File Offset: 0x00085E23
		private void OnStateChanging(DataGridViewElementStates elementState)
		{
			if (base.DataGridView != null)
			{
				if (this.bandIsRow)
				{
					if (this.bandIndex != -1)
					{
						base.DataGridView.OnDataGridViewElementStateChanging(this, -1, elementState);
						return;
					}
				}
				else
				{
					base.DataGridView.OnDataGridViewElementStateChanging(this, -1, elementState);
				}
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x00087C5C File Offset: 0x00085E5C
		protected override void OnDataGridViewChanged()
		{
			if (this.HasDefaultCellStyle)
			{
				if (base.DataGridView == null)
				{
					this.DefaultCellStyle.RemoveScope(this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
				}
				else
				{
					this.DefaultCellStyle.AddScope(base.DataGridView, this.bandIsRow ? DataGridViewCellStyleScopes.Row : DataGridViewCellStyleScopes.Column);
				}
			}
			base.OnDataGridViewChanged();
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00087CB8 File Offset: 0x00085EB8
		private bool ShouldSerializeDefaultHeaderCellType()
		{
			Type left = (Type)this.Properties.GetObject(DataGridViewBand.PropDefaultHeaderCellType);
			return left != null;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00087CE2 File Offset: 0x00085EE2
		internal bool ShouldSerializeResizable()
		{
			return (this.State & DataGridViewElementStates.ResizableSet) > DataGridViewElementStates.None;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00087CF0 File Offset: 0x00085EF0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(36);
			stringBuilder.Append("DataGridViewBand { Index=");
			stringBuilder.Append(this.Index.ToString(CultureInfo.CurrentCulture));
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000C76 RID: 3190
		private static readonly int PropContextMenuStrip = PropertyStore.CreateKey();

		// Token: 0x04000C77 RID: 3191
		private static readonly int PropDefaultCellStyle = PropertyStore.CreateKey();

		// Token: 0x04000C78 RID: 3192
		private static readonly int PropDefaultHeaderCellType = PropertyStore.CreateKey();

		// Token: 0x04000C79 RID: 3193
		private static readonly int PropDividerThickness = PropertyStore.CreateKey();

		// Token: 0x04000C7A RID: 3194
		private static readonly int PropHeaderCell = PropertyStore.CreateKey();

		// Token: 0x04000C7B RID: 3195
		private static readonly int PropUserData = PropertyStore.CreateKey();

		// Token: 0x04000C7C RID: 3196
		internal const int minBandThickness = 2;

		// Token: 0x04000C7D RID: 3197
		internal const int maxBandThickness = 65536;

		// Token: 0x04000C7E RID: 3198
		private PropertyStore propertyStore;

		// Token: 0x04000C7F RID: 3199
		private int thickness;

		// Token: 0x04000C80 RID: 3200
		private int cachedThickness;

		// Token: 0x04000C81 RID: 3201
		private int minimumThickness;

		// Token: 0x04000C82 RID: 3202
		private int bandIndex;

		// Token: 0x04000C83 RID: 3203
		internal bool bandIsRow;
	}
}
