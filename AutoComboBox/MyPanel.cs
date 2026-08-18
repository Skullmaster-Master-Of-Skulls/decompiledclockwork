using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AutoComboBox.MyControls;
using DevComponents.DotNetBar;

namespace AutoComboBox
{
	// Token: 0x020000C4 RID: 196
	public class MyPanel : Panel
	{
		// Token: 0x06000747 RID: 1863
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000748 RID: 1864 RVA: 0x0003B0B4 File Offset: 0x0003A0B4
		// (remove) Token: 0x06000749 RID: 1865 RVA: 0x0003B0F0 File Offset: 0x0003A0F0
		public event DataChangedEventHandler OnDataRenderCompleted;

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0003B12C File Offset: 0x0003A12C
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0003B144 File Offset: 0x0003A144
		public string Student_no
		{
			get
			{
				return this.student_no;
			}
			set
			{
				this.student_no = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0003B150 File Offset: 0x0003A150
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0003B168 File Offset: 0x0003A168
		public int Pid
		{
			get
			{
				return this.pid;
			}
			set
			{
				this.pid = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0003B174 File Offset: 0x0003A174
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x0003B18C File Offset: 0x0003A18C
		public string FirstName
		{
			get
			{
				return this.firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0003B198 File Offset: 0x0003A198
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0003B1B0 File Offset: 0x0003A1B0
		public string LastName
		{
			get
			{
				return this.lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0003B1BC File Offset: 0x0003A1BC
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0003B1D4 File Offset: 0x0003A1D4
		public int DefaultActiveControl
		{
			get
			{
				return this.defaultActiveControl;
			}
			set
			{
				this.defaultActiveControl = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0003B1E0 File Offset: 0x0003A1E0
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0003B1F8 File Offset: 0x0003A1F8
		public BalloonTip BalloonTip
		{
			get
			{
				return this.balloonTip;
			}
			set
			{
				this.balloonTip = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0003B204 File Offset: 0x0003A204
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0003B21C File Offset: 0x0003A21C
		public ToolTip Tooltip
		{
			get
			{
				return this.tooltip;
			}
			set
			{
				this.tooltip = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0003B228 File Offset: 0x0003A228
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0003B240 File Offset: 0x0003A240
		public bool IsDynamicScreenContainer
		{
			get
			{
				return this.isDynamicScreenContainer;
			}
			set
			{
				this.isDynamicScreenContainer = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0003B24C File Offset: 0x0003A24C
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0003B264 File Offset: 0x0003A264
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0003B270 File Offset: 0x0003A270
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x0003B288 File Offset: 0x0003A288
		public object Tag3
		{
			get
			{
				return this.tag3;
			}
			set
			{
				this.tag3 = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0003B294 File Offset: 0x0003A294
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0003B2AC File Offset: 0x0003A2AC
		public int TagInt
		{
			get
			{
				return this.tagInt;
			}
			set
			{
				this.tagInt = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0003B2B8 File Offset: 0x0003A2B8
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0003B2D0 File Offset: 0x0003A2D0
		public object Screen
		{
			get
			{
				return this.screen;
			}
			set
			{
				this.screen = value;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0003B2DC File Offset: 0x0003A2DC
		public void FireDataRenderCompleted(int personId)
		{
			if (this.OnDataRenderCompleted != null)
			{
				this.OnDataRenderCompleted(this, new EventArgs(), personId);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0003B30C File Offset: 0x0003A30C
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0003B324 File Offset: 0x0003A324
		public string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0003B330 File Offset: 0x0003A330
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x0003B348 File Offset: 0x0003A348
		public string PrimaryClientDescription
		{
			get
			{
				return this.primaryClientDescription;
			}
			set
			{
				this.primaryClientDescription = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0003B354 File Offset: 0x0003A354
		// (set) Token: 0x06000768 RID: 1896 RVA: 0x0003B36C File Offset: 0x0003A36C
		public int PrimaryClientPid
		{
			get
			{
				return this.primaryClientPid;
			}
			set
			{
				this.primaryClientPid = value;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0003B376 File Offset: 0x0003A376
		protected override void Dispose(bool disposing)
		{
			this.alreadyDisabledControls.Clear();
			this.RemoveEnabledChangedHandlers(this);
			base.Dispose(disposing);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0003B398 File Offset: 0x0003A398
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0003B3B0 File Offset: 0x0003A3B0
		public new bool Enabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
				this.SetControlsEnabledDisabled(this);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0003B3C4 File Offset: 0x0003A3C4
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0003B3DC File Offset: 0x0003A3DC
		protected override void OnControlAdded(ControlEventArgs e)
		{
			if (e.Control is MyTextBox)
			{
				MyTextBox myTextBox = (MyTextBox)e.Control;
				if (myTextBox.ReadOnly)
				{
					this.alreadyDisabledControls.Add(myTextBox);
				}
				myTextBox.ReadOnlyChanged += this.mtb_ReadOnlyChanged;
			}
			else
			{
				if (!e.Control.Enabled)
				{
					this.alreadyDisabledControls.Add(e.Control);
				}
				e.Control.EnabledChanged += this.Control_EnabledChanged;
			}
			this.SetControlEnabledDisabled(e.Control);
			base.OnControlAdded(e);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0003B48C File Offset: 0x0003A48C
		private void mtb_ReadOnlyChanged(object sender, EventArgs e)
		{
			if (!this.ignoreControlEnabledUpdate)
			{
				MyTextBox myTextBox = (MyTextBox)sender;
				if (!myTextBox.ReadOnly)
				{
					if (this.alreadyDisabledControls.Contains(myTextBox))
					{
						this.alreadyDisabledControls.Remove(myTextBox);
					}
				}
				else
				{
					this.alreadyDisabledControls.Add(myTextBox);
				}
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0003B4F0 File Offset: 0x0003A4F0
		private void SetControlEnabledDisabled(Control c)
		{
			if (this.isEnabled)
			{
				if (!this.alreadyDisabledControls.Contains(c))
				{
					this.SetControlEnabledDisabled(c, true);
				}
			}
			else
			{
				this.SetControlEnabledDisabled(c, false);
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0003B534 File Offset: 0x0003A534
		private void SetControlEnabledDisabled(Control c, bool enabledVal)
		{
			this.ignoreControlEnabledUpdate = true;
			if (c is MyTextBox)
			{
				MyTextBox myTextBox = (MyTextBox)c;
				myTextBox.ReadOnly = !enabledVal;
			}
			else if (c is MyRichText)
			{
				MyRichText myRichText = (MyRichText)c;
				myRichText.ReadOnly = !enabledVal;
				myRichText.BaseReadOnly = !enabledVal;
			}
			else if (c is MyLayoutPanel)
			{
				MyLayoutPanel myLayoutPanel = (MyLayoutPanel)c;
				myLayoutPanel.Enabled2 = enabledVal;
			}
			else if (c is MyTabControl)
			{
				MyTabControl myTabControl = (MyTabControl)c;
				Panel tabButtonsPanel = myTabControl.TabButtonsPanel;
				foreach (object obj in myTabControl.Controls)
				{
					Control control = (Control)obj;
					if (control != tabButtonsPanel)
					{
						this.SetControlEnabledDisabled(control);
					}
				}
			}
			else if (c is MyTabPage)
			{
				MyTabPage myTabPage = (MyTabPage)c;
				foreach (object obj2 in myTabPage.Controls)
				{
					Control controlEnabledDisabled = (Control)obj2;
					this.SetControlEnabledDisabled(controlEnabledDisabled);
				}
			}
			else
			{
				c.Enabled = enabledVal;
			}
			this.ignoreControlEnabledUpdate = false;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0003B6FC File Offset: 0x0003A6FC
		public MyTabControl ClearTabDisplayIfFieldsAreFilledIn()
		{
			return this.ClearTabDisplayIfFieldsAreFilledIn(this);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0003B718 File Offset: 0x0003A718
		private MyTabControl ClearTabDisplayIfFieldsAreFilledIn(Control parentControl)
		{
			foreach (object obj in parentControl.Controls)
			{
				Control control = (Control)obj;
				if (control is MyTabControl)
				{
					MyTabControl myTabControl = (MyTabControl)control;
					myTabControl.ClearDisplayIfFieldsAreFilledIn();
					return myTabControl;
				}
				if (control.Controls.Count > 0)
				{
					MyTabControl myTabControl2 = this.ClearTabDisplayIfFieldsAreFilledIn(control);
					if (myTabControl2 != null)
					{
						return myTabControl2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0003B7DC File Offset: 0x0003A7DC
		private void SetControlsEnabledDisabled(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control controlEnabledDisabled = (Control)obj;
				this.SetControlEnabledDisabled(controlEnabledDisabled);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0003B844 File Offset: 0x0003A844
		private void RemoveEnabledChangedHandlers(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is MyTextBox)
				{
					MyTextBox myTextBox = (MyTextBox)control;
					myTextBox.ReadOnlyChanged -= this.mtb_ReadOnlyChanged;
				}
				else
				{
					control.EnabledChanged -= this.Control_EnabledChanged;
				}
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0003B8EC File Offset: 0x0003A8EC
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			if (this.alreadyDisabledControls.Contains(e.Control))
			{
				this.alreadyDisabledControls.Remove(e.Control);
			}
			if (e.Control is MyTextBox)
			{
				MyTextBox myTextBox = (MyTextBox)e.Control;
				myTextBox.ReadOnlyChanged -= this.mtb_ReadOnlyChanged;
			}
			else
			{
				e.Control.EnabledChanged -= this.Control_EnabledChanged;
			}
			base.OnControlRemoved(e);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0003B97C File Offset: 0x0003A97C
		private void Control_EnabledChanged(object sender, EventArgs e)
		{
			if (!this.ignoreControlEnabledUpdate)
			{
				Control control = (Control)sender;
				if (control.Enabled)
				{
					if (this.alreadyDisabledControls.Contains(control))
					{
						this.alreadyDisabledControls.Remove(control);
					}
				}
				else
				{
					this.alreadyDisabledControls.Add(control);
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0003B9E0 File Offset: 0x0003A9E0
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0003B9F8 File Offset: 0x0003A9F8
		public bool IsTopLevelDynamicControlsContainer
		{
			get
			{
				return this.isTopLevelDynamicControlsContainer;
			}
			set
			{
				this.isTopLevelDynamicControlsContainer = value;
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0003BA04 File Offset: 0x0003AA04
		public MyPanel()
		{
			this.balloonTip = null;
			this.tooltip = null;
			this.isDynamicScreenContainer = false;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0003BA80 File Offset: 0x0003AA80
		public void RegisterHelpText(int displayMethod, Control c, string title, string description)
		{
			switch (displayMethod)
			{
			case 1:
				if (this.balloonTip != null)
				{
					this.balloonTip.SetBalloonCaption(c, title);
					this.balloonTip.SetBalloonText(c, description);
				}
				break;
			case 2:
				if (this.tooltip != null)
				{
					this.tooltip.SetToolTip(c, title + Environment.NewLine + description);
				}
				break;
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0003BAFC File Offset: 0x0003AAFC
		public Image GetScreenShotFull()
		{
			this.AutoScrollPosition = new Point(0, 0);
			Bitmap bitmap = new Bitmap(this.DisplayRectangle.Width, this.DisplayRectangle.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				using (Brush brush = new SolidBrush(this.BackColor))
				{
					graphics.FillRectangle(brush, 0, 0, this.DisplayRectangle.Width, this.DisplayRectangle.Height);
				}
				IntPtr hdc = graphics.GetHdc();
				this.OnPaint(new PaintEventArgs(graphics, this.DisplayRectangle));
				MyPanel.SendMessage(this.Handle, 791, (int)hdc, 60);
				graphics.ReleaseHdc(hdc);
			}
			return bitmap;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0003BC08 File Offset: 0x0003AC08
		public static MyPanel FindMyPanel(Control c)
		{
			for (Control control = c; control != null; control = control.Parent)
			{
				if (control is MyPanel)
				{
					return (MyPanel)control;
				}
			}
			return null;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0003BC4C File Offset: 0x0003AC4C
		public void FormLoaded()
		{
			if (this.defaultActiveControl > 0)
			{
				Control control = this.FindControl(this, this.defaultActiveControl);
				if (control != null)
				{
					if (control.TopLevelControl != null && control.TopLevelControl is Form)
					{
						Form form = (Form)control.TopLevelControl;
						form.ActiveControl = control;
					}
					else
					{
						control.Focus();
					}
				}
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0003BCC8 File Offset: 0x0003ACC8
		public static Control FindFirstControl(Control parent, Type controlType)
		{
			Control result;
			if (parent.GetType() == controlType)
			{
				result = parent;
			}
			else
			{
				foreach (object obj in parent.Controls)
				{
					Control parent2 = (Control)obj;
					Control control = MyPanel.FindFirstControl(parent2, controlType);
					if (control != null)
					{
						return control;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0003BD60 File Offset: 0x0003AD60
		private Control FindControl(Control parent, int cid)
		{
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
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				Control control = this.FindControl(parent2, cid);
				if (control != null)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0003BE60 File Offset: 0x0003AE60
		public List<SearchMatchResult> FindControls(string searchString)
		{
			List<SearchMatchResult> result = new List<SearchMatchResult>();
			this.FindControls(this, searchString.ToLower(), ref result);
			return result;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0003BE8C File Offset: 0x0003AE8C
		private void FindControls(Control parent, string searchStringLowerCase, ref List<SearchMatchResult> matches)
		{
			if (parent.Text.ToLower().Contains(searchStringLowerCase))
			{
				SearchMatchResult item = new SearchMatchResult(searchStringLowerCase, parent);
				matches.Add(item);
			}
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				this.FindControls(parent2, searchStringLowerCase, ref matches);
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0003BF24 File Offset: 0x0003AF24
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0003BF36 File Offset: 0x0003AF36
		public void RefreshSummaryControl()
		{
			this.RefreshSummaryControl(this);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0003BF44 File Offset: 0x0003AF44
		private void RefreshSummaryControl(Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is MyWebBrowser)
				{
					MyWebBrowser myWebBrowser = (MyWebBrowser)control;
					myWebBrowser.RefreshSummary();
				}
				if (control.Controls.Count > 0)
				{
					this.RefreshSummaryControl(control);
				}
			}
		}

		// Token: 0x040005A6 RID: 1446
		private const int WM_PRINT = 791;

		// Token: 0x040005A8 RID: 1448
		private BalloonTip balloonTip;

		// Token: 0x040005A9 RID: 1449
		private ToolTip tooltip;

		// Token: 0x040005AA RID: 1450
		private bool isDynamicScreenContainer;

		// Token: 0x040005AB RID: 1451
		private object tag2 = null;

		// Token: 0x040005AC RID: 1452
		private object tag3 = null;

		// Token: 0x040005AD RID: 1453
		private int tagInt = -1;

		// Token: 0x040005AE RID: 1454
		private object screen = null;

		// Token: 0x040005AF RID: 1455
		private int defaultActiveControl = 0;

		// Token: 0x040005B0 RID: 1456
		private string student_no;

		// Token: 0x040005B1 RID: 1457
		private string firstName;

		// Token: 0x040005B2 RID: 1458
		private string lastName;

		// Token: 0x040005B3 RID: 1459
		private int pid;

		// Token: 0x040005B4 RID: 1460
		private string caption = "";

		// Token: 0x040005B5 RID: 1461
		private string primaryClientDescription;

		// Token: 0x040005B6 RID: 1462
		private int primaryClientPid;

		// Token: 0x040005B7 RID: 1463
		private bool isEnabled = true;

		// Token: 0x040005B8 RID: 1464
		private ArrayList alreadyDisabledControls = new ArrayList();

		// Token: 0x040005B9 RID: 1465
		private bool ignoreControlEnabledUpdate = false;

		// Token: 0x040005BA RID: 1466
		private bool isTopLevelDynamicControlsContainer = false;

		// Token: 0x020000C5 RID: 197
		[Flags]
		private enum DrawingOptions
		{
			// Token: 0x040005BC RID: 1468
			PRF_CHECKVISIBLE = 1,
			// Token: 0x040005BD RID: 1469
			PRF_NONCLIENT = 2,
			// Token: 0x040005BE RID: 1470
			PRF_CLIENT = 4,
			// Token: 0x040005BF RID: 1471
			PRF_ERASEBKGND = 8,
			// Token: 0x040005C0 RID: 1472
			PRF_CHILDREN = 16,
			// Token: 0x040005C1 RID: 1473
			PRF_OWNED = 32
		}
	}
}
