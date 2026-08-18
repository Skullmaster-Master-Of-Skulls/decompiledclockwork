using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200049E RID: 1182
	public class PdfEncodings
	{
		// Token: 0x0600280D RID: 10253 RVA: 0x000F11E8 File Offset: 0x000F01E8
		static PdfEncodings()
		{
			for (int i = 128; i < 161; i++)
			{
				char c = PdfEncodings.winansiByteToChar[i];
				if (c != '�')
				{
					PdfEncodings.winansi[(int)c] = i;
				}
			}
			for (int j = 128; j < 161; j++)
			{
				char c2 = PdfEncodings.pdfEncodingByteToChar[j];
				if (c2 != '�')
				{
					PdfEncodings.pdfEncoding[(int)c2] = j;
				}
			}
			PdfEncodings.AddExtraEncoding("Wingdings", new PdfEncodings.WingdingsConversion());
			PdfEncodings.AddExtraEncoding("Symbol", new PdfEncodings.SymbolConversion(true));
			PdfEncodings.AddExtraEncoding("ZapfDingbats", new PdfEncodings.SymbolConversion(false));
			PdfEncodings.AddExtraEncoding("SymbolTT", new PdfEncodings.SymbolTTConversion());
			PdfEncodings.AddExtraEncoding("Cp437", new PdfEncodings.Cp437Conversion());
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000F133C File Offset: 0x000F033C
		public static byte[] ConvertToBytes(string text, string encoding)
		{
			if (text == null)
			{
				return new byte[0];
			}
			if (encoding == null || encoding.Length == 0)
			{
				int length = text.Length;
				byte[] array = new byte[length];
				for (int i = 0; i < length; i++)
				{
					array[i] = (byte)text[i];
				}
				return array;
			}
			IExtraEncoding extraEncoding;
			PdfEncodings.extraEncodings.TryGetValue(encoding.ToLower(CultureInfo.InvariantCulture), out extraEncoding);
			if (extraEncoding != null)
			{
				byte[] array2 = extraEncoding.CharToByte(text, encoding);
				if (array2 != null)
				{
					return array2;
				}
			}
			IntHashtable intHashtable = null;
			if (encoding.Equals("Cp1252"))
			{
				intHashtable = PdfEncodings.winansi;
			}
			else if (encoding.Equals("PDF"))
			{
				intHashtable = PdfEncodings.pdfEncoding;
			}
			if (intHashtable != null)
			{
				char[] array3 = text.ToCharArray();
				int num = array3.Length;
				int num2 = 0;
				byte[] array4 = new byte[num];
				for (int j = 0; j < num; j++)
				{
					char c = array3[j];
					int num3;
					if (c < '\u0080' || (c > '\u00a0' && c <= 'ÿ'))
					{
						num3 = (int)c;
					}
					else
					{
						num3 = intHashtable[(int)c];
					}
					if (num3 != 0)
					{
						array4[num2++] = (byte)num3;
					}
				}
				if (num2 == num)
				{
					return array4;
				}
				byte[] array5 = new byte[num2];
				Array.Copy(array4, 0, array5, 0, num2);
				return array5;
			}
			else
			{
				Encoding encodingEncoding = IanaEncodings.GetEncodingEncoding(encoding);
				byte[] preamble = encodingEncoding.GetPreamble();
				if (preamble.Length == 0)
				{
					return encodingEncoding.GetBytes(text);
				}
				byte[] bytes = encodingEncoding.GetBytes(text);
				byte[] array6 = new byte[bytes.Length + preamble.Length];
				Array.Copy(preamble, 0, array6, 0, preamble.Length);
				Array.Copy(bytes, 0, array6, preamble.Length, bytes.Length);
				return array6;
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000F14D8 File Offset: 0x000F04D8
		public static byte[] ConvertToBytes(char char1, string encoding)
		{
			if (encoding == null || encoding.Length == 0)
			{
				return new byte[]
				{
					(byte)char1
				};
			}
			IExtraEncoding extraEncoding;
			PdfEncodings.extraEncodings.TryGetValue(encoding.ToLower(CultureInfo.InvariantCulture), out extraEncoding);
			if (extraEncoding != null)
			{
				byte[] array = extraEncoding.CharToByte(char1, encoding);
				if (array != null)
				{
					return array;
				}
			}
			IntHashtable intHashtable = null;
			if (encoding.Equals("Cp1252"))
			{
				intHashtable = PdfEncodings.winansi;
			}
			else if (encoding.Equals("PDF"))
			{
				intHashtable = PdfEncodings.pdfEncoding;
			}
			if (intHashtable != null)
			{
				int num;
				if (char1 < '\u0080' || (char1 > '\u00a0' && char1 <= 'ÿ'))
				{
					num = (int)char1;
				}
				else
				{
					num = intHashtable[(int)char1];
				}
				if (num != 0)
				{
					return new byte[]
					{
						(byte)num
					};
				}
				return new byte[0];
			}
			else
			{
				Encoding encodingEncoding = IanaEncodings.GetEncodingEncoding(encoding);
				byte[] preamble = encodingEncoding.GetPreamble();
				char[] chars = new char[]
				{
					char1
				};
				if (preamble.Length == 0)
				{
					return encodingEncoding.GetBytes(chars);
				}
				byte[] bytes = encodingEncoding.GetBytes(chars);
				byte[] array2 = new byte[bytes.Length + preamble.Length];
				Array.Copy(preamble, 0, array2, 0, preamble.Length);
				Array.Copy(bytes, 0, array2, preamble.Length, bytes.Length);
				return array2;
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000F1608 File Offset: 0x000F0608
		public static string ConvertToString(byte[] bytes, string encoding)
		{
			if (bytes == null)
			{
				return "";
			}
			if (encoding == null || encoding.Length == 0)
			{
				char[] array = new char[bytes.Length];
				for (int i = 0; i < bytes.Length; i++)
				{
					array[i] = (char)(bytes[i] & byte.MaxValue);
				}
				return new string(array);
			}
			IExtraEncoding extraEncoding;
			PdfEncodings.extraEncodings.TryGetValue(encoding.ToLower(CultureInfo.InvariantCulture), out extraEncoding);
			if (extraEncoding != null)
			{
				string text = extraEncoding.ByteToChar(bytes, encoding);
				if (text != null)
				{
					return text;
				}
			}
			char[] array2 = null;
			if (encoding.Equals("Cp1252"))
			{
				array2 = PdfEncodings.winansiByteToChar;
			}
			else if (encoding.Equals("PDF"))
			{
				array2 = PdfEncodings.pdfEncodingByteToChar;
			}
			if (array2 != null)
			{
				int num = bytes.Length;
				char[] array3 = new char[num];
				for (int j = 0; j < num; j++)
				{
					array3[j] = array2[(int)(bytes[j] & byte.MaxValue)];
				}
				return new string(array3);
			}
			string text2 = encoding.ToUpper(CultureInfo.InvariantCulture);
			bool flag = false;
			bool flag2 = false;
			int num2 = 0;
			if (bytes.Length >= 2)
			{
				if (bytes[0] == 254 && bytes[1] == 255)
				{
					flag = true;
					flag2 = true;
					num2 = 2;
				}
				else if (bytes[0] == 255 && bytes[1] == 254)
				{
					flag = true;
					flag2 = false;
					num2 = 2;
				}
			}
			Encoding encoding2 = null;
			if (text2.Equals("UNICODEBIGUNMARKED") || text2.Equals("UNICODEBIG"))
			{
				encoding2 = new UnicodeEncoding(!flag || flag2, false);
			}
			if (text2.Equals("UNICODELITTLEUNMARKED") || text2.Equals("UNICODELITTLE"))
			{
				encoding2 = new UnicodeEncoding(flag && flag2, false);
			}
			if (encoding2 != null)
			{
				return encoding2.GetString(bytes, num2, bytes.Length - num2);
			}
			return IanaEncodings.GetEncodingEncoding(encoding).GetString(bytes);
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000F17C0 File Offset: 0x000F07C0
		public static bool IsPdfDocEncoding(string text)
		{
			if (text == null)
			{
				return true;
			}
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c >= '\u0080' && (c <= '\u00a0' || c > 'ÿ') && !PdfEncodings.pdfEncoding.ContainsKey((int)c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000F1818 File Offset: 0x000F0818
		public static void ClearCmap(string name)
		{
			lock (PdfEncodings.cmaps)
			{
				if (name.Length == 0)
				{
					PdfEncodings.cmaps.Clear();
				}
				else
				{
					PdfEncodings.cmaps.Remove(name);
				}
			}
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000F186C File Offset: 0x000F086C
		public static void LoadCmap(string name, byte[][] newline)
		{
			char[][] array;
			lock (PdfEncodings.cmaps)
			{
				PdfEncodings.cmaps.TryGetValue(name, out array);
			}
			if (array == null)
			{
				array = PdfEncodings.ReadCmap(name, newline);
				lock (PdfEncodings.cmaps)
				{
					PdfEncodings.cmaps[name] = array;
				}
			}
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000F18E4 File Offset: 0x000F08E4
		public static string ConvertCmap(string name, byte[] seq)
		{
			return PdfEncodings.ConvertCmap(name, seq, 0, seq.Length);
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000F18F4 File Offset: 0x000F08F4
		public static string ConvertCmap(string name, byte[] seq, int start, int length)
		{
			char[][] array;
			lock (PdfEncodings.cmaps)
			{
				PdfEncodings.cmaps.TryGetValue(name, out array);
			}
			if (array == null)
			{
				array = PdfEncodings.ReadCmap(name, null);
				lock (PdfEncodings.cmaps)
				{
					PdfEncodings.cmaps[name] = array;
				}
			}
			return PdfEncodings.DecodeSequence(seq, start, length, array);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000F1978 File Offset: 0x000F0978
		internal static string DecodeSequence(byte[] seq, int start, int length, char[][] planes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = start + length;
			int num2 = 0;
			for (int i = start; i < num; i++)
			{
				int num3 = (int)(seq[i] & byte.MaxValue);
				char[] array = planes[num2];
				int num4 = (int)array[num3];
				if ((num4 & 32768) == 0)
				{
					stringBuilder.Append((char)num4);
					num2 = 0;
				}
				else
				{
					num2 = (num4 & 32767);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000F19DC File Offset: 0x000F09DC
		internal static char[][] ReadCmap(string name, byte[][] newline)
		{
			List<char[]> list = new List<char[]>();
			list.Add(new char[256]);
			PdfEncodings.ReadCmap(name, list);
			if (newline != null)
			{
				for (int i = 0; i < newline.Length; i++)
				{
					PdfEncodings.EncodeSequence(newline[i].Length, newline[i], '翿', list);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000F1A30 File Offset: 0x000F0A30
		internal static void ReadCmap(string name, List<char[]> planes)
		{
			string key = "iTextSharp.text.pdf.fonts.cmaps." + name;
			Stream resourceStream = BaseFont.GetResourceStream(key);
			if (resourceStream == null)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("the.cmap.1.was.not.found", name));
			}
			PdfEncodings.EncodeStream(resourceStream, planes);
			resourceStream.Close();
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000F1A74 File Offset: 0x000F0A74
		internal static void EncodeStream(Stream inp, List<char[]> planes)
		{
			StreamReader streamReader = new StreamReader(inp, Encoding.ASCII);
			int num = 0;
			byte[] seqs = new byte[7];
			string text;
			while ((text = streamReader.ReadLine()) != null)
			{
				if (text.Length >= 6)
				{
					switch (num)
					{
					case 0:
						if (text.IndexOf("begincidrange") >= 0)
						{
							num = 1;
						}
						else if (text.IndexOf("begincidchar") >= 0)
						{
							num = 2;
						}
						else if (text.IndexOf("usecmap") >= 0)
						{
							StringTokenizer stringTokenizer = new StringTokenizer(text);
							string text2 = stringTokenizer.NextToken();
							PdfEncodings.ReadCmap(text2.Substring(1), planes);
						}
						break;
					case 1:
						if (text.IndexOf("endcidrange") >= 0)
						{
							num = 0;
						}
						else
						{
							StringTokenizer stringTokenizer2 = new StringTokenizer(text);
							string text3 = stringTokenizer2.NextToken();
							int size = text3.Length / 2 - 1;
							long num2 = long.Parse(text3.Substring(1, text3.Length - 2), NumberStyles.HexNumber);
							text3 = stringTokenizer2.NextToken();
							long num3 = long.Parse(text3.Substring(1, text3.Length - 2), NumberStyles.HexNumber);
							text3 = stringTokenizer2.NextToken();
							int num4 = int.Parse(text3);
							for (long num5 = num2; num5 <= num3; num5 += 1L)
							{
								PdfEncodings.BreakLong(num5, size, seqs);
								PdfEncodings.EncodeSequence(size, seqs, (char)num4, planes);
								num4++;
							}
						}
						break;
					case 2:
						if (text.IndexOf("endcidchar") >= 0)
						{
							num = 0;
						}
						else
						{
							StringTokenizer stringTokenizer3 = new StringTokenizer(text);
							string text4 = stringTokenizer3.NextToken();
							int size2 = text4.Length / 2 - 1;
							long n = long.Parse(text4.Substring(1, text4.Length - 2), NumberStyles.HexNumber);
							text4 = stringTokenizer3.NextToken();
							int num6 = int.Parse(text4);
							PdfEncodings.BreakLong(n, size2, seqs);
							PdfEncodings.EncodeSequence(size2, seqs, (char)num6, planes);
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000F1C60 File Offset: 0x000F0C60
		internal static void BreakLong(long n, int size, byte[] seqs)
		{
			for (int i = 0; i < size; i++)
			{
				seqs[i] = (byte)(n >> (size - 1 - i) * 8);
			}
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000F1C8C File Offset: 0x000F0C8C
		internal static void EncodeSequence(int size, byte[] seqs, char cid, List<char[]> planes)
		{
			size--;
			int index = 0;
			char[] array;
			for (int i = 0; i < size; i++)
			{
				array = planes[index];
				int num = (int)(seqs[i] & byte.MaxValue);
				char c = array[num];
				if (c != '\0' && (c & '耀') == '\0')
				{
					throw new Exception(MessageLocalization.GetComposedMessage("inconsistent.mapping"));
				}
				if (c == '\0')
				{
					planes.Add(new char[256]);
					c = (char)(planes.Count - 1 | 32768);
					array[num] = c;
				}
				index = (int)(c & '翿');
			}
			array = planes[index];
			int num2 = (int)(seqs[size] & byte.MaxValue);
			char c2 = array[num2];
			if ((c2 & '耀') != '\0')
			{
				throw new Exception(MessageLocalization.GetComposedMessage("inconsistent.mapping"));
			}
			array[num2] = cid;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000F1D4C File Offset: 0x000F0D4C
		public static void AddExtraEncoding(string name, IExtraEncoding enc)
		{
			lock (PdfEncodings.extraEncodings)
			{
				Dictionary<string, IExtraEncoding> dictionary = new Dictionary<string, IExtraEncoding>(PdfEncodings.extraEncodings);
				dictionary[name.ToLower(CultureInfo.InvariantCulture)] = enc;
				PdfEncodings.extraEncodings = dictionary;
			}
		}

		// Token: 0x04001B85 RID: 7045
		protected const int CIDNONE = 0;

		// Token: 0x04001B86 RID: 7046
		protected const int CIDRANGE = 1;

		// Token: 0x04001B87 RID: 7047
		protected const int CIDCHAR = 2;

		// Token: 0x04001B88 RID: 7048
		internal static char[] winansiByteToChar = new char[]
		{
			'\0',
			'\u0001',
			'\u0002',
			'\u0003',
			'\u0004',
			'\u0005',
			'\u0006',
			'\a',
			'\b',
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u000e',
			'\u000f',
			'\u0010',
			'\u0011',
			'\u0012',
			'\u0013',
			'\u0014',
			'\u0015',
			'\u0016',
			'\u0017',
			'\u0018',
			'\u0019',
			'\u001a',
			'\u001b',
			'\u001c',
			'\u001d',
			'\u001e',
			'\u001f',
			' ',
			'!',
			'"',
			'#',
			'$',
			'%',
			'&',
			'\'',
			'(',
			')',
			'*',
			'+',
			',',
			'-',
			'.',
			'/',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			':',
			';',
			'<',
			'=',
			'>',
			'?',
			'@',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'[',
			'\\',
			']',
			'^',
			'_',
			'`',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'{',
			'|',
			'}',
			'~',
			'\u007f',
			'€',
			'�',
			'‚',
			'ƒ',
			'„',
			'…',
			'†',
			'‡',
			'ˆ',
			'‰',
			'Š',
			'‹',
			'Œ',
			'�',
			'Ž',
			'�',
			'�',
			'‘',
			'’',
			'“',
			'”',
			'•',
			'–',
			'—',
			'˜',
			'™',
			'š',
			'›',
			'œ',
			'�',
			'ž',
			'Ÿ',
			'\u00a0',
			'¡',
			'¢',
			'£',
			'¤',
			'¥',
			'¦',
			'§',
			'¨',
			'©',
			'ª',
			'«',
			'¬',
			'­',
			'®',
			'¯',
			'°',
			'±',
			'²',
			'³',
			'´',
			'µ',
			'¶',
			'·',
			'¸',
			'¹',
			'º',
			'»',
			'¼',
			'½',
			'¾',
			'¿',
			'À',
			'Á',
			'Â',
			'Ã',
			'Ä',
			'Å',
			'Æ',
			'Ç',
			'È',
			'É',
			'Ê',
			'Ë',
			'Ì',
			'Í',
			'Î',
			'Ï',
			'Ð',
			'Ñ',
			'Ò',
			'Ó',
			'Ô',
			'Õ',
			'Ö',
			'×',
			'Ø',
			'Ù',
			'Ú',
			'Û',
			'Ü',
			'Ý',
			'Þ',
			'ß',
			'à',
			'á',
			'â',
			'ã',
			'ä',
			'å',
			'æ',
			'ç',
			'è',
			'é',
			'ê',
			'ë',
			'ì',
			'í',
			'î',
			'ï',
			'ð',
			'ñ',
			'ò',
			'ó',
			'ô',
			'õ',
			'ö',
			'÷',
			'ø',
			'ù',
			'ú',
			'û',
			'ü',
			'ý',
			'þ',
			'ÿ'
		};

		// Token: 0x04001B89 RID: 7049
		internal static char[] pdfEncodingByteToChar = new char[]
		{
			'\0',
			'\u0001',
			'\u0002',
			'\u0003',
			'\u0004',
			'\u0005',
			'\u0006',
			'\a',
			'\b',
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u000e',
			'\u000f',
			'\u0010',
			'\u0011',
			'\u0012',
			'\u0013',
			'\u0014',
			'\u0015',
			'\u0016',
			'\u0017',
			'\u0018',
			'\u0019',
			'\u001a',
			'\u001b',
			'\u001c',
			'\u001d',
			'\u001e',
			'\u001f',
			' ',
			'!',
			'"',
			'#',
			'$',
			'%',
			'&',
			'\'',
			'(',
			')',
			'*',
			'+',
			',',
			'-',
			'.',
			'/',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			':',
			';',
			'<',
			'=',
			'>',
			'?',
			'@',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'[',
			'\\',
			']',
			'^',
			'_',
			'`',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'{',
			'|',
			'}',
			'~',
			'\u007f',
			'•',
			'†',
			'‡',
			'…',
			'—',
			'–',
			'ƒ',
			'⁄',
			'‹',
			'›',
			'−',
			'‰',
			'„',
			'“',
			'”',
			'‘',
			'’',
			'‚',
			'™',
			'ﬁ',
			'ﬂ',
			'Ł',
			'Œ',
			'Š',
			'Ÿ',
			'Ž',
			'ı',
			'ł',
			'œ',
			'š',
			'ž',
			'�',
			'€',
			'¡',
			'¢',
			'£',
			'¤',
			'¥',
			'¦',
			'§',
			'¨',
			'©',
			'ª',
			'«',
			'¬',
			'­',
			'®',
			'¯',
			'°',
			'±',
			'²',
			'³',
			'´',
			'µ',
			'¶',
			'·',
			'¸',
			'¹',
			'º',
			'»',
			'¼',
			'½',
			'¾',
			'¿',
			'À',
			'Á',
			'Â',
			'Ã',
			'Ä',
			'Å',
			'Æ',
			'Ç',
			'È',
			'É',
			'Ê',
			'Ë',
			'Ì',
			'Í',
			'Î',
			'Ï',
			'Ð',
			'Ñ',
			'Ò',
			'Ó',
			'Ô',
			'Õ',
			'Ö',
			'×',
			'Ø',
			'Ù',
			'Ú',
			'Û',
			'Ü',
			'Ý',
			'Þ',
			'ß',
			'à',
			'á',
			'â',
			'ã',
			'ä',
			'å',
			'æ',
			'ç',
			'è',
			'é',
			'ê',
			'ë',
			'ì',
			'í',
			'î',
			'ï',
			'ð',
			'ñ',
			'ò',
			'ó',
			'ô',
			'õ',
			'ö',
			'÷',
			'ø',
			'ù',
			'ú',
			'û',
			'ü',
			'ý',
			'þ',
			'ÿ'
		};

		// Token: 0x04001B8A RID: 7050
		internal static IntHashtable winansi = new IntHashtable();

		// Token: 0x04001B8B RID: 7051
		internal static IntHashtable pdfEncoding = new IntHashtable();

		// Token: 0x04001B8C RID: 7052
		internal static Dictionary<string, IExtraEncoding> extraEncodings = new Dictionary<string, IExtraEncoding>();

		// Token: 0x04001B8D RID: 7053
		internal static Dictionary<string, char[][]> cmaps = new Dictionary<string, char[][]>();

		// Token: 0x04001B8E RID: 7054
		public static byte[][] CRLF_CID_NEWLINE = new byte[][]
		{
			new byte[]
			{
				10
			},
			new byte[]
			{
				13,
				10
			}
		};

		// Token: 0x020004A0 RID: 1184
		private class WingdingsConversion : IExtraEncoding
		{
			// Token: 0x06002821 RID: 10273 RVA: 0x000F1DAC File Offset: 0x000F0DAC
			public byte[] CharToByte(char char1, string encoding)
			{
				if (char1 == ' ')
				{
					return new byte[]
					{
						(byte)char1
					};
				}
				if (char1 >= '✁' && char1 <= '➾')
				{
					byte b = PdfEncodings.WingdingsConversion.table[(int)(char1 - '✀')];
					if (b != 0)
					{
						return new byte[]
						{
							b
						};
					}
				}
				return new byte[0];
			}

			// Token: 0x06002822 RID: 10274 RVA: 0x000F1E00 File Offset: 0x000F0E00
			public byte[] CharToByte(string text, string encoding)
			{
				char[] array = text.ToCharArray();
				byte[] array2 = new byte[array.Length];
				int num = 0;
				int num2 = array.Length;
				for (int i = 0; i < num2; i++)
				{
					char c = array[i];
					if (c == ' ')
					{
						array2[num++] = (byte)c;
					}
					else if (c >= '✁' && c <= '➾')
					{
						byte b = PdfEncodings.WingdingsConversion.table[(int)(c - '✀')];
						if (b != 0)
						{
							array2[num++] = b;
						}
					}
				}
				if (num == num2)
				{
					return array2;
				}
				byte[] array3 = new byte[num];
				Array.Copy(array2, 0, array3, 0, num);
				return array3;
			}

			// Token: 0x06002823 RID: 10275 RVA: 0x000F1E95 File Offset: 0x000F0E95
			public string ByteToChar(byte[] b, string encoding)
			{
				return null;
			}

			// Token: 0x04001B8F RID: 7055
			private static byte[] table = new byte[]
			{
				0,
				35,
				34,
				0,
				0,
				0,
				41,
				62,
				81,
				42,
				0,
				0,
				65,
				63,
				0,
				0,
				0,
				0,
				0,
				252,
				0,
				0,
				0,
				251,
				0,
				0,
				0,
				0,
				0,
				0,
				86,
				0,
				88,
				89,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				181,
				0,
				0,
				0,
				0,
				0,
				182,
				0,
				0,
				0,
				173,
				175,
				172,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				124,
				123,
				0,
				0,
				0,
				84,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				166,
				0,
				0,
				0,
				113,
				114,
				0,
				0,
				0,
				117,
				0,
				0,
				0,
				0,
				0,
				0,
				125,
				126,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				140,
				141,
				142,
				143,
				144,
				145,
				146,
				147,
				148,
				149,
				129,
				130,
				131,
				132,
				133,
				134,
				135,
				136,
				137,
				138,
				140,
				141,
				142,
				143,
				144,
				145,
				146,
				147,
				148,
				149,
				232,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				232,
				216,
				0,
				0,
				196,
				198,
				0,
				0,
				240,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				220,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
		}

		// Token: 0x020004A1 RID: 1185
		private class Cp437Conversion : IExtraEncoding
		{
			// Token: 0x06002826 RID: 10278 RVA: 0x000F1F7C File Offset: 0x000F0F7C
			public byte[] CharToByte(string text, string encoding)
			{
				char[] array = text.ToCharArray();
				byte[] array2 = new byte[array.Length];
				int num = 0;
				int num2 = array.Length;
				for (int i = 0; i < num2; i++)
				{
					char c = array[i];
					if (c < '\u0080')
					{
						array2[num++] = (byte)c;
					}
					else
					{
						byte b = (byte)PdfEncodings.Cp437Conversion.c2b[(int)c];
						if (b != 0)
						{
							array2[num++] = b;
						}
					}
				}
				if (num == num2)
				{
					return array2;
				}
				byte[] array3 = new byte[num];
				Array.Copy(array2, 0, array3, 0, num);
				return array3;
			}

			// Token: 0x06002827 RID: 10279 RVA: 0x000F2004 File Offset: 0x000F1004
			public byte[] CharToByte(char char1, string encoding)
			{
				if (char1 < '\u0080')
				{
					return new byte[]
					{
						(byte)char1
					};
				}
				byte b = (byte)PdfEncodings.Cp437Conversion.c2b[(int)char1];
				if (b != 0)
				{
					return new byte[]
					{
						b
					};
				}
				return new byte[0];
			}

			// Token: 0x06002828 RID: 10280 RVA: 0x000F204C File Offset: 0x000F104C
			public string ByteToChar(byte[] b, string encoding)
			{
				int num = b.Length;
				char[] array = new char[num];
				int length = 0;
				for (int i = 0; i < num; i++)
				{
					int num2 = (int)(b[i] & byte.MaxValue);
					if (num2 >= 32)
					{
						if (num2 < 128)
						{
							array[length++] = (char)num2;
						}
						else
						{
							char c = PdfEncodings.Cp437Conversion.table[num2 - 128];
							array[length++] = c;
						}
					}
				}
				return new string(array, 0, length);
			}

			// Token: 0x06002829 RID: 10281 RVA: 0x000F21C0 File Offset: 0x000F11C0
			static Cp437Conversion()
			{
				for (int i = 0; i < PdfEncodings.Cp437Conversion.table.Length; i++)
				{
					PdfEncodings.Cp437Conversion.c2b[(int)PdfEncodings.Cp437Conversion.table[i]] = i + 128;
				}
			}

			// Token: 0x04001B90 RID: 7056
			private static IntHashtable c2b = new IntHashtable();

			// Token: 0x04001B91 RID: 7057
			private static char[] table = new char[]
			{
				'Ç',
				'ü',
				'é',
				'â',
				'ä',
				'à',
				'å',
				'ç',
				'ê',
				'ë',
				'è',
				'ï',
				'î',
				'ì',
				'Ä',
				'Å',
				'É',
				'æ',
				'Æ',
				'ô',
				'ö',
				'ò',
				'û',
				'ù',
				'ÿ',
				'Ö',
				'Ü',
				'¢',
				'£',
				'¥',
				'₧',
				'ƒ',
				'á',
				'í',
				'ó',
				'ú',
				'ñ',
				'Ñ',
				'ª',
				'º',
				'¿',
				'⌐',
				'¬',
				'½',
				'¼',
				'¡',
				'«',
				'»',
				'░',
				'▒',
				'▓',
				'│',
				'┤',
				'╡',
				'╢',
				'╖',
				'╕',
				'╣',
				'║',
				'╗',
				'╝',
				'╜',
				'╛',
				'┐',
				'└',
				'┴',
				'┬',
				'├',
				'─',
				'┼',
				'╞',
				'╟',
				'╚',
				'╔',
				'╩',
				'╦',
				'╠',
				'═',
				'╬',
				'╧',
				'╨',
				'╤',
				'╥',
				'╙',
				'╘',
				'╒',
				'╓',
				'╫',
				'╪',
				'┘',
				'┌',
				'█',
				'▄',
				'▌',
				'▐',
				'▀',
				'α',
				'ß',
				'Γ',
				'π',
				'Σ',
				'σ',
				'µ',
				'τ',
				'Φ',
				'Θ',
				'Ω',
				'δ',
				'∞',
				'φ',
				'ε',
				'∩',
				'≡',
				'±',
				'≥',
				'≤',
				'⌠',
				'⌡',
				'÷',
				'≈',
				'°',
				'∙',
				'·',
				'√',
				'ⁿ',
				'²',
				'■',
				'\u00a0'
			};
		}

		// Token: 0x020004A2 RID: 1186
		private class SymbolConversion : IExtraEncoding
		{
			// Token: 0x0600282B RID: 10283 RVA: 0x000F2223 File Offset: 0x000F1223
			internal SymbolConversion(bool symbol)
			{
				if (symbol)
				{
					this.translation = PdfEncodings.SymbolConversion.t1;
					return;
				}
				this.translation = PdfEncodings.SymbolConversion.t2;
			}

			// Token: 0x0600282C RID: 10284 RVA: 0x000F2248 File Offset: 0x000F1248
			public byte[] CharToByte(string text, string encoding)
			{
				char[] array = text.ToCharArray();
				byte[] array2 = new byte[array.Length];
				int num = 0;
				int num2 = array.Length;
				for (int i = 0; i < num2; i++)
				{
					char key = array[i];
					byte b = (byte)this.translation[(int)key];
					if (b != 0)
					{
						array2[num++] = b;
					}
				}
				if (num == num2)
				{
					return array2;
				}
				byte[] array3 = new byte[num];
				Array.Copy(array2, 0, array3, 0, num);
				return array3;
			}

			// Token: 0x0600282D RID: 10285 RVA: 0x000F22BC File Offset: 0x000F12BC
			public byte[] CharToByte(char char1, string encoding)
			{
				byte b = (byte)this.translation[(int)char1];
				if (b != 0)
				{
					return new byte[]
					{
						b
					};
				}
				return new byte[0];
			}

			// Token: 0x0600282E RID: 10286 RVA: 0x000F22ED File Offset: 0x000F12ED
			public string ByteToChar(byte[] b, string encoding)
			{
				return null;
			}

			// Token: 0x0600282F RID: 10287 RVA: 0x000F2670 File Offset: 0x000F1670
			static SymbolConversion()
			{
				for (int i = 0; i < PdfEncodings.SymbolConversion.table1.Length; i++)
				{
					int num = (int)PdfEncodings.SymbolConversion.table1[i];
					if (num != 0)
					{
						PdfEncodings.SymbolConversion.t1[num] = i + 32;
					}
				}
				for (int j = 0; j < PdfEncodings.SymbolConversion.table2.Length; j++)
				{
					int num2 = (int)PdfEncodings.SymbolConversion.table2[j];
					if (num2 != 0)
					{
						PdfEncodings.SymbolConversion.t2[num2] = j + 32;
					}
				}
			}

			// Token: 0x04001B92 RID: 7058
			private static IntHashtable t1 = new IntHashtable();

			// Token: 0x04001B93 RID: 7059
			private static IntHashtable t2 = new IntHashtable();

			// Token: 0x04001B94 RID: 7060
			private IntHashtable translation;

			// Token: 0x04001B95 RID: 7061
			private static char[] table1 = new char[]
			{
				' ',
				'!',
				'∀',
				'#',
				'∃',
				'%',
				'&',
				'∋',
				'(',
				')',
				'*',
				'+',
				',',
				'-',
				'.',
				'/',
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9',
				':',
				';',
				'<',
				'=',
				'>',
				'?',
				'≅',
				'Α',
				'Β',
				'Χ',
				'Δ',
				'Ε',
				'Φ',
				'Γ',
				'Η',
				'Ι',
				'ϑ',
				'Κ',
				'Λ',
				'Μ',
				'Ν',
				'Ο',
				'Π',
				'Θ',
				'Ρ',
				'Σ',
				'Τ',
				'Υ',
				'ς',
				'Ω',
				'Ξ',
				'Ψ',
				'Ζ',
				'[',
				'∴',
				']',
				'⊥',
				'_',
				'̅',
				'α',
				'β',
				'χ',
				'δ',
				'ε',
				'ϕ',
				'γ',
				'η',
				'ι',
				'φ',
				'κ',
				'λ',
				'μ',
				'ν',
				'ο',
				'π',
				'θ',
				'ρ',
				'σ',
				'τ',
				'υ',
				'ϖ',
				'ω',
				'ξ',
				'ψ',
				'ζ',
				'{',
				'|',
				'}',
				'~',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'€',
				'ϒ',
				'′',
				'≤',
				'⁄',
				'∞',
				'ƒ',
				'♣',
				'♦',
				'♥',
				'♠',
				'↔',
				'←',
				'↑',
				'→',
				'↓',
				'°',
				'±',
				'″',
				'≥',
				'×',
				'∝',
				'∂',
				'•',
				'÷',
				'≠',
				'≡',
				'≈',
				'…',
				'│',
				'─',
				'↵',
				'ℵ',
				'ℑ',
				'ℜ',
				'℘',
				'⊗',
				'⊕',
				'∅',
				'∩',
				'∪',
				'⊃',
				'⊇',
				'⊄',
				'⊂',
				'⊆',
				'∈',
				'∉',
				'∠',
				'∇',
				'®',
				'©',
				'™',
				'∏',
				'√',
				'•',
				'¬',
				'∧',
				'∨',
				'⇔',
				'⇐',
				'⇑',
				'⇒',
				'⇓',
				'◊',
				'〈',
				'\0',
				'\0',
				'\0',
				'∑',
				'⎛',
				'⎜',
				'⎝',
				'⎡',
				'⎢',
				'⎣',
				'⎧',
				'⎨',
				'⎩',
				'⎪',
				'\0',
				'〉',
				'∫',
				'⌠',
				'⎮',
				'⌡',
				'⎞',
				'⎟',
				'⎠',
				'⎤',
				'⎥',
				'⎦',
				'⎫',
				'⎬',
				'⎭',
				'\0'
			};

			// Token: 0x04001B96 RID: 7062
			private static char[] table2 = new char[]
			{
				' ',
				'✁',
				'✂',
				'✃',
				'✄',
				'☎',
				'✆',
				'✇',
				'✈',
				'✉',
				'☛',
				'☞',
				'✌',
				'✍',
				'✎',
				'✏',
				'✐',
				'✑',
				'✒',
				'✓',
				'✔',
				'✕',
				'✖',
				'✗',
				'✘',
				'✙',
				'✚',
				'✛',
				'✜',
				'✝',
				'✞',
				'✟',
				'✠',
				'✡',
				'✢',
				'✣',
				'✤',
				'✥',
				'✦',
				'✧',
				'★',
				'✩',
				'✪',
				'✫',
				'✬',
				'✭',
				'✮',
				'✯',
				'✰',
				'✱',
				'✲',
				'✳',
				'✴',
				'✵',
				'✶',
				'✷',
				'✸',
				'✹',
				'✺',
				'✻',
				'✼',
				'✽',
				'✾',
				'✿',
				'❀',
				'❁',
				'❂',
				'❃',
				'❄',
				'❅',
				'❆',
				'❇',
				'❈',
				'❉',
				'❊',
				'❋',
				'●',
				'❍',
				'■',
				'❏',
				'❐',
				'❑',
				'❒',
				'▲',
				'▼',
				'◆',
				'❖',
				'◗',
				'❘',
				'❙',
				'❚',
				'❛',
				'❜',
				'❝',
				'❞',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'\0',
				'❡',
				'❢',
				'❣',
				'❤',
				'❥',
				'❦',
				'❧',
				'♣',
				'♦',
				'♥',
				'♠',
				'①',
				'②',
				'③',
				'④',
				'⑤',
				'⑥',
				'⑦',
				'⑧',
				'⑨',
				'⑩',
				'❶',
				'❷',
				'❸',
				'❹',
				'❺',
				'❻',
				'❼',
				'❽',
				'❾',
				'❿',
				'➀',
				'➁',
				'➂',
				'➃',
				'➄',
				'➅',
				'➆',
				'➇',
				'➈',
				'➉',
				'➊',
				'➋',
				'➌',
				'➍',
				'➎',
				'➏',
				'➐',
				'➑',
				'➒',
				'➓',
				'➔',
				'→',
				'↔',
				'↕',
				'➘',
				'➙',
				'➚',
				'➛',
				'➜',
				'➝',
				'➞',
				'➟',
				'➠',
				'➡',
				'➢',
				'➣',
				'➤',
				'➥',
				'➦',
				'➧',
				'➨',
				'➩',
				'➪',
				'➫',
				'➬',
				'➭',
				'➮',
				'➯',
				'\0',
				'➱',
				'➲',
				'➳',
				'➴',
				'➵',
				'➶',
				'➷',
				'➸',
				'➹',
				'➺',
				'➻',
				'➼',
				'➽',
				'➾',
				'\0'
			};
		}

		// Token: 0x020004A3 RID: 1187
		private class SymbolTTConversion : IExtraEncoding
		{
			// Token: 0x06002830 RID: 10288 RVA: 0x000F2720 File Offset: 0x000F1720
			public byte[] CharToByte(char char1, string encoding)
			{
				if ((char1 & '＀') == '\0' || (char1 & '＀') == '')
				{
					return new byte[]
					{
						(byte)char1
					};
				}
				return new byte[0];
			}

			// Token: 0x06002831 RID: 10289 RVA: 0x000F2758 File Offset: 0x000F1758
			public byte[] CharToByte(string text, string encoding)
			{
				char[] array = text.ToCharArray();
				byte[] array2 = new byte[array.Length];
				int num = 0;
				int num2 = array.Length;
				for (int i = 0; i < num2; i++)
				{
					char c = array[i];
					if ((c & '＀') == '\0' || (c & '＀') == '')
					{
						array2[num++] = (byte)c;
					}
				}
				if (num == num2)
				{
					return array2;
				}
				byte[] array3 = new byte[num];
				Array.Copy(array2, 0, array3, 0, num);
				return array3;
			}

			// Token: 0x06002832 RID: 10290 RVA: 0x000F27CF File Offset: 0x000F17CF
			public string ByteToChar(byte[] b, string encoding)
			{
				return null;
			}
		}
	}
}
