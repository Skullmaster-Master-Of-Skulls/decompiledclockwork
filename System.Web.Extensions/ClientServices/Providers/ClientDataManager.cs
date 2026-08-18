using System;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x0200011A RID: 282
	internal static class ClientDataManager
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00035B69 File Offset: 0x00033D69
		internal static ClientData GetAppClientData(bool useIsolatedStore)
		{
			if (ClientDataManager._applicationClientData == null)
			{
				ClientDataManager._applicationClientData = ClientData.Load(null, useIsolatedStore);
			}
			return ClientDataManager._applicationClientData;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00035B83 File Offset: 0x00033D83
		internal static ClientData GetUserClientData(string username, bool useIsolatedStore)
		{
			if (username != ClientDataManager._curUserName)
			{
				ClientDataManager._curUserName = username;
				ClientDataManager._userClientData = ClientData.Load(username, useIsolatedStore);
			}
			return ClientDataManager._userClientData;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00035BAC File Offset: 0x00033DAC
		internal static string GetCookie(string username, string cookieName, bool useIsolatedStore)
		{
			ClientData userClientData = ClientDataManager.GetUserClientData(username, useIsolatedStore);
			if (userClientData.CookieNames == null)
			{
				userClientData.CookieNames = new string[0];
				userClientData.CookieValues = new string[0];
				return null;
			}
			for (int i = 0; i < userClientData.CookieNames.Length; i++)
			{
				if (string.Compare(cookieName, userClientData.CookieNames[i], StringComparison.OrdinalIgnoreCase) == 0)
				{
					return userClientData.CookieValues[i];
				}
			}
			return null;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00035C14 File Offset: 0x00033E14
		internal static string StoreCookie(string username, string cookieName, string cookieValue, bool useIsolatedStore)
		{
			ClientData userClientData = ClientDataManager.GetUserClientData(username, useIsolatedStore);
			if (userClientData.CookieNames == null)
			{
				userClientData.CookieNames = new string[0];
				userClientData.CookieValues = new string[0];
			}
			else
			{
				for (int i = 0; i < userClientData.CookieNames.Length; i++)
				{
					if (userClientData.CookieValues[i].StartsWith(cookieName + "=", StringComparison.OrdinalIgnoreCase))
					{
						if (userClientData.CookieValues[i] != cookieName + "=" + cookieValue)
						{
							userClientData.CookieValues[i] = cookieName + "=" + cookieValue;
							userClientData.Save();
						}
						return userClientData.CookieNames[i];
					}
				}
			}
			string text = Guid.NewGuid().ToString("N");
			string[] array = new string[userClientData.CookieNames.Length + 1];
			string[] array2 = new string[userClientData.CookieNames.Length + 1];
			userClientData.CookieNames.CopyTo(array, 0);
			userClientData.CookieValues.CopyTo(array2, 0);
			array[userClientData.CookieNames.Length] = text;
			array2[userClientData.CookieNames.Length] = cookieName + "=" + cookieValue;
			userClientData.CookieNames = array;
			userClientData.CookieValues = array2;
			userClientData.Save();
			return text;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00035D44 File Offset: 0x00033F44
		internal static void DeleteAllCookies(string username, bool useIsolatedStore)
		{
			ClientData userClientData = ClientDataManager.GetUserClientData(username, useIsolatedStore);
			userClientData.CookieNames = new string[0];
			userClientData.CookieValues = new string[0];
		}

		// Token: 0x0400042D RID: 1069
		private static ClientData _applicationClientData;

		// Token: 0x0400042E RID: 1070
		private static ClientData _userClientData;

		// Token: 0x0400042F RID: 1071
		private static string _curUserName;
	}
}
