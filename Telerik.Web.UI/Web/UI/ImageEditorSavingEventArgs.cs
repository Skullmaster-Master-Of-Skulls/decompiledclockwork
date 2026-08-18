using System;
using Telerik.Web.UI.ImageEditor;

namespace Telerik.Web.UI
{
	// Token: 0x02000E45 RID: 3653
	public class ImageEditorSavingEventArgs : ImageEditorEventArgs
	{
		// Token: 0x06008AB5 RID: 35509 RVA: 0x001F9F14 File Offset: 0x001F8114
		public ImageEditorSavingEventArgs(EditableImage image) : this(image, "", true)
		{
		}

		// Token: 0x06008AB6 RID: 35510 RVA: 0x001F9F23 File Offset: 0x001F8123
		public ImageEditorSavingEventArgs(EditableImage image, string fileName) : this(image, fileName, true)
		{
		}

		// Token: 0x06008AB7 RID: 35511 RVA: 0x001F9F2E File Offset: 0x001F812E
		public ImageEditorSavingEventArgs(EditableImage image, string fileName, bool overwriteFile) : this(image, fileName, overwriteFile, string.Empty)
		{
		}

		// Token: 0x06008AB8 RID: 35512 RVA: 0x001F9F3E File Offset: 0x001F813E
		public ImageEditorSavingEventArgs(EditableImage image, string fileName, bool overwriteFile, string argument) : base(image)
		{
			this.FileName = fileName;
			this.OverwriteFile = overwriteFile;
			this.Argument = argument;
		}

		// Token: 0x17002BD4 RID: 11220
		// (get) Token: 0x06008AB9 RID: 35513 RVA: 0x001F9F68 File Offset: 0x001F8168
		// (set) Token: 0x06008ABA RID: 35514 RVA: 0x001F9F70 File Offset: 0x001F8170
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

		// Token: 0x17002BD5 RID: 11221
		// (get) Token: 0x06008ABB RID: 35515 RVA: 0x001F9F79 File Offset: 0x001F8179
		// (set) Token: 0x06008ABC RID: 35516 RVA: 0x001F9F81 File Offset: 0x001F8181
		public string FileName
		{
			get
			{
				return this._fName;
			}
			set
			{
				this._fName = value;
			}
		}

		// Token: 0x17002BD6 RID: 11222
		// (get) Token: 0x06008ABD RID: 35517 RVA: 0x001F9F8A File Offset: 0x001F818A
		// (set) Token: 0x06008ABE RID: 35518 RVA: 0x001F9F92 File Offset: 0x001F8192
		public bool OverwriteFile { get; set; }

		// Token: 0x17002BD7 RID: 11223
		// (get) Token: 0x06008ABF RID: 35519 RVA: 0x001F9F9B File Offset: 0x001F819B
		// (set) Token: 0x06008AC0 RID: 35520 RVA: 0x001F9FA3 File Offset: 0x001F81A3
		public string Argument { get; set; }

		// Token: 0x040026D0 RID: 9936
		private bool _cancel;

		// Token: 0x040026D1 RID: 9937
		private string _fName = string.Empty;
	}
}
