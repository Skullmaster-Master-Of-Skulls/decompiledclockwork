using System;
using System.CodeDom;
using System.Diagnostics;
using System.Net.Security;
using System.ServiceModel.Security;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C7 RID: 967
	[DebuggerDisplay("Name={name}, Action={action}, DetailType={detailType}")]
	[__DynamicallyInvokable]
	public class FaultDescription
	{
		// Token: 0x06002469 RID: 9321 RVA: 0x000841D9 File Offset: 0x000823D9
		[__DynamicallyInvokable]
		public FaultDescription(string action)
		{
			if (action == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("action"));
			}
			this.action = action;
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x00084200 File Offset: 0x00082400
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x00084208 File Offset: 0x00082408
		[__DynamicallyInvokable]
		public string Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this.action;
			}
			internal set
			{
				this.action = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x00084211 File Offset: 0x00082411
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x00084219 File Offset: 0x00082419
		[__DynamicallyInvokable]
		public Type DetailType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.detailType;
			}
			[__DynamicallyInvokable]
			set
			{
				this.detailType = value;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x00084222 File Offset: 0x00082422
		// (set) Token: 0x0600246F RID: 9327 RVA: 0x0008422A File Offset: 0x0008242A
		internal CodeTypeReference DetailTypeReference
		{
			get
			{
				return this.detailTypeReference;
			}
			set
			{
				this.detailTypeReference = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x00084233 File Offset: 0x00082433
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x00084240 File Offset: 0x00082440
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name.EncodedName;
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetNameAndElement(new XmlName(value, true));
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x0008424F File Offset: 0x0008244F
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x00084257 File Offset: 0x00082457
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x00084260 File Offset: 0x00082460
		// (set) Token: 0x06002475 RID: 9333 RVA: 0x00084268 File Offset: 0x00082468
		internal XmlName ElementName
		{
			get
			{
				return this.elementName;
			}
			set
			{
				this.elementName = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x00084271 File Offset: 0x00082471
		// (set) Token: 0x06002477 RID: 9335 RVA: 0x00084279 File Offset: 0x00082479
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.protectionLevel = value;
				this.hasProtectionLevel = true;
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000842A6 File Offset: 0x000824A6
		public bool ShouldSerializeProtectionLevel()
		{
			return this.HasProtectionLevel;
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x000842AE File Offset: 0x000824AE
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000842B6 File Offset: 0x000824B6
		internal void ResetProtectionLevel()
		{
			this.protectionLevel = ProtectionLevel.None;
			this.hasProtectionLevel = false;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000842C8 File Offset: 0x000824C8
		internal void SetNameAndElement(XmlName name)
		{
			this.name = name;
			this.elementName = name;
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000842E5 File Offset: 0x000824E5
		internal void SetNameOnly(XmlName name)
		{
			this.name = name;
		}

		// Token: 0x04002071 RID: 8305
		private string action;

		// Token: 0x04002072 RID: 8306
		private Type detailType;

		// Token: 0x04002073 RID: 8307
		private CodeTypeReference detailTypeReference;

		// Token: 0x04002074 RID: 8308
		private XmlName elementName;

		// Token: 0x04002075 RID: 8309
		private XmlName name;

		// Token: 0x04002076 RID: 8310
		private string ns;

		// Token: 0x04002077 RID: 8311
		private ProtectionLevel protectionLevel;

		// Token: 0x04002078 RID: 8312
		private bool hasProtectionLevel;
	}
}
