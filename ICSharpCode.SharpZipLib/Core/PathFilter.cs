using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000067 RID: 103
	public class PathFilter : IScanFilter
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x000165CC File Offset: 0x000155CC
		public PathFilter(string filter)
		{
			this.nameFilter_ = new NameFilter(filter);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000165E0 File Offset: 0x000155E0
		public virtual bool IsMatch(string name)
		{
			bool result = false;
			if (name != null)
			{
				string name2 = (name.Length > 0) ? Path.GetFullPath(name) : "";
				result = this.nameFilter_.IsMatch(name2);
			}
			return result;
		}

		// Token: 0x040002D8 RID: 728
		private NameFilter nameFilter_;
	}
}
