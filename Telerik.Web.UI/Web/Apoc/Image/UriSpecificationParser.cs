using System;

namespace Telerik.Web.Apoc.Image
{
	// Token: 0x020015D5 RID: 5589
	internal class UriSpecificationParser
	{
		// Token: 0x0600DA04 RID: 55812 RVA: 0x002FC8FD File Offset: 0x002FAAFD
		internal UriSpecificationParser(string input)
		{
			this.uri = this.ParseUri(input);
		}

		// Token: 0x17004308 RID: 17160
		// (get) Token: 0x0600DA05 RID: 55813 RVA: 0x002FC912 File Offset: 0x002FAB12
		internal string Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x0600DA06 RID: 55814 RVA: 0x002FC91C File Offset: 0x002FAB1C
		internal string ParseUri(string href)
		{
			href = href.Trim();
			if (href.StartsWith("url(") && href.LastIndexOf(')') != -1)
			{
				href = href.Substring(4, href.LastIndexOf(')') - 4).Trim();
				if (href.StartsWith("'") && href.EndsWith("'"))
				{
					href = href.Substring(1, href.Length - 2);
				}
				else if (href.StartsWith("\"") && href.EndsWith("\""))
				{
					href = href.Substring(1, href.Length - 2);
				}
			}
			return href;
		}

		// Token: 0x04003C72 RID: 15474
		private string uri;
	}
}
