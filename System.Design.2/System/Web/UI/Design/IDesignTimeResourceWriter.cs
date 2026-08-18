using System;
using System.Resources;

namespace System.Web.UI.Design
{
	// Token: 0x02000051 RID: 81
	public interface IDesignTimeResourceWriter : IResourceWriter, IDisposable
	{
		// Token: 0x060002A0 RID: 672
		string CreateResourceKey(string resourceName, object obj);
	}
}
