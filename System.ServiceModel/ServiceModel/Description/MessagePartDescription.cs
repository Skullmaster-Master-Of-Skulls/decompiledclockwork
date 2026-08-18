using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.Reflection;
using System.ServiceModel.Security;

namespace System.ServiceModel.Description
{
	// Token: 0x020003D3 RID: 979
	[DebuggerDisplay("Name={name}, Namespace={ns}, Type={Type}, Index={index}}")]
	[__DynamicallyInvokable]
	public class MessagePartDescription
	{
		// Token: 0x060024C7 RID: 9415 RVA: 0x00084A5C File Offset: 0x00082C5C
		[__DynamicallyInvokable]
		public MessagePartDescription(string name, string ns)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name", SR.GetString("SFxParameterNameCannotBeNull"));
			}
			this.name = new XmlName(name, true);
			if (!string.IsNullOrEmpty(ns))
			{
				NamingHelper.CheckUriParameter(ns, "ns");
			}
			this.ns = ns;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00084AB4 File Offset: 0x00082CB4
		internal MessagePartDescription(MessagePartDescription other)
		{
			this.name = other.name;
			this.ns = other.ns;
			this.index = other.index;
			this.type = other.type;
			this.serializationPosition = other.serializationPosition;
			this.hasProtectionLevel = other.hasProtectionLevel;
			this.protectionLevel = other.protectionLevel;
			this.memberInfo = other.memberInfo;
			this.multiple = other.multiple;
			this.additionalAttributesProvider = other.additionalAttributesProvider;
			this.baseType = other.baseType;
			this.uniquePartName = other.uniquePartName;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00084B57 File Offset: 0x00082D57
		internal virtual MessagePartDescription Clone()
		{
			return new MessagePartDescription(this);
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x00084B5F File Offset: 0x00082D5F
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x00084B67 File Offset: 0x00082D67
		internal string BaseType
		{
			get
			{
				return this.baseType;
			}
			set
			{
				this.baseType = value;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x00084B70 File Offset: 0x00082D70
		internal XmlName XmlName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x00084B78 File Offset: 0x00082D78
		internal string CodeName
		{
			get
			{
				return this.name.DecodedName;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x00084B85 File Offset: 0x00082D85
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name.EncodedName;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x00084B92 File Offset: 0x00082D92
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x00084B9A File Offset: 0x00082D9A
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x00084BA2 File Offset: 0x00082DA2
		[__DynamicallyInvokable]
		public Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this.type;
			}
			[__DynamicallyInvokable]
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x00084BAB File Offset: 0x00082DAB
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x00084BB3 File Offset: 0x00082DB3
		[__DynamicallyInvokable]
		public int Index
		{
			[__DynamicallyInvokable]
			get
			{
				return this.index;
			}
			[__DynamicallyInvokable]
			set
			{
				this.index = value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x00084BBC File Offset: 0x00082DBC
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x00084BC4 File Offset: 0x00082DC4
		[DefaultValue(false)]
		[__DynamicallyInvokable]
		public bool Multiple
		{
			[__DynamicallyInvokable]
			get
			{
				return this.multiple;
			}
			[__DynamicallyInvokable]
			set
			{
				this.multiple = value;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x00084BCD File Offset: 0x00082DCD
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x00084BD5 File Offset: 0x00082DD5
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.protectionLevel = value;
				this.hasProtectionLevel = true;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00084C02 File Offset: 0x00082E02
		public bool HasProtectionLevel
		{
			get
			{
				return this.hasProtectionLevel;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x00084C0A File Offset: 0x00082E0A
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x00084C12 File Offset: 0x00082E12
		[__DynamicallyInvokable]
		public MemberInfo MemberInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this.memberInfo;
			}
			[__DynamicallyInvokable]
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x00084C1B File Offset: 0x00082E1B
		// (set) Token: 0x060024DC RID: 9436 RVA: 0x00084C2D File Offset: 0x00082E2D
		internal ICustomAttributeProvider AdditionalAttributesProvider
		{
			get
			{
				return this.additionalAttributesProvider ?? this.memberInfo;
			}
			set
			{
				this.additionalAttributesProvider = value;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x00084C36 File Offset: 0x00082E36
		// (set) Token: 0x060024DE RID: 9438 RVA: 0x00084C3E File Offset: 0x00082E3E
		internal string UniquePartName
		{
			get
			{
				return this.uniquePartName;
			}
			set
			{
				this.uniquePartName = value;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x00084C47 File Offset: 0x00082E47
		// (set) Token: 0x060024E0 RID: 9440 RVA: 0x00084C4F File Offset: 0x00082E4F
		internal int SerializationPosition
		{
			get
			{
				return this.serializationPosition;
			}
			set
			{
				this.serializationPosition = value;
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00084C58 File Offset: 0x00082E58
		internal void ResetProtectionLevel()
		{
			this.protectionLevel = ProtectionLevel.None;
			this.hasProtectionLevel = false;
		}

		// Token: 0x0400208E RID: 8334
		private XmlName name;

		// Token: 0x0400208F RID: 8335
		private string ns;

		// Token: 0x04002090 RID: 8336
		private int index;

		// Token: 0x04002091 RID: 8337
		private Type type;

		// Token: 0x04002092 RID: 8338
		private int serializationPosition;

		// Token: 0x04002093 RID: 8339
		private ProtectionLevel protectionLevel;

		// Token: 0x04002094 RID: 8340
		private bool hasProtectionLevel;

		// Token: 0x04002095 RID: 8341
		private MemberInfo memberInfo;

		// Token: 0x04002096 RID: 8342
		private ICustomAttributeProvider additionalAttributesProvider;

		// Token: 0x04002097 RID: 8343
		private bool multiple;

		// Token: 0x04002098 RID: 8344
		private string baseType;

		// Token: 0x04002099 RID: 8345
		private string uniquePartName;
	}
}
