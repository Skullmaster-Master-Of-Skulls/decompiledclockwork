using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.WebServices.Data;

namespace TechnoPro.Common.DAO.Exchange.Impl.Adapters
{
	// Token: 0x02000008 RID: 8
	public static class ExchangeMessageBodyAdapter
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00006C58 File Offset: 0x00004E58
		public static string GetMemoPlainText(this MessageBody MessageBody)
		{
			bool flag = MessageBody.BodyType == BodyType.HTML && !string.IsNullOrEmpty(MessageBody.Text);
			string result;
			if (flag)
			{
				result = ExchangeMessageBodyAdapter.StripHTML(MessageBody.Text);
			}
			else
			{
				result = MessageBody.Text;
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00006C9C File Offset: 0x00004E9C
		private static string StripHTML(string source)
		{
			string result;
			try
			{
				string text = source.Replace("\r", " ");
				text = text.Replace("\n", " ");
				text = text.Replace("\t", string.Empty);
				text = Regex.Replace(text, "( )+", " ");
				text = Regex.Replace(text, "<( )*head([^>])*>", "<head>", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(<( )*(/)( )*head( )*>)", "</head>", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(<head>).*(</head>)", string.Empty, RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*script([^>])*>", "<script>", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(<( )*(/)( )*script( )*>)", "</script>", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(<script>).*(</script>)", string.Empty, RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*style([^>])*>", "<style>", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(<( )*(/)( )*style( )*>)", "</style>", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(<style>).*(</style>)", string.Empty, RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*td([^>])*>", "\t", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*br( )*>", "\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*li( )*>", "\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*div([^>])*>", "\r\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*tr([^>])*>", "\r\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<( )*p([^>])*>", "\r\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
				text = Regex.Replace(text, " ", " ", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&bull;", " * ", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&lsaquo;", "<", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&rsaquo;", ">", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&trade;", "(tm)", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&frasl;", "/", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&lt;", "<", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&gt;", ">", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&copy;", "(c)", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&reg;", "(r)", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "&(.{2,6});", string.Empty, RegexOptions.IgnoreCase);
				text = text.Replace("\n", "\r");
				text = Regex.Replace(text, "(\r)( )+(\r)", "\r\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(\t)( )+(\t)", "\t\t", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(\t)( )+(\r)", "\t\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(\r)( )+(\t)", "\r\t", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(\r)(\t)+(\r)", "\r\r", RegexOptions.IgnoreCase);
				text = Regex.Replace(text, "(\r)(\t)+", "\r\t", RegexOptions.IgnoreCase);
				string text2 = "\r\r\r";
				string text3 = "\t\t\t\t\t";
				for (int i = 0; i < text.Length; i++)
				{
					text = text.Replace(text2, "\r\r");
					text = text.Replace(text3, "\t\t\t\t");
					text2 += "\r";
					text3 += "\t";
				}
				result = text;
			}
			catch
			{
				result = source;
			}
			return result;
		}
	}
}
