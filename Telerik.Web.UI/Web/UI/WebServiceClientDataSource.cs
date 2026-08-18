using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.DataSourceSettings;

namespace Telerik.Web.UI
{
	// Token: 0x02000065 RID: 101
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ToolboxItem(false)]
	public class WebServiceClientDataSource : RadClientDataSource
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000B386 File Offset: 0x00009586
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Description("Contains settings about the web service data sources used in RadClientDataSource.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WebServiceDataSourceSettings WebServiceSettings
		{
			get
			{
				if (this._dataSource == null)
				{
					this._dataSource = new ClientDataSourceSettings(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._dataSource).TrackViewState();
				}
				return this._dataSource.WebServiceDataSourceSettings;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000B3BA File Offset: 0x000095BA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Client")]
		[Browsable(false)]
		[Description("Contains settings about the web service data sources used in RadClientDataSource.")]
		public override ClientDataSourceSettings DataSource
		{
			get
			{
				if (this._dataSource == null)
				{
					this._dataSource = new ClientDataSourceSettings(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._dataSource).TrackViewState();
				}
				return this._dataSource;
			}
		}

		// Token: 0x04000082 RID: 130
		private ClientDataSourceSettings _dataSource;
	}
}
