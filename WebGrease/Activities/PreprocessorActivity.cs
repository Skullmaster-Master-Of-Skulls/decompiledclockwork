using System;
using System.Collections.Generic;
using System.IO;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease.Activities
{
	// Token: 0x0200003D RID: 61
	internal sealed class PreprocessorActivity
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		internal PreprocessorActivity(IWebGreaseContext context)
		{
			this.context = context;
			this.Inputs = new List<InputSpec>();
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000C1FA File Offset: 0x0000A3FA
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000C202 File Offset: 0x0000A402
		internal List<InputSpec> Inputs { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000C20B File Offset: 0x0000A40B
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000C213 File Offset: 0x0000A413
		internal string OutputFolder { private get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000C21C File Offset: 0x0000A41C
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000C224 File Offset: 0x0000A424
		internal PreprocessingConfig PreprocessingConfig { private get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000C22D File Offset: 0x0000A42D
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000C235 File Offset: 0x0000A435
		internal bool MinimalOutput { get; set; }

		// Token: 0x060003D3 RID: 979 RVA: 0x0000C240 File Offset: 0x0000A440
		internal IEnumerable<ContentItem> Execute()
		{
			List<ContentItem> list = new List<ContentItem>();
			string sourceDirectory = this.context.Configuration.SourceDirectory;
			foreach (string text in this.Inputs.GetFiles(sourceDirectory, null, false))
			{
				FileInfo fileInfo = new FileInfo(text);
				if (!fileInfo.Exists)
				{
					throw new FileNotFoundException("Could not find the file {0} to preprocess on.");
				}
				if (!Directory.Exists(this.OutputFolder))
				{
					Directory.CreateDirectory(this.OutputFolder);
				}
				ContentItem contentItem = ContentItem.FromFile(text, text.MakeRelativeToDirectory(sourceDirectory), null, new ResourcePivotKey[0]);
				contentItem = this.context.Preprocessing.Process(contentItem, this.PreprocessingConfig, this.MinimalOutput);
				if (contentItem == null)
				{
					throw new WorkflowException("An error occurred while processing the file: " + text);
				}
				list.Add(contentItem);
			}
			return list;
		}

		// Token: 0x040000D7 RID: 215
		private readonly IWebGreaseContext context;
	}
}
