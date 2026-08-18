using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200076D RID: 1901
	internal class EagerInternalContext : InternalContext
	{
		// Token: 0x0600562B RID: 22059 RVA: 0x00175D11 File Offset: 0x00173F11
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public EagerInternalContext(DbContext owner) : base(owner, null)
		{
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x00175D1C File Offset: 0x00173F1C
		public EagerInternalContext(DbContext owner, ObjectContext objectContext, bool objectContextOwned) : base(owner, null)
		{
			this._objectContext = objectContext;
			this._objectContextOwned = objectContextOwned;
			this._originalConnectionString = InternalConnection.GetStoreConnectionString(this._objectContext.Connection);
			this._objectContext.InterceptionContext = this._objectContext.InterceptionContext.WithDbContext(owner);
			base.ResetDbSets();
			this._objectContext.InitializeMappingViewCacheFactory(base.Owner);
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x0600562D RID: 22061 RVA: 0x00175D88 File Offset: 0x00173F88
		public override ObjectContext ObjectContext
		{
			get
			{
				base.Initialize();
				return this.ObjectContextInUse;
			}
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x00175D96 File Offset: 0x00173F96
		public override ObjectContext GetObjectContextWithoutDatabaseInitialization()
		{
			this.InitializeContext();
			return this.ObjectContextInUse;
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x0600562F RID: 22063 RVA: 0x00175DA4 File Offset: 0x00173FA4
		private ObjectContext ObjectContextInUse
		{
			get
			{
				return base.TempObjectContext ?? this._objectContext;
			}
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x00175DB6 File Offset: 0x00173FB6
		protected override void InitializeContext()
		{
			base.CheckContextNotDisposed();
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x00175DBE File Offset: 0x00173FBE
		public override void MarkDatabaseNotInitialized()
		{
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x00175DC0 File Offset: 0x00173FC0
		public override void MarkDatabaseInitialized()
		{
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x00175DC2 File Offset: 0x00173FC2
		protected override void InitializeDatabase()
		{
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06005634 RID: 22068 RVA: 0x00175DC4 File Offset: 0x00173FC4
		public override IDatabaseInitializer<DbContext> DefaultInitializer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00175DC7 File Offset: 0x00173FC7
		public override void DisposeContext(bool disposing)
		{
			if (!base.IsDisposed)
			{
				base.DisposeContext(disposing);
				if (disposing && this._objectContextOwned)
				{
					this._objectContext.Dispose();
				}
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06005636 RID: 22070 RVA: 0x00175DEE File Offset: 0x00173FEE
		public override DbConnection Connection
		{
			get
			{
				base.CheckContextNotDisposed();
				return ((EntityConnection)this._objectContext.Connection).StoreConnection;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06005637 RID: 22071 RVA: 0x00175E0B File Offset: 0x0017400B
		public override string OriginalConnectionString
		{
			get
			{
				return this._originalConnectionString;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06005638 RID: 22072 RVA: 0x00175E13 File Offset: 0x00174013
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				return DbConnectionStringOrigin.UserCode;
			}
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x00175E16 File Offset: 0x00174016
		public override void OverrideConnection(IInternalConnection connection)
		{
			throw Error.EagerInternalContext_CannotSetConnectionInfo();
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x0600563A RID: 22074 RVA: 0x00175E1D File Offset: 0x0017401D
		// (set) Token: 0x0600563B RID: 22075 RVA: 0x00175E2F File Offset: 0x0017402F
		public override bool EnsureTransactionsForFunctionsAndCommands
		{
			get
			{
				return this.ObjectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands = value;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600563C RID: 22076 RVA: 0x00175E42 File Offset: 0x00174042
		// (set) Token: 0x0600563D RID: 22077 RVA: 0x00175E54 File Offset: 0x00174054
		public override bool LazyLoadingEnabled
		{
			get
			{
				return this.ObjectContextInUse.ContextOptions.LazyLoadingEnabled;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.LazyLoadingEnabled = value;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x0600563E RID: 22078 RVA: 0x00175E67 File Offset: 0x00174067
		// (set) Token: 0x0600563F RID: 22079 RVA: 0x00175E79 File Offset: 0x00174079
		public override bool ProxyCreationEnabled
		{
			get
			{
				return this.ObjectContextInUse.ContextOptions.ProxyCreationEnabled;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.ProxyCreationEnabled = value;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06005640 RID: 22080 RVA: 0x00175E8C File Offset: 0x0017408C
		// (set) Token: 0x06005641 RID: 22081 RVA: 0x00175EA1 File Offset: 0x001740A1
		public override bool UseDatabaseNullSemantics
		{
			get
			{
				return !this.ObjectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior;
			}
			set
			{
				this.ObjectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior = !value;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06005642 RID: 22082 RVA: 0x00175EB7 File Offset: 0x001740B7
		// (set) Token: 0x06005643 RID: 22083 RVA: 0x00175EC4 File Offset: 0x001740C4
		public override int? CommandTimeout
		{
			get
			{
				return this.ObjectContextInUse.CommandTimeout;
			}
			set
			{
				this.ObjectContextInUse.CommandTimeout = value;
			}
		}

		// Token: 0x040022EE RID: 8942
		private readonly ObjectContext _objectContext;

		// Token: 0x040022EF RID: 8943
		private readonly bool _objectContextOwned;

		// Token: 0x040022F0 RID: 8944
		private readonly string _originalConnectionString;
	}
}
