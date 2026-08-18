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
	// Token: 0x0200016B RID: 363
	public class MediaPublisherDAO : IMediaPublisherDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000746E2 File Offset: 0x000728E2
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x000746EA File Offset: 0x000728EA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000746F3 File Offset: 0x000728F3
		public MediaPublisherDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00074708 File Offset: 0x00072908
		public int CreatePublisher(MediaPublisher publisher)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@publisherid", DbType.Int32, 0),
				databaseLayer.GetParameter("@publishername", DbType.String, publisher.Name),
				databaseLayer.GetParameter("@publisherdescription", DbType.String, publisher.Description ?? string.Empty),
				databaseLayer.GetParameter("@publishernotes", DbType.String, publisher.Notes ?? string.Empty),
				databaseLayer.GetParameter("@publisherphone", DbType.String, publisher.Phone ?? string.Empty),
				databaseLayer.GetParameter("@publisheraddress", DbType.String, publisher.Address ?? string.Empty),
				databaseLayer.GetParameter("@publisherfax", DbType.String, publisher.Fax ?? string.Empty),
				databaseLayer.GetParameter("@publisheremail", DbType.String, publisher.Email ?? string.Empty),
				databaseLayer.GetParameter("@publisherwebsite", DbType.String, publisher.Website ?? string.Empty)
			};
			databaseLayer.ExecuteNonQuery("SET @publisherid = 0\r\n\r\nIF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Publisher] WHERE PublisherName=@publishername)\r\nBEGIN\r\n\tINSERT INTO [AlternativeFormat_Publisher]\r\n        ([PublisherName]\r\n        ,[PublisherDescription]\r\n        ,[PublisherNotes]\r\n        ,[PublisherPhone]\r\n        ,[PublisherAddress]\r\n        ,[PublisherFax]\r\n        ,[PublisherEmail]\r\n        ,[PublisherWebsite])\r\n    VALUES\r\n        (@publishername\r\n        ,@publisherdescription\r\n        ,@publishernotes\r\n        ,@publisherphone\r\n        ,@publisheraddress\r\n        ,@publisherfax\r\n        ,@publisheremail\r\n        ,@publisherwebsite)\r\n\r\n\tSET @publisherid = SCOPE_IDENTITY()\r\nEND", array);
			return publisher.Id = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00074874 File Offset: 0x00072A74
		public bool UpdatePublisher(MediaPublisher publisher)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@publisherid", DbType.Int32, publisher.PublisherId),
				databaseLayer.GetParameter("@publishername", DbType.String, publisher.Name),
				databaseLayer.GetParameter("@publisherdescription", DbType.String, publisher.Description ?? string.Empty),
				databaseLayer.GetParameter("@publishernotes", DbType.String, publisher.Notes ?? string.Empty),
				databaseLayer.GetParameter("@publisherphone", DbType.String, publisher.Phone ?? string.Empty),
				databaseLayer.GetParameter("@publisheraddress", DbType.String, publisher.Address ?? string.Empty),
				databaseLayer.GetParameter("@publisherfax", DbType.String, publisher.Fax ?? string.Empty),
				databaseLayer.GetParameter("@publisheremail", DbType.String, publisher.Email ?? string.Empty),
				databaseLayer.GetParameter("@publisherwebsite", DbType.String, publisher.Website)
			};
			return databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT 1 FROM [AlternativeFormat_Publisher] WHERE PublisherName=@publishername and PublisherId <> @publisherid)\r\nBEGIN\r\n\tupdate [AlternativeFormat_Publisher]\r\n    set   [PublisherName]=@publishername\r\n        ,[PublisherDescription]=@publisherdescription\r\n        ,[PublisherNotes]=@publishernotes\r\n        ,[PublisherPhone]=@publisherphone\r\n        ,[PublisherAddress]=@publisheraddress\r\n        ,[PublisherFax]=@publisherfax\r\n        ,[PublisherEmail]=@publisheremail\r\n        ,[PublisherWebsite]=@publisherwebsite\r\n    where PublisherId=@publisherid\r\nEND", parameters) > 0;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000749B8 File Offset: 0x00072BB8
		public bool DeletePublisher(int publisherId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@publisherid", DbType.Int32, publisherId);
			return databaseLayer.ExecuteNonQuery("if not exists (select 1 from AlternativeFormat_MediaContent where IsActive=1 AND PublisherID=@publisherid)\r\nbegin\r\n\tdelete from AlternativeFormat_Publisher where publisherid=@publisherid\r\nend", new DbParameter[]
			{
				parameter
			}) > 0;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00074A10 File Offset: 0x00072C10
		public MediaPublisher LoadPublisherById(int publisherId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@publisherid", DbType.Int32, publisherId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from AlternativeFormat_Publisher where publisherid=@publisherid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return MediaPublisherDAO.GetPublisherFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00074AA4 File Offset: 0x00072CA4
		public MediaPublisher LoadPublisherByName(string publisherName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@publishername", DbType.String, publisherName);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from AlternativeFormat_Publisher where publishername=@publishername", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return MediaPublisherDAO.GetPublisherFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00074B30 File Offset: 0x00072D30
		public IList<MediaPublisher> LoadAllPublishers()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaPublisher> list = new List<MediaPublisher>();
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from AlternativeFormat_Publisher"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaPublisher publisherFromReader = MediaPublisherDAO.GetPublisherFromReader(dataReader);
						bool flag2 = publisherFromReader != null;
						if (flag2)
						{
							list.Add(publisherFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00074BC8 File Offset: 0x00072DC8
		internal static MediaPublisher GetPublisherFromReader(IDataReader record)
		{
			return new MediaPublisher
			{
				PublisherId = (int)record["publisherid"],
				Name = (string)record["publishername"],
				Description = (string)record["publisherdescription"],
				Notes = (string)record["publishernotes"],
				Phone = (string)record["publisherphone"],
				Address = (string)record["publisheraddress"],
				Email = (string)record["publisheremail"],
				Fax = (string)record["publisherfax"],
				Website = (string)record["publisherwebsite"]
			};
		}
	}
}
