using System;
using System.Reflection;

namespace System.Web.Compilation
{
	// Token: 0x02000813 RID: 2067
	internal class BuildResultCustomString : BuildResultCompiledAssembly
	{
		// Token: 0x06006314 RID: 25364 RVA: 0x0015B9AF File Offset: 0x00159BAF
		internal BuildResultCustomString()
		{
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x0015B9B7 File Offset: 0x00159BB7
		internal BuildResultCustomString(Assembly a, string customString) : base(a)
		{
			this._customString = customString;
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x0015B9C7 File Offset: 0x00159BC7
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultCustomString;
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x0015B9CA File Offset: 0x00159BCA
		internal override void GetPreservedAttributes(PreservationFileReader pfr)
		{
			base.GetPreservedAttributes(pfr);
			this._customString = pfr.GetAttribute("customString");
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x0015B9E4 File Offset: 0x00159BE4
		internal override void SetPreservedAttributes(PreservationFileWriter pfw)
		{
			base.SetPreservedAttributes(pfw);
			pfw.SetAttribute("customString", this._customString);
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x06006319 RID: 25369 RVA: 0x0015B9FE File Offset: 0x00159BFE
		internal string CustomString
		{
			get
			{
				return this._customString;
			}
		}

		// Token: 0x04003371 RID: 13169
		private string _customString;
	}
}
