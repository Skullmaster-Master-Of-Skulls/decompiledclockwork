using System;
using System.Data.Metadata.Edm;
using System.Data.Spatial;

namespace System.Data.SqlClient.Internal
{
	// Token: 0x02000041 RID: 65
	internal struct DbGeometryAdapter : IDbSpatialValue
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x00017A40 File Offset: 0x00015C40
		internal DbGeometryAdapter(DbGeometry geomValue)
		{
			this.value = geomValue;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017A4C File Offset: 0x00015C4C
		private TResult NullIfNotImplemented<TResult>(Func<DbGeometry, TResult> accessor) where TResult : class
		{
			TResult result;
			try
			{
				result = accessor(this.value);
			}
			catch (NotImplementedException)
			{
				result = default(TResult);
			}
			return result;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00017A88 File Offset: 0x00015C88
		private int? NullIfNotImplemented(Func<DbGeometry, int> accessor)
		{
			int? result;
			try
			{
				result = new int?(accessor(this.value));
			}
			catch (NotImplementedException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000173E2 File Offset: 0x000155E2
		public bool IsGeography
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public PrimitiveTypeKind PrimitiveType
		{
			get
			{
				return PrimitiveTypeKind.Geometry;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00017ACC File Offset: 0x00015CCC
		public object ProviderValue
		{
			get
			{
				return this.NullIfNotImplemented<object>((DbGeometry geom) => geom.ProviderValue);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00017AF3 File Offset: 0x00015CF3
		public int? CoordinateSystemId
		{
			get
			{
				return this.NullIfNotImplemented((DbGeometry geom) => geom.CoordinateSystemId);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00017B1C File Offset: 0x00015D1C
		public string WellKnownText
		{
			get
			{
				string result;
				if ((result = this.NullIfNotImplemented<string>((DbGeometry geom) => geom.AsTextIncludingElevationAndMeasure())) == null)
				{
					result = this.NullIfNotImplemented<string>((DbGeometry geom) => geom.AsText());
				}
				return result;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00017B77 File Offset: 0x00015D77
		public byte[] WellKnownBinary
		{
			get
			{
				return this.NullIfNotImplemented<byte[]>((DbGeometry geom) => geom.AsBinary());
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00017B9E File Offset: 0x00015D9E
		public string GmlString
		{
			get
			{
				return this.NullIfNotImplemented<string>((DbGeometry geom) => geom.AsGml());
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00017BC5 File Offset: 0x00015DC5
		public Exception NotSqlCompatible()
		{
			return EntityUtil.GeometryValueNotSqlCompatible();
		}

		// Token: 0x04000747 RID: 1863
		private readonly DbGeometry value;
	}
}
