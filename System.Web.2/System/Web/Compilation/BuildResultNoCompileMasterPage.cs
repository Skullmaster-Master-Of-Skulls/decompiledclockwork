using System;
using System.Collections;
using System.Globalization;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200081E RID: 2078
	internal class BuildResultNoCompileMasterPage : BuildResultNoCompileUserControl
	{
		// Token: 0x06006368 RID: 25448 RVA: 0x0015C427 File Offset: 0x0015A627
		internal BuildResultNoCompileMasterPage(Type baseType, TemplateParser parser) : base(baseType, parser)
		{
			this._placeHolderList = ((MasterPageParser)parser).PlaceHolderList;
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x0015C444 File Offset: 0x0015A644
		public override object CreateInstance()
		{
			MasterPage masterPage = (MasterPage)base.CreateInstance();
			foreach (object obj in this._placeHolderList)
			{
				string text = (string)obj;
				masterPage.ContentPlaceHolders.Add(text.ToLower(CultureInfo.InvariantCulture));
			}
			return masterPage;
		}

		// Token: 0x04003384 RID: 13188
		private ICollection _placeHolderList;
	}
}
