using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000852 RID: 2130
	internal class PageThemeBuildProvider : BaseTemplateBuildProvider
	{
		// Token: 0x06006503 RID: 25859 RVA: 0x00162EA6 File Offset: 0x001610A6
		internal PageThemeBuildProvider(VirtualPath virtualDirPath)
		{
			this._virtualDirPath = virtualDirPath;
			base.SetVirtualPath(virtualDirPath);
		}

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x06006504 RID: 25860 RVA: 0x00162EBC File Offset: 0x001610BC
		internal virtual string AssemblyNamePrefix
		{
			get
			{
				return "App_Theme_";
			}
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x00162EC3 File Offset: 0x001610C3
		internal void AddSkinFile(VirtualPath virtualPath)
		{
			if (this._skinFileList == null)
			{
				this._skinFileList = new StringCollection();
			}
			this._skinFileList.Add(virtualPath.VirtualPathString);
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x00162EEA File Offset: 0x001610EA
		internal void AddCssFile(VirtualPath virtualPath)
		{
			if (this._cssFileList == null)
			{
				this._cssFileList = new ArrayList();
			}
			this._cssFileList.Add(virtualPath.AppRelativeVirtualPathString);
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00162F11 File Offset: 0x00161111
		protected override TemplateParser CreateParser()
		{
			if (this._cssFileList != null)
			{
				this._cssFileList.Sort();
			}
			return new PageThemeParser(this._virtualDirPath, this._skinFileList, this._cssFileList);
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x00162F3D File Offset: 0x0016113D
		internal override BaseCodeDomTreeGenerator CreateCodeDomTreeGenerator(TemplateParser parser)
		{
			return new PageThemeCodeDomTreeGenerator((PageThemeParser)parser);
		}

		// Token: 0x04003419 RID: 13337
		private VirtualPath _virtualDirPath;

		// Token: 0x0400341A RID: 13338
		private IList _skinFileList;

		// Token: 0x0400341B RID: 13339
		private ArrayList _cssFileList;
	}
}
