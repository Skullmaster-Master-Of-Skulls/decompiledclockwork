using System;

namespace System.Xml.Schema
{
	// Token: 0x02000229 RID: 553
	internal class Datatype_Name : Datatype_token
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x000B72FB File Offset: 0x000B54FB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Name;
			}
		}
	}
}
