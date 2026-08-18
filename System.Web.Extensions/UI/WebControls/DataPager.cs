using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200008F RID: 143
	[ParseChildren(true)]
	[PersistChildren(false)]
	[Themeable(true)]
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.DataPagerDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[ToolboxBitmap(typeof(DataPager), "DataPager.bmp")]
	public class DataPager : Control, IAttributeAccessor, INamingContainer, ICompositeControlDesignerAccessor
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0001B4B1 File Offset: 0x000196B1
		public DataPager()
		{
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001B4C1 File Offset: 0x000196C1
		internal DataPager(IPage page)
		{
			this._page = page;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001B4D8 File Offset: 0x000196D8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new AttributeCollection(new StateBag(true));
				}
				return this._attributes;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001AC0A File Offset: 0x00018E0A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001B4FC File Offset: 0x000196FC
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataPagerFieldTypeEditor, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Default")]
		[ResourceDescription("DataPager_Fields")]
		public virtual DataPagerFieldCollection Fields
		{
			get
			{
				if (this._fields == null)
				{
					this._fields = new DataPagerFieldCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._fields).TrackViewState();
					}
					this._fields.FieldsChanged += this.OnFieldsChanged;
				}
				return this._fields;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001B54D File Offset: 0x0001974D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001B558 File Offset: 0x00019758
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0001B585 File Offset: 0x00019785
		[DefaultValue("")]
		[IDReferenceProperty(typeof(IPageableItemContainer))]
		[WebCategory("Paging")]
		[ResourceDescription("DataPager_PagedControlID")]
		[Themeable(false)]
		public virtual string PagedControlID
		{
			get
			{
				object obj = this.ViewState["PagedControlID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PagedControlID"] = value;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001B598 File Offset: 0x00019798
		internal IPage IPage
		{
			get
			{
				if (this._page != null)
				{
					return this._page;
				}
				Page page = this.Page;
				if (page == null)
				{
					throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
				}
				return new PageWrapper(page);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001B54D File Offset: 0x0001974D
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001B5D0 File Offset: 0x000197D0
		[DefaultValue(10)]
		[WebCategory("Paging")]
		[ResourceDescription("DataPager_PageSize")]
		public int PageSize
		{
			get
			{
				return this._maximumRows;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this._maximumRows)
				{
					this._maximumRows = value;
					if (this._initialized)
					{
						this.CreatePagerFields();
						this.SetPageProperties(this._startRowIndex, this._maximumRows, true);
					}
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001B620 File Offset: 0x00019820
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001B64D File Offset: 0x0001984D
		[WebCategory("Paging")]
		[DefaultValue("")]
		[ResourceDescription("DataPager_QueryStringField")]
		public string QueryStringField
		{
			get
			{
				object obj = this.ViewState["QueryStringField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["QueryStringField"] = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001B660 File Offset: 0x00019860
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001B668 File Offset: 0x00019868
		internal bool QueryStringHandled
		{
			get
			{
				return this._queryStringHandled;
			}
			set
			{
				this._queryStringHandled = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001B671 File Offset: 0x00019871
		internal string QueryStringValue
		{
			get
			{
				if (base.DesignMode)
				{
					return string.Empty;
				}
				return this.IPage.Request.QueryString[this.QueryStringField];
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001B69C File Offset: 0x0001989C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001B6A4 File Offset: 0x000198A4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001B6A8 File Offset: 0x000198A8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001B6B0 File Offset: 0x000198B0
		protected virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			if (this._attributes != null)
			{
				AttributeCollection attributes = this.Attributes;
				foreach (object obj in attributes.Keys)
				{
					string text = (string)obj;
					writer.AddAttribute(text, attributes[text]);
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001B712 File Offset: 0x00019912
		protected virtual void ConnectToEvents(IPageableItemContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			this._pageableItemContainer.TotalRowCountAvailable += this.OnTotalRowCountAvailable;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001B73C File Offset: 0x0001993C
		protected virtual void CreatePagerFields()
		{
			this._creatingPagerFields = true;
			this.Controls.Clear();
			if (this._fields != null)
			{
				foreach (object obj in this._fields)
				{
					DataPagerField dataPagerField = (DataPagerField)obj;
					DataPagerFieldItem dataPagerFieldItem = new DataPagerFieldItem(dataPagerField, this);
					this.Controls.Add(dataPagerFieldItem);
					if (dataPagerField.Visible)
					{
						dataPagerField.CreateDataPagers(dataPagerFieldItem, this._startRowIndex, this._maximumRows, this._totalRowCount, this._fields.IndexOf(dataPagerField));
						dataPagerFieldItem.DataBind();
					}
				}
			}
			this._creatingPagerFields = false;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001B7F8 File Offset: 0x000199F8
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.EnsureChildControls();
			this.DataBindChildren();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001B814 File Offset: 0x00019A14
		protected virtual IPageableItemContainer FindPageableItemContainer()
		{
			if (string.IsNullOrEmpty(this.PagedControlID))
			{
				Control namingContainer = this.NamingContainer;
				IPageableItemContainer pageableItemContainer = null;
				while (pageableItemContainer == null && namingContainer != this.Page)
				{
					if (namingContainer == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.DataPager_NoNamingContainer, new object[]
						{
							this.ID
						}));
					}
					pageableItemContainer = (namingContainer as IPageableItemContainer);
					namingContainer = namingContainer.NamingContainer;
				}
				return pageableItemContainer;
			}
			Control control = DataBoundControlHelper.FindControl(this, this.PagedControlID);
			if (control == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.DataPager_PageableItemContainerNotFound, new object[]
				{
					this.PagedControlID
				}));
			}
			IPageableItemContainer pageableItemContainer2 = control as IPageableItemContainer;
			if (pageableItemContainer2 == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.DataPager_ControlIsntPageable, new object[]
				{
					this.PagedControlID
				}));
			}
			return pageableItemContainer2;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		internal string GetQueryStringNavigateUrl(int pageNumber)
		{
			if (this._queryStringNavigateUrl == null)
			{
				string queryStringField = this.QueryStringField;
				StringBuilder stringBuilder = new StringBuilder();
				if (base.DesignMode)
				{
					stringBuilder.Append("?");
				}
				else
				{
					bool flag = this.IPage.Form != null && this.IPage.Form.Method.Equals("GET", StringComparison.OrdinalIgnoreCase);
					HttpRequestBase request = this.IPage.Request;
					stringBuilder.Append(request.Path);
					stringBuilder.Append("?");
					foreach (string text in request.QueryString.AllKeys)
					{
						if (!string.IsNullOrEmpty(text) && (!flag || !ControlUtil.IsBuiltInHiddenField(text)) && !text.Equals(queryStringField, StringComparison.OrdinalIgnoreCase))
						{
							stringBuilder.Append(HttpUtility.UrlEncode(text));
							stringBuilder.Append("=");
							stringBuilder.Append(HttpUtility.UrlEncode(request.QueryString[text]));
							stringBuilder.Append("&");
						}
					}
				}
				stringBuilder.Append(queryStringField);
				stringBuilder.Append("=");
				this._queryStringNavigateUrl = stringBuilder.ToString();
			}
			return this._queryStringNavigateUrl + pageNumber.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001BA2C File Offset: 0x00019C2C
		protected internal override void LoadControlState(object savedState)
		{
			this._startRowIndex = 0;
			this._maximumRows = 10;
			this._totalRowCount = -1;
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array[0]);
				if (array[1] != null)
				{
					this._startRowIndex = (int)array[1];
				}
				if (array[2] != null)
				{
					this._maximumRows = (int)array[2];
				}
				if (array[3] != null)
				{
					this._totalRowCount = (int)array[3];
				}
			}
			else
			{
				base.LoadControlState(null);
			}
			if (this._pageableItemContainer == null)
			{
				this._pageableItemContainer = this.FindPageableItemContainer();
				if (this._pageableItemContainer == null)
				{
					throw new InvalidOperationException(AtlasWeb.DataPager_NoPageableItemContainer);
				}
				this.ConnectToEvents(this._pageableItemContainer);
			}
			this._pageableItemContainer.SetPageProperties(this._startRowIndex, this._maximumRows, false);
			this._setPageProperties = true;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001BAF6 File Offset: 0x00019CF6
		private bool HasAttributes
		{
			get
			{
				return this._attributes != null && this._attributes.Count > 0;
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001BB10 File Offset: 0x00019D10
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Fields).LoadViewState(array[1]);
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001BB44 File Offset: 0x00019D44
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			DataPagerFieldCommandEventArgs dataPagerFieldCommandEventArgs = e as DataPagerFieldCommandEventArgs;
			bool result = false;
			if (dataPagerFieldCommandEventArgs != null)
			{
				DataPagerFieldItem item = dataPagerFieldCommandEventArgs.Item;
				if (item != null && item.PagerField != null)
				{
					item.PagerField.HandleEvent(dataPagerFieldCommandEventArgs);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001BB7E File Offset: 0x00019D7E
		private void OnFieldsChanged(object source, EventArgs e)
		{
			if (this._initialized)
			{
				this.SetPageProperties(this._startRowIndex, this._maximumRows, true);
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001BB9C File Offset: 0x00019D9C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				this._pageableItemContainer = this.FindPageableItemContainer();
				if (this._pageableItemContainer != null)
				{
					this.ConnectToEvents(this._pageableItemContainer);
					if (!string.IsNullOrEmpty(this.QueryStringField))
					{
						this._startRowIndex = this.GetStartRowIndexFromQueryString();
					}
					this._pageableItemContainer.SetPageProperties(this._startRowIndex, this._maximumRows, false);
					this._setPageProperties = true;
				}
				if (this.Page != null)
				{
					this.Page.RegisterRequiresControlState(this);
				}
			}
			this._initialized = true;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001BC2C File Offset: 0x00019E2C
		private int GetStartRowIndexFromQueryString()
		{
			int result = 0;
			int num = 0;
			if (int.TryParse(this.QueryStringValue, out num))
			{
				result = (num - 1) * this._maximumRows;
			}
			return result;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001BC58 File Offset: 0x00019E58
		protected internal override void OnLoad(EventArgs e)
		{
			if (this._pageableItemContainer == null)
			{
				this._pageableItemContainer = this.FindPageableItemContainer();
			}
			if (this._pageableItemContainer == null)
			{
				throw new InvalidOperationException(AtlasWeb.DataPager_NoPageableItemContainer);
			}
			if (!this._setPageProperties)
			{
				this.ConnectToEvents(this._pageableItemContainer);
				if (!string.IsNullOrEmpty(this.QueryStringField))
				{
					this._startRowIndex = this.GetStartRowIndexFromQueryString();
				}
				this._pageableItemContainer.SetPageProperties(this._startRowIndex, this._maximumRows, false);
				this._setPageProperties = true;
			}
			base.OnLoad(e);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		protected virtual void OnTotalRowCountAvailable(object sender, PageEventArgs e)
		{
			this._totalRowCount = e.TotalRowCount;
			this._startRowIndex = e.StartRowIndex;
			this._maximumRows = e.MaximumRows;
			if (this._totalRowCount <= this._startRowIndex && this._totalRowCount > 0)
			{
				int num = this._startRowIndex - this._maximumRows;
				if (num < 0)
				{
					num = 0;
				}
				if (num >= this._totalRowCount)
				{
					num = 0;
				}
				this._pageableItemContainer.SetPageProperties(num, this._maximumRows, true);
				return;
			}
			if (!this._creatingPagerFields)
			{
				this.CreatePagerFields();
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001BD69 File Offset: 0x00019F69
		protected virtual void RecreateChildControls()
		{
			base.ChildControlsCreated = false;
			this.EnsureChildControls();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001BD78 File Offset: 0x00019F78
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.EnsureChildControls();
				this.OnTotalRowCountAvailable(null, new PageEventArgs(0, this.PageSize, 101));
			}
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001BDB1 File Offset: 0x00019FB1
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001BDC6 File Offset: 0x00019FC6
		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			if (obj != null || this._startRowIndex != 0 || this._maximumRows != 10 || this._totalRowCount != -1)
			{
				return new object[]
				{
					obj,
					(this._startRowIndex == 0) ? null : this._startRowIndex,
					(this._maximumRows == 10) ? null : this._maximumRows,
					(this._totalRowCount == -1) ? null : this._totalRowCount
				};
			}
			return null;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001BE60 File Offset: 0x0001A060
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = (this._fields != null) ? ((IStateManager)this._fields).SaveViewState() : null;
			return new object[]
			{
				obj,
				obj2
			};
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001BE99 File Offset: 0x0001A099
		public virtual void SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			if (base.DesignMode)
			{
				return;
			}
			if (this._pageableItemContainer == null)
			{
				throw new InvalidOperationException(AtlasWeb.DataPager_PagePropertiesCannotBeSet);
			}
			this._startRowIndex = startRowIndex;
			this._maximumRows = maximumRows;
			this._pageableItemContainer.SetPageProperties(startRowIndex, maximumRows, databind);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001BED3 File Offset: 0x0001A0D3
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._fields != null)
			{
				((IStateManager)this._fields).TrackViewState();
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001BEEE File Offset: 0x0001A0EE
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			this.RecreateChildControls();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001BEF6 File Offset: 0x0001A0F6
		string IAttributeAccessor.GetAttribute(string name)
		{
			return this.Attributes[name];
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001BF04 File Offset: 0x0001A104
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x04000238 RID: 568
		private new readonly IPage _page;

		// Token: 0x04000239 RID: 569
		private DataPagerFieldCollection _fields;

		// Token: 0x0400023A RID: 570
		private IPageableItemContainer _pageableItemContainer;

		// Token: 0x0400023B RID: 571
		private int _startRowIndex;

		// Token: 0x0400023C RID: 572
		private int _maximumRows = 10;

		// Token: 0x0400023D RID: 573
		private int _totalRowCount;

		// Token: 0x0400023E RID: 574
		private bool _setPageProperties;

		// Token: 0x0400023F RID: 575
		private bool _initialized;

		// Token: 0x04000240 RID: 576
		private AttributeCollection _attributes;

		// Token: 0x04000241 RID: 577
		private bool _creatingPagerFields;

		// Token: 0x04000242 RID: 578
		private bool _queryStringHandled;

		// Token: 0x04000243 RID: 579
		private string _queryStringNavigateUrl;
	}
}
