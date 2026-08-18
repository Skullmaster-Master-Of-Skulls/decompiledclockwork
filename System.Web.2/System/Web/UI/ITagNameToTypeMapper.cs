using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000305 RID: 773
	internal interface ITagNameToTypeMapper
	{
		// Token: 0x060023C2 RID: 9154
		Type GetControlType(string tagName, IDictionary attribs);
	}
}
