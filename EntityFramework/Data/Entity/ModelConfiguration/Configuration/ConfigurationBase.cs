using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007A9 RID: 1961
	internal abstract class ConfigurationBase
	{
		// Token: 0x06005882 RID: 22658 RVA: 0x0017C462 File Offset: 0x0017A662
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x0017C46A File Offset: 0x0017A66A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x0017C473 File Offset: 0x0017A673
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x0017C47B File Offset: 0x0017A67B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
