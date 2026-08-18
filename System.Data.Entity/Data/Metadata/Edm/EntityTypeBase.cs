using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D2 RID: 466
	public abstract class EntityTypeBase : StructuralType
	{
		// Token: 0x06001FBA RID: 8122 RVA: 0x0006F180 File Offset: 0x0006D380
		internal EntityTypeBase(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._keyMembers = new ReadOnlyMetadataCollection<EdmMember>(new MetadataCollection<EdmMember>());
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0006F19B File Offset: 0x0006D39B
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public ReadOnlyMetadataCollection<EdmMember> KeyMembers
		{
			get
			{
				if (base.BaseType != null && ((EntityTypeBase)base.BaseType).KeyMembers.Count != 0)
				{
					return ((EntityTypeBase)base.BaseType).KeyMembers;
				}
				return this._keyMembers;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0006F1D4 File Offset: 0x0006D3D4
		internal string[] KeyMemberNames
		{
			get
			{
				if (this._keyMemberNames == null)
				{
					string[] array = new string[this.KeyMembers.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.KeyMembers[i].Name;
					}
					this._keyMemberNames = array;
				}
				return this._keyMemberNames;
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0006F22B File Offset: 0x0006D42B
		internal void AddKeyMember(EdmMember member)
		{
			EntityUtil.GenericCheckArgumentNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (!base.Members.Contains(member))
			{
				base.AddMember(member);
			}
			this._keyMembers.Source.Add(member);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0006F265 File Offset: 0x0006D465
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				this._keyMembers.Source.SetReadOnly();
				base.SetReadOnly();
			}
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0006F288 File Offset: 0x0006D488
		internal static void CheckAndAddMembers(IEnumerable<EdmMember> members, EntityType entityType)
		{
			foreach (EdmMember edmMember in members)
			{
				if (edmMember == null)
				{
					throw EntityUtil.CollectionParameterElementIsNull("members");
				}
				entityType.AddMember(edmMember);
			}
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0006F2E0 File Offset: 0x0006D4E0
		internal void CheckAndAddKeyMembers(IEnumerable<string> keyMembers)
		{
			foreach (string text in keyMembers)
			{
				if (text == null)
				{
					throw EntityUtil.CollectionParameterElementIsNull("keyMembers");
				}
				EdmMember member;
				if (!base.Members.TryGetValue(text, false, out member))
				{
					throw EntityUtil.Argument(Strings.InvalidKeyMember(text));
				}
				this.AddKeyMember(member);
			}
		}

		// Token: 0x04000E07 RID: 3591
		private readonly ReadOnlyMetadataCollection<EdmMember> _keyMembers;

		// Token: 0x04000E08 RID: 3592
		private string[] _keyMemberNames;
	}
}
