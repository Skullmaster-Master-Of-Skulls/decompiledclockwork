using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using UnivOleDb;

namespace AutoComboBox.InputDialogControls
{
	// Token: 0x0200007F RID: 127
	public partial class IconPicker : Form
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x00029A9A File Offset: 0x00028A9A
		public IconPicker()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00029AC8 File Offset: 0x00028AC8
		public IconPicker(ImageList _ImageList, int[] _ImageIndices, bool _AddOverImageIndicesAutomatically, Point _StartupScreenLocation, DataTable _IconInfo, int[] indicesToIgnore)
		{
			this.InitializeComponent();
			this.Init(_ImageList, _ImageIndices, _AddOverImageIndicesAutomatically, _StartupScreenLocation, _IconInfo, indicesToIgnore);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00029B08 File Offset: 0x00028B08
		public IconPicker(UnivDataAdapter da)
		{
			this.InitializeComponent();
			da.SelectCommand.CommandText = "SELECT appointmenticoninfoid,iconindex,icontext,iconletteridentifier FROM appointmenticoninfo";
			DataTable t = new DataTable("iconinfo");
			da.Fill(t);
			this.Init(this.iconsImageList, null, false, new Point(300, 200), t, null);
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00029B88 File Offset: 0x00028B88
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00029BA0 File Offset: 0x00028BA0
		public bool AllowEditing
		{
			get
			{
				return this.allowEditing;
			}
			set
			{
				this.allowEditing = value;
				this.btn_save.Visible = this.allowEditing;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00029BBC File Offset: 0x00028BBC
		private bool ArrayContains(int[] array, int item)
		{
			bool result;
			if (array == null)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == item)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00029C04 File Offset: 0x00028C04
		private void Init(ImageList _ImageList, int[] _ImageIndices, bool _AddOverImageIndicesAutomatically, Point _StartupScreenLocation, DataTable _IconInfo, int[] indicesToIgnore)
		{
			int num = (indicesToIgnore == null) ? 0 : indicesToIgnore.Length;
			this.iconInfo = _IconInfo;
			this.imageList = _ImageList;
			this.toolbar.ImageList = this.imageList;
			this.addOverImageIndicesAutomatically = _AddOverImageIndicesAutomatically;
			if (_ImageIndices == null)
			{
				if (this.addOverImageIndicesAutomatically)
				{
					int num2 = (this.imageList.Images.Count - num) / 2;
					this.imageIndices = new int[num2];
					int i = 0;
					int num3 = 0;
					while (i < this.imageList.Images.Count)
					{
						if (this.ArrayContains(indicesToIgnore, i))
						{
							i++;
						}
						else if (i < this.imageList.Images.Count)
						{
							this.imageIndices[num3++] = i;
							i += 2;
						}
						else
						{
							i += 2;
						}
					}
				}
				else
				{
					int num2 = this.imageList.Images.Count - num;
					this.imageIndices = new int[num2];
					int num3 = 0;
					for (int j = 0; j < this.imageList.Images.Count; j++)
					{
						if (!this.ArrayContains(indicesToIgnore, j))
						{
							this.imageIndices[num3++] = j;
						}
					}
				}
			}
			else
			{
				this.imageIndices = _ImageIndices;
			}
			this.startupScreenLocation = _StartupScreenLocation;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00029D78 File Offset: 0x00028D78
		private void IconPicker_Load(object sender, EventArgs e)
		{
			int num = 10;
			int num2 = Convert.ToInt32(this.imageList.Images.Count / num) + 2;
			base.Width = num * (this.imageList.ImageSize.Width + 10);
			base.Height = num2 * (this.imageList.ImageSize.Height + 10);
			int num3 = this.startupScreenLocation.X - base.Width;
			if (num3 < 0)
			{
				num3 = 0;
			}
			this.startupScreenLocation.X = num3;
			base.Location = this.startupScreenLocation;
			foreach (int num4 in this.imageIndices)
			{
				ToolStripButton toolStripButton = new ToolStripButton("", this.toolbar.ImageList.Images[num4]);
				toolStripButton.CheckOnClick = true;
				toolStripButton.Click += this.btn_Click;
				toolStripButton.MouseMove += this.btn_MouseMove;
				toolStripButton.Tag = num4;
				this.toolbar.Items.Add(toolStripButton);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00029EC4 File Offset: 0x00028EC4
		private void btn_Click(object sender, EventArgs e)
		{
			ToolStripButton toolStripButton = (ToolStripButton)sender;
			this.selectedImageIndex = (int)toolStripButton.Tag;
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00029EFC File Offset: 0x00028EFC
		public Image GetIconImage(int iconNum)
		{
			Image result;
			if (iconNum >= 0)
			{
				result = this.iconsImageList.Images[iconNum];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00029F2C File Offset: 0x00028F2C
		private void btn_MouseMove(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00029F2F File Offset: 0x00028F2F
		private void btn_save_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00029F41 File Offset: 0x00028F41
		private void btn_cancel2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x04000430 RID: 1072
		private ImageList imageList;

		// Token: 0x04000431 RID: 1073
		private int[] imageIndices;

		// Token: 0x04000432 RID: 1074
		private Button btn_cancel;

		// Token: 0x04000433 RID: 1075
		private bool addOverImageIndicesAutomatically;

		// Token: 0x04000434 RID: 1076
		private Point startupScreenLocation;

		// Token: 0x04000435 RID: 1077
		private DataTable iconInfo;

		// Token: 0x04000436 RID: 1078
		private bool allowEditing = false;

		// Token: 0x04000437 RID: 1079
		public int selectedImageIndex = -1;

		// Token: 0x04000438 RID: 1080
		private int lastIndexMouseOver = -1;
	}
}
