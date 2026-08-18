using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000AE RID: 174
	public class JSVariableField
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00036622 File Offset: 0x00034822
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0003662A File Offset: 0x0003482A
		public Context OriginalContext { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00036633 File Offset: 0x00034833
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0003663B File Offset: 0x0003483B
		public string Name { get; private set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x00036644 File Offset: 0x00034844
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0003664C File Offset: 0x0003484C
		public FieldType FieldType { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00036655 File Offset: 0x00034855
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0003665D File Offset: 0x0003485D
		public FieldAttributes Attributes { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00036666 File Offset: 0x00034866
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x0003666E File Offset: 0x0003486E
		public object FieldValue { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00036677 File Offset: 0x00034877
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0003667F File Offset: 0x0003487F
		public bool IsFunction { get; internal set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00036688 File Offset: 0x00034888
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00036690 File Offset: 0x00034890
		public bool IsAmbiguous { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00036699 File Offset: 0x00034899
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x000366A1 File Offset: 0x000348A1
		public bool IsPlaceholder { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x000366AA File Offset: 0x000348AA
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x000366B2 File Offset: 0x000348B2
		public bool HasNoReferences { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x000366BB File Offset: 0x000348BB
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x000366C3 File Offset: 0x000348C3
		public bool InitializationOnly { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x000366CC File Offset: 0x000348CC
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x000366D4 File Offset: 0x000348D4
		public int Position { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x000366DD File Offset: 0x000348DD
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x000366E5 File Offset: 0x000348E5
		public bool WasRemoved { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000366EE File Offset: 0x000348EE
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x000366F6 File Offset: 0x000348F6
		public bool IsExported { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000366FF File Offset: 0x000348FF
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x00036707 File Offset: 0x00034907
		public JSVariableField OuterField { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00036710 File Offset: 0x00034910
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0003672C File Offset: 0x0003492C
		public ActivationObject OwningScope
		{
			get
			{
				if (this.OuterField != null)
				{
					return this.OuterField.OwningScope;
				}
				return this.m_owningScope;
			}
			set
			{
				this.m_owningScope = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00036735 File Offset: 0x00034935
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0003673D File Offset: 0x0003493D
		public JSVariableField GhostedField { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00036746 File Offset: 0x00034946
		public int RefCount
		{
			get
			{
				return this.m_referenceTable.Count;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00036753 File Offset: 0x00034953
		public ICollection<INameReference> References
		{
			get
			{
				return this.m_referenceTable;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0003675C File Offset: 0x0003495C
		public INameReference OnlyReference
		{
			get
			{
				INameReference[] array = new INameReference[1];
				if (this.m_referenceTable.Count == 1)
				{
					this.m_referenceTable.CopyTo(array, 0);
				}
				return array[0];
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0003678E File Offset: 0x0003498E
		public ICollection<INameDeclaration> Declarations
		{
			get
			{
				return this.m_declarationTable;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x00036798 File Offset: 0x00034998
		public INameDeclaration OnlyDeclaration
		{
			get
			{
				INameDeclaration[] array = new INameDeclaration[1];
				if (this.m_declarationTable.Count == 1)
				{
					this.m_declarationTable.CopyTo(array, 0);
				}
				return array[0];
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000367CA File Offset: 0x000349CA
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x000367DB File Offset: 0x000349DB
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x000367E3 File Offset: 0x000349E3
		public bool CanCrunch
		{
			get
			{
				return this.m_canCrunch;
			}
			set
			{
				this.m_canCrunch = value;
				if (this.OuterField != null && !value)
				{
					this.OuterField.CanCrunch = false;
				}
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00036803 File Offset: 0x00034A03
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0003680B File Offset: 0x00034A0B
		public bool IsDeclared
		{
			get
			{
				return this.m_isDeclared;
			}
			set
			{
				this.m_isDeclared = value;
				if (this.OuterField != null)
				{
					this.OuterField.IsDeclared = value;
				}
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00036828 File Offset: 0x00034A28
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x00036844 File Offset: 0x00034A44
		public bool IsGenerated
		{
			get
			{
				if (this.OuterField == null)
				{
					return this.m_isGenerated;
				}
				return this.OuterField.IsGenerated;
			}
			set
			{
				this.m_isGenerated = value;
				if (this.OuterField != null)
				{
					this.OuterField.IsGenerated = value;
				}
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00036864 File Offset: 0x00034A64
		public bool IsOuterReference
		{
			get
			{
				if (this.OuterField != null)
				{
					for (JSVariableField outerField = this.OuterField; outerField != null; outerField = outerField.OuterField)
					{
						if (!outerField.IsPlaceholder)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00036897 File Offset: 0x00034A97
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x000368B3 File Offset: 0x00034AB3
		public string CrunchedName
		{
			get
			{
				if (this.OuterField == null)
				{
					return this.m_crunchedName;
				}
				return this.OuterField.CrunchedName;
			}
			set
			{
				if (this.m_canCrunch)
				{
					if (this.OuterField != null)
					{
						this.OuterField.CrunchedName = value;
						return;
					}
					this.m_crunchedName = value;
				}
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000368DC File Offset: 0x00034ADC
		public bool IsReferenced
		{
			get
			{
				FunctionObject functionObject = this.FieldValue as FunctionObject;
				if (functionObject != null)
				{
					return functionObject.IsReferenced;
				}
				return this.FieldValue is ClassNode || this.RefCount > 0;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00036918 File Offset: 0x00034B18
		public bool IsReferencedInnerScope
		{
			get
			{
				foreach (INameReference nameReference in this.References)
				{
					if (nameReference.VariableField.OuterField != null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00036974 File Offset: 0x00034B74
		public JSVariableField(FieldType fieldType, string name, FieldAttributes fieldAttributes, object value)
		{
			this.m_referenceTable = new HashSet<INameReference>();
			this.m_declarationTable = new HashSet<INameDeclaration>();
			this.Name = name;
			this.Attributes = fieldAttributes;
			this.FieldValue = value;
			this.SetFieldsBasedOnType(fieldType);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000369B0 File Offset: 0x00034BB0
		internal JSVariableField(FieldType fieldType, JSVariableField outerField)
		{
			if (outerField == null)
			{
				throw new ArgumentNullException("outerField");
			}
			this.m_referenceTable = new HashSet<INameReference>();
			this.m_declarationTable = new HashSet<INameDeclaration>();
			this.OuterField = outerField;
			this.Name = outerField.Name;
			this.Attributes = outerField.Attributes;
			this.FieldValue = outerField.FieldValue;
			this.IsGenerated = outerField.IsGenerated;
			this.SetFieldsBasedOnType(fieldType);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00036A28 File Offset: 0x00034C28
		private void SetFieldsBasedOnType(FieldType fieldType)
		{
			this.FieldType = fieldType;
			switch (this.FieldType)
			{
			case FieldType.Local:
				this.CanCrunch = true;
				return;
			case FieldType.Predefined:
				this.IsDeclared = false;
				this.CanCrunch = false;
				return;
			case FieldType.Global:
			case FieldType.WithField:
			case FieldType.UndefinedGlobal:
			case FieldType.Super:
				this.CanCrunch = false;
				return;
			case FieldType.Arguments:
				this.IsDeclared = false;
				this.CanCrunch = false;
				return;
			case FieldType.Argument:
			case FieldType.CatchError:
				this.IsDeclared = true;
				this.CanCrunch = true;
				return;
			case FieldType.GhostCatch:
				this.CanCrunch = true;
				this.IsPlaceholder = true;
				return;
			case FieldType.GhostFunction:
				this.CanCrunch = (this.OuterField == null || this.OuterField.CanCrunch);
				this.IsFunction = true;
				this.IsPlaceholder = true;
				return;
			default:
				throw new ArgumentException("Invalid field type", "fieldType");
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00036AFD File Offset: 0x00034CFD
		public void AddReference(INameReference reference)
		{
			if (reference != null)
			{
				this.m_referenceTable.Add(reference);
				if (this.OuterField != null)
				{
					this.OuterField.AddReference(reference);
				}
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00036B24 File Offset: 0x00034D24
		public void AddReferences(IEnumerable<INameReference> references)
		{
			if (references != null)
			{
				foreach (INameReference reference in references)
				{
					this.AddReference(reference);
				}
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00036B70 File Offset: 0x00034D70
		public void Detach()
		{
			this.OuterField = null;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00036B7C File Offset: 0x00034D7C
		public override string ToString()
		{
			string crunchedName = this.CrunchedName;
			if (!string.IsNullOrEmpty(crunchedName))
			{
				return crunchedName;
			}
			return this.Name;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00036BA0 File Offset: 0x00034DA0
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00036BB0 File Offset: 0x00034DB0
		public bool IsSameField(JSVariableField otherField)
		{
			if (this == otherField)
			{
				return true;
			}
			if (otherField == null)
			{
				return false;
			}
			JSVariableField jsvariableField = (this.OuterField != null) ? this.OuterField : this;
			while (jsvariableField.OuterField != null)
			{
				jsvariableField = jsvariableField.OuterField;
			}
			JSVariableField jsvariableField2 = (otherField.OuterField != null) ? otherField.OuterField : otherField;
			while (jsvariableField2.OuterField != null)
			{
				jsvariableField2 = jsvariableField2.OuterField;
			}
			return jsvariableField == jsvariableField2;
		}

		// Token: 0x040004A2 RID: 1186
		private ActivationObject m_owningScope;

		// Token: 0x040004A3 RID: 1187
		private HashSet<INameReference> m_referenceTable;

		// Token: 0x040004A4 RID: 1188
		private HashSet<INameDeclaration> m_declarationTable;

		// Token: 0x040004A5 RID: 1189
		private bool m_canCrunch;

		// Token: 0x040004A6 RID: 1190
		private bool m_isDeclared;

		// Token: 0x040004A7 RID: 1191
		private bool m_isGenerated;

		// Token: 0x040004A8 RID: 1192
		private string m_crunchedName;
	}
}
