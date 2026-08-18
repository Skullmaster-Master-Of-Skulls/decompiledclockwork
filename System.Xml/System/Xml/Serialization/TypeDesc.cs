using System;
using System.Xml.Schema;
using System.Xml.Serialization.Advanced;

namespace System.Xml.Serialization
{
	// Token: 0x020002F7 RID: 759
	internal class TypeDesc
	{
		// Token: 0x06002370 RID: 9072 RVA: 0x000A8278 File Offset: 0x000A7278
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

		// Token: 0x06002371 RID: 9073 RVA: 0x000A8323 File Offset: 0x000A7323
		internal TypeDesc(string name, string fullName, XmlSchemaType dataType, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags) : this(name, fullName, dataType, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000A8335 File Offset: 0x000A7335
		internal TypeDesc(string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags) : this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x000A8346 File Offset: 0x000A7346
		internal TypeDesc(Type type, bool isXsdType, XmlSchemaType dataType, string formatterName, TypeFlags flags) : this(type.Name, type.FullName, dataType, TypeKind.Primitive, null, flags, formatterName)
		{
			this.isXsdType = isXsdType;
			this.type = type;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000A836F File Offset: 0x000A736F
		internal TypeDesc(Type type, string name, string fullName, TypeKind kind, TypeDesc baseTypeDesc, TypeFlags flags, TypeDesc arrayElementTypeDesc) : this(name, fullName, null, kind, baseTypeDesc, flags, null)
		{
			this.arrayElementTypeDesc = arrayElementTypeDesc;
			this.type = type;
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000A8390 File Offset: 0x000A7390
		public override string ToString()
		{
			return this.fullName;
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x000A8398 File Offset: 0x000A7398
		internal TypeFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x000A83A0 File Offset: 0x000A73A0
		internal bool IsXsdType
		{
			get
			{
				return this.isXsdType;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x000A83A8 File Offset: 0x000A73A8
		internal bool IsMappedType
		{
			get
			{
				return this.extendedType != null;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002379 RID: 9081 RVA: 0x000A83B6 File Offset: 0x000A73B6
		internal MappedTypeDesc ExtendedType
		{
			get
			{
				return this.extendedType;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000A83BE File Offset: 0x000A73BE
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x000A83C6 File Offset: 0x000A73C6
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x000A83CE File Offset: 0x000A73CE
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

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000A8404 File Offset: 0x000A7404
		internal XmlSchemaType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x000A840C File Offset: 0x000A740C
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000A8414 File Offset: 0x000A7414
		internal string FormatterName
		{
			get
			{
				return this.formatterName;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x000A841C File Offset: 0x000A741C
		internal TypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x000A8424 File Offset: 0x000A7424
		internal bool IsValueType
		{
			get
			{
				return (this.flags & TypeFlags.Reference) == TypeFlags.None;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x000A8431 File Offset: 0x000A7431
		internal bool CanBeAttributeValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeAttributeValue) != TypeFlags.None;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000A8441 File Offset: 0x000A7441
		internal bool XmlEncodingNotRequired
		{
			get
			{
				return (this.flags & TypeFlags.XmlEncodingNotRequired) != TypeFlags.None;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x000A8455 File Offset: 0x000A7455
		internal bool CanBeElementValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeElementValue) != TypeFlags.None;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000A8466 File Offset: 0x000A7466
		internal bool CanBeTextValue
		{
			get
			{
				return (this.flags & TypeFlags.CanBeTextValue) != TypeFlags.None;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000A8477 File Offset: 0x000A7477
		// (set) Token: 0x06002387 RID: 9095 RVA: 0x000A8489 File Offset: 0x000A7489
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

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x000A8492 File Offset: 0x000A7492
		internal bool IsSpecial
		{
			get
			{
				return (this.flags & TypeFlags.Special) != TypeFlags.None;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x000A84A2 File Offset: 0x000A74A2
		internal bool IsAmbiguousDataType
		{
			get
			{
				return (this.flags & TypeFlags.AmbiguousDataType) != TypeFlags.None;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x000A84B6 File Offset: 0x000A74B6
		internal bool HasCustomFormatter
		{
			get
			{
				return (this.flags & TypeFlags.HasCustomFormatter) != TypeFlags.None;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000A84C7 File Offset: 0x000A74C7
		internal bool HasDefaultSupport
		{
			get
			{
				return (this.flags & TypeFlags.IgnoreDefault) == TypeFlags.None;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000A84D8 File Offset: 0x000A74D8
		internal bool HasIsEmpty
		{
			get
			{
				return (this.flags & TypeFlags.HasIsEmpty) != TypeFlags.None;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000A84EC File Offset: 0x000A74EC
		internal bool CollapseWhitespace
		{
			get
			{
				return (this.flags & TypeFlags.CollapseWhitespace) != TypeFlags.None;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x000A8500 File Offset: 0x000A7500
		internal bool HasDefaultConstructor
		{
			get
			{
				return (this.flags & TypeFlags.HasDefaultConstructor) != TypeFlags.None;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x000A8514 File Offset: 0x000A7514
		internal bool IsUnsupported
		{
			get
			{
				return (this.flags & TypeFlags.Unsupported) != TypeFlags.None;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x000A8528 File Offset: 0x000A7528
		internal bool IsGenericInterface
		{
			get
			{
				return (this.flags & TypeFlags.GenericInterface) != TypeFlags.None;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000A853C File Offset: 0x000A753C
		internal bool IsPrivateImplementation
		{
			get
			{
				return (this.flags & TypeFlags.UsePrivateImplementation) != TypeFlags.None;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x000A8550 File Offset: 0x000A7550
		internal bool CannotNew
		{
			get
			{
				return !this.HasDefaultConstructor || this.ConstructorInaccessible;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x000A8562 File Offset: 0x000A7562
		internal bool IsAbstract
		{
			get
			{
				return (this.flags & TypeFlags.Abstract) != TypeFlags.None;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x000A8572 File Offset: 0x000A7572
		internal bool IsOptionalValue
		{
			get
			{
				return (this.flags & TypeFlags.OptionalValue) != TypeFlags.None;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x000A8586 File Offset: 0x000A7586
		internal bool UseReflection
		{
			get
			{
				return (this.flags & TypeFlags.UseReflection) != TypeFlags.None;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x000A859A File Offset: 0x000A759A
		internal bool IsVoid
		{
			get
			{
				return this.kind == TypeKind.Void;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x000A85A5 File Offset: 0x000A75A5
		internal bool IsClass
		{
			get
			{
				return this.kind == TypeKind.Class;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x000A85B0 File Offset: 0x000A75B0
		internal bool IsStructLike
		{
			get
			{
				return this.kind == TypeKind.Struct || this.kind == TypeKind.Class;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x000A85C6 File Offset: 0x000A75C6
		internal bool IsArrayLike
		{
			get
			{
				return this.kind == TypeKind.Array || this.kind == TypeKind.Collection || this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x000A85E5 File Offset: 0x000A75E5
		internal bool IsCollection
		{
			get
			{
				return this.kind == TypeKind.Collection;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x0600239B RID: 9115 RVA: 0x000A85F0 File Offset: 0x000A75F0
		internal bool IsEnumerable
		{
			get
			{
				return this.kind == TypeKind.Enumerable;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x000A85FB File Offset: 0x000A75FB
		internal bool IsArray
		{
			get
			{
				return this.kind == TypeKind.Array;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x000A8606 File Offset: 0x000A7606
		internal bool IsPrimitive
		{
			get
			{
				return this.kind == TypeKind.Primitive;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600239E RID: 9118 RVA: 0x000A8611 File Offset: 0x000A7611
		internal bool IsEnum
		{
			get
			{
				return this.kind == TypeKind.Enum;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000A861C File Offset: 0x000A761C
		internal bool IsNullable
		{
			get
			{
				return !this.IsValueType;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000A8627 File Offset: 0x000A7627
		internal bool IsRoot
		{
			get
			{
				return this.kind == TypeKind.Root;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060023A1 RID: 9121 RVA: 0x000A8632 File Offset: 0x000A7632
		internal bool ConstructorInaccessible
		{
			get
			{
				return (this.flags & TypeFlags.CtorInaccessible) != TypeFlags.None;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x000A8646 File Offset: 0x000A7646
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x000A864E File Offset: 0x000A764E
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

		// Token: 0x060023A4 RID: 9124 RVA: 0x000A8658 File Offset: 0x000A7658
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

		// Token: 0x060023A5 RID: 9125 RVA: 0x000A86D0 File Offset: 0x000A76D0
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

		// Token: 0x060023A6 RID: 9126 RVA: 0x000A873C File Offset: 0x000A773C
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

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x000A8799 File Offset: 0x000A7799
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

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x000A87AF File Offset: 0x000A77AF
		// (set) Token: 0x060023A9 RID: 9129 RVA: 0x000A87B7 File Offset: 0x000A77B7
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

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x000A87C0 File Offset: 0x000A77C0
		internal int Weight
		{
			get
			{
				return this.weight;
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000A87C8 File Offset: 0x000A77C8
		internal TypeDesc CreateArrayTypeDesc()
		{
			if (this.arrayTypeDesc == null)
			{
				this.arrayTypeDesc = new TypeDesc(null, this.name + "[]", this.fullName + "[]", TypeKind.Array, null, TypeFlags.Reference | (this.flags & TypeFlags.UseReflection), this);
			}
			return this.arrayTypeDesc;
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000A8820 File Offset: 0x000A7820
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

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x000A887F File Offset: 0x000A787F
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x000A8887 File Offset: 0x000A7887
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

		// Token: 0x060023AF RID: 9135 RVA: 0x000A88B0 File Offset: 0x000A78B0
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

		// Token: 0x060023B0 RID: 9136 RVA: 0x000A88D8 File Offset: 0x000A78D8
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

		// Token: 0x0400151B RID: 5403
		private string name;

		// Token: 0x0400151C RID: 5404
		private string fullName;

		// Token: 0x0400151D RID: 5405
		private string cSharpName;

		// Token: 0x0400151E RID: 5406
		private TypeDesc arrayElementTypeDesc;

		// Token: 0x0400151F RID: 5407
		private TypeDesc arrayTypeDesc;

		// Token: 0x04001520 RID: 5408
		private TypeDesc nullableTypeDesc;

		// Token: 0x04001521 RID: 5409
		private TypeKind kind;

		// Token: 0x04001522 RID: 5410
		private XmlSchemaType dataType;

		// Token: 0x04001523 RID: 5411
		private Type type;

		// Token: 0x04001524 RID: 5412
		private TypeDesc baseTypeDesc;

		// Token: 0x04001525 RID: 5413
		private TypeFlags flags;

		// Token: 0x04001526 RID: 5414
		private string formatterName;

		// Token: 0x04001527 RID: 5415
		private bool isXsdType;

		// Token: 0x04001528 RID: 5416
		private bool isMixed;

		// Token: 0x04001529 RID: 5417
		private MappedTypeDesc extendedType;

		// Token: 0x0400152A RID: 5418
		private int weight;

		// Token: 0x0400152B RID: 5419
		private Exception exception;
	}
}
