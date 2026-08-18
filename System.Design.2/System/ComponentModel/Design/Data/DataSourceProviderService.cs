using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001FA RID: 506
	[Guid("ABE5C1F0-C96E-40c4-A22D-4A5CEC899BDC")]
	public abstract class DataSourceProviderService
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001320 RID: 4896
		public abstract bool SupportsAddNewDataSource { get; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001321 RID: 4897
		public abstract bool SupportsConfigureDataSource { get; }

		// Token: 0x06001322 RID: 4898
		public abstract DataSourceGroupCollection GetDataSources();

		// Token: 0x06001323 RID: 4899
		public abstract DataSourceGroup InvokeAddNewDataSource(IWin32Window parentWindow, FormStartPosition startPosition);

		// Token: 0x06001324 RID: 4900
		public abstract bool InvokeConfigureDataSource(IWin32Window parentWindow, FormStartPosition startPosition, DataSourceDescriptor dataSourceDescriptor);

		// Token: 0x06001325 RID: 4901
		public abstract object AddDataSourceInstance(IDesignerHost host, DataSourceDescriptor dataSourceDescriptor);

		// Token: 0x06001326 RID: 4902
		public abstract void NotifyDataSourceComponentAdded(object dsc);
	}
}
