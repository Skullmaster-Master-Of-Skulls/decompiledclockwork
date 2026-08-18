using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200053F RID: 1343
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class DelegatingTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x0600329F RID: 12959 RVA: 0x000E26EF File Offset: 0x000E08EF
		internal DelegatingTypeDescriptionProvider(Type type)
		{
			this._type = type;
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000E26FE File Offset: 0x000E08FE
		internal TypeDescriptionProvider Provider
		{
			get
			{
				return TypeDescriptor.GetProviderRecursive(this._type);
			}
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000E270B File Offset: 0x000E090B
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			return this.Provider.CreateInstance(provider, objectType, argTypes, args);
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000E271D File Offset: 0x000E091D
		public override IDictionary GetCache(object instance)
		{
			return this.Provider.GetCache(instance);
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000E272B File Offset: 0x000E092B
		public override string GetFullComponentName(object component)
		{
			return this.Provider.GetFullComponentName(component);
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000E2739 File Offset: 0x000E0939
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return this.Provider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000E2747 File Offset: 0x000E0947
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			return this.Provider.GetExtenderProviders(instance);
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000E2755 File Offset: 0x000E0955
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return this.Provider.GetReflectionType(objectType, instance);
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000E2764 File Offset: 0x000E0964
		public override Type GetRuntimeType(Type objectType)
		{
			return this.Provider.GetRuntimeType(objectType);
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000E2772 File Offset: 0x000E0972
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return this.Provider.GetTypeDescriptor(objectType, instance);
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000E2781 File Offset: 0x000E0981
		public override bool IsSupportedType(Type type)
		{
			return this.Provider.IsSupportedType(type);
		}

		// Token: 0x04002986 RID: 10630
		private Type _type;
	}
}
