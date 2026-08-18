using System;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Activities;
using WebGrease.Configuration;
using WebGrease.Css;

namespace WebGrease
{
	// Token: 0x02000199 RID: 409
	public class CssMinifier
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x0007A568 File Offset: 0x00078768
		public CssMinifier(IWebGreaseContext context)
		{
			this.CssActivity = new MinifyCssActivity(context)
			{
				ShouldMinify = true,
				ShouldOptimize = true,
				ShouldValidateForLowerCase = false,
				ShouldExcludeProperties = false,
				ShouldAssembleBackgroundImages = false
			};
			this.ShouldMinify = true;
			this.Errors = new List<string>();
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0007A5BE File Offset: 0x000787BE
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x0007A5C6 File Offset: 0x000787C6
		public List<string> Errors { get; private set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0007A5CF File Offset: 0x000787CF
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x0007A5D7 File Offset: 0x000787D7
		public bool ShouldMinify { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0007A5E0 File Offset: 0x000787E0
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x0007A5E8 File Offset: 0x000787E8
		private MinifyCssActivity CssActivity { get; set; }

		// Token: 0x0600150F RID: 5391 RVA: 0x0007A5F4 File Offset: 0x000787F4
		public string Minify(string cssContent)
		{
			this.CssActivity.ShouldMinify = this.ShouldMinify;
			MinifyCssResult minifyCssResult = null;
			Exception ex = null;
			try
			{
				minifyCssResult = this.CssActivity.Process(ContentItem.FromContent(cssContent, new ResourcePivotKey[0]), null);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (ex != null)
			{
				AggregateException ex3 = ex as AggregateException;
				if (ex3 != null)
				{
					this.Errors.AddRange(ex3.DedupeCSSErrors());
				}
			}
			if (minifyCssResult == null || minifyCssResult.Css == null || !minifyCssResult.Css.Any<ContentItem>())
			{
				return null;
			}
			return minifyCssResult.Css.FirstOrDefault<ContentItem>().Content;
		}
	}
}
