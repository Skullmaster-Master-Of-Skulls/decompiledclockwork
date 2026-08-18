using System;
using System.Data.Metadata.Edm;
using System.Data.Spatial;

namespace System.Data.SqlClient.Internal
{
	// Token: 0x02000040 RID: 64
	internal struct DbGeographyAdapter : IDbSpatialValue
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x000178B3 File Offset: 0x00015AB3
		internal DbGeographyAdapter(DbGeography geomValue)
		{
			this.value = geomValue;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000178BC File Offset: 0x00015ABC
		private TResult NullIfNotImplemented<TResult>(Func<DbGeography, TResult> accessor) where TResult : class
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

		// Token: 0x0600055C RID: 1372 RVA: 0x000178F8 File Offset: 0x00015AF8
		private int? NullIfNotImplemented(Func<DbGeography, int> accessor)
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00017938 File Offset: 0x00015B38
		public bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001793B File Offset: 0x00015B3B
		public PrimitiveTypeKind PrimitiveType
		{
			get
			{
				return PrimitiveTypeKind.Geography;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001793F File Offset: 0x00015B3F
		public object ProviderValue
		{
			get
			{
				return this.NullIfNotImplemented<object>((DbGeography geog) => geog.ProviderValue);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00017966 File Offset: 0x00015B66
		public int? CoordinateSystemId
		{
			get
			{
				return this.NullIfNotImplemented((DbGeography geog) => geog.CoordinateSystemId);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00017990 File Offset: 0x00015B90
		public string WellKnownText
		{
			get
			{
				string result;
				if ((result = this.NullIfNotImplemented<string>((DbGeography geog) => geog.AsTextIncludingElevationAndMeasure())) == null)
				{
					result = this.NullIfNotImplemented<string>((DbGeography geog) => geog.AsText());
				}
				return result;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000179EB File Offset: 0x00015BEB
		public byte[] WellKnownBinary
		{
			get
			{
				return this.NullIfNotImplemented<byte[]>((DbGeography geog) => geog.AsBinary());
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00017A12 File Offset: 0x00015C12
		public string GmlString
		{
			get
			{
				return this.NullIfNotImplemented<string>((DbGeography geog) => geog.AsGml());
			}
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00017A39 File Offset: 0x00015C39
		public Exception NotSqlCompatible()
		{
			return EntityUtil.GeographyValueNotSqlCompatible();
		}

		// Token: 0x04000746 RID: 1862
		private readonly DbGeography value;
	}
}
