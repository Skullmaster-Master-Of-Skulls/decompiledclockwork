using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000312 RID: 786
	internal partial class MaskDesignerDialog : Form
	{
		// Token: 0x06001F00 RID: 7936 RVA: 0x000B9EC0 File Offset: 0x000B80C0
		public MaskDesignerDialog(MaskedTextBox instance, IHelpService helpService)
		{
			if (instance == null)
			{
				this.maskedTextBox = new MaskedTextBox();
			}
			else
			{
				this.maskedTextBox = MaskedTextBoxDesigner.GetDesignMaskedTextBox(instance);
			}
			this.helpService = helpService;
			this.InitializeComponent();
			DesignerUtils.ApplyListViewThemeStyles(this.listViewCannedMasks);
			base.SuspendLayout();
			this.txtBoxMask.Text = this.maskedTextBox.Mask;
			this.AddDefaultMaskDescriptors(this.maskedTextBox.Culture);
			this.maskDescriptionHeader.Text = SR.GetString("MaskDesignerDialogMaskDescription");
			this.maskDescriptionHeader.Width = this.listViewCannedMasks.Width / 3;
			this.dataFormatHeader.Text = SR.GetString("MaskDesignerDialogDataFormat");
			this.dataFormatHeader.Width = this.listViewCannedMasks.Width / 3;
			this.validatingTypeHeader.Text = SR.GetString("MaskDesignerDialogValidatingType");
			this.validatingTypeHeader.Width = this.listViewCannedMasks.Width / 3 - SystemInformation.VerticalScrollBarWidth - 4;
			base.ResumeLayout(false);
			this.HookEvents();
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000B9FE4 File Offset: 0x000B81E4
		private void HookEvents()
		{
			this.listViewCannedMasks.SelectedIndexChanged += this.listViewCannedMasks_SelectedIndexChanged;
			this.listViewCannedMasks.ColumnClick += this.listViewCannedMasks_ColumnClick;
			this.listViewCannedMasks.Enter += this.listViewCannedMasks_Enter;
			this.btnOK.Click += this.btnOK_Click;
			this.txtBoxMask.TextChanged += this.txtBoxMask_TextChanged;
			this.txtBoxMask.Validating += this.txtBoxMask_Validating;
			this.maskedTextBox.KeyDown += this.maskedTextBox_KeyDown;
			this.maskedTextBox.MaskInputRejected += this.maskedTextBox_MaskInputRejected;
			base.Load += this.MaskDesignerDialog_Load;
			base.HelpButtonClicked += this.MaskDesignerDialog_HelpButtonClicked;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x000BA8BE File Offset: 0x000B8ABE
		public string Mask
		{
			get
			{
				return this.maskedTextBox.Mask;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x000BA8CB File Offset: 0x000B8ACB
		public Type ValidatingType
		{
			get
			{
				return this.mtpValidatingType;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x000BA8D3 File Offset: 0x000B8AD3
		public IEnumerator MaskDescriptors
		{
			get
			{
				return this.maskDescriptors.GetEnumerator();
			}
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000BA8E8 File Offset: 0x000B8AE8
		private void AddDefaultMaskDescriptors(CultureInfo culture)
		{
			this.customMaskDescriptor = new MaskDescriptorTemplate(null, SR.GetString("MaskDesignerDialogCustomEntry"), null, null, null, true);
			List<MaskDescriptor> localizedMaskDescriptors = MaskDescriptorTemplate.GetLocalizedMaskDescriptors(culture);
			this.InsertMaskDescriptor(0, this.customMaskDescriptor, false);
			foreach (MaskDescriptor maskDescriptor in localizedMaskDescriptors)
			{
				this.InsertMaskDescriptor(0, maskDescriptor);
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000BA968 File Offset: 0x000B8B68
		private bool ContainsMaskDescriptor(MaskDescriptor maskDescriptor)
		{
			foreach (MaskDescriptor maskDescriptor2 in this.maskDescriptors)
			{
				if (maskDescriptor.Equals(maskDescriptor2) || maskDescriptor.Name.Trim() == maskDescriptor2.Name.Trim())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000BA9E4 File Offset: 0x000B8BE4
		public void DiscoverMaskDescriptors(ITypeDiscoveryService discoveryService)
		{
			if (discoveryService != null)
			{
				ICollection collection = DesignerUtils.FilterGenericTypes(discoveryService.GetTypes(typeof(MaskDescriptor), false));
				foreach (object obj in collection)
				{
					Type type = (Type)obj;
					if (!type.IsAbstract && type.IsPublic)
					{
						try
						{
							MaskDescriptor maskDescriptor = (MaskDescriptor)Activator.CreateInstance(type);
							this.InsertMaskDescriptor(0, maskDescriptor);
						}
						catch (Exception ex)
						{
							if (ClientUtils.IsCriticalException(ex))
							{
								throw;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000BAA94 File Offset: 0x000B8C94
		private int GetMaskDescriptorIndex(MaskDescriptor maskDescriptor)
		{
			for (int i = 0; i < this.maskDescriptors.Count; i++)
			{
				MaskDescriptor maskDescriptor2 = this.maskDescriptors[i];
				if (maskDescriptor2 == maskDescriptor)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000BAACC File Offset: 0x000B8CCC
		private void SelectMtbMaskDescriptor()
		{
			int num = -1;
			if (!string.IsNullOrEmpty(this.maskedTextBox.Mask))
			{
				for (int i = 0; i < this.maskDescriptors.Count; i++)
				{
					MaskDescriptor maskDescriptor = this.maskDescriptors[i];
					if (maskDescriptor.Mask == this.maskedTextBox.Mask && maskDescriptor.ValidatingType == this.maskedTextBox.ValidatingType)
					{
						num = i;
						break;
					}
				}
			}
			if (num == -1)
			{
				num = this.GetMaskDescriptorIndex(this.customMaskDescriptor);
			}
			if (num != -1)
			{
				this.SetSelectedMaskDescriptor(num);
			}
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000BAB64 File Offset: 0x000B8D64
		private void SetSelectedMaskDescriptor(MaskDescriptor maskDex)
		{
			int maskDescriptorIndex = this.GetMaskDescriptorIndex(maskDex);
			this.SetSelectedMaskDescriptor(maskDescriptorIndex);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000BAB80 File Offset: 0x000B8D80
		private void SetSelectedMaskDescriptor(int maskDexIndex)
		{
			if (maskDexIndex >= 0 && this.listViewCannedMasks.Items.Count > maskDexIndex)
			{
				this.listViewCannedMasks.Items[maskDexIndex].Selected = true;
				this.listViewCannedMasks.FocusedItem = this.listViewCannedMasks.Items[maskDexIndex];
				this.listViewCannedMasks.EnsureVisible(maskDexIndex);
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000BABE4 File Offset: 0x000B8DE4
		private void UpdateSortedListView(MaskDescriptorComparer.SortType sortType)
		{
			if (!this.listViewCannedMasks.IsHandleCreated)
			{
				return;
			}
			MaskDescriptor maskDescriptor = null;
			if (this.listViewCannedMasks.SelectedItems.Count > 0)
			{
				int index = this.listViewCannedMasks.SelectedIndices[0];
				maskDescriptor = this.maskDescriptors[index];
			}
			this.maskDescriptors.RemoveAt(this.maskDescriptors.Count - 1);
			this.maskDescriptors.Sort(new MaskDescriptorComparer(sortType, this.listViewSortOrder));
			UnsafeNativeMethods.SendMessage(this.listViewCannedMasks.Handle, 11, false, 0);
			try
			{
				this.listViewCannedMasks.Items.Clear();
				string @string = SR.GetString("MaskDescriptorValidatingTypeNone");
				foreach (MaskDescriptor maskDescriptor2 in this.maskDescriptors)
				{
					string text = (maskDescriptor2.ValidatingType != null) ? maskDescriptor2.ValidatingType.Name : @string;
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(maskDescriptor2.Mask, maskDescriptor2.Culture);
					bool flag = maskedTextProvider.Add(maskDescriptor2.Sample);
					string text2 = maskedTextProvider.ToString(false, true);
					this.listViewCannedMasks.Items.Add(new ListViewItem(new string[]
					{
						maskDescriptor2.Name,
						text2,
						text
					}));
				}
				this.maskDescriptors.Add(this.customMaskDescriptor);
				this.listViewCannedMasks.Items.Add(new ListViewItem(new string[]
				{
					this.customMaskDescriptor.Name,
					"",
					@string
				}));
				if (maskDescriptor != null)
				{
					this.SetSelectedMaskDescriptor(maskDescriptor);
				}
			}
			finally
			{
				UnsafeNativeMethods.SendMessage(this.listViewCannedMasks.Handle, 11, true, 0);
				this.listViewCannedMasks.Invalidate();
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000BADF0 File Offset: 0x000B8FF0
		private void InsertMaskDescriptor(int index, MaskDescriptor maskDescriptor)
		{
			this.InsertMaskDescriptor(index, maskDescriptor, true);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000BADFC File Offset: 0x000B8FFC
		private void InsertMaskDescriptor(int index, MaskDescriptor maskDescriptor, bool validateDescriptor)
		{
			string text;
			if (validateDescriptor && !MaskDescriptor.IsValidMaskDescriptor(maskDescriptor, out text))
			{
				return;
			}
			if (!this.ContainsMaskDescriptor(maskDescriptor))
			{
				this.maskDescriptors.Insert(index, maskDescriptor);
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000BAE30 File Offset: 0x000B9030
		private void RemoveMaskDescriptor(MaskDescriptor maskDescriptor)
		{
			int maskDescriptorIndex = this.GetMaskDescriptorIndex(maskDescriptor);
			if (maskDescriptorIndex >= 0)
			{
				this.maskDescriptors.RemoveAt(maskDescriptorIndex);
				return;
			}
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000BAE58 File Offset: 0x000B9058
		private void listViewCannedMasks_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			switch (this.listViewSortOrder)
			{
			case SortOrder.None:
			case SortOrder.Descending:
				this.listViewSortOrder = SortOrder.Ascending;
				break;
			case SortOrder.Ascending:
				this.listViewSortOrder = SortOrder.Descending;
				break;
			}
			this.UpdateSortedListView((MaskDescriptorComparer.SortType)e.Column);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000BAE9C File Offset: 0x000B909C
		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.checkBoxUseValidatingType.Checked)
			{
				this.mtpValidatingType = this.maskedTextBox.ValidatingType;
				return;
			}
			this.mtpValidatingType = null;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000BAEC4 File Offset: 0x000B90C4
		private void listViewCannedMasks_Enter(object sender, EventArgs e)
		{
			if (this.listViewCannedMasks.FocusedItem == null && this.listViewCannedMasks.Items.Count > 0)
			{
				this.listViewCannedMasks.Items[0].Focused = true;
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000BAF00 File Offset: 0x000B9100
		private void listViewCannedMasks_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listViewCannedMasks.SelectedItems.Count != 0)
			{
				int index = this.listViewCannedMasks.SelectedIndices[0];
				MaskDescriptor maskDescriptor = this.maskDescriptors[index];
				if (maskDescriptor != this.customMaskDescriptor)
				{
					this.txtBoxMask.Text = maskDescriptor.Mask;
					this.maskedTextBox.Mask = maskDescriptor.Mask;
					this.maskedTextBox.ValidatingType = maskDescriptor.ValidatingType;
					return;
				}
				this.maskedTextBox.ValidatingType = null;
			}
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000BAF87 File Offset: 0x000B9187
		private void MaskDesignerDialog_Load(object sender, EventArgs e)
		{
			this.UpdateSortedListView(MaskDescriptorComparer.SortType.ByName);
			this.SelectMtbMaskDescriptor();
			this.btnCancel.Select();
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000BAFA1 File Offset: 0x000B91A1
		private void maskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{
			this.errorProvider.SetError(this.maskedTextBox, MaskedTextBoxDesigner.GetMaskInputRejectedErrorMessage(e));
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000BAFBA File Offset: 0x000B91BA
		private string HelpTopic
		{
			get
			{
				return "net.ComponentModel.MaskPropertyEditor";
			}
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000BAFC1 File Offset: 0x000B91C1
		private void ShowHelp()
		{
			if (this.helpService != null)
			{
				this.helpService.ShowHelpFromKeyword(this.HelpTopic);
			}
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000BAFDC File Offset: 0x000B91DC
		private void MaskDesignerDialog_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.ShowHelp();
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000BAFEB File Offset: 0x000B91EB
		private void maskedTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			this.errorProvider.Clear();
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000BAFF8 File Offset: 0x000B91F8
		private void txtBoxMask_Validating(object sender, CancelEventArgs e)
		{
			try
			{
				this.maskedTextBox.Mask = this.txtBoxMask.Text;
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000BB030 File Offset: 0x000B9230
		private void txtBoxMask_TextChanged(object sender, EventArgs e)
		{
			MaskDescriptor maskDescriptor = null;
			if (this.listViewCannedMasks.SelectedItems.Count != 0)
			{
				int index = this.listViewCannedMasks.SelectedIndices[0];
				maskDescriptor = this.maskDescriptors[index];
			}
			if (maskDescriptor == null || (maskDescriptor != this.customMaskDescriptor && maskDescriptor.Mask != this.txtBoxMask.Text))
			{
				this.SetSelectedMaskDescriptor(this.customMaskDescriptor);
			}
		}

		// Token: 0x040017F9 RID: 6137
		private List<MaskDescriptor> maskDescriptors = new List<MaskDescriptor>();

		// Token: 0x040017FA RID: 6138
		private MaskDescriptor customMaskDescriptor;

		// Token: 0x040017FB RID: 6139
		private SortOrder listViewSortOrder = SortOrder.Ascending;

		// Token: 0x040017FC RID: 6140
		private Type mtpValidatingType;

		// Token: 0x040017FE RID: 6142
		private IHelpService helpService;
	}
}
