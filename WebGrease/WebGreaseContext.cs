using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebGrease.Activities;
using WebGrease.Configuration;
using WebGrease.Extensions;
using WebGrease.Preprocessing;

namespace WebGrease
{
	// Token: 0x020001BC RID: 444
	public class WebGreaseContext : IWebGreaseContext
	{
		// Token: 0x060016C4 RID: 5828 RVA: 0x00082A04 File Offset: 0x00080C04
		public WebGreaseContext(IWebGreaseContext parentContext, FileInfo configFile)
		{
			WebGreaseConfiguration webGreaseConfiguration = new WebGreaseConfiguration(parentContext.Configuration, configFile);
			webGreaseConfiguration.Validate();
			if (webGreaseConfiguration.Global.TreatWarningsAsErrors != null && parentContext.Log != null)
			{
				parentContext.Log.TreatWarningsAsErrors = (webGreaseConfiguration.Global.TreatWarningsAsErrors == true);
			}
			WebGreaseContext webGreaseContext = parentContext as WebGreaseContext;
			if (webGreaseContext != null)
			{
				this.threadedMeasureResults = webGreaseContext.threadedMeasureResults;
			}
			this.Initialize(webGreaseConfiguration, parentContext.Log, parentContext.Cache, parentContext.Preprocessing, parentContext.SessionStartTime, parentContext.Measure);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00082AD4 File Offset: 0x00080CD4
		public WebGreaseContext(WebGreaseConfiguration configuration, LogManager logManager, ICacheSection parentCacheSection = null, PreprocessingManager preprocessingManager = null)
		{
			DateTimeOffset now = DateTimeOffset.Now;
			configuration.Validate();
			ITimeMeasure timeMeasure2;
			if (!configuration.Measure)
			{
				ITimeMeasure timeMeasure = new NullTimeMeasure();
				timeMeasure2 = timeMeasure;
			}
			else
			{
				timeMeasure2 = new TimeMeasure();
			}
			ITimeMeasure timeMeasure3 = timeMeasure2;
			ICacheManager cacheManager2;
			if (!configuration.CacheEnabled)
			{
				ICacheManager cacheManager = new NullCacheManager();
				cacheManager2 = cacheManager;
			}
			else
			{
				cacheManager2 = new CacheManager(configuration, logManager, parentCacheSection);
			}
			ICacheManager cacheManager3 = cacheManager2;
			this.Initialize(configuration, logManager, cacheManager3, (preprocessingManager != null) ? new PreprocessingManager(preprocessingManager) : new PreprocessingManager(configuration, logManager, timeMeasure3), now, timeMeasure3);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00082B6C File Offset: 0x00080D6C
		public WebGreaseContext(WebGreaseConfiguration configuration, Action<string, MessageImportance> logInformation = null, Action<string> logWarning = null, LogExtendedError logExtendedWarning = null, Action<string> logErrorMessage = null, LogError logError = null, LogExtendedError logExtendedError = null) : this(configuration, new LogManager(logInformation, logWarning, logExtendedWarning, logErrorMessage, logError, logExtendedError, configuration.Global.TreatWarningsAsErrors), null, null)
		{
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00082B9C File Offset: 0x00080D9C
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x00082BA4 File Offset: 0x00080DA4
		public ICacheManager Cache { get; private set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x00082BAD File Offset: 0x00080DAD
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x00082BB5 File Offset: 0x00080DB5
		public WebGreaseConfiguration Configuration { get; private set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x00082BBE File Offset: 0x00080DBE
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x00082BC6 File Offset: 0x00080DC6
		public LogManager Log { get; private set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00082BCF File Offset: 0x00080DCF
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x00082BD7 File Offset: 0x00080DD7
		public ITimeMeasure Measure { get; private set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00082BE0 File Offset: 0x00080DE0
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x00082BE8 File Offset: 0x00080DE8
		public PreprocessingManager Preprocessing { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00082BF1 File Offset: 0x00080DF1
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x00082BF9 File Offset: 0x00080DF9
		public DateTimeOffset SessionStartTime { get; private set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00082C02 File Offset: 0x00080E02
		public IEnumerable<KeyValuePair<string, IEnumerable<TimeMeasureResult>>> ThreadedMeasureResults
		{
			get
			{
				return this.threadedMeasureResults;
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00082C0A File Offset: 0x00080E0A
		public IWebGreaseSection SectionedAction(params string[] idParts)
		{
			return WebGreaseSection.Create(this, idParts, false);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00082C14 File Offset: 0x00080E14
		public IWebGreaseSection SectionedActionGroup(params string[] idParts)
		{
			return WebGreaseSection.Create(this, idParts, true);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00082C1E File Offset: 0x00080E1E
		public bool TemporaryIgnore(IFileSet fileSet, ContentItem contentItem)
		{
			return this.Configuration != null && this.Configuration.Overrides != null && (this.Configuration.Overrides.ShouldIgnore(fileSet) || this.Configuration.Overrides.ShouldIgnore(contentItem));
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00082C5D File Offset: 0x00080E5D
		public bool TemporaryIgnore(IEnumerable<ResourcePivotKey> resourcePivotKey)
		{
			return this.Configuration != null && this.Configuration.Overrides != null && this.Configuration.Overrides.ShouldIgnore(resourcePivotKey);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00082C88 File Offset: 0x00080E88
		public void CleanCache(LogManager logManager = null)
		{
			string rootPath = this.Cache.RootPath;
			(logManager ?? this.Log).Information("Cleaning Cache: {0}".InvariantFormat(new object[]
			{
				rootPath
			}), MessageImportance.High);
			this.CleanDirectory(rootPath, new string[]
			{
				"webgrease.caching.lock"
			});
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00082CE0 File Offset: 0x00080EE0
		public void CleanDestination()
		{
			string destinationDirectory = this.Configuration.DestinationDirectory;
			this.Log.Information("Cleaning Destination: {0}".InvariantFormat(new object[]
			{
				destinationDirectory
			}), MessageImportance.High);
			this.CleanDirectory(destinationDirectory, null);
			string logsDirectory = this.Configuration.LogsDirectory;
			this.Log.Information("Cleaning Destination: {0}".InvariantFormat(new object[]
			{
				logsDirectory
			}), MessageImportance.High);
			this.CleanDirectory(logsDirectory, null);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00082F54 File Offset: 0x00081154
		public IDictionary<string, string> GetAvailableFiles(string rootDirectory, IEnumerable<string> directories, IEnumerable<string> extensions, FileTypes fileType)
		{
			string key = new
			{
				rootDirectory,
				directories,
				extensions,
				fileType
			}.ToJson(false);
			IDictionary<string, string> result;
			if (!this.availableFileCollections.TryGetValue(key, out result))
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (directories == null)
				{
					return dictionary;
				}
				foreach (string path in directories)
				{
					foreach (string searchPattern in extensions)
					{
						dictionary.AddRange((from f in Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories)
						select f.ToLowerInvariant()).ToDictionary((string f) => f.MakeRelativeToDirectory(rootDirectory), (string f) => f));
					}
				}
				this.availableFileCollections.Add(key, result = dictionary);
			}
			return result;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000830B8 File Offset: 0x000812B8
		public string GetValueHash(string value)
		{
			return this.SectionedAction(new string[]
			{
				"ContentHash"
			}).Execute<string>(() => WebGreaseContext.ComputeContentHash(value ?? string.Empty, null));
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00083114 File Offset: 0x00081314
		public string GetBitmapHash(Bitmap bitmap, ImageFormat format)
		{
			string result;
			lock (bitmap)
			{
				result = this.SectionedAction(new string[]
				{
					"BitmapHash"
				}).Execute<string>(() => WebGreaseContext.ComputeBitmapHash(bitmap, format));
			}
			return result;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000831B4 File Offset: 0x000813B4
		public string GetContentItemHash(ContentItem contentItem)
		{
			return this.SectionedAction(new string[]
			{
				"ContentHash"
			}).Execute<string>(() => contentItem.GetContentHash(this));
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000831FC File Offset: 0x000813FC
		public string GetFileHash(string filePath)
		{
			string text = null;
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists)
			{
				throw new FileNotFoundException("Could not find the file to create a hash for", filePath);
			}
			string fullName = fileInfo.FullName;
			if (this.sessionCachedFileHashes.TryGetValue(fullName, out text))
			{
				return text;
			}
			Tuple<DateTime, long, string> tuple;
			WebGreaseContext.CachedFileHashes.TryGetValue(fullName, out tuple);
			if (tuple != null && tuple.Item1 == fileInfo.LastWriteTimeUtc && tuple.Item2 == fileInfo.Length)
			{
				return tuple.Item3;
			}
			text = WebGreaseContext.ComputeFileHash(fileInfo.FullName);
			WebGreaseContext.CachedFileHashes[fullName] = new Tuple<DateTime, long, string>(fileInfo.LastWriteTimeUtc, fileInfo.Length, text);
			this.sessionCachedFileHashes[fullName] = text;
			return text;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000832AF File Offset: 0x000814AF
		public string MakeRelativeToApplicationRoot(string absolutePath)
		{
			return absolutePath.MakeRelativeTo(this.Configuration.ApplicationRootDirectory, new char[0]);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000832C8 File Offset: 0x000814C8
		public string GetWorkingSourceDirectory(string relativePath)
		{
			string text = this.Configuration.SourceDirectory ?? string.Empty;
			string fileName = Path.Combine(text, relativePath);
			FileInfo fileInfo = new FileInfo(fileName);
			if (!text.IsNullOrWhitespace() && !fileInfo.FullName.StartsWith(text, StringComparison.OrdinalIgnoreCase))
			{
				return text;
			}
			return fileInfo.DirectoryName;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00083318 File Offset: 0x00081518
		public void Touch(string filePath)
		{
			DateTime utcDateTime = this.SessionStartTime.UtcDateTime;
			try
			{
				File.SetLastWriteTimeUtc(filePath, utcDateTime);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00083350 File Offset: 0x00081550
		public string EnsureErrorFileOnDisk(string sourceFile, ContentItem sourceContentItem)
		{
			if (sourceContentItem == null)
			{
				return sourceFile;
			}
			if (sourceFile.IsNullOrWhitespace() || !File.Exists(sourceFile))
			{
				sourceFile = sourceContentItem.RelativeContentPath;
				if (sourceFile.IsNullOrWhitespace())
				{
					sourceFile = Guid.NewGuid().ToString().Replace("-", string.Empty);
				}
				if (sourceContentItem.ResourcePivotKeys != null)
				{
					ResourcePivotKey resourcePivotKey = sourceContentItem.ResourcePivotKeys.FirstOrDefault<ResourcePivotKey>();
					if (resourcePivotKey != null)
					{
						string extension = Path.GetExtension(sourceFile);
						sourceFile = Path.ChangeExtension(sourceFile, "." + resourcePivotKey.ToString("{0}.{1}") + extension);
					}
				}
			}
			sourceFile = sourceFile.NormalizeUrl();
			if (!Path.IsPathRooted(sourceFile))
			{
				sourceFile = Path.Combine(this.Configuration.IntermediateErrorDirectory, sourceFile);
			}
			sourceContentItem.WriteTo(sourceFile, false);
			return sourceFile;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x000836D0 File Offset: 0x000818D0
		public void ParallelForEach<T>(Func<T, string[]> idParts, IEnumerable<T> items, Func<IWebGreaseContext, T, ParallelLoopState, bool> parallelAction, Func<IWebGreaseContext, T, bool> serialAction = null)
		{
			WebGreaseContext.<>c__DisplayClass17<T> CS$<>8__locals1 = new WebGreaseContext.<>c__DisplayClass17<T>();
			CS$<>8__locals1.idParts = idParts;
			CS$<>8__locals1.items = items;
			CS$<>8__locals1.parallelAction = parallelAction;
			CS$<>8__locals1.serialAction = serialAction;
			CS$<>8__locals1.<>4__this = this;
			string[] idParts2 = CS$<>8__locals1.idParts(default(T));
			this.SectionedAction(idParts2).Execute(delegate()
			{
				object serialLock = new object();
				List<Tuple<IWebGreaseContext, DelayedLogManager, T>> parallelForEachItems = new List<Tuple<IWebGreaseContext, DelayedLogManager, T>>();
				int done = 0;
				foreach (T t in CS$<>8__locals1.items)
				{
					DelayedLogManager delayedLogManager = new DelayedLogManager(CS$<>8__locals1.<>4__this.Log, t.ToString());
					WebGreaseContext webGreaseContext = new WebGreaseContext(new WebGreaseConfiguration(CS$<>8__locals1.<>4__this.Configuration), delayedLogManager.LogManager, CS$<>8__locals1.<>4__this.Cache.CurrentCacheSection, CS$<>8__locals1.<>4__this.Preprocessing);
					bool flag = true;
					if (CS$<>8__locals1.serialAction != null)
					{
						flag = CS$<>8__locals1.serialAction(webGreaseContext, t);
					}
					if (flag)
					{
						parallelForEachItems.Add(new Tuple<IWebGreaseContext, DelayedLogManager, T>(webGreaseContext, delayedLogManager, t));
					}
				}
				Parallel.ForEach<Tuple<IWebGreaseContext, DelayedLogManager, T>>(parallelForEachItems, delegate(Tuple<IWebGreaseContext, DelayedLogManager, T> item, ParallelLoopState state)
				{
					WebGreaseContext.<>c__DisplayClass17<T> CS$<>8__locals18 = CS$<>8__locals1;
					Tuple<IWebGreaseContext, DelayedLogManager, T> item = item;
					string sectionId = WebGreaseContext.ToStringId(CS$<>8__locals1.idParts(item.Item3));
					CS$<>8__locals1.parallelAction(item.Item1, item.Item3, state);
					TimeMeasureResult[] measureResult = item.Item1.Measure.GetResults();
					Safe.Lock(serialLock, int.MaxValue, delegate()
					{
						CS$<>8__locals18.<>4__this.threadedMeasureResults.AddRange(item.Item1.ThreadedMeasureResults);
						CS$<>8__locals18.<>4__this.threadedMeasureResults.Add(new KeyValuePair<string, IEnumerable<TimeMeasureResult>>(sectionId, measureResult));
						item.Item2.Flush();
						done++;
						if (done == parallelForEachItems.Count - 1)
						{
							parallelForEachItems.ForEach(delegate(Tuple<IWebGreaseContext, DelayedLogManager, T> i)
							{
								i.Item2.Flush();
							});
						}
					});
				});
			});
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00083734 File Offset: 0x00081934
		internal static string ToStringId(IEnumerable<string> idParts)
		{
			return string.Join(".", idParts);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00083744 File Offset: 0x00081944
		internal static IEnumerable<string> ToIdParts(string id)
		{
			return id.Split(new char[]
			{
				"."[0]
			});
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00083770 File Offset: 0x00081970
		internal static string ComputeContentHash(string content, Encoding encoding = null)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream, encoding ?? WebGreaseContext.DefaultEncoding.Value);
				streamWriter.Write(content);
				streamWriter.Flush();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = WebGreaseContext.BytesToHash(WebGreaseContext.Hasher.Value.ComputeHash(memoryStream));
			}
			return result;
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x000837E4 File Offset: 0x000819E4
		internal static string ComputeFileHash(string filePath)
		{
			string result;
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				result = WebGreaseContext.BytesToHash(WebGreaseContext.Hasher.Value.ComputeHash(fileStream));
			}
			return result;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00083848 File Offset: 0x00081A48
		private static bool DelTree(string directory, string[] filesToIgnore)
		{
			bool flag = false;
			string[] files = Directory.GetFiles(directory);
			string[] array = files;
			int i = 0;
			while (i < array.Length)
			{
				string file = array[i];
				if (filesToIgnore == null)
				{
					goto IL_40;
				}
				if (!filesToIgnore.Any((string fti) => file.EndsWith(fti, StringComparison.OrdinalIgnoreCase)))
				{
					goto IL_40;
				}
				flag = true;
				IL_4F:
				i++;
				continue;
				IL_40:
				File.Delete(file);
				goto IL_4F;
			}
			string[] directories = Directory.GetDirectories(directory);
			foreach (string directory2 in directories)
			{
				flag |= WebGreaseContext.DelTree(directory2, filesToIgnore);
			}
			if (!flag)
			{
				Directory.Delete(directory);
			}
			return flag;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000838F0 File Offset: 0x00081AF0
		private static string ComputeBitmapHash(Bitmap bitmap, ImageFormat format)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmap.Save(memoryStream, format);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = WebGreaseContext.BytesToHash(WebGreaseContext.Hasher.Value.ComputeHash(memoryStream));
			}
			return result;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00083948 File Offset: 0x00081B48
		private static string BytesToHash(byte[] hash)
		{
			return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x0008396C File Offset: 0x00081B6C
		private void CleanDirectory(string directory, string[] filesToIgnore = null)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
				{
					WebGreaseContext.DelTree(directory, filesToIgnore);
				}
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}
			}
			catch (Exception ex)
			{
				this.Log.Warning("Error while cleaning {0}: {1}".InvariantFormat(new object[]
				{
					directory,
					ex.Message
				}));
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x000839E0 File Offset: 0x00081BE0
		private void Initialize(WebGreaseConfiguration configuration, LogManager logManager, ICacheManager cacheManager, PreprocessingManager preprocessingManager, DateTimeOffset runStartTime, ITimeMeasure timeMeasure)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (configuration.Global.TreatWarningsAsErrors != null)
			{
				logManager.TreatWarningsAsErrors = (configuration.Global.TreatWarningsAsErrors == true);
			}
			this.Configuration = configuration;
			this.Configuration.Validate();
			this.Measure = timeMeasure;
			this.Log = logManager;
			this.Cache = cacheManager;
			this.Preprocessing = preprocessingManager;
			this.SessionStartTime = runStartTime;
			this.Cache.SetContext(this);
			this.Preprocessing.SetContext(this);
		}

		// Token: 0x04000C01 RID: 3073
		private const string IdPartsDelimiter = ".";

		// Token: 0x04000C02 RID: 3074
		private static readonly ConcurrentDictionary<string, Tuple<DateTime, long, string>> CachedFileHashes = new ConcurrentDictionary<string, Tuple<DateTime, long, string>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000C03 RID: 3075
		private static readonly ThreadLocal<MD5CryptoServiceProvider> Hasher = new ThreadLocal<MD5CryptoServiceProvider>(() => new MD5CryptoServiceProvider());

		// Token: 0x04000C04 RID: 3076
		private static readonly ThreadLocal<Encoding> DefaultEncoding = new ThreadLocal<Encoding>(() => new UTF8Encoding(false, true));

		// Token: 0x04000C05 RID: 3077
		private readonly ConcurrentDictionary<string, string> sessionCachedFileHashes = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000C06 RID: 3078
		private readonly IDictionary<string, IDictionary<string, string>> availableFileCollections = new Dictionary<string, IDictionary<string, string>>();

		// Token: 0x04000C07 RID: 3079
		private readonly List<KeyValuePair<string, IEnumerable<TimeMeasureResult>>> threadedMeasureResults = new List<KeyValuePair<string, IEnumerable<TimeMeasureResult>>>();
	}
}
