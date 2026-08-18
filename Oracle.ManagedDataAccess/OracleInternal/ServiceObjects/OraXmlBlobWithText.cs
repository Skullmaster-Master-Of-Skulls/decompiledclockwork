using System;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.I18N;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001C0 RID: 448
	internal class OraXmlBlobWithText
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x000BF4E8 File Offset: 0x000BD6E8
		internal OraXmlBlobWithText(OracleConnection conn, byte[] lobLocator, int csid)
		{
			this.m_xmlBlob = new OracleBlob(conn, lobLocator);
			this.m_csid = csid;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x000BF518 File Offset: 0x000BD718
		internal OraXmlBlobWithText(OracleBlob blob, int csid)
		{
			this.m_xmlBlob = blob;
			this.m_csid = csid;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x000BF540 File Offset: 0x000BD740
		internal bool IsEmpty
		{
			get
			{
				if (!this.m_bGetIsEmpty)
				{
					this.m_bIsEmpty = this.m_xmlBlob.IsEmpty;
				}
				return this.m_bIsEmpty;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x000BF564 File Offset: 0x000BD764
		internal string Value
		{
			get
			{
				if (!this.m_bGetValue)
				{
					byte[] value = this.m_xmlBlob.Value;
					if (value != null && value.Length > 0)
					{
						Conv instance = Conv.GetInstance(this.m_csid);
						this.m_strValue = instance.ConvertBytesToString(value, 0, value.Length, null, true);
					}
					else
					{
						this.m_strValue = string.Empty;
					}
					this.m_bIsEmpty = string.IsNullOrEmpty(this.m_strValue);
					this.m_bGetIsEmpty = true;
					this.m_bGetValue = true;
				}
				return this.m_strValue;
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000BF5E8 File Offset: 0x000BD7E8
		internal OraXmlBlobWithText Clone()
		{
			return new OraXmlBlobWithText((OracleBlob)this.m_xmlBlob.Clone(), this.m_csid);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000BF608 File Offset: 0x000BD808
		internal void Dispose()
		{
			if (this.m_xmlBlob != null)
			{
				this.m_xmlBlob.Dispose();
				this.m_xmlBlob = null;
			}
		}

		// Token: 0x0400139D RID: 5021
		internal OracleBlob m_xmlBlob;

		// Token: 0x0400139E RID: 5022
		internal int m_csid;

		// Token: 0x0400139F RID: 5023
		internal bool m_bGetValue;

		// Token: 0x040013A0 RID: 5024
		internal bool m_bGetIsEmpty;

		// Token: 0x040013A1 RID: 5025
		internal string m_strValue = string.Empty;

		// Token: 0x040013A2 RID: 5026
		internal bool m_bIsEmpty = true;
	}
}
