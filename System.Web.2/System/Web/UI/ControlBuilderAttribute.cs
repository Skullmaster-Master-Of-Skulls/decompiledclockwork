using System;

namespace System.Web.UI
{
	// Token: 0x02000261 RID: 609
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ControlBuilderAttribute : Attribute
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x0005EB4B File Offset: 0x0005CD4B
		public ControlBuilderAttribute(Type builderType)
		{
			this.builderType = builderType;
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0005EB5A File Offset: 0x0005CD5A
		public Type BuilderType
		{
			get
			{
				return this.builderType;
			}
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x0005EB62 File Offset: 0x0005CD62
		public override int GetHashCode()
		{
			if (!(this.BuilderType != null))
			{
				return 0;
			}
			return this.BuilderType.GetHashCode();
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0005EB7F File Offset: 0x0005CD7F
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is ControlBuilderAttribute && ((ControlBuilderAttribute)obj).BuilderType == this.builderType);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0005EBAA File Offset: 0x0005CDAA
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ControlBuilderAttribute.Default);
		}

		// Token: 0x0400193C RID: 6460
		public static readonly ControlBuilderAttribute Default = new ControlBuilderAttribute(null);

		// Token: 0x0400193D RID: 6461
		private Type builderType;
	}
}
