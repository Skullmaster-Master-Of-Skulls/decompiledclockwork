using System;
using System.Globalization;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200030E RID: 782
	internal class SchemaEnumMember : SchemaElement
	{
		// Token: 0x06002E7A RID: 11898 RVA: 0x000A9632 File Offset: 0x000A7832
		public SchemaEnumMember(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x000AFAAA File Offset: 0x000ADCAA
		// (set) Token: 0x06002E7C RID: 11900 RVA: 0x000AFAB2 File Offset: 0x000ADCB2
		public long? Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000AFABC File Offset: 0x000ADCBC
		protected override bool HandleAttribute(XmlReader reader)
		{
			bool flag = base.HandleAttribute(reader);
			if (!flag && (flag = SchemaElement.CanHandleAttribute(reader, "Value")))
			{
				this.HandleValueAttribute(reader);
			}
			return flag;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000AFAEC File Offset: 0x000ADCEC
		private void HandleValueAttribute(XmlReader reader)
		{
			long value;
			if (long.TryParse(reader.Value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value))
			{
				this._value = new long?(value);
			}
		}

		// Token: 0x04001426 RID: 5158
		private long? _value;
	}
}
