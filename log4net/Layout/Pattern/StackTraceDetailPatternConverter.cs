using System;
using System.Text;
using log4net.Core;
using log4net.Util;

namespace log4net.Layout.Pattern
{
	// Token: 0x020000A1 RID: 161
	internal class StackTraceDetailPatternConverter : StackTracePatternConverter
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		internal override string GetMethodInformation(MethodItem method)
		{
			string result = "";
			try
			{
				string str = "";
				string[] parameters = method.Parameters;
				StringBuilder stringBuilder = new StringBuilder();
				if (parameters != null && parameters.GetUpperBound(0) > 0)
				{
					for (int i = 0; i <= parameters.GetUpperBound(0); i++)
					{
						stringBuilder.AppendFormat("{0}, ", parameters[i]);
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
					str = stringBuilder.ToString();
				}
				result = base.GetMethodInformation(method) + "(" + str + ")";
			}
			catch (Exception exception)
			{
				LogLog.Error(StackTraceDetailPatternConverter.declaringType, "An exception ocurred while retreiving method information.", exception);
			}
			return result;
		}

		// Token: 0x04000205 RID: 517
		private static readonly Type declaringType = typeof(StackTracePatternConverter);
	}
}
