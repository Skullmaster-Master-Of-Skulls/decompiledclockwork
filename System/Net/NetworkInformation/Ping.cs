using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000623 RID: 1571
	public class Ping : Component, IDisposable
	{
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06003048 RID: 12360 RVA: 0x000D0447 File Offset: 0x000CF447
		// (remove) Token: 0x06003049 RID: 12361 RVA: 0x000D0460 File Offset: 0x000CF460
		public event PingCompletedEventHandler PingCompleted;

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000D0479 File Offset: 0x000CF479
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x000D0494 File Offset: 0x000CF494
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

		// Token: 0x0600304C RID: 12364 RVA: 0x000D04CC File Offset: 0x000CF4CC
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

		// Token: 0x0600304D RID: 12365 RVA: 0x000D0533 File Offset: 0x000CF533
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

		// Token: 0x0600304E RID: 12366 RVA: 0x000D0554 File Offset: 0x000CF554
		protected void OnPingCompleted(PingCompletedEventArgs e)
		{
			if (this.PingCompleted != null)
			{
				this.PingCompleted(this, e);
			}
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000D056B File Offset: 0x000CF56B
		private void PingCompletedWaitCallback(object operationState)
		{
			this.OnPingCompleted((PingCompletedEventArgs)operationState);
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000D0579 File Offset: 0x000CF579
		public Ping()
		{
			this.onPingCompletedDelegate = new SendOrPostCallback(this.PingCompletedWaitCallback);
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000D0594 File Offset: 0x000CF594
		private void InternalDispose()
		{
			this.disposeRequested = true;
			if (Interlocked.CompareExchange(ref this.status, 2, 0) != 0)
			{
				return;
			}
			if (this.pingSocket != null)
			{
				this.pingSocket.Close();
				this.pingSocket = null;
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
			if (this.registeredWait != null)
			{
				this.registeredWait.Unregister(null);
			}
			if (this.pingEvent != null)
			{
				this.pingEvent.Close();
			}
			if (this.replyBuffer != null)
			{
				this.replyBuffer.Close();
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000D0641 File Offset: 0x000CF641
		void IDisposable.Dispose()
		{
			this.InternalDispose();
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000D064C File Offset: 0x000CF64C
		public void SendAsyncCancel()
		{
			lock (this)
			{
				if (!this.InAsyncCall)
				{
					return;
				}
				this.cancelled = true;
				if (this.pingSocket != null)
				{
					this.pingSocket.Close();
					this.pingSocket = null;
				}
			}
			this.asyncFinished.WaitOne();
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000D06B4 File Offset: 0x000CF6B4
		private static void PingCallback(object state, bool signaled)
		{
			Ping ping = (Ping)state;
			PingCompletedEventArgs arg = null;
			bool flag = false;
			AsyncOperation asyncOperation = null;
			SendOrPostCallback d = null;
			try
			{
				lock (ping)
				{
					flag = ping.cancelled;
					asyncOperation = ping.asyncOp;
					d = ping.onPingCompletedDelegate;
					if (!flag)
					{
						SafeLocalFree safeLocalFree = ping.replyBuffer;
						if (ping.ipv6)
						{
							UnsafeNetInfoNativeMethods.Icmp6ParseReplies(safeLocalFree.DangerousGetHandle(), 65791U);
						}
						else if (ComNetOS.IsPostWin2K)
						{
							UnsafeNetInfoNativeMethods.IcmpParseReplies(safeLocalFree.DangerousGetHandle(), 65791U);
						}
						else
						{
							UnsafeIcmpNativeMethods.IcmpParseReplies(safeLocalFree.DangerousGetHandle(), 65791U);
						}
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
				}
			}
			catch (Exception innerException)
			{
				PingException error = new PingException(SR.GetString("net_ping"), innerException);
				arg = new PingCompletedEventArgs(null, error, false, asyncOperation.UserSuppliedState);
			}
			catch
			{
				PingException error2 = new PingException(SR.GetString("net_ping"), new Exception(SR.GetString("net_nonClsCompliantException")));
				arg = new PingCompletedEventArgs(null, error2, false, asyncOperation.UserSuppliedState);
			}
			finally
			{
				ping.FreeUnmanagedStructures();
				ping.Finish(true);
			}
			if (flag)
			{
				arg = new PingCompletedEventArgs(null, null, true, asyncOperation.UserSuppliedState);
			}
			asyncOperation.PostOperationCompleted(d, arg);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000D08B0 File Offset: 0x000CF8B0
		private static void PingSendCallback(IAsyncResult result)
		{
			Ping ping = (Ping)result.AsyncState;
			PingCompletedEventArgs arg = null;
			try
			{
				ping.pingSocket.EndSendTo(result);
				PingReply pingReply = null;
				if (!ping.cancelled)
				{
					EndPoint endPoint = new IPEndPoint(0L, 0);
					int dataLength;
					do
					{
						dataLength = ping.pingSocket.ReceiveFrom(ping.downlevelReplyBuffer, ref endPoint);
						if (Ping.CorrectPacket(ping.downlevelReplyBuffer, ping.packet))
						{
							goto IL_7B;
						}
					}
					while (Environment.TickCount - ping.startTime <= ping.llTimeout);
					pingReply = new PingReply(IPStatus.TimedOut);
					IL_7B:
					int time = Environment.TickCount - ping.startTime;
					if (pingReply == null)
					{
						pingReply = new PingReply(ping.downlevelReplyBuffer, dataLength, ((IPEndPoint)endPoint).Address, time);
					}
					arg = new PingCompletedEventArgs(pingReply, null, false, ping.asyncOp.UserSuppliedState);
				}
			}
			catch (Exception ex)
			{
				PingReply pingReply2 = null;
				PingException error = null;
				SocketException ex2 = ex as SocketException;
				if (ex2 != null)
				{
					if (ex2.ErrorCode == 10060)
					{
						pingReply2 = new PingReply(IPStatus.TimedOut);
					}
					else if (ex2.ErrorCode == 10040)
					{
						pingReply2 = new PingReply(IPStatus.PacketTooBig);
					}
				}
				if (pingReply2 == null)
				{
					error = new PingException(SR.GetString("net_ping"), ex);
				}
				arg = new PingCompletedEventArgs(pingReply2, error, false, ping.asyncOp.UserSuppliedState);
			}
			catch
			{
				PingException error2 = new PingException(SR.GetString("net_ping"), new Exception(SR.GetString("net_nonClsCompliantException")));
				arg = new PingCompletedEventArgs(null, error2, false, ping.asyncOp.UserSuppliedState);
			}
			try
			{
				if (ping.cancelled)
				{
					arg = new PingCompletedEventArgs(null, null, true, ping.asyncOp.UserSuppliedState);
				}
				ping.asyncOp.PostOperationCompleted(ping.onPingCompletedDelegate, arg);
			}
			finally
			{
				ping.Finish(true);
			}
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000D0A90 File Offset: 0x000CFA90
		public PingReply Send(string hostNameOrAddress)
		{
			return this.Send(hostNameOrAddress, 5000, this.DefaultSendBuffer, null);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000D0AA5 File Offset: 0x000CFAA5
		public PingReply Send(string hostNameOrAddress, int timeout)
		{
			return this.Send(hostNameOrAddress, timeout, this.DefaultSendBuffer, null);
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000D0AB6 File Offset: 0x000CFAB6
		public PingReply Send(IPAddress address)
		{
			return this.Send(address, 5000, this.DefaultSendBuffer, null);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x000D0ACB File Offset: 0x000CFACB
		public PingReply Send(IPAddress address, int timeout)
		{
			return this.Send(address, timeout, this.DefaultSendBuffer, null);
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x000D0ADC File Offset: 0x000CFADC
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.Send(hostNameOrAddress, timeout, buffer, null);
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000D0AE8 File Offset: 0x000CFAE8
		public PingReply Send(IPAddress address, int timeout, byte[] buffer)
		{
			return this.Send(address, timeout, buffer, null);
		}

		// Token: 0x0600305C RID: 12380 RVA: 0x000D0AF4 File Offset: 0x000CFAF4
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			if (ValidationHelper.IsBlankString(hostNameOrAddress))
			{
				throw new ArgumentNullException("hostNameOrAddress");
			}
			IPAddress address;
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
			return this.Send(address, timeout, buffer, options);
		}

		// Token: 0x0600305D RID: 12381 RVA: 0x000D0B5C File Offset: 0x000CFB5C
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
			catch
			{
				throw new PingException(SR.GetString("net_ping"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			finally
			{
				this.Finish(false);
			}
			return result;
		}

		// Token: 0x0600305E RID: 12382 RVA: 0x000D0C90 File Offset: 0x000CFC90
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, object userToken)
		{
			this.SendAsync(hostNameOrAddress, 5000, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x0600305F RID: 12383 RVA: 0x000D0CA5 File Offset: 0x000CFCA5
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000D0CB6 File Offset: 0x000CFCB6
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, object userToken)
		{
			this.SendAsync(address, 5000, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x06003061 RID: 12385 RVA: 0x000D0CCB File Offset: 0x000CFCCB
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, object userToken)
		{
			this.SendAsync(address, timeout, this.DefaultSendBuffer, userToken);
		}

		// Token: 0x06003062 RID: 12386 RVA: 0x000D0CDC File Offset: 0x000CFCDC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, buffer, null, userToken);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x000D0CEA File Offset: 0x000CFCEA
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(address, timeout, buffer, null, userToken);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x000D0CF8 File Offset: 0x000CFCF8
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

		// Token: 0x06003065 RID: 12389 RVA: 0x000D0DCC File Offset: 0x000CFDCC
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
				this.asyncOp = AsyncOperationManager.CreateOperation(userToken);
				this.InternalSend(address2, buffer, timeout, options, true);
			}
			catch (Exception innerException)
			{
				this.Finish(true);
				throw new PingException(SR.GetString("net_ping"), innerException);
			}
			catch
			{
				this.Finish(true);
				throw new PingException(SR.GetString("net_ping"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000D0F08 File Offset: 0x000CFF08
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
			catch
			{
				PingException error2 = new PingException(SR.GetString("net_ping"), new Exception(SR.GetString("net_nonClsCompliantException")));
				PingCompletedEventArgs arg2 = new PingCompletedEventArgs(null, error2, false, this.asyncOp.UserSuppliedState);
				this.Finish(true);
				this.asyncOp.PostOperationCompleted(this.onPingCompletedDelegate, arg2);
			}
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000D1008 File Offset: 0x000D0008
		private PingReply InternalSend(IPAddress address, byte[] buffer, int timeout, PingOptions options, bool async)
		{
			this.cancelled = false;
			if (address.AddressFamily == AddressFamily.InterNetworkV6 && !ComNetOS.IsPostWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
			}
			if (!ComNetOS.IsWin2K)
			{
				return this.InternalDownLevelSend(address, buffer, timeout, options, async);
			}
			this.ipv6 = (address.AddressFamily == AddressFamily.InterNetworkV6);
			this.sendSize = buffer.Length;
			if (!this.ipv6 && this.handlePingV4 == null)
			{
				if (ComNetOS.IsPostWin2K)
				{
					this.handlePingV4 = UnsafeNetInfoNativeMethods.IcmpCreateFile();
				}
				else
				{
					this.handlePingV4 = UnsafeIcmpNativeMethods.IcmpCreateFile();
				}
			}
			else if (this.ipv6 && this.handlePingV6 == null)
			{
				this.handlePingV6 = UnsafeNetInfoNativeMethods.Icmp6CreateFile();
			}
			IPOptions ipoptions = new IPOptions(options);
			if (this.replyBuffer == null)
			{
				this.replyBuffer = SafeLocalFree.LocalAlloc(65791);
			}
			if (this.registeredWait != null)
			{
				this.registeredWait.Unregister(null);
				this.registeredWait = null;
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
					if (ComNetOS.IsPostWin2K)
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
					else if (async)
					{
						num = (int)UnsafeIcmpNativeMethods.IcmpSendEcho2(this.handlePingV4, this.pingEvent.SafeWaitHandle, IntPtr.Zero, IntPtr.Zero, (uint)address.m_Address, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
					}
					else
					{
						num = (int)UnsafeIcmpNativeMethods.IcmpSendEcho2(this.handlePingV4, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, (uint)address.m_Address, this.requestBuffer, (ushort)buffer.Length, ref ipoptions, this.replyBuffer, 65791U, (uint)timeout);
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
				if (this.registeredWait != null)
				{
					this.registeredWait.Unregister(null);
				}
				throw;
			}
			if (num == 0)
			{
				num = Marshal.GetLastWin32Error();
				if (num != 0)
				{
					this.FreeUnmanagedStructures();
					return new PingReply((IPStatus)num);
				}
			}
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

		// Token: 0x06003068 RID: 12392 RVA: 0x000D13E0 File Offset: 0x000D03E0
		private PingReply InternalDownLevelSend(IPAddress address, byte[] buffer, int timeout, PingOptions options, bool async)
		{
			PingReply result;
			try
			{
				if (options == null)
				{
					options = new PingOptions();
				}
				if (this.downlevelReplyBuffer == null)
				{
					this.downlevelReplyBuffer = new byte[64000];
				}
				this.llTimeout = timeout;
				this.packet = new IcmpPacket(buffer);
				byte[] bytes = this.packet.GetBytes();
				IPEndPoint remoteEP = new IPEndPoint(address, 0);
				EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
				if (this.pingSocket == null)
				{
					this.pingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
				}
				this.pingSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
				this.pingSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, timeout);
				this.pingSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, options.Ttl);
				this.pingSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DontFragment, options.DontFragment);
				this.startTime = Environment.TickCount;
				if (async)
				{
					this.pingSocket.BeginSendTo(bytes, 0, bytes.Length, SocketFlags.None, remoteEP, new AsyncCallback(Ping.PingSendCallback), this);
					result = null;
				}
				else
				{
					this.pingSocket.SendTo(bytes, bytes.Length, SocketFlags.None, remoteEP);
					int dataLength;
					do
					{
						dataLength = this.pingSocket.ReceiveFrom(this.downlevelReplyBuffer, ref endPoint);
						if (Ping.CorrectPacket(this.downlevelReplyBuffer, this.packet))
						{
							goto IL_15B;
						}
					}
					while (Environment.TickCount - this.startTime <= this.llTimeout);
					return new PingReply(IPStatus.TimedOut);
					IL_15B:
					int time = Environment.TickCount - this.startTime;
					result = new PingReply(this.downlevelReplyBuffer, dataLength, ((IPEndPoint)endPoint).Address, time);
				}
			}
			catch (SocketException ex)
			{
				if (ex.ErrorCode == 10060)
				{
					result = new PingReply(IPStatus.TimedOut);
				}
				else
				{
					if (ex.ErrorCode != 10040)
					{
						throw ex;
					}
					PingReply pingReply = new PingReply(IPStatus.PacketTooBig);
					if (!async)
					{
						result = pingReply;
					}
					else
					{
						PingCompletedEventArgs arg = new PingCompletedEventArgs(pingReply, null, false, this.asyncOp.UserSuppliedState);
						this.asyncOp.PostOperationCompleted(this.onPingCompletedDelegate, arg);
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000D1604 File Offset: 0x000D0604
		private unsafe void SetUnmanagedStructures(byte[] buffer)
		{
			this.requestBuffer = SafeLocalFree.LocalAlloc(buffer.Length);
			byte* ptr = (byte*)((void*)this.requestBuffer.DangerousGetHandle());
			for (int i = 0; i < buffer.Length; i++)
			{
				ptr[i] = buffer[i];
			}
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x000D1645 File Offset: 0x000D0645
		private void FreeUnmanagedStructures()
		{
			if (this.requestBuffer != null)
			{
				this.requestBuffer.Close();
				this.requestBuffer = null;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000D1664 File Offset: 0x000D0664
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

		// Token: 0x0600306C RID: 12396 RVA: 0x000D16AC File Offset: 0x000D06AC
		internal static bool CorrectPacket(byte[] buffer, IcmpPacket packet)
		{
			if (buffer[20] == 0 && buffer[21] == 0)
			{
				if (((int)buffer[25] << 8 | (int)buffer[24]) == (int)packet.Identifier && ((int)buffer[27] << 8 | (int)buffer[26]) == (int)packet.sequenceNumber)
				{
					return true;
				}
			}
			else if (((int)buffer[53] << 8 | (int)buffer[52]) == (int)packet.Identifier && ((int)buffer[55] << 8 | (int)buffer[54]) == (int)packet.sequenceNumber)
			{
				return true;
			}
			return false;
		}

		// Token: 0x04002DFE RID: 11774
		private const int MaxUdpPacket = 65791;

		// Token: 0x04002DFF RID: 11775
		private const int MaxBufferSize = 65500;

		// Token: 0x04002E00 RID: 11776
		private const int DefaultTimeout = 5000;

		// Token: 0x04002E01 RID: 11777
		private const int DefaultSendBufferSize = 32;

		// Token: 0x04002E02 RID: 11778
		private const int TimeoutErrorCode = 10060;

		// Token: 0x04002E03 RID: 11779
		private const int PacketTooBigErrorCode = 10040;

		// Token: 0x04002E04 RID: 11780
		private const int Free = 0;

		// Token: 0x04002E05 RID: 11781
		private const int InProgress = 1;

		// Token: 0x04002E06 RID: 11782
		private new const int Disposed = 2;

		// Token: 0x04002E07 RID: 11783
		private byte[] defaultSendBuffer;

		// Token: 0x04002E08 RID: 11784
		private bool ipv6;

		// Token: 0x04002E09 RID: 11785
		private bool cancelled;

		// Token: 0x04002E0A RID: 11786
		private bool disposeRequested;

		// Token: 0x04002E0B RID: 11787
		internal ManualResetEvent pingEvent;

		// Token: 0x04002E0C RID: 11788
		private RegisteredWaitHandle registeredWait;

		// Token: 0x04002E0D RID: 11789
		private SafeLocalFree requestBuffer;

		// Token: 0x04002E0E RID: 11790
		private SafeLocalFree replyBuffer;

		// Token: 0x04002E0F RID: 11791
		private int sendSize;

		// Token: 0x04002E10 RID: 11792
		private Socket pingSocket;

		// Token: 0x04002E11 RID: 11793
		private byte[] downlevelReplyBuffer;

		// Token: 0x04002E12 RID: 11794
		private SafeCloseIcmpHandle handlePingV4;

		// Token: 0x04002E13 RID: 11795
		private SafeCloseIcmpHandle handlePingV6;

		// Token: 0x04002E14 RID: 11796
		private int startTime;

		// Token: 0x04002E15 RID: 11797
		private IcmpPacket packet;

		// Token: 0x04002E16 RID: 11798
		private int llTimeout;

		// Token: 0x04002E17 RID: 11799
		private AsyncOperation asyncOp;

		// Token: 0x04002E18 RID: 11800
		private SendOrPostCallback onPingCompletedDelegate;

		// Token: 0x04002E1A RID: 11802
		private ManualResetEvent asyncFinished;

		// Token: 0x04002E1B RID: 11803
		private int status;

		// Token: 0x02000624 RID: 1572
		internal class AsyncStateObject
		{
			// Token: 0x0600306D RID: 12397 RVA: 0x000D1716 File Offset: 0x000D0716
			internal AsyncStateObject(string hostName, byte[] buffer, int timeout, PingOptions options, object userToken)
			{
				this.hostName = hostName;
				this.buffer = buffer;
				this.timeout = timeout;
				this.options = options;
				this.userToken = userToken;
			}

			// Token: 0x04002E1C RID: 11804
			internal byte[] buffer;

			// Token: 0x04002E1D RID: 11805
			internal string hostName;

			// Token: 0x04002E1E RID: 11806
			internal int timeout;

			// Token: 0x04002E1F RID: 11807
			internal PingOptions options;

			// Token: 0x04002E20 RID: 11808
			internal object userToken;
		}
	}
}
