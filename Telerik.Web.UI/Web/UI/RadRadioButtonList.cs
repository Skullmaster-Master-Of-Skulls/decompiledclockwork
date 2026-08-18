using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;

namespace Telerik.Web.UI
{
	// Token: 0x0200077B RID: 1915
	[EmbeddedSkin("Button")]
	[TelerikToolboxCategory("Navigation")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Button", "Default")]
	[ToolboxData("<{0}:RadRadioButtonList Runat=server></{0}:RadRadioButtonList>")]
	[ClientScriptResource("Telerik.Web.UI.RadRadioButtonList", "Telerik.Web.UI.RadioButtonList.RadRadioButtonListScripts.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[Designer("Telerik.Web.Design.RadRadioButtonListDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadRadioButtonList), "Telerik.Web.UI.Button.png")]
	public class RadRadioButtonList : RadButtonList
	{
		// Token: 0x0600439A RID: 17306 RVA: 0x000D3846 File Offset: 0x000D1A46
		protected override CheckableButton CreateCheckableButton()
		{
			return new RadRadioButton();
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x000D384D File Offset: 0x000D1A4D
		protected override void SetCheckableButtonProperties(CheckableButton checkableButton, ButtonListItem item)
		{
			base.SetCheckableButtonProperties(checkableButton, item);
			checkableButton.Checked = new bool?(base.Items.IndexOf(item) == base.SelectedIndex && item.Selected);
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x000D387F File Offset: 0x000D1A7F
		protected override void SetSelectedItem(ButtonListItem item)
		{
			this.ClearItemsSelection();
			base.SetSelectedItem(item);
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x000D3890 File Offset: 0x000D1A90
		internal void ClearItemsSelection()
		{
			foreach (object obj in base.Items)
			{
				ButtonListItem buttonListItem = (ButtonListItem)obj;
				buttonListItem.Selected = false;
			}
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x000D38EC File Offset: 0x000D1AEC
		protected override void SetSelectedIndex(int value)
		{
			if ((base.Items.Count != 0 && value < base.Items.Count) || value == -1)
			{
				this.ClearItemsSelection();
				if (value >= 0)
				{
					base.Items[value].Selected = true;
				}
			}
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x000D392C File Offset: 0x000D1B2C
		protected override void SetSelectedValue(string value)
		{
			ButtonListItem item = base.GetItem(value);
			if (item != null)
			{
				this.ClearItemsSelection();
				item.Selected = true;
			}
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x000D3951 File Offset: 0x000D1B51
		internal override void RaiseItemCreated(ButtonListItem item)
		{
			base.RaiseItemCreated(item);
			item.ItemSelected += this.Item_ItemSelected;
			if (item.Selected)
			{
				this.ClearItemsSelection();
				item.Selected = true;
			}
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x000D3981 File Offset: 0x000D1B81
		private void Item_ItemSelected(object sender, EventArgs e)
		{
			this.ClearItemsSelection();
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x000D3989 File Offset: 0x000D1B89
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000D3992 File Offset: 0x000D1B92
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}
	}
}
