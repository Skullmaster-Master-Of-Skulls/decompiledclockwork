using System;
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200006A RID: 106
	public class SelectedProperty
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000690C File Offset: 0x00004B0C
		public SelectedProperty(PropertyInfo property, IDependencyResolverPolicy resolver)
		{
			this.property = property;
			this.resolver = resolver;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00006922 File Offset: 0x00004B22
		public PropertyInfo Property
		{
			get
			{
				return this.property;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000692A File Offset: 0x00004B2A
		public IDependencyResolverPolicy Resolver
		{
			get
			{
				return this.resolver;
			}
		}

		// Token: 0x04000067 RID: 103
		private readonly PropertyInfo property;

		// Token: 0x04000068 RID: 104
		private readonly IDependencyResolverPolicy resolver;
	}
}
