using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000613 RID: 1555
	public sealed class NetworkChange
	{
		// Token: 0x06002FFC RID: 12284 RVA: 0x000CF5C2 File Offset: 0x000CE5C2
		private NetworkChange()
		{
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06002FFD RID: 12285 RVA: 0x000CF5CA File Offset: 0x000CE5CA
		// (remove) Token: 0x06002FFE RID: 12286 RVA: 0x000CF5E9 File Offset: 0x000CE5E9
		public static event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				if (!ComNetOS.IsWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
				}
				NetworkChange.AvailabilityChangeListener.Start(value);
			}
			remove
			{
				NetworkChange.AvailabilityChangeListener.Stop(value);
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06002FFF RID: 12287 RVA: 0x000CF5F1 File Offset: 0x000CE5F1
		// (remove) Token: 0x06003000 RID: 12288 RVA: 0x000CF610 File Offset: 0x000CE610
		public static event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			add
			{
				if (!ComNetOS.IsWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
				}
				NetworkChange.AddressChangeListener.Start(value);
			}
			remove
			{
				NetworkChange.AddressChangeListener.Stop(value);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000CF618 File Offset: 0x000CE618
		internal static bool CanListenForNetworkChanges
		{
			get
			{
				return ComNetOS.IsWin2K;
			}
		}

		// Token: 0x02000614 RID: 1556
		internal static class AvailabilityChangeListener
		{
			// Token: 0x06003002 RID: 12290 RVA: 0x000CF624 File Offset: 0x000CE624
			private static void RunHandlerCallback(object state)
			{
				((NetworkAvailabilityChangedEventHandler)state)(null, new NetworkAvailabilityEventArgs(NetworkChange.AvailabilityChangeListener.isAvailable));
			}

			// Token: 0x06003003 RID: 12291 RVA: 0x000CF63C File Offset: 0x000CE63C
			private static void ChangedAddress(object sender, EventArgs eventArgs)
			{
				lock (NetworkChange.AvailabilityChangeListener.syncObject)
				{
					bool flag = SystemNetworkInterface.InternalGetIsNetworkAvailable();
					if (flag != NetworkChange.AvailabilityChangeListener.isAvailable)
					{
						NetworkChange.AvailabilityChangeListener.isAvailable = flag;
						DictionaryEntry[] array = new DictionaryEntry[NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count];
						NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.CopyTo(array, 0);
						for (int i = 0; i < array.Length; i++)
						{
							NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = (NetworkAvailabilityChangedEventHandler)array[i].Key;
							ExecutionContext executionContext = (ExecutionContext)array[i].Value;
							if (executionContext == null)
							{
								networkAvailabilityChangedEventHandler(null, new NetworkAvailabilityEventArgs(NetworkChange.AvailabilityChangeListener.isAvailable));
							}
							else
							{
								ExecutionContext.Run(executionContext.CreateCopy(), NetworkChange.AvailabilityChangeListener.s_RunHandlerCallback, networkAvailabilityChangedEventHandler);
							}
						}
					}
				}
			}

			// Token: 0x06003004 RID: 12292 RVA: 0x000CF6FC File Offset: 0x000CE6FC
			internal static void Start(NetworkAvailabilityChangedEventHandler caller)
			{
				lock (NetworkChange.AvailabilityChangeListener.syncObject)
				{
					if (NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray == null)
					{
						NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray = new ListDictionary();
						NetworkChange.AvailabilityChangeListener.addressChange = new NetworkAddressChangedEventHandler(NetworkChange.AvailabilityChangeListener.ChangedAddress);
					}
					if (NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count == 0)
					{
						NetworkChange.AvailabilityChangeListener.isAvailable = NetworkInterface.GetIsNetworkAvailable();
						NetworkChange.AddressChangeListener.UnsafeStart(NetworkChange.AvailabilityChangeListener.addressChange);
					}
					if (caller != null && !NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Contains(caller))
					{
						NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Add(caller, ExecutionContext.Capture());
					}
				}
			}

			// Token: 0x06003005 RID: 12293 RVA: 0x000CF790 File Offset: 0x000CE790
			internal static void Stop(NetworkAvailabilityChangedEventHandler caller)
			{
				lock (NetworkChange.AvailabilityChangeListener.syncObject)
				{
					NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Remove(caller);
					if (NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count == 0)
					{
						NetworkChange.AddressChangeListener.Stop(NetworkChange.AvailabilityChangeListener.addressChange);
					}
				}
			}

			// Token: 0x04002DD2 RID: 11730
			private static object syncObject = new object();

			// Token: 0x04002DD3 RID: 11731
			private static ListDictionary s_availabilityCallerArray = null;

			// Token: 0x04002DD4 RID: 11732
			private static NetworkAddressChangedEventHandler addressChange = null;

			// Token: 0x04002DD5 RID: 11733
			private static bool isAvailable = false;

			// Token: 0x04002DD6 RID: 11734
			private static ContextCallback s_RunHandlerCallback = new ContextCallback(NetworkChange.AvailabilityChangeListener.RunHandlerCallback);
		}

		// Token: 0x02000615 RID: 1557
		internal static class AddressChangeListener
		{
			// Token: 0x06003007 RID: 12295 RVA: 0x000CF814 File Offset: 0x000CE814
			private static void AddressChangedCallback(object stateObject, bool signaled)
			{
				lock (NetworkChange.AddressChangeListener.s_callerArray)
				{
					NetworkChange.AddressChangeListener.s_isPending = false;
					if (NetworkChange.AddressChangeListener.s_isListening)
					{
						NetworkChange.AddressChangeListener.s_isListening = false;
						DictionaryEntry[] array = new DictionaryEntry[NetworkChange.AddressChangeListener.s_callerArray.Count];
						NetworkChange.AddressChangeListener.s_callerArray.CopyTo(array, 0);
						NetworkChange.AddressChangeListener.StartHelper(null, false, (StartIPOptions)stateObject);
						for (int i = 0; i < array.Length; i++)
						{
							NetworkAddressChangedEventHandler networkAddressChangedEventHandler = (NetworkAddressChangedEventHandler)array[i].Key;
							ExecutionContext executionContext = (ExecutionContext)array[i].Value;
							if (executionContext == null)
							{
								networkAddressChangedEventHandler(null, EventArgs.Empty);
							}
							else
							{
								ExecutionContext.Run(executionContext.CreateCopy(), NetworkChange.AddressChangeListener.s_runHandlerCallback, networkAddressChangedEventHandler);
							}
						}
					}
				}
			}

			// Token: 0x06003008 RID: 12296 RVA: 0x000CF8E0 File Offset: 0x000CE8E0
			private static void RunHandlerCallback(object state)
			{
				((NetworkAddressChangedEventHandler)state)(null, EventArgs.Empty);
			}

			// Token: 0x06003009 RID: 12297 RVA: 0x000CF8F3 File Offset: 0x000CE8F3
			internal static void Start(NetworkAddressChangedEventHandler caller)
			{
				NetworkChange.AddressChangeListener.StartHelper(caller, true, StartIPOptions.Both);
			}

			// Token: 0x0600300A RID: 12298 RVA: 0x000CF8FD File Offset: 0x000CE8FD
			internal static void UnsafeStart(NetworkAddressChangedEventHandler caller)
			{
				NetworkChange.AddressChangeListener.StartHelper(caller, false, StartIPOptions.Both);
			}

			// Token: 0x0600300B RID: 12299 RVA: 0x000CF908 File Offset: 0x000CE908
			private static void StartHelper(NetworkAddressChangedEventHandler caller, bool captureContext, StartIPOptions startIPOptions)
			{
				lock (NetworkChange.AddressChangeListener.s_callerArray)
				{
					if (NetworkChange.AddressChangeListener.s_ipv4Socket == null)
					{
						Socket.InitializeSockets();
						if (Socket.SupportsIPv4)
						{
							int num = -1;
							NetworkChange.AddressChangeListener.s_ipv4Socket = SafeCloseSocketAndEvent.CreateWSASocketWithEvent(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP, true, false);
							UnsafeNclNativeMethods.OSSOCK.ioctlsocket(NetworkChange.AddressChangeListener.s_ipv4Socket, -2147195266, ref num);
							NetworkChange.AddressChangeListener.s_ipv4WaitHandle = NetworkChange.AddressChangeListener.s_ipv4Socket.GetEventHandle();
						}
						if (Socket.OSSupportsIPv6)
						{
							int num = -1;
							NetworkChange.AddressChangeListener.s_ipv6Socket = SafeCloseSocketAndEvent.CreateWSASocketWithEvent(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.IP, true, false);
							UnsafeNclNativeMethods.OSSOCK.ioctlsocket(NetworkChange.AddressChangeListener.s_ipv6Socket, -2147195266, ref num);
							NetworkChange.AddressChangeListener.s_ipv6WaitHandle = NetworkChange.AddressChangeListener.s_ipv6Socket.GetEventHandle();
						}
					}
					if (caller != null && !NetworkChange.AddressChangeListener.s_callerArray.Contains(caller))
					{
						NetworkChange.AddressChangeListener.s_callerArray.Add(caller, captureContext ? ExecutionContext.Capture() : null);
					}
					if (!NetworkChange.AddressChangeListener.s_isListening && NetworkChange.AddressChangeListener.s_callerArray.Count != 0)
					{
						if (!NetworkChange.AddressChangeListener.s_isPending)
						{
							if (Socket.SupportsIPv4 && (startIPOptions & StartIPOptions.StartIPv4) != StartIPOptions.None)
							{
								NetworkChange.AddressChangeListener.s_registeredWait = ThreadPool.UnsafeRegisterWaitForSingleObject(NetworkChange.AddressChangeListener.s_ipv4WaitHandle, new WaitOrTimerCallback(NetworkChange.AddressChangeListener.AddressChangedCallback), StartIPOptions.StartIPv4, -1, true);
								int num2;
								SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking(NetworkChange.AddressChangeListener.s_ipv4Socket.DangerousGetHandle(), 671088663, null, 0, null, 0, out num2, SafeNativeOverlapped.Zero, IntPtr.Zero);
								if (socketError != SocketError.Success)
								{
									NetworkInformationException ex = new NetworkInformationException();
									if ((long)ex.ErrorCode != 10035L)
									{
										throw ex;
									}
								}
								socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(NetworkChange.AddressChangeListener.s_ipv4Socket, NetworkChange.AddressChangeListener.s_ipv4Socket.GetEventHandle().SafeWaitHandle, AsyncEventBits.FdAddressListChange);
								if (socketError != SocketError.Success)
								{
									throw new NetworkInformationException();
								}
							}
							if (Socket.OSSupportsIPv6 && (startIPOptions & StartIPOptions.StartIPv6) != StartIPOptions.None)
							{
								NetworkChange.AddressChangeListener.s_registeredWait = ThreadPool.UnsafeRegisterWaitForSingleObject(NetworkChange.AddressChangeListener.s_ipv6WaitHandle, new WaitOrTimerCallback(NetworkChange.AddressChangeListener.AddressChangedCallback), StartIPOptions.StartIPv6, -1, true);
								int num2;
								SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAIoctl_Blocking(NetworkChange.AddressChangeListener.s_ipv6Socket.DangerousGetHandle(), 671088663, null, 0, null, 0, out num2, SafeNativeOverlapped.Zero, IntPtr.Zero);
								if (socketError != SocketError.Success)
								{
									NetworkInformationException ex2 = new NetworkInformationException();
									if ((long)ex2.ErrorCode != 10035L)
									{
										throw ex2;
									}
								}
								socketError = UnsafeNclNativeMethods.OSSOCK.WSAEventSelect(NetworkChange.AddressChangeListener.s_ipv6Socket, NetworkChange.AddressChangeListener.s_ipv6Socket.GetEventHandle().SafeWaitHandle, AsyncEventBits.FdAddressListChange);
								if (socketError != SocketError.Success)
								{
									throw new NetworkInformationException();
								}
							}
						}
						NetworkChange.AddressChangeListener.s_isListening = true;
						NetworkChange.AddressChangeListener.s_isPending = true;
					}
				}
			}

			// Token: 0x0600300C RID: 12300 RVA: 0x000CFB58 File Offset: 0x000CEB58
			internal static void Stop(object caller)
			{
				lock (NetworkChange.AddressChangeListener.s_callerArray)
				{
					NetworkChange.AddressChangeListener.s_callerArray.Remove(caller);
					if (NetworkChange.AddressChangeListener.s_callerArray.Count == 0 && NetworkChange.AddressChangeListener.s_isListening)
					{
						NetworkChange.AddressChangeListener.s_isListening = false;
					}
				}
			}

			// Token: 0x04002DD7 RID: 11735
			private static ListDictionary s_callerArray = new ListDictionary();

			// Token: 0x04002DD8 RID: 11736
			private static ContextCallback s_runHandlerCallback = new ContextCallback(NetworkChange.AddressChangeListener.RunHandlerCallback);

			// Token: 0x04002DD9 RID: 11737
			private static RegisteredWaitHandle s_registeredWait;

			// Token: 0x04002DDA RID: 11738
			private static bool s_isListening = false;

			// Token: 0x04002DDB RID: 11739
			private static bool s_isPending = false;

			// Token: 0x04002DDC RID: 11740
			private static SafeCloseSocketAndEvent s_ipv4Socket = null;

			// Token: 0x04002DDD RID: 11741
			private static SafeCloseSocketAndEvent s_ipv6Socket = null;

			// Token: 0x04002DDE RID: 11742
			private static WaitHandle s_ipv4WaitHandle = null;

			// Token: 0x04002DDF RID: 11743
			private static WaitHandle s_ipv6WaitHandle = null;
		}
	}
}
