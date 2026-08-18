using System;

namespace System.Data.Entity
{
	// Token: 0x02000736 RID: 1846
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class DbModelBuilderVersionAttribute : Attribute
	{
		// Token: 0x06005395 RID: 21397 RVA: 0x0016FEBE File Offset: 0x0016E0BE
		public DbModelBuilderVersionAttribute(DbModelBuilderVersion version)
		{
			if (!Enum.IsDefined(typeof(DbModelBuilderVersion), version))
			{
				throw new ArgumentOutOfRangeException("version");
			}
			this.Version = version;
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06005396 RID: 21398 RVA: 0x0016FEEF File Offset: 0x0016E0EF
		// (set) Token: 0x06005397 RID: 21399 RVA: 0x0016FEF7 File Offset: 0x0016E0F7
		public DbModelBuilderVersion Version { get; private set; }
	}
}
