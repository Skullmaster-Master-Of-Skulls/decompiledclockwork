using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200074F RID: 1871
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridExportSettings : StateManager
	{
		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x000D012B File Offset: 0x000CE32B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Export")]
		public PivotGridExcelExportSettings Excel
		{
			get
			{
				if (this._excelSettings == null)
				{
					this._excelSettings = new PivotGridExcelExportSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._excelSettings).TrackViewState();
					}
				}
				return this._excelSettings;
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06004242 RID: 16962 RVA: 0x000D0159 File Offset: 0x000CE359
		// (set) Token: 0x06004243 RID: 16963 RVA: 0x000D0188 File Offset: 0x000CE388
		[NotifyParentProperty(true)]
		[DefaultValue("RadPivotGridExport")]
		[Description("")]
		public string FileName
		{
			get
			{
				if (base.ViewState["_fn"] == null)
				{
					return "RadPivotGridExport";
				}
				return (string)base.ViewState["_fn"];
			}
			set
			{
				base.ViewState["_fn"] = value;
			}
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06004244 RID: 16964 RVA: 0x000D019B File Offset: 0x000CE39B
		// (set) Token: 0x06004245 RID: 16965 RVA: 0x000D01C6 File Offset: 0x000CE3C6
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool IgnorePaging
		{
			get
			{
				return base.ViewState["_ip"] != null && (bool)base.ViewState["_ip"];
			}
			set
			{
				base.ViewState["_ip"] = value;
			}
		}

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06004246 RID: 16966 RVA: 0x000D01DE File Offset: 0x000CE3DE
		// (set) Token: 0x06004247 RID: 16967 RVA: 0x000D0209 File Offset: 0x000CE409
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("")]
		public bool UseItemStyles
		{
			get
			{
				return base.ViewState["_uitms"] != null && (bool)base.ViewState["_uitms"];
			}
			set
			{
				base.ViewState["_uitms"] = value;
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06004248 RID: 16968 RVA: 0x000D0221 File Offset: 0x000CE421
		// (set) Token: 0x06004249 RID: 16969 RVA: 0x000D024C File Offset: 0x000CE44C
		[DefaultValue(false)]
		[Description("")]
		[NotifyParentProperty(true)]
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

		// Token: 0x0600424A RID: 16970 RVA: 0x000D0264 File Offset: 0x000CE464
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.Excel).TrackViewState();
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x000D0288 File Offset: 0x000CE488
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.Excel).LoadViewState(array[num++]);
			}
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x000D02C0 File Offset: 0x000CE4C0
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Excel).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x04001192 RID: 4498
		private PivotGridExcelExportSettings _excelSettings;
	}
}
