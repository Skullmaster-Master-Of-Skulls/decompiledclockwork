using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200065E RID: 1630
	public class Dialogs : StateManager, IDefaultCheck
	{
		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000C26AE File Offset: 0x000C08AE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ExportAsDialog ExportAsDialogMessages
		{
			get
			{
				if (this._exportAsDialog == null)
				{
					this._exportAsDialog = new ExportAsDialog();
				}
				return this._exportAsDialog;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000C26C9 File Offset: 0x000C08C9
		// (set) Token: 0x06003BBE RID: 15294 RVA: 0x000C26E9 File Offset: 0x000C08E9
		[DefaultValue("OK")]
		public string OkText
		{
			get
			{
				return (string)(base.ViewState["OkText"] ?? "OK");
			}
			set
			{
				base.ViewState["OkText"] = value;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000C26FC File Offset: 0x000C08FC
		// (set) Token: 0x06003BC0 RID: 15296 RVA: 0x000C271C File Offset: 0x000C091C
		[DefaultValue("Save")]
		public string Save
		{
			get
			{
				return (string)(base.ViewState["Save"] ?? "Save");
			}
			set
			{
				base.ViewState["Save"] = value;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000C272F File Offset: 0x000C092F
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x000C274F File Offset: 0x000C094F
		[DefaultValue("Cancel")]
		public string Cancel
		{
			get
			{
				return (string)(base.ViewState["Cancel"] ?? "Cancel");
			}
			set
			{
				base.ViewState["Cancel"] = value;
			}
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000C2762 File Offset: 0x000C0962
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ExportAsDialogMessages.SetDirty();
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000C2778 File Offset: 0x000C0978
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ExportAsDialogMessages).LoadViewState(array[num++]);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000C27B0 File Offset: 0x000C09B0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ExportAsDialogMessages).SaveViewState()
			};
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000C27DE File Offset: 0x000C09DE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ExportAsDialogMessages).TrackViewState();
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000C27F4 File Offset: 0x000C09F4
		public bool IsDefault
		{
			get
			{
				return this.ExportAsDialogMessages.IsDefault && this.OkText == "OK" && this.Save == "Save" && this.Cancel == "Cancel";
			}
		}

		// Token: 0x04001027 RID: 4135
		private ExportAsDialog _exportAsDialog;
	}
}
