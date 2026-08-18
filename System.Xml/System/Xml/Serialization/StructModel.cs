using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002DA RID: 730
	internal class StructModel : TypeModel
	{
		// Token: 0x06002251 RID: 8785 RVA: 0x000A09F6 File Offset: 0x0009F9F6
		internal StructModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000A0A04 File Offset: 0x0009FA04
		internal MemberInfo[] GetMemberInfos()
		{
			MemberInfo[] members = base.Type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			MemberInfo[] array = new MemberInfo[members.Length];
			int num = 0;
			for (int i = 0; i < members.Length; i++)
			{
				if ((members[i].MemberType & MemberTypes.Property) == (MemberTypes)0)
				{
					array[num++] = members[i];
				}
			}
			for (int j = 0; j < members.Length; j++)
			{
				if ((members[j].MemberType & MemberTypes.Property) != (MemberTypes)0)
				{
					array[num++] = members[j];
				}
			}
			return array;
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000A0A7C File Offset: 0x0009FA7C
		internal FieldModel GetFieldModel(MemberInfo memberInfo)
		{
			FieldModel fieldModel = null;
			if (memberInfo is FieldInfo)
			{
				fieldModel = this.GetFieldModel((FieldInfo)memberInfo);
			}
			else if (memberInfo is PropertyInfo)
			{
				fieldModel = this.GetPropertyModel((PropertyInfo)memberInfo);
			}
			if (fieldModel != null && fieldModel.ReadOnly && fieldModel.FieldTypeDesc.Kind != TypeKind.Collection && fieldModel.FieldTypeDesc.Kind != TypeKind.Enumerable)
			{
				return null;
			}
			return fieldModel;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000A0AE4 File Offset: 0x0009FAE4
		private void CheckSupportedMember(TypeDesc typeDesc, MemberInfo member, Type type)
		{
			if (typeDesc == null)
			{
				return;
			}
			if (typeDesc.IsUnsupported)
			{
				if (typeDesc.Exception == null)
				{
					typeDesc.Exception = new NotSupportedException(Res.GetString("XmlSerializerUnsupportedType", new object[]
					{
						typeDesc.FullName
					}));
				}
				throw new InvalidOperationException(Res.GetString("XmlSerializerUnsupportedMember", new object[]
				{
					member.DeclaringType.FullName + "." + member.Name,
					type.FullName
				}), typeDesc.Exception);
			}
			this.CheckSupportedMember(typeDesc.BaseTypeDesc, member, type);
			this.CheckSupportedMember(typeDesc.ArrayElementTypeDesc, member, type);
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x000A0B8C File Offset: 0x0009FB8C
		private FieldModel GetFieldModel(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsStatic)
			{
				return null;
			}
			if (fieldInfo.DeclaringType != base.Type)
			{
				return null;
			}
			TypeDesc typeDesc = base.ModelScope.TypeScope.GetTypeDesc(fieldInfo.FieldType, fieldInfo, true, false);
			if (fieldInfo.IsInitOnly && typeDesc.Kind != TypeKind.Collection && typeDesc.Kind != TypeKind.Enumerable)
			{
				return null;
			}
			this.CheckSupportedMember(typeDesc, fieldInfo, fieldInfo.FieldType);
			return new FieldModel(fieldInfo, fieldInfo.FieldType, typeDesc);
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000A0C04 File Offset: 0x0009FC04
		private FieldModel GetPropertyModel(PropertyInfo propertyInfo)
		{
			if (propertyInfo.DeclaringType != base.Type)
			{
				return null;
			}
			if (!StructModel.CheckPropertyRead(propertyInfo))
			{
				return null;
			}
			TypeDesc typeDesc = base.ModelScope.TypeScope.GetTypeDesc(propertyInfo.PropertyType, propertyInfo, true, false);
			if (!propertyInfo.CanWrite && typeDesc.Kind != TypeKind.Collection && typeDesc.Kind != TypeKind.Enumerable)
			{
				return null;
			}
			this.CheckSupportedMember(typeDesc, propertyInfo, propertyInfo.PropertyType);
			return new FieldModel(propertyInfo, propertyInfo.PropertyType, typeDesc);
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000A0C7C File Offset: 0x0009FC7C
		internal static bool CheckPropertyRead(PropertyInfo propertyInfo)
		{
			if (!propertyInfo.CanRead)
			{
				return false;
			}
			MethodInfo getMethod = propertyInfo.GetGetMethod();
			if (getMethod.IsStatic)
			{
				return false;
			}
			ParameterInfo[] parameters = getMethod.GetParameters();
			return parameters.Length <= 0;
		}
	}
}
