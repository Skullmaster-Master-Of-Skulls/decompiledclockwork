using System;
using System.Data.Entity.Core;
using System.Data.Entity.Spatial;
using System.Data.Entity.SqlServer.Resources;
using System.Data.Entity.SqlServer.Utilities;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000E RID: 14
	internal class DbGeographyAdapter : IDbSpatialValue
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003FCA File Offset: 0x000021CA
		internal DbGeographyAdapter(DbGeography value)
		{
			this._value = value;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003FD9 File Offset: 0x000021D9
		public bool IsGeography
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003FE9 File Offset: 0x000021E9
		public object ProviderValue
		{
			get
			{
				return (() => this._value.ProviderValue).NullIfNotImplemented<object>();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000400E File Offset: 0x0000220E
		public int? CoordinateSystemId
		{
			get
			{
				return (() => new int?(this._value.CoordinateSystemId)).NullIfNotImplemented<int?>();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004046 File Offset: 0x00002246
		public string WellKnownText
		{
			get
			{
				return (() => this._value.Provider.AsTextIncludingElevationAndMeasure(this._value)).NullIfNotImplemented<string>() ?? (() => this._value.AsText()).NullIfNotImplemented<string>();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000407B File Offset: 0x0000227B
		public byte[] WellKnownBinary
		{
			get
			{
				return (() => this._value.AsBinary()).NullIfNotImplemented<byte[]>();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000409B File Offset: 0x0000229B
		public string GmlString
		{
			get
			{
				return (() => this._value.AsGml()).NullIfNotImplemented<string>();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000040AE File Offset: 0x000022AE
		public Exception NotSqlCompatible()
		{
			return new ProviderIncompatibleException(Strings.SqlProvider_GeographyValueNotSqlCompatible);
		}

		// Token: 0x04000016 RID: 22
		private readonly DbGeography _value;
	}
}
