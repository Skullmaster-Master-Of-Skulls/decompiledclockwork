using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Exceptions;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat
{
	// Token: 0x0200016A RID: 362
	public class MediaContentDAO : IMediaContentDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000729BD File Offset: 0x00070BBD
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x000729C5 File Offset: 0x00070BC5
		public OperationContext OpContext { get; set; }

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public MediaContentDAO()
		{
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000729CE File Offset: 0x00070BCE
		public MediaContentDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000729E0 File Offset: 0x00070BE0
		public IList<MediaContent> GetMediaContentMatchingUsingEquivalentCoursesAlt(string searchText, int lucourseid = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			DbParameter[] array;
			if (lucourseid <= 0)
			{
				(array = new DbParameter[1])[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
			}
			else
			{
				DbParameter[] array2 = new DbParameter[2];
				array2[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
				array = array2;
				array2[1] = databaseLayer.GetParameter("@lucourseid", DbType.Int32, lucourseid);
			}
			DbParameter[] parameters = array;
			string storeProcedureName = (lucourseid > 0) ? "sp_AlternateFormat_SearchMediaContentUsingEquivalentCoursesAlt" : "sp_AlternateFormat_SearchMediaContent";
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader(storeProcedureName, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, true);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00072AE8 File Offset: 0x00070CE8
		public IList<MediaContent> GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAlt(string searchText, int lucourseid = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			DbParameter[] array;
			if (lucourseid <= 0)
			{
				(array = new DbParameter[1])[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
			}
			else
			{
				DbParameter[] array2 = new DbParameter[2];
				array2[0] = databaseLayer.GetParameter("@searchtext", DbType.String, searchText);
				array = array2;
				array2[1] = databaseLayer.GetParameter("@lucourseid", DbType.Int32, lucourseid);
			}
			DbParameter[] parameters = array;
			string storeProcedureName = (lucourseid > 0) ? "sp_AlternateFormat_SearchMediaContentUsingEquivalentCoursesAlt_UserDefined" : "sp_AlternateFormat_SearchMediaContent";
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader(storeProcedureName, parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, true);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00072BF0 File Offset: 0x00070DF0
		[DebuggerStepThrough]
		public Task<IList<MediaContent>> GetMediaContentMatchingUsingEquivalentCoursesAltAsync(string searchText, int lucourseid = 0)
		{
			MediaContentDAO.<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__8 <GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__ = new MediaContentDAO.<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__8();
			<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContent>>.Create();
			<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__.<>4__this = this;
			<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__.searchText = searchText;
			<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__.lucourseid = lucourseid;
			<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__.<>1__state = -1;
			<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__.<>t__builder.Start<MediaContentDAO.<GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__8>(ref <GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__);
			return <GetMediaContentMatchingUsingEquivalentCoursesAltAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00072C44 File Offset: 0x00070E44
		[DebuggerStepThrough]
		public Task<IList<MediaContent>> GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync(string searchText, int lucourseid = 0)
		{
			MediaContentDAO.<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__9 <GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__ = new MediaContentDAO.<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__9();
			<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<MediaContent>>.Create();
			<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__.<>4__this = this;
			<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__.searchText = searchText;
			<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__.lucourseid = lucourseid;
			<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__.<>1__state = -1;
			<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__.<>t__builder.Start<MediaContentDAO.<GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__9>(ref <GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__);
			return <GetMediaContentMatchingUsingUserDefinedEquivalentCoursesAltAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00072C98 File Offset: 0x00070E98
		public MediaContent LoadMediaContentById(Guid mediaContentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediaContentid", DbType.Guid, mediaContentId);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentById", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetMediaContentFromReader<MediaContent>(dataReader, null, true);
				}
			}
			return null;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00072D2C File Offset: 0x00070F2C
		public MediaContent LoadMediaContentByISBN(string isbn)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@isbn", DbType.String, isbn);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentByISBN", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetMediaContentFromReader<MediaContent>(dataReader, null, true);
				}
			}
			return null;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00072DBC File Offset: 0x00070FBC
		public IList<MediaContent> LoadMediaContentByCourseUsingEquivalentCoursesAlt(int courseId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			DbParameter parameter = databaseLayer.GetParameter("@lucourseid", DbType.Int32, courseId);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentByCourseUsingEquivalentCoursesAlt", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, true);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00072E84 File Offset: 0x00071084
		public IList<MediaContent> LoadMediaContentByCourseUsingUserDefinedEquivalentCoursesAlt(int courseId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			DbParameter parameter = databaseLayer.GetParameter("@lucourseid", DbType.Int32, courseId);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentByCourseUsingEquivalentCoursesAlt_UserDefined", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, true);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00072F4C File Offset: 0x0007114C
		public IList<MediaContent> LoadMediaContentByTitle(string title)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			DbParameter parameter = databaseLayer.GetParameter("@title", DbType.String, title);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentByTitle", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, true);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00073010 File Offset: 0x00071210
		public IList<MediaContent> LoadMediaContentByPublisher(int publisherId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			DbParameter parameter = databaseLayer.GetParameter("@publisherId", DbType.Int32, publisherId);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentByPublisher", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, true);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000730D8 File Offset: 0x000712D8
		public IList<MediaContent> LoadMediaContentByCategory(eMediaContentCategory mediaContentCategory)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			DbParameter parameter = databaseLayer.GetParameter("@mediaCategoryId", DbType.Int32, (int)mediaContentCategory);
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_MediaContentByCategory", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, true);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000731A0 File Offset: 0x000713A0
		public MediaContentIdentifier CreateMediaContent(MediaContent mediaContent)
		{
			MediaContentIdentifier identifier;
			try
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] array = new DbParameter[22];
				array[0] = databaseLayer.GetOutputParameter("@mediacontentdataid", DbType.Int32, 0);
				array[1] = databaseLayer.GetInOutParameter("@mediacontentid", DbType.Guid, (mediaContent.MediaContentUniqueId == Guid.Empty) ? Guid.NewGuid() : mediaContent.MediaContentUniqueId);
				array[2] = databaseLayer.GetParameter("@shorttitle", DbType.String, mediaContent.ShortTitle ?? string.Empty);
				array[3] = databaseLayer.GetParameter("@longtitle", DbType.String, mediaContent.LongTitle ?? string.Empty);
				int num = 4;
				DatabaseLayer databaseLayer2 = databaseLayer;
				string pName = "@authors";
				DbType pType = DbType.String;
				object value;
				if (mediaContent.Authors == null || mediaContent.Authors.Count <= 0)
				{
					value = string.Empty;
				}
				else
				{
					value = string.Join("|", (from s in mediaContent.Authors
					where !string.IsNullOrWhiteSpace(s)
					select s).ToArray<string>());
				}
				array[num] = databaseLayer2.GetParameter(pName, pType, value);
				array[5] = databaseLayer.GetParameter("@edition", DbType.String, mediaContent.Edition ?? string.Empty);
				array[6] = databaseLayer.GetParameter("@summary", DbType.String, mediaContent.Summary ?? string.Empty);
				array[7] = databaseLayer.GetParameter("@publisherid", DbType.Int32, (mediaContent.Publisher != null && mediaContent.Publisher.PublisherId > 0) ? mediaContent.Publisher.PublisherId : DBNull.Value);
				int num2 = 8;
				DatabaseLayer databaseLayer3 = databaseLayer;
				string pName2 = "@publisheddate";
				DbType pType2 = DbType.DateTime;
				DateTime? publishedDate = mediaContent.PublishedDate;
				array[num2] = databaseLayer3.GetParameter(pName2, pType2, (publishedDate != null) ? publishedDate.GetValueOrDefault() : DBNull.Value);
				array[9] = databaseLayer.GetParameter("@isbn", DbType.String, mediaContent.ISBN ?? string.Empty);
				array[10] = databaseLayer.GetParameter("@length", DbType.String, mediaContent.Length ?? string.Empty);
				array[11] = databaseLayer.GetParameter("@website", DbType.String, mediaContent.WebSite ?? string.Empty);
				array[12] = databaseLayer.GetParameter("@notes", DbType.String, mediaContent.Notes ?? string.Empty);
				array[13] = databaseLayer.GetParameter("@mediacontentcategory", DbType.String, mediaContent.ContentCategory.ToString());
				array[14] = databaseLayer.GetParameter("@proofofpurchaserequired", DbType.Boolean, mediaContent.ProofOfPurchaseRequired);
				int num3 = 15;
				DatabaseLayer databaseLayer4 = databaseLayer;
				string pName3 = "@whoentered";
				DbType pType3 = DbType.Int32;
				PersonBase whoEntered = mediaContent.WhoEntered;
				int num4;
				if (whoEntered == null)
				{
					OperationContext opContext2 = this.OpContext;
					num4 = ((opContext2 != null) ? opContext2.WhoAmI : 0);
				}
				else
				{
					num4 = whoEntered.PersonId;
				}
				array[num3] = databaseLayer4.GetParameter(pName3, pType3, num4);
				array[16] = databaseLayer.GetParameter("@datecreated", DbType.DateTime, mediaContent.DateCreated);
				array[17] = databaseLayer.GetParameter("@isactive", DbType.Boolean, mediaContent.IsActive);
				array[18] = databaseLayer.GetParameter("@courses", DbType.String, (mediaContent.CourseIdList != null && mediaContent.CourseIdList.Count > 0) ? mediaContent.CourseIdList.ToList<int>().CommaSeparatedValuesWithoutSpace<int>() : string.Empty);
				array[19] = databaseLayer.GetParameter("@externalid", DbType.String, mediaContent.ExternalId ?? string.Empty);
				array[20] = databaseLayer.GetParameter("@externalsourceprovider", DbType.String, mediaContent.ExternalSourceProvider ?? string.Empty);
				array[21] = databaseLayer.GetParameter("@thumbnailimageurl", DbType.String, mediaContent.ThumbnailImageUrl ?? string.Empty);
				DbParameter[] array2 = array;
				databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_CreateMediaContent", array2);
				mediaContent.Identifier.MediaContentId = ((array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value));
				mediaContent.Identifier.MediaContentUniqueId = new Guid?((array2[1].Value is DBNull) ? Guid.Empty : ((Guid)array2[1].Value));
				identifier = mediaContent.Identifier;
			}
			catch (Exception innerEx)
			{
				throw new DataAccessLayerException("Exception when a media content is been created", innerEx);
			}
			return identifier;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000735D8 File Offset: 0x000717D8
		public void UpdateMediaContent(MediaContent mediaContent)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContent.MediaContentUniqueId),
				databaseLayer.GetParameter("@mediacontentdataid", DbType.Int32, mediaContent.MediaContentDataID),
				databaseLayer.GetParameter("@shorttitle", DbType.String, mediaContent.ShortTitle ?? string.Empty),
				databaseLayer.GetParameter("@longtitle", DbType.String, mediaContent.LongTitle ?? string.Empty),
				databaseLayer.GetParameter("@authors", DbType.String, (mediaContent.Authors != null && mediaContent.Authors.Count > 0) ? string.Join("|", mediaContent.Authors.ToArray<string>()) : string.Empty),
				databaseLayer.GetParameter("@edition", DbType.String, mediaContent.Edition ?? string.Empty),
				databaseLayer.GetParameter("@summary", DbType.String, mediaContent.Summary ?? string.Empty),
				databaseLayer.GetParameter("@publisherid", DbType.Int32, (mediaContent.Publisher != null) ? mediaContent.Publisher.PublisherId : DBNull.Value),
				databaseLayer.GetParameter("@publisheddate", DbType.DateTime, (mediaContent.PublishedDate != null) ? mediaContent.PublishedDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@isbn", DbType.String, mediaContent.ISBN ?? string.Empty),
				databaseLayer.GetParameter("@length", DbType.String, mediaContent.Length ?? string.Empty),
				databaseLayer.GetParameter("@website", DbType.String, mediaContent.WebSite ?? string.Empty),
				databaseLayer.GetParameter("@notes", DbType.String, mediaContent.Notes ?? string.Empty),
				databaseLayer.GetParameter("@mediacontentcategory", DbType.String, mediaContent.ContentCategory.ToString()),
				databaseLayer.GetParameter("@proofofpurchaserequired", DbType.Boolean, mediaContent.ProofOfPurchaseRequired),
				databaseLayer.GetParameter("@whoentered", DbType.Int32, (mediaContent.WhoEntered != null) ? mediaContent.WhoEntered.PersonId : this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@datecreated", DbType.DateTime, mediaContent.DateCreated),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, mediaContent.IsActive),
				databaseLayer.GetParameter("@courses", DbType.String, (mediaContent.CourseIdList != null && mediaContent.CourseIdList.Count > 0) ? mediaContent.CourseIdList.Distinct<int>().ToList<int>().CommaSeparatedValuesWithoutSpace<int>() : string.Empty),
				databaseLayer.GetParameter("@externalid", DbType.String, mediaContent.ExternalId ?? string.Empty),
				databaseLayer.GetParameter("@externalsourceprovider", DbType.String, mediaContent.ExternalSourceProvider ?? string.Empty),
				databaseLayer.GetParameter("@thumbnailimageurl", DbType.String, mediaContent.ThumbnailImageUrl ?? string.Empty)
			};
			databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_UpdateMediaContent", parameters);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00073940 File Offset: 0x00071B40
		public IList<MediaContent> GetAllMediaContent()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			List<MediaContent> list = new List<MediaContent>();
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_AlternateFormat_AllMediaContent", Array.Empty<DbParameter>()))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaContent mediaContentFromReader = this.GetMediaContentFromReader<MediaContent>(dataReader, batchDecryptor, false);
						bool flag2 = mediaContentFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000739EC File Offset: 0x00071BEC
		public MediaContentPerFormatInfo GetMediaContentPerFormatInfoById(int mediaContentPerFormat)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormat);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from [AlternativeFormat_MediaContent_x_MediaContentFormat] \r\n                where MediaContentPerFormatID = @mediacontentperformatid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetMediaContentPerFormatInfoFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00073A80 File Offset: 0x00071C80
		public bool DeleteMediaContent(Guid mediaContentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId);
			return databaseLayer.ExecuteNonQuery("if not exists (select 1 from AlternativeFormat_StudentMediaRequestDetail where FKMediaContentID=@mediacontentid and IsCancelled = 0 and IsCompleted = 0)\r\nAND not exists (select 1 from AlternativeFormat_MediaJob mj left join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf ON mcxf.MediaContentPerFormatID = mj.FKMediaContentPerFormatID\r\n\t\t\t\twhere mcxf.FKMediaContentID=@mediacontentid)\r\nbegin\r\n\tupdate [AlternativeFormat_MediaContent] set IsActive = 0 where [MediaContentID]=@mediacontentid\r\nend", new DbParameter[]
			{
				parameter
			}) > 0;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00073AD8 File Offset: 0x00071CD8
		public IList<MediaContentPerFormatInfo> LoadMediaContentPerFormatInfoByMediaContent(Guid mediaContentId)
		{
			List<MediaContentPerFormatInfo> list = new List<MediaContentPerFormatInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("Select * from [AlternativeFormat_MediaContent_x_MediaContentFormat] where [FKMediaContentID] = @mediacontentid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaContentPerFormatInfo mediaContentPerFormatInfoFromReader = this.GetMediaContentPerFormatInfoFromReader(dataReader);
						bool flag2 = mediaContentPerFormatInfoFromReader != null;
						if (flag2)
						{
							list.Add(mediaContentPerFormatInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00073B90 File Offset: 0x00071D90
		public int GetMediaContentPerFormatId(Guid mediaContentId, MediaContentFormat mediaContentFormat)
		{
			bool flag = mediaContentId == Guid.Empty;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] array = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@mediacontentperformatid", DbType.Int32, 0),
					databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId),
					databaseLayer.GetParameter("@mediacontentformat", DbType.String, mediaContentFormat.ToString())
				};
				databaseLayer.ExecuteNonQuery("if not exists (select 1 from AlternativeFormat_MediaContent_x_MediaContentFormat where MediaContentFormat = @mediacontentformat and FKMediaContentID = @mediacontentid)\r\n\t            begin\r\n\t\t            insert into AlternativeFormat_MediaContent_x_MediaContentFormat (MediaContentFormat, FKMediaContentID) values (@mediacontentformat, @mediacontentid)\r\n\t\t            set @mediacontentperformatid = SCOPE_IDENTITY()\r\n\t            end\r\n            else\r\n\t            begin\r\n\t\t            set @mediacontentperformatid = (select MediaContentPerFormatID from AlternativeFormat_MediaContent_x_MediaContentFormat where MediaContentFormat = @mediacontentformat and FKMediaContentID = @mediacontentid)\r\n\t            end", array);
				result = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
			}
			return result;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00073C48 File Offset: 0x00071E48
		[DebuggerStepThrough]
		public Task<int> GetMediaContentPerFormatIdAsync(Guid mediaContentId, MediaContentFormat mediaContentFormat)
		{
			MediaContentDAO.<GetMediaContentPerFormatIdAsync>d__24 <GetMediaContentPerFormatIdAsync>d__ = new MediaContentDAO.<GetMediaContentPerFormatIdAsync>d__24();
			<GetMediaContentPerFormatIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<GetMediaContentPerFormatIdAsync>d__.<>4__this = this;
			<GetMediaContentPerFormatIdAsync>d__.mediaContentId = mediaContentId;
			<GetMediaContentPerFormatIdAsync>d__.mediaContentFormat = mediaContentFormat;
			<GetMediaContentPerFormatIdAsync>d__.<>1__state = -1;
			<GetMediaContentPerFormatIdAsync>d__.<>t__builder.Start<MediaContentDAO.<GetMediaContentPerFormatIdAsync>d__24>(ref <GetMediaContentPerFormatIdAsync>d__);
			return <GetMediaContentPerFormatIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00073C9C File Offset: 0x00071E9C
		public Image GetMediaContentThumbnail(Guid mediaContentId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId)
			};
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select Thumbnail from [AlternativeFormat_MediaContentImage] where MediaContentId = @mediacontentid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return (dataReader["Thumbnail"] is DBNull) ? null : ((byte[])dataReader["Thumbnail"]).Deserialize();
				}
			}
			return null;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x00073D40 File Offset: 0x00071F40
		public byte[] GetMediaContentThumbnailBytes(Guid mediaContentId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId)
			};
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select Thumbnail from [AlternativeFormat_MediaContentImage] where MediaContentId = @mediacontentid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return (dataReader["Thumbnail"] is DBNull) ? null : ((byte[])dataReader["Thumbnail"]);
				}
			}
			return null;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00073DE0 File Offset: 0x00071FE0
		[DebuggerStepThrough]
		public Task<byte[]> GetMediaContentThumbnailBytesAsync(Guid mediaContentId)
		{
			MediaContentDAO.<GetMediaContentThumbnailBytesAsync>d__27 <GetMediaContentThumbnailBytesAsync>d__ = new MediaContentDAO.<GetMediaContentThumbnailBytesAsync>d__27();
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<GetMediaContentThumbnailBytesAsync>d__.<>4__this = this;
			<GetMediaContentThumbnailBytesAsync>d__.mediaContentId = mediaContentId;
			<GetMediaContentThumbnailBytesAsync>d__.<>1__state = -1;
			<GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Start<MediaContentDAO.<GetMediaContentThumbnailBytesAsync>d__27>(ref <GetMediaContentThumbnailBytesAsync>d__);
			return <GetMediaContentThumbnailBytesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00073E2C File Offset: 0x0007202C
		public void SetMediaContentThumbnail(Guid mediaContentId, Image thumbnail)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			bool flag = thumbnail == null;
			if (flag)
			{
				DbParameter parameter = clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId);
				clockWorkFiles.ExecuteNonQuery("delete from AlternativeFormat_MediaContentImage where MediaContentId=@mediacontentid", new DbParameter[]
				{
					parameter
				});
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId),
					clockWorkFiles.GetParameter("@thumbnail", DbType.Binary, thumbnail.Serialize())
				};
				clockWorkFiles.ExecuteNonQuery("IF EXISTS (SELECT 1 FROM [AlternativeFormat_MediaContentImage] where MediaContentId=@mediacontentid)\r\n                begin\r\n                    update [AlternativeFormat_MediaContentImage] set [Thumbnail]=@thumbnail where MediaContentId=@mediacontentid\r\n                end\r\n              ELSE\r\n                begin\r\n                    insert into [AlternativeFormat_MediaContentImage] (MediaContentId, [Thumbnail]) VALUES (@mediacontentid, @thumbnail)\r\n                end", parameters);
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00073EB8 File Offset: 0x000720B8
		[DebuggerStepThrough]
		public Task SetMediaContentThumbnailAsync(Guid mediaContentId, Image thumbnail)
		{
			MediaContentDAO.<SetMediaContentThumbnailAsync>d__29 <SetMediaContentThumbnailAsync>d__ = new MediaContentDAO.<SetMediaContentThumbnailAsync>d__29();
			<SetMediaContentThumbnailAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SetMediaContentThumbnailAsync>d__.<>4__this = this;
			<SetMediaContentThumbnailAsync>d__.mediaContentId = mediaContentId;
			<SetMediaContentThumbnailAsync>d__.thumbnail = thumbnail;
			<SetMediaContentThumbnailAsync>d__.<>1__state = -1;
			<SetMediaContentThumbnailAsync>d__.<>t__builder.Start<MediaContentDAO.<SetMediaContentThumbnailAsync>d__29>(ref <SetMediaContentThumbnailAsync>d__);
			return <SetMediaContentThumbnailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00073F0C File Offset: 0x0007210C
		public bool IsThumbnailAvailable(Guid mediaContentId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId)
			};
			object obj = clockWorkFiles.ExecuteScalar("select MediaContentId from [AlternativeFormat_MediaContentImage] where MediaContentId = @mediacontentid", parameters);
			return obj != null && !Convert.IsDBNull(obj) && !string.IsNullOrEmpty(obj.ToString());
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00073F6C File Offset: 0x0007216C
		public Image GetMediaContentCoverImage(Guid mediaContentId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId)
			};
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select BookCover from [AlternativeFormat_MediaContentImage] where MediaContentId = @mediacontentid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return (dataReader["BookCover"] is DBNull) ? null : ((byte[])dataReader["BookCover"]).Deserialize();
				}
			}
			return null;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00074010 File Offset: 0x00072210
		public byte[] GetMediaContentCoverImageBytes(Guid mediaContentId)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId)
			};
			using (IDataReader dataReader = clockWorkFiles.ExecuteQueryReader("select BookCover from [AlternativeFormat_MediaContentImage] where MediaContentId = @mediacontentid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return (dataReader["CoverImage"] is DBNull) ? null : ((byte[])dataReader["CoverImage"]);
				}
			}
			return null;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000740B0 File Offset: 0x000722B0
		public void SetMediaContentCoverImage(Guid mediaContentId, Image cover, Image thumbnail)
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			bool flag = cover == null;
			if (flag)
			{
				DbParameter parameter = clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId);
				clockWorkFiles.ExecuteNonQuery("update AlternativeFormat_MediaContentImage set BookCover=NULL where MediaContentId=@mediacontentid", new DbParameter[]
				{
					parameter
				});
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					clockWorkFiles.GetParameter("@mediacontentid", DbType.Guid, mediaContentId),
					clockWorkFiles.GetParameter("@cover", DbType.Binary, cover.Serialize()),
					clockWorkFiles.GetParameter("@thumbnail", DbType.Binary, thumbnail.Serialize())
				};
				clockWorkFiles.ExecuteNonQuery("IF EXISTS (SELECT 1 FROM [AlternativeFormat_MediaContentImage] where MediaContentId=@mediacontentid)\r\n                begin\r\n                    update [AlternativeFormat_MediaContentImage] set [BookCover]=@cover, [Thumbnail]=@thumbnail where MediaContentId=@mediacontentid\r\n                end\r\n              ELSE\r\n                begin\r\n                    insert into [AlternativeFormat_MediaContentImage] (MediaContentId, [Thumbnail], [BookCover]) VALUES (@mediacontentid, @thumbnail, @cover)\r\n                end", parameters);
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00074150 File Offset: 0x00072350
		public IList<LookupCourseBase> GetMediaContentCourses(Guid mediaContentId)
		{
			List<LookupCourseBase> list = new List<LookupCourseBase>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select FK_CourseId from [AlternativeFormat_MediaContent_x_Course] where FK_MediaContentID=@mediacontentid", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					ILookupCourseDAO lookupCourseDAO = new LookupCourseDAO(this.OpContext);
					while (dataReader.Read())
					{
						int luCourseId = (int)dataReader["FK_CourseId"];
						LookupCourse lookupCourse = lookupCourseDAO.LoadCourse(luCourseId);
						bool flag2 = lookupCourse != null;
						if (flag2)
						{
							list.Add(lookupCourse);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00074228 File Offset: 0x00072428
		internal T GetMediaContentFromReader<T>(IDataReader reader, IBatchDecryptor decryptor = null, bool loadIsThumbnailAvailable = true) where T : MediaContent
		{
			bool flag = reader.ContainsColumn("MediaContentID");
			T result;
			if (flag)
			{
				Guid guid = (Guid)reader["MediaContentID"];
				MediaContentIdentifier identifier = new MediaContentIdentifier
				{
					MediaContentUniqueId = new Guid?(guid),
					ISBN = Convert.ToString(reader["ISBN"]),
					MediaContentId = Convert.ToInt32(reader["MediaContentDataID"]),
					ExternalId = (reader.ContainsColumn("ExternalId") ? ((string)reader["ExternalId"]) : string.Empty),
					ExternalSourceProvider = (reader.ContainsColumn("ExternalSourceProvider") ? ((string)reader["ExternalSourceProvider"]) : string.Empty)
				};
				T t = Activator.CreateInstance<T>();
				t.Identifier = identifier;
				BasicMediaContent basicMediaContent = t;
				IList<string> authors;
				if (!string.IsNullOrEmpty((string)reader["Authors"]))
				{
					authors = (from s in Convert.ToString(reader["Authors"]).Split(new char[]
					{
						'|'
					})
					where !string.IsNullOrWhiteSpace(s)
					select s).ToList<string>();
				}
				else
				{
					authors = null;
				}
				basicMediaContent.Authors = authors;
				t.ContentCategory = (eMediaContentCategory)Enum.Parse(typeof(eMediaContentCategory), Convert.ToString(reader["MediaContentCategory"]));
				bool flag2 = reader.ContainsColumn("courseids");
				if (flag2)
				{
					t.CourseIdList = ((reader["courseids"] is DBNull) ? null : ((string)reader["courseids"]).SplitIntValues());
				}
				t.Edition = Convert.ToString(reader["Edition"]);
				t.Length = Convert.ToString(reader["Length"]);
				t.LongTitle = Convert.ToString(reader["LongTitle"]);
				t.Notes = Convert.ToString(reader["Notes"]);
				t.PublishedDate = ((reader["PublishedDate"] is DBNull) ? null : new DateTime?(Convert.ToDateTime(reader["PublishedDate"])));
				t.Publisher = ((reader["PublisherID"] is DBNull) ? null : MediaPublisherDAO.GetPublisherFromReader(reader));
				t.ShortTitle = Convert.ToString(reader["ShortTitle"]);
				t.Summary = Convert.ToString(reader["Summary"]);
				t.WebSite = Convert.ToString(reader["Website"]);
				t.ProofOfPurchaseRequired = Convert.ToBoolean(reader["ProofOfPurchaseRequired"]);
				t.WhoEntered = PeopleDAO.GetPersonFromReader("", reader, this.OpContext, decryptor);
				t.DateCreated = Convert.ToDateTime(reader["DateCreated"]);
				t.IsActive = Convert.ToBoolean(reader["IsActive"]);
				t.IsThumbnailAvailable = (loadIsThumbnailAvailable && this.IsThumbnailAvailable(guid));
				t.ThumbnailImageUrl = (reader.ContainsColumn("ThumbnailImageUrl") ? Convert.ToString(reader["ThumbnailImageUrl"]) : string.Empty);
				bool flag3 = reader.ContainsColumn("formats");
				if (flag3)
				{
					t.AvailableFormats = ((reader["formats"] is DBNull) ? string.Empty : ((string)reader["formats"]));
				}
				bool flag4 = reader.ContainsColumn("coursedescriptions");
				if (flag4)
				{
					t.CourseDescriptions = ((reader["coursedescriptions"] is DBNull) ? string.Empty : ((string)reader["coursedescriptions"]));
				}
				result = t;
			}
			else
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00074670 File Offset: 0x00072870
		private MediaContentPerFormatInfo GetMediaContentPerFormatInfoFromReader(IDataReader reader)
		{
			return new MediaContentPerFormatInfo
			{
				MediaContentPerFormatId = Convert.ToInt32(reader["MediaContentPerFormatID"]),
				MediaContentFormat = (MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), Convert.ToString(reader["MediaContentFormat"])),
				MediaContentId = (Guid)reader["FKMediaContentID"]
			};
		}
	}
}
