using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020004C7 RID: 1223
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridVirtualization : ObjectWithState
	{
		// Token: 0x06002C53 RID: 11347 RVA: 0x00091A17 File Offset: 0x0008FC17
		public GridVirtualization(StateBag OwnerStateBag, RadGrid owner) : base("cs_virtualization_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x00091A2C File Offset: 0x0008FC2C
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x00091A55 File Offset: 0x0008FC55
		[Description("Gets or sets a value determining if the RadGrid Virtualization will be turned on")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public bool EnableVirtualization
		{
			get
			{
				object obj = base.ViewState["EnableVirtualization"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableVirtualization"] = value;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x00091A70 File Offset: 0x0008FC70
		// (set) Token: 0x06002C57 RID: 11351 RVA: 0x00091A99 File Offset: 0x0008FC99
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating if the scrolling will be done for the whole data source or only for the current page.")]
		[DefaultValue(false)]
		public bool EnableCurrentPageScrollOnly
		{
			get
			{
				object obj = base.ViewState["EnableCurrentPageScrollOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableCurrentPageScrollOnly"] = value;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x00091AB4 File Offset: 0x0008FCB4
		// (set) Token: 0x06002C59 RID: 11353 RVA: 0x00091AE1 File Offset: 0x0008FCE1
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the number of records that will be initially send from the server and cached on the client")]
		[DefaultValue(5000)]
		public int InitiallyCachedItemsCount
		{
			get
			{
				object obj = base.ViewState["InitiallyCachedItemsCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 5000;
			}
			set
			{
				if (value < 20)
				{
					throw new GridException("RadGrid.ClientSettings.Virtualization.InitiallyCachedItemsCount value should be bigger than 20");
				}
				base.ViewState["InitiallyCachedItemsCount"] = value;
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x00091B0C File Offset: 0x0008FD0C
		// (set) Token: 0x06002C5B RID: 11355 RVA: 0x00091B39 File Offset: 0x0008FD39
		[DefaultValue(1000)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the value that determines how many items will be retrieved every time a request is made")]
		public int RetrievedItemsPerRequest
		{
			get
			{
				object obj = base.ViewState["RetrievedItemsPerRequest"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 1000;
			}
			set
			{
				if (value < 20)
				{
					throw new GridException("RadGrid.ClientSettings.Virtualization.RetrievedItemsPerRequest value should be bigger than 20");
				}
				base.ViewState["RetrievedItemsPerRequest"] = value;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x00091B64 File Offset: 0x0008FD64
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x00091B8E File Offset: 0x0008FD8E
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets the index of the RadGrid active row.")]
		[DefaultValue(100)]
		public int ItemsPerView
		{
			get
			{
				object obj = base.ViewState["ItemsPerView"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 100;
			}
			set
			{
				if (value < 20)
				{
					throw new GridException("RadGrid.ClientSettings.Virtualization.ItemsPerView value should be bigger than 20");
				}
				base.ViewState["ItemsPerView"] = value;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x00091BB8 File Offset: 0x0008FDB8
		// (set) Token: 0x06002C5F RID: 11359 RVA: 0x00091BE5 File Offset: 0x0008FDE5
		[DefaultValue(2147483647)]
		[Category("Client")]
		[Description("Gets or sets the index of the RadGrid active row.")]
		[NotifyParentProperty(true)]
		public int MaxCacheSize
		{
			get
			{
				object obj = base.ViewState["MaxCacheSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return int.MaxValue;
			}
			set
			{
				base.ViewState["MaxCacheSize"] = value;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x00091C00 File Offset: 0x0008FE00
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x00091C2D File Offset: 0x0008FE2D
		[Description("Gets or sets the index of the RadGrid active row.")]
		[Category("Client")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string LoadingPanelID
		{
			get
			{
				object obj = base.ViewState["LoadingPanelID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["LoadingPanelID"] = value;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x00091C40 File Offset: 0x0008FE40
		// (set) Token: 0x06002C63 RID: 11363 RVA: 0x00091C48 File Offset: 0x0008FE48
		internal int CurrentPageIndex { get; set; }

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x00091C51 File Offset: 0x0008FE51
		// (set) Token: 0x06002C65 RID: 11365 RVA: 0x00091C59 File Offset: 0x0008FE59
		internal int FirstIndexInPage { get; set; }

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06002C66 RID: 11366 RVA: 0x00091C62 File Offset: 0x0008FE62
		// (set) Token: 0x06002C67 RID: 11367 RVA: 0x00091C6A File Offset: 0x0008FE6A
		internal decimal ItemAtTop { get; set; }

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x00091C73 File Offset: 0x0008FE73
		// (set) Token: 0x06002C69 RID: 11369 RVA: 0x00091C7B File Offset: 0x0008FE7B
		internal int StartIndex { get; set; }

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x00091C84 File Offset: 0x0008FE84
		internal bool ShouldCreateCustomScrollbar
		{
			get
			{
				return this.EnableVirtualization && (!this.EnableCurrentPageScrollOnly || (this.EnableCurrentPageScrollOnly && !this.owner.AllowPaging) || (this.EnableCurrentPageScrollOnly && this.owner.AllowPaging && this.owner.PageSize > this.ItemsPerView));
			}
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x00091CE4 File Offset: 0x0008FEE4
		internal void ValidateProperties()
		{
			if (this.EnableVirtualization)
			{
				if (this.InitiallyCachedItemsCount < this.ItemsPerView)
				{
					throw new GridException("RadGrid.ClientSettings.Virtualization.InitiallyCachedItemsCount value should be bigger or equal to RadGrid.ClientSettings.Virtualization.ItemsPerView");
				}
				if (this.RetrievedItemsPerRequest < this.ItemsPerView)
				{
					throw new GridException("RadGrid.ClientSettings.Virtualization.RetrievedItemsPerRequest value should be bigger or equal to RadGrid.ClientSettings.Virtualization.ItemsPerView");
				}
				if (this.MaxCacheSize < this.ItemsPerView)
				{
					throw new GridException("RadGrid.ClientSettings.Virtualization.MaxCacheSize value should be bigger or equal to RadGrid.ClientSettings.Virtualization.ItemsPerView");
				}
			}
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x00091D44 File Offset: 0x0008FF44
		internal void ValidateTableViewLimitations(GridTableView tableView)
		{
			if (tableView.GroupByExpressions.Count > 0)
			{
				throw new GridException("RadGrid Virtualization functionality is not supported with grouping");
			}
		}

		// Token: 0x04000B77 RID: 2935
		private readonly RadGrid owner;
	}
}
