using System;

namespace Telerik.Web.UI
{
	// Token: 0x020009A3 RID: 2467
	public class RadXmlHttpPanelEventArgs : EventArgs
	{
		// Token: 0x06005E3C RID: 24124 RVA: 0x0011FC65 File Offset: 0x0011DE65
		public RadXmlHttpPanelEventArgs(string val)
		{
			this._value = val;
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x0011FC74 File Offset: 0x0011DE74
		// (set) Token: 0x06005E3E RID: 24126 RVA: 0x0011FC7C File Offset: 0x0011DE7C
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x040016AF RID: 5807
		private string _value;
	}
}
