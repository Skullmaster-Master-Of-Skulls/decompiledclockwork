using System;
using System.Collections.Specialized;
using MailBee;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x02000471 RID: 1137
	internal class b
	{
		// Token: 0x06002762 RID: 10082 RVA: 0x000B667E File Offset: 0x000B567E
		private b()
		{
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x000B6686 File Offset: 0x000B5686
		public static ExtendedSmtpOptions a()
		{
			if (!Global.SafeMode)
			{
				return ExtendedSmtpOptions.Default;
			}
			return ExtendedSmtpOptions.NoChunking | ExtendedSmtpOptions.NoDsn | ExtendedSmtpOptions.NoSize;
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x000B6694 File Offset: 0x000B5694
		public static StringDictionary a(string[] A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			StringDictionary stringDictionary = new StringDictionary();
			int i = 0;
			while (i < A_0.Length)
			{
				string text = r.a(A_0[i]);
				int num = text.IndexOf(' ');
				if (i != 0)
				{
					goto IL_40;
				}
				int num2 = text.IndexOf('.');
				if (num2 <= 0 || (num >= 0 && num2 >= num))
				{
					goto IL_40;
				}
				IL_6F:
				i++;
				continue;
				IL_40:
				try
				{
					if (num < 0)
					{
						stringDictionary.Add(text, "");
					}
					else
					{
						stringDictionary.Add(text.Substring(0, num), text.Substring(num + 1));
					}
				}
				catch (ArgumentException)
				{
				}
				goto IL_6F;
			}
			return stringDictionary;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x000B672C File Offset: 0x000B572C
		public static AuthenticationMethods a(string A_0, SaslMethod A_1)
		{
			if (A_0 == null)
			{
				return AuthenticationMethods.None;
			}
			string[] array = A_0.Split(null);
			AuthenticationMethods authenticationMethods = AuthenticationMethods.None;
			for (int i = 0; i < array.Length; i++)
			{
				authenticationMethods |= SaslMethod.a(array[i], A_1);
			}
			return authenticationMethods;
		}

		// Token: 0x04001AEA RID: 6890
		public static string a = "BINARYMIME";

		// Token: 0x04001AEB RID: 6891
		public static string b = "8BITMIME";

		// Token: 0x04001AEC RID: 6892
		public static string c = "DSN";

		// Token: 0x04001AED RID: 6893
		public static string d = "PIPELINING";

		// Token: 0x04001AEE RID: 6894
		public static string e = "CHUNKING";

		// Token: 0x04001AEF RID: 6895
		public static string f = "SIZE";

		// Token: 0x04001AF0 RID: 6896
		public static string g = "AUTH";
	}
}
