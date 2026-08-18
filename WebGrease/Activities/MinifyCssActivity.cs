using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebGrease.Common;
using WebGrease.Configuration;
using WebGrease.Css;
using WebGrease.Css.Ast;
using WebGrease.Css.Extensions;
using WebGrease.Css.ImageAssemblyAnalysis;
using WebGrease.Css.ImageAssemblyAnalysis.LogModel;
using WebGrease.Css.Visitor;
using WebGrease.Extensions;
using WebGrease.ImageAssemble;

namespace WebGrease.Activities
{
	// Token: 0x02000048 RID: 72
	internal sealed class MinifyCssActivity
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		internal MinifyCssActivity(IWebGreaseContext context)
		{
			this.context = context;
			this.HackSelectors = new HashSet<string>();
			this.BannedSelectors = new HashSet<string>();
			this.ShouldExcludeProperties = true;
			this.ShouldValidateForLowerCase = false;
			this.ShouldOptimize = true;
			this.ShouldAssembleBackgroundImages = true;
			this.ImageAssembleReferencesToIgnore = new HashSet<string>();
			this.OutputUnitFactor = 1.0;
			this.ShouldPreventOrderBasedConflict = false;
			this.ShouldMergeBasedOnCommonDeclarations = false;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000DD38 File Offset: 0x0000BF38
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000DD40 File Offset: 0x0000BF40
		internal string ImageBasePrefixToRemoveFromOutputPathInLog { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000DD49 File Offset: 0x0000BF49
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000DD51 File Offset: 0x0000BF51
		internal string ImageBasePrefixToAddToOutputPath { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000DD5A File Offset: 0x0000BF5A
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000DD62 File Offset: 0x0000BF62
		internal string OutputUnit { private get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000DD6B File Offset: 0x0000BF6B
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000DD73 File Offset: 0x0000BF73
		internal string MissingImageUrl { private get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000DD84 File Offset: 0x0000BF84
		internal double OutputUnitFactor { private get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000DD8D File Offset: 0x0000BF8D
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000DD95 File Offset: 0x0000BF95
		internal ImageType? ForcedSpritingImageType { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000DD9E File Offset: 0x0000BF9E
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000DDA6 File Offset: 0x0000BFA6
		internal bool IgnoreImagesWithNonDefaultBackgroundSize { private get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000DDAF File Offset: 0x0000BFAF
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000DDB7 File Offset: 0x0000BFB7
		internal IList<string> ImageDirectories { private get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		internal IList<string> ImageExtensions { private get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000DDD1 File Offset: 0x0000BFD1
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000DDD9 File Offset: 0x0000BFD9
		internal string SourceFile { private get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000DDE2 File Offset: 0x0000BFE2
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000DDEA File Offset: 0x0000BFEA
		internal string DestinationFile { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000DDF3 File Offset: 0x0000BFF3
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000DDFB File Offset: 0x0000BFFB
		internal bool ShouldExcludeProperties { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000DE04 File Offset: 0x0000C004
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000DE0C File Offset: 0x0000C00C
		internal bool ShouldValidateForLowerCase { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000DE15 File Offset: 0x0000C015
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000DE1D File Offset: 0x0000C01D
		internal bool ShouldOptimize { private get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000DE26 File Offset: 0x0000C026
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000DE2E File Offset: 0x0000C02E
		internal bool ShouldMergeMediaQueries { private get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000DE37 File Offset: 0x0000C037
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000DE3F File Offset: 0x0000C03F
		internal bool ShouldAssembleBackgroundImages { private get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000DE48 File Offset: 0x0000C048
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000DE50 File Offset: 0x0000C050
		internal bool ShouldMinify { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000DE59 File Offset: 0x0000C059
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000DE61 File Offset: 0x0000C061
		internal HashSet<string> HackSelectors { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000DE6A File Offset: 0x0000C06A
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000DE72 File Offset: 0x0000C072
		internal HashSet<string> BannedSelectors { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000DE7B File Offset: 0x0000C07B
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000DE83 File Offset: 0x0000C083
		internal HashSet<string> NonMergeSelectors { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000DE8C File Offset: 0x0000C08C
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000DE94 File Offset: 0x0000C094
		internal string ImageAssembleScanDestinationFile { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000DE9D File Offset: 0x0000C09D
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000DEA5 File Offset: 0x0000C0A5
		internal string ImageSpritingLogPath { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000DEAE File Offset: 0x0000C0AE
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000DEB6 File Offset: 0x0000C0B6
		internal string ImagesOutputDirectory { private get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000DEBF File Offset: 0x0000C0BF
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000DEC7 File Offset: 0x0000C0C7
		internal HashSet<string> ImageAssembleReferencesToIgnore { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000DED0 File Offset: 0x0000C0D0
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		internal int? ImageAssemblyPadding { private get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000DEE1 File Offset: 0x0000C0E1
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000DEE9 File Offset: 0x0000C0E9
		internal bool ErrorOnInvalidSprite { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000DEF2 File Offset: 0x0000C0F2
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000DEFA File Offset: 0x0000C0FA
		internal HashSet<float> Dpi { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000DF03 File Offset: 0x0000C103
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000DF0B File Offset: 0x0000C10B
		internal IDictionary<string, IDictionary<string, string>> DpiResources { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000DF14 File Offset: 0x0000C114
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000DF1C File Offset: 0x0000C11C
		internal Dictionary<string, IDictionary<string, IDictionary<string, string>>> MergedResources { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000DF25 File Offset: 0x0000C125
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000DF2D File Offset: 0x0000C12D
		internal bool ShouldPreventOrderBasedConflict { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000DF36 File Offset: 0x0000C136
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000DF3E File Offset: 0x0000C13E
		internal bool ShouldMergeBasedOnCommonDeclarations { get; set; }

		// Token: 0x0600046B RID: 1131 RVA: 0x0000E464 File Offset: 0x0000C664
		internal MinifyCssResult Process(ContentItem contentItem, FileHasherActivity imageHasher = null)
		{
			if (imageHasher != null)
			{
				this.availableSourceImages = this.context.GetAvailableFiles(this.context.Configuration.SourceDirectory, this.ImageDirectories, this.ImageExtensions, FileTypes.Image);
			}
			string content = contentItem.Content;
			BlockingCollection<ContentItem> minifiedContentItems = new BlockingCollection<ContentItem>();
			BlockingCollection<ContentItem> hashedImageContentItems = new BlockingCollection<ContentItem>();
			BlockingCollection<ContentItem> spritedImageContentItems = new BlockingCollection<ContentItem>();
			Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> usedGroupedResources = ResourcePivotActivity.GetUsedGroupedResources(content, this.MergedResources);
			HashSet<float> hashSet = this.Dpi;
			if (hashSet == null || !hashSet.Any<float>())
			{
				hashSet = new HashSet<float>(new float[]
				{
					1f
				});
			}
			MinifyCssPivot[] source = MinifyCssActivity.GetMinifyCssPivots(contentItem, hashSet, usedGroupedResources, this.DpiResources).ToArray<MinifyCssPivot>();
			MinifyCssPivot[] items = (from p in source
			where !this.context.TemporaryIgnore(p.NewContentResourcePivotKeys)
			select p).ToArray<MinifyCssPivot>();
			StyleSheetNode parsedStylesheetNode = CssParser.Parse(this.context, content, false);
			this.context.ParallelForEach<MinifyCssPivot>((MinifyCssPivot item) => new string[]
			{
				"MinifyCssActivity"
			}, items, delegate(IWebGreaseContext threadContext, MinifyCssPivot pivot, ParallelLoopState parallelLoopState)
			{
				ContentItem minifiedContentItem = null;
				ContentItem varByContentItem = ContentItem.FromContent(contentItem.Content, pivot.NewContentResourcePivotKeys);
				bool flag = threadContext.SectionedAction(new string[]
				{
					"MinifyCssActivity",
					"Process"
				}).MakeCachable(varByContentItem, this.GetVarBySettings(imageHasher, pivot.NewContentResourcePivotKeys, pivot.MergedResource), false, false).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
				{
					minifiedContentItem = cacheSection.GetCachedContentItem("MinifyCssResult", contentItem.RelativeContentPath, contentItem.AbsoluteDiskPath, pivot.NewContentResourcePivotKeys);
					hashedImageContentItems.AddRange(cacheSection.GetCachedContentItems("HashedImage", false));
					spritedImageContentItems.AddRange(cacheSection.GetCachedContentItems("HashedSpriteImage", false));
					if (minifiedContentItem == null)
					{
						this.context.Log.Error("Css minify cache result is null");
						return false;
					}
					if (spritedImageContentItems.Any((ContentItem hi) => hi == null))
					{
						this.context.Log.Error("Sprited image cache result is null");
						return false;
					}
					if (hashedImageContentItems.Any((ContentItem hi) => hi == null))
					{
						this.context.Log.Error("Hashed image cache result is null");
						return false;
					}
					return true;
				}).Execute(delegate(ICacheSection cacheSection)
				{
					try
					{
						StyleSheetNode stylesheetNode = MinifyCssActivity.ApplyResources(parsedStylesheetNode, pivot.MergedResource, threadContext) as StyleSheetNode;
						stylesheetNode = (this.ApplyValidation(stylesheetNode, threadContext) as StyleSheetNode);
						stylesheetNode = (this.ApplyOptimization(stylesheetNode, threadContext) as StyleSheetNode);
						stylesheetNode = (this.ApplySpriting(stylesheetNode, pivot.Dpi, spritedImageContentItems, threadContext) as StyleSheetNode);
						string text = threadContext.SectionedAction(new string[]
						{
							"MinifyCssActivity",
							"PrintCss"
						}).Execute<string>(delegate()
						{
							if (!this.ShouldMinify)
							{
								return stylesheetNode.PrettyPrint();
							}
							return stylesheetNode.MinifyPrint();
						});
						if (imageHasher != null)
						{
							Tuple<string, IEnumerable<ContentItem>> tuple = MinifyCssActivity.HashImages(text, imageHasher, cacheSection, threadContext, this.availableSourceImages, this.MissingImageUrl);
							text = tuple.Item1;
							hashedImageContentItems.AddRange(tuple.Item2);
						}
						minifiedContentItem = ContentItem.FromContent(text, this.DestinationFile, null, pivot.NewContentResourcePivotKeys);
						cacheSection.AddResult(minifiedContentItem, "MinifyCssResult", false);
					}
					catch (Exception ex)
					{
						this.context.Log.Error(ex, ex.ToString(), null);
						return false;
					}
					return true;
				});
				Safe.Lock(minifiedContentItems, delegate()
				{
					minifiedContentItems.Add(minifiedContentItem);
				});
				if (!flag)
				{
					this.context.Log.Error("An errror occurred while minifying '{0}' with resources '{1}'".InvariantFormat(new object[]
					{
						contentItem.RelativeContentPath,
						pivot
					}));
				}
				return flag;
			}, null);
			return new MinifyCssResult(minifiedContentItems, spritedImageContentItems.DistinctBy((ContentItem hi) => hi.RelativeContentPath).ToArray<ContentItem>(), hashedImageContentItems.DistinctBy((ContentItem hi) => hi.RelativeContentPath).ToArray<ContentItem>());
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000E618 File Offset: 0x0000C818
		internal void Execute(ContentItem contentItem = null, FileHasherActivity imageHasher = null)
		{
			if (contentItem == null)
			{
				if (string.IsNullOrWhiteSpace(this.SourceFile))
				{
					throw new ArgumentException("MinifyCssActivity - The source file cannot be null or whitespace.");
				}
				if (!File.Exists(this.SourceFile))
				{
					throw new FileNotFoundException("MinifyCssActivity - The source file cannot be found.", this.SourceFile);
				}
			}
			if (string.IsNullOrWhiteSpace(this.DestinationFile))
			{
				throw new ArgumentException("MinifyCssActivity - The destination file cannot be null or whitespace.");
			}
			if (contentItem == null)
			{
				contentItem = ContentItem.FromFile(this.SourceFile, Path.IsPathRooted(this.SourceFile) ? this.SourceFile.MakeRelativeToDirectory(this.context.Configuration.SourceDirectory) : this.SourceFile, null, new ResourcePivotKey[0]);
			}
			MinifyCssResult minifyCssResult = this.Process(contentItem, imageHasher);
			ContentItem contentItem2 = minifyCssResult.Css.FirstOrDefault<ContentItem>();
			if (contentItem2 != null)
			{
				contentItem2.WriteTo(this.DestinationFile, false);
			}
			if (minifyCssResult.SpritedImages != null && minifyCssResult.SpritedImages.Any<ContentItem>())
			{
				foreach (ContentItem contentItem3 in minifyCssResult.SpritedImages)
				{
					contentItem3.WriteToContentPath(this.context.Configuration.DestinationDirectory, false);
				}
			}
			if (minifyCssResult.HashedImages != null && minifyCssResult.HashedImages.Any<ContentItem>())
			{
				foreach (ContentItem contentItem4 in minifyCssResult.HashedImages)
				{
					contentItem4.WriteToRelativeHashedPath(this.context.Configuration.DestinationDirectory, false);
				}
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		private static Tuple<string, IEnumerable<ContentItem>> HashImages(string cssContent, FileHasherActivity imageHasher, ICacheSection cacheSection, IWebGreaseContext threadContext, IDictionary<string, string> sourceImages, string missingImage)
		{
			return threadContext.SectionedAction(new string[]
			{
				"MinifyCssActivity",
				"ImageHash"
			}).Execute<Tuple<string, IEnumerable<ContentItem>>>(delegate()
			{
				HashSet<string> contentImagesToHash = new HashSet<string>();
				List<ContentItem> hashedContentItems = new List<ContentItem>();
				Dictionary<string, string> hashedImages = new Dictionary<string, string>();
				cssContent = MinifyCssActivity.UrlHashRegexPattern.Replace(cssContent, delegate(Match match)
				{
					string value = match.Groups["url"].Value;
					string value2 = match.Groups["extra"].Value;
					if (ResourcesResolver.LocalizationResourceKeyRegex.IsMatch(value))
					{
						return match.Value;
					}
					string text = value.NormalizeUrl();
					string text2 = sourceImages.TryGetValue(text);
					if (text2 == null && !string.IsNullOrWhiteSpace(missingImage))
					{
						text2 = sourceImages.TryGetValue(missingImage);
					}
					if (text2 == null)
					{
						throw new BuildWorkflowException("Could not find a matching source image for url: {0}".InvariantFormat(new object[]
						{
							value
						}));
					}
					if (contentImagesToHash.Add(text))
					{
						ContentItem contentItem = ContentItem.FromFile(text2, text, null, new ResourcePivotKey[0]);
						contentItem = imageHasher.Hash(contentItem);
						cacheSection.AddSourceDependency(text2);
						hashedContentItems.Add(contentItem);
						text2 = Path.Combine(imageHasher.BasePrefixToAddToOutputPath ?? Path.AltDirectorySeparatorChar.ToString(CultureInfo.InvariantCulture), contentItem.RelativeHashedContentPath.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
						hashedImages.Add(text, text2);
					}
					else
					{
						text2 = hashedImages[text];
					}
					return "url(" + text2 + value2 + ")";
				});
				return Tuple.Create<string, IEnumerable<ContentItem>>(cssContent, hashedContentItems);
			});
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000ED04 File Offset: 0x0000CF04
		private static IEnumerable<MinifyCssPivot> GetMinifyCssPivots(ContentItem contentItem, IEnumerable<float> dpiValues, Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> mergedResources, IDictionary<string, IDictionary<string, string>> allDpiResources)
		{
			IEnumerable<ResourcePivotKey> contentResourcePivotKeys = contentItem.ResourcePivotKeys ?? ((IEnumerable<ResourcePivotKey>)new ResourcePivotKey[0]);
			IEnumerable<<>f__AnonymousType9<float, string, ResourcePivotKey, IDictionary<string, string>>> dpiPivots = dpiValues.Select(delegate(float dpi)
			{
				string text = EverythingActivity.DpiToResolutionName(dpi);
				IDictionary<string, string> dpiResources = null;
				if (allDpiResources != null)
				{
					allDpiResources.TryGetValue(text, out dpiResources);
				}
				ResourcePivotKey dpiResourcePivotKey = new ResourcePivotKey("dpi", text);
				return new
				{
					dpi = dpi,
					dpiResolutionName = text,
					dpiResourcePivotKey = dpiResourcePivotKey,
					dpiResources = dpiResources
				};
			});
			return mergedResources.SelectMany(delegate(KeyValuePair<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> mergedResourceValues)
			{
				List<IDictionary<string, string>> mergedResource = mergedResourceValues.Value.Values.ToList<IDictionary<string, string>>();
				return dpiPivots.Select(delegate(dpiPivot)
				{
					List<IDictionary<string, string>> list = mergedResource.ToList<IDictionary<string, string>>();
					if (dpiPivot.dpiResources != null)
					{
						list.Add(dpiPivot.dpiResources);
					}
					return new MinifyCssPivot(list, contentResourcePivotKeys.Concat(mergedResourceValues.Key).Concat(new ResourcePivotKey[]
					{
						dpiPivot.dpiResourcePivotKey
					}).ToArray<ResourcePivotKey>(), dpiPivot.dpi);
				});
			});
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		private static AstNode ApplyResources(AstNode stylesheetNode, IEnumerable<IDictionary<string, string>> resources, IWebGreaseContext threadContext)
		{
			if (resources.Any<IDictionary<string, string>>())
			{
				threadContext.SectionedAction(new string[]
				{
					"MinifyCssActivity",
					"ResourcesResolution"
				}).Execute(delegate()
				{
					stylesheetNode = stylesheetNode.Accept(new ResourceResolutionVisitor(resources));
				});
			}
			return stylesheetNode;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000EE10 File Offset: 0x0000D010
		private static ImageLog RestoreSpritedImagesFromCache(string mapXmlFile, ICacheSection cacheSection, BlockingCollection<ContentItem> results, string destinationDirectory, string imageAssembleScanDestinationFile)
		{
			ContentItem contentItem = cacheSection.GetCachedContentItems("SpriteLogFile", false).FirstOrDefault<ContentItem>();
			if (contentItem == null)
			{
				return null;
			}
			if (!string.IsNullOrWhiteSpace(imageAssembleScanDestinationFile))
			{
				ContentItem contentItem2 = cacheSection.GetCachedContentItems("SpriteLogFileXml", false).FirstOrDefault<ContentItem>();
				if (contentItem2 != null)
				{
					contentItem2.WriteTo(mapXmlFile, false);
				}
			}
			ImageLog result = contentItem.Content.FromJson(true);
			IEnumerable<ContentItem> cachedContentItems = cacheSection.GetCachedContentItems("HashedSpriteImage", false);
			cachedContentItems.ForEach(delegate(ContentItem sici)
			{
				sici.WriteToContentPath(destinationDirectory, false);
			});
			results.AddRange(cachedContentItems);
			return result;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000EF14 File Offset: 0x0000D114
		private static string GetRelativeSpriteCacheKey(IEnumerable<InputImage> imageReferencesToAssemble, IWebGreaseContext threadContext)
		{
			return string.Join(">", from ir in imageReferencesToAssemble
			select "{0}|{1}|{2}".InvariantFormat(new object[]
			{
				threadContext.MakeRelativeToApplicationRoot(ir.AbsoluteImagePath),
				ir.Position,
				string.Join(":", ir.DuplicateImagePaths.Select(new Func<string, string>(threadContext.MakeRelativeToApplicationRoot)))
			}));
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000F870 File Offset: 0x0000DA70
		private object GetVarBySettings(FileHasherActivity imageHasher, IEnumerable<ResourcePivotKey> resourcePivotKeys, IEnumerable<IDictionary<string, string>> dpiResources)
		{
			return new
			{
				resourcePivotKeys = resourcePivotKeys,
				dpiResources = dpiResources,
				ShouldExcludeProperties = this.ShouldExcludeProperties,
				ShouldValidateForLowerCase = this.ShouldValidateForLowerCase,
				ShouldMergeMediaQueries = this.ShouldMergeMediaQueries,
				ShouldOptimize = this.ShouldOptimize,
				ShouldAssembleBackgroundImages = this.ShouldAssembleBackgroundImages,
				ShouldMinify = this.ShouldMinify,
				ShouldPreventOrderBasedConflict = this.ShouldPreventOrderBasedConflict,
				ShouldMergeBasedOnCommonDeclarations = this.ShouldMergeBasedOnCommonDeclarations,
				IgnoreImagesWithNonDefaultBackgroundSize = this.IgnoreImagesWithNonDefaultBackgroundSize,
				HackSelectors = this.HackSelectors,
				BannedSelectors = this.BannedSelectors,
				NonMergeSelectors = this.NonMergeSelectors,
				ImageAssembleReferencesToIgnore = this.ImageAssembleReferencesToIgnore,
				OutputUnit = this.OutputUnit,
				OutputUnitFactor = this.OutputUnitFactor,
				ImageAssemblyPadding = this.ImageAssemblyPadding,
				HashImages = (imageHasher == null),
				ForcedSpritingImageType = this.ForcedSpritingImageType,
				ErrorOnInvalidSprite = this.ErrorOnInvalidSprite,
				ImageAssembleScanDestinationFile = this.ImageAssembleScanDestinationFile,
				ImageSpritingLogPath = this.ImageSpritingLogPath
			};
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000F900 File Offset: 0x0000DB00
		private AstNode ApplySpriting(AstNode stylesheetNode, float dpi, BlockingCollection<ContentItem> spritedImageContentItems, IWebGreaseContext threadContext)
		{
			if (this.ShouldAssembleBackgroundImages)
			{
				stylesheetNode = this.SpriteBackgroundImages(stylesheetNode, dpi, threadContext, spritedImageContentItems);
			}
			return stylesheetNode;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		private AstNode ApplyOptimization(AstNode stylesheetNode, IWebGreaseContext threadContext)
		{
			if (this.ShouldOptimize)
			{
				threadContext.SectionedAction(new string[]
				{
					"MinifyCssActivity",
					"Optimize"
				}).Execute(delegate()
				{
					stylesheetNode = stylesheetNode.Accept(new OptimizationVisitor
					{
						ShouldMergeMediaQueries = this.ShouldMergeMediaQueries,
						ShouldPreventOrderBasedConflict = this.ShouldPreventOrderBasedConflict,
						ShouldMergeBasedOnCommonDeclarations = this.ShouldMergeBasedOnCommonDeclarations,
						NonMergeRuleSetSelectors = this.NonMergeSelectors
					});
					stylesheetNode = stylesheetNode.Accept(new ColorOptimizationVisitor());
					stylesheetNode = stylesheetNode.Accept(new FloatOptimizationVisitor());
				});
			}
			return stylesheetNode;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000FB00 File Offset: 0x0000DD00
		private AstNode ApplyValidation(AstNode stylesheetNode, IWebGreaseContext threadContext)
		{
			threadContext.SectionedAction(new string[]
			{
				"MinifyCssActivity",
				"Validate"
			}).Execute(delegate()
			{
				if (this.ShouldExcludeProperties)
				{
					stylesheetNode = stylesheetNode.Accept(new ExcludePropertyVisitor());
				}
				if (this.ShouldValidateForLowerCase)
				{
					stylesheetNode = stylesheetNode.Accept(new ValidateLowercaseVisitor());
				}
				if (this.HackSelectors != null && this.HackSelectors.Any<string>())
				{
					stylesheetNode = stylesheetNode.Accept(new SelectorValidationOptimizationVisitor(this.HackSelectors, false, true));
				}
				if (this.BannedSelectors != null && this.BannedSelectors.Any<string>())
				{
					stylesheetNode = stylesheetNode.Accept(new SelectorValidationOptimizationVisitor(this.BannedSelectors, false, false));
				}
			});
			return stylesheetNode;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000FD74 File Offset: 0x0000DF74
		private AstNode SpriteBackgroundImages(AstNode stylesheetNode, float dpi, IWebGreaseContext threadContext, BlockingCollection<ContentItem> spritedImageContentItems)
		{
			return threadContext.SectionedAction(new string[]
			{
				"MinifyCssActivity",
				"Spriting"
			}).Execute<AstNode>(delegate()
			{
				ImageAssemblyScanVisitor imageAssemblyScanVisitor = this.ExecuteImageAssemblyScan(stylesheetNode, threadContext);
				List<ImageLog> list = new List<ImageLog>();
				int num = 0;
				foreach (ImageAssemblyScanOutput scanOutput in imageAssemblyScanVisitor.ImageAssemblyScanOutputs)
				{
					ImageLog imageLog = this.SpriteImageFromLog(scanOutput, this.ImageAssembleScanDestinationFile + ((num == 0) ? string.Empty : ("." + num)) + ".xml", imageAssemblyScanVisitor.ImageAssemblyAnalysisLog, threadContext, spritedImageContentItems);
					if (imageLog != null)
					{
						list.Add(imageLog);
						num++;
					}
				}
				stylesheetNode = this.ExecuteImageAssemblyUpdate(stylesheetNode, list, dpi);
				if (!string.IsNullOrWhiteSpace(this.ImageSpritingLogPath))
				{
					imageAssemblyScanVisitor.ImageAssemblyAnalysisLog.Save(this.ImageSpritingLogPath);
				}
				ImageAssemblyAnalysisLog imageAssemblyAnalysisLog = imageAssemblyScanVisitor.ImageAssemblyAnalysisLog;
				if (this.ErrorOnInvalidSprite && imageAssemblyAnalysisLog.FailedSprites.Any<ImageAssemblyAnalysis>())
				{
					foreach (ImageAssemblyAnalysis imageAssemblyAnalysis in imageAssemblyAnalysisLog.FailedSprites)
					{
						string failureMessage = ImageAssemblyAnalysisLog.GetFailureMessage(imageAssemblyAnalysis);
						if (!string.IsNullOrWhiteSpace(imageAssemblyAnalysis.Image))
						{
							threadContext.Log.Error("Failed to sprite image {0}\r\nReason:{1}\r\nCss:{2}".InvariantFormat(new object[]
							{
								imageAssemblyAnalysis.Image,
								failureMessage,
								imageAssemblyAnalysis.AstNode.PrettyPrint()
							}));
						}
						else
						{
							threadContext.Log.Error("Failed to sprite:{0}\r\nReason:{1}".InvariantFormat(new object[]
							{
								imageAssemblyAnalysis.Image,
								failureMessage
							}));
						}
					}
				}
				return stylesheetNode;
			});
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000FF8C File Offset: 0x0000E18C
		private ImageLog SpriteImageFromLog(ImageAssemblyScanOutput scanOutput, string mapXmlFile, ImageAssemblyAnalysisLog imageAssemblyAnalysisLog, IWebGreaseContext threadContext, BlockingCollection<ContentItem> spritedImageContentItems)
		{
			if (scanOutput == null || !scanOutput.ImageReferencesToAssemble.Any<InputImage>())
			{
				return null;
			}
			ImageLog imageLog = null;
			IList<InputImage> imageReferencesToAssemble = scanOutput.ImageReferencesToAssemble;
			if (imageReferencesToAssemble == null || imageReferencesToAssemble.Count == 0)
			{
				return null;
			}
			var varBySettings = new
			{
				imageMap = MinifyCssActivity.GetRelativeSpriteCacheKey(imageReferencesToAssemble, threadContext),
				ImageAssemblyPadding = this.ImageAssemblyPadding
			};
			if (!threadContext.SectionedAction(new string[]
			{
				"MinifyCssActivity",
				"Spriting",
				"Assembly"
			}).MakeCachable(varBySettings, false, true).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
			{
				imageLog = MinifyCssActivity.RestoreSpritedImagesFromCache(mapXmlFile, cacheSection, spritedImageContentItems, threadContext.Configuration.DestinationDirectory, this.ImageAssembleScanDestinationFile);
				return imageLog != null;
			}).Execute(delegate(ICacheSection cacheSection)
			{
				imageLog = this.CreateSpritedImages(mapXmlFile, imageAssemblyAnalysisLog, imageReferencesToAssemble, cacheSection, spritedImageContentItems, threadContext);
				return imageLog != null;
			}))
			{
				return null;
			}
			return imageLog;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00010088 File Offset: 0x0000E288
		private ImageLog CreateSpritedImages(string mapXmlFile, ImageAssemblyAnalysisLog imageAssemblyAnalysisLog, IEnumerable<InputImage> imageReferencesToAssemble, ICacheSection cacheSection, BlockingCollection<ContentItem> results, IWebGreaseContext threadContext)
		{
			if (!Directory.Exists(this.ImagesOutputDirectory))
			{
				Directory.CreateDirectory(this.ImagesOutputDirectory);
			}
			ImageMap imageMap = ImageAssembleGenerator.AssembleImages(imageReferencesToAssemble.ToSafeReadOnlyCollection<InputImage>(), SpritePackingType.Vertical, this.ImagesOutputDirectory, string.Empty, true, threadContext, this.ImageAssemblyPadding, imageAssemblyAnalysisLog, this.ForcedSpritingImageType);
			if (imageMap == null || imageMap.Document == null)
			{
				return null;
			}
			string destinationDirectory = threadContext.Configuration.DestinationDirectory;
			if (!string.IsNullOrWhiteSpace(this.ImageAssembleScanDestinationFile))
			{
				string content = imageMap.Document.ToString();
				FileHelper.WriteFile(mapXmlFile, content);
				cacheSection.AddResult(ContentItem.FromFile(mapXmlFile, null, null, new ResourcePivotKey[0]), "SpriteLogFileXml", false);
			}
			ImageLog imageLog = new ImageLog(imageMap.Document);
			cacheSection.AddResult(ContentItem.FromContent(imageLog.ToJson(true), new ResourcePivotKey[0]), "SpriteLogFile", false);
			foreach (string text in (from il in imageLog.InputImages
			select il.OutputFilePath).Distinct<string>())
			{
				ContentItem contentItem = ContentItem.FromFile(text, text.MakeRelativeToDirectory(destinationDirectory), null, new ResourcePivotKey[0]);
				results.Add(contentItem);
				cacheSection.AddResult(contentItem, "HashedSpriteImage", false);
			}
			return imageLog;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000101F0 File Offset: 0x0000E3F0
		private ImageAssemblyScanVisitor ExecuteImageAssemblyScan(AstNode stylesheetNode, IWebGreaseContext threadContext)
		{
			ImageAssemblyScanVisitor imageAssemblyScanVisitor = new ImageAssemblyScanVisitor(this.SourceFile, this.ImageAssembleReferencesToIgnore, this.IgnoreImagesWithNonDefaultBackgroundSize, this.OutputUnit, this.OutputUnitFactor, this.availableSourceImages, this.MissingImageUrl, true)
			{
				Context = threadContext
			};
			stylesheetNode.Accept(imageAssemblyScanVisitor);
			return imageAssemblyScanVisitor;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00010240 File Offset: 0x0000E440
		private AstNode ExecuteImageAssemblyUpdate(AstNode stylesheetNode, IEnumerable<ImageLog> imageLogs, float dpi)
		{
			ImageAssemblyUpdateVisitor nodeVisitor = new ImageAssemblyUpdateVisitor(this.SourceFile, imageLogs, dpi, this.OutputUnit, this.OutputUnitFactor, this.ImageBasePrefixToRemoveFromOutputPathInLog, this.ImageBasePrefixToAddToOutputPath, this.availableSourceImages, this.MissingImageUrl);
			stylesheetNode = stylesheetNode.Accept(nodeVisitor);
			return stylesheetNode;
		}

		// Token: 0x04000105 RID: 261
		private static readonly Regex UrlHashRegexPattern = new Regex("url\\((?<quote>[\"']?)(?:hash\\((?<url>[^)]*))\\)(?<extra>.*?)\\k<quote>\\)", RegexOptions.Compiled);

		// Token: 0x04000106 RID: 262
		private readonly IWebGreaseContext context;

		// Token: 0x04000107 RID: 263
		private IDictionary<string, string> availableSourceImages;
	}
}
