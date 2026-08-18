using System;
using System.Runtime.CompilerServices;

namespace Spire.License.V1_0
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class Product
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00008118 File Offset: 0x00006318
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000815C File Offset: 0x0000635C
		public string Name
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.a;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.a = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000081A0 File Offset: 0x000063A0
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000081E4 File Offset: 0x000063E4
		public string Version
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.b;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.b = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00008228 File Offset: 0x00006428
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000826C File Offset: 0x0000646C
		public LicenseSubscription Subscription
		{
			[CompilerGenerated]
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.c;
			}
			[CompilerGenerated]
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.c = value;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000082C4 File Offset: 0x000064C4
		internal static string b(string A_0, int A_1)
		{
			char[] array = A_0.ToCharArray();
			int num = 1822219167 + A_1;
			int num3;
			int num2;
			if ((num2 = (num3 = 0)) < 1)
			{
				goto IL_47;
			}
			IL_14:
			int num5;
			int num4 = num5 = num2;
			char[] array2 = array;
			int num6 = num5;
			char c = array[num5];
			byte b = (byte)((int)(c & 'ÿ') ^ num++);
			byte b2 = (byte)((int)(c >> 8) ^ num++);
			byte b3 = b2;
			b2 = b;
			b = b3;
			array2[num6] = (ushort)((int)b2 << 8 | (int)b);
			num3 = num4 + 1;
			IL_47:
			if ((num2 = num3) >= array.Length)
			{
				return string.Intern(new string(array));
			}
			goto IL_14;
		}

		// Token: 0x04000082 RID: 130
		private string \u25D8\u0097\u009Bª;

		// Token: 0x04000083 RID: 131
		private string \u25D9\u0087\u00AE\u009F;

		// Token: 0x04000084 RID: 132
		private float \u2593\u00AC\u0088\u009D;

		// Token: 0x04000085 RID: 133
		private int[] \u25D9\u0091\u0090\u00AD;

		// Token: 0x04000086 RID: 134
		private string \u25D8\u00A8\u009D\u008B;

		// Token: 0x04000087 RID: 135
		private float \u2609\u00AB\u0083\u00B0;

		// Token: 0x04000088 RID: 136
		[CompilerGenerated]
		private string a;

		// Token: 0x04000089 RID: 137
		[CompilerGenerated]
		private string b;

		// Token: 0x0400008A RID: 138
		[CompilerGenerated]
		private LicenseSubscription c;
	}
}
