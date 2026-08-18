using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease.Activities
{
	// Token: 0x0200003C RID: 60
	internal class BundleActivity
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x0000BAF5 File Offset: 0x00009CF5
		public BundleActivity(WebGreaseContext webGreaseContext)
		{
			this.context = webGreaseContext;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000BB04 File Offset: 0x00009D04
		internal bool Execute(IEnumerable<IFileSet> fileSets)
		{
			bool flag = true;
			flag &= this.BundleFileSets(fileSets.OfType<JSFileSet>(), FileTypes.JS);
			return flag & this.BundleFileSets(fileSets.OfType<CssFileSet>(), FileTypes.CSS);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000BD48 File Offset: 0x00009F48
		private bool BundleFileSets(IEnumerable<IFileSet> fileSets, FileTypes fileType)
		{
			if (fileSets.Any<IFileSet>())
			{
				var varBySettings = new
				{
					fileSets,
					fileType,
					this.context.Configuration
				};
				return this.context.SectionedAction(new string[]
				{
					"BundleActivity",
					fileType.ToString()
				}).MakeCachable(varBySettings, true, false).RestoreFromCacheAction(new Func<ICacheSection, bool>(this.RestoreBundleFromCache)).Execute(delegate(ICacheSection cacheSection)
				{
					this.context.Log.Information("Begin {0} bundle pipeline".InvariantFormat(new object[]
					{
						fileType
					}), MessageImportance.Normal);
					bool result = this.Bundle(fileSets, fileType);
					this.context.Log.Information("End {0} bundle pipeline".InvariantFormat(new object[]
					{
						fileType
					}), MessageImportance.Normal);
					return result;
				});
			}
			return true;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000BE14 File Offset: 0x0000A014
		private bool RestoreBundleFromCache(ICacheSection cacheSection)
		{
			IEnumerable<ContentItem> cachedContentItems = cacheSection.GetCachedContentItems("AssemblerResult", true);
			cachedContentItems.ForEach(delegate(ContentItem er)
			{
				er.WriteToContentPath(this.context.Configuration.DestinationDirectory, false);
			});
			return true;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C04C File Offset: 0x0000A24C
		private bool Bundle(IEnumerable<IFileSet> fileSets, FileTypes fileType)
		{
			bool flag = true;
			WebGreaseContext sessionContext = this.context;
			using (IEnumerator<IFileSet> enumerator = fileSets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IFileSet fileSet = enumerator.Current;
					string configType = this.context.Configuration.ConfigType;
					BundlingConfig bundleConfig = fileSet.Bundling.GetNamedConfig(configType);
					if (bundleConfig.ShouldBundleFiles)
					{
						string outputFile = Path.Combine(this.context.Configuration.DestinationDirectory, fileSet.Output);
						PreprocessingConfig preprocessingConfig = fileSet.Preprocessing.GetNamedConfig(configType);
						flag &= sessionContext.SectionedAction(new string[]
						{
							"BundleActivity",
							fileType.ToString(),
							"Process"
						}).MakeCachable(fileSet, new
						{
							bundleConfig,
							preprocessingConfig
						}, true, false).RestoreFromCacheAction(new Func<ICacheSection, bool>(this.RestoreBundleFromCache)).Execute(delegate(ICacheSection fileSetCacheSection)
						{
							fileSet.LoadedConfigurationFiles.ForEach(new Action<string>(fileSetCacheSection.AddSourceDependency));
							if (Path.GetExtension(outputFile).IsNullOrWhitespace())
							{
								Console.WriteLine(ResourceStrings.InvalidBundlingOutputFile, outputFile);
								return true;
							}
							AssemblerActivity assemblerActivity = new AssemblerActivity(sessionContext);
							assemblerActivity.OutputFile = outputFile;
							assemblerActivity.Inputs.Clear();
							assemblerActivity.PreprocessingConfig = preprocessingConfig;
							assemblerActivity.Inputs.AddRange(fileSet.InputSpecs);
							assemblerActivity.MinimalOutput = bundleConfig.MinimalOutput;
							ContentItem contentItem = assemblerActivity.Execute(ContentItemType.Path);
							fileSetCacheSection.AddResult(contentItem, "AssemblerResult", true);
							return true;
						});
					}
				}
			}
			return flag;
		}

		// Token: 0x040000D6 RID: 214
		private readonly WebGreaseContext context;
	}
}
