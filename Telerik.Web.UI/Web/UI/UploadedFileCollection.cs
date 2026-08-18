using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x0200134D RID: 4941
	public sealed class UploadedFileCollection : CollectionBase
	{
		// Token: 0x17004263 RID: 16995
		public UploadedFile this[int index]
		{
			get
			{
				return (UploadedFile)base.InnerList[index];
			}
		}

		// Token: 0x17004264 RID: 16996
		public UploadedFile this[string id]
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					UploadedFile uploadedFile = (UploadedFile)obj;
					if (uploadedFile.InputFieldName == id)
					{
						return uploadedFile;
					}
				}
				return null;
			}
		}

		// Token: 0x0600CE71 RID: 52849 RVA: 0x002DEA74 File Offset: 0x002DCC74
		internal UploadedFile Add(UploadedFile obj)
		{
			base.InnerList.Add(obj);
			return obj;
		}

		// Token: 0x0600CE72 RID: 52850 RVA: 0x002DEA84 File Offset: 0x002DCC84
		internal UploadedFile Remove(UploadedFile obj)
		{
			foreach (object obj2 in base.InnerList)
			{
				UploadedFile uploadedFile = (UploadedFile)obj2;
				if (uploadedFile == obj)
				{
					base.InnerList.Remove(obj);
					break;
				}
			}
			return obj;
		}

		// Token: 0x0600CE73 RID: 52851 RVA: 0x002DEAEC File Offset: 0x002DCCEC
		internal UploadedFileCollection()
		{
		}
	}
}
