using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.Web.UI.OrgChart.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000BE9 RID: 3049
	[XmlRoot("Item")]
	public class OrgChartGroupItem : IItem, IOrgChartRendererContainer, IXmlSerializable, IDisposable
	{
		// Token: 0x06007416 RID: 29718 RVA: 0x001B1651 File Offset: 0x001AF851
		public OrgChartGroupItem()
		{
		}

		// Token: 0x06007417 RID: 29719 RVA: 0x001B1664 File Offset: 0x001AF864
		internal OrgChartGroupItem(RadOrgChart orgChart)
		{
			this.OrgChart = orgChart;
		}

		// Token: 0x170025C5 RID: 9669
		// (get) Token: 0x06007418 RID: 29720 RVA: 0x001B167E File Offset: 0x001AF87E
		// (set) Token: 0x06007419 RID: 29721 RVA: 0x001B1686 File Offset: 0x001AF886
		[XmlIgnore]
		[Browsable(false)]
		public RadOrgChart OrgChart { get; set; }

		// Token: 0x170025C6 RID: 9670
		// (get) Token: 0x0600741A RID: 29722 RVA: 0x001B168F File Offset: 0x001AF88F
		// (set) Token: 0x0600741B RID: 29723 RVA: 0x001B1697 File Offset: 0x001AF897
		public string ImageUrl { get; set; }

		// Token: 0x170025C7 RID: 9671
		// (get) Token: 0x0600741C RID: 29724 RVA: 0x001B16A0 File Offset: 0x001AF8A0
		// (set) Token: 0x0600741D RID: 29725 RVA: 0x001B16A8 File Offset: 0x001AF8A8
		public string ImageAltText { get; set; }

		// Token: 0x170025C8 RID: 9672
		// (get) Token: 0x0600741E RID: 29726 RVA: 0x001B16B1 File Offset: 0x001AF8B1
		// (set) Token: 0x0600741F RID: 29727 RVA: 0x001B16B9 File Offset: 0x001AF8B9
		public string Text { get; set; }

		// Token: 0x170025C9 RID: 9673
		// (get) Token: 0x06007420 RID: 29728 RVA: 0x001B16C2 File Offset: 0x001AF8C2
		public OrgChartGroupItemRendererBase Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = RendererFactory.CreateOrgChartGroupItemRenderer(this.OrgChart);
				}
				return this._renderer;
			}
		}

		// Token: 0x170025CA RID: 9674
		// (get) Token: 0x06007421 RID: 29729 RVA: 0x001B16E3 File Offset: 0x001AF8E3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartRenderedFieldCollection RenderedFields
		{
			get
			{
				if (this._renderedFields == null)
				{
					this._renderedFields = new OrgChartRenderedFieldCollection();
				}
				return this._renderedFields;
			}
		}

		// Token: 0x170025CB RID: 9675
		// (get) Token: 0x06007422 RID: 29730 RVA: 0x001B16FE File Offset: 0x001AF8FE
		// (set) Token: 0x06007423 RID: 29731 RVA: 0x001B1706 File Offset: 0x001AF906
		[TemplateContainer(typeof(OrgChartGroupItemRendererBase))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
			}
		}

		// Token: 0x170025CC RID: 9676
		// (get) Token: 0x06007424 RID: 29732 RVA: 0x001B170F File Offset: 0x001AF90F
		// (set) Token: 0x06007425 RID: 29733 RVA: 0x001B1717 File Offset: 0x001AF917
		[Browsable(false)]
		public OrgChartNode Node { get; internal set; }

		// Token: 0x06007426 RID: 29734 RVA: 0x001B1720 File Offset: 0x001AF920
		public Control FindControl(string id)
		{
			return this.Renderer.FindControl(id);
		}

		// Token: 0x170025CD RID: 9677
		// (get) Token: 0x06007427 RID: 29735 RVA: 0x001B172E File Offset: 0x001AF92E
		public string ID
		{
			get
			{
				return this._groupItemID;
			}
		}

		// Token: 0x170025CE RID: 9678
		// (get) Token: 0x06007428 RID: 29736 RVA: 0x001B1736 File Offset: 0x001AF936
		// (set) Token: 0x06007429 RID: 29737 RVA: 0x001B173E File Offset: 0x001AF93E
		public string CssClass { get; set; }

		// Token: 0x170025CF RID: 9679
		// (get) Token: 0x0600742A RID: 29738 RVA: 0x001B1747 File Offset: 0x001AF947
		// (set) Token: 0x0600742B RID: 29739 RVA: 0x001B174F File Offset: 0x001AF94F
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				if (value != null)
				{
					this._dataItem = value;
				}
			}
		}

		// Token: 0x0600742C RID: 29740 RVA: 0x001B175B File Offset: 0x001AF95B
		public void DataBind()
		{
		}

		// Token: 0x170025D0 RID: 9680
		// (get) Token: 0x0600742D RID: 29741 RVA: 0x001B175D File Offset: 0x001AF95D
		[Browsable(false)]
		IList IItem.Children
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600742E RID: 29742 RVA: 0x001B1760 File Offset: 0x001AF960
		public void SyncRenderedProperties()
		{
			if (this.ShouldRenderImage)
			{
				this.Renderer.ImageUrl = this.ImageUrl;
				this.Renderer.ImageAltText = this.ImageAltText;
				this.Renderer.ShouldRenderImage = true;
				this.Renderer.DefaultImageUrl = this.DefaultImageUrl;
			}
			else
			{
				this.Renderer.ShouldRenderImage = false;
			}
			this.Renderer.Text = this.Text;
			this.Renderer.IsSimpleBinding = this.IsSimpleBinding;
			this.Renderer.EnableCollapsing = this.EnableCollapsing;
			this.Renderer.Collapsed = (this.Node != null && this.Node.Collapsed);
			this.Renderer.HasNodes = (this.Node != null && this.Node.HasNodes);
			this.Renderer.HasNodesForLoad = (this.Node != null && this.Node.HasNodesForLoad);
			this.Renderer.IsInGroup = this.IsInGroup;
			this.Renderer.IsFirst = this.IsFirst;
			this.Renderer.IsLast = this.IsLast;
			this.Renderer.CssClass = this.CssClass;
			foreach (OrgChartRenderedField item in this.RenderedFields)
			{
				if (!this.Renderer.RenderedFields.Contains(item))
				{
					this.Renderer.RenderedFields.Add(item);
				}
			}
			this.Renderer.IsFirstInRow = this.IsFirstInRow;
			this.Renderer.IsLastInRow = this.IsLastInRow;
			if (!this.IsTemplateInstantiated)
			{
				this.ApplyTemplate();
				this.Renderer.IsTemplated = this.IsTemplateInstantiated;
			}
		}

		// Token: 0x0600742F RID: 29743 RVA: 0x001B1940 File Offset: 0x001AFB40
		private void ClearTemplate()
		{
			this.IsTemplateInstantiated = false;
			this.Renderer.Controls.Clear();
		}

		// Token: 0x06007430 RID: 29744 RVA: 0x001B195C File Offset: 0x001AFB5C
		internal void ApplyTemplate()
		{
			this.ClearTemplate();
			ITemplate templateToApply = this.TemplateToApply;
			if (templateToApply != null)
			{
				this.Renderer.DataItem = this.DataItem;
				templateToApply.InstantiateIn(this.Renderer);
				this.IsTemplateInstantiated = true;
				this.Renderer.IsTemplated = this.IsTemplateInstantiated;
				this.Renderer.DataBind();
			}
		}

		// Token: 0x170025D1 RID: 9681
		// (get) Token: 0x06007431 RID: 29745 RVA: 0x001B19B9 File Offset: 0x001AFBB9
		private ITemplate TemplateToApply
		{
			get
			{
				ITemplate result;
				if ((result = this.Template) == null)
				{
					result = (this.Node.ItemTemplate ?? this.OrgChart.ItemTemplate);
				}
				return result;
			}
		}

		// Token: 0x170025D2 RID: 9682
		// (get) Token: 0x06007432 RID: 29746 RVA: 0x001B19DF File Offset: 0x001AFBDF
		// (set) Token: 0x06007433 RID: 29747 RVA: 0x001B19E7 File Offset: 0x001AFBE7
		private bool IsTemplateInstantiated { get; set; }

		// Token: 0x170025D3 RID: 9683
		// (get) Token: 0x06007434 RID: 29748 RVA: 0x001B19F0 File Offset: 0x001AFBF0
		private bool IsInGroup
		{
			get
			{
				bool result = false;
				if (this.Node != null)
				{
					result = this.Node.GroupItems.IsGroup;
				}
				return result;
			}
		}

		// Token: 0x170025D4 RID: 9684
		// (get) Token: 0x06007435 RID: 29749 RVA: 0x001B1A1C File Offset: 0x001AFC1C
		private bool IsSimpleBinding
		{
			get
			{
				bool result = false;
				if (this.Node != null && this.Node.GroupItems != null)
				{
					result = this.OrgChart.IsSimpleBinding;
				}
				return result;
			}
		}

		// Token: 0x170025D5 RID: 9685
		// (get) Token: 0x06007436 RID: 29750 RVA: 0x001B1A4D File Offset: 0x001AFC4D
		private bool EnableCollapsing
		{
			get
			{
				return this.OrgChart != null && this.OrgChart.EnableCollapsing;
			}
		}

		// Token: 0x170025D6 RID: 9686
		// (get) Token: 0x06007437 RID: 29751 RVA: 0x001B1A64 File Offset: 0x001AFC64
		private bool IsFirst
		{
			get
			{
				bool result = false;
				if (this.Node != null)
				{
					result = (this.Node.GroupItems.IndexOf(this) == 0);
				}
				return result;
			}
		}

		// Token: 0x170025D7 RID: 9687
		// (get) Token: 0x06007438 RID: 29752 RVA: 0x001B1A94 File Offset: 0x001AFC94
		private bool IsFirstInRow
		{
			get
			{
				bool result = false;
				int num = -1;
				if (this.Node != null)
				{
					num = this.Node.AppliedColumnCount;
				}
				if (num > 0)
				{
					if (num == 1)
					{
						result = true;
					}
					else
					{
						int num2 = this.Node.GroupItems.IndexOf(this) + 1;
						int num3 = num2 % num;
						result = (num3 == 1);
					}
				}
				return result;
			}
		}

		// Token: 0x170025D8 RID: 9688
		// (get) Token: 0x06007439 RID: 29753 RVA: 0x001B1AE4 File Offset: 0x001AFCE4
		private bool IsLastInRow
		{
			get
			{
				bool result = false;
				int num = -1;
				if (this.Node != null)
				{
					num = this.Node.AppliedColumnCount;
				}
				if (num > 0)
				{
					if (num == 1)
					{
						result = true;
					}
					else
					{
						int num2 = this.Node.GroupItems.IndexOf(this) + 1;
						int num3 = num2 % num;
						result = (num3 == 0);
					}
				}
				return result;
			}
		}

		// Token: 0x170025D9 RID: 9689
		// (get) Token: 0x0600743A RID: 29754 RVA: 0x001B1B34 File Offset: 0x001AFD34
		private bool IsLast
		{
			get
			{
				bool result = false;
				if (this.Node != null)
				{
					result = (this.Node.GroupItems.IndexOf(this) == this.Node.GroupItems.Count - 1);
				}
				return result;
			}
		}

		// Token: 0x170025DA RID: 9690
		// (get) Token: 0x0600743B RID: 29755 RVA: 0x001B1B74 File Offset: 0x001AFD74
		private bool ShouldRenderImage
		{
			get
			{
				bool result = true;
				if (this.OrgChart != null)
				{
					result = ((this.OrgChart.DisableDefaultImage && !string.IsNullOrEmpty(this.ImageUrl)) || !this.OrgChart.DisableDefaultImage);
				}
				return result;
			}
		}

		// Token: 0x170025DB RID: 9691
		// (get) Token: 0x0600743C RID: 29756 RVA: 0x001B1BB8 File Offset: 0x001AFDB8
		private string DefaultImageUrl
		{
			get
			{
				return this.OrgChart.DefaultImageUrl;
			}
		}

		// Token: 0x0600743D RID: 29757 RVA: 0x001B1BC8 File Offset: 0x001AFDC8
		void IItem.PopulateFromDataItem(PropertyDescriptorCache properties, object dataItem, string dataMember, int depth)
		{
			if (this.OrgChart.IsGroupEnabledBinding)
			{
				object propertyValue = properties.GetPropertyValue(dataItem, this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataFieldNodeID);
				if (!this.OrgChart.ItemsHash.ContainsKey(propertyValue))
				{
					this.OrgChart.ItemsHash.Add(propertyValue, new List<OrgChartGroupItem>());
				}
				List<OrgChartGroupItem> list = this.OrgChart.ItemsHash[propertyValue];
				list.Add(this);
				this._groupItemID = properties.GetPropertyValue(dataItem, this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataFieldID).ToString();
				if (this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataImageUrlField != null)
				{
					this.ImageUrl = (properties.GetPropertyValue(dataItem, this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataImageUrlField) as string);
				}
				if (this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataImageAltTextField != null)
				{
					this.ImageAltText = (properties.GetPropertyValue(dataItem, this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataImageAltTextField) as string);
				}
				if (this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataTextField != null)
				{
					this.Text = (properties.GetPropertyValue(dataItem, this.OrgChart.GroupEnabledBinding.GroupItemBindingSettings.DataTextField) as string);
				}
			}
			else
			{
				if (this.OrgChart.DataImageUrlField != null)
				{
					this.ImageUrl = (properties.GetPropertyValue(dataItem, this.OrgChart.DataImageUrlField) as string);
				}
				if (this.OrgChart.DataImageAltTextField != null)
				{
					this.ImageAltText = (properties.GetPropertyValue(dataItem, this.OrgChart.DataImageAltTextField) as string);
				}
				if (this.OrgChart.DataTextField != null)
				{
					this.Text = (properties.GetPropertyValue(dataItem, this.OrgChart.DataTextField) as string);
				}
			}
			INavigateUIData navigateUIData = dataItem as INavigateUIData;
			if (navigateUIData != null)
			{
				this.Text = navigateUIData.Name;
			}
			OrgChartRenderedFieldCollection itemFields = this.OrgChart.RenderedFields.ItemFields;
			foreach (OrgChartRenderedField orgChartRenderedField in itemFields)
			{
				string text = properties.GetPropertyValue(dataItem, orgChartRenderedField.DataField) as string;
				this.RenderedFields.Add(new OrgChartRenderedField
				{
					Text = text,
					MasterField = orgChartRenderedField
				});
			}
		}

		// Token: 0x0600743E RID: 29758 RVA: 0x001B1E48 File Offset: 0x001B0048
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600743F RID: 29759 RVA: 0x001B1E50 File Offset: 0x001B0050
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader.HasAttributes)
			{
				this.ImageUrl = reader.GetAttribute("ImageUrl");
				this.ImageAltText = reader.GetAttribute("ImageAltText");
				this.Text = reader.GetAttribute("Text");
				this._groupItemID = reader.GetAttribute("ID");
			}
			while (reader.Read())
			{
				if (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Comment)
				{
					using (XmlReader xmlReader = reader.ReadSubtree())
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartRenderedFieldCollection));
						OrgChartRenderedFieldCollection orgChartRenderedFieldCollection = (OrgChartRenderedFieldCollection)xmlSerializer.Deserialize(xmlReader);
						foreach (OrgChartRenderedField item in orgChartRenderedFieldCollection)
						{
							this.RenderedFields.Add(item);
						}
						reader.MoveToContent();
					}
				}
			}
		}

		// Token: 0x06007440 RID: 29760 RVA: 0x001B1F5C File Offset: 0x001B015C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlForOwnedAttributes(writer);
		}

		// Token: 0x06007441 RID: 29761 RVA: 0x001B1F68 File Offset: 0x001B0168
		private void WriteXmlForOwnedAttributes(XmlWriter writer)
		{
			if (this.ImageUrl != null)
			{
				writer.WriteAttributeString("ImageUrl", this.ImageUrl);
			}
			if (this.ImageAltText != null)
			{
				writer.WriteAttributeString("ImageAltText", this.ImageAltText);
			}
			if (this.Text != null)
			{
				writer.WriteAttributeString("Text", this.Text);
			}
			if (!string.IsNullOrEmpty(this.ID))
			{
				writer.WriteAttributeString("ID", this.ID);
			}
			if (this.RenderedFields.Count > 0)
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrgChartRenderedFieldCollection));
				xmlSerializer.Serialize(writer, this.RenderedFields);
			}
		}

		// Token: 0x06007442 RID: 29762 RVA: 0x001B2009 File Offset: 0x001B0209
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06007443 RID: 29763 RVA: 0x001B2018 File Offset: 0x001B0218
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._renderer != null)
			{
				this._renderer.Dispose();
				this._renderer = null;
			}
		}

		// Token: 0x04001F89 RID: 8073
		private OrgChartGroupItemRendererBase _renderer;

		// Token: 0x04001F8A RID: 8074
		private OrgChartRenderedFieldCollection _renderedFields;

		// Token: 0x04001F8B RID: 8075
		private ITemplate _template;

		// Token: 0x04001F8C RID: 8076
		private object _dataItem;

		// Token: 0x04001F8D RID: 8077
		private string _groupItemID = string.Empty;
	}
}
