using System;
using System.Runtime.InteropServices;

namespace System.Web.Util
{
	// Token: 0x020001EB RID: 491
	[Guid("a1cca730-0e36-4870-aa7d-ca39c211f99d")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IManagedContext
	{
		// Token: 0x06001809 RID: 6153
		[return: MarshalAs(UnmanagedType.I4)]
		int Context_IsPresent();

		// Token: 0x0600180A RID: 6154
		void Application_Lock();

		// Token: 0x0600180B RID: 6155
		void Application_UnLock();

		// Token: 0x0600180C RID: 6156
		[return: MarshalAs(UnmanagedType.BStr)]
		string Application_GetContentsNames();

		// Token: 0x0600180D RID: 6157
		[return: MarshalAs(UnmanagedType.BStr)]
		string Application_GetStaticNames();

		// Token: 0x0600180E RID: 6158
		object Application_GetContentsObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x0600180F RID: 6159
		void Application_SetContentsObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] object obj);

		// Token: 0x06001810 RID: 6160
		void Application_RemoveContentsObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x06001811 RID: 6161
		void Application_RemoveAllContentsObjects();

		// Token: 0x06001812 RID: 6162
		object Application_GetStaticObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x06001813 RID: 6163
		[return: MarshalAs(UnmanagedType.BStr)]
		string Request_GetAsString([MarshalAs(UnmanagedType.I4)] [In] int what);

		// Token: 0x06001814 RID: 6164
		[return: MarshalAs(UnmanagedType.BStr)]
		string Request_GetCookiesAsString();

		// Token: 0x06001815 RID: 6165
		[return: MarshalAs(UnmanagedType.I4)]
		int Request_GetTotalBytes();

		// Token: 0x06001816 RID: 6166
		[return: MarshalAs(UnmanagedType.I4)]
		int Request_BinaryRead([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] bytes, int size);

		// Token: 0x06001817 RID: 6167
		[return: MarshalAs(UnmanagedType.BStr)]
		string Response_GetCookiesAsString();

		// Token: 0x06001818 RID: 6168
		void Response_AddCookie([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x06001819 RID: 6169
		void Response_SetCookieText([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string text);

		// Token: 0x0600181A RID: 6170
		void Response_SetCookieSubValue([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string key, [MarshalAs(UnmanagedType.LPWStr)] [In] string value);

		// Token: 0x0600181B RID: 6171
		void Response_SetCookieExpires([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.R8)] [In] double dtExpires);

		// Token: 0x0600181C RID: 6172
		void Response_SetCookieDomain([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string domain);

		// Token: 0x0600181D RID: 6173
		void Response_SetCookiePath([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string path);

		// Token: 0x0600181E RID: 6174
		void Response_SetCookieSecure([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.I4)] [In] int secure);

		// Token: 0x0600181F RID: 6175
		void Response_Write([MarshalAs(UnmanagedType.LPWStr)] [In] string text);

		// Token: 0x06001820 RID: 6176
		void Response_BinaryWrite([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] byte[] bytes, int size);

		// Token: 0x06001821 RID: 6177
		void Response_Redirect([MarshalAs(UnmanagedType.LPWStr)] [In] string url);

		// Token: 0x06001822 RID: 6178
		void Response_AddHeader([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [MarshalAs(UnmanagedType.LPWStr)] [In] string value);

		// Token: 0x06001823 RID: 6179
		void Response_Pics([MarshalAs(UnmanagedType.LPWStr)] [In] string value);

		// Token: 0x06001824 RID: 6180
		void Response_Clear();

		// Token: 0x06001825 RID: 6181
		void Response_Flush();

		// Token: 0x06001826 RID: 6182
		void Response_End();

		// Token: 0x06001827 RID: 6183
		void Response_AppendToLog([MarshalAs(UnmanagedType.LPWStr)] [In] string entry);

		// Token: 0x06001828 RID: 6184
		[return: MarshalAs(UnmanagedType.BStr)]
		string Response_GetContentType();

		// Token: 0x06001829 RID: 6185
		void Response_SetContentType([MarshalAs(UnmanagedType.LPWStr)] [In] string contentType);

		// Token: 0x0600182A RID: 6186
		[return: MarshalAs(UnmanagedType.BStr)]
		string Response_GetCharSet();

		// Token: 0x0600182B RID: 6187
		void Response_SetCharSet([MarshalAs(UnmanagedType.LPWStr)] [In] string charSet);

		// Token: 0x0600182C RID: 6188
		[return: MarshalAs(UnmanagedType.BStr)]
		string Response_GetCacheControl();

		// Token: 0x0600182D RID: 6189
		void Response_SetCacheControl([MarshalAs(UnmanagedType.LPWStr)] [In] string cacheControl);

		// Token: 0x0600182E RID: 6190
		[return: MarshalAs(UnmanagedType.BStr)]
		string Response_GetStatus();

		// Token: 0x0600182F RID: 6191
		void Response_SetStatus([MarshalAs(UnmanagedType.LPWStr)] [In] string status);

		// Token: 0x06001830 RID: 6192
		[return: MarshalAs(UnmanagedType.I4)]
		int Response_GetExpiresMinutes();

		// Token: 0x06001831 RID: 6193
		void Response_SetExpiresMinutes([MarshalAs(UnmanagedType.I4)] [In] int expiresMinutes);

		// Token: 0x06001832 RID: 6194
		[return: MarshalAs(UnmanagedType.R8)]
		double Response_GetExpiresAbsolute();

		// Token: 0x06001833 RID: 6195
		void Response_SetExpiresAbsolute([MarshalAs(UnmanagedType.R8)] [In] double dtExpires);

		// Token: 0x06001834 RID: 6196
		[return: MarshalAs(UnmanagedType.I4)]
		int Response_GetIsBuffering();

		// Token: 0x06001835 RID: 6197
		void Response_SetIsBuffering([MarshalAs(UnmanagedType.I4)] [In] int isBuffering);

		// Token: 0x06001836 RID: 6198
		[return: MarshalAs(UnmanagedType.I4)]
		int Response_IsClientConnected();

		// Token: 0x06001837 RID: 6199
		[return: MarshalAs(UnmanagedType.Interface)]
		object Server_CreateObject([MarshalAs(UnmanagedType.LPWStr)] [In] string progId);

		// Token: 0x06001838 RID: 6200
		[return: MarshalAs(UnmanagedType.BStr)]
		string Server_MapPath([MarshalAs(UnmanagedType.LPWStr)] [In] string logicalPath);

		// Token: 0x06001839 RID: 6201
		[return: MarshalAs(UnmanagedType.BStr)]
		string Server_HTMLEncode([MarshalAs(UnmanagedType.LPWStr)] [In] string str);

		// Token: 0x0600183A RID: 6202
		[return: MarshalAs(UnmanagedType.BStr)]
		string Server_URLEncode([MarshalAs(UnmanagedType.LPWStr)] [In] string str);

		// Token: 0x0600183B RID: 6203
		[return: MarshalAs(UnmanagedType.BStr)]
		string Server_URLPathEncode([MarshalAs(UnmanagedType.LPWStr)] [In] string str);

		// Token: 0x0600183C RID: 6204
		[return: MarshalAs(UnmanagedType.I4)]
		int Server_GetScriptTimeout();

		// Token: 0x0600183D RID: 6205
		void Server_SetScriptTimeout([MarshalAs(UnmanagedType.I4)] [In] int timeoutSeconds);

		// Token: 0x0600183E RID: 6206
		void Server_Execute([MarshalAs(UnmanagedType.LPWStr)] [In] string url);

		// Token: 0x0600183F RID: 6207
		void Server_Transfer([MarshalAs(UnmanagedType.LPWStr)] [In] string url);

		// Token: 0x06001840 RID: 6208
		[return: MarshalAs(UnmanagedType.I4)]
		int Session_IsPresent();

		// Token: 0x06001841 RID: 6209
		[return: MarshalAs(UnmanagedType.BStr)]
		string Session_GetID();

		// Token: 0x06001842 RID: 6210
		[return: MarshalAs(UnmanagedType.I4)]
		int Session_GetTimeout();

		// Token: 0x06001843 RID: 6211
		void Session_SetTimeout([MarshalAs(UnmanagedType.I4)] [In] int value);

		// Token: 0x06001844 RID: 6212
		[return: MarshalAs(UnmanagedType.I4)]
		int Session_GetCodePage();

		// Token: 0x06001845 RID: 6213
		void Session_SetCodePage([MarshalAs(UnmanagedType.I4)] [In] int value);

		// Token: 0x06001846 RID: 6214
		[return: MarshalAs(UnmanagedType.I4)]
		int Session_GetLCID();

		// Token: 0x06001847 RID: 6215
		void Session_SetLCID([MarshalAs(UnmanagedType.I4)] [In] int value);

		// Token: 0x06001848 RID: 6216
		void Session_Abandon();

		// Token: 0x06001849 RID: 6217
		[return: MarshalAs(UnmanagedType.BStr)]
		string Session_GetContentsNames();

		// Token: 0x0600184A RID: 6218
		[return: MarshalAs(UnmanagedType.BStr)]
		string Session_GetStaticNames();

		// Token: 0x0600184B RID: 6219
		object Session_GetContentsObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x0600184C RID: 6220
		void Session_SetContentsObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name, [In] object obj);

		// Token: 0x0600184D RID: 6221
		void Session_RemoveContentsObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name);

		// Token: 0x0600184E RID: 6222
		void Session_RemoveAllContentsObjects();

		// Token: 0x0600184F RID: 6223
		object Session_GetStaticObject([MarshalAs(UnmanagedType.LPWStr)] [In] string name);
	}
}
