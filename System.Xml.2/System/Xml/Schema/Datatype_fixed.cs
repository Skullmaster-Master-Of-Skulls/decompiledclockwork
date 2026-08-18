using System;

namespace System.Xml.Schema
{
	// Token: 0x02000241 RID: 577
	internal class Datatype_fixed : Datatype_decimal
	{
		// Token: 0x0600229C RID: 8860 RVA: 0x000B7EC4 File Offset: 0x000B60C4
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

		// Token: 0x0600229D RID: 8861 RVA: 0x000B7F44 File Offset: 0x000B6144
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
