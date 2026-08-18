using System;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB8 RID: 3256
	public class GetDescriptionsDataCompletedEventArgs : EventArgs
	{
		// Token: 0x060079CF RID: 31183 RVA: 0x001BF563 File Offset: 0x001BD763
		public GetDescriptionsDataCompletedEventArgs(Exception error, object userState, IFieldInfoData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.DescriptionsData = data;
			this.Error = error;
			this.State = userState;
		}

		// Token: 0x17002738 RID: 10040
		// (get) Token: 0x060079D0 RID: 31184 RVA: 0x001BF58E File Offset: 0x001BD78E
		// (set) Token: 0x060079D1 RID: 31185 RVA: 0x001BF596 File Offset: 0x001BD796
		public Exception Error { get; private set; }

		// Token: 0x17002739 RID: 10041
		// (get) Token: 0x060079D2 RID: 31186 RVA: 0x001BF59F File Offset: 0x001BD79F
		// (set) Token: 0x060079D3 RID: 31187 RVA: 0x001BF5A7 File Offset: 0x001BD7A7
		public object State { get; private set; }

		// Token: 0x1700273A RID: 10042
		// (get) Token: 0x060079D4 RID: 31188 RVA: 0x001BF5B0 File Offset: 0x001BD7B0
		// (set) Token: 0x060079D5 RID: 31189 RVA: 0x001BF5BE File Offset: 0x001BD7BE
		public IFieldInfoData DescriptionsData
		{
			get
			{
				this.RaiseExceptionIfNecessary();
				return this.descriptionsData;
			}
			private set
			{
				this.descriptionsData = value;
			}
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x001BF5C7 File Offset: 0x001BD7C7
		private void RaiseExceptionIfNecessary()
		{
			if (this.Error != null)
			{
				throw new TargetInvocationException("Cannot access data because an error has occurred.", this.Error);
			}
		}

		// Token: 0x04002155 RID: 8533
		private IFieldInfoData descriptionsData;
	}
}
