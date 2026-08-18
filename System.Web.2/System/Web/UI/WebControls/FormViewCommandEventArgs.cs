using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003FF RID: 1023
	public class FormViewCommandEventArgs : CommandEventArgs
	{
		// Token: 0x060031F7 RID: 12791 RVA: 0x000A3701 File Offset: 0x000A1901
		public FormViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs) : base(originalArgs)
		{
			this._commandSource = commandSource;
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000A3711 File Offset: 0x000A1911
		public object CommandSource
		{
			get
			{
				return this._commandSource;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x000A3719 File Offset: 0x000A1919
		// (set) Token: 0x060031FA RID: 12794 RVA: 0x000A3721 File Offset: 0x000A1921
		public bool Handled { get; set; }

		// Token: 0x040020F0 RID: 8432
		private object _commandSource;
	}
}
