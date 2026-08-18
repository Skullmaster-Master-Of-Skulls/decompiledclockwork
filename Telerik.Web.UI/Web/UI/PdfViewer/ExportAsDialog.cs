using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000662 RID: 1634
	public class ExportAsDialog : StateManager, IDefaultCheck
	{
		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x000C2A3A File Offset: 0x000C0C3A
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x000C2A5A File Offset: 0x000C0C5A
		[DefaultValue("Export...")]
		public string Title
		{
			get
			{
				return (string)(base.ViewState["Title"] ?? "Export...");
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x000C2A6D File Offset: 0x000C0C6D
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x000C2A8D File Offset: 0x000C0C8D
		[DefaultValue("Document")]
		public string DefaultFileName
		{
			get
			{
				return (string)(base.ViewState["DefaultFileName"] ?? "Document");
			}
			set
			{
				base.ViewState["DefaultFileName"] = value;
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x000C2AA0 File Offset: 0x000C0CA0
		// (set) Token: 0x06003BDC RID: 15324 RVA: 0x000C2AC0 File Offset: 0x000C0CC0
		[DefaultValue("Portable Document Format (.pdf)")]
		public string Pdf
		{
			get
			{
				return (string)(base.ViewState["Pdf"] ?? "Portable Document Format (.pdf)");
			}
			set
			{
				base.ViewState["Pdf"] = value;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x000C2AD3 File Offset: 0x000C0CD3
		// (set) Token: 0x06003BDE RID: 15326 RVA: 0x000C2AF3 File Offset: 0x000C0CF3
		[DefaultValue("Portable Network Graphics (.png)")]
		public string Png
		{
			get
			{
				return (string)(base.ViewState["Png"] ?? "Portable Network Graphics (.png)");
			}
			set
			{
				base.ViewState["Png"] = value;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x000C2B06 File Offset: 0x000C0D06
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x000C2B26 File Offset: 0x000C0D26
		[DefaultValue("Scalable Vector Graphics (.svg)")]
		public string Svg
		{
			get
			{
				return (string)(base.ViewState["Svg"] ?? "Scalable Vector Graphics (.svg)");
			}
			set
			{
				base.ViewState["Svg"] = value;
			}
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x000C2B39 File Offset: 0x000C0D39
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Labels LabelsMessages
		{
			get
			{
				if (this._labels == null)
				{
					this._labels = new Labels();
				}
				return this._labels;
			}
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000C2B54 File Offset: 0x000C0D54
		internal override void SetDirty()
		{
			base.SetDirty();
			this.LabelsMessages.SetDirty();
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000C2B68 File Offset: 0x000C0D68
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.LabelsMessages).LoadViewState(array[num++]);
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000C2BA0 File Offset: 0x000C0DA0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.LabelsMessages).SaveViewState()
			};
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x000C2BCE File Offset: 0x000C0DCE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.LabelsMessages).TrackViewState();
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000C2BE4 File Offset: 0x000C0DE4
		public bool IsDefault
		{
			get
			{
				return this.Title == "Export..." && this.DefaultFileName == "Document" && this.Pdf == "Portable Document Format (.pdf)" && this.Png == "Portable Network Graphics (.png)" && this.Svg == "Scalable Vector Graphics (.svg)" && this.LabelsMessages.IsDefault;
			}
		}

		// Token: 0x04001028 RID: 4136
		private Labels _labels;
	}
}
