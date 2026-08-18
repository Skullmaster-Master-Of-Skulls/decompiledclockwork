using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm.Provider
{
	// Token: 0x020004F7 RID: 1271
	internal class ClrProviderManifest : DbProviderManifest
	{
		// Token: 0x06002F38 RID: 12088 RVA: 0x000E10F9 File Offset: 0x000DF2F9
		private ClrProviderManifest()
		{
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000E1101 File Offset: 0x000DF301
		internal static ClrProviderManifest Instance
		{
			get
			{
				return ClrProviderManifest._instance;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000E1108 File Offset: 0x000DF308
		public override string NamespaceName
		{
			get
			{
				return "System";
			}
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000E1110 File Offset: 0x000DF310
		internal bool TryGetPrimitiveType(Type clrType, out PrimitiveType primitiveType)
		{
			primitiveType = null;
			PrimitiveTypeKind index;
			if (ClrProviderManifest.TryGetPrimitiveTypeKind(clrType, out index))
			{
				this.InitializePrimitiveTypes();
				primitiveType = this._primitiveTypes[(int)index];
				return true;
			}
			return false;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000E1144 File Offset: 0x000DF344
		internal static bool TryGetPrimitiveTypeKind(Type clrType, out PrimitiveTypeKind resolvedPrimitiveTypeKind)
		{
			PrimitiveTypeKind? primitiveTypeKind = null;
			if (!clrType.IsEnum())
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

		// Token: 0x06002F3D RID: 12093 RVA: 0x000E1318 File Offset: 0x000DF518
		public override ReadOnlyCollection<EdmFunction> GetStoreFunctions()
		{
			return Helper.EmptyEdmFunctionReadOnlyCollection;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000E1320 File Offset: 0x000DF520
		public override ReadOnlyCollection<FacetDescription> GetFacetDescriptions(EdmType type)
		{
			if (Helper.IsPrimitiveType(type) && type.DataSpace == DataSpace.OSpace)
			{
				PrimitiveType primitiveType = (PrimitiveType)type.BaseType;
				return primitiveType.ProviderManifest.GetFacetDescriptions(primitiveType);
			}
			return Helper.EmptyFacetDescriptionEnumerable;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000E135C File Offset: 0x000DF55C
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

		// Token: 0x06002F40 RID: 12096 RVA: 0x000E14F4 File Offset: 0x000DF6F4
		private PrimitiveType CreatePrimitiveType(Type clrType, PrimitiveTypeKind primitiveTypeKind)
		{
			PrimitiveType primitiveType = MetadataItem.EdmProviderManifest.GetPrimitiveType(primitiveTypeKind);
			PrimitiveType primitiveType2 = new PrimitiveType(clrType, primitiveType, this);
			primitiveType2.SetReadOnly();
			return primitiveType2;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000E151D File Offset: 0x000DF71D
		public override ReadOnlyCollection<PrimitiveType> GetStoreTypes()
		{
			this.InitializePrimitiveTypes();
			return this._primitiveTypes;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000E152B File Offset: 0x000DF72B
		public override TypeUsage GetEdmType(TypeUsage storeType)
		{
			Check.NotNull<TypeUsage>(storeType, "storeType");
			throw new NotImplementedException();
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000E153E File Offset: 0x000DF73E
		public override TypeUsage GetStoreType(TypeUsage edmType)
		{
			Check.NotNull<TypeUsage>(edmType, "edmType");
			throw new NotImplementedException();
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000E1551 File Offset: 0x000DF751
		protected override XmlReader GetDbInformation(string informationType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001212 RID: 4626
		private const int s_PrimitiveTypeCount = 17;

		// Token: 0x04001213 RID: 4627
		private ReadOnlyCollection<PrimitiveType> _primitiveTypes;

		// Token: 0x04001214 RID: 4628
		private static readonly ClrProviderManifest _instance = new ClrProviderManifest();
	}
}
