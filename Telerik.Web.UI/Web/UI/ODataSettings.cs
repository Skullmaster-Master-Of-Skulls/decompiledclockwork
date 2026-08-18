using System;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B05 RID: 2821
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[RequiredScript(typeof(OData))]
	[ClientScriptResource("Telerik.Web.UI.ODataSettings", "Telerik.Web.UI.Common.Navigation.OData.OData.js")]
	[Obsolete]
	public class ODataSettings
	{
		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x060069A7 RID: 27047 RVA: 0x0018D1D4 File Offset: 0x0018B3D4
		// (set) Token: 0x060069A8 RID: 27048 RVA: 0x0018D1DC File Offset: 0x0018B3DC
		[DefaultValue(ODataResponseType.JSON)]
		[Category("Behavior")]
		[Description("Specifies the url of the web service to be used")]
		public ODataResponseType ResponseType { get; set; }

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x060069A9 RID: 27049 RVA: 0x0018D1E5 File Offset: 0x0018B3E5
		// (set) Token: 0x060069AA RID: 27050 RVA: 0x0018D1ED File Offset: 0x0018B3ED
		[DefaultValue("")]
		[Description("Gets or sets the initial collection to bind against")]
		[Category("Behavior")]
		public string InitialContainerName
		{
			get
			{
				return this.initialContainername;
			}
			set
			{
				this.initialContainername = value;
			}
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x060069AB RID: 27051 RVA: 0x0018D1F6 File Offset: 0x0018B3F6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ODataEntityTypeCollection Entities
		{
			get
			{
				if (this._entities == null)
				{
					this._entities = new ODataEntityTypeCollection();
				}
				return this._entities;
			}
		}

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x060069AC RID: 27052 RVA: 0x0018D211 File Offset: 0x0018B411
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ODataEntityContainerCollection EntityContainer
		{
			get
			{
				if (this._container == null)
				{
					this._container = new ODataEntityContainerCollection();
				}
				return this._container;
			}
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x0018D22C File Offset: 0x0018B42C
		internal virtual void Describe(WebServiceSettings settings, string propertyName, JavaScriptSerializer serializer, IScriptDescriptor descriptor)
		{
			descriptor.AddProperty(propertyName, serializer.Serialize(settings));
		}

		// Token: 0x04001C8F RID: 7311
		private string initialContainername = "";

		// Token: 0x04001C90 RID: 7312
		private ODataEntityTypeCollection _entities;

		// Token: 0x04001C91 RID: 7313
		private ODataEntityContainerCollection _container;
	}
}
