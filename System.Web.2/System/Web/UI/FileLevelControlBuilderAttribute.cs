using System;

namespace System.Web.UI
{
	// Token: 0x0200028A RID: 650
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FileLevelControlBuilderAttribute : Attribute
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x000621EB File Offset: 0x000603EB
		public FileLevelControlBuilderAttribute(Type builderType)
		{
			this.builderType = builderType;
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x000621FA File Offset: 0x000603FA
		public Type BuilderType
		{
			get
			{
				return this.builderType;
			}
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00062202 File Offset: 0x00060402
		public override int GetHashCode()
		{
			return this.builderType.GetHashCode();
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0006220F File Offset: 0x0006040F
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is FileLevelControlBuilderAttribute && ((FileLevelControlBuilderAttribute)obj).BuilderType == this.builderType);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0006223A File Offset: 0x0006043A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(FileLevelControlBuilderAttribute.Default);
		}

		// Token: 0x040019A2 RID: 6562
		public static readonly FileLevelControlBuilderAttribute Default = new FileLevelControlBuilderAttribute(null);

		// Token: 0x040019A3 RID: 6563
		private Type builderType;
	}
}
