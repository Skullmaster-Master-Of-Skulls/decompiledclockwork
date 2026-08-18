using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200012E RID: 302
	internal partial class TreeViewImageGenerator : DesignerForm
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00045E14 File Offset: 0x00044014
		public TreeViewImageGenerator(System.Web.UI.WebControls.TreeView treeView) : base(treeView.Site)
		{
			this._previewPictureBox = new PictureBox();
			this._previewLabel = new System.Windows.Forms.Label();
			this._previewPanel = new System.Windows.Forms.Panel();
			this._previewFrameTextBox = new System.Windows.Forms.TextBox();
			this._okButton = new System.Windows.Forms.Button();
			this._cancelButton = new System.Windows.Forms.Button();
			this._folderNameLabel = new System.Windows.Forms.Label();
			this._folderNameTextBox = new System.Windows.Forms.TextBox();
			this._propertiesLabel = new System.Windows.Forms.Label();
			this._propertyGrid = new VsPropertyGrid(base.ServiceProvider);
			this._progressBar = new ProgressBar();
			this._progressBarLabel = new System.Windows.Forms.Label();
			this._previewPanel.SuspendLayout();
			base.SuspendLayout();
			this._previewPictureBox.Name = "_previewPictureBox";
			this._previewPictureBox.SizeMode = PictureBoxSizeMode.Normal;
			this._previewPictureBox.TabIndex = 10;
			this._previewPictureBox.TabStop = false;
			this._previewPictureBox.BackColor = Color.White;
			this._previewLabel.Location = new Point(12, 12);
			this._previewLabel.Name = "_previewLabel";
			this._previewLabel.Size = new Size(180, 14);
			this._previewLabel.TabIndex = 9;
			this._previewLabel.Text = SR.GetString("TreeViewImageGenerator_Preview");
			this._previewPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._previewPanel.AutoScroll = true;
			this._previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this._previewPanel.Controls.AddRange(new Control[]
			{
				this._previewPictureBox
			});
			this._previewPanel.Location = new Point(13, 29);
			this._previewPanel.Name = "_previewPanel";
			this._previewPanel.Size = new Size(178, 242);
			this._previewPanel.TabIndex = 11;
			this._previewFrameTextBox.Multiline = true;
			this._previewFrameTextBox.Enabled = false;
			this._previewFrameTextBox.TabStop = false;
			this._previewFrameTextBox.Location = new Point(12, 28);
			this._previewFrameTextBox.Size = new Size(180, 244);
			this._okButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this._okButton.FlatStyle = FlatStyle.System;
			this._okButton.Location = new Point(376, 324);
			this._okButton.Name = "_okButton";
			this._okButton.Size = new Size(75, 23);
			this._okButton.TabIndex = 20;
			this._okButton.Text = SR.GetString("OKCaption");
			this._okButton.Click += this.OnOKButtonClick;
			this._cancelButton.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this._cancelButton.FlatStyle = FlatStyle.System;
			this._cancelButton.Location = new Point(456, 324);
			this._cancelButton.Name = "_cancelButton";
			this._cancelButton.Size = new Size(75, 23);
			this._cancelButton.TabIndex = 21;
			this._cancelButton.Text = SR.GetString("CancelCaption");
			this._cancelButton.Click += this.OnCancelButtonClick;
			this._folderNameLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this._folderNameLabel.Location = new Point(213, 279);
			this._folderNameLabel.Name = "_folderNameLabel";
			this._folderNameLabel.Size = new Size(315, 14);
			this._folderNameLabel.TabIndex = 17;
			this._folderNameLabel.Text = SR.GetString("TreeViewImageGenerator_FolderName");
			this._folderNameTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._folderNameTextBox.Location = new Point(213, 295);
			this._folderNameTextBox.Name = "_folderNameTextBox";
			this._folderNameTextBox.Size = new Size(315, 20);
			this._folderNameTextBox.TabIndex = 18;
			this._folderNameTextBox.Text = SR.GetString("TreeViewImageGenerator_DefaultFolderName");
			this._folderNameTextBox.WordWrap = false;
			this._folderNameTextBox.TextChanged += this.OnFolderNameTextBoxTextChanged;
			this._progressBarLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this._progressBarLabel.Location = new Point(12, 279);
			this._progressBarLabel.Name = "_progressBarLabel";
			this._progressBarLabel.Size = new Size(180, 14);
			this._progressBarLabel.Text = SR.GetString("TreeViewImageGenerator_ProgressBarName");
			this._progressBarLabel.Visible = false;
			this._progressBar.Location = new Point(12, 295);
			this._progressBar.Size = new Size(180, 16);
			this._progressBar.Maximum = 16;
			this._progressBar.Minimum = 0;
			this._progressBar.Visible = false;
			this._propertiesLabel.Location = new Point(213, 12);
			this._propertiesLabel.Name = "_propertiesLabel";
			this._propertiesLabel.Size = new Size(315, 14);
			this._propertiesLabel.TabIndex = 12;
			this._propertiesLabel.Text = SR.GetString("TreeViewImageGenerator_Properties");
			this._propertyGrid.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this._propertyGrid.CommandsVisibleIfAvailable = true;
			this._propertyGrid.LargeButtons = false;
			this._propertyGrid.LineColor = SystemColors.ScrollBar;
			this._propertyGrid.Location = new Point(213, 28);
			this._propertyGrid.Name = "_propertyGrid";
			this._propertyGrid.PropertySort = PropertySort.Alphabetical;
			this._propertyGrid.Size = new Size(315, 244);
			this._propertyGrid.TabIndex = 13;
			this._propertyGrid.ToolbarVisible = true;
			this._propertyGrid.ViewBackColor = SystemColors.Window;
			this._propertyGrid.ViewForeColor = SystemColors.WindowText;
			this._propertyGrid.PropertyValueChanged += this.OnPropertyGridPropertyValueChanged;
			base.AcceptButton = this._okButton;
			base.CancelButton = this._cancelButton;
			base.ClientSize = new Size(540, 359);
			base.Controls.AddRange(new Control[]
			{
				this._propertyGrid,
				this._propertiesLabel,
				this._progressBar,
				this._progressBarLabel,
				this._folderNameTextBox,
				this._folderNameLabel,
				this._cancelButton,
				this._okButton,
				this._previewPanel,
				this._previewLabel,
				this._previewFrameTextBox
			});
			this.MinimumSize = new Size(540, 359);
			base.Name = "TreeLineImageGenerator";
			this.Text = SR.GetString("TreeViewImageGenerator_Title");
			base.Resize += this.OnFormResize;
			this._previewPanel.ResumeLayout(false);
			base.InitializeForm();
			base.ResumeLayout(false);
			this._imageInfo = new TreeViewImageGenerator.LineImageInfo();
			this._propertyGrid.SelectedObject = this._imageInfo;
			this._treeView = treeView;
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = null;
			this.UpdatePreview();
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00046593 File Offset: 0x00044793
		private System.Drawing.Image DefaultMinusImage
		{
			get
			{
				if (TreeViewImageGenerator.defaultMinusImage == null)
				{
					TreeViewImageGenerator.defaultMinusImage = new Bitmap(typeof(TreeViewImageGenerator), "Minus.gif");
				}
				return TreeViewImageGenerator.defaultMinusImage;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x000465BA File Offset: 0x000447BA
		private System.Drawing.Image DefaultPlusImage
		{
			get
			{
				if (TreeViewImageGenerator.defaultPlusImage == null)
				{
					TreeViewImageGenerator.defaultPlusImage = new Bitmap(typeof(TreeViewImageGenerator), "Plus.gif");
				}
				return TreeViewImageGenerator.defaultPlusImage;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x000465E1 File Offset: 0x000447E1
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.TreeView.ImageGenerator";
			}
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000465E8 File Offset: 0x000447E8
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000465F0 File Offset: 0x000447F0
		private void OnFormResize(object sender, EventArgs e)
		{
			this.UpdatePreview();
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000465F8 File Offset: 0x000447F8
		private void OnOKButtonClick(object sender, EventArgs e)
		{
			string text = this._folderNameTextBox.Text.Trim();
			if (text.Length == 0)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_MissingFolderName"));
				return;
			}
			if (text.IndexOfAny(Path.GetInvalidPathChars()) != -1)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_InvalidFolderName", new object[]
				{
					text
				}));
				return;
			}
			IWebApplication webApplication = (IWebApplication)this._treeView.Site.GetService(typeof(IWebApplication));
			if (webApplication == null)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_ErrorWriting"));
				return;
			}
			IFolderProjectItem folderProjectItem = (IFolderProjectItem)webApplication.RootProjectItem;
			IProjectItem projectItemFromUrl = webApplication.GetProjectItemFromUrl(Path.Combine("~/", text));
			if (projectItemFromUrl != null && !(projectItemFromUrl is IFolderProjectItem))
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_DocumentExists", new object[]
				{
					text
				}));
				return;
			}
			IFolderProjectItem folderProjectItem2 = (IFolderProjectItem)projectItemFromUrl;
			if (folderProjectItem2 == null)
			{
				if (UIServiceHelper.ShowMessage(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_NonExistentFolderName", new object[]
				{
					text
				}), SR.GetString("TreeViewImageGenerator_Title"), MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					try
					{
						folderProjectItem2 = folderProjectItem.AddFolder(text);
						goto IL_149;
					}
					catch
					{
						UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_ErrorCreatingFolder", new object[]
						{
							text
						}));
						return;
					}
				}
				return;
			}
			IL_149:
			System.Drawing.Image expandImage = this._imageInfo.ExpandImage;
			if (expandImage == null)
			{
				expandImage = this.DefaultPlusImage;
			}
			System.Drawing.Image collapseImage = this._imageInfo.CollapseImage;
			if (collapseImage == null)
			{
				collapseImage = this.DefaultMinusImage;
			}
			System.Drawing.Image noExpandImage = this._imageInfo.NoExpandImage;
			int width = this._imageInfo.Width;
			if (width < 1)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_InvalidValue", new object[]
				{
					"Width"
				}));
				return;
			}
			int height = this._imageInfo.Height;
			if (height < 1)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_InvalidValue", new object[]
				{
					"Height"
				}));
				return;
			}
			int lineWidth = this._imageInfo.LineWidth;
			if (lineWidth < 1)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_InvalidValue", new object[]
				{
					"LineWidth"
				}));
				return;
			}
			int lineStyle = (int)this._imageInfo.LineStyle;
			Color lineColor = this._imageInfo.LineColor;
			this._progressBar.Value = 0;
			this._progressBar.Visible = true;
			this._progressBarLabel.Visible = true;
			try
			{
				bool flag = false;
				bool flag2 = false;
				Bitmap bitmap = new Bitmap(width, height);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.FillRectangle(new SolidBrush(this._imageInfo.TransparentColor), 0, 0, width, height);
				this.RenderImage(graphics, 0, 0, width, height, 'i', lineStyle, lineWidth, lineColor, null);
				flag2 |= this.SaveTransparentGif(bitmap, folderProjectItem2, "i.gif", ref flag);
				ProgressBar progressBar = this._progressBar;
				int value = progressBar.Value;
				progressBar.Value = value + 1;
				string text2 = "-rtl ";
				for (int i = 0; i < text2.Length; i++)
				{
					bitmap = new Bitmap(width, height);
					graphics = Graphics.FromImage(bitmap);
					graphics.FillRectangle(new SolidBrush(this._imageInfo.TransparentColor), 0, 0, width, height);
					this.RenderImage(graphics, 0, 0, width, height, text2[i], lineStyle, lineWidth, lineColor, collapseImage);
					graphics.Dispose();
					string text3 = "minus.gif";
					if (text2[i] == '-')
					{
						text3 = "dash" + text3;
					}
					else if (text2[i] != ' ')
					{
						text3 = text2[i].ToString() + text3;
					}
					flag2 |= this.SaveTransparentGif(bitmap, folderProjectItem2, text3, ref flag);
					ProgressBar progressBar2 = this._progressBar;
					value = progressBar2.Value;
					progressBar2.Value = value + 1;
				}
				for (int j = 0; j < text2.Length; j++)
				{
					bitmap = new Bitmap(width, height);
					graphics = Graphics.FromImage(bitmap);
					graphics.FillRectangle(new SolidBrush(this._imageInfo.TransparentColor), 0, 0, width, height);
					this.RenderImage(graphics, 0, 0, width, height, text2[j], lineStyle, lineWidth, lineColor, expandImage);
					graphics.Dispose();
					string text3 = "plus.gif";
					if (text2[j] == '-')
					{
						text3 = "dash" + text3;
					}
					else if (text2[j] != ' ')
					{
						text3 = text2[j].ToString() + text3;
					}
					flag2 |= this.SaveTransparentGif(bitmap, folderProjectItem2, text3, ref flag);
					ProgressBar progressBar3 = this._progressBar;
					value = progressBar3.Value;
					progressBar3.Value = value + 1;
				}
				for (int k = 0; k < text2.Length; k++)
				{
					bitmap = new Bitmap(width, height);
					graphics = Graphics.FromImage(bitmap);
					graphics.FillRectangle(new SolidBrush(this._imageInfo.TransparentColor), 0, 0, width, height);
					this.RenderImage(graphics, 0, 0, width, height, text2[k], lineStyle, lineWidth, lineColor, noExpandImage);
					graphics.Dispose();
					string text3 = ".gif";
					if (text2[k] == '-')
					{
						text3 = "dash" + text3;
					}
					else if (text2[k] == ' ')
					{
						text3 = "noexpand" + text3;
					}
					else
					{
						text3 = text2[k].ToString() + text3;
					}
					flag2 |= this.SaveTransparentGif(bitmap, folderProjectItem2, text3, ref flag);
					ProgressBar progressBar4 = this._progressBar;
					value = progressBar4.Value;
					progressBar4.Value = value + 1;
				}
				this._progressBar.Visible = false;
				this._progressBarLabel.Visible = false;
				if (flag2)
				{
					UIServiceHelper.ShowMessage(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_LineImagesGenerated", new object[]
					{
						text
					}));
				}
			}
			catch
			{
				this._progressBar.Visible = false;
				this._progressBarLabel.Visible = false;
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_ErrorWriting", new object[]
				{
					text
				}));
				return;
			}
			this._treeView.LineImagesFolder = "~/" + text;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00046C90 File Offset: 0x00044E90
		private void OnFolderNameTextBoxTextChanged(object sender, EventArgs e)
		{
			if (this._folderNameTextBox.Text.Trim().Length > 0)
			{
				this._okButton.Enabled = true;
				return;
			}
			this._okButton.Enabled = false;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000465F0 File Offset: 0x000447F0
		private void OnPropertyGridPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			this.UpdatePreview();
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00046CC4 File Offset: 0x00044EC4
		private void RenderImage(Graphics g, int x, int y, int width, int height, char lineType, int lineStyle, int lineWidth, Color lineColor, System.Drawing.Image image)
		{
			Pen pen = new Pen(lineColor, (float)lineWidth);
			if (lineStyle != 0)
			{
				if (lineStyle != 1)
				{
					pen.DashStyle = DashStyle.Solid;
				}
				else
				{
					pen.DashStyle = DashStyle.Dash;
				}
			}
			else
			{
				pen.DashStyle = DashStyle.Dot;
			}
			if (lineType == 'i')
			{
				g.DrawLine(pen, x + width / 2, y, x + width / 2, y + height);
			}
			else if (lineType == 'r')
			{
				g.DrawLine(pen, x + width / 2, y + height / 2, x + width, y + height / 2);
				g.DrawLine(pen, x + width / 2, y + height / 2, x + width / 2, y + height);
			}
			else if (lineType == 't')
			{
				g.DrawLine(pen, x + width / 2, y, x + width / 2, y + height);
				g.DrawLine(pen, x + width / 2, y + height / 2, x + width, y + height / 2);
			}
			else if (lineType == 'l')
			{
				g.DrawLine(pen, x + width / 2, y, x + width / 2, y + height / 2);
				g.DrawLine(pen, x + width / 2, y + height / 2, x + width, y + height / 2);
			}
			else if (lineType == '-')
			{
				g.DrawLine(pen, x + width / 2, y + height / 2, x + width, y + height / 2);
			}
			if (image != null)
			{
				int num = Math.Min(image.Width, width);
				int num2 = Math.Min(image.Height, height);
				g.DrawImage(image, x + (width - num + 1) / 2, y + (height - num2 + 1) / 2, num, num2);
			}
			pen.Dispose();
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00046E50 File Offset: 0x00045050
		private void UpdatePreview()
		{
			System.Drawing.Image expandImage = this._imageInfo.ExpandImage;
			if (expandImage == null)
			{
				expandImage = this.DefaultPlusImage;
			}
			System.Drawing.Image collapseImage = this._imageInfo.CollapseImage;
			if (collapseImage == null)
			{
				collapseImage = this.DefaultMinusImage;
			}
			System.Drawing.Image noExpandImage = this._imageInfo.NoExpandImage;
			int width = this._imageInfo.Width;
			if (width < 1)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_InvalidValue", new object[]
				{
					"Width"
				}));
				return;
			}
			int height = this._imageInfo.Height;
			if (height < 1)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_InvalidValue", new object[]
				{
					"Height"
				}));
				return;
			}
			int lineWidth = this._imageInfo.LineWidth;
			if (lineWidth < 1)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, SR.GetString("TreeViewImageGenerator_InvalidValue", new object[]
				{
					"LineWidth"
				}));
				return;
			}
			int lineStyle = (int)this._imageInfo.LineStyle;
			Color lineColor = this._imageInfo.LineColor;
			Font font = new Font("Tahoma", 10f);
			Graphics graphics = Graphics.FromHwnd(base.Handle);
			int num = width * 2 + (int)graphics.MeasureString(SR.GetString("TreeViewImageGenerator_SampleParent", new object[]
			{
				1
			}), font).Width;
			int num2 = Math.Max((int)graphics.MeasureString(SR.GetString("TreeViewImageGenerator_SampleParent", new object[]
			{
				1
			}), font).Height, height);
			graphics.Dispose();
			int num3 = num2 * 6;
			int num4 = Math.Max(width, this._treeView.NodeIndent);
			Bitmap bitmap = new Bitmap(Math.Max(num, this._previewPanel.Width), Math.Max(num3, this._previewPanel.Height));
			Graphics graphics2 = Graphics.FromImage(bitmap);
			int num5 = 5;
			int num6 = 5;
			graphics2.FillRectangle(Brushes.White, num5, num6, num, num3);
			this.RenderImage(graphics2, num5, num6, width, height, '-', lineStyle, lineWidth, lineColor, expandImage);
			num5 += width;
			graphics2.DrawString(SR.GetString("TreeViewImageGenerator_SampleRoot", new object[]
			{
				1
			}), font, Brushes.Black, (float)num5, (float)num6 + ((float)height - graphics2.MeasureString(SR.GetString("TreeViewImageGenerator_SampleRoot", new object[]
			{
				1
			}), font).Height + 1f) / 2f);
			num6 += num2;
			num5 -= width;
			this.RenderImage(graphics2, num5, num6, width, height, 'r', lineStyle, lineWidth, lineColor, collapseImage);
			num5 += width;
			graphics2.DrawString(SR.GetString("TreeViewImageGenerator_SampleRoot", new object[]
			{
				2
			}), font, Brushes.Black, (float)num5, (float)num6 + ((float)height - graphics2.MeasureString(SR.GetString("TreeViewImageGenerator_SampleRoot", new object[]
			{
				2
			}), font).Height + 1f) / 2f);
			num6 += num2;
			num5 -= width;
			this.RenderImage(graphics2, num5, num6, width, height, 'i', lineStyle, lineWidth, lineColor, null);
			num5 += num4;
			this.RenderImage(graphics2, num5, num6, width, height, 't', lineStyle, lineWidth, lineColor, expandImage);
			num5 += width;
			graphics2.DrawString(SR.GetString("TreeViewImageGenerator_SampleParent", new object[]
			{
				1
			}), font, Brushes.Black, (float)num5, (float)num6 + ((float)height - graphics2.MeasureString(SR.GetString("TreeViewImageGenerator_SampleParent", new object[]
			{
				1
			}), font).Height + 1f) / 2f);
			num6 += num2;
			num5 -= width + num4;
			this.RenderImage(graphics2, num5, num6, width, height, 'i', lineStyle, lineWidth, lineColor, null);
			num5 += num4;
			this.RenderImage(graphics2, num5, num6, width, height, 't', lineStyle, lineWidth, lineColor, noExpandImage);
			num5 += width;
			graphics2.DrawString(SR.GetString("TreeViewImageGenerator_SampleLeaf", new object[]
			{
				1
			}), font, Brushes.Black, (float)num5, (float)num6 + ((float)height - graphics2.MeasureString(SR.GetString("TreeViewImageGenerator_SampleLeaf", new object[]
			{
				1
			}), font).Height + 1f) / 2f);
			num6 += num2;
			num5 -= width + num4;
			this.RenderImage(graphics2, num5, num6, width, height, 'i', lineStyle, lineWidth, lineColor, null);
			num5 += num4;
			this.RenderImage(graphics2, num5, num6, width, height, 'l', lineStyle, lineWidth, lineColor, noExpandImage);
			num5 += width;
			graphics2.DrawString(SR.GetString("TreeViewImageGenerator_SampleLeaf", new object[]
			{
				2
			}), font, Brushes.Black, (float)num5, (float)num6 + ((float)height - graphics2.MeasureString(SR.GetString("TreeViewImageGenerator_SampleLeaf", new object[]
			{
				2
			}), font).Height + 1f) / 2f);
			num6 += num2;
			num5 -= width + num4;
			this.RenderImage(graphics2, num5, num6, width, height, 'l', lineStyle, lineWidth, lineColor, expandImage);
			num5 += width;
			graphics2.DrawString(SR.GetString("TreeViewImageGenerator_SampleRoot", new object[]
			{
				3
			}), font, Brushes.Black, (float)num5, (float)num6 + ((float)height - graphics2.MeasureString(SR.GetString("TreeViewImageGenerator_SampleRoot", new object[]
			{
				3
			}), font).Height + 1f) / 2f);
			graphics2.Dispose();
			bitmap.MakeTransparent(this._imageInfo.TransparentColor);
			this._previewPictureBox.Image = bitmap;
			this._previewPictureBox.Width = Math.Max(num, this._previewPanel.Width);
			this._previewPictureBox.Height = Math.Max(num3, this._previewPanel.Height);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00047474 File Offset: 0x00045674
		private unsafe static System.Drawing.Image ReduceColors(Bitmap bitmap, int maxColors, int numBits, Color transparentColor)
		{
			if (numBits < 3 || numBits > 8)
			{
				throw new ArgumentOutOfRangeException("numBits");
			}
			if (maxColors < 16)
			{
				throw new ArgumentOutOfRangeException("maxColors");
			}
			int width = bitmap.Width;
			int height = bitmap.Height;
			TreeViewImageGenerator.Octree octree = new TreeViewImageGenerator.Octree(maxColors, numBits, transparentColor);
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					octree.AddColor(bitmap.GetPixel(i, j));
				}
			}
			TreeViewImageGenerator.ColorIndexTable colorIndexTable = octree.GetColorIndexTable();
			Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
			ColorPalette palette = bitmap2.Palette;
			Rectangle rect = new Rectangle(0, 0, width, height);
			BitmapData bitmapData = bitmap2.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
			IntPtr scan = bitmapData.Scan0;
			byte* ptr;
			if (bitmapData.Stride > 0)
			{
				ptr = (byte*)scan.ToPointer();
			}
			else
			{
				ptr = (byte*)scan.ToPointer() + bitmapData.Stride * (height - 1);
			}
			int num = Math.Abs(bitmapData.Stride);
			for (int k = 0; k < height; k++)
			{
				for (int l = 0; l < width; l++)
				{
					byte* ptr2 = ptr + k * num + l;
					Color pixel = bitmap.GetPixel(l, k);
					byte b = (byte)colorIndexTable[pixel];
					*ptr2 = b;
				}
			}
			colorIndexTable.CopyToColorPalette(palette);
			bitmap2.Palette = palette;
			bitmap2.UnlockBits(bitmapData);
			return bitmap2;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000475CC File Offset: 0x000457CC
		private bool SaveTransparentGif(Bitmap bitmap, IFolderProjectItem folder, string name, ref bool overwrite)
		{
			System.Drawing.Image image = TreeViewImageGenerator.ReduceColors(bitmap, 256, 5, this._imageInfo.TransparentColor);
			bool result = false;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				image.Save(memoryStream, ImageFormat.Gif);
				memoryStream.Flush();
				memoryStream.Capacity = (int)memoryStream.Length;
				folder.AddDocument(name, memoryStream.GetBuffer());
			}
			finally
			{
				image.Dispose();
			}
			return result;
		}

		// Token: 0x04000686 RID: 1670
		private static System.Drawing.Image defaultMinusImage;

		// Token: 0x04000687 RID: 1671
		private static System.Drawing.Image defaultPlusImage;

		// Token: 0x04000688 RID: 1672
		private System.Web.UI.WebControls.TreeView _treeView;

		// Token: 0x04000689 RID: 1673
		private PictureBox _previewPictureBox;

		// Token: 0x0400068A RID: 1674
		private System.Windows.Forms.TextBox _previewFrameTextBox;

		// Token: 0x0400068B RID: 1675
		private System.Windows.Forms.Label _previewLabel;

		// Token: 0x0400068C RID: 1676
		private System.Windows.Forms.Panel _previewPanel;

		// Token: 0x0400068D RID: 1677
		private System.Windows.Forms.Button _okButton;

		// Token: 0x0400068E RID: 1678
		private System.Windows.Forms.Button _cancelButton;

		// Token: 0x0400068F RID: 1679
		private System.Windows.Forms.Label _folderNameLabel;

		// Token: 0x04000690 RID: 1680
		private System.Windows.Forms.Label _propertiesLabel;

		// Token: 0x04000691 RID: 1681
		private PropertyGrid _propertyGrid;

		// Token: 0x04000692 RID: 1682
		private System.Windows.Forms.TextBox _folderNameTextBox;

		// Token: 0x04000693 RID: 1683
		private ProgressBar _progressBar;

		// Token: 0x04000694 RID: 1684
		private System.Windows.Forms.Label _progressBarLabel;

		// Token: 0x04000695 RID: 1685
		private TreeViewImageGenerator.LineImageInfo _imageInfo;

		// Token: 0x02000451 RID: 1105
		private enum LineStyle
		{
			// Token: 0x04001D2B RID: 7467
			Dotted,
			// Token: 0x04001D2C RID: 7468
			Dashed,
			// Token: 0x04001D2D RID: 7469
			Solid
		}

		// Token: 0x02000452 RID: 1106
		private class LineImageInfo
		{
			// Token: 0x0600293E RID: 10558 RVA: 0x000F9E62 File Offset: 0x000F8062
			public LineImageInfo()
			{
				this._height = 20;
				this._width = 19;
				this._lineWidth = 1;
				this._lineStyle = TreeViewImageGenerator.LineStyle.Dotted;
				this._lineColor = Color.Black;
				this._transparentColor = Color.Magenta;
			}

			// Token: 0x170008B7 RID: 2231
			// (get) Token: 0x0600293F RID: 10559 RVA: 0x000F9E9E File Offset: 0x000F809E
			// (set) Token: 0x06002940 RID: 10560 RVA: 0x000F9EA6 File Offset: 0x000F80A6
			[DefaultValue(null)]
			[SRDescription("TreeViewImageGenerator_CollapseImage")]
			public System.Drawing.Image CollapseImage
			{
				get
				{
					return this._collapseImage;
				}
				set
				{
					this._collapseImage = value;
				}
			}

			// Token: 0x170008B8 RID: 2232
			// (get) Token: 0x06002941 RID: 10561 RVA: 0x000F9EAF File Offset: 0x000F80AF
			// (set) Token: 0x06002942 RID: 10562 RVA: 0x000F9EB7 File Offset: 0x000F80B7
			[DefaultValue(null)]
			[SRDescription("TreeViewImageGenerator_ExpandImage")]
			public System.Drawing.Image ExpandImage
			{
				get
				{
					return this._expandImage;
				}
				set
				{
					this._expandImage = value;
				}
			}

			// Token: 0x170008B9 RID: 2233
			// (get) Token: 0x06002943 RID: 10563 RVA: 0x000F9EC0 File Offset: 0x000F80C0
			// (set) Token: 0x06002944 RID: 10564 RVA: 0x000F9EC8 File Offset: 0x000F80C8
			[SRDescription("TreeViewImageGenerator_LineColor")]
			public Color LineColor
			{
				get
				{
					return this._lineColor;
				}
				set
				{
					this._lineColor = value;
				}
			}

			// Token: 0x170008BA RID: 2234
			// (get) Token: 0x06002945 RID: 10565 RVA: 0x000F9ED1 File Offset: 0x000F80D1
			// (set) Token: 0x06002946 RID: 10566 RVA: 0x000F9ED9 File Offset: 0x000F80D9
			[SRDescription("TreeViewImageGenerator_LineStyle")]
			public TreeViewImageGenerator.LineStyle LineStyle
			{
				get
				{
					return this._lineStyle;
				}
				set
				{
					this._lineStyle = value;
				}
			}

			// Token: 0x170008BB RID: 2235
			// (get) Token: 0x06002947 RID: 10567 RVA: 0x000F9EE2 File Offset: 0x000F80E2
			// (set) Token: 0x06002948 RID: 10568 RVA: 0x000F9EEA File Offset: 0x000F80EA
			[SRDescription("TreeViewImageGenerator_LineWidth")]
			public int LineWidth
			{
				get
				{
					return this._lineWidth;
				}
				set
				{
					if (value > 300)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					this._lineWidth = value;
				}
			}

			// Token: 0x170008BC RID: 2236
			// (get) Token: 0x06002949 RID: 10569 RVA: 0x000F9F06 File Offset: 0x000F8106
			// (set) Token: 0x0600294A RID: 10570 RVA: 0x000F9F0E File Offset: 0x000F810E
			[SRDescription("TreeViewImageGenerator_LineImageHeight")]
			public int Height
			{
				get
				{
					return this._height;
				}
				set
				{
					if (value > 300)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					this._height = value;
				}
			}

			// Token: 0x170008BD RID: 2237
			// (get) Token: 0x0600294B RID: 10571 RVA: 0x000F9F2A File Offset: 0x000F812A
			// (set) Token: 0x0600294C RID: 10572 RVA: 0x000F9F32 File Offset: 0x000F8132
			[DefaultValue(null)]
			[SRDescription("TreeViewImageGenerator_NoExpandImage")]
			public System.Drawing.Image NoExpandImage
			{
				get
				{
					return this._noExpandImage;
				}
				set
				{
					this._noExpandImage = value;
				}
			}

			// Token: 0x170008BE RID: 2238
			// (get) Token: 0x0600294D RID: 10573 RVA: 0x000F9F3B File Offset: 0x000F813B
			// (set) Token: 0x0600294E RID: 10574 RVA: 0x000F9F43 File Offset: 0x000F8143
			[DefaultValue(typeof(Color), "Magenta")]
			[SRDescription("TreeViewImageGenerator_TransparentColor")]
			public Color TransparentColor
			{
				get
				{
					return this._transparentColor;
				}
				set
				{
					this._transparentColor = value;
				}
			}

			// Token: 0x170008BF RID: 2239
			// (get) Token: 0x0600294F RID: 10575 RVA: 0x000F9F4C File Offset: 0x000F814C
			// (set) Token: 0x06002950 RID: 10576 RVA: 0x000F9F54 File Offset: 0x000F8154
			[SRDescription("TreeViewImageGenerator_LineImageWidth")]
			public int Width
			{
				get
				{
					return this._width;
				}
				set
				{
					if (value > 300)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					this._width = value;
				}
			}

			// Token: 0x04001D2E RID: 7470
			private int _height;

			// Token: 0x04001D2F RID: 7471
			private int _width;

			// Token: 0x04001D30 RID: 7472
			private int _lineWidth;

			// Token: 0x04001D31 RID: 7473
			private TreeViewImageGenerator.LineStyle _lineStyle;

			// Token: 0x04001D32 RID: 7474
			private Color _lineColor;

			// Token: 0x04001D33 RID: 7475
			private Color _transparentColor;

			// Token: 0x04001D34 RID: 7476
			private System.Drawing.Image _collapseImage;

			// Token: 0x04001D35 RID: 7477
			private System.Drawing.Image _expandImage;

			// Token: 0x04001D36 RID: 7478
			private System.Drawing.Image _noExpandImage;

			// Token: 0x04001D37 RID: 7479
			private const int MaxSize = 300;
		}

		// Token: 0x02000453 RID: 1107
		private class Octree
		{
			// Token: 0x06002951 RID: 10577 RVA: 0x000F9F70 File Offset: 0x000F8170
			public Octree(int maxColors, int numBits, Color transparentColor)
			{
				this._root = new TreeViewImageGenerator.OctreeNode();
				this._maxColors = maxColors;
				this._leafNodes = new ArrayList();
				this._numBits = numBits;
				this._transparentColor = transparentColor;
				if (!this._transparentColor.IsEmpty)
				{
					this._hasTransparency = true;
					this._maxColors--;
				}
				this._levels = new ArrayList[this._numBits - 1];
				for (int i = 0; i < this._levels.Length; i++)
				{
					this._levels[i] = new ArrayList();
				}
			}

			// Token: 0x06002952 RID: 10578 RVA: 0x000FA004 File Offset: 0x000F8204
			public void AddColor(Color c)
			{
				if (this._hasTransparency && this._transparentColor.R == c.R && this._transparentColor.G == c.G && this._transparentColor.B == c.B)
				{
					return;
				}
				int i = -1;
				if (this._leafNodes.Count >= this._maxColors)
				{
					TreeViewImageGenerator.OctreeNode octreeNode = null;
					for (int j = this._numBits - 2; j > 0; j--)
					{
						ArrayList arrayList = this._levels[j];
						if (arrayList.Count > 0)
						{
							i = j;
							int num = -1;
							for (int k = 0; k < arrayList.Count; k++)
							{
								TreeViewImageGenerator.OctreeNode octreeNode2 = (TreeViewImageGenerator.OctreeNode)arrayList[k];
								if (octreeNode2.PixelCount > num)
								{
									octreeNode = octreeNode2;
									num = octreeNode2.PixelCount;
								}
							}
							break;
						}
					}
					this.ReduceNode(octreeNode, i);
					this._leafNodes.Add(octreeNode);
				}
				TreeViewImageGenerator.OctreeNode octreeNode3 = this._root;
				i = 0;
				bool flag = false;
				while (i < this._numBits - 1)
				{
					int index = this.GetIndex(c, i);
					TreeViewImageGenerator.OctreeNode octreeNode4 = octreeNode3[index];
					if (octreeNode4 == null)
					{
						octreeNode4 = new TreeViewImageGenerator.OctreeNode();
						octreeNode3[index] = octreeNode4;
						flag = true;
						if (octreeNode3.NodeCount == 2)
						{
							this._levels[i].Add(octreeNode3);
						}
					}
					octreeNode3 = octreeNode4;
					octreeNode3.AddColor(c);
					if (octreeNode3.Reduced)
					{
						break;
					}
					i++;
				}
				if (flag)
				{
					this._leafNodes.Add(octreeNode3);
				}
			}

			// Token: 0x06002953 RID: 10579 RVA: 0x000FA178 File Offset: 0x000F8378
			public TreeViewImageGenerator.ColorIndexTable GetColorIndexTable()
			{
				Hashtable hashtable = new Hashtable();
				int maxColors = this._maxColors;
				Color[] array = new Color[maxColors];
				int num = 0;
				if (!this._transparentColor.IsEmpty)
				{
					hashtable[TreeViewImageGenerator.ColorIndexTable.GetColorKey(this._transparentColor)] = 0;
					array[0] = Color.FromArgb(0, this._transparentColor);
					num = 1;
				}
				foreach (object obj in this._leafNodes)
				{
					TreeViewImageGenerator.OctreeNode octreeNode = (TreeViewImageGenerator.OctreeNode)obj;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					foreach (object obj2 in octreeNode.Colors)
					{
						Color c = (Color)obj2;
						int colorKey = TreeViewImageGenerator.ColorIndexTable.GetColorKey(c);
						hashtable[colorKey] = num;
						num2 += (int)c.R;
						num3 += (int)c.G;
						num4 += (int)c.B;
					}
					int count = octreeNode.Colors.Count;
					array[num] = Color.FromArgb(255, num2 / count, num3 / count, num4 / count);
					num++;
				}
				return new TreeViewImageGenerator.ColorIndexTable(hashtable, array);
			}

			// Token: 0x06002954 RID: 10580 RVA: 0x000FA2F8 File Offset: 0x000F84F8
			private void ReduceNode(TreeViewImageGenerator.OctreeNode node, int depth)
			{
				ArrayList arrayList = null;
				if (depth < this._numBits - 2)
				{
					arrayList = this._levels[depth + 1];
				}
				for (int i = 0; i < 8; i++)
				{
					TreeViewImageGenerator.OctreeNode octreeNode = node[i];
					if (octreeNode != null)
					{
						if (depth < this._numBits - 2)
						{
							this.ReduceNode(octreeNode, depth + 1);
						}
						if (arrayList != null)
						{
							arrayList.Remove(octreeNode);
						}
						if (octreeNode.NodeCount == 0)
						{
							this._leafNodes.Remove(octreeNode);
						}
						node[i] = null;
					}
					this._levels[depth].Remove(node);
					node.Reduced = true;
				}
			}

			// Token: 0x06002955 RID: 10581 RVA: 0x000FA384 File Offset: 0x000F8584
			private int GetIndex(Color c, int depth)
			{
				int num = 7 - depth;
				return (c.R >> num & 1) << 2 | (c.G >> num & 1) << 1 | (c.B >> num & 1);
			}

			// Token: 0x04001D38 RID: 7480
			private TreeViewImageGenerator.OctreeNode _root;

			// Token: 0x04001D39 RID: 7481
			private ArrayList _leafNodes;

			// Token: 0x04001D3A RID: 7482
			private int _maxColors;

			// Token: 0x04001D3B RID: 7483
			private int _numBits;

			// Token: 0x04001D3C RID: 7484
			private Color _transparentColor;

			// Token: 0x04001D3D RID: 7485
			private bool _hasTransparency;

			// Token: 0x04001D3E RID: 7486
			private ArrayList[] _levels;
		}

		// Token: 0x02000454 RID: 1108
		private class OctreeNode
		{
			// Token: 0x06002956 RID: 10582 RVA: 0x000FA3C5 File Offset: 0x000F85C5
			public OctreeNode()
			{
				this._nodes = new TreeViewImageGenerator.OctreeNode[8];
				this._colors = new ArrayList();
				this._nodeCount = 0;
				this._reduced = false;
			}

			// Token: 0x170008C0 RID: 2240
			// (get) Token: 0x06002957 RID: 10583 RVA: 0x000FA3F2 File Offset: 0x000F85F2
			public ICollection Colors
			{
				get
				{
					return this._colors;
				}
			}

			// Token: 0x170008C1 RID: 2241
			// (get) Token: 0x06002958 RID: 10584 RVA: 0x000FA3FA File Offset: 0x000F85FA
			public int NodeCount
			{
				get
				{
					return this._nodeCount;
				}
			}

			// Token: 0x170008C2 RID: 2242
			// (get) Token: 0x06002959 RID: 10585 RVA: 0x000FA402 File Offset: 0x000F8602
			public int PixelCount
			{
				get
				{
					return this._colors.Count;
				}
			}

			// Token: 0x170008C3 RID: 2243
			// (get) Token: 0x0600295A RID: 10586 RVA: 0x000FA40F File Offset: 0x000F860F
			// (set) Token: 0x0600295B RID: 10587 RVA: 0x000FA417 File Offset: 0x000F8617
			public bool Reduced
			{
				get
				{
					return this._reduced;
				}
				set
				{
					this._reduced = value;
				}
			}

			// Token: 0x170008C4 RID: 2244
			public TreeViewImageGenerator.OctreeNode this[int index]
			{
				get
				{
					return this._nodes[index];
				}
				set
				{
					this._nodes[index] = value;
					if (this._nodes[index] == null)
					{
						this._nodeCount--;
						return;
					}
					this._nodeCount++;
				}
			}

			// Token: 0x0600295E RID: 10590 RVA: 0x000FA45C File Offset: 0x000F865C
			public void AddColor(Color c)
			{
				this._colors.Add(c);
			}

			// Token: 0x04001D3F RID: 7487
			private TreeViewImageGenerator.OctreeNode[] _nodes;

			// Token: 0x04001D40 RID: 7488
			private ArrayList _colors;

			// Token: 0x04001D41 RID: 7489
			private int _nodeCount;

			// Token: 0x04001D42 RID: 7490
			private bool _reduced;
		}

		// Token: 0x02000455 RID: 1109
		private class ColorIndexTable
		{
			// Token: 0x0600295F RID: 10591 RVA: 0x000FA470 File Offset: 0x000F8670
			internal ColorIndexTable(IDictionary table, Color[] colors)
			{
				this._table = table;
				this._colors = colors;
			}

			// Token: 0x170008C5 RID: 2245
			public int this[Color c]
			{
				get
				{
					object obj = this._table[TreeViewImageGenerator.ColorIndexTable.GetColorKey(c)];
					if (obj == null)
					{
						return 0;
					}
					return (int)obj;
				}
			}

			// Token: 0x06002961 RID: 10593 RVA: 0x000FA4B8 File Offset: 0x000F86B8
			public void CopyToColorPalette(ColorPalette palette)
			{
				for (int i = 0; i < this._colors.Length; i++)
				{
					palette.Entries[i] = this._colors[i];
				}
			}

			// Token: 0x06002962 RID: 10594 RVA: 0x000FA4F0 File Offset: 0x000F86F0
			internal static int GetColorKey(Color c)
			{
				return (int)(c.R & byte.MaxValue) << 16 | (int)(c.G & byte.MaxValue) << 8 | (int)(c.B & byte.MaxValue);
			}

			// Token: 0x04001D43 RID: 7491
			private IDictionary _table;

			// Token: 0x04001D44 RID: 7492
			private Color[] _colors;
		}
	}
}
