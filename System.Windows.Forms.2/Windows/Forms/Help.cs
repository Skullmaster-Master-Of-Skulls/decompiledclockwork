using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000271 RID: 625
	public class Help
	{
		// Token: 0x06002803 RID: 10243 RVA: 0x00002843 File Offset: 0x00000A43
		private Help()
		{
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000BA376 File Offset: 0x000B8576
		public static void ShowHelp(Control parent, string url)
		{
			Help.ShowHelp(parent, url, HelpNavigator.TableOfContents, null);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000BA385 File Offset: 0x000B8585
		public static void ShowHelp(Control parent, string url, HelpNavigator navigator)
		{
			Help.ShowHelp(parent, url, navigator, null);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000BA390 File Offset: 0x000B8590
		public static void ShowHelp(Control parent, string url, string keyword)
		{
			if (keyword != null && keyword.Length != 0)
			{
				Help.ShowHelp(parent, url, HelpNavigator.Topic, keyword);
				return;
			}
			Help.ShowHelp(parent, url, HelpNavigator.TableOfContents, null);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000BA3B8 File Offset: 0x000B85B8
		public static void ShowHelp(Control parent, string url, HelpNavigator command, object parameter)
		{
			int helpFileType = Help.GetHelpFileType(url);
			if (helpFileType == 2)
			{
				Help.ShowHTML10Help(parent, url, command, parameter);
				return;
			}
			if (helpFileType != 3)
			{
				return;
			}
			Help.ShowHTMLFile(parent, url, command, parameter);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000BA3E8 File Offset: 0x000B85E8
		public static void ShowHelpIndex(Control parent, string url)
		{
			Help.ShowHelp(parent, url, HelpNavigator.Index, null);
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000BA3F8 File Offset: 0x000B85F8
		public static void ShowPopup(Control parent, string caption, Point location)
		{
			NativeMethods.HH_POPUP hh_POPUP = new NativeMethods.HH_POPUP();
			IntPtr intPtr = Marshal.StringToCoTaskMemAuto(caption);
			try
			{
				hh_POPUP.pszText = intPtr;
				hh_POPUP.idString = 0;
				hh_POPUP.pt = new NativeMethods.POINT(location.X, location.Y);
				hh_POPUP.clrBackground = (Color.FromKnownColor(KnownColor.Window).ToArgb() & 16777215);
				Help.ShowHTML10Help(parent, null, HelpNavigator.Topic, hh_POPUP);
			}
			finally
			{
				Marshal.FreeCoTaskMem(intPtr);
			}
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000BA47C File Offset: 0x000B867C
		private static void ShowHTML10Help(Control parent, string url, HelpNavigator command, object param)
		{
			IntSecurity.UnmanagedCode.Demand();
			string pszFile = url;
			Uri uri = Help.Resolve(url);
			if (uri != null)
			{
				pszFile = uri.AbsoluteUri;
			}
			if (uri == null || uri.IsFile)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string lpszLongPath = (uri != null && uri.IsFile) ? uri.LocalPath : url;
				uint shortPathName = UnsafeNativeMethods.GetShortPathName(lpszLongPath, stringBuilder, 0U);
				if (shortPathName > 0U)
				{
					stringBuilder.Capacity = (int)shortPathName;
					shortPathName = UnsafeNativeMethods.GetShortPathName(lpszLongPath, stringBuilder, shortPathName);
					pszFile = stringBuilder.ToString();
				}
			}
			HandleRef hwndCaller;
			if (parent != null)
			{
				hwndCaller = new HandleRef(parent, parent.Handle);
			}
			else
			{
				hwndCaller = new HandleRef(null, UnsafeNativeMethods.GetActiveWindow());
			}
			string text = param as string;
			if (text != null)
			{
				object obj;
				int uCommand = Help.MapCommandToHTMLCommand(command, text, out obj);
				string text2 = obj as string;
				if (text2 != null)
				{
					SafeNativeMethods.HtmlHelp(hwndCaller, pszFile, uCommand, text2);
					return;
				}
				if (obj is int)
				{
					SafeNativeMethods.HtmlHelp(hwndCaller, pszFile, uCommand, (int)obj);
					return;
				}
				if (obj is NativeMethods.HH_FTS_QUERY)
				{
					SafeNativeMethods.HtmlHelp(hwndCaller, pszFile, uCommand, (NativeMethods.HH_FTS_QUERY)obj);
					return;
				}
				if (obj is NativeMethods.HH_AKLINK)
				{
					SafeNativeMethods.HtmlHelp(NativeMethods.NullHandleRef, pszFile, 0, null);
					SafeNativeMethods.HtmlHelp(hwndCaller, pszFile, uCommand, (NativeMethods.HH_AKLINK)obj);
					return;
				}
				SafeNativeMethods.HtmlHelp(hwndCaller, pszFile, uCommand, (string)param);
				return;
			}
			else
			{
				if (param == null)
				{
					object obj;
					SafeNativeMethods.HtmlHelp(hwndCaller, pszFile, Help.MapCommandToHTMLCommand(command, null, out obj), 0);
					return;
				}
				if (param is NativeMethods.HH_POPUP)
				{
					SafeNativeMethods.HtmlHelp(hwndCaller, pszFile, 14, (NativeMethods.HH_POPUP)param);
					return;
				}
				if (param.GetType() == typeof(int))
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"param",
						"Integer"
					}));
				}
				return;
			}
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000BA63C File Offset: 0x000B883C
		private static void ShowHTMLFile(Control parent, string url, HelpNavigator command, object param)
		{
			Uri uri = Help.Resolve(url);
			if (uri == null)
			{
				throw new ArgumentException(SR.GetString("HelpInvalidURL", new object[]
				{
					url
				}), "url");
			}
			string scheme = uri.Scheme;
			if (scheme == "http" || scheme == "https")
			{
				new WebPermission(NetworkAccess.Connect, url).Demand();
			}
			else
			{
				IntSecurity.UnmanagedCode.Demand();
			}
			if (command != HelpNavigator.Topic)
			{
				if (command - HelpNavigator.TableOfContents > 2)
				{
				}
			}
			else if (param != null && param is string)
			{
				uri = new Uri(uri.ToString() + "#" + (string)param);
			}
			HandleRef hwnd;
			if (parent != null)
			{
				hwnd = new HandleRef(parent, parent.Handle);
			}
			else
			{
				hwnd = new HandleRef(null, UnsafeNativeMethods.GetActiveWindow());
			}
			UnsafeNativeMethods.ShellExecute_NoBFM(hwnd, null, uri.ToString(), null, null, 1);
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000BA720 File Offset: 0x000B8920
		private static Uri Resolve(string partialUri)
		{
			Uri uri = null;
			if (!string.IsNullOrEmpty(partialUri))
			{
				try
				{
					uri = new Uri(partialUri);
				}
				catch (UriFormatException)
				{
				}
				catch (ArgumentNullException)
				{
				}
			}
			if (uri != null && uri.Scheme == "file")
			{
				string localPath = NativeMethods.GetLocalPath(partialUri);
				new FileIOPermission(FileIOPermissionAccess.Read, localPath).Assert();
				try
				{
					if (!File.Exists(localPath))
					{
						uri = null;
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			if (uri == null)
			{
				try
				{
					uri = new Uri(new Uri(AppDomain.CurrentDomain.SetupInformation.ApplicationBase), partialUri);
				}
				catch (UriFormatException)
				{
				}
				catch (ArgumentNullException)
				{
				}
				if (uri != null && uri.Scheme == "file")
				{
					string path = uri.LocalPath + uri.Fragment;
					new FileIOPermission(FileIOPermissionAccess.Read, path).Assert();
					try
					{
						if (!File.Exists(path))
						{
							uri = null;
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			return uri;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000BA84C File Offset: 0x000B8A4C
		private static int GetHelpFileType(string url)
		{
			if (url == null)
			{
				return 3;
			}
			Uri uri = Help.Resolve(url);
			if (uri == null || uri.Scheme == "file")
			{
				string a = Path.GetExtension((uri == null) ? url : (uri.LocalPath + uri.Fragment)).ToLower(CultureInfo.InvariantCulture);
				if (a == ".chm" || a == ".col")
				{
					return 2;
				}
			}
			return 3;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000BA8CC File Offset: 0x000B8ACC
		private static int MapCommandToHTMLCommand(HelpNavigator command, string param, out object htmlParam)
		{
			htmlParam = param;
			if (string.IsNullOrEmpty(param) && (command == HelpNavigator.AssociateIndex || command == HelpNavigator.KeywordIndex))
			{
				return 2;
			}
			switch (command)
			{
			case HelpNavigator.Topic:
				return 0;
			case HelpNavigator.TableOfContents:
				return 1;
			case HelpNavigator.Index:
				return 2;
			case HelpNavigator.Find:
				htmlParam = new NativeMethods.HH_FTS_QUERY
				{
					pszSearchQuery = param
				};
				return 3;
			case HelpNavigator.AssociateIndex:
			case HelpNavigator.KeywordIndex:
				break;
			case HelpNavigator.TopicId:
				try
				{
					htmlParam = int.Parse(param, CultureInfo.InvariantCulture);
					return 15;
				}
				catch
				{
					return 2;
				}
				break;
			default:
				return (int)command;
			}
			htmlParam = new NativeMethods.HH_AKLINK
			{
				pszKeywords = param,
				fIndexOnFail = true,
				fReserved = false
			};
			if (command != HelpNavigator.KeywordIndex)
			{
				return 19;
			}
			return 13;
		}

		// Token: 0x0400106F RID: 4207
		internal static readonly TraceSwitch WindowsFormsHelpTrace;

		// Token: 0x04001070 RID: 4208
		private const int HH_DISPLAY_TOPIC = 0;

		// Token: 0x04001071 RID: 4209
		private const int HH_HELP_FINDER = 0;

		// Token: 0x04001072 RID: 4210
		private const int HH_DISPLAY_TOC = 1;

		// Token: 0x04001073 RID: 4211
		private const int HH_DISPLAY_INDEX = 2;

		// Token: 0x04001074 RID: 4212
		private const int HH_DISPLAY_SEARCH = 3;

		// Token: 0x04001075 RID: 4213
		private const int HH_SET_WIN_TYPE = 4;

		// Token: 0x04001076 RID: 4214
		private const int HH_GET_WIN_TYPE = 5;

		// Token: 0x04001077 RID: 4215
		private const int HH_GET_WIN_HANDLE = 6;

		// Token: 0x04001078 RID: 4216
		private const int HH_ENUM_INFO_TYPE = 7;

		// Token: 0x04001079 RID: 4217
		private const int HH_SET_INFO_TYPE = 8;

		// Token: 0x0400107A RID: 4218
		private const int HH_SYNC = 9;

		// Token: 0x0400107B RID: 4219
		private const int HH_ADD_NAV_UI = 10;

		// Token: 0x0400107C RID: 4220
		private const int HH_ADD_BUTTON = 11;

		// Token: 0x0400107D RID: 4221
		private const int HH_GETBROWSER_APP = 12;

		// Token: 0x0400107E RID: 4222
		private const int HH_KEYWORD_LOOKUP = 13;

		// Token: 0x0400107F RID: 4223
		private const int HH_DISPLAY_TEXT_POPUP = 14;

		// Token: 0x04001080 RID: 4224
		private const int HH_HELP_CONTEXT = 15;

		// Token: 0x04001081 RID: 4225
		private const int HH_TP_HELP_CONTEXTMENU = 16;

		// Token: 0x04001082 RID: 4226
		private const int HH_TP_HELP_WM_HELP = 17;

		// Token: 0x04001083 RID: 4227
		private const int HH_CLOSE_ALL = 18;

		// Token: 0x04001084 RID: 4228
		private const int HH_ALINK_LOOKUP = 19;

		// Token: 0x04001085 RID: 4229
		private const int HH_GET_LAST_ERROR = 20;

		// Token: 0x04001086 RID: 4230
		private const int HH_ENUM_CATEGORY = 21;

		// Token: 0x04001087 RID: 4231
		private const int HH_ENUM_CATEGORY_IT = 22;

		// Token: 0x04001088 RID: 4232
		private const int HH_RESET_IT_FILTER = 23;

		// Token: 0x04001089 RID: 4233
		private const int HH_SET_INCLUSIVE_FILTER = 24;

		// Token: 0x0400108A RID: 4234
		private const int HH_SET_EXCLUSIVE_FILTER = 25;

		// Token: 0x0400108B RID: 4235
		private const int HH_SET_GUID = 26;

		// Token: 0x0400108C RID: 4236
		private const int HTML10HELP = 2;

		// Token: 0x0400108D RID: 4237
		private const int HTMLFILE = 3;
	}
}
