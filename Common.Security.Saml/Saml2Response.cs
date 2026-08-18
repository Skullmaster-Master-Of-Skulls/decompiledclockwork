using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000010 RID: 16
	public class Saml2Response
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000038F9 File Offset: 0x00001AF9
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003901 File Offset: 0x00001B01
		public Saml2SecurityToken Assertion { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000390A File Offset: 0x00001B0A
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003912 File Offset: 0x00001B12
		public string ID { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000391B File Offset: 0x00001B1B
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003923 File Offset: 0x00001B23
		public string Version { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000392C File Offset: 0x00001B2C
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003934 File Offset: 0x00001B34
		public string InResponseTo { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000393D File Offset: 0x00001B3D
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003945 File Offset: 0x00001B45
		public string Destination { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000394E File Offset: 0x00001B4E
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003956 File Offset: 0x00001B56
		public string Issuer { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000395F File Offset: 0x00001B5F
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003967 File Offset: 0x00001B67
		public DateTime IssueInstant { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003970 File Offset: 0x00001B70
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003978 File Offset: 0x00001B78
		public SamlResponseStatusCode? StatusCode { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003981 File Offset: 0x00001B81
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003989 File Offset: 0x00001B89
		public X509Certificate2 Signature { get; set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00003994 File Offset: 0x00001B94
		public virtual void ReadXml(string samlResponse, SecurityTokenElement tokenIssuer)
		{
			StringReader input = new StringReader(samlResponse);
			XmlReader xmlReader = XmlReader.Create(input);
			SamlResponseReader responseReader = this.ResponseReader;
			responseReader.DeserializeSamlResponse(xmlReader, this, tokenIssuer);
			this.StatusCode = new SamlResponseStatusCode?(SamlResponseStatusCode.Success);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000039CE File Offset: 0x00001BCE
		protected virtual SamlResponseReader ResponseReader
		{
			get
			{
				return new SamlResponseReader();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000039D8 File Offset: 0x00001BD8
		public IDictionary<string, string> GetClaims()
		{
			bool flag = this.Assertion == null;
			if (flag)
			{
				throw new Exception("samlResponse.Assertion is null");
			}
			bool flag2 = this.Assertion.Assertion == null;
			if (flag2)
			{
				throw new Exception("samlResponse.Assertion.Assertion is null");
			}
			bool flag3 = this.Assertion.Assertion.Statements == null;
			if (flag3)
			{
				throw new Exception("resp.Assertion.Assertion.Statements is null");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (Saml2Statement saml2Statement in this.Assertion.Assertion.Statements)
			{
				Saml2AttributeStatement saml2AttributeStatement = saml2Statement as Saml2AttributeStatement;
				bool flag4 = saml2AttributeStatement == null;
				if (!flag4)
				{
					foreach (Saml2Attribute saml2Attribute in saml2AttributeStatement.Attributes)
					{
						string name = saml2Attribute.Name;
						string value = string.Join(", ", (from string str in saml2Attribute.Values
						select str ?? "" into h
						where h.Length > 0
						select h).ToArray<string>());
						bool flag5 = !dictionary.ContainsKey(name);
						if (flag5)
						{
							dictionary.Add(name, value);
						}
					}
				}
			}
			return dictionary;
		}
	}
}
