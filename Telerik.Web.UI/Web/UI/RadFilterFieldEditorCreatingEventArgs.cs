using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200189D RID: 6301
	public class RadFilterFieldEditorCreatingEventArgs : EventArgs
	{
		// Token: 0x0600F3BA RID: 62394 RVA: 0x00376ED6 File Offset: 0x003750D6
		public RadFilterFieldEditorCreatingEventArgs(RadFilterDataFieldEditor editor, string editorType)
		{
			this._editor = editor;
			this._editorType = editorType;
		}

		// Token: 0x17004972 RID: 18802
		// (get) Token: 0x0600F3BB RID: 62395 RVA: 0x00376EF7 File Offset: 0x003750F7
		// (set) Token: 0x0600F3BC RID: 62396 RVA: 0x00376EFF File Offset: 0x003750FF
		public RadFilterDataFieldEditor Editor
		{
			get
			{
				return this._editor;
			}
			set
			{
				this._editor = value;
			}
		}

		// Token: 0x17004973 RID: 18803
		// (get) Token: 0x0600F3BD RID: 62397 RVA: 0x00376F08 File Offset: 0x00375108
		public string EditorType
		{
			get
			{
				return this._editorType;
			}
		}

		// Token: 0x040045E2 RID: 17890
		private string _editorType = "";

		// Token: 0x040045E3 RID: 17891
		private RadFilterDataFieldEditor _editor;
	}
}
