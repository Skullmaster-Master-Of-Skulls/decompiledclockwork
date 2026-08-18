using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000F1C RID: 3868
	public class CustomNonEmbeddedSkinsCollection : CollectionBase
	{
		// Token: 0x17002EB9 RID: 11961
		public CustomNonEmbeddedSkin this[int index]
		{
			get
			{
				return (CustomNonEmbeddedSkin)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060093BB RID: 37819 RVA: 0x002129C1 File Offset: 0x00210BC1
		public int IndexOf(CustomNonEmbeddedSkin skin)
		{
			return base.List.IndexOf(skin);
		}

		// Token: 0x060093BC RID: 37820 RVA: 0x002129CF File Offset: 0x00210BCF
		public void Add(CustomNonEmbeddedSkin skin)
		{
			base.List.Add(skin);
		}

		// Token: 0x060093BD RID: 37821 RVA: 0x002129DE File Offset: 0x00210BDE
		public bool Contains(CustomNonEmbeddedSkin skin)
		{
			return base.List.Contains(skin);
		}

		// Token: 0x060093BE RID: 37822 RVA: 0x002129EC File Offset: 0x00210BEC
		public bool Contains(string resourceName)
		{
			bool result = false;
			foreach (object obj in base.List)
			{
				CustomNonEmbeddedSkin customNonEmbeddedSkin = (CustomNonEmbeddedSkin)obj;
				if (customNonEmbeddedSkin.ResourceName == resourceName)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x060093BF RID: 37823 RVA: 0x00212A54 File Offset: 0x00210C54
		public CustomNonEmbeddedSkin GetSkinByResourceName(string resourceName)
		{
			CustomNonEmbeddedSkin result = null;
			foreach (object obj in base.List)
			{
				CustomNonEmbeddedSkin customNonEmbeddedSkin = (CustomNonEmbeddedSkin)obj;
				if (customNonEmbeddedSkin.ResourceName == resourceName)
				{
					result = customNonEmbeddedSkin;
					break;
				}
			}
			return result;
		}
	}
}
