using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000340 RID: 832
	internal class TableLayoutPanelDesigner : FlowPanelDesigner
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x000C6A5F File Offset: 0x000C4C5F
		private TableLayoutPanelBehavior Behavior
		{
			get
			{
				if (this.tlpBehavior == null)
				{
					this.tlpBehavior = new TableLayoutPanelBehavior(this.Table, this, base.Component.Site);
				}
				return this.tlpBehavior;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x000C6A8C File Offset: 0x000C4C8C
		private TableLayoutColumnStyleCollection ColumnStyles
		{
			get
			{
				return this.Table.ColumnStyles;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x000C6A99 File Offset: 0x000C4C99
		private TableLayoutRowStyleCollection RowStyles
		{
			get
			{
				return this.Table.RowStyles;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x000C6AA6 File Offset: 0x000C4CA6
		// (set) Token: 0x060020B7 RID: 8375 RVA: 0x000C6AB3 File Offset: 0x000C4CB3
		public int RowCount
		{
			get
			{
				return this.Table.RowCount;
			}
			set
			{
				if (value <= 0 && !this.Undoing)
				{
					throw new ArgumentException(SR.GetString("TableLayoutPanelDesignerInvalidColumnRowCount", new object[]
					{
						"RowCount"
					}));
				}
				this.Table.RowCount = value;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x000C6AEB File Offset: 0x000C4CEB
		// (set) Token: 0x060020B9 RID: 8377 RVA: 0x000C6AF8 File Offset: 0x000C4CF8
		public int ColumnCount
		{
			get
			{
				return this.Table.ColumnCount;
			}
			set
			{
				if (value <= 0 && !this.Undoing)
				{
					throw new ArgumentException(SR.GetString("TableLayoutPanelDesignerInvalidColumnRowCount", new object[]
					{
						"ColumnCount"
					}));
				}
				this.Table.ColumnCount = value;
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x000C6B30 File Offset: 0x000C4D30
		private bool IsLocalizable()
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(designerHost.RootComponent)["Localizable"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool))
				{
					return (bool)propertyDescriptor.GetValue(designerHost.RootComponent);
				}
			}
			return false;
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000C6B99 File Offset: 0x000C4D99
		private bool ShouldSerializeColumnStyles()
		{
			return !this.IsLocalizable();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000C6B99 File Offset: 0x000C4D99
		private bool ShouldSerializeRowStyles()
		{
			return !this.IsLocalizable();
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000C6BA4 File Offset: 0x000C4DA4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		private TableLayoutPanelDesigner.DesignerTableLayoutControlCollection Controls
		{
			get
			{
				if (this.controls == null)
				{
					this.controls = new TableLayoutPanelDesigner.DesignerTableLayoutControlCollection((TableLayoutPanel)this.Control);
				}
				return this.controls;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x000C6BCC File Offset: 0x000C4DCC
		private ContextMenuStrip DesignerContextMenuStrip
		{
			get
			{
				if (this.designerContextMenuStrip == null)
				{
					this.designerContextMenuStrip = new BaseContextMenuStrip(base.Component.Site, this.Table);
					ContextMenuStripGroup contextMenuStripGroup = this.designerContextMenuStrip.Groups["Verbs"];
					foreach (object obj in this.Verbs)
					{
						DesignerVerb designerVerb = (DesignerVerb)obj;
						if (!designerVerb.Text.Equals(SR.GetString("TableLayoutPanelDesignerEditRowAndCol")))
						{
							foreach (ToolStripItem toolStripItem in contextMenuStripGroup.Items)
							{
								if (toolStripItem.Text.Equals(designerVerb.Text))
								{
									contextMenuStripGroup.Items.Remove(toolStripItem);
									break;
								}
							}
						}
					}
					ToolStripDropDownMenu dropDown = this.BuildMenu(true);
					ToolStripDropDownMenu dropDown2 = this.BuildMenu(false);
					this.contextMenuRow = new ToolStripMenuItem();
					this.contextMenuRow.DropDown = dropDown;
					this.contextMenuRow.Text = SR.GetString("TableLayoutPanelDesignerRowMenu");
					this.contextMenuCol = new ToolStripMenuItem();
					this.contextMenuCol.DropDown = dropDown2;
					this.contextMenuCol.Text = SR.GetString("TableLayoutPanelDesignerColMenu");
					contextMenuStripGroup.Items.Insert(0, this.contextMenuCol);
					contextMenuStripGroup.Items.Insert(0, this.contextMenuRow);
					contextMenuStripGroup = this.designerContextMenuStrip.Groups["Edit"];
					foreach (ToolStripItem toolStripItem2 in contextMenuStripGroup.Items)
					{
						if (toolStripItem2.Text.Equals(SR.GetString("ContextMenuCut")))
						{
							toolStripItem2.Text = SR.GetString("TableLayoutPanelDesignerContextMenuCut");
						}
						else if (toolStripItem2.Text.Equals(SR.GetString("ContextMenuCopy")))
						{
							toolStripItem2.Text = SR.GetString("TableLayoutPanelDesignerContextMenuCopy");
						}
						else if (toolStripItem2.Text.Equals(SR.GetString("ContextMenuDelete")))
						{
							toolStripItem2.Text = SR.GetString("TableLayoutPanelDesignerContextMenuDelete");
						}
					}
				}
				bool enabled = this.IsOverValidCell(false);
				this.contextMenuRow.Enabled = enabled;
				this.contextMenuCol.Enabled = enabled;
				return this.designerContextMenuStrip;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x000C6E6C File Offset: 0x000C506C
		private bool IsLoading
		{
			get
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				return designerHost != null && designerHost.Loading;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x000C6E9A File Offset: 0x000C509A
		internal TableLayoutPanel Table
		{
			get
			{
				return base.Component as TableLayoutPanel;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x000C6EA8 File Offset: 0x000C50A8
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x000C6F28 File Offset: 0x000C5128
		private bool Undoing
		{
			get
			{
				if (this.undoEngine == null)
				{
					this.undoEngine = (this.GetService(typeof(UndoEngine)) as UndoEngine);
					if (this.undoEngine != null)
					{
						this.undoEngine.Undoing += this.OnUndoing;
						if (this.undoEngine.UndoInProgress)
						{
							this.undoing = true;
							this.undoEngine.Undone += this.OnUndone;
						}
					}
				}
				return this.undoing;
			}
			set
			{
				this.undoing = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x000C6F34 File Offset: 0x000C5134
		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (this.verbs == null)
				{
					this.removeColVerb = new DesignerVerb(SR.GetString("TableLayoutPanelDesignerRemoveColumn"), new EventHandler(this.OnVerbRemove));
					this.removeRowVerb = new DesignerVerb(SR.GetString("TableLayoutPanelDesignerRemoveRow"), new EventHandler(this.OnVerbRemove));
					this.verbs = new DesignerVerbCollection();
					this.verbs.Add(new DesignerVerb(SR.GetString("TableLayoutPanelDesignerAddColumn"), new EventHandler(this.OnVerbAdd)));
					this.verbs.Add(new DesignerVerb(SR.GetString("TableLayoutPanelDesignerAddRow"), new EventHandler(this.OnVerbAdd)));
					this.verbs.Add(this.removeColVerb);
					this.verbs.Add(this.removeRowVerb);
					this.verbs.Add(new DesignerVerb(SR.GetString("TableLayoutPanelDesignerEditRowAndCol"), new EventHandler(this.OnVerbEdit)));
					this.CheckVerbStatus();
				}
				return this.verbs;
			}
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000C7040 File Offset: 0x000C5240
		private void RefreshSmartTag()
		{
			DesignerActionUIService designerActionUIService = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
			if (designerActionUIService != null)
			{
				designerActionUIService.Refresh(base.Component);
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000C7074 File Offset: 0x000C5274
		private void CheckVerbStatus()
		{
			if (this.Table != null)
			{
				if (this.removeColVerb != null)
				{
					bool flag = this.Table.ColumnCount > 1;
					if (this.removeColVerb.Enabled != flag)
					{
						this.removeColVerb.Enabled = flag;
					}
				}
				if (this.removeRowVerb != null)
				{
					bool flag2 = this.Table.RowCount > 1;
					if (this.removeRowVerb.Enabled != flag2)
					{
						this.removeRowVerb.Enabled = flag2;
					}
				}
				this.RefreshSmartTag();
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x000C70F1 File Offset: 0x000C52F1
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this.actionLists == null)
				{
					this.BuildActionLists();
				}
				return this.actionLists;
			}
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000C7108 File Offset: 0x000C5308
		private ToolStripDropDownMenu BuildMenu(bool isRow)
		{
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
			ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
			ToolStripLabel toolStripLabel = new ToolStripLabel();
			ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem6 = new ToolStripMenuItem();
			toolStripMenuItem.Text = SR.GetString("TableLayoutPanelDesignerAddMenu");
			toolStripMenuItem.Tag = isRow;
			toolStripMenuItem.Name = "add";
			toolStripMenuItem.Click += this.OnAddClick;
			toolStripMenuItem2.Text = SR.GetString("TableLayoutPanelDesignerInsertMenu");
			toolStripMenuItem2.Tag = isRow;
			toolStripMenuItem2.Name = "insert";
			toolStripMenuItem2.Click += this.OnInsertClick;
			toolStripMenuItem3.Text = SR.GetString("TableLayoutPanelDesignerDeleteMenu");
			toolStripMenuItem3.Tag = isRow;
			toolStripMenuItem3.Name = "delete";
			toolStripMenuItem3.Click += this.OnDeleteClick;
			toolStripLabel.Text = SR.GetString("TableLayoutPanelDesignerLabelMenu");
			if (SR.GetString("TableLayoutPanelDesignerDontBoldLabel") == "0")
			{
				toolStripLabel.Font = new Font(toolStripLabel.Font, FontStyle.Bold);
			}
			toolStripLabel.Name = "sizemode";
			toolStripMenuItem4.Text = SR.GetString("TableLayoutPanelDesignerAbsoluteMenu");
			toolStripMenuItem4.Tag = isRow;
			toolStripMenuItem4.Name = "absolute";
			toolStripMenuItem4.Click += this.OnAbsoluteClick;
			toolStripMenuItem5.Text = SR.GetString("TableLayoutPanelDesignerPercentageMenu");
			toolStripMenuItem5.Tag = isRow;
			toolStripMenuItem5.Name = "percent";
			toolStripMenuItem5.Click += this.OnPercentClick;
			toolStripMenuItem6.Text = SR.GetString("TableLayoutPanelDesignerAutoSizeMenu");
			toolStripMenuItem6.Tag = isRow;
			toolStripMenuItem6.Name = "autosize";
			toolStripMenuItem6.Click += this.OnAutoSizeClick;
			ToolStripDropDownMenu toolStripDropDownMenu = new ToolStripDropDownMenu();
			toolStripDropDownMenu.Items.AddRange(new ToolStripItem[]
			{
				toolStripMenuItem,
				toolStripMenuItem2,
				toolStripMenuItem3,
				toolStripSeparator,
				toolStripLabel,
				toolStripMenuItem4,
				toolStripMenuItem5,
				toolStripMenuItem6
			});
			toolStripDropDownMenu.Tag = isRow;
			toolStripDropDownMenu.Opening += this.OnRowColMenuOpening;
			IUIService iuiservice = this.GetService(typeof(IUIService)) as IUIService;
			if (iuiservice != null)
			{
				toolStripDropDownMenu.Renderer = (ToolStripProfessionalRenderer)iuiservice.Styles["VsRenderer"];
				if (iuiservice.Styles["VsColorPanelText"] is Color)
				{
					toolStripDropDownMenu.ForeColor = (Color)iuiservice.Styles["VsColorPanelText"];
				}
			}
			return toolStripDropDownMenu;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000C73BE File Offset: 0x000C55BE
		private void BuildActionLists()
		{
			this.actionLists = new DesignerActionListCollection();
			this.actionLists.Add(new TableLayoutPanelDesigner.TableLayouPanelRowColumnActionList(this));
			this.actionLists[0].AutoShow = true;
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000C73F0 File Offset: 0x000C55F0
		private void RemoveControlInternal(Control c)
		{
			this.Table.ControlRemoved -= this.OnControlRemoved;
			this.Table.Controls.Remove(c);
			this.Table.ControlRemoved += this.OnControlRemoved;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000C743C File Offset: 0x000C563C
		private void AddControlInternal(Control c, int col, int row)
		{
			this.Table.ControlAdded -= this.OnControlAdded;
			this.Table.Controls.Add(c, col, row);
			this.Table.ControlAdded += this.OnControlAdded;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000C748C File Offset: 0x000C568C
		private void ControlAddedInternal(Control control, Point newControlPosition, bool localReposition, bool fullTable, DragEventArgs de)
		{
			if (fullTable)
			{
				if (this.Table.GrowStyle == TableLayoutPanelGrowStyle.AddRows)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.Table)["RowCount"];
					if (propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(this.Table, this.Table.GetRowHeights().Length);
					}
					newControlPosition.X = 0;
					newControlPosition.Y = this.Table.RowCount - 1;
				}
				else if (this.Table.GrowStyle == TableLayoutPanelGrowStyle.AddColumns)
				{
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(this.Table)["ColumnCount"];
					if (propertyDescriptor2 != null)
					{
						propertyDescriptor2.SetValue(this.Table, this.Table.GetColumnWidths().Length);
					}
					newControlPosition.X = this.Table.ColumnCount - 1;
					newControlPosition.Y = 0;
				}
			}
			DesignerTransaction designerTransaction = null;
			PropertyDescriptor prop = TypeDescriptor.GetProperties(this.Table)["Controls"];
			try
			{
				bool flag = de != null && de.Effect == DragDropEffects.Copy && localReposition;
				Control control2 = ((TableLayoutPanel)this.Control).GetControlFromPosition(newControlPosition.X, newControlPosition.Y);
				if (flag)
				{
					IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null)
					{
						designerTransaction = designerHost.CreateTransaction(SR.GetString("BehaviorServiceCopyControl", new object[]
						{
							control.Site.Name
						}));
					}
					this.PropChanging(prop);
				}
				else if (control2 != null && !control2.Equals(control))
				{
					if (localReposition)
					{
						IDesignerHost designerHost2 = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (designerHost2 != null)
						{
							designerTransaction = designerHost2.CreateTransaction(SR.GetString("TableLayoutPanelDesignerControlsSwapped", new object[]
							{
								control.Site.Name,
								control2.Site.Name
							}));
						}
						this.PropChanging(prop);
						this.RemoveControlInternal(control2);
					}
					else
					{
						this.PropChanging(prop);
						control2 = null;
					}
				}
				else
				{
					if (localReposition)
					{
						IDesignerHost designerHost3 = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (designerHost3 != null)
						{
							designerTransaction = designerHost3.CreateTransaction(SR.GetString("BehaviorServiceMoveControl", new object[]
							{
								control.Site.Name
							}));
						}
					}
					control2 = null;
					this.PropChanging(prop);
				}
				if (flag)
				{
					ArrayList arrayList = DesignerUtils.CopyDragObjects(new ArrayList
					{
						control
					}, base.Component.Site) as ArrayList;
					control = (arrayList[0] as Control);
				}
				if (localReposition)
				{
					Point controlPosition = this.GetControlPosition(control);
					if (controlPosition != ControlDesigner.InvalidPoint)
					{
						this.RemoveControlInternal(control);
						if (controlPosition != newControlPosition && control2 != null)
						{
							this.AddControlInternal(control2, controlPosition.X, controlPosition.Y);
						}
					}
				}
				if (localReposition)
				{
					this.AddControlInternal(control, newControlPosition.X, newControlPosition.Y);
				}
				else
				{
					this.Table.SetCellPosition(control, new TableLayoutPanelCellPosition(newControlPosition.X, newControlPosition.Y));
				}
				this.PropChanged(prop);
				if (de != null)
				{
					base.OnDragComplete(de);
				}
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
					designerTransaction = null;
				}
				if (flag)
				{
					ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
					if (selectionService != null)
					{
						selectionService.SetSelectedComponents(new object[]
						{
							control
						}, SelectionTypes.Replace | SelectionTypes.Click);
					}
				}
			}
			catch (ArgumentException ex)
			{
				IUIService iuiservice = this.GetService(typeof(IUIService)) as IUIService;
				if (iuiservice != null)
				{
					iuiservice.ShowError(ex);
				}
			}
			catch (Exception ex2)
			{
				if (ClientUtils.IsCriticalException(ex2))
				{
					throw;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
				}
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000C7868 File Offset: 0x000C5A68
		private void CreateEmptyTable()
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.Table)["ColumnCount"];
			if (propertyDescriptor != null)
			{
				propertyDescriptor.SetValue(this.Table, DesignerUtils.DEFAULTCOLUMNCOUNT);
			}
			PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(this.Table)["RowCount"];
			if (propertyDescriptor2 != null)
			{
				propertyDescriptor2.SetValue(this.Table, DesignerUtils.DEFAULTROWCOUNT);
			}
			this.EnsureAvailableStyles();
			this.InitializeNewStyles();
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000C78E0 File Offset: 0x000C5AE0
		private void InitializeNewStyles()
		{
			this.Table.ColumnStyles[0].SizeType = SizeType.Percent;
			this.Table.ColumnStyles[0].Width = (float)DesignerUtils.MINIMUMSTYLEPERCENT;
			this.Table.ColumnStyles[1].SizeType = SizeType.Percent;
			this.Table.ColumnStyles[1].Width = (float)DesignerUtils.MINIMUMSTYLEPERCENT;
			this.Table.RowStyles[0].SizeType = SizeType.Percent;
			this.Table.RowStyles[0].Height = (float)DesignerUtils.MINIMUMSTYLEPERCENT;
			this.Table.RowStyles[1].SizeType = SizeType.Percent;
			this.Table.RowStyles[1].Height = (float)DesignerUtils.MINIMUMSTYLEPERCENT;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000C79BC File Offset: 0x000C5BBC
		private static bool SubsetExists(bool[,] cells, int columns, int rows, int subsetColumns, int subsetRows)
		{
			bool flag = false;
			for (int i = 0; i < rows - subsetRows + 1; i++)
			{
				for (int j = 0; j < columns - subsetColumns + 1; j++)
				{
					if (!cells[j, i])
					{
						flag = true;
						int num = i;
						while (num < i + subsetRows && flag)
						{
							for (int k = j; k < j + subsetColumns; k++)
							{
								if (cells[k, num])
								{
									flag = false;
									break;
								}
							}
							num++;
						}
						if (flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000C7A38 File Offset: 0x000C5C38
		protected internal override bool CanAddComponent(IComponent component)
		{
			if (this.Table.GrowStyle != TableLayoutPanelGrowStyle.FixedSize)
			{
				return true;
			}
			Control control = base.GetControl(component);
			if (control == null)
			{
				return false;
			}
			int rowSpan = this.Table.GetRowSpan(control);
			int columnSpan = this.Table.GetColumnSpan(control);
			int num = this.Table.GetRowHeights().Length;
			int num2 = this.Table.GetColumnWidths().Length;
			int num3 = 0;
			int num4 = num * num2;
			int num5 = rowSpan * columnSpan;
			bool[,] array = null;
			if (num5 > 1)
			{
				array = new bool[num2, num];
			}
			if (num5 <= num4)
			{
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						if (this.Table.GetControlFromPosition(j, i) != null)
						{
							num3++;
							if (num5 > 1)
							{
								array[j, i] = true;
							}
						}
					}
				}
			}
			if (num3 + num5 > num4)
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				iuiservice.ShowError(SR.GetString("TableLayoutPanelFullDesc"));
				return false;
			}
			if (num5 > 1 && !TableLayoutPanelDesigner.SubsetExists(array, num2, num, columnSpan, rowSpan))
			{
				IUIService iuiservice2 = (IUIService)this.GetService(typeof(IUIService));
				iuiservice2.ShowError(SR.GetString("TableLayoutPanelSpanDesc"));
				return false;
			}
			return true;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000C7B78 File Offset: 0x000C5D78
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					designerHost.TransactionClosing -= this.OnTransactionClosing;
				}
				if (this.undoEngine != null)
				{
					if (this.Undoing)
					{
						this.undoEngine.Undone -= this.OnUndone;
					}
					this.undoEngine.Undoing -= this.OnUndoing;
				}
				if (this.compSvc != null)
				{
					this.compSvc.ComponentChanged -= this.OnComponentChanged;
					this.compSvc.ComponentChanging -= this.OnComponentChanging;
				}
				if (this.Table != null)
				{
					this.Table.ControlAdded -= this.OnControlAdded;
					this.Table.ControlRemoved -= this.OnControlRemoved;
				}
				if (this.contextMenuRow != null)
				{
					this.contextMenuRow.Dispose();
				}
				if (this.contextMenuCol != null)
				{
					this.contextMenuCol.Dispose();
				}
				this.rowStyleProp = null;
				this.colStyleProp = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000C7C9C File Offset: 0x000C5E9C
		protected override void DrawBorder(Graphics graphics)
		{
			if (this.Table.CellBorderStyle != TableLayoutPanelCellBorderStyle.None)
			{
				return;
			}
			base.DrawBorder(graphics);
			Rectangle displayRectangle = this.Control.DisplayRectangle;
			int num = displayRectangle.Width;
			displayRectangle.Width = num - 1;
			num = displayRectangle.Height;
			displayRectangle.Height = num - 1;
			int[] columnWidths = this.Table.GetColumnWidths();
			int[] rowHeights = this.Table.GetRowHeights();
			using (Pen borderPen = base.BorderPen)
			{
				if (columnWidths.Length > 1)
				{
					bool flag = this.Table.RightToLeft == RightToLeft.Yes;
					int num2 = flag ? displayRectangle.Right : displayRectangle.Left;
					for (int i = 0; i < columnWidths.Length - 1; i++)
					{
						if (flag)
						{
							num2 -= columnWidths[i];
						}
						else
						{
							num2 += columnWidths[i];
						}
						graphics.DrawLine(borderPen, num2, displayRectangle.Top, num2, displayRectangle.Bottom);
					}
				}
				if (rowHeights.Length > 1)
				{
					int num3 = displayRectangle.Top;
					for (int j = 0; j < rowHeights.Length - 1; j++)
					{
						num3 += rowHeights[j];
						graphics.DrawLine(borderPen, displayRectangle.Left, num3, displayRectangle.Right, num3);
					}
				}
			}
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000C7DE4 File Offset: 0x000C5FE4
		internal void SuspendEnsureAvailableStyles()
		{
			this.ensureSuspendCount++;
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000C7DF4 File Offset: 0x000C5FF4
		internal void ResumeEnsureAvailableStyles(bool performEnsure)
		{
			if (this.ensureSuspendCount > 0)
			{
				this.ensureSuspendCount--;
				if (this.ensureSuspendCount == 0 && performEnsure)
				{
					this.EnsureAvailableStyles();
				}
			}
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000C7E24 File Offset: 0x000C6024
		private bool EnsureAvailableStyles()
		{
			if (this.IsLoading || this.Undoing || this.ensureSuspendCount > 0)
			{
				return false;
			}
			int[] columnWidths = this.Table.GetColumnWidths();
			int[] rowHeights = this.Table.GetRowHeights();
			this.Table.SuspendLayout();
			try
			{
				if (columnWidths.Length > this.Table.ColumnStyles.Count)
				{
					int num = columnWidths.Length - this.Table.ColumnStyles.Count;
					this.PropChanging(this.rowStyleProp);
					for (int i = 0; i < num; i++)
					{
						this.Table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, (float)DesignerUtils.MINIMUMSTYLESIZE));
					}
					this.PropChanged(this.rowStyleProp);
				}
				if (rowHeights.Length > this.Table.RowStyles.Count)
				{
					int num2 = rowHeights.Length - this.Table.RowStyles.Count;
					this.PropChanging(this.colStyleProp);
					for (int j = 0; j < num2; j++)
					{
						this.Table.RowStyles.Add(new RowStyle(SizeType.Absolute, (float)DesignerUtils.MINIMUMSTYLESIZE));
					}
					this.PropChanged(this.colStyleProp);
				}
			}
			finally
			{
				this.Table.ResumeLayout();
			}
			return true;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000C7F6C File Offset: 0x000C616C
		private Control ExtractControlFromDragEvent(DragEventArgs de)
		{
			DropSourceBehavior.BehaviorDataObject behaviorDataObject = de.Data as DropSourceBehavior.BehaviorDataObject;
			if (behaviorDataObject != null)
			{
				this.dragComps = new ArrayList(behaviorDataObject.DragComponents);
				return this.dragComps[0] as Control;
			}
			return null;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000C7FAC File Offset: 0x000C61AC
		private Point GetCellPosition(Point pos)
		{
			int[] rowHeights = this.Table.GetRowHeights();
			int[] columnWidths = this.Table.GetColumnWidths();
			Point location = this.Table.PointToScreen(this.Table.DisplayRectangle.Location);
			Rectangle rectangle = new Rectangle(location, this.Table.DisplayRectangle.Size);
			Point result = new Point(-1, -1);
			bool flag = this.Table.RightToLeft == RightToLeft.Yes;
			int num = rectangle.X;
			if (flag)
			{
				if (pos.X <= rectangle.X)
				{
					result.X = columnWidths.Length;
				}
				else if (pos.X < rectangle.Right)
				{
					num = rectangle.Right;
					for (int i = 0; i < columnWidths.Length; i++)
					{
						result.X = i;
						if (pos.X >= num - columnWidths[i])
						{
							break;
						}
						num -= columnWidths[i];
					}
				}
			}
			else if (pos.X >= rectangle.Right)
			{
				result.X = columnWidths.Length;
			}
			else if (pos.X > rectangle.X)
			{
				for (int j = 0; j < columnWidths.Length; j++)
				{
					result.X = j;
					if (pos.X <= num + columnWidths[j])
					{
						break;
					}
					num += columnWidths[j];
				}
			}
			num = rectangle.Y;
			if (pos.Y >= rectangle.Bottom)
			{
				result.Y = rowHeights.Length;
			}
			else if (pos.Y > rectangle.Y)
			{
				for (int k = 0; k < rowHeights.Length; k++)
				{
					if (pos.Y <= num + rowHeights[k])
					{
						result.Y = k;
						break;
					}
					num += rowHeights[k];
				}
			}
			return result;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000C8174 File Offset: 0x000C6374
		private Point GetControlPosition(Control control)
		{
			TableLayoutPanelCellPosition positionFromControl = this.Table.GetPositionFromControl(control);
			if (positionFromControl.Row == -1 && positionFromControl.Column == -1)
			{
				return ControlDesigner.InvalidPoint;
			}
			return new Point(positionFromControl.Column, positionFromControl.Row);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000C81BC File Offset: 0x000C63BC
		public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
		{
			GlyphCollection glyphs = base.GetGlyphs(selectionType);
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Locked"];
			bool flag = propertyDescriptor != null && (bool)propertyDescriptor.GetValue(base.Component);
			bool flag2 = this.EnsureAvailableStyles();
			if (selectionType != GlyphSelectionType.NotSelected && !flag && this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly)
			{
				Point location = base.BehaviorService.MapAdornerWindowPoint(this.Table.Handle, this.Table.DisplayRectangle.Location);
				Rectangle rectangle = new Rectangle(location, this.Table.DisplayRectangle.Size);
				Point location2 = base.BehaviorService.ControlToAdornerWindow(this.Control);
				Rectangle rectangle2 = new Rectangle(location2, this.Control.ClientSize);
				int[] columnWidths = this.Table.GetColumnWidths();
				int[] rowHeights = this.Table.GetRowHeights();
				int num = DesignerUtils.RESIZEGLYPHSIZE / 2;
				bool flag3 = this.Table.RightToLeft == RightToLeft.Yes;
				int num2 = flag3 ? rectangle.Right : rectangle.X;
				if (flag2)
				{
					for (int i = 0; i < columnWidths.Length - 1; i++)
					{
						if (columnWidths[i] != 0)
						{
							if (flag3)
							{
								num2 -= columnWidths[i];
							}
							else
							{
								num2 += columnWidths[i];
							}
							Rectangle rectangle3 = new Rectangle(num2 - num, rectangle2.Top, DesignerUtils.RESIZEGLYPHSIZE, rectangle2.Height);
							if (rectangle2.Contains(rectangle3) && this.Table.ColumnStyles[i] != null)
							{
								TableLayoutPanelResizeGlyph value = new TableLayoutPanelResizeGlyph(rectangle3, this.Table.ColumnStyles[i], Cursors.VSplit, this.Behavior);
								glyphs.Add(value);
							}
						}
					}
					num2 = rectangle.Y;
					for (int j = 0; j < rowHeights.Length - 1; j++)
					{
						if (rowHeights[j] != 0)
						{
							num2 += rowHeights[j];
							Rectangle rectangle4 = new Rectangle(rectangle2.Left, num2 - num, rectangle2.Width, DesignerUtils.RESIZEGLYPHSIZE);
							if (rectangle2.Contains(rectangle4) && this.Table.RowStyles[j] != null)
							{
								TableLayoutPanelResizeGlyph value2 = new TableLayoutPanelResizeGlyph(rectangle4, this.Table.RowStyles[j], Cursors.HSplit, this.Behavior);
								glyphs.Add(value2);
							}
						}
					}
				}
			}
			return glyphs;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000C8430 File Offset: 0x000C6630
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				designerHost.TransactionClosing += this.OnTransactionClosing;
				this.compSvc = (designerHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService);
			}
			if (this.compSvc != null)
			{
				this.compSvc.ComponentChanging += this.OnComponentChanging;
				this.compSvc.ComponentChanged += this.OnComponentChanged;
			}
			this.Control.ControlAdded += this.OnControlAdded;
			this.Control.ControlRemoved += this.OnControlRemoved;
			this.rowStyleProp = TypeDescriptor.GetProperties(this.Table)["RowStyles"];
			this.colStyleProp = TypeDescriptor.GetProperties(this.Table)["ColumnStyles"];
			if (this.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
			{
				for (int i = 0; i < this.Control.Controls.Count; i++)
				{
					TypeDescriptor.AddAttributes(this.Control.Controls[i], new Attribute[]
					{
						InheritanceAttribute.InheritedReadOnly
					});
				}
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000A9391 File Offset: 0x000A7591
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (base.InheritanceAttribute == InheritanceAttribute.Inherited || base.InheritanceAttribute == InheritanceAttribute.InheritedReadOnly)
				{
					return InheritanceAttribute.InheritedReadOnly;
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000C8571 File Offset: 0x000C6771
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			this.CreateEmptyTable();
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000C8580 File Offset: 0x000C6780
		protected override IComponent[] CreateToolCore(ToolboxItem tool, int x, int y, int width, int height, bool hasLocation, bool hasSize)
		{
			this.rowCountBeforeAdd = Math.Max(0, this.Table.GetRowHeights().Length);
			this.colCountBeforeAdd = Math.Max(0, this.Table.GetColumnWidths().Length);
			return base.CreateToolCore(tool, x, y, width, height, hasLocation, hasSize);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000C85D0 File Offset: 0x000C67D0
		private void OnControlAdded(object sender, ControlEventArgs e)
		{
			if (this.IsLoading || this.Undoing)
			{
				return;
			}
			int num = 0;
			int[] rowHeights = this.Table.GetRowHeights();
			int[] columnWidths = this.Table.GetColumnWidths();
			for (int i = 0; i < rowHeights.Length; i++)
			{
				for (int j = 0; j < columnWidths.Length; j++)
				{
					if (this.Table.GetControlFromPosition(j, i) != null)
					{
						num++;
					}
				}
			}
			bool fullTable = num - 1 >= Math.Max(1, this.colCountBeforeAdd) * Math.Max(1, this.rowCountBeforeAdd);
			if (this.droppedCellPosition == ControlDesigner.InvalidPoint)
			{
				this.droppedCellPosition = this.GetControlPosition(e.Control);
			}
			this.ControlAddedInternal(e.Control, this.droppedCellPosition, false, fullTable, null);
			this.droppedCellPosition = ControlDesigner.InvalidPoint;
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000C86A7 File Offset: 0x000C68A7
		private void OnControlRemoved(object sender, ControlEventArgs e)
		{
			if (e != null && e.Control != null)
			{
				this.Table.SetCellPosition(e.Control, new TableLayoutPanelCellPosition(-1, -1));
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000C86CC File Offset: 0x000C68CC
		private bool IsOverValidCell(bool dragOp)
		{
			Point cellPosition = this.GetCellPosition(Control.MousePosition);
			int[] rowHeights = this.Table.GetRowHeights();
			int[] columnWidths = this.Table.GetColumnWidths();
			if (cellPosition.Y < 0 || cellPosition.Y >= rowHeights.Length || cellPosition.X < 0 || cellPosition.X >= columnWidths.Length)
			{
				return false;
			}
			if (dragOp)
			{
				Control controlFromPosition = ((TableLayoutPanel)this.Control).GetControlFromPosition(cellPosition.X, cellPosition.Y);
				if ((controlFromPosition != null && this.localDragControl == null) || (this.localDragControl != null && this.dragComps.Count > 1) || (this.localDragControl != null && controlFromPosition != null && Control.ModifierKeys == Keys.Control))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000C8788 File Offset: 0x000C6988
		protected override void OnContextMenu(int x, int y)
		{
			Point cellPosition = this.GetCellPosition(new Point(x, y));
			this.curRow = cellPosition.Y;
			this.curCol = cellPosition.X;
			this.EnsureAvailableStyles();
			this.DesignerContextMenuStrip.Show(x, y);
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000C87D4 File Offset: 0x000C69D4
		protected override void OnDragEnter(DragEventArgs de)
		{
			base.OnDragEnter(de);
			if (this.localDragControl == null)
			{
				Control control = this.ExtractControlFromDragEvent(de);
				if (control != null && this.Table.Controls.Contains(control))
				{
					this.localDragControl = control;
				}
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000C8815 File Offset: 0x000C6A15
		protected override void OnDragLeave(EventArgs e)
		{
			this.localDragControl = null;
			this.dragComps = null;
			base.OnDragLeave(e);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000C882C File Offset: 0x000C6A2C
		protected override void OnDragDrop(DragEventArgs de)
		{
			this.droppedCellPosition = this.GetCellPosition(Control.MousePosition);
			if (this.localDragControl != null)
			{
				this.ControlAddedInternal(this.localDragControl, this.droppedCellPosition, true, false, de);
				this.localDragControl = null;
			}
			else
			{
				this.rowCountBeforeAdd = Math.Max(0, this.Table.GetRowHeights().Length);
				this.colCountBeforeAdd = Math.Max(0, this.Table.GetColumnWidths().Length);
				base.OnDragDrop(de);
				if (this.dragComps != null)
				{
					foreach (object obj in this.dragComps)
					{
						Control control = (Control)obj;
						if (control != null)
						{
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["ColumnSpan"];
							PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(control)["RowSpan"];
							if (propertyDescriptor != null)
							{
								propertyDescriptor.SetValue(control, 1);
							}
							if (propertyDescriptor2 != null)
							{
								propertyDescriptor2.SetValue(control, 1);
							}
						}
					}
				}
			}
			this.droppedCellPosition = ControlDesigner.InvalidPoint;
			this.dragComps = null;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000C8954 File Offset: 0x000C6B54
		protected override void OnDragOver(DragEventArgs de)
		{
			if (!this.IsOverValidCell(true))
			{
				de.Effect = DragDropEffects.None;
				return;
			}
			base.OnDragOver(de);
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000C8970 File Offset: 0x000C6B70
		private Dictionary<string, bool> ExtenderProperties
		{
			get
			{
				if (this.extenderProperties == null && base.Component != null)
				{
					this.extenderProperties = new Dictionary<string, bool>();
					AttributeCollection attributes = TypeDescriptor.GetAttributes(base.Component.GetType());
					foreach (object obj in attributes)
					{
						Attribute attribute = (Attribute)obj;
						ProvidePropertyAttribute providePropertyAttribute = attribute as ProvidePropertyAttribute;
						if (providePropertyAttribute != null)
						{
							this.extenderProperties[providePropertyAttribute.PropertyName] = true;
						}
					}
				}
				return this.extenderProperties;
			}
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000C8A10 File Offset: 0x000C6C10
		private bool DoesPropertyAffectPosition(MemberDescriptor member)
		{
			bool result = false;
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = member.Attributes[typeof(DesignerSerializationVisibilityAttribute)] as DesignerSerializationVisibilityAttribute;
			if (designerSerializationVisibilityAttribute != null)
			{
				result = (designerSerializationVisibilityAttribute.Visibility == DesignerSerializationVisibility.Hidden && this.ExtenderProperties.ContainsKey(member.Name));
			}
			return result;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000C8A5C File Offset: 0x000C6C5C
		private void OnComponentChanging(object sender, ComponentChangingEventArgs e)
		{
			Control control = e.Component as Control;
			if (control != null && control.Parent == base.Component && e.Member != null && this.DoesPropertyAffectPosition(e.Member))
			{
				PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Controls"];
				this.compSvc.OnComponentChanging(base.Component, member);
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000C8AC4 File Offset: 0x000C6CC4
		private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
		{
			if (e.Component != null)
			{
				Control control = e.Component as Control;
				if (control != null && control.Parent != null && control.Parent.Equals(this.Control) && e.Member != null && (e.Member.Name == "Row" || e.Member.Name == "Column"))
				{
					this.EnsureAvailableStyles();
				}
				if (control != null && control.Parent == base.Component && e.Member != null && this.DoesPropertyAffectPosition(e.Member))
				{
					PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Controls"];
					this.compSvc.OnComponentChanged(base.Component, member, null, null);
				}
			}
			this.CheckVerbStatus();
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000C8B9C File Offset: 0x000C6D9C
		private void OnTransactionClosing(object sender, DesignerTransactionCloseEventArgs e)
		{
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			if (selectionService != null && this.Table != null)
			{
				ICollection selectedComponents = selectionService.GetSelectedComponents();
				bool flag = false;
				foreach (object obj in selectedComponents)
				{
					Control control = obj as Control;
					if (control != null && control.Parent == this.Table)
					{
						flag = true;
						break;
					}
				}
				if (selectionService.GetComponentSelected(this.Table) || flag)
				{
					this.Table.SuspendLayout();
					this.EnsureAvailableStyles();
					this.Table.ResumeLayout(false);
					this.Table.PerformLayout();
				}
			}
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000C8C74 File Offset: 0x000C6E74
		private void OnUndoing(object sender, EventArgs e)
		{
			if (!this.Undoing)
			{
				if (this.undoEngine != null)
				{
					this.undoEngine.Undone += this.OnUndone;
				}
				this.Undoing = true;
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000C8CA4 File Offset: 0x000C6EA4
		private void OnUndone(object sender, EventArgs e)
		{
			if (this.Undoing)
			{
				if (this.undoEngine != null)
				{
					this.undoEngine.Undone -= this.OnUndone;
				}
				this.Undoing = false;
				bool flag = this.EnsureAvailableStyles();
				if (flag)
				{
					this.Refresh();
				}
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000C8CF0 File Offset: 0x000C6EF0
		protected override void OnMouseDragBegin(int x, int y)
		{
			if (this.IsOverValidCell(true))
			{
				IToolboxService toolboxService = (IToolboxService)this.GetService(typeof(IToolboxService));
				if (toolboxService != null && toolboxService.GetSelectedToolboxItem((IDesignerHost)this.GetService(typeof(IDesignerHost))) != null)
				{
					this.droppedCellPosition = this.GetCellPosition(Control.MousePosition);
				}
			}
			else
			{
				this.droppedCellPosition = ControlDesigner.InvalidPoint;
				Cursor.Current = Cursors.No;
			}
			base.OnMouseDragBegin(x, y);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000C8D6C File Offset: 0x000C6F6C
		protected override void OnMouseDragMove(int x, int y)
		{
			if (this.droppedCellPosition == ControlDesigner.InvalidPoint)
			{
				Cursor.Current = Cursors.No;
				return;
			}
			base.OnMouseDragMove(x, y);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000C8D93 File Offset: 0x000C6F93
		protected override void OnMouseDragEnd(bool cancel)
		{
			if (this.droppedCellPosition == ControlDesigner.InvalidPoint)
			{
				cancel = true;
			}
			base.OnMouseDragEnd(cancel);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000C8DB4 File Offset: 0x000C6FB4
		private void OnRowColMenuOpening(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			ToolStripDropDownMenu toolStripDropDownMenu = sender as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null)
			{
				int num = 0;
				ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
				if (selectionService != null)
				{
					num = selectionService.SelectionCount;
				}
				bool enabled = num == 1 && this.InheritanceAttribute != InheritanceAttribute.InheritedReadOnly;
				toolStripDropDownMenu.Items["add"].Enabled = enabled;
				toolStripDropDownMenu.Items["insert"].Enabled = enabled;
				toolStripDropDownMenu.Items["delete"].Enabled = enabled;
				toolStripDropDownMenu.Items["sizemode"].Enabled = enabled;
				toolStripDropDownMenu.Items["absolute"].Enabled = enabled;
				toolStripDropDownMenu.Items["percent"].Enabled = enabled;
				toolStripDropDownMenu.Items["autosize"].Enabled = enabled;
				if (num == 1)
				{
					((ToolStripMenuItem)toolStripDropDownMenu.Items["absolute"]).Checked = false;
					((ToolStripMenuItem)toolStripDropDownMenu.Items["percent"]).Checked = false;
					((ToolStripMenuItem)toolStripDropDownMenu.Items["autosize"]).Checked = false;
					bool flag = (bool)toolStripDropDownMenu.Tag;
					switch (flag ? this.Table.RowStyles[this.curRow].SizeType : this.Table.ColumnStyles[this.curCol].SizeType)
					{
					case SizeType.AutoSize:
						((ToolStripMenuItem)toolStripDropDownMenu.Items["autosize"]).Checked = true;
						break;
					case SizeType.Absolute:
						((ToolStripMenuItem)toolStripDropDownMenu.Items["absolute"]).Checked = true;
						break;
					case SizeType.Percent:
						((ToolStripMenuItem)toolStripDropDownMenu.Items["percent"]).Checked = true;
						break;
					}
					if ((flag ? this.Table.RowCount : this.Table.ColumnCount) < 2)
					{
						toolStripDropDownMenu.Items["delete"].Enabled = false;
					}
				}
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000C8FEC File Offset: 0x000C71EC
		private void OnAdd(bool isRow)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null && this.Table.Site != null)
			{
				using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString(isRow ? "TableLayoutPanelDesignerAddRowUndoUnit" : "TableLayoutPanelDesignerAddColumnUndoUnit", new object[]
				{
					this.Table.Site.Name
				})))
				{
					try
					{
						this.Table.SuspendLayout();
						this.InsertRowCol(isRow, isRow ? this.Table.RowCount : this.Table.ColumnCount);
						this.Table.ResumeLayout();
						designerTransaction.Commit();
					}
					catch (CheckoutException obj)
					{
						if (!CheckoutException.Canceled.Equals(obj))
						{
							throw;
						}
						if (designerTransaction != null)
						{
							designerTransaction.Cancel();
						}
					}
				}
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000C90DC File Offset: 0x000C72DC
		private void OnAddClick(object sender, EventArgs e)
		{
			this.OnAdd((bool)((ToolStripMenuItem)sender).Tag);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000C90F4 File Offset: 0x000C72F4
		internal void InsertRowCol(bool isRow, int index)
		{
			try
			{
				if (isRow)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.Table)["RowCount"];
					if (propertyDescriptor != null)
					{
						this.PropChanging(this.rowStyleProp);
						this.Table.RowStyles.Insert(index, new RowStyle(SizeType.Absolute, (float)DesignerUtils.MINIMUMSTYLESIZE));
						this.PropChanged(this.rowStyleProp);
						propertyDescriptor.SetValue(this.Table, this.Table.RowCount + 1);
					}
				}
				else
				{
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(this.Table)["ColumnCount"];
					if (propertyDescriptor2 != null)
					{
						this.PropChanging(this.colStyleProp);
						this.Table.ColumnStyles.Insert(index, new ColumnStyle(SizeType.Absolute, (float)DesignerUtils.MINIMUMSTYLESIZE));
						this.PropChanged(this.colStyleProp);
						propertyDescriptor2.SetValue(this.Table, this.Table.ColumnCount + 1);
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				iuiservice.ShowError(ex.Message);
			}
			base.BehaviorService.Invalidate(base.BehaviorService.ControlRectInAdornerWindow(this.Control));
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000C9234 File Offset: 0x000C7434
		internal void FixUpControlsOnInsert(bool isRow, int index)
		{
			PropertyDescriptor prop = TypeDescriptor.GetProperties(this.Table)["Controls"];
			this.PropChanging(prop);
			foreach (object obj in this.Table.Controls)
			{
				Control control = (Control)obj;
				int num = isRow ? this.Table.GetRow(control) : this.Table.GetColumn(control);
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)[isRow ? "Row" : "Column"];
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(control)[isRow ? "RowSpan" : "ColumnSpan"];
				if (num != -1)
				{
					if (num >= index)
					{
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(control, num + 1);
						}
					}
					else
					{
						int num2 = isRow ? this.Table.GetRowSpan(control) : this.Table.GetColumnSpan(control);
						if (num + num2 > index && propertyDescriptor2 != null)
						{
							propertyDescriptor2.SetValue(control, num2 + 1);
						}
					}
				}
			}
			this.PropChanged(prop);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000C9368 File Offset: 0x000C7568
		private void OnInsertClick(object sender, EventArgs e)
		{
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null && this.Table.Site != null)
			{
				bool flag = (bool)((ToolStripMenuItem)sender).Tag;
				using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString(flag ? "TableLayoutPanelDesignerAddRowUndoUnit" : "TableLayoutPanelDesignerAddColumnUndoUnit", new object[]
				{
					this.Table.Site.Name
				})))
				{
					try
					{
						this.Table.SuspendLayout();
						this.InsertRowCol(flag, flag ? this.curRow : this.curCol);
						this.FixUpControlsOnInsert(flag, flag ? this.curRow : this.curCol);
						this.Table.ResumeLayout();
						designerTransaction.Commit();
					}
					catch (CheckoutException obj)
					{
						if (!CheckoutException.Canceled.Equals(obj))
						{
							throw;
						}
						if (designerTransaction != null)
						{
							designerTransaction.Cancel();
						}
					}
					catch (InvalidOperationException ex)
					{
						IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
						iuiservice.ShowError(ex.Message);
					}
				}
			}
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000C94AC File Offset: 0x000C76AC
		internal void FixUpControlsOnDelete(bool isRow, int index, ArrayList deleteList)
		{
			PropertyDescriptor prop = TypeDescriptor.GetProperties(this.Table)["Controls"];
			this.PropChanging(prop);
			foreach (object obj in this.Table.Controls)
			{
				Control control = (Control)obj;
				int num = isRow ? this.Table.GetRow(control) : this.Table.GetColumn(control);
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)[isRow ? "Row" : "Column"];
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(control)[isRow ? "RowSpan" : "ColumnSpan"];
				if (num == index)
				{
					if (!deleteList.Contains(control))
					{
						deleteList.Add(control);
					}
				}
				else if (num != -1 && !deleteList.Contains(control))
				{
					if (num > index)
					{
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(control, num - 1);
						}
					}
					else
					{
						int num2 = isRow ? this.Table.GetRowSpan(control) : this.Table.GetColumnSpan(control);
						if (num + num2 > index && propertyDescriptor2 != null)
						{
							propertyDescriptor2.SetValue(control, num2 - 1);
						}
					}
				}
			}
			this.PropChanged(prop);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000C9600 File Offset: 0x000C7800
		internal void DeleteRowCol(bool isRow, int index)
		{
			if (isRow)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.Table)["RowCount"];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(this.Table, this.Table.RowCount - 1);
					this.PropChanging(this.rowStyleProp);
					this.Table.RowStyles.RemoveAt(index);
					this.PropChanged(this.rowStyleProp);
					return;
				}
			}
			else
			{
				PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(this.Table)["ColumnCount"];
				if (propertyDescriptor2 != null)
				{
					propertyDescriptor2.SetValue(this.Table, this.Table.ColumnCount - 1);
					this.PropChanging(this.colStyleProp);
					this.Table.ColumnStyles.RemoveAt(index);
					this.PropChanged(this.colStyleProp);
				}
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000C96D4 File Offset: 0x000C78D4
		private void OnRemoveInternal(bool isRow, int index)
		{
			if ((isRow ? this.Table.RowCount : this.Table.ColumnCount) < 2)
			{
				return;
			}
			IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
			if (designerHost != null && this.Table.Site != null)
			{
				using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString(isRow ? "TableLayoutPanelDesignerRemoveRowUndoUnit" : "TableLayoutPanelDesignerRemoveColumnUndoUnit", new object[]
				{
					this.Table.Site.Name
				})))
				{
					try
					{
						this.Table.SuspendLayout();
						ArrayList arrayList = new ArrayList();
						this.FixUpControlsOnDelete(isRow, index, arrayList);
						this.DeleteRowCol(isRow, index);
						if (arrayList.Count > 0)
						{
							PropertyDescriptor prop = TypeDescriptor.GetProperties(this.Table)["Controls"];
							this.PropChanging(prop);
							foreach (object obj in arrayList)
							{
								ArrayList arrayList2 = new ArrayList();
								DesignerUtils.GetAssociatedComponents((IComponent)obj, designerHost, arrayList2);
								foreach (object obj2 in arrayList2)
								{
									IComponent component = (IComponent)obj2;
									this.compSvc.OnComponentChanging(component, null);
								}
								designerHost.DestroyComponent(obj as Component);
							}
							this.PropChanged(prop);
						}
						this.Table.ResumeLayout();
						designerTransaction.Commit();
					}
					catch (CheckoutException obj3)
					{
						if (!CheckoutException.Canceled.Equals(obj3))
						{
							throw;
						}
						if (designerTransaction != null)
						{
							designerTransaction.Cancel();
						}
					}
				}
			}
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000C98F0 File Offset: 0x000C7AF0
		private void OnRemove(bool isRow)
		{
			this.OnRemoveInternal(isRow, isRow ? (this.Table.RowCount - 1) : (this.Table.ColumnCount - 1));
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000C9918 File Offset: 0x000C7B18
		private void OnDeleteClick(object sender, EventArgs e)
		{
			try
			{
				bool flag = (bool)((ToolStripMenuItem)sender).Tag;
				this.OnRemoveInternal(flag, flag ? this.curRow : this.curCol);
			}
			catch (InvalidOperationException ex)
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				iuiservice.ShowError(ex.Message);
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000C9988 File Offset: 0x000C7B88
		private void ChangeSizeType(bool isRow, SizeType newType)
		{
			try
			{
				TableLayoutStyleCollection tableLayoutStyleCollection;
				if (isRow)
				{
					tableLayoutStyleCollection = this.Table.RowStyles;
				}
				else
				{
					tableLayoutStyleCollection = this.Table.ColumnStyles;
				}
				int num = isRow ? this.curRow : this.curCol;
				if (tableLayoutStyleCollection[num].SizeType != newType)
				{
					int[] rowHeights = this.Table.GetRowHeights();
					int[] columnWidths = this.Table.GetColumnWidths();
					if ((!isRow || rowHeights.Length >= num - 1) && (isRow || columnWidths.Length >= num - 1))
					{
						IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
						if (designerHost != null && this.Table.Site != null)
						{
							using (DesignerTransaction designerTransaction = designerHost.CreateTransaction(SR.GetString("TableLayoutPanelDesignerChangeSizeTypeUndoUnit", new object[]
							{
								this.Table.Site.Name
							})))
							{
								try
								{
									this.Table.SuspendLayout();
									this.PropChanging(isRow ? this.rowStyleProp : this.colStyleProp);
									switch (newType)
									{
									case SizeType.AutoSize:
										tableLayoutStyleCollection[num].SizeType = SizeType.AutoSize;
										break;
									case SizeType.Absolute:
										tableLayoutStyleCollection[num].SizeType = SizeType.Absolute;
										if (isRow)
										{
											this.Table.RowStyles[num].Height = (float)rowHeights[num];
										}
										else
										{
											this.Table.ColumnStyles[num].Width = (float)columnWidths[num];
										}
										break;
									case SizeType.Percent:
										tableLayoutStyleCollection[num].SizeType = SizeType.Percent;
										if (isRow)
										{
											this.Table.RowStyles[num].Height = (float)DesignerUtils.MINIMUMSTYLEPERCENT;
										}
										else
										{
											this.Table.ColumnStyles[num].Width = (float)DesignerUtils.MINIMUMSTYLEPERCENT;
										}
										break;
									}
									this.PropChanged(isRow ? this.rowStyleProp : this.colStyleProp);
									this.Table.ResumeLayout();
									designerTransaction.Commit();
								}
								catch (CheckoutException obj)
								{
									if (!CheckoutException.Canceled.Equals(obj))
									{
										throw;
									}
									if (designerTransaction != null)
									{
										designerTransaction.Cancel();
									}
								}
							}
						}
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				iuiservice.ShowError(ex.Message);
			}
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000C9C14 File Offset: 0x000C7E14
		private void OnAbsoluteClick(object sender, EventArgs e)
		{
			this.ChangeSizeType((bool)((ToolStripMenuItem)sender).Tag, SizeType.Absolute);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000C9C2D File Offset: 0x000C7E2D
		private void OnPercentClick(object sender, EventArgs e)
		{
			this.ChangeSizeType((bool)((ToolStripMenuItem)sender).Tag, SizeType.Percent);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000C9C46 File Offset: 0x000C7E46
		private void OnAutoSizeClick(object sender, EventArgs e)
		{
			this.ChangeSizeType((bool)((ToolStripMenuItem)sender).Tag, SizeType.AutoSize);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000C9C60 File Offset: 0x000C7E60
		private void OnEdit()
		{
			try
			{
				EditorServiceContext.EditValue(this, this.Table, "ColumnStyles");
			}
			catch (InvalidOperationException ex)
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				iuiservice.ShowError(ex.Message);
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000C9CB8 File Offset: 0x000C7EB8
		private string ReplaceText(string text)
		{
			if (text != null)
			{
				return Regex.Replace(text, "\\(\\&.\\)", "");
			}
			return null;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000C9CD0 File Offset: 0x000C7ED0
		private void OnVerbRemove(object sender, EventArgs e)
		{
			bool isRow = ((DesignerVerb)sender).Text.Equals(this.ReplaceText(SR.GetString("TableLayoutPanelDesignerRemoveRow")));
			this.OnRemove(isRow);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000C9D08 File Offset: 0x000C7F08
		private void OnVerbAdd(object sender, EventArgs e)
		{
			bool isRow = ((DesignerVerb)sender).Text.Equals(this.ReplaceText(SR.GetString("TableLayoutPanelDesignerAddRow")));
			this.OnAdd(isRow);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000C9D3D File Offset: 0x000C7F3D
		private void OnVerbEdit(object sender, EventArgs e)
		{
			this.OnEdit();
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000C9D48 File Offset: 0x000C7F48
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"ColumnStyles",
				"RowStyles",
				"ColumnCount",
				"RowCount"
			};
			Attribute[] attributes = new Attribute[]
			{
				new BrowsableAttribute(true)
			};
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(TableLayoutPanelDesigner), propertyDescriptor, attributes);
				}
			}
			PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties["Controls"];
			if (propertyDescriptor2 != null)
			{
				Attribute[] array2 = new Attribute[propertyDescriptor2.Attributes.Count];
				propertyDescriptor2.Attributes.CopyTo(array2, 0);
				properties["Controls"] = TypeDescriptor.CreateProperty(typeof(TableLayoutPanelDesigner), "Controls", typeof(TableLayoutPanelDesigner.DesignerTableLayoutControlCollection), array2);
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000C9E2F File Offset: 0x000C802F
		private void Refresh()
		{
			base.BehaviorService.SyncSelection();
			if (this.Table != null)
			{
				this.Table.Invalidate(true);
			}
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000C9E50 File Offset: 0x000C8050
		private void PropChanging(PropertyDescriptor prop)
		{
			if (this.compSvc != null && prop != null)
			{
				this.compSvc.OnComponentChanging(this.Table, prop);
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000C9E6F File Offset: 0x000C806F
		private void PropChanged(PropertyDescriptor prop)
		{
			if (this.compSvc != null && prop != null)
			{
				this.compSvc.OnComponentChanged(this.Table, prop, null, null);
			}
		}

		// Token: 0x040018FE RID: 6398
		private TableLayoutPanelBehavior tlpBehavior;

		// Token: 0x040018FF RID: 6399
		private Point droppedCellPosition = ControlDesigner.InvalidPoint;

		// Token: 0x04001900 RID: 6400
		private bool undoing;

		// Token: 0x04001901 RID: 6401
		private UndoEngine undoEngine;

		// Token: 0x04001902 RID: 6402
		private Control localDragControl;

		// Token: 0x04001903 RID: 6403
		private ArrayList dragComps;

		// Token: 0x04001904 RID: 6404
		private DesignerVerbCollection verbs;

		// Token: 0x04001905 RID: 6405
		private TableLayoutPanelDesigner.DesignerTableLayoutControlCollection controls;

		// Token: 0x04001906 RID: 6406
		private DesignerVerb removeRowVerb;

		// Token: 0x04001907 RID: 6407
		private DesignerVerb removeColVerb;

		// Token: 0x04001908 RID: 6408
		private DesignerActionListCollection actionLists;

		// Token: 0x04001909 RID: 6409
		private BaseContextMenuStrip designerContextMenuStrip;

		// Token: 0x0400190A RID: 6410
		private int curRow = -1;

		// Token: 0x0400190B RID: 6411
		private int curCol = -1;

		// Token: 0x0400190C RID: 6412
		private IComponentChangeService compSvc;

		// Token: 0x0400190D RID: 6413
		private PropertyDescriptor rowStyleProp;

		// Token: 0x0400190E RID: 6414
		private PropertyDescriptor colStyleProp;

		// Token: 0x0400190F RID: 6415
		private int rowCountBeforeAdd;

		// Token: 0x04001910 RID: 6416
		private int colCountBeforeAdd;

		// Token: 0x04001911 RID: 6417
		private ToolStripMenuItem contextMenuRow;

		// Token: 0x04001912 RID: 6418
		private ToolStripMenuItem contextMenuCol;

		// Token: 0x04001913 RID: 6419
		private int ensureSuspendCount;

		// Token: 0x04001914 RID: 6420
		private Dictionary<string, bool> extenderProperties;

		// Token: 0x02000592 RID: 1426
		private class TableLayouPanelRowColumnActionList : DesignerActionList
		{
			// Token: 0x060032FE RID: 13054 RVA: 0x00116308 File Offset: 0x00114508
			public TableLayouPanelRowColumnActionList(TableLayoutPanelDesigner owner) : base(owner.Component)
			{
				this.owner = owner;
			}

			// Token: 0x060032FF RID: 13055 RVA: 0x00116320 File Offset: 0x00114520
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "AddColumn", SR.GetString("TableLayoutPanelDesignerAddColumn"), false));
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "AddRow", SR.GetString("TableLayoutPanelDesignerAddRow"), false));
				if (this.owner.Table.ColumnCount > 1)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "RemoveColumn", SR.GetString("TableLayoutPanelDesignerRemoveColumn"), false));
				}
				if (this.owner.Table.RowCount > 1)
				{
					designerActionItemCollection.Add(new DesignerActionMethodItem(this, "RemoveRow", SR.GetString("TableLayoutPanelDesignerRemoveRow"), false));
				}
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditRowAndCol", SR.GetString("TableLayoutPanelDesignerEditRowAndCol"), false));
				return designerActionItemCollection;
			}

			// Token: 0x06003300 RID: 13056 RVA: 0x001163EB File Offset: 0x001145EB
			public void AddColumn()
			{
				this.owner.OnAdd(false);
			}

			// Token: 0x06003301 RID: 13057 RVA: 0x001163F9 File Offset: 0x001145F9
			public void AddRow()
			{
				this.owner.OnAdd(true);
			}

			// Token: 0x06003302 RID: 13058 RVA: 0x00116407 File Offset: 0x00114607
			public void RemoveColumn()
			{
				this.owner.OnRemove(false);
			}

			// Token: 0x06003303 RID: 13059 RVA: 0x00116415 File Offset: 0x00114615
			public void RemoveRow()
			{
				this.owner.OnRemove(true);
			}

			// Token: 0x06003304 RID: 13060 RVA: 0x00116423 File Offset: 0x00114623
			public void EditRowAndCol()
			{
				this.owner.OnEdit();
			}

			// Token: 0x04002229 RID: 8745
			private TableLayoutPanelDesigner owner;
		}

		// Token: 0x02000593 RID: 1427
		[ListBindable(false)]
		[DesignerSerializer(typeof(TableLayoutPanelDesigner.DesignerTableLayoutControlCollectionCodeDomSerializer), typeof(CodeDomSerializer))]
		internal class DesignerTableLayoutControlCollection : TableLayoutControlCollection, IList, ICollection, IEnumerable
		{
			// Token: 0x06003305 RID: 13061 RVA: 0x00116430 File Offset: 0x00114630
			public DesignerTableLayoutControlCollection(TableLayoutPanel owner) : base(owner)
			{
				this.realCollection = owner.Controls;
			}

			// Token: 0x170009F8 RID: 2552
			// (get) Token: 0x06003306 RID: 13062 RVA: 0x00116445 File Offset: 0x00114645
			public override int Count
			{
				get
				{
					return this.realCollection.Count;
				}
			}

			// Token: 0x170009F9 RID: 2553
			// (get) Token: 0x06003307 RID: 13063 RVA: 0x0000CA50 File Offset: 0x0000AC50
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170009FA RID: 2554
			// (get) Token: 0x06003308 RID: 13064 RVA: 0x0000445B File Offset: 0x0000265B
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009FB RID: 2555
			// (get) Token: 0x06003309 RID: 13065 RVA: 0x0000445B File Offset: 0x0000265B
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170009FC RID: 2556
			// (get) Token: 0x0600330A RID: 13066 RVA: 0x00116452 File Offset: 0x00114652
			public new bool IsReadOnly
			{
				get
				{
					return this.realCollection.IsReadOnly;
				}
			}

			// Token: 0x0600330B RID: 13067 RVA: 0x0011645F File Offset: 0x0011465F
			int IList.Add(object control)
			{
				return ((IList)this.realCollection).Add(control);
			}

			// Token: 0x0600330C RID: 13068 RVA: 0x0011646D File Offset: 0x0011466D
			public override void Add(Control c)
			{
				this.realCollection.Add(c);
			}

			// Token: 0x0600330D RID: 13069 RVA: 0x0011647B File Offset: 0x0011467B
			public override void AddRange(Control[] controls)
			{
				this.realCollection.AddRange(controls);
			}

			// Token: 0x0600330E RID: 13070 RVA: 0x00116489 File Offset: 0x00114689
			bool IList.Contains(object control)
			{
				return ((IList)this.realCollection).Contains(control);
			}

			// Token: 0x0600330F RID: 13071 RVA: 0x00116497 File Offset: 0x00114697
			public new void CopyTo(Array dest, int index)
			{
				this.realCollection.CopyTo(dest, index);
			}

			// Token: 0x06003310 RID: 13072 RVA: 0x001164A6 File Offset: 0x001146A6
			public override bool Equals(object other)
			{
				return this.realCollection.Equals(other);
			}

			// Token: 0x06003311 RID: 13073 RVA: 0x001164B4 File Offset: 0x001146B4
			public new IEnumerator GetEnumerator()
			{
				return this.realCollection.GetEnumerator();
			}

			// Token: 0x06003312 RID: 13074 RVA: 0x001164C1 File Offset: 0x001146C1
			public override int GetHashCode()
			{
				return this.realCollection.GetHashCode();
			}

			// Token: 0x06003313 RID: 13075 RVA: 0x001164CE File Offset: 0x001146CE
			int IList.IndexOf(object control)
			{
				return ((IList)this.realCollection).IndexOf(control);
			}

			// Token: 0x06003314 RID: 13076 RVA: 0x001164DC File Offset: 0x001146DC
			void IList.Insert(int index, object value)
			{
				((IList)this.realCollection).Insert(index, value);
			}

			// Token: 0x06003315 RID: 13077 RVA: 0x001164EB File Offset: 0x001146EB
			void IList.Remove(object control)
			{
				((IList)this.realCollection).Remove(control);
			}

			// Token: 0x06003316 RID: 13078 RVA: 0x001164F9 File Offset: 0x001146F9
			void IList.RemoveAt(int index)
			{
				((IList)this.realCollection).RemoveAt(index);
			}

			// Token: 0x170009FD RID: 2557
			object IList.this[int index]
			{
				get
				{
					return ((IList)this.realCollection)[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06003319 RID: 13081 RVA: 0x00116515 File Offset: 0x00114715
			public override void Add(Control control, int column, int row)
			{
				this.realCollection.Add(control, column, row);
			}

			// Token: 0x0600331A RID: 13082 RVA: 0x00116525 File Offset: 0x00114725
			public override int GetChildIndex(Control child, bool throwException)
			{
				return this.realCollection.GetChildIndex(child, throwException);
			}

			// Token: 0x0600331B RID: 13083 RVA: 0x00116534 File Offset: 0x00114734
			public override void SetChildIndex(Control child, int newIndex)
			{
				this.realCollection.SetChildIndex(child, newIndex);
			}

			// Token: 0x0600331C RID: 13084 RVA: 0x00116544 File Offset: 0x00114744
			public override void Clear()
			{
				for (int i = this.realCollection.Count - 1; i >= 0; i--)
				{
					if (this.realCollection[i] != null && this.realCollection[i].Site != null && TypeDescriptor.GetAttributes(this.realCollection[i]).Contains(InheritanceAttribute.NotInherited))
					{
						this.realCollection.RemoveAt(i);
					}
				}
			}

			// Token: 0x0400222A RID: 8746
			private TableLayoutControlCollection realCollection;
		}

		// Token: 0x02000594 RID: 1428
		internal class DesignerTableLayoutControlCollectionCodeDomSerializer : TableLayoutControlCollectionCodeDomSerializer
		{
			// Token: 0x0600331D RID: 13085 RVA: 0x001165B4 File Offset: 0x001147B4
			protected override object SerializeCollection(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, ICollection originalCollection, ICollection valuesToSerialize)
			{
				ArrayList arrayList = new ArrayList();
				if (valuesToSerialize != null && valuesToSerialize.Count > 0)
				{
					foreach (object obj in valuesToSerialize)
					{
						IComponent component = obj as IComponent;
						if (component != null && component.Site != null && !(component.Site is INestedSite))
						{
							arrayList.Add(component);
						}
					}
				}
				return base.SerializeCollection(manager, targetExpression, targetType, originalCollection, arrayList);
			}
		}
	}
}
