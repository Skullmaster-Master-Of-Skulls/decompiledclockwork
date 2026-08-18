using System;

namespace System.Xml.Schema
{
	// Token: 0x0200025C RID: 604
	internal sealed class SchemaEntity : IDtdEntityInfo
	{
		// Token: 0x06002436 RID: 9270 RVA: 0x000C64B4 File Offset: 0x000C46B4
		internal SchemaEntity(XmlQualifiedName qname, bool isParameter)
		{
			this.qname = qname;
			this.isParameter = isParameter;
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x000C64D5 File Offset: 0x000C46D5
		string IDtdEntityInfo.Name
		{
			get
			{
				return this.Name.Name;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x000C64E2 File Offset: 0x000C46E2
		bool IDtdEntityInfo.IsExternal
		{
			get
			{
				return this.IsExternal;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x000C64EA File Offset: 0x000C46EA
		bool IDtdEntityInfo.IsDeclaredInExternal
		{
			get
			{
				return this.DeclaredInExternal;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x0600243A RID: 9274 RVA: 0x000C64F2 File Offset: 0x000C46F2
		bool IDtdEntityInfo.IsUnparsedEntity
		{
			get
			{
				return !this.NData.IsEmpty;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x000C6502 File Offset: 0x000C4702
		bool IDtdEntityInfo.IsParameterEntity
		{
			get
			{
				return this.isParameter;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x000C650A File Offset: 0x000C470A
		string IDtdEntityInfo.BaseUriString
		{
			get
			{
				return this.BaseURI;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000C6512 File Offset: 0x000C4712
		string IDtdEntityInfo.DeclaredUriString
		{
			get
			{
				return this.DeclaredURI;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x000C651A File Offset: 0x000C471A
		string IDtdEntityInfo.SystemId
		{
			get
			{
				return this.Url;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000C6522 File Offset: 0x000C4722
		string IDtdEntityInfo.PublicId
		{
			get
			{
				return this.Pubid;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x000C652A File Offset: 0x000C472A
		string IDtdEntityInfo.Text
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000C6532 File Offset: 0x000C4732
		int IDtdEntityInfo.LineNumber
		{
			get
			{
				return this.Line;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x000C653A File Offset: 0x000C473A
		int IDtdEntityInfo.LinePosition
		{
			get
			{
				return this.Pos;
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000C6544 File Offset: 0x000C4744
		internal static bool IsPredefinedEntity(string n)
		{
			return n == "lt" || n == "gt" || n == "amp" || n == "apos" || n == "quot";
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x000C6592 File Offset: 0x000C4792
		internal XmlQualifiedName Name
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x000C659A File Offset: 0x000C479A
		// (set) Token: 0x06002446 RID: 9286 RVA: 0x000C65A2 File Offset: 0x000C47A2
		internal string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
				this.isExternal = true;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x000C65B2 File Offset: 0x000C47B2
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x000C65BA File Offset: 0x000C47BA
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000C65C3 File Offset: 0x000C47C3
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x000C65CB File Offset: 0x000C47CB
		internal bool IsExternal
		{
			get
			{
				return this.isExternal;
			}
			set
			{
				this.isExternal = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x000C65D4 File Offset: 0x000C47D4
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x000C65DC File Offset: 0x000C47DC
		internal bool DeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000C65E5 File Offset: 0x000C47E5
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x000C65ED File Offset: 0x000C47ED
		internal XmlQualifiedName NData
		{
			get
			{
				return this.ndata;
			}
			set
			{
				this.ndata = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x000C65F6 File Offset: 0x000C47F6
		// (set) Token: 0x06002450 RID: 9296 RVA: 0x000C65FE File Offset: 0x000C47FE
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.isExternal = false;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000C660E File Offset: 0x000C480E
		// (set) Token: 0x06002452 RID: 9298 RVA: 0x000C6616 File Offset: 0x000C4816
		internal int Line
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x000C661F File Offset: 0x000C481F
		// (set) Token: 0x06002454 RID: 9300 RVA: 0x000C6627 File Offset: 0x000C4827
		internal int Pos
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x000C6630 File Offset: 0x000C4830
		// (set) Token: 0x06002456 RID: 9302 RVA: 0x000C6646 File Offset: 0x000C4846
		internal string BaseURI
		{
			get
			{
				if (this.baseURI != null)
				{
					return this.baseURI;
				}
				return string.Empty;
			}
			set
			{
				this.baseURI = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x000C664F File Offset: 0x000C484F
		// (set) Token: 0x06002458 RID: 9304 RVA: 0x000C6657 File Offset: 0x000C4857
		internal bool ParsingInProgress
		{
			get
			{
				return this.parsingInProgress;
			}
			set
			{
				this.parsingInProgress = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x000C6660 File Offset: 0x000C4860
		// (set) Token: 0x0600245A RID: 9306 RVA: 0x000C6676 File Offset: 0x000C4876
		internal string DeclaredURI
		{
			get
			{
				if (this.declaredURI != null)
				{
					return this.declaredURI;
				}
				return string.Empty;
			}
			set
			{
				this.declaredURI = value;
			}
		}

		// Token: 0x04000F24 RID: 3876
		private XmlQualifiedName qname;

		// Token: 0x04000F25 RID: 3877
		private string url;

		// Token: 0x04000F26 RID: 3878
		private string pubid;

		// Token: 0x04000F27 RID: 3879
		private string text;

		// Token: 0x04000F28 RID: 3880
		private XmlQualifiedName ndata = XmlQualifiedName.Empty;

		// Token: 0x04000F29 RID: 3881
		private int lineNumber;

		// Token: 0x04000F2A RID: 3882
		private int linePosition;

		// Token: 0x04000F2B RID: 3883
		private bool isParameter;

		// Token: 0x04000F2C RID: 3884
		private bool isExternal;

		// Token: 0x04000F2D RID: 3885
		private bool parsingInProgress;

		// Token: 0x04000F2E RID: 3886
		private bool isDeclaredInExternal;

		// Token: 0x04000F2F RID: 3887
		private string baseURI;

		// Token: 0x04000F30 RID: 3888
		private string declaredURI;
	}
}
