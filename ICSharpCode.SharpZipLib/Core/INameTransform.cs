using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000008 RID: 8
	public interface INameTransform
	{
		// Token: 0x06000024 RID: 36
		string TransformFile(string name);

		// Token: 0x06000025 RID: 37
		string TransformDirectory(string name);
	}
}
