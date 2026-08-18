using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.InputDialogControls;
using DevComponents.DotNetBar;
using DynamicScreens.Properties;
using UnivOleDb;

namespace DynamicScreens.AdminTools
{
	// Token: 0x0200002A RID: 42
	public partial class ScreenDetails : Form
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0001BFF3 File Offset: 0x0001AFF3
		public ScreenDetails()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0001C01C File Offset: 0x0001B01C
		public DataRow ScreenDr
		{
			get
			{
				return this.screenDr;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001C034 File Offset: 0x0001B034
		public ScreenDetails(DataRow screenDr, UnivDataAdapter da, ImageList imageListBig, ImageList imageListSmall)
		{
			this.da = da;
			this.screenDr = screenDr;
			this.imageListBig = imageListBig;
			this.imageListSmall = imageListSmall;
			this.screenNum = ((screenDr["screennum"] != DBNull.Value) ? ((int)screenDr["screennum"]) : 0);
			this.InitializeComponent();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0001C0B0 File Offset: 0x0001B0B0
		private void ToScreen()
		{
			DataRow dataRow = this.screenDr;
			int num = (int)dataRow["typecode"];
			ScreenType screenType = (ScreenType)num;
			this.lbl_screenType.Text = Enum.GetName(typeof(ScreenType), screenType);
			this.txt_screenCaption.Text = dataRow["description"].ToString();
			this.txt_screenCaptionFrench.Text = dataRow["shorttext"].ToString();
			this.txt_groupName.Text = dataRow["longdescription"].ToString();
			this.chk_enabled.Checked = (dataRow["isactive"] != DBNull.Value && Convert.ToBoolean(dataRow["isactive"]));
			this.chk_bottomless.Checked = (dataRow["bottomless"] != DBNull.Value && Convert.ToBoolean(dataRow["bottomless"]));
			this.chk_studentNameIsHidden.Checked = (dataRow["studentnamehidden"] != DBNull.Value && Convert.ToBoolean(dataRow["studentnamehidden"]));
			this.chk_showAsButton.Checked = (dataRow["showasbutton"] != DBNull.Value && Convert.ToBoolean(dataRow["showasbutton"]));
			this.txt_verticalControlPadding.Text = dataRow["verticalcontrolpad"].ToString();
			this.txt_colWidth.Text = dataRow["columnwidth"].ToString();
			this.txt_colPadding.Text = dataRow["columnpad"].ToString();
			this.txt_groupIds.Text = dataRow["groupids"].ToString();
			this.txt_filledOutCid.Text = dataRow["controlidtoactivate"].ToString();
			this.txt_studentNumberCaption.Text = dataRow["studentnumbercaption"].ToString();
			this.txt_studentNumAutoGenerateRule.Text = dataRow["studentnumberautogeneraterule"].ToString();
			int num2 = (dataRow["largeiconindex"] == DBNull.Value) ? -1 : ((int)dataRow["largeiconindex"]);
			int num3 = (dataRow["iconindex"] == DBNull.Value) ? -1 : ((int)dataRow["iconindex"]);
			if (num2 < this.imageListBig.Images.Count && num2 >= 0)
			{
				this.btn_bigImage.Image = this.imageListBig.Images[num2];
			}
			if (num3 < this.imageListSmall.Images.Count && num3 >= 0)
			{
				this.btn_littleImage.Image = this.imageListSmall.Images[num3];
			}
			this.btn_bigImage.Tag = num2;
			this.btn_littleImage.Tag = num3;
			this.Text = "Screen Details (screen no. " + this.screenNum.ToString() + ")";
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001C3E5 File Offset: 0x0001B3E5
		private void btn_close_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001C3EF File Offset: 0x0001B3EF
		private void btn_save_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001C3FC File Offset: 0x0001B3FC
		private void Save()
		{
			DataRow dataRow = this.screenDr;
			dataRow["description"] = this.txt_screenCaption.Text;
			dataRow["shorttext"] = this.txt_screenCaptionFrench.Text;
			dataRow["longdescription"] = this.txt_groupName.Text;
			dataRow["isactive"] = this.chk_enabled.Checked;
			dataRow["bottomless"] = this.chk_bottomless.Checked;
			dataRow["studentnamehidden"] = this.chk_studentNameIsHidden.Checked;
			dataRow["verticalcontrolpad"] = this.txt_verticalControlPadding.Text;
			dataRow["columnwidth"] = this.txt_colWidth.Text;
			dataRow["columnpad"] = this.txt_colPadding.Text;
			dataRow["groupids"] = this.txt_groupIds.Text;
			dataRow["controlidtoactivate"] = this.txt_filledOutCid.Text;
			dataRow["studentnumbercaption"] = this.txt_studentNumberCaption.Text;
			dataRow["studentnumberautogeneraterule"] = this.txt_studentNumAutoGenerateRule.Text;
			dataRow["showasbutton"] = this.chk_showAsButton.Checked;
			dataRow["largeiconindex"] = ((this.btn_bigImage.Tag != null) ? ((int)this.btn_bigImage.Tag) : -1);
			dataRow["iconindex"] = ((this.btn_littleImage.Tag != null) ? ((int)this.btn_littleImage.Tag) : -1);
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001C5D8 File Offset: 0x0001B5D8
		public static void WriteScreenChangesToDatabase(UnivDataAdapter da, DataRow dr)
		{
			da.SelectCommand.CommandText = "UPDATE screens SET description=@description,typecode=@typecode,bottomless=@bottomless,verticalcontrolpad=@verticalcontrolpad,columnwidth=@columnwidth,columnpad=@columnpad,dateadded=@dateadded,datemodified=@datemodified,isactive=@isactive,iconindex=@iconindex,largeiconindex=@largeiconindex,shorttext=@shorttext,studentnamenumeditable=@studentnamenumeditable,showasbutton=@showasbutton,fontname=@fontname,fontsize=@fontsize,groupids=@groupids,iswebscreen=@iswebscreen,longdescription=@longdescription,controlidtoactivate=@controlidtoactivate,studentnumbercaption=@studentnumbercaption,studentnumberautogeneraterule=@studentnumberautogeneraterule,studentnamehidden=@studentnamehidden WHERE screennum=@screennum";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@description", dr["description"]);
			da.SelectCommand.Parameters.Add("@typecode", dr["typecode"]);
			da.SelectCommand.Parameters.Add("@bottomless", dr["bottomless"]);
			da.SelectCommand.Parameters.Add("@verticalcontrolpad", dr["verticalcontrolpad"]);
			da.SelectCommand.Parameters.Add("@columnwidth", dr["columnwidth"]);
			da.SelectCommand.Parameters.Add("@columnpad", dr["columnpad"]);
			da.SelectCommand.Parameters.Add("@dateadded", dr["dateadded"]);
			da.SelectCommand.Parameters.Add("@datemodified", dr["datemodified"]);
			da.SelectCommand.Parameters.Add("@isactive", dr["isactive"]);
			da.SelectCommand.Parameters.Add("@iconindex", dr["iconindex"]);
			da.SelectCommand.Parameters.Add("@largeiconindex", dr["largeiconindex"]);
			da.SelectCommand.Parameters.Add("@shorttext", dr["shorttext"]);
			da.SelectCommand.Parameters.Add("@studentnamenumeditable", dr["studentnamenumeditable"]);
			da.SelectCommand.Parameters.Add("@showasbutton", dr["showasbutton"]);
			da.SelectCommand.Parameters.Add("@fontname", dr["fontname"]);
			da.SelectCommand.Parameters.Add("@fontsize", dr["fontsize"]);
			da.SelectCommand.Parameters.Add("@groupids", dr["groupids"]);
			da.SelectCommand.Parameters.Add("@iswebscreen", dr["iswebscreen"]);
			da.SelectCommand.Parameters.Add("@longdescription", dr["longdescription"]);
			da.SelectCommand.Parameters.Add("@controlidtoactivate", dr["controlidtoactivate"]);
			da.SelectCommand.Parameters.Add("@studentnumbercaption", dr["studentnumbercaption"]);
			da.SelectCommand.Parameters.Add("@studentnumberautogeneraterule", dr["studentnumberautogeneraterule"]);
			da.SelectCommand.Parameters.Add("@studentnamehidden", dr["studentnamehidden"]);
			da.SelectCommand.Parameters.Add("@screennum", dr["screennum"]);
			DataTable t = new DataTable();
			string text;
			da.Fill(t, out text);
			if (text != null && text.Length > 0)
			{
				MessageBox.Show(text);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001C94F File Offset: 0x0001B94F
		private void ScreenDetails_Load(object sender, EventArgs e)
		{
			this.ToScreen();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001C959 File Offset: 0x0001B959
		private void btn_bigImage_Click(object sender, EventArgs e)
		{
			this.LargeIconClick();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001C963 File Offset: 0x0001B963
		private void btn_littleImage_Click(object sender, EventArgs e)
		{
			this.SmallIconClick();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001C988 File Offset: 0x0001B988
		private void LargeIconClick()
		{
			Point position = Cursor.Position;
			IconPicker iconPicker = new IconPicker(this.imageListBig, null, true, position, null, new int[]
			{
				4,
				5,
				6,
				59,
				60,
				61
			});
			DialogResult dialogResult = iconPicker.ShowDialog(this);
			if (dialogResult == DialogResult.OK && iconPicker.selectedImageIndex >= 0)
			{
				this.btn_bigImage.Image = this.imageListBig.Images[iconPicker.selectedImageIndex];
				this.btn_bigImage.Tag = iconPicker.selectedImageIndex;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001CA14 File Offset: 0x0001BA14
		private void SmallIconClick()
		{
			Point position = Cursor.Position;
			IconPicker iconPicker = new IconPicker(this.imageListSmall, null, false, position, null, new int[0]);
			DialogResult dialogResult = iconPicker.ShowDialog(this);
			if (dialogResult == DialogResult.OK && iconPicker.selectedImageIndex >= 0)
			{
				this.btn_littleImage.Image = this.imageListSmall.Images[iconPicker.selectedImageIndex];
				this.btn_littleImage.Tag = iconPicker.selectedImageIndex;
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001CA95 File Offset: 0x0001BA95
		private void btn_colWidthPercent_third_Click(object sender, EventArgs e)
		{
			this.txt_colWidth.Text = "32";
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001CAA9 File Offset: 0x0001BAA9
		private void btn_colWidthPercent_half_Click(object sender, EventArgs e)
		{
			this.txt_colWidth.Text = "45";
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001CABD File Offset: 0x0001BABD
		private void btn_colWidthPercent_full_Click(object sender, EventArgs e)
		{
			this.txt_colWidth.Text = "95";
		}

		// Token: 0x0400019A RID: 410
		private UnivDataAdapter da;

		// Token: 0x0400019B RID: 411
		private int screenNum;

		// Token: 0x0400019C RID: 412
		private DataRow screenDr;

		// Token: 0x0400019D RID: 413
		private ImageList imageListBig = null;

		// Token: 0x0400019E RID: 414
		private ImageList imageListSmall = null;
	}
}
