using System;
using System.Text;

namespace a.f
{
	// Token: 0x020000F7 RID: 247
	internal class u : n
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x00025AE2 File Offset: 0x00024AE2
		public static u a()
		{
			if (u.a == null)
			{
				u.a = new u();
			}
			return u.a;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00025AFA File Offset: 0x00024AFA
		public override int j9(string A_0, object A_1)
		{
			return 1;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00025B00 File Offset: 0x00024B00
		public override object ka(string A_0, object A_1, Encoding A_2)
		{
			if (!(A_0 == "MESSAGES"))
			{
				if (!(A_0 == "RECENT"))
				{
					if (A_0 == "UNSEEN")
					{
						goto IL_A5;
					}
					if (A_0 == "UIDNEXT")
					{
						goto IL_CD;
					}
					if (!(A_0 == "UIDVALIDITY"))
					{
						return A_1;
					}
					goto IL_F5;
				}
			}
			else
			{
				try
				{
					return int.Parse(((ao)A_1).a(Encoding.ASCII));
				}
				catch
				{
					return -1;
				}
			}
			try
			{
				return int.Parse(((ao)A_1).a(Encoding.ASCII));
			}
			catch
			{
				return -1;
			}
			IL_A5:
			try
			{
				return int.Parse(((ao)A_1).a(Encoding.ASCII));
			}
			catch
			{
				return -1;
			}
			IL_CD:
			try
			{
				return long.Parse(((ao)A_1).a(Encoding.ASCII));
			}
			catch
			{
				return -1;
			}
			IL_F5:
			try
			{
				return long.Parse(((ao)A_1).a(Encoding.ASCII));
			}
			catch
			{
				return -1;
			}
			return A_1;
		}

		// Token: 0x04000565 RID: 1381
		private static u a;
	}
}
