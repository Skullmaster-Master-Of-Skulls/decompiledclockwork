using System;

namespace System.Xml.Linq
{
	// Token: 0x02000027 RID: 39
	[__DynamicallyInvokable]
	public class XProcessingInstruction : XNode
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00008687 File Offset: 0x00006887
		[__DynamicallyInvokable]
		public XProcessingInstruction(string target, string data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			XProcessingInstruction.ValidateName(target);
			this.target = target;
			this.data = data;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000086B1 File Offset: 0x000068B1
		[__DynamicallyInvokable]
		public XProcessingInstruction(XProcessingInstruction other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.target = other.target;
			this.data = other.data;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000086DF File Offset: 0x000068DF
		internal XProcessingInstruction(XmlReader r)
		{
			this.target = r.Name;
			this.data = r.Value;
			r.Read();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008706 File Offset: 0x00006906
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00008710 File Offset: 0x00006910
		[__DynamicallyInvokable]
		public string Data
		{
			[__DynamicallyInvokable]
			get
			{
				return this.data;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this.data = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000874F File Offset: 0x0000694F
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00008752 File Offset: 0x00006952
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000875C File Offset: 0x0000695C
		[__DynamicallyInvokable]
		public string Target
		{
			[__DynamicallyInvokable]
			get
			{
				return this.target;
			}
			[__DynamicallyInvokable]
			set
			{
				XProcessingInstruction.ValidateName(value);
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Name);
				this.target = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008793 File Offset: 0x00006993
		[__DynamicallyInvokable]
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteProcessingInstruction(this.target, this.data);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000087B5 File Offset: 0x000069B5
		internal override XNode CloneNode()
		{
			return new XProcessingInstruction(this);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000087C0 File Offset: 0x000069C0
		internal override bool DeepEquals(XNode node)
		{
			XProcessingInstruction xprocessingInstruction = node as XProcessingInstruction;
			return xprocessingInstruction != null && this.target == xprocessingInstruction.target && this.data == xprocessingInstruction.data;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000087FD File Offset: 0x000069FD
		internal override int GetDeepHashCode()
		{
			return this.target.GetHashCode() ^ this.data.GetHashCode();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008816 File Offset: 0x00006A16
		private static void ValidateName(string name)
		{
			XmlConvert.VerifyNCName(name);
			if (string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ArgumentException(Res.GetString("Argument_InvalidPIName", new object[]
				{
					name
				}));
			}
		}

		// Token: 0x040000A3 RID: 163
		internal string target;

		// Token: 0x040000A4 RID: 164
		internal string data;
	}
}
