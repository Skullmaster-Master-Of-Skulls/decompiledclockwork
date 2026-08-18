using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Spire.License.V1_0;

namespace Spire.License
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Assembly)]
	public class ReleaseDateAttribute : Attribute
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00007040 File Offset: 0x00005240
		public ReleaseDateAttribute(string releaseDate)
		{
			this.ReleaseDate = releaseDate;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000705C File Offset: 0x0000525C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000070A0 File Offset: 0x000052A0
		public string ReleaseDate
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

		// Token: 0x0600008B RID: 139 RVA: 0x000070E4 File Offset: 0x000052E4
		public static DateTime? GetReleaseDate(Assembly assembly)
		{
			int a_ = 8;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				ReleaseDateAttribute releaseDateAttribute = (ReleaseDateAttribute)Attribute.GetCustomAttribute(assembly, Type.GetType(Product.b("ﮧ\udaa9얫\udcad햯鲱\udfb5\udbb7\udfb9튻춽ꖿ雃ꏅ꓇꿉귋뷍뗏雑뗓ꋕ뷗鯙꣛ꫝ鋟诡蛣鏥鳧迩", a_)));
				if (releaseDateAttribute != null)
				{
					try
					{
						return new DateTime?(DateTime.Parse(releaseDateAttribute.ReleaseDate));
					}
					catch (Exception)
					{
						return null;
					}
					break;
				}
				break;
			}
			}
			if (true)
			{
			}
			return null;
		}

		// Token: 0x0400005B RID: 91
		private byte \u25D8\u0093\u00A7\u007F;

		// Token: 0x0400005C RID: 92
		private byte[] \u2609\u0091\u00AB\u0092;

		// Token: 0x0400005D RID: 93
		private byte \u2609\u0082\u0085\u00A4;

		// Token: 0x0400005E RID: 94
		private long \u2460\u00A7\u00AB\u0088;

		// Token: 0x0400005F RID: 95
		private float \u25D9\u0093\u00AC\u007F;

		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		private string a;
	}
}
