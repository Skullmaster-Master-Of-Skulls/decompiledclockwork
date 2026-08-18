using System;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;
using System.Data.SqlTypes;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000045 RID: 69
	internal sealed class SqlSpatialDataReader : DbSpatialDataReader
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x00017314 File Offset: 0x00015514
		internal SqlSpatialDataReader(DbSpatialServices spatialServices, SqlDataReaderWrapper underlyingReader)
		{
			this._spatialServices = spatialServices;
			this._reader = underlyingReader;
			int fieldCount = this._reader.FieldCount;
			this._geographyColumns = new bool[fieldCount];
			this._geometryColumns = new bool[fieldCount];
			for (int i = 0; i < this._reader.FieldCount; i++)
			{
				string dataTypeName = this._reader.GetDataTypeName(i);
				if (dataTypeName.EndsWith("sys.geography", StringComparison.Ordinal))
				{
					this._geographyColumns[i] = true;
				}
				else if (dataTypeName.EndsWith("sys.geometry", StringComparison.Ordinal))
				{
					this._geometryColumns[i] = true;
				}
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000173AC File Offset: 0x000155AC
		public override DbGeography GetGeography(int ordinal)
		{
			this.EnsureGeographyColumn(ordinal);
			SqlBytes sqlBytes = this._reader.GetSqlBytes(ordinal);
			object providerValue = SqlSpatialDataReader._sqlGeographyFromBinaryReader.Value(new BinaryReader(sqlBytes.Stream));
			return this._spatialServices.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000173F4 File Offset: 0x000155F4
		public override DbGeometry GetGeometry(int ordinal)
		{
			this.EnsureGeometryColumn(ordinal);
			SqlBytes sqlBytes = this._reader.GetSqlBytes(ordinal);
			object providerValue = SqlSpatialDataReader._sqlGeometryFromBinaryReader.Value(new BinaryReader(sqlBytes.Stream));
			return this._spatialServices.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001743C File Offset: 0x0001563C
		public override bool IsGeographyColumn(int ordinal)
		{
			return this._geographyColumns[ordinal];
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00017446 File Offset: 0x00015646
		public override bool IsGeometryColumn(int ordinal)
		{
			return this._geometryColumns[ordinal];
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00017450 File Offset: 0x00015650
		private void EnsureGeographyColumn(int ordinal)
		{
			if (!this.IsGeographyColumn(ordinal))
			{
				throw new InvalidDataException(Strings.SqlProvider_InvalidGeographyColumn(this._reader.GetDataTypeName(ordinal)));
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00017472 File Offset: 0x00015672
		private void EnsureGeometryColumn(int ordinal)
		{
			if (!this.IsGeometryColumn(ordinal))
			{
				throw new InvalidDataException(Strings.SqlProvider_InvalidGeometryColumn(this._reader.GetDataTypeName(ordinal)));
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00017494 File Offset: 0x00015694
		private static Func<BinaryReader, object> CreateBinaryReadDelegate(Type spatialType)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(BinaryReader));
			ParameterExpression parameterExpression2 = Expression.Variable(spatialType);
			MethodInfo publicInstanceMethod = spatialType.GetPublicInstanceMethod("Read", new Type[]
			{
				typeof(BinaryReader)
			});
			Expression<Func<BinaryReader, object>> expression = Expression.Lambda<Func<BinaryReader, object>>(Expression.Block(new ParameterExpression[]
			{
				parameterExpression2
			}, new Expression[]
			{
				Expression.Assign(parameterExpression2, Expression.New(spatialType)),
				Expression.Call(parameterExpression2, publicInstanceMethod, new Expression[]
				{
					parameterExpression
				}),
				parameterExpression2
			}), new ParameterExpression[]
			{
				parameterExpression
			});
			return expression.Compile();
		}

		// Token: 0x04000115 RID: 277
		private const string GeometrySqlType = "sys.geometry";

		// Token: 0x04000116 RID: 278
		private const string GeographySqlType = "sys.geography";

		// Token: 0x04000117 RID: 279
		private static readonly Lazy<Func<BinaryReader, object>> _sqlGeographyFromBinaryReader = new Lazy<Func<BinaryReader, object>>(() => SqlSpatialDataReader.CreateBinaryReadDelegate(SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().SqlGeographyType), true);

		// Token: 0x04000118 RID: 280
		private static readonly Lazy<Func<BinaryReader, object>> _sqlGeometryFromBinaryReader = new Lazy<Func<BinaryReader, object>>(() => SqlSpatialDataReader.CreateBinaryReadDelegate(SqlTypesAssemblyLoader.DefaultInstance.GetSqlTypesAssembly().SqlGeometryType), true);

		// Token: 0x04000119 RID: 281
		private readonly DbSpatialServices _spatialServices;

		// Token: 0x0400011A RID: 282
		private readonly SqlDataReaderWrapper _reader;

		// Token: 0x0400011B RID: 283
		private readonly bool[] _geographyColumns;

		// Token: 0x0400011C RID: 284
		private readonly bool[] _geometryColumns;
	}
}
