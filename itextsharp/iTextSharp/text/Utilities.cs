using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x02000416 RID: 1046
	public class Utilities
	{
		// Token: 0x0600238B RID: 9099 RVA: 0x000D9BAC File Offset: 0x000D8BAC
		public static ICollection<K> GetKeySet<K, V>(Dictionary<K, V> table)
		{
			if (table != null)
			{
				return table.Keys;
			}
			return new List<K>();
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000D9BCC File Offset: 0x000D8BCC
		public static object[][] AddToArray(object[][] original, object[] item)
		{
			if (original == null)
			{
				original = new object[][]
				{
					item
				};
				return original;
			}
			object[][] array = new object[original.Length + 1][];
			Array.Copy(original, 0, array, 0, original.Length);
			array[original.Length] = item;
			return array;
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x000D9C08 File Offset: 0x000D8C08
		public static bool CheckTrueOrFalse(Properties attributes, string key)
		{
			return Util.EqualsIgnoreCase("true", attributes[key]);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x000D9C1C File Offset: 0x000D8C1C
		public static Uri ToURL(string filename)
		{
			Uri result;
			try
			{
				result = new Uri(filename);
			}
			catch
			{
				result = new Uri("file:///" + filename);
			}
			return result;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x000D9C58 File Offset: 0x000D8C58
		public static string UnEscapeURL(string src)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = src.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				char c = array[i];
				if (c == '%')
				{
					if (i + 2 >= array.Length)
					{
						stringBuilder.Append(c);
					}
					else
					{
						int hex = PRTokeniser.GetHex((int)array[i + 1]);
						int hex2 = PRTokeniser.GetHex((int)array[i + 2]);
						if (hex < 0 || hex2 < 0)
						{
							stringBuilder.Append(c);
						}
						else
						{
							stringBuilder.Append((char)(hex * 16 + hex2));
							i += 2;
						}
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x000D9CEC File Offset: 0x000D8CEC
		public static void Skip(Stream istr, int size)
		{
			while (size > 0)
			{
				int num = istr.Read(Utilities.skipBuffer, 0, Math.Min(Utilities.skipBuffer.Length, size));
				if (num <= 0)
				{
					return;
				}
				size -= num;
			}
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x000D9D23 File Offset: 0x000D8D23
		public static float MillimetersToPoints(float value)
		{
			return Utilities.InchesToPoints(Utilities.MillimetersToInches(value));
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000D9D30 File Offset: 0x000D8D30
		public static float MillimetersToInches(float value)
		{
			return value / 25.4f;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000D9D39 File Offset: 0x000D8D39
		public static float PointsToMillimeters(float value)
		{
			return Utilities.InchesToMillimeters(Utilities.PointsToInches(value));
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000D9D46 File Offset: 0x000D8D46
		public static float PointsToInches(float value)
		{
			return value / 72f;
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x000D9D4F File Offset: 0x000D8D4F
		public static float InchesToMillimeters(float value)
		{
			return value * 25.4f;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000D9D58 File Offset: 0x000D8D58
		public static float InchesToPoints(float value)
		{
			return value * 72f;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x000D9D61 File Offset: 0x000D8D61
		public static bool IsSurrogateHigh(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000D9D78 File Offset: 0x000D8D78
		public static bool IsSurrogateLow(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x000D9D8F File Offset: 0x000D8D8F
		public static bool IsSurrogatePair(string text, int idx)
		{
			return idx >= 0 && idx <= text.Length - 2 && Utilities.IsSurrogateHigh(text[idx]) && Utilities.IsSurrogateLow(text[idx + 1]);
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000D9DC0 File Offset: 0x000D8DC0
		public static bool IsSurrogatePair(char[] text, int idx)
		{
			return idx >= 0 && idx <= text.Length - 2 && Utilities.IsSurrogateHigh(text[idx]) && Utilities.IsSurrogateLow(text[idx + 1]);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000D9DE6 File Offset: 0x000D8DE6
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000D9E03 File Offset: 0x000D8E03
		public static int ConvertToUtf32(char[] text, int idx)
		{
			return (int)((text[idx] - '\ud800') * 'Ѐ' + (text[idx + 1] - '\udc00')) + 65536;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000D9E26 File Offset: 0x000D8E26
		public static int ConvertToUtf32(string text, int idx)
		{
			return (int)((text[idx] - '\ud800') * 'Ѐ' + (text[idx + 1] - '\udc00')) + 65536;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x000D9E54 File Offset: 0x000D8E54
		public static string ConvertFromUtf32(int codePoint)
		{
			if (codePoint < 65536)
			{
				return char.ToString((char)codePoint);
			}
			codePoint -= 65536;
			return new string(new char[]
			{
				(char)(codePoint / 1024 + 55296),
				(char)(codePoint % 1024 + 56320)
			});
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000D9EAC File Offset: 0x000D8EAC
		public static string ReadFileToString(string path)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x0400188B RID: 6283
		private static byte[] skipBuffer = new byte[4096];
	}
}
