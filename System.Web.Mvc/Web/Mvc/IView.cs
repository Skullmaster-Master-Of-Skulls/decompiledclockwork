using System;
using System.IO;

namespace System.Web.Mvc
{
	// Token: 0x02000062 RID: 98
	public interface IView
	{
		// Token: 0x06000298 RID: 664
		void Render(ViewContext viewContext, TextWriter writer);
	}
}
