using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200122C RID: 4652
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListExportSettings : StateManager, IDisposable
	{
		// Token: 0x0600BFE1 RID: 49121 RVA: 0x002A940B File Offset: 0x002A760B
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			((IStateManager)this.Pdf).TrackViewState();
			((IStateManager)this.Excel).TrackViewState();
			((IStateManager)this.Word).TrackViewState();
		}

		// Token: 0x0600BFE2 RID: 49122 RVA: 0x002A9444 File Offset: 0x002A7644
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.Pdf).LoadViewState(array[num++]);
				((IStateManager)this.Excel).LoadViewState(array[num++]);
				((IStateManager)this.Word).LoadViewState(array[num++]);
			}
		}

		// Token: 0x0600BFE3 RID: 49123 RVA: 0x002A94A0 File Offset: 0x002A76A0
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Pdf).SaveViewState(),
				((IStateManager)this.Excel).SaveViewState(),
				((IStateManager)this.Word).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x17003DE5 RID: 15845
		// (get) Token: 0x0600BFE4 RID: 49124 RVA: 0x002A9506 File Offset: 0x002A7706
		[Category("Export")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListExcelExportSettings Excel
		{
			get
			{
				if (this._excelSettings == null)
				{
					this._excelSettings = new TreeListExcelExportSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._excelSettings).TrackViewState();
					}
				}
				return this._excelSettings;
			}
		}

		// Token: 0x17003DE6 RID: 15846
		// (get) Token: 0x0600BFE5 RID: 49125 RVA: 0x002A9534 File Offset: 0x002A7734
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Export")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListWordExportSettings Word
		{
			get
			{
				if (this._wordSettings == null)
				{
					this._wordSettings = new TreeListWordExportSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._wordSettings).TrackViewState();
					}
				}
				return this._wordSettings;
			}
		}

		// Token: 0x17003DE7 RID: 15847
		// (get) Token: 0x0600BFE6 RID: 49126 RVA: 0x002A9562 File Offset: 0x002A7762
		[Category("Export")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListPdfExportSettings Pdf
		{
			get
			{
				if (this._pdfSettings == null)
				{
					this._pdfSettings = new TreeListPdfExportSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._pdfSettings).TrackViewState();
					}
				}
				return this._pdfSettings;
			}
		}

		// Token: 0x17003DE8 RID: 15848
		// (get) Token: 0x0600BFE7 RID: 49127 RVA: 0x002A9590 File Offset: 0x002A7790
		// (set) Token: 0x0600BFE8 RID: 49128 RVA: 0x002A95BB File Offset: 0x002A77BB
		[NotifyParentProperty(true)]
		[DefaultValue(TreeListExportMode.RemoveControls)]
		[Category("Behavior")]
		public TreeListExportMode ExportMode
		{
			get
			{
				if (base.ViewState["ExportOnlyDataMode"] == null)
				{
					return TreeListExportMode.RemoveControls;
				}
				return (TreeListExportMode)base.ViewState["ExportOnlyDataMode"];
			}
			set
			{
				base.ViewState["ExportOnlyDataMode"] = value;
			}
		}

		// Token: 0x17003DE9 RID: 15849
		// (get) Token: 0x0600BFE9 RID: 49129 RVA: 0x002A95D3 File Offset: 0x002A77D3
		// (set) Token: 0x0600BFEA RID: 49130 RVA: 0x002A95FE File Offset: 0x002A77FE
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool IgnorePaging
		{
			get
			{
				return base.ViewState["IgnorePaging"] != null && (bool)base.ViewState["IgnorePaging"];
			}
			set
			{
				base.ViewState["IgnorePaging"] = value;
			}
		}

		// Token: 0x17003DEA RID: 15850
		// (get) Token: 0x0600BFEB RID: 49131 RVA: 0x002A9616 File Offset: 0x002A7816
		// (set) Token: 0x0600BFEC RID: 49132 RVA: 0x002A9641 File Offset: 0x002A7841
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool OpenInNewWindow
		{
			get
			{
				return base.ViewState["OpenInNewWindow"] != null && (bool)base.ViewState["OpenInNewWindow"];
			}
			set
			{
				base.ViewState["OpenInNewWindow"] = value;
			}
		}

		// Token: 0x17003DEB RID: 15851
		// (get) Token: 0x0600BFED RID: 49133 RVA: 0x002A965C File Offset: 0x002A785C
		// (set) Token: 0x0600BFEE RID: 49134 RVA: 0x002A96BF File Offset: 0x002A78BF
		[DefaultValue("RadTreeList")]
		[Category("Misc")]
		[NotifyParentProperty(true)]
		public string FileName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("[{0}{1}]+", new string(Path.GetInvalidFileNameChars()), new string(Path.GetInvalidPathChars()));
				string input = (base.ViewState["FileName"] as string) ?? "RadTreeList";
				return Regex.Replace(input, stringBuilder.ToString(), "_");
			}
			set
			{
				base.ViewState["FileName"] = value;
			}
		}

		// Token: 0x0600BFEF RID: 49135 RVA: 0x002A96D2 File Offset: 0x002A78D2
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600BFF0 RID: 49136 RVA: 0x002A96DB File Offset: 0x002A78DB
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._excelSettings != null)
				{
					this._excelSettings.Dispose();
				}
				if (this._pdfSettings != null)
				{
					this._pdfSettings.Dispose();
				}
				if (this._wordSettings != null)
				{
					this._wordSettings.Dispose();
				}
			}
		}

		// Token: 0x0400325A RID: 12890
		private TreeListPdfExportSettings _pdfSettings;

		// Token: 0x0400325B RID: 12891
		private TreeListExcelExportSettings _excelSettings;

		// Token: 0x0400325C RID: 12892
		private TreeListWordExportSettings _wordSettings;
	}
}
