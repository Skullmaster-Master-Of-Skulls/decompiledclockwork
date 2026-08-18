using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Internal;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using System.Windows.Forms.Internal;
using System.Windows.Forms.VisualStyles;
using Accessibility;
using Microsoft.Win32;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FA RID: 1274
	internal class PropertyGridView : Control, IWin32Window, IWindowsFormsEditorService, IServiceProvider
	{
		// Token: 0x060052A1 RID: 21153 RVA: 0x00156D90 File Offset: 0x00154F90
		public PropertyGridView(IServiceProvider serviceProvider, PropertyGrid propertyGrid)
		{
			if (DpiHelper.IsScalingRequired)
			{
				this.paintWidth = DpiHelper.LogicalToDeviceUnitsX(20);
				this.paintIndent = DpiHelper.LogicalToDeviceUnitsX(26);
				this.outlineSizeExplorerTreeStyle = DpiHelper.LogicalToDeviceUnitsX(16);
				this.outlineSize = DpiHelper.LogicalToDeviceUnitsX(9);
				this.maxListBoxHeight = DpiHelper.LogicalToDeviceUnitsY(200);
			}
			this.ehValueClick = new EventHandler(this.OnGridEntryValueClick);
			this.ehLabelClick = new EventHandler(this.OnGridEntryLabelClick);
			this.ehOutlineClick = new EventHandler(this.OnGridEntryOutlineClick);
			this.ehValueDblClick = new EventHandler(this.OnGridEntryValueDoubleClick);
			this.ehLabelDblClick = new EventHandler(this.OnGridEntryLabelDoubleClick);
			this.ehRecreateChildren = new GridEntryRecreateChildrenEventHandler(this.OnRecreateChildren);
			this.ownerGrid = propertyGrid;
			this.serviceProvider = serviceProvider;
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.ResizeRedraw, false);
			base.SetStyle(ControlStyles.UserMouse, true);
			this.BackColor = SystemColors.Window;
			this.ForeColor = SystemColors.WindowText;
			this.grayTextColor = SystemColors.GrayText;
			this.backgroundBrush = SystemBrushes.Window;
			base.TabStop = true;
			this.Text = "PropertyGridView";
			this.CreateUI();
			this.LayoutWindow(true);
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x060052A3 RID: 21155 RVA: 0x00156F84 File Offset: 0x00155184
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				this.backgroundBrush = new SolidBrush(value);
				base.BackColor = value;
			}
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x00156F99 File Offset: 0x00155199
		internal Brush GetBackgroundBrush(Graphics g)
		{
			return this.backgroundBrush;
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x00156FA4 File Offset: 0x001551A4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanCopy
		{
			get
			{
				if (this.selectedGridEntry == null)
				{
					return false;
				}
				if (!this.Edit.Focused)
				{
					string propertyTextValue = this.selectedGridEntry.GetPropertyTextValue();
					return propertyTextValue != null && propertyTextValue.Length > 0;
				}
				return true;
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x00156FE4 File Offset: 0x001551E4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanCut
		{
			get
			{
				return this.CanCopy && this.selectedGridEntry != null && this.selectedGridEntry.IsTextEditable;
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x00157003 File Offset: 0x00155203
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanPaste
		{
			get
			{
				return this.selectedGridEntry != null && this.selectedGridEntry.IsTextEditable;
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x0015701A File Offset: 0x0015521A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool CanUndo
		{
			get
			{
				return this.Edit.Visible && this.Edit.Focused && (int)this.Edit.SendMessage(198, 0, 0) != 0;
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x00157054 File Offset: 0x00155254
		internal DropDownButton DropDownButton
		{
			get
			{
				if (this.btnDropDown == null)
				{
					this.btnDropDown = new DropDownButton();
					this.btnDropDown.UseComboBoxTheme = true;
					Bitmap image = this.CreateResizedBitmap("Arrow.ico", 16, 16);
					this.btnDropDown.Image = image;
					this.btnDropDown.BackColor = SystemColors.Control;
					this.btnDropDown.ForeColor = SystemColors.ControlText;
					this.btnDropDown.Click += this.OnBtnClick;
					this.btnDropDown.GotFocus += this.OnDropDownButtonGotFocus;
					this.btnDropDown.LostFocus += this.OnChildLostFocus;
					this.btnDropDown.TabIndex = 2;
					this.CommonEditorSetup(this.btnDropDown);
					this.btnDropDown.Size = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight) : new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight));
				}
				return this.btnDropDown;
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x0015715C File Offset: 0x0015535C
		internal Button DialogButton
		{
			get
			{
				if (this.btnDialog == null)
				{
					this.btnDialog = new DropDownButton();
					this.btnDialog.BackColor = SystemColors.Control;
					this.btnDialog.ForeColor = SystemColors.ControlText;
					this.btnDialog.TabIndex = 3;
					this.btnDialog.Image = this.CreateResizedBitmap("dotdotdot.ico", 7, 8);
					this.btnDialog.Click += this.OnBtnClick;
					this.btnDialog.KeyDown += this.OnBtnKeyDown;
					this.btnDialog.GotFocus += this.OnDropDownButtonGotFocus;
					this.btnDialog.LostFocus += this.OnChildLostFocus;
					this.btnDialog.Size = (DpiHelper.EnableDpiChangedHighDpiImprovements ? new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight) : new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight));
					this.CommonEditorSetup(this.btnDialog);
				}
				return this.btnDialog;
			}
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0015726C File Offset: 0x0015546C
		private static Bitmap GetBitmapFromIcon(string iconName, int iconsWidth, int iconsHeight)
		{
			Size size = new Size(iconsWidth, iconsHeight);
			Icon icon = new Icon(BitmapSelector.GetResourceStream(typeof(PropertyGrid), iconName), size);
			Bitmap bitmap = icon.ToBitmap();
			icon.Dispose();
			if ((DpiHelper.IsScalingRequired || DpiHelper.EnableDpiChangedHighDpiImprovements) && (bitmap.Size.Width != iconsWidth || bitmap.Size.Height != iconsHeight))
			{
				Bitmap bitmap2 = DpiHelper.CreateResizedBitmap(bitmap, size);
				if (bitmap2 != null)
				{
					bitmap.Dispose();
					bitmap = bitmap2;
				}
			}
			return bitmap;
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x001572F0 File Offset: 0x001554F0
		private PropertyGridView.GridViewEdit Edit
		{
			get
			{
				if (this.edit == null)
				{
					this.edit = new PropertyGridView.GridViewEdit(this);
					this.edit.BorderStyle = BorderStyle.None;
					this.edit.AutoSize = false;
					this.edit.TabStop = false;
					this.edit.AcceptsReturn = true;
					this.edit.BackColor = this.BackColor;
					this.edit.ForeColor = this.ForeColor;
					this.edit.KeyDown += this.OnEditKeyDown;
					this.edit.KeyPress += this.OnEditKeyPress;
					this.edit.GotFocus += this.OnEditGotFocus;
					this.edit.LostFocus += this.OnEditLostFocus;
					this.edit.MouseDown += this.OnEditMouseDown;
					this.edit.TextChanged += this.OnEditChange;
					this.edit.TabIndex = 1;
					this.CommonEditorSetup(this.edit);
				}
				return this.edit;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x0015740E File Offset: 0x0015560E
		internal AccessibleObject EditAccessibleObject
		{
			get
			{
				return this.Edit.AccessibilityObject;
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x0015741C File Offset: 0x0015561C
		internal PropertyGridView.GridViewListBox DropDownListBox
		{
			get
			{
				if (this.listBox == null)
				{
					this.listBox = new PropertyGridView.GridViewListBox(this);
					this.listBox.DrawMode = DrawMode.OwnerDrawFixed;
					this.listBox.MouseUp += this.OnListMouseUp;
					this.listBox.DrawItem += this.OnListDrawItem;
					this.listBox.SelectedIndexChanged += this.OnListChange;
					this.listBox.KeyDown += this.OnListKeyDown;
					this.listBox.LostFocus += this.OnChildLostFocus;
					this.listBox.Visible = true;
					this.listBox.ItemHeight = this.RowHeight;
				}
				return this.listBox;
			}
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x001574E2 File Offset: 0x001556E2
		internal AccessibleObject DropDownListBoxAccessibleObject
		{
			get
			{
				if (this.DropDownListBox.Visible)
				{
					return this.DropDownListBox.AccessibilityObject;
				}
				return null;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x00157500 File Offset: 0x00155700
		internal bool DrawValuesRightToLeft
		{
			get
			{
				if (this.edit != null && this.edit.IsHandleCreated)
				{
					int num = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this.edit, this.edit.Handle), -20));
					return (num & 8192) != 0;
				}
				return false;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x00157552 File Offset: 0x00155752
		internal PropertyGridView.DropDownHolder DropDownControlHolder
		{
			get
			{
				return this.dropDownHolder;
			}
		}

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x0015755A File Offset: 0x0015575A
		internal bool DropDownVisible
		{
			get
			{
				return this.dropDownHolder != null && this.dropDownHolder.Visible;
			}
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060052B3 RID: 21171 RVA: 0x00157571 File Offset: 0x00155771
		public bool FocusInside
		{
			get
			{
				return base.ContainsFocus || (this.dropDownHolder != null && this.dropDownHolder.ContainsFocus);
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060052B4 RID: 21172 RVA: 0x00157594 File Offset: 0x00155794
		// (set) Token: 0x060052B5 RID: 21173 RVA: 0x00157618 File Offset: 0x00155818
		internal Color GrayTextColor
		{
			get
			{
				if (this.grayTextColorModified)
				{
					return this.grayTextColor;
				}
				if (this.ForeColor.ToArgb() == SystemColors.WindowText.ToArgb())
				{
					return SystemColors.GrayText;
				}
				int num = this.ForeColor.ToArgb();
				int num2 = num >> 24 & 255;
				if (num2 != 0)
				{
					num2 /= 2;
					num &= 16777215;
					num |= (int)((long)((long)num2 << 24) & (long)((ulong)-16777216));
				}
				else
				{
					num /= 2;
				}
				return Color.FromArgb(num);
			}
			set
			{
				this.grayTextColor = value;
				this.grayTextColorModified = true;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x00157628 File Offset: 0x00155828
		private GridErrorDlg ErrorDialog
		{
			get
			{
				if (this.errorDlg == null)
				{
					using (DpiHelper.EnterDpiAwarenessScope(DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE))
					{
						this.errorDlg = new GridErrorDlg(this.ownerGrid);
					}
				}
				return this.errorDlg;
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060052B7 RID: 21175 RVA: 0x00157678 File Offset: 0x00155878
		private bool HasEntries
		{
			get
			{
				return this.topLevelGridEntries != null && this.topLevelGridEntries.Count > 0;
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x00157692 File Offset: 0x00155892
		protected int InternalLabelWidth
		{
			get
			{
				if (this.GetFlag(128))
				{
					this.UpdateUIBasedOnFont(true);
				}
				if (this.labelWidth == -1)
				{
					this.SetConstants();
				}
				return this.labelWidth;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (set) Token: 0x060052B9 RID: 21177 RVA: 0x001576BD File Offset: 0x001558BD
		internal int LabelPaintMargin
		{
			set
			{
				this.requiredLabelPaintMargin = (short)Math.Max(Math.Max(value, (int)this.requiredLabelPaintMargin), 2);
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x001576D8 File Offset: 0x001558D8
		protected bool NeedsCommit
		{
			get
			{
				if (this.edit == null || !this.Edit.Visible)
				{
					return false;
				}
				string text = this.Edit.Text;
				return ((text != null && text.Length != 0) || (this.originalTextValue != null && this.originalTextValue.Length != 0)) && (text == null || this.originalTextValue == null || !text.Equals(this.originalTextValue));
			}
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x00157744 File Offset: 0x00155944
		public PropertyGrid OwnerGrid
		{
			get
			{
				return this.ownerGrid;
			}
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060052BC RID: 21180 RVA: 0x0015774C File Offset: 0x0015594C
		protected int RowHeight
		{
			get
			{
				if (this.cachedRowHeight == -1)
				{
					this.cachedRowHeight = this.Font.Height + 2;
				}
				return this.cachedRowHeight;
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060052BD RID: 21181 RVA: 0x00157770 File Offset: 0x00155970
		public Point ContextMenuDefaultLocation
		{
			get
			{
				Rectangle rectangle = this.GetRectangle(this.selectedRow, 1);
				Point point = base.PointToScreen(new Point(rectangle.X, rectangle.Y));
				return new Point(point.X + rectangle.Width / 2, point.Y + rectangle.Height / 2);
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060052BE RID: 21182 RVA: 0x001577CC File Offset: 0x001559CC
		private ScrollBar ScrollBar
		{
			get
			{
				if (this.scrollBar == null)
				{
					this.scrollBar = new VScrollBar();
					this.scrollBar.Scroll += this.OnScroll;
					base.Controls.Add(this.scrollBar);
				}
				return this.scrollBar;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x060052BF RID: 21183 RVA: 0x0015781A File Offset: 0x00155A1A
		// (set) Token: 0x060052C0 RID: 21184 RVA: 0x00157824 File Offset: 0x00155A24
		internal GridEntry SelectedGridEntry
		{
			get
			{
				return this.selectedGridEntry;
			}
			set
			{
				if (this.allGridEntries != null)
				{
					foreach (object obj in this.allGridEntries)
					{
						GridEntry gridEntry = (GridEntry)obj;
						if (gridEntry == value)
						{
							this.SelectGridEntry(value, true);
							return;
						}
					}
				}
				GridEntry gridEntry2 = this.FindEquivalentGridEntry(new GridEntryCollection(null, new GridEntry[]
				{
					value
				}));
				if (gridEntry2 != null)
				{
					this.SelectGridEntry(gridEntry2, true);
					return;
				}
				throw new ArgumentException(SR.GetString("PropertyGridInvalidGridEntry"));
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x001578C0 File Offset: 0x00155AC0
		// (set) Token: 0x060052C2 RID: 21186 RVA: 0x001578C8 File Offset: 0x00155AC8
		public IServiceProvider ServiceProvider
		{
			get
			{
				return this.serviceProvider;
			}
			set
			{
				if (value != this.serviceProvider)
				{
					this.serviceProvider = value;
					this.topHelpService = null;
					if (this.helpService != null && this.helpService is IDisposable)
					{
						((IDisposable)this.helpService).Dispose();
					}
					this.helpService = null;
				}
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x000A8615 File Offset: 0x000A6815
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3;
			}
		}

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x060052C4 RID: 21188 RVA: 0x00157918 File Offset: 0x00155B18
		// (set) Token: 0x060052C5 RID: 21189 RVA: 0x00157929 File Offset: 0x00155B29
		private int TipColumn
		{
			get
			{
				return (this.tipInfo & -65536) >> 16;
			}
			set
			{
				this.tipInfo &= 65535;
				this.tipInfo |= (value & 65535) << 16;
			}
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x060052C6 RID: 21190 RVA: 0x00157954 File Offset: 0x00155B54
		// (set) Token: 0x060052C7 RID: 21191 RVA: 0x00157962 File Offset: 0x00155B62
		private int TipRow
		{
			get
			{
				return this.tipInfo & 65535;
			}
			set
			{
				this.tipInfo &= -65536;
				this.tipInfo |= (value & 65535);
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x060052C8 RID: 21192 RVA: 0x0015798C File Offset: 0x00155B8C
		private GridToolTip ToolTip
		{
			get
			{
				if (this.toolTip == null)
				{
					this.toolTip = new GridToolTip(new Control[]
					{
						this,
						this.Edit
					});
					this.toolTip.ToolTip = "";
					this.toolTip.Font = this.Font;
				}
				return this.toolTip;
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x060052C9 RID: 21193 RVA: 0x001579E6 File Offset: 0x00155BE6
		internal GridEntryCollection TopLevelGridEntries
		{
			get
			{
				return this.topLevelGridEntries;
			}
		}

		// Token: 0x060052CA RID: 21194 RVA: 0x001579EE File Offset: 0x00155BEE
		internal GridEntryCollection AccessibilityGetGridEntries()
		{
			return this.GetAllGridEntries();
		}

		// Token: 0x060052CB RID: 21195 RVA: 0x001579F8 File Offset: 0x00155BF8
		internal Rectangle AccessibilityGetGridEntryBounds(GridEntry gridEntry)
		{
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			if (AccessibilityImprovements.Level4)
			{
				if (rowFromGridEntry < 0)
				{
					return Rectangle.Empty;
				}
			}
			else if (rowFromGridEntry == -1)
			{
				return new Rectangle(0, 0, 0, 0);
			}
			Rectangle rectangle = this.GetRectangle(rowFromGridEntry, 3);
			NativeMethods.POINT point = new NativeMethods.POINT(rectangle.X, rectangle.Y);
			UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
			if (AccessibilityImprovements.Level4)
			{
				bool flag;
				if (gridEntry == null)
				{
					flag = (null != null);
				}
				else
				{
					PropertyGrid propertyGrid = gridEntry.OwnerGrid;
					flag = (((propertyGrid != null) ? propertyGrid.GridViewAccessibleObject : null) != null);
				}
				if (flag)
				{
					int num = gridEntry.OwnerGrid.GridViewAccessibleObject.Bounds.Bottom - 1;
					if (point.y > num)
					{
						return Rectangle.Empty;
					}
					if (point.y + rectangle.Height > num)
					{
						rectangle.Height = num - point.y;
					}
				}
			}
			return new Rectangle(point.x, point.y, rectangle.Width, rectangle.Height);
		}

		// Token: 0x060052CC RID: 21196 RVA: 0x00157AEC File Offset: 0x00155CEC
		internal int AccessibilityGetGridEntryChildID(GridEntry gridEntry)
		{
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection == null)
			{
				return -1;
			}
			for (int i = 0; i < gridEntryCollection.Count; i++)
			{
				if (gridEntryCollection[i].Equals(gridEntry))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060052CD RID: 21197 RVA: 0x00157B28 File Offset: 0x00155D28
		internal void AccessibilitySelect(GridEntry entry)
		{
			this.SelectGridEntry(entry, true);
			this.FocusInternal();
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00157B3C File Offset: 0x00155D3C
		private void AddGridEntryEvents(GridEntryCollection ipeArray, int startIndex, int count)
		{
			if (ipeArray == null)
			{
				return;
			}
			if (count == -1)
			{
				count = ipeArray.Count - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (ipeArray[i] != null)
				{
					GridEntry entry = ipeArray.GetEntry(i);
					entry.AddOnValueClick(this.ehValueClick);
					entry.AddOnLabelClick(this.ehLabelClick);
					entry.AddOnOutlineClick(this.ehOutlineClick);
					entry.AddOnOutlineDoubleClick(this.ehOutlineClick);
					entry.AddOnValueDoubleClick(this.ehValueDblClick);
					entry.AddOnLabelDoubleClick(this.ehLabelDblClick);
					entry.AddOnRecreateChildren(this.ehRecreateChildren);
				}
			}
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x00157BCE File Offset: 0x00155DCE
		protected virtual void AdjustOrigin(Graphics g, Point newOrigin, ref Rectangle r)
		{
			g.ResetTransform();
			g.TranslateTransform((float)newOrigin.X, (float)newOrigin.Y);
			r.Offset(-newOrigin.X, -newOrigin.Y);
		}

		// Token: 0x060052D0 RID: 21200 RVA: 0x00157C02 File Offset: 0x00155E02
		private void CancelSplitterMove()
		{
			if (this.GetFlag(4))
			{
				this.SetFlag(4, false);
				base.CaptureInternal = false;
				if (this.selectedRow != -1)
				{
					this.SelectRow(this.selectedRow);
				}
			}
		}

		// Token: 0x060052D1 RID: 21201 RVA: 0x00157C31 File Offset: 0x00155E31
		internal PropertyGridView.GridPositionData CaptureGridPositionData()
		{
			return new PropertyGridView.GridPositionData(this);
		}

		// Token: 0x060052D2 RID: 21202 RVA: 0x00157C3C File Offset: 0x00155E3C
		private void ClearGridEntryEvents(GridEntryCollection ipeArray, int startIndex, int count)
		{
			if (ipeArray == null)
			{
				return;
			}
			if (count == -1)
			{
				count = ipeArray.Count - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (ipeArray[i] != null)
				{
					GridEntry entry = ipeArray.GetEntry(i);
					entry.RemoveOnValueClick(this.ehValueClick);
					entry.RemoveOnLabelClick(this.ehLabelClick);
					entry.RemoveOnOutlineClick(this.ehOutlineClick);
					entry.RemoveOnOutlineDoubleClick(this.ehOutlineClick);
					entry.RemoveOnValueDoubleClick(this.ehValueDblClick);
					entry.RemoveOnLabelDoubleClick(this.ehLabelDblClick);
					entry.RemoveOnRecreateChildren(this.ehRecreateChildren);
				}
			}
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x00157CCE File Offset: 0x00155ECE
		public void ClearProps()
		{
			if (!this.HasEntries)
			{
				return;
			}
			this.CommonEditorHide();
			this.topLevelGridEntries = null;
			this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
			this.allGridEntries = null;
			this.selectedRow = -1;
			this.tipInfo = -1;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00157D09 File Offset: 0x00155F09
		public void CloseDropDown()
		{
			this.CloseDropDownInternal(true);
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x00157D14 File Offset: 0x00155F14
		private void CloseDropDownInternal(bool resetFocus)
		{
			if (this.GetFlag(32))
			{
				return;
			}
			try
			{
				this.SetFlag(32, true);
				if (this.dropDownHolder != null && this.dropDownHolder.Visible)
				{
					if (this.dropDownHolder.Component == this.DropDownListBox && this.GetFlag(64))
					{
						this.OnListClick(null, null);
					}
					this.Edit.Filter = false;
					this.dropDownHolder.SetComponent(null, false);
					this.dropDownHolder.Visible = false;
					if (resetFocus)
					{
						if (this.DialogButton.Visible)
						{
							this.DialogButton.FocusInternal();
						}
						else if (this.DropDownButton.Visible)
						{
							this.DropDownButton.FocusInternal();
						}
						else if (this.Edit.Visible)
						{
							this.Edit.FocusInternal();
						}
						else
						{
							this.FocusInternal();
						}
						if (this.selectedRow != -1)
						{
							this.SelectRow(this.selectedRow);
						}
					}
					if (AccessibilityImprovements.Level3 && this.selectedRow != -1)
					{
						GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
						if (gridEntryFromRow != null)
						{
							gridEntryFromRow.AccessibilityObject.RaiseAutomationEvent(20005);
							gridEntryFromRow.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Expanded, UnsafeNativeMethods.ExpandCollapseState.Collapsed);
						}
					}
				}
			}
			finally
			{
				this.SetFlag(32, false);
			}
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x00157E84 File Offset: 0x00156084
		private void CommonEditorHide()
		{
			this.CommonEditorHide(false);
		}

		// Token: 0x060052D7 RID: 21207 RVA: 0x00157E90 File Offset: 0x00156090
		private void CommonEditorHide(bool always)
		{
			if (!always && !this.HasEntries)
			{
				return;
			}
			this.CloseDropDown();
			bool flag = false;
			if ((this.Edit.Focused || this.DialogButton.Focused || this.DropDownButton.Focused) && base.IsHandleCreated && base.Visible && base.Enabled)
			{
				flag = (IntPtr.Zero != UnsafeNativeMethods.SetFocus(new HandleRef(this, base.Handle)));
			}
			try
			{
				this.Edit.DontFocus = true;
				if (this.Edit.Focused && !flag)
				{
					flag = this.FocusInternal();
				}
				this.Edit.Visible = false;
				this.Edit.SelectionStart = 0;
				this.Edit.SelectionLength = 0;
				if (this.DialogButton.Focused && !flag)
				{
					flag = this.FocusInternal();
				}
				this.DialogButton.Visible = false;
				if (this.DropDownButton.Focused && !flag)
				{
					flag = this.FocusInternal();
				}
				this.DropDownButton.Visible = false;
				this.currentEditor = null;
			}
			finally
			{
				this.Edit.DontFocus = false;
			}
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x00157FC0 File Offset: 0x001561C0
		protected virtual void CommonEditorSetup(Control ctl)
		{
			ctl.Visible = false;
			base.Controls.Add(ctl);
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x00157FD8 File Offset: 0x001561D8
		protected virtual void CommonEditorUse(Control ctl, Rectangle rectTarget)
		{
			Rectangle bounds = ctl.Bounds;
			Rectangle clientRectangle = base.ClientRectangle;
			clientRectangle.Inflate(-1, -1);
			try
			{
				rectTarget = Rectangle.Intersect(clientRectangle, rectTarget);
				if (!rectTarget.IsEmpty)
				{
					if (!rectTarget.Equals(bounds))
					{
						ctl.SetBounds(rectTarget.X, rectTarget.Y, rectTarget.Width, rectTarget.Height);
					}
					ctl.Visible = true;
				}
			}
			catch
			{
				rectTarget = Rectangle.Empty;
			}
			if (rectTarget.IsEmpty)
			{
				ctl.Visible = false;
			}
			this.currentEditor = ctl;
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x00158080 File Offset: 0x00156280
		private int CountPropsFromOutline(GridEntryCollection rgipes)
		{
			if (rgipes == null)
			{
				return 0;
			}
			int num = rgipes.Count;
			for (int i = 0; i < rgipes.Count; i++)
			{
				if (((GridEntry)rgipes[i]).InternalExpanded)
				{
					num += this.CountPropsFromOutline(((GridEntry)rgipes[i]).Children);
				}
			}
			return num;
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x001580D8 File Offset: 0x001562D8
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new PropertyGridView.PropertyGridViewAccessibleObject(this, this.ownerGrid);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x001580E8 File Offset: 0x001562E8
		private Bitmap CreateResizedBitmap(string icon, int width, int height)
		{
			Bitmap result = null;
			int num = width;
			int num2 = height;
			try
			{
				if (DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					num = base.LogicalToDeviceUnits(width);
					num2 = base.LogicalToDeviceUnits(height);
				}
				else if (DpiHelper.IsScalingRequired)
				{
					num = DpiHelper.LogicalToDeviceUnitsX(width);
					num2 = DpiHelper.LogicalToDeviceUnitsY(height);
				}
				result = PropertyGridView.GetBitmapFromIcon(icon, num, num2);
			}
			catch (Exception ex)
			{
				result = new Bitmap(num, num2);
			}
			return result;
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00158150 File Offset: 0x00156350
		protected virtual void CreateUI()
		{
			this.UpdateUIBasedOnFont(false);
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x0015815C File Offset: 0x0015635C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.scrollBar != null)
				{
					this.scrollBar.Dispose();
				}
				if (this.listBox != null)
				{
					this.listBox.Dispose();
				}
				if (this.dropDownHolder != null)
				{
					this.dropDownHolder.Dispose();
				}
				this.scrollBar = null;
				this.listBox = null;
				this.dropDownHolder = null;
				this.ownerGrid = null;
				this.topLevelGridEntries = null;
				this.allGridEntries = null;
				this.serviceProvider = null;
				this.topHelpService = null;
				if (this.helpService != null && this.helpService is IDisposable)
				{
					((IDisposable)this.helpService).Dispose();
				}
				this.helpService = null;
				if (this.edit != null)
				{
					this.edit.Dispose();
					this.edit = null;
				}
				if (this.fontBold != null)
				{
					this.fontBold.Dispose();
					this.fontBold = null;
				}
				if (this.btnDropDown != null)
				{
					this.btnDropDown.Dispose();
					this.btnDropDown = null;
				}
				if (this.btnDialog != null)
				{
					this.btnDialog.Dispose();
					this.btnDialog = null;
				}
				if (this.toolTip != null)
				{
					this.toolTip.Dispose();
					this.toolTip = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x00158295 File Offset: 0x00156495
		public void DoCopyCommand()
		{
			if (this.CanCopy)
			{
				if (this.Edit.Focused)
				{
					this.Edit.Copy();
					return;
				}
				if (this.selectedGridEntry != null)
				{
					Clipboard.SetDataObject(this.selectedGridEntry.GetPropertyTextValue());
				}
			}
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x001582D0 File Offset: 0x001564D0
		public void DoCutCommand()
		{
			if (this.CanCut)
			{
				this.DoCopyCommand();
				if (this.Edit.Visible)
				{
					this.Edit.Cut();
				}
			}
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x001582F8 File Offset: 0x001564F8
		public void DoPasteCommand()
		{
			if (this.CanPaste && this.Edit.Visible)
			{
				if (this.Edit.Focused)
				{
					this.Edit.Paste();
					return;
				}
				IDataObject dataObject = Clipboard.GetDataObject();
				if (dataObject != null)
				{
					string text = (string)dataObject.GetData(typeof(string));
					if (text != null)
					{
						this.Edit.FocusInternal();
						this.Edit.Text = text;
						this.SetCommitError(0, true);
					}
				}
			}
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x00158375 File Offset: 0x00156575
		public void DoUndoCommand()
		{
			if (this.CanUndo && this.Edit.Visible)
			{
				this.Edit.SendMessage(772, 0, 0);
			}
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x001583A0 File Offset: 0x001565A0
		internal void DumpPropsToConsole(GridEntry entry, string prefix)
		{
			Type type = entry.PropertyType;
			if (entry.PropertyValue != null)
			{
				type = entry.PropertyValue.GetType();
			}
			Console.WriteLine(string.Concat(new string[]
			{
				prefix,
				entry.PropertyLabel,
				", value type=",
				(type == null) ? "(null)" : type.FullName,
				", value=",
				(entry.PropertyValue == null) ? "(null)" : entry.PropertyValue.ToString(),
				", flags=",
				entry.Flags.ToString(CultureInfo.InvariantCulture),
				", TypeConverter=",
				(entry.TypeConverter == null) ? "(null)" : entry.TypeConverter.GetType().FullName,
				", UITypeEditor=",
				(entry.UITypeEditor == null) ? "(null)" : entry.UITypeEditor.GetType().FullName
			}));
			GridEntryCollection children = entry.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					GridEntry entry2 = (GridEntry)obj;
					this.DumpPropsToConsole(entry2, prefix + "\t");
				}
			}
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x00158504 File Offset: 0x00156704
		private int GetIPELabelIndent(GridEntry gridEntry)
		{
			return gridEntry.PropertyLabelIndent + 1;
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x00158510 File Offset: 0x00156710
		private int GetIPELabelLength(Graphics g, GridEntry gridEntry)
		{
			SizeF value = PropertyGrid.MeasureTextHelper.MeasureText(this.ownerGrid, g, gridEntry.PropertyLabel, this.Font);
			Size size = Size.Ceiling(value);
			return this.ptOurLocation.X + this.GetIPELabelIndent(gridEntry) + size.Width;
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x00158558 File Offset: 0x00156758
		private bool IsIPELabelLong(Graphics g, GridEntry gridEntry)
		{
			if (gridEntry == null)
			{
				return false;
			}
			int ipelabelLength = this.GetIPELabelLength(g, gridEntry);
			return ipelabelLength > this.ptOurLocation.X + this.InternalLabelWidth;
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x00158588 File Offset: 0x00156788
		protected virtual void DrawLabel(Graphics g, int row, Rectangle rect, bool selected, bool fLongLabelRequest, ref Rectangle clipRect)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow == null || rect.IsEmpty)
			{
				return;
			}
			Point newOrigin = new Point(rect.X, rect.Y);
			Rectangle clipRect2 = Rectangle.Intersect(rect, clipRect);
			if (clipRect2.IsEmpty)
			{
				return;
			}
			this.AdjustOrigin(g, newOrigin, ref rect);
			clipRect2.Offset(-newOrigin.X, -newOrigin.Y);
			try
			{
				bool paintFullLabel = false;
				int ipelabelIndent = this.GetIPELabelIndent(gridEntryFromRow);
				if (fLongLabelRequest)
				{
					int ipelabelLength = this.GetIPELabelLength(g, gridEntryFromRow);
					paintFullLabel = this.IsIPELabelLong(g, gridEntryFromRow);
				}
				gridEntryFromRow.PaintLabel(g, rect, clipRect2, selected, paintFullLabel);
			}
			catch (Exception ex)
			{
			}
			finally
			{
				this.ResetOrigin(g);
			}
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x00158654 File Offset: 0x00156854
		protected virtual void DrawValueEntry(Graphics g, int row, ref Rectangle clipRect)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow == null)
			{
				return;
			}
			Rectangle rectangle = this.GetRectangle(row, 2);
			Point newOrigin = new Point(rectangle.X, rectangle.Y);
			Rectangle clipRect2 = Rectangle.Intersect(clipRect, rectangle);
			if (clipRect2.IsEmpty)
			{
				return;
			}
			this.AdjustOrigin(g, newOrigin, ref rectangle);
			clipRect2.Offset(-newOrigin.X, -newOrigin.Y);
			try
			{
				this.DrawValueEntry(g, rectangle, clipRect2, gridEntryFromRow, null, true);
			}
			catch
			{
			}
			finally
			{
				this.ResetOrigin(g);
			}
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x001586F8 File Offset: 0x001568F8
		private void DrawValueEntry(Graphics g, Rectangle rect, Rectangle clipRect, GridEntry gridEntry, object value, bool fetchValue)
		{
			this.DrawValue(g, rect, clipRect, gridEntry, value, false, true, fetchValue, true);
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x00158718 File Offset: 0x00156918
		private void DrawValue(Graphics g, Rectangle rect, Rectangle clipRect, GridEntry gridEntry, object value, bool drawSelected, bool checkShouldSerialize, bool fetchValue, bool paintInPlace)
		{
			GridEntry.PaintValueFlags paintValueFlags = GridEntry.PaintValueFlags.None;
			if (drawSelected)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.DrawSelected;
			}
			if (checkShouldSerialize)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.CheckShouldSerialize;
			}
			if (fetchValue)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.FetchValue;
			}
			if (paintInPlace)
			{
				paintValueFlags |= GridEntry.PaintValueFlags.PaintInPlace;
			}
			gridEntry.PaintValue(value, g, rect, clipRect, paintValueFlags);
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x00158754 File Offset: 0x00156954
		private void F4Selection(bool popupModalDialog)
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return;
			}
			if (this.errorState != 0 && this.Edit.Visible)
			{
				this.Edit.FocusInternal();
				return;
			}
			if (this.DropDownButton.Visible)
			{
				this.PopupDialog(this.selectedRow);
				return;
			}
			if (!this.DialogButton.Visible)
			{
				if (this.Edit.Visible)
				{
					this.Edit.FocusInternal();
					this.SelectEdit(false);
				}
				return;
			}
			if (popupModalDialog)
			{
				this.PopupDialog(this.selectedRow);
				return;
			}
			this.DialogButton.FocusInternal();
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x001587F8 File Offset: 0x001569F8
		public void DoubleClickRow(int row, bool toggleExpand, int type)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow == null)
			{
				return;
			}
			if (!toggleExpand || type == 2)
			{
				try
				{
					bool flag = gridEntryFromRow.DoubleClickPropertyValue();
					if (flag)
					{
						this.SelectRow(row);
						return;
					}
				}
				catch (Exception ex)
				{
					this.SetCommitError(1);
					this.ShowInvalidMessage(gridEntryFromRow.PropertyLabel, null, ex);
					return;
				}
			}
			this.SelectGridEntry(gridEntryFromRow, true);
			if (type == 1 && toggleExpand && gridEntryFromRow.Expandable)
			{
				this.SetExpand(gridEntryFromRow, !gridEntryFromRow.InternalExpanded);
				return;
			}
			if (gridEntryFromRow.IsValueEditable && gridEntryFromRow.Enumerable)
			{
				int num = this.GetCurrentValueIndex(gridEntryFromRow);
				if (num != -1)
				{
					object[] propertyValueList = gridEntryFromRow.GetPropertyValueList();
					if (propertyValueList == null || num >= propertyValueList.Length - 1)
					{
						num = 0;
					}
					else
					{
						num++;
					}
					this.CommitValue(propertyValueList[num]);
					this.SelectRow(this.selectedRow);
					this.Refresh();
					return;
				}
			}
			if (this.Edit.Visible)
			{
				this.Edit.FocusInternal();
				this.SelectEdit(false);
				return;
			}
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x00158900 File Offset: 0x00156B00
		public Font GetBaseFont()
		{
			return this.Font;
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x00158908 File Offset: 0x00156B08
		public Font GetBoldFont()
		{
			if (this.fontBold == null)
			{
				this.fontBold = new Font(this.Font, FontStyle.Bold);
			}
			return this.fontBold;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0015892A File Offset: 0x00156B2A
		internal IntPtr GetBaseHfont()
		{
			if (this.baseHfont == IntPtr.Zero)
			{
				this.baseHfont = this.GetBaseFont().ToHfont();
			}
			return this.baseHfont;
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x00158958 File Offset: 0x00156B58
		internal GridEntry GetElementFromPoint(int x, int y)
		{
			Point pt = new Point(x, y);
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			GridEntry[] array = new GridEntry[gridEntryCollection.Count];
			try
			{
				this.GetGridEntriesFromOutline(gridEntryCollection, 0, gridEntryCollection.Count - 1, array);
			}
			catch (Exception ex)
			{
			}
			foreach (GridEntry gridEntry in array)
			{
				if (gridEntry.AccessibilityObject.Bounds.Contains(pt))
				{
					return gridEntry;
				}
			}
			return null;
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x001589E4 File Offset: 0x00156BE4
		internal IntPtr GetBoldHfont()
		{
			if (this.boldHfont == IntPtr.Zero)
			{
				this.boldHfont = this.GetBoldFont().ToHfont();
			}
			return this.boldHfont;
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00158A0F File Offset: 0x00156C0F
		private bool GetFlag(short flag)
		{
			return (this.flags & flag) != 0;
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x00158A1C File Offset: 0x00156C1C
		public virtual Color GetLineColor()
		{
			return this.ownerGrid.LineColor;
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00158A2C File Offset: 0x00156C2C
		public virtual Brush GetLineBrush(Graphics g)
		{
			if (this.ownerGrid.lineBrush == null)
			{
				Color nearestColor = g.GetNearestColor(this.ownerGrid.LineColor);
				this.ownerGrid.lineBrush = new SolidBrush(nearestColor);
			}
			return this.ownerGrid.lineBrush;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x00158A74 File Offset: 0x00156C74
		public virtual Color GetSelectedItemWithFocusForeColor()
		{
			return this.ownerGrid.SelectedItemWithFocusForeColor;
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00158A81 File Offset: 0x00156C81
		public virtual Color GetSelectedItemWithFocusBackColor()
		{
			return this.ownerGrid.SelectedItemWithFocusBackColor;
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x00158A90 File Offset: 0x00156C90
		public virtual Brush GetSelectedItemWithFocusBackBrush(Graphics g)
		{
			if (this.ownerGrid.selectedItemWithFocusBackBrush == null)
			{
				Color nearestColor = g.GetNearestColor(this.ownerGrid.SelectedItemWithFocusBackColor);
				this.ownerGrid.selectedItemWithFocusBackBrush = new SolidBrush(nearestColor);
			}
			return this.ownerGrid.selectedItemWithFocusBackBrush;
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x00158AD8 File Offset: 0x00156CD8
		public virtual IntPtr GetHostHandle()
		{
			return base.Handle;
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00158AE0 File Offset: 0x00156CE0
		public virtual int GetLabelWidth()
		{
			return this.InternalLabelWidth;
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x060052FA RID: 21242 RVA: 0x00158AE8 File Offset: 0x00156CE8
		internal bool IsExplorerTreeSupported
		{
			get
			{
				return this.ownerGrid.CanShowVisualStyleGlyphs && UnsafeNativeMethods.IsVista && VisualStyleRenderer.IsSupported;
			}
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x00158B08 File Offset: 0x00156D08
		public virtual int GetOutlineIconSize()
		{
			if (this.IsExplorerTreeSupported)
			{
				return this.outlineSizeExplorerTreeStyle;
			}
			return this.outlineSize;
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x00158B1F File Offset: 0x00156D1F
		public virtual int GetGridEntryHeight()
		{
			return this.RowHeight;
		}

		// Token: 0x060052FD RID: 21245 RVA: 0x00158B28 File Offset: 0x00156D28
		internal int GetPropertyLocation(string propName, bool getXY, bool rowValue)
		{
			if (this.allGridEntries != null && this.allGridEntries.Count > 0)
			{
				int i = 0;
				while (i < this.allGridEntries.Count)
				{
					if (string.Compare(propName, this.allGridEntries.GetEntry(i).PropertyLabel, true, CultureInfo.InvariantCulture) == 0)
					{
						if (!getXY)
						{
							return i;
						}
						int rowFromGridEntry = this.GetRowFromGridEntry(this.allGridEntries.GetEntry(i));
						if (rowFromGridEntry < 0 || rowFromGridEntry >= this.visibleRows)
						{
							return -1;
						}
						Rectangle rectangle = this.GetRectangle(rowFromGridEntry, rowValue ? 2 : 1);
						return rectangle.Y << 16 | (rectangle.X & 65535);
					}
					else
					{
						i++;
					}
				}
			}
			return -1;
		}

		// Token: 0x060052FE RID: 21246 RVA: 0x00158BD6 File Offset: 0x00156DD6
		public new object GetService(Type classService)
		{
			if (classService == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			if (this.ServiceProvider != null)
			{
				return this.serviceProvider.GetService(classService);
			}
			return null;
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x00013062 File Offset: 0x00011262
		public virtual int GetSplitterWidth()
		{
			return 1;
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x00158C02 File Offset: 0x00156E02
		public virtual int GetTotalWidth()
		{
			return this.GetLabelWidth() + this.GetSplitterWidth() + this.GetValueWidth();
		}

		// Token: 0x06005301 RID: 21249 RVA: 0x00158C18 File Offset: 0x00156E18
		public virtual int GetValuePaintIndent()
		{
			return this.paintIndent;
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x00158C20 File Offset: 0x00156E20
		public virtual int GetValuePaintWidth()
		{
			return this.paintWidth;
		}

		// Token: 0x06005303 RID: 21251 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual int GetValueStringIndent()
		{
			return 0;
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x00158C28 File Offset: 0x00156E28
		public virtual int GetValueWidth()
		{
			return (int)((double)this.InternalLabelWidth * (this.labelRatio - 1.0));
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x00158C44 File Offset: 0x00156E44
		private void SetDropDownWindowPosition(Rectangle rect, bool setBounds = false)
		{
			Size size = this.dropDownHolder.Size;
			size.Width = Math.Max(rect.Width + 1, size.Width);
			Point point = base.PointToScreen(new Point(0, 0));
			Rectangle workingArea = Screen.FromControl(this.Edit).WorkingArea;
			point.X = Math.Min(workingArea.X + workingArea.Width - size.Width, Math.Max(workingArea.X, point.X + rect.X + rect.Width - size.Width));
			point.Y += rect.Y;
			if (workingArea.Y + workingArea.Height < size.Height + point.Y + this.Edit.Height)
			{
				point.Y -= size.Height;
				this.dropDownHolder.ResizeUp = true;
			}
			else
			{
				point.Y += rect.Height + 1;
				this.dropDownHolder.ResizeUp = false;
			}
			int num = 20;
			if (point.X == 0 && point.Y == 0)
			{
				num |= 2;
			}
			if (base.Width == size.Width && base.Height == size.Height)
			{
				num |= 1;
			}
			SafeNativeMethods.SetWindowPos(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), NativeMethods.NullHandleRef, point.X, point.Y, size.Width, size.Height, num);
			if (setBounds)
			{
				this.dropDownHolder.SetBounds(point.X, point.Y, size.Width, size.Height);
			}
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x00158E10 File Offset: 0x00157010
		public void DropDownControl(Control ctl)
		{
			if (this.dropDownHolder == null)
			{
				this.dropDownHolder = new PropertyGridView.DropDownHolder(this);
			}
			this.dropDownHolder.Visible = false;
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				Rectangle rectangle = this.GetRectangle(this.selectedRow, 2);
				this.dropDownHolder.SuspendAllLayout(this.dropDownHolder);
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), -8, new HandleRef(this, base.Handle));
				this.SetDropDownWindowPosition(rectangle, false);
				this.dropDownHolder.SetComponent(ctl, this.GetFlag(1024));
				this.SetDropDownWindowPosition(rectangle, false);
				this.dropDownHolder.ResumeAllLayout(this.dropDownHolder, true);
				SafeNativeMethods.ShowWindow(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), 8);
				this.SetDropDownWindowPosition(rectangle, true);
			}
			else
			{
				this.dropDownHolder.SetComponent(ctl, this.GetFlag(1024));
				Rectangle rectangle2 = this.GetRectangle(this.selectedRow, 2);
				Size size = this.dropDownHolder.Size;
				Point point = base.PointToScreen(new Point(0, 0));
				Rectangle workingArea = Screen.FromControl(this.Edit).WorkingArea;
				size.Width = Math.Max(rectangle2.Width + 1, size.Width);
				point.X = Math.Min(workingArea.X + workingArea.Width - size.Width, Math.Max(workingArea.X, point.X + rectangle2.X + rectangle2.Width - size.Width));
				point.Y += rectangle2.Y;
				if (workingArea.Y + workingArea.Height < size.Height + point.Y + this.Edit.Height)
				{
					point.Y -= size.Height;
					this.dropDownHolder.ResizeUp = true;
				}
				else
				{
					point.Y += rectangle2.Height + 1;
					this.dropDownHolder.ResizeUp = false;
				}
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), -8, new HandleRef(this, base.Handle));
				this.dropDownHolder.SetBounds(point.X, point.Y, size.Width, size.Height);
				SafeNativeMethods.ShowWindow(new HandleRef(this.dropDownHolder, this.dropDownHolder.Handle), 8);
			}
			this.Edit.Filter = true;
			this.dropDownHolder.Visible = true;
			this.dropDownHolder.FocusComponent();
			this.SelectEdit(false);
			if (AccessibilityImprovements.Level3)
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				if (gridEntryFromRow != null)
				{
					gridEntryFromRow.AccessibilityObject.RaiseAutomationEvent(20005);
					gridEntryFromRow.AccessibilityObject.RaiseAutomationPropertyChangedEvent(30070, UnsafeNativeMethods.ExpandCollapseState.Collapsed, UnsafeNativeMethods.ExpandCollapseState.Expanded);
				}
			}
			try
			{
				this.DropDownButton.IgnoreMouse = true;
				this.dropDownHolder.DoModalLoop();
			}
			finally
			{
				this.DropDownButton.IgnoreMouse = false;
			}
			if (this.selectedRow != -1)
			{
				this.FocusInternal();
				this.SelectRow(this.selectedRow);
			}
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x0015916C File Offset: 0x0015736C
		public virtual void DropDownDone()
		{
			this.CloseDropDown();
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x00159174 File Offset: 0x00157374
		public virtual void DropDownUpdate()
		{
			if (this.dropDownHolder != null && this.dropDownHolder.GetUsed())
			{
				int row = this.selectedRow;
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
				this.Edit.Text = gridEntryFromRow.GetPropertyTextValue();
			}
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x001591B6 File Offset: 0x001573B6
		public bool EnsurePendingChangesCommitted()
		{
			this.CloseDropDown();
			return this.Commit();
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x001591C4 File Offset: 0x001573C4
		private bool FilterEditWndProc(ref Message m)
		{
			if (this.dropDownHolder != null && this.dropDownHolder.Visible && m.Msg == 256 && (int)m.WParam != 9)
			{
				Control component = this.dropDownHolder.Component;
				if (component != null)
				{
					m.Result = component.SendMessage(m.Msg, m.WParam, m.LParam);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x00159234 File Offset: 0x00157434
		private bool FilterReadOnlyEditKeyPress(char keyChar)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow.Enumerable && gridEntryFromRow.IsValueEditable)
			{
				int currentValueIndex = this.GetCurrentValueIndex(gridEntryFromRow);
				object[] propertyValueList = gridEntryFromRow.GetPropertyValueList();
				string strB = new string(new char[]
				{
					keyChar
				});
				for (int i = 0; i < propertyValueList.Length; i++)
				{
					object value = propertyValueList[(i + currentValueIndex + 1) % propertyValueList.Length];
					string propertyTextValue = gridEntryFromRow.GetPropertyTextValue(value);
					if (propertyTextValue != null && propertyTextValue.Length > 0 && string.Compare(propertyTextValue.Substring(0, 1), strB, true, CultureInfo.InvariantCulture) == 0)
					{
						this.CommitValue(value);
						if (this.Edit.Focused)
						{
							this.SelectEdit(false);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x001592F4 File Offset: 0x001574F4
		public virtual bool WillFilterKeyPress(char charPressed)
		{
			if (!this.Edit.Visible)
			{
				return false;
			}
			Keys modifierKeys = Control.ModifierKeys;
			if ((modifierKeys & ~Keys.Shift) != Keys.None)
			{
				return false;
			}
			if (this.selectedGridEntry != null)
			{
				if (charPressed == '\t')
				{
					return false;
				}
				switch (charPressed)
				{
				case '*':
				case '+':
				case '-':
					return !this.selectedGridEntry.Expandable;
				}
			}
			return true;
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0015935C File Offset: 0x0015755C
		public void FilterKeyPress(char keyChar)
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return;
			}
			this.Edit.FilterKeyPress(keyChar);
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x00159388 File Offset: 0x00157588
		private GridEntry FindEquivalentGridEntry(GridEntryCollection ipeHier)
		{
			if (ipeHier == null || ipeHier.Count == 0)
			{
				return null;
			}
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection == null || gridEntryCollection.Count == 0)
			{
				return null;
			}
			GridEntry gridEntry = null;
			int num = 0;
			int num2 = gridEntryCollection.Count;
			for (int i = 0; i < ipeHier.Count; i++)
			{
				if (ipeHier[i] != null)
				{
					if (gridEntry != null)
					{
						int count = gridEntryCollection.Count;
						if (!gridEntry.InternalExpanded)
						{
							this.SetExpand(gridEntry, true);
							gridEntryCollection = this.GetAllGridEntries();
						}
						num2 = gridEntry.VisibleChildCount;
					}
					int num3 = num;
					gridEntry = null;
					while (num < gridEntryCollection.Count && num - num3 <= num2)
					{
						if (ipeHier.GetEntry(i).NonParentEquals(gridEntryCollection[num]))
						{
							gridEntry = gridEntryCollection.GetEntry(num);
							num++;
							break;
						}
						num++;
					}
					if (gridEntry == null)
					{
						break;
					}
				}
			}
			return gridEntry;
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x00159450 File Offset: 0x00157650
		protected virtual Point FindPosition(int x, int y)
		{
			if (this.RowHeight == -1)
			{
				return PropertyGridView.InvalidPosition;
			}
			Size ourSize = this.GetOurSize();
			if (x < 0 || x > ourSize.Width + this.ptOurLocation.X)
			{
				return PropertyGridView.InvalidPosition;
			}
			Point result = new Point(1, 0);
			if (x > this.InternalLabelWidth + this.ptOurLocation.X)
			{
				result.X = 2;
			}
			result.Y = (y - this.ptOurLocation.Y) / (1 + this.RowHeight);
			return result;
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x001594D7 File Offset: 0x001576D7
		public virtual void Flush()
		{
			if (this.Commit() && this.Edit.Focused)
			{
				this.FocusInternal();
			}
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x001594F5 File Offset: 0x001576F5
		private GridEntryCollection GetAllGridEntries()
		{
			return this.GetAllGridEntries(false);
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x00159500 File Offset: 0x00157700
		private GridEntryCollection GetAllGridEntries(bool fUpdateCache)
		{
			if (this.visibleRows == -1 || this.totalProps == -1 || !this.HasEntries)
			{
				return null;
			}
			if (this.allGridEntries != null && !fUpdateCache)
			{
				return this.allGridEntries;
			}
			GridEntry[] array = new GridEntry[this.totalProps];
			try
			{
				this.GetGridEntriesFromOutline(this.topLevelGridEntries, 0, 0, array);
			}
			catch (Exception ex)
			{
			}
			this.allGridEntries = new GridEntryCollection(null, array);
			this.AddGridEntryEvents(this.allGridEntries, 0, -1);
			return this.allGridEntries;
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x00159590 File Offset: 0x00157790
		private int GetCurrentValueIndex(GridEntry gridEntry)
		{
			if (!gridEntry.Enumerable)
			{
				return -1;
			}
			try
			{
				object[] propertyValueList = gridEntry.GetPropertyValueList();
				object propertyValue = gridEntry.PropertyValue;
				string strA = gridEntry.TypeConverter.ConvertToString(gridEntry, propertyValue);
				if (propertyValueList != null && propertyValueList.Length != 0)
				{
					int num = -1;
					int num2 = -1;
					for (int i = 0; i < propertyValueList.Length; i++)
					{
						object obj = propertyValueList[i];
						string strB = gridEntry.TypeConverter.ConvertToString(obj);
						if (propertyValue == obj || string.Compare(strA, strB, true, CultureInfo.InvariantCulture) == 0)
						{
							num = i;
						}
						if (propertyValue != null && obj != null && obj.Equals(propertyValue))
						{
							num2 = i;
						}
						if (num == num2 && num != -1)
						{
							return num;
						}
					}
					if (num != -1)
					{
						return num;
					}
					if (num2 != -1)
					{
						return num2;
					}
				}
			}
			catch (Exception ex)
			{
			}
			return -1;
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0015966C File Offset: 0x0015786C
		public virtual int GetDefaultOutlineIndent()
		{
			return 10;
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x00159670 File Offset: 0x00157870
		private IHelpService GetHelpService()
		{
			if (this.helpService == null && this.ServiceProvider != null)
			{
				this.topHelpService = (IHelpService)this.ServiceProvider.GetService(typeof(IHelpService));
				if (this.topHelpService != null)
				{
					IHelpService helpService = this.topHelpService.CreateLocalContext(HelpContextType.ToolWindowSelection);
					if (helpService != null)
					{
						this.helpService = helpService;
					}
				}
			}
			return this.helpService;
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x001596D4 File Offset: 0x001578D4
		public virtual int GetScrollOffset()
		{
			if (this.scrollBar == null)
			{
				return 0;
			}
			return this.ScrollBar.Value;
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x001596F8 File Offset: 0x001578F8
		private GridEntryCollection GetGridEntryHierarchy(GridEntry gridEntry)
		{
			if (gridEntry == null)
			{
				return null;
			}
			int propertyDepth = gridEntry.PropertyDepth;
			if (propertyDepth > 0)
			{
				GridEntry[] array = new GridEntry[propertyDepth + 1];
				while (gridEntry != null && propertyDepth >= 0)
				{
					array[propertyDepth] = gridEntry;
					gridEntry = gridEntry.ParentGridEntry;
					propertyDepth = gridEntry.PropertyDepth;
				}
				return new GridEntryCollection(null, array);
			}
			return new GridEntryCollection(null, new GridEntry[]
			{
				gridEntry
			});
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x00159752 File Offset: 0x00157952
		private GridEntry GetGridEntryFromRow(int row)
		{
			return this.GetGridEntryFromOffset(row + this.GetScrollOffset());
		}

		// Token: 0x06005319 RID: 21273 RVA: 0x00159764 File Offset: 0x00157964
		private GridEntry GetGridEntryFromOffset(int offset)
		{
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection != null && offset >= 0 && offset < gridEntryCollection.Count)
			{
				return gridEntryCollection.GetEntry(offset);
			}
			return null;
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x00159794 File Offset: 0x00157994
		private int GetGridEntriesFromOutline(GridEntryCollection rgipe, int cCur, int cTarget, GridEntry[] rgipeTarget)
		{
			if (rgipe == null || rgipe.Count == 0)
			{
				return cCur;
			}
			cCur--;
			for (int i = 0; i < rgipe.Count; i++)
			{
				cCur++;
				if (cCur >= cTarget + rgipeTarget.Length)
				{
					break;
				}
				GridEntry entry = rgipe.GetEntry(i);
				if (cCur >= cTarget)
				{
					rgipeTarget[cCur - cTarget] = entry;
				}
				if (entry.InternalExpanded)
				{
					GridEntryCollection children = entry.Children;
					if (children != null && children.Count > 0)
					{
						cCur = this.GetGridEntriesFromOutline(children, cCur + 1, cTarget, rgipeTarget);
					}
				}
			}
			return cCur;
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x00159810 File Offset: 0x00157A10
		private Size GetOurSize()
		{
			Size clientSize = base.ClientSize;
			if (clientSize.Width == 0)
			{
				Size size = base.Size;
				if (size.Width > 10)
				{
					clientSize.Width = size.Width;
					clientSize.Height = size.Height;
				}
			}
			if (!this.GetScrollbarHidden())
			{
				Size size2 = this.ScrollBar.Size;
				clientSize.Width -= size2.Width;
			}
			clientSize.Width -= 2;
			clientSize.Height -= 2;
			return clientSize;
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x001598A4 File Offset: 0x00157AA4
		public Rectangle GetRectangle(int row, int flRow)
		{
			Rectangle result = new Rectangle(0, 0, 0, 0);
			Size ourSize = this.GetOurSize();
			result.X = this.ptOurLocation.X;
			bool flag = (flRow & 1) != 0;
			bool flag2 = (flRow & 2) != 0;
			if (flag && flag2)
			{
				result.X = 1;
				result.Width = ourSize.Width - 1;
			}
			else if (flag)
			{
				result.X = 1;
				result.Width = this.InternalLabelWidth - 1;
			}
			else if (flag2)
			{
				result.X = this.ptOurLocation.X + this.InternalLabelWidth;
				result.Width = ourSize.Width - this.InternalLabelWidth;
			}
			result.Y = row * (this.RowHeight + 1) + 1 + this.ptOurLocation.Y;
			result.Height = this.RowHeight;
			return result;
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x0015997C File Offset: 0x00157B7C
		private int GetRowFromGridEntry(GridEntry gridEntry)
		{
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntry == null || gridEntryCollection == null)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < gridEntryCollection.Count; i++)
			{
				if (gridEntry == gridEntryCollection[i])
				{
					return i - this.GetScrollOffset();
				}
				if (num == -1 && gridEntry.Equals(gridEntryCollection[i]))
				{
					num = i - this.GetScrollOffset();
				}
			}
			if (num != -1)
			{
				return num;
			}
			return -1 - this.GetScrollOffset();
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x001599E8 File Offset: 0x00157BE8
		internal int GetRowFromGridEntryInternal(GridEntry gridEntry)
		{
			return this.GetRowFromGridEntry(gridEntry);
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x001599F1 File Offset: 0x00157BF1
		public virtual bool GetInPropertySet()
		{
			return this.GetFlag(16);
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x001599FB File Offset: 0x00157BFB
		protected virtual bool GetScrollbarHidden()
		{
			return this.scrollBar == null || !this.ScrollBar.Visible;
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x00159A18 File Offset: 0x00157C18
		public virtual string GetTestingInfo(int entry)
		{
			GridEntry gridEntry = (entry < 0) ? this.GetGridEntryFromRow(this.selectedRow) : this.GetGridEntryFromOffset(entry);
			if (gridEntry == null)
			{
				return "";
			}
			return gridEntry.GetTestingInfo();
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x00159A4E File Offset: 0x00157C4E
		public Color GetTextColor()
		{
			return this.ForeColor;
		}

		// Token: 0x06005323 RID: 21283 RVA: 0x00159A58 File Offset: 0x00157C58
		private void LayoutWindow(bool invalidate)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			Size size = new Size(clientRectangle.Width, clientRectangle.Height);
			if (this.scrollBar != null)
			{
				Rectangle bounds = this.ScrollBar.Bounds;
				bounds.X = size.Width - bounds.Width - 1;
				bounds.Y = 1;
				bounds.Height = size.Height - 2;
				this.ScrollBar.Bounds = bounds;
			}
			if (invalidate)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06005324 RID: 21284 RVA: 0x00159ADC File Offset: 0x00157CDC
		internal void InvalidateGridEntryValue(GridEntry ge)
		{
			int rowFromGridEntry = this.GetRowFromGridEntry(ge);
			if (rowFromGridEntry != -1)
			{
				this.InvalidateRows(rowFromGridEntry, rowFromGridEntry, 2);
			}
		}

		// Token: 0x06005325 RID: 21285 RVA: 0x00159AFE File Offset: 0x00157CFE
		private void InvalidateRow(int row)
		{
			this.InvalidateRows(row, row, 3);
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x00159B09 File Offset: 0x00157D09
		private void InvalidateRows(int startRow, int endRow)
		{
			this.InvalidateRows(startRow, endRow, 3);
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x00159B14 File Offset: 0x00157D14
		private void InvalidateRows(int startRow, int endRow, int type)
		{
			if (endRow == -1)
			{
				Rectangle rectangle = this.GetRectangle(startRow, type);
				rectangle.Height = base.Size.Height - rectangle.Y - 1;
				base.Invalidate(rectangle);
				return;
			}
			for (int i = startRow; i <= endRow; i++)
			{
				Rectangle rectangle = this.GetRectangle(i, type);
				base.Invalidate(rectangle);
			}
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x00159B74 File Offset: 0x00157D74
		protected override bool IsInputKey(Keys keyData)
		{
			Keys keys = keyData & Keys.KeyCode;
			if (keys <= Keys.Return)
			{
				if (keys != Keys.Tab)
				{
					if (keys != Keys.Return)
					{
						goto IL_34;
					}
					if (this.Edit.Focused)
					{
						return false;
					}
					goto IL_34;
				}
			}
			else if (keys != Keys.Escape && keys != Keys.F4)
			{
				goto IL_34;
			}
			return false;
			IL_34:
			return base.IsInputKey(keyData);
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x00159BBC File Offset: 0x00157DBC
		private bool IsMyChild(Control c)
		{
			if (c == this || c == null)
			{
				return false;
			}
			for (Control parentInternal = c.ParentInternal; parentInternal != null; parentInternal = parentInternal.ParentInternal)
			{
				if (parentInternal == this)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x00159BEC File Offset: 0x00157DEC
		private bool IsScrollValueValid(int newValue)
		{
			return newValue != this.ScrollBar.Value && newValue >= 0 && newValue <= this.ScrollBar.Maximum && newValue + (this.ScrollBar.LargeChange - 1) < this.totalProps;
		}

		// Token: 0x0600532B RID: 21291 RVA: 0x00159C28 File Offset: 0x00157E28
		internal bool IsSiblingControl(Control c1, Control c2)
		{
			Control parentInternal = c1.ParentInternal;
			for (Control parentInternal2 = c2.ParentInternal; parentInternal2 != null; parentInternal2 = parentInternal2.ParentInternal)
			{
				if (parentInternal == parentInternal2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600532C RID: 21292 RVA: 0x00159C58 File Offset: 0x00157E58
		private void MoveSplitterTo(int xpos)
		{
			int width = this.GetOurSize().Width;
			int x = this.ptOurLocation.X;
			int num = Math.Max(Math.Min(xpos, width - 10), this.GetOutlineIconSize() * 2);
			int internalLabelWidth = this.InternalLabelWidth;
			this.labelRatio = (double)width / (double)(num - x);
			this.SetConstants();
			if (this.selectedRow != -1)
			{
				this.SelectRow(this.selectedRow);
			}
			Rectangle clientRectangle = base.ClientRectangle;
			if (internalLabelWidth > this.InternalLabelWidth)
			{
				int num2 = this.InternalLabelWidth - (int)this.requiredLabelPaintMargin;
				base.Invalidate(new Rectangle(num2, 0, base.Size.Width - num2, base.Size.Height));
				return;
			}
			clientRectangle.X = internalLabelWidth - (int)this.requiredLabelPaintMargin;
			clientRectangle.Width -= clientRectangle.X;
			base.Invalidate(clientRectangle);
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x00159D44 File Offset: 0x00157F44
		private void OnBtnClick(object sender, EventArgs e)
		{
			if (this.GetFlag(256))
			{
				return;
			}
			if (sender == this.DialogButton && !this.Commit())
			{
				return;
			}
			this.SetCommitError(0);
			try
			{
				this.Commit();
				this.SetFlag(256, true);
				this.PopupDialog(this.selectedRow);
			}
			finally
			{
				this.SetFlag(256, false);
			}
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00159DB8 File Offset: 0x00157FB8
		private void OnBtnKeyDown(object sender, KeyEventArgs ke)
		{
			this.OnKeyDown(sender, ke);
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x00159DC2 File Offset: 0x00157FC2
		private void OnChildLostFocus(object sender, EventArgs e)
		{
			base.InvokeLostFocus(this, e);
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x00159DCC File Offset: 0x00157FCC
		private void OnDropDownButtonGotFocus(object sender, EventArgs e)
		{
			if (AccessibilityImprovements.Level3)
			{
				DropDownButton dropDownButton = sender as DropDownButton;
				if (dropDownButton != null)
				{
					dropDownButton.AccessibilityObject.SetFocus();
				}
			}
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x00159DF8 File Offset: 0x00157FF8
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (e != null && !this.GetInPropertySet() && !this.Commit())
			{
				this.Edit.FocusInternal();
				return;
			}
			if (this.selectedGridEntry != null && this.GetRowFromGridEntry(this.selectedGridEntry) != -1)
			{
				this.selectedGridEntry.Focus = true;
				this.SelectGridEntry(this.selectedGridEntry, false);
			}
			else
			{
				this.SelectRow(0);
			}
			if (this.selectedGridEntry != null && this.selectedGridEntry.GetValueOwner() != null)
			{
				this.UpdateHelpAttributes(null, this.selectedGridEntry);
			}
			if (this.totalProps <= 0 && AccessibilityImprovements.Level1)
			{
				int num = 2 * this.offset_2Units;
				if (base.Size.Width > num && base.Size.Height > num)
				{
					using (Graphics graphics = base.CreateGraphicsInternal())
					{
						ControlPaint.DrawFocusRectangle(graphics, new Rectangle(this.offset_2Units, this.offset_2Units, base.Size.Width - num, base.Size.Height - num));
					}
				}
			}
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x00159F1C File Offset: 0x0015811C
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			SystemEvents.UserPreferenceChanged += this.OnSysColorChange;
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x00159F36 File Offset: 0x00158136
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.OnSysColorChange;
			if (this.toolTip != null && !base.RecreatingHandle)
			{
				this.toolTip.Dispose();
				this.toolTip = null;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06005334 RID: 21300 RVA: 0x00159F74 File Offset: 0x00158174
		private void OnListChange(object sender, EventArgs e)
		{
			if (!this.DropDownListBox.InSetSelectedIndex())
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				this.Edit.Text = gridEntryFromRow.GetPropertyTextValue(this.DropDownListBox.SelectedItem);
				this.Edit.FocusInternal();
				this.SelectEdit(false);
			}
			this.SetFlag(64, true);
		}

		// Token: 0x06005335 RID: 21301 RVA: 0x00159FD3 File Offset: 0x001581D3
		private void OnListMouseUp(object sender, MouseEventArgs me)
		{
			this.OnListClick(sender, me);
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x00159FE0 File Offset: 0x001581E0
		private void OnListClick(object sender, EventArgs e)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (this.DropDownListBox.Items.Count == 0)
			{
				this.CommonEditorHide();
				this.SetCommitError(0);
				this.SelectRow(this.selectedRow);
				return;
			}
			object selectedItem = this.DropDownListBox.SelectedItem;
			this.SetFlag(64, false);
			if (selectedItem != null && !this.CommitText((string)selectedItem))
			{
				this.SetCommitError(0);
				this.SelectRow(this.selectedRow);
			}
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x0015A060 File Offset: 0x00158260
		private void OnListDrawItem(object sender, DrawItemEventArgs die)
		{
			int index = die.Index;
			if (index < 0 || this.selectedGridEntry == null)
			{
				return;
			}
			string text = (string)this.DropDownListBox.Items[die.Index];
			die.DrawBackground();
			die.DrawFocusRectangle();
			Rectangle bounds = die.Bounds;
			bounds.Y++;
			bounds.X--;
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			try
			{
				this.DrawValue(die.Graphics, bounds, bounds, gridEntryFromRow, gridEntryFromRow.ConvertTextToValue(text), (die.State & DrawItemState.Selected) > DrawItemState.None, false, false, false);
			}
			catch (FormatException ex)
			{
				this.ShowFormatExceptionMessage(gridEntryFromRow.PropertyLabel, text, ex);
				if (this.DropDownListBox.IsHandleCreated)
				{
					this.DropDownListBox.Visible = false;
				}
			}
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0015A140 File Offset: 0x00158340
		private void OnListKeyDown(object sender, KeyEventArgs ke)
		{
			if (ke.KeyCode == Keys.Return)
			{
				this.OnListClick(null, null);
				if (this.selectedGridEntry != null)
				{
					this.selectedGridEntry.OnValueReturnKey();
				}
			}
			this.OnKeyDown(sender, ke);
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x0015A170 File Offset: 0x00158370
		protected override void OnLostFocus(EventArgs e)
		{
			if (e != null)
			{
				base.OnLostFocus(e);
			}
			if (this.FocusInside)
			{
				base.OnLostFocus(e);
				return;
			}
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow != null)
			{
				gridEntryFromRow.Focus = false;
				this.CommonEditorHide();
				this.InvalidateRow(this.selectedRow);
			}
			base.OnLostFocus(e);
			if (this.totalProps <= 0 && AccessibilityImprovements.Level1)
			{
				using (Graphics graphics = base.CreateGraphicsInternal())
				{
					Rectangle rect = new Rectangle(1, 1, base.Size.Width - 2, base.Size.Height - 2);
					graphics.FillRectangle(this.backgroundBrush, rect);
				}
			}
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x0015A230 File Offset: 0x00158430
		private void OnEditChange(object sender, EventArgs e)
		{
			this.SetCommitError(0, this.Edit.Focused);
			this.ToolTip.ToolTip = "";
			this.ToolTip.Visible = false;
			if (!this.Edit.InSetText())
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				if (gridEntryFromRow != null && (gridEntryFromRow.Flags & 8) != 0)
				{
					this.Commit();
				}
			}
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0015A29C File Offset: 0x0015849C
		private void OnEditGotFocus(object sender, EventArgs e)
		{
			if (!this.Edit.Visible)
			{
				this.FocusInternal();
				return;
			}
			short num = this.errorState;
			if (num != 1)
			{
				if (num == 2)
				{
					return;
				}
				if (this.NeedsCommit)
				{
					this.SetCommitError(0, true);
				}
			}
			else if (this.Edit.Visible)
			{
				this.Edit.HookMouseDown = true;
			}
			if (this.selectedGridEntry != null && this.GetRowFromGridEntry(this.selectedGridEntry) != -1)
			{
				this.selectedGridEntry.Focus = true;
				this.InvalidateRow(this.selectedRow);
				(this.Edit.AccessibilityObject as Control.ControlAccessibleObject).NotifyClients(AccessibleEvents.Focus);
				if (AccessibilityImprovements.Level3)
				{
					this.Edit.AccessibilityObject.SetFocus();
					return;
				}
			}
			else
			{
				this.SelectRow(0);
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0015A364 File Offset: 0x00158564
		private bool ProcessEnumUpAndDown(GridEntry gridEntry, Keys keyCode, bool closeDropDown = true)
		{
			object propertyValue = gridEntry.PropertyValue;
			object[] propertyValueList = gridEntry.GetPropertyValueList();
			if (propertyValueList != null)
			{
				for (int i = 0; i < propertyValueList.Length; i++)
				{
					object obj = propertyValueList[i];
					if (propertyValue != null && obj != null && propertyValue.GetType() != obj.GetType() && gridEntry.TypeConverter.CanConvertTo(gridEntry, propertyValue.GetType()))
					{
						obj = gridEntry.TypeConverter.ConvertTo(gridEntry, CultureInfo.CurrentCulture, obj, propertyValue.GetType());
					}
					bool flag = propertyValue == obj || (propertyValue != null && propertyValue.Equals(obj));
					if (!flag && propertyValue is string && obj != null)
					{
						flag = (string.Compare((string)propertyValue, obj.ToString(), true, CultureInfo.CurrentCulture) == 0);
					}
					if (flag)
					{
						object value;
						if (keyCode == Keys.Up)
						{
							if (i == 0)
							{
								return true;
							}
							value = propertyValueList[i - 1];
						}
						else
						{
							if (i == propertyValueList.Length - 1)
							{
								return true;
							}
							value = propertyValueList[i + 1];
						}
						this.CommitValue(gridEntry, value, closeDropDown);
						this.SelectEdit(false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600533D RID: 21309 RVA: 0x0015A464 File Offset: 0x00158664
		private void OnEditKeyDown(object sender, KeyEventArgs ke)
		{
			if (!ke.Alt && (ke.KeyCode == Keys.Up || ke.KeyCode == Keys.Down))
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
				if (!gridEntryFromRow.Enumerable || !gridEntryFromRow.IsValueEditable)
				{
					return;
				}
				ke.Handled = true;
				bool flag = this.ProcessEnumUpAndDown(gridEntryFromRow, ke.KeyCode, true);
				if (flag)
				{
					return;
				}
			}
			else if ((ke.KeyCode == Keys.Left || ke.KeyCode == Keys.Right) && (ke.Modifiers & ~Keys.Shift) != Keys.None)
			{
				return;
			}
			this.OnKeyDown(sender, ke);
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0015A4F4 File Offset: 0x001586F4
		private void OnEditKeyPress(object sender, KeyPressEventArgs ke)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow == null)
			{
				return;
			}
			if (!gridEntryFromRow.IsTextEditable)
			{
				ke.Handled = this.FilterReadOnlyEditKeyPress(ke.KeyChar);
			}
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x0015A52C File Offset: 0x0015872C
		private void OnEditLostFocus(object sender, EventArgs e)
		{
			if (this.Edit.Focused || this.errorState == 2 || this.errorState == 1 || this.GetInPropertySet())
			{
				return;
			}
			if (this.dropDownHolder != null && this.dropDownHolder.Visible)
			{
				bool flag = false;
				IntPtr intPtr = UnsafeNativeMethods.GetForegroundWindow();
				while (intPtr != IntPtr.Zero)
				{
					if (intPtr == this.dropDownHolder.Handle)
					{
						flag = true;
					}
					intPtr = UnsafeNativeMethods.GetParent(new HandleRef(null, intPtr));
				}
				if (flag)
				{
					return;
				}
			}
			if (this.FocusInside)
			{
				return;
			}
			if (!this.Commit())
			{
				this.Edit.FocusInternal();
				return;
			}
			base.InvokeLostFocus(this, EventArgs.Empty);
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x0015A5E0 File Offset: 0x001587E0
		private void OnEditMouseDown(object sender, MouseEventArgs me)
		{
			if (!this.FocusInside)
			{
				this.SelectGridEntry(this.selectedGridEntry, false);
			}
			if (me.Clicks % 2 == 0)
			{
				this.DoubleClickRow(this.selectedRow, false, 2);
				this.Edit.SelectAll();
			}
			if (this.rowSelectTime == 0L)
			{
				return;
			}
			long ticks = DateTime.Now.Ticks;
			int num = (int)((ticks - this.rowSelectTime) / 10000L);
			if (num < SystemInformation.DoubleClickTime)
			{
				Point point = this.Edit.PointToScreen(new Point(me.X, me.Y));
				if (Math.Abs(point.X - this.rowSelectPos.X) < SystemInformation.DoubleClickSize.Width && Math.Abs(point.Y - this.rowSelectPos.Y) < SystemInformation.DoubleClickSize.Height)
				{
					this.DoubleClickRow(this.selectedRow, false, 2);
					this.Edit.SendMessage(514, 0, me.Y << 16 | (me.X & 65535));
					this.Edit.SelectAll();
				}
				this.rowSelectPos = Point.Empty;
				this.rowSelectTime = 0L;
			}
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x0015A719 File Offset: 0x00158919
		private bool OnF4(Control sender)
		{
			if (Control.ModifierKeys != Keys.None)
			{
				return false;
			}
			if (sender == this || sender == this.ownerGrid)
			{
				this.F4Selection(true);
			}
			else
			{
				this.UnfocusSelection();
			}
			return true;
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0015A744 File Offset: 0x00158944
		private bool OnEscape(Control sender)
		{
			if ((Control.ModifierKeys & (Keys.Control | Keys.Alt)) != Keys.None)
			{
				return false;
			}
			this.SetFlag(64, false);
			if (sender != this.Edit || !this.Edit.Focused)
			{
				if (sender != this)
				{
					this.CloseDropDown();
					this.FocusInternal();
				}
				return false;
			}
			if (this.errorState == 0)
			{
				this.Edit.Text = this.originalTextValue;
				this.FocusInternal();
				return true;
			}
			if (this.NeedsCommit)
			{
				bool flag = false;
				this.Edit.Text = this.originalTextValue;
				bool flag2 = true;
				if (this.selectedGridEntry != null)
				{
					string propertyTextValue = this.selectedGridEntry.GetPropertyTextValue();
					flag2 = (this.originalTextValue != propertyTextValue && (!string.IsNullOrEmpty(this.originalTextValue) || !string.IsNullOrEmpty(propertyTextValue)));
				}
				if (flag2)
				{
					try
					{
						flag = this.CommitText(this.originalTextValue);
						goto IL_CC;
					}
					catch
					{
						goto IL_CC;
					}
				}
				flag = true;
				IL_CC:
				if (!flag)
				{
					this.Edit.FocusInternal();
					this.SelectEdit(false);
					return true;
				}
			}
			this.SetCommitError(0);
			this.FocusInternal();
			return true;
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0015A868 File Offset: 0x00158A68
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			this.OnKeyDown(this, ke);
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0015A874 File Offset: 0x00158A74
		private void OnKeyDown(object sender, KeyEventArgs ke)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow == null)
			{
				return;
			}
			ke.Handled = true;
			bool control = ke.Control;
			bool shift = ke.Shift;
			bool flag = control && shift;
			bool alt = ke.Alt;
			Keys keyCode = ke.KeyCode;
			bool flag2 = false;
			if (keyCode == Keys.Tab && this.ProcessDialogKey(ke.KeyData))
			{
				ke.Handled = true;
				return;
			}
			if (keyCode == Keys.Down && alt && this.DropDownButton.Visible)
			{
				this.F4Selection(false);
				return;
			}
			if (keyCode == Keys.Up && alt && this.DropDownButton.Visible && this.dropDownHolder != null && this.dropDownHolder.Visible)
			{
				this.UnfocusSelection();
				return;
			}
			if (this.ToolTip.Visible)
			{
				this.ToolTip.ToolTip = "";
			}
			if (flag || sender == this || sender == this.ownerGrid)
			{
				if (keyCode <= Keys.C)
				{
					if (keyCode <= Keys.Delete)
					{
						if (keyCode != Keys.Return)
						{
							switch (keyCode)
							{
							case Keys.Prior:
							case Keys.Next:
							{
								bool flag3 = keyCode == Keys.Next;
								int num = flag3 ? (this.visibleRows - 1) : (1 - this.visibleRows);
								int row = this.selectedRow;
								if (control && !shift)
								{
									return;
								}
								if (this.selectedRow != -1)
								{
									int scrollOffset = this.GetScrollOffset();
									this.SetScrollOffset(scrollOffset + num);
									this.SetConstants();
									if (this.GetScrollOffset() != scrollOffset + num)
									{
										if (flag3)
										{
											row = this.visibleRows - 1;
										}
										else
										{
											row = 0;
										}
									}
								}
								this.SelectRow(row);
								this.Refresh();
								return;
							}
							case Keys.End:
							case Keys.Home:
							{
								GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
								int index = (keyCode == Keys.Home) ? 0 : (gridEntryCollection.Count - 1);
								this.SelectGridEntry(gridEntryCollection.GetEntry(index), true);
								return;
							}
							case Keys.Left:
								if (control)
								{
									this.MoveSplitterTo(this.InternalLabelWidth - 3);
									return;
								}
								if (gridEntryFromRow.InternalExpanded)
								{
									this.SetExpand(gridEntryFromRow, false);
									return;
								}
								this.SelectGridEntry(this.GetGridEntryFromRow(this.selectedRow - 1), true);
								return;
							case Keys.Up:
							case Keys.Down:
							{
								int row2 = (keyCode == Keys.Up) ? (this.selectedRow - 1) : (this.selectedRow + 1);
								this.SelectGridEntry(this.GetGridEntryFromRow(row2), true);
								this.SetFlag(512, false);
								return;
							}
							case Keys.Right:
								if (control)
								{
									this.MoveSplitterTo(this.InternalLabelWidth + 3);
									return;
								}
								if (!gridEntryFromRow.Expandable)
								{
									this.SelectGridEntry(this.GetGridEntryFromRow(this.selectedRow + 1), true);
									return;
								}
								if (gridEntryFromRow.InternalExpanded)
								{
									GridEntryCollection children = gridEntryFromRow.Children;
									this.SelectGridEntry(children.GetEntry(0), true);
									return;
								}
								this.SetExpand(gridEntryFromRow, true);
								return;
							case Keys.Select:
							case Keys.Print:
							case Keys.Execute:
							case Keys.Snapshot:
								goto IL_440;
							case Keys.Insert:
								if (shift && !control && !alt)
								{
									flag2 = true;
									goto IL_3FC;
								}
								break;
							case Keys.Delete:
								if (shift && !control && !alt)
								{
									flag2 = true;
									goto IL_3D6;
								}
								goto IL_440;
							default:
								goto IL_440;
							}
						}
						else
						{
							if (gridEntryFromRow.Expandable)
							{
								this.SetExpand(gridEntryFromRow, !gridEntryFromRow.InternalExpanded);
								return;
							}
							gridEntryFromRow.OnValueReturnKey();
							return;
						}
					}
					else if (keyCode != Keys.D8)
					{
						if (keyCode != Keys.A)
						{
							if (keyCode != Keys.C)
							{
								goto IL_440;
							}
						}
						else
						{
							if (control && !alt && !shift && this.Edit.Visible)
							{
								this.Edit.FocusInternal();
								this.Edit.SelectAll();
								goto IL_440;
							}
							goto IL_440;
						}
					}
					else
					{
						if (shift)
						{
							goto IL_308;
						}
						goto IL_440;
					}
					if (control && !alt && !shift)
					{
						this.DoCopyCommand();
						return;
					}
					goto IL_440;
				}
				else if (keyCode <= Keys.X)
				{
					if (keyCode == Keys.V)
					{
						goto IL_3FC;
					}
					if (keyCode != Keys.X)
					{
						goto IL_440;
					}
					goto IL_3D6;
				}
				else
				{
					switch (keyCode)
					{
					case Keys.Multiply:
						goto IL_308;
					case Keys.Add:
					case Keys.Subtract:
						break;
					case Keys.Separator:
						goto IL_440;
					default:
						if (keyCode != Keys.Oemplus && keyCode != Keys.OemMinus)
						{
							goto IL_440;
						}
						break;
					}
					if (gridEntryFromRow.Expandable)
					{
						this.SetFlag(8, true);
						bool value = keyCode == Keys.Add || keyCode == Keys.Oemplus;
						this.SetExpand(gridEntryFromRow, value);
						base.Invalidate();
						ke.Handled = true;
						return;
					}
					goto IL_440;
				}
				IL_308:
				this.SetFlag(8, true);
				this.RecursivelyExpand(gridEntryFromRow, true, true, 10);
				ke.Handled = false;
				return;
				IL_3D6:
				if (flag2 || (control && !alt && !shift))
				{
					Clipboard.SetDataObject(gridEntryFromRow.GetPropertyTextValue());
					this.CommitText("");
					return;
				}
				goto IL_440;
				IL_3FC:
				if (flag2 || (control && !alt && !shift))
				{
					this.DoPasteCommand();
				}
			}
			IL_440:
			if (gridEntryFromRow != null && ke.KeyData == (Keys)458819)
			{
				Clipboard.SetDataObject(gridEntryFromRow.GetTestingInfo());
				return;
			}
			if (AccessibilityImprovements.Level3 && this.selectedGridEntry != null && this.selectedGridEntry.Enumerable && this.dropDownHolder != null && this.dropDownHolder.Visible && (keyCode == Keys.Up || keyCode == Keys.Down))
			{
				this.ProcessEnumUpAndDown(this.selectedGridEntry, keyCode, false);
			}
			ke.Handled = false;
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x0015AD34 File Offset: 0x00158F34
		protected override void OnKeyPress(KeyPressEventArgs ke)
		{
			bool flag = false;
			bool flag2 = false;
			if ((!flag || !flag2) && this.WillFilterKeyPress(ke.KeyChar))
			{
				this.FilterKeyPress(ke.KeyChar);
			}
			this.SetFlag(8, false);
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0015AD70 File Offset: 0x00158F70
		protected override void OnMouseDown(MouseEventArgs me)
		{
			if (me.Button == MouseButtons.Left && this.SplitterInside(me.X, me.Y) && this.totalProps != 0)
			{
				if (!this.Commit())
				{
					return;
				}
				if (me.Clicks == 2)
				{
					this.MoveSplitterTo(base.Width / 2);
					return;
				}
				this.UnfocusSelection();
				this.SetFlag(4, true);
				this.tipInfo = -1;
				base.CaptureInternal = true;
				return;
			}
			else
			{
				Point left = this.FindPosition(me.X, me.Y);
				if (left == PropertyGridView.InvalidPosition)
				{
					return;
				}
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(left.Y);
				if (gridEntryFromRow != null)
				{
					Rectangle rectangle = this.GetRectangle(left.Y, 1);
					this.lastMouseDown = new Point(me.X, me.Y);
					if (me.Button == MouseButtons.Left)
					{
						gridEntryFromRow.OnMouseClick(me.X - rectangle.X, me.Y - rectangle.Y, me.Clicks, me.Button);
					}
					else
					{
						this.SelectGridEntry(gridEntryFromRow, false);
					}
					this.lastMouseDown = PropertyGridView.InvalidPosition;
					gridEntryFromRow.Focus = true;
					this.SetFlag(512, false);
				}
				return;
			}
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0015AEA4 File Offset: 0x001590A4
		protected override void OnMouseLeave(EventArgs e)
		{
			if (!this.GetFlag(4))
			{
				this.Cursor = Cursors.Default;
			}
			base.OnMouseLeave(e);
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x0015AEC4 File Offset: 0x001590C4
		protected override void OnMouseMove(MouseEventArgs me)
		{
			Point left = Point.Empty;
			bool flag = false;
			int num;
			if (me == null)
			{
				num = -1;
				left = PropertyGridView.InvalidPosition;
			}
			else
			{
				left = this.FindPosition(me.X, me.Y);
				if (left == PropertyGridView.InvalidPosition || (left.X != 1 && left.X != 2))
				{
					num = -1;
					this.ToolTip.ToolTip = "";
				}
				else
				{
					num = left.Y;
					flag = (left.X == 1);
				}
			}
			if (left == PropertyGridView.InvalidPosition || me == null)
			{
				return;
			}
			if (this.GetFlag(4))
			{
				this.MoveSplitterTo(me.X);
			}
			if ((num != this.TipRow || left.X != this.TipColumn) && !this.GetFlag(4))
			{
				GridEntry gridEntryFromRow = this.GetGridEntryFromRow(num);
				string text = "";
				this.tipInfo = -1;
				if (gridEntryFromRow != null)
				{
					Rectangle rectangle = this.GetRectangle(left.Y, left.X);
					if (flag && gridEntryFromRow.GetLabelToolTipLocation(me.X - rectangle.X, me.Y - rectangle.Y) != PropertyGridView.InvalidPoint)
					{
						text = gridEntryFromRow.LabelToolTipText;
						this.TipRow = num;
						this.TipColumn = left.X;
					}
					else if (!flag && gridEntryFromRow.ValueToolTipLocation != PropertyGridView.InvalidPoint && !this.Edit.Focused)
					{
						if (!this.NeedsCommit)
						{
							text = gridEntryFromRow.GetPropertyTextValue();
						}
						this.TipRow = num;
						this.TipColumn = left.X;
					}
				}
				IntPtr foregroundWindow = UnsafeNativeMethods.GetForegroundWindow();
				if (UnsafeNativeMethods.IsChild(new HandleRef(null, foregroundWindow), new HandleRef(null, base.Handle)))
				{
					if (this.dropDownHolder == null || this.dropDownHolder.Component == null || num == this.selectedRow)
					{
						this.ToolTip.ToolTip = text;
					}
				}
				else
				{
					this.ToolTip.ToolTip = "";
				}
			}
			if (this.totalProps != 0 && (this.SplitterInside(me.X, me.Y) || this.GetFlag(4)))
			{
				this.Cursor = Cursors.VSplit;
			}
			else
			{
				this.Cursor = Cursors.Default;
			}
			base.OnMouseMove(me);
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x0015B0F8 File Offset: 0x001592F8
		protected override void OnMouseUp(MouseEventArgs me)
		{
			this.CancelSplitterMove();
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0015B100 File Offset: 0x00159300
		protected override void OnMouseWheel(MouseEventArgs me)
		{
			this.ownerGrid.OnGridViewMouseWheel(me);
			HandledMouseEventArgs handledMouseEventArgs = me as HandledMouseEventArgs;
			if (handledMouseEventArgs != null)
			{
				if (handledMouseEventArgs.Handled)
				{
					return;
				}
				handledMouseEventArgs.Handled = true;
			}
			if ((Control.ModifierKeys & (Keys.Shift | Keys.Alt)) != Keys.None || Control.MouseButtons != MouseButtons.None)
			{
				return;
			}
			int mouseWheelScrollLines = SystemInformation.MouseWheelScrollLines;
			if (mouseWheelScrollLines == 0)
			{
				return;
			}
			if (this.selectedGridEntry != null && this.selectedGridEntry.Enumerable && this.Edit.Focused && this.selectedGridEntry.IsValueEditable)
			{
				int num = this.GetCurrentValueIndex(this.selectedGridEntry);
				if (num != -1)
				{
					int num2 = (me.Delta > 0) ? -1 : 1;
					object[] propertyValueList = this.selectedGridEntry.GetPropertyValueList();
					if (num2 > 0 && num >= propertyValueList.Length - 1)
					{
						num = 0;
					}
					else if (num2 < 0 && num == 0)
					{
						num = propertyValueList.Length - 1;
					}
					else
					{
						num += num2;
					}
					this.CommitValue(propertyValueList[num]);
					this.SelectGridEntry(this.selectedGridEntry, true);
					this.Edit.FocusInternal();
					return;
				}
			}
			int num3 = this.GetScrollOffset();
			this.cumulativeVerticalWheelDelta += me.Delta;
			float num4 = (float)this.cumulativeVerticalWheelDelta / 120f;
			int num5 = (int)num4;
			if (mouseWheelScrollLines == -1)
			{
				if (num5 != 0)
				{
					int num6 = num3;
					int num7 = num5 * this.scrollBar.LargeChange;
					int num8 = Math.Max(0, num3 - num7);
					num8 = Math.Min(num8, this.totalProps - this.visibleRows + 1);
					num3 -= num5 * this.scrollBar.LargeChange;
					if (Math.Abs(num3 - num6) >= Math.Abs(num5 * this.scrollBar.LargeChange))
					{
						this.cumulativeVerticalWheelDelta -= num5 * 120;
					}
					else
					{
						this.cumulativeVerticalWheelDelta = 0;
					}
					if (!this.ScrollRows(num8))
					{
						this.cumulativeVerticalWheelDelta = 0;
						return;
					}
				}
			}
			else
			{
				int num9 = (int)((float)mouseWheelScrollLines * num4);
				if (num9 != 0)
				{
					if (this.ToolTip.Visible)
					{
						this.ToolTip.ToolTip = "";
					}
					int num10 = Math.Max(0, num3 - num9);
					num10 = Math.Min(num10, this.totalProps - this.visibleRows + 1);
					if (num9 > 0)
					{
						if (this.scrollBar.Value <= this.scrollBar.Minimum)
						{
							this.cumulativeVerticalWheelDelta = 0;
						}
						else
						{
							this.cumulativeVerticalWheelDelta -= (int)((float)num9 * (120f / (float)mouseWheelScrollLines));
						}
					}
					else if (this.scrollBar.Value > this.scrollBar.Maximum - this.visibleRows + 1)
					{
						this.cumulativeVerticalWheelDelta = 0;
					}
					else
					{
						this.cumulativeVerticalWheelDelta -= (int)((float)num9 * (120f / (float)mouseWheelScrollLines));
					}
					if (!this.ScrollRows(num10))
					{
						this.cumulativeVerticalWheelDelta = 0;
						return;
					}
				}
				else
				{
					this.cumulativeVerticalWheelDelta = 0;
				}
			}
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x0015916C File Offset: 0x0015736C
		protected override void OnMove(EventArgs e)
		{
			this.CloseDropDown();
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x000072B6 File Offset: 0x000054B6
		protected override void OnPaintBackground(PaintEventArgs pe)
		{
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x0015B3D0 File Offset: 0x001595D0
		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics graphics = pe.Graphics;
			int num = 0;
			int num2 = 0;
			int num3 = this.visibleRows - 1;
			Rectangle clipRectangle = pe.ClipRectangle;
			clipRectangle.Inflate(0, 2);
			try
			{
				Size size = base.Size;
				Point left = this.FindPosition(clipRectangle.X, clipRectangle.Y);
				Point left2 = this.FindPosition(clipRectangle.X, clipRectangle.Y + clipRectangle.Height);
				if (left != PropertyGridView.InvalidPosition)
				{
					num2 = Math.Max(0, left.Y);
				}
				if (left2 != PropertyGridView.InvalidPosition)
				{
					num3 = left2.Y;
				}
				int num4 = Math.Min(this.totalProps - this.GetScrollOffset(), 1 + this.visibleRows);
				this.SetFlag(1, false);
				Size ourSize = this.GetOurSize();
				Point point = this.ptOurLocation;
				if (this.GetGridEntryFromRow(num4 - 1) == null)
				{
					num4--;
				}
				if (this.totalProps > 0)
				{
					num4 = Math.Min(num4, num3 + 1);
					Pen pen = new Pen(this.ownerGrid.LineColor, (float)this.GetSplitterWidth());
					pen.DashStyle = DashStyle.Solid;
					graphics.DrawLine(pen, this.labelWidth, point.Y, this.labelWidth, num4 * (this.RowHeight + 1) + point.Y);
					pen.Dispose();
					Pen pen2 = new Pen(graphics.GetNearestColor(this.ownerGrid.LineColor));
					int x = point.X + ourSize.Width;
					int x2 = point.X;
					int num5 = this.GetTotalWidth() + 1;
					int num6;
					for (int i = num2; i < num4; i++)
					{
						try
						{
							num6 = i * (this.RowHeight + 1) + point.Y;
							graphics.DrawLine(pen2, x2, num6, x, num6);
							this.DrawValueEntry(graphics, i, ref clipRectangle);
							Rectangle rectangle = this.GetRectangle(i, 1);
							num = rectangle.Y + rectangle.Height;
							this.DrawLabel(graphics, i, rectangle, i == this.selectedRow, false, ref clipRectangle);
							if (i == this.selectedRow)
							{
								this.Edit.Invalidate();
							}
						}
						catch
						{
						}
					}
					num6 = num4 * (this.RowHeight + 1) + point.Y;
					graphics.DrawLine(pen2, x2, num6, x, num6);
					pen2.Dispose();
				}
				if (num < base.Size.Height)
				{
					num++;
					Rectangle rect = new Rectangle(1, num, base.Size.Width - 2, base.Size.Height - num - 1);
					graphics.FillRectangle(this.backgroundBrush, rect);
				}
				using (Pen pen3 = new Pen(this.ownerGrid.ViewBorderColor, 1f))
				{
					graphics.DrawRectangle(pen3, 0, 0, size.Width - 1, size.Height - 1);
				}
				this.fontBold = null;
			}
			catch
			{
			}
			finally
			{
				this.ClearCachedFontInfo();
			}
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x0015B730 File Offset: 0x00159930
		private void OnGridEntryLabelDoubleClick(object s, EventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			if (gridEntry != this.lastClickedEntry)
			{
				return;
			}
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			this.DoubleClickRow(rowFromGridEntry, gridEntry.Expandable, 1);
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0015B764 File Offset: 0x00159964
		private void OnGridEntryValueDoubleClick(object s, EventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			if (gridEntry != this.lastClickedEntry)
			{
				return;
			}
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			this.DoubleClickRow(rowFromGridEntry, gridEntry.Expandable, 2);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0015B798 File Offset: 0x00159998
		private void OnGridEntryLabelClick(object s, EventArgs e)
		{
			this.lastClickedEntry = (GridEntry)s;
			this.SelectGridEntry(this.lastClickedEntry, true);
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0015B7B4 File Offset: 0x001599B4
		private void OnGridEntryOutlineClick(object s, EventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			Cursor cursor = this.Cursor;
			if (!this.ShouldSerializeCursor())
			{
				cursor = null;
			}
			this.Cursor = Cursors.WaitCursor;
			try
			{
				this.SetExpand(gridEntry, !gridEntry.InternalExpanded);
				this.SelectGridEntry(gridEntry, false);
			}
			finally
			{
				this.Cursor = cursor;
			}
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0015B818 File Offset: 0x00159A18
		private void OnGridEntryValueClick(object s, EventArgs e)
		{
			this.lastClickedEntry = (GridEntry)s;
			bool flag = s != this.selectedGridEntry;
			this.SelectGridEntry(this.lastClickedEntry, true);
			this.Edit.FocusInternal();
			if (this.lastMouseDown != PropertyGridView.InvalidPosition)
			{
				this.rowSelectTime = 0L;
				Point p = base.PointToScreen(this.lastMouseDown);
				p = this.Edit.PointToClientInternal(p);
				this.Edit.SendMessage(513, 0, p.Y << 16 | (p.X & 65535));
				this.Edit.SendMessage(514, 0, p.Y << 16 | (p.X & 65535));
			}
			if (flag)
			{
				this.rowSelectTime = DateTime.Now.Ticks;
				this.rowSelectPos = base.PointToScreen(this.lastMouseDown);
				return;
			}
			this.rowSelectTime = 0L;
			this.rowSelectPos = Point.Empty;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0015B91C File Offset: 0x00159B1C
		private void ClearCachedFontInfo()
		{
			if (this.baseHfont != IntPtr.Zero)
			{
				SafeNativeMethods.ExternalDeleteObject(new HandleRef(this, this.baseHfont));
				this.baseHfont = IntPtr.Zero;
			}
			if (this.boldHfont != IntPtr.Zero)
			{
				SafeNativeMethods.ExternalDeleteObject(new HandleRef(this, this.boldHfont));
				this.boldHfont = IntPtr.Zero;
			}
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x0015B988 File Offset: 0x00159B88
		protected override void OnFontChanged(EventArgs e)
		{
			this.ClearCachedFontInfo();
			this.cachedRowHeight = -1;
			if (base.Disposing || this.ParentInternal == null || this.ParentInternal.Disposing)
			{
				return;
			}
			this.fontBold = null;
			this.ToolTip.Font = this.Font;
			this.SetFlag(128, true);
			this.UpdateUIBasedOnFont(true);
			base.OnFontChanged(e);
			if (this.selectedGridEntry != null)
			{
				this.SelectGridEntry(this.selectedGridEntry, true);
			}
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0015BA08 File Offset: 0x00159C08
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Disposing || this.ParentInternal == null || this.ParentInternal.Disposing)
			{
				return;
			}
			if (base.Visible && this.ParentInternal != null)
			{
				this.SetConstants();
				if (this.selectedGridEntry != null)
				{
					this.SelectGridEntry(this.selectedGridEntry, true);
				}
				if (this.toolTip != null)
				{
					this.ToolTip.Font = this.Font;
				}
			}
			base.OnVisibleChanged(e);
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0015BA80 File Offset: 0x00159C80
		protected virtual void OnRecreateChildren(object s, GridEntryRecreateChildrenEventArgs e)
		{
			GridEntry gridEntry = (GridEntry)s;
			if (gridEntry.Expanded)
			{
				GridEntry[] array = new GridEntry[this.allGridEntries.Count];
				this.allGridEntries.CopyTo(array, 0);
				int num = -1;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == gridEntry)
					{
						num = i;
						break;
					}
				}
				this.ClearGridEntryEvents(this.allGridEntries, num + 1, e.OldChildCount);
				if (e.OldChildCount != e.NewChildCount)
				{
					int num2 = array.Length + (e.NewChildCount - e.OldChildCount);
					GridEntry[] array2 = new GridEntry[num2];
					Array.Copy(array, 0, array2, 0, num + 1);
					Array.Copy(array, num + e.OldChildCount + 1, array2, num + e.NewChildCount + 1, array.Length - (num + e.OldChildCount + 1));
					array = array2;
				}
				GridEntryCollection children = gridEntry.Children;
				int count = children.Count;
				for (int j = 0; j < count; j++)
				{
					array[num + j + 1] = children.GetEntry(j);
				}
				this.allGridEntries.Clear();
				this.allGridEntries.AddRange(array);
				this.AddGridEntryEvents(this.allGridEntries, num + 1, count);
			}
			if (e.OldChildCount != e.NewChildCount)
			{
				this.totalProps = this.CountPropsFromOutline(this.topLevelGridEntries);
				this.SetConstants();
			}
			base.Invalidate();
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0015BBDC File Offset: 0x00159DDC
		protected override void OnResize(EventArgs e)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = (this.lastClientRect == Rectangle.Empty) ? 0 : (clientRectangle.Height - this.lastClientRect.Height);
			bool flag = this.selectedRow + 1 == this.visibleRows;
			bool visible = this.ScrollBar.Visible;
			if (!this.lastClientRect.IsEmpty && clientRectangle.Width > this.lastClientRect.Width)
			{
				Rectangle rc = new Rectangle(this.lastClientRect.Width - 1, 0, clientRectangle.Width - this.lastClientRect.Width + 1, this.lastClientRect.Height);
				base.Invalidate(rc);
			}
			if (!this.lastClientRect.IsEmpty && num > 0)
			{
				Rectangle rc2 = new Rectangle(0, this.lastClientRect.Height - 1, this.lastClientRect.Width, clientRectangle.Height - this.lastClientRect.Height + 1);
				base.Invalidate(rc2);
			}
			int scrollOffset = this.GetScrollOffset();
			this.SetScrollOffset(0);
			this.SetConstants();
			this.SetScrollOffset(scrollOffset);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.SetFlag(128, true);
				this.UpdateUIBasedOnFont(true);
				base.OnFontChanged(e);
			}
			this.CommonEditorHide();
			this.LayoutWindow(false);
			bool fPageIn = this.selectedGridEntry != null && this.selectedRow >= 0 && this.selectedRow <= this.visibleRows;
			this.SelectGridEntry(this.selectedGridEntry, fPageIn);
			this.lastClientRect = clientRectangle;
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0015BD6C File Offset: 0x00159F6C
		private void OnScroll(object sender, ScrollEventArgs se)
		{
			if (!this.Commit() || !this.IsScrollValueValid(se.NewValue))
			{
				se.NewValue = this.ScrollBar.Value;
				return;
			}
			int num = -1;
			GridEntry gridEntry = this.selectedGridEntry;
			if (this.selectedGridEntry != null)
			{
				num = this.GetRowFromGridEntry(gridEntry);
			}
			this.ScrollBar.Value = se.NewValue;
			if (gridEntry != null)
			{
				this.selectedRow = -1;
				this.SelectGridEntry(gridEntry, this.ScrollBar.Value == this.totalProps);
				int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
				if (num != rowFromGridEntry)
				{
					base.Invalidate();
					return;
				}
			}
			else
			{
				base.Invalidate();
			}
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0015BE0C File Offset: 0x0015A00C
		private void OnSysColorChange(object sender, UserPreferenceChangedEventArgs e)
		{
			if (e.Category == UserPreferenceCategory.Color || e.Category == UserPreferenceCategory.Accessibility)
			{
				this.SetFlag(128, true);
			}
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0015BE2C File Offset: 0x0015A02C
		public virtual void PopupDialog(int row)
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (gridEntryFromRow != null)
			{
				if (this.dropDownHolder != null && this.dropDownHolder.GetUsed())
				{
					this.CloseDropDown();
					return;
				}
				bool needsDropDownButton = gridEntryFromRow.NeedsDropDownButton;
				bool enumerable = gridEntryFromRow.Enumerable;
				bool needsCustomEditorButton = gridEntryFromRow.NeedsCustomEditorButton;
				if (enumerable && !needsDropDownButton)
				{
					this.DropDownListBox.Items.Clear();
					object propertyValue = gridEntryFromRow.PropertyValue;
					object[] propertyValueList = gridEntryFromRow.GetPropertyValueList();
					int num = 0;
					IntPtr dc = UnsafeNativeMethods.GetDC(new HandleRef(this.DropDownListBox, this.DropDownListBox.Handle));
					IntPtr handle = this.Font.ToHfont();
					System.Internal.HandleCollector.Add(handle, NativeMethods.CommonHandles.GDI);
					NativeMethods.TEXTMETRIC textmetric = default(NativeMethods.TEXTMETRIC);
					int num2 = -1;
					try
					{
						handle = SafeNativeMethods.SelectObject(new HandleRef(this.DropDownListBox, dc), new HandleRef(this.Font, handle));
						num2 = this.GetCurrentValueIndex(gridEntryFromRow);
						if (propertyValueList != null && propertyValueList.Length != 0)
						{
							IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
							for (int i = 0; i < propertyValueList.Length; i++)
							{
								string propertyTextValue = gridEntryFromRow.GetPropertyTextValue(propertyValueList[i]);
								this.DropDownListBox.Items.Add(propertyTextValue);
								IntUnsafeNativeMethods.GetTextExtentPoint32(new HandleRef(this.DropDownListBox, dc), propertyTextValue, size);
								num = Math.Max(size.cx, num);
							}
						}
						SafeNativeMethods.GetTextMetrics(new HandleRef(this.DropDownListBox, dc), ref textmetric);
						num += 2 + textmetric.tmMaxCharWidth + SystemInformation.VerticalScrollBarWidth;
						handle = SafeNativeMethods.SelectObject(new HandleRef(this.DropDownListBox, dc), new HandleRef(this.Font, handle));
					}
					finally
					{
						SafeNativeMethods.DeleteObject(new HandleRef(this.Font, handle));
						UnsafeNativeMethods.ReleaseDC(new HandleRef(this.DropDownListBox, this.DropDownListBox.Handle), new HandleRef(this.DropDownListBox, dc));
					}
					if (num2 != -1)
					{
						this.DropDownListBox.SelectedIndex = num2;
					}
					this.SetFlag(64, false);
					this.DropDownListBox.Height = Math.Max(textmetric.tmHeight + 2, Math.Min(this.maxListBoxHeight, this.DropDownListBox.PreferredHeight));
					this.DropDownListBox.Width = Math.Max(num, this.GetRectangle(row, 2).Width);
					try
					{
						bool value = this.DropDownListBox.Items.Count > this.DropDownListBox.Height / this.DropDownListBox.ItemHeight;
						this.SetFlag(1024, value);
						this.DropDownControl(this.DropDownListBox);
					}
					finally
					{
						this.SetFlag(1024, false);
					}
					this.Refresh();
					return;
				}
				if (needsCustomEditorButton || needsDropDownButton)
				{
					try
					{
						this.SetFlag(16, true);
						this.Edit.DisableMouseHook = true;
						try
						{
							this.SetFlag(1024, gridEntryFromRow.UITypeEditor.IsDropDownResizable);
							gridEntryFromRow.EditPropertyValue(this);
						}
						finally
						{
							this.SetFlag(1024, false);
						}
					}
					finally
					{
						this.SetFlag(16, false);
						this.Edit.DisableMouseHook = false;
					}
					this.Refresh();
					if (this.FocusInside)
					{
						this.SelectGridEntry(gridEntryFromRow, false);
					}
				}
			}
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0015C174 File Offset: 0x0015A374
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.HasEntries)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Return)
				{
					if (keys != Keys.Tab)
					{
						if (keys == Keys.Return)
						{
							if (this.DialogButton.Focused || this.DropDownButton.Focused)
							{
								this.OnBtnClick(this.DialogButton.Focused ? this.DialogButton : this.DropDownButton, new EventArgs());
								return true;
							}
							if (this.selectedGridEntry != null && this.selectedGridEntry.Expandable)
							{
								this.SetExpand(this.selectedGridEntry, !this.selectedGridEntry.InternalExpanded);
								return true;
							}
						}
					}
					else if ((keyData & Keys.Control) == Keys.None && (keyData & Keys.Alt) == Keys.None)
					{
						bool flag = (keyData & Keys.Shift) == Keys.None;
						Control control = Control.FromHandleInternal(UnsafeNativeMethods.GetFocus());
						if (control == null || !this.IsMyChild(control))
						{
							if (flag)
							{
								this.TabSelection();
								control = Control.FromHandleInternal(UnsafeNativeMethods.GetFocus());
								return this.IsMyChild(control) || base.ProcessDialogKey(keyData);
							}
						}
						else if (this.Edit.Focused)
						{
							if (!flag)
							{
								this.SelectGridEntry(this.GetGridEntryFromRow(this.selectedRow), false);
								return true;
							}
							if (this.DropDownButton.Visible)
							{
								this.DropDownButton.FocusInternal();
								return true;
							}
							if (this.DialogButton.Visible)
							{
								this.DialogButton.FocusInternal();
								return true;
							}
						}
						else if ((this.DialogButton.Focused || this.DropDownButton.Focused) && !flag && this.Edit.Visible)
						{
							this.Edit.FocusInternal();
							return true;
						}
					}
				}
				else
				{
					if (keys - Keys.Left <= 3)
					{
						return false;
					}
					if (keys == Keys.F4 && this.FocusInside)
					{
						return this.OnF4(this);
					}
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0015C350 File Offset: 0x0015A550
		protected virtual void RecalculateProps()
		{
			int num = this.CountPropsFromOutline(this.topLevelGridEntries);
			if (this.totalProps != num)
			{
				this.totalProps = num;
				this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
				this.allGridEntries = null;
			}
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0015C390 File Offset: 0x0015A590
		internal void RecursivelyExpand(GridEntry gridEntry, bool fInit, bool expand, int maxExpands)
		{
			if (gridEntry == null || (expand && --maxExpands < 0))
			{
				return;
			}
			this.SetExpand(gridEntry, expand);
			GridEntryCollection children = gridEntry.Children;
			if (children != null)
			{
				for (int i = 0; i < children.Count; i++)
				{
					this.RecursivelyExpand(children.GetEntry(i), false, expand, maxExpands);
				}
			}
			if (fInit)
			{
				GridEntry gridEntry2 = this.selectedGridEntry;
				this.Refresh();
				this.SelectGridEntry(gridEntry2, false);
				base.Invalidate();
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0015C400 File Offset: 0x0015A600
		public override void Refresh()
		{
			this.Refresh(false, -1, -1);
			if (this.topLevelGridEntries != null && DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				int outlineIconSize = this.GetOutlineIconSize();
				foreach (object obj in this.topLevelGridEntries)
				{
					GridEntry gridEntry = (GridEntry)obj;
					if (gridEntry.OutlineRect.Height != outlineIconSize || gridEntry.OutlineRect.Width != outlineIconSize)
					{
						this.ResetOutline(gridEntry);
					}
				}
			}
			base.Invalidate();
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0015C4A4 File Offset: 0x0015A6A4
		public void Refresh(bool fullRefresh)
		{
			this.Refresh(fullRefresh, -1, -1);
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0015C4B0 File Offset: 0x0015A6B0
		private void Refresh(bool fullRefresh, int rowStart, int rowEnd)
		{
			this.SetFlag(1, true);
			GridEntry gridEntry = null;
			if (base.IsDisposed)
			{
				return;
			}
			bool fPageIn = true;
			if (rowStart == -1)
			{
				rowStart = 0;
			}
			if (fullRefresh || this.ownerGrid.HavePropEntriesChanged())
			{
				if (this.HasEntries && !this.GetInPropertySet() && !this.Commit())
				{
					this.OnEscape(this);
				}
				int num = this.totalProps;
				object obj = (this.topLevelGridEntries == null || this.topLevelGridEntries.Count == 0) ? null : ((GridEntry)this.topLevelGridEntries[0]).GetValueOwner();
				if (fullRefresh)
				{
					this.ownerGrid.RefreshProperties(true);
				}
				if (num > 0 && !this.GetFlag(512))
				{
					this.positionData = this.CaptureGridPositionData();
					this.CommonEditorHide(true);
				}
				this.UpdateHelpAttributes(this.selectedGridEntry, null);
				this.selectedGridEntry = null;
				this.SetFlag(2, true);
				this.topLevelGridEntries = this.ownerGrid.GetPropEntries();
				this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
				this.allGridEntries = null;
				this.RecalculateProps();
				int num2 = this.totalProps;
				if (num2 > 0)
				{
					if (num2 < num)
					{
						this.SetScrollbarLength();
						this.SetScrollOffset(0);
					}
					this.SetConstants();
					if (this.positionData != null)
					{
						gridEntry = this.positionData.Restore(this);
						object obj2 = (this.topLevelGridEntries == null || this.topLevelGridEntries.Count == 0) ? null : ((GridEntry)this.topLevelGridEntries[0]).GetValueOwner();
						fPageIn = (gridEntry == null || num != num2 || obj2 != obj);
					}
					if (gridEntry == null)
					{
						gridEntry = this.ownerGrid.GetDefaultGridEntry();
						this.SetFlag(512, gridEntry == null && this.totalProps > 0);
					}
					this.InvalidateRows(rowStart, rowEnd);
					if (gridEntry == null)
					{
						this.selectedRow = 0;
						this.selectedGridEntry = this.GetGridEntryFromRow(this.selectedRow);
					}
				}
				else
				{
					if (num == 0)
					{
						return;
					}
					this.SetConstants();
				}
				this.positionData = null;
				this.lastClickedEntry = null;
			}
			if (!this.HasEntries)
			{
				this.CommonEditorHide(this.selectedRow != -1);
				this.ownerGrid.SetStatusBox(null, null);
				this.SetScrollOffset(0);
				this.selectedRow = -1;
				base.Invalidate();
				return;
			}
			this.ownerGrid.ClearValueCaches();
			this.InvalidateRows(rowStart, rowEnd);
			if (gridEntry != null)
			{
				this.SelectGridEntry(gridEntry, fPageIn);
			}
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0015C700 File Offset: 0x0015A900
		public virtual void Reset()
		{
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			if (gridEntryFromRow == null)
			{
				return;
			}
			gridEntryFromRow.ResetPropertyValue();
			this.SelectRow(this.selectedRow);
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0015C730 File Offset: 0x0015A930
		protected virtual void ResetOrigin(Graphics g)
		{
			g.ResetTransform();
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0015C738 File Offset: 0x0015A938
		internal void RestoreHierarchyState(ArrayList expandedItems)
		{
			if (expandedItems == null)
			{
				return;
			}
			foreach (object obj in expandedItems)
			{
				GridEntryCollection ipeHier = (GridEntryCollection)obj;
				this.FindEquivalentGridEntry(ipeHier);
			}
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0015C794 File Offset: 0x0015A994
		public virtual DialogResult RunDialog(Form dialog)
		{
			return this.ShowDialog(dialog);
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0015C79D File Offset: 0x0015A99D
		internal ArrayList SaveHierarchyState(GridEntryCollection entries)
		{
			return this.SaveHierarchyState(entries, null);
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0015C7A8 File Offset: 0x0015A9A8
		private ArrayList SaveHierarchyState(GridEntryCollection entries, ArrayList expandedItems)
		{
			if (entries == null)
			{
				return new ArrayList();
			}
			if (expandedItems == null)
			{
				expandedItems = new ArrayList();
			}
			for (int i = 0; i < entries.Count; i++)
			{
				if (((GridEntry)entries[i]).InternalExpanded)
				{
					GridEntry entry = entries.GetEntry(i);
					expandedItems.Add(this.GetGridEntryHierarchy(entry.Children.GetEntry(0)));
					this.SaveHierarchyState(entry.Children, expandedItems);
				}
			}
			return expandedItems;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0015C81C File Offset: 0x0015AA1C
		private bool ScrollRows(int newOffset)
		{
			GridEntry gridEntry = this.selectedGridEntry;
			if (!this.IsScrollValueValid(newOffset) || !this.Commit())
			{
				return false;
			}
			bool visible = this.Edit.Visible;
			bool visible2 = this.DropDownButton.Visible;
			bool visible3 = this.DialogButton.Visible;
			this.Edit.Visible = false;
			this.DialogButton.Visible = false;
			this.DropDownButton.Visible = false;
			this.SetScrollOffset(newOffset);
			if (gridEntry != null)
			{
				int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
				if (rowFromGridEntry >= 0 && rowFromGridEntry < this.visibleRows - 1)
				{
					this.Edit.Visible = visible;
					this.DialogButton.Visible = visible3;
					this.DropDownButton.Visible = visible2;
					this.SelectGridEntry(gridEntry, true);
				}
				else
				{
					this.CommonEditorHide();
				}
			}
			else
			{
				this.CommonEditorHide();
			}
			base.Invalidate();
			return true;
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0015C8F2 File Offset: 0x0015AAF2
		private void SelectEdit(bool caretAtEnd)
		{
			if (this.edit != null)
			{
				this.Edit.SelectAll();
			}
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0015C908 File Offset: 0x0015AB08
		internal void SelectGridEntry(GridEntry gridEntry, bool fPageIn)
		{
			if (gridEntry == null)
			{
				return;
			}
			int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
			if (rowFromGridEntry + this.GetScrollOffset() < 0)
			{
				return;
			}
			int num = (int)Math.Ceiling((double)this.GetOurSize().Height / (double)(1 + this.RowHeight));
			if (!fPageIn || (rowFromGridEntry >= 0 && rowFromGridEntry < num - 1))
			{
				this.SelectRow(rowFromGridEntry);
				return;
			}
			this.selectedRow = -1;
			int scrollOffset = this.GetScrollOffset();
			if (rowFromGridEntry < 0)
			{
				this.SetScrollOffset(rowFromGridEntry + scrollOffset);
				base.Invalidate();
				this.SelectRow(0);
				return;
			}
			int num2 = rowFromGridEntry + scrollOffset - (num - 2);
			if (num2 >= this.ScrollBar.Minimum && num2 < this.ScrollBar.Maximum)
			{
				this.SetScrollOffset(num2);
			}
			base.Invalidate();
			this.SelectGridEntry(gridEntry, false);
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x0015C9C8 File Offset: 0x0015ABC8
		private void SelectRow(int row)
		{
			if (!this.GetFlag(2))
			{
				if (this.FocusInside)
				{
					if (this.errorState != 0 || (row != this.selectedRow && !this.Commit()))
					{
						return;
					}
				}
				else
				{
					this.FocusInternal();
				}
			}
			GridEntry gridEntryFromRow = this.GetGridEntryFromRow(row);
			if (row != this.selectedRow)
			{
				this.UpdateResetCommand(gridEntryFromRow);
			}
			if (this.GetFlag(2) && this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				this.CommonEditorHide();
			}
			this.UpdateHelpAttributes(this.selectedGridEntry, gridEntryFromRow);
			if (this.selectedGridEntry != null)
			{
				this.selectedGridEntry.Focus = false;
			}
			if (row < 0 || row >= this.visibleRows)
			{
				this.CommonEditorHide();
				this.selectedRow = row;
				this.selectedGridEntry = gridEntryFromRow;
				this.Refresh();
				return;
			}
			if (gridEntryFromRow == null)
			{
				return;
			}
			bool flag = false;
			int row2 = this.selectedRow;
			if (this.selectedRow != row || !gridEntryFromRow.Equals(this.selectedGridEntry))
			{
				this.CommonEditorHide();
				flag = true;
			}
			if (!flag)
			{
				this.CloseDropDown();
			}
			Rectangle rectangle = this.GetRectangle(row, 2);
			string propertyTextValue = gridEntryFromRow.GetPropertyTextValue();
			bool flag2 = gridEntryFromRow.NeedsDropDownButton | gridEntryFromRow.Enumerable;
			bool needsCustomEditorButton = gridEntryFromRow.NeedsCustomEditorButton;
			bool isTextEditable = gridEntryFromRow.IsTextEditable;
			bool isCustomPaint = gridEntryFromRow.IsCustomPaint;
			rectangle.X++;
			rectangle.Width--;
			if ((needsCustomEditorButton || flag2) && !gridEntryFromRow.ShouldRenderReadOnly && this.FocusInside)
			{
				Control control = flag2 ? this.DropDownButton : this.DialogButton;
				Size size = DpiHelper.EnableDpiChangedHighDpiImprovements ? new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight) : new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight);
				Rectangle rectTarget = new Rectangle(rectangle.X + rectangle.Width - size.Width, rectangle.Y, size.Width, rectangle.Height);
				this.CommonEditorUse(control, rectTarget);
				size = control.Size;
				rectangle.Width -= size.Width;
				control.Invalidate();
			}
			if (isCustomPaint)
			{
				rectangle.X += this.paintIndent + 1;
				rectangle.Width -= this.paintIndent + 1;
			}
			else
			{
				rectangle.X++;
				rectangle.Width--;
			}
			if ((this.GetFlag(2) || !this.Edit.Focused) && propertyTextValue != null && !propertyTextValue.Equals(this.Edit.Text))
			{
				this.Edit.Text = propertyTextValue;
				this.originalTextValue = propertyTextValue;
				this.Edit.SelectionStart = 0;
				this.Edit.SelectionLength = 0;
			}
			this.Edit.AccessibleName = gridEntryFromRow.Label;
			switch (PropertyGridView.inheritRenderMode)
			{
			case 2:
				if (gridEntryFromRow.ShouldSerializePropertyValue())
				{
					rectangle.X += 8;
					rectangle.Width -= 8;
				}
				break;
			case 3:
				if (gridEntryFromRow.ShouldSerializePropertyValue())
				{
					this.Edit.Font = this.GetBoldFont();
				}
				else
				{
					this.Edit.Font = this.Font;
				}
				break;
			}
			if (this.GetFlag(4) || !gridEntryFromRow.HasValue || !this.FocusInside)
			{
				this.Edit.Visible = false;
			}
			else
			{
				rectangle.Offset(1, 1);
				rectangle.Height--;
				rectangle.Width--;
				this.CommonEditorUse(this.Edit, rectangle);
				bool shouldRenderReadOnly = gridEntryFromRow.ShouldRenderReadOnly;
				this.Edit.ForeColor = (shouldRenderReadOnly ? this.GrayTextColor : this.ForeColor);
				this.Edit.BackColor = this.BackColor;
				this.Edit.ReadOnly = (shouldRenderReadOnly || !gridEntryFromRow.IsTextEditable);
				this.Edit.UseSystemPasswordChar = gridEntryFromRow.ShouldRenderPassword;
			}
			GridEntry gridEntry = this.selectedGridEntry;
			this.selectedRow = row;
			this.selectedGridEntry = gridEntryFromRow;
			this.ownerGrid.SetStatusBox(gridEntryFromRow.PropertyLabel, gridEntryFromRow.PropertyDescription);
			if (this.selectedGridEntry != null)
			{
				this.selectedGridEntry.Focus = this.FocusInside;
			}
			if (!this.GetFlag(2))
			{
				this.FocusInternal();
			}
			this.InvalidateRow(row2);
			this.InvalidateRow(row);
			if (this.FocusInside)
			{
				this.SetFlag(2, false);
			}
			try
			{
				if (this.selectedGridEntry != gridEntry)
				{
					this.ownerGrid.OnSelectedGridItemChanged(gridEntry, this.selectedGridEntry);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x0015CE6C File Offset: 0x0015B06C
		public virtual void SetConstants()
		{
			this.visibleRows = (int)Math.Ceiling((double)this.GetOurSize().Height / (double)(1 + this.RowHeight));
			Size ourSize = this.GetOurSize();
			if (ourSize.Width >= 0)
			{
				this.labelRatio = Math.Max(Math.Min(this.labelRatio, 9.0), 1.1);
				this.labelWidth = this.ptOurLocation.X + (int)((double)ourSize.Width / this.labelRatio);
			}
			int num = this.labelWidth;
			bool flag = this.SetScrollbarLength();
			GridEntryCollection gridEntryCollection = this.GetAllGridEntries();
			if (gridEntryCollection != null)
			{
				int scrollOffset = this.GetScrollOffset();
				if (scrollOffset + this.visibleRows >= gridEntryCollection.Count)
				{
					this.visibleRows = gridEntryCollection.Count - scrollOffset;
				}
			}
			if (flag && ourSize.Width >= 0)
			{
				this.labelRatio = (double)this.GetOurSize().Width / (double)(num - this.ptOurLocation.X);
			}
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0015CF6B File Offset: 0x0015B16B
		private void SetCommitError(short error)
		{
			this.SetCommitError(error, error == 1);
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0015CF78 File Offset: 0x0015B178
		private void SetCommitError(short error, bool capture)
		{
			this.errorState = error;
			if (error != 0)
			{
				this.CancelSplitterMove();
			}
			this.Edit.HookMouseDown = capture;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0015CF98 File Offset: 0x0015B198
		internal void SetExpand(GridEntry gridEntry, bool value)
		{
			if (gridEntry != null && gridEntry.Expandable)
			{
				int rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
				int num = this.visibleRows - rowFromGridEntry;
				int num2 = this.selectedRow;
				if (this.selectedRow != -1 && rowFromGridEntry < this.selectedRow && this.Edit.Visible)
				{
					this.FocusInternal();
				}
				int scrollOffset = this.GetScrollOffset();
				int num3 = this.totalProps;
				gridEntry.InternalExpanded = value;
				if (AccessibilityImprovements.Level4)
				{
					UnsafeNativeMethods.ExpandCollapseState expandCollapseState = value ? UnsafeNativeMethods.ExpandCollapseState.Collapsed : UnsafeNativeMethods.ExpandCollapseState.Expanded;
					UnsafeNativeMethods.ExpandCollapseState expandCollapseState2 = value ? UnsafeNativeMethods.ExpandCollapseState.Expanded : UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					GridEntry gridEntry2 = this.selectedGridEntry;
					if (gridEntry2 != null)
					{
						AccessibleObject accessibilityObject = gridEntry2.AccessibilityObject;
						if (accessibilityObject != null)
						{
							accessibilityObject.RaiseAutomationPropertyChangedEvent(30070, expandCollapseState, expandCollapseState2);
						}
					}
				}
				this.RecalculateProps();
				GridEntry gridEntry3 = this.selectedGridEntry;
				if (!value)
				{
					for (GridEntry gridEntry4 = gridEntry3; gridEntry4 != null; gridEntry4 = gridEntry4.ParentGridEntry)
					{
						if (gridEntry4.Equals(gridEntry))
						{
							gridEntry3 = gridEntry;
						}
					}
				}
				rowFromGridEntry = this.GetRowFromGridEntry(gridEntry);
				this.SetConstants();
				int num4 = this.totalProps - num3;
				if (value && num4 > 0 && num4 < this.visibleRows && rowFromGridEntry + num4 >= this.visibleRows && num4 < num2)
				{
					this.SetScrollOffset(this.totalProps - num3 + scrollOffset);
				}
				base.Invalidate();
				this.SelectGridEntry(gridEntry3, false);
				int scrollOffset2 = this.GetScrollOffset();
				this.SetScrollOffset(0);
				this.SetConstants();
				this.SetScrollOffset(scrollOffset2);
			}
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0015D0FD File Offset: 0x0015B2FD
		private void SetFlag(short flag, bool value)
		{
			if (value)
			{
				this.flags = (short)((ushort)this.flags | (ushort)flag);
				return;
			}
			this.flags &= ~flag;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0015D128 File Offset: 0x0015B328
		public virtual void SetScrollOffset(int cOffset)
		{
			int num = Math.Max(0, Math.Min(this.totalProps - this.visibleRows + 1, cOffset));
			int value = this.ScrollBar.Value;
			if (num != value && this.IsScrollValueValid(num) && this.visibleRows > 0)
			{
				this.ScrollBar.Value = num;
				base.Invalidate();
				this.selectedRow = this.GetRowFromGridEntry(this.selectedGridEntry);
			}
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0015D197 File Offset: 0x0015B397
		internal virtual bool _Commit()
		{
			return this.Commit();
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0015D1A0 File Offset: 0x0015B3A0
		private bool Commit()
		{
			if (this.errorState == 2)
			{
				return false;
			}
			if (!this.NeedsCommit)
			{
				this.SetCommitError(0);
				return true;
			}
			if (this.GetInPropertySet())
			{
				return false;
			}
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return true;
			}
			bool flag = false;
			try
			{
				flag = this.CommitText(this.Edit.Text);
			}
			finally
			{
				if (!flag)
				{
					this.Edit.FocusInternal();
					this.SelectEdit(false);
				}
				else
				{
					this.SetCommitError(0);
				}
			}
			return flag;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0015D22C File Offset: 0x0015B42C
		private bool CommitValue(object value)
		{
			GridEntry gridEntryFromRow = this.selectedGridEntry;
			if (this.selectedGridEntry == null && this.selectedRow != -1)
			{
				gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			}
			return gridEntryFromRow == null || this.CommitValue(gridEntryFromRow, value, true);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0015D26C File Offset: 0x0015B46C
		internal bool CommitValue(GridEntry ipeCur, object value, bool closeDropDown = true)
		{
			int childCount = ipeCur.ChildCount;
			bool hookMouseDown = this.Edit.HookMouseDown;
			object oldValue = null;
			try
			{
				oldValue = ipeCur.PropertyValue;
			}
			catch
			{
			}
			try
			{
				this.SetFlag(16, true);
				if (ipeCur != null && ipeCur.Enumerable && closeDropDown)
				{
					this.CloseDropDown();
				}
				try
				{
					this.Edit.DisableMouseHook = true;
					ipeCur.PropertyValue = value;
				}
				finally
				{
					this.Edit.DisableMouseHook = false;
					this.Edit.HookMouseDown = hookMouseDown;
				}
			}
			catch (Exception ex)
			{
				this.SetCommitError(1);
				this.ShowInvalidMessage(ipeCur.PropertyLabel, value, ex);
				return false;
			}
			finally
			{
				this.SetFlag(16, false);
			}
			this.SetCommitError(0);
			string propertyTextValue = ipeCur.GetPropertyTextValue();
			if (!string.Equals(propertyTextValue, this.Edit.Text))
			{
				this.Edit.Text = propertyTextValue;
				this.Edit.SelectionStart = 0;
				this.Edit.SelectionLength = 0;
			}
			this.originalTextValue = propertyTextValue;
			this.UpdateResetCommand(ipeCur);
			if (ipeCur.ChildCount != childCount)
			{
				this.ClearGridEntryEvents(this.allGridEntries, 0, -1);
				this.allGridEntries = null;
				this.SelectGridEntry(ipeCur, true);
			}
			if (ipeCur.Disposed)
			{
				bool flag = this.edit != null && this.edit.Focused;
				this.SelectGridEntry(ipeCur, true);
				ipeCur = this.selectedGridEntry;
				if (flag && this.edit != null)
				{
					this.edit.Focus();
				}
			}
			this.ownerGrid.OnPropertyValueSet(ipeCur, oldValue);
			return true;
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0015D41C File Offset: 0x0015B61C
		private bool CommitText(string text)
		{
			object value = null;
			GridEntry gridEntryFromRow = this.selectedGridEntry;
			if (this.selectedGridEntry == null && this.selectedRow != -1)
			{
				gridEntryFromRow = this.GetGridEntryFromRow(this.selectedRow);
			}
			if (gridEntryFromRow == null)
			{
				return true;
			}
			try
			{
				value = gridEntryFromRow.ConvertTextToValue(text);
			}
			catch (Exception ex)
			{
				this.SetCommitError(1);
				this.ShowInvalidMessage(gridEntryFromRow.PropertyLabel, text, ex);
				return false;
			}
			this.SetCommitError(0);
			return this.CommitValue(value);
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0015D49C File Offset: 0x0015B69C
		internal void ReverseFocus()
		{
			if (this.selectedGridEntry == null)
			{
				this.FocusInternal();
				return;
			}
			this.SelectGridEntry(this.selectedGridEntry, true);
			if (this.DialogButton.Visible)
			{
				this.DialogButton.FocusInternal();
				return;
			}
			if (this.DropDownButton.Visible)
			{
				this.DropDownButton.FocusInternal();
				return;
			}
			if (this.Edit.Visible)
			{
				this.Edit.SelectAll();
				this.Edit.FocusInternal();
			}
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x0015D520 File Offset: 0x0015B720
		private bool SetScrollbarLength()
		{
			bool result = false;
			if (this.totalProps != -1)
			{
				if (this.totalProps < this.visibleRows)
				{
					this.SetScrollOffset(0);
				}
				else if (this.GetScrollOffset() > this.totalProps)
				{
					this.SetScrollOffset(this.totalProps + 1 - this.visibleRows);
				}
				bool flag = !this.ScrollBar.Visible;
				if (this.visibleRows > 0)
				{
					this.ScrollBar.LargeChange = this.visibleRows - 1;
				}
				this.ScrollBar.Maximum = Math.Max(0, this.totalProps - 1);
				if (flag != this.totalProps < this.visibleRows)
				{
					result = true;
					this.ScrollBar.Visible = flag;
					Size ourSize = this.GetOurSize();
					if (this.labelWidth != -1 && ourSize.Width > 0)
					{
						if (this.labelWidth > this.ptOurLocation.X + ourSize.Width)
						{
							this.labelWidth = this.ptOurLocation.X + (int)((double)ourSize.Width / this.labelRatio);
						}
						else
						{
							this.labelRatio = (double)this.GetOurSize().Width / (double)(this.labelWidth - this.ptOurLocation.X);
						}
					}
					base.Invalidate();
				}
			}
			return result;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0015D664 File Offset: 0x0015B864
		public DialogResult ShowDialog(Form dialog)
		{
			if (dialog.StartPosition == FormStartPosition.CenterScreen)
			{
				Control control = this;
				if (control != null)
				{
					while (control.ParentInternal != null)
					{
						control = control.ParentInternal;
					}
					if (control.Size.Equals(dialog.Size))
					{
						dialog.StartPosition = FormStartPosition.Manual;
						Point location = control.Location;
						location.Offset(25, 25);
						dialog.Location = location;
					}
				}
			}
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			DialogResult result;
			if (iuiservice != null)
			{
				result = iuiservice.ShowDialog(dialog);
			}
			else
			{
				result = dialog.ShowDialog(this);
			}
			if (focus != IntPtr.Zero)
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(null, focus));
			}
			return result;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0015D720 File Offset: 0x0015B920
		private void ShowFormatExceptionMessage(string propName, object value, Exception ex)
		{
			if (value == null)
			{
				value = "(null)";
			}
			if (propName == null)
			{
				propName = "(unknown)";
			}
			bool hookMouseDown = this.Edit.HookMouseDown;
			this.Edit.DisableMouseHook = true;
			this.SetCommitError(2, false);
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			while (UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 512, 522, 1))
			{
			}
			if (ex is TargetInvocationException)
			{
				ex = ex.InnerException;
			}
			string message = ex.Message;
			while (message == null || message.Length == 0)
			{
				ex = ex.InnerException;
				if (ex == null)
				{
					break;
				}
				message = ex.Message;
			}
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			this.ErrorDialog.Message = SR.GetString("PBRSFormatExceptionMessage");
			this.ErrorDialog.Text = SR.GetString("PBRSErrorTitle");
			this.ErrorDialog.Details = message;
			bool flag;
			if (iuiservice != null)
			{
				flag = (DialogResult.Cancel == iuiservice.ShowDialog(this.ErrorDialog));
			}
			else
			{
				flag = (DialogResult.Cancel == this.ShowDialog(this.ErrorDialog));
			}
			this.Edit.DisableMouseHook = false;
			if (hookMouseDown)
			{
				this.SelectGridEntry(this.selectedGridEntry, true);
			}
			this.SetCommitError(1, hookMouseDown);
			if (flag)
			{
				this.OnEscape(this.Edit);
			}
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x0015D864 File Offset: 0x0015BA64
		internal void ShowInvalidMessage(string propName, object value, Exception ex)
		{
			if (value == null)
			{
				value = "(null)";
			}
			if (propName == null)
			{
				propName = "(unknown)";
			}
			bool hookMouseDown = this.Edit.HookMouseDown;
			this.Edit.DisableMouseHook = true;
			this.SetCommitError(2, false);
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			while (UnsafeNativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 512, 522, 1))
			{
			}
			if (ex is TargetInvocationException)
			{
				ex = ex.InnerException;
			}
			string message = ex.Message;
			while (message == null || message.Length == 0)
			{
				ex = ex.InnerException;
				if (ex == null)
				{
					break;
				}
				message = ex.Message;
			}
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			this.ErrorDialog.Message = SR.GetString("PBRSErrorInvalidPropertyValue");
			this.ErrorDialog.Text = SR.GetString("PBRSErrorTitle");
			this.ErrorDialog.Details = message;
			bool flag;
			if (iuiservice != null)
			{
				flag = (DialogResult.Cancel == iuiservice.ShowDialog(this.ErrorDialog));
			}
			else
			{
				flag = (DialogResult.Cancel == this.ShowDialog(this.ErrorDialog));
			}
			this.Edit.DisableMouseHook = false;
			if (hookMouseDown)
			{
				this.SelectGridEntry(this.selectedGridEntry, true);
			}
			this.SetCommitError(1, hookMouseDown);
			if (flag)
			{
				this.OnEscape(this.Edit);
			}
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x0015D9A6 File Offset: 0x0015BBA6
		private bool SplitterInside(int x, int y)
		{
			return Math.Abs(x - this.InternalLabelWidth) < 4;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x0015D9B8 File Offset: 0x0015BBB8
		private void TabSelection()
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return;
			}
			if (this.Edit.Visible)
			{
				this.Edit.FocusInternal();
				this.SelectEdit(false);
				return;
			}
			if (this.dropDownHolder != null && this.dropDownHolder.Visible)
			{
				this.dropDownHolder.FocusComponent();
				return;
			}
			if (this.currentEditor != null)
			{
				this.currentEditor.FocusInternal();
			}
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0015DA2C File Offset: 0x0015BC2C
		internal void RemoveSelectedEntryHelpAttributes()
		{
			this.UpdateHelpAttributes(this.selectedGridEntry, null);
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0015DA3C File Offset: 0x0015BC3C
		private void UpdateHelpAttributes(GridEntry oldEntry, GridEntry newEntry)
		{
			IHelpService helpService = this.GetHelpService();
			if (helpService == null || oldEntry == newEntry)
			{
				return;
			}
			GridEntry gridEntry = oldEntry;
			if (oldEntry != null && !oldEntry.Disposed)
			{
				while (gridEntry != null)
				{
					helpService.RemoveContextAttribute("Keyword", gridEntry.HelpKeyword);
					gridEntry = gridEntry.ParentGridEntry;
				}
			}
			if (newEntry != null)
			{
				this.UpdateHelpAttributes(helpService, newEntry, true);
			}
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0015DA90 File Offset: 0x0015BC90
		private void UpdateHelpAttributes(IHelpService helpSvc, GridEntry entry, bool addAsF1)
		{
			if (entry == null)
			{
				return;
			}
			this.UpdateHelpAttributes(helpSvc, entry.ParentGridEntry, false);
			string helpKeyword = entry.HelpKeyword;
			if (helpKeyword != null)
			{
				helpSvc.AddContextAttribute("Keyword", helpKeyword, addAsF1 ? HelpKeywordType.F1Keyword : HelpKeywordType.GeneralKeyword);
			}
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x0015DACC File Offset: 0x0015BCCC
		private void UpdateUIBasedOnFont(bool layoutRequired)
		{
			if (base.IsHandleCreated && this.GetFlag(128))
			{
				try
				{
					if (this.listBox != null)
					{
						this.DropDownListBox.ItemHeight = this.RowHeight + 2;
					}
					if (this.btnDropDown != null)
					{
						if (DpiHelper.EnableDpiChangedHighDpiImprovements)
						{
							this.btnDropDown.Size = new Size(SystemInformation.VerticalScrollBarArrowHeightForDpi(this.deviceDpi), this.RowHeight);
						}
						else
						{
							this.btnDropDown.Size = new Size(SystemInformation.VerticalScrollBarArrowHeight, this.RowHeight);
						}
						if (this.btnDialog != null)
						{
							this.DialogButton.Size = this.DropDownButton.Size;
							if (DpiHelper.EnableDpiChangedHighDpiImprovements)
							{
								this.btnDialog.Image = this.CreateResizedBitmap("dotdotdot.ico", 7, 8);
							}
						}
						if (DpiHelper.EnableDpiChangedHighDpiImprovements)
						{
							this.btnDropDown.Image = this.CreateResizedBitmap("Arrow.ico", 16, 16);
						}
					}
					if (layoutRequired)
					{
						this.LayoutWindow(true);
					}
				}
				finally
				{
					this.SetFlag(128, false);
				}
			}
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0015DBE8 File Offset: 0x0015BDE8
		private bool UnfocusSelection()
		{
			if (this.GetGridEntryFromRow(this.selectedRow) == null)
			{
				return true;
			}
			bool flag = this.Commit();
			if (flag && this.FocusInside)
			{
				this.FocusInternal();
			}
			return flag;
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x0015DC24 File Offset: 0x0015BE24
		private void UpdateResetCommand(GridEntry gridEntry)
		{
			if (this.totalProps > 0)
			{
				IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
				if (menuCommandService != null)
				{
					MenuCommand menuCommand = menuCommandService.FindCommand(PropertyGridCommands.Reset);
					if (menuCommand != null)
					{
						menuCommand.Enabled = (gridEntry != null && gridEntry.CanResetPropertyValue());
					}
				}
			}
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x0015DC74 File Offset: 0x0015BE74
		internal bool WantsTab(bool forward)
		{
			if (forward)
			{
				if (this.Focused)
				{
					if (this.DropDownButton.Visible || this.DialogButton.Visible || this.Edit.Visible)
					{
						return true;
					}
				}
				else if (this.Edit.Focused && (this.DropDownButton.Visible || this.DialogButton.Visible))
				{
					return true;
				}
				return this.ownerGrid.WantsTab(forward);
			}
			return this.Edit.Focused || this.DropDownButton.Focused || this.DialogButton.Focused || this.ownerGrid.WantsTab(forward);
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x0015DD20 File Offset: 0x0015BF20
		private unsafe bool WmNotify(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)((void*)m.LParam);
				if (ptr->hwndFrom == this.ToolTip.Handle)
				{
					int code = ptr->code;
					if (code != -522 && code == -521)
					{
						Point point = Cursor.Position;
						point = base.PointToClientInternal(point);
						point = this.FindPosition(point.X, point.Y);
						if (!(point == PropertyGridView.InvalidPosition))
						{
							GridEntry gridEntryFromRow = this.GetGridEntryFromRow(point.Y);
							if (gridEntryFromRow != null)
							{
								Rectangle rectangle = this.GetRectangle(point.Y, point.X);
								Point point2 = Point.Empty;
								if (point.X == 1)
								{
									point2 = gridEntryFromRow.GetLabelToolTipLocation(point.X - rectangle.X, point.Y - rectangle.Y);
								}
								else
								{
									if (point.X != 2)
									{
										return false;
									}
									point2 = gridEntryFromRow.ValueToolTipLocation;
								}
								if (point2 != PropertyGridView.InvalidPoint)
								{
									rectangle.Offset(point2);
									this.ToolTip.PositionToolTip(this, rectangle);
									m.Result = (IntPtr)1;
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0015DE64 File Offset: 0x0015C064
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 135)
			{
				if (msg <= 21)
				{
					if (msg != 7)
					{
						if (msg == 21)
						{
							base.Invalidate();
						}
					}
					else if (!this.GetInPropertySet() && this.Edit.Visible && (this.errorState != 0 || !this.Commit()))
					{
						base.WndProc(ref m);
						this.Edit.FocusInternal();
						return;
					}
				}
				else if (msg != 78)
				{
					if (msg == 135)
					{
						int num = 129;
						if (this.selectedGridEntry != null && (Control.ModifierKeys & Keys.Shift) == Keys.None && this.edit.Visible)
						{
							num |= 2;
						}
						m.Result = (IntPtr)num;
						return;
					}
				}
				else if (this.WmNotify(ref m))
				{
					return;
				}
			}
			else if (msg <= 271)
			{
				if (msg == 269)
				{
					this.Edit.FocusInternal();
					this.Edit.Clear();
					UnsafeNativeMethods.PostMessage(new HandleRef(this.Edit, this.Edit.Handle), 269, 0, 0);
					return;
				}
				if (msg == 271)
				{
					this.Edit.FocusInternal();
					UnsafeNativeMethods.PostMessage(new HandleRef(this.Edit, this.Edit.Handle), 271, m.WParam, m.LParam);
					return;
				}
			}
			else if (msg != 512)
			{
				if (msg == 1110)
				{
					m.Result = (IntPtr)Math.Min(this.visibleRows, this.totalProps);
					return;
				}
				if (msg == 1111)
				{
					m.Result = (IntPtr)this.GetRowFromGridEntry(this.selectedGridEntry);
					return;
				}
			}
			else
			{
				if ((int)((long)m.LParam) == this.lastMouseMove)
				{
					return;
				}
				this.lastMouseMove = (int)((long)m.LParam);
			}
			base.WndProc(ref m);
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0015E05E File Offset: 0x0015C25E
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.RescaleConstants();
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0015E070 File Offset: 0x0015C270
		private void RescaleConstants()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.ClearCachedFontInfo();
				this.cachedRowHeight = -1;
				this.paintWidth = base.LogicalToDeviceUnits(20);
				this.paintIndent = base.LogicalToDeviceUnits(26);
				this.outlineSizeExplorerTreeStyle = base.LogicalToDeviceUnits(16);
				this.outlineSize = base.LogicalToDeviceUnits(9);
				this.maxListBoxHeight = base.LogicalToDeviceUnits(200);
				this.offset_2Units = base.LogicalToDeviceUnits(PropertyGridView.OFFSET_2PIXELS);
				if (this.topLevelGridEntries != null)
				{
					foreach (object obj in this.topLevelGridEntries)
					{
						GridEntry entry = (GridEntry)obj;
						this.ResetOutline(entry);
					}
				}
			}
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0015E144 File Offset: 0x0015C344
		private void ResetOutline(GridEntry entry)
		{
			entry.OutlineRect = Rectangle.Empty;
			if (entry.ChildCount > 0)
			{
				foreach (object obj in entry.Children)
				{
					GridEntry entry2 = (GridEntry)obj;
					this.ResetOutline(entry2);
				}
			}
		}

		// Token: 0x04003663 RID: 13923
		protected static readonly Point InvalidPoint = new Point(int.MinValue, int.MinValue);

		// Token: 0x04003664 RID: 13924
		public const int RENDERMODE_LEFTDOT = 2;

		// Token: 0x04003665 RID: 13925
		public const int RENDERMODE_BOLD = 3;

		// Token: 0x04003666 RID: 13926
		public const int RENDERMODE_TRIANGLE = 4;

		// Token: 0x04003667 RID: 13927
		public static int inheritRenderMode = 3;

		// Token: 0x04003668 RID: 13928
		public static TraceSwitch GridViewDebugPaint = new TraceSwitch("GridViewDebugPaint", "PropertyGridView: Debug property painting");

		// Token: 0x04003669 RID: 13929
		private PropertyGrid ownerGrid;

		// Token: 0x0400366A RID: 13930
		private const int LEFTDOT_SIZE = 4;

		// Token: 0x0400366B RID: 13931
		private const int EDIT_INDENT = 0;

		// Token: 0x0400366C RID: 13932
		private const int OUTLINE_INDENT = 10;

		// Token: 0x0400366D RID: 13933
		private const int OUTLINE_SIZE = 9;

		// Token: 0x0400366E RID: 13934
		private const int OUTLINE_SIZE_EXPLORER_TREE_STYLE = 16;

		// Token: 0x0400366F RID: 13935
		private int outlineSize = 9;

		// Token: 0x04003670 RID: 13936
		private int outlineSizeExplorerTreeStyle = 16;

		// Token: 0x04003671 RID: 13937
		private const int PAINT_WIDTH = 20;

		// Token: 0x04003672 RID: 13938
		private int paintWidth = 20;

		// Token: 0x04003673 RID: 13939
		private const int PAINT_INDENT = 26;

		// Token: 0x04003674 RID: 13940
		private int paintIndent = 26;

		// Token: 0x04003675 RID: 13941
		private const int ROWLABEL = 1;

		// Token: 0x04003676 RID: 13942
		private const int ROWVALUE = 2;

		// Token: 0x04003677 RID: 13943
		private const int MAX_LISTBOX_HEIGHT = 200;

		// Token: 0x04003678 RID: 13944
		private int maxListBoxHeight = 200;

		// Token: 0x04003679 RID: 13945
		private const short ERROR_NONE = 0;

		// Token: 0x0400367A RID: 13946
		private const short ERROR_THROWN = 1;

		// Token: 0x0400367B RID: 13947
		private const short ERROR_MSGBOX_UP = 2;

		// Token: 0x0400367C RID: 13948
		internal const short GDIPLUS_SPACE = 2;

		// Token: 0x0400367D RID: 13949
		internal const int MaxRecurseExpand = 10;

		// Token: 0x0400367E RID: 13950
		private const int DOTDOTDOT_ICONWIDTH = 7;

		// Token: 0x0400367F RID: 13951
		private const int DOTDOTDOT_ICONHEIGHT = 8;

		// Token: 0x04003680 RID: 13952
		private const int DOWNARROW_ICONWIDTH = 16;

		// Token: 0x04003681 RID: 13953
		private const int DOWNARROW_ICONHEIGHT = 16;

		// Token: 0x04003682 RID: 13954
		private static int OFFSET_2PIXELS = 2;

		// Token: 0x04003683 RID: 13955
		private int offset_2Units = PropertyGridView.OFFSET_2PIXELS;

		// Token: 0x04003684 RID: 13956
		protected static readonly Point InvalidPosition = new Point(int.MinValue, int.MinValue);

		// Token: 0x04003685 RID: 13957
		private Brush backgroundBrush;

		// Token: 0x04003686 RID: 13958
		private Font fontBold;

		// Token: 0x04003687 RID: 13959
		private Color grayTextColor;

		// Token: 0x04003688 RID: 13960
		private bool grayTextColorModified;

		// Token: 0x04003689 RID: 13961
		private GridEntryCollection topLevelGridEntries;

		// Token: 0x0400368A RID: 13962
		private GridEntryCollection allGridEntries;

		// Token: 0x0400368B RID: 13963
		internal int totalProps = -1;

		// Token: 0x0400368C RID: 13964
		private int visibleRows = -1;

		// Token: 0x0400368D RID: 13965
		private int labelWidth = -1;

		// Token: 0x0400368E RID: 13966
		public double labelRatio = 2.0;

		// Token: 0x0400368F RID: 13967
		private short requiredLabelPaintMargin = 2;

		// Token: 0x04003690 RID: 13968
		private int selectedRow = -1;

		// Token: 0x04003691 RID: 13969
		private GridEntry selectedGridEntry;

		// Token: 0x04003692 RID: 13970
		private int tipInfo = -1;

		// Token: 0x04003693 RID: 13971
		private PropertyGridView.GridViewEdit edit;

		// Token: 0x04003694 RID: 13972
		private DropDownButton btnDropDown;

		// Token: 0x04003695 RID: 13973
		private DropDownButton btnDialog;

		// Token: 0x04003696 RID: 13974
		private PropertyGridView.GridViewListBox listBox;

		// Token: 0x04003697 RID: 13975
		private PropertyGridView.DropDownHolder dropDownHolder;

		// Token: 0x04003698 RID: 13976
		private Rectangle lastClientRect = Rectangle.Empty;

		// Token: 0x04003699 RID: 13977
		private Control currentEditor;

		// Token: 0x0400369A RID: 13978
		private ScrollBar scrollBar;

		// Token: 0x0400369B RID: 13979
		internal GridToolTip toolTip;

		// Token: 0x0400369C RID: 13980
		private GridErrorDlg errorDlg;

		// Token: 0x0400369D RID: 13981
		private const short FlagNeedsRefresh = 1;

		// Token: 0x0400369E RID: 13982
		private const short FlagIsNewSelection = 2;

		// Token: 0x0400369F RID: 13983
		private const short FlagIsSplitterMove = 4;

		// Token: 0x040036A0 RID: 13984
		private const short FlagIsSpecialKey = 8;

		// Token: 0x040036A1 RID: 13985
		private const short FlagInPropertySet = 16;

		// Token: 0x040036A2 RID: 13986
		private const short FlagDropDownClosing = 32;

		// Token: 0x040036A3 RID: 13987
		private const short FlagDropDownCommit = 64;

		// Token: 0x040036A4 RID: 13988
		private const short FlagNeedUpdateUIBasedOnFont = 128;

		// Token: 0x040036A5 RID: 13989
		private const short FlagBtnLaunchedEditor = 256;

		// Token: 0x040036A6 RID: 13990
		private const short FlagNoDefault = 512;

		// Token: 0x040036A7 RID: 13991
		private const short FlagResizableDropDown = 1024;

		// Token: 0x040036A8 RID: 13992
		private short flags = 131;

		// Token: 0x040036A9 RID: 13993
		private short errorState;

		// Token: 0x040036AA RID: 13994
		private Point ptOurLocation = new Point(1, 1);

		// Token: 0x040036AB RID: 13995
		private string originalTextValue;

		// Token: 0x040036AC RID: 13996
		private int cumulativeVerticalWheelDelta;

		// Token: 0x040036AD RID: 13997
		private long rowSelectTime;

		// Token: 0x040036AE RID: 13998
		private Point rowSelectPos = Point.Empty;

		// Token: 0x040036AF RID: 13999
		private Point lastMouseDown = PropertyGridView.InvalidPosition;

		// Token: 0x040036B0 RID: 14000
		private int lastMouseMove;

		// Token: 0x040036B1 RID: 14001
		private GridEntry lastClickedEntry;

		// Token: 0x040036B2 RID: 14002
		private IServiceProvider serviceProvider;

		// Token: 0x040036B3 RID: 14003
		private IHelpService topHelpService;

		// Token: 0x040036B4 RID: 14004
		private IHelpService helpService;

		// Token: 0x040036B5 RID: 14005
		private EventHandler ehValueClick;

		// Token: 0x040036B6 RID: 14006
		private EventHandler ehLabelClick;

		// Token: 0x040036B7 RID: 14007
		private EventHandler ehOutlineClick;

		// Token: 0x040036B8 RID: 14008
		private EventHandler ehValueDblClick;

		// Token: 0x040036B9 RID: 14009
		private EventHandler ehLabelDblClick;

		// Token: 0x040036BA RID: 14010
		private GridEntryRecreateChildrenEventHandler ehRecreateChildren;

		// Token: 0x040036BB RID: 14011
		private int cachedRowHeight = -1;

		// Token: 0x040036BC RID: 14012
		private IntPtr baseHfont;

		// Token: 0x040036BD RID: 14013
		private IntPtr boldHfont;

		// Token: 0x040036BE RID: 14014
		private PropertyGridView.GridPositionData positionData;

		// Token: 0x02000880 RID: 2176
		private class GridViewEdit : TextBox, PropertyGridView.IMouseHookClient
		{
			// Token: 0x170018DC RID: 6364
			// (set) Token: 0x06007188 RID: 29064 RVA: 0x0019FB39 File Offset: 0x0019DD39
			public bool DontFocus
			{
				set
				{
					this.dontFocusMe = value;
				}
			}

			// Token: 0x170018DD RID: 6365
			// (get) Token: 0x06007189 RID: 29065 RVA: 0x0019FB42 File Offset: 0x0019DD42
			// (set) Token: 0x0600718A RID: 29066 RVA: 0x0019FB4A File Offset: 0x0019DD4A
			public virtual bool Filter
			{
				get
				{
					return this.filter;
				}
				set
				{
					this.filter = value;
				}
			}

			// Token: 0x170018DE RID: 6366
			// (get) Token: 0x0600718B RID: 29067 RVA: 0x000A8615 File Offset: 0x000A6815
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x170018DF RID: 6367
			// (get) Token: 0x0600718C RID: 29068 RVA: 0x0019FB53 File Offset: 0x0019DD53
			public override bool Focused
			{
				get
				{
					return !this.dontFocusMe && base.Focused;
				}
			}

			// Token: 0x170018E0 RID: 6368
			// (get) Token: 0x0600718D RID: 29069 RVA: 0x00167408 File Offset: 0x00165608
			// (set) Token: 0x0600718E RID: 29070 RVA: 0x0019FB65 File Offset: 0x0019DD65
			public override string Text
			{
				get
				{
					return base.Text;
				}
				set
				{
					this.fInSetText = true;
					base.Text = value;
					this.fInSetText = false;
				}
			}

			// Token: 0x170018E1 RID: 6369
			// (set) Token: 0x0600718F RID: 29071 RVA: 0x0019FB7C File Offset: 0x0019DD7C
			public bool DisableMouseHook
			{
				set
				{
					this.mouseHook.DisableMouseHook = value;
				}
			}

			// Token: 0x170018E2 RID: 6370
			// (get) Token: 0x06007190 RID: 29072 RVA: 0x0019FB8A File Offset: 0x0019DD8A
			// (set) Token: 0x06007191 RID: 29073 RVA: 0x0019FB97 File Offset: 0x0019DD97
			public virtual bool HookMouseDown
			{
				get
				{
					return this.mouseHook.HookMouseDown;
				}
				set
				{
					this.mouseHook.HookMouseDown = value;
					if (value)
					{
						this.FocusInternal();
					}
				}
			}

			// Token: 0x06007192 RID: 29074 RVA: 0x0019FBAF File Offset: 0x0019DDAF
			public GridViewEdit(PropertyGridView psheet)
			{
				this.psheet = psheet;
				this.mouseHook = new PropertyGridView.MouseHook(this, this, psheet);
			}

			// Token: 0x06007193 RID: 29075 RVA: 0x0019FBCC File Offset: 0x0019DDCC
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level5)
				{
					return new PropertyGridView.GridViewEdit.GridViewEditAccessibleObjectLevel5(this);
				}
				if (AccessibilityImprovements.Level2)
				{
					return new PropertyGridView.GridViewEdit.GridViewEditAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x06007194 RID: 29076 RVA: 0x0019FBF0 File Offset: 0x0019DDF0
			protected override void DestroyHandle()
			{
				this.mouseHook.HookMouseDown = false;
				base.DestroyHandle();
			}

			// Token: 0x06007195 RID: 29077 RVA: 0x0019FC04 File Offset: 0x0019DE04
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.mouseHook.Dispose();
				}
				base.Dispose(disposing);
			}

			// Token: 0x06007196 RID: 29078 RVA: 0x0019FC1B File Offset: 0x0019DE1B
			public void FilterKeyPress(char keyChar)
			{
				if (this.IsInputChar(keyChar))
				{
					this.FocusInternal();
					base.SelectAll();
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 258, (IntPtr)((int)keyChar), IntPtr.Zero);
				}
			}

			// Token: 0x06007197 RID: 29079 RVA: 0x0019FC58 File Offset: 0x0019DE58
			protected override bool IsInputKey(Keys keyData)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Return)
				{
					if (keys != Keys.Tab && keys != Keys.Return)
					{
						goto IL_2A;
					}
				}
				else if (keys != Keys.Escape && keys != Keys.F1 && keys != Keys.F4)
				{
					goto IL_2A;
				}
				return false;
				IL_2A:
				return !this.psheet.NeedsCommit && base.IsInputKey(keyData);
			}

			// Token: 0x06007198 RID: 29080 RVA: 0x0019FCA8 File Offset: 0x0019DEA8
			protected override bool IsInputChar(char keyChar)
			{
				return keyChar != '\t' && keyChar != '\r' && base.IsInputChar(keyChar);
			}

			// Token: 0x06007199 RID: 29081 RVA: 0x0019FCCA File Offset: 0x0019DECA
			protected override void OnKeyDown(KeyEventArgs ke)
			{
				if (this.ProcessDialogKey(ke.KeyData))
				{
					ke.Handled = true;
					return;
				}
				base.OnKeyDown(ke);
			}

			// Token: 0x0600719A RID: 29082 RVA: 0x0019FCE9 File Offset: 0x0019DEE9
			protected override void OnKeyPress(KeyPressEventArgs ke)
			{
				if (!this.IsInputChar(ke.KeyChar))
				{
					ke.Handled = true;
					return;
				}
				base.OnKeyPress(ke);
			}

			// Token: 0x0600719B RID: 29083 RVA: 0x0019FD08 File Offset: 0x0019DF08
			public bool OnClickHooked()
			{
				return !this.psheet._Commit();
			}

			// Token: 0x0600719C RID: 29084 RVA: 0x0019FD18 File Offset: 0x0019DF18
			protected override void OnMouseEnter(EventArgs e)
			{
				base.OnMouseEnter(e);
				if (!this.Focused)
				{
					Graphics graphics = base.CreateGraphics();
					if (this.psheet.SelectedGridEntry != null && base.ClientRectangle.Width <= this.psheet.SelectedGridEntry.GetValueTextWidth(this.Text, graphics, this.Font))
					{
						this.psheet.ToolTip.ToolTip = (this.PasswordProtect ? "" : this.Text);
					}
					graphics.Dispose();
				}
			}

			// Token: 0x0600719D RID: 29085 RVA: 0x0019FDA0 File Offset: 0x0019DFA0
			protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Delete)
				{
					if (keys != Keys.Insert)
					{
						if (keys == Keys.Delete)
						{
							if ((keyData & Keys.Control) == Keys.None && (keyData & Keys.Shift) != Keys.None && (keyData & Keys.Alt) == Keys.None)
							{
								return false;
							}
							if ((keyData & Keys.Control) == Keys.None && (keyData & Keys.Shift) == Keys.None && (keyData & Keys.Alt) == Keys.None && this.psheet.SelectedGridEntry != null && !this.psheet.SelectedGridEntry.Enumerable && !this.psheet.SelectedGridEntry.IsTextEditable && this.psheet.SelectedGridEntry.CanResetPropertyValue())
							{
								object propertyValue = this.psheet.SelectedGridEntry.PropertyValue;
								this.psheet.SelectedGridEntry.ResetPropertyValue();
								this.psheet.UnfocusSelection();
								this.psheet.ownerGrid.OnPropertyValueSet(this.psheet.SelectedGridEntry, propertyValue);
							}
						}
					}
					else if ((keyData & Keys.Alt) == Keys.None && ((keyData & Keys.Control) > Keys.None ^ (keyData & Keys.Shift) == Keys.None))
					{
						return false;
					}
				}
				else if (keys != Keys.A)
				{
					if (keys != Keys.C)
					{
						switch (keys)
						{
						case Keys.V:
						case Keys.X:
						case Keys.Z:
							break;
						case Keys.W:
						case Keys.Y:
							goto IL_195;
						default:
							goto IL_195;
						}
					}
					if ((keyData & Keys.Control) != Keys.None && (keyData & Keys.Shift) == Keys.None && (keyData & Keys.Alt) == Keys.None)
					{
						return false;
					}
				}
				else if ((keyData & Keys.Control) != Keys.None && (keyData & Keys.Shift) == Keys.None && (keyData & Keys.Alt) == Keys.None)
				{
					base.SelectAll();
					return true;
				}
				IL_195:
				return base.ProcessCmdKey(ref msg, keyData);
			}

			// Token: 0x0600719E RID: 29086 RVA: 0x0019FF4C File Offset: 0x0019E14C
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if ((keyData & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.None)
				{
					Keys keys = keyData & Keys.KeyCode;
					if (keys == Keys.Return)
					{
						bool flag = !this.psheet.NeedsCommit;
						if (this.psheet.UnfocusSelection() && flag && this.psheet.SelectedGridEntry != null)
						{
							this.psheet.SelectedGridEntry.OnValueReturnKey();
						}
						return true;
					}
					if (keys == Keys.Escape)
					{
						this.psheet.OnEscape(this);
						return true;
					}
					if (keys == Keys.F4)
					{
						this.psheet.F4Selection(true);
						return true;
					}
				}
				if ((keyData & Keys.KeyCode) == Keys.Tab && (keyData & (Keys.Control | Keys.Alt)) == Keys.None)
				{
					return !this.psheet._Commit();
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x0600719F RID: 29087 RVA: 0x001A0000 File Offset: 0x0019E200
			protected override void SetVisibleCore(bool value)
			{
				if (!value && this.HookMouseDown)
				{
					this.mouseHook.HookMouseDown = false;
				}
				base.SetVisibleCore(value);
			}

			// Token: 0x060071A0 RID: 29088 RVA: 0x001A0020 File Offset: 0x0019E220
			internal bool WantsTab(bool forward)
			{
				return this.psheet.WantsTab(forward);
			}

			// Token: 0x060071A1 RID: 29089 RVA: 0x001A0030 File Offset: 0x0019E230
			private unsafe bool WmNotify(ref Message m)
			{
				if (m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)((void*)m.LParam);
					if (ptr->hwndFrom == this.psheet.ToolTip.Handle)
					{
						int code = ptr->code;
						if (code == -521)
						{
							this.psheet.ToolTip.PositionToolTip(this, base.ClientRectangle);
							m.Result = (IntPtr)1;
							return true;
						}
						this.psheet.WndProc(ref m);
					}
				}
				return false;
			}

			// Token: 0x060071A2 RID: 29090 RVA: 0x001A00BC File Offset: 0x0019E2BC
			protected override void WndProc(ref Message m)
			{
				if (this.filter && this.psheet.FilterEditWndProc(ref m))
				{
					return;
				}
				int msg = m.Msg;
				if (msg <= 78)
				{
					if (msg != 2)
					{
						if (msg != 24)
						{
							if (msg == 78)
							{
								if (this.WmNotify(ref m))
								{
									return;
								}
							}
						}
						else if (IntPtr.Zero == m.WParam)
						{
							this.mouseHook.HookMouseDown = false;
						}
					}
					else
					{
						this.mouseHook.HookMouseDown = false;
					}
				}
				else if (msg <= 135)
				{
					if (msg != 125)
					{
						if (msg == 135)
						{
							m.Result = (IntPtr)((long)m.Result | 1L | 128L);
							if (this.psheet.NeedsCommit || this.WantsTab((Control.ModifierKeys & Keys.Shift) == Keys.None))
							{
								m.Result = (IntPtr)((long)m.Result | 4L | 2L);
							}
							return;
						}
					}
					else if (((int)((long)m.WParam) & -20) != 0)
					{
						this.psheet.Invalidate();
					}
				}
				else if (msg != 512)
				{
					if (msg == 770)
					{
						if (base.ReadOnly)
						{
							return;
						}
					}
				}
				else
				{
					if ((int)((long)m.LParam) == this.lastMove)
					{
						return;
					}
					this.lastMove = (int)((long)m.LParam);
				}
				base.WndProc(ref m);
			}

			// Token: 0x060071A3 RID: 29091 RVA: 0x001A023B File Offset: 0x0019E43B
			public virtual bool InSetText()
			{
				return this.fInSetText;
			}

			// Token: 0x0400447E RID: 17534
			internal bool fInSetText;

			// Token: 0x0400447F RID: 17535
			internal bool filter;

			// Token: 0x04004480 RID: 17536
			internal PropertyGridView psheet;

			// Token: 0x04004481 RID: 17537
			private bool dontFocusMe;

			// Token: 0x04004482 RID: 17538
			private int lastMove;

			// Token: 0x04004483 RID: 17539
			private PropertyGridView.MouseHook mouseHook;

			// Token: 0x0200097E RID: 2430
			[ComVisible(true)]
			private class GridViewEditAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
			{
				// Token: 0x06007588 RID: 30088 RVA: 0x001A9810 File Offset: 0x001A7A10
				public GridViewEditAccessibleObjectLevel5(PropertyGridView.GridViewEdit owner) : base(owner)
				{
					this._owningPropertyGridView = owner.psheet;
				}

				// Token: 0x06007589 RID: 30089 RVA: 0x001A9825 File Offset: 0x001A7A25
				internal override void ClearOwnerControlInternal()
				{
					this._owningPropertyGridView = null;
					base.ClearOwnerControlInternal();
				}

				// Token: 0x17001AF8 RID: 6904
				// (get) Token: 0x0600758A RID: 30090 RVA: 0x001A9834 File Offset: 0x001A7A34
				public override AccessibleStates State
				{
					get
					{
						AccessibleStates accessibleStates = base.State;
						if (this.IsReadOnly)
						{
							accessibleStates |= AccessibleStates.ReadOnly;
						}
						else
						{
							accessibleStates &= ~AccessibleStates.ReadOnly;
						}
						return accessibleStates;
					}
				}

				// Token: 0x0600758B RID: 30091 RVA: 0x001A9860 File Offset: 0x001A7A60
				internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
					PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject = ((selectedGridEntry != null) ? selectedGridEntry.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
					if (propertyDescriptorGridEntryAccessibleObject == null)
					{
						return null;
					}
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.Parent:
						return propertyDescriptorGridEntryAccessibleObject;
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetNextChildFragment(this);
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetPreviousChildFragment(this);
					default:
						return base.FragmentNavigate(direction);
					}
				}

				// Token: 0x17001AF9 RID: 6905
				// (get) Token: 0x0600758C RID: 30092 RVA: 0x001A98C6 File Offset: 0x001A7AC6
				internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return null;
						}
						PropertyGrid ownerGrid = this._owningPropertyGridView.OwnerGrid;
						if (ownerGrid == null)
						{
							return null;
						}
						return ownerGrid.AccessibilityObject;
					}
				}

				// Token: 0x0600758D RID: 30093 RVA: 0x001A98E8 File Offset: 0x001A7AE8
				internal override object GetPropertyValue(int propertyID)
				{
					if (propertyID == 30010)
					{
						return !this.IsReadOnly;
					}
					return base.GetPropertyValue(propertyID);
				}

				// Token: 0x0600758E RID: 30094 RVA: 0x000110D4 File Offset: 0x0000F2D4
				internal override bool IsPatternSupported(int patternId)
				{
					return !base.IsOwnerControlDestroyed() && (patternId == 10002 || base.IsPatternSupported(patternId));
				}

				// Token: 0x17001AFA RID: 6906
				// (get) Token: 0x0600758F RID: 30095 RVA: 0x001A9908 File Offset: 0x001A7B08
				// (set) Token: 0x06007590 RID: 30096 RVA: 0x0001106B File Offset: 0x0000F26B
				public override string Name
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return base.Name;
						}
						string accessibleName = base.Owner.AccessibleName;
						if (accessibleName != null)
						{
							return accessibleName;
						}
						GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
						if (selectedGridEntry != null)
						{
							return selectedGridEntry.AccessibilityObject.Name;
						}
						return base.Name;
					}
					set
					{
						base.Name = value;
					}
				}

				// Token: 0x17001AFB RID: 6907
				// (get) Token: 0x06007591 RID: 30097 RVA: 0x001A9958 File Offset: 0x001A7B58
				internal override bool IsReadOnly
				{
					get
					{
						if (!base.IsOwnerControlDestroyed())
						{
							PropertyDescriptorGridEntry propertyDescriptorGridEntry = this._owningPropertyGridView.SelectedGridEntry as PropertyDescriptorGridEntry;
							if (propertyDescriptorGridEntry != null)
							{
								return propertyDescriptorGridEntry.IsPropertyReadOnly;
							}
						}
						return true;
					}
				}

				// Token: 0x06007592 RID: 30098 RVA: 0x0015F2B1 File Offset: 0x0015D4B1
				internal override void SetFocus()
				{
					if (base.IsOwnerControlDestroyed())
					{
						return;
					}
					base.RaiseAutomationEvent(20005);
					base.SetFocus();
				}

				// Token: 0x040047D3 RID: 18387
				private PropertyGridView _owningPropertyGridView;
			}

			// Token: 0x0200097F RID: 2431
			[ComVisible(true)]
			protected class GridViewEditAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x06007593 RID: 30099 RVA: 0x001A9989 File Offset: 0x001A7B89
				public GridViewEditAccessibleObject(PropertyGridView.GridViewEdit owner) : base(owner)
				{
					this.propertyGridView = owner.psheet;
				}

				// Token: 0x06007594 RID: 30100 RVA: 0x001A999E File Offset: 0x001A7B9E
				internal override void ClearOwnerControlInternal()
				{
					this.propertyGridView = null;
					base.ClearOwnerControlInternal();
				}

				// Token: 0x17001AFC RID: 6908
				// (get) Token: 0x06007595 RID: 30101 RVA: 0x001A99B0 File Offset: 0x001A7BB0
				public override AccessibleStates State
				{
					get
					{
						AccessibleStates accessibleStates = base.State;
						if (this.IsReadOnly)
						{
							accessibleStates |= AccessibleStates.ReadOnly;
						}
						else
						{
							accessibleStates &= ~AccessibleStates.ReadOnly;
						}
						return accessibleStates;
					}
				}

				// Token: 0x06007596 RID: 30102 RVA: 0x00162A9D File Offset: 0x00160C9D
				internal override bool IsIAccessibleExSupported()
				{
					return !base.IsOwnerControlDestroyed();
				}

				// Token: 0x06007597 RID: 30103 RVA: 0x001A99DC File Offset: 0x001A7BDC
				internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					if (AccessibilityImprovements.Level3)
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.Parent && this.propertyGridView.SelectedGridEntry != null)
						{
							return this.propertyGridView.SelectedGridEntry.AccessibilityObject;
						}
						if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
						{
							if (this.propertyGridView.DropDownButton.Visible)
							{
								return this.propertyGridView.DropDownButton.AccessibilityObject;
							}
							if (this.propertyGridView.DialogButton.Visible)
							{
								return this.propertyGridView.DialogButton.AccessibilityObject;
							}
						}
					}
					return base.FragmentNavigate(direction);
				}

				// Token: 0x17001AFD RID: 6909
				// (get) Token: 0x06007598 RID: 30104 RVA: 0x001A9A6C File Offset: 0x001A7C6C
				internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return null;
						}
						if (AccessibilityImprovements.Level3)
						{
							return this.propertyGridView.AccessibilityObject;
						}
						return base.FragmentRoot;
					}
				}

				// Token: 0x06007599 RID: 30105 RVA: 0x001A9A94 File Offset: 0x001A7C94
				internal override object GetPropertyValue(int propertyID)
				{
					if (propertyID == 30010)
					{
						return !this.IsReadOnly;
					}
					if (propertyID == 30043)
					{
						return this.IsPatternSupported(10002);
					}
					if (AccessibilityImprovements.Level3)
					{
						if (propertyID == 30003)
						{
							return 50004;
						}
						if (propertyID == 30005)
						{
							return this.Name;
						}
					}
					return base.GetPropertyValue(propertyID);
				}

				// Token: 0x0600759A RID: 30106 RVA: 0x000A8A81 File Offset: 0x000A6C81
				internal override bool IsPatternSupported(int patternId)
				{
					return !base.IsOwnerControlDestroyed() && (patternId == 10002 || base.IsPatternSupported(patternId));
				}

				// Token: 0x17001AFE RID: 6910
				// (get) Token: 0x0600759B RID: 30107 RVA: 0x001A9B04 File Offset: 0x001A7D04
				// (set) Token: 0x0600759C RID: 30108 RVA: 0x0001106B File Offset: 0x0000F26B
				public override string Name
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return string.Empty;
						}
						if (AccessibilityImprovements.Level3)
						{
							string accessibleName = base.Owner.AccessibleName;
							if (accessibleName != null)
							{
								return accessibleName;
							}
							GridEntry selectedGridEntry = this.propertyGridView.SelectedGridEntry;
							if (selectedGridEntry != null)
							{
								return selectedGridEntry.AccessibilityObject.Name;
							}
						}
						return base.Name;
					}
					set
					{
						base.Name = value;
					}
				}

				// Token: 0x17001AFF RID: 6911
				// (get) Token: 0x0600759D RID: 30109 RVA: 0x001A9B58 File Offset: 0x001A7D58
				internal override bool IsReadOnly
				{
					get
					{
						if (base.IsOwnerControlDestroyed())
						{
							return true;
						}
						PropertyDescriptorGridEntry propertyDescriptorGridEntry = this.propertyGridView.SelectedGridEntry as PropertyDescriptorGridEntry;
						return propertyDescriptorGridEntry == null || propertyDescriptorGridEntry.IsPropertyReadOnly;
					}
				}

				// Token: 0x0600759E RID: 30110 RVA: 0x001A9B8B File Offset: 0x001A7D8B
				internal override void SetFocus()
				{
					if (base.IsOwnerControlDestroyed())
					{
						return;
					}
					if (AccessibilityImprovements.Level3)
					{
						base.RaiseAutomationEvent(20005);
					}
					base.SetFocus();
				}

				// Token: 0x040047D4 RID: 18388
				private PropertyGridView propertyGridView;
			}
		}

		// Token: 0x02000881 RID: 2177
		internal class DropDownHolder : Form, PropertyGridView.IMouseHookClient
		{
			// Token: 0x060071A4 RID: 29092 RVA: 0x001A0244 File Offset: 0x0019E444
			internal DropDownHolder(PropertyGridView psheet)
			{
				this.MinDropDownSize = new Size(SystemInformation.VerticalScrollBarWidth * 4, SystemInformation.HorizontalScrollBarHeight * 4);
				this.ResizeGripSize = SystemInformation.HorizontalScrollBarHeight;
				this.ResizeBarSize = this.ResizeGripSize + 1;
				this.ResizeBorderSize = this.ResizeBarSize / 2;
				base.ShowInTaskbar = false;
				base.ControlBox = false;
				base.MinimizeBox = false;
				base.MaximizeBox = false;
				this.Text = "";
				base.FormBorderStyle = FormBorderStyle.None;
				base.AutoScaleMode = AutoScaleMode.None;
				this.mouseHook = new PropertyGridView.MouseHook(this, this, psheet);
				base.Visible = false;
				this.gridView = psheet;
				this.BackColor = this.gridView.BackColor;
			}

			// Token: 0x170018E3 RID: 6371
			// (get) Token: 0x060071A5 RID: 29093 RVA: 0x001A0318 File Offset: 0x0019E518
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.ExStyle |= 128;
					createParams.Style |= -2139095040;
					if (OSFeature.IsPresent(SystemParameter.DropShadow))
					{
						createParams.ClassStyle |= 131072;
					}
					if (this.gridView != null)
					{
						createParams.Parent = this.gridView.ParentInternal.Handle;
					}
					return createParams;
				}
			}

			// Token: 0x170018E4 RID: 6372
			// (get) Token: 0x060071A6 RID: 29094 RVA: 0x001A0389 File Offset: 0x0019E589
			private LinkLabel CreateNewLink
			{
				get
				{
					if (this.createNewLink == null)
					{
						this.createNewLink = new LinkLabel();
						this.createNewLink.LinkClicked += this.OnNewLinkClicked;
					}
					return this.createNewLink;
				}
			}

			// Token: 0x170018E5 RID: 6373
			// (get) Token: 0x060071A7 RID: 29095 RVA: 0x001A03BB File Offset: 0x0019E5BB
			// (set) Token: 0x060071A8 RID: 29096 RVA: 0x001A03C8 File Offset: 0x0019E5C8
			public virtual bool HookMouseDown
			{
				get
				{
					return this.mouseHook.HookMouseDown;
				}
				set
				{
					this.mouseHook.HookMouseDown = value;
				}
			}

			// Token: 0x170018E6 RID: 6374
			// (set) Token: 0x060071A9 RID: 29097 RVA: 0x001A03D8 File Offset: 0x0019E5D8
			public bool ResizeUp
			{
				set
				{
					if (this.resizeUp != value)
					{
						this.sizeGripGlyph = null;
						this.resizeUp = value;
						if (this.resizable)
						{
							base.DockPadding.Bottom = 0;
							base.DockPadding.Top = 0;
							if (value)
							{
								base.DockPadding.Top = this.ResizeBarSize;
								return;
							}
							base.DockPadding.Bottom = this.ResizeBarSize;
						}
					}
				}
			}

			// Token: 0x060071AA RID: 29098 RVA: 0x001A0442 File Offset: 0x0019E642
			protected override void DestroyHandle()
			{
				this.mouseHook.HookMouseDown = false;
				base.DestroyHandle();
			}

			// Token: 0x060071AB RID: 29099 RVA: 0x001A0456 File Offset: 0x0019E656
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.createNewLink != null)
				{
					this.createNewLink.Dispose();
					this.createNewLink = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x060071AC RID: 29100 RVA: 0x001A047C File Offset: 0x0019E67C
			public void DoModalLoop()
			{
				while (base.Visible)
				{
					Application.DoEventsModal();
					UnsafeNativeMethods.MsgWaitForMultipleObjectsEx(0, IntPtr.Zero, 250, 255, 4);
				}
			}

			// Token: 0x170018E7 RID: 6375
			// (get) Token: 0x060071AD RID: 29101 RVA: 0x001A04A4 File Offset: 0x0019E6A4
			public virtual Control Component
			{
				get
				{
					return this.currentControl;
				}
			}

			// Token: 0x060071AE RID: 29102 RVA: 0x001A04AC File Offset: 0x0019E6AC
			private InstanceCreationEditor GetInstanceCreationEditor(PropertyDescriptorGridEntry entry)
			{
				if (entry == null)
				{
					return null;
				}
				InstanceCreationEditor instanceCreationEditor = null;
				PropertyDescriptor propertyDescriptor = entry.PropertyDescriptor;
				if (propertyDescriptor != null)
				{
					instanceCreationEditor = (propertyDescriptor.GetEditor(typeof(InstanceCreationEditor)) as InstanceCreationEditor);
				}
				if (instanceCreationEditor == null)
				{
					UITypeEditor uitypeEditor = entry.UITypeEditor;
					if (uitypeEditor != null && uitypeEditor.GetEditStyle() == UITypeEditorEditStyle.DropDown)
					{
						instanceCreationEditor = (InstanceCreationEditor)TypeDescriptor.GetEditor(uitypeEditor, typeof(InstanceCreationEditor));
					}
				}
				return instanceCreationEditor;
			}

			// Token: 0x060071AF RID: 29103 RVA: 0x001A0510 File Offset: 0x0019E710
			private Bitmap GetSizeGripGlyph(Graphics g)
			{
				if (this.sizeGripGlyph != null)
				{
					return this.sizeGripGlyph;
				}
				this.sizeGripGlyph = new Bitmap(this.ResizeGripSize, this.ResizeGripSize, g);
				using (Graphics graphics = Graphics.FromImage(this.sizeGripGlyph))
				{
					Matrix matrix = new Matrix();
					matrix.Translate((float)(this.ResizeGripSize + 1), (float)(this.resizeUp ? (this.ResizeGripSize + 1) : 0));
					matrix.Scale(-1f, (float)(this.resizeUp ? -1 : 1));
					graphics.Transform = matrix;
					ControlPaint.DrawSizeGrip(graphics, this.BackColor, 0, 0, this.ResizeGripSize, this.ResizeGripSize);
					graphics.ResetTransform();
				}
				this.sizeGripGlyph.MakeTransparent(this.BackColor);
				return this.sizeGripGlyph;
			}

			// Token: 0x060071B0 RID: 29104 RVA: 0x001A05EC File Offset: 0x0019E7EC
			public virtual bool GetUsed()
			{
				return this.currentControl != null;
			}

			// Token: 0x060071B1 RID: 29105 RVA: 0x001A05F7 File Offset: 0x0019E7F7
			public virtual void FocusComponent()
			{
				if (this.currentControl != null && base.Visible)
				{
					this.currentControl.FocusInternal();
				}
			}

			// Token: 0x060071B2 RID: 29106 RVA: 0x001A0618 File Offset: 0x0019E818
			private bool OwnsWindow(IntPtr hWnd)
			{
				while (hWnd != IntPtr.Zero)
				{
					hWnd = UnsafeNativeMethods.GetWindowLong(new HandleRef(null, hWnd), -8);
					if (hWnd == IntPtr.Zero)
					{
						return false;
					}
					if (hWnd == base.Handle)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060071B3 RID: 29107 RVA: 0x001A0664 File Offset: 0x0019E864
			public bool OnClickHooked()
			{
				this.gridView.CloseDropDownInternal(false);
				return false;
			}

			// Token: 0x060071B4 RID: 29108 RVA: 0x001A0674 File Offset: 0x0019E874
			private void OnCurrentControlResize(object o, EventArgs e)
			{
				if (this.currentControl != null && !this.resizing)
				{
					int width = base.Width;
					Size size = new Size(2 + this.currentControl.Width, 2 + this.currentControl.Height);
					if (this.resizable)
					{
						size.Height += this.ResizeBarSize;
					}
					try
					{
						this.resizing = true;
						base.SuspendLayout();
						base.Size = size;
					}
					finally
					{
						this.resizing = false;
						base.ResumeLayout(false);
					}
					base.Left -= base.Width - width;
				}
			}

			// Token: 0x060071B5 RID: 29109 RVA: 0x001A0724 File Offset: 0x0019E924
			protected override void OnLayout(LayoutEventArgs levent)
			{
				try
				{
					this.resizing = true;
					base.OnLayout(levent);
				}
				finally
				{
					this.resizing = false;
				}
			}

			// Token: 0x060071B6 RID: 29110 RVA: 0x001A075C File Offset: 0x0019E95C
			private void OnNewLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
			{
				InstanceCreationEditor instanceCreationEditor = e.Link.LinkData as InstanceCreationEditor;
				if (instanceCreationEditor != null)
				{
					PropertyGridView propertyGridView = this.gridView;
					if (((propertyGridView != null) ? propertyGridView.SelectedGridEntry : null) != null)
					{
						Type propertyType = this.gridView.SelectedGridEntry.PropertyType;
						if (propertyType != null)
						{
							this.gridView.CloseDropDown();
							object obj = instanceCreationEditor.CreateInstance(this.gridView.SelectedGridEntry, propertyType);
							if (obj != null)
							{
								if (!propertyType.IsInstanceOfType(obj))
								{
									throw new InvalidCastException(SR.GetString("PropertyGridViewEditorCreatedInvalidObject", new object[]
									{
										propertyType
									}));
								}
								this.gridView.CommitValue(obj);
							}
						}
					}
				}
			}

			// Token: 0x060071B7 RID: 29111 RVA: 0x001A07FC File Offset: 0x0019E9FC
			private int MoveTypeFromPoint(int x, int y)
			{
				Rectangle rectangle = new Rectangle(0, base.Height - this.ResizeGripSize, this.ResizeGripSize, this.ResizeGripSize);
				Rectangle rectangle2 = new Rectangle(0, 0, this.ResizeGripSize, this.ResizeGripSize);
				if (!this.resizeUp && rectangle.Contains(x, y))
				{
					return 3;
				}
				if (this.resizeUp && rectangle2.Contains(x, y))
				{
					return 6;
				}
				if (!this.resizeUp && Math.Abs(base.Height - y) < this.ResizeBorderSize)
				{
					return 1;
				}
				if (this.resizeUp && Math.Abs(y) < this.ResizeBorderSize)
				{
					return 4;
				}
				return 0;
			}

			// Token: 0x060071B8 RID: 29112 RVA: 0x001A08A4 File Offset: 0x0019EAA4
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					this.currentMoveType = this.MoveTypeFromPoint(e.X, e.Y);
					if (this.currentMoveType != 0)
					{
						this.dragStart = base.PointToScreen(new Point(e.X, e.Y));
						this.dragBaseRect = base.Bounds;
						base.Capture = true;
					}
					else
					{
						this.gridView.CloseDropDown();
					}
				}
				base.OnMouseDown(e);
			}

			// Token: 0x060071B9 RID: 29113 RVA: 0x001A0924 File Offset: 0x0019EB24
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (this.currentMoveType == 0)
				{
					switch (this.MoveTypeFromPoint(e.X, e.Y))
					{
					case 1:
					case 4:
						this.Cursor = Cursors.SizeNS;
						goto IL_1CB;
					case 3:
						this.Cursor = Cursors.SizeNESW;
						goto IL_1CB;
					case 6:
						this.Cursor = Cursors.SizeNWSE;
						goto IL_1CB;
					}
					this.Cursor = null;
				}
				else
				{
					Point point = base.PointToScreen(new Point(e.X, e.Y));
					Rectangle bounds = base.Bounds;
					if ((this.currentMoveType & 1) == 1)
					{
						bounds.Height = Math.Max(this.MinDropDownSize.Height, this.dragBaseRect.Height + (point.Y - this.dragStart.Y));
					}
					if ((this.currentMoveType & 4) == 4)
					{
						int num = point.Y - this.dragStart.Y;
						if (this.dragBaseRect.Height - num > this.MinDropDownSize.Height)
						{
							bounds.Y = this.dragBaseRect.Top + num;
							bounds.Height = this.dragBaseRect.Height - num;
						}
					}
					if ((this.currentMoveType & 2) == 2)
					{
						int num2 = point.X - this.dragStart.X;
						if (this.dragBaseRect.Width - num2 > this.MinDropDownSize.Width)
						{
							bounds.X = this.dragBaseRect.Left + num2;
							bounds.Width = this.dragBaseRect.Width - num2;
						}
					}
					if (bounds != base.Bounds)
					{
						try
						{
							this.resizing = true;
							base.Bounds = bounds;
						}
						finally
						{
							this.resizing = false;
						}
					}
					base.Invalidate();
				}
				IL_1CB:
				base.OnMouseMove(e);
			}

			// Token: 0x060071BA RID: 29114 RVA: 0x001A0B14 File Offset: 0x0019ED14
			protected override void OnMouseLeave(EventArgs e)
			{
				this.Cursor = null;
				base.OnMouseLeave(e);
			}

			// Token: 0x060071BB RID: 29115 RVA: 0x001A0B24 File Offset: 0x0019ED24
			protected override void OnMouseUp(MouseEventArgs e)
			{
				base.OnMouseUp(e);
				if (e.Button == MouseButtons.Left)
				{
					this.currentMoveType = 0;
					this.dragStart = Point.Empty;
					this.dragBaseRect = Rectangle.Empty;
					base.Capture = false;
				}
			}

			// Token: 0x060071BC RID: 29116 RVA: 0x001A0B60 File Offset: 0x0019ED60
			protected override void OnPaint(PaintEventArgs pe)
			{
				base.OnPaint(pe);
				if (this.resizable)
				{
					Rectangle rect = new Rectangle(0, this.resizeUp ? 0 : (base.Height - this.ResizeGripSize), this.ResizeGripSize, this.ResizeGripSize);
					pe.Graphics.DrawImage(this.GetSizeGripGlyph(pe.Graphics), rect);
					int num = this.resizeUp ? (this.ResizeBarSize - 1) : (base.Height - this.ResizeBarSize);
					Pen pen = new Pen(SystemColors.ControlDark, 1f);
					pen.DashStyle = DashStyle.Solid;
					pe.Graphics.DrawLine(pen, 0, num, base.Width, num);
					pen.Dispose();
				}
			}

			// Token: 0x060071BD RID: 29117 RVA: 0x001A0C18 File Offset: 0x0019EE18
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if ((keyData & (Keys.Shift | Keys.Control | Keys.Alt)) == Keys.None)
				{
					Keys keys = keyData & Keys.KeyCode;
					if (keys == Keys.Return)
					{
						if (this.gridView.UnfocusSelection() && this.gridView.SelectedGridEntry != null)
						{
							this.gridView.SelectedGridEntry.OnValueReturnKey();
						}
						return true;
					}
					if (keys == Keys.Escape)
					{
						this.gridView.OnEscape(this);
						return true;
					}
					if (keys == Keys.F4)
					{
						this.gridView.F4Selection(true);
						return true;
					}
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x060071BE RID: 29118 RVA: 0x001A0C98 File Offset: 0x0019EE98
			public void SetComponent(Control ctl, bool resizable)
			{
				this.resizable = resizable;
				this.Font = this.gridView.Font;
				InstanceCreationEditor instanceCreationEditor = (ctl == null) ? null : this.GetInstanceCreationEditor(this.gridView.SelectedGridEntry as PropertyDescriptorGridEntry);
				if (this.currentControl != null)
				{
					this.currentControl.Resize -= this.OnCurrentControlResize;
					base.Controls.Remove(this.currentControl);
					this.currentControl = null;
				}
				if (this.createNewLink != null && this.createNewLink.Parent == this)
				{
					base.Controls.Remove(this.createNewLink);
				}
				if (ctl != null)
				{
					this.currentControl = ctl;
					base.DockPadding.All = 0;
					if (this.currentControl is PropertyGridView.GridViewListBox)
					{
						ListBox listBox = (ListBox)this.currentControl;
						if (listBox.Items.Count == 0)
						{
							listBox.Height = Math.Max(listBox.Height, listBox.ItemHeight);
						}
					}
					try
					{
						base.SuspendLayout();
						base.Controls.Add(ctl);
						Size size = new Size(2 + ctl.Width, 2 + ctl.Height);
						if (instanceCreationEditor != null)
						{
							this.CreateNewLink.Text = instanceCreationEditor.Text;
							this.CreateNewLink.Links.Clear();
							this.CreateNewLink.Links.Add(0, instanceCreationEditor.Text.Length, instanceCreationEditor);
							int num = this.CreateNewLink.Height;
							using (Graphics graphics = this.gridView.CreateGraphics())
							{
								num = (int)PropertyGrid.MeasureTextHelper.MeasureText(this.gridView.ownerGrid, graphics, instanceCreationEditor.Text, this.gridView.GetBaseFont()).Height;
							}
							this.CreateNewLink.Height = num + 1;
							size.Height += num + 2;
						}
						if (resizable)
						{
							size.Height += this.ResizeBarSize;
							if (this.resizeUp)
							{
								base.DockPadding.Top = this.ResizeBarSize;
							}
							else
							{
								base.DockPadding.Bottom = this.ResizeBarSize;
							}
						}
						base.Size = size;
						if (DpiHelper.EnableDpiChangedHighDpiImprovements)
						{
							ctl.Visible = true;
							if (base.Size.Height < base.PreferredSize.Height)
							{
								base.Size = new Size(base.Size.Width, base.PreferredSize.Height);
							}
							ctl.Dock = DockStyle.Fill;
						}
						else
						{
							ctl.Dock = DockStyle.Fill;
							ctl.Visible = true;
						}
						if (instanceCreationEditor != null)
						{
							this.CreateNewLink.Dock = DockStyle.Bottom;
							base.Controls.Add(this.CreateNewLink);
						}
					}
					finally
					{
						base.ResumeLayout(true);
					}
					this.currentControl.Resize += this.OnCurrentControlResize;
				}
				base.Enabled = (this.currentControl != null);
			}

			// Token: 0x060071BF RID: 29119 RVA: 0x001A0FB0 File Offset: 0x0019F1B0
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 6)
				{
					base.SetState(32, true);
					IntPtr lparam = m.LParam;
					if (base.Visible && NativeMethods.Util.LOWORD(m.WParam) == 0 && !this.OwnsWindow(lparam))
					{
						this.gridView.CloseDropDownInternal(false);
						return;
					}
				}
				else
				{
					if (m.Msg == 16)
					{
						if (base.Visible)
						{
							this.gridView.CloseDropDown();
						}
						return;
					}
					if (m.Msg == 736 && DpiHelper.EnableDpiChangedHighDpiImprovements)
					{
						int deviceDpi = this.deviceDpi;
						this.deviceDpi = (int)UnsafeNativeMethods.GetDpiForWindow(new HandleRef(this, base.HandleInternal));
						if (deviceDpi != this.deviceDpi)
						{
							this.RescaleConstantsForDpi(deviceDpi, this.deviceDpi);
							base.PerformLayout();
						}
						m.Result = IntPtr.Zero;
						return;
					}
				}
				base.WndProc(ref m);
			}

			// Token: 0x060071C0 RID: 29120 RVA: 0x001A108C File Offset: 0x0019F28C
			protected override void RescaleConstantsForDpi(int oldDpi, int newDpi)
			{
				base.RescaleConstantsForDpi(oldDpi, newDpi);
				if (!DpiHelper.EnableDpiChangedHighDpiImprovements)
				{
					return;
				}
				int horizontalScrollBarHeightForDpi = SystemInformation.GetHorizontalScrollBarHeightForDpi(newDpi);
				this.MinDropDownSize = new Size(SystemInformation.GetVerticalScrollBarWidthForDpi(newDpi) * 4, horizontalScrollBarHeightForDpi * 4);
				this.ResizeGripSize = horizontalScrollBarHeightForDpi;
				this.ResizeBarSize = this.ResizeGripSize + 1;
				this.ResizeBorderSize = this.ResizeBarSize / 2;
				double num = (double)newDpi / (double)oldDpi;
				base.Height = (int)Math.Round(num * (double)base.Height);
			}

			// Token: 0x04004484 RID: 17540
			private Control currentControl;

			// Token: 0x04004485 RID: 17541
			private PropertyGridView gridView;

			// Token: 0x04004486 RID: 17542
			private PropertyGridView.MouseHook mouseHook;

			// Token: 0x04004487 RID: 17543
			private LinkLabel createNewLink;

			// Token: 0x04004488 RID: 17544
			private bool resizable = true;

			// Token: 0x04004489 RID: 17545
			private bool resizing;

			// Token: 0x0400448A RID: 17546
			private bool resizeUp;

			// Token: 0x0400448B RID: 17547
			private Point dragStart = Point.Empty;

			// Token: 0x0400448C RID: 17548
			private Rectangle dragBaseRect = Rectangle.Empty;

			// Token: 0x0400448D RID: 17549
			private int currentMoveType;

			// Token: 0x0400448E RID: 17550
			private int ResizeBarSize;

			// Token: 0x0400448F RID: 17551
			private int ResizeBorderSize;

			// Token: 0x04004490 RID: 17552
			private int ResizeGripSize;

			// Token: 0x04004491 RID: 17553
			private Size MinDropDownSize;

			// Token: 0x04004492 RID: 17554
			private Bitmap sizeGripGlyph;

			// Token: 0x04004493 RID: 17555
			private const int DropDownHolderBorder = 1;

			// Token: 0x04004494 RID: 17556
			private const int MoveTypeNone = 0;

			// Token: 0x04004495 RID: 17557
			private const int MoveTypeBottom = 1;

			// Token: 0x04004496 RID: 17558
			private const int MoveTypeLeft = 2;

			// Token: 0x04004497 RID: 17559
			private const int MoveTypeTop = 4;
		}

		// Token: 0x02000882 RID: 2178
		internal class GridViewListBox : ListBox
		{
			// Token: 0x060071C1 RID: 29121 RVA: 0x001A1104 File Offset: 0x0019F304
			public GridViewListBox(PropertyGridView gridView)
			{
				base.IntegralHeight = false;
				this._owningPropertyGridView = gridView;
				base.BackColor = gridView.BackColor;
			}

			// Token: 0x170018E8 RID: 6376
			// (get) Token: 0x060071C2 RID: 29122 RVA: 0x001A1128 File Offset: 0x0019F328
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style &= -8388609;
					createParams.ExStyle &= -513;
					return createParams;
				}
			}

			// Token: 0x170018E9 RID: 6377
			// (get) Token: 0x060071C3 RID: 29123 RVA: 0x001A1161 File Offset: 0x0019F361
			internal PropertyGridView OwningPropertyGridView
			{
				get
				{
					return this._owningPropertyGridView;
				}
			}

			// Token: 0x170018EA RID: 6378
			// (get) Token: 0x060071C4 RID: 29124 RVA: 0x000A8615 File Offset: 0x000A6815
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x060071C5 RID: 29125 RVA: 0x001A1169 File Offset: 0x0019F369
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level3)
				{
					return new PropertyGridView.GridViewListBoxAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x060071C6 RID: 29126 RVA: 0x001A117F File Offset: 0x0019F37F
			public virtual bool InSetSelectedIndex()
			{
				return this.fInSetSelectedIndex;
			}

			// Token: 0x060071C7 RID: 29127 RVA: 0x001A1188 File Offset: 0x0019F388
			protected override void OnSelectedIndexChanged(EventArgs e)
			{
				this.fInSetSelectedIndex = true;
				base.OnSelectedIndexChanged(e);
				this.fInSetSelectedIndex = false;
				PropertyGridView.GridViewListBoxAccessibleObject gridViewListBoxAccessibleObject = base.AccessibilityObject as PropertyGridView.GridViewListBoxAccessibleObject;
				if (gridViewListBoxAccessibleObject != null)
				{
					gridViewListBoxAccessibleObject.SetListBoxItemFocus();
				}
			}

			// Token: 0x04004498 RID: 17560
			internal bool fInSetSelectedIndex;

			// Token: 0x04004499 RID: 17561
			private PropertyGridView _owningPropertyGridView;
		}

		// Token: 0x02000883 RID: 2179
		[ComVisible(true)]
		private class GridViewListBoxItemAccessibleObject : AccessibleObject
		{
			// Token: 0x060071C8 RID: 29128 RVA: 0x001A11BF File Offset: 0x0019F3BF
			public GridViewListBoxItemAccessibleObject(PropertyGridView.GridViewListBox owningGridViewListBox, object owningItem)
			{
				this._owningGridViewListBox = owningGridViewListBox;
				this._owningItem = owningItem;
				base.UseStdAccessibleObjects(this._owningGridViewListBox.Handle);
			}

			// Token: 0x060071C9 RID: 29129 RVA: 0x001A11E6 File Offset: 0x0019F3E6
			public void ClearOwnerGridViewListBox()
			{
				this._owningGridViewListBox = null;
				this._owningItem = null;
			}

			// Token: 0x060071CA RID: 29130 RVA: 0x001A11F6 File Offset: 0x0019F3F6
			internal bool IsOwnerGridViewListBoxDestroyed()
			{
				return LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this._owningGridViewListBox == null;
			}

			// Token: 0x170018EB RID: 6379
			// (get) Token: 0x060071CB RID: 29131 RVA: 0x001A120C File Offset: 0x0019F40C
			public override Rectangle Bounds
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					int x;
					int y;
					int width;
					int height;
					systemIAccessibleInternal.accLocation(out x, out y, out width, out height, this.GetChildId());
					return new Rectangle(x, y, width, height);
				}
			}

			// Token: 0x170018EC RID: 6380
			// (get) Token: 0x060071CC RID: 29132 RVA: 0x001A1244 File Offset: 0x0019F444
			public override string DefaultAction
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accDefaultAction(this.GetChildId());
				}
			}

			// Token: 0x060071CD RID: 29133 RVA: 0x001A126C File Offset: 0x0019F46C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (this.IsOwnerGridViewListBoxDestroyed())
				{
					return null;
				}
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.Parent:
					return this._owningGridViewListBox.AccessibilityObject;
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					int currentIndex = this.GetCurrentIndex();
					PropertyGridView.GridViewListBoxAccessibleObject gridViewListBoxAccessibleObject = this._owningGridViewListBox.AccessibilityObject as PropertyGridView.GridViewListBoxAccessibleObject;
					if (gridViewListBoxAccessibleObject != null)
					{
						int childFragmentCount = gridViewListBoxAccessibleObject.GetChildFragmentCount();
						int num = currentIndex + 1;
						if (childFragmentCount > num)
						{
							return gridViewListBoxAccessibleObject.GetChildFragment(num);
						}
					}
					break;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					int currentIndex = this.GetCurrentIndex();
					PropertyGridView.GridViewListBoxAccessibleObject gridViewListBoxAccessibleObject = this._owningGridViewListBox.AccessibilityObject as PropertyGridView.GridViewListBoxAccessibleObject;
					if (gridViewListBoxAccessibleObject != null)
					{
						int childFragmentCount2 = gridViewListBoxAccessibleObject.GetChildFragmentCount();
						int num2 = currentIndex - 1;
						if (num2 >= 0)
						{
							return gridViewListBoxAccessibleObject.GetChildFragment(num2);
						}
					}
					break;
				}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170018ED RID: 6381
			// (get) Token: 0x060071CE RID: 29134 RVA: 0x001A1312 File Offset: 0x0019F512
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (!this.IsOwnerGridViewListBoxDestroyed())
					{
						return this._owningGridViewListBox.AccessibilityObject;
					}
					return null;
				}
			}

			// Token: 0x060071CF RID: 29135 RVA: 0x001A1329 File Offset: 0x0019F529
			private int GetCurrentIndex()
			{
				if (!this.IsOwnerGridViewListBoxDestroyed())
				{
					return this._owningGridViewListBox.Items.IndexOf(this._owningItem);
				}
				return -1;
			}

			// Token: 0x060071D0 RID: 29136 RVA: 0x001A134B File Offset: 0x0019F54B
			internal override int GetChildId()
			{
				return this.GetCurrentIndex() + 1;
			}

			// Token: 0x060071D1 RID: 29137 RVA: 0x001A1358 File Offset: 0x0019F558
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
					return 50007;
				case 30005:
					return this.Name;
				case 30007:
					return this.KeyboardShortcut;
				case 30008:
					return !this.IsOwnerGridViewListBoxDestroyed() && this._owningGridViewListBox.Focused;
				case 30009:
					return (this.State & AccessibleStates.Focusable) == AccessibleStates.Focusable;
				case 30010:
					return !this.IsOwnerGridViewListBoxDestroyed() && this._owningGridViewListBox.Enabled;
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

			// Token: 0x170018EE RID: 6382
			// (get) Token: 0x060071D2 RID: 29138 RVA: 0x001A1478 File Offset: 0x0019F678
			public override string Help
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accHelp(this.GetChildId());
				}
			}

			// Token: 0x170018EF RID: 6383
			// (get) Token: 0x060071D3 RID: 29139 RVA: 0x001A14A0 File Offset: 0x0019F6A0
			public override string KeyboardShortcut
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return systemIAccessibleInternal.get_accKeyboardShortcut(this.GetChildId());
				}
			}

			// Token: 0x060071D4 RID: 29140 RVA: 0x001A14C5 File Offset: 0x0019F6C5
			internal override bool IsPatternSupported(int patternId)
			{
				return !this.IsOwnerGridViewListBoxDestroyed() && (patternId == 10018 || patternId == 10000 || base.IsPatternSupported(patternId));
			}

			// Token: 0x170018F0 RID: 6384
			// (get) Token: 0x060071D5 RID: 29141 RVA: 0x001A14EA File Offset: 0x0019F6EA
			// (set) Token: 0x060071D6 RID: 29142 RVA: 0x0017012F File Offset: 0x0016E32F
			public override string Name
			{
				get
				{
					if (this._owningGridViewListBox != null)
					{
						return this._owningItem.ToString();
					}
					return base.Name;
				}
				set
				{
					base.Name = value;
				}
			}

			// Token: 0x170018F1 RID: 6385
			// (get) Token: 0x060071D7 RID: 29143 RVA: 0x001A1508 File Offset: 0x0019F708
			public override AccessibleRole Role
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleRole)systemIAccessibleInternal.get_accRole(this.GetChildId());
				}
			}

			// Token: 0x170018F2 RID: 6386
			// (get) Token: 0x060071D8 RID: 29144 RVA: 0x001A1534 File Offset: 0x0019F734
			internal override int[] RuntimeId
			{
				get
				{
					int[] array = new int[3];
					array[0] = 42;
					if (this.IsOwnerGridViewListBoxDestroyed())
					{
						array[1] = 0;
						array[2] = 0;
					}
					else
					{
						array[1] = (int)((long)this._owningGridViewListBox.Handle);
						array[2] = this._owningItem.GetHashCode();
					}
					return array;
				}
			}

			// Token: 0x170018F3 RID: 6387
			// (get) Token: 0x060071D9 RID: 29145 RVA: 0x001A1584 File Offset: 0x0019F784
			public override AccessibleStates State
			{
				get
				{
					IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
					return (AccessibleStates)systemIAccessibleInternal.get_accState(this.GetChildId());
				}
			}

			// Token: 0x060071DA RID: 29146 RVA: 0x001A15AE File Offset: 0x0019F7AE
			internal override void SetFocus()
			{
				if (this.IsOwnerGridViewListBoxDestroyed())
				{
					return;
				}
				base.RaiseAutomationEvent(20005);
				base.SetFocus();
			}

			// Token: 0x0400449A RID: 17562
			private PropertyGridView.GridViewListBox _owningGridViewListBox;

			// Token: 0x0400449B RID: 17563
			private object _owningItem;
		}

		// Token: 0x02000884 RID: 2180
		private class GridViewListBoxItemAccessibleObjectCollection : Hashtable
		{
			// Token: 0x060071DB RID: 29147 RVA: 0x001A15CB File Offset: 0x0019F7CB
			public GridViewListBoxItemAccessibleObjectCollection(PropertyGridView.GridViewListBox owningGridViewListBox)
			{
				this._owningGridViewListBox = owningGridViewListBox;
			}

			// Token: 0x060071DC RID: 29148 RVA: 0x001A15DA File Offset: 0x0019F7DA
			public void ClearOwnerGridViewListBox()
			{
				this._owningGridViewListBox = null;
			}

			// Token: 0x170018F4 RID: 6388
			public override object this[object key]
			{
				get
				{
					if (LocalAppContextSwitches.FreeControlsForRefCountedAccessibleObjectsInLevel5 && this._owningGridViewListBox == null)
					{
						return null;
					}
					if (!this.ContainsKey(key))
					{
						PropertyGridView.GridViewListBoxItemAccessibleObject value = new PropertyGridView.GridViewListBoxItemAccessibleObject(this._owningGridViewListBox, key);
						base[key] = value;
					}
					return base[key];
				}
				set
				{
					base[key] = value;
				}
			}

			// Token: 0x0400449C RID: 17564
			private PropertyGridView.GridViewListBox _owningGridViewListBox;
		}

		// Token: 0x02000885 RID: 2181
		[ComVisible(true)]
		private class GridViewListBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060071DF RID: 29151 RVA: 0x001A1631 File Offset: 0x0019F831
			public GridViewListBoxAccessibleObject(PropertyGridView.GridViewListBox owningGridViewListBox) : base(owningGridViewListBox)
			{
				this._owningGridViewListBox = owningGridViewListBox;
				this._owningPropertyGridView = owningGridViewListBox.OwningPropertyGridView;
				this._itemAccessibleObjects = new PropertyGridView.GridViewListBoxItemAccessibleObjectCollection(owningGridViewListBox);
			}

			// Token: 0x060071E0 RID: 29152 RVA: 0x001A165C File Offset: 0x0019F85C
			internal override void ClearOwnerControlInternal()
			{
				if (this._itemAccessibleObjects != null)
				{
					if (this._owningGridViewListBox != null)
					{
						foreach (object obj in this._owningGridViewListBox.Items)
						{
							if (obj != null)
							{
								PropertyGridView.GridViewListBoxItemAccessibleObject gridViewListBoxItemAccessibleObject = this._itemAccessibleObjects[obj] as PropertyGridView.GridViewListBoxItemAccessibleObject;
								if (gridViewListBoxItemAccessibleObject != null)
								{
									gridViewListBoxItemAccessibleObject.ClearOwnerGridViewListBox();
								}
							}
						}
					}
					this._itemAccessibleObjects.ClearOwnerGridViewListBox();
					this._itemAccessibleObjects.Clear();
					this._itemAccessibleObjects = null;
				}
				this._owningGridViewListBox = null;
				this._owningPropertyGridView = null;
				base.ClearOwnerControlInternal();
			}

			// Token: 0x060071E1 RID: 29153 RVA: 0x001A1710 File Offset: 0x0019F910
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (AccessibilityImprovements.Level5)
				{
					if (!this._owningPropertyGridView.DropDownVisible || this._owningPropertyGridView.DropDownControlHolder.Component != this._owningGridViewListBox)
					{
						return null;
					}
					GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
					PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject propertyDescriptorGridEntryAccessibleObject = ((selectedGridEntry != null) ? selectedGridEntry.AccessibilityObject : null) as PropertyDescriptorGridEntry.PropertyDescriptorGridEntryAccessibleObject;
					if (propertyDescriptorGridEntryAccessibleObject == null)
					{
						return null;
					}
					switch (direction)
					{
					case UnsafeNativeMethods.NavigateDirection.Parent:
						return propertyDescriptorGridEntryAccessibleObject;
					case UnsafeNativeMethods.NavigateDirection.NextSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetNextChildFragment(this);
					case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
						return propertyDescriptorGridEntryAccessibleObject.GetPreviousChildFragment(this);
					}
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent && this._owningPropertyGridView.SelectedGridEntry != null)
				{
					return this._owningPropertyGridView.SelectedGridEntry.AccessibilityObject;
				}
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
				else if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					return this._owningPropertyGridView.Edit.AccessibilityObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170018F5 RID: 6389
			// (get) Token: 0x060071E2 RID: 29154 RVA: 0x001A17FF File Offset: 0x0019F9FF
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					return this._owningPropertyGridView.AccessibilityObject;
				}
			}

			// Token: 0x060071E3 RID: 29155 RVA: 0x001A1818 File Offset: 0x0019FA18
			public AccessibleObject GetChildFragment(int index)
			{
				if (base.IsOwnerControlDestroyed() || index < 0 || index >= this._owningGridViewListBox.Items.Count)
				{
					return null;
				}
				object key = this._owningGridViewListBox.Items[index];
				return this._itemAccessibleObjects[key] as AccessibleObject;
			}

			// Token: 0x060071E4 RID: 29156 RVA: 0x001A1869 File Offset: 0x0019FA69
			public int GetChildFragmentCount()
			{
				if (!base.IsOwnerControlDestroyed())
				{
					return this._owningGridViewListBox.Items.Count;
				}
				return 0;
			}

			// Token: 0x060071E5 RID: 29157 RVA: 0x001A1885 File Offset: 0x0019FA85
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50008;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x060071E6 RID: 29158 RVA: 0x0015F2B1 File Offset: 0x0015D4B1
			internal override void SetFocus()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				base.RaiseAutomationEvent(20005);
				base.SetFocus();
			}

			// Token: 0x060071E7 RID: 29159 RVA: 0x001A18B0 File Offset: 0x0019FAB0
			internal void SetListBoxItemFocus()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				object selectedItem = this._owningGridViewListBox.SelectedItem;
				if (selectedItem != null)
				{
					AccessibleObject accessibleObject = this._itemAccessibleObjects[selectedItem] as AccessibleObject;
					if (accessibleObject != null)
					{
						accessibleObject.SetFocus();
					}
				}
			}

			// Token: 0x0400449D RID: 17565
			private PropertyGridView.GridViewListBox _owningGridViewListBox;

			// Token: 0x0400449E RID: 17566
			private PropertyGridView _owningPropertyGridView;

			// Token: 0x0400449F RID: 17567
			private PropertyGridView.GridViewListBoxItemAccessibleObjectCollection _itemAccessibleObjects;
		}

		// Token: 0x02000886 RID: 2182
		internal interface IMouseHookClient
		{
			// Token: 0x060071E8 RID: 29160
			bool OnClickHooked();
		}

		// Token: 0x02000887 RID: 2183
		internal class MouseHook
		{
			// Token: 0x060071E9 RID: 29161 RVA: 0x001A18F0 File Offset: 0x0019FAF0
			public MouseHook(Control control, PropertyGridView.IMouseHookClient client, PropertyGridView gridView)
			{
				this.control = control;
				this.gridView = gridView;
				this.client = client;
			}

			// Token: 0x170018F6 RID: 6390
			// (set) Token: 0x060071EA RID: 29162 RVA: 0x001A1918 File Offset: 0x0019FB18
			public bool DisableMouseHook
			{
				set
				{
					this.hookDisable = value;
					if (value)
					{
						this.UnhookMouse();
					}
				}
			}

			// Token: 0x170018F7 RID: 6391
			// (get) Token: 0x060071EB RID: 29163 RVA: 0x001A192A File Offset: 0x0019FB2A
			// (set) Token: 0x060071EC RID: 29164 RVA: 0x001A1942 File Offset: 0x0019FB42
			public virtual bool HookMouseDown
			{
				get
				{
					GC.KeepAlive(this);
					return this.mouseHookHandle != IntPtr.Zero;
				}
				set
				{
					if (value && !this.hookDisable)
					{
						this.HookMouse();
						return;
					}
					this.UnhookMouse();
				}
			}

			// Token: 0x060071ED RID: 29165 RVA: 0x001A195C File Offset: 0x0019FB5C
			public void Dispose()
			{
				this.UnhookMouse();
			}

			// Token: 0x060071EE RID: 29166 RVA: 0x001A1964 File Offset: 0x0019FB64
			private void HookMouse()
			{
				GC.KeepAlive(this);
				lock (this)
				{
					if (!(this.mouseHookHandle != IntPtr.Zero))
					{
						if (this.thisProcessID == 0)
						{
							SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this.control, this.control.Handle), out this.thisProcessID);
						}
						NativeMethods.HookProc hookProc = new NativeMethods.HookProc(new PropertyGridView.MouseHook.MouseHookObject(this).Callback);
						this.mouseHookRoot = GCHandle.Alloc(hookProc);
						this.mouseHookHandle = UnsafeNativeMethods.SetWindowsHookEx(7, hookProc, NativeMethods.NullHandleRef, SafeNativeMethods.GetCurrentThreadId());
					}
				}
			}

			// Token: 0x060071EF RID: 29167 RVA: 0x001A1A14 File Offset: 0x0019FC14
			private IntPtr MouseHookProc(int nCode, IntPtr wparam, IntPtr lparam)
			{
				GC.KeepAlive(this);
				if (nCode == 0)
				{
					NativeMethods.MOUSEHOOKSTRUCT mousehookstruct = (NativeMethods.MOUSEHOOKSTRUCT)UnsafeNativeMethods.PtrToStructure(lparam, typeof(NativeMethods.MOUSEHOOKSTRUCT));
					if (mousehookstruct != null)
					{
						int num = (int)((long)wparam);
						if (num <= 164)
						{
							if (num != 33 && num != 161 && num != 164)
							{
								goto IL_97;
							}
						}
						else if (num <= 513)
						{
							if (num != 167 && num != 513)
							{
								goto IL_97;
							}
						}
						else if (num != 516 && num != 519)
						{
							goto IL_97;
						}
						if (this.ProcessMouseDown(mousehookstruct.hWnd, mousehookstruct.pt_x, mousehookstruct.pt_y))
						{
							return (IntPtr)1;
						}
					}
				}
				IL_97:
				return UnsafeNativeMethods.CallNextHookEx(new HandleRef(this, this.mouseHookHandle), nCode, wparam, lparam);
			}

			// Token: 0x060071F0 RID: 29168 RVA: 0x001A1ACC File Offset: 0x0019FCCC
			private void UnhookMouse()
			{
				GC.KeepAlive(this);
				lock (this)
				{
					if (this.mouseHookHandle != IntPtr.Zero)
					{
						UnsafeNativeMethods.UnhookWindowsHookEx(new HandleRef(this, this.mouseHookHandle));
						this.mouseHookRoot.Free();
						this.mouseHookHandle = IntPtr.Zero;
					}
				}
			}

			// Token: 0x060071F1 RID: 29169 RVA: 0x001A1B44 File Offset: 0x0019FD44
			private bool ProcessMouseDown(IntPtr hWnd, int x, int y)
			{
				if (this.processing)
				{
					return false;
				}
				IntPtr handle = this.control.Handle;
				Control control = Control.FromHandleInternal(hWnd);
				if (hWnd != handle && !this.control.Contains(control))
				{
					int num;
					SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(null, hWnd), out num);
					if (num != this.thisProcessID)
					{
						this.HookMouseDown = false;
						return false;
					}
					bool flag = control == null || !this.gridView.IsSiblingControl(this.control, control);
					try
					{
						this.processing = true;
						if (flag && this.client.OnClickHooked())
						{
							return true;
						}
					}
					finally
					{
						this.processing = false;
					}
					this.HookMouseDown = false;
					return false;
				}
				return false;
			}

			// Token: 0x040044A0 RID: 17568
			private PropertyGridView gridView;

			// Token: 0x040044A1 RID: 17569
			private Control control;

			// Token: 0x040044A2 RID: 17570
			private PropertyGridView.IMouseHookClient client;

			// Token: 0x040044A3 RID: 17571
			internal int thisProcessID;

			// Token: 0x040044A4 RID: 17572
			private GCHandle mouseHookRoot;

			// Token: 0x040044A5 RID: 17573
			private IntPtr mouseHookHandle = IntPtr.Zero;

			// Token: 0x040044A6 RID: 17574
			private bool hookDisable;

			// Token: 0x040044A7 RID: 17575
			private bool processing;

			// Token: 0x02000980 RID: 2432
			private class MouseHookObject
			{
				// Token: 0x0600759F RID: 30111 RVA: 0x001A9BAF File Offset: 0x001A7DAF
				public MouseHookObject(PropertyGridView.MouseHook parent)
				{
					this.reference = new WeakReference(parent, false);
				}

				// Token: 0x060075A0 RID: 30112 RVA: 0x001A9BC4 File Offset: 0x001A7DC4
				public virtual IntPtr Callback(int nCode, IntPtr wparam, IntPtr lparam)
				{
					IntPtr result = IntPtr.Zero;
					try
					{
						PropertyGridView.MouseHook mouseHook = (PropertyGridView.MouseHook)this.reference.Target;
						if (mouseHook != null)
						{
							result = mouseHook.MouseHookProc(nCode, wparam, lparam);
						}
					}
					catch
					{
					}
					return result;
				}

				// Token: 0x040047D5 RID: 18389
				internal WeakReference reference;
			}
		}

		// Token: 0x02000888 RID: 2184
		[ComVisible(true)]
		internal class PropertyGridViewAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060071F2 RID: 29170 RVA: 0x001A1C0C File Offset: 0x0019FE0C
			public PropertyGridViewAccessibleObject(PropertyGridView owner, PropertyGrid parentPropertyGrid) : base(owner)
			{
				this._owningPropertyGridView = owner;
				this._parentPropertyGrid = parentPropertyGrid;
			}

			// Token: 0x060071F3 RID: 29171 RVA: 0x001A1C24 File Offset: 0x0019FE24
			internal override void ClearOwnerControlInternal()
			{
				if (this._owningPropertyGridView != null)
				{
					GridEntryCollection gridEntryCollection = this._owningPropertyGridView.AccessibilityGetGridEntries();
					if (gridEntryCollection != null)
					{
						for (int i = 0; i < gridEntryCollection.Count; i++)
						{
							AccessibleObject accessibilityObject = gridEntryCollection.GetEntry(i).AccessibilityObject;
							GridEntry.GridEntryAccessibleObject gridEntryAccessibleObject = accessibilityObject as GridEntry.GridEntryAccessibleObject;
							if (gridEntryAccessibleObject != null)
							{
								gridEntryAccessibleObject.ClearOwnerGridEntry();
							}
						}
					}
					this._owningPropertyGridView = null;
				}
				this._parentPropertyGrid = null;
				base.ClearOwnerControlInternal();
			}

			// Token: 0x060071F4 RID: 29172 RVA: 0x001A1C8A File Offset: 0x0019FE8A
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				if (AccessibilityImprovements.Level3)
				{
					return this.HitTest((int)x, (int)y);
				}
				return base.ElementProviderFromPoint(x, y);
			}

			// Token: 0x060071F5 RID: 29173 RVA: 0x001A1CA8 File Offset: 0x0019FEA8
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (AccessibilityImprovements.Level3)
				{
					PropertyGridAccessibleObject propertyGridAccessibleObject = this._parentPropertyGrid.AccessibilityObject as PropertyGridAccessibleObject;
					if (propertyGridAccessibleObject != null)
					{
						UnsafeNativeMethods.IRawElementProviderFragment rawElementProviderFragment = propertyGridAccessibleObject.ChildFragmentNavigate(this, direction);
						if (rawElementProviderFragment != null)
						{
							return rawElementProviderFragment;
						}
					}
					if (this._owningPropertyGridView.OwnerGrid.SortedByCategories)
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
						{
							return this.GetFirstCategory();
						}
						if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
						{
							return this.GetLastCategory();
						}
					}
					else
					{
						if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
						{
							return this.GetChild(0);
						}
						if (direction == UnsafeNativeMethods.NavigateDirection.LastChild)
						{
							int childCount = this.GetChildCount();
							if (childCount > 0)
							{
								return this.GetChild(childCount - 1);
							}
							return null;
						}
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170018F8 RID: 6392
			// (get) Token: 0x060071F6 RID: 29174 RVA: 0x001A1D42 File Offset: 0x0019FF42
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					if (AccessibilityImprovements.Level3)
					{
						return this._owningPropertyGridView.OwnerGrid.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x060071F7 RID: 29175 RVA: 0x001A1D6C File Offset: 0x0019FF6C
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (AccessibilityImprovements.Level3)
				{
					return this.GetFocused();
				}
				return base.FragmentRoot;
			}

			// Token: 0x060071F8 RID: 29176 RVA: 0x001A1D8C File Offset: 0x0019FF8C
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3)
				{
					if (propertyID == 30003)
					{
						return 50036;
					}
					if (propertyID == 30005)
					{
						return this.Name;
					}
				}
				if (AccessibilityImprovements.Level4 && (propertyID == 30030 || propertyID == 30038))
				{
					return true;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x060071F9 RID: 29177 RVA: 0x001A1DE9 File Offset: 0x0019FFE9
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && ((AccessibilityImprovements.Level4 && (patternId == 10006 || patternId == 10012)) || base.IsPatternSupported(patternId));
			}

			// Token: 0x170018F9 RID: 6393
			// (get) Token: 0x060071FA RID: 29178 RVA: 0x001A1E18 File Offset: 0x001A0018
			public override string Name
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return string.Empty;
					}
					string accessibleName = base.Owner.AccessibleName;
					if (accessibleName != null)
					{
						return accessibleName;
					}
					return SR.GetString("PropertyGridDefaultAccessibleName");
				}
			}

			// Token: 0x170018FA RID: 6394
			// (get) Token: 0x060071FB RID: 29179 RVA: 0x001A1E50 File Offset: 0x001A0050
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.Table;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Table;
				}
			}

			// Token: 0x060071FC RID: 29180 RVA: 0x001A1E7C File Offset: 0x001A007C
			public AccessibleObject Next(GridEntry current)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				int rowFromGridEntry = this._owningPropertyGridView.GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow = this._owningPropertyGridView.GetGridEntryFromRow(rowFromGridEntry + 1);
				if (gridEntryFromRow != null)
				{
					return gridEntryFromRow.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x060071FD RID: 29181 RVA: 0x001A1EBC File Offset: 0x001A00BC
			internal AccessibleObject GetCategory(int categoryIndex)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				GridEntry[] array = new GridEntry[1];
				GridEntryCollection topLevelGridEntries = this._owningPropertyGridView.TopLevelGridEntries;
				int count = topLevelGridEntries.Count;
				if (count > 0)
				{
					GridItem gridItem = topLevelGridEntries[categoryIndex];
					CategoryGridEntry categoryGridEntry = gridItem as CategoryGridEntry;
					if (categoryGridEntry != null)
					{
						return categoryGridEntry.AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x060071FE RID: 29182 RVA: 0x001A1F0E File Offset: 0x001A010E
			internal AccessibleObject GetFirstCategory()
			{
				return this.GetCategory(0);
			}

			// Token: 0x060071FF RID: 29183 RVA: 0x001A1F18 File Offset: 0x001A0118
			internal AccessibleObject GetLastCategory()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				GridEntryCollection topLevelGridEntries = this._owningPropertyGridView.TopLevelGridEntries;
				int count = topLevelGridEntries.Count;
				return this.GetCategory(topLevelGridEntries.Count - 1);
			}

			// Token: 0x06007200 RID: 29184 RVA: 0x001A1F50 File Offset: 0x001A0150
			internal AccessibleObject GetPreviousGridEntry(GridEntry currentGridEntry, GridEntryCollection gridEntryCollection, out bool currentGridEntryFound)
			{
				GridEntry gridEntry = null;
				currentGridEntryFound = false;
				foreach (object obj in gridEntryCollection)
				{
					GridEntry gridEntry2 = (GridEntry)obj;
					if (currentGridEntry == gridEntry2)
					{
						currentGridEntryFound = true;
						if (gridEntry != null)
						{
							return gridEntry.AccessibilityObject;
						}
						return null;
					}
					else
					{
						gridEntry = gridEntry2;
						if (gridEntry2.ChildCount > 0)
						{
							AccessibleObject previousGridEntry = this.GetPreviousGridEntry(currentGridEntry, gridEntry2.Children, out currentGridEntryFound);
							if (previousGridEntry != null)
							{
								return previousGridEntry;
							}
							if (currentGridEntryFound)
							{
								return null;
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06007201 RID: 29185 RVA: 0x001A1FEC File Offset: 0x001A01EC
			internal AccessibleObject GetNextGridEntry(GridEntry currentGridEntry, GridEntryCollection gridEntryCollection, out bool currentGridEntryFound)
			{
				currentGridEntryFound = false;
				foreach (object obj in gridEntryCollection)
				{
					GridEntry gridEntry = (GridEntry)obj;
					if (currentGridEntryFound)
					{
						return gridEntry.AccessibilityObject;
					}
					if (currentGridEntry == gridEntry)
					{
						currentGridEntryFound = true;
					}
					else if (gridEntry.ChildCount > 0)
					{
						AccessibleObject nextGridEntry = this.GetNextGridEntry(currentGridEntry, gridEntry.Children, out currentGridEntryFound);
						if (nextGridEntry != null)
						{
							return nextGridEntry;
						}
						if (currentGridEntryFound)
						{
							return null;
						}
					}
				}
				return null;
			}

			// Token: 0x06007202 RID: 29186 RVA: 0x001A2080 File Offset: 0x001A0280
			internal AccessibleObject GetFirstChildProperty(CategoryGridEntry current)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (current.ChildCount > 0)
				{
					GridEntryCollection children = current.Children;
					if (children != null && children.Count > 0)
					{
						GridEntry[] array = new GridEntry[1];
						try
						{
							this._owningPropertyGridView.GetGridEntriesFromOutline(children, 0, 0, array);
						}
						catch (Exception ex)
						{
						}
						return array[0].AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x06007203 RID: 29187 RVA: 0x001A20EC File Offset: 0x001A02EC
			internal AccessibleObject GetLastChildProperty(CategoryGridEntry current)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				if (current.ChildCount > 0)
				{
					GridEntryCollection children = current.Children;
					if (children != null && children.Count > 0)
					{
						GridEntry[] array = new GridEntry[1];
						try
						{
							this._owningPropertyGridView.GetGridEntriesFromOutline(children, 0, children.Count - 1, array);
						}
						catch (Exception ex)
						{
						}
						return array[0].AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x06007204 RID: 29188 RVA: 0x001A215C File Offset: 0x001A035C
			internal AccessibleObject GetNextCategory(CategoryGridEntry current)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				int num = this._owningPropertyGridView.GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow;
				for (;;)
				{
					gridEntryFromRow = this._owningPropertyGridView.GetGridEntryFromRow(++num);
					if (gridEntryFromRow is CategoryGridEntry)
					{
						break;
					}
					if (gridEntryFromRow == null)
					{
						goto Block_3;
					}
				}
				return gridEntryFromRow.AccessibilityObject;
				Block_3:
				return null;
			}

			// Token: 0x06007205 RID: 29189 RVA: 0x001A21A4 File Offset: 0x001A03A4
			public AccessibleObject Previous(GridEntry current)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				int rowFromGridEntry = this._owningPropertyGridView.GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow = this._owningPropertyGridView.GetGridEntryFromRow(rowFromGridEntry - 1);
				if (gridEntryFromRow != null)
				{
					return gridEntryFromRow.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x06007206 RID: 29190 RVA: 0x001A21E4 File Offset: 0x001A03E4
			internal AccessibleObject GetPreviousCategory(CategoryGridEntry current)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				int num = this._owningPropertyGridView.GetRowFromGridEntry(current);
				GridEntry gridEntryFromRow;
				for (;;)
				{
					gridEntryFromRow = this._owningPropertyGridView.GetGridEntryFromRow(--num);
					if (gridEntryFromRow is CategoryGridEntry)
					{
						break;
					}
					if (gridEntryFromRow == null)
					{
						goto Block_3;
					}
				}
				return gridEntryFromRow.AccessibilityObject;
				Block_3:
				return null;
			}

			// Token: 0x06007207 RID: 29191 RVA: 0x001A222C File Offset: 0x001A042C
			public override AccessibleObject GetChild(int index)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				GridEntryCollection gridEntryCollection = this._owningPropertyGridView.AccessibilityGetGridEntries();
				if (gridEntryCollection != null && index >= 0 && index < gridEntryCollection.Count)
				{
					return gridEntryCollection.GetEntry(index).AccessibilityObject;
				}
				return null;
			}

			// Token: 0x06007208 RID: 29192 RVA: 0x001A2270 File Offset: 0x001A0470
			public override int GetChildCount()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return 0;
				}
				GridEntryCollection gridEntryCollection = this._owningPropertyGridView.AccessibilityGetGridEntries();
				if (gridEntryCollection != null)
				{
					return gridEntryCollection.Count;
				}
				return 0;
			}

			// Token: 0x06007209 RID: 29193 RVA: 0x001A22A0 File Offset: 0x001A04A0
			public override AccessibleObject GetFocused()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
				if (selectedGridEntry != null && selectedGridEntry.Focus)
				{
					return selectedGridEntry.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x0600720A RID: 29194 RVA: 0x001A22D8 File Offset: 0x001A04D8
			public override AccessibleObject GetSelected()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				GridEntry selectedGridEntry = this._owningPropertyGridView.SelectedGridEntry;
				if (selectedGridEntry != null)
				{
					return selectedGridEntry.AccessibilityObject;
				}
				return null;
			}

			// Token: 0x0600720B RID: 29195 RVA: 0x001A2308 File Offset: 0x001A0508
			public override AccessibleObject HitTest(int x, int y)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				NativeMethods.POINT point = new NativeMethods.POINT(x, y);
				UnsafeNativeMethods.ScreenToClient(new HandleRef(this._owningPropertyGridView, this._owningPropertyGridView.Handle), point);
				Point left = this._owningPropertyGridView.FindPosition(point.x, point.y);
				if (left != PropertyGridView.InvalidPosition)
				{
					GridEntry gridEntryFromRow = this._owningPropertyGridView.GetGridEntryFromRow(left.Y);
					if (gridEntryFromRow != null)
					{
						return gridEntryFromRow.AccessibilityObject;
					}
				}
				return null;
			}

			// Token: 0x0600720C RID: 29196 RVA: 0x001774C4 File Offset: 0x001756C4
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override AccessibleObject Navigate(AccessibleNavigation navdir)
			{
				if (this.GetChildCount() > 0)
				{
					if (navdir == AccessibleNavigation.FirstChild)
					{
						return this.GetChild(0);
					}
					if (navdir == AccessibleNavigation.LastChild)
					{
						return this.GetChild(this.GetChildCount() - 1);
					}
				}
				return null;
			}

			// Token: 0x0600720D RID: 29197 RVA: 0x001A2387 File Offset: 0x001A0587
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetItem(int row, int column)
			{
				if (AccessibilityImprovements.Level4)
				{
					return this.GetChild(row);
				}
				return base.GetItem(row, column);
			}

			// Token: 0x170018FB RID: 6395
			// (get) Token: 0x0600720E RID: 29198 RVA: 0x001A23A0 File Offset: 0x001A05A0
			internal override int RowCount
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return 0;
					}
					if (!AccessibilityImprovements.Level4)
					{
						return base.RowCount;
					}
					GridEntryCollection topLevelGridEntries = this._owningPropertyGridView.TopLevelGridEntries;
					if (topLevelGridEntries == null || this._owningPropertyGridView.OwnerGrid == null)
					{
						return 0;
					}
					if (!this._owningPropertyGridView.OwnerGrid.SortedByCategories)
					{
						return topLevelGridEntries.Count;
					}
					int num = 0;
					foreach (object obj in topLevelGridEntries)
					{
						if (obj is CategoryGridEntry)
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x170018FC RID: 6396
			// (get) Token: 0x0600720F RID: 29199 RVA: 0x001A244C File Offset: 0x001A064C
			internal override int ColumnCount
			{
				get
				{
					if (AccessibilityImprovements.Level4)
					{
						return 1;
					}
					return base.ColumnCount;
				}
			}

			// Token: 0x040044A8 RID: 17576
			private PropertyGridView _owningPropertyGridView;

			// Token: 0x040044A9 RID: 17577
			private PropertyGrid _parentPropertyGrid;
		}

		// Token: 0x02000889 RID: 2185
		internal class GridPositionData
		{
			// Token: 0x06007210 RID: 29200 RVA: 0x001A2460 File Offset: 0x001A0660
			public GridPositionData(PropertyGridView gridView)
			{
				this.selectedItemTree = gridView.GetGridEntryHierarchy(gridView.selectedGridEntry);
				this.expandedState = gridView.SaveHierarchyState(gridView.topLevelGridEntries);
				this.itemRow = gridView.selectedRow;
				this.itemCount = gridView.totalProps;
			}

			// Token: 0x06007211 RID: 29201 RVA: 0x001A24B0 File Offset: 0x001A06B0
			public GridEntry Restore(PropertyGridView gridView)
			{
				gridView.RestoreHierarchyState(this.expandedState);
				GridEntry gridEntry = gridView.FindEquivalentGridEntry(this.selectedItemTree);
				if (gridEntry != null)
				{
					gridView.SelectGridEntry(gridEntry, true);
					int num = gridView.selectedRow - this.itemRow;
					if (num != 0 && gridView.ScrollBar.Visible && this.itemRow < gridView.visibleRows)
					{
						num += gridView.GetScrollOffset();
						if (num < 0)
						{
							num = 0;
						}
						else if (num > gridView.ScrollBar.Maximum)
						{
							num = gridView.ScrollBar.Maximum - 1;
						}
						gridView.SetScrollOffset(num);
					}
				}
				return gridEntry;
			}

			// Token: 0x040044AA RID: 17578
			private ArrayList expandedState;

			// Token: 0x040044AB RID: 17579
			private GridEntryCollection selectedItemTree;

			// Token: 0x040044AC RID: 17580
			private int itemRow;

			// Token: 0x040044AD RID: 17581
			private int itemCount;
		}
	}
}
