using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000146 RID: 326
	internal abstract class Accessor
	{
		// Token: 0x06001722 RID: 5922 RVA: 0x000670F5 File Offset: 0x000652F5
		internal Accessor()
		{
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x000670FD File Offset: 0x000652FD
		// (set) Token: 0x06001724 RID: 5924 RVA: 0x00067105 File Offset: 0x00065305
		internal TypeMapping Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0006710E File Offset: 0x0006530E
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x00067116 File Offset: 0x00065316
		internal object Default
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0006711F File Offset: 0x0006531F
		internal bool HasDefault
		{
			get
			{
				return this.defaultValue != null && this.defaultValue != DBNull.Value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x0006713B File Offset: 0x0006533B
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x00067151 File Offset: 0x00065351
		internal virtual string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x0006715A File Offset: 0x0006535A
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x00067162 File Offset: 0x00065362
		internal bool Any
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x0006716B File Offset: 0x0006536B
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x00067173 File Offset: 0x00065373
		internal string AnyNamespaces
		{
			get
			{
				return this.anyNs;
			}
			set
			{
				this.anyNs = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0006717C File Offset: 0x0006537C
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x00067184 File Offset: 0x00065384
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x0006718D File Offset: 0x0006538D
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x00067195 File Offset: 0x00065395
		internal XmlSchemaForm Form
		{
			get
			{
				return this.form;
			}
			set
			{
				this.form = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0006719E File Offset: 0x0006539E
		// (set) Token: 0x06001733 RID: 5939 RVA: 0x000671A6 File Offset: 0x000653A6
		internal bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				this.isFixed = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x000671AF File Offset: 0x000653AF
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x000671B7 File Offset: 0x000653B7
		internal bool IsOptional
		{
			get
			{
				return this.isOptional;
			}
			set
			{
				this.isOptional = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x000671C0 File Offset: 0x000653C0
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x000671C8 File Offset: 0x000653C8
		internal bool IsTopLevelInSchema
		{
			get
			{
				return this.topLevelInSchema;
			}
			set
			{
				this.topLevelInSchema = value;
			}
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000671D1 File Offset: 0x000653D1
		internal static string EscapeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			return XmlConvert.EncodeLocalName(name);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000671E8 File Offset: 0x000653E8
		internal static string EscapeQName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			int num = name.LastIndexOf(':');
			if (num < 0)
			{
				return XmlConvert.EncodeLocalName(name);
			}
			if (num == 0 || num == name.Length - 1)
			{
				throw new ArgumentException(Res.GetString("Xml_InvalidNameChars", new object[]
				{
					name
				}), "name");
			}
			return new XmlQualifiedName(XmlConvert.EncodeLocalName(name.Substring(num + 1)), XmlConvert.EncodeLocalName(name.Substring(0, num))).ToString();
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00067268 File Offset: 0x00065468
		internal static string UnescapeName(string name)
		{
			return XmlConvert.DecodeName(name);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00067270 File Offset: 0x00065470
		internal string ToString(string defaultNs)
		{
			if (this.Any)
			{
				return ((this.Namespace == null) ? "##any" : this.Namespace) + ":" + this.Name;
			}
			if (!(this.Namespace == defaultNs))
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		// Token: 0x04000AC2 RID: 2754
		private string name;

		// Token: 0x04000AC3 RID: 2755
		private object defaultValue;

		// Token: 0x04000AC4 RID: 2756
		private string ns;

		// Token: 0x04000AC5 RID: 2757
		private TypeMapping mapping;

		// Token: 0x04000AC6 RID: 2758
		private bool any;

		// Token: 0x04000AC7 RID: 2759
		private string anyNs;

		// Token: 0x04000AC8 RID: 2760
		private bool topLevelInSchema;

		// Token: 0x04000AC9 RID: 2761
		private bool isFixed;

		// Token: 0x04000ACA RID: 2762
		private bool isOptional;

		// Token: 0x04000ACB RID: 2763
		private XmlSchemaForm form;
	}
}
