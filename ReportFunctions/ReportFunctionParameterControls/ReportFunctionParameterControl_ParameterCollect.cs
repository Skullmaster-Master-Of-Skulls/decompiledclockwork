using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ClockWorkAPI;
using DynamicScreens;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x02000008 RID: 8
	public class ReportFunctionParameterControl_ParameterCollect : UserControl, iReportFunctionParameter
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002C18 File Offset: 0x00001C18
		public ReportFunctionParameterControl_ParameterCollect()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002C3C File Offset: 0x00001C3C
		public void Initialize(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.da = da;
			this.tripleDES = tripleDES;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002C4D File Offset: 0x00001C4D
		private void ReportFunctionParameterControl_ParameterCollect_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002C50 File Offset: 0x00001C50
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002C98 File Offset: 0x00001C98
		public string Parameter
		{
			get
			{
				string result;
				try
				{
					result = CompressionTP.Compress(this.parameters);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
					result = "";
				}
				return result;
			}
			set
			{
				try
				{
					this.parameters = CompressionTP.Decompress(value);
					this.RefreshPreview();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CE0 File Offset: 0x00001CE0
		private void RefreshPreview()
		{
			try
			{
				if (this.p_data.Controls.Count > 0)
				{
					this.p_data.Controls.Clear();
				}
				if (!string.IsNullOrEmpty(this.parameters))
				{
					DataSet dataSet = new DataSet();
					StringReader input = new StringReader(this.parameters);
					XmlReader reader = XmlReader.Create(input);
					dataSet.ReadXml(reader, XmlReadMode.ReadSchema);
					if (dataSet.Tables.Count > 0)
					{
						DataTable controlListTable = dataSet.Tables[0];
						DataSet dataSet2 = new DataSet();
						ScreenInfo screenInfo = new ScreenInfo(0, this.p_data, true, 0, 350, 0, this.Font, -1, "", false, false, Color.Transparent, Color.Transparent);
						screenInfo.WidthPercent = 0.95;
						DynamicScreen.TranslateControls(this.da, this.tripleDES, ref this.p_data, screenInfo, controlListTable, ref dataSet2, null, new DataSet(), new ArrayList(), 1);
						this.p_data.AutoScroll = true;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002E20 File Offset: 0x00001E20
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002E33 File Offset: 0x00001E33
		public bool AllowTab
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E38 File Offset: 0x00001E38
		private void button1_Click(object sender, EventArgs e)
		{
			ScreenEditor screenEditor = new ScreenEditor(this.da, 0, this.tripleDES, null);
			screenEditor.XmlDefinition = this.parameters;
			DialogResult dialogResult = screenEditor.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				this.parameters = screenEditor.XmlDefinition;
				this.RefreshPreview();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E90 File Offset: 0x00001E90
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002EC8 File Offset: 0x00001EC8
		private void InitializeComponent()
		{
			this.btn_editP = new Button();
			this.p_data = new Panel();
			base.SuspendLayout();
			this.btn_editP.Location = new Point(3, 3);
			this.btn_editP.Name = "btn_editP";
			this.btn_editP.Size = new Size(75, 23);
			this.btn_editP.TabIndex = 0;
			this.btn_editP.Text = "&Edit Form";
			this.btn_editP.UseVisualStyleBackColor = true;
			this.btn_editP.Click += this.button1_Click;
			this.p_data.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.p_data.BackColor = SystemColors.ControlDark;
			this.p_data.BorderStyle = BorderStyle.Fixed3D;
			this.p_data.Location = new Point(4, 33);
			this.p_data.Name = "p_data";
			this.p_data.Size = new Size(266, 91);
			this.p_data.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.p_data);
			base.Controls.Add(this.btn_editP);
			base.Name = "ReportFunctionParameterControl_ParameterCollect";
			base.Size = new Size(273, 127);
			base.Load += this.ReportFunctionParameterControl_ParameterCollect_Load;
			base.ResumeLayout(false);
		}

		// Token: 0x0400009D RID: 157
		private UnivDataAdapter da;

		// Token: 0x0400009E RID: 158
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x0400009F RID: 159
		private string parameters = "";

		// Token: 0x040000A0 RID: 160
		private IContainer components = null;

		// Token: 0x040000A1 RID: 161
		private Button btn_editP;

		// Token: 0x040000A2 RID: 162
		private Panel p_data;
	}
}
