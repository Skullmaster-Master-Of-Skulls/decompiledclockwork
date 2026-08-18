using System;
using System.CodeDom;
using System.Collections;
using System.Data.Common;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x02000204 RID: 516
	public interface IDataEnvironment
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001357 RID: 4951
		ICollection Connections { get; }

		// Token: 0x06001358 RID: 4952
		DesignerDataConnection BuildConnection(IWin32Window owner, DesignerDataConnection initialConnection);

		// Token: 0x06001359 RID: 4953
		string BuildQuery(IWin32Window owner, DesignerDataConnection connection, QueryBuilderMode mode, string initialQueryText);

		// Token: 0x0600135A RID: 4954
		DesignerDataConnection ConfigureConnection(IWin32Window owner, DesignerDataConnection connection, string name);

		// Token: 0x0600135B RID: 4955
		IDesignerDataSchema GetConnectionSchema(DesignerDataConnection connection);

		// Token: 0x0600135C RID: 4956
		DbConnection GetDesignTimeConnection(DesignerDataConnection connection);

		// Token: 0x0600135D RID: 4957
		CodeExpression GetCodeExpression(DesignerDataConnection connection);
	}
}
