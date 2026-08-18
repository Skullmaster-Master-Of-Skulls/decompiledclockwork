using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.DataSourceSettings
{
	// Token: 0x02000108 RID: 264
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class WebServiceDataSourceSettings : StateManager
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x000274DC File Offset: 0x000256DC
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00027505 File Offset: 0x00025705
		[Category("Client")]
		[Description("Gets or sets the type of the service that will provide data for the data source. The default is Default")]
		[NotifyParentProperty(true)]
		[DefaultValue(ClientDataSourceServiceType.Default)]
		public virtual ClientDataSourceServiceType ServiceType
		{
			get
			{
				object obj = base.ViewState["ServiceType"];
				if (obj != null)
				{
					return (ClientDataSourceServiceType)obj;
				}
				return ClientDataSourceServiceType.Default;
			}
			set
			{
				base.ViewState["ServiceType"] = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0002751D File Offset: 0x0002571D
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0002753D File Offset: 0x0002573D
		[Description("Gets or sets the base URL of the web service.")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		[Category("Client")]
		public virtual string BaseUrl
		{
			get
			{
				return (base.ViewState["CDSBaseUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CDSBaseUrl"] = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00027550 File Offset: 0x00025750
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[Description("Contains service settings for the select method of the RadClientDataSource.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual WebServiceBaseSettings Select
		{
			get
			{
				if (this._webServiceSelectSettings == null)
				{
					this._webServiceSelectSettings = new WebServiceBaseSettings();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this._webServiceSelectSettings).TrackViewState();
				}
				return this._webServiceSelectSettings;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0002757E File Offset: 0x0002577E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Contains service settings for the insert method of the RadClientDataSource.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual WebServiceBaseSettings Insert
		{
			get
			{
				if (this._webServiceInsertSettings == null)
				{
					this._webServiceInsertSettings = new WebServiceBaseSettings();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this._webServiceInsertSettings).TrackViewState();
				}
				return this._webServiceInsertSettings;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x000275AC File Offset: 0x000257AC
		[Description("Contains service settings for the update method of the RadClientDataSource.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual WebServiceBaseSettings Update
		{
			get
			{
				if (this._webServiceUpdateSettings == null)
				{
					this._webServiceUpdateSettings = new WebServiceBaseSettings();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this._webServiceUpdateSettings).TrackViewState();
				}
				return this._webServiceUpdateSettings;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x000275DA File Offset: 0x000257DA
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Description("Contains service settings for the delete method of the RadClientDataSource.")]
		public virtual WebServiceBaseSettings Delete
		{
			get
			{
				if (this._webServiceDeleteSettings == null)
				{
					this._webServiceDeleteSettings = new WebServiceBaseSettings();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this._webServiceDeleteSettings).TrackViewState();
				}
				return this._webServiceDeleteSettings;
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00027608 File Offset: 0x00025808
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.Select).SaveViewState());
			arrayList.Add(((IStateManager)this.Insert).SaveViewState());
			arrayList.Add(((IStateManager)this.Update).SaveViewState());
			arrayList.Add(((IStateManager)this.Delete).SaveViewState());
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00027684 File Offset: 0x00025884
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.Select).LoadViewState(array[num++]);
				((IStateManager)this.Insert).LoadViewState(array[num++]);
				((IStateManager)this.Update).LoadViewState(array[num++]);
				((IStateManager)this.Delete).LoadViewState(array[num++]);
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000276F2 File Offset: 0x000258F2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.IsTrackingViewState)
			{
				return;
			}
			((IStateManager)this.Select).TrackViewState();
			((IStateManager)this.Insert).TrackViewState();
			((IStateManager)this.Update).TrackViewState();
			((IStateManager)this.Delete).TrackViewState();
		}

		// Token: 0x040002AC RID: 684
		private WebServiceBaseSettings _webServiceSelectSettings;

		// Token: 0x040002AD RID: 685
		private WebServiceBaseSettings _webServiceInsertSettings;

		// Token: 0x040002AE RID: 686
		private WebServiceBaseSettings _webServiceUpdateSettings;

		// Token: 0x040002AF RID: 687
		private WebServiceBaseSettings _webServiceDeleteSettings;
	}
}
