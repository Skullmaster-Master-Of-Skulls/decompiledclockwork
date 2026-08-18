using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease.Activities
{
	// Token: 0x02000047 RID: 71
	internal sealed class AssemblerActivity
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0000D60B File Offset: 0x0000B80B
		internal AssemblerActivity(IWebGreaseContext context)
		{
			this.context = context;
			this.Inputs = new List<InputSpec>();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000D625 File Offset: 0x0000B825
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000D62D File Offset: 0x0000B82D
		internal List<InputSpec> Inputs { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000D636 File Offset: 0x0000B836
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000D63E File Offset: 0x0000B83E
		internal string OutputFile { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000D647 File Offset: 0x0000B847
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000D64F File Offset: 0x0000B84F
		internal PreprocessingConfig PreprocessingConfig { private get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000D658 File Offset: 0x0000B858
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000D660 File Offset: 0x0000B860
		internal bool AddSemicolons { private get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000D669 File Offset: 0x0000B869
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000D671 File Offset: 0x0000B871
		internal bool MinimalOutput { get; set; }

		// Token: 0x06000428 RID: 1064 RVA: 0x0000D970 File Offset: 0x0000BB70
		internal ContentItem Execute(ContentItemType resultContentItemType = ContentItemType.Path)
		{
			if (string.IsNullOrWhiteSpace(this.OutputFile))
			{
				throw new ArgumentException("AssemblerActivity - The output file path cannot be null or whitespace.");
			}
			string text = Path.GetExtension(this.OutputFile);
			if (!string.IsNullOrWhiteSpace(text))
			{
				text = text.Trim(new char[]
				{
					'.'
				});
			}
			ContentItem contentItem = null;
			this.context.SectionedAction(new string[]
			{
				"AssemblerActivity",
				text
			}).MakeCachable(new
			{
				Inputs = this.Inputs,
				PreprocessingConfig = this.PreprocessingConfig,
				AddSemicolons = this.AddSemicolons,
				output = ((resultContentItemType == ContentItemType.Path) ? this.OutputFile : null)
			}, false, false).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
			{
				ContentItem cachedContentItem = cacheSection.GetCachedContentItem("AssemblerResult");
				if (cachedContentItem == null)
				{
					return false;
				}
				contentItem = ContentItem.FromContentItem(cachedContentItem, Path.GetFileName(this.OutputFile), null);
				return true;
			}).Execute(delegate(ICacheSection cacheSection)
			{
				try
				{
					this.Inputs.ForEach(new Action<InputSpec>(this.context.Cache.CurrentCacheSection.AddSourceDependency));
					string directoryName = Path.GetDirectoryName(this.OutputFile);
					if (resultContentItemType == ContentItemType.Path && !string.IsNullOrWhiteSpace(directoryName))
					{
						Directory.CreateDirectory(directoryName);
					}
					this.endedInSemicolon = true;
					contentItem = this.Bundle(resultContentItemType, directoryName, this.OutputFile, this.context.Configuration.SourceDirectory);
					cacheSection.AddResult(contentItem, "AssemblerResult", false);
				}
				catch (Exception inner)
				{
					throw new WorkflowException("AssemblerActivity - Error happened while executing the assembler activity", inner);
				}
				return true;
			});
			return contentItem;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000DA50 File Offset: 0x0000BC50
		private ContentItem Bundle(ContentItemType targetContentItemType, string outputDirectory, string outputFile, string sourceDirectory)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = (targetContentItemType == ContentItemType.Path) ? new StreamWriter(outputFile, false, Encoding.UTF8) : new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				this.context.Log.Information("Start bundling output file: {0}".InvariantFormat(new object[]
				{
					outputFile
				}), MessageImportance.Normal);
				foreach (string filePath in this.Inputs.GetFiles(sourceDirectory, this.context.Log, true))
				{
					this.Append(textWriter, filePath, sourceDirectory, this.PreprocessingConfig);
				}
				this.context.Log.Information("End bundling output file: {0}".InvariantFormat(new object[]
				{
					outputFile
				}), MessageImportance.Normal);
			}
			if (targetContentItemType != ContentItemType.Path)
			{
				return ContentItem.FromContent(stringBuilder.ToString(), outputFile.MakeRelativeTo(outputDirectory, new char[0]), null, new ResourcePivotKey[0]);
			}
			return ContentItem.FromFile(outputFile, outputFile.MakeRelativeTo(outputDirectory, new char[0]), null, new ResourcePivotKey[0]);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000DB88 File Offset: 0x0000BD88
		private void Append(TextWriter writer, string filePath, string sourceDirectory, PreprocessingConfig preprocessingConfig = null)
		{
			writer.WriteLine();
			writer.WriteLine();
			if (this.AddSemicolons && !this.endedInSemicolon)
			{
				writer.Write(';');
			}
			string text = (Path.IsPathRooted(filePath) && !sourceDirectory.IsNullOrWhitespace()) ? filePath.MakeRelativeTo(sourceDirectory, new char[0]) : filePath;
			if (!this.MinimalOutput)
			{
				writer.WriteLine("/* {0} {1} */".InvariantFormat(new object[]
				{
					text,
					(filePath != text) ? ("(" + filePath + ")") : string.Empty
				}));
				writer.WriteLine();
			}
			ContentItem contentItem = ContentItem.FromFile(filePath, text, null, new ResourcePivotKey[0]);
			if (preprocessingConfig != null && preprocessingConfig.Enabled)
			{
				contentItem = this.context.Preprocessing.Process(contentItem, preprocessingConfig, this.MinimalOutput);
				if (contentItem == null)
				{
					throw new WorkflowException("Could not assembly the file {0} because one of the preprocessors threw an error.".InvariantFormat(new object[]
					{
						filePath
					}));
				}
			}
			string content = contentItem.Content;
			writer.Write(content);
			writer.WriteLine();
			if (this.AddSemicolons)
			{
				this.endedInSemicolon = AssemblerActivity.EndsWithSemicolon.IsMatch(content);
			}
		}

		// Token: 0x040000FD RID: 253
		private static readonly Regex EndsWithSemicolon = new Regex(";\\s*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000FE RID: 254
		private readonly IWebGreaseContext context;

		// Token: 0x040000FF RID: 255
		private bool endedInSemicolon;
	}
}
