using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace OracleInternal.I18N
{
	// Token: 0x020000FA RID: 250
	internal class I18nStringResourceManager
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x00075B84 File Offset: 0x00073D84
		static I18nStringResourceManager()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assembly assembly = null;
			foreach (Assembly assembly2 in assemblies)
			{
				if (assembly2.GetName().Name.Equals("Oracle.ManagedDataAccess"))
				{
					assembly = assembly2;
					break;
				}
			}
			I18nStringResourceManager.s_rm = new ResourceManager("Oracle.ManagedDataAccess.src.Client.Resources.Exception", assembly);
			I18nStringResourceManager.resourceStringConstants = assembly.GetType("Oracle.ManagedDataAccess.Client.ResourceStringConstants");
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00075BF4 File Offset: 0x00073DF4
		private I18nStringResourceManager()
		{
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00075BFC File Offset: 0x00073DFC
		internal static ResourceManager Instance()
		{
			return I18nStringResourceManager.s_rm;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00075C04 File Offset: 0x00073E04
		internal static string GetErrorMesg(string errorcode, params string[] args)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			int value = (int)I18nStringResourceManager.resourceStringConstants.GetField(errorcode, BindingFlags.Static | BindingFlags.NonPublic).GetValue(I18nStringResourceManager.resourceStringConstants);
			string @string = I18nStringResourceManager.Instance().GetString(Convert.ToString(value), currentCulture);
			string result;
			if (@string != null)
			{
				result = string.Format(@string, args);
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00075C64 File Offset: 0x00073E64
		internal static string GetErrorMesgWithErrCode(string errorcode, params string[] args)
		{
			string errorMesg = I18nStringResourceManager.GetErrorMesg(errorcode, args);
			return string.Format("ORA-{0}: {1}", errorcode, errorMesg);
		}

		// Token: 0x04000CA6 RID: 3238
		private static ResourceManager s_rm;

		// Token: 0x04000CA7 RID: 3239
		internal static Type resourceStringConstants;
	}
}
