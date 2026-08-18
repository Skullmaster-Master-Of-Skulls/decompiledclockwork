using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x0200000C RID: 12
	public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> where TUser : IdentityUser
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00006FC7 File Offset: 0x000051C7
		public IdentityDbContext() : this("DefaultConnection")
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006FD4 File Offset: 0x000051D4
		public IdentityDbContext(string nameOrConnectionString) : this(nameOrConnectionString, true)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006FDE File Offset: 0x000051DE
		public IdentityDbContext(string nameOrConnectionString, bool throwIfV1Schema) : base(nameOrConnectionString)
		{
			if (throwIfV1Schema && IdentityDbContext<TUser>.IsIdentityV1Schema(this))
			{
				throw new InvalidOperationException(IdentityResources.IdentityV1SchemaError);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006FFD File Offset: 0x000051FD
		public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00007008 File Offset: 0x00005208
		public IdentityDbContext(DbCompiledModel model) : base(model)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00007011 File Offset: 0x00005211
		public IdentityDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000701B File Offset: 0x0000521B
		public IdentityDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007028 File Offset: 0x00005228
		internal static bool IsIdentityV1Schema(DbContext db)
		{
			SqlConnection sqlConnection = db.Database.Connection as SqlConnection;
			if (sqlConnection == null)
			{
				return false;
			}
			if (db.Database.Exists())
			{
				using (SqlConnection sqlConnection2 = new SqlConnection(sqlConnection.ConnectionString))
				{
					sqlConnection2.Open();
					return IdentityDbContext<TUser>.VerifyColumns(sqlConnection2, "AspNetUsers", new string[]
					{
						"Id",
						"UserName",
						"PasswordHash",
						"SecurityStamp",
						"Discriminator"
					}) && IdentityDbContext<TUser>.VerifyColumns(sqlConnection2, "AspNetRoles", new string[]
					{
						"Id",
						"Name"
					}) && IdentityDbContext<TUser>.VerifyColumns(sqlConnection2, "AspNetUserRoles", new string[]
					{
						"UserId",
						"RoleId"
					}) && IdentityDbContext<TUser>.VerifyColumns(sqlConnection2, "AspNetUserClaims", new string[]
					{
						"Id",
						"ClaimType",
						"ClaimValue",
						"User_Id"
					}) && IdentityDbContext<TUser>.VerifyColumns(sqlConnection2, "AspNetUserLogins", new string[]
					{
						"UserId",
						"ProviderKey",
						"LoginProvider"
					});
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000719C File Offset: 0x0000539C
		internal static bool VerifyColumns(SqlConnection conn, string table, params string[] columns)
		{
			List<string> list = new List<string>();
			using (SqlCommand sqlCommand = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table", conn))
			{
				sqlCommand.Parameters.Add(new SqlParameter("Table", table));
				using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
				{
					while (sqlDataReader.Read())
					{
						list.Add(sqlDataReader.GetString(0));
					}
				}
			}
			return columns.All(new Func<string, bool>(list.Contains));
		}
	}
}
