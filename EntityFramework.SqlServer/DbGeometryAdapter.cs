using System;
using System.Data.Entity.Core;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000F RID: 15
	internal class DbGeometryAdapter : IDbSpatialValue
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000040BA File Offset: 0x000022BA
		internal DbGeometryAdapter(DbGeometry value)
		{
			this._value = value;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000040C9 File Offset: 0x000022C9
		public bool IsGeography
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000040D9 File Offset: 0x000022D9
		public object ProviderValue
		{
			get
			{
				return (() => this._value.ProviderValue).NullIfNotImplemented<object>();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000040FE File Offset: 0x000022FE
		public int? CoordinateSystemId
		{
			get
			{
				return (() => new int?(this._value.CoordinateSystemId)).NullIfNotImplemented<int?>();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004136 File Offset: 0x00002336
		public string WellKnownText
		{
			get
			{
				return (() => this._value.Provider.AsTextIncludingElevationAndMeasure(this._value)).NullIfNotImplemented<string>() ?? (() => this._value.AsText()).NullIfNotImplemented<string>();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000416B File Offset: 0x0000236B
		public byte[] WellKnownBinary
		{
			get
			{
				return (() => this._value.AsBinary()).NullIfNotImplemented<byte[]>();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000418B File Offset: 0x0000238B
		public string GmlString
		{
			get
			{
				return (() => this._value.AsGml()).NullIfNotImplemented<string>();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000419E File Offset: 0x0000239E
		public Exception NotSqlCompatible()
		{
			return new ProviderIncompatibleException(Strings.SqlProvider_GeometryValueNotSqlCompatible);
		}

		// Token: 0x04000017 RID: 23
		private readonly DbGeometry _value;
	}
}
