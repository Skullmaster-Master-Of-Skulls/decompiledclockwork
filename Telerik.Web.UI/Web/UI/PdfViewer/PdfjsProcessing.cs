using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200066C RID: 1644
	public class PdfjsProcessing : StateManager, IDefaultCheck
	{
		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000C349A File Offset: 0x000C169A
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x000C34BA File Offset: 0x000C16BA
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		public string File
		{
			get
			{
				return (string)(base.ViewState["File"] ?? "");
			}
			set
			{
				base.ViewState["File"] = value;
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000C34CD File Offset: 0x000C16CD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public File FileSettings
		{
			get
			{
				if (this._file == null)
				{
					this._file = new File();
				}
				return this._file;
			}
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x000C34E8 File Offset: 0x000C16E8
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FileSettings.SetDirty();
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x000C34FC File Offset: 0x000C16FC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FileSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x000C3534 File Offset: 0x000C1734
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FileSettings).SaveViewState()
			};
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x000C3562 File Offset: 0x000C1762
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FileSettings).TrackViewState();
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x000C3575 File Offset: 0x000C1775
		public bool IsDefault
		{
			get
			{
				return this.File == "" && this.FileSettings.IsDefault;
			}
		}

		// Token: 0x0400102C RID: 4140
		private File _file;
	}
}
