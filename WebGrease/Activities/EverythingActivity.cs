using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Antlr.Runtime;
using WebGrease.Configuration;
using WebGrease.Css;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease.Activities
{
	// Token: 0x02000038 RID: 56
	internal sealed class EverythingActivity
	{
		// Token: 0x06000387 RID: 903 RVA: 0x000088B0 File Offset: 0x00006AB0
		internal EverythingActivity(IWebGreaseContext context)
		{
			this.context = context;
			this.logDirectory = context.Configuration.LogsDirectory;
			this.toolsTempDirectory = (context.Configuration.ToolsTempDirectory.IsNullOrWhitespace() ? Path.Combine(context.Configuration.LogsDirectory, "ToolsTemp") : context.Configuration.ToolsTempDirectory);
			this.imagesLogFile = Path.Combine(this.logDirectory, "images_log.xml");
			this.preprocessingTempDirectory = Path.Combine(this.toolsTempDirectory, "PreCompiler");
			this.staticAssemblerDirectory = Path.Combine(this.toolsTempDirectory, "StaticAssemblerOutput");
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00008958 File Offset: 0x00006B58
		internal static string DpiToResolutionName(float dpi)
		{
			return "Resolution{0:0.##}X".InvariantFormat(new object[]
			{
				dpi.ToString(CultureInfo.InvariantCulture).Replace(".", string.Empty)
			});
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00008995 File Offset: 0x00006B95
		internal bool Execute()
		{
			this.ExecuteHashImages();
			return this.Execute(this.context.Configuration.CssFileSets.OfType<IFileSet>().Concat(this.context.Configuration.JSFileSets), FileTypes.All);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal bool Execute(IEnumerable<IFileSet> fileSets, FileTypes fileType = FileTypes.All)
		{
			bool flag = true;
			JSFileSet[] array = fileSets.OfType<JSFileSet>().ToArray<JSFileSet>();
			if (array.Any<JSFileSet>())
			{
				flag &= this.ExecuteJS(array, this.context.Configuration.ConfigType, this.context.Configuration.SourceDirectory, this.context.Configuration.DestinationDirectory);
			}
			CssFileSet[] array2 = fileSets.OfType<CssFileSet>().ToArray<CssFileSet>();
			if (array2.Any<CssFileSet>())
			{
				flag &= this.ExecuteCss(array2, this.context.Configuration.SourceDirectory, this.context.Configuration.DestinationDirectory, this.context.Configuration.ConfigType, this.context.Configuration.ImageDirectories, this.context.Configuration.ImageExtensions);
			}
			if (fileType.HasFlag(FileTypes.Image))
			{
				this.ExecuteHashImages();
			}
			return flag;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00008AB8 File Offset: 0x00006CB8
		internal void ExecuteHashImages()
		{
			if (this.context.Configuration.ImageDirectoriesToHash.Any<string>())
			{
				FileHasherActivity imageFileHasher = this.GetImageFileHasher(this.context.Configuration.DestinationDirectory, this.context.Configuration.ImageExtensions);
				EverythingActivity.HashImages(this.context, imageFileHasher, this.context.Configuration.ImageDirectoriesToHash, this.context.Configuration.ImageExtensions);
				imageFileHasher.Save(true);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00008B38 File Offset: 0x00006D38
		private static FileHasherActivity GetFileHasher(IWebGreaseContext context, string hashOutputPath, string logFileName, FileTypes fileType, string outputRelativeToPath, string basePrefixToAddToOutputPath = null, IEnumerable<string> fileTypeFilters = null)
		{
			string fileTypeFilter = (fileTypeFilters != null) ? string.Join(new string(Strings.FileFilterSeparator), fileTypeFilters) : null;
			return new FileHasherActivity(context)
			{
				DestinationDirectory = hashOutputPath,
				BasePrefixToRemoveFromOutputPathInLog = outputRelativeToPath,
				CreateExtraDirectoryLevelFromHashes = true,
				ShouldPreserveSourceDirectoryStructure = false,
				LogFileName = logFileName,
				FileType = fileType,
				FileTypeFilter = fileTypeFilter,
				BasePrefixToAddToOutputPath = basePrefixToAddToOutputPath
			};
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00008BA0 File Offset: 0x00006DA0
		private static IDictionary<string, IDictionary<string, string>> GetMergedResources(IWebGreaseContext context, FileTypes fileType, string resourceGroupKey, IEnumerable<string> resourceKeys)
		{
			ResourcesResolutionActivity resourcesResolutionActivity = new ResourcesResolutionActivity(context)
			{
				SourceDirectory = context.Configuration.SourceDirectory,
				ApplicationDirectoryName = context.Configuration.TokensDirectory,
				SiteDirectoryName = context.Configuration.OverrideTokensDirectory,
				ResourceGroupKey = resourceGroupKey,
				FileType = fileType
			};
			resourcesResolutionActivity.ResourceKeys.AddRange(resourceKeys);
			return resourcesResolutionActivity.GetMergedResources();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00008C09 File Offset: 0x00006E09
		private static void EnsureCssLogFile(FileHasherActivity cssHasher, FileHasherActivity imageHasher, ICacheSection cacheSection)
		{
			EverythingActivity.EnsureLogFile(cssHasher, cacheSection.GetCachedContentItems("HashedMinifiedCssResult", false));
			EverythingActivity.EnsureLogFile(imageHasher, cacheSection.GetCachedContentItems("HashedImage", false));
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00008C54 File Offset: 0x00006E54
		private static string GetContentPivotDestinationFilePath(string relativeContentPath, string destinationDirectoryName, string destinationExtension, string destinationPathFormat, IEnumerable<ResourcePivotKey> resourcePivotKeys = null)
		{
			if (string.IsNullOrWhiteSpace(destinationPathFormat))
			{
				ResourcePivotKey resourcePivotKey;
				if (resourcePivotKeys == null)
				{
					resourcePivotKey = null;
				}
				else
				{
					resourcePivotKey = resourcePivotKeys.FirstOrDefault((ResourcePivotKey rpk) => rpk.GroupKey.Equals("themes"));
				}
				ResourcePivotKey resourcePivotKey2 = resourcePivotKey;
				string str = (resourcePivotKey2 != null && !resourcePivotKey2.Key.IsNullOrWhitespace()) ? (resourcePivotKey2.Key + "_") : string.Empty;
				ResourcePivotKey resourcePivotKey3;
				if (resourcePivotKeys == null)
				{
					resourcePivotKey3 = null;
				}
				else
				{
					resourcePivotKey3 = resourcePivotKeys.FirstOrDefault((ResourcePivotKey rpk) => rpk.GroupKey.Equals("locales"));
				}
				ResourcePivotKey resourcePivotKey4 = resourcePivotKey3;
				string path = (resourcePivotKey4 != null && !resourcePivotKey4.Key.IsNullOrWhitespace()) ? resourcePivotKey4.Key : string.Empty;
				return Path.Combine(path, destinationDirectoryName, str + Path.ChangeExtension(relativeContentPath, destinationExtension));
			}
			destinationPathFormat = destinationPathFormat.ToLowerInvariant();
			if (resourcePivotKeys != null)
			{
				foreach (ResourcePivotKey resourcePivotKey5 in resourcePivotKeys)
				{
					destinationPathFormat = destinationPathFormat.Replace("{" + resourcePivotKey5.GroupKey.ToLowerInvariant() + "}", resourcePivotKey5.Key);
				}
			}
			destinationPathFormat = destinationPathFormat.Replace("{output}", relativeContentPath);
			if (destinationPathFormat.IndexOf("{", StringComparison.OrdinalIgnoreCase) != -1)
			{
				throw new BuildWorkflowException("Could not generate the correct output file, one key was not replaced: {0}".InvariantFormat(new object[]
				{
					destinationPathFormat
				}));
			}
			return Path.Combine(destinationDirectoryName, Path.ChangeExtension(destinationPathFormat, destinationExtension));
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00008DDC File Offset: 0x00006FDC
		private static void EnsureJsLogFile(FileHasherActivity jsHasher, ICacheSection cacheSection)
		{
			EverythingActivity.EnsureLogFile(jsHasher, cacheSection.GetCachedContentItems("HashedMinifiedJsResult", false));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00008DF0 File Offset: 0x00006FF0
		private static void EnsureLogFile(FileHasherActivity hasher, IEnumerable<ContentItem> contentItems)
		{
			hasher.AppendToWorkLog(contentItems);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000090A4 File Offset: 0x000072A4
		private static void HashImages(IWebGreaseContext context, FileHasherActivity imageHasher, IEnumerable<string> imageDirectoriesToHash, IEnumerable<string> imageExtensions)
		{
			context.SectionedAction(new string[]
			{
				"ImageHash"
			}).MakeCachable(new
			{
				imageDirectoriesToHash,
				imageExtensions
			}, false, false).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
			{
				IEnumerable<ContentItem> cachedContentItems = cacheSection.GetCachedContentItems("HashedImage", false);
				cachedContentItems.ForEach(delegate(ContentItem ci)
				{
					ci.WriteToRelativeHashedPath(context.Configuration.DestinationDirectory, false);
				});
				EverythingActivity.EnsureLogFile(imageHasher, cachedContentItems);
				return true;
			}).WhenSkipped(delegate(ICacheSection cacheSection)
			{
				EverythingActivity.EnsureLogFile(imageHasher, cacheSection.GetCachedContentItems("HashedImage", false));
			}).Execute(delegate(ICacheSection cacheSection)
			{
				IEnumerable<InputSpec> list = from imageDirectoryToHash in imageDirectoriesToHash
				select new InputSpec
				{
					Path = imageDirectoryToHash,
					IsOptional = true,
					SearchPattern = "*.*",
					SearchOption = SearchOption.AllDirectories
				};
				list.ForEach(new Action<InputSpec>(cacheSection.AddSourceDependency));
				IDictionary<string, string> availableFiles = context.GetAvailableFiles(context.Configuration.SourceDirectory, imageDirectoriesToHash, imageExtensions, FileTypes.Image);
				foreach (KeyValuePair<string, string> keyValuePair in availableFiles)
				{
					ContentItem contentItem = ContentItem.FromFile(keyValuePair.Value, keyValuePair.Key, null, new ResourcePivotKey[0]);
					ContentItem contentItem2 = imageHasher.Hash(contentItem);
					cacheSection.AddResult(contentItem2, "HashedImage", true);
				}
				return true;
			});
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00009160 File Offset: 0x00007360
		private static IEnumerable<IEnumerable<ResourcePivotKey>> GetGroupedResourceKeys(ResourcePivotKey[] flatResourceKeyList)
		{
			IEnumerable<string> enumerable = (from k in flatResourceKeyList
			select k.GroupKey).Distinct<string>();
			List<IEnumerable<ResourcePivotKey>> list = new List<IEnumerable<ResourcePivotKey>>();
			using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string groupKey = enumerator.Current;
					List<IEnumerable<ResourcePivotKey>> list2 = new List<IEnumerable<ResourcePivotKey>>();
					IEnumerable<ResourcePivotKey> enumerable2 = from k in flatResourceKeyList
					where k.GroupKey.Equals(groupKey)
					select k;
					foreach (ResourcePivotKey resourcePivotKey in enumerable2)
					{
						if (!list.Any<IEnumerable<ResourcePivotKey>>())
						{
							list2.Add(new List<ResourcePivotKey>(new ResourcePivotKey[]
							{
								resourcePivotKey
							}));
						}
						else
						{
							foreach (IEnumerable<ResourcePivotKey> first in list)
							{
								list2.Add(first.Concat(new ResourcePivotKey[]
								{
									resourcePivotKey
								}));
							}
						}
					}
					list = list2;
				}
			}
			return list;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000092C0 File Offset: 0x000074C0
		private IEnumerable<string> GetDestinationFilePaths(ContentItem inputFile, string destinationDirectoryName, string destinationExtension, string destinationPathFormat)
		{
			if (inputFile.ResourcePivotKeys == null || !inputFile.ResourcePivotKeys.Any<ResourcePivotKey>())
			{
				return new string[]
				{
					EverythingActivity.GetContentPivotDestinationFilePath(inputFile.RelativeContentPath, destinationDirectoryName, destinationExtension, destinationPathFormat, null)
				};
			}
			List<string> list = new List<string>();
			foreach (IEnumerable<ResourcePivotKey> enumerable in EverythingActivity.GetGroupedResourceKeys(inputFile.ResourcePivotKeys.ToArray<ResourcePivotKey>()))
			{
				if (!this.context.TemporaryIgnore(enumerable))
				{
					list.Add(EverythingActivity.GetContentPivotDestinationFilePath(inputFile.RelativeContentPath, destinationDirectoryName, destinationExtension, destinationPathFormat, enumerable));
				}
			}
			return list;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000096A0 File Offset: 0x000078A0
		private bool ExecuteCss(IEnumerable<CssFileSet> cssFileSets, string sourceDirectory, string destinationDirectory, string configType, IList<string> imageDirectories, IList<string> imageExtensions)
		{
			string logFileName = Path.Combine(this.context.Configuration.LogsDirectory, "css_log.xml");
			string hashOutputPath = Path.Combine(destinationDirectory, "css");
			string imageHashedOutputPath = Path.Combine(destinationDirectory, "i");
			if (!cssFileSets.Any<CssFileSet>())
			{
				return true;
			}
			FileHasherActivity imageHasher = this.GetImageFileHasher(destinationDirectory, imageExtensions);
			FileHasherActivity cssHasher = EverythingActivity.GetFileHasher(this.context, hashOutputPath, logFileName, FileTypes.CSS, this.context.Configuration.ApplicationRootDirectory, null, null);
			bool flag = this.context.SectionedAction(new string[]
			{
				"EverythingActivity",
				"Css"
			}).MakeCachable(new
			{
				cssFileSets,
				sourceDirectory,
				destinationDirectory,
				configType,
				imageExtensions,
				imageDirectories
			}, true, false).WhenSkipped(delegate(ICacheSection cacheSection)
			{
				EverythingActivity.EnsureCssLogFile(cssHasher, imageHasher, cacheSection);
			}).Execute(delegate(ICacheSection cacheSection)
			{
				bool flag2 = true;
				foreach (CssFileSet cssFileSet in cssFileSets)
				{
					flag2 &= this.ExecuteCssFileSet(configType, imageDirectories, imageExtensions, cssFileSet, cssHasher, imageHasher, imageHashedOutputPath);
				}
				return flag2;
			});
			if (flag)
			{
				imageHasher.Save(true);
				cssHasher.Save(true);
			}
			return flag;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000097E5 File Offset: 0x000079E5
		private FileHasherActivity GetImageFileHasher(string destinationDirectory, IList<string> imageExtensions)
		{
			return EverythingActivity.GetFileHasher(this.context, Path.Combine(destinationDirectory, "i"), this.imagesLogFile, FileTypes.Image, destinationDirectory, "../../", imageExtensions);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000A0A0 File Offset: 0x000082A0
		private bool ExecuteCssFileSet(string configType, IList<string> imageDirectories, IList<string> imageExtensions, CssFileSet cssFileSet, FileHasherActivity cssHasher, FileHasherActivity imageHasher, string imagesDestinationDirectory)
		{
			CssSpritingConfig cssSpritingConfig = cssFileSet.ImageSpriting.GetNamedConfig(configType);
			CssMinificationConfig cssMinificationConfig = cssFileSet.Minification.GetNamedConfig(configType);
			var varBySettings = new
			{
				configType = configType,
				ImageSpriting = cssSpritingConfig,
				Global = cssFileSet.GlobalConfig,
				Bundling = cssFileSet.Bundling.GetNamedConfig(configType),
				Minification = cssMinificationConfig,
				Preprocessing = cssFileSet.Preprocessing.GetNamedConfig(configType),
				Locales = cssFileSet.Locales,
				Themes = cssFileSet.Themes
			};
			return this.context.SectionedAction(new string[]
			{
				"CssFileSet"
			}).MakeCachable(cssFileSet, varBySettings, true, false).WhenSkipped(delegate(ICacheSection cacheSection)
			{
				EverythingActivity.EnsureCssLogFile(cssHasher, imageHasher, cacheSection);
			}).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
			{
				cssFileSet.LoadedConfigurationFiles.ForEach(new Action<string>(cacheSection.AddSourceDependency));
				IEnumerable<ContentItem> cachedContentItems = cacheSection.GetCachedContentItems("HashedMinifiedCssResult", false);
				cachedContentItems.ForEach(delegate(ContentItem ci)
				{
					ci.WriteToRelativeHashedPath(this.context.Configuration.DestinationDirectory, false);
				});
				EverythingActivity.EnsureLogFile(cssHasher, cachedContentItems);
				IEnumerable<ContentItem> cachedContentItems2 = cacheSection.GetCachedContentItems("HashedImage", false);
				cachedContentItems2.ForEach(delegate(ContentItem ci)
				{
					ci.WriteToRelativeHashedPath(this.context.Configuration.DestinationDirectory, false);
				});
				EverythingActivity.EnsureLogFile(imageHasher, cachedContentItems2);
				IEnumerable<ContentItem> cachedContentItems3 = cacheSection.GetCachedContentItems("HashedSpriteImage", false);
				cachedContentItems3.ForEach(delegate(ContentItem ci)
				{
					ci.WriteToContentPath(this.context.Configuration.DestinationDirectory, false);
				});
				return cachedContentItems.Any<ContentItem>();
			}).Execute(delegate(ICacheSection cacheSection)
			{
				Dictionary<string, IDictionary<string, IDictionary<string, string>>> mergedResource = (from rp in cssFileSet.ResourcePivots
				where rp.ApplyMode == ResourcePivotApplyMode.ApplyAsStringReplace
				select rp).ToDictionary((ResourcePivotGroup rpg) => rpg.Key, (ResourcePivotGroup rpg) => EverythingActivity.GetMergedResources(this.context, FileTypes.CSS, rpg.Key, rpg.Keys));
				Dictionary<string, IDictionary<string, IDictionary<string, string>>> dictionary = (from rp in cssFileSet.ResourcePivots
				where rp.ApplyMode != ResourcePivotApplyMode.ApplyAsStringReplace
				select rp).ToDictionary((ResourcePivotGroup rpg) => rpg.Key, (ResourcePivotGroup rpg) => EverythingActivity.GetMergedResources(this.context, FileTypes.CSS, rpg.Key, rpg.Keys));
				IDictionary<string, IDictionary<string, string>> mergedResources = EverythingActivity.GetMergedResources(this.context, FileTypes.CSS, "dpi", cssFileSet.Dpi.Select(new Func<float, string>(EverythingActivity.DpiToResolutionName)));
				MinifyCssActivity minifyCssActivity = this.CreateCssMinifier(imageHasher, imageExtensions, imageDirectories, imagesDestinationDirectory, cssMinificationConfig, cssSpritingConfig, cssFileSet.Dpi, dictionary, mergedResources);
				string outputFile = Path.Combine(this.staticAssemblerDirectory, cssFileSet.Output);
				IEnumerable<ContentItem> enumerable = this.Bundle(cssFileSet, outputFile, FileTypes.CSS, configType, minifyCssActivity.ShouldMinify);
				if (enumerable == null)
				{
					return false;
				}
				this.context.Log.Information(ResourceStrings.ResolvingTokensAndPerformingLocalization, MessageImportance.Normal);
				IEnumerable<ContentItem> enumerable2 = this.ApplyResources(enumerable, mergedResource);
				if (!enumerable2.All((ContentItem l) => l != null))
				{
					this.context.Log.Error(null, ResourceStrings.ThereWereErrorsWhileApplyingCssresources, null);
					return false;
				}
				IEnumerable<MinifyCssResult> source = this.MinifyCss(enumerable2, minifyCssActivity, imageHasher, cssSpritingConfig.WriteLogFile, dictionary);
				if (source.Any((MinifyCssResult i) => i == null))
				{
					this.context.Log.Error(null, ResourceStrings.ThereWereErrorsWhileMinifyingTheCssFiles, null);
					return false;
				}
				IEnumerable<ContentItem> list = this.HashContentItems(cssHasher, from n in source.SelectMany((MinifyCssResult i) => i.Css)
				where n != null
				select n, "css", "css", cssFileSet.OutputPathFormat);
				list.ForEach(delegate(ContentItem hi)
				{
					cacheSection.AddResult(hi, "HashedMinifiedCssResult", false);
				});
				IEnumerable<ContentItem> list2 = from n in source.SelectMany((MinifyCssResult mci) => mci.HashedImages)
				where n != null
				select n;
				list2.ForEach(delegate(ContentItem hi)
				{
					cacheSection.AddResult(hi, "HashedImage", false);
				});
				return true;
			});
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000A43C File Offset: 0x0000863C
		private bool ExecuteJS(IEnumerable<JSFileSet> jsFileSets, string configType, string sourceDirectory, string destinationDirectory)
		{
			string logFileName = Path.Combine(this.context.Configuration.LogsDirectory, "js_log.xml");
			string hashOutputPath = Path.Combine(destinationDirectory, "js");
			if (!jsFileSets.Any<JSFileSet>())
			{
				return true;
			}
			FileHasherActivity jsHasher = EverythingActivity.GetFileHasher(this.context, hashOutputPath, logFileName, FileTypes.JS, this.context.Configuration.ApplicationRootDirectory, null, null);
			var varBySettings = new
			{
				jsFileSets,
				configType,
				sourceDirectory,
				destinationDirectory
			};
			bool flag = this.context.SectionedAction(new string[]
			{
				"EverythingActivity",
				"Js"
			}).MakeCachable(varBySettings, true, false).WhenSkipped(delegate(ICacheSection cacheSection)
			{
				EverythingActivity.EnsureJsLogFile(jsHasher, cacheSection);
			}).Execute(delegate(ICacheSection cacheSection)
			{
				bool flag2 = true;
				foreach (JSFileSet jsFileSet in jsFileSets)
				{
					flag2 &= this.ExecuteJSFileSet(jsFileSet, jsHasher, configType);
				}
				return flag2;
			});
			if (flag)
			{
				jsHasher.Save(true);
			}
			return flag;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		private bool ExecuteJSFileSet(JSFileSet jsFileSet, FileHasherActivity jsHasher, string configType)
		{
			return this.context.SectionedAction(new string[]
			{
				"JsFileSet"
			}).MakeCachable(jsFileSet, new
			{
				configType
			}, true, false).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
			{
				IEnumerable<ContentItem> cachedContentItems = cacheSection.GetCachedContentItems("HashedMinifiedJsResult", false);
				cachedContentItems.ForEach(delegate(ContentItem ci)
				{
					ci.WriteToRelativeHashedPath(this.context.Configuration.DestinationDirectory, false);
				});
				EverythingActivity.EnsureLogFile(jsHasher, cachedContentItems);
				return cachedContentItems.Any<ContentItem>();
			}).WhenSkipped(delegate(ICacheSection cacheSection)
			{
				EverythingActivity.EnsureJsLogFile(jsHasher, cacheSection);
			}).Execute(delegate(ICacheSection cacheSection)
			{
				jsFileSet.LoadedConfigurationFiles.ForEach(new Action<string>(cacheSection.AddSourceDependency));
				JsMinificationConfig namedConfig = jsFileSet.Minification.GetNamedConfig(configType);
				string outputFile = Path.Combine(this.staticAssemblerDirectory, jsFileSet.Output);
				IEnumerable<ContentItem> enumerable = this.Bundle(jsFileSet, outputFile, FileTypes.JS, configType, namedConfig.ShouldMinify);
				if (enumerable == null)
				{
					return false;
				}
				Dictionary<string, IDictionary<string, IDictionary<string, string>>> mergedResource = jsFileSet.ResourcePivots.ToDictionary((ResourcePivotGroup rpg) => rpg.Key, (ResourcePivotGroup rpg) => EverythingActivity.GetMergedResources(this.context, FileTypes.CSS, rpg.Key, rpg.Keys));
				this.context.Log.Information(ResourceStrings.ResolvingTokensAndPerformingLocalization, MessageImportance.Normal);
				IEnumerable<ContentItem> enumerable2 = this.ApplyResources(enumerable, mergedResource);
				if (enumerable2 == null)
				{
					this.context.Log.Error(null, "There were errors encountered while resolving tokens.", null);
					return false;
				}
				this.context.Log.Information("Minimizing javascript files", MessageImportance.Normal);
				IEnumerable<ContentItem> enumerable3 = this.MinifyJs(enumerable2, namedConfig, jsFileSet.Validation.GetNamedConfig(configType));
				if (enumerable3.Any((ContentItem ci) => ci == null))
				{
					this.context.Log.Error(null, "There were errors encountered while minimizing javascript files.", null);
					return false;
				}
				IEnumerable<ContentItem> list = this.HashContentItems(jsHasher, enumerable3, "js", "js", jsFileSet.OutputPathFormat);
				list.ForEach(delegate(ContentItem ci)
				{
					cacheSection.AddResult(ci, "HashedMinifiedJsResult", true);
				});
				return true;
			});
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000A948 File Offset: 0x00008B48
		private IEnumerable<ContentItem> HashContentItems(FileHasherActivity hasher, IEnumerable<ContentItem> contentItems, string destinationDirectoryName, string destinationExtension, string destinationPathFormat)
		{
			List<ContentItem> list = new List<ContentItem>();
			foreach (ContentItem contentItem in from ci in contentItems
			where ci != null
			select ci)
			{
				IEnumerable<string> destinationFilePaths = this.GetDestinationFilePaths(contentItem, destinationDirectoryName, destinationExtension, destinationPathFormat);
				list.AddRange(hasher.Hash(contentItem, destinationFilePaths));
			}
			return list;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		private IEnumerable<ContentItem> Bundle(IFileSet fileSet, string outputFile, FileTypes fileType, string configType, bool minimalOutput)
		{
			BundlingConfig namedConfig = fileSet.Bundling.GetNamedConfig(configType);
			PreprocessingConfig namedConfig2 = fileSet.Preprocessing.GetNamedConfig(this.context.Configuration.ConfigType);
			if (namedConfig.ShouldBundleFiles)
			{
				this.context.Log.Information(ResourceStrings.BundlingFiles, MessageImportance.Normal);
				ContentItem contentItem = this.BundleFiles(fileSet.InputSpecs, outputFile, namedConfig2, fileType, minimalOutput || namedConfig.MinimalOutput);
				if (contentItem == null)
				{
					this.context.Log.Error(null, ResourceStrings.ThereWereErrorsWhileBundlingFiles, null);
					return null;
				}
				return new ContentItem[]
				{
					contentItem
				};
			}
			else
			{
				if (namedConfig2 != null && namedConfig2.Enabled)
				{
					return this.PreprocessFiles(this.preprocessingTempDirectory, fileSet.InputSpecs, namedConfig2);
				}
				fileSet.InputSpecs.ForEach(new Action<InputSpec>(this.context.Cache.CurrentCacheSection.AddSourceDependency));
				return from f in fileSet.InputSpecs.GetFiles(this.context.Configuration.SourceDirectory, null, false)
				select ContentItem.FromFile(f, f, this.context.Configuration.SourceDirectory, new ResourcePivotKey[0]);
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000AB00 File Offset: 0x00008D00
		private MinifyCssActivity CreateCssMinifier(FileHasherActivity imageHasher, IList<string> imageExtensions, IList<string> imageDirectories, string imagesDestinationDirectory, CssMinificationConfig minificationConfig, CssSpritingConfig spritingConfig, HashSet<float> dpi, Dictionary<string, IDictionary<string, IDictionary<string, string>>> mergedResoures, IDictionary<string, IDictionary<string, string>> dpiResources)
		{
			return new MinifyCssActivity(this.context)
			{
				ShouldAssembleBackgroundImages = spritingConfig.ShouldAutoSprite,
				ShouldMinify = minificationConfig.ShouldMinify,
				ShouldMergeMediaQueries = minificationConfig.ShouldMergeMediaQueries,
				ShouldOptimize = (minificationConfig.ShouldMinify || minificationConfig.ShouldOptimize),
				ShouldValidateForLowerCase = minificationConfig.ShouldValidateLowerCase,
				ShouldExcludeProperties = minificationConfig.ShouldExcludeProperties,
				ShouldMergeBasedOnCommonDeclarations = minificationConfig.ShouldMergeBasedOnCommonDeclarations,
				ShouldPreventOrderBasedConflict = minificationConfig.ShouldPreventOrderBasedConflict,
				ImageExtensions = imageExtensions,
				ImageDirectories = imageDirectories,
				BannedSelectors = new HashSet<string>(minificationConfig.RemoveSelectors.ToArray<string>()),
				HackSelectors = new HashSet<string>(minificationConfig.ForbiddenSelectors.ToArray<string>()),
				NonMergeSelectors = new HashSet<string>(minificationConfig.NonMergeSelectors.ToArray<string>()),
				ImageAssembleReferencesToIgnore = new HashSet<string>(spritingConfig.ImagesToIgnore.ToArray<string>()),
				ImageAssemblyPadding = new int?(spritingConfig.ImagePadding),
				ErrorOnInvalidSprite = spritingConfig.ErrorOnInvalidSprite,
				OutputUnit = spritingConfig.OutputUnit,
				OutputUnitFactor = spritingConfig.OutputUnitFactor,
				ImagesOutputDirectory = imagesDestinationDirectory,
				IgnoreImagesWithNonDefaultBackgroundSize = spritingConfig.IgnoreImagesWithNonDefaultBackgroundSize,
				ImageBasePrefixToRemoveFromOutputPathInLog = ((imageHasher != null) ? imageHasher.BasePrefixToRemoveFromOutputPathInLog : null),
				ImageBasePrefixToAddToOutputPath = ((imageHasher != null) ? imageHasher.BasePrefixToAddToOutputPath : null),
				ForcedSpritingImageType = spritingConfig.ForceImageType,
				Dpi = dpi,
				MergedResources = mergedResoures,
				DpiResources = dpiResources
			};
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000AC94 File Offset: 0x00008E94
		private IEnumerable<ContentItem> PreprocessFiles(string targetFolder, IEnumerable<InputSpec> inputFiles, PreprocessingConfig preprocessingConfig)
		{
			PreprocessorActivity preprocessorActivity = new PreprocessorActivity(this.context)
			{
				OutputFolder = targetFolder,
				PreprocessingConfig = preprocessingConfig
			};
			preprocessorActivity.Inputs.AddRange(inputFiles);
			return preprocessorActivity.Execute();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000ACD8 File Offset: 0x00008ED8
		private IEnumerable<MinifyCssResult> MinifyCss(IEnumerable<ContentItem> inputCssItems, MinifyCssActivity minifier, FileHasherActivity imageHasher, bool writeSpriteLogFile, Dictionary<string, IDictionary<string, IDictionary<string, string>>> mergedResources)
		{
			List<MinifyCssResult> list = new List<MinifyCssResult>();
			foreach (ContentItem contentItem in inputCssItems)
			{
				string relativeContentPath = contentItem.RelativeContentPath;
				string relativeContentPath2 = contentItem.RelativeContentPath;
				IEnumerable<string> values = from p in contentItem.ResourcePivotKeys
				select p.ToString();
				string text = string.Join(".", values);
				this.context.Log.Information("Css Minify start: {0} : {1}".InvariantFormat(new object[]
				{
					relativeContentPath2,
					text
				}), MessageImportance.Normal);
				minifier.SourceFile = relativeContentPath;
				minifier.MergedResources = mergedResources;
				minifier.DestinationFile = relativeContentPath2;
				if (writeSpriteLogFile)
				{
					ResourcePivotKey resourcePivotKey = contentItem.ResourcePivotKeys.FirstOrDefault<ResourcePivotKey>();
					minifier.ImageSpritingLogPath = Path.Combine(this.context.Configuration.ReportPath, relativeContentPath2 + ((resourcePivotKey != null) ? ("." + resourcePivotKey.ToString("{0}.{1}")) : string.Empty) + ".spritingLog.xml");
				}
				try
				{
					MinifyCssResult item = minifier.Process(contentItem, imageHasher);
					list.Add(item);
				}
				catch (Exception ex)
				{
					list.Add(null);
					this.HandleCssAggregateException(ex, relativeContentPath, contentItem);
				}
			}
			return list;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000AE5C File Offset: 0x0000905C
		private void HandleCssAggregateException(Exception ex, string sourceFile, ContentItem inputFile)
		{
			AggregateException ex2;
			if ((ex2 = (ex as AggregateException)) != null || (ex.InnerException != null && (ex2 = (ex.InnerException as AggregateException)) != null))
			{
				List<RecognitionException> list = new List<RecognitionException>();
				List<AggregateException> list2 = new List<AggregateException>();
				List<Exception> list3 = new List<Exception>();
				foreach (Exception ex3 in ex2.InnerExceptions)
				{
					RecognitionException ex4 = ex3 as RecognitionException;
					if (ex4 != null)
					{
						list.Add(ex4);
					}
					else
					{
						AggregateException ex5 = ex3 as AggregateException;
						if (ex5 != null)
						{
							list2.Add(ex5);
						}
						else
						{
							list3.Add(ex3);
						}
					}
				}
				IEnumerable<BuildWorkflowException> enumerable = list.CreateBuildErrors(sourceFile);
				foreach (BuildWorkflowException ex6 in enumerable)
				{
					this.HandleError(inputFile, ex6, sourceFile);
				}
				foreach (AggregateException ex7 in list2)
				{
					this.HandleCssAggregateException(ex7, sourceFile, inputFile);
				}
				using (List<Exception>.Enumerator enumerator4 = list3.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						Exception ex8 = enumerator4.Current;
						this.HandleError(inputFile, ex8, sourceFile);
					}
					return;
				}
			}
			this.HandleError(inputFile, ex, sourceFile);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000AFF8 File Offset: 0x000091F8
		private IEnumerable<ContentItem> MinifyJs(IEnumerable<ContentItem> inputFiles, JsMinificationConfig jsConfig, JSValidationConfig jsValidateConfig)
		{
			List<ContentItem> list = new List<ContentItem>();
			MinifyJSActivity minifyJSActivity = new MinifyJSActivity(this.context)
			{
				ShouldMinify = jsConfig.ShouldMinify,
				ShouldAnalyze = jsValidateConfig.ShouldAnalyze,
				AnalyzeArgs = jsValidateConfig.AnalyzeArguments
			};
			if (!string.IsNullOrWhiteSpace(jsConfig.GlobalsToIgnore))
			{
				minifyJSActivity.MinifyArgs = string.Concat(new object[]
				{
					"/global:",
					jsConfig.GlobalsToIgnore,
					' ',
					jsConfig.MinificationArugments
				});
			}
			else
			{
				minifyJSActivity.MinifyArgs = jsConfig.MinificationArugments;
			}
			foreach (ContentItem contentItem in inputFiles)
			{
				string relativeContentPath = contentItem.RelativeContentPath;
				if (!this.context.TemporaryIgnore(contentItem.ResourcePivotKeys))
				{
					LogManager log = this.context.Log;
					string format = "Js Minify start: {0}{1}";
					object[] array = new object[2];
					array[0] = relativeContentPath;
					array[1] = string.Join(string.Empty, from p in contentItem.ResourcePivotKeys
					select p.ToString());
					log.Information(format.InvariantFormat(array), MessageImportance.Normal);
					try
					{
						list.Add(minifyJSActivity.Minify(contentItem));
					}
					catch (Exception ex)
					{
						list.Add(null);
						this.HandleError(contentItem, ex, relativeContentPath);
					}
				}
			}
			return list;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000B188 File Offset: 0x00009388
		private IEnumerable<ContentItem> ApplyResources(IEnumerable<ContentItem> inputItems, Dictionary<string, IDictionary<string, IDictionary<string, string>>> mergedResource)
		{
			if (!mergedResource.Any<KeyValuePair<string, IDictionary<string, IDictionary<string, string>>>>())
			{
				return inputItems;
			}
			List<ContentItem> list = new List<ContentItem>();
			foreach (ContentItem contentItem in inputItems)
			{
				try
				{
					list.AddRange(ResourcePivotActivity.ApplyResourceKeys(contentItem, mergedResource));
				}
				catch (Exception ex)
				{
					this.HandleError(contentItem, ex, contentItem.RelativeContentPath);
					list.Add(null);
				}
			}
			return list;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000B210 File Offset: 0x00009410
		private ContentItem BundleFiles(IEnumerable<InputSpec> inputSpecs, string outputFile, PreprocessingConfig preprocessing, FileTypes fileType, bool minimalOutput)
		{
			AssemblerActivity assemblerActivity = new AssemblerActivity(this.context)
			{
				PreprocessingConfig = preprocessing,
				AddSemicolons = (fileType == FileTypes.JS),
				MinimalOutput = minimalOutput
			};
			foreach (InputSpec item in inputSpecs)
			{
				assemblerActivity.Inputs.Add(item);
			}
			assemblerActivity.OutputFile = outputFile;
			try
			{
				return assemblerActivity.Execute(ContentItemType.Value);
			}
			catch (Exception ex)
			{
				this.HandleError(null, ex, null);
			}
			return null;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000B2B8 File Offset: 0x000094B8
		private void HandleError(ContentItem contentItem, Exception ex, string file = null)
		{
			if (ex.InnerException is BuildWorkflowException)
			{
				ex = ex.InnerException;
			}
			BuildWorkflowException ex2 = ex as BuildWorkflowException;
			if (contentItem != null)
			{
				file = this.context.EnsureErrorFileOnDisk((ex2 != null) ? ex2.File : file, contentItem);
				if (ex2 != null)
				{
					ex2.File = file;
				}
			}
			if (!string.IsNullOrWhiteSpace(file) && (ex2 == null || ex2.File.IsNullOrWhitespace()))
			{
				this.context.Log.Error(null, string.Format(CultureInfo.InvariantCulture, ResourceStrings.ErrorsInFileFormat, new object[]
				{
					file
				}), file);
			}
			this.context.Log.Error(ex, ex.ToString(), null);
			AggregateException ex3 = ex as AggregateException;
			if (ex3 != null)
			{
				using (IEnumerator<Exception> enumerator = ex3.InnerExceptions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Exception ex4 = enumerator.Current;
						this.HandleError(contentItem, ex4, null);
					}
					return;
				}
			}
			if (ex.InnerException != null)
			{
				this.HandleError(contentItem, ex.InnerException, null);
			}
		}

		// Token: 0x040000BD RID: 189
		private const string ImagesDestinationDirectoryName = "i";

		// Token: 0x040000BE RID: 190
		private const string JsDestinationDirectoryName = "js";

		// Token: 0x040000BF RID: 191
		private const string CssDestinationDirectoryName = "css";

		// Token: 0x040000C0 RID: 192
		private const string ToolsTempDirectoryName = "ToolsTemp";

		// Token: 0x040000C1 RID: 193
		private const string StaticAssemblerDirectoryName = "StaticAssemblerOutput";

		// Token: 0x040000C2 RID: 194
		private const string PreprocessingTempDirectory = "PreCompiler";

		// Token: 0x040000C3 RID: 195
		private readonly string toolsTempDirectory;

		// Token: 0x040000C4 RID: 196
		private readonly string staticAssemblerDirectory;

		// Token: 0x040000C5 RID: 197
		private readonly string logDirectory;

		// Token: 0x040000C6 RID: 198
		private readonly string preprocessingTempDirectory;

		// Token: 0x040000C7 RID: 199
		private readonly string imagesLogFile;

		// Token: 0x040000C8 RID: 200
		private readonly IWebGreaseContext context;
	}
}
