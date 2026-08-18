using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using WebGrease.Configuration;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease.Preprocessing
{
	// Token: 0x020001B8 RID: 440
	public class PreprocessingManager
	{
		// Token: 0x06001666 RID: 5734 RVA: 0x00081170 File Offset: 0x0007F370
		internal PreprocessingManager(WebGreaseConfiguration webGreaseConfiguration, LogManager logManager, ITimeMeasure timeMeasure)
		{
			if (webGreaseConfiguration == null)
			{
				throw new ArgumentNullException("webGreaseConfiguration");
			}
			if (logManager == null)
			{
				throw new ArgumentNullException("logManager");
			}
			if (timeMeasure == null)
			{
				throw new ArgumentNullException("timeMeasure");
			}
			this.Initialize(webGreaseConfiguration.PreprocessingPluginPath, logManager, timeMeasure);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000811D4 File Offset: 0x0007F3D4
		internal PreprocessingManager(PreprocessingManager preprocessingManager)
		{
			preprocessingManager.registeredPreprocessingEngines.ForEach(delegate(IPreprocessingEngine rp)
			{
				this.registeredPreprocessingEngines.Add(rp);
			});
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00081210 File Offset: 0x0007F410
		internal void SetContext(IWebGreaseContext webGreaseContext)
		{
			this.context = webGreaseContext;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00081464 File Offset: 0x0007F664
		internal ContentItem Process(ContentItem contentItem, PreprocessingConfig preprocessConfig, bool minimalOutput = false)
		{
			this.context.Log.Information("Registered preprocessors to use: {0}".InvariantFormat(new object[]
			{
				string.Join(";", preprocessConfig.PreprocessingEngines)
			}), MessageImportance.Normal);
			IPreprocessingEngine[] preprocessorsToUse = this.GetProcessors(contentItem, preprocessConfig);
			if (!preprocessorsToUse.Any<IPreprocessingEngine>())
			{
				return contentItem;
			}
			this.context.SectionedAction(new string[]
			{
				"Preprocessing"
			}).MakeCachable(contentItem, new
			{
				relativePath = Path.GetDirectoryName(contentItem.RelativeContentPath),
				preprocessConfig = preprocessConfig,
				pptu = from pptu in preprocessorsToUse
				select pptu.Name
			}, false, false).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
			{
				contentItem = cacheSection.GetCachedContentItem("PreprocessingResult");
				return contentItem != null;
			}).Execute(delegate(ICacheSection cacheSection)
			{
				IPreprocessingEngine[] preprocessorsToUse;
				foreach (IPreprocessingEngine preprocessingEngine in preprocessorsToUse)
				{
					this.context.Log.Information("preprocessing with: {0}".InvariantFormat(new object[]
					{
						preprocessingEngine.Name
					}), MessageImportance.Normal);
					contentItem = preprocessingEngine.Process(this.context, contentItem, preprocessConfig, minimalOutput);
					if (contentItem == null)
					{
						return false;
					}
				}
				cacheSection.AddResult(contentItem, "PreprocessingResult", false);
				return true;
			});
			return contentItem;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00081604 File Offset: 0x0007F804
		internal IPreprocessingEngine[] GetProcessors(ContentItem contentItem, PreprocessingConfig preprocessConfig)
		{
			return (from pptu in preprocessConfig.PreprocessingEngines.SelectMany((string ppe) => from rppe in this.registeredPreprocessingEngines
			where rppe.Name.Equals(ppe, StringComparison.OrdinalIgnoreCase)
			select rppe)
			where pptu.CanProcess(this.context, contentItem, preprocessConfig)
			select pptu).ToArray<IPreprocessingEngine>();
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00081660 File Offset: 0x0007F860
		private void Initialize(string pluginPath, LogManager logManager, ITimeMeasure timeMeasure)
		{
			timeMeasure.Start(false, new string[]
			{
				"Preprocessing",
				"Initialize"
			});
			logManager.Information(ResourceStrings.PreprocessingInitializeStart.InvariantFormat(new object[]
			{
				pluginPath
			}), MessageImportance.Normal);
			if (string.IsNullOrWhiteSpace(pluginPath))
			{
				FileInfo fileInfo = new FileInfo(Assembly.GetCallingAssembly().FullName);
				pluginPath = fileInfo.DirectoryName;
			}
			if (!string.IsNullOrWhiteSpace(pluginPath))
			{
				if (!Directory.Exists(pluginPath))
				{
					logManager.Error(new DirectoryNotFoundException(pluginPath), ResourceStrings.PreprocessingCouldNotFindThePluginPath.InvariantFormat(new object[]
					{
						pluginPath
					}), null);
					return;
				}
				logManager.Information(ResourceStrings.PreprocessingPluginPath.InvariantFormat(new object[]
				{
					pluginPath
				}), MessageImportance.Normal);
				using (AggregateCatalog aggregateCatalog = new AggregateCatalog())
				{
					aggregateCatalog.Catalogs.Add(new DirectoryCatalog(pluginPath));
					using (CompositionContainer compositionContainer = new CompositionContainer(aggregateCatalog, new ExportProvider[0]))
					{
						try
						{
							compositionContainer.ComposeParts(new object[]
							{
								this
							});
						}
						catch (CompositionException exception)
						{
							logManager.Error(exception, ResourceStrings.PreprocessingLoadingError, null);
						}
						foreach (IPreprocessingEngine preprocessingEngine in this.registeredPreprocessingEngines)
						{
							logManager.Information(ResourceStrings.PreprocessingEngineFound.InvariantFormat(new object[]
							{
								preprocessingEngine.Name
							}), MessageImportance.Normal);
						}
					}
				}
			}
			logManager.Information(ResourceStrings.PreprocessingInitializeEnd, MessageImportance.Normal);
			timeMeasure.End(false, new string[]
			{
				"Preprocessing",
				"Initialize"
			});
		}

		// Token: 0x04000BCE RID: 3022
		[ImportMany(typeof(IPreprocessingEngine))]
		private readonly IList<IPreprocessingEngine> registeredPreprocessingEngines = new List<IPreprocessingEngine>();

		// Token: 0x04000BCF RID: 3023
		private IWebGreaseContext context;
	}
}
