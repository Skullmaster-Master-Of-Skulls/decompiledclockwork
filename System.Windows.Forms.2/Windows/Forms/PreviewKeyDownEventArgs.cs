using System;

namespace System.Windows.Forms
{
	// Token: 0x02000325 RID: 805
	public class PreviewKeyDownEventArgs : EventArgs
	{
		// Token: 0x06003309 RID: 13065 RVA: 0x000E3BB2 File Offset: 0x000E1DB2
		public PreviewKeyDownEventArgs(Keys keyData)
		{
			this._keyData = keyData;
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x000E3BC1 File Offset: 0x000E1DC1
		public bool Alt
		{
			get
			{
				return (this._keyData & Keys.Alt) == Keys.Alt;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x000E3BD6 File Offset: 0x000E1DD6
		public bool Control
		{
			get
			{
				return (this._keyData & Keys.Control) == Keys.Control;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000E3BEC File Offset: 0x000E1DEC
		public Keys KeyCode
		{
			get
			{
				Keys keys = this._keyData & Keys.KeyCode;
				if (!Enum.IsDefined(typeof(Keys), (int)keys))
				{
					return Keys.None;
				}
				return keys;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000E3C20 File Offset: 0x000E1E20
		public int KeyValue
		{
			get
			{
				return (int)(this._keyData & Keys.KeyCode);
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000E3C2E File Offset: 0x000E1E2E
		public Keys KeyData
		{
			get
			{
				return this._keyData;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000E3C36 File Offset: 0x000E1E36
		public Keys Modifiers
		{
			get
			{
				return this._keyData & Keys.Modifiers;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000E3C44 File Offset: 0x000E1E44
		public bool Shift
		{
			get
			{
				return (this._keyData & Keys.Shift) == Keys.Shift;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000E3C59 File Offset: 0x000E1E59
		// (set) Token: 0x06003312 RID: 13074 RVA: 0x000E3C61 File Offset: 0x000E1E61
		public bool IsInputKey
		{
			get
			{
				return this._isInputKey;
			}
			set
			{
				this._isInputKey = value;
			}
		}

		// Token: 0x04001EC1 RID: 7873
		private readonly Keys _keyData;

		// Token: 0x04001EC2 RID: 7874
		private bool _isInputKey;
	}
}
