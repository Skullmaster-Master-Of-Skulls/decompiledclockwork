using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000154 RID: 340
	internal abstract class AccessorMapping : Mapping
	{
		// Token: 0x060017A0 RID: 6048 RVA: 0x00067C3C File Offset: 0x00065E3C
		internal AccessorMapping()
		{
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00067C44 File Offset: 0x00065E44
		protected AccessorMapping(AccessorMapping mapping) : base(mapping)
		{
			this.typeDesc = mapping.typeDesc;
			this.attribute = mapping.attribute;
			this.elements = mapping.elements;
			this.sortedElements = mapping.sortedElements;
			this.text = mapping.text;
			this.choiceIdentifier = mapping.choiceIdentifier;
			this.xmlns = mapping.xmlns;
			this.ignore = mapping.ignore;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00067CB8 File Offset: 0x00065EB8
		internal bool IsAttribute
		{
			get
			{
				return this.attribute != null;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x00067CC3 File Offset: 0x00065EC3
		internal bool IsText
		{
			get
			{
				return this.text != null && (this.elements == null || this.elements.Length == 0);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00067CE3 File Offset: 0x00065EE3
		internal bool IsParticle
		{
			get
			{
				return this.elements != null && this.elements.Length != 0;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x00067CF9 File Offset: 0x00065EF9
		// (set) Token: 0x060017A6 RID: 6054 RVA: 0x00067D01 File Offset: 0x00065F01
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00067D0A File Offset: 0x00065F0A
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x00067D12 File Offset: 0x00065F12
		internal AttributeAccessor Attribute
		{
			get
			{
				return this.attribute;
			}
			set
			{
				this.attribute = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x00067D1B File Offset: 0x00065F1B
		// (set) Token: 0x060017AA RID: 6058 RVA: 0x00067D23 File Offset: 0x00065F23
		internal ElementAccessor[] Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
				this.sortedElements = null;
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00067D33 File Offset: 0x00065F33
		internal static void SortMostToLeastDerived(ElementAccessor[] elements)
		{
			Array.Sort(elements, new AccessorMapping.AccessorComparer());
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00067D40 File Offset: 0x00065F40
		internal ElementAccessor[] ElementsSortedByDerivation
		{
			get
			{
				if (this.sortedElements != null)
				{
					return this.sortedElements;
				}
				if (this.elements == null)
				{
					return null;
				}
				this.sortedElements = new ElementAccessor[this.elements.Length];
				Array.Copy(this.elements, 0, this.sortedElements, 0, this.elements.Length);
				AccessorMapping.SortMostToLeastDerived(this.sortedElements);
				return this.sortedElements;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x00067DA5 File Offset: 0x00065FA5
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x00067DAD File Offset: 0x00065FAD
		internal TextAccessor Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x00067DB6 File Offset: 0x00065FB6
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x00067DBE File Offset: 0x00065FBE
		internal ChoiceIdentifierAccessor ChoiceIdentifier
		{
			get
			{
				return this.choiceIdentifier;
			}
			set
			{
				this.choiceIdentifier = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00067DC7 File Offset: 0x00065FC7
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x00067DCF File Offset: 0x00065FCF
		internal XmlnsAccessor Xmlns
		{
			get
			{
				return this.xmlns;
			}
			set
			{
				this.xmlns = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00067DD8 File Offset: 0x00065FD8
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00067DE0 File Offset: 0x00065FE0
		internal bool Ignore
		{
			get
			{
				return this.ignore;
			}
			set
			{
				this.ignore = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x00067DE9 File Offset: 0x00065FE9
		internal Accessor Accessor
		{
			get
			{
				if (this.xmlns != null)
				{
					return this.xmlns;
				}
				if (this.attribute != null)
				{
					return this.attribute;
				}
				if (this.elements != null && this.elements.Length != 0)
				{
					return this.elements[0];
				}
				return this.text;
			}
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00067E2C File Offset: 0x0006602C
		private static bool IsNeedNullableMember(ElementAccessor element)
		{
			if (element.Mapping is ArrayMapping)
			{
				ArrayMapping arrayMapping = (ArrayMapping)element.Mapping;
				return arrayMapping.Elements != null && arrayMapping.Elements.Length == 1 && AccessorMapping.IsNeedNullableMember(arrayMapping.Elements[0]);
			}
			return element.IsNullable && element.Mapping.TypeDesc.IsValueType;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x00067E8F File Offset: 0x0006608F
		internal bool IsNeedNullable
		{
			get
			{
				return this.xmlns == null && this.attribute == null && (this.elements != null && this.elements.Length == 1) && AccessorMapping.IsNeedNullableMember(this.elements[0]);
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x00067EC8 File Offset: 0x000660C8
		internal static bool ElementsMatch(ElementAccessor[] a, ElementAccessor[] b)
		{
			if (a == null)
			{
				return b == null;
			}
			if (b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i].Name != b[i].Name || a[i].Namespace != b[i].Namespace || a[i].Form != b[i].Form || a[i].IsNullable != b[i].IsNullable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00067F54 File Offset: 0x00066154
		internal bool Match(AccessorMapping mapping)
		{
			if (this.Elements != null && this.Elements.Length != 0)
			{
				if (!AccessorMapping.ElementsMatch(this.Elements, mapping.Elements))
				{
					return false;
				}
				if (this.Text == null)
				{
					return mapping.Text == null;
				}
			}
			if (this.Attribute != null)
			{
				return mapping.Attribute != null && (this.Attribute.Name == mapping.Attribute.Name && this.Attribute.Namespace == mapping.Attribute.Namespace) && this.Attribute.Form == mapping.Attribute.Form;
			}
			if (this.Text != null)
			{
				return mapping.Text != null;
			}
			return mapping.Accessor == null;
		}

		// Token: 0x04000AF2 RID: 2802
		private TypeDesc typeDesc;

		// Token: 0x04000AF3 RID: 2803
		private AttributeAccessor attribute;

		// Token: 0x04000AF4 RID: 2804
		private ElementAccessor[] elements;

		// Token: 0x04000AF5 RID: 2805
		private ElementAccessor[] sortedElements;

		// Token: 0x04000AF6 RID: 2806
		private TextAccessor text;

		// Token: 0x04000AF7 RID: 2807
		private ChoiceIdentifierAccessor choiceIdentifier;

		// Token: 0x04000AF8 RID: 2808
		private XmlnsAccessor xmlns;

		// Token: 0x04000AF9 RID: 2809
		private bool ignore;

		// Token: 0x0200047A RID: 1146
		internal class AccessorComparer : IComparer
		{
			// Token: 0x060030C6 RID: 12486 RVA: 0x0011D620 File Offset: 0x0011B820
			public int Compare(object o1, object o2)
			{
				if (o1 == o2)
				{
					return 0;
				}
				Accessor accessor = (Accessor)o1;
				Accessor accessor2 = (Accessor)o2;
				int weight = accessor.Mapping.TypeDesc.Weight;
				int weight2 = accessor2.Mapping.TypeDesc.Weight;
				if (weight == weight2)
				{
					return 0;
				}
				if (weight < weight2)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
