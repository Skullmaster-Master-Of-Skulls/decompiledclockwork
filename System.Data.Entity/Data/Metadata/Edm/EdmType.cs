using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001CC RID: 460
	public abstract class EdmType : GlobalItem
	{
		// Token: 0x06001F67 RID: 8039 RVA: 0x0006E4D0 File Offset: 0x0006C6D0
		internal EdmType()
		{
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0006E4D8 File Offset: 0x0006C6D8
		internal EdmType(string name, string namespaceName, DataSpace dataSpace)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			EntityUtil.GenericCheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmType.Initialize(this, name, namespaceName, dataSpace, false, null);
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0006E503 File Offset: 0x0006C703
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x0006E50B File Offset: 0x0006C70B
		internal string CacheIdentity
		{
			get
			{
				return this._identity;
			}
			private set
			{
				this._identity = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0006E514 File Offset: 0x0006C714
		internal override string Identity
		{
			get
			{
				if (this.CacheIdentity == null)
				{
					StringBuilder stringBuilder = new StringBuilder(50);
					this.BuildIdentity(stringBuilder);
					this.CacheIdentity = stringBuilder.ToString();
				}
				return this.CacheIdentity;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0006E54A File Offset: 0x0006C74A
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x0006E552 File Offset: 0x0006C752
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
			internal set
			{
				this._name = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0006E55B File Offset: 0x0006C75B
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x0006E563 File Offset: 0x0006C763
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string NamespaceName
		{
			get
			{
				return this._namespace;
			}
			internal set
			{
				this._namespace = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x0006E56C File Offset: 0x0006C76C
		// (set) Token: 0x06001F71 RID: 8049 RVA: 0x0006E576 File Offset: 0x0006C776
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool Abstract
		{
			get
			{
				return base.GetFlag(MetadataItem.MetadataFlags.IsAbstract);
			}
			internal set
			{
				base.SetFlag(MetadataItem.MetadataFlags.IsAbstract, value);
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0006E581 File Offset: 0x0006C781
		// (set) Token: 0x06001F73 RID: 8051 RVA: 0x0006E58C File Offset: 0x0006C78C
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType BaseType
		{
			get
			{
				return this._baseType;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				for (EdmType edmType = value; edmType != null; edmType = edmType.BaseType)
				{
				}
				this._baseType = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0006E5B4 File Offset: 0x0006C7B4
		public virtual string FullName
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual Type ClrType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0006E5BC File Offset: 0x0006C7BC
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (this.CacheIdentity != null)
			{
				builder.Append(this.CacheIdentity);
				return;
			}
			builder.Append(EdmType.CreateEdmTypeIdentity(this.NamespaceName, this.Name));
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0006E5EC File Offset: 0x0006C7EC
		internal static string CreateEdmTypeIdentity(string namespaceName, string name)
		{
			string str = string.Empty;
			if (string.Empty != namespaceName)
			{
				str = namespaceName + ".";
			}
			return str + name;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0006E621 File Offset: 0x0006C821
		internal static void Initialize(EdmType edmType, string name, string namespaceName, DataSpace dataSpace, bool isAbstract, EdmType baseType)
		{
			edmType._baseType = baseType;
			edmType._name = name;
			edmType._namespace = namespaceName;
			edmType.DataSpace = dataSpace;
			edmType.Abstract = isAbstract;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0006E648 File Offset: 0x0006C848
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0006E650 File Offset: 0x0006C850
		public CollectionType GetCollectionType()
		{
			if (this._collectionType == null)
			{
				Interlocked.CompareExchange<CollectionType>(ref this._collectionType, new CollectionType(this), null);
			}
			return this._collectionType;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0006E673 File Offset: 0x0006C873
		internal virtual bool IsSubtypeOf(EdmType otherType)
		{
			return Helper.IsSubtypeOf(this, otherType);
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0006E67C File Offset: 0x0006C87C
		internal virtual bool IsBaseTypeOf(EdmType otherType)
		{
			return otherType != null && otherType.IsSubtypeOf(this);
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0006E68A File Offset: 0x0006C88A
		internal virtual bool IsAssignableFrom(EdmType otherType)
		{
			return Helper.IsAssignableFrom(this, otherType);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0006E694 File Offset: 0x0006C894
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				EdmType baseType = this.BaseType;
				if (baseType != null)
				{
					baseType.SetReadOnly();
				}
			}
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0006E6BF File Offset: 0x0006C8BF
		internal virtual IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return MetadataItem.GetGeneralFacetDescriptions();
		}

		// Token: 0x04000DEA RID: 3562
		private CollectionType _collectionType;

		// Token: 0x04000DEB RID: 3563
		private string _identity;

		// Token: 0x04000DEC RID: 3564
		private string _name;

		// Token: 0x04000DED RID: 3565
		private string _namespace;

		// Token: 0x04000DEE RID: 3566
		private EdmType _baseType;
	}
}
