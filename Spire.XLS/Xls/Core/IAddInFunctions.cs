using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001F5 RID: 501
	public interface IAddInFunctions : IExcelApplication
	{
		// Token: 0x17000A95 RID: 2709
		IAddInFunction this[int index]
		{
			get;
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06001C79 RID: 7289
		int Count { get; }

		// Token: 0x06001C7A RID: 7290
		int Add(string strFileName, string strFunctionName);

		// Token: 0x06001C7B RID: 7291
		int Add(string strFunctionName);
	}
}
