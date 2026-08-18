using System;
using System.ServiceModel.Description;

namespace System.ServiceModel
{
	// Token: 0x02000124 RID: 292
	[__DynamicallyInvokable]
	public class FaultCode
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0002098F File Offset: 0x0001EB8F
		[__DynamicallyInvokable]
		public FaultCode(string name) : this(name, "", null)
		{
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0002099E File Offset: 0x0001EB9E
		[__DynamicallyInvokable]
		public FaultCode(string name, FaultCode subCode) : this(name, "", subCode)
		{
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000209AD File Offset: 0x0001EBAD
		[__DynamicallyInvokable]
		public FaultCode(string name, string ns) : this(name, ns, null)
		{
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000209B8 File Offset: 0x0001EBB8
		[__DynamicallyInvokable]
		public FaultCode(string name, string ns, FaultCode subCode)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (name.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("name"));
			}
			if (!string.IsNullOrEmpty(ns))
			{
				NamingHelper.CheckUriParameter(ns, "ns");
			}
			this.name = name;
			this.ns = ns;
			this.subCode = subCode;
			if (ns == "http://www.w3.org/2003/05/soap-envelope")
			{
				this.version = EnvelopeVersion.Soap12;
				return;
			}
			if (ns == "http://schemas.xmlsoap.org/soap/envelope/")
			{
				this.version = EnvelopeVersion.Soap11;
				return;
			}
			if (ns == "http://schemas.microsoft.com/ws/2005/05/envelope/none")
			{
				this.version = EnvelopeVersion.None;
				return;
			}
			this.version = null;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00020A7A File Offset: 0x0001EC7A
		[__DynamicallyInvokable]
		public bool IsPredefinedFault
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns.Length == 0 || this.version != null;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00020A94 File Offset: 0x0001EC94
		[__DynamicallyInvokable]
		public bool IsSenderFault
		{
			[__DynamicallyInvokable]
			get
			{
				return this.IsPredefinedFault && this.name == (this.version ?? EnvelopeVersion.Soap12).SenderFaultName;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00020ABF File Offset: 0x0001ECBF
		[__DynamicallyInvokable]
		public bool IsReceiverFault
		{
			[__DynamicallyInvokable]
			get
			{
				return this.IsPredefinedFault && this.name == (this.version ?? EnvelopeVersion.Soap12).ReceiverFaultName;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00020AEA File Offset: 0x0001ECEA
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00020AF2 File Offset: 0x0001ECF2
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00020AFA File Offset: 0x0001ECFA
		[__DynamicallyInvokable]
		public FaultCode SubCode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.subCode;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00020B02 File Offset: 0x0001ED02
		[__DynamicallyInvokable]
		public static FaultCode CreateSenderFaultCode(FaultCode subCode)
		{
			return new FaultCode("Sender", subCode);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00020B0F File Offset: 0x0001ED0F
		public static FaultCode CreateSenderFaultCode(string name, string ns)
		{
			return FaultCode.CreateSenderFaultCode(new FaultCode(name, ns));
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00020B1D File Offset: 0x0001ED1D
		public static FaultCode CreateReceiverFaultCode(FaultCode subCode)
		{
			if (subCode == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("subCode"));
			}
			return new FaultCode("Receiver", subCode);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00020B42 File Offset: 0x0001ED42
		public static FaultCode CreateReceiverFaultCode(string name, string ns)
		{
			return FaultCode.CreateReceiverFaultCode(new FaultCode(name, ns));
		}

		// Token: 0x04000AF0 RID: 2800
		private FaultCode subCode;

		// Token: 0x04000AF1 RID: 2801
		private string name;

		// Token: 0x04000AF2 RID: 2802
		private string ns;

		// Token: 0x04000AF3 RID: 2803
		private EnvelopeVersion version;
	}
}
