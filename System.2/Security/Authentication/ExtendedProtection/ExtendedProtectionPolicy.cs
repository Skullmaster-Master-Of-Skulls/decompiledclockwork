using System;
using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x02000442 RID: 1090
	[TypeConverter(typeof(ExtendedProtectionPolicyTypeConverter))]
	[Serializable]
	public class ExtendedProtectionPolicy : ISerializable
	{
		// Token: 0x0600288E RID: 10382 RVA: 0x000BA300 File Offset: 0x000B8500
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ProtectionScenario protectionScenario, ServiceNameCollection customServiceNames)
		{
			if (policyEnforcement == PolicyEnforcement.Never)
			{
				throw new ArgumentException(SR.GetString("security_ExtendedProtectionPolicy_UseDifferentConstructorForNever"), "policyEnforcement");
			}
			if (customServiceNames != null && customServiceNames.Count == 0)
			{
				throw new ArgumentException(SR.GetString("security_ExtendedProtectionPolicy_NoEmptyServiceNameCollection"), "customServiceNames");
			}
			this.policyEnforcement = policyEnforcement;
			this.protectionScenario = protectionScenario;
			this.customServiceNames = customServiceNames;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000BA360 File Offset: 0x000B8560
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ProtectionScenario protectionScenario, ICollection customServiceNames) : this(policyEnforcement, protectionScenario, (customServiceNames == null) ? null : new ServiceNameCollection(customServiceNames))
		{
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000BA378 File Offset: 0x000B8578
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement, ChannelBinding customChannelBinding)
		{
			if (policyEnforcement == PolicyEnforcement.Never)
			{
				throw new ArgumentException(SR.GetString("security_ExtendedProtectionPolicy_UseDifferentConstructorForNever"), "policyEnforcement");
			}
			if (customChannelBinding == null)
			{
				throw new ArgumentNullException("customChannelBinding");
			}
			this.policyEnforcement = policyEnforcement;
			this.protectionScenario = ProtectionScenario.TransportSelected;
			this.customChannelBinding = customChannelBinding;
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000BA3C6 File Offset: 0x000B85C6
		public ExtendedProtectionPolicy(PolicyEnforcement policyEnforcement)
		{
			this.policyEnforcement = policyEnforcement;
			this.protectionScenario = ProtectionScenario.TransportSelected;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000BA3DC File Offset: 0x000B85DC
		protected ExtendedProtectionPolicy(SerializationInfo info, StreamingContext context)
		{
			this.policyEnforcement = (PolicyEnforcement)info.GetInt32("policyEnforcement");
			this.protectionScenario = (ProtectionScenario)info.GetInt32("protectionScenario");
			this.customServiceNames = (ServiceNameCollection)info.GetValue("customServiceNames", typeof(ServiceNameCollection));
			byte[] array = (byte[])info.GetValue("customChannelBinding", typeof(byte[]));
			if (array != null)
			{
				this.customChannelBinding = SafeLocalFreeChannelBinding.LocalAlloc(array.Length);
				Marshal.Copy(array, 0, this.customChannelBinding.DangerousGetHandle(), array.Length);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000BA472 File Offset: 0x000B8672
		public ServiceNameCollection CustomServiceNames
		{
			get
			{
				return this.customServiceNames;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000BA47A File Offset: 0x000B867A
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return this.policyEnforcement;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000BA482 File Offset: 0x000B8682
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return this.protectionScenario;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x000BA48A File Offset: 0x000B868A
		public ChannelBinding CustomChannelBinding
		{
			get
			{
				return this.customChannelBinding;
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000BA494 File Offset: 0x000B8694
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ProtectionScenario=");
			stringBuilder.Append(this.protectionScenario.ToString());
			stringBuilder.Append("; PolicyEnforcement=");
			stringBuilder.Append(this.policyEnforcement.ToString());
			stringBuilder.Append("; CustomChannelBinding=");
			if (this.customChannelBinding == null)
			{
				stringBuilder.Append("<null>");
			}
			else
			{
				stringBuilder.Append(this.customChannelBinding.ToString());
			}
			stringBuilder.Append("; ServiceNames=");
			if (this.customServiceNames == null)
			{
				stringBuilder.Append("<null>");
			}
			else
			{
				bool flag = true;
				foreach (object obj in this.customServiceNames)
				{
					string value = (string)obj;
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x000BA5B0 File Offset: 0x000B87B0
		public static bool OSSupportsExtendedProtection
		{
			get
			{
				return AuthenticationManager.OSSupportsExtendedProtection;
			}
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000BA5B8 File Offset: 0x000B87B8
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("policyEnforcement", (int)this.policyEnforcement);
			info.AddValue("protectionScenario", (int)this.protectionScenario);
			info.AddValue("customServiceNames", this.customServiceNames, typeof(ServiceNameCollection));
			if (this.customChannelBinding == null)
			{
				info.AddValue("customChannelBinding", null, typeof(byte[]));
				return;
			}
			byte[] array = new byte[this.customChannelBinding.Size];
			Marshal.Copy(this.customChannelBinding.DangerousGetHandle(), array, 0, this.customChannelBinding.Size);
			info.AddValue("customChannelBinding", array, typeof(byte[]));
		}

		// Token: 0x04002263 RID: 8803
		private const string policyEnforcementName = "policyEnforcement";

		// Token: 0x04002264 RID: 8804
		private const string protectionScenarioName = "protectionScenario";

		// Token: 0x04002265 RID: 8805
		private const string customServiceNamesName = "customServiceNames";

		// Token: 0x04002266 RID: 8806
		private const string customChannelBindingName = "customChannelBinding";

		// Token: 0x04002267 RID: 8807
		private ServiceNameCollection customServiceNames;

		// Token: 0x04002268 RID: 8808
		private PolicyEnforcement policyEnforcement;

		// Token: 0x04002269 RID: 8809
		private ProtectionScenario protectionScenario;

		// Token: 0x0400226A RID: 8810
		private ChannelBinding customChannelBinding;
	}
}
