using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002CE RID: 718
	internal abstract class AccessorMapping : Mapping
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0009F960 File Offset: 0x0009E960
		internal bool IsAttribute
		{
			get
			{
				return this.attribute != null;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0009F96E File Offset: 0x0009E96E
		internal bool IsText
		{
			get
			{
				return this.text != null && (this.elements == null || this.elements.Length == 0);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0009F98F File Offset: 0x0009E98F
		internal bool IsParticle
		{
			get
			{
				return this.elements != null && this.elements.Length > 0;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x0009F9A6 File Offset: 0x0009E9A6
		// (set) Token: 0x060021F9 RID: 8697 RVA: 0x0009F9AE File Offset: 0x0009E9AE
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

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x0009F9B7 File Offset: 0x0009E9B7
		// (set) Token: 0x060021FB RID: 8699 RVA: 0x0009F9BF File Offset: 0x0009E9BF
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

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x0009F9C8 File Offset: 0x0009E9C8
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x0009F9D0 File Offset: 0x0009E9D0
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

		// Token: 0x060021FE RID: 8702 RVA: 0x0009F9E0 File Offset: 0x0009E9E0
		internal static void SortMostToLeastDerived(ElementAccessor[] elements)
		{
			Array.Sort(elements, new AccessorMapping.AccessorComparer());
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x0009F9F0 File Offset: 0x0009E9F0
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

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x0009FA55 File Offset: 0x0009EA55
		// (set) Token: 0x06002201 RID: 8705 RVA: 0x0009FA5D File Offset: 0x0009EA5D
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

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x0009FA66 File Offset: 0x0009EA66
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x0009FA6E File Offset: 0x0009EA6E
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

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0009FA77 File Offset: 0x0009EA77
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x0009FA7F File Offset: 0x0009EA7F
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

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x0009FA88 File Offset: 0x0009EA88
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x0009FA90 File Offset: 0x0009EA90
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

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x0009FA9C File Offset: 0x0009EA9C
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
				if (this.elements != null && this.elements.Length > 0)
				{
					return this.elements[0];
				}
				return this.text;
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0009FAEC File Offset: 0x0009EAEC
		private static bool IsNeedNullableMember(ElementAccessor element)
		{
			if (element.Mapping is ArrayMapping)
			{
				ArrayMapping arrayMapping = (ArrayMapping)element.Mapping;
				return arrayMapping.Elements != null && arrayMapping.Elements.Length == 1 && AccessorMapping.IsNeedNullableMember(arrayMapping.Elements[0]);
			}
			return element.IsNullable && element.Mapping.TypeDesc.IsValueType;
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0009FB4F File Offset: 0x0009EB4F
		internal bool IsNeedNullable
		{
			get
			{
				return this.xmlns == null && this.attribute == null && (this.elements != null && this.elements.Length == 1) && AccessorMapping.IsNeedNullableMember(this.elements[0]);
			}
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0009FB88 File Offset: 0x0009EB88
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

		// Token: 0x0600220C RID: 8716 RVA: 0x0009FC14 File Offset: 0x0009EC14
		internal bool Match(AccessorMapping mapping)
		{
			if (this.Elements != null && this.Elements.Length > 0)
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

		// Token: 0x0400148C RID: 5260
		private TypeDesc typeDesc;

		// Token: 0x0400148D RID: 5261
		private AttributeAccessor attribute;

		// Token: 0x0400148E RID: 5262
		private ElementAccessor[] elements;

		// Token: 0x0400148F RID: 5263
		private ElementAccessor[] sortedElements;

		// Token: 0x04001490 RID: 5264
		private TextAccessor text;

		// Token: 0x04001491 RID: 5265
		private ChoiceIdentifierAccessor choiceIdentifier;

		// Token: 0x04001492 RID: 5266
		private XmlnsAccessor xmlns;

		// Token: 0x04001493 RID: 5267
		private bool ignore;

		// Token: 0x020002CF RID: 719
		internal class AccessorComparer : IComparer
		{
			// Token: 0x0600220E RID: 8718 RVA: 0x0009FCE8 File Offset: 0x0009ECE8
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
