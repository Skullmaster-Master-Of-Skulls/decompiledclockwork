using System;
using System.Globalization;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000022 RID: 34
	internal class NameGenerator
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00005738 File Offset: 0x00003938
		private NameGenerator()
		{
			this.prefix = "_" + Guid.NewGuid().ToString().Replace('-', '_') + "_";
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000577C File Offset: 0x0000397C
		public static string Next()
		{
			long num = Interlocked.Increment(ref NameGenerator.nameGenerator.id);
			return NameGenerator.nameGenerator.prefix + num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0400008A RID: 138
		private static NameGenerator nameGenerator = new NameGenerator();

		// Token: 0x0400008B RID: 139
		private long id;

		// Token: 0x0400008C RID: 140
		private string prefix;
	}
}
