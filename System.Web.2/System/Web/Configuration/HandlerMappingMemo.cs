using System;

namespace System.Web.Configuration
{
	// Token: 0x020006EC RID: 1772
	internal class HandlerMappingMemo
	{
		// Token: 0x06005523 RID: 21795 RVA: 0x00129A84 File Offset: 0x00127C84
		internal HandlerMappingMemo(HttpHandlerAction mapping, string verb, VirtualPath path)
		{
			this._mapping = mapping;
			this._verb = verb;
			this._path = path;
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x00129AA1 File Offset: 0x00127CA1
		internal bool IsMatch(string verb, VirtualPath path)
		{
			return this._verb.Equals(verb) && this._path.Equals(path);
		}

		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x06005525 RID: 21797 RVA: 0x00129ABF File Offset: 0x00127CBF
		internal HttpHandlerAction Mapping
		{
			get
			{
				return this._mapping;
			}
		}

		// Token: 0x04002C98 RID: 11416
		private HttpHandlerAction _mapping;

		// Token: 0x04002C99 RID: 11417
		private string _verb;

		// Token: 0x04002C9A RID: 11418
		private VirtualPath _path;
	}
}
