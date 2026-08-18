using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200066F RID: 1647
	public class ToolBarMessages : StateManager, IDefaultCheck
	{
		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x000C3826 File Offset: 0x000C1A26
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x000C3846 File Offset: 0x000C1A46
		[DefaultValue("Open")]
		public string Open
		{
			get
			{
				return (string)(base.ViewState["Open"] ?? "Open");
			}
			set
			{
				base.ViewState["Open"] = value;
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x000C3859 File Offset: 0x000C1A59
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x000C3879 File Offset: 0x000C1A79
		[DefaultValue("Export")]
		public string ExportAs
		{
			get
			{
				return (string)(base.ViewState["ExportAs"] ?? "Export");
			}
			set
			{
				base.ViewState["ExportAs"] = value;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x000C388C File Offset: 0x000C1A8C
		// (set) Token: 0x06003C34 RID: 15412 RVA: 0x000C38AC File Offset: 0x000C1AAC
		[DefaultValue("Download")]
		public string Download
		{
			get
			{
				return (string)(base.ViewState["Download"] ?? "Download");
			}
			set
			{
				base.ViewState["Download"] = value;
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06003C35 RID: 15413 RVA: 0x000C38BF File Offset: 0x000C1ABF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Pager PagerMessages
		{
			get
			{
				if (this._pager == null)
				{
					this._pager = new Pager();
				}
				return this._pager;
			}
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x000C38DA File Offset: 0x000C1ADA
		internal override void SetDirty()
		{
			base.SetDirty();
			this.PagerMessages.SetDirty();
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000C38F0 File Offset: 0x000C1AF0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.PagerMessages).LoadViewState(array[num++]);
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x000C3928 File Offset: 0x000C1B28
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.PagerMessages).SaveViewState()
			};
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x000C3956 File Offset: 0x000C1B56
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.PagerMessages).TrackViewState();
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x000C396C File Offset: 0x000C1B6C
		public bool IsDefault
		{
			get
			{
				return this.Open == "Open" && this.ExportAs == "Export" && this.Download == "Download" && this.PagerMessages.IsDefault;
			}
		}

		// Token: 0x0400102D RID: 4141
		private Pager _pager;
	}
}
