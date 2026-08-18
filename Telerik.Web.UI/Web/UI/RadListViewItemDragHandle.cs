using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020019BC RID: 6588
	[ParseChildren(ChildrenAsProperties = true)]
	[ToolboxBitmap(typeof(RadListViewItemDragHandle), "Telerik.Web.UI.ListView.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data")]
	public class RadListViewItemDragHandle : WebControl
	{
		// Token: 0x0600FE94 RID: 65172 RVA: 0x0039280B File Offset: 0x00390A0B
		public RadListViewItemDragHandle() : base(HtmlTextWriterTag.Input)
		{
		}

		// Token: 0x0600FE95 RID: 65173 RVA: 0x00392818 File Offset: 0x00390A18
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.listViewItem = this.GetParentListViewDataItem();
			if (this.listViewItem != null)
			{
				this.clientMouseDownHandler = string.Format(this.ClientMouseDownHandlerFormat, this.listViewItem.OwnerListView.ClientID, this.listViewItem.DisplayIndex);
			}
			this.CssClass = this.GetMergedCustomAndSkinCssClasses();
		}

		// Token: 0x0600FE96 RID: 65174 RVA: 0x00392880 File Offset: 0x00390A80
		protected virtual RadListViewDataItem GetParentListViewDataItem()
		{
			Control parent = this.Parent;
			while (parent != null && parent != this.Page.Form)
			{
				RadListViewDataItem radListViewDataItem = parent as RadListViewDataItem;
				if (radListViewDataItem != null)
				{
					return radListViewDataItem;
				}
				parent = parent.Parent;
			}
			return null;
		}

		// Token: 0x0600FE97 RID: 65175 RVA: 0x003928BC File Offset: 0x00390ABC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			if (!string.IsNullOrEmpty(this.clientMouseDownHandler))
			{
				if (this.Page.Request != null && !string.IsNullOrEmpty(this.Page.Request.UserAgent) && (Regex.IsMatch(this.Page.Request.UserAgent, "like\\sMac\\sOS\\sX.*Mobile\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "Android.*Safari\\S+") || Regex.IsMatch(this.Page.Request.UserAgent, "BlackBerry.*Safari\\S+")))
				{
					writer.AddAttribute("ontouchstart", this.clientMouseDownHandler);
					return;
				}
				writer.AddAttribute("onmousedown", this.clientMouseDownHandler);
			}
		}

		// Token: 0x0600FE98 RID: 65176 RVA: 0x00392988 File Offset: 0x00390B88
		protected virtual string GetMergedCustomAndSkinCssClasses()
		{
			if (string.IsNullOrEmpty(this.CssClass))
			{
				return this.SkinCssClass;
			}
			if (this.CssClass == this.SkinCssClass || this.CssClass.Contains(" " + this.SkinCssClass) || this.CssClass.Contains(this.SkinCssClass + " "))
			{
				return this.CssClass;
			}
			return this.CssClass + " " + this.SkinCssClass;
		}

		// Token: 0x17004CDB RID: 19675
		// (get) Token: 0x0600FE99 RID: 65177 RVA: 0x00392A13 File Offset: 0x00390C13
		public virtual string SkinCssClass
		{
			get
			{
				return "rlvDrag";
			}
		}

		// Token: 0x17004CDC RID: 19676
		// (get) Token: 0x0600FE9A RID: 65178 RVA: 0x00392A1A File Offset: 0x00390C1A
		internal string ClientMouseDownHandlerFormat
		{
			get
			{
				return "$find('{0}')._itemDrag._dragHandleMouseDown(event, {1})";
			}
		}

		// Token: 0x0400483E RID: 18494
		private RadListViewDataItem listViewItem;

		// Token: 0x0400483F RID: 18495
		private string clientMouseDownHandler;
	}
}
