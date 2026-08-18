using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200015A RID: 346
	internal class ModelScope
	{
		// Token: 0x060017F6 RID: 6134 RVA: 0x00068B95 File Offset: 0x00066D95
		internal ModelScope(TypeScope typeScope)
		{
			this.typeScope = typeScope;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x00068BBA File Offset: 0x00066DBA
		internal TypeScope TypeScope
		{
			get
			{
				return this.typeScope;
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00068BC2 File Offset: 0x00066DC2
		internal TypeModel GetTypeModel(Type type)
		{
			return this.GetTypeModel(type, true);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00068BCC File Offset: 0x00066DCC
		internal TypeModel GetTypeModel(Type type, bool directReference)
		{
			TypeModel typeModel = (TypeModel)this.models[type];
			if (typeModel != null)
			{
				return typeModel;
			}
			TypeDesc typeDesc = this.typeScope.GetTypeDesc(type, null, directReference);
			switch (typeDesc.Kind)
			{
			case TypeKind.Root:
			case TypeKind.Struct:
			case TypeKind.Class:
				typeModel = new StructModel(type, typeDesc, this);
				break;
			case TypeKind.Primitive:
				typeModel = new PrimitiveModel(type, typeDesc, this);
				break;
			case TypeKind.Enum:
				typeModel = new EnumModel(type, typeDesc, this);
				break;
			case TypeKind.Array:
			case TypeKind.Collection:
			case TypeKind.Enumerable:
				typeModel = new ArrayModel(type, typeDesc, this);
				break;
			default:
				if (!typeDesc.IsSpecial)
				{
					throw new NotSupportedException(Res.GetString("XmlUnsupportedTypeKind", new object[]
					{
						type.FullName
					}));
				}
				typeModel = new SpecialModel(type, typeDesc, this);
				break;
			}
			this.models.Add(type, typeModel);
			return typeModel;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00068C98 File Offset: 0x00066E98
		internal ArrayModel GetArrayModel(Type type)
		{
			TypeModel typeModel = (TypeModel)this.arrayModels[type];
			if (typeModel == null)
			{
				typeModel = this.GetTypeModel(type);
				if (!(typeModel is ArrayModel))
				{
					TypeDesc arrayTypeDesc = this.typeScope.GetArrayTypeDesc(type);
					typeModel = new ArrayModel(type, arrayTypeDesc, this);
				}
				this.arrayModels.Add(type, typeModel);
			}
			return (ArrayModel)typeModel;
		}

		// Token: 0x04000B16 RID: 2838
		private TypeScope typeScope;

		// Token: 0x04000B17 RID: 2839
		private Hashtable models = new Hashtable();

		// Token: 0x04000B18 RID: 2840
		private Hashtable arrayModels = new Hashtable();
	}
}
