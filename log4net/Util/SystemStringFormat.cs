using System;
using System.Text;

namespace log4net.Util
{
	// Token: 0x02000117 RID: 279
	public sealed class SystemStringFormat
	{
		// Token: 0x06000838 RID: 2104 RVA: 0x0001972E File Offset: 0x0001792E
		public SystemStringFormat(IFormatProvider provider, string format, params object[] args)
		{
			this.m_provider = provider;
			this.m_format = format;
			this.m_args = args;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001974B File Offset: 0x0001794B
		public override string ToString()
		{
			return SystemStringFormat.StringFormat(this.m_provider, this.m_format, this.m_args);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00019764 File Offset: 0x00017964
		private static string StringFormat(IFormatProvider provider, string format, params object[] args)
		{
			string result;
			try
			{
				if (format == null)
				{
					result = null;
				}
				else if (args == null)
				{
					result = format;
				}
				else
				{
					result = string.Format(provider, format, args);
				}
			}
			catch (Exception ex)
			{
				LogLog.Warn(SystemStringFormat.declaringType, "Exception while rendering format [" + format + "]", ex);
				result = SystemStringFormat.StringFormatError(ex, format, args);
			}
			return result;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000197C4 File Offset: 0x000179C4
		private static string StringFormatError(Exception formatException, string format, object[] args)
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder("<log4net.Error>");
				if (formatException != null)
				{
					stringBuilder.Append("Exception during StringFormat: ").Append(formatException.Message);
				}
				else
				{
					stringBuilder.Append("Exception during StringFormat");
				}
				stringBuilder.Append(" <format>").Append(format).Append("</format>");
				stringBuilder.Append("<args>");
				SystemStringFormat.RenderArray(args, stringBuilder);
				stringBuilder.Append("</args>");
				stringBuilder.Append("</log4net.Error>");
				result = stringBuilder.ToString();
			}
			catch (Exception exception)
			{
				LogLog.Error(SystemStringFormat.declaringType, "INTERNAL ERROR during StringFormat error handling", exception);
				result = "<log4net.Error>Exception during StringFormat. See Internal Log.</log4net.Error>";
			}
			return result;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00019880 File Offset: 0x00017A80
		private static void RenderArray(Array array, StringBuilder buffer)
		{
			if (array == null)
			{
				buffer.Append(SystemInfo.NullText);
				return;
			}
			if (array.Rank != 1)
			{
				buffer.Append(array.ToString());
				return;
			}
			buffer.Append("{");
			int length = array.Length;
			if (length > 0)
			{
				SystemStringFormat.RenderObject(array.GetValue(0), buffer);
				for (int i = 1; i < length; i++)
				{
					buffer.Append(", ");
					SystemStringFormat.RenderObject(array.GetValue(i), buffer);
				}
			}
			buffer.Append("}");
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001990C File Offset: 0x00017B0C
		private static void RenderObject(object obj, StringBuilder buffer)
		{
			if (obj == null)
			{
				buffer.Append(SystemInfo.NullText);
				return;
			}
			try
			{
				buffer.Append(obj);
			}
			catch (Exception ex)
			{
				buffer.Append("<Exception: ").Append(ex.Message).Append(">");
			}
		}

		// Token: 0x040002FA RID: 762
		private readonly IFormatProvider m_provider;

		// Token: 0x040002FB RID: 763
		private readonly string m_format;

		// Token: 0x040002FC RID: 764
		private readonly object[] m_args;

		// Token: 0x040002FD RID: 765
		private static readonly Type declaringType = typeof(SystemStringFormat);
	}
}
