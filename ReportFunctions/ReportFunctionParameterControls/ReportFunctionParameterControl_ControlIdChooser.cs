using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DynamicScreens.CustomControls;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x02000051 RID: 81
	public class ReportFunctionParameterControl_ControlIdChooser : UserControl, iReportFunctionParameter
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0004F896 File Offset: 0x0004E896
		public ReportFunctionParameterControl_ControlIdChooser()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0004F8B0 File Offset: 0x0004E8B0
		public void Initialize(UnivDataAdapter da, TripleDESEncryptionClass tripleDES)
		{
			this.dynamicControlChooser1.Initialize(da, tripleDES, true, "", new int[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6
			});
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0004F8F4 File Offset: 0x0004E8F4
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0004F911 File Offset: 0x0004E911
		public string Parameter
		{
			get
			{
				return this.dynamicControlChooser1.GetSelectedControlIdsStringCommaSeparated();
			}
			set
			{
				this.dynamicControlChooser1.SetSelectedControlIdsStringCommaSeparated(value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0004F924 File Offset: 0x0004E924
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0004F937 File Offset: 0x0004E937
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

		// Token: 0x060004A4 RID: 1188 RVA: 0x0004F93C File Offset: 0x0004E93C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0004F974 File Offset: 0x0004E974
		private void InitializeComponent()
		{
			this.dynamicControlChooser1 = new DynamicControlChooser();
			base.SuspendLayout();
			this.dynamicControlChooser1.Dock = DockStyle.Fill;
			this.dynamicControlChooser1.Location = new Point(0, 0);
			this.dynamicControlChooser1.Name = "dynamicControlChooser1";
			this.dynamicControlChooser1.Size = new Size(150, 150);
			this.dynamicControlChooser1.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.dynamicControlChooser1);
			base.Name = "ReportFunctionParameterControl_ControlIdChooser";
			base.ResumeLayout(false);
		}

		// Token: 0x04000286 RID: 646
		private IContainer components = null;

		// Token: 0x04000287 RID: 647
		private DynamicControlChooser dynamicControlChooser1;
	}
}
