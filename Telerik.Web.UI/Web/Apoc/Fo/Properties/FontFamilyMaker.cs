using System;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014CA RID: 5322
	internal class FontFamilyMaker : StringProperty.Maker
	{
		// Token: 0x0600D5A6 RID: 54694 RVA: 0x002F3CFA File Offset: 0x002F1EFA
		public new static PropertyMaker Maker(string propName)
		{
			return new FontFamilyMaker(propName);
		}

		// Token: 0x0600D5A7 RID: 54695 RVA: 0x002F3D02 File Offset: 0x002F1F02
		protected FontFamilyMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5A8 RID: 54696 RVA: 0x002F3D0B File Offset: 0x002F1F0B
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5A9 RID: 54697 RVA: 0x002F3D10 File Offset: 0x002F1F10
		public override Property Make(PropertyList propertyList)
		{
			PdfRendererOptions pdfRendererOptions = ApocDriver.ActiveDriver.Options as PdfRendererOptions;
			bool flag = false;
			StringProperty stringProperty = this.m_defaultProp as StringProperty;
			if (stringProperty != null)
			{
				flag = (stringProperty.GetString() != pdfRendererOptions.DefaultFontFamily);
			}
			if (this.m_defaultProp == null || flag)
			{
				string value = string.IsNullOrEmpty(pdfRendererOptions.DefaultFontFamily) ? "serif" : pdfRendererOptions.DefaultFontFamily;
				this.m_defaultProp = this.Make(propertyList, value, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A6F RID: 14959
		private Property m_defaultProp;
	}
}
