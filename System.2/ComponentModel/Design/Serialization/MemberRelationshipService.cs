using System;
using System.Collections.Generic;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000610 RID: 1552
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class MemberRelationshipService
	{
		// Token: 0x17000D9A RID: 3482
		public MemberRelationship this[MemberRelationship source]
		{
			get
			{
				if (source.Owner == null)
				{
					throw new ArgumentNullException("Owner");
				}
				if (source.Member == null)
				{
					throw new ArgumentNullException("Member");
				}
				return this.GetRelationship(source);
			}
			set
			{
				if (source.Owner == null)
				{
					throw new ArgumentNullException("Owner");
				}
				if (source.Member == null)
				{
					throw new ArgumentNullException("Member");
				}
				this.SetRelationship(source, value);
			}
		}

		// Token: 0x17000D9B RID: 3483
		public MemberRelationship this[object sourceOwner, MemberDescriptor sourceMember]
		{
			get
			{
				if (sourceOwner == null)
				{
					throw new ArgumentNullException("sourceOwner");
				}
				if (sourceMember == null)
				{
					throw new ArgumentNullException("sourceMember");
				}
				return this.GetRelationship(new MemberRelationship(sourceOwner, sourceMember));
			}
			set
			{
				if (sourceOwner == null)
				{
					throw new ArgumentNullException("sourceOwner");
				}
				if (sourceMember == null)
				{
					throw new ArgumentNullException("sourceMember");
				}
				this.SetRelationship(new MemberRelationship(sourceOwner, sourceMember), value);
			}
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000F22F4 File Offset: 0x000F04F4
		protected virtual MemberRelationship GetRelationship(MemberRelationship source)
		{
			MemberRelationshipService.RelationshipEntry relationshipEntry;
			if (this._relationships != null && this._relationships.TryGetValue(new MemberRelationshipService.RelationshipEntry(source), out relationshipEntry) && relationshipEntry.Owner.IsAlive)
			{
				return new MemberRelationship(relationshipEntry.Owner.Target, relationshipEntry.Member);
			}
			return MemberRelationship.Empty;
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000F2348 File Offset: 0x000F0548
		protected virtual void SetRelationship(MemberRelationship source, MemberRelationship relationship)
		{
			if (!relationship.IsEmpty && !this.SupportsRelationship(source, relationship))
			{
				string text = TypeDescriptor.GetComponentName(source.Owner);
				string text2 = TypeDescriptor.GetComponentName(relationship.Owner);
				if (text == null)
				{
					text = source.Owner.ToString();
				}
				if (text2 == null)
				{
					text2 = relationship.Owner.ToString();
				}
				throw new ArgumentException(SR.GetString("MemberRelationshipService_RelationshipNotSupported", new object[]
				{
					text,
					source.Member.Name,
					text2,
					relationship.Member.Name
				}));
			}
			if (this._relationships == null)
			{
				this._relationships = new Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry>();
			}
			this._relationships[new MemberRelationshipService.RelationshipEntry(source)] = new MemberRelationshipService.RelationshipEntry(relationship);
		}

		// Token: 0x060038E1 RID: 14561
		public abstract bool SupportsRelationship(MemberRelationship source, MemberRelationship relationship);

		// Token: 0x04002B7F RID: 11135
		private Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry> _relationships = new Dictionary<MemberRelationshipService.RelationshipEntry, MemberRelationshipService.RelationshipEntry>();

		// Token: 0x020008B2 RID: 2226
		private struct RelationshipEntry
		{
			// Token: 0x06004629 RID: 17961 RVA: 0x00124E5B File Offset: 0x0012305B
			internal RelationshipEntry(MemberRelationship rel)
			{
				this.Owner = new WeakReference(rel.Owner);
				this.Member = rel.Member;
				this.hashCode = ((rel.Owner == null) ? 0 : rel.Owner.GetHashCode());
			}

			// Token: 0x0600462A RID: 17962 RVA: 0x00124E9C File Offset: 0x0012309C
			public override bool Equals(object o)
			{
				if (o is MemberRelationshipService.RelationshipEntry)
				{
					MemberRelationshipService.RelationshipEntry re = (MemberRelationshipService.RelationshipEntry)o;
					return this == re;
				}
				return false;
			}

			// Token: 0x0600462B RID: 17963 RVA: 0x00124EC8 File Offset: 0x001230C8
			public static bool operator ==(MemberRelationshipService.RelationshipEntry re1, MemberRelationshipService.RelationshipEntry re2)
			{
				object obj = re1.Owner.IsAlive ? re1.Owner.Target : null;
				object obj2 = re2.Owner.IsAlive ? re2.Owner.Target : null;
				return obj == obj2 && re1.Member.Equals(re2.Member);
			}

			// Token: 0x0600462C RID: 17964 RVA: 0x00124F24 File Offset: 0x00123124
			public static bool operator !=(MemberRelationshipService.RelationshipEntry re1, MemberRelationshipService.RelationshipEntry re2)
			{
				return !(re1 == re2);
			}

			// Token: 0x0600462D RID: 17965 RVA: 0x00124F30 File Offset: 0x00123130
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x04003B5F RID: 15199
			internal WeakReference Owner;

			// Token: 0x04003B60 RID: 15200
			internal MemberDescriptor Member;

			// Token: 0x04003B61 RID: 15201
			private int hashCode;
		}
	}
}
