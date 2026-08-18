using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x0200015F RID: 351
	internal class StructModel : TypeModel
	{
		// Token: 0x06001803 RID: 6147 RVA: 0x00068D62 File Offset: 0x00066F62
		internal StructModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00068D70 File Offset: 0x00066F70
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

		// Token: 0x06001805 RID: 6149 RVA: 0x00068DE8 File Offset: 0x00066FE8
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

		// Token: 0x06001806 RID: 6150 RVA: 0x00068E50 File Offset: 0x00067050
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

		// Token: 0x06001807 RID: 6151 RVA: 0x00068EF4 File Offset: 0x000670F4
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

		// Token: 0x06001808 RID: 6152 RVA: 0x00068F74 File Offset: 0x00067174
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

		// Token: 0x06001809 RID: 6153 RVA: 0x00068FF4 File Offset: 0x000671F4
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
			return parameters.Length == 0;
		}
	}
}
