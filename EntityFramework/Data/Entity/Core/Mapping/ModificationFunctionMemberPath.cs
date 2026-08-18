using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C7 RID: 967
	public sealed class ModificationFunctionMemberPath : MappingItem
	{
		// Token: 0x0600235D RID: 9053 RVA: 0x000A500C File Offset: 0x000A320C
		public ModificationFunctionMemberPath(IEnumerable<EdmMember> members, AssociationSet associationSet)
		{
			Check.NotNull<IEnumerable<EdmMember>>(members, "members");
			this._members = new ReadOnlyCollection<EdmMember>(new List<EdmMember>(members));
			if (associationSet != null)
			{
				this._associationSetEnd = associationSet.AssociationSetEnds[this.Members[1].Name];
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x000A5061 File Offset: 0x000A3261
		public ReadOnlyCollection<EdmMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x000A5069 File Offset: 0x000A3269
		public AssociationSetEnd AssociationSetEnd
		{
			get
			{
				return this._associationSetEnd;
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000A5074 File Offset: 0x000A3274
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
			{
				(this.AssociationSetEnd == null) ? string.Empty : ("[" + this.AssociationSetEnd.ParentAssociationSet + "]"),
				StringUtil.BuildDelimitedList<EdmMember>(this.Members, null, ".")
			});
		}

		// Token: 0x04000C6E RID: 3182
		private readonly ReadOnlyCollection<EdmMember> _members;

		// Token: 0x04000C6F RID: 3183
		private readonly AssociationSetEnd _associationSetEnd;
	}
}
