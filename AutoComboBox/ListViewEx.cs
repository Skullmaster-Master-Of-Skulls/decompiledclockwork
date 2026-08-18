using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoComboBox.MyControls;

namespace AutoComboBox
{
	// Token: 0x020000F3 RID: 243
	public class ListViewEx : ListView
	{
		// Token: 0x060009AA RID: 2474
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

		// Token: 0x060009AB RID: 2475
		[DllImport("User32", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, uint wParam, ref ListViewEx.LVCOLUMN lParam);

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060009AC RID: 2476 RVA: 0x0004B5E8 File Offset: 0x0004A5E8
		// (remove) Token: 0x060009AD RID: 2477 RVA: 0x0004B624 File Offset: 0x0004A624
		public new event DrawItemEventHandler DrawItem;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060009AE RID: 2478 RVA: 0x0004B660 File Offset: 0x0004A660
		// (remove) Token: 0x060009AF RID: 2479 RVA: 0x0004B69C File Offset: 0x0004A69C
		public event MeasureItemEventHandler MeasureItem;

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0004B6D8 File Offset: 0x0004A6D8
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0004B6F0 File Offset: 0x0004A6F0
		public bool IsFileList
		{
			get
			{
				return this.isFileList;
			}
			set
			{
				this.isFileList = value;
				this.mi_emailAllFiles.Visible = this.isFileList;
				this.mi_emailSelectedFiles.Visible = this.isFileList;
				this.mi_filesSeparator.Visible = this.isFileList;
				this.mi_openSelectedFile.Visible = this.isFileList;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0004B750 File Offset: 0x0004A750
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0004B768 File Offset: 0x0004A768
		public object Tag2
		{
			get
			{
				return this.tag2;
			}
			set
			{
				this.tag2 = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0004B774 File Offset: 0x0004A774
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x0004B78C File Offset: 0x0004A78C
		public bool NoEditing
		{
			get
			{
				return this.noEditing;
			}
			set
			{
				this.noEditing = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0004B798 File Offset: 0x0004A798
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x0004B7B0 File Offset: 0x0004A7B0
		public bool NoDeleting
		{
			get
			{
				return this.noDeleting;
			}
			set
			{
				this.noDeleting = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0004B7BC File Offset: 0x0004A7BC
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0004B7D4 File Offset: 0x0004A7D4
		public bool AutoSortingEnabled
		{
			get
			{
				return this.autoSortingEnabled;
			}
			set
			{
				if (this.autoSortingEnabled != value)
				{
					this.autoSortingEnabled = value;
					if (this.autoSortingEnabled)
					{
						this.ResetAutoSorting();
					}
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0004B810 File Offset: 0x0004A810
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x0004B828 File Offset: 0x0004A828
		public int DefaultSortByColInd
		{
			get
			{
				return this.defaultSortByColInd;
			}
			set
			{
				this.defaultSortByColInd = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0004B834 File Offset: 0x0004A834
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x0004B84C File Offset: 0x0004A84C
		public bool DefaultSortByAsc
		{
			get
			{
				return this.defaultSortByAsc;
			}
			set
			{
				this.defaultSortByAsc = value;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0004B858 File Offset: 0x0004A858
		private void ResetAutoSorting()
		{
			this.listViewColumnSortings = new bool[base.Columns.Count];
			for (int i = 0; i < this.listViewColumnSortings.Length; i++)
			{
				this.listViewColumnSortings[i] = (i == this.defaultSortByColInd);
			}
			this.lastTaskColumnSorted = this.defaultSortByColInd;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0004B8B4 File Offset: 0x0004A8B4
		public bool AllowFileDropping
		{
			get
			{
				return this.allowFileDropping;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0004B8CC File Offset: 0x0004A8CC
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0004B8E4 File Offset: 0x0004A8E4
		public int CalcButtonCid
		{
			get
			{
				return this.calcButtonCid;
			}
			set
			{
				this.calcButtonCid = value;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0004B8F0 File Offset: 0x0004A8F0
		public void InformCalcButtonOfChange()
		{
			if (this.calcButtonCid > 0)
			{
				Control parent = ListViewEx.GetParent(this);
				Control control = ListViewEx.FindControl(parent, this.calcButtonCid);
				if (control != null && control is MyDynamicControl)
				{
					MyDynamicControl myDynamicControl = (MyDynamicControl)control;
					myDynamicControl.Refresh();
				}
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0004B94C File Offset: 0x0004A94C
		public static Control GetParent(Control start)
		{
			Control parent;
			for (parent = start.Parent; parent != null; parent = parent.Parent)
			{
				if (parent.Parent == null)
				{
					break;
				}
				if (parent.Parent is Form)
				{
					break;
				}
			}
			return parent;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0004B9A8 File Offset: 0x0004A9A8
		public static Control FindControl(Control parent, int cid)
		{
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				Control control = ListViewEx.FindControl(parent2, cid);
				if (control != null)
				{
					return control;
				}
			}
			if (parent.Tag != null && parent.Tag is DataRow)
			{
				DataRow dataRow = (DataRow)parent.Tag;
				if (dataRow.Table.Columns.Contains("controlid"))
				{
					int num = (int)dataRow["controlid"];
					if (num == cid)
					{
						return parent;
					}
				}
			}
			return null;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0004BAA8 File Offset: 0x0004AAA8
		public void AllowUserToDragAFile_WillFireAddNewItem(EventHandler addnewClick)
		{
			this.AddNewClick = addnewClick;
			this.AllowDrop = true;
			this.allowFileDropping = true;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0004BAC4 File Offset: 0x0004AAC4
		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);
			if (this.allowFileDropping)
			{
				if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
				{
					e.Effect = DragDropEffects.All;
				}
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0004BB10 File Offset: 0x0004AB10
		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);
			if (this.allowFileDropping)
			{
				this.droppedFilenames = (string[])e.Data.GetData(DataFormats.FileDrop);
				if (this.AddNewClick != null)
				{
					this.AddNewClick(this, new EventArgs());
				}
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0004BB70 File Offset: 0x0004AB70
		protected override void OnColumnClick(ColumnClickEventArgs e)
		{
			base.OnColumnClick(e);
			if (this.autoSortingEnabled && this.listViewColumnSortings != null && this.listViewColumnSortings.Length >= base.Columns.Count)
			{
				bool flag = !this.listViewColumnSortings[e.Column];
				this.listViewColumnSortings[e.Column] = flag;
				base.ListViewItemSorter = new ListViewMultipleColCompare(new int[]
				{
					e.Column
				}, flag);
				if (this.lastTaskColumnSorted >= 0)
				{
					this.SetHeaderImage(this.lastTaskColumnSorted, -1);
				}
				this.lastTaskColumnSorted = e.Column;
				int imageIndex;
				if (flag)
				{
					imageIndex = 0;
				}
				else
				{
					imageIndex = 1;
				}
				this.SetHeaderImage(e.Column, imageIndex);
				for (int i = 0; i < this.listViewColumnSortings.Length; i++)
				{
					if (i != e.Column)
					{
						this.SetHeaderImage(i, -1);
					}
				}
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0004BC6C File Offset: 0x0004AC6C
		public void SetHeaderImage(int colIndex, int imageIndex)
		{
			IntPtr hWnd = ListViewEx.SendMessage(base.Handle, 4127U, 0U, 0U);
			ListViewEx.SendMessage(hWnd, 4616U, 0U, (uint)((int)this.headerImageList.Handle));
			ListViewEx.LVCOLUMN lvcolumn;
			lvcolumn.mask = 17U;
			lvcolumn.fmt = 2048;
			lvcolumn.fmt |= 4096;
			lvcolumn.iImage = imageIndex;
			lvcolumn.pszText = IntPtr.Zero;
			lvcolumn.cchTextMax = 0;
			lvcolumn.cx = 0;
			lvcolumn.iSubItem = 0;
			lvcolumn.iOrder = 0;
			ListViewEx.SendMessage(base.Handle, 4122U, (uint)colIndex, ref lvcolumn);
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0004BD1C File Offset: 0x0004AD1C
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x0004BD34 File Offset: 0x0004AD34
		public bool EnterTriggersDoubleClickEvent
		{
			get
			{
				return this.enterTriggersDoubleClickEvent;
			}
			set
			{
				this.enterTriggersDoubleClickEvent = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0004BD40 File Offset: 0x0004AD40
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x0004BD58 File Offset: 0x0004AD58
		public ImageList HeaderImageList
		{
			get
			{
				return this.headerImageList;
			}
			set
			{
				this.headerImageList = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0004BD64 File Offset: 0x0004AD64
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0004BD7C File Offset: 0x0004AD7C
		public int ItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
				base.Invalidate();
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0004BD90 File Offset: 0x0004AD90
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0004BDA8 File Offset: 0x0004ADA8
		public Color BackColourSelected
		{
			get
			{
				return this.backColourSelected;
			}
			set
			{
				this.backColourSelected = value;
				this.backColourSelectedBrush = new SolidBrush(this.backColourSelected);
				base.Invalidate();
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0004BDCC File Offset: 0x0004ADCC
		public SolidBrush BackColourSelectedBrush
		{
			get
			{
				if (this.backColourSelectedBrush == null)
				{
					this.backColourSelectedBrush = new SolidBrush(this.BackColourSelected);
				}
				return this.backColourSelectedBrush;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0004BE08 File Offset: 0x0004AE08
		public SolidBrush BackColourBrush
		{
			get
			{
				if (this.backColourBrush == null)
				{
					this.backColourBrush = new SolidBrush(this.BackColor);
				}
				return this.backColourBrush;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0004BE44 File Offset: 0x0004AE44
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= ((this.drawMode != DrawMode.Normal) ? 1024 : 0);
				return createParams;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0004BE7C File Offset: 0x0004AE7C
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0004BE94 File Offset: 0x0004AE94
		public virtual DrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				this.drawMode = value;
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0004BE9E File Offset: 0x0004AE9E
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0004BEA1 File Offset: 0x0004AEA1
		public virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
		}

		// Token: 0x060009D9 RID: 2521
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wPar, IntPtr lPar);

		// Token: 0x060009DA RID: 2522
		[DllImport("user32.dll", CharSet = CharSet.Ansi)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int len, ref int[] order);

		// Token: 0x060009DB RID: 2523
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, IntPtr lParam);

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060009DC RID: 2524 RVA: 0x0004BEA4 File Offset: 0x0004AEA4
		// (remove) Token: 0x060009DD RID: 2525 RVA: 0x0004BEE0 File Offset: 0x0004AEE0
		public event SubItemClickEventHandler SubItemClicked;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060009DE RID: 2526 RVA: 0x0004BF1C File Offset: 0x0004AF1C
		// (remove) Token: 0x060009DF RID: 2527 RVA: 0x0004BF58 File Offset: 0x0004AF58
		public event SubItemClickEventHandler SubItemEndEditing;

		// Token: 0x060009E0 RID: 2528 RVA: 0x0004BF94 File Offset: 0x0004AF94
		public ListViewEx()
		{
			this.InitializeComponent();
			this.MENU_print.Click += this.MENU_print_Click;
			this.MENU_selectAll.Click += this.MENU_selectAll_Click;
			this.MENU_printAll.Click += this.MENU_printAll_Click;
			base.SetStyle(ControlStyles.EnableNotifyMessage, true);
			this.drawMode = DrawMode.Normal;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0004C0E2 File Offset: 0x0004B0E2
		private void MENU_printAll_Click(object sender, EventArgs e)
		{
			this.Print(true);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0004C0F0 File Offset: 0x0004B0F0
		private void MENU_selectAll_Click(object sender, EventArgs e)
		{
			foreach (object obj in base.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Selected = true;
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0004C158 File Offset: 0x0004B158
		private void MENU_print_Click(object sender, EventArgs e)
		{
			this.Print(false);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0004C164 File Offset: 0x0004B164
		private void Print(bool printAll)
		{
			string text = "<br />";
			MyPanel myPanel = MyPanel.FindMyPanel(this);
			StringBuilder stringBuilder = new StringBuilder();
			if (myPanel != null)
			{
				bool flag = string.IsNullOrEmpty(myPanel.PrimaryClientDescription);
				bool flag2 = string.IsNullOrEmpty(myPanel.Caption);
				if (!flag && !flag2)
				{
					if (flag)
					{
						stringBuilder.Append(myPanel.Caption);
					}
					else
					{
						bool flag3 = 0 == 0;
						stringBuilder.AppendFormat("<h1>{0}<br />{1}</h1>", myPanel.Caption, myPanel.PrimaryClientDescription);
					}
				}
			}
			stringBuilder.Append(text + text);
			List<ListViewItem> list = new List<ListViewItem>();
			if (printAll)
			{
				foreach (object obj in base.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					list.Add(listViewItem);
				}
			}
			else
			{
				foreach (object obj2 in base.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj2;
					list.Add(listViewItem);
				}
			}
			foreach (ListViewItem listViewItem in list)
			{
				stringBuilder.AppendFormat("<b>{0}</b>: ", base.Columns[base.Columns.Count - 1].Text);
				ListViewItem listViewItem;
				stringBuilder.Append(listViewItem.SubItems[base.Columns.Count - 1].Text.Replace(Environment.NewLine, "<br />"));
				stringBuilder.Append(text);
				for (int i = 0; i < base.Columns.Count - 1; i++)
				{
					stringBuilder.AppendFormat("<b>{0}</b>: ", base.Columns[i].Text);
					stringBuilder.Append(listViewItem.SubItems[i].Text.Replace(Environment.NewLine, "<br />"));
					stringBuilder.Append(text);
				}
				stringBuilder.Append(text);
			}
			Form form = new Form();
			MyWebBrowser myWebBrowser = new MyWebBrowser();
			form.Controls.Add(myWebBrowser);
			myWebBrowser.Dock = DockStyle.Fill;
			myWebBrowser.HideTitle();
			myWebBrowser.HideRefreshButton();
			myWebBrowser.ShowHtml(stringBuilder.ToString());
			form.WindowState = FormWindowState.Maximized;
			form.ShowDialog(this);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0004C484 File Offset: 0x0004B484
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.AddNewClick != null)
				{
					this.AddNewClick = null;
				}
				if (this.components != null)
				{
					this.components.Dispose();
				}
				if (this.tag2 != null)
				{
					this.tag2 = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0004C4E8 File Offset: 0x0004B4E8
		private void InitializeComponent()
		{
			this.components = new Container();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.MENU_print = new ToolStripMenuItem();
			this.MENU_printAll = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.MENU_selectAll = new ToolStripMenuItem();
			this.mi_filesSeparator = new ToolStripSeparator();
			this.mi_emailAllFiles = new ToolStripMenuItem();
			this.mi_emailSelectedFiles = new ToolStripMenuItem();
			this.mi_openSelectedFile = new ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.mi_emailAllFiles,
				this.mi_emailSelectedFiles,
				this.mi_openSelectedFile,
				this.mi_filesSeparator,
				this.MENU_print,
				this.MENU_printAll,
				this.toolStripSeparator1,
				this.MENU_selectAll
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(174, 148);
			this.MENU_print.Name = "MENU_print";
			this.MENU_print.Size = new Size(173, 22);
			this.MENU_print.Text = "&Print selected";
			this.MENU_printAll.Name = "MENU_printAll";
			this.MENU_printAll.Size = new Size(173, 22);
			this.MENU_printAll.Text = "Print a&ll";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(170, 6);
			this.MENU_selectAll.Name = "MENU_selectAll";
			this.MENU_selectAll.Size = new Size(173, 22);
			this.MENU_selectAll.Text = "Select &all";
			this.mi_filesSeparator.Name = "mi_filesSeparator";
			this.mi_filesSeparator.Size = new Size(170, 6);
			this.mi_filesSeparator.Visible = false;
			this.mi_emailAllFiles.Name = "mi_emailAllFiles";
			this.mi_emailAllFiles.Size = new Size(173, 22);
			this.mi_emailAllFiles.Text = "Email all files";
			this.mi_emailAllFiles.Visible = false;
			this.mi_emailAllFiles.Click += this.mi_emailAllFiles_Click;
			this.mi_emailSelectedFiles.Name = "mi_emailSelectedFiles";
			this.mi_emailSelectedFiles.Size = new Size(173, 22);
			this.mi_emailSelectedFiles.Text = "Email selected files";
			this.mi_emailSelectedFiles.Visible = false;
			this.mi_emailSelectedFiles.Click += this.mi_emailSelectedFiles_Click;
			this.mi_openSelectedFile.Name = "mi_openSelectedFile";
			this.mi_openSelectedFile.Size = new Size(173, 22);
			this.mi_openSelectedFile.Text = "Open selected file";
			this.mi_openSelectedFile.Visible = false;
			this.mi_openSelectedFile.Click += this.mi_openSelectedFile_Click;
			this.ContextMenuStrip = this.contextMenuStrip1;
			base.KeyPress += this.ListViewEx_KeyPress;
			base.KeyDown += this.ListViewEx_KeyDown;
			base.BackColorChanged += this.ListViewEx_BackColorChanged;
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0004C890 File Offset: 0x0004B890
		protected override void OnNotifyMessage(Message m)
		{
			if (m.Msg == 78)
			{
				if (((ListViewEx.NMHDR)Marshal.PtrToStructure(m.LParam, typeof(ListViewEx.NMHDR))).code == -530)
				{
					this.NeedText();
				}
			}
			base.OnNotifyMessage(m);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0004C8F4 File Offset: 0x0004B8F4
		private void NeedText()
		{
			ListViewEx.ItemHoverEventArgs itemHoverEventArgs = new ListViewEx.ItemHoverEventArgs();
			ListViewEx.LVHITTESTINFO lvhi = default(ListViewEx.LVHITTESTINFO);
			lvhi.pt = base.PointToClient(Control.MousePosition);
			this.ListView_SubItemHitTest(ref lvhi);
			if (lvhi.iItem >= 0 && lvhi.iSubItem >= 0)
			{
				itemHoverEventArgs.Item = lvhi.iItem;
				itemHoverEventArgs.SubItem = lvhi.iSubItem;
				itemHoverEventArgs.ItemTextInVisible = this.IsItemTextHidden(lvhi);
				if (this.m_itemHover != null)
				{
					this.m_itemHover(this, itemHoverEventArgs);
				}
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0004C994 File Offset: 0x0004B994
		private bool IsItemTextHidden(ListViewEx.LVHITTESTINFO lvhi)
		{
			Rectangle rect = Rectangle.Empty;
			bool result;
			if (lvhi.iSubItem > 0 && lvhi.iItem >= 0)
			{
				int num = this.ListView_GetStringWidth(base.Items[lvhi.iItem].SubItems[lvhi.iSubItem].Text);
				int num2 = this.ListView_GetColumnWidth(lvhi.iSubItem);
				result = (num + 12 > num2);
			}
			else
			{
				int num = this.ListView_GetStringWidth(base.Items[lvhi.iItem].Text);
				int num2 = this.ListView_GetColumnWidth(0);
				this.ListView_GetItemRect(lvhi.iItem, 2, ref rect);
				rect = Rectangle.Inflate(rect, -2, -2);
				result = (rect.Left + num + 4 > num2);
			}
			return result;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0004CA64 File Offset: 0x0004BA64
		private void ListView_SubItemHitTest(ref ListViewEx.LVHITTESTINFO lvhi)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(lvhi));
			Marshal.StructureToPtr(lvhi, intPtr, true);
			ListViewEx.SendMessage(base.Handle, 4153, IntPtr.Zero, intPtr);
			lvhi = (ListViewEx.LVHITTESTINFO)Marshal.PtrToStructure(intPtr, typeof(ListViewEx.LVHITTESTINFO));
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0004CAD4 File Offset: 0x0004BAD4
		private int ListView_GetColumnWidth(int iCol)
		{
			return ListViewEx.SendMessage(base.Handle, 4125, iCol, IntPtr.Zero);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0004CAFC File Offset: 0x0004BAFC
		private int ListView_GetStringWidth(string psz)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAuto(psz);
			int result = ListViewEx.SendMessage(base.Handle, 4183, 0, intPtr);
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0004CB30 File Offset: 0x0004BB30
		private bool ListView_GetItemRect(int iItem, int code, ref Rectangle lpRect)
		{
			Rectangle rectangle = default(Rectangle);
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(rectangle));
			Marshal.StructureToPtr(rectangle, intPtr, true);
			ListViewEx.SendMessage(base.Handle, 4110, iItem, intPtr);
			lpRect = (Rectangle)Marshal.PtrToStructure(intPtr, typeof(Rectangle));
			Marshal.FreeHGlobal(intPtr);
			return true;
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060009EE RID: 2542 RVA: 0x0004CBA0 File Offset: 0x0004BBA0
		// (remove) Token: 0x060009EF RID: 2543 RVA: 0x0004CBDC File Offset: 0x0004BBDC
		protected event ListViewEx.ItemHoverEventHandler m_itemHover;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060009F0 RID: 2544 RVA: 0x0004CC18 File Offset: 0x0004BC18
		// (remove) Token: 0x060009F1 RID: 2545 RVA: 0x0004CC23 File Offset: 0x0004BC23
		public event ListViewEx.ItemHoverEventHandler ItemHover
		{
			add
			{
				this.m_itemHover += value;
			}
			remove
			{
				this.m_itemHover -= value;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0004CC30 File Offset: 0x0004BC30
		public static void SwapListViewItems(ref ListViewItem lvi1, ref ListViewItem lvi2)
		{
			for (int i = 0; i < lvi1.SubItems.Count; i++)
			{
				string text = lvi1.SubItems[i].Text;
				lvi1.SubItems[i].Text = lvi2.SubItems[i].Text;
				lvi2.SubItems[i].Text = text;
			}
			object tag = lvi1.Tag;
			lvi1.Tag = lvi2.Tag;
			lvi2.Tag = tag;
			int imageIndex = lvi1.ImageIndex;
			lvi1.ImageIndex = lvi2.ImageIndex;
			lvi2.ImageIndex = imageIndex;
			bool selected = lvi1.Selected;
			lvi1.Selected = lvi2.Selected;
			lvi2.Selected = selected;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0004CD0C File Offset: 0x0004BD0C
		public int[] GetColumnOrder()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * base.Columns.Count);
			int[] result;
			if (ListViewEx.SendMessage(base.Handle, 4155, new IntPtr(base.Columns.Count), intPtr).ToInt32() == 0)
			{
				Marshal.FreeHGlobal(intPtr);
				result = null;
			}
			else
			{
				int[] array = new int[base.Columns.Count];
				Marshal.Copy(intPtr, array, 0, base.Columns.Count);
				Marshal.FreeHGlobal(intPtr);
				result = array;
			}
			return result;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0004CDB0 File Offset: 0x0004BDB0
		public int GetSubItemAt(int x, int y, out ListViewItem item)
		{
			item = base.GetItemAt(x, y);
			if (item != null)
			{
				int[] columnOrder = this.GetColumnOrder();
				int num = item.GetBounds(ItemBoundsPortion.Entire).Left;
				for (int i = 0; i < columnOrder.Length; i++)
				{
					ColumnHeader columnHeader = base.Columns[columnOrder[i]];
					if (x < num + columnHeader.Width)
					{
						return columnHeader.Index;
					}
					num += columnHeader.Width;
				}
			}
			return -1;
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060009F5 RID: 2549 RVA: 0x0004CE48 File Offset: 0x0004BE48
		// (remove) Token: 0x060009F6 RID: 2550 RVA: 0x0004CE84 File Offset: 0x0004BE84
		public event ListViewEx.SelectedIndexChangingEventHandler SelectedIndexChanging;

		// Token: 0x060009F7 RID: 2551 RVA: 0x0004CEC0 File Offset: 0x0004BEC0
		protected virtual void OnSelectedIndexChanging(ItemChangingEventArgs e)
		{
			if (this.SelectedIndexChanging != null)
			{
				this.SelectedIndexChanging(this, e);
			}
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0004CEEC File Offset: 0x0004BEEC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.sel = new ArrayList();
			foreach (object obj in base.SelectedIndices)
			{
				int num = (int)obj;
				this.sel.Add(num);
			}
			base.OnMouseDown(e);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0004CF70 File Offset: 0x0004BF70
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			bool flag = true;
			if (this.sel != null)
			{
				foreach (object obj in base.SelectedIndices)
				{
					int num = (int)obj;
					if (this.sel.Contains(num))
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag && e.Button == MouseButtons.Left)
			{
				ListViewItem item;
				int subItemAt = this.GetSubItemAt(e.X, e.Y, out item);
				if (subItemAt >= 0)
				{
					if (this.SubItemClicked != null)
					{
						this.SubItemClicked(this, new SubItemClickEventArgs(item, subItemAt));
					}
				}
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0004D078 File Offset: 0x0004C078
		public Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
		{
			int[] columnOrder = this.GetColumnOrder();
			Rectangle empty = Rectangle.Empty;
			if (SubItem >= columnOrder.Length)
			{
				throw new IndexOutOfRangeException("SubItem " + SubItem + " out of range");
			}
			if (Item == null)
			{
				throw new ArgumentNullException("Item");
			}
			Rectangle bounds = Item.GetBounds(ItemBoundsPortion.Entire);
			int num = bounds.Left;
			int i;
			for (i = 0; i < columnOrder.Length; i++)
			{
				ColumnHeader columnHeader = base.Columns[columnOrder[i]];
				if (columnHeader.Index == SubItem)
				{
					break;
				}
				num += columnHeader.Width;
			}
			empty = new Rectangle(num, bounds.Top, base.Columns[columnOrder[i]].Width, bounds.Height);
			return empty;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0004D160 File Offset: 0x0004C160
		public void DrawCheckbox(Graphics g, Rectangle r, bool _checked)
		{
			Size size = new Size(16, 16);
			int num = Convert.ToInt32((r.Height - size.Height) / 2);
			int num2 = 4;
			Pen pen = new Pen(Color.DarkGray);
			g.DrawRectangle(pen, num2, num, size.Width, size.Height);
			if (_checked)
			{
				g.DrawLine(pen, num2, num, num2 + size.Width, num + size.Height);
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0004D1DE File Offset: 0x0004C1DE
		public void StartEditing(Control c, ListViewItem Item, int SubItem)
		{
			this.StartEditing(c, Item, SubItem, true);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0004D1EC File Offset: 0x0004C1EC
		public void StartEditing(Control c, ListViewItem Item, int SubItem, bool resizeControlToMatchColumn)
		{
			Rectangle bounds;
			if (resizeControlToMatchColumn)
			{
				bounds = this.GetSubItemBounds(Item, SubItem);
				if (bounds.X < 0)
				{
					bounds.Width += bounds.X;
					bounds.X = 0;
				}
				if (bounds.X + bounds.Width > base.Width)
				{
					bounds.Width = base.Width - bounds.Left;
				}
			}
			else
			{
				bounds = c.Bounds;
			}
			bounds.Offset(base.Left, base.Top);
			Point p = new Point(0, 0);
			Point point = base.Parent.PointToScreen(p);
			Point point2 = c.Parent.PointToScreen(p);
			bounds.Offset(point.X - point2.X, point.Y - point2.Y);
			if (c is MyDateTimePicker)
			{
				bounds.Width = c.Width;
				c.Bounds = bounds;
				MyDateTimePicker myDateTimePicker = (MyDateTimePicker)c;
				string text = Item.SubItems[SubItem].Text.Trim();
				if (text.Length < 1)
				{
					myDateTimePicker.Value = DateTime.MinValue;
				}
				else
				{
					try
					{
						myDateTimePicker.Value = Convert.ToDateTime(text);
					}
					catch
					{
						myDateTimePicker.Value = DateTime.MinValue;
					}
				}
			}
			else if (c is AutoComboBox)
			{
				bounds.Width = c.Width;
				c.Bounds = bounds;
				AutoComboBox autoComboBox = (AutoComboBox)c;
				if (autoComboBox.DataSource is DataTable && autoComboBox.ValueMember.Length > 0)
				{
					DataTable dataTable = (DataTable)autoComboBox.DataSource;
					int num = dataTable.Columns.IndexOf(autoComboBox.DisplayMember);
					int num2 = dataTable.Columns.IndexOf(autoComboBox.ValueMember);
					if (num < 0 || num2 < 0)
					{
						autoComboBox.SelectedText = Item.SubItems[SubItem].Text;
					}
					else
					{
						string strB = Item.SubItems[SubItem].Text.Trim().ToLower();
						int num3 = 0;
						bool flag = false;
						foreach (object obj in dataTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							string text = dataRow[num].ToString().Trim().ToLower();
							if (text.CompareTo(strB) == 0)
							{
								autoComboBox.SelectedIndex = num3;
								flag = true;
								break;
							}
							num3++;
						}
						if (!flag)
						{
							autoComboBox.SelectedText = Item.SubItems[SubItem].Text;
						}
					}
				}
				else
				{
					autoComboBox.SelectedText = Item.SubItems[SubItem].Text;
				}
			}
			else if (c is TextBox && ((TextBox)c).Multiline)
			{
				int width;
				if (bounds.Left + c.Width > Item.ListView.Width)
				{
					width = Item.ListView.Width - bounds.Left;
				}
				else
				{
					width = c.Width;
				}
				bounds.Height = c.Height;
				bounds.Width = width;
				c.Bounds = bounds;
				c.Text = Item.SubItems[SubItem].Text;
			}
			else if (c is TextBox || c is MyTextBox)
			{
				c.Bounds = bounds;
				c.Text = Item.SubItems[SubItem].Text;
			}
			else if (c is CheckBox || c is MyCheckBox)
			{
				c.Bounds = bounds;
				CheckBox checkBox = (CheckBox)c;
				string text2 = Item.SubItems[SubItem].Text.ToLower();
				checkBox.Checked = (text2.CompareTo("true") == 0 || text2.CompareTo("t") == 0 || text2.CompareTo("yes") == 0 || text2.CompareTo("x") == 0);
			}
			else
			{
				c.Bounds = bounds;
				c.Text = Item.SubItems[SubItem].Text;
			}
			c.Visible = true;
			c.BringToFront();
			c.Focus();
			this._editingControl = c;
			this._editingControl.Leave += this._editControl_Leave;
			this._editingControl.KeyPress += this._editControl_KeyPress;
			this._editItem = Item;
			this._editSubItem = SubItem;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0004D75C File Offset: 0x0004C75C
		private void _editControl_Leave(object sender, EventArgs e)
		{
			this.EndEditing(true);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0004D768 File Offset: 0x0004C768
		private void _editControl_KeyPress(object sender, KeyPressEventArgs e)
		{
			char keyChar = e.KeyChar;
			if (keyChar != '\r')
			{
				if (keyChar == '\u001b')
				{
					this.EndEditing(false);
				}
			}
			else if (sender == null || !(sender is TextBox) || !((TextBox)sender).Multiline)
			{
				this.EndEditing(true);
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0004D7C4 File Offset: 0x0004C7C4
		public void EndEditing(bool AcceptChanges)
		{
			if (this._editingControl != null)
			{
				if (AcceptChanges)
				{
					if (this._editingControl is MyDateTimePicker)
					{
						MyDateTimePicker myDateTimePicker = (MyDateTimePicker)this._editingControl;
						DateTime value = myDateTimePicker.Value;
						if (value == DateTime.MinValue)
						{
							this._editItem.SubItems[this._editSubItem].Text = "";
						}
						else
						{
							this._editItem.SubItems[this._editSubItem].Text = this._editingControl.Text;
						}
					}
					else if (this._editingControl is CheckBox || this._editingControl is MyCheckBox)
					{
						CheckBox checkBox = (CheckBox)this._editingControl;
						if (checkBox.Checked)
						{
							this._editItem.SubItems[this._editSubItem].Text = "Yes";
						}
						else
						{
							this._editItem.SubItems[this._editSubItem].Text = "No";
						}
					}
					else
					{
						this._editItem.SubItems[this._editSubItem].Text = this._editingControl.Text;
					}
				}
				else
				{
					this._editingControl.Text = this._editItem.SubItems[this._editSubItem].Text;
				}
				this._editingControl.Leave -= this._editControl_Leave;
				this._editingControl.KeyPress -= this._editControl_KeyPress;
				if (this.SubItemEndEditing != null)
				{
					this.SubItemEndEditing(this, new SubItemClickEventArgs(this._editItem, this._editSubItem));
				}
				this._editingControl.Visible = false;
				this._editingControl = null;
				this._editItem = null;
				this._editSubItem = -1;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0004D9D4 File Offset: 0x0004C9D4
		protected override void WndProc(ref Message msg)
		{
			bool flag = false;
			int msg2 = msg.Msg;
			if (msg2 <= 78)
			{
				if (msg2 != 5)
				{
					if (msg2 != 78)
					{
						goto IL_3FB;
					}
					ListViewEx.NMHDR nmhdr = (ListViewEx.NMHDR)Marshal.PtrToStructure(msg.LParam, typeof(ListViewEx.NMHDR));
					if (nmhdr.code == -310 || nmhdr.code == -300 || nmhdr.code == -320)
					{
						base.Focus();
					}
					goto IL_3FB;
				}
			}
			else
			{
				switch (msg2)
				{
				case 276:
				case 277:
					break;
				default:
					switch (msg2)
					{
					case 8235:
						if (this.drawMode == DrawMode.OwnerDrawFixed || this.drawMode == DrawMode.OwnerDrawVariable)
						{
							flag = true;
							base.WndProc(ref msg);
							ListViewEx.DrawItemStruct drawItemStruct = (ListViewEx.DrawItemStruct)msg.GetLParam(typeof(ListViewEx.DrawItemStruct));
							if (drawItemStruct.ctlType == 100)
							{
								Control topLevelControl = base.TopLevelControl;
								topLevelControl.Text += "*";
							}
							else
							{
								Graphics graphics = Graphics.FromHdc(drawItemStruct.hDC);
								Rectangle rect = new Rectangle(drawItemStruct.rcItem.left, drawItemStruct.rcItem.top, drawItemStruct.rcItem.right - drawItemStruct.rcItem.left, drawItemStruct.rcItem.bottom - drawItemStruct.rcItem.top);
								int itemID = drawItemStruct.itemID;
								DrawItemState state = DrawItemState.None;
								DrawItemEventArgs e = new DrawItemEventArgs(graphics, this.Font, rect, itemID, state, this.ForeColor, this.BackColor);
								if (this.DrawItem != null)
								{
									this.DrawItem(this, e);
								}
								this.OnDrawItem(e);
								graphics.Dispose();
							}
						}
						goto IL_3FB;
					case 8236:
						if (this.drawMode == DrawMode.OwnerDrawFixed || this.drawMode == DrawMode.OwnerDrawVariable)
						{
							this.WmReflectMeasureItem(ref msg);
						}
						goto IL_3FB;
					default:
						if (msg2 != 8270)
						{
							goto IL_3FB;
						}
						if (msg.LParam != IntPtr.Zero)
						{
							ListViewEx.NMHDR nmhdr = (ListViewEx.NMHDR)Marshal.PtrToStructure(msg.LParam, typeof(ListViewEx.NMHDR));
							if (((ListViewEx.NMHDR)msg.GetLParam(typeof(ListViewEx.NMHDR))).code == -100)
							{
								ListViewEx.NMLISTVIEW nmlistview = (ListViewEx.NMLISTVIEW)msg.GetLParam(typeof(ListViewEx.NMLISTVIEW));
								if ((nmlistview.uChanged & 8) == 8)
								{
									if ((nmlistview.uOldState & 2) != 2 && (nmlistview.uNewState & 2) == 2)
									{
										ItemChangingEventArgs itemChangingEventArgs = new ItemChangingEventArgs(nmlistview.iItem);
										this.OnSelectedIndexChanging(itemChangingEventArgs);
										if (itemChangingEventArgs.Cancel)
										{
											msg.Result = new IntPtr(1);
											return;
										}
									}
									else if ((nmlistview.uOldState & 2) == 2 && (nmlistview.uNewState & 2) != 2)
									{
										ItemChangingEventArgs itemChangingEventArgs = new ItemChangingEventArgs(nmlistview.iItem);
										this.OnSelectedIndexChanging(itemChangingEventArgs);
										if (itemChangingEventArgs.Cancel)
										{
											msg.Result = new IntPtr(1);
											return;
										}
									}
								}
							}
						}
						goto IL_3FB;
					}
					break;
				}
			}
			if (msg.WParam == this.SB_PAGEUP || msg.WParam == this.SB_PAGEDOWN || msg.WParam == this.SB_LINEDOWN || msg.WParam == this.SB_LINEUP)
			{
				base.SuspendLayout();
				base.WndProc(ref msg);
				flag = true;
				base.ResumeLayout();
				base.Invalidate();
			}
			IL_3FB:
			if (!flag)
			{
				try
				{
					base.WndProc(ref msg);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0004DE08 File Offset: 0x0004CE08
		private void WmReflectMeasureItem(ref Message m)
		{
			ListViewEx.MEASUREITEMSTRUCT measureitemstruct = (ListViewEx.MEASUREITEMSTRUCT)m.GetLParam(typeof(ListViewEx.MEASUREITEMSTRUCT));
			if (this.drawMode == DrawMode.OwnerDrawVariable && measureitemstruct.itemID >= 0)
			{
				Graphics graphics = Graphics.FromHwnd(base.Handle);
				MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, measureitemstruct.itemID, 20);
				try
				{
					if (this.MeasureItem != null)
					{
						this.MeasureItem(this, measureItemEventArgs);
					}
					this.OnMeasureItem(measureItemEventArgs);
					measureitemstruct.itemHeight = measureItemEventArgs.ItemHeight;
				}
				finally
				{
					graphics.Dispose();
				}
			}
			measureitemstruct.itemHeight = this.ItemHeight;
			Marshal.StructureToPtr(measureitemstruct, m.LParam, false);
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0004DEE4 File Offset: 0x0004CEE4
		private void ListViewEx_BackColorChanged(object sender, EventArgs e)
		{
			this.backColourBrush = new SolidBrush(this.BackColor);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0004DEF8 File Offset: 0x0004CEF8
		private void ListViewEx_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.OnDoubleClick(new EventArgs());
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0004DF28 File Offset: 0x0004CF28
		private void ListViewEx_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.C)
			{
				this.CopyListViewToClipboard(this);
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0004DF5C File Offset: 0x0004CF5C
		public void CopyListViewToClipboard(ListView lv)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < lv.Columns.Count; i++)
			{
				stringBuilder.Append(lv.Columns[i].Text);
				stringBuilder.Append("\t");
			}
			stringBuilder.Append("\n");
			for (int i = 0; i < lv.Items.Count; i++)
			{
				for (int j = 0; j < lv.Columns.Count; j++)
				{
					string value = (j < lv.Items[i].SubItems.Count) ? lv.Items[i].SubItems[j].Text : "";
					stringBuilder.Append(value);
					stringBuilder.Append("\t");
				}
				stringBuilder.Append("\n");
			}
			Clipboard.SetText(stringBuilder.ToString());
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0004E06C File Offset: 0x0004D06C
		private void mi_emailAllFiles_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			foreach (object obj in base.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				string text = listViewItem.SubItems[listViewItem.SubItems.Count - 1].Text;
				list.Add(text);
				MessageBox.Show(text);
			}
			this.EmailFiles(list);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0004E110 File Offset: 0x0004D110
		private void EmailFiles(List<string> filesInfo)
		{
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0004E114 File Offset: 0x0004D114
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0004E12B File Offset: 0x0004D12B
		public int EmailTemplateId { get; set; }

		// Token: 0x06000A0B RID: 2571 RVA: 0x0004E134 File Offset: 0x0004D134
		private void mi_emailSelectedFiles_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			foreach (object obj in base.SelectedItems)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				string text = listViewItem.SubItems[listViewItem.SubItems.Count - 1].Text;
				list.Add(text);
				MessageBox.Show(text);
			}
			this.EmailFiles(list);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0004E1D8 File Offset: 0x0004D1D8
		private void mi_openSelectedFile_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x040006E9 RID: 1769
		public const uint LVM_GETHEADER = 4127U;

		// Token: 0x040006EA RID: 1770
		public const uint HDM_SETIMAGELIST = 4616U;

		// Token: 0x040006EB RID: 1771
		public const uint LVM_SETCOLUMN = 4122U;

		// Token: 0x040006EC RID: 1772
		public const uint LVCF_FMT = 1U;

		// Token: 0x040006ED RID: 1773
		public const uint LVCF_IMAGE = 16U;

		// Token: 0x040006EE RID: 1774
		public const int LVCFMT_IMAGE = 2048;

		// Token: 0x040006EF RID: 1775
		public const int LVS_OWNERDRAWFIXED = 1024;

		// Token: 0x040006F0 RID: 1776
		private const int LVM_FIRST = 4096;

		// Token: 0x040006F1 RID: 1777
		private const int LVM_GETCOLUMNORDERARRAY = 4155;

		// Token: 0x040006F2 RID: 1778
		private const int LVM_GETITEMRECT = 4110;

		// Token: 0x040006F3 RID: 1779
		private const int LVM_GETCOLUMNWIDTH = 4125;

		// Token: 0x040006F4 RID: 1780
		private const int LVM_SUBITEMHITTEST = 4153;

		// Token: 0x040006F5 RID: 1781
		private const int LVM_GETSTRINGWIDTHW = 4183;

		// Token: 0x040006F6 RID: 1782
		private const int LVIR_LABEL = 2;

		// Token: 0x040006F7 RID: 1783
		private const int WM_NOTIFY = 78;

		// Token: 0x040006F8 RID: 1784
		private const int TTN_FIRST = -520;

		// Token: 0x040006F9 RID: 1785
		private const int TTN_NEEDTEXT = -530;

		// Token: 0x040006FA RID: 1786
		private const int WM_HSCROLL = 276;

		// Token: 0x040006FB RID: 1787
		private const int WM_VSCROLL = 277;

		// Token: 0x040006FC RID: 1788
		private const int WM_SIZE = 5;

		// Token: 0x040006FD RID: 1789
		private const int HDN_FIRST = -300;

		// Token: 0x040006FE RID: 1790
		private const int HDN_BEGINDRAG = -310;

		// Token: 0x040006FF RID: 1791
		private const int HDN_ITEMCHANGINGA = -300;

		// Token: 0x04000700 RID: 1792
		private const int HDN_ITEMCHANGINGW = -320;

		// Token: 0x04000701 RID: 1793
		private const int WM_USER = 1024;

		// Token: 0x04000702 RID: 1794
		private const int OCM_BASE = 8192;

		// Token: 0x04000703 RID: 1795
		private const int OCM_NOTIFY = 8270;

		// Token: 0x04000704 RID: 1796
		private const int LVN_FIRST = -100;

		// Token: 0x04000705 RID: 1797
		private const int LVN_ITEMCHANGING = -100;

		// Token: 0x04000706 RID: 1798
		private const int LVN_ITEMCHANGED = -101;

		// Token: 0x04000707 RID: 1799
		private const int LVIF_STATE = 8;

		// Token: 0x04000708 RID: 1800
		private const int LVIS_SELECTED = 2;

		// Token: 0x04000709 RID: 1801
		private ContextMenuStrip contextMenuStrip1;

		// Token: 0x0400070A RID: 1802
		private ToolStripMenuItem MENU_print;

		// Token: 0x0400070B RID: 1803
		private ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400070C RID: 1804
		private ToolStripMenuItem MENU_selectAll;

		// Token: 0x0400070D RID: 1805
		private ToolStripMenuItem MENU_printAll;

		// Token: 0x0400070E RID: 1806
		private ToolStripMenuItem mi_emailAllFiles;

		// Token: 0x0400070F RID: 1807
		private ToolStripSeparator mi_filesSeparator;

		// Token: 0x04000710 RID: 1808
		private ToolStripMenuItem mi_emailSelectedFiles;

		// Token: 0x04000711 RID: 1809
		private ToolStripMenuItem mi_openSelectedFile;

		// Token: 0x04000714 RID: 1812
		private IntPtr SB_PAGEUP = new IntPtr(2);

		// Token: 0x04000715 RID: 1813
		private IntPtr SB_PAGEDOWN = new IntPtr(3);

		// Token: 0x04000716 RID: 1814
		private IntPtr SB_ENDSCROLL = new IntPtr(8);

		// Token: 0x04000717 RID: 1815
		private IntPtr SB_LINEUP = new IntPtr(0);

		// Token: 0x04000718 RID: 1816
		private IntPtr SB_LINEDOWN = new IntPtr(1);

		// Token: 0x04000719 RID: 1817
		private DrawMode drawMode;

		// Token: 0x0400071A RID: 1818
		private Color backColourSelected = Color.LightBlue;

		// Token: 0x0400071B RID: 1819
		private SolidBrush backColourSelectedBrush = null;

		// Token: 0x0400071C RID: 1820
		private SolidBrush backColourBrush = null;

		// Token: 0x0400071D RID: 1821
		private int itemHeight = 16;

		// Token: 0x0400071E RID: 1822
		private bool isFileList = false;

		// Token: 0x0400071F RID: 1823
		private object tag2 = null;

		// Token: 0x04000720 RID: 1824
		private ImageList headerImageList = new ImageList();

		// Token: 0x04000721 RID: 1825
		private bool autoSortingEnabled = false;

		// Token: 0x04000722 RID: 1826
		private bool noEditing = false;

		// Token: 0x04000723 RID: 1827
		private bool noDeleting = false;

		// Token: 0x04000724 RID: 1828
		private int defaultSortByColInd = -1;

		// Token: 0x04000725 RID: 1829
		private bool defaultSortByAsc = true;

		// Token: 0x04000726 RID: 1830
		private bool allowFileDropping = false;

		// Token: 0x04000727 RID: 1831
		private int calcButtonCid = 0;

		// Token: 0x04000728 RID: 1832
		private EventHandler AddNewClick = null;

		// Token: 0x04000729 RID: 1833
		public string[] droppedFilenames = null;

		// Token: 0x0400072A RID: 1834
		private bool[] listViewColumnSortings = null;

		// Token: 0x0400072B RID: 1835
		private int lastTaskColumnSorted = -1;

		// Token: 0x0400072C RID: 1836
		private bool enterTriggersDoubleClickEvent = false;

		// Token: 0x0400072D RID: 1837
		private IContainer components;

		// Token: 0x04000732 RID: 1842
		private ArrayList sel = new ArrayList();

		// Token: 0x04000733 RID: 1843
		private Control _editingControl;

		// Token: 0x04000734 RID: 1844
		private ListViewItem _editItem;

		// Token: 0x04000735 RID: 1845
		private int _editSubItem;

		// Token: 0x020000F4 RID: 244
		public struct NMHDR
		{
			// Token: 0x04000737 RID: 1847
			public IntPtr hwndFrom;

			// Token: 0x04000738 RID: 1848
			public int idFrom;

			// Token: 0x04000739 RID: 1849
			public int code;
		}

		// Token: 0x020000F5 RID: 245
		private struct MEASUREITEMSTRUCT
		{
			// Token: 0x0400073A RID: 1850
			public int CtlType;

			// Token: 0x0400073B RID: 1851
			public int CtlID;

			// Token: 0x0400073C RID: 1852
			public int itemID;

			// Token: 0x0400073D RID: 1853
			public int itemWidth;

			// Token: 0x0400073E RID: 1854
			public int itemHeight;

			// Token: 0x0400073F RID: 1855
			public IntPtr itemData;
		}

		// Token: 0x020000F6 RID: 246
		private struct RECT
		{
			// Token: 0x04000740 RID: 1856
			public int left;

			// Token: 0x04000741 RID: 1857
			public int top;

			// Token: 0x04000742 RID: 1858
			public int right;

			// Token: 0x04000743 RID: 1859
			public int bottom;
		}

		// Token: 0x020000F7 RID: 247
		private struct DrawItemStruct
		{
			// Token: 0x04000744 RID: 1860
			public int ctlType;

			// Token: 0x04000745 RID: 1861
			public int ctlID;

			// Token: 0x04000746 RID: 1862
			public int itemID;

			// Token: 0x04000747 RID: 1863
			public int itemAction;

			// Token: 0x04000748 RID: 1864
			public int itemState;

			// Token: 0x04000749 RID: 1865
			public IntPtr hWndItem;

			// Token: 0x0400074A RID: 1866
			public IntPtr hDC;

			// Token: 0x0400074B RID: 1867
			public ListViewEx.RECT rcItem;

			// Token: 0x0400074C RID: 1868
			public IntPtr itemData;
		}

		// Token: 0x020000F8 RID: 248
		private enum ReflectedMessages
		{
			// Token: 0x0400074E RID: 1870
			OCM__BASE = 8192,
			// Token: 0x0400074F RID: 1871
			OCM_DRAWITEM = 8235
		}

		// Token: 0x020000F9 RID: 249
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 8)]
		public struct LVCOLUMN
		{
			// Token: 0x04000750 RID: 1872
			public uint mask;

			// Token: 0x04000751 RID: 1873
			public int fmt;

			// Token: 0x04000752 RID: 1874
			public int cx;

			// Token: 0x04000753 RID: 1875
			public IntPtr pszText;

			// Token: 0x04000754 RID: 1876
			public int cchTextMax;

			// Token: 0x04000755 RID: 1877
			public int iSubItem;

			// Token: 0x04000756 RID: 1878
			public int iImage;

			// Token: 0x04000757 RID: 1879
			public int iOrder;
		}

		// Token: 0x020000FA RID: 250
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct LVHITTESTINFO
		{
			// Token: 0x04000758 RID: 1880
			public Point pt;

			// Token: 0x04000759 RID: 1881
			public int flags;

			// Token: 0x0400075A RID: 1882
			public int iItem;

			// Token: 0x0400075B RID: 1883
			public int iSubItem;
		}

		// Token: 0x020000FB RID: 251
		public class ItemHoverEventArgs : EventArgs
		{
			// Token: 0x17000202 RID: 514
			// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0004E1DC File Offset: 0x0004D1DC
			// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0004E1F4 File Offset: 0x0004D1F4
			public int Item
			{
				get
				{
					return this.m_item;
				}
				set
				{
					this.m_item = value;
				}
			}

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0004E200 File Offset: 0x0004D200
			// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0004E218 File Offset: 0x0004D218
			public int SubItem
			{
				get
				{
					return this.m_subitem;
				}
				set
				{
					this.m_subitem = value;
				}
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0004E224 File Offset: 0x0004D224
			// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0004E23C File Offset: 0x0004D23C
			public bool ItemTextInVisible
			{
				get
				{
					return this.m_itemTextVisible;
				}
				set
				{
					this.m_itemTextVisible = value;
				}
			}

			// Token: 0x0400075C RID: 1884
			protected int m_item;

			// Token: 0x0400075D RID: 1885
			protected int m_subitem;

			// Token: 0x0400075E RID: 1886
			protected bool m_itemTextVisible;
		}

		// Token: 0x020000FC RID: 252
		// (Invoke) Token: 0x06000A15 RID: 2581
		public delegate void ItemHoverEventHandler(object sender, ListViewEx.ItemHoverEventArgs e);

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x06000A19 RID: 2585
		public delegate void SelectedIndexChangingEventHandler(object sender, ItemChangingEventArgs e);

		// Token: 0x020000FE RID: 254
		public struct NMLISTVIEW
		{
			// Token: 0x0400075F RID: 1887
			public ListViewEx.NMHDR hdr;

			// Token: 0x04000760 RID: 1888
			public int iItem;

			// Token: 0x04000761 RID: 1889
			public int iSubItem;

			// Token: 0x04000762 RID: 1890
			public int uNewState;

			// Token: 0x04000763 RID: 1891
			public int uOldState;

			// Token: 0x04000764 RID: 1892
			public int uChanged;

			// Token: 0x04000765 RID: 1893
			public ListViewEx.POINT ptAction;

			// Token: 0x04000766 RID: 1894
			public int lParam;
		}

		// Token: 0x020000FF RID: 255
		public struct POINT
		{
			// Token: 0x04000767 RID: 1895
			public int x;

			// Token: 0x04000768 RID: 1896
			public int y;
		}
	}
}
