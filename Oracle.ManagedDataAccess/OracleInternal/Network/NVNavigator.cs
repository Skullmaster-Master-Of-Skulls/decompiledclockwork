using System;

namespace OracleInternal.Network
{
	// Token: 0x02000158 RID: 344
	internal class NVNavigator
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x000921C0 File Offset: 0x000903C0
		internal NVNavigator(NVPair nvp)
		{
			this.m_nvp = nvp;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000921D0 File Offset: 0x000903D0
		internal static NVPair FindNVPairRecurse(NVPair nvp, string name)
		{
			if (nvp == null || string.Equals(name, nvp.Name, StringComparison.InvariantCultureIgnoreCase))
			{
				return nvp;
			}
			if (nvp.RHSType == NVPair.RHS_ATOM)
			{
				return null;
			}
			for (int i = 0; i < nvp.ListSize; i++)
			{
				NVPair nvpair = NVNavigator.FindNVPairRecurse(nvp.GetListElement(i), name);
				if (nvpair != null)
				{
					return nvpair;
				}
			}
			return null;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00092228 File Offset: 0x00090428
		internal void SetFindString(string name)
		{
			this.m_SearchName = name;
			this.m_cursor = 0;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00092238 File Offset: 0x00090438
		internal NVPair FindNVPair()
		{
			NVPair nvpair = null;
			if (this.m_SearchName == null || this.m_nvp == null || this.m_nvp.RHSType != NVPair.RHS_LIST)
			{
				return null;
			}
			while (this.m_cursor < this.m_nvp.ListSize && nvpair == null)
			{
				NVPair listElement = this.m_nvp.GetListElement(this.m_cursor);
				if (string.Equals(this.m_SearchName, listElement.Name, StringComparison.InvariantCultureIgnoreCase))
				{
					nvpair = listElement;
				}
				this.m_cursor++;
			}
			if (this.m_cursor == this.m_nvp.ListSize)
			{
				this.m_cursor = 0;
				this.m_SearchName = null;
			}
			return nvpair;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000922DC File Offset: 0x000904DC
		internal static NVPair FindNVPair(NVPair nvp, string name)
		{
			if (nvp == null || nvp.RHSType != NVPair.RHS_LIST)
			{
				return null;
			}
			for (int i = 0; i < nvp.ListSize; i++)
			{
				NVPair listElement = nvp.GetListElement(i);
				if (string.Equals(name, listElement.Name, StringComparison.InvariantCultureIgnoreCase))
				{
					return listElement;
				}
			}
			return null;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00092328 File Offset: 0x00090528
		internal static NVPair FindNVPair(NVPair nvp, string[] names)
		{
			NVPair nvpair = nvp;
			for (int i = 0; i < names.Length; i++)
			{
				nvpair = NVNavigator.FindNVPair(nvpair, names[i]);
				if (nvpair == null)
				{
					return null;
				}
			}
			return nvpair;
		}

		// Token: 0x04000F1D RID: 3869
		private string m_SearchName;

		// Token: 0x04000F1E RID: 3870
		private int m_cursor;

		// Token: 0x04000F1F RID: 3871
		private NVPair m_nvp;
	}
}
