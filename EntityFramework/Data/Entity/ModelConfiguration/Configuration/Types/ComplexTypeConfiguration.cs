using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Types
{
	// Token: 0x020007DF RID: 2015
	internal class ComplexTypeConfiguration : StructuralTypeConfiguration
	{
		// Token: 0x06005BAA RID: 23466 RVA: 0x0018A454 File Offset: 0x00188654
		internal ComplexTypeConfiguration(Type structuralType) : base(structuralType)
		{
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x0018A45D File Offset: 0x0018865D
		private ComplexTypeConfiguration(ComplexTypeConfiguration source) : base(source)
		{
		}

		// Token: 0x06005BAC RID: 23468 RVA: 0x0018A466 File Offset: 0x00188666
		internal virtual ComplexTypeConfiguration Clone()
		{
			return new ComplexTypeConfiguration(this);
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x0018A46E File Offset: 0x0018866E
		internal virtual void Configure(ComplexType complexType)
		{
			base.Configure(complexType.Name, complexType.Properties, complexType.GetMetadataProperties());
		}
	}
}
