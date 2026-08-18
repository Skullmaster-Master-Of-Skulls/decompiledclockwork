using System;

namespace System.Xml.Schema
{
	// Token: 0x02000240 RID: 576
	internal class Datatype_char : Datatype_anySimpleType
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x000B7DEA File Offset: 0x000B5FEA
		public override Type ValueType
		{
			get
			{
				return Datatype_char.atomicValueType;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000B7DF1 File Offset: 0x000B5FF1
		internal override Type ListValueType
		{
			get
			{
				return Datatype_char.listValueType;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x000B7DF8 File Offset: 0x000B5FF8
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000B7DFC File Offset: 0x000B5FFC
		internal override int Compare(object value1, object value2)
		{
			return ((char)value1).CompareTo(value2);
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000B7E18 File Offset: 0x000B6018
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object result;
			try
			{
				result = XmlConvert.ToChar(s);
			}
			catch (XmlSchemaException ex)
			{
				throw ex;
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("Sch_InvalidValue", new object[]
				{
					s
				}), innerException);
			}
			return result;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000B7E70 File Offset: 0x000B6070
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			char c;
			Exception ex = XmlConvert.TryToChar(s, out c);
			if (ex == null)
			{
				typedValue = c;
				return null;
			}
			return ex;
		}

		// Token: 0x04000EA2 RID: 3746
		private static readonly Type atomicValueType = typeof(char);

		// Token: 0x04000EA3 RID: 3747
		private static readonly Type listValueType = typeof(char[]);
	}
}
