using System;
using System.Data.Metadata.Edm;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000174 RID: 372
	internal sealed class DataContractImplementor
	{
		// Token: 0x06001B41 RID: 6977 RVA: 0x0005E400 File Offset: 0x0005C600
		internal DataContractImplementor(EntityType ospaceEntityType)
		{
			this._baseClrType = ospaceEntityType.ClrType;
			DataContractAttribute[] array = (DataContractAttribute[])this._baseClrType.GetCustomAttributes(typeof(DataContractAttribute), false);
			if (array.Length != 0)
			{
				this._dataContract = array[0];
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0005E448 File Offset: 0x0005C648
		internal void Implement(TypeBuilder typeBuilder, Action<FieldBuilder, bool> registerField)
		{
			if (this._dataContract != null)
			{
				object[] propertyValues = new object[]
				{
					this._dataContract.IsReference
				};
				CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(DataContractImplementor.s_DataContractAttributeConstructor, new object[0], DataContractImplementor.s_DataContractProperties, propertyValues);
				typeBuilder.SetCustomAttribute(customAttribute);
			}
		}

		// Token: 0x04000B61 RID: 2913
		private static readonly ConstructorInfo s_DataContractAttributeConstructor = typeof(DataContractAttribute).GetConstructor(Type.EmptyTypes);

		// Token: 0x04000B62 RID: 2914
		private static readonly PropertyInfo[] s_DataContractProperties = new PropertyInfo[]
		{
			typeof(DataContractAttribute).GetProperty("IsReference")
		};

		// Token: 0x04000B63 RID: 2915
		private readonly Type _baseClrType;

		// Token: 0x04000B64 RID: 2916
		private readonly DataContractAttribute _dataContract;
	}
}
