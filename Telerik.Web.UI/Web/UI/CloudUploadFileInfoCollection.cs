using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200012B RID: 299
	public class CloudUploadFileInfoCollection : CollectionBase
	{
		// Token: 0x17000450 RID: 1104
		public CloudUploadFileInfo this[int index]
		{
			get
			{
				return (CloudUploadFileInfo)base.InnerList[index];
			}
		}

		// Token: 0x17000451 RID: 1105
		public CloudUploadFileInfo this[string id]
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					CloudUploadFileInfo cloudUploadFileInfo = (CloudUploadFileInfo)obj;
					if (cloudUploadFileInfo.OriginalFileName == id)
					{
						return cloudUploadFileInfo;
					}
				}
				return null;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002D708 File Offset: 0x0002B908
		internal void Add(CloudUploadFileInfo obj)
		{
			base.InnerList.Add(obj);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002D718 File Offset: 0x0002B918
		internal CloudUploadFileInfo Remove(CloudUploadFileInfo obj)
		{
			foreach (object obj2 in base.InnerList)
			{
				CloudUploadFileInfo cloudUploadFileInfo = (CloudUploadFileInfo)obj2;
				if (cloudUploadFileInfo == obj)
				{
					base.InnerList.Remove(obj);
					break;
				}
			}
			return obj;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002D780 File Offset: 0x0002B980
		internal CloudUploadFileInfoCollection()
		{
		}
	}
}
