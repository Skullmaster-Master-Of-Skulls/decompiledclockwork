using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;

namespace Telerik.Web.UI
{
	// Token: 0x02000977 RID: 2423
	[TelerikToolboxCategory("Data")]
	[Designer("Telerik.Web.Design.RadTreeMapDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	[EmbeddedSkin("TreeMap", typeof(RadTreeMap))]
	[EmbeddedSkin("TreeMap", "Default", typeof(RadTreeMap))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ToolboxData("<{0}:RadTreeMap runat=\"server\"></{0}:RadTreeMap>")]
	[RequiredScript(typeof(Html5DataVizTreeMap))]
	[ClientScriptResource("Telerik.Web.UI.RadTreeMap", "Telerik.Web.UI.TreeMap.RadTreeMapScripts.js")]
	[ToolboxBitmap(typeof(RadTreeMap), "Telerik.Web.UI.TreeMap.png")]
	public class RadTreeMap : RadDataBoundControl, IHierarchicalItemContainer, IItemContainer
	{
		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x00118948 File Offset: 0x00116B48
		// (set) Token: 0x06005C06 RID: 23558 RVA: 0x00118950 File Offset: 0x00116B50
		internal string CurrentSiteMapUrl { get; set; }

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x06005C07 RID: 23559 RVA: 0x00118959 File Offset: 0x00116B59
		// (set) Token: 0x06005C08 RID: 23560 RVA: 0x00118961 File Offset: 0x00116B61
		int IHierarchicalItemContainer.MaxDataBindDepth
		{
			get
			{
				return this._maxBindDepth;
			}
			set
			{
				this._maxBindDepth = value;
			}
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x0011896A File Offset: 0x00116B6A
		IItem IItemContainer.CreateItem()
		{
			return new TreeMapItem(this);
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x00118974 File Offset: 0x00116B74
		void IItemContainer.RaiseItemDataBound(IItem item)
		{
			TreeMapItemDataBoundEventHnadler treeMapItemDataBoundEventHnadler = (TreeMapItemDataBoundEventHnadler)base.Events[RadTreeMap.TreeMapItemDataBoundEvent];
			TreeMapItem treeMapItem = item as TreeMapItem;
			treeMapItem.TemplateData = new Dictionary<string, string>();
			foreach (string text in this.DataKeyNames)
			{
				try
				{
					string value = DataBinder.Eval(treeMapItem.DataItem, text) as string;
					treeMapItem.TemplateData.Add(text, value);
				}
				catch (Exception)
				{
					throw new Exception("The data item does not contain the " + text + "data field");
				}
			}
			TreeMapItemDataBoundEventArguments e = new TreeMapItemDataBoundEventArguments(treeMapItem);
			if (treeMapItemDataBoundEventHnadler != null)
			{
				treeMapItemDataBoundEventHnadler(this, e);
			}
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x00118A28 File Offset: 0x00116C28
		IList IItemContainer.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x00118A30 File Offset: 0x00116C30
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				return;
			}
			this.Items.Clear();
			ControlDataBinder controlDataBinder = new ControlDataBinder(this);
			IHierarchicalEnumerable hierarchyData = this.GetHierarchyData(data);
			if (hierarchyData != null)
			{
				controlDataBinder.BindToHierarchicalData(hierarchyData);
				return;
			}
			DataView dataView = data as DataView;
			if (dataView != null && !base.DesignMode && !string.IsNullOrEmpty(this.DataFieldID) && !string.IsNullOrEmpty(this.DataFieldParentID))
			{
				controlDataBinder.BindToDataTable(dataView.ToTable(), this.DataFieldID, this.DataFieldParentID);
				return;
			}
			controlDataBinder.BindToEnumerableData(data, this.DataFieldID, this.DataFieldParentID);
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x00118AC8 File Offset: 0x00116CC8
		private Control FindDataSourceControl()
		{
			Control control = this;
			Control control2 = null;
			while (control2 == null && control != this.Page)
			{
				control = control.NamingContainer;
				if (control == null)
				{
					break;
				}
				control2 = control.FindControl(this.DataSourceID);
			}
			return control2;
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x00118B00 File Offset: 0x00116D00
		protected override IDataSource GetDataSource()
		{
			if (!base.IsBoundUsingDataSourceID)
			{
				return base.GetDataSource();
			}
			Control control = this.FindDataSourceControl();
			IHierarchicalDataSource hierarchicalDataSource = control as IHierarchicalDataSource;
			if (hierarchicalDataSource != null)
			{
				SiteMapDataSource siteMapDataSource = control as SiteMapDataSource;
				if (siteMapDataSource != null)
				{
					IHierarchyData currentNode = siteMapDataSource.Provider.CurrentNode;
					if (currentNode != null)
					{
						this.CurrentSiteMapUrl = currentNode.Path;
					}
				}
				return new DecoratingDataSource(hierarchicalDataSource);
			}
			return base.GetDataSource();
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x00118B60 File Offset: 0x00116D60
		private IHierarchicalEnumerable GetHierarchyData(IEnumerable data)
		{
			IHierarchicalEnumerable result = null;
			IHierarchicalEnumerable hierarchicalEnumerable = data as IHierarchicalEnumerable;
			if (this.GetDataSource() is IHierarchicalDataSource)
			{
				IHierarchicalDataSource hierarchicalDataSource = (IHierarchicalDataSource)this.GetDataSource();
				result = hierarchicalDataSource.GetHierarchicalView("").Select();
			}
			else if (this.DataSource is IHierarchicalDataSource)
			{
				IHierarchicalDataSource hierarchicalDataSource2 = (IHierarchicalDataSource)this.DataSource;
				result = hierarchicalDataSource2.GetHierarchicalView("").Select();
			}
			else if (hierarchicalEnumerable != null)
			{
				result = hierarchicalEnumerable;
			}
			return result;
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x00118BD4 File Offset: 0x00116DD4
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[1]);
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x00118C10 File Offset: 0x00116E10
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x00118C4A File Offset: 0x00116E4A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x00118C60 File Offset: 0x00116E60
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			if (this.Colors.Count > 0)
			{
				descriptor.AddScriptProperty("colors", this.SerializeArrayData(true));
			}
			if (this.DataKeyNames.Length > 0)
			{
				descriptor.AddScriptProperty("dataKeyNames", this.SerializeArrayData(false));
			}
			descriptor.AddScriptProperty("itemsData", this.SerializeItemsData());
			base.DescribeProperty<string>(descriptor, "_clientItemTemplate", this.ClientItemTemplate, string.Empty);
			base.DescribeProperty<string>(descriptor, "_algorithmType", this.AlgorithmType.ToString().ToLowerInvariant(), TreeMapAlgorithmType.Squarified.ToString().ToLowerInvariant());
			base.DescribeProperty<string>(descriptor, "_skin", base.RuntimeSkin.ToLowerInvariant(), string.Empty);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "itemCreated", this.OnClientItemCreated);
			this.DescribeClientDataSource(descriptor);
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x00118D68 File Offset: 0x00116F68
		private void DescribeClientDataSource(IScriptDescriptor descriptor)
		{
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				if (!string.IsNullOrEmpty(this.DataTextField))
				{
					base.DescribeProperty<string>(descriptor, "_dataTextField", this.DataTextField, string.Empty);
				}
				if (!string.IsNullOrEmpty(this.DataValueField))
				{
					base.DescribeProperty<string>(descriptor, "_dataValueField", this.DataValueField, string.Empty);
				}
				if (!string.IsNullOrEmpty(this.DataColorField))
				{
					base.DescribeProperty<string>(descriptor, "_dataColorField", this.DataColorField, string.Empty);
				}
				if (!string.IsNullOrEmpty(this.DataFieldID))
				{
					base.DescribeProperty<string>(descriptor, "_dataFieldID", this.DataFieldID, string.Empty);
				}
				if (!string.IsNullOrEmpty(this.DataFieldParentID))
				{
					base.DescribeProperty<string>(descriptor, "_dataFieldParentID", this.DataFieldParentID, string.Empty);
				}
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					base.DescribeProperty<string>(descriptor, "_clientDataSourceID", control.ClientID, string.Empty);
				}
				catch (Exception)
				{
					base.DescribeProperty<string>(descriptor, "_clientDataSourceID", this.ClientDataSourceID, string.Empty);
				}
			}
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x00118E8C File Offset: 0x0011708C
		internal string SerializeItemsData()
		{
			JavaScriptConverter[] converters = new JavaScriptConverter[]
			{
				new TreeMapItemJavaScriptConverter()
			};
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(converters);
			javaScriptSerializer.MaxJsonLength = int.MaxValue;
			return javaScriptSerializer.Serialize(this.Items);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x00118ED0 File Offset: 0x001170D0
		internal string SerializeArrayData(bool isColor)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			if (isColor)
			{
				num = this.Colors.Count;
			}
			else
			{
				num = this.DataKeyNames.Length;
			}
			stringBuilder.Append("[");
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append("'");
				if (isColor)
				{
					stringBuilder.Append(ColorTranslator.ToHtml(this.Colors[i]));
				}
				else
				{
					stringBuilder.Append(this.DataKeyNames[i].Trim());
				}
				stringBuilder.Append("'");
				if (i != num - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x00118F84 File Offset: 0x00117184
		internal string SerializeColorsData()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = this.Colors.Count;
			stringBuilder.Append("[");
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append("'");
				stringBuilder.Append(ColorTranslator.ToHtml(this.Colors[i]));
				stringBuilder.Append("'");
				if (i != count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x06005C18 RID: 23576 RVA: 0x0011900F File Offset: 0x0011720F
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x06005C19 RID: 23577 RVA: 0x00119012 File Offset: 0x00117212
		protected override string CssClassFormatString
		{
			get
			{
				return "RadTreeMap RadTreeMap_{0}";
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x06005C1A RID: 23578 RVA: 0x00119019 File Offset: 0x00117219
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x06005C1B RID: 23579 RVA: 0x0011901D File Offset: 0x0011721D
		// (set) Token: 0x06005C1C RID: 23580 RVA: 0x0011903D File Offset: 0x0011723D
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataFieldID
		{
			get
			{
				return (string)(this.ViewState["DataFieldID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataFieldID"] = value;
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x06005C1D RID: 23581 RVA: 0x00119050 File Offset: 0x00117250
		// (set) Token: 0x06005C1E RID: 23582 RVA: 0x00119070 File Offset: 0x00117270
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataFieldParentID
		{
			get
			{
				return (string)(this.ViewState["DataFieldParentID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataFieldParentID"] = value;
			}
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x06005C1F RID: 23583 RVA: 0x00119083 File Offset: 0x00117283
		// (set) Token: 0x06005C20 RID: 23584 RVA: 0x001190A3 File Offset: 0x001172A3
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataTextField
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

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x06005C21 RID: 23585 RVA: 0x001190B6 File Offset: 0x001172B6
		// (set) Token: 0x06005C22 RID: 23586 RVA: 0x001190D6 File Offset: 0x001172D6
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataValueField
		{
			get
			{
				return (string)(this.ViewState["DataValueField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x06005C23 RID: 23587 RVA: 0x001190E9 File Offset: 0x001172E9
		// (set) Token: 0x06005C24 RID: 23588 RVA: 0x00119109 File Offset: 0x00117309
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataColorField
		{
			get
			{
				return (string)(this.ViewState["DataColorField"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataColorField"] = value;
			}
		}

		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x06005C25 RID: 23589 RVA: 0x0011911C File Offset: 0x0011731C
		// (set) Token: 0x06005C26 RID: 23590 RVA: 0x0011913C File Offset: 0x0011733C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual string ClientItemTemplate
		{
			get
			{
				return (this.ViewState["ClientItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ClientItemTemplate"] = value;
			}
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x06005C27 RID: 23591 RVA: 0x0011914F File Offset: 0x0011734F
		// (set) Token: 0x06005C28 RID: 23592 RVA: 0x00119170 File Offset: 0x00117370
		[Category("Data")]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Description("Comma delimited list of data-field Names")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[TypeConverter(typeof(ListConverter))]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string[] DataKeyNames
		{
			get
			{
				return (string[])(this.ViewState["DataKeyNames"] ?? new string[0]);
			}
			set
			{
				this.ViewState["DataKeyNames"] = value;
			}
		}

		// Token: 0x17001E5F RID: 7775
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x00119183 File Offset: 0x00117383
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeMapItemCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new TreeMapItemCollection(this);
				}
				return this._items;
			}
		}

		// Token: 0x17001E60 RID: 7776
		// (get) Token: 0x06005C2A RID: 23594 RVA: 0x001191A0 File Offset: 0x001173A0
		// (set) Token: 0x06005C2B RID: 23595 RVA: 0x001191D5 File Offset: 0x001173D5
		public virtual TreeMapAlgorithmType AlgorithmType
		{
			get
			{
				TreeMapAlgorithmType? treeMapAlgorithmType = (TreeMapAlgorithmType?)this.ViewState["AlgorithmType"];
				if (treeMapAlgorithmType == null)
				{
					return TreeMapAlgorithmType.Squarified;
				}
				return treeMapAlgorithmType.GetValueOrDefault();
			}
			set
			{
				this.ViewState["AlgorithmType"] = value;
			}
		}

		// Token: 0x17001E61 RID: 7777
		// (get) Token: 0x06005C2C RID: 23596 RVA: 0x001191ED File Offset: 0x001173ED
		[Browsable(false)]
		public List<Color> Colors
		{
			get
			{
				if (this._colors == null)
				{
					this._colors = new List<Color>();
				}
				return this._colors;
			}
		}

		// Token: 0x17001E62 RID: 7778
		// (get) Token: 0x06005C2D RID: 23597 RVA: 0x00119208 File Offset: 0x00117408
		public override RenderMode ResolvedRenderMode
		{
			get
			{
				return RenderMode.Lightweight;
			}
		}

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x06005C2E RID: 23598 RVA: 0x0011920B File Offset: 0x0011740B
		// (remove) Token: 0x06005C2F RID: 23599 RVA: 0x0011921E File Offset: 0x0011741E
		public event TreeMapItemDataBoundEventHnadler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadTreeMap.TreeMapItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadTreeMap.TreeMapItemDataBoundEvent, value);
			}
		}

		// Token: 0x17001E63 RID: 7779
		// (get) Token: 0x06005C30 RID: 23600 RVA: 0x00119231 File Offset: 0x00117431
		// (set) Token: 0x06005C31 RID: 23601 RVA: 0x00119251 File Offset: 0x00117451
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed after the control is loaded")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Browsable(true)]
		[Bindable(true)]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17001E64 RID: 7780
		// (get) Token: 0x06005C32 RID: 23602 RVA: 0x00119264 File Offset: 0x00117464
		// (set) Token: 0x06005C33 RID: 23603 RVA: 0x00119284 File Offset: 0x00117484
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed after an item is created")]
		public string OnClientItemCreated
		{
			get
			{
				return ((string)this.ViewState["OnClientItemCreated"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientItemCreated"] = value;
			}
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x00119297 File Offset: 0x00117497
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x0400161E RID: 5662
		private TreeMapItemCollection _items;

		// Token: 0x0400161F RID: 5663
		private List<Color> _colors;

		// Token: 0x04001620 RID: 5664
		private int _maxBindDepth = -1;

		// Token: 0x04001621 RID: 5665
		private static readonly object TreeMapItemDataBoundEvent = new object();
	}
}
