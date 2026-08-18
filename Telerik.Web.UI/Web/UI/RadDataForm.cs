using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x02000200 RID: 512
	[ClientScriptResource("Telerik.Web.UI.RadDataForm", "Telerik.Web.UI.DataForm.RadDataFormScripts.js")]
	[ControlValueProperty("SelectedValue")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDataForm))]
	[Description("Telerik RadDataForm")]
	[Designer("Telerik.Web.Design.RadDataFormDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadDataForm), "Telerik.Web.UI.DataForm.png")]
	[DefaultProperty("")]
	[DefaultEvent("NeedDataSource")]
	[LightweightRendering]
	[ToolboxData("<{0}:RadDataForm runat=server></{0}:RadDataForm>")]
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("DataForm", typeof(RadDataForm))]
	[EmbeddedSkin("DataForm", "Default", typeof(RadDataForm))]
	public class RadDataForm : RadCompositeDataBoundControl, IRadPageableItemContainer, IPostBackEventHandler, IPageableItemContainer
	{
		// Token: 0x06001241 RID: 4673 RVA: 0x000410BC File Offset: 0x0003F2BC
		static RadDataForm()
		{
			RadDataForm.EventItemCreated = new object();
			RadDataForm.EventItemDataBound = new object();
			RadDataForm.EventItemCommand = new object();
			RadDataForm.EventNeedDataSource = new object();
			RadDataForm.EventPageIndexChanged = new object();
			RadDataForm.EventTotalRowCountAvailable = new object();
			RadDataForm.EventTotalRowCountAvailableAsp = new object();
			RadDataForm.EventSelectedIndexChanged = new object();
			RadDataForm.EventItemEditing = new object();
			RadDataForm.EventItemUpdating = new object();
			RadDataForm.EventItemCanceling = new object();
			RadDataForm.EventItemUpdated = new object();
			RadDataForm.EventItemDeleting = new object();
			RadDataForm.EventItemDeleted = new object();
			RadDataForm.EventItemInserting = new object();
			RadDataForm.EventItemInserted = new object();
			RadDataForm.EventFieldDescriptorsReady = new object();
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000411ED File Offset: 0x0003F3ED
		internal bool IsUsingModelBinding
		{
			get
			{
				return base.IsUsingModelBinders;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x000411F5 File Offset: 0x0003F3F5
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x000411FD File Offset: 0x0003F3FD
		protected internal bool IsModelValid
		{
			get
			{
				return this._isModelValid;
			}
			set
			{
				this._isModelValid = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00041206 File Offset: 0x0003F406
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00041209 File Offset: 0x0003F409
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x00041211 File Offset: 0x0003F411
		protected Control LayoutTemplateWrapper { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004121A File Offset: 0x0003F41A
		internal DataFormControlLocator ControlLocator
		{
			get
			{
				if (this._controlLocator == null)
				{
					this._controlLocator = new DataFormControlLocator();
				}
				return this._controlLocator;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00041235 File Offset: 0x0003F435
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0004123D File Offset: 0x0003F43D
		protected bool IsDataBinding { get; set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00041246 File Offset: 0x0003F446
		private DataFormControlStateManager ControlState
		{
			get
			{
				if (this._controlStateManager == null)
				{
					this._controlStateManager = new DataFormControlStateManager();
				}
				return this._controlStateManager;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00041261 File Offset: 0x0003F461
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0004126C File Offset: 0x0003F46C
		private string[] DataKeyNamesInternal
		{
			get
			{
				object obj = this.ViewState["DataKeyNames"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0004129A File Offset: 0x0003F49A
		private List<DataKey> DataKeysArrayList
		{
			get
			{
				if (this._dataKeysArrayList == null)
				{
					this._dataKeysArrayList = new List<DataKey>();
				}
				return this._dataKeysArrayList;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x000412B5 File Offset: 0x0003F4B5
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x000412BD File Offset: 0x0003F4BD
		protected IEnumerable CurrentDataSource { get; set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000412C8 File Offset: 0x0003F4C8
		protected virtual DataFormEnumerableBase ResolvedDataSource
		{
			get
			{
				if (this._resolvedDataSource == null)
				{
					if (this.IsDataBinding)
					{
						this._resolvedDataSource = this.DataSourceHelper.GetResolvedDataSource(this, this.CurrentDataSource, this.DataMember);
					}
					else
					{
						this._resolvedDataSource = new DataFormEnumerableFromViewState(this.ControlState);
					}
					if (this._resolvedDataSource == null)
					{
						throw new InvalidOperationException(string.Format("Cannot resolve data source. DataMember: '{0}'", this.DataMember));
					}
					this.PrepareDataSource();
				}
				return this._resolvedDataSource;
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00041340 File Offset: 0x0003F540
		protected virtual void PrepareDataSource()
		{
			DataFormEnumerableBase resolvedDataSource = this._resolvedDataSource;
			resolvedDataSource.IsBoundUsingDataSourceID = base.IsBoundUsingDataSourceID;
			if (resolvedDataSource.SupportsPaging)
			{
				RadDataFormPagingManager pagingManager = resolvedDataSource.PagingManager;
				pagingManager.CurrentPageIndex = this.CurrentPageIndex;
				pagingManager.PageSize = this.PageSize;
				pagingManager.AllowPaging = this.AllowPaging;
				pagingManager.AllowCustomPaging = this.AllowCustomPaging;
				pagingManager.VirtualItemCount = this.VirtualItemCount;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x000413AB File Offset: 0x0003F5AB
		internal virtual DataFormDataSourceHelper DataSourceHelper
		{
			get
			{
				if (this._dataSourceHelper == null)
				{
					this._dataSourceHelper = new DataFormDataSourceHelper();
				}
				return this._dataSourceHelper;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x000413C6 File Offset: 0x0003F5C6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool DataSourceIsAssigned
		{
			get
			{
				return this.DataSource != null || base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000413D8 File Offset: 0x0003F5D8
		internal string FormatCssClass(string prefix, string userDefined)
		{
			string text = prefix;
			if (prefix == "RadDataForm")
			{
				text = string.Concat(new string[]
				{
					prefix,
					" ",
					prefix,
					"_",
					base.RuntimeSkin
				});
			}
			if (userDefined.IndexOf(text) >= 0)
			{
				return userDefined;
			}
			if (string.IsNullOrEmpty(userDefined))
			{
				return text;
			}
			return string.Format("{0} {1}", text, userDefined);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00041448 File Offset: 0x0003F648
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.EnsureLayoutTemplate();
			this.RemoveItems();
			this.IsDataBinding = dataBinding;
			this.CurrentDataSource = dataSource;
			if (this.ResolvedDataSource == DataFormEnumerableBase.Null && dataSource != null)
			{
				this.ClearResolvedDataSource();
			}
			if (this.ResolvedDataSource == DataFormEnumerableBase.Null && dataSource == null)
			{
				this.CreateEmptyDataItem(0);
				return 0;
			}
			if (dataBinding)
			{
				DataSourceView data = this.GetData();
				bool flag = false;
				if (this.AllowPaging && data.CanPage && data.CanRetrieveTotalRowCount)
				{
					this.AllowCustomPaging = true;
					this.VirtualItemCount = base.SelectArguments.TotalRowCount;
					flag = true;
				}
				if (flag)
				{
					this.PrepareDataSource();
				}
			}
			dataSource = this.ResolvedDataSource.RawEnumerable();
			if (dataBinding && this.LayoutTemplateWrapper != null)
			{
				this.LayoutTemplateWrapper.DataBind();
			}
			int dataItemsCount = 0;
			if (this.ItemTemplate != null)
			{
				dataItemsCount = this.CreateDataItems(this.LayoutTemplateWrapper, dataSource, dataBinding);
			}
			this.SavePagingData(dataBinding, this.ResolvedDataSource.PagingManager);
			this.DataSourceCount = this.ResolvedDataSource.DataSourceCount;
			this.CurrentDataSource = null;
			this.ClearResolvedDataSource();
			this.CreateEmptyDataItem(dataItemsCount);
			if (this.ControlState["_!ItemCount"] == null)
			{
				return 0;
			}
			return (int)this.ControlState["_!ItemCount"];
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00041584 File Offset: 0x0003F784
		private void AutoIDControl(Control control)
		{
			control.ID = string.Format("ctrl{0}", this._autoIDIndex++.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000415C0 File Offset: 0x0003F7C0
		protected virtual void RemoveItems()
		{
			if (this._itemsCreatedInContainerCount > 0 && this._itemsWrapperContainer != null)
			{
				for (int i = 0; i < this._itemsCreatedInContainerCount; i++)
				{
					this._itemsWrapperContainer.Controls.RemoveAt(this._placeholderControlIndex);
				}
				this._itemsCreatedInContainerCount = 0;
			}
			this._autoIDIndex = 0;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004161C File Offset: 0x0003F81C
		protected override void CreateChildControls()
		{
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				this.EnsureLayoutTemplate();
				if (this._itemsWrapperContainer == null)
				{
					this._placeholderControlIndex = this.PrepareItemContainer(this.LayoutTemplateWrapper, false, delegate(Control control)
					{
						this._itemsWrapperContainer = control;
					}, string.Empty);
				}
				if (this.ItemTemplate != null)
				{
					RadDataFormDataItem radDataFormDataItem = new RadDataFormDataItem(this, 0);
					this.ItemTemplate.InstantiateIn(radDataFormDataItem);
					this.OnItemCreated(new RadDataFormItemEventArgs(radDataFormDataItem));
					this._itemsWrapperContainer.Controls.AddAt(this._placeholderControlIndex, radDataFormDataItem);
				}
				if (this.EditItemTemplate != null)
				{
					RadDataFormEditableItem radDataFormEditableItem = new RadDataFormEditableItem(this, 0);
					this.EditItemTemplate.InstantiateIn(radDataFormEditableItem);
					this.OnItemCreated(new RadDataFormItemEventArgs(radDataFormEditableItem));
					this._itemsWrapperContainer.Controls.AddAt(this._placeholderControlIndex, radDataFormEditableItem);
				}
				if (this.InsertItemTemplate != null)
				{
					RadDataFormInsertItem radDataFormInsertItem = new RadDataFormInsertItem(this, 0);
					this.InsertItemTemplate.InstantiateIn(radDataFormInsertItem);
					this.OnItemCreated(new RadDataFormItemEventArgs(radDataFormInsertItem));
					this._itemsWrapperContainer.Controls.AddAt(this._placeholderControlIndex, radDataFormInsertItem);
				}
				if (this.EmptyDataTemplate != null)
				{
					RadDataFormEmptyDataItem radDataFormEmptyDataItem = new RadDataFormEmptyDataItem(this);
					this.EmptyDataTemplate.InstantiateIn(radDataFormEmptyDataItem);
					this.OnItemCreated(new RadDataFormItemEventArgs(radDataFormEmptyDataItem));
					this._itemsWrapperContainer.Controls.Add(radDataFormEmptyDataItem);
				}
				return;
			}
			object obj = this.ViewState["_!ItemCount"];
			if (obj == null && base.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
			if (obj != null && (int)obj != -1)
			{
				object[] dataSource = new object[(int)obj];
				this.CreateChildControls(dataSource, false);
				base.ClearChildViewState();
				return;
			}
			if (!this._instantiatedEmptyDataTemplate)
			{
				this.EnsureLayoutTemplate();
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000417CC File Offset: 0x0003F9CC
		private void SavePagingData(bool useDataSource, RadDataFormPagingManager pagingManager)
		{
			if (useDataSource)
			{
				this.ControlState["_!DSIC"] = pagingManager.DataSourceCount;
				this.ControlState["_!ItemCount"] = this.Items.Count;
				if (pagingManager.IsPagingEnabled)
				{
					this.ControlState["_!PCount"] = pagingManager.PageCount;
					this.ControlState["_!DSIC"] = pagingManager.DataSourceCount;
				}
				else
				{
					this.ControlState["_!PCount"] = null;
				}
			}
			this.UpdateDataPager(pagingManager);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00041874 File Offset: 0x0003FA74
		private void UpdateDataPager(RadDataFormPagingManager pagingManager)
		{
			int totalRowCount = (this.VirtualItemCount > 0) ? this.VirtualItemCount : pagingManager.DataSourceCount;
			RadDataPagerPageEventArgs e = new RadDataPagerPageEventArgs(this.StartRowIndex, 1, totalRowCount);
			this.OnTotalRowCountAvailable(e);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000418B0 File Offset: 0x0003FAB0
		public static bool IsBindableType(Type type)
		{
			return type.IsPrimitive || !(type != typeof(string)) || !(type != typeof(DateTime)) || !(type != typeof(TimeSpan)) || !(type != typeof(decimal)) || !(type != typeof(Guid)) || type.IsEnum || (type.IsValueType && type.IsGenericType && type.GetGenericArguments().Length == 1 && RadDataForm.IsBindableType(type.GetGenericArguments()[0]));
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00041956 File Offset: 0x0003FB56
		private void ClearResolvedDataSource()
		{
			this._resolvedDataSource = null;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00041960 File Offset: 0x0003FB60
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			DataSourceView data = this.GetData();
			if (this.AllowPaging && data.CanPage)
			{
				if (data.CanRetrieveTotalRowCount)
				{
					dataSourceSelectArguments.MaximumRows = this.PageSize;
					dataSourceSelectArguments.StartRowIndex = this.PageSize * this.CurrentPageIndex;
					dataSourceSelectArguments.RetrieveTotalRowCount = true;
				}
				else
				{
					dataSourceSelectArguments.MaximumRows = -1;
					dataSourceSelectArguments.StartRowIndex = this.PageSize * this.CurrentPageIndex;
				}
			}
			return dataSourceSelectArguments;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x000419D5 File Offset: 0x0003FBD5
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			if (!this._ignoreDataSourceViewChanged)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x000419E6 File Offset: 0x0003FBE6
		protected bool IsLinqDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.LinqDataSourceView";
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00041A02 File Offset: 0x0003FC02
		protected bool IsEnityDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString() == "System.Web.UI.WebControls.EntityDataSourceView" || this.GetData().GetType().ToString() == "Microsoft.AspNet.EntityDataSource.EntityDataSourceView";
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00041A3C File Offset: 0x0003FC3C
		protected bool IsOpenAccessDataSourceView
		{
			get
			{
				return this.GetData().GetType().ToString().IndexOf("OpenAccess.RT.DataSource.OpenAccessDataSourceView") > -1;
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00041A5C File Offset: 0x0003FC5C
		protected override void PerformDataBinding(IEnumerable data)
		{
			this.DataKeysArrayList.Clear();
			this.TrackViewState();
			int num = this.CreateChildControls(data, true);
			base.ChildControlsCreated = true;
			this.ViewState["_!ItemCount"] = num;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00041AA0 File Offset: 0x0003FCA0
		protected virtual void EnsureLayoutTemplate()
		{
			if (this.Controls.Count == 0 || this._instantiatedEmptyDataTemplate)
			{
				this.Controls.Clear();
				this.InitializeLayoutTemplate();
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00041AC9 File Offset: 0x0003FCC9
		private int CalculateItemDataIndex(int currentDisplayIndex)
		{
			return currentDisplayIndex + this.CurrentPageIndex * this.PageSize;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00041ADC File Offset: 0x0003FCDC
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private int BuildDataItem(int itemsCreatedCount, Control itemContainer, bool dataBinding, object dataItem, ref int controlIndex)
		{
			RadDataFormDataItem radDataFormDataItem = this.CreateDataItem(itemsCreatedCount);
			radDataFormDataItem.DataItemIndex = this.CalculateItemDataIndex(itemsCreatedCount);
			itemsCreatedCount++;
			this.AutoIDControl(radDataFormDataItem);
			this.InstantiateDataItemTemplate(itemsCreatedCount, radDataFormDataItem);
			this.AddItemToContainer(itemContainer, radDataFormDataItem, controlIndex);
			controlIndex++;
			if (dataBinding)
			{
				this.PopulateDataKeys(dataItem);
			}
			this.OnItemCreated(new RadDataFormItemEventArgs(radDataFormDataItem));
			this.Items.Add(radDataFormDataItem);
			if (dataBinding)
			{
				radDataFormDataItem.DataItem = dataItem;
				radDataFormDataItem.DataBind();
				this.OnItemDataBound(new RadDataFormItemEventArgs(radDataFormDataItem));
				radDataFormDataItem.ExtractValues(radDataFormDataItem.SavedOldValues);
			}
			return itemsCreatedCount;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00041B71 File Offset: 0x0003FD71
		protected virtual RadDataFormEmptyItem CreateEmptyItem()
		{
			return new RadDataFormEmptyItem(this);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00041B84 File Offset: 0x0003FD84
		protected virtual int CreateDataItems(Control container, IEnumerable dataSource, bool dataBinding)
		{
			int num = 0;
			if (dataSource == null)
			{
				return num;
			}
			if (this._itemsWrapperContainer == null)
			{
				this._placeholderControlIndex = this.PrepareItemContainer(container, false, delegate(Control control)
				{
					this._itemsWrapperContainer = control;
				}, string.Empty);
			}
			int num2 = this._placeholderControlIndex;
			this.Items.Clear();
			if (this.IsItemInserted && (this.InsertItemPosition == RadDataFormInsertItemPosition.FirstItem || this.InsertItemPosition == RadDataFormInsertItemPosition.Single))
			{
				this.BuildInsertItem(this._itemsWrapperContainer, num2, dataBinding);
				num2++;
			}
			if (!this.IsItemInserted || this.InsertItemPosition != RadDataFormInsertItemPosition.Single)
			{
				foreach (object dataItem in dataSource)
				{
					num = this.BuildDataItem(num, this._itemsWrapperContainer, dataBinding, dataItem, ref num2);
				}
			}
			if (this.IsItemInserted && this.InsertItemPosition == RadDataFormInsertItemPosition.LastItem)
			{
				this.BuildInsertItem(this._itemsWrapperContainer, num2, dataBinding);
				num2++;
			}
			this._itemsCreatedInContainerCount = num2 - this._placeholderControlIndex;
			return num;
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00041C9C File Offset: 0x0003FE9C
		protected void BuildInsertItem(Control itemsContainer, int controlIndex, bool dataBinding)
		{
			RadDataFormItem radDataFormItem = this.CreateInsertItem();
			this.AddItemToContainer(itemsContainer, radDataFormItem, controlIndex);
			this.OnItemCreated(new RadDataFormItemEventArgs(radDataFormItem));
			if (dataBinding)
			{
				((RadDataFormInsertItem)radDataFormItem).DataItem = this.GetDefaultInsertionObject();
				radDataFormItem.DataBind();
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00041CE0 File Offset: 0x0003FEE0
		protected virtual RadDataFormItem CreateInsertItem()
		{
			if (this.InsertItemTemplate == null)
			{
				throw new InvalidOperationException("The RadDataForm control does not have an InsertItemTemplate template specified.");
			}
			RadDataFormInsertItem radDataFormInsertItem = new RadDataFormInsertItem(this, -1);
			this.InsertItem = radDataFormInsertItem;
			this.AutoIDControl(radDataFormInsertItem);
			this.InstantiateInsertItemTemplate(radDataFormInsertItem);
			return radDataFormInsertItem;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00041D1E File Offset: 0x0003FF1E
		protected virtual void InstantiateInsertItemTemplate(Control container)
		{
			if (this.InsertItemTemplate != null)
			{
				this.InsertItemTemplate.InstantiateIn(container);
			}
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00041D34 File Offset: 0x0003FF34
		protected internal void PopulateDataKeys(object dataItem)
		{
			if (this.IsDesignMode || this.DataKeyNamesInternal.Length == 0)
			{
				return;
			}
			DataKey dataKey = new DataKey(base.IsTrackingViewState);
			this.DataKeysArrayList.Add(dataKey);
			try
			{
				foreach (string text in this.DataKeyNamesInternal)
				{
					dataKey[text] = this.ExtractDataKeyValue(dataItem, text);
				}
			}
			catch (ArgumentNullException innerException)
			{
				throw new ArgumentException("There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.", innerException);
			}
			catch (HttpException innerException2)
			{
				throw new ArgumentException("There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.", innerException2);
			}
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00041DD4 File Offset: 0x0003FFD4
		protected virtual void AddItemSeparatorToContainer(Control container, Control itemSeparatorContainer, int index)
		{
			container.Controls.AddAt(index, itemSeparatorContainer);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00041DE4 File Offset: 0x0003FFE4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		private void InstantiateDataItemTemplate(int itemsCreatedCount, RadDataFormDataItem DataFormItem)
		{
			ITemplate template = this.ItemTemplate;
			if (this.ItemTemplate == null)
			{
				throw new InvalidOperationException("The RadDataForm control does not have an ItemTemplate template specified.");
			}
			if (this.EditItemTemplate != null && this.EditIndex != -1)
			{
				template = this.EditItemTemplate;
			}
			template.InstantiateIn(DataFormItem);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00041E2C File Offset: 0x0004002C
		protected virtual int PrepareItemContainer(Control container, bool isGroupContainer, Action<Control> retrieveItemPlaceHolderParentControl, string groupPlaceholderID)
		{
			Control control = this.RetrivePlaceHolderControl(container, this.ItemPlaceholderID);
			Control parent = control.Parent;
			retrieveItemPlaceHolderParentControl(parent);
			int result = parent.Controls.IndexOf(control);
			parent.Controls.Remove(control);
			return result;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00041E70 File Offset: 0x00040070
		protected virtual Control RetrivePlaceHolderControl(Control container, string placeholderId)
		{
			Control control = this.ControlLocator.RetriveFromContainer(container, placeholderId);
			if (control == null)
			{
				throw new InvalidOperationException("The RadDataForm control does not have an item placeholder specified.");
			}
			return control;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00041E9A File Offset: 0x0004009A
		protected virtual RadDataFormDataItem CreateDataItem(int displayIndex)
		{
			if (this.EditItemTemplate != null && this.EditIndex == displayIndex)
			{
				return new RadDataFormEditableItem(this, displayIndex);
			}
			return new RadDataFormDataItem(this, displayIndex);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00041EBC File Offset: 0x000400BC
		protected virtual void CreateEmptyDataItem(int dataItemsCount)
		{
			if (dataItemsCount == 0 && this.EmptyDataTemplate != null && !this.IsItemInserted)
			{
				this._instantiatedEmptyDataTemplate = true;
				this.Controls.Clear();
				RadDataFormEmptyDataItem radDataFormEmptyDataItem = new RadDataFormEmptyDataItem(this);
				this.EmptyDataTemplate.InstantiateIn(radDataFormEmptyDataItem);
				this.OnItemCreated(new RadDataFormItemEventArgs(radDataFormEmptyDataItem));
				this.Controls.Add(radDataFormEmptyDataItem);
			}
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00041F1C File Offset: 0x0004011C
		protected virtual void AutoDataBind(RadDataFormRebindReason rebindReason)
		{
			if (!this.Visible && (rebindReason & RadDataFormRebindReason.ExplicitRebind) != RadDataFormRebindReason.ExplicitRebind)
			{
				return;
			}
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
			if ((this.DataSource != null && !base.IsBoundUsingDataSourceID) || (base.IsBoundUsingDataSourceID && rebindReason == RadDataFormRebindReason.ExplicitRebind) || (this.DataSource != null && rebindReason == RadDataFormRebindReason.ExplicitRebind) || (this.IsUsingModelBinding && rebindReason == RadDataFormRebindReason.ExplicitRebind))
			{
				this.DataBind();
			}
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00041F7F File Offset: 0x0004017F
		protected override void PerformSelect()
		{
			this._ignoreDataSourceViewChanged = true;
			this._currentDataSource = null;
			base.PerformSelect();
			this._ignoreDataSourceViewChanged = false;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00041F9C File Offset: 0x0004019C
		protected override DataSourceView GetData()
		{
			if (this._currentDataSource == null)
			{
				if (this.IsBoundToIQueryableCollection)
				{
					this._currentDataSource = ((IDataSource)new RadDataForm.DummyDataSource((IEnumerable)this.DataSource)).GetView(this.DataMember);
				}
				else
				{
					this._currentDataSource = base.GetData();
				}
			}
			return this._currentDataSource;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00041FEE File Offset: 0x000401EE
		protected bool IsBoundToIQueryableCollection
		{
			get
			{
				return !base.IsBoundUsingDataSourceID && this.DataSource is IQueryable;
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00042008 File Offset: 0x00040208
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool flag = false;
			RadDataFormCommandEventArgs radDataFormCommandEventArgs = args as RadDataFormCommandEventArgs;
			if (radDataFormCommandEventArgs != null)
			{
				this.OnItemCommand(radDataFormCommandEventArgs);
				flag = true;
			}
			IRadDataFormCommandEvent radDataFormCommandEvent = args as IRadDataFormCommandEvent;
			if (radDataFormCommandEvent != null)
			{
				if (!radDataFormCommandEvent.Canceled)
				{
					radDataFormCommandEvent.ExecuteCommand(source);
				}
				flag = true;
			}
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (!flag && commandEventArgs != null)
			{
				RadDataFormCommandEventArgs radDataFormCommandEventArgs2 = RadDataFormCommandEventArgsFactory.CreateCommandEventArgs(new RadDataFormItem(RadDataFormItemType.EditItem, this), source, commandEventArgs);
				this.OnItemCommand(radDataFormCommandEventArgs2);
				flag = (radDataFormCommandEventArgs2.Canceled || RadDataFormCommandEventArgsFactory.HandleCommand(this, source, commandEventArgs));
			}
			return flag;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00042084 File Offset: 0x00040284
		private Control RetriveDataItemsContainer(Control container, string itemPlaceholderId)
		{
			Control control = container.FindControl(itemPlaceholderId);
			if (control == null)
			{
				throw new InvalidOperationException("The RadDataForm control does not have an item placeholder specified.");
			}
			return control;
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000420A8 File Offset: 0x000402A8
		protected virtual int InitializeLayoutTemplate()
		{
			this._itemsWrapperContainer = null;
			this._itemsCreatedInContainerCount = 0;
			this.LayoutTemplateWrapper = new Control();
			if (this.LayoutTemplate == null)
			{
				this.LayoutTemplate = new RadDataFormDefaultLayoutTemplate(this.ItemPlaceholderID);
			}
			this.LayoutTemplate.InstantiateIn(this.LayoutTemplateWrapper);
			this.Controls.Add(this.LayoutTemplateWrapper);
			this.OnLayoutCreated(new EventArgs());
			return 1;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00042118 File Offset: 0x00040318
		private string GetItemPlaceHolderClientID()
		{
			if (this.LayoutTemplateWrapper != null && !string.IsNullOrEmpty(this.ItemPlaceholderID))
			{
				Control control = this.LayoutTemplateWrapper.FindControl(this.ItemPlaceholderID);
				if (control != null)
				{
					return control.ClientID;
				}
			}
			return string.Empty;
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0004215B File Offset: 0x0004035B
		private void SetRequiresDataBindingIfInitialized()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0004216C File Offset: 0x0004036C
		private void AddItemToContainer(Control container, RadDataFormItem DataFormItem, int controlIndex)
		{
			if (container is HtmlTableRow)
			{
				RadDataFormHtmlTableCell radDataFormHtmlTableCell = new RadDataFormHtmlTableCell();
				radDataFormHtmlTableCell.Controls.Add(DataFormItem);
				container.Controls.AddAt(controlIndex, radDataFormHtmlTableCell);
				return;
			}
			if (container is HtmlTable)
			{
				RadDataFormHtmlTableRow radDataFormHtmlTableRow = new RadDataFormHtmlTableRow();
				radDataFormHtmlTableRow.Controls.Add(DataFormItem);
				container.Controls.AddAt(controlIndex, radDataFormHtmlTableRow);
				return;
			}
			if (container is TableRow)
			{
				RadDataFormTableCell radDataFormTableCell = new RadDataFormTableCell();
				radDataFormTableCell.Controls.Add(DataFormItem);
				container.Controls.AddAt(controlIndex, radDataFormTableCell);
				return;
			}
			if (container is Table)
			{
				RadDataFormTableRow radDataFormTableRow = new RadDataFormTableRow();
				radDataFormTableRow.Controls.Add(DataFormItem);
				container.Controls.AddAt(controlIndex, radDataFormTableRow);
				return;
			}
			container.Controls.AddAt(controlIndex, DataFormItem);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00042226 File Offset: 0x00040426
		internal void ObtainDataSource(RadDataFormRebindReason rebindReason, bool isBoundUsingDataSourceId)
		{
			if (!this.DataSourceIsAssigned && !isBoundUsingDataSourceId)
			{
				this.OnNeedDataSource(new RadDataFormNeedDataSourceEventArgs(rebindReason));
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004223F File Offset: 0x0004043F
		internal void ObtainDataSource(RadDataFormRebindReason rebindReason)
		{
			this.ObtainDataSource(rebindReason, base.IsBoundUsingDataSourceID);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0004224E File Offset: 0x0004044E
		internal bool IsBoundUsingDataSourceIDInternal
		{
			get
			{
				return base.IsBoundUsingDataSourceID;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x00042256 File Offset: 0x00040456
		internal bool UsesControlState
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00042264 File Offset: 0x00040464
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int current = 0;
				base.LoadViewState(array[current++]);
				if (!this.UsesControlState)
				{
					this.LoadControlStateObject(array, current);
				}
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004229A File Offset: 0x0004049A
		protected virtual void LoadControlStateObject(object[] objArray1, int current)
		{
			((IStateManager)this.ControlState).LoadViewState(objArray1[current++]);
			((IStateManager)this.DataKeyValues).LoadViewState(objArray1[current++]);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000422C4 File Offset: 0x000404C4
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			if (!this.UsesControlState)
			{
				this.SaveControlStateObject(arrayList);
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00042305 File Offset: 0x00040505
		protected virtual void SaveControlStateObject(IList state)
		{
			state.Add(((IStateManager)this.ControlState).SaveViewState());
			state.Add(((IStateManager)this.DataKeyValues).SaveViewState());
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004232B File Offset: 0x0004052B
		protected override void TrackViewState()
		{
			if (base.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.DataKeyValues).TrackViewState();
			((IStateManager)this.ControlState).TrackViewState();
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00042358 File Offset: 0x00040558
		protected override object SaveControlState()
		{
			object value = base.SaveControlState();
			ArrayList arrayList = new ArrayList();
			arrayList.Add(value);
			this.SaveControlStateObject(arrayList);
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00042394 File Offset: 0x00040594
		protected override void LoadControlState(object savedState)
		{
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array);
				this.LoadControlStateObject(array, 1);
			}
			else
			{
				base.LoadControlState(savedState);
			}
			if (!base.IsViewStateEnabled)
			{
				int totalRowCount = (this.VirtualItemCount > 0) ? this.VirtualItemCount : this.GetDataItemCountFromState();
				this.OnTotalRowCountAvailable(new RadDataPagerPageEventArgs(this.StartRowIndex, this.PageSize, totalRowCount));
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000423FB File Offset: 0x000405FB
		private int GetDataItemCountFromState()
		{
			return (int)(this.ControlState.ContainsKey("_!DSIC") ? this.ControlState["_!DSIC"] : 0);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004242C File Offset: 0x0004062C
		protected virtual void SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			if (maximumRows < 1)
			{
				throw new ArgumentOutOfRangeException("maximumRows");
			}
			if (startRowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startRowIndex");
			}
			bool flag = false;
			if (startRowIndex != this.StartRowIndex)
			{
				flag = true;
			}
			if (databind)
			{
				if (flag)
				{
					int num = Math.Max(1, this.PageSize);
					int num2 = startRowIndex / num;
					RadDataFormPageChangedEventArgs radDataFormPageChangedEventArgs = new RadDataFormPageChangedEventArgs(null, null, num2)
					{
						NewPageIndex = num2
					};
					this.FirePageIndexChanged(radDataFormPageChangedEventArgs);
					if (radDataFormPageChangedEventArgs.Canceled)
					{
						return;
					}
					this.CurrentPageIndex = num2;
				}
				base.RequiresDataBinding = true;
				this.ClearEditItems();
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000424B9 File Offset: 0x000406B9
		void IRadPageableItemContainer.SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			this.SetPageProperties(startRowIndex, maximumRows, databind);
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000424C4 File Offset: 0x000406C4
		int IRadPageableItemContainer.MaximumRows
		{
			get
			{
				return this.PageSize;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x000424CC File Offset: 0x000406CC
		protected virtual int StartRowIndex
		{
			get
			{
				return this.CurrentPageIndex * this.PageSize;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x000424DB File Offset: 0x000406DB
		int IRadPageableItemContainer.StartRowIndex
		{
			get
			{
				return this.StartRowIndex;
			}
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000424E4 File Offset: 0x000406E4
		private void FillDataKeys(IDictionary keys, RadDataFormDataItem item)
		{
			if (item.DisplayIndex < 0)
			{
				return;
			}
			foreach (string key in this.DataKeyNames)
			{
				keys[key] = this.DataKeyValues[item.DisplayIndex][key];
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00042532 File Offset: 0x00040732
		protected override bool LoadClientState(Dictionary<string, object> clientState)
		{
			this.LoadPagingState(clientState);
			return base.LoadClientState(clientState);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00042544 File Offset: 0x00040744
		private void LoadPagingState(Dictionary<string, object> clientState)
		{
			if (this.AllowPaging)
			{
				if (clientState.ContainsKey("currentPageIndex"))
				{
					this.CurrentPageIndex = (int)clientState["currentPageIndex"];
				}
				if (this.VirtualItemCount > 0 && clientState.ContainsKey("virtualItemCount"))
				{
					this.VirtualItemCount = (int)clientState["virtualItemCount"];
				}
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000425A8 File Offset: 0x000407A8
		public virtual void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Contains("FireCommand:"))
			{
				this.HandleClientFireCommand(RadDataForm.parseFireCommandEventName(eventArgument), RadDataForm.parseFireCommandArgs(eventArgument));
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00042674 File Offset: 0x00040874
		protected virtual void HandleClientFireCommand(string eventName, string eventArgs)
		{
			Action<Action<int>> action2 = delegate(Action<int> action)
			{
				int obj;
				if (int.TryParse(eventArgs, out obj))
				{
					action(obj);
				}
			};
			switch (eventName)
			{
			case "Edit":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Edit", string.Empty);
				});
				return;
			case "Cancel":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Cancel", string.Empty);
				});
				return;
			case "Update":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Update", string.Empty);
				});
				return;
			case "Delete":
				action2(delegate(int itemIndex)
				{
					this.Items[itemIndex].FireCommandEvent("Delete", string.Empty);
				});
				return;
			case "InitInsert":
			{
				RadDataFormInsertItemPosition itemPosition = RadDataFormInsertItemPosition.Single;
				if (!string.IsNullOrEmpty(eventArgs) && Enum.Parse(typeof(RadDataFormInsertItemPosition), eventArgs) != null)
				{
					itemPosition = (RadDataFormInsertItemPosition)Enum.Parse(typeof(RadDataFormInsertItemPosition), eventArgs);
				}
				this.ShowInsertItem(itemPosition);
				return;
			}
			case "CancelInsert":
			{
				RadDataFormInsertItem insertItem = this.InsertItem;
				if (insertItem != null)
				{
					insertItem.FireCommandEvent("Cancel", string.Empty);
					return;
				}
				this.IsItemInserted = false;
				this.Rebind();
				return;
			}
			case "PerformInsert":
			{
				RadDataFormInsertItem insertItem2 = this.InsertItem;
				if (insertItem2 != null)
				{
					insertItem2.FireCommandEvent("PerformInsert", string.Empty);
					return;
				}
				return;
			}
			case "Page":
				RadDataFormPageChangedEventArgs.HandlePaging(this, this, eventArgs);
				return;
			case "RebindDataForm":
				this.Rebind();
				return;
			}
			this.OnBubbleEvent(this, new CommandEventArgs(eventName, eventArgs));
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000428AA File Offset: 0x00040AAA
		void IPageableItemContainer.SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			this.SetPageProperties(startRowIndex, maximumRows, databind);
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x000428B5 File Offset: 0x00040AB5
		int IPageableItemContainer.StartRowIndex
		{
			get
			{
				return this.StartRowIndex;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x000428BD File Offset: 0x00040ABD
		int IPageableItemContainer.MaximumRows
		{
			get
			{
				return this.PageSize;
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001297 RID: 4759 RVA: 0x000428C5 File Offset: 0x00040AC5
		// (remove) Token: 0x06001298 RID: 4760 RVA: 0x000428D8 File Offset: 0x00040AD8
		event EventHandler<PageEventArgs> IPageableItemContainer.TotalRowCountAvailable
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventTotalRowCountAvailableAsp, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventTotalRowCountAvailableAsp, value);
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x000428EB File Offset: 0x00040AEB
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x000428F3 File Offset: 0x00040AF3
		protected bool IsNeedDataSourceInProgress { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x000428FC File Offset: 0x00040AFC
		// (set) Token: 0x0600129C RID: 4764 RVA: 0x00042904 File Offset: 0x00040B04
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Description("Gets or sets the custom content for the root container in a RadDataForm control.")]
		[Browsable(false)]
		[TemplateContainer(typeof(RadDataForm))]
		public virtual ITemplate LayoutTemplate { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0004290D File Offset: 0x00040B0D
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x00042915 File Offset: 0x00040B15
		[DefaultValue(null)]
		[Browsable(false)]
		[Description("Gets or sets the custom content for the data item in a RadDataForm control")]
		[TemplateContainer(typeof(RadDataFormDataItem), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate ItemTemplate { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0004291E File Offset: 0x00040B1E
		// (set) Token: 0x060012A0 RID: 4768 RVA: 0x00042926 File Offset: 0x00040B26
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadDataFormDataItem), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[Description("Gets or sets the custom content for the item in edit mode.")]
		[Browsable(false)]
		public virtual ITemplate EditItemTemplate { get; set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0004292F File Offset: 0x00040B2F
		// (set) Token: 0x060012A2 RID: 4770 RVA: 0x00042937 File Offset: 0x00040B37
		[DefaultValue(null)]
		[TemplateContainer(typeof(RadDataFormDataItem), BindingDirection.TwoWay)]
		[Browsable(false)]
		[Description("Gets or sets the custom content for an insert item in the RadDataForm control.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate InsertItemTemplate { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00042940 File Offset: 0x00040B40
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x0004297A File Offset: 0x00040B7A
		[Description("Gets or sets the ID for the item placeholder in a RadDataForm control. ")]
		[DefaultValue("itemPlaceholder")]
		[Category("Behavior")]
		public virtual string ItemPlaceholderID
		{
			get
			{
				object obj = this.ViewState["ItemPlaceholderID"];
				if (obj == null || string.IsNullOrEmpty((string)obj))
				{
					obj = "itemPlaceholder";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ItemPlaceholderID"] = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x0004298D File Offset: 0x00040B8D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets a collection of RadDataFormDataItem objects that represent the data items of the current page of data in a DataForm control.")]
		public virtual RadDataFormDataItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new RadDataFormDataItemCollection();
				}
				return this._items;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x000429A8 File Offset: 0x00040BA8
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x000429B0 File Offset: 0x00040BB0
		[TemplateContainer(typeof(RadDataForm))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Template that will be displayed if there are no records in the DataSource assigned")]
		[Browsable(false)]
		[DefaultValue(null)]
		public virtual ITemplate EmptyDataTemplate { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000429BC File Offset: 0x00040BBC
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x000429F4 File Offset: 0x00040BF4
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(RadDataFormStringArrayConverter))]
		[DefaultValue(null)]
		[Category("Data")]
		[Description("Comma delimited list of data-field Names")]
		public virtual string[] DataKeyNames
		{
			get
			{
				object obj = this.ViewState["DataKeyNames"] ?? new string[0];
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (!DataFormArrayComparerHelper.CompareStringArrays(value, this.DataKeyNamesInternal))
				{
					this.ViewState["DataKeyNames"] = ((value != null) ? value.Clone() : null);
					this.DataKeysArrayList.Clear();
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00042A31 File Offset: 0x00040C31
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadDataFormDataKeyArray DataKeyValues
		{
			get
			{
				if (this._dataKeyValues == null)
				{
					this._dataKeyValues = new RadDataFormDataKeyArray(this.DataKeysArrayList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._dataKeyValues).TrackViewState();
					}
				}
				return this._dataKeyValues;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00042A68 File Offset: 0x00040C68
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x00042A91 File Offset: 0x00040C91
		[SimplePersistenceSetting]
		[Bindable(true)]
		[Browsable(false)]
		[Description("Gets or sets a value indicating the index of the currently active page in case paging is enabled")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CurrentPageIndex
		{
			get
			{
				object obj = this.ControlState["CurrentPageIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ControlState["CurrentPageIndex"] = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00042AB8 File Offset: 0x00040CB8
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x00042AE6 File Offset: 0x00040CE6
		[SimplePersistenceSetting]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsItemInserted
		{
			get
			{
				object obj = this.ControlState["_!iii"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				this.ControlState["_!iii"] = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00042AFE File Offset: 0x00040CFE
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00042B06 File Offset: 0x00040D06
		public int DataSourceCount { get; private set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00042B0F File Offset: 0x00040D0F
		[NotifyParentProperty(true)]
		[Category("Paging")]
		[DefaultValue(1)]
		[SimplePersistenceSetting]
		[Description("Specify the maximum number of items that would appear in a page,when paging is enabled by AllowPaging property.")]
		internal int PageSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00042B14 File Offset: 0x00040D14
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Gets the number of pages required to display the records of the data source in a RadDataForm control.")]
		[Browsable(false)]
		[Category("Paging")]
		public virtual int PageCount
		{
			get
			{
				if (this._resolvedDataSource != null)
				{
					return this._resolvedDataSource.PagingManager.PageCount;
				}
				object obj = this.ControlState["_!PCount"];
				if (obj == null)
				{
					return 1;
				}
				return (int)obj;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00042B58 File Offset: 0x00040D58
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00042B81 File Offset: 0x00040D81
		[NotifyParentProperty(true)]
		[DefaultValue(0)]
		[Category("Paging")]
		[Browsable(true)]
		[Bindable(true)]
		[Description("VisibleItemCount")]
		public virtual int VirtualItemCount
		{
			get
			{
				object obj = this.ControlState["VirtualItemCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ControlState["VirtualItemCount"] = value;
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060012B5 RID: 4789 RVA: 0x00042BA8 File Offset: 0x00040DA8
		// (remove) Token: 0x060012B6 RID: 4790 RVA: 0x00042BBB File Offset: 0x00040DBB
		[Category("Action")]
		[Description("Raised when LayoutTemplate is created")]
		public event EventHandler<EventArgs> LayoutCreated
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventLayoutCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventLayoutCreated, value);
			}
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00042BD0 File Offset: 0x00040DD0
		protected virtual void OnLayoutCreated(EventArgs e)
		{
			EventHandler<EventArgs> eventHandler = base.Events[RadDataForm.EventLayoutCreated] as EventHandler<EventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060012B8 RID: 4792 RVA: 0x00042BFE File Offset: 0x00040DFE
		// (remove) Token: 0x060012B9 RID: 4793 RVA: 0x00042C11 File Offset: 0x00040E11
		[Category("Action")]
		[Description("Raised when RadDataFormItem is created")]
		public event EventHandler<RadDataFormItemEventArgs> ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemCreated, value);
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00042C24 File Offset: 0x00040E24
		protected virtual void OnItemCreated(RadDataFormItemEventArgs e)
		{
			EventHandler<RadDataFormItemEventArgs> eventHandler = base.Events[RadDataForm.EventItemCreated] as EventHandler<RadDataFormItemEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060012BB RID: 4795 RVA: 0x00042C52 File Offset: 0x00040E52
		// (remove) Token: 0x060012BC RID: 4796 RVA: 0x00042C65 File Offset: 0x00040E65
		[Category("Action")]
		[Description("Raised when RadDataFormItem is data bound")]
		public event EventHandler<RadDataFormItemEventArgs> ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemDataBound, value);
			}
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00042C78 File Offset: 0x00040E78
		protected virtual void OnItemDataBound(RadDataFormItemEventArgs e)
		{
			EventHandler<RadDataFormItemEventArgs> eventHandler = base.Events[RadDataForm.EventItemDataBound] as EventHandler<RadDataFormItemEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060012BE RID: 4798 RVA: 0x00042CA6 File Offset: 0x00040EA6
		// (remove) Token: 0x060012BF RID: 4799 RVA: 0x00042CB9 File Offset: 0x00040EB9
		[Description("Raised when a button in a RadDataForm control is clicked.")]
		[Category("Action")]
		public event EventHandler<RadDataFormCommandEventArgs> ItemCommand
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemCommand, value);
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00042CCC File Offset: 0x00040ECC
		protected virtual void OnItemCommand(RadDataFormCommandEventArgs e)
		{
			EventHandler<RadDataFormCommandEventArgs> eventHandler = base.Events[RadDataForm.EventItemCommand] as EventHandler<RadDataFormCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060012C1 RID: 4801 RVA: 0x00042CFA File Offset: 0x00040EFA
		// (remove) Token: 0x060012C2 RID: 4802 RVA: 0x00042D0D File Offset: 0x00040F0D
		[Description("Fires when \"Page\" command bubbles")]
		[Category("Action")]
		public event EventHandler<RadDataFormPageChangedEventArgs> PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventPageIndexChanged, value);
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00042D20 File Offset: 0x00040F20
		protected virtual void OnPageIndexChanged(RadDataFormPageChangedEventArgs e)
		{
			EventHandler<RadDataFormPageChangedEventArgs> eventHandler = base.Events[RadDataForm.EventPageIndexChanged] as EventHandler<RadDataFormPageChangedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00042D4E File Offset: 0x00040F4E
		internal void FirePageIndexChanged(RadDataFormPageChangedEventArgs e)
		{
			this.OnPageIndexChanged(e);
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060012C5 RID: 4805 RVA: 0x00042D57 File Offset: 0x00040F57
		// (remove) Token: 0x060012C6 RID: 4806 RVA: 0x00042D6A File Offset: 0x00040F6A
		[Category("Action")]
		[Description("Occurs when an insert operation is requested, but before the RadDataForm control performs the insert.")]
		public event EventHandler<RadDataFormCommandEventArgs> ItemInserting
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemInserting, value);
			}
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00042D80 File Offset: 0x00040F80
		protected virtual void OnItemInserting(RadDataFormCommandEventArgs e)
		{
			EventHandler<RadDataFormCommandEventArgs> eventHandler = base.Events[RadDataForm.EventItemInserting] as EventHandler<RadDataFormCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00042DAE File Offset: 0x00040FAE
		internal void FireItemInserting(RadDataFormCommandEventArgs e)
		{
			this.OnItemInserting(e);
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060012C9 RID: 4809 RVA: 0x00042DB7 File Offset: 0x00040FB7
		// (remove) Token: 0x060012CA RID: 4810 RVA: 0x00042DCA File Offset: 0x00040FCA
		[Description("Occurs when an insert operation is requested, after the RadDataForm control has inserted the item in the data source.")]
		[Category("Action")]
		public event EventHandler<RadDataFormInsertedEventArgs> ItemInserted
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemInserted, value);
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00042DE0 File Offset: 0x00040FE0
		protected virtual void OnItemInserted(RadDataFormInsertedEventArgs e)
		{
			EventHandler<RadDataFormInsertedEventArgs> eventHandler = base.Events[RadDataForm.EventItemInserted] as EventHandler<RadDataFormInsertedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060012CC RID: 4812 RVA: 0x00042E0E File Offset: 0x0004100E
		// (remove) Token: 0x060012CD RID: 4813 RVA: 0x00042E21 File Offset: 0x00041021
		[Description("Occurs when an edit operation is requested, but before the RadDataForm item is put in edit mode")]
		[Category("Action")]
		public event EventHandler<RadDataFormCommandEventArgs> ItemEditing
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemEditing, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemEditing, value);
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00042E34 File Offset: 0x00041034
		protected virtual void OnItemEditing(RadDataFormCommandEventArgs e)
		{
			EventHandler<RadDataFormCommandEventArgs> eventHandler = base.Events[RadDataForm.EventItemEditing] as EventHandler<RadDataFormCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00042E62 File Offset: 0x00041062
		internal void FireItemEditing(RadDataFormCommandEventArgs e)
		{
			this.OnItemEditing(e);
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060012D0 RID: 4816 RVA: 0x00042E6B File Offset: 0x0004106B
		// (remove) Token: 0x060012D1 RID: 4817 RVA: 0x00042E7E File Offset: 0x0004107E
		[Description("Occurs when a delete operation is requested, but before the RadDataForm control deletes the item.")]
		[Category("Action")]
		public event EventHandler<RadDataFormCommandEventArgs> ItemDeleting
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemDeleting, value);
			}
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00042E94 File Offset: 0x00041094
		protected virtual void OnItemDeleting(RadDataFormCommandEventArgs e)
		{
			EventHandler<RadDataFormCommandEventArgs> eventHandler = base.Events[RadDataForm.EventItemDeleting] as EventHandler<RadDataFormCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00042EC2 File Offset: 0x000410C2
		internal void FireItemDeleting(RadDataFormCommandEventArgs e)
		{
			this.OnItemDeleting(e);
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060012D4 RID: 4820 RVA: 0x00042ECB File Offset: 0x000410CB
		// (remove) Token: 0x060012D5 RID: 4821 RVA: 0x00042EDE File Offset: 0x000410DE
		[Description("Occurs when a delete operation is requested, after the RadDataForm control deletes the item.")]
		[Category("Action")]
		public event EventHandler<RadDataFormDeletedEventArgs> ItemDeleted
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemDeleted, value);
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00042EF4 File Offset: 0x000410F4
		protected virtual void OnItemDeleted(RadDataFormDeletedEventArgs e)
		{
			EventHandler<RadDataFormDeletedEventArgs> eventHandler = base.Events[RadDataForm.EventItemDeleted] as EventHandler<RadDataFormDeletedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060012D7 RID: 4823 RVA: 0x00042F22 File Offset: 0x00041122
		// (remove) Token: 0x060012D8 RID: 4824 RVA: 0x00042F35 File Offset: 0x00041135
		[Description("Occurs when an edit operation is requested, but before the RadDataForm item is put in edit mode")]
		[Category("Action")]
		public event EventHandler<RadDataFormCommandEventArgs> ItemUpdating
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemUpdating, value);
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00042F48 File Offset: 0x00041148
		protected virtual void OnItemUpdating(RadDataFormCommandEventArgs e)
		{
			EventHandler<RadDataFormCommandEventArgs> eventHandler = base.Events[RadDataForm.EventItemUpdating] as EventHandler<RadDataFormCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00042F76 File Offset: 0x00041176
		internal void FireItemUpdating(RadDataFormCommandEventArgs e)
		{
			this.OnItemUpdating(e);
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060012DB RID: 4827 RVA: 0x00042F7F File Offset: 0x0004117F
		// (remove) Token: 0x060012DC RID: 4828 RVA: 0x00042F92 File Offset: 0x00041192
		[Description("Occurs when an update operation is requested, after the RadDataForm control updates the item.")]
		[Category("Action")]
		public event EventHandler<RadDataFormUpdatedEventArgs> ItemUpdated
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemUpdated, value);
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00042FA8 File Offset: 0x000411A8
		protected virtual void OnItemUpdated(RadDataFormUpdatedEventArgs e)
		{
			EventHandler<RadDataFormUpdatedEventArgs> eventHandler = base.Events[RadDataForm.EventItemUpdated] as EventHandler<RadDataFormUpdatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00042FD6 File Offset: 0x000411D6
		internal void FireItemUpdated(RadDataFormUpdatedEventArgs e)
		{
			this.OnItemUpdated(e);
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060012DF RID: 4831 RVA: 0x00042FDF File Offset: 0x000411DF
		// (remove) Token: 0x060012E0 RID: 4832 RVA: 0x00042FF2 File Offset: 0x000411F2
		[Description("Occurs when a cancel operation is requested, but before the RadDataForm control cancels the insert or edit operation. ")]
		[Category("Action")]
		public event EventHandler<RadDataFormCommandEventArgs> ItemCanceling
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventItemCanceling, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventItemCanceling, value);
			}
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00043008 File Offset: 0x00041208
		protected virtual void OnItemCanceling(RadDataFormCommandEventArgs e)
		{
			EventHandler<RadDataFormCommandEventArgs> eventHandler = base.Events[RadDataForm.EventItemCanceling] as EventHandler<RadDataFormCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00043036 File Offset: 0x00041236
		internal void FireItemCanceling(RadDataFormCommandEventArgs e)
		{
			this.OnItemCanceling(e);
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060012E3 RID: 4835 RVA: 0x0004303F File Offset: 0x0004123F
		// (remove) Token: 0x060012E4 RID: 4836 RVA: 0x00043052 File Offset: 0x00041252
		[Category("Action")]
		[Description("Raised when the DataForm is about to be bound and the data source must be assigned")]
		public event EventHandler<RadDataFormNeedDataSourceEventArgs> NeedDataSource
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventNeedDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventNeedDataSource, value);
			}
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00043068 File Offset: 0x00041268
		protected virtual void OnNeedDataSource(RadDataFormNeedDataSourceEventArgs e)
		{
			this.IsNeedDataSourceInProgress = true;
			try
			{
				EventHandler<RadDataFormNeedDataSourceEventArgs> eventHandler = base.Events[RadDataForm.EventNeedDataSource] as EventHandler<RadDataFormNeedDataSourceEventArgs>;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
			finally
			{
				this.IsNeedDataSourceInProgress = false;
			}
		}

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060012E6 RID: 4838 RVA: 0x000430B8 File Offset: 0x000412B8
		// (remove) Token: 0x060012E7 RID: 4839 RVA: 0x000430CB File Offset: 0x000412CB
		event EventHandler<RadDataPagerPageEventArgs> IRadPageableItemContainer.TotalRowCountAvailable
		{
			add
			{
				base.Events.AddHandler(RadDataForm.EventTotalRowCountAvailable, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadDataForm.EventTotalRowCountAvailable, value);
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000430E0 File Offset: 0x000412E0
		protected virtual void OnTotalRowCountAvailable(RadDataPagerPageEventArgs e)
		{
			EventHandler<RadDataPagerPageEventArgs> eventHandler = base.Events[RadDataForm.EventTotalRowCountAvailable] as EventHandler<RadDataPagerPageEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			PageEventArgs e2 = new PageEventArgs(e.StartRowIndex, e.MaximumRows, e.TotalRowCount);
			EventHandler<PageEventArgs> eventHandler2 = base.Events[RadDataForm.EventTotalRowCountAvailableAsp] as EventHandler<PageEventArgs>;
			if (eventHandler2 != null)
			{
				eventHandler2(this, e2);
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00043147 File Offset: 0x00041347
		internal bool ShouldBeBound
		{
			get
			{
				return this.ControlState["_!DSIC"] == null;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0004315C File Offset: 0x0004135C
		internal bool AlwaysAutoBindOnPostBack
		{
			get
			{
				return !base.IsViewStateEnabled;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00043167 File Offset: 0x00041367
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x0004316F File Offset: 0x0004136F
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets or sets the name of the method to call in order to update data")]
		public new string UpdateMethod
		{
			get
			{
				return base.UpdateMethod;
			}
			set
			{
				base.UpdateMethod = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00043178 File Offset: 0x00041378
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x00043180 File Offset: 0x00041380
		[Category("Data")]
		[Description("Gets or sets the name of the method to call in order to insert data")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public new string InsertMethod
		{
			get
			{
				return base.InsertMethod;
			}
			set
			{
				base.InsertMethod = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00043189 File Offset: 0x00041389
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x00043191 File Offset: 0x00041391
		[Description("Gets or sets the name of the method to call in order to delete data")]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public new string DeleteMethod
		{
			get
			{
				return base.DeleteMethod;
			}
			set
			{
				base.DeleteMethod = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0004319A File Offset: 0x0004139A
		[DefaultValue(true)]
		[Category("Paging")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether the automatic paging feature is enabled.")]
		internal virtual bool AllowPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x000431A0 File Offset: 0x000413A0
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x000431C9 File Offset: 0x000413C9
		internal virtual bool AllowCustomPaging
		{
			get
			{
				object obj = this.ControlState["AllowCustomPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["AllowCustomPaging"] = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x000431E1 File Offset: 0x000413E1
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Validation settings")]
		[Category("Validation")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadDataFormValidationSettings ValidationSettings
		{
			get
			{
				if (this._validationSettings == null)
				{
					this._validationSettings = new RadDataFormValidationSettings(this.ViewState, this);
				}
				return this._validationSettings;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00043203 File Offset: 0x00041403
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x00043238 File Offset: 0x00041438
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[Browsable(false)]
		public int EditIndex
		{
			get
			{
				if (this.ControlState["_editIndex"] != null)
				{
					this._editIndex = (int)this.ControlState["_editIndex"];
				}
				return this._editIndex;
			}
			set
			{
				this._editIndex = value;
				this.ControlState["_editIndex"] = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00043258 File Offset: 0x00041458
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x00043281 File Offset: 0x00041481
		[Category("Behavior")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = this.ControlState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ControlState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0004329C File Offset: 0x0004149C
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x000432C5 File Offset: 0x000414C5
		[Description("Gets or sets the location of the InsertItemTemplate template when it is rendered as part of the RadDataForm control.")]
		[DefaultValue(RadDataFormInsertItemPosition.Single)]
		[Category("Default")]
		public virtual RadDataFormInsertItemPosition InsertItemPosition
		{
			get
			{
				object obj = this.ViewState["InsertItemPosition"];
				if (obj != null)
				{
					return (RadDataFormInsertItemPosition)obj;
				}
				return RadDataFormInsertItemPosition.Single;
			}
			set
			{
				if (this.InsertItemPosition != value)
				{
					this.ViewState["InsertItemPosition"] = value;
					this.SetRequiresDataBindingIfInitialized();
				}
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x000432EC File Offset: 0x000414EC
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x000432F4 File Offset: 0x000414F4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Description("Gets the insert item of a RadDataForm control.")]
		public RadDataFormInsertItem InsertItem { get; protected set; }

		// Token: 0x060012FD RID: 4861 RVA: 0x00043300 File Offset: 0x00041500
		public void ClearEditItems()
		{
			foreach (RadDataFormDataItem radDataFormDataItem in this.Items)
			{
				radDataFormDataItem.Edit = false;
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00043354 File Offset: 0x00041554
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.UsesControlState)
			{
				this.Page.RegisterRequiresControlState(this);
			}
			if (this.AlwaysAutoBindOnPostBack && this.Page.IsPostBack)
			{
				this._shouldCallDataBindOnLoad = false;
				base.RequiresDataBinding = true;
				this.AutoDataBind(RadDataFormRebindReason.PostbackViewStateNotPersisted);
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000433A6 File Offset: 0x000415A6
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			this._pagePreLoadFired = true;
			base.OnPagePreLoad(sender, e);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x000433B7 File Offset: 0x000415B7
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x000433D7 File Offset: 0x000415D7
		[Description("Gets or sets ID of RadClientDataSource control that to be used for client side binding")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		public string ClientDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ClientDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientDataSourceID"] = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x000433EA File Offset: 0x000415EA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadDataFormClientSettings ClientSettings
		{
			get
			{
				if (this._clientSettings == null)
				{
					this._clientSettings = new RadDataFormClientSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._clientSettings).TrackViewState();
					}
				}
				return this._clientSettings;
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00043418 File Offset: 0x00041618
		protected override void OnLoad(EventArgs e)
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				base.ConfirmInitState();
				if (this.Page != null && !this._pagePreLoadFired && this.ViewState["_!DataBound"] == null)
				{
					if (!this.Page.IsPostBack)
					{
						base.RequiresDataBinding = true;
					}
					else if (base.IsViewStateEnabled)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
			else
			{
				base.OnLoad(e);
			}
			if (this.ShouldBeBound)
			{
				this.AutoDataBind(RadDataFormRebindReason.InitialLoad);
				return;
			}
			if (this.AlwaysAutoBindOnPostBack && this._shouldCallDataBindOnLoad)
			{
				this.AutoDataBind(RadDataFormRebindReason.PostbackViewStateNotPersisted);
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x000434AC File Offset: 0x000416AC
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x000434D5 File Offset: 0x000416D5
		[Description("RenderWrapper property determines if the LayoutTemplate should be wrapped inside Div element with ID of the DataForm")]
		[Category("Appearance")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool RenderWrapper
		{
			get
			{
				object obj = this.ControlState["_!rr"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ControlState["_!rr"] = value;
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000434F0 File Offset: 0x000416F0
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.RenderWrapper)
			{
				base.Render(writer);
				return;
			}
			this.RenderContents(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0004353D File Offset: 0x0004173D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00043541 File Offset: 0x00041741
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.FormatCssClass("RadDataForm", this.CssClass));
			base.RenderBeginTag(writer);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00043563 File Offset: 0x00041763
		protected override void OnPreRender(EventArgs e)
		{
			if (base.RequiresDataBinding)
			{
				this.Rebind();
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004357A File Offset: 0x0004177A
		protected override void RenderContents(HtmlTextWriter writer)
		{
			BaseClass.RenderVersionStamp(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			base.RenderContents(writer);
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0004359D File Offset: 0x0004179D
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x000435A5 File Offset: 0x000417A5
		internal bool SkipDataBinding { get; set; }

		// Token: 0x0600130D RID: 4877 RVA: 0x000435AE File Offset: 0x000417AE
		public override void DataBind()
		{
			if (this.SkipDataBinding)
			{
				return;
			}
			if (this.IsNeedDataSourceInProgress)
			{
				throw new InvalidOperationException("You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.");
			}
			base.DataBind();
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000435D2 File Offset: 0x000417D2
		public virtual void Rebind()
		{
			this.AutoDataBind(RadDataFormRebindReason.ExplicitRebind);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000435DC File Offset: 0x000417DC
		public virtual void ExtractValuesFromItem(IDictionary newValues, RadDataFormDataItem dataItem, bool includePrimaryKey)
		{
			if (newValues == null)
			{
				throw new ArgumentNullException("newValues");
			}
			if (dataItem == null)
			{
				throw new ArgumentNullException("dataItem");
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			RadDataForm.ExtractItemFromBindableControl(orderedDictionary, dataItem);
			this.FillValues(newValues, includePrimaryKey, orderedDictionary);
			IBindableTemplate bindableTemplate;
			if (dataItem.IsInEditMode)
			{
				if (dataItem is IRadDataFormInsertItem)
				{
					bindableTemplate = (this.InsertItemTemplate as IBindableTemplate);
				}
				else
				{
					bindableTemplate = (this.EditItemTemplate as IBindableTemplate);
				}
			}
			else
			{
				bindableTemplate = (this.ItemTemplate as IBindableTemplate);
			}
			if (bindableTemplate != null)
			{
				this.FillValues(newValues, includePrimaryKey, bindableTemplate.ExtractValues(dataItem));
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00043668 File Offset: 0x00041868
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Web.UI.IBindableControl")]
		private static void ExtractItemFromBindableControl(IOrderedDictionary values, Control container)
		{
			IBindableControl bindableControl = container as IBindableControl;
			if (bindableControl != null)
			{
				bindableControl.ExtractValues(values);
			}
			if (container is DataBoundControl || container is CompositeControl)
			{
				return;
			}
			foreach (object obj in container.Controls)
			{
				Control container2 = (Control)obj;
				RadDataForm.ExtractItemFromBindableControl(values, container2);
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000436E4 File Offset: 0x000418E4
		private void FillValues(IDictionary newValues, bool includePrimaryKey, IOrderedDictionary extractValues)
		{
			foreach (object obj in extractValues)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (includePrimaryKey || Array.IndexOf<object>(this.DataKeyNamesInternal, dictionaryEntry.Key) == -1)
				{
					object obj2 = dictionaryEntry.Value;
					string text = obj2 as string;
					if (this.ConvertEmptyStringToNull && text != null && text.Length == 0)
					{
						obj2 = null;
					}
					newValues.Add(dictionaryEntry.Key, obj2);
				}
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00043780 File Offset: 0x00041980
		public void PerformUpdate(RadDataFormDataItem editedItem)
		{
			this.PerformUpdate(editedItem, false);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004382C File Offset: 0x00041A2C
		public virtual void PerformUpdate(RadDataFormDataItem editedItem, bool suppressRebind)
		{
			if (editedItem == null)
			{
				throw new ArgumentNullException("editedItem");
			}
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.UpdateMethod))
				{
					throw new Exception("Updating is not supported unless the UpdateMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
			}
			if (data.CanUpdate)
			{
				Hashtable keys = new Hashtable();
				this.FillDataKeys(keys, editedItem);
				Hashtable hashtable = new Hashtable();
				this.ExtractValuesFromItem(hashtable, editedItem, false);
				data.Update(keys, hashtable, editedItem.SavedOldValues, delegate(int affectedRows, Exception exception)
				{
					RadDataFormUpdatedEventArgs radDataFormUpdatedEventArgs = new RadDataFormUpdatedEventArgs(affectedRows, exception, editedItem)
					{
						KeepInEditMode = (exception != null)
					};
					if (this.IsUsingModelBinding)
					{
						if (this.Page.ModelState.IsValid)
						{
							this.FireItemUpdatedEvent(editedItem, suppressRebind, exception, radDataFormUpdatedEventArgs);
						}
						else
						{
							editedItem.Edit = true;
						}
					}
					else
					{
						this.FireItemUpdatedEvent(editedItem, suppressRebind, exception, radDataFormUpdatedEventArgs);
					}
					return radDataFormUpdatedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000438F9 File Offset: 0x00041AF9
		private void FireItemUpdatedEvent(RadDataFormDataItem editedItem, bool suppressRebind, Exception exception, RadDataFormUpdatedEventArgs args)
		{
			this.FireItemUpdated(args);
			this.ValidateModel("Update", args);
			if (!args.KeepInEditMode)
			{
				editedItem.Edit = false;
			}
			if (exception == null && !suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004392C File Offset: 0x00041B2C
		private void ValidateModel(string commandName, RadDataFormDataChangeEventArgs args)
		{
			RadDataFormValidationSettings validationSettings = this.ValidationSettings;
			RadDataForm ownerDataForm = args.Item.OwnerDataForm;
			if (args.Exception != null && !args.ExceptionHandled && validationSettings.EnableModelValidation && !validationSettings.ValidateCommand(commandName))
			{
				ownerDataForm.IsModelValid = false;
				args.ExceptionHandled = true;
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004397B File Offset: 0x00041B7B
		public virtual void PerformDelete(RadDataFormDataItem editedItem)
		{
			this.PerformDelete(editedItem, false);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000439E4 File Offset: 0x00041BE4
		public virtual void PerformDelete(RadDataFormDataItem editedItem, bool suppressRebind)
		{
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.DeleteMethod))
				{
					throw new Exception("Deleting is not supported unless the DeleteMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
			}
			if (data.CanDelete)
			{
				editedItem.Edit = false;
				Hashtable keys = new Hashtable();
				this.FillDataKeys(keys, editedItem);
				Hashtable hashtable = new Hashtable();
				this.ExtractValuesFromItem(hashtable, editedItem, false);
				data.Delete(keys, hashtable, delegate(int affectedRows, Exception exception)
				{
					RadDataFormDeletedEventArgs radDataFormDeletedEventArgs = new RadDataFormDeletedEventArgs(affectedRows, exception, editedItem);
					this.OnItemDeleted(radDataFormDeletedEventArgs);
					this.ValidateModel("Delete", radDataFormDeletedEventArgs);
					if (exception == null && !suppressRebind)
					{
						this.Rebind();
					}
					return radDataFormDeletedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00043A9E File Offset: 0x00041C9E
		public virtual void ShowInsertItem()
		{
			if (this.InsertItemPosition != RadDataFormInsertItemPosition.None)
			{
				this.ShowInsertItem(this.InsertItemPosition);
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00043AB4 File Offset: 0x00041CB4
		public virtual void ShowInsertItem(RadDataFormInsertItemPosition itemPosition)
		{
			this.ShowInsertItem(itemPosition, null);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00043ABE File Offset: 0x00041CBE
		public virtual void ShowInsertItem(IDictionary defaultValues)
		{
			this.ShowInsertItem(RadDataFormInsertItemPosition.LastItem, defaultValues);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00043AC8 File Offset: 0x00041CC8
		public virtual void ShowInsertItem(RadDataFormInsertItemPosition itemPosition, IDictionary defaultValues)
		{
			this.InitializeInsertObjectDefaultValues(defaultValues);
			this.InsertItemPosition = itemPosition;
			this.IsItemInserted = true;
			this.Rebind();
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00043AE5 File Offset: 0x00041CE5
		public virtual void ShowInsertItem(object dataItem)
		{
			this._insertObject = dataItem;
			this.ShowInsertItem(RadDataFormInsertItemPosition.Single, null);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00043AF6 File Offset: 0x00041CF6
		public virtual void ShowInsertItem(RadDataFormInsertItemPosition itemPosition, object dataItem)
		{
			this._insertObject = dataItem;
			this.ShowInsertItem(itemPosition, null);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00043B07 File Offset: 0x00041D07
		private void InitializeInsertObjectDefaultValues(IDictionary defaultValues)
		{
			this._insertObjectDefaultValues = defaultValues;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00043B10 File Offset: 0x00041D10
		protected virtual object GetDefaultInsertionObject()
		{
			if (this._insertObject != null)
			{
				return this._insertObject;
			}
			if (this._insertObjectDefaultValues != null)
			{
				return this.ResolvedDataSource.GetInsertionObject(this._insertObjectDefaultValues);
			}
			return null;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00043B3C File Offset: 0x00041D3C
		public virtual void PerformInsert()
		{
			if (this.InsertItem == null)
			{
				throw new InvalidOperationException("Insert item is available only when RadDataForm is in insert mode.");
			}
			this.PerformInsert(this.InsertItem, false);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00043C18 File Offset: 0x00041E18
		public virtual void PerformInsert(RadDataFormInsertItem insertItem, bool suppressRebind)
		{
			if (insertItem == null)
			{
				throw new ArgumentNullException("insertItem");
			}
			DataSourceView data = base.GetData();
			ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
			if (modelDataSourceView != null)
			{
				if (string.IsNullOrWhiteSpace(this.InsertMethod))
				{
					throw new Exception("Inserting is not supported unless the InsertMethod is specified.");
				}
				this.ModelBindingUpdateProperties(modelDataSourceView);
				suppressRebind = true;
			}
			if (data.CanInsert)
			{
				Hashtable hashtable = new Hashtable();
				this.ExtractValuesFromItem(hashtable, insertItem, true);
				data.Insert(hashtable, delegate(int affectedRows, Exception exception)
				{
					RadDataFormInsertedEventArgs radDataFormInsertedEventArgs = new RadDataFormInsertedEventArgs(affectedRows, exception, insertItem)
					{
						KeepInInsertMode = (exception != null)
					};
					if (this.IsUsingModelBinding)
					{
						if (this.Page.ModelState.IsValid)
						{
							this.FireItemInsertedEvent(insertItem, suppressRebind, exception, radDataFormInsertedEventArgs);
						}
						else
						{
							this.IsItemInserted = true;
							this.RequiresDataBinding = false;
							insertItem.Edit = true;
						}
					}
					else
					{
						this.FireItemInsertedEvent(insertItem, suppressRebind, exception, radDataFormInsertedEventArgs);
					}
					return radDataFormInsertedEventArgs.ExceptionHandled;
				});
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00043CC8 File Offset: 0x00041EC8
		private void FireItemInsertedEvent(RadDataFormInsertItem insertItem, bool suppressRebind, Exception exception, RadDataFormInsertedEventArgs args)
		{
			this.OnItemInserted(args);
			this.ValidateModel("PerformInsert", args);
			if (!args.KeepInInsertMode)
			{
				this.IsItemInserted = false;
				insertItem.Edit = false;
			}
			if (exception == null && !suppressRebind)
			{
				this.Rebind();
			}
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00043D04 File Offset: 0x00041F04
		private void ModelBindingUpdateProperties(ModelDataSourceView modelView)
		{
			string dataKeyName = string.Empty;
			if (this.DataKeyNames.Length > 0)
			{
				dataKeyName = this.DataKeyNames[0];
			}
			modelView.UpdateProperties(this.ItemType, this.SelectMethod, this.UpdateMethod, base.InsertMethod, this.DeleteMethod, dataKeyName);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00043D50 File Offset: 0x00041F50
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>(scriptReferences);
			if (this.EnableEmbeddedScripts)
			{
				this.AddFeatureSpecificScriptReferences(list);
				bool flag = true;
				RadScriptManager radScriptManager = ScriptManager.GetCurrent(this.Page) as RadScriptManager;
				if (radScriptManager != null)
				{
					flag = radScriptManager.EnableEmbeddedjQuery;
				}
				if (flag)
				{
					list.Add(new ScriptReference("Telerik.Web.UI.Common.jQuery.js", RadDataForm.CurrentAssemblyName));
				}
			}
			return list;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00043DD8 File Offset: 0x00041FD8
		private void AddFeatureSpecificScriptReferences(List<ScriptReference> baseReferences)
		{
			string resourceNameSuffix = "Script";
			(string resourceName) => new ScriptReference(string.Format("{0}{1}.js", resourceName, resourceNameSuffix), RadDataForm.CurrentAssemblyName);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00043E1C File Offset: 0x0004201C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.RegisterClientSideEvents(delegate(string eventName, string eventValue)
			{
				RadCompositeDataBoundControl.DescribeEvent(descriptor, eventName, eventValue);
			});
			this.DescribeProperties(descriptor);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00043E74 File Offset: 0x00042074
		private void RegisterClientSideEvents(TAction<string, string> eventData)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientSettings.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text = propertyDescriptor.DisplayName.Replace("On", string.Empty);
					text = Regex.Replace(text, "^[A-Z]", (Match match) => match.ToString().ToLower(CultureInfo.InvariantCulture));
					string text2 = propertyDescriptor.GetValue(this.ClientSettings.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						eventData(text, text2);
					}
				}
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00043F58 File Offset: 0x00042158
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			new JavaScriptSerializer();
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			if (this.CurrentPageIndex != 0)
			{
				descriptor.AddProperty("_currentPageIndex", this.CurrentPageIndex);
			}
			int num = (this.VirtualItemCount > 0) ? this.VirtualItemCount : this.GetDataItemCountFromState();
			if (num > 0)
			{
				descriptor.AddProperty("_virtualItemCount", num);
			}
			if (this.IsItemInserted)
			{
				descriptor.AddProperty("_isItemInserted", this.IsItemInserted);
			}
			if (this.DataKeyNames.Length > 0)
			{
				descriptor.AddProperty("_clientDataKeyNames", this.DataKeyNames);
			}
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("_clientDataSourceID", control.ClientID);
				}
				catch (GridException)
				{
					descriptor.AddProperty("_clientDataSourceID", this.ClientDataSourceID);
				}
			}
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00044054 File Offset: 0x00042254
		private object ExtractDataKeyValue(object dataItem, string name)
		{
			DataRowView dataRowView = dataItem as DataRowView;
			DataRow dataRow = dataItem as DataRow;
			object result;
			if (dataRowView != null)
			{
				result = dataRowView[name];
			}
			else if (dataRow != null)
			{
				result = dataRow[name];
			}
			else
			{
				if (name.Contains("."))
				{
					try
					{
						return DataBinder.GetPropertyValue(dataItem, name);
					}
					catch
					{
						return DataBinder.Eval(dataItem, name);
					}
				}
				result = DataBinder.GetPropertyValue(dataItem, name);
			}
			return result;
		}

		// Token: 0x04000514 RID: 1300
		internal const string DataSourceItemCountControlStateKey = "_!DSIC";

		// Token: 0x04000515 RID: 1301
		internal const string ItemCountControlStateKey = "_!ItemCount";

		// Token: 0x04000516 RID: 1302
		internal const string PageCountViewStateKey = "_!PCount";

		// Token: 0x04000517 RID: 1303
		internal const string ClientPostbackFunctionFormat = "FireCommand:{0}|;{1}|;";

		// Token: 0x04000518 RID: 1304
		internal const string FirstLevelDataGroupKey = "__0level";

		// Token: 0x04000519 RID: 1305
		private const int _defaultPageSize = 1;

		// Token: 0x0400051A RID: 1306
		public const string PageCommandName = "Page";

		// Token: 0x0400051B RID: 1307
		public const string EditCommandName = "Edit";

		// Token: 0x0400051C RID: 1308
		public const string UpdateCommandName = "Update";

		// Token: 0x0400051D RID: 1309
		public const string CancelCommandName = "Cancel";

		// Token: 0x0400051E RID: 1310
		public const string DeleteCommandName = "Delete";

		// Token: 0x0400051F RID: 1311
		public const string PerformInsertCommandName = "PerformInsert";

		// Token: 0x04000520 RID: 1312
		public const string InitInsertCommandName = "InitInsert";

		// Token: 0x04000521 RID: 1313
		public const string RebindDataFormCommandName = "RebindDataForm";

		// Token: 0x04000522 RID: 1314
		internal static readonly string CurrentAssemblyName = Assembly.GetExecutingAssembly().FullName;

		// Token: 0x04000523 RID: 1315
		private static TFunc<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x04000524 RID: 1316
		private static TFunc<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};

		// Token: 0x04000525 RID: 1317
		private static readonly object EventItemCommand;

		// Token: 0x04000526 RID: 1318
		private static readonly object EventItemCreated;

		// Token: 0x04000527 RID: 1319
		private static readonly object EventItemDataBound;

		// Token: 0x04000528 RID: 1320
		private static readonly object EventLayoutCreated = new object();

		// Token: 0x04000529 RID: 1321
		private static readonly object EventNeedDataSource;

		// Token: 0x0400052A RID: 1322
		private static readonly object EventPageIndexChanged;

		// Token: 0x0400052B RID: 1323
		private static readonly object EventTotalRowCountAvailable;

		// Token: 0x0400052C RID: 1324
		private static readonly object EventTotalRowCountAvailableAsp;

		// Token: 0x0400052D RID: 1325
		private static readonly object EventSelectedIndexChanged;

		// Token: 0x0400052E RID: 1326
		private static readonly object EventItemEditing;

		// Token: 0x0400052F RID: 1327
		private static readonly object EventItemUpdating;

		// Token: 0x04000530 RID: 1328
		private static readonly object EventItemCanceling;

		// Token: 0x04000531 RID: 1329
		private static readonly object EventItemUpdated;

		// Token: 0x04000532 RID: 1330
		private static readonly object EventItemDeleting;

		// Token: 0x04000533 RID: 1331
		private static readonly object EventItemDeleted;

		// Token: 0x04000534 RID: 1332
		private static readonly object EventItemInserting;

		// Token: 0x04000535 RID: 1333
		private static readonly object EventItemInserted;

		// Token: 0x04000536 RID: 1334
		private static readonly object EventFieldDescriptorsReady;

		// Token: 0x04000537 RID: 1335
		private DataFormControlLocator _controlLocator;

		// Token: 0x04000538 RID: 1336
		private DataFormControlStateManager _controlStateManager;

		// Token: 0x04000539 RID: 1337
		private List<DataKey> _dataKeysArrayList;

		// Token: 0x0400053A RID: 1338
		private DataFormDataSourceHelper _dataSourceHelper;

		// Token: 0x0400053B RID: 1339
		private RadDataFormDataItemCollection _items;

		// Token: 0x0400053C RID: 1340
		private DataFormEnumerableBase _resolvedDataSource;

		// Token: 0x0400053D RID: 1341
		private Control _itemsWrapperContainer;

		// Token: 0x0400053E RID: 1342
		private bool _instantiatedEmptyDataTemplate;

		// Token: 0x0400053F RID: 1343
		private int _placeholderControlIndex;

		// Token: 0x04000540 RID: 1344
		private int _itemsCreatedInContainerCount;

		// Token: 0x04000541 RID: 1345
		private int _autoIDIndex;

		// Token: 0x04000542 RID: 1346
		private int _editIndex = -1;

		// Token: 0x04000543 RID: 1347
		private RadDataFormDataKeyArray _dataKeyValues;

		// Token: 0x04000544 RID: 1348
		private RadDataFormValidationSettings _validationSettings;

		// Token: 0x04000545 RID: 1349
		private bool _shouldCallDataBindOnLoad = true;

		// Token: 0x04000546 RID: 1350
		private RadDataFormClientSettings _clientSettings;

		// Token: 0x04000547 RID: 1351
		private bool _isModelValid = true;

		// Token: 0x04000548 RID: 1352
		private bool _ignoreDataSourceViewChanged;

		// Token: 0x04000549 RID: 1353
		private DataSourceView _currentDataSource;

		// Token: 0x0400054A RID: 1354
		private bool _pagePreLoadFired;

		// Token: 0x0400054B RID: 1355
		private IDictionary _insertObjectDefaultValues;

		// Token: 0x0400054C RID: 1356
		private object _insertObject;

		// Token: 0x02000201 RID: 513
		private class DummyDataSource : DataSourceControl
		{
			// Token: 0x06001333 RID: 4915 RVA: 0x000440C4 File Offset: 0x000422C4
			public DummyDataSource(IEnumerable source)
			{
				this._source = source;
			}

			// Token: 0x06001334 RID: 4916 RVA: 0x000440D3 File Offset: 0x000422D3
			protected override DataSourceView GetView(string viewName)
			{
				return new RadDataForm.DummyDataSource.DummyDataView(this, viewName, this._source);
			}

			// Token: 0x0400055C RID: 1372
			private IEnumerable _source;

			// Token: 0x02000202 RID: 514
			private class DummyDataView : DataSourceView
			{
				// Token: 0x06001335 RID: 4917 RVA: 0x000440E2 File Offset: 0x000422E2
				public DummyDataView(IDataSource owner, string viewName, IEnumerable source) : base(owner, viewName)
				{
					this._source = source;
				}

				// Token: 0x06001336 RID: 4918 RVA: 0x000440F3 File Offset: 0x000422F3
				protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
				{
					return this._source;
				}

				// Token: 0x0400055D RID: 1373
				private IEnumerable _source;
			}
		}

		// Token: 0x02000203 RID: 515
		private class Messages
		{
			// Token: 0x0400055E RID: 1374
			public const string GroupItemCountOutOfRange = "The GroupItemCount property is set to a value less than 1.";

			// Token: 0x0400055F RID: 1375
			public const string NotInInsertMode = "Insert item is available only when RadDataForm is in insert mode.";

			// Token: 0x04000560 RID: 1376
			public const string BindCalledDuringNeedDataSourceException = "You should not call DataBind in NeedDataSource event handler. DataBind would take place automatically right after NeedDataSource handler finishes execution.";

			// Token: 0x04000561 RID: 1377
			public const string ItemTemplateIsRequired = "The RadDataForm control does not have an ItemTemplate template specified.";

			// Token: 0x04000562 RID: 1378
			public const string NoItemPlaceholderIdError = "The RadDataForm control does not have an item placeholder specified.";

			// Token: 0x04000563 RID: 1379
			public const string ThereWasAProblemExtractingDataKeyValuesFromTheDataSource = "There was a problem extracting DataKeyValues from the DataSource. Please ensure that DataKeyNames are specified correctly and all fields specified exist in the DataSource.";

			// Token: 0x04000564 RID: 1380
			public const string InsertItemTemplateRequired = "The RadDataForm control does not have an InsertItemTemplate template specified.";
		}
	}
}
