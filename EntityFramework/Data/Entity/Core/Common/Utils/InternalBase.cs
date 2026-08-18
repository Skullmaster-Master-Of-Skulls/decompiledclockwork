using System;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x02000328 RID: 808
	internal abstract class InternalBase
	{
		// Token: 0x06001BD4 RID: 7124
		internal abstract void ToCompactString(StringBuilder builder);

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00088ED7 File Offset: 0x000870D7
		internal virtual void ToFullString(StringBuilder builder)
		{
			this.ToCompactString(builder);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00088EE0 File Offset: 0x000870E0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00088F00 File Offset: 0x00087100
		internal virtual string ToFullString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToFullString(stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
