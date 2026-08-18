using System;
using System.Data.Common;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x020006B6 RID: 1718
	public sealed class LocalDbConnectionFactory : IDbConnectionFactory
	{
		// Token: 0x06004473 RID: 17523 RVA: 0x00144020 File Offset: 0x00142220
		public LocalDbConnectionFactory(string localDbVersion)
		{
			Check.NotEmpty(localDbVersion, "localDbVersion");
			this._localDbVersion = localDbVersion;
			this._baseConnectionString = "Integrated Security=True; MultipleActiveResultSets=True;";
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x00144046 File Offset: 0x00142246
		public LocalDbConnectionFactory(string localDbVersion, string baseConnectionString)
		{
			Check.NotEmpty(localDbVersion, "localDbVersion");
			Check.NotNull<string>(baseConnectionString, "baseConnectionString");
			this._localDbVersion = localDbVersion;
			this._baseConnectionString = baseConnectionString;
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06004475 RID: 17525 RVA: 0x00144074 File Offset: 0x00142274
		public string BaseConnectionString
		{
			get
			{
				return this._baseConnectionString;
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x0014407C File Offset: 0x0014227C
		public DbConnection CreateConnection(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			string text = string.IsNullOrEmpty(AppDomain.CurrentDomain.GetData("DataDirectory") as string) ? " " : string.Format(CultureInfo.InvariantCulture, " AttachDbFilename=|DataDirectory|{0}.mdf; ", new object[]
			{
				nameOrConnectionString
			});
			return new SqlConnectionFactory(string.Format(CultureInfo.InvariantCulture, "Data Source=(localdb)\\{1};{0};{2}", new object[]
			{
				this._baseConnectionString,
				this._localDbVersion,
				text
			})).CreateConnection(nameOrConnectionString);
		}

		// Token: 0x04001934 RID: 6452
		private readonly string _baseConnectionString;

		// Token: 0x04001935 RID: 6453
		private readonly string _localDbVersion;
	}
}
