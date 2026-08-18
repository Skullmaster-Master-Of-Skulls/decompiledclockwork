using System;

namespace System.Xml.Schema
{
	// Token: 0x02000242 RID: 578
	internal class Datatype_uuid : Datatype_anySimpleType
	{
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x000B7F91 File Offset: 0x000B6191
		public override Type ValueType
		{
			get
			{
				return Datatype_uuid.atomicValueType;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000B7F98 File Offset: 0x000B6198
		internal override Type ListValueType
		{
			get
			{
				return Datatype_uuid.listValueType;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000B7F9F File Offset: 0x000B619F
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000B7FA4 File Offset: 0x000B61A4
		internal override int Compare(object value1, object value2)
		{
			if (!((Guid)value1).Equals(value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000B7FCC File Offset: 0x000B61CC
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

		// Token: 0x060022A4 RID: 8868 RVA: 0x000B8024 File Offset: 0x000B6224
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

		// Token: 0x04000EA4 RID: 3748
		private static readonly Type atomicValueType = typeof(Guid);

		// Token: 0x04000EA5 RID: 3749
		private static readonly Type listValueType = typeof(Guid[]);
	}
}
