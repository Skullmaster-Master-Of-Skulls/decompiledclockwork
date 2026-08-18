using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000153 RID: 339
	internal class StructMapping : TypeMapping, INameScope
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00067718 File Offset: 0x00065918
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x00067720 File Offset: 0x00065920
		internal StructMapping BaseMapping
		{
			get
			{
				return this.baseMapping;
			}
			set
			{
				this.baseMapping = value;
				if (!base.IsAnonymousType && this.baseMapping != null)
				{
					this.nextDerivedMapping = this.baseMapping.derivedMappings;
					this.baseMapping.derivedMappings = this;
				}
				if (value.isSequence && !this.isSequence)
				{
					this.isSequence = true;
					if (this.baseMapping.IsSequence)
					{
						for (StructMapping structMapping = this.derivedMappings; structMapping != null; structMapping = structMapping.NextDerivedMapping)
						{
							structMapping.SetSequence();
						}
					}
				}
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0006779E File Offset: 0x0006599E
		internal StructMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x000677A6 File Offset: 0x000659A6
		internal bool IsFullyInitialized
		{
			get
			{
				return this.baseMapping != null && this.Members != null;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000677BB File Offset: 0x000659BB
		internal NameTable LocalElements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new NameTable();
				}
				return this.elements;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x000677D6 File Offset: 0x000659D6
		internal NameTable LocalAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new NameTable();
				}
				return this.attributes;
			}
		}

		// Token: 0x170004EA RID: 1258
		object INameScope.this[string name, string ns]
		{
			get
			{
				object obj = this.LocalElements[name, ns];
				if (obj != null)
				{
					return obj;
				}
				if (this.baseMapping != null)
				{
					return ((INameScope)this.baseMapping)[name, ns];
				}
				return null;
			}
			set
			{
				this.LocalElements[name, ns] = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0006783B File Offset: 0x00065A3B
		internal StructMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x00067843 File Offset: 0x00065A43
		internal bool HasSimpleContent
		{
			get
			{
				return this.hasSimpleContent;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0006784C File Offset: 0x00065A4C
		internal bool HasXmlnsMember
		{
			get
			{
				for (StructMapping structMapping = this; structMapping != null; structMapping = structMapping.BaseMapping)
				{
					if (structMapping.XmlnsMember != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x00067872 File Offset: 0x00065A72
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x0006787A File Offset: 0x00065A7A
		internal MemberMapping[] Members
		{
			get
			{
				return this.members;
			}
			set
			{
				this.members = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x00067883 File Offset: 0x00065A83
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x0006788B File Offset: 0x00065A8B
		internal MemberMapping XmlnsMember
		{
			get
			{
				return this.xmlnsMember;
			}
			set
			{
				this.xmlnsMember = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x00067894 File Offset: 0x00065A94
		// (set) Token: 0x06001794 RID: 6036 RVA: 0x0006789C File Offset: 0x00065A9C
		internal bool IsOpenModel
		{
			get
			{
				return this.openModel;
			}
			set
			{
				this.openModel = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x000678A5 File Offset: 0x00065AA5
		// (set) Token: 0x06001796 RID: 6038 RVA: 0x000678C0 File Offset: 0x00065AC0
		internal CodeIdentifiers Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new CodeIdentifiers();
				}
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000678CC File Offset: 0x00065ACC
		internal MemberMapping FindDeclaringMapping(MemberMapping member, out StructMapping declaringMapping, string parent)
		{
			declaringMapping = null;
			if (this.BaseMapping != null)
			{
				MemberMapping memberMapping = this.BaseMapping.FindDeclaringMapping(member, out declaringMapping, parent);
				if (memberMapping != null)
				{
					return memberMapping;
				}
			}
			if (this.members == null)
			{
				return null;
			}
			int i = 0;
			while (i < this.members.Length)
			{
				if (this.members[i].Name == member.Name)
				{
					if (this.members[i].TypeDesc != member.TypeDesc)
					{
						throw new InvalidOperationException(Res.GetString("XmlHiddenMember", new object[]
						{
							parent,
							member.Name,
							member.TypeDesc.FullName,
							base.TypeName,
							this.members[i].Name,
							this.members[i].TypeDesc.FullName
						}));
					}
					if (!this.members[i].Match(member))
					{
						throw new InvalidOperationException(Res.GetString("XmlInvalidXmlOverride", new object[]
						{
							parent,
							member.Name,
							base.TypeName,
							this.members[i].Name
						}));
					}
					declaringMapping = this;
					return this.members[i];
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00067A08 File Offset: 0x00065C08
		internal bool Declares(MemberMapping member, string parent)
		{
			StructMapping structMapping;
			return this.FindDeclaringMapping(member, out structMapping, parent) != null;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00067A24 File Offset: 0x00065C24
		internal void SetContentModel(TextAccessor text, bool hasElements)
		{
			if (this.BaseMapping == null || this.BaseMapping.TypeDesc.IsRoot)
			{
				this.hasSimpleContent = (!hasElements && text != null && !text.Mapping.IsList);
			}
			else if (this.BaseMapping.HasSimpleContent)
			{
				if (text != null || hasElements)
				{
					throw new InvalidOperationException(Res.GetString("XmlIllegalSimpleContentExtension", new object[]
					{
						base.TypeDesc.FullName,
						this.BaseMapping.TypeDesc.FullName
					}));
				}
				this.hasSimpleContent = true;
			}
			else
			{
				this.hasSimpleContent = false;
			}
			if (!this.hasSimpleContent && text != null && !text.Mapping.TypeDesc.CanBeTextValue)
			{
				throw new InvalidOperationException(Res.GetString("XmlIllegalTypedTextAttribute", new object[]
				{
					base.TypeDesc.FullName,
					text.Name,
					text.Mapping.TypeDesc.FullName
				}));
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x00067B23 File Offset: 0x00065D23
		internal bool HasElements
		{
			get
			{
				return this.elements != null && this.elements.Values.Count > 0;
			}
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00067B44 File Offset: 0x00065D44
		internal bool HasExplicitSequence()
		{
			if (this.members != null)
			{
				for (int i = 0; i < this.members.Length; i++)
				{
					if (this.members[i].IsParticle && this.members[i].IsSequence)
					{
						return true;
					}
				}
			}
			return this.baseMapping != null && this.baseMapping.HasExplicitSequence();
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00067BA4 File Offset: 0x00065DA4
		internal void SetSequence()
		{
			if (base.TypeDesc.IsRoot)
			{
				return;
			}
			StructMapping structMapping = this;
			while (!structMapping.BaseMapping.IsSequence && structMapping.BaseMapping != null && !structMapping.BaseMapping.TypeDesc.IsRoot)
			{
				structMapping = structMapping.BaseMapping;
			}
			structMapping.IsSequence = true;
			for (StructMapping structMapping2 = structMapping.DerivedMappings; structMapping2 != null; structMapping2 = structMapping2.NextDerivedMapping)
			{
				structMapping2.SetSequence();
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00067C11 File Offset: 0x00065E11
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x00067C2B File Offset: 0x00065E2B
		internal bool IsSequence
		{
			get
			{
				return this.isSequence && !base.TypeDesc.IsRoot;
			}
			set
			{
				this.isSequence = value;
			}
		}

		// Token: 0x04000AE7 RID: 2791
		private MemberMapping[] members;

		// Token: 0x04000AE8 RID: 2792
		private StructMapping baseMapping;

		// Token: 0x04000AE9 RID: 2793
		private StructMapping derivedMappings;

		// Token: 0x04000AEA RID: 2794
		private StructMapping nextDerivedMapping;

		// Token: 0x04000AEB RID: 2795
		private MemberMapping xmlnsMember;

		// Token: 0x04000AEC RID: 2796
		private bool hasSimpleContent;

		// Token: 0x04000AED RID: 2797
		private bool openModel;

		// Token: 0x04000AEE RID: 2798
		private bool isSequence;

		// Token: 0x04000AEF RID: 2799
		private NameTable elements;

		// Token: 0x04000AF0 RID: 2800
		private NameTable attributes;

		// Token: 0x04000AF1 RID: 2801
		private CodeIdentifiers scope;
	}
}
