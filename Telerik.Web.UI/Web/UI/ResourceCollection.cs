using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020012F0 RID: 4848
	public class ResourceCollection : StronglyTypedStateManagedCollection<Resource>, IEnumerable<Resource>, IEnumerable
	{
		// Token: 0x0600CB9A RID: 52122 RVA: 0x002D7CD8 File Offset: 0x002D5ED8
		public Resource GetResourceByType(string type)
		{
			foreach (object obj in this)
			{
				Resource resource = (Resource)obj;
				if (resource.Type == type)
				{
					return resource;
				}
			}
			return null;
		}

		// Token: 0x0600CB9B RID: 52123 RVA: 0x002D7D3C File Offset: 0x002D5F3C
		public IList<Resource> GetResourcesByType(string type)
		{
			List<Resource> list = new List<Resource>();
			foreach (object obj in this)
			{
				Resource resource = (Resource)obj;
				if (resource.Type == type)
				{
					list.Add(resource);
				}
			}
			return list;
		}

		// Token: 0x0600CB9C RID: 52124 RVA: 0x002D7DA8 File Offset: 0x002D5FA8
		internal void ClearByType(string type)
		{
			foreach (Resource item in this.GetResourcesByType(type))
			{
				this.Remove(item);
			}
		}

		// Token: 0x0600CB9D RID: 52125 RVA: 0x002D7DF8 File Offset: 0x002D5FF8
		public Resource GetResource(string type, object key)
		{
			IEnumerable<Resource> resourcesByType = this.GetResourcesByType(type);
			foreach (Resource resource in resourcesByType)
			{
				if (key != null && resource.Key.Equals(key))
				{
					return resource;
				}
			}
			return null;
		}

		// Token: 0x0600CB9E RID: 52126 RVA: 0x002D7E5C File Offset: 0x002D605C
		internal object[] GetResourceKeysByType(string type)
		{
			ArrayList arrayList = new ArrayList();
			foreach (Resource resource in this.GetResourcesByType(type))
			{
				arrayList.Add(resource.Key);
			}
			return arrayList.ToArray();
		}

		// Token: 0x0600CB9F RID: 52127 RVA: 0x002D7EBC File Offset: 0x002D60BC
		internal string[] GetResourceTextByType(string type)
		{
			List<string> list = new List<string>();
			foreach (Resource resource in this.GetResourcesByType(type))
			{
				list.Add(resource.Text);
			}
			return list.ToArray();
		}

		// Token: 0x0600CBA0 RID: 52128 RVA: 0x002D7F1C File Offset: 0x002D611C
		internal string[] GetResourceTypes()
		{
			List<string> list = new List<string>();
			foreach (object obj in this)
			{
				Resource resource = (Resource)obj;
				if (!list.Contains(resource.Type))
				{
					list.Add(resource.Type);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600CBA1 RID: 52129 RVA: 0x002D7F90 File Offset: 0x002D6190
		protected override void SetDirtyObject(object o)
		{
			((Resource)o).SetDirty();
		}

		// Token: 0x0600CBA2 RID: 52130 RVA: 0x002D80E0 File Offset: 0x002D62E0
		IEnumerator<Resource> IEnumerable<Resource>.GetEnumerator()
		{
			foreach (object obj in this)
			{
				Resource resource = (Resource)obj;
				yield return resource;
			}
			yield break;
		}
	}
}
