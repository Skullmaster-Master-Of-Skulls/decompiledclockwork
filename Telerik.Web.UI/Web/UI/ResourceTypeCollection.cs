using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020012F2 RID: 4850
	public class ResourceTypeCollection : StronglyTypedStateManagedCollection<ResourceType>, IEnumerable<ResourceType>, IEnumerable
	{
		// Token: 0x0600CBBA RID: 52154 RVA: 0x002D8367 File Offset: 0x002D6567
		protected override void SetDirtyObject(object o)
		{
			((ResourceType)o).SetDirty();
		}

		// Token: 0x0600CBBB RID: 52155 RVA: 0x002D8374 File Offset: 0x002D6574
		public ResourceType FindByName(string name)
		{
			foreach (object obj in this)
			{
				ResourceType resourceType = (ResourceType)obj;
				if (resourceType.Name == name)
				{
					return resourceType;
				}
			}
			return null;
		}

		// Token: 0x0600CBBC RID: 52156 RVA: 0x002D83D8 File Offset: 0x002D65D8
		internal ResourceType FindByForeignKey(string foreignKey)
		{
			foreach (object obj in this)
			{
				ResourceType resourceType = (ResourceType)obj;
				if (resourceType.ForeignKeyField == foreignKey)
				{
					return resourceType;
				}
			}
			return null;
		}

		// Token: 0x0600CBBD RID: 52157 RVA: 0x002D843C File Offset: 0x002D663C
		private void EnsureUniqueName(ResourceType resourceType)
		{
			if (this.FindByName(resourceType.Name) != null)
			{
				throw new InvalidOperationException("Resource types must have unique names.");
			}
		}

		// Token: 0x0600CBBE RID: 52158 RVA: 0x002D845D File Offset: 0x002D665D
		public override void Add(ResourceType item)
		{
			this.EnsureUniqueName(item);
			base.Add(item);
		}

		// Token: 0x0600CBBF RID: 52159 RVA: 0x002D846D File Offset: 0x002D666D
		public override void Insert(int index, ResourceType item)
		{
			this.EnsureUniqueName(item);
			base.Insert(index, item);
		}

		// Token: 0x0600CBC0 RID: 52160 RVA: 0x002D85C0 File Offset: 0x002D67C0
		IEnumerator<ResourceType> IEnumerable<ResourceType>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				ResourceType resourceType = (ResourceType)obj;
				yield return resourceType;
			}
			yield break;
		}
	}
}
