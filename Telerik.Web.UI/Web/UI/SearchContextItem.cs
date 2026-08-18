using System;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.SearchBox.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000877 RID: 2167
	[ToolboxItem(false)]
	public class SearchContextItem : WebControl, IMarkableStateManager, IStateManager
	{
		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x0600503A RID: 20538 RVA: 0x000FAFD0 File Offset: 0x000F91D0
		internal IRenderer Renderer
		{
			get
			{
				if (this._renderer != null)
				{
					return this._renderer;
				}
				return this._renderer = this.CreateControlRenderer();
			}
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x000FAFFB File Offset: 0x000F91FB
		protected internal virtual IRenderer CreateControlRenderer()
		{
			return new SearchContextItemRenderer(this);
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x000FB003 File Offset: 0x000F9203
		public SearchContextItem()
		{
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x000FB00B File Offset: 0x000F920B
		public SearchContextItem(string text)
		{
			this.Text = text;
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x000FB01A File Offset: 0x000F921A
		public SearchContextItem(string text, string key)
		{
			this.Text = text;
			this.Key = key;
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x000FB030 File Offset: 0x000F9230
		public SearchContextItem(string text, string key, object dataItem) : this(text, key)
		{
			this.DataItem = dataItem;
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x06005040 RID: 20544 RVA: 0x000FB041 File Offset: 0x000F9241
		// (set) Token: 0x06005041 RID: 20545 RVA: 0x000FB061 File Offset: 0x000F9261
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return ((string)this.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x000FB074 File Offset: 0x000F9274
		// (set) Token: 0x06005043 RID: 20547 RVA: 0x000FB094 File Offset: 0x000F9294
		[DefaultValue("")]
		public string Key
		{
			get
			{
				return ((string)this.ViewState["Key"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Key"] = value;
			}
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06005044 RID: 20548 RVA: 0x000FB0A7 File Offset: 0x000F92A7
		// (set) Token: 0x06005045 RID: 20549 RVA: 0x000FB0C7 File Offset: 0x000F92C7
		[UrlProperty]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				return ((string)this.ViewState["ImageUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06005046 RID: 20550 RVA: 0x000FB0DA File Offset: 0x000F92DA
		// (set) Token: 0x06005047 RID: 20551 RVA: 0x000FB0E2 File Offset: 0x000F92E2
		[Category("Behavior")]
		[Description("Whether the item is selected or not.")]
		[DefaultValue(false)]
		public bool Selected { get; set; }

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x000FB0EB File Offset: 0x000F92EB
		// (set) Token: 0x06005049 RID: 20553 RVA: 0x000FB0F3 File Offset: 0x000F92F3
		[Browsable(false)]
		public virtual object DataItem { get; set; }

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x0600504A RID: 20554 RVA: 0x000FB0FC File Offset: 0x000F92FC
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x0600504B RID: 20555 RVA: 0x000FB109 File Offset: 0x000F9309
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x000FB117 File Offset: 0x000F9317
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x000FB128 File Offset: 0x000F9328
		internal static string GetTextFromDataItem(object dataItem, string dataTextField)
		{
			if (!string.IsNullOrEmpty(dataTextField))
			{
				try
				{
					return DataBinder.GetPropertyValue(dataItem, dataTextField, null);
				}
				catch (ArgumentException)
				{
					if (dataItem is DataRowView)
					{
						return "Databound";
					}
					throw;
				}
			}
			return dataItem.ToString();
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x000FB178 File Offset: 0x000F9378
		internal static string GetKeyFromDataItem(object dataItem, string dataKeyField)
		{
			if (!string.IsNullOrEmpty(dataKeyField))
			{
				try
				{
					return DataBinder.GetPropertyValue(dataItem, dataKeyField, null);
				}
				catch (ArgumentException)
				{
					throw new Exception("Field set to DataKeyField property does not exists in the data item");
				}
			}
			return dataItem.ToString();
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x0600504F RID: 20559 RVA: 0x000FB1BC File Offset: 0x000F93BC
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x06005050 RID: 20560 RVA: 0x000FB1C4 File Offset: 0x000F93C4
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x000FB1E4 File Offset: 0x000F93E4
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x000FB202 File Offset: 0x000F9402
		protected virtual object SaveChildViewState()
		{
			return null;
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x000FB205 File Offset: 0x000F9405
		void IStateManager.TrackViewState()
		{
			base.TrackViewState();
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x000FB20D File Offset: 0x000F940D
		public void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x040013E6 RID: 5094
		private IRenderer _renderer;
	}
}
