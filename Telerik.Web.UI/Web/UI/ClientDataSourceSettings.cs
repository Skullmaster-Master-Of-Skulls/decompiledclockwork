using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.DataSourceSettings;

namespace Telerik.Web.UI
{
	// Token: 0x02000104 RID: 260
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientDataSourceSettings : StateManager
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x00026BE3 File Offset: 0x00024DE3
		public ClientDataSourceSettings(RadClientDataSource ownerDataSource)
		{
			this._ownerDataSource = ownerDataSource;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00026BF2 File Offset: 0x00024DF2
		[Description("Contains web service data source settings for the RadClientDataSource.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual WebServiceDataSourceSettings WebServiceDataSourceSettings
		{
			get
			{
				if (this._webServiceDataSourceSettings == null)
				{
					this._webServiceDataSourceSettings = new WebServiceDataSourceSettings();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this._webServiceDataSourceSettings).TrackViewState();
				}
				return this._webServiceDataSourceSettings;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00026C20 File Offset: 0x00024E20
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Contains data source control settings for the RadClientDataSource.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual DataSourceControlSettings DataSourceControlSettings
		{
			get
			{
				if (this._dataSourceControlSettings == null)
				{
					this._dataSourceControlSettings = new DataSourceControlSettings();
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this._dataSourceControlSettings).TrackViewState();
				}
				return this._dataSourceControlSettings;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00026C4E File Offset: 0x00024E4E
		private StaticDataSourceSettings StaticDataSourceSettings
		{
			get
			{
				if (this._staticDataSourceSettings == null)
				{
					this._staticDataSourceSettings = new StaticDataSourceSettings(this._ownerDataSource);
				}
				if (this.IsTrackingViewState)
				{
					((IStateManager)this._staticDataSourceSettings).TrackViewState();
				}
				return this._staticDataSourceSettings;
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00026C84 File Offset: 0x00024E84
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.WebServiceDataSourceSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.DataSourceControlSettings).SaveViewState());
			arrayList.Add(((IStateManager)this.StaticDataSourceSettings).SaveViewState());
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00026CEC File Offset: 0x00024EEC
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.WebServiceDataSourceSettings).LoadViewState(array[num++]);
				((IStateManager)this.DataSourceControlSettings).LoadViewState(array[num++]);
				((IStateManager)this.StaticDataSourceSettings).LoadViewState(array[num++]);
			}
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00026D48 File Offset: 0x00024F48
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.IsTrackingViewState)
			{
				return;
			}
			((IStateManager)this.WebServiceDataSourceSettings).TrackViewState();
			((IStateManager)this.DataSourceControlSettings).TrackViewState();
			((IStateManager)this.StaticDataSourceSettings).TrackViewState();
		}

		// Token: 0x040002A2 RID: 674
		private WebServiceDataSourceSettings _webServiceDataSourceSettings;

		// Token: 0x040002A3 RID: 675
		private DataSourceControlSettings _dataSourceControlSettings;

		// Token: 0x040002A4 RID: 676
		private StaticDataSourceSettings _staticDataSourceSettings;

		// Token: 0x040002A5 RID: 677
		private RadClientDataSource _ownerDataSource;
	}
}
