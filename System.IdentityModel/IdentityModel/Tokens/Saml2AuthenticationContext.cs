using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000138 RID: 312
	public class Saml2AuthenticationContext
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x000248D8 File Offset: 0x00022AD8
		public Saml2AuthenticationContext() : this(null, null)
		{
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000248E2 File Offset: 0x00022AE2
		public Saml2AuthenticationContext(Uri classReference) : this(classReference, null)
		{
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000248EC File Offset: 0x00022AEC
		public Saml2AuthenticationContext(Uri classReference, Uri declarationReference)
		{
			if (null != classReference && !classReference.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("classReference", SR.GetString("ID0013"));
			}
			if (null != declarationReference && !declarationReference.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("declarationReference", SR.GetString("ID0013"));
			}
			this.classReference = classReference;
			this.declarationReference = declarationReference;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x0002496E File Offset: 0x00022B6E
		public Collection<Uri> AuthenticatingAuthorities
		{
			get
			{
				return this.authenticatingAuthorities;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00024976 File Offset: 0x00022B76
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0002497E File Offset: 0x00022B7E
		public Uri ClassReference
		{
			get
			{
				return this.classReference;
			}
			set
			{
				if (null != value && !value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID0013"));
				}
				this.classReference = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000249B2 File Offset: 0x00022BB2
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x000249BA File Offset: 0x00022BBA
		public Uri DeclarationReference
		{
			get
			{
				return this.declarationReference;
			}
			set
			{
				if (null != value && !value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID0013"));
				}
				this.declarationReference = value;
			}
		}

		// Token: 0x04000B3E RID: 2878
		private Collection<Uri> authenticatingAuthorities = new AbsoluteUriCollection();

		// Token: 0x04000B3F RID: 2879
		private Uri classReference;

		// Token: 0x04000B40 RID: 2880
		private Uri declarationReference;
	}
}
