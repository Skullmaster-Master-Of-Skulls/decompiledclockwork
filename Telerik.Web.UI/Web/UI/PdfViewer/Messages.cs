using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000668 RID: 1640
	public class Messages : StateManager, IDefaultCheck
	{
		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x000C2F7A File Offset: 0x000C117A
		// (set) Token: 0x06003C00 RID: 15360 RVA: 0x000C2F9A File Offset: 0x000C119A
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

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06003C01 RID: 15361 RVA: 0x000C2FAD File Offset: 0x000C11AD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ToolBarMessages ToolBarMessages
		{
			get
			{
				if (this._toolBarMessages == null)
				{
					this._toolBarMessages = new ToolBarMessages();
				}
				return this._toolBarMessages;
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x000C2FC8 File Offset: 0x000C11C8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ErrorMessages ErrorMessages
		{
			get
			{
				if (this._errorMessages == null)
				{
					this._errorMessages = new ErrorMessages();
				}
				return this._errorMessages;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06003C03 RID: 15363 RVA: 0x000C2FE3 File Offset: 0x000C11E3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Dialogs DialogsMessages
		{
			get
			{
				if (this._dialogs == null)
				{
					this._dialogs = new Dialogs();
				}
				return this._dialogs;
			}
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x000C2FFE File Offset: 0x000C11FE
		internal override void SetDirty()
		{
			base.SetDirty();
			this.DialogsMessages.SetDirty();
			this.ErrorMessages.SetDirty();
			this.ToolBarMessages.SetDirty();
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x000C3028 File Offset: 0x000C1228
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.DialogsMessages).LoadViewState(array[num++]);
			((IStateManager)this.ErrorMessages).LoadViewState(array[num++]);
			((IStateManager)this.ToolBarMessages).LoadViewState(array[num++]);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x000C3084 File Offset: 0x000C1284
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.DialogsMessages).SaveViewState(),
				((IStateManager)this.ErrorMessages).SaveViewState(),
				((IStateManager)this.ToolBarMessages).SaveViewState()
			};
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000C30CE File Offset: 0x000C12CE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.DialogsMessages).TrackViewState();
			((IStateManager)this.ErrorMessages).TrackViewState();
			((IStateManager)this.ToolBarMessages).TrackViewState();
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x000C30F7 File Offset: 0x000C12F7
		public bool IsDefault
		{
			get
			{
				return this.DefaultFileName == "Document" && this.ToolBarMessages.IsDefault && this.ErrorMessages.IsDefault && this.DialogsMessages.IsDefault;
			}
		}

		// Token: 0x04001029 RID: 4137
		private ToolBarMessages _toolBarMessages;

		// Token: 0x0400102A RID: 4138
		private ErrorMessages _errorMessages;

		// Token: 0x0400102B RID: 4139
		private Dialogs _dialogs;
	}
}
