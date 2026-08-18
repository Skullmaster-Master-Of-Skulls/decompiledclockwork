using System;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C9A RID: 3226
	public sealed class PrepareDescriptionForFieldEventArgs : EventArgs
	{
		// Token: 0x06007952 RID: 31058 RVA: 0x001BE5DF File Offset: 0x001BC7DF
		internal PrepareDescriptionForFieldEventArgs(IPivotFieldInfo fieldInfo, IDescriptionBase description, DataProviderDescriptionType descriptionType)
		{
			this.FieldInfo = fieldInfo;
			this.Description = description;
			this.DescriptionType = descriptionType;
		}

		// Token: 0x1700271E RID: 10014
		// (get) Token: 0x06007953 RID: 31059 RVA: 0x001BE5FC File Offset: 0x001BC7FC
		// (set) Token: 0x06007954 RID: 31060 RVA: 0x001BE604 File Offset: 0x001BC804
		public IPivotFieldInfo FieldInfo { get; private set; }

		// Token: 0x1700271F RID: 10015
		// (get) Token: 0x06007955 RID: 31061 RVA: 0x001BE60D File Offset: 0x001BC80D
		// (set) Token: 0x06007956 RID: 31062 RVA: 0x001BE615 File Offset: 0x001BC815
		public DataProviderDescriptionType DescriptionType { get; private set; }

		// Token: 0x17002720 RID: 10016
		// (get) Token: 0x06007957 RID: 31063 RVA: 0x001BE61E File Offset: 0x001BC81E
		// (set) Token: 0x06007958 RID: 31064 RVA: 0x001BE626 File Offset: 0x001BC826
		public IDescriptionBase Description { get; set; }
	}
}
