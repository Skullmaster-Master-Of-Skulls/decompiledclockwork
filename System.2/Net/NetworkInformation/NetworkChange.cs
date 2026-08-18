using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002DC RID: 732
	[__DynamicallyInvokable]
	public class NetworkChange
	{
		// Token: 0x060019E2 RID: 6626 RVA: 0x0007E4F2 File Offset: 0x0007C6F2
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public NetworkChange()
		{
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0007E4FA File Offset: 0x0007C6FA
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void RegisterNetworkChange(NetworkChange nc)
		{
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060019E4 RID: 6628 RVA: 0x0007E4FC File Offset: 0x0007C6FC
		// (remove) Token: 0x060019E5 RID: 6629 RVA: 0x0007E504 File Offset: 0x0007C704
		public static event NetworkAvailabilityChangedEventHandler NetworkAvailabilityChanged
		{
			add
			{
				NetworkChange.AvailabilityChangeListener.Start(value);
			}
			remove
			{
				NetworkChange.AvailabilityChangeListener.Stop(value);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060019E6 RID: 6630 RVA: 0x0007E50C File Offset: 0x0007C70C
		// (remove) Token: 0x060019E7 RID: 6631 RVA: 0x0007E514 File Offset: 0x0007C714
		[__DynamicallyInvokable]
		public static event NetworkAddressChangedEventHandler NetworkAddressChanged
		{
			[__DynamicallyInvokable]
			add
			{
				NetworkChange.AddressChangeListener.Start(value);
			}
			[__DynamicallyInvokable]
			remove
			{
				NetworkChange.AddressChangeListener.Stop(value);
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0007E51C File Offset: 0x0007C71C
		internal static bool CanListenForNetworkChanges
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001A53 RID: 6739
		private static readonly object s_globalLock = new object();

		// Token: 0x04001A54 RID: 6740
		private static readonly object s_protectCallbackLock = new object();

		// Token: 0x020007A4 RID: 1956
		internal static class AvailabilityChangeListener
		{
			// Token: 0x06004316 RID: 17174 RVA: 0x001196D9 File Offset: 0x001178D9
			private static void RunHandlerCallback(object state)
			{
				((NetworkAvailabilityChangedEventHandler)state)(null, new NetworkAvailabilityEventArgs(NetworkChange.AvailabilityChangeListener.isAvailable));
			}

			// Token: 0x06004317 RID: 17175 RVA: 0x001196F4 File Offset: 0x001178F4
			private static void ChangedAddress(object sender, EventArgs eventArgs)
			{
				DictionaryEntry[] array = null;
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					bool flag2 = SystemNetworkInterface.InternalGetIsNetworkAvailable();
					if (flag2 != NetworkChange.AvailabilityChangeListener.isAvailable)
					{
						NetworkChange.AvailabilityChangeListener.isAvailable = flag2;
						array = new DictionaryEntry[NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count];
						NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.CopyTo(array, 0);
					}
				}
				if (array != null)
				{
					object s_protectCallbackLock = NetworkChange.s_protectCallbackLock;
					lock (s_protectCallbackLock)
					{
						foreach (DictionaryEntry dictionaryEntry in array)
						{
							NetworkAvailabilityChangedEventHandler networkAvailabilityChangedEventHandler = (NetworkAvailabilityChangedEventHandler)dictionaryEntry.Key;
							ExecutionContext executionContext = (ExecutionContext)dictionaryEntry.Value;
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

			// Token: 0x06004318 RID: 17176 RVA: 0x00119800 File Offset: 0x00117A00
			internal static void Start(NetworkAvailabilityChangedEventHandler caller)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
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

			// Token: 0x06004319 RID: 17177 RVA: 0x0011987C File Offset: 0x00117A7C
			internal static void Stop(NetworkAvailabilityChangedEventHandler caller)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Remove(caller);
					if (NetworkChange.AvailabilityChangeListener.s_availabilityCallerArray.Count == 0)
					{
						NetworkChange.AddressChangeListener.Stop(NetworkChange.AvailabilityChangeListener.addressChange);
					}
				}
			}

			// Token: 0x040033DA RID: 13274
			private static ListDictionary s_availabilityCallerArray = new ListDictionary();

			// Token: 0x040033DB RID: 13275
			private static NetworkAddressChangedEventHandler addressChange = new NetworkAddressChangedEventHandler(NetworkChange.AvailabilityChangeListener.ChangedAddress);

			// Token: 0x040033DC RID: 13276
			private static volatile bool isAvailable = false;

			// Token: 0x040033DD RID: 13277
			private static ContextCallback s_RunHandlerCallback = new ContextCallback(NetworkChange.AvailabilityChangeListener.RunHandlerCallback);
		}

		// Token: 0x020007A5 RID: 1957
		internal static class AddressChangeListener
		{
			// Token: 0x0600431B RID: 17179 RVA: 0x00119910 File Offset: 0x00117B10
			private static void AddressChangedCallback(object stateObject, bool signaled)
			{
				DictionaryEntry[] array = null;
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					NetworkChange.AddressChangeListener.s_isPending = false;
					if (!NetworkChange.AddressChangeListener.s_isListening)
					{
						return;
					}
					NetworkChange.AddressChangeListener.s_isListening = false;
					if (NetworkChange.AddressChangeListener.s_callerArray.Count > 0)
					{
						array = new DictionaryEntry[NetworkChange.AddressChangeListener.s_callerArray.Count];
						NetworkChange.AddressChangeListener.s_callerArray.CopyTo(array, 0);
					}
					try
					{
						NetworkChange.AddressChangeListener.StartHelper(null, false, (StartIPOptions)stateObject);
					}
					catch (NetworkInformationException e)
					{
						if (Logging.On)
						{
							Logging.Exception(Logging.Web, "AddressChangeListener", "AddressChangedCallback", e);
						}
					}
				}
				if (array != null)
				{
					object s_protectCallbackLock = NetworkChange.s_protectCallbackLock;
					lock (s_protectCallbackLock)
					{
						foreach (DictionaryEntry dictionaryEntry in array)
						{
							NetworkAddressChangedEventHandler networkAddressChangedEventHandler = (NetworkAddressChangedEventHandler)dictionaryEntry.Key;
							ExecutionContext executionContext = (ExecutionContext)dictionaryEntry.Value;
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

			// Token: 0x0600431C RID: 17180 RVA: 0x00119A5C File Offset: 0x00117C5C
			private static void RunHandlerCallback(object state)
			{
				((NetworkAddressChangedEventHandler)state)(null, EventArgs.Empty);
			}

			// Token: 0x0600431D RID: 17181 RVA: 0x00119A6F File Offset: 0x00117C6F
			internal static void Start(NetworkAddressChangedEventHandler caller)
			{
				NetworkChange.AddressChangeListener.StartHelper(caller, true, StartIPOptions.Both);
			}

			// Token: 0x0600431E RID: 17182 RVA: 0x00119A79 File Offset: 0x00117C79
			internal static void UnsafeStart(NetworkAddressChangedEventHandler caller)
			{
				NetworkChange.AddressChangeListener.StartHelper(caller, false, StartIPOptions.Both);
			}

			// Token: 0x0600431F RID: 17183 RVA: 0x00119A84 File Offset: 0x00117C84
			private static void StartHelper(NetworkAddressChangedEventHandler caller, bool captureContext, StartIPOptions startIPOptions)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					if (NetworkChange.AddressChangeListener.s_ipv4Socket == null)
					{
						Socket.InitializeSockets();
						if (Socket.OSSupportsIPv4)
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
							if (Socket.OSSupportsIPv4 && (startIPOptions & StartIPOptions.StartIPv4) != StartIPOptions.None)
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

			// Token: 0x06004320 RID: 17184 RVA: 0x00119CE4 File Offset: 0x00117EE4
			internal static void Stop(object caller)
			{
				object s_globalLock = NetworkChange.s_globalLock;
				lock (s_globalLock)
				{
					NetworkChange.AddressChangeListener.s_callerArray.Remove(caller);
					if (NetworkChange.AddressChangeListener.s_callerArray.Count == 0 && NetworkChange.AddressChangeListener.s_isListening)
					{
						NetworkChange.AddressChangeListener.s_isListening = false;
					}
				}
			}

			// Token: 0x040033DE RID: 13278
			private static ListDictionary s_callerArray = new ListDictionary();

			// Token: 0x040033DF RID: 13279
			private static ContextCallback s_runHandlerCallback = new ContextCallback(NetworkChange.AddressChangeListener.RunHandlerCallback);

			// Token: 0x040033E0 RID: 13280
			private static RegisteredWaitHandle s_registeredWait;

			// Token: 0x040033E1 RID: 13281
			private static bool s_isListening = false;

			// Token: 0x040033E2 RID: 13282
			private static bool s_isPending = false;

			// Token: 0x040033E3 RID: 13283
			private static SafeCloseSocketAndEvent s_ipv4Socket = null;

			// Token: 0x040033E4 RID: 13284
			private static SafeCloseSocketAndEvent s_ipv6Socket = null;

			// Token: 0x040033E5 RID: 13285
			private static WaitHandle s_ipv4WaitHandle = null;

			// Token: 0x040033E6 RID: 13286
			private static WaitHandle s_ipv6WaitHandle = null;
		}
	}
}
