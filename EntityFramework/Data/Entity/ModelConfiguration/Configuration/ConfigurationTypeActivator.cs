using System;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002AD RID: 685
	internal class ConfigurationTypeActivator
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x000798EC File Offset: 0x00077AEC
		public virtual TStructuralTypeConfiguration Activate<TStructuralTypeConfiguration>(Type type) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			if (type.GetDeclaredConstructor(new Type[0]) == null)
			{
				throw new InvalidOperationException(Strings.CreateConfigurationType_NoParameterlessConstructor(type.Name));
			}
			return (TStructuralTypeConfiguration)((object)typeof(StructuralTypeConfiguration<>).MakeGenericType(new Type[]
			{
				type.TryGetElementType(typeof(StructuralTypeConfiguration<>))
			}).GetDeclaredProperty("Configuration").GetValue(Activator.CreateInstance(type, true), null));
		}
	}
}
