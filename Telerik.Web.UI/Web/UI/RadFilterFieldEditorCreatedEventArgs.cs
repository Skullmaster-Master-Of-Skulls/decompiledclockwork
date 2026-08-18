using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200189B RID: 6299
	public class RadFilterFieldEditorCreatedEventArgs : EventArgs
	{
		// Token: 0x0600F3B3 RID: 62387 RVA: 0x00376EB6 File Offset: 0x003750B6
		public RadFilterFieldEditorCreatedEventArgs(RadFilterDataFieldEditor editor)
		{
			this._editor = editor;
		}

		// Token: 0x17004971 RID: 18801
		// (get) Token: 0x0600F3B4 RID: 62388 RVA: 0x00376EC5 File Offset: 0x003750C5
		// (set) Token: 0x0600F3B5 RID: 62389 RVA: 0x00376ECD File Offset: 0x003750CD
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

		// Token: 0x040045E1 RID: 17889
		private RadFilterDataFieldEditor _editor;
	}
}
