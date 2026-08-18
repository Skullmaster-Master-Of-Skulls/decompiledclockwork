using System;
using Telerik.Web.UI.ImageEditor;

namespace Telerik.Web.UI
{
	// Token: 0x02000E44 RID: 3652
	public class ImageEditorLoadingEventArgs : EventArgs
	{
		// Token: 0x06008AB0 RID: 35504 RVA: 0x001F9EE3 File Offset: 0x001F80E3
		public ImageEditorLoadingEventArgs(EditableImage image)
		{
			this.Image = image;
		}

		// Token: 0x17002BD2 RID: 11218
		// (get) Token: 0x06008AB1 RID: 35505 RVA: 0x001F9EF2 File Offset: 0x001F80F2
		// (set) Token: 0x06008AB2 RID: 35506 RVA: 0x001F9EFA File Offset: 0x001F80FA
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x17002BD3 RID: 11219
		// (get) Token: 0x06008AB3 RID: 35507 RVA: 0x001F9F03 File Offset: 0x001F8103
		// (set) Token: 0x06008AB4 RID: 35508 RVA: 0x001F9F0B File Offset: 0x001F810B
		public EditableImage Image { get; set; }

		// Token: 0x040026CE RID: 9934
		private bool _cancel;
	}
}
