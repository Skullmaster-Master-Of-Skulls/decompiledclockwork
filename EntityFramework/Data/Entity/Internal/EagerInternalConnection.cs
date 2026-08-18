using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200076B RID: 1899
	internal class EagerInternalConnection : InternalConnection
	{
		// Token: 0x060055AA RID: 21930 RVA: 0x001747F8 File Offset: 0x001729F8
		public EagerInternalConnection(DbContext context, DbConnection existingConnection, bool connectionOwned) : base(new DbInterceptionContext().WithDbContext(context))
		{
			base.UnderlyingConnection = existingConnection;
			this._connectionOwned = connectionOwned;
			base.OnConnectionInitialized();
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x0017481F File Offset: 0x00172A1F
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				return DbConnectionStringOrigin.UserCode;
			}
		}

		// Token: 0x060055AC RID: 21932 RVA: 0x00174822 File Offset: 0x00172A22
		public override void Dispose()
		{
			if (this._connectionOwned)
			{
				if (base.UnderlyingConnection is EntityConnection)
				{
					base.UnderlyingConnection.Dispose();
					return;
				}
				DbInterception.Dispatch.Connection.Dispose(base.UnderlyingConnection, base.InterceptionContext);
			}
		}

		// Token: 0x040022CB RID: 8907
		private readonly bool _connectionOwned;
	}
}
