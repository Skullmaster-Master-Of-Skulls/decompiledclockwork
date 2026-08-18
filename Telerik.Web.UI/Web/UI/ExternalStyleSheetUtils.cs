using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02000F9F RID: 3999
	internal class ExternalStyleSheetUtils
	{
		// Token: 0x0600990F RID: 39183 RVA: 0x0022237C File Offset: 0x0022057C
		static ExternalStyleSheetUtils()
		{
			string text = ConfigurationManager.AppSettings["Telerik.Web.UI.StyleSheetFolder"];
			string text2 = ConfigurationManager.AppSettings["Telerik.Web.UI.StyleSheetFolders"];
			if (text == null && text2 == null)
			{
				throw new ConfigurationSettingMissingException("Telerik.Web.UI.StyleSheetFolders", "<appSettings />", "This setting is required when registering external style sheet files in RadStyleSheetManager.");
			}
			ExternalStyleSheetUtils.CacheFoldersWithAbsolutePaths(text, text2);
		}

		// Token: 0x06009910 RID: 39184 RVA: 0x002223DC File Offset: 0x002205DC
		private static void CacheFoldersWithAbsolutePaths(string styleSheetFolder, string styleSheetFolders)
		{
			ExternalStyleSheetUtils._styleSheetFoldersRelativePaths = new List<string>();
			ExternalStyleSheetUtils._styleSheetFoldersAbsolutePaths = new List<string>();
			if (styleSheetFolder != null)
			{
				ExternalStyleSheetUtils.CacheWithAbsolutePath(styleSheetFolder);
			}
			if (styleSheetFolders != null)
			{
				foreach (string text in styleSheetFolders.Split(new char[]
				{
					';'
				}))
				{
					ExternalStyleSheetUtils.CacheWithAbsolutePath(text.Trim());
				}
			}
		}

		// Token: 0x06009911 RID: 39185 RVA: 0x0022243A File Offset: 0x0022063A
		private static void CacheWithAbsolutePath(string relativePath)
		{
			if (string.IsNullOrEmpty(relativePath))
			{
				return;
			}
			ExternalStyleSheetUtils._styleSheetFoldersRelativePaths.Add(relativePath);
			ExternalStyleSheetUtils._styleSheetFoldersAbsolutePaths.Add(ExternalStyleSheetUtils.GetAbsolutePathWithTrailingSlash(relativePath));
		}

		// Token: 0x06009912 RID: 39186 RVA: 0x00222460 File Offset: 0x00220660
		private static string GetAbsolutePathWithTrailingSlash(string relativePath)
		{
			string text = HttpContext.Current.Server.MapPath(relativePath);
			if (!text.EndsWith("\\"))
			{
				return text + "\\";
			}
			return text;
		}

		// Token: 0x06009913 RID: 39187 RVA: 0x00222498 File Offset: 0x00220698
		internal static string ResolveSecurePath(string styleSheetRelativePath)
		{
			string text = HttpContext.Current.Server.MapPath(styleSheetRelativePath);
			for (int i = 0; i < ExternalStyleSheetUtils._styleSheetFoldersAbsolutePaths.Count; i++)
			{
				string text2 = string.Concat(new string[]
				{
					ExternalStyleSheetUtils._styleSheetFoldersAbsolutePaths[i]
				});
				if (text.StartsWith(text2, StringComparison.InvariantCultureIgnoreCase))
				{
					string arg = text.Substring(text2.Length);
					return string.Format("{0}|{1}", i.ToString(), arg);
				}
			}
			throw new InsecureExternalStyleSheetException(styleSheetRelativePath);
		}

		// Token: 0x06009914 RID: 39188 RVA: 0x0022251C File Offset: 0x0022071C
		public static string GetSecurePathFromHash(string hash)
		{
			if (ExternalStyleSheetUtils._styleSheetFoldersFiles == null)
			{
				ExternalStyleSheetUtils._styleSheetFoldersFiles = new Dictionary<string, FileInfo[]>();
			}
			for (int i = 0; i < ExternalStyleSheetUtils._styleSheetFoldersAbsolutePaths.Count; i++)
			{
				string text = ExternalStyleSheetUtils._styleSheetFoldersAbsolutePaths[i];
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				FileInfo[] files;
				if (!ExternalStyleSheetUtils._styleSheetFoldersFiles.TryGetValue(text, out files))
				{
					files = directoryInfo.GetFiles("*.css", SearchOption.AllDirectories);
					ExternalStyleSheetUtils._styleSheetFoldersFiles[text] = files;
				}
				foreach (FileInfo fileInfo in files)
				{
					string arg = fileInfo.FullName.Replace(text, string.Empty);
					string text2 = string.Format("{0}|{1}", i.ToString(), arg);
					if (hash.Equals(ScriptEntry.GetHashCode(text2)))
					{
						return text2;
					}
				}
			}
			if (RadStyleSheetManager.AllowFolderLookup)
			{
				RadStyleSheetManager.AllowFolderLookup = false;
				ExternalStyleSheetUtils._styleSheetFoldersFiles = null;
				return ExternalStyleSheetUtils.GetSecurePathFromHash(hash);
			}
			return null;
		}

		// Token: 0x06009915 RID: 39189 RVA: 0x0022260C File Offset: 0x0022080C
		private static FileInfo GetStyleSheet(string securePath)
		{
			string[] array = securePath.Split(new char[]
			{
				'|'
			});
			string str = ExternalStyleSheetUtils._styleSheetFoldersAbsolutePaths[int.Parse(array[0])];
			return new FileInfo(str + array[1]);
		}

		// Token: 0x06009916 RID: 39190 RVA: 0x00222650 File Offset: 0x00220850
		public static string LoadContent(string securePath)
		{
			FileInfo styleSheet = ExternalStyleSheetUtils.GetStyleSheet(securePath);
			string text = string.Empty;
			if (styleSheet.Exists)
			{
				using (StreamReader streamReader = styleSheet.OpenText())
				{
					text = streamReader.ReadToEnd();
					text = ExternalStyleSheetUtils.FixRelativeImageUrls(securePath, text);
				}
			}
			return text;
		}

		// Token: 0x06009917 RID: 39191 RVA: 0x002226DC File Offset: 0x002208DC
		private static string FixRelativeImageUrls(string securePath, string content)
		{
			string styleSheetFolderRelativePath = ExternalStyleSheetUtils.GetStyleSheetFolderRelativePath(securePath);
			content = ExternalStyleSheetUtils._urlRegex.Replace(content, delegate(Match match)
			{
				Group group = match.Groups[1];
				return ExternalStyleSheetUtils.GetFixedImageUrl(group.Value, styleSheetFolderRelativePath);
			});
			return content;
		}

		// Token: 0x06009918 RID: 39192 RVA: 0x00222718 File Offset: 0x00220918
		private static string GetStyleSheetFolderRelativePath(string securePath)
		{
			string[] array = securePath.Split(new char[]
			{
				'|'
			});
			return ExternalStyleSheetUtils._styleSheetFoldersRelativePaths[int.Parse(array[0])];
		}

		// Token: 0x06009919 RID: 39193 RVA: 0x00222750 File Offset: 0x00220950
		private static string GetFixedImageUrl(string brokenUrl, string styleSheetFolderRelativePath)
		{
			brokenUrl = brokenUrl.Trim();
			if (string.IsNullOrEmpty(brokenUrl) || brokenUrl.StartsWith("/") || brokenUrl.StartsWith("http:") || brokenUrl.StartsWith("https:") || brokenUrl.StartsWith("ftp:") || brokenUrl.StartsWith("data:") || brokenUrl.StartsWith("//"))
			{
				return string.Format("url('{0}')", brokenUrl);
			}
			return string.Format("url('{0}')", ExternalStyleSheetUtils.GetNormalizedBaseRelativePath(styleSheetFolderRelativePath) + brokenUrl);
		}

		// Token: 0x0600991A RID: 39194 RVA: 0x002227E0 File Offset: 0x002209E0
		private static string GetNormalizedBaseRelativePath(string relativePath)
		{
			string text = relativePath.TrimStart(new char[]
			{
				'~',
				'/'
			}).TrimEnd(new char[]
			{
				'/'
			});
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			return string.Format("{0}/", text);
		}

		// Token: 0x0600991B RID: 39195 RVA: 0x00222830 File Offset: 0x00220A30
		public static long GetLastModifiedInTicks(string securePath)
		{
			FileInfo styleSheet = ExternalStyleSheetUtils.GetStyleSheet(securePath);
			long result = -1L;
			if (styleSheet.Exists)
			{
				result = styleSheet.LastWriteTime.Ticks;
			}
			return result;
		}

		// Token: 0x04002B94 RID: 11156
		private const string StyleSheetFolderKey = "Telerik.Web.UI.StyleSheetFolder";

		// Token: 0x04002B95 RID: 11157
		private const string StyleSheetFoldersKey = "Telerik.Web.UI.StyleSheetFolders";

		// Token: 0x04002B96 RID: 11158
		private const string AppSettingsSectionName = "<appSettings />";

		// Token: 0x04002B97 RID: 11159
		private const string StyleSheetFolderSettingTip = "This setting is required when registering external style sheet files in RadStyleSheetManager.";

		// Token: 0x04002B98 RID: 11160
		private const string CssImageUrlPattern = "url('{0}')";

		// Token: 0x04002B99 RID: 11161
		private static List<string> _styleSheetFoldersRelativePaths;

		// Token: 0x04002B9A RID: 11162
		private static List<string> _styleSheetFoldersAbsolutePaths;

		// Token: 0x04002B9B RID: 11163
		private static Dictionary<string, FileInfo[]> _styleSheetFoldersFiles;

		// Token: 0x04002B9C RID: 11164
		private static readonly Regex _urlRegex = new Regex("url\\(\\s*[\\\"\\']?\\s*(?<url>.*?)\\s*[\\\"\\']?\\s*\\)", RegexOptions.Compiled);
	}
}
