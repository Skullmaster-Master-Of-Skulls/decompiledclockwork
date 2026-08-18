using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001957 RID: 6487
	public class RadDataPagerPageSizeField : RadDataPagerField, IDisposable
	{
		// Token: 0x17004BE8 RID: 19432
		// (get) Token: 0x0600FB29 RID: 64297 RVA: 0x003893FA File Offset: 0x003875FA
		// (set) Token: 0x0600FB2A RID: 64298 RVA: 0x00389425 File Offset: 0x00387625
		[NotifyParentProperty(true)]
		[Description("Gets or sets the type of the page size drop down control")]
		public PagerDropDownControlType PageSizeControlType
		{
			get
			{
				if (base.ViewState["PageSizeControlType"] == null)
				{
					return PagerDropDownControlType.RadComboBox;
				}
				return (PagerDropDownControlType)base.ViewState["PageSizeControlType"];
			}
			set
			{
				base.ViewState["PageSizeControlType"] = value;
			}
		}

		// Token: 0x17004BE9 RID: 19433
		// (get) Token: 0x0600FB2B RID: 64299 RVA: 0x0038943D File Offset: 0x0038763D
		// (set) Token: 0x0600FB2C RID: 64300 RVA: 0x00389473 File Offset: 0x00387673
		[NotifyParentProperty(true)]
		[DefaultValue("Page size")]
		public string PageSizeText
		{
			get
			{
				return this.CheckDefaultValue(base.ViewState["PageSizeText"], "Page size") ?? base.Owner.Localization.PageSizeText;
			}
			set
			{
				base.ViewState["PageSizeText"] = value;
			}
		}

		// Token: 0x17004BEA RID: 19434
		// (get) Token: 0x0600FB2D RID: 64301 RVA: 0x00389488 File Offset: 0x00387688
		// (set) Token: 0x0600FB2E RID: 64302 RVA: 0x003894B2 File Offset: 0x003876B2
		[NotifyParentProperty(true)]
		[DefaultValue(50)]
		public int PageSizeComboWidth
		{
			get
			{
				object obj = base.ViewState["PageSizeComboWidth"];
				if (obj == null)
				{
					return 50;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["PageSizeComboWidth"] = value;
			}
		}

		// Token: 0x17004BEB RID: 19435
		// (get) Token: 0x0600FB2F RID: 64303 RVA: 0x003894CA File Offset: 0x003876CA
		// (set) Token: 0x0600FB30 RID: 64304 RVA: 0x003894E1 File Offset: 0x003876E1
		[TypeConverter(typeof(IntegerArrayConverter))]
		[Category("Data")]
		[DefaultValue(null)]
		[Description("Comma or Semicolon delimited list of page sizes values")]
		[NotifyParentProperty(true)]
		public virtual int[] PageSizes
		{
			get
			{
				return base.ViewState["PageSizes"] as int[];
			}
			set
			{
				if (value != null && value.Length > 0)
				{
					base.ViewState["PageSizes"] = value;
					return;
				}
				base.ViewState["PageSizes"] = null;
			}
		}

		// Token: 0x0600FB31 RID: 64305 RVA: 0x00389528 File Offset: 0x00387728
		public override void InitializeFieldControls(RadDataPagerFieldItem inItem)
		{
			if (base.Owner.AllowRouting)
			{
				this.routingEnabled = true;
			}
			if (this.PageSizeControlType == PagerDropDownControlType.None)
			{
				return;
			}
			inItem.Controls.Add(new LiteralControl(string.Format("<span class='{1}'>{0}</span>", this.PageSizeText, RadDataPagerPageSizeField.PageSizeLabelClassName)));
			ControlItemContainer controlItemContainer = this.CreateDropDownControlInstance();
			this.PreparePageSizes();
			this.InsertPageSizes(controlItemContainer);
			this.AdjustDropDownControlWidth(controlItemContainer);
			if (!this.InitializePageSizeCombo(controlItemContainer as RadComboBox))
			{
				this.InitializePageSizeDropDownList(controlItemContainer as RadDropDownList);
			}
			controlItemContainer.PreRender += delegate(object sender, EventArgs e)
			{
				(sender as ISkinnableControl).Skin = base.Owner.RuntimeSkin;
			};
			this.PrepareSkinnableControlProperties(controlItemContainer);
			inItem.Controls.Add(controlItemContainer);
		}

		// Token: 0x0600FB32 RID: 64306 RVA: 0x003895D4 File Offset: 0x003877D4
		private ControlItemContainer CreateDropDownControlInstance()
		{
			ControlItemContainer controlItemContainer;
			if (this.PageSizeControlType == PagerDropDownControlType.RadComboBox)
			{
				controlItemContainer = new RadComboBox
				{
					ID = "PageSizeComboBox"
				};
			}
			else
			{
				controlItemContainer = new RadDropDownList
				{
					ID = "PageSizeDropDownList"
				};
			}
			controlItemContainer.RenderMode = base.Owner.RenderMode;
			return controlItemContainer;
		}

		// Token: 0x0600FB33 RID: 64307 RVA: 0x00389624 File Offset: 0x00387824
		private bool InitializePageSizeCombo(RadComboBox pageSizeCombo)
		{
			bool result = false;
			if (pageSizeCombo != null)
			{
				pageSizeCombo.SelectedValue = base.Owner.PageSize.ToString();
				pageSizeCombo.EnableAriaSupport = base.Owner.EnableAriaSupport;
				if (base.Owner.AllowSEOPaging || this.routingEnabled)
				{
					pageSizeCombo.OnClientSelectedIndexChanged = "Telerik.Web.UI.RadDataPager.ChangePageSizeComboHandler";
				}
				else
				{
					pageSizeCombo.AutoPostBack = true;
					pageSizeCombo.SelectedIndexChanged += this.PageSizeComboIndexChanged;
				}
				if (pageSizeCombo.EnableAriaSupport && (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile))
				{
					pageSizeCombo.InputTitle = "Page size";
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600FB34 RID: 64308 RVA: 0x003896D4 File Offset: 0x003878D4
		private void InitializePageSizeDropDownList(RadDropDownList ddl)
		{
			if (ddl != null)
			{
				ddl.SelectedValue = base.Owner.PageSize.ToString();
				if (base.Owner.AllowSEOPaging || this.routingEnabled)
				{
					ddl.OnClientSelectedIndexChanged = "Telerik.Web.UI.RadDataPager.ChangePageSizeComboHandler";
					return;
				}
				ddl.AutoPostBack = true;
				ddl.SelectedIndexChanged += this.PageSizeDropDownListIndexChanged;
			}
		}

		// Token: 0x0600FB35 RID: 64309 RVA: 0x0038978C File Offset: 0x0038798C
		private void InsertPageSizes(ControlItemContainer ddControl)
		{
			RadDataPagerPageSizeField.<>c__DisplayClass6 CS$<>8__locals1 = new RadDataPagerPageSizeField.<>c__DisplayClass6();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.combo = (ddControl as RadComboBox);
			if (CS$<>8__locals1.combo != null)
			{
				this.allowedPageSizeValues.ForEach(delegate(int item)
				{
					CS$<>8__locals1.combo.Items.Add(CS$<>8__locals1.<>4__this.CreateNewComboItem(item));
				});
				return;
			}
			RadDropDownList ddl = ddControl as RadDropDownList;
			if (ddl != null)
			{
				this.allowedPageSizeValues.ForEach(delegate(int item)
				{
					ddl.Items.Add(CS$<>8__locals1.<>4__this.CreateNewDropDownItem(item));
				});
			}
		}

		// Token: 0x0600FB36 RID: 64310 RVA: 0x00389818 File Offset: 0x00387A18
		private RadComboBoxItem CreateNewComboItem(int pageSize)
		{
			RadComboBoxItem radComboBoxItem = new RadComboBoxItem(pageSize.ToString(), pageSize.ToString());
			radComboBoxItem.Attributes.Add("seoRedirectUrl", base.Owner.GeneratePagingStateAttributeLink(pageSize));
			radComboBoxItem.Attributes.Add("seoPagerKey", base.Owner.SEOPagingQueryPageKey);
			radComboBoxItem.Attributes.Add("dataPagerClientId", base.Owner.ClientID);
			return radComboBoxItem;
		}

		// Token: 0x0600FB37 RID: 64311 RVA: 0x0038988C File Offset: 0x00387A8C
		private DropDownListItem CreateNewDropDownItem(int pageSize)
		{
			DropDownListItem dropDownListItem = new DropDownListItem(pageSize.ToString(), pageSize.ToString());
			dropDownListItem.Attributes.Add("seoRedirectUrl", base.Owner.GeneratePagingStateAttributeLink(pageSize));
			dropDownListItem.Attributes.Add("seoPagerKey", base.Owner.SEOPagingQueryPageKey);
			dropDownListItem.Attributes.Add("dataPagerClientId", base.Owner.ClientID);
			return dropDownListItem;
		}

		// Token: 0x0600FB38 RID: 64312 RVA: 0x00389900 File Offset: 0x00387B00
		private void PreparePageSizes()
		{
			bool flag = false;
			if (this.PageSizes != null && this.PageSizes.Length > 0)
			{
				this.allowedPageSizeValues.Clear();
				this.allowedPageSizeValues.AddRange(this.PageSizes);
			}
			if (!this.allowedPageSizeValues.Contains(base.Owner.PageSize))
			{
				this.allowedPageSizeValues.Add(base.Owner.PageSize);
				flag = true;
			}
			if (!this.allowedPageSizeValues.Contains(base.Owner.OriginalPageSize) && base.Owner.OriginalPageSize != -1)
			{
				this.allowedPageSizeValues.Add(base.Owner.OriginalPageSize);
				flag = true;
			}
			if (this.PageSizes != null && this.PageSizes.Length > 0 && !this.allowedPageSizeValues.Contains(10) && base.Owner.OriginalPageSize == -1)
			{
				this.allowedPageSizeValues.Add(10);
				flag = true;
			}
			if (flag)
			{
				this.allowedPageSizeValues.Sort();
			}
		}

		// Token: 0x0600FB39 RID: 64313 RVA: 0x003899FC File Offset: 0x00387BFC
		private void AdjustDropDownControlWidth(ControlItemContainer ddControl)
		{
			if (base.Owner.ResolvedRenderMode == RenderMode.Classic)
			{
				if ((base.Owner.RuntimeSkin == "MetroTouch" || base.Owner.RuntimeSkin == "Glow" || base.Owner.RuntimeSkin == "Silk" || base.Owner.RuntimeSkin == "BlackMetroTouch") && base.ViewState["PageSizeComboWidth"] == null)
				{
					this.PageSizeComboWidth += 12;
				}
				ddControl.Width = Unit.Pixel(this.PageSizeComboWidth);
				return;
			}
			int length = base.Owner.PageSize.ToString().Length;
			ddControl.Width = Unit.Parse(length * 2 + "em");
		}

		// Token: 0x0600FB3A RID: 64314 RVA: 0x00389ADF File Offset: 0x00387CDF
		protected virtual void PageSizeComboIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			base.Owner.FireCommand("PageSizeChange", e.Value);
		}

		// Token: 0x0600FB3B RID: 64315 RVA: 0x00389AF7 File Offset: 0x00387CF7
		protected virtual void PageSizeDropDownListIndexChanged(object sender, DropDownListEventArgs e)
		{
			base.Owner.FireCommand("PageSizeChange", e.Value);
		}

		// Token: 0x0600FB3C RID: 64316 RVA: 0x00389B0F File Offset: 0x00387D0F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600FB3D RID: 64317 RVA: 0x00389B1E File Offset: 0x00387D1E
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed")]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0400476A RID: 18282
		protected static string PageSizeLabelClassName = "rdpPagerLabel";

		// Token: 0x0400476B RID: 18283
		[SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected List<int> allowedPageSizeValues = new List<int>
		{
			5,
			10,
			20,
			50
		};

		// Token: 0x0400476C RID: 18284
		private bool routingEnabled;
	}
}
