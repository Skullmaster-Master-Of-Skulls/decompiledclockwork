using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000394 RID: 916
	[TypeConverter(typeof(TableLayoutSettingsTypeConverter))]
	[Serializable]
	public sealed class TableLayoutSettings : LayoutSettings, ISerializable
	{
		// Token: 0x06003BF9 RID: 15353 RVA: 0x0010642E File Offset: 0x0010462E
		internal TableLayoutSettings() : base(null)
		{
			this._stub = new TableLayoutSettings.TableLayoutSettingsStub();
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x000AFC6F File Offset: 0x000ADE6F
		internal TableLayoutSettings(IArrangedElement owner) : base(owner)
		{
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x00106444 File Offset: 0x00104644
		internal TableLayoutSettings(SerializationInfo serializationInfo, StreamingContext context) : this()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(this);
			string @string = serializationInfo.GetString("SerializedString");
			if (!string.IsNullOrEmpty(@string) && converter != null)
			{
				TableLayoutSettings tableLayoutSettings = converter.ConvertFromInvariantString(@string) as TableLayoutSettings;
				if (tableLayoutSettings != null)
				{
					this.ApplySettings(tableLayoutSettings);
				}
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06003BFC RID: 15356 RVA: 0x001057F1 File Offset: 0x001039F1
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return TableLayout.Instance;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x0010648C File Offset: 0x0010468C
		private TableLayout TableLayout
		{
			get
			{
				return (TableLayout)this.LayoutEngine;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06003BFE RID: 15358 RVA: 0x00106499 File Offset: 0x00104699
		// (set) Token: 0x06003BFF RID: 15359 RVA: 0x001064A4 File Offset: 0x001046A4
		[DefaultValue(TableLayoutPanelCellBorderStyle.None)]
		[SRCategory("CatAppearance")]
		[SRDescription("TableLayoutPanelCellBorderStyleDescr")]
		internal TableLayoutPanelCellBorderStyle CellBorderStyle
		{
			get
			{
				return this._borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 6))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"CellBorderStyle",
						value.ToString()
					}));
				}
				this._borderStyle = value;
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				containerInfo.CellBorderWidth = TableLayoutSettings.borderStyleToOffset[(int)value];
				LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.CellBorderStyle);
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003C00 RID: 15360 RVA: 0x00106525 File Offset: 0x00104725
		[DefaultValue(0)]
		internal int CellBorderWidth
		{
			get
			{
				return TableLayout.GetContainerInfo(base.Owner).CellBorderWidth;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06003C01 RID: 15361 RVA: 0x00106538 File Offset: 0x00104738
		// (set) Token: 0x06003C02 RID: 15362 RVA: 0x00106558 File Offset: 0x00104758
		[SRDescription("GridPanelColumnsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		public int ColumnCount
		{
			get
			{
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.MaxColumns;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("ColumnCount", value, SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ColumnCount",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				containerInfo.MaxColumns = value;
				LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.Columns);
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06003C03 RID: 15363 RVA: 0x001065DC File Offset: 0x001047DC
		// (set) Token: 0x06003C04 RID: 15364 RVA: 0x001065FC File Offset: 0x001047FC
		[SRDescription("GridPanelRowsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		public int RowCount
		{
			get
			{
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.MaxRows;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("RowCount", value, SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"RowCount",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				containerInfo.MaxRows = value;
				LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.Rows);
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x00106680 File Offset: 0x00104880
		[SRDescription("GridPanelRowStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		public TableLayoutRowStyleCollection RowStyles
		{
			get
			{
				if (this.IsStub)
				{
					return this._stub.RowStyles;
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.RowStyles;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x001066B4 File Offset: 0x001048B4
		[SRDescription("GridPanelColumnStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		public TableLayoutColumnStyleCollection ColumnStyles
		{
			get
			{
				if (this.IsStub)
				{
					return this._stub.ColumnStyles;
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				return containerInfo.ColumnStyles;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003C07 RID: 15367 RVA: 0x001066E7 File Offset: 0x001048E7
		// (set) Token: 0x06003C08 RID: 15368 RVA: 0x001066FC File Offset: 0x001048FC
		[SRDescription("TableLayoutPanelGrowStyleDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
		public TableLayoutPanelGrowStyle GrowStyle
		{
			get
			{
				return TableLayout.GetContainerInfo(base.Owner).GrowStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"GrowStyle",
						value.ToString()
					}));
				}
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(base.Owner);
				if (containerInfo.GrowStyle != value)
				{
					containerInfo.GrowStyle = value;
					LayoutTransaction.DoLayout(base.Owner, base.Owner, PropertyNames.GrowStyle);
				}
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003C09 RID: 15369 RVA: 0x00106779 File Offset: 0x00104979
		internal bool IsStub
		{
			get
			{
				return this._stub != null;
			}
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x00106786 File Offset: 0x00104986
		internal void ApplySettings(TableLayoutSettings settings)
		{
			if (settings.IsStub)
			{
				if (!this.IsStub)
				{
					settings._stub.ApplySettings(this);
					return;
				}
				this._stub = settings._stub;
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x001067B4 File Offset: 0x001049B4
		public int GetColumnSpan(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				return this._stub.GetColumnSpan(control);
			}
			IArrangedElement element = this.LayoutEngine.CastToArrangedElement(control);
			return TableLayout.GetLayoutInfo(element).ColumnSpan;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x001067FC File Offset: 0x001049FC
		public void SetColumnSpan(object control, int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("ColumnSpan", SR.GetString("InvalidArgument", new object[]
				{
					"ColumnSpan",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.IsStub)
			{
				this._stub.SetColumnSpan(control, value);
				return;
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			if (arrangedElement.Container != null)
			{
				TableLayout.ClearCachedAssignments(TableLayout.GetContainerInfo(arrangedElement.Container));
			}
			TableLayout.GetLayoutInfo(arrangedElement).ColumnSpan = value;
			LayoutTransaction.DoLayout(arrangedElement.Container, arrangedElement, PropertyNames.ColumnSpan);
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x00106898 File Offset: 0x00104A98
		public int GetRowSpan(object control)
		{
			if (this.IsStub)
			{
				return this._stub.GetRowSpan(control);
			}
			IArrangedElement element = this.LayoutEngine.CastToArrangedElement(control);
			return TableLayout.GetLayoutInfo(element).RowSpan;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x001068D4 File Offset: 0x00104AD4
		public void SetRowSpan(object control, int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("RowSpan", SR.GetString("InvalidArgument", new object[]
				{
					"RowSpan",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				this._stub.SetRowSpan(control, value);
				return;
			}
			IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
			if (arrangedElement.Container != null)
			{
				TableLayout.ClearCachedAssignments(TableLayout.GetContainerInfo(arrangedElement.Container));
			}
			TableLayout.GetLayoutInfo(arrangedElement).RowSpan = value;
			LayoutTransaction.DoLayout(arrangedElement.Container, arrangedElement, PropertyNames.RowSpan);
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x00106980 File Offset: 0x00104B80
		[SRDescription("GridPanelRowDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public int GetRow(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				return this._stub.GetRow(control);
			}
			IArrangedElement element = this.LayoutEngine.CastToArrangedElement(control);
			TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(element);
			return layoutInfo.RowPosition;
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x001069CC File Offset: 0x00104BCC
		public void SetRow(object control, int row)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (row < -1)
			{
				throw new ArgumentOutOfRangeException("Row", SR.GetString("InvalidArgument", new object[]
				{
					"Row",
					row.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.SetCellPosition(control, row, -1, true, false);
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00106A28 File Offset: 0x00104C28
		[SRDescription("TableLayoutSettingsGetCellPositionDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public TableLayoutPanelCellPosition GetCellPosition(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			return new TableLayoutPanelCellPosition(this.GetColumn(control), this.GetRow(control));
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x00106A4B File Offset: 0x00104C4B
		[SRDescription("TableLayoutSettingsSetCellPositionDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public void SetCellPosition(object control, TableLayoutPanelCellPosition cellPosition)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this.SetCellPosition(control, cellPosition.Row, cellPosition.Column, true, true);
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00106A74 File Offset: 0x00104C74
		[SRDescription("GridPanelColumnDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(-1)]
		public int GetColumn(object control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.IsStub)
			{
				return this._stub.GetColumn(control);
			}
			IArrangedElement element = this.LayoutEngine.CastToArrangedElement(control);
			TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(element);
			return layoutInfo.ColumnPosition;
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x00106AC0 File Offset: 0x00104CC0
		public void SetColumn(object control, int column)
		{
			if (column < -1)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"Column",
					column.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.IsStub)
			{
				this._stub.SetColumn(control, column);
				return;
			}
			this.SetCellPosition(control, -1, column, false, true);
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x00106B20 File Offset: 0x00104D20
		private void SetCellPosition(object control, int row, int column, bool rowSpecified, bool colSpecified)
		{
			if (this.IsStub)
			{
				if (colSpecified)
				{
					this._stub.SetColumn(control, column);
				}
				if (rowSpecified)
				{
					this._stub.SetRow(control, row);
					return;
				}
			}
			else
			{
				IArrangedElement arrangedElement = this.LayoutEngine.CastToArrangedElement(control);
				if (arrangedElement.Container != null)
				{
					TableLayout.ClearCachedAssignments(TableLayout.GetContainerInfo(arrangedElement.Container));
				}
				TableLayout.LayoutInfo layoutInfo = TableLayout.GetLayoutInfo(arrangedElement);
				if (colSpecified)
				{
					layoutInfo.ColumnPosition = column;
				}
				if (rowSpecified)
				{
					layoutInfo.RowPosition = row;
				}
				LayoutTransaction.DoLayout(arrangedElement.Container, arrangedElement, PropertyNames.TableIndex);
			}
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00106BAB File Offset: 0x00104DAB
		internal IArrangedElement GetControlFromPosition(int column, int row)
		{
			return this.TableLayout.GetControlFromPosition(base.Owner, column, row);
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x00106BC0 File Offset: 0x00104DC0
		internal TableLayoutPanelCellPosition GetPositionFromControl(IArrangedElement element)
		{
			return this.TableLayout.GetPositionFromControl(base.Owner, element);
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x00106BD4 File Offset: 0x00104DD4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(this);
			string value = (converter != null) ? converter.ConvertToInvariantString(this) : null;
			if (!string.IsNullOrEmpty(value))
			{
				si.AddValue("SerializedString", value);
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00106C0C File Offset: 0x00104E0C
		internal List<TableLayoutSettings.ControlInformation> GetControlsInformation()
		{
			if (this.IsStub)
			{
				return this._stub.GetControlsInformation();
			}
			List<TableLayoutSettings.ControlInformation> list = new List<TableLayoutSettings.ControlInformation>(base.Owner.Children.Count);
			foreach (object obj in base.Owner.Children)
			{
				IArrangedElement arrangedElement = (IArrangedElement)obj;
				Control control = arrangedElement as Control;
				if (control != null)
				{
					TableLayoutSettings.ControlInformation item = default(TableLayoutSettings.ControlInformation);
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["Name"];
					if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
					{
						item.Name = propertyDescriptor.GetValue(control);
					}
					item.Row = this.GetRow(control);
					item.RowSpan = this.GetRowSpan(control);
					item.Column = this.GetColumn(control);
					item.ColumnSpan = this.GetColumnSpan(control);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x04002395 RID: 9109
		private static int[] borderStyleToOffset = new int[]
		{
			0,
			1,
			2,
			3,
			2,
			3,
			3
		};

		// Token: 0x04002396 RID: 9110
		private TableLayoutPanelCellBorderStyle _borderStyle;

		// Token: 0x04002397 RID: 9111
		private TableLayoutSettings.TableLayoutSettingsStub _stub;

		// Token: 0x020007F0 RID: 2032
		internal struct ControlInformation
		{
			// Token: 0x06006E60 RID: 28256 RVA: 0x00194C43 File Offset: 0x00192E43
			internal ControlInformation(object name, int row, int column, int rowSpan, int columnSpan)
			{
				this.Name = name;
				this.Row = row;
				this.Column = column;
				this.RowSpan = rowSpan;
				this.ColumnSpan = columnSpan;
			}

			// Token: 0x040042DA RID: 17114
			internal object Name;

			// Token: 0x040042DB RID: 17115
			internal int Row;

			// Token: 0x040042DC RID: 17116
			internal int Column;

			// Token: 0x040042DD RID: 17117
			internal int RowSpan;

			// Token: 0x040042DE RID: 17118
			internal int ColumnSpan;
		}

		// Token: 0x020007F1 RID: 2033
		private class TableLayoutSettingsStub
		{
			// Token: 0x06006E62 RID: 28258 RVA: 0x00194C7C File Offset: 0x00192E7C
			internal void ApplySettings(TableLayoutSettings settings)
			{
				TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(settings.Owner);
				Control control = containerInfo.Container as Control;
				if (control != null && this.controlsInfo != null)
				{
					foreach (object obj in this.controlsInfo.Keys)
					{
						TableLayoutSettings.ControlInformation controlInformation = this.controlsInfo[obj];
						foreach (object obj2 in control.Controls)
						{
							Control control2 = (Control)obj2;
							if (control2 != null)
							{
								string @string = null;
								PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control2)["Name"];
								if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string))
								{
									@string = (propertyDescriptor.GetValue(control2) as string);
								}
								if (WindowsFormsUtils.SafeCompareStrings(@string, obj as string, false))
								{
									settings.SetRow(control2, controlInformation.Row);
									settings.SetColumn(control2, controlInformation.Column);
									settings.SetRowSpan(control2, controlInformation.RowSpan);
									settings.SetColumnSpan(control2, controlInformation.ColumnSpan);
									break;
								}
							}
						}
					}
				}
				containerInfo.RowStyles = this.rowStyles;
				containerInfo.ColumnStyles = this.columnStyles;
				this.columnStyles = null;
				this.rowStyles = null;
				this.isValid = false;
			}

			// Token: 0x1700181B RID: 6171
			// (get) Token: 0x06006E63 RID: 28259 RVA: 0x00194E38 File Offset: 0x00193038
			public TableLayoutColumnStyleCollection ColumnStyles
			{
				get
				{
					if (this.columnStyles == null)
					{
						this.columnStyles = new TableLayoutColumnStyleCollection();
					}
					return this.columnStyles;
				}
			}

			// Token: 0x1700181C RID: 6172
			// (get) Token: 0x06006E64 RID: 28260 RVA: 0x00194E53 File Offset: 0x00193053
			public bool IsValid
			{
				get
				{
					return this.isValid;
				}
			}

			// Token: 0x1700181D RID: 6173
			// (get) Token: 0x06006E65 RID: 28261 RVA: 0x00194E5B File Offset: 0x0019305B
			public TableLayoutRowStyleCollection RowStyles
			{
				get
				{
					if (this.rowStyles == null)
					{
						this.rowStyles = new TableLayoutRowStyleCollection();
					}
					return this.rowStyles;
				}
			}

			// Token: 0x06006E66 RID: 28262 RVA: 0x00194E78 File Offset: 0x00193078
			internal List<TableLayoutSettings.ControlInformation> GetControlsInformation()
			{
				if (this.controlsInfo == null)
				{
					return new List<TableLayoutSettings.ControlInformation>();
				}
				List<TableLayoutSettings.ControlInformation> list = new List<TableLayoutSettings.ControlInformation>(this.controlsInfo.Count);
				foreach (object obj in this.controlsInfo.Keys)
				{
					TableLayoutSettings.ControlInformation item = this.controlsInfo[obj];
					item.Name = obj;
					list.Add(item);
				}
				return list;
			}

			// Token: 0x06006E67 RID: 28263 RVA: 0x00194F08 File Offset: 0x00193108
			private TableLayoutSettings.ControlInformation GetControlInformation(object controlName)
			{
				if (this.controlsInfo == null)
				{
					return TableLayoutSettings.TableLayoutSettingsStub.DefaultControlInfo;
				}
				if (!this.controlsInfo.ContainsKey(controlName))
				{
					return TableLayoutSettings.TableLayoutSettingsStub.DefaultControlInfo;
				}
				return this.controlsInfo[controlName];
			}

			// Token: 0x06006E68 RID: 28264 RVA: 0x00194F38 File Offset: 0x00193138
			public int GetColumn(object controlName)
			{
				return this.GetControlInformation(controlName).Column;
			}

			// Token: 0x06006E69 RID: 28265 RVA: 0x00194F46 File Offset: 0x00193146
			public int GetColumnSpan(object controlName)
			{
				return this.GetControlInformation(controlName).ColumnSpan;
			}

			// Token: 0x06006E6A RID: 28266 RVA: 0x00194F54 File Offset: 0x00193154
			public int GetRow(object controlName)
			{
				return this.GetControlInformation(controlName).Row;
			}

			// Token: 0x06006E6B RID: 28267 RVA: 0x00194F62 File Offset: 0x00193162
			public int GetRowSpan(object controlName)
			{
				return this.GetControlInformation(controlName).RowSpan;
			}

			// Token: 0x06006E6C RID: 28268 RVA: 0x00194F70 File Offset: 0x00193170
			private void SetControlInformation(object controlName, TableLayoutSettings.ControlInformation info)
			{
				if (this.controlsInfo == null)
				{
					this.controlsInfo = new Dictionary<object, TableLayoutSettings.ControlInformation>();
				}
				this.controlsInfo[controlName] = info;
			}

			// Token: 0x06006E6D RID: 28269 RVA: 0x00194F94 File Offset: 0x00193194
			public void SetColumn(object controlName, int column)
			{
				if (this.GetColumn(controlName) != column)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.Column = column;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x06006E6E RID: 28270 RVA: 0x00194FC4 File Offset: 0x001931C4
			public void SetColumnSpan(object controlName, int value)
			{
				if (this.GetColumnSpan(controlName) != value)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.ColumnSpan = value;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x06006E6F RID: 28271 RVA: 0x00194FF4 File Offset: 0x001931F4
			public void SetRow(object controlName, int row)
			{
				if (this.GetRow(controlName) != row)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.Row = row;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x06006E70 RID: 28272 RVA: 0x00195024 File Offset: 0x00193224
			public void SetRowSpan(object controlName, int value)
			{
				if (this.GetRowSpan(controlName) != value)
				{
					TableLayoutSettings.ControlInformation controlInformation = this.GetControlInformation(controlName);
					controlInformation.RowSpan = value;
					this.SetControlInformation(controlName, controlInformation);
				}
			}

			// Token: 0x040042DF RID: 17119
			private static TableLayoutSettings.ControlInformation DefaultControlInfo = new TableLayoutSettings.ControlInformation(null, -1, -1, 1, 1);

			// Token: 0x040042E0 RID: 17120
			private TableLayoutColumnStyleCollection columnStyles;

			// Token: 0x040042E1 RID: 17121
			private TableLayoutRowStyleCollection rowStyles;

			// Token: 0x040042E2 RID: 17122
			private Dictionary<object, TableLayoutSettings.ControlInformation> controlsInfo;

			// Token: 0x040042E3 RID: 17123
			private bool isValid = true;
		}

		// Token: 0x020007F2 RID: 2034
		internal class StyleConverter : TypeConverter
		{
			// Token: 0x06006E72 RID: 28274 RVA: 0x00027AC8 File Offset: 0x00025CC8
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06006E73 RID: 28275 RVA: 0x00195064 File Offset: 0x00193264
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw new ArgumentNullException("destinationType");
				}
				if (destinationType == typeof(InstanceDescriptor) && value is TableLayoutStyle)
				{
					TableLayoutStyle tableLayoutStyle = (TableLayoutStyle)value;
					SizeType sizeType = tableLayoutStyle.SizeType;
					if (sizeType == SizeType.AutoSize)
					{
						return new InstanceDescriptor(tableLayoutStyle.GetType().GetConstructor(new Type[0]), new object[0]);
					}
					if (sizeType - SizeType.Absolute <= 1)
					{
						return new InstanceDescriptor(tableLayoutStyle.GetType().GetConstructor(new Type[]
						{
							typeof(SizeType),
							typeof(int)
						}), new object[]
						{
							tableLayoutStyle.SizeType,
							tableLayoutStyle.Size
						});
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
