using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DynamicScreens.CustomControls;
using DynamicScreens.Properties;
using EncryptionClassLibrary;
using UnivOleDb;

namespace DynamicScreens.Dialogs
{
	// Token: 0x0200001A RID: 26
	public partial class DynamicControlChooserForm : Form
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00017354 File Offset: 0x00016354
		public DynamicControlChooserForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00017374 File Offset: 0x00016374
		public DynamicControlChooserForm(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, bool showAccommodations)
		{
			this.type = -1;
			this.da = da;
			this.tripleDES = tripleDES;
			this.showAccommodations = showAccommodations;
			this.InitializeComponent();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000173B0 File Offset: 0x000163B0
		public DynamicControlChooserForm(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int type)
		{
			this.type = type;
			this.da = da;
			this.tripleDES = tripleDES;
			this.InitializeComponent();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000173E5 File Offset: 0x000163E5
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000173EF File Offset: 0x000163EF
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00017404 File Offset: 0x00016404
		public string GetSelectedCidsString()
		{
			return this.dynamicControlChooser1.GetSelectedControlIdsStringCommaSeparated();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00017424 File Offset: 0x00016424
		public List<string> GetSelectedCidCommaDescriptions()
		{
			return this.dynamicControlChooser1.GetSelectedCidCommaDescriptions();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00017444 File Offset: 0x00016444
		private void DynamicControlChooserForm_Load(object sender, EventArgs e)
		{
			this.dynamicControlChooser1.Initialize(this.da, this.tripleDES, true, "", this.showAccommodations, new int[]
			{
				this.type
			});
		}

		// Token: 0x04000137 RID: 311
		private bool showAccommodations = false;

		// Token: 0x04000138 RID: 312
		private int type;

		// Token: 0x04000139 RID: 313
		private UnivDataAdapter da;

		// Token: 0x0400013A RID: 314
		private TripleDESEncryptionClass tripleDES;
	}
}
