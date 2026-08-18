using System;

namespace System.Web.UI
{
	// Token: 0x020002C4 RID: 708
	public class FileLevelMasterPageControlBuilder : FileLevelPageControlBuilder
	{
		// Token: 0x06001FFB RID: 8187 RVA: 0x00065ABC File Offset: 0x00063CBC
		internal override void AddContentTemplate(object obj, string templateName, ITemplate template)
		{
			MasterPage masterPage = (MasterPage)obj;
			masterPage.AddContentTemplate(templateName, template);
		}
	}
}
