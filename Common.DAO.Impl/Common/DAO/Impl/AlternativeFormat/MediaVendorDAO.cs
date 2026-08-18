using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat
{
	// Token: 0x0200016C RID: 364
	public class MediaVendorDAO : IMediaVendorDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x00074CAE File Offset: 0x00072EAE
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x00074CB6 File Offset: 0x00072EB6
		public OperationContext OpContext { get; set; }

		// Token: 0x06000B03 RID: 2819 RVA: 0x00074CBF File Offset: 0x00072EBF
		public MediaVendorDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00074CD4 File Offset: 0x00072ED4
		public int CreateMediaVendor(MediaVendor vendor)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@vendorid", DbType.Int32, 0),
				databaseLayer.GetParameter("@vendorname", DbType.String, vendor.Name),
				databaseLayer.GetParameter("@vendordescription", DbType.String, vendor.Description ?? string.Empty),
				databaseLayer.GetParameter("@vendornotes", DbType.String, vendor.Notes ?? string.Empty),
				databaseLayer.GetParameter("@vendorphone", DbType.String, vendor.Phone ?? string.Empty),
				databaseLayer.GetParameter("@vendorcellphone", DbType.String, vendor.Cellphone ?? string.Empty),
				databaseLayer.GetParameter("@vendoraddress", DbType.String, vendor.Address ?? string.Empty),
				databaseLayer.GetParameter("@vendorfax", DbType.String, vendor.Fax ?? string.Empty),
				databaseLayer.GetParameter("@vendoremail", DbType.String, vendor.Email ?? string.Empty),
				databaseLayer.GetParameter("@vendorwebsite", DbType.String, vendor.Website ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("SET @vendorid = 0\r\n\r\nIF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Vendor] WHERE VendorName=@vendorname)\r\nBEGIN\r\n\tINSERT INTO [AlternativeFormat_Vendor]\r\n            ([VendorName]\r\n            ,[VendorDescription]\r\n            ,[VendorNotes]\r\n            ,[VendorPhone]\r\n            ,[VendorCellPhone]\r\n            ,[VendorAddress]\r\n            ,[VendorFax]\r\n            ,[VendorEmail]\r\n            ,[VendorWebSite])\r\n        VALUES\r\n            (@vendorname\r\n            ,@vendordescription\r\n            ,@vendornotes\r\n            ,@vendorphone\r\n            ,@vendorcellphone\r\n            ,@vendoraddress\r\n            ,@vendorfax\r\n            ,@vendoremail\r\n            ,@vendorwebsite)\r\n\r\n    set @vendorid = SCOPE_IDENTITY()\r\nEND", array);
			return vendor.Id = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00074E60 File Offset: 0x00073060
		public bool UpdateMediaVendor(MediaVendor vendor)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@vendorid", DbType.Int32, vendor.VendorId),
				databaseLayer.GetParameter("@vendorname", DbType.String, vendor.Name),
				databaseLayer.GetParameter("@vendordescription", DbType.String, vendor.Description ?? string.Empty),
				databaseLayer.GetParameter("@vendornotes", DbType.String, vendor.Notes ?? string.Empty),
				databaseLayer.GetParameter("@vendorphone", DbType.String, vendor.Phone ?? string.Empty),
				databaseLayer.GetParameter("@vendorcellphone", DbType.String, vendor.Cellphone ?? string.Empty),
				databaseLayer.GetParameter("@vendoraddress", DbType.String, vendor.Address ?? string.Empty),
				databaseLayer.GetParameter("@vendorfax", DbType.String, vendor.Fax ?? string.Empty),
				databaseLayer.GetParameter("@vendoremail", DbType.String, vendor.Email ?? string.Empty),
				databaseLayer.GetParameter("@vendorwebsite", DbType.String, vendor.Website ?? string.Empty)
			};
			return databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Vendor] WHERE VendorName=@vendorname and VendorId <> @vendorid)\r\nBEGIN\r\n\tUPDATE [AlternativeFormat_Vendor]\r\n    SET [VendorName] = @vendorname\r\n        ,[VendorDescription] = @vendordescription\r\n        ,[VendorNotes] = @vendornotes\r\n        ,[VendorPhone] = @vendorphone\r\n        ,[VendorCellPhone] = @vendorcellphone\r\n        ,[VendorAddress] = @vendoraddress\r\n        ,[VendorFax] = @vendorfax\r\n        ,[VendorEmail] = @vendoremail\r\n        ,[VendorWebSite] = @vendorwebsite\r\n    WHERE VendorId = @vendorid\r\nEND", parameters) > 0;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00074FCC File Offset: 0x000731CC
		public void DeleteMediaVendor(int vendorId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@vendorid", DbType.Int32, vendorId);
			databaseLayer.ExecuteNonQuery("delete from AlternativeFormat_Vendor where vendorid=@vendorid", new DbParameter[]
			{
				parameter
			});
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00075020 File Offset: 0x00073220
		public MediaVendor LoadMediaVendorById(int vendorId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@vendorid", DbType.Int32, vendorId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from AlternativeFormat_Vendor where vendorid=@vendorid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetVendorFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000750B4 File Offset: 0x000732B4
		public MediaVendor LoadMediaVendorByName(string vendorName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@vendorname", DbType.String, vendorName);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from AlternativeFormat_Vendor where vendorname=@vendorname", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetVendorFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00075144 File Offset: 0x00073344
		public IList<MediaVendor> LoadAllMediaVendors()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaVendor> list = new List<MediaVendor>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select * from AlternativeFormat_Vendor"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaVendor vendorFromReader = this.GetVendorFromReader(dataReader);
						bool flag2 = vendorFromReader != null;
						if (flag2)
						{
							list.Add(vendorFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000751DC File Offset: 0x000733DC
		private MediaVendor GetVendorFromReader(IDataReader record)
		{
			return new MediaVendor
			{
				VendorId = (int)record["vendorid"],
				Name = (string)record["vendorname"],
				Description = (string)record["vendordescription"],
				Notes = (string)record["vendornotes"],
				Phone = (string)record["vendorphone"],
				Cellphone = (string)record["vendorcellphone"],
				Address = (string)record["vendoraddress"],
				Fax = (string)record["vendorfax"],
				Email = (string)record["vendoremail"],
				Website = (string)record["vendorwebsite"]
			};
		}
	}
}
