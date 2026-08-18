using System;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Authentication.ExtendedProtection.Configuration;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C5 RID: 1989
	internal static class ChannelBindingUtility
	{
		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x00112DDD File Offset: 0x00110FDD
		public static ExtendedProtectionPolicy DisabledPolicy
		{
			get
			{
				return ChannelBindingUtility.disabledPolicy;
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x00112DE4 File Offset: 0x00110FE4
		public static ExtendedProtectionPolicy DefaultPolicy
		{
			get
			{
				return ChannelBindingUtility.defaultPolicy;
			}
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00112DEB File Offset: 0x00110FEB
		public static bool IsDefaultPolicy(ExtendedProtectionPolicy policy)
		{
			return policy == ChannelBindingUtility.defaultPolicy;
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x00112DF8 File Offset: 0x00110FF8
		public static void CopyFrom(ExtendedProtectionPolicyElement source, ExtendedProtectionPolicyElement destination)
		{
			destination.PolicyEnforcement = source.PolicyEnforcement;
			destination.ProtectionScenario = source.ProtectionScenario;
			destination.CustomServiceNames.Clear();
			foreach (object obj in source.CustomServiceNames)
			{
				ServiceNameElement serviceNameElement = (ServiceNameElement)obj;
				ServiceNameElement serviceNameElement2 = new ServiceNameElement();
				serviceNameElement2.Name = serviceNameElement.Name;
				destination.CustomServiceNames.Add(serviceNameElement2);
			}
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x00112E8C File Offset: 0x0011108C
		public static void InitializeFrom(ExtendedProtectionPolicy source, ExtendedProtectionPolicyElement destination)
		{
			if (!ChannelBindingUtility.IsDefaultPolicy(source))
			{
				destination.PolicyEnforcement = source.PolicyEnforcement;
				destination.ProtectionScenario = source.ProtectionScenario;
				destination.CustomServiceNames.Clear();
				if (source.CustomServiceNames != null)
				{
					foreach (object obj in source.CustomServiceNames)
					{
						string name = (string)obj;
						ServiceNameElement serviceNameElement = new ServiceNameElement();
						serviceNameElement.Name = name;
						destination.CustomServiceNames.Add(serviceNameElement);
					}
				}
			}
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x00112F2C File Offset: 0x0011112C
		public static ExtendedProtectionPolicy BuildPolicy(ExtendedProtectionPolicyElement configurationPolicy)
		{
			if (configurationPolicy.ElementInformation.IsPresent)
			{
				return configurationPolicy.BuildPolicy();
			}
			return ChannelBindingUtility.DefaultPolicy;
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x00112F47 File Offset: 0x00111147
		public static ChannelBinding GetToken(SslStream stream)
		{
			return ChannelBindingUtility.GetToken(stream.TransportContext);
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00112F54 File Offset: 0x00111154
		public static ChannelBinding GetToken(TransportContext context)
		{
			ChannelBinding result = null;
			if (context != null)
			{
				result = context.GetChannelBinding(ChannelBindingKind.Endpoint);
			}
			return result;
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00112F70 File Offset: 0x00111170
		public static ChannelBinding DuplicateToken(ChannelBinding source)
		{
			if (source == null)
			{
				return null;
			}
			return ChannelBindingUtility.DuplicatedChannelBinding.CreateCopy(source);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00112F80 File Offset: 0x00111180
		public static void TryAddToMessage(ChannelBinding channelBindingToken, Message message, bool messagePropertyOwnsCleanup)
		{
			if (channelBindingToken != null)
			{
				ChannelBindingMessageProperty channelBindingMessageProperty = new ChannelBindingMessageProperty(channelBindingToken, messagePropertyOwnsCleanup);
				channelBindingMessageProperty.AddTo(message);
				channelBindingMessageProperty.Dispose();
			}
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00112FA8 File Offset: 0x001111A8
		public static bool AreEqual(ExtendedProtectionPolicy policy1, ExtendedProtectionPolicy policy2)
		{
			return (policy1.PolicyEnforcement == PolicyEnforcement.Never && policy2.PolicyEnforcement == PolicyEnforcement.Never) || (policy1.PolicyEnforcement == policy2.PolicyEnforcement && policy1.ProtectionScenario == policy2.ProtectionScenario && policy1.CustomChannelBinding == policy2.CustomChannelBinding);
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00112FF8 File Offset: 0x001111F8
		public static bool IsSubset(ServiceNameCollection primaryList, ServiceNameCollection subset)
		{
			bool result;
			if (subset == null || subset.Count == 0)
			{
				result = true;
			}
			else if (primaryList == null || primaryList.Count < subset.Count)
			{
				result = false;
			}
			else
			{
				ServiceNameCollection serviceNameCollection = primaryList.Merge(subset);
				result = (serviceNameCollection.Count == primaryList.Count);
			}
			return result;
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00113044 File Offset: 0x00111244
		public static void Dispose(ref ChannelBinding channelBinding)
		{
			IDisposable disposable = channelBinding;
			channelBinding = null;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x04002F2D RID: 12077
		private static ExtendedProtectionPolicy disabledPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);

		// Token: 0x04002F2E RID: 12078
		private static ExtendedProtectionPolicy defaultPolicy = ChannelBindingUtility.disabledPolicy;

		// Token: 0x02000CFB RID: 3323
		private class DuplicatedChannelBinding : ChannelBinding
		{
			// Token: 0x06007AA9 RID: 31401 RVA: 0x001C8F56 File Offset: 0x001C7156
			private DuplicatedChannelBinding()
			{
			}

			// Token: 0x17001BBF RID: 7103
			// (get) Token: 0x06007AAA RID: 31402 RVA: 0x001C8F5E File Offset: 0x001C715E
			public override int Size
			{
				[SecuritySafeCritical]
				get
				{
					return this.size;
				}
			}

			// Token: 0x06007AAB RID: 31403 RVA: 0x001C8F68 File Offset: 0x001C7168
			[SecuritySafeCritical]
			internal static ChannelBinding CreateCopy(ChannelBinding source)
			{
				if (source.IsInvalid || source.IsClosed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(source.GetType().FullName));
				}
				if (source.Size <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("source.Size", source.Size, SR.GetString("ValueMustBePositive")));
				}
				ChannelBindingUtility.DuplicatedChannelBinding duplicatedChannelBinding = new ChannelBindingUtility.DuplicatedChannelBinding();
				duplicatedChannelBinding.Initialize(source);
				return duplicatedChannelBinding;
			}

			// Token: 0x06007AAC RID: 31404 RVA: 0x001C8FE4 File Offset: 0x001C71E4
			[SecurityCritical]
			private unsafe void Initialize(ChannelBinding source)
			{
				this.AllocateMemory(source.Size);
				byte* ptr = (byte*)source.DangerousGetHandle().ToPointer();
				byte* ptr2 = (byte*)this.handle.ToPointer();
				for (int i = 0; i < source.Size; i++)
				{
					ptr2[i] = ptr[i];
				}
				this.size = source.Size;
			}

			// Token: 0x06007AAD RID: 31405 RVA: 0x001C9040 File Offset: 0x001C7240
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			private void AllocateMemory(int bytesToAllocate)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					base.SetHandle(Marshal.AllocHGlobal(bytesToAllocate));
				}
			}

			// Token: 0x06007AAE RID: 31406 RVA: 0x001C9074 File Offset: 0x001C7274
			protected override bool ReleaseHandle()
			{
				Marshal.FreeHGlobal(this.handle);
				base.SetHandle(IntPtr.Zero);
				return true;
			}

			// Token: 0x04004626 RID: 17958
			[SecurityCritical]
			private int size;
		}
	}
}
