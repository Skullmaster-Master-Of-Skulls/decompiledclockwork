using System;
using System.Configuration;
using System.Xml;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200066E RID: 1646
	internal class CodeDomConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003BA8 RID: 15272 RVA: 0x000F6E04 File Offset: 0x000F5004
		internal CodeDomConfigurationHandler()
		{
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000F6E0C File Offset: 0x000F500C
		public virtual object Create(object inheritedObject, object configContextObj, XmlNode node)
		{
			return CodeDomCompilationConfiguration.SectionHandler.CreateStatic(inheritedObject, node);
		}
	}
}
