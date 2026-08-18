using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000764 RID: 1892
	internal class DatabaseOperations
	{
		// Token: 0x06005550 RID: 21840 RVA: 0x00172EBC File Offset: 0x001710BC
		public virtual bool Create(ObjectContext objectContext)
		{
			objectContext.CreateDatabase();
			return true;
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x00172EC8 File Offset: 0x001710C8
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public virtual bool Exists(DbConnection connection, int? commandTimeout, Lazy<StoreItemCollection> storeItemCollection)
		{
			if (connection.State == ConnectionState.Open)
			{
				return true;
			}
			bool result;
			try
			{
				result = DbProviderServices.GetProviderServices(connection).DatabaseExists(connection, commandTimeout, storeItemCollection);
			}
			catch
			{
				try
				{
					connection.Open();
					result = true;
				}
				catch (Exception)
				{
					result = false;
				}
				finally
				{
					connection.Close();
				}
			}
			return result;
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x00172F34 File Offset: 0x00171134
		public virtual void Delete(ObjectContext objectContext)
		{
			objectContext.DeleteDatabase();
		}
	}
}
