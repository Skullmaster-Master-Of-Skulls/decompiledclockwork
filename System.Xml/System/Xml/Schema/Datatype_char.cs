using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EA RID: 490
	internal class Datatype_char : Datatype_anySimpleType
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x000644A6 File Offset: 0x000634A6
		public override Type ValueType
		{
			get
			{
				return Datatype_char.atomicValueType;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x000644AD File Offset: 0x000634AD
		internal override Type ListValueType
		{
			get
			{
				return Datatype_char.listValueType;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x000644B4 File Offset: 0x000634B4
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000644B8 File Offset: 0x000634B8
		internal override int Compare(object value1, object value2)
		{
			return ((char)value1).CompareTo(value2);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000644D4 File Offset: 0x000634D4
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

		// Token: 0x0600176F RID: 5999 RVA: 0x00064530 File Offset: 0x00063530
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

		// Token: 0x04000DB0 RID: 3504
		private static readonly Type atomicValueType = typeof(char);

		// Token: 0x04000DB1 RID: 3505
		private static readonly Type listValueType = typeof(char[]);
	}
}
