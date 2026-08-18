using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.PeerResolvers;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A56 RID: 2646
	internal sealed class PnrpPeerResolver : PeerResolver
	{
		// Token: 0x06006863 RID: 26723 RVA: 0x00185498 File Offset: 0x00183698
		static PnrpPeerResolver()
		{
			PnrpPeerResolver.isPnrpAvailable = false;
			using (PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase discoveryBase = new PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase())
			{
				PnrpPeerResolver.isPnrpInstalled = discoveryBase.IsPnrpInstalled();
				PnrpPeerResolver.isPnrpAvailable = discoveryBase.IsPnrpAvailable(PnrpPeerResolver.TimeToWaitForStatus);
			}
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x0018552C File Offset: 0x0018372C
		internal PnrpPeerResolver() : this(PeerReferralPolicy.Share)
		{
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x00185535 File Offset: 0x00183735
		internal PnrpPeerResolver(PeerReferralPolicy referralPolicy)
		{
			this.referralPolicy = referralPolicy;
		}

		// Token: 0x170018F9 RID: 6393
		// (get) Token: 0x06006866 RID: 26726 RVA: 0x00185556 File Offset: 0x00183756
		private static Encoding PnrpEncoder
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x170018FA RID: 6394
		// (get) Token: 0x06006867 RID: 26727 RVA: 0x0018555D File Offset: 0x0018375D
		public static bool IsPnrpAvailable
		{
			get
			{
				return PnrpPeerResolver.isPnrpAvailable;
			}
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06006868 RID: 26728 RVA: 0x00185564 File Offset: 0x00183764
		public static bool IsPnrpInstalled
		{
			get
			{
				return PnrpPeerResolver.isPnrpInstalled;
			}
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x0018556C File Offset: 0x0018376C
		public static IPEndPoint GetHint()
		{
			byte[] array = new byte[16];
			object sharedLock = PnrpPeerResolver.SharedLock;
			lock (sharedLock)
			{
				PnrpPeerResolver.randomGenerator.NextBytes(array);
			}
			return new IPEndPoint(new IPAddress(array), 0);
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x001855C4 File Offset: 0x001837C4
		public static bool HasPeerNodeForMesh(string meshId)
		{
			PeerNodeImplementation peerNodeImplementation = null;
			return PeerNodeImplementation.TryGet(meshId, out peerNodeImplementation);
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x001855DB File Offset: 0x001837DB
		internal void SetMeshExtensions(string local, string remote)
		{
			this.localExtension = local;
			this.remoteExtension = remote;
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x001855EC File Offset: 0x001837EC
		internal PnrpPeerResolver.PnrpResolveScope EnumerateClouds(bool forResolve, Dictionary<uint, string> LinkCloudNames, Dictionary<uint, string> SiteCloudNames)
		{
			bool flag = false;
			PnrpPeerResolver.PnrpResolveScope pnrpResolveScope = PnrpPeerResolver.PnrpResolveScope.None;
			LinkCloudNames.Clear();
			SiteCloudNames.Clear();
			PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo[] clouds = PnrpPeerResolver.UnsafePnrpNativeMethods.PeerCloudEnumerator.GetClouds();
			if (forResolve)
			{
				foreach (PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo cloudInfo in clouds)
				{
					if (cloudInfo.State == PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudState.Active)
					{
						if (cloudInfo.Scope == PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope.Global)
						{
							pnrpResolveScope |= PnrpPeerResolver.PnrpResolveScope.Global;
							flag = true;
						}
						else if (cloudInfo.Scope == PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope.LinkLocal)
						{
							LinkCloudNames.Add(cloudInfo.ScopeId, cloudInfo.Name);
							pnrpResolveScope |= PnrpPeerResolver.PnrpResolveScope.LinkLocal;
							flag = true;
						}
						else if (cloudInfo.Scope == PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope.SiteLocal)
						{
							SiteCloudNames.Add(cloudInfo.ScopeId, cloudInfo.Name);
							pnrpResolveScope |= PnrpPeerResolver.PnrpResolveScope.SiteLocal;
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				foreach (PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo cloudInfo2 in clouds)
				{
					if (cloudInfo2.State != PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudState.Dead && cloudInfo2.State != PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudState.Disabled && cloudInfo2.State != PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudState.NoNet)
					{
						if (cloudInfo2.Scope == PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope.Global)
						{
							pnrpResolveScope |= PnrpPeerResolver.PnrpResolveScope.Global;
						}
						else if (cloudInfo2.Scope == PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope.LinkLocal)
						{
							LinkCloudNames.Add(cloudInfo2.ScopeId, cloudInfo2.Name);
							pnrpResolveScope |= PnrpPeerResolver.PnrpResolveScope.LinkLocal;
						}
						else if (cloudInfo2.Scope == PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope.SiteLocal)
						{
							SiteCloudNames.Add(cloudInfo2.ScopeId, cloudInfo2.Name);
							pnrpResolveScope |= PnrpPeerResolver.PnrpResolveScope.SiteLocal;
						}
					}
				}
			}
			return pnrpResolveScope;
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x0600686D RID: 26733 RVA: 0x00185733 File Offset: 0x00183933
		public override bool CanShareReferrals
		{
			get
			{
				return this.referralPolicy != PeerReferralPolicy.DoNotShare;
			}
		}

		// Token: 0x0600686E RID: 26734 RVA: 0x00185744 File Offset: 0x00183944
		public override object Register(string meshId, PeerNodeAddress nodeAddress, TimeSpan timeout)
		{
			this.ThrowIfNoPnrp();
			PnrpPeerResolver.PnrpRegistration pnrpRegistration = null;
			PnrpPeerResolver.PnrpRegistration[] array = null;
			PnrpPeerResolver.PnrpRegistration[] array2 = null;
			PnrpPeerResolver.RegistrationHandle registrationHandle = new PnrpPeerResolver.RegistrationHandle(meshId);
			Dictionary<uint, string> siteCloudNames = new Dictionary<uint, string>();
			Dictionary<uint, string> linkCloudNames = new Dictionary<uint, string>();
			PnrpPeerResolver.PnrpResolveScope pnrpResolveScope = this.EnumerateClouds(false, linkCloudNames, siteCloudNames);
			if (pnrpResolveScope == PnrpPeerResolver.PnrpResolveScope.None)
			{
				PeerExceptionHelper.ThrowInvalidOperation_PnrpNoClouds();
			}
			if (this.localExtension != null)
			{
				meshId += this.localExtension;
			}
			try
			{
				this.PeerNodeAddressToPnrpRegistrations(meshId, linkCloudNames, siteCloudNames, nodeAddress, out array, out array2, out pnrpRegistration);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerPnrpIllegalUri"), ex));
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			try
			{
				PnrpPeerResolver.PnrpResolveScope pnrpResolveScope2 = PnrpPeerResolver.PnrpResolveScope.None;
				if (pnrpRegistration != null && pnrpRegistration.Addresses.Length != 0 && (pnrpResolveScope & PnrpPeerResolver.PnrpResolveScope.Global) != PnrpPeerResolver.PnrpResolveScope.None)
				{
					this.registrar.Register(pnrpRegistration, timeoutHelper.RemainingTime());
					registrationHandle.AddCloud(pnrpRegistration.CloudName);
					pnrpResolveScope2 |= PnrpPeerResolver.PnrpResolveScope.Global;
				}
				if (array.Length != 0)
				{
					foreach (PnrpPeerResolver.PnrpRegistration pnrpRegistration2 in array)
					{
						if (pnrpRegistration2.Addresses.Length != 0)
						{
							this.registrar.Register(pnrpRegistration2, timeoutHelper.RemainingTime());
							registrationHandle.AddCloud(pnrpRegistration2.CloudName);
						}
					}
					pnrpResolveScope2 |= PnrpPeerResolver.PnrpResolveScope.LinkLocal;
				}
				if (array2.Length != 0)
				{
					foreach (PnrpPeerResolver.PnrpRegistration pnrpRegistration3 in array2)
					{
						if (pnrpRegistration3.Addresses.Length != 0)
						{
							this.registrar.Register(pnrpRegistration3, timeoutHelper.RemainingTime());
							registrationHandle.AddCloud(pnrpRegistration3.CloudName);
						}
					}
					pnrpResolveScope2 |= PnrpPeerResolver.PnrpResolveScope.SiteLocal;
				}
				if (pnrpResolveScope2 == PnrpPeerResolver.PnrpResolveScope.None)
				{
					PeerExceptionHelper.ThrowInvalidOperation_PnrpAddressesUnsupported();
				}
			}
			catch (SocketException)
			{
				try
				{
					this.Unregister(registrationHandle, timeoutHelper.RemainingTime());
				}
				catch (SocketException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				throw;
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				PnrpRegisterTraceRecord extendedData = new PnrpRegisterTraceRecord(meshId, pnrpRegistration, array2, array);
				TraceUtility.TraceEvent(TraceEventType.Information, 262216, SR.GetString("TraceCodePnrpRegisteredAddresses"), extendedData, this, null);
			}
			return registrationHandle;
		}

		// Token: 0x0600686F RID: 26735 RVA: 0x00185940 File Offset: 0x00183B40
		private void ThrowIfNoPnrp()
		{
			if (!PnrpPeerResolver.isPnrpAvailable)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerPnrpNotAvailable")));
			}
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x00185964 File Offset: 0x00183B64
		public override void Unregister(object registrationId, TimeSpan timeout)
		{
			PnrpPeerResolver.RegistrationHandle registrationHandle = registrationId as PnrpPeerResolver.RegistrationHandle;
			if (registrationHandle == null || string.IsNullOrEmpty(registrationHandle.PeerName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerInvalidRegistrationId", new object[]
				{
					registrationHandle
				}), "registrationId"));
			}
			string peerName = registrationHandle.PeerName;
			string peerName2 = string.Format(CultureInfo.InvariantCulture, "0.{0}", new object[]
			{
				peerName
			});
			this.registrar.Unregister(peerName2, registrationHandle.Clouds, timeout);
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				PnrpPeerResolverTraceRecord extendedData = new PnrpPeerResolverTraceRecord(peerName, new List<PeerNodeAddress>());
				TraceUtility.TraceEvent(TraceEventType.Information, 262217, SR.GetString("TraceCodePnrpUnregisteredAddresses"), extendedData, this, null);
			}
		}

		// Token: 0x06006871 RID: 26737 RVA: 0x00185A10 File Offset: 0x00183C10
		public override void Update(object registrationId, PeerNodeAddress updatedNodeAddress, TimeSpan timeout)
		{
			PnrpPeerResolver.RegistrationHandle registrationHandle = registrationId as PnrpPeerResolver.RegistrationHandle;
			if (registrationHandle == null || string.IsNullOrEmpty(registrationHandle.PeerName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerInvalidRegistrationId", new object[]
				{
					registrationHandle
				}), "registrationId"));
			}
			string peerName = registrationHandle.PeerName;
			this.Register(peerName, updatedNodeAddress, timeout);
		}

		// Token: 0x06006872 RID: 26738 RVA: 0x00185A70 File Offset: 0x00183C70
		private PeerNodeAddress PeerNodeAddressFromPnrpRegistration(PnrpPeerResolver.PnrpRegistration input)
		{
			List<IPAddress> addresses = new List<IPAddress>();
			PeerNodeAddress result = null;
			StringBuilder pathBuilder = new StringBuilder(200);
			int num = 0;
			try
			{
				if (input == null || string.IsNullOrEmpty(input.Comment))
				{
					return null;
				}
				Array.ForEach<IPEndPoint>(input.Addresses, delegate(IPEndPoint obj)
				{
					addresses.Add(obj.Address);
				});
				if (addresses.Count != 0)
				{
					UriBuilder uriBuilder = new UriBuilder();
					uriBuilder.Port = input.Addresses[0].Port;
					uriBuilder.Host = addresses[0].ToString();
					pathBuilder.Append("PeerChannelEndpoints");
					string scheme;
					Guid[] array;
					PnrpPeerResolver.CharEncoder.Decode(input.Comment, out num, out scheme, out array);
					if (num == 1 && array != null && array.Length <= 2 && array.Length >= 1)
					{
						uriBuilder.Scheme = scheme;
						Array.ForEach<Guid>(array, delegate(Guid guid)
						{
							pathBuilder.Append("/" + string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
							{
								guid.ToString()
							}));
						});
						uriBuilder.Path = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
						{
							pathBuilder.ToString()
						});
						result = new PeerNodeAddress(new EndpointAddress(uriBuilder.Uri, new AddressHeader[0]), new ReadOnlyCollection<IPAddress>(addresses));
					}
				}
			}
			catch (ArgumentException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (FormatException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (IndexOutOfRangeException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
			}
			return result;
		}

		// Token: 0x06006873 RID: 26739 RVA: 0x00185C28 File Offset: 0x00183E28
		private void TrimToMaxAddresses(List<IPEndPoint> addressList)
		{
			if (addressList.Count > 10)
			{
				addressList.RemoveRange(10, addressList.Count - 10);
			}
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x00185C48 File Offset: 0x00183E48
		private void PeerNodeAddressToPnrpRegistrations(string meshName, Dictionary<uint, string> LinkCloudNames, Dictionary<uint, string> SiteCloudNames, PeerNodeAddress input, out PnrpPeerResolver.PnrpRegistration[] linkRegs, out PnrpPeerResolver.PnrpRegistration[] siteRegs, out PnrpPeerResolver.PnrpRegistration global)
		{
			PnrpPeerResolver.PnrpRegistration pnrpRegistration = new PnrpPeerResolver.PnrpRegistration();
			Dictionary<uint, PnrpPeerResolver.PnrpRegistration> dictionary = new Dictionary<uint, PnrpPeerResolver.PnrpRegistration>();
			Dictionary<uint, PnrpPeerResolver.PnrpRegistration> dictionary2 = new Dictionary<uint, PnrpPeerResolver.PnrpRegistration>();
			PnrpPeerResolver.PnrpRegistration pnrpRegistration2 = null;
			string protocolName;
			Guid[] guids;
			this.ParseServiceUri(input.EndpointAddress.Uri, out protocolName, out guids);
			int num = input.EndpointAddress.Uri.Port;
			if (num <= 0)
			{
				num = 808;
			}
			string peerName = string.Format(CultureInfo.InvariantCulture, "0.{0}", new object[]
			{
				meshName
			});
			string comment = PnrpPeerResolver.CharEncoder.Encode(1, protocolName, guids);
			global = null;
			string empty = string.Empty;
			foreach (IPAddress ipaddress in input.IPAddresses)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6 && (ipaddress.IsIPv6LinkLocal || ipaddress.IsIPv6SiteLocal))
				{
					if (ipaddress.IsIPv6LinkLocal)
					{
						if (!dictionary.TryGetValue((uint)ipaddress.ScopeId, out pnrpRegistration2))
						{
							if (!LinkCloudNames.TryGetValue((uint)ipaddress.ScopeId, out empty))
							{
								continue;
							}
							pnrpRegistration2 = PnrpPeerResolver.PnrpRegistration.Create(peerName, comment, empty);
							dictionary.Add((uint)ipaddress.ScopeId, pnrpRegistration2);
						}
					}
					else if (!dictionary2.TryGetValue((uint)ipaddress.ScopeId, out pnrpRegistration2))
					{
						if (!SiteCloudNames.TryGetValue((uint)ipaddress.ScopeId, out empty))
						{
							continue;
						}
						pnrpRegistration2 = PnrpPeerResolver.PnrpRegistration.Create(peerName, comment, empty);
						dictionary2.Add((uint)ipaddress.ScopeId, pnrpRegistration2);
					}
					pnrpRegistration2.addressList.Add(new IPEndPoint(ipaddress, num));
				}
				else
				{
					if (global == null)
					{
						global = PnrpPeerResolver.PnrpRegistration.Create(peerName, comment, "Global_");
					}
					global.addressList.Add(new IPEndPoint(ipaddress, num));
				}
			}
			if (global != null)
			{
				if (global.addressList != null)
				{
					this.TrimToMaxAddresses(global.addressList);
					global.Addresses = global.addressList.ToArray();
				}
				else
				{
					global.Addresses = new IPEndPoint[0];
				}
			}
			if (dictionary.Count != 0)
			{
				foreach (PnrpPeerResolver.PnrpRegistration pnrpRegistration3 in dictionary.Values)
				{
					if (pnrpRegistration3.addressList != null)
					{
						this.TrimToMaxAddresses(pnrpRegistration3.addressList);
						pnrpRegistration3.Addresses = pnrpRegistration3.addressList.ToArray();
					}
					else
					{
						pnrpRegistration3.Addresses = new IPEndPoint[0];
					}
				}
				linkRegs = new PnrpPeerResolver.PnrpRegistration[dictionary.Count];
				dictionary.Values.CopyTo(linkRegs, 0);
			}
			else
			{
				linkRegs = new PnrpPeerResolver.PnrpRegistration[0];
			}
			if (dictionary2.Count != 0)
			{
				foreach (PnrpPeerResolver.PnrpRegistration pnrpRegistration4 in dictionary2.Values)
				{
					if (pnrpRegistration4.addressList != null)
					{
						this.TrimToMaxAddresses(pnrpRegistration4.addressList);
						pnrpRegistration4.Addresses = pnrpRegistration4.addressList.ToArray();
					}
					else
					{
						pnrpRegistration4.Addresses = new IPEndPoint[0];
					}
				}
				siteRegs = new PnrpPeerResolver.PnrpRegistration[dictionary2.Count];
				dictionary2.Values.CopyTo(siteRegs, 0);
				return;
			}
			siteRegs = new PnrpPeerResolver.PnrpRegistration[0];
		}

		// Token: 0x06006875 RID: 26741 RVA: 0x00185FBC File Offset: 0x001841BC
		private static int ProtocolFromName(string name)
		{
			if (name == Uri.UriSchemeNetTcp)
			{
				return 1;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("name", SR.GetString("PeerPnrpIllegalUri"));
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x00185FE6 File Offset: 0x001841E6
		private static string NameFromProtocol(byte number)
		{
			if (number == 1)
			{
				return Uri.UriSchemeNetTcp;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerPnrpIllegalUri")));
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x0018600C File Offset: 0x0018420C
		private void ParseServiceUri(Uri uri, out string scheme, out Guid[] result)
		{
			if (uri != null && PnrpPeerResolver.ProtocolFromName(uri.Scheme) != 0 && !string.IsNullOrEmpty(uri.AbsolutePath))
			{
				scheme = uri.Scheme;
				string[] array = uri.AbsolutePath.Trim(new char[]
				{
					' ',
					'/'
				}).Split(new char[]
				{
					'/'
				});
				if (string.Compare(array[0], "PeerChannelEndpoints", StringComparison.OrdinalIgnoreCase) == 0 && array.Length >= 1 && array.Length <= 3)
				{
					result = new Guid[array.Length - 1];
					try
					{
						for (int i = 1; i < array.Length; i++)
						{
							result[i - 1] = Fx.CreateGuid(array[i]);
						}
						return;
					}
					catch (FormatException innerException)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerPnrpIllegalUri"), innerException));
					}
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerPnrpIllegalUri")));
		}

		// Token: 0x06006878 RID: 26744 RVA: 0x0018610C File Offset: 0x0018430C
		private void MergeResults(Dictionary<string, PnrpPeerResolver.PnrpRegistration> results, List<PnrpPeerResolver.PnrpRegistration> regs)
		{
			PnrpPeerResolver.PnrpRegistration pnrpRegistration = null;
			foreach (PnrpPeerResolver.PnrpRegistration pnrpRegistration2 in regs)
			{
				if (!results.TryGetValue(pnrpRegistration2.Comment, out pnrpRegistration))
				{
					pnrpRegistration = pnrpRegistration2;
					results.Add(pnrpRegistration2.Comment, pnrpRegistration2);
					pnrpRegistration.addressList = new List<IPEndPoint>();
				}
				pnrpRegistration.addressList.AddRange(pnrpRegistration2.Addresses);
				pnrpRegistration2.Addresses = null;
			}
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x00186198 File Offset: 0x00184398
		private void MergeResults(List<PeerNodeAddress> nodeAddressList, List<PnrpPeerResolver.PnrpRegistration> globalRegistrations, List<PnrpPeerResolver.PnrpRegistration> linkRegistrations, List<PnrpPeerResolver.PnrpRegistration> siteRegistrations)
		{
			Dictionary<string, PnrpPeerResolver.PnrpRegistration> dictionary = new Dictionary<string, PnrpPeerResolver.PnrpRegistration>();
			this.MergeResults(dictionary, globalRegistrations);
			this.MergeResults(dictionary, siteRegistrations);
			this.MergeResults(dictionary, linkRegistrations);
			foreach (PnrpPeerResolver.PnrpRegistration pnrpRegistration in dictionary.Values)
			{
				pnrpRegistration.Addresses = pnrpRegistration.addressList.ToArray();
				PeerNodeAddress peerNodeAddress = this.PeerNodeAddressFromPnrpRegistration(pnrpRegistration);
				if (peerNodeAddress != null)
				{
					nodeAddressList.Add(peerNodeAddress);
				}
			}
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x00186228 File Offset: 0x00184428
		public override ReadOnlyCollection<PeerNodeAddress> Resolve(string meshId, int maxAddresses, TimeSpan timeout)
		{
			this.ThrowIfNoPnrp();
			List<PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver> list = new List<PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver>();
			List<PnrpPeerResolver.PnrpRegistration> list2 = new List<PnrpPeerResolver.PnrpRegistration>();
			List<PnrpPeerResolver.PnrpRegistration> list3 = new List<PnrpPeerResolver.PnrpRegistration>();
			List<PnrpPeerResolver.PnrpRegistration> list4 = new List<PnrpPeerResolver.PnrpRegistration>();
			List<WaitHandle> list5 = new List<WaitHandle>();
			Dictionary<uint, string> dictionary = new Dictionary<uint, string>();
			Dictionary<uint, string> dictionary2 = new Dictionary<uint, string>();
			PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria resolveCriteria = PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria.NearestNonCurrentProcess;
			TimeoutHelper timeoutHelper = new TimeoutHelper((TimeSpan.Compare(timeout, PnrpPeerResolver.MaxResolveTimeout) <= 0) ? timeout : PnrpPeerResolver.MaxResolveTimeout);
			if (!PnrpPeerResolver.HasPeerNodeForMesh(meshId))
			{
				resolveCriteria = PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria.Any;
			}
			PnrpPeerResolver.PnrpResolveScope pnrpResolveScope = this.EnumerateClouds(true, dictionary2, dictionary);
			if (this.remoteExtension != null)
			{
				meshId += this.remoteExtension;
			}
			string peerName = string.Format(CultureInfo.InvariantCulture, "0.{0}", new object[]
			{
				meshId
			});
			if ((pnrpResolveScope & PnrpPeerResolver.PnrpResolveScope.Global) != PnrpPeerResolver.PnrpResolveScope.None)
			{
				PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver peerNameResolver = new PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver(peerName, maxAddresses, resolveCriteria, 0U, "Global_", timeoutHelper.RemainingTime(), list2);
				list5.Add(peerNameResolver.AsyncWaitHandle);
				list.Add(peerNameResolver);
			}
			if ((pnrpResolveScope & PnrpPeerResolver.PnrpResolveScope.LinkLocal) != PnrpPeerResolver.PnrpResolveScope.None)
			{
				foreach (KeyValuePair<uint, string> keyValuePair in dictionary2)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver peerNameResolver = new PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver(peerName, maxAddresses, resolveCriteria, keyValuePair.Key, keyValuePair.Value, timeoutHelper.RemainingTime(), list3);
					list5.Add(peerNameResolver.AsyncWaitHandle);
					list.Add(peerNameResolver);
				}
			}
			if ((pnrpResolveScope & PnrpPeerResolver.PnrpResolveScope.SiteLocal) != PnrpPeerResolver.PnrpResolveScope.None)
			{
				foreach (KeyValuePair<uint, string> keyValuePair2 in dictionary)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver peerNameResolver = new PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver(peerName, maxAddresses, resolveCriteria, keyValuePair2.Key, keyValuePair2.Value, timeoutHelper.RemainingTime(), list4);
					list5.Add(peerNameResolver.AsyncWaitHandle);
					list.Add(peerNameResolver);
				}
			}
			if (list5.Count == 0)
			{
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					Exception exception = new InvalidOperationException(SR.GetString("PnrpNoClouds"));
					PnrpResolveExceptionTraceRecord extendedData = new PnrpResolveExceptionTraceRecord(meshId, string.Empty, exception);
					TraceUtility.TraceEvent(TraceEventType.Warning, 262218, SR.GetString("TraceCodePnrpResolvedAddresses"), extendedData, this, null);
				}
				return new ReadOnlyCollection<PeerNodeAddress>(new List<PeerNodeAddress>());
			}
			Exception ex = null;
			foreach (PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver peerNameResolver2 in list)
			{
				try
				{
					peerNameResolver2.End();
				}
				catch (SocketException ex2)
				{
					DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
					ex = ex2;
				}
			}
			List<PeerNodeAddress> list6 = new List<PeerNodeAddress>();
			this.MergeResults(list6, list2, list3, list4);
			if (ex != null && list6.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
			}
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				PnrpPeerResolverTraceRecord extendedData2 = new PnrpPeerResolverTraceRecord(meshId, list6);
				TraceUtility.TraceEvent(TraceEventType.Information, 262218, SR.GetString("TraceCodePnrpResolvedAddresses"), extendedData2, this, null);
			}
			return new ReadOnlyCollection<PeerNodeAddress>(list6);
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x00186504 File Offset: 0x00184704
		public override bool Equals(object other)
		{
			return other is PnrpPeerResolver;
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x0018650F File Offset: 0x0018470F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04003BD6 RID: 15318
		private PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameRegistrar registrar = new PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameRegistrar();

		// Token: 0x04003BD7 RID: 15319
		private static bool isPnrpAvailable;

		// Token: 0x04003BD8 RID: 15320
		private static bool isPnrpInstalled;

		// Token: 0x04003BD9 RID: 15321
		private const PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria resolutionScope = PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria.NearestNonCurrentProcess;

		// Token: 0x04003BDA RID: 15322
		public const int PNRPINFO_HINT = 1;

		// Token: 0x04003BDB RID: 15323
		internal const int CommentLength = 80;

		// Token: 0x04003BDC RID: 15324
		internal const byte TcpTransport = 1;

		// Token: 0x04003BDD RID: 15325
		internal const byte PayloadVersion = 1;

		// Token: 0x04003BDE RID: 15326
		internal const char PathSeparator = '/';

		// Token: 0x04003BDF RID: 15327
		internal const int MinGuids = 1;

		// Token: 0x04003BE0 RID: 15328
		internal const int MaxGuids = 2;

		// Token: 0x04003BE1 RID: 15329
		internal const byte GuidEscape = 255;

		// Token: 0x04003BE2 RID: 15330
		internal const int MaxAddressEntries = 10;

		// Token: 0x04003BE3 RID: 15331
		internal const int MaxAddressEntriesV1 = 4;

		// Token: 0x04003BE4 RID: 15332
		internal const int MaxPathLength = 200;

		// Token: 0x04003BE5 RID: 15333
		private static TimeSpan MaxTimeout = new TimeSpan(0, 10, 0);

		// Token: 0x04003BE6 RID: 15334
		private static TimeSpan MaxResolveTimeout = new TimeSpan(0, 0, 45);

		// Token: 0x04003BE7 RID: 15335
		internal const string GlobalCloudName = "Global_";

		// Token: 0x04003BE8 RID: 15336
		private static object SharedLock = new object();

		// Token: 0x04003BE9 RID: 15337
		private static Random randomGenerator = new Random();

		// Token: 0x04003BEA RID: 15338
		private static TimeSpan TimeToWaitForStatus = TimeSpan.FromSeconds(15.0);

		// Token: 0x04003BEB RID: 15339
		private PeerReferralPolicy referralPolicy = PeerReferralPolicy.Share;

		// Token: 0x04003BEC RID: 15340
		private string localExtension;

		// Token: 0x04003BED RID: 15341
		private string remoteExtension;

		// Token: 0x02000E89 RID: 3721
		[Flags]
		internal enum PnrpResolveScope
		{
			// Token: 0x04004B7E RID: 19326
			None = 0,
			// Token: 0x04004B7F RID: 19327
			Global = 1,
			// Token: 0x04004B80 RID: 19328
			SiteLocal = 2,
			// Token: 0x04004B81 RID: 19329
			LinkLocal = 4,
			// Token: 0x04004B82 RID: 19330
			All = 7
		}

		// Token: 0x02000E8A RID: 3722
		private class RegistrationHandle
		{
			// Token: 0x060083FD RID: 33789 RVA: 0x001E8108 File Offset: 0x001E6308
			public RegistrationHandle(string peerName)
			{
				this.PeerName = peerName;
				this.Clouds = new List<string>();
			}

			// Token: 0x060083FE RID: 33790 RVA: 0x001E8122 File Offset: 0x001E6322
			public void AddCloud(string name)
			{
				this.Clouds.Add(name);
			}

			// Token: 0x04004B83 RID: 19331
			public string PeerName;

			// Token: 0x04004B84 RID: 19332
			public List<string> Clouds;
		}

		// Token: 0x02000E8B RID: 3723
		internal class PnrpRegistration
		{
			// Token: 0x060083FF RID: 33791 RVA: 0x001E8130 File Offset: 0x001E6330
			internal static PnrpPeerResolver.PnrpRegistration Create(string peerName, string comment, string cloudName)
			{
				return new PnrpPeerResolver.PnrpRegistration
				{
					Comment = comment,
					CloudName = cloudName,
					PeerName = peerName,
					addressList = new List<IPEndPoint>()
				};
			}

			// Token: 0x04004B85 RID: 19333
			public string PeerName;

			// Token: 0x04004B86 RID: 19334
			public string CloudName;

			// Token: 0x04004B87 RID: 19335
			public string Comment;

			// Token: 0x04004B88 RID: 19336
			public IPEndPoint[] Addresses;

			// Token: 0x04004B89 RID: 19337
			public List<IPEndPoint> addressList;
		}

		// Token: 0x02000E8C RID: 3724
		internal class CharEncoder
		{
			// Token: 0x06008401 RID: 33793 RVA: 0x001E816C File Offset: 0x001E636C
			private static void CheckAtLimit(int current)
			{
				if (current + 1 >= 80)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("PeerPnrpIllegalUri")));
				}
			}

			// Token: 0x06008402 RID: 33794 RVA: 0x001E8190 File Offset: 0x001E6390
			private static void EncodeByte(byte b, ref int offset, byte[] bytes)
			{
				int num;
				if (b == 0 || b == 255)
				{
					PnrpPeerResolver.CharEncoder.CheckAtLimit(offset);
					num = offset;
					offset = num + 1;
					bytes[num] = byte.MaxValue;
				}
				PnrpPeerResolver.CharEncoder.CheckAtLimit(offset);
				num = offset;
				offset = num + 1;
				bytes[num] = b;
			}

			// Token: 0x06008403 RID: 33795 RVA: 0x001E81D4 File Offset: 0x001E63D4
			internal static string Encode(int version, string protocolName, Guid[] guids)
			{
				byte[] array = new byte[80];
				int num = 0;
				int value = PnrpPeerResolver.ProtocolFromName(protocolName);
				PnrpPeerResolver.CharEncoder.EncodeByte(Convert.ToByte(version), ref num, array);
				PnrpPeerResolver.CharEncoder.EncodeByte(Convert.ToByte(value), ref num, array);
				PnrpPeerResolver.CharEncoder.EncodeByte(Convert.ToByte(guids.Length), ref num, array);
				foreach (Guid guid in guids)
				{
					foreach (byte value2 in guid.ToByteArray())
					{
						PnrpPeerResolver.CharEncoder.EncodeByte(Convert.ToByte(value2), ref num, array);
					}
				}
				if (num % 2 != 0 && num < array.Length)
				{
					array[num] = byte.MaxValue;
				}
				int num2 = num;
				int num3 = num2 / 2 + num2 % 2;
				char[] array3 = new char[num3];
				num = 0;
				for (int k = 0; k < num3; k++)
				{
					array3[k] = Convert.ToChar((int)array[num++] * 256 + (int)array[num++]);
				}
				return new string(array3);
			}

			// Token: 0x06008404 RID: 33796 RVA: 0x001E82D4 File Offset: 0x001E64D4
			private static byte GetByte(int offset, char[] chars)
			{
				int num = offset / 2;
				int num2 = offset % 2;
				return Convert.ToByte((int)((num2 == 1) ? (chars[num] & 'ÿ') : (chars[num] / 'Ā')));
			}

			// Token: 0x06008405 RID: 33797 RVA: 0x001E8308 File Offset: 0x001E6508
			private static byte DecodeByte(ref int offset, char[] chars)
			{
				int num = offset;
				offset = num + 1;
				byte @byte = PnrpPeerResolver.CharEncoder.GetByte(num, chars);
				if (@byte == 255)
				{
					num = offset;
					offset = num + 1;
					@byte = PnrpPeerResolver.CharEncoder.GetByte(num, chars);
				}
				return @byte;
			}

			// Token: 0x06008406 RID: 33798 RVA: 0x001E8340 File Offset: 0x001E6540
			internal static void Decode(string buffer, out int version, out string protocolName, out Guid[] guids)
			{
				char[] chars = buffer.ToCharArray();
				int num = 0;
				version = (int)PnrpPeerResolver.CharEncoder.DecodeByte(ref num, chars);
				byte number = PnrpPeerResolver.CharEncoder.DecodeByte(ref num, chars);
				protocolName = PnrpPeerResolver.NameFromProtocol(number);
				int num2 = (int)PnrpPeerResolver.CharEncoder.DecodeByte(ref num, chars);
				guids = new Guid[num2];
				for (int i = 0; i < num2; i++)
				{
					byte[] array = new byte[16];
					for (int j = 0; j < 16; j++)
					{
						array[j] = PnrpPeerResolver.CharEncoder.DecodeByte(ref num, chars);
					}
					guids[i] = new Guid(array);
				}
			}
		}

		// Token: 0x02000E8D RID: 3725
		internal enum PnrpErrorCodes
		{
			// Token: 0x04004B8B RID: 19339
			WSA_PNRP_ERROR_BASE = 11500,
			// Token: 0x04004B8C RID: 19340
			WSA_PNRP_CLOUD_NOT_FOUND,
			// Token: 0x04004B8D RID: 19341
			WSA_PNRP_CLOUD_DISABLED,
			// Token: 0x04004B8E RID: 19342
			WSA_PNRP_CLOUD_IS_RESOLVE_ONLY = 11505,
			// Token: 0x04004B8F RID: 19343
			WSA_PNRP_FW_PORT_BLOCKED = 11507,
			// Token: 0x04004B90 RID: 19344
			WSA_PNRP_DUPLICATE_PEER_NAME
		}

		// Token: 0x02000E8E RID: 3726
		internal class PnrpException : SocketException
		{
			// Token: 0x06008408 RID: 33800 RVA: 0x001E83D1 File Offset: 0x001E65D1
			internal PnrpException(int errorCode, string cloud) : base(errorCode)
			{
				this.LoadMessage(errorCode, cloud);
			}

			// Token: 0x17001D20 RID: 7456
			// (get) Token: 0x06008409 RID: 33801 RVA: 0x001E83E2 File Offset: 0x001E65E2
			public override string Message
			{
				get
				{
					if (!string.IsNullOrEmpty(this.message))
					{
						return this.message;
					}
					return base.Message;
				}
			}

			// Token: 0x0600840A RID: 33802 RVA: 0x001E8400 File Offset: 0x001E6600
			private void LoadMessage(int errorCode, string cloud)
			{
				string text;
				switch (errorCode)
				{
				case 11501:
					text = "PnrpCloudNotFound";
					goto IL_5A;
				case 11502:
					text = "PnrpCloudDisabled";
					goto IL_5A;
				case 11505:
					text = "PnrpCloudResolveOnly";
					goto IL_5A;
				case 11507:
					text = "PnrpPortBlocked";
					goto IL_5A;
				case 11508:
					text = "PnrpDuplicatePeerName";
					goto IL_5A;
				}
				text = null;
				IL_5A:
				if (text != null)
				{
					this.message = SR.GetString(text, new object[]
					{
						cloud
					});
				}
			}

			// Token: 0x04004B91 RID: 19345
			private string message;
		}

		// Token: 0x02000E8F RID: 3727
		internal static class UnsafePnrpNativeMethods
		{
			// Token: 0x0600840B RID: 33803
			[DllImport("ws2_32.dll", CharSet = CharSet.Unicode)]
			private static extern int WSASetService(CriticalAllocHandle querySet, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaSetServiceOp essOperation, int dwControlFlags);

			// Token: 0x0600840C RID: 33804
			[DllImport("ws2_32.dll", CharSet = CharSet.Unicode)]
			private static extern int WSALookupServiceNext(PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalLookupHandle hLookup, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNspControlFlags dwControlFlags, ref int lpdwBufferLength, IntPtr Results);

			// Token: 0x0600840D RID: 33805
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("ws2_32.dll", CharSet = CharSet.Unicode)]
			private static extern int WSALookupServiceEnd(IntPtr hLookup);

			// Token: 0x0600840E RID: 33806
			[DllImport("ws2_32.dll", CharSet = CharSet.Unicode)]
			private static extern int WSALookupServiceBegin(CriticalAllocHandle query, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNspControlFlags dwControlFlags, out PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalLookupHandle hLookup);

			// Token: 0x0600840F RID: 33807
			[DllImport("ws2_32.dll", CharSet = CharSet.Ansi)]
			private static extern int WSAStartup(short wVersionRequested, ref PnrpPeerResolver.UnsafePnrpNativeMethods.WsaData lpWSAData);

			// Token: 0x06008410 RID: 33808
			[DllImport("ws2_32.dll", CharSet = CharSet.Ansi)]
			private static extern int WSACleanup();

			// Token: 0x06008411 RID: 33809
			[DllImport("ws2_32.dll", CharSet = CharSet.Ansi)]
			private static extern int WSAGetLastError();

			// Token: 0x06008412 RID: 33810
			[DllImport("ws2_32.dll", CharSet = CharSet.Unicode)]
			private static extern int WSAEnumNameSpaceProviders(ref int lpdwBufferLength, IntPtr lpnspBuffer);

			// Token: 0x04004B92 RID: 19346
			private static Guid SvcIdCloud = new Guid(3257113830U, 192, 20415, 186, 214, 24, 19, 147, 133, 164, 154);

			// Token: 0x04004B93 RID: 19347
			private static Guid SvcIdNameV1 = new Guid(3257113829U, 192, 20415, 186, 214, 24, 19, 147, 133, 164, 154);

			// Token: 0x04004B94 RID: 19348
			private static Guid SvcIdName = new Guid(3257113831U, 192, 20415, 186, 214, 24, 19, 147, 133, 164, 154);

			// Token: 0x04004B95 RID: 19349
			private static Guid NsProviderName = new Guid(67013069, 30317, 18806, 185, 193, 187, 155, 196, 44, 123, 77);

			// Token: 0x04004B96 RID: 19350
			private static Guid NsProviderCloud = new Guid(67013070, 30317, 18806, 185, 193, 187, 155, 196, 44, 123, 77);

			// Token: 0x04004B97 RID: 19351
			private const int MaxAddresses = 10;

			// Token: 0x04004B98 RID: 19352
			private const int MaxAddressesV1 = 4;

			// Token: 0x04004B99 RID: 19353
			private const short RequiredWinsockVersion = 514;

			// Token: 0x02000F99 RID: 3993
			[Serializable]
			internal enum NspNamespaces
			{
				// Token: 0x04004FAB RID: 20395
				Cloud = 39,
				// Token: 0x04004FAC RID: 20396
				Name = 38
			}

			// Token: 0x02000F9A RID: 3994
			[Flags]
			[Serializable]
			internal enum PnrpCloudFlags
			{
				// Token: 0x04004FAE RID: 20398
				None = 0,
				// Token: 0x04004FAF RID: 20399
				LocalName = 1
			}

			// Token: 0x02000F9B RID: 3995
			[Serializable]
			internal enum PnrpCloudState
			{
				// Token: 0x04004FB1 RID: 20401
				Virtual,
				// Token: 0x04004FB2 RID: 20402
				Synchronizing,
				// Token: 0x04004FB3 RID: 20403
				Active,
				// Token: 0x04004FB4 RID: 20404
				Dead,
				// Token: 0x04004FB5 RID: 20405
				Disabled,
				// Token: 0x04004FB6 RID: 20406
				NoNet,
				// Token: 0x04004FB7 RID: 20407
				Alone
			}

			// Token: 0x02000F9C RID: 3996
			[Serializable]
			internal enum PnrpExtendedPayloadType
			{
				// Token: 0x04004FB9 RID: 20409
				None,
				// Token: 0x04004FBA RID: 20410
				Binary,
				// Token: 0x04004FBB RID: 20411
				String
			}

			// Token: 0x02000F9D RID: 3997
			[Serializable]
			internal enum PnrpResolveCriteria
			{
				// Token: 0x04004FBD RID: 20413
				Default,
				// Token: 0x04004FBE RID: 20414
				Remote,
				// Token: 0x04004FBF RID: 20415
				NearestRemote,
				// Token: 0x04004FC0 RID: 20416
				NonCurrentProcess,
				// Token: 0x04004FC1 RID: 20417
				NearestNonCurrentProcess,
				// Token: 0x04004FC2 RID: 20418
				Any,
				// Token: 0x04004FC3 RID: 20419
				Nearest
			}

			// Token: 0x02000F9E RID: 3998
			[Serializable]
			internal enum PnrpRegisteredIdState
			{
				// Token: 0x04004FC5 RID: 20421
				Ok = 1,
				// Token: 0x04004FC6 RID: 20422
				Problem
			}

			// Token: 0x02000F9F RID: 3999
			internal enum PnrpScope
			{
				// Token: 0x04004FC8 RID: 20424
				Any,
				// Token: 0x04004FC9 RID: 20425
				Global,
				// Token: 0x04004FCA RID: 20426
				SiteLocal,
				// Token: 0x04004FCB RID: 20427
				LinkLocal
			}

			// Token: 0x02000FA0 RID: 4000
			[Flags]
			internal enum WsaNspControlFlags
			{
				// Token: 0x04004FCD RID: 20429
				Deep = 1,
				// Token: 0x04004FCE RID: 20430
				Containers = 2,
				// Token: 0x04004FCF RID: 20431
				NoContainers = 4,
				// Token: 0x04004FD0 RID: 20432
				Nearest = 8,
				// Token: 0x04004FD1 RID: 20433
				ReturnName = 16,
				// Token: 0x04004FD2 RID: 20434
				ReturnType = 32,
				// Token: 0x04004FD3 RID: 20435
				ReturnVersion = 64,
				// Token: 0x04004FD4 RID: 20436
				ReturnComment = 128,
				// Token: 0x04004FD5 RID: 20437
				ReturnAddr = 256,
				// Token: 0x04004FD6 RID: 20438
				ReturnBlob = 512,
				// Token: 0x04004FD7 RID: 20439
				ReturnAliases = 1024,
				// Token: 0x04004FD8 RID: 20440
				ReturnQueryString = 2048,
				// Token: 0x04004FD9 RID: 20441
				ReturnAll = 4080,
				// Token: 0x04004FDA RID: 20442
				ResService = 32768,
				// Token: 0x04004FDB RID: 20443
				FlushCache = 4096,
				// Token: 0x04004FDC RID: 20444
				FlushPrevious = 8192
			}

			// Token: 0x02000FA1 RID: 4001
			internal enum WsaError
			{
				// Token: 0x04004FDE RID: 20446
				WSAEINVAL = 10022,
				// Token: 0x04004FDF RID: 20447
				WSAEFAULT = 10014,
				// Token: 0x04004FE0 RID: 20448
				WSAENOMORE = 10102,
				// Token: 0x04004FE1 RID: 20449
				WSA_E_NO_MORE = 10110,
				// Token: 0x04004FE2 RID: 20450
				WSANO_DATA = 11004
			}

			// Token: 0x02000FA2 RID: 4002
			internal enum WsaSetServiceOp
			{
				// Token: 0x04004FE4 RID: 20452
				Register,
				// Token: 0x04004FE5 RID: 20453
				Deregister,
				// Token: 0x04004FE6 RID: 20454
				Delete
			}

			// Token: 0x02000FA3 RID: 4003
			internal struct BlobSafe
			{
				// Token: 0x04004FE7 RID: 20455
				public int cbSize;

				// Token: 0x04004FE8 RID: 20456
				public CriticalAllocHandle pBlobData;
			}

			// Token: 0x02000FA4 RID: 4004
			internal struct BlobNative
			{
				// Token: 0x04004FE9 RID: 20457
				public int cbSize;

				// Token: 0x04004FEA RID: 20458
				public IntPtr pBlobData;
			}

			// Token: 0x02000FA5 RID: 4005
			internal class CloudInfo
			{
				// Token: 0x04004FEB RID: 20459
				public string Name;

				// Token: 0x04004FEC RID: 20460
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope Scope;

				// Token: 0x04004FED RID: 20461
				public uint ScopeId;

				// Token: 0x04004FEE RID: 20462
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudState State;

				// Token: 0x04004FEF RID: 20463
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudFlags Flags;
			}

			// Token: 0x02000FA6 RID: 4006
			[Serializable]
			internal struct CsAddrInfo
			{
				// Token: 0x04004FF0 RID: 20464
				public IPEndPoint LocalAddr;

				// Token: 0x04004FF1 RID: 20465
				public IPEndPoint RemoteAddr;

				// Token: 0x04004FF2 RID: 20466
				public int iSocketType;

				// Token: 0x04004FF3 RID: 20467
				public int iProtocol;
			}

			// Token: 0x02000FA7 RID: 4007
			[StructLayout(LayoutKind.Sequential)]
			internal class CsAddrInfoSafe : IDisposable
			{
				// Token: 0x06008876 RID: 34934 RVA: 0x001FB69C File Offset: 0x001F989C
				public static PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe[] FromAddresses(PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfo[] addresses)
				{
					if (addresses == null || addresses.Length == 0)
					{
						return null;
					}
					PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe[] array = new PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe[addresses.Length];
					int num = 0;
					foreach (PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfo csAddrInfo in addresses)
					{
						PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe csAddrInfoSafe = new PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe();
						csAddrInfoSafe.LocalAddr = PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE.SocketAddressFromIPEndPoint(csAddrInfo.LocalAddr);
						csAddrInfoSafe.RemoteAddr = PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE.SocketAddressFromIPEndPoint(csAddrInfo.RemoteAddr);
						csAddrInfoSafe.iProtocol = csAddrInfo.iProtocol;
						csAddrInfoSafe.iSocketType = csAddrInfo.iSocketType;
						array[num++] = csAddrInfoSafe;
					}
					return array;
				}

				// Token: 0x06008877 RID: 34935 RVA: 0x001FB72C File Offset: 0x001F992C
				public static void StructureToPtr(PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe input, IntPtr target)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoNative csAddrInfoNative;
					csAddrInfoNative.iProtocol = input.iProtocol;
					csAddrInfoNative.iSocketType = input.iSocketType;
					csAddrInfoNative.LocalAddr.iSockaddrLength = input.LocalAddr.iSockaddrLength;
					csAddrInfoNative.LocalAddr.lpSockAddr = input.LocalAddr.lpSockAddr;
					csAddrInfoNative.RemoteAddr.iSockaddrLength = input.RemoteAddr.iSockaddrLength;
					csAddrInfoNative.RemoteAddr.lpSockAddr = input.RemoteAddr.lpSockAddr;
					Marshal.StructureToPtr(csAddrInfoNative, target, false);
				}

				// Token: 0x06008878 RID: 34936 RVA: 0x001FB7C8 File Offset: 0x001F99C8
				~CsAddrInfoSafe()
				{
					this.Dispose(false);
				}

				// Token: 0x06008879 RID: 34937 RVA: 0x001FB7F8 File Offset: 0x001F99F8
				public virtual void Dispose()
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}

				// Token: 0x0600887A RID: 34938 RVA: 0x001FB807 File Offset: 0x001F9A07
				private void Dispose(bool disposing)
				{
					if (this.disposed && disposing)
					{
						this.LocalAddr.Dispose();
						this.RemoteAddr.Dispose();
					}
					this.disposed = true;
				}

				// Token: 0x04004FF4 RID: 20468
				public PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE LocalAddr;

				// Token: 0x04004FF5 RID: 20469
				public PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE RemoteAddr;

				// Token: 0x04004FF6 RID: 20470
				public int iSocketType;

				// Token: 0x04004FF7 RID: 20471
				public int iProtocol;

				// Token: 0x04004FF8 RID: 20472
				private bool disposed;
			}

			// Token: 0x02000FA8 RID: 4008
			internal struct CsAddrInfoNative
			{
				// Token: 0x04004FF9 RID: 20473
				public PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_NATIVE LocalAddr;

				// Token: 0x04004FFA RID: 20474
				public PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_NATIVE RemoteAddr;

				// Token: 0x04004FFB RID: 20475
				public int iSocketType;

				// Token: 0x04004FFC RID: 20476
				public int iProtocol;
			}

			// Token: 0x02000FA9 RID: 4009
			[Serializable]
			internal struct PnrpCloudId
			{
				// Token: 0x04004FFD RID: 20477
				public int AddressFamily;

				// Token: 0x04004FFE RID: 20478
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope Scope;

				// Token: 0x04004FFF RID: 20479
				public uint ScopeId;
			}

			// Token: 0x02000FAA RID: 4010
			internal struct PnrpCloudInfo
			{
				// Token: 0x04005000 RID: 20480
				public int dwSize;

				// Token: 0x04005001 RID: 20481
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudId Cloud;

				// Token: 0x04005002 RID: 20482
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudState dwCloudState;

				// Token: 0x04005003 RID: 20483
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudFlags Flags;
			}

			// Token: 0x02000FAB RID: 4011
			internal struct PnrpInfoNative
			{
				// Token: 0x04005004 RID: 20484
				public int dwSize;

				// Token: 0x04005005 RID: 20485
				public string lpwszIdentity;

				// Token: 0x04005006 RID: 20486
				public int nMaxResolve;

				// Token: 0x04005007 RID: 20487
				public int dwTimeout;

				// Token: 0x04005008 RID: 20488
				public int dwLifetime;

				// Token: 0x04005009 RID: 20489
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria enResolveCriteria;

				// Token: 0x0400500A RID: 20490
				public int dwFlags;

				// Token: 0x0400500B RID: 20491
				public PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_NATIVE saHint;

				// Token: 0x0400500C RID: 20492
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpRegisteredIdState enNameState;
			}

			// Token: 0x02000FAC RID: 4012
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct PnrpInfo
			{
				// Token: 0x0600887C RID: 34940 RVA: 0x001FB83C File Offset: 0x001F9A3C
				public static void ToPnrpInfoNative(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo source, ref PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfoNative target)
				{
					target.dwSize = source.dwSize;
					target.lpwszIdentity = source.lpwszIdentity;
					target.nMaxResolve = source.nMaxResolve;
					target.dwTimeout = source.dwTimeout;
					target.dwLifetime = source.dwLifetime;
					target.enResolveCriteria = source.enResolveCriteria;
					target.dwFlags = source.dwFlags;
					if (source.saHint != null)
					{
						target.saHint.lpSockAddr = source.saHint.lpSockAddr;
						target.saHint.iSockaddrLength = source.saHint.iSockaddrLength;
					}
					else
					{
						target.saHint.lpSockAddr = IntPtr.Zero;
						target.saHint.iSockaddrLength = 0;
					}
					target.enNameState = source.enNameState;
				}

				// Token: 0x0400500D RID: 20493
				public int dwSize;

				// Token: 0x0400500E RID: 20494
				public string lpwszIdentity;

				// Token: 0x0400500F RID: 20495
				public int nMaxResolve;

				// Token: 0x04005010 RID: 20496
				public int dwTimeout;

				// Token: 0x04005011 RID: 20497
				public int dwLifetime;

				// Token: 0x04005012 RID: 20498
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria enResolveCriteria;

				// Token: 0x04005013 RID: 20499
				public int dwFlags;

				// Token: 0x04005014 RID: 20500
				public PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE saHint;

				// Token: 0x04005015 RID: 20501
				public PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpRegisteredIdState enNameState;
			}

			// Token: 0x02000FAD RID: 4013
			[Serializable]
			internal struct sockaddr_in
			{
				// Token: 0x04005016 RID: 20502
				public short sin_family;

				// Token: 0x04005017 RID: 20503
				public ushort sin_port;

				// Token: 0x04005018 RID: 20504
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
				public byte[] sin_addr;

				// Token: 0x04005019 RID: 20505
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
				public byte[] sin_zero;
			}

			// Token: 0x02000FAE RID: 4014
			[Serializable]
			internal struct sockaddr_in6
			{
				// Token: 0x0400501A RID: 20506
				public short sin6_family;

				// Token: 0x0400501B RID: 20507
				public ushort sin6_port;

				// Token: 0x0400501C RID: 20508
				public uint sin6_flowinfo;

				// Token: 0x0400501D RID: 20509
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
				public byte[] sin6_addr;

				// Token: 0x0400501E RID: 20510
				public uint sin6_scope_id;
			}

			// Token: 0x02000FAF RID: 4015
			internal class SOCKET_ADDRESS_SAFE : IDisposable
			{
				// Token: 0x0600887D RID: 34941 RVA: 0x001FB900 File Offset: 0x001F9B00
				public static PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE SocketAddressFromIPEndPoint(IPEndPoint endpoint)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE socket_ADDRESS_SAFE = new PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE();
					if (endpoint == null)
					{
						return socket_ADDRESS_SAFE;
					}
					if (endpoint.AddressFamily == AddressFamily.InterNetwork)
					{
						socket_ADDRESS_SAFE.iSockaddrLength = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in));
						socket_ADDRESS_SAFE.lpSockAddr = CriticalAllocHandle.FromSize(socket_ADDRESS_SAFE.iSockaddrLength);
						Marshal.StructureToPtr(new PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in
						{
							sin_family = 2,
							sin_port = (ushort)endpoint.Port,
							sin_addr = endpoint.Address.GetAddressBytes()
						}, socket_ADDRESS_SAFE.lpSockAddr, false);
					}
					else if (endpoint.AddressFamily == AddressFamily.InterNetworkV6)
					{
						socket_ADDRESS_SAFE.iSockaddrLength = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in6));
						socket_ADDRESS_SAFE.lpSockAddr = CriticalAllocHandle.FromSize(socket_ADDRESS_SAFE.iSockaddrLength);
						Marshal.StructureToPtr(new PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in6
						{
							sin6_family = 23,
							sin6_port = (ushort)endpoint.Port,
							sin6_addr = endpoint.Address.GetAddressBytes(),
							sin6_scope_id = (uint)endpoint.Address.ScopeId
						}, socket_ADDRESS_SAFE.lpSockAddr, false);
					}
					return socket_ADDRESS_SAFE;
				}

				// Token: 0x0600887E RID: 34942 RVA: 0x001FBA24 File Offset: 0x001F9C24
				~SOCKET_ADDRESS_SAFE()
				{
					this.Dispose(false);
				}

				// Token: 0x0600887F RID: 34943 RVA: 0x001FBA54 File Offset: 0x001F9C54
				public virtual void Dispose()
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}

				// Token: 0x06008880 RID: 34944 RVA: 0x001FBA63 File Offset: 0x001F9C63
				private void Dispose(bool disposing)
				{
					if (!this.disposed && disposing)
					{
						this.lpSockAddr.Dispose();
					}
					this.disposed = true;
				}

				// Token: 0x0400501F RID: 20511
				public CriticalAllocHandle lpSockAddr;

				// Token: 0x04005020 RID: 20512
				public int iSockaddrLength;

				// Token: 0x04005021 RID: 20513
				private bool disposed;
			}

			// Token: 0x02000FB0 RID: 4016
			internal struct SOCKET_ADDRESS_NATIVE
			{
				// Token: 0x04005022 RID: 20514
				public IntPtr lpSockAddr;

				// Token: 0x04005023 RID: 20515
				public int iSockaddrLength;
			}

			// Token: 0x02000FB1 RID: 4017
			[Serializable]
			internal struct WsaData
			{
				// Token: 0x04005024 RID: 20516
				public short wVersion;

				// Token: 0x04005025 RID: 20517
				public short wHighVersion;

				// Token: 0x04005026 RID: 20518
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
				public string szDescription;

				// Token: 0x04005027 RID: 20519
				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
				public string szSystemStatus;

				// Token: 0x04005028 RID: 20520
				public short iMaxSockets;

				// Token: 0x04005029 RID: 20521
				public short iMaxUdpDg;

				// Token: 0x0400502A RID: 20522
				public IntPtr lpVendorInfo;
			}

			// Token: 0x02000FB2 RID: 4018
			[Serializable]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WsaNamespaceInfo
			{
				// Token: 0x0400502B RID: 20523
				public Guid NSProviderId;

				// Token: 0x0400502C RID: 20524
				public int dwNameSpace;

				// Token: 0x0400502D RID: 20525
				public int fActive;

				// Token: 0x0400502E RID: 20526
				public int dwVersion;

				// Token: 0x0400502F RID: 20527
				public IntPtr lpszIdentifier;
			}

			// Token: 0x02000FB3 RID: 4019
			internal class WsaQuerySet
			{
				// Token: 0x06008882 RID: 34946 RVA: 0x001FBA8C File Offset: 0x001F9C8C
				public static PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe ToWsaQuerySetSafe(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet input)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe wsaQuerySetSafe = new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe();
					if (input == null)
					{
						return wsaQuerySetSafe;
					}
					wsaQuerySetSafe.dwSize = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative));
					wsaQuerySetSafe.lpszServiceInstanceName = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleString.FromString(input.ServiceInstanceName);
					wsaQuerySetSafe.lpServiceClassId = CriticalAllocHandleGuid.FromGuid(input.ServiceClassId);
					wsaQuerySetSafe.lpszComment = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleString.FromString(input.Comment);
					wsaQuerySetSafe.dwNameSpace = input.NameSpace;
					wsaQuerySetSafe.lpNSProviderId = CriticalAllocHandleGuid.FromGuid(input.NSProviderId);
					wsaQuerySetSafe.lpszContext = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleString.FromString(input.Context);
					wsaQuerySetSafe.dwNumberOfProtocols = 0;
					wsaQuerySetSafe.lpafpProtocols = IntPtr.Zero;
					wsaQuerySetSafe.lpszQueryString = IntPtr.Zero;
					if (input.CsAddrInfos != null)
					{
						wsaQuerySetSafe.dwNumberOfCsAddrs = input.CsAddrInfos.Length;
						wsaQuerySetSafe.addressList = PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe.FromAddresses(input.CsAddrInfos);
					}
					wsaQuerySetSafe.dwOutputFlags = 0;
					wsaQuerySetSafe.lpBlob = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandlePnrpBlob.FromPnrpBlob(input.Blob);
					return wsaQuerySetSafe;
				}

				// Token: 0x04005030 RID: 20528
				public string ServiceInstanceName;

				// Token: 0x04005031 RID: 20529
				public Guid ServiceClassId;

				// Token: 0x04005032 RID: 20530
				public string Comment;

				// Token: 0x04005033 RID: 20531
				public PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces NameSpace;

				// Token: 0x04005034 RID: 20532
				public Guid NSProviderId;

				// Token: 0x04005035 RID: 20533
				public string Context;

				// Token: 0x04005036 RID: 20534
				public PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfo[] CsAddrInfos;

				// Token: 0x04005037 RID: 20535
				public object Blob;
			}

			// Token: 0x02000FB4 RID: 4020
			internal class CriticalAllocHandlePnrpBlob : CriticalAllocHandle
			{
				// Token: 0x06008884 RID: 34948 RVA: 0x001FBB80 File Offset: 0x001F9D80
				public static CriticalAllocHandle FromPnrpBlob(object input)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.BlobSafe blobSafe = default(PnrpPeerResolver.UnsafePnrpNativeMethods.BlobSafe);
					if (input != null)
					{
						if (input.GetType() == typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo))
						{
							int num = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfoNative));
							blobSafe.pBlobData = CriticalAllocHandle.FromSize(num + Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative)));
							PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative blobNative;
							blobNative.cbSize = num;
							blobNative.pBlobData = (IntPtr)(blobSafe.pBlobData.ToInt64() + (long)Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative)));
							Marshal.StructureToPtr(blobNative, blobSafe.pBlobData, false);
							PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo source = (PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo)input;
							source.dwSize = num;
							PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfoNative pnrpInfoNative = default(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfoNative);
							PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo.ToPnrpInfoNative(source, ref pnrpInfoNative);
							Marshal.StructureToPtr(pnrpInfoNative, blobNative.pBlobData, false);
							blobSafe.cbSize = num;
						}
						else
						{
							if (!(input.GetType() == typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo)))
							{
								throw Fx.AssertAndThrow("Unknown payload type!");
							}
							int num2 = Marshal.SizeOf(input.GetType());
							blobSafe.pBlobData = CriticalAllocHandle.FromSize(num2 + Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative)));
							PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative blobNative2;
							blobNative2.cbSize = num2;
							blobNative2.pBlobData = (IntPtr)(blobSafe.pBlobData.ToInt64() + (long)Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative)));
							Marshal.StructureToPtr(blobNative2, blobSafe.pBlobData, false);
							PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo pnrpCloudInfo = (PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo)input;
							pnrpCloudInfo.dwSize = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo));
							Marshal.StructureToPtr(pnrpCloudInfo, blobNative2.pBlobData, false);
							blobSafe.cbSize = num2;
						}
					}
					return blobSafe.pBlobData;
				}
			}

			// Token: 0x02000FB5 RID: 4021
			internal class CriticalAllocHandleString : CriticalAllocHandle
			{
				// Token: 0x06008886 RID: 34950 RVA: 0x001FBD60 File Offset: 0x001F9F60
				public static CriticalAllocHandle FromString(string input)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleString criticalAllocHandleString = new PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleString();
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						criticalAllocHandleString.SetHandle(Marshal.StringToHGlobalUni(input));
					}
					return criticalAllocHandleString;
				}
			}

			// Token: 0x02000FB6 RID: 4022
			internal class CriticalAllocHandleWsaQuerySetSafe : CriticalAllocHandle
			{
				// Token: 0x06008888 RID: 34952 RVA: 0x001FBDA0 File Offset: 0x001F9FA0
				private static int CalculateSize(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe safeQuerySet)
				{
					int num = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative));
					if (safeQuerySet.addressList != null)
					{
						num += safeQuerySet.addressList.Length * Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoNative));
					}
					return num;
				}

				// Token: 0x06008889 RID: 34953 RVA: 0x001FBDE4 File Offset: 0x001F9FE4
				public static CriticalAllocHandle FromWsaQuerySetSafe(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe safeQuerySet)
				{
					CriticalAllocHandle criticalAllocHandle = CriticalAllocHandle.FromSize(PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleWsaQuerySetSafe.CalculateSize(safeQuerySet));
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe.StructureToPtr(safeQuerySet, criticalAllocHandle);
					return criticalAllocHandle;
				}
			}

			// Token: 0x02000FB7 RID: 4023
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal class WsaQuerySetSafe : IDisposable
			{
				// Token: 0x0600888B RID: 34955 RVA: 0x001FBE14 File Offset: 0x001FA014
				~WsaQuerySetSafe()
				{
					this.Dispose(false);
				}

				// Token: 0x0600888C RID: 34956 RVA: 0x001FBE44 File Offset: 0x001FA044
				public virtual void Dispose()
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}

				// Token: 0x0600888D RID: 34957 RVA: 0x001FBE54 File Offset: 0x001FA054
				private void Dispose(bool disposing)
				{
					if (!this.disposed && disposing)
					{
						if (this.lpszServiceInstanceName != null)
						{
							this.lpszServiceInstanceName.Dispose();
						}
						if (this.lpServiceClassId != null)
						{
							this.lpServiceClassId.Dispose();
						}
						if (this.lpszComment != null)
						{
							this.lpszComment.Dispose();
						}
						if (this.lpNSProviderId != null)
						{
							this.lpNSProviderId.Dispose();
						}
						if (this.lpBlob != null)
						{
							this.lpBlob.Dispose();
						}
						if (this.addressList != null)
						{
							foreach (PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe csAddrInfoSafe in this.addressList)
							{
								csAddrInfoSafe.Dispose();
							}
						}
					}
					this.disposed = true;
				}

				// Token: 0x0600888E RID: 34958 RVA: 0x001FBF00 File Offset: 0x001FA100
				public static void StructureToPtr(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe input, IntPtr target)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative wsaQuerySetNative = default(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative);
					wsaQuerySetNative.dwSize = input.dwSize;
					wsaQuerySetNative.lpszServiceInstanceName = input.lpszServiceInstanceName;
					wsaQuerySetNative.lpServiceClassId = input.lpServiceClassId;
					wsaQuerySetNative.lpVersion = IntPtr.Zero;
					wsaQuerySetNative.lpszComment = input.lpszComment;
					wsaQuerySetNative.dwNameSpace = input.dwNameSpace;
					wsaQuerySetNative.lpNSProviderId = input.lpNSProviderId;
					wsaQuerySetNative.lpszContext = input.lpszContext;
					wsaQuerySetNative.dwNumberOfProtocols = 0;
					wsaQuerySetNative.lpafpProtocols = IntPtr.Zero;
					wsaQuerySetNative.lpszQueryString = IntPtr.Zero;
					wsaQuerySetNative.dwNumberOfCsAddrs = input.dwNumberOfCsAddrs;
					wsaQuerySetNative.dwOutputFlags = 0;
					wsaQuerySetNative.lpBlob = input.lpBlob;
					long value = target.ToInt64() + (long)Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative));
					wsaQuerySetNative.lpcsaBuffer = (IntPtr)value;
					Marshal.StructureToPtr(wsaQuerySetNative, target, false);
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe.MarshalSafeAddressesToNative(input, (IntPtr)value);
				}

				// Token: 0x0600888F RID: 34959 RVA: 0x001FC01C File Offset: 0x001FA21C
				public static void MarshalSafeAddressesToNative(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe safeQuery, IntPtr target)
				{
					if (safeQuery.addressList != null && safeQuery.addressList.Length != 0)
					{
						int num = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoNative));
						long num2 = target.ToInt64();
						foreach (PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe input in safeQuery.addressList)
						{
							PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe.StructureToPtr(input, (IntPtr)num2);
							num2 += (long)num;
						}
					}
				}

				// Token: 0x04005038 RID: 20536
				public int dwSize;

				// Token: 0x04005039 RID: 20537
				public CriticalAllocHandle lpszServiceInstanceName;

				// Token: 0x0400503A RID: 20538
				public CriticalAllocHandle lpServiceClassId;

				// Token: 0x0400503B RID: 20539
				public IntPtr lpVersion;

				// Token: 0x0400503C RID: 20540
				public CriticalAllocHandle lpszComment;

				// Token: 0x0400503D RID: 20541
				public PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces dwNameSpace;

				// Token: 0x0400503E RID: 20542
				public CriticalAllocHandle lpNSProviderId;

				// Token: 0x0400503F RID: 20543
				public CriticalAllocHandle lpszContext;

				// Token: 0x04005040 RID: 20544
				public int dwNumberOfProtocols;

				// Token: 0x04005041 RID: 20545
				public IntPtr lpafpProtocols;

				// Token: 0x04005042 RID: 20546
				public IntPtr lpszQueryString;

				// Token: 0x04005043 RID: 20547
				public int dwNumberOfCsAddrs;

				// Token: 0x04005044 RID: 20548
				public PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoSafe[] addressList;

				// Token: 0x04005045 RID: 20549
				public int dwOutputFlags;

				// Token: 0x04005046 RID: 20550
				public CriticalAllocHandle lpBlob;

				// Token: 0x04005047 RID: 20551
				private bool disposed;
			}

			// Token: 0x02000FB8 RID: 4024
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
			internal struct WsaQuerySetNative
			{
				// Token: 0x04005048 RID: 20552
				public int dwSize;

				// Token: 0x04005049 RID: 20553
				public IntPtr lpszServiceInstanceName;

				// Token: 0x0400504A RID: 20554
				public IntPtr lpServiceClassId;

				// Token: 0x0400504B RID: 20555
				public IntPtr lpVersion;

				// Token: 0x0400504C RID: 20556
				public IntPtr lpszComment;

				// Token: 0x0400504D RID: 20557
				public PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces dwNameSpace;

				// Token: 0x0400504E RID: 20558
				public IntPtr lpNSProviderId;

				// Token: 0x0400504F RID: 20559
				public IntPtr lpszContext;

				// Token: 0x04005050 RID: 20560
				public int dwNumberOfProtocols;

				// Token: 0x04005051 RID: 20561
				public IntPtr lpafpProtocols;

				// Token: 0x04005052 RID: 20562
				public IntPtr lpszQueryString;

				// Token: 0x04005053 RID: 20563
				public int dwNumberOfCsAddrs;

				// Token: 0x04005054 RID: 20564
				public IntPtr lpcsaBuffer;

				// Token: 0x04005055 RID: 20565
				public int dwOutputFlags;

				// Token: 0x04005056 RID: 20566
				public IntPtr lpBlob;
			}

			// Token: 0x02000FB9 RID: 4025
			internal class CriticalLookupHandle : CriticalHandleZeroOrMinusOneIsInvalid
			{
				// Token: 0x06008891 RID: 34961 RVA: 0x001FC086 File Offset: 0x001FA286
				protected override bool ReleaseHandle()
				{
					return PnrpPeerResolver.UnsafePnrpNativeMethods.WSALookupServiceEnd(this.handle) == 0;
				}
			}

			// Token: 0x02000FBA RID: 4026
			internal class DiscoveryBase : MarshalByRefObject, IDisposable
			{
				// Token: 0x06008893 RID: 34963 RVA: 0x001FC0A0 File Offset: 0x001FA2A0
				public DiscoveryBase()
				{
					object obj = PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase.refCountLock;
					lock (obj)
					{
						if (PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase.refCount == 0)
						{
							PnrpPeerResolver.UnsafePnrpNativeMethods.WsaData wsaData = default(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaData);
							int num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSAStartup(514, ref wsaData);
							if (num != 0)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SocketException(num));
							}
						}
						PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase.refCount++;
					}
				}

				// Token: 0x06008894 RID: 34964 RVA: 0x001FC11C File Offset: 0x001FA31C
				public void Dispose()
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}

				// Token: 0x06008895 RID: 34965 RVA: 0x001FC12C File Offset: 0x001FA32C
				public void Dispose(bool disposing)
				{
					if (!this.disposed)
					{
						object obj = PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase.refCountLock;
						lock (obj)
						{
							PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase.refCount--;
							if (PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase.refCount == 0)
							{
								PnrpPeerResolver.UnsafePnrpNativeMethods.WSACleanup();
							}
						}
					}
					this.disposed = true;
				}

				// Token: 0x06008896 RID: 34966 RVA: 0x001FC190 File Offset: 0x001FA390
				~DiscoveryBase()
				{
					this.Dispose(false);
				}

				// Token: 0x06008897 RID: 34967 RVA: 0x001FC1C0 File Offset: 0x001FA3C0
				public bool IsPnrpServiceRunning(TimeSpan waitForService)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(waitForService);
					bool result;
					try
					{
						using (ServiceController serviceController = new ServiceController("pnrpsvc"))
						{
							try
							{
								if (serviceController.Status == ServiceControllerStatus.StopPending)
								{
									serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeoutHelper.RemainingTime());
								}
								if (serviceController.Status == ServiceControllerStatus.Stopped)
								{
									serviceController.Start();
								}
								serviceController.WaitForStatus(ServiceControllerStatus.Running, timeoutHelper.RemainingTime());
							}
							catch (Exception ex)
							{
								if (Fx.IsFatal(ex))
								{
									throw;
								}
								if (ex is InvalidOperationException || ex is System.ServiceProcess.TimeoutException)
								{
									return false;
								}
								throw;
							}
							result = (serviceController.Status == ServiceControllerStatus.Running);
						}
					}
					catch (InvalidOperationException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						result = false;
					}
					return result;
				}

				// Token: 0x06008898 RID: 34968 RVA: 0x001FC284 File Offset: 0x001FA484
				public bool IsPnrpAvailable(TimeSpan waitForService)
				{
					if (!this.IsPnrpInstalled())
					{
						return false;
					}
					if (!this.IsPnrpServiceRunning(waitForService))
					{
						return false;
					}
					int num = this.InvokeService(new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet
					{
						NSProviderId = PnrpPeerResolver.UnsafePnrpNativeMethods.NsProviderName,
						ServiceClassId = PnrpPeerResolver.UnsafePnrpNativeMethods.SvcIdNameV1
					}, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaSetServiceOp.Register, 0);
					return num == 10022 || num == 11004;
				}

				// Token: 0x06008899 RID: 34969 RVA: 0x001FC2E0 File Offset: 0x001FA4E0
				public bool IsPnrpInstalled()
				{
					int size = 0;
					CriticalAllocHandle safeHandle = null;
					int num;
					for (;;)
					{
						num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSAEnumNameSpaceProviders(ref size, safeHandle);
						if (num != -1)
						{
							goto IL_2F;
						}
						int num2 = PnrpPeerResolver.UnsafePnrpNativeMethods.WSAGetLastError();
						if (num2 != 10014)
						{
							break;
						}
						safeHandle = CriticalAllocHandle.FromSize(size);
					}
					return false;
					IL_2F:
					for (int i = 0; i < num; i++)
					{
						IntPtr ptr = (IntPtr)(safeHandle.ToInt64() + (long)(i * Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNamespaceInfo))));
						PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNamespaceInfo wsaNamespaceInfo = (PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNamespaceInfo)Marshal.PtrToStructure(ptr, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNamespaceInfo));
						if (wsaNamespaceInfo.NSProviderId == PnrpPeerResolver.UnsafePnrpNativeMethods.NsProviderName && wsaNamespaceInfo.fActive != 0)
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x0600889A RID: 34970 RVA: 0x001FC390 File Offset: 0x001FA590
				private int InvokeService(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet registerQuery, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaSetServiceOp op, int flags)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe wsaQuerySetSafe = PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet.ToWsaQuerySetSafe(registerQuery);
					int result = 0;
					using (wsaQuerySetSafe)
					{
						CriticalAllocHandle criticalAllocHandle = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleWsaQuerySetSafe.FromWsaQuerySetSafe(wsaQuerySetSafe);
						using (criticalAllocHandle)
						{
							int num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSASetService(criticalAllocHandle, op, flags);
							if (num != 0)
							{
								result = PnrpPeerResolver.UnsafePnrpNativeMethods.WSAGetLastError();
							}
						}
					}
					return result;
				}

				// Token: 0x04005057 RID: 20567
				private static int refCount = 0;

				// Token: 0x04005058 RID: 20568
				private static object refCountLock = new object();

				// Token: 0x04005059 RID: 20569
				private bool disposed;
			}

			// Token: 0x02000FBB RID: 4027
			public class PeerCloudEnumerator : PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase
			{
				// Token: 0x0600889C RID: 34972 RVA: 0x001FC410 File Offset: 0x001FA610
				public static PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo[] GetClouds()
				{
					int num = 0;
					ArrayList arrayList = new ArrayList();
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet wsaQuerySet = new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet();
					PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo pnrpCloudInfo = default(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo);
					pnrpCloudInfo.dwSize = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo));
					pnrpCloudInfo.Cloud.Scope = PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpScope.Any;
					pnrpCloudInfo.dwCloudState = PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudState.Virtual;
					pnrpCloudInfo.Flags = PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudFlags.None;
					wsaQuerySet.NameSpace = PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces.Cloud;
					wsaQuerySet.NSProviderId = PnrpPeerResolver.UnsafePnrpNativeMethods.NsProviderCloud;
					wsaQuerySet.ServiceClassId = PnrpPeerResolver.UnsafePnrpNativeMethods.SvcIdCloud;
					wsaQuerySet.Blob = pnrpCloudInfo;
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe wsaQuerySetSafe = PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet.ToWsaQuerySetSafe(wsaQuerySet);
					PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalLookupHandle criticalLookupHandle;
					using (wsaQuerySetSafe)
					{
						CriticalAllocHandle query = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleWsaQuerySetSafe.FromWsaQuerySetSafe(wsaQuerySetSafe);
						num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSALookupServiceBegin(query, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNspControlFlags.ReturnAll, out criticalLookupHandle);
					}
					if (num != 0)
					{
						SocketException exception = new SocketException(PnrpPeerResolver.UnsafePnrpNativeMethods.WSAGetLastError());
						Utility.CloseInvalidOutCriticalHandle(criticalLookupHandle);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
					}
					int size = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe)) + 200;
					CriticalAllocHandle criticalAllocHandle = CriticalAllocHandle.FromSize(size);
					using (criticalLookupHandle)
					{
						int num2;
						for (;;)
						{
							num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSALookupServiceNext(criticalLookupHandle, (PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNspControlFlags)0, ref size, criticalAllocHandle);
							if (num != 0)
							{
								num2 = PnrpPeerResolver.UnsafePnrpNativeMethods.WSAGetLastError();
								if (num2 == 10102)
								{
									break;
								}
								if (num2 == 10110)
								{
									break;
								}
								if (num2 != 10014)
								{
									goto IL_142;
								}
								if (criticalAllocHandle != null)
								{
									criticalAllocHandle.Dispose();
								}
								criticalAllocHandle = CriticalAllocHandle.FromSize(size);
							}
							else if (criticalAllocHandle != IntPtr.Zero)
							{
								PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet wsaQuerySet2 = PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver.MarshalWsaQuerySetNativeToWsaQuerySet(criticalAllocHandle, 0U);
								PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo cloudInfo = new PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo();
								PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo pnrpCloudInfo2 = (PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo)wsaQuerySet2.Blob;
								cloudInfo.Name = wsaQuerySet2.ServiceInstanceName;
								cloudInfo.Scope = pnrpCloudInfo2.Cloud.Scope;
								cloudInfo.ScopeId = pnrpCloudInfo2.Cloud.ScopeId;
								cloudInfo.State = pnrpCloudInfo2.dwCloudState;
								cloudInfo.Flags = pnrpCloudInfo2.Flags;
								arrayList.Add(cloudInfo);
							}
						}
						goto IL_1F7;
						IL_142:
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SocketException(num2));
					}
					IL_1F7:
					return (PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo[])arrayList.ToArray(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.CloudInfo));
				}
			}

			// Token: 0x02000FBC RID: 4028
			internal class PeerNameRegistrar : PnrpPeerResolver.UnsafePnrpNativeMethods.DiscoveryBase
			{
				// Token: 0x0600889F RID: 34975 RVA: 0x001FC670 File Offset: 0x001FA870
				public void Register(PnrpPeerResolver.PnrpRegistration registration, TimeSpan timeout)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo pnrpInfo = default(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo);
					pnrpInfo.dwLifetime = 3600;
					pnrpInfo.lpwszIdentity = null;
					pnrpInfo.dwSize = Marshal.SizeOf(pnrpInfo);
					pnrpInfo.dwFlags = 1;
					IPEndPoint hint = PnrpPeerResolver.GetHint();
					pnrpInfo.saHint = PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE.SocketAddressFromIPEndPoint(hint);
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet wsaQuerySet = new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet();
					wsaQuerySet.NameSpace = PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces.Name;
					wsaQuerySet.NSProviderId = PnrpPeerResolver.UnsafePnrpNativeMethods.NsProviderName;
					wsaQuerySet.ServiceClassId = PnrpPeerResolver.UnsafePnrpNativeMethods.SvcIdNameV1;
					wsaQuerySet.ServiceInstanceName = registration.PeerName;
					wsaQuerySet.Comment = registration.Comment;
					wsaQuerySet.Context = registration.CloudName;
					if (registration.Addresses != null)
					{
						wsaQuerySet.CsAddrInfos = new PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfo[registration.Addresses.Length];
						for (int i = 0; i < registration.Addresses.Length; i++)
						{
							wsaQuerySet.CsAddrInfos[i].LocalAddr = registration.Addresses[i];
							wsaQuerySet.CsAddrInfos[i].iProtocol = 6;
							wsaQuerySet.CsAddrInfos[i].iSocketType = 1;
						}
					}
					wsaQuerySet.Blob = pnrpInfo;
					this.RegisterService(wsaQuerySet);
				}

				// Token: 0x060088A0 RID: 34976 RVA: 0x001FC78C File Offset: 0x001FA98C
				public void Unregister(string peerName, List<string> clouds, TimeSpan timeout)
				{
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					foreach (string cloudName in clouds)
					{
						try
						{
							this.Unregister(peerName, cloudName, timeoutHelper.RemainingTime());
						}
						catch (SocketException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						}
					}
				}

				// Token: 0x060088A1 RID: 34977 RVA: 0x001FC804 File Offset: 0x001FAA04
				public void Unregister(string peerName, string cloudName, TimeSpan timeout)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo pnrpInfo = default(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo);
					pnrpInfo.lpwszIdentity = null;
					pnrpInfo.dwSize = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo));
					this.DeleteService(new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet
					{
						NameSpace = PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces.Name,
						NSProviderId = PnrpPeerResolver.UnsafePnrpNativeMethods.NsProviderName,
						ServiceClassId = PnrpPeerResolver.UnsafePnrpNativeMethods.SvcIdNameV1,
						ServiceInstanceName = peerName,
						Context = cloudName,
						Blob = pnrpInfo
					});
				}

				// Token: 0x060088A2 RID: 34978 RVA: 0x001FC87C File Offset: 0x001FAA7C
				private void RegisterService(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet registerQuery)
				{
					try
					{
						PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameRegistrar.InvokeService(registerQuery, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaSetServiceOp.Register, 0);
					}
					catch (PnrpPeerResolver.PnrpException)
					{
						if (4 >= registerQuery.CsAddrInfos.Length)
						{
							throw;
						}
						List<PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfo> list = new List<PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfo>(registerQuery.CsAddrInfos);
						list.RemoveRange(4, registerQuery.CsAddrInfos.Length - 4);
						registerQuery.CsAddrInfos = list.ToArray();
						PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameRegistrar.InvokeService(registerQuery, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaSetServiceOp.Register, 0);
					}
				}

				// Token: 0x060088A3 RID: 34979 RVA: 0x001FC8E8 File Offset: 0x001FAAE8
				private void DeleteService(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet registerQuery)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameRegistrar.InvokeService(registerQuery, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaSetServiceOp.Delete, 0);
				}

				// Token: 0x060088A4 RID: 34980 RVA: 0x001FC8F4 File Offset: 0x001FAAF4
				private static void InvokeService(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet registerQuery, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaSetServiceOp op, int flags)
				{
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe wsaQuerySetSafe = PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet.ToWsaQuerySetSafe(registerQuery);
					using (wsaQuerySetSafe)
					{
						CriticalAllocHandle querySet = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleWsaQuerySetSafe.FromWsaQuerySetSafe(wsaQuerySetSafe);
						int num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSASetService(querySet, op, flags);
						if (num != 0)
						{
							int errorCode = PnrpPeerResolver.UnsafePnrpNativeMethods.WSAGetLastError();
							PeerExceptionHelper.ThrowPnrpError(errorCode, registerQuery.Context);
						}
					}
				}

				// Token: 0x0400505A RID: 20570
				private const int RegistrationLifetime = 3600;
			}

			// Token: 0x02000FBD RID: 4029
			internal class PeerNameResolver : AsyncResult
			{
				// Token: 0x060088A5 RID: 34981 RVA: 0x001FC950 File Offset: 0x001FAB50
				public PeerNameResolver(string peerName, int numberOfResultsRequested, PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria resolveCriteria, TimeSpan timeout, List<PnrpPeerResolver.PnrpRegistration> results) : this(peerName, numberOfResultsRequested, resolveCriteria, 0U, "Global_", timeout, results)
				{
				}

				// Token: 0x060088A6 RID: 34982 RVA: 0x001FC968 File Offset: 0x001FAB68
				public PeerNameResolver(string peerName, int numberOfResultsRequested, PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpResolveCriteria resolveCriteria, uint scopeId, string cloudName, TimeSpan timeout, List<PnrpPeerResolver.PnrpRegistration> results) : base(null, null)
				{
					if (timeout > PnrpPeerResolver.MaxTimeout)
					{
						timeout = PnrpPeerResolver.MaxTimeout;
					}
					this.timeoutHelper = new TimeoutHelper(timeout);
					PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo pnrpInfo = default(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo);
					pnrpInfo.dwSize = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo));
					pnrpInfo.nMaxResolve = numberOfResultsRequested;
					pnrpInfo.dwTimeout = (int)timeout.TotalSeconds;
					pnrpInfo.dwLifetime = 0;
					pnrpInfo.enNameState = (PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpRegisteredIdState)0;
					pnrpInfo.lpwszIdentity = null;
					pnrpInfo.dwFlags = 1;
					IPEndPoint hint = PnrpPeerResolver.GetHint();
					pnrpInfo.enResolveCriteria = resolveCriteria;
					pnrpInfo.saHint = PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_SAFE.SocketAddressFromIPEndPoint(hint);
					this.resolveQuery = new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet();
					this.resolveQuery.ServiceInstanceName = peerName;
					this.resolveQuery.ServiceClassId = PnrpPeerResolver.UnsafePnrpNativeMethods.SvcIdNameV1;
					this.resolveQuery.NameSpace = PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces.Name;
					this.resolveQuery.NSProviderId = PnrpPeerResolver.UnsafePnrpNativeMethods.NsProviderName;
					this.resolveQuery.Context = cloudName;
					this.resolveQuery.Blob = pnrpInfo;
					this.results = results;
					this.scopeId = scopeId;
					ActionItem.Schedule(new Action<object>(this.SyncEnumeration), null);
				}

				// Token: 0x060088A7 RID: 34983 RVA: 0x001FCA93 File Offset: 0x001FAC93
				public void End()
				{
					AsyncResult.End<PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver>(this);
				}

				// Token: 0x060088A8 RID: 34984 RVA: 0x001FCA9C File Offset: 0x001FAC9C
				public void SyncEnumeration(object state)
				{
					int num = 0;
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe wsaQuerySetSafe = PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet.ToWsaQuerySetSafe(this.resolveQuery);
					PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalLookupHandle criticalLookupHandle;
					using (wsaQuerySetSafe)
					{
						CriticalAllocHandle query = PnrpPeerResolver.UnsafePnrpNativeMethods.CriticalAllocHandleWsaQuerySetSafe.FromWsaQuerySetSafe(wsaQuerySetSafe);
						num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSALookupServiceBegin(query, PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNspControlFlags.ReturnAll, out criticalLookupHandle);
					}
					if (num != 0)
					{
						this.lastException = new PnrpPeerResolver.PnrpException(PnrpPeerResolver.UnsafePnrpNativeMethods.WSAGetLastError(), this.resolveQuery.Context);
						Utility.CloseInvalidOutCriticalHandle(criticalLookupHandle);
						base.Complete(false, this.lastException);
						return;
					}
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet wsaQuerySet = new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet();
					int size = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetSafe)) + 400;
					CriticalAllocHandle safeHandle = CriticalAllocHandle.FromSize(size);
					try
					{
						using (criticalLookupHandle)
						{
							while (!(this.timeoutHelper.RemainingTime() == TimeSpan.Zero))
							{
								num = PnrpPeerResolver.UnsafePnrpNativeMethods.WSALookupServiceNext(criticalLookupHandle, (PnrpPeerResolver.UnsafePnrpNativeMethods.WsaNspControlFlags)0, ref size, safeHandle);
								if (num != 0)
								{
									int num2 = PnrpPeerResolver.UnsafePnrpNativeMethods.WSAGetLastError();
									if (num2 == 10102 || num2 == 10110)
									{
										break;
									}
									if (num2 == 10014)
									{
										safeHandle = CriticalAllocHandle.FromSize(size);
									}
									else
									{
										PeerExceptionHelper.ThrowPnrpError(num2, wsaQuerySet.Context);
									}
								}
								else if (safeHandle != IntPtr.Zero)
								{
									wsaQuerySet = PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver.MarshalWsaQuerySetNativeToWsaQuerySet(safeHandle, this.scopeId);
									PnrpPeerResolver.PnrpRegistration pnrpRegistration = new PnrpPeerResolver.PnrpRegistration();
									pnrpRegistration.CloudName = wsaQuerySet.Context;
									pnrpRegistration.Comment = wsaQuerySet.Comment;
									pnrpRegistration.PeerName = wsaQuerySet.ServiceInstanceName;
									pnrpRegistration.Addresses = new IPEndPoint[wsaQuerySet.CsAddrInfos.Length];
									for (int i = 0; i < wsaQuerySet.CsAddrInfos.Length; i++)
									{
										pnrpRegistration.Addresses[i] = wsaQuerySet.CsAddrInfos[i].LocalAddr;
									}
									List<PnrpPeerResolver.PnrpRegistration> obj = this.results;
									lock (obj)
									{
										this.results.Add(pnrpRegistration);
										continue;
									}
									break;
								}
							}
						}
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							PnrpResolveExceptionTraceRecord extendedData = new PnrpResolveExceptionTraceRecord(this.resolveQuery.ServiceInstanceName, this.resolveQuery.Context, exception);
							if (DiagnosticUtility.ShouldTraceError)
							{
								TraceUtility.TraceEvent(TraceEventType.Error, 262219, SR.GetString("TraceCodePnrpResolveException"), extendedData, this, null);
							}
						}
						this.lastException = exception;
					}
					finally
					{
						base.Complete(false, this.lastException);
					}
				}

				// Token: 0x060088A9 RID: 34985 RVA: 0x001FCD7C File Offset: 0x001FAF7C
				internal static PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet MarshalWsaQuerySetNativeToWsaQuerySet(IntPtr pNativeData)
				{
					return PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver.MarshalWsaQuerySetNativeToWsaQuerySet(pNativeData, 0U);
				}

				// Token: 0x060088AA RID: 34986 RVA: 0x001FCD88 File Offset: 0x001FAF88
				internal static PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet MarshalWsaQuerySetNativeToWsaQuerySet(IntPtr pNativeData, uint scopeId)
				{
					if (pNativeData == IntPtr.Zero)
					{
						return null;
					}
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet wsaQuerySet = new PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet();
					PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative wsaQuerySetNative = (PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative)Marshal.PtrToStructure(pNativeData, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySetNative));
					int num = Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoNative));
					wsaQuerySet.Context = Marshal.PtrToStringUni(wsaQuerySetNative.lpszContext);
					wsaQuerySet.NameSpace = wsaQuerySetNative.dwNameSpace;
					wsaQuerySet.ServiceInstanceName = Marshal.PtrToStringUni(wsaQuerySetNative.lpszServiceInstanceName);
					wsaQuerySet.Comment = Marshal.PtrToStringUni(wsaQuerySetNative.lpszComment);
					wsaQuerySet.CsAddrInfos = new PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfo[wsaQuerySetNative.dwNumberOfCsAddrs];
					for (int i = 0; i < wsaQuerySetNative.dwNumberOfCsAddrs; i++)
					{
						IntPtr ptr = (IntPtr)(wsaQuerySetNative.lpcsaBuffer.ToInt64() + (long)(i * num));
						PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoNative csAddrInfoNative = (PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoNative)Marshal.PtrToStructure(ptr, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.CsAddrInfoNative));
						wsaQuerySet.CsAddrInfos[i].iProtocol = csAddrInfoNative.iProtocol;
						wsaQuerySet.CsAddrInfos[i].iSocketType = csAddrInfoNative.iSocketType;
						wsaQuerySet.CsAddrInfos[i].LocalAddr = PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver.IPEndPointFromSocketAddress(csAddrInfoNative.LocalAddr, scopeId);
						wsaQuerySet.CsAddrInfos[i].RemoteAddr = PnrpPeerResolver.UnsafePnrpNativeMethods.PeerNameResolver.IPEndPointFromSocketAddress(csAddrInfoNative.RemoteAddr, scopeId);
					}
					if (wsaQuerySetNative.lpNSProviderId != IntPtr.Zero)
					{
						wsaQuerySet.NSProviderId = (Guid)Marshal.PtrToStructure(wsaQuerySetNative.lpNSProviderId, typeof(Guid));
					}
					if (wsaQuerySetNative.lpServiceClassId != IntPtr.Zero)
					{
						wsaQuerySet.ServiceClassId = (Guid)Marshal.PtrToStructure(wsaQuerySetNative.lpServiceClassId, typeof(Guid));
					}
					if (wsaQuerySet.NameSpace == PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces.Cloud)
					{
						if (wsaQuerySetNative.lpBlob != IntPtr.Zero)
						{
							wsaQuerySet.Blob = default(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo);
							PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative blobNative = (PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative)Marshal.PtrToStructure(wsaQuerySetNative.lpBlob, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.BlobNative));
							if (blobNative.pBlobData != IntPtr.Zero)
							{
								wsaQuerySet.Blob = (PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo)Marshal.PtrToStructure(blobNative.pBlobData, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpCloudInfo));
							}
						}
					}
					else if (wsaQuerySet.NameSpace == PnrpPeerResolver.UnsafePnrpNativeMethods.NspNamespaces.Name && wsaQuerySetNative.lpBlob != IntPtr.Zero)
					{
						wsaQuerySet.Blob = default(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo);
						PnrpPeerResolver.UnsafePnrpNativeMethods.BlobSafe blobSafe = (PnrpPeerResolver.UnsafePnrpNativeMethods.BlobSafe)Marshal.PtrToStructure(wsaQuerySetNative.lpBlob, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.BlobSafe));
						if (blobSafe.pBlobData != IntPtr.Zero)
						{
							PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo pnrpInfo = (PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo)Marshal.PtrToStructure(blobSafe.pBlobData, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.PnrpInfo));
							wsaQuerySet.Blob = pnrpInfo;
						}
					}
					return wsaQuerySet;
				}

				// Token: 0x060088AB RID: 34987 RVA: 0x001FD068 File Offset: 0x001FB268
				private static IPEndPoint IPEndPointFromSocketAddress(PnrpPeerResolver.UnsafePnrpNativeMethods.SOCKET_ADDRESS_NATIVE socketAddress, uint scopeId)
				{
					IPEndPoint result = null;
					if (socketAddress.lpSockAddr != IntPtr.Zero)
					{
						AddressFamily addressFamily = (AddressFamily)Marshal.ReadInt16(socketAddress.lpSockAddr);
						if (addressFamily == AddressFamily.InterNetwork)
						{
							if (socketAddress.iSockaddrLength == Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in)))
							{
								PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in sockaddr_in = (PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in)Marshal.PtrToStructure(socketAddress.lpSockAddr, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in));
								result = new IPEndPoint(new IPAddress(sockaddr_in.sin_addr), (int)sockaddr_in.sin_port);
							}
						}
						else if (addressFamily == AddressFamily.InterNetworkV6 && socketAddress.iSockaddrLength == Marshal.SizeOf(typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in6)))
						{
							PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in6 sockaddr_in2 = (PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in6)Marshal.PtrToStructure(socketAddress.lpSockAddr, typeof(PnrpPeerResolver.UnsafePnrpNativeMethods.sockaddr_in6));
							if (scopeId != 0U && sockaddr_in2.sin6_scope_id != 0U)
							{
								scopeId = sockaddr_in2.sin6_scope_id;
							}
							result = new IPEndPoint(new IPAddress(sockaddr_in2.sin6_addr, (long)((ulong)scopeId)), (int)sockaddr_in2.sin6_port);
						}
					}
					return result;
				}

				// Token: 0x0400505B RID: 20571
				private PnrpPeerResolver.UnsafePnrpNativeMethods.WsaQuerySet resolveQuery;

				// Token: 0x0400505C RID: 20572
				private List<PnrpPeerResolver.PnrpRegistration> results;

				// Token: 0x0400505D RID: 20573
				private uint scopeId;

				// Token: 0x0400505E RID: 20574
				private Exception lastException;

				// Token: 0x0400505F RID: 20575
				private TimeoutHelper timeoutHelper;
			}
		}
	}
}
