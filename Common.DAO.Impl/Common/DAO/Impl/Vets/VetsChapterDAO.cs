using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.DAO.Impl.Vets
{
	// Token: 0x02000021 RID: 33
	public class VetsChapterDAO : IVetsChapterDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000649A File Offset: 0x0000469A
		public VetsChapterDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000064AC File Offset: 0x000046AC
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000064B4 File Offset: 0x000046B4
		public OperationContext OpContext { get; set; }

		// Token: 0x060000D4 RID: 212 RVA: 0x000064C0 File Offset: 0x000046C0
		public static VetsChapter GetChapterFromRecord(IDataRecord record)
		{
			bool flag = record == null || record["ChapterId"] is DBNull;
			VetsChapter result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new VetsChapter
				{
					ChapterId = (Guid)record["ChapterId"],
					ChapterTitle = record["ChapterTitle"].ToString().Trim(),
					ChapterDescription = record["ChapterDescription"].ToString().Trim(),
					IsDisabled = (!(record["IsDisabled"] is DBNull) && (bool)record["IsDisabled"]),
					AssociatedFormId = ((record["ChapterFormId"] is DBNull) ? Guid.Empty : ((Guid)record["ChapterFormId"])),
					OrderNum = ((record["OrderNum"] is DBNull) ? 0 : ((int)record["OrderNum"]))
				};
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000065D4 File Offset: 0x000047D4
		[DebuggerStepThrough]
		public Task<bool> DeleteChapterAsync(Guid chapterId)
		{
			VetsChapterDAO.<DeleteChapterAsync>d__6 <DeleteChapterAsync>d__ = new VetsChapterDAO.<DeleteChapterAsync>d__6();
			<DeleteChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteChapterAsync>d__.<>4__this = this;
			<DeleteChapterAsync>d__.chapterId = chapterId;
			<DeleteChapterAsync>d__.<>1__state = -1;
			<DeleteChapterAsync>d__.<>t__builder.Start<VetsChapterDAO.<DeleteChapterAsync>d__6>(ref <DeleteChapterAsync>d__);
			return <DeleteChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006620 File Offset: 0x00004820
		[DebuggerStepThrough]
		public Task ChangeChapterDisabled(Guid chapterId, bool newIsDisabled)
		{
			VetsChapterDAO.<ChangeChapterDisabled>d__7 <ChangeChapterDisabled>d__ = new VetsChapterDAO.<ChangeChapterDisabled>d__7();
			<ChangeChapterDisabled>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ChangeChapterDisabled>d__.<>4__this = this;
			<ChangeChapterDisabled>d__.chapterId = chapterId;
			<ChangeChapterDisabled>d__.newIsDisabled = newIsDisabled;
			<ChangeChapterDisabled>d__.<>1__state = -1;
			<ChangeChapterDisabled>d__.<>t__builder.Start<VetsChapterDAO.<ChangeChapterDisabled>d__7>(ref <ChangeChapterDisabled>d__);
			return <ChangeChapterDisabled>d__.<>t__builder.Task;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006674 File Offset: 0x00004874
		[DebuggerStepThrough]
		public Task UpdateChapterAsync(VetsChapter chapter)
		{
			VetsChapterDAO.<UpdateChapterAsync>d__8 <UpdateChapterAsync>d__ = new VetsChapterDAO.<UpdateChapterAsync>d__8();
			<UpdateChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateChapterAsync>d__.<>4__this = this;
			<UpdateChapterAsync>d__.chapter = chapter;
			<UpdateChapterAsync>d__.<>1__state = -1;
			<UpdateChapterAsync>d__.<>t__builder.Start<VetsChapterDAO.<UpdateChapterAsync>d__8>(ref <UpdateChapterAsync>d__);
			return <UpdateChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000066C0 File Offset: 0x000048C0
		[DebuggerStepThrough]
		public Task<Guid> CreateChapterAsync(VetsChapter chapter)
		{
			VetsChapterDAO.<CreateChapterAsync>d__9 <CreateChapterAsync>d__ = new VetsChapterDAO.<CreateChapterAsync>d__9();
			<CreateChapterAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid>.Create();
			<CreateChapterAsync>d__.<>4__this = this;
			<CreateChapterAsync>d__.chapter = chapter;
			<CreateChapterAsync>d__.<>1__state = -1;
			<CreateChapterAsync>d__.<>t__builder.Start<VetsChapterDAO.<CreateChapterAsync>d__9>(ref <CreateChapterAsync>d__);
			return <CreateChapterAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000670C File Offset: 0x0000490C
		[DebuggerStepThrough]
		public Task<IList<VetsChapter>> GetChaptersAsync()
		{
			VetsChapterDAO.<GetChaptersAsync>d__10 <GetChaptersAsync>d__ = new VetsChapterDAO.<GetChaptersAsync>d__10();
			<GetChaptersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<VetsChapter>>.Create();
			<GetChaptersAsync>d__.<>4__this = this;
			<GetChaptersAsync>d__.<>1__state = -1;
			<GetChaptersAsync>d__.<>t__builder.Start<VetsChapterDAO.<GetChaptersAsync>d__10>(ref <GetChaptersAsync>d__);
			return <GetChaptersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006750 File Offset: 0x00004950
		public IList<VetsChapter> GetChapters()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			IList<VetsChapter> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT ChapterId,ChapterTitle,ChapterDescription,ChapterFormId,IsDisabled,OrderNum FROM VetsChapter WHERE IsDisabled=0 ORDER BY OrderNum"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<VetsChapter> list = new List<VetsChapter>();
					while (dataReader.Read())
					{
						VetsChapter chapterFromRecord = VetsChapterDAO.GetChapterFromRecord(dataReader);
						bool flag2 = chapterFromRecord != null;
						if (flag2)
						{
							list.Add(chapterFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
