using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200038E RID: 910
	[ProvideProperty("ColumnSpan", typeof(Control))]
	[ProvideProperty("RowSpan", typeof(Control))]
	[ProvideProperty("Row", typeof(Control))]
	[ProvideProperty("Column", typeof(Control))]
	[ProvideProperty("CellPosition", typeof(Control))]
	[DefaultProperty("ColumnCount")]
	[DesignerSerializer("System.Windows.Forms.Design.TableLayoutPanelCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Docking(DockingBehavior.Never)]
	[Designer("System.Windows.Forms.Design.TableLayoutPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionTableLayoutPanel")]
	public class TableLayoutPanel : Panel, IExtenderProvider
	{
		// Token: 0x06003BB7 RID: 15287 RVA: 0x001057DD File Offset: 0x001039DD
		public TableLayoutPanel()
		{
			this._tableLayoutSettings = TableLayout.CreateSettings(this);
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x001057F1 File Offset: 0x001039F1
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return TableLayout.Instance;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x001057F8 File Offset: 0x001039F8
		// (set) Token: 0x06003BBA RID: 15290 RVA: 0x00105800 File Offset: 0x00103A00
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TableLayoutSettings LayoutSettings
		{
			get
			{
				return this._tableLayoutSettings;
			}
			set
			{
				if (value != null && value.IsStub)
				{
					using (new LayoutTransaction(this, this, PropertyNames.LayoutSettings))
					{
						this._tableLayoutSettings.ApplySettings(value);
						return;
					}
				}
				throw new NotSupportedException(SR.GetString("TableLayoutSettingSettingsIsNotSupported"));
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000FFF15 File Offset: 0x000FE115
		// (set) Token: 0x06003BBC RID: 15292 RVA: 0x000FFF1D File Offset: 0x000FE11D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Localizable(true)]
		public new BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x00105860 File Offset: 0x00103A60
		// (set) Token: 0x06003BBE RID: 15294 RVA: 0x0010586D File Offset: 0x00103A6D
		[DefaultValue(TableLayoutPanelCellBorderStyle.None)]
		[SRCategory("CatAppearance")]
		[SRDescription("TableLayoutPanelCellBorderStyleDescr")]
		[Localizable(true)]
		public TableLayoutPanelCellBorderStyle CellBorderStyle
		{
			get
			{
				return this._tableLayoutSettings.CellBorderStyle;
			}
			set
			{
				this._tableLayoutSettings.CellBorderStyle = value;
				if (value != TableLayoutPanelCellBorderStyle.None)
				{
					base.SetStyle(ControlStyles.ResizeRedraw, true);
				}
				base.Invalidate();
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x0010588D File Offset: 0x00103A8D
		private int CellBorderWidth
		{
			get
			{
				return this._tableLayoutSettings.CellBorderWidth;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x0010589A File Offset: 0x00103A9A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ControlControlsDescr")]
		public new TableLayoutControlCollection Controls
		{
			get
			{
				return (TableLayoutControlCollection)base.Controls;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x001058A7 File Offset: 0x00103AA7
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x001058B4 File Offset: 0x00103AB4
		[SRDescription("GridPanelColumnsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		[Localizable(true)]
		public int ColumnCount
		{
			get
			{
				return this._tableLayoutSettings.ColumnCount;
			}
			set
			{
				this._tableLayoutSettings.ColumnCount = value;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x001058C2 File Offset: 0x00103AC2
		// (set) Token: 0x06003BC4 RID: 15300 RVA: 0x001058CF File Offset: 0x00103ACF
		[SRDescription("TableLayoutPanelGrowStyleDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
		public TableLayoutPanelGrowStyle GrowStyle
		{
			get
			{
				return this._tableLayoutSettings.GrowStyle;
			}
			set
			{
				this._tableLayoutSettings.GrowStyle = value;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x001058DD File Offset: 0x00103ADD
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x001058EA File Offset: 0x00103AEA
		[SRDescription("GridPanelRowsDescr")]
		[SRCategory("CatLayout")]
		[DefaultValue(0)]
		[Localizable(true)]
		public int RowCount
		{
			get
			{
				return this._tableLayoutSettings.RowCount;
			}
			set
			{
				this._tableLayoutSettings.RowCount = value;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x001058F8 File Offset: 0x00103AF8
		[SRDescription("GridPanelRowStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		[DisplayName("Rows")]
		[MergableProperty(false)]
		[Browsable(false)]
		public TableLayoutRowStyleCollection RowStyles
		{
			get
			{
				return this._tableLayoutSettings.RowStyles;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x00105905 File Offset: 0x00103B05
		[SRDescription("GridPanelColumnStylesDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRCategory("CatLayout")]
		[DisplayName("Columns")]
		[Browsable(false)]
		[MergableProperty(false)]
		public TableLayoutColumnStyleCollection ColumnStyles
		{
			get
			{
				return this._tableLayoutSettings.ColumnStyles;
			}
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x00105912 File Offset: 0x00103B12
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TableLayoutControlCollection(this);
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x0010591C File Offset: 0x00103B1C
		private bool ShouldSerializeControls()
		{
			TableLayoutControlCollection controls = this.Controls;
			return controls != null && controls.Count > 0;
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x00105940 File Offset: 0x00103B40
		bool IExtenderProvider.CanExtend(object obj)
		{
			Control control = obj as Control;
			return control != null && control.Parent == this;
		}

		// Token: 0x06003BCC RID: 15308 RVA: 0x00105962 File Offset: 0x00103B62
		[SRDescription("GridPanelGetColumnSpanDescr")]
		[DefaultValue(1)]
		[SRCategory("CatLayout")]
		[DisplayName("ColumnSpan")]
		public int GetColumnSpan(Control control)
		{
			return this._tableLayoutSettings.GetColumnSpan(control);
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x00105970 File Offset: 0x00103B70
		public void SetColumnSpan(Control control, int value)
		{
			this._tableLayoutSettings.SetColumnSpan(control, value);
		}

		// Token: 0x06003BCE RID: 15310 RVA: 0x0010597F File Offset: 0x00103B7F
		[SRDescription("GridPanelGetRowSpanDescr")]
		[DefaultValue(1)]
		[SRCategory("CatLayout")]
		[DisplayName("RowSpan")]
		public int GetRowSpan(Control control)
		{
			return this._tableLayoutSettings.GetRowSpan(control);
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x0010598D File Offset: 0x00103B8D
		public void SetRowSpan(Control control, int value)
		{
			this._tableLayoutSettings.SetRowSpan(control, value);
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x0010599C File Offset: 0x00103B9C
		[DefaultValue(-1)]
		[SRDescription("GridPanelRowDescr")]
		[SRCategory("CatLayout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DisplayName("Row")]
		public int GetRow(Control control)
		{
			return this._tableLayoutSettings.GetRow(control);
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x001059AA File Offset: 0x00103BAA
		public void SetRow(Control control, int row)
		{
			this._tableLayoutSettings.SetRow(control, row);
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x001059B9 File Offset: 0x00103BB9
		[DefaultValue(typeof(TableLayoutPanelCellPosition), "-1,-1")]
		[SRDescription("GridPanelCellPositionDescr")]
		[SRCategory("CatLayout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DisplayName("Cell")]
		public TableLayoutPanelCellPosition GetCellPosition(Control control)
		{
			return this._tableLayoutSettings.GetCellPosition(control);
		}

		// Token: 0x06003BD3 RID: 15315 RVA: 0x001059C7 File Offset: 0x00103BC7
		public void SetCellPosition(Control control, TableLayoutPanelCellPosition position)
		{
			this._tableLayoutSettings.SetCellPosition(control, position);
		}

		// Token: 0x06003BD4 RID: 15316 RVA: 0x001059D6 File Offset: 0x00103BD6
		[DefaultValue(-1)]
		[SRDescription("GridPanelColumnDescr")]
		[SRCategory("CatLayout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DisplayName("Column")]
		public int GetColumn(Control control)
		{
			return this._tableLayoutSettings.GetColumn(control);
		}

		// Token: 0x06003BD5 RID: 15317 RVA: 0x001059E4 File Offset: 0x00103BE4
		public void SetColumn(Control control, int column)
		{
			this._tableLayoutSettings.SetColumn(control, column);
		}

		// Token: 0x06003BD6 RID: 15318 RVA: 0x001059F3 File Offset: 0x00103BF3
		public Control GetControlFromPosition(int column, int row)
		{
			return (Control)this._tableLayoutSettings.GetControlFromPosition(column, row);
		}

		// Token: 0x06003BD7 RID: 15319 RVA: 0x00105A07 File Offset: 0x00103C07
		public TableLayoutPanelCellPosition GetPositionFromControl(Control control)
		{
			return this._tableLayoutSettings.GetPositionFromControl(control);
		}

		// Token: 0x06003BD8 RID: 15320 RVA: 0x00105A18 File Offset: 0x00103C18
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int[] GetColumnWidths()
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			if (containerInfo.Columns == null)
			{
				return new int[0];
			}
			int[] array = new int[containerInfo.Columns.Length];
			for (int i = 0; i < containerInfo.Columns.Length; i++)
			{
				array[i] = containerInfo.Columns[i].MinSize;
			}
			return array;
		}

		// Token: 0x06003BD9 RID: 15321 RVA: 0x00105A74 File Offset: 0x00103C74
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int[] GetRowHeights()
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			if (containerInfo.Rows == null)
			{
				return new int[0];
			}
			int[] array = new int[containerInfo.Rows.Length];
			for (int i = 0; i < containerInfo.Rows.Length; i++)
			{
				array[i] = containerInfo.Rows[i].MinSize;
			}
			return array;
		}

		// Token: 0x140002DE RID: 734
		// (add) Token: 0x06003BDA RID: 15322 RVA: 0x00105ACD File Offset: 0x00103CCD
		// (remove) Token: 0x06003BDB RID: 15323 RVA: 0x00105AE0 File Offset: 0x00103CE0
		[SRCategory("CatAppearance")]
		[SRDescription("TableLayoutPanelOnPaintCellDescr")]
		public event TableLayoutCellPaintEventHandler CellPaint
		{
			add
			{
				base.Events.AddHandler(TableLayoutPanel.EventCellPaint, value);
			}
			remove
			{
				base.Events.RemoveHandler(TableLayoutPanel.EventCellPaint, value);
			}
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x00105AF3 File Offset: 0x00103CF3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			base.Invalidate();
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x00105B04 File Offset: 0x00103D04
		protected virtual void OnCellPaint(TableLayoutCellPaintEventArgs e)
		{
			TableLayoutCellPaintEventHandler tableLayoutCellPaintEventHandler = (TableLayoutCellPaintEventHandler)base.Events[TableLayoutPanel.EventCellPaint];
			if (tableLayoutCellPaintEventHandler != null)
			{
				tableLayoutCellPaintEventHandler(this, e);
			}
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x00105B34 File Offset: 0x00103D34
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			int cellBorderWidth = this.CellBorderWidth;
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			TableLayout.Strip[] columns = containerInfo.Columns;
			TableLayout.Strip[] rows = containerInfo.Rows;
			TableLayoutPanelCellBorderStyle cellBorderStyle = this.CellBorderStyle;
			if (columns == null || rows == null)
			{
				return;
			}
			int num = columns.Length;
			int num2 = rows.Length;
			int num3 = 0;
			int num4 = 0;
			Graphics graphics = e.Graphics;
			Rectangle displayRectangle = this.DisplayRectangle;
			Rectangle clipRectangle = e.ClipRectangle;
			bool flag = this.RightToLeft == RightToLeft.Yes;
			int num5;
			if (flag)
			{
				num5 = displayRectangle.Right - cellBorderWidth / 2;
			}
			else
			{
				num5 = displayRectangle.X + cellBorderWidth / 2;
			}
			for (int i = 0; i < num; i++)
			{
				int num6 = displayRectangle.Y + cellBorderWidth / 2;
				if (flag)
				{
					num5 -= columns[i].MinSize;
				}
				for (int j = 0; j < num2; j++)
				{
					int x = num5;
					int y = num6;
					TableLayout.Strip strip = columns[i];
					int minSize = strip.MinSize;
					strip = rows[j];
					Rectangle bound = new Rectangle(x, y, minSize, strip.MinSize);
					Rectangle rectangle = new Rectangle(bound.X + (cellBorderWidth + 1) / 2, bound.Y + (cellBorderWidth + 1) / 2, bound.Width - (cellBorderWidth + 1) / 2, bound.Height - (cellBorderWidth + 1) / 2);
					if (clipRectangle.IntersectsWith(rectangle))
					{
						using (TableLayoutCellPaintEventArgs tableLayoutCellPaintEventArgs = new TableLayoutCellPaintEventArgs(graphics, clipRectangle, rectangle, i, j))
						{
							this.OnCellPaint(tableLayoutCellPaintEventArgs);
						}
						ControlPaint.PaintTableCellBorder(cellBorderStyle, graphics, bound);
					}
					num6 += rows[j].MinSize;
					if (i == 0)
					{
						num4 += rows[j].MinSize;
					}
				}
				if (!flag)
				{
					num5 += columns[i].MinSize;
				}
				num3 += columns[i].MinSize;
			}
			if (!base.HScroll && !base.VScroll && cellBorderStyle != TableLayoutPanelCellBorderStyle.None)
			{
				Rectangle bound2 = new Rectangle(cellBorderWidth / 2 + displayRectangle.X, cellBorderWidth / 2 + displayRectangle.Y, displayRectangle.Width - cellBorderWidth, displayRectangle.Height - cellBorderWidth);
				if (cellBorderStyle == TableLayoutPanelCellBorderStyle.Inset)
				{
					graphics.DrawLine(SystemPens.ControlDark, bound2.Right, bound2.Y, bound2.Right, bound2.Bottom);
					graphics.DrawLine(SystemPens.ControlDark, bound2.X, bound2.Y + bound2.Height - 1, bound2.X + bound2.Width - 1, bound2.Y + bound2.Height - 1);
				}
				else
				{
					if (cellBorderStyle == TableLayoutPanelCellBorderStyle.Outset)
					{
						using (Pen pen = new Pen(SystemColors.Window))
						{
							graphics.DrawLine(pen, bound2.X + bound2.Width - 1, bound2.Y, bound2.X + bound2.Width - 1, bound2.Y + bound2.Height - 1);
							graphics.DrawLine(pen, bound2.X, bound2.Y + bound2.Height - 1, bound2.X + bound2.Width - 1, bound2.Y + bound2.Height - 1);
							goto IL_342;
						}
					}
					ControlPaint.PaintTableCellBorder(cellBorderStyle, graphics, bound2);
				}
				IL_342:
				ControlPaint.PaintTableControlBorder(cellBorderStyle, graphics, displayRectangle);
				return;
			}
			ControlPaint.PaintTableControlBorder(cellBorderStyle, graphics, displayRectangle);
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x00105EB8 File Offset: 0x001040B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
			this.ScaleAbsoluteStyles(new SizeF(dx, dy));
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x00105ECF File Offset: 0x001040CF
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
			this.ScaleAbsoluteStyles(factor);
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00105EE0 File Offset: 0x001040E0
		private void ScaleAbsoluteStyles(SizeF factor)
		{
			TableLayout.ContainerInfo containerInfo = TableLayout.GetContainerInfo(this);
			int num = 0;
			int num2 = -1;
			int num3 = containerInfo.Rows.Length - 1;
			if (containerInfo.Rows.Length != 0)
			{
				num2 = containerInfo.Rows[num3].MinSize;
			}
			int num4 = -1;
			int num5 = containerInfo.Columns.Length - 1;
			if (containerInfo.Columns.Length != 0)
			{
				num4 = containerInfo.Columns[containerInfo.Columns.Length - 1].MinSize;
			}
			foreach (object obj in ((IEnumerable)this.ColumnStyles))
			{
				ColumnStyle columnStyle = (ColumnStyle)obj;
				if (columnStyle.SizeType == SizeType.Absolute)
				{
					if (num == num5 && num4 > 0)
					{
						columnStyle.Width = (float)Math.Round((double)((float)num4 * factor.Width));
					}
					else
					{
						columnStyle.Width = (float)Math.Round((double)(columnStyle.Width * factor.Width));
					}
				}
				num++;
			}
			num = 0;
			foreach (object obj2 in ((IEnumerable)this.RowStyles))
			{
				RowStyle rowStyle = (RowStyle)obj2;
				if (rowStyle.SizeType == SizeType.Absolute)
				{
					if (num == num3 && num2 > 0)
					{
						rowStyle.Height = (float)Math.Round((double)((float)num2 * factor.Height));
					}
					else
					{
						rowStyle.Height = (float)Math.Round((double)(rowStyle.Height * factor.Height));
					}
				}
			}
		}

		// Token: 0x04002384 RID: 9092
		private TableLayoutSettings _tableLayoutSettings;

		// Token: 0x04002385 RID: 9093
		private static readonly object EventCellPaint = new object();
	}
}
