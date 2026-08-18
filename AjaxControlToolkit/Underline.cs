using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000AF RID: 175
	public class Underline : HtmlEditorExtenderButton
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000E591 File Offset: 0x0000C791
		public override string CommandName
		{
			get
			{
				return "Underline";
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000E598 File Offset: 0x0000C798
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"u",
						new string[]
						{
							"style"
						}
					}
				};
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"style",
						new string[0]
					}
				};
			}
		}
	}
}
