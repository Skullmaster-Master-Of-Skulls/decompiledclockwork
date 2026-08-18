using System;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Spatial;
using System.Data.SqlTypes;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.SqlClient
{
	// Token: 0x02000025 RID: 37
	internal sealed class SqlSpatialDataReader : DbSpatialDataReader
	{
		// Token: 0x06000251 RID: 593 RVA: 0x000083D5 File Offset: 0x000065D5
		internal SqlSpatialDataReader(SqlDataReader underlyingReader)
		{
			this.reader = underlyingReader;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000083E4 File Offset: 0x000065E4
		public override DbGeography GetGeography(int ordinal)
		{
			this.EnsureGeographyColumn(ordinal);
			SqlBytes sqlBytes = this.reader.GetSqlBytes(ordinal);
			object providerValue = SqlSpatialDataReader.sqlGeographyFromBinaryReader.Value(new BinaryReader(sqlBytes.Stream));
			return SqlSpatialServices.Instance.GeographyFromProviderValue(providerValue);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000842C File Offset: 0x0000662C
		public override DbGeometry GetGeometry(int ordinal)
		{
			this.EnsureGeometryColumn(ordinal);
			SqlBytes sqlBytes = this.reader.GetSqlBytes(ordinal);
			object providerValue = SqlSpatialDataReader.sqlGeometryFromBinaryReader.Value(new BinaryReader(sqlBytes.Stream));
			return SqlSpatialServices.Instance.GeometryFromProviderValue(providerValue);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008474 File Offset: 0x00006674
		private void EnsureGeographyColumn(int ordinal)
		{
			string dataTypeName = this.reader.GetDataTypeName(ordinal);
			if (!dataTypeName.EndsWith("sys.geography", StringComparison.Ordinal))
			{
				throw new InvalidDataException(Strings.SqlProvider_InvalidGeographyColumn(dataTypeName));
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000084A8 File Offset: 0x000066A8
		private void EnsureGeometryColumn(int ordinal)
		{
			string dataTypeName = this.reader.GetDataTypeName(ordinal);
			if (!dataTypeName.EndsWith("sys.geometry", StringComparison.Ordinal))
			{
				throw new InvalidDataException(Strings.SqlProvider_InvalidGeometryColumn(dataTypeName));
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000084DC File Offset: 0x000066DC
		private static Func<BinaryReader, object> CreateBinaryReadDelegate(Type spatialType)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(BinaryReader));
			ParameterExpression parameterExpression2 = Expression.Variable(spatialType);
			MethodInfo method = spatialType.GetMethod("Read", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(BinaryReader)
			}, null);
			Expression<Func<BinaryReader, object>> expression = Expression.Lambda<Func<BinaryReader, object>>(Expression.Block(new ParameterExpression[]
			{
				parameterExpression2
			}, new Expression[]
			{
				Expression.Assign(parameterExpression2, Expression.New(spatialType)),
				Expression.Call(parameterExpression2, method, new Expression[]
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

		// Token: 0x04000654 RID: 1620
		private readonly SqlDataReader reader;

		// Token: 0x04000655 RID: 1621
		private const string geometrySqlType = "sys.geometry";

		// Token: 0x04000656 RID: 1622
		private const string geographySqlType = "sys.geography";

		// Token: 0x04000657 RID: 1623
		private static readonly Singleton<Func<BinaryReader, object>> sqlGeographyFromBinaryReader = new Singleton<Func<BinaryReader, object>>(() => SqlSpatialDataReader.CreateBinaryReadDelegate(SqlProviderServices.GetSqlTypesAssembly().SqlGeographyType));

		// Token: 0x04000658 RID: 1624
		private static readonly Singleton<Func<BinaryReader, object>> sqlGeometryFromBinaryReader = new Singleton<Func<BinaryReader, object>>(() => SqlSpatialDataReader.CreateBinaryReadDelegate(SqlProviderServices.GetSqlTypesAssembly().SqlGeometryType));
	}
}
