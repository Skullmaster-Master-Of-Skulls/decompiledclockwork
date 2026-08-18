using System;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x02000313 RID: 787
	public class XmlMembersMapping : XmlMapping
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x000AE23C File Offset: 0x000AD23C
		internal XmlMembersMapping(TypeScope scope, ElementAccessor accessor, XmlMappingAccess access) : base(scope, accessor, access)
		{
			MembersMapping membersMapping = (MembersMapping)accessor.Mapping;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(":");
			this.mappings = new XmlMemberMapping[membersMapping.Members.Length];
			for (int i = 0; i < this.mappings.Length; i++)
			{
				if (membersMapping.Members[i].TypeDesc.Type != null)
				{
					stringBuilder.Append(XmlMapping.GenerateKey(membersMapping.Members[i].TypeDesc.Type, null, null));
					stringBuilder.Append(":");
				}
				this.mappings[i] = new XmlMemberMapping(membersMapping.Members[i]);
			}
			base.SetKeyInternal(stringBuilder.ToString());
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002534 RID: 9524 RVA: 0x000AE2F7 File Offset: 0x000AD2F7
		public string TypeName
		{
			get
			{
				return base.Accessor.Mapping.TypeName;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x000AE309 File Offset: 0x000AD309
		public string TypeNamespace
		{
			get
			{
				return base.Accessor.Mapping.Namespace;
			}
		}

		// Token: 0x17000934 RID: 2356
		public XmlMemberMapping this[int index]
		{
			get
			{
				return this.mappings[index];
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x000AE325 File Offset: 0x000AD325
		public int Count
		{
			get
			{
				return this.mappings.Length;
			}
		}

		// Token: 0x0400158D RID: 5517
		private XmlMemberMapping[] mappings;
	}
}
