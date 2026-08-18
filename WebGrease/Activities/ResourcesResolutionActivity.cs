using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WebGrease.Activities
{
	// Token: 0x02000045 RID: 69
	internal sealed class ResourcesResolutionActivity
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000D33E File Offset: 0x0000B53E
		public ResourcesResolutionActivity(IWebGreaseContext context)
		{
			this.context = context;
			this.ResourceKeys = new List<string>();
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000D358 File Offset: 0x0000B558
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000D360 File Offset: 0x0000B560
		internal string SourceDirectory { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000D369 File Offset: 0x0000B569
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000D371 File Offset: 0x0000B571
		internal string ResourceGroupKey { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000D37A File Offset: 0x0000B57A
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000D382 File Offset: 0x0000B582
		internal string ApplicationDirectoryName { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000D38B File Offset: 0x0000B58B
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000D393 File Offset: 0x0000B593
		internal string SiteDirectoryName { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000D39C File Offset: 0x0000B59C
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		internal string DestinationDirectory { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000D3AD File Offset: 0x0000B5AD
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000D3B5 File Offset: 0x0000B5B5
		internal List<string> ResourceKeys { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000D3BE File Offset: 0x0000B5BE
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
		internal FileTypes FileType { get; set; }

		// Token: 0x06000417 RID: 1047 RVA: 0x0000D470 File Offset: 0x0000B670
		internal IDictionary<string, IDictionary<string, string>> GetMergedResources()
		{
			if (!this.HasSomethingToResolve())
			{
				return ResourcesResolutionActivity.EmptyResult;
			}
			return this.context.SectionedAction(new string[]
			{
				"ResourcesResolutionActivity",
				this.FileType.ToString(),
				this.ResourceGroupKey
			}).Execute<IDictionary<string, IDictionary<string, string>>>(delegate()
			{
				IDictionary<string, IDictionary<string, string>> mergedResources;
				try
				{
					ResourcesResolver resourcesResolver = ResourcesResolver.Factory(this.context, this.SourceDirectory, this.ResourceGroupKey, this.ApplicationDirectoryName, this.SiteDirectoryName, this.ResourceKeys, this.DestinationDirectory);
					mergedResources = resourcesResolver.GetMergedResources();
				}
				catch (ResourceOverrideException ex)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "ResourcesResolutionActivity - {0} has more than one value assigned. Only one value per key name is allowed in libraries and features. Resource key overrides are allowed at the product level only.", new object[]
					{
						ex.TokenKey
					});
					throw new WorkflowException(message, ex);
				}
				catch (Exception inner)
				{
					throw new WorkflowException("ResourcesResolutionActivity - Error happened while executing the resolve resources activity", inner);
				}
				return mergedResources;
			});
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000D570 File Offset: 0x0000B770
		internal void Execute()
		{
			if (!this.HasSomethingToResolve())
			{
				return;
			}
			this.context.SectionedAction(new string[]
			{
				"ResourcesResolutionActivity",
				this.FileType.ToString(),
				this.ResourceGroupKey
			}).Execute(delegate()
			{
				try
				{
					ResourcesResolver resourcesResolver = ResourcesResolver.Factory(this.context, this.SourceDirectory, this.ResourceGroupKey, this.ApplicationDirectoryName, this.SiteDirectoryName, this.ResourceKeys, this.DestinationDirectory);
					resourcesResolver.ResolveHierarchy();
				}
				catch (ResourceOverrideException ex)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "ResourcesResolutionActivity - {0} has more than one value assigned. Only one value per key name is allowed in libraries and features. Resource key overrides are allowed at the product level only.", new object[]
					{
						ex.TokenKey
					});
					throw new WorkflowException(message, ex);
				}
				catch (Exception inner)
				{
					throw new WorkflowException("ResourcesResolutionActivity - Error happened while executing the resolve resources activity", inner);
				}
			});
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000D5CE File Offset: 0x0000B7CE
		private bool HasSomethingToResolve()
		{
			return this.ResourceKeys != null && this.ResourceKeys.Any<string>() && !string.IsNullOrWhiteSpace(this.SourceDirectory) && Directory.Exists(this.SourceDirectory);
		}

		// Token: 0x040000F1 RID: 241
		private static readonly Dictionary<string, IDictionary<string, string>> EmptyResult = new Dictionary<string, IDictionary<string, string>>();

		// Token: 0x040000F2 RID: 242
		private readonly IWebGreaseContext context;
	}
}
