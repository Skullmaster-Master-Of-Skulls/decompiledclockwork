using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000EC RID: 236
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public struct CngProperty : IEquatable<CngProperty>
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00018580 File Offset: 0x00016780
		public CngProperty(string name, byte[] value, CngPropertyOptions options)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
			this.m_propertyOptions = options;
			this.m_hashCode = null;
			if (value != null)
			{
				this.m_value = (value.Clone() as byte[]);
				return;
			}
			this.m_value = null;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000185D1 File Offset: 0x000167D1
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000185D9 File Offset: 0x000167D9
		public CngPropertyOptions Options
		{
			get
			{
				return this.m_propertyOptions;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000185E1 File Offset: 0x000167E1
		internal byte[] Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000185EC File Offset: 0x000167EC
		public byte[] GetValue()
		{
			byte[] result = null;
			if (this.m_value != null)
			{
				result = (this.m_value.Clone() as byte[]);
			}
			return result;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00018615 File Offset: 0x00016815
		public static bool operator ==(CngProperty left, CngProperty right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001861F File Offset: 0x0001681F
		public static bool operator !=(CngProperty left, CngProperty right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001862C File Offset: 0x0001682C
		public override bool Equals(object obj)
		{
			return obj != null && obj is CngProperty && this.Equals((CngProperty)obj);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00018648 File Offset: 0x00016848
		public bool Equals(CngProperty other)
		{
			if (!string.Equals(this.Name, other.Name, StringComparison.Ordinal))
			{
				return false;
			}
			if (this.Options != other.Options)
			{
				return false;
			}
			if (this.m_value == null)
			{
				return other.m_value == null;
			}
			if (other.m_value == null)
			{
				return false;
			}
			if (this.m_value.Length != other.m_value.Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_value.Length; i++)
			{
				if (this.m_value[i] != other.m_value[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000186D8 File Offset: 0x000168D8
		public override int GetHashCode()
		{
			if (this.m_hashCode == null)
			{
				int num = this.Name.GetHashCode() ^ this.Options.GetHashCode();
				if (this.m_value != null)
				{
					for (int i = 0; i < this.m_value.Length; i++)
					{
						int num2 = (int)this.m_value[i] << i % 4 * 8;
						num ^= num2;
					}
				}
				this.m_hashCode = new int?(num);
			}
			return this.m_hashCode.Value;
		}

		// Token: 0x04000623 RID: 1571
		private string m_name;

		// Token: 0x04000624 RID: 1572
		private CngPropertyOptions m_propertyOptions;

		// Token: 0x04000625 RID: 1573
		private byte[] m_value;

		// Token: 0x04000626 RID: 1574
		private int? m_hashCode;
	}
}
