using System;
using System.Collections;
using System.Text;
using MailBee.ImapMail;

namespace a.f
{
	// Token: 0x020000F5 RID: 245
	internal class k : n
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x000258B6 File Offset: 0x000248B6
		public static k a()
		{
			if (k.a == null)
			{
				k.a = new k();
			}
			return k.a;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000258D0 File Offset: 0x000248D0
		public override int j9(string A_0, object A_1)
		{
			if (A_0 == "ALERT" || A_0 == "PARSE" || A_0 == "READ-ONLY" || A_0 == "READ-WRITE" || A_0 == "TRYCREATE")
			{
				return 0;
			}
			if (A_0 == "BADCHARSET")
			{
				if (!(A_1 is ArrayList))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				if (A_0 == "CAPABILITY")
				{
					return -1;
				}
				if (A_0 == "APPENDUID")
				{
					return 2;
				}
				if (A_0 == "COPYUID")
				{
					return 3;
				}
				return 1;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00025967 File Offset: 0x00024967
		public void a(Hashtable A_0, string A_1)
		{
			if (A_0.ContainsKey("ALERT"))
			{
				A_0["ALERT"] = A_1;
			}
			if (A_0.ContainsKey("PARSE"))
			{
				A_0["PARSE"] = A_1;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002599C File Offset: 0x0002499C
		public override object ka(string A_0, object A_1, Encoding A_2)
		{
			if (A_0 == "BADCHARSET")
			{
				return A_1;
			}
			if (A_0 == "CAPABILITY")
			{
				return A_1;
			}
			if (!(A_0 == "PERMANENTFLAGS"))
			{
				if (!(A_0 == "UIDNEXT"))
				{
					if (!(A_0 == "UIDVALIDITY"))
					{
						if (!(A_0 == "UNSEEN"))
						{
							return A_1;
						}
						goto IL_B4;
					}
				}
				else
				{
					try
					{
						return long.Parse(((ao)A_1).a(Encoding.ASCII));
					}
					catch
					{
						return -1;
					}
				}
				try
				{
					return long.Parse(((ao)A_1).a(Encoding.ASCII));
				}
				catch
				{
					return -1;
				}
				IL_B4:
				try
				{
					return int.Parse(((ao)A_1).a(Encoding.ASCII));
				}
				catch
				{
					return -1;
				}
				return A_1;
			}
			return MessageFlagSet.a(A_1 as ArrayList, A_2);
		}

		// Token: 0x04000563 RID: 1379
		private static k a;
	}
}
