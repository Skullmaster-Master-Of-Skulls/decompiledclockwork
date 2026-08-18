using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.ObjectBuilder
{
	// Token: 0x02000095 RID: 149
	public class NamedTypeDependencyResolverPolicy : IDependencyResolverPolicy, IBuilderPolicy
	{
		// Token: 0x060002AF RID: 687 RVA: 0x000088F9 File Offset: 0x00006AF9
		public NamedTypeDependencyResolverPolicy(Type type, string name)
		{
			this.type = type;
			this.name = name;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000890F File Offset: 0x00006B0F
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation is done by Guard class.")]
		public object Resolve(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			return context.NewBuildUp(new NamedTypeBuildKey(this.type, this.name));
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00008933 File Offset: 0x00006B33
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This is the type part of the key.")]
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000893B File Offset: 0x00006B3B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000094 RID: 148
		private Type type;

		// Token: 0x04000095 RID: 149
		private string name;
	}
}
