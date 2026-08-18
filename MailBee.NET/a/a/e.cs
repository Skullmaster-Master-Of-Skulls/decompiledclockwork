using System;
using System.Collections.Specialized;
using MailBee;

namespace a.a
{
	// Token: 0x020003E3 RID: 995
	internal class e
	{
		// Token: 0x06002386 RID: 9094 RVA: 0x0009493A File Offset: 0x0009393A
		private e()
		{
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00094944 File Offset: 0x00093944
		public static StringDictionary a(string[] A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			StringDictionary stringDictionary = new StringDictionary();
			for (int i = 0; i < A_0.Length; i++)
			{
				int num = A_0[i].IndexOf(' ');
				try
				{
					if (num < 0)
					{
						stringDictionary.Add(A_0[i], "");
					}
					else
					{
						string key = A_0[i].Substring(0, num);
						if (stringDictionary.ContainsKey(key))
						{
							stringDictionary[key] += A_0[i].Substring(num);
						}
						else
						{
							stringDictionary.Add(key, A_0[i].Substring(num + 1));
						}
					}
				}
				catch (ArgumentException)
				{
				}
			}
			return stringDictionary;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000949E8 File Offset: 0x000939E8
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

		// Token: 0x06002389 RID: 9097 RVA: 0x00094A20 File Offset: 0x00093A20
		public static AuthenticationMethods a(string[] A_0, SaslMethod A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException();
			}
			AuthenticationMethods authenticationMethods = AuthenticationMethods.None;
			for (int i = 0; i < A_0.Length; i++)
			{
				authenticationMethods |= SaslMethod.a(A_0[i], A_1);
			}
			return authenticationMethods;
		}

		// Token: 0x0400176F RID: 5999
		public static string a = "PIPELINING";

		// Token: 0x04001770 RID: 6000
		public static string b = "SASL";
	}
}
