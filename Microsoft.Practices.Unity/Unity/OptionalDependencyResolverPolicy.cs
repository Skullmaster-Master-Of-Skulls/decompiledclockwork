using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000028 RID: 40
	public class OptionalDependencyResolverPolicy : IDependencyResolverPolicy, IBuilderPolicy
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000031E8 File Offset: 0x000013E8
		public OptionalDependencyResolverPolicy(Type type, string name)
		{
			Guard.ArgumentNotNull(type, "type");
			if (type.GetTypeInfo().IsValueType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.OptionalDependenciesMustBeReferenceTypes, new object[]
				{
					type.GetTypeInfo().Name
				}));
			}
			this.type = type;
			this.name = name;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000324C File Offset: 0x0000144C
		public OptionalDependencyResolverPolicy(Type type) : this(type, null)
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003256 File Offset: 0x00001456
		public Type DependencyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000325E File Offset: 0x0000145E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003268 File Offset: 0x00001468
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Entire purpose of this class is to eat the exception")]
		public object Resolve(IBuilderContext context)
		{
			Guard.ArgumentNotNull(context, "context");
			NamedTypeBuildKey newBuildKey = new NamedTypeBuildKey(this.type, this.name);
			object result;
			try
			{
				result = context.NewBuildUp(newBuildKey);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0400001C RID: 28
		private readonly Type type;

		// Token: 0x0400001D RID: 29
		private readonly string name;
	}
}
