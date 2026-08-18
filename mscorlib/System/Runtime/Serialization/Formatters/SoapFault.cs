using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Security.Permissions;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x020007BC RID: 1980
	[SoapType(Embedded = true)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapFault : ISerializable
	{
		// Token: 0x0600468D RID: 18061 RVA: 0x000F08EC File Offset: 0x000EF8EC
		public SoapFault()
		{
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x000F08F4 File Offset: 0x000EF8F4
		public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
		{
			this.faultCode = faultCode;
			this.faultString = faultString;
			this.faultActor = faultActor;
			this.detail = serverFault;
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x000F091C File Offset: 0x000EF91C
		internal SoapFault(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				object value = enumerator.Value;
				if (string.Compare(name, "faultCode", true, CultureInfo.InvariantCulture) == 0)
				{
					int num = ((string)value).IndexOf(':');
					if (num > -1)
					{
						this.faultCode = ((string)value).Substring(num + 1);
					}
					else
					{
						this.faultCode = (string)value;
					}
				}
				else if (string.Compare(name, "faultString", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultString = (string)value;
				}
				else if (string.Compare(name, "faultActor", true, CultureInfo.InvariantCulture) == 0)
				{
					this.faultActor = (string)value;
				}
				else if (string.Compare(name, "detail", true, CultureInfo.InvariantCulture) == 0)
				{
					this.detail = value;
				}
			}
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x000F09FC File Offset: 0x000EF9FC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("faultcode", "SOAP-ENV:" + this.faultCode);
			info.AddValue("faultstring", this.faultString);
			if (this.faultActor != null)
			{
				info.AddValue("faultactor", this.faultActor);
			}
			info.AddValue("detail", this.detail, typeof(object));
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x000F0A69 File Offset: 0x000EFA69
		// (set) Token: 0x06004692 RID: 18066 RVA: 0x000F0A71 File Offset: 0x000EFA71
		public string FaultCode
		{
			get
			{
				return this.faultCode;
			}
			set
			{
				this.faultCode = value;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x000F0A7A File Offset: 0x000EFA7A
		// (set) Token: 0x06004694 RID: 18068 RVA: 0x000F0A82 File Offset: 0x000EFA82
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x000F0A8B File Offset: 0x000EFA8B
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x000F0A93 File Offset: 0x000EFA93
		public string FaultActor
		{
			get
			{
				return this.faultActor;
			}
			set
			{
				this.faultActor = value;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x000F0A9C File Offset: 0x000EFA9C
		// (set) Token: 0x06004698 RID: 18072 RVA: 0x000F0AA4 File Offset: 0x000EFAA4
		public object Detail
		{
			get
			{
				return this.detail;
			}
			set
			{
				this.detail = value;
			}
		}

		// Token: 0x04002315 RID: 8981
		private string faultCode;

		// Token: 0x04002316 RID: 8982
		private string faultString;

		// Token: 0x04002317 RID: 8983
		private string faultActor;

		// Token: 0x04002318 RID: 8984
		[SoapField(Embedded = true)]
		private object detail;
	}
}
