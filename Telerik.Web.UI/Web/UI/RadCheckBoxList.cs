using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;

namespace Telerik.Web.UI
{
	// Token: 0x020000AF RID: 175
	[Designer("Telerik.Web.Design.RadCheckBoxListDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("Button")]
	[EmbeddedSkin("Button", "Default")]
	[ToolboxBitmap(typeof(RadCheckBoxList), "Telerik.Web.UI.Button.png")]
	[ClientScriptResource("Telerik.Web.UI.RadCheckBoxList", "Telerik.Web.UI.CheckBoxList.RadCheckBoxListScripts.js")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxData("<{0}:RadCheckBoxList Runat=server></{0}:RadCheckBoxList>")]
	[TelerikToolboxCategory("Navigation")]
	[RequiredScript(typeof(jQueryPlugins))]
	public class RadCheckBoxList : RadButtonList
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x0001BB98 File Offset: 0x00019D98
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = base.LoadPostData(postDataKey, postCollection);
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			CheckBoxListClientState checkBoxListClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				checkBoxListClientState = javaScriptSerializer.Deserialize<CheckBoxListClientState>(text);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (checkBoxListClientState == null)
			{
				return false;
			}
			if (!this.SelectedIndices.ToList<int>().SequenceEqual(checkBoxListClientState.SelectedIndices.ToList<int>()))
			{
				this.SelectedIndices = checkBoxListClientState.SelectedIndices;
				result = (this.isSelectedIndexChanged = true);
			}
			return result;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001BC38 File Offset: 0x00019E38
		protected override CheckableButton CreateCheckableButton()
		{
			return new RadCheckBox();
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001BC3F File Offset: 0x00019E3F
		protected override void SetCheckableButtonProperties(CheckableButton checkableButton, ButtonListItem item)
		{
			base.SetCheckableButtonProperties(checkableButton, item);
			checkableButton.Checked = new bool?(item.Selected);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001BC5C File Offset: 0x00019E5C
		private int[] GetSelectedIndices()
		{
			List<int> list = new List<int>();
			foreach (object obj in base.Items)
			{
				ButtonListItem buttonListItem = (ButtonListItem)obj;
				if (buttonListItem.Selected)
				{
					list.Add(base.Items.IndexOf(buttonListItem));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001BCD4 File Offset: 0x00019ED4
		private void SetSelectedIndices(int[] indices)
		{
			foreach (object obj in base.Items)
			{
				ButtonListItem buttonListItem = (ButtonListItem)obj;
				buttonListItem.Selected = indices.Contains(base.Items.IndexOf(buttonListItem));
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001BD40 File Offset: 0x00019F40
		private List<ButtonListItem> GetSelectedItems()
		{
			List<ButtonListItem> list = new List<ButtonListItem>();
			foreach (object obj in base.Items)
			{
				ButtonListItem buttonListItem = (ButtonListItem)obj;
				if (buttonListItem.Selected)
				{
					list.Add(buttonListItem);
				}
			}
			return list;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		private string[] GetSelectedValues()
		{
			List<string> list = new List<string>();
			foreach (object obj in base.Items)
			{
				ButtonListItem buttonListItem = (ButtonListItem)obj;
				if (buttonListItem.Selected)
				{
					list.Add(buttonListItem.Value);
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001BE1C File Offset: 0x0001A01C
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001BE24 File Offset: 0x0001A024
		[ClientPropertyName("selectedIndices")]
		[Description("Gets a collection of all selected checkboxes by indices. ")]
		[ClientControlProperty]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("Behavior")]
		public int[] SelectedIndices
		{
			get
			{
				return this.GetSelectedIndices();
			}
			private set
			{
				this.SetSelectedIndices(value);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001BE2D File Offset: 0x0001A02D
		[Category("Behavior")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Description("Gets a collection of all selected checkbox items.")]
		[Browsable(false)]
		public IList<ButtonListItem> SelectedItems
		{
			get
			{
				return this.GetSelectedItems();
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001BE35 File Offset: 0x0001A035
		[Description("Gets a collection of all selected checkboxes by values.")]
		[Category("Behavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public string[] SelectedValues
		{
			get
			{
				return this.GetSelectedValues();
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001BE3D File Offset: 0x0001A03D
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int[]>(descriptor, "selectedIndices", this.SelectedIndices, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001BE59 File Offset: 0x0001A059
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}
	}
}
