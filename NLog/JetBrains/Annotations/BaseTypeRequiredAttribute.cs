using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000A RID: 10
	[BaseTypeRequired(typeof(Attribute))]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	internal sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000021A3 File Offset: 0x000003A3
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021B2 File Offset: 0x000003B2
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021BA File Offset: 0x000003BA
		[NotNull]
		public Type BaseType { get; private set; }
	}
}
