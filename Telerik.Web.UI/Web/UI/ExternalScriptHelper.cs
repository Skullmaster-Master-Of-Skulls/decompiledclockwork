using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02000862 RID: 2146
	internal class ExternalScriptHelper
	{
		// Token: 0x06004F06 RID: 20230 RVA: 0x000F7B38 File Offset: 0x000F5D38
		static ExternalScriptHelper()
		{
			ScriptManagerConfigurationSettings configuration = ScriptManagerConfigurationSettings.GetConfiguration();
			string scriptFolder = configuration.ScriptFolder;
			string scriptsFolders = configuration.ScriptsFolders;
			ExternalScriptHelper.CacheFoldersWithAbsolutePaths(scriptFolder, scriptsFolders);
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x000F7B70 File Offset: 0x000F5D70
		private static void CacheFoldersWithAbsolutePaths(string scriptFolder, string scriptFolders)
		{
			ExternalScriptHelper._scriptFoldersRelativePaths = new List<string>();
			ExternalScriptHelper._scriptFoldersAbsolutePaths = new List<string>();
			if (scriptFolder != null)
			{
				ExternalScriptHelper.CacheWithAbsolutePath(scriptFolder);
			}
			if (scriptFolders != null)
			{
				foreach (string text in scriptFolders.Split(new char[]
				{
					';'
				}))
				{
					ExternalScriptHelper.CacheWithAbsolutePath(text.Trim());
				}
			}
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x000F7BCE File Offset: 0x000F5DCE
		private static void CacheWithAbsolutePath(string relativePath)
		{
			if (string.IsNullOrEmpty(relativePath))
			{
				return;
			}
			ExternalScriptHelper._scriptFoldersRelativePaths.Add(relativePath);
			ExternalScriptHelper._scriptFoldersAbsolutePaths.Add(ExternalScriptHelper.GetAbsolutePathWithTrailingSlash(relativePath));
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x000F7BF4 File Offset: 0x000F5DF4
		private static string GetAbsolutePathWithTrailingSlash(string relativePath)
		{
			string text = HttpContext.Current.Server.MapPath(relativePath);
			if (!text.EndsWith("\\"))
			{
				return text + "\\";
			}
			return text;
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x000F7C2C File Offset: 0x000F5E2C
		internal static string ResolveSecurePath(string scriptRelativePath)
		{
			string text = HttpContext.Current.Server.MapPath(scriptRelativePath);
			for (int i = 0; i < ExternalScriptHelper._scriptFoldersAbsolutePaths.Count; i++)
			{
				string text2 = string.Concat(new string[]
				{
					ExternalScriptHelper._scriptFoldersAbsolutePaths[i]
				});
				if (text.StartsWith(text2, StringComparison.InvariantCultureIgnoreCase))
				{
					string arg = text.Substring(text2.Length);
					return string.Format("{0}|{1}", i.ToString(), arg);
				}
			}
			throw new InsecureExternalStyleSheetException(scriptRelativePath);
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x000F7CB0 File Offset: 0x000F5EB0
		public static string GetSecurePathFromHash(string hash)
		{
			if (ExternalScriptHelper._scriptFoldersFiles == null)
			{
				ExternalScriptHelper._scriptFoldersFiles = new Dictionary<string, FileInfo[]>();
			}
			for (int i = 0; i < ExternalScriptHelper._scriptFoldersAbsolutePaths.Count; i++)
			{
				string text = ExternalScriptHelper._scriptFoldersAbsolutePaths[i];
				DirectoryInfo directoryInfo = new DirectoryInfo(text);
				FileInfo[] files;
				if (!ExternalScriptHelper._scriptFoldersFiles.TryGetValue(text, out files))
				{
					files = directoryInfo.GetFiles("*.js", SearchOption.AllDirectories);
					ExternalScriptHelper._scriptFoldersFiles[text] = files;
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
			return null;
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x000F7D84 File Offset: 0x000F5F84
		private static FileInfo GetScriptFileInfo(string securePath)
		{
			string[] array = securePath.Split(new char[]
			{
				'|'
			});
			string str = ExternalScriptHelper._scriptFoldersAbsolutePaths[int.Parse(array[0])];
			return new FileInfo(str + array[1]);
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x000F7DC8 File Offset: 0x000F5FC8
		public static string LoadContent(string securePath)
		{
			FileInfo scriptFileInfo = ExternalScriptHelper.GetScriptFileInfo(securePath);
			string result = string.Empty;
			if (scriptFileInfo.Exists)
			{
				using (StreamReader streamReader = scriptFileInfo.OpenText())
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x000F7E18 File Offset: 0x000F6018
		public static long GetLastModifiedInTicks(string securePath)
		{
			FileInfo scriptFileInfo = ExternalScriptHelper.GetScriptFileInfo(securePath);
			long result = -1L;
			if (scriptFileInfo.Exists)
			{
				result = scriptFileInfo.LastWriteTime.Ticks;
			}
			return result;
		}

		// Token: 0x040013AD RID: 5037
		private const string AppSettingsSectionName = "<appSettings />";

		// Token: 0x040013AE RID: 5038
		private const string ScriptFolderSettingTip = "This setting is required when combining external script files in RadScriptManager.";

		// Token: 0x040013AF RID: 5039
		private const string CssImageUrlPattern = "url('{0}')";

		// Token: 0x040013B0 RID: 5040
		private static List<string> _scriptFoldersRelativePaths;

		// Token: 0x040013B1 RID: 5041
		private static List<string> _scriptFoldersAbsolutePaths;

		// Token: 0x040013B2 RID: 5042
		private static Dictionary<string, FileInfo[]> _scriptFoldersFiles;

		// Token: 0x040013B3 RID: 5043
		private static readonly Regex _urlRegex = new Regex("url\\(\\s*[\\\"\\']?\\s*(?<url>.*?)\\s*[\\\"\\']?\\s*\\)", RegexOptions.Compiled);
	}
}
