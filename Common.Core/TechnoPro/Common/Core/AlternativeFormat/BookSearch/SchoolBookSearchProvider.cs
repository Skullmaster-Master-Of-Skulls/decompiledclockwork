using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.ICore.AlternativeFormat.BookSearch;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.AlternativeFormat.BookSearch
{
	// Token: 0x02000161 RID: 353
	public class SchoolBookSearchProvider : IBookSearchProvider, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00075061 File Offset: 0x00073261
		// (set) Token: 0x06000FFF RID: 4095 RVA: 0x00075069 File Offset: 0x00073269
		private int BooksProviderReportId { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001000 RID: 4096 RVA: 0x0007157E File Offset: 0x0006F77E
		public eBookSearchProviderType SearchProviderType
		{
			get
			{
				return eBookSearchProviderType.LocalOnly;
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00075074 File Offset: 0x00073274
		public SchoolBookSearchProvider(OperationContext opContext)
		{
			this.OpContext = opContext;
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			this.BooksProviderReportId = oldUserSettingManager.GetSettingValue_Int(this.OpContext.WhoAmI, eSettingCode.SETTING_AlternateFormat_SchoolBookSearchProviderReportId);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000750BC File Offset: 0x000732BC
		public IList<EBookSearchResult> SearchForVolumes(EBookSearchRequest request)
		{
			bool flag = this.BooksProviderReportId == 0;
			IList<EBookSearchResult> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IReportManager reportManager = new ReportManager(this.OpContext);
				ReportParameter reportParameter = new ReportParameter
				{
					Name = "searchtext",
					Value = request.SearchText
				};
				RunReportResult runReportResult = reportManager.ExecuteReport2(this.BooksProviderReportId, new ReportParameter[]
				{
					reportParameter
				});
				bool flag2 = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully;
				if (flag2)
				{
					CWLogger.Logger.Error("SchoolBookSearchProvider::SearchForVolumes: Executing reportId={0} failed", this.BooksProviderReportId);
					result = null;
				}
				else
				{
					DataTable dt = (runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table;
					result = this.GetBookSearchResultListFromDataTable(dt);
				}
			}
			return result;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00075188 File Offset: 0x00073388
		public EBookSearchResult GetVolumeByISBN(string isbn)
		{
			IReportManager reportManager = new ReportManager(this.OpContext);
			ReportParameter reportParameter = new ReportParameter
			{
				Name = "isbn",
				Value = isbn
			};
			RunReportResult runReportResult = reportManager.ExecuteReport2(this.BooksProviderReportId, new ReportParameter[]
			{
				reportParameter
			});
			bool flag = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully;
			EBookSearchResult result;
			if (flag)
			{
				CWLogger.Logger.Error("SchoolBookSearchProvider::GetVolumenByISBN: Executing reportId={0} failed", this.BooksProviderReportId);
				result = null;
			}
			else
			{
				DataTable dataTable = (runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table;
				result = ((dataTable != null && dataTable.Rows.Count > 0) ? this.GetBookSearchResultFromDataTable(dataTable.Rows[0]) : null);
			}
			return result;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00075258 File Offset: 0x00073458
		public EBookSearchResult GetVolumeById(string id)
		{
			IReportManager reportManager = new ReportManager(this.OpContext);
			ReportParameter reportParameter = new ReportParameter
			{
				Name = "externalid",
				Value = id
			};
			RunReportResult runReportResult = reportManager.ExecuteReport2(this.BooksProviderReportId, new ReportParameter[]
			{
				reportParameter
			});
			bool flag = runReportResult == null || runReportResult.ReportStatus == null || runReportResult.ReportStatus.LastStatusStep != eRunStatusStep.CompletedSuccessfully;
			EBookSearchResult result;
			if (flag)
			{
				CWLogger.Logger.Error("SchoolBookSearchProvider::GetVolumenById: Executing reportId={0} failed", this.BooksProviderReportId);
				result = null;
			}
			else
			{
				DataTable dataTable = (runReportResult.PrimaryData == null) ? null : runReportResult.PrimaryData.Table;
				result = ((dataTable != null && dataTable.Rows.Count > 0) ? this.GetBookSearchResultFromDataTable(dataTable.Rows[0]) : null);
			}
			return result;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00075328 File Offset: 0x00073528
		[DebuggerStepThrough]
		public Task<IList<EBookSearchResult>> SearchForVolumesAsync(EBookSearchRequest request)
		{
			SchoolBookSearchProvider.<SearchForVolumesAsync>d__10 <SearchForVolumesAsync>d__ = new SchoolBookSearchProvider.<SearchForVolumesAsync>d__10();
			<SearchForVolumesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<EBookSearchResult>>.Create();
			<SearchForVolumesAsync>d__.<>4__this = this;
			<SearchForVolumesAsync>d__.request = request;
			<SearchForVolumesAsync>d__.<>1__state = -1;
			<SearchForVolumesAsync>d__.<>t__builder.Start<SchoolBookSearchProvider.<SearchForVolumesAsync>d__10>(ref <SearchForVolumesAsync>d__);
			return <SearchForVolumesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00075374 File Offset: 0x00073574
		[DebuggerStepThrough]
		public Task<EBookSearchResult> GetVolumeByISBNAsync(string isbn)
		{
			SchoolBookSearchProvider.<GetVolumeByISBNAsync>d__11 <GetVolumeByISBNAsync>d__ = new SchoolBookSearchProvider.<GetVolumeByISBNAsync>d__11();
			<GetVolumeByISBNAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResult>.Create();
			<GetVolumeByISBNAsync>d__.<>4__this = this;
			<GetVolumeByISBNAsync>d__.isbn = isbn;
			<GetVolumeByISBNAsync>d__.<>1__state = -1;
			<GetVolumeByISBNAsync>d__.<>t__builder.Start<SchoolBookSearchProvider.<GetVolumeByISBNAsync>d__11>(ref <GetVolumeByISBNAsync>d__);
			return <GetVolumeByISBNAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000753C0 File Offset: 0x000735C0
		[DebuggerStepThrough]
		public Task<EBookSearchResult> GetVolumeByIdAsync(string id)
		{
			SchoolBookSearchProvider.<GetVolumeByIdAsync>d__12 <GetVolumeByIdAsync>d__ = new SchoolBookSearchProvider.<GetVolumeByIdAsync>d__12();
			<GetVolumeByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<EBookSearchResult>.Create();
			<GetVolumeByIdAsync>d__.<>4__this = this;
			<GetVolumeByIdAsync>d__.id = id;
			<GetVolumeByIdAsync>d__.<>1__state = -1;
			<GetVolumeByIdAsync>d__.<>t__builder.Start<SchoolBookSearchProvider.<GetVolumeByIdAsync>d__12>(ref <GetVolumeByIdAsync>d__);
			return <GetVolumeByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0007540C File Offset: 0x0007360C
		private EBookSearchResult GetBookSearchResultFromDataTable(DataRow row)
		{
			EBookSearchResult result;
			try
			{
				result = new EBookSearchResult
				{
					Id = (row.ContainsColumn("externalid") ? row.Field("externalid") : null),
					ISBN = (row.ContainsColumn("isbn") ? row.Field("isbn") : null),
					Authors = (row.ContainsColumn("authors") ? row.Field("authors").SplitValues() : null),
					Publisher = (row.ContainsColumn("publisher") ? row.Field("publisher") : null),
					PublisherDate = (row.ContainsColumn("publisheddate") ? row.Field("publisheddate") : null),
					Summary = (row.ContainsColumn("summary") ? row.Field("summary") : null),
					Title = (row.ContainsColumn("title") ? row.Field("title") : null),
					Url = (row.ContainsColumn("url") ? row.Field("url") : null),
					ThumbnailUrl = (row.ContainsColumn("thumbnailurl") ? row.Field("thumbnailurl") : null),
					SearchEngine = eBookSearchProviderName.SchoolBookProvider
				};
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("SchoolBookSearchProvider::GetBookSearchResultFromDataTable: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x000755AC File Offset: 0x000737AC
		private IList<EBookSearchResult> GetBookSearchResultListFromDataTable(DataTable dt)
		{
			List<EBookSearchResult> list = new List<EBookSearchResult>();
			bool flag = dt != null && dt.Rows != null;
			if (flag)
			{
				list.AddRange(from book in dt.Rows.Cast<DataRow>().Select(new Func<DataRow, EBookSearchResult>(this.GetBookSearchResultFromDataTable))
				where book != null
				select book);
			}
			return list;
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0007561F File Offset: 0x0007381F
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x00075627 File Offset: 0x00073827
		public OperationContext OpContext { get; set; }
	}
}
