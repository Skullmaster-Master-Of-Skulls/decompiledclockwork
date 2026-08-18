using System;

namespace System.IO
{
	// Token: 0x02000002 RID: 2
	public static class Path2
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002113 File Offset: 0x00000313
		internal static void CheckInvalidPathChars(string path, bool checkAdditional = false)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!Path2.HasIllegalCharacters(path, checkAdditional))
			{
				return;
			}
			throw new ArgumentException("The path has invalid characters.", "path");
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000213C File Offset: 0x0000033C
		internal static bool HasIllegalCharacters(string path, bool checkAdditional)
		{
			for (int i = 0; i < path.Length; i++)
			{
				int num = (int)path.get_Chars(i);
				if (num == 34 || num == 60 || num == 62 || num == 124 || num < 32)
				{
					return true;
				}
				if (checkAdditional && (num == 63 || num == 42))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002190 File Offset: 0x00000390
		public static string GetFileName(string path)
		{
			if (path != null)
			{
				Path2.CheckInvalidPathChars(path, false);
				int length = path.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = path.get_Chars(num);
					if (c == Path2.DirectorySeparatorChar || c == Path2.AltDirectorySeparatorChar || c == Path2.VolumeSeparatorChar)
					{
						return path.Substring(num + 1, length - num - 1);
					}
				}
				return path;
			}
			return path;
		}

		// Token: 0x04000001 RID: 1
		internal const int MAX_PATH = 260;

		// Token: 0x04000002 RID: 2
		internal const int MAX_DIRECTORY_PATH = 248;

		// Token: 0x04000003 RID: 3
		public static readonly char DirectorySeparatorChar = '\\';

		// Token: 0x04000004 RID: 4
		public static readonly char AltDirectorySeparatorChar = '/';

		// Token: 0x04000005 RID: 5
		public static readonly char VolumeSeparatorChar = ':';

		// Token: 0x04000006 RID: 6
		[Obsolete("Please use GetInvalidPathChars or GetInvalidFileNameChars instead.")]
		public static readonly char[] InvalidPathChars = new char[]
		{
			'"',
			'<',
			'>',
			'|',
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
			'\u001f'
		};

		// Token: 0x04000007 RID: 7
		internal static readonly char[] TrimEndChars = new char[]
		{
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			' ',
			'\u0085',
			'\u00a0'
		};

		// Token: 0x04000008 RID: 8
		private static readonly char[] RealInvalidPathChars = new char[]
		{
			'"',
			'<',
			'>',
			'|',
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
			'\u001f'
		};

		// Token: 0x04000009 RID: 9
		private static readonly char[] InvalidFileNameChars = new char[]
		{
			'"',
			'<',
			'>',
			'|',
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
			':',
			'*',
			'?',
			'\\',
			'/'
		};

		// Token: 0x0400000A RID: 10
		public static readonly char PathSeparator = ';';

		// Token: 0x0400000B RID: 11
		internal static readonly int MaxPath = 260;

		// Token: 0x0400000C RID: 12
		private static readonly int MaxDirectoryLength = 255;

		// Token: 0x0400000D RID: 13
		internal static readonly int MaxLongPath = 32000;

		// Token: 0x0400000E RID: 14
		private static readonly string Prefix = "\\\\?\\";

		// Token: 0x0400000F RID: 15
		private static readonly char[] s_Base32Char = new char[]
		{
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
			'0',
			'1',
			'2',
			'3',
			'4',
			'5'
		};
	}
}
