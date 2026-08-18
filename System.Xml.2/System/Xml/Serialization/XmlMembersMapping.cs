using System;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x0200019A RID: 410
	public class XmlMembersMapping : XmlMapping
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x000770C0 File Offset: 0x000752C0
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

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x00077181 File Offset: 0x00075381
		public string TypeName
		{
			get
			{
				return base.Accessor.Mapping.TypeName;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x00077193 File Offset: 0x00075393
		public string TypeNamespace
		{
			get
			{
				return base.Accessor.Mapping.Namespace;
			}
		}

		// Token: 0x170005F9 RID: 1529
		public XmlMemberMapping this[int index]
		{
			get
			{
				return this.mappings[index];
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000771AF File Offset: 0x000753AF
		public int Count
		{
			get
			{
				return this.mappings.Length;
			}
		}

		// Token: 0x04000C02 RID: 3074
		private XmlMemberMapping[] mappings;
	}
}
