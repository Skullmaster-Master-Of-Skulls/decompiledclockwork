using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000396 RID: 918
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public abstract class DataGridColumn : IStateManager
	{
		// Token: 0x06002BC2 RID: 11202 RVA: 0x0008EF3B File Offset: 0x0008D13B
		protected DataGridColumn()
		{
			this.statebag = new StateBag();
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x0008EF4E File Offset: 0x0008D14E
		protected bool DesignMode
		{
			get
			{
				return this.owner != null && this.owner.DesignMode;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x0008EF65 File Offset: 0x0008D165
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("DataGridColumn_FooterStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.footerStyle).TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x0008EF93 File Offset: 0x0008D193
		internal TableItemStyle FooterStyleInternal
		{
			get
			{
				return this.footerStyle;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002BC6 RID: 11206 RVA: 0x0008EF9C File Offset: 0x0008D19C
		// (set) Token: 0x06002BC7 RID: 11207 RVA: 0x0008EFC9 File Offset: 0x0008D1C9
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("DataGridColumn_FooterText")]
		public virtual string FooterText
		{
			get
			{
				object obj = this.ViewState["FooterText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FooterText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06002BC8 RID: 11208 RVA: 0x0008EFE4 File Offset: 0x0008D1E4
		// (set) Token: 0x06002BC9 RID: 11209 RVA: 0x0008F011 File Offset: 0x0008D211
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[UrlProperty]
		[WebSysDescription("DataGridColumn_HeaderImageUrl")]
		public virtual string HeaderImageUrl
		{
			get
			{
				object obj = this.ViewState["HeaderImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderImageUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06002BCA RID: 11210 RVA: 0x0008F02A File Offset: 0x0008D22A
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("DataGridColumn_HeaderStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.headerStyle).TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x0008F058 File Offset: 0x0008D258
		internal TableItemStyle HeaderStyleInternal
		{
			get
			{
				return this.headerStyle;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x0008F060 File Offset: 0x0008D260
		// (set) Token: 0x06002BCD RID: 11213 RVA: 0x0008F08D File Offset: 0x0008D28D
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("DataGridColumn_HeaderText")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0008F0A6 File Offset: 0x0008D2A6
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("DataGridColumn_ItemStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.itemStyle == null)
				{
					this.itemStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.itemStyle).TrackViewState();
					}
				}
				return this.itemStyle;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x0008F0D4 File Offset: 0x0008D2D4
		internal TableItemStyle ItemStyleInternal
		{
			get
			{
				return this.itemStyle;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x0008F0DC File Offset: 0x0008D2DC
		protected DataGrid Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x0008F0E4 File Offset: 0x0008D2E4
		// (set) Token: 0x06002BD2 RID: 11218 RVA: 0x0008F111 File Offset: 0x0008D311
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("DataGridColumn_SortExpression")]
		public virtual string SortExpression
		{
			get
			{
				object obj = this.ViewState["SortExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SortExpression"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x0008F12A File Offset: 0x0008D32A
		protected StateBag ViewState
		{
			get
			{
				return this.statebag;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x0008F134 File Offset: 0x0008D334
		// (set) Token: 0x06002BD5 RID: 11221 RVA: 0x0008F15D File Offset: 0x0008D35D
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("DataGridColumn_Visible")]
		public bool Visible
		{
			get
			{
				object obj = this.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Visible"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void Initialize()
		{
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x0008F17C File Offset: 0x0008D37C
		public virtual void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			if (itemType != ListItemType.Header)
			{
				if (itemType != ListItemType.Footer)
				{
					return;
				}
				string text = this.FooterText;
				if (text.Length == 0)
				{
					text = "&nbsp;";
				}
				cell.Text = text;
			}
			else
			{
				WebControl webControl = null;
				bool flag = true;
				string text2 = null;
				if (this.owner != null && !this.owner.AllowSorting)
				{
					flag = false;
				}
				if (flag)
				{
					text2 = this.SortExpression;
					if (text2.Length == 0)
					{
						flag = false;
					}
				}
				string headerImageUrl = this.HeaderImageUrl;
				if (headerImageUrl.Length != 0)
				{
					if (flag)
					{
						webControl = new ImageButton
						{
							ImageUrl = this.HeaderImageUrl,
							CommandName = "Sort",
							CommandArgument = text2,
							CausesValidation = false
						};
					}
					else
					{
						webControl = new Image
						{
							ImageUrl = headerImageUrl
						};
					}
				}
				else
				{
					string text3 = this.HeaderText;
					if (flag)
					{
						webControl = new DataGridLinkButton
						{
							Text = text3,
							CommandName = "Sort",
							CommandArgument = text2,
							CausesValidation = false
						};
					}
					else
					{
						if (text3.Length == 0)
						{
							text3 = "&nbsp;";
						}
						cell.Text = text3;
					}
				}
				if (webControl != null)
				{
					cell.Controls.Add(webControl);
					return;
				}
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x0008F2A4 File Offset: 0x0008D4A4
		protected bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0008F2AC File Offset: 0x0008D4AC
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					((IStateManager)this.ViewState).LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					((IStateManager)this.ItemStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.HeaderStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.FooterStyle).LoadViewState(array[3]);
				}
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x0008F310 File Offset: 0x0008D510
		protected virtual void TrackViewState()
		{
			this.marked = true;
			((IStateManager)this.ViewState).TrackViewState();
			if (this.itemStyle != null)
			{
				((IStateManager)this.itemStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x0008F368 File Offset: 0x0008D568
		protected virtual void OnColumnChanged()
		{
			if (this.owner != null)
			{
				this.owner.OnColumnsChanged();
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x0008F380 File Offset: 0x0008D580
		protected virtual object SaveViewState()
		{
			object obj = ((IStateManager)this.ViewState).SaveViewState();
			object obj2 = (this.itemStyle != null) ? ((IStateManager)this.itemStyle).SaveViewState() : null;
			object obj3 = (this.headerStyle != null) ? ((IStateManager)this.headerStyle).SaveViewState() : null;
			object obj4 = (this.footerStyle != null) ? ((IStateManager)this.footerStyle).SaveViewState() : null;
			if (obj != null || obj2 != null || obj3 != null || obj4 != null)
			{
				return new object[]
				{
					obj,
					obj2,
					obj3,
					obj4
				};
			}
			return null;
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0008F402 File Offset: 0x0008D602
		internal void SetOwner(DataGrid owner)
		{
			this.owner = owner;
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00028752 File Offset: 0x00026952
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x0008F40B File Offset: 0x0008D60B
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x0008F413 File Offset: 0x0008D613
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x0008F41C File Offset: 0x0008D61C
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0008F424 File Offset: 0x0008D624
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x04001F21 RID: 7969
		private DataGrid owner;

		// Token: 0x04001F22 RID: 7970
		private TableItemStyle itemStyle;

		// Token: 0x04001F23 RID: 7971
		private TableItemStyle headerStyle;

		// Token: 0x04001F24 RID: 7972
		private TableItemStyle footerStyle;

		// Token: 0x04001F25 RID: 7973
		private StateBag statebag;

		// Token: 0x04001F26 RID: 7974
		private bool marked;
	}
}
