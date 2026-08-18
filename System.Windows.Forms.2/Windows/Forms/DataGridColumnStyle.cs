using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000181 RID: 385
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultProperty("Header")]
	public abstract class DataGridColumnStyle : Component, IDataGridColumnStyleEditingNotificationService
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x000508C8 File Offset: 0x0004EAC8
		public DataGridColumnStyle()
		{
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00050904 File Offset: 0x0004EB04
		public DataGridColumnStyle(PropertyDescriptor prop) : this()
		{
			this.PropertyDescriptor = prop;
			if (prop != null)
			{
				this.readOnly = prop.IsReadOnly;
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00050922 File Offset: 0x0004EB22
		internal DataGridColumnStyle(PropertyDescriptor prop, bool isDefault) : this(prop)
		{
			this.isDefault = isDefault;
			if (isDefault)
			{
				this.headerName = prop.Name;
				this.mappingName = prop.Name;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0005094D File Offset: 0x0004EB4D
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x00050958 File Offset: 0x0004EB58
		[SRCategory("CatDisplay")]
		[Localizable(true)]
		[DefaultValue(HorizontalAlignment.Left)]
		public virtual HorizontalAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DataGridLineStyle));
				}
				if (this.alignment != value)
				{
					this.alignment = value;
					this.OnAlignmentChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x0600165E RID: 5726 RVA: 0x000509AC File Offset: 0x0004EBAC
		// (remove) Token: 0x0600165F RID: 5727 RVA: 0x000509BF File Offset: 0x0004EBBF
		public event EventHandler AlignmentChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventAlignment, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventAlignment, value);
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000072B6 File Offset: 0x000054B6
		protected internal virtual void UpdateUI(CurrencyManager source, int rowNum, string displayText)
		{
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x000509D2 File Offset: 0x0004EBD2
		[Browsable(false)]
		public AccessibleObject HeaderAccessibleObject
		{
			get
			{
				if (this.headerAccessibleObject == null)
				{
					this.headerAccessibleObject = this.CreateHeaderAccessibleObject();
				}
				return this.headerAccessibleObject;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x000509EE File Offset: 0x0004EBEE
		// (set) Token: 0x06001663 RID: 5731 RVA: 0x000509F6 File Offset: 0x0004EBF6
		[DefaultValue(null)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.propertyDescriptor;
			}
			set
			{
				if (this.propertyDescriptor != value)
				{
					this.propertyDescriptor = value;
					this.OnPropertyDescriptorChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06001664 RID: 5732 RVA: 0x00050A13 File Offset: 0x0004EC13
		// (remove) Token: 0x06001665 RID: 5733 RVA: 0x00050A26 File Offset: 0x0004EC26
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler PropertyDescriptorChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventPropertyDescriptor, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventPropertyDescriptor, value);
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00050A39 File Offset: 0x0004EC39
		protected virtual AccessibleObject CreateHeaderAccessibleObject()
		{
			return new DataGridColumnStyle.DataGridColumnHeaderAccessibleObject(this);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00050A41 File Offset: 0x0004EC41
		protected virtual void SetDataGrid(DataGrid value)
		{
			this.SetDataGridInColumn(value);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00050A4C File Offset: 0x0004EC4C
		protected virtual void SetDataGridInColumn(DataGrid value)
		{
			if (this.PropertyDescriptor == null && value != null)
			{
				CurrencyManager listManager = value.ListManager;
				if (listManager == null)
				{
					return;
				}
				PropertyDescriptorCollection itemProperties = listManager.GetItemProperties();
				int count = itemProperties.Count;
				for (int i = 0; i < itemProperties.Count; i++)
				{
					PropertyDescriptor propertyDescriptor = itemProperties[i];
					if (!typeof(IList).IsAssignableFrom(propertyDescriptor.PropertyType) && propertyDescriptor.Name.Equals(this.HeaderText))
					{
						this.PropertyDescriptor = propertyDescriptor;
						return;
					}
				}
			}
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00050ACC File Offset: 0x0004ECCC
		internal void SetDataGridInternalInColumn(DataGrid value)
		{
			if (value == null || value.Initializing)
			{
				return;
			}
			this.SetDataGridInColumn(value);
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00050AE1 File Offset: 0x0004ECE1
		[Browsable(false)]
		public virtual DataGridTableStyle DataGridTableStyle
		{
			get
			{
				return this.dataGridTableStyle;
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00050AEC File Offset: 0x0004ECEC
		internal void SetDataGridTableInColumn(DataGridTableStyle value, bool force)
		{
			if (this.dataGridTableStyle != null && this.dataGridTableStyle.Equals(value) && !force)
			{
				return;
			}
			if (value != null && value.DataGrid != null && !value.DataGrid.Initializing)
			{
				this.SetDataGridInColumn(value.DataGrid);
			}
			this.dataGridTableStyle = value;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x00050B3E File Offset: 0x0004ED3E
		protected int FontHeight
		{
			get
			{
				if (this.fontHeight != -1)
				{
					return this.fontHeight;
				}
				if (this.DataGridTableStyle != null)
				{
					return this.DataGridTableStyle.DataGrid.FontHeight;
				}
				return DataGridTableStyle.defaultFontHeight;
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x00050B6E File Offset: 0x0004ED6E
		private bool ShouldSerializeFont()
		{
			return this.font != null;
		}

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x0600166E RID: 5742 RVA: 0x000072B6 File Offset: 0x000054B6
		// (remove) Token: 0x0600166F RID: 5743 RVA: 0x000072B6 File Offset: 0x000054B6
		public event EventHandler FontChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00050B79 File Offset: 0x0004ED79
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x00050B81 File Offset: 0x0004ED81
		[Localizable(true)]
		[SRCategory("CatDisplay")]
		public virtual string HeaderText
		{
			get
			{
				return this.headerName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.headerName.Equals(value))
				{
					return;
				}
				this.headerName = value;
				this.OnHeaderTextChanged(EventArgs.Empty);
				if (this.PropertyDescriptor != null)
				{
					this.Invalidate();
				}
			}
		}

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06001672 RID: 5746 RVA: 0x00050BBC File Offset: 0x0004EDBC
		// (remove) Token: 0x06001673 RID: 5747 RVA: 0x00050BCF File Offset: 0x0004EDCF
		public event EventHandler HeaderTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventHeaderText, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventHeaderText, value);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x00050BE2 File Offset: 0x0004EDE2
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x00050BEC File Offset: 0x0004EDEC
		[Editor("System.Windows.Forms.Design.DataGridColumnStyleMappingNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
		public string MappingName
		{
			get
			{
				return this.mappingName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.mappingName.Equals(value))
				{
					return;
				}
				string text = this.mappingName;
				this.mappingName = value;
				try
				{
					if (this.dataGridTableStyle != null)
					{
						this.dataGridTableStyle.GridColumnStyles.CheckForMappingNameDuplicates(this);
					}
				}
				catch
				{
					this.mappingName = text;
					throw;
				}
				this.OnMappingNameChanged(EventArgs.Empty);
			}
		}

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06001676 RID: 5750 RVA: 0x00050C60 File Offset: 0x0004EE60
		// (remove) Token: 0x06001677 RID: 5751 RVA: 0x00050C73 File Offset: 0x0004EE73
		public event EventHandler MappingNameChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventMappingName, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventMappingName, value);
			}
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00050C86 File Offset: 0x0004EE86
		private bool ShouldSerializeHeaderText()
		{
			return this.headerName.Length != 0;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00050C96 File Offset: 0x0004EE96
		public void ResetHeaderText()
		{
			this.HeaderText = "";
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00050CA3 File Offset: 0x0004EEA3
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x00050CAB File Offset: 0x0004EEAB
		[Localizable(true)]
		[SRCategory("CatDisplay")]
		public virtual string NullText
		{
			get
			{
				return this.nullText;
			}
			set
			{
				if (this.nullText != null && this.nullText.Equals(value))
				{
					return;
				}
				this.nullText = value;
				this.OnNullTextChanged(EventArgs.Empty);
				this.Invalidate();
			}
		}

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x0600167C RID: 5756 RVA: 0x00050CDC File Offset: 0x0004EEDC
		// (remove) Token: 0x0600167D RID: 5757 RVA: 0x00050CEF File Offset: 0x0004EEEF
		public event EventHandler NullTextChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventNullText, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventNullText, value);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00050D02 File Offset: 0x0004EF02
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x00050D0A File Offset: 0x0004EF0A
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				if (this.readOnly != value)
				{
					this.readOnly = value;
					this.OnReadOnlyChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06001680 RID: 5760 RVA: 0x00050D27 File Offset: 0x0004EF27
		// (remove) Token: 0x06001681 RID: 5761 RVA: 0x00050D3A File Offset: 0x0004EF3A
		public event EventHandler ReadOnlyChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventReadOnly, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventReadOnly, value);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00050D4D File Offset: 0x0004EF4D
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x00050D58 File Offset: 0x0004EF58
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(100)]
		public virtual int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (this.width != value)
				{
					this.width = value;
					DataGrid dataGrid = (this.DataGridTableStyle == null) ? null : this.DataGridTableStyle.DataGrid;
					if (dataGrid != null)
					{
						dataGrid.PerformLayout();
						dataGrid.InvalidateInside();
					}
					this.OnWidthChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06001684 RID: 5764 RVA: 0x00050DA6 File Offset: 0x0004EFA6
		// (remove) Token: 0x06001685 RID: 5765 RVA: 0x00050DB9 File Offset: 0x0004EFB9
		public event EventHandler WidthChanged
		{
			add
			{
				base.Events.AddHandler(DataGridColumnStyle.EventWidth, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataGridColumnStyle.EventWidth, value);
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00050DCC File Offset: 0x0004EFCC
		protected void BeginUpdate()
		{
			this.updating = true;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00050DD5 File Offset: 0x0004EFD5
		protected void EndUpdate()
		{
			this.updating = false;
			if (this.invalid)
			{
				this.invalid = false;
				this.Invalidate();
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool WantArrows
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00050DF3 File Offset: 0x0004EFF3
		internal virtual string GetDisplayText(object value)
		{
			return value.ToString();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00050DFB File Offset: 0x0004EFFB
		private void ResetNullText()
		{
			this.NullText = SR.GetString("DataGridNullText");
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00050E0D File Offset: 0x0004F00D
		private bool ShouldSerializeNullText()
		{
			return !SR.GetString("DataGridNullText").Equals(this.nullText);
		}

		// Token: 0x0600168C RID: 5772
		protected internal abstract Size GetPreferredSize(Graphics g, object value);

		// Token: 0x0600168D RID: 5773
		protected internal abstract int GetMinimumHeight();

		// Token: 0x0600168E RID: 5774
		protected internal abstract int GetPreferredHeight(Graphics g, object value);

		// Token: 0x0600168F RID: 5775 RVA: 0x00050E28 File Offset: 0x0004F028
		protected internal virtual object GetColumnValueAtRow(CurrencyManager source, int rowNum)
		{
			this.CheckValidDataSource(source);
			if (this.PropertyDescriptor == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridColumnNoPropertyDescriptor"));
			}
			return this.PropertyDescriptor.GetValue(source[rowNum]);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00050E68 File Offset: 0x0004F068
		protected virtual void Invalidate()
		{
			if (this.updating)
			{
				this.invalid = true;
				return;
			}
			DataGridTableStyle dataGridTableStyle = this.DataGridTableStyle;
			if (dataGridTableStyle != null)
			{
				dataGridTableStyle.InvalidateColumn(this);
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00050E98 File Offset: 0x0004F098
		protected void CheckValidDataSource(CurrencyManager value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", "DataGridColumnStyle.CheckValidDataSource(DataSource value), value == null");
			}
			if (this.PropertyDescriptor == null)
			{
				throw new InvalidOperationException(SR.GetString("DataGridColumnUnbound", new object[]
				{
					this.HeaderText
				}));
			}
		}

		// Token: 0x06001692 RID: 5778
		protected internal abstract void Abort(int rowNum);

		// Token: 0x06001693 RID: 5779
		protected internal abstract bool Commit(CurrencyManager dataSource, int rowNum);

		// Token: 0x06001694 RID: 5780 RVA: 0x00050EE1 File Offset: 0x0004F0E1
		protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly)
		{
			this.Edit(source, rowNum, bounds, readOnly, null, true);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00050EF0 File Offset: 0x0004F0F0
		protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText)
		{
			this.Edit(source, rowNum, bounds, readOnly, displayText, true);
		}

		// Token: 0x06001696 RID: 5782
		protected internal abstract void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible);

		// Token: 0x06001697 RID: 5783 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal virtual bool MouseDown(int rowNum, int x, int y)
		{
			return false;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000072B6 File Offset: 0x000054B6
		protected internal virtual void EnterNullValue()
		{
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00050F00 File Offset: 0x0004F100
		internal virtual bool KeyPress(int rowNum, Keys keyData)
		{
			if (this.ReadOnly || (this.DataGridTableStyle != null && this.DataGridTableStyle.DataGrid != null && this.DataGridTableStyle.DataGrid.ReadOnly))
			{
				return false;
			}
			if (keyData == (Keys)131168 || keyData == (Keys.ShiftKey | Keys.Space | Keys.Control))
			{
				this.EnterNullValue();
				return true;
			}
			return false;
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000072B6 File Offset: 0x000054B6
		protected internal virtual void ConcedeFocus()
		{
		}

		// Token: 0x0600169B RID: 5787
		protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum);

		// Token: 0x0600169C RID: 5788
		protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight);

		// Token: 0x0600169D RID: 5789 RVA: 0x00050F57 File Offset: 0x0004F157
		protected internal virtual void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
		{
			this.Paint(g, bounds, source, rowNum, alignToRight);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00050F68 File Offset: 0x0004F168
		private void OnPropertyDescriptorChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventPropertyDescriptor] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00050F98 File Offset: 0x0004F198
		private void OnAlignmentChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventAlignment] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00050FC8 File Offset: 0x0004F1C8
		private void OnHeaderTextChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventHeaderText] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00050FF8 File Offset: 0x0004F1F8
		private void OnMappingNameChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventMappingName] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00051028 File Offset: 0x0004F228
		private void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventReadOnly] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00051058 File Offset: 0x0004F258
		private void OnNullTextChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventNullText] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00051088 File Offset: 0x0004F288
		private void OnWidthChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DataGridColumnStyle.EventWidth] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000510B8 File Offset: 0x0004F2B8
		protected internal virtual void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
		{
			this.CheckValidDataSource(source);
			if (source.Position != rowNum)
			{
				throw new ArgumentException(SR.GetString("DataGridColumnListManagerPosition"), "rowNum");
			}
			if (source[rowNum] is IEditableObject)
			{
				((IEditableObject)source[rowNum]).BeginEdit();
			}
			this.PropertyDescriptor.SetValue(source[rowNum], value);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0005111C File Offset: 0x0004F31C
		protected internal virtual void ColumnStartedEditing(Control editingControl)
		{
			this.DataGridTableStyle.DataGrid.ColumnStartedEditing(editingControl);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005112F File Offset: 0x0004F32F
		void IDataGridColumnStyleEditingNotificationService.ColumnStartedEditing(Control editingControl)
		{
			this.ColumnStartedEditing(editingControl);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x000072B6 File Offset: 0x000054B6
		protected internal virtual void ReleaseHostedControl()
		{
		}

		// Token: 0x04000A43 RID: 2627
		private HorizontalAlignment alignment;

		// Token: 0x04000A44 RID: 2628
		private PropertyDescriptor propertyDescriptor;

		// Token: 0x04000A45 RID: 2629
		private DataGridTableStyle dataGridTableStyle;

		// Token: 0x04000A46 RID: 2630
		private Font font;

		// Token: 0x04000A47 RID: 2631
		internal int fontHeight = -1;

		// Token: 0x04000A48 RID: 2632
		private string mappingName = "";

		// Token: 0x04000A49 RID: 2633
		private string headerName = "";

		// Token: 0x04000A4A RID: 2634
		private bool invalid;

		// Token: 0x04000A4B RID: 2635
		private string nullText = SR.GetString("DataGridNullText");

		// Token: 0x04000A4C RID: 2636
		private bool readOnly;

		// Token: 0x04000A4D RID: 2637
		private bool updating;

		// Token: 0x04000A4E RID: 2638
		internal int width = -1;

		// Token: 0x04000A4F RID: 2639
		private bool isDefault;

		// Token: 0x04000A50 RID: 2640
		private AccessibleObject headerAccessibleObject;

		// Token: 0x04000A51 RID: 2641
		private static readonly object EventAlignment = new object();

		// Token: 0x04000A52 RID: 2642
		private static readonly object EventPropertyDescriptor = new object();

		// Token: 0x04000A53 RID: 2643
		private static readonly object EventHeaderText = new object();

		// Token: 0x04000A54 RID: 2644
		private static readonly object EventMappingName = new object();

		// Token: 0x04000A55 RID: 2645
		private static readonly object EventNullText = new object();

		// Token: 0x04000A56 RID: 2646
		private static readonly object EventReadOnly = new object();

		// Token: 0x04000A57 RID: 2647
		private static readonly object EventWidth = new object();

		// Token: 0x0200064D RID: 1613
		protected class CompModSwitches
		{
			// Token: 0x170015A2 RID: 5538
			// (get) Token: 0x060064E4 RID: 25828 RVA: 0x00177B9D File Offset: 0x00175D9D
			public static TraceSwitch DGEditColumnEditing
			{
				get
				{
					if (DataGridColumnStyle.CompModSwitches.dgEditColumnEditing == null)
					{
						DataGridColumnStyle.CompModSwitches.dgEditColumnEditing = new TraceSwitch("DGEditColumnEditing", "Editing related tracing");
					}
					return DataGridColumnStyle.CompModSwitches.dgEditColumnEditing;
				}
			}

			// Token: 0x040039DB RID: 14811
			private static TraceSwitch dgEditColumnEditing;
		}

		// Token: 0x0200064E RID: 1614
		[ComVisible(true)]
		protected class DataGridColumnHeaderAccessibleObject : AccessibleObject
		{
			// Token: 0x060064E6 RID: 25830 RVA: 0x00177BBF File Offset: 0x00175DBF
			public DataGridColumnHeaderAccessibleObject(DataGridColumnStyle owner) : this()
			{
				this.owner = owner;
			}

			// Token: 0x060064E7 RID: 25831 RVA: 0x00177BCE File Offset: 0x00175DCE
			public DataGridColumnHeaderAccessibleObject()
			{
			}

			// Token: 0x170015A3 RID: 5539
			// (get) Token: 0x060064E8 RID: 25832 RVA: 0x00177BD8 File Offset: 0x00175DD8
			public override Rectangle Bounds
			{
				get
				{
					if (this.owner.PropertyDescriptor == null)
					{
						return Rectangle.Empty;
					}
					DataGrid dataGrid = this.DataGrid;
					if (dataGrid.DataGridRowsLength == 0)
					{
						return Rectangle.Empty;
					}
					GridColumnStylesCollection gridColumnStyles = this.owner.dataGridTableStyle.GridColumnStyles;
					int col = -1;
					for (int i = 0; i < gridColumnStyles.Count; i++)
					{
						if (gridColumnStyles[i] == this.owner)
						{
							col = i;
							break;
						}
					}
					Rectangle cellBounds = dataGrid.GetCellBounds(0, col);
					cellBounds.Y = dataGrid.GetColumnHeadersRect().Y;
					return dataGrid.RectangleToScreen(cellBounds);
				}
			}

			// Token: 0x170015A4 RID: 5540
			// (get) Token: 0x060064E9 RID: 25833 RVA: 0x00177C70 File Offset: 0x00175E70
			public override string Name
			{
				get
				{
					return this.Owner.headerName;
				}
			}

			// Token: 0x170015A5 RID: 5541
			// (get) Token: 0x060064EA RID: 25834 RVA: 0x00177C7D File Offset: 0x00175E7D
			protected DataGridColumnStyle Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x170015A6 RID: 5542
			// (get) Token: 0x060064EB RID: 25835 RVA: 0x00177C85 File Offset: 0x00175E85
			public override AccessibleObject Parent
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					return this.DataGrid.AccessibilityObject;
				}
			}

			// Token: 0x170015A7 RID: 5543
			// (get) Token: 0x060064EC RID: 25836 RVA: 0x00177C92 File Offset: 0x00175E92
			private DataGrid DataGrid
			{
				get
				{
					return this.owner.dataGridTableStyle.dataGrid;
				}
			}

			// Token: 0x170015A8 RID: 5544
			// (get) Token: 0x060064ED RID: 25837 RVA: 0x00177CA4 File Offset: 0x00175EA4
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ColumnHeader;
				}
			}

			// Token: 0x060064EE RID: 25838 RVA: 0x00177CA8 File Offset: 0x00175EA8
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				switch (navdir)
				{
				case AccessibleNavigation.Up:
				case AccessibleNavigation.Left:
				case AccessibleNavigation.Previous:
					return this.Parent.GetChild(1 + this.Owner.dataGridTableStyle.GridColumnStyles.IndexOf(this.Owner) - 1);
				case AccessibleNavigation.Down:
				case AccessibleNavigation.Right:
				case AccessibleNavigation.Next:
					return this.Parent.GetChild(1 + this.Owner.dataGridTableStyle.GridColumnStyles.IndexOf(this.Owner) + 1);
				default:
					return null;
				}
			}

			// Token: 0x040039DC RID: 14812
			private DataGridColumnStyle owner;
		}
	}
}
