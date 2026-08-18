using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000BB RID: 187
	public class CreateLink : HtmlEditorExtenderButton
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000EA7B File Offset: 0x0000CC7B
		public override string CommandName
		{
			get
			{
				return "createLink";
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000EA82 File Offset: 0x0000CC82
		public override string Tooltip
		{
			get
			{
				return "Create Link";
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000EA8C File Offset: 0x0000CC8C
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"a",
						new string[]
						{
							"href"
						}
					}
				};
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000EABC File Offset: 0x0000CCBC
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
				dictionary.Add("href", new string[0]);
				return null;
			}
		}
	}
}
