using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002EA RID: 746
	public class Ping : Component
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001A26 RID: 6694 RVA: 0x0007EDDC File Offset: 0x0007CFDC
		// (remove) Token: 0x06001A27 RID: 6695 RVA: 0x0007EE14 File Offset: 0x0007D014
		public event PingCompletedEventHandler PingCompleted;

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0007EE49 File Offset: 0x0007D049
		// (set) Token: 0x06001A29 RID: 6697 RVA: 0x0007EE64 File Offset: 0x0007D064
		private bool InAsyncCall
		{
			get
			{
				return this.asyncFinished != null && !this.asyncFinished.WaitOne(0);
			}
			set
			{
				if (this.asyncFinished == null)
				{
					this.asyncFinished = new ManualResetEvent(!value);
					return;
				}
				if (value)
				{
					this.asyncFinished.Reset();
					return;
				}
				this.asyncFinished.Set();
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0007EE9C File Offset: 0x0007D09C
		private void CheckStart(bool async)
		{
			if (this.disposeRequested)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			int num = Interlocked.CompareExchange(ref this.status, 1, 0);
			if (num == 1)
			{
				throw new InvalidOperationException(SR.GetString("net_inasync"));
			}
			if (num == 2)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (async)
			{
				this.InAsyncCall = true;
			}
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0007EF03 File Offset: 0x0007D103
		private void Finish(bool async)
		{
			this.status = 0;
			if (async)
			{
				this.InAsyncCall = false;
			}
			if (this.disposeRequested)
			{
				this.InternalDispose();
			}
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0007EF24 File Offset: 0x0007D124
		protected void OnPingCompleted(PingCompletedEventArgs e)
		{
			if (this.PingCompleted != null)
			{
				this.PingCompleted(this, e);
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0007EF3B File Offset: 0x0007D13B
		private void PingCompletedWaitCallback(object operationState)
		{
			this.OnPingCompleted((PingCompletedEventArgs)operationState);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0007EF49 File Offset: 0x0007D149
		public Ping()
		{
			this.onPingCompletedDelegate = new SendOrPostCallback(this.PingCompletedWaitCallback);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0007EF70 File Offset: 0x0007D170
		private void InternalDispose()
		{
			this.disposeRequested = true;
			if (Interlocked.CompareExchange(ref this.status, 2, 0) != 0)
			{
				return;
			}
			if (this.handlePingV4 != null)
			{
				this.handlePingV4.Close();
				this.handlePingV4 = null;
			}
			if (this.handlePingV6 != null)
			{
				this.handlePingV6.Close();
				this.handlePingV6 = null;
			}
			this.UnregisterWaitHandle();
			if (this.pingEvent != null)
			{
				this.pingEvent.Close();
				this.pingEvent = null;
			}
			if (this.replyBuffer != null)
			{
				this.replyBuffer.Close();
				this.replyBuffer = null;
			}
			if (this.asyncFinished != null)
			{
				this.asyncFinished.Close();
				this.asyncFinished = null;
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0007F01C File Offset: 0x0007D21C
		private void UnregisterWaitHandle()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (this.registeredWait != null)
				{
					this.registeredWait.Unregister(null);
					this.registeredWait = null;
				}
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0007F074 File Offset: 0x0007D274
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0007F088 File Offset: 0x0007D288
		public void SendAsyncCancel()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (!this.InAsyncCall)
				{
					return;
				}
				this.cancelled = true;
			}
			this.asyncFinished.WaitOne();
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0007F0E0 File Offset: 0x0007D2E0
		private static void PingCallback(object state, bool signaled)
		{
			Ping ping = (Ping)state;
			PingCompletedEventArgs arg = null;
			AsyncOperation asyncOperation = null;
			SendOrPostCallback d = null;
			try
			{
				object obj = ping.lockObject;
				lock (obj)
				{
					bool flag2 = ping.cancelled;
					asyncOperation = ping.asyncOp;
					d = ping.onPingCompletedDelegate;
					if (!flag2)
					{
						SafeLocalFree safeLocalFree = ping.replyBuffer;
						PingReply reply2;
						if (ping.ipv6)
						{
							Icmp6EchoReply reply = (Icmp6EchoReply)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(Icmp6EchoReply));
							reply2 = new PingReply(reply, safeLocalFree.DangerousGetHandle(), ping.sendSize);
						}
						else
						{
							IcmpEchoReply reply3 = (IcmpEchoReply)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(IcmpEchoReply));
							reply2 = new PingReply(reply3);
						}
						arg = new PingCompletedEventArgs(reply2, null, false, asyncOperation.UserSuppliedState);
					}
					else
					{
						arg = new PingCompletedEventArgs(null, null, true, asyncOperation.UserSuppliedState);
					}
				}
			}
			catch (Exception innerException)
			{
				PingException error = new PingException(SR.GetString("net_ping"), innerException);
				arg = new PingCompletedEventArgs(null, error, false, asyncOperation.UserSuppliedState);
			}
			finally
			{
				ping.FreeUnmanagedStructures();
				ping.UnregisterWaitHandle();
				ping.Finish(true);
			}
			asyncOperation.PostOperationCompleted(d, arg);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0007F234 File Offset: 0x0007D434
		public PingReply Send(string hostNameOrAddress)
		{
			return this.Send(hostNameOrAddress, 5000, this.DefaultSendBuffer, null);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0007F249 File Offset: 0x0007D449
		public PingReply Send(string hostNameOrAddress, int timeout)
		{
			return this.Send(hostNameOrAddress, timeout, this.DefaultSendBuffer, null);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0007F25A File Offset: 0x0007D45A
		public PingReply Send(IPAddress address)
		{
			return this.Send(address, 5000, this.DefaultSendBuffer, null);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0007F26F File Offset: 0x0007D46F
		public PingReply Send(IPAddress address, int timeout)
		{
			return this.Send(address, timeout, this.DefaultSendBuffer, null);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0007F280 File Offset: 0x0007D480
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.Send(hostNameOrAddress, timeout, buffer, null);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0007F28C File Offset: 0x0007D48C
		public PingReply Send(IPAddress address, int timeout, byte[] buffer)
		{
			return this.Send(address, timeout, buffer, null);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0007F298 File Offset: 0x0007D498
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			if (ValidationHelper.IsBlankString(hostNameOrAddress))
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			IPAddress address;
			if (!IPAddress.TryParse(hostNameOrAddress, out address))
			{
				try
				{
					address = Dns.GetHostAddresses(hostNameOrAddress)[0];
				}
				catch (ArgumentException)
				{
					throw;
				}
				catch (Exception innerException)
				{
					throw new PingException(SR.GetString("net_ping"), innerException);
				}
			}
			return this.Send(address, timeout, buffer, options);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0007F30C File Offset: 0x0007D50C
		public PingReply Send(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException(SR.GetString("net_invalidPingBufferSize"), "buffer");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.TestIsIpSupported(address);
			if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any))
			{
				throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "address");
			}
			IPAddress address2;
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				address2 = new IPAddress(address.GetAddressBytes());
			}
			else
			{
				address2 = new IPAddress(address.GetAddressBytes(), address.ScopeId);
			}
			new NetworkInformationPermission(NetworkInformationAccess.Ping).Demand();
			this.CheckStart(false);
			PingReply result;
			try
			{
				result = this.InternalSend(address2, buffer, timeout, options, false);
			}
			catch (Exception innerException)
			{
				throw new PingException(SR.GetString("net_ping"), innerException);
			}
			finally
			{
				this.Finish(false);
			}
			return result;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0007F41C File Offset: 0x0007D61C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, object userToken)
		{
			this.SendAsync(hostNameOrAddress, 5000, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0007F431 File Offset: 0x0007D631
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0007F442 File Offset: 0x0007D642
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, object userToken)
		{
			this.SendAsync(address, 5000, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0007F457 File Offset: 0x0007D657
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, object userToken)
		{
			this.SendAsync(address, timeout, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0007F468 File Offset: 0x0007D668
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, buffer, null, userToken);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0007F476 File Offset: 0x0007D676
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(address, timeout, buffer, null, userToken);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0007F484 File Offset: 0x0007D684
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			if (ValidationHelper.IsBlankString(hostNameOrAddress))
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException(SR.GetString("net_invalidPingBufferSize"), "buffer");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			IPAddress address;
			if (IPAddress.TryParse(hostNameOrAddress, out address))
			{
				this.SendAsync(address, timeout, buffer, options, userToken);
				return;
			}
			this.CheckStart(true);
			try
			{
				this.cancelled = false;
				this.asyncOp = AsyncOperationManager.CreateOperation(userToken);
				Ping.AsyncStateObject state = new Ping.AsyncStateObject(hostNameOrAddress, buffer, timeout, options, userToken);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ContinueAsyncSend), state);
			}
			catch (Exception innerException)
			{
				this.Finish(true);
				throw new PingException(SR.GetString("net_ping"), innerException);
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0007F560 File Offset: 0x0007D760
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException(SR.GetString("net_invalidPingBufferSize"), "buffer");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.TestIsIpSupported(address);
			if (address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any))
			{
				throw new ArgumentException(SR.GetString("net_invalid_ip_addr"), "address");
			}
			IPAddress address2;
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				address2 = new IPAddress(address.GetAddressBytes());
			}
			else
			{
				address2 = new IPAddress(address.GetAddressBytes(), address.ScopeId);
			}
			new NetworkInformationPermission(NetworkInformationAccess.Ping).Demand();
			this.CheckStart(true);
			try
			{
				this.cancelled = false;
				this.asyncOp = AsyncOperationManager.CreateOperation(userToken);
				this.InternalSend(address2, buffer, timeout, options, true);
			}
			catch (Exception innerException)
			{
				this.Finish(true);
				throw new PingException(SR.GetString("net_ping"), innerException);
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0007F674 File Offset: 0x0007D874
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, tcs);
			});
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x0007F6A8 File Offset: 0x0007D8A8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, tcs);
			});
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x0007F6DC File Offset: 0x0007D8DC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, timeout, tcs);
			});
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x0007F718 File Offset: 0x0007D918
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, timeout, tcs);
			});
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0007F754 File Offset: 0x0007D954
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout, byte[] buffer)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, timeout, buffer, tcs);
			});
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0007F798 File Offset: 0x0007D998
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, timeout, buffer, tcs);
			});
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0007F7DC File Offset: 0x0007D9DC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(address, timeout, buffer, options, tcs);
			});
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0007F828 File Offset: 0x0007DA28
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<PingReply> SendPingAsync(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			return this.SendPingAsyncCore(delegate(TaskCompletionSource<PingReply> tcs)
			{
				this.SendAsync(hostNameOrAddress, timeout, buffer, options, tcs);
			});
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0007F874 File Offset: 0x0007DA74
		private Task<PingReply> SendPingAsyncCore(Action<TaskCompletionSource<PingReply>> sendAsync)
		{
			TaskCompletionSource<PingReply> tcs = new TaskCompletionSource<PingReply>();
			PingCompletedEventHandler handler = null;
			handler = delegate(object sender, PingCompletedEventArgs e)
			{
				this.HandleCompletion(tcs, e, handler);
			};
			this.PingCompleted += handler;
			try
			{
				sendAsync(tcs);
			}
			catch
			{
				this.PingCompleted -= handler;
				throw;
			}
			return tcs.Task;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0007F8F8 File Offset: 0x0007DAF8
		private void HandleCompletion(TaskCompletionSource<PingReply> tcs, PingCompletedEventArgs e, PingCompletedEventHandler handler)
		{
			if (e.UserState == tcs)
			{
				try
				{
					this.PingCompleted -= handler;
				}
				finally
				{
					if (e.Error != null)
					{
						tcs.TrySetException(e.Error);
					}
					else if (e.Cancelled)
					{
						tcs.TrySetCanceled();
					}
					else
					{
						tcs.TrySetResult(e.Reply);
					}
				}
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0007F960 File Offset: 0x0007DB60
		private void ContinueAsyncSend(object state)
		{
			Ping.AsyncStateObject asyncStateObject = (Ping.AsyncStateObject)state;
			try
			{
				IPAddress address = Dns.GetHostAddresses(asyncStateObject.hostName)[0];
				new NetworkInformationPermission(NetworkInformationAccess.Ping).Demand();
				this.InternalSend(address, asyncStateObject.buffer, asyncStateObject.timeout, asyncStateObject.options, true);
			}
			catch (Exception innerException)
			{
				PingException error = new PingException(SR.GetString("net_ping"), innerException);
				PingCompletedEventArgs arg = new PingCompletedEventArgs(null, error, false, this.asyncOp.UserSuppliedState);
				this.Finish(true);
				this.asyncOp.PostOperationCompleted(this.onPingCompletedDelegate, arg);
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x0007FA00 File Offset: 0x0007DC00
		private PingReply InternalSend(IPAddress address, byte[] buffer, int timeout, PingOptions options, bool async)
		{
			this.ipv6 = (address.AddressFamily == AddressFamily.InterNetworkV6);
			this.sendSize = buffer.Length;
			if (!this.ipv6 && this.handlePingV4 == null)
			{
				this.handlePingV4 = UnsafeNetInfoNativeMethods.IcmpCreateFile();
				if (this.handlePingV4.IsInvalid)
				{
					this.handlePingV4 = null;
					throw new Win32Exception();
				}
			}
			else if (this.ipv6 && this.handlePingV6 == null)
			{
				this.handlePingV6 = UnsafeNetInfoNativeMethods.Icmp6CreateFile();
				if (this.handlePingV6.IsInvalid)
				{
					this.handlePingV6 = null;
					throw new Win32Exception();
				}
			}
			IPOptions ipoptions = new IPOptions(options);
			if (this.replyBuffer == null)
			{
				this.replyBuffer = SafeLocalFree.LocalAlloc(65791);
			}
			int num;
			try
			{
				if (async)
				{
					if (this.pingEvent == null)
					{
						this.pingEvent = new ManualResetEvent(false);
					}
					else
					{
						this.pingEvent.Reset();
					}
					this.registeredWait = ThreadPool.RegisterWaitForSingleObject(this.pingEvent, new WaitOrTimerCallback(Ping.PingCallback), this, -1, true);
				}
				this.SetUnmanagedStructures(buffer);
				if (!this.ipv6)
				{
					if (async)
					{
						num = (int)UnsafeNetInfoNativeMethods.IcmpSendEcho2(this.handlePingV4, this.pingEvent.SafeWaitHandle, IntPtr.Zero, IntPtr.Zero, (uint)address.m_Address, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
					else
					{
						num = (int)UnsafeNetInfoNativeMethods.IcmpSendEcho2(this.handlePingV4, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, (uint)address.m_Address, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
				}
				else
				{
					IPEndPoint ipendPoint = new IPEndPoint(address, 0);
					SocketAddress socketAddress = ipendPoint.Serialize();
					byte[] sourceSocketAddress = new byte[28];
					if (async)
					{
						num = (int)UnsafeNetInfoNativeMethods.Icmp6SendEcho2(this.handlePingV6, this.pingEvent.SafeWaitHandle, IntPtr.Zero, IntPtr.Zero, sourceSocketAddress, socketAddress.m_Buffer, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
					else
					{
						num = (int)UnsafeNetInfoNativeMethods.Icmp6SendEcho2(this.handlePingV6, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, sourceSocketAddress, socketAddress.m_Buffer, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
				}
			}
			catch
			{
				this.UnregisterWaitHandle();
				throw;
			}
			if (num == 0)
			{
				num = Marshal.GetLastWin32Error();
				if (async && (long)num == 997L)
				{
					return null;
				}
				this.FreeUnmanagedStructures();
				this.UnregisterWaitHandle();
				if (async || num < 11002 || num > 11045)
				{
					throw new Win32Exception(num);
				}
				return new PingReply((IPStatus)num);
			}
			else
			{
				if (async)
				{
					return null;
				}
				this.FreeUnmanagedStructures();
				PingReply result;
				if (this.ipv6)
				{
					Icmp6EchoReply reply = (Icmp6EchoReply)Marshal.PtrToStructure(this.replyBuffer.DangerousGetHandle(), typeof(Icmp6EchoReply));
					result = new PingReply(reply, this.replyBuffer.DangerousGetHandle(), this.sendSize);
				}
				else
				{
					IcmpEchoReply reply2 = (IcmpEchoReply)Marshal.PtrToStructure(this.replyBuffer.DangerousGetHandle(), typeof(IcmpEchoReply));
					result = new PingReply(reply2);
				}
				GC.KeepAlive(this.replyBuffer);
				return result;
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0007FD28 File Offset: 0x0007DF28
		private void TestIsIpSupported(IPAddress ip)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork && !Socket.OSSupportsIPv4)
			{
				throw new NotSupportedException(SR.GetString("net_ipv4_not_installed"));
			}
			if (ip.AddressFamily == AddressFamily.InterNetworkV6 && !Socket.OSSupportsIPv6)
			{
				throw new NotSupportedException(SR.GetString("net_ipv6_not_installed"));
			}
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0007FD78 File Offset: 0x0007DF78
		private unsafe void SetUnmanagedStructures(byte[] buffer)
		{
			this.requestBuffer = SafeLocalFree.LocalAlloc(buffer.Length);
			byte* ptr = (byte*)((void*)this.requestBuffer.DangerousGetHandle());
			for (int i = 0; i < buffer.Length; i++)
			{
				ptr[i] = buffer[i];
			}
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0007FDB9 File Offset: 0x0007DFB9
		private void FreeUnmanagedStructures()
		{
			if (this.requestBuffer != null)
			{
				this.requestBuffer.Close();
				this.requestBuffer = null;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0007FDD8 File Offset: 0x0007DFD8
		private byte[] DefaultSendBuffer
		{
			get
			{
				if (this.defaultSendBuffer == null)
				{
					this.defaultSendBuffer = new byte[32];
					for (int i = 0; i < 32; i++)
					{
						this.defaultSendBuffer[i] = (byte)(97 + i % 23);
					}
				}
				return this.defaultSendBuffer;
			}
		}

		// Token: 0x04001A73 RID: 6771
		private const int MaxUdpPacket = 65791;

		// Token: 0x04001A74 RID: 6772
		private const int MaxBufferSize = 65500;

		// Token: 0x04001A75 RID: 6773
		private const int DefaultTimeout = 5000;

		// Token: 0x04001A76 RID: 6774
		private const int DefaultSendBufferSize = 32;

		// Token: 0x04001A77 RID: 6775
		private byte[] defaultSendBuffer;

		// Token: 0x04001A78 RID: 6776
		private bool ipv6;

		// Token: 0x04001A79 RID: 6777
		private bool cancelled;

		// Token: 0x04001A7A RID: 6778
		private bool disposeRequested;

		// Token: 0x04001A7B RID: 6779
		private object lockObject = new object();

		// Token: 0x04001A7C RID: 6780
		internal ManualResetEvent pingEvent;

		// Token: 0x04001A7D RID: 6781
		private RegisteredWaitHandle registeredWait;

		// Token: 0x04001A7E RID: 6782
		private SafeLocalFree requestBuffer;

		// Token: 0x04001A7F RID: 6783
		private SafeLocalFree replyBuffer;

		// Token: 0x04001A80 RID: 6784
		private int sendSize;

		// Token: 0x04001A81 RID: 6785
		private SafeCloseIcmpHandle handlePingV4;

		// Token: 0x04001A82 RID: 6786
		private SafeCloseIcmpHandle handlePingV6;

		// Token: 0x04001A83 RID: 6787
		private AsyncOperation asyncOp;

		// Token: 0x04001A84 RID: 6788
		private SendOrPostCallback onPingCompletedDelegate;

		// Token: 0x04001A86 RID: 6790
		private ManualResetEvent asyncFinished;

		// Token: 0x04001A87 RID: 6791
		private const int Free = 0;

		// Token: 0x04001A88 RID: 6792
		private const int InProgress = 1;

		// Token: 0x04001A89 RID: 6793
		private new const int Disposed = 2;

		// Token: 0x04001A8A RID: 6794
		private int status;

		// Token: 0x020007A6 RID: 1958
		internal class AsyncStateObject
		{
			// Token: 0x06004322 RID: 17186 RVA: 0x00119D90 File Offset: 0x00117F90
			internal AsyncStateObject(string hostName, byte[] buffer, int timeout, PingOptions options, object userToken)
			{
				this.hostName = hostName;
				this.buffer = buffer;
				this.timeout = timeout;
				this.options = options;
				this.userToken = userToken;
			}

			// Token: 0x040033E7 RID: 13287
			internal byte[] buffer;

			// Token: 0x040033E8 RID: 13288
			internal string hostName;

			// Token: 0x040033E9 RID: 13289
			internal int timeout;

			// Token: 0x040033EA RID: 13290
			internal PingOptions options;

			// Token: 0x040033EB RID: 13291
			internal object userToken;
		}
	}
}
