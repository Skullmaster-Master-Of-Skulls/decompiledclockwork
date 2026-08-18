using System;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.exceptions;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002CA RID: 714
	public class PRTokeniser
	{
		// Token: 0x06001AB5 RID: 6837 RVA: 0x0009D4F8 File Offset: 0x0009C4F8
		public PRTokeniser(string filename)
		{
			this.file = new RandomAccessFileOrArray(filename);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0009D50C File Offset: 0x0009C50C
		public PRTokeniser(byte[] pdfIn)
		{
			this.file = new RandomAccessFileOrArray(pdfIn);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0009D520 File Offset: 0x0009C520
		public PRTokeniser(RandomAccessFileOrArray file)
		{
			this.file = file;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0009D52F File Offset: 0x0009C52F
		public void Seek(int pos)
		{
			this.file.Seek(pos);
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0009D53D File Offset: 0x0009C53D
		public int FilePointer
		{
			get
			{
				return this.file.FilePointer;
			}
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0009D54A File Offset: 0x0009C54A
		public void Close()
		{
			this.file.Close();
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x0009D557 File Offset: 0x0009C557
		public int Length
		{
			get
			{
				return this.file.Length;
			}
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0009D564 File Offset: 0x0009C564
		public int Read()
		{
			return this.file.Read();
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0009D571 File Offset: 0x0009C571
		public RandomAccessFileOrArray SafeFile
		{
			get
			{
				return new RandomAccessFileOrArray(this.file);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0009D57E File Offset: 0x0009C57E
		public RandomAccessFileOrArray File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0009D588 File Offset: 0x0009C588
		public string ReadString(int size)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (size-- > 0)
			{
				int num = this.file.Read();
				if (num == -1)
				{
					break;
				}
				stringBuilder.Append((char)num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0009D5C5 File Offset: 0x0009C5C5
		public static bool IsWhitespace(int ch)
		{
			return ch == 0 || ch == 9 || ch == 10 || ch == 12 || ch == 13 || ch == 32;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0009D5E5 File Offset: 0x0009C5E5
		public static bool IsDelimiter(int ch)
		{
			return ch == 40 || ch == 41 || ch == 60 || ch == 62 || ch == 91 || ch == 93 || ch == 47 || ch == 37;
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0009D611 File Offset: 0x0009C611
		public PRTokeniser.TokType TokenType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0009D619 File Offset: 0x0009C619
		public string StringValue
		{
			get
			{
				return this.stringValue;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0009D621 File Offset: 0x0009C621
		public int Reference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0009D629 File Offset: 0x0009C629
		public int Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0009D631 File Offset: 0x0009C631
		public void BackOnePosition(int ch)
		{
			if (ch != -1)
			{
				this.file.PushBack((byte)ch);
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0009D644 File Offset: 0x0009C644
		public void ThrowError(string error)
		{
			throw new InvalidPdfException(MessageLocalization.GetComposedMessage("1.at.file.pointer.2", error, this.file.FilePointer));
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0009D668 File Offset: 0x0009C668
		public char CheckPdfHeader()
		{
			this.file.StartOffset = 0;
			string text = this.ReadString(1024);
			int num = text.IndexOf("%PDF-");
			if (num < 0)
			{
				throw new InvalidPdfException(MessageLocalization.GetComposedMessage("pdf.header.not.found"));
			}
			this.file.StartOffset = num;
			return text[num + 7];
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0009D6C4 File Offset: 0x0009C6C4
		public void CheckFdfHeader()
		{
			this.file.StartOffset = 0;
			string text = this.ReadString(1024);
			int num = text.IndexOf("%FDF-1.2");
			if (num < 0)
			{
				throw new InvalidPdfException(MessageLocalization.GetComposedMessage("fdf.header.not.found"));
			}
			this.file.StartOffset = num;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0009D718 File Offset: 0x0009C718
		public int Startxref
		{
			get
			{
				int num = Math.Min(1024, this.file.Length);
				int num2 = this.file.Length - num;
				this.file.Seek(num2);
				string text = this.ReadString(1024);
				int num3 = text.LastIndexOf("startxref");
				if (num3 < 0)
				{
					throw new InvalidPdfException(MessageLocalization.GetComposedMessage("pdf.startxref.not.found"));
				}
				return num2 + num3;
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0009D784 File Offset: 0x0009C784
		public static int GetHex(int v)
		{
			if (v >= 48 && v <= 57)
			{
				return v - 48;
			}
			if (v >= 65 && v <= 70)
			{
				return v - 65 + 10;
			}
			if (v >= 97 && v <= 102)
			{
				return v - 97 + 10;
			}
			return -1;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0009D7BC File Offset: 0x0009C7BC
		public void NextValidToken()
		{
			int num = 0;
			string s = null;
			string s2 = null;
			int pos = 0;
			while (this.NextToken())
			{
				if (this.type != PRTokeniser.TokType.COMMENT)
				{
					switch (num)
					{
					case 0:
						if (this.type != PRTokeniser.TokType.NUMBER)
						{
							return;
						}
						pos = this.file.FilePointer;
						s = this.stringValue;
						num++;
						break;
					case 1:
						if (this.type != PRTokeniser.TokType.NUMBER)
						{
							this.file.Seek(pos);
							this.type = PRTokeniser.TokType.NUMBER;
							this.stringValue = s;
							return;
						}
						s2 = this.stringValue;
						num++;
						break;
					default:
						if (this.type != PRTokeniser.TokType.OTHER || !this.stringValue.Equals("R"))
						{
							this.file.Seek(pos);
							this.type = PRTokeniser.TokType.NUMBER;
							this.stringValue = s;
							return;
						}
						this.type = PRTokeniser.TokType.REF;
						this.reference = int.Parse(s);
						this.generation = int.Parse(s2);
						return;
					}
				}
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0009D8B0 File Offset: 0x0009C8B0
		public bool NextToken()
		{
			int num;
			do
			{
				num = this.file.Read();
			}
			while (num != -1 && PRTokeniser.IsWhitespace(num));
			if (num == -1)
			{
				this.type = PRTokeniser.TokType.ENDOFFILE;
				return false;
			}
			StringBuilder stringBuilder = null;
			this.stringValue = "";
			int num2 = num;
			if (num2 <= 40)
			{
				if (num2 == 37)
				{
					this.type = PRTokeniser.TokType.COMMENT;
					do
					{
						num = this.file.Read();
						if (num == -1 || num == 13)
						{
							break;
						}
					}
					while (num != 10);
					goto IL_4BB;
				}
				if (num2 == 40)
				{
					stringBuilder = new StringBuilder();
					this.type = PRTokeniser.TokType.STRING;
					this.hexString = false;
					int num3 = 0;
					for (;;)
					{
						num = this.file.Read();
						if (num == -1)
						{
							break;
						}
						if (num == 40)
						{
							num3++;
						}
						else if (num == 41)
						{
							num3--;
						}
						else if (num == 92)
						{
							bool flag = false;
							num = this.file.Read();
							int num4 = num;
							if (num4 <= 92)
							{
								if (num4 <= 13)
								{
									if (num4 != 10)
									{
										if (num4 != 13)
										{
											goto IL_360;
										}
										flag = true;
										num = this.file.Read();
										if (num != 10)
										{
											this.BackOnePosition(num);
										}
									}
									else
									{
										flag = true;
									}
								}
								else
								{
									switch (num4)
									{
									case 40:
									case 41:
										break;
									default:
										if (num4 != 92)
										{
											goto IL_360;
										}
										break;
									}
								}
							}
							else if (num4 <= 102)
							{
								if (num4 != 98)
								{
									if (num4 != 102)
									{
										goto IL_360;
									}
									num = 12;
								}
								else
								{
									num = 8;
								}
							}
							else if (num4 != 110)
							{
								switch (num4)
								{
								case 114:
									num = 13;
									break;
								case 115:
									goto IL_360;
								case 116:
									num = 9;
									break;
								default:
									goto IL_360;
								}
							}
							else
							{
								num = 10;
							}
							IL_3D3:
							if (flag)
							{
								continue;
							}
							if (num < 0)
							{
								break;
							}
							goto IL_404;
							IL_360:
							if (num < 48 || num > 55)
							{
								goto IL_3D3;
							}
							int num5 = num - 48;
							num = this.file.Read();
							if (num < 48 || num > 55)
							{
								this.BackOnePosition(num);
								num = num5;
								goto IL_3D3;
							}
							num5 = (num5 << 3) + num - 48;
							num = this.file.Read();
							if (num < 48 || num > 55)
							{
								this.BackOnePosition(num);
								num = num5;
								goto IL_3D3;
							}
							num5 = (num5 << 3) + num - 48;
							num = (num5 & 255);
							goto IL_3D3;
						}
						else if (num == 13)
						{
							num = this.file.Read();
							if (num < 0)
							{
								break;
							}
							if (num != 10)
							{
								this.BackOnePosition(num);
								num = 10;
							}
						}
						IL_404:
						if (num3 == -1)
						{
							break;
						}
						stringBuilder.Append((char)num);
					}
					if (num == -1)
					{
						this.ThrowError(MessageLocalization.GetComposedMessage("error.reading.string"));
						goto IL_4BB;
					}
					goto IL_4BB;
				}
			}
			else
			{
				if (num2 == 47)
				{
					stringBuilder = new StringBuilder();
					this.type = PRTokeniser.TokType.NAME;
					for (;;)
					{
						num = this.file.Read();
						if (num == -1 || PRTokeniser.IsDelimiter(num) || PRTokeniser.IsWhitespace(num))
						{
							break;
						}
						if (num == 35)
						{
							num = (PRTokeniser.GetHex(this.file.Read()) << 4) + PRTokeniser.GetHex(this.file.Read());
						}
						stringBuilder.Append((char)num);
					}
					this.BackOnePosition(num);
					goto IL_4BB;
				}
				switch (num2)
				{
				case 60:
				{
					int num6 = this.file.Read();
					if (num6 == 60)
					{
						this.type = PRTokeniser.TokType.START_DIC;
						goto IL_4BB;
					}
					stringBuilder = new StringBuilder();
					this.type = PRTokeniser.TokType.STRING;
					this.hexString = true;
					int num7 = 0;
					for (;;)
					{
						if (!PRTokeniser.IsWhitespace(num6))
						{
							if (num6 == 62)
							{
								goto IL_1F5;
							}
							num6 = PRTokeniser.GetHex(num6);
							if (num6 < 0)
							{
								goto IL_1F5;
							}
							num7 = this.file.Read();
							while (PRTokeniser.IsWhitespace(num7))
							{
								num7 = this.file.Read();
							}
							if (num7 == 62)
							{
								break;
							}
							num7 = PRTokeniser.GetHex(num7);
							if (num7 < 0)
							{
								goto IL_1F5;
							}
							num = (num6 << 4) + num7;
							stringBuilder.Append((char)num);
							num6 = this.file.Read();
						}
						else
						{
							num6 = this.file.Read();
						}
					}
					num = num6 << 4;
					stringBuilder.Append((char)num);
					IL_1F5:
					if (num6 < 0 || num7 < 0)
					{
						this.ThrowError(MessageLocalization.GetComposedMessage("error.reading.string"));
						goto IL_4BB;
					}
					goto IL_4BB;
				}
				case 61:
					break;
				case 62:
					num = this.file.Read();
					if (num != 62)
					{
						this.ThrowError(MessageLocalization.GetComposedMessage("greaterthan.not.expected"));
					}
					this.type = PRTokeniser.TokType.END_DIC;
					goto IL_4BB;
				default:
					switch (num2)
					{
					case 91:
						this.type = PRTokeniser.TokType.START_ARRAY;
						goto IL_4BB;
					case 93:
						this.type = PRTokeniser.TokType.END_ARRAY;
						goto IL_4BB;
					}
					break;
				}
			}
			stringBuilder = new StringBuilder();
			if (num == 45 || num == 43 || num == 46 || (num >= 48 && num <= 57))
			{
				this.type = PRTokeniser.TokType.NUMBER;
				do
				{
					stringBuilder.Append((char)num);
					num = this.file.Read();
					if (num == -1)
					{
						break;
					}
				}
				while ((num >= 48 && num <= 57) || num == 46);
			}
			else
			{
				this.type = PRTokeniser.TokType.OTHER;
				do
				{
					stringBuilder.Append((char)num);
					num = this.file.Read();
				}
				while (num != -1 && !PRTokeniser.IsDelimiter(num) && !PRTokeniser.IsWhitespace(num));
			}
			this.BackOnePosition(num);
			IL_4BB:
			if (stringBuilder != null)
			{
				this.stringValue = stringBuilder.ToString();
			}
			return true;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x0009DD88 File Offset: 0x0009CD88
		public int IntValue
		{
			get
			{
				return int.Parse(this.stringValue);
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0009DD98 File Offset: 0x0009CD98
		public bool ReadLineSegment(byte[] input)
		{
			int num = -1;
			bool flag = false;
			int num2 = 0;
			int num3 = input.Length;
			if (num2 < num3)
			{
				while (PRTokeniser.IsWhitespace(num = this.Read()))
				{
				}
			}
			while (!flag && num2 < num3)
			{
				int num4 = num;
				if (num4 != -1 && num4 != 10)
				{
					if (num4 != 13)
					{
						input[num2++] = (byte)num;
					}
					else
					{
						flag = true;
						int filePointer = this.FilePointer;
						if (this.Read() != 10)
						{
							this.Seek(filePointer);
						}
					}
				}
				else
				{
					flag = true;
				}
				if (flag || num3 <= num2)
				{
					break;
				}
				num = this.Read();
			}
			if (num2 >= num3)
			{
				flag = false;
				while (!flag)
				{
					int num5;
					num = (num5 = this.Read());
					if (num5 != -1 && num5 != 10)
					{
						if (num5 == 13)
						{
							flag = true;
							int filePointer2 = this.FilePointer;
							if (this.Read() != 10)
							{
								this.Seek(filePointer2);
							}
						}
					}
					else
					{
						flag = true;
					}
				}
			}
			if (num == -1 && num2 == 0)
			{
				return false;
			}
			if (num2 + 2 <= num3)
			{
				input[num2++] = 32;
				input[num2] = 88;
			}
			return true;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0009DE80 File Offset: 0x0009CE80
		public static int[] CheckObjectStart(byte[] line)
		{
			try
			{
				PRTokeniser prtokeniser = new PRTokeniser(line);
				if (!prtokeniser.NextToken() || prtokeniser.TokenType != PRTokeniser.TokType.NUMBER)
				{
					return null;
				}
				int intValue = prtokeniser.IntValue;
				if (!prtokeniser.NextToken() || prtokeniser.TokenType != PRTokeniser.TokType.NUMBER)
				{
					return null;
				}
				int intValue2 = prtokeniser.IntValue;
				if (!prtokeniser.NextToken())
				{
					return null;
				}
				if (!prtokeniser.StringValue.Equals("obj"))
				{
					return null;
				}
				return new int[]
				{
					intValue,
					intValue2
				};
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0009DF20 File Offset: 0x0009CF20
		public bool IsHexString()
		{
			return this.hexString;
		}

		// Token: 0x040011C8 RID: 4552
		internal const string EMPTY = "";

		// Token: 0x040011C9 RID: 4553
		protected RandomAccessFileOrArray file;

		// Token: 0x040011CA RID: 4554
		protected PRTokeniser.TokType type;

		// Token: 0x040011CB RID: 4555
		protected string stringValue;

		// Token: 0x040011CC RID: 4556
		protected int reference;

		// Token: 0x040011CD RID: 4557
		protected int generation;

		// Token: 0x040011CE RID: 4558
		protected bool hexString;

		// Token: 0x020002CB RID: 715
		public enum TokType
		{
			// Token: 0x040011D0 RID: 4560
			NUMBER = 1,
			// Token: 0x040011D1 RID: 4561
			STRING,
			// Token: 0x040011D2 RID: 4562
			NAME,
			// Token: 0x040011D3 RID: 4563
			COMMENT,
			// Token: 0x040011D4 RID: 4564
			START_ARRAY,
			// Token: 0x040011D5 RID: 4565
			END_ARRAY,
			// Token: 0x040011D6 RID: 4566
			START_DIC,
			// Token: 0x040011D7 RID: 4567
			END_DIC,
			// Token: 0x040011D8 RID: 4568
			REF,
			// Token: 0x040011D9 RID: 4569
			OTHER,
			// Token: 0x040011DA RID: 4570
			ENDOFFILE
		}
	}
}
