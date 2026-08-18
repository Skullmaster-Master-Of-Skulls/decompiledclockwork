using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	internal sealed class MeansImplicitUseAttribute : Attribute
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002219 File Offset: 0x00000419
		public MeansImplicitUseAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002223 File Offset: 0x00000423
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags) : this(useKindFlags, ImplicitUseTargetFlags.Default)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000222D File Offset: 0x0000042D
		public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags) : this(ImplicitUseKindFlags.Default, targetFlags)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002237 File Offset: 0x00000437
		public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
		{
			this.UseKindFlags = useKindFlags;
			this.TargetFlags = targetFlags;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002255 File Offset: 0x00000455
		[UsedImplicitly]
		public ImplicitUseKindFlags UseKindFlags { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000225E File Offset: 0x0000045E
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002266 File Offset: 0x00000466
		[UsedImplicitly]
		public ImplicitUseTargetFlags TargetFlags { get; private set; }
	}
}
