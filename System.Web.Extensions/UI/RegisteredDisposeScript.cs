using System;

namespace System.Web.UI
{
	// Token: 0x02000063 RID: 99
	public sealed class RegisteredDisposeScript
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00013A80 File Offset: 0x00011C80
		internal RegisteredDisposeScript(Control control, string disposeScript, UpdatePanel parentUpdatePanel)
		{
			this._control = control;
			this._script = disposeScript;
			this._parentUpdatePanel = parentUpdatePanel;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00013A9D File Offset: 0x00011C9D
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00013AA5 File Offset: 0x00011CA5
		public string Script
		{
			get
			{
				return this._script;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00013AAD File Offset: 0x00011CAD
		internal UpdatePanel ParentUpdatePanel
		{
			get
			{
				return this._parentUpdatePanel;
			}
		}

		// Token: 0x04000155 RID: 341
		private Control _control;

		// Token: 0x04000156 RID: 342
		private UpdatePanel _parentUpdatePanel;

		// Token: 0x04000157 RID: 343
		private string _script;
	}
}
