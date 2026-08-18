using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200141A RID: 5146
	internal class Attributes
	{
		// Token: 0x0600D2DB RID: 53979 RVA: 0x002ED1E7 File Offset: 0x002EB3E7
		internal int getLength()
		{
			return this.attArray.Count;
		}

		// Token: 0x0600D2DC RID: 53980 RVA: 0x002ED1F4 File Offset: 0x002EB3F4
		internal string getQName(int index)
		{
			return ((SaxAttribute)this.attArray[index]).Name;
		}

		// Token: 0x0600D2DD RID: 53981 RVA: 0x002ED21C File Offset: 0x002EB41C
		internal string getValue(int index)
		{
			return ((SaxAttribute)this.attArray[index]).Value;
		}

		// Token: 0x0600D2DE RID: 53982 RVA: 0x002ED244 File Offset: 0x002EB444
		internal string getValue(string name)
		{
			foreach (object obj in this.attArray)
			{
				SaxAttribute saxAttribute = (SaxAttribute)obj;
				if (saxAttribute.Name.Equals(name))
				{
					return saxAttribute.Value;
				}
			}
			return null;
		}

		// Token: 0x0600D2DF RID: 53983 RVA: 0x002ED2B4 File Offset: 0x002EB4B4
		internal Attributes TrimArray()
		{
			this.attArray.TrimToSize();
			return this;
		}

		// Token: 0x0400391E RID: 14622
		internal ArrayList attArray = new ArrayList(3);
	}
}
