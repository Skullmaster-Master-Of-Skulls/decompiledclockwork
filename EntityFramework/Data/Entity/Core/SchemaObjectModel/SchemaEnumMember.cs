using System;
using System.Globalization;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200038B RID: 907
	internal class SchemaEnumMember : SchemaElement
	{
		// Token: 0x060020D3 RID: 8403 RVA: 0x0009A8E0 File Offset: 0x00098AE0
		public SchemaEnumMember(SchemaElement parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0009A8EA File Offset: 0x00098AEA
		// (set) Token: 0x060020D5 RID: 8405 RVA: 0x0009A8F2 File Offset: 0x00098AF2
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

		// Token: 0x060020D6 RID: 8406 RVA: 0x0009A8FC File Offset: 0x00098AFC
		protected override bool HandleAttribute(XmlReader reader)
		{
			bool flag = base.HandleAttribute(reader);
			if (!flag && (flag = SchemaElement.CanHandleAttribute(reader, "Value")))
			{
				this.HandleValueAttribute(reader);
			}
			return flag;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0009A92C File Offset: 0x00098B2C
		private void HandleValueAttribute(XmlReader reader)
		{
			long value;
			if (long.TryParse(reader.Value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out value))
			{
				this._value = new long?(value);
			}
		}

		// Token: 0x04000B9D RID: 2973
		private long? _value;
	}
}
