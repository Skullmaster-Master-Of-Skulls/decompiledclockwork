using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D1 RID: 465
	internal sealed class ClrEntityType : EntityType
	{
		// Token: 0x06001FB1 RID: 8113 RVA: 0x0006ED88 File Offset: 0x0006CF88
		internal ClrEntityType(Type type, string cspaceNamespaceName, string cspaceTypeName) : base(EntityUtil.GenericCheckArgumentNull<Type>(type, "type").Name, type.Namespace ?? string.Empty, DataSpace.OSpace)
		{
			this._type = type.TypeHandle;
			this._cspaceNamespaceName = cspaceNamespaceName;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = type.IsAbstract;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0006EDEC File Offset: 0x0006CFEC
		// (set) Token: 0x06001FB3 RID: 8115 RVA: 0x0006EDF4 File Offset: 0x0006CFF4
		internal Delegate Constructor
		{
			get
			{
				return this._constructor;
			}
			set
			{
				Interlocked.CompareExchange<Delegate>(ref this._constructor, value, null);
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0006EE04 File Offset: 0x0006D004
		internal override Type ClrType
		{
			get
			{
				return Type.GetTypeFromHandle(this._type);
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0006EE11 File Offset: 0x0006D011
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0006EE19 File Offset: 0x0006D019
		internal string CSpaceNamespaceName
		{
			get
			{
				return this._cspaceNamespaceName;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0006EE21 File Offset: 0x0006D021
		internal string HashedDescription
		{
			get
			{
				if (this._hash == null)
				{
					Interlocked.CompareExchange<string>(ref this._hash, this.BuildEntityTypeHash(), null);
				}
				return this._hash;
			}
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0006EE44 File Offset: 0x0006D044
		private string BuildEntityTypeHash()
		{
			byte[] array = MetadataHelper.CreateSHA256HashAlgorithm().ComputeHash(Encoding.ASCII.GetBytes(this.BuildEntityTypeDescription()));
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0006EEAC File Offset: 0x0006D0AC
		private string BuildEntityTypeDescription()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("CLR:").Append(this.ClrType.FullName);
			stringBuilder.Append("Conceptual:").Append(this.CSpaceTypeName);
			SortedSet<string> sortedSet = new SortedSet<string>();
			foreach (NavigationProperty navigationProperty in base.NavigationProperties)
			{
				sortedSet.Add(string.Concat(new string[]
				{
					navigationProperty.Name,
					"*",
					navigationProperty.FromEndMember.Name,
					"*",
					navigationProperty.FromEndMember.RelationshipMultiplicity.ToString(),
					"*",
					navigationProperty.ToEndMember.Name,
					"*",
					navigationProperty.ToEndMember.RelationshipMultiplicity.ToString(),
					"*"
				}));
			}
			stringBuilder.Append("NavProps:");
			foreach (string value in sortedSet)
			{
				stringBuilder.Append(value);
			}
			SortedSet<string> sortedSet2 = new SortedSet<string>();
			foreach (string item in base.KeyMemberNames)
			{
				sortedSet2.Add(item);
			}
			stringBuilder.Append("Keys:");
			foreach (string str in sortedSet2)
			{
				stringBuilder.Append(str + "*");
			}
			SortedSet<string> sortedSet3 = new SortedSet<string>();
			foreach (EdmMember edmMember in base.Members)
			{
				if (!sortedSet2.Contains(edmMember.Name))
				{
					sortedSet3.Add(edmMember.Name + "*");
				}
			}
			stringBuilder.Append("Scalars:");
			foreach (string str2 in sortedSet3)
			{
				stringBuilder.Append(str2 + "*");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000E02 RID: 3586
		private readonly RuntimeTypeHandle _type;

		// Token: 0x04000E03 RID: 3587
		private Delegate _constructor;

		// Token: 0x04000E04 RID: 3588
		private readonly string _cspaceTypeName;

		// Token: 0x04000E05 RID: 3589
		private readonly string _cspaceNamespaceName;

		// Token: 0x04000E06 RID: 3590
		private string _hash;
	}
}
