using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200001C RID: 28
	internal class OpoErrResManager
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000101F7 File Offset: 0x0000F1F7
		private OpoErrResManager()
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000101FF File Offset: 0x0000F1FF
		internal static ResourceManager Instance()
		{
			return OpoErrResManager.s_rm;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00010208 File Offset: 0x0000F208
		internal static string GetErrorMesg(int errorcode, params string[] args)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			string @string = OpoErrResManager.Instance().GetString(Convert.ToString(errorcode), currentCulture);
			string text;
			if (@string != null)
			{
				text = string.Format(@string, args);
			}
			else
			{
				text = string.Empty;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new object[]
					{
						" (ERROR) ODP error code=",
						errorcode,
						"; ODP message=",
						text,
						"\n"
					})
				});
			}
			return text;
		}

		// Token: 0x040000B2 RID: 178
		private static ResourceManager s_rm = new ResourceManager("Oracle.DataAccess.src.Client.Resources.Exception", Assembly.GetExecutingAssembly());
	}
}
