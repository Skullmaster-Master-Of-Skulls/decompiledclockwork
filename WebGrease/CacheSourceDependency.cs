using System;
using System.IO;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x020000E7 RID: 231
	public class CacheSourceDependency
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x000463F7 File Offset: 0x000445F7
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x000463FF File Offset: 0x000445FF
		public InputSpec InputSpec { get; private set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00046408 File Offset: 0x00044608
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x00046410 File Offset: 0x00044610
		public string InputSpecHash { get; private set; }

		// Token: 0x06000F15 RID: 3861 RVA: 0x0004641C File Offset: 0x0004461C
		internal static CacheSourceDependency Create(IWebGreaseContext context, InputSpec inputSpec)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (inputSpec == null)
			{
				throw new ArgumentNullException("inputSpec");
			}
			CacheSourceDependency cacheSourceDependency = new CacheSourceDependency();
			if (Directory.Exists(inputSpec.Path))
			{
				inputSpec.Path.EnsureEndSeparator();
			}
			cacheSourceDependency.InputSpecHash = CacheSourceDependency.GetInputSpecHash(context, inputSpec);
			inputSpec.Path = inputSpec.Path.MakeRelativeToDirectory(context.Configuration.SourceDirectory);
			cacheSourceDependency.InputSpec = inputSpec;
			return cacheSourceDependency;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00046495 File Offset: 0x00044695
		internal bool HasChanged(IWebGreaseContext context)
		{
			return !this.InputSpecHash.Equals(CacheSourceDependency.GetInputSpecHash(context, this.InputSpec), StringComparison.Ordinal);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000464D4 File Offset: 0x000446D4
		private static string GetInputSpecHash(IWebGreaseContext context, InputSpec inputSpec)
		{
			return inputSpec.GetFiles(context.Configuration.SourceDirectory, null, false).ToDictionary((string f) => f.MakeRelativeToDirectory(context.Configuration.SourceDirectory), new Func<string, string>(context.GetFileHash)).ToJson(false);
		}
	}
}
