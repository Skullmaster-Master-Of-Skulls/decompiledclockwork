using System;
using System.ComponentModel;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x0200051D RID: 1309
	public class EditableImageConfiguration : StateManager
	{
		// Token: 0x06002EBB RID: 11963 RVA: 0x00098BEC File Offset: 0x00096DEC
		public EditableImageConfiguration(RadImageEditor imageEditor)
		{
			this._imageEditor = imageEditor;
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x00098BFB File Offset: 0x00096DFB
		// (set) Token: 0x06002EBD RID: 11965 RVA: 0x00098C20 File Offset: 0x00096E20
		[DefaultValue(2097152)]
		[Description("Property to define the maximum length of image saving using canvas or applying thousands of client-based commands. The default is 2097152\u00a0characters, which is equivalent to 4\u00a0MB of Unicode string data.")]
		[Category("Behavior")]
		public int MaxJsonLength
		{
			get
			{
				return (int)(base.ViewState["MaxJsonLength"] ?? 2097152);
			}
			set
			{
				base.ViewState["MaxJsonLength"] = value;
				this._imageEditor.UpdateEditableImageHttpPanel_MaxJsonLength();
			}
		}

		// Token: 0x04000C47 RID: 3143
		private readonly RadImageEditor _imageEditor;
	}
}
