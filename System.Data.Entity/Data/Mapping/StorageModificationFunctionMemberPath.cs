using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.Mapping
{
	// Token: 0x02000249 RID: 585
	internal sealed class StorageModificationFunctionMemberPath
	{
		// Token: 0x06002481 RID: 9345 RVA: 0x00084354 File Offset: 0x00082554
		internal StorageModificationFunctionMemberPath(IEnumerable<EdmMember> members, AssociationSet associationSetNavigation)
		{
			this.Members = new ReadOnlyCollection<EdmMember>(new List<EdmMember>(EntityUtil.CheckArgumentNull<IEnumerable<EdmMember>>(members, "members")));
			if (associationSetNavigation != null)
			{
				this.AssociationSetEnd = associationSetNavigation.AssociationSetEnds[this.Members[1].Name];
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000843A8 File Offset: 0x000825A8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
			{
				(this.AssociationSetEnd == null) ? string.Empty : ("[" + this.AssociationSetEnd.ParentAssociationSet.ToString() + "]"),
				StringUtil.BuildDelimitedList<EdmMember>(this.Members, null, ".")
			});
		}

		// Token: 0x04001035 RID: 4149
		internal readonly ReadOnlyCollection<EdmMember> Members;

		// Token: 0x04001036 RID: 4150
		internal readonly AssociationSetEnd AssociationSetEnd;
	}
}
