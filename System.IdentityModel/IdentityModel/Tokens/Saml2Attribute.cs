using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000135 RID: 309
	public class Saml2Attribute
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x00024514 File Offset: 0x00022714
		public Saml2Attribute(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			this.name = StringUtil.OptimizeString(name);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00024564 File Offset: 0x00022764
		public Saml2Attribute(string name, IEnumerable<string> values) : this(name)
		{
			if (values == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("values");
			}
			foreach (string item in values)
			{
				this.values.Add(item);
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x000245CC File Offset: 0x000227CC
		public Saml2Attribute(string name, string value) : this(name, new string[]
		{
			value
		})
		{
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000245DF File Offset: 0x000227DF
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x000245E7 File Offset: 0x000227E7
		public string FriendlyName
		{
			get
			{
				return this.friendlyName;
			}
			set
			{
				this.friendlyName = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000245F5 File Offset: 0x000227F5
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x000245FD File Offset: 0x000227FD
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.name = StringUtil.OptimizeString(value);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00024628 File Offset: 0x00022828
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00024630 File Offset: 0x00022830
		public Uri NameFormat
		{
			get
			{
				return this.nameFormat;
			}
			set
			{
				if (null != value && !value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("error", SR.GetString("ID0013"));
				}
				this.nameFormat = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00024664 File Offset: 0x00022864
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0002466C File Offset: 0x0002286C
		public string OriginalIssuer
		{
			get
			{
				return this.originalIssuer;
			}
			set
			{
				if (value == string.Empty)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4251"));
				}
				this.originalIssuer = StringUtil.OptimizeString(value);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000246A1 File Offset: 0x000228A1
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x000246AC File Offset: 0x000228AC
		public string AttributeValueXsiType
		{
			get
			{
				return this.attributeValueXsiType;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				int num = value.IndexOf('#');
				if (num == -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				string text = value.Substring(0, num);
				if (text.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				string text2 = value.Substring(num + 1);
				if (text2.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4254"));
				}
				this.attributeValueXsiType = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00024760 File Offset: 0x00022960
		public Collection<string> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x04000B36 RID: 2870
		private string friendlyName;

		// Token: 0x04000B37 RID: 2871
		private string name;

		// Token: 0x04000B38 RID: 2872
		private Uri nameFormat;

		// Token: 0x04000B39 RID: 2873
		private Collection<string> values = new Collection<string>();

		// Token: 0x04000B3A RID: 2874
		private string originalIssuer;

		// Token: 0x04000B3B RID: 2875
		private string attributeValueXsiType = "http://www.w3.org/2001/XMLSchema#string";
	}
}
