using System;
using System.Design;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x0200016A RID: 362
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class UnsettableComboBox : ComboBox
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x0005292E File Offset: 0x00050B2E
		public UnsettableComboBox()
		{
			this.notSetText = SR.GetString("UnsettableComboBox_NotSet");
			base.Items.Add(this.notSetText);
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00052958 File Offset: 0x00050B58
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x00052960 File Offset: 0x00050B60
		public string NotSetText
		{
			get
			{
				return this.notSetText;
			}
			set
			{
				this.notSetText = value;
				base.Items.RemoveAt(0);
				base.Items.Insert(0, this.notSetText);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00052987 File Offset: 0x00050B87
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x000529A6 File Offset: 0x00050BA6
		public override string Text
		{
			get
			{
				if (this.SelectedIndex == 0 || this.SelectedIndex == -1)
				{
					return string.Empty;
				}
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000529AF File Offset: 0x00050BAF
		public void AddItem(object item)
		{
			base.Items.Add(item);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x000529BE File Offset: 0x00050BBE
		public void EnsureNotSetItem()
		{
			if (base.Items.Count == 0)
			{
				base.Items.Add(this.notSetText);
			}
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x000529DF File Offset: 0x00050BDF
		public bool IsSet()
		{
			return this.SelectedIndex > 0;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000529EA File Offset: 0x00050BEA
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (this.SelectedIndex == 0)
			{
				this.internalChange = true;
				this.SelectedIndex = -1;
				this.internalChange = false;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00052A10 File Offset: 0x00050C10
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (!this.internalChange)
			{
				base.OnSelectedIndexChanged(e);
			}
		}

		// Token: 0x040007D1 RID: 2001
		private string notSetText;

		// Token: 0x040007D2 RID: 2002
		private bool internalChange;
	}
}
