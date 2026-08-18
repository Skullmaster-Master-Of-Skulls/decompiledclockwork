using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf.fonts.cmaps
{
	// Token: 0x02000593 RID: 1427
	public class CMapParser
	{
		// Token: 0x060030D4 RID: 12500 RVA: 0x0012D30C File Offset: 0x0012C30C
		public CMap Parse(Stream input)
		{
			PushbackStream pis = new PushbackStream(input);
			CMap cmap = new CMap();
			object obj = null;
			object obj2;
			while ((obj2 = this.ParseNextToken(pis)) != null)
			{
				if (obj2 is CMapParser.Operator)
				{
					CMapParser.Operator @operator = (CMapParser.Operator)obj2;
					if (@operator.op.Equals("begincodespacerange"))
					{
						IConvertible convertible = (IConvertible)obj;
						for (int i = 0; i < convertible.ToInt32(CultureInfo.InvariantCulture); i++)
						{
							byte[] start = (byte[])this.ParseNextToken(pis);
							byte[] end = (byte[])this.ParseNextToken(pis);
							CodespaceRange codespaceRange = new CodespaceRange();
							codespaceRange.SetStart(start);
							codespaceRange.SetEnd(end);
							cmap.AddCodespaceRange(codespaceRange);
						}
					}
					else if (@operator.op.Equals("beginbfchar"))
					{
						IConvertible convertible2 = (IConvertible)obj;
						for (int j = 0; j < convertible2.ToInt32(CultureInfo.InvariantCulture); j++)
						{
							byte[] src = (byte[])this.ParseNextToken(pis);
							object obj3 = this.ParseNextToken(pis);
							if (obj3 is byte[])
							{
								byte[] bytes = (byte[])obj3;
								string dest = this.CreateStringFromBytes(bytes);
								cmap.AddMapping(src, dest);
							}
							else
							{
								if (!(obj3 is CMapParser.LiteralName))
								{
									throw new IOException(MessageLocalization.GetComposedMessage("error.parsing.cmap.beginbfchar.expected.cosstring.or.cosname.and.not.1", obj3));
								}
								cmap.AddMapping(src, ((CMapParser.LiteralName)obj3).name);
							}
						}
					}
					else if (@operator.op.Equals("beginbfrange"))
					{
						IConvertible convertible3 = (IConvertible)obj;
						for (int k = 0; k < convertible3.ToInt32(CultureInfo.InvariantCulture); k++)
						{
							byte[] array = (byte[])this.ParseNextToken(pis);
							byte[] second = (byte[])this.ParseNextToken(pis);
							object obj4 = this.ParseNextToken(pis);
							IList<byte[]> list = null;
							byte[] array2;
							if (obj4 is IList<byte[]>)
							{
								list = (IList<byte[]>)obj4;
								array2 = list[0];
							}
							else
							{
								array2 = (byte[])obj4;
							}
							int num = 0;
							bool flag = false;
							while (!flag)
							{
								if (this.Compare(array, second) >= 0)
								{
									flag = true;
								}
								string dest2 = this.CreateStringFromBytes(array2);
								cmap.AddMapping(array, dest2);
								this.Increment(array);
								if (list == null)
								{
									this.Increment(array2);
								}
								else
								{
									num++;
									if (num < list.Count)
									{
										array2 = list[num];
									}
								}
							}
						}
					}
				}
				obj = obj2;
			}
			return cmap;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x0012D574 File Offset: 0x0012C574
		private object ParseNextToken(PushbackStream pis)
		{
			object result = null;
			int num = pis.ReadByte();
			while (num == 9 || num == 32 || num == 13 || num == 10)
			{
				num = pis.ReadByte();
			}
			int num2 = num;
			if (num2 != -1)
			{
				switch (num2)
				{
				case 37:
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append((char)num);
					this.ReadUntilEndOfLine(pis, stringBuilder);
					return stringBuilder.ToString();
				}
				case 38:
				case 39:
				case 41:
				case 42:
				case 43:
				case 44:
				case 45:
				case 46:
				case 58:
				case 59:
				case 61:
					break;
				case 40:
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					int num3 = pis.ReadByte();
					while (num3 != -1 && num3 != 41)
					{
						stringBuilder2.Append((char)num3);
						num3 = pis.ReadByte();
					}
					return stringBuilder2.ToString();
				}
				case 47:
				{
					StringBuilder stringBuilder3 = new StringBuilder();
					int num4 = pis.ReadByte();
					while (!this.IsWhitespaceOrEOF(num4))
					{
						stringBuilder3.Append((char)num4);
						num4 = pis.ReadByte();
					}
					return new CMapParser.LiteralName(stringBuilder3.ToString());
				}
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				{
					StringBuilder stringBuilder4 = new StringBuilder();
					stringBuilder4.Append((char)num);
					num = pis.ReadByte();
					while (!this.IsWhitespaceOrEOF(num) && (char.IsDigit((char)num) || num == 46))
					{
						stringBuilder4.Append((char)num);
						num = pis.ReadByte();
					}
					pis.Unread(num);
					string text = stringBuilder4.ToString();
					if (text.IndexOf('.') >= 0)
					{
						return double.Parse(text, CultureInfo.InvariantCulture);
					}
					return int.Parse(text, CultureInfo.InvariantCulture);
				}
				case 60:
				{
					int num5 = pis.ReadByte();
					if (num5 == 60)
					{
						IDictionary<string, object> dictionary = new Dictionary<string, object>();
						object obj = this.ParseNextToken(pis);
						while (obj is CMapParser.LiteralName && !">>".Equals(obj))
						{
							object value = this.ParseNextToken(pis);
							dictionary[((CMapParser.LiteralName)obj).name] = value;
							obj = this.ParseNextToken(pis);
						}
						return dictionary;
					}
					int num6 = 16;
					int num7 = -1;
					while (num5 != -1 && num5 != 62)
					{
						int num8;
						if (num5 >= 48 && num5 <= 57)
						{
							num8 = num5 - 48;
						}
						else if (num5 >= 65 && num5 <= 70)
						{
							num8 = 10 + num5 - 65;
						}
						else
						{
							if (num5 < 97 || num5 > 102)
							{
								throw new IOException(MessageLocalization.GetComposedMessage("error.expected.hex.character.and.not.char.thenextbyte.1", num5));
							}
							num8 = 10 + num5 - 97;
						}
						num8 *= num6;
						if (num6 == 16)
						{
							num7++;
							this.tokenParserByteBuffer[num7] = 0;
							num6 = 1;
						}
						else
						{
							num6 = 16;
						}
						byte[] array = this.tokenParserByteBuffer;
						int num9 = num7;
						array[num9] += (byte)num8;
						num5 = pis.ReadByte();
					}
					byte[] array2 = new byte[num7 + 1];
					Array.Copy(this.tokenParserByteBuffer, 0, array2, 0, num7 + 1);
					return array2;
				}
				case 62:
				{
					int num10 = pis.ReadByte();
					if (num10 == 62)
					{
						return ">>";
					}
					throw new IOException(MessageLocalization.GetComposedMessage("error.expected.the.end.of.a.dictionary"));
				}
				default:
					switch (num2)
					{
					case 91:
					{
						IList<object> list = new List<object>();
						object obj2 = this.ParseNextToken(pis);
						while (!"]".Equals(obj2))
						{
							list.Add(obj2);
							obj2 = this.ParseNextToken(pis);
						}
						return list;
					}
					case 93:
						return "]";
					}
					break;
				}
				StringBuilder stringBuilder5 = new StringBuilder();
				stringBuilder5.Append((char)num);
				num = pis.ReadByte();
				while (!this.IsWhitespaceOrEOF(num))
				{
					stringBuilder5.Append((char)num);
					num = pis.ReadByte();
				}
				result = new CMapParser.Operator(stringBuilder5.ToString());
			}
			return result;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x0012D964 File Offset: 0x0012C964
		private void ReadUntilEndOfLine(Stream pis, StringBuilder buf)
		{
			int num = pis.ReadByte();
			while (num != -1 && num != 13 && num != 10)
			{
				buf.Append((char)num);
				num = pis.ReadByte();
			}
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x0012D998 File Offset: 0x0012C998
		private bool IsWhitespaceOrEOF(int aByte)
		{
			return aByte == -1 || aByte == 32 || aByte == 13 || aByte == 10;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0012D9AF File Offset: 0x0012C9AF
		private void Increment(byte[] data)
		{
			this.Increment(data, data.Length - 1);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0012D9BD File Offset: 0x0012C9BD
		private void Increment(byte[] data, int position)
		{
			if (position > 0 && ((int)data[position] + 256) % 256 == 255)
			{
				data[position] = 0;
				this.Increment(data, position - 1);
				return;
			}
			data[position] += 1;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0012D9F4 File Offset: 0x0012C9F4
		private string CreateStringFromBytes(byte[] bytes)
		{
			string result;
			if (bytes.Length == 1)
			{
				result = Convert.ToString((char)bytes[0]);
			}
			else
			{
				result = Encoding.BigEndianUnicode.GetString(bytes);
			}
			return result;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x0012DA24 File Offset: 0x0012CA24
		private int Compare(byte[] first, byte[] second)
		{
			int result = 1;
			bool flag = false;
			int num = 0;
			while (num < first.Length && !flag)
			{
				if (first[num] != second[num])
				{
					if (((int)first[num] + 256) % 256 < ((int)second[num] + 256) % 256)
					{
						flag = true;
						result = -1;
					}
					else
					{
						flag = true;
						result = 1;
					}
				}
				num++;
			}
			return result;
		}

		// Token: 0x0400217A RID: 8570
		private const string BEGIN_CODESPACE_RANGE = "begincodespacerange";

		// Token: 0x0400217B RID: 8571
		private const string BEGIN_BASE_FONT_CHAR = "beginbfchar";

		// Token: 0x0400217C RID: 8572
		private const string BEGIN_BASE_FONT_RANGE = "beginbfrange";

		// Token: 0x0400217D RID: 8573
		private const string MARK_END_OF_DICTIONARY = ">>";

		// Token: 0x0400217E RID: 8574
		private const string MARK_END_OF_ARRAY = "]";

		// Token: 0x0400217F RID: 8575
		private byte[] tokenParserByteBuffer = new byte[512];

		// Token: 0x02000594 RID: 1428
		private class LiteralName
		{
			// Token: 0x060030DC RID: 12508 RVA: 0x0012DA79 File Offset: 0x0012CA79
			public LiteralName(string theName)
			{
				this.name = theName;
			}

			// Token: 0x04002180 RID: 8576
			public string name;
		}

		// Token: 0x02000595 RID: 1429
		private class Operator
		{
			// Token: 0x060030DD RID: 12509 RVA: 0x0012DA88 File Offset: 0x0012CA88
			public Operator(string theOp)
			{
				this.op = theOp;
			}

			// Token: 0x04002181 RID: 8577
			public string op;
		}
	}
}
