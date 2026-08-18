using System;
using System.Threading;

namespace Renci.SshNet
{
	// Token: 0x0200000C RID: 12
	internal class ForwardedPortStatus
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003B5B File Offset: 0x00001D5B
		private ForwardedPortStatus(int value, string name)
		{
			this._value = value;
			this._name = name;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B74 File Offset: 0x00001D74
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			ForwardedPortStatus forwardedPortStatus = other as ForwardedPortStatus;
			return !(forwardedPortStatus == null) && forwardedPortStatus._value == this._value;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003BAC File Offset: 0x00001DAC
		public static bool operator ==(ForwardedPortStatus left, ForwardedPortStatus right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003BBD File Offset: 0x00001DBD
		public static bool operator !=(ForwardedPortStatus left, ForwardedPortStatus right)
		{
			return !(left == right);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003BC9 File Offset: 0x00001DC9
		public override int GetHashCode()
		{
			return this._value;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003BD1 File Offset: 0x00001DD1
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003BDC File Offset: 0x00001DDC
		public static bool ToStopping(ref ForwardedPortStatus status)
		{
			ForwardedPortStatus forwardedPortStatus = Interlocked.CompareExchange<ForwardedPortStatus>(ref status, ForwardedPortStatus.Stopping, ForwardedPortStatus.Started);
			if (forwardedPortStatus == ForwardedPortStatus.Stopping || forwardedPortStatus == ForwardedPortStatus.Stopped)
			{
				return false;
			}
			if (status == ForwardedPortStatus.Stopping)
			{
				return true;
			}
			forwardedPortStatus = Interlocked.CompareExchange<ForwardedPortStatus>(ref status, ForwardedPortStatus.Stopping, ForwardedPortStatus.Starting);
			if (forwardedPortStatus == ForwardedPortStatus.Stopping || forwardedPortStatus == ForwardedPortStatus.Stopped)
			{
				return false;
			}
			if (status == ForwardedPortStatus.Stopping)
			{
				return true;
			}
			throw new InvalidOperationException(string.Format("Forwarded port cannot transition from '{0}' to '{1}'.", forwardedPortStatus, ForwardedPortStatus.Stopping));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C78 File Offset: 0x00001E78
		public static bool ToStarting(ref ForwardedPortStatus status)
		{
			ForwardedPortStatus forwardedPortStatus = Interlocked.CompareExchange<ForwardedPortStatus>(ref status, ForwardedPortStatus.Starting, ForwardedPortStatus.Stopped);
			if (forwardedPortStatus == ForwardedPortStatus.Starting || forwardedPortStatus == ForwardedPortStatus.Started)
			{
				return false;
			}
			if (status == ForwardedPortStatus.Starting)
			{
				return true;
			}
			throw new InvalidOperationException(string.Format("Forwarded port cannot transition from '{0}' to '{1}'.", forwardedPortStatus, ForwardedPortStatus.Starting));
		}

		// Token: 0x0400003E RID: 62
		private readonly int _value;

		// Token: 0x0400003F RID: 63
		private readonly string _name;

		// Token: 0x04000040 RID: 64
		public static readonly ForwardedPortStatus Stopped = new ForwardedPortStatus(1, "Stopped");

		// Token: 0x04000041 RID: 65
		public static readonly ForwardedPortStatus Stopping = new ForwardedPortStatus(2, "Stopping");

		// Token: 0x04000042 RID: 66
		public static readonly ForwardedPortStatus Started = new ForwardedPortStatus(3, "Started");

		// Token: 0x04000043 RID: 67
		public static readonly ForwardedPortStatus Starting = new ForwardedPortStatus(4, "Starting");
	}
}
