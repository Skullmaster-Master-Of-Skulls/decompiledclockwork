using System;
using System.Collections;

namespace ImportExportClassLibrary
{
	// Token: 0x02000044 RID: 68
	public class TemplateCodeCollection : CollectionBase
	{
		// Token: 0x1700004D RID: 77
		public TemplateCode this[int index]
		{
			get
			{
				return (TemplateCode)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0001C6CE File Offset: 0x0001B6CE
		public int Add(TemplateCode templateCode)
		{
			return base.List.Add(templateCode);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001C6DC File Offset: 0x0001B6DC
		public int Add(string codeName, object codeValue, Type codeDataType, params string[] alias)
		{
			TemplateCode value = new TemplateCode(codeName, codeValue, codeDataType);
			return base.List.Add(value);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001C6FE File Offset: 0x0001B6FE
		public void Insert(int index, TemplateCode templateCode)
		{
			base.List.Insert(index, templateCode);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001C70D File Offset: 0x0001B70D
		public void Remove(TemplateCode templateCode)
		{
			base.List.Remove(templateCode);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001C71B File Offset: 0x0001B71B
		public bool Contains(TemplateCode templateCode)
		{
			return base.List.Contains(templateCode);
		}
	}
}
