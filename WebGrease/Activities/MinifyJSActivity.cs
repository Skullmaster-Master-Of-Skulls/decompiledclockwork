using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Ajax.Utilities;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease.Activities
{
	// Token: 0x0200003B RID: 59
	internal sealed class MinifyJSActivity
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0000B3CC File Offset: 0x000095CC
		public MinifyJSActivity(IWebGreaseContext context)
		{
			this.context = context;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000B3DB File Offset: 0x000095DB
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000B3E3 File Offset: 0x000095E3
		internal string SourceFile { private get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000B3EC File Offset: 0x000095EC
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000B3F4 File Offset: 0x000095F4
		internal string DestinationFile { private get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000B3FD File Offset: 0x000095FD
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000B405 File Offset: 0x00009605
		internal string MinifyArgs { private get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000B40E File Offset: 0x0000960E
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0000B416 File Offset: 0x00009616
		internal string AnalyzeArgs { private get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000B41F File Offset: 0x0000961F
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000B427 File Offset: 0x00009627
		internal bool ShouldAnalyze { private get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000B430 File Offset: 0x00009630
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000B438 File Offset: 0x00009638
		internal bool ShouldMinify { private get; set; }

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B444 File Offset: 0x00009644
		internal void Execute(ContentItem contentItem = null)
		{
			string destinationDirectory = this.context.Configuration.DestinationDirectory;
			if (contentItem == null && string.IsNullOrWhiteSpace(this.SourceFile))
			{
				throw new ArgumentException("MinifyJSActivity - The source file cannot be null or whitespace.");
			}
			if (string.IsNullOrWhiteSpace(this.DestinationFile))
			{
				throw new ArgumentException("MinifyJSActivity - The destination file cannot be null or whitespace.");
			}
			if (contentItem == null)
			{
				contentItem = ContentItem.FromFile(this.SourceFile, Path.IsPathRooted(this.SourceFile) ? this.SourceFile.MakeRelativeToDirectory(destinationDirectory) : this.SourceFile, null, new ResourcePivotKey[0]);
			}
			ContentItem contentItem2 = this.Minify(contentItem);
			if (contentItem2 != null)
			{
				contentItem2.WriteTo(this.DestinationFile, false);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B7C4 File Offset: 0x000099C4
		internal ContentItem Minify(ContentItem sourceContentItem)
		{
			ContentItem minifiedJsContentItem = null;
			this.context.SectionedAction(new string[]
			{
				"MinifyJsActivity"
			}).MakeCachable(sourceContentItem, new
			{
				this.ShouldAnalyze,
				this.ShouldMinify,
				this.AnalyzeArgs,
				this.context.Configuration.Global.TreatWarningsAsErrors
			}, false, false).RestoreFromCacheAction(delegate(ICacheSection cacheSection)
			{
				minifiedJsContentItem = cacheSection.GetCachedContentItem("MinifiedJsResult", sourceContentItem.RelativeContentPath, null, sourceContentItem.ResourcePivotKeys);
				return minifiedJsContentItem != null;
			}).Execute(delegate(ICacheSection cacheSection)
			{
				Minifier minifier = new Minifier
				{
					FileName = this.SourceFile
				};
				SwitchParser minifierSettings = this.GetMinifierSettings(minifier);
				string text = minifier.MinifyJavaScript(sourceContentItem.Content, minifierSettings.JSSettings);
				this.HandleMinifierErrors(sourceContentItem, minifier.ErrorList);
				if (text != null)
				{
					minifiedJsContentItem = ContentItem.FromContent(text, sourceContentItem.RelativeContentPath, null, (sourceContentItem.ResourcePivotKeys == null) ? null : sourceContentItem.ResourcePivotKeys.ToArray<ResourcePivotKey>());
					cacheSection.AddResult(minifiedJsContentItem, "MinifiedJsResult", false);
				}
				return minifiedJsContentItem != null && !minifier.ErrorList.Any<ContextError>();
			});
			return minifiedJsContentItem;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B86C File Offset: 0x00009A6C
		private void HandleMinifierErrors(ContentItem contentItem, ICollection<ContextError> errorsAndWarnings)
		{
			if (errorsAndWarnings != null && errorsAndWarnings.Count > 0)
			{
				bool flag = false;
				string message;
				if (this.context.Log.HasExtendedErrorHandler)
				{
					foreach (ContextError contextError in errorsAndWarnings)
					{
						string file = this.context.EnsureErrorFileOnDisk(contextError.File, contentItem);
						flag |= (this.context.Log.TreatWarningsAsErrors || contextError.IsError);
						LogExtendedError logExtendedError = contextError.IsError ? new LogExtendedError(this.context.Log.Error) : new LogExtendedError(this.context.Log.Warning);
						logExtendedError(contextError.Subcategory, contextError.ErrorCode, contextError.HelpKeyword, file, new int?(contextError.StartLine), new int?(contextError.StartColumn), new int?(contextError.EndLine), new int?(contextError.EndColumn), contextError.Message);
					}
					message = "Error minifying the JS";
				}
				else
				{
					flag = true;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (ContextError contextError2 in errorsAndWarnings)
					{
						stringBuilder.AppendLine(contextError2.ToString());
					}
					message = stringBuilder.ToString();
				}
				if (flag)
				{
					string file2 = this.context.EnsureErrorFileOnDisk(this.SourceFile ?? contentItem.RelativeContentPath, contentItem);
					throw new BuildWorkflowException(message, "MinifyJSActivity", "WF000", null, file2, 0, 0, 0, 0, null);
				}
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000BA30 File Offset: 0x00009C30
		private SwitchParser GetMinifierSettings(Minifier minifier)
		{
			CodeSettings scriptSettings;
			if (this.ShouldMinify)
			{
				scriptSettings = new CodeSettings
				{
					TermSemicolons = true
				};
			}
			else
			{
				scriptSettings = new CodeSettings
				{
					OutputMode = OutputMode.MultipleLines,
					PreserveFunctionNames = true,
					CollapseToLiteral = false,
					LocalRenaming = LocalRenaming.KeepAll,
					ReorderScopeDeclarations = false,
					RemoveFunctionExpressionNames = false,
					RemoveUnneededCode = false,
					StripDebugStatements = false,
					EvalLiteralExpressions = false,
					TermSemicolons = true,
					KillSwitch = -1L
				};
			}
			string commandLine = this.ShouldAnalyze ? (this.AnalyzeArgs + ' ' + this.MinifyArgs) : this.MinifyArgs;
			SwitchParser switchParser = new SwitchParser(scriptSettings, null);
			switchParser.Parse(commandLine);
			minifier.WarningLevel = switchParser.WarningLevel;
			return switchParser;
		}

		// Token: 0x040000CF RID: 207
		private readonly IWebGreaseContext context;
	}
}
