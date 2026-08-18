using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x020018BC RID: 6332
	[EmbeddedSkin("Filter", typeof(RadFilter))]
	[ParseChildren(true)]
	[Description("Telerik RadFilter")]
	[EmbeddedSkin("Filter", "Default", typeof(RadFilter))]
	[ClientScriptResource("Telerik.Web.UI.RadFilter", "Telerik.Web.UI.Filter.RadFilterScripts.js")]
	[ToolboxBitmap(typeof(RadFilter), "Telerik.Web.UI.Filter.png")]
	[ToolboxData("<{0}:RadFilter runat=server></{0}:RadFilter>")]
	[TelerikToolboxCategory("Miscellaneous")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadFilter))]
	[PersistChildren(false)]
	[Designer("Telerik.Web.Design.RadFilterDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadFilter))]
	[LightweightRendering]
	[RequiredScript(typeof(MaterialRipple))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadFilter : RadWebControl, INamingContainer, IPostBackEventHandler, ILocalizableControl
	{
		// Token: 0x170049C5 RID: 18885
		// (get) Token: 0x0600F4B6 RID: 62646 RVA: 0x00379314 File Offset: 0x00377514
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170049C6 RID: 18886
		// (get) Token: 0x0600F4B7 RID: 62647 RVA: 0x00379317 File Offset: 0x00377517
		protected bool ShouldSearchForContainer
		{
			get
			{
				return !string.IsNullOrEmpty(this.FilterContainerID);
			}
		}

		// Token: 0x170049C7 RID: 18887
		// (get) Token: 0x0600F4B8 RID: 62648 RVA: 0x00379327 File Offset: 0x00377527
		// (set) Token: 0x0600F4B9 RID: 62649 RVA: 0x0037932F File Offset: 0x0037752F
		protected bool IsAttchedToContainer { get; set; }

		// Token: 0x170049C8 RID: 18888
		// (get) Token: 0x0600F4BA RID: 62650 RVA: 0x00379338 File Offset: 0x00377538
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x0600F4BB RID: 62651 RVA: 0x00379340 File Offset: 0x00377540
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.ShouldSearchForContainer)
			{
				this.FilterContainer = this.ContainerLocator.RetrieveFilterableContainer(this, this.FilterContainerID, new List<string>
				{
					this.ID
				});
				if (this.FilterContainer != null)
				{
					this.AttachToContainer();
					this.IsAttchedToContainer = true;
				}
			}
			if (!string.IsNullOrEmpty(this.DataSourceControlID) && !this.IsAttchedToContainer)
			{
				this.DataSourceControl = this.ContainerLocator.RetrieveDataSourceControl(this, this.DataSourceControlID, new List<string>
				{
					this.ID
				});
				this.AttachToDataSource();
			}
		}

		// Token: 0x0600F4BC RID: 62652 RVA: 0x003793E4 File Offset: 0x003775E4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.contextMenu != null)
			{
				this.contextMenu.RegisterWithScriptManager = true;
			}
			if (this.ShouldSearchForContainer)
			{
				if (this.FilterContainer == null)
				{
					this.FilterContainer = this.ContainerLocator.RetrieveFilterableContainer(this, this.FilterContainerID, new List<string>
					{
						this.ID
					});
				}
				if (this.FilterContainer != null && !this.IsAttchedToContainer)
				{
					this.AttachToContainer();
					this.IsAttchedToContainer = true;
					return;
				}
			}
			else if (!string.IsNullOrEmpty(this.DataSourceControlID) && this.DataSourceControl == null)
			{
				this.DataSourceControl = this.ContainerLocator.RetrieveDataSourceControl(this, this.DataSourceControlID, new List<string>
				{
					this.ID
				});
				this.AttachToDataSource();
			}
		}

		// Token: 0x0600F4BD RID: 62653 RVA: 0x003794AB File Offset: 0x003776AB
		protected void ApplyExpressionsToContainer(bool shouldBind)
		{
			this.FilterContainer.ApplyFilterExpressions(this.RootGroup, shouldBind);
		}

		// Token: 0x0600F4BE RID: 62654 RVA: 0x003794BF File Offset: 0x003776BF
		protected virtual void AttachToContainer()
		{
			this.FilterContainer.FieldDescriptorsReady += this.ContainerFieldDescriptorsReady;
		}

		// Token: 0x0600F4BF RID: 62655 RVA: 0x003794D8 File Offset: 0x003776D8
		protected void ContainerFieldDescriptorsReady(object sender, RadFilterFildDesciptorsEventArgs e)
		{
			this.FetchContainerArguments(e.FilterableView);
			this.RecreateControl();
		}

		// Token: 0x170049C9 RID: 18889
		// (get) Token: 0x0600F4C0 RID: 62656 RVA: 0x003794EC File Offset: 0x003776EC
		// (set) Token: 0x0600F4C1 RID: 62657 RVA: 0x003794F4 File Offset: 0x003776F4
		[SimplePersistenceSetting]
		internal string FilterExpressions
		{
			get
			{
				return this.SaveSettings();
			}
			set
			{
				this.LoadSettings(value);
			}
		}

		// Token: 0x0600F4C2 RID: 62658 RVA: 0x003794FD File Offset: 0x003776FD
		private void FetchContainerArguments(RadFilterFilterableView filterableView)
		{
			this.FieldDescriptors = filterableView.DataFields;
			this.SupportedFilterFunctions = filterableView.SupportedFilterFunctions;
			this.SupportedGroupTypes = filterableView.SupportedGroupTypes;
			this.PreparedFieldEditors();
		}

		// Token: 0x0600F4C3 RID: 62659 RVA: 0x0037954C File Offset: 0x0037774C
		private void PreparedFieldEditors()
		{
			RadFilterDataFieldEditor[] array = new RadFilterDataFieldEditor[this.FieldEditors.Count];
			this.FieldEditors.CopyTo(array, 0);
			List<RadFilterDataFieldEditor> list = new List<RadFilterDataFieldEditor>(array);
			this.FieldEditors.Clear();
			using (IEnumerator<RadFilterFieldDescriptor> enumerator = this.FieldDescriptors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RadFilterFieldDescriptor descriptor = enumerator.Current;
					RadFilterDataFieldEditor radFilterDataFieldEditor = list.Find((RadFilterDataFieldEditor current) => current.FieldName == descriptor.FieldName);
					if (radFilterDataFieldEditor != null)
					{
						this.FieldEditors.Add(radFilterDataFieldEditor);
					}
					else
					{
						RadFilterDataFieldEditor radFilterDataFieldEditor2;
						if (RadFilterTypeHelper.IsNumericType(descriptor.DataType))
						{
							radFilterDataFieldEditor2 = new RadFilterNumericFieldEditor();
						}
						else if (RadFilterTypeHelper.GetNonNullableType(descriptor.DataType) == typeof(bool))
						{
							radFilterDataFieldEditor2 = new RadFilterBooleanFieldEditor();
						}
						else if (RadFilterTypeHelper.IsDateType(descriptor.DataType))
						{
							radFilterDataFieldEditor2 = new RadFilterDateFieldEditor();
						}
						else
						{
							radFilterDataFieldEditor2 = new RadFilterTextFieldEditor();
						}
						this.FieldEditors.Add(radFilterDataFieldEditor2);
						radFilterDataFieldEditor2.DataType = descriptor.DataType;
						radFilterDataFieldEditor2.FieldName = descriptor.FieldName;
						radFilterDataFieldEditor2.DisplayName = descriptor.DisplayName;
						radFilterDataFieldEditor = radFilterDataFieldEditor2;
					}
					this.CallOnFieldEditorCreated(new RadFilterFieldEditorCreatedEventArgs(radFilterDataFieldEditor));
				}
			}
		}

		// Token: 0x0600F4C4 RID: 62660 RVA: 0x003796D0 File Offset: 0x003778D0
		internal void CallOnExpressionItemCreated(RadFilterExpressionItemCreatedEventArgs e)
		{
			this.OnExpressionItemCreated(e);
		}

		// Token: 0x0600F4C5 RID: 62661 RVA: 0x003796D9 File Offset: 0x003778D9
		internal void CallOnFieldEditorCreating(RadFilterFieldEditorCreatingEventArgs e)
		{
			this.OnFieldEditorCreating(e);
		}

		// Token: 0x0600F4C6 RID: 62662 RVA: 0x003796E2 File Offset: 0x003778E2
		internal void CallOnFieldEditorCreated(RadFilterFieldEditorCreatedEventArgs e)
		{
			this.OnFieldEditorCreated(e);
		}

		// Token: 0x0600F4C7 RID: 62663 RVA: 0x003796EB File Offset: 0x003778EB
		internal bool isGroupSupported(RadFilterGroupOperation operation)
		{
			return this.SupportedGroupTypes == null || this.SupportedFilterFunctions.Count == 0 || this.SupportedGroupTypes.Contains(operation);
		}

		// Token: 0x0600F4C8 RID: 62664 RVA: 0x00379710 File Offset: 0x00377910
		internal bool isFilterFunctionSupported(RadFilterFunction function)
		{
			return function != RadFilterFunction.Group && (this.SupportedFilterFunctions == null || this.SupportedFilterFunctions.Count == 0 || this.SupportedFilterFunctions.Contains(function));
		}

		// Token: 0x0600F4C9 RID: 62665 RVA: 0x0037973C File Offset: 0x0037793C
		protected void EnsureItemsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x0600F4CA RID: 62666 RVA: 0x00379744 File Offset: 0x00377944
		protected override void CreateChildControls()
		{
			if (this.contextMenu != null)
			{
				if (this._isAfterPrerender)
				{
					this.contextMenu.RegisterWithScriptManager = false;
				}
				else
				{
					this.contextMenu.RegisterWithScriptManager = true;
				}
			}
			this.Controls.Clear();
			base.CreateChildControls();
			this.CreateControlHierarchy();
		}

		// Token: 0x0600F4CB RID: 62667 RVA: 0x00379794 File Offset: 0x00377994
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this._isAfterPrerender = true;
			if (this.ExpressionPreviewPosition != RadFilterExpressionPreviewPosition.None)
			{
				this.FetchExpressionsValues(this.RootGroupItem);
				RadFilterQueryProvider expressionPreviewProvider = this.ExpressionPreviewProvider;
				expressionPreviewProvider.ProcessGroup(this.RootGroup);
				string arg = (this.ExpressionPreviewPosition == RadFilterExpressionPreviewPosition.Top) ? " rfPreviewTop" : "";
				this.ExpressionPreviewHolder.Text = string.Format("<div class=\"rfPreview{1}\">{0}</div>", expressionPreviewProvider.Result, arg);
			}
		}

		// Token: 0x0600F4CC RID: 62668 RVA: 0x00379808 File Offset: 0x00377A08
		protected virtual void CreateControlHierarchy()
		{
			if (this.ExpressionPreviewPosition == RadFilterExpressionPreviewPosition.Top)
			{
				this.CreateExpressionPreview();
			}
			this.CreateFilterItems();
			if (this.ExpressionPreviewPosition == RadFilterExpressionPreviewPosition.Bottom)
			{
				this.CreateExpressionPreview();
			}
			this.CreateApplyButton();
			this.CreateContextMenu();
		}

		// Token: 0x0600F4CD RID: 62669 RVA: 0x0037983C File Offset: 0x00377A3C
		private void CreateDummyGroupItem()
		{
			RadFilterGroupExpressionItem radFilterGroupExpressionItem = new RadFilterGroupExpressionItem(this.RootGroup, false);
			radFilterGroupExpressionItem.InitializeItem();
			this.Controls.Add(radFilterGroupExpressionItem);
		}

		// Token: 0x0600F4CE RID: 62670 RVA: 0x00379868 File Offset: 0x00377A68
		protected void CreateContextMenu()
		{
			this.Controls.Add(this.ContextMenu);
			this.ContextMenu.BuildContextMenuItems();
			this.ContextMenu.Visible = !base.DesignMode;
		}

		// Token: 0x0600F4CF RID: 62671 RVA: 0x0037989A File Offset: 0x00377A9A
		protected void CreateApplyButton()
		{
			this.ApplyButton.Visible = this.ShowApplyButton;
			this.ApplyButton.Text = this.ApplyButtonText;
			this.Controls.Add(this.ApplyButton);
		}

		// Token: 0x0600F4D0 RID: 62672 RVA: 0x003798D0 File Offset: 0x00377AD0
		protected void CreateFilterItems()
		{
			RadFilterItemBuilder radFilterItemBuilder = new RadFilterItemBuilder();
			RadFilterExpressionContainer radFilterExpressionContainer = new RadFilterExpressionContainer();
			bool flag = true;
			foreach (RadFilterExpressionItem radFilterExpressionItem in radFilterItemBuilder.BuildNextItem(this.RootGroup, null))
			{
				radFilterExpressionItem.SetOwnerFilter(this);
				if (flag)
				{
					flag = false;
					this._rootGroupItem = (RadFilterGroupExpressionItem)radFilterExpressionItem;
					radFilterExpressionItem.InitializeItem();
					radFilterExpressionContainer.Controls.Add(radFilterExpressionItem);
					radFilterExpressionContainer.ShowLineImages = this.ShowLineImages;
				}
				else
				{
					radFilterExpressionItem.InitializeItem();
					radFilterItemBuilder.AddItem(radFilterExpressionItem);
				}
				this.CallOnExpressionItemCreated(new RadFilterExpressionItemCreatedEventArgs(radFilterExpressionItem));
			}
			this.Controls.Add(radFilterExpressionContainer);
		}

		// Token: 0x170049CA RID: 18890
		// (get) Token: 0x0600F4D1 RID: 62673 RVA: 0x00379990 File Offset: 0x00377B90
		protected LiteralControl ExpressionPreviewHolder
		{
			get
			{
				if (this._expressionPreviewHolder == null)
				{
					this._expressionPreviewHolder = new LiteralControl();
				}
				return this._expressionPreviewHolder;
			}
		}

		// Token: 0x0600F4D2 RID: 62674 RVA: 0x003799AB File Offset: 0x00377BAB
		protected void CreateExpressionPreview()
		{
			this.Controls.Add(this.ExpressionPreviewHolder);
		}

		// Token: 0x0600F4D3 RID: 62675 RVA: 0x003799C0 File Offset: 0x00377BC0
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool flag = false;
			if (args is RadFilterCommandEventArgs)
			{
				RadFilterCommandEventArgs e = (RadFilterCommandEventArgs)args;
				this.OnItemCommand(e);
				flag = true;
			}
			if (args is IRadFilterCommandEvent)
			{
				IRadFilterCommandEvent radFilterCommandEvent = (IRadFilterCommandEvent)args;
				if (!radFilterCommandEvent.Canceled)
				{
					radFilterCommandEvent.ExecuteCommand(source);
				}
				flag = true;
			}
			if (!flag && args is CommandEventArgs)
			{
				RadFilterCommandEventArgs radFilterCommandEventArgs = RadFilterCommandEventArgsFactory.CreateCommandEventArgs(null, source, args as CommandEventArgs);
				if (RadFilterCommandEventArgsFactory.ShouldHandleCommandInternal(args as CommandEventArgs))
				{
					this.OnItemCommand(radFilterCommandEventArgs);
					if (!radFilterCommandEventArgs.Canceled)
					{
						RadFilterCommandEventArgsFactory.HandleCommand(this, source, radFilterCommandEventArgs);
					}
				}
				else
				{
					this.OnItemCommand(radFilterCommandEventArgs);
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x170049CB RID: 18891
		// (get) Token: 0x0600F4D4 RID: 62676 RVA: 0x00379A50 File Offset: 0x00377C50
		protected override string CssClassFormatString
		{
			get
			{
				if (string.IsNullOrEmpty(base.RuntimeSkin))
				{
					return "RadFilter";
				}
				return "RadFilter RadFilter_{0}";
			}
		}

		// Token: 0x170049CC RID: 18892
		// (get) Token: 0x0600F4D5 RID: 62677 RVA: 0x00379A6A File Offset: 0x00377C6A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600F4D6 RID: 62678 RVA: 0x00379A9C File Offset: 0x00377C9C
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
				writer.Write("<style type='text/css'>.rfTools{width:70px;}</style>");
				TFunc<int, string> tfunc = delegate(int lettersCount)
				{
					string format = "<style type='text/css'>div.RadFilter .rfApply a{{width:{0}px;padding:0;}}.rfApply a input{{width:{1}px;}}</style>";
					int num = lettersCount * 10;
					return string.Format(format, num, num - 4);
				};
				writer.Write(tfunc(this.ApplyButtonText.Length));
				this.RecreateControl();
			}
			base.Render(writer);
		}

		// Token: 0x0600F4D7 RID: 62679 RVA: 0x00379B0C File Offset: 0x00377D0C
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				base.LoadViewState(array[0]);
				((IStateManager)this.RootGroup).LoadViewState(array[1]);
				((IStateManager)this.FieldEditors).LoadViewState(array[2]);
				this.LoadSupportedOperations(array[3] as Pair);
				((IStateManager)this.ClientSettings).LoadViewState(array[4]);
				return;
			}
			base.LoadViewState(savedState);
		}

		// Token: 0x0600F4D8 RID: 62680 RVA: 0x00379B6C File Offset: 0x00377D6C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.RootGroup).SaveViewState(),
				((IStateManager)this.FieldEditors).SaveViewState(),
				this.SaveSupportedOperations(),
				((IStateManager)this.ClientSettings).SaveViewState()
			};
		}

		// Token: 0x0600F4D9 RID: 62681 RVA: 0x00379BBD File Offset: 0x00377DBD
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.RootGroup).TrackViewState();
			((IStateManager)this.FieldEditors).TrackViewState();
		}

		// Token: 0x0600F4DA RID: 62682 RVA: 0x00379BDC File Offset: 0x00377DDC
		protected virtual Pair SaveSupportedOperations()
		{
			Pair pair = new Pair();
			ArrayList arrayList = new ArrayList();
			if (this.SupportedGroupTypes != null)
			{
				foreach (RadFilterGroupOperation radFilterGroupOperation in this.SupportedGroupTypes)
				{
					arrayList.Add(radFilterGroupOperation.ToString());
				}
			}
			pair.First = arrayList.ToArray(typeof(object));
			arrayList = new ArrayList();
			if (this.SupportedFilterFunctions != null)
			{
				foreach (RadFilterFunction radFilterFunction in this.SupportedFilterFunctions)
				{
					arrayList.Add(radFilterFunction.ToString());
				}
			}
			pair.Second = arrayList.ToArray(typeof(object));
			return pair;
		}

		// Token: 0x0600F4DB RID: 62683 RVA: 0x00379CD4 File Offset: 0x00377ED4
		protected virtual void LoadSupportedOperations(Pair statePair)
		{
			object[] array = statePair.First as object[];
			object[] array2 = statePair.Second as object[];
			if (array.Length > 0)
			{
				this.SupportedGroupTypes = new List<RadFilterGroupOperation>();
				foreach (string value in array)
				{
					this.SupportedGroupTypes.Add((RadFilterGroupOperation)Enum.Parse(typeof(RadFilterGroupOperation), value));
				}
			}
			if (array2.Length > 0)
			{
				this.SupportedFilterFunctions = new List<RadFilterFunction>();
				foreach (string value2 in array2)
				{
					this.SupportedFilterFunctions.Add((RadFilterFunction)Enum.Parse(typeof(RadFilterFunction), value2));
				}
			}
		}

		// Token: 0x0600F4DC RID: 62684 RVA: 0x00379DB4 File Offset: 0x00377FB4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			this._expressionsList.Clear();
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			descriptor.AddProperty("_clientID", this.ClientID);
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddProperty("_dataFields", javaScriptSerializer.Serialize(this.DescribeDataFields()));
			this._expressionsList.Add(this.RootGroupItem.HierarchicalIndex, "");
			this.DescribeExpressions(this.RootGroupItem);
			descriptor.AddProperty("_expressionItems", javaScriptSerializer.Serialize(this._expressionsList));
			this.DescribeFieldEditorTypes(descriptor);
			base.DescribeRenderMode(descriptor);
			if (this.IsClientOperationMode)
			{
				descriptor.AddProperty("_isClientOperationMode", this.OperationMode);
			}
			if (this.UseBetweenValidation)
			{
				descriptor.AddProperty("_useBetweenValidation", this.UseBetweenValidation);
			}
			if (this.EnableAriaSupport)
			{
				descriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			this.RegisterClientSideEvents(delegate(string eventName, string eventValue)
			{
				RadWebControl.DescribeEvent(descriptor, eventName, eventValue);
			});
		}

		// Token: 0x0600F4DD RID: 62685 RVA: 0x00379F20 File Offset: 0x00378120
		private void DescribeFieldEditorTypes(IScriptDescriptor descriptor)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (RadFilterDataFieldEditor radFilterDataFieldEditor in this.FieldEditors)
			{
				if (radFilterDataFieldEditor is RadFilterDropDownEditor)
				{
					dictionary.Add(radFilterDataFieldEditor.FieldName, "DropDown");
				}
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddProperty("_editorTypes", javaScriptSerializer.Serialize(dictionary));
		}

		// Token: 0x0600F4DE RID: 62686 RVA: 0x00379FA0 File Offset: 0x003781A0
		public List<Dictionary<string, object>> DescribeDataFields()
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (RadFilterDataFieldEditor radFilterDataFieldEditor in this.FieldEditors)
			{
				list.Add(new Dictionary<string, object>
				{
					{
						"FieldName",
						radFilterDataFieldEditor.FieldName
					},
					{
						"DataType",
						radFilterDataFieldEditor.DataType.ToString()
					}
				});
			}
			return list;
		}

		// Token: 0x0600F4DF RID: 62687 RVA: 0x0037A024 File Offset: 0x00378224
		public void DescribeExpressions(RadFilterGroupExpressionItem rootItem)
		{
			foreach (RadFilterExpressionItem radFilterExpressionItem in rootItem.ChildItems)
			{
				RadFilterSingleExpressionItem radFilterSingleExpressionItem = radFilterExpressionItem as RadFilterSingleExpressionItem;
				if (radFilterSingleExpressionItem != null)
				{
					this._expressionsList.Add(radFilterExpressionItem.HierarchicalIndex, radFilterSingleExpressionItem.Expression.FieldName);
				}
				else
				{
					this._expressionsList.Add(radFilterExpressionItem.HierarchicalIndex, "");
					this.DescribeExpressions((RadFilterGroupExpressionItem)radFilterExpressionItem);
				}
			}
		}

		// Token: 0x0600F4E0 RID: 62688 RVA: 0x0037A0C4 File Offset: 0x003782C4
		private void RegisterClientSideEvents(TAction<string, string> eventData)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ClientSettings.ClientEvents);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!(propertyDescriptor.DisplayName == "ViewState"))
				{
					string text = propertyDescriptor.DisplayName.Replace("On", string.Empty);
					text = Regex.Replace(text, "^[A-Z]", (Match match) => match.ToString().ToLower());
					string text2 = propertyDescriptor.GetValue(this.ClientSettings.ClientEvents).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						eventData(text, text2);
					}
				}
			}
		}

		// Token: 0x0600F4E1 RID: 62689 RVA: 0x0037A1A8 File Offset: 0x003783A8
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			if (clientState.ContainsKey("clientChanges"))
			{
				Dictionary<string, object> dictionary = clientState["clientChanges"] as Dictionary<string, object>;
				Dictionary<string, object> dictionary2 = clientState["removedItems"] as Dictionary<string, object>;
				foreach (RadFilterExpressionItem radFilterExpressionItem in this.GetAllExpressionItems())
				{
					if (dictionary2 != null && dictionary2.ContainsKey(radFilterExpressionItem.HierarchicalIndex))
					{
						RadFilterSingleExpressionItem radFilterSingleExpressionItem = radFilterExpressionItem as RadFilterSingleExpressionItem;
						RadFilterGroupExpressionItem radFilterGroupExpressionItem = radFilterExpressionItem as RadFilterGroupExpressionItem;
						if (radFilterSingleExpressionItem != null)
						{
							this.RemoveFilterExpression(radFilterSingleExpressionItem, false);
						}
						else if (radFilterGroupExpressionItem != null)
						{
							this.RemoveGroupFilterExpression(radFilterGroupExpressionItem, false);
						}
					}
					else if (dictionary != null && dictionary.ContainsKey(radFilterExpressionItem.HierarchicalIndex))
					{
						string value = dictionary[radFilterExpressionItem.HierarchicalIndex].ToString();
						RadFilterSingleExpressionItem radFilterSingleExpressionItem2 = radFilterExpressionItem as RadFilterSingleExpressionItem;
						RadFilterGroupExpressionItem radFilterGroupExpressionItem2 = radFilterExpressionItem as RadFilterGroupExpressionItem;
						if (radFilterSingleExpressionItem2 != null)
						{
							this.ChangeFilterFunction(radFilterSingleExpressionItem2, (RadFilterFunction)Enum.Parse(typeof(RadFilterFunction), value), false);
						}
						else if (radFilterGroupExpressionItem2 != null)
						{
							this.ChangeGroupOperator(radFilterGroupExpressionItem2, (RadFilterGroupOperation)Enum.Parse(typeof(RadFilterGroupOperation), value), false);
						}
					}
					else if (clientState.ContainsKey("newFieldName"))
					{
						string[] array = clientState["newFieldName"].ToString().Split(new string[]
						{
							"||"
						}, StringSplitOptions.RemoveEmptyEntries);
						if (array.Length >= 2)
						{
							string b = array[0];
							string fieldName = array[1];
							if (radFilterExpressionItem.HierarchicalIndex == b)
							{
								this.ChangeExpressionFieldName(radFilterExpressionItem as RadFilterSingleExpressionItem, fieldName);
							}
						}
					}
				}
			}
			if (clientState.ContainsKey("newItems"))
			{
				string newSingleExpressionName = null;
				if (clientState.ContainsKey("newSingleExpressionItemName"))
				{
					newSingleExpressionName = clientState["newSingleExpressionItemName"].ToString();
				}
				Dictionary<string, object> removedItems = null;
				if (clientState.ContainsKey("removedItems"))
				{
					removedItems = (clientState["removedItems"] as Dictionary<string, object>);
				}
				this.CreateNewItems(clientState["newItems"] as Dictionary<string, object>, removedItems, null, newSingleExpressionName);
			}
		}

		// Token: 0x0600F4E2 RID: 62690 RVA: 0x0037A410 File Offset: 0x00378610
		private void CreateNewItems(Dictionary<string, object> items, Dictionary<string, object> removedItems, RadFilterGroupExpression group, string newSingleExpressionName)
		{
			new Dictionary<string, RadFilterGroupExpression>();
			using (Dictionary<string, object>.Enumerator enumerator = items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, object> pair = enumerator.Current;
					if (group == null)
					{
						RadFilterGroupExpressionItem radFilterGroupExpressionItem = this.GetAllExpressionItems().FirstOrDefault(delegate(RadFilterExpressionItem i)
						{
							string hierarchicalIndex = i.HierarchicalIndex;
							KeyValuePair<string, object> pair4 = pair;
							return hierarchicalIndex == pair4.Key;
						}) as RadFilterGroupExpressionItem;
						group = radFilterGroupExpressionItem.Expression;
					}
					if (removedItems != null)
					{
						KeyValuePair<string, object> pair5 = pair;
						if (removedItems.ContainsKey(pair5.Key))
						{
							KeyValuePair<string, object> pair2 = pair;
							if (pair2.Key != "0")
							{
								continue;
							}
						}
					}
					KeyValuePair<string, object> pair3 = pair;
					Dictionary<string, object> dictionary = pair3.Value as Dictionary<string, object>;
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						if (removedItems == null || !removedItems.ContainsKey(keyValuePair.Key))
						{
							RadFilterGroupExpression radFilterGroupExpression = new RadFilterGroupExpression();
							radFilterGroupExpression.GroupOperation = this.DefaultGroupOperation;
							group.AddExpression(radFilterGroupExpression);
							this.CreateNewItems(keyValuePair.Value as Dictionary<string, object>, removedItems, radFilterGroupExpression, newSingleExpressionName);
							if (newSingleExpressionName == keyValuePair.Key)
							{
								this.AddChildExpression(radFilterGroupExpression, false);
							}
						}
					}
				}
			}
			this.RecreateControl();
		}

		// Token: 0x0600F4E3 RID: 62691 RVA: 0x0037A5A4 File Offset: 0x003787A4
		private string GetValue(object value)
		{
			return (value as Dictionary<string, object>)["name"].ToString();
		}

		// Token: 0x0600F4E4 RID: 62692 RVA: 0x0037A5BB File Offset: 0x003787BB
		private int GetCount(object value)
		{
			return int.Parse((value as Dictionary<string, object>)["count"].ToString());
		}

		// Token: 0x0600F4E5 RID: 62693 RVA: 0x0037A5D7 File Offset: 0x003787D7
		public void RaisePostBackEvent(string eventArgument)
		{
			if (eventArgument.Contains("FireCommand:"))
			{
				this.HandleClientCommand(RadFilter.parseFireCommandEventName(eventArgument), RadFilter.parseFireCommandArgs(eventArgument));
			}
		}

		// Token: 0x0600F4E6 RID: 62694 RVA: 0x0037A604 File Offset: 0x00378804
		protected virtual void HandleClientCommand(string commandName, string commandArgs)
		{
			string[] array = commandArgs.Split(new string[]
			{
				"||"
			}, StringSplitOptions.RemoveEmptyEntries);
			string hierarchicalIndex = array[0];
			if (commandName != null)
			{
				if (commandName == "AddExpression")
				{
					this.RootGroupItem.ChildItems[hierarchicalIndex].FireCommandEvent("AddExpression", string.Empty);
					return;
				}
				if (commandName == "AddGroup")
				{
					this.RootGroupItem.ChildItems[hierarchicalIndex].FireCommandEvent("AddGroup", string.Empty);
					return;
				}
				if (!(commandName == "ChangeGroupOperator"))
				{
					if (!(commandName == "ChangeFilterFunction"))
					{
						if (commandName == "ChangeExpressionFieldName")
						{
							if (array.Length < 2)
							{
								return;
							}
							string commandArgument = array[1];
							this.RootGroupItem.ChildItems[hierarchicalIndex].FireCommandEvent("ChangeExpressionFieldName", commandArgument);
							return;
						}
					}
					else
					{
						if (array.Length < 2)
						{
							return;
						}
						string commandArgument2 = array[1];
						this.RootGroupItem.ChildItems[hierarchicalIndex].FireCommandEvent("ChangeFilterFunction", commandArgument2);
						return;
					}
				}
				else
				{
					if (array.Length < 2)
					{
						return;
					}
					string commandArgument3 = array[1];
					this.RootGroupItem.ChildItems[hierarchicalIndex].FireCommandEvent("ChangeGroupOperator", commandArgument3);
					return;
				}
			}
			this.OnBubbleEvent(this, new CommandEventArgs(commandName, commandArgs));
		}

		// Token: 0x0600F4E7 RID: 62695 RVA: 0x0037A755 File Offset: 0x00378955
		public void RecreateControl()
		{
			this.FetchExpressionsValues(this.RootGroupItem);
			base.ChildControlsCreated = false;
			this.EnsureItemsCreated();
		}

		// Token: 0x0600F4E8 RID: 62696 RVA: 0x0037A770 File Offset: 0x00378970
		public List<RadFilterExpressionItem> GetAllExpressionItems()
		{
			List<RadFilterExpressionItem> result = new List<RadFilterExpressionItem>();
			this.GetExpressionItems(this.RootGroupItem.ChildItems, result);
			return result;
		}

		// Token: 0x0600F4E9 RID: 62697 RVA: 0x0037A7A4 File Offset: 0x003789A4
		public List<RadFilterSingleExpressionItem> GetSingleExpressionItems()
		{
			List<RadFilterExpressionItem> list = new List<RadFilterExpressionItem>();
			this.GetExpressionItems(this.RootGroupItem.ChildItems, list);
			return (from i in list
			where i is RadFilterSingleExpressionItem
			select i).Cast<RadFilterSingleExpressionItem>().ToList<RadFilterSingleExpressionItem>();
		}

		// Token: 0x0600F4EA RID: 62698 RVA: 0x0037A81C File Offset: 0x00378A1C
		public List<RadFilterSingleExpressionItem> GetSingleExpressionItems(string fieldName)
		{
			List<RadFilterExpressionItem> list = new List<RadFilterExpressionItem>();
			this.GetExpressionItems(this.RootGroupItem.ChildItems, list);
			return (from RadFilterSingleExpressionItem i in 
				from i in list
				where i is RadFilterSingleExpressionItem
				select i
			where i.FieldName == fieldName
			select i).ToList<RadFilterSingleExpressionItem>();
		}

		// Token: 0x0600F4EB RID: 62699 RVA: 0x0037A898 File Offset: 0x00378A98
		public List<RadFilterGroupExpressionItem> GetGroupExpressionItems()
		{
			List<RadFilterExpressionItem> list = new List<RadFilterExpressionItem>();
			this.GetExpressionItems(this.RootGroupItem.ChildItems, list);
			return (from i in list
			where i is RadFilterGroupExpressionItem
			select i).Cast<RadFilterGroupExpressionItem>().ToList<RadFilterGroupExpressionItem>();
		}

		// Token: 0x0600F4EC RID: 62700 RVA: 0x0037A8EC File Offset: 0x00378AEC
		private void GetExpressionItems(RadFilterItemsCollection items, List<RadFilterExpressionItem> result)
		{
			if (result.Count == 0)
			{
				result.Add(this.RootGroupItem);
			}
			foreach (RadFilterExpressionItem radFilterExpressionItem in items)
			{
				result.Add(radFilterExpressionItem);
				RadFilterGroupExpressionItem radFilterGroupExpressionItem = radFilterExpressionItem as RadFilterGroupExpressionItem;
				if (radFilterGroupExpressionItem != null)
				{
					this.GetExpressionItems(radFilterGroupExpressionItem.ChildItems, result);
				}
			}
		}

		// Token: 0x0600F4ED RID: 62701 RVA: 0x0037A960 File Offset: 0x00378B60
		internal void AddChildExpression(RadFilterGroupExpressionItem groupItem, bool isGroup)
		{
			this.AddChildExpression(groupItem.Expression, isGroup);
			this.RecreateControl();
		}

		// Token: 0x0600F4EE RID: 62702 RVA: 0x0037A978 File Offset: 0x00378B78
		internal void AddChildExpression(RadFilterGroupExpression group, bool isGroup)
		{
			RadFilterExpression radFilterExpression;
			if (isGroup)
			{
				radFilterExpression = RadFilterExpression.CreateExpressionFromTypeName(typeof(RadFilterGroupExpression).Name, string.Empty);
				((RadFilterGroupExpression)radFilterExpression).GroupOperation = this.DefaultGroupOperation;
			}
			else
			{
				RadFilterDataFieldEditor radFilterDataFieldEditor = this.FieldEditors[0];
				if (!string.IsNullOrEmpty(this.DefaultFieldEditorFieldName))
				{
					radFilterDataFieldEditor = this.FieldEditors.FindEditorForFieldName(this.DefaultFieldEditorFieldName);
				}
				radFilterExpression = RadFilterExpression.CreateExpressionForFilterFunction(radFilterDataFieldEditor.DefaultFilterFunction, radFilterDataFieldEditor.DataType.FullName);
				((RadFilterNonGroupExpression)radFilterExpression).FieldName = radFilterDataFieldEditor.FieldName;
			}
			group.AddExpression(radFilterExpression);
		}

		// Token: 0x0600F4EF RID: 62703 RVA: 0x0037AA10 File Offset: 0x00378C10
		internal void RemoveFilterExpression(RadFilterSingleExpressionItem item, bool shouldRecreate = true)
		{
			item.OwnerGroup.Expression.Expressions.Remove(item.Expression);
			if (shouldRecreate)
			{
				this.RecreateControl();
			}
		}

		// Token: 0x0600F4F0 RID: 62704 RVA: 0x0037AA38 File Offset: 0x00378C38
		internal void RemoveGroupFilterExpression(RadFilterGroupExpressionItem groupItem, bool shouldRecreate = true)
		{
			if (groupItem.IsRootGroup)
			{
				groupItem.Expression.Expressions.Clear();
			}
			else
			{
				groupItem.OwnerGroup.Expression.Expressions.Remove(groupItem.Expression);
			}
			if (shouldRecreate)
			{
				this.RecreateControl();
			}
		}

		// Token: 0x0600F4F1 RID: 62705 RVA: 0x0037AA84 File Offset: 0x00378C84
		internal void ChangeGroupOperator(RadFilterGroupExpressionItem groupItem, RadFilterGroupOperation groupOperation, bool shouldRecreate = true)
		{
			groupItem.Expression.GroupOperation = groupOperation;
			if (shouldRecreate)
			{
				this.RecreateControl();
			}
		}

		// Token: 0x0600F4F2 RID: 62706 RVA: 0x0037AA9C File Offset: 0x00378C9C
		internal void ChangeFilterFunction(RadFilterSingleExpressionItem item, RadFilterFunction function, bool shouldRecreate = true)
		{
			Type type = this.FieldEditors.RetrieveTypeForEditor(item.Expression.FieldName);
			RadFilterExpression radFilterExpression = RadFilterExpression.CreateExpressionForFilterFunction(function, type.FullName);
			item.OwnerGroup.Expression.Expressions.Remove(item.Expression);
			item.OwnerGroup.Expression.Expressions.Insert(Math.Max(0, Math.Min(item.ItemIndex, item.OwnerGroup.Expression.Expressions.Count)), radFilterExpression);
			((RadFilterNonGroupExpression)radFilterExpression).FieldName = item.Expression.FieldName;
			IRadFilterValueExpression radFilterValueExpression = radFilterExpression as IRadFilterValueExpression;
			if (radFilterValueExpression != null)
			{
				ArrayList values = item.ExtractValues();
				radFilterValueExpression.SetValues(values);
			}
			if (shouldRecreate)
			{
				this.RecreateControl();
			}
		}

		// Token: 0x0600F4F3 RID: 62707 RVA: 0x0037AB80 File Offset: 0x00378D80
		internal void ChangeExpressionFieldName(RadFilterSingleExpressionItem item, string fieldName)
		{
			RadFilterDataFieldEditor radFilterDataFieldEditor = this.FieldEditors.RetrieveEditorForFieldName(fieldName);
			RadFilterExpression radFilterExpression = RadFilterExpression.CreateExpressionForFilterFunction(radFilterDataFieldEditor.DefaultFilterFunction, radFilterDataFieldEditor.DataType.FullName);
			((RadFilterNonGroupExpression)radFilterExpression).FieldName = fieldName;
			item.OwnerGroup.Expression.Expressions.Remove(item.Expression);
			item.OwnerGroup.Expression.Expressions.Insert(Math.Max(0, Math.Min(item.ItemIndex, item.OwnerGroup.Expression.Expressions.Count)), radFilterExpression);
			this._shouldExtractValues = ((string index) => !(index == item.HierarchicalIndex));
			this.RecreateControl();
			this._shouldExtractValues = ((string index) => true);
		}

		// Token: 0x0600F4F4 RID: 62708 RVA: 0x0037AC78 File Offset: 0x00378E78
		internal void HandleApplyCommand()
		{
			this.FetchExpressionsValues(this.RootGroupItem);
			this.OnApplyExpressions(new RadFilterApplyExpressionsEventArgs(this.RootGroup));
			if (this.IsAttchedToContainer && this.FilterContainer != null)
			{
				this.FilterContainer.ApplyFilterExpressions(this.RootGroup, true);
				return;
			}
			if (this.DataSourceControl != null)
			{
				this.ApplyFilterExpressonsOnDataSource();
			}
		}

		// Token: 0x0600F4F5 RID: 62709 RVA: 0x0037ACD4 File Offset: 0x00378ED4
		protected virtual void FetchExpressionsValues(RadFilterGroupExpressionItem group)
		{
			foreach (RadFilterExpressionItem radFilterExpressionItem in group.ChildItems)
			{
				RadFilterSingleExpressionItem radFilterSingleExpressionItem = radFilterExpressionItem as RadFilterSingleExpressionItem;
				if (radFilterSingleExpressionItem != null)
				{
					if (this._shouldExtractValues(radFilterExpressionItem.HierarchicalIndex))
					{
						ArrayList values = radFilterSingleExpressionItem.ExtractValues();
						IRadFilterValueExpression radFilterValueExpression = radFilterSingleExpressionItem.Expression as IRadFilterValueExpression;
						if (radFilterValueExpression != null)
						{
							radFilterValueExpression.SetValues(values);
						}
					}
				}
				else
				{
					this.FetchExpressionsValues(radFilterExpressionItem as RadFilterGroupExpressionItem);
				}
			}
		}

		// Token: 0x170049CD RID: 18893
		// (get) Token: 0x0600F4F6 RID: 62710 RVA: 0x0037AD68 File Offset: 0x00378F68
		// (set) Token: 0x0600F4F7 RID: 62711 RVA: 0x0037AD88 File Offset: 0x00378F88
		protected string AppliedDSExpressions
		{
			get
			{
				return ((string)this.ViewState["AppliedDSExpressions"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["AppliedDSExpressions"] = value;
			}
		}

		// Token: 0x170049CE RID: 18894
		// (get) Token: 0x0600F4F8 RID: 62712 RVA: 0x0037AD9B File Offset: 0x00378F9B
		protected virtual RadFilterQueryProvider GetQueryProvider
		{
			get
			{
				if (this._queryProvider == null)
				{
					this._queryProvider = RadFilter.QueryProviderFactory.GetProvider(this.DataSourceControl);
				}
				return this._queryProvider;
			}
		}

		// Token: 0x0600F4F9 RID: 62713 RVA: 0x0037ADBC File Offset: 0x00378FBC
		protected void AttachToDataSource()
		{
			RadFilter.RadFilterDataSourceHelper.AttachEventHandler("Selected", typeof(RadFilter).GetMethod("OnDataSourceSelectedHandler"), this, this.DataSourceControl);
			RadFilter.RadFilterDataSourceHelper.AttachEventHandler("Selecting", typeof(RadFilter).GetMethod("OnDataSourceSelectingHandler"), this, this.DataSourceControl);
		}

		// Token: 0x0600F4FA RID: 62714 RVA: 0x0037AE20 File Offset: 0x00379020
		public virtual void OnDataSourceSelectedHandler(object sender, EventArgs args)
		{
			SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = args as SqlDataSourceStatusEventArgs;
			IEnumerable dataSource;
			if (sqlDataSourceStatusEventArgs != null)
			{
				dataSource = this.ExtractSqlDataSourceData(sqlDataSourceStatusEventArgs);
			}
			else
			{
				dataSource = this.ExtractResultCollection(args);
			}
			this.FillFieldDescriptors(dataSource);
			this.PreparedFieldEditors();
		}

		// Token: 0x0600F4FB RID: 62715 RVA: 0x0037AE58 File Offset: 0x00379058
		private IEnumerable ExtractSqlDataSourceData(SqlDataSourceStatusEventArgs args)
		{
			DbCommand command = args.Command;
			command.Connection.Open();
			SqlDataReader source = command.ExecuteReader() as SqlDataReader;
			return source.Cast<IDataRecord>();
		}

		// Token: 0x0600F4FC RID: 62716 RVA: 0x0037AE8C File Offset: 0x0037908C
		public virtual void OnDataSourceSelectingHandler(object sender, EventArgs args)
		{
			if (!this._isApplyCommandTriggered && !string.IsNullOrEmpty(this.AppliedDSExpressions))
			{
				object obj = sender;
				PropertyInfo propertyInfo = RadFilter.RadFilterDataSourceHelper.ExtractWhereProperty(sender.GetType());
				if (propertyInfo == null)
				{
					propertyInfo = RadFilter.RadFilterDataSourceHelper.ExtractWhereProperty(this.DataSourceControl.GetType());
					obj = this.DataSourceControl;
				}
				if (propertyInfo == null)
				{
					return;
				}
				propertyInfo.SetValue(obj, this.AppliedDSExpressions, null);
			}
		}

		// Token: 0x0600F4FD RID: 62717 RVA: 0x0037AF00 File Offset: 0x00379100
		protected void FillFieldDescriptors(IEnumerable dataSource)
		{
			ItemPropertiesDescriptor itemPropertiesDescriptor = new ItemPropertiesDescriptor(dataSource);
			this.FieldDescriptors = new List<RadFilterFieldDescriptor>();
			PropertyDescriptorCollection propertyDescriptorCollection = itemPropertiesDescriptor.Process();
			foreach (object obj in propertyDescriptorCollection)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (RadFilter.RadFilterDataSourceHelper.IsBindableType(propertyDescriptor.PropertyType))
				{
					this.FieldDescriptors.Add(new RadFilterFieldDescriptor(propertyDescriptor.DisplayName, propertyDescriptor.PropertyType));
				}
			}
		}

		// Token: 0x0600F4FE RID: 62718 RVA: 0x0037AF94 File Offset: 0x00379194
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		protected IEnumerable ExtractResultCollection(EventArgs args)
		{
			PropertyInfo propertyInfo = RadFilter.RadFilterDataSourceHelper.ExtractResultProperty(args.GetType());
			return (IEnumerable)propertyInfo.GetValue(args, null);
		}

		// Token: 0x0600F4FF RID: 62719 RVA: 0x0037AFC0 File Offset: 0x003791C0
		public void ApplyFilterExpressonsOnDataSource()
		{
			PropertyInfo propertyInfo = RadFilter.RadFilterDataSourceHelper.ExtractWhereProperty(this.DataSourceControl.GetType());
			SqlDataSource sqlDataSource = this.DataSourceControl as SqlDataSource;
			if (propertyInfo != null || sqlDataSource != null)
			{
				RadFilterQueryProvider getQueryProvider = this.GetQueryProvider;
				getQueryProvider.ProcessGroup(this.RootGroup);
				this.AppliedDSExpressions = getQueryProvider.Result;
				this._isApplyCommandTriggered = true;
				if (sqlDataSource != null)
				{
					if (string.IsNullOrEmpty(this.AppliedDSExpressions))
					{
						sqlDataSource.FilterExpression = " ";
						return;
					}
					sqlDataSource.FilterExpression = this.AppliedDSExpressions;
					return;
				}
				else
				{
					propertyInfo.SetValue(this.DataSourceControl, getQueryProvider.Result, null);
				}
			}
		}

		// Token: 0x170049CF RID: 18895
		// (get) Token: 0x0600F500 RID: 62720 RVA: 0x0037B05D File Offset: 0x0037925D
		// (set) Token: 0x0600F501 RID: 62721 RVA: 0x0037B065 File Offset: 0x00379265
		public override RenderMode RenderMode
		{
			get
			{
				return base.RenderMode;
			}
			set
			{
				base.RenderMode = value;
				this.SharedCalendar.RenderMode = value;
			}
		}

		// Token: 0x170049D0 RID: 18896
		// (get) Token: 0x0600F502 RID: 62722 RVA: 0x0037B07A File Offset: 0x0037927A
		public override RenderMode ResolvedRenderMode
		{
			get
			{
				this.SharedCalendar.RenderMode = base.ResolvedRenderMode;
				return base.ResolvedRenderMode;
			}
		}

		// Token: 0x170049D1 RID: 18897
		// (get) Token: 0x0600F503 RID: 62723 RVA: 0x0037B094 File Offset: 0x00379294
		[Browsable(false)]
		public Button ApplyButton
		{
			get
			{
				if (this.applyButton == null)
				{
					this.applyButton = new RadFilterApplyButton(this);
					this.applyButton.ID = "ApplyButton";
					this.applyButton.CausesValidation = false;
					this.applyButton.CommandName = "ApplyExpressions";
				}
				return this.applyButton;
			}
		}

		// Token: 0x170049D2 RID: 18898
		// (get) Token: 0x0600F504 RID: 62724 RVA: 0x0037B0E7 File Offset: 0x003792E7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadFilterContextMenu ContextMenu
		{
			get
			{
				if (this.contextMenu == null)
				{
					this.contextMenu = new RadFilterContextMenu(this);
				}
				return this.contextMenu;
			}
		}

		// Token: 0x170049D3 RID: 18899
		// (get) Token: 0x0600F505 RID: 62725 RVA: 0x0037B103 File Offset: 0x00379303
		// (set) Token: 0x0600F506 RID: 62726 RVA: 0x0037B110 File Offset: 0x00379310
		[NotifyParentProperty(true)]
		[DefaultValue("Apply")]
		[Localizable(true)]
		public string ApplyButtonText
		{
			get
			{
				return this.Localization.ApplyButtonText;
			}
			set
			{
				this.Localization.ApplyButtonText = value;
			}
		}

		// Token: 0x170049D4 RID: 18900
		// (get) Token: 0x0600F507 RID: 62727 RVA: 0x0037B120 File Offset: 0x00379320
		// (set) Token: 0x0600F508 RID: 62728 RVA: 0x0037B149 File Offset: 0x00379349
		public bool UseBetweenValidation
		{
			get
			{
				object obj = this.ViewState["UseBetweenValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["UseBetweenValidation"] = value;
			}
		}

		// Token: 0x170049D5 RID: 18901
		// (get) Token: 0x0600F509 RID: 62729 RVA: 0x0037B164 File Offset: 0x00379364
		// (set) Token: 0x0600F50A RID: 62730 RVA: 0x0037B192 File Offset: 0x00379392
		[NotifyParentProperty(true)]
		[DefaultValue(RadFilterOperationMode.Server)]
		public RadFilterOperationMode OperationMode
		{
			get
			{
				object obj = this.ViewState["OperationMode"] ?? RadFilterOperationMode.Server;
				return (RadFilterOperationMode)obj;
			}
			set
			{
				this.ViewState["OperationMode"] = value;
			}
		}

		// Token: 0x170049D6 RID: 18902
		// (get) Token: 0x0600F50B RID: 62731 RVA: 0x0037B1AA File Offset: 0x003793AA
		internal bool IsClientOperationMode
		{
			get
			{
				return this.OperationMode == RadFilterOperationMode.ServerAndClient;
			}
		}

		// Token: 0x170049D7 RID: 18903
		// (get) Token: 0x0600F50C RID: 62732 RVA: 0x0037B1B8 File Offset: 0x003793B8
		// (set) Token: 0x0600F50D RID: 62733 RVA: 0x0037B1E1 File Offset: 0x003793E1
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowFilterOnBlur
		{
			get
			{
				object obj = this.ViewState["AllowFilterOnBlur"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowFilterOnBlur"] = value;
			}
		}

		// Token: 0x170049D8 RID: 18904
		// (get) Token: 0x0600F50E RID: 62734 RVA: 0x0037B1FC File Offset: 0x003793FC
		// (set) Token: 0x0600F50F RID: 62735 RVA: 0x0037B22A File Offset: 0x0037942A
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ShowAddGroupExpressionButton
		{
			get
			{
				object obj = this.ViewState["ShowAddGroupExpressionButton"] ?? true;
				return (bool)obj;
			}
			set
			{
				this.ViewState["ShowAddGroupExpressionButton"] = value;
			}
		}

		// Token: 0x170049D9 RID: 18905
		// (get) Token: 0x0600F510 RID: 62736 RVA: 0x0037B242 File Offset: 0x00379442
		// (set) Token: 0x0600F511 RID: 62737 RVA: 0x0037B24F File Offset: 0x0037944F
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Add Expression")]
		public string AddExpressionToolTip
		{
			get
			{
				return this.Localization.AddExpressionToolTip;
			}
			set
			{
				this.Localization.AddExpressionToolTip = value;
			}
		}

		// Token: 0x170049DA RID: 18906
		// (get) Token: 0x0600F512 RID: 62738 RVA: 0x0037B25D File Offset: 0x0037945D
		// (set) Token: 0x0600F513 RID: 62739 RVA: 0x0037B26A File Offset: 0x0037946A
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Add Group")]
		public string AddGroupToolTip
		{
			get
			{
				return this.Localization.AddGroupToolTip;
			}
			set
			{
				this.Localization.AddGroupToolTip = value;
			}
		}

		// Token: 0x170049DB RID: 18907
		// (get) Token: 0x0600F514 RID: 62740 RVA: 0x0037B278 File Offset: 0x00379478
		// (set) Token: 0x0600F515 RID: 62741 RVA: 0x0037B285 File Offset: 0x00379485
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Remove Item")]
		public string RemoveToolTip
		{
			get
			{
				return this.Localization.RemoveToolTip;
			}
			set
			{
				this.Localization.RemoveToolTip = value;
			}
		}

		// Token: 0x170049DC RID: 18908
		// (get) Token: 0x0600F516 RID: 62742 RVA: 0x0037B293 File Offset: 0x00379493
		// (set) Token: 0x0600F517 RID: 62743 RVA: 0x0037B2A0 File Offset: 0x003794A0
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("And")]
		public string BetweenDelimeterText
		{
			get
			{
				return this.Localization.BetweenDelimeterText;
			}
			set
			{
				this.Localization.BetweenDelimeterText = value;
			}
		}

		// Token: 0x170049DD RID: 18909
		// (get) Token: 0x0600F518 RID: 62744 RVA: 0x0037B2AE File Offset: 0x003794AE
		[Browsable(false)]
		public RadCalendar SharedCalendar
		{
			get
			{
				if (this.sharedCalendar == null)
				{
					this.sharedCalendar = new RadCalendar();
				}
				return this.sharedCalendar;
			}
		}

		// Token: 0x170049DE RID: 18910
		// (get) Token: 0x0600F519 RID: 62745 RVA: 0x0037B2CC File Offset: 0x003794CC
		// (set) Token: 0x0600F51A RID: 62746 RVA: 0x0037B305 File Offset: 0x00379505
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(DateTime), "1/1/1980")]
		public DateTime SharedCalendarMinDate
		{
			get
			{
				object obj = this.ViewState["SharedCalendarMinDate"] ?? new DateTime(1980, 1, 1);
				return (DateTime)obj;
			}
			set
			{
				this.ViewState["SharedCalendarMinDate"] = value;
			}
		}

		// Token: 0x170049DF RID: 18911
		// (get) Token: 0x0600F51B RID: 62747 RVA: 0x0037B320 File Offset: 0x00379520
		// (set) Token: 0x0600F51C RID: 62748 RVA: 0x0037B35B File Offset: 0x0037955B
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(DateTime), "12/30/2099")]
		public DateTime SharedCalendarMaxDate
		{
			get
			{
				object obj = this.ViewState["SharedCalendarMaxDate"] ?? new DateTime(2099, 12, 30);
				return (DateTime)obj;
			}
			set
			{
				this.ViewState["SharedCalendarMaxDate"] = value;
			}
		}

		// Token: 0x170049E0 RID: 18912
		// (get) Token: 0x0600F51D RID: 62749 RVA: 0x0037B373 File Offset: 0x00379573
		// (set) Token: 0x0600F51E RID: 62750 RVA: 0x0037B393 File Offset: 0x00379593
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string DefaultFieldEditorFieldName
		{
			get
			{
				return ((string)this.ViewState["DefaultFieldEditorFieldName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DefaultFieldEditorFieldName"] = value;
			}
		}

		// Token: 0x170049E1 RID: 18913
		// (get) Token: 0x0600F51F RID: 62751 RVA: 0x0037B3A8 File Offset: 0x003795A8
		// (set) Token: 0x0600F520 RID: 62752 RVA: 0x0037B3D6 File Offset: 0x003795D6
		[DefaultValue(RadFilterGroupOperation.And)]
		[NotifyParentProperty(true)]
		public RadFilterGroupOperation DefaultGroupOperation
		{
			get
			{
				object obj = this.ViewState["DefaultGroupOperation"] ?? RadFilterGroupOperation.And;
				return (RadFilterGroupOperation)obj;
			}
			set
			{
				this.ViewState["DefaultGroupOperation"] = value;
			}
		}

		// Token: 0x170049E2 RID: 18914
		// (get) Token: 0x0600F521 RID: 62753 RVA: 0x0037B3EE File Offset: 0x003795EE
		// (set) Token: 0x0600F522 RID: 62754 RVA: 0x0037B40F File Offset: 0x0037960F
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Shows / Hides the filter control line images.")]
		[NotifyParentProperty(true)]
		public bool ShowLineImages
		{
			get
			{
				return (bool)(this.ViewState["ShowLineImages"] ?? true);
			}
			set
			{
				this.ViewState["ShowLineImages"] = value;
			}
		}

		// Token: 0x170049E3 RID: 18915
		// (get) Token: 0x0600F523 RID: 62755 RVA: 0x0037B427 File Offset: 0x00379627
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RadFilterGroupExpression RootGroup
		{
			get
			{
				if (this._rootGroup == null)
				{
					this._rootGroup = new RadFilterGroupExpression();
					this._rootGroup.GroupOperation = this.DefaultGroupOperation;
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._rootGroup).TrackViewState();
					}
				}
				return this._rootGroup;
			}
		}

		// Token: 0x170049E4 RID: 18916
		// (get) Token: 0x0600F524 RID: 62756 RVA: 0x0037B466 File Offset: 0x00379666
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public RadFilterGroupExpressionItem RootGroupItem
		{
			get
			{
				if (this._rootGroupItem == null)
				{
					this.EnsureItemsCreated();
				}
				return this._rootGroupItem;
			}
		}

		// Token: 0x170049E5 RID: 18917
		// (get) Token: 0x0600F525 RID: 62757 RVA: 0x0037B47C File Offset: 0x0037967C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Default")]
		[Editor("Telerik.Web.Design.RadFilterDataFieldEditorForm, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[MergableProperty(false)]
		public RadFilterDataFieldEditorCollection FieldEditors
		{
			get
			{
				if (this._fieldEditors == null)
				{
					this._fieldEditors = new RadFilterDataFieldEditorCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._fieldEditors).TrackViewState();
					}
				}
				return this._fieldEditors;
			}
		}

		// Token: 0x170049E6 RID: 18918
		// (get) Token: 0x0600F526 RID: 62758 RVA: 0x0037B4AC File Offset: 0x003796AC
		// (set) Token: 0x0600F527 RID: 62759 RVA: 0x0037B4D9 File Offset: 0x003796D9
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[IDReferenceProperty(typeof(IRadFilterableContainer))]
		public virtual string FilterContainerID
		{
			get
			{
				object obj = this.ViewState["FilterContainerID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FilterContainerID"] = value;
			}
		}

		// Token: 0x170049E7 RID: 18919
		// (get) Token: 0x0600F528 RID: 62760 RVA: 0x0037B4EC File Offset: 0x003796EC
		// (set) Token: 0x0600F529 RID: 62761 RVA: 0x0037B4F4 File Offset: 0x003796F4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IRadFilterableContainer FilterContainer
		{
			get
			{
				return this._filterContainer;
			}
			internal set
			{
				this._filterContainer = value;
			}
		}

		// Token: 0x170049E8 RID: 18920
		// (get) Token: 0x0600F52A RID: 62762 RVA: 0x0037B500 File Offset: 0x00379700
		// (set) Token: 0x0600F52B RID: 62763 RVA: 0x0037B52D File Offset: 0x0037972D
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[IDReferenceProperty(typeof(IDataSource))]
		public virtual string DataSourceControlID
		{
			get
			{
				object obj = this.ViewState["DataSourceControlID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DataSourceControlID"] = value;
			}
		}

		// Token: 0x170049E9 RID: 18921
		// (get) Token: 0x0600F52C RID: 62764 RVA: 0x0037B540 File Offset: 0x00379740
		// (set) Token: 0x0600F52D RID: 62765 RVA: 0x0037B548 File Offset: 0x00379748
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IDataSource DataSourceControl { get; internal set; }

		// Token: 0x170049EA RID: 18922
		// (get) Token: 0x0600F52E RID: 62766 RVA: 0x0037B551 File Offset: 0x00379751
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadFilterableContainerLocator ContainerLocator
		{
			get
			{
				if (this._containerLocator == null)
				{
					this._containerLocator = new RadFilterableContainerLocator();
				}
				return this._containerLocator;
			}
		}

		// Token: 0x170049EB RID: 18923
		// (get) Token: 0x0600F52F RID: 62767 RVA: 0x0037B56C File Offset: 0x0037976C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public RadFilterClientSettings ClientSettings
		{
			get
			{
				if (this._clientSettings == null)
				{
					this._clientSettings = new RadFilterClientSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._clientSettings).TrackViewState();
					}
				}
				return this._clientSettings;
			}
		}

		// Token: 0x140001CC RID: 460
		// (add) Token: 0x0600F530 RID: 62768 RVA: 0x0037B59A File Offset: 0x0037979A
		// (remove) Token: 0x0600F531 RID: 62769 RVA: 0x0037B5AD File Offset: 0x003797AD
		[Category("Action")]
		[Description("Event raised when a new RadFilterExpressionItem is created. The event could be used to manipulate the controls inside each of the items.")]
		public event EventHandler<RadFilterExpressionItemCreatedEventArgs> ExpressionItemCreated
		{
			add
			{
				base.Events.AddHandler(RadFilter.EventExpressionItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadFilter.EventExpressionItemCreated, value);
			}
		}

		// Token: 0x0600F532 RID: 62770 RVA: 0x0037B5C0 File Offset: 0x003797C0
		protected virtual void OnExpressionItemCreated(RadFilterExpressionItemCreatedEventArgs e)
		{
			EventHandler<RadFilterExpressionItemCreatedEventArgs> eventHandler = base.Events[RadFilter.EventExpressionItemCreated] as EventHandler<RadFilterExpressionItemCreatedEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140001CD RID: 461
		// (add) Token: 0x0600F533 RID: 62771 RVA: 0x0037B5EE File Offset: 0x003797EE
		// (remove) Token: 0x0600F534 RID: 62772 RVA: 0x0037B601 File Offset: 0x00379801
		[Category("Action")]
		[Description("Raised when a button in a RadFilter control is clicked.")]
		public event EventHandler<RadFilterCommandEventArgs> ItemCommand
		{
			add
			{
				base.Events.AddHandler(RadFilter.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadFilter.EventItemCommand, value);
			}
		}

		// Token: 0x0600F535 RID: 62773 RVA: 0x0037B614 File Offset: 0x00379814
		protected virtual void OnItemCommand(RadFilterCommandEventArgs e)
		{
			EventHandler<RadFilterCommandEventArgs> eventHandler = base.Events[RadFilter.EventItemCommand] as EventHandler<RadFilterCommandEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140001CE RID: 462
		// (add) Token: 0x0600F536 RID: 62774 RVA: 0x0037B642 File Offset: 0x00379842
		// (remove) Token: 0x0600F537 RID: 62775 RVA: 0x0037B655 File Offset: 0x00379855
		[Category("Action")]
		[Description("Raised when a button Apply in a RadFilter control is clicked.")]
		public event EventHandler<RadFilterApplyExpressionsEventArgs> ApplyExpressions
		{
			add
			{
				base.Events.AddHandler(RadFilter.EventApplyExpressions, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadFilter.EventApplyExpressions, value);
			}
		}

		// Token: 0x0600F538 RID: 62776 RVA: 0x0037B668 File Offset: 0x00379868
		protected virtual void OnApplyExpressions(RadFilterApplyExpressionsEventArgs e)
		{
			EventHandler<RadFilterApplyExpressionsEventArgs> eventHandler = base.Events[RadFilter.EventApplyExpressions] as EventHandler<RadFilterApplyExpressionsEventArgs>;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140001CF RID: 463
		// (add) Token: 0x0600F539 RID: 62777 RVA: 0x0037B696 File Offset: 0x00379896
		// (remove) Token: 0x0600F53A RID: 62778 RVA: 0x0037B6A9 File Offset: 0x003798A9
		[Description("Raised when custom field editor is creating on postback")]
		[Category("Action")]
		public event FilterFieldEditorCreatingEventHandler<RadFilterFieldEditorCreatingEventArgs> FieldEditorCreating
		{
			add
			{
				base.Events.AddHandler(RadFilter.EventFieldEditorCreating, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadFilter.EventFieldEditorCreating, value);
			}
		}

		// Token: 0x0600F53B RID: 62779 RVA: 0x0037B6BC File Offset: 0x003798BC
		protected virtual void OnFieldEditorCreating(RadFilterFieldEditorCreatingEventArgs e)
		{
			FilterFieldEditorCreatingEventHandler<RadFilterFieldEditorCreatingEventArgs> filterFieldEditorCreatingEventHandler = base.Events[RadFilter.EventFieldEditorCreating] as FilterFieldEditorCreatingEventHandler<RadFilterFieldEditorCreatingEventArgs>;
			if (filterFieldEditorCreatingEventHandler != null)
			{
				filterFieldEditorCreatingEventHandler(this, e);
			}
		}

		// Token: 0x0600F53C RID: 62780 RVA: 0x0037B6EC File Offset: 0x003798EC
		protected virtual void OnFieldEditorCreated(RadFilterFieldEditorCreatedEventArgs e)
		{
			FilterFieldEditorCreatedEventHandler<RadFilterFieldEditorCreatedEventArgs> filterFieldEditorCreatedEventHandler = base.Events[RadFilter.EventFieldEditorCreated] as FilterFieldEditorCreatedEventHandler<RadFilterFieldEditorCreatedEventArgs>;
			if (filterFieldEditorCreatedEventHandler != null)
			{
				filterFieldEditorCreatedEventHandler(this, e);
			}
		}

		// Token: 0x140001D0 RID: 464
		// (add) Token: 0x0600F53D RID: 62781 RVA: 0x0037B71A File Offset: 0x0037991A
		// (remove) Token: 0x0600F53E RID: 62782 RVA: 0x0037B72D File Offset: 0x0037992D
		[Description("Raised when field editor is created when RadFilter is used integrated with IRadFilterableContainer")]
		[Category("Action")]
		public event FilterFieldEditorCreatedEventHandler<RadFilterFieldEditorCreatedEventArgs> FieldEditorCreated
		{
			add
			{
				base.Events.AddHandler(RadFilter.EventFieldEditorCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadFilter.EventFieldEditorCreated, value);
			}
		}

		// Token: 0x0600F53F RID: 62783 RVA: 0x0037B740 File Offset: 0x00379940
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Triggers ApplyExpressions command.")]
		public void FireApplyCommand()
		{
			RadFilterCommandEventArgs args = new RadFilterCommandEventArgs(null, this, new CommandEventArgs("ApplyExpressions", string.Empty));
			RadFilterCommandEventArgsFactory.HandleCommand(this, this, args);
		}

		// Token: 0x170049EC RID: 18924
		// (get) Token: 0x0600F540 RID: 62784 RVA: 0x0037B76C File Offset: 0x0037996C
		// (set) Token: 0x0600F541 RID: 62785 RVA: 0x0037B795 File Offset: 0x00379995
		[NotifyParentProperty(true)]
		[Description("Indicates whether the Apply button should be visible.")]
		[DefaultValue(true)]
		public bool ShowApplyButton
		{
			get
			{
				object obj = this.ViewState["ShowApplyButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowApplyButton"] = value;
			}
		}

		// Token: 0x170049ED RID: 18925
		// (get) Token: 0x0600F542 RID: 62786 RVA: 0x0037B7AD File Offset: 0x003799AD
		// (set) Token: 0x0600F543 RID: 62787 RVA: 0x0037B7CD File Offset: 0x003799CD
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Category("Appearance")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				if (value != this.ViewState["Culture"])
				{
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x170049EE RID: 18926
		// (get) Token: 0x0600F544 RID: 62788 RVA: 0x0037B7FA File Offset: 0x003799FA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FilterStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new FilterStrings(new LocalizationProvider("RadFilter.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x170049EF RID: 18927
		// (get) Token: 0x0600F545 RID: 62789 RVA: 0x0037B839 File Offset: 0x00379A39
		// (set) Token: 0x0600F546 RID: 62790 RVA: 0x0037B85C File Offset: 0x00379A5C
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadFilter will look for its .resx localization files.")]
		[DefaultValue("")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x170049F0 RID: 18928
		// (get) Token: 0x0600F547 RID: 62791 RVA: 0x0037B8AF File Offset: 0x00379AAF
		// (set) Token: 0x0600F548 RID: 62792 RVA: 0x0037B8D0 File Offset: 0x00379AD0
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		[DefaultValue(false)]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x170049F1 RID: 18929
		// (get) Token: 0x0600F549 RID: 62793 RVA: 0x0037B8E8 File Offset: 0x00379AE8
		// (set) Token: 0x0600F54A RID: 62794 RVA: 0x0037B909 File Offset: 0x00379B09
		[Description("When set to true changes the rendering of the Apply button in order to pass accessibility validation.")]
		[DefaultValue(false)]
		[Category("Appearance")]
		public bool UseAccessibleApplyButton
		{
			get
			{
				return (bool)(this.ViewState["UseAccessibleApplyButton"] ?? false);
			}
			set
			{
				this.ViewState["UseAccessibleApplyButton"] = value;
			}
		}

		// Token: 0x170049F2 RID: 18930
		// (get) Token: 0x0600F54B RID: 62795 RVA: 0x0037B924 File Offset: 0x00379B24
		// (set) Token: 0x0600F54C RID: 62796 RVA: 0x0037B94D File Offset: 0x00379B4D
		[NotifyParentProperty(true)]
		[DefaultValue(RadFilterExpressionPreviewPosition.None)]
		public RadFilterExpressionPreviewPosition ExpressionPreviewPosition
		{
			get
			{
				object obj = this.ViewState["ExpressionPreviewPosition"];
				if (obj == null)
				{
					return RadFilterExpressionPreviewPosition.None;
				}
				return (RadFilterExpressionPreviewPosition)obj;
			}
			set
			{
				this.ViewState["ExpressionPreviewPosition"] = value;
			}
		}

		// Token: 0x170049F3 RID: 18931
		// (get) Token: 0x0600F54D RID: 62797 RVA: 0x0037B965 File Offset: 0x00379B65
		// (set) Token: 0x0600F54E RID: 62798 RVA: 0x0037B981 File Offset: 0x00379B81
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual RadFilterQueryProvider ExpressionPreviewProvider
		{
			get
			{
				if (this._expressionPreviewProvider == null)
				{
					this._expressionPreviewProvider = new RadFilterExpressionPreviewProvider(this);
				}
				return this._expressionPreviewProvider;
			}
			set
			{
				this._expressionPreviewProvider = value;
			}
		}

		// Token: 0x170049F4 RID: 18932
		// (get) Token: 0x0600F54F RID: 62799 RVA: 0x0037B98C File Offset: 0x00379B8C
		// (set) Token: 0x0600F550 RID: 62800 RVA: 0x0037B9B5 File Offset: 0x00379BB5
		[NotifyParentProperty(true)]
		[DefaultValue(RadFilterSettingsFormatter.BinaryFormatter)]
		[Browsable(false)]
		public virtual RadFilterSettingsFormatter SettingsFormatter
		{
			get
			{
				object obj = this.ViewState["SettingsFormatter"];
				if (obj == null)
				{
					return RadFilterSettingsFormatter.BinaryFormatter;
				}
				return (RadFilterSettingsFormatter)obj;
			}
			set
			{
				this.ViewState["SettingsFormatter"] = value;
			}
		}

		// Token: 0x0600F551 RID: 62801 RVA: 0x0037B9D0 File Offset: 0x00379BD0
		protected virtual IFormatter GetSettingsFormatter()
		{
			if (this.SettingsFormatter == RadFilterSettingsFormatter.ObjectStateFormatter)
			{
				return new ObjectStateFormatter();
			}
			return new BinaryFormatter
			{
				AssemblyFormat = FormatterAssemblyStyle.Simple
			};
		}

		// Token: 0x0600F552 RID: 62802 RVA: 0x0037B9FC File Offset: 0x00379BFC
		public string SaveSettings()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.FetchExpressionsValues(this.RootGroupItem);
				object graph = ((IStateManager)this.RootGroup).SaveViewState();
				this.GetSettingsFormatter().Serialize(memoryStream, graph);
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x0600F553 RID: 62803 RVA: 0x0037BA60 File Offset: 0x00379C60
		public void LoadSettings(string state)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = Convert.FromBase64String(state);
				memoryStream.Write(array, 0, array.Length);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				object state2 = this.GetSettingsFormatter().Deserialize(memoryStream);
				this.RootGroup.Expressions.Clear();
				((IStateManager)this.RootGroup).LoadViewState(state2);
				this.RecreateControl();
			}
		}

		// Token: 0x0400460C RID: 17932
		public const string AddExpressionCommandName = "AddExpression";

		// Token: 0x0400460D RID: 17933
		public const string AddGroupCommandName = "AddGroup";

		// Token: 0x0400460E RID: 17934
		public const string RemoveExpressionCommandName = "RemoveExpression";

		// Token: 0x0400460F RID: 17935
		public const string RemoveGroupCommandName = "RemoveGroup";

		// Token: 0x04004610 RID: 17936
		public const string ChangeGroupOperatorCommandName = "ChangeGroupOperator";

		// Token: 0x04004611 RID: 17937
		public const string ChangeFilterFunctionCommandName = "ChangeFilterFunction";

		// Token: 0x04004612 RID: 17938
		public const string ChangeExpressionFieldNameCommandName = "ChangeExpressionFieldName";

		// Token: 0x04004613 RID: 17939
		public const string ApplyCommandName = "ApplyExpressions";

		// Token: 0x04004614 RID: 17940
		private static TFunc<string, string> parseFireCommandArgs = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[2];
		};

		// Token: 0x04004615 RID: 17941
		private static TFunc<string, string> parseFireCommandEventName = delegate(string input)
		{
			string input2 = input.Split(new char[]
			{
				':'
			})[1];
			return new Regex("(\\|;)").Split(input2)[0];
		};

		// Token: 0x04004616 RID: 17942
		private static readonly object EventExpressionItemCreated = new object();

		// Token: 0x04004617 RID: 17943
		private static readonly object EventItemCommand = new object();

		// Token: 0x04004618 RID: 17944
		private static readonly object EventApplyExpressions = new object();

		// Token: 0x04004619 RID: 17945
		private static readonly object EventFieldEditorCreating = new object();

		// Token: 0x0400461A RID: 17946
		private static readonly object EventFieldEditorCreated = new object();

		// Token: 0x0400461B RID: 17947
		private FilterStrings _localization;

		// Token: 0x0400461C RID: 17948
		private RadFilterGroupExpression _rootGroup;

		// Token: 0x0400461D RID: 17949
		private RadFilterDataFieldEditorCollection _fieldEditors;

		// Token: 0x0400461E RID: 17950
		private IRadFilterableContainer _filterContainer;

		// Token: 0x0400461F RID: 17951
		private RadFilterableContainerLocator _containerLocator;

		// Token: 0x04004620 RID: 17952
		private RadFilterGroupExpressionItem _rootGroupItem;

		// Token: 0x04004621 RID: 17953
		private bool _isAfterPrerender;

		// Token: 0x04004622 RID: 17954
		private RadFilterClientSettings _clientSettings;

		// Token: 0x04004623 RID: 17955
		private IList<RadFilterFieldDescriptor> FieldDescriptors;

		// Token: 0x04004624 RID: 17956
		private IList<RadFilterFunction> SupportedFilterFunctions;

		// Token: 0x04004625 RID: 17957
		private IList<RadFilterGroupOperation> SupportedGroupTypes;

		// Token: 0x04004626 RID: 17958
		private RadFilterQueryProvider _expressionPreviewProvider;

		// Token: 0x04004627 RID: 17959
		private LiteralControl _expressionPreviewHolder;

		// Token: 0x04004628 RID: 17960
		internal Dictionary<string, object> _expressionsList = new Dictionary<string, object>();

		// Token: 0x04004629 RID: 17961
		private TFunc<string, bool> _shouldExtractValues = (string index) => true;

		// Token: 0x0400462A RID: 17962
		private bool _isApplyCommandTriggered;

		// Token: 0x0400462B RID: 17963
		private RadFilterQueryProvider _queryProvider;

		// Token: 0x0400462C RID: 17964
		private Button applyButton;

		// Token: 0x0400462D RID: 17965
		private RadFilterContextMenu contextMenu;

		// Token: 0x0400462E RID: 17966
		private RadCalendar sharedCalendar;

		// Token: 0x020018BD RID: 6333
		internal class QueryProviderFactory
		{
			// Token: 0x0600F55D RID: 62813 RVA: 0x0037BADC File Offset: 0x00379CDC
			[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.Compare(System.String,System.String)")]
			public static RadFilterQueryProvider GetProvider(IDataSource dataSource)
			{
				RadFilterQueryProvider result = null;
				if (dataSource is SqlDataSource)
				{
					result = new RadFilterSqlQueryProvider();
				}
				else if (dataSource is LinqDataSource || string.Compare(dataSource.GetType().FullName, "Telerik.OpenAccess.Web.OpenAccessLinqDataSource") == 0)
				{
					result = new RadFilterDynamicLinqQueryProvider();
				}
				else if (string.Compare(dataSource.GetType().FullName, "System.Web.UI.WebControls.EntityDataSource") == 0)
				{
					result = new RadFilterEntitySqlQueryProvider();
				}
				else if (string.Compare(dataSource.GetType().FullName, "Telerik.OpenAccess.OpenAccessDataSource") == 0)
				{
					result = new RadFilterOqlQueryProvider();
				}
				return result;
			}
		}

		// Token: 0x020018BE RID: 6334
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public class RadFilterDataSourceHelper
		{
			// Token: 0x0600F55F RID: 62815 RVA: 0x0037BB68 File Offset: 0x00379D68
			public static bool IsBindableType(Type type)
			{
				return !type.IsEnum && (type.IsPrimitive || !(type != typeof(string)) || !(type != typeof(DateTime)) || !(type != typeof(TimeSpan)) || !(type != typeof(decimal)) || !(type != typeof(Guid)) || (type.IsValueType && type.IsGenericType && type.GetGenericArguments().Length == 1 && RadFilter.RadFilterDataSourceHelper.IsBindableType(type.GetGenericArguments()[0])));
			}

			// Token: 0x0400463A RID: 17978
			private static BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;

			// Token: 0x0400463B RID: 17979
			[SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
			public static Func<Type, PropertyInfo> ExtractResultProperty = delegate(Type type)
			{
				PropertyInfo property = type.GetProperty("Result", RadFilter.RadFilterDataSourceHelper.flags);
				if (property == null)
				{
					property = type.GetProperty("Results", RadFilter.RadFilterDataSourceHelper.flags);
				}
				return property;
			};

			// Token: 0x0400463C RID: 17980
			[SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
			public static Func<Type, PropertyInfo> ExtractWhereProperty = (Type type) => type.GetProperty("Where", RadFilter.RadFilterDataSourceHelper.flags);

			// Token: 0x0400463D RID: 17981
			[SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
			public static Action<string, MethodInfo, RadFilter, IDataSource> AttachEventHandler = delegate(string eventName, MethodInfo eventHandler, RadFilter filterInstance, IDataSource dataSourceInstance)
			{
				EventInfo @event = dataSourceInstance.GetType().GetEvent(eventName, RadFilter.RadFilterDataSourceHelper.flags);
				if (@event == null)
				{
					return;
				}
				Delegate handler = Delegate.CreateDelegate(@event.EventHandlerType, filterInstance, eventHandler);
				@event.AddEventHandler(dataSourceInstance, handler);
			};
		}
	}
}
