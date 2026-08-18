using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000348 RID: 840
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GanttExportSettings : ObjectWithState
	{
		// Token: 0x06001CA6 RID: 7334 RVA: 0x0005A900 File Offset: 0x00058B00
		public GanttExportSettings(StateBag OwnerStateBag) : base("ges_", OwnerStateBag)
		{
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x0005A90E File Offset: 0x00058B0E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Pdf")]
		public ClientExportManagerPdfSettings Pdf
		{
			get
			{
				if (this._pdfSettings == null)
				{
					this._pdfSettings = new ClientExportManagerPdfSettings();
				}
				return this._pdfSettings;
			}
		}

		// Token: 0x0400074B RID: 1867
		private ClientExportManagerPdfSettings _pdfSettings;
	}
}
