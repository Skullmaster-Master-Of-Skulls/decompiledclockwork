using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000161 RID: 353
	internal class FieldModel
	{
		// Token: 0x0600180A RID: 6154 RVA: 0x0006902A File Offset: 0x0006722A
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist) : this(name, fieldType, fieldTypeDesc, checkSpecified, checkShouldPersist, false)
		{
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0006903A File Offset: 0x0006723A
		internal FieldModel(string name, Type fieldType, TypeDesc fieldTypeDesc, bool checkSpecified, bool checkShouldPersist, bool readOnly)
		{
			this.fieldTypeDesc = fieldTypeDesc;
			this.name = name;
			this.fieldType = fieldType;
			this.checkSpecified = (checkSpecified ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.None);
			this.checkShouldPersist = checkShouldPersist;
			this.readOnly = readOnly;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00069078 File Offset: 0x00067278
		internal FieldModel(MemberInfo memberInfo, Type fieldType, TypeDesc fieldTypeDesc)
		{
			this.name = memberInfo.Name;
			this.fieldType = fieldType;
			this.fieldTypeDesc = fieldTypeDesc;
			this.memberInfo = memberInfo;
			this.checkShouldPersistMethodInfo = memberInfo.DeclaringType.GetMethod("ShouldSerialize" + memberInfo.Name, new Type[0]);
			this.checkShouldPersist = (this.checkShouldPersistMethodInfo != null);
			FieldInfo field = memberInfo.DeclaringType.GetField(memberInfo.Name + "Specified");
			if (field != null)
			{
				if (field.FieldType != typeof(bool))
				{
					throw new InvalidOperationException(Res.GetString("XmlInvalidSpecifiedType", new object[]
					{
						field.Name,
						field.FieldType.FullName,
						typeof(bool).FullName
					}));
				}
				this.checkSpecified = (field.IsInitOnly ? SpecifiedAccessor.ReadOnly : SpecifiedAccessor.ReadWrite);
				this.checkSpecifiedMemberInfo = field;
			}
			else
			{
				PropertyInfo property = memberInfo.DeclaringType.GetProperty(memberInfo.Name + "Specified");
				if (property != null)
				{
					if (StructModel.CheckPropertyRead(property))
					{
						this.checkSpecified = (property.CanWrite ? SpecifiedAccessor.ReadWrite : SpecifiedAccessor.ReadOnly);
						this.checkSpecifiedMemberInfo = property;
					}
					if (this.checkSpecified != SpecifiedAccessor.None && property.PropertyType != typeof(bool))
					{
						throw new InvalidOperationException(Res.GetString("XmlInvalidSpecifiedType", new object[]
						{
							property.Name,
							property.PropertyType.FullName,
							typeof(bool).FullName
						}));
					}
				}
			}
			if (memberInfo is PropertyInfo)
			{
				this.readOnly = !((PropertyInfo)memberInfo).CanWrite;
				this.isProperty = true;
				return;
			}
			if (memberInfo is FieldInfo)
			{
				this.readOnly = ((FieldInfo)memberInfo).IsInitOnly;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0006925F File Offset: 0x0006745F
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00069267 File Offset: 0x00067467
		internal Type FieldType
		{
			get
			{
				return this.fieldType;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0006926F File Offset: 0x0006746F
		internal TypeDesc FieldTypeDesc
		{
			get
			{
				return this.fieldTypeDesc;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x00069277 File Offset: 0x00067477
		internal bool CheckShouldPersist
		{
			get
			{
				return this.checkShouldPersist;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0006927F File Offset: 0x0006747F
		internal SpecifiedAccessor CheckSpecified
		{
			get
			{
				return this.checkSpecified;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x00069287 File Offset: 0x00067487
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0006928F File Offset: 0x0006748F
		internal MemberInfo CheckSpecifiedMemberInfo
		{
			get
			{
				return this.checkSpecifiedMemberInfo;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00069297 File Offset: 0x00067497
		internal MethodInfo CheckShouldPersistMethodInfo
		{
			get
			{
				return this.checkShouldPersistMethodInfo;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0006929F File Offset: 0x0006749F
		internal bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x000692A7 File Offset: 0x000674A7
		internal bool IsProperty
		{
			get
			{
				return this.isProperty;
			}
		}

		// Token: 0x04000B20 RID: 2848
		private SpecifiedAccessor checkSpecified;

		// Token: 0x04000B21 RID: 2849
		private MemberInfo memberInfo;

		// Token: 0x04000B22 RID: 2850
		private MemberInfo checkSpecifiedMemberInfo;

		// Token: 0x04000B23 RID: 2851
		private MethodInfo checkShouldPersistMethodInfo;

		// Token: 0x04000B24 RID: 2852
		private bool checkShouldPersist;

		// Token: 0x04000B25 RID: 2853
		private bool readOnly;

		// Token: 0x04000B26 RID: 2854
		private bool isProperty;

		// Token: 0x04000B27 RID: 2855
		private Type fieldType;

		// Token: 0x04000B28 RID: 2856
		private string name;

		// Token: 0x04000B29 RID: 2857
		private TypeDesc fieldTypeDesc;
	}
}
