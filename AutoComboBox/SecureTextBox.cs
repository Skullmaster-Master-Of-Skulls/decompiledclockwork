using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000087 RID: 135
	public class SecureTextBox : TextBox
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x0002D046 File Offset: 0x0002C046
		public SecureTextBox()
		{
			this.InitializeComponent();
			base.PasswordChar = '*';
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0002D088 File Offset: 0x0002C088
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0002D0A0 File Offset: 0x0002C0A0
		public SecureString SecureText
		{
			get
			{
				return this._secureEntry;
			}
			set
			{
				this._secureEntry = value;
				this.Text = new string(base.PasswordChar, this._secureEntry.Length);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0002D0C8 File Offset: 0x0002C0C8
		public char[] CharacterData
		{
			get
			{
				char[] array = new char[this._secureEntry.Length];
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					intPtr = Marshal.SecureStringToBSTR(this._secureEntry);
					array = new char[this._secureEntry.Length];
					Marshal.Copy(intPtr, array, 0, this._secureEntry.Length);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.ZeroFreeBSTR(intPtr);
					}
				}
				return array;
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002D154 File Offset: 0x0002C154
		protected override bool ProcessKeyMessage(ref Message m)
		{
			bool result;
			if (this._displayChar)
			{
				result = base.ProcessKeyMessage(ref m);
			}
			else
			{
				this._displayChar = true;
				result = true;
			}
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002D188 File Offset: 0x0002C188
		protected override bool IsInputChar(char charCode)
		{
			int num = base.SelectionStart;
			bool flag = base.IsInputChar(charCode);
			if (flag)
			{
				if (!char.IsControl(charCode) && !char.IsHighSurrogate(charCode) && !char.IsLowSurrogate(charCode))
				{
					if (this.SelectionLength > 0)
					{
						for (int i = 0; i < this.SelectionLength; i++)
						{
							this._secureEntry.RemoveAt(base.SelectionStart);
						}
					}
					if (num == this._secureEntry.Length)
					{
						this._secureEntry.AppendChar(charCode);
					}
					else
					{
						this._secureEntry.InsertAt(num, charCode);
					}
					this.Text = new string(base.PasswordChar, this._secureEntry.Length);
					this._displayChar = false;
					num++;
					base.SelectionStart = num;
				}
				else
				{
					switch (charCode)
					{
					case '\b':
						if (this.SelectionLength == 0 && num > 0)
						{
							num--;
							this._secureEntry.RemoveAt(num);
							this.Text = new string('*', this._secureEntry.Length);
							base.SelectionStart = num;
						}
						else if (this.SelectionLength > 0)
						{
							for (int i = 0; i < this.SelectionLength; i++)
							{
								this._secureEntry.RemoveAt(base.SelectionStart);
							}
						}
						this._displayChar = false;
						break;
					case '\t':
						base.TopLevelControl.SelectNextControl(this, true, true, true, true);
						this._displayChar = false;
						break;
					default:
						if (charCode != '\r')
						{
							if (charCode == '\u001b')
							{
								IButtonControl buttonControl = base.FindForm().CancelButton;
								if (buttonControl != null)
								{
									buttonControl.PerformClick();
								}
								this._displayChar = false;
							}
						}
						else
						{
							IButtonControl buttonControl = base.FindForm().AcceptButton;
							if (buttonControl != null)
							{
								buttonControl.PerformClick();
							}
							this._displayChar = false;
						}
						break;
					}
				}
			}
			else
			{
				this._displayChar = true;
			}
			return flag;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002D3C4 File Offset: 0x0002C3C4
		protected override bool IsInputKey(Keys keyData)
		{
			bool result = true;
			bool flag = (keyData & Keys.Delete) == Keys.Delete;
			if (flag)
			{
				if (this.SelectionLength == this._secureEntry.Length)
				{
					this._secureEntry.Clear();
				}
				else if (this.SelectionLength > 0)
				{
					for (int i = 0; i < this.SelectionLength; i++)
					{
						this._secureEntry.RemoveAt(base.SelectionStart);
					}
				}
				else if ((keyData & Keys.Delete) == Keys.Delete && base.SelectionStart < this.Text.Length)
				{
					this._secureEntry.RemoveAt(base.SelectionStart);
				}
			}
			return result;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002D498 File Offset: 0x0002C498
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002D4CF File Offset: 0x0002C4CF
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x0400047E RID: 1150
		private bool _displayChar = false;

		// Token: 0x0400047F RID: 1151
		private SecureString _secureEntry = new SecureString();

		// Token: 0x04000480 RID: 1152
		private TextBox _innerTextBox = new TextBox();

		// Token: 0x04000481 RID: 1153
		private IContainer components = null;
	}
}
