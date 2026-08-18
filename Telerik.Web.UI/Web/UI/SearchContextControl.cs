using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.SearchBox;
using Telerik.Web.UI.SearchBox.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000875 RID: 2165
	internal class SearchContextControl : DataBoundControl, INamingContainer
	{
		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x06005007 RID: 20487 RVA: 0x000FA858 File Offset: 0x000F8A58
		protected internal SearchContextItemCollection Children
		{
			[DebuggerStepThrough]
			get
			{
				if (this._children == null)
				{
					this._children = new SearchContextItemCollection();
				}
				return this._children;
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x06005008 RID: 20488 RVA: 0x000FA874 File Offset: 0x000F8A74
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

		// Token: 0x06005009 RID: 20489 RVA: 0x000FA8A0 File Offset: 0x000F8AA0
		protected internal SearchContextItem FindFirstAvailableItem()
		{
			foreach (object obj in this.Items)
			{
				SearchContextItem searchContextItem = (SearchContextItem)obj;
				if (searchContextItem.Enabled)
				{
					return searchContextItem;
				}
			}
			return null;
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x000FA904 File Offset: 0x000F8B04
		protected internal virtual IRenderer CreateControlRenderer()
		{
			return new SearchContextRenderer(this);
		}

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x0600500B RID: 20491 RVA: 0x000FA90C File Offset: 0x000F8B0C
		internal bool IsUsingWebServiceBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.WebServiceSettings.Path);
			}
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x0600500C RID: 20492 RVA: 0x000FA921 File Offset: 0x000F8B21
		internal bool IsUsingODataBinding
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataModelID);
			}
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x000FA934 File Offset: 0x000F8B34
		protected internal virtual void Describe(JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new SearchContextItemConverter(),
				new WebServiceSettingsConverter()
			};
			serializer.RegisterConverters(converters);
			if (this.Items.Count > 0)
			{
				dictionary.Add("contextItemData", this.Items);
			}
			if (this.IsUsingWebServiceBinding)
			{
				dictionary.Add("webServiceSettings", serializer.Serialize(this.WebServiceSettings));
			}
			if (this.IsUsingODataBinding)
			{
				dictionary.Add("odataClientSettings", new SearchContextControl.ContextODataClientSetting(this));
			}
			if (!this.Enabled)
			{
				dictionary.Add("enabled", false);
			}
			if (!this.ShowDefaultItem)
			{
				dictionary.Add("showDefaultItem", false);
			}
			int num;
			if (this.IsUsingODataBinding || this.IsUsingWebServiceBinding)
			{
				num = this._CachedSelectedIndex;
			}
			else
			{
				num = this.SelectedIndex;
			}
			dictionary.Add("selectedIndex", num);
			descriptor.AddScriptProperty("searchContextData", serializer.Serialize(dictionary));
			if (!string.IsNullOrEmpty(this.OnClientItemDataBound))
			{
				descriptor.AddEvent("itemDataBound", this.OnClientItemDataBound);
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x0600500E RID: 20494 RVA: 0x000FAA53 File Offset: 0x000F8C53
		[DefaultValue(null)]
		protected internal SearchContextItemCollection Items
		{
			get
			{
				return this.Children;
			}
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x0600500F RID: 20495 RVA: 0x000FAA5B File Offset: 0x000F8C5B
		// (set) Token: 0x06005010 RID: 20496 RVA: 0x000FAA7B File Offset: 0x000F8C7B
		[DefaultValue("")]
		protected internal virtual string DataTextField
		{
			get
			{
				return (string)(this.ViewState["DataTextField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x06005011 RID: 20497 RVA: 0x000FAA8E File Offset: 0x000F8C8E
		// (set) Token: 0x06005012 RID: 20498 RVA: 0x000FAAAE File Offset: 0x000F8CAE
		[DefaultValue("")]
		protected internal virtual string DataKeyField
		{
			get
			{
				return (string)(this.ViewState["DataKeyField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataKeyField"] = value;
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x000FAAC1 File Offset: 0x000F8CC1
		// (set) Token: 0x06005014 RID: 20500 RVA: 0x000FAAE1 File Offset: 0x000F8CE1
		[DefaultValue("")]
		protected internal virtual string DataModelID
		{
			get
			{
				return (string)(this.ViewState["DataModelID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataModelID"] = value;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06005015 RID: 20501 RVA: 0x000FAAF4 File Offset: 0x000F8CF4
		// (set) Token: 0x06005016 RID: 20502 RVA: 0x000FAB15 File Offset: 0x000F8D15
		[DefaultValue(true)]
		protected internal virtual bool ShowDefaultItem
		{
			get
			{
				return (bool)(this.ViewState["ShowDefaultItem"] ?? true);
			}
			set
			{
				this.ViewState["ShowDefaultItem"] = value;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x000FAB2D File Offset: 0x000F8D2D
		// (set) Token: 0x06005018 RID: 20504 RVA: 0x000FAB4D File Offset: 0x000F8D4D
		[DefaultValue("")]
		protected internal string DropDownCssClass
		{
			get
			{
				return ((string)this.ViewState["DropDownCssClass"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DropDownCssClass"] = value;
			}
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x000FAB60 File Offset: 0x000F8D60
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		protected internal WebServiceSettings WebServiceSettings
		{
			get
			{
				if (this._webServiceSettings == null)
				{
					this._webServiceSettings = new WebServiceSettings(this.ViewState);
				}
				return this._webServiceSettings;
			}
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x000FAB81 File Offset: 0x000F8D81
		// (set) Token: 0x0600501B RID: 20507 RVA: 0x000FAB8A File Offset: 0x000F8D8A
		public override Version RenderingCompatibility
		{
			get
			{
				return new Version(3, 5);
			}
			set
			{
			}
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x000FAB8C File Offset: 0x000F8D8C
		// (set) Token: 0x0600501D RID: 20509 RVA: 0x000FABEC File Offset: 0x000F8DEC
		protected internal int SelectedIndex
		{
			get
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						return i;
					}
				}
				if (!this.ShowDefaultItem)
				{
					SearchContextItem searchContextItem = this.FindFirstAvailableItem();
					if (searchContextItem != null)
					{
						searchContextItem.Selected = true;
						return this.Children.IndexOf(searchContextItem);
					}
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					if (this.Items.Count != 0)
					{
						throw new ArgumentOutOfRangeException("value", value, "The index was set to less than -1, or greater than or equal to the number of items on the list at the time the list is rendered.");
					}
					value = -1;
				}
				if ((this.Items.Count != 0 && value < this.Items.Count) || value == -1)
				{
					this.ClearSelection();
					if (value >= 0)
					{
						this.Items[value].Selected = true;
					}
				}
				this._CachedSelectedIndex = value;
			}
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x0600501E RID: 20510 RVA: 0x000FAC65 File Offset: 0x000F8E65
		// (set) Token: 0x0600501F RID: 20511 RVA: 0x000FAC90 File Offset: 0x000F8E90
		[DefaultValue(-1)]
		protected internal int _CachedSelectedIndex
		{
			get
			{
				if (this.ViewState["_CachedSelectedIndex"] == null)
				{
					return -1;
				}
				return (int)this.ViewState["_CachedSelectedIndex"];
			}
			set
			{
				this.ViewState["_CachedSelectedIndex"] = value;
			}
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x06005020 RID: 20512 RVA: 0x000FACA8 File Offset: 0x000F8EA8
		protected internal virtual SearchContextItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0)
				{
					return this.Items[selectedIndex];
				}
				return null;
			}
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x000FACD0 File Offset: 0x000F8ED0
		protected internal void ClearSelection()
		{
			foreach (object obj in this.Items)
			{
				SearchContextItem searchContextItem = (SearchContextItem)obj;
				searchContextItem.Selected = false;
			}
		}

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x06005022 RID: 20514 RVA: 0x000FAD2C File Offset: 0x000F8F2C
		// (remove) Token: 0x06005023 RID: 20515 RVA: 0x000FAD3F File Offset: 0x000F8F3F
		protected internal event SearchBoxContextItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(SearchContextControl.ItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(SearchContextControl.ItemDataBoundEvent, value);
			}
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x000FAD52 File Offset: 0x000F8F52
		protected internal void OnItemDataBound(SearchBoxContextItemEventArgs e)
		{
			this.RaiseEvent(SearchContextControl.ItemDataBoundEvent, e);
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x000FAD60 File Offset: 0x000F8F60
		private void RaiseEvent(object eventKey, SearchBoxContextItemEventArgs e)
		{
			SearchBoxContextItemEventHandler searchBoxContextItemEventHandler = (SearchBoxContextItemEventHandler)base.Events[eventKey];
			if (searchBoxContextItemEventHandler != null)
			{
				searchBoxContextItemEventHandler(this, e);
			}
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x06005026 RID: 20518 RVA: 0x000FAD8A File Offset: 0x000F8F8A
		// (set) Token: 0x06005027 RID: 20519 RVA: 0x000FADAA File Offset: 0x000F8FAA
		protected internal string OnClientItemDataBound
		{
			get
			{
				return (string)(this.ViewState["OnClientItemDataBound"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientItemDataBound"] = value;
			}
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x000FADC0 File Offset: 0x000F8FC0
		protected override void PerformDataBinding(IEnumerable data)
		{
			this.PrepareForDataBinding();
			foreach (object dataItem in data)
			{
				string textFromDataItem = SearchContextItem.GetTextFromDataItem(dataItem, this.DataTextField);
				string textFromDataItem2 = SearchContextItem.GetTextFromDataItem(dataItem, this.DataKeyField);
				SearchContextItem searchContextItem = new SearchContextItem(textFromDataItem, textFromDataItem2, dataItem);
				SearchBoxContextItemEventArgs e = new SearchBoxContextItemEventArgs(searchContextItem);
				this.Children.Add(searchContextItem);
				this.OnItemDataBound(e);
				searchContextItem.DataItem = null;
			}
			if (this._CachedSelectedIndex != -1)
			{
				this.SelectedIndex = this._CachedSelectedIndex;
			}
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x000FAE70 File Offset: 0x000F9070
		protected void PrepareForDataBinding()
		{
			this.Children.Clear();
			base.ClearChildViewState();
			this.TrackViewState();
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x000FAE89 File Offset: 0x000F9089
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x000FAE96 File Offset: 0x000F9096
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x000FAEA4 File Offset: 0x000F90A4
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x000FAEAD File Offset: 0x000F90AD
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x000FAEBC File Offset: 0x000F90BC
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Children.Clear();
				return;
			}
			((IStateManager)this.Children).LoadViewState(array[1]);
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x000FAEF8 File Offset: 0x000F90F8
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Children).SaveViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x000FAF32 File Offset: 0x000F9132
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Children).TrackViewState();
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x000FAF45 File Offset: 0x000F9145
		// Note: this type is marked as 'beforefieldinit'.
		static SearchContextControl()
		{
			SearchContextControl.ItemDataBoundEvent = new object();
		}

		// Token: 0x040013DD RID: 5085
		private WebServiceSettings _webServiceSettings;

		// Token: 0x040013DE RID: 5086
		private SearchContextItemCollection _children;

		// Token: 0x040013DF RID: 5087
		protected internal string DefaultItemText = "All";

		// Token: 0x040013E0 RID: 5088
		protected internal string LoadingItemsMessage = "Loading";

		// Token: 0x040013E1 RID: 5089
		private IRenderer _renderer;

		// Token: 0x02000876 RID: 2166
		private class ContextODataClientSetting
		{
			// Token: 0x17001A3F RID: 6719
			// (get) Token: 0x06005033 RID: 20531 RVA: 0x000FAF6F File Offset: 0x000F916F
			// (set) Token: 0x06005034 RID: 20532 RVA: 0x000FAF77 File Offset: 0x000F9177
			public string DataModelID { get; set; }

			// Token: 0x17001A40 RID: 6720
			// (get) Token: 0x06005035 RID: 20533 RVA: 0x000FAF80 File Offset: 0x000F9180
			// (set) Token: 0x06005036 RID: 20534 RVA: 0x000FAF88 File Offset: 0x000F9188
			public string DataTextField { get; set; }

			// Token: 0x17001A41 RID: 6721
			// (get) Token: 0x06005037 RID: 20535 RVA: 0x000FAF91 File Offset: 0x000F9191
			// (set) Token: 0x06005038 RID: 20536 RVA: 0x000FAF99 File Offset: 0x000F9199
			public string DataKeyField { get; set; }

			// Token: 0x06005039 RID: 20537 RVA: 0x000FAFA2 File Offset: 0x000F91A2
			public ContextODataClientSetting(SearchContextControl control)
			{
				this.DataModelID = control.DataModelID;
				this.DataTextField = control.DataTextField;
				this.DataKeyField = control.DataKeyField;
			}
		}
	}
}
