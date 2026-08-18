using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200016F RID: 367
	[Designer(typeof(ReorderListDesigner))]
	[ToolboxBitmap(typeof(Accessor), "ReorderList.bmp")]
	public class ReorderList : CompositeDataBoundControl, IRepeatInfoUser, INamingContainer, ICallbackEventHandler, IPostBackEventHandler
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060009D4 RID: 2516 RVA: 0x00018F74 File Offset: 0x00017174
		// (remove) Token: 0x060009D5 RID: 2517 RVA: 0x00018F87 File Offset: 0x00017187
		public event EventHandler<ReorderListCommandEventArgs> ItemCommand
		{
			add
			{
				base.Events.AddHandler(ReorderList.ItemCommandKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.ItemCommandKey, value);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060009D6 RID: 2518 RVA: 0x00018F9A File Offset: 0x0001719A
		// (remove) Token: 0x060009D7 RID: 2519 RVA: 0x00018FAD File Offset: 0x000171AD
		public event EventHandler<ReorderListCommandEventArgs> CancelCommand
		{
			add
			{
				base.Events.AddHandler(ReorderList.CancelCommandKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.CancelCommandKey, value);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060009D8 RID: 2520 RVA: 0x00018FC0 File Offset: 0x000171C0
		// (remove) Token: 0x060009D9 RID: 2521 RVA: 0x00018FD3 File Offset: 0x000171D3
		public event EventHandler<ReorderListCommandEventArgs> DeleteCommand
		{
			add
			{
				base.Events.AddHandler(ReorderList.DeleteCommandKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.DeleteCommandKey, value);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060009DA RID: 2522 RVA: 0x00018FE6 File Offset: 0x000171E6
		// (remove) Token: 0x060009DB RID: 2523 RVA: 0x00018FF9 File Offset: 0x000171F9
		public event EventHandler<ReorderListCommandEventArgs> EditCommand
		{
			add
			{
				base.Events.AddHandler(ReorderList.EditCommandKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.EditCommandKey, value);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060009DC RID: 2524 RVA: 0x0001900C File Offset: 0x0001720C
		// (remove) Token: 0x060009DD RID: 2525 RVA: 0x0001901F File Offset: 0x0001721F
		public event EventHandler<ReorderListCommandEventArgs> InsertCommand
		{
			add
			{
				base.Events.AddHandler(ReorderList.InsertCommandKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.InsertCommandKey, value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060009DE RID: 2526 RVA: 0x00019032 File Offset: 0x00017232
		// (remove) Token: 0x060009DF RID: 2527 RVA: 0x00019045 File Offset: 0x00017245
		public event EventHandler<ReorderListCommandEventArgs> UpdateCommand
		{
			add
			{
				base.Events.AddHandler(ReorderList.UpdateCommandKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.UpdateCommandKey, value);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060009E0 RID: 2528 RVA: 0x00019058 File Offset: 0x00017258
		// (remove) Token: 0x060009E1 RID: 2529 RVA: 0x0001906B File Offset: 0x0001726B
		public event EventHandler<ReorderListItemEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(ReorderList.ItemDataBoundKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.ItemDataBoundKey, value);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060009E2 RID: 2530 RVA: 0x0001907E File Offset: 0x0001727E
		// (remove) Token: 0x060009E3 RID: 2531 RVA: 0x00019091 File Offset: 0x00017291
		public event EventHandler<ReorderListItemEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(ReorderList.ItemCreatedKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.ItemCreatedKey, value);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060009E4 RID: 2532 RVA: 0x000190A4 File Offset: 0x000172A4
		// (remove) Token: 0x060009E5 RID: 2533 RVA: 0x000190B7 File Offset: 0x000172B7
		public event EventHandler<ReorderListItemReorderEventArgs> ItemReorder
		{
			add
			{
				base.Events.AddHandler(ReorderList.ItemReorderKey, value);
			}
			remove
			{
				base.Events.RemoveHandler(ReorderList.ItemReorderKey, value);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x000190CA File Offset: 0x000172CA
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x000190D8 File Offset: 0x000172D8
		[DefaultValue(false)]
		public bool AllowReorder
		{
			get
			{
				return this.GetPropertyValue<bool>("AllowReorder", true);
			}
			set
			{
				this.SetPropertyValue<bool>("AllowReorder", value);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000190E8 File Offset: 0x000172E8
		private IOrderedDictionary BoundFieldValues
		{
			get
			{
				if (this.ViewState["BoundFieldValues"] == null)
				{
					OrderedDictionary value = new OrderedDictionary();
					this.ViewState["BoundFieldValues"] = value;
				}
				return (IOrderedDictionary)this.ViewState["BoundFieldValues"];
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00019133 File Offset: 0x00017333
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00019145 File Offset: 0x00017345
		[DefaultValue("")]
		public string CallbackCssStyle
		{
			get
			{
				return this.GetPropertyValue<string>("CallbackCssStyle", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("CallbackCssStyle", value);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00019154 File Offset: 0x00017354
		internal BulletedList ChildList
		{
			get
			{
				if (this._childList == null)
				{
					this._childList = new BulletedList();
					this._childList.ID = "_rbl";
					this.Controls.Add(this._childList);
				}
				else if (this._childList.Parent == null)
				{
					this.Controls.Add(this._childList);
				}
				return this._childList;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000191BB File Offset: 0x000173BB
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x000191CD File Offset: 0x000173CD
		[DefaultValue("")]
		public string DataKeyField
		{
			get
			{
				return this.GetPropertyValue<string>("DataKeyName", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("DataKeyName", value);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000191DB File Offset: 0x000173DB
		[Browsable(false)]
		public DataKeyCollection DataKeys
		{
			get
			{
				return new DataKeyCollection(this.DataKeysArray);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000191E8 File Offset: 0x000173E8
		private bool DataBindPending
		{
			get
			{
				this.EnsureChildControls();
				if (this._dropWatcherExtender != null)
				{
					string clientState = this._dropWatcherExtender.ClientState;
					return !string.IsNullOrEmpty(clientState);
				}
				return false;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001921A File Offset: 0x0001741A
		protected ArrayList DataKeysArray
		{
			get
			{
				if (this.ViewState["DataKeysArray"] == null)
				{
					this.ViewState["DataKeysArray"] = new ArrayList();
				}
				return (ArrayList)this.ViewState["DataKeysArray"];
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00019258 File Offset: 0x00017458
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00019260 File Offset: 0x00017460
		[TypeConverter(typeof(TypedControlIDConverter<IDataSource>))]
		public override string DataSourceID
		{
			get
			{
				return base.DataSourceID;
			}
			set
			{
				base.DataSourceID = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00019269 File Offset: 0x00017469
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x00019277 File Offset: 0x00017477
		[DefaultValue(ReorderHandleAlignment.Left)]
		public ReorderHandleAlignment DragHandleAlignment
		{
			get
			{
				return this.GetPropertyValue<ReorderHandleAlignment>("DragHandleAlignment", ReorderHandleAlignment.Left);
			}
			set
			{
				this.SetPropertyValue<ReorderHandleAlignment>("DragHandleAlignment", value);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00019285 File Offset: 0x00017485
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x0001928D File Offset: 0x0001748D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(ReorderListItem))]
		[DefaultValue("")]
		public ITemplate DragHandleTemplate
		{
			get
			{
				return this._dragHandleTemplate;
			}
			set
			{
				this._dragHandleTemplate = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00019296 File Offset: 0x00017496
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0001929E File Offset: 0x0001749E
		[Browsable(false)]
		[TemplateContainer(typeof(ReorderListItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public ITemplate EmptyListTemplate
		{
			get
			{
				return this._emptyListTemplate;
			}
			set
			{
				this._emptyListTemplate = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x000192A7 File Offset: 0x000174A7
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x000192B5 File Offset: 0x000174B5
		[DefaultValue(-1)]
		public int EditItemIndex
		{
			get
			{
				return this.GetPropertyValue<int>("EditItemIndex", -1);
			}
			set
			{
				this.SetPropertyValue<int>("EditItemIndex", value);
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x000192C3 File Offset: 0x000174C3
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x000192CB File Offset: 0x000174CB
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public ITemplate EditItemTemplate
		{
			get
			{
				return this._editItemTemplate;
			}
			set
			{
				this._editItemTemplate = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x000192D4 File Offset: 0x000174D4
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x000192E2 File Offset: 0x000174E2
		[DefaultValue(ReorderListInsertLocation.Beginning)]
		public ReorderListInsertLocation ItemInsertLocation
		{
			get
			{
				return this.GetPropertyValue<ReorderListInsertLocation>("ItemInsertLocation", ReorderListInsertLocation.Beginning);
			}
			set
			{
				this.SetPropertyValue<ReorderListInsertLocation>("ItemInsertLocation", value);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000192F0 File Offset: 0x000174F0
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x000192F8 File Offset: 0x000174F8
		[Browsable(false)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public ITemplate InsertItemTemplate
		{
			get
			{
				return this._insertItemTemplate;
			}
			set
			{
				this._insertItemTemplate = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00019301 File Offset: 0x00017501
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00019309 File Offset: 0x00017509
		[Browsable(false)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		public ITemplate ItemTemplate
		{
			get
			{
				return this._itemTemplate;
			}
			set
			{
				this._itemTemplate = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00019312 File Offset: 0x00017512
		[Browsable(false)]
		public ReorderListItemCollection Items
		{
			get
			{
				this.EnsureDataBound();
				return new ReorderListItemCollection(this);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00019320 File Offset: 0x00017520
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00019328 File Offset: 0x00017528
		[DefaultValue(ReorderListItemLayoutType.Table)]
		public ReorderListItemLayoutType LayoutType
		{
			get
			{
				return this._layoutType;
			}
			set
			{
				this._layoutType = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00019331 File Offset: 0x00017531
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0001933F File Offset: 0x0001753F
		[DefaultValue("true")]
		public bool PostBackOnReorder
		{
			get
			{
				return this.GetPropertyValue<bool>("PostBackOnReorder", false);
			}
			set
			{
				this.SetPropertyValue<bool>("PostBackOnReorder", value);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0001934D File Offset: 0x0001754D
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0001935F File Offset: 0x0001755F
		[DefaultValue("")]
		public string SortOrderField
		{
			get
			{
				return this.GetPropertyValue<string>("SortOrderField", string.Empty);
			}
			set
			{
				this.SetPropertyValue<string>("SortOrderField", value);
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001936D File Offset: 0x0001756D
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00019375 File Offset: 0x00017575
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[TemplateContainer(typeof(ReorderListItem))]
		[DefaultValue("")]
		[Browsable(false)]
		public ITemplate ReorderTemplate
		{
			get
			{
				return this._reorderTemplate;
			}
			set
			{
				this._reorderTemplate = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0001937E File Offset: 0x0001757E
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00019397 File Offset: 0x00017597
		[DefaultValue(false)]
		public bool ShowInsertItem
		{
			get
			{
				return this.GetPropertyValue<bool>("ShowInsertItem", this.InsertItemTemplate != null);
			}
			set
			{
				this.SetPropertyValue<bool>("ShowInsertItem", value);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x000193A5 File Offset: 0x000175A5
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000193C4 File Offset: 0x000175C4
		private static IDictionary CopyDictionary(IDictionary source, IDictionary dest)
		{
			if (dest == null)
			{
				dest = new OrderedDictionary(source.Count);
			}
			foreach (object obj in source)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				dest[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return dest;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00019438 File Offset: 0x00017638
		private void ClearChildren()
		{
			this.ChildList.Controls.Clear();
			this._dropTemplateControl = null;
			if (this._draggableItems != null)
			{
				foreach (ReorderList.DraggableListItemInfo draggableListItemInfo in this._draggableItems)
				{
					if (draggableListItemInfo.Extender != null)
					{
						draggableListItemInfo.Extender.Dispose();
					}
				}
			}
			this._draggableItems = null;
			for (int i = this.Controls.Count - 1; i >= 0; i--)
			{
				if (this.Controls[i] is DropWatcherExtender)
				{
					this.Controls[i].Dispose();
				}
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000194FC File Offset: 0x000176FC
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.ClearChildren();
			int num = 0;
			ArrayList dataKeysArray = this.DataKeysArray;
			this.itemsArray = new ArrayList();
			int num2 = base.DesignMode ? 1 : 0;
			if (dataBinding)
			{
				dataKeysArray.Clear();
				ICollection collection = dataSource as ICollection;
				if (collection != null)
				{
					dataKeysArray.Capacity = collection.Count;
					this.itemsArray.Capacity = collection.Count;
				}
			}
			if (dataSource != null)
			{
				string dataKeyField = this.DataKeyField;
				bool flag = dataBinding && !string.IsNullOrEmpty(dataKeyField);
				bool hasDragHandle = this.AllowReorder && this.DragHandleTemplate != null;
				num2 = 0;
				int num3 = 0;
				foreach (object obj in dataSource)
				{
					if (flag)
					{
						dataKeysArray.Add(DataBinder.GetPropertyValue(obj, dataKeyField));
					}
					ListItemType itemType = ListItemType.Item;
					if (num3 == this.EditItemIndex)
					{
						itemType = ListItemType.EditItem;
					}
					this.CreateItem(num3, dataBinding, obj, itemType, hasDragHandle);
					num2++;
					num3++;
				}
				if (this.ShowInsertItem && this.InsertItemTemplate != null)
				{
					this.CreateInsertItem(num3);
					num++;
				}
			}
			if (this.AllowReorder && num2 > 1 && this._draggableItems != null)
			{
				foreach (ReorderList.DraggableListItemInfo draggableListItemInfo in this._draggableItems)
				{
					draggableListItemInfo.Extender = new DraggableListItemExtender();
					draggableListItemInfo.Extender.TargetControlID = draggableListItemInfo.TargetControl.ID;
					draggableListItemInfo.Extender.Handle = draggableListItemInfo.HandleControl.ClientID;
					draggableListItemInfo.Extender.ID = string.Format(CultureInfo.InvariantCulture, "{0}_{1}", new object[]
					{
						this.ID,
						draggableListItemInfo.Extender.TargetControlID
					});
					this.Controls.Add(draggableListItemInfo.Extender);
				}
				Control control;
				Control control2;
				this.GetDropTemplateControl(out control, out control2);
				this._dropWatcherExtender = new DropWatcherExtender();
				this._dropWatcherExtender.ArgReplaceString = "_~Arg~_";
				this._dropWatcherExtender.CallbackCssStyle = this.CallbackCssStyle;
				this._dropWatcherExtender.DropLayoutElement = control.ID;
				if (this.PostBackOnReorder)
				{
					this._dropWatcherExtender.PostBackCode = this.Page.ClientScript.GetPostBackEventReference(this, "_~Arg~_");
				}
				else
				{
					this._dropWatcherExtender.PostBackCode = this.Page.ClientScript.GetCallbackEventReference(this, "'_~Arg~_'", "_~Success~_", "'_~Context~_'", "_~Error~_", true);
					this._dropWatcherExtender.ArgContextString = "_~Context~_";
					this._dropWatcherExtender.ArgSuccessString = "_~Success~_";
					this._dropWatcherExtender.ArgErrorString = "_~Error~_";
				}
				this._dropWatcherExtender.EnableClientState = !this.PostBackOnReorder;
				this._dropWatcherExtender.BehaviorID = this.UniqueID + "_dItemEx";
				this._dropWatcherExtender.TargetControlID = this.ChildList.ID;
				this.Controls.Add(this._dropWatcherExtender);
			}
			return this.ChildList.Controls.Count - num;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00019868 File Offset: 0x00017A68
		private Control CreateReorderArea(int index, string reorderKey)
		{
			Panel panel = new Panel();
			panel.ID = string.Format(CultureInfo.InvariantCulture, "__drop{1}{0}", new object[]
			{
				index,
				reorderKey
			});
			if (this.ReorderTemplate != null)
			{
				this.ReorderTemplate.InstantiateIn(panel);
			}
			return panel;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000198BC File Offset: 0x00017ABC
		protected virtual ReorderListItem CreateInsertItem(int index)
		{
			if (this.InsertItemTemplate != null && this.ShowInsertItem)
			{
				ReorderListItem reorderListItem = new ReorderListItem(index, true);
				this.InsertItemTemplate.InstantiateIn(reorderListItem);
				this.ChildList.Controls.Add(reorderListItem);
				return reorderListItem;
			}
			return null;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00019904 File Offset: 0x00017B04
		protected virtual void CreateDragHandle(ReorderListItem item)
		{
			if (!this.AllowReorder)
			{
				return;
			}
			Control control2;
			if (this.DragHandleTemplate != null)
			{
				Control control;
				Control dest;
				if (this.LayoutType == ReorderListItemLayoutType.User)
				{
					control = new Panel();
					Panel panel = new Panel();
					Panel panel2 = new Panel();
					control2 = panel2;
					dest = panel;
					if (this.DragHandleAlignment == ReorderHandleAlignment.Left || this.DragHandleAlignment == ReorderHandleAlignment.Top)
					{
						control.Controls.Add(panel2);
						control.Controls.Add(panel);
					}
					else
					{
						control.Controls.Add(panel);
						control.Controls.Add(panel2);
					}
				}
				else
				{
					Table table = new Table();
					control = table;
					table.BorderWidth = 0;
					table.Style.Add("border-spacing", "0 0");
					TableCell tableCell = new TableCell();
					dest = tableCell;
					tableCell.Width = new Unit(100.0, UnitType.Percentage);
					tableCell.Style.Add("padding", "0");
					TableCell tableCell2 = new TableCell();
					tableCell2.Style.Add("padding", "0");
					control2 = tableCell2;
					switch (this.DragHandleAlignment)
					{
					case ReorderHandleAlignment.Top:
					case ReorderHandleAlignment.Bottom:
					{
						TableRow tableRow = new TableRow();
						TableRow tableRow2 = new TableRow();
						tableRow.Cells.Add(tableCell);
						tableRow2.Cells.Add(tableCell2);
						if (this.DragHandleAlignment == ReorderHandleAlignment.Top)
						{
							table.Rows.Add(tableRow2);
							table.Rows.Add(tableRow);
						}
						else
						{
							table.Rows.Add(tableRow);
							table.Rows.Add(tableRow2);
						}
						break;
					}
					case ReorderHandleAlignment.Left:
					case ReorderHandleAlignment.Right:
					{
						TableRow tableRow3 = new TableRow();
						if (this.DragHandleAlignment == ReorderHandleAlignment.Left)
						{
							tableRow3.Cells.Add(tableCell2);
							tableRow3.Cells.Add(tableCell);
						}
						else
						{
							tableRow3.Cells.Add(tableCell);
							tableRow3.Cells.Add(tableCell2);
						}
						table.Rows.Add(tableRow3);
						break;
					}
					}
				}
				ReorderList.MoveChildren(item, dest);
				ReorderListItem reorderListItem = new ReorderListItem(item, HtmlTextWriterTag.Div);
				this.DragHandleTemplate.InstantiateIn(reorderListItem);
				control2.Controls.Add(reorderListItem);
				item.Controls.Add(control);
			}
			else
			{
				Panel panel3 = new Panel();
				ReorderList.MoveChildren(item, panel3);
				control2 = panel3;
				item.Controls.Add(panel3);
			}
			control2.ID = string.Format(CultureInfo.InvariantCulture, "__dih{0}", new object[]
			{
				item.ItemIndex
			});
			if (this._draggableItems == null)
			{
				this._draggableItems = new List<ReorderList.DraggableListItemInfo>();
			}
			ReorderList.DraggableListItemInfo draggableListItemInfo = new ReorderList.DraggableListItemInfo();
			draggableListItemInfo.TargetControl = item;
			draggableListItemInfo.HandleControl = control2;
			this._draggableItems.Add(draggableListItemInfo);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00019BD0 File Offset: 0x00017DD0
		protected virtual ReorderListItem CreateItem(int index, bool dataBind, object dataItem, ListItemType itemType, bool hasDragHandle)
		{
			if (itemType != ListItemType.Item && itemType != ListItemType.EditItem && itemType != ListItemType.Separator)
			{
				throw new ArgumentException("Unknown value", "itemType");
			}
			ReorderListItem reorderListItem = new ReorderListItem(dataItem, index, itemType);
			reorderListItem.ClientIDMode = ClientIDMode.AutoID;
			this.OnItemCreated(new ReorderListItemEventArgs(reorderListItem));
			ITemplate template = this.ItemTemplate;
			if (index == this.EditItemIndex)
			{
				template = this.EditItemTemplate;
			}
			if (itemType == ListItemType.Separator)
			{
				template = this.ReorderTemplate;
			}
			if (template != null)
			{
				template.InstantiateIn(reorderListItem);
			}
			if (itemType == ListItemType.Item && template == null && dataItem != null && this.DataSource is IList)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(dataItem);
				if (converter != null)
				{
					Label label = new Label();
					label.Text = converter.ConvertToString(null, CultureInfo.CurrentUICulture, dataItem);
					reorderListItem.Controls.Add(label);
				}
			}
			this.CreateDragHandle(reorderListItem);
			this.ChildList.Controls.Add(reorderListItem);
			if (dataBind)
			{
				reorderListItem.DataBind();
				this.OnItemDataBound(new ReorderListItemEventArgs(reorderListItem));
				reorderListItem.DataItem = null;
			}
			return reorderListItem;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00019D20 File Offset: 0x00017F20
		protected virtual bool DoReorder(int oldIndex, int newIndex)
		{
			if (base.IsBoundUsingDataSourceID && this.SortOrderField != null)
			{
				DataSourceView dsv = this.GetData();
				EventWaitHandle w = new EventWaitHandle(false, EventResetMode.AutoReset);
				bool success = false;
				base.RequiresDataBinding = true;
				try
				{
					dsv.Select(new DataSourceSelectArguments(), delegate(IEnumerable dataSource)
					{
						success = this.DoReorderInternal(dataSource, oldIndex, newIndex, dsv);
						w.Set();
					});
					w.WaitOne();
				}
				catch (Exception ex)
				{
					this.CallbackResult = ex.Message;
					throw;
				}
				return success;
			}
			if (this.DataSource is DataTable || this.DataSource is DataView)
			{
				DataTable dataTable = this.DataSource as DataTable;
				if (dataTable == null)
				{
					dataTable = ((DataView)this.DataSource).Table;
				}
				return this.DoReorderInternal(dataTable, oldIndex, newIndex);
			}
			if (this.DataSource is IList && !((IList)this.DataSource).IsReadOnly)
			{
				IList list = (IList)this.DataSource;
				object value = list[oldIndex];
				if (oldIndex > newIndex)
				{
					for (int i = oldIndex; i > newIndex; i--)
					{
						list[i] = list[i - 1];
					}
				}
				else
				{
					for (int j = oldIndex; j < newIndex; j++)
					{
						list[j] = list[j + 1];
					}
				}
				list[newIndex] = value;
				return true;
			}
			return false;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00019F04 File Offset: 0x00018104
		private bool DoReorderInternal(DataTable dataSource, int oldIndex, int newIndex)
		{
			if (string.IsNullOrEmpty(this.SortOrderField))
			{
				return false;
			}
			int num = Math.Min(oldIndex, newIndex);
			int num2 = Math.Max(oldIndex, newIndex);
			string filterExpression = string.Format(CultureInfo.InvariantCulture, "{0} >= {1} AND {0} <= {2}", new object[]
			{
				this.SortOrderField,
				num,
				num2
			});
			DataRow[] array = dataSource.Select(filterExpression, this.SortOrderField + " ASC");
			DataColumn column = dataSource.Columns[this.SortOrderField];
			object value = array[newIndex - num][column];
			if (oldIndex > newIndex)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					array[i][column] = array[i + 1][column];
				}
			}
			else
			{
				for (int j = array.Length - 1; j > 0; j--)
				{
					array[j][column] = array[j - 1][column];
				}
			}
			array[oldIndex - num][column] = value;
			dataSource.AcceptChanges();
			return true;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001A028 File Offset: 0x00018228
		private bool DoReorderInternal(IEnumerable dataSource, int oldIndex, int newIndex, DataSourceView dsv)
		{
			string sortOrderField = this.SortOrderField;
			List<IOrderedDictionary> list = new List<IOrderedDictionary>(Math.Abs(oldIndex - newIndex));
			int num = Math.Min(oldIndex, newIndex);
			int num2 = Math.Max(oldIndex, newIndex);
			if (num == num2)
			{
				return false;
			}
			int num3 = 0;
			foreach (object component in dataSource)
			{
				try
				{
					if (num3 >= num)
					{
						if (num3 > num2)
						{
							break;
						}
						OrderedDictionary orderedDictionary = new OrderedDictionary();
						Hashtable hashtable = new Hashtable();
						PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
						foreach (object obj in properties)
						{
							PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
							object obj2 = propertyDescriptor.GetValue(component);
							if (propertyDescriptor.PropertyType.IsValueType && obj2 == DBNull.Value)
							{
								obj2 = null;
							}
							orderedDictionary[propertyDescriptor.Name] = obj2;
							if (propertyDescriptor.Name == this.DataKeyField)
							{
								hashtable[propertyDescriptor.Name] = orderedDictionary[propertyDescriptor.Name];
								orderedDictionary.Remove(propertyDescriptor.Name);
							}
						}
						orderedDictionary[ReorderList.KeysKey] = hashtable;
						list.Add(orderedDictionary);
					}
				}
				finally
				{
					num3++;
				}
			}
			oldIndex -= num;
			newIndex -= num;
			int num4 = int.MinValue;
			if (list.Count > 0 && list[0].Contains(sortOrderField))
			{
				object obj3 = list[0][sortOrderField];
				string s;
				if (obj3 is int)
				{
					num4 = (int)obj3;
				}
				else if ((s = (obj3 as string)) != null)
				{
					if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4))
					{
						return false;
					}
				}
				else
				{
					if (obj3 != null && obj3.GetType().IsValueType && obj3.GetType().IsPrimitive)
					{
						num4 = Convert.ToInt32(obj3, CultureInfo.InvariantCulture);
						return true;
					}
					return false;
				}
				if (num4 == -2147483648)
				{
					num4 = 0;
				}
				IOrderedDictionary item = list[oldIndex];
				list.RemoveAt(oldIndex);
				list.Insert(newIndex, item);
				foreach (IOrderedDictionary orderedDictionary2 in list)
				{
					IDictionary keys = (IDictionary)orderedDictionary2[ReorderList.KeysKey];
					orderedDictionary2.Remove(ReorderList.KeysKey);
					IDictionary oldValues = ReorderList.CopyDictionary(orderedDictionary2, null);
					orderedDictionary2[sortOrderField] = num4++;
					dsv.Update(keys, orderedDictionary2, oldValues, delegate(int rowsAffected, Exception ex)
					{
						if (ex != null)
						{
							throw new Exception("Failed to reorder.", ex);
						}
						return true;
					});
				}
				return true;
			}
			throw new InvalidOperationException("Couldn't find sort field '" + this.SortOrderField + "' in bound data.");
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001A370 File Offset: 0x00018570
		protected override void OnPreRender(EventArgs e)
		{
			if (this.DataBindPending)
			{
				base.RequiresDataBinding = true;
			}
			base.OnPreRender(e);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001A388 File Offset: 0x00018588
		private void ExtractRowValues(IOrderedDictionary fieldValues, ReorderListItem item, bool includePrimaryKey, bool isAddOperation)
		{
			if (fieldValues == null)
			{
				return;
			}
			IBindableTemplate bindableTemplate = this.ItemTemplate as IBindableTemplate;
			if (!isAddOperation)
			{
				ListItemType itemType = item.ItemType;
				if (itemType != ListItemType.Item)
				{
					if (itemType != ListItemType.EditItem)
					{
						return;
					}
					bindableTemplate = (this.EditItemTemplate as IBindableTemplate);
				}
			}
			else
			{
				bindableTemplate = (this.InsertItemTemplate as IBindableTemplate);
			}
			if (bindableTemplate != null)
			{
				string dataKeyField = this.DataKeyField;
				IOrderedDictionary orderedDictionary = bindableTemplate.ExtractValues(item);
				foreach (object obj in orderedDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (includePrimaryKey || string.Compare((string)dictionaryEntry.Key, dataKeyField, StringComparison.OrdinalIgnoreCase) != 0)
					{
						fieldValues[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001A45C File Offset: 0x0001865C
		protected WebControl GetDropTemplateControl(out Control dropItem, out Control emptyItem)
		{
			dropItem = null;
			emptyItem = null;
			if (!this.AllowReorder || base.DesignMode)
			{
				return null;
			}
			if (this._dropTemplateControl == null)
			{
				BulletedList bulletedList = new BulletedList();
				bulletedList.Style["visibility"] = "hidden";
				bulletedList.Style["display"] = "none";
				BulletedListItem bulletedListItem = new BulletedListItem();
				bulletedListItem.ID = "_dat";
				bulletedListItem.Style["vertical-align"] = "middle";
				if (this.ReorderTemplate == null)
				{
					bulletedListItem.Style["border"] = "1px solid black";
				}
				else
				{
					this.ReorderTemplate.InstantiateIn(bulletedListItem);
				}
				dropItem = bulletedListItem;
				bulletedList.Controls.Add(bulletedListItem);
				this._dropTemplateControl = bulletedList;
				this.Controls.Add(bulletedList);
			}
			else
			{
				dropItem = this._dropTemplateControl.FindControl("_dat");
				emptyItem = null;
			}
			return (WebControl)this._dropTemplateControl;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001A618 File Offset: 0x00018818
		private int GetNewItemSortValue(out bool success)
		{
			DataSourceView data = this.GetData();
			EventWaitHandle w = new EventWaitHandle(false, EventResetMode.AutoReset);
			int newIndex = 0;
			bool bSuccess = false;
			data.Select(new DataSourceSelectArguments(), delegate(IEnumerable dataSource)
			{
				try
				{
					IList list = dataSource as IList;
					if (list != null)
					{
						if (list.Count == 0)
						{
							bSuccess = true;
						}
						else
						{
							int num = 1;
							object component;
							if (this.ItemInsertLocation == ReorderListInsertLocation.End)
							{
								component = list[list.Count - 1];
							}
							else
							{
								component = list[0];
								num = -1;
							}
							PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[this.SortOrderField];
							if (propertyDescriptor != null)
							{
								object value = propertyDescriptor.GetValue(component);
								if (value is int)
								{
									newIndex = (int)value + num;
									bSuccess = true;
								}
							}
						}
					}
				}
				finally
				{
					w.Set();
				}
			});
			w.WaitOne();
			success = bSuccess;
			return newIndex;
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001A685 File Offset: 0x00018885
		private void HandleCancel(ReorderListCommandEventArgs e)
		{
			if (base.IsBoundUsingDataSourceID)
			{
				this.EditItemIndex = -1;
				base.RequiresDataBinding = true;
			}
			this.OnCancelCommand(e);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001A6C8 File Offset: 0x000188C8
		private void HandleDelete(ReorderListCommandEventArgs e)
		{
			if (base.IsBoundUsingDataSourceID)
			{
				DataSourceView data = this.GetData();
				if (data != null)
				{
					IDictionary oldValues;
					IOrderedDictionary orderedDictionary;
					IDictionary keys;
					this.PrepareRowValues(e, out oldValues, out orderedDictionary, out keys);
					data.Delete(keys, oldValues, delegate(int rows, Exception ex)
					{
						if (ex != null)
						{
							return false;
						}
						this.OnDeleteCommand(e);
						return true;
					});
					return;
				}
			}
			this.OnDeleteCommand(e);
			base.RequiresDataBinding = true;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001A746 File Offset: 0x00018946
		private void HandleEdit(ReorderListCommandEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item)
			{
				this.EditItemIndex = e.Item.ItemIndex;
				base.RequiresDataBinding = true;
			}
			this.OnEditCommand(e);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001A798 File Offset: 0x00018998
		private void HandleInsert(ReorderListCommandEventArgs e)
		{
			if (base.IsBoundUsingDataSourceID && this.SortOrderField != null)
			{
				IDictionary dictionary;
				IOrderedDictionary orderedDictionary;
				IDictionary dictionary2;
				this.PrepareRowValues(e, out dictionary, out orderedDictionary, out dictionary2, true);
				DataSourceView data = this.GetData();
				bool flag;
				int newItemSortValue = this.GetNewItemSortValue(out flag);
				if (flag)
				{
					orderedDictionary[this.SortOrderField] = newItemSortValue;
				}
				if (data != null)
				{
					data.Insert(orderedDictionary, delegate(int rows, Exception ex)
					{
						if (ex != null)
						{
							return false;
						}
						this.OnInsertCommand(e);
						return true;
					});
					return;
				}
			}
			this.OnInsertCommand(e);
			base.RequiresDataBinding = true;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001A86C File Offset: 0x00018A6C
		private void HandleUpdate(ReorderListCommandEventArgs e, int itemIndex)
		{
			if (base.IsBoundUsingDataSourceID)
			{
				if (e == null && itemIndex != -1)
				{
					e = new ReorderListCommandEventArgs(new CommandEventArgs("Update", null), this, (ReorderListItem)this.ChildList.Controls[itemIndex]);
				}
				IDictionary oldValues;
				IOrderedDictionary values;
				IDictionary keys;
				this.PrepareRowValues(e, out oldValues, out values, out keys);
				DataSourceView data = this.GetData();
				if (data != null)
				{
					data.Update(keys, values, oldValues, delegate(int rows, Exception ex)
					{
						if (ex != null)
						{
							return false;
						}
						this.OnUpdateCommand(e);
						this.EditItemIndex = -1;
						return true;
					});
					return;
				}
			}
			this.OnUpdateCommand(e);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001A920 File Offset: 0x00018B20
		private static void MoveChildren(Control source, Control dest)
		{
			for (int i = source.Controls.Count - 1; i >= 0; i--)
			{
				dest.Controls.AddAt(0, source.Controls[i]);
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001A960 File Offset: 0x00018B60
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			ReorderListCommandEventArgs reorderListCommandEventArgs = args as ReorderListCommandEventArgs;
			if (reorderListCommandEventArgs != null)
			{
				this.OnItemCommand(reorderListCommandEventArgs);
				if (reorderListCommandEventArgs.CommandArgument != null)
				{
					string text = reorderListCommandEventArgs.CommandName.ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
					string a;
					if ((a = text) != null)
					{
						if (a == "INSERT")
						{
							this.HandleInsert(reorderListCommandEventArgs);
							return true;
						}
						if (a == "UPDATE")
						{
							this.HandleUpdate(reorderListCommandEventArgs, -1);
							return true;
						}
						if (a == "EDIT")
						{
							this.HandleEdit(reorderListCommandEventArgs);
							return true;
						}
						if (a == "DELETE")
						{
							this.HandleDelete(reorderListCommandEventArgs);
							return true;
						}
						if (a == "CANCEL")
						{
							this.HandleCancel(reorderListCommandEventArgs);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001AA19 File Offset: 0x00018C19
		protected virtual void OnItemCreated(EventArgs e)
		{
			this.Invoke(ReorderList.ItemCreatedKey, e);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001AA27 File Offset: 0x00018C27
		protected virtual void OnItemDataBound(EventArgs e)
		{
			this.Invoke(ReorderList.ItemDataBoundKey, e);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001AA35 File Offset: 0x00018C35
		protected virtual void OnItemCommand(EventArgs e)
		{
			this.Invoke(ReorderList.ItemCommandKey, e);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001AA44 File Offset: 0x00018C44
		protected virtual void OnItemReorder(ReorderListItemReorderEventArgs e)
		{
			try
			{
				if ((this.DataSource != null || base.IsBoundUsingDataSourceID) && !this.DoReorder(e.OldIndex, e.NewIndex))
				{
					throw new InvalidOperationException("Can't reorder data source.  It is not a DataSource and does not implement IList.");
				}
			}
			catch (Exception ex)
			{
				this.CallbackResult = ex.Message;
				throw;
			}
			this.Invoke(ReorderList.ItemReorderKey, e);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		protected virtual void OnCancelCommand(EventArgs e)
		{
			this.Invoke(ReorderList.CancelCommandKey, e);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001AABE File Offset: 0x00018CBE
		protected virtual void OnDeleteCommand(EventArgs e)
		{
			this.Invoke(ReorderList.DeleteCommandKey, e);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001AACC File Offset: 0x00018CCC
		protected virtual void OnEditCommand(EventArgs e)
		{
			this.Invoke(ReorderList.EditCommandKey, e);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001AADA File Offset: 0x00018CDA
		protected virtual void OnInsertCommand(EventArgs e)
		{
			this.Invoke(ReorderList.InsertCommandKey, e);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001AAE8 File Offset: 0x00018CE8
		protected virtual void OnUpdateCommand(EventArgs e)
		{
			this.Invoke(ReorderList.UpdateCommandKey, e);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		protected void Invoke(object key, EventArgs e)
		{
			Delegate @delegate = base.Events[key];
			if (@delegate != null)
			{
				@delegate.DynamicInvoke(new object[]
				{
					this,
					e
				});
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001AB2C File Offset: 0x00018D2C
		protected override void PerformDataBinding(IEnumerable data)
		{
			this.ClearChildren();
			base.PerformDataBinding(data);
			if (base.IsBoundUsingDataSourceID && this.EditItemIndex != -1 && this.EditItemIndex < this.Controls.Count && base.IsViewStateEnabled)
			{
				this.BoundFieldValues.Clear();
				this.ExtractRowValues(this.BoundFieldValues, this.ChildList.Controls[this.EditItemIndex] as ReorderListItem, false, false);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001ABA6 File Offset: 0x00018DA6
		private void PrepareRowValues(ReorderListCommandEventArgs e, out IDictionary oldValues, out IOrderedDictionary newValues, out IDictionary keys)
		{
			this.PrepareRowValues(e, out oldValues, out newValues, out keys, false);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		private void PrepareRowValues(ReorderListCommandEventArgs e, out IDictionary oldValues, out IOrderedDictionary newValues, out IDictionary keys, bool isAddOperation)
		{
			if (!isAddOperation)
			{
				oldValues = ReorderList.CopyDictionary(this.BoundFieldValues, null);
			}
			else
			{
				oldValues = null;
			}
			newValues = new OrderedDictionary((oldValues == null) ? 0 : oldValues.Count);
			if (this.DataKeyField != null && !isAddOperation)
			{
				keys = new OrderedDictionary(1);
				keys[this.DataKeyField] = this.DataKeysArray[e.Item.ItemIndex];
			}
			else
			{
				keys = null;
			}
			this.ExtractRowValues(newValues, e.Item, true, isAddOperation);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001AC40 File Offset: 0x00018E40
		private void ProcessReorder(int oldIndex, int newIndex)
		{
			try
			{
				if (oldIndex != newIndex && Math.Max(oldIndex, newIndex) != this.DataKeysArray.Count)
				{
					Control control = this.Items[oldIndex];
					this.OnItemReorder(new ReorderListItemReorderEventArgs(control as ReorderListItem, oldIndex, newIndex));
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001AC9C File Offset: 0x00018E9C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.ChildList.Controls.Count == 0)
			{
				if (this.EmptyListTemplate != null)
				{
					Panel panel = new Panel();
					panel.ID = this.ClientID;
					this.EmptyListTemplate.InstantiateIn(panel);
					panel.RenderControl(writer);
				}
				return;
			}
			base.RenderContents(writer);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		public void UpdateItem(int rowIndex)
		{
			this.HandleUpdate(null, rowIndex);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			ReorderListItem item = this.GetItem(itemType, repeatIndex);
			return item.ControlStyle;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0001AD18 File Offset: 0x00018F18
		public bool HasFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0001AD1B File Offset: 0x00018F1B
		public bool HasHeader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001AD1E File Offset: 0x00018F1E
		public bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001AD24 File Offset: 0x00018F24
		public void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			ReorderListItem item = this.GetItem(itemType, repeatIndex);
			item.RenderControl(writer);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001AD44 File Offset: 0x00018F44
		private ReorderListItem GetItem(ListItemType itemType, int repeatIndex)
		{
			switch (itemType)
			{
			case ListItemType.Item:
			case ListItemType.EditItem:
				return (ReorderListItem)this.Controls[repeatIndex];
			case ListItemType.Separator:
				return (ReorderListItem)this.Controls[repeatIndex * 2];
			}
			throw new ArgumentException("Unknown value", "itemType");
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0001ADA6 File Offset: 0x00018FA6
		public int RepeatedItemCount
		{
			get
			{
				if (this.itemsArray != null)
				{
					return this.itemsArray.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		private static bool ParsePostBack(string eventArgument, out string eventName, out string itemId, out string[] args)
		{
			itemId = null;
			eventName = null;
			args = new string[0];
			string[] array = eventArgument.Split(new char[]
			{
				':'
			});
			if (array.Length < 2)
			{
				return false;
			}
			eventName = array[0];
			itemId = array[1];
			if (array.Length > 2)
			{
				args = new string[array.Length - 2];
				Array.Copy(array, 2, args, 0, args.Length);
			}
			return true;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001AE24 File Offset: 0x00019024
		protected void RaisePostBackEvent(string eventArgument)
		{
			string text;
			string text2;
			string[] array;
			string a;
			if (ReorderList.ParsePostBack(eventArgument, out text, out text2, out array) && (a = text) != null)
			{
				if (!(a == "reorder"))
				{
					return;
				}
				this.ProcessReorder(int.Parse(array[0], CultureInfo.InvariantCulture), int.Parse(array[1], CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0001AE72 File Offset: 0x00019072
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x0001AE7A File Offset: 0x0001907A
		private string CallbackResult
		{
			get
			{
				return this._callbackResult;
			}
			set
			{
				this._callbackResult = value;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001AE83 File Offset: 0x00019083
		string ICallbackEventHandler.GetCallbackResult()
		{
			return this.CallbackResult;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001AE8B File Offset: 0x0001908B
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.CallbackResult = string.Empty;
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001AE9F File Offset: 0x0001909F
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.CallbackResult = string.Empty;
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001AEB3 File Offset: 0x000190B3
		protected V GetPropertyValue<V>(string propertyName, V nullValue)
		{
			if (this.ViewState[propertyName] == null)
			{
				return nullValue;
			}
			return (V)((object)this.ViewState[propertyName]);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001AED6 File Offset: 0x000190D6
		protected void SetPropertyValue<V>(string propertyName, V value)
		{
			this.ViewState[propertyName] = value;
		}

		// Token: 0x040003CC RID: 972
		private const string ArgReplace = "_~Arg~_";

		// Token: 0x040003CD RID: 973
		private const string ArgContext = "_~Context~_";

		// Token: 0x040003CE RID: 974
		private const string ArgSuccess = "_~Success~_";

		// Token: 0x040003CF RID: 975
		private const string ArgError = "_~Error~_";

		// Token: 0x040003D0 RID: 976
		private static object ItemCommandKey = new object();

		// Token: 0x040003D1 RID: 977
		private static object CancelCommandKey = new object();

		// Token: 0x040003D2 RID: 978
		private static object EditCommandKey = new object();

		// Token: 0x040003D3 RID: 979
		private static object DeleteCommandKey = new object();

		// Token: 0x040003D4 RID: 980
		private static object UpdateCommandKey = new object();

		// Token: 0x040003D5 RID: 981
		private static object InsertCommandKey = new object();

		// Token: 0x040003D6 RID: 982
		private static object ItemDataBoundKey = new object();

		// Token: 0x040003D7 RID: 983
		private static object ItemCreatedKey = new object();

		// Token: 0x040003D8 RID: 984
		private static object ItemReorderKey = new object();

		// Token: 0x040003D9 RID: 985
		private static object KeysKey = new object();

		// Token: 0x040003DA RID: 986
		private BulletedList _childList;

		// Token: 0x040003DB RID: 987
		private Control _dropTemplateControl;

		// Token: 0x040003DC RID: 988
		private ITemplate _reorderTemplate;

		// Token: 0x040003DD RID: 989
		private ITemplate _itemTemplate;

		// Token: 0x040003DE RID: 990
		private ITemplate _editItemTemplate;

		// Token: 0x040003DF RID: 991
		private ITemplate _insertItemTemplate;

		// Token: 0x040003E0 RID: 992
		private ITemplate _dragHandleTemplate;

		// Token: 0x040003E1 RID: 993
		private ITemplate _emptyListTemplate;

		// Token: 0x040003E2 RID: 994
		private List<ReorderList.DraggableListItemInfo> _draggableItems;

		// Token: 0x040003E3 RID: 995
		private DropWatcherExtender _dropWatcherExtender;

		// Token: 0x040003E4 RID: 996
		private ArrayList itemsArray;

		// Token: 0x040003E5 RID: 997
		private ReorderListItemLayoutType _layoutType = ReorderListItemLayoutType.Table;

		// Token: 0x040003E6 RID: 998
		private string _callbackResult = string.Empty;

		// Token: 0x02000170 RID: 368
		private class DraggableListItemInfo
		{
			// Token: 0x040003E8 RID: 1000
			public Control TargetControl;

			// Token: 0x040003E9 RID: 1001
			public Control HandleControl;

			// Token: 0x040003EA RID: 1002
			public DraggableListItemExtender Extender;
		}
	}
}
