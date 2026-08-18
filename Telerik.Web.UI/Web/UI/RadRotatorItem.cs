using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001A00 RID: 6656
	[ToolboxItem(false)]
	[XmlRoot("Item")]
	public class RadRotatorItem : WebControl, INamingContainer, IMarkableStateManager, IStateManager
	{
		// Token: 0x060101AC RID: 65964 RVA: 0x0039EBBE File Offset: 0x0039CDBE
		public RadRotatorItem()
		{
		}

		// Token: 0x060101AD RID: 65965 RVA: 0x0039EBC6 File Offset: 0x0039CDC6
		public RadRotatorItem(object dataItem)
		{
			this.DataItem = dataItem;
		}

		// Token: 0x17004DBB RID: 19899
		// (get) Token: 0x060101AE RID: 65966 RVA: 0x0039EBD5 File Offset: 0x0039CDD5
		// (set) Token: 0x060101AF RID: 65967 RVA: 0x0039EBDD File Offset: 0x0039CDDD
		[Browsable(false)]
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
			set
			{
				this._dataItem = value;
			}
		}

		// Token: 0x17004DBC RID: 19900
		// (get) Token: 0x060101B0 RID: 65968 RVA: 0x0039EBE6 File Offset: 0x0039CDE6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Index
		{
			get
			{
				return this._container.Items.IndexOf(this);
			}
		}

		// Token: 0x17004DBD RID: 19901
		// (get) Token: 0x060101B1 RID: 65969 RVA: 0x0039EBF9 File Offset: 0x0039CDF9
		// (set) Token: 0x060101B2 RID: 65970 RVA: 0x0039EC01 File Offset: 0x0039CE01
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(RadRotatorItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[Bindable(false)]
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

		// Token: 0x17004DBE RID: 19902
		// (get) Token: 0x060101B3 RID: 65971 RVA: 0x0039EC0A File Offset: 0x0039CE0A
		// (set) Token: 0x060101B4 RID: 65972 RVA: 0x0039EC12 File Offset: 0x0039CE12
		internal bool TemplateInstantiated
		{
			get
			{
				return this._templateInstantiated;
			}
			set
			{
				this._templateInstantiated = value;
			}
		}

		// Token: 0x17004DBF RID: 19903
		// (get) Token: 0x060101B5 RID: 65973 RVA: 0x0039EC1B File Offset: 0x0039CE1B
		// (set) Token: 0x060101B6 RID: 65974 RVA: 0x0039EC23 File Offset: 0x0039CE23
		private protected RadRotator Container
		{
			protected get
			{
				return this._container;
			}
			private set
			{
				this._container = value;
			}
		}

		// Token: 0x060101B7 RID: 65975 RVA: 0x0039EC2C File Offset: 0x0039CE2C
		protected internal void SetItemContainer(RadRotator itemContainer)
		{
			this.Container = itemContainer;
			itemContainer.InitializeItem(this);
		}

		// Token: 0x060101B8 RID: 65976 RVA: 0x0039EC3C File Offset: 0x0039CE3C
		void IMarkableStateManager.SetDirty()
		{
			this.ViewState.SetDirty(true);
			base.ControlStyle.SetDirty();
		}

		// Token: 0x17004DC0 RID: 19904
		// (get) Token: 0x060101B9 RID: 65977 RVA: 0x0039EC55 File Offset: 0x0039CE55
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return base.IsTrackingViewState;
			}
		}

		// Token: 0x060101BA RID: 65978 RVA: 0x0039EC60 File Offset: 0x0039CE60
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			this.LoadViewState(array[0]);
		}

		// Token: 0x060101BB RID: 65979 RVA: 0x0039EC80 File Offset: 0x0039CE80
		object IStateManager.SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x060101BC RID: 65980 RVA: 0x0039ECA0 File Offset: 0x0039CEA0
		void IStateManager.TrackViewState()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.A);
			webControl.CopyBaseAttributes(this);
			base.TrackViewState();
			base.CopyBaseAttributes(webControl);
		}

		// Token: 0x17004DC1 RID: 19905
		// (get) Token: 0x060101BD RID: 65981 RVA: 0x0039ECC8 File Offset: 0x0039CEC8
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x060101BE RID: 65982 RVA: 0x0039ECCC File Offset: 0x0039CECC
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrItem");
			if (!this.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			}
			if (!this.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			base.RenderBeginTag(writer);
		}

		// Token: 0x060101BF RID: 65983 RVA: 0x0039ED50 File Offset: 0x0039CF50
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Unit height = this.Height;
			Unit width = this.Width;
			if (!this.Height.IsEmpty)
			{
				this.Height = Unit.Empty;
			}
			if (!this.Width.IsEmpty)
			{
				this.Width = Unit.Empty;
			}
			base.AddAttributesToRender(writer);
			if (!height.IsEmpty)
			{
				this.Height = height;
			}
			if (!width.IsEmpty)
			{
				this.Width = width;
			}
		}

		// Token: 0x060101C0 RID: 65984 RVA: 0x0039EDC8 File Offset: 0x0039CFC8
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			base.RenderEndTag(writer);
			writer.RenderEndTag();
		}

		// Token: 0x040048EE RID: 18670
		private object _dataItem;

		// Token: 0x040048EF RID: 18671
		private RadRotator _container;

		// Token: 0x040048F0 RID: 18672
		private ITemplate _itemTemplate;

		// Token: 0x040048F1 RID: 18673
		private bool _templateInstantiated;
	}
}
