using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x0200044A RID: 1098
	public class PrintControllerWithStatusDialog : PrintController
	{
		// Token: 0x06004C2A RID: 19498 RVA: 0x0013C1CC File Offset: 0x0013A3CC
		public PrintControllerWithStatusDialog(PrintController underlyingController) : this(underlyingController, SR.GetString("PrintControllerWithStatusDialog_DialogTitlePrint"))
		{
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x0013C1DF File Offset: 0x0013A3DF
		public PrintControllerWithStatusDialog(PrintController underlyingController, string dialogTitle)
		{
			this.underlyingController = underlyingController;
			this.dialogTitle = dialogTitle;
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06004C2C RID: 19500 RVA: 0x0013C1F5 File Offset: 0x0013A3F5
		public override bool IsPreview
		{
			get
			{
				return this.underlyingController != null && this.underlyingController.IsPreview;
			}
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0013C20C File Offset: 0x0013A40C
		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			base.OnStartPrint(document, e);
			this.document = document;
			this.pageNumber = 1;
			if (SystemInformation.UserInteractive)
			{
				this.backgroundThread = new PrintControllerWithStatusDialog.BackgroundThread(this);
			}
			try
			{
				this.underlyingController.OnStartPrint(document, e);
			}
			catch
			{
				if (this.backgroundThread != null)
				{
					this.backgroundThread.Stop();
				}
				throw;
			}
			finally
			{
				if (this.backgroundThread != null && this.backgroundThread.canceled)
				{
					e.Cancel = true;
				}
			}
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x0013C2A4 File Offset: 0x0013A4A4
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			base.OnStartPage(document, e);
			if (this.backgroundThread != null)
			{
				this.backgroundThread.UpdateLabel();
			}
			Graphics result = this.underlyingController.OnStartPage(document, e);
			if (this.backgroundThread != null && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			return result;
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0013C2F8 File Offset: 0x0013A4F8
		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
			this.underlyingController.OnEndPage(document, e);
			if (this.backgroundThread != null && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			this.pageNumber++;
			base.OnEndPage(document, e);
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0013C344 File Offset: 0x0013A544
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
			this.underlyingController.OnEndPrint(document, e);
			if (this.backgroundThread != null && this.backgroundThread.canceled)
			{
				e.Cancel = true;
			}
			if (this.backgroundThread != null)
			{
				this.backgroundThread.Stop();
			}
			base.OnEndPrint(document, e);
		}

		// Token: 0x0400287D RID: 10365
		private PrintController underlyingController;

		// Token: 0x0400287E RID: 10366
		private PrintDocument document;

		// Token: 0x0400287F RID: 10367
		private PrintControllerWithStatusDialog.BackgroundThread backgroundThread;

		// Token: 0x04002880 RID: 10368
		private int pageNumber;

		// Token: 0x04002881 RID: 10369
		private string dialogTitle;

		// Token: 0x02000832 RID: 2098
		private class BackgroundThread
		{
			// Token: 0x06007059 RID: 28761 RVA: 0x0019BDF5 File Offset: 0x00199FF5
			internal BackgroundThread(PrintControllerWithStatusDialog parent)
			{
				this.parent = parent;
				this.thread = new Thread(new ThreadStart(this.Run));
				this.thread.SetApartmentState(ApartmentState.STA);
				this.thread.Start();
			}

			// Token: 0x0600705A RID: 28762 RVA: 0x0019BE34 File Offset: 0x0019A034
			[UIPermission(SecurityAction.Assert, Window = UIPermissionWindow.AllWindows)]
			[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
			private void Run()
			{
				try
				{
					lock (this)
					{
						if (this.alreadyStopped)
						{
							return;
						}
						this.dialog = new PrintControllerWithStatusDialog.StatusDialog(this, this.parent.dialogTitle);
						this.ThreadUnsafeUpdateLabel();
						this.dialog.Visible = true;
					}
					if (!this.alreadyStopped)
					{
						Application.Run(this.dialog);
					}
				}
				finally
				{
					lock (this)
					{
						if (this.dialog != null)
						{
							this.dialog.Dispose();
							this.dialog = null;
						}
					}
				}
			}

			// Token: 0x0600705B RID: 28763 RVA: 0x0019BEFC File Offset: 0x0019A0FC
			internal void Stop()
			{
				lock (this)
				{
					if (this.dialog != null && this.dialog.IsHandleCreated)
					{
						this.dialog.BeginInvoke(new MethodInvoker(this.dialog.Close));
					}
					else
					{
						this.alreadyStopped = true;
					}
				}
			}

			// Token: 0x0600705C RID: 28764 RVA: 0x0019BF6C File Offset: 0x0019A16C
			private void ThreadUnsafeUpdateLabel()
			{
				this.dialog.label1.Text = SR.GetString("PrintControllerWithStatusDialog_NowPrinting", new object[]
				{
					this.parent.pageNumber,
					this.parent.document.DocumentName
				});
			}

			// Token: 0x0600705D RID: 28765 RVA: 0x0019BFBF File Offset: 0x0019A1BF
			internal void UpdateLabel()
			{
				if (this.dialog != null && this.dialog.IsHandleCreated)
				{
					this.dialog.BeginInvoke(new MethodInvoker(this.ThreadUnsafeUpdateLabel));
				}
			}

			// Token: 0x0400435C RID: 17244
			private PrintControllerWithStatusDialog parent;

			// Token: 0x0400435D RID: 17245
			private PrintControllerWithStatusDialog.StatusDialog dialog;

			// Token: 0x0400435E RID: 17246
			private Thread thread;

			// Token: 0x0400435F RID: 17247
			internal bool canceled;

			// Token: 0x04004360 RID: 17248
			private bool alreadyStopped;
		}

		// Token: 0x02000833 RID: 2099
		private class StatusDialog : Form
		{
			// Token: 0x0600705E RID: 28766 RVA: 0x0019BFEE File Offset: 0x0019A1EE
			internal StatusDialog(PrintControllerWithStatusDialog.BackgroundThread backgroundThread, string dialogTitle)
			{
				this.InitializeComponent();
				this.backgroundThread = backgroundThread;
				this.Text = dialogTitle;
				this.MinimumSize = base.Size;
			}

			// Token: 0x17001882 RID: 6274
			// (get) Token: 0x0600705F RID: 28767 RVA: 0x001620FA File Offset: 0x001602FA
			private static bool IsRTLResources
			{
				get
				{
					return SR.GetString("RTL") != "RTL_False";
				}
			}

			// Token: 0x06007060 RID: 28768 RVA: 0x0019C018 File Offset: 0x0019A218
			private void InitializeComponent()
			{
				if (PrintControllerWithStatusDialog.StatusDialog.IsRTLResources)
				{
					this.RightToLeft = RightToLeft.Yes;
				}
				this.tableLayoutPanel1 = new TableLayoutPanel();
				this.label1 = new Label();
				this.button1 = new Button();
				this.label1.AutoSize = true;
				this.label1.Location = new Point(8, 16);
				this.label1.TextAlign = ContentAlignment.MiddleCenter;
				this.label1.Size = new Size(240, 64);
				this.label1.TabIndex = 1;
				this.label1.Anchor = AnchorStyles.None;
				this.button1.AutoSize = true;
				this.button1.Size = new Size(75, 23);
				this.button1.TabIndex = 0;
				this.button1.Text = SR.GetString("PrintControllerWithStatusDialog_Cancel");
				this.button1.Location = new Point(88, 88);
				this.button1.Anchor = AnchorStyles.None;
				this.button1.Click += this.button1_Click;
				this.tableLayoutPanel1.AutoSize = true;
				this.tableLayoutPanel1.ColumnCount = 1;
				this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
				this.tableLayoutPanel1.Dock = DockStyle.Fill;
				this.tableLayoutPanel1.Location = new Point(0, 0);
				this.tableLayoutPanel1.RowCount = 2;
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
				this.tableLayoutPanel1.TabIndex = 0;
				this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
				this.tableLayoutPanel1.Controls.Add(this.button1, 0, 1);
				base.AutoScaleDimensions = new Size(6, 13);
				base.AutoScaleMode = AutoScaleMode.Font;
				base.MaximizeBox = false;
				base.ControlBox = false;
				base.MinimizeBox = false;
				Size size = new Size(256, 122);
				if (DpiHelper.IsScalingRequired)
				{
					base.ClientSize = DpiHelper.LogicalToDeviceUnits(size, 0);
				}
				else
				{
					base.ClientSize = size;
				}
				base.CancelButton = this.button1;
				base.SizeGripStyle = SizeGripStyle.Hide;
				base.Controls.Add(this.tableLayoutPanel1);
			}

			// Token: 0x06007061 RID: 28769 RVA: 0x0019C277 File Offset: 0x0019A477
			private void button1_Click(object sender, EventArgs e)
			{
				this.button1.Enabled = false;
				this.label1.Text = SR.GetString("PrintControllerWithStatusDialog_Canceling");
				this.backgroundThread.canceled = true;
			}

			// Token: 0x04004361 RID: 17249
			internal Label label1;

			// Token: 0x04004362 RID: 17250
			private Button button1;

			// Token: 0x04004363 RID: 17251
			private TableLayoutPanel tableLayoutPanel1;

			// Token: 0x04004364 RID: 17252
			private PrintControllerWithStatusDialog.BackgroundThread backgroundThread;
		}
	}
}
