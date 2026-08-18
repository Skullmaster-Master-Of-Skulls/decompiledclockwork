using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox
{
	// Token: 0x020000C7 RID: 199
	public partial class InputBox : Form
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x0003C374 File Offset: 0x0003B374
		public InputBox(string title, string message, string defaultText, bool EnterKeySaves)
		{
			this.InitializeComponent();
			this.enterKeySaves = EnterKeySaves;
			this.Text = title;
			this.label1.Text = message;
			this.textBox1.Text = defaultText;
			this.cancelled = true;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0003C3C4 File Offset: 0x0003B3C4
		public InputBox(string title, string message, string defaultText, int multiLineTextBoxNumLines, bool showEncryptCheckbox, bool EnterKeySaves)
		{
			this.InitializeComponent();
			this.enterKeySaves = EnterKeySaves;
			this.Text = title;
			this.label1.Text = message;
			this.textBox1.Text = defaultText;
			this.cancelled = true;
			if (multiLineTextBoxNumLines > 0)
			{
				this.textBox1.Multiline = true;
				int num = this.textBox1.Height * multiLineTextBoxNumLines;
				base.Height += num;
			}
			if (showEncryptCheckbox)
			{
				this.btn_split.Visible = true;
				this.btn_encrypt.Visible = true;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0003C470 File Offset: 0x0003B470
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0003C487 File Offset: 0x0003B487
		public eStartupCursorLocation StartupCursorLocation { get; set; }

		// Token: 0x06000794 RID: 1940 RVA: 0x0003C490 File Offset: 0x0003B490
		public static string GetUserInputPassword(IWin32Window owner, string title, string message, string defaultText)
		{
			InputBox inputBox = new InputBox(title, message, defaultText, true);
			inputBox.textBox1.PasswordChar = '*';
			inputBox.textBox1.Multiline = false;
			inputBox.ShowDialog(owner);
			string result;
			if (inputBox.cancelled)
			{
				result = null;
			}
			else
			{
				result = inputBox.GetInput();
			}
			return result;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0003C4E8 File Offset: 0x0003B4E8
		public static string[] GetUserInputPassword2(IWin32Window owner, string title, string message, string defaultText)
		{
			InputBox inputBox = new InputBox(title, message, defaultText, true);
			inputBox.Size = new Size(inputBox.Width, inputBox.label2.Height + inputBox.textBox2.Height + 10);
			inputBox.textBox1.Dock = DockStyle.Top;
			inputBox.textBox2.Visible = true;
			inputBox.label2.Visible = true;
			inputBox.textBox2.SendToBack();
			inputBox.label2.SendToBack();
			inputBox.textBox1.SendToBack();
			inputBox.label1.SendToBack();
			inputBox.label1.Text = "Please enter the password:";
			inputBox.label2.Text = "Please enter the password again:";
			inputBox.textBox1.PasswordChar = '*';
			inputBox.textBox2.PasswordChar = '*';
			inputBox.textBox2.Multiline = false;
			inputBox.textBox1.Multiline = false;
			inputBox.ShowDialog(owner);
			string[] result;
			if (inputBox.cancelled)
			{
				result = null;
			}
			else
			{
				result = inputBox.GetInput2();
			}
			return result;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0003C600 File Offset: 0x0003B600
		public static string GetUserInput(IWin32Window owner, string title, string message, string defaultText)
		{
			return InputBox.GetUserInput(owner, title, message, defaultText, true, eStartupCursorLocation.UseDefault);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003C620 File Offset: 0x0003B620
		public static string GetUserInput(IWin32Window owner, string title, string message, string defaultText, bool enterKeySaves = true)
		{
			return InputBox.GetUserInput(owner, title, message, defaultText, enterKeySaves, eStartupCursorLocation.UseDefault);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0003C640 File Offset: 0x0003B640
		public static string GetUserInput(IWin32Window owner, string title, string message, string defaultText, eStartupCursorLocation startupCursorLocation = eStartupCursorLocation.UseDefault)
		{
			return InputBox.GetUserInput(owner, title, message, defaultText, true, startupCursorLocation);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0003C660 File Offset: 0x0003B660
		public static string GetUserInput(IWin32Window owner, string title, string message, string defaultText, bool enterKeySaves, eStartupCursorLocation startupCursorLocation)
		{
			InputBox inputBox = new InputBox(title, message, defaultText, enterKeySaves);
			inputBox.ShowDialog(owner);
			string result;
			if (inputBox.cancelled)
			{
				result = null;
			}
			else
			{
				result = inputBox.GetInput();
			}
			return result;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0003C69C File Offset: 0x0003B69C
		public static string GetUserInput(IWin32Window owner, string title, string message, string defaultText, int height)
		{
			return InputBox.GetUserInput(owner, title, message, defaultText, height, true);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0003C6BC File Offset: 0x0003B6BC
		public static string GetUserInput(IWin32Window owner, string title, string message, string defaultText, int height, bool enterKeySaves)
		{
			InputBox inputBox = new InputBox(title, message, defaultText, enterKeySaves);
			inputBox.SetHeight(height);
			inputBox.ShowDialog(owner);
			string result;
			if (inputBox.cancelled)
			{
				result = null;
			}
			else
			{
				result = inputBox.GetInput();
			}
			return result;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003C704 File Offset: 0x0003B704
		public string GetInput()
		{
			string result;
			if (this.textBox1.PasswordChar != ' ')
			{
				result = this.textBox1.Text;
			}
			else
			{
				result = this.textBox1.Text.Trim();
			}
			return result;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0003C748 File Offset: 0x0003B748
		public string[] GetInput2()
		{
			return new string[]
			{
				this.textBox1.Text,
				this.textBox2.Text
			};
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003D08F File Offset: 0x0003C08F
		private void OK()
		{
			base.DialogResult = DialogResult.OK;
			this.cancelled = false;
			base.Close();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0003D0A8 File Offset: 0x0003C0A8
		private void InputBox_KeyUp(object sender, KeyEventArgs e)
		{
			bool flag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
			if (e.KeyCode == Keys.Return && this.enterKeySaves && !flag)
			{
				if (base.ActiveControl is TextBox)
				{
					TextBox textBox = (TextBox)base.ActiveControl;
					textBox.Text = textBox.Text.Replace(Environment.NewLine, "");
				}
				this.OK();
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003D12B File Offset: 0x0003C12B
		private void btn_cancelFake_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0003D138 File Offset: 0x0003C138
		public bool EncryptedChecked()
		{
			return this.btn_encrypt.Checked;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003D158 File Offset: 0x0003C158
		public string TextEntered()
		{
			return this.textBox1.Text.Trim();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0003D17C File Offset: 0x0003C17C
		private void InputBox_Load(object sender, EventArgs e)
		{
			if (this.label1.Text.Length > 0)
			{
				Graphics graphics = this.label1.CreateGraphics();
				SizeF sizeF = graphics.MeasureString(this.label1.Text, this.label1.Font, this.label1.Width);
				this.label1.Height = Convert.ToInt32(sizeF.Height);
			}
			else
			{
				this.label1.Visible = false;
			}
			int num = this.label1.Height + this.textBox1.Height + this.panel2.Height + 48;
			if (num > 0 && num != base.Height)
			{
				base.Height = num;
			}
			base.ActiveControl = this.textBox1;
			base.Activate();
			if (this.textBox1.Text.Length > 0)
			{
				switch (this.StartupCursorLocation)
				{
				case eStartupCursorLocation.Top:
					this.textBox1.SelectionLength = 0;
					this.textBox1.SelectionStart = 0;
					break;
				case eStartupCursorLocation.Bottom:
					this.textBox1.SelectionLength = 0;
					this.textBox1.SelectionStart = this.textBox1.Text.Length - 1;
					break;
				}
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003D2D8 File Offset: 0x0003C2D8
		public void SetHeight(int newHeight)
		{
			if (newHeight > 0)
			{
				base.Height = newHeight;
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0003D2F9 File Offset: 0x0003C2F9
		public void EnableColourButton()
		{
			this.btn_colour.Visible = true;
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0003D30C File Offset: 0x0003C30C
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0003D329 File Offset: 0x0003C329
		public Color ColourChosen
		{
			get
			{
				return this.btn_colour.BackColor;
			}
			set
			{
				this.btn_colour.BackColor = value;
			}
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003D33C File Offset: 0x0003C33C
		private void btn_colour_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			DialogResult dialogResult = colorDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				this.btn_colour.BackColor = colorDialog.Color;
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0003D375 File Offset: 0x0003C375
		private void btn_ok_Click(object sender, EventArgs e)
		{
			this.OK();
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0003D37F File Offset: 0x0003C37F
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x040005CA RID: 1482
		private bool cancelled;

		// Token: 0x040005D4 RID: 1492
		private bool enterKeySaves;
	}
}
