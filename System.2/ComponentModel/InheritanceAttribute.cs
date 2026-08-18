using System;

namespace System.ComponentModel
{
	// Token: 0x020005BD RID: 1469
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class InheritanceAttribute : Attribute
	{
		// Token: 0x0600371D RID: 14109 RVA: 0x000EFDE8 File Offset: 0x000EDFE8
		public InheritanceAttribute()
		{
			this.inheritanceLevel = InheritanceAttribute.Default.inheritanceLevel;
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000EFE00 File Offset: 0x000EE000
		public InheritanceAttribute(InheritanceLevel inheritanceLevel)
		{
			this.inheritanceLevel = inheritanceLevel;
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x0600371F RID: 14111 RVA: 0x000EFE0F File Offset: 0x000EE00F
		public InheritanceLevel InheritanceLevel
		{
			get
			{
				return this.inheritanceLevel;
			}
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000EFE18 File Offset: 0x000EE018
		public override bool Equals(object value)
		{
			if (value == this)
			{
				return true;
			}
			if (!(value is InheritanceAttribute))
			{
				return false;
			}
			InheritanceLevel inheritanceLevel = ((InheritanceAttribute)value).InheritanceLevel;
			return inheritanceLevel == this.inheritanceLevel;
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000EFE4A File Offset: 0x000EE04A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000EFE52 File Offset: 0x000EE052
		public override bool IsDefaultAttribute()
		{
			return this.Equals(InheritanceAttribute.Default);
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000EFE5F File Offset: 0x000EE05F
		public override string ToString()
		{
			return TypeDescriptor.GetConverter(typeof(InheritanceLevel)).ConvertToString(this.InheritanceLevel);
		}

		// Token: 0x04002AC7 RID: 10951
		private readonly InheritanceLevel inheritanceLevel;

		// Token: 0x04002AC8 RID: 10952
		public static readonly InheritanceAttribute Inherited = new InheritanceAttribute(InheritanceLevel.Inherited);

		// Token: 0x04002AC9 RID: 10953
		public static readonly InheritanceAttribute InheritedReadOnly = new InheritanceAttribute(InheritanceLevel.InheritedReadOnly);

		// Token: 0x04002ACA RID: 10954
		public static readonly InheritanceAttribute NotInherited = new InheritanceAttribute(InheritanceLevel.NotInherited);

		// Token: 0x04002ACB RID: 10955
		public static readonly InheritanceAttribute Default = InheritanceAttribute.NotInherited;
	}
}
