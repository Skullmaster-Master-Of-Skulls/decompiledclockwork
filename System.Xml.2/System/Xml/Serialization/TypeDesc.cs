using System;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	// Token: 0x0200017E RID: 382
	internal class TypeDesc
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x00070BD8 File Offset: 0x0006EDD8
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, string formatterName)
		{
			this.name = name.Replace('+', '.');
			this.fullName = fullName.Replace('+', '.');
			this.kind = kind;
			this.baseTypeDesc = baseTypeDesc;
			this.flags = flags;
			this.isXsdType = (kind == TypeKind.Primitive);
			if (this.isXsdType)
			{
				this.weight = 1;
			}
			else if (kind == TypeKind.Enum)
			{
				this.weight = 2;
			}
			else if (this.kind == TypeKind.Root)
			{
				this.weight = -1;
			}
			else
			{
				this.weight = ((baseTypeDesc == null) ? 0 : (baseTypeDesc.Weight + 1));
			}
			this.dataType = dataType;
			this.formatterName = formatterName;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00070C83 File Offset: 0x0006EE83
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags) : this(name, fullName, dataType, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00070C95 File Offset: 0x0006EE95
		internal TypeDesc(string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags) : this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00070CA6 File Offset: 0x0006EEA6
		internal TypeDesc(Type type, bool isXsdType, XmlSchemaType dataType, string formatterName, TypeFlags flags) : this(type.Name, type.FullName, dataType, TypeKind.Primitive, null, flags, formatterName)
		{
			this.isXsdType = isXsdType;
			this.type = type;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00070CCF File Offset: 0x0006EECF
		internal TypeDesc(Type type, string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, TypeDesc arrayElementTypeDesc) : this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
			this.arrayElementTypeDesc = arrayElementTypeDesc;
			this.type = type;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00070CF0 File Offset: 0x0006EEF0
		public override string ToString()
		{
			return this.fullName;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00070CF8 File Offset: 0x0006EEF8
		internal TypeFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x00070D00 File Offset: 0x0006EF00
		internal bool IsXsdType
		{
			get
			{
				return this.isXsdType;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00070D08 File Offset: 0x0006EF08
		internal bool IsMappedType
		{
			get
			{
				return this.extendedType != null;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x00070D13 File Offset: 0x0006EF13
		internal MappedTypeDesc ExtendedType
		{
			get
			{
				return this.extendedType;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00070D1B File Offset: 0x0006EF1B
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00070D23 File Offset: 0x0006EF23
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00070D2B File Offset: 0x0006EF2B
		internal string CSharpName
		{
			get
			{
				if (this.cSharpName == null)
				{
					this.cSharpName = ((this.type == null) ? CodeIdentifier.GetCSharpName(this.fullName) : CodeIdentifier.GetCSharpName(this.type));
				}
				return this.cSharpName;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00070D67 File Offset: 0x0006EF67
		internal XmlSchemaType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00070D6F File Offset: 0x0006EF6F
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00070D77 File Offset: 0x0006EF77
		internal string FormatterName
		{
			get
			{
				return this.formatterName;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00070D7F File Offset: 0x0006EF7F
		internal TypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00070D87 File Offset: 0x0006EF87
		internal bool IsValueType
		{
			get
			{
				return (this.flags & TypeFlags.Reference) == TypeFlags.None;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00070D94 File Offset: 0x0006EF94
		internal bool CanBeAttributeValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeAttributeValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00070DA1 File Offset: 0x0006EFA1
		internal bool XmlEncodingNotRequired
		{
			get
			{
				return (this.flags & TypeFlags.XmlEncodingNotRequired) > TypeFlags.None;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001948 RID: 6472 RVA: 0x00070DB2 File Offset: 0x0006EFB2
		internal bool CanBeElementValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeElementValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x00070DC0 File Offset: 0x0006EFC0
		internal bool CanBeTextValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeTextValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x00070DCE File Offset: 0x0006EFCE
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x00070DE0 File Offset: 0x0006EFE0
		internal bool IsMixed
		{
			get
			{
				return this.isMixed || this.CanBeTextValue;
			}
			set
			{
				this.isMixed = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x00070DE9 File Offset: 0x0006EFE9
		internal bool IsSpecial
		{
			get
			{
				return (this.flags & TypeFlags.Special) > TypeFlags.None;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x00070DF6 File Offset: 0x0006EFF6
		internal bool IsAmbiguousDataType
		{
			get
			{
				return (this.flags & TypeFlags.AmbiguousDataType) > TypeFlags.None;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x00070E07 File Offset: 0x0006F007
		internal bool HasCustomFormatter
		{
			get
			{
				return (this.flags & TypeFlags.HasCustomFormatter) > TypeFlags.None;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x00070E15 File Offset: 0x0006F015
		internal bool HasDefaultSupport
		{
			get
			{
				return (this.flags & TypeFlags.IgnoreDefault) == TypeFlags.None;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00070E26 File Offset: 0x0006F026
		internal bool HasIsEmpty
		{
			get
			{
				return (this.flags & TypeFlags.HasIsEmpty) > TypeFlags.None;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x00070E37 File Offset: 0x0006F037
		internal bool CollapseWhitespace
		{
			get
			{
				return (this.flags & TypeFlags.CollapseWhitespace) > TypeFlags.None;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x00070E48 File Offset: 0x0006F048
		internal bool HasDefaultConstructor
		{
			get
			{
				return (this.flags & TypeFlags.HasDefaultConstructor) > TypeFlags.None;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00070E59 File Offset: 0x0006F059
		internal bool IsUnsupported
		{
			get
			{
				return (this.flags & TypeFlags.Unsupported) > TypeFlags.None;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00070E6A File Offset: 0x0006F06A
		internal bool IsGenericInterface
		{
			get
			{
				return (this.flags & TypeFlags.GenericInterface) > TypeFlags.None;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00070E7B File Offset: 0x0006F07B
		internal bool IsPrivateImplementation
		{
			get
			{
				return (this.flags & TypeFlags.UsePrivateImplementation) > TypeFlags.None;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00070E8C File Offset: 0x0006F08C
		internal bool CannotNew
		{
			get
			{
				return !this.HasDefaultConstructor || this.ConstructorInaccessible;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x00070E9E File Offset: 0x0006F09E
		internal bool IsAbstract
		{
			get
			{
				return (this.flags & TypeFlags.Abstract) > TypeFlags.None;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00070EAB File Offset: 0x0006F0AB
		internal bool IsOptionalValue
		{
			get
			{
				return (this.flags & TypeFlags.OptionalValue) > TypeFlags.None;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x00070EBC File Offset: 0x0006F0BC
		internal bool UseReflection
		{
			get
			{
				return (this.flags & TypeFlags.UseReflection) > TypeFlags.None;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00070ECD File Offset: 0x0006F0CD
		internal bool IsVoid
		{
			get
			{
				return this.kind == TypeKind.Void;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00070ED8 File Offset: 0x0006F0D8
		internal bool IsClass
		{
			get
			{
				return this.kind == TypeKind.Class;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00070EE3 File Offset: 0x0006F0E3
		internal bool IsStructLike
		{
			get
			{
				return this.kind == TypeKind.Struct || this.kind == TypeKind.Class;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00070EF9 File Offset: 0x0006F0F9
		internal bool IsArrayLike
		{
			get
			{
				return this.kind == TypeKind.Array || this.kind == TypeKind.Collection || this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00070F18 File Offset: 0x0006F118
		internal bool IsCollection
		{
			get
			{
				return this.kind == TypeKind.Collection;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00070F23 File Offset: 0x0006F123
		internal bool IsEnumerable
		{
			get
			{
				return this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00070F2E File Offset: 0x0006F12E
		internal bool IsArray
		{
			get
			{
				return this.kind == TypeKind.Array;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00070F39 File Offset: 0x0006F139
		internal bool IsPrimitive
		{
			get
			{
				return this.kind == TypeKind.Primitive;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00070F44 File Offset: 0x0006F144
		internal bool IsEnum
		{
			get
			{
				return this.kind == TypeKind.Enum;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00070F4F File Offset: 0x0006F14F
		internal bool IsNullable
		{
			get
			{
				return !this.IsValueType;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00070F5A File Offset: 0x0006F15A
		internal bool IsRoot
		{
			get
			{
				return this.kind == TypeKind.Root;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x00070F65 File Offset: 0x0006F165
		internal bool ConstructorInaccessible
		{
			get
			{
				return (this.flags & TypeFlags.CtorInaccessible) > TypeFlags.None;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x00070F76 File Offset: 0x0006F176
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x00070F7E File Offset: 0x0006F17E
		internal Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00070F88 File Offset: 0x0006F188
		internal TypeDesc GetNullableTypeDesc(Type type)
		{
			if (this.IsOptionalValue)
			{
				return this;
			}
			if (this.nullableTypeDesc == null)
			{
				this.nullableTypeDesc = new TypeDesc("NullableOf" + this.name, "System.Nullable`1[" + this.fullName + "]", null, TypeKind.Struct, this, this.flags | TypeFlags.OptionalValue, this.formatterName);
				this.nullableTypeDesc.type = type;
			}
			return this.nullableTypeDesc;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00071000 File Offset: 0x0006F200
		internal void CheckSupported()
		{
			if (!this.IsUnsupported)
			{
				if (this.baseTypeDesc != null)
				{
					this.baseTypeDesc.CheckSupported();
				}
				if (this.arrayElementTypeDesc != null)
				{
					this.arrayElementTypeDesc.CheckSupported();
				}
				return;
			}
			if (this.Exception != null)
			{
				throw this.Exception;
			}
			throw new NotSupportedException(Res.GetString("XmlSerializerUnsupportedType", new object[]
			{
				this.FullName
			}));
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0007106C File Offset: 0x0006F26C
		internal void CheckNeedConstructor()
		{
			if (!this.IsValueType && !this.IsAbstract && !this.HasDefaultConstructor)
			{
				this.flags |= TypeFlags.Unsupported;
				this.exception = new InvalidOperationException(Res.GetString("XmlConstructorInaccessible", new object[]
				{
					this.FullName
				}));
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600196B RID: 6507 RVA: 0x000710C7 File Offset: 0x0006F2C7
		internal string ArrayLengthName
		{
			get
			{
				if (this.kind != TypeKind.Array)
				{
					return "Count";
				}
				return "Length";
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x000710DD File Offset: 0x0006F2DD
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x000710E5 File Offset: 0x0006F2E5
		internal TypeDesc ArrayElementTypeDesc
		{
			get
			{
				return this.arrayElementTypeDesc;
			}
			set
			{
				this.arrayElementTypeDesc = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x000710EE File Offset: 0x0006F2EE
		internal int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000710F8 File Offset: 0x0006F2F8
		internal TypeDesc CreateArrayTypeDesc()
		{
			if (this.arrayTypeDesc == null)
			{
				this.arrayTypeDesc = new TypeDesc(null, this.name + "[]", this.fullName + "[]", TypeKind.Array, null, TypeFlags.Reference | (this.flags & TypeFlags.UseReflection), this);
			}
			return this.arrayTypeDesc;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00071150 File Offset: 0x0006F350
		internal TypeDesc CreateMappedTypeDesc(MappedTypeDesc extension)
		{
			return new TypeDesc(extension.Name, extension.Name, null, this.kind, this.baseTypeDesc, this.flags, null)
			{
				isXsdType = this.isXsdType,
				isMixed = this.isMixed,
				extendedType = extension,
				dataType = this.dataType
			};
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x000711AF File Offset: 0x0006F3AF
		// (set) Token: 0x06001972 RID: 6514 RVA: 0x000711B7 File Offset: 0x0006F3B7
		internal TypeDesc BaseTypeDesc
		{
			get
			{
				return this.baseTypeDesc;
			}
			set
			{
				this.baseTypeDesc = value;
				this.weight = ((this.baseTypeDesc == null) ? 0 : (this.baseTypeDesc.Weight + 1));
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000711E0 File Offset: 0x0006F3E0
		internal bool IsDerivedFrom(TypeDesc baseTypeDesc)
		{
			for (TypeDesc typeDesc = this; typeDesc != null; typeDesc = typeDesc.BaseTypeDesc)
			{
				if (typeDesc == baseTypeDesc)
				{
					return true;
				}
			}
			return baseTypeDesc.IsRoot;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00071208 File Offset: 0x0006F408
		internal static TypeDesc FindCommonBaseTypeDesc(TypeDesc[] typeDescs)
		{
			if (typeDescs.Length == 0)
			{
				return null;
			}
			TypeDesc typeDesc = null;
			int num = int.MaxValue;
			for (int i = 0; i < typeDescs.Length; i++)
			{
				int num2 = typeDescs[i].Weight;
				if (num2 < num)
				{
					num = num2;
					typeDesc = typeDescs[i];
				}
			}
			while (typeDesc != null)
			{
				int num3 = 0;
				while (num3 < typeDescs.Length && typeDescs[num3].IsDerivedFrom(typeDesc))
				{
					num3++;
				}
				if (num3 == typeDescs.Length)
				{
					break;
				}
				typeDesc = typeDesc.BaseTypeDesc;
			}
			return typeDesc;
		}

		// Token: 0x04000B8F RID: 2959
		private string name;

		// Token: 0x04000B90 RID: 2960
		private string fullName;

		// Token: 0x04000B91 RID: 2961
		private string cSharpName;

		// Token: 0x04000B92 RID: 2962
		private TypeDesc arrayElementTypeDesc;

		// Token: 0x04000B93 RID: 2963
		private TypeDesc arrayTypeDesc;

		// Token: 0x04000B94 RID: 2964
		private TypeDesc nullableTypeDesc;

		// Token: 0x04000B95 RID: 2965
		private TypeKind kind;

		// Token: 0x04000B96 RID: 2966
		private XmlSchemaType dataType;

		// Token: 0x04000B97 RID: 2967
		private Type type;

		// Token: 0x04000B98 RID: 2968
		private TypeDesc baseTypeDesc;

		// Token: 0x04000B99 RID: 2969
		private TypeFlags flags;

		// Token: 0x04000B9A RID: 2970
		private string formatterName;

		// Token: 0x04000B9B RID: 2971
		private bool isXsdType;

		// Token: 0x04000B9C RID: 2972
		private bool isMixed;

		// Token: 0x04000B9D RID: 2973
		private MappedTypeDesc extendedType;

		// Token: 0x04000B9E RID: 2974
		private int weight;

		// Token: 0x04000B9F RID: 2975
		private Exception exception;
	}
}
