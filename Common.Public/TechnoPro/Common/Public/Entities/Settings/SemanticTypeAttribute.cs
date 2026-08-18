using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D7 RID: 471
	[Serializable]
	public class SemanticTypeAttribute : Attribute
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000158F6 File Offset: 0x00013AF6
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x000158FE File Offset: 0x00013AFE
		public Type SystemType { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00015907 File Offset: 0x00013B07
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0001590F File Offset: 0x00013B0F
		public string WinFormsEditorControlClass { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00015918 File Offset: 0x00013B18
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00015920 File Offset: 0x00013B20
		public string WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00015929 File Offset: 0x00013B29
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00015931 File Offset: 0x00013B31
		public bool IsFullScreenEditor { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0001593C File Offset: 0x00013B3C
		public IDictionary<string, string> WinFormsEditorControlAdditionalArguments
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs);
				IDictionary<string, string> result;
				if (flag)
				{
					result = null;
				}
				else
				{
					string[] array = this.WinFormsEditorControlAdditionalArguments_NameEqualsValueCommaSeparatedPairs.Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					foreach (string text in array)
					{
						int num = text.IndexOf('=');
						bool flag2 = num > 0;
						if (flag2)
						{
							dictionary.Add(text.Substring(0, num), text.Substring(num + 1));
						}
					}
					result = dictionary;
				}
				return result;
			}
		}
	}
}
