using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity
{
	// Token: 0x020000DA RID: 218
	[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
	[SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class DbConfigurationTypeAttribute : Attribute
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00025038 File Offset: 0x00023238
		public DbConfigurationTypeAttribute(Type configurationType)
		{
			Check.NotNull<Type>(configurationType, "configurationType");
			this._configurationType = configurationType;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00025054 File Offset: 0x00023254
		public DbConfigurationTypeAttribute(string configurationTypeName)
		{
			Check.NotEmpty(configurationTypeName, "configurationTypeName");
			try
			{
				this._configurationType = Type.GetType(configurationTypeName, true);
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException(Strings.DbConfigurationTypeInAttributeNotFound(configurationTypeName), innerException);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000250A0 File Offset: 0x000232A0
		public Type ConfigurationType
		{
			get
			{
				return this._configurationType;
			}
		}

		// Token: 0x040001B9 RID: 441
		private readonly Type _configurationType;
	}
}
