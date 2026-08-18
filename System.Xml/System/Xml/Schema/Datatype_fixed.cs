using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EB RID: 491
	internal class Datatype_fixed : Datatype_decimal
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x00064584 File Offset: 0x00063584
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			Exception ex;
			try
			{
				Numeric10FacetsChecker numeric10FacetsChecker = this.FacetsChecker as Numeric10FacetsChecker;
				decimal num = XmlConvert.ToDecimal(s);
				ex = numeric10FacetsChecker.CheckTotalAndFractionDigits(num, 18, 4, true, true);
				if (ex == null)
				{
					return num;
				}
			}
			catch (XmlSchemaException ex2)
			{
				throw ex2;
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("Sch_InvalidValue", new object[]
				{
					s
				}), innerException);
			}
			throw ex;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00064608 File Offset: 0x00063608
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			decimal num;
			Exception ex = XmlConvert.TryToDecimal(s, out num);
			if (ex == null)
			{
				Numeric10FacetsChecker numeric10FacetsChecker = this.FacetsChecker as Numeric10FacetsChecker;
				ex = numeric10FacetsChecker.CheckTotalAndFractionDigits(num, 18, 4, true, true);
				if (ex == null)
				{
					typedValue = num;
					return null;
				}
			}
			return ex;
		}
	}
}
