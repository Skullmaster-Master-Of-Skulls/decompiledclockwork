using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000AFC RID: 2812
	public class NavigationControlWebServiceSettings : WebServiceSettings
	{
		// Token: 0x0600698B RID: 27019 RVA: 0x0018D059 File Offset: 0x0018B259
		public NavigationControlWebServiceSettings(string prefix, StateBag viewState) : base(prefix, viewState)
		{
		}

		// Token: 0x0600698C RID: 27020 RVA: 0x0018D063 File Offset: 0x0018B263
		public NavigationControlWebServiceSettings(StateBag viewState) : this("WebServiceSettings", viewState)
		{
		}

		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x0600698D RID: 27021 RVA: 0x0018D071 File Offset: 0x0018B271
		internal bool IsOData
		{
			get
			{
				return this._odataSettings != null;
			}
		}

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x0600698E RID: 27022 RVA: 0x0018D07F File Offset: 0x0018B27F
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Obsolete("These OData Setting are deprecated, use the RadODataDataSource control when consuming OData services")]
		[DefaultValue(null)]
		[Description("OData settings")]
		[Category("Behavior")]
		public ODataSettings ODataSettings
		{
			get
			{
				if (this._odataSettings == null)
				{
					this._odataSettings = new ODataSettings();
				}
				return this._odataSettings;
			}
		}

		// Token: 0x0600698F RID: 27023 RVA: 0x0018D09A File Offset: 0x0018B29A
		internal override void Describe(string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			if (!this.IsOData)
			{
				base.Describe(propertyName, serializer, descriptor);
				return;
			}
			this.ODataSettings.Describe(this, propertyName, serializer, descriptor);
		}

		// Token: 0x04001C82 RID: 7298
		private ODataSettings _odataSettings;
	}
}
