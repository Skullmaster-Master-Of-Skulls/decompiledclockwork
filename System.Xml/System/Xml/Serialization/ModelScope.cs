using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002D5 RID: 725
	internal class ModelScope
	{
		// Token: 0x06002244 RID: 8772 RVA: 0x000A0824 File Offset: 0x0009F824
		internal ModelScope(TypeScope typeScope)
		{
			this.typeScope = typeScope;
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000A0849 File Offset: 0x0009F849
		internal TypeScope TypeScope
		{
			get
			{
				return this.typeScope;
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000A0851 File Offset: 0x0009F851
		internal TypeModel GetTypeModel(Type type)
		{
			return this.GetTypeModel(type, true);
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000A085C File Offset: 0x0009F85C
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

		// Token: 0x06002248 RID: 8776 RVA: 0x000A092C File Offset: 0x0009F92C
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

		// Token: 0x040014AD RID: 5293
		private TypeScope typeScope;

		// Token: 0x040014AE RID: 5294
		private Hashtable models = new Hashtable();

		// Token: 0x040014AF RID: 5295
		private Hashtable arrayModels = new Hashtable();
	}
}
