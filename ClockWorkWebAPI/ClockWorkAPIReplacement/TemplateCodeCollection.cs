using System;
using System.Collections;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200006E RID: 110
	public class TemplateCodeCollection : CollectionBase
	{
		// Token: 0x170001C5 RID: 453
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

		// Token: 0x06000575 RID: 1397 RVA: 0x00023D5C File Offset: 0x00021F5C
		public int Add(TemplateCode templateCode)
		{
			return base.List.Add(templateCode);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00023D7C File Offset: 0x00021F7C
		public int Add(string codeName, object codeValue, Type codeDataType, params string[] alias)
		{
			TemplateCode value = new TemplateCode(codeName, codeValue, codeDataType);
			return base.List.Add(value);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000365E File Offset: 0x0000185E
		public void Insert(int index, TemplateCode templateCode)
		{
			base.List.Insert(index, templateCode);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000366F File Offset: 0x0000186F
		public void Remove(TemplateCode templateCode)
		{
			base.List.Remove(templateCode);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00023DA4 File Offset: 0x00021FA4
		public bool Contains(TemplateCode templateCode)
		{
			return base.List.Contains(templateCode);
		}
	}
}
