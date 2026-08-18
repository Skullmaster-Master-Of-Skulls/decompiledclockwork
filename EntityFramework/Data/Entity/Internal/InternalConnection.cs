using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200076A RID: 1898
	internal abstract class InternalConnection : IInternalConnection, IDisposable
	{
		// Token: 0x06005597 RID: 21911 RVA: 0x001744E3 File Offset: 0x001726E3
		public InternalConnection(DbInterceptionContext interceptionContext)
		{
			this.InterceptionContext = (interceptionContext ?? new DbInterceptionContext());
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06005598 RID: 21912 RVA: 0x001744FB File Offset: 0x001726FB
		// (set) Token: 0x06005599 RID: 21913 RVA: 0x00174503 File Offset: 0x00172703
		private protected DbInterceptionContext InterceptionContext { protected get; private set; }

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x0017450C File Offset: 0x0017270C
		public virtual DbConnection Connection
		{
			get
			{
				EntityConnection entityConnection = this.UnderlyingConnection as EntityConnection;
				if (entityConnection == null)
				{
					return this.UnderlyingConnection;
				}
				return entityConnection.StoreConnection;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x0600559B RID: 21915 RVA: 0x00174538 File Offset: 0x00172738
		public virtual string ConnectionKey
		{
			get
			{
				string result;
				if ((result = this._key) == null)
				{
					result = (this._key = string.Format(CultureInfo.InvariantCulture, "{0};{1}", new object[]
					{
						this.UnderlyingConnection.GetType(),
						this.OriginalConnectionString
					}));
				}
				return result;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x0600559C RID: 21916 RVA: 0x00174586 File Offset: 0x00172786
		public virtual bool ConnectionHasModel
		{
			get
			{
				return this.UnderlyingConnection is EntityConnection;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x0600559D RID: 21917
		public abstract DbConnectionStringOrigin ConnectionStringOrigin { get; }

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x0600559E RID: 21918 RVA: 0x00174596 File Offset: 0x00172796
		// (set) Token: 0x0600559F RID: 21919 RVA: 0x0017459E File Offset: 0x0017279E
		public virtual AppConfig AppConfig { get; set; }

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x001745A8 File Offset: 0x001727A8
		// (set) Token: 0x060055A1 RID: 21921 RVA: 0x001745DE File Offset: 0x001727DE
		public virtual string ProviderName
		{
			get
			{
				string result;
				if ((result = this._providerName) == null)
				{
					result = (this._providerName = ((this.UnderlyingConnection == null) ? null : this.Connection.GetProviderInvariantName()));
				}
				return result;
			}
			set
			{
				this._providerName = value;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060055A2 RID: 21922 RVA: 0x001745E7 File Offset: 0x001727E7
		public virtual string ConnectionStringName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x001745EC File Offset: 0x001727EC
		public virtual string OriginalConnectionString
		{
			get
			{
				string b = (this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.Database : DbInterception.Dispatch.Connection.GetDatabase(this.UnderlyingConnection, this.InterceptionContext);
				string b2 = (this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.DataSource : DbInterception.Dispatch.Connection.GetDataSource(this.UnderlyingConnection, this.InterceptionContext);
				if (!string.Equals(this._originalDatabaseName, b, StringComparison.OrdinalIgnoreCase) || !string.Equals(this._originalDataSource, b2, StringComparison.OrdinalIgnoreCase))
				{
					this.OnConnectionInitialized();
				}
				return this._originalConnectionString;
			}
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x00174690 File Offset: 0x00172890
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public virtual ObjectContext CreateObjectContextFromConnectionModel()
		{
			ObjectContext objectContext = new ObjectContext((EntityConnection)this.UnderlyingConnection);
			ReadOnlyCollection<EntityContainer> items = objectContext.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace);
			if (items.Count == 1)
			{
				objectContext.DefaultContainerName = items.Single<EntityContainer>().Name;
			}
			return objectContext;
		}

		// Token: 0x060055A5 RID: 21925
		public abstract void Dispose();

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060055A6 RID: 21926 RVA: 0x001746D6 File Offset: 0x001728D6
		// (set) Token: 0x060055A7 RID: 21927 RVA: 0x001746DE File Offset: 0x001728DE
		protected DbConnection UnderlyingConnection { get; set; }

		// Token: 0x060055A8 RID: 21928 RVA: 0x001746E8 File Offset: 0x001728E8
		protected void OnConnectionInitialized()
		{
			this._originalConnectionString = InternalConnection.GetStoreConnectionString(this.UnderlyingConnection);
			try
			{
				this._originalDatabaseName = ((this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.Database : DbInterception.Dispatch.Connection.GetDatabase(this.UnderlyingConnection, this.InterceptionContext));
			}
			catch (NotImplementedException)
			{
			}
			try
			{
				this._originalDataSource = ((this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.DataSource : DbInterception.Dispatch.Connection.GetDataSource(this.UnderlyingConnection, this.InterceptionContext));
			}
			catch (NotImplementedException)
			{
			}
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x001747A4 File Offset: 0x001729A4
		public static string GetStoreConnectionString(DbConnection connection)
		{
			EntityConnection entityConnection = connection as EntityConnection;
			string result;
			if (entityConnection != null)
			{
				connection = entityConnection.StoreConnection;
				result = ((connection != null) ? DbInterception.Dispatch.Connection.GetConnectionString(connection, new DbInterceptionContext()) : null);
			}
			else
			{
				result = DbInterception.Dispatch.Connection.GetConnectionString(connection, new DbInterceptionContext());
			}
			return result;
		}

		// Token: 0x040022C3 RID: 8899
		private string _key;

		// Token: 0x040022C4 RID: 8900
		private string _providerName;

		// Token: 0x040022C5 RID: 8901
		private string _originalConnectionString;

		// Token: 0x040022C6 RID: 8902
		private string _originalDatabaseName;

		// Token: 0x040022C7 RID: 8903
		private string _originalDataSource;
	}
}
