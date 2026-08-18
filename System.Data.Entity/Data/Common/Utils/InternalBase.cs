using System;
using System.Text;

namespace System.Data.Common.Utils
{
	// Token: 0x02000394 RID: 916
	internal abstract class InternalBase
	{
		// Token: 0x0600329F RID: 12959
		internal abstract void ToCompactString(StringBuilder builder);

		// Token: 0x060032A0 RID: 12960 RVA: 0x000C5C9C File Offset: 0x000C3E9C
		internal virtual void ToFullString(StringBuilder builder)
		{
			this.ToCompactString(builder);
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000C5CA8 File Offset: 0x000C3EA8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToCompactString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000C5CC8 File Offset: 0x000C3EC8
		internal virtual string ToFullString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToFullString(stringBuilder);
			return stringBuilder.ToString();
		}
	}
}
