using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace WebGrease.Extensions
{
	// Token: 0x020000FE RID: 254
	public static class StringPathExtensions
	{
		// Token: 0x0600105B RID: 4187 RVA: 0x0004998B File Offset: 0x00047B8B
		public static string EnsureEndSeparator(this string directory)
		{
			if (directory.EndsWith(new string(Path.DirectorySeparatorChar, 1), StringComparison.OrdinalIgnoreCase))
			{
				return directory;
			}
			return directory + Path.DirectorySeparatorChar;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000499B4 File Offset: 0x00047BB4
		internal static string GetFullPathWithLowercase(this string originalPath)
		{
			if (string.IsNullOrWhiteSpace(originalPath))
			{
				return originalPath;
			}
			string result;
			try
			{
				result = (string.IsNullOrWhiteSpace(originalPath) ? originalPath : Path.GetFullPath(originalPath).ToLower(CultureInfo.CurrentUICulture));
			}
			catch (Exception ex)
			{
				Trace.TraceWarning("Exception occurred while trying to get the full path for path: {0}\r\n{1} ".InvariantFormat(new object[]
				{
					originalPath,
					ex.ToString()
				}));
				result = originalPath.ToLower(CultureInfo.CurrentUICulture);
			}
			return result;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00049A30 File Offset: 0x00047C30
		internal static string MakeAbsoluteTo(this string pathToConvert, string pathToConvertFrom)
		{
			string result;
			try
			{
				if (string.IsNullOrWhiteSpace(pathToConvert) || string.IsNullOrWhiteSpace(pathToConvertFrom))
				{
					result = pathToConvert;
				}
				else
				{
					result = Path.Combine(Path.GetDirectoryName(pathToConvertFrom), pathToConvert).GetFullPathWithLowercase();
				}
			}
			catch (Exception ex)
			{
				Trace.TraceWarning("Exception occurred while trying make {0} absolute to {1}\r\n{2} ".InvariantFormat(new object[]
				{
					pathToConvert,
					pathToConvertFrom,
					ex.ToString()
				}));
				result = pathToConvert;
			}
			return result;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00049AA4 File Offset: 0x00047CA4
		internal static string MakeRelativeTo(this string pathToConvert, string pathToConvertFrom, params char[] separators)
		{
			if (string.IsNullOrWhiteSpace(pathToConvert))
			{
				throw new ArgumentNullException("pathToConvert");
			}
			if (pathToConvertFrom.IsNullOrWhitespace())
			{
				return null;
			}
			char c = Path.DirectorySeparatorChar;
			char value = Path.AltDirectorySeparatorChar;
			if (separators != null && separators.Length == 2)
			{
				c = separators[0];
				value = separators[1];
			}
			string[] array = pathToConvert.Split(new char[]
			{
				c
			});
			string[] array2 = pathToConvertFrom.Split(new char[]
			{
				c
			});
			if (array2.Length == 0 || array.Length == 0 || array2[0] != array[0])
			{
				return pathToConvert;
			}
			int num = 1;
			while (num < array2.Length && num < array.Length && !(array2[num] != array[num]))
			{
				num++;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = num; i < array2.Length - 1; i++)
			{
				stringBuilder.Append("..");
				stringBuilder.Append(value);
			}
			for (int j = num; j < array.Length; j++)
			{
				stringBuilder.Append(array[j]);
				if (j < array.Length - 1)
				{
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString().ToLower(CultureInfo.CurrentUICulture);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00049BCC File Offset: 0x00047DCC
		internal static string MakeRelativeToDirectory(this string absolutePath, string relativeTo)
		{
			if (string.IsNullOrWhiteSpace(relativeTo))
			{
				return absolutePath;
			}
			if (absolutePath.Equals(relativeTo, StringComparison.OrdinalIgnoreCase))
			{
				return string.Empty;
			}
			relativeTo = relativeTo.EnsureEndSeparator();
			return new Uri(relativeTo).MakeRelativeUri(new Uri(absolutePath)).ToString().Replace("/", "\\");
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00049C20 File Offset: 0x00047E20
		internal static string NormalizeUrl(this string url)
		{
			url = url.Trim(new char[]
			{
				'\'',
				'"'
			});
			if (url.StartsWith("hash(", StringComparison.OrdinalIgnoreCase) && url.EndsWith(")", StringComparison.OrdinalIgnoreCase))
			{
				url = url.Substring(5, url.Length - 6);
			}
			if (url.StartsWith("hash://", StringComparison.OrdinalIgnoreCase))
			{
				url = url.Substring(7);
			}
			return url.Replace('/', '\\').TrimStart(new char[]
			{
				'\\'
			}).ToLowerInvariant();
		}
	}
}
