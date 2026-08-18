using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Spire.License.V1_0;

namespace Spire.License
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class PackageAttribute : Attribute
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00007188 File Offset: 0x00005388
		public PackageAttribute(string name, string version)
		{
			int a_ = 7;
			base..ctor();
			this.Name = name;
			this.Version = version;
			Match match = global::a.a.Match(version);
			if (match != null)
			{
				if (match.Success)
				{
					this.a = int.Parse(match.Groups[1].Value);
					this.b = int.Parse(match.Groups[2].Value);
					return;
				}
			}
			throw new ArgumentException(Product.b("잨\uddaa첬쎮\ud8b0ힲ閴솶\udcb8즺캼횾껀귂ꃈ뿊볎말볒ꃔ믖뷘ﯚ뇜뛞諠蛢엤훦쟨\ud9ea췬胮菰폲쓴\ud9f6쯸헺컼\udefe", a_), Product.b("톦첨\ud9aa\udeac욮\udeb0\uddb2", a_));
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00007228 File Offset: 0x00005428
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000726C File Offset: 0x0000546C
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000072B0 File Offset: 0x000054B0
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000072F4 File Offset: 0x000054F4
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
				return this.d;
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
				this.d = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00007338 File Offset: 0x00005538
		internal int MajorVersion
		{
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
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000737C File Offset: 0x0000557C
		internal int MinorVersion
		{
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
				return this.b;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000073C0 File Offset: 0x000055C0
		public static PackageAttribute[] GetPackage(Assembly assembly)
		{
			int a_ = 13;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (PackageAttribute[])Attribute.GetCustomAttributes(assembly, Type.GetType(Product.b("ﺬ\udfae\ud8b0솲킴馶튺\udebc\udabe꿀냂ꃄ駈꫊껌꓎냐듒냔雖귘꿚꿜뛞菠離釤苦", a_)));
		}

		// Token: 0x04000061 RID: 97
		private int a;

		// Token: 0x04000062 RID: 98
		private int[] \u25D8\u00A2\u0098\u0091;

		// Token: 0x04000063 RID: 99
		private int b;

		// Token: 0x04000064 RID: 100
		private bool \u2593\u008D\u00A2\u008A;

		// Token: 0x04000065 RID: 101
		private int \u2460\u00AB\u00B0\u009D;

		// Token: 0x04000066 RID: 102
		private int \u25D9\u00A8\u0099\u0087;

		// Token: 0x04000067 RID: 103
		[CompilerGenerated]
		private string c;

		// Token: 0x04000068 RID: 104
		[CompilerGenerated]
		private string d;
	}
}
