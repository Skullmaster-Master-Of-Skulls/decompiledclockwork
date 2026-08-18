using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EC RID: 492
	internal class Datatype_uuid : Datatype_anySimpleType
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00064655 File Offset: 0x00063655
		public override Type ValueType
		{
			get
			{
				return Datatype_uuid.atomicValueType;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x0006465C File Offset: 0x0006365C
		internal override Type ListValueType
		{
			get
			{
				return Datatype_uuid.listValueType;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x00064663 File Offset: 0x00063663
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00064668 File Offset: 0x00063668
		internal override int Compare(object value1, object value2)
		{
			if (!((Guid)value1).Equals(value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00064690 File Offset: 0x00063690
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			object result;
			try
			{
				result = XmlConvert.ToGuid(s);
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

		// Token: 0x0600177A RID: 6010 RVA: 0x000646EC File Offset: 0x000636EC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Guid guid;
			Exception ex = XmlConvert.TryToGuid(s, out guid);
			if (ex == null)
			{
				typedValue = guid;
				return null;
			}
			return ex;
		}

		// Token: 0x04000DB2 RID: 3506
		private static readonly Type atomicValueType = typeof(Guid);

		// Token: 0x04000DB3 RID: 3507
		private static readonly Type listValueType = typeof(Guid[]);
	}
}
