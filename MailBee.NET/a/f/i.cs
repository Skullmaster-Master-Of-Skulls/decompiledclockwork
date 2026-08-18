using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MailBee;

namespace a.f
{
	// Token: 0x020000E0 RID: 224
	internal class i
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00021CE0 File Offset: 0x00020CE0
		public static StringDictionary a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			StringDictionary stringDictionary = new StringDictionary();
			for (int i = 0; i < A_0.Count; i++)
			{
				string key = null;
				if (A_0[i] != null)
				{
					try
					{
						key = ((ao)A_0[i]).a(A_1);
					}
					catch
					{
						return null;
					}
					try
					{
						stringDictionary.Add(key, string.Empty);
					}
					catch
					{
					}
				}
			}
			return stringDictionary;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00021D60 File Offset: 0x00020D60
		public static AuthenticationMethods a(StringDictionary A_0, SaslMethod A_1)
		{
			if (A_0 == null)
			{
				return AuthenticationMethods.None;
			}
			AuthenticationMethods authenticationMethods = AuthenticationMethods.None;
			foreach (object obj in A_0.Keys)
			{
				string text = (string)obj;
				if (text.Length > 5 && text.Substring(0, 5) == "auth=")
				{
					authenticationMethods |= SaslMethod.a(text.Substring(5), A_1);
				}
			}
			return authenticationMethods;
		}
	}
}
