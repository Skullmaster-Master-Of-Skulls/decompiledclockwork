using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	internal sealed class UsedImplicitlyAttribute : Attribute
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000021C3 File Offset: 0x000003C3
		public UsedImplicitlyAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000021CD File Offset: 0x000003CD
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000021D7 File Offset: 0x000003D7
		public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000021E1 File Offset: 0x000003E1
		public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021F7 File Offset: 0x000003F7
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000021FF File Offset: 0x000003FF
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002208 File Offset: 0x00000408
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002210 File Offset: 0x00000410
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
