using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CD RID: 1229
	[SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
	internal sealed class ClrEntityType : EntityType
	{
		// Token: 0x06002D6C RID: 11628 RVA: 0x000DBCCC File Offset: 0x000D9ECC
		internal ClrEntityType(Type type, string cspaceNamespaceName, string cspaceTypeName) : base(Check.NotNull<Type>(type, "type").Name, type.NestingNamespace() ?? string.Empty, DataSpace.OSpace)
		{
			this._type = type;
			this._cspaceNamespaceName = cspaceNamespaceName;
			this._cspaceTypeName = cspaceNamespaceName + "." + cspaceTypeName;
			base.Abstract = type.IsAbstract();
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x000DBD2B File Offset: 0x000D9F2B
		// (set) Token: 0x06002D6E RID: 11630 RVA: 0x000DBD33 File Offset: 0x000D9F33
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Func<object> Constructor
		{
			get
			{
				return this._constructor;
			}
			set
			{
				Interlocked.CompareExchange<Func<object>>(ref this._constructor, value, null);
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000DBD43 File Offset: 0x000D9F43
		internal override Type ClrType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x000DBD4B File Offset: 0x000D9F4B
		internal string CSpaceTypeName
		{
			get
			{
				return this._cspaceTypeName;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x000DBD53 File Offset: 0x000D9F53
		internal string CSpaceNamespaceName
		{
			get
			{
				return this._cspaceNamespaceName;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000DBD5B File Offset: 0x000D9F5B
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

		// Token: 0x06002D73 RID: 11635 RVA: 0x000DBD80 File Offset: 0x000D9F80
		private string BuildEntityTypeHash()
		{
			string result;
			using (SHA256 sha = MetadataHelper.CreateSHA256HashAlgorithm())
			{
				byte[] array = sha.ComputeHash(Encoding.ASCII.GetBytes(this.BuildEntityTypeDescription()));
				StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
				foreach (byte b in array)
				{
					stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000DBE10 File Offset: 0x000DA010
		private string BuildEntityTypeDescription()
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("CLR:").Append(this.ClrType.FullName);
			stringBuilder.Append("Conceptual:").Append(this.CSpaceTypeName);
			SortedSet<string> sortedSet = new SortedSet<string>();
			foreach (NavigationProperty navigationProperty in base.NavigationProperties)
			{
				sortedSet.Add(string.Concat(new object[]
				{
					navigationProperty.Name,
					"*",
					navigationProperty.FromEndMember.Name,
					"*",
					navigationProperty.FromEndMember.RelationshipMultiplicity,
					"*",
					navigationProperty.ToEndMember.Name,
					"*",
					navigationProperty.ToEndMember.RelationshipMultiplicity,
					"*"
				}));
			}
			stringBuilder.Append("NavProps:");
			foreach (string value in sortedSet)
			{
				stringBuilder.Append(value);
			}
			SortedSet<string> sortedSet2 = new SortedSet<string>();
			foreach (string item in this.KeyMemberNames)
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

		// Token: 0x040010A7 RID: 4263
		private readonly Type _type;

		// Token: 0x040010A8 RID: 4264
		private Func<object> _constructor;

		// Token: 0x040010A9 RID: 4265
		private readonly string _cspaceTypeName;

		// Token: 0x040010AA RID: 4266
		private readonly string _cspaceNamespaceName;

		// Token: 0x040010AB RID: 4267
		private string _hash;
	}
}
