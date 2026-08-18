using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x02000792 RID: 1938
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntities : ISoapXsd
	{
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x000EB649 File Offset: 0x000EA649
		public static string XsdType
		{
			get
			{
				return "ENTITIES";
			}
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x000EB650 File Offset: 0x000EA650
		public string GetXsdType()
		{
			return SoapEntities.XsdType;
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x000EB657 File Offset: 0x000EA657
		public SoapEntities()
		{
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x000EB65F File Offset: 0x000EA65F
		public SoapEntities(string value)
		{
			this._value = value;
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x000EB66E File Offset: 0x000EA66E
		// (set) Token: 0x06004510 RID: 17680 RVA: 0x000EB676 File Offset: 0x000EA676
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x000EB67F File Offset: 0x000EA67F
		public override string ToString()
		{
			return SoapType.Escape(this._value);
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x000EB68C File Offset: 0x000EA68C
		public static SoapEntities Parse(string value)
		{
			return new SoapEntities(value);
		}

		// Token: 0x04002265 RID: 8805
		private string _value;
	}
}
