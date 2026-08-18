using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020002A0 RID: 672
	public class InputLanguageCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06002A26 RID: 10790 RVA: 0x000BFAF9 File Offset: 0x000BDCF9
		internal InputLanguageCollection(InputLanguage[] value)
		{
			base.InnerList.AddRange(value);
		}

		// Token: 0x170009DB RID: 2523
		public InputLanguage this[int index]
		{
			get
			{
				return (InputLanguage)base.InnerList[index];
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000BFB20 File Offset: 0x000BDD20
		public bool Contains(InputLanguage value)
		{
			return base.InnerList.Contains(value);
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x000BFB2E File Offset: 0x000BDD2E
		public void CopyTo(InputLanguage[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x000BFB3D File Offset: 0x000BDD3D
		public int IndexOf(InputLanguage value)
		{
			return base.InnerList.IndexOf(value);
		}
	}
}
