using System;
using System.Reflection;

namespace System.Web.Util
{
	// Token: 0x020001EE RID: 494
	internal static class AssemblyUtil
	{
		// Token: 0x060018B8 RID: 6328 RVA: 0x0004CA58 File Offset: 0x0004AC58
		public static string GetAssemblyFileVersion(Assembly assembly)
		{
			AssemblyFileVersionAttribute[] array = (AssemblyFileVersionAttribute[])assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
			string text;
			if (array.Length != 0)
			{
				text = array[0].Version;
				if (string.IsNullOrEmpty(text))
				{
					text = "0.0.0.0";
				}
			}
			else
			{
				text = "0.0.0.0";
			}
			return text;
		}

		// Token: 0x04001781 RID: 6017
		private const string _emptyFileVersion = "0.0.0.0";
	}
}
