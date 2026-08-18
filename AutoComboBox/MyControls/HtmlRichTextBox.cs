using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000016 RID: 22
	public class HtmlRichTextBox : RichTextBox
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000062B8 File Offset: 0x000052B8
		public void BeginUpdate()
		{
			this.updating++;
			if (this.updating <= 1)
			{
				this.oldEventMask = HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1073, 0, 0);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 11, 0, 0);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000631C File Offset: 0x0000531C
		public void EndUpdate()
		{
			this.updating--;
			if (this.updating <= 0)
			{
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 11, 1, 0);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1073, 0, this.oldEventMask);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006380 File Offset: 0x00005380
		public bool InternalUpdating
		{
			get
			{
				return this.updating != 0;
			}
		}

		// Token: 0x06000091 RID: 145
		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

		// Token: 0x06000092 RID: 146
		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref HtmlRichTextBox.PARAFORMAT lp);

		// Token: 0x06000093 RID: 147
		[DllImport("user32", CharSet = CharSet.Auto)]
		private static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref HtmlRichTextBox.CHARFORMAT lp);

		// Token: 0x06000094 RID: 148 RVA: 0x000063A0 File Offset: 0x000053A0
		public void SetSuperScript(bool bSet)
		{
			HtmlRichTextBox.CHARFORMAT charFormat = this.CharFormat;
			if (bSet)
			{
				charFormat.dwMask |= 196608U;
				charFormat.dwEffects |= 131072U;
			}
			else
			{
				charFormat.dwEffects &= 4294836223U;
			}
			this.CharFormat = charFormat;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006404 File Offset: 0x00005404
		public void SetSubScript(bool bSet)
		{
			HtmlRichTextBox.CHARFORMAT charFormat = this.CharFormat;
			if (bSet)
			{
				charFormat.dwMask |= 196608U;
				charFormat.dwEffects |= 65536U;
			}
			else
			{
				charFormat.dwEffects &= 4294901759U;
			}
			this.CharFormat = charFormat;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006468 File Offset: 0x00005468
		public bool IsSuperScript()
		{
			return (this.CharFormat.dwEffects & 131072U) == 131072U;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006498 File Offset: 0x00005498
		public bool IsSubScript()
		{
			return (this.CharFormat.dwEffects & 65536U) == 65536U;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000064C8 File Offset: 0x000054C8
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00006510 File Offset: 0x00005510
		public HtmlRichTextBox.PARAFORMAT ParaFormat
		{
			get
			{
				HtmlRichTextBox.PARAFORMAT paraformat = default(HtmlRichTextBox.PARAFORMAT);
				paraformat.cbSize = Marshal.SizeOf(paraformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1085, 1, ref paraformat);
				return paraformat;
			}
			set
			{
				HtmlRichTextBox.PARAFORMAT paraformat = value;
				paraformat.cbSize = Marshal.SizeOf(paraformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1095, 1, ref paraformat);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000654C File Offset: 0x0000554C
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00006594 File Offset: 0x00005594
		public HtmlRichTextBox.PARAFORMAT DefaultParaFormat
		{
			get
			{
				HtmlRichTextBox.PARAFORMAT paraformat = default(HtmlRichTextBox.PARAFORMAT);
				paraformat.cbSize = Marshal.SizeOf(paraformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1085, 4, ref paraformat);
				return paraformat;
			}
			set
			{
				HtmlRichTextBox.PARAFORMAT paraformat = value;
				paraformat.cbSize = Marshal.SizeOf(paraformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1095, 4, ref paraformat);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000065D0 File Offset: 0x000055D0
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00006618 File Offset: 0x00005618
		public HtmlRichTextBox.CHARFORMAT CharFormat
		{
			get
			{
				HtmlRichTextBox.CHARFORMAT charformat = default(HtmlRichTextBox.CHARFORMAT);
				charformat.cbSize = Marshal.SizeOf(charformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1082, 1, ref charformat);
				return charformat;
			}
			set
			{
				HtmlRichTextBox.CHARFORMAT charformat = value;
				charformat.cbSize = Marshal.SizeOf(charformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1092, 1, ref charformat);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00006654 File Offset: 0x00005654
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000669C File Offset: 0x0000569C
		public HtmlRichTextBox.CHARFORMAT DefaultCharFormat
		{
			get
			{
				HtmlRichTextBox.CHARFORMAT charformat = default(HtmlRichTextBox.CHARFORMAT);
				charformat.cbSize = Marshal.SizeOf(charformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1082, 4, ref charformat);
				return charformat;
			}
			set
			{
				HtmlRichTextBox.CHARFORMAT charformat = value;
				charformat.cbSize = Marshal.SizeOf(charformat);
				HtmlRichTextBox.SendMessage(new HandleRef(this, base.Handle), 1092, 4, ref charformat);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000066D8 File Offset: 0x000056D8
		private Color GetColor(int crColor)
		{
			byte red = (byte)crColor;
			byte green = (byte)(crColor >> 8);
			byte blue = (byte)(crColor >> 16);
			return Color.FromArgb((int)red, (int)green, (int)blue);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006700 File Offset: 0x00005700
		private int GetCOLORREF(int r, int g, int b)
		{
			int num = g << 8;
			int num2 = b << 16;
			return r | num | num2;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006728 File Offset: 0x00005728
		private int GetCOLORREF(Color color)
		{
			int r = (int)color.R;
			int g = (int)color.G;
			int b = (int)color.B;
			return this.GetCOLORREF(r, g, b);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000676C File Offset: 0x0000576C
		public string GetHTML(bool bHTML, bool bParaFormat)
		{
			HtmlRichTextBox.ctformatStates ctformatStates = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates2 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates3 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates4 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates5 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates6 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates7 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates8 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates9 = HtmlRichTextBox.ctformatStates.nctNone;
			HtmlRichTextBox.ctformatStates ctformatStates10 = HtmlRichTextBox.ctformatStates.nctNone;
			string text = "";
			int num = 0;
			Color color = default(Color);
			int num2 = 0;
			ArrayList arrayList = new ArrayList();
			string text2 = "";
			base.HideSelection = true;
			this.BeginUpdate();
			int selectionStart = base.SelectionStart;
			int selectionLength = this.SelectionLength;
			try
			{
				int i;
				if (bHTML)
				{
					char[] array = new char[]
					{
						'&',
						'<',
						'>',
						'"',
						'\''
					};
					string[] array2 = new string[]
					{
						"&amp;",
						"&lt;",
						"&gt;",
						"&quot;",
						"&apos;"
					};
					for (i = 0; i < array.Length; i++)
					{
						char[] characterSet = new char[]
						{
							array[i]
						};
						for (int num3 = base.Find(characterSet, 0); num3 != -1; num3 = base.Find(characterSet, num3 + 1))
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = num3,
								nLen = 1,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_ENTITY,
								strValue = array2[i]
							});
						}
					}
				}
				string text3 = "";
				int num4 = this.TextLength;
				char[] array3 = new char[2];
				array3[0] = ' ';
				char[] trimChars = array3;
				for (i = 0; i < num4; i++)
				{
					base.Select(i, 1);
					string selectedText = this.SelectedText;
					if (bHTML)
					{
						HtmlRichTextBox.CHARFORMAT charFormat = this.CharFormat;
						HtmlRichTextBox.PARAFORMAT paraFormat = this.ParaFormat;
						string text4 = new string(charFormat.szFaceName);
						text4 = text4.Trim(trimChars);
						if (text != text4 || num != charFormat.crTextColor || num2 != charFormat.yHeight)
						{
							if (text != "")
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "</font>"
								});
							}
							text = text4;
							num = charFormat.crTextColor;
							num2 = charFormat.yHeight;
							int num5 = num2 / 100;
							color = this.GetColor(num);
							HtmlRichTextBox.cMyREFormat cMyREFormat = default(HtmlRichTextBox.cMyREFormat);
							string text5 = "#" + (color.ToArgb() & 16777215).ToString("X6");
							cMyREFormat.nPos = i;
							cMyREFormat.nLen = 0;
							cMyREFormat.nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG;
							cMyREFormat.strValue = string.Concat(new object[]
							{
								"<font face=\"",
								text,
								"\" color=\"",
								text5,
								"\" size=\"",
								num5,
								"\">"
							});
							arrayList.Add(cMyREFormat);
						}
						if (selectedText == "\r" || selectedText == "\n")
						{
							if (bParaFormat)
							{
								ctformatStates10 = HtmlRichTextBox.ctformatStates.nctNone;
								ctformatStates8 = HtmlRichTextBox.ctformatStates.nctNone;
								ctformatStates9 = HtmlRichTextBox.ctformatStates.nctNone;
								ctformatStates7 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (ctformatStates2 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "</i>"
								});
								ctformatStates2 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (ctformatStates != HtmlRichTextBox.ctformatStates.nctNone)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "</b>"
								});
								ctformatStates = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (ctformatStates4 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "</u>"
								});
								ctformatStates4 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (ctformatStates3 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "</s>"
								});
								ctformatStates3 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (ctformatStates5 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "</sup>"
								});
								ctformatStates5 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (ctformatStates6 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "</sub>"
								});
								ctformatStates6 = HtmlRichTextBox.ctformatStates.nctNone;
							}
						}
						if (bParaFormat)
						{
							if (paraFormat.wAlignment == 3)
							{
								if (ctformatStates7 == HtmlRichTextBox.ctformatStates.nctNone)
								{
									ctformatStates7 = HtmlRichTextBox.ctformatStates.nctNew;
								}
								else
								{
									ctformatStates7 = HtmlRichTextBox.ctformatStates.nctContinue;
								}
							}
							else if (ctformatStates7 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates7 = HtmlRichTextBox.ctformatStates.nctReset;
							}
							if (ctformatStates7 == HtmlRichTextBox.ctformatStates.nctNew)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "<p align=\"center\">"
								});
							}
							else if (ctformatStates7 == HtmlRichTextBox.ctformatStates.nctReset)
							{
								ctformatStates7 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (paraFormat.wAlignment == 1)
							{
								if (ctformatStates8 == HtmlRichTextBox.ctformatStates.nctNone)
								{
									ctformatStates8 = HtmlRichTextBox.ctformatStates.nctNew;
								}
								else
								{
									ctformatStates8 = HtmlRichTextBox.ctformatStates.nctContinue;
								}
							}
							else if (ctformatStates8 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates8 = HtmlRichTextBox.ctformatStates.nctReset;
							}
							if (ctformatStates8 == HtmlRichTextBox.ctformatStates.nctNew)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "<p align=\"left\">"
								});
							}
							else if (ctformatStates8 == HtmlRichTextBox.ctformatStates.nctReset)
							{
								ctformatStates8 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (paraFormat.wAlignment == 2)
							{
								if (ctformatStates9 == HtmlRichTextBox.ctformatStates.nctNone)
								{
									ctformatStates9 = HtmlRichTextBox.ctformatStates.nctNew;
								}
								else
								{
									ctformatStates9 = HtmlRichTextBox.ctformatStates.nctContinue;
								}
							}
							else if (ctformatStates9 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates9 = HtmlRichTextBox.ctformatStates.nctReset;
							}
							if (ctformatStates9 == HtmlRichTextBox.ctformatStates.nctNew)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "<p align=\"right\">"
								});
							}
							else if (ctformatStates9 == HtmlRichTextBox.ctformatStates.nctReset)
							{
								ctformatStates9 = HtmlRichTextBox.ctformatStates.nctNone;
							}
							if (paraFormat.wNumbering == 1)
							{
								if (ctformatStates10 == HtmlRichTextBox.ctformatStates.nctNone)
								{
									ctformatStates10 = HtmlRichTextBox.ctformatStates.nctNew;
								}
								else
								{
									ctformatStates10 = HtmlRichTextBox.ctformatStates.nctContinue;
								}
							}
							else if (ctformatStates10 != HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates10 = HtmlRichTextBox.ctformatStates.nctReset;
							}
							if (ctformatStates10 == HtmlRichTextBox.ctformatStates.nctNew)
							{
								arrayList.Add(new HtmlRichTextBox.cMyREFormat
								{
									nPos = i,
									nLen = 0,
									nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
									strValue = "<li>"
								});
							}
							else if (ctformatStates10 == HtmlRichTextBox.ctformatStates.nctReset)
							{
								ctformatStates10 = HtmlRichTextBox.ctformatStates.nctNone;
							}
						}
						if ((charFormat.dwEffects & 1U) == 1U)
						{
							if (ctformatStates == HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates = HtmlRichTextBox.ctformatStates.nctNew;
							}
							else
							{
								ctformatStates = HtmlRichTextBox.ctformatStates.nctContinue;
							}
						}
						else if (ctformatStates != HtmlRichTextBox.ctformatStates.nctNone)
						{
							ctformatStates = HtmlRichTextBox.ctformatStates.nctReset;
						}
						if (ctformatStates == HtmlRichTextBox.ctformatStates.nctNew)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "<b>"
							});
						}
						else if (ctformatStates == HtmlRichTextBox.ctformatStates.nctReset)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "</b>"
							});
							ctformatStates = HtmlRichTextBox.ctformatStates.nctNone;
						}
						if ((charFormat.dwEffects & 2U) == 2U)
						{
							if (ctformatStates2 == HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates2 = HtmlRichTextBox.ctformatStates.nctNew;
							}
							else
							{
								ctformatStates2 = HtmlRichTextBox.ctformatStates.nctContinue;
							}
						}
						else if (ctformatStates2 != HtmlRichTextBox.ctformatStates.nctNone)
						{
							ctformatStates2 = HtmlRichTextBox.ctformatStates.nctReset;
						}
						if (ctformatStates2 == HtmlRichTextBox.ctformatStates.nctNew)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "<i>"
							});
						}
						else if (ctformatStates2 == HtmlRichTextBox.ctformatStates.nctReset)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "</i>"
							});
							ctformatStates2 = HtmlRichTextBox.ctformatStates.nctNone;
						}
						if ((charFormat.dwEffects & 8U) == 8U)
						{
							if (ctformatStates3 == HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates3 = HtmlRichTextBox.ctformatStates.nctNew;
							}
							else
							{
								ctformatStates3 = HtmlRichTextBox.ctformatStates.nctContinue;
							}
						}
						else if (ctformatStates3 != HtmlRichTextBox.ctformatStates.nctNone)
						{
							ctformatStates3 = HtmlRichTextBox.ctformatStates.nctReset;
						}
						if (ctformatStates3 == HtmlRichTextBox.ctformatStates.nctNew)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "<s>"
							});
						}
						else if (ctformatStates3 == HtmlRichTextBox.ctformatStates.nctReset)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "</s>"
							});
							ctformatStates3 = HtmlRichTextBox.ctformatStates.nctNone;
						}
						if ((charFormat.dwEffects & 4U) == 4U)
						{
							if (ctformatStates4 == HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates4 = HtmlRichTextBox.ctformatStates.nctNew;
							}
							else
							{
								ctformatStates4 = HtmlRichTextBox.ctformatStates.nctContinue;
							}
						}
						else if (ctformatStates4 != HtmlRichTextBox.ctformatStates.nctNone)
						{
							ctformatStates4 = HtmlRichTextBox.ctformatStates.nctReset;
						}
						if (ctformatStates4 == HtmlRichTextBox.ctformatStates.nctNew)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "<u>"
							});
						}
						else if (ctformatStates4 == HtmlRichTextBox.ctformatStates.nctReset)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "</u>"
							});
							ctformatStates4 = HtmlRichTextBox.ctformatStates.nctNone;
						}
						if ((charFormat.dwEffects & 131072U) == 131072U)
						{
							if (ctformatStates5 == HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates5 = HtmlRichTextBox.ctformatStates.nctNew;
							}
							else
							{
								ctformatStates5 = HtmlRichTextBox.ctformatStates.nctContinue;
							}
						}
						else if (ctformatStates5 != HtmlRichTextBox.ctformatStates.nctNone)
						{
							ctformatStates5 = HtmlRichTextBox.ctformatStates.nctReset;
						}
						if (ctformatStates5 == HtmlRichTextBox.ctformatStates.nctNew)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "<sup>"
							});
						}
						else if (ctformatStates5 == HtmlRichTextBox.ctformatStates.nctReset)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "</sup>"
							});
							ctformatStates5 = HtmlRichTextBox.ctformatStates.nctNone;
						}
						if ((charFormat.dwEffects & 65536U) == 65536U)
						{
							if (ctformatStates6 == HtmlRichTextBox.ctformatStates.nctNone)
							{
								ctformatStates6 = HtmlRichTextBox.ctformatStates.nctNew;
							}
							else
							{
								ctformatStates6 = HtmlRichTextBox.ctformatStates.nctContinue;
							}
						}
						else if (ctformatStates6 != HtmlRichTextBox.ctformatStates.nctNone)
						{
							ctformatStates6 = HtmlRichTextBox.ctformatStates.nctReset;
						}
						if (ctformatStates6 == HtmlRichTextBox.ctformatStates.nctNew)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "<sub>"
							});
						}
						else if (ctformatStates6 == HtmlRichTextBox.ctformatStates.nctReset)
						{
							arrayList.Add(new HtmlRichTextBox.cMyREFormat
							{
								nPos = i,
								nLen = 0,
								nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
								strValue = "</sub>"
							});
							ctformatStates6 = HtmlRichTextBox.ctformatStates.nctNone;
						}
					}
					text3 += selectedText;
				}
				if (bHTML)
				{
					if (ctformatStates != HtmlRichTextBox.ctformatStates.nctNone)
					{
						arrayList.Add(new HtmlRichTextBox.cMyREFormat
						{
							nPos = i,
							nLen = 0,
							nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
							strValue = "</b>"
						});
					}
					if (ctformatStates2 != HtmlRichTextBox.ctformatStates.nctNone)
					{
						arrayList.Add(new HtmlRichTextBox.cMyREFormat
						{
							nPos = i,
							nLen = 0,
							nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
							strValue = "</i>"
						});
					}
					if (ctformatStates3 != HtmlRichTextBox.ctformatStates.nctNone)
					{
						arrayList.Add(new HtmlRichTextBox.cMyREFormat
						{
							nPos = i,
							nLen = 0,
							nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
							strValue = "</s>"
						});
					}
					if (ctformatStates4 != HtmlRichTextBox.ctformatStates.nctNone)
					{
						arrayList.Add(new HtmlRichTextBox.cMyREFormat
						{
							nPos = i,
							nLen = 0,
							nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
							strValue = "</u>"
						});
					}
					if (ctformatStates5 != HtmlRichTextBox.ctformatStates.nctNone)
					{
						arrayList.Add(new HtmlRichTextBox.cMyREFormat
						{
							nPos = i,
							nLen = 0,
							nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
							strValue = "</sup>"
						});
					}
					if (ctformatStates6 != HtmlRichTextBox.ctformatStates.nctNone)
					{
						arrayList.Add(new HtmlRichTextBox.cMyREFormat
						{
							nPos = i,
							nLen = 0,
							nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
							strValue = "</sub>"
						});
					}
					if (text != "")
					{
						arrayList.Add(new HtmlRichTextBox.cMyREFormat
						{
							nPos = i,
							nLen = 0,
							nType = HtmlRichTextBox.uMyREType.U_MYRE_TYPE_TAG,
							strValue = "</font>"
						});
					}
				}
				num4 = arrayList.Count;
				for (i = 0; i < num4 - 1; i++)
				{
					for (int j = i + 1; j < num4; j++)
					{
						HtmlRichTextBox.cMyREFormat cMyREFormat = (HtmlRichTextBox.cMyREFormat)arrayList[i];
						HtmlRichTextBox.cMyREFormat cMyREFormat2 = (HtmlRichTextBox.cMyREFormat)arrayList[j];
						if (cMyREFormat2.nPos < cMyREFormat.nPos)
						{
							arrayList.RemoveAt(j);
							arrayList.Insert(i, cMyREFormat2);
							j--;
						}
						else if (cMyREFormat2.nPos == cMyREFormat.nPos && cMyREFormat2.nLen < cMyREFormat.nLen)
						{
							arrayList.RemoveAt(j);
							arrayList.Insert(i, cMyREFormat2);
							j--;
						}
					}
				}
				int num6 = 0;
				for (i = 0; i < num4; i++)
				{
					HtmlRichTextBox.cMyREFormat cMyREFormat = (HtmlRichTextBox.cMyREFormat)arrayList[i];
					text2 = text2 + text3.Substring(num6, cMyREFormat.nPos - num6) + cMyREFormat.strValue;
					num6 = cMyREFormat.nPos + cMyREFormat.nLen;
				}
				if (num6 < text3.Length)
				{
					text2 += text3.Substring(num6);
				}
			}
			catch
			{
			}
			finally
			{
				base.SelectionStart = selectionStart;
				this.SelectionLength = selectionLength;
				this.EndUpdate();
				base.HideSelection = false;
			}
			return text2;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007868 File Offset: 0x00006868
		public void AddHTML(string strHTML)
		{
			HtmlRichTextBox.CHARFORMAT defaultCharFormat = this.DefaultCharFormat;
			HtmlRichTextBox.PARAFORMAT defaultParaFormat = this.DefaultParaFormat;
			char[] array = new char[2];
			array[0] = ' ';
			char[] trimChars = array;
			base.HideSelection = true;
			this.BeginUpdate();
			try
			{
				while (strHTML.Length > 0)
				{
					string text = strHTML;
					int num;
					do
					{
						num = strHTML.IndexOf('<');
						if (num < 0)
						{
							goto IL_798;
						}
						if (num > 0)
						{
							goto Block_5;
						}
						int num2 = strHTML.IndexOf('>', num);
						if (num2 <= num)
						{
							goto IL_78B;
						}
						if (num2 - num <= 0)
						{
							goto IL_77F;
						}
						string text2 = strHTML.Substring(num, num2 - num + 1);
						text2 = text2.ToLower();
						if (text2 == "<b>")
						{
							defaultCharFormat.dwMask |= 4194305U;
							defaultCharFormat.dwEffects |= 1U;
							defaultCharFormat.wWeight = 700;
						}
						else if (text2 == "<i>")
						{
							defaultCharFormat.dwMask |= 2U;
							defaultCharFormat.dwEffects |= 2U;
						}
						else if (text2 == "<u>")
						{
							defaultCharFormat.dwMask |= 8388612U;
							defaultCharFormat.dwEffects |= 4U;
							defaultCharFormat.bUnderlineType = 1;
						}
						else if (text2 == "<s>")
						{
							defaultCharFormat.dwMask |= 8U;
							defaultCharFormat.dwEffects |= 8U;
						}
						else if (text2 == "<sup>")
						{
							defaultCharFormat.dwMask |= 196608U;
							defaultCharFormat.dwEffects |= 131072U;
						}
						else if (text2 == "<sub>")
						{
							defaultCharFormat.dwMask |= 196608U;
							defaultCharFormat.dwEffects |= 65536U;
						}
						else if (text2.Length > 2 && text2.Substring(0, 2) == "<p")
						{
							if (text2.IndexOf("align=\"left\"") > 0)
							{
								defaultParaFormat.dwMask |= 8U;
								defaultParaFormat.wAlignment = 1;
							}
							else if (text2.IndexOf("align=\"right\"") > 0)
							{
								defaultParaFormat.dwMask |= 8U;
								defaultParaFormat.wAlignment = 2;
							}
							else if (text2.IndexOf("align=\"center\"") > 0)
							{
								defaultParaFormat.dwMask |= 8U;
								defaultParaFormat.wAlignment = 3;
							}
						}
						else if (text2.Length > 5 && text2.Substring(0, 5) == "<font")
						{
							string text3 = new string(defaultCharFormat.szFaceName);
							text3 = text3.Trim(trimChars);
							int crTextColor = defaultCharFormat.crTextColor;
							int num3 = defaultCharFormat.yHeight;
							int num4 = text2.IndexOf("face=");
							if (num4 > 0)
							{
								int num5 = text2.IndexOf('"', num4 + 6);
								if (num5 > num4)
								{
									text3 = text2.Substring(num4 + 6, num5 - num4 - 6);
								}
							}
							int num6 = text2.IndexOf("size=");
							if (num6 > 0)
							{
								int num7 = text2.IndexOf('"', num6 + 6);
								if (num7 > num6)
								{
									num3 = int.Parse(text2.Substring(num6 + 6, num7 - num6 - 6));
									num3 *= 100;
								}
							}
							int num8 = text2.IndexOf("color=");
							if (num8 > 0)
							{
								int num9 = text2.IndexOf('"', num8 + 7);
								if (num9 > num8)
								{
									if (text2.Substring(num8 + 7, 1) == "#")
									{
										string value = text2.Substring(num8 + 8, num9 - num8 - 8);
										int argb = Convert.ToInt32(value, 16);
										Color color = Color.FromArgb(argb);
										crTextColor = this.GetCOLORREF(color);
									}
									else
									{
										crTextColor = int.Parse(text2.Substring(num8 + 7, num9 - num8 - 7));
									}
								}
							}
							defaultCharFormat.szFaceName = new char[32];
							text3.CopyTo(0, defaultCharFormat.szFaceName, 0, Math.Min(31, text3.Length));
							defaultCharFormat.crTextColor = crTextColor;
							defaultCharFormat.yHeight = num3;
							defaultCharFormat.dwMask |= 3758096384U;
							defaultCharFormat.dwEffects &= 3221225471U;
						}
						else if (text2 == "<li>")
						{
							if (defaultParaFormat.wNumbering != 1)
							{
								defaultParaFormat.dwMask |= 32U;
								defaultParaFormat.wNumbering = 1;
							}
						}
						else if (text2 == "</b>")
						{
							defaultCharFormat.dwEffects &= 4294967294U;
							defaultCharFormat.wWeight = 400;
						}
						else if (text2 == "</i>")
						{
							defaultCharFormat.dwEffects &= 4294967293U;
						}
						else if (text2 == "</u>")
						{
							defaultCharFormat.dwEffects &= 4294967291U;
						}
						else if (text2 == "</s>")
						{
							defaultCharFormat.dwEffects &= 4294967287U;
						}
						else if (text2 == "</sup>")
						{
							defaultCharFormat.dwEffects &= 4294836223U;
						}
						else if (text2 == "</sub>")
						{
							defaultCharFormat.dwEffects &= 4294901759U;
						}
						else if (!(text2 == "</font>"))
						{
							if (!(text2 == "</p>"))
							{
								if (text2 == "</li>")
								{
								}
							}
						}
						int num10 = strHTML.IndexOf("<", num2 + 1);
						if (num10 > 0)
						{
							text = strHTML.Substring(num2 + 1, num10 - num2 - 1);
							strHTML = strHTML.Substring(num10);
						}
						else
						{
							if (num2 + 1 < strHTML.Length)
							{
								text = strHTML.Substring(num2 + 1);
							}
							else
							{
								text = "";
							}
							strHTML = "";
						}
						if (text.Length <= 0)
						{
							break;
						}
					}
					while (text[0] == '<');
					IL_7A1:
					if (text.Length > 0)
					{
						text = text.Replace("&amp;", "&");
						text = text.Replace("&lt;", "<");
						text = text.Replace("&gt;", ">");
						text = text.Replace("&apos;", "'");
						text = text.Replace("&quot;", "\"");
						string text4 = text;
						while (text4.Length > 0)
						{
							int length = text4.Length;
							int selectionStart = base.SelectionStart;
							string text5 = text4.Substring(0, length);
							this.SelectedText = text5;
							text4 = text4.Remove(0, length);
							base.SelectionStart = selectionStart;
							this.SelectionLength = text5.Length;
							this.ParaFormat = defaultParaFormat;
							this.CharFormat = defaultCharFormat;
							base.SelectionStart = this.TextLength + 1;
							this.SelectionLength = 0;
						}
						base.SelectionStart = this.TextLength + 1;
						this.SelectionLength = 0;
						if (text.IndexOf("\r\n", 0) >= 0 || text.IndexOf("\n", 0) >= 0)
						{
							defaultParaFormat.dwMask = 40U;
							defaultParaFormat.wAlignment = 1;
							defaultParaFormat.wNumbering = 0;
						}
					}
					continue;
					IL_795:
					goto IL_7A1;
					Block_5:
					text = strHTML.Substring(0, num);
					strHTML = strHTML.Substring(num);
					goto IL_795;
					IL_798:
					strHTML = "";
					IL_794:
					goto IL_795;
					IL_78B:
					strHTML = "";
					IL_788:
					goto IL_794;
					IL_77F:
					strHTML = "";
					IL_77C:
					goto IL_788;
					goto IL_77C;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				base.SelectionStart = this.TextLength + 1;
				this.SelectionLength = 0;
				this.EndUpdate();
				base.HideSelection = false;
			}
		}

		// Token: 0x0400008C RID: 140
		private const int EM_FORMATRANGE = 1081;

		// Token: 0x0400008D RID: 141
		private const int WM_USER = 1024;

		// Token: 0x0400008E RID: 142
		private const int EM_GETCHARFORMAT = 1082;

		// Token: 0x0400008F RID: 143
		private const int EM_SETCHARFORMAT = 1092;

		// Token: 0x04000090 RID: 144
		private const int EM_SETEVENTMASK = 1073;

		// Token: 0x04000091 RID: 145
		private const int EM_GETPARAFORMAT = 1085;

		// Token: 0x04000092 RID: 146
		private const int EM_SETPARAFORMAT = 1095;

		// Token: 0x04000093 RID: 147
		private const int EM_SETTYPOGRAPHYOPTIONS = 1226;

		// Token: 0x04000094 RID: 148
		private const int WM_SETREDRAW = 11;

		// Token: 0x04000095 RID: 149
		private const int TO_ADVANCEDTYPOGRAPHY = 1;

		// Token: 0x04000096 RID: 150
		private const int SCF_SELECTION = 1;

		// Token: 0x04000097 RID: 151
		private const int SCF_WORD = 2;

		// Token: 0x04000098 RID: 152
		private const int SCF_ALL = 4;

		// Token: 0x04000099 RID: 153
		public const int LF_FACESIZE = 32;

		// Token: 0x0400009A RID: 154
		public const uint CFM_BOLD = 1U;

		// Token: 0x0400009B RID: 155
		public const uint CFM_ITALIC = 2U;

		// Token: 0x0400009C RID: 156
		public const uint CFM_UNDERLINE = 4U;

		// Token: 0x0400009D RID: 157
		public const uint CFM_STRIKEOUT = 8U;

		// Token: 0x0400009E RID: 158
		public const uint CFM_PROTECTED = 16U;

		// Token: 0x0400009F RID: 159
		public const uint CFM_LINK = 32U;

		// Token: 0x040000A0 RID: 160
		public const uint CFM_SIZE = 2147483648U;

		// Token: 0x040000A1 RID: 161
		public const uint CFM_COLOR = 1073741824U;

		// Token: 0x040000A2 RID: 162
		public const uint CFM_FACE = 536870912U;

		// Token: 0x040000A3 RID: 163
		public const uint CFM_OFFSET = 268435456U;

		// Token: 0x040000A4 RID: 164
		public const uint CFM_CHARSET = 134217728U;

		// Token: 0x040000A5 RID: 165
		public const uint CFM_SUBSCRIPT = 196608U;

		// Token: 0x040000A6 RID: 166
		public const uint CFM_SUPERSCRIPT = 196608U;

		// Token: 0x040000A7 RID: 167
		public const uint CFE_BOLD = 1U;

		// Token: 0x040000A8 RID: 168
		public const uint CFE_ITALIC = 2U;

		// Token: 0x040000A9 RID: 169
		public const uint CFE_UNDERLINE = 4U;

		// Token: 0x040000AA RID: 170
		public const uint CFE_STRIKEOUT = 8U;

		// Token: 0x040000AB RID: 171
		public const uint CFE_PROTECTED = 16U;

		// Token: 0x040000AC RID: 172
		public const uint CFE_LINK = 32U;

		// Token: 0x040000AD RID: 173
		public const uint CFE_AUTOCOLOR = 1073741824U;

		// Token: 0x040000AE RID: 174
		public const uint CFE_SUBSCRIPT = 65536U;

		// Token: 0x040000AF RID: 175
		public const uint CFE_SUPERSCRIPT = 131072U;

		// Token: 0x040000B0 RID: 176
		public const byte CFU_UNDERLINENONE = 0;

		// Token: 0x040000B1 RID: 177
		public const byte CFU_UNDERLINE = 1;

		// Token: 0x040000B2 RID: 178
		public const byte CFU_UNDERLINEWORD = 2;

		// Token: 0x040000B3 RID: 179
		public const byte CFU_UNDERLINEDOUBLE = 3;

		// Token: 0x040000B4 RID: 180
		public const byte CFU_UNDERLINEDOTTED = 4;

		// Token: 0x040000B5 RID: 181
		public const byte CFU_UNDERLINEDASH = 5;

		// Token: 0x040000B6 RID: 182
		public const byte CFU_UNDERLINEDASHDOT = 6;

		// Token: 0x040000B7 RID: 183
		public const byte CFU_UNDERLINEDASHDOTDOT = 7;

		// Token: 0x040000B8 RID: 184
		public const byte CFU_UNDERLINEWAVE = 8;

		// Token: 0x040000B9 RID: 185
		public const byte CFU_UNDERLINETHICK = 9;

		// Token: 0x040000BA RID: 186
		public const byte CFU_UNDERLINEHAIRLINE = 10;

		// Token: 0x040000BB RID: 187
		public const int CFM_SMALLCAPS = 64;

		// Token: 0x040000BC RID: 188
		public const int CFM_ALLCAPS = 128;

		// Token: 0x040000BD RID: 189
		public const int CFM_HIDDEN = 256;

		// Token: 0x040000BE RID: 190
		public const int CFM_OUTLINE = 512;

		// Token: 0x040000BF RID: 191
		public const int CFM_SHADOW = 1024;

		// Token: 0x040000C0 RID: 192
		public const int CFM_EMBOSS = 2048;

		// Token: 0x040000C1 RID: 193
		public const int CFM_IMPRINT = 4096;

		// Token: 0x040000C2 RID: 194
		public const int CFM_DISABLED = 8192;

		// Token: 0x040000C3 RID: 195
		public const int CFM_REVISED = 16384;

		// Token: 0x040000C4 RID: 196
		public const int CFM_BACKCOLOR = 67108864;

		// Token: 0x040000C5 RID: 197
		public const int CFM_LCID = 33554432;

		// Token: 0x040000C6 RID: 198
		public const int CFM_UNDERLINETYPE = 8388608;

		// Token: 0x040000C7 RID: 199
		public const int CFM_WEIGHT = 4194304;

		// Token: 0x040000C8 RID: 200
		public const int CFM_SPACING = 2097152;

		// Token: 0x040000C9 RID: 201
		public const int CFM_KERNING = 1048576;

		// Token: 0x040000CA RID: 202
		public const int CFM_STYLE = 524288;

		// Token: 0x040000CB RID: 203
		public const int CFM_ANIMATION = 262144;

		// Token: 0x040000CC RID: 204
		public const int CFM_REVAUTHOR = 32768;

		// Token: 0x040000CD RID: 205
		public const short FW_DONTCARE = 0;

		// Token: 0x040000CE RID: 206
		public const short FW_THIN = 100;

		// Token: 0x040000CF RID: 207
		public const short FW_EXTRALIGHT = 200;

		// Token: 0x040000D0 RID: 208
		public const short FW_LIGHT = 300;

		// Token: 0x040000D1 RID: 209
		public const short FW_NORMAL = 400;

		// Token: 0x040000D2 RID: 210
		public const short FW_MEDIUM = 500;

		// Token: 0x040000D3 RID: 211
		public const short FW_SEMIBOLD = 600;

		// Token: 0x040000D4 RID: 212
		public const short FW_BOLD = 700;

		// Token: 0x040000D5 RID: 213
		public const short FW_EXTRABOLD = 800;

		// Token: 0x040000D6 RID: 214
		public const short FW_HEAVY = 900;

		// Token: 0x040000D7 RID: 215
		public const short FW_ULTRALIGHT = 200;

		// Token: 0x040000D8 RID: 216
		public const short FW_REGULAR = 400;

		// Token: 0x040000D9 RID: 217
		public const short FW_DEMIBOLD = 600;

		// Token: 0x040000DA RID: 218
		public const short FW_ULTRABOLD = 800;

		// Token: 0x040000DB RID: 219
		public const short FW_BLACK = 900;

		// Token: 0x040000DC RID: 220
		public const uint PFM_STARTINDENT = 1U;

		// Token: 0x040000DD RID: 221
		public const uint PFM_RIGHTINDENT = 2U;

		// Token: 0x040000DE RID: 222
		public const uint PFM_OFFSET = 4U;

		// Token: 0x040000DF RID: 223
		public const uint PFM_ALIGNMENT = 8U;

		// Token: 0x040000E0 RID: 224
		public const uint PFM_TABSTOPS = 16U;

		// Token: 0x040000E1 RID: 225
		public const uint PFM_NUMBERING = 32U;

		// Token: 0x040000E2 RID: 226
		public const uint PFM_OFFSETINDENT = 2147483648U;

		// Token: 0x040000E3 RID: 227
		public const ushort PFN_BULLET = 1;

		// Token: 0x040000E4 RID: 228
		public const ushort PFA_LEFT = 1;

		// Token: 0x040000E5 RID: 229
		public const ushort PFA_RIGHT = 2;

		// Token: 0x040000E6 RID: 230
		public const ushort PFA_CENTER = 3;

		// Token: 0x040000E7 RID: 231
		private int updating = 0;

		// Token: 0x040000E8 RID: 232
		private int oldEventMask = 0;

		// Token: 0x02000017 RID: 23
		public struct PARAFORMAT
		{
			// Token: 0x040000E9 RID: 233
			public int cbSize;

			// Token: 0x040000EA RID: 234
			public uint dwMask;

			// Token: 0x040000EB RID: 235
			public short wNumbering;

			// Token: 0x040000EC RID: 236
			public short wReserved;

			// Token: 0x040000ED RID: 237
			public int dxStartIndent;

			// Token: 0x040000EE RID: 238
			public int dxRightIndent;

			// Token: 0x040000EF RID: 239
			public int dxOffset;

			// Token: 0x040000F0 RID: 240
			public short wAlignment;

			// Token: 0x040000F1 RID: 241
			public short cTabCount;

			// Token: 0x040000F2 RID: 242
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public int[] rgxTabs;

			// Token: 0x040000F3 RID: 243
			public int dySpaceBefore;

			// Token: 0x040000F4 RID: 244
			public int dySpaceAfter;

			// Token: 0x040000F5 RID: 245
			public int dyLineSpacing;

			// Token: 0x040000F6 RID: 246
			public short sStyle;

			// Token: 0x040000F7 RID: 247
			public byte bLineSpacingRule;

			// Token: 0x040000F8 RID: 248
			public byte bOutlineLevel;

			// Token: 0x040000F9 RID: 249
			public short wShadingWeight;

			// Token: 0x040000FA RID: 250
			public short wShadingStyle;

			// Token: 0x040000FB RID: 251
			public short wNumberingStart;

			// Token: 0x040000FC RID: 252
			public short wNumberingStyle;

			// Token: 0x040000FD RID: 253
			public short wNumberingTab;

			// Token: 0x040000FE RID: 254
			public short wBorderSpace;

			// Token: 0x040000FF RID: 255
			public short wBorderWidth;

			// Token: 0x04000100 RID: 256
			public short wBorders;
		}

		// Token: 0x02000018 RID: 24
		public struct CHARFORMAT
		{
			// Token: 0x04000101 RID: 257
			public int cbSize;

			// Token: 0x04000102 RID: 258
			public uint dwMask;

			// Token: 0x04000103 RID: 259
			public uint dwEffects;

			// Token: 0x04000104 RID: 260
			public int yHeight;

			// Token: 0x04000105 RID: 261
			public int yOffset;

			// Token: 0x04000106 RID: 262
			public int crTextColor;

			// Token: 0x04000107 RID: 263
			public byte bCharSet;

			// Token: 0x04000108 RID: 264
			public byte bPitchAndFamily;

			// Token: 0x04000109 RID: 265
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public char[] szFaceName;

			// Token: 0x0400010A RID: 266
			public short wWeight;

			// Token: 0x0400010B RID: 267
			public short sSpacing;

			// Token: 0x0400010C RID: 268
			public int crBackColor;

			// Token: 0x0400010D RID: 269
			public uint lcid;

			// Token: 0x0400010E RID: 270
			public uint dwReserved;

			// Token: 0x0400010F RID: 271
			public short sStyle;

			// Token: 0x04000110 RID: 272
			public short wKerning;

			// Token: 0x04000111 RID: 273
			public byte bUnderlineType;

			// Token: 0x04000112 RID: 274
			public byte bAnimation;

			// Token: 0x04000113 RID: 275
			public byte bRevAuthor;

			// Token: 0x04000114 RID: 276
			public byte bReserved1;
		}

		// Token: 0x02000019 RID: 25
		private enum ctformatStates
		{
			// Token: 0x04000116 RID: 278
			nctNone,
			// Token: 0x04000117 RID: 279
			nctNew,
			// Token: 0x04000118 RID: 280
			nctContinue,
			// Token: 0x04000119 RID: 281
			nctReset
		}

		// Token: 0x0200001A RID: 26
		private enum uMyREType
		{
			// Token: 0x0400011B RID: 283
			U_MYRE_TYPE_TAG,
			// Token: 0x0400011C RID: 284
			U_MYRE_TYPE_EMO,
			// Token: 0x0400011D RID: 285
			U_MYRE_TYPE_ENTITY
		}

		// Token: 0x0200001B RID: 27
		private struct cMyREFormat
		{
			// Token: 0x0400011E RID: 286
			public HtmlRichTextBox.uMyREType nType;

			// Token: 0x0400011F RID: 287
			public int nLen;

			// Token: 0x04000120 RID: 288
			public int nPos;

			// Token: 0x04000121 RID: 289
			public string strValue;
		}
	}
}
