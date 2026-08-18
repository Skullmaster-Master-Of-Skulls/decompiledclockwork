using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C9 RID: 1225
	public abstract class EntityTypeBase : StructuralType
	{
		// Token: 0x06002D3A RID: 11578 RVA: 0x000DB309 File Offset: 0x000D9509
		internal EntityTypeBase(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._keyMembers = new ReadOnlyMetadataCollection<EdmMember>(new MetadataCollection<EdmMember>());
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000DB32F File Offset: 0x000D952F
		[MetadataProperty(BuiltInTypeKind.EdmMember, true)]
		public virtual ReadOnlyMetadataCollection<EdmMember> KeyMembers
		{
			get
			{
				if (this.BaseType != null && ((EntityTypeBase)this.BaseType).KeyMembers.Count != 0)
				{
					return ((EntityTypeBase)this.BaseType).KeyMembers;
				}
				return this._keyMembers;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000DB368 File Offset: 0x000D9568
		public virtual ReadOnlyMetadataCollection<EdmProperty> KeyProperties
		{
			get
			{
				ReadOnlyMetadataCollection<EdmProperty> keyProperties = this._keyProperties;
				if (keyProperties == null)
				{
					lock (this._keyPropertiesSync)
					{
						if (this._keyProperties == null)
						{
							this.KeyMembers.SourceAccessed += this.KeyMembersSourceAccessedEventHandler;
							this._keyProperties = new ReadOnlyMetadataCollection<EdmProperty>(this.KeyMembers.Cast<EdmProperty>().ToList<EdmProperty>());
						}
						keyProperties = this._keyProperties;
					}
				}
				return keyProperties;
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x000DB3F0 File Offset: 0x000D95F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ResetKeyPropertiesCache()
		{
			if (this._keyProperties != null)
			{
				lock (this._keyPropertiesSync)
				{
					if (this._keyProperties != null)
					{
						this._keyProperties = null;
						this.KeyMembers.SourceAccessed -= this.KeyMembersSourceAccessedEventHandler;
					}
				}
			}
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000DB458 File Offset: 0x000D9658
		private void KeyMembersSourceAccessedEventHandler(object sender, EventArgs e)
		{
			this.ResetKeyPropertiesCache();
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000DB460 File Offset: 0x000D9660
		internal virtual string[] KeyMemberNames
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

		// Token: 0x06002D40 RID: 11584 RVA: 0x000DB4B7 File Offset: 0x000D96B7
		public void AddKeyMember(EdmMember member)
		{
			Check.NotNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (!base.Members.Contains(member))
			{
				base.AddMember(member);
			}
			this._keyMembers.Source.Add(member);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000DB4F1 File Offset: 0x000D96F1
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				this._keyMembers.Source.SetReadOnly();
				base.SetReadOnly();
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000DB514 File Offset: 0x000D9714
		internal static void CheckAndAddMembers(IEnumerable<EdmMember> members, EntityType entityType)
		{
			foreach (EdmMember edmMember in members)
			{
				if (edmMember == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("members"));
				}
				entityType.AddMember(edmMember);
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000DB570 File Offset: 0x000D9770
		internal void CheckAndAddKeyMembers(IEnumerable<string> keyMembers)
		{
			foreach (string text in keyMembers)
			{
				if (text == null)
				{
					throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("keyMembers"));
				}
				EdmMember member;
				if (!base.Members.TryGetValue(text, false, out member))
				{
					throw new ArgumentException(Strings.InvalidKeyMember(text));
				}
				this.AddKeyMember(member);
			}
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000DB5E8 File Offset: 0x000D97E8
		public override void RemoveMember(EdmMember member)
		{
			Check.NotNull<EdmMember>(member, "member");
			Util.ThrowIfReadOnly(this);
			if (this._keyMembers.Contains(member))
			{
				this._keyMembers.Source.Remove(member);
			}
			base.RemoveMember(member);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000DB623 File Offset: 0x000D9823
		internal override void NotifyItemIdentityChanged(EdmMember item, string initialIdentity)
		{
			base.NotifyItemIdentityChanged(item, initialIdentity);
			this._keyMembers.Source.HandleIdentityChange(item, initialIdentity);
		}

		// Token: 0x04001097 RID: 4247
		private readonly ReadOnlyMetadataCollection<EdmMember> _keyMembers;

		// Token: 0x04001098 RID: 4248
		private readonly object _keyPropertiesSync = new object();

		// Token: 0x04001099 RID: 4249
		private ReadOnlyMetadataCollection<EdmProperty> _keyProperties;

		// Token: 0x0400109A RID: 4250
		private string[] _keyMemberNames;
	}
}
