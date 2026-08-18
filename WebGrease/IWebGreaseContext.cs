using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using WebGrease.Activities;
using WebGrease.Configuration;
using WebGrease.Preprocessing;

namespace WebGrease
{
	// Token: 0x02000104 RID: 260
	public interface IWebGreaseContext
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001073 RID: 4211
		ICacheManager Cache { get; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001074 RID: 4212
		WebGreaseConfiguration Configuration { get; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001075 RID: 4213
		LogManager Log { get; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001076 RID: 4214
		ITimeMeasure Measure { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001077 RID: 4215
		PreprocessingManager Preprocessing { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001078 RID: 4216
		DateTimeOffset SessionStartTime { get; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001079 RID: 4217
		IEnumerable<KeyValuePair<string, IEnumerable<TimeMeasureResult>>> ThreadedMeasureResults { get; }

		// Token: 0x0600107A RID: 4218
		void CleanCache(LogManager logManager = null);

		// Token: 0x0600107B RID: 4219
		void CleanDestination();

		// Token: 0x0600107C RID: 4220
		IDictionary<string, string> GetAvailableFiles(string rootDirectory, IEnumerable<string> directories, IEnumerable<string> extensions, FileTypes fileType);

		// Token: 0x0600107D RID: 4221
		string GetValueHash(string value);

		// Token: 0x0600107E RID: 4222
		string GetFileHash(string filePath);

		// Token: 0x0600107F RID: 4223
		string MakeRelativeToApplicationRoot(string absolutePath);

		// Token: 0x06001080 RID: 4224
		string GetWorkingSourceDirectory(string relativePath);

		// Token: 0x06001081 RID: 4225
		void Touch(string filePath);

		// Token: 0x06001082 RID: 4226
		IWebGreaseSection SectionedAction(params string[] idParts);

		// Token: 0x06001083 RID: 4227
		IWebGreaseSection SectionedActionGroup(params string[] idParts);

		// Token: 0x06001084 RID: 4228
		bool TemporaryIgnore(IFileSet fileSet, ContentItem contentItem);

		// Token: 0x06001085 RID: 4229
		bool TemporaryIgnore(IEnumerable<ResourcePivotKey> resourcePivotKey);

		// Token: 0x06001086 RID: 4230
		string EnsureErrorFileOnDisk(string sourceFile, ContentItem sourceContentItem);

		// Token: 0x06001087 RID: 4231
		void ParallelForEach<T>(Func<T, string[]> idParts, IEnumerable<T> items, Func<IWebGreaseContext, T, ParallelLoopState, bool> parallelAction, Func<IWebGreaseContext, T, bool> serialAction = null);

		// Token: 0x06001088 RID: 4232
		string GetBitmapHash(Bitmap bitmap, ImageFormat format);
	}
}
