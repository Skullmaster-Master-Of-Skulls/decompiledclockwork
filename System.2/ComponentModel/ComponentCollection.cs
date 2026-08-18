using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200052C RID: 1324
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class ComponentCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600321F RID: 12831 RVA: 0x000E0C47 File Offset: 0x000DEE47
		public ComponentCollection(IComponent[] components)
		{
			base.InnerList.AddRange(components);
		}

		// Token: 0x17000C51 RID: 3153
		public virtual IComponent this[string name]
		{
			get
			{
				if (name != null)
				{
					IList innerList = base.InnerList;
					foreach (object obj in innerList)
					{
						IComponent component = (IComponent)obj;
						if (component != null && component.Site != null && component.Site.Name != null && string.Equals(component.Site.Name, name, StringComparison.OrdinalIgnoreCase))
						{
							return component;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17000C52 RID: 3154
		public virtual IComponent this[int index]
		{
			get
			{
				return (IComponent)base.InnerList[index];
			}
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000E0CFF File Offset: 0x000DEEFF
		public void CopyTo(IComponent[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}
	}
}
