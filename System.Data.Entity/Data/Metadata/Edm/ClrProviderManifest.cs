using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Spatial;
using System.Threading;
using System.Xml;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001ED RID: 493
	internal class ClrProviderManifest : DbProviderManifest
	{
		// Token: 0x060020D8 RID: 8408 RVA: 0x000729E7 File Offset: 0x00070BE7
		private ClrProviderManifest()
		{
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000729EF File Offset: 0x00070BEF
		internal static ClrProviderManifest Instance
		{
			get
			{
				return ClrProviderManifest._instance;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000729F6 File Offset: 0x00070BF6
		public override string NamespaceName
		{
			get
			{
				return "System";
			}
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00072A00 File Offset: 0x00070C00
		internal bool TryGetPrimitiveType(Type clrType, out PrimitiveType primitiveType)
		{
			primitiveType = null;
			PrimitiveTypeKind index;
			if (this.TryGetPrimitiveTypeKind(clrType, out index))
			{
				this.InitializePrimitiveTypes();
				primitiveType = this._primitiveTypes[(int)index];
				return true;
			}
			return false;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00072A34 File Offset: 0x00070C34
		internal bool TryGetPrimitiveTypeKind(Type clrType, out PrimitiveTypeKind resolvedPrimitiveTypeKind)
		{
			PrimitiveTypeKind? primitiveTypeKind = null;
			if (!clrType.IsEnum)
			{
				switch (Type.GetTypeCode(clrType))
				{
				case TypeCode.Object:
					if (typeof(byte[]) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Binary);
					}
					else if (typeof(DateTimeOffset) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.DateTimeOffset);
					}
					else if (typeof(DbGeography).IsAssignableFrom(clrType))
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Geography);
					}
					else if (typeof(DbGeometry).IsAssignableFrom(clrType))
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Geometry);
					}
					else if (typeof(Guid) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Guid);
					}
					else if (typeof(TimeSpan) == clrType)
					{
						primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Time);
					}
					break;
				case TypeCode.Boolean:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Boolean);
					break;
				case TypeCode.SByte:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.SByte);
					break;
				case TypeCode.Byte:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Byte);
					break;
				case TypeCode.Int16:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Int16);
					break;
				case TypeCode.Int32:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Int32);
					break;
				case TypeCode.Int64:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Int64);
					break;
				case TypeCode.Single:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Single);
					break;
				case TypeCode.Double:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Double);
					break;
				case TypeCode.Decimal:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.Decimal);
					break;
				case TypeCode.DateTime:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.DateTime);
					break;
				case TypeCode.String:
					primitiveTypeKind = new PrimitiveTypeKind?(PrimitiveTypeKind.String);
					break;
				}
			}
			if (primitiveTypeKind != null)
			{
				resolvedPrimitiveTypeKind = primitiveTypeKind.Value;
				return true;
			}
			resolvedPrimitiveTypeKind = PrimitiveTypeKind.Binary;
			return false;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00072C08 File Offset: 0x00070E08
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return Helper.EmptyEdmFunctionReadOnlyCollection;
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00072C10 File Offset: 0x00070E10
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType type)
		{
			if (Helper.IsPrimitiveType(type) && ((PrimitiveType)type).DataSpace == DataSpace.OSpace)
			{
				PrimitiveType primitiveType = (PrimitiveType)type.BaseType;
				return primitiveType.ProviderManifest.GetFacetDescriptions(primitiveType);
			}
			return Helper.EmptyFacetDescriptionEnumerable;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00072C50 File Offset: 0x00070E50
		private void InitializePrimitiveTypes()
		{
			if (this._primitiveTypes != null)
			{
				return;
			}
			PrimitiveType[] array = new PrimitiveType[17];
			array[0] = this.CreatePrimitiveType(typeof(byte[]), PrimitiveTypeKind.Binary);
			array[1] = this.CreatePrimitiveType(typeof(bool), PrimitiveTypeKind.Boolean);
			array[2] = this.CreatePrimitiveType(typeof(byte), PrimitiveTypeKind.Byte);
			array[3] = this.CreatePrimitiveType(typeof(DateTime), PrimitiveTypeKind.DateTime);
			array[13] = this.CreatePrimitiveType(typeof(TimeSpan), PrimitiveTypeKind.Time);
			array[14] = this.CreatePrimitiveType(typeof(DateTimeOffset), PrimitiveTypeKind.DateTimeOffset);
			array[4] = this.CreatePrimitiveType(typeof(decimal), PrimitiveTypeKind.Decimal);
			array[5] = this.CreatePrimitiveType(typeof(double), PrimitiveTypeKind.Double);
			array[16] = this.CreatePrimitiveType(typeof(DbGeography), PrimitiveTypeKind.Geography);
			array[15] = this.CreatePrimitiveType(typeof(DbGeometry), PrimitiveTypeKind.Geometry);
			array[6] = this.CreatePrimitiveType(typeof(Guid), PrimitiveTypeKind.Guid);
			array[9] = this.CreatePrimitiveType(typeof(short), PrimitiveTypeKind.Int16);
			array[10] = this.CreatePrimitiveType(typeof(int), PrimitiveTypeKind.Int32);
			array[11] = this.CreatePrimitiveType(typeof(long), PrimitiveTypeKind.Int64);
			array[8] = this.CreatePrimitiveType(typeof(sbyte), PrimitiveTypeKind.SByte);
			array[7] = this.CreatePrimitiveType(typeof(float), PrimitiveTypeKind.Single);
			array[12] = this.CreatePrimitiveType(typeof(string), PrimitiveTypeKind.String);
			ReadOnlyCollection<PrimitiveType> value = new ReadOnlyCollection<PrimitiveType>(array);
			Interlocked.CompareExchange<ReadOnlyCollection<PrimitiveType>>(ref this._primitiveTypes, value, null);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00072DE8 File Offset: 0x00070FE8
		private PrimitiveType CreatePrimitiveType(Type clrType, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType primitiveType = MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind);
			PrimitiveType primitiveType2 = new PrimitiveType(clrType, primitiveType, this);
			primitiveType2.SetReadOnly();
			return primitiveType2;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00072E11 File Offset: 0x00071011
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00072E1F File Offset: 0x0007101F
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00072E1F File Offset: 0x0007101F
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00072E1F File Offset: 0x0007101F
		protected override XmlReader GetDbInformation(string informationType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000E90 RID: 3728
		private const int s_PrimitiveTypeCount = 17;

		// Token: 0x04000E91 RID: 3729
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04000E92 RID: 3730
		private static ClrProviderManifest _instance = new ClrProviderManifest();
	}
}
