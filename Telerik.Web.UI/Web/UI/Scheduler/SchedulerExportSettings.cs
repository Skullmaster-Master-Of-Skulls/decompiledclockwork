using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000EDB RID: 3803
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class SchedulerExportSettings : ObjectWithState
	{
		// Token: 0x0600906E RID: 36974 RVA: 0x00208E9E File Offset: 0x0020709E
		public SchedulerExportSettings(StateBag OwnerStateBag) : base("ges_", OwnerStateBag)
		{
		}

		// Token: 0x17002DB3 RID: 11699
		// (get) Token: 0x0600906F RID: 36975 RVA: 0x00208EAC File Offset: 0x002070AC
		// (set) Token: 0x06009070 RID: 36976 RVA: 0x00208EDB File Offset: 0x002070DB
		[Description("")]
		[DefaultValue("RadSchedulerExport")]
		[NotifyParentProperty(true)]
		public string FileName
		{
			get
			{
				if (base.ViewState["_fn"] == null)
				{
					return "RadSchedulerExport";
				}
				return (string)base.ViewState["_fn"];
			}
			set
			{
				base.ViewState["_fn"] = value;
			}
		}

		// Token: 0x17002DB4 RID: 11700
		// (get) Token: 0x06009071 RID: 36977 RVA: 0x00208EEE File Offset: 0x002070EE
		// (set) Token: 0x06009072 RID: 36978 RVA: 0x00208F19 File Offset: 0x00207119
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool OpenInNewWindow
		{
			get
			{
				return base.ViewState["_osw"] != null && (bool)base.ViewState["_osw"];
			}
			set
			{
				base.ViewState["_osw"] = value;
			}
		}

		// Token: 0x17002DB5 RID: 11701
		// (get) Token: 0x06009073 RID: 36979 RVA: 0x00208F31 File Offset: 0x00207131
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Pdf")]
		public SchedulerPdfSettings Pdf
		{
			get
			{
				if (this._pdfSettings == null)
				{
					this._pdfSettings = new SchedulerPdfSettings(base.OwnerViewState);
				}
				return this._pdfSettings;
			}
		}

		// Token: 0x040028E0 RID: 10464
		private SchedulerPdfSettings _pdfSettings;
	}
}
