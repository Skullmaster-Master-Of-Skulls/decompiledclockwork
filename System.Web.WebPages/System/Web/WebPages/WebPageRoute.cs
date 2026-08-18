using System;
using System.IO;
using System.Web.WebPages.Deployment;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages
{
	// Token: 0x02000088 RID: 136
	internal sealed class WebPageRoute
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000D230 File Offset: 0x0000B430
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000D25F File Offset: 0x0000B45F
		internal bool IsExplicitlyDisabled
		{
			get
			{
				bool? isExplicitlyDisabled = this._isExplicitlyDisabled;
				if (isExplicitlyDisabled == null)
				{
					return WebPageRoute._isRootExplicitlyDisabled.Value;
				}
				return isExplicitlyDisabled.GetValueOrDefault();
			}
			set
			{
				this._isExplicitlyDisabled = new bool?(value);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000D270 File Offset: 0x0000B470
		internal void DoPostResolveRequestCache(HttpContextBase context)
		{
			if (this.IsExplicitlyDisabled)
			{
				return;
			}
			string text = context.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + context.Request.PathInfo;
			string[] supportedExtensions = WebPageHttpHandler.SupportedExtensions;
			WebPageMatch webPageMatch = WebPageRoute.MatchRequest(text, supportedExtensions, VirtualPathFactoryManager.InstancePathExists, context, DisplayModeProvider.Instance);
			if (webPageMatch != null)
			{
				context.Items[typeof(WebPageMatch)] = webPageMatch;
				string text2 = "~/" + webPageMatch.MatchedPath;
				if (!WebPagesDeployment.IsExplicitlyDisabled(text2))
				{
					IHttpHandler httpHandler = WebPageHttpHandler.CreateFromVirtualPath(text2);
					if (httpHandler != null)
					{
						SessionStateUtil.SetUpSessionState(context, httpHandler);
						context.RemapHandler(httpHandler);
						return;
					}
				}
			}
			else
			{
				string extension = PathUtil.GetExtension(text);
				foreach (string str in supportedExtensions)
				{
					if (string.Equals("." + str, extension, StringComparison.OrdinalIgnoreCase))
					{
						throw new HttpException(404, null);
					}
				}
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000D358 File Offset: 0x0000B558
		private static bool FileExists(string virtualPath, Func<string, bool> virtualPathExists)
		{
			string arg = "~/" + virtualPath;
			return virtualPathExists(arg);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000D378 File Offset: 0x0000B578
		internal static WebPageMatch GetWebPageMatch(HttpContextBase context)
		{
			return (WebPageMatch)context.Items[typeof(WebPageMatch)];
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		private static string GetRouteLevelMatch(string pathValue, string[] supportedExtensions, Func<string, bool> virtualPathExists, HttpContextBase context, DisplayModeProvider displayModeProvider)
		{
			int i = 0;
			while (i < supportedExtensions.Length)
			{
				string text = supportedExtensions[i];
				string virtualPath;
				if (!PathHelpers.EndsWithExtension(pathValue, text))
				{
					virtualPath = "~/" + pathValue + "." + text;
				}
				else
				{
					virtualPath = "~/" + pathValue;
				}
				DisplayInfo displayInfoForVirtualPath = displayModeProvider.GetDisplayInfoForVirtualPath(virtualPath, context, virtualPathExists, null);
				if (displayInfoForVirtualPath != null)
				{
					if (Path.GetFileName(displayInfoForVirtualPath.FilePath).StartsWith("_", StringComparison.OrdinalIgnoreCase))
					{
						throw new HttpException(404, WebPageResources.WebPageRoute_UnderscoreBlocked);
					}
					string text2 = displayInfoForVirtualPath.FilePath;
					if (text2.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
					{
						text2 = text2.Remove(0, 2);
					}
					DisplayModeProvider.SetDisplayMode(context, displayInfoForVirtualPath.DisplayMode);
					return text2;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000D45C File Offset: 0x0000B65C
		internal static WebPageMatch MatchRequest(string pathValue, string[] supportedExtensions, Func<string, bool> virtualPathExists, HttpContextBase context, DisplayModeProvider displayModes)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(pathValue))
			{
				if (WebPageRoute.FileExists(pathValue, virtualPathExists))
				{
					bool flag = false;
					foreach (string extension in supportedExtensions)
					{
						if (PathHelpers.EndsWithExtension(pathValue, extension))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return null;
					}
				}
				text = pathValue;
				string pathInfo = string.Empty;
				string routeLevelMatch;
				for (;;)
				{
					routeLevelMatch = WebPageRoute.GetRouteLevelMatch(text, supportedExtensions, virtualPathExists, context, displayModes);
					if (routeLevelMatch != null)
					{
						break;
					}
					int num = text.LastIndexOf('/');
					if (num == -1)
					{
						goto IL_89;
					}
					text = text.Substring(0, num);
					pathInfo = pathValue.Substring(num + 1);
				}
				return new WebPageMatch(routeLevelMatch, pathInfo);
			}
			IL_89:
			return WebPageRoute.MatchDefaultFiles(pathValue, supportedExtensions, virtualPathExists, context, displayModes, text);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000D500 File Offset: 0x0000B700
		private static WebPageMatch MatchDefaultFiles(string pathValue, string[] supportedExtensions, Func<string, bool> virtualPathExists, HttpContextBase context, DisplayModeProvider displayModes, string currentLevel)
		{
			currentLevel = pathValue;
			string pathValue2;
			string pathValue3;
			if (string.IsNullOrEmpty(currentLevel))
			{
				pathValue2 = "default";
				pathValue3 = "index";
			}
			else
			{
				if (currentLevel[currentLevel.Length - 1] != '/')
				{
					currentLevel += "/";
				}
				pathValue2 = currentLevel + "default";
				pathValue3 = currentLevel + "index";
			}
			string routeLevelMatch = WebPageRoute.GetRouteLevelMatch(pathValue2, supportedExtensions, virtualPathExists, context, displayModes);
			if (routeLevelMatch != null)
			{
				return new WebPageMatch(routeLevelMatch, string.Empty);
			}
			string routeLevelMatch2 = WebPageRoute.GetRouteLevelMatch(pathValue3, supportedExtensions, virtualPathExists, context, displayModes);
			if (routeLevelMatch2 != null)
			{
				return new WebPageMatch(routeLevelMatch2, string.Empty);
			}
			return null;
		}

		// Token: 0x0400012C RID: 300
		private static readonly Lazy<bool> _isRootExplicitlyDisabled = new Lazy<bool>(() => WebPagesDeployment.IsExplicitlyDisabled("~/"));

		// Token: 0x0400012D RID: 301
		private bool? _isExplicitlyDisabled;
	}
}
