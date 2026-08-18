using System;

namespace System.Web.UI
{
	// Token: 0x02000062 RID: 98
	public sealed class RegisteredArrayDeclaration
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00013A4B File Offset: 0x00011C4B
		internal RegisteredArrayDeclaration(Control control, string arrayName, string arrayValue)
		{
			this._control = control;
			this._name = arrayName;
			this._value = arrayValue;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00013A68 File Offset: 0x00011C68
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00013A70 File Offset: 0x00011C70
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00013A78 File Offset: 0x00011C78
		public Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x04000152 RID: 338
		private Control _control;

		// Token: 0x04000153 RID: 339
		private string _name;

		// Token: 0x04000154 RID: 340
		private string _value;
	}
}
