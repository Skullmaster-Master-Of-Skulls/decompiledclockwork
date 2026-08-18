using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000579 RID: 1401
	internal sealed class DataContractImplementor
	{
		// Token: 0x060036A9 RID: 13993 RVA: 0x00103952 File Offset: 0x00101B52
		internal DataContractImplementor(EntityType ospaceEntityType)
		{
			this._baseClrType = ospaceEntityType.ClrType;
			this._dataContract = this._baseClrType.GetCustomAttributes(false).FirstOrDefault<DataContractAttribute>();
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x00103980 File Offset: 0x00101B80
		internal void Implement(TypeBuilder typeBuilder)
		{
			if (this._dataContract != null)
			{
				object[] propertyValues = new object[]
				{
					this._dataContract.IsReference
				};
				CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(DataContractImplementor.DataContractAttributeConstructor, new object[0], DataContractImplementor.DataContractProperties, propertyValues);
				typeBuilder.SetCustomAttribute(customAttribute);
			}
		}

		// Token: 0x040014E7 RID: 5351
		internal static readonly ConstructorInfo DataContractAttributeConstructor = typeof(DataContractAttribute).GetDeclaredConstructor(new Type[0]);

		// Token: 0x040014E8 RID: 5352
		internal static readonly PropertyInfo[] DataContractProperties = new PropertyInfo[]
		{
			typeof(DataContractAttribute).GetDeclaredProperty("IsReference")
		};

		// Token: 0x040014E9 RID: 5353
		private readonly Type _baseClrType;

		// Token: 0x040014EA RID: 5354
		private readonly DataContractAttribute _dataContract;
	}
}
