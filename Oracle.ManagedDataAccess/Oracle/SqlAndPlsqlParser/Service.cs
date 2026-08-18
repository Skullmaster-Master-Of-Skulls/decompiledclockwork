using System;
using System.IO;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000285 RID: 645
	internal class Service
	{
		// Token: 0x0600192A RID: 6442 RVA: 0x00108308 File Offset: 0x00106508
		public static string ReadFile(string file)
		{
			string result = string.Empty;
			InputStream inputStream = null;
			try
			{
				inputStream = new InputStream(file);
				result = inputStream.ReadToEnd();
			}
			catch (Exception ex)
			{
				Logger.GetLogger("Service").Log(LoggerLevel.WARNING, ex.StackTrace, ex);
			}
			finally
			{
				if (inputStream != null)
				{
					inputStream.Close();
				}
			}
			return result;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00108370 File Offset: 0x00106570
		public static void Copy(Uri url, string dstdir)
		{
			try
			{
				string absolutePath = url.AbsolutePath;
				FileInfo fileInfo = new FileInfo(absolutePath);
				string name = fileInfo.Name;
				Stream stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read);
				Stream stream2 = new FileStream(new FileInfo(dstdir + Path.DirectorySeparatorChar.ToString() + name).FullName, FileMode.Create);
				sbyte[] array = new sbyte[1024];
				int count;
				while ((count = SupportClass.ReadInput(stream, array, 0, array.Length)) > 0)
				{
					stream2.Write(SupportClass.ToByteArray(array), 0, count);
				}
				stream.Close();
				stream2.Close();
			}
			catch (IOException ex)
			{
				Logger.GetLogger("Messages").Log(LoggerLevel.WARNING, ex.StackTrace, ex);
			}
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00108430 File Offset: 0x00106630
		public static string ToNull(string src)
		{
			if (src != null && src.Length != 0)
			{
				return src;
			}
			return null;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00108440 File Offset: 0x00106640
		public static string IndentLine(int level, string txt)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(' ', level);
			stringBuilder.Append(txt);
			return stringBuilder.ToString();
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x0010846C File Offset: 0x0010666C
		public static void IndentLine(int level, string txt, ref StringBuilder sb)
		{
			sb.Append(' ', level);
			sb.Append(txt);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00108484 File Offset: 0x00106684
		public static int Pair(int x, int y)
		{
			return y << 16 | x;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0010848C File Offset: 0x0010668C
		public static int Y(int p)
		{
			return p >> 16 & 65535;
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00108498 File Offset: 0x00106698
		public static int X(int p)
		{
			return p & 65535;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x001084A4 File Offset: 0x001066A4
		public static long LongPair(int x, int y)
		{
			return (long)y << 32 | (long)((ulong)x);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x001084B0 File Offset: 0x001066B0
		public static int LongY(long p)
		{
			return (int)(p >> 32);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x001084B8 File Offset: 0x001066B8
		public static int LongX(long p)
		{
			return (int)p;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x001084BC File Offset: 0x001066BC
		public static int DecrementPair(int p)
		{
			return p - 65537;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x001084C8 File Offset: 0x001066C8
		public static string HandleMixedCase(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			if (name[0] == '"')
			{
				int length = name.Length;
				if (length > 1 && name[length - 1] == '"')
				{
					name = name.Replace("\".\"", ".").Substring(1, length - 2);
				}
			}
			else
			{
				name = name.ToUpper();
			}
			return name;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0010852C File Offset: 0x0010672C
		public static string Into2Chars(string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char i2 in data)
			{
				stringBuilder.Append((char)(Service.PeriodicRemainder((int)i2, 16) + 70));
				stringBuilder.Append((char)(Service.PeriodicDivision((int)i2, 16) + 70));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00108588 File Offset: 0x00106788
		public static string From2Chars(string data)
		{
			if (data == null)
			{
				return null;
			}
			int length = data.Length;
			if (length % 2 != 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < length)
			{
				stringBuilder.Append((data[i++] - 'F') * '\u0010' + (data[i++] - 'F'));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x001085E4 File Offset: 0x001067E4
		internal static char[] Into2Chars(char input)
		{
			return new char[]
			{
				(char)(Service.PeriodicRemainder((int)input, 16) + 70),
				(char)(Service.PeriodicDivision((int)input, 16) + 70)
			};
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00108618 File Offset: 0x00106818
		internal static int PeriodicRemainder(int i, int j)
		{
			int num = i % j;
			if (num < 0)
			{
				num += j;
			}
			return num;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00108634 File Offset: 0x00106834
		internal static int PeriodicDivision(int i, int j)
		{
			int num = i / j;
			if (num < 0)
			{
				num--;
			}
			return num;
		}
	}
}
